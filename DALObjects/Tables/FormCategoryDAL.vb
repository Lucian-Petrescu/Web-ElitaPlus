'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/9/2009)********************


Public Class FormCategoryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_FORM_CATEGORY"
    Public Const TABLE_KEY_NAME As String = "form_category_id"

    Public Const COL_NAME_FORM_CATEGORY_ID As String = "form_category_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_PARENT_CATEGORY_ID As String = "parent_category_id"
    Public Const COL_NAME_TAB_ID As String = "tab_id"
    Public Const COL_NAME_DICT_ITEM_ID As String = "dict_item_id"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("form_category_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(Language_id As Guid, guidTab As Guid, _
                             strCode As String, strDescription As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", Language_id.ToByteArray)}
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet = New DataSet

        If guidTab <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND t.Tab_id = HEXTORAW('" & GuidToSQLString(guidTab) & "')"
        End If

        If FormatSearchMask(strCode) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(fc.code) " & strCode.ToUpper
        End If

        If FormatSearchMask(strDescription) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(Tr.TRANSLATION) " & strDescription.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadTabList(Language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_TAB_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", Language_id.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAllCategories(Language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CATEGORY_ALL")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", Language_id.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadFormList(Language_id As Guid, guidTab As Guid, guidFormCateory As Guid, _
                             strFormDescription As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_FORM_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", Language_id.ToByteArray)}
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet = New DataSet

        If guidTab <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND t.Tab_id = HEXTORAW('" & GuidToSQLString(guidTab) & "')"
        End If

        If guidFormCateory <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND fc.form_category_id = HEXTORAW('" & GuidToSQLString(guidFormCateory) & "')"
        End If

        If FormatSearchMask(strFormDescription) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(ft.TRANSLATION) " & strFormDescription.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Dictionary related"
    Public Sub Delete(row As DataRow, DictItemId As Guid)
        Dim sqlStmtDictItem As String, sqlStmtTranslation As String
        Dim trans As IDbTransaction
        sqlStmtDictItem = Config("/SQL/DELETE_DICT_ITEM_TRANSLATION/DELETE_DIC_ITEM")
        sqlStmtTranslation = Config("/SQL/DELETE_DICT_ITEM_TRANSLATION/DELETE_TRANSLATION")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("dict_item_id", DictItemId.ToByteArray)}

        Try
            trans = DBHelper.GetNewTransaction
            '1. delete the dictionary item translation first
            DBHelper.Execute(sqlStmtTranslation, parameters, trans)
            '2. delete the form category row
            DBHelper.Execute(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, trans)
            '3. delete the dictionary item 
            DBHelper.Execute(sqlStmtDictItem, parameters, trans)
            
            trans.Commit()
            trans = Nothing
        Catch ex As Exception
            trans.Rollback()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub UpdateDescriptionOnly(IsDescChanged As Boolean, DictItemId As Guid, strTranslation As String)
        Dim sqlStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        If IsDescChanged Then
            sqlStmt = Config("/SQL/UPDATE_FROM_CATEGORY_DESCRIPTION")
            parameters = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("translation", strTranslation), _
                            New DBHelper.DBHelperParameter("DictItemId", DictItemId.ToByteArray)}
        End If
        Try
            If IsDescChanged Then
                DBHelper.ExecuteWithParam(sqlStmt, parameters)
            End If            
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub UpdateExisting(row As DataRow, IsDescChanged As Boolean, DictItemId As Guid, strTranslation As String)
        Dim sqlStmt As String, trans As IDbTransaction
        Dim parameters() As DBHelper.DBHelperParameter
        If IsDescChanged Then
            sqlStmt = Config("/SQL/UPDATE_FROM_CATEGORY_DESCRIPTION")
            parameters = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("translation", strTranslation), _
                            New DBHelper.DBHelperParameter("DictItemId", DictItemId.ToByteArray)}
        End If

        Try
            trans = DBHelper.GetNewTransaction
            If IsDescChanged Then
                DBHelper.ExecuteWithParam(sqlStmt, parameters)
            End If
            Update(row, trans)
            trans.Commit()
            trans = Nothing
        Catch ex As Exception
            trans.Rollback()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub AddNew(row As DataRow, DictItemId As Guid, strTranslation As String)
        If DictItemId = Guid.Empty Then Return
        Dim sqlStmt As String
        Dim trans As IDbTransaction
        sqlStmt = Config("/SQL/LOAD_DICT_ITEM_TRANSLATION_PROC")
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("P_DICT_ITEM_id", DictItemId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("P_UI_PROG_CODE", String.Empty)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("P_DESCRIPTION", strTranslation)
        inParameters.Add(param)

        Try
            trans = DBHelper.GetNewTransaction

            'Insert the dictionary item first
            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, Nothing, trans)
            'Then insert the form category row
            DBHelper.Execute(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, trans)

            trans.Commit()
            trans = Nothing
        Catch ex As Exception
            trans.Rollback()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

#End Region
#Region "Form updates"
    Public Sub UpdateForm_FormCategory(FormID As Guid, FormCategoryID As Guid)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter 
        If FormCategoryID = Guid.Empty Then
            selectStmt = Config("/SQL/UPDATE_FORM_FORM_CATEGORY/RemoveFromFormCategory")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("form_id", FormID.ToByteArray)}
        Else
            selectStmt = Config("/SQL/UPDATE_FORM_FORM_CATEGORY/AssignToFormCategory")
            parameters = New DBHelper.DBHelperParameter() { _
                                New DBHelper.DBHelperParameter("form_category_id", FormCategoryID.ToByteArray), _
                                New DBHelper.DBHelperParameter("form_id", FormID.ToByteArray)}
        End If
        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
End Class


