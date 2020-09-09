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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                Return Me.State.CertificateId
            End Get
            Set(ByVal Value As Guid)
                Me.State.CertificateId = Value
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
                Return Me.State.WorkingPanelVisible
            End Get
            Set(ByVal value As Boolean)
                Me.State.WorkingPanelVisible = value

                ControlMgr.SetVisibleControl(Me, CancellationPanel, Not Me.State.WorkingPanelVisible)
                ControlMgr.SetVisibleControl(Me, moCertificateInfoCtrlCancel, Not Me.State.WorkingPanelVisible)

                ControlMgr.SetVisibleControl(Me, WorkingPanel, Me.State.WorkingPanelVisible)
                ControlMgr.SetVisibleControl(Me, moCertificateInfoController, Me.State.WorkingPanelVisible)

                If Me.State.WorkingPanelVisible Then
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Certificate_Detail)
                Else
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Cancel_Certificate)
                End If
            End Set
        End Property

        Public ReadOnly Property HasInstallment As Boolean
            Get
                If Me.State.TheDirectDebitState.certInstallment Is Nothing Then
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

                    oContract = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value)

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

                    oContract = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value)

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
            If Not Me.State.MyBO.Dealer Is Nothing AndAlso Me.State.MyBO.Dealer.Validate_Address = Codes.EXT_YESNO_Y Then
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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
            Public CertExtFieldsSortExpression As String = Certificate.CertExtendedFieldsDv.COL_FIELD_NAME & " DESC"
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
                If ((Me.NavController Is Nothing) OrElse (Me.NavController.Context <> "CERT_DETAIL")) Then
                    'Restart flow
                    Me.StartNavControl()
                    Me.NavController.State = CType(Session(Me.SESSION_KEY_BACKUP_STATE), MyState)
                ElseIf Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                ElseIf (Me.GetType.BaseType.FullName <>
                        Me.NavController.State.GetType.ReflectedType.FullName) Then
                    'Restart flow
                    Me.StartNavControl()
                    Me.NavController.State = CType(Session(Me.SESSION_KEY_BACKUP_STATE), MyState)
                Else
                    If Me.NavController.IsFlowEnded Then
                        'Restart flow
                        Dim s As MyState = CType(Me.NavController.State, MyState)
                        Me.StartNavControl()
                        Me.NavController.State = s

                    End If
                End If
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                End If
                retState = CType(Me.NavController.State, MyState)
                Session(Me.SESSION_KEY_BACKUP_STATE) = retState
                Return retState
            End Get
        End Property

        Public Class Parameters

            Public CertificateId As Guid = Nothing
            Public IsCallerAuthenticated As Boolean = False

            Public Sub New(ByVal certificateId As Guid, Optional IsCallerAuthenticated As Boolean = False)
                Me.CertificateId = certificateId
                Me.IsCallerAuthenticated = IsCallerAuthenticated
            End Sub

        End Class

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                Me.WorkingPanelVisible = True
                If Not Me.CallingParameters Is Nothing Then

                    '   Me.StartNavControl() 'arf 12-20-04

                    'Me.State.workingPanelVisible = True
                    'Get the id from the parent

                    ' DEF-4245 Clean up old data on new search
                    Me.State.CoveragesearchDV = Nothing
                    Me.State.ClaimsearchDV = Nothing

                    Try
                        Me.State.MyBO = New Certificate(CType(Me.CallingParameters, Guid))
                    Catch ex As Exception
                        Me.State.MyBO = New Certificate(CType(Me.CallingParameters, Parameters).CertificateId)
                        Me.state.IsCallerAuthenticated = CType(Me.CallingParameters, Parameters).IsCallerAuthenticated
                    End Try
                    Me.CertId = Me.State.MyBO.Id
                    Me.State.ValFlag = Me.State.MyBO.GetValFlag()
                    Me.State.certificateChanged = False

                    Me.State.PreviousCertificate = Nothing
                    Me.State.OriginalCertificate = Nothing

                Else
                    Throw New Exception("No Calling Parameters")
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub CertificateForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            'set tab display info
            hdnSelectedTab.Value = Me.State.selectedTab
            Dim strTemp As String = String.Empty
            If listDisabledTabs.Count > 0 Then
                For Each i As Integer In listDisabledTabs
                    strTemp = strTemp + "," + i.ToString
                Next
                strTemp = strTemp.Substring(1) 'remove the first comma
            End If
            Me.State.disabledTabs = strTemp 'store the disabled state
            hdnDisabledTabs.Value = Me.State.disabledTabs
            If hdnInitDisabledTabs.Value = "NA" Then 'set once only
                hdnInitDisabledTabs.Value = hdnDisabledTabs.Value
            End If
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State.MyBO Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                                              TranslationBase.TranslateLabelOrMessage("Certificate") & " " & Me.State.MyBO.CertNumber
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Certificate_Detail) & " (<strong>" &
                                              Me.State.MyBO.CertNumber & "</strong>) " & TranslationBase.TranslateLabelOrMessage("SUMMARY")
                End If
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            Try
                ' This is an scenario where Dynamic Claim Recording is called from Certificate form
                If Not Me.IsPostBack Then
                    If (Not Me.NavController Is Nothing) AndAlso (Not Me.NavController.PrevNavState Is Nothing) AndAlso (Me.NavController.PrevNavState.Name = "SEND_SERVICE_ORDER") AndAlso
                        (Not String.IsNullOrEmpty(Me.State.ClaimRecordingXcd)) AndAlso (Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH)) Then
                        If (Me.Navigator.NavStackCount > 1) Then
                            MyBase.SetPageOutOfNavigation()
                        End If
                        Me.Navigator.SetCurrentPage(mobjPage, mobjState)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")

            UpdateBreadCrum()

            Try
                If (Not Me.NavController Is Nothing) AndAlso (Not Me.NavController.PrevNavState Is Nothing) AndAlso (Me.NavController.PrevNavState.Name = "CERT_ITEM") Then
                    Dim ciParamObj As Object = Me.NavController.ParametersPassed
                    Dim ciRetObj As Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemForm.ReturnType = CType(ciParamObj, Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemForm.ReturnType)
                    If ciRetObj.BoChanged Then
                        Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                    End If
                End If

                If (Not Me.NavController Is Nothing) AndAlso (Not Me.NavController.PrevNavState Is Nothing) AndAlso (Me.NavController.PrevNavState.Name = "CREATE_NEW_ENDORSEMENT") Then
                    Dim enParamObj As Object = Me.NavController.ParametersPassed
                    Dim enRetObj As Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementForm.ReturnType = CType(enParamObj, Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementForm.ReturnType)
                    Me.State.blnMFGChanged = enRetObj.HasMFGCoverageChanged
                    If enRetObj.HasDataChanged Then
                        Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                    Else
                        Me.State.NavigateToEndorment = False ' No Changes on Endorsement page, so no need to navigate to Endorsement tab
                    End If
                End If

                ' if coming back from Linked Certificate, reset the source certificate.
                If Me.State.MyBO Is Nothing AndAlso Not Session("SourceCertificateId") Is Nothing AndAlso Not CType(Session("SourceCertificateId"), Guid).Equals(Guid.Empty) Then

                    SourceCertificateId = CType(Session("SourceCertificateId"), Guid)
                    Me.State.MyBO = New Certificate(SourceCertificateId)

                    SourceCertificateId = Guid.Empty
                    Session("SourceCertificateId") = SourceCertificateId
                    Me.State.PreviousCertificate = Nothing
                    Me.State.OriginalCertificate = Nothing
                End If

                If Not Me.State.MyBO Is Nothing AndAlso Not ElitaPlusIdentity.Current.ActiveUser.LanguageId.Equals(Guid.Empty) Then
                    HoverMenuExtender1.DynamicContextKey = Me.State.MyBO.Id.ToString & ":" & ElitaPlusIdentity.Current.ActiveUser.LanguageId.ToString
                End If

                Me.State.selectedTab = 0
                '------------------------------------------------------------------------

                Dim selectedCancellationReason As Guid
                Dim selectedPaymentMethod As Guid
                Dim oCancellatioReason As Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason

                'Put user code to initialize the page here

                Me.MasterPage.MessageController.Clear()
                Me.WorkingPanelVisible = Me.State.WorkingPanelVisible

                Me.State.TheItemCoverageState.IsGridVisible = True
                Me.State.IsGridVisible = True

                Me.State.IsCommentsGridVisible = True
                Me.State.IsEndorsementsGridVisible = True
                Me.State.IsCertHistoryGridVisible = True
                Me.State.IsCertExtFieldsGridVisible = True
                
                ' Me.State.isItemsGridVisible = True

                If Not Me.State.MyBO Is Nothing AndAlso Contract.HasContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value) Then
                    ControlMgr.SetVisibleControl(Me, TNCButton_WRITE, True)
                Else
                    ControlMgr.SetVisibleControl(Me, TNCButton_WRITE, False)
                End If

                If Me.State.MyBO.CustomerId = Guid.Empty Then
                    ControlMgr.SetVisibleControl(Me, btnCustProfileHistory_Write, False)
                End If

                EnableDisableControls(Me.moCertificateDetailPanel, True)
                ' Address Validation
                EnableDisableAddressValidation()

                'KDDI
                If Me.State.AddressFlag = False Then
                    EnableDisableControls(Me.moCertificateDetailPanel, False)
                Else
                    EnableDisableControls(Me.moCertificateDetailPanel, True)
                End If


                Me.moTaxIdText.Attributes.Add("OnChange", "fillTB(this,'" & moNewTaxIdText.ClientID & "')")
                Me.moNewTaxIdText.Attributes.Add("OnChange", "fillTB(this,'" & moTaxIdText.ClientID & "')")

                If Not Me.IsPostBack Then
                    If Not Me.State.MyBO.CountryPurchaseId.Equals(Guid.Empty) Then
                        Dim countryBO As Country = New Country(Me.State.MyBO.CountryPurchaseId)
                        Me.State.CountryOfPurchase = countryBO.Description
                        Me.State.AllowForget = countryBO.AllowForget
                    End If

                    Me.State.directDebitPayment = False
                    Me.State.creditCardPayment = False

                    ObtainBooleanFlags()

                    PopulateFormFromBOs()

                    Me.TranslateGridHeader(Me.CertOtherCustomers)
                    Me.PopulateCustomerGrid()

                    Me.PopulatePremiumInfoTab()

                    If Me.State.TheItemCoverageState.IsGridVisible Then
                        Me.PopulateCoveragesGrid()
                    End If

                    If Me.State.IsGridVisible Then
                        Me.PopulateClaimsGrid()
                    End If

                    If Me.State.IsCommentsGridVisible Then
                        Me.PopulateCommentsGrid()
                    End If

                    If Me.State.isItemsGridVisible Then
                        Me.TranslateGridHeader(Me.ItemsGrid)
                        Me.PopulateItemsGrid()
                    End If

                    If Me.State.isRegItemsGridVisible Then
                        Me.TranslateGridHeader(Me.RegisteredItemsGrid)
                        Me.PopulateRegisterItemsGrid()
                    End If

                    If Me.State.IsEndorsementsGridVisible Then
                        Me.PopulateEndorsementsGrid()
                    End If

                    If Me.State.IsCertExtFieldsGridVisible Then
                        Me.PopulateCertExtendedFieldsGrid()
                    End If

                    PopulateInstallmentHistoryGrid()
                    PopulateExtensionsGrid()

                    EnableDisablePremiunBankInfoOption()

                    If Me.State.MyBO.StatusCode = CLOSED Then
                        'If the Cert was cancelled due to a replacement Policy, inform the user with a message
                        If (Not Me.State.MyBO.TheCertCancellationBO Is Nothing) Then
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_CANCELLATION_REASONS, Me.State.MyBO.TheCertCancellationBO.CancellationReasonId) = "REP" Then
                                Me.MasterPage.MessageController.AddInformation(Message.MSG_REINSTATEMENT_NOT_ALLOWED_FOR_REPLACEMENT_POLICY, True)
                            End If
                        End If
                        Me.PopulateCancellationInfoTab()
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                    Else
                        If allCoveragesExpired Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        End If
                    End If

                    Dim attvalue As AttributeValue = Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CANCEL_RULES_FOR_SFR).FirstOrDefault
                    If attvalue Is Nothing Then
                        Me.State.CancelRulesForSFR = Codes.YESNO_N
                    Else
                        Me.State.CancelRulesForSFR = attvalue.Value
                    End If

                    Dim attValueComputeCancellation As AttributeValue = Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_COMPUTE_CANCELLATION_DATE_AS_EOFMONTH).FirstOrDefault
                    If attValueComputeCancellation Is Nothing Then
                        Me.State.ComputeCancellationDateEOfM = Codes.YESNO_N
                    Else
                        Me.State.ComputeCancellationDateEOfM = attValueComputeCancellation.Value
                    End If

                    'PopulateCancelRequestReasonDropdown(Me.moCancelRequestReasonDrop)
                    Me.State.CertCancelRequestId = Me.State.MyBO.getCertCancelRequestID
                    If Me.State.MyBO.getCancelationRequestFlag = YES Then
                        If Not Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                            Me.State.certCancelRequestBO = New CertCancelRequest(Me.State.CertCancelRequestId)
                            If Me.State.CancelRulesForSFR = Codes.YESNO_Y AndAlso Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Or Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            End If
                        Else
                            Me.State.certCancelRequestBO = New CertCancelRequest
                        End If
                        populateCancelRequestInfoTab()
                    ElseIf Not Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                        Me.State.certCancelRequestBO = New CertCancelRequest(Me.State.CertCancelRequestId)
                        ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                        populateCancelRequestInfoTab()
                    End If

                    'Populate coverage history grid 
                    If ExpiredCoveragesExist Then
                        'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_COVERAGE_HISTORY_TAB), True)
                        EnableTab(CERT_COVERAGE_HISTORY_TAB, True)
                        Me.PopulateCoveragesHistoryGrid()
                    End If

                    If Not Me.State.MyBO.Product.UpgFinanceInfoRequireId.Equals(Guid.Empty) Then
                        populateFinanceTab()
                    End If

                    'If ((tsHoriz.SelectedIndex < 0) AndAlso (tsHoriz.Items(0).Enabled = True)) Then
                    '    Me.tsHoriz.SelectedIndex = 0
                    'End If
                    Me.EnableDisableFields()
                    Me.AddCalendar(Me.BtnProductSalesDate, Me.moProductSalesDateText)
                    Me.AddCalendar(Me.BtnDocumentIssueDate, Me.moDocumentIssueDateText)
                    Me.AddCalendar(Me.BtnWarrantySoldDate, Me.moWarrantySoldText)
                    Me.AddCalendar(Me.BtnDateOfBirth, Me.moDateOfBirthText)
                    Me.AddCalendar(Me.moCancelDateImageButton, Me.moCancelDateTextBox)
                    Me.AddCalendar(Me.moCancelRequestDateImagebutton, Me.moCancelRequestDateTextBox)
                    Me.AddCalendar(Me.BtnCertificateVerificationDate, Me.moCertificateVerificationDateText)
                    Me.AddCalendar(Me.BtnSEPAMandateDate, Me.moSEPAMandateDateText)
                    Me.AddCalendar(Me.BtnCheckVerificationDate, Me.moCheckVerificationDateText)
                    Me.AddCalendar(Me.BtnContractCheckCompleteDate, Me.moContractCheckCompleteDateText)
                    Me.AddCalendar(Me.BtnServiceStartDate, Me.moServiceStartDateText)
                Else 'page posted back
                    Me.State.selectedTab = hdnSelectedTab.Value 'store the selected tab
                    'populate the disabled tab list
                    If (Not Me.State Is Nothing) Then
                        If Me.State.disabledTabs.Trim = String.Empty Then
                            listDisabledTabs.Clear()
                        Else
                            For Each str As String In Me.State.disabledTabs.Split(",")
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

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If

                'Me.moCancellationReasonDrop.AutoPostBack = True
                Me.PaymentMethodDrop.AutoPostBack = True

                '*ANI  Me.PaymentReasonDrop.AutoPostBack = True
                selectedCancellationReason = Me.GetSelectedItem(moCancellationReasonDrop)
                selectedPaymentMethod = Me.GetSelectedItem(PaymentMethodDrop)

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
                If Not Me.State.IsClaimAllowed Then
                    ControlMgr.SetVisibleControl(Me, btnNewClaim, False)
                    'ElseIf Not String.IsNullOrEmpty(Me.State.ClaimRecordingXcd) AndAlso Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                    '    'REQ-6155
                    '    ControlMgr.SetVisibleControl(Me, btnNewClaim, False)
                Else
                    If (Me.State.AllowOldAndNewDCMClaims) Then
                        btnNewClaim.Text = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM_OLD")
                    Else
                        btnNewClaim.Text = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM")
                    End If
                    ControlMgr.SetVisibleControl(Me, btnNewClaimDcm, Me.State.AllowOldAndNewDCMClaims)
                End If
                'DEF-2486

                Dim dealerBO As Dealer = New Dealer(Me.State.MyBO.DealerId)
                If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If dealerBO.AttributeValues.Contains(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) Then

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
                    Me.TranslateGridHeader(Me.GridDataProtection)

                    If (Me.State.MyBO.CertificateIsRestricted) Then
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


                    If Me.State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Not Me.State.MyBO.CertificateIsRestricted Then
                        If Not Me.State.AllowForget Is Nothing AndAlso String.Compare(Me.State.AllowForget, Codes.EXT_YESNO_Y, True) = 0 Then
                            btnRightToForgotten.Visible = True
                        End If
                    End If

                End If

                btnNewClaim.Visible = Not Me.State.MyBO.CertificateIsRestricted


                ' Save Right To Forgotten after confirmation
                SaveRightToForgotten()

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

            '   Me.State.TheItemCoverageState.hi()
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.WorkingPanelVisible = True
                If Me.CalledUrl = ClaimForm.URL And ClaimForm.Save_Ok = True Then
                    ClaimForm.Save_Ok = False
                    Me.State.ClaimsearchDV = Nothing

                End If

                If ReturnFromUrl.Contains(ClaimRecordingForm.Url2) Then
                    If TypeOf ReturnPar Is ClaimRecordingForm.ReturnType Then
                        Dim retObjCRF As ClaimRecordingForm.ReturnType = CType(ReturnPar, ClaimRecordingForm.ReturnType)
                        Select Case retObjCRF.LastOperation
                            Case ElitaPlusPage.DetailPageCommand.Cancel
                                If Not retObjCRF Is Nothing Then
                                    Me.State.MyBO = New Certificate(retObjCRF.CertificateId)
                                    Me.State.IsCallerAuthenticated = retObjCRF.IsCallerAuthenticated
                                End If
                        End Select
                    ElseIf TypeOf ReturnPar Is ClaimForm.ReturnType Then
                        Dim retObjCF As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                        Select Case retObjCF.LastOperation
                            Case ElitaPlusPage.DetailPageCommand.Back
                                If Not retObjCF Is Nothing Then
                                    Me.State.MyBO = New Certificate(retObjCF.EditingBo.CertificateId)
                                    Me.State.IsCallerAuthenticated = retObjCF.IsCallerAuthenticated
                                End If
                        End Select
                    End If
                Else
                    Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            If Not retObj Is Nothing Then
                                Me.State.MyBO = New Certificate(retObj.EditingBo.CertificateId)
                                Me.State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                            End If
                    End Select
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CheckIfComingFromComments()
            If Me.State.NavigateToComment Then
                If isTabEnabled(CERT_COMMENTS_TAB) = True Then Me.State.selectedTab = CERT_COMMENTS_TAB
                Me.State.NavigateToComment = False
            End If
        End Sub

        Private Sub CheckIfComingFromCheckPayment()
            If Me.State.NavigateToCheckPayment Then
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then Me.State.selectedTab = CERT_PREMIUM_INFO_TAB
                Me.State.NavigateToCheckPayment = False
            End If
        End Sub

        Private Sub CheckIfComingFromEndorse()
            If Me.State.NavigateToEndorment Then
                If isTabEnabled(CERT_ENDORSEMENTS_TAB) = True Then Me.State.selectedTab = CERT_ENDORSEMENTS_TAB
                Me.State.NavigateToEndorment = False
                Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                PopulateFormFromBOs()
            End If

        End Sub

        Private Sub CheckIfComingFromBillingHistory()
            If Me.State.NavigateToBillingHistory Then
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then Me.State.selectedTab = CERT_PREMIUM_INFO_TAB
                Me.State.NavigateToBillingHistory = False
            End If

        End Sub

        Private Sub CheckIfComingFromPaymentHistory()
            If Me.State.NavigateToPaymentHistory Then
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then Me.State.selectedTab = CERT_PREMIUM_INFO_TAB
                Me.State.NavigateToPaymentHistory = False
            End If

        End Sub

        Private Sub CheckIfComingFromBillPayHistory()
            If Me.State.NavigateToBillpayHistory Then
                If isTabEnabled(CERT_PREMIUM_INFO_TAB) = True Then Me.State.selectedTab = CERT_PREMIUM_INFO_TAB
                Me.State.NavigateToBillpayHistory = False
            End If
        End Sub

        Private Sub CheckIfCoverageGridRefreshNeeded()
            If (Not Me.State.TheItemCoverageState Is Nothing) AndAlso Me.State.isCovgGridRefreshNeeded AndAlso Me.State.TheItemCoverageState.IsGridVisible Then
                Me.State.CoveragesearchDV = Nothing
                Me.PopulateCoveragesGrid()
                Me.State.isCovgGridRefreshNeeded = False
            End If
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub cboDocumentTypeId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocumentTypeId.SelectedIndexChanged
            EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
        End Sub
        Protected Sub EnableDisableFields()

            Me.btnBack.Enabled = True
            If Me.State.MyBO.getCancelationRequestFlag = YES Then
                ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
            Else
                If Me.State.MyBO.IsChildCertificate Then
                    ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
                Else
                    ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, True)
                End If
            End If

            If Not (Me.State.MyBO.PaymentTypeId.Equals(Guid.Empty)) Then
                If (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) _
                   Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) _
                   Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD) _
                   Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY) _
                   Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__PARTIAL_PAYMENT And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) Then
                    Me.State.creditCardPayment = True
                ElseIf (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) _
                       Or (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__PARTIAL_PAYMENT And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) Then
                    Me.State.directDebitPayment = True
                End If
            End If


            If Not Me.State.IsEdit Then

                Me.EnableDisableTabs(Me.State.IsEdit)

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
                ControlMgr.SetVisibleControl(Me, Me.moSalutationText, False)
                ControlMgr.SetVisibleControl(Me, Me.moSalutationLabel, False)
                ControlMgr.SetVisibleControl(Me, Me.cboSalutationId, False)
                ControlMgr.SetVisibleControl(Me, Me.moPostPaidLabel, False)
                ControlMgr.SetVisibleControl(Me, Me.moPostPaidText, False)
                ControlMgr.SetVisibleControl(Me, Me.moLangPrefText, False)
                ControlMgr.SetVisibleControl(Me, Me.cboLangPref, False)

                ControlMgr.SetVisibleControl(Me, moCancelRequestDateImagebutton, False)
                ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
                ControlMgr.SetVisibleControl(Me, moCallerNameTextBox, False)
                ControlMgr.SetVisibleControl(Me, moCallerNameLabel, False)
                ControlMgr.SetVisibleControl(Me, moCommentsTextbox, False)
                ControlMgr.SetVisibleControl(Me, moCommentsLabel, False)

                Me.moCancelDateTextBox.ReadOnly = True
                Me.moCancelDateTextBox.CssClass = "FLATTEXTBOX"
                Me.moCancelRequestDateTextBox.ReadOnly = True
                Me.moCancelRequestDateTextBox.CssClass = "FLATTEXTBOX"
                ControlMgr.SetEnableControl(Me, moCancelRequestReasonDrop, False)
                Me.moCancelRequestReasonDrop.CssClass = "FLATTEXTBOX"

                ControlMgr.SetVisibleControl(Me, moCanReqJustificationLabel, False)
                ControlMgr.SetVisibleControl(Me, moCancelRequestJustificationDrop, False)
                Me.moCancelRequestJustificationDrop.CssClass = "FLATTEXTBOX"

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

                If (Not Me.State.MyBO.StatusCode = CERT_CANCEL_STATUS) And Me.State.MyBO.getCancelationRequestFlag = YES Then
                    If Me.State.certCancelRequestBO Is Nothing Then
                        Me.State.CertCancelRequestId = Me.State.MyBO.getCertCancelRequestID
                        If Not Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                            Me.State.certCancelRequestBO = New CertCancelRequest(Me.State.CertCancelRequestId)
                            If Me.State.CancelRulesForSFR = Codes.YESNO_Y AndAlso Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Or Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            Else
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                            End If
                            If Me.State.CancelRulesForSFR = Codes.YESNO_Y AndAlso Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED And Me.State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE Then
                                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, True)
                            Else
                                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                        End If
                    ElseIf Me.State.certCancelRequestBO Is Nothing And Not Me.State.MyBO.getCertCancelRequestID.Equals(guid.Empty) Then
                        Me.State.CertCancelRequestId = Me.State.MyBO.getCertCancelRequestID
                        Me.State.certCancelRequestBO = New CertCancelRequest(Me.State.CertCancelRequestId)
                        ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                    Else
                        If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
                            If Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_ACCEPTED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                            ElseIf Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED Then
                                ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, True)
                        End If
                        If Me.State.CancelRulesForSFR = Codes.YESNO_Y AndAlso Me.State.certCancelRequestBO.Status = CERT_CAN_REQ_DENIED And Me.State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE Then
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
                If Me.State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
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


                If Not (Me.State.IsNewBillPayBtnVisible) And Me.State.oPaymentCount > 0 Then
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                Else
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                End If

                If Me.State.oBillingCount > 0 Then
                    Dim dvBillingTotals As BillingDetail.BillingTotals = BillingDetail.getBillingTotals(Me.State.MyBO.Id)
                    If Not dvBillingTotals Is Nothing And dvBillingTotals.Count > 0 And dvBillingTotals.Item(0).Row(0) > 0 Then
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                    End If
                Else
                    ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                End If
                'Bug-216874: Added condition to check bill pay total.
                Dim dvBillPay As DataView
                dvBillPay = BillingPayDetail.getBillPayTotals(Me.State.MyBO.Id)
                Dim cnt = 0
                If Not dvBillPay Is Nothing And dvBillPay.Count > 0 And dvBillPay.Table.Rows.Count > 0 Then
                    cnt = CType(dvBillPay.Table.Rows(0).Item(0), Integer)
                End If

                If (Me.State.oBillPayCount > 0 Or cnt > 0) And Me.State.IsNewBillPayBtnVisible Then
                    ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                    ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                ElseIf (Me.State.oPayColltCount > 0) Then
                    ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                    If (Me.State.IsNewBillPayBtnVisible) Then
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                    End If
                Else
                    If (Me.State.IsNewBillPayBtnVisible) Then
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
                If Not Me.State.MyBO.StatusCode = CERT_STATUS Then
                    ControlMgr.SetEnableControl(Me, Me.btnDebitEdit_WRITE, False)
                End If

                If Me.State.isSalutation Then
                    ControlMgr.SetVisibleControl(Me, Me.moSalutationText, True)
                    ControlMgr.SetVisibleControl(Me, Me.moSalutationLabel, True)
                End If

                If Me.State.isPostPrePaid Then
                    ControlMgr.SetVisibleControl(Me, Me.moPostPaidLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.moPostPaidText, True)
                End If
                ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, False)
                ControlMgr.SetVisibleControl(Me, moDocumentTypeText, True)
                ControlMgr.SetVisibleControl(Me, cboLangPref, False)
                ControlMgr.SetVisibleControl(Me, moLangPrefText, True)

                If Me.State.isItemsGridVisible Then
                    Me.PopulateItemsGrid()
                End If

                If Me.State.isRegItemsGridVisible Then
                    Me.PopulateRegisterItemsGrid()
                End If

                If Me.State.MyBO.IsChildCertificate Then
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, False)
                ElseIf Me.State.MyBO.getCancelationRequestFlag = YES Or Not Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, True)
                Else
                    EnableTab(CERT_CANCEL_REQUEST_INFO_TAB, False)
                End If
                'AA REQ-910 new fields added BEGIN
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3=Display and Require at enrollment
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
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment

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

                EnableDisableControls(Me.moCertificateDetailPanel, True)
                EnableDisableControls(Me.moItemsTabPanel_WRITE, True)
                'Calling the method EnableDisableControls to disable the Premium Tab contents.

                EnableDisableControls(Me.moPremiumInformationTabPanel_WRITE, True)
                ' Start - REQ:5932
                If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If (Me.State.MyBO.CustomerFirstName Is Nothing) And (Me.State.MyBO.CustomerLastName Is Nothing) Then
                        Me.moCustName1.Attributes("style") = "display: none"
                        Me.moCustName2.Attributes("style") = "display: none"
                        moAltCustLasName.Attributes("style") = "display: none"
                        moAltCustFirstName.Attributes("style") = "display: none"
                        Me.AdditionalCustomer.Attributes("style") = "display: none"
                    Else
                        Me.moCustName01.Attributes("style") = "display: none"
                        Me.moCustName02.Attributes("style") = "display: none"
                        'Me.moCustLegalInfo1.Attributes("style") = "display: none"
                        Me.moAMLRegulations3.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, moIncomeRangeLabel, False)
                        ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                        ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILLabel, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)
                    End If
                Else
                    Me.moCustName1.Attributes("style") = "display: none"
                    Me.moCustName2.Attributes("style") = "display: none"
                    moAltCustLasName.Attributes("style") = "display: none"
                    moAltCustFirstName.Attributes("style") = "display: none"
                    Me.AdditionalCustomer.Attributes("style") = "display: none"
                End If
                ' End - REQ:5932

                ControlMgr.SetVisibleControl(Me, BtnServiceStartDate, False)

            Else 'Edit Mode
                '   Me.MenuEnabled = False
                ControlMgr.SetEnableControl(Me, btnCancelCertificate_WRITE, False)
                Select Case Me.State.selectedTab 'Me.tsHoriz.SelectedIndex
                    Case Me.CERT_DETAIL_TAB
                        If Me.State.isSalutation Then
                            ControlMgr.SetVisibleControl(Me, cboSalutationId, True)
                            ControlMgr.SetEnableControl(Me, cboSalutationId, True)
                            ControlMgr.SetVisibleControl(Me, moSalutationText, False)
                        End If
                        If Me.State.isPostPrePaid Then
                            ControlMgr.SetVisibleControl(Me, Me.moPostPaidLabel, True)
                            ControlMgr.SetVisibleControl(Me, Me.moPostPaidText, True)
                        End If
                        Me.EnableDisableTabs(Me.State.IsEdit)
                        Me.setCertDetailTab()
                    Case Me.CERT_INFORMATION_TAB
                        Me.EnableDisableTabs(Me.State.IsEdit)
                        Me.setCertInfoTab()
                    Case Me.CERT_TAX_ID_TAB
                        Me.EnableDisableTabs(Me.State.IsEdit)
                        Me.setTaxIdTab()
                        'START  DEF-1986
                        EnableDisableTaxIdControls(Me.cboDocumentTypeId.SelectedItem.Text)
                        'END    DEF-1986
                    Case Me.CERT_ITEMS_INFO_TAB
                        Me.EnableDisableTabs(Me.State.IsEdit)
                    Case Me.CERT_CANCEL_REQUEST_INFO_TAB
                        If Me.State.MyBO.getCancelationRequestFlag = YES Then
                            Me.EnableDisableTabs(Me.State.IsEdit)
                        ElseIf Not Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                            Me.EnableDisableTabs(True)
                        Else
                            Me.EnableDisableTabs(Me.State.IsEdit)
                        End If
                        Me.setCancelRequestTab()
                    Case Me.CERT_PREMIUM_INFO_TAB
                        Me.EnableDisableTabs(Me.State.IsEdit)
                        Dim oBillingCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.GetSelectedItem(Me.moBillingStatusId))
                        If oBillingCode <> "A" Then
                            ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, True)
                            Me.CheckBoxSendLetter.CssClass = ""
                        End If
                        ControlMgr.SetEnableControl(Me, btnUndoDebit_WRITE, True)
                        ControlMgr.SetEnableControl(Me, btnDebitEdit_WRITE, False)
                        ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                        If (Me.State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                        End If
                        ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, True)
                        ControlMgr.SetEnableControl(Me, moBillingStatusId, True)
                        ControlMgr.SetEnableControl(Me, moPaymentTypeId, False)
                        If Me.State.directDebitPayment Then
                            ControlMgr.SetEnableControl(Me, moBankAccountOwnerText, True)
                            ControlMgr.SetEnableControl(Me, moBankRoutingNumberText, True)
                            ControlMgr.SetEnableControl(Me, moBankAccountNumberText, True)
                            Me.moBillingStatusId.CssClass = ""
                            moBankAccountOwnerText.ReadOnly = False
                            moBankRoutingNumberText.ReadOnly = False
                            moBankAccountNumberText.ReadOnly = False
                        ElseIf Me.State.creditCardPayment Then
                            ControlMgr.SetEnableControl(Me, moCreditCardNumberText, True)
                            ControlMgr.SetEnableControl(Me, moCreditCardTypeIDDropDown, True)
                            ControlMgr.SetEnableControl(Me, moExpirationDateText, True)
                            ControlMgr.SetEnableControl(Me, moNameOnCreditCardText, True)
                            moCreditCardNumberText.ReadOnly = False
                            moNameOnCreditCardText.ReadOnly = False
                            moExpirationDateText.ReadOnly = False
                        End If
                End Select
                'AA REQ-910 new fields added BEGIN
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only  or 3= Display and Require At Enrollment
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
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only  or 3= Display and Require At Enrollment
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
                If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    If (Me.State.MyBO.CustomerFirstName Is Nothing) And (Me.State.MyBO.CustomerLastName Is Nothing) Then
                        Me.moCustName1.Attributes("style") = "display: none"
                        Me.moCustName2.Attributes("style") = "display: none"
                        moAltCustLasName.Attributes("style") = "display: none"
                        moAltCustFirstName.Attributes("style") = "display: none"
                        Me.AdditionalCustomer.Attributes("style") = "display: none"
                    Else
                        Me.moCustName01.Attributes("style") = "display: none"
                        Me.moCustName02.Attributes("style") = "display: none"
                        'Me.moCustLegalInfo1.Attributes("style") = "display: none"
                        Me.moAMLRegulations3.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, moIncomeRangeLabel, False)
                        ControlMgr.SetVisibleControl(Me, moIncomeRangeText, False)
                        ControlMgr.SetVisibleControl(Me, cboIncomeRangeId, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILLabel, False)
                        ControlMgr.SetVisibleControl(Me, moCUIT_CUILText, False)

                        'To Check if Transfer Of Ownership flag is true, then disable Customer name and TAX ID
                        Dim objComp As New Company(Me.State.MyBO.CompanyId)
                        Dim strTranferOfOwnership As String
                        If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                            strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                        Else
                            strTranferOfOwnership = String.Empty
                        End If
                        If strTranferOfOwnership = YES And Me.State.MyBO.StatusCode = CERT_STATUS Then
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
                    Me.moCustName1.Attributes("style") = "display: none"
                    Me.moCustName2.Attributes("style") = "display: none"
                    moAltCustLasName.Attributes("style") = "display: none"
                    moAltCustFirstName.Attributes("style") = "display: none"
                    Me.AdditionalCustomer.Attributes("style") = "display: none"
                End If

                ControlMgr.SetVisibleControl(Me, BtnServiceStartDate, True)
                ' End - REQ:5932
            End If



            If Me.State.MyBO.IsChildCertificate Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_ENDORSEMENTS_TAB), False)
                EnableTab(CERT_ENDORSEMENTS_TAB, False)
            End If

            If Me.State.MyBO.StatusCode = CLOSED Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                EnableTab(CERT_CANCELLATION_INFO_TAB, True)
            Else
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), False)
                EnableTab(CERT_CANCELLATION_INFO_TAB, False)
            End If

            If Me.State.ValFlag = Me.State.MyBO.VALIDATION_FLAG_NONE Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_TAX_ID_TAB), False)
                EnableTab(CERT_TAX_ID_TAB, False)
            End If

            If Not Me.State.isPremiumTAbVisible Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_PREMIUM_INFO_TAB), False)
                EnableTab(CERT_PREMIUM_INFO_TAB, False)
            End If

            If Not ExpiredCoveragesExist Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_COVERAGE_HISTORY_TAB), False)
                EnableTab(CERT_COVERAGE_HISTORY_TAB, False)
            End If

            ' This gets enable on the Top of this SUB, with  EnableDisableTabs 
            If Me.State.MyBO.Product.UpgFinanceInfoRequireId.Equals(Guid.Empty) Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_FINANCED_TAB), False)
                EnableTab(CERT_FINANCED_TAB, False)
            End If


            If (Not Me.State.CertInstallmentHistoryDV Is Nothing AndAlso Me.State.CertInstallmentHistoryDV.Count = 0) Then
                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), False)
                EnableTab(CERT_REPRICE_TAB, False)
            End If

            If Me.State.ComputeCancellationDateEOfM.Equals(Codes.YESNO_Y) Then
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

            If Not Me.State.MyBO Is Nothing AndAlso Not Me.State.MyBO.Company Is Nothing Then

                'Get Dealer Type
                Dim dealerBO As Dealer = New Dealer(Me.State.MyBO.DealerId)
                If Not dealerBO Is Nothing Then

                    If dealerBO.AttributeValues.Contains(Codes.DLR_ATTRBT__UPDATE_BANK_FOR_INSTALLMENTS) Then

                        If dealerBO.AttributeValues.Value(Codes.DLR_ATTRBT__UPDATE_BANK_FOR_INSTALLMENTS) = Codes.YESNO_Y Then

                            Dim countryBO As Country = New Country(Me.State.MyBO.Company.CountryId)
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
        Private Sub EnableDisableTaxIdControls(ByVal docType As String)
            'START  DEF-1986
            If docType = "CNPJ" Then
                ControlMgr.SetEnableControl(Me, Me.moIDTypeText, False)
                ControlMgr.SetEnableControl(Me, Me.moDocumentAgencyText, False)
                ControlMgr.SetEnableControl(Me, Me.moRGNumberText, False)
                ControlMgr.SetEnableControl(Me, Me.moDocumentIssueDateText, False)
                ControlMgr.SetEnableControl(Me, Me.BtnDocumentIssueDate, False)
                moIDTypeText.Text = String.Empty
                moDocumentAgencyText.Text = String.Empty
                moDocumentIssueDateText.Text = String.Empty
                moRGNumberText.Text = String.Empty
            Else
                ControlMgr.SetEnableControl(Me, Me.moIDTypeText, True)
                ControlMgr.SetEnableControl(Me, Me.moDocumentAgencyText, True)
                ControlMgr.SetEnableControl(Me, Me.moRGNumberText, True)
                ControlMgr.SetEnableControl(Me, Me.moDocumentIssueDateText, True)
                ControlMgr.SetEnableControl(Me, Me.BtnDocumentIssueDate, True)
            End If
        End Sub
        'END DEF-1986

        Private Sub DisableCreditCardInformation()

            Me.moCreditCardInformation1.Attributes("style") = "display: none"
            Me.moCreditCardInformation2.Attributes("style") = "display: none"

        End Sub

        Private Sub setCancelRequestTab()
            ControlMgr.SetEnableControl(Me, btnCancelRequestEdit_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCancelRequestSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCancelRequestUndo_WRITE, True)

            ControlMgr.SetEnableControl(Me, moCancelRequestReasonDrop, True)
            Me.moCancelRequestReasonDrop.CssClass = "FLATTEXTBOX"

            ControlMgr.SetVisibleControl(Me, moCanReqJustificationLabel, True)
            ControlMgr.SetVisibleControl(Me, moCancelRequestJustificationDrop, True)
            Me.moCancelRequestJustificationDrop.CssClass = "FLATTEXTBOX"

            Me.moCancelDateLabel.Enabled = True
            Me.moCancelDateTextBox.ReadOnly = False
            'Me.moCancelDateTextBox.BackColor = Color.White
            'Me.moCancelDateTextBox.CssClass = ""

            Me.moCancelRequestDateLabel.Enabled = True
            Me.moCancelRequestDateTextBox.ReadOnly = False
            'Me.moCancelRequestDateTextBox.BackColor = Color.White
            'Me.moCancelRequestDateTextBox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCallerNameLabel, True)
            ControlMgr.SetVisibleControl(Me, moCallerNameTextBox, True)
            Me.moCallerNameLabel.Enabled = True
            Me.moCallerNameTextBox.ReadOnly = False
            'Me.moCallerNameTextBox.BackColor = Color.White
            'Me.moCallerNameTextBox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCommentsLabel, True)
            ControlMgr.SetVisibleControl(Me, moCommentsTextbox, True)
            Me.moCommentsLabel.Enabled = True
            Me.moCommentsTextbox.ReadOnly = False
            'Me.moCommentsTextbox.BackColor = Color.White
            'Me.moCommentsTextbox.CssClass = ""

            ControlMgr.SetVisibleControl(Me, moCancelRequestDateImagebutton, True)
            Me.moCancelRequestDateImagebutton.Enabled = True
            ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, True)
            Me.moCancelDateImageButton.Enabled = True
            If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
                ControlMgr.SetEnableControl(Me, moCancelDateTextBox, False)
                ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
            End If
            ControlMgr.SetEnableControl(Me, moCRIBANNumberText, True)
        End Sub

        Private Sub setCertDetailTab()
            ControlMgr.SetEnableControl(Me, btnEditCertDetail_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoCertDetail_Write, True)
            ControlMgr.SetEnableControl(Me, btnSaveCertDetail_WRITE, True)

            Me.moHomePhoneLabel.Enabled = True
            Me.moHomePhoneText.ReadOnly = False

            Me.moWorkPhoneLabel.Enabled = True
            Me.moWorkPhoneText.ReadOnly = False

            Me.moEmailAddressLabel.Enabled = True
            Me.moEmailAddressText.ReadOnly = False

            Me.moTaxIdLabel.Enabled = True
            Me.moTaxIdText.ReadOnly = False

            Dim dealerBO As Dealer = New Dealer(Me.State.MyBO.DealerId)
            If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                If dealerBO.AttributeValues.Contains(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) Then

                    If dealerBO.AttributeValues.Value(Codes.DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME) = Codes.YESNO_Y Then
                        ControlMgr.SetEnableControl(Me, moAlternativeLastNameLabel, True)
                        ControlMgr.SetVisibleControl(Me, moAlternativeLastNameText, True)
                        ControlMgr.SetEnableControl(Me, moAlternativeFirstNameLabel, True)
                        ControlMgr.SetVisibleControl(Me, moAlternativeFirstNameText, True)
                    End If
                End If
            End If

            If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                Me.moCustomerFirstNameLabel.Enabled = True
                Me.moCustomerFirstNameText.ReadOnly = False
                Me.moCustomerMiddleNameLabel.Enabled = True
                Me.moCustomerMiddleNameText.ReadOnly = False
                Me.moCustomerLastNameLabel.Enabled = True
                Me.moCustomerLastNameText.ReadOnly = False
                Me.moAlternativeLastNameLabel.Enabled = True
                Me.moAlternativeLastNameText.ReadOnly = False
                Me.moAlternativeFirstNameLabel.Enabled = True
                Me.moAlternativeFirstNameText.ReadOnly = False
                Me.moCorporateNameLabel.Enabled = True
                Me.moCorporateNameText.ReadOnly = False
            Else
                Me.moCustomerNameLabel.Enabled = True
                Me.moCustomerNameText.ReadOnly = False
            End If


            AddressCtr.EnableControls(False, True)

            'DEF-21659 - START
            'If Me.State.MyBO.DateOfBirth Is Nothing Then
            '    ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
            'Else
            Me.moDateOfBirthLabel.Enabled = True
            Me.moDateOfBirthText.ReadOnly = False
            ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, True)
            Me.BtnDateOfBirth.Enabled = True
            'End If
            'DEF-21659 - END

            If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                Me.moCityOfBirthText.Enabled = True
                Me.moCityOfBirthText.ReadOnly = False
            End If

            ControlMgr.SetVisibleControl(Me, cboLangPref, True)
            ControlMgr.SetEnableControl(Me, cboLangPref, True)
            ControlMgr.SetVisibleControl(Me, moLangPrefText, False)

            If Me.State.MyBO.IsChildCertificate OrElse Me.State.MyBO.IsParentCertificate Then
                ControlMgr.SetVisibleControl(Me, cboLangPref, False)
                ControlMgr.SetEnableControl(Me, cboLangPref, False)
                ControlMgr.SetVisibleControl(Me, moLangPrefText, True)
            End If


            'AA REQ-910 new fields added BEGIN
            If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
               Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
               Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                Me.moOccupationLabel.Enabled = True
                Me.moOccupationText.ReadOnly = False

                Me.moPoliticallyExposedLabel.Enabled = True
                Me.moPoliticallyExposedText.ReadOnly = False

                Me.moIncomeRangeLabel.Enabled = True
                Me.moIncomeRangeText.ReadOnly = False
            End If
            'AA REQ-910 new fields added END

            'REQ-1255 -- START
            If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
               Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
               Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                Me.moCUIT_CUILLabel.Enabled = True
                Me.moCUIT_CUILText.ReadOnly = False
            End If
            'REQ-1255 -- END
        End Sub

        Private Sub setCertInfoTab()
            ControlMgr.SetEnableControl(Me, btnEditCertInfo_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoCertInfo_Write, True)
            ControlMgr.SetEnableControl(Me, btnSaveCertInfo_WRITE, True)

            Me.moInvoiceNumberLabel.Enabled = True
            Me.moInvoiceNumberText.ReadOnly = False

            If Me.State.MyBO.Source.ToUpper().Equals("VSC") Then
                Me.moVehicleLicenseTagText.Enabled = True
                Me.moVehicleLicenseTagText.ReadOnly = False
            End If

            If Me.State.TheItemCoverageState.manufaturerWarranty Then
                Me.moWarrantySoldLabel.Enabled = True
                'Me.moWarrantySoldText.ReadOnly = False
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, True)
                Me.BtnWarrantySoldDate.Enabled = True
            Else
                Me.moProductSalesDateLabel.Enabled = True
                Me.moProductSalesDateText.ReadOnly = False
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, True)
                Me.BtnProductSalesDate.Enabled = True
            End If

            If Not Me.State.TheItemCoverageState.IsRetailer Then
                Me.moRetailerLabel.Enabled = True
                Me.moRetailerText.ReadOnly = False
            End If

            If Me.State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, True)
                Me.BtnContractCheckCompleteDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, True)
                Me.BtnCertificateVerificationDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, True)
                Me.BtnSEPAMandateDate.Enabled = True
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, True)
                Me.BtnCheckVerificationDate.Enabled = True
            End If

            moSalesRepNumberLabel.Enabled = True
            moSalesRepNumberText.ReadOnly = False

            moAccountNumberText.ReadOnly = False
            cboAccountType.Enabled = True

            moVATNumLabel.Enabled = True
            moVATNumText.ReadOnly = False

            moRegionText.Enabled = True
            moRegionText.ReadOnly = False

            If Me.State.MyBO.IsParentCertificate OrElse Me.State.MyBO.IsChildCertificate Then
                ControlMgr.SetEnableControl(Me, moProductSalesDateText, False)
                ControlMgr.SetEnableControl(Me, moActivationDateText, False)
            End If
            ControlMgr.SetEnableControl(Me, moFulfillmentConsentActionDrop, False)

        End Sub

        Private Sub setTaxIdTab()

            ControlMgr.SetEnableControl(Me, btnEditTaxID_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndoTaxID_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSaveTaxID_WRITE, True)

            ControlMgr.SetVisibleControl(Me, Me.moTaxIdText, True)

            ControlMgr.SetVisibleControl(Me, BtnDocumentIssueDate, True)
            Me.BtnDocumentIssueDate.Enabled = True

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
            Me.BindBOPropertyToLabel(Me.State.TheDirectDebitState.certInstallment, "BillingStatusId", Me.moBilling_StatusLabel)
            Me.BindBOPropertyToLabel(Me.State.TheDirectDebitState.certInstallment, "SendLetterId", Me.moSendLetterIdLabel)

            Me.BindBOPropertyToLabel(Me.State.TheDirectDebitState.bankInfo, "Account_Name", Me.moBankAccountOwnerLabel)
            Me.BindBOPropertyToLabel(Me.State.TheDirectDebitState.bankInfo, "Bank_Id", Me.moBankRoutingNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.TheDirectDebitState.bankInfo, "Account_Number", Me.moBankAccountNumberLabel)
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

        Protected Sub BindBoPropertiesToLabels()
            'pedro
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerName", Me.moCustomerNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "HomePhone", Me.moHomePhoneLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "WorkPhone", Me.moWorkPhoneLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Email", Me.moEmailAddressLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IdentificationNumber", Me.moTaxIdLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceNumber", Me.moInvoiceNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDate", Me.moProductSalesDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "WarrantySalesDate", Me.moWarrantySoldLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Retailer", Me.moRetailerLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SalutationId", Me.moSalutationLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LanguageId", Me.moLangPrefLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PostPrePaidId", Me.moPostPaidLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "VehicleLicenseTag", Me.moVehicleLicenseTagLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentIssueDate", Me.moDocumentIssueDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentTypeID", Me.moDocumentTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RgNumber", Me.moRGNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentAgency", Me.moDocumentAgencyLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IdType", Me.moIDTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIDNumb", Me.moNewTaxIdLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SalesRepNumber", Me.moSalesRepNumberLabel)

            Me.BindBOPropertyToLabel(Me.State.CreditCardInfoBO, "CreditCardFormatId", Me.moCreditCardTypeIDLabel)
            Me.BindBOPropertyToLabel(Me.State.CreditCardInfoBO, "CreditCardNumber", Me.moCreditCardNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.CreditCardInfoBO, "ExpirationDate", Me.moExpirationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.CreditCardInfoBO, "NameOnCreditCard", Me.moNameOnCreditCardLabel)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "DateOfBirth", Me.moDateOfBirthLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "VatNum", Me.moVATNumLabel)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "CapitalizationSeries", Me.moCapSeriesLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CapitalizationNumber", Me.moCapNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SubStatusChangeDate", Me.moSubStatusChangeDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LinesOnAccount", Me.moLinesOnAccountLabel)

            Me.ClearGridHeadersAndLabelsErrSign()

            'Added for Req-910 - Start
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Occupation", Me.moOccupationLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PoliticallyExposedId", Me.moPoliticallyExposedLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IncomeRangeId", Me.moIncomeRangeLabel)
            'Added for Req-910 - End

            'REQ-1255 - AML Regulations - START
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaritalStatus", Me.moMaritalStatusLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Nationality", Me.moNationalityLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PlaceOfBirth", Me.moPlaceOfBirthLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CityOfBirth", Me.mocityOfBirthLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PersonType", Me.moPerson_typeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Gender", Me.moGenderLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CUIT_CUIL", Me.moCUIT_CUILLabel)
            'REQ-1255 - AML Regulations - END

            Me.BindBOPropertyToLabel(Me.State.MyBO, "Finance_Tab_Amount", Me.moFinanceAmountLabel)
            '  Me.BindBOPropertyToLabel(Me.State.MyBO, "", Me.moCurrentOutstandingBalanceLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Finance_Term", Me.moFinanceTermLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FinanceDate", Me.moFinanceDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Finance_Frequency", Me.moFinanceFrequencyLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DownPayment", Me.moDownPaymentLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Finance_Installment_Number", Me.moFinanceInstallmentNumLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Finance_Installment_Amount", Me.moFinanceInstallmentAmountLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AdvancePayment", Me.moAdvancePaymentLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NumOfConsecutivePayments", Me.moNumOfConsecutivePaymentsLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BillingAccountNumber", Me.moBillingAccountNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerRewardPoints", Me.moDealerRewardPointsLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerCurrentPlanCode", Me.moDealerCurrentPlanCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerScheduledPlanCode", Me.moDealerScheduledPlanCodeLabel)
            '            Me.BindBOPropertyToLabel(Me.State.MyBO, "", Me.lblUpgradeTermUnitOfMeasure)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UpgradeProgram", Me.moUpgradeProgramLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UpgradeFixedTerm", Me.moUpgradeFixedTermLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UpgradeTermFrom", Me.moUpgradeTermLabelFrom)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UpgradeTermTo", Me.moUpgradeTermLabelTo)
            If Not moCurrentOutstandingBalanceLabel.Text.EndsWith(":") Then
                moCurrentOutstandingBalanceLabel.Text = moCurrentOutstandingBalanceLabel.Text & ":"
            End If
            If Not lblUpgradeTermUnitOfMeasure.Text.EndsWith(":") Then
                lblUpgradeTermUnitOfMeasure.Text = lblUpgradeTermUnitOfMeasure.Text & ":"
            End If

            Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentShiftNumber", Me.moPaymentShiftNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LoanCode", Me.moLoanCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PenaltyFee", Me.moPenaltyFeeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificateSigned", Me.moCertificateSignedLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SepaMandateSigned", Me.moSEPAMandateSignedLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CheckSigned", Me.moCheckSignedLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContractCheckCompleteDate", Me.moContractCheckCompleteDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificateVerificationDate", Me.moCertificateVerificationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SepaMandateDate", Me.moSEPAMandateDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CheckVerificationDate", Me.moCheckVerificationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContractCheckComplete", Me.moContractCheckCompleteLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InsuranceOrderNumber", Me.moInsuranceOrderNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DeviceOrderNumber", Me.moDeviceOrderNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UpgradeType", Me.moUpgradeTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FulfillmentConsentAction", Me.moFulfillmentConsentAction)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PlanType", Me.moPlanTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceStartDate", Me.moServiceStartDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceID", Me.moServiceIdLabel)
        End Sub

        Private Sub ObtainBooleanFlags()
            Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(Me.State.MyBO.CompanyId)
            Dim oDealer As Assurant.ElitaPlus.BusinessObjectsNew.Dealer = New Assurant.ElitaPlus.BusinessObjectsNew.Dealer(Me.State.MyBO.DealerId)
            Dim oClaimSystem As New ClaimSystem(oDealer.ClaimSystemId) 'DEF-2486
            Dim noId As New Guid 'DEF-2486

            Me.State.companyCode = companyBO.Code

            Me.State.isSalutation = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, companyBO.SalutationId)

            Me.State.IsNewBillPayBtnVisible = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, oDealer.UseNewBillForm)

            'DEF-2486
            noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

            If oClaimSystem.NewClaimId.Equals(noId) Then
                Me.State.IsClaimAllowed = False
            End If
            'DEF-2486

            'Task-203459
            If oDealer.ClaimRecordingXcd = Codes.DEALER_CLAIM_RECORDING_BOTH Then
                Me.State.AllowOldAndNewDCMClaims = True
            Else
                Me.State.AllowOldAndNewDCMClaims = False
            End If
            'Task-203459

            If Not (Me.State.MyBO.PostPrePaidId.Equals(Guid.Empty)) Then
                Me.State.isPostPrePaid = True
            Else
                Me.State.isPostPrePaid = False
            End If
            Me.State.TheItemCoverageState.IsRetailer = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, oDealer.RetailerId)
            Me.State.TheItemCoverageState.dealerName = oDealer.DealerName
            Me.State.ClaimRecordingXcd = oDealer.ClaimRecordingXcd
        End Sub

        Private Function GetYesNo(ByVal LanguageId As Guid, ByVal oId As Guid) As Boolean
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

            If Me.State.isSalutation Then
                PopulateSalutationDropdown(cboSalutationId)
            End If

            PopulateLangPrefDropdown(Me.cboLangPref)

            If Not (Me.State.MyBO.LanguageId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboLangPref, Me.State.MyBO.LanguageId)
                Me.PopulateControlFromBOProperty(Me.moLangPrefText, Me.State.MyBO.getLanguagePrefDesc)
            End If

            If Not (Me.State.MyBO.PostPrePaidId.Equals(Guid.Empty)) Then
                Me.PopulateControlFromBOProperty(Me.moPostPaidText, Me.State.MyBO.getPostPrePaidDesc)
            End If

            If Not (Me.State.MyBO.TypeOfEquipmentId.Equals(Guid.Empty)) Then
                Dim dv As DataView = LookupListNew.GetListItemId(Me.State.MyBO.TypeOfEquipmentId, Authentication.LangId)
                typeOfEquip = dv.Item(FIRST_ROW).Item(DESCRIPTION).ToString
            End If

            If Not (Me.State.MyBO.PaymentTypeId.Equals(Guid.Empty)) Then
                If (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__CREDIT_CARD) _
                   OrElse (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) _
                   OrElse (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY) _
                   OrElse (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD) Then
                    Me.State.creditCardPayment = True
                End If
                Me.PopulateControlFromBOProperty(Me.moPaymentByText, Me.State.MyBO.getPaymentTypeDescription)
            End If

            If Not (Me.State.MyBO.PurchaseCurrencyId.Equals(Guid.Empty)) Then
                Me.PopulateControlFromBOProperty(Me.moCurrencyPurchaseText, Me.State.MyBO.getPurchaseCurrencyDescription)
            End If

            moCertificateInfoController = Me.UserCertificateCtr
            moCertificateInfoController.InitController(Me.State.MyBO, , Me.State.companyCode)
            UpdateBreadCrum()

            With Me.State.MyBO
                AddressCtr.Bind(.AddressChild, Me.State.MyBO.Product.ClaimProfile)
                Me.PopulateControlFromBOProperty(Me.moCountryOfPurchaseTextBox, Me.State.CountryOfPurchase)
                Me.PopulateControlFromBOProperty(Me.moCustomerNameText, .CustomerName)
                Me.PopulateControlFromBOProperty(Me.moHomePhoneText, .HomePhone)
                Me.PopulateControlFromBOProperty(Me.moWorkPhoneText, .WorkPhone)
                If .Email Is Nothing OrElse .Email.Equals(String.Empty) Then
                    Me.State.EmailIsNull = TranslationBase.TranslateLabelOrMessage("THERE_IS_NO_VALUE")
                    Me.PopulateControlFromBOProperty(Me.moEmailAddressText, Me.State.EmailIsNull)
                Else
                    Me.PopulateControlFromBOProperty(Me.moEmailAddressText, .Email)
                End If

                Me.PopulateControlFromBOProperty(Me.moTaxIdText, .IdentificationNumber)
                ' Based on identification number type - change the label text
                If Not (.IdentificationNumberType Is Nothing) AndAlso Not (.IdentificationNumberType = IDENTIFICATION_NUMBER_TYPE_DEFAULT) Then
                    moTaxIdLabel.Text = TranslationBase.TranslateLabelOrMessage(.IdentificationNumberType)
                End If

                Me.PopulateControlFromBOProperty(Me.moProductSalesDateText, .ProductSalesDate)
                Me.PopulateControlFromBOProperty(Me.moActivationDateText, .InsuranceActivationDate)
                Me.PopulateControlFromBOProperty(Me.moOldNumberText, .OldNumber)
                Me.PopulateControlFromBOProperty(Me.moWarrantySoldText, .WarrantySalesDate)
                Me.PopulateControlFromBOProperty(Me.moInvoiceNumberText, .InvoiceNumber)
                Me.PopulateControlFromBOProperty(Me.moAccountNumberText, .MembershipNumber)
                Me.PopulateControlFromBOProperty(Me.moCampaignNumberText, .CampaignNumber)
                Me.PopulateControlFromBOProperty(Me.moDealerBranchCodeText, .DealerBranchCode)
                Me.PopulateControlFromBOProperty(Me.moSalesRepNumberText, .SalesRepNumber)
                Me.PopulateControlFromBOProperty(Me.moDealerItemText, .DealerItem)
                Me.PopulateControlFromBOProperty(Me.moSalesPriceText, .SalesPrice)
                Me.PopulateControlFromBOProperty(Me.moProductCodeText, .ProductCode)
                Me.PopulateControlFromBOProperty(Me.moDealerProductCodeText, .DealerProductCode)
                Me.PopulateControlFromBOProperty(Me.moDateAddedText, .DateAdded)
                Me.PopulateControlFromBOProperty(Me.moLastMaintainedTextbox, .LastDateMaintained)
                Me.PopulateControlFromBOProperty(Me.moBillingDateText, .DatePaidFor)

                Me.PopulateControlFromBOProperty(Me.moVehicleLicenseTagText, .VehicleLicenseTag)

                'Added for Req-703 - Start
                Me.PopulateControlFromBOProperty(Me.moCapSeriesText, .CapitalizationSeries)
                Me.PopulateControlFromBOProperty(Me.moCapNumberText, .CapitalizationNumber)
                'Added for Req-703 - End               

                Me.PopulateControlFromBOProperty(Me.moDatePaidText, .DatePaid)

                Me.PopulateControlFromBOProperty(Me.moDocumentIssueDateText, .DocumentIssueDate)

                PopulateDocumentTypeDropdown(cboDocumentTypeId)
                SetSelectedItem(cboDocumentTypeId, .DocumentTypeID)

                PopulateMembershipTypeDropdown(cboAccountType)
                SetSelectedItem(cboAccountType, .MembershipTypeId)

                Me.PopulateControlFromBOProperty(Me.moDocumentTypeText, .getDocTypeDesc)

                Me.PopulateControlFromBOProperty(Me.moRGNumberText, .RgNumber)
                Me.PopulateControlFromBOProperty(Me.moDocumentAgencyText, .DocumentAgency)
                Me.PopulateControlFromBOProperty(Me.moIDTypeText, .IdType)
                Me.PopulateControlFromBOProperty(Me.moNewTaxIdText, .TaxIDNumb)
                Me.PopulateControlFromBOProperty(Me.TextboxRatingPlan, .RatingPlan)
                Me.PopulateControlFromBOProperty(Me.txtSource, .Source)
                Me.PopulateControlFromBOProperty(Me.moServiceLineNumberText, .ServiceLineNumber)
                Me.PopulateControlFromBOProperty(Me.moRegionText, .Region)

                If Not .SubStatusChangeDate Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.moSubStatusChangeDateText, .SubStatusChangeDate)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moSubStatusChangeDateLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.moSubStatusChangeDateText, False)
                End If

                If Not .LinesOnAccount Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.moLinesOnAccountText, .LinesOnAccount)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moLinesOnAccountLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.moLinesOnAccountText, False)
                End If

                If Not .BillingPlan Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.moBillingPlanText, .BillingPlan)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moBillingPlanLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.moBillingPlanText, False)
                End If

                If Not .BillingCycle Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.moBillingCycleText, .BillingCycle)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moBillingCycleLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.moBillingCycleText, False)
                End If

                If Not .DocumentIssueDate Is Nothing _
                   Or Not .IdType Is Nothing _
                   Or Not .DocumentAgency Is Nothing _
                   Or Not .RgNumber Is Nothing _
                   Or Not .DocumentTypeID.Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, Me.moTaxIdLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.moTaxIdText, False)
                End If

                If Me.State.isSalutation Then
                    Me.SetSelectedItem(Me.cboSalutationId, .SalutationId)
                    Me.PopulateControlFromBOProperty(Me.moSalutationText, .getSalutationDescription)
                End If

                If Me.State.isPostPrePaid Then
                    Me.PopulateControlFromBOProperty(Me.moPostPaidText, .getPostPrePaidDesc)
                End If

                If Me.State.TheItemCoverageState.IsRetailer Then
                    Me.PopulateControlFromBOProperty(Me.moRetailerText, Me.State.TheItemCoverageState.dealerName)
                Else
                    Me.PopulateControlFromBOProperty(Me.moRetailerText, .Retailer)
                End If

                If (.DatePaid Is Nothing) AndAlso (.DatePaidFor Is Nothing) Then
                    ControlMgr.SetVisibleControl(Me, moBillingDateText, False)
                    ControlMgr.SetVisibleControl(Me, moBillingDateLabel, False)
                    Me.tdBillingDate.InnerHtml = ""
                    ControlMgr.SetVisibleControl(Me, moDatePaidText, False)
                    ControlMgr.SetVisibleControl(Me, moDatePaidLabel, False)
                    Me.tdDatePaid.InnerHtml = ""
                End If

                If ((.Source Is Nothing) Or (Not .Source.ToUpper().Equals("VSC"))) Then 'Or (.VehicleLicenseTag Is Nothing Or .VehicleLicenseTag = "")) Then
                    ControlMgr.SetVisibleControl(Me, moVehicleLicenseTagText, False)
                    ControlMgr.SetVisibleControl(Me, moVehicleLicenseTagLabel, False)
                    Me.tdVehicleLicenseTag.InnerHtml = ""
                End If

                If Not blnPremiumEdit Then PopulatePaymentTypesDropdown(Me.moPaymentTypeId)

                If Me.State.creditCardPayment Then
                    'Get the credit card types:
                    If Not blnPremiumEdit Then
                        PopulateCreditCardTypesDropdown(Me.moCreditCardTypeIDDropDown)
                    Else
                        'User cannot change the type from credit to bank or vis versa.

                        'Me.State.TheDirectDebitState.CreditCardInfo = Me.State.TheDirectDebitState.certInstallment.AddCreditCardInfo(Guid.Empty)
                    End If

                Else
                    DisableCreditCardInformation()
                End If

                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .GetProdCodeDesc)
                Me.PopulateControlFromBOProperty(Me.moTypeOfEquipmentText, typeOfEquip)
                Me.PopulateControlFromBOProperty(Me.moVATNumText, .VatNum)
                Me.PopulateControlFromBOProperty(Me.moSEPAMandateDateText, .SepaMandateDate)
                Me.PopulateControlFromBOProperty(Me.moCheckVerificationDateText, .CheckVerificationDate)
                Me.PopulateControlFromBOProperty(Me.moCertificateVerificationDateText, .CertificateVerificationDate)
                Me.PopulateControlFromBOProperty(Me.moContractCheckCompleteDateText, .ContractCheckCompleteDate)
                Me.PopulateControlFromBOProperty(Me.moServiceStartDateText, .ServiceStartDate)
                Me.PopulateControlFromBOProperty(Me.moServiceIdText, .ServiceID)

                'REQ-910 New fields BEGIN
                If Me.State.ReqCustomerLegalInfoId.Equals(Guid.Empty) Then
                    Me.State.ReqCustomerLegalInfoId = (New Company(Me.State.MyBO.CompanyId).ReqCustomerLegalInfoId)
                End If

                If Me.State.MyBO.Dealer.DisplayDobXcd Is Nothing Then
                    IsDisplayMaskFlag = False
                Else
                    IsDisplayMaskFlag = Me.State.MyBO.Dealer.DisplayDobXcd.ToUpper().Equals("YESNO-Y")
                End If

                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) Then '0= None
                    Me.moCustLegalInfo1.Attributes("style") = "display: none"
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
                    Me.PopulateControlFromBOProperty(Me.moOccupationText, .Occupation)
                    Me.SetSelectedItem(Me.cboPoliticallyExposedId, .PoliticallyExposedId)
                    Me.SetSelectedItem(Me.cboIncomeRangeId, .IncomeRangeId)
                    Me.PopulateControlFromBOProperty(Me.moPoliticallyExposedText, Me.cboPoliticallyExposedId.SelectedItem.Text.ToString)
                    Me.PopulateControlFromBOProperty(Me.moIncomeRangeText, Me.cboIncomeRangeId.SelectedItem.Text.ToString)
                End If
                'REQ-910 New fields END

                'REQ-1255 - AML Regulations - START
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) Then '0= None
                    Me.moAMLRegulations0.Attributes("style") = "display: none"
                    Me.moAMLRegulations1.Attributes("style") = "display: none"
                    Me.moAMLRegulations2.Attributes("style") = "display: none"
                    Me.moAMLRegulations3.Attributes("style") = "display: none"
                    Me.moAMLRegulations4.Attributes("style") = "display: none"
                Else
                    'populate the dropdowns
                    'Marital Status
                    PopulateMaritalStatusDropdown(ddlMaritalStatus)
                    Me.SetSelectedItem(Me.ddlMaritalStatus, .MaritalStatus)
                    Me.PopulateControlFromBOProperty(Me.moMaritalStatusText, Me.ddlMaritalStatus.SelectedItem.Text) '.MaritalStatus
                    'Nationality
                    PopulateNationalityDropdown(ddlNationality)
                    Me.SetSelectedItem(Me.ddlNationality, .Nationality)
                    Me.PopulateControlFromBOProperty(Me.moNationalityText, Me.ddlNationality.SelectedItem.Text) '.Nationality
                    'PlaceOfBirth
                    PopulatePlaceOfBirthDropdown(ddlPlaceOfBirth)
                    Me.SetSelectedItem(Me.ddlPlaceOfBirth, .PlaceOfBirth)
                    Me.PopulateControlFromBOProperty(Me.moPlaceOfBirthText, Me.ddlPlaceOfBirth.SelectedItem.Text) '.PlaceOfBirth
                    'CityOfBirth
                    If Not .CustomerId.Equals(Guid.Empty) Then
                        Me.PopulateControlFromBOProperty(Me.moCityOfBirthText, .CityOfBirth)
                    End If
                    'Gender
                    PopulateGenderDropdown(ddlGender)
                        Me.SetSelectedItem(Me.ddlGender, .Gender)
                        Me.PopulateControlFromBOProperty(Me.moGenderText, Me.ddlGender.SelectedItem.Text) '.Gender
                        'CUIT_CUIL
                        Me.PopulateControlFromBOProperty(Me.moCUIT_CUILText, .CUIT_CUIL)
                        'Person Type
                        PopulatePersonTypeDropdown(ddlPersonType)
                        Me.SetSelectedItem(Me.ddlPersonType, .PersonTypeId)
                        Me.PopulateControlFromBOProperty(Me.moPersonTypeText, Me.ddlPersonType.SelectedItem.Text)
                    End If
                    'REQ-1255 - AML Regulations - END
                    If Not .ReinsuranceStatusId.Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusLabel, True)
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusText, True)
                    Me.PopulateControlFromBOProperty(Me.moReinsuranceStatusText, LookupListNew.GetDescriptionFromId(LookupListNew.LK_REINSURANCE_STATUSES, .ReinsuranceStatusId))
                Else
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, moReinsuranceStatusText, False)
                End If
                If (Not .ReinsuranceStatusId.Equals(Guid.Empty)) AndAlso .ReinsuranceStatusId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REINSURANCE_STATUSES, LookupListCache.LK_REINS_STATUS_REJECTED)) Then
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonLabel, True)
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonText, True)
                    Me.PopulateControlFromBOProperty(Me.moReinsRejectReasonText, .ReinsuredRejectReason)
                Else
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonLabel, False)
                    ControlMgr.SetVisibleControl(Me, moReinsRejectReasonText, False)
                End If
                ' REQ 5932

                If Not .CustomerId.Equals(Guid.Empty) Then
                    Me.PopulateControlFromBOProperty(Me.moCustomerFirstNameText, .CustomerFirstName)
                    Me.PopulateControlFromBOProperty(Me.moCustomerMiddleNameText, .CustomerMiddleName)
                    Me.PopulateControlFromBOProperty(Me.moCustomerLastNameText, .CustomerLastName)
                    Me.PopulateControlFromBOProperty(Me.moAlternativeLastNameText, .AlternativeLastName)
                    Me.PopulateControlFromBOProperty(Me.moAlternativeFirstNameText, .AlternativeFirstName)
                    Me.PopulateControlFromBOProperty(Me.moCorporateNameText, .CorporateName)
                End If
                Me.moCertificateSigneddrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                'Me.SetSelectedItem(Me.moCertificateSigneddrop, .CertificateSigned)
                BindSelectItem(.CertificateSigned, Me.moCertificateSigneddrop)
                Me.moSEPAMandateSignedDrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.SepaMandateSigned, Me.moSEPAMandateSignedDrop)
                Me.moCheckSignedDrop.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                'Check Signed Drop Down will not have Invalid Signature and Filled By Hand values.
                Dim listItem1 As WebControls.ListItem = moCheckSignedDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("DOCUMENT_STATUS", "IS"))
                Me.moCheckSignedDrop.Items.Remove(listItem1)
                Dim listItem2 As WebControls.ListItem = moCheckSignedDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("DOCUMENT_STATUS", "FH"))
                Me.moCheckSignedDrop.Items.Remove(listItem2)
                BindSelectItem(.CheckSigned, Me.moCheckSignedDrop)

                Me.moContractCheckCompleteDrop.PopulateOld("CONTRACT_CHECK_COMPLETE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.ContractCheckComplete, Me.moContractCheckCompleteDrop)

                Me.PopulateControlFromBOProperty(Me.txtBillingDocType, .BillingDocumentType)
                Me.PopulateControlFromBOProperty(Me.txtDealerUpdateReason, .DealerUpdateReason)
                Me.PopulateControlFromBOProperty(Me.moInsuranceOrderNumberText, .InsuranceOrderNumber)
                Me.PopulateControlFromBOProperty(Me.moDeviceOrderNumberText, .DeviceOrderNumber)
                Me.PopulateControlFromBOProperty(Me.moUpgradeTypeText, .UpgradeType)
                Me.PopulateControlFromBOProperty(Me.moPlanTypeText, .PlanType)

                Me.moFulfillmentConsentActionDrop.PopulateOld("CONSENT_ACTION", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                BindSelectItem(.FulfillmentConsentAction, Me.moFulfillmentConsentActionDrop)

                Me.PopulateControlFromBOProperty(Me.moClaimWaitingPeriodEndDateText, .WaitingPeriodEndDate)

                If Not Me.State.MyBO.PreviousCertificateId.Equals(Guid.Empty) Then

                    Me.State.PreviousCertificate = New Certificate(Me.State.MyBO.PreviousCertificateId)
                    linkPrevCertId.Text = Me.State.PreviousCertificate.CertNumber

                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, True)
                    'ControlMgr.SetVisibleControl(Me, linkPrevCertId, True)
                Else
                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, False)
                    'ControlMgr.SetVisibleControl(Me, linkPrevCertId, False)
                End If

                If Not Me.State.MyBO.OriginalCertificateId.Equals(Guid.Empty) Then

                    Me.State.OriginalCertificate = New Certificate(Me.State.MyBO.OriginalCertificateId)
                    linkOrigCertId.Text = Me.State.OriginalCertificate.CertNumber

                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, True)
                    'ControlMgr.SetVisibleControl(Me, linkOrigCertId, True)
                Else
                    ControlMgr.SetVisibleControl(Me, moCertificatesLinkPanel, False)
                    'ControlMgr.SetVisibleControl(Me, linkOrigCertId, False)
                End If

                Me.PopulateControlFromBOProperty(Me.moDateOfBirthText, .DateOfBirth)
                DisplayMaskDob()

            End With
        End Sub
        Protected Sub DisplayMaskDob()
            Dim IsDobDisplay As Boolean
            If Me.State.MyBO.Dealer.DisplayDobXcd Is Nothing Then
                IsDobDisplay = False
            Else
                IsDobDisplay = Me.State.MyBO.Dealer.DisplayDobXcd.ToUpper().Equals("YESNO-Y")
            End If

            Dim IsCustomerLglInfo As Boolean = Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0"))

            If (Not IsDobDisplay And IsCustomerLglInfo) Then
                ControlMgr.SetVisibleControl(Me, moDateOfBirthLabel, False)
                ControlMgr.SetVisibleControl(Me, moDateOfBirthText, False)
                ControlMgr.SetVisibleControl(Me, BtnDateOfBirth, False)
                ControlMgr.SetVisibleControl(Me, tdDateOfBirthCalTag, False)
            End If

            If (Not IsDobDisplay And Not IsCustomerLglInfo) Then
                If Not (Me.State.IsEdit) Then
                    Me.moDateOfBirthText.Text = Me.State.MyBO.MaskDatePart(Me.moDateOfBirthText.Text, True)
                End If
            End If

            If (IsDobDisplay) Then
                If Not (Me.State.IsEdit) Then
                    Me.moDateOfBirthText.Text = Me.State.MyBO.MaskDatePart(Me.moDateOfBirthText.Text, False)
                End If
            End If
        End Sub

        Protected Sub PopulateCanceDueDateFromForm()
            '  If txtFutureCancelationDateEdit.Text.Trim = String.Empty Then
            Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "CancellationDueDate", String.Empty)
            '  End If
        End Sub

        Protected Sub PopulateInstalmenBoFromForm()
            If Not Me.State.MyBO.PaymentTypeId.Equals(Guid.Empty) Then


                Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
                Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

                Me.State.BillingStatusId = Me.State.TheDirectDebitState.certInstallment.BillingStatusId

                If HasInstallment Then
                    Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "BillingStatusId", Me.moBillingStatusId)

                    If CheckBoxSendLetter.Checked = True Then
                        Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "SendLetterId", yesId)
                    Else
                        Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "SendLetterId", noId)
                    End If

                    Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "DateLetterSent", Me.moDateLetterSentText)
                    Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "PaymentDueDate", Me.moNextDueDateText)
                    'Req - 1016 Start
                    Me.PopulateBOProperty(Me.State.TheDirectDebitState.certInstallment, "NextBillingDate", Me.moNextBillingDateText)
                    'Req - 1016 End

                    'If (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS _
                    '    And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) Then
                    If Me.State.directDebitPayment Then
                        If Me.IfDirectDebitInfoChanged() Then
                            Me.State.BillingInformationChanged = True
                            Me.State.BankInfoBO = New BankInfo
                            Me.State.BankInfoBO.CopyFrom(Me.State.TheDirectDebitState.bankInfo)
                            Me.State.TheDirectDebitState.bankInfo = Me.State.TheDirectDebitState.certInstallment.AddBankInfo(Guid.Empty)
                            Me.State.TheDirectDebitState.bankInfo.CountryID = Me.State.BankInfoBO.CountryID
                            Me.State.TheDirectDebitState.bankInfo.SourceCountryID = Me.State.BankInfoBO.SourceCountryID
                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.bankInfo, "Account_Name", Me.moBankAccountOwnerText)
                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.bankInfo, "Bank_Id", Me.moBankRoutingNumberText)
                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.bankInfo, "Account_Number", Me.moBankAccountNumberText)
                        End If

                    ElseIf Me.State.creditCardPayment Then
                        If Me.IfCreditCardInfoChanged() Then
                            Me.State.BillingInformationChanged = True
                            If State.CreditCardInfoBO Is Nothing Then
                                Me.State.CreditCardInfoBO = New CreditCardInfo
                                Me.State.CreditCardInfoBO.CopyFrom(Me.State.TheDirectDebitState.CreditCardInfo)
                                Me.State.TheDirectDebitState.CreditCardInfo = Me.State.TheDirectDebitState.certInstallment.AddCreditCardInfo(Guid.Empty)
                            End If

                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.CreditCardInfo, "ExpirationDate", Me.moExpirationDateText)
                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.CreditCardInfo, "NameOnCreditCard", Me.moNameOnCreditCardText)
                            Me.PopulateBOProperty(Me.State.TheDirectDebitState.CreditCardInfo, "CreditCardFormatId", Me.moCreditCardTypeIDDropDown)

                            If Me.moCreditCardNumberText.Text = "****" & Me.State.CreditCardInfoBO.Last4Digits Then
                                Me.State.TheDirectDebitState.CreditCardInfo.CreditCardNumber = Me.State.CreditCardInfoBO.CreditCardNumber
                                Me.State.TheDirectDebitState.CreditCardInfo.Last4Digits = Me.State.CreditCardInfoBO.Last4Digits
                            Else
                                CreditCardValidation(moCreditCardNumberText.Text, State.TheDirectDebitState.CreditCardInfo.CreditCardFormatId)
                                Me.PopulateBOProperty(Me.State.TheDirectDebitState.CreditCardInfo, "CreditCardNumber", Me.moCreditCardNumberText)
                                Dim strCC As String = Me.State.TheDirectDebitState.CreditCardInfo.CreditCardNumber.Trim
                                Me.State.TheDirectDebitState.CreditCardInfo.Last4Digits = strCC.Substring(strCC.Length - 4, 4)
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
                    If Me.State.TheDirectDebitState.certInstallment.PaymentDueDate.Value.Date < Me.State.MyBO.WarrantySalesDate.Value.Date Then
                        Throw New GUIException(Message.PAYMENT_DUE_DATE_LESS_THAN_WSD, Assurant.ElitaPlus.Common.ErrorCodes.PAYMENT_DUE_DATE_LESS_THAN_WSD)
                    End If

                    If State.TheDirectDebitState.certInstallment.NextBillingDate Is Nothing OrElse State.TheDirectDebitState.certInstallment.NextBillingDate.Value.Date < Me.State.MyBO.WarrantySalesDate.Value.Date Then
                        Throw New GUIException(Message.NEXT_BILLING_DATE_LESS_THAN_WSD, Assurant.ElitaPlus.Common.ErrorCodes.NEXT_BILLING_DATE_LESS_THAN_WSD)
                    End If

                End If
            End If

        End Sub
        Private Sub CreditCardValidation(ByVal CreditCardNumber As String, ByVal CreditCardFormatId As Guid)
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
            With Me.State.TheDirectDebitState.bankInfo
                If .Account_Name = Me.moBankAccountOwnerText.Text AndAlso .Bank_Id = Me.moBankRoutingNumberText.Text AndAlso .Account_Number = Me.moBankAccountNumberText.Text Then
                    Return False
                Else
                    Return True
                End If
            End With

        End Function
        Private Function IfCreditCardInfoChanged() As Boolean
            With Me.State.TheDirectDebitState.CreditCardInfo
                If .ExpirationDate = Me.moExpirationDateText.Text _
                   AndAlso .NameOnCreditCard = Me.moNameOnCreditCardText.Text _
                   AndAlso .CreditCardFormatId.Equals(GetSelectedItem(Me.moCreditCardTypeIDDropDown)) _
                   AndAlso "****" & .Last4Digits = Me.moCreditCardNumberText.Text Then
                    Return False
                Else
                    Return True
                End If
            End With
        End Function
        Protected Sub PopulateBOsFromForm()
            With Me.State.MyBO
                ' ValFlag()
                Dim blnUpdateZipLocator = Me.State.MyBO.Company.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.COMP_ATTR__UPD_ZIP_LOCATOR).FirstOrDefault

                If blnUpdateZipLocator Is Nothing Then
                    blnUpdateZipLocator = False
                Else
                    blnUpdateZipLocator = IIf(blnUpdateZipLocator.Value = Codes.YESNO_Y, True, False)
                End If

                Me.AddressCtr.PopulateBOFromControl(True, blnUpdateZipLocator)
                If (Not Me.AddressCtr.MyBO Is Nothing AndAlso
                    (Me.AddressCtr.MyBO.IsDeleted = False) AndAlso
                    (Me.AddressCtr.MyBO.IsEmpty = False)) Then
                    Me.State.MyBO.AddressId = Me.AddressCtr.MyBO.Id
                End If

                Me.PopulateBOProperty(Me.State.MyBO, "ServiceStartDate", Me.moServiceStartDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceID", Me.moServiceIdText)
                Me.PopulateBOProperty(Me.State.MyBO, "DocumentIssueDate", Me.moDocumentIssueDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "DocumentTypeID", cboDocumentTypeId)
                Me.PopulateBOProperty(Me.State.MyBO, "RgNumber", Me.moRGNumberText)
                Me.PopulateBOProperty(Me.State.MyBO, "DocumentAgency", Me.moDocumentAgencyText)
                Me.PopulateBOProperty(Me.State.MyBO, "IdType", Me.moIDTypeText)

                If Me.State.selectedTab = CERT_TAX_ID_TAB Then
                    Me.PopulateBOProperty(Me.State.MyBO, "TaxIDNumb", Me.moNewTaxIdText)
                End If

                Me.PopulateBOProperty(Me.State.MyBO, "CustomerName", Me.moCustomerNameText)
                Me.PopulateBOProperty(Me.State.MyBO, "HomePhone", Me.moHomePhoneText)
                Me.PopulateBOProperty(Me.State.MyBO, "WorkPhone", Me.moWorkPhoneText)
                If Not Me.moEmailAddressText.Text Is Nothing AndAlso Me.moEmailAddressText.Text.Equals(Me.State.EmailIsNull) Then
                    Me.moEmailAddressText.Text = ""
                End If
                Me.PopulateBOProperty(Me.State.MyBO, "Email", Me.moEmailAddressText)
                If Me.State.selectedTab = CERT_DETAIL_TAB Then
                    Me.PopulateBOProperty(Me.State.MyBO, "IdentificationNumber", Me.moTaxIdText)
                End If
                Me.PopulateBOProperty(Me.State.MyBO, "InvoiceNumber", Me.moInvoiceNumberText)
                Me.PopulateBOProperty(Me.State.MyBO, "VehicleLicenseTag", Me.moVehicleLicenseTagText)
                Me.PopulateBOProperty(Me.State.MyBO, "LanguageId", Me.cboLangPref)

                If Me.State.isSalutation Then
                    Me.PopulateBOProperty(Me.State.MyBO, "SalutationId", Me.cboSalutationId)
                End If

                If Me.State.TheItemCoverageState.manufaturerWarranty Then
                    Me.PopulateBOProperty(Me.State.MyBO, "WarrantySalesDate", Me.moWarrantySoldText)
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "ProductSalesDate", Me.moProductSalesDateText)
                End If

                Me.PopulateBOProperty(Me.State.MyBO, "SalesRepNumber", Me.moSalesRepNumberText)

                Me.PopulateBOProperty(Me.State.MyBO, "MembershipTypeId", Me.cboAccountType)

                Me.PopulateBOProperty(Me.State.MyBO, "MembershipNumber", Me.moAccountNumberText)

                If Not Me.State.TheItemCoverageState.IsRetailer Then
                    Me.PopulateBOProperty(Me.State.MyBO, "Retailer", Me.moRetailerText)
                End If

                'DEF-21659 - START
                'If Not .DateOfBirth Is Nothing Then
                If (Me.State.IsEdit) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "DateOfBirth", Me.moDateOfBirthText)
                End If

                'End If
                'DEF-21659 - END

                'Me.PopulateBOProperty(Me.State.MyBO, "PaymentTypeId", Me.moPaymentTypeId)
                Me.PopulateBOProperty(Me.State.MyBO, "VatNum", Me.moVATNumText)
                Me.PopulateBOProperty(Me.State.MyBO, "Region", Me.moRegionText)

                'AA REQ-910 new fields added BEGIN
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment
                    Me.PopulateBOProperty(Me.State.MyBO, "IncomeRangeId", Me.cboIncomeRangeId)
                    Me.PopulateBOProperty(Me.State.MyBO, "Occupation", Me.moOccupationText)
                    Me.PopulateBOProperty(Me.State.MyBO, "PoliticallyExposedId", Me.cboPoliticallyExposedId)
                End If
                'AA REQ-910 new fields added END
                'REQ-1255 - AML Regulations - START
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3")) Then  ' 1= Display and Require When Cancelling or 2= Display Only or 3= Display and Require At Enrollment  
                    Me.PopulateBOProperty(Me.State.MyBO, "MaritalStatus", Me.ddlMaritalStatus)
                    Me.PopulateBOProperty(Me.State.MyBO, "Nationality", Me.ddlNationality)
                    Me.PopulateBOProperty(Me.State.MyBO, "PlaceOfBirth", Me.ddlPlaceOfBirth)
                    If Not .CustomerId.Equals(Guid.Empty) Then
                        Me.PopulateBOProperty(Me.State.MyBO, "CityOfBirth", Me.moCityOfBirthText)
                    End If
                    Me.PopulateBOProperty(Me.State.MyBO, "Gender", Me.ddlGender)
                        Me.PopulateBOProperty(Me.State.MyBO, "PersonTypeId", Me.ddlPersonType)
                        Me.PopulateBOProperty(Me.State.MyBO, "CUIT_CUIL", Me.moCUIT_CUILText)
                    End If
                    'REQ-1255 - AML Regulations - END
                    If Not .CustomerId.Equals(Guid.Empty) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "CustomerFirstName", Me.moCustomerFirstNameText)
                    Me.PopulateBOProperty(Me.State.MyBO, "CustomerMiddleName", Me.moCustomerMiddleNameText)
                    Me.PopulateBOProperty(Me.State.MyBO, "CustomerLastName", Me.moCustomerLastNameText)
                    Me.PopulateBOProperty(Me.State.MyBO, "AlternativeLastName", Me.moAlternativeLastNameText)
                    Me.PopulateBOProperty(Me.State.MyBO, "AlternativeFirstName", Me.moAlternativeFirstNameText)
                    Me.PopulateBOProperty(Me.State.MyBO, "CorporateName", Me.moCorporateNameText)
                End If
            End With
            If Me.State.MyBO.Dealer.ContractManualVerification = "YESNO-Y" Then
                Me.PopulateBOProperty(Me.State.MyBO, "CertificateSigned", Me.moCertificateSigneddrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "SepaMandateSigned", Me.moSEPAMandateSignedDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "ContractCheckCompleteDate", Me.moContractCheckCompleteDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "ContractCheckComplete", Me.moContractCheckCompleteDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "CertificateVerificationDate", Me.moCertificateVerificationDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "SepaMandateDate", Me.moSEPAMandateDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "CheckSigned", Me.moCheckSignedDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "CheckVerificationDate", Me.moCheckVerificationDateText)
            End If

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            Me.State.MyBO.ValFlag = Me.State.ValFlag

        End Sub

        Protected Sub PopulateBankInfoBOFromUserCtr()
            With Me.State.BankInfoBO
                Me.moBankInfoController.PopulateBOFromControl()
            End With
        End Sub
        Protected Sub PopulatePaymentOrderInfoBOFromUserCtr()
            With Me.State.PaymentOrderInfoBO
                Me.moPaymentOrderInfoCtrl.PopulateBOFromControl()
            End With
        End Sub

        Public Sub PupulateCommEntityGrid()
            Dim dv As DataView
            dv = Me.State.MyBO.GetCommissionForEntities(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Date.Today.ToString(SP_DATE_FORMAT))
            If dv.Count = 0 Then
                Me.moCommEntityInformationLine.Attributes("style") = "display: none"
            End If
            Me.moCommEntityGrid.DataSource = dv
            Me.moCommEntityGrid.DataBind()
        End Sub

        Public Sub PopulatePremiumInfoTab()

            Dim dv As DataView
            dv = Me.State.MyBO.PremiumTotals(Me.State.MyBO.Id)

            If dv.Count = 0 Then
                Me.State.isPremiumTAbVisible = False
                Exit Sub
            End If

            Me.PopulateControlFromBOProperty(Me.TextboxCURRENCY_OF_CERT, LookupListNew.GetDescriptionFromId(LookupListNew.LK_CURRENCIES, Me.State.MyBO.CurrencyOfCertId))

            ' Gross Amount received
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED) Then
                Me.PopulateControlFromBOProperty(Me.moGrossAmtReceivedText, 0, Me.DECIMAL_FORMAT)
                Me.State.grossAmtReceived = 0
            Else
                Me.PopulateControlFromBOProperty(Me.moGrossAmtReceivedText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal), Me.DECIMAL_FORMAT)
                Me.State.grossAmtReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)
            End If

            ' Total Premium Written
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_PREMIUM_WRITTEN) Then
                Me.PopulateControlFromBOProperty(Me.moPremiumWrittenText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moPremiumWrittenText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_PREMIUM_WRITTEN), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Original Premium
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_ORIGINAL_PREMIUM) Then
                Me.PopulateControlFromBOProperty(Me.moOriginalPremiumText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moOriginalPremiumText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_ORIGINAL_PREMIUM), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Loss Cost
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_LOSS_COST) Then
                Me.PopulateControlFromBOProperty(Me.moLossCostText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moLossCostText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_LOSS_COST), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Commission
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_COMISSION) Then
                Me.PopulateControlFromBOProperty(Me.moComissionsText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moComissionsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_COMISSION), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Admin expense
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_ADMIN_EXPENSE) Then
                Me.PopulateControlFromBOProperty(Me.moAdminExpensesText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moAdminExpensesText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_ADMIN_EXPENSE), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Marketing Expense
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MARKETING_EXPENSE) Then
                Me.PopulateControlFromBOProperty(Me.moMarketingExpenseText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moMarketingExpenseText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MARKETING_EXPENSE), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Other
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_OTHER) Then
                Me.PopulateControlFromBOProperty(Me.moOtherText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moOtherText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_OTHER), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Sales Tax
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_SALES_TAX) Then
                Me.PopulateControlFromBOProperty(Me.moSalesTaxText, 0, Me.DECIMAL_FORMAT)

            Else
                Me.PopulateControlFromBOProperty(Me.moSalesTaxText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_SALES_TAX), Decimal), Me.DECIMAL_FORMAT)

            End If

            ' Total MTD Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MTD_PAYMENTS) Then
                Me.PopulateControlFromBOProperty(Me.moMTDPaymentsText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moMTDPaymentsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MTD_PAYMENTS), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total YTD Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_YTD_PAYMENTS) Then
                Me.PopulateControlFromBOProperty(Me.moYTDPaymentsText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moYTDPaymentsText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_YTD_PAYMENTS), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total MTD Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_MTD_INCOMING_PAYMENTS) Then
                Me.PopulateControlFromBOProperty(Me.moMTDPaymentFromCustText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moMTDPaymentFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_MTD_INCOMING_PAYMENTS), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total YTD Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_YTD_INCOMING_PAYMENTS) Then
                Me.PopulateControlFromBOProperty(Me.moYTDPaymentFromCustText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moYTDPaymentFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_YTD_INCOMING_PAYMENTS), Decimal), Me.DECIMAL_FORMAT)
            End If

            ' Total Incoming Payments
            If dv.Table.Rows(0).IsNull(Certificate.COL_TOTAL_INCOMING_PAYMENTS) Then
                Me.PopulateControlFromBOProperty(Me.moPaymentRcvdFromCustText, 0, Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moPaymentRcvdFromCustText, CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_INCOMING_PAYMENTS), Decimal), Me.DECIMAL_FORMAT)
            End If

            Try
                Me.State.TheDirectDebitState.certInstallment = New CertInstallment(Me.State.MyBO.Id, True)
            Catch ex As Exception
                Me.State.TheDirectDebitState.certInstallment = Nothing
            End Try

            If Not Me.State.MyBO.PaymentTypeId.Equals(Guid.Empty) Then
                If HasInstallment AndAlso Not (State.TheDirectDebitState.certInstallment.CreditCardInfoId = Guid.Empty) Then
                    Me.State.creditCardPayment = True
                ElseIf HasInstallment AndAlso Not (State.TheDirectDebitState.certInstallment.BankInfoId = Guid.Empty) Then
                    Me.State.directDebitPayment = True
                ElseIf (Me.State.MyBO.getCollectionMethodCode = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS And Me.State.MyBO.getPaymentInstrumentCode = Codes.PAYMENT_INSTRUMENT__DEALER_BILL) Then
                    Me.State.dealerBillPayment = True
                End If
            End If

            Me.SetSelectedItem(Me.moPaymentTypeId, Me.State.MyBO.PaymentTypeId)

            If Me.State.IsNewBillPayBtnVisible Then
                dv = CollectedPayDetail.getCollectPayTotals(Me.State.MyBO.Id)
                If dv.Count > 0 Then
                    Me.State.oPayColltCount = CType(dv.Table.Rows(0).Item(CollectedPayDetail.CollectPayTotals.COL_DETAIL_COUNT), Integer)
                    If Me.State.oPayColltCount > 0 Then
                        ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                    Else
                        If (Me.State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetEnableControl(Me, btnBillPayHist, False)
                        End If
                    End If
                End If
            Else
                dv = PaymentDetail.getPaymentTotals(Me.State.MyBO.Id)
                If dv.Count > 0 Then
                    Me.State.oPaymentCount = CType(dv.Table.Rows(0).Item(PaymentDetail.PaymentTotals.COL_DETAIL_COUNT), Integer)
                    If Me.State.oPaymentCount > 0 Then
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, True)
                    Else
                        ControlMgr.SetEnableControl(Me, btnPaymentHistory, False)
                    End If
                End If
            End If



            If HasInstallment Then
                PopulateBillingStatusDropdown()

                Try
                    If Me.State.directDebitPayment Then Me.State.TheDirectDebitState.bankInfo = Me.State.TheDirectDebitState.certInstallment.BankInfo
                    If Me.State.creditCardPayment Then Me.State.TheDirectDebitState.CreditCardInfo = Me.State.TheDirectDebitState.certInstallment.CreditCardInfo

                    Me.PopulateControlFromBOProperty(Me.moBillingStatusId, Me.State.TheDirectDebitState.certInstallment.BillingStatusId)


                    If Me.State.IsNewBillPayBtnVisible Then

                        Me.State.oBillingTotalAmount = 0
                        'dv = BillingPayDetail.getBillPayTotals(Me.State.MyBO.Id)
                        dv = BillingPayDetail.getBillPayTotalsNew(Me.State.MyBO.Id)

                        If Not dv Is Nothing AndAlso Not dv.Table Is Nothing AndAlso dv.Table.Rows.Count > 0 AndAlso Not dv.Table.Rows(0)(installments_Collected) Is Nothing AndAlso Not dv.Table.Rows(0)(installments_Collected).Equals(System.DBNull.Value) Then
                            Me.State.oBillPayCount = dv.Table.Rows(0)(installments_Collected)
                        Else
                            Me.State.oBillPayCount = 0
                        End If

                        If Not dv Is Nothing AndAlso Not dv.Table Is Nothing AndAlso dv.Table.Rows.Count > 0 AndAlso Not dv.Table.Rows(0)(Amount_Collected) Is Nothing AndAlso Not dv.Table.Rows(0)(Amount_Collected).Equals(System.DBNull.Value) Then
                            Me.State.oBillingTotalAmount = dv.Table.Rows(0)(Amount_Collected)
                        Else
                            Me.State.oBillingTotalAmount = 0
                        End If
                        Me.PopulateControlFromBOProperty(Me.moBalanceRemainingText, dv.Table.Rows(0)(remaining_amount))
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentText, dv.Table.Rows(0)(Total_installments)) 'remaining_amount
                        Me.moNumberOfInstallmentText.Text = String.Empty
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentText, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentCollectedLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentCollectedText, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentRemainingLabel, False)
                        ControlMgr.SetVisibleControl(Me, moNumberOfInstallmentRemainingLText, False)


                    Else
                        Me.State.oBillingTotalAmount = 0
                        dv = BillingDetail.getBillingTotals(Me.State.MyBO.Id)
                        Me.State.oBillingCount = 0

                        If (Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_DETAIL_COUNT)) AndAlso CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer) > 0 Then
                            If Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL) Then
                                Me.State.oBillingTotalAmount = CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                            Else
                                Me.State.oBillingTotalAmount = 0
                            End If
                            If Me.State.TheDirectDebitState.certInstallment.InstallmentAmount.Value = 0 Then
                                If Not dv.Table.Rows(0).IsNull(BillingDetail.BillingTotals.COL_DETAIL_COUNT) Then
                                    Me.State.oBillingCount = CType(dv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer)
                                Else
                                    Me.State.oBillingCount = 0
                                End If
                            Else
                                Me.State.oBillingCount = CType(Decimal.Round(Decimal.Divide(Me.State.oBillingTotalAmount, Me.State.TheDirectDebitState.certInstallment.InstallmentAmount.Value)), Integer)
                            End If
                        Else
                            ControlMgr.SetEnableControl(Me, btnDebitHistory, False)
                        End If
                        Me.PopulateControlFromBOProperty(Me.moBalanceRemainingText, Me.State.grossAmtReceived - Me.State.oBillingTotalAmount)
                        Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentText, CType(Me.State.TheDirectDebitState.certInstallment.NumberOfInstallments, Integer))
                        Me.moNumberOfInstallmentRemainingLabel.Text = Me.moNumberOfInstallmentRemainingLabel.Text & ":"
                        Me.moNumberOfInstallmentLabel.Text = Me.moNumberOfInstallmentLabel.Text & ":"
                        Me.moNumberOfInstallmentCollectedLabel.Text = Me.moNumberOfInstallmentCollectedLabel.Text & ":"

                    End If

                    If Not Me.State.TheDirectDebitState.certInstallment.CancellationDueDate Is Nothing Then
                        If Not Me.State.TheDirectDebitState.certInstallment.CancellationDueDate.Value < Date.Today Then
                            ControlMgr.SetVisibleControl(Me, Me.lblFutureCancelationDate, True)
                            ControlMgr.SetVisibleControl(Me, Me.txtFutureCancelationDate, True)
                            ControlMgr.SetVisibleControl(Me, Me.btnRemoveCancelDueDate_WRITE, True)
                            If Not Me.lblFutureCancelationDate.Text.EndsWith(":") Then
                                Me.lblFutureCancelationDate.Text &= ":"
                            End If
                            Me.PopulateControlFromBOProperty(Me.txtFutureCancelationDate, Me.State.TheDirectDebitState.certInstallment.CancellationDueDate)
                        End If
                    Else
                        ControlMgr.SetVisibleControl(Me, Me.lblFutureCancelationDate, False)
                        ControlMgr.SetVisibleControl(Me, Me.txtFutureCancelationDate, False)
                        ControlMgr.SetVisibleControl(Me, Me.btnRemoveCancelDueDate_WRITE, False)
                    End If





                    Me.PopulateControlFromBOProperty(Me.moTotalAmountCollectedText, Me.State.oBillingTotalAmount)

                    'REQ-5761 Defect 29135
                    If Me.State.IsNewBillPayBtnVisible Then
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentCollectedText, Me.State.oBillPayCount)
                        Me.moNumberOfInstallmentCollectedText.Text = String.Empty
                        'Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentRemainingLText, dv.Table.Rows(0)(Remaining_Installments))
                        Me.moNumberOfInstallmentRemainingLText.Text = String.Empty 'CType(Me.moNumberOfInstallmentRemainingLText.Text, Integer)
                    Else
                        Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentCollectedText, Me.State.oBillingCount)
                        Me.PopulateControlFromBOProperty(Me.moNumberOfInstallmentRemainingLText, Me.State.TheDirectDebitState.certInstallment.NumberOfInstallments.Value - Me.State.oBillingCount)

                    End If

                    Me.PopulateControlFromBOProperty(Me.moInstallAmountText, Me.State.TheDirectDebitState.certInstallment.InstallmentAmount)
                    Me.PopulateControlFromBOProperty(Me.moNextDueDateText, Me.State.TheDirectDebitState.certInstallment.PaymentDueDate)
                    'Req - 1016 Start
                    Me.PopulateControlFromBOProperty(Me.moNextBillingDateText, Me.State.TheDirectDebitState.certInstallment.NextBillingDate)
                    'Req - 1016 End
                    Me.PopulateControlFromBOProperty(Me.moDateLetterSentText, Me.State.TheDirectDebitState.certInstallment.DateLetterSent)

                    If Me.State.directDebitPayment Then
                        Me.PopulateControlFromBOProperty(Me.moBankAccountNumberText, Me.State.TheDirectDebitState.bankInfo.Account_Number)
                        Me.PopulateControlFromBOProperty(Me.moBankRoutingNumberText, Me.State.TheDirectDebitState.bankInfo.Bank_Id)
                        Me.PopulateControlFromBOProperty(Me.moBankAccountOwnerText, Me.State.TheDirectDebitState.bankInfo.Account_Name)
                        Me.moCreditCardInformation1.Attributes("style") = "display: none"
                        Me.moCreditCardInformation2.Attributes("style") = "display: none"
                    ElseIf Me.State.creditCardPayment Then
                        Dim strCreditCardNumber As String = "****" & Me.State.TheDirectDebitState.CreditCardInfo.Last4Digits
                        Me.PopulateControlFromBOProperty(Me.moCreditCardNumberText, strCreditCardNumber)
                        Me.SetSelectedItem(Me.moCreditCardTypeIDDropDown, Me.State.TheDirectDebitState.CreditCardInfo.CreditCardFormatId)
                        Me.PopulateControlFromBOProperty(Me.moExpirationDateText, Me.State.TheDirectDebitState.CreditCardInfo.ExpirationDate)
                        Me.PopulateControlFromBOProperty(Me.moNameOnCreditCardText, Me.State.TheDirectDebitState.CreditCardInfo.NameOnCreditCard)
                        Me.moDirectDebitInformation6.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation7.Attributes("style") = "display: none"
                    Else
                        Me.moCreditCardInformation1.Attributes("style") = "display: none"
                        Me.moCreditCardInformation2.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation6.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation7.Attributes("style") = "display: none"
                    End If

                    If Me.State.MyBO.StatusCode = CERT_STATUS Then
                        PupulateCommEntityGrid()
                    Else
                        Me.moCommEntityInformation.Attributes("style") = "display: none"
                        Me.moCommEntityInformationLine.Attributes("style") = "display: none"
                    End If

                    Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

                    CheckBoxSendLetter.Checked = False
                    If Me.State.TheDirectDebitState.certInstallment.SendLetterId = yesId Then
                        CheckBoxSendLetter.Checked = True
                    End If

                Catch ex As ElitaPlusException

                    If ex.GetType Is GetType(DataNotFoundException) Then

                        Me.moCreditCardInformation1.Attributes("style") = "display: none"
                        Me.moCreditCardInformation2.Attributes("style") = "display: none"

                        Me.moDirectDebitInformation1.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation2.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation3.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation4.Attributes("style") = "display: none"
                        'Req-1016 Start
                        Me.moDirectDebitInformation4A.Attributes("style") = "display: none"
                        'Req-1016 End
                        Me.moDirectDebitInformation5.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation5A.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation6.Attributes("style") = "display: none"
                        Me.moDirectDebitInformation7.Attributes("style") = "display: none"

                        Me.moCommEntityInformation.Attributes("style") = "display: none"
                        Me.moCommEntityInformationLine.Attributes("style") = "display: none"

                        ControlMgr.SetVisibleControl(Me, btnDebitSave_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnUndoDebit_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnDebitEdit_WRITE, False)
                        ControlMgr.SetVisibleControl(Me, btnDebitHistory, False)
                        If (Me.State.IsNewBillPayBtnVisible) Then
                            ControlMgr.SetVisibleControl(Me, btnBillPayHist, True)
                        Else
                            ControlMgr.SetVisibleControl(Me, btnBillPayHist, False)
                        End If
                    End If

                End Try

            Else

                Me.moCreditCardInformation1.Attributes("style") = "display: none"
                Me.moCreditCardInformation2.Attributes("style") = "display: none"

                Me.moDirectDebitInformation1.Attributes("style") = "display: none"
                Me.moDirectDebitInformation2.Attributes("style") = "display: none"
                Me.moDirectDebitInformation3.Attributes("style") = "display: none"
                Me.moDirectDebitInformation4.Attributes("style") = "display: none"
                'Req-1016 Start
                Me.moDirectDebitInformation4A.Attributes("style") = "display: none"
                'Req-1016 End
                Me.moDirectDebitInformation5.Attributes("style") = "display: none"
                Me.moDirectDebitInformation5A.Attributes("style") = "display: none"
                Me.moDirectDebitInformation6.Attributes("style") = "display: none"
                Me.moDirectDebitInformation7.Attributes("style") = "display: none"

                Me.moCommEntityInformation.Attributes("style") = "display: none"
                Me.moCommEntityInformationLine.Attributes("style") = "display: none"

                ControlMgr.SetVisibleControl(Me, btnDebitSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnUndoDebit_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnDebitEdit_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnDebitHistory, False)
            End If

            Dim oContract As New Contract
            oContract = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value)

            If oContract Is Nothing Then
                Throw New GUIException(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
            Else
                If (oContract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value).InsPremiumFactor) Is Nothing Then
                    Me.State.InsPremiumFactor = 0
                    Me.moCustPaymentInfo1.Attributes("style") = "display: none"
                    Me.moCustPaymentInfo2.Attributes("style") = "display: none"
                Else
                    Me.State.InsPremiumFactor = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value).InsPremiumFactor.Value
                    If Me.State.InsPremiumFactor <= 0 Then
                        Me.moCustPaymentInfo1.Attributes("style") = "display: none"
                        Me.moCustPaymentInfo2.Attributes("style") = "display: none"
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

            If (Not Me.State.MyBO.TheCertCancellationBO Is Nothing) Then
                With Me.State.MyBO.TheCertCancellationBO
                    Me.PopulateControlFromBOProperty(Me.moCancellationReasonTextbox, .getCancellationReasonDescription)
                    Me.PopulateControlFromBOProperty(Me.moCancellationDateTextbox, .CancellationDate)
                    Me.PopulateControlFromBOProperty(Me.moSourceTextbox, .Source)
                    Me.PopulateControlFromBOProperty(Me.moProcessedDateTextbox, .ProcessedDate)
                    Me.PopulateControlFromBOProperty(Me.moOriginalStateProvinceTextbox, .getRegionDescription)

                    Dim dv As DataView = LookupListNew.GetListItemId(Me.State.MyBO.TheCertCancellationBO.getRefundComputeMethodId, Authentication.LangId)
                    computationMethod = dv.Item(FIRST_ROW).Item(DESCRIPTION).ToString
                    computationMethodCode = dv.Item(FIRST_ROW).Item(CODE).ToString
                    Me.PopulateControlFromBOProperty(Me.moRefundMethodMeaningTextbox, computationMethod)
                    policyCost = .getPolicyCost
                    Me.PopulateControlFromBOProperty(Me.moPolicyCostTextbox, policyCost, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.moComputedRefundTextbox, .ComputedRefund)


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
                            Me.moCostRetainedLabel.Text = Me.moCostRetainedLabel.Text & ":"
                        End If
                        ControlMgr.SetVisibleControl(Me, moCostRetainedLabel, True)
                        ControlMgr.SetVisibleControl(Me, moCostRetainedTextbox, True)
                        costRetained = policyCost - .ComputedRefund.Value
                        Me.PopulateControlFromBOProperty(Me.moCostRetainedTextbox, costRetained)
                    End If
                    Me.PopulateControlFromBOProperty(Me.moRefundAmtTextbox, .RefundAmt)

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
                        Dim dv2 As DataView = LookupListNew.GetListItemId(Me.State.MyBO.TheCertCancellationBO.RefundDestId, Authentication.LangId)
                        issuedTo = dv2.Item(FIRST_ROW).Item(DESCRIPTION).ToString
                        Me.PopulateControlFromBOProperty(Me.moIssuedToTextbox, issuedTo)
                    End If

                    If (Me.State.MyBO.TheCertCancellationBO.PaymentMethodId.Equals(Guid.Empty)) Then
                        ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, False)
                        ControlMgr.SetVisibleControl(Me, moPaymentMethodTextbox, False)
                        ControlMgr.SetVisibleControl(Me, moBankInfoLabel, False)
                        Me.moCancPaymentOrderInfoCtrl.Visible = False
                        Me.moCancBankInfoController.Visible = False
                    Else
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_DESTINATION, Me.State.MyBO.TheCertCancellationBO.RefundDestId) = Codes.REFUND_DESTINATION__CUSTOMER AndAlso Me.State.MyBO.TheCertCancellationBO.RefundAmt.Value > 0 Then
                            If moPaymentMethodLabel.Text.IndexOf(":") > 0 Then
                                moPaymentMethodLabel.Text = moPaymentMethodLabel.Text
                            Else
                                moPaymentMethodLabel.Text = moPaymentMethodLabel.Text & ":"
                            End If
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodTextbox, True)
                            PaymentMethod = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.TheCertCancellationBO.PaymentMethodId)
                            Me.PopulateControlFromBOProperty(Me.moPaymentMethodTextbox, PaymentMethod)
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.TheCertCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                                Me.State.CancBankInfoBO = Me.State.MyBO.TheCertCancellationBO.bankinfo
                                Me.State.CancBankInfoBO.SourceCountryID = Me.State.CancBankInfoBO.CountryID
                                Me.moCancBankInfoController.Bind(Me.State.CancBankInfoBO)
                                Me.moCancBankInfoController.DisableAllFields()
                                Me.moCancBankInfoController.Visible = True
                                Me.moCancPaymentOrderInfoCtrl.Visible = False
                            ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.TheCertCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                                Me.State.CancPaymentOrderInfoBO = Me.State.MyBO.TheCertCancellationBO.PmtOrderinfo(True)
                                Me.moCancPaymentOrderInfoCtrl.Bind(Me.State.CancPaymentOrderInfoBO)
                                Me.moCancPaymentOrderInfoCtrl.DisableAllFields()
                                Me.moCancPaymentOrderInfoCtrl.Visible = True
                                Me.moCancBankInfoController.Visible = False
                            Else
                                Me.moCancPaymentOrderInfoCtrl.Visible = False
                                Me.moCancBankInfoController.Visible = False
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

                    Me.PopulateControlFromBOProperty(Me.moCanGrossAmtReceivedTextbox, .GrossAmtReceived.Value, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.moCanOriginalPremiumTextbox, .OriginalPremium)
                    Me.PopulateControlFromBOProperty(Me.moCanPremiumWrittenTextbox, .PremiumWritten)
                    Me.PopulateControlFromBOProperty(Me.moCanLossCostTextbox, .LossCost)
                    Me.PopulateControlFromBOProperty(Me.moCanComissionsTextbox, .Commission)
                    Me.PopulateControlFromBOProperty(Me.moCanAdminExpensesTextbox, .AdminExpense)
                    Me.PopulateControlFromBOProperty(Me.moCanMarketingExpensesTextbox, .MarketingExpense)
                    Me.PopulateControlFromBOProperty(Me.moCanOtherTextbox, .Other)
                    Me.PopulateControlFromBOProperty(Me.moCanSalesTaxTextbox, .SalesTax)

                    'following 3 fields added for showing the status information of accounting - Felita to elita passback WR
                    AcctStatus = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCTSTATUS, Me.State.MyBO.TheCertCancellationBO.StatusId)
                    Me.PopulateControlFromBOProperty(Me.moAcctStatusTextbox, AcctStatus)
                    Me.PopulateControlFromBOProperty(Me.moAcctStatusDateTextbox, .StatusDate)
                    Me.PopulateControlFromBOProperty(Me.moAcctTrackNumTextbox, .TrackingNumber)
                    Dim strRefundMethod As String = LookupListNew.GetDescriptionFromExtCode(LookupListNew.LK_CRST, ElitaPlusIdentity.Current.ActiveUser.LanguageId, .RefundStatus)

                    Me.PopulateControlFromBOProperty(Me.moRefundRejectCodeText, .PayRejectCode)
                    Me.PopulateControlFromBOProperty(Me.moRefundRejectStatusText, strRefundMethod)

                    If Not .BankInfoId.Equals(Guid.Empty) Then
                        Me.State.RefundBankInfoBO = New BankInfo(.BankInfoId)
                        Me.PopulateControlFromBOProperty(Me.moRfAccountNumberText, Me.State.RefundBankInfoBO.Account_Number_Last4Digits)
                        Me.PopulateControlFromBOProperty(Me.moRfIBANNumberText, Me.State.RefundBankInfoBO.IbanNumberLast4Digits)
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
            If Me.State.MyBO.IsReverseCancellationEnabled(Me.State.MyBO.Id) Then
                If Me.State.MyBO.IsChildCertificate Then
                    ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, True)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, ReverseCancellationButton_WRITE, False)
            End If

        End Sub


