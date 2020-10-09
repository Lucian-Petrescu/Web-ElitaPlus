'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/17/2012)********************


Public Class WorkqueueHistoryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_WORKQUEUE_ITEM_HISTORY"
    Public Const TABLE_KEY_NAME As String = "workqueue_item_hist_id"

    Public Const COL_NAME_WORKQUEUE_ITEM_HIST_ID As String = "workqueue_item_hist_id"
    Public Const COL_NAME_WORKQUEUE_ITEM_ID As String = "workqueue_item_id"
    Public Const COL_NAME_WORKQUEUE_ITEM_DESC As String = "workqueue_item_desc"
    Public Const COL_NAME_WORKQUEUE_ID As String = "workqueue_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_TIME_STAMP As String = "time_stamp"
    Public Const COL_NAME_HISTORY_ACTION_ID As String = "history_action_id"
    Public Const COL_NAME_REASON As String = "reason"
    Public Const COL_NAME_USER_NAME As String = "user_name"
    Public Const COL_NAME_ITEMS_ACCESSED As String = "Items_Accessed"
    Public Const COL_NAME_ITEMS_REDIRECTED As String = "Items_Redirected"
    Public Const COL_NAME_ITEMS_REQUEUED As String = "Items_Requeued"
    Public Const COL_NAME_ITEMS_PROCESSED As String = "Items_Processed"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("workqueue_item_hist_id", id.ToByteArray)}
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

    Public Function LoadWorkQueueItemHistory(WorkQueueId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_WORKQUEUE_HISTORY")
        Dim parameters() As OracleParameter

        'parameters = New OracleParameter() {New OracleParameter(PAR_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_WORKQUEUE_ID, WorkQueueId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME, TABLE_NAME, parameters))
            'Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadWorkQueueUsersActions(WorkQueueId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_WORKQUEUE_USERS_ACTIONS")
        Dim parameters() As OracleParameter

        'parameters = New OracleParameter() {New OracleParameter(PAR_NAME_LANGUAGE_ID, languageId.ToByteArray), _
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_WORKQUEUE_ID, WorkQueueId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME, TABLE_NAME, parameters))
            'Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME))
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
#End Region


End Class