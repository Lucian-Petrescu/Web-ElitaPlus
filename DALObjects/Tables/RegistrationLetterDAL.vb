'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/6/2008)********************


Public Class RegistrationLetterDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REGISTRATION_LETTER"
    Public Const TABLE_KEY_NAME As String = "registration_letter_id"

    Public Const COL_NAME_REGISTRATION_LETTER_ID As String = "registration_letter_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_LETTER_TYPE As String = "letter_type"
    Public Const COL_NAME_NUMBER_OF_DAYS As String = "number_of_days"
    Public Const COL_NAME_EMAIL_SUBJECT As String = "email_subject"
    Public Const COL_NAME_EMAIL_TEXT As String = "email_text"
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_EMAIL_FROM As String = "email_from"
    Public Const COL_NAME_EMAIL_TO As String = "email_to"
    Public Const COL_NAME_ATTACHMENT_FILE_NAME As String = "attachment_file_name"
    'Public Const COL_NAME_ATTACHMENT_FILE_DATA As String = "attachment_file_data"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("registration_letter_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compIds As ArrayList, dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""

        inClausecondition &= "And edealer." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, False)

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "edealer.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMaxDay(dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_MAX_DAY")

        Try
            Dim ds As New DataSet
            Dim dealerIdPar As New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {dealerIdPar})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Handle Attachment File"
    Public Sub UpdateAttachment(rowID As Guid, data As Byte())
        Dim sql As String = Config("/SQL/ADD_ATTACHMENT") '"UPDATE elp_registration_letter SET attachment_file_data = :attachment_file_data WHERE registration_letter_id = " & MiscUtil.GetDbStringFromGuid(rowID)
        sql = sql.Replace(":registration_letter_id", MiscUtil.GetDbStringFromGuid(rowID))
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Try
            Dim cmd As New OracleCommand(sql, con)
            Dim filedata As New OracleParameter
            With filedata
                .OracleDbType = OracleDbType.Blob
                .ParameterName = "attachment_file_data"
                .IsNullable = True
                If data Is Nothing Then
                    .Value = DBNull.Value
                Else
                    .Value = data
                End If
            End With
            cmd.Parameters.Add(filedata)

            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region
    

End Class


