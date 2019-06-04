Public Class DropdownItem
    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/12/2007)  ********************
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
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
            Dim dal As New DropdownItemDAL
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
            Dim dal As New DropdownItemDAL
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
            If Row(DropdownItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DropdownItemDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownItemDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property MaintainableByUser() As String
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_MAINTAINABLE_BY_USER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownItemDAL.COL_NAME_MAINTAINABLE_BY_USER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_MAINTAINABLE_BY_USER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property DisplayToUser() As String
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_DISPLAY_TO_USER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownItemDAL.COL_NAME_DISPLAY_TO_USER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_DISPLAY_TO_USER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ListId() As Guid
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DropdownItemDAL.COL_NAME_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DictItemId() As Guid
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DropdownItemDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property ActiveFlag() As String
        Get
            CheckDeleted()
            If Row(DropdownItemDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DropdownItemDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DropdownItemDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DropdownItemDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function AddDropdownItem(ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal list_id As Guid, ByVal englishTranslation As String, ByVal userId As String) As Integer
        Dim dal As New DropdownItemDAL
        Return dal.AddDropdownItem(code, maintainable_by_user, display_to_user, list_id, englishTranslation, userId)
    End Function

    Public Function UpdateDropdownItem(ByVal listItemId As Guid, ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal englishTranslation As String, ByVal userId As String) As Integer
        Dim dal As New DropdownItemDAL
        Return dal.UpdateDropdownItem(listItemId, code, maintainable_by_user, display_to_user, englishTranslation, userId)
    End Function

    Public Function DeleteDropdownItem(ByVal listItemId As Guid) As Integer
        Dim dal As New DropdownItemDAL
        Return dal.DeleteDropdownItem(listItemId)
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function AdminLoadListItems(ByVal LanguageId As Guid, ByVal ListId As Guid) As System.Data.DataView
        Try
            Dim dal As New DropdownItemDAL
            Dim ds As New DataSet
            dal.AdminLoadListItems(ds, LanguageId, ListId)
            Return New System.Data.DataView(ds.Tables(DropdownItemDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

     Public Shared Function DeviceLoadListItems(ByVal LanguageId As Guid, ByVal Code As String) As System.Data.DataView
        Try
            Dim dal As New DropdownItemDAL
            Dim ds As New DataSet
            dal.DeviceLoadListItems(ds, LanguageId, Code)
            Return New System.Data.DataView(ds.Tables(DropdownItemDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function AdminLoadListItemTranslation(ByVal ListItemId As Guid) As System.Data.DataView
        Try
            Dim dal As New DropdownItemDAL
            Dim ds As New DataSet
            dal.AdminLoadListItemTranslation(ds, ListItemId)
            Return New System.Data.DataView(ds.Tables(DropdownItemDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


End Class
