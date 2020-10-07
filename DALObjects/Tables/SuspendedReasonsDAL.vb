'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/17/2013)********************
Imports System.Text

Public Class SuspendedReasonsDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SUSPENDED_REASONS"
    Public Const TABLE_KEY_NAME As String = "suspended_reason_id"

    Public Const COL_NAME_ID As String = "suspended_reason_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_CLAIM_ALLOWED_STR As String = "claim_allowed_str"
    Public Const COL_NAME_CLAIM_ALLOWED As String = "claim_allowed"

    Public Const MAX_NUMBER_OF_ROWS As Int32 = 101

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Dim dt As DataTable = ds.Tables.Add(TABLE_NAME)

        Dim guidTemp As New Guid

        '******************************************************************************
        '** WARNING
        '** The Structure Need to be the same as the Load_List Query in the Dal XML ***
        '******************************************************************************
        With dt.Columns
            .Add(COL_NAME_ID, guidTemp.ToByteArray.GetType)
            .Add(COL_NAME_DEALER_ID, guidTemp.ToByteArray.GetType)
            .Add(COL_NAME_DEALER_NAME, GetType(String))
            .Add(COL_NAME_CODE, GetType(String))
            .Add(COL_NAME_DESCRIPTION, GetType(String))
            .Add(COL_NAME_CLAIM_ALLOWED, GetType(String))
            .Add(COL_NAME_CLAIM_ALLOWED_STR, GetType(String))
            .Add(COL_NAME_CREATED_BY, GetType(String))
            .Add(COL_NAME_MODIFIED_BY, GetType(String))
        End With

    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid, Network_Id As String)
        Dim selectStmt As String = Config("/SQL/LOAD")
        '  Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("suspended_reason_id", id.ToByteArray)}
        Dim parameters() As OracleParameter

        selectStmt = selectStmt.Replace(":NETWORK_ID", "'" & Network_Id & "'")
        selectStmt = selectStmt.Replace(":SUSPENDED_REASON_ID", "hextoraw('" & GuidToSQLString(id) & "')")

        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
            '            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            'familyDS = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            'DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(CompanyGroupId As Guid, DealerId As Guid _
                             , strCode As String, strDec As String _
                             , Claim_Allowed As String, Network_Id As String) As DataSet


        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        selectStmt = selectStmt.Replace(":NETWORK_ID", "'" & Network_Id & "'")

        If DealerId <> Guid.Empty Then
            whereClauseConditions &= " AND SR.DEALER_ID = " & "hextoraw('" & GuidToSQLString(DealerId) & "')"
        End If

        If FormatSearchMask(strCode) Then
            whereClauseConditions &= " AND sr.code " & strCode.ToUpper
        End If

        If Claim_Allowed.Equals("Y") OrElse Claim_Allowed.Equals("N") Then
            whereClauseConditions &= " AND sr.Claim_Allowed = '" & Claim_Allowed & "'"
        End If

        If FormatSearchMask(strDec) Then
            whereClauseConditions &= " AND UPPER(sr.description) " & strDec.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub InserRow(row As DataRow, UserId As String, ByRef RowId As Guid)

        Dim selectStmt As String = Config("/SQL/INSERT")
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("suspended_reason_id", id.ToByteArray)}

        Dim ParmStr As String = ""
        Dim tmpDesc As String = ""

        Try
            RowId = New Guid(CType(row(COL_NAME_ID), Byte()))

            tmpDesc = row(COL_NAME_DESCRIPTION) & ""
            FormatToSQLString(tmpDesc)

            ' ** Insert statement from XML
            ' ** INSERT INTO elp_suspended_reason (suspended_reason_id, dealer_id,code,description,claim_allowed,created_by)

            ParmStr &= "hextoraw('" & GuidToSQLString(RowId) & "')" _
                       & ",hextoraw('" & ByteArrayToSQLString(row(COL_NAME_DEALER_ID)) & "')" _
                       & ",'" & row(COL_NAME_CODE) & "'" _
                       & ",'" & tmpDesc & "'" _
                       & ",'" & row(COL_NAME_CLAIM_ALLOWED) & "'" _
                       & ",'" & row(COL_NAME_CREATED_BY) & "'"

            '                       & ",GETCODEFROMLISTITEM('" & Me.ByteArrayToSQLString(row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_ID)) & "')" _

            selectStmt = selectStmt.Replace("--dynamic_Inser_Values", ParmStr)

            DBHelper.Execute(selectStmt, Nothing, Nothing, Nothing)

            LookupListCache.ClearFromCache([GetType].ToString)

        Catch ex As Exception
            Throw ex
            '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Sub UpdateRow(row As DataRow, ByRef RowId As Guid)

        Dim selectStmt As String = Config("/SQL/UPDATE")
        Dim tmpDesc As String = ""

        Try
            RowId = New Guid(CType(row(COL_NAME_ID), Byte()))

            tmpDesc = row(COL_NAME_DESCRIPTION) & ""
            FormatToSQLString(tmpDesc)

            selectStmt = selectStmt.Replace(":dealer_id", "hextoraw('" & ByteArrayToSQLString(row(COL_NAME_DEALER_ID)) & "')")
            selectStmt = selectStmt.Replace(":description", "'" & tmpDesc & "'")
            '            selectStmt = selectStmt.Replace(":claim_allowed", "GETCODEFROMLISTITEM('" & Me.ByteArrayToSQLString(row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_ID)) & "')")
            selectStmt = selectStmt.Replace(":claim_allowed", "'" & row(COL_NAME_CLAIM_ALLOWED) & "'")
            selectStmt = selectStmt.Replace(":modified_by", "'" & row(COL_NAME_MODIFIED_BY) & "'")
            selectStmt = selectStmt.Replace(":SUSPENDED_REASON_ID", "hextoraw('" & GuidToSQLString(RowId) & "')")

            DBHelper.Execute(selectStmt, Nothing, Nothing, Nothing)

        Catch ex As Exception
            Throw ex
            '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Shared Function ByteArrayToSQLString(byteArray As Byte()) As String
        Dim i As Integer
        Dim result As New StringBuilder

        For i = 0 To byteArray.Length - 1
            Dim hexStr As String = byteArray(i).ToString("X")
            If hexStr.Length < 2 Then
                hexStr = "0" & hexStr
            End If
            result.Append(hexStr)
        Next

        Return result.ToString
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


