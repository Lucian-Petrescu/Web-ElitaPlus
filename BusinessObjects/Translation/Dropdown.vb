Public Class Dropdown
    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/12/2007)  ********************

    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
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
            Dim dal As New DropdownDAL
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
            Dim dal As New DropdownDAL
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
            If Row(DropdownDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DropdownDAL.COL_NAME_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property MaintainableByUser As String
        Get
            CheckDeleted()
            If Row(DropdownDAL.COL_NAME_MAINTAINABLE_BY_USER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownDAL.COL_NAME_MAINTAINABLE_BY_USER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DropdownDAL.COL_NAME_MAINTAINABLE_BY_USER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DictItemId As Guid
        Get
            CheckDeleted()
            If Row(DropdownDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DropdownDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DropdownDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(DropdownDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DropdownDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property ActiveFlag As String
        Get
            CheckDeleted()
            If Row(DropdownDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DropdownDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DropdownDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function DGPageLocator(oDataView As DataView, sSearchValue As String, nPageSize As Integer, sColumnName As String) As Integer

        Dim i As Int16
        Dim nGridPage As Int16
        Dim nSearchLen As Integer = sSearchValue.Length
        Dim myResult As Double
        Dim sDataColumnValue As String

        For i = 0 To CType(oDataView.Count - 1, Int16)
            sDataColumnValue = oDataView.Table.Rows(i)(sColumnName).ToString

            If sDataColumnValue.Length >= nSearchLen Then
                If sSearchValue.ToUpper = sDataColumnValue.Substring(0, nSearchLen).ToUpper Then
                    Exit For
                End If
            End If
        Next

        myResult = i Mod nPageSize

        nGridPage = CType(Math.Floor(i / nPageSize), Int16)

        If oDataView.Count = i Then
            nGridPage = -1
        End If

        Return nGridPage


    End Function

    Public Function AddDropdown(code As String, maintainable_by_user As String, englishTranslation As String, userId As String) As Integer
        Dim dal As New DropdownDAL
        Return dal.AddDropdown(code, maintainable_by_user, englishTranslation, userId)
    End Function

    Public Function UpdateDropdown(listId As Guid, code As String, maintainable_by_user As String, englishTranslation As String, userId As String) As Integer
        Dim dal As New DropdownDAL
        Return dal.UpdateDropdown(listId, code, maintainable_by_user, englishTranslation, userId)
    End Function

    Public Function UpdateTranslation(DictItemTranslationId As Guid, Translation As String, userId As String) As Integer
        Dim dal As New DropdownDAL
        Return dal.UpdateTranslation(DictItemTranslationId, Translation, userId)
    End Function

    Public Function DeleteDropdown(listId As Guid) As Integer
        Dim dal As New DropdownDAL
        Return dal.DeleteDropdown(listId)
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function AdminLoadList(LanguageId As Guid) As System.Data.DataView
        Try
            Dim dal As New DropdownDAL
            Dim ds As New DataSet
            dal.AdminLoadList(ds, LanguageId)
            Return New System.Data.DataView(ds.Tables(DropdownDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

     Public Shared Function DeviceLoadList(LanguageId As Guid) As System.Data.DataView
        Try
            Dim dal As New DropdownDAL
            Dim ds As New DataSet
            dal.DeviceLoadList(ds, LanguageId)
            Return New System.Data.DataView(ds.Tables(DropdownDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function AdminLoadListTranslation(ListId As Guid) As System.Data.DataView
        Try
            Dim dal As New DropdownDAL
            Dim ds As New DataSet
            dal.AdminLoadListTranslation(ds, ListId)
            Return New System.Data.DataView(ds.Tables(DropdownDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetTranslation(LanguageId As Guid, DictItemId As Guid) As String
        Dim dal As New DropdownDAL
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim strTranslation As String = ""
        dal.GetTranslation(ds, LanguageId, DictItemId)
        dv = New DataView(ds.Tables(0))
        If dv.Count > 0 Then
            strTranslation = dv(0)(0).ToString
        End If
        Return strTranslation
    End Function

#End Region



End Class