#Region "DropDowns"

        Private Sub PopulateSalutationDropdown(ByVal salutationDropDownList As DropDownList)

            Try
                Dim salutation As ListItem() = CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode())
                salutationDropDownList.Populate(salutation, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateMembershipTypeDropdown(ByVal membershipTypeDropDownList As DropDownList)

            Try
                Dim membershipType As ListItem() = CommonConfigManager.Current.ListManager.GetList("MEMTYPE", Thread.CurrentPrincipal.GetLanguageCode())

                membershipTypeDropDownList.Populate(membershipType, New PopulateOptions() With
                                                       {
                                                       .AddBlankItem = True
                                                       })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateLangPrefDropdown(ByVal langPrefDropDownList As DropDownList)

            Try

                Dim langList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
                langPrefDropDownList.Populate(langList, New PopulateOptions() With
                                                 {
                                                 .AddBlankItem = True
                                                 })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateCreditCardTypesDropdown(ByVal cboCreditCardTypesDropDownList As DropDownList)

            Try

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CompanyId = Me.State.MyBO.CompanyId
                Dim creditCardTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CreditCardByCompany", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                cboCreditCardTypesDropDownList.Populate(creditCardTypeList, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulatePaymentTypesDropdown(ByVal cboPaymentTypesTypesDropDownList As DropDownList)

            Try

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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateCancellationReasonDropdown(ByVal cancellationReasonDropDownList As DropDownList, ByVal filtercondition As String, ByVal defaultDescription As String)

            Try

                Dim listcontext As ListContext = New ListContext()
                listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                listcontext.CompanyId = Me.State.MyBO.CompanyId
                Dim cancellationReasolist As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByRole", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cancellationReasonDropDownList.Populate(cancellationReasolist, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })

                BindSelectItemByText(defaultDescription, cancellationReasonDropDownList)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateCancelRequestReasonDropdown(ByVal cancellationReasonDropDownList As DropDownList, ByVal filtercondition As String)

            Try

                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyId = Me.State.MyBO.CompanyId
                Dim cancellationRequestReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                Dim filteredCancellationReasonList As ListItem()
                If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Function FilterCancellationReasonDropdown(CancellationReasonDV As DataView)
            If (CancellationReasonDV.Count > 0) Then
                Dim strRowFilter As String = CancellationReasonDV.RowFilter

                If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
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

        Private Sub PopulateCancelCommentTypeDropdown(ByVal CancelCommentTypeDropDownList As DropDownList)

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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Sub PopulatePaymentMethodDropdown(ByVal PaymentMethodDropDownList As DropDownList)

            Try

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                oListContext.CompanyId = Me.State.MyBO.CompanyId
                oListContext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                Dim paymentMethodList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.PaymentMethodByRoleCompany, Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                PaymentMethodDropDownList.Populate(paymentMethodList, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True,
                                                      .SortFunc = AddressOf PopulateOptions.GetDescription
                                                      })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateMaritalStatusDropdown(ByVal MaritalStatusDropDownList As DropDownList)
            Try

                Dim maritalStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("MARITAL_STATUS", Thread.CurrentPrincipal.GetLanguageCode())
                MaritalStatusDropDownList.Populate(maritalStatus, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True
                                                      })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateNationalityDropdown(ByVal nationalityDropDownList As DropDownList)
            Try
                Dim nationalities As ListItem() = CommonConfigManager.Current.ListManager.GetList("NATIONALITY", Thread.CurrentPrincipal.GetLanguageCode())
                nationalityDropDownList.Populate(nationalities, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePlaceOfBirthDropdown(ByVal PlaceOfBirthDropDownList As DropDownList)
            Try

                Dim placeOfBirth As ListItem() = CommonConfigManager.Current.ListManager.GetList("PLACEOFBIRTH", Thread.CurrentPrincipal.GetLanguageCode())
                PlaceOfBirthDropDownList.Populate(placeOfBirth, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True
                                                     })

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGenderDropdown(ByVal GenderDropDownList As DropDownList)
            Try

                Dim gender As ListItem() = CommonConfigManager.Current.ListManager.GetList("GENDER", Thread.CurrentPrincipal.GetLanguageCode())
                GenderDropDownList.Populate(gender, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePersonTypeDropdown(ByVal PersonTypeDropDownList As DropDownList)
            Try
                Dim personType As ListItem() = CommonConfigManager.Current.ListManager.GetList("PERSON_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
                PersonTypeDropDownList.Populate(personType, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateIncomeRangeDropdown(ByVal IncomeRangeDropDownList As DropDownList)
            Try
                Dim incomeRange As ListItem() = CommonConfigManager.Current.ListManager.GetList("IRNG", Thread.CurrentPrincipal.GetLanguageCode())

                IncomeRangeDropDownList.Populate(incomeRange, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePoliticallyExposedDropdown(ByVal PoliticallyExposedDropDownList As DropDownList)
            Try
                Dim politicallyExposed As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())

                PoliticallyExposedDropDownList.Populate(politicallyExposed, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

        Public Sub populateFormFromCertCancellationBO()

            If Not Me.State.certCancellationBO Is Nothing AndAlso Not Me.State.certCancellationBO.CancellationRequestedDate Is Nothing Then
                Me.CancelCertReqDateTextbox.Text = GetDateFormattedStringNullable(Me.State.certCancellationBO.CancellationRequestedDate.Value)
            Else
                Me.CancelCertReqDateTextbox.Text = GetDateFormattedStringNullable(System.DateTime.Today)
            End If
            Me.AddCalendar(Me.CancelCertReqDateImageButton, Me.CancelCertReqDateTextbox)

            If Not Me.State.certCancellationBO Is Nothing AndAlso Not Me.State.certCancellationBO.CancellationDate Is Nothing Then
                Me.CancelCertDateTextbox.Text = GetDateFormattedStringNullable(Me.State.certCancellationBO.CancellationDate.Value)
            Else
                If Not Me.State.MyBO.PreviousCertificateId.Equals(Guid.Empty) And Me.State.MyBO.WarrantySalesDate.Value > System.DateTime.Today Then
                    Me.CancelCertDateTextbox.Text = GetDateFormattedStringNullable(Me.State.MyBO.WarrantySalesDate.Value)
                    ControlMgr.SetEnableControl(Me, CancelCertDateTextbox, False)
                    ControlMgr.SetVisibleControl(Me, CancelCertDateImagebutton, False)
                Else
                    Me.CancelCertDateTextbox.Text = GetDateFormattedString(System.DateTime.Today)
                End If
            End If
            Me.AddCalendar(Me.CancelCertDateImagebutton, Me.CancelCertDateTextbox)

        End Sub

        Public Sub ClearCommentsControls()
            Me.moCallerNameTextBox.Text = String.Empty
            Me.moCommentsTextbox.Text = String.Empty
        End Sub
        Public Sub PopulateCancellationDate()
            Dim iFullRefundDays As Integer
            Dim oContract As New Contract, dtCancellationDate As Date
            If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then

                If Me.moCancelRequestDateTextBox.Text <> String.Empty Then
                    If Not IsDate(moCancelRequestDateTextBox.Text) Then
                        ElitaPlusPage.SetLabelError(moCancelRequestDateLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE)
                    Else
                        ClearLabelError(moCancelRequestDateLabel)
                        If IsNothing(State.certCancelRequestBO) Then
                            Me.State.certCancelRequestBO = New CertCancelRequest
                        End If
                        Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CancellationRequestDate", Me.moCancelRequestDateTextBox)

                        If (State.certCancelRequestBO.CancellationRequestDate.Value > DateTime.Today) Then
                            ElitaPlusPage.SetLabelError(moCancelRequestDateLabel)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE)
                        End If

                        oContract = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value)
                        iFullRefundDays = oContract.FullRefundDays
                        Me.State.CertTerm = State.MyBO.getCertTerm
                        If Not GetSelectedItem(Me.moCancelRequestReasonDrop).Equals(Guid.Empty) Then
                            Dim oCancellationReason As New CancellationReason(GetSelectedItem(moCancelRequestReasonDrop))
                            State.CancReasonIsLawful = oCancellationReason.IsLawful
                            State.RefundComputeMethod = LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_COMP_METHOD, oCancellationReason.RefundComputeMethodId)
                            State.CancReasonCode = oCancellationReason.Code
                        End If

                        'Incase of Full Refund Computation method assign cancellation request date as Cancellation Date
                        If State.RefundComputeMethod = Codes.REFUND_COMPUTE_METHOD__1 Then
                            dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                        Else

                            If DateDiff("D", Me.State.MyBO.WarrantySalesDate.Value, State.certCancelRequestBO.CancellationRequestDate.Value) <= iFullRefundDays Then
                                dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                            Else
                                If State.CancReasonIsLawful = Codes.EXT_YESNO_N Then
                                    If State.certCancelRequestBO.CancellationRequestDate.Value < DateAdd("M", State.CertTerm, Me.State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", -1, DateAdd("M", State.CertTerm, Me.State.MyBO.WarrantySalesDate.Value))
                                    Else
                                        dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                                    End If
                                ElseIf State.CancReasonIsLawful = Codes.EXT_YESNO_Y Then
                                    If State.CancReasonCode = Codes.SFR_CR_CHATELLAW And State.certCancelRequestBO.CancellationRequestDate.Value < DateAdd("M", State.CertTerm, Me.State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", -1, DateAdd("M", State.CertTerm, Me.State.MyBO.WarrantySalesDate.Value))
                                    ElseIf State.CancReasonCode = Codes.SFR_CR_HAMONLAW And State.certCancelRequestBO.CancellationRequestDate.Value > DateAdd("M", State.CertTerm, Me.State.MyBO.WarrantySalesDate.Value) Then
                                        dtCancellationDate = DateAdd("D", +30, State.certCancelRequestBO.CancellationRequestDate.Value)
                                    Else
                                        dtCancellationDate = State.certCancelRequestBO.CancellationRequestDate.Value
                                    End If
                                End If
                            End If
                        End If
                    End If
                    Me.PopulateControlFromBOProperty(Me.moCancelDateTextBox, dtCancellationDate)
                End If
            End If
        End Sub
        Public Sub populateCancelRequestInfoTab()
            Try

                PopulateCancelRequestReasonDropdown(Me.moCancelRequestReasonDrop, Nothing)
                PopulateCancelCommentTypeDropdown(moCancelRequestJustificationDrop)
                Me.moProofOfDocumentationDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Me.moUseExistingBankDetailsDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

                Me.State.CertInstalBankInfoId = Me.State.MyBO.getCertInstalBankInfoID

                If Me.State.CertCancelRequestId.Equals(Guid.Empty) Then
                    ''status
                    ControlMgr.SetVisibleControl(Me, moCancelRequestStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCancelRequestStatusText, False)

                    If Me.State.IsEdit = True Then
                        If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
                            Me.moCancelDateTextBox.Text = String.Empty
                            Me.moCancelRequestDateTextBox.Text = String.Empty
                        Else
                            Me.moCancelDateTextBox.Text = GetDateFormattedString(System.DateTime.Now)
                            Me.moCancelRequestDateTextBox.Text = GetDateFormattedString(System.DateTime.Now)
                        End If
                    Else
                        Me.moCancelDateTextBox.Text = String.Empty
                        Me.moCancelRequestDateTextBox.Text = String.Empty
                        Me.moCancelRequestReasonDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Me.moCancelRequestJustificationDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Me.moProofOfDocumentationDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Me.moUseExistingBankDetailsDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Me.moCRIBANNumberText.Text = String.Empty
                        If Me.State.CancelRulesForSFR = Codes.YESNO_Y Then
                            ControlMgr.SetEnableControl(Me, moCancelDateTextBox, False)
                            ControlMgr.SetVisibleControl(Me, moCancelDateImageButton, False)
                        End If
                    End If
                    If Not Me.State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, True)
                        ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, True)
                        If Me.State.IsEdit = True Then
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
                    With Me.State.certCancelRequestBO
                        Me.PopulateControlFromBOProperty(Me.moCancelRequestReasonDrop, .CancellationReasonId)
                        Me.PopulateControlFromBOProperty(Me.moCancelDateTextBox, .CancellationDate)
                        Me.PopulateControlFromBOProperty(Me.moCancelRequestDateTextBox, .CancellationRequestDate)
                        ''status
                        ControlMgr.SetVisibleControl(Me, moCancelRequestStatusLabel, True)
                        ControlMgr.SetVisibleControl(Me, moCancelRequestStatusText, True)
                        Me.PopulateControlFromBOProperty(Me.moCancelRequestStatusText, .StatusDescription)

                        If (Me.State.CancelRulesForSFR = Codes.YESNO_Y) Then

                            If Not .ProofOfDocumentation Is Nothing AndAlso Not String.IsNullOrEmpty(.ProofOfDocumentation) Then
                                BindSelectItem(.ProofOfDocumentation.ToString, moProofOfDocumentationDrop)
                            End If
                            If Me.State.CancReasonIsLawful Is Nothing Then
                                Dim oCancellationReason As New CancellationReason(.CancellationReasonId)
                                Me.State.CancReasonIsLawful = oCancellationReason.IsLawful
                                Me.State.CancReasonCode = oCancellationReason.Code
                            End If
                            If Not Me.State.certCancelRequestBO.BankInfoId.Equals(Guid.Empty) Then
                                If Me.State.CertInstalBankInfoId.Equals(Me.State.certCancelRequestBO.BankInfoId) Then
                                    BindSelectItem(Codes.EXT_YESNO_Y, moUseExistingBankDetailsDrop)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, False)
                                    Me.moCRIBANNumberText.Text = String.Empty
                                Else
                                    BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                                    Me.State.CRequestBankInfoBO = New BankInfo(Me.State.certCancelRequestBO.BankInfoId)
                                    Me.PopulateControlFromBOProperty(Me.moCRIBANNumberText, Me.State.CRequestBankInfoBO.IbanNumberLast4Digits)
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
                            If Me.State.IsEdit = False Then
                                ControlMgr.SetEnableControl(Me, moProofOfDocumentationDrop, False)
                                ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)

                            End If

                            If Me.State.CancReasonIsLawful = Codes.EXT_YESNO_Y And (Me.State.CancReasonCode = Codes.SFR_CR_DEATH Or Me.State.CancReasonCode = Codes.SFR_CR_MOVINGABROAD) Then
                                ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, True)
                                ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, True)
                            End If

                        End If
                    End With
                End If
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Me.State.MyBO.Dealer.UseNewBillForm) = Codes.YESNO_Y) Then
                    Dim bankobj As BankInfo
                    If Not Me.State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        bankobj = New BankInfo(Me.State.CertInstalBankInfoId)
                        If bankobj.IbanNumber Is Nothing Then
                            If Me.State.IsEdit = True Then
                                BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                                Ibanoperation(True)
                            Else
                                Ibanoperation(False)

                            End If

                        End If
                    End If

                    If Me.State.CertInstalBankInfoId.Equals(Guid.Empty) Then
                        If Me.State.IsEdit = True Then
                            BindSelectItem(Codes.EXT_YESNO_N, moUseExistingBankDetailsDrop)
                            Ibanoperation(True)
                        Else
                            Ibanoperation(False)
                        End If
                    End If
                End If

                If Not Me.State.CertCancelRequestId.Equals(Guid.Empty) And Not Me.State.MyBO.getCancelationRequestFlag = YES Then
                    ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)
                End If

                ClearCommentsControls()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateFinanceTab()

            With Me.State.MyBO
                If Not String.IsNullOrEmpty(.Finance_Tab_Amount) Then
                    Me.PopulateControlFromBOProperty(Me.moFinanceAmount, CType(.Finance_Tab_Amount, Decimal), Me.DECIMAL_FORMAT)
                Else
                    Me.PopulateControlFromBOProperty(Me.moFinanceAmount, 0, Me.DECIMAL_FORMAT)
                End If

                Me.PopulateControlFromBOProperty(Me.moFinanceTerm, .Finance_Term, Me.DECIMAL_FORMAT)

                If (Not String.IsNullOrEmpty(.Finance_Frequency)) AndAlso Convert.ToDecimal(.Finance_Frequency) > 0 Then
                    Me.PopulateControlFromBOProperty(Me.moFinanceFrequency, LookupListNew.GetDescrionFromListCode("FINFREQ", .Finance_Frequency))
                Else
                    Me.PopulateControlFromBOProperty(Me.moFinanceFrequency, "")
                End If

                Me.PopulateControlFromBOProperty(Me.moFinanceInstallmentNum, .Finance_Installment_Number, Me.DECIMAL_FORMAT)

                If (Not String.IsNullOrEmpty(.Finance_Installment_Amount)) Then
                    Me.PopulateControlFromBOProperty(Me.moFinanceInstallmentAmount, CType(.Finance_Installment_Amount, Decimal), Me.DECIMAL_FORMAT)
                Else
                    Me.PopulateControlFromBOProperty(Me.moFinanceInstallmentAmount, 0, Me.DECIMAL_FORMAT)
                End If

                Dim blnIncomingAmount As Boolean = Me.State.MyBO.Product.UpgradeFinanceBalanceComputationMethod.Equals(Codes.UPG_FINANCE_BAL_COMP_METH_IA)

                If ((Not String.IsNullOrEmpty(.Finance_Tab_Amount)) AndAlso Convert.ToDecimal(.Finance_Tab_Amount) > 0) OrElse blnIncomingAmount Then
                    Me.PopulateControlFromBOProperty(Me.moCurrentOutstandingBalanceText, CType(.GetFinancialAmountprodcode, Decimal), Me.DECIMAL_FORMAT)
                    If blnIncomingAmount Then
                        Me.PopulateControlFromBOProperty(Me.moOutstandingBalanceDueDateText, .OutstandingBalanceDueDate)
                    Else
                        ControlMgr.SetVisibleControl(Me, moOutstandingBalanceDueDateLabel, False)
                        ControlMgr.SetVisibleControl(Me, moOutstandingBalanceDueDateText, False)
                    End If
                    Me.PopulateControlFromBOProperty(Me.moFinanceDateText, .FinanceDate)
                    Me.PopulateControlFromBOProperty(Me.moDownPaymentText, .DownPayment, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.moAdvancePaymentText, .AdvancePayment, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.moUpgradeFixedTermText, .UpgradeFixedTerm, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.moBillingAccountNumberText, .BillingAccountNumber)
                Else
                    Me.PopulateControlFromBOProperty(Me.moCurrentOutstandingBalanceText, "")
                    Me.PopulateControlFromBOProperty(Me.moFinanceDateText, "")
                    Me.PopulateControlFromBOProperty(Me.moDownPaymentText, "")
                    Me.PopulateControlFromBOProperty(Me.moAdvancePaymentText, "")
                    Me.PopulateControlFromBOProperty(Me.moUpgradeFixedTermText, "")
                    Me.PopulateControlFromBOProperty(Me.moBillingAccountNumberText, "")
                End If

                If .NumOfConsecutivePayments >= 0 Then
                    Me.PopulateControlFromBOProperty(Me.moNumOfConsecutivePaymentsText, .NumOfConsecutivePayments)
                Else
                    Me.PopulateControlFromBOProperty(Me.moNumOfConsecutivePaymentsText, "")
                End If

                If (Not String.IsNullOrEmpty(.DealerCurrentPlanCode)) Then
                    Me.PopulateControlFromBOProperty(Me.moDealerCurrentPlanCodeText, .DealerCurrentPlanCode)
                Else
                    Me.PopulateControlFromBOProperty(Me.moDealerCurrentPlanCodeText, "")
                End If

                If (Not String.IsNullOrEmpty(.DealerScheduledPlanCode)) Then
                    Me.PopulateControlFromBOProperty(Me.moDealerScheduledPlanCodeText, .DealerScheduledPlanCode)
                Else
                    Me.PopulateControlFromBOProperty(Me.moDealerScheduledPlanCodeText, "")
                End If
                If (Not String.IsNullOrEmpty(.DealerRewardPoints)) Then
                    Me.PopulateControlFromBOProperty(Me.moDealerRewardPointsText, .DealerRewardPoints)
                Else
                    Me.PopulateControlFromBOProperty(Me.moDealerRewardPointsText, "")
                End If

                If Not Me.State.MyBO.UpgradeTermUomId.Equals(Guid.Empty) Then
                    Dim strUpgradeTermUomCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Me.State.MyBO.UpgradeTermUomId)
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

                        Me.PopulateControlFromBOProperty(Me.moUpgradeFixedTermText, .UpgradeFixedTerm)
                        Dim strUpgradeTermUomDesc As String = LookupListNew.GetDescriptionFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Me.State.MyBO.UpgradeTermUomId, True)
                        Me.PopulateControlFromBOProperty(Me.moUpgradeTermUnitOfMeasureText, strUpgradeTermUomDesc)
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
                        Me.PopulateControlFromBOProperty(Me.moUpgradeTermTextFROM, .UpgradeTermFrom)
                        Me.PopulateControlFromBOProperty(Me.moUpgradeTermTextTo, .UpgradeTermTo)
                        Dim strUpgradeTermUomDesc As String = LookupListNew.GetDescriptionFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Me.State.MyBO.UpgradeTermUomId, True)
                        Me.PopulateControlFromBOProperty(Me.moUpgradeTermUnitOfMeasureText, strUpgradeTermUomDesc)
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

                Me.PopulateControlFromBOProperty(Me.moLoanCodeText, .LoanCode)
                Me.PopulateControlFromBOProperty(Me.moPaymentShiftNumberText, .PaymentShiftNumber)

                If (Not String.IsNullOrEmpty(.PenaltyFee)) Then
                    Me.PopulateControlFromBOProperty(Me.moPenaltyFeeText, .PenaltyFee)
                Else
                    Me.PopulateControlFromBOProperty(Me.moPenaltyFeeText, "")
                End If

                If (Not String.IsNullOrEmpty(.AppleCareFee)) Then
                    Me.PopulateControlFromBOProperty(Me.moAppleCareFeeText, .AppleCareFee)
                Else
                    Me.PopulateControlFromBOProperty(Me.moAppleCareFeeText, "")
                End If

                Me.PopulateControlFromBOProperty(Me.moUpgradeProgramText, .UpgradeProgram)
            End With
            PopulateUPgradeDataExtensionsGrid()
        End Sub

        Public Sub populateCertCancellationBOFromForm()
            Try
                With Me.State.certCancellationBO
                    Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationReasonId", Me.moCancellationReasonDrop)
                    Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationDate", Me.CancelCertDateTextbox)
                    Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationRequestedDate", Me.CancelCertReqDateTextbox)
                    Me.PopulateBOProperty(Me.State.certCancellationBO, "Retailer", Me.moRetailerText)
                End With
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelCommentBOFromForm()
            Try
                Me.State.CancCommentBO = Comment.GetNewComment(Me.State.MyBO.Id)
                Me.PopulateBOProperty(Me.State.CancCommentBO, "CallerName", Me.CancelCallerNameTextbox)
                Me.PopulateBOProperty(Me.State.CancCommentBO, "Comments", Me.moCancelCommentsTextbox)
                Me.PopulateBOProperty(Me.State.CancCommentBO, "CommentTypeId", Me.moCancelCommentType)

                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelRequestBOFromForm()
            Try
                If IsNothing(Me.State.certCancelRequestBO) Then
                    Me.State.certCancelRequestBO = New CertCancelRequest
                End If

                ' Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CertId", Me.State.CertificateId)
                Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CancellationReasonId", Me.moCancelRequestReasonDrop)
                Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CancellationDate", Me.moCancelDateTextBox)
                Me.PopulateBOProperty(Me.State.certCancelRequestBO, "CancellationRequestDate", Me.moCancelRequestDateTextBox)
                Me.PopulateBOProperty(Me.State.certCancelRequestBO, "ProofOfDocumentation", Me.moProofOfDocumentationDrop, False, True)

                Me.State.certCancelRequestBO.BankInfoId = Me.State.CertInstalBankInfoId
                If IsNothing(Me.State.CRequestBankInfoBO) Then
                    Me.State.CRequestBankInfoBO = New BankInfo
                End If
                If GetSelectedValue(moUseExistingBankDetailsDrop) = Codes.EXT_YESNO_N Then
                    Me.PopulateBOProperty(Me.State.CRequestBankInfoBO, "IbanNumber", Me.moCRIBANNumberText)
                    Me.PopulateBOProperty(Me.State.CRequestBankInfoBO, "CountryID", Me.State.MyBO.Company.CountryId)
                    'Me.PopulateBOProperty(Me.State.CRequestBankInfoBO, "Account_Number", Me.moCRAccountNumberText)
                    Me.State.useExistingBankInfo = Codes.YESNO_N
                Else
                    Me.State.useExistingBankInfo = Codes.YESNO_Y
                End If

                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub populateCertCancelRequestCommentBOFromForm()
            Try
                'Dim oCommTypeId As Guid
                Me.State.CancReqCommentBO = Comment.GetNewComment(Me.State.MyBO.Id)
                Me.PopulateBOProperty(Me.State.CancReqCommentBO, "CallerName", Me.moCallerNameTextBox)
                Me.PopulateBOProperty(Me.State.CancReqCommentBO, "Comments", Me.moCommentsTextbox)
                PopulateBOProperty(Me.State.CancReqCommentBO, "CommentTypeId", GetSelectedItem(Me.moCancelRequestJustificationDrop))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'This method will change the Page Index and the Selected Index
        Public Function FindDVSelectedRowIndex(ByVal dv As CertItemCoverage.CertItemCoverageSearchDV) As Integer
            If Me.State.TheItemCoverageState.selectedItemId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte())).Equals(Me.State.TheItemCoverageState.selectedItemId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        End Function

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If Not Me.State Is Nothing Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.State.LastErrMsg = ""
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception
                '  Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CheckIfComingFromRemoveCancelDueDateConfirm()
            Dim confResponse As String = Me.HiddenRemoveCancelDueDatePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                SaveCancellationDueDate()
                PopulatePremiumInfoTab()
            End If
            Me.HiddenRemoveCancelDueDatePromptResponse.Value = ""
        End Sub

        Protected Sub CheckIfComingFromTransferOfOwnershipConfirm()
            Try
                Dim confResponse As String = Me.HiddenTransferOfOwnershipPromptResponse.Value
                If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                    Me.State.IsEdit = False
                    Me.State.NavigateToEndorment = True
                    Me.PopulateBOsFromForm()
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                    Me.NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Me.saveCertificate()
                End If
                Me.HiddenTransferOfOwnershipPromptResponse.Value = ""
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Dim myBo As Certificate = Me.State.MyBO
            Dim lastAction As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
            Dim lastErrMsg As String = Me.State.LastErrMsg
            'Clean after consuming the action
            CleanPopupInput()
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                Select Case lastAction
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.State.MyBO.Save()
                        Me.saveCertificate()
                        Me.State.certificateChanged = True
                        Me.State.selectedTab = 0
                        Me.State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, Me.State.certificateChanged)
                        Me.NavController = Nothing
                        Me.ReturnToCallingPage(retObj)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.State.selectedTab = 0
                        Me.State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(lastAction, myBo, Me.State.certificateChanged)
                        Me.NavController = Nothing
                        Me.ReturnToCallingPage(retObj)
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case lastAction
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.State.selectedTab = 0
                        Me.State.CertHistoryDV = Nothing
                        Dim retObj As ReturnType = New ReturnType(lastAction, myBo, Me.State.certificateChanged)
                        Me.NavController = Nothing
                        Me.ReturnToCallingPage(retObj)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.State.selectedTab = 0
                        Me.State.CertHistoryDV = Nothing
                        Me.MasterPage.MessageController.AddErrorAndShow(lastErrMsg)
                End Select
            End If
        End Sub

        Private Function CheckCUITCUIL(ByVal cuit As String) As Boolean
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

                If Me.moVehicleLicenseTagText.Text <> "" Then
                    Dim dv As DataView, oCert As Certificate
                    Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    dv = oCert.ValidateLicenseFlag(Me.moVehicleLicenseTagText.Text, Me.State.MyBO.CertNumber, compGroupId)
                    If dv.Count > 0 Then
                        ElitaPlusPage.SetLabelError(Me.moVehicleLicenseTagLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VEHICLE_LICENSE_TAG_ERR)
                    End If
                End If

                'REQ-1255 -- START
                'Validate CUIT_CUIL field
                If Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) Or
                   Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "2")) Then  '1= Display and Require When Cancelling or 2= Display Only
                    If Me.moCUIT_CUILText.Text <> String.Empty Then
                        Dim CUIT_CUIL_Number As Int64 = 0
                        Dim DigitCheckerResult As Boolean = False
                        'Checking for numeric
                        If Me.moCUIT_CUILText.Text.Length > 11 Or Not Int64.TryParse(Me.moCUIT_CUILText.Text, CUIT_CUIL_Number) Then
                            Throw New GUIException(Message.MSG_INVALID_CUIT_CUIL_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CUIT_CUIL_NUMBER_ERR)
                        End If
                        'Checking for valid number
                        DigitCheckerResult = CheckCUITCUIL(Me.moCUIT_CUILText.Text)
                        If Not DigitCheckerResult Then
                            Throw New GUIException(Message.MSG_INVALID_CUIT_CUIL_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CUIT_CUIL_NUMBER_ERR)
                        End If
                    End If
                End If
                'REQ-1255 -- END

                Me.PopulateBOsFromForm()

                ' Validate User Selected Required Fields
                ValidateRequiredFields()

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    PopulateFormFromBOs()
                    Me.State.IsEdit = False
                    Me.EnableDisableFields()
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Me.State.certificateChanged = True
                Else
                    Me.State.IsEdit = False
                    Me.MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                    Me.EnableDisableFields()
                End If
                DisplayMaskDob()
            Catch ex As DataNotFoundException
                Me.MasterPage.MessageController.AddError(ex.Message)
                Me.State.IsEdit = True
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.State.IsEdit = True
                Me.EnableDisableFields()
            End Try
        End Sub
        Public Sub SaveCancellationDueDate()
            Me.PopulateCanceDueDateFromForm()
            Me.State.TheDirectDebitState.certInstallment.Save()
            Me.EnableDisableFields()
        End Sub
        Public Sub saveCertInstallment(ByVal StatusChenge As Boolean)
            Try

                If Me.State.TheDirectDebitState.StatusChenge Then

                    'REQ-5761
                    If Me.State.IsNewBillPayBtnVisible Then
                        Me.State.TheDirectDebitState.certInstallment.SP_ChngOfBillingStatus(Me.GetSelectedItem(Me.moBillingStatusId))
                    Else
                        Me.State.TheDirectDebitState.certInstallment.SP_ChangeOfBillingStatus(Me.GetSelectedItem(Me.moBillingStatusId))
                    End If

                    Me.State.TheDirectDebitState.StatusChenge = False
                    Me.PopulateControlFromBOProperty(Me.moNextDueDateText, Me.State.TheDirectDebitState.certInstallment.newPaymentDueDate)
                End If

                Me.PopulateInstalmenBoFromForm()
                If Me.State.TheDirectDebitState.certInstallment.IsFamilyDirty Then
                    If Me.State.creditCardPayment Then
                        Me.State.TheDirectDebitState.CreditCardInfo.Save()
                        Me.State.TheDirectDebitState.certInstallment.CreditCardInfoId = Me.State.TheDirectDebitState.CreditCardInfo.Id
                    ElseIf Me.State.directDebitPayment Then
                        Me.State.TheDirectDebitState.bankInfo.DomesticTransfer = False
                        Me.State.TheDirectDebitState.bankInfo.InternationalTransfer = False
                        Me.State.TheDirectDebitState.bankInfo.InternationalTransfer = False
                        Me.State.TheDirectDebitState.bankInfo.SourceCountryID = Me.State.TheDirectDebitState.bankInfo.CountryID
                        Me.State.TheDirectDebitState.bankInfo.Save()
                        Me.State.TheDirectDebitState.certInstallment.BankInfoId = Me.State.TheDirectDebitState.bankInfo.Id
                    End If

                    If Me.State.BillingInformationChanged Then AdjustTheBillingStatus()

                    Me.State.TheDirectDebitState.certInstallment.Save()
                    Me.State.IsEdit = False
                    Me.PopulatePremiumInfoTab()
                    Me.EnableDisableFields()
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As ApplicationException
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.State.IsEdit = True
                Me.EnableDisableFields()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub AdjustTheBillingStatus()
            With Me.State.TheDirectDebitState.certInstallment
                Dim currentStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.BillingStatusId)
                Dim selectedStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.GetSelectedItem(Me.moBillingStatusId))
                Dim objBillingDetail As BillingDetail
                Dim objBillingPayDetail As BillingPayDetail

                ' There might not be any billingDetail records yet for this installment, so the next section is made to catch no data found error.

                'REQ-5761
                If Me.State.IsNewBillPayBtnVisible Then
                    Try
                        objBillingPayDetail = .CurrentBillingPaydetail
                    Catch ex As DataNotFoundException
                    Catch ex As Exception

                    End Try

                    If Not Me.State.MyBO Is Nothing AndAlso Not objBillingPayDetail Is Nothing _
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

                    If Not Me.State.MyBO Is Nothing AndAlso Not objBillingDetail Is Nothing _
                       AndAlso currentStatus = Codes.BILLING_STATUS__REJECTED _
                       AndAlso selectedStatus = Codes.BILLING_STATUS__ACTIVE _
                       AndAlso Not objBillingDetail.ReAttemptCount.Equals(DBNull.Value) _
                       AndAlso objBillingDetail.ReAttemptCount >= Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value).CollectionReAttempts.Value Then
                        Throw New GUIException(Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED, Message.MSG_BILLING_STATUS_CANNOT_BE_CHANGED)
                    End If
                End If

                If currentStatus = Codes.BILLING_STATUS__REJECTED Or currentStatus = Codes.BILLING_STATUS__ON_HOLD Then
                    .BillingStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, Codes.BILLING_STATUS__ACTIVE)
                End If

            End With
        End Sub

        Public Function IsRetailerAssociated(ByVal odealer As Dealer) As Boolean
            Dim oYesList As DataView = LookupListNew.GetListItemId(odealer.RetailerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
            If oYesNo = "N" Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Function IsEffectiveCoverage(ByVal BeginDate As Date, ByVal EndDate As Date) As Boolean
            Dim todayDate As Date
            If Me.State.MyBO.StatusCode = CLOSED Then
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

            If Me.State.MyBO.CustomerName Is Nothing OrElse Me.State.MyBO.CustomerName.Trim = String.Empty Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                addressErrorExist = True
            End If

            Dim strAddFmt As String = New Country(Me.State.MyBO.AddressChild.CountryId).MailAddrFormat.ToUpper
            If Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "ADR1") Then
                If Me.State.MyBO.AddressChild.Address1 Is Nothing OrElse Me.State.MyBO.AddressChild.Address1.Trim = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "CITY") Then
                If Me.State.MyBO.AddressChild.City Is Nothing OrElse Me.State.MyBO.AddressChild.City.Trim = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "ZIP") Then
                If Me.State.MyBO.AddressChild.PostalCode Is Nothing OrElse Me.State.MyBO.AddressChild.PostalCode.Trim = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "RGNAME") OrElse
               Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "RGCODE") Then
                If Me.State.MyBO.AddressChild.RegionId.Equals(Guid.Empty) Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    addressErrorExist = True
                End If
            End If

            If Me.State.MyBO.AddressChild.IsAddressComponentRequired(strAddFmt, "COU") Then
                If Me.State.MyBO.AddressChild.CountryId.Equals(Guid.Empty) Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
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

            If Me.State.MyBO.CountryPurchaseId = Nothing Then
                Throw New GUIException(Message.MSG_INVALID_COUNTRY, Message.MSG_INVALID_COUNTRY)
            Else
                _country = New Country(Me.State.MyBO.CountryPurchaseId)
                If (Not (Nothing Is _country)) AndAlso (_country.AddressInfoReqFields <> Nothing) Then
                    strRequiredFieldSetting = _country.AddressInfoReqFields.ToUpper()
                End If
            End If

            If strRequiredFieldSetting.Trim().Length > 0 Then
                ' Validating Customer Salutation
                If strRequiredFieldSetting.Contains("[SALU]") Then
                    If Me.State.MyBO.SalutationId = Nothing OrElse Me.State.MyBO.SalutationId = Guid.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("INVALID_SALUTATION")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Name
                If strRequiredFieldSetting.Contains("[NAME]") Then
                    If Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                        If Me.State.MyBO.CustomerName Is Nothing OrElse Me.State.MyBO.CustomerName.Trim() = String.Empty Then
                            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                            requiredFieldsErrorExist = True
                        End If
                    Else
                        If (Me.State.MyBO.CustomerFirstName Is Nothing OrElse Me.State.MyBO.CustomerFirstName.Trim() = String.Empty) Then
                            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_FIRST_NAME_ERR)
                            requiredFieldsErrorExist = True
                        ElseIf (Me.State.MyBO.CustomerLastName Is Nothing OrElse Me.State.MyBO.CustomerLastName.Trim() = String.Empty) Then
                            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_LAST_NAME_ERR)
                            requiredFieldsErrorExist = True
                        End If
                    End If
                End If

                ' Validating Customer Address1
                If strRequiredFieldSetting.Contains("[ADR1]") Then
                    If Me.State.MyBO.AddressChild.Address1 Is Nothing OrElse Me.State.MyBO.AddressChild.Address1.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ADDRESS1_FIELD_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Address2
                If strRequiredFieldSetting.Contains("[ADR2]") Then
                    If Me.State.MyBO.AddressChild.Address2 Is Nothing OrElse Me.State.MyBO.AddressChild.Address2.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ADDRESS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Address3
                If strRequiredFieldSetting.Contains("[ADR3]") Then
                    If Me.State.MyBO.AddressChild.Address3 Is Nothing OrElse Me.State.MyBO.AddressChild.Address3.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ADDRESS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer City
                If strRequiredFieldSetting.Contains("[CITY]") Then
                    If Me.State.MyBO.AddressChild.City Is Nothing OrElse Me.State.MyBO.AddressChild.City.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("CITY_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Zip
                If strRequiredFieldSetting.Contains("[ZIP]") Then
                    If Me.State.MyBO.AddressChild.PostalCode Is Nothing OrElse Me.State.MyBO.AddressChild.PostalCode.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ZIP_IS_MISSING")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer State/Provience OR Region
                If strRequiredFieldSetting.Contains("[PRO]") Then
                    If Me.State.MyBO.AddressChild.RegionId = Nothing OrElse Me.State.MyBO.AddressChild.RegionId = Guid.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ERR_INVALID_STATE")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Country
                If strRequiredFieldSetting.Contains("[COU]") Then
                    If Me.State.MyBO.AddressChild.CountryId = Nothing OrElse Me.State.MyBO.AddressChild.CountryId = Guid.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("ERR_INVALID_COUNTRY")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Email
                If strRequiredFieldSetting.Contains("[EMAIL]") Then
                    If Me.State.MyBO.Email Is Nothing OrElse Me.State.MyBO.Email.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("EMAIL_IS_REQUIRED_ERR")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Work Phone
                If strRequiredFieldSetting.Contains("[WPHONE]") Then
                    If Me.State.MyBO.WorkPhone Is Nothing OrElse Me.State.MyBO.WorkPhone.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("CELL_PHONE_NUMBER_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Home Phone
                If strRequiredFieldSetting.Contains("[HPHONE]") Then
                    If Me.State.MyBO.HomePhone Is Nothing OrElse Me.State.MyBO.HomePhone.Trim() = String.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("HOME_PHONE_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

                ' Validating Customer Region
                If strRequiredFieldSetting.Contains("[RGN]") Then
                    If Me.State.MyBO.AddressChild.RegionId = Nothing OrElse Me.State.MyBO.AddressChild.RegionId = Guid.Empty Then
                        Me.MasterPage.MessageController.AddErrorAndShow("REGION_IS_REQUIRED")
                        requiredFieldsErrorExist = True
                    End If
                End If

            End If

            If Me.State.MyBO.AddressChild.IsEmpty Then
                If (Me.State.ClaimsearchDV Is Nothing) Then
                    Me.State.ClaimsearchDV = Me.State.MyBO.ClaimsForCertificate(Me.State.CertificateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
                If (Me.State.ClaimsearchDV.Table.Rows.Count > 0) Then
                    Throw New GUIException(Message.MSG_CANNOT_REMOVE_ADDRESS, Message.MSG_CANNOT_REMOVE_ADDRESS)
                End If
            End If

            If requiredFieldsErrorExist Then
                Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Message.MSG_GUI_INVALID_VALUE)
            End If

        End Sub

        Private Sub validateCancellationProcessFields()

            Dim CanellProcErrorExist As Boolean = False

            If Me.State.certCancellationBO.CancellationReasonId.Equals(Guid.Empty) Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE)
                CanellProcErrorExist = True
            End If

            If Me.State.certCancellationBO.CancellationDate.Equals(DBNull.Value) Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE)
                CanellProcErrorExist = True
            End If

            If CanellProcErrorExist Then
                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED)
            End If
        End Sub
        Private Sub validateUpdateBankInfoRefundFields()

            ClearLabelError(moRfIBANNumberLabel)
            Try
                If Me.State.RefundBankInfoBO.IbanNumber <> String.Empty Then
                    Me.State.RefundBankInfoBO.Validate()
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

            If Me.State.CancCommentBO.CallerName Is Nothing OrElse Me.State.CancCommentBO.CallerName.Trim = String.Empty Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.MSG_CALLER_NAME_REQUIRED)
                CanellCommentErrorExist = True
            End If

            If Me.State.CancCommentBO.CommentTypeId.Equals(Guid.Empty) Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_JUSTIFICATION_MUST_BE_SELECTED_ERR)
                CanellCommentErrorExist = True
            End If

            If Me.State.CancCommentBO.Comments Is Nothing OrElse Me.State.CancCommentBO.Comments.Trim = String.Empty Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMMENTS_ARE_REQUIRED)
                CanellCommentErrorExist = True
            End If

            If CanellCommentErrorExist Then
                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED)
            End If
        End Sub
