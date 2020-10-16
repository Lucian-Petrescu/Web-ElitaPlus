Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualBasic
Imports System.Web.Services
Imports System.Xml.Linq
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita
Imports System.Text

Namespace Certificates
    Partial Class CertificateForm
        Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "~/Certificates/CertificateForm.aspx"
        Public Const URL_CertList As String = "~/Certificates/CertificateListForm.aspx"
        Public Const URL_BANKINFO As String = "~/Certificates/BankInfoForm.aspx"
        'KDDI
        Private Const ValidateAddressButton As String = "btnValidate_Address"
        Public Const NOTHING_SELECTED As Integer = 0

        Public Const CLOSED As String = "C"
        Public Const NO As String = "N"
        Public Const YES As String = "Y"
        Public Const SEMICOLON As Char = CType(":", Char)
        Public Const AcselXIntegrationComputeMthd As String = "22"
        Public Const CancRsn_VOID As String = "VOID"
        Public Const SUCCESS = "Success"
        Public Const FUTURE = "Future"
        Public Const CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD = "CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
        Public Const REFUND_METHOD_SEPA = "RFM-SEPA"
        'Data Protection Tab
        Public Const RestrictCode As String = "R"
        Public Const UnRestrictCode As String = "UR"
        Public Const ForgottenCode As String = "F"
        Public Const EntityType As String = "Certificate"
        Public Const CommentCode As String = "RESTRICT"
        Public Const CallerName As String = "DPO"
        Public Const RestrictionListCode As String = "RST"
        'Tab related
        Private Const TAB_TOTAL_COUNT As Integer = 14
        Public Const CERT_DETAIL_TAB As Integer = 0
        Public Const CERT_INFORMATION_TAB As Integer = 1
        Public Const CERT_ITEMS_INFO_TAB As Integer = 2
        Public Const CERT_PREMIUM_INFO_TAB As Integer = 3
        Public Const CERT_CANCEL_REQUEST_INFO_TAB As Integer = 4
        Public Const CERT_CANCELLATION_INFO_TAB As Integer = 5
        Public Const CERT_COMMENTS_TAB As Integer = 6
        Public Const CERT_ENDORSEMENTS_TAB As Integer = 7
        Public Const CERT_TAX_ID_TAB As Integer = 8
        Public Const CERT_CERT_HISTORY_TAB As Integer = 9
        Public Const CERT_COVERAGE_HISTORY_TAB As Integer = 10
        Public Const CERT_FINANCED_TAB As Integer = 11
        Public Const CERT_REPRICE_TAB As Integer = 12
        'Data Protection Tab
        Public Const CERT_DATA_PROTECTION_TAB As Integer = 13
        Public Const CERT_EXTENDED_FIELDS_TAB As Integer = 14
        Public Const Total_installments As Integer = 0
        Public Const Remaining_Installments As Integer = 1
        Public Const Amount_Collected As Integer = 2
        Public Const installments_Collected As Integer = 3
        Public Const remaining_amount As Integer = 4
        Public Const CertHistoryGridColInforceDateIdx As Integer = 4
        Public Const CertHistoryGridColProcessedDateIdx As Integer = 5
        Public Const CertHistoryGridColStatusChangeDateIdx As Integer = 22

#Region "Coverage Grid"
        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL As String = "btnEditCoverage"
        Public Const GRID_COL_RISK_TYPE_DESCRIPTION_IDX As Integer = 0
        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX As Integer = 1
        Public Const GRID_COL_SEQUENCE_IDX As Integer = 2
        Public Const GRID_COL_BEGIN_DATE_IDX As Integer = 3
        Public Const GRID_COL_END_DATE_IDX As Integer = 4
        Public Const GRID_COL_COVERAGE_DURATION_IDX As Integer = 5
        Public Const GRID_COL_COVERAGE_EXPIRATION_DATE_IDX As Integer = 6
        Public Const GRID_COL_MAX_RENEWAL_DURATION_IDX As Integer = 7
        Public Const GRID_COL_NO_OF_RENEWALS_IDX As Integer = 8
        Public Const GRID_COL_NO_OF_RENEWALS_REMAINING_IDX As Integer = 9
        Public Const GRID_COL_RENEWAL_DATE_IDX As Integer = 10
        Public Const GRID_COL_COVERAGE_TOTAL_PAID_AMOUNT_IDX As Integer = 11
        Public Const GRID_COL_COVERAGE_REMAIN_LIABILITY_LIMIT_IDX As Integer = 12
        'Public Const GRID_COL_BEGIN_KM_IDX As Integer = 10
        'Public Const GRID_COL_END_KM_IDX As Integer = 11

#End Region

#Region "Claim Grid"
        Public Const GRID_COL_CLAIM_NUMBER_CTRL As String = "btnEditClaim"
        Public Const GRID_COL_CLAIM_NUMBER As Integer = 0
        Public Const GRID_COL_CREATED_DATE As Integer = 1
        Public Const GRID_COL_STATUS_CODE As Integer = 2
        Public Const GRID_COL_AUTHORIZED_AMOUNT As Integer = 3
        Public Const GRID_COL_TOTAL_PAID As Integer = 4
        Public Const GRID_COL_EXTENDED_STATUS As Integer = 5
#End Region

#Region "Item Grid"
        Public Const ITEMS_GRID_COL_EDIT_CTRL As String = "btnEditItem"
        Public Const ITEMS_GRID_COL_ITEM_NUMBER_IDX As Integer = 0
        Public Const ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX As Integer = 1
        Public Const ITEMS_GRID_COL_ITEM_DESCIPTION_IDX As Integer = 2
        Public Const ITEMS_GRID_COL_MAKE_IDX As Integer = 3
        Public Const ITEMS_GRID_COL_MODEL_IDX As Integer = 4
        Public Const ITEMS_GRID_COL_EXPIRATION_DATE_IDX As Integer = 5
        Public Const ITEMS_GRID_COL_BENEFIT_STATUS_IDX As Integer = 6
#End Region

#Region "Registered Item Grid"
        Public Const REG_ITEMS_GRID_COL_EDIT_CTRL As String = "btnRegEditItem"
        Public Const REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX As Integer = 0
        Public Const REG_ITEMS_COL_DEVICE_TYPE_IDX As Integer = 1
        Public Const REG_ITEMS_COL_ITEM_DESCRIPTION_IDX As Integer = 2
        Public Const REG_ITEMS_COL_MAKE_IDX As Integer = 3
        Public Const REG_ITEMS_COL_MODEL_IDX As Integer = 4
        Public Const REG_ITEMS_COL_PURCHASE_DATE_IDX As Integer = 5
        Public Const REG_ITEMS_COL_PURCHASE_PRICE_IDX As Integer = 6
        Public Const REG_ITEMS_COL_SERIAL_NUMBER_IDX As Integer = 7
        'REQ-6002
        Public Const REG_ITEMS_COL_REGISTRATION_DATE_IDX As Integer = 8
        Public Const REG_ITEMS_COL_RETAIL_PRICE_IDX As Integer = 9
        Public Const REG_ITEMS_COL_EXPIRATION_DATE_IDX As Integer = 10
        Public Const REG_ITEMS_COL_ITEM_STATUS_IDX As Integer = 11
#End Region

#Region "Comment Grid"
        Public Const COMMENT_GRID_COL_EDIT_CTRL As String = "btnEditItem"
        Public Const GRID_COL_TIME_STAMP As Integer = 0
        Public Const GRID_COL_CALLER_NAME As Integer = 1
        Public Const GRID_COL_ADDED_BY As Integer = 2
        Public Const GRID_COL_COMMENT_TEXT As Integer = 3
#End Region

#Region "Data Protection Grid"

        Public Const GDP_COL_REQUEST_ID As Integer = 0
        Public Const GDP_COL_COMMENTS As Integer = 1
        Public Const GDP_COL_ADDED_BY As Integer = 2
        Public Const GDP_COL_TIME_STAMP As Integer = 3
        Public Const GDP_COL_STATUS As Integer = 4
        Private Const GRID_CTRL_NAME_REQUEST_ID As String = "txtRequestID"
        Private Const GRID_CTRL_NAME_COMMENT As String = "txtComment"
        Private Const GRID_CTRL_NAME_REQUEST_ID_HEADER As String = "lblRequestHeader"
        Private Const GRID_CTRL_NAME_COMMENT_HEADER As String = "lblCommentHeader"

#End Region

#Region "Cert Extended Fields Grid"

        Public Const CERT_EXT_ID_ID_IDX As Integer = 0
        Public Const CERT_EXT_CERT_ID_IDX As Integer = 1
        Public Const CERT_EXT_FIELD_NAME_IDX As Integer = 2
        Public Const CERT_EXT_FIELD_VALUE_IDX As Integer = 3
        Public Const CERT_EXT_CREATED_BY_IDX as Integer = 4
        Public Const CERT_EXT_CREATED_DATE_IDX as Integer = 5
        Public Const CERT_EXT_MODIFIED_BY_IDX as Integer = 6
        Public Const CERT_EXT_MODIFIED_DATE_IDX as Integer = 7

#End Region

#Region "Endorsement Grid"
        Public Const ENDORSEMENT_GRID_COL_EDIT_CTRL As String = "btnEditItem"
        Public Const GRID_COL_ENDORSE_NUMBER As Integer = 0
        Public Const GRID_COL_ENDORSE_CREATED_BY As Integer = 1
        Public Const GRID_COL_ENDORSE_CREATED_DATE As Integer = 2
        Public Const GRID_COL_ENDORSE_TYPE As Integer = 3
        Public Const GRID_COL_ENDORSE_ENDORSEMENT_REASON As Integer = 4
        Public Const GRID_COL_ENDORSE_EFFECTIVE_DATE As Integer = 5
        Public Const GRID_COL_ENDORSE_EXPIRATION_DATE As Integer = 6
#End Region

#Region "Sales Tax Details Repeater"
        Public Const REPEATER_COL_TAX_TYPE As String = "tax_type"
        Public Const REPEATER_COL_TAX1_DESCRIPTION As String = "tax1_description"
        Public Const REPEATER_COL_TAX1 As String = "tax1"
        Public Const REPEATER_COL_TAX2_DESCRIPTION As String = "tax2_description"
        Public Const REPEATER_COL_TAX2 As String = "tax2"
        Public Const REPEATER_COL_TAX3_DESCRIPTION As String = "tax3_description"
        Public Const REPEATER_COL_TAX3 As String = "tax3"
        Public Const REPEATER_COL_TAX4_DESCRIPTION As String = "tax4_description"
        Public Const REPEATER_COL_TAX4 As String = "tax4"
        Public Const REPEATER_COL_TAX5_DESCRIPTION As String = "tax5_description"
        Public Const REPEATER_COL_TAX5 As String = "tax5"
        Public Const REPEATER_COL_TAX6_DESCRIPTION As String = "tax6_description"
        Public Const REPEATER_COL_TAX6 As String = "tax6"
        Public Const REPEATER_COL_TAX_TOTAL_VALUE As String = "tax_total"
        Public Const PARAM_CERTICATE_ID As Integer = 0
        Public Const PARAM_LANGAUGE_ID As Integer = 1


#End Region

#Region "Installment History Grid"
        Public Const INSTALLMENT_HISTORY_GRID_COL_START_DATE As Integer = 0
        Public Const INSTALLMENT_HISTORY_GRID_COL_END_DATE As Integer = 1
        Public Const INSTALLMENT_HISTORY_GRID_COL_INSTALLMENT_AMOUNT As Integer = 2
#End Region


        Public Const CLAIMS_GRID_TOTAL_COLUMNS As Integer = 7

        Public Const CLAIM_FORM_URL As String = "../Claims/ClaimForm.aspx"
        Public Const CREATE_NEW_ENDORSEMENT As String = "new_endorsement"
        Public Const CREATE_NEW_CHECK_PAYMENT As String = "new_check_payment"
        Public Const CREATE_NEW_BILLING_PAYMENT As String = "billing_payment_selected"
        Public Const CREATE_NEW_ITEM As String = "new_item_selected"
        Public Const CREATE_NEW_REGISTER_ITEM As String = "new_reg_item_selected"
        Public Const BILLING_HISTORY_FORM_URL As String = "BillingHistoryListForm.aspx"


        Public Const MANUFACTURER As String = "Manufacturer"
        Public Const CERT_STATUS As String = "A"
        Public Const CERT_CANCEL_STATUS As String = "C"
        Public Const CANCEL_REASON_EXP As String = "EXP"
        Public Const COL_CERT_ITEM_ID As String = "cert_item_id"
        Public Const CODE As String = "Code"
        Public Const DESCRIPTION As String = "Description"
        Public Const FIRST_ROW As Integer = 0
        Public Const CERT_CAN_REQ_ACCEPTED As String = "CERT_CANCEL_REQUEST_STATUS-A"
        Public Const CERT_CAN_REQ_DENIED As String = "CERT_CANCEL_REQUEST_STATUS-D"

        Private Const NO_CONTRACT_FOUND As String = "NO_CONTRACT_FOUND"
        Private Const NO_COVERAGE_FOUND As String = "NO_COVERAGE_FOUND"
        Private Const CERTIFICATEDFORM_FORM001 As String = "CERTIFICATEDFORM_FORM001" ' The filename does not exists or it is empty
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        'Public Const INVALID_CANCELLATION_DATE As String = "INVALID_CANCELLATION_DATE"
        'Public Const INVALID_CANCELLATION_REASON_FOR_CERTIFICATE As String = "INVALID_CANCELLATION_REASON_FOR_CERTIFICATE"
        'Public Const CANCELLATION_REASON_REQUIRES_AMT As String = "CANCELLATION_REASON_REQUIRES_AMT"
        'Public Const INVALID_AMOUNT_ENTERED As String = "INVALID_AMOUNT_ENTERED"
        'Public Const REFUND_AMT_BELOW_TOLERANCE As String = "REFUND_AMT_BELOW_TOLERANCE"
        Public Const IDENTIFICATION_NUMBER_TYPE_DEFAULT = "IDENTIFICATION_NUMBER"

#Region "SalesTaxesDetail"
        Private Const COL_SALES_TAX1 As String = "tax1"
        Private Const COL_SALES_TAX2 As String = "tax2"
        Private Const COL_SALES_TAX3 As String = "tax3"
        Private Const COL_SALES_TAX4 As String = "tax4"
        Private Const COL_SALES_TAX5 As String = "tax5"
        Private Const COL_SALES_TAX6 As String = "tax6"
        Private Const COL_SALES_TAX1_DESCRIPTION As String = "tax1_description"
        Private Const COL_SALES_TAX2_DESCRIPTION As String = "tax2_description"
        Private Const COL_SALES_TAX3_DESCRIPTION As String = "tax3_description"
        Private Const COL_SALES_TAX4_DESCRIPTION As String = "tax4_description"
        Private Const COL_SALES_TAX5_DESCRIPTION As String = "tax5_description"
        Private Const COL_SALES_TAX6_DESCRIPTION As String = "tax6_description"
#End Region

#End Region

#Region "Variables"

        Private mbIsFirstPass As Boolean = True
        Private gIsSinglePremium As String
        Private mIsExtendCovByAutoRenew As String

        'Tabs related
        Private listDisabledTabs As New Collections.Generic.List(Of Integer)
        Private SourceCertificateId As Guid

#End Region

#Region "Attributes"

        Private moCertificate As Certificate
        'Dim IsEdit As Boolean
        Dim allCoveragesExpired As Boolean = False
        Dim ExpiredCoveragesExist As Boolean = False

#End Region

#Region "Properties"

        Private Property CertId() As Guid
            Get
                Return State.CertificateId
            End Get
            Set(Value As Guid)
                State.CertificateId = Value
            End Set

        End Property

        Private ReadOnly Property TheCertificate() As Certificate
            Get
                moCertificate = New Certificate(CertId)
                Return moCertificate
            End Get
        End Property

        Public ReadOnly Property AddressCtr() As UserControlAddress_New
            Get
                Return moAddressController
            End Get
        End Property

        'Public ReadOnly Property BankInfoCtr() As UserControlBankInfo
        '    Get
        '        Return moBankInfoController
        '    End Get
        'End Property

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property UserCertificateCtrCancel() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoCtrlCancel Is Nothing Then
                    moCertificateInfoCtrlCancel = CType(FindControl("moCertificateInfoCtrlCancel"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoCtrlCancel
            End Get
        End Property

        Public ReadOnly Property UserBankInfoCtr() As UserControlBankInfo_New
            Get
                If moBankInfoController Is Nothing Then
                    moBankInfoController = CType(FindControl("moBankInfoController"), UserControlBankInfo_New)
                End If
                Return moBankInfoController
            End Get
        End Property

        Public ReadOnly Property UserPaymentOrderInfoCtr() As UserControlPaymentOrderInfo
            Get
                If moPaymentOrderInfoCtrl Is Nothing Then
                    moPaymentOrderInfoCtrl = CType(FindControl("moPaymentOrderInfoCtrl"), UserControlPaymentOrderInfo)
                End If
                Return moPaymentOrderInfoCtrl
            End Get
        End Property

        Public ReadOnly Property CancUserBankCancInfoCtr() As UserControlBankInfo_New
            Get
                If moCancBankInfoController Is Nothing Then
                    moCancBankInfoController = CType(FindControl("moCancBankInfoController"), UserControlBankInfo_New)
                End If
                Return moCancBankInfoController
            End Get
        End Property

        Public ReadOnly Property UserPaymentOrderCancInfoCtr() As UserControlPaymentOrderInfo
            Get
                If moCancPaymentOrderInfoCtrl Is Nothing Then
                    moCancPaymentOrderInfoCtrl = CType(FindControl("moCancPaymentOrderInfoCtrl"), UserControlPaymentOrderInfo)
                End If
                Return moCancPaymentOrderInfoCtrl
            End Get
        End Property

        Public Property WorkingPanelVisible As Boolean
            Get
                Return State.WorkingPanelVisible
            End Get
            Set(value As Boolean)
                State.WorkingPanelVisible = value

                ControlMgr.SetVisibleControl(Me, CancellationPanel, Not State.WorkingPanelVisible)
                ControlMgr.SetVisibleControl(Me, moCertificateInfoCtrlCancel, Not State.WorkingPanelVisible)

                ControlMgr.SetVisibleControl(Me, WorkingPanel, State.WorkingPanelVisible)
                ControlMgr.SetVisibleControl(Me, moCertificateInfoController, State.WorkingPanelVisible)

                If State.WorkingPanelVisible Then
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Certificate_Detail)
                Else
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Cancel_Certificate)
                End If
            End Set
        End Property

        Public ReadOnly Property HasInstallment As Boolean
            Get
                If State.TheDirectDebitState.certInstallment Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property


        Public ReadOnly Property IsSinglePremium As Boolean
            Get
                If gIsSinglePremium Is Nothing Then
                    Dim oContract As New Contract

                    oContract = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value)

                    If oContract Is Nothing Then
                        Throw New GUIException(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
                    Else
                        Dim emptyGuid As Guid = Guid.Empty

                        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)


                        If oContract.RecurringPremiumId.Equals(singlePremiumId) OrElse oContract.RecurringPremiumId.Equals(emptyGuid) Then
                            gIsSinglePremium = "Y"
                        Else
                            gIsSinglePremium = "N"
                        End If
                    End If
                End If

                IsSinglePremium = (gIsSinglePremium = "Y")
            End Get
        End Property

        Public ReadOnly Property IsExtendCovByAutoRenew As Boolean
            Get
                If mIsExtendCovByAutoRenew Is Nothing Then
                    Dim oContract As New Contract

                    oContract = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value)

                    If oContract Is Nothing Then
                        Throw New GUIException(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
                    Else
                        Dim emptyGuid As Guid = Guid.Empty

                        Dim ExtendCovByAutoRenewId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COVERAGE_EXTENSIONS, Codes.COV_EXT_AUTOMATIC)


                        If oContract.ExtendCoverageId.Equals(ExtendCovByAutoRenewId) OrElse oContract.RecurringPremiumId.Equals(emptyGuid) Then
                            mIsExtendCovByAutoRenew = "Y"
                        Else
                            mIsExtendCovByAutoRenew = "N"
                        End If
                    End If
                End If

                IsExtendCovByAutoRenew = (mIsExtendCovByAutoRenew = "Y")
            End Get
        End Property

        'KDDI
        Protected Sub EnableDisableAddressValidation()
            Dim btnValidate_Address As Button = AddressCtr.FindControl(ValidateAddressButton)
            If State.MyBO.Dealer IsNot Nothing AndAlso State.MyBO.Dealer.Validate_Address = Codes.EXT_YESNO_Y Then
                ControlMgr.SetVisibleControl(Me, btnValidate_Address, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnValidate_Address, False)
            End If
        End Sub

#End Region

#Region "Page Return Type"

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Certificate
            Public BoChanged As Boolean = False
            Public IsCallerAuthenticated As Boolean = False
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.BoChanged = boChanged
                Me.IsCallerAuthenticated = IsCallerAuthenticated
            End Sub
        End Class

#End Region

#Region "Page State"

        Class BaseState
            Public NavCtrl As INavigationController
        End Class

#Region "MyState"


        Class MyState
            Public ClaimsearchDV As Certificate.CertificateClaimsDV = Nothing
            Public CoveragesearchDV As CertItemCoverage.CertItemCoverageSearchDV = Nothing
            Public CertHistoryDV As Certificate.CertificateHistoryDV = Nothing
            Public CertInstallmentHistoryDV As Certificate.CertInstallmentHistoryDV = Nothing
            Public CertExtensionsDV As Certificate.CertExtensionsDV = Nothing
            Public CertUpgradeExtensionsDV As Certificate.CertUpgradeExtensionsDV = Nothing



#Region "MyState Item-Coverage"
            Class ItemCoverageState

#Region "MyState ItemCoverageState Variables"

                Public cert_item_id As Guid = Guid.Empty
                Public hi As String
                Public selectedItemId As Guid = Guid.Empty
                Public PageIndex As Integer = 0
                Public IsGridVisible As Boolean = False
                Public manufaturerWarranty As Boolean = False
                Public IsRetailer As Boolean = True
                Public dealerName As String
                Public CoverageHistoryDV As DataView
#End Region

                Public Sub New()

                End Sub
            End Class
#Region "MyState Items"
            Class ItemsState

#Region "MyState ItemsState Variables"

                Public cert_item_id As Guid = Guid.Empty
                Public hi As String
                Public selectedItemId As Guid = Guid.Empty
                Public selectedRegisteredItemId As Guid = Guid.Empty
                Public PageIndex As Integer = 0
#End Region

                Public Sub New()

                End Sub
            End Class

#Region "MyState Installment"

            Class DirectDebitState
                Public certInstallment As CertInstallment
                'Public billingHeader As BillingHeader
                Public BillingDetailTotal As DataView
                Public BillingLastRecord As DataView
                Public bankInfo As BankInfo
                Public CreditCardInfo As CreditCardInfo
                Public StatusChenge As Boolean = False

                Public Sub New()

                End Sub
            End Class
#End Region


#Region "MyState EndorsementsState"
            Class EndorsementsState

#Region "MyState EndorsementsState Variables"
                Public coveragebegindate As Date
                Public covererageenddate As Date
                Public coverageDV As DataView

#End Region
                Public Sub New()

                End Sub
            End Class
#End Region
#End Region
#End Region
#Region "MyState Variables"
            Public MyBO As Certificate
            Public BankInfoBO As BankInfo
            Public PaymentOrderInfoBO As PaymentOrderInfo

            Public CancBankInfoBO As BankInfo
            Public CancPaymentOrderInfoBO As PaymentOrderInfo
            Public ScreenSnapShotBO As Certificate
            Public ValFlag As String = MyBO.VALIDATION_FLAG_NONE
            Public IsEdit As Boolean = False
            Public certInstallment As CertInstallment
            Public certCancellationBO As CertCancellation
            Public certCancelRequestBO As CertCancelRequest
            Public CancellationRequestedDate As Date
            Public useExistingBankInfo As String
            Public CancReasonIsLawful As String
            Public CancReasonCode As String
            Public CancelRulesForSFR As String
            Public RefundComputeMethod As String
            Public ComputeCancellationDateEOfM As String
            Public CertTerm As Integer
            Public CertInstalBankInfoId As Guid
            Public CRequestBankInfoBO As BankInfo
            Public RefundBankInfoBO As BankInfo
            Public CertCancelRequestId As Guid
            Public CancReqCommentBO As Comment
            Public selectedClaimItemId As Guid = Guid.Empty
            Public SelectedCommentId As Guid = Guid.Empty
            Public SelectedEndorseId As Guid = Guid.Empty
            Public SelectedCertExtId As Guid = Guid.Empty
            Public isPremiumTAbVisible As Boolean = True
            Public PageIndex As Integer = 0
            Public PageIndexClaimsGrid As Integer = 0
            Public IsGridVisible As Boolean = False
            Public CommentsPageIndex As Integer = 0
            Public CertExtFieldsPageIndex As Integer = 0
            Public IsCommentsGridVisible As Boolean = False
            Public IsCertExtFieldsGridVisible As Boolean = False
            Public EndorsementsPageIndex As Integer = 0
            Public ItemsPageIndex As Integer = 0
            Public ExtensionGridPageIndex As Integer = 0
            Public IsEndorsementsGridVisible As Boolean = False
            Public IsCertHistoryGridVisible As Boolean = False
            Public certificateChanged As Boolean = False
            Public NavigateToComment As Boolean = False
            Public NavigateToCheckPayment As Boolean = False
            Public NavigateToEndorment As Boolean = False
            Public NavigateToNewCertItem As Boolean = False
            Public NavigateToBillingHistory As Boolean = False
            Public NavigateToBankInfoHistory As Boolean = False
            Public NavigateToPaymentHistory As Boolean = False
            Public NavigateToBillpayHistory As Boolean = False
            Public NavigateToCustProfileHistory As Boolean = False
            Public NavigateToItems As Boolean = False
            Public isSalutation As Boolean = False
            Public isPostPrePaid As Boolean = False
            Public isItemsGridVisible As Boolean = True
            Public isRegItemsGridVisible As Boolean = True
            Public isCovgGridRefreshNeeded As Boolean = False
            Public IsClaimAllowed As Boolean = True 'DEF-2486
            Public AllowOldAndNewDCMClaims As Boolean = False 'Task-203459
            Public InstallmentHistoryPageIndex As Integer = 0

            Public CommentsSortExpression As String = Comment.CommentSearchDV.COL_CREATED_DATE & " DESC"
            Public ClaimsSortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUM & " DESC"
            Public CoverageSortExpression As String = CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE & " DESC"
            Public CertHistorySortExpression As String = Certificate.CertificateHistoryDV.COL_PROCESSED_DATE & " ASC"
            Public CertInstallmentHistorySortExpression As String = Certificate.CertInstallmentHistoryDV.COL_START_DATE & " ASC"
            Public CertExtensionSortExpression As String = Certificate.CertExtensionsDV.COL_FIELD_NAME & " ASC"
            Public CertUpgradeExtensionSortExpression As String = Certificate.CertUpgradeExtensionsDV.COL_SEQUENCE_NUMBER & " ASC"
            Public CertExtFieldsSortExpression As String = Certificate.CertExtendedFieldsDv.COL_FIELD_NAME & " ASC"
            Public IsRepriceGridVisible As Boolean = False
            Public IsNew As Boolean
            Public CertificateId As Guid
            Public LanguageId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public TheItemCoverageState As ItemCoverageState
            Public TheItemsState As ItemsState
            Public TheDirectDebitState As DirectDebitState
            Public WorkingPanelVisible As Boolean
            Public BankInfoCtrVisible As Boolean
            Public PaymentOrderInfoCtrVisible As Boolean
            Public IsNewBillPayBtnVisible As Boolean = False

            Public companyCode As String
            Public InsPremiumFactor As Decimal
            Public grossAmtReceived As Decimal
            Public oBillingTotalAmount As Decimal
            Public oBillingCount As Int32

            Public oPaymentCount As Int32
            Public oBillPayCount As Int32
            Public oPayColltCount As Int32
            Public CountryOfPurchase As String
            Public validateAddress As Boolean = False

            Public CreditCardInfoBO As CreditCardInfo

            Public directDebitPayment As Boolean = False
            Public creditCardPayment As Boolean = False
            Public dealerBillPayment As Boolean = False
            Public BillingInformationChanged As Boolean = False
            Public BillingStatusId As Guid
            Public ReqCustomerLegalInfoId As Guid
            Public ReqAMLRegulationsId As Guid
            Public QuotedRefundAmt As Decimal
            Public QuotedInstallmentsPaid As Long
            Public CancCommentBO As Comment

            Public PageSize As Integer = 30

            Public EmailIsNull As String = Nothing

            Public selectedTab As Integer = 0
            Public disabledTabs As String = String.Empty
            Public ClaimRecordingXcd As String

            'Data Protection         

            Public DataProtectionHistBO As DataProtectionHistory
            Public DBCommentBO As Comment
            Public DBCertificateBO As Certificate
            Public CertForgotBO As CertForgotRequest
            Public AllowForget As String

            ' IP Migration
            Public PreviousCertificate As Certificate
            Public OriginalCertificate As Certificate

            'KDDI
            Public AddressFlag As Boolean = True
            'Authentication
            Public IsCallerAuthenticated As Boolean = False
            Public blnMFGChanged As Boolean = False

#End Region
#Region "MyState Constructor"
            Public Sub New()
                TheItemCoverageState = New ItemCoverageState
                TheItemsState = New ItemsState
                'TheEndorsementState = New EndorsementsState
                TheDirectDebitState = New DirectDebitState
            End Sub

#End Region

        End Class
#End Region

        Public Sub New()
            MyBase.New(New BaseState)
        End Sub

#Region "Navigation Controller"
        Public Const SESSION_KEY_BACKUP_STATE As String = "SESSION_KEY_BACKUP_STATE"

#End Region

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Dim retState As MyState
                If ((NavController Is Nothing) OrElse (NavController.Context <> "CERT_DETAIL")) Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
                ElseIf NavController.State Is Nothing Then
                    NavController.State = New MyState
                ElseIf ([GetType].BaseType.FullName <>
                        NavController.State.GetType.ReflectedType.FullName) Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(SESSION_KEY_BACKUP_STATE), MyState)
                Else
                    If NavController.IsFlowEnded Then
                        'Restart flow
                        Dim s As MyState = CType(NavController.State, MyState)
                        StartNavControl()
                        NavController.State = s

                    End If
                End If
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                End If
                retState = CType(NavController.State, MyState)
                Session(SESSION_KEY_BACKUP_STATE) = retState
                Return retState
            End Get
        End Property

        Public Class Parameters

            Public CertificateId As Guid = Nothing
            Public IsCallerAuthenticated As Boolean = False

            Public Sub New(certificateId As Guid, Optional IsCallerAuthenticated As Boolean = False)
                Me.CertificateId = certificateId
                Me.IsCallerAuthenticated = IsCallerAuthenticated
            End Sub

        End Class

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                WorkingPanelVisible = True
                If CallingParameters IsNot Nothing Then

                    '   Me.StartNavControl() 'arf 12-20-04

                    'Me.State.workingPanelVisible = True
                    'Get the id from the parent

                    ' DEF-4245 Clean up old data on new search
                    State.CoveragesearchDV = Nothing
                    State.ClaimsearchDV = Nothing

                    Try
                        State.MyBO = New Certificate(CType(CallingParameters, Guid))
                    Catch ex As Exception
                        State.MyBO = New Certificate(CType(CallingParameters, Parameters).CertificateId)
                        state.IsCallerAuthenticated = CType(CallingParameters, Parameters).IsCallerAuthenticated
                    End Try
                    CertId = State.MyBO.Id
                    State.ValFlag = State.MyBO.GetValFlag()
                    State.certificateChanged = False

                    State.PreviousCertificate = Nothing
                    State.OriginalCertificate = Nothing

                Else
                    Throw New Exception("No Calling Parameters")
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub CertificateForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            'set tab display info
            hdnSelectedTab.Value = State.selectedTab
            Dim strTemp As String = String.Empty
            If listDisabledTabs.Count > 0 Then
                For Each i As Integer In listDisabledTabs
                    strTemp = strTemp + "," + i.ToString
                Next
                strTemp = strTemp.Substring(1) 'remove the first comma
            End If
            State.disabledTabs = strTemp 'store the disabled state
            hdnDisabledTabs.Value = State.disabledTabs
            If hdnInitDisabledTabs.Value = "NA" Then 'set once only
                hdnInitDisabledTabs.Value = hdnDisabledTabs.Value
            End If
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State.MyBO IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                              TranslationBase.TranslateLabelOrMessage("Certificate") & " " & State.MyBO.CertNumber
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Certificate_Detail) & " (<strong>" &
                                              State.MyBO.CertNumber & "</strong>) " & TranslationBase.TranslateLabelOrMessage("SUMMARY")
                End If
            End If
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            Try
                ' This is an scenario where Dynamic Claim Recording is called from Certificate form
                If Not IsPostBack Then
                    If (NavController IsNot Nothing) AndAlso (NavController.PrevNavState IsNot Nothing) AndAlso (NavController.PrevNavState.Name = "SEND_SERVICE_ORDER") AndAlso
                        (Not String.IsNullOrEmpty(State.ClaimRecordingXcd)) AndAlso (State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH)) Then
                        If (Navigator.NavStackCount > 1) Then
                            MyBase.SetPageOutOfNavigation()
                        End If
                        Navigator.SetCurrentPage(mobjPage, mobjState)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")

            UpdateBreadCrum()

            Try
                If (NavController IsNot Nothing) AndAlso (NavController.PrevNavState IsNot Nothing) AndAlso (NavController.PrevNavState.Name = "CERT_ITEM") Then
                    Dim ciParamObj As Object = NavController.ParametersPassed
                    Dim ciRetObj As Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemForm.ReturnType = CType(ciParamObj, Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemForm.ReturnType)
                    If ciRetObj.BoChanged Then
                        State.MyBO = New Certificate(State.MyBO.Id)
                    End If
                End If

                If (NavController IsNot Nothing) AndAlso (NavController.PrevNavState IsNot Nothing) AndAlso (NavController.PrevNavState.Name = "CREATE_NEW_ENDORSEMENT") Then
                    Dim enParamObj As Object = NavController.ParametersPassed
                    Dim enRetObj As Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementForm.ReturnType = CType(enParamObj, Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementForm.ReturnType)
                    State.blnMFGChanged = enRetObj.HasMFGCoverageChanged
                    If enRetObj.HasDataChanged Then
                        State.MyBO = New Certificate(State.MyBO.Id)
                    Else
                        State.NavigateToEndorment = False ' No Changes on Endorsement page, so no need to navigate to Endorsement tab
                    End If
                End If

                ' if coming back from Linked Certificate, reset the source certificate.
                If State.MyBO Is Nothing AndAlso Session("SourceCertificateId") IsNot Nothing AndAlso Not CType(Session("SourceCertificateId"), Guid).Equals(Guid.Empty) Then

                    SourceCertificateId = CType(Session("SourceCertificateId"), Guid)
                    State.MyBO = New Certificate(SourceCertificateId)

                    SourceCertificateId = Guid.Empty
                    Session("SourceCertificateId") = SourceCertificateId
                    State.PreviousCertificate = Nothing
                    State.OriginalCertificate = Nothing
                End If

                If State.MyBO IsNot Nothing AndAlso Not ElitaPlusIdentity.Current.ActiveUser.LanguageId.Equals(Guid.Empty) Then
                    HoverMenuExtender1.DynamicContextKey = State.MyBO.Id.ToString & ":" & ElitaPlusIdentity.Current.ActiveUser.LanguageId.ToString
                End If

                State.selectedTab = 0
                '------------------------------------------------------------------------

                Dim selectedCancellationReason As Guid
                Dim selectedPaymentMethod As Guid
                Dim oCancellatioReason As Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason

                'Put user code to initialize the page here

                MasterPage.MessageController.Clear()
                WorkingPanelVisible = State.WorkingPanelVisible

                State.TheItemCoverageState.IsGridVisible = True
                State.IsGridVisible = True

                State.IsCommentsGridVisible = True
                State.IsEndorsementsGridVisible = True
                State.IsCertHistoryGridVisible = True
                State.IsCertExtFieldsGridVisible = True
                ' Me.State.isItemsGridVisible = True

                If State.MyBO IsNot Nothing AndAlso Contract.HasContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value) Then
                    ControlMgr.SetVisibleControl(Me, TNCButton_WRITE, True)
                Else
                    ControlMgr.SetVisibleControl(Me, TNCButton_WRITE, False)
                End If

                'If Not String.IsNullOrEmpty(Me.State.MyBO.Dealer.AcceptPaymentByCheck) AndAlso
                '    Me.State.MyBO.Dealer.AcceptPaymentByCheck = "YESNO-Y" Then
                '    ControlMgr.SetVisibleControl(Me, btnAddCheckPayment, True)
                'Else
                '    ControlMgr.SetVisibleControl(Me, btnAddCheckPayment, False)
                'End If

                If State.MyBO.CustomerId = Guid.Empty Then
                    ControlMgr.SetVisibleControl(Me, btnCustProfileHistory_Write, False)
                End If

                EnableDisableControls(moCertificateDetailPanel, True)
                ' Address Validation
                EnableDisableAddressValidation()

                'KDDI
                If State.AddressFlag = False Then
                    EnableDisableControls(moCertificateDetailPanel, False)
                    'Dim btnValidate_Address1 As Button = AddressCtr.FindControl(ValidateAddressButton)
                    'ControlMgr.SetVisibleControl(Me, btnValidate_Address1, True)
                Else
                    EnableDisableControls(moCertificateDetailPanel, True)
                    'Dim btnValidate_Address1 As Button = AddressCtr.FindControl(ValidateAddressButton)
                    'ControlMgr.SetVisibleControl(Me, btnValidate_Address1, False)
                End If

                ' EnableDisableControls(Me.moTaxIdPanel, True)
                'EnableDisableControls(moGeneral_InformationTabPanel_WRITE, True)
                'moTaxIdText.ReadOnly = False
                'EnableDisableControls(Me.moPremiumInformationTabPanel_WRITE, True)

                moTaxIdText.Attributes.Add("OnChange", "fillTB(this,'" & moNewTaxIdText.ClientID & "')")
                moNewTaxIdText.Attributes.Add("OnChange", "fillTB(this,'" & moTaxIdText.ClientID & "')")
                'Me.tsHoriz.Items(Me.CERT_ITEMS_INFO_TAB).Text = TranslationBase.TranslateLabelOrMessage("ITEMS")  'Added by Pedro


                'START DEF - 1986
                'Dim doctypeFunction As String
                'doctypeFunction = String.Format("DisableTextbox('{0}','{1}','{2}', '{3}', '{4}');", Me.cboDocumentTypeId.ClientID, Me.moIDTypeText.ClientID, Me.moDocumentAgencyText.ClientID, Me.moRGNumberText.ClientID, Me.moDocumentIssueDateText.ClientID)
                'Me.cboDocumentTypeId.Attributes.Add("onChange", "doctypeFunction")
                'END    DEF-1986

                If Not IsPostBack Then
                    If Not State.MyBO.CountryPurchaseId.Equals(Guid.Empty) Then
                        Dim countryBO As Country = New Country(State.MyBO.CountryPurchaseId)
                        State.CountryOfPurchase = countryBO.Description
                        State.AllowForget = countryBO.AllowForget
                    End If

                    State.directDebitPayment = False
                    State.creditCardPayment = False

                    ObtainBooleanFlags()

                    PopulateFormFromBOs()

                    TranslateGridHeader(CertOtherCustomers)
                    PopulateCustomerGrid()

                    PopulatePremiumInfoTab()

                    If State.TheItemCoverageState.IsGridVisible Then
                        PopulateCoveragesGrid()
                    End If

                    If State.IsGridVisible Then
                        PopulateClaimsGrid()
                    End If

                    If State.IsCommentsGridVisible Then
                        PopulateCommentsGrid()
                    End If

                    If State.isItemsGridVisible Then
                        TranslateGridHeader(ItemsGrid)
                        PopulateItemsGrid()
                    End If

                    If State.isRegItemsGridVisible Then
                        TranslateGridHeader(RegisteredItemsGrid)
                        PopulateRegisterItemsGrid()
                    End If

                    If State.IsEndorsementsGridVisible Then
                        PopulateEndorsementsGrid()
                    End If

                    If State.IsCertExtFieldsGridVisible Then
                        PopulateCertExtendedFieldsGrid()
                    End If

                    'Certificate History tab
                    'If Me.State.IsCertHistoryGridVisible Then
                    '    Me.TranslateGridHeader(Me.CertHistoryGrid)
                    '    Me.PopulateCertificateHistoryTabInfo()
                    'End If

                    PopulateInstallmentHistoryGrid()
                    PopulateExtensionsGrid()

                    EnableDisablePremiunBankInfoOption()

                    If State.MyBO.StatusCode = CLOSED Then
                        'If the Cert was cancelled due to a replacement Policy, inform the user with a message
                        If (State.MyBO.TheCertCancellationBO IsNot Nothing) Then
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_CANCELLATION_REASONS, State.MyBO.TheCertCancellationBO.CancellationReasonId) = "REP" Then
                                MasterPage.MessageController.AddInformation(Message.MSG_REINSTATEMENT_NOT_ALLOWED_FOR_REPLACEMENT_POLICY, True)
                            End If
                        End If
                        PopulateCancellationInfoTab()
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                    Else
                        If allCoveragesExpired Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        End If
                    End If

                    Dim attvalue As AttributeValue = State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CANCEL_RULES_FOR_SFR).FirstOrDefault
                    If attvalue Is Nothing Then
                        State.CancelRulesForSFR = Codes.YESNO_N
                    Else
                        State.CancelRulesForSFR = attvalue.Value
                    End If

                    Dim attValueComputeCancellation As AttributeValue = State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_COMPUTE_CANCELLATION_DATE_AS_EOFMONTH).FirstOrDefault
                    If attValueComputeCancellation Is Nothing Then
                        State.ComputeCancellationDateEOfM = Codes.YESNO_N
                    Else
                        State.ComputeCancellationDateEOfM = attValueComputeCancellation.Value
                    End If

                    'PopulateCancelRequestReasonDropdown(Me.moCancelRequestReasonDrop)
                    State.CertCancelRequestId = State.MyBO.getCertCancelRequestID
                    If State.MyBO.getCancelationRequestFlag = YES Then
                        If Not State.CertCancelRequestId.Equals(Guid.Empty) Then
                            State.certCancelRequestBO = New CertCancelRequest(State.CertCancelRequestId)
                            If State.CancelRulesForSFR = Codes.YESNO_Y AndAlso State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Or State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            End If
                        Else
                            State.certCancelRequestBO = New CertCancelRequest
                        End If
                        populateCancelRequestInfoTab()
                    ElseIf Not State.CertCancelRequestId.Equals(Guid.Empty) Then
                        State.certCancelRequestBO = New CertCancelRequest(State.CertCancelRequestId)
                        ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                        populateCancelRequestInfoTab()
                    End If

                    'Populate coverage history grid 
                    If ExpiredCoveragesExist Then
                        'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_COVERAGE_HISTORY_TAB), True)
                        EnableTab(CERT_COVERAGE_HISTORY_TAB, True)
                        PopulateCoveragesHistoryGrid()
                    End If

                    If Not State.MyBO.Product.UpgFinanceInfoRequireId.Equals(Guid.Empty) Then
                        populateFinanceTab()
                    End If

                    'If ((tsHoriz.SelectedIndex < 0) AndAlso (tsHoriz.Items(0).Enabled = True)) Then
                    '    Me.tsHoriz.SelectedIndex = 0
                    'End If
                    EnableDisableFields()
                    AddCalendar(BtnProductSalesDate, moProductSalesDateText)
                    AddCalendar(BtnDocumentIssueDate, moDocumentIssueDateText)
                    AddCalendar(BtnWarrantySoldDate, moWarrantySoldText)
                    AddCalendar(BtnDateOfBirth, moDateOfBirthText)
                    AddCalendar(moCancelDateImageButton, moCancelDateTextBox)
                    AddCalendar(moCancelRequestDateImagebutton, moCancelRequestDateTextBox)
                    AddCalendar(BtnCertificateVerificationDate, moCertificateVerificationDateText)
                    AddCalendar(BtnSEPAMandateDate, moSEPAMandateDateText)
                    AddCalendar(BtnCheckVerificationDate, moCheckVerificationDateText)
                    AddCalendar(BtnContractCheckCompleteDate, moContractCheckCompleteDateText)
                    AddCalendar(BtnServiceStartDate, moServiceStartDateText)
                Else 'page posted back
                    State.selectedTab = hdnSelectedTab.Value 'store the selected tab
                    'populate the disabled tab list
                    If (State IsNot Nothing) Then
                        If State.disabledTabs.Trim = String.Empty Then
                            listDisabledTabs.Clear()
                        Else
                            For Each str As String In State.disabledTabs.Split(",")
                                Dim intTemp As Integer
                                If str.Trim <> String.Empty AndAlso Integer.TryParse(str.Trim, intTemp) Then
                                    listDisabledTabs.Add(intTemp)
                                End If
                            Next
                        End If
                    End If
                End If

                BindBoPropertiesToLabels()
                BindCancelRequestBoPropertiesToLabels()
                BindCancellationBoPropertiesToLabels()
                CheckIfComingFromTransferOfOwnershipConfirm()
                CheckIfComingFromSaveConfirm()

                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If

                'Me.moCancellationReasonDrop.AutoPostBack = True
                PaymentMethodDrop.AutoPostBack = True

                '*ANI  Me.PaymentReasonDrop.AutoPostBack = True
                selectedCancellationReason = GetSelectedItem(moCancellationReasonDrop)
                selectedPaymentMethod = GetSelectedItem(PaymentMethodDrop)

                If Not (selectedCancellationReason.Equals(Guid.Empty)) Then
                    oCancellatioReason = New Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason(selectedCancellationReason)

                    Dim oYesList As DataView = LookupListNew.GetListItemId(oCancellatioReason.InputAmtReqId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
                    Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
                End If
                '*** needed ? Me.PaymentMethodDrop.Visible = False
                CheckIfComingFromComments()
                CheckIfComingFromEndorse()
                CheckIfComingFromItems()
                CheckIfComingFromBillingHistory()
                CheckIfComingFromPaymentHistory()
                CheckIfComingFromBillPayHistory()
                CheckIfComingFromRemoveCancelDueDateConfirm()

                CheckIfCoverageGridRefreshNeeded()

                'DEF-2486
                If Not State.IsClaimAllowed Then
                    ControlMgr.SetVisibleControl(Me, btnNewClaim, False)
                    'ElseIf Not String.IsNullOrEmpty(Me.State.ClaimRecordingXcd) AndAlso Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                    '    'REQ-6155
                    '    ControlMgr.SetVisibleControl(Me, btnNewClaim, False)
                Else
                    If (State.AllowOldAndNewDCMClaims) Then
                        btnNewClaim.Text = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM_OLD")
                    Else
                        btnNewClaim.Text = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM")
                    End If
                    ControlMgr.SetVisibleControl(Me, btnNewClaimDcm, State.AllowOldAndNewDCMClaims)
                End If
                'DEF-2486

                Dim dealerBO As Dealer = New Dealer(State.MyBO.DealerId)
                If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If dealerBO.AttributeValues.Contains(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) Then

                        'If dealerBO.AttributeValues.Value(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) = Codes.YESNO_Y Then
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeLastNameLabel, True)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeLastNameText, True)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameLabel, True)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameText, True)
                        'Else
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeLastNameLabel, False)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeLastNameText, False)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameLabel, False)
                        '    ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameText, False)
                        'End If
                        If Not (dealerBO.AttributeValues.Value(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) = Codes.YESNO_Y) Then
                            moAltCustLasName.Attributes("style") = "display: none"
                            moAltCustFirstName.Attributes("style") = "display: none"
                        End If
                    Else
                        moAltCustLasName.Attributes("style") = "display: none"
                        moAltCustFirstName.Attributes("style") = "display: none"
                    End If
                End If

                'Data Protection

                If Not Page.IsPostBack Then
                    TranslateGridHeader(GridDataProtection)

                    If (State.MyBO.CertificateIsRestricted) Then
                        ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                        ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                        ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                        ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                        ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, True)
                    End If

                    tabs_Data_Protection_WRITE.Visible = False
                    ControlMgr.SetVisibleControl(Me, tabs_Data_Protection_WRITE, True)

                    Dim objPrincipal As ElitaPlusIdentity
                    objPrincipal = CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal).Identity

                    'Trace GDPR-OKTA issue
                    Try
                        Dim logEntry As String = " Certificate_Screen; UserID=" & objPrincipal.ActiveUser.NetworkId & "; PrivacyUserType=" & objPrincipal.PrivacyUserType & "; Time=" & Now.ToString
                        AppConfig.DebugLog(logEntry)
                    Catch ex As Exception
                        Dim logEntry As String = "UseNew Exception(logEntry)rID=" & objPrincipal.ActiveUser.NetworkId & "; PrivacyUserType=" & objPrincipal.PrivacyUserType & "; Time=" & Now.ToString
                        AppConfig.DebugLog(logEntry)

                    End Try


                    If objPrincipal.PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        EnableTab(CERT_DATA_PROTECTION_TAB, True)

                        PopulateDataProtection()
                    Else
                        EnableTab(CERT_DATA_PROTECTION_TAB, False)
                        ControlMgr.SetVisibleControl(Me, Label22, False)

                    End If


                    If State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Not State.MyBO.CertificateIsRestricted Then
                        If State.AllowForget IsNot Nothing AndAlso String.Compare(State.AllowForget, Codes.EXT_YESNO_Y, True) = 0 Then
                            btnRightToForgotten.Visible = True
                        End If
                    End If

                End If

                btnNewClaim.Visible = Not State.MyBO.CertificateIsRestricted


                ' Save Right To Forgotten after confirmation
                SaveRightToForgotten()

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

            '   Me.State.TheItemCoverageState.hi()
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                WorkingPanelVisible = True
                If CalledUrl = ClaimForm.URL And ClaimForm.Save_Ok = True Then
                    ClaimForm.Save_Ok = False
                    State.ClaimsearchDV = Nothing

                End If

                If ReturnFromUrl.Contains(ClaimRecordingForm.Url2) Then
                    If TypeOf ReturnPar Is ClaimRecordingForm.ReturnType Then
                        Dim retObjCRF As ClaimRecordingForm.ReturnType = CType(ReturnPar, ClaimRecordingForm.ReturnType)
                        Select Case retObjCRF.LastOperation
                            Case ElitaPlusPage.DetailPageCommand.Cancel
                                If retObjCRF IsNot Nothing Then
                                    State.MyBO = New Certificate(retObjCRF.CertificateId)
                                    State.IsCallerAuthenticated = retObjCRF.IsCallerAuthenticated
                                End If
                        End Select
                    ElseIf TypeOf ReturnPar Is ClaimForm.ReturnType Then
                        Dim retObjCF As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                        Select Case retObjCF.LastOperation
                            Case ElitaPlusPage.DetailPageCommand.Back
                                If retObjCF IsNot Nothing Then
                                    State.MyBO = New Certificate(retObjCF.EditingBo.CertificateId)
                                    State.IsCallerAuthenticated = retObjCF.IsCallerAuthenticated
                                End If
                        End Select
                    End If
                Else
                    Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            If retObj IsNot Nothing Then
                                State.MyBO = New Certificate(retObj.EditingBo.CertificateId)
                                State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                            End If
                    End Select
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CheckIfComingFromComments()
            If State.NavigateToComment Then
                If isTabEnabled(CERT_COMMENTS_TAB) = True Then State.selectedTab = CERT_COMMENTS_TAB
                State.NavigateToComment = False
            End If
        End Sub

        Private Sub CheckIfComingFromCheckPayment()
            If State.NavigateToCheckPayment Then
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then State.selectedTab = CERT_PREMIUM_INFO_TAB
                State.NavigateToCheckPayment = False
            End If
        End Sub

        Private Sub CheckIfComingFromEndorse()
            If State.NavigateToEndorment Then
                'If tsHoriz.Items(CERT_ENDORSEMENTS_TAB).Enabled = True Then Me.tsHoriz.SelectedIndex = CERT_ENDORSEMENTS_TAB
                If isTabEnabled(CERT_ENDORSEMENTS_TAB) = True Then State.selectedTab = CERT_ENDORSEMENTS_TAB
                State.NavigateToEndorment = False
                State.MyBO = New Certificate(State.MyBO.Id)
                PopulateFormFromBOs()
            End If

        End Sub

        Private Sub CheckIfComingFromBillingHistory()
            If State.NavigateToBillingHistory Then
                'If tsHoriz.Items(CERT_PREMIUM_INFO_TAB).Enabled = True Then Me.tsHoriz.SelectedIndex = CERT_PREMIUM_INFO_TAB
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then State.selectedTab = CERT_PREMIUM_INFO_TAB
                State.NavigateToBillingHistory = False
            End If

        End Sub

        Private Sub CheckIfComingFromPaymentHistory()
            If State.NavigateToPaymentHistory Then
                'If tsHoriz.Items(CERT_PREMIUM_INFO_TAB).Enabled = True Then Me.tsHoriz.SelectedIndex = CERT_PREMIUM_INFO_TAB
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then State.selectedTab = CERT_PREMIUM_INFO_TAB
                State.NavigateToPaymentHistory = False
            End If

        End Sub

        Private Sub CheckIfComingFromBillPayHistory()
            If State.NavigateToBillpayHistory Then
                'If tsHoriz.Items(CERT_PREMIUM_INFO_TAB).Enabled = True Then
                '    Me.tsHoriz.SelectedIndex = CERT_PREMIUM_INFO_TAB
                '    Me.State.NavigateToBillpayHistory = False
                'End If
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then State.selectedTab = CERT_PREMIUM_INFO_TAB
                State.NavigateToBillpayHistory = False
            End If
        End Sub

        Private Sub CheckIfCoverageGridRefreshNeeded()
            If (State.TheItemCoverageState IsNot Nothing) AndAlso State.isCovgGridRefreshNeeded AndAlso State.TheItemCoverageState.IsGridVisible Then
                State.CoveragesearchDV = Nothing
                PopulateCoveragesGrid()
                State.isCovgGridRefreshNeeded = False
            End If
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub cboDocumentTypeId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDocumentTypeId.SelectedIndexChanged
            EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
        End Sub
        Protected Sub EnableDisableFields()

            btnBack.Enabled = True
            If State.MyBO.getCancelationRequestFlag = YES Then
                ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
            Else
                If State.MyBO.IsChildCertificate Then
                    ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
                Else
                    ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, True)
                End If
            End If

            If Not (State.MyBO.PaymentTypeId.Equals(Guid.Empty)) Then
                If (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) _
                   Or (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) _
                   Or (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD) _
                   Or (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY) _
                   Or (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__PARTIAL_PAYMENT And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) Then
                    State.creditCardPayment = True
                ElseIf (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) _
                       Or (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__PARTIAL_PAYMENT And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) Then
                    State.directDebitPayment = True
                End If
            End If


            If Not State.IsEdit Then

                EnableDisableTabs(State.IsEdit)

                ControlMgr.SetEnableControl(Me, btnEditCertDetail_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnEditCertInfo_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnUndoCertDetail_Write, False)
                ControlMgr.SetEnableControl(Me, btnUndoCertInfo_Write, False)
                ControlMgr.SetEnableControl(Me, btnSaveCertDetail_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnSaveCertInfo_WRITE, False)
                AddressCtr.EnableControls(True)
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, False)
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, False)
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                ControlMgr.SetVisibleControl(Me, moSalutationText, False)
                ControlMgr.SetVisibleControl(Me, moSalutationLabel, False)
                ControlMgr.SetVisibleControl(Me, cboSalutationId, False)
                ControlMgr.SetVisibleControl(Me, moPostPaidLabel, False)
                ControlMgr.SetVisibleControl(Me, moPostPaidText, False)
                ControlMgr.SetVisibleControl(Me, moLangPrefText, False)
                ControlMgr.SetVisibleControl(Me, cboLangPref, False)

                ControlMgr.SetVisibleControl(Me, moCancelRequestDateImagebutton, False)
                ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
                ControlMgr.SetVisibleControl(Me, moCallerNameTextBox, False)
                ControlMgr.SetVisibleControl(Me, moCallerNameLabel, False)
                ControlMgr.SetVisibleControl(Me, moCommentsTextbox, False)
                ControlMgr.SetVisibleControl(Me, moCommentsLabel, False)

                moCancelDateTextBox.ReadOnly = True
                moCancelDateTextBox.CssClass = "FLATTEXTBOX"
                moCancelRequestDateTextBox.ReadOnly = True
                moCancelRequestDateTextBox.CssClass = "FLATTEXTBOX"
                ControlMgr.SetEnableControl(Me, moCancelRequestReasonDrop, False)
                moCancelRequestReasonDrop.CssClass = "FLATTEXTBOX"

                ControlMgr.SetVisibleControl(Me, moCanReqJustificationLabel, False)
                ControlMgr.SetVisibleControl(Me, moCancelRequestJustificationDrop, False)
                moCancelRequestJustificationDrop.CssClass = "FLATTEXTBOX"

                ControlMgr.SetVisibleControl(Me, BtnDocumentIssueDate, False)
                ControlMgr.SetEnableControl(Me, btnSaveTaxID_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndoTaxID_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnEditTaxID_WRITE, True)
                ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, False)
                ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndoDebit_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnDebitEdit_WRITE, True)

                ControlMgr.SetEnableControl(Me, btnCancelRequestSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCancelRequestUndo_WRITE, False)
                ControlMgr.SetEnableControl(Me, moFulfillmentConsentActionDrop, False)

                If (Not State.MyBO.StatusCode = CERT_CANCEL_STATUS) And State.MyBO.getCancelationRequestFlag = YES Then
                    If State.certCancelRequestBO Is Nothing Then
                        State.CertCancelRequestId = State.MyBO.getCertCancelRequestID
                        If Not State.CertCancelRequestId.Equals(Guid.Empty) Then
                            State.certCancelRequestBO = New CertCancelRequest(State.CertCancelRequestId)
                            If State.CancelRulesForSFR = Codes.YESNO_Y AndAlso State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Or State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            Else
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                            End If
                            If State.CancelRulesForSFR = Codes.YESNO_Y AndAlso State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED And State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE Then
                                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, True)
                            Else
                                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                        End If
                    ElseIf State.certCancelRequestBO Is Nothing And Not State.MyBO.getCertCancelRequestID.Equals(guid.Empty) Then
                        State.CertCancelRequestId = State.MyBO.getCertCancelRequestID
                        State.certCancelRequestBO = New CertCancelRequest(State.CertCancelRequestId)
                        ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                    Else
                        If State.CancelRulesForSFR = Codes.YESNO_Y Then
                            If State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                            ElseIf State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                        End If
                        If State.CancelRulesForSFR = Codes.YESNO_Y AndAlso State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED And State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE Then
                            ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, True)
                        Else
                            ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                        End If
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, False)
                End If

                ControlMgr.SetVisibleControl(Me, cboAccountType, True)
                ControlMgr.SetVisibleControl(Me, moAccountNumberText, True)
                If State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
                    ControlMgr.SetVisibleControl(Me, moCertificateSignedLabel, True)
                    ControlMgr.SetVisibleControl(Me, moSEPAMandateSignedLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCheckSignedLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCheckVerificationDateLabel, True)
                    ControlMgr.SetVisibleControl(Me, moContractCheckCompleteDateLabel, True)
                    ControlMgr.SetVisibleControl(Me, moContractCheckCompleteLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCertificateSigneddrop, True)
                    ControlMgr.SetVisibleControl(Me, moSEPAMandateSignedDrop, True)
                    ControlMgr.SetVisibleControl(Me, moCheckSignedDrop, True)
                    ControlMgr.SetVisibleControl(Me, moCheckVerificationDateText, True)
                    ControlMgr.SetVisibleControl(Me, moContractCheckCompleteDateText, True)
                    ControlMgr.SetVisibleControl(Me, moContractCheckCompleteDrop, True)
                    ControlMgr.SetVisibleControl(Me, moCertificateVerificationDateLabel, True)
                    ControlMgr.SetVisibleControl(Me, moSEPAMandateDateLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCertificateVerificationDateText, True)
                    ControlMgr.SetVisibleControl(Me, moSEPAMandateDateText, True)
                End If


                If Not (State.IsNewBillPayBtnVisible) And State.oPaymentCount > 0 Then
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                Else
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                End If

                If State.oBillingCount > 0 Then
                    Dim dvBillingTotals As BillingDetail.BillingTotals = BillingDetail.getBillingTotals(State.MyBO.Id)
                    If dvBillingTotals IsNot Nothing And dvBillingTotals.Count > 0 And dvBillingTotals.Item(0).Row(0) > 0 Then
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                    End If
                Else
                    ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                End If
                'Bug-216874: Added condition to check bill pay total.
                Dim dvBillPay As DataView
                dvBillPay = BillingPayDetail.getBillPayTotals(State.MyBO.Id)
                Dim cnt = 0
                If dvBillPay IsNot Nothing And dvBillPay.Count > 0 And dvBillPay.Table.Rows.Count > 0 Then
                    cnt = CType(dvBillPay.Table.Rows(0).Item(0), Integer)
                End If

                If (State.oBillPayCount > 0 Or cnt > 0) And State.IsNewBillPayBtnVisible Then
                    ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                    ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                ElseIf (State.oPayColltCount > 0) Then
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                    If (State.IsNewBillPayBtnVisible) Then
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                    End If
                Else
                    If (State.IsNewBillPayBtnVisible) Then
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                    End If
                End If

                'If (Me.State.oPayColltCount > 0) Then
                '    ControlMgr.SetEnableControl(Me, btnPaymentHistory, True
                '                                )
                'End If

                ControlMgr.SetEnableControl(Me, moBillingStatusId, False)
                ControlMgr.SetEnableControl(Me, moPaymentTypeId, False)
                If Not State.MyBO.StatusCode = CERT_STATUS Then
                    ControlMgr.SetEnableControl(Me, btnDebitEdit_WRITE, False)
                End If

                If State.isSalutation Then
                    ControlMgr.SetVisibleControl(Me, moSalutationText, True)
                    ControlMgr.SetVisibleControl(Me, moSalutationLabel, True)
                End If

                If State.isPostPrePaid Then
                    ControlMgr.SetVisibleControl(Me, moPostPaidLabel, True)
                    ControlMgr.SetVisibleControl(Me, moPostPaidText, True)
                End If
                ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, False)
                ControlMgr.SetVisibleControl(Me, moDocumentTypeText, True)
                ControlMgr.SetVisibleControl(Me, cboLangPref, False)
                ControlMgr.SetVisibleControl(Me, moLangPrefText, True)

                If State.isItemsGridVisible Then
                    PopulateItemsGrid()
                End If

                If State.isRegItemsGridVisible Then
                    PopulateRegisterItemsGrid()
                End If

                If State.MyBO.IsChildCertificate Then
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, False)
                ElseIf State.MyBO.getCancelationRequestFlag = YES Or Not State.CertCancelRequestId.Equals(Guid.Empty) Then
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, True)
                Else
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, False)
                End If
                'AA REQ-910 new fields added BEGIN
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3=Display and Require at enrollment
                    ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)
                    ControlMgr.SetVisibleControl(Me, cboPoliticallyExposedId, False)
                    ControlMgr.SetVisibleControl(Me, moIncomeRangeText, True)
                    ControlMgr.SetVisibleControl(Me, moPoliticallyExposedText, True)
                    ControlMgr.SetEnableControl(Me, moOccupationText, True)
                    ControlMgr.SetEnableControl(Me, moIncomeRangeText, True)
                    ControlMgr.SetEnableControl(Me, moPoliticallyExposedText, True)
                End If
                'AA REQ-910 new fields added END

                'REQ-1255 - AML Regulations - START
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment

                    'Marital Status
                    ControlMgr.SetVisibleControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetEnableControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetVisibleControl(Me, moMaritalStatusText, True)
                    ControlMgr.SetEnableControl(Me, moMaritalStatusText, True)
                    'Nationality
                    ControlMgr.SetVisibleControl(Me, ddlNationality, False)
                    ControlMgr.SetEnableControl(Me, ddlNationality, False)
                    ControlMgr.SetVisibleControl(Me, moNationalityText, True)
                    ControlMgr.SetEnableControl(Me, moNationalityText, True)
                    'Place Of Birth
                    ControlMgr.SetVisibleControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetEnableControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetVisibleControl(Me, moPlaceOfBirthText, True)
                    ControlMgr.SetEnableControl(Me, moPlaceOfBirthText, True)
                    'City of Birth
                    ControlMgr.SetVisibleControl(Me, moCityOfBirthText, True)
                    ControlMgr.SetEnableControl(Me, moCityOfBirthText, True)
                    'Gender
                    ControlMgr.SetVisibleControl(Me, ddlGender, False)
                    ControlMgr.SetEnableControl(Me, ddlGender, False)
                    ControlMgr.SetVisibleControl(Me, moGenderText, True)
                    ControlMgr.SetEnableControl(Me, moGenderText, True)
                    'CUIT_CUIL
                    ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, True)
                    ControlMgr.SetEnableControl(Me, moCUIT_CUILText, True)
                    'Person Type
                    ControlMgr.SetVisibleControl(Me, ddlPersonType, False)
                    ControlMgr.SetEnableControl(Me, ddlPersonType, False)
                    ControlMgr.SetVisibleControl(Me, moPersonTypeText, True)
                    ControlMgr.SetEnableControl(Me, moPersonTypeText, True)
                Else
                    'Marital Status
                    ControlMgr.SetVisibleControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetEnableControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetVisibleControl(Me, moMaritalStatusText, False)
                    ControlMgr.SetEnableControl(Me, moMaritalStatusText, False)
                    'Nationality
                    ControlMgr.SetVisibleControl(Me, ddlNationality, False)
                    ControlMgr.SetEnableControl(Me, ddlNationality, False)
                    ControlMgr.SetVisibleControl(Me, moNationalityText, False)
                    ControlMgr.SetEnableControl(Me, moNationalityText, False)
                    'Place Of Birth
                    ControlMgr.SetVisibleControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetEnableControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetVisibleControl(Me, moPlaceOfBirthText, False)
                    ControlMgr.SetEnableControl(Me, moPlaceOfBirthText, False)
                    'City of Birth
                    ControlMgr.SetVisibleControl(Me, moCityOfBirthText, False)
                    ControlMgr.SetEnableControl(Me, moCityOfBirthText, False)
                    'Gender
                    ControlMgr.SetVisibleControl(Me, ddlGender, False)
                    ControlMgr.SetEnableControl(Me, ddlGender, False)
                    ControlMgr.SetVisibleControl(Me, moGenderText, False)
                    ControlMgr.SetEnableControl(Me, moGenderText, False)
                    'CUIT_CUIL
                    ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)
                    ControlMgr.SetEnableControl(Me, moCUIT_CUILText, False)
                    'Person Type
                    ControlMgr.SetVisibleControl(Me, ddlPersonType, False)
                    ControlMgr.SetEnableControl(Me, ddlPersonType, False)
                    ControlMgr.SetVisibleControl(Me, moPersonTypeText, False)
                    ControlMgr.SetEnableControl(Me, moPersonTypeText, False)
                End If
                'REQ-1255 - END

                EnableDisableControls(moCertificateDetailPanel, True)
                EnableDisableControls(moItemsTabPanel_WRITE, True)
                'Calling the method EnableDisableControls to disable the Premium Tab contents.

                EnableDisableControls(moPremiumInformationTabPanel_WRITE, True)
                ' Start - REQ:5932
                If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If (State.MyBO.CustomerFirstName Is Nothing) And (State.MyBO.CustomerLastName Is Nothing) Then
                        moCustName1.Attributes("style") = "display: none"
                        moCustName2.Attributes("style") = "display: none"
                        moAltCustLasName.Attributes("style") = "display: none"
                        moAltCustFirstName.Attributes("style") = "display: none"
                        AdditionalCustomer.Attributes("style") = "display: none"
                    Else
                        moCustName01.Attributes("style") = "display: none"
                        moCustName02.Attributes("style") = "display: none"
                        'Me.moCustLegalInfo1.Attributes("style") = "display: none"
                        moAMLRegulations3.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, moIncomeRangeLabel, False)
                        ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                        ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILLabel, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)
                    End If
                Else
                    moCustName1.Attributes("style") = "display: none"
                    moCustName2.Attributes("style") = "display: none"
                    moAltCustLasName.Attributes("style") = "display: none"
                    moAltCustFirstName.Attributes("style") = "display: none"
                    AdditionalCustomer.Attributes("style") = "display: none"
                End If
                ' End - REQ:5932

                ControlMgr.SetVisibleControl(Me, BtnServiceStartDate, False)

            Else 'Edit Mode
                '   Me.MenuEnabled = False
                ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
                Select Case State.selectedTab 'Me.tsHoriz.SelectedIndex
                    Case CERT_DETAIL_TAB
                        If State.isSalutation Then
                            ControlMgr.SetVisibleControl(Me, cboSalutationId, True)
                            ControlMgr.SetEnableControl(Me, cboSalutationId, True)
                            ControlMgr.SetVisibleControl(Me, moSalutationText, False)
                        End If
                        If State.isPostPrePaid Then
                            ControlMgr.SetVisibleControl(Me, moPostPaidLabel, True)
                            ControlMgr.SetVisibleControl(Me, moPostPaidText, True)
                        End If
                        EnableDisableTabs(State.IsEdit)
                        setCertDetailTab()
                    Case CERT_INFORMATION_TAB
                        EnableDisableTabs(State.IsEdit)
                        setCertInfoTab()
                    Case CERT_TAX_ID_TAB
                        EnableDisableTabs(State.IsEdit)
                        setTaxIdTab()
                        'START  DEF-1986
                        EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
                        'END    DEF-1986
                    Case CERT_ITEMS_INFO_TAB
                        EnableDisableTabs(State.IsEdit)
                    Case CERT_CANCEL_REQUEST_INFO_TAB
                        If State.MyBO.getCancelationRequestFlag = YES Then
                            EnableDisableTabs(State.IsEdit)
                        ElseIf Not State.CertCancelRequestId.Equals(Guid.Empty) Then
                            EnableDisableTabs(True)
                        Else
                            EnableDisableTabs(State.IsEdit)
                        End If
                        setCancelRequestTab()
                    Case CERT_PREMIUM_INFO_TAB
                        EnableDisableTabs(State.IsEdit)
                        Dim oBillingCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moBillingStatusId))
                        If oBillingCode <> "A" Then
                            ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, True)
                            CheckBoxSendLetter.CssClass = ""
                        End If
                        ControlMgr.SetEnableControl(Me, btnUndoDebit_WRITE, True)
                        ControlMgr.SetEnableControl(Me, btnDebitEdit_WRITE, False)
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                        If (State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                        End If
                        ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, True)
                        ControlMgr.SetEnableControl(Me, moBillingStatusId, True)
                        ControlMgr.SetEnableControl(Me, moPaymentTypeId, False)
                        If State.directDebitPayment Then
                            ControlMgr.SetEnableControl(Me, moBankAccountOwnerText, True)
                            ControlMgr.SetEnableControl(Me, moBankRoutingNumberText, True)
                            ControlMgr.SetEnableControl(Me, moBankAccountNumberText, True)
                            moBillingStatusId.CssClass = ""
                            'Me.moBankAccountOwnerText.CssClass = ""
                            'Me.moBankAccountNumberText.CssClass = ""
                            'Me.moBankRoutingNumberText.CssClass = ""
                            moBankAccountOwnerText.ReadOnly = False
                            moBankRoutingNumberText.ReadOnly = False
                            moBankAccountNumberText.ReadOnly = False
                        ElseIf State.creditCardPayment Then
                            ControlMgr.SetEnableControl(Me, moCreditCardNumberText, True)
                            ControlMgr.SetEnableControl(Me, moCreditCardTypeIDDropDown, True)
                            ControlMgr.SetEnableControl(Me, moExpirationDateText, True)
                            ControlMgr.SetEnableControl(Me, moNameOnCreditCardText, True)
                            'Me.moCreditCardNumberText.CssClass = ""
                            'Me.moCreditCardTypeIDDropDown.CssClass = ""
                            'Me.moExpirationDateText.CssClass = ""
                            'Me.moNameOnCreditCardText.CssClass = ""
                            moCreditCardNumberText.ReadOnly = False
                            moNameOnCreditCardText.ReadOnly = False
                            moExpirationDateText.ReadOnly = False
                        End If
                End Select
                'AA REQ-910 new fields added BEGIN
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only  or 3= Display and Require At Enrollment
                    ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, True)
                    ControlMgr.SetVisibleControl(Me, cboPoliticallyExposedId, True)
                    ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                    ControlMgr.SetVisibleControl(Me, moPoliticallyExposedText, False)

                    ControlMgr.SetEnableControl(Me, cboIncomeRangeId, True)
                    ControlMgr.SetEnableControl(Me, moOccupationText, True)
                    ControlMgr.SetEnableControl(Me, cboPoliticallyExposedId, True)
                End If
                'AA REQ-910 new fields added END
                'REQ-1255 - AML Regulations - START
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only  or 3= Display and Require At Enrollment
                    'Marital Status
                    ControlMgr.SetVisibleControl(Me, ddlMaritalStatus, True)
                    ControlMgr.SetEnableControl(Me, ddlMaritalStatus, True)
                    ControlMgr.SetVisibleControl(Me, moMaritalStatusText, False)
                    ControlMgr.SetEnableControl(Me, moMaritalStatusText, False)
                    'Nationality
                    ControlMgr.SetVisibleControl(Me, ddlNationality, True)
                    ControlMgr.SetEnableControl(Me, ddlNationality, True)
                    ControlMgr.SetVisibleControl(Me, moNationalityText, False)
                    ControlMgr.SetEnableControl(Me, moNationalityText, False)
                    'Place Of Birth
                    ControlMgr.SetVisibleControl(Me, ddlPlaceOfBirth, True)
                    ControlMgr.SetEnableControl(Me, ddlPlaceOfBirth, True)
                    ControlMgr.SetVisibleControl(Me, moPlaceOfBirthText, False)
                    ControlMgr.SetEnableControl(Me, moPlaceOfBirthText, False)
                    'City of Birth
                    ControlMgr.SetVisibleControl(Me, moCityOfBirthText, True)
                    ControlMgr.SetEnableControl(Me, moCityOfBirthText, True)
                    'Gender
                    ControlMgr.SetVisibleControl(Me, ddlGender, True)
                    ControlMgr.SetEnableControl(Me, ddlGender, True)
                    ControlMgr.SetVisibleControl(Me, moGenderText, False)
                    ControlMgr.SetEnableControl(Me, moGenderText, False)
                    'CUIT_CUIL
                    ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, True)
                    ControlMgr.SetEnableControl(Me, moCUIT_CUILText, True)
                    'PersonType
                    ControlMgr.SetVisibleControl(Me, ddlPersonType, True)
                    ControlMgr.SetEnableControl(Me, ddlPersonType, True)
                    ControlMgr.SetVisibleControl(Me, moPersonTypeText, False)
                    ControlMgr.SetEnableControl(Me, moPersonTypeText, False)
                Else
                    'Marital Status
                    ControlMgr.SetVisibleControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetEnableControl(Me, ddlMaritalStatus, False)
                    ControlMgr.SetVisibleControl(Me, moMaritalStatusText, False)
                    ControlMgr.SetEnableControl(Me, moMaritalStatusText, False)
                    'Nationality
                    ControlMgr.SetVisibleControl(Me, ddlNationality, False)
                    ControlMgr.SetEnableControl(Me, ddlNationality, False)
                    ControlMgr.SetVisibleControl(Me, moNationalityText, False)
                    ControlMgr.SetEnableControl(Me, moNationalityText, False)
                    'Place Of Birth
                    ControlMgr.SetVisibleControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetEnableControl(Me, ddlPlaceOfBirth, False)
                    ControlMgr.SetVisibleControl(Me, moPlaceOfBirthText, False)
                    ControlMgr.SetEnableControl(Me, moPlaceOfBirthText, False)
                    'City of Birth
                    ControlMgr.SetVisibleControl(Me, moCityOfBirthText, False)
                    ControlMgr.SetEnableControl(Me, moCityOfBirthText, False)
                    'Gender
                    ControlMgr.SetVisibleControl(Me, ddlGender, False)
                    ControlMgr.SetEnableControl(Me, ddlGender, False)
                    ControlMgr.SetVisibleControl(Me, moGenderText, False)
                    ControlMgr.SetEnableControl(Me, moGenderText, False)
                    'CUIT_CUIL
                    ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)
                    ControlMgr.SetEnableControl(Me, moCUIT_CUILText, False)
                    'PersonType
                    ControlMgr.SetVisibleControl(Me, ddlPersonType, False)
                    ControlMgr.SetEnableControl(Me, ddlPersonType, False)
                    ControlMgr.SetVisibleControl(Me, moPersonTypeText, False)
                    ControlMgr.SetEnableControl(Me, moPersonTypeText, False)
                End If
                'REQ-1255 - END
                ' Start - REQ:5932
                If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If (State.MyBO.CustomerFirstName Is Nothing) And (State.MyBO.CustomerLastName Is Nothing) Then
                        moCustName1.Attributes("style") = "display: none"
                        moCustName2.Attributes("style") = "display: none"
                        moAltCustLasName.Attributes("style") = "display: none"
                        moAltCustFirstName.Attributes("style") = "display: none"
                        AdditionalCustomer.Attributes("style") = "display: none"
                    Else
                        moCustName01.Attributes("style") = "display: none"
                        moCustName02.Attributes("style") = "display: none"
                        'Me.moCustLegalInfo1.Attributes("style") = "display: none"
                        moAMLRegulations3.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, moIncomeRangeLabel, False)
                        ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                        ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILLabel, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)

                        'To Check if Transfer Of Ownership flag is true, then disable Customer name and TAX ID
                        Dim objComp As New Company(State.MyBO.CompanyId)
                        Dim strTranferOfOwnership As String
                        If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                            strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                        Else
                            strTranferOfOwnership = String.Empty
                        End If
                        If strTranferOfOwnership = YES And State.MyBO.StatusCode = CERT_STATUS Then
                            ControlMgr.SetEnableControl(Me, moTaxIdText, False)
                            ControlMgr.SetEnableControl(Me, moCustomerFirstNameText, False)
                            ControlMgr.SetEnableControl(Me, moCustomerMiddleNameText, False)
                            ControlMgr.SetEnableControl(Me, moCustomerLastNameText, False)
                            ControlMgr.SetEnableControl(Me, moAlternativeLastNameText, False)
                            ControlMgr.SetEnableControl(Me, moAlternativeFirstNameText, False)
                            ControlMgr.SetEnableControl(Me, moCorporateNameText, False)
                        End If
                    End If
                Else
                    moCustName1.Attributes("style") = "display: none"
                    moCustName2.Attributes("style") = "display: none"
                    moAltCustLasName.Attributes("style") = "display: none"
                    moAltCustFirstName.Attributes("style") = "display: none"
                    AdditionalCustomer.Attributes("style") = "display: none"
                End If

                ControlMgr.SetVisibleControl(Me, BtnServiceStartDate, True)
                ' End - REQ:5932
            End If



            If State.MyBO.IsChildCertificate Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_ENDORSEMENTS_TAB), False)
                EnableTab(CERT_ENDORSEMENTS_TAB, False)
            End If

            If State.MyBO.StatusCode = CLOSED Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                EnableTab(CERT_CANCELLATION_INFO_TAB, True)
            Else
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), False)
                EnableTab(CERT_CANCELLATION_INFO_TAB, False)
            End If

            If State.ValFlag = State.MyBO.VALIDATION_FLAG_NONE Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_TAX_ID_TAB), False)
                EnableTab(CERT_TAX_ID_TAB, False)
            End If

            If Not State.isPremiumTAbVisible Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_PREMIUM_INFO_TAB), False)
                EnableTab(CERT_PREMIUM_INFO_TAB, False)
            End If

            If Not ExpiredCoveragesExist Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_COVERAGE_HISTORY_TAB), False)
                EnableTab(CERT_COVERAGE_HISTORY_TAB, False)
            End If

            ' This gets enable on the Top of this SUB, with  EnableDisableTabs 
            If State.MyBO.Product.UpgFinanceInfoRequireId.Equals(Guid.Empty) Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_FINANCED_TAB), False)
                EnableTab(CERT_FINANCED_TAB, False)
            End If


            If (State.CertInstallmentHistoryDV IsNot Nothing AndAlso State.CertInstallmentHistoryDV.Count = 0) Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), False)
                EnableTab(CERT_REPRICE_TAB, False)
            End If

            If State.ComputeCancellationDateEOfM.Equals(Codes.YESNO_Y) Then
                ControlMgr.SetVisibleControl(Me, CancelCertReqDateRowHeader, True)
                ControlMgr.SetVisibleControl(Me, CancelCertDateImagebutton, False)
                ControlMgr.SetEnableControl(Me, CancelCertDateTextbox, False)
            Else
                ControlMgr.SetVisibleControl(Me, CancelCertReqDateRowHeader, False)
                ControlMgr.SetVisibleControl(Me, CancelCertDateImagebutton, True)
                ControlMgr.SetEnableControl(Me, CancelCertDateTextbox, True)
            End If


        End Sub

        Private Function ComputeEndOfMonth(baseDate As Date) As Date

            Dim endOfMonth As Date = New Date(baseDate.Year, baseDate.Month, DateTime.DaysInMonth(baseDate.Year, baseDate.Month))
            Return endOfMonth

        End Function

        Private Sub EnableDisablePremiunBankInfoOption()
            Dim result As Boolean = False

            If State.MyBO IsNot Nothing AndAlso State.MyBO.Company IsNot Nothing Then

                'Get Dealer Type
                Dim dealerBO As Dealer = New Dealer(State.MyBO.DealerId)
                If dealerBO IsNot Nothing Then

                    If dealerBO.AttributeValues.Contains(Codes.DLR_ATTRBT__UPDATE_BANK_FOR_INSTALLMENTS) Then

                        If dealerBO.AttributeValues.Value(Codes.DLR_ATTRBT__UPDATE_BANK_FOR_INSTALLMENTS) = Codes.YESNO_Y Then

                            Dim countryBO As Country = New Country(State.MyBO.Company.CountryId)
                            Dim validBankInfoValue = LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, countryBO.ValidateBankInfoId)
                            If Not String.IsNullOrEmpty(validBankInfoValue) Then
                                If String.Compare(validBankInfoValue, Codes.Country_Code_France, True) = 0 Then
                                    result = True
                                End If
                            End If
                        End If

                    End If
                End If
            End If
            ControlMgr.SetVisibleControl(Me, btnBankInfo, result)

        End Sub



        'START  DEF-1986
        Private Sub EnableDisableTaxIdControls(docType As String)
            'START  DEF-1986
            If docType = "CNPJ" Then
                ControlMgr.SetEnableControl(Me, moIDTypeText, False)
                ControlMgr.SetEnableControl(Me, moDocumentAgencyText, False)
                ControlMgr.SetEnableControl(Me, moRGNumberText, False)
                ControlMgr.SetEnableControl(Me, moDocumentIssueDateText, False)
                ControlMgr.SetEnableControl(Me, BtnDocumentIssueDate, False)
                moIDTypeText.Text = String.Empty
                moDocumentAgencyText.Text = String.Empty
                moDocumentIssueDateText.Text = String.Empty
                moRGNumberText.Text = String.Empty
            Else
                ControlMgr.SetEnableControl(Me, moIDTypeText, True)
                ControlMgr.SetEnableControl(Me, moDocumentAgencyText, True)
                ControlMgr.SetEnableControl(Me, moRGNumberText, True)
                ControlMgr.SetEnableControl(Me, moDocumentIssueDateText, True)
                ControlMgr.SetEnableControl(Me, BtnDocumentIssueDate, True)
            End If
        End Sub
        'END DEF-1986

        Private Sub DisableCreditCardInformation()

            moCreditCardInformation1.Attributes("style") = "display: none"
            moCreditCardInformation2.Attributes("style") = "display: none"

        End Sub

        Private Sub setCancelRequestTab()
            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCancelRequestSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCancelRequestUndo_WRITE, True)

            ControlMgr.SetEnableControl(Me, moCancelRequestReasonDrop, True)
            moCancelRequestReasonDrop.CssClass = "FLATTEXTBOX"

            ControlMgr.SetVisibleControl(Me, moCanReqJustificationLabel, True)
            ControlMgr.SetVisibleControl(Me, moCancelRequestJustificationDrop, True)
            moCancelRequestJustificationDrop.CssClass = "FLATTEXTBOX"

            moCancelDateLabel.Enabled = True
            moCancelDateTextBox.ReadOnly = False
            'Me.moCancelDateTextBox.BackColor = Color.White
            'Me.moCancelDateTextBox.CssClass = ""

            moCancelRequestDateLabel.Enabled = True
            moCancelRequestDateTextBox.ReadOnly = False
            'Me.moCancelRequestDateTextBox.BackColor = Color.White
            'Me.moCancelRequestDateTextBox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCallerNameLabel, True)
            ControlMgr.SetVisibleControl(Me, moCallerNameTextBox, True)
            moCallerNameLabel.Enabled = True
            moCallerNameTextBox.ReadOnly = False
            'Me.moCallerNameTextBox.BackColor = Color.White
            'Me.moCallerNameTextBox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCommentsLabel, True)
            ControlMgr.SetVisibleControl(Me, moCommentsTextbox, True)
            moCommentsLabel.Enabled = True
            moCommentsTextbox.ReadOnly = False
            'Me.moCommentsTextbox.BackColor = Color.White
            'Me.moCommentsTextbox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCancelRequestDateImagebutton, True)
            moCancelRequestDateImagebutton.Enabled = True
            ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, True)
            moCancelDateImageButton.Enabled = True
            If State.CancelRulesForSFR = Codes.YESNO_Y Then
                ControlMgr.SetEnableControl(Me, moCancelDateTextBox, False)
                ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
            End If
            ControlMgr.SetEnableControl(Me, moCRIBANNumberText, True)
        End Sub

        Private Sub setCertDetailTab()
            ControlMgr.SetEnableControl(Me, btnEditCertDetail_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoCertDetail_Write, True)
            ControlMgr.SetEnableControl(Me, btnSaveCertDetail_WRITE, True)

            moHomePhoneLabel.Enabled = True
            moHomePhoneText.ReadOnly = False

            moWorkPhoneLabel.Enabled = True
            moWorkPhoneText.ReadOnly = False

            moEmailAddressLabel.Enabled = True
            moEmailAddressText.ReadOnly = False

            moTaxIdLabel.Enabled = True
            moTaxIdText.ReadOnly = False

            Dim dealerBO As Dealer = New Dealer(State.MyBO.DealerId)
            If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                If dealerBO.AttributeValues.Contains(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) Then

                    If dealerBO.AttributeValues.Value(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) = Codes.YESNO_Y Then
                        ControlMgr.SetEnableControl(Me, moAlternativeLastNameLabel, True)
                        ControlMgr.SetVisibleControl(Me, moAlternativeLastNameText, True)
                        ControlMgr.SetEnableControl(Me, moAlternativeFirstNameLabel, True)
                        ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameText, True)
                    End If
                End If
            End If

            If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                moCustomerFirstNameLabel.Enabled = True
                moCustomerFirstNameText.ReadOnly = False
                moCustomerMiddleNameLabel.Enabled = True
                moCustomerMiddleNameText.ReadOnly = False
                moCustomerLastNameLabel.Enabled = True
                moCustomerLastNameText.ReadOnly = False
                moAlternativeLastNameLabel.Enabled = True
                moAlternativeLastNameText.ReadOnly = False
                moAlternativeFirstNameLabel.Enabled = True
                moAlternativeFirstNameText.ReadOnly = False
                moCorporateNameLabel.Enabled = True
                moCorporateNameText.ReadOnly = False
            Else
                moCustomerNameLabel.Enabled = True
                moCustomerNameText.ReadOnly = False
            End If


            AddressCtr.EnableControls(False, True)

            'DEF-21659 - START
            'If Me.State.MyBO.DateOfBirth Is Nothing Then
            '    ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
            'Else
            moDateOfBirthLabel.Enabled = True
            moDateOfBirthText.ReadOnly = False
            ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, True)
            BtnDateOfBirth.Enabled = True
            'End If
            'DEF-21659 - END

            If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                moCityOfBirthText.Enabled = True
                moCityOfBirthText.ReadOnly = False
            End If

            ControlMgr.SetVisibleControl(Me, cboLangPref, True)
            ControlMgr.SetEnableControl(Me, cboLangPref, True)
            ControlMgr.SetVisibleControl(Me, moLangPrefText, False)

            If State.MyBO.IsChildCertificate OrElse State.MyBO.IsParentCertificate Then
                ControlMgr.SetVisibleControl(Me, cboLangPref, False)
                ControlMgr.SetEnableControl(Me, cboLangPref, False)
                ControlMgr.SetVisibleControl(Me, moLangPrefText, True)
            End If


            'AA REQ-910 new fields added BEGIN
            If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
               State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
               State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                moOccupationLabel.Enabled = True
                moOccupationText.ReadOnly = False

                moPoliticallyExposedLabel.Enabled = True
                moPoliticallyExposedText.ReadOnly = False

                moIncomeRangeLabel.Enabled = True
                moIncomeRangeText.ReadOnly = False
            End If
            'AA REQ-910 new fields added END

            'REQ-1255 -- START
            If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
               State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
               State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                moCUIT_CUILLabel.Enabled = True
                moCUIT_CUILText.ReadOnly = False
            End If
            'REQ-1255 -- END
        End Sub

        Private Sub setCertInfoTab()
            ControlMgr.SetEnableControl(Me, btnEditCertInfo_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoCertInfo_Write, True)
            ControlMgr.SetEnableControl(Me, btnSaveCertInfo_WRITE, True)

            moInvoiceNumberLabel.Enabled = True
            moInvoiceNumberText.ReadOnly = False

            If State.MyBO.Source.ToUpper().Equals("VSC") Then
                moVehicleLicenseTagText.Enabled = True
                moVehicleLicenseTagText.ReadOnly = False
            End If

            If State.TheItemCoverageState.manufaturerWarranty Then
                moWarrantySoldLabel.Enabled = True
                'Me.moWarrantySoldText.ReadOnly = False
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, True)
                BtnWarrantySoldDate.Enabled = True
            Else
                moProductSalesDateLabel.Enabled = True
                moProductSalesDateText.ReadOnly = False
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, True)
                BtnProductSalesDate.Enabled = True
            End If

            If Not State.TheItemCoverageState.IsRetailer Then
                moRetailerLabel.Enabled = True
                moRetailerText.ReadOnly = False
            End If

            If State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, True)
                BtnContractCheckCompleteDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, True)
                BtnCertificateVerificationDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, True)
                BtnSEPAMandateDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, True)
                BtnCheckVerificationDate.Enabled = True
            End If

            moSalesRepNumberLabel.Enabled = True
            moSalesRepNumberText.ReadOnly = False

            moAccountNumberText.ReadOnly = False
            cboAccountType.Enabled = True

            moVATNumLabel.Enabled = True
            moVATNumText.ReadOnly = False

            moRegionText.Enabled = True
            moRegionText.ReadOnly = False

            If State.MyBO.IsParentCertificate OrElse State.MyBO.IsChildCertificate Then
                ControlMgr.SetEnableControl(Me, moProductSalesDateText, False)
                ControlMgr.SetEnableControl(Me, moActivationDateText, False)
            End If
            ControlMgr.SetEnableControl(Me, moFulfillmentConsentActionDrop, False)

        End Sub

        Private Sub setTaxIdTab()

            ControlMgr.SetEnableControl(Me, btnEditTaxID_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoTaxID_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSaveTaxID_WRITE, True)

            ControlMgr.SetVisibleControl(Me, moTaxIdText, True)

            ControlMgr.SetVisibleControl(Me, BtnDocumentIssueDate, True)
            BtnDocumentIssueDate.Enabled = True

            moDocumentIssueDateText.ReadOnly = False

            ControlMgr.SetEnableControl(Me, cboDocumentTypeId, True)
            ControlMgr.SetVisibleControl(Me, moDocumentTypeText, False)
            ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, True)

            moRGNumberText.ReadOnly = False
            moDocumentAgencyText.ReadOnly = False
            moIDTypeText.ReadOnly = False
            moNewTaxIdText.ReadOnly = False
        End Sub

        Protected Sub BindInstallmentBoPropertiesToLabels()
            BindBOPropertyToLabel(State.TheDirectDebitState.certInstallment, "BillingStatusId", moBilling_StatusLabel)
            BindBOPropertyToLabel(State.TheDirectDebitState.certInstallment, "SendLetterId", moSendLetterIdLabel)

            BindBOPropertyToLabel(State.TheDirectDebitState.bankInfo, "Account_Name", moBankAccountOwnerLabel)
            BindBOPropertyToLabel(State.TheDirectDebitState.bankInfo, "Bank_Id", moBankRoutingNumberLabel)
            BindBOPropertyToLabel(State.TheDirectDebitState.bankInfo, "Account_Number", moBankAccountNumberLabel)
        End Sub

        Protected Sub BindCancelRequestBoPropertiesToLabels()
            If Not moCancelRequestReasonLabel.Text.StartsWith("* ") Then
                moCancelRequestReasonLabel.Text = "* " & moCancelRequestReasonLabel.Text
            End If
            If Not moCancelRequestReasonLabel.Text.EndsWith(":") Then
                moCancelRequestReasonLabel.Text = moCancelRequestReasonLabel.Text & ":"
            End If

            If Not moCancelRequestDateLabel.Text.StartsWith("* ") Then
                moCancelRequestDateLabel.Text = "* " & moCancelRequestDateLabel.Text
            End If
            If Not moCancelRequestDateLabel.Text.EndsWith(":") Then
                moCancelRequestDateLabel.Text = moCancelRequestDateLabel.Text & ":"
            End If

            If Not moCancelDateLabel.Text.StartsWith("* ") Then
                moCancelDateLabel.Text = "* " & moCancelDateLabel.Text
            End If
            If Not moUseExistingBankDetailsLabel.Text.EndsWith(":") Then
                moUseExistingBankDetailsLabel.Text = moUseExistingBankDetailsLabel.Text & ":"
            End If
            If Not moProofOfDocumentationLabel.Text.EndsWith(":") Then
                moProofOfDocumentationLabel.Text = moProofOfDocumentationLabel.Text & ":"
            End If
            If Not moCRIBANNumberLabel.Text.EndsWith(":") Then
                moCRIBANNumberLabel.Text = moCRIBANNumberLabel.Text & ":"
            End If
            If Not moCancelDateLabel.Text.EndsWith(":") Then
                moCancelDateLabel.Text = moCancelDateLabel.Text & ":"
            End If
            If Not moCanReqJustificationLabel.Text.StartsWith("* ") Then
                moCanReqJustificationLabel.Text = "* " & moCanReqJustificationLabel.Text
            End If
            If Not moCanReqJustificationLabel.Text.EndsWith(":") Then
                moCanReqJustificationLabel.Text = moCanReqJustificationLabel.Text & ":"
            End If

            If Not moCallerNameLabel.Text.StartsWith("* ") Then
                moCallerNameLabel.Text = "* " & moCallerNameLabel.Text
            End If
            If Not moCallerNameLabel.Text.EndsWith(":") Then
                moCallerNameLabel.Text = moCallerNameLabel.Text & ":"
            End If

            If Not moCommentsLabel.Text.StartsWith("* ") Then
                moCommentsLabel.Text = "* " & moCommentsLabel.Text
            End If
            If Not moCommentsLabel.Text.EndsWith(":") Then
                moCommentsLabel.Text = moCommentsLabel.Text & ":"
            End If
            If Not moCancelRequestStatusLabel.Text.EndsWith(":") Then
                moCancelRequestStatusLabel.Text = moCancelRequestStatusLabel.Text & ":"
            End If
        End Sub

        Protected Sub BindCancellationBoPropertiesToLabels()

            If Not moCancelCallerNameLabel.Text.StartsWith("* ") Then
                moCancelCallerNameLabel.Text = "* " & moCancelCallerNameLabel.Text
            End If

            If Not moCancelCallerNameLabel.Text.EndsWith(":") Then
                moCancelCallerNameLabel.Text = moCancelCallerNameLabel.Text & ":"
            End If

            If Not moCancellationReasonDrpLabel.Text.StartsWith("* ") Then
                moCancellationReasonDrpLabel.Text = "* " & moCancellationReasonDrpLabel.Text
            End If

            If Not moCancellationReasonDrpLabel.Text.EndsWith(":") Then
                moCancellationReasonDrpLabel.Text = moCancellationReasonDrpLabel.Text & ":"
            End If

            If Not CancelCertDateLabel.Text.StartsWith("* ") Then
                CancelCertDateLabel.Text = "* " & CancelCertDateLabel.Text
            End If

            If Not CancelCertDateLabel.Text.EndsWith(":") Then
                CancelCertDateLabel.Text = CancelCertDateLabel.Text & ":"
            End If


            If Not CancelCommentType.Text.StartsWith("* ") Then
                CancelCommentType.Text = "* " & CancelCommentType.Text
            End If

            If Not CancelCommentType.Text.EndsWith(":") Then
                CancelCommentType.Text = CancelCommentType.Text & ":"
            End If

            If Not moCanelCommentsLabel.Text.StartsWith("* ") Then
                moCanelCommentsLabel.Text = "* " & moCanelCommentsLabel.Text
            End If

            If Not moCanelCommentsLabel.Text.EndsWith(":") Then
                moCanelCommentsLabel.Text = moCanelCommentsLabel.Text & ":"
            End If
        End Sub

        'Public Function getCancelRequestReasonDescription(ByVal CancellationReasonId As Guid) As String
        '    Dim cancellationReasonDesc As String
        '    Dim cancellationReasonDV As DataView = LookupListNew.GetCancellationReasonWithCodeLookupList(Me.State.MyBO.CompanyId)
        '    cancellationReasonDV.RowFilter = "code in ('16','17')"
        '    cancellationReasonDesc = LookupListNew.GetDescriptionFromId(cancellationReasonDV, CancellationReasonId)
        '    Return cancellationReasonDesc
        'End Function

        Protected Sub BindBoPropertiesToLabels()
            'pedro
            BindBOPropertyToLabel(State.MyBO, "CustomerName", moCustomerNameLabel)
            BindBOPropertyToLabel(State.MyBO, "HomePhone", moHomePhoneLabel)
            BindBOPropertyToLabel(State.MyBO, "WorkPhone", moWorkPhoneLabel)
            BindBOPropertyToLabel(State.MyBO, "Email", moEmailAddressLabel)
            BindBOPropertyToLabel(State.MyBO, "IdentificationNumber", moTaxIdLabel)
            BindBOPropertyToLabel(State.MyBO, "InvoiceNumber", moInvoiceNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "ProductSalesDate", moProductSalesDateLabel)
            BindBOPropertyToLabel(State.MyBO, "WarrantySalesDate", moWarrantySoldLabel)
            BindBOPropertyToLabel(State.MyBO, "Retailer", moRetailerLabel)
            BindBOPropertyToLabel(State.MyBO, "SalutationId", moSalutationLabel)
            BindBOPropertyToLabel(State.MyBO, "LanguageId", moLangPrefLabel)
            BindBOPropertyToLabel(State.MyBO, "PostPrePaidId", moPostPaidLabel)
            BindBOPropertyToLabel(State.MyBO, "VehicleLicenseTag", moVehicleLicenseTagLabel)
            BindBOPropertyToLabel(State.MyBO, "DocumentIssueDate", moDocumentIssueDateLabel)
            BindBOPropertyToLabel(State.MyBO, "DocumentTypeID", moDocumentTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "RgNumber", moRGNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "DocumentAgency", moDocumentAgencyLabel)
            BindBOPropertyToLabel(State.MyBO, "IdType", moIDTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "TaxIDNumb", moNewTaxIdLabel)
            BindBOPropertyToLabel(State.MyBO, "SalesRepNumber", moSalesRepNumberLabel)

            BindBOPropertyToLabel(State.CreditCardInfoBO, "CreditCardFormatId", moCreditCardTypeIDLabel)
            BindBOPropertyToLabel(State.CreditCardInfoBO, "CreditCardNumber", moCreditCardNumberLabel)
            BindBOPropertyToLabel(State.CreditCardInfoBO, "ExpirationDate", moExpirationDateLabel)
            BindBOPropertyToLabel(State.CreditCardInfoBO, "NameOnCreditCard", moNameOnCreditCardLabel)

            BindBOPropertyToLabel(State.MyBO, "DateOfBirth", moDateOfBirthLabel)
            BindBOPropertyToLabel(State.MyBO, "VatNum", moVATNumLabel)

            BindBOPropertyToLabel(State.MyBO, "CapitalizationSeries", moCapSeriesLabel)
            BindBOPropertyToLabel(State.MyBO, "CapitalizationNumber", moCapNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "SubStatusChangeDate", moSubStatusChangeDateLabel)
            BindBOPropertyToLabel(State.MyBO, "LinesOnAccount", moLinesOnAccountLabel)

            ClearGridHeadersAndLabelsErrSign()

            'Added for Req-910 - Start
            BindBOPropertyToLabel(State.MyBO, "Occupation", moOccupationLabel)
            BindBOPropertyToLabel(State.MyBO, "PoliticallyExposedId", moPoliticallyExposedLabel)
            BindBOPropertyToLabel(State.MyBO, "IncomeRangeId", moIncomeRangeLabel)
            'Added for Req-910 - End

            'REQ-1255 - AML Regulations - START
            BindBOPropertyToLabel(State.MyBO, "MaritalStatus", moMaritalStatusLabel)
            BindBOPropertyToLabel(State.MyBO, "Nationality", moNationalityLabel)
            BindBOPropertyToLabel(State.MyBO, "PlaceOfBirth", moPlaceOfBirthLabel)
            BindBOPropertyToLabel(State.MyBO, "CityOfBirth", mocityOfBirthLabel)
            BindBOPropertyToLabel(State.MyBO, "PersonType", moPerson_typeLabel)
            BindBOPropertyToLabel(State.MyBO, "Gender", moGenderLabel)
            BindBOPropertyToLabel(State.MyBO, "CUIT_CUIL", moCUIT_CUILLabel)
            'REQ-1255 - AML Regulations - END

            BindBOPropertyToLabel(State.MyBO, "Finance_Tab_Amount", moFinanceAmountLabel)
            '  Me.BindBOPropertyToLabel(Me.State.MyBO, "", Me.moCurrentOutstandingBalanceLabel)
            BindBOPropertyToLabel(State.MyBO, "Finance_Term", moFinanceTermLabel)
            BindBOPropertyToLabel(State.MyBO, "FinanceDate", moFinanceDateLabel)
            BindBOPropertyToLabel(State.MyBO, "Finance_Frequency", moFinanceFrequencyLabel)
            BindBOPropertyToLabel(State.MyBO, "DownPayment", moDownPaymentLabel)
            BindBOPropertyToLabel(State.MyBO, "Finance_Installment_Number", moFinanceInstallmentNumLabel)
            BindBOPropertyToLabel(State.MyBO, "Finance_Installment_Amount", moFinanceInstallmentAmountLabel)
            BindBOPropertyToLabel(State.MyBO, "AdvancePayment", moAdvancePaymentLabel)
            BindBOPropertyToLabel(State.MyBO, "NumOfConsecutivePayments", moNumOfConsecutivePaymentsLabel)
            BindBOPropertyToLabel(State.MyBO, "BillingAccountNumber", moBillingAccountNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "DealerRewardPoints", moDealerRewardPointsLabel)
            BindBOPropertyToLabel(State.MyBO, "DealerCurrentPlanCode", moDealerCurrentPlanCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "DealerScheduledPlanCode", moDealerScheduledPlanCodeLabel)
            '            Me.BindBOPropertyToLabel(Me.State.MyBO, "", Me.lblUpgradeTermUnitOfMeasure)
            BindBOPropertyToLabel(State.MyBO, "UpgradeProgram", moUpgradeProgramLabel)
            BindBOPropertyToLabel(State.MyBO, "UpgradeFixedTerm", moUpgradeFixedTermLabel)
            BindBOPropertyToLabel(State.MyBO, "UpgradeTermFrom", moUpgradeTermLabelFrom)
            BindBOPropertyToLabel(State.MyBO, "UpgradeTermTo", moUpgradeTermLabelTo)
            If Not moCurrentOutstandingBalanceLabel.Text.EndsWith(":") Then
                moCurrentOutstandingBalanceLabel.Text = moCurrentOutstandingBalanceLabel.Text & ":"
            End If
            If Not lblUpgradeTermUnitOfMeasure.Text.EndsWith(":") Then
                lblUpgradeTermUnitOfMeasure.Text = lblUpgradeTermUnitOfMeasure.Text & ":"
            End If

            BindBOPropertyToLabel(State.MyBO, "PaymentShiftNumber", moPaymentShiftNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "LoanCode", moLoanCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "PenaltyFee", moPenaltyFeeLabel)
            BindBOPropertyToLabel(State.MyBO, "CertificateSigned", moCertificateSignedLabel)
            BindBOPropertyToLabel(State.MyBO, "SepaMandateSigned", moSEPAMandateSignedLabel)
            BindBOPropertyToLabel(State.MyBO, "CheckSigned", moCheckSignedLabel)
            BindBOPropertyToLabel(State.MyBO, "ContractCheckCompleteDate", moContractCheckCompleteDateLabel)
            BindBOPropertyToLabel(State.MyBO, "CertificateVerificationDate", moCertificateVerificationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "SepaMandateDate", moSEPAMandateDateLabel)
            BindBOPropertyToLabel(State.MyBO, "CheckVerificationDate", moCheckVerificationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "ContractCheckComplete", moContractCheckCompleteLabel)
            BindBOPropertyToLabel(State.MyBO, "InsuranceOrderNumber", moInsuranceOrderNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "DeviceOrderNumber", moDeviceOrderNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "UpgradeType", moUpgradeTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "FulfillmentConsentAction", moFulfillmentConsentAction)
            BindBOPropertyToLabel(State.MyBO, "PlanType", moPlanTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "ServiceStartDate", moServiceStartDateLabel)
            BindBOPropertyToLabel(State.MyBO, "ServiceID", moServiceIdLabel)
        End Sub

        Private Sub ObtainBooleanFlags()
            Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(State.MyBO.CompanyId)
            Dim oDealer As Assurant.ElitaPlus.BusinessObjectsNew.Dealer = New Assurant.ElitaPlus.BusinessObjectsNew.Dealer(State.MyBO.DealerId)
            Dim oClaimSystem As New ClaimSystem(oDealer.ClaimSystemId) 'DEF-2486
            Dim noId As New Guid 'DEF-2486

            State.companyCode = companyBO.Code

            State.isSalutation = GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, companyBO.SalutationId)

            State.IsNewBillPayBtnVisible = GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, oDealer.UseNewBillForm)

            'DEF-2486
            noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

            If oClaimSystem.NewClaimId.Equals(noId) Then
                State.IsClaimAllowed = False
            End If
            'DEF-2486

            'Task-203459
            If oDealer.ClaimRecordingXcd = Codes.DEALER_CLAIM_RECORDING_BOTH Then
                State.AllowOldAndNewDCMClaims = True
            Else
                State.AllowOldAndNewDCMClaims = False
            End If
            'Task-203459

            If Not (State.MyBO.PostPrePaidId.Equals(Guid.Empty)) Then
                State.isPostPrePaid = True
            Else
                State.isPostPrePaid = False
            End If
            State.TheItemCoverageState.IsRetailer = GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, oDealer.RetailerId)
            State.TheItemCoverageState.dealerName = oDealer.DealerName
            State.ClaimRecordingXcd = oDealer.ClaimRecordingXcd
        End Sub

        Private Function GetYesNo(LanguageId As Guid, oId As Guid) As Boolean
            Dim oYesList As DataView = LookupListNew.GetListItemId(oId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
            If oYesNo = YES Then
                Return True
            Else
                Return False
            End If
        End Function

        Protected Sub PopulateFormFromBOs(Optional ByVal blnPremiumEdit As Boolean = False)
            Dim typeOfEquip As String
            Dim IsDisplayMaskFlag As Boolean

            If State.isSalutation Then
                PopulateSalutationDropdown(cboSalutationId)
            End If

            PopulateLangPrefDropdown(cboLangPref)

            If Not (State.MyBO.LanguageId.Equals(Guid.Empty)) Then
                SetSelectedItem(cboLangPref, State.MyBO.LanguageId)
                PopulateControlFromBOProperty(moLangPrefText, State.MyBO.getLanguagePrefDesc)
            End If

            If Not (State.MyBO.PostPrePaidId.Equals(Guid.Empty)) Then
                PopulateControlFromBOProperty(moPostPaidText, State.MyBO.getPostPrePaidDesc)
            End If

            If Not (State.MyBO.TypeOfEquipmentId.Equals(Guid.Empty)) Then
                Dim dv As DataView = LookupListNew.GetListItemId(State.MyBO.TypeOfEquipmentId, Authentication.LangId)
                typeOfEquip = dv.Item(FIRST_ROW).Item(DESCRIPTION).ToString
            End If

            If Not (State.MyBO.PaymentTypeId.Equals(Guid.Empty)) Then
                If (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) _
                   OrElse (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) _
                   OrElse (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY) _
                   OrElse (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD) Then
                    State.creditCardPayment = True
                End If
                PopulateControlFromBOProperty(moPaymentByText, State.MyBO.getPaymentTypeDescription)
            End If

            If Not (State.MyBO.PurchaseCurrencyId.Equals(Guid.Empty)) Then
                PopulateControlFromBOProperty(moCurrencyPurchaseText, State.MyBO.getPurchaseCurrencyDescription)
            End If

            'If Not (Me.State.MyBO.getClaimWaitingPeriod.Equals(Guid.Empty)) Then
            '    Dim waitingPeriodEndDate = Me.State.MyBO.WarrantySalesDate.Value
            '    Dim wpDate = waitingPeriodEndDate.AddDays(State.MyBO.getClaimWaitingPeriod)
            '    Me.PopulateControlFromBOProperty(Me.moClaimWaitingPeriodEndDateText, wpDate)
            'Else
            '    Me.PopulateControlFromBOProperty(Me.moClaimWaitingPeriodEndDateText, Me.State.MyBO.WarrantySalesDate)
            'End If

            moCertificateInfoController = UserCertificateCtr
            moCertificateInfoController.InitController(State.MyBO, , State.companyCode)
            UpdateBreadCrum()

            With State.MyBO
                AddressCtr.Bind(.AddressChild, State.MyBO.Product.ClaimProfile)
                PopulateControlFromBOProperty(moCountryOfPurchaseTextBox, State.CountryOfPurchase)
                PopulateControlFromBOProperty(moCustomerNameText, .CustomerName)
                PopulateControlFromBOProperty(moHomePhoneText, .HomePhone)
                PopulateControlFromBOProperty(moWorkPhoneText, .WorkPhone)
                If .Email Is Nothing OrElse .Email.Equals(String.Empty) Then
                    State.EmailIsNull = TranslationBase.TranslateLabelOrMessage("THERE_IS_NO_VALUE")
                    PopulateControlFromBOProperty(moEmailAddressText, State.EmailIsNull)
                Else
                    PopulateControlFromBOProperty(moEmailAddressText, .Email)
                End If

                PopulateControlFromBOProperty(moTaxIdText, .IdentificationNumber)
                ' Based on identification number type - change the label text
                If Not (.IdentificationNumberType Is Nothing) AndAlso Not (.IdentificationNumberType = IDENTIFICATION_NUMBER_TYPE_DEFAULT) Then
                    moTaxIdLabel.Text = TranslationBase.TranslateLabelOrMessage(.IdentificationNumberType)
                End If

                PopulateControlFromBOProperty(moProductSalesDateText, .ProductSalesDate)
                PopulateControlFromBOProperty(moActivationDateText, .InsuranceActivationDate)
                PopulateControlFromBOProperty(moOldNumberText, .OldNumber)
                PopulateControlFromBOProperty(moWarrantySoldText, .WarrantySalesDate)
                PopulateControlFromBOProperty(moInvoiceNumberText, .InvoiceNumber)
                PopulateControlFromBOProperty(moAccountNumberText, .MembershipNumber)
                PopulateControlFromBOProperty(moCampaignNumberText, .CampaignNumber)
                PopulateControlFromBOProperty(moDealerBranchCodeText, .DealerBranchCode)
                PopulateControlFromBOProperty(moSalesRepNumberText, .SalesRepNumber)
                PopulateControlFromBOProperty(moDealerItemText, .DealerItem)
                PopulateControlFromBOProperty(moSalesPriceText, .SalesPrice)
                PopulateControlFromBOProperty(moProductCodeText, .ProductCode)
                PopulateControlFromBOProperty(moDealerProductCodeText, .DealerProductCode)
                PopulateControlFromBOProperty(moDateAddedText, .DateAdded)
                PopulateControlFromBOProperty(moLastMaintainedTextbox, .LastDateMaintained)
                PopulateControlFromBOProperty(moBillingDateText, .DatePaidFor)

                PopulateControlFromBOProperty(moVehicleLicenseTagText, .VehicleLicenseTag)

                'Added for Req-703 - Start
                PopulateControlFromBOProperty(moCapSeriesText, .CapitalizationSeries)
                PopulateControlFromBOProperty(moCapNumberText, .CapitalizationNumber)
                'Added for Req-703 - End               

                PopulateControlFromBOProperty(moDatePaidText, .DatePaid)

                PopulateControlFromBOProperty(moDocumentIssueDateText, .DocumentIssueDate)

                PopulateDocumentTypeDropdown(cboDocumentTypeId)
                SetSelectedItem(cboDocumentTypeId, .DocumentTypeID)

                PopulateMembershipTypeDropdown(cboAccountType)
                SetSelectedItem(cboAccountType, .MembershipTypeId)

                PopulateControlFromBOProperty(moDocumentTypeText, .getDocTypeDesc)

                PopulateControlFromBOProperty(moRGNumberText, .RgNumber)
                PopulateControlFromBOProperty(moDocumentAgencyText, .DocumentAgency)
                PopulateControlFromBOProperty(moIDTypeText, .IdType)
                PopulateControlFromBOProperty(moNewTaxIdText, .TaxIDNumb)
                PopulateControlFromBOProperty(TextboxRatingPlan, .RatingPlan)
                PopulateControlFromBOProperty(txtSource, .Source)
                PopulateControlFromBOProperty(moServiceLineNumberText, .ServiceLineNumber)
                PopulateControlFromBOProperty(moRegionText, .Region)

                If .SubStatusChangeDate IsNot Nothing Then
                    PopulateControlFromBOProperty(moSubStatusChangeDateText, .SubStatusChangeDate)
                Else
                    ControlMgr.SetVisibleControl(Me, moSubStatusChangeDateLabel, False)
                    ControlMgr.SetVisibleControl(Me, moSubStatusChangeDateText, False)
                End If

                If .LinesOnAccount IsNot Nothing Then
                    PopulateControlFromBOProperty(moLinesOnAccountText, .LinesOnAccount)
                Else
                    ControlMgr.SetVisibleControl(Me, moLinesOnAccountLabel, False)
                    ControlMgr.SetVisibleControl(Me, moLinesOnAccountText, False)
                End If

                If .BillingPlan IsNot Nothing Then
                    PopulateControlFromBOProperty(moBillingPlanText, .BillingPlan)
                Else
                    ControlMgr.SetVisibleControl(Me, moBillingPlanLabel, False)
                    ControlMgr.SetVisibleControl(Me, moBillingPlanText, False)
                End If

                If .BillingCycle IsNot Nothing Then
                    PopulateControlFromBOProperty(moBillingCycleText, .BillingCycle)
                Else
                    ControlMgr.SetVisibleControl(Me, moBillingCycleLabel, False)
                    ControlMgr.SetVisibleControl(Me, moBillingCycleText, False)
                End If

                If .DocumentIssueDate IsNot Nothing _
                   Or .IdType IsNot Nothing _
                   Or .DocumentAgency IsNot Nothing _
                   Or .RgNumber IsNot Nothing _
                   Or Not .DocumentTypeID.Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, moTaxIdLabel, False)
                    ControlMgr.SetVisibleControl(Me, moTaxIdText, False)
                End If

                If State.isSalutation Then
                    SetSelectedItem(cboSalutationId, .SalutationId)
                    PopulateControlFromBOProperty(moSalutationText, .getSalutationDescription)
                End If

                If State.isPostPrePaid Then
                    PopulateControlFromBOProperty(moPostPaidText, .getPostPrePaidDesc)
                End If

                If State.TheItemCoverageState.IsRetailer Then
                    PopulateControlFromBOProperty(moRetailerText, State.TheItemCoverageState.dealerName)
                Else
                    PopulateControlFromBOProperty(moRetailerText, .Retailer)
                End If

                If (.DatePaid Is Nothing) AndAlso (.DatePaidFor Is Nothing) Then
                    ControlMgr.SetVisibleControl(Me, moBillingDateText, False)
                    ControlMgr.SetVisibleControl(Me, moBillingDateLabel, False)
                    tdBillingDate.InnerHtml = ""
                    ControlMgr.SetVisibleControl(Me, moDatePaidText, False)
                    ControlMgr.SetVisibleControl(Me, moDatePaidLabel, False)
                    tdDatePaid.InnerHtml = ""
                End If

                If ((.Source Is Nothing) Or (Not .Source.ToUpper().Equals("VSC"))) Then 'Or (.VehicleLicenseTag Is Nothing Or .VehicleLicenseTag = "")) Then
                    ControlMgr.SetVisibleControl(Me, moVehicleLicenseTagText, False)
                    ControlMgr.SetVisibleControl(Me, moVehicleLicenseTagLabel, False)
                    tdVehicleLicenseTag.InnerHtml = ""
                End If

                'If (Me.State.MyBO.Company.EnablePeriodMileageVal.ToUpper().Equals("YESNO-N")) Then
                '    ControlMgr.SetVisibleControl(Me, trmfgdate, False)
                '    ControlMgr.SetVisibleControl(Me, trmfgkm, False)
                'Else
                '    ControlMgr.SetVisibleControl(Me, trmfgdate, True)
                '    ControlMgr.SetVisibleControl(Me, trmfgkm, True)
                'End If

                'DEF-21659 - START
                'If .DateOfBirth Is Nothing Then
                'ControlMgr.SetVisibleControl(Me, moDateOfBirthText, False)
                'ControlMgr.SetVisibleControl(Me, moDateOfBirthLabel, False)
                'Me.tdDateOfBirthTag.InnerHtml = ""
                'ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                'End If
                'Me.PopulateControlFromBOProperty(Me.moDateOfBirthText, .DateOfBirth)
                'DEF-21659 - END

                If Not blnPremiumEdit Then PopulatePaymentTypesDropdown(moPaymentTypeId)

                If State.creditCardPayment Then
                    'Get the credit card types:
                    If Not blnPremiumEdit Then
                        PopulateCreditCardTypesDropdown(moCreditCardTypeIDDropDown)
                    Else
                        'User cannot change the type from credit to bank or vis versa.

                        'Me.State.TheDirectDebitState.CreditCardInfo = Me.State.TheDirectDebitState.certInstallment.AddCreditCardInfo(Guid.Empty)
                    End If

                Else
                    DisableCreditCardInformation()
                End If

                PopulateControlFromBOProperty(moDescriptionText, .GetProdCodeDesc)
                PopulateControlFromBOProperty(moTypeOfEquipmentText, typeOfEquip)
                PopulateControlFromBOProperty(moVATNumText, .VatNum)
                PopulateControlFromBOProperty(moSEPAMandateDateText, .SepaMandateDate)
                PopulateControlFromBOProperty(moCheckVerificationDateText, .CheckVerificationDate)
                PopulateControlFromBOProperty(moCertificateVerificationDateText, .CertificateVerificationDate)
                PopulateControlFromBOProperty(moContractCheckCompleteDateText, .ContractCheckCompleteDate)
                PopulateControlFromBOProperty(moServiceStartDateText, .ServiceStartDate)
                PopulateControlFromBOProperty(moServiceIdText, .ServiceID)

                'Me.PopulateControlFromBOProperty(Me.txtMfgBeginDate, .MfgBeginDate)
                'Me.PopulateControlFromBOProperty(Me.txtMfgEndDate, .MfgEndDate)
                'Me.PopulateControlFromBOProperty(Me.txtMfgBeginKm, .MfgBeginKm)
                'Me.PopulateControlFromBOProperty(Me.txtMfgEndKm, .MfgEndKm)
                'REQ-910 New fields BEGIN
                If State.ReqCustomerLegalInfoId.Equals(Guid.Empty) Then
                    State.ReqCustomerLegalInfoId = (New Company(State.MyBO.CompanyId).ReqCustomerLegalInfoId)
                End If

                If State.MyBO.Dealer.DisplayDobXcd Is Nothing Then
                    IsDisplayMaskFlag = False
                Else
                    IsDisplayMaskFlag = State.MyBO.Dealer.DisplayDobXcd.ToUpper().Equals("YESNO-Y")
                End If

                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) Then '0= None
                    moCustLegalInfo1.Attributes("style") = "display: none"
                    ControlMgr.SetVisibleControl(Me, moIncomeRangeLabel, False)   'Me.moCustLegalInfo2.Attributes("style") = "display: none"
                    ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                    ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)

                    If (Not IsDisplayMaskFlag) Then
                        ControlMgr.SetVisibleControl(Me, moDateOfBirthLabel, False)
                        ControlMgr.SetVisibleControl(Me, moDateOfBirthText, False)
                        ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                        ControlMgr.SetVisibleControl(Me, tdDateOfBirthCalTag, False)
                    End If

                Else
                        'populate the dropdowns
                        PopulateIncomeRangeDropdown(cboIncomeRangeId)
                    PopulatePoliticallyExposedDropdown(cboPoliticallyExposedId)
                    PopulateControlFromBOProperty(moOccupationText, .Occupation)
                    SetSelectedItem(cboPoliticallyExposedId, .PoliticallyExposedId)
                    SetSelectedItem(cboIncomeRangeId, .IncomeRangeId)
                    PopulateControlFromBOProperty(moPoliticallyExposedText, cboPoliticallyExposedId.SelectedItem.Text.ToString)
                    PopulateControlFromBOProperty(moIncomeRangeText, cboIncomeRangeId.SelectedItem.Text.ToString)
                End If
                'REQ-910 New fields END

                'REQ-1255 - AML Regulations - START
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) Then '0= None
                    moAMLRegulations0.Attributes("style") = "display: none"
                    moAMLRegulations1.Attributes("style") = "display: none"
                    moAMLRegulations2.Attributes("style") = "display: none"
                    moAMLRegulations3.Attributes("style") = "display: none"
                    moAMLRegulations4.Attributes("style") = "display: none"
                Else
                    'populate the dropdowns
                    'Marital Status
                    PopulateMaritalStatusDropdown(ddlMaritalStatus)
                    SetSelectedItem(ddlMaritalStatus, .MaritalStatus)
                    PopulateControlFromBOProperty(moMaritalStatusText, ddlMaritalStatus.SelectedItem.Text) '.MaritalStatus
                    'Nationality
                    PopulateNationalityDropdown(ddlNationality)
                    SetSelectedItem(ddlNationality, .Nationality)
                    PopulateControlFromBOProperty(moNationalityText, ddlNationality.SelectedItem.Text) '.Nationality
                    'PlaceOfBirth
                    PopulatePlaceOfBirthDropdown(ddlPlaceOfBirth)
                    SetSelectedItem(ddlPlaceOfBirth, .PlaceOfBirth)
                    PopulateControlFromBOProperty(moPlaceOfBirthText, ddlPlaceOfBirth.SelectedItem.Text) '.PlaceOfBirth
                    'CityOfBirth
                    If Not .CustomerId.Equals(Guid.Empty) Then
                        PopulateControlFromBOProperty(moCityOfBirthText, .CityOfBirth)
                    End If
                    'Gender
                    PopulateGenderDropdown(ddlGender)
                        SetSelectedItem(ddlGender, .Gender)
                        PopulateControlFromBOProperty(moGenderText, ddlGender.SelectedItem.Text) '.Gender
                        'CUIT_CUIL
                        PopulateControlFromBOProperty(moCUIT_CUILText, .CUIT_CUIL)
                        'Person Type
                        PopulatePersonTypeDropdown(ddlPersonType)
                        SetSelectedItem(ddlPersonType, .PersonTypeId)
                        PopulateControlFromBOProperty(moPersonTypeText, ddlPersonType.SelectedItem.Text)
                    End If
                    'REQ-1255 - AML Regulations - END
                    If Not .ReinsuranceStatusId.Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusLabel, True)
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusText, True)
                    PopulateControlFromBOProperty(moReinsuranceStatusText, LookupListNew.GetDescriptionFromId(LookupListNew.LK_REINSURANCE_STATUSES, .ReinsuranceStatusId))
                Else
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusText, False)
                End If
                If (Not .ReinsuranceStatusId.Equals(Guid.Empty)) AndAlso .ReinsuranceStatusId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REINSURANCE_STATUSES, LookupListCache.LK_REINS_STATUS_REJECTED)) Then
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonLabel, True)
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonText, True)
                    PopulateControlFromBOProperty(moReinsRejectReasonText, .ReinsuredRejectReason)
                Else
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonLabel, False)
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonText, False)
                End If
                ' REQ 5932

                If Not .CustomerId.Equals(Guid.Empty) Then
                    PopulateControlFromBOProperty(moCustomerFirstNameText, .CustomerFirstName)
                    PopulateControlFromBOProperty(moCustomerMiddleNameText, .CustomerMiddleName)
                    PopulateControlFromBOProperty(moCustomerLastNameText, .CustomerLastName)
                    PopulateControlFromBOProperty(moAlternativeLastNameText, .AlternativeLastName)
                    PopulateControlFromBOProperty(moAlternativeFirstNameText, .AlternativeFirstName)
                    PopulateControlFromBOProperty(moCorporateNameText, .CorporateName)
                End If
                moCertificateSigneddrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                'Me.SetSelectedItem(Me.moCertificateSigneddrop, .CertificateSigned)
                BindSelectItem(.CertificateSigned, moCertificateSigneddrop)
                moSEPAMandateSignedDrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.SepaMandateSigned, moSEPAMandateSignedDrop)
                moCheckSignedDrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                'Check Signed Drop Down will not have Invalid Signature and Filled By Hand values.
                Dim listItem1 As WebControls.ListItem = moCheckSignedDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("DOCUMENT_STATUS", "IS"))
                moCheckSignedDrop.Items.Remove(listItem1)
                Dim listItem2 As WebControls.ListItem = moCheckSignedDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("DOCUMENT_STATUS", "FH"))
                moCheckSignedDrop.Items.Remove(listItem2)
                BindSelectItem(.CheckSigned, moCheckSignedDrop)

                moContractCheckCompleteDrop.PopulateOld("CONTRACT_CHECK_COMPLETE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.ContractCheckComplete, moContractCheckCompleteDrop)

                PopulateControlFromBOProperty(txtBillingDocType, .BillingDocumentType)
                PopulateControlFromBOProperty(txtDealerUpdateReason, .DealerUpdateReason)
                PopulateControlFromBOProperty(moInsuranceOrderNumberText, .InsuranceOrderNumber)
                PopulateControlFromBOProperty(moDeviceOrderNumberText, .DeviceOrderNumber)
                PopulateControlFromBOProperty(moUpgradeTypeText, .UpgradeType)
                PopulateControlFromBOProperty(moPlanTypeText, .PlanType)

                moFulfillmentConsentActionDrop.PopulateOld("CONSENT_ACTION", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.FulfillmentConsentAction, moFulfillmentConsentActionDrop)

                PopulateControlFromBOProperty(moClaimWaitingPeriodEndDateText, .WaitingPeriodEndDate)

                If Not State.MyBO.PreviousCertificateId.Equals(Guid.Empty) Then

                    State.PreviousCertificate = New Certificate(State.MyBO.PreviousCertificateId)
                    linkPrevCertId.Text = State.PreviousCertificate.CertNumber

                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, True)
                    'ControlMgr.SetVisibleControl(Me, linkPrevCertId, True)
                Else
                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, False)
                    'ControlMgr.SetVisibleControl(Me, linkPrevCertId, False)
                End If

                If Not State.MyBO.OriginalCertificateId.Equals(Guid.Empty) Then

                    State.OriginalCertificate = New Certificate(State.MyBO.OriginalCertificateId)
                    linkOrigCertId.Text = State.OriginalCertificate.CertNumber

                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, True)
                    'ControlMgr.SetVisibleControl(Me, linkOrigCertId, True)
                Else
                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, False)
                    'ControlMgr.SetVisibleControl(Me, linkOrigCertId, False)
                End If

                PopulateControlFromBOProperty(moDateOfBirthText, .DateOfBirth)
                DisplayMaskDob()

            End With
        End Sub
        Protected Sub DisplayMaskDob()
            Dim IsDobDisplay As Boolean
            If State.MyBO.Dealer.DisplayDobXcd Is Nothing Then
                IsDobDisplay = False
            Else
                IsDobDisplay = State.MyBO.Dealer.DisplayDobXcd.ToUpper().Equals("YESNO-Y")
            End If

            Dim IsCustomerLglInfo As Boolean = State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0"))

            If (Not IsDobDisplay And IsCustomerLglInfo) Then
                ControlMgr.SetVisibleControl(Me, moDateOfBirthLabel, False)
                ControlMgr.SetVisibleControl(Me, moDateOfBirthText, False)
                ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                ControlMgr.SetVisibleControl(Me, tdDateOfBirthCalTag, False)
            End If

            If (Not IsDobDisplay And Not IsCustomerLglInfo) Then
                If Not (State.IsEdit) Then
                    moDateOfBirthText.Text = State.MyBO.MaskDatePart(moDateOfBirthText.Text, True)
                End If
            End If

            If (IsDobDisplay) Then
                If Not (State.IsEdit) Then
                    moDateOfBirthText.Text = State.MyBO.MaskDatePart(moDateOfBirthText.Text, False)
                End If
            End If
        End Sub

        Protected Sub PopulateCanceDueDateFromForm()
            '  If txtFutureCancelationDateEdit.Text.Trim = String.Empty Then
            PopulateBOProperty(State.TheDirectDebitState.certInstallment, "CancellationDueDate", String.Empty)
            '  End If
        End Sub

        Protected Sub PopulateInstalmenBoFromForm()
            If Not State.MyBO.PaymentTypeId.Equals(Guid.Empty) Then


                Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
                Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

                State.BillingStatusId = State.TheDirectDebitState.certInstallment.BillingStatusId

                'If (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS _
                '    And (Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD _
                '         Or Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT)) _
                '    Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS _
                '        And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) Then
                'If Me.State.directDebitPayment Or Me.State.creditCardPayment Or Me.State.dealerBillPayment Then
                If HasInstallment Then
                    PopulateBOProperty(State.TheDirectDebitState.certInstallment, "BillingStatusId", moBillingStatusId)

                    If CheckBoxSendLetter.Checked = True Then
                        PopulateBOProperty(State.TheDirectDebitState.certInstallment, "SendLetterId", yesId)
                    Else
                        PopulateBOProperty(State.TheDirectDebitState.certInstallment, "SendLetterId", noId)
                    End If

                    PopulateBOProperty(State.TheDirectDebitState.certInstallment, "DateLetterSent", moDateLetterSentText)
                    PopulateBOProperty(State.TheDirectDebitState.certInstallment, "PaymentDueDate", moNextDueDateText)
                    'Req - 1016 Start
                    PopulateBOProperty(State.TheDirectDebitState.certInstallment, "NextBillingDate", moNextBillingDateText)
                    'Req - 1016 End

                    'If (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS _
                    '    And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) Then
                    If State.directDebitPayment Then
                        If IfDirectDebitInfoChanged() Then
                            State.BillingInformationChanged = True
                            State.BankInfoBO = New BankInfo
                            State.BankInfoBO.CopyFrom(State.TheDirectDebitState.bankInfo)
                            State.TheDirectDebitState.bankInfo = State.TheDirectDebitState.certInstallment.AddBankInfo(Guid.Empty)
                            State.TheDirectDebitState.bankInfo.CountryID = State.BankInfoBO.CountryID
                            State.TheDirectDebitState.bankInfo.SourceCountryID = State.BankInfoBO.SourceCountryID
                            PopulateBOProperty(State.TheDirectDebitState.bankInfo, "Account_Name", moBankAccountOwnerText)
                            PopulateBOProperty(State.TheDirectDebitState.bankInfo, "Bank_Id", moBankRoutingNumberText)
                            PopulateBOProperty(State.TheDirectDebitState.bankInfo, "Account_Number", moBankAccountNumberText)
                        End If


                        'ElseIf (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS _
                        '        And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) _
                        '        Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS _
                        '            And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) Then
                    ElseIf State.creditCardPayment Then
                        If IfCreditCardInfoChanged() Then
                            State.BillingInformationChanged = True
                            If State.CreditCardInfoBO Is Nothing Then
                                State.CreditCardInfoBO = New CreditCardInfo
                                State.CreditCardInfoBO.CopyFrom(State.TheDirectDebitState.CreditCardInfo)
                                State.TheDirectDebitState.CreditCardInfo = State.TheDirectDebitState.certInstallment.AddCreditCardInfo(Guid.Empty)
                            End If

                            PopulateBOProperty(State.TheDirectDebitState.CreditCardInfo, "ExpirationDate", moExpirationDateText)
                            PopulateBOProperty(State.TheDirectDebitState.CreditCardInfo, "NameOnCreditCard", moNameOnCreditCardText)
                            PopulateBOProperty(State.TheDirectDebitState.CreditCardInfo, "CreditCardFormatId", moCreditCardTypeIDDropDown)

                            If moCreditCardNumberText.Text = "****" & State.CreditCardInfoBO.Last4Digits Then
                                State.TheDirectDebitState.CreditCardInfo.CreditCardNumber = State.CreditCardInfoBO.CreditCardNumber
                                State.TheDirectDebitState.CreditCardInfo.Last4Digits = State.CreditCardInfoBO.Last4Digits
                            Else
                                CreditCardValidation(moCreditCardNumberText.Text, State.TheDirectDebitState.CreditCardInfo.CreditCardFormatId)
                                PopulateBOProperty(State.TheDirectDebitState.CreditCardInfo, "CreditCardNumber", moCreditCardNumberText)
                                Dim strCC As String = State.TheDirectDebitState.CreditCardInfo.CreditCardNumber.Trim
                                State.TheDirectDebitState.CreditCardInfo.Last4Digits = strCC.Substring(strCC.Length - 4, 4)
                                Try
                                    ' secure the credit card number
                                    State.TheDirectDebitState.CreditCardInfo.Secure()
                                Catch ex As Exception
                                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.PCI_SECURE_ERR, True)
                                    Throw
                                End Try
                            End If
                        End If
                    End If

                    'Validate that the next due date and next billing date are not less than the warranty sales date 
                    If State.TheDirectDebitState.certInstallment.PaymentDueDate.Value.Date < State.MyBO.WarrantySalesDate.Value.Date Then
                        Throw New GUIException(Message.PAYMENT_DUE_DATE_LESS_THAN_WSD, Assurant.ElitaPlus.Common.ErrorCodes.PAYMENT_DUE_DATE_LESS_THAN_WSD)
                    End If

                    If State.TheDirectDebitState.certInstallment.NextBillingDate Is Nothing OrElse State.TheDirectDebitState.certInstallment.NextBillingDate.Value.Date < State.MyBO.WarrantySalesDate.Value.Date Then
                        Throw New GUIException(Message.NEXT_BILLING_DATE_LESS_THAN_WSD, Assurant.ElitaPlus.Common.ErrorCodes.NEXT_BILLING_DATE_LESS_THAN_WSD)
                    End If

                End If
            End If

        End Sub
        Private Sub CreditCardValidation(CreditCardNumber As String, CreditCardFormatId As Guid)
            If String.IsNullOrEmpty(CreditCardNumber) Then
                Throw New GUIException(Message.MSG_CREDIT_CARD_NUMBER_MANDATORY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
            ElseIf CreditCardNumber.Length > 16 Then
                Throw New GUIException(Message.MSG_CREDIT_CARD_NUMBER_MAX_LENGTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CREDIT_CARD_NUMBER_MAX_LENGTH)
            Else
                Dim dv As DataView = LookupListNew.GetCompanyCreditCardsFormatLookupList(Authentication.CurrentUser.LanguageId)
                Dim CreditCardTypeCode As String = LookupListNew.GetCodeFromId(dv, CreditCardFormatId)
                Dim IsCCvalid = CreditCardFormat.IsCreditCardValid(CreditCardTypeCode, CreditCardNumber)
                If Not IsCCvalid Then
                    Throw New GUIException(Message.MSG_CREDIT_CARD_NUMBER_INVALID_FORMAT, Assurant.ElitaPlus.Common.ErrorCodes.WS_INVALID_CREDIT_CARD_NUMBER)
                End If
            End If
        End Sub
        Private Function IfDirectDebitInfoChanged() As Boolean
            With State.TheDirectDebitState.bankInfo
                If .Account_Name = moBankAccountOwnerText.Text AndAlso .Bank_Id = moBankRoutingNumberText.Text AndAlso .Account_Number = moBankAccountNumberText.Text Then
                    Return False
                Else
                    Return True
                End If
            End With

        End Function
        Private Function IfCreditCardInfoChanged() As Boolean
            With State.TheDirectDebitState.CreditCardInfo
                If .ExpirationDate = moExpirationDateText.Text _
                   AndAlso .NameOnCreditCard = moNameOnCreditCardText.Text _
                   AndAlso .CreditCardFormatId.Equals(GetSelectedItem(moCreditCardTypeIDDropDown)) _
                   AndAlso "****" & .Last4Digits = moCreditCardNumberText.Text Then
                    Return False
                Else
                    Return True
                End If
            End With
        End Function
        Protected Sub PopulateBOsFromForm()
            With State.MyBO
                ' ValFlag()
                Dim blnUpdateZipLocator = State.MyBO.Company.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.COMP_ATTR__UPD_ZIP_LOCATOR).FirstOrDefault

                If blnUpdateZipLocator Is Nothing Then
                    blnUpdateZipLocator = False
                Else
                    blnUpdateZipLocator = IIf(blnUpdateZipLocator.Value = Codes.YESNO_Y, True, False)
                End If

                AddressCtr.PopulateBOFromControl(True, blnUpdateZipLocator)
                If (AddressCtr.MyBO IsNot Nothing AndAlso
                    (AddressCtr.MyBO.IsDeleted = False) AndAlso
                    (AddressCtr.MyBO.IsEmpty = False)) Then
                    State.MyBO.AddressId = AddressCtr.MyBO.Id
                End If

                PopulateBOProperty(State.MyBO, "ServiceStartDate", moServiceStartDateText)
                PopulateBOProperty(State.MyBO, "ServiceID", moServiceIdText)
                PopulateBOProperty(State.MyBO, "DocumentIssueDate", moDocumentIssueDateText)
                PopulateBOProperty(State.MyBO, "DocumentTypeID", cboDocumentTypeId)
                PopulateBOProperty(State.MyBO, "RgNumber", moRGNumberText)
                PopulateBOProperty(State.MyBO, "DocumentAgency", moDocumentAgencyText)
                PopulateBOProperty(State.MyBO, "IdType", moIDTypeText)

                'Me.PopulateBOProperty(Me.State.MyBO, "MfgBeginDate", Me.txtMfgBeginDate)
                'Me.PopulateBOProperty(Me.State.MyBO, "MfgEndDate", Me.txtMfgEndDate)
                'Me.PopulateBOProperty(Me.State.MyBO, "MfgBeginKm", Me.txtMfgBeginKm)
                'Me.PopulateBOProperty(Me.State.MyBO, "MfgEndKm", Me.txtMfgEndKm)

                'If Me.tsHoriz.SelectedIndex = CERT_TAX_ID_TAB Then
                If State.selectedTab = CERT_TAX_ID_TAB Then
                    PopulateBOProperty(State.MyBO, "TaxIDNumb", moNewTaxIdText)
                End If

                PopulateBOProperty(State.MyBO, "CustomerName", moCustomerNameText)
                PopulateBOProperty(State.MyBO, "HomePhone", moHomePhoneText)
                PopulateBOProperty(State.MyBO, "WorkPhone", moWorkPhoneText)
                If moEmailAddressText.Text IsNot Nothing AndAlso moEmailAddressText.Text.Equals(State.EmailIsNull) Then
                    moEmailAddressText.Text = ""
                End If
                PopulateBOProperty(State.MyBO, "Email", moEmailAddressText)
                If State.selectedTab = CERT_DETAIL_TAB Then
                    PopulateBOProperty(State.MyBO, "IdentificationNumber", moTaxIdText)
                End If
                PopulateBOProperty(State.MyBO, "InvoiceNumber", moInvoiceNumberText)
                PopulateBOProperty(State.MyBO, "VehicleLicenseTag", moVehicleLicenseTagText)
                PopulateBOProperty(State.MyBO, "LanguageId", cboLangPref)

                If State.isSalutation Then
                    PopulateBOProperty(State.MyBO, "SalutationId", cboSalutationId)
                End If

                If State.TheItemCoverageState.manufaturerWarranty Then
                    PopulateBOProperty(State.MyBO, "WarrantySalesDate", moWarrantySoldText)
                Else
                    PopulateBOProperty(State.MyBO, "ProductSalesDate", moProductSalesDateText)
                End If

                PopulateBOProperty(State.MyBO, "SalesRepNumber", moSalesRepNumberText)

                PopulateBOProperty(State.MyBO, "MembershipTypeId", cboAccountType)

                PopulateBOProperty(State.MyBO, "MembershipNumber", moAccountNumberText)

                If Not State.TheItemCoverageState.IsRetailer Then
                    PopulateBOProperty(State.MyBO, "Retailer", moRetailerText)
                End If

                'DEF-21659 - START
                'If Not .DateOfBirth Is Nothing Then
                If (State.IsEdit) Then
                    PopulateBOProperty(State.MyBO, "DateOfBirth", moDateOfBirthText)
                End If

                'End If
                'DEF-21659 - END

                'Me.PopulateBOProperty(Me.State.MyBO, "PaymentTypeId", Me.moPaymentTypeId)
                PopulateBOProperty(State.MyBO, "VatNum", moVATNumText)
                PopulateBOProperty(State.MyBO, "Region", moRegionText)

                'AA REQ-910 new fields added BEGIN
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                    PopulateBOProperty(State.MyBO, "IncomeRangeId", cboIncomeRangeId)
                    PopulateBOProperty(State.MyBO, "Occupation", moOccupationText)
                    PopulateBOProperty(State.MyBO, "PoliticallyExposedId", cboPoliticallyExposedId)
                End If
                'AA REQ-910 new fields added END
                'REQ-1255 - AML Regulations - START
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment  
                    PopulateBOProperty(State.MyBO, "MaritalStatus", ddlMaritalStatus)
                    PopulateBOProperty(State.MyBO, "Nationality", ddlNationality)
                    PopulateBOProperty(State.MyBO, "PlaceOfBirth", ddlPlaceOfBirth)
                    If Not .CustomerId.Equals(Guid.Empty) Then
                        PopulateBOProperty(State.MyBO, "CityOfBirth", moCityOfBirthText)
                    End If
                    PopulateBOProperty(State.MyBO, "Gender", ddlGender)
                        PopulateBOProperty(State.MyBO, "PersonTypeId", ddlPersonType)
                        PopulateBOProperty(State.MyBO, "CUIT_CUIL", moCUIT_CUILText)
                    End If
                    'REQ-1255 - AML Regulations - END
                    If Not .CustomerId.Equals(Guid.Empty) Then
                    PopulateBOProperty(State.MyBO, "CustomerFirstName", moCustomerFirstNameText)
                    PopulateBOProperty(State.MyBO, "CustomerMiddleName", moCustomerMiddleNameText)
                    PopulateBOProperty(State.MyBO, "CustomerLastName", moCustomerLastNameText)
                    PopulateBOProperty(State.MyBO, "AlternativeLastName", moAlternativeLastNameText)
                    PopulateBOProperty(State.MyBO, "AlternativeFirstName", moAlternativeFirstNameText)
                    PopulateBOProperty(State.MyBO, "CorporateName", moCorporateNameText)
                End If
            End With
            If State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
                PopulateBOProperty(State.MyBO, "CertificateSigned", moCertificateSigneddrop, False, True)
                PopulateBOProperty(State.MyBO, "SepaMandateSigned", moSEPAMandateSignedDrop, False, True)
                PopulateBOProperty(State.MyBO, "ContractCheckCompleteDate", moContractCheckCompleteDateText)
                PopulateBOProperty(State.MyBO, "ContractCheckComplete", moContractCheckCompleteDrop, False, True)
                PopulateBOProperty(State.MyBO, "CertificateVerificationDate", moCertificateVerificationDateText)
                PopulateBOProperty(State.MyBO, "SepaMandateDate", moSEPAMandateDateText)
                PopulateBOProperty(State.MyBO, "CheckSigned", moCheckSignedDrop, False, True)
                PopulateBOProperty(State.MyBO, "CheckVerificationDate", moCheckVerificationDateText)
            End If

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            State.MyBO.ValFlag = State.ValFlag

        End Sub

        Protected Sub PopulateBankInfoBOFromUserCtr()
            With State.BankInfoBO
                moBankInfoController.PopulateBOFromControl()
            End With
        End Sub
        Protected Sub PopulatePaymentOrderInfoBOFromUserCtr()
            With State.PaymentOrderInfoBO
                moPaymentOrderInfoCtrl.PopulateBOFromControl()
            End With
        End Sub

        Public Sub PupulateCommEntityGrid()
            Dim dv As DataView
            dv = State.MyBO.GetCommissionForEntities(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Date.Today.ToString(SP_DATE_FORMAT))
            If dv.Count = 0 Then
                moCommEntityInformationLine.Attributes("style") = "display: none"
            End If
            moCommEntityGrid.DataSource = dv
            moCommEntityGrid.DataBind()
        End Sub

        Public Sub PopulatePremiumInfoTab()

            Dim dv As DataView
            dv = State.MyBO.PremiumTotals(State.MyBO.Id)

            If dv.Count = 0 Then
                State.isPremiumTAbVisible = False
                Exit Sub
            End If

            PopulateControlFromBOProperty(TextboxCURRENCY_OF_CERT, LookupListNew.GetDescriptionFromId(LookupListNew.LK_CURRENCIES, State.MyBO.CurrencyOfCertId))

            ' Gross Amount received
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED) Then
                PopulateControlFromBOProperty(moGrossAmtReceivedText, 0, DECIMAL_FORMAT)
                State.grossAmtReceived = 0
            Else
                PopulateControlFromBOProperty(moGrossAmtReceivedText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal), DECIMAL_FORMAT)
                State.grossAmtReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)
            End If

            ' Total Premium Written
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_PREMIUM_WRITTEN) Then
                PopulateControlFromBOProperty(moPremiumWrittenText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moPremiumWrittenText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_PREMIUM_WRITTEN), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Original Premium
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_ORIGINAL_PREMIUM) Then
                PopulateControlFromBOProperty(moOriginalPremiumText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moOriginalPremiumText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_ORIGINAL_PREMIUM), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Loss Cost
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_LOSS_COST) Then
                PopulateControlFromBOProperty(moLossCostText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moLossCostText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_LOSS_COST), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Commission
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_COMISSION) Then
                PopulateControlFromBOProperty(moComissionsText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moComissionsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_COMISSION), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Admin expense
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_ADMIN_EXPENSE) Then
                PopulateControlFromBOProperty(moAdminExpensesText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moAdminExpensesText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_ADMIN_EXPENSE), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Marketing Expense
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MARKETING_EXPENSE) Then
                PopulateControlFromBOProperty(moMarketingExpenseText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moMarketingExpenseText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MARKETING_EXPENSE), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Other
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_OTHER) Then
                PopulateControlFromBOProperty(moOtherText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moOtherText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_OTHER), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Sales Tax
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_SALES_TAX) Then
                PopulateControlFromBOProperty(moSalesTaxText, 0, DECIMAL_FORMAT)

            Else
                PopulateControlFromBOProperty(moSalesTaxText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_SALES_TAX), Decimal), DECIMAL_FORMAT)

            End If

            ' Total MTD Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MTD_PAYMENTS) Then
                PopulateControlFromBOProperty(moMTDPaymentsText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moMTDPaymentsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MTD_PAYMENTS), Decimal), DECIMAL_FORMAT)
            End If

            ' Total YTD Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_YTD_PAYMENTS) Then
                PopulateControlFromBOProperty(moYTDPaymentsText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moYTDPaymentsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_YTD_PAYMENTS), Decimal), DECIMAL_FORMAT)
            End If

            ' Total MTD Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MTD_INCOMING_PAYMENTS) Then
                PopulateControlFromBOProperty(moMTDPaymentFromCustText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moMTDPaymentFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MTD_INCOMING_PAYMENTS), Decimal), DECIMAL_FORMAT)
            End If

            ' Total YTD Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_YTD_INCOMING_PAYMENTS) Then
                PopulateControlFromBOProperty(moYTDPaymentFromCustText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moYTDPaymentFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_YTD_INCOMING_PAYMENTS), Decimal), DECIMAL_FORMAT)
            End If

            ' Total Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_INCOMING_PAYMENTS) Then
                PopulateControlFromBOProperty(moPaymentRcvdFromCustText, 0, DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moPaymentRcvdFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_INCOMING_PAYMENTS), Decimal), DECIMAL_FORMAT)
            End If

            Try
                State.TheDirectDebitState.certInstallment = New CertInstallment(State.MyBO.Id, True)
            Catch ex As Exception
                State.TheDirectDebitState.certInstallment = Nothing
            End Try

            If Not State.MyBO.PaymentTypeId.Equals(Guid.Empty) Then
                If HasInstallment AndAlso Not (State.TheDirectDebitState.certInstallment.CreditCardInfoId = Guid.Empty) Then
                    State.creditCardPayment = True
                ElseIf HasInstallment AndAlso Not (State.TheDirectDebitState.certInstallment.BankInfoId = Guid.Empty) Then
                    State.directDebitPayment = True
                ElseIf (State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEALER_BILL) Then
                    State.dealerBillPayment = True
                End If
            End If

            SetSelectedItem(moPaymentTypeId, State.MyBO.PaymentTypeId)

            If State.IsNewBillPayBtnVisible Then
                dv = CollectedPayDetail.getCollectPayTotals(State.MyBO.Id)
                If dv.Count > 0 Then
                    State.oPayColltCount = CType(dv.Table.Rows(0).Item(CollectedPayDetail.CollectPayTotals.COL_DETAIL_COUNT), Integer)
                    If State.oPayColltCount > 0 Then
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                    Else
                        If (State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                        End If
                    End If
                End If
            Else
                dv = PaymentDetail.getPaymentTotals(State.MyBO.Id)
                If dv.Count > 0 Then
                    State.oPaymentCount = CType(dv.Table.Rows(0).Item(PaymentDetail.PaymentTotals.COL_DETAIL_COUNT), Integer)
                    If State.oPaymentCount > 0 Then
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                    End If
                End If
            End If



            If HasInstallment Then
                PopulateBillingStatusDropdown()

                Try
                    If State.directDebitPayment Then State.TheDirectDebitState.bankInfo = State.TheDirectDebitState.certInstallment.BankInfo
                    If State.creditCardPayment Then State.TheDirectDebitState.CreditCardInfo = State.TheDirectDebitState.certInstallment.CreditCardInfo

                    PopulateControlFromBOProperty(moBillingStatusId, State.TheDirectDebitState.certInstallment.BillingStatusId)


                    If State.IsNewBillPayBtnVisible Then

                        State.oBillingTotalAmount = 0
                        'dv = BillingPayDetail.getBillPayTotals(Me.State.MyBO.Id)
                        dv = BillingPayDetail.getBillPayTotalsNew(State.MyBO.Id)

                        If dv IsNot Nothing AndAlso dv.Table IsNot Nothing AndAlso dv.Table.Rows.Count > 0 AndAlso dv.Table.Rows(0)(installments_Collected) IsNot Nothing AndAlso Not dv.Table.Rows(0)(installments_Collected).Equals(System.DBNull.Value) Then
                            State.oBillPayCount = dv.Table.Rows(0)(installments_Collected)
                        Else
                            State.oBillPayCount = 0
                        End If

                        If dv IsNot Nothing AndAlso dv.Table IsNot Nothing AndAlso dv.Table.Rows.Count > 0 AndAlso dv.Table.Rows(0)(Amount_Collected) IsNot Nothing AndAlso Not dv.Table.Rows(0)(Amount_Collected).Equals(System.DBNull.Value) Then
                            State.oBillingTotalAmount = dv.Table.Rows(0)(Amount_Collected)
                        Else
                            State.oBillingTotalAmount = 0
                        End If
                        PopulateControlFromBOProperty(moBalanceRemainingText, dv.Table.Rows(0)(remaining_amount))
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentText, dv.Table.Rows(0)(Total_installments)) 'remaining_amount
                        moNumberOfInstallmentText.Text = String.Empty
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentText, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentCollectedLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentCollectedText, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentRemainingLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentRemainingLText, False)


                    Else
                        State.oBillingTotalAmount = 0
                        dv = BillingDetail.getBillingTotals(State.MyBO.Id)
                        State.oBillingCount = 0

                        If (Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_DETAIL_COUNT)) AndAlso CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer) > 0 Then
                            If Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL) Then
                                State.oBillingTotalAmount = CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                            Else
                                State.oBillingTotalAmount = 0
                            End If
                            If State.TheDirectDebitState.certInstallment.InstallmentAmount.Value = 0 Then
                                If Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_DETAIL_COUNT) Then
                                    State.oBillingCount = CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer)
                                Else
                                    State.oBillingCount = 0
                                End If
                            Else
                                State.oBillingCount = CType(Decimal.Round(Decimal.Divide(State.oBillingTotalAmount, State.TheDirectDebitState.certInstallment.InstallmentAmount.Value)), Integer)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                        End If
                        PopulateControlFromBOProperty(moBalanceRemainingText, State.grossAmtReceived - State.oBillingTotalAmount)
                        PopulateControlFromBOProperty(moNumberOfInstallmentText, CType(State.TheDirectDebitState.certInstallment.NumberOfInstallments, Integer))
                        moNumberOfInstallmentRemainingLabel.Text = moNumberOfInstallmentRemainingLabel.Text & ":"
                        moNumberOfInstallmentLabel.Text = moNumberOfInstallmentLabel.Text & ":"
                        moNumberOfInstallmentCollectedLabel.Text = moNumberOfInstallmentCollectedLabel.Text & ":"

                    End If

                    If State.TheDirectDebitState.certInstallment.CancellationDueDate IsNot Nothing Then
                        If Not State.TheDirectDebitState.certInstallment.CancellationDueDate.Value < Date.Today Then
                            ControlMgr.SetVisibleControl(Me, lblFutureCancelationDate, True)
                            ControlMgr.SetVisibleControl(Me, txtFutureCancelationDate, True)
                            ControlMgr.SetVisibleControl(Me, btnRemoveCancelDueDate_WRITE, True)
                            If Not lblFutureCancelationDate.Text.EndsWith(":") Then
                                lblFutureCancelationDate.Text &= ":"
                            End If
                            PopulateControlFromBOProperty(txtFutureCancelationDate, State.TheDirectDebitState.certInstallment.CancellationDueDate)
                        End If
                    Else
                        ControlMgr.SetVisibleControl(Me, lblFutureCancelationDate, False)
                        ControlMgr.SetVisibleControl(Me, txtFutureCancelationDate, False)
                        ControlMgr.SetVisibleControl(Me, btnRemoveCancelDueDate_WRITE, False)
                    End If





                    PopulateControlFromBOProperty(moTotalAmountCollectedText, State.oBillingTotalAmount)

                    'REQ-5761 Defect 29135
                    If State.IsNewBillPayBtnVisible Then
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentCollectedText, Me.State.oBillPayCount)
                        moNumberOfInstallmentCollectedText.Text = String.Empty
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentRemainingLText, dv.Table.Rows(0)(Remaining_Installments))
                        moNumberOfInstallmentRemainingLText.Text = String.Empty 'CType(Me.moNumberOfInstallmentRemainingLText.Text, Integer)
                    Else
                        PopulateControlFromBOProperty(moNumberOfInstallmentCollectedText, State.oBillingCount)
                        PopulateControlFromBOProperty(moNumberOfInstallmentRemainingLText, State.TheDirectDebitState.certInstallment.NumberOfInstallments.Value - State.oBillingCount)

                    End If

                    PopulateControlFromBOProperty(moInstallAmountText, State.TheDirectDebitState.certInstallment.InstallmentAmount)
                    PopulateControlFromBOProperty(moNextDueDateText, State.TheDirectDebitState.certInstallment.PaymentDueDate)
                    'Req - 1016 Start
                    PopulateControlFromBOProperty(moNextBillingDateText, State.TheDirectDebitState.certInstallment.NextBillingDate)
                    'Req - 1016 End
                    PopulateControlFromBOProperty(moDateLetterSentText, State.TheDirectDebitState.certInstallment.DateLetterSent)

                    If State.directDebitPayment Then
                        PopulateControlFromBOProperty(moBankAccountNumberText, State.TheDirectDebitState.bankInfo.Account_Number)
                        PopulateControlFromBOProperty(moBankRoutingNumberText, State.TheDirectDebitState.bankInfo.Bank_Id)
                        PopulateControlFromBOProperty(moBankAccountOwnerText, State.TheDirectDebitState.bankInfo.Account_Name)
                        moCreditCardInformation1.Attributes("style") = "display: none"
                        moCreditCardInformation2.Attributes("style") = "display: none"
                    ElseIf State.creditCardPayment Then
                        Dim strCreditCardNumber As String = "****" & State.TheDirectDebitState.CreditCardInfo.Last4Digits
                        PopulateControlFromBOProperty(moCreditCardNumberText, strCreditCardNumber)
                        SetSelectedItem(moCreditCardTypeIDDropDown, State.TheDirectDebitState.CreditCardInfo.CreditCardFormatId)
                        PopulateControlFromBOProperty(moExpirationDateText, State.TheDirectDebitState.CreditCardInfo.ExpirationDate)
                        PopulateControlFromBOProperty(moNameOnCreditCardText, State.TheDirectDebitState.CreditCardInfo.NameOnCreditCard)
                        moDirectDebitInformation6.Attributes("style") = "display: none"
                        moDirectDebitInformation7.Attributes("style") = "display: none"
                    Else
                        moCreditCardInformation1.Attributes("style") = "display: none"
                        moCreditCardInformation2.Attributes("style") = "display: none"
                        moDirectDebitInformation6.Attributes("style") = "display: none"
                        moDirectDebitInformation7.Attributes("style") = "display: none"
                    End If

                    If State.MyBO.StatusCode = CERT_STATUS Then
                        PupulateCommEntityGrid()
                    Else
                        moCommEntityInformation.Attributes("style") = "display: none"
                        moCommEntityInformationLine.Attributes("style") = "display: none"
                    End If

                    Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

                    CheckBoxSendLetter.Checked = False
                    If State.TheDirectDebitState.certInstallment.SendLetterId = yesId Then
                        CheckBoxSendLetter.Checked = True
                    End If

                Catch ex As ElitaPlusException

                    If ex.GetType Is GetType(DataNotFoundException) Then

                        moCreditCardInformation1.Attributes("style") = "display: none"
                        moCreditCardInformation2.Attributes("style") = "display: none"

                        moDirectDebitInformation1.Attributes("style") = "display: none"
                        moDirectDebitInformation2.Attributes("style") = "display: none"
                        moDirectDebitInformation3.Attributes("style") = "display: none"
                        moDirectDebitInformation4.Attributes("style") = "display: none"
                        'Req-1016 Start
                        moDirectDebitInformation4A.Attributes("style") = "display: none"
                        'Req-1016 End
                        moDirectDebitInformation5.Attributes("style") = "display: none"
                        moDirectDebitInformation5A.Attributes("style") = "display: none"
                        moDirectDebitInformation6.Attributes("style") = "display: none"
                        moDirectDebitInformation7.Attributes("style") = "display: none"

                        moCommEntityInformation.Attributes("style") = "display: none"
                        moCommEntityInformationLine.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, btnDebitSave_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnUndoDebit_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnDebitEdit_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnDebitHistory, False)
                        If (State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetVisibleControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetVisibleControl(Me, btnBillPayHist, False)
                        End If
                    End If

                End Try

            Else

                moCreditCardInformation1.Attributes("style") = "display: none"
                moCreditCardInformation2.Attributes("style") = "display: none"

                moDirectDebitInformation1.Attributes("style") = "display: none"
                moDirectDebitInformation2.Attributes("style") = "display: none"
                moDirectDebitInformation3.Attributes("style") = "display: none"
                moDirectDebitInformation4.Attributes("style") = "display: none"
                'Req-1016 Start
                moDirectDebitInformation4A.Attributes("style") = "display: none"
                'Req-1016 End
                moDirectDebitInformation5.Attributes("style") = "display: none"
                moDirectDebitInformation5A.Attributes("style") = "display: none"
                moDirectDebitInformation6.Attributes("style") = "display: none"
                moDirectDebitInformation7.Attributes("style") = "display: none"

                moCommEntityInformation.Attributes("style") = "display: none"
                moCommEntityInformationLine.Attributes("style") = "display: none"

                ControlMgr.SetVisibleControl(Me, btnDebitSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnUndoDebit_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnDebitEdit_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnDebitHistory, False)
            End If

            Dim oContract As New Contract
            oContract = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value)

            If oContract Is Nothing Then
                Throw New GUIException(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
            Else
                If (oContract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value).InsPremiumFactor) Is Nothing Then
                    State.InsPremiumFactor = 0
                    moCustPaymentInfo1.Attributes("style") = "display: none"
                    moCustPaymentInfo2.Attributes("style") = "display: none"
                Else
                    State.InsPremiumFactor = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value).InsPremiumFactor.Value
                    If State.InsPremiumFactor <= 0 Then
                        moCustPaymentInfo1.Attributes("style") = "display: none"
                        moCustPaymentInfo2.Attributes("style") = "display: none"
                    End If
                End If
            End If
        End Sub

        Public Sub PopulateCancellationInfoTab()

            Dim TodayDate As Date
            Dim computationMethod As String
            Dim computationMethodCode As String
            Dim issuedTo As String
            Dim PaymentMethod, PaymentReason, AcctStatus As String
            Dim policyCost As Decimal = 0
            Dim costRetained As Decimal = 0
            Dim certCancellationId As Guid
            Dim oCertCancellation As CertCancellation

            If (State.MyBO.TheCertCancellationBO IsNot Nothing) Then
                With State.MyBO.TheCertCancellationBO
                    PopulateControlFromBOProperty(moCancellationReasonTextbox, .getCancellationReasonDescription)
                    PopulateControlFromBOProperty(moCancellationDateTextbox, .CancellationDate)
                    PopulateControlFromBOProperty(moSourceTextbox, .Source)
                    PopulateControlFromBOProperty(moProcessedDateTextbox, .ProcessedDate)
                    PopulateControlFromBOProperty(moOriginalStateProvinceTextbox, .getRegionDescription)

                    Dim dv As DataView = LookupListNew.GetListItemId(State.MyBO.TheCertCancellationBO.getRefundComputeMethodId, Authentication.LangId)
                    computationMethod = dv.Item(FIRST_ROW).Item(DESCRIPTION).ToString
                    computationMethodCode = dv.Item(FIRST_ROW).Item(CODE).ToString
                    PopulateControlFromBOProperty(moRefundMethodMeaningTextbox, computationMethod)
                    policyCost = .getPolicyCost
                    PopulateControlFromBOProperty(moPolicyCostTextbox, policyCost, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(moComputedRefundTextbox, .ComputedRefund)


                    Dim CompRefund As Decimal

                    If .ComputedRefund Is Nothing Then
                        CompRefund = 0
                    Else
                        CompRefund = .ComputedRefund.Value
                    End If

                    If CompRefund = 0 Then
                        ControlMgr.SetVisibleControl(Me, moCostRetainedLabel, False)
                        ControlMgr.SetVisibleControl(Me, moCostRetainedTextbox, False)
                    Else
                        If moCostRetainedLabel.Text.IndexOf(":") <= 0 Then
                            moCostRetainedLabel.Text = moCostRetainedLabel.Text & ":"
                        End If
                        ControlMgr.SetVisibleControl(Me, moCostRetainedLabel, True)
                        ControlMgr.SetVisibleControl(Me, moCostRetainedTextbox, True)
                        costRetained = policyCost - .ComputedRefund.Value
                        PopulateControlFromBOProperty(moCostRetainedTextbox, costRetained)
                    End If
                    PopulateControlFromBOProperty(moRefundAmtTextbox, .RefundAmt)

                    If .CreditIssuedFlag = NO Then
                        ControlMgr.SetVisibleControl(Me, moIssuedToLabel, False)
                        ControlMgr.SetVisibleControl(Me, moIssuedToTextbox, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, moIssuedToLabel, True)
                        ControlMgr.SetVisibleControl(Me, moIssuedToTextbox, True)
                        If moIssuedToLabel.Text.IndexOf(":") > 0 Then
                            moIssuedToLabel.Text = moIssuedToLabel.Text
                        Else
                            moIssuedToLabel.Text = moIssuedToLabel.Text & ":"
                        End If
                        Dim dv2 As DataView = LookupListNew.GetListItemId(State.MyBO.TheCertCancellationBO.RefundDestId, Authentication.LangId)
                        issuedTo = dv2.Item(FIRST_ROW).Item(DESCRIPTION).ToString
                        PopulateControlFromBOProperty(moIssuedToTextbox, issuedTo)
                    End If

                    If (State.MyBO.TheCertCancellationBO.PaymentMethodId.Equals(Guid.Empty)) Then
                        ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, False)
                        ControlMgr.SetVisibleControl(Me, moPaymentMethodTextbox, False)
                        ControlMgr.SetVisibleControl(Me, moBankInfoLabel, False)
                        moCancPaymentOrderInfoCtrl.Visible = False
                        moCancBankInfoController.Visible = False
                    Else
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_DESTINATION, State.MyBO.TheCertCancellationBO.RefundDestId) = Codes.REFUND_DESTINATION__CUSTOMER AndAlso State.MyBO.TheCertCancellationBO.RefundAmt.Value > 0 Then
                            If moPaymentMethodLabel.Text.IndexOf(":") > 0 Then
                                moPaymentMethodLabel.Text = moPaymentMethodLabel.Text
                            Else
                                moPaymentMethodLabel.Text = moPaymentMethodLabel.Text & ":"
                            End If
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodTextbox, True)
                            PaymentMethod = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.TheCertCancellationBO.PaymentMethodId)
                            PopulateControlFromBOProperty(moPaymentMethodTextbox, PaymentMethod)
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.TheCertCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                                State.CancBankInfoBO = State.MyBO.TheCertCancellationBO.bankinfo
                                State.CancBankInfoBO.SourceCountryID = State.CancBankInfoBO.CountryID
                                moCancBankInfoController.Bind(State.CancBankInfoBO)
                                moCancBankInfoController.DisableAllFields()
                                moCancBankInfoController.Visible = True
                                moCancPaymentOrderInfoCtrl.Visible = False
                            ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.TheCertCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                                State.CancPaymentOrderInfoBO = State.MyBO.TheCertCancellationBO.PmtOrderinfo(True)
                                moCancPaymentOrderInfoCtrl.Bind(State.CancPaymentOrderInfoBO)
                                moCancPaymentOrderInfoCtrl.DisableAllFields()
                                moCancPaymentOrderInfoCtrl.Visible = True
                                moCancBankInfoController.Visible = False
                            Else
                                moCancPaymentOrderInfoCtrl.Visible = False
                                moCancBankInfoController.Visible = False
                            End If
                        Else
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, False)
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodTextbox, False)
                        End If
                    End If

                    If .GrossAmtReceived Is Nothing Then
                        .GrossAmtReceived = 0
                    Else
                        .GrossAmtReceived = .GrossAmtReceived.Value
                    End If

                    PopulateControlFromBOProperty(moCanGrossAmtReceivedTextbox, .GrossAmtReceived.Value, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(moCanOriginalPremiumTextbox, .OriginalPremium)
                    PopulateControlFromBOProperty(moCanPremiumWrittenTextbox, .PremiumWritten)
                    PopulateControlFromBOProperty(moCanLossCostTextbox, .LossCost)
                    PopulateControlFromBOProperty(moCanComissionsTextbox, .Commission)
                    PopulateControlFromBOProperty(moCanAdminExpensesTextbox, .AdminExpense)
                    PopulateControlFromBOProperty(moCanMarketingExpensesTextbox, .MarketingExpense)
                    PopulateControlFromBOProperty(moCanOtherTextbox, .Other)
                    PopulateControlFromBOProperty(moCanSalesTaxTextbox, .SalesTax)

                    'following 3 fields added for showing the status information of accounting - Felita to elita passback WR
                    AcctStatus = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCTSTATUS, State.MyBO.TheCertCancellationBO.StatusId)
                    PopulateControlFromBOProperty(moAcctStatusTextbox, AcctStatus)
                    PopulateControlFromBOProperty(moAcctStatusDateTextbox, .StatusDate)
                    PopulateControlFromBOProperty(moAcctTrackNumTextbox, .TrackingNumber)
                    Dim strRefundMethod As String = LookupListNew.GetDescriptionFromExtCode(LookupListNew.LK_CRST, ElitaPlusIdentity.Current.ActiveUser.LanguageId, .RefundStatus)

                    PopulateControlFromBOProperty(moRefundRejectCodeText, .PayRejectCode)
                    PopulateControlFromBOProperty(moRefundRejectStatusText, strRefundMethod)

                    If Not .BankInfoId.Equals(Guid.Empty) Then
                        State.RefundBankInfoBO = New BankInfo(.BankInfoId)
                        PopulateControlFromBOProperty(moRfAccountNumberText, State.RefundBankInfoBO.Account_Number_Last4Digits)
                        PopulateControlFromBOProperty(moRfIBANNumberText, State.RefundBankInfoBO.IbanNumberLast4Digits)
                    End If

                    If .RefundMethod = REFUND_METHOD_SEPA And (.PayRejectCode <> String.Empty Or .RefundStatus = Codes.REFUND_STATUS_PENDING) Then
                        ControlMgr.SetEnableControl(Me, moRfIBANNumberText, True)
                        ControlMgr.SetEnableControl(Me, moRfAccountNumberText, True)
                        ControlMgr.SetVisibleControl(Me, UpdateBankInfoButton_WRITE, True)
                    Else
                        ControlMgr.SetEnableControl(Me, moRfIBANNumberText, False)
                        ControlMgr.SetEnableControl(Me, moRfAccountNumberText, False)
                        ControlMgr.SetVisibleControl(Me, UpdateBankInfoButton_WRITE, False)
                    End If

                End With
            End If
            'Call the Common Logic to Enable Reverse Cancellation/Reinstatement
            If State.MyBO.IsReverseCancellationEnabled(State.MyBO.Id) Then
                If State.MyBO.IsChildCertificate Then
                    ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, True)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, False)
            End If

        End Sub


#Region "DropDowns"

        Private Sub PopulateSalutationDropdown(salutationDropDownList As DropDownList)

            Try
                Dim salutation As ListItem() = CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode())
                salutationDropDownList.Populate(salutation, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateMembershipTypeDropdown(membershipTypeDropDownList As DropDownList)

            Try
                Dim membershipType As ListItem() = CommonConfigManager.Current.ListManager.GetList("MEMTYPE", Thread.CurrentPrincipal.GetLanguageCode())

                membershipTypeDropDownList.Populate(membershipType, New PopulateOptions() With
                                                       {
                                                       .AddBlankItem = True
                                                       })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateLangPrefDropdown(langPrefDropDownList As DropDownList)

            Try

                Dim langList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
                langPrefDropDownList.Populate(langList, New PopulateOptions() With
                                                 {
                                                 .AddBlankItem = True
                                                 })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateCreditCardTypesDropdown(cboCreditCardTypesDropDownList As DropDownList)

            Try

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CompanyId = State.MyBO.CompanyId
                Dim creditCardTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CreditCardByCompany", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                cboCreditCardTypesDropDownList.Populate(creditCardTypeList, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulatePaymentTypesDropdown(cboPaymentTypesTypesDropDownList As DropDownList)

            Try

                'Dim PaymentTypesDV As DataView = LookupListNew.GetPaymentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
                'Me.BindListControlToDataView(cboPaymentTypesTypesDropDownList, PaymentTypesDV, "DESCRIPTION", "ID", True)

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId
                Dim paymentTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PaymentTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                cboPaymentTypesTypesDropDownList.Populate(paymentTypeList, New PopulateOptions() With
                                                             {
                                                             .AddBlankItem = True
                                                             })

                ' populate payment instrument list
                Dim paymentInstrumentList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTINSTR", Thread.CurrentPrincipal.GetLanguageCode())
                moPaymentInstrument.Populate(paymentInstrumentList, New PopulateOptions() With
                                                {
                                                .AddBlankItem = True,
                                                .BlankItemValue = "0",
                                                .ValueFunc = AddressOf PopulateOptions.GetCode,
                                                .SortFunc = AddressOf PopulateOptions.GetCode
                                                })


                SetSelectedItem(moPaymentInstrument, State.MyBO.getPaymentInstrumentCode)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateDocumentTypeDropdown(documentTypeDropDownlist As DropDownList)

            Try
                Dim documentType As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                documentTypeDropDownlist.Populate(documentType, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True
                                                     })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateCancellationReasonDropdown(cancellationReasonDropDownList As DropDownList, filtercondition As String, defaultDescription As String)

            Try

                Dim listcontext As ListContext = New ListContext()
                listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                listcontext.CompanyId = State.MyBO.CompanyId
                Dim cancellationReasolist As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByRole", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cancellationReasonDropDownList.Populate(cancellationReasolist, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })

                BindSelectItemByText(defaultDescription, cancellationReasonDropDownList)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateCancelRequestReasonDropdown(cancellationReasonDropDownList As DropDownList, filtercondition As String)

            Try
                'Dim cancellationReasonDV As DataView = LookupListNew.GetCancellationReasonWithCodeLookupList(Me.State.MyBO.CompanyId)
                'If Not filtercondition Is Nothing Then cancellationReasonDV.RowFilter = filtercondition
                'BindListControlToDataView(cancellationReasonDropDownList, cancellationReasonDV, "DESCRIPTION", "ID", True)

                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyId = State.MyBO.CompanyId
                Dim cancellationRequestReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                Dim filteredCancellationReasonList As ListItem()
                If State.CancelRulesForSFR = Codes.YESNO_Y Then
                    filteredCancellationReasonList = (From x In cancellationRequestReason
                                                      Where x.Translation.Contains("SFR")
                                                      Select x).ToArray()
                Else
                    filteredCancellationReasonList = cancellationRequestReason.ToArray()
                End If

                cancellationReasonDropDownList.Populate(filteredCancellationReasonList, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True,
                                                           .TextFunc = Function(x)
                                                                           Return x.Code + " - " + x.Translation
                                                                       End Function
                                                           })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function FilterCancellationReasonDropdown(CancellationReasonDV As DataView)
            If (CancellationReasonDV.Count > 0) Then
                Dim strRowFilter As String = CancellationReasonDV.RowFilter

                If State.CancelRulesForSFR = Codes.YESNO_Y Then
                    strRowFilter = "Description Like '%SFR%'"
                Else
                    strRowFilter = "Description not like '%SFR%'"

                End If
                If (Not String.IsNullOrWhiteSpace(CancellationReasonDV.RowFilter)) Then
                    CancellationReasonDV.RowFilter = CancellationReasonDV.RowFilter & "and " & strRowFilter
                Else
                    CancellationReasonDV.RowFilter = strRowFilter
                End If

                Return CancellationReasonDV

            End If

        End Function

        Private Sub PopulateCancelCommentTypeDropdown(CancelCommentTypeDropDownList As DropDownList)

            Try
                Dim cancelComment As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMT", Thread.CurrentPrincipal.GetLanguageCode())
                CancelCommentTypeDropDownList.Populate(cancelComment, New PopulateOptions() With
                                                          {
                                                          .AddBlankItem = True,
                                                          .TextFunc = Function(x)
                                                                          Return x.Code + " - " + x.Translation
                                                                      End Function
                                                          })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub PopulatePaymentMethodDropdown(PaymentMethodDropDownList As DropDownList)

            Try

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                oListContext.CompanyId = State.MyBO.CompanyId
                oListContext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                Dim paymentMethodList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.PaymentMethodByRoleCompany, Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                PaymentMethodDropDownList.Populate(paymentMethodList, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True,
                                                      .SortFunc = AddressOf PopulateOptions.GetDescription
                                                      })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateMaritalStatusDropdown(MaritalStatusDropDownList As DropDownList)
            Try

                Dim maritalStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("MARITAL_STATUS", Thread.CurrentPrincipal.GetLanguageCode())
                MaritalStatusDropDownList.Populate(maritalStatus, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True
                                                      })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateNationalityDropdown(nationalityDropDownList As DropDownList)
            Try
                Dim nationalities As ListItem() = CommonConfigManager.Current.ListManager.GetList("NATIONALITY", Thread.CurrentPrincipal.GetLanguageCode())
                nationalityDropDownList.Populate(nationalities, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePlaceOfBirthDropdown(PlaceOfBirthDropDownList As DropDownList)
            Try

                Dim placeOfBirth As ListItem() = CommonConfigManager.Current.ListManager.GetList("PLACEOFBIRTH", Thread.CurrentPrincipal.GetLanguageCode())
                PlaceOfBirthDropDownList.Populate(placeOfBirth, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True
                                                     })

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGenderDropdown(GenderDropDownList As DropDownList)
            Try

                Dim gender As ListItem() = CommonConfigManager.Current.ListManager.GetList("GENDER", Thread.CurrentPrincipal.GetLanguageCode())
                GenderDropDownList.Populate(gender, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePersonTypeDropdown(PersonTypeDropDownList As DropDownList)
            Try
                Dim personType As ListItem() = CommonConfigManager.Current.ListManager.GetList("PERSON_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
                PersonTypeDropDownList.Populate(personType, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateIncomeRangeDropdown(IncomeRangeDropDownList As DropDownList)
            Try
                Dim incomeRange As ListItem() = CommonConfigManager.Current.ListManager.GetList("IRNG", Thread.CurrentPrincipal.GetLanguageCode())

                IncomeRangeDropDownList.Populate(incomeRange, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePoliticallyExposedDropdown(PoliticallyExposedDropDownList As DropDownList)
            Try
                Dim politicallyExposed As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())

                PoliticallyExposedDropDownList.Populate(politicallyExposed, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateBillingStatusDropdown()
            Try
                Dim billingStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("BLST", Thread.CurrentPrincipal.GetLanguageCode())

                moBillingStatusId.Populate(billingStatus, New PopulateOptions() With
                                              {
                                              .AddBlankItem = True
                                              })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

        Public Sub populateFormFromCertCancellationBO()

            If State.certCancellationBO IsNot Nothing AndAlso State.certCancellationBO.CancellationRequestedDate IsNot Nothing Then
                CancelCertReqDateTextbox.Text = GetDateFormattedStringNullable(State.certCancellationBO.CancellationRequestedDate.Value)
            Else
                CancelCertReqDateTextbox.Text = GetDateFormattedStringNullable(System.DateTime.Today)
            End If
            AddCalendar(CancelCertReqDateImageButton, CancelCertReqDateTextbox)

            If State.certCancellationBO IsNot Nothing AndAlso State.certCancellationBO.CancellationDate IsNot Nothing Then
                CancelCertDateTextbox.Text = GetDateFormattedStringNullable(State.certCancellationBO.CancellationDate.Value)
            Else
                If Not State.MyBO.PreviousCertificateId.Equals(Guid.Empty) And State.MyBO.WarrantySalesDate.Value > System.DateTime.Today Then
                    CancelCertDateTextbox.Text = GetDateFormattedStringNullable(State.MyBO.WarrantySalesDate.Value)
                    ControlMgr.SetEnableControl(Me, CancelCertDateTextbox, False)
                    ControlMgr.SetVisibleControl(Me, CancelCertDateImagebutton, False)
                Else
                    CancelCertDateTextbox.Text = GetDateFormattedString(System.DateTime.Today)
                End If
            End If
            AddCalendar(CancelCertDateImagebutton, CancelCertDateTextbox)

        End Sub

        Public Sub ClearCommentsControls()
            moCallerNameTextBox.Text = String.Empty
            moCommentsTextbox.Text = String.Empty
        End Sub
        Public Sub PopulateCancellationDate()
            Dim iFullRefundDays As Integer
            Dim oContract As New Contract, dtCancellationDate As Date
            If State.CancelRulesForSFR = Codes.YESNO_Y Then

                If moCancelRequestDateTextBox.Text <> String.Empty Then
                    If Not IsDate(moCancelRequestDateTextBox.Text) Then
                        ElitaPlusPage.SetLabelError(moCancelRequestDateLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE)
                    Else
                        ClearLabelError(moCancelRequestDateLabel)
                        If IsNothing(State.certCancelRequestBO) Then
                            State.certCancelRequestBO = New CertCancelRequest
                        End If
                        PopulateBOProperty(State.certCancelRequestBO, "CancellationRequestDate", moCancelRequestDateTextBox)

                        If (State.certCancelRequestBO.CancellationRequestDate.Value > DateTime.Today) Then
                            ElitaPlusPage.SetLabelError(moCancelRequestDateLabel)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE)
                        End If

                        oContract = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value)
                        iFullRefundDays = oContract.FullRefundDays
                        State.CertTerm = State.MyBO.getCertTerm
                        If Not GetSelectedItem(moCancelRequestReasonDrop).Equals(Guid.Empty) Then
                            Dim oCancellationReason As New CancellationReason(GetSelectedItem(moCancelRequestReasonDrop))
                            State.CancReasonIsLawful = oCancellationReason.IsLawful
                            State.RefundComputeMethod = LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_COMP_METHOD, oCancellationReason.RefundComputeMethodId)
                            State.CancReasonCode = oCancellationReason.Code
                        End If

                        'Incase of Full Refund Computation method assign cancellation request date as Cancellation Date
                        If State.RefundComputeMethod = Codes.REFUND_COMPUTE_METHOD__1 Then
                            dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                        Else

                            If DateDiff("D", State.MyBO.WarrantySalesDate.Value, State.certCancelRequestBO.CancellationRequestDate.Value) <= iFullRefundDays Then
                                dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                            Else
                                If State.CancReasonIsLawful = Codes.EXT_YESNO_N Then
                                    If State.certCancelRequestBO.CancellationRequestDate.Value < DateAdd("M", State.CertTerm, State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", -1, DateAdd("M", State.CertTerm, State.MyBO.WarrantySalesDate.Value))
                                    Else
                                        dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                                    End If
                                ElseIf State.CancReasonIsLawful = Codes.EXT_YESNO_Y Then
                                    If State.CancReasonCode = Codes.SFR_CR_CHATELLAW And State.certCancelRequestBO.CancellationRequestDate.Value < DateAdd("M", State.CertTerm, State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", -1, DateAdd("M", State.CertTerm, State.MyBO.WarrantySalesDate.Value))
                                    ElseIf State.CancReasonCode = Codes.SFR_CR_HAMONLAW And State.certCancelRequestBO.CancellationRequestDate.Value > DateAdd("M", State.CertTerm, State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", +30, State.certCancelRequestBO.CancellationRequestDate.Value)
                                    Else
                                        dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                                    End If
                                End If
                            End If
                        End If
                    End If
                    PopulateControlFromBOProperty(moCancelDateTextBox, dtCancellationDate)
                End If
            End If
        End Sub
        Public Sub populateCancelRequestInfoTab()
            Try

                PopulateCancelRequestReasonDropdown(moCancelRequestReasonDrop, Nothing)
                PopulateCancelCommentTypeDropdown(moCancelRequestJustificationDrop)
                moProofOfDocumentationDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                moUseExistingBankDetailsDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

                State.CertInstalBankInfoId = State.MyBO.getCertInstalBankInfoID

                If State.CertCancelRequestId.Equals(Guid.Empty) Then
                    ''status
                    ControlMgr.SetVisibleControl(Me, moCancelRequestStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCancelRequestStatusText, False)

                    If State.IsEdit = True Then
                        If State.CancelRulesForSFR = Codes.YESNO_Y Then
                            moCancelDateTextBox.Text = String.Empty
                            moCancelRequestDateTextBox.Text = String.Empty
                        Else
                            moCancelDateTextBox.Text = GetDateFormattedString(System.DateTime.Now)
                            moCancelRequestDateTextBox.Text = GetDateFormattedString(System.DateTime.Now)
                        End If
                    Else
                        moCancelDateTextBox.Text = String.Empty
                        moCancelRequestDateTextBox.Text = String.Empty
                        moCancelRequestReasonDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        moCancelRequestJustificationDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        moProofOfDocumentationDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        moUseExistingBankDetailsDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        moCRIBANNumberText.Text = String.Empty
                        If State.CancelRulesForSFR = Codes.YESNO_Y Then
                            ControlMgr.SetEnableControl(Me, moCancelDateTextBox, False)
                            ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
                        End If
                    End If
                    If Not State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, True)
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, True)
                        If State.IsEdit = True Then
                            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, True)
                            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsLabel, True)
                        Else
                            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)
                            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsLabel, False)
                        End If
                    Else
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, False)
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, False)
                    End If

                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
                Else
                    'If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
                    '    ControlMgr.SetEnableControl(Me, moCancelDateTextBox, False)
                    '    ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
                    'End If
                    With State.certCancelRequestBO
                        PopulateControlFromBOProperty(moCancelRequestReasonDrop, .CancellationReasonId)
                        PopulateControlFromBOProperty(moCancelDateTextBox, .CancellationDate)
                        PopulateControlFromBOProperty(moCancelRequestDateTextBox, .CancellationRequestDate)
                        ''status
                        ControlMgr.SetVisibleControl(Me, moCancelRequestStatusLabel, True)
                        ControlMgr.SetVisibleControl(Me, moCancelRequestStatusText, True)
                        PopulateControlFromBOProperty(moCancelRequestStatusText, .StatusDescription)

                        If (State.CancelRulesForSFR = Codes.YESNO_Y) Then

                            If .ProofOfDocumentation IsNot Nothing AndAlso Not String.IsNullOrEmpty(.ProofOfDocumentation) Then
                                BindSelectItem(.ProofOfDocumentation.ToString, moProofOfDocumentationDrop)
                            End If
                            If State.CancReasonIsLawful Is Nothing Then
                                Dim oCancellationReason As New CancellationReason(.CancellationReasonId)
                                State.CancReasonIsLawful = oCancellationReason.IsLawful
                                State.CancReasonCode = oCancellationReason.Code
                            End If
                            If Not State.certCancelRequestBO.BankInfoId.Equals(Guid.Empty) Then
                                If State.CertInstalBankInfoId.Equals(State.certCancelRequestBO.BankInfoId) Then
                                    BindSelectItem(Codes.EXT_YESNO_Y, moUseExistingBankDetailsDrop)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
                                    moCRIBANNumberText.Text = String.Empty
                                Else
                                    BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                                    State.CRequestBankInfoBO = New BankInfo(State.certCancelRequestBO.BankInfoId)
                                    PopulateControlFromBOProperty(moCRIBANNumberText, State.CRequestBankInfoBO.IbanNumberLast4Digits)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, True)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, True)
                                    'ControlMgr.SetEnableControl(Me, moCRIBANNumberText, False)
                                End If
                                ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, True)
                                ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, True)
                                ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, True)
                            Else
                                BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                                ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, True)
                                ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, True)
                                ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, True)
                                ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                                ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
                            End If
                            If State.IsEdit = False Then
                                ControlMgr.SetEnableControl(Me, moProofOfDocumentationDrop, False)
                                ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)

                            End If

                            If State.CancReasonIsLawful = Codes.EXT_YESNO_Y And (State.CancReasonCode = Codes.SFR_CR_DEATH Or State.CancReasonCode = Codes.SFR_CR_MOVINGABROAD) Then
                                ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, True)
                                ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, True)
                            End If

                        End If
                    End With
                End If
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, State.MyBO.Dealer.UseNewBillForm) = Codes.YESNO_Y) Then
                    Dim bankobj As BankInfo
                    If Not State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        bankobj = New BankInfo(State.CertInstalBankInfoId)
                        If bankobj.IbanNumber Is Nothing Then
                            If State.IsEdit = True Then
                                BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                                Ibanoperation(True)
                            Else
                                Ibanoperation(False)

                            End If

                        End If
                    End If

                    If State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        If State.IsEdit = True Then
                            BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                            Ibanoperation(True)
                        Else
                            Ibanoperation(False)
                        End If
                    End If
                End If

                If Not State.CertCancelRequestId.Equals(Guid.Empty) And Not State.MyBO.getCancelationRequestFlag = YES Then
                    ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)
                End If

                ClearCommentsControls()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateFinanceTab()

            With State.MyBO
                If Not String.IsNullOrEmpty(.Finance_Tab_Amount) Then
                    PopulateControlFromBOProperty(moFinanceAmount, CType(.Finance_Tab_Amount, Decimal), DECIMAL_FORMAT)
                Else
                    PopulateControlFromBOProperty(moFinanceAmount, 0, DECIMAL_FORMAT)
                End If

                PopulateControlFromBOProperty(moFinanceTerm, .Finance_Term, DECIMAL_FORMAT)

                If (Not String.IsNullOrEmpty(.Finance_Frequency)) AndAlso Convert.ToDecimal(.Finance_Frequency) > 0 Then
                    PopulateControlFromBOProperty(moFinanceFrequency, LookupListNew.GetDescrionFromListCode("FINFREQ", .Finance_Frequency))
                Else
                    PopulateControlFromBOProperty(moFinanceFrequency, "")
                End If

                PopulateControlFromBOProperty(moFinanceInstallmentNum, .Finance_Installment_Number, DECIMAL_FORMAT)

                If (Not String.IsNullOrEmpty(.Finance_Installment_Amount)) Then
                    PopulateControlFromBOProperty(moFinanceInstallmentAmount, CType(.Finance_Installment_Amount, Decimal), DECIMAL_FORMAT)
                Else
                    PopulateControlFromBOProperty(moFinanceInstallmentAmount, 0, DECIMAL_FORMAT)
                End If

                Dim blnIncomingAmount As Boolean = State.MyBO.Product.UpgradeFinanceBalanceComputationMethod.Equals(Codes.UPG_FINANCE_BAL_COMP_METH_IA)

                If ((Not String.IsNullOrEmpty(.Finance_Tab_Amount)) AndAlso Convert.ToDecimal(.Finance_Tab_Amount) > 0) OrElse blnIncomingAmount Then
                    PopulateControlFromBOProperty(moCurrentOutstandingBalanceText, CType(.GetFinancialAmountprodcode, Decimal), DECIMAL_FORMAT)
                    If blnIncomingAmount Then
                        PopulateControlFromBOProperty(moOutstandingBalanceDueDateText, .OutstandingBalanceDueDate)
                    Else
                        ControlMgr.SetVisibleControl(Me, moOutstandingBalanceDueDateLabel, False)
                        ControlMgr.SetVisibleControl(Me, moOutstandingBalanceDueDateText, False)
                    End If
                    PopulateControlFromBOProperty(moFinanceDateText, .FinanceDate)
                    PopulateControlFromBOProperty(moDownPaymentText, .DownPayment, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(moAdvancePaymentText, .AdvancePayment, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(moUpgradeFixedTermText, .UpgradeFixedTerm, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(moBillingAccountNumberText, .BillingAccountNumber)
                Else
                    PopulateControlFromBOProperty(moCurrentOutstandingBalanceText, "")
                    PopulateControlFromBOProperty(moFinanceDateText, "")
                    PopulateControlFromBOProperty(moDownPaymentText, "")
                    PopulateControlFromBOProperty(moAdvancePaymentText, "")
                    PopulateControlFromBOProperty(moUpgradeFixedTermText, "")
                    PopulateControlFromBOProperty(moBillingAccountNumberText, "")
                End If

                If .NumOfConsecutivePayments >= 0 Then
                    PopulateControlFromBOProperty(moNumOfConsecutivePaymentsText, .NumOfConsecutivePayments)
                Else
                    PopulateControlFromBOProperty(moNumOfConsecutivePaymentsText, "")
                End If

                If (Not String.IsNullOrEmpty(.DealerCurrentPlanCode)) Then
                    PopulateControlFromBOProperty(moDealerCurrentPlanCodeText, .DealerCurrentPlanCode)
                Else
                    PopulateControlFromBOProperty(moDealerCurrentPlanCodeText, "")
                End If

                If (Not String.IsNullOrEmpty(.DealerScheduledPlanCode)) Then
                    PopulateControlFromBOProperty(moDealerScheduledPlanCodeText, .DealerScheduledPlanCode)
                Else
                    PopulateControlFromBOProperty(moDealerScheduledPlanCodeText, "")
                End If
                If (Not String.IsNullOrEmpty(.DealerRewardPoints)) Then
                    PopulateControlFromBOProperty(moDealerRewardPointsText, .DealerRewardPoints)
                Else
                    PopulateControlFromBOProperty(moDealerRewardPointsText, "")
                End If

                If Not State.MyBO.UpgradeTermUomId.Equals(Guid.Empty) Then
                    Dim strUpgradeTermUomCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, State.MyBO.UpgradeTermUomId)
                    If strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_DAYS) Or strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_MONTHS) Then

                        ControlMgr.SetVisibleControl(Me, lblUpgradeTermUnitOfMeasure, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermUnitOfMeasureText, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermLabel, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermText, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelFrom, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextFROM, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelTo, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextTo, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramLabel, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramText, True)

                        PopulateControlFromBOProperty(moUpgradeFixedTermText, .UpgradeFixedTerm)
                        Dim strUpgradeTermUomDesc As String = LookupListNew.GetDescriptionFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, State.MyBO.UpgradeTermUomId, True)
                        PopulateControlFromBOProperty(moUpgradeTermUnitOfMeasureText, strUpgradeTermUomDesc)
                    ElseIf strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_DAYS) Or strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_MONTHS) Then
                        ControlMgr.SetVisibleControl(Me, lblUpgradeTermUnitOfMeasure, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermUnitOfMeasureText, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermLabel, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermText, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelFrom, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextFROM, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelTo, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextTo, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramLabel, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramText, True)
                        PopulateControlFromBOProperty(moUpgradeTermTextFROM, .UpgradeTermFrom)
                        PopulateControlFromBOProperty(moUpgradeTermTextTo, .UpgradeTermTo)
                        Dim strUpgradeTermUomDesc As String = LookupListNew.GetDescriptionFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, State.MyBO.UpgradeTermUomId, True)
                        PopulateControlFromBOProperty(moUpgradeTermUnitOfMeasureText, strUpgradeTermUomDesc)
                    Else
                        ControlMgr.SetVisibleControl(Me, lblUpgradeTermUnitOfMeasure, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermUnitOfMeasureText, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermLabel, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermText, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelFrom, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextFROM, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelTo, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeTermTextTo, False)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramLabel, True)
                        ControlMgr.SetVisibleControl(Me, moUpgradeProgramText, True)

                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, lblUpgradeTermUnitOfMeasure, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeTermUnitOfMeasureText, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermLabel, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeFixedTermText, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelFrom, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeTermTextFROM, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeTermLabelTo, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeTermTextTo, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeProgramLabel, False)
                    ControlMgr.SetVisibleControl(Me, moUpgradeProgramText, False)
                End If

                PopulateControlFromBOProperty(moLoanCodeText, .LoanCode)
                PopulateControlFromBOProperty(moPaymentShiftNumberText, .PaymentShiftNumber)

                If (Not String.IsNullOrEmpty(.PenaltyFee)) Then
                    PopulateControlFromBOProperty(moPenaltyFeeText, .PenaltyFee)
                Else
                    PopulateControlFromBOProperty(moPenaltyFeeText, "")
                End If

                If (Not String.IsNullOrEmpty(.AppleCareFee)) Then
                    PopulateControlFromBOProperty(moAppleCareFeeText, .AppleCareFee)
                Else
                    PopulateControlFromBOProperty(moAppleCareFeeText, "")
                End If

                PopulateControlFromBOProperty(moUpgradeProgramText, .UpgradeProgram)
            End With
            PopulateUPgradeDataExtensionsGrid()
        End Sub

        Public Sub populateCertCancellationBOFromForm()
            Try
                With State.certCancellationBO
                    PopulateBOProperty(State.certCancellationBO, "CancellationReasonId", moCancellationReasonDrop)
                    PopulateBOProperty(State.certCancellationBO, "CancellationDate", CancelCertDateTextbox)
                    PopulateBOProperty(State.certCancellationBO, "CancellationRequestedDate", CancelCertReqDateTextbox)
                    PopulateBOProperty(State.certCancellationBO, "Retailer", moRetailerText)
                End With
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelCommentBOFromForm()
            Try
                State.CancCommentBO = Comment.GetNewComment(State.MyBO.Id)
                PopulateBOProperty(State.CancCommentBO, "CallerName", CancelCallerNameTextbox)
                PopulateBOProperty(State.CancCommentBO, "Comments", moCancelCommentsTextbox)
                PopulateBOProperty(State.CancCommentBO, "CommentTypeId", moCancelCommentType)

                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelRequestBOFromForm()
            Try
                If IsNothing(State.certCancelRequestBO) Then
                    State.certCancelRequestBO = New CertCancelRequest
                End If

                ' Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CertId", Me.State.CertificateId)
                PopulateBOProperty(State.certCancelRequestBO, "CancellationReasonId", moCancelRequestReasonDrop)
                PopulateBOProperty(State.certCancelRequestBO, "CancellationDate", moCancelDateTextBox)
                PopulateBOProperty(State.certCancelRequestBO, "CancellationRequestDate", moCancelRequestDateTextBox)
                PopulateBOProperty(State.certCancelRequestBO, "ProofOfDocumentation", moProofOfDocumentationDrop, False, True)

                State.certCancelRequestBO.BankInfoId = State.CertInstalBankInfoId
                If IsNothing(State.CRequestBankInfoBO) Then
                    State.CRequestBankInfoBO = New BankInfo
                End If
                If GetSelectedValue(moUseExistingBankDetailsDrop) = Codes.EXT_YESNO_N Then
                    PopulateBOProperty(State.CRequestBankInfoBO, "IbanNumber", moCRIBANNumberText)
                    PopulateBOProperty(State.CRequestBankInfoBO, "CountryID", State.MyBO.Company.CountryId)
                    'Me.PopulateBOProperty(Me.State.CRequestBankInfoBO, "Account_Number", Me.moCRAccountNumberText)
                    State.useExistingBankInfo = Codes.YESNO_N
                Else
                    State.useExistingBankInfo = Codes.YESNO_Y
                End If

                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelRequestCommentBOFromForm()
            Try
                'Dim oCommTypeId As Guid
                State.CancReqCommentBO = Comment.GetNewComment(State.MyBO.Id)
                PopulateBOProperty(State.CancReqCommentBO, "CallerName", moCallerNameTextBox)
                PopulateBOProperty(State.CancReqCommentBO, "Comments", moCommentsTextbox)
                'oCommTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CERT_CANCEL_REQUEST)
                'Me.PopulateBOProperty(Me.State.CancReqCommentBO, "CommentTypeId", oCommTypeId)
                PopulateBOProperty(State.CancReqCommentBO, "CommentTypeId", GetSelectedItem(moCancelRequestJustificationDrop))
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'This method will change the Page Index and the Selected Index
        Public Function FindDVSelectedRowIndex(dv As CertItemCoverage.CertItemCoverageSearchDV) As Integer
            If State.TheItemCoverageState.selectedItemId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte())).Equals(State.TheItemCoverageState.selectedItemId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        End Function

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    State.LastErrMsg = ""
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception
                '  Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CheckIfComingFromRemoveCancelDueDateConfirm()
            Dim confResponse As String = HiddenRemoveCancelDueDatePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                SaveCancellationDueDate()
                PopulatePremiumInfoTab()
            End If
            HiddenRemoveCancelDueDatePromptResponse.Value = ""
        End Sub

        Protected Sub CheckIfComingFromTransferOfOwnershipConfirm()
            Try
                Dim confResponse As String = HiddenTransferOfOwnershipPromptResponse.Value
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    State.IsEdit = False
                    State.NavigateToEndorment = True
                    PopulateBOsFromForm()
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                    NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(State.MyBO.Id, State.TheItemCoverageState.manufaturerWarranty))
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    saveCertificate()
                End If
                HiddenTransferOfOwnershipPromptResponse.Value = ""
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim myBo As Certificate = State.MyBO
            Dim lastAction As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
            Dim lastErrMsg As String = State.LastErrMsg
            'Clean after consuming the action
            CleanPopupInput()
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                Select Case lastAction
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.MyBO.Save()
                        saveCertificate()
                        State.certificateChanged = True
                        State.selectedTab = 0
                        State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, State.certificateChanged)
                        NavController = Nothing
                        ReturnToCallingPage(retObj)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        State.selectedTab = 0
                        State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(lastAction, myBo, State.certificateChanged)
                        NavController = Nothing
                        ReturnToCallingPage(retObj)
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case lastAction
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.selectedTab = 0
                        State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(lastAction, myBo, State.certificateChanged)
                        NavController = Nothing
                        ReturnToCallingPage(retObj)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        State.selectedTab = 0
                        State.CertHistoryDV = Nothing
                        MasterPage.MessageController.AddErrorAndShow(lastErrMsg)
                End Select
            End If
        End Sub

        Private Function CheckCUITCUIL(cuit As String) As Boolean
            'Validation for CUIT size
            If cuit.Trim().Length <> 11 Then
                Return False
            End If

            'Validation for All Digits
            Dim ChkAllDigits As Long
            If Not Long.TryParse(cuit, ChkAllDigits) Then
                Return False
            End If

            Dim Prefix As Int16
            Dim Suffix As Int16

            'Validation based on Prefix
            Prefix = Int16.Parse(cuit.Substring(0, 2))
            If (Prefix <> 20 And Prefix <> 23 And Prefix <> 24 And Prefix <> 27 And Prefix <> 30 And Prefix <> 33 And Prefix <> 34) Then
                Return False
            End If

            'Validation based on Suffix
            Suffix = Int16.Parse(cuit.Substring(10, 1))

            Dim nSumatoria As Integer
            nSumatoria = ((5 * Int16.Parse(cuit.Substring(0, 1))) +
                          (4 * Int16.Parse(cuit.Substring(1, 1))) +
                          (3 * Int16.Parse(cuit.Substring(2, 1))) +
                          (2 * Int16.Parse(cuit.Substring(3, 1))) +
                          (7 * Int16.Parse(cuit.Substring(4, 1))) +
                          (6 * Int16.Parse(cuit.Substring(5, 1))) +
                          (5 * Int16.Parse(cuit.Substring(6, 1))) +
                          (4 * Int16.Parse(cuit.Substring(7, 1))) +
                          (3 * Int16.Parse(cuit.Substring(8, 1))) +
                          (2 * Int16.Parse(cuit.Substring(9, 1))))

            Dim Newsuffix As Integer = 11 - (nSumatoria Mod 11)

            If Newsuffix = 11 Then
                Newsuffix = 0
            ElseIf Newsuffix = 10 Then
                Newsuffix = 9
            End If

            Return (Newsuffix = Suffix)

        End Function

        Public Sub saveCertificate()
            Try

                If moVehicleLicenseTagText.Text <> "" Then
                    Dim dv As DataView, oCert As Certificate
                    Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    dv = oCert.ValidateLicenseFlag(moVehicleLicenseTagText.Text, State.MyBO.CertNumber, compGroupId)
                    If dv.Count > 0 Then
                        ElitaPlusPage.SetLabelError(moVehicleLicenseTagLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VEHICLE_LICENSE_TAG_ERR)
                    End If
                End If

                'REQ-1255 -- START
                'Validate CUIT_CUIL field
                If State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Then  '1= Display and Require When Cancelling or 2= Display Only
                    If moCUIT_CUILText.Text <> String.Empty Then
                        Dim CUIT_CUIL_Number As Int64 = 0
                        Dim DigitCheckerResult As Boolean = False
                        'Checking for numeric
                        If moCUIT_CUILText.Text.Length > 11 Or Not Int64.TryParse(moCUIT_CUILText.Text, CUIT_CUIL_Number) Then
                            Throw New GUIException(Message.MSG_INVALID_CUIT_CUIL_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CUIT_CUIL_NUMBER_ERR)
                        End If
                        'Checking for valid number
                        DigitCheckerResult = CheckCUITCUIL(moCUIT_CUILText.Text)
                        If Not DigitCheckerResult Then
                            Throw New GUIException(Message.MSG_INVALID_CUIT_CUIL_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CUIT_CUIL_NUMBER_ERR)
                        End If
                    End If
                End If
                'REQ-1255 -- END

                PopulateBOsFromForm()

                ' Validate User Selected Required Fields
                ValidateRequiredFields()

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    PopulateFormFromBOs()
                    State.IsEdit = False
                    EnableDisableFields()
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    State.certificateChanged = True
                Else
                    State.IsEdit = False
                    MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                    EnableDisableFields()
                End If
                DisplayMaskDob()
            Catch ex As DataNotFoundException
                MasterPage.MessageController.AddError(ex.Message)
                State.IsEdit = True
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                State.IsEdit = True
                EnableDisableFields()
            End Try
        End Sub
        Public Sub SaveCancellationDueDate()
            PopulateCanceDueDateFromForm()
            State.TheDirectDebitState.certInstallment.Save()
            EnableDisableFields()
        End Sub
        Public Sub saveCertInstallment(StatusChenge As Boolean)
            Try

                If State.TheDirectDebitState.StatusChenge Then

                    'REQ-5761
                    If State.IsNewBillPayBtnVisible Then
                        State.TheDirectDebitState.certInstallment.SP_ChngOfBillingStatus(GetSelectedItem(moBillingStatusId))
                    Else
                        State.TheDirectDebitState.certInstallment.SP_ChangeOfBillingStatus(GetSelectedItem(moBillingStatusId))
                    End If

                    State.TheDirectDebitState.StatusChenge = False
                    PopulateControlFromBOProperty(moNextDueDateText, State.TheDirectDebitState.certInstallment.newPaymentDueDate)
                End If

                PopulateInstalmenBoFromForm()
                If State.TheDirectDebitState.certInstallment.IsFamilyDirty Then
                    If State.creditCardPayment Then
                        State.TheDirectDebitState.CreditCardInfo.Save()
                        State.TheDirectDebitState.certInstallment.CreditCardInfoId = State.TheDirectDebitState.CreditCardInfo.Id
                    ElseIf State.directDebitPayment Then
                        State.TheDirectDebitState.bankInfo.DomesticTransfer = False
                        State.TheDirectDebitState.bankInfo.InternationalTransfer = False
                        State.TheDirectDebitState.bankInfo.InternationalTransfer = False
                        State.TheDirectDebitState.bankInfo.SourceCountryID = State.TheDirectDebitState.bankInfo.CountryID
                        State.TheDirectDebitState.bankInfo.Save()
                        State.TheDirectDebitState.certInstallment.BankInfoId = State.TheDirectDebitState.bankInfo.Id
                    End If

                    If State.BillingInformationChanged Then AdjustTheBillingStatus()

                    State.TheDirectDebitState.certInstallment.Save()
                    State.IsEdit = False
                    PopulatePremiumInfoTab()
                    EnableDisableFields()
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As ApplicationException
                HandleErrors(ex, MasterPage.MessageController)
                State.IsEdit = True
                EnableDisableFields()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub AdjustTheBillingStatus()
            With State.TheDirectDebitState.certInstallment
                Dim currentStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.BillingStatusId)
                Dim selectedStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moBillingStatusId))
                Dim objBillingDetail As BillingDetail
                Dim objBillingPayDetail As BillingPayDetail

                ' There might not be any billingDetail records yet for this installment, so the next section is made to catch no data found error.

                'REQ-5761
                If State.IsNewBillPayBtnVisible Then
                    Try
                        objBillingPayDetail = .CurrentBillingPaydetail
                    Catch ex As DataNotFoundException
                    Catch ex As Exception

                    End Try

                    If State.MyBO IsNot Nothing AndAlso objBillingPayDetail IsNot Nothing _
                       AndAlso currentStatus = Codes.BILLING_STATUS__REJECTED _
                       AndAlso selectedStatus = Codes.BILLING_STATUS__ACTIVE Then
                        Throw New GUIException(Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED, Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED)
                    End If
                Else
                    Try
                        objBillingDetail = .CurrentBillingDetail
                    Catch ex As DataNotFoundException
                    Catch ex As Exception

                    End Try

                    If State.MyBO IsNot Nothing AndAlso objBillingDetail IsNot Nothing _
                       AndAlso currentStatus = Codes.BILLING_STATUS__REJECTED _
                       AndAlso selectedStatus = Codes.BILLING_STATUS__ACTIVE _
                       AndAlso Not objBillingDetail.ReAttemptCount.Equals(DBNull.Value) _
                       AndAlso objBillingDetail.ReAttemptCount >= Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value).CollectionReAttempts.Value Then
                        Throw New GUIException(Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED, Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED)
                    End If
                End If

                If currentStatus = Codes.BILLING_STATUS__REJECTED Or currentStatus = Codes.BILLING_STATUS__ON_HOLD Then
                    .BillingStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, Codes.BILLING_STATUS__ACTIVE)
                End If

            End With
        End Sub

        Public Function IsRetailerAssociated(odealer As Dealer) As Boolean
            Dim oYesList As DataView = LookupListNew.GetListItemId(odealer.RetailerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
            If oYesNo = "N" Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsEffectiveCoverage(BeginDate As Date, EndDate As Date) As Boolean
            Dim todayDate As Date
            If State.MyBO.StatusCode = CLOSED Then
                Return False
            Else
                If todayDate.Today >= BeginDate AndAlso todayDate.Today <= EndDate Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Private Sub ValidateAddressField()
            Dim addressErrorExist As Boolean = False

            If State.MyBO.CustomerName Is Nothing OrElse State.MyBO.CustomerName.Trim = String.Empty Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                addressErrorExist = True
            End If

            Dim strAddFmt As String = New Country(State.MyBO.AddressChild.CountryId).MailAddrFormat.ToUpper
            If State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "ADR1") Then
                If State.MyBO.AddressChild.Address1 Is Nothing OrElse State.MyBO.AddressChild.Address1.Trim = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "CITY") Then
                If State.MyBO.AddressChild.City Is Nothing OrElse State.MyBO.AddressChild.City.Trim = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "ZIP") Then
                If State.MyBO.AddressChild.PostalCode Is Nothing OrElse State.MyBO.AddressChild.PostalCode.Trim = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "RGNAME") OrElse
               State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "RGCODE") Then
                If State.MyBO.AddressChild.RegionId.Equals(Guid.Empty) Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "COU") Then
                If State.MyBO.AddressChild.CountryId.Equals(Guid.Empty) Then
                    MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If addressErrorExist Then
                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED)
            End If

        End Sub

        Private Sub ValidateRequiredFields()
            Dim requiredFieldsErrorExist As Boolean = False
            Dim _country As Country
            Dim strRequiredFieldSetting As String = String.Empty

            If State.MyBO.CountryPurchaseId = Nothing Then
                Throw New GUIException(Message.MSG_INVALID_COUNTRY, Message.MSG_INVALID_COUNTRY)
            Else
                _country = New Country(State.MyBO.CountryPurchaseId)
                If (Not (Nothing Is _country)) AndAlso (_country.AddressInfoReqFields <> Nothing) Then
                    strRequiredFieldSetting = _country.AddressInfoReqFields.ToUpper()
                End If
            End If

            If strRequiredFieldSetting.Trim().Length > 0 Then
                ' Validating Customer Salutation
                If strRequiredFieldSetting.Contains("[SALU]") Then
                    If State.MyBO.SalutationId = Nothing OrElse State.MyBO.SalutationId = Guid.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("INVALID_SALUTATION")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Name
                If strRequiredFieldSetting.Contains("[NAME]") Then
                    If State.MyBO.CustomerId.Equals(Guid.Empty) Then
                        If State.MyBO.CustomerName Is Nothing OrElse State.MyBO.CustomerName.Trim() = String.Empty Then
                            MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                            requiredFieldsErrorExist = True
                        End If
                    Else
                        If (State.MyBO.CustomerFirstName Is Nothing OrElse State.MyBO.CustomerFirstName.Trim() = String.Empty) Then
                            MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_FIRST_NAME_ERR)
                            requiredFieldsErrorExist = True
                        ElseIf (State.MyBO.CustomerLastName Is Nothing OrElse State.MyBO.CustomerLastName.Trim() = String.Empty) Then
                            MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_LAST_NAME_ERR)
                            requiredFieldsErrorExist = True
                        End If
                    End If
                End If

                ' Validating Customer Address1
                If strRequiredFieldSetting.Contains("[ADR1]") Then
                    If State.MyBO.AddressChild.Address1 Is Nothing OrElse State.MyBO.AddressChild.Address1.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ADDRESS1_FIELD_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Address2
                If strRequiredFieldSetting.Contains("[ADR2]") Then
                    If State.MyBO.AddressChild.Address2 Is Nothing OrElse State.MyBO.AddressChild.Address2.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ADDRESS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Address3
                If strRequiredFieldSetting.Contains("[ADR3]") Then
                    If State.MyBO.AddressChild.Address3 Is Nothing OrElse State.MyBO.AddressChild.Address3.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ADDRESS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer City
                If strRequiredFieldSetting.Contains("[CITY]") Then
                    If State.MyBO.AddressChild.City Is Nothing OrElse State.MyBO.AddressChild.City.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("CITY_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Zip
                If strRequiredFieldSetting.Contains("[ZIP]") Then
                    If State.MyBO.AddressChild.PostalCode Is Nothing OrElse State.MyBO.AddressChild.PostalCode.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ZIP_IS_MISSING")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer State/Provience OR Region
                If strRequiredFieldSetting.Contains("[PRO]") Then
                    If State.MyBO.AddressChild.RegionId = Nothing OrElse State.MyBO.AddressChild.RegionId = Guid.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ERR_INVALID_STATE")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Country
                If strRequiredFieldSetting.Contains("[COU]") Then
                    If State.MyBO.AddressChild.CountryId = Nothing OrElse State.MyBO.AddressChild.CountryId = Guid.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("ERR_INVALID_COUNTRY")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Email
                If strRequiredFieldSetting.Contains("[EMAIL]") Then
                    If State.MyBO.Email Is Nothing OrElse State.MyBO.Email.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("EMAIL_IS_REQUIRED_ERR")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Work Phone
                If strRequiredFieldSetting.Contains("[WPHONE]") Then
                    If State.MyBO.WorkPhone Is Nothing OrElse State.MyBO.WorkPhone.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("CELL_PHONE_NUMBER_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Home Phone
                If strRequiredFieldSetting.Contains("[HPHONE]") Then
                    If State.MyBO.HomePhone Is Nothing OrElse State.MyBO.HomePhone.Trim() = String.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("HOME_PHONE_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Region
                If strRequiredFieldSetting.Contains("[RGN]") Then
                    If State.MyBO.AddressChild.RegionId = Nothing OrElse State.MyBO.AddressChild.RegionId = Guid.Empty Then
                        MasterPage.MessageController.AddErrorAndShow("REGION_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

            End If

            If State.MyBO.AddressChild.IsEmpty Then
                If (State.ClaimsearchDV Is Nothing) Then
                    State.ClaimsearchDV = State.MyBO.ClaimsForCertificate(State.CertificateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
                If (State.ClaimsearchDV.Table.Rows.Count > 0) Then
                    Throw New GUIException(Message.MSG_CANNOT_REMOVE_ADDRESS, Message.MSG_CANNOT_REMOVE_ADDRESS)
                End If
            End If

            If requiredFieldsErrorExist Then
                Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Message.MSG_GUI_INVALID_VALUE)
            End If

        End Sub

        Private Sub validateCancellationProcessFields()

            Dim CanellProcErrorExist As Boolean = False

            If State.certCancellationBO.CancellationReasonId.Equals(Guid.Empty) Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE)
                CanellProcErrorExist = True
            End If

            If State.certCancellationBO.CancellationDate.Equals(DBNull.Value) Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE)
                CanellProcErrorExist = True
            End If

            If CanellProcErrorExist Then
                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED)
            End If
        End Sub
        Private Sub validateUpdateBankInfoRefundFields()

            'If Me.State.RefundBankInfoBO.Account_Number.Length <= 4 Then
            '    ElitaPlusPage.SetLabelError(moRfAccountNumberLabel)
            '    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ACCTNO_LENGTH)
            'Else
            '    ClearLabelError(moRfAccountNumberLabel)
            'End If
            ClearLabelError(moRfIBANNumberLabel)
            Try
                If State.RefundBankInfoBO.IbanNumber <> String.Empty Then
                    State.RefundBankInfoBO.Validate()
                    'If Me.State.RefundBankInfoBO.IbanNumber.Length <= 4 Then
                    '    ElitaPlusPage.SetLabelError(moRfIBANNumberLabel)
                    '    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_LENGTH)
                    'End If
                Else
                    ElitaPlusPage.SetLabelError(moRfIBANNumberLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
                End If

            Catch ex As ApplicationException
                ElitaPlusPage.SetLabelError(moRfIBANNumberLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_INVALID)

            End Try
        End Sub
        Private Sub ValidateCancellationCommentsFields()

            Dim CanellCommentErrorExist As Boolean = False

            If State.CancCommentBO.CallerName Is Nothing OrElse State.CancCommentBO.CallerName.Trim = String.Empty Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_CALLER_NAME_REQUIRED)
                CanellCommentErrorExist = True
            End If

            If State.CancCommentBO.CommentTypeId.Equals(Guid.Empty) Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_JUSTIFICATION_MUST_BE_SELECTED_ERR)
                CanellCommentErrorExist = True
            End If

            If State.CancCommentBO.Comments Is Nothing OrElse State.CancCommentBO.Comments.Trim = String.Empty Then
                MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMMENTS_ARE_REQUIRED)
                CanellCommentErrorExist = True
            End If

            If CanellCommentErrorExist Then
                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED)
            End If
        End Sub
#End Region

#Region "Other Customer Information"
        <WebMethod(), Script.Services.ScriptMethod()>
        Public Shared Function GetOtherCustomerDetails(customerId As String, custInfoExclude As String, cust_salutation_exclude As String, lang_id As String, identification_number_type As String) As String
            Try
                Dim dv As Certificate.OtherCustomerInfoDV = Certificate.GetOtherCustomerDetails(New Guid(customerId), New Guid(lang_id), identification_number_type)
                Dim ds As DataSet
                Dim dtHeaders As New DataTable("Headers")
                Dim dtFlagInfo As New DataTable("FlagCheck")

                ds = dv.Table.DataSet
                ds.DataSetName = "CustomerDetailDS"
                ds.Tables(0).TableName = "CustomerDetail"

                dtHeaders.Columns.Add("UI_PROG_CODE", GetType(String))
                dtHeaders.Columns.Add("TRANSLATION", GetType(String))
                ds.Tables.Add(dtHeaders)

                dtHeaders.Rows.Add(New String() {"SALUTATION", TranslationBase.TranslateLabelOrMessage("SALUTATION")})
                dtHeaders.Rows.Add(New String() {"CUSTOMER_FIRST_NAME", TranslationBase.TranslateLabelOrMessage("CUSTOMER_FIRST_NAME")})
                dtHeaders.Rows.Add(New String() {"CUSTOMER_MIDDLE_NAME", TranslationBase.TranslateLabelOrMessage("CUSTOMER_MIDDLE_NAME")})
                dtHeaders.Rows.Add(New String() {"CUSTOMER_LAST_NAME", TranslationBase.TranslateLabelOrMessage("CUSTOMER_LAST_NAME")})
                dtHeaders.Rows.Add(New String() {"EMAIL", TranslationBase.TranslateLabelOrMessage("EMAIL")})
                dtHeaders.Rows.Add(New String() {"HOME_PHONE", TranslationBase.TranslateLabelOrMessage("HOME_PHONE")})
                dtHeaders.Rows.Add(New String() {"TAX_ID", TranslationBase.TranslateLabelOrMessage("TAX_ID")})
                dtHeaders.Rows.Add(New String() {"WORK_PHONE", TranslationBase.TranslateLabelOrMessage("WORK_PHONE")})
                dtHeaders.Rows.Add(New String() {"ADDRESS1", TranslationBase.TranslateLabelOrMessage("ADDRESS1")})
                dtHeaders.Rows.Add(New String() {"COUNTRY", TranslationBase.TranslateLabelOrMessage("COUNTRY")})
                dtHeaders.Rows.Add(New String() {"ADDRESS2", TranslationBase.TranslateLabelOrMessage("ADDRESS2")})
                dtHeaders.Rows.Add(New String() {"STATE_PROVINCE", TranslationBase.TranslateLabelOrMessage("STATE_PROVINCE")})
                dtHeaders.Rows.Add(New String() {"ADDRESS3", TranslationBase.TranslateLabelOrMessage("ADDRESS3")})
                dtHeaders.Rows.Add(New String() {"CITY", TranslationBase.TranslateLabelOrMessage("CITY")})
                dtHeaders.Rows.Add(New String() {"ZIP", TranslationBase.TranslateLabelOrMessage("ZIP")})
                dtHeaders.Rows.Add(New String() {"DATE_OF_BIRTH", TranslationBase.TranslateLabelOrMessage("DATE_OF_BIRTH")})
                dtHeaders.Rows.Add(New String() {"MARITAL_STATUS", TranslationBase.TranslateLabelOrMessage("MARITAL_STATUS")})
                dtHeaders.Rows.Add(New String() {"NATIONALITY", TranslationBase.TranslateLabelOrMessage("NATIONALITY")})
                dtHeaders.Rows.Add(New String() {"PLACE_OF_BIRTH", TranslationBase.TranslateLabelOrMessage("PLACE_OF_BIRTH")})
                dtHeaders.Rows.Add(New String() {"GENDER", TranslationBase.TranslateLabelOrMessage("GENDER")})

                dtFlagInfo.Columns.Add("FLAG_NAME", GetType(String))
                dtFlagInfo.Columns.Add("FLAG_VALUE", GetType(String))
                ds.Tables.Add(dtFlagInfo)

                If (New Guid(custInfoExclude)).Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) Then '0= None
                    dtFlagInfo.Rows.Add(New String() {"CUSTINFOEXCLUDE", "TRUE"})
                Else
                    dtFlagInfo.Rows.Add(New String() {"CUSTINFOEXCLUDE", "FALSE"})
                End If

                dtFlagInfo.Rows.Add(New String() {"CUSTSALUTATIONEXCLUDE", cust_salutation_exclude})

                Return ds.GetXml()
            Catch ex As Exception
                Return "Error!"
            End Try
        End Function


        Public Sub PopulateCustomerGrid()

            Dim cert As Certificate = State.MyBO
            Dim dv As Certificate.OtherCustomerInfoDV = Certificate.GetOtherCustomerInfo(State.MyBO.Id, State.MyBO.IdentificationNumberType)
            Dim objDataColCustInfoExclude As DataColumn = New DataColumn()
            Dim objDataColCustSalutationExclude As DataColumn = New DataColumn()
            Dim objDataColLangID As DataColumn = New DataColumn()
            Dim objDataColIdentificationNumberType As DataColumn = New DataColumn()

            If State.ReqCustomerLegalInfoId.Equals(Guid.Empty) Then
                State.ReqCustomerLegalInfoId = (New Company(State.MyBO.CompanyId).ReqCustomerLegalInfoId)
            End If





            If dv.Count > 0 Then
                CustomerCount.Text = "(" + dv.Table.Rows.Count.ToString() + ")"

                objDataColCustInfoExclude.ColumnName = "CUST_INFO_EXCLUDE"
                objDataColCustInfoExclude.DataType = GetType(String)
                objDataColCustInfoExclude.DefaultValue = State.ReqCustomerLegalInfoId.ToString()
                dv.Table.Columns.Add(objDataColCustInfoExclude)

                objDataColCustSalutationExclude.ColumnName = "CUST_SALUTATION_EXCLUDE"
                objDataColCustSalutationExclude.DataType = GetType(String)
                If State.isSalutation Then
                    objDataColCustSalutationExclude.DefaultValue = "FALSE"
                Else
                    objDataColCustSalutationExclude.DefaultValue = "TRUE"
                End If

                dv.Table.Columns.Add(objDataColCustSalutationExclude)

                objDataColLangID.ColumnName = "LANG_ID"
                objDataColLangID.DataType = GetType(String)
                objDataColLangID.DefaultValue = ElitaPlusIdentity.Current.ActiveUser.LanguageId.ToString()
                dv.Table.Columns.Add(objDataColLangID)

                objDataColIdentificationNumberType.ColumnName = "IDENTIFICATION_NUMBER_TYPE"
                objDataColIdentificationNumberType.DataType = GetType(String)
                objDataColIdentificationNumberType.DefaultValue = State.MyBO.IdentificationNumberType.ToString()
                dv.Table.Columns.Add(objDataColIdentificationNumberType)


                CertOtherCustomers.AutoGenerateColumns = False
                CertOtherCustomers.DataSource = dv
                CertOtherCustomers.DataBind()
                ControlMgr.SetVisibleControl(Me, CertOtherCustomers, State.isItemsGridVisible)
            Else
                CustomerCount.Text = "(0)"
            End If

        End Sub
#End Region
#Region "Coverage Datagrid Related"

        Public Sub PopulateCoveragesGrid()
            'Me.GetSelectedItem(moDealerDrop)

            Dim dv As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(State.MyBO.Id)
            'Dim dv1 As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetCurrentProductCodeCoverages(Me.State.MyBO.Id)
            Dim Row As DataRowView

            ' Check if the Coverages Data is already fetched from the DB, else get the data from the DB 
            If (State.CoveragesearchDV Is Nothing) Or State.blnMFGChanged Then
                State.CoveragesearchDV = CertItemCoverage.GetCurrentProductCodeCoverages(State.MyBO.Id)
            End If


            Dim CertItemCoverageIds As String = String.Empty


            'If dv1.Count > 0 Then

            'For Each Row In dv1
            If State.CoveragesearchDV.Count > 0 Then
                For Each Row In State.CoveragesearchDV

                    CertItemCoverageIds = CertItemCoverageIds & "'" & GuidControl.GuidToHexString(New Guid(CType(Row(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte()))) & "',"
                Next

            End If

            If Not CertItemCoverageIds.Equals(String.Empty) Then
                CertItemCoverageIds = CertItemCoverageIds.Substring(0, (CertItemCoverageIds.Length - 1))
            End If

            Dim todayDate As Date

            '   dv.Sort = Me.State.CurrentSortExpresion
            Try
                dv.Sort = Grid.DataMember
                Grid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(dv, State.TheItemCoverageState.selectedItemId, Grid, State.TheItemCoverageState.PageIndex)

                State.TheItemCoverageState.PageIndex = Grid.CurrentPageIndex
                Grid.DataSource = State.CoveragesearchDV
                State.PageIndex = Grid.CurrentPageIndex

                If (Not State.CoverageSortExpression.Equals(String.Empty)) Then
                    State.CoveragesearchDV.Sort = State.CoverageSortExpression

                End If

                HighLightSortColumn(Grid, State.CoverageSortExpression, IsNewUI)

                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                If IsSinglePremium Then
                    Grid.Columns(GRID_COL_SEQUENCE_IDX).Visible = False
                Else
                    Grid.Columns(GRID_COL_SEQUENCE_IDX).Visible = True
                End If

                If IsExtendCovByAutoRenew Then
                    Grid.Columns(GRID_COL_NO_OF_RENEWALS_IDX).Visible = True
                    Grid.Columns(GRID_COL_RENEWAL_DATE_IDX).Visible = True
                    Grid.Columns(GRID_COL_COVERAGE_DURATION_IDX).Visible = True
                Else
                    Grid.Columns(GRID_COL_NO_OF_RENEWALS_IDX).Visible = False
                    Grid.Columns(GRID_COL_RENEWAL_DATE_IDX).Visible = False
                    Grid.Columns(GRID_COL_COVERAGE_DURATION_IDX).Visible = False
                End If

                'If (Me.State.MyBO.Company.EnablePeriodMileageVal.ToUpper().Equals("YESNO-N")) Then
                '    Me.Grid.Columns(GRID_COL_BEGIN_KM_IDX).Visible = False
                '    Me.Grid.Columns(GRID_COL_END_KM_IDX).Visible = False
                'Else
                '    Me.Grid.Columns(GRID_COL_BEGIN_KM_IDX).Visible = True
                '    Me.Grid.Columns(GRID_COL_END_KM_IDX).Visible = True
                'End If


                If dv.Count = 0 Then
                    Throw New DataNotFoundException(NO_COVERAGE_FOUND)
                End If

                'Dim tempDV As DataView = dv
                'tempDV.RowFilter = "end_date >= '" & todayDate.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture) & "'"
                'If tempDV.Count <= 0 Then
                '    allCoveragesExpired = True
                'Else
                '    allCoveragesExpired = False

                'End If


                'Check if there are expired coverages to enable coverage history tab
                State.TheItemCoverageState.CoverageHistoryDV = New DataView(dv.Table)
                If Not CertItemCoverageIds.Equals(String.Empty) Then
                    State.TheItemCoverageState.CoverageHistoryDV.RowFilter = " cert_item_coverage_id_hex not in (" & CertItemCoverageIds & ")"
                End If
                If State.TheItemCoverageState.CoverageHistoryDV.Count > 0 Then
                    ExpiredCoveragesExist = True
                End If

                ' removed as it is already used above
                'Me.Grid.DataSource = dv1
                'Me.Grid.DataBind()

                'ControlMgr.SetVisibleControl(Me, Grid, Me.State.TheItemCoverageState.IsGridVisible)

                'PM 2/14/2006 begin
                For Each cov As CertItemCoverage In State.MyBO.AssociatedItemCoverages
                    If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                        State.TheItemCoverageState.manufaturerWarranty = True
                    End If
                Next

                'For Each ciRow As DataRow In dv.Table.Rows
                '    Dim ciCov As String = CType(ciRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE), String)
                '    If ciCov = MANUFACTURER Then
                '        Me.State.TheItemCoverageState.manufaturerWarranty = True
                '    End If
                'Next
                'PM 2/14/2006 end

                Dim tempDV As DataView = New DataView(dv.Table)
                tempDV.RowFilter = "end_date >= '" & todayDate.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture) & "'"
                If tempDV.Count <= 0 Then
                    allCoveragesExpired = True
                Else
                    allCoveragesExpired = False
                End If

                'allCoveragesExpired = False
                'tempDV.Sort = CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE
                'Dim coverageRow As DataRow = tempDV.Table.Rows(0)
                'Dim lastDate As Date = CType(coverageRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date)
                'If lastDate < todayDate.Today Then
                '    allCoveragesExpired = True
                'End If
            Catch ex As DataNotFoundException
                MasterPage.MessageController.AddError(ex.Message)
                State.IsEdit = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
            Try
                If State.CoverageSortExpression.StartsWith(e.SortExpression) Then
                    If State.CoverageSortExpression.EndsWith(" DESC") Then
                        State.CoverageSortExpression = e.SortExpression
                    Else
                        State.CoverageSortExpression &= " DESC"
                    End If
                Else
                    State.CoverageSortExpression = e.SortExpression
                End If
                State.TheItemCoverageState.selectedItemId = Nothing
                State.PageIndex = 0
                PopulateCoveragesGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grid.ItemCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    'Me.State.MyBO.SelectedCoverageId = Me.State.TheItemCoverageState.selectedItemId
                    'Me.callPage(CertItemForm.URL, Me.State.MyBO)
                    'arf 12-20-04 'Me.callPage(CertItemForm.URL, Me.State.TheItemCoverageState.selectedItemId)
                    'arf 12-20-04 begin
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = State.TheItemCoverageState.selectedItemId
                    NavController.Navigate(Me, "coverage_selected", New CertItemForm.Parameters("FromLink"))
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub


        private function CoverageExpirationDate(coverageBeginDate as Date, maxRenewalDuration as Integer) as Date?
            dim expirationDate as Date = DateAdd(DateInterval.Month, maxRenewalDuration, coverageBeginDate)
            return DateAdd(DateInterval.Day, -1, expirationDate)
        End function

        private function NumberOfRenewalsRemaining(renewalDuration as Integer, coverageDuration as Integer, numberOfRenewals As Integer) As Integer
            Dim renewalsRemaining as Integer = renewalDuration/coverageDuration - (numberOfRenewals + 1) '+1 for initial registration

            if renewalsRemaining < 0 then
                'potential data issue - should we log an 'exception'?
                renewalsRemaining = 0
            End If

            return renewalsRemaining
        End function

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditCoverage As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Dim maxRenewalDuration As String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_MAX_RENEWAL_DURATION).ToString
                    Dim coverageBeginDate as Date = CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date)
                    Dim coverageDuration as String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_COVERAGE_DURATION).ToString
                    Dim numberOfRenewals as String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_NO_OF_RENEWALS).ToString

                    e.Item.Cells(GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString
                    e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).Text = GetDateFormattedStringNullable(coverageBeginDate)
                    e.Item.Cells(GRID_COL_END_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    e.Item.Cells(GRID_COL_SEQUENCE_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_SEQUENCE).ToString

                    e.Item.Cells(GRID_COL_COVERAGE_DURATION_IDX).Text = coverageDuration
                    e.Item.Cells(GRID_COL_NO_OF_RENEWALS_IDX).Text = numberOfRenewals.ToInteger()
                    'e.Item.Cells(Me.GRID_COL_BEGIN_KM_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_Ext_Begin_KM_MI).ToString
                    'e.Item.Cells(Me.GRID_COL_END_KM_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_Ext_End_KM_MI).ToString

                    If (Not String.IsNullOrWhiteSpace(maxRenewalDuration)) then
                        e.Item.Cells(GRID_COL_MAX_RENEWAL_DURATION_IDX).Text = maxRenewalDuration
                        Dim renewalDuration as Integer = maxRenewalDuration.ToInteger()
                        If (0 < renewalDuration) Then
                            e.Item.Cells(GRID_COL_COVERAGE_EXPIRATION_DATE_IDX).Text = GetDateFormattedStringNullable(CoverageExpirationDate(coverageBeginDate, renewalDuration))
                        End If
                        Dim coverDuration as Integer = coverageDuration.ToInteger()
                        If (0 < coverDuration) Then
                            e.Item.Cells(GRID_COL_NO_OF_RENEWALS_REMAINING_IDX).Text = NumberOfRenewalsRemaining(renewalDuration, coverDuration, numberOfRenewals.ToInteger()).ToString
                        End If
                    End If

                    If Not Convert.IsDBNull(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_RENEWAL_DATE)) Then
                        e.Item.Cells(GRID_COL_RENEWAL_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_RENEWAL_DATE), Date))
                    End If
                    e.Item.Cells(GRID_COL_COVERAGE_TOTAL_PAID_AMOUNT_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TOTAL_PAID_AMOUNT).ToString
                    e.Item.Cells(GRID_COL_COVERAGE_REMAIN_LIABILITY_LIMIT_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_REMAIN_LIABILITY_LIMIT).ToString

                    If (e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL) IsNot Nothing) Then
                        btnEditCoverage = CType(e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL), LinkButton)
                        btnEditCoverage.Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        btnEditCoverage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte()))
                        btnEditCoverage.CommandName = SELECT_ACTION_COMMAND
                    End If

                    If IsEffectiveCoverage(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date), CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date)) Then
                        e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).CssClass = "StatActive"
                        e.Item.Cells(GRID_COL_END_DATE_IDX).CssClass = "StatActive"
                        'e.Item.Cells(Me.GRID_COL_BEGIN_KM_IDX).CssClass = "StatActive"
                        'e.Item.Cells(Me.GRID_COL_END_KM_IDX).CssClass = "StatActive"
                    Else
                        e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).CssClass = "StatInactive"
                        e.Item.Cells(GRID_COL_END_DATE_IDX).CssClass = "StatInactive"
                        'e.Item.Cells(Me.GRID_COL_BEGIN_KM_IDX).CssClass = "StatInactive"
                        'e.Item.Cells(Me.GRID_COL_END_KM_IDX).CssClass = "StatInactive"
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                State.TheItemCoverageState.PageIndex = e.NewPageIndex
                State.TheItemCoverageState.selectedItemId = Guid.Empty
                PopulateCoveragesGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Coverage History Datagrid Related"

        Public Sub PopulateCoveragesHistoryGrid()
            'Me.GetSelectedItem(moDealerDrop)

            'Dim dv As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(Me.State.MyBO.Id)
            Dim todayDate As Date

            '   dv.Sort = Me.State.CurrentSortExpresion
            Try
                State.TheItemCoverageState.CoverageHistoryDV.Sort = CoverageHistoryGrid.DataMember
                CoverageHistoryGrid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(State.TheItemCoverageState.CoverageHistoryDV, State.TheItemCoverageState.selectedItemId, CoverageHistoryGrid, State.TheItemCoverageState.PageIndex)
                State.TheItemCoverageState.PageIndex = CoverageHistoryGrid.CurrentPageIndex
                If State.TheItemCoverageState.CoverageHistoryDV.Count = 0 Then
                    Throw New DataNotFoundException(NO_COVERAGE_FOUND)
                End If

                'Filter only expired coverages 
                'dv.RowFilter = "end_date < '" & todayDate.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture) & "'"

                CoverageHistoryGrid.DataSource = State.TheItemCoverageState.CoverageHistoryDV
                CoverageHistoryGrid.DataBind()

                ControlMgr.SetVisibleControl(Me, CoverageHistoryGrid, State.TheItemCoverageState.IsGridVisible)

                If IsSinglePremium Then
                    CoverageHistoryGrid.Columns(GRID_COL_SEQUENCE_IDX).Visible = False
                Else
                    CoverageHistoryGrid.Columns(GRID_COL_SEQUENCE_IDX).Visible = True
                End If

            Catch ex As DataNotFoundException
                MasterPage.MessageController.AddError(ex.Message)
                State.IsEdit = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub CoverageHistoryGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles CoverageHistoryGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    'Me.State.MyBO.SelectedCoverageId = Me.State.TheItemCoverageState.selectedItemId
                    'Me.callPage(CertItemForm.URL, Me.State.MyBO)
                    'arf 12-20-04 'Me.callPage(CertItemForm.URL, Me.State.TheItemCoverageState.selectedItemId)
                    'arf 12-20-04 begin
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = State.TheItemCoverageState.selectedItemId
                    NavController.Navigate(Me, "coverage_selected", New CertItemForm.Parameters("FromLink"))
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CoverageHistoryGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CoverageHistoryGrid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub CoverageHistoryGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CoverageHistoryGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditCoverage As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString
                    e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                    e.Item.Cells(GRID_COL_END_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    e.Item.Cells(GRID_COL_SEQUENCE_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_SEQUENCE).ToString
                    If (e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL) IsNot Nothing) Then
                        btnEditCoverage = CType(e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL), LinkButton)
                        btnEditCoverage.Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        btnEditCoverage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte()))
                        btnEditCoverage.CommandName = SELECT_ACTION_COMMAND
                    End If

                    If IsEffectiveCoverage(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date), CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date)) Then
                        e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).CssClass = "StatActive"
                        e.Item.Cells(GRID_COL_END_DATE_IDX).CssClass = "StatActive"
                    Else
                        e.Item.Cells(GRID_COL_BEGIN_DATE_IDX).CssClass = "StatInactive"
                        e.Item.Cells(GRID_COL_END_DATE_IDX).CssClass = "StatInactive"
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CoverageHistoryGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CoverageHistoryGrid.PageIndexChanged

            Try
                State.TheItemCoverageState.PageIndex = e.NewPageIndex
                State.TheItemCoverageState.selectedItemId = Guid.Empty
                PopulateCoveragesHistoryGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Claims Datagrid Related"

        Public Sub PopulateClaimsGrid()

            'Me.GetSelectedItem(moDealerDrop)

            ' Check if the Claims Data is already fetched from the DB, else get the data from the DB 
            ' Dim dv As Certificate.CertificateClaimsDV = Me.State.MyBO.ClaimsForCertificate(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If (State.ClaimsearchDV Is Nothing) Then
                State.ClaimsearchDV = State.MyBO.ClaimsForCertificate(State.CertificateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            ' remove the sort, since column wise sorting is to be implemented
            ' dv.Sort = Me.State.CurrentSortExpresion
            ' dv.Sort = moClaimsDatagrid.DataMember
            moClaimsDatagrid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.ClaimsearchDV, State.selectedClaimItemId, moClaimsDatagrid, State.PageIndexClaimsGrid)
            State.PageIndexClaimsGrid = moClaimsDatagrid.CurrentPageIndex
            moClaimsDatagrid.DataSource = State.ClaimsearchDV
            State.PageIndex = moClaimsDatagrid.CurrentPageIndex
            If (Not State.ClaimsSortExpression.Equals(String.Empty)) Then
                State.ClaimsearchDV.Sort = State.ClaimsSortExpression
            End If

            HighLightSortColumn(moClaimsDatagrid, State.ClaimsSortExpression, IsNewUI)

            moClaimsDatagrid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        End Sub


        Private Sub moClaimsDatagrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moClaimsDatagrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.selectedClaimItemId = New Guid(CType(e.CommandArgument, String))
                    ''Me.State.selectedClaimItemId = New Guid(e.Item.Cells(Me.GRID_COL_EDIT_CLAIM_IDX).Text)
                    State.MyBO.SelectedCoverageId = State.TheItemCoverageState.selectedItemId
                    Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.selectedClaimItemId)
                    If claimBo.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                        If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                        Else
                            NavController = Nothing
                            callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, claimBo))
                        End If

                    Else
                        'Save the Current Flow NavCtrl
                        '  CType(MyBase.State, BaseState).NavCtrl = Me.NavController
                        callPage(ClaimForm.URL, State.selectedClaimItemId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
                   (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moClaimsDatagrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moClaimsDatagrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditClaim As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (e.Item.Cells(GRID_COL_CLAIM_NUMBER).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) IsNot Nothing) Then
                        btnEditClaim = CType(e.Item.Cells(GRID_COL_CLAIM_NUMBER).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                        btnEditClaim.Text = dvRow(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER).ToString
                        btnEditClaim.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Certificate.CertificateClaimsDV.COL_CLAIM_ID), Byte()))
                        btnEditClaim.CommandName = SELECT_ACTION_COMMAND
                    End If

                    e.Item.Cells(GRID_COL_CREATED_DATE).Text = GetDateFormattedStringNullable(CType(dvRow(Certificate.CertificateClaimsDV.COL_CREATED_DATE), Date))
                    e.Item.Cells(GRID_COL_STATUS_CODE).Text = dvRow(Certificate.CertificateClaimsDV.COL_STATUS_CODE).ToString

                    If Not dvRow.Row.IsNull(Certificate.CertificateClaimsDV.COL_AUTHORIZED_AMOUNT) Then
                        e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT).Text = GetAmountFormattedString(CType(dvRow(Certificate.CertificateClaimsDV.COL_AUTHORIZED_AMOUNT), Decimal))
                    Else
                        e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT).Text = GetAmountFormattedString(0)
                    End If

                    If Not dvRow.Row.IsNull(Certificate.CertificateClaimsDV.COL_TOTAL_PAID) Then
                        e.Item.Cells(GRID_COL_TOTAL_PAID).Text = GetAmountFormattedString(CType(dvRow(Certificate.CertificateClaimsDV.COL_TOTAL_PAID), Decimal))
                    Else
                        e.Item.Cells(GRID_COL_TOTAL_PAID).Text = GetAmountFormattedString(0)
                    End If

                    e.Item.Cells(GRID_COL_EXTENDED_STATUS).Text = dvRow(Certificate.CertificateClaimsDV.COL_EXTENDED_STATUS).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moClaimsDatagrid.PageIndexChanged

            Try
                State.PageIndexClaimsGrid = e.NewPageIndex
                State.selectedClaimItemId = Guid.Empty
                PopulateClaimsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moClaimsDatagrid.SortCommand
            Try
                If State.ClaimsSortExpression.StartsWith(e.SortExpression) Then
                    If State.ClaimsSortExpression.EndsWith(" DESC") Then
                        State.ClaimsSortExpression = e.SortExpression
                    Else
                        State.ClaimsSortExpression &= " DESC"
                    End If
                Else
                    State.ClaimsSortExpression = e.SortExpression
                End If
                State.selectedClaimItemId = Nothing
                State.PageIndex = 0
                PopulateClaimsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Items Datagrid Related"

        Public Sub PopulateItemsGrid()
            Dim blnAddItemAllowed, blnAddItemAfterExpired As Boolean
            Dim i As Integer, blnCoveragesExpired As Boolean
            Dim endDate, todayDate As Date
            Dim cert As Certificate = State.MyBO
            Dim dv As CertItem.CertItemSearchDV = CertItem.GetItems(State.MyBO.Id)
            'Me.tsHoriz.Items(Me.CERT_ITEMS_INFO_TAB).Text = TranslationBase.TranslateLabelOrMessage("ITEMS")

            Dim dvPrdSS As DataView
            dvPrdSS = Certificate.ValidateProductForSpecialServices(cert.DealerId, cert.ProductCode)
            dvPrdSS.RowFilter = "add_item_allowed='Y'"
            If dvPrdSS.Count > 0 Then
                blnAddItemAllowed = True
            End If

            dvPrdSS.RowFilter = ""
            dvPrdSS.RowFilter = "add_item_after_expired='Y'"
            If dvPrdSS.Count > 0 Then
                blnAddItemAfterExpired = True
            End If

            Dim dvItemCov As DataView = CertItemCoverage.GetItemCoverages(State.MyBO.Id)
            dvItemCov.RowFilter = "end_date >= '" & todayDate.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture) & "'"
            If dvItemCov.Count <= 0 Then
                blnCoveragesExpired = True
            Else
                blnCoveragesExpired = False
            End If

            ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, False)
            If (cert.Product.AllowRegisteredItems = Codes.EXT_YESNO_Y And cert.StatusCode = CERT_STATUS) Then
                ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, True)
            End If
            If (cert.StatusCode = CERT_STATUS And blnAddItemAllowed) Then
                ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, True)
            End If

            If (cert.StatusCode = CERT_STATUS And blnCoveragesExpired And blnAddItemAfterExpired And blnAddItemAllowed) Then
                ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, True)
            End If

            If (cert.StatusCode = CERT_CANCEL_STATUS And blnAddItemAfterExpired And blnAddItemAllowed) Then
                If State.MyBO.TheCertCancellationBO.getCancellationReasonCode = CANCEL_REASON_EXP Then
                    ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, True)
                End If
            End If

            If dv.Count < 1 Then
                State.isItemsGridVisible = False
                'Me.EnableDisableTabs(False)
                Exit Sub
            End If
            'dv.Sort = CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE
            Dim ItemsRow As DataRow = dv.Table.Rows(0)
            ItemsGrid.AutoGenerateColumns = False
            ItemsGrid.Columns(ITEMS_GRID_COL_ITEM_NUMBER_IDX).SortExpression = CertItem.CertItemSearchDV.COL_ITEM_NUMBER
            ItemsGrid.Columns(ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).SortExpression = CertItem.CertItemSearchDV.COL_RISK_TYPE
            ItemsGrid.Columns(ITEMS_GRID_COL_ITEM_DESCIPTION_IDX).SortExpression = CertItem.CertItemSearchDV.COL_ITEM_DESCRIPTION
            ItemsGrid.Columns(ITEMS_GRID_COL_MAKE_IDX).SortExpression = CertItem.CertItemSearchDV.COL_MAKE
            ItemsGrid.Columns(ITEMS_GRID_COL_MODEL_IDX).SortExpression = CertItem.CertItemSearchDV.COL_MODEL
            ItemsGrid.Columns(ITEMS_GRID_COL_EXPIRATION_DATE_IDX).SortExpression = CertItem.CertItemSearchDV.COL_EXPIRATION_DATE
            ItemsGrid.Columns(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).SortExpression = CertItem.CertItemSearchDV.COL_BENEFIT_STATUS

            ItemsGrid.EditIndex = -1
            'SetPageAndSelectedIndexFromGuid(EndorseDV, Me.State.SelectedCommentId, Me.EndorsementsGrid, Me.State.EndorsementsPageIndex)
            ItemsGrid.PageIndex = State.ItemsPageIndex
            ItemsGrid.DataSource = dv
            ItemsGrid.DataBind()

            'Dispaly benefit status column only if the flag is turned on
            If (Not String.IsNullOrWhiteSpace(State.MyBO.Product.BenefitEligibleFlagXCD) AndAlso State.MyBO.Product.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y) Then
                ItemsGrid.Columns(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Visible = True
            Else
                ItemsGrid.Columns(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Visible = False
            End If

            ' Original retail price captured at cert item level. Populated in certificate generl tab working on the assumption that
            ' wireless customers using this field will have only one cert item -- VS

            If Not (ItemsRow.Item(CertItem.CertItemSearchDV.COL_ORIGINAL_RETAIL_PRICE).Equals(DBNull.Value)) And dv.Count = 1 Then
                PopulateControlFromBOProperty(moOriginalRetailPriceText, CType(ItemsRow.Item(CertItem.CertItemSearchDV.COL_ORIGINAL_RETAIL_PRICE), Decimal), DECIMAL_FORMAT)
            Else
                PopulateControlFromBOProperty(moOriginalRetailPriceText, Nothing)
            End If

            ControlMgr.SetVisibleControl(Me, ItemsGrid, State.isItemsGridVisible)

        End Sub


        Public Sub ItemsGrid_ItemCommand(source As Object, e As GridViewCommandEventArgs) Handles ItemsGrid.RowCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.TheItemsState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = State.TheItemsState.selectedItemId
                    State.NavigateToItems = True
                    NavController.Navigate(Me, "item_selected")
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemsGrid_ItemCreated(sender As Object, e As GridViewRowEventArgs) Handles ItemsGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ItemsGrid_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles ItemsGrid.RowDataBound
            Dim btnEditItem As LinkButton
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(ITEMS_GRID_COL_ITEM_NUMBER_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_ITEM_NUMBER).ToString
                    If (e.Row.Cells(ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).FindControl(ITEMS_GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).FindControl(ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItem.CertItemSearchDV.COL_CERT_ITEM_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(CertItem.CertItemSearchDV.COL_RISK_TYPE).ToString
                    End If
                    e.Row.Cells(ITEMS_GRID_COL_ITEM_DESCIPTION_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_ITEM_DESCRIPTION).ToString
                    e.Row.Cells(ITEMS_GRID_COL_MAKE_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_MAKE).ToString
                    e.Row.Cells(ITEMS_GRID_COL_MODEL_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_MODEL).ToString
                    If (dvRow(CertItem.CertItemSearchDV.COL_EXPIRATION_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(ITEMS_GRID_COL_EXPIRATION_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItem.CertItemSearchDV.COL_EXPIRATION_DATE), Date))
                    End If
                    e.Row.Cells(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_BENEFIT_STATUS).ToString()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ItemsGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ItemsGrid.PageIndexChanging
            Try
                State.TheItemCoverageState.PageIndex = e.NewPageIndex
                State.TheItemCoverageState.selectedItemId = Guid.Empty
                PopulateItemsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Registered Items Datagrid Related"
        Public Sub PopulateRegisterItemsGrid()
            Dim cert As Certificate = State.MyBO
            Dim dv As CertItem.CertRegItemSearchDV = CertItem.GetRegisteredItems(State.MyBO.Id)

            ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, False)
            If (cert.Product.AllowRegisteredItems = Codes.EXT_YESNO_Y And cert.StatusCode = CERT_STATUS) Then
                ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, True)
            End If

            If dv.Count < 1 Then
                State.isRegItemsGridVisible = False
                Exit Sub
            End If

            Dim ItemsRow As DataRow = dv.Table.Rows(0)
            RegisteredItemsGrid.AutoGenerateColumns = False
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_REGISTERED_ITEM_NAME
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_DEVICE_TYPE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_DEVICE_TYPE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_ITEM_DESCRIPTION_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_ITEM_DESCRIPTION
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_MAKE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_MAKE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_MODEL_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_MODEL
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_PURCHASE_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_PURCHASE_PRICE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_PURCHASE_PRICE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_SERIAL_NUMBER_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_SERIAL_NUMBER
            'REQ-6002
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_REGISTRATION_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_RETAIL_PRICE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_EXPIRATION_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE
            RegisteredItemsGrid.Columns(REG_ITEMS_COL_ITEM_STATUS_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_ITEM_STATUS

            RegisteredItemsGrid.EditIndex = -1
            RegisteredItemsGrid.PageIndex = State.ItemsPageIndex
            RegisteredItemsGrid.DataSource = dv
            RegisteredItemsGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, RegisteredItemsGrid, State.isRegItemsGridVisible)

        End Sub

        Public Sub RegisteredItemsGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RegisteredItemsGrid.RowCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.TheItemsState.selectedRegisteredItemId = New Guid(e.CommandArgument.ToString())
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERT_REGISTERED_ITEM_ID) = State.TheItemsState.selectedRegisteredItemId
                    State.NavigateToItems = True
                    NavController.Navigate(Me, "register_item_selected")

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Public Sub RegisteredItemsGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RegisteredItemsGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub
        Public Sub RegisteredItemsGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RegisteredItemsGrid.RowDataBound
            Dim btnRegEditItem As LinkButton
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (e.Row.Cells(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).FindControl(REG_ITEMS_GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                        btnRegEditItem = CType(e.Row.Cells(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).FindControl(REG_ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnRegEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItem.CertRegItemSearchDV.COL_CERT_REGISTERED_ITEM_ID), Byte()))
                        btnRegEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnRegEditItem.Text = dvRow(CertItem.CertRegItemSearchDV.COL_REGISTERED_ITEM_NAME).ToString
                    End If
                    e.Row.Cells(REG_ITEMS_COL_DEVICE_TYPE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_DEVICE_TYPE).ToString
                    e.Row.Cells(REG_ITEMS_COL_ITEM_DESCRIPTION_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_DESCRIPTION).ToString
                    e.Row.Cells(REG_ITEMS_COL_MAKE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_MAKE).ToString
                    e.Row.Cells(REG_ITEMS_COL_MODEL_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_MODEL).ToString
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_PURCHASE_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE), Date))
                    End If
                    e.Row.Cells(REG_ITEMS_COL_PURCHASE_PRICE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_PRICE).ToString
                    e.Row.Cells(REG_ITEMS_COL_SERIAL_NUMBER_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_SERIAL_NUMBER).ToString
                    'REQ-6002
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_REGISTRATION_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE), Date))
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE) IsNot DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_RETAIL_PRICE_IDX).Text = GetAmountFormattedString(CType(dvRow(CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE), Decimal))
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_EXPIRATION_DATE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE)
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_STATUS) IsNot DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_ITEM_STATUS_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_STATUS)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RegisteredItemsGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RegisteredItemsGrid.PageIndexChanging
            Try
                State.TheItemCoverageState.PageIndex = e.NewPageIndex
                State.TheItemCoverageState.selectedItemId = Guid.Empty
                PopulateRegisterItemsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CheckIfComingFromItems()
            If State.NavigateToItems Then
                'If tsHoriz.Items(CERT_ITEMS_INFO_TAB).Enabled = True Then Me.tsHoriz.SelectedIndex = CERT_ITEMS_INFO_TAB
                If isTabEnabled(CERT_ITEMS_INFO_TAB) = True Then State.selectedTab = CERT_ITEMS_INFO_TAB

                State.NavigateToItems = False
            End If

        End Sub
#End Region

#Region "Installment History Datagrid Related"
        Public Sub PopulateInstallmentHistoryGrid()

            Try
                If (State.CertInstallmentHistoryDV Is Nothing) Then
                    State.CertInstallmentHistoryDV = Certificate.GetCertInstallmentHistoryInfo(State.MyBO.Id)
                End If

                CertInstallmentHistoryGrid.PageSize = State.PageSize
                If State.CertInstallmentHistoryDV.Count > 0 Then
                    State.CertInstallmentHistoryDV.Sort = State.CertInstallmentHistorySortExpression

                    SetPageAndSelectedIndexFromGuid(State.CertInstallmentHistoryDV, Guid.Empty, CertInstallmentHistoryGrid, State.PageIndex)
                    CertInstallmentHistoryGrid.DataSource = State.CertInstallmentHistoryDV
                    State.PageIndex = CertInstallmentHistoryGrid.PageIndex

                    HighLightSortColumn(CertInstallmentHistoryGrid, State.CertInstallmentHistorySortExpression, IsNewUI)
                    CertInstallmentHistoryGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertInstallmentHistoryGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, CertInstallmentHistoryGrid.Visible)
                    Session("recCount") = State.CertInstallmentHistoryDV.Count
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), True)
                    EnableTab(CERT_REPRICE_TAB, True)
                End If
            Catch ex As Exception

            End Try

        End Sub


#End Region

#Region "Comments Datagrid Related "

        'The Binding LOgic is here  
        Private Sub CommentsGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CommentsGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (e.Item.Cells(GRID_COL_TIME_STAMP).FindControl(COMMENT_GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Item.Cells(GRID_COL_TIME_STAMP).FindControl(ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Comment.CommentSearchDV.COL_COMMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        Dim createdDate As Date = CType(dvRow(Comment.CommentSearchDV.COL_CREATED_DATE), Date)
                        btnEditItem.Text = GetLongDateFormattedString(createdDate)
                    End If
                    e.Item.Cells(GRID_COL_CALLER_NAME).Text = dvRow(Comment.CommentSearchDV.COL_CALLER_NAME).ToString
                    e.Item.Cells(GRID_COL_ADDED_BY).Text = dvRow(Comment.CommentSearchDV.COL_ADDED_BY).ToString
                    e.Item.Cells(GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CommentsGrid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CommentsGrid.SortCommand
            Try
                If State.CommentsSortExpression.StartsWith(e.SortExpression) Then
                    If State.CommentsSortExpression.EndsWith(" DESC") Then
                        State.CommentsSortExpression = e.SortExpression
                    Else
                        State.CommentsSortExpression &= " DESC"
                    End If
                Else
                    State.CommentsSortExpression = e.SortExpression
                End If
                State.SelectedCommentId = Nothing
                State.PageIndex = 0
                PopulateCommentsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub CommentsGrid_ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles CommentsGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    State.SelectedCommentId = New Guid(e.CommandArgument.ToString())
                    Dim originalComment As Comment = New Comment(State.SelectedCommentId)
                    Dim newComment As Comment = Comment.GetNewComment(originalComment)
                    State.NavigateToComment = True
                    NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(newComment))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub CommentsGrid_ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CommentsGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CommentsGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CommentsGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                State.CommentsPageIndex = e.NewPageIndex
                State.SelectedCommentId = Guid.Empty
                PopulateCommentsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateCommentsGrid()
            Dim cert As Certificate = State.MyBO
            Dim dv As Comment.CommentSearchDV = Comment.getList(State.MyBO.Id)
            dv.Sort = State.CommentsSortExpression

            CommentsGrid.AutoGenerateColumns = False
            CommentsGrid.Columns(GRID_COL_ADDED_BY).SortExpression = Comment.CommentSearchDV.COL_ADDED_BY
            CommentsGrid.Columns(GRID_COL_CALLER_NAME).SortExpression = Comment.CommentSearchDV.COL_CALLER_NAME
            CommentsGrid.Columns(GRID_COL_TIME_STAMP).SortExpression = Comment.CommentSearchDV.COL_CREATED_DATE
            CommentsGrid.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(dv, State.SelectedCommentId, CommentsGrid, State.CommentsPageIndex)
            State.CommentsPageIndex = CommentsGrid.CurrentPageIndex
            CommentsGrid.DataSource = dv
            CommentsGrid.DataBind()

            'Me.tsHoriz.Items(Me.CERT_COMMENTS_TAB).Text = TranslationBase.TranslateLabelOrMessage("COMMENTS") & " : " & dv.Count.ToString()
            lblTabCommentHeader.Text = TranslationBase.TranslateLabelOrMessage("COMMENTS") & " : " & dv.Count.ToString()

            ControlMgr.SetVisibleControl(Me, CommentsGrid, State.IsCommentsGridVisible)
        End Sub
#End Region

#Region "Data Protection Datagrid Related "

        'Restrict functionality
        Public Sub PopulateDataProtection()
            Try
                Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(State.MyBO.Id)
                dv.Sort = State.CommentsSortExpression
                GridDataProtection.AutoGenerateColumns = False
                GridDataProtection.DataSource = dv
                GridDataProtection.DataBind()
                AddGridLabelDecorations()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnRestrict_Click(sender As Object, e As EventArgs) Handles btnRestrict.Click
            Try
                State.DataProtectionHistBO = New DataProtectionHistory()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                With State.DataProtectionHistBO
                    State.DataProtectionHistBO.RestrictedStatus = RestrictCode
                End With
                AddNewRowToGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Try
                If (State.DataProtectionHistBO.RestrictedStatus = RestrictCode Or State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    State.DBCommentBO = State.DataProtectionHistBO.AddComments(Guid.Empty)
                    State.DBCertificateBO = State.DataProtectionHistBO.AddCertificate(State.MyBO.Id)
                ElseIf (State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    State.DBCommentBO = State.CertForgotBO.AddComment(Guid.Empty)
                    State.DBCertificateBO = State.CertForgotBO.AddCertificate(State.MyBO.Id)
                End If
                If (State.DataProtectionHistBO.RestrictedStatus = RestrictCode) Then
                    'Save for Restriction
                    PopulateDataProtectionBOsFormFrom(RestrictCode)
                    If (ValidateDataProtectionFields(RestrictCode)) Then
                        State.DBCommentBO.Validate()
                        State.DataProtectionHistBO.Save()
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                        ControlMgr.SetVisibleControl(Me, btnNewClaim, False)
                        ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                        ControlMgr.SetVisibleControl(Me, btnCancel, False)
                        ControlMgr.SetVisibleControl(Me, btnSave, False)
                        ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                        GridDataProtection.ShowFooter = False
                        PopulateDataProtection()
                        ClearStateProperties()
                        ' To refresh user control 'Restricted' label
                        moCertificateInfoController = UserCertificateCtr
                        moCertificateInfoController.InitController(State.MyBO.Id, , State.companyCode)
                    End If
                ElseIf (State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    'Save for UnRestriction
                    PopulateDataProtectionBOsFormFrom(UnRestrictCode)
                    If (ValidateDataProtectionFields(UnRestrictCode)) Then
                        State.DBCommentBO.Validate()
                        State.DataProtectionHistBO.Save()
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                        ControlMgr.SetVisibleControl(Me, btnNewClaim, True)
                        ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                        ControlMgr.SetVisibleControl(Me, btnCancel, False)
                        ControlMgr.SetVisibleControl(Me, btnSave, False)
                        ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, True)
                        If State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                            ControlMgr.SetVisibleControl(Me, btnRightToForgotten, True)
                        End If
                        GridDataProtection.ShowFooter = False
                        PopulateDataProtection()
                        ClearStateProperties()
                        ' To refresh user control 'Restricted' label
                        moCertificateInfoController = UserCertificateCtr
                        moCertificateInfoController.InitController(State.MyBO.Id, , State.companyCode)
                    End If
                ElseIf (State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    PopulateDataProtectionBOsFormFrom(ForgottenCode)
                    If (ValidateDataProtectionFields(ForgottenCode)) Then
                        State.DBCommentBO.Validate()
                        State.CertForgotBO.Validate()
                        DisplayMessage(Message.MSG_PROMPT_FOR_FORGOTTEN, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSavePromptResponse)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                If (State.DataProtectionHistBO IsNot Nothing AndAlso State.DataProtectionHistBO.RestrictedStatus = RestrictCode) Then
                    ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)

                ElseIf (State.DataProtectionHistBO IsNot Nothing AndAlso State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnRestrict, False)

                ElseIf (State.CertForgotBO IsNot Nothing AndAlso Not State.MyBO.CertificateIsRestricted AndAlso State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)

                ElseIf (State.CertForgotBO IsNot Nothing AndAlso State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnRestrict, False)

                End If
                If State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Not State.MyBO.CertificateIsRestricted Then
                    ControlMgr.SetVisibleControl(Me, btnRightToForgotten, True)
                Else
                    ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                End If


                ControlMgr.SetVisibleControl(Me, btnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnSave, False)

                If (State.CertForgotBO IsNot Nothing) Then
                    With State.CertForgotBO
                        State.CertForgotBO.RestrictedStatus = String.Empty
                    End With
                End If

                If (State.DataProtectionHistBO IsNot Nothing) Then
                    With State.DataProtectionHistBO
                        State.DataProtectionHistBO.RestrictedStatus = String.Empty
                    End With
                End If

                GridDataProtection.ShowFooter = False
                PopulateDataProtection()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub GridDataProtection_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridDataProtection.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If (e.Row.RowType = DataControlRowType.DataRow) Then
                    e.Row.Cells(GDP_COL_REQUEST_ID).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_REQUEST_ID).ToString
                    e.Row.Cells(GDP_COL_COMMENTS).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_COMMENTS).ToString
                    e.Row.Cells(GDP_COL_ADDED_BY).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_ADDED_BY).ToString
                    e.Row.Cells(GDP_COL_TIME_STAMP).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_CREATED_DATE).ToString
                    e.Row.Cells(GDP_COL_STATUS).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_STATUS).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub AddGridLabelDecorations()
            Try
                Dim lblRequestHeader As Label
                Dim lblCommentHeader As Label
                lblRequestHeader = CType(GridDataProtection.HeaderRow.FindControl(GRID_CTRL_NAME_REQUEST_ID_HEADER), Label)
                lblCommentHeader = CType(GridDataProtection.HeaderRow.FindControl(GRID_CTRL_NAME_COMMENT_HEADER), Label)
                lblRequestHeader.Text = "<span class=""mandatory"">*</span>" & TranslationBase.TranslateLabelOrMessage(lblRequestHeader.Text)
                lblCommentHeader.Text = "<span class=""mandatory"">*</span>" & TranslationBase.TranslateLabelOrMessage(lblCommentHeader.Text)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNewRowToGrid()
            Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(State.MyBO.Id)
            Dim dt As DataTable, row As DataRow
            dt = dv.Table
            row = dt.NewRow
            If (dv.Count = 0) Then
                dt.Rows.Add(row)
            End If
            GridDataProtection.ShowFooter = True
            GridDataProtection.DataSource = dt
            GridDataProtection.DataBind()
            AddGridLabelDecorations()
        End Sub

        Protected Sub PopulateDataProtectionBOsFormFrom(actionCode As String)
            Try
                Dim ind As Integer = GridDataProtection.EditIndex
                Dim commentTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCommentTypeLookupList(Authentication.LangId), CommentCode)
                Dim restrictionId As Guid = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(RestrictionListCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), actionCode)

                If (actionCode.Equals("R") Or actionCode.Equals("UR")) Then
                    'Logic For Restriction And  UnRestriction '

                    With State.DBCommentBO
                        PopulateBOProperty(State.DBCommentBO, "Comments", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_COMMENT), TextBox))
                        PopulateBOProperty(State.DBCommentBO, "CommentTypeId", commentTypeId)
                        State.DBCommentBO.CertId = State.MyBO.Id
                        State.DBCommentBO.CallerName = CallerName
                    End With

                    With State.DBCertificateBO
                        If (actionCode.Equals("R")) Then
                            State.DBCertificateBO.IsRestricted = "Y"
                        ElseIf (actionCode.Equals("UR")) Then
                            State.DBCertificateBO.IsRestricted = "N"
                        End If

                    End With
                    With State.DataProtectionHistBO
                        PopulateBOProperty(State.DataProtectionHistBO, "RequestId", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_REQUEST_ID), TextBox))
                        State.DataProtectionHistBO.EntityId = State.MyBO.Id
                        State.DataProtectionHistBO.CommentId = State.DBCommentBO.Id
                        PopulateBOProperty(State.DataProtectionHistBO, "AddedBy", ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        PopulateBOProperty(State.DataProtectionHistBO, "EntityType", EntityType)
                        PopulateBOProperty(State.DataProtectionHistBO, "Status", restrictionId)
                        State.DataProtectionHistBO.IsRequestIdUsed = State.DataProtectionHistBO.GetRequestIdUsedInfo(State.DataProtectionHistBO.RequestId, actionCode, State.MyBO.Id)
                    End With
                ElseIf (actionCode.Equals("F")) Then
                    ' Logic For Right To Forgotten'

                    With State.CertForgotBO
                        State.CertForgotBO.CertId = State.MyBO.Id
                        State.CertForgotBO.CertNumber = State.MyBO.CertNumber
                        State.CertForgotBO.DealerId = State.MyBO.DealerId
                        State.CertForgotBO.IsForgotten = "N"
                        PopulateBOProperty(State.CertForgotBO, "RequestId", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_REQUEST_ID), TextBox))
                        State.CertForgotBO.IsRequestIdUsed = State.DataProtectionHistBO.GetRequestIdUsedInfo(State.CertForgotBO.RequestId, actionCode, State.MyBO.Id)
                    End With


                    With State.DBCommentBO
                        PopulateBOProperty(State.DBCommentBO, "Comments", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_COMMENT), TextBox))
                        PopulateBOProperty(State.DBCommentBO, "CommentTypeId", commentTypeId)
                        State.DBCommentBO.CallerName = CallerName
                        State.DBCommentBO.ForgotRequestId = State.CertForgotBO.Id

                    End With
                    With State.DBCertificateBO
                        State.DBCertificateBO.IsRestricted = "D"
                    End With
                End If


                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function ValidateDataProtectionFields(actionCode As String)
            Dim blnSuccess As Boolean = True

            If State.DBCommentBO.Comments = String.Empty Then
                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("COMMENT") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnSuccess = False
            End If

            If (actionCode.Equals("R") Or actionCode.Equals("UR")) Then
                If State.DataProtectionHistBO.RequestId = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("REQUEST_ID") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                    blnSuccess = False
                End If

                If State.DataProtectionHistBO.IsRequestIdUsed Then
                    MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.REQUEST_ID_IS_USED_ERR))
                    blnSuccess = False
                End If
            ElseIf (actionCode.Equals("F")) Then
                If State.CertForgotBO.RequestId = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("REQUEST_ID") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                    blnSuccess = False
                End If

                If State.CertForgotBO.IsRequestIdUsed Then
                    MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.REQUEST_ID_IS_USED_ERR))
                    blnSuccess = False
                End If
            End If

            Return blnSuccess
        End Function

        Private Sub ClearStateProperties()
            With State
                .DBCommentBO.Comments = ""
                .DBCommentBO.CommentTypeId = Guid.Empty
                .DBCommentBO.CertId = Guid.Empty
                .DBCommentBO.CallerName = ""
                If (.CertForgotBO IsNot Nothing) Then
                    .CertForgotBO.IsForgotten = String.Empty
                    .CertForgotBO.CertId = Guid.Empty
                    .CertForgotBO.CertNumber = String.Empty
                    .CertForgotBO.IsForgotten = String.Empty
                    .CertForgotBO.IsRequestIdUsed = False
                Else
                    .DataProtectionHistBO.RequestId = ""
                    .DataProtectionHistBO.RestrictedStatus = ""
                    .DataProtectionHistBO.EntityType = ""
                    .DBCertificateBO.IsRestricted = String.Empty
                    .DataProtectionHistBO.IsRequestIdUsed = False
                End If

            End With
        End Sub

        'UnRestrict functionality
        Protected Sub btnUnRestrict_Click(sender As Object, e As EventArgs) Handles btnUnRestrict.Click
            Try
                State.DataProtectionHistBO = New DataProtectionHistory()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)

                With State.DataProtectionHistBO
                    State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode
                End With
                AddNewRowToGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Right To Forgotten "

        Protected Sub btnRightToForgotten_Click(sender As Object, e As EventArgs) Handles btnRightToForgotten.Click
            Try
                State.DataProtectionHistBO = New DataProtectionHistory()
                State.CertForgotBO = New CertForgotRequest()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                With State.CertForgotBO
                    State.CertForgotBO.RestrictedStatus = ForgottenCode
                End With
                AddNewRowForRightToForgotten()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SaveRightToForgotten()
            If HiddenSavePromptResponse.Value IsNot Nothing AndAlso HiddenSavePromptResponse.Value = MSG_VALUE_YES Then
                State.CertForgotBO.Save()
                ClearStateProperties()
                Redirect(URL_CertList)
            End If
            HiddenSavePromptResponse.Value = ""
        End Sub

        Private Sub AddNewRowForRightToForgotten()

            Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(State.MyBO.Id)
            Dim dt As DataTable, row As DataRow
            dt = dv.Table.Clone()
            row = dt.NewRow
            dt.Rows.Add(row)
            GridDataProtection.ShowFooter = True
            GridDataProtection.DataSource = dt
            GridDataProtection.DataBind()
            AddGridLabelDecorations()

        End Sub

#End Region

#Region "Endorsements Datagrid Related "

        'The Binding Logic is here  
        Private Sub EndorsementsGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles EndorsementsGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (e.Item.Cells(GRID_COL_ENDORSE_NUMBER).FindControl(ENDORSEMENT_GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Item.Cells(GRID_COL_ENDORSE_NUMBER).FindControl(ENDORSEMENT_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSE_NUMB).ToString
                    End If
                    e.Item.Cells(GRID_COL_ENDORSE_CREATED_BY).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ADDED_BY).ToString
                    Dim createdDate As Date = CType(dvRow(CertEndorse.EndorseSearchDV.COL_CREATED_DATE), Date)
                    e.Item.Cells(GRID_COL_ENDORSE_CREATED_DATE).Text = GetLongDateFormattedString(createdDate)
                    e.Item.Cells(GRID_COL_ENDORSE_TYPE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_TYPE).ToString
                    e.Item.Cells(GRID_COL_ENDORSE_ENDORSEMENT_REASON).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_REASON).ToString

                    e.Item.Cells(GRID_COL_ENDORSE_EFFECTIVE_DATE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_EFFECTIVE_DATE).ToString

                    e.Item.Cells(GRID_COL_ENDORSE_EXPIRATION_DATE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_EXPIRATION_DATE).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub EndorsementsGrid_ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EndorsementsGrid.ItemCommand

            Try
                If e.CommandName = "SelectAction" Then
                    State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = State.TheItemCoverageState.selectedItemId
                    State.NavigateToEndorment = True
                    NavController.Navigate(Me, FlowEvents.EVENT_ENDORSEMENT_SELECTED, New EndorsementDetailForm.Parameters(State.TheItemCoverageState.manufaturerWarranty))

                    'Me.NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))

                End If
                '"endorsement_detail"
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub EndorsementsGrid_ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles EndorsementsGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EndorsementsGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles EndorsementsGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                State.EndorsementsPageIndex = e.NewPageIndex
                State.SelectedEndorseId = Guid.Empty
                PopulateEndorsementsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateEndorsementsGrid()

            If Not State.IsEndorsementsGridVisible Then
                Exit Sub
            End If

            Dim cert As Certificate = State.MyBO
            Dim ItemDV As CertItem.CertItemSearchDV = CertItem.GetItems(State.MyBO.Id)
            Dim dv As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(State.MyBO.Id)

            dv.Sort = CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE
            If dv.Count > 0 Then
                Dim coverageRow As DataRow = dv.Table.Rows(0)
            End If

            If ItemDV.Count > 1 Or cert.StatusCode <> CERT_STATUS Or State.MyBO.IsChildCertificate Then
                btnAddEndorsement_WRITE.Enabled = False
            End If

            State.TheItemCoverageState.cert_item_id = New Guid(CType(ItemDV(0)(CertItem.CertItemSearchDV.COL_CERT_ITEM_ID), Byte()))
            Dim EndorseDV As CertEndorse.EndorseSearchDV = CertEndorse.getList(State.MyBO.Id)

            EndorsementsGrid.AutoGenerateColumns = False
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_NUMBER).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSE_NUMB
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_CREATED_BY).SortExpression = CertEndorse.EndorseSearchDV.COL_ADDED_BY
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_CREATED_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_CREATED_DATE
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_TYPE).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_TYPE
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_ENDORSEMENT_REASON).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_REASON
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_EFFECTIVE_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_EFFECTIVE_DATE
            EndorsementsGrid.Columns(GRID_COL_ENDORSE_EXPIRATION_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_EXPIRATION_DATE

            EndorsementsGrid.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(EndorseDV, State.SelectedCommentId, EndorsementsGrid, State.EndorsementsPageIndex)
            EndorsementsGrid.CurrentPageIndex = State.EndorsementsPageIndex
            EndorsementsGrid.DataSource = EndorseDV
            EndorsementsGrid.DataBind()

            'Me.tsHoriz.Items(Me.CERT_ENDORSEMENTS_TAB).Text = TranslationBase.TranslateLabelOrMessage("ENDORSEMENTS") '& " : " & EndorseDV.Count.ToString()

            ControlMgr.SetVisibleControl(Me, EndorsementsGrid, State.IsEndorsementsGridVisible)
        End Sub

#End Region

#Region "Extensions Datagrid Related"
        Public Sub PopulateExtensionsGrid()

            Try
                If (State.CertExtensionsDV Is Nothing) Then
                    State.CertExtensionsDV = Certificate.GetCertExtensionsInfo(State.MyBO.Id)
                End If
                trCertExtn.Visible = False
                CertExtnGrid.PageSize = State.PageSize
                If State.CertExtensionsDV.Count > 0 Then
                    trCertExtn.Visible = True
                    State.CertExtensionsDV.Sort = State.CertExtensionSortExpression

                    SetPageAndSelectedIndexFromGuid(State.CertExtensionsDV, Guid.Empty, CertExtnGrid, State.PageIndex)
                    CertExtnGrid.DataSource = State.CertExtensionsDV
                    State.PageIndex = CertExtnGrid.PageIndex

                    HighLightSortColumn(CertExtnGrid, State.CertExtensionSortExpression, IsNewUI)
                    CertExtnGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertExtnGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, CertExtnGrid.Visible)
                    Session("recCount") = State.CertExtensionsDV.Count
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), True)
                End If
            Catch ex As Exception

            End Try

        End Sub
        Private Sub ExtensionsGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CertExtnGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                State.ExtensionGridPageIndex = e.NewPageIndex
                PopulateExtensionsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Upgrade Data Extensions Datagrid Related"
        Public Sub PopulateUPgradeDataExtensionsGrid()

            Try
                If (State.CertUpgradeExtensionsDV Is Nothing) Then
                    State.CertUpgradeExtensionsDV = Certificate.GetCertUpgradeDataExtensionsInfo(State.MyBO.Id)
                End If

                UpgradeDataGridtr.Visible = False
                CertUpgradeDatagrid.PageSize = State.PageSize
                If State.CertUpgradeExtensionsDV.Count > 0 Then
                    UpgradeDataGridtr.Visible = True
                    State.CertUpgradeExtensionsDV.Sort = State.CertUpgradeExtensionSortExpression

                    SetPageAndSelectedIndexFromGuid(State.CertUpgradeExtensionsDV, Guid.Empty, CertUpgradeDatagrid, State.PageIndex)
                    CertUpgradeDatagrid.DataSource = State.CertUpgradeExtensionsDV

                    HighLightSortColumn(CertUpgradeDatagrid, State.CertUpgradeExtensionSortExpression, IsNewUI)
                    CertUpgradeDatagrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertUpgradeDatagrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, CertUpgradeDatagrid.Visible)
                    Session("recCount") = State.CertUpgradeExtensionsDV.Count
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub CertUpgradeDatagrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CertUpgradeDatagrid.PageIndexChanged
            Try
                State.ExtensionGridPageIndex = e.NewPageIndex
                PopulateUPgradeDataExtensionsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Certificate Extended Fields grid Related"
        Private Sub GridCertExtFields_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GridCertExtFields.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(CERT_EXT_CERT_ID_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_CERT_ID).ToString
                    e.Item.Cells(CERT_EXT_FIELD_NAME_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_FIELD_NAME).ToString
                    e.Item.Cells(CERT_EXT_FIELD_VALUE_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_FIELD_VALUE).ToString
                    e.Item.Cells(CERT_EXT_CREATED_BY_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_CREATED_BY).ToString
                    e.Item.Cells(CERT_EXT_MODIFIED_BY_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_BY).ToString
                    e.Item.Cells(CERT_EXT_CREATED_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(Certificate.CertExtendedFieldsDv.COL_CREATED_DATE), Date))
                    If Not IsDBNull(dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE)) then
                        e.Item.Cells(CERT_EXT_MODIFIED_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE), Date))
                    Else 
                        e.Item.Cells(CERT_EXT_MODIFIED_DATE_IDX).Text = String.Empty
                    End If
                    
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub GridCertExtFields_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GridCertExtFields.SortCommand
            Try
                If State.CertExtFieldsSortExpression.StartsWith(e.SortExpression) Then
                    If State.CertExtFieldsSortExpression.EndsWith(" DESC") Then
                        State.CertExtFieldsSortExpression = e.SortExpression
                    Else
                        State.CertExtFieldsSortExpression &= " DESC"
                    End If
                Else
                    State.CertExtFieldsSortExpression = e.SortExpression
                End If
                State.SelectedCertExtId = Nothing
                State.PageIndex = 0
                PopulateCertExtendedFieldsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Public Sub GridCertExtFields_ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GridCertExtFields.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCertExtFields_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GridCertExtFields.PageIndexChanged
            Try
                State.CertExtFieldsPageIndex = e.NewPageIndex
                State.SelectedCertExtId = Guid.Empty
                PopulateCertExtendedFieldsGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateCertExtendedFieldsGrid()
            Dim cert As Certificate = State.MyBO
            Dim dv As Certificate.CertExtendedFieldsDv = Certificate.GetCertExtensionFieldsList(State.MyBO.Id, Authentication.CurrentUser.LanguageId)
            dv.Sort = State.CertExtFieldsSortExpression

            GridCertExtFields.AutoGenerateColumns = False
            GridCertExtFields.Columns(CERT_EXT_FIELD_NAME_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_FIELD_NAME
            GridCertExtFields.Columns(CERT_EXT_FIELD_VALUE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_FIELD_VALUE
            GridCertExtFields.Columns(CERT_EXT_CREATED_BY_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_CREATED_BY
            GridCertExtFields.Columns(CERT_EXT_CREATED_DATE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_CREATED_DATE
            GridCertExtFields.Columns(CERT_EXT_MODIFIED_BY_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_MODIFIED_BY
            GridCertExtFields.Columns(CERT_EXT_MODIFIED_DATE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE
            GridCertExtFields.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(dv, State.SelectedCertExtId, GridCertExtFields, State.CertExtFieldsPageIndex)
            State.CertExtFieldsPageIndex = GridCertExtFields.CurrentPageIndex
            GridCertExtFields.DataSource = dv
            GridCertExtFields.DataBind()

            lblCertificateExtendedFields.Text = TranslationBase.TranslateLabelOrMessage("CERT_EXT_FIELDS") & " : " & dv.Count.ToString()

            ControlMgr.SetVisibleControl(Me, GridCertExtFields, State.IsCertExtFieldsGridVisible)
        End Sub
        
#End Region

#Region "Button Clicks"

        Private Sub TNCButton_WRITE_Click(sender As Object, e As System.EventArgs) Handles TNCButton_WRITE.Click
            Try
                callPage(Tables.TermAndConditionsForm.URL, State.MyBO)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAddComment_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAddComment_WRITE.Click
            Try
                State.NavigateToComment = True
                NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(Comment.GetNewComment(State.MyBO.Id)))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'Private Sub btnAddCheckPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCheckPayment.Click
        '    Try
        '        Me.State.NavigateToCheckPayment = True
        '        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
        '        Me.NavController.Navigate(Me, CREATE_NEW_CHECK_PAYMENT, New CheckPaymentForm.Parameters(Me.State.MyBO.Id))
        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        Private Sub btnAddEndorsement_WRITE__Click(sender As System.Object, e As System.EventArgs) Handles btnAddEndorsement_WRITE.Click

            Try
                State.NavigateToEndorment = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(State.MyBO.Id, State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewCertItem_WRITE__Click(sender As System.Object, e As System.EventArgs) Handles btnNewCertItem_WRITE.Click

            Try
                State.NavigateToNewCertItem = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                NavController.Navigate(Me, CREATE_NEW_ITEM, New CertAddNewItemForm.Parameters(State.MyBO.Id))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim myBo As Certificate = State.MyBO
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, State.certificateChanged, State.IsCallerAuthenticated)
                    State.selectedTab = 0
                    State.CertHistoryDV = Nothing
                    NavController = Nothing
                    Session(SESSION_KEY_BACKUP_STATE) = New MyState
                    ReturnToCallingPage(retObj)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Try
                    If AddressCtr.MyBO Is Nothing Then
                        Dim myBo As Certificate = State.MyBO
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, State.certificateChanged, State.IsCallerAuthenticated)
                        NavController = Nothing
                        Session(SESSION_KEY_BACKUP_STATE) = New MyState
                        ReturnToCallingPage(retObj)
                    End If
                Catch ex2 As Threading.ThreadAbortException
                Catch ex2 As Exception
                    HandleErrors(ex2, MasterPage.MessageController)
                End Try

                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub


        Private Sub btnNewClaim_Click(sender As Object, e As System.EventArgs) Handles btnNewClaim.Click
            Try
                ' DEF-4245  Clean up the claim BO so any new claims added will be display when returning to this page.
                State.ClaimsearchDV = Nothing

                NavController.FlowSession.Clear()
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                Dim dealer As New Dealer(State.MyBO.DealerId)

                If Not String.IsNullOrEmpty(State.ClaimRecordingXcd) AndAlso
                   State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                    callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(State.MyBO.Id, Nothing, Nothing, Codes.CASE_PURPOSE__REPORT_CLAIM, State.IsCallerAuthenticated))
                Else
                    'REQ-863 Claims Payable
                    'Check whether to use new claim Authorization structure or not
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, dealer.UseClaimAuthorizationId) = Codes.YESNO_N) Then
                        NavController.Navigate(Me, "locate_eligible_coverages")
                    Else
                        Dim certId As Guid = State.MyBO.Id
                        NavController = Nothing
                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step1, certId, Nothing, Nothing, True,, State.IsCallerAuthenticated))
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewClaimDcm_Click(sender As Object, e As System.EventArgs) Handles btnNewClaimDcm.Click
            Try
                ' DEF-4245  Clean up the claim BO so any new claims added will be display when returning to this page.
                State.ClaimsearchDV = Nothing

                NavController.FlowSession.Clear()
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                Dim dealer As New Dealer(State.MyBO.DealerId)

                If Not String.IsNullOrEmpty(State.ClaimRecordingXcd) AndAlso
                   (State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse
                    State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH)) Then
                    callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(State.MyBO.Id, Nothing, Nothing, Codes.CASE_PURPOSE__REPORT_CLAIM))
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveCertDetail_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveCertDetail_WRITE.Click
            Try
                Dim objComp As New Company(State.MyBO.CompanyId)
                Dim strTranferOfOwnership As String
                If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                    strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                Else
                    strTranferOfOwnership = String.Empty
                End If
                If strTranferOfOwnership = YES And State.MyBO.StatusCode = CERT_STATUS Then
                    If State.MyBO.CustomerName IsNot Nothing Then
                        If (State.MyBO.CustomerName <> moCustomerNameText.Text And moCustomerNameText.Text.Trim <> String.Empty) Or (moTaxIdText.Visible = True And State.MyBO.TaxIDNumb <> moTaxIdText.Text) Then
                            DisplayMessage(Message.MSG_TRANSFER_Of_OWNERSHIP_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenTransferOfOwnershipPromptResponse)
                        Else
                            saveCertificate()
                        End If
                    End If
                Else
                    saveCertificate()
                End If
                'KDDI
                'Dim btnValidate_Address As Button = AddressCtr.FindControl(ValidateAddressButton)
                'ControlMgr.SetVisibleControl(Me, btnValidate_Address, False)
                ' Address Validation
                EnableDisableAddressValidation()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveCertInfo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveCertInfo_WRITE.Click
            Try
                saveCertificate()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveTaxID_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSaveTaxID_WRITE.Click
            Try
                Dim objComp As New Company(State.MyBO.CompanyId)
                Dim strTranferOfOwnership As String
                If State.MyBO.TaxIDNumb Is Nothing Then
                    saveCertificate()
                Else
                    If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                        strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                    Else
                        strTranferOfOwnership = String.Empty
                    End If
                    If strTranferOfOwnership = YES And State.MyBO.StatusCode = CERT_STATUS Then
                        If cboDocumentTypeId.SelectedIndex <= 0 Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                        End If
                        If (State.MyBO.TaxIDNumb <> moNewTaxIdText.Text And moNewTaxIdText.Text.Trim <> String.Empty) Or (cboDocumentTypeId.SelectedItem.Text <> State.MyBO.getDocTypeDesc) Then
                            DisplayMessage(Message.MSG_TRANSFER_Of_OWNERSHIP_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenTransferOfOwnershipPromptResponse)
                        Else
                            saveCertificate()
                        End If
                    Else
                        saveCertificate()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDebitSave_WRITE.Click
            Try
                BindInstallmentBoPropertiesToLabels()
                saveCertInstallment(State.TheDirectDebitState.StatusChenge)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitHistory_Click(sender As Object, e As System.EventArgs) Handles btnDebitHistory.Click
            Try
                State.NavigateToBillingHistory = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                callPage(BillingHistoryListForm.URL, State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        'REQ-6189
        Private Sub btnBankInfo_Click(sender As Object, e As System.EventArgs) Handles btnBankInfo.Click
            Try
                State.NavigateToBankInfoHistory = True

                If State.MyBO IsNot Nothing AndAlso Not State.MyBO.Id.Equals(Guid.Empty) Then

                    State.certInstallment = New CertInstallment(State.MyBO.Id, True)

                    If State.certInstallment IsNot Nothing Then

                        Dim bankInfo As BankInfo = New BankInfo()
                        If State.certInstallment.BankInfoId <> Guid.Empty Then
                            REM bankInfo = New BankInfo(Me.State.certInstallment.BankInfoId)
                            Dim currentBankInfo = New BankInfo(State.certInstallment.BankInfoId)

                            With bankInfo
                                .CountryID = currentBankInfo.CountryID
                                .Account_Name = currentBankInfo.Account_Name
                                .Bank_Id = currentBankInfo.Bank_Id
                                .Account_Number = currentBankInfo.Account_Number
                                .SwiftCode = currentBankInfo.SwiftCode
                                .IbanNumber = currentBankInfo.IbanNumber
                                .AccountTypeId = currentBankInfo.AccountTypeId
                                .PaymentReasonID = currentBankInfo.PaymentReasonID
                                .BranchName = currentBankInfo.BranchName
                                .BankName = currentBankInfo.BankName
                                .BankSortCode = currentBankInfo.BankSortCode
                                .BankSubCode = currentBankInfo.BankSubCode
                                .TransactionLimit = currentBankInfo.TransactionLimit
                                .BankLookupCode = currentBankInfo.BankLookupCode
                                .SourceCountryID = currentBankInfo.SourceCountryID
                                .ValidateFieldsforBR = currentBankInfo.ValidateFieldsforBR
                                .DomesticTransfer = currentBankInfo.DomesticTransfer
                                .InternationalTransfer = currentBankInfo.InternationalTransfer
                                .InternationalEUTransfer = currentBankInfo.InternationalEUTransfer
                                .BranchDigit = currentBankInfo.BranchDigit
                                .AccountDigit = currentBankInfo.AccountDigit
                                .BranchNumber = currentBankInfo.BranchNumber
                                .PaymentMethodId = currentBankInfo.PaymentMethodId
                                .PayeeId = currentBankInfo.PayeeId
                                .TaxId = currentBankInfo.TaxId
                            End With
                            bankInfo.CopyOriginalIbanNumber(currentBankInfo.OriginalIbanNumber)
                            bankInfo.SourceCountryID = bankInfo.CountryID
                        Else

                            ' Verify this assumption: Set the countries of the bank info instance to the country of the cert.
                            bankInfo.CountryID = State.MyBO.Company.CountryId
                            bankInfo.SourceCountryID = State.MyBO.Company.CountryId
                        End If

                        bankInfo.ValidateFieldsforFR = True

                        NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                        NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_INSTALLMENT) = State.certInstallment
                        NavController.FlowSession(FlowSessionKeys.SESSION_BANK_INFO) = bankInfo

                        callPage(URL_BANKINFO, State.MyBO.Id)
                    End If
                End If



            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'Nandan
        Private Sub btnPaymentHistory_Click(sender As Object, e As System.EventArgs) Handles btnPaymentHistory.Click
            Try
                State.NavigateToPaymentHistory = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                callPage(PaymentHistoryListForm.URL, State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBillpayHist_click(sender As Object, e As System.EventArgs) Handles btnBillPayHist.Click
            Try
                State.NavigateToBillpayHistory = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                NavController.Navigate(Me, CREATE_NEW_BILLING_PAYMENT, New BillingPaymentHistoryListForm.Parameters(State.MyBO.Id))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoCertDetail_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndoCertDetail_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Certificate(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New Certificate
                End If
                State.IsEdit = False
                PopulateFormFromBOs()
                EnableDisableFields()
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, False)
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, False)
                ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, False)
                'KDDI
                Dim btnValidate_Address As Button = AddressCtr.FindControl(ValidateAddressButton)
                ControlMgr.SetVisibleControl(Me, btnValidate_Address, False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoCertInfo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndoCertInfo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Certificate(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New Certificate
                End If
                State.IsEdit = False
                PopulateFormFromBOs()
                EnableDisableFields()
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, False)
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoTaxID_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndoTaxID_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Certificate(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New Certificate
                End If
                State.IsEdit = False
                PopulateFormFromBOs()
                EnableDisableFields()
                ControlMgr.SetVisibleControl(Me, BtnDocumentIssueDate, False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoDebit_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndoDebit_WRITE.Click

            State.IsEdit = False
            PopulatePremiumInfoTab()
            EnableDisableFields()

        End Sub

        Private Sub btnCancelRequestUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCancelRequestUndo_WRITE.Click

            State.IsEdit = False
            populateCancelRequestInfoTab()
            EnableDisableFields()

            SetFocus(moCancelRequestReasonDrop)
        End Sub
        Private Sub btnEditCertDetail_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEditCertDetail_WRITE.Click
            Try
                State.IsEdit = True
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs()
                EnableDisableFields()
                If moEmailAddressText.Text IsNot Nothing AndAlso moEmailAddressText.Text.Equals(State.EmailIsNull) Then
                    moEmailAddressText.Text = ""
                End If
                If Not State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    SetFocus(moCustomerFirstNameText)
                Else
                    SetFocus(moCustomerNameText)
                End If
                'KDDI
                EnableDisableAddressValidation()
                State.AddressFlag = False

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitEdit_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDebitEdit_WRITE.Click
            Try
                State.IsEdit = True
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs(True)
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEditCertInfo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEditCertInfo_WRITE.Click
            Try
                State.IsEdit = True
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEditTaxID_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnEditTaxID_WRITE.Click
            Try
                State.IsEdit = True
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCancelRequestEdit_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCancelRequestEdit_WRITE.Click
            Try
                State.IsEdit = True
                State.CertCancelRequestId = State.MyBO.getCertCancelRequestID
                populateCancelRequestInfoTab()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnCreateNewRequest_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCreateNewRequest_WRITE.Click
            Try
                State.IsEdit = True
                State.CertCancelRequestId = Guid.Empty
                State.certCancelRequestBO = New CertCancelRequest
                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                populateCancelRequestInfoTab()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnRemoveCancelDueDate_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnRemoveCancelDueDate_WRITE.Click
            Try
                DisplayMessage(Message.REMOVE_CANCELDUEDATE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenRemoveCancelDueDatePromptResponse)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DocumentsButton_Click(sender As System.Object, e As System.EventArgs) Handles DocumentsButton.Click
            Try
                callPage(CertificateDocumentForm.URL, State.MyBO)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCancelCertificate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelCertificate_WRITE.Click
            Try
                WorkingPanelVisible = False

                ControlMgr.SetVisibleControl(Me, RefundAmtTextbox, False)
                ControlMgr.SetVisibleControl(Me, RefundAmtLabel, False)
                If RefundAmtLabel.Text.IndexOf(":") > 0 Then
                    RefundAmtLabel.Text = RefundAmtLabel.Text
                Else
                    RefundAmtLabel.Text = RefundAmtLabel.Text & ":"
                End If
                ControlMgr.SetVisibleControl(Me, LabelWarningRefundAmtBelowTolerance, False)
                'ControlMgr.SetVisibleControl(Me, moAmtPaidTextbox, False)
                'ControlMgr.SetVisibleControl(Me, moAmtPaidLabel, False)
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                '*ANI
                'ControlMgr.SetVisibleControl(Me, PaymentReasonDrop, False)
                'ControlMgr.SetVisibleControl(Me, PaymentReasonDrpLabel, False)
                '*ANI
                ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, False)
                MasterPage.MessageController.Clear()
                moCertificateInfoCtrlCancel = UserCertificateCtrCancel
                moBankInfoController = UserBankInfoCtr
                'moBankInfoController.InitController()
                moBankInfoController.Visible = False
                'Me.State.BankInfoCtrVisible = False

                moPaymentOrderInfoCtrl = UserPaymentOrderInfoCtr
                'moBankInfoController.InitController()
                moPaymentOrderInfoCtrl.Visible = False
                'Me.State.BankInfoCtrVisible = False

                moCertificateInfoCtrlCancel.InitController(State.MyBO.Id, , State.companyCode)
                State.certCancellationBO = New CertCancellation
                If State.ComputeCancellationDateEOfM = Codes.YESNO_Y Then
                    State.certCancellationBO.CancellationRequestedDate = Date.Today
                    State.certCancellationBO.CancellationDate = ComputeEndOfMonth(Date.Today)
                End If
                PopulateCancellationReasonDropdown(moCancellationReasonDrop, Nothing, Nothing)
                PopulateCancelCommentTypeDropdown(moCancelCommentType)
                PopulateControlFromBOProperty(CancelCallerNameTextbox, State.MyBO.CustomerName)
                populateFormFromCertCancellationBO()

                '** populate the paymentmethod drop down here ???
                'Me.PopulatePaymentMethodDropdown(Me.PaymentMethodDrop)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BackCancelCertButton_Click(sender As System.Object, e As System.EventArgs) Handles BackCancelCertButton.Click
            Try
                WorkingPanelVisible = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub QuoteButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles QuoteButton_WRITE.Click
            Dim oCancelCertificateData As New CertCancellationData

            Try
                State.QuotedRefundAmt = Nothing
                State.QuotedInstallmentsPaid = Nothing
                WorkingPanelVisible = False
                MasterPage.MessageController.Clear()
                moBankInfoController.Visible = False
                moPaymentOrderInfoCtrl.Visible = False
                oCancelCertificateData.refundAmount = Nothing
                oCancelCertificateData.InstallmentsPaid = Nothing
                State.BankInfoBO = Nothing
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, False)
                'Me.trbank.Visible = True

                PopulateBOProperty(State.certCancellationBO, "CancellationReasonId", moCancellationReasonDrop)
                PopulateBOProperty(State.certCancellationBO, "CancellationDate", CancelCertDateTextbox)
                PopulateBOProperty(State.certCancellationBO, "CancellationRequestedDate", CancelCertReqDateTextbox)

                State.MyBO.QuoteCancellation(State.certCancellationBO, oCancelCertificateData)
                If Not oCancelCertificateData.errorExist Then
                    If Not oCancelCertificateData.inputAmountRequiredMissing Then
                        ControlMgr.SetVisibleControl(Me, RefundAmtTextbox, True)
                        ControlMgr.SetVisibleControl(Me, RefundAmtLabel, True)
                        RefundAmtTextbox.ReadOnly = True

                        'allow cancellation even if the computed refund is below company tolerance amt, but dont show the payment mthd
                        ' because refund_amount should be zero here.
                        If oCancelCertificateData.refund_amt_reset = "Y" Then 'AndAlso oCancelCertificateData.creditIssued = "N" Then
                            If oCancelCertificateData.refundAmount <> 0 Then
                                ControlMgr.SetVisibleControl(Me, LabelWarningRefundAmtBelowTolerance, True)
                            End If
                        Else
                            ControlMgr.SetVisibleControl(Me, LabelWarningRefundAmtBelowTolerance, False)
                        End If
                        PopulateControlFromBOProperty(RefundAmtTextbox, oCancelCertificateData.refundAmount)
                        PopulateControlFromBOProperty(txtInstallmentsPaid, oCancelCertificateData.InstallmentsPaid)
                        State.QuotedRefundAmt = oCancelCertificateData.refundAmount
                        State.QuotedInstallmentsPaid = oCancelCertificateData.InstallmentsPaid

                        '  If Not Me.State.MyBO.getPaymentTypeCode = Me.State.MyBO.PAYMENT_PRE_AUTHORIZED Then
                        If ((LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_DESTINATION, oCancelCertificateData.refund_dest_id) = Codes.REFUND_DESTINATION__CUSTOMER AndAlso oCancelCertificateData.refundAmount > 0)) Then
                            '** populate the paymentmethod drop down here 
                            State.validateAddress = True
                            PopulatePaymentMethodDropdown(PaymentMethodDrop)
                            If PaymentMethodDrpLabel.Text.IndexOf(":") > 0 Then
                                PaymentMethodDrpLabel.Text = PaymentMethodDrpLabel.Text
                            Else
                                PaymentMethodDrpLabel.Text = PaymentMethodDrpLabel.Text & ":"
                            End If
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, True)

                            SetSelectedItem(PaymentMethodDrop, oCancelCertificateData.def_refund_payment_method_id)
                        Else
                            '** do not show payment method if refund amt=0 or refund is not to customer
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                            State.validateAddress = False
                        End If
                        PopulateBOProperty(State.certCancellationBO, "PaymentMethodId", PaymentMethodDrop)
                    End If 'Not inputAmountRequiredMissing
                Else 'Not errorExist
                    ControlMgr.SetVisibleControl(Me, RefundAmtTextbox, False)
                    ControlMgr.SetVisibleControl(Me, RefundAmtLabel, False)
                    ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                    ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                End If

                If oCancelCertificateData.errorExist2 = False Then
                    ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, True)
                    'When an App User picks up the cancellation reason ‘REP’ from Screen at the time of cancellation
                    '(provide the user has a role that is not excluded for this cancellation reason) 
                    'System should display a warning that the Certificate would not get reinstated manually
                    If oCancelCertificateData.cancellationCode = "REP" Then
                        MasterPage.MessageController.AddWarning(Message.MSG_PROMPT_FOR_REINSTATEMENT_NOT_ALLOWED_ON_REPLACEMENT, True)
                    End If
                Else
                    MasterPage.MessageController.AddErrorAndShow(oCancelCertificateData.errorCode)
                    ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, False)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            Finally
                PopulatePremiumInfoTab()
            End Try
        End Sub

        Private Sub btnCancelRequestSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCancelRequestSave_WRITE.Click
            Dim oCertCancelRequestData As New CertCancelRequestData
            Dim dblRefundAmount As Double, strMsg, strMsgCertCancelSuccess As String
            Try
                MasterPage.MessageController.Clear()
                populateCertCancelRequestBOFromForm()
                populateCertCancelRequestCommentBOFromForm()
                ClearLabelError(moCRIBANNumberLabel)
                State.MyBO.ValidateCancelRequest(State.certCancelRequestBO, State.CancReqCommentBO, oCertCancelRequestData, State.useExistingBankInfo, State.CRequestBankInfoBO)
                If Not oCertCancelRequestData.errorExist Then

                    State.MyBO.ProcessCancelRequest(State.certCancelRequestBO, State.useExistingBankInfo, State.CRequestBankInfoBO, State.CancReqCommentBO, oCertCancelRequestData, dblRefundAmount, strMsg)

                    State.MyBO = New Certificate(State.CertificateId)
                    PopulateFormFromBOs()
                    State.CertCancelRequestId = State.MyBO.getCertCancelRequestID
                    State.certCancelRequestBO = New CertCancelRequest(State.CertCancelRequestId)
                    State.IsEdit = False
                    populateCancelRequestInfoTab()
                    PopulateCommentsGrid()
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    WorkingPanelVisible = True
                    If State.MyBO.StatusCode = "C" Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        State.certificateChanged = True
                        PopulateCancellationInfoTab()
                        If strMsg = SUCCESS Then
                            strMsgCertCancelSuccess = GetTranslation(Message.CANCEL_SUCCESS_WITH_REFUND)
                            strMsgCertCancelSuccess = strMsgCertCancelSuccess & " " & dblRefundAmount.ToString
                            MasterPage.MessageController.AddSuccess(strMsgCertCancelSuccess, False)
                        End If
                    Else
                        If State.MyBO.getCancelationRequestFlag = YES Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Else
                            If State.MyBO.IsChildCertificate Then
                                ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                            Else
                                ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                            End If
                        End If
                        MasterPage.MessageController.Clear()
                        If strMsg = FUTURE Then
                            MasterPage.MessageController.AddSuccess(Message.CANCEL_CERT_IN_FUTURE, True)
                        ElseIf strMsg = CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD Then
                            MasterPage.MessageController.AddSuccess(Message.CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD, True)
                        Else
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        End If
                    End If
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                    EnableTab(CERT_CANCELLATION_INFO_TAB, True)
                    'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                    If isTabEnabled(0) = True Then State.selectedTab = 0
                    State.IsEdit = False
                    EnableDisableFields()
                    State.isCovgGridRefreshNeeded = True
                Else
                    If oCertCancelRequestData.errorCode = Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_INVALID Then
                        ElitaPlusPage.SetLabelError(moCRIBANNumberLabel)
                    End If
                    MasterPage.MessageController.AddError(oCertCancelRequestData.errorCode)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ProcessCancellationButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles ProcessCancellationButton_WRITE.Click
            Dim errorExist As Boolean = False
            Dim oCancelCertificateData As New CertCancellationData

            Try
                'REQ-910 New fields BEGIN 
                If (State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) _
                    Or State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3"))) AndAlso (State.QuotedRefundAmt > 0) Then ' 1 = Display and Require When Cancelling 
                    If (State.MyBO.Occupation Is Nothing OrElse State.MyBO.Occupation.Equals(String.Empty)) Or
                       (State.MyBO.PoliticallyExposedId.Equals(Guid.Empty)) Or
                       (State.MyBO.IncomeRangeId.Equals(Guid.Empty)) Then
                        Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_LEGAL_INFO_MISSING)
                    End If
                End If



                'REQ-910 New fields END

                MasterPage.MessageController.Clear()
                PopulateBOProperty(State.certCancellationBO, "CancellationReasonId", moCancellationReasonDrop)
                PopulateBOProperty(State.certCancellationBO, "CancellationDate", CancelCertDateTextbox)
                PopulateBOProperty(State.certCancellationBO, "ComputedRefund", RefundAmtTextbox)
                PopulateBOProperty(State.certCancellationBO, "InstallmentsPaid", txtInstallmentsPaid)
                populateCertCancelCommentBOFromForm()

                validateCancellationProcessFields()
                ValidateCancellationCommentsFields()

                If PaymentMethodDrop.Visible = True Then
                    If (State.certCancellationBO.PaymentMethodId.Equals(Guid.Empty)) Then
                        MasterPage.MessageController.AddErrorAndShow(Message.MSG_INVALID_PAYMENT_METHOD)
                        errorExist = True
                    Else
                        If State.validateAddress Then
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                                ValidateAddressField()
                            End If
                        End If
                    End If
                Else
                    State.certCancellationBO.PaymentMethodId = System.Guid.Empty
                End If
                If Not errorExist Then

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                        If moBankInfoController.Visible = True Then
                            State.BankInfoCtrVisible = True
                            PopulateBankInfoBOFromUserCtr()
                        Else
                            State.BankInfoBO = Nothing
                        End If
                    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                        If moPaymentOrderInfoCtrl.Visible = True Then
                            State.PaymentOrderInfoCtrVisible = True
                            PopulatePaymentOrderInfoBOFromUserCtr()

                            'If ((Me.State.MyBO.CustomerName Is Nothing OrElse Me.State.MyBO.CustomerName.Equals(String.Empty)) Or
                            '    (Me.State.MyBO.IdentificationNumber.Equals(Guid.Empty)) Or _
                            '    (Me.State.MyBO.IdType.Equals(Guid.Empty))) Then
                            '    Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_IDNUMBER_IDTYPE_INFO_MISSING)
                            'End If

                            If ((State.MyBO.CustomerName Is Nothing OrElse State.MyBO.CustomerName.Equals(String.Empty))) Then
                                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_NAME_INFO_MISSING)
                            End If
                        Else
                            State.PaymentOrderInfoBO = Nothing
                        End If
                    Else
                        State.BankInfoBO = Nothing
                        State.PaymentOrderInfoBO = Nothing
                    End If

                    Dim oContract As New Contract
                    oContract = Contract.GetContract(State.MyBO.DealerId, State.MyBO.WarrantySalesDate.Value)


                    If State.certCancellationBO.CancellationDate > DateTime.Today Then
                        If State.MyBO.PreviousCertificateId.Equals(Guid.Empty) Then
                            ' the cancellation will be in the future
                            State.MyBO.SetFutureCancellation(State.certCancellationBO, State.BankInfoBO, oContract, State.CancCommentBO, , State.PaymentOrderInfoBO)
                        Else
                            State.MyBO.ProcessCancellation(State.certCancellationBO, State.BankInfoBO, oContract, State.CancCommentBO, , State.PaymentOrderInfoBO)
                        End If
                    Else
                        State.MyBO.ProcessCancellation(State.certCancellationBO, State.BankInfoBO, oContract, State.CancCommentBO, , State.PaymentOrderInfoBO)
                    End If


                    If IsSinglePremium Then
                        PopulateCoveragesGrid()
                    End If

                    State.MyBO = New Certificate(State.CertificateId)

                    PopulateFormFromBOs()
                    PopulateCancellationInfoTab()

                    PopulateCommentsGrid()

                    DisplayMessage(Message.MSG_CERTIFICATE_CANCELLATION_CONFIRM, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    WorkingPanelVisible = True

                    If State.MyBO.StatusCode = "C" Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        State.certificateChanged = True
                    Else
                        If State.MyBO.IsChildCertificate Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Else
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                        End If

                    End If

                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                    EnableTab(CERT_CANCELLATION_INFO_TAB, True)

                    'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                    If isTabEnabled(0) = True Then State.selectedTab = 0

                    EnableDisableFields()
                    State.isCovgGridRefreshNeeded = True
                Else
                    ControlMgr.SetVisibleControl(Me, moBankInfoController, False)
                    State.BankInfoCtrVisible = False

                    ControlMgr.SetVisibleControl(Me, moPaymentOrderInfoCtrl, False)
                    State.PaymentOrderInfoCtrVisible = False
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ReverseCancellationButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles ReverseCancellationButton_WRITE.Click
            Try
                Dim oReverseCancelCertificateData As New ReverseCancellationData
                Dim compId As Guid = State.MyBO.CompanyId

                With oReverseCancelCertificateData
                    .companyId = compId
                    .dealerId = State.MyBO.DealerId
                    .certificate = State.MyBO.CertNumber
                    .source = ElitaPlusIdentity.Current.ActiveUser.UserName
                    .sourceType = "USER"
                End With
                Dim cancDate As Date = State.MyBO.TheCertCancellationBO.CancellationDate
                Dim attvalue As AttributeValue = State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CAN_DT_GTRTHN_12_MNTHS).FirstOrDefault

                If (State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CAN_DT_GTRTHN_12_MNTHS).Count > 0) Then
                    If cancDate.AddYears(1) < DateTime.Now Then
                        MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_MESSAGE_FUNCTIONALITY)
                        Throw New GUIException("", "")
                    End If
                End If

                Dim oContract As Contract = Contract.GetContract(State.MyBO.DealerId, DateTime.Today)
                If oContract Is Nothing Then
                    MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.NO_CONTRACT_FOUND)
                    Throw New GUIException("", "")
                End If

                State.certCancellationBO.ReverseCancellation(oReverseCancelCertificateData)
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs()
                PopulatePremiumInfoTab()
                PopulateCommentsGrid()
                DisplayMessage(Message.MSG_CERTIFICATE__REVERSE_CANCELLATION_CONFIRM, "", MSG_BTN_OK, MSG_TYPE_INFO)
                WorkingPanelVisible = True
                State.certificateChanged = True

                If State.MyBO.StatusCode = "C" Then
                    ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                Else
                    If State.MyBO.IsChildCertificate Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                    End If
                End If

                'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                If isTabEnabled(0) = True Then State.selectedTab = 0

                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), False)
                EnableTab(CERT_CANCELLATION_INFO_TAB, False)

                EnableDisableFields()
                State.isCovgGridRefreshNeeded = True

            Catch ex As GUIException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBillingStatusId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moBillingStatusId.SelectedIndexChanged

            Dim oBillingCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moBillingStatusId))
            Dim originalStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.TheDirectDebitState.certInstallment.BillingStatusId)
            Dim dv As DataView = BillingDetail.getBillingTotals(State.MyBO.Id)
            Dim oCount As Integer = 0

            ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, True)

            Try
                If Not originalStatus = "A" AndAlso Not oBillingCode = "A" Then
                    PopulateControlFromBOProperty(moBillingStatusId, State.TheDirectDebitState.certInstallment.BillingStatusId)
                    Throw New GUIException("Cannot Reject an On Hold Record or Vice-Versa", "Cannot Reject an On Hold Record or Vice-Versa")
                End If

                If originalStatus = "A" AndAlso oBillingCode = "R" Then
                    PopulateControlFromBOProperty(moBillingStatusId, State.TheDirectDebitState.certInstallment.BillingStatusId)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_REJECT_ACTIVE_RECORD)
                End If


                If oBillingCode = "R" Then
                    If State.oBillingTotalAmount > 0 Then
                        If State.TheDirectDebitState.BillingLastRecord Is Nothing Then
                            State.TheDirectDebitState.BillingLastRecord = BillingDetail.getBillingLaterRow(State.MyBO.Id)
                        End If

                        Dim lastBilledAmount As Decimal
                        If Not State.TheDirectDebitState.BillingLastRecord.Table.Rows(0).IsNull(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT) Then
                            lastBilledAmount = CType(State.TheDirectDebitState.BillingLastRecord.Table.Rows(0).Item(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT), Decimal)
                        End If

                        If lastBilledAmount < 0 Then
                            Throw New GUIException("Cannot Reject a Reversal.  Please See History", "Cannot reject a reversal.  Please see history")
                        End If
                    Else
                        Throw New GUIException("Can Not Reject What Was Not Send to Collection", "Can Not Reject What Was Not Send to Collection")
                    End If
                End If

                State.TheDirectDebitState.StatusChenge = False

                If (Not State.TheDirectDebitState.certInstallment.BillingStatusId.Equals(GetSelectedItem(moBillingStatusId))) AndAlso oBillingCode = "H" AndAlso State.oBillingTotalAmount > 0 Then
                    State.TheDirectDebitState.StatusChenge = True
                End If

                If Not oBillingCode = "A" Then
                    ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, True)
                    CheckBoxSendLetter.CssClass = ""
                Else
                    CheckBoxSendLetter.Checked = False
                    ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, False)
                    moDateLetterSentText.Text = ""
                End If

                State.IsEdit = True
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                State.IsEdit = True
                EnableDisableFields()
                ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, False)
            End Try
        End Sub

#End Region

#Region "Page Control Events"

#End Region

#Region "Error Handling"

#End Region

#Region "Datagrid Related"

#End Region

#Region "State Controller"
        Sub StartNavControl()
            Dim nav As New ElitaPlusNavigation
            NavController = New NavControllerBase(nav.Flow("CREATE_CLAIM_FROM_CERTIFICATE"))
        End Sub
#End Region

#Region "Dropdown Related"

        Private Sub PaymentMethodDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles PaymentMethodDrop.SelectedIndexChanged
            Dim objCompany As New Company(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY, State.companyCode))
            Dim selectedPaymentMethod, selectedPaymentReason As Guid
            selectedPaymentMethod = GetSelectedItem(PaymentMethodDrop)
            PopulateBOProperty(State.certCancellationBO, "PaymentMethodId", PaymentMethodDrop)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then


                ' SHOW THE BANK INFO USER CONTROL HERE -----
                ' Me.trbank.Visible = True
                moBankInfoController.Visible = True
                State.BankInfoBO = Nothing
                State.BankInfoBO = State.certCancellationBO.bankinfo

                State.BankInfoBO.CountryID = State.MyBO.AddressChild.CountryId
                State.BankInfoBO.SourceCountryID = objCompany.CountryId
                moBankInfoController.EnableDisableControls()
                State.BankInfoBO.Account_Name = moCustomerNameText.Text.Trim
                State.BankInfoBO.PaymentMethodId = selectedPaymentMethod
                'Me.State.BankInfoBO.ACCOUNT_NUMBER = System.DBNull.Value

                moBankInfoController.Bind(State.BankInfoBO)
                State.BankInfoCtrVisible = True

                moPaymentOrderInfoCtrl.Visible = False
                State.PaymentOrderInfoCtrVisible = False

                'Me.moBankInfoController.
            ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                moBankInfoController.Visible = False
                State.BankInfoCtrVisible = False

                State.PaymentOrderInfoCtrVisible = True
                moPaymentOrderInfoCtrl.Visible = True

                State.PaymentOrderInfoBO = Nothing
                State.PaymentOrderInfoBO = State.certCancellationBO.PmtOrderinfo

                State.PaymentOrderInfoBO.CountryID = objCompany.CountryId
                State.PaymentOrderInfoBO.SourceCountryID = objCompany.CountryId
                moPaymentOrderInfoCtrl.EnableDisableControls()
                State.PaymentOrderInfoBO.Account_Name = moCustomerNameText.Text.Trim
                State.PaymentOrderInfoBO.PaymentMethodId = selectedPaymentMethod
                State.PaymentOrderInfoBO.CountryID = objCompany.CountryId
                'Me.State.BankInfoBO.ACCOUNT_NUMBER = System.DBNull.Value

                moPaymentOrderInfoCtrl.Bind(State.PaymentOrderInfoBO)

            Else
                moBankInfoController.Visible = False
                State.BankInfoCtrVisible = False
                moPaymentOrderInfoCtrl.Visible = False
                State.PaymentOrderInfoCtrVisible = False
            End If

        End Sub

#End Region

#Region "Certificate History Tab"

        Private Sub PopulateCertificateHistoryTabInfo()
            Try
                moCertificateHistory_cboPageSize.SelectedValue = CType(State.PageSize, String)
                If (State.CertHistoryDV Is Nothing) Then
                    State.CertHistoryDV = Certificate.GetCertHistoryInfo(State.MyBO.CertNumber, State.MyBO.DealerId, IIf(chbShowUpdates.Checked, "true", "false").ToString())
                End If

                CertHistoryGrid.PageSize = State.PageSize
                If Not (State.CertHistoryDV Is Nothing) Then
                    State.CertHistoryDV.Sort = State.CertHistorySortExpression

                    SetPageAndSelectedIndexFromGuid(State.CertHistoryDV, Guid.Empty, CertHistoryGrid, State.PageIndex)
                    CertHistoryGrid.DataSource = State.CertHistoryDV
                    State.PageIndex = CertHistoryGrid.PageIndex

                    HighLightSortColumn(CertHistoryGrid, State.CertHistorySortExpression, IsNewUI)
                    CertHistoryGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertHistoryGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, CertHistoryGrid.Visible)
                    Session("recCount") = State.CertHistoryDV.Count

                    If CertHistoryGrid.Visible Then
                        lblRecordCount.Text = State.CertHistoryDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    End If

                    If State.CertHistoryDV.Count = 0 Then
                        For Each gvRow As GridViewRow In CertHistoryGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                        moCertificateHistory_lblPageSize.Visible = False
                        moCertificateHistory_cboPageSize.Visible = False
                        colonSepertor.Visible = False
                        chbShowUpdates.Visible = False
                    Else
                        moCertificateHistory_lblPageSize.Visible = True
                        moCertificateHistory_cboPageSize.Visible = True
                        colonSepertor.Visible = True
                        chbShowUpdates.Visible = True
                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub CertHistoryGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles CertHistoryGrid.RowDataBound
            Try
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    Dim strInforceDate As String = Convert.ToString(e.Row.Cells(CertHistoryGridColInforceDateIdx).Text)
                    strInforceDate = strInforceDate.Replace("&nbsp;", "")
                    If String.IsNullOrWhiteSpace(strInforceDate) = False Then
                        Dim tempInforceDate = Convert.ToDateTime(e.Row.Cells(CertHistoryGridColInforceDateIdx).Text.Trim())
                        Dim formattedInforceDate = GetDateFormattedString(tempInforceDate)
                        e.Row.Cells(CertHistoryGridColInforceDateIdx).Text = Convert.ToString(formattedInforceDate)
                    End If
                    Dim strProcessedDate As String = Convert.ToString(e.Row.Cells(CertHistoryGridColProcessedDateIdx).Text)
                    strProcessedDate = strProcessedDate.Replace("&nbsp;", "")
                    If String.IsNullOrEmpty(strProcessedDate) = False Then
                        Dim tempProcessedDate = Convert.ToString(e.Row.Cells(CertHistoryGridColProcessedDateIdx).Text.Trim())
                        Dim formattedProcessedDate = GetDateFormattedStringNullable(tempProcessedDate)
                        e.Row.Cells(CertHistoryGridColProcessedDateIdx).Text = Convert.ToString(formattedProcessedDate)
                    End If
                    Dim strStatusChangeDate As String = Convert.ToString(e.Row.Cells(CertHistoryGridColStatusChangeDateIdx).Text)
                    strStatusChangeDate = strStatusChangeDate.Replace("&nbsp;", "")
                    If String.IsNullOrEmpty(strStatusChangeDate) = False Then
                        Dim tempStatusChangeDate = Convert.ToString(e.Row.Cells(CertHistoryGridColStatusChangeDateIdx).Text.Trim())
                        Dim formattedStatusChangeDate = GetDateFormattedStringNullable(tempStatusChangeDate)
                        e.Row.Cells(CertHistoryGridColStatusChangeDateIdx).Text = Convert.ToString(formattedStatusChangeDate)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CertHistoryGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles CertHistoryGrid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateCertificateHistoryTabInfo()


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles CertHistoryGrid.Sorting
            Try
                If State.CertHistorySortExpression.StartsWith(e.SortExpression) Then
                    If State.CertHistorySortExpression.EndsWith(" DESC") Then
                        State.CertHistorySortExpression = e.SortExpression
                    Else
                        State.CertHistorySortExpression &= " DESC"
                    End If
                Else
                    State.CertHistorySortExpression = e.SortExpression
                End If
                State.PageIndex = 0
                PopulateCertificateHistoryTabInfo()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles moCertificateHistory_cboPageSize.SelectedIndexChanged
            Try
                CertHistoryGrid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(moCertificateHistory_cboPageSize.SelectedValue, Int32))
                State.PageSize = CType(moCertificateHistory_cboPageSize.SelectedValue, Integer)
                PopulateCertificateHistoryTabInfo()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub chbShowUpdates_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbShowUpdates.CheckedChanged
            Try
                lblRecordCount.Text = "Panel Updated at " & DateTime.Now.ToString

                State.CertHistoryDV = Certificate.GetCertHistoryInfo(State.MyBO.CertNumber, State.MyBO.DealerId, IIf(chbShowUpdates.Checked, "true", "false").ToString())

                PopulateCertificateHistoryTabInfo()

                'if there are no records available after checking this box then do not hide this check box 
                If State.CertHistoryDV.Count = 0 Then
                    chbShowUpdates.Visible = True
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub btnGetCertHistory_Click(sender As Object, e As EventArgs) Handles btnGetCertHistory.Click
            Try
                'Certificate History Tab
                If State.IsCertHistoryGridVisible Then
                    TranslateGridHeader(CertHistoryGrid)
                    PopulateCertificateHistoryTabInfo()
                End If
                uplCertHistory.Update()
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region "Tab related"
        Private Sub EnableTab(tabInd As Integer, blnFlag As Boolean)
            If blnFlag = True Then 'enable - remove from disabled list
                If listDisabledTabs.Contains(tabInd) = True Then
                    listDisabledTabs.Remove(tabInd)
                End If
            Else 'disable - add to the disabled list
                If listDisabledTabs.Contains(tabInd) = False Then
                    listDisabledTabs.Add(tabInd)
                End If
            End If
        End Sub

        Private Function isTabEnabled(tabInd As Integer) As Boolean
            If listDisabledTabs.Contains(tabInd) = True Then 'tab is diabled
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub EnableDisableTabs(editMode As Boolean)
            Dim i As Short = 0
            Dim limit As Integer = TAB_TOTAL_COUNT


            If Not editMode Then
                listDisabledTabs.Clear() 'enable all tabs
            Else
                For i = 0 To CType((limit - 1), Short)
                    'enable only the selected tab when in edit mode
                    If i = State.selectedTab Then
                        EnableTab(i, True)
                    Else
                        EnableTab(i, False)
                    End If
                Next
            End If
        End Sub

        Private Sub btnCustProfileHistory_Write_Click(sender As Object, e As EventArgs) Handles btnCustProfileHistory_Write.Click
            Try
                State.NavigateToCustProfileHistory = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                callPage(CustomerProfileHistory.URL, State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnOutboundCommHistory_Write_Click(sender As Object, e As EventArgs) Handles btnOutboundCommHistory.Click
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                callPage(Tables.OcMessageSearchForm.URL, New Tables.OcMessageSearchForm.CallType("cert_number", State.MyBO.CertNumber, State.MyBO.Id, State.MyBO.DealerId))
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewCertRegItem_WRITE__Click(sender As System.Object, e As System.EventArgs) Handles btnNewCertRegItem_WRITE.Click

            Try
                State.NavigateToNewCertItem = True
                State.isRegItemsGridVisible = True
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State.MyBO
                NavController.Navigate(Me, CREATE_NEW_REGISTER_ITEM)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCancelRequestDateTextBox_TextChanged(sender As Object, e As EventArgs) Handles moCancelRequestDateTextBox.TextChanged
            Try
                PopulateCancellationDate()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub moUseExistingBankDetailsDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moUseExistingBankDetailsDrop.SelectedIndexChanged
            If GetSelectedValue(moUseExistingBankDetailsDrop) = String.Empty Then
                ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
            Else
                If GetSelectedValue(moUseExistingBankDetailsDrop) = Codes.EXT_YESNO_N Then
                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, True)
                Else
                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
                End If

            End If
        End Sub

        Private Sub moCancelRequestReasonDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moCancelRequestReasonDrop.SelectedIndexChanged
            If (State.CancelRulesForSFR = Codes.YESNO_Y) Then
                If GetSelectedItem(moCancelRequestReasonDrop).Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, False)
                    moProofOfDocumentationDrop.SelectedIndex = -1
                Else
                    Dim oCancellationReason As New CancellationReason(GetSelectedItem(moCancelRequestReasonDrop))
                    State.CancReasonIsLawful = oCancellationReason.IsLawful
                    If State.CancReasonIsLawful = Codes.EXT_YESNO_Y And (oCancellationReason.Code = Codes.SFR_CR_DEATH Or oCancellationReason.Code = Codes.SFR_CR_MOVINGABROAD) Then
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, True)
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, True)
                        ControlMgr.SetEnableControl(Me, moProofOfDocumentationDrop, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, False)
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, False)
                        moProofOfDocumentationDrop.SelectedIndex = -1
                    End If
                    moCancelDateTextBox.Text = String.Empty
                    moCancelRequestDateTextBox.Text = String.Empty
                End If
            End If

        End Sub

        Private Sub UpdateBankInfoButton_WRITE_Click(sender As Object, e As EventArgs) Handles UpdateBankInfoButton_WRITE.Click
            Try
                Dim oUpdateBankInfoForRejectsData As New UpdateBankInfoForRejectsData

                MasterPage.MessageController.Clear()

                If IsNothing(State.RefundBankInfoBO) Then
                    State.RefundBankInfoBO = New BankInfo
                End If
                PopulateBOProperty(State.RefundBankInfoBO, "IbanNumber", moRfIBANNumberText)
                PopulateBOProperty(State.RefundBankInfoBO, "Account_Number", moRfAccountNumberText)
                PopulateBOProperty(State.RefundBankInfoBO, "CountryID", State.MyBO.Company.CountryId)

                validateUpdateBankInfoRefundFields()

                With oUpdateBankInfoForRejectsData
                    .certCancellationId = State.MyBO.TheCertCancellationBO.Id
                    .bankInfoId = State.MyBO.TheCertCancellationBO.BankInfoId
                    .accountNumber = State.RefundBankInfoBO.Account_Number
                    .IBANNumber = State.RefundBankInfoBO.IbanNumber
                End With

                State.certCancellationBO.UpdateBankInfoForRejectsSP(oUpdateBankInfoForRejectsData)
                State.MyBO = New Certificate(State.CertificateId)
                PopulateFormFromBOs()
                PopulateCancellationInfoTab()

                WorkingPanelVisible = True
                State.certificateChanged = True
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

                ControlMgr.SetVisibleControl(Me, UpdateBankInfoButton_WRITE, False)

                EnableDisableFields()
            Catch ex As GUIException
                HandleErrors(ex, MasterPage.MessageController)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelCertReqDateTextbox_TextChanged(sender As Object, e As EventArgs) Handles CancelCertReqDateTextbox.TextChanged
            Dim endOfMonth As Date

            Try
                If String.IsNullOrEmpty(CancelCertReqDateTextbox.Text) Then
                    endOfMonth = ComputeEndOfMonth(Date.Today)
                    CancelCertDateTextbox.Text = GetDateFormattedStringNullable(endOfMonth)
                Else
                    Dim userDate As Date
                    userDate = GetDateFormattedStringNullable(Convert.ToDateTime(CancelCertReqDateTextbox.Text))
                    endOfMonth = ComputeEndOfMonth(userDate)
                    CancelCertDateTextbox.Text = GetDateFormattedStringNullable(endOfMonth)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Ibanoperation(enable As Boolean)

            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)
            ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, enable)
            ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, enable)
            ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, enable)
            ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, enable)
        End Sub

        Private Sub linkPrevCertId_Click(sender As Object, e As EventArgs) Handles linkPrevCertId.Click
            ' Me.State.selectedCertificateId = New Guid(e.CommandArgument.ToString())
            SourceCertificateId = State.MyBO.Id
            Session("SourceCertificateId") = SourceCertificateId

            callPage(CertificateForm.URL, State.MyBO.PreviousCertificateId)
        End Sub

        Private Sub linkOrigCertId_Click(sender As Object, e As EventArgs) Handles linkOrigCertId.Click
            SourceCertificateId = State.MyBO.Id
            Session("SourceCertificateId") = SourceCertificateId
            callPage(CertificateForm.URL, State.MyBO.OriginalCertificateId)
        End Sub
#End Region
#Region "Tax Details WebService"

        <WebMethod(), Script.Services.ScriptMethod()>
        Public Shared Function GetSalesTaxDetails(contextKey As String) As String

            Try
                Dim strbuilder As StringBuilder = New StringBuilder
                Dim inputParam = contextKey.Split(":")
                strbuilder.Append("<table width='40px' class='dataGrid'><tbody>")

                If Not IsNothing(inputParam) AndAlso Not IsNothing(inputParam(PARAM_CERTICATE_ID)) AndAlso Not IsNothing(inputParam(PARAM_LANGAUGE_ID)) Then

                    Dim dv As DataView = Certificate.SalesTaxDetail(Guid.Parse(inputParam(PARAM_CERTICATE_ID)), Guid.Parse(inputParam(PARAM_LANGAUGE_ID)))

                    If IsNothing(dv) Then
                        Return String.Empty
                    End If

                    For Each TaxDetailsDr As DataRow In dv.ToTable.Rows

                        strbuilder.Append("<tr><td colspan='2' > " & TaxDetailsDr(REPEATER_COL_TAX_TYPE) & " : </td> <td colspan='4' align='left'> " &
                                          TaxDetailsDr(REPEATER_COL_TAX_TOTAL_VALUE) & " </td></tr>")
                        strbuilder.Append("<tr><td>" & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX1_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX1"),
                                                  TaxDetailsDr(REPEATER_COL_TAX1_DESCRIPTION)) & " </td>")
                        strbuilder.Append("<td> " & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX2_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX2"),
                                                  TaxDetailsDr(REPEATER_COL_TAX2_DESCRIPTION)) & " </td>")
                        strbuilder.Append("<td>" & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX3_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX3"),
                                              TaxDetailsDr(REPEATER_COL_TAX3_DESCRIPTION)) & " </td>")
                        strbuilder.Append("<td> " & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX4_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX4"),
                                              TaxDetailsDr(REPEATER_COL_TAX4_DESCRIPTION)) & " </td>")
                        strbuilder.Append("<td>" & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX5_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX5"),
                                              TaxDetailsDr(REPEATER_COL_TAX5_DESCRIPTION)) & " </td>")
                        strbuilder.Append("<td> " & If(IsDBNull(TaxDetailsDr(REPEATER_COL_TAX6_DESCRIPTION)), TranslationBase.TranslateLabelOrMessage("TAX6"),
                                              TaxDetailsDr(REPEATER_COL_TAX6_DESCRIPTION)) & "  </td></tr>")
                        strbuilder.Append("<tr> <td>" & TaxDetailsDr(REPEATER_COL_TAX1) & "</td>" & "<td> " & TaxDetailsDr(REPEATER_COL_TAX2) & "</td>")
                        strbuilder.Append("<td>" & TaxDetailsDr(REPEATER_COL_TAX3) & "</td>" & "<td> " & TaxDetailsDr(REPEATER_COL_TAX4) & "</td>")
                        strbuilder.Append("<td>" & TaxDetailsDr(REPEATER_COL_TAX5) & "</td>" & "<td> " & TaxDetailsDr(REPEATER_COL_TAX6) & "</td></tr>")

                    Next
                End If

                strbuilder.Append("</tbody></table>")

                Return strbuilder.ToString

            Catch ex As Exception
                Return TranslationBase.TranslateLabelOrMessage(Message.MSG_TAX_DETAILS_POPUP_ERROR)
            End Try
        End Function

#End Region

    End Class

End Namespace