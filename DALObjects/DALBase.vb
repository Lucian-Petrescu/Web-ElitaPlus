Imports System.Text

Public Class DALBase

#Region "Private attributes"

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Constants"
    'Common Audit Columns
    Public Const COL_NAME_CREATED_DATE As String = "CREATED_DATE"
    Public Const COL_NAME_MODIFIED_DATE As String = "MODIFIED_DATE"
    Public Const COL_NAME_CREATED_BY As String = "CREATED_BY"
    Public Const COL_NAME_PROCESSED_BY As String = "CREATED_BY_NAME"
    Public Const COL_NAME_MODIFIED_BY As String = "MODIFIED_BY"
    Public Const COL_NAME_CODE As String = "CODE"
    Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"
    Public Const SYSTEM_SEQUENCE_COL_NAME As String = "_SYSTEM_SEQUENCE_COL_NAME_"
    Public Const COL_NAME_ID As String = "ID"
    Public Const COL_NAME_CLAIM_ID As String = "CLAIM_ID"

    Public Const WILDCARD_CHAR As Char = "%"
    Public Const SPECIAL_CHAR As String = "[%"
    Public Const ASTERISK As Char = "*"
    Public Const TICKMARK As String = "'"
    Public Const DOUBLETICKMARK As String = "''"
    Public Const LIKE_CLAUSE As String = " LIKE "
    Public Const EQUALS_CLAUSE As String = " = "
    Public Const MAX_NUMBER_OF_ROWS As Int32 = 101
    Public Const ORACLE_QUERY_DATEFORMAT = "mmddyyyy"
    Public Const DOTNET_QUERY_DATEFORMAT = "MMddyyyy"


    Public Const DYNAMIC_IN_CLAUSE_PLACE_HOLDER As String = "--dynamic_in_clause"
    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER As String = "--dynamic_where_clause"
    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER_AUTH As String = "--dynamic_where_auth_clause"
    Public Const DYNAMIC_JOIN_CLAUSE_PLACE_HOLDER As String = "--dynamic_join_clause"
    Public Const DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER As String = "--dynamic_order_by_clause"
    Public Const DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER As String = "--dynamic_field_selector"
    Public Const DYNAMIC_FROM_CLAUSE_PLACE_HOLDER As String = "--dynamic_from_clause"

    Public Const PAR_NAME_ROW_NUMBER As String = "row_num"
    Public Const PAR_NAME_COMPANY_ID As String = "company_id"

    Public Const COL_NAME_DATE_TO_CONVERT As String = "dateToConvert"
    Public Const COL_NAME_TO_TIME_ZONE As String = "fromTimeZone"

    'US 224101 
    Public Const PAR_OUT_NAME_RETURN_CODE As String = "po_return_code"

#End Region



    'Retrieve the node value from the specific descriptor of the object 
    Public ReadOnly Property Config(nodeXpath As String) As String
        Get
            Return ConfigReader.GetNodeValue([GetType], nodeXpath)
        End Get
    End Property

    'Retrieve the node value from the DALBase descriptor
    Public ReadOnly Property ConfigCommon(nodeXpath As String) As String
        Get
            Return ConfigReader.GetNodeValue(GetType(DALBase), nodeXpath)
        End Get
    End Property

