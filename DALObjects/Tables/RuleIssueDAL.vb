'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/30/2012)********************


Public Class RuleIssueDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_RULE_ISSUE"
    Public Const TABLE_KEY_NAME As String = "rule_issue_id"

    Public Const COL_NAME_RULE_ISSUE_ID As String = "rule_issue_id"
    Public Const COL_NAME_ISSUE_ID As String = "issue_id"
    Public Const COL_NAME_RULE_ID As String = "rule_id"
    Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("rule_issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, RuleId As Guid, CurrentDate As DateTime)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter(COL_NAME_RULE_ID, OracleDbType.Raw, 16)}

        parameters(0).Value = RuleId.ToByteArray

        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_RULE_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim Rule_Issue As New RuleIssueDAL
        'Dim DealerRule As New DealerRuleListDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            Rule_Issue.Update(familyDataset, tr, DataRowState.Deleted)
            'DealerRule.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            Rule_Issue.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            'DealerRule.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)


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

    Public Function IsChild(RuleId As Guid, IssueId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet

        Try
            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(COL_NAME_RULE_ID, RuleId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRulesInList(IssueId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/RULES_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Try
            IssueIdParam = New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {IssueIdParam})
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


End Class


