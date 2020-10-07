'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/18/2018)  ********************

Public Class ListItemByEntity
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
            Dim dal As New ListItemByEntityDAL
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

    Protected Sub Load(ByVal id As Guid)               
        Try
            Dim dal As New ListItemByEntityDAL            
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
    Private _listCode As String
    Private _entityCode As String
    Private _listDescription As String
    Private _entityDescription As String
    Private _entityType As String
    Private _searchType As String
    Private Sub Initialize()        
    End Sub
#End Region


#Region "Properties"

    Public Property ListCode() As String
        Get
            Return _listCode
        End Get
        Set(ByVal Value As String)
            _listCode = Value
        End Set
    End Property

    Public Property EntityCode() As String
        Get
            Return _entityCode
        End Get
        Set(ByVal Value As String)
            _entityCode = Value
        End Set
    End Property
    Public Property ListDescription() As String
        Get
            Return _listDescription
        End Get
        Set(ByVal Value As String)
            _listDescription = Value
        End Set
    End Property

    Public Property EntityDescription() As String
        Get
            Return _entityDescription
        End Get
        Set(ByVal Value As String)
            _entityDescription = Value
        End Set
    End Property
    Public Property EntityType() As String
        Get
            Return _entityType
        End Get
        Set(ByVal Value As String)
            _entityType = Value
        End Set
    End Property

    Public Property SearchType() As String
        Get
            Return _searchType
        End Get
        Set(ByVal Value As String)
            _searchType = Value
        End Set
    End Property

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ListItemByEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ListItemByEntityDAL.COL_NAME_LIST_ITEM_BY_ENTITY_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
    Public Property EntityReference() As String
        Get
            CheckDeleted()
            If row(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property EntityReferenceId() As Guid
        Get
            CheckDeleted()
            If row(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ListItemByEntityDAL.COL_NAME_ENTITY_REFERENCE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property ListItemId() As Guid
        Get
            CheckDeleted()
            If row(ListItemByEntityDAL.COL_NAME_LIST_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ListItemByEntityDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ListItemByEntityDAL.COL_NAME_LIST_ITEM_ID, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ListItemByEntityDAL
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

#Region "DataView Retrieveing Methods"
    Public Shared Function GetSelectedListItem(ByVal oLanguageCode As String, ByVal entityRefId As Guid, ByVal listCode As String) As DataView
        Dim dal As New ListItemByEntityDAL
        Dim ds As DataSet
        ds = dal.LoadSelectedListItem(oLanguageCode, entityRefId, listCode)
        Return ds.Tables(ListItemByEntityDAL.TABLE_LIST_ITEM).DefaultView
    End Function

    Public Shared Function GetAvailableListItem(ByVal oLanguageCode As String, ByVal entityRefId As Guid, ByVal listCode As String) As DataView
        Dim dal As New ListItemByEntityDAL
        Dim ds As DataSet
        ds = dal.LoadAvailableListItem(oLanguageCode, entityRefId, listCode)
        Return ds.Tables(ListItemByEntityDAL.TABLE_LIST_ITEM).DefaultView
    End Function
    Public Function UpdateListItem(ByVal oEntityitem As ArrayList) As ArrayList
        Dim oItemId As Guid
        Dim oListItemByEntity As ListItemByEntity
        Dim oDataset As DataSet = New DataSet
        Dim oUserDAL As New ListItemByEntityDAL
        Try
            ' Create The New User Companies 
            For Each oItemId In oEntityitem
                oListItemByEntity = New ListItemByEntity(oDataset)
                oListItemByEntity.EntityReference = EntityReference
                oListItemByEntity.ListItemId = oItemId
                oListItemByEntity.EntityReferenceId = EntityReferenceId
                oListItemByEntity.Save()
            Next
            ' Update Delete, Insert
            oUserDAL.UpdateListItem(EntityReferenceId, ListCode, oDataset)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
    Public Shared Function LoadEntityList(ByVal oLanguageCode As String, ByVal oEntityRefId As Guid, ByVal listCode As String, ByVal entityType As String, ByVal shortByExpression As String) As System.Data.DataView
        Try
            Dim dal As New ListItemByEntityDAL
            Dim ds As New DataSet
            Return New System.Data.DataView(dal.LoadEntityList(oLanguageCode, oEntityRefId, listCode, entityType, shortByExpression).Tables(ListItemByEntityDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Function DeleteDropdown(ByVal listCode As String, ByVal listId As Guid) As Integer
        Dim dal As New ListItemByEntityDAL
        dal.DeleteDropdown(listCode, listId)
    End Function
#End Region

End Class


