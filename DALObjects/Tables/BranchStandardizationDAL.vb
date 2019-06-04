'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2008)********************


Public Class BranchStandardizationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BRANCH_STANDARDIZATION"
    Public Const TABLE_KEY_NAME As String = "branch_standardization_id"
    Public Const DSNAME As String = "LIST"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"

    Public Const COL_NAME_BRANCH_STANDARDIZATION_ID As String = "branch_standardization_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_BRANCH_CODE As String = "dealer_branch_code"
    Public Const COL_NAME_BRANCH_ID As String = "branch_id"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"



    Public Const DEALER_ID = 0
    Public Const BRANCH_ID = 1
    Public Const DEALER_BRANCH_CODE_ID = 2
    Public Const TOTAL_PARAM = 2 '3 '4

    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("branch_standardization_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function


    'Public Function LoadList(ByVal companyIds As ArrayList, ByVal dealerId As Guid, ByVal dealerBranchCode As String, ByVal BranchId As Guid) As DataSet

    '    Dim selectStmt As String

    '    If companyIds.Count > 1 Then
    '        selectStmt = Me.Config("/SQL/LOAD_LIST_MULTIPLE_COMPANIES")
    '    Else
    '        selectStmt = Me.Config("/SQL/LOAD_LIST")
    '    End If

    '    Dim parameters(TOTAL_PARAM) As OracleParameter

    '    If dealerId.Equals(Guid.Empty) Then
    '        parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, WILDCARD)
    '    Else
    '        parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)
    '    End If

    '    If BranchId.Equals(Guid.Empty) Then
    '        parameters(BRANCH_ID) = New OracleParameter(Me.COL_NAME_BRANCH_ID, WILDCARD)
    '    Else
    '        parameters(BRANCH_ID) = New OracleParameter(Me.COL_NAME_BRANCH_ID, BranchId.ToByteArray)
    '    End If

    '    parameters(DEALER_BRANCH_CODE_ID) = New OracleParameter(COL_NAME_DEALER_BRANCH_CODE, dealerBranchCode)

    '    Dim inClauseCondition As String = MiscUtil.BuildListForSql("AND d." & Me.COL_NAME_COMPANY_ID, companyIds, True)
    '    selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

    '    Try
    '        Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function GetBranchAliasList(ByVal description As String, _
                                    ByVal branchId As Guid, _
                                    ByVal dealerId As Guid, _
                                    ByVal companyIds As ArrayList) As DataSet
        Dim selectStmt As String
        If companyIds.Count > 1 Then
            selectStmt = Me.Config("/SQL/LOAD_LIST_MULTIPLE_COMPANIES")
        Else
            selectStmt = Me.Config("/SQL/LOAD_LIST")
        End If

        Dim parameters() As OracleParameter
        Dim ds As New DataSet

        If (Not description Is Nothing AndAlso Not (description.Equals(String.Empty))) AndAlso (Me.FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND brchstand." & Me.COL_NAME_DEALER_BRANCH_CODE & description
        End If

        If Not branchId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND brchstand." & Me.COL_NAME_BRANCH_ID & " = '" & Me.GuidToSQLString(branchId) & "'"
        End If

        selectStmt &= Environment.NewLine & MiscUtil.BuildListForSql("AND d." & Me.COL_NAME_COMPANY_ID, companyIds, True)

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(brchstand." & Me.COL_NAME_DEALER_BRANCH_CODE & ")"

        If Not dealerId.Equals(Guid.Empty) Then
            parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DEALER_ID, dealerId.ToByteArray)}
        Else
            parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DEALER_ID, WILDCARD)}
        End If
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds

    End Function

#End Region

#Region "Overloaded Methods"

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

#End Region


End Class


