'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/7/2012)********************


Public Class RuleProcessDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PROCESS_RULE" '"ELP_RULE_PROCESS"
    Public Const TABLE_KEY_NAME As String = "rule_process_id"

    Public Const COL_NAME_RULE_PROCESS_ID As String = "rule_process_id"
    Public Const COL_NAME_RULE_ID As String = "rule_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_PROCESS_ID As String = "process_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_PROCESS_ORDER As String = "process_order"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("rule_process_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal RuleId As Guid, ByVal CurrentDate As DateTime) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        'Dim parameters() As OracleParameter = New OracleParameter() _
        '                                      {New OracleParameter(COL_NAME_RULE_ID, OracleDbType.Raw, 16), New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter(COL_NAME_RULE_ID, RuleId.ToByteArray)}


        'parameters(0).Value = RuleId.ToByteArray

        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim Rule_Process As New RuleProcessDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            Rule_Process.Update(familyDataset, tr, DataRowState.Deleted)            
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            Rule_Process.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction and we should commit it and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction and we should commit it and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Function IsChild(ByVal RuleProcessId As Guid, ByVal ProcessId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet

        Try
            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(Me.COL_NAME_PROCESS_ID, ProcessId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(Me.COL_NAME_RULE_PROCESS_ID, RuleProcessId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, params)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


