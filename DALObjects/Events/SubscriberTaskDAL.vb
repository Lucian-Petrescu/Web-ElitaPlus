'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2012)********************


Public Class SubscriberTaskDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SUBSCRIBER_TASK"
    Public Const TABLE_KEY_NAME As String = "subscriber_task_id"

    Public Const COL_NAME_SUBSCRIBER_TASK_ID As String = "subscriber_task_id"
    Public Const COL_NAME_TASK_ID As String = "task_id"
    Public Const COL_NAME_SUBSCRIBER_TYPE_ID As String = "subscriber_type_id"
    Public Const COL_NAME_SUBSCRIBER_STATUS_ID As String = "subscriber_status_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("subscriber_task_id", id.ToByteArray)}
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

    Public Function LoadList(TaskID As Guid, SubscriberTypeID As Guid, SubscriberStatusID As Guid, _
                             LanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = "", strTemp As String
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", LanguageID.ToByteArray)}

        If TaskID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and st.Task_ID = " & MiscUtil.GetDbStringFromGuid(TaskID, True)
        End If

        If SubscriberStatusID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and st.SUBSCRIBER_STATUS_ID = " & MiscUtil.GetDbStringFromGuid(SubscriberStatusID, True)
        End If

        If SubscriberTypeID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and st.SUBSCRIBER_TYPE_ID = " & MiscUtil.GetDbStringFromGuid(SubscriberTypeID, True)
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
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


