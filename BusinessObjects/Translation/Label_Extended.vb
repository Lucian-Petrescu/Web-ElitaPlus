'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/4/2008)  ********************

Public Class Label_Extended
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal useFamilyId As Boolean = False)
        MyBase.New(False)
        Dataset = familyDS
        Load(id, useFamilyId)
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
            Dim dal As New LabelDAL
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

    Protected Sub Load(ByVal id As Guid, Optional ByVal useFamilyId As Boolean = False)
        Try
            Dim dal As New LabelDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                If useFamilyId Then
                    Row = FindRow(id, dal.COL_NAME_DICT_ITEM_ID, Dataset.Tables(dal.TABLE_NAME))
                Else
                    Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                End If
                'Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id, useFamilyId)
                If useFamilyId Then
                    Row = FindRow(id, dal.COL_NAME_DICT_ITEM_ID, Dataset.Tables(dal.TABLE_NAME))
                Else
                    Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                End If

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
            If Row(LabelDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(LabelDAL.COL_NAME_LABEL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property UiProgCode() As String
        Get
            CheckDeleted()
            If row(LabelDAL.COL_NAME_UI_PROG_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LabelDAL.COL_NAME_UI_PROG_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LabelDAL.COL_NAME_UI_PROG_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property InUse() As String
        Get
            CheckDeleted()
            If Row(LabelDAL.COL_NAME_IN_USE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(LabelDAL.COL_NAME_IN_USE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LabelDAL.COL_NAME_IN_USE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DictItemId() As Guid
        Get
            CheckDeleted()
            If Row(LabelDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(LabelDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(LabelDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New LabelDAL
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

    Public Shared Function LoadList(ByVal SearchMask As String, Optional ByVal OrderByTrans As Boolean = False) As LabelSearchDV
        Try
            Dim dal As New LabelDAL
            Dim ds As DataSet
            Dim EngLangId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetLanguageLookupList(), Codes.ENGLISH_LANG_CODE)
            Return New LabelSearchDV(dal.LoadList(EngLangId, SearchMask, OrderByTrans).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class LabelSearchDV
        Inherits DataView

#Region "Constants"

        Public Const GRID_COL_ENGLISH As String = "english"
        Public Const GRID_COL_UI_PROG_CODE As String = "ui_prog_code"
        Public Const DICT_ITEM_ID As String = "dict_item_id"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function FindById(ByVal Id As Guid) As DictItemTranslation
            Dim bo As DictItemTranslation
            For Each bo In Me
                If bo.Id.Equals(Id) Then Return bo
            Next
            Return Nothing
        End Function

    End Class
#End Region

End Class


