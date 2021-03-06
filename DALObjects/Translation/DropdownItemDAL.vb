Public Class DropdownItemDAL
    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/12/2007)********************
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_LIST_ITEM"
    Public Const TABLE_KEY_NAME As String = "list_item_id"

    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_MAINTAINABLE_BY_USER As String = "maintainable_by_user"
    Public Const COL_NAME_DISPLAY_TO_USER As String = "display_to_user"
    Public Const COL_NAME_LIST_ID As String = "list_id"
    Public Const COL_NAME_DICT_ITEM_ID As String = "dict_item_id"
    Public Const COL_NAME_ACTIVE_FLAG As String = "active_flag"
    Public Const COL_NAME_LANG_ID As String = "language_id"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("list_item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function AdminLoadListItems(ByVal ds As DataSet, ByVal LanguageId As Guid, ByVal ListId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_ITEMS_ADMIN")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_LANG_ID, LanguageId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_LIST_ID, ListId.ToByteArray)}
        Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
    End Function

    Public Function DeviceLoadListItems(ByVal ds As DataSet, ByVal LanguageId As Guid, ByVal Code As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_ITEMS_DEVICE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_LANG_ID, LanguageId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_CODE, Code)}
        Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
    End Function

    Public Function AdminLoadListItemTranslation(ByVal ds As DataSet, ByVal ListItemId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_ITEM_TRANSLATION")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_LIST_ITEM_ID, ListItemId.ToByteArray)}
        Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
    End Function

#End Region


#Region "Public Members"

    Public Function AddDropdownItem(ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal list_id As Guid, ByVal englishTranslation As String, ByVal userId As String) As Integer
        Dim selectStmt As String = Me.Config("/SQL/ADD_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_code", code), _
                            New DBHelper.DBHelperParameter("p_maintainable_by_user", maintainable_by_user), _
                            New DBHelper.DBHelperParameter("p_display_to_user", display_to_user), _
                            New DBHelper.DBHelperParameter("p_list_id", list_id), _
                            New DBHelper.DBHelperParameter("p_english_translation", englishTranslation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function UpdateDropdownItem(ByVal listItemId As Guid, ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal englishTranslation As String, ByVal userId As String) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_list_item_id", listItemId.ToByteArray), _
                            New DBHelper.DBHelperParameter("p_code", code), _
                            New DBHelper.DBHelperParameter("p_maintainable_by_user", maintainable_by_user), _
                            New DBHelper.DBHelperParameter("p_display_to_user", display_to_user), _
                            New DBHelper.DBHelperParameter("p_english_translation", englishTranslation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function DeleteDropdownItem(ByVal listItemId As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/DELETE_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_list_item_id", listItemId.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


