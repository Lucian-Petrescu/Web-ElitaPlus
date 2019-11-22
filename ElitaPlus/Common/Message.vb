Public Class Message
#Region "Messages"
    'After Abdullah, Andres and Alejandro researched, concluded that these messages are not entered in the 
    'label table for translation. Only the ErrorCode is translated.
    '
    'This Message Takes two Parameters : 1-Records Actually Deleted, 2-Total Records Processed (Attempted)
    Public Const BATCH_DELETE_PROCESS As String = "MSG_BATCH_DELETE_PROCESS"
    Public Const RECORD_ADDED_OK As String = "MSG_RECORD_ADDED_OK"
    Public Const ERR_DELETING_DATA As String = "MSG_ERROR_DELETING_DATA"
    Public Const ERR_SAVING_DATA As String = "MSG_ERROR_SAVING_DATA"
    Public Const CANNOT_FIND_NEW_RECORD As String = "MSG_CANNOT_FIND_NEW_RECORD"
    Public Const DELETE_RECORD_CONFIRMATION As String = "MSG_RECORD_DELETED_OK"
    Public Const EXPIRE_RECORD_CONFIRMATION As String = "MSG_RECORD_EXPIRED_OK"
    Public Const SAVE_RECORD_CONFIRMATION As String = "MSG_RECORD_SAVED_OK"
    Public Const CANCEL_SUCCESS_WITH_REFUND As String = "MSG_CANCEL_SUCCESS_WITH_REFUND"
    Public Const CANCEL_CERT_IN_FUTURE As String = "MSG_CANCEL_CERT_IN_FUTURE"
    Public Const CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD As String = "MSG_CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD"
    Public Const DELETE_RECORD_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Public Const MSG_CONFIRM_REOPEN As String = "MSG_CONFIRM_REOPEN"
    Public Const SAVE_CHANGES_PROMPT As String = "MSG_THE_CURRENT_RECORD_HAS_BEEN_CHANGED"
    Public Const NO_CHANGES_TO_RECORD As String = "MSG_THERE_ARE_NO_CHANGES_TO_THE_RECORD"
    Public Const ACCT_SETTING_PROMPT As String = "MSG_ACCT_SETTING_PROMPT"
    Public Const MSG_COMPANY_REQUIRED As String = "MSG_ONE_MORE_COMPANIES_ARE_REQUIRED"
    Public Const MSG_INVALID_COLOR_CODE As String = "MSG_INVALID_COLOR_CODE"
    Public Const MSG_COMPANY_IS_REQUIRED As String = "MSG_COMPANY_IS_REQUIRED"
    Public Const MSG_LIST_ITEM_REQUIRED As String = "MSG_LIST_ITEM_IS_REQUIRED"
    Public Const MSG_COMPANY_GROUP_REQUIRED As String = "MSG_COMPANY_GROUP_ARE_REQUIRED"
    Public Const MSG_AT_LEAST_ONE_SELECTION_IS_REQUIRED As String = "AT_LEAST_ONE_SELECTION_IS_REQUIRED"
    Public Const MSG_PAGE_SAVE_PROMPT As String = "MSG_PAGE_SAVE_PROMPT"
    Public Const MSG_PAGE_ALERT_PROMPT As String = "MSG_PAGE_ALERT_PROMPT"
    Public Const MSG_PAGE_SAVED As String = "MSG_PAGE_SAVED"
    Public Const MSG_PAGE_NOT_SAVED As String = "MSG_PAGE_NOT_SAVED"
    Public Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Public Const MSG_PRODUCTCODE_ITEM_EXISTS_PROMPT As String = "MSG_PRODUCTCODE_ITEM_EXISTS_PROMPT"
    Public Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED"
    Public Const MSG_DUPLICATE_DEALER_NOT_ALLOWED As String = "DUPLICATE_DEALER_NOT_ALLOWED"
    Public Const MSG_PROMPT_FOR_LEAVING_WHEN_ERROR As String = "MSG_PROMPT_FOR_LEAVING_WHEN_ERROR"
    Public Const REMOVE_CANCELDUEDATE_PROMPT As String = "REMOVE_CANCELDUEDATE_PROMPT"
    Public Const MSG_CANNOTDELETE_SOFTQUESTION As String = "MSG_CANNOTDELETE_SOFTQUESTION"
    Public Const MSG_CANNOTMODIFY_SOFTQUESTION As String = "MSG_CANNOTMODIFY_SOFTQUESTION"
    Public Const MSG_SERVICEORDER_SUBJECT As String = "MSG_SERVICEORDER_SUBJECT"
    Public Const MSG_SERVICEORDER_SUBJECT_AUTH As String = "MSG_SERVICEORDER_SUBJECT_AUTH"
    Public Const MSG_SERVICEORDER_EMAIL_SENT As String = "MSG_SERVICEORDER_EMAIL_SENT"
    Public Const MSG_SERVICEORDER_SEND_EMAIL_CONFIRM As String = "MSG_SERVICEORDER_SEND_EMAIL_CONFIRM"
    Public Const MSG_SERVICEORDER_SEND_CUSTOMER_EMAIL_CONFIRM As String = "MSG_SERVICEORDER_SEND_CUSTOMER_EMAIL_CONFIRM"
    Public Const MSG_AND_A_COPY_TO As String = "MSG_AND_A_COPY_TO"
    Public Const MSG_SERVICE_ORDER_RECORD_NOT_FOUND As String = "MSG_SERVICE_ORDER_RECORD_NOT_FOUND"
    Public Const MSG_CERTIFICATE_CANCELLATION_CONFIRM As String = "MSG_CERTIFICATE_CANCELLATION_CONFIRM"
    Public Const MSG_CERTIFICATE__REVERSE_CANCELLATION_CONFIRM As String = "MSG_CERTIFICATE__REVERSE_CANCELLATION_CONFIRM"
    Public Const MSG_PROMPT_FOR_CLOSING_THE_CLAIM As String = "MSG_PROMPT_FOR_CLOSING_THE_CLAIM"
    Public Const MSG_PROMPT_FOR_HAVE_ITEM_REPLACED As String = "MSG_PROMPT_FOR_HAVE_ITEM_REPLACED"
    Public Const MSG_PROMPT_FOR_KEEPING_SAME_LOCATION As String = "MSG_PROMPT_FOR_KEEPING_SAME_LOCATION"
    Public Const MSG_PROMPT_FOR_PAY As String = "MSG_PROMPT_FOR_PAY"
    Public Const MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS As String = "MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS"
    Public Const MSG_PROMPT_FOR_PAYING_CLAIM_FROM_NEW_CLAIM_FORM As String = "MSG_PROMPT_FOR_PAYING_CLAIM_FROM_NEW_CLAIM_FORM"
    Public Const MSG_PROMPT_FOR_CREATING_CLAIM_WITH_P_WHEN_THEFT_AND_NO_POLICE_REPORT As String = "MSG_PROMPT_FOR_CREATING_CLAIM_WITH_P_WHEN_THEFT_AND_NO_POLICE_REPORT"
    Public Const MSG_INVALID_AUTHORIZED_AMOUNT As String = "MSG_INVALID_AUTHORIZED_AMOUNT"
    Public Const MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA As String = "MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA"
    Public Const MSG_MAX_LIMIT_EXCEEDED_GENERIC As String = "MSG_MAX_LIMIT_EXCEEDED_GENERIC"
    Public Const MSG_NO_RECORDS_FOUND As String = "MSG_NO_RECORDS_FOUND"
    Public Const MSG_PROMPT_FOR_SOFTQUESTION_COMMENT As String = "MSG_PROMPT_FOR_SOFTQUESTION_COMMENT"
    Public Const MSG_CLAIM_ADDED As String = "MSG_CLAIM_ADDED"
    Public Const MSG_CLAIM_UPDATED As String = "MSG_CLAIM_UPDATED"
    Public Const MSG_EMAIL_SENT As String = "Email Sent Successfully"
    Public Const MSG_EMAIL_NOT_SENT As String = "MSG_EMAIL_NOT_SENT"
    Public Const MSG_EMAIL_NOT_SENT_SEND_PROMPT As String = "MSG_EMAIL_NOT_SENT_SEND_PROMPT"
    Public Const MSG_REPORT_NOT_DEFINED As String = "MSG_REPORT_NOT_DEFINED"
    Public Const MSG_COMMENT_ADDED As String = "MSG_COMMENT_ADDED"
    Public Const MSG_CHECK_PAYMENT_ADDED As String = "MSG_CHECK_PAYMENT_ADDED"
    Public Const MSG_CANCELLATION_CANNOT_BE_PROCESSED As String = "MSG_CANCELLATION_CANNOT_BE_PROCESSED"
    Public Const MSG_REINSTATEMENT_NOT_ALLOWED_FOR_REPLACEMENT_POLICY As String = "MSG_REINSTATEMENT_NOT_ALLOWED_FOR_REPLACEMENT_POLICY"
    Public Const MSG_PROMPT_FOR_REINSTATEMENT_NOT_ALLOWED_ON_REPLACEMENT As String = "MSG_PROMPT_FOR_REINSTATEMENT_NOT_ALLOWED_ON_REPLACEMENT"
    Public Const MSG_INVALID_PAYMENT_METHOD As String = "MSG_INVALID_PAYMENT_METHOD"
    ' Public Const MSG_INVALID_AMOUNT_ENTERED As String = "MSG_INVALID_AMOUNT_ENTERED"
    ' Public Const MSG_INVALID_CANCELLATION_DATE As String = "MSG_INVALID_CANCELLATION_DATE"
    ' Public Const MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE As String = "MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE"
    ' Public Const MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS As String = "MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS"
    Public Const MSG_CANCELLATION_REASON_REQUIRES_AMT As String = "MSG_CANCELLATION_REASON_REQUIRES_AMT"
    'Public Const MSG_REFUND_AMT_BELOW_TOLERANCE As String = "MSG_REFUND_AMT_BELOW_TOLERANCE"
    Public Const MSG_EXPIRE_PREVIOUS_WQ_SCHEDULE As String = "MSG_EXPIRE_PREVIOUS_WORK_QUEUE_SCHEDULE"
    Public Const MSG_INVALID_DATE As String = "MSG_INVALID_DATE"
    Public Const MSG_INVALID_30DAYS_RANGE_DATE As String = "MSG_INVALID_30DAYS_RANGE_DATE"
    Public Const MSG_NO_RISKGROUP_FOUND As String = "MSG_NO_RISKGROUP_FOUND"
    Public Const MSG_NO_PARTSDESC_FOUND As String = "MSG_NO_PARTSDESC_FOUND"
    Public Const MSG_NO_MORE_PARTSDESC_FOUND As String = "MSG_NO_MORE_PARTSDESC_FOUND"
    Public Const MSG_CLAIMS_FOUND As String = "MSG_CLAIMS_FOUND"
    Public Const MSG_CERTIFICATES_FOUND As String = "MSG_CERTIFICATES_FOUND"
    Public Const MSG_RECORDS_FOUND As String = "MSG_RECORDS_FOUND"
    Public Const MSG_PROMPT_ARE_YOU_SURE As String = "ARE YOU SURE?"
    Public Const MSG_INVALID_COVERAGE As String = "INVALID_COVERAGE"
    Public Const Certificate_Detail As String = "Certificate_Detail"
    Public Const Cancel_Certificate As String = "Cancel Certificate"
    Public Const MSG_PROMPT_FOR_PAY_CLAIM_WITH_ZERO_AMOUNT As String = "MSG_PROMPT_FOR_PAY_CLAIM_WITH_ZERO_AMOUNT"
    Public Const MSG_PROMPT_FOR_CLAIM_PENDING_REVIEW As String = "MSG_PROMPT_FOR_CLAIM_PENDING_REVIEW"
    Public Const MSG_PAY_INVOICE_ALERT_FOR_CLAIM_PENDING_REVIEW As String = "MSG_PAY_INVOICE_ALERT_FOR_CLAIM_PENDING_REVIEW"
    Public Const MSG_SVC_LVL_DTL_UNIQUE As String = "MSG_SVC_LVL_DTL_UNIQUE"
    Public Const MSG_AUTHORIZATION_LIMIT_EXCEEDED As String = "MSG_AUTHORIZATION_LIMIT_EXCEEDED"
    Public Const MSG_AUTH_LIMIT_EXCEEDED_PICK_OTHER_SVC_CTR As String = "MSG_AUTH_LIMIT_EXCEEDED_PICK_OTHER_SVC_CTR"
    Public Const MSG_INVALID_WARRANTY_SALES_DATE As String = "MSG_INVALID_WARRANTY_SALES_DATE"
    Public Const MSG_INVALID_PRODUCT_SALES_DATE As String = "MSG_INVALID_PRODUCT_SALES_DATE"
    Public Const MSG_BEGIN_END_DATE As String = "MSG_BEGIN_END_DATE"
    Public Const MSG_INVALID_MIN_MAX_VALUE As String = "MSG_INVALID_MIN_MAX_VALUE"
    Public Const MSG_NEXT_STATUS_MUST_BE_DIFF_THAN_CURRENT_STATUS As String = "MSG_NEXT_STATUS_MUST_BE_DIFF_THAN_CURRENT_STATUS"
    Public Const MSG_INVALID_FILE_NAME As String = "MSG_INVALID_FILE_NAME"
    Public Const MSG_INVALID_YEARMONTH As String = "MSG_INVALID_YEARMONTH"
    Public Const MSG_INVALID_DEALER As String = "MSG_INVALID_DEALER"
    Public Const MSG_INVALID_SERVICE_CENTER As String = "MSG_INVALID_SERVICE_CENTER"
    Public Const MSG_ENTER_A_SERVICE_CENTER_OR_DEALER As String = "MSG_ENTER_A_SERVICE_CENTER_OR_DEALER"
    Public Const MSG_INVALID_EXTENDED_STATUS As String = "MSG_INVALID_EXTENDED_STATUS"
    Public Const MSG_INVALID_RISK_TYPE As String = "MSG_INVALID_RISK_TYPE"
    Public Const MSG_INVALID_CLAIM_TYPE As String = "MSG_INVALID_CLAIM_TYPE"
    Public Const MSG_INVOICE_NUMBER_REQUIRED As String = "MSG_INVOICE_NUMBER_REQUIRED"
    Public Const MSG_NO_PAYEE_FOUND As String = "MSG_NO_PAYEE_FOUND"
    Public Const MSG_THE_FILE_TRANSFER_HAS_COMPLETED As String = "MSG_THE_FILE_TRANSFER_HAS_COMPLETED"
    Public Const MSG_NO_PARENT_DEALER_FOUND As String = "MSG_NO_PARENT_DEALER_FOUND"
    Public Const MSG_INTERFACES_HAS_COMPLETED As String = "MSG_INTERFACES_HAS_COMPLETED"
    Public Const MSG_INVALID_PRICE_LIST As String = "MSG_INVALID_PRICE_LIST"
    Public Const MSG_INVALID_ZIP_DISTRICT As String = "MSG_INVALID_ZIP_DISTRICT"
    Public Const MSG_INVALID_MANUFACTURER As String = "MSG_INVALID_MANUFACTURER"
    Public Const MSG_INVALID_SERVICE_GROUP As String = "MSG_INVALID_SERVICE_GROUP"
    Public Const MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED As String = "MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED"
    Public Const MSG_GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND As String = "MSG_GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND"
    Public Const MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED As String = "MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED"
    Public Const MSG_NO_CLAIM_INVOICE_RECORDS_FOUND As String = "MSG_NO_CLAIM_INVOICE_RECORDS_FOUND"
    Public Const MSG_EXTERNAL_USER_DEALER_OR_SC_REQUIRED As String = "MSG_EXTERNAL_USER_DEALER_OR_SC_REQUIRED"
    Public Const MSG_CAUSE_OF_LOSS_NOT_AVAILABLE As String = "MSG_CAUSE_OF_LOSS_NOT_AVAILABLE"
    Public Const MSG_CAUSE_OF_LOSS_IS_REQUIRED As String = "MSG_CAUSE_OF_LOSS_IS_REQUIRED"
    Public Const MSG_GUI_ACCOUNTING_DATE_NOT_SELECTED As String = "MSG_GUI_ACCOUNTING_DATE_NOT_SELECTED"
    Public Const MSG_GUI_ENDORSEMENT_NUMBER_NOT_ENTERED As String = "MSG_GUI_ENDORSEMENT_NUMBER_NOT_ENTERED"
    Public Const MSG_GUI_INVALID_SELECTION As String = "MSG_GUI_INVALID_SELECTION"
    Public Const MSG_GUI_INVALID_VALUE As String = "MSG_GUI_INVALID_VALUE"
    Public Const MSG_GUI_INVALID_EMPTY_DATE As String = "MSG_GUI_INVALID_EMPTY_DATE"
    Public Const MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE As String = "MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE"
    Public Const MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE As String = "MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE"
    Public Const MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE As String = "MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE"
    Public Const MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS As String = "MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS"
    Public Const MSG_ENTERED_DATE_NOT_WITHIN_CONTRACT As String = "MSG_ENTERED_DATE_NOT_WITHIN_CONTRACT"
    Public Const MSG_TO_DEALER_ALREADY_CONTAINS_DEFINITIONS As String = "MSG_TO_DEALER_ALREADY_CONTAINS_DEFINITIONS"
    Public Const MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT As String = "MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT"
    Public Const MSG_TO_DEALER_MUST_HAVE_SAME_RECURRING_PREMIUM_SETTING As String = "MSG_TO_DEALER_MUST_HAVE_SAME_RECURRING_PREMIUM_SETTING"
    Public Const MSG_FROM_DEALER_ALREADY_CONTAINS_CERTIFICATES As String = "MSG_FROM_DEALER_ALREADY_CONTAINS_CERTIFICATES"
    Public Const MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_CANNOT_BE_DELETED As String = "MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_CANNOT_BE_DELETED"
    Public Const MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_DEALERCODE_CANNOT_BE_UPDATED As String = "MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_DEALERCODE_CANNOT_BE_UPDATED"
    Public Const MSG_DEALER_ALREADY_HAS_DEFINED_COVERAGES_DEFINITIONS_CANNOT_BE_DELETED As String = "MSG_DEALER_ALREADY_HAS_DEFINED_COVERAGES_DEFINITIONS_CANNOT_BE_DELETED"
    Public Const MSG_COPY_WAS_COMPLETED_SUCCESSFULLY As String = "MSG_COPY_WAS_COMPLETED_SUCCESSFULLY"
    Public Const MSG_NOTHING_SELECTED As String = "MSG_NOTHING_SELECTED"
    Public Const MSG_COPY_FAILED As String = "MSG_COPY_FAILED"
    Public Const MSG_DELETE_WAS_COMPLETED_SUCCESSFULLY As String = "MSG_DELETE_WAS_COMPLETED_SUCCESSFULLY"
    Public Const MSG_DELETE_FAILED As String = "MSG_DELETE_FAILED"
    Public Const MSG_RENEW_WAS_COMPLETED_SUCCESSFULLY As String = "MSG_RENEW_WAS_COMPLETED_SUCCESSFULLY"
    Public Const MSG_NO_COVERAGE_AVAILABLE As String = "MSG_NO_COVERAGE_AVAILABLE"
    Public Const MSG_RENEW_COVERAGE_FAILED As String = "MSG_RENEW_COVERAGE_FAILED"
    Public Const MSG_INVALID_AUTHORIZED_AMOUNT_ERR As String = "INVALID_AUTHORIZED_AMOUNT_ERR"
    Public Const MSG_ACTIVE_TRADEIN_QUOTE_EXISTS_ERR As String = "ACTIVE_TRADEIN_QUOTE_EXISTS_ERR"
    Public Const MSG_PERFORMING_REQUEST As String = "MSG_PERFORMING_REQUEST"
    Public Const MSG_PICKUP_DATE_IS_REQUIRED As String = "MSG_PICKUP_DATE_IS_REQUIRED"
    Public Const MSG_RECORD_SAVED_OK_NEED_TO_SELECT_COMPANY As String = "MSG_RECORD_SAVED_OK_NEED_TO_SELECT_COMPANY"
    Public Const MSG_INVALID_NUMBER As String = "MSG_INVALID_NUMBER"
    Public Const MSG_DEALER_USER_CLAIM_INTERFACES As String = "MSG_DEALER_USER_CLAIM_INTERFACES"
    Public Const MSG_POTENTIAL_SERVICE_WARRANTY As String = "MSG_POTENTIAL_SERVICE_WARRANTY"
    Public Const MSG_INVALID_COUNTRY As String = "MSG_INVALID_COUNTRY"
    Public Const MSG_INVALID_USER_ID As String = "MSG_INVALID_USER_ID"
    Public Const MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN As String = "MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN" '"Another user has modified this claim; the system must refresh this screen."
    Public Const MSG_DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED As String = "DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED"
    Public Const MSG_BILLING_STATUS_REQUIRED As String = "MSG_BILLING_STATUS_REQUIRED"
    Public Const MSG_INVALID_CLAIM_STAGE As String = "MSG_INVALID_CLAIM_STAGE"
    Public Const MSG_INVALID_CLAIM_STAGE_STATUS As String = "MSG_INVALID_CLAIM_STAGE_STATUS"
    Public Const MSG_ENTER_A_VALUE As String = "MSG_THE_VALUE_IS_REQUIRED"
    Public Const MSG_ENTER_A_VALID_VALUE As String = "MSG_ENTER_A_VALID_VALUE"
    Public Const MSG_VALUE_ALREADY_IN_USE As String = "MSG_COMBINATION_ALREADY_IN_USE"
    Public Const MSG_CANNOT_REMOVE_ADDRESS As String = "MSG_CANNOT_REMOVE_ADDRESS"

    'To Dealer Must Have a Valid Contract
    Public Const MSG_SHIPPING_INFO_UPDATED As String = "MSG_SHIPPING_INFO_UPDATED"
    Public Const MSG_POLICE_REPORT_UPDATED As String = "MSG_POLICE_REPORT_UPDATED"
    Public Const MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES As String = "MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES"
    Public Const MSG_PROMPT_FOR_SETUP_SERVICE_CENTER_OF_DEALER As String = "MSG_PROMPT_FOR_SETUP_SERVICE_CENTER_OF_DEALER"
    Public Const MSG_REPORT_REQUEST_IS_GENERATED As String = "MSG_REPORT_REQUEST_IS_GENERATED"

    'Public Const MSG_REPORT_REQUEST_IS_GENERATED As String = "MSG_REPORT_REQUEST_IS_GENERATED"
    Public Const MSG_Email_not_configured As String = "MSG_Email_not_configured"
    Public Const MSG_INVALID_DAYS As String = "MSG_INVALID_DAYS"

    Public Const MSG_INVALID_SEARCH As String = "MSG_INVALID_SEARCH"
    Public Const MSG_INVALID_LIABILITY_LIMIT As String = "MSG_INVALID_LIABILITY_LIMIT"
    Public Const MSG_INVALID_AUTO_APPROVE_PSP As String = "MSG_INVALID_AUTO_APPROVE_PSP"
    Public Const MSG_INVALID_CUIT_CUIL_NUMBER As String = "MSG_INVALID_CUIT_CUIL_NUMBER"
    Public Const PAYMENT_DUE_DATE_LESS_THAN_WSD As String = "PAYMENT_DUE_DATE_LESS_THAN_WSD"
    Public Const NEXT_BILLING_DATE_LESS_THAN_WSD As String = "NEXT_BILLING_DATE_LESS_THAN_WSD"
    Public Const MSG_CLAIM_AUTO_APPROVE_MUST_BE_SELECTED As String = "CLAIM_AUTO_APPROVE_MUST_BE_SELECTED"
    Public Const ATTRIBUTE_VALUE_REQUIRED As String = "ATTRIBUTE_VALUE_REQUIRED"
    Public Const INVALID_ATTRIBUTE As String = "INVALID_ATTRIBUTE"

    Public Const MSG_NO_INVOICE_FOUND As String = "MSG_NO_INVOICE_FOUND"
    Public Const MSG_CONN_PROBLEM As String = "MSG_CONN_PROBLEM"
    Public Const MSG_NO_REPORTS_FOUND As String = "MSG_NO_REPORTS_FOUND"

    Public Const MSG_WORKSHEET_REQUIRED As String = "MSG_WORKSHEET_REQUIRED"
    Public Const GUI_MSG_NO_EVENT_EXISTS As String = "GUI_MSG_NO_EVENT_EXISTS"

    Public Const MSG_CLAIM_DENIAL_WARNING As String = "MSG_CLAIM_DENIAL_WARNING"
    Public Const MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED As String = "MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED"
    Public Const MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_PERIOD As String = "MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_PERIOD"
    Public Const MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD As String = "CLAIM_NOT_REPORTED_WITHIN_PERIOD"
    Public Const MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED As String = "MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED"
    Public Const MSG_DEFAULT_SVC_CENTER_NOT_SETUP As String = "DEFAULT_SVC_CENTER_NOT_SETUP"

    Public Const MSG_THE_REPORT_IS_BEING_DOWNLOADED As String = "MSG_THE_REPORT_IS_BEING_DOWNLOADED"

    '5623
    Public Const MSG_INVALID_GRACE_PERIOD_DAYS As String = "MSG_INVALID_GRACE_PERIOD_DAYS"
    Public Const MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD As String = "MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD"
    Public Const MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD As String = "MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD"
    Public Const MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE As String = "MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE"

    Public Const MSG_DEALER_REQUIRED As String = "MSG_DEALER_REQUIRED"
    Public Const MSG_INVALID_SALES_PRICE_ERR As String = "INVALID_SALES_PRICE_ERR"
    Public Const MSG_INVALID_DATE_RANGE As String = "INVALID_DATE_RANGE"
    Public Const MSG_MAX_YEAR_DATE_RANGE As String = "MAX_YEAR_DATE_RANGE"
    Public Const MSG_INVALID_BEGIN_END_DATES_ERR As String = "INVALID_BEGIN_END_DATES_ERR"
    Public Const MSG_DEFINITION_NOT_FOUND_ERR As String = "DEFINITION_NOT_FOUND_ERR"

    Public Const MSG_DUPLICATE_POLICE_REPORT_NUMBER As String = "MSG_DUPLICATE_POLICE_REPORT_NUMBER"
    Public Const MSG_CONTINUE_WITH_CLAIM As String = "MSG_CONTINUE_WITH_CLAIM"
    Public Const MSG_CONTINUE As String = "MSG_CONTINUE"

    Public Const MSG_INSTANCE_EXITS As String = "MSG_INSTANCE_EXITS"
    Public Const MSG_VIEW_REPORT As String = "MSG_VIEW_REPORT"

    Public Const MSG_PROMPT_FOR_RESEND_TRANSACTION As String = "MSG_PROMPT_FOR_RESEND_TRANSACTION"
    Public Const MSG_PROMPT_FOR_HIDE_TRANSACTION As String = "MSG_PROMPT_FOR_HIDE_TRANSACTION"
    Public Const MSG_PROMPT_FOR_PROCESS_RECORDS As String = "MSG_PROMPT_FOR_PROCESS_RECORDS"

    Public Const MSG_BILLING_STATUS_CANNOT_BE_CHANGED As String = "MSG_BILLING_STATUS_CANNOT_BE_CHANGED"

    Public Const MSG_ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED As String = "ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED"

    Public Const MSG_EXPIRE_PREVIOUS_LIST As String = "MSG_EXPIRE_PREVIOUS_LIST"
    Public Const MSG_EXPIRE_PREVIOUS_ISSUE As String = "MSG_EXPIRE_PREVIOUS_ISSUE"
    Public Const MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE As String = "MSG_GUI_LIST_CODE_ASSIGN_TO_DEALER_NO_DELETE"
    Public Const MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE As String = "MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE"
    Public Const MSG_GUI_QUESTION_ASSIGNED_TO_ISSUE As String = "MSG_GUI_QUESTION_ASSIGNED_TO_ISSUE"
    Public Const MSG_GUI_ISSUE_ASSIGNED_TO_RULE As String = "MSG_GUI_ISSUE_ASSIGNED_TO_RULE"
    Public Const MSG_CALLER_NAME_REQUIRED As String = "MSG_CALLER_NAME_REQUIRED"
    Public Const MSG_COVERAGE_TYPE_REQUIRED As String = "MSG_COVERAGE_TYPE_REQUIRED"
    Public Const MSG_PROBLEM_DESCRIPTION_REQUIRED As String = "MSG_PROBLEM_DESCRIPTION_REQUIRED"

    'Questions and Generic
    Public Const MSG_GUI_OVERLAPPING_RECORDS As String = "MSG_GUI_OVERLAPPING_RECORDS"
    Public Const MSG_GUI_OVERLAPPING_RULES As String = "MSG_GUI_OVERLAPPING_RULES"

    'Price List
    Public Const MSG_GUI_PRICE_LIST_ASSIGNED_TO_SERVICE_CENTER As String = "MSG_GUI_PRICE_LIST_ASSIGNED_TO_SERVICE_CENTER"
    Public Const MSG_PRICE_LIST_DETAIL As String = "MSG_PRICE_LIST_DETAIL"
    Public Const MSG_EDIT_PRICE_LIST As String = "MSG_EDIT_PRICE_LIST"
    Public Const MSG_AVAILABLE_VENDORS As String = "AVAILABLE_VENDORS"
    Public Const MSG_SELECTED_VENDORS As String = "MSG_SELECTED_VENDORS"
    Public Const MSG_GUI_OVERLAPPING_PRICE_LIST As String = "MSG_GUI_OVERLAPPING_PRICE_LIST"
    Public Const PRICELIST_INVALID_EFFECIVE_DATE As String = "PRICELIST_INVALID_EFFECIVE_DATE"


    'CLAIM ISSUES
    Public Const MSG_CLAIM_ISSUES_PENDING As String = "MSG_CLAIM_ISSUES_PENDING"
    Public Const MSG_CLAIM_ISSUES_RESOLVED As String = "MSG_CLAIM_ISSUES_RESOLVED"
    Public Const MSG_CLAIM_ISSUES_REJECTED As String = "MSG_CLAIM_ISSUES_REJECTED"
    Public Const MSG_CLAIM_ISSUES_INVALID_DATE As String = "MSG_CLAIM_ISSUES_INVALID_DATE"
    Public Const MSG_CLAIM_ISSUES_DATE_PRIOR_TO_DATE_OF_LOSS As String = "MSG_CLAIM_ISSUES_DATE_PRIOR_TO_DATE_OF_LOSS"

    'Claim Lock
    Public Const MSG_GUI_CLAIM_IS_LOCKED As String = "MSG_GUI_CLAIM_IS_LOCKED"

    'req 861
    Public Const WRkQueue_COMPANY_USER_EXISTS As String = "WRkQueue_COMPANY_USER_EXISTS"
    Public Const MSG_USER_ALREADY_ADDED_TO_THE_WORK_QUEUE As String = "MSG_USER_ALREADY_ADDED_TO_THE_WORK_QUEUE"
    Public Const MSG_USER_ADDED_TO_THE_WORK_QUEUE As String = "MSG_USER_ADDED_TO_THE_WORK_QUEUE"
    'Req 1151
    Public Const MSG_ATLEAST_ONE_RECORD_SHLD_BE_UNCHECK As String = "ATLEAST_ONE_RECORD_SHLD_BE_UNCHEK"
    Public Const MSG_ATLEAST_ONE_RECORD_SHLD_BE_CHECKED As String = "ATLEAST_ONE_RECORD_SHLD_BE_CHECKED"

    'REQ-1162
    Public Const MSG_TRANSFER_Of_OWNERSHIP_PROMPT As String = "MSG_TRANSFER_Of_OWNERSHIP_PROMPT"

    Public Const MSG_CLAIMS_AUTHORIZATION_FOUND As String = "MSG_CLAIMS_AUTHORIZATION_FOUND"
    'Price List Details
    Public Const INVALID_ONLY_RISKTYPE_OR_EQUIPMENTCLASS_OR_EQUIPMENT_SELECTION As String = "INVALID_ONLY_RISKTYPE_OR_EQUIPMENTCLASS_OR_EQUIPMENT_SELECTION"
    Public Const INVALID_CONDITION_SELECT_WHEN_EQUIPMENT_SELECTED As String = "INVALID_CONDITION_SELECT_WHEN_EQUIPMENT_SELECTED"
    Public Const INVALID_SELECT_EQUIPMENT_LIST As String = "INVALID_SELECT_EQUIPMENT_LIST"
    Public Const MSG_SELECTED_VENDORS_CAN_NOT_BE_MOVED As String = "MSG_SELECTED_VENDORS_CAN_NOT_BE_MOVED"

    'REQ-863
    Public Const MSG_LINE_ITEM_CANNOT_BE_DELETED As String = "MSG_LINE_ITEM_CANNOT_BE_DELETED"
    Public Const MSG_AUTHORIZATION_SERVICE_CENTER As String = "MSG_CLAIM_AUTHORIZATION_SERVICE_CENTER"
    Public Const MSG_CLAIM_AUTHORIZATION_NOT_FOUND As String = "MSG_CLAIM_AUTHORIZATION_NOT_FOUND"
    Public Const MSG_CANNOT_ADD_CLAIM_AUTHORIZATION As String = "MSG_CANNOT_ADD_CLAIM_AUTHORIZATION"
    Public Const MSG_VENDOR_SKU_LIST_NOT_FOUND As String = "MSG_VENDOR_SKU_LIST_NOT_FOUND"
    Public Const MSG_CANNOT_MODIFY_CLAIM_AUTHORIZATION As String = "MSG_CANNOT_MODIFY_CLAIM_AUTHORIZATION"

    Public Const MSG_BANK_NAME_VALUE_REQUIRED As String = "MSG_BANK_NAME_VALUE_REQUIRED"
    Public Const MSG_BANK_CODE_VALUE_REQUIRED As String = "MSG_BANK_CODE_VALUE_REQUIRED"
    Public Const MSG_DUPLICATE_BANK_NAME_CODE As String = "MSG_DUPLICATE_BANK_NAME_CODE"
    Public Const MSG_DUPLICATE_BANK_NAME As String = "MSG_DUPLICATE_BANK_NAME"
    Public Const MSG_DUPLICATE_BANK_CODE As String = "MSG_DUPLICATE_BANK_CODE"

    'Req-1318
    Public Const MSG_VALIDATE_PROCESS_STARTED As String = "MSG_VALIDATE_PROCESS_STARTED"
    Public Const MSG_LOAD_PROCESS_STARTED As String = "MSG_LOAD_PROCESS_STARTED"
    Public Const MSG_GENERATE_RESPONSE_PROCESS_STARTED As String = "MSG_GENERATE_RESPONSE_PROCESS_STARTED"
    Public Const MSG_FILE_CANNOT__BE_DELETED_PLEASE_REFRESH_THE_SCREEN As String = "MSG_FILE_CANNOT__BE_DELETED_PLEASE_REFRESH_THE_SCREEN"

    'REQ-5565
    Public Const MSG_APPROVE_PRE_INVOICE As String = "MSG_APPROVE_PRE_INVOICE"
    Public Const MSG_REMOVE_CLAIM_FROM_INVOICING_CYCLE As String = "MSG_REMOVE_CLAIM_FROM_INVOICING_CYCLE"
    Public Const MSG_RECON_OVERRIDEN_SUCCESSFULLY As String = "MSG_RECON_OVERRIDEN_SUCCESSFULLY"
    Public Const MSG_COULD_NOT_OVERRIDE_RECON As String = "MSG_COULD_NOT_OVERRIDE_RECON"

    'Def-24499: Added const to display message while validating and processing records.
    Public Const MSG_FILE_CANNOT_BE_PROCESSED_PLEASE_REFRESH_THE_SCREEN As String = "MSG_FILE_CANNOT_BE_PROCESSED_PLEASE_REFRESH_THE_SCREEN"
    Public Const MSG_FILE_CANNOT_BE_VALIDATED_PLEASE_REFRESH_THE_SCREEN As String = "MSG_FILE_CANNOT_BE_VALIDATED_PLEASE_REFRESH_THE_SCREEN"

    Public Const MSG_CLAIM_PROCESS_IN_PROGRESS_ERR As String = "CLAIM_PROCESS_IN_PROGRESS_ERR" 'Req-5615
    Public Const MSG_BATCH_CLAIMS_ALREADY_CLOSED_ERR As String = "MSG_BATCH_CLAIMS_ALREADY_CLOSED" 'Req-5615
    Public Const MSG_INVOICE_REJECTED_SUCCESS As String = "INVOICE_REJECTED_SUCCESS" 'Req-5615
    Public Const MSG_BEGIN_DATE_LESSTHAN6MONTHS_ERR As String = "MSG_BEGIN_DATE_LESSTHAN6MONTHS_ERR"
    Public Const MSG_PRESENT_PAST_DATE As String = "MSG_PRESENT_PAST_DATE"
    Public Const MSG_SELECT_DEALERS As String = "MSG_SELECT_DEALERS"
    Public Const MSG_FUTURE_DATE As String = "MSG_FUTURE_DATE"
    Public Const MSG_DATE_MANDATORY As String = "MSG_DATE_MANDATORY"
    Public Const MSG_CLAIM_NUMBERS_MANDATORY As String = "MSG_GUI_CLAIM_NUMBERS_MANDATORY"
    Public Const MSG_NUMBER_NEGATIVE As String = "MSG_NUMBER_NEGATIVE"
    Public Const MSG_NUMBER_TOO_LONG As String = "MSG_NUMBER_TOO_LONG"

    'REQ - 6031 
    Public Const MSG_DATE_RANGE As String = "DATE_RANGE_CANNOT_BE_GREATER_THAN_25_MONTHS"
    Public Const PLS_SELECT_COMPANY As String = "PLEASE_SELECT_A_COMPANY"

    'Def-26680:  Added const to display message when work queue details not found.
    Public Const MSG_WORK_QUEUE_NOT_FOUND As String = "MSG_WORK_QUEUE_NOT_FOUND"

    Public Const MSG_DEALER_MUST_BE_SELECTED As String = "DEALER_MUST_BE_SELECTED_ERR"

    'REQ-5978
    Public Const MSG_MULTIPLE_COUNTRIES As String = "MSG_MULTIPLE_COUNTRIES"

    Public Const CASE_DETAIL As String = "CASE_DETAIL"

    'REQ-6150
    Public Const MSG_SELECT_A_SINGLE_DEALER As String = "MSG_SELECT_A_SINGLE_DEALER"
    Public Const MSG_INVALID_BASEON_SELECTION As String = "MSG_INVALID_BASEON_SELECTION"
    'END-REQ-6150


    Public Const MSG_ERR_MANDATORY_ISSUE_PROBLEM_DESCRIPTION As String = "ISSUE_PROBLEM_DESCRIPTION_MANDATORY_ERR" ' Please enter the problem description
    Public Const MSG_ERR_MANDATORY_ISSUE_PROBLEM_DESCRIPTION_MAX_LENGTH_ERR As String = "ISSUE_PROBLEM_DESCRIPTION_MAX_LENGTH_ERR" ' The maximum length for problem description field is 240 characters

    'Bug-176521
    Public Const MSG_ACCT_COMPANY_NOT_CONFIGURED As String = "MSG_ACCT_COMPANY_NOT_CONFIGURED"

    Public Const MSG_ERR_SELECT_A_DEALER As String = "SELECT_A_DEALER_ERR"
    Public Const MSG_ERR_SELECT_A_SERVICE_CENTER As String = "SELECT_A_SERVICE_CENTER_ERR"
    Public Const MSG_ERR_MANDATORY_MAKE_MODEL_COLOR_MEMORY As String = "MAKE_MODEL_COLOR_MEMORY_MANDATORY_ERR" ' Make/Model/Color/Memory is mandatory, provide atleast one value.

    Public Const MSG_ERR_SELECT_A_DEVICE As String = "SELECT_A_DEVICE"

    Public Const MSG_REJECT_DATE As String = "MSG_REJECT_DATE"

    Public Const MSG_IMEI_NUMBER_MANDATORY As String = "MSG_IMEI_NUMBER_MANDATORY" ' IMEI number is mandatory field
    Public Const MSG_CLAIM_EQUIPMENT_RECORD_NOT_FOUND As String = "MSG_CLAIM_EQUIPMENT_RECORD_NOT_FOUND" ' Claim Equipment record not found for the claim

    'Public Const MSG_INVALID_DOL_BYPASS_CONFIRM As String = "MSG_INVALID_DOL_BYPASS_CONFIRM"
    Public Const MSG_INVALID_DOL_BYPASS_YES As String = "BYPASS_INVALID_DOL_YES"
    Public Const MSG_INVALID_DOL_BYPASS_NO As String = "BYPASS_INVALID_DOL_NO"

    Public Const MSG_DUPLICATE_KEY_VIOLATED As String = "DUPLICATE_CODE"

    Public Const MSG_INVALID_BATCH_NUMBER As String = "INVALID_BATCH_NUMBER"

    Public Const MSG_CREDIT_CARD_NUMBER_MAX_LENGTH As String = "CREDIT_CARD_NUMBER_MAX_LENGTH" ' Maximum length of Credit Card number field is 16
    Public Const MSG_CREDIT_CARD_NUMBER_MANDATORY As String = "MSG_VALUE_MANDATORY_ERR" ' The Value Is Required
    Public Const MSG_CREDIT_CARD_NUMBER_INVALID_FORMAT As String = "INVALID_CREDIT_CARD_NUMBER" ' Invalid Credit Card Number

    Public Const MSG_CUSOTMER_IDENTITY_LOOKUP_ERR As String = "MSG_CUSOTMER_IDENTITY_LOOKUP_ERR"

    Public Const REPROCESS_RECORD_CONFIRMATION As String = "MSG_REPROCESS_INITIATED_OK"

    Public Const MSG_PROMPT_FOR_FORGOTTEN As String = "MSG_PROMPT_FOR_FORGOTTEN"

    Public Const MSG_PROMPT_FOR_CANCEL_SHIPMENT As String = "MSG_PROMPT_FOR_CANCEL_SHIPMENT"

    Public Const MSG_PROMPT_FOR_RESHIPMENT As String = "MSG_PROMPT_FOR_RESHIPMENT"

    Public Const MSG_PROMPT_RESHIPMENT_REASON As String = "MSG_PROMPT_RESHIPMENT_REASON"

    Public Const MSG_PROMPT_CANCEL_SHIPMENT_NOT_ALLOWED As String = "MSG_PROMPT_CANCEL_SHIPMENT_NOT_ALLOWED"


    Public Const MSG_ERR_SELECT_A_FULFILLMENT_OPTIONS As String = "SELECT_A_FULFILLMENT_OPTIONS"
    Public Const MSG_ERR_SELECT_A_LOGISTIC_OPTION As String = "SELECT_A_LOGISTIC_OPTION"
    Public Const MSG_ERR_SELECT_EXPEDITED_DELIVERY_BUTTON As String = "MSG_ERR_SELECT_EXPEDITED_DELIVERY_BUTTON"
    Public Const MSG_ERR_STORE_NUMBER_MANDATORY As String = "MSG_ERR_STORE_NUMBER_MANDATORY" ' Store number is mandatory
    Public Const MSG_ERR_DELIVERY_DATE_MANDATORY As String = "MSG_ERR_DELIVERY_DATE_MANDATORY" ' Delivery date is mandatory
    Public Const MSG_ERR_COUNTRY_POSTAL_MANDATORY As String = "MSG_ERR_COUNTRY_POSTAL_MANDATORY" ' Country and Postal code is mandatory to get the estimated delivery date
    Public Const MSG_ERR_DEFAULT_SERVICE_CENTER As String = "MSG_ERR_DEFAULT_SERVICE_CENTER" ' Default service center for the selected country is not set up
    Public Const MSG_ERR_COURIER_PRODUCT_NOT_FOUND As String = "MSG_ERR_COURIER_PRODUCT_NOT_FOUND" ' Configured courier product data is not found
    Public Const MSG_ERR_GET_DELIVERY_DATE As String = "MSG_ERR_GET_DELIVERY_DATE" ' Country/Postal code is changed, please get the delivery date again
    Public Const MSG_ERR_DEFAULT_SERVICE_CENTER_ADDRESS As String = "MSG_ERR_DEFAULT_SERVICE_CENTER_ADDRESS" ' Address of the default service center for the select country is not set up
    Public Const MSG_ERR_ESTIMATED_DELIVERY_DATE_NOT_FOUND As String = "MSG_ERR_ESTIMATED_DELIVERY_DATE_NOT_FOUND" ' Estimated delivery date is not found for the country/zip code
    Public Const MSG_ERR_ESTIMATED_DELIVERY_SLOT_NOT_FOUND As String = "MSG_ERR_ESTIMATED_DELIVERY_SLOT_NOT_FOUND" ' Estimated delivery date is not found for the TimeSlot
    Public Const MSG_PROMPT_COUNTRY_REQUIRED As String = "MSG_PROMPT_COUNTRY_REQUIRED"
    Public Const MSG_PROMPT_STATE_NOT_CONFIGURED As String = "MSG_PROMPT_STATE_NOT_CONFIGURED"
    Public Const MSG_PROMPT_ZIPCODE_REQUIRED As String = "MSG_PROMPT_ZIPCODE_REQUIRED"
    Public Const MSG_PROMPT_ADDRESS_REQUIRED As String = "MSG_PROMPT_ADDRESS_REQUIRED"
    Public Const MSG_PROMPT_ADDRESS1_FIELD_IS_REQUIRED As String = "ADDRESS1_FIELD_IS_REQUIRED"

    Public Const MSG_ERR_SERVICE_CENTER_MANDATORY As String = "SERVICE_CENTER_IS_REQUIRED" ' Service Center is mandatory

    Public Const MSG_CODE_EXISTS_PROMPT As String = "MSG_CODE_EXISTS_PROMPT"
    Public Const MSG_END_DATE_CANNOT_BE_PAST_DATE As String = "MSG_END_DATE_CANNOT_BE_PAST_DATE"

    Public Const MSG_CERT_AUTO_NUM_GEN_IS_NO As String = "MSG_CERT_AUTO_NUM_GEN_IS_NO"
    Public Const MSG_COUNTRY_ID_FOR_LOB_NOT_FOUND As String = "MSG_COUNTRY_ID_FOR_LOB_NOT_FOUND"
    Public Const MSG_SELECT_LOB_WITH_AUTOGEN As String = "MSG_SELECT_LOB_WITH_AUTOGEN"
    Public Const MSG_CERT_AUTO_NUM_GEN_YES_IND_POL As String = "MSG_CERT_AUTO_NUM_GEN_YES_IND_POL"
    Public Const MSG_GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND As String = "GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND"
    Public Const MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT As String = "MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT"

    Public Const MSG_ERR_ESTIMATED_DELIVERY_DATE_TIME_NOT_SELECTABLE As String = "MSG_ERR_ESTIMATED_DELIVERY_DATE_TIME_NOT_SELECTABLE" ' Estimated delivery date/time is not available for selection
    Public Const MSG_ERR_WHEN_DEALER_MARKUP_ALLOWED_TAX_TYPE_SHOULD_BE_EMPTY_OR_POS As String = "MSG_ERR_WHEN_DEALER_MARKUP_ALLOWED_TAX_TYPE_SHOULD_BE_EMPTY_OR_POS_TYPE"
    Public Const MSG_TAX_METHOD_COMPUTE_ON_GROSS_NOT_ALLOWED_ON_1ST_BRACKET As String = "MSG_TAX_METHOD_COMPUTE_ON_GROSS_NOT_ALLOWED_ON_1ST_BRACKET"

    Public Const MSG_TAX_DETAILS_POPUP_ERROR As String = "MSG_TAX_DETAILS_POPUP_ERROR"




#End Region
End Class

