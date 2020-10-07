'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/12/2008)********************


Public Class ClaimHistoryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_HISTORY"
    Public Const TABLE_KEY_NAME As String = "claim_history_id"

    Public Const COL_NAME_CLAIM_HISTORY_ID As String = "claim_history_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_STATUS_CODE_OLD As String = "status_code_old"
    Public Const COL_NAME_STATUS_CODE_NEW As String = "status_code_new"
    Public Const COL_NAME_AUTHORIZED_AMOUNT_OLD As String = "authorized_amount_old"
    Public Const COL_NAME_AUTHORIZED_AMOUNT_NEW As String = "authorized_amount_new"
    Public Const COL_NAME_CLAIM_CLOSED_DATE_OLD As String = "claim_closed_date_old"
    Public Const COL_NAME_CLAIM_CLOSED_DATE_NEW As String = "claim_closed_date_new"
    Public Const COL_NAME_REPAIR_DATE_OLD As String = "repair_date_old"
    Public Const COL_NAME_REPAIR_DATE_NEW As String = "repair_date_new"
    Public Const COL_NAME_CLAIM_MODIFIED_DATE_NEW As String = "claim_modified_date_new"
    Public Const COL_NAME_CLAIM_MODIFIED_BY_NEW As String = "claim_modified_by_new"
    Public Const COL_NAME_CLAIM_MODIFIED_DATE_OLD As String = "claim_modified_date_old"
    Public Const COL_NAME_CLAIM_MODIFIED_BY_OLD As String = "claim_modified_by_old"
    Public Const COL_NAME_LIABILITY_LIMIT_OLD As String = "liability_limit_old"
    Public Const COL_NAME_LIABILITY_LIMIT_NEW As String = "liability_limit_new"
    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID_OLD As String = "cert_item_coverage_id_old"
    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID_NEW As String = "cert_item_coverage_id_new"
    Public Const COL_NAME_DEDUCTIBLE_NEW As String = "deductible_new"
    Public Const COL_NAME_DEDUCTIBLE_OLD As String = "deductible_old"
    Public Const COL_NAME_SERVICE_CENTER_NEW As String = "service_center_new"
    Public Const COL_NAME_SERVICE_CENTER_OLD As String = "service_center_old"
    Public Const COL_NAME_BATCH_NUMBER_NEW As String = "batch_number_new"
    Public Const COL_NAME_BATCH_NUMBER_OLD As String = "batch_number_old"
    Public Const COL_NAME_IS_LAWSUIT_ID_OLD As String = "is_lawsuit_id_old"
    Public Const COL_NAME_IS_LAWSUIT_ID_NEW As String = "is_lawsuit_id_new"

    Public Const COL_NAME_PARAM_LANGUAGE_ID As String = "p_language_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_PARAM_CLAIM_ID As String = "p_claim_id"
    Private Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_history_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDS As DataSet, claimId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetClaimHistoryDetails(claimId As Guid, languageId As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/CLAIM_HISTORY_DETAILS")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                           {New OracleParameter(COL_NAME_PARAM_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_PARAM_CLAIM_ID, claimId.ToByteArray)}



        Try

            Return New DataView((DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)).Tables(0))
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
#End Region


End Class



