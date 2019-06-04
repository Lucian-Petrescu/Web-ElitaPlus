'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2012)  ********************

Public Class SubscriberTask
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New SubscriberTaskDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New SubscriberTaskDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
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
            If row(SubscriberTaskDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_TASK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TaskId() As Guid
        Get
            CheckDeleted()
            If row(SubscriberTaskDAL.COL_NAME_TASK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SubscriberTaskDAL.COL_NAME_TASK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SubscriberTaskDAL.COL_NAME_TASK_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property SubscriberTypeId() As Guid
        Get
            CheckDeleted()
            If row(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property SubscriberStatusId() As Guid
        Get
            CheckDeleted()
            If row(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SubscriberTaskDAL.COL_NAME_SUBSCRIBER_STATUS_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New SubscriberTaskDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal TaskID As Guid, ByVal SubscriberTypeID As Guid, ByVal SubscriberStatusID As Guid) As SubscriberTaskSearchDV
        Try
            Dim dal As New SubscriberTaskDAL
            Return New SubscriberTaskSearchDV(dal.LoadList(TaskID, SubscriberTypeID, SubscriberStatusID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Search DV"
    Public Class SubscriberTaskSearchDV
        Inherits DataView

        Public Const COL_SUBSCRIBER_TASK_ID As String = "SUBSCRIBER_TASK_ID"
        Public Const COL_TASK_ID As String = "TASK_ID"
        Public Const COL_SUBSCRIBER_TYPE_ID As String = "SUBSCRIBER_TYPE_ID"
        Public Const COL_SUBSCRIBER_TYPE_DESC As String = "SUBSCRIBER_TYPE_DESC"
        Public Const COL_SUBSCRIBER_STATUS_ID As String = "SUBSCRIBER_STATUS_ID"
        Public Const COL_SUBSCRIBER_STATUS_DESC As String = "SUBSCRIBER_STATUS_DESC"
        Public Const COL_TASK_CODE As String = "TASK_CODE"
        Public Const COL_TASK_DESC As String = "TASK_DESC"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As SubscriberTaskSearchDV, ByVal NewBO As SubscriberTask)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(SubscriberTaskSearchDV.COL_SUBSCRIBER_TASK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(SubscriberTaskSearchDV.COL_TASK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC, GetType(String))
                dt.Columns.Add(SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_DESC, GetType(String))
                dt.Columns.Add(SubscriberTaskSearchDV.COL_TASK_CODE, GetType(String))
                dt.Columns.Add(SubscriberTaskSearchDV.COL_TASK_DESC, GetType(String))
                
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(SubscriberTaskSearchDV.COL_SUBSCRIBER_TASK_ID) = NewBO.Id.ToByteArray
            row(SubscriberTaskSearchDV.COL_TASK_ID) = NewBO.TaskId.ToByteArray
            row(SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_ID) = NewBO.SubscriberTypeId.ToByteArray
            row(SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_ID) = NewBO.SubscriberStatusId.ToByteArray            
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New SubscriberTaskSearchDV(dt)
        End If
    End Sub
#End Region
End Class


