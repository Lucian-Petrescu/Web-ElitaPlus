'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/11/2008)********************


Public Class ClaimStatusDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_STATUS"
    Public Const TABLE_NAME_WS As String = "CLAIM_EXTENDED_STATUS"
    Public Const TABLE_KEY_NAME As String = "claim_status_id"
    Public Const COL_NAME_CLAIM_STATUS As String = "Extended_Status_Code"

    Public Const COL_NAME_CLAIM_STATUS_ID As String = "claim_status_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_STATUS_COMMENTS As String = "status_comments"
    Public Const COL_NAME_STATUS_DATE_1 As String = "status_date"
    Public Const COL_NAME_PARAM_CLAIM_ID As String = "p_claim_id"
    Public Const COL_NAME_PARAM_LANGUAGE_ID As String = "p_language_id"
    Public Const COL_NAME_PARAM_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const COL_NAME_PARAM_CLAIM_STATUS_BY_COMPANY_GROUP_ID As String = "p_claim_status_by_groupID"
    Public Const COL_NAME_PARAM_EXTERNAL_USER_NAME As String = "p_external_user_name"
    Public Const COL_NAME_PARAM_COMMENTS As String = "p_comments"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const COL_NAME_STATUS_ORDER As String = "status_order"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_STATUS_DESCRIPTION As String = "status_description"
    Public Const COL_NAME_OWNER As String = "owner"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_STATUS_DATE As String = "col_name_status_date"
    Public Const COL_NAME_EXTERNAL_USER_NAME As String = "external_user_name"
    Public Const COL_NAME_GROUP_NUMBER As String = "group_number"

    Public Const PARAM_RETURN As String = "p_return_code"
    Public Const PARAM_EXCEPTION_MSG As String = "p_return_msg"

    Public Const P_RETURN As Integer = 0
    Public Const P_EXCEPTION_MSG As Integer = 1

    Private Const DSNAME As String = "LIST"
    Public Const WAITING_ON_BUDGET_APPROVAL As String = "COD"
    Public Const WAITING_DOCUMENTATION As String = "WDOC"
    Public Const BUDGET_APPROVED As String = "BAPP"
    Public Const NEW_EXTENDED_CLAIM_STATUS As String = "NEW"
    Public Const INVOICE_PAID_EXTENDED_CLAIM_STATUS As String = "IP"
    Public Const WORK_ORDER_OPENED As String = "WOPE"
    '5623
    Public Const COL_NAME_USER_ID As String = "user_id"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet, languageId As Guid)
        Load(ds, Guid.Empty, languageId)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid, languageId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                            {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                                             New DBHelper.DBHelperParameter(COL_NAME_CLAIM_STATUS_ID, id.ToByteArray), _
                                                             New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(claimId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLatestClaimStatus(claimId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/MAX_CLAIM_STATUS")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimStatus(claimId As Guid, languageId As Guid, companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_STATUS_HISTORY")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_PARAM_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_PARAM_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_PARAM_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}

        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    '5623
    Public Function GetClaimStatusByUserRole(claimId As Guid, languageId As Guid, companyGroupId As Guid, userId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_STATUS_BY_USER_ROLE")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                             New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray), _
                                              New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}

        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetExtendedStatusForCompanyGroup(languageId As Guid, companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/EXTENDED_CLAIM_STATUS_FOR_COMPANY_GROUP")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_PARAM_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(COL_NAME_PARAM_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}

        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimStatusHistoryOnly(claimId As Guid, languageId As Guid, companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_STATUS_HISTORY_ONLY")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_PARAM_LANGUAGE_ID, languageId.ToByteArray),
                                             New OracleParameter(COL_NAME_PARAM_CLAIM_ID, claimId.ToByteArray)}

        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimHistoryDetails(claimId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_HISTORY_DETAILS")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                           {New OracleParameter(COL_NAME_PARAM_CLAIM_ID, claimId.ToByteArray)}
        Try

            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function GetExtendedMVStatus(claimId As Guid)
        Dim selectStmt As String = Config("/SQL/GET_EXTENDED_CLAIM_MV")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}
        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, "ELP_CLAIM_EXTENDED_MV", parameters)
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Sub AddClaimToNewPickList(claim_id As Guid, claim_status_by_groupID As Guid, external_user_name As String, comments As String) 

        Dim selectStmt As String = Config("/SQL/ADD_CLAIM_TO_NEWPICKLIST")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARAM_CLAIM_ID, claim_id.ToByteArray), _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARAM_CLAIM_STATUS_BY_COMPANY_GROUP_ID, claim_status_by_groupID.ToByteArray), _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARAM_EXTERNAL_USER_NAME, external_user_name), _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARAM_COMMENTS, comments)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(PARAM_RETURN, GetType(Integer)), _                                                                 
                                                                 New DBHelper.DBHelperParameter(PARAM_EXCEPTION_MSG, GetType(String), 200)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            If outputParameters(P_RETURN).Value <> 0 Then _
                Throw New StoredProcedureGeneratedException("Data base exception occurred", outputParameters(P_EXCEPTION_MSG).Value)


        Catch ex As DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            '   ds.Dispose()
        End Try




    End Sub
    Public Sub UpdateExtendedMV(claimId As System.Guid, claimStatusId As System.Guid, statusCode As String)
        Dim updateStmt As String = Config("/SQL/UPDATE_EXTENDED_CLAIM_MV")
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PARAM_CLAIM_ID, claimId.ToByteArray), _
                                                                                       New DBHelper.DBHelperParameter(COL_NAME_CLAIM_STATUS_ID, claimStatusId.ToByteArray), _
                                                                                       New DBHelper.DBHelperParameter(COL_NAME_STATUS_CODE, statusCode)}
        Try
            DBHelper.Execute(updateStmt, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region


End Class