#End Region

#Region "Other Customer Information"
        <WebMethod(), Script.Services.ScriptMethod()>
        Public Shared Function GetOtherCustomerDetails(ByVal customerId As String, ByVal custInfoExclude As String, ByVal cust_salutation_exclude As String, ByVal lang_id As String, ByVal identification_number_type As String) As String
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

            Dim cert As Certificate = Me.State.MyBO
            Dim dv As Certificate.OtherCustomerInfoDV = Certificate.GetOtherCustomerInfo(Me.State.MyBO.Id, Me.State.MyBO.IdentificationNumberType)
            Dim objDataColCustInfoExclude As DataColumn = New DataColumn()
            Dim objDataColCustSalutationExclude As DataColumn = New DataColumn()
            Dim objDataColLangID As DataColumn = New DataColumn()
            Dim objDataColIdentificationNumberType As DataColumn = New DataColumn()

            If Me.State.ReqCustomerLegalInfoId.Equals(Guid.Empty) Then
                Me.State.ReqCustomerLegalInfoId = (New Company(Me.State.MyBO.CompanyId).ReqCustomerLegalInfoId)
            End If





            If dv.Count > 0 Then
                Me.CustomerCount.Text = "(" + dv.Table.Rows.Count.ToString() + ")"

                objDataColCustInfoExclude.ColumnName = "CUST_INFO_EXCLUDE"
                objDataColCustInfoExclude.DataType = GetType(String)
                objDataColCustInfoExclude.DefaultValue = Me.State.ReqCustomerLegalInfoId.ToString()
                dv.Table.Columns.Add(objDataColCustInfoExclude)

                objDataColCustSalutationExclude.ColumnName = "CUST_SALUTATION_EXCLUDE"
                objDataColCustSalutationExclude.DataType = GetType(String)
                If Me.State.isSalutation Then
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
                objDataColIdentificationNumberType.DefaultValue = Me.State.MyBO.IdentificationNumberType.ToString()
                dv.Table.Columns.Add(objDataColIdentificationNumberType)


                Me.CertOtherCustomers.AutoGenerateColumns = False
                Me.CertOtherCustomers.DataSource = dv
                Me.CertOtherCustomers.DataBind()
                ControlMgr.SetVisibleControl(Me, CertOtherCustomers, Me.State.isItemsGridVisible)
            Else
                Me.CustomerCount.Text = "(0)"
            End If

        End Sub
