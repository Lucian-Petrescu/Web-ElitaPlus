'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/2/2010)********************


Public Class ClaimloadReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_CLAIMLOAD_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "claimload_recon_wrk_id"

    Public Const COL_NAME_CLAIMLOAD_RECON_WRK_ID As String = "claimload_recon_wrk_id"
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CLAIM_LOADED As String = "claim_loaded"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_CLAIM_TYPE As String = "claim_type"
    Public Const COL_NAME_AUTHORIZATION_NUM As String = "authorization_num"
    Public Const COL_NAME_EXTERNAL_CREATED_DATE As String = "external_created_date"
    Public Const COL_NAME_COVERAGE_CODE As String = "coverage_code"
    Public Const COL_NAME_DATE_OF_LOSS As String = "date_of_loss"
    Public Const COL_NAME_CAUSE_OF_LOSS As String = "cause_of_loss"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_SPECIAL_INSTRUCTIONS As String = "special_instructions"
    Public Const COL_NAME_REASON_CLOSED As String = "reason_closed"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ESTIMATE_AMOUNT As String = "estimate_amount"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_DEFECT_REASON As String = "defect_reason"
    Public Const COL_NAME_REPAIR_CODE As String = "repair_code"
    Public Const COL_NAME_CALLER_NAME As String = "caller_name"
    Public Const COL_NAME_CONTACT_NAME As String = "contact_name"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_VISIT_DATE As String = "visit_date"
    Public Const COL_NAME_PICKUP_DATE As String = "pickup_date"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CERT_ITEM_COV_ID As String = "cert_item_cov_id"
    Public Const COL_NAME_ORIGINAL_CLAIM_ID As String = "original_claim_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_POLICE_REPORT_NUM As String = "police_report_num"
    Public Const COL_NAME_OFFICER_NAME As String = "officer_name"
    Public Const COL_NAME_POLICE_STATION_CODE As String = "police_station_code"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_RG_NUMBER As String = "rg_number"
    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_ID_TYPE As String = "id_type"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = "device_reception_date"
    Public Const COL_NAME_REPLACEMENT_TYPE As String = "replacement_type"
    Public Const COL_NAME_REPLACEMENT_MANUFACTURER As String = "replacement_manufacturer"
    Public Const COL_NAME_REPLACEMENT_MODEL As String = "replacement_model"
    Public Const COL_NAME_REPLACEMENT_SERIAL_NUMBER As String = "replacement_serial_number"
    Public Const COL_NAME_REPLACEMENT_SKU As String = "replacement_sku"
    Public Const COL_NAME_DEDUCTIBLE_COLLECTED As String = "deductible_collected"
    Public Const COL_NAME_LABOR_AMOUNT As String = "labor_amount"
    Public Const COL_NAME_PARTS_AMOUNT As String = "parts_amount"
    Public Const COL_NAME_SERVICE_AMOUNT As String = "service_amount"
    Public Const COL_NAME_SHIPPING_AMOUNT As String = "shipping_amount"
    Public Const COL_NAME_PART_1_SKU As String = "part_1_sku"
    Public Const COL_NAME_PART_1_DESCRIPTION As String = "part_1_description"
    Public Const COL_NAME_PART_2_SKU As String = "part_2_sku"
    Public Const COL_NAME_PART_2_DESCRIPTION As String = "part_2_description"
    Public Const COL_NAME_PART_3_SKU As String = "part_3_sku"
    Public Const COL_NAME_PART_3_DESCRIPTION As String = "part_3_description"
    Public Const COL_NAME_PART_4_SKU As String = "part_4_sku"
    Public Const COL_NAME_PART_4_DESCRIPTION As String = "part_4_description"
    Public Const COL_NAME_PART_5_SKU As String = "part_5_sku"
    Public Const COL_NAME_PART_5_DESCRIPTION As String = "part_5_description"
    Public Const COL_NAME_SERVICE_LEVEL As String = "service_level"
    Public Const COL_NAME_DEALER_REFERENCE As String = "dealer_reference"
    Public Const COL_NAME_POS As String = "pos"
    Public Const COL_NAME_PROBLEM_FOUND As String = "problem_found"
    Public Const COL_NAME_FINAL_STATUS As String = "final_status"
    Public Const COL_NAME_TECHNICAL_REPORT As String = "technical_report"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_INCOMING_ENTIRE_RECORD As String = "incoming_entire_record"
    Public Const COL_NAME_COMMENT_TYPE_ID As String = "comment_type_id"
    Public Const COL_NAME_COMMENT_TYPE As String = "comment_type"
    Public Const COL_NAME_TRACKING_NUMBER As String = "tracking_number"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_RISK_TYPE_ENGLISH As String = "risk_type_english"
    Public Const COL_NAME_ITEM_DESCRIPTION As String = "item_description"
    Public Const COL_NAME_TRIP_AMOUNT As String = "trip_amount"
    Public Const COL_NAME_OTHER_AMOUNT As String = "other_amount"
    Public Const COL_NAME_OTHER_EXPLANATION As String = "other_explanation"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_EXTENDED_STATUS_CODE As String = "extended_status_code"
    Public Const COL_NAME_EXTENDED_STATUS_DATE As String = "extended_status_date"
    Public Const COL_NAME_TRACKING_NUM_TO_CUST As String = "tracking_num_to_cust"
    Public Const COL_NAME_TRACKING_NUM_TO_SC As String = "tracking_num_to_sc"
    Public Const COL_NAME_DEVICE_SHIPPED_TO_SC_DATE As String = "device_shipped_to_sc_date"
    Public Const COL_NAME_Deductile_Included As String = "deductible_included"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claimload_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(strFileName As String, languageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("filename", strFileName)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadByClaimLoadFileProcessedId(ds As DataSet, claimLoadFileProcessedId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CLAIM_LOAD_FILE_PROCESSED_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ClaimloadFileProcessedDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, claimLoadFileProcessedId.ToByteArray)}
        Try
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
#End Region


End Class


