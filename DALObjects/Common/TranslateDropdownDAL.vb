Public Class TranslateDropdownDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_LIST As String = "ELP_LIST"
    Public Const TABLE_LIST_ITEM As String = "ELP_LIST_ITEM"
    Public Const COL_NAME_LIST_ID = "list_id"
    Public Const COL_NAME_LIST_ITEM_ID = "list_item_id"
#End Region

#Region "Crude Methods"

    Public Function GetDropdownId(listCode As String) As Guid
        Dim selectStmt As String = Config("/SQL/DROPDOWN_ID")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim listCodeParam As DBHelper.DBHelperParameter
        Dim id As Byte()
        Try
            listCodeParam = New DBHelper.DBHelperParameter(COL_NAME_CODE, listCode)
            id = DBHelper.Fetch(ds, selectStmt, TABLE_LIST, New DBHelper.DBHelperParameter() {listCodeParam}).Tables(TABLE_LIST).Rows(0)(COL_NAME_LIST_ID)
            If Not id Is Nothing Then
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDropdownItemId(DropdownId As Guid, itemCode As String) As Guid
        Dim selectStmt As String = Config("/SQL/DROPDOWN_ITEM_ID")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim itemCodeParam As DBHelper.DBHelperParameter
        Dim DropdownIdParam As DBHelper.DBHelperParameter
        Dim tempDS As DataSet
        Dim id As Byte()
        Try
            itemCodeParam = New DBHelper.DBHelperParameter(COL_NAME_CODE, itemCode)
            DropdownIdParam = New DBHelper.DBHelperParameter(COL_NAME_LIST_ID, DropdownId.ToByteArray)
            DBHelper.Fetch(ds, selectStmt, TABLE_LIST_ITEM, New DBHelper.DBHelperParameter() {itemCodeParam, DropdownIdParam})
            If ds.Tables(TABLE_LIST_ITEM).Rows.Count > 0 Then
                id = ds.Tables(TABLE_LIST_ITEM).Rows(0)(COL_NAME_LIST_ITEM_ID)
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class
