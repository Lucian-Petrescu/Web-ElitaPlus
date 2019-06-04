'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/4/2008)  ********************

Public Class DictItemTranslation
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
            Dim dal As New DictItemTranslationDAL
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
            Dim dal As New DictItemTranslationDAL
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
            If row(DictItemTranslationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DictItemTranslationDAL.COL_NAME_DICT_ITEM_TRANSLATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Translation() As String
        Get
            CheckDeleted()
            If row(DictItemTranslationDAL.COL_NAME_TRANSLATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DictItemTranslationDAL.COL_NAME_TRANSLATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DictItemTranslationDAL.COL_NAME_TRANSLATION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LanguageId() As Guid
        Get
            CheckDeleted()
            If row(DictItemTranslationDAL.COL_NAME_LANGUAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DictItemTranslationDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DictItemTranslationDAL.COL_NAME_LANGUAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DictItemId() As Guid
        Get
            CheckDeleted()
            If row(DictItemTranslationDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DictItemTranslationDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DictItemTranslationDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DictItemTranslationDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually

                'ALR- Update Translations in Cache is needed
                TranslationBase.UpdateTranslationInCache(Me.Dataset, Me.LanguageId.ToString)

                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                    'Me._address = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            'Me._address = Nothing
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function GetDataset() As DataSet
        'If ds Is Nothing Then
        '    ds = New DataSet
        'End If
        'Dim dal As New DictItemTranslationDAL
        'dal.Load(ds, Id)
        Return Me.Dataset
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetTranslationList(ByVal Language_Id As Guid, ByVal SearchMask As String, Optional ByVal OrderByTrans As Boolean = False) As TranslationSearchDV
        Try
            Dim dal As New DictItemTranslationDAL
            Dim ds As DataView
            Dim EngLangId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetLanguageLookupList(), Codes.ENGLISH_LANG_CODE)
            'Dim CompanyLangId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCompanyLookupList(Language_Id), Codes.ENGLISH_LANG_CODE)


            Return New TranslationSearchDV(dal.LoadList(EngLangId, Language_Id, SearchMask, OrderByTrans).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetTranslationsList(ByVal dictItemId As Guid) As TranslationSearchDV
        Try
            Dim dal As New DictItemTranslationDAL
            'Dim ds As DataSet

            Return New TranslationSearchDV(dal.LoadList(dictItemId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Private Shared Function GetDictionaryInfoList(ByVal parent As DictionaryItem) As DataTable

    '    Try
    '        If Not parent.IsChildrenCollectionLoaded(GetType(DictionaryInfoList)) Then
    '            Dim dal As New DictItemTranslationDAL
    '            dal.LoadList(parent.Id, parent.Dataset)
    '            parent.AddChildrenCollection(GetType(DictionaryInfoList))
    '        End If

    '        Return parent.Dataset.Tables(DictItemTranslationDAL.TABLE_NAME)
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function


    Public Class TranslationSearchDV
        Inherits DataView

#Region "Constants"

        Public Const GRID_COL_TRANSLATION As String = "translation"
        Public Const GRID_COL_ID As String = "dict_item_translation_id"
        Public Const GRID_COL_ENGLISH As String = "english"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    'Public Class DictionaryInfoList
    '    Inherits BusinessObjectListBase
    '    Public Sub New(ByVal parent As DictionaryItem)
    '        MyBase.New(GetDictionaryInfoList(parent), GetType(DictItemTranslation), parent)
    '    End Sub

    '    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
    '        Return True
    '    End Function

    '    Public Function FindById(ByVal Id As Guid) As DictItemTranslation
    '        Dim bo As DictItemTranslation
    '        For Each bo In Me
    '            If bo.Id.Equals(Id) Then Return bo
    '        Next
    '        Return Nothing
    '    End Function


    'End Class

#End Region

End Class


