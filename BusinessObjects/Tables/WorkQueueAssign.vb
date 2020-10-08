'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/25/2012)  ********************

Public Class WorkQueueAssign
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New WorkQueueAssignDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New WorkQueueAssignDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(WorkQueueAssignDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ASSIGN_ID), Byte()))
            End If
        End Get
    End Property


    Public Property WorkqueueId As Guid
        Get
            CheckDeleted()
            If Row(WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ID, Value)
        End Set
    End Property



    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(WorkQueueAssignDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkQueueAssignDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WorkQueueAssignDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property



    Public Property UserId As Guid
        Get
            CheckDeleted()
            If Row(WorkQueueAssignDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(WorkQueueAssignDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WorkQueueAssignDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New WorkQueueAssignDAL
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

    Public Sub Copy(original As RuleIssue)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Rule Issue.")
        End If
        CopyFrom(original)
    End Sub

#End Region

#Region "WorkqueueAssign Selection View"
    Public Class WorkQueueAssignList
        Inherits BusinessObjectListBase
        ReadOnly const_today As DateTime = DateTime.Now
        Public Sub New(parent As User)
            MyBase.New(LoadTable(parent), GetType(WorkQueueAssign), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, WorkQueueAssign).UserId.Equals(CType(Parent, User).Id)
        End Function

        Private Shared Function LoadTable(parent As User) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(WorkQueueAssignList)) Then
                    Dim dal As New WorkQueueAssignDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(WorkQueueAssignList))
                End If
                Return parent.Dataset.Tables(WorkQueueAssignDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class



#End Region

End Class