#End Region
#Region "Coverage Datagrid Related"

        Public Sub PopulateCoveragesGrid()
            'Me.GetSelectedItem(moDealerDrop)

            Dim dv As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(Me.State.MyBO.Id)
            'Dim dv1 As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetCurrentProductCodeCoverages(Me.State.MyBO.Id)
            Dim Row As DataRowView

            ' Check if the Coverages Data is already fetched from the DB, else get the data from the DB 
            If (Me.State.CoveragesearchDV Is Nothing) Or Me.State.blnMFGChanged Then
                Me.State.CoveragesearchDV = CertItemCoverage.GetCurrentProductCodeCoverages(Me.State.MyBO.Id)
            End If


            Dim CertItemCoverageIds As String = String.Empty


            'If dv1.Count > 0 Then

            'For Each Row In dv1
            If Me.State.CoveragesearchDV.Count > 0 Then
                For Each Row In Me.State.CoveragesearchDV

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
                Me.Grid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(dv, Me.State.TheItemCoverageState.selectedItemId, Me.Grid, Me.State.TheItemCoverageState.PageIndex)

                Me.State.TheItemCoverageState.PageIndex = Me.Grid.CurrentPageIndex
                Me.Grid.DataSource = Me.State.CoveragesearchDV
                Me.State.PageIndex = Me.Grid.CurrentPageIndex

                If (Not Me.State.CoverageSortExpression.Equals(String.Empty)) Then
                    Me.State.CoveragesearchDV.Sort = Me.State.CoverageSortExpression

                End If

                HighLightSortColumn(Me.Grid, Me.State.CoverageSortExpression, Me.IsNewUI)

                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                If Me.IsSinglePremium Then
                    Me.Grid.Columns(GRID_COL_SEQUENCE_IDX).Visible = False
                Else
                    Me.Grid.Columns(GRID_COL_SEQUENCE_IDX).Visible = True
                End If

                If IsExtendCovByAutoRenew Then
                    Me.Grid.Columns(GRID_COL_NO_OF_RENEWALS_IDX).Visible = True
                    Me.Grid.Columns(GRID_COL_RENEWAL_DATE_IDX).Visible = True
                    Me.Grid.Columns(GRID_COL_COVERAGE_DURATION_IDX).Visible = True
                Else
                    Me.Grid.Columns(GRID_COL_NO_OF_RENEWALS_IDX).Visible = False
                    Me.Grid.Columns(GRID_COL_RENEWAL_DATE_IDX).Visible = False
                    Me.Grid.Columns(GRID_COL_COVERAGE_DURATION_IDX).Visible = False
                End If

                If dv.Count = 0 Then
                    Throw New DataNotFoundException(NO_COVERAGE_FOUND)
                End If

                'Check if there are expired coverages to enable coverage history tab
                Me.State.TheItemCoverageState.CoverageHistoryDV = New DataView(dv.Table)
                If Not CertItemCoverageIds.Equals(String.Empty) Then
                    Me.State.TheItemCoverageState.CoverageHistoryDV.RowFilter = " cert_item_coverage_id_hex not in (" & CertItemCoverageIds & ")"
                End If
                If Me.State.TheItemCoverageState.CoverageHistoryDV.Count > 0 Then
                    ExpiredCoveragesExist = True
                End If

                'PM 2/14/2006 begin
                For Each cov As CertItemCoverage In Me.State.MyBO.AssociatedItemCoverages
                    If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                        Me.State.TheItemCoverageState.manufaturerWarranty = True
                    End If
                Next

                'PM 2/14/2006 end

                Dim tempDV As DataView = New DataView(dv.Table)
                tempDV.RowFilter = "end_date >= '" & todayDate.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture) & "'"
                If tempDV.Count <= 0 Then
                    allCoveragesExpired = True
                Else
                    allCoveragesExpired = False
                End If

            Catch ex As DataNotFoundException
                Me.MasterPage.MessageController.AddError(ex.Message)
                Me.State.IsEdit = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
            Try
                If Me.State.CoverageSortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.CoverageSortExpression.EndsWith(" DESC") Then
                        Me.State.CoverageSortExpression = e.SortExpression
                    Else
                        Me.State.CoverageSortExpression &= " DESC"
                    End If
                Else
                    Me.State.CoverageSortExpression = e.SortExpression
                End If
                Me.State.TheItemCoverageState.selectedItemId = Nothing
                Me.State.PageIndex = 0
                Me.PopulateCoveragesGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Grid.ItemCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    Me.State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    'arf 12-20-04 'Me.callPage(CertItemForm.URL, Me.State.TheItemCoverageState.selectedItemId)
                    'arf 12-20-04 begin
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.TheItemCoverageState.selectedItemId
                    Me.NavController.Navigate(Me, "coverage_selected", New CertItemForm.Parameters("FromLink"))
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub


        private function CoverageExpirationDate(ByVal coverageBeginDate as Date, ByVal maxRenewalDuration as Integer) as Date?
            dim expirationDate as Date = DateAdd(DateInterval.Month, maxRenewalDuration, coverageBeginDate)
            return DateAdd(DateInterval.Day, -1, expirationDate)
        End function

        private function NumberOfRenewalsRemaining(ByVal renewalDuration as Integer, ByVal coverageDuration as Integer, ByVal numberOfRenewals As Integer) As Integer
            Dim renewalsRemaining as Integer = renewalDuration/coverageDuration - (numberOfRenewals + 1) '+1 for initial registration

            if renewalsRemaining < 0 then
                'potential data issue - should we log an 'exception'?
                renewalsRemaining = 0
            End If

            return renewalsRemaining
        End function

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditCoverage As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Dim maxRenewalDuration as String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_MAX_RENEWAL_DURATION).ToString
                    Dim coverageBeginDate as Date = CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date)
                    Dim coverageDuration as String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_COVERAGE_DURATION).ToString
                    Dim numberOfRenewals as String = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_NO_OF_RENEWALS).ToString

                    e.Item.Cells(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString
                    e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).Text = Me.GetDateFormattedStringNullable(coverageBeginDate)
                    e.Item.Cells(Me.GRID_COL_END_DATE_IDX).Text = Me.GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    e.Item.Cells(Me.GRID_COL_SEQUENCE_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_SEQUENCE).ToString

                    e.Item.Cells(Me.GRID_COL_COVERAGE_DURATION_IDX).Text = coverageDuration
                    e.Item.Cells(Me.GRID_COL_NO_OF_RENEWALS_IDX).Text = numberOfRenewals.ToInteger()

                    If (Not String.IsNullOrWhiteSpace(maxRenewalDuration)) then
                        e.Item.Cells(Me.GRID_COL_MAX_RENEWAL_DURATION_IDX).Text = maxRenewalDuration
                        Dim renewalDuration as Integer = maxRenewalDuration.ToInteger()
                        If (0 < renewalDuration) Then
                            e.Item.Cells(Me.GRID_COL_COVERAGE_EXPIRATION_DATE_IDX).Text = Me.GetDateFormattedStringNullable(CoverageExpirationDate(coverageBeginDate, renewalDuration))
                        End If
                        Dim coverDuration as Integer = coverageDuration.ToInteger()
                        If (0 < coverDuration) Then
                            e.Item.Cells(Me.GRID_COL_NO_OF_RENEWALS_REMAINING_IDX).Text = NumberOfRenewalsRemaining(renewalDuration, coverDuration, numberOfRenewals.ToInteger()).ToString
                        End If
                    End If

                    If Not Convert.IsDBNull(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_RENEWAL_DATE)) Then
                        e.Item.Cells(Me.GRID_COL_RENEWAL_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_RENEWAL_DATE), Date))
                    End If
                    e.Item.Cells(Me.GRID_COL_COVERAGE_TOTAL_PAID_AMOUNT_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TOTAL_PAID_AMOUNT).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_REMAIN_LIABILITY_LIMIT_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_REMAIN_LIABILITY_LIMIT).ToString

                    If (Not e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL) Is Nothing) Then
                        btnEditCoverage = CType(e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL), LinkButton)
                        btnEditCoverage.Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        btnEditCoverage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte()))
                        btnEditCoverage.CommandName = SELECT_ACTION_COMMAND
                    End If

                    If Me.IsEffectiveCoverage(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date), CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date)) Then
                        e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).CssClass = "StatActive"
                        e.Item.Cells(Me.GRID_COL_END_DATE_IDX).CssClass = "StatActive"
                    Else
                        e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).CssClass = "StatInactive"
                        e.Item.Cells(Me.GRID_COL_END_DATE_IDX).CssClass = "StatInactive"
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                Me.State.TheItemCoverageState.PageIndex = e.NewPageIndex
                Me.State.TheItemCoverageState.selectedItemId = Guid.Empty
                Me.PopulateCoveragesGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Coverage History Datagrid Related"

        Public Sub PopulateCoveragesHistoryGrid()

            Dim todayDate As Date

            Try
                Me.State.TheItemCoverageState.CoverageHistoryDV.Sort = CoverageHistoryGrid.DataMember
                Me.CoverageHistoryGrid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(Me.State.TheItemCoverageState.CoverageHistoryDV, Me.State.TheItemCoverageState.selectedItemId, Me.CoverageHistoryGrid, Me.State.TheItemCoverageState.PageIndex)
                Me.State.TheItemCoverageState.PageIndex = Me.CoverageHistoryGrid.CurrentPageIndex
                If Me.State.TheItemCoverageState.CoverageHistoryDV.Count = 0 Then
                    Throw New DataNotFoundException(NO_COVERAGE_FOUND)
                End If

                Me.CoverageHistoryGrid.DataSource = Me.State.TheItemCoverageState.CoverageHistoryDV
                Me.CoverageHistoryGrid.DataBind()

                ControlMgr.SetVisibleControl(Me, CoverageHistoryGrid, Me.State.TheItemCoverageState.IsGridVisible)

                If Me.IsSinglePremium Then
                    Me.CoverageHistoryGrid.Columns(GRID_COL_SEQUENCE_IDX).Visible = False
                Else
                    Me.CoverageHistoryGrid.Columns(GRID_COL_SEQUENCE_IDX).Visible = True
                End If

            Catch ex As DataNotFoundException
                Me.MasterPage.MessageController.AddError(ex.Message)
                Me.State.IsEdit = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Sub CoverageHistoryGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles CoverageHistoryGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    'Me.State.MyBO.SelectedCoverageId = Me.State.TheItemCoverageState.selectedItemId
                    'Me.callPage(CertItemForm.URL, Me.State.MyBO)
                    'arf 12-20-04 'Me.callPage(CertItemForm.URL, Me.State.TheItemCoverageState.selectedItemId)
                    'arf 12-20-04 begin
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.TheItemCoverageState.selectedItemId
                    Me.NavController.Navigate(Me, "coverage_selected", New CertItemForm.Parameters("FromLink"))
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CoverageHistoryGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CoverageHistoryGrid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub CoverageHistoryGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CoverageHistoryGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditCoverage As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString
                    e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).Text = Me.GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                    e.Item.Cells(Me.GRID_COL_END_DATE_IDX).Text = Me.GetDateFormattedStringNullable(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    e.Item.Cells(Me.GRID_COL_SEQUENCE_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_SEQUENCE).ToString
                    If (Not e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL) Is Nothing) Then
                        btnEditCoverage = CType(e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).FindControl(GRID_COL_COVERAGE_TYPE_DESCRIPTION_CTRL), LinkButton)
                        btnEditCoverage.Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        btnEditCoverage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte()))
                        btnEditCoverage.CommandName = SELECT_ACTION_COMMAND
                    End If

                    If Me.IsEffectiveCoverage(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date), CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date)) Then
                        e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).CssClass = "StatActive"
                        e.Item.Cells(Me.GRID_COL_END_DATE_IDX).CssClass = "StatActive"
                    Else
                        e.Item.Cells(Me.GRID_COL_BEGIN_DATE_IDX).CssClass = "StatInactive"
                        e.Item.Cells(Me.GRID_COL_END_DATE_IDX).CssClass = "StatInactive"
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CoverageHistoryGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CoverageHistoryGrid.PageIndexChanged

            Try
                Me.State.TheItemCoverageState.PageIndex = e.NewPageIndex
                Me.State.TheItemCoverageState.selectedItemId = Guid.Empty
                Me.PopulateCoveragesHistoryGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Claims Datagrid Related"

        Public Sub PopulateClaimsGrid()

            'Me.GetSelectedItem(moDealerDrop)

            ' Check if the Claims Data is already fetched from the DB, else get the data from the DB 
            ' Dim dv As Certificate.CertificateClaimsDV = Me.State.MyBO.ClaimsForCertificate(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If (Me.State.ClaimsearchDV Is Nothing) Then
                Me.State.ClaimsearchDV = Me.State.MyBO.ClaimsForCertificate(Me.State.CertificateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            Me.moClaimsDatagrid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.ClaimsearchDV, Me.State.selectedClaimItemId, Me.moClaimsDatagrid, Me.State.PageIndexClaimsGrid)
            Me.State.PageIndexClaimsGrid = Me.moClaimsDatagrid.CurrentPageIndex
            Me.moClaimsDatagrid.DataSource = Me.State.ClaimsearchDV
            Me.State.PageIndex = Me.moClaimsDatagrid.CurrentPageIndex
            If (Not Me.State.ClaimsSortExpression.Equals(String.Empty)) Then
                Me.State.ClaimsearchDV.Sort = Me.State.ClaimsSortExpression
            End If

            HighLightSortColumn(Me.moClaimsDatagrid, Me.State.ClaimsSortExpression, Me.IsNewUI)

            Me.moClaimsDatagrid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        End Sub


        Private Sub moClaimsDatagrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moClaimsDatagrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.selectedClaimItemId = New Guid(CType(e.CommandArgument, String))
                    ''Me.State.selectedClaimItemId = New Guid(e.Item.Cells(Me.GRID_COL_EDIT_CLAIM_IDX).Text)
                    Me.State.MyBO.SelectedCoverageId = Me.State.TheItemCoverageState.selectedItemId
                    Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.selectedClaimItemId)
                    If claimBo.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                        If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                            Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                            Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                        Else
                            Me.NavController = Nothing
                            Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, claimBo))
                        End If

                    Else
                        'Save the Current Flow NavCtrl
                        '  CType(MyBase.State, BaseState).NavCtrl = Me.NavController
                        Me.callPage(ClaimForm.URL, Me.State.selectedClaimItemId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
                   (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moClaimsDatagrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moClaimsDatagrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditClaim As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (Not e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) Is Nothing) Then
                        btnEditClaim = CType(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                        btnEditClaim.Text = dvRow(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER).ToString
                        btnEditClaim.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Certificate.CertificateClaimsDV.COL_CLAIM_ID), Byte()))
                        btnEditClaim.CommandName = SELECT_ACTION_COMMAND
                    End If

                    e.Item.Cells(Me.GRID_COL_CREATED_DATE).Text = Me.GetDateFormattedStringNullable(CType(dvRow(Certificate.CertificateClaimsDV.COL_CREATED_DATE), Date))
                    e.Item.Cells(Me.GRID_COL_STATUS_CODE).Text = dvRow(Certificate.CertificateClaimsDV.COL_STATUS_CODE).ToString

                    If Not dvRow.Row.IsNull(Certificate.CertificateClaimsDV.COL_AUTHORIZED_AMOUNT) Then
                        e.Item.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT).Text = Me.GetAmountFormattedString(CType(dvRow(Certificate.CertificateClaimsDV.COL_AUTHORIZED_AMOUNT), Decimal))
                    Else
                        e.Item.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT).Text = Me.GetAmountFormattedString(0)
                    End If

                    If Not dvRow.Row.IsNull(Certificate.CertificateClaimsDV.COL_TOTAL_PAID) Then
                        e.Item.Cells(Me.GRID_COL_TOTAL_PAID).Text = Me.GetAmountFormattedString(CType(dvRow(Certificate.CertificateClaimsDV.COL_TOTAL_PAID), Decimal))
                    Else
                        e.Item.Cells(Me.GRID_COL_TOTAL_PAID).Text = Me.GetAmountFormattedString(0)
                    End If

                    e.Item.Cells(Me.GRID_COL_EXTENDED_STATUS).Text = dvRow(Certificate.CertificateClaimsDV.COL_EXTENDED_STATUS).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moClaimsDatagrid.PageIndexChanged

            Try
                Me.State.PageIndexClaimsGrid = e.NewPageIndex
                Me.State.selectedClaimItemId = Guid.Empty
                Me.PopulateClaimsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moClaimsDatagrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moClaimsDatagrid.SortCommand
            Try
                If Me.State.ClaimsSortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.ClaimsSortExpression.EndsWith(" DESC") Then
                        Me.State.ClaimsSortExpression = e.SortExpression
                    Else
                        Me.State.ClaimsSortExpression &= " DESC"
                    End If
                Else
                    Me.State.ClaimsSortExpression = e.SortExpression
                End If
                Me.State.selectedClaimItemId = Nothing
                Me.State.PageIndex = 0
                Me.PopulateClaimsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Items Datagrid Related"

        Public Sub PopulateItemsGrid()
            Dim blnAddItemAllowed, blnAddItemAfterExpired As Boolean
            Dim i As Integer, blnCoveragesExpired As Boolean
            Dim endDate, todayDate As Date
            Dim cert As Certificate = Me.State.MyBO
            Dim dv As CertItem.CertItemSearchDV = CertItem.GetItems(Me.State.MyBO.Id)

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

            Dim dvItemCov As DataView = CertItemCoverage.GetItemCoverages(Me.State.MyBO.Id)
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
                If Me.State.MyBO.TheCertCancellationBO.getCancellationReasonCode = CANCEL_REASON_EXP Then
                    ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, True)
                End If
            End If

            If dv.Count < 1 Then
                Me.State.isItemsGridVisible = False
                'Me.EnableDisableTabs(False)
                Exit Sub
            End If
            'dv.Sort = CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE
            Dim ItemsRow As DataRow = dv.Table.Rows(0)
            Me.ItemsGrid.AutoGenerateColumns = False
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_ITEM_NUMBER_IDX).SortExpression = CertItem.CertItemSearchDV.COL_ITEM_NUMBER
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).SortExpression = CertItem.CertItemSearchDV.COL_RISK_TYPE
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_ITEM_DESCIPTION_IDX).SortExpression = CertItem.CertItemSearchDV.COL_ITEM_DESCRIPTION
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_MAKE_IDX).SortExpression = CertItem.CertItemSearchDV.COL_MAKE
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_MODEL_IDX).SortExpression = CertItem.CertItemSearchDV.COL_MODEL
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_EXPIRATION_DATE_IDX).SortExpression = CertItem.CertItemSearchDV.COL_EXPIRATION_DATE
            Me.ItemsGrid.Columns(Me.ITEMS_GRID_COL_BENEFIT_STATUS_IDX).SortExpression = CertItem.CertItemSearchDV.COL_BENEFIT_STATUS

            Me.ItemsGrid.EditIndex = -1
            'SetPageAndSelectedIndexFromGuid(EndorseDV, Me.State.SelectedCommentId, Me.EndorsementsGrid, Me.State.EndorsementsPageIndex)
            Me.ItemsGrid.PageIndex = Me.State.ItemsPageIndex
            Me.ItemsGrid.DataSource = dv
            Me.ItemsGrid.DataBind()

            'Dispaly benefit status column only if the flag is turned on
            If (Not String.IsNullOrWhiteSpace(Me.State.MyBO.Product.BenefitEligibleFlagXCD) AndAlso Me.State.MyBO.Product.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y) Then
                Me.ItemsGrid.Columns(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Visible = True
            Else
                Me.ItemsGrid.Columns(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Visible = False
            End If

            ' Original retail price captured at cert item level. Populated in certificate generl tab working on the assumption that
            ' wireless customers using this field will have only one cert item -- VS

            If Not (ItemsRow.Item(CertItem.CertItemSearchDV.COL_ORIGINAL_RETAIL_PRICE).Equals(DBNull.Value)) And dv.Count = 1 Then
                Me.PopulateControlFromBOProperty(Me.moOriginalRetailPriceText, CType(ItemsRow.Item(CertItem.CertItemSearchDV.COL_ORIGINAL_RETAIL_PRICE), Decimal), Me.DECIMAL_FORMAT)
            Else
                Me.PopulateControlFromBOProperty(Me.moOriginalRetailPriceText, Nothing)
            End If

            ControlMgr.SetVisibleControl(Me, ItemsGrid, Me.State.isItemsGridVisible)

        End Sub


        Public Sub ItemsGrid_ItemCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles ItemsGrid.RowCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.TheItemsState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = Me.State.TheItemsState.selectedItemId
                    Me.State.NavigateToItems = True
                    Me.NavController.Navigate(Me, "item_selected")
                    'arf 12-20-04 end
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemsGrid_ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ItemsGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub ItemsGrid_ItemDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ItemsGrid.RowDataBound
            Dim btnEditItem As LinkButton
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.ITEMS_GRID_COL_ITEM_NUMBER_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_ITEM_NUMBER).ToString
                    If (Not e.Row.Cells(Me.ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).FindControl(ITEMS_GRID_COL_EDIT_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Row.Cells(Me.ITEMS_GRID_COL_RISK_TYPE_DESCRIPTION_IDX).FindControl(ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItem.CertItemSearchDV.COL_CERT_ITEM_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(CertItem.CertItemSearchDV.COL_RISK_TYPE).ToString
                    End If
                    e.Row.Cells(Me.ITEMS_GRID_COL_ITEM_DESCIPTION_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_ITEM_DESCRIPTION).ToString
                    e.Row.Cells(Me.ITEMS_GRID_COL_MAKE_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_MAKE).ToString
                    e.Row.Cells(Me.ITEMS_GRID_COL_MODEL_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_MODEL).ToString
                    If (Not dvRow(CertItem.CertItemSearchDV.COL_EXPIRATION_DATE) Is DBNull.Value) Then
                        e.Row.Cells(Me.ITEMS_GRID_COL_EXPIRATION_DATE_IDX).Text = Me.GetDateFormattedStringNullable(CType(dvRow(CertItem.CertItemSearchDV.COL_EXPIRATION_DATE), Date))
                    End If
                    e.Row.Cells(ITEMS_GRID_COL_BENEFIT_STATUS_IDX).Text = dvRow(CertItem.CertItemSearchDV.COL_BENEFIT_STATUS).ToString()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ItemsGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ItemsGrid.PageIndexChanging
            Try
                Me.State.TheItemCoverageState.PageIndex = e.NewPageIndex
                Me.State.TheItemCoverageState.selectedItemId = Guid.Empty
                Me.PopulateItemsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Registered Items Datagrid Related"
        Public Sub PopulateRegisterItemsGrid()
            Dim cert As Certificate = Me.State.MyBO
            Dim dv As CertItem.CertRegItemSearchDV = CertItem.GetRegisteredItems(Me.State.MyBO.Id)

            ControlMgr.SetVisibleControl(Me, btnNewCertItem_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, False)
            If (cert.Product.AllowRegisteredItems = Codes.EXT_YESNO_Y And cert.StatusCode = CERT_STATUS) Then
                ControlMgr.SetVisibleControl(Me, btnNewCertRegItem_WRITE, True)
            End If

            If dv.Count < 1 Then
                Me.State.isRegItemsGridVisible = False
                Exit Sub
            End If

            Dim ItemsRow As DataRow = dv.Table.Rows(0)
            Me.RegisteredItemsGrid.AutoGenerateColumns = False
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_REGISTERED_ITEM_NAME
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_DEVICE_TYPE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_DEVICE_TYPE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_ITEM_DESCRIPTION_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_ITEM_DESCRIPTION
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_MAKE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_MAKE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_MODEL_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_MODEL
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_PURCHASE_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_PURCHASE_PRICE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_PURCHASE_PRICE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_SERIAL_NUMBER_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_SERIAL_NUMBER
            'REQ-6002
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_REGISTRATION_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_RETAIL_PRICE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_EXPIRATION_DATE_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE
            Me.RegisteredItemsGrid.Columns(REG_ITEMS_COL_ITEM_STATUS_IDX).SortExpression = CertItem.CertRegItemSearchDV.COL_ITEM_STATUS

            Me.RegisteredItemsGrid.EditIndex = -1
            Me.RegisteredItemsGrid.PageIndex = Me.State.ItemsPageIndex
            Me.RegisteredItemsGrid.DataSource = dv
            Me.RegisteredItemsGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, RegisteredItemsGrid, Me.State.isRegItemsGridVisible)

        End Sub

        Public Sub RegisteredItemsGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RegisteredItemsGrid.RowCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.TheItemsState.selectedRegisteredItemId = New Guid(e.CommandArgument.ToString())
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERT_REGISTERED_ITEM_ID) = Me.State.TheItemsState.selectedRegisteredItemId
                    Me.State.NavigateToItems = True
                    Me.NavController.Navigate(Me, "register_item_selected")

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Public Sub RegisteredItemsGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RegisteredItemsGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub
        Public Sub RegisteredItemsGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RegisteredItemsGrid.RowDataBound
            Dim btnRegEditItem As LinkButton
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (Not e.Row.Cells(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).FindControl(REG_ITEMS_GRID_COL_EDIT_CTRL) Is Nothing) Then
                        btnRegEditItem = CType(e.Row.Cells(REG_ITEMS_COL_REGISTERED_ITEM_NAME_IDX).FindControl(REG_ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnRegEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertItem.CertRegItemSearchDV.COL_CERT_REGISTERED_ITEM_ID), Byte()))
                        btnRegEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnRegEditItem.Text = dvRow(CertItem.CertRegItemSearchDV.COL_REGISTERED_ITEM_NAME).ToString
                    End If
                    e.Row.Cells(REG_ITEMS_COL_DEVICE_TYPE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_DEVICE_TYPE).ToString
                    e.Row.Cells(REG_ITEMS_COL_ITEM_DESCRIPTION_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_DESCRIPTION).ToString
                    e.Row.Cells(REG_ITEMS_COL_MAKE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_MAKE).ToString
                    e.Row.Cells(REG_ITEMS_COL_MODEL_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_MODEL).ToString
                    If (Not dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE) Is DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_PURCHASE_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_DATE), Date))
                    End If
                    e.Row.Cells(REG_ITEMS_COL_PURCHASE_PRICE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_PURCHASE_PRICE).ToString
                    e.Row.Cells(REG_ITEMS_COL_SERIAL_NUMBER_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_SERIAL_NUMBER).ToString
                    'REQ-6002
                    If (Not dvRow(CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE) Is DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_REGISTRATION_DATE_IDX).Text = GetDateFormattedStringNullable(CType(dvRow(CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE), Date))
                    End If
                    If (Not dvRow(CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE) Is DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_RETAIL_PRICE_IDX).Text = GetAmountFormattedString(CType(dvRow(CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE), Decimal))
                    End If
                    If (Not dvRow(CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE) Is DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_EXPIRATION_DATE_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE)
                    End If
                    If (Not dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_STATUS) Is DBNull.Value) Then
                        e.Row.Cells(REG_ITEMS_COL_ITEM_STATUS_IDX).Text = dvRow(CertItem.CertRegItemSearchDV.COL_ITEM_STATUS)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RegisteredItemsGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RegisteredItemsGrid.PageIndexChanging
            Try
                Me.State.TheItemCoverageState.PageIndex = e.NewPageIndex
                Me.State.TheItemCoverageState.selectedItemId = Guid.Empty
                Me.PopulateRegisterItemsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CheckIfComingFromItems()
            If Me.State.NavigateToItems Then
                'If tsHoriz.Items(CERT_ITEMS_INFO_TAB).Enabled = True Then Me.tsHoriz.SelectedIndex = CERT_ITEMS_INFO_TAB
                If isTabEnabled(CERT_ITEMS_INFO_TAB) = True Then Me.State.selectedTab = CERT_ITEMS_INFO_TAB

                Me.State.NavigateToItems = False
            End If

        End Sub
