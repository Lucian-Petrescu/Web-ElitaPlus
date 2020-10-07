'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/9/2009)  ********************

Public Class FormCategory
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
            Dim dal As New FormCategoryDAL
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
            Dim dal As New FormCategoryDAL
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
        If DictItemId = Guid.Empty Then DictItemId = Guid.NewGuid
    End Sub

    Private _Description As String
    Private _IsDescriptionChanged As Boolean = False
#End Region

#Region "Constant"
    Public Const CODE_MUST_UNIQUE As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED"
#End Region
#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(FormCategoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(FormCategoryDAL.COL_NAME_FORM_CATEGORY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255), ValidUniqueCode("")> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(FormCategoryDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FormCategoryDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FormCategoryDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    Public Property ParentCategoryId As Guid
        Get
            CheckDeleted()
            If row(FormCategoryDAL.COL_NAME_PARENT_CATEGORY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(FormCategoryDAL.COL_NAME_PARENT_CATEGORY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FormCategoryDAL.COL_NAME_PARENT_CATEGORY_ID, Value)
        End Set
    End Property

    Public Property TabId As Guid
        Get
            CheckDeleted()
            If Row(FormCategoryDAL.COL_NAME_TAB_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(FormCategoryDAL.COL_NAME_TAB_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FormCategoryDAL.COL_NAME_TAB_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DictItemId As Guid
        Get
            CheckDeleted()
            If row(FormCategoryDAL.COL_NAME_DICT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(FormCategoryDAL.COL_NAME_DICT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FormCategoryDAL.COL_NAME_DICT_ITEM_ID, Value)
        End Set
    End Property
    <ValueMandatoryWhenAddNew("")> _
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set
            If _Description <> String.Empty AndAlso _Description <> value Then
                _IsDescriptionChanged = True
            End If
            _Description = value
        End Set
    End Property

    Public ReadOnly Property IsDescriptionChanged As Boolean
        Get
            Return _IsDescriptionChanged
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            If IsDirty Then
                MyBase.Save()
            ElseIf IsDescriptionChanged Then
                Dim dal As New FormCategoryDAL
                dal.UpdateDescriptionOnly(IsDescriptionChanged, DictItemId, Description)
                dal = Nothing
            End If
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New FormCategoryDAL
                If IsNew Then
                    dal.AddNew(Row, DictItemId, Description)
                Else
                    dal.UpdateExisting(Row, IsDescriptionChanged, DictItemId, Description)
                End If
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

    Public Sub SaveDelete(ByVal guidDictItemID As Guid)
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New FormCategoryDAL
                If IsDeleted Then
                    dal.Delete(Row, guidDictItemID)
                End If
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

    Public Shared Sub AssignFormCategory(ByVal FormID As Guid, ByVal FormCategoryID As Guid)
        Dim dal As New FormCategoryDAL
        dal.UpdateForm_FormCategory(FormID, FormCategoryID)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Class FormCategorySearchDV
        Inherits DataView

        Public Const COL_FORM_CATEGORY_ID As String = "Form_Category_id"
        Public Const COL_CODE As String = "CODE"
        Public Const COL_DESCRIPTION As String = "Description"
        Public Const COL_TAB_ID As String = "TAB_ID"
        Public Const COL_TAB_DESC As String = "Tab_Name"
        Public Const COL_FORM_COUNT As String = "Form_Count"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As FormCategorySearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(FormCategorySearchDV.COL_FORM_CATEGORY_ID) = (New Guid()).ToByteArray
            row(FormCategorySearchDV.COL_CODE) = ""
            row(FormCategorySearchDV.COL_DESCRIPTION) = ""
            row(FormCategorySearchDV.COL_TAB_ID) = Guid.Empty.ToByteArray
            row(FormCategorySearchDV.COL_TAB_DESC) = ""
            row(FormCategorySearchDV.COL_FORM_COUNT) = 0
            dt.Rows.Add(row)
            Return New FormCategorySearchDV(dt)
        End Function
    End Class

    Public Class FormSearchDV
        Inherits DataView
        Public Const COL_FORM_ID As String = "Form_id"
        Public Const COL_FORM_NAME As String = "Form_Name"
        Public Const COL_TAB_NAME As String = "Tab_Name"
        Public Const COL_FORM_CATEGORY_ID As String = "Form_Category_id"
        Public Const COL_FORM_CATEGORY_NAME As String = "Form_Category_Name"
        
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As FormSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(FormSearchDV.COL_FORM_ID) = (New Guid()).ToByteArray
            row(FormSearchDV.COL_FORM_NAME) = ""
            row(FormSearchDV.COL_TAB_NAME) = ""
            row(FormSearchDV.COL_FORM_CATEGORY_ID) = Guid.Empty.ToByteArray
            row(FormSearchDV.COL_FORM_CATEGORY_NAME) = ""
            dt.Rows.Add(row)
            Return New FormSearchDV(dt)
        End Function
    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As FormCategorySearchDV, ByVal NewFormCategoryBO As FormCategory)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewFormCategoryBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(FormCategorySearchDV.COL_FORM_CATEGORY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(FormCategorySearchDV.COL_CODE, GetType(String))
                dt.Columns.Add(FormCategorySearchDV.COL_DESCRIPTION, GetType(String))
                dt.Columns.Add(FormCategorySearchDV.COL_TAB_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(FormCategorySearchDV.COL_TAB_DESC, GetType(String))
                dt.Columns.Add(FormCategorySearchDV.COL_FORM_COUNT, GetType(Integer))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(FormCategorySearchDV.COL_FORM_CATEGORY_ID) = NewFormCategoryBO.Id.ToByteArray
            row(FormCategorySearchDV.COL_CODE) = String.Empty
            row(FormCategorySearchDV.COL_DESCRIPTION) = String.Empty
            row(FormCategorySearchDV.COL_TAB_ID) = NewFormCategoryBO.TabId.ToByteArray
            row(FormCategorySearchDV.COL_TAB_DESC) = String.Empty
            row(FormCategorySearchDV.COL_FORM_COUNT) = 0
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New FormCategorySearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal guidTab As Guid, ByVal strCode As String, ByVal strDescription As String) As FormCategorySearchDV
        Try
            Dim dal As New FormCategoryDAL
            Return New FormCategorySearchDV(dal.LoadList(Authentication.CurrentUser.LanguageId, guidTab, strCode, strDescription).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getTabList() As DataView
        Try
            Dim dal As New FormCategoryDAL
            Return dal.LoadTabList(Authentication.CurrentUser.LanguageId).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getFormCategoryList() As DataView
        Try
            Dim dal As New FormCategoryDAL
            Return dal.LoadAllCategories(Authentication.CurrentUser.LanguageId).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getFormList(ByVal guidTab As Guid, ByVal guidCategory As Guid, ByVal strFormDesc As String) As FormSearchDV
        Try
            Dim dal As New FormCategoryDAL
            Return New FormSearchDV(dal.LoadFormList(Authentication.CurrentUser.LanguageId, guidTab, guidCategory, strFormDesc).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function IsCodeInUse(ByVal strCode As String, Optional ByVal IsNew As Boolean = True) As Boolean
        Dim blnInUse As Boolean = True
        Try
            Dim dal As New FormCategoryDAL
            Dim ds As DataSet = dal.LoadList(Authentication.CurrentUser.LanguageId, Guid.Empty, strCode, String.Empty)
            If IsNew Then
                If ds.Tables(0).Rows.Count = 0 Then blnInUse = False
            Else
                If ds.Tables(0).Rows.Count <= 1 Then blnInUse = False
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
        Return blnInUse
    End Function
#End Region

#Region "Dictionary related functions"
    
#End Region
#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidUniqueCode
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CODE_MUST_UNIQUE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As FormCategory = CType(objectToValidate, FormCategory)
            If Not obj.IsDeleted Then 'when not deleting, New or save existing
                If IsCodeInUse(obj.Code, obj.IsNew) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryWhenAddNew
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As FormCategory = CType(objectToValidate, FormCategory)
            If obj.IsNew Then
                If obj.Description.Trim = String.Empty Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region
End Class