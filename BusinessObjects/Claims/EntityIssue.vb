'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/26/2012)  ********************

Public Class EntityIssue
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
            Dim dal As New EntityIssueDAL
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
            Dim dal As New EntityIssueDAL
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EntityIssueDAL.COL_NAME_ENTITY, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EntityIssueDAL.COL_NAME_ENTITY_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EntityIssueDAL.COL_NAME_ISSUE_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EntityIssueDAL.COL_NAME_WORKQUEUE_ITEM_CREATED_ID, Value)
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
    Public Shared Function GetClaimIssuesByIssueType(ByVal issueTypeId As Guid) As DataView
        Dim dal As New EntityIssueDAL
        return dal.LoadClaimIssuesByIssueType(issueTypeId, ElitaPlusIdentity.Current.ActiveUser.Id).Tables(0).DefaultView                    
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class