#End Region

#Region "Installment History Datagrid Related"
        Public Sub PopulateInstallmentHistoryGrid()

            Try
                If (Me.State.CertInstallmentHistoryDV Is Nothing) Then
                    Me.State.CertInstallmentHistoryDV = Certificate.GetCertInstallmentHistoryInfo(Me.State.MyBO.Id)
                End If

                Me.CertInstallmentHistoryGrid.PageSize = Me.State.PageSize
                If Me.State.CertInstallmentHistoryDV.Count > 0 Then
                    Me.State.CertInstallmentHistoryDV.Sort = Me.State.CertInstallmentHistorySortExpression

                    SetPageAndSelectedIndexFromGuid(Me.State.CertInstallmentHistoryDV, Guid.Empty, Me.CertInstallmentHistoryGrid, Me.State.PageIndex)
                    Me.CertInstallmentHistoryGrid.DataSource = Me.State.CertInstallmentHistoryDV
                    Me.State.PageIndex = Me.CertInstallmentHistoryGrid.PageIndex

                    HighLightSortColumn(Me.CertInstallmentHistoryGrid, Me.State.CertInstallmentHistorySortExpression, Me.IsNewUI)
                    Me.CertInstallmentHistoryGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertInstallmentHistoryGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.CertInstallmentHistoryGrid.Visible)
                    Session("recCount") = Me.State.CertInstallmentHistoryDV.Count
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), True)
                    EnableTab(CERT_REPRICE_TAB, True)
                End If
            Catch ex As Exception

            End Try

        End Sub


