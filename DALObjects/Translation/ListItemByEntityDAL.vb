'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/18/2018)******************

Public Class ListItemByEntityDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_LIST_ITEM_BY_ENTITY"
	Public Const TABLE_KEY_NAME As String = "list_item_by_entity_id"
	
	Public Const COL_NAME_LIST_ITEM_BY_ENTITY_ID As String = "list_item_by_entity_id"
	Public Const COL_NAME_ENTITY_REFERENCE As String = "entity_reference"
	Public Const COL_NAME_ENTITY_REFERENCE_ID As String = "entity_reference_id"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const TABLE_LIST_ITEM As String = "list_item"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("list_item_by_entity_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
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


    Public Function LoadSelectedListItem(oLanguageCode As String, entityRefId As Guid, listCode As String) As DataSet

        Dim selectStmt As String = Config("/SQL/SelectedListItem")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        ' Dim tbl As String = "LoadEntityList"
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_language_code", oLanguageCode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_entity", entityRefId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_listcode", listCode)
        inParameters.Add(param)

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, TABLE_LIST_ITEM, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

    Public Function LoadAvailableListItem(oLanguageCode As String, entityRefId As Guid, listCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/AvailableListItem")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        ' Dim tbl As String = "LoadEntityList"
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_language_code", oLanguageCode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_entity", entityRefId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_listcode", listCode)
        inParameters.Add(param)

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, TABLE_LIST_ITEM, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub UpdateListItem(oEntityRefId As Guid, listCode As String, oDataset As DataSet)
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction
        Dim deleteStmt As String = Config("/SQL/DELETE_ENTITY_ITEM")
        Dim inputParameters(1) As DBHelper.DBHelperParameter

        Try
            ' Delete User Companies
            inputParameters(0) = New DBHelper.DBHelperParameter(COL_NAME_ENTITY_REFERENCE_ID, oEntityRefId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("code", listCode)
            DBHelper.Execute(deleteStmt, inputParameters, tr)

            ' Insert User Companies
            Update(oDataset, tr, DataRowState.Added Or DataRowState.Modified)

            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.Commit(tr)
        Catch ex As Exception
            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.RollBack(tr)

            Throw ex
        End Try
    End Sub

    Public Function LoadEntityList(oLanguageCode As String, oEntityRefId As Guid, listCode As String, entityType As String, shortExpression As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LoadEntityList")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        ' Dim tbl As String = "LoadEntityList"
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_language_code", oLanguageCode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_entity", oEntityRefId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_listcode", listCode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_entitytype", entityType)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_sort_expression", shortExpression)
        inParameters.Add(param)
        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, TABLE_NAME, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub DeleteDropdown(listCode As String, oEntityRefId As Guid)

        Dim tr As IDbTransaction = DBHelper.GetNewTransaction
        Dim deleteStmt As String = Config("/SQL/DELETE_ENTITY_ITEM")
        Dim inputParameters(1) As DBHelper.DBHelperParameter
        Try
            ' Delete User Companies
            inputParameters(0) = New DBHelper.DBHelperParameter(COL_NAME_ENTITY_REFERENCE_ID, oEntityRefId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("code", listCode)
            DBHelper.Execute(deleteStmt, inputParameters, tr)


            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.Commit(tr)
        Catch ex As Exception
            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.RollBack(tr)

            Throw ex
        End Try

    End Sub
End Class