#Region "Public Shared Methods"

    Public Shared Function GetOracleDate(dateColumn As String, Optional ByVal formatStr As String = "") As String

        If (formatStr Is Nothing) Or (formatStr = String.Empty) Or (formatStr.Length = 0) Then
            formatStr = ORACLE_QUERY_DATEFORMAT
        End If
        Return " to_char(" & dateColumn & " , '" & formatStr & "' )"

    End Function

    Public Shared Function GuidToSQLString(Value As Guid) As String
        Dim byteArray As Byte() = Value.ToByteArray
        Dim i As Integer
        Dim result As New StringBuilder("")
        For i = 0 To byteArray.Length - 1
            Dim hexStr As String = byteArray(i).ToString("X")
            If hexStr.Length < 2 Then
                hexStr = "0" & hexStr
            End If
            result.Append(hexStr)
        Next
        Return result.ToString
    End Function

    Protected Shared Sub AddSequenceColumn(table As DataTable)
        table.Columns.Add(SYSTEM_SEQUENCE_COL_NAME, GetType(Long))
        Dim i As Long
        For i = 0 To table.Rows.Count - 1
            table.Rows(i)(SYSTEM_SEQUENCE_COL_NAME) = i
        Next
    End Sub

    Protected Sub FormatToSQLString(ByRef str As String)

        If (Not (str.Equals(String.Empty))) Then
            If str.IndexOf(TICKMARK) > -1 Then
                str = str.Replace(TICKMARK, DOUBLETICKMARK)
            End If
            If ((str.Trim.Equals(WILDCARD_CHAR)) OrElse (str.Trim.Equals(ASTERISK))) Then
                str = String.Empty
            ElseIf (str.IndexOf(ASTERISK) > -1) Then
                str = str.Replace(ASTERISK, WILDCARD_CHAR)
            End If

        End If

    End Sub

    Protected Function FormatSearchMask(ByRef str As String) As Boolean

        If (Not str Is Nothing) AndAlso (Not (str.Equals(String.Empty))) Then
            If str.IndexOf(TICKMARK) > -1 Then
                str = str.Replace(TICKMARK, DOUBLETICKMARK)
            End If
            If (str.Trim.Equals(WILDCARD_CHAR) OrElse (str.Trim.Equals(ASTERISK))) Then
                Return (False)
            ElseIf (str.IndexOf(ASTERISK) > -1) Then
                str = LIKE_CLAUSE & "'" & str.Replace(ASTERISK, WILDCARD_CHAR) & "'"
            Else
                str = EQUALS_CLAUSE & "'" & str & "'"
            End If

            Return (True)
        End If
        Return (False)

    End Function

    Protected Function DictFormatSearchMask(ByRef str As String) As Boolean

        If (Not str Is Nothing) AndAlso (Not (str.Equals(String.Empty))) Then

            If str.IndexOf(TICKMARK) > -1 Then
                str = str.Replace(TICKMARK, DOUBLETICKMARK)
            End If

            If (str.Trim.Equals(ASTERISK)) Then
                Return (False)
            End If

            If (str.IndexOf(WILDCARD_CHAR) > -1) Then
                If (str.IndexOf(ASTERISK) > -1) Then
                    str = LIKE_CLAUSE & "'" & (str.Replace(WILDCARD_CHAR, SPECIAL_CHAR))
                    str = (str.Replace(ASTERISK, WILDCARD_CHAR)) & "' ESCAPE '['"
                Else
                    str = LIKE_CLAUSE & "'" & str.Replace(WILDCARD_CHAR, SPECIAL_CHAR) & "' ESCAPE '['"
                End If
            ElseIf (str.IndexOf(ASTERISK) > -1) Then
                str = LIKE_CLAUSE & "'" & str.Replace(ASTERISK, WILDCARD_CHAR) & "'"
            Else
                str = EQUALS_CLAUSE & "'" & str & "'"
            End If

            Return (True)

        End If
        Return (False)

    End Function

    Protected Function IsLikeClause(str As String) As Boolean
        Dim bLikeClause As Boolean = False

        If (Not str Is Nothing) AndAlso (Not (str.Equals(String.Empty))) Then
            ' Is Not Empty ?
            If (str.Trim.Equals(WILDCARD_CHAR) OrElse (str.Trim.Equals(ASTERISK))) Then
                ' Does it have only % or *
                bLikeClause = False
            ElseIf (str.IndexOf(ASTERISK) > -1) Then
                ' Is there an *
                bLikeClause = True
            Else
                ' There is not an *
                bLikeClause = False
            End If
        End If
        Return (bLikeClause)

    End Function

    Public Function GetFormattedSearchStringForSQL(str As String) As String
        If (Not IsNothing(str)) Then
            If str.IndexOf(TICKMARK) > -1 Then
                str = str.Replace(TICKMARK, DOUBLETICKMARK)
            End If
            str = str.Trim
            str = str.ToUpper
            If (str.IndexOf(ASTERISK) > -1) Then
                str = str.Replace(ASTERISK, WILDCARD_CHAR)
            End If
        Else
            str &= WILDCARD_CHAR
        End If
        Return (str)
    End Function

    Public Shared Function IsNothing(Value As Object) As Boolean

        If Value Is Nothing Then
            Return True
        ElseIf (Value.GetType Is GetType(Guid)) Then
            If (Value.Equals(Guid.Empty)) Then
                Return True
            End If
        ElseIf Value.GetType Is GetType(String) Then
            If CType(Value, String).Trim.Length = 0 Then
                Return True
            Else
                Return False
            End If
        ElseIf Value = Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    'Time Zone Related 
    Public Function LoadConvertedTime_From_DB_ServerTimeZone(dateToConvert As DateTime, toTimeZone As String) As DataView

        Dim ds As New DataSet



        Dim selectStmt As String = Config("/SQL/CONVERTED_TIME")



        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_DATE_TO_CONVERT, dateToConvert),
                                            New OracleParameter(COL_NAME_TO_TIME_ZONE, toTimeZone)}
        Try
            DBHelper.Fetch(ds, selectStmt, "TimeConverted", parameters)
            Return New DataView(ds.Tables(0))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function


#End Region