#End Region

#Region "Comments Datagrid Related "

        'The Binding LOgic is here  
        Private Sub CommentsGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CommentsGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (Not e.Item.Cells(Me.GRID_COL_TIME_STAMP).FindControl(COMMENT_GRID_COL_EDIT_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Item.Cells(Me.GRID_COL_TIME_STAMP).FindControl(ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Comment.CommentSearchDV.COL_COMMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        Dim createdDate As Date = CType(dvRow(Comment.CommentSearchDV.COL_CREATED_DATE), Date)
                        btnEditItem.Text = GetLongDateFormattedString(createdDate)
                    End If
                    e.Item.Cells(Me.GRID_COL_CALLER_NAME).Text = dvRow(Comment.CommentSearchDV.COL_CALLER_NAME).ToString
                    e.Item.Cells(Me.GRID_COL_ADDED_BY).Text = dvRow(Comment.CommentSearchDV.COL_ADDED_BY).ToString
                    e.Item.Cells(Me.GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CommentsGrid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CommentsGrid.SortCommand
            Try
                If Me.State.CommentsSortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.CommentsSortExpression.EndsWith(" DESC") Then
                        Me.State.CommentsSortExpression = e.SortExpression
                    Else
                        Me.State.CommentsSortExpression &= " DESC"
                    End If
                Else
                    Me.State.CommentsSortExpression = e.SortExpression
                End If
                Me.State.SelectedCommentId = Nothing
                Me.State.PageIndex = 0
                Me.PopulateCommentsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub CommentsGrid_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles CommentsGrid.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.SelectedCommentId = New Guid(e.CommandArgument.ToString())
                    Dim originalComment As Comment = New Comment(Me.State.SelectedCommentId)
                    Dim newComment As Comment = Comment.GetNewComment(originalComment)
                    Me.State.NavigateToComment = True
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(newComment))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub CommentsGrid_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CommentsGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CommentsGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CommentsGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                Me.State.CommentsPageIndex = e.NewPageIndex
                Me.State.SelectedCommentId = Guid.Empty
                Me.PopulateCommentsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateCommentsGrid()
            Dim cert As Certificate = Me.State.MyBO
            Dim dv As Comment.CommentSearchDV = Comment.getList(Me.State.MyBO.Id)
            dv.Sort = Me.State.CommentsSortExpression

            Me.CommentsGrid.AutoGenerateColumns = False
            Me.CommentsGrid.Columns(Me.GRID_COL_ADDED_BY).SortExpression = Comment.CommentSearchDV.COL_ADDED_BY
            Me.CommentsGrid.Columns(Me.GRID_COL_CALLER_NAME).SortExpression = Comment.CommentSearchDV.COL_CALLER_NAME
            Me.CommentsGrid.Columns(Me.GRID_COL_TIME_STAMP).SortExpression = Comment.CommentSearchDV.COL_CREATED_DATE
            Me.CommentsGrid.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedCommentId, Me.CommentsGrid, Me.State.CommentsPageIndex)
            Me.State.CommentsPageIndex = Me.CommentsGrid.CurrentPageIndex
            Me.CommentsGrid.DataSource = dv
            Me.CommentsGrid.DataBind()

            'Me.tsHoriz.Items(Me.CERT_COMMENTS_TAB).Text = TranslationBase.TranslateLabelOrMessage("COMMENTS") & " : " & dv.Count.ToString()
            lblTabCommentHeader.Text = TranslationBase.TranslateLabelOrMessage("COMMENTS") & " : " & dv.Count.ToString()

            ControlMgr.SetVisibleControl(Me, CommentsGrid, Me.State.IsCommentsGridVisible)
        End Sub
#End Region

#Region "Data Protection Datagrid Related "

        'Restrict functionality
        Public Sub PopulateDataProtection()
            Try
                Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(Me.State.MyBO.Id)
                dv.Sort = Me.State.CommentsSortExpression
                Me.GridDataProtection.AutoGenerateColumns = False
                Me.GridDataProtection.DataSource = dv
                Me.GridDataProtection.DataBind()
                AddGridLabelDecorations()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnRestrict_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestrict.Click
            Try
                Me.State.DataProtectionHistBO = New DataProtectionHistory()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                With Me.State.DataProtectionHistBO
                    Me.State.DataProtectionHistBO.RestrictedStatus = RestrictCode
                End With
                AddNewRowToGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Try
                If (Me.State.DataProtectionHistBO.RestrictedStatus = RestrictCode Or Me.State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    Me.State.DBCommentBO = Me.State.DataProtectionHistBO.AddComments(Guid.Empty)
                    Me.State.DBCertificateBO = Me.State.DataProtectionHistBO.AddCertificate(Me.State.MyBO.Id)
                ElseIf (Me.State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    Me.State.DBCommentBO = Me.State.CertForgotBO.AddComment(Guid.Empty)
                    Me.State.DBCertificateBO = Me.State.CertForgotBO.AddCertificate(Me.State.MyBO.Id)
                End If
                If (Me.State.DataProtectionHistBO.RestrictedStatus = RestrictCode) Then
                    'Save for Restriction
                    PopulateDataProtectionBOsFormFrom(RestrictCode)
                    If (ValidateDataProtectionFields(RestrictCode)) Then
                        Me.State.DBCommentBO.Validate()
                        Me.State.DataProtectionHistBO.Save()
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
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
                        moCertificateInfoController = Me.UserCertificateCtr
                        moCertificateInfoController.InitController(Me.State.MyBO.Id, , Me.State.companyCode)
                    End If
                ElseIf (Me.State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    'Save for UnRestriction
                    PopulateDataProtectionBOsFormFrom(UnRestrictCode)
                    If (ValidateDataProtectionFields(UnRestrictCode)) Then
                        Me.State.DBCommentBO.Validate()
                        Me.State.DataProtectionHistBO.Save()
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                        ControlMgr.SetVisibleControl(Me, btnNewClaim, True)
                        ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                        ControlMgr.SetVisibleControl(Me, btnCancel, False)
                        ControlMgr.SetVisibleControl(Me, btnSave, False)
                        ControlMgr.SetVisibleControl(Me, btnCancelRequestEdit_WRITE, True)
                        If Me.State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                            ControlMgr.SetVisibleControl(Me, btnRightToForgotten, True)
                        End If
                        GridDataProtection.ShowFooter = False
                        PopulateDataProtection()
                        ClearStateProperties()
                        ' To refresh user control 'Restricted' label
                        moCertificateInfoController = Me.UserCertificateCtr
                        moCertificateInfoController.InitController(Me.State.MyBO.Id, , Me.State.companyCode)
                    End If
                ElseIf (Me.State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    PopulateDataProtectionBOsFormFrom(ForgottenCode)
                    If (ValidateDataProtectionFields(ForgottenCode)) Then
                        Me.State.DBCommentBO.Validate()
                        Me.State.CertForgotBO.Validate()
                        Me.DisplayMessage(Message.MSG_PROMPT_FOR_FORGOTTEN, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSavePromptResponse)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Try
                If (Not Me.State.DataProtectionHistBO Is Nothing AndAlso Me.State.DataProtectionHistBO.RestrictedStatus = RestrictCode) Then
                    ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)

                ElseIf (Not Me.State.DataProtectionHistBO Is Nothing AndAlso Me.State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode) Then
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnRestrict, False)

                ElseIf (Not Me.State.CertForgotBO Is Nothing AndAlso Not Me.State.MyBO.CertificateIsRestricted AndAlso Me.State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    ControlMgr.SetVisibleControl(Me, btnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)

                ElseIf (Not Me.State.CertForgotBO Is Nothing AndAlso Me.State.CertForgotBO.RestrictedStatus = ForgottenCode) Then
                    ControlMgr.SetVisibleControl(Me, btnUnRestrict, True)
                    ControlMgr.SetVisibleControl(Me, btnRestrict, False)

                End If
                If Me.State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Not Me.State.MyBO.CertificateIsRestricted Then
                    ControlMgr.SetVisibleControl(Me, btnRightToForgotten, True)
                Else
                    ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                End If


                ControlMgr.SetVisibleControl(Me, btnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnSave, False)

                If (Not Me.State.CertForgotBO Is Nothing) Then
                    With Me.State.CertForgotBO
                        Me.State.CertForgotBO.RestrictedStatus = String.Empty
                    End With
                End If

                If (Not Me.State.DataProtectionHistBO Is Nothing) Then
                    With Me.State.DataProtectionHistBO
                        Me.State.DataProtectionHistBO.RestrictedStatus = String.Empty
                    End With
                End If

                GridDataProtection.ShowFooter = False
                PopulateDataProtection()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub GridDataProtection_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridDataProtection.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If (e.Row.RowType = DataControlRowType.DataRow) Then
                    e.Row.Cells(Me.GDP_COL_REQUEST_ID).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_REQUEST_ID).ToString
                    e.Row.Cells(Me.GDP_COL_COMMENTS).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_COMMENTS).ToString
                    e.Row.Cells(Me.GDP_COL_ADDED_BY).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_ADDED_BY).ToString
                    e.Row.Cells(Me.GDP_COL_TIME_STAMP).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_CREATED_DATE).ToString
                    e.Row.Cells(Me.GDP_COL_STATUS).Text = dvRow(DataProtectionHistory.DataProtectionCommentDV.COL_STATUS).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNewRowToGrid()
            Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(Me.State.MyBO.Id)
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

        Protected Sub PopulateDataProtectionBOsFormFrom(ByVal actionCode As String)
            Try
                Dim ind As Integer = GridDataProtection.EditIndex
                Dim commentTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCommentTypeLookupList(Authentication.LangId), CommentCode)
                Dim restrictionId As Guid = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(RestrictionListCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), actionCode)

                If (actionCode.Equals("R") Or actionCode.Equals("UR")) Then
                    'Logic For Restriction And  UnRestriction '

                    With Me.State.DBCommentBO
                        Me.PopulateBOProperty(Me.State.DBCommentBO, "Comments", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_COMMENT), TextBox))
                        Me.PopulateBOProperty(Me.State.DBCommentBO, "CommentTypeId", commentTypeId)
                        Me.State.DBCommentBO.CertId = Me.State.MyBO.Id
                        Me.State.DBCommentBO.CallerName = CallerName
                    End With

                    With Me.State.DBCertificateBO
                        If (actionCode.Equals("R")) Then
                            Me.State.DBCertificateBO.IsRestricted = "Y"
                        ElseIf (actionCode.Equals("UR")) Then
                            Me.State.DBCertificateBO.IsRestricted = "N"
                        End If

                    End With
                    With Me.State.DataProtectionHistBO
                        Me.PopulateBOProperty(Me.State.DataProtectionHistBO, "RequestId", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_REQUEST_ID), TextBox))
                        Me.State.DataProtectionHistBO.EntityId = Me.State.MyBO.Id
                        Me.State.DataProtectionHistBO.CommentId = Me.State.DBCommentBO.Id
                        Me.PopulateBOProperty(Me.State.DataProtectionHistBO, "AddedBy", ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        Me.PopulateBOProperty(Me.State.DataProtectionHistBO, "EntityType", EntityType)
                        Me.PopulateBOProperty(Me.State.DataProtectionHistBO, "Status", restrictionId)
                        Me.State.DataProtectionHistBO.IsRequestIdUsed = Me.State.DataProtectionHistBO.GetRequestIdUsedInfo(Me.State.DataProtectionHistBO.RequestId, actionCode, Me.State.MyBO.Id)
                    End With
                ElseIf (actionCode.Equals("F")) Then
                    ' Logic For Right To Forgotten'

                    With Me.State.CertForgotBO
                        Me.State.CertForgotBO.CertId = Me.State.MyBO.Id
                        Me.State.CertForgotBO.CertNumber = Me.State.MyBO.CertNumber
                        Me.State.CertForgotBO.DealerId = Me.State.MyBO.DealerId
                        Me.State.CertForgotBO.IsForgotten = "N"
                        Me.PopulateBOProperty(Me.State.CertForgotBO, "RequestId", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_REQUEST_ID), TextBox))
                        Me.State.CertForgotBO.IsRequestIdUsed = Me.State.DataProtectionHistBO.GetRequestIdUsedInfo(Me.State.CertForgotBO.RequestId, actionCode, Me.State.MyBO.Id)
                    End With


                    With Me.State.DBCommentBO
                        Me.PopulateBOProperty(Me.State.DBCommentBO, "Comments", CType(GridDataProtection.FooterRow.FindControl(GRID_CTRL_NAME_COMMENT), TextBox))
                        Me.PopulateBOProperty(Me.State.DBCommentBO, "CommentTypeId", commentTypeId)
                        Me.State.DBCommentBO.CallerName = CallerName
                        Me.State.DBCommentBO.ForgotRequestId = Me.State.CertForgotBO.Id

                    End With
                    With Me.State.DBCertificateBO
                        Me.State.DBCertificateBO.IsRestricted = "D"
                    End With
                End If


                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function ValidateDataProtectionFields(ByVal actionCode As String)
            Dim blnSuccess As Boolean = True

            If Me.State.DBCommentBO.Comments = String.Empty Then
                Me.MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("COMMENT") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnSuccess = False
            End If

            If (actionCode.Equals("R") Or actionCode.Equals("UR")) Then
                If Me.State.DataProtectionHistBO.RequestId = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("REQUEST_ID") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                    blnSuccess = False
                End If

                If Me.State.DataProtectionHistBO.IsRequestIdUsed Then
                    Me.MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.REQUEST_ID_IS_USED_ERR))
                    blnSuccess = False
                End If
            ElseIf (actionCode.Equals("F")) Then
                If Me.State.CertForgotBO.RequestId = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage("REQUEST_ID") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                    blnSuccess = False
                End If

                If Me.State.CertForgotBO.IsRequestIdUsed Then
                    Me.MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.REQUEST_ID_IS_USED_ERR))
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
                If (Not .CertForgotBO Is Nothing) Then
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
        Protected Sub btnUnRestrict_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUnRestrict.Click
            Try
                Me.State.DataProtectionHistBO = New DataProtectionHistory()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)

                With Me.State.DataProtectionHistBO
                    Me.State.DataProtectionHistBO.RestrictedStatus = UnRestrictCode
                End With
                AddNewRowToGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Right To Forgotten "

        Protected Sub btnRightToForgotten_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRightToForgotten.Click
            Try
                Me.State.DataProtectionHistBO = New DataProtectionHistory()
                Me.State.CertForgotBO = New CertForgotRequest()
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnRestrict, False)
                ControlMgr.SetVisibleControl(Me, btnRightToForgotten, False)
                ControlMgr.SetVisibleControl(Me, btnUnRestrict, False)
                With Me.State.CertForgotBO
                    Me.State.CertForgotBO.RestrictedStatus = ForgottenCode
                End With
                AddNewRowForRightToForgotten()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SaveRightToForgotten()
            If Not Me.HiddenSavePromptResponse.Value Is Nothing AndAlso Me.HiddenSavePromptResponse.Value = Me.MSG_VALUE_YES Then
                Me.State.CertForgotBO.Save()
                ClearStateProperties()
                Me.Redirect(URL_CertList)
            End If
            Me.HiddenSavePromptResponse.Value = ""
        End Sub

        Private Sub AddNewRowForRightToForgotten()

            Dim dv As DataProtectionHistory.DataProtectionCommentDV = DataProtectionHistory.GetList(Me.State.MyBO.Id)
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
        Private Sub EndorsementsGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles EndorsementsGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (Not e.Item.Cells(Me.GRID_COL_ENDORSE_NUMBER).FindControl(ENDORSEMENT_GRID_COL_EDIT_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Item.Cells(Me.GRID_COL_ENDORSE_NUMBER).FindControl(ENDORSEMENT_GRID_COL_EDIT_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSE_NUMB).ToString
                    End If
                    e.Item.Cells(Me.GRID_COL_ENDORSE_CREATED_BY).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ADDED_BY).ToString
                    Dim createdDate As Date = CType(dvRow(CertEndorse.EndorseSearchDV.COL_CREATED_DATE), Date)
                    e.Item.Cells(Me.GRID_COL_ENDORSE_CREATED_DATE).Text = GetLongDateFormattedString(createdDate)
                    e.Item.Cells(Me.GRID_COL_ENDORSE_TYPE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_TYPE).ToString
                    e.Item.Cells(Me.GRID_COL_ENDORSE_ENDORSEMENT_REASON).Text = dvRow(CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_REASON).ToString

                    e.Item.Cells(Me.GRID_COL_ENDORSE_EFFECTIVE_DATE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_EFFECTIVE_DATE).ToString

                    e.Item.Cells(Me.GRID_COL_ENDORSE_EXPIRATION_DATE).Text = dvRow(CertEndorse.EndorseSearchDV.COL_EXPIRATION_DATE).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub EndorsementsGrid_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EndorsementsGrid.ItemCommand

            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.TheItemCoverageState.selectedItemId = New Guid(e.CommandArgument.ToString())
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = Me.State.TheItemCoverageState.selectedItemId
                    Me.State.NavigateToEndorment = True
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_ENDORSEMENT_SELECTED, New EndorsementDetailForm.Parameters(Me.State.TheItemCoverageState.manufaturerWarranty))

                    'Me.NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))

                End If
                '"endorsement_detail"
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub EndorsementsGrid_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles EndorsementsGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EndorsementsGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles EndorsementsGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                Me.State.EndorsementsPageIndex = e.NewPageIndex
                Me.State.SelectedEndorseId = Guid.Empty
                Me.PopulateEndorsementsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateEndorsementsGrid()

            If Not Me.State.IsEndorsementsGridVisible Then
                Exit Sub
            End If

            Dim cert As Certificate = Me.State.MyBO
            Dim ItemDV As CertItem.CertItemSearchDV = CertItem.GetItems(Me.State.MyBO.Id)
            Dim dv As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(Me.State.MyBO.Id)

            dv.Sort = CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE
            If dv.Count > 0 Then
                Dim coverageRow As DataRow = dv.Table.Rows(0)
            End If

            If ItemDV.Count > 1 Or cert.StatusCode <> CERT_STATUS Or Me.State.MyBO.IsChildCertificate Then
                Me.btnAddEndorsement_WRITE.Enabled = False
            End If

            Me.State.TheItemCoverageState.cert_item_id = New Guid(CType(ItemDV(0)(CertItem.CertItemSearchDV.COL_CERT_ITEM_ID), Byte()))
            Dim EndorseDV As CertEndorse.EndorseSearchDV = CertEndorse.getList(Me.State.MyBO.Id)

            Me.EndorsementsGrid.AutoGenerateColumns = False
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_NUMBER).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSE_NUMB
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_CREATED_BY).SortExpression = CertEndorse.EndorseSearchDV.COL_ADDED_BY
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_CREATED_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_CREATED_DATE
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_TYPE).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_TYPE
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_ENDORSEMENT_REASON).SortExpression = CertEndorse.EndorseSearchDV.COL_ENDORSEMENT_REASON
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_EFFECTIVE_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_EFFECTIVE_DATE
            Me.EndorsementsGrid.Columns(Me.GRID_COL_ENDORSE_EXPIRATION_DATE).SortExpression = CertEndorse.EndorseSearchDV.COL_EXPIRATION_DATE

            Me.EndorsementsGrid.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(EndorseDV, Me.State.SelectedCommentId, Me.EndorsementsGrid, Me.State.EndorsementsPageIndex)
            Me.EndorsementsGrid.CurrentPageIndex = Me.State.EndorsementsPageIndex
            Me.EndorsementsGrid.DataSource = EndorseDV
            Me.EndorsementsGrid.DataBind()

            'Me.tsHoriz.Items(Me.CERT_ENDORSEMENTS_TAB).Text = TranslationBase.TranslateLabelOrMessage("ENDORSEMENTS") '& " : " & EndorseDV.Count.ToString()

            ControlMgr.SetVisibleControl(Me, EndorsementsGrid, Me.State.IsEndorsementsGridVisible)
        End Sub

