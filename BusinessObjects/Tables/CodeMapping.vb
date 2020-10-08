'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/1/2008)  ********************

Public Class CodeMapping
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
            Dim dal As New CodeMappingDAL
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
            Dim dal As New CodeMappingDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(CodeMappingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CodeMappingDAL.COL_NAME_CODE_MAPPING_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(CodeMappingDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CodeMappingDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CodeMappingDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ListItemId As Guid
        Get
            CheckDeleted()
            If row(CodeMappingDAL.COL_NAME_LIST_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CodeMappingDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CodeMappingDAL.COL_NAME_LIST_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property NewDescription As String
        Get
            CheckDeleted()
            If row(CodeMappingDAL.COL_NAME_NEW_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CodeMappingDAL.COL_NAME_NEW_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CodeMappingDAL.COL_NAME_NEW_DESCRIPTION, Value)
        End Set
    End Property


    Public ReadOnly Property AssociatedCodeMapping(id As Guid, Optional ByVal isNew As Boolean = False) As CodeMapping
        Get
            If Not isNew Then
                Return New CodeMapping(id, Dataset)
            Else
                Return New CodeMapping(Dataset)
            End If
        End Get
    End Property

    Public ReadOnly Property MyDataset As DataSet
        Get
            Return Dataset
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CodeMappingDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
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

    'Public Shared Function AdminLoadListItems(ByVal LanguageId As Guid, ByVal ListId As Guid) As System.Data.DataView
    '    Try
    '        Dim dal As New CodeMappingDAL
    '        Dim ds As New DataSet
    '        dal.AdminLoadListItems(ds, LanguageId, ListId)
    '        Return New System.Data.DataView(ds.Tables(DropdownItemDAL.TABLE_NAME))
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function

    Public Shared Function AdminLoadListItems(LanguageId As Guid, ListId As Guid, companyId As Guid) As ListItemSearchDV
        Try
            Dim dal As New CodeMappingDAL
            'Dim ds As DataSet

            Return New ListItemSearchDV(dal.AdminLoadListItems(LanguageId, ListId, companyId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCovertedCode(companyID As Guid, strListCode As String, strListItemCode As String) As String
        Dim dal As New CodeMappingDAL
        Return dal.LoadCovertedCode(companyID, strListCode, strListItemCode)
    End Function
#End Region
    Public Class ListItemSearchDV
        Inherits DataView

#Region "Constants"

        Public Const GRID_COL_LIST_ITEM_ID As String = "list_item_id"
        Public Const GRID_COL_CODE As String = "code"
        Public Const GRID_COL_DESCRIPTION As String = "description"
        Public Const GRID_COL_NEW_DESCRIPTION As String = "new_description"
        Public Const GRID_COL_CODE_MAPPING_ID As String = "code_mapping_id"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
End Class



