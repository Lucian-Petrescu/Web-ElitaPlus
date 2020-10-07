'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/17/2012)  ********************

Public Class WorkqueueHistory
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New WorkqueueHistoryDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New WorkqueueHistoryDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(WorkqueueHistoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_HIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property WorkqueueItemId() As Guid
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property WorkqueueId() As Guid
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property UserId() As Guid
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkqueueHistoryDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TimeStamp() As DateType
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_TIME_STAMP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(WorkqueueHistoryDAL.COL_NAME_TIME_STAMP), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_TIME_STAMP, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property HistoryActionId() As Guid
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_HISTORY_ACTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkqueueHistoryDAL.COL_NAME_HISTORY_ACTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_HISTORY_ACTION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property Reason() As String
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WorkqueueHistoryDAL.COL_NAME_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_REASON, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property WorkqueueItemDesc() As String
        Get
            CheckDeleted()
            If Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(WorkqueueHistoryDAL.COL_NAME_WORKQUEUE_ITEM_DESC, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New WorkqueueHistoryDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Shared Members"

    Public Shared Sub AddItem(ByVal wkQItemId As Guid, ByVal wkQId As Guid, ByVal reason As String, ByVal actionCode As String, ByVal wkQItemDesc As String)

        Dim oHist As WorkqueueHistory = New WorkqueueHistory()
        oHist.WorkqueueItemId = wkQItemId
        oHist.WorkqueueId = wkQId
        oHist.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
        oHist.Reason = reason
        oHist.HistoryActionId = LookupListNew.GetIdFromCode(LookupListCache.LK_WQ_HIST_ACTION, actionCode)
        oHist.WorkqueueItemDesc = wkQItemDesc
        oHist.TimeStamp = Date.Now()
        oHist.Save()

    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadWorkQueueItemHistory(ByVal WorkQueueId As Guid, ByVal languageId As Guid) As DataView
        Try
            Dim dal As New WorkqueueHistoryDAL
            Dim ds As DataSet

            ds = dal.LoadWorkQueueItemHistory(WorkQueueId, languageId)
            Return (ds.Tables(WorkqueueHistoryDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadWorkQueueUsersActions(ByVal WorkQueueId As Guid, ByVal languageId As Guid) As DataView
        Try
            Dim dal As New WorkqueueHistoryDAL
            Dim ds As DataSet

            ds = dal.LoadWorkQueueUsersActions(WorkQueueId, languageId)
            Return (ds.Tables(WorkqueueHistoryDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class