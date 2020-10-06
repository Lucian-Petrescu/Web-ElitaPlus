'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/13/2011)********************


Public Class DealerVscReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"

    Public Const TABLE_NAME As String = "ELP_DEALER_VSC_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "dealer_vsc_recon_wrk_id"

    Public Const COL_NAME_DEALER_VSC_RECON_WRK_ID As String = "dealer_vsc_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_ENROLLMENT_ID As String = "enrollment_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CERTIFICATE_LOADED As String = "certificate_loaded"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_CERTIFICATE_NUMBER As String = "certificate_number"
    Public Const COL_NAME_CUSTOMERS As String = "customers"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_COUNTRY_CODE As String = "country_code"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_MODEL_YEAR As String = "model_year"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_ENGINE_VERSION As String = "engine_version"
    Public Const COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const COL_NAME_ODOMETER As String = "odometer"
    Public Const COL_NAME_VIN As String = "vin"
    Public Const COL_NAME_PURCHASE_PRICE As String = "purchase_price"
    Public Const COL_NAME_PURCHASE_DATE As String = "purchase_date"
    Public Const COL_NAME_IN_SERVICE_DATE As String = "in_service_date"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_PLAN_CODE As String = "plan_code"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_TERM_MONTHS As String = "term_months"
    Public Const COL_NAME_TERM_KM_MI As String = "term_km_mi"
    Public Const COL_NAME_AGENT_NUMBER As String = "agent_number"
    Public Const COL_NAME_WARRANTY_SALE_DATE As String = "warranty_sale_date"
    Public Const COL_NAME_PLAN_AMOUNT As String = "plan_amount"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_IDENTITY_DOC_NO As String = "identity_doc_no"
    Public Const COL_NAME_RG_NO As String = "rg_no"
    Public Const COL_NAME_ID_TYPE As String = "id_type"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_QUOTE_NUMBER As String = "quote_number"
    Public Const COL_NAME_QUOTE_ITEM_NUMBER As String = "quote_item_number"
    Public Const COL_NAME_NEW_USED As String = "new_used"
    Public Const COL_NAME_OPTIONAL_COVERAGE As String = "optional_coverage"
    Public Const COL_NAME_BIRTH_DATE As String = "birth_date"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_PAYMENT_TYPE_CODE As String = "payment_type_code"
    Public Const COL_NAME_PAYMENT_INSTRUMENT_CODE As String = "payment_instrument_code"
    Public Const COL_NAME_INSTALLMENTS_NUMBER As String = "installments_number"
    Public Const COL_NAME_CREDIT_CARD_TYPE_CODE As String = "credit_card_type_code"
    Public Const COL_NAME_NAME_ON_CREDIT_CARD As String = "name_on_credit_card"
    Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_NAME_ON_ACCOUNT As String = "name_on_account"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_EXTERNAL_CAR_CODE As String = "external_car_code"
    Public Const COL_NAME_PLAN_AMOUNT_WITH_MARKUP As String = "plan_amount_with_markup"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_CANCELLATION_DATE As String = "cancellation_date"
    Public Const COL_NAME_CANCELLATION_REASON_CODE As String = "cancellation_reason_code"
    Public Const COL_NAME_CANCEL_COMMENT_TYPE_CODE As String = "cancel_comment_type_code"
    Public Const COL_NAME_CANCELLATION_COMMENT As String = "cancellation_comment"
    Public Const COL_NAME_FINANCING_AGENCY As String = "financing_agency"
    Public Const COL_NAME_NC_PAYMENT_METHOD_CODE As String = "nc_payment_method_code"
    Public Const COL_NAME_ACCOUNT_TYPE_CODE As String = "account_type_code"
    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_BRANCH_DIGIT As String = "branch_digit"
    Public Const COL_NAME_ACCOUNT_DIGIT As String = "account_digit"
    Public Const COL_NAME_REFUND_AMOUNT As String = "refund_amount"


    'for reject message translation
    Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_REJECT_REASON As String = "reject_reason"
    Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_vsc_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerfileProcessedID As Guid, languageID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                     New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadRejectList(dealerfileProcessedID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_REJECT_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
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


