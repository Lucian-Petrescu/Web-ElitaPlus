'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/15/2004)********************


Public Class DisbursementDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DISBURSEMENT"
    Public Const TABLE_KEY_NAME As String = "disbursement_id"

    Public Const COL_NAME_DISBURSEMENT_ID As String = "disbursement_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_SVC_CONTROL_NUMBER As String = "svc_control_number"
    Public Const COL_NAME_RECORD_COUNT As String = "record_count"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_SERVICE_CENTER_NAME As String = "service_center_name"
    Public Const COL_NAME_CHECK_NUMBER As String = "check_number"
    Public Const COL_NAME_TRACKING_NUMBER As String = "tracking_number"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_PAYEE As String = "payee"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_DESC As String = "region_desc"
    Public Const COL_NAME_ZIP As String = "zip"
    Public Const COL_NAME_PAYEE_MAILING_LABEL As String = "payee_mailing_label"
    Public Const COL_NAME_TAX_AMOUNT As String = "tax_amount"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_PROCESSED As String = "processed"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    'Public Const COL_NAME_PAYEE_ADDRESS_ID As String = "payee_address_id"
    'Public Const COL_NAME_PAYEE_OPTION As String = "payee_option"
    Public Const COL_NAME_PAYEE_OPTION_ID As String = "payee_option_id"
    Public Const COL_NAME_DEDUCTIBLE_AMOUNT As String = "deductible_amount"
    Public Const COL_NAME_DEDUCTIBLE_TAX_AMOUNT As String = "deductible_tax_amount"

    Public Const COL_NAME_ACCOUNT_NAME As String = "account_name"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_PAYMENT_METHOD As String = "payment_method"
    Public Const COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_COUNTRY As String = "country"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_PAYMENT_REASON As String = "payment_reason"
    Public Const COL_NAME_IBAN_NUMBER As String = "iban_number"
    Public Const COL_NAME_SWIFT_CODE As String = "swift_code"
    Public Const COL_NAME_ACCOUNT_TYPE As String = "account_type"
    Public Const COL_NAME_ACCOUNT_TYPE_ID As String = "account_type_id"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_ACCT_STATUS As String = "acct_status"
    Public Const COL_NAME_VENDOR_NUM_ACCT As String = "vendor_num_acct"
    Public Const COL_NAME_VENDOR_REGION_DESC As String = "vendor_region_desc"
    Public Const COL_NAME_PERCEPTION_IVA As String = "perception_iva"
    Public Const COL_NAME_PERCEPTION_IIBB As String = "perception_iibb"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_BRANCH_NAME As String = "branch_name"
    Public Const COL_NAME_BANK_NAME As String = "bank_name"
    Public Const COL_NAME_BANK_SORT_CODE As String = "bank_sort_code"
    Public Const COL_NAME_BANK_LOOKUP_CODE As String = "bank_lookup_code"
    Public Const COL_NAME_BANK_SUB_CODE As String = "bank_sub_code"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"

    Public Const COL_NAME_BONUS As String = "bonus"
    Public Const COL_NAME_BONUS_TAX As String = "bonus_tax"
    Public Const COL_NAME_LABOR_AMOUNT As String = "labor_amount"
    Public Const COL_NAME_PART_AMOUNT As String = "part_amount"
    Public Const COL_NAME_SERVICE_AMOUNT As String = "service_amount"
    Public Const COL_NAME_TRIP_AMOUNT As String = "trip_amount"
    Public Const COL_NAME_SHIPPING_AMOUNT As String = "shipping_amount"
    Public Const COL_NAME_DISPOSITION_AMOUNT As String = "disposition_amount"
    Public Const COL_NAME_OTHER_AMOUNT As String = "other_amount"
    Public Const COL_NAME_DIAGNOSTICS_AMOUNT As String = "diagnostics_amount"
    Public Const COL_NAME_WITHHOLDING_AMOUNT As String = "withholding_amount"
    Public Const COL_NAME_PAY_TO_CUST_AMOUNT As String = "pay_to_cust_amount"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("disbursement_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadPayeeList(svcControlNumber As String, companyList As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PAYEE_LIST")
        Dim whereClauseCondition As String = ""

        whereClauseCondition = " AND " & Environment.NewLine & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, companyList)

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, whereClauseCondition)

        'svcControlNumber = GetFormattedSearchStringForSQL(svcControlNumber)
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("svc_control_number", svcControlNumber.ToUpper)}
        'New DBHelper.DBHelperParameter("company_id", companylist)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDisbursement(id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("disbursement_id", id.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Public methods"
    Public Function LoadRemainingInvoicePayment(svcControlNumber As String, OrigDisbID As Guid, ClaimID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_OTHER_UNREVERSED_PAYMENT")

        'selectStmt = selectStmt.Replace(":svc_control_number", svcControlNumber).Replace(":disbursement_id", MiscUtil.GetDbStringFromGuid(OrigDisbID, True)).Replace(":claim_id", MiscUtil.GetDbStringFromGuid(ClaimID, True))
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                    New DBHelper.DBHelperParameter("svc_control_number", svcControlNumber),
                                    New DBHelper.DBHelperParameter("disbursement_id", OrigDisbID.ToByteArray),
                                    New DBHelper.DBHelperParameter("claim_id", ClaimID.ToByteArray),
                                    New DBHelper.DBHelperParameter("svc_control_number", svcControlNumber),
                                    New DBHelper.DBHelperParameter("disbursement_id", OrigDisbID.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadDisbursementFromClaim(ClaimID As Guid) As DataTable
        Dim selectStmt As String = Config("/SQL/GET_DISBURSEMENT_FROM_CLAIM")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                    New DBHelper.DBHelperParameter("claim_id", ClaimID.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds.Tables(0))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


