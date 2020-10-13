'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2017)********************
Public Class OcTemplateGroupDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_TEMPLATE_GROUP"
    Public Const TABLE_KEY_NAME As String = "oc_template_group_id"

    Public Const COL_NAME_OC_TEMPLATE_GROUP_ID As String = "oc_template_group_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_GROUP_ACCOUNT_USER_NAME As String = "group_account_user_name"
    Public Const COL_NAME_GROUP_ACCOUNT_PASSWORD As String = "group_account_password"

    Public Const COL_NAME_NUMBER_OF_TEMPLATES As String = "number_of_templates"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_group_id", id.ToByteArray)}
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

    Public Function LoadList(code As String) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("code", code)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAssociatedTemplateCount(templateGroupId As Guid)
        Try
            Dim selectStmt As String = Config("/SQL/GET_ASSOCIATED_TEMPLATE_COUNT")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_group_id", templateGroupId.ToByteArray)}
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

    Public Sub UpdateFamily(dataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction

        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        Dim templateGroupDealerDAL As New OcTemplateGroupDealerDAL

        Try
            'First Pass updates Deletions           
            MyBase.Update(dataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)
            templateGroupDealerDAL.Update(dataset, tr, DataRowState.Deleted)

            'Second Pass updates additions and changes 
            Update(dataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            templateGroupDealerDAL.Update(dataset, tr, DataRowState.Added Or DataRowState.Modified)

            If dataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) IsNot Nothing AndAlso dataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(dataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                dataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
#End Region
End Class