#End Region

#Region "Extensions Datagrid Related"
        Public Sub PopulateExtensionsGrid()

            Try
                If (Me.State.CertExtensionsDV Is Nothing) Then
                    Me.State.CertExtensionsDV = Certificate.GetCertExtensionsInfo(Me.State.MyBO.Id)
                End If
                Me.trCertExtn.Visible = False
                Me.CertExtnGrid.PageSize = Me.State.PageSize
                If Me.State.CertExtensionsDV.Count > 0 Then
                    Me.trCertExtn.Visible = True
                    Me.State.CertExtensionsDV.Sort = Me.State.CertExtensionSortExpression

                    SetPageAndSelectedIndexFromGuid(Me.State.CertExtensionsDV, Guid.Empty, Me.CertExtnGrid, Me.State.PageIndex)
                    Me.CertExtnGrid.DataSource = Me.State.CertExtensionsDV
                    Me.State.PageIndex = Me.CertExtnGrid.PageIndex

                    HighLightSortColumn(Me.CertExtnGrid, Me.State.CertExtensionSortExpression, Me.IsNewUI)
                    Me.CertExtnGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertExtnGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.CertExtnGrid.Visible)
                    Session("recCount") = Me.State.CertExtensionsDV.Count
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(CERT_REPRICE_TAB), True)
                End If
            Catch ex As Exception

            End Try

        End Sub
        Private Sub ExtensionsGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CertExtnGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                Me.State.ExtensionGridPageIndex = e.NewPageIndex
                Me.PopulateExtensionsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Upgrade Data Extensions Datagrid Related"
        Public Sub PopulateUPgradeDataExtensionsGrid()

            Try
                If (Me.State.CertUpgradeExtensionsDV Is Nothing) Then
                    Me.State.CertUpgradeExtensionsDV = Certificate.GetCertUpgradeDataExtensionsInfo(Me.State.MyBO.Id)
                End If

                Me.UpgradeDataGridtr.Visible = False
                Me.CertUpgradeDatagrid.PageSize = Me.State.PageSize
                If Me.State.CertUpgradeExtensionsDV.Count > 0 Then
                    Me.UpgradeDataGridtr.Visible = True
                    Me.State.CertUpgradeExtensionsDV.Sort = Me.State.CertUpgradeExtensionSortExpression

                    SetPageAndSelectedIndexFromGuid(Me.State.CertUpgradeExtensionsDV, Guid.Empty, Me.CertUpgradeDatagrid, Me.State.PageIndex)
                    Me.CertUpgradeDatagrid.DataSource = Me.State.CertUpgradeExtensionsDV

                    HighLightSortColumn(Me.CertUpgradeDatagrid, Me.State.CertUpgradeExtensionSortExpression, Me.IsNewUI)
                    Me.CertUpgradeDatagrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertUpgradeDatagrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.CertUpgradeDatagrid.Visible)
                    Session("recCount") = Me.State.CertUpgradeExtensionsDV.Count
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub CertUpgradeDatagrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CertUpgradeDatagrid.PageIndexChanged
            Try
                Me.State.ExtensionGridPageIndex = e.NewPageIndex
                Me.PopulateUPgradeDataExtensionsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Certificate Extended Fields grid Related "
        Private Sub GridCertExtFields_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GridCertExtFields.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                'Dim btnEditItem As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    'If (Not e.Item.Cells(GRID_COL_TIME_STAMP).FindControl(COMMENT_GRID_COL_EDIT_CTRL) Is Nothing) Then
                    '    btnEditItem = CType(e.Item.Cells(GRID_COL_TIME_STAMP).FindControl(ITEMS_GRID_COL_EDIT_CTRL), LinkButton)
                    '    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Comment.CommentSearchDV.COL_COMMENT_ID), Byte()))
                    '    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    '    Dim createdDate As Date = CType(dvRow(Comment.CommentSearchDV.COL_CREATED_DATE), Date)
                    '    btnEditItem.Text = GetLongDateFormattedString(createdDate)
                    'End If
                    e.Item.Cells(CERT_EXT_CERT_ID_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_CERT_ID).ToString
                    e.Item.Cells(CERT_EXT_FIELD_NAME_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_FIELD_NAME).ToString
                    e.Item.Cells(CERT_EXT_FIELD_VALUE_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_FIELD_VALUE).ToString
                    e.Item.Cells(CERT_EXT_CREATED_BY_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_CREATED_BY).ToString
                    e.Item.Cells(CERT_EXT_MODIFIED_BY_IDX).Text = dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_BY).ToString

                    Dim strCreationDate As String = dvRow(Certificate.CertExtendedFieldsDv.COL_CREATED_DATE).Text.ToString
                    strCreationDate = strCreationDate.Replace("&nbsp;", "")
                    If String.IsNullOrWhiteSpace(strCreationDate) = False Then
                        Dim tempCreationDate = Convert.ToDateTime(dvRow(Certificate.CertExtendedFieldsDv.COL_CREATED_DATE).Text.Trim())
                        Dim formattedCreationDate = GetLongDate12FormattedString(tempCreationDate)
                        e.Item.Cells(CERT_EXT_CREATED_DATE_IDX).Text = Convert.ToString(formattedCreationDate)
                    End If

                    Dim strModifiedDate As String = dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE).Text.ToString
                    strModifiedDate = strModifiedDate.Replace("&nbsp;", "")
                    If String.IsNullOrWhiteSpace(strModifiedDate) = False Then
                        Dim tempModifiedDate = Convert.ToDateTime(dvRow(Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE).Text.Trim())
                        Dim formattedModifiedDate = GetLongDate12FormattedString(tempModifiedDate)
                        e.Item.Cells(CERT_EXT_MODIFIED_DATE_IDX).Text = Convert.ToString(formattedModifiedDate)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCertExtFields_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles GridCertExtFields.SortCommand
            Try
                If Me.State.CertExtFieldsSortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.CertExtFieldsSortExpression.EndsWith(" DESC") Then
                        Me.State.CertExtFieldsSortExpression = e.SortExpression
                    Else
                        Me.State.CertExtFieldsSortExpression &= " DESC"
                    End If
                Else
                    Me.State.CertExtFieldsSortExpression = e.SortExpression
                End If
                Me.State.SelectedCertExtId = Nothing
                Me.State.PageIndex = 0
                Me.PopulateCertExtendedFieldsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub GridCertExtFields_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GridCertExtFields.ItemCommand
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.SelectedCertExtId = New Guid(e.CommandArgument.ToString())
                    Dim originalComment As Comment = New Comment(Me.State.SelectedCertExtId)
                    Dim newComment As Comment = Comment.GetNewComment(originalComment)
                    Me.State.NavigateToComment = True
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(newComment))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub GridCertExtFields_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GridCertExtFields.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCertExtFields_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles GridCertExtFields.PageIndexChanged
            Try
                Me.State.CertExtFieldsPageIndex = e.NewPageIndex
                Me.State.SelectedCertExtId = Guid.Empty
                Me.PopulateCertExtendedFieldsGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateCertExtendedFieldsGrid()
            Dim cert As Certificate = Me.State.MyBO
            Dim dv As Certificate.CertExtendedFieldsDv = Certificate.GetCertExtensionFieldsList(Me.State.MyBO.Id, Authentication.CurrentUser.LanguageId)
            dv.Sort = Me.State.CertExtFieldsSortExpression

            Me.GridCertExtFields.AutoGenerateColumns = False
            Me.GridCertExtFields.Columns(CERT_EXT_FIELD_NAME_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_FIELD_NAME
            Me.GridCertExtFields.Columns(CERT_EXT_FIELD_VALUE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_FIELD_VALUE
            Me.GridCertExtFields.Columns(CERT_EXT_CREATED_BY_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_CREATED_BY
            Me.GridCertExtFields.Columns(CERT_EXT_CREATED_DATE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_CREATED_DATE
            Me.GridCertExtFields.Columns(CERT_EXT_MODIFIED_BY_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_MODIFIED_BY
            Me.GridCertExtFields.Columns(CERT_EXT_MODIFIED_DATE_IDX).SortExpression = Certificate.CertExtendedFieldsDv.COL_MODIFIED_DATE
            Me.GridCertExtFields.EditItemIndex = -1
            SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedCertExtId, Me.GridCertExtFields, Me.State.CertExtFieldsPageIndex)
            Me.State.CertExtFieldsPageIndex = Me.GridCertExtFields.CurrentPageIndex
            Me.GridCertExtFields.DataSource = dv
            Me.GridCertExtFields.DataBind()

            lblTabCommentHeader.Text = TranslationBase.TranslateLabelOrMessage("CERT_EXT_FIELDS") & " : " & dv.Count.ToString()

            ControlMgr.SetVisibleControl(Me, GridCertExtFields, Me.State.IsCertExtFieldsGridVisible)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub TNCButton_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TNCButton_WRITE.Click
            Try
                Me.callPage(Tables.TermAndConditionsForm.URL, Me.State.MyBO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAddComment_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComment_WRITE.Click
            Try
                Me.State.NavigateToComment = True
                Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(Comment.GetNewComment(Me.State.MyBO.Id)))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        Private Sub btnAddEndorsement_WRITE__Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEndorsement_WRITE.Click

            Try
                Me.State.NavigateToEndorment = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.NavController.Navigate(Me, CREATE_NEW_ENDORSEMENT, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewCertItem_WRITE__Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCertItem_WRITE.Click

            Try
                Me.State.NavigateToNewCertItem = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.NavController.Navigate(Me, CREATE_NEW_ITEM, New CertAddNewItemForm.Parameters(Me.State.MyBO.Id))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim myBo As Certificate = Me.State.MyBO
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, Me.State.certificateChanged, Me.State.IsCallerAuthenticated)
                    Me.State.selectedTab = 0
                    Me.State.CertHistoryDV = Nothing
                    Me.NavController = Nothing
                    Session(Me.SESSION_KEY_BACKUP_STATE) = New MyState
                    Me.ReturnToCallingPage(retObj)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Try
                    If Me.AddressCtr.MyBO Is Nothing Then
                        Dim myBo As Certificate = Me.State.MyBO
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, Me.State.certificateChanged, Me.State.IsCallerAuthenticated)
                        Me.NavController = Nothing
                        Session(Me.SESSION_KEY_BACKUP_STATE) = New MyState
                        Me.ReturnToCallingPage(retObj)
                    End If
                Catch ex2 As Threading.ThreadAbortException
                Catch ex2 As Exception
                    Me.HandleErrors(ex2, Me.MasterPage.MessageController)
                End Try

                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub


        Private Sub btnNewClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewClaim.Click
            Try
                ' DEF-4245  Clean up the claim BO so any new claims added will be display when returning to this page.
                Me.State.ClaimsearchDV = Nothing

                Me.NavController.FlowSession.Clear()
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Dim dealer As New Dealer(Me.State.MyBO.DealerId)

                If Not String.IsNullOrEmpty(Me.State.ClaimRecordingXcd) AndAlso
                   Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                    callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(Me.State.MyBO.Id, Nothing, Nothing, Codes.CASE_PURPOSE__REPORT_CLAIM, Me.State.IsCallerAuthenticated))
                Else
                    'REQ-863 Claims Payable
                    'Check whether to use new claim Authorization structure or not
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, dealer.UseClaimAuthorizationId) = Codes.YESNO_N) Then
                        Me.NavController.Navigate(Me, "locate_eligible_coverages")
                    Else
                        Dim certId As Guid = Me.State.MyBO.Id
                        Me.NavController = Nothing
                        Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step1, certId, Nothing, Nothing, True,, Me.State.IsCallerAuthenticated))
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewClaimDcm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewClaimDcm.Click
            Try
                ' DEF-4245  Clean up the claim BO so any new claims added will be display when returning to this page.
                Me.State.ClaimsearchDV = Nothing

                Me.NavController.FlowSession.Clear()
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Dim dealer As New Dealer(Me.State.MyBO.DealerId)

                If Not String.IsNullOrEmpty(Me.State.ClaimRecordingXcd) AndAlso
                   (Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse
                    Me.State.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH)) Then
                    callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(Me.State.MyBO.Id, Nothing, Nothing, Codes.CASE_PURPOSE__REPORT_CLAIM))
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveCertDetail_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCertDetail_WRITE.Click
            Try
                Dim objComp As New Company(Me.State.MyBO.CompanyId)
                Dim strTranferOfOwnership As String
                If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                    strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                Else
                    strTranferOfOwnership = String.Empty
                End If
                If strTranferOfOwnership = YES And Me.State.MyBO.StatusCode = CERT_STATUS Then
                    If Not Me.State.MyBO.CustomerName Is Nothing Then
                        If (Me.State.MyBO.CustomerName <> moCustomerNameText.Text And moCustomerNameText.Text.Trim <> String.Empty) Or (moTaxIdText.Visible = True And Me.State.MyBO.TaxIDNumb <> moTaxIdText.Text) Then
                            Me.DisplayMessage(Message.MSG_TRANSFER_Of_OWNERSHIP_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenTransferOfOwnershipPromptResponse)
                        Else
                            Me.saveCertificate()
                        End If
                    End If
                Else
                    Me.saveCertificate()
                End If
                'KDDI
                'Dim btnValidate_Address As Button = AddressCtr.FindControl(ValidateAddressButton)
                'ControlMgr.SetVisibleControl(Me, btnValidate_Address, False)
                ' Address Validation
                EnableDisableAddressValidation()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveCertInfo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCertInfo_WRITE.Click
            Try
                Me.saveCertificate()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveTaxID_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTaxID_WRITE.Click
            Try
                Dim objComp As New Company(Me.State.MyBO.CompanyId)
                Dim strTranferOfOwnership As String
                If Me.State.MyBO.TaxIDNumb Is Nothing Then
                    Me.saveCertificate()
                Else
                    If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                        strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
                    Else
                        strTranferOfOwnership = String.Empty
                    End If
                    If strTranferOfOwnership = YES And Me.State.MyBO.StatusCode = CERT_STATUS Then
                        If Me.cboDocumentTypeId.SelectedIndex <= 0 Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                        End If
                        If (Me.State.MyBO.TaxIDNumb <> moNewTaxIdText.Text And moNewTaxIdText.Text.Trim <> String.Empty) Or (Me.cboDocumentTypeId.SelectedItem.Text <> Me.State.MyBO.getDocTypeDesc) Then
                            Me.DisplayMessage(Message.MSG_TRANSFER_Of_OWNERSHIP_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenTransferOfOwnershipPromptResponse)
                        Else
                            Me.saveCertificate()
                        End If
                    Else
                        Me.saveCertificate()
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebitSave_WRITE.Click
            Try
                BindInstallmentBoPropertiesToLabels()
                Me.saveCertInstallment(Me.State.TheDirectDebitState.StatusChenge)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebitHistory.Click
            Try
                Me.State.NavigateToBillingHistory = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.callPage(BillingHistoryListForm.URL, Me.State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        'REQ-6189
        Private Sub btnBankInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBankInfo.Click
            Try
                Me.State.NavigateToBankInfoHistory = True

                If Not Me.State.MyBO Is Nothing AndAlso Not Me.State.MyBO.Id.Equals(Guid.Empty) Then

                    Me.State.certInstallment = New CertInstallment(Me.State.MyBO.Id, True)

                    If Not Me.State.certInstallment Is Nothing Then

                        Dim bankInfo As BankInfo = New BankInfo()
                        If Me.State.certInstallment.BankInfoId <> Guid.Empty Then
                            REM bankInfo = New BankInfo(Me.State.certInstallment.BankInfoId)
                            Dim currentBankInfo = New BankInfo(Me.State.certInstallment.BankInfoId)

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
                            bankInfo.CountryID = Me.State.MyBO.Company.CountryId
                            bankInfo.SourceCountryID = Me.State.MyBO.Company.CountryId
                        End If

                        bankInfo.ValidateFieldsforFR = True

                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_INSTALLMENT) = Me.State.certInstallment
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_BANK_INFO) = bankInfo

                        Me.callPage(URL_BANKINFO, Me.State.MyBO.Id)
                    End If
                End If



            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'Nandan
        Private Sub btnPaymentHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPaymentHistory.Click
            Try
                Me.State.NavigateToPaymentHistory = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.callPage(PaymentHistoryListForm.URL, Me.State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBillpayHist_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBillPayHist.Click
            Try
                Me.State.NavigateToBillpayHistory = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.NavController.Navigate(Me, CREATE_NEW_BILLING_PAYMENT, New BillingPaymentHistoryListForm.Parameters(Me.State.MyBO.Id))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoCertDetail_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndoCertDetail_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New Certificate
                End If
                Me.State.IsEdit = False
                PopulateFormFromBOs()
                Me.EnableDisableFields()
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoCertInfo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndoCertInfo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New Certificate
                End If
                Me.State.IsEdit = False
                PopulateFormFromBOs()
                Me.EnableDisableFields()
                ControlMgr.SetVisibleControl(Me, BtnProductSalesDate, False)
                ControlMgr.SetVisibleControl(Me, BtnWarrantySoldDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCertificateVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnSEPAMandateDate, False)
                ControlMgr.SetVisibleControl(Me, BtnCheckVerificationDate, False)
                ControlMgr.SetVisibleControl(Me, BtnContractCheckCompleteDate, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoTaxID_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoTaxID_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Certificate(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New Certificate
                End If
                Me.State.IsEdit = False
                PopulateFormFromBOs()
                Me.EnableDisableFields()
                ControlMgr.SetVisibleControl(Me, BtnDocumentIssueDate, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndoDebit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoDebit_WRITE.Click

            Me.State.IsEdit = False
            Me.PopulatePremiumInfoTab()
            Me.EnableDisableFields()

        End Sub

        Private Sub btnCancelRequestUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRequestUndo_WRITE.Click

            Me.State.IsEdit = False
            populateCancelRequestInfoTab()
            Me.EnableDisableFields()

            SetFocus(moCancelRequestReasonDrop)
        End Sub
        Private Sub btnEditCertDetail_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditCertDetail_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs()
                Me.EnableDisableFields()
                If Not Me.moEmailAddressText.Text Is Nothing AndAlso Me.moEmailAddressText.Text.Equals(Me.State.EmailIsNull) Then
                    Me.moEmailAddressText.Text = ""
                End If
                If Not Me.State.MyBO.CustomerId.Equals(Guid.Empty) Then
                    SetFocus(moCustomerFirstNameText)
                Else
                    SetFocus(moCustomerNameText)
                End If
                'KDDI
                Me.EnableDisableAddressValidation()
                Me.State.AddressFlag = False

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDebitEdit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebitEdit_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs(True)
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEditCertInfo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditCertInfo_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEditTaxID_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditTaxID_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCancelRequestEdit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRequestEdit_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.CertCancelRequestId = Me.State.MyBO.getCertCancelRequestID
                populateCancelRequestInfoTab()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnCreateNewRequest_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateNewRequest_WRITE.Click
            Try
                Me.State.IsEdit = True
                Me.State.CertCancelRequestId = Guid.Empty
                Me.State.certCancelRequestBO = New CertCancelRequest
                ControlMgr.SetVisibleControl(Me, btnCreateNewRequest_WRITE, False)
                populateCancelRequestInfoTab()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnRemoveCancelDueDate_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveCancelDueDate_WRITE.Click
            Try
                Me.DisplayMessage(Message.REMOVE_CANCELDUEDATE_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenRemoveCancelDueDatePromptResponse)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DocumentsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentsButton.Click
            Try
                Me.callPage(CertificateDocumentForm.URL, Me.State.MyBO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCancelCertificate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelCertificate_WRITE.Click
            Try
                Me.WorkingPanelVisible = False

                ControlMgr.SetVisibleControl(Me, RefundAmtTextbox, False)
                ControlMgr.SetVisibleControl(Me, RefundAmtLabel, False)
                If RefundAmtLabel.Text.IndexOf(":") > 0 Then
                    Me.RefundAmtLabel.Text = Me.RefundAmtLabel.Text
                Else
                    Me.RefundAmtLabel.Text = Me.RefundAmtLabel.Text & ":"
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
                Me.MasterPage.MessageController.Clear()
                moCertificateInfoCtrlCancel = Me.UserCertificateCtrCancel
                moBankInfoController = Me.UserBankInfoCtr
                'moBankInfoController.InitController()
                Me.moBankInfoController.Visible = False
                'Me.State.BankInfoCtrVisible = False

                moPaymentOrderInfoCtrl = Me.UserPaymentOrderInfoCtr
                'moBankInfoController.InitController()
                Me.moPaymentOrderInfoCtrl.Visible = False
                'Me.State.BankInfoCtrVisible = False

                moCertificateInfoCtrlCancel.InitController(Me.State.MyBO.Id, , Me.State.companyCode)
                Me.State.certCancellationBO = New CertCancellation
                If Me.State.ComputeCancellationDateEOfM = Codes.YESNO_Y Then
                    Me.State.certCancellationBO.CancellationRequestedDate = Date.Today
                    Me.State.certCancellationBO.CancellationDate = ComputeEndOfMonth(Date.Today)
                End If
                Me.PopulateCancellationReasonDropdown(Me.moCancellationReasonDrop, Nothing, Nothing)
                PopulateCancelCommentTypeDropdown(moCancelCommentType)
                Me.PopulateControlFromBOProperty(Me.CancelCallerNameTextbox, Me.State.MyBO.CustomerName)
                Me.populateFormFromCertCancellationBO()

                '** populate the paymentmethod drop down here ???
                'Me.PopulatePaymentMethodDropdown(Me.PaymentMethodDrop)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BackCancelCertButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackCancelCertButton.Click
            Try
                Me.WorkingPanelVisible = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub QuoteButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuoteButton_WRITE.Click
            Dim oCancelCertificateData As New CertCancellationData

            Try
                Me.State.QuotedRefundAmt = Nothing
                Me.State.QuotedInstallmentsPaid = Nothing
                Me.WorkingPanelVisible = False
                Me.MasterPage.MessageController.Clear()
                Me.moBankInfoController.Visible = False
                Me.moPaymentOrderInfoCtrl.Visible = False
                oCancelCertificateData.refundAmount = Nothing
                oCancelCertificateData.InstallmentsPaid = Nothing
                Me.State.BankInfoBO = Nothing
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, False)
                'Me.trbank.Visible = True

                Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationReasonId", Me.moCancellationReasonDrop)
                Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationDate", Me.CancelCertDateTextbox)
                Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationRequestedDate", Me.CancelCertReqDateTextbox)

                State.MyBO.QuoteCancellation(Me.State.certCancellationBO, oCancelCertificateData)
                If Not oCancelCertificateData.errorExist Then
                    If Not oCancelCertificateData.inputAmountRequiredMissing Then
                        ControlMgr.SetVisibleControl(Me, RefundAmtTextbox, True)
                        ControlMgr.SetVisibleControl(Me, RefundAmtLabel, True)
                        Me.RefundAmtTextbox.ReadOnly = True

                        'allow cancellation even if the computed refund is below company tolerance amt, but dont show the payment mthd
                        ' because refund_amount should be zero here.
                        If oCancelCertificateData.refund_amt_reset = "Y" Then 'AndAlso oCancelCertificateData.creditIssued = "N" Then
                            If oCancelCertificateData.refundAmount <> 0 Then
                                ControlMgr.SetVisibleControl(Me, LabelWarningRefundAmtBelowTolerance, True)
                            End If
                        Else
                            ControlMgr.SetVisibleControl(Me, LabelWarningRefundAmtBelowTolerance, False)
                        End If
                        Me.PopulateControlFromBOProperty(Me.RefundAmtTextbox, oCancelCertificateData.refundAmount)
                        Me.PopulateControlFromBOProperty(Me.txtInstallmentsPaid, oCancelCertificateData.InstallmentsPaid)
                        Me.State.QuotedRefundAmt = oCancelCertificateData.refundAmount
                        Me.State.QuotedInstallmentsPaid = oCancelCertificateData.InstallmentsPaid

                        '  If Not Me.State.MyBO.getPaymentTypeCode = Me.State.MyBO.PAYMENT_PRE_AUTHORIZED Then
                        If ((LookupListNew.GetCodeFromId(LookupListNew.LK_REFUND_DESTINATION, oCancelCertificateData.refund_dest_id) = Codes.REFUND_DESTINATION__CUSTOMER AndAlso oCancelCertificateData.refundAmount > 0)) Then
                            '** populate the paymentmethod drop down here 
                            State.validateAddress = True
                            PopulatePaymentMethodDropdown(PaymentMethodDrop)
                            If PaymentMethodDrpLabel.Text.IndexOf(":") > 0 Then
                                Me.PaymentMethodDrpLabel.Text = Me.PaymentMethodDrpLabel.Text
                            Else
                                Me.PaymentMethodDrpLabel.Text = Me.PaymentMethodDrpLabel.Text & ":"
                            End If
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, True)

                            Me.SetSelectedItem(Me.PaymentMethodDrop, oCancelCertificateData.def_refund_payment_method_id)
                        Else
                            '** do not show payment method if refund amt=0 or refund is not to customer
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrpLabel, False)
                            State.validateAddress = False
                        End If
                        Me.PopulateBOProperty(Me.State.certCancellationBO, "PaymentMethodId", Me.PaymentMethodDrop)
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
                        Me.MasterPage.MessageController.AddWarning(Message.MSG_PROMPT_FOR_REINSTATEMENT_NOT_ALLOWED_ON_REPLACEMENT, True)
                    End If
                Else
                    Me.MasterPage.MessageController.AddErrorAndShow(oCancelCertificateData.errorCode)
                    ControlMgr.SetVisibleControl(Me, ProcessCancellationButton_WRITE, False)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Finally
                Me.PopulatePremiumInfoTab()
            End Try
        End Sub

        Private Sub btnCancelRequestSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRequestSave_WRITE.Click
            Dim oCertCancelRequestData As New CertCancelRequestData
            Dim dblRefundAmount As Double, strMsg, strMsgCertCancelSuccess As String
            Try
                MasterPage.MessageController.Clear()
                populateCertCancelRequestBOFromForm()
                populateCertCancelRequestCommentBOFromForm()
                ClearLabelError(moCRIBANNumberLabel)
                Me.State.MyBO.ValidateCancelRequest(Me.State.certCancelRequestBO, Me.State.CancReqCommentBO, oCertCancelRequestData, Me.State.useExistingBankInfo, Me.State.CRequestBankInfoBO)
                If Not oCertCancelRequestData.errorExist Then

                    Me.State.MyBO.ProcessCancelRequest(Me.State.certCancelRequestBO, Me.State.useExistingBankInfo, Me.State.CRequestBankInfoBO, Me.State.CancReqCommentBO, oCertCancelRequestData, dblRefundAmount, strMsg)

                    Me.State.MyBO = New Certificate(Me.State.CertificateId)
                    PopulateFormFromBOs()
                    Me.State.CertCancelRequestId = Me.State.MyBO.getCertCancelRequestID
                    Me.State.certCancelRequestBO = New CertCancelRequest(Me.State.CertCancelRequestId)
                    Me.State.IsEdit = False
                    populateCancelRequestInfoTab()
                    PopulateCommentsGrid()
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.WorkingPanelVisible = True
                    If Me.State.MyBO.StatusCode = "C" Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Me.State.certificateChanged = True
                        PopulateCancellationInfoTab()
                        If strMsg = SUCCESS Then
                            strMsgCertCancelSuccess = GetTranslation(Message.CANCEL_SUCCESS_WITH_REFUND)
                            strMsgCertCancelSuccess = strMsgCertCancelSuccess & " " & dblRefundAmount.ToString
                            Me.MasterPage.MessageController.AddSuccess(strMsgCertCancelSuccess, False)
                        End If
                    Else
                        If Me.State.MyBO.getCancelationRequestFlag = YES Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Else
                            If Me.State.MyBO.IsChildCertificate Then
                                ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                            Else
                                ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                            End If
                        End If
                        MasterPage.MessageController.Clear()
                        If strMsg = FUTURE Then
                            Me.MasterPage.MessageController.AddSuccess(Message.CANCEL_CERT_IN_FUTURE, True)
                        ElseIf strMsg = CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD Then
                            Me.MasterPage.MessageController.AddSuccess(Message.CNL_RSN_NOT_AVBL_FOR_SELECTED_PERIOD, True)
                        Else
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        End If
                    End If
                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                    EnableTab(CERT_CANCELLATION_INFO_TAB, True)
                    'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                    If isTabEnabled(0) = True Then Me.State.selectedTab = 0
                    Me.State.IsEdit = False
                    Me.EnableDisableFields()
                    Me.State.isCovgGridRefreshNeeded = True
                Else
                    If oCertCancelRequestData.errorCode = Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_INVALID Then
                        ElitaPlusPage.SetLabelError(moCRIBANNumberLabel)
                    End If
                    Me.MasterPage.MessageController.AddError(oCertCancelRequestData.errorCode)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ProcessCancellationButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessCancellationButton_WRITE.Click
            Dim errorExist As Boolean = False
            Dim oCancelCertificateData As New CertCancellationData

            Try
                'REQ-910 New fields BEGIN 
                If (Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "1")) _
                    Or Me.State.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "3"))) AndAlso (Me.State.QuotedRefundAmt > 0) Then ' 1 = Display and Require When Cancelling 
                    If (Me.State.MyBO.Occupation Is Nothing OrElse Me.State.MyBO.Occupation.Equals(String.Empty)) Or
                       (Me.State.MyBO.PoliticallyExposedId.Equals(Guid.Empty)) Or
                       (Me.State.MyBO.IncomeRangeId.Equals(Guid.Empty)) Then
                        Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_LEGAL_INFO_MISSING)
                    End If
                End If



                'REQ-910 New fields END

                Me.MasterPage.MessageController.Clear()
                Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationReasonId", Me.moCancellationReasonDrop)
                Me.PopulateBOProperty(Me.State.certCancellationBO, "CancellationDate", Me.CancelCertDateTextbox)
                Me.PopulateBOProperty(Me.State.certCancellationBO, "ComputedRefund", Me.RefundAmtTextbox)
                Me.PopulateBOProperty(Me.State.certCancellationBO, "InstallmentsPaid", Me.txtInstallmentsPaid)
                populateCertCancelCommentBOFromForm()

                validateCancellationProcessFields()
                ValidateCancellationCommentsFields()

                If Me.PaymentMethodDrop.Visible = True Then
                    If (Me.State.certCancellationBO.PaymentMethodId.Equals(Guid.Empty)) Then
                        Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_INVALID_PAYMENT_METHOD)
                        errorExist = True
                    Else
                        If State.validateAddress Then
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                                ValidateAddressField()
                            End If
                        End If
                    End If
                Else
                    Me.State.certCancellationBO.PaymentMethodId = System.Guid.Empty
                End If
                If Not errorExist Then

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                        If Me.moBankInfoController.Visible = True Then
                            Me.State.BankInfoCtrVisible = True
                            Me.PopulateBankInfoBOFromUserCtr()
                        Else
                            Me.State.BankInfoBO = Nothing
                        End If
                    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                        If Me.moPaymentOrderInfoCtrl.Visible = True Then
                            Me.State.PaymentOrderInfoCtrVisible = True
                            Me.PopulatePaymentOrderInfoBOFromUserCtr()

                            'If ((Me.State.MyBO.CustomerName Is Nothing OrElse Me.State.MyBO.CustomerName.Equals(String.Empty)) Or
                            '    (Me.State.MyBO.IdentificationNumber.Equals(Guid.Empty)) Or _
                            '    (Me.State.MyBO.IdType.Equals(Guid.Empty))) Then
                            '    Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_IDNUMBER_IDTYPE_INFO_MISSING)
                            'End If

                            If ((Me.State.MyBO.CustomerName Is Nothing OrElse Me.State.MyBO.CustomerName.Equals(String.Empty))) Then
                                Throw New GUIException(Message.MSG_CANCELLATION_CANNOT_BE_PROCESSED, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_CANCEL_CERT_CUST_NAME_INFO_MISSING)
                            End If
                        Else
                            Me.State.PaymentOrderInfoBO = Nothing
                        End If
                    Else
                        Me.State.BankInfoBO = Nothing
                        Me.State.PaymentOrderInfoBO = Nothing
                    End If

                    Dim oContract As New Contract
                    oContract = Contract.GetContract(Me.State.MyBO.DealerId, Me.State.MyBO.WarrantySalesDate.Value)


                    If Me.State.certCancellationBO.CancellationDate > DateTime.Today Then
                        If Me.State.MyBO.PreviousCertificateId.Equals(Guid.Empty) Then
                            ' the cancellation will be in the future
                            Me.State.MyBO.SetFutureCancellation(Me.State.certCancellationBO, Me.State.BankInfoBO, oContract, Me.State.CancCommentBO, , Me.State.PaymentOrderInfoBO)
                        Else
                            Me.State.MyBO.ProcessCancellation(Me.State.certCancellationBO, Me.State.BankInfoBO, oContract, Me.State.CancCommentBO, , Me.State.PaymentOrderInfoBO)
                        End If
                    Else
                        Me.State.MyBO.ProcessCancellation(Me.State.certCancellationBO, Me.State.BankInfoBO, oContract, Me.State.CancCommentBO, , Me.State.PaymentOrderInfoBO)
                    End If


                    If Me.IsSinglePremium Then
                        Me.PopulateCoveragesGrid()
                    End If

                    Me.State.MyBO = New Certificate(Me.State.CertificateId)

                    PopulateFormFromBOs()
                    Me.PopulateCancellationInfoTab()

                    PopulateCommentsGrid()

                    Me.DisplayMessage(Message.MSG_CERTIFICATE_CANCELLATION_CONFIRM, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.WorkingPanelVisible = True

                    If Me.State.MyBO.StatusCode = "C" Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Me.State.certificateChanged = True
                    Else
                        If Me.State.MyBO.IsChildCertificate Then
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                        Else
                            ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                        End If

                    End If

                    'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), True)
                    EnableTab(CERT_CANCELLATION_INFO_TAB, True)

                    'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                    If isTabEnabled(0) = True Then Me.State.selectedTab = 0

                    Me.EnableDisableFields()
                    Me.State.isCovgGridRefreshNeeded = True
                Else
                    ControlMgr.SetVisibleControl(Me, moBankInfoController, False)
                    Me.State.BankInfoCtrVisible = False

                    ControlMgr.SetVisibleControl(Me, moPaymentOrderInfoCtrl, False)
                    Me.State.PaymentOrderInfoCtrVisible = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ReverseCancellationButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReverseCancellationButton_WRITE.Click
            Try
                Dim oReverseCancelCertificateData As New ReverseCancellationData
                Dim compId As Guid = Me.State.MyBO.CompanyId

                With oReverseCancelCertificateData
                    .companyId = compId
                    .dealerId = Me.State.MyBO.DealerId
                    .certificate = Me.State.MyBO.CertNumber
                    .source = ElitaPlusIdentity.Current.ActiveUser.UserName
                    .sourceType = "USER"
                End With
                Dim cancDate As Date = Me.State.MyBO.TheCertCancellationBO.CancellationDate
                Dim attvalue As AttributeValue = Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CAN_DT_GTRTHN_12_MNTHS).FirstOrDefault

                If (Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CAN_DT_GTRTHN_12_MNTHS).Count > 0) Then
                    If cancDate.AddYears(1) < DateTime.Now Then
                        Me.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_MESSAGE_FUNCTIONALITY)
                        Throw New GUIException("", "")
                    End If
                End If

                Dim oContract As Contract = Contract.GetContract(Me.State.MyBO.DealerId, DateTime.Today)
                If oContract Is Nothing Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.NO_CONTRACT_FOUND)
                    Throw New GUIException("", "")
                End If

                Me.State.certCancellationBO.ReverseCancellation(oReverseCancelCertificateData)
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs()
                PopulatePremiumInfoTab()
                PopulateCommentsGrid()
                Me.DisplayMessage(Message.MSG_CERTIFICATE__REVERSE_CANCELLATION_CONFIRM, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.WorkingPanelVisible = True
                Me.State.certificateChanged = True

                If Me.State.MyBO.StatusCode = "C" Then
                    ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                Else
                    If Me.State.MyBO.IsChildCertificate Then
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, btnCancelCertificate_WRITE, True)
                    End If
                End If

                'If tsHoriz.Items(0).Enabled = True Then Me.tsHoriz.SelectedIndex = 0
                If isTabEnabled(0) = True Then Me.State.selectedTab = 0

                'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(Me.CERT_CANCELLATION_INFO_TAB), False)
                EnableTab(CERT_CANCELLATION_INFO_TAB, False)

                Me.EnableDisableFields()
                Me.State.isCovgGridRefreshNeeded = True

            Catch ex As GUIException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBillingStatusId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBillingStatusId.SelectedIndexChanged

            Dim oBillingCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.GetSelectedItem(Me.moBillingStatusId))
            Dim originalStatus As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusListShort(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.TheDirectDebitState.certInstallment.BillingStatusId)
            Dim dv As DataView = BillingDetail.getBillingTotals(Me.State.MyBO.Id)
            Dim oCount As Integer = 0

            ControlMgr.SetEnableControl(Me, btnDebitSave_WRITE, True)

            Try
                If Not originalStatus = "A" AndAlso Not oBillingCode = "A" Then
                    Me.PopulateControlFromBOProperty(Me.moBillingStatusId, Me.State.TheDirectDebitState.certInstallment.BillingStatusId)
                    Throw New GUIException("Cannot Reject an On Hold Record or Vice-Versa", "Cannot Reject an On Hold Record or Vice-Versa")
                End If

                If originalStatus = "A" AndAlso oBillingCode = "R" Then
                    Me.PopulateControlFromBOProperty(Me.moBillingStatusId, Me.State.TheDirectDebitState.certInstallment.BillingStatusId)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_REJECT_ACTIVE_RECORD)
                End If


                If oBillingCode = "R" Then
                    If Me.State.oBillingTotalAmount > 0 Then
                        If Me.State.TheDirectDebitState.BillingLastRecord Is Nothing Then
                            Me.State.TheDirectDebitState.BillingLastRecord = BillingDetail.getBillingLaterRow(Me.State.MyBO.Id)
                        End If

                        Dim lastBilledAmount As Decimal
                        If Not Me.State.TheDirectDebitState.BillingLastRecord.Table.Rows(0).IsNull(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT) Then
                            lastBilledAmount = CType(Me.State.TheDirectDebitState.BillingLastRecord.Table.Rows(0).Item(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT), Decimal)
                        End If

                        If lastBilledAmount < 0 Then
                            Throw New GUIException("Cannot Reject a Reversal.  Please See History", "Cannot reject a reversal.  Please see history")
                        End If
                    Else
                        Throw New GUIException("Can Not Reject What Was Not Send to Collection", "Can Not Reject What Was Not Send to Collection")
                    End If
                End If

                Me.State.TheDirectDebitState.StatusChenge = False

                If (Not Me.State.TheDirectDebitState.certInstallment.BillingStatusId.Equals(Me.GetSelectedItem(Me.moBillingStatusId))) AndAlso oBillingCode = "H" AndAlso Me.State.oBillingTotalAmount > 0 Then
                    Me.State.TheDirectDebitState.StatusChenge = True
                End If

                If Not oBillingCode = "A" Then
                    ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, True)
                    Me.CheckBoxSendLetter.CssClass = ""
                Else
                    CheckBoxSendLetter.Checked = False
                    ControlMgr.SetEnableControl(Me, CheckBoxSendLetter, False)
                    Me.moDateLetterSentText.Text = ""
                End If

                Me.State.IsEdit = True
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.State.IsEdit = True
                Me.EnableDisableFields()
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
            Me.NavController = New NavControllerBase(nav.Flow("CREATE_CLAIM_FROM_CERTIFICATE"))
        End Sub