#Region "Common CRUD Methods"
    'This Method assumes you have the nodes  "/SQL/INSERT", "/SQL/UPDATE", "/SQL/DELETE" in your xml file
    Public Overridable Sub Update(row As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        DBHelper.Execute(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
        LookupListCache.ClearFromCache([GetType].ToString)
    End Sub

    Public Overridable Sub UpdateWithParam(row As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        DBHelper.ExecuteWithParam(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
        LookupListCache.ClearFromCache([GetType].ToString)
    End Sub

    'This Method assumes you have the nodes  "/SQL/INSERT", "/SQL/UPDATE", "/SQL/DELETE" in your xml file
    Public Overridable Sub Update(table As DataTable, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        DBHelper.Execute(table, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
        LookupListCache.ClearFromCache([GetType].ToString)
    End Sub

    Public Overridable Sub UpdateWithParam(table As DataTable, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        DBHelper.ExecuteWithParam(table, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
        LookupListCache.ClearFromCache([GetType].ToString)
    End Sub

    Public Overridable Sub UpdateFromSP(table As DataTable,
                                      Optional ByVal transaction As OracleTransaction = Nothing,
                                      Optional ByVal changesFilter As DataRowState = DataRowState.Added Or DataRowState.Deleted Or DataRowState.Modified)

        ' When Filter is blank then do not do anything.
        If (Not (changesFilter Or DataRowState.Added = DataRowState.Added OrElse
                 changesFilter Or DataRowState.Modified = DataRowState.Modified OrElse
                 changesFilter Or DataRowState.Deleted = DataRowState.Deleted)) Then
            Return
        End If

        '' Create Data Adapter and Process Request
        Using da As OracleDataAdapter = CreateDataAdapter(table.TableName, transaction, changesFilter)
            ' First Execute Inserts, then Updates and finally Deletes
            If ((changesFilter And DataRowState.Added) = DataRowState.Added) Then
                ' Execute the Inserts
                If (Not table.GetChanges(DataRowState.Added) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Added))
                End If
            End If
            If ((changesFilter And DataRowState.Modified) = DataRowState.Modified) Then
                ' Execute the Inserts
                If (Not table.GetChanges(DataRowState.Modified) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Modified))
                End If
            End If
            If ((changesFilter And DataRowState.Deleted) = DataRowState.Deleted) Then
                ' Execute the Deletes
                If (Not table.GetChanges(DataRowState.Deleted) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Deleted))
                End If
            End If

        End Using

        LookupListCache.ClearFromCache([GetType].ToString)
    End Sub
#End Region

#Region "Overridable Methods"

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Update <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotImplementedException("Update Commands must be Configured")
    End Sub

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Insert <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotImplementedException("Insert Commands must be Configured")
    End Sub

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Delete <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotImplementedException("Delete Commands must be Configured")
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Creates an instance of new <see cref="OracleDataAdapter" /> and assembles Insert, Update and Delete
    ''' commands, Transaction and Connection Objects.
    ''' </summary>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <param name="transaction">Optional. Instance of <see cref="OracleTransaction" />. When the value is provided, the updates 
    ''' will be part of the transaction represented by value of this parameter. Also, the <see cref="OracleConnection" /> used by 
    ''' this <see cref="OracleTransaction" /> will be used for all <see cref="OracleCommand"> objects during
    ''' updates to database.</param>
    ''' <param name="changesFilter">Optional. When value is provided; only rows having one of the flag values will be updated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataAdapter(tableName As String,
                                       Optional ByVal transaction As OracleTransaction = Nothing,
                                       Optional ByVal changesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)
        Dim da As OracleDataAdapter
        Dim connection As OracleConnection

        If (transaction Is Nothing) Then
            connection = CreateConnection()
        Else
            connection = transaction.Connection
        End If

        da = New OracleDataAdapter()

        If ((changesFilter And DataRowState.Added) = DataRowState.Added) Then
            da.InsertCommand = CreateCommand(Config("/SQL/INSERT"), CommandType.StoredProcedure, connection, transaction)
            ConfigureInsertCommand(da.InsertCommand, tableName)
        End If
        If ((changesFilter And DataRowState.Modified) = DataRowState.Modified) Then
            da.UpdateCommand = CreateCommand(Config("/SQL/UPDATE"), CommandType.StoredProcedure, connection, transaction)
            ConfigureUpdateCommand(da.UpdateCommand, tableName)
        End If
        If ((changesFilter And DataRowState.Deleted) = DataRowState.Deleted) Then
            da.DeleteCommand = CreateCommand(Config("/SQL/DELETE"), CommandType.StoredProcedure, connection, transaction)
            ConfigureDeleteCommand(da.DeleteCommand, tableName)
        End If

        Return da
    End Function

#End Region

End Class
