'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/13/2004)********************

Public Class CertItemCoverageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_ITEM_COVERAGE"
    Public Const TABLE_KEY_NAME As String = "cert_item_coverage_id"
    Public Const TABLE_PREMIUM_TOTALS As String = "premium_totals"
    Public Const TABLE_CLAIMS As String = "claims"

    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID As String = "cert_item_coverage_id"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = "deductible_based_on_id"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_ORIGINAL_REGION_ID As String = "original_region_id"
    Public Const COL_NAME_BEGIN_DATE As String = "begin_date"
    Public Const COL_NAME_END_DATE As String = "end_date"
    Public Const COL_NAME_LIABILITY_LIMITS As String = "liability_limits"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT As String = "deductible_percent"
    Public Const COL_NAME_GROSS_AMT_RECEIVED As String = "gross_amt_received"
    Public Const COL_NAME_PREMIUM_WRITTEN As String = "premium_written"
    Public Const COL_NAME_ORIGINAL_PREMIUM As String = "original_premium"
    Public Const COL_NAME_LOSS_COST As String = "loss_cost"
    Public Const COL_NAME_COMMISSION As String = "commission"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_MARKETING_EXPENSE As String = "marketing_expense"
    Public Const COL_NAME_OTHER As String = "other"
    Public Const COL_NAME_SALES_TAX As String = "sales_tax"
    Public Const COL_NAME_TAX1 As String = "tax1"
    Public Const COL_NAME_TAX2 As String = "tax2"
    Public Const COL_NAME_TAX3 As String = "tax3"
    Public Const COL_NAME_TAX4 As String = "tax4"
    Public Const COL_NAME_TAX5 As String = "tax5"
    Public Const COL_NAME_TAX6 As String = "tax6"
    Public Const COL_NAME_MTD_PAYMENTS As String = "mtd_payments"
    Public Const COL_NAME_YTD_PAYMENTS As String = "ytd_payments"
    Public Const COL_NAME_ASSURANT_GWP As String = "assurant_gwp"
    Public Const COL_NAME_MARKUP_COMMISSION As String = "markup_commission"
    Public Const COL_NAME_DEALER_DISCOUNT_AMT As String = "dealer_discount_amt"
    Public Const COL_NAME_DEALER_DISCOUNT_PERCENT As String = "dealer_discount_percent"
    Public Const COL_NAME_IS_CLAIM_ALLOWED As String = "is_claim_allowed"
    Public Const COL_NAME_IS_DISCOUNT As String = "is_discount"
    Public Const COL_NAME_COVERAGE_TYPE_CODE As String = "coverage_type_code"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"

    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_MASTER_CLAIM_NUMBER As String = "master_claim_number"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

    Public Const COL_TOTAL_GROSS_AMT_RECEIVED As String = "total_gross_amt_received"
    Public Const COL_TOTAL_PREMIUM_WRITTEN As String = "Total_Premium_Written"
    Public Const COL_TOTAL_ORIGINAL_PREMIUM As String = "Total_Original_Premium"
    Public Const COL_TOTAL_LOSS_COST As String = "Total_Loss_cost"
    Public Const COL_TOTAL_COMISSION As String = "Total_Comission"
    Public Const COL_TOTAL_ADMIN_EXPENSE As String = "total_admin_expense"
    Public Const COL_TOTAL_MARKETING_EXPENSE As String = "total_marketing_expense"
    Public Const COL_TOTAL_OTHER As String = "total_other"
    Public Const COL_TOTAL_SALES_TAX As String = "total_sales_tax"
    Public Const COL_TOTAL_MTD_PAYMENTS As String = "total_mtd_payments"
    Public Const COL_TOTAL_YTD_PAYMENTS As String = "total_ytd_payments"
    Public Const COL_NAME_REPAIR_DISCOUNT_PCT As String = "repair_discount_pct"
    Public Const COL_NAME_REPLACEMENT_DISCOUNT_PCT As String = "replacement_discount_pct"

    Public Const COL_NAME_LOSS_DATE As String = "loss_date"
    Public Const COL_NAME_CLAIM_STATUS As String = "claim_status"
    Public Const COL_NAME_INVOICE_PROCESS_DATE As String = "invoice_process_date"

    Public Const COL_NAME_MARKUP_COMMISSION_VAT As String = "markup_commission_vat"

    Public Const COL_NAME_COVERAGE_LIABILITY_LIMIT As String = "Coverage_liability_limit"
    Public Const COL_NAME_COVERAGE_REMAIN_LIABILITY_LIMIT As String = "Cov_Remain_Liability_Limit"

    Public Const COL_NAME_COVERAGE_DURATION As String = "Coverage_Duration"
    Public Const COL_NAME_NO_OF_RENEWALS As String = "No_of_Renewals"
    Public Const COL_NAME_RENEWAL_DATE As String = "Renewal_Date"
    Public Const COL_NAME_REINSURANCE_STATUS_ID As String = "reinsurance_status_id"
    Public Const COL_NAME_REINSURANCE_REJECT_REASON As String = "reinsurance_reject_reason"
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = "Deductible_Expression_Id"
    Public Const COL_NAME_FULFILLMENT_PROFILE_CODE As String = "Fulfillment_Profile_Code"
    Public Const PAR_NAME_CLAIM_WAITING_PERIOD As String = "p_claim_waiting_period"
    Public Const PAR_NAME_IGNORE_WAITING_PERIOD As String = "p_ignore_waiting_period_id"
    Public Const PAR_NAME_CERT_ITEM_COVERAGE_ID As String = "p_certItemCoverageId"
    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_item_coverage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    'Public Function LoadPremiumTotals(ByVal certId As Guid) As DataSet
    '    Dim ds As DataSet
    '    Dim parameters() As OracleParameter
    '    Dim selectStmt As String = Me.Config("/SQL/GET_TOTALS")
    '    parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_ID, certId.ToByteArray)}
    '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_PREMIUM_TOTALS, parameters)
    'End Function

    'Public Sub LoadList(ByVal ds As DataSet, ByVal certificateId As Guid)
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Dim certParam As New DBHelper.DBHelperParameter("cert_item_id", certificateId.ToByteArray)
    '    DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {certParam})
    'End Sub

    Public Function LoadList(ByVal certificateId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadCovListWithProdSplitWarr(ByVal certificateId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COV_PRD_SPLIT_WARR")
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function
    Public Function LoadEligibleCoverages(ByVal certificateId As Guid, ByVal languageId As Guid, ByVal dateOfLoss As Date) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ELIGIBLE_COVERAGES")
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LOSS_DATE, dateOfLoss)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadListMainItem(ByVal certificateId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_MAIN_ITEM")
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadClaimCoverageType(ByVal certificateId As Guid, ByVal languageId As Guid, _
        ByVal certItemCoverageId As Guid, ByVal lossDate As Date, ByVal claimStatus As String, _
        ByVal invoiceProcessDate As Date) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_COVERAGE_TYPE")
        Dim invProcessDate As String = "Not Null"
        If invoiceProcessDate = Nothing Then invProcessDate = "Null"

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LOSS_DATE, lossDate), _
                                     New OracleParameter(COL_NAME_CLAIM_STATUS, claimStatus), _
                                     New OracleParameter(COL_NAME_INVOICE_PROCESS_DATE, invProcessDate)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function


    Public Function LoadItemCoverages(ByVal certId As Guid) As DataSet
        Dim ds As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_TOTALS")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_ID, certId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_PREMIUM_TOTALS, parameters)
    End Function

    Public Function LoadClaims(ByVal certItemCoverageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/CLAIMS")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS, parameters)
    End Function

    Public Function LoadAllClaims(ByVal certItemCoverageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/ALL_CLAIMS")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS, parameters)
    End Function

    Public Sub LoadAllItemCoveragesForCertificate(ByVal certId As Guid, ByVal familyDataset As DataSet)
        Dim ds As DataSet = familyDataset
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_CERTIFICATE")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_ID, certId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Sub

    Public Function LoadAllItemCoveragesForGalaxyCertificate(ByVal certId As Guid, ByVal compId As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_GALAXY_CERTIFICATE")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_ID, certId.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_COMPANY_ID, compId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds
    End Function

    Public Function LoadAllItemCoveragesForGalaxyClaim(ByVal certId As Guid, ByVal compId As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_GALAXY_CLAIM")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_ID, certId.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_COMPANY_ID, compId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds
    End Function

    Public Function LoadAllItemCoveragesForGalaxyClaimUpdate(ByVal masterClaimNumber As String, ByVal compId As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_GALAXY_CLAIM_UPDATE")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_MASTER_CLAIM_NUMBER, masterClaimNumber), _
                                            New OracleParameter(Me.COL_NAME_COMPANY_ID, compId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds
    End Function

    Public Function LoadCurrentProductCodeCoverage(ByVal certificateId As Guid, ByVal languageId As Guid) As DataSet
        'Dim ds As New DataSet
        'Dim parameters() As OracleParameter
        'Dim selectStmt As String = Me.Config("/SQL/LOAD_COVERAGE_CURRENT_PRODUCT_CODE")
        'parameters = New OracleParameter() _
        '                            {New OracleParameter(COL_NAME_CERT_ID, certificateId.ToByteArray), _
        '                             New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        'Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters

        Dim selectStmt As String = Me.Config("/SQL/LOAD_COVERAGE_CURRENT_PRODUCT_CODE")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("pi_cert_id", certificateId.ToByteArray),
                                New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("po_coverage_info", GetType(DataSet))}

        Dim ds As New DataSet

        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, Me.TABLE_NAME)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try




    End Function

    Public Function GetClaimWaitingPeriod(ByVal certItemCoverageId As Guid, ByRef ignoreWaitingPeriodID As Guid) As Integer

        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIM_WAITING_PERIOD")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_WAITING_PERIOD, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_IGNORE_WAITING_PERIOD, GetType(Byte()),16), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 100)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If CType(outputParameters(2).Value, Integer) <> 0 Then
            If CType(outputParameters(2).Value, Integer) = 300 Then
                Throw New StoredProcedureGeneratedException("Get Claim Waiting Period Generated Error: ", outputParameters(2).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameters(2).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        Else
            ignoreWaitingPeriodID = New Guid(Convert.ToString(outputParameters(1).Value))
            Return CType(outputParameters(0).Value, Integer)
        End If
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region


End Class

