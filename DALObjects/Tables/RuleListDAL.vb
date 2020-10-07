'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/25/2012)********************


Public Class RuleListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_RULE_LIST"
    Public Const TABLE_KEY_NAME As String = "rule_list_id"

    Public Const COL_NAME_RULE_LIST_ID As String = "rule_list_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("rule_list_id", id.ToByteArray)}
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


    Public Function GetList(code As String, Description As String, ActiveOn As DateTimeType) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_SEARCH")
        Dim whereClauseConditions As String = ""

        Dim parameters() As OracleParameter = _
            New OracleParameter() {New OracleParameter("Code", OracleDbType.Varchar2), _
                                   New OracleParameter("Description", OracleDbType.Varchar2)}

        If code.Contains(ASTERISK) Then code = code.Replace(ASTERISK, WILDCARD_CHAR) Else code &= WILDCARD_CHAR
        If Description.Contains(ASTERISK) Then Description = Description.Replace(ASTERISK, WILDCARD_CHAR) Else Description &= WILDCARD_CHAR

        If Not ActiveOn = Nothing Then
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.Length - 1) = New OracleParameter("ActiveOn", OracleDbType.Varchar2, 10)
            parameters(parameters.Length - 1).Value = ActiveOn.Value.ToString("MM/dd/yyyy")
            selectStmt &= " AND TO_DATE(:ACTIVEON,'MM/DD/YYYY') BETWEEN trunc(effective,'DDD') AND trunc(expiration,'DDD')"
        End If

        Dim ds As New DataSet
        parameters(0).Value = code                      'populate code
        parameters(1).Value = Description               'populate description

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim RuleListDet As New RuleListDetailDAL
        Dim DealerRule As New DealerRuleListDAL
        Dim CompanyRule As New CompanyRuleListDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            RuleListDet.Update(familyDataset, tr, DataRowState.Deleted)
            DealerRule.Update(familyDataset, tr, DataRowState.Deleted)
            CompanyRule.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            RuleListDet.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            DealerRule.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            CompanyRule.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

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

#End Region


End Class


