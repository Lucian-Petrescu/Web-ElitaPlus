'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/1/2008)********************


Public Class CodeMappingDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CODE_MAPPING"
    Public Const TABLE_KEY_NAME As String = "code_mapping_id"

    Public Const COL_NAME_CODE_MAPPING_ID As String = "code_mapping_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const COL_NAME_NEW_DESCRIPTION As String = "new_description"
    Public Const COL_NAME_LANG_ID As String = "language_id"
    Public Const COL_NAME_LIST_ID As String = "list_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("code_mapping_id", id.ToByteArray)}
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

    Public Function AdminLoadListItems(LanguageId As Guid, ListId As Guid, companyId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_LANG_ID, LanguageId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_LIST_ID, ListId.ToByteArray)}
        Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
    End Function

    Public Function LoadCovertedCode(companyID As Guid, ListCode As String, ListItemCode As String) As String
        Dim strResult As String = String.Empty
        Dim selectStmt As String = Config("/SQL/GET_CONVERTED_CODE")
        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyID.ToByteArray), _
                 New DBHelper.DBHelperParameter("list_code", ListCode), _
                 New DBHelper.DBHelperParameter("list_item_code", ListItemCode)}

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables(0).Rows.Count > 0 Then
                strResult = ds.Tables(0).Rows(0).Item(0)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return strResult
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

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

#End Region


End Class



