'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/24/2012)********************


Public Class RuleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_RULE"
    Public Const TABLE_KEY_NAME As String = "rule_id"

    Public Const COL_NAME_RULE_ID As String = "rule_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_RULE_TYPE_ID As String = "rule_type_id"
    Public Const COL_NAME_RULE_CATEGORY_ID As String = "rule_category_id"
    Public Const COL_NAME_RULE_EXECUTION_POINT As String = "rule_execution_point"
    Public Const COL_NAME_RULE_DATA_SET As String = "rule_data_set"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const WILDCARD As Char = "%"
    Public Const ELITA_WILDCARD As Char = "*"
    Private Const const_AND As String = " AND "
    Private Const const_OR As String = " OR "
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DATE As String = "nowdate"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("rule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim Rule_Issue As New RuleIssueDAL
        Dim Rule_Process As New RuleProcessDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            Rule_Issue.Update(familyDataset, tr, DataRowState.Deleted)
            Rule_Process.Update(familyDataset, tr, DataRowState.Deleted)

            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            Rule_Issue.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            Rule_Process.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)


            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Function LoadRulesByDealerAndCompnay(dealerId As Guid, company_id As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_RULES_BY_DEALER_AND_COMPANY")

        Dim parameters() As OracleParameter =
           New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, OracleDbType.Raw, 16),
                                  New OracleParameter(COL_NAME_COMPANY_ID, OracleDbType.Raw, 16)}


        Dim ds As New DataSet

        parameters(0).Value = dealerId.ToByteArray
        parameters(1).Value = company_id.ToByteArray


        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return New DataView(ds.Tables(TABLE_NAME))
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

    Function IsIssueAssignedtoRule(IssueId As Guid) As Boolean
        Dim selectStmt As String = Config("/SQL/IsIssueAssignedtoRule")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_id", IssueId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables(TABLE_NAME).Rows.Count > 0 Then Return True Else Return False
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

    Function getList(Code As String, Description As String, ActiveOn As DateTimeType, _
                     ruleType As Guid, ruleCategory As Guid, lang_id As Guid) As DataTable

        Dim DS As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_SEARCH_LIST")
        Dim parameters() As OracleParameter = _
            New OracleParameter() {New OracleParameter("Code", OracleDbType.Varchar2), _
                                   New OracleParameter("Description", OracleDbType.Varchar2), _
                                   New OracleParameter("LANG_ID", OracleDbType.Raw, 16)}

        If Code.Contains(ASTERISK) Then Code = Code.Replace(ASTERISK, WILDCARD_CHAR) Else Code &= WILDCARD_CHAR
        If Description.Contains(ASTERISK) Then Description = Description.Replace(ASTERISK, WILDCARD_CHAR) Else Description &= WILDCARD_CHAR

        parameters(0).Value = Code                      'populate code
        parameters(1).Value = Description               'populate description
        parameters(2).Value = lang_id.ToByteArray            'populate language id

        If Not ruleType = Guid.Empty Then
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.Length - 1) = New OracleParameter("RuleType", OracleDbType.Raw, 16)
            parameters(parameters.Length - 1).Value = ruleType.ToByteArray
            selectStmt &= " AND " & RuleDAL.COL_NAME_RULE_TYPE_ID & " = :RuleType"
        End If
        If Not ruleCategory = Guid.Empty Then
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.Length - 1) = New OracleParameter("RuleCategory", OracleDbType.Raw, 16)
            parameters(parameters.Length - 1).Value = ruleCategory.ToByteArray
            selectStmt &= " AND " & RuleDAL.COL_NAME_RULE_CATEGORY_ID & " = :RuleCategory"
        End If

        If Not ActiveOn = Nothing Then
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.Length - 1) = New OracleParameter("ActiveOn", OracleDbType.Varchar2, 10)
            parameters(parameters.Length - 1).Value = ActiveOn.Value.ToString("MM/dd/yyyy")
            selectStmt &= " AND TO_DATE(:ACTIVEON,'MM/DD/YYYY') BETWEEN trunc(effective,'DDD') AND trunc(expiration,'DDD')"
        End If

        Try
            Return DBHelper.Fetch(DS, selectStmt, RuleListDAL.TABLE_NAME, parameters).Tables(0)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


End Class


