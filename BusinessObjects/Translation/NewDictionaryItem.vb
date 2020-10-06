'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/5/2008)  ********************

Public Class NewDictionaryItem
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
            Dim dal As New NewDictionaryItemDAL
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
            Dim dal As New NewDictionaryItemDAL
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
            If row(NewDictionaryItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(NewDictionaryItemDAL.COL_NAME_NEW_DICT_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property UiProgCode() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_UI_PROG_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_UI_PROG_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_UI_PROG_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1600)> _
    Public Property EnglishTranslation() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_ENGLISH_TRANSLATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_ENGLISH_TRANSLATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_ENGLISH_TRANSLATION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property Approved() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_APPROVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_APPROVED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_APPROVED, Value)
        End Set
    End Property

    Public Property DictItemId() As Guid
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(NewDictionaryItemDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property Imported() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_IMPORTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_IMPORTED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_IMPORTED, Value)
        End Set
    End Property



    Public ReadOnly Property ModifiedDate() As DateType
        Get
            If Row(NewDictionaryItemDAL.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then Return Nothing
            Return New DateType(CType(Row(NewDictionaryItemDAL.COL_NAME_MODIFIED_DATE), Date))
        End Get
    End Property

    Public ReadOnly Property ModifiedById() As String
        Get
            If Row(NewDictionaryItemDAL.COL_NAME_MODIFIED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(NewDictionaryItemDAL.COL_NAME_MODIFIED_BY), String)
        End Get
    End Property


    Public ReadOnly Property CreatedDate() As DateType
        Get
            If Row(NewDictionaryItemDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then Return Nothing
            Return New DateType(CType(Row(NewDictionaryItemDAL.COL_NAME_CREATED_DATE), Date))
        End Get
    End Property

    '<ValueMandatory("")> _
    Public ReadOnly Property CreatedById() As String
        Get
            If Row(NewDictionaryItemDAL.COL_NAME_CREATED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(NewDictionaryItemDAL.COL_NAME_CREATED_BY), String)
        End Get
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property MsgCode() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_MSG_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_MSG_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_MSG_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property MsgType() As String
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_MSG_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(NewDictionaryItemDAL.COL_NAME_MSG_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_MSG_TYPE, Value)
        End Set
    End Property



    Public Property MsgParameterCount() As LongType
        Get
            CheckDeleted()
            If row(NewDictionaryItemDAL.COL_NAME_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(NewDictionaryItemDAL.COL_NAME_MSG_PARAMETER_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(NewDictionaryItemDAL.COL_NAME_MSG_PARAMETER_COUNT, Value)
        End Set
    End Property
#End Region

#Region "Generate new Dictionary Items"
    Public Sub GenerateNewLabels(ByRef LabelID As Guid)

        Dim searchDV As DataView
        Dim oDictItem As New DictionaryItem(Dataset)
        oDictItem.Save()
        Dim oDictItemTrans As DictItemTranslation
        Dim oLabel As Label_Extended
        oLabel = oDictItem.AssociatedLabel(Guid.Empty, , True)
        oLabel.UiProgCode = UiProgCode
        oLabel.InUse = "Y"
        oLabel.DictItemId = oDictItem.Id
        oLabel.Save()
        searchDV = oDictItem.GetLanguageList()
        DictItemId = oDictItem.Id
        Imported = "Y"

        For i As Integer = 0 To searchDV.Count - 1
            oDictItemTrans = oDictItem.AssociatedTranslation(Guid.Empty, True)
            oDictItemTrans.Translation = EnglishTranslation
            oDictItemTrans.LanguageId = New Guid(CType(searchDV.Table.Rows(i).Item(3), Byte()))
            oDictItemTrans.DictItemId = oDictItem.Id
            oDictItemTrans.Save()
        Next

        LabelID = oLabel.Id
    End Sub

    Public Sub ChangeLabels(ByVal Id As Guid)
        Dim oDictItemTrans As DictItemTranslation
        Dim oDictItem As DictionaryItem
        Dim oLabel As Label_Extended = New Label_Extended(Id, Dataset, True)
        oLabel.UiProgCode = UiProgCode
        oLabel.Save()
        Dim searchDV As DataView = oDictItemTrans.GetTranslationsList(oLabel.DictItemId)
        For i As Integer = 0 To searchDV.Count - 1
            Dim DictItemTrans As DictItemTranslation = New DictItemTranslation(New Guid(CType(searchDV.Table.Rows(i).Item(0), Byte())), Dataset)
            DictItemTrans.Translation = EnglishTranslation
            DictItemTrans.Save()
        Next
    End Sub

    Public Sub RemoveLabels(ByVal Id As Guid)
        'Dim oDictItemTrans As DictItemTranslation
        If Imported = "Y" Then
            Dim oDictItem As DictionaryItem = New DictionaryItem(Id, Dataset)
            Dim oLabel As Label_Extended = New Label_Extended(Id, Dataset, True)
            Dim searchDV As DataView = DictItemTranslation.GetTranslationsList(Id)
            oLabel.Delete()
            oLabel.Save()

            For i As Integer = 0 To searchDV.Count - 1
                Dim oDictItemTrans As DictItemTranslation = New DictItemTranslation(New Guid(CType(searchDV.Table.Rows(i).Item(0), Byte())), Dataset)
                oDictItemTrans.Delete()
                oDictItemTrans.Save()
            Next

            oDictItem.Delete()
            oDictItem.Save()
        End If

    End Sub
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New NewDictionaryItemDAL
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

    Public Sub DeleteAll()
        Dim dal As New NewDictionaryItemDAL
        Try
            dal.DeleteAll()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList() As NewDictItemSearchDV
        Try
            Dim dal As New NewDictionaryItemDAL
            'Dim oCompany As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            Return New NewDictItemSearchDV(dal.LoadList.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadList(ByVal scNetworkId As Guid) As DataSet
        Try
            Dim dal As New RouteDAL
            Return dal.LoadList(scNetworkId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal bo As NewDictionaryItem) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(NewDictionaryItemDAL.COL_NAME_UI_PROG_CODE) = bo.UiProgCode 'String.Empty
        row(NewDictionaryItemDAL.COL_NAME_ENGLISH_TRANSLATION) = bo.EnglishTranslation 'String.Empty
        row(NewDictionaryItemDAL.COL_NAME_NEW_DICT_ITEM_ID) = bo.Id.ToByteArray
        row(NewDictionaryItemDAL.COL_NAME_DICT_ITEM_ID) = bo.DictItemId.ToByteArray
        row(NewDictionaryItemDAL.COL_NAME_IMPORTED) = bo.Imported
        row(NewDictionaryItemDAL.COL_NAME_APPROVED) = bo.Approved
        row(NewDictionaryItemDAL.COL_NAME_CREATED_DATE) = DBNull.Value
        row(NewDictionaryItemDAL.COL_NAME_MODIFIED_DATE) = DBNull.Value
        row(NewDictionaryItemDAL.COL_NAME_CREATED_BY) = bo.CreatedById
        row(NewDictionaryItemDAL.COL_NAME_MODIFIED_BY) = bo.ModifiedById
        row(NewDictionaryItemDAL.COL_NAME_MSG_CODE) = DBNull.Value
        row(NewDictionaryItemDAL.COL_NAME_MSG_TYPE) = DBNull.Value
        row(NewDictionaryItemDAL.COL_NAME_MSG_PARAMETER_COUNT) = 0
        dt.Rows.Add(row)
        Return (dv)

    End Function

#End Region

#Region "NewDictItemSearchDV"

    Public Class NewDictItemSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_UI_PROG_CODE As String = NewDictionaryItemDAL.COL_NAME_UI_PROG_CODE
        Public Const COL_NAME_ENGLISH_TRANSLATION As String = NewDictionaryItemDAL.COL_NAME_ENGLISH_TRANSLATION
        Public Const COL_NAME_APPROVED As String = NewDictionaryItemDAL.COL_NAME_APPROVED
        Public Const COL_NAME_IMPORTED As String = NewDictionaryItemDAL.COL_NAME_IMPORTED
        Public Const COL_NAME_DICT_ITEM_ID = NewDictionaryItemDAL.COL_NAME_DICT_ITEM_ID
        Public Const COL_NAME_NEW_DICT_ITEM_ID = NewDictionaryItemDAL.COL_NAME_NEW_DICT_ITEM_ID
        Public Const COL_NAME_CREATED_DATE = NewDictionaryItemDAL.COL_NAME_CREATED_DATE
        Public Const COL_NAME_MODIFIED_DATE = NewDictionaryItemDAL.COL_NAME_MODIFIED_DATE
        Public Const COL_NAME_CREATED_BY = NewDictionaryItemDAL.COL_NAME_CREATED_BY
        Public Const COL_NAME_MODIFIED_BY = NewDictionaryItemDAL.COL_NAME_MODIFIED_BY
        Public Const COL_NAME_MSG_TYPE = NewDictionaryItemDAL.COL_NAME_MSG_TYPE
        Public Const COL_NAME_MSG_CODE = NewDictionaryItemDAL.COL_NAME_MSG_CODE
        Public Const COL_NAME_MSG_PARAM_COUNT = NewDictionaryItemDAL.COL_NAME_MSG_PARAMETER_COUNT

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property Id(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_NEW_DICT_ITEM_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property UiprogCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_UI_PROG_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property EngTranslation(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_ENGLISH_TRANSLATION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Approved(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_APPROVED).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Impoted(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_IMPORTED).ToString
            End Get
        End Property

        Public Shared ReadOnly Property DictItemId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_DICT_ITEM_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CreatedDate(ByVal row As DataRow) As DateType
            Get
                Return row(COL_NAME_CREATED_DATE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ModifiedDate(ByVal row As DataRow) As DateType
            Get
                Return row(COL_NAME_MODIFIED_DATE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CreatedBy(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CREATED_BY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ModifiedBy(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MODIFIED_BY).ToString
            End Get
        End Property

        Public Function Find(ByVal Id As Guid) As NewDictionaryItem
            Dim bo As NewDictionaryItem
            For Each bo In Me
                If bo.Id.Equals(Id) Then Return bo
            Next
            Return Nothing
        End Function

    End Class
#End Region

End Class


