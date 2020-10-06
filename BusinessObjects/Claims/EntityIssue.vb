'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/26/2012)  ********************

Public Class EntityIssue
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
            Dim dal As New EntityIssueDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New EntityIssueDAL
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
            If row(EntityIssueDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(EntityIssueDAL.COL_NAME_ENTITY_ISSUE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Entity() As String
        Get
            CheckDeleted()
            If row(EntityIssueDAL.COL_NAME_ENTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(EntityIssueDAL.COL_NAME_ENTITY), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(EntityIssueDAL.COL_NAME_ENTITY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Protected Overridable Property EntityId() As Guid
        Get
            CheckDeleted()
            If row(EntityIssueDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(EntityIssueDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(EntityIssueDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IssueId() As Guid
        Get
            CheckDeleted()
            If Row(EntityIssueDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EntityIssueDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(EntityIssueDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property

    Protected Overridable Property WorkQueueItemCreatedId() As Guid
        Get
            CheckDeleted()
            If Row(EntityIssueDAL.COL_NAME_WORKQUEUE_ITEM_CREATED_ID) Is DBNull.Value Then
                Return LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
            Else
                Return New Guid(CType(Row(EntityIssueDAL.COL_NAME_WORKQUEUE_ITEM_CREATED_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(EntityIssueDAL.COL_NAME_WORKQUEUE_ITEM_CREATED_ID, Value)
        End Set
    End Property
    Public ReadOnly Property EntityIssueData() As String
        Get
            CheckDeleted()
            If Row(EntityIssueDAL.COL_NAME_ENTITY_ISSUE_DATA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EntityIssueDAL.COL_NAME_ENTITY_ISSUE_DATA), String)
            End If
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Shared Function GetClaimIssuesByIssueType(issueTypeId As Guid) As DataView
        Dim dal As New EntityIssueDAL
        return dal.LoadClaimIssuesByIssueType(issueTypeId, ElitaPlusIdentity.Current.ActiveUser.Id).Tables(0).DefaultView                    
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class