#End Region

#Region "Dropdown Related"

        Private Sub PaymentMethodDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentMethodDrop.SelectedIndexChanged
            Dim objCompany As New Company(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY, Me.State.companyCode))
            Dim selectedPaymentMethod, selectedPaymentReason As Guid
            selectedPaymentMethod = Me.GetSelectedItem(Me.PaymentMethodDrop)
            Me.PopulateBOProperty(Me.State.certCancellationBO, "PaymentMethodId", Me.PaymentMethodDrop)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then


                ' SHOW THE BANK INFO USER CONTROL HERE -----
                ' Me.trbank.Visible = True
                Me.moBankInfoController.Visible = True
                Me.State.BankInfoBO = Nothing
                Me.State.BankInfoBO = Me.State.certCancellationBO.bankinfo

                Me.State.BankInfoBO.CountryID = Me.State.MyBO.AddressChild.CountryId
                Me.State.BankInfoBO.SourceCountryID = objCompany.CountryId
                Me.moBankInfoController.EnableDisableControls()
                Me.State.BankInfoBO.Account_Name = Me.moCustomerNameText.Text.Trim
                Me.State.BankInfoBO.PaymentMethodId = selectedPaymentMethod
                'Me.State.BankInfoBO.ACCOUNT_NUMBER = System.DBNull.Value

                Me.moBankInfoController.Bind(Me.State.BankInfoBO)
                Me.State.BankInfoCtrVisible = True

                Me.moPaymentOrderInfoCtrl.Visible = False
                Me.State.PaymentOrderInfoCtrVisible = False

                'Me.moBankInfoController.
            ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.certCancellationBO.PaymentMethodId) = Codes.PAYMENT_METHOD__PAYMENT_ORDER Then
                Me.moBankInfoController.Visible = False
                Me.State.BankInfoCtrVisible = False

                Me.State.PaymentOrderInfoCtrVisible = True
                Me.moPaymentOrderInfoCtrl.Visible = True

                Me.State.PaymentOrderInfoBO = Nothing
                Me.State.PaymentOrderInfoBO = Me.State.certCancellationBO.PmtOrderinfo

                Me.State.PaymentOrderInfoBO.CountryID = objCompany.CountryId
                Me.State.PaymentOrderInfoBO.SourceCountryID = objCompany.CountryId
                Me.moPaymentOrderInfoCtrl.EnableDisableControls()
                Me.State.PaymentOrderInfoBO.Account_Name = Me.moCustomerNameText.Text.Trim
                Me.State.PaymentOrderInfoBO.PaymentMethodId = selectedPaymentMethod
                Me.State.PaymentOrderInfoBO.CountryID = objCompany.CountryId
                'Me.State.BankInfoBO.ACCOUNT_NUMBER = System.DBNull.Value

                Me.moPaymentOrderInfoCtrl.Bind(Me.State.PaymentOrderInfoBO)

            Else
                Me.moBankInfoController.Visible = False
                Me.State.BankInfoCtrVisible = False
                Me.moPaymentOrderInfoCtrl.Visible = False
                Me.State.PaymentOrderInfoCtrVisible = False
            End If

        End Sub

#End Region

#Region "Certificate History Tab"

        Private Sub PopulateCertificateHistoryTabInfo()
            Try
                moCertificateHistory_cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                If (Me.State.CertHistoryDV Is Nothing) Then
                    Me.State.CertHistoryDV = Certificate.GetCertHistoryInfo(Me.State.MyBO.CertNumber, Me.State.MyBO.DealerId, IIf(chbShowUpdates.Checked, "true", "false").ToString())
                End If

                Me.CertHistoryGrid.PageSize = Me.State.PageSize
                If Not (Me.State.CertHistoryDV Is Nothing) Then
                    Me.State.CertHistoryDV.Sort = Me.State.CertHistorySortExpression

                    SetPageAndSelectedIndexFromGuid(Me.State.CertHistoryDV, Guid.Empty, Me.CertHistoryGrid, Me.State.PageIndex)
                    Me.CertHistoryGrid.DataSource = Me.State.CertHistoryDV
                    Me.State.PageIndex = Me.CertHistoryGrid.PageIndex

                    HighLightSortColumn(Me.CertHistoryGrid, Me.State.CertHistorySortExpression, Me.IsNewUI)
                    Me.CertHistoryGrid.DataBind()

                    ControlMgr.SetVisibleControl(Me, CertHistoryGrid, True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.CertHistoryGrid.Visible)
                    Session("recCount") = Me.State.CertHistoryDV.Count

                    If Me.CertHistoryGrid.Visible Then
                        Me.lblRecordCount.Text = Me.State.CertHistoryDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub CertHistoryGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles CertHistoryGrid.RowDataBound
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

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CertHistoryGrid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles CertHistoryGrid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateCertificateHistoryTabInfo()


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles CertHistoryGrid.Sorting
            Try
                If Me.State.CertHistorySortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.CertHistorySortExpression.EndsWith(" DESC") Then
                        Me.State.CertHistorySortExpression = e.SortExpression
                    Else
                        Me.State.CertHistorySortExpression &= " DESC"
                    End If
                Else
                    Me.State.CertHistorySortExpression = e.SortExpression
                End If
                Me.State.PageIndex = 0
                Me.PopulateCertificateHistoryTabInfo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles moCertificateHistory_cboPageSize.SelectedIndexChanged
            Try
                CertHistoryGrid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(moCertificateHistory_cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = CType(moCertificateHistory_cboPageSize.SelectedValue, Integer)
                Me.PopulateCertificateHistoryTabInfo()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub chbShowUpdates_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbShowUpdates.CheckedChanged
            Try
                lblRecordCount.Text = "Panel Updated at " & DateTime.Now.ToString

                Me.State.CertHistoryDV = Certificate.GetCertHistoryInfo(Me.State.MyBO.CertNumber, Me.State.MyBO.DealerId, IIf(chbShowUpdates.Checked, "true", "false").ToString())

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
                If Me.State.IsCertHistoryGridVisible Then
                    Me.TranslateGridHeader(Me.CertHistoryGrid)
                    Me.PopulateCertificateHistoryTabInfo()
                End If
                Me.uplCertHistory.Update()
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region "Tab related"
        Private Sub EnableTab(ByVal tabInd As Integer, ByVal blnFlag As Boolean)
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

        Private Function isTabEnabled(ByVal tabInd As Integer) As Boolean
            If listDisabledTabs.Contains(tabInd) = True Then 'tab is diabled
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub EnableDisableTabs(ByVal editMode As Boolean)
            Dim i As Short = 0
            Dim limit As Integer = TAB_TOTAL_COUNT


            If Not editMode Then
                listDisabledTabs.Clear() 'enable all tabs
            Else
                For i = 0 To CType((limit - 1), Short)
                    'enable only the selected tab when in edit mode
                    If i = Me.State.selectedTab Then
                        EnableTab(i, True)
                    Else
                        EnableTab(i, False)
                    End If
                Next
            End If
        End Sub

        Private Sub btnCustProfileHistory_Write_Click(sender As Object, e As EventArgs) Handles btnCustProfileHistory_Write.Click
            Try
                Me.State.NavigateToCustProfileHistory = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.callPage(CustomerProfileHistory.URL, Me.State.MyBO.Id)
                'Me.NavController.Navigate(Me, BILLING_HISTORY_FORM_URL, New EndorsementForm.Parameters(Me.State.MyBO.Id, Me.State.TheItemCoverageState.manufaturerWarranty))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnOutboundCommHistory_Write_Click(sender As Object, e As EventArgs) Handles btnOutboundCommHistory.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.callPage(Tables.OcMessageSearchForm.URL, New Tables.OcMessageSearchForm.CallType("cert_number", Me.State.MyBO.CertNumber, Me.State.MyBO.Id, Me.State.MyBO.DealerId))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewCertRegItem_WRITE__Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCertRegItem_WRITE.Click

            Try
                Me.State.NavigateToNewCertItem = True
                Me.State.isRegItemsGridVisible = True
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.MyBO
                Me.NavController.Navigate(Me, CREATE_NEW_REGISTER_ITEM)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCancelRequestDateTextBox_TextChanged(sender As Object, e As EventArgs) Handles moCancelRequestDateTextBox.TextChanged
            Try
                PopulateCancellationDate()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            If (Me.State.CancelRulesForSFR = Codes.YESNO_Y) Then
                If GetSelectedItem(Me.moCancelRequestReasonDrop).Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, False)
                    moProofOfDocumentationDrop.SelectedIndex = -1
                Else
                    Dim oCancellationReason As New CancellationReason(GetSelectedItem(moCancelRequestReasonDrop))
                    Me.State.CancReasonIsLawful = oCancellationReason.IsLawful
                    If Me.State.CancReasonIsLawful = Codes.EXT_YESNO_Y And (oCancellationReason.Code = Codes.SFR_CR_DEATH Or oCancellationReason.Code = Codes.SFR_CR_MOVINGABROAD) Then
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, True)
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, True)
                        ControlMgr.SetEnableControl(Me, moProofOfDocumentationDrop, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationLabel, False)
                        ControlMgr.SetVisibleControl(Me, moProofOfDocumentationDrop, False)
                        moProofOfDocumentationDrop.SelectedIndex = -1
                    End If
                    Me.moCancelDateTextBox.Text = String.Empty
                    Me.moCancelRequestDateTextBox.Text = String.Empty
                End If
            End If

        End Sub

        Private Sub UpdateBankInfoButton_WRITE_Click(sender As Object, e As EventArgs) Handles UpdateBankInfoButton_WRITE.Click
            Try
                Dim oUpdateBankInfoForRejectsData As New UpdateBankInfoForRejectsData

                Me.MasterPage.MessageController.Clear()

                If IsNothing(Me.State.RefundBankInfoBO) Then
                    Me.State.RefundBankInfoBO = New BankInfo
                End If
                Me.PopulateBOProperty(Me.State.RefundBankInfoBO, "IbanNumber", Me.moRfIBANNumberText)
                Me.PopulateBOProperty(Me.State.RefundBankInfoBO, "Account_Number", Me.moRfAccountNumberText)
                PopulateBOProperty(Me.State.RefundBankInfoBO, "CountryID", Me.State.MyBO.Company.CountryId)

                validateUpdateBankInfoRefundFields()

                With oUpdateBankInfoForRejectsData
                    .certCancellationId = Me.State.MyBO.TheCertCancellationBO.Id
                    .bankInfoId = Me.State.MyBO.TheCertCancellationBO.BankInfoId
                    .accountNumber = Me.State.RefundBankInfoBO.Account_Number
                    .IBANNumber = Me.State.RefundBankInfoBO.IbanNumber
                End With

                State.certCancellationBO.UpdateBankInfoForRejectsSP(oUpdateBankInfoForRejectsData)
                Me.State.MyBO = New Certificate(Me.State.CertificateId)
                PopulateFormFromBOs()
                PopulateCancellationInfoTab()

                Me.WorkingPanelVisible = True
                Me.State.certificateChanged = True
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

                ControlMgr.SetVisibleControl(Me, UpdateBankInfoButton_WRITE, False)

                Me.EnableDisableFields()
            Catch ex As GUIException
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                    userDate = GetDateFormattedStringNullable(Convert.ToDateTime(Me.CancelCertReqDateTextbox.Text))
                    endOfMonth = ComputeEndOfMonth(userDate)
                    CancelCertDateTextbox.Text = GetDateFormattedStringNullable(endOfMonth)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Ibanoperation(ByVal enable As Boolean)

            ControlMgr.SetEnableControl(Me, moUseExistingBankDetailsDrop, False)
            ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsDrop, enable)
            ControlMgr.SetVisibleControl(Me, moUseExistingBankDetailsLabel, enable)
            ControlMgr.SetVisibleControl(Me, moCRIBANNumberLabel, enable)
            ControlMgr.SetVisibleControl(Me, moCRIBANNumberText, enable)
        End Sub

        Private Sub linkPrevCertId_Click(sender As Object, e As EventArgs) Handles linkPrevCertId.Click
            ' Me.State.selectedCertificateId = New Guid(e.CommandArgument.ToString())
            SourceCertificateId = Me.State.MyBO.Id
            Session("SourceCertificateId") = SourceCertificateId

            Me.callPage(CertificateForm.URL, Me.State.MyBO.PreviousCertificateId)
        End Sub

        Private Sub linkOrigCertId_Click(sender As Object, e As EventArgs) Handles linkOrigCertId.Click
            SourceCertificateId = Me.State.MyBO.Id
            Session("SourceCertificateId") = SourceCertificateId
            Me.callPage(CertificateForm.URL, Me.State.MyBO.OriginalCertificateId)
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