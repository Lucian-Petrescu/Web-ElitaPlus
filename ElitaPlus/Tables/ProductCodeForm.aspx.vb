Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic


Namespace Tables

    Partial Class ProductCodeForm
        Inherits ElitaPlusSearchPage
#Region "Page State"
#Region "Constants"
        'Parent Grid
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 1
        Public Const GRID_COL_EFFECTIVE_IDX As Integer = 2
        Public Const GRID_COL_EXPIRATION_IDX As Integer = 3
        Public Const GRID_COL_SMART_BUNDLE_FLAT_AMT As Integer = 4
        Public Const GRID_COL_SMART_BUNDLE_FLAT_AMT_CURRENCY As Integer = 5
        Public Const GRID_COL_PAYMENT_SPLIT_RULE_IDX As Integer = 6
        'child Grid
        Public Const CHILD_GRID_COL_PRODUCT_CODE_IDX As Integer = 2
        Public Const CHILD_GRID_COL_EFFECTIVE_IDX As Integer = 3
        Public Const CHILD_GRID_COL_EXPIRATION_IDX As Integer = 4

#Region "Product Equipment Constants"
        Private Const GRID_COL_PE_ID_IDX As Integer = 1 ' Product Equipment ID
        Private Const GRID_COL_PE_EXPIRATION_IDX As Integer = 5 ' Product Equipment Expiration date
        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_CANCEL As String = "ACTION_CANCEL"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"

        Private Const PE_EXPIRATION_DATE_TEXTBOX_NAME As String = "txtProdEquipmentExpirationDate"
        Private Const PE_EXPIRATION_DATE_IMAGEBUTTON_NAME As String = "ProdEquipmentExpirationDateImageButton"
        ' Property Name
        Private Const EXPIRATION_DATE_PRODUCT_EQUIP_PROPERTY As String = "ExpirationDateProductEquip"
#End Region

#End Region

#Region "MyState"

        Class MyState
            Public moProductCodeId As Guid = Guid.Empty
            Public Parent_Product_code_id As Guid = Guid.Empty

            Public IsProductCodeNew As Boolean = False
            Public oDealer As Dealer
            Public oContract As Contract
            Public isInstallmentOn As Boolean = False
            Public isInstallmentPayment As Boolean = False
            Public moProductCode As ProductCode
            Public ScreenSnapShotBO As ProductCode

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False

            '    Public dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
            Public dealerTypeVSC As Guid
            Public DealerTypeID As Guid
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public dealerInstallmentDefCode As String = String.Empty

            Public PreviousProductPolicySearchDV As ProductPolicy.ProductPolicySearchDV = Nothing
            Public IsProductPolicyEditMode As Boolean
            Public IsChildGridEditMode As Boolean
            Public IsProdPolicyNew As Boolean

            Public ProductPolicySearchDV As ProductPolicy.ProductPolicySearchDV = Nothing
            Public ProductPolicyId As Guid = Guid.Empty
            Public MyProductPolicyBO As ProductPolicy
            Public PageIndex As Integer = 0
            Public ProductPolicySortExpression As String = ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT
            Public IsProductPolicyAfterSave As Boolean
            Public AddingProductPolicyNewRow As Boolean
            Public Company_ID As Guid = Guid.Empty
            Public CountryId As Guid = Guid.Empty
            Public ProductPolicyUsed As Boolean = False
            Public ProductPolicyGridTranslated As Boolean = False
            Public MyProdPolicyChildBO As ProductPolicy

            'Product Rewards
            Public PreviousProductRewardsSearchDV As ProductRewards.ProductRewardsSearchDV = Nothing
            Public PreviousProductBenefitsSearchDV As ProductEquipment.ProductBenefitsSearchDV = Nothing
            Public IsProductRewardsEditMode As Boolean
            Public IsProductBenefitsEditMode As Boolean
            Public IsProdRewardsNew As Boolean
            Public ProductRewardsSearchDV As ProductRewards.ProductRewardsSearchDV = Nothing
            Public ProductBenefitsSearchDV As ProductEquipment.ProductBenefitsSearchDV = Nothing
            Public MyProductBenefitsBO As ProductEquipment = Nothing
            Public ProductRewardsId As Guid = Guid.Empty
            Public MyProductRewardsBO As ProductRewards
            Public ProductRewardsSortExpression As String = ProductRewards.ProductRewardsSearchDV.COL_NAME_FROM_RENEWAL
            Public IsProductRewardsAfterSave As Boolean
            Public IsProductRewardsDelete As Boolean = False
            Public AddingProductRewardsNewRow As Boolean
            Public ProductBenefitsSortExpression As String = ProductEquipment.ProductEquipmentSearchDV.COL_NAME_MANUFACTURER_ID
            Public IsProductBenefitsAfterSave As Boolean
            Public IsProductBenefitsDelete As Boolean = False
            Public AddingProductBenefitsNewRow As Boolean
            Public ProductRewardsGridTranslated As Boolean = False
            Public ExtendedAttributesGridTranslated As Boolean = False
            Public MyProdRewardsDetailChildBO As ProductRewards
            Public MyProdBenefitsDetailChildBO As ProductEquipment
            Public ProdRewardsAddNew As Boolean = False
            Public ProdBenefitsAddNew As Boolean = False
            Public ProdRewardsEdit As Boolean = False
            Public ProdBenefitsEdit As Boolean = False
            'DEF-3066
            '    Public ProductPolicySearchDV As ProductPolicy.ProductPolicySearchDV = Nothing
            Public SelectedEquipmentType As String
            Public SelectedExternalProdCode As String
            Public ProdPolicyAddNew As Boolean = False
            Public ProdPolicyEdit As Boolean = False

            Public IsEditMode As Boolean
            Public IsReadOnly As Boolean = False
            Public Action As String

            'DEF-3066
            Public ProductPolicyAssigned As ProductPolicy.ProductPolicyAssignedDV = Nothing
            Public MyProdPolicyDetailChildBO As ProductPolicy

            Public ModeOperation As String
            Public parentsearchDV As ProductCodeParent.ProductCodeSearchDV = Nothing
            Public childsearchDV As ProductCodeDetail.ProductCodeSearchDV = Nothing
            Public childlistDV As ProductCodeDetail.ProductCodeSearchDV = Nothing
            Public IsProductChildEditMode As Boolean = False
            Public ProdChildAddNew As Boolean = False
            Public SelectedCompany As String
            Public SelectedDealer As String
            Public SelectedChildProd As String
            Public SelectedEffective As String
            Public SelectedExpiration As String
            Public CompanyCode As String
            Public DealerCode As String
            Public ProductCodeDetailId As Guid = Guid.Empty
            Public MyProductChildBO As ProductCodeDetail

            Public ProductEquipmentId As Guid = Guid.Empty
            Public ProductBenefitsId As Guid = Guid.Empty
            Public ProductEquipmentGridTranslated As Boolean = False
            Public ProductEquipmentBenefitsGridTranslated As Boolean = False
            Public AttachChildCount As Integer = 0
            'REQ
            Public moClaimRecordingXcd As String

            'REQ-6289
            Public SelectedProductLimitApplicableToXCD As Guid = Guid.Empty
            'REQ6289-END

            Public DeviceTypeAction As Integer = DeviceTypeNone
            Public ProductEquipmentWorkingItem As ProductEquipment
            Public ProductEquipmentOrig As ProductEquipment
            Public DeviceGroupCode As String
            Public DeviceTypesDetailsGridTranslated As Boolean = False

            'US 327809
            Public PaymentSplitRuleLkl As ListItem() = Nothing

            Public Sub New()
            End Sub
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            'Me.State.moProductCodeId = CType(Me.CallingParameters, Guid)
            State.dealerTypeVSC = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
            State.DealerTypeID = GetDealerTypeID()
            'set the product code as new for the first time only
            If Not State.IsProductCodeNew Then
                If State.moProductCodeId.Equals(Guid.Empty) Then
                    State.IsProductCodeNew = True
                    ClearAll()
                    SetButtonsState(True)
                Else
                    State.IsProductCodeNew = False
                    SetButtonsState(False)
                End If
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Constants"

        Private Const PRODUCTCODE_FORM001 As String = "PRODUCTCODE_FORM001" ' Maintain ProductCode Fetch Exception
        Private Const PRODUCTCODE_FORM002 As String = "PRODUCTCODE_FORM002" ' Maintain ProductCode Update Exception
        '   Private Const PRODUCTCODE_FORM003 As String = "PRODUCTCODE_FORM003" ' ProductCode is Required
        '   Private Const PRODUCTCODE_FORM004 As String = "PRODUCTCODE_FORM004" ' Maximum lenght of ProductCode is 5
        '   Private Const PRODUCTCODE_FORM005 As String = "PRODUCTCODE_FORM005" ' PercentOfRetail is greater or equal to 0 and less or equal to 100

        Private Const PRODUCT_EQUIPMENT_TAB001 As String = "PRODUCT_EQUIPMENT_TAB001" '  Product Equipment list Exception
        Private Const PRODUCT_BENEFITS_TAB001 As String = "PRODUCT_BENEFITS_TAB001" '  Product Equipment list Exception

        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"

        'Private Const PRODUCTCODE_LIST As String = "ProductCodeSearchForm.aspx"
        Public Const URL As String = "ProductCodeForm.aspx"
        'Public Const URL1 As String = "ProductPriceRangeByRepairMethod.aspx"
        ' Property Name
        Public Const PRODUCT_CODE_PROPERTY As String = "ProductCode"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const RISK_GROUP_ID_PROPERTY As String = "RiskGroupId"
        Public Const METHOD_OF_REPAIR_PROPERTY As String = "MethodOfRepairId"
        Public Const TYPE_OF_EQUIPMENT_ID_PROPERTY As String = "TypeOfEquipmentId"
        Public Const PRICE_MATRIX_ID_PROPERTY As String = "PriceMatrixId"
        Public Const USE_DEPRECIATION_PROPERTY As String = "UseDepreciation"
        Public Const PERCENT_OF_RETAIL_PROPERTY As String = "PercentOfRetail"
        Public Const METHOD_OF_REPAIR_BY_PRICE_ID_PROPERTY As String = "MethodOfRepairByPriceId"
        Public Const SPECIAL_SERVICE_PROPERTY As String = "SpecialService"
        Public Const BILLING_FREQUENCY_ID_PROPERTY As String = "BillingFrequencyId"
        Public Const SPLIT_WARRANTY_ID_PROPERTY As String = "SplitWarrantyId"
        Public Const NUMBER_OF_INSTALLMENTS_PROPERTY As String = "NumberOfInstallments"
        Public Const NUMBER_OF_CLAIMS_PROPERTY As String = "NumOfClaims"
        Public Const NUMBER_OF_REPAIR_CLAIMS_PROPERTY As String = "NumOfRepairClaims"
        Public Const NUMBER_OF_REPLACEMENTS_CLAIMS_PROPERTY As String = "NumOfReplacementClaims"
        Public Const BUNDLED_ITEM_ID_PROPERTY As String = "BundledItemId"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const CLAIM_WAITING_PERIOD_PROPERTY As String = "ClaimWaitingPeriod"
        Public Const DAYS_TO_CANCEL_PROPERTY As String = "FullRefundDays"
        Public Const IGNORE_WAITING_PERIOD_WSDPSD As String = "IgnoreWaitingPeriodWsdPsd"
        'REQ-5586 Start
        Public Const PRODUCT_LIABILITY_LIMIT_BASED_ON_PROPERTY As String = "ProductLiabilityLimitBasedOnId"
        Public Const PRODUCT_LIABILITY_LIMIT_POLICY_PROPERTY As String = "ProductLiabilityLimitPolicyId"
        Public Const PRODUCT_LIABILITY_LIMIT_PROPERTY As String = "ProductLiabilityLimitId"
        Public Const PRODUCT_LIABILITY_LIMIT_PERCENT_PROPERTY As String = "ProductLiabilityLimitPercentId"
        'REQ-5586 End
        Public Const UPGRADE_TERM_UOM_PROPERTY As String = "UpgradeTermUomId"
        Public Const UPGRADE_FIXED_TERM_PROPERTY As String = "UpgradeFixedTerm"
        Public Const UPGRADE_TERM_FROM_PROPERTY As String = "UpgradeTermFrom"
        Public Const UPGRADE_TERM_TO_PROPERTY As String = "UpgradeTermTo"
        Public Const UPG_FINANCE_INFO_REQUIRE_PROPERTY As String = "UpgFinanceInfoRequireId"
        Public Const UPG_FINANCE_BAL_COMP_METH_PROPERTY As String = "UPGFinanceBalCompMethId"
        Public Const UPGRADE_PROGRAM_PROPERTY As String = "UpgradeProgramId"

        Public Const CLAIM_AUTO_APPROVE_PSP_PROPERTY As String = "ClaimAutoApprovePsp"

        Public Const COMMENTS_PROPERTY As String = "Comments"
        Private Const LABEL_SELECT_PRODUCTCODE As String = "PRODUCT_CODE"
        Public Const VSCCode As String = "2"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"

        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Private Const PRODUCT_POLICY_ID As Integer = 3
        Private Const TYPE_OF_EQUIPMENT_ID As Integer = 4
        Private Const TYPE_OF_EQUIPMENT As Integer = 0
        Private Const EXTERNAL_PROD_CODE_ID As Integer = 5
        Private Const EXTERNAL_PROD_CODE As Integer = 1
        Private Const POLICY_NUM As Integer = 2


        Private Const REWARD_NAME As Integer = 0
        Private Const REWARD_TYPE As Integer = 1
        Private Const REWARD_AMOUNT As Integer = 2
        Private Const MIN_PURCHASE_PRICE As Integer = 3
        Private Const DAYS_TO_REDEEM As Integer = 4
        Private Const FROM_RENEWAL As Integer = 5
        Private Const TO_RENEWAL As Integer = 6
        Private Const START_DATE As Integer = 7
        Private Const END_DATE As Integer = 8
        Private Const PRODUCT_REWARDS_ID As Integer = 9

        Private Const BENEFIT_CELL_ID As Integer = 0
        Private Const BENEFIT_CELL_MAKE As Integer = 1
        Private Const BENEFIT_CELL_MODEL As Integer = 2
        Private Const BENEFIT_CELL_START_DATE As Integer = 3
        Private Const BENEFIT_CELL_END_DATE As Integer = 4
        Private Const BENEFIT_CELL_CREATED_DATE As Integer = 5
        Private Const BENEFIT_CELL_EQUIPMENT_ID As Integer = 5

        Private Const EFFECTIVE As Integer = 3
        Private Const EXPIRATION As Integer = 4
        Private Const COMPANY_CODE As Integer = 0
        Private Const DEALER_CODE As Integer = 1
        Private Const CHILD_PRODUCT_CODE As Integer = 2
        Private Const PRODUCT_CODE_DETAIL_ID As Integer = 5


        Private Const ID_CONTROL_NAME As String = "IdLabel"
        ' Private Const TYPE_OF_EQUIPMENT_ID_LABEL_CONTROL_NAME As String = "lblTypeOfEquipmentID"
        Private Const TYPE_OF_EQUIPMENT_LABEL_CONTROL_NAME As String = "lblTypeOfEquipment"
        Private Const TYPE_OF_EQUIPMENT_CONTROL_NAME As String = "cboTypeOfEquipmentInGrid"
        ' Private Const EXTERNAL_PROD_CODE_ID_LABEL_CONTROL_NAME As String = "lblExtProdCodeID"
        Private Const EXTERNAL_PROD_CODE_LABEL_CONTROL_NAME As String = "lblExtProdCode"
        Private Const EXTERNAL_PROD_CODE_CONTROL_NAME As String = "cboExtProdCodeInGrid"
        Private Const POLICY_NUM_LABEL_CONTROL_NAME As String = "lblPolicyNum"
        Private Const POLICY_NUM_TEXTBOX_CONTROL_NAME As String = "txtPolicyNum"
        Private Const Is_REINSURED_PROPERTY As String = "IsReInsuredId"

        Private Const REWARD_ID_CONTROL_NAME As String = "IdLabel"
        Private Const REWARD_NAME_LABEL_CONTROL_NAME As String = "lblRewardName"
        Private Const REWARD_NAME_TEXTBOX_CONTROL_NAME As String = "txtRewardName"
        Private Const REWARD_TYPE_LABEL_CONTROL_NAME As String = "lblRewardType"
        Private Const REWARD_TYPE_CONTROL_NAME As String = "cboRewardType"
        Private Const REWARD_AMOUNT_LABEL_CONTROL_NAME As String = "lblRewardAmount"
        Private Const REWARD_AMOUNT_TEXTBOX_CONTROL_NAME As String = "txtRewardAmount"
        Private Const MIN_PURCHASE_PRICE_LABEL_CONTROL_NAME As String = "lblMinPurchasePrice"
        Private Const MIN_PURCHASE_PRICE_TEXTBOX_CONTROL_NAME As String = "txtMinPurchasePrice"
        Private Const DAYS_TO_REDEEM_LABEL_CONTROL_NAME As String = "lblDaysToRedeem"
        Private Const DAYS_TO_REDEEM_TEXTBOX_CONTROL_NAME As String = "txtDaysToRedeem"
        Private Const FROM_RENEWAL_LABEL_CONTROL_NAME As String = "lblFromRenewal"
        Private Const FROM_RENEWAL_TEXTBOX_CONTROL_NAME As String = "txtFromRenewal"
        Private Const TO_RENEWAL_LABEL_CONTROL_NAME As String = "lblToRenewal"
        Private Const TO_RENEWAL_TEXTBOX_CONTROL_NAME As String = "txtToRenewal"
        Private Const START_DATE_IMAGE_BUTTON As String = "btnStartDate"
        Private Const END_DATE_IMAGE_BUTTON As String = "btnEndDate"
        Private Const START_DATE_TEXTBOX As String = "txtStartDate"
        Private Const STRAT_DATE_LABEL_CONTROL_NAME As String = "lblStartDate"
        Private Const END_DATE_TEXTBOX As String = "txtEndDate"
        Private Const END_DATE_LABEL_CONTROL_NAME As String = "lblEndDate"

        Private Const PROD_BENEFITS_EFFECTIVE_DATE_LABEL As String = "lblProdBenefitsEffectiveDate"
        Private Const PROD_BENEFITS_EXPIRATION_DATE_LABEL As String = "lblProdBenefitsExpirationDate"
        Private Const PROD_BENEFITS_EFFECTIVE_DATE_TEXTBOX As String = "txtProdBenefitsEffectiveDate"
        Private Const PROD_BENEFITS_EXPIRATION_DATE_TEXTBOX As String = "txtProdBenefitsExpirationDate"
        Private Const PROD_BENEFITS_ID_LABEL As String = "lblProductBenefitsID"
        Private Const PROD_BENEFITS_MAKE_LABEL As String = "lblEquipmentMake"
        Private Const PROD_BENEFITS_MODEL_LABEL As String = "lblEquipmentModel"
        Private Const PROD_BENEFITS_CREATEDDATE_LABEL As String = "lblProdBenefitsCreateDate"
        Private Const PROD_BENEFITS_MAKE_DROPDOWNLIST As String = "cboEquipmentMakeModel"
        Private Const PROD_BENEFITS_EFFECTIVE_DATE_IMAGE_BUTTON As String = "btnProdBenefitsEffectiveDate"
        Private Const PROD_BENEFITS_EXPIRATION_DATE_IMAGE_BUTTON As String = "btnProdBenefitsExpirationDate"

        Private Const COMPANY_CONTROL_NAME As String = "cboCompany"
        Private Const COMPANY_LABEL_CONTROL_NAME As String = "lblCompanyCode"
        Private Const DEALER_CONTROL_NAME As String = "cboDealer"
        Private Const DEALER_LABEL_CONTROL_NAME As String = "lblDealerCode"
        Private Const PRODUCTCODE_CONTROL_NAME As String = "cboProductCode"
        Private Const PRODUCT_LABEL_CONTROL_NAME As String = "lblProductCode"
        Private Const EFFECTIVE_IMAGE_BUTTON As String = "EffectiveDateImageButton"
        Private Const EXPIRATION_IMAGE_BUTTON As String = "ExpirationDateImageButton"
        Private Const EFFECTIVE_TEXTBOX As String = "EffectiveDateTextBox"
        Private Const EFFECTIVE_LABEL_CONTROL_NAME As String = "EffectiveDateLabel"
        Private Const EXPIRATION_TEXTBOX As String = "ExpirationDateTextBox"
        Private Const EXPIRATION_LABEL_CONTROL_NAME As String = "ExpirationDateLabel"
        Private Const ID_DETAIL_CONTROL_NAME As String = "IdLabelDetail"

        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        'REQ-5586 Start
        Private Const PROD_LIAB_BASED_ON_NOT_APP As String = "NOTAPPL"
        Private Const PORD_LIAB_LIMIT_POLICY_DEFAULT As String = "KEEPCERT"
        'REQ-5586 End

        'REG-6289
        Private Const PRODUCT_LIMIT_APP_TO_XCD As String = "ProductLimitApplicableToXCD"
        Private Const PRODUCT_LIMIT_APP_TO_XCD_DEFAULT As String = "PRODLIMITAPPTO-ALL"
        'REG-6289-END

        'pavan REQ-5733
        Private Const ANALYSIS_CODE_1_PROPERTY As String = "AnalysisCode1"
        Private Const ANALYSIS_CODE_2_PROPERTY As String = "AnalysisCode2"
        Private Const ANALYSIS_CODE_3_PROPERTY As String = "AnalysisCode3"
        Private Const ANALYSIS_CODE_4_PROPERTY As String = "AnalysisCode4"
        Private Const ANALYSIS_CODE_5_PROPERTY As String = "AnalysisCode5"
        Private Const ANALYSIS_CODE_6_PROPERTY As String = "AnalysisCode6"
        Private Const ANALYSIS_CODE_7_PROPERTY As String = "AnalysisCode7"
        Private Const ANALYSIS_CODE_8_PROPERTY As String = "AnalysisCode8"
        Private Const ANALYSIS_CODE_9_PROPERTY As String = "AnalysisCode9"
        Private Const ANALYSIS_CODE_10_PROPERTY As String = "AnalysisCode10"
        Private Const INSTALLMENT_REPRICABLE_PROPERTY As String = "InstallmentRepricableId"

        Private Const BILLING_CRITERIA_PROPERTY As String = "BillingCriteriaId"
        Private Const CANCELLATION_DEPENDENCY_PROPERTY As String = "CnlDependencyId"
        Private Const POST_PRE_PAID_PROPERTY As String = "PostPrePaidId"
        Private Const CNL_LUMPSUM_BILLING_PROPERTY As String = "CnlLumpsumBillingId"
        Private Const UPGRADE_FEE_PROPERTY As String = "UpgradeFee"
        Private Const ALLOW_REGISTERED_ITEM_PROPERTY As String = "AllowRegisteredItems"
        Private Const MAX_AGE_OF_REGISTERED_ITEM_PROPERTY As String = "MaxAgeOfRegisteredItem"
        Private Const MAX_REGISTRATIONS_ALLOWED_PROPERTY As String = "MaxRegistrationsAllowed"
        Private Const LIST_FOR_DEVICE_GROUP_PROPERTY As String = "ListForDeviceGroups"
        'REQ

        Private Const CLAIM_LIMIT_PER_REG_ITEM As String = "ClaimLimitPerRegisteredItem"
        Private Const CANCELLATION_WITHIN_TERM_PROPERTY As String = "CancellationWithinTerm"
        Private Const EXPIRATION_NOTIFICATION_DAYS_PROPERTY As String = "ExpirationNotificationDays"
        'REQ-6002
        Private Const UPDATE_REPLACE_REG_ITEMS_ID_PROPERTY As String = "UpdateReplaceRegItemsId"
        Private Const REGISTERED_ITEMS_LIMIT_PROPERTY As String = "RegisteredItemsLimit"
        'LL: Missing from previous requirement.
        Private Const PRODUCT_EQUIPMENT_VALIDATION_PROPERTY As String = "ProductEquipmentValidation"

        Private Const FulfillmentReimThresholdProperty As String = "FullillmentReimburesementThreshold"
        Private Const INTEGRITY_CONSTRAINT_VIOLATION_MSG As String = "Integrity Constraint Violation"
        Private Const PRICE_MATRIX_USES_WP_XCD_PROPERTY As String = "PriceMatrixUsesWpXcd"
        Private Const EXPECTED_PREMIUM_IS_WP_XCD_PROPERTY As String = "ExpectedPremiumIsWpXcd"



#Region "Tabs"
        Public Const Tab_RegionDetail As String = "0"
        Public Const Tab_ExtProdCode_Policy_WRITE As String = "1"
        Public Const Tab_Accounting_Codes As String = "2"
        Public Const Tab_moAttributes_WRITE As String = "3"
        Public Const Tab_ExtendedAttributes As String = "4"
        Public Const Tab_moProductEquipment As String = "5"
        Public Const tab_moProductRewards As String = "6"
        Public Const tab_DeviceTypeDetails As String = "7"
        Public Const tab_moProductBenefits As String = "8"
        Dim DisabledTabsList As New List(Of String)()

#End Region
#End Region

#Region "Variables"

#End Region

#Region "Attributes"
        Private moProductEquipment As ProductEquipment
        Private moProductBenefits As ProductEquipment
        Private _moDepreciationScdRelation As DepreciationScdRelation
#End Region

#Region "Properties"

        Private ReadOnly Property TheProductCode(Optional ByVal objProd As ProductCode = Nothing) As ProductCode
            Get
                '  If objProd Is Nothing Then
                If State.moProductCode Is Nothing Then
                    If State.IsProductCodeNew = True Then
                        ' For creating, inserting
                        State.moProductCode = New ProductCode
                        State.moProductCodeId = State.moProductCode.Id
                    Else
                        ' For updating, deleting
                        '  Dim oProductCodeId As Guid = Me.GetGuidFromString(Me.State.moProductCodeId)
                        State.moProductCode = New ProductCode(State.moProductCodeId)
                        State.oDealer = New Dealer(State.moProductCode.DealerId)
                        If State.dealerInstallmentDefCode.Equals(String.Empty) Then State.dealerInstallmentDefCode = LookupListNew.GetCodeFromId(LookupListNew.LK_INSTALLMENT_DEFINITION, State.oDealer.UseInstallmentDefnId)
                        If State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE) _
                            OrElse State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE_OR_CONTRACT) Then
                            ControlMgr.SetVisibleControl(Me, pnlInstallment, True)
                            State.isInstallmentOn = True
                        End If
                        'Pouplating Parent Gridview
                        State.parentsearchDV = ProductCodeParent.GetList(State.oDealer.Id, State.moProductCode.Id)

                        'Populating Child Gridview

                        State.childsearchDV = ProductCodeDetail.GetList(State.oDealer.Id, State.moProductCode.Id)
                        State.CountryId = Dealer.GetDealerCountryId(State.oDealer.Id)
                        'Dim oContract As Contract
                        State.oContract = Contract.GetContract(State.moProductCode.DealerId, System.DateTime.Now)
                        If State.oContract IsNot Nothing Then
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.oContract.InstallmentPaymentId) = Codes.YESNO_Y Then
                                'ControlMgr.SetVisibleControl(Me, pnlUpfrontComm, True)
                                State.isInstallmentPayment = True
                            End If
                        End If

                    End If
                End If
                'Else

                'Me.State.moProductCode = objProd
                'End If

                Return State.moProductCode
            End Get
        End Property
        Public ReadOnly Property ProductCodeMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If ProductDropControl Is Nothing Then
                    ProductDropControl = CType(FindControl("ProductDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return ProductDropControl
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
            Get
                If DealerDropControl Is Nothing Then
                    DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return DealerDropControl
            End Get
        End Property

        Public ReadOnly Property GetDealerTypeID() As Guid
            Get
                Dim dealertypeid As Guid
                Dim oDealertypeView As DataView = LookupListNew.GetDealerTypeId(Authentication.CurrentUser.CompanyGroup.Id)
                If oDealertypeView.Count > 0 Then
                    dealertypeid = GuidControl.ByteArrayToGuid(CType(oDealertypeView(FIRST_POS)(COL_NAME), Byte()))
                End If
                Return dealertypeid
            End Get
        End Property
        'Private Property ProductCodeId() As String
        '    Get
        '        Return moProductCodeIdLabel.Text
        '    End Get
        '    Set(ByVal Value As String)
        '        moProductCodeIdLabel.Text = Value
        '    End Set
        'End Property

        'Private Property IsNewProductCode() As Boolean
        '    Get
        '        Return Convert.ToBoolean(moIsNewProductCodeLabel.Text)
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        moIsNewProductCodeLabel.Text = Value.ToString
        '    End Set
        'End Property

        Private Property IsNewProductPolicy() As Boolean
            Get
                Return State.IsProdPolicyNew
            End Get
            Set(Value As Boolean)
                State.IsProdPolicyNew = Value
            End Set
        End Property
#Region "Product Equipment Properties"
        Private ReadOnly Property TheProductEquipment() As ProductEquipment
            Get
                If moProductEquipment Is Nothing Then
                    moProductEquipment = New ProductEquipment(State.ProductEquipmentId)
                End If
                Return moProductEquipment
            End Get
        End Property
        Private ReadOnly Property TheDepreciationSchedule() As DepreciationScdRelation
            Get
                If _moDepreciationScdRelation Is Nothing Then
                    _moDepreciationScdRelation = TheProductCode.GetProductDepreciationScdChild()
                End If
                Return _moDepreciationScdRelation
            End Get
        End Property

#End Region
#Region "Product Benefits Properties"
        Private ReadOnly Property TheProductBenefits() As ProductEquipment
            Get
                If moProductBenefits Is Nothing Then
                    moProductBenefits = New ProductEquipment(State.ProductBenefitsId)
                End If
                Return moProductBenefits
            End Get
        End Property
#End Region
#End Region

#Region "Handlers"

#Region "Handlers-Init, page events"

        ' Protected WithEvents moErrorController As ErrorController

        Protected WithEvents moProductCodeMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moProductCodeDrop As System.Web.UI.WebControls.DropDownList

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)
            State.moProductCodeId = retObj.EditingId
            SetStateProperties()
            'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
            'Me.MSG_TYPE_CONFIRM, True)
            State.LastOperation = DetailPageCommand.Redirect_

            EnableDisableFields()

        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.moProductCodeId = CType(CallingParameters, Guid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ProductCodeForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            'Dim bdirty As Boolean = True
            'bdirty = IsDirtyBO()
            'Me.State.IsDirty = True
            If TheProductCode.Inuseflag = "Y" Then ' The coverage record is in use and should not allow changes except Configuration Super User Roles
                'Display a warning of this record is in use when opening the page first time
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.AddWarning("RECORD_IN_USE")
                End If
                If ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                    'diable the save button to prevent any change to the coverage record
                    btnApply_WRITE.Enabled = False
                    btnDelete_WRITE.Enabled = False
                End If
            End If

            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()

                ' Clear/hide the Product Equipment Error control 
                ErrorControllerProductEquipment.Clear_Hide()

                ClearGridHeaders(ProductEquipmentGridView)
                ClearGridHeaders(ProductBenefitsGridView)

                ' ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                        MSG_TYPE_CONFIRM, True)
                    AttributeValues.ParentBusinessObject = CType(State.moProductCode, IAttributable)
                    'AttributeValues.TranslateHeaders()--Commented since it is calling two times
                    EnableDisableFields()
                    PopulateProductParentGrid()
                    If TheProductCode.ListForDeviceGroups = Guid.Empty Then
                        If DisabledTabsList.Contains(tab_DeviceTypeDetails) = False Then
                            DisabledTabsList.Add(tab_DeviceTypeDetails)
                        End If
                    End If
                Else
                    AttributeValues.ParentBusinessObject = CType(State.moProductCode, IAttributable)

                    If State.IsProductRewardsDelete = True Then
                        CheckIfComingFromDeleteConfirmRewards()
                        State.IsProductRewardsDelete = False
                    ElseIf State.IsProductBenefitsDelete = True Then
                        CheckIfComingFromDeleteConfirmBenefits()
                        State.IsProductBenefitsDelete = False
                    Else
                        CheckIfComingFromDeleteConfirm()
                    End If


                    GetDisabledTabs()
                End If

                AttributeValues.BindBoProperties()
                BindBoPropertiesToLabels()
                BindBoPropertiesToGridHeaders()
                BindBoPropertiesToRewardsGridHeaders()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(TheProductCode)
                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

        Private Sub PopulateProductParentGrid()
            If State.IsProductCodeNew = False AndAlso State.moProductCode.IsParentProduct Then
                mo_ParentsGrid.DataSource = State.parentsearchDV
                If Not State.ExtendedAttributesGridTranslated Then
                    TranslateGridHeader(mo_ParentsGrid)
                    State.ExtendedAttributesGridTranslated = True
                End If
                mo_ParentsGrid.DataBind()
            Else
                ' Add tabs to disable.
                DisabledTabsList.Add(Tab_ExtendedAttributes)
            End If
        End Sub

        Private Sub GetDisabledTabs()
            Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(DisabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnProdRepairPrice_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnProdRepairPrice_WRITE.Click
            Try
                'Me.callPage(ProductCodeForm.URL1, New ProductPriceRangeByRepairMethod.Parameters(Me.State.moProductCodeId))
                callPage(ProductPriceRangeByRepairMethod.URL, State.moProductCodeId)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click

            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moProductCodeId, State.boChanged)
            ReturnToCallingPage(retType)
            'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)

        End Sub

        Private Function IsEditAllowed() As Boolean
            If TheProductCode.Inuseflag = "Y" AndAlso ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.IsProductCodeNew Then
                    'Reload from the DB
                    State.moProductCode = New ProductCode(State.moProductCodeId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.moProductCode.Clone(State.ScreenSnapShotBO)
                Else
                    State.moProductCode = New ProductCode
                End If
                PopulateAll()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moProductCodeId = Guid.Empty
            State.IsProductCodeNew = True
            State.moProductCode = New ProductCode
            ClearAll()
            ' disable the parent child tab
            DisabledTabsList.Add(Tab_ExtendedAttributes)
            hdnSelectedTab.Value = 0

            SetButtonsState(True)
            PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)
            ControlMgr.SetVisibleControl(Me, pnlInstallment, False)
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()

            PopulateBOsFromForm()

            ' Dim newObjDummy As New ProductCode
            Dim newObj As New ProductCode
            newObj.Copy(TheProductCode)
            newObj.Inuseflag = "N"

            State.moProductCode = newObj
            'newObjDummy = TheProductCode(newObj)

            State.moProductCodeId = Guid.Empty
            State.IsProductCodeNew = True

            With TheProductCode '(newObj)
                .ProductCode = Nothing
            End With

            '  Me.EnableDisableFields()
            ' disable the parent child tab
            DisabledTabsList.Add(Tab_ExtendedAttributes)
            hdnSelectedTab.Value = 0

            SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            State.ScreenSnapShotBO = New ProductCode
            State.ScreenSnapShotBO.Copy(TheProductCode)

            State.ProductPolicySearchDV = Nothing
            PopulateMyProductPolicyGrid()
            PopulateMyProductRewardsGrid()

            ' clear the product equipment tab
            ClearProductEquipment()

        End Sub


        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteProductCode() = True Then
                    State.boChanged = True
                    'Dim param As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                    '                Me.State.moProductCodeId)
                    'param.BoChanged = True
                    'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)
                    Dim retType As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moProductCodeId, State.boChanged)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                        Handles DealerDropControl.SelectedDropChanged
            Try
                PopulateProductDepreciationScheduleDropdown()
                PopulateUserControlAvailableSelectedDealer()
                PopulateUserControlAvailableSelectedRegions()
                PopulateDeviceType()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            moProductCodeText.Text = Nothing
            moDescriptionText.Text = Nothing
            moPercentOfRetailText.Text = Nothing
            moInstallmentText.Text = Nothing
            moProdLiabilityLimitText.Text = Nothing
            moProdLiabilityLimitPercentText.Text = Nothing
            moUpgradeTermText.Text = Nothing
            moUpgradeTermFROMText.Text = Nothing
            moUpgradeTermToText.Text = Nothing
            moNumOfRepairClaimsText.Text = Nothing
            moNumOfReplacementsText.Text = Nothing
            moNumOfReplClaimsText.Text = Nothing
            moUpgFeeText.Text = Nothing
            moMaxAgeOfRegisteredItemText.Text = Nothing
            moMaxRegistrationsAllowedText.Text = Nothing
            moClaimLimitPerRegisteredItemText.Text = Nothing
            TextBoxFulfillmentReimThresholdValue.Text = Nothing
            moPerIncidentLiabilityLimitCapText.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            'ClearList(moDealerDrop)
            TheDealerControl.ClearMultipleDrop()
            ClearList(moRiskGroupDrop)
            ClearList(moMethodOfRepairDrop)
            ClearList(moSplitWarrantyDrop)
            'Amod
            ClearList(moUPGFinanceBalCompMethDrop)
            ClearList(moUpgradeProgramDrop)
            'Amod
            ClearList(moupgFinanceInfoRequireDrop)
            ClearList(moUpgTermUOMDrop)
            ClearList(moTypeOfEquipmentDrop)
            ClearList(moPriceMatrixDrop)
            ClearList(moUseDepreciationDrop)
            ClearList(moBundledItemId)
            ClearList(moBillingFrequencyId)
            ClearList(moProdLiabilityLimitBasedOnDrop)
            ClearList(moProdLiabilityLimitPolicyDrop)
            ClearList(moProdLimitApplicableToXCDDrop)
            State.ProductPolicySearchDV = Nothing
            ClearList(moInstallmentRepricableDrop)
            ClearProductEquipment() ' Clear the product equipment tab
            ClearProductEquipmentBenefits() ' Clear the product-equipment-benefits tab
            cboProductEquipmentValidation.ClearSelection()
            cboAllowRegisteredItems.ClearSelection()
            ClearList(cboListForDeviceGroup)

            'REQ-6002
            ClearList(cboUpdateReplaceRegItemsId)

            ClearList(ddlDepSchCashReimbursement)
            'Benefits
            ClearList(moBenefitsEligibleFlagDropDownList)
            ClearList(moBenefitsEligibleActionDropDownList)

            ClearList(ddlClaimProfile)
            ClearList(ddlCalcCovgEndDateBasedOn)
            cboPriceMatrixUsesWpXcd.ClearSelection()
            cboExpectedPremiumIsWpXcd.ClearSelection()

        End Sub
        Private Sub ClearProductEquipment()
            If State.IsProductCodeNew Then
                EnableEditRateButtons(False)
                ProductEquipmentGridView.DataBind()
            End If
        End Sub
        Private Sub ClearProductEquipmentBenefits()
            If State.IsProductCodeNew Then
                EnableEditBenefitsButtons(False)
                ProductBenefitsGridView.DataBind()
            End If
        End Sub
#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Product_Code")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Product_Code")
                End If
            End If
        End Sub

        Private Sub PopulateDealer()
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
                If State.IsProductCodeNew = True Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    TheDealerControl.SelectedGuid = TheProductCode.DealerId
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(PRODUCTCODE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim oVSCPlanview As DataView
            'Dim oYesNoDataView As DataView = LookupListNew.GetYesNoLookupList(oLanguageId)
            Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            Try
                'BindListControlToDataView(moRiskGroupDrop, LookupListNew.GetRiskGroupsLookupList(oLanguageId), , , True)
                Dim riskGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode())
                moRiskGroupDrop.Populate(riskGroupLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moMethodOfRepairDrop, LookupListNew.GetMethodOfRepairLookupList(oLanguageId), , , True)
                Dim methodofRepairLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode())
                moMethodOfRepairDrop.Populate(methodofRepairLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moTypeOfEquipmentDrop, LookupListNew.GetTypeOfEquipmentLookupList(oLanguageId), , , True)
                Dim typeOfEquipmentLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode())
                moTypeOfEquipmentDrop.Populate(typeOfEquipmentLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moPriceMatrixDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                moPriceMatrixDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moUseDepreciationDrop, oYesNoDataView, , , True)
                moUseDepreciationDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moMethodOfRepairByPriceDrop, oYesNoDataView, , , True)
                moMethodOfRepairByPriceDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moBundledItemId, oYesNoDataView, , , False)
                moBundledItemId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })

                'BindListControlToDataView(moSplitWarrantyDrop, oYesNoDataView, , , True)
                moSplitWarrantyDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moUPGFinanceBalCompMethDrop, LookupListNew.GetMethodOfFinBalForUpgList(oLanguageId), , , True)
                Dim upgFinanceBalCompMethLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UPG_FINANCE_BAL_COMP_METH", Thread.CurrentPrincipal.GetLanguageCode())
                moUPGFinanceBalCompMethDrop.Populate(upgFinanceBalCompMethLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moReInsuredDrop, oYesNoDataView, , , True)
                moReInsuredDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moupgFinanceInfoRequireDrop, LookupListNew.GetUpgFinanceInfoRequireLookupList(oLanguageId), , , True)
                Dim upgFinanceInfoReqLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UPG_FINANCE_INFO_REQUIRE", Thread.CurrentPrincipal.GetLanguageCode())
                moupgFinanceInfoRequireDrop.Populate(upgFinanceInfoReqLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moUpgTermUOMDrop, LookupListNew.GetUpgradeTermUnitOfMeasureLookupList(oLanguageId), , , True)
                Dim upgtermUnitOfMeasureLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UPG_TERM_UNIT_OF_MEASURE", Thread.CurrentPrincipal.GetLanguageCode())
                moUpgTermUOMDrop.Populate(upgtermUnitOfMeasureLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moUpgradeProgramDrop, oYesNoDataView, , , True)
                moUpgradeProgramDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'moBenefitsEligibleFlagDropDownList.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moBenefitsEligibleFlagDropDownList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode,
                                                        .SortFunc = AddressOf .GetDescription
                                                       })

                'BindCodeToListControl(moBenefitsEligibleActionDropDownList, LookupListNew.GetBenefitsEligibleActionList(oLanguageId), , , True)
                Dim benefitsEligibleActionLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("BENEFITS_ELIGIBLE_ACTION", Thread.CurrentPrincipal.GetLanguageCode())
                moBenefitsEligibleActionDropDownList.Populate(benefitsEligibleActionLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim calcCovgEndDateBasedOnLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CALC_COVG_END_DATE_BASED_ON", Thread.CurrentPrincipal.GetLanguageCode())
                ddlCalcCovgEndDateBasedOn.Populate(calcCovgEndDateBasedOnLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                'BindListControlToDataView(cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, oYesNoDataView, , , True)
                cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moProdLiabilityLimitBasedOnDrop, LookupListNew.GetProdLiabilityLimitBasedOnList(oLanguageId), , , True)
                Dim productLiabilityLmtLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRODLILIMBASEDON", Thread.CurrentPrincipal.GetLanguageCode())
                moProdLiabilityLimitBasedOnDrop.Populate(productLiabilityLmtLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moProdLiabilityLimitPolicyDrop, LookupListNew.GetProdLiabilityLimitPolicyList(oLanguageId), , , True)
                Dim productLiabLmtPolicyLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRODLILIMPOLICY", Thread.CurrentPrincipal.GetLanguageCode())
                moProdLiabilityLimitPolicyDrop.Populate(productLiabLmtPolicyLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'REQ-6289
                'BindCodeToListControl(moProdLimitApplicableToXCDDrop, LookupListNew.GetProdLimitAppToXCDList(oLanguageId), , , True)
                Dim prodLimitApplicableLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRODLIMITAPPTO", Thread.CurrentPrincipal.GetLanguageCode())
                moProdLimitApplicableToXCDDrop.Populate(prodLimitApplicableLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

                'REQ-6289-END
                'BindListControlToDataView(Me.moBillingFrequencyId, LookupListNew.GetBillingFrequencyList(oLanguageId), , , )
                Dim billingFrequencyLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("BLFQ", Thread.CurrentPrincipal.GetLanguageCode())
                moBillingFrequencyId.Populate(billingFrequencyLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(moInstallmentRepricableDrop, oYesNoDataView, , , True)
                moInstallmentRepricableDrop.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                moPercentOfRetailText.Text = GetAmountFormattedDoubleString("0")

                'Me.cboProductEquipmentValidation.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                cboProductEquipmentValidation.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                'Me.cboAllowRegisteredItems.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                cboAllowRegisteredItems.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                If State.DealerTypeID.Equals(State.dealerTypeVSC) Then
                    oVSCPlanview = LookupListNew.GetVSCPlanLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                    ProductCodeMultipleDrop.NothingSelected = True
                    ProductCodeMultipleDrop.SetControl(False,
                                                       ProductCodeMultipleDrop.MODES.NEW_MODE,
                                                      True,
                                                      oVSCPlanview,
                                                      "*" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_PRODUCTCODE),
                                                        True, True, ,
                                                        "multipleDropControl_moMultipleColumnDrop",
                                                        "multipleDropControl_moMultipleColumnDropDesc",
                                                        "multipleDropControl_lb_DropDown",
                                                        False,
                                                        0)
                End If

                If State.IsProductCodeNew = True Then
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N").ToString, moUseDepreciationDrop)
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N").ToString, moBundledItemId)
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N").ToString, moSplitWarrantyDrop)
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N").ToString, moUpgradeProgramDrop)
                End If

                'BindListControlToDataView(Me.moBillingCriteriaDrop, LookupListNew.GetBillingCriteriaList(oLanguageId), , , )
                Dim billingCriteriaLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("BLCR", Thread.CurrentPrincipal.GetLanguageCode())
                moBillingCriteriaDrop.Populate(billingCriteriaLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(Me.moCancellationDependencyDrop, LookupListNew.GetCancellationDependencyList(oLanguageId), , , )
                Dim CancellationDependencyLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CNLDEP", Thread.CurrentPrincipal.GetLanguageCode())
                moCancellationDependencyDrop.Populate(CancellationDependencyLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(Me.moPostPrePaidDrop, LookupListNew.GetPostPrePaidLookupList(oLanguageId), , , )
                Dim postprepaidLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("POSTPRE", Thread.CurrentPrincipal.GetLanguageCode())
                moPostPrePaidDrop.Populate(postprepaidLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(Me.moCnlLumpsumBillingDrop, LookupListNew.GetCancellationLumpsumBillingList(oLanguageId), , , )
                Dim cancellationLumpsumBillingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CNLSBL", Thread.CurrentPrincipal.GetLanguageCode())
                moCnlLumpsumBillingDrop.Populate(cancellationLumpsumBillingLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'BindListControlToDataView(cboListForDeviceGroup, LookupListNew.GetDeviceGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                Dim deviceGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DeviceGroups", Thread.CurrentPrincipal.GetLanguageCode())
                cboListForDeviceGroup.Populate(deviceGroupLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'REQ-6002
                'BindListControlToDataView(cboUpdateReplaceRegItemsId, LookupListNew.GetUpdateReplaceRegisteredItemsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                Dim UpdateReplaceRegisteredItmsLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RIUR", Thread.CurrentPrincipal.GetLanguageCode())
                cboUpdateReplaceRegItemsId.Populate(UpdateReplaceRegisteredItmsLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                'REQ-5980
                'Currency
                'Me.BindListControlToDataView(Me.ddlSmartBundleCurrency, LookupListNew.GetCurrencyTypeLookupList(), , , False)
                'Me.AddCalendarwithTime_New(btneffective, txtEffective, , txtEffective.Text)
                'Me.AddCalendarwithTime_New(btnExpiration, txtExpiration, , txtExpiration.Text)

                'REQ
                'BindListTextToDataView(Me.cboInitialQuestionSet, LookupListNew.GetDcmQuestionSetLookupList(), "DESCRIPTION", "code", True)

                'Me.cboCancellationWithinTerm.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                cboCancellationWithinTerm.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                PopulateProductDepreciationScheduleDropdown()
                Dim claimProfile As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClaimProfile")
                ddlClaimProfile.Populate(claimProfile, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf .GetCode
                                                       })

                State.PaymentSplitRuleLkl = CommonConfigManager.Current.ListManager.GetList("PAYSPLITRULE", Thread.CurrentPrincipal.GetLanguageCode())


                cboExpectedPremiumIsWpXcd.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })
                cboPriceMatrixUsesWpXcd.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

            Catch ex As Exception
                MasterPage.MessageController.AddError(PRODUCTCODE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub
        Private Sub PopulateProductDepreciationScheduleDropdown()
            If State.oDealer IsNot Nothing Then
                'Dim dv As DataView = DepreciationScd.LoadList(State.oDealer.CompanyId)
                'dv.RowFilter = " (active_xcd = 'YESNO-Y' Or code = '" & TheDepreciationSchedule.DepreciationScheduleCode & "')"
                'BindListControlToDataView(ddlDepSchCashReimbursement, dv, "code", "depreciation_schedule_id")

                Dim oListContext As New ListContext
                oListContext.CompanyId = State.oDealer.CompanyId
                Dim depreciationScheduleListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DepreciationScheduleByCompany", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                Dim filteredDepreciationScheduleListForCompany As DataElements.ListItem() = (From lst In depreciationScheduleListForCompany
                                                                                             Where lst.ExtendedCode = "YESNO-Y" OrElse lst.Code = TheDepreciationSchedule.DepreciationScheduleCode
                                                                                             Select lst).ToArray()
                ddlDepSchCashReimbursement.Populate(filteredDepreciationScheduleListForCompany, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .TextFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })
            End If
        End Sub
        Private Sub PopulateTexts()
            Try

                With TheProductCode
                    If State.IsProductCodeNew = True Then
                        BindSelectItem(Nothing, moRiskGroupDrop)
                        BindSelectItem(Nothing, moMethodOfRepairDrop)
                        BindSelectItem(Nothing, moTypeOfEquipmentDrop)
                        BindSelectItem(Nothing, moPriceMatrixDrop)
                        BindSelectItem(Nothing, moBenefitsEligibleFlagDropDownList)
                        BindSelectItem(.UseDepreciation.ToString, moUseDepreciationDrop)
                        BindSelectItem(.BundledItemId.ToString, moBundledItemId)
                        BindSelectItem(Nothing, moMethodOfRepairByPriceDrop)
                        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                        SetSelectedItem(moMethodOfRepairByPriceDrop, noId)
                        BindSelectItem(Nothing, moSplitWarrantyDrop)
                        Dim billFreq As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_FREQUENCY, Codes.MONTHLY)
                        SetSelectedItem(moBillingFrequencyId, billFreq)
                        Dim oCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_BILLING_FREQUENCY, billFreq)
                        PopulateControlFromBOProperty(moInstallmentText, oCode)
                        ControlMgr.SetVisibleControl(Me, pnlInstallment, False)
                        ControlMgr.SetVisibleControl(Me, pnlMethdOfRepair_Replacement, False)

                        BindSelectItem(Nothing, moProdLiabilityLimitBasedOnDrop)
                        BindSelectItem(Nothing, moProdLiabilityLimitPolicyDrop)

                        txtAutoApprovePSP.Text = Nothing
                        moUpgradeTermText.Text = Nothing
                        BindSelectItem(Nothing, moUPGFinanceBalCompMethDrop)
                        BindSelectItem(Nothing, moUpgradeProgramDrop)
                        BindSelectItem(Nothing, moReInsuredDrop)
                        BindSelectItem(Nothing, moupgFinanceInfoRequireDrop)
                        BindSelectItem(Nothing, moUpgTermUOMDrop)
                        BindSelectItem(Nothing, moInstallmentRepricableDrop)
                        BindSelectItem(Nothing, moBillingCriteriaDrop)
                        BindSelectItem(Nothing, moCancellationDependencyDrop)
                        BindSelectItem(.ProductEquipmentValidation, cboProductEquipmentValidation)
                        BindSelectItem(.AllowRegisteredItems, cboAllowRegisteredItems)
                        BindSelectItem(Nothing, cboListForDeviceGroup)
                        'REQ

                        'REQ-6289
                        BindSelectItem(Nothing, moProdLimitApplicableToXCDDrop)
                        'REQ-6289-END
                        'REQ-6002
                        BindSelectItem(Nothing, cboUpdateReplaceRegItemsId)
                        txtRegisteredItemsLimit.Text = Nothing
                        BindSelectItem(Nothing, cboCancellationWithinTerm)
                        moExpNotificationDaysText.Text = Nothing

                        TextBoxFulfillmentReimThresholdValue.Text = Nothing

                        BindSelectItem(Nothing, ddlDepSchCashReimbursement)

                        BindSelectItem(Nothing, ddlClaimProfile)
                        BindSelectItem(.PriceMatrixUsesWpXcd, cboPriceMatrixUsesWpXcd)
                        BindSelectItem(.ExpectedPremiumIsWpXcd, cboExpectedPremiumIsWpXcd)
                    Else
                        BindSelectItem(.RiskGroupId.ToString, moRiskGroupDrop)
                        BindSelectItem(.MethodOfRepairId.ToString, moMethodOfRepairDrop)
                        BindSelectItem(.TypeOfEquipmentId.ToString, moTypeOfEquipmentDrop)
                        BindSelectItem(.PriceMatrixId.ToString, moPriceMatrixDrop)

                        If TheProductCode.BenefitEligibleFlagXCD IsNot Nothing Then
                            BindSelectItem(.BenefitEligibleFlagXCD.ToString, moBenefitsEligibleFlagDropDownList)
                        End If

                        If TheProductCode.BenefitEligibleActionXCD IsNot Nothing Then
                            BindSelectItem(.BenefitEligibleActionXCD.ToString, moBenefitsEligibleActionDropDownList)
                        End If

                        If TheProductCode.CalcCovgEndDateBasedOnXCD IsNot Nothing Then
                            BindSelectItem(.CalcCovgEndDateBasedOnXCD.ToString, ddlCalcCovgEndDateBasedOn)
                        End If

                        BindSelectItem(.UseDepreciation.ToString, moUseDepreciationDrop)
                        BindSelectItem(.BundledItemId.ToString, moBundledItemId)
                        BindSelectItem(.SplitWarrantyId.ToString, moSplitWarrantyDrop)
                        BindSelectItem(.BillingFrequencyId.ToString, moBillingFrequencyId)
                        BindSelectItem(.ProdLiabilityLimit, moProdLimitApplicableToXCDDrop)

                        If State.DealerTypeID.Equals(State.dealerTypeVSC) Then
                            Dim oPlanview As DataView = LookupListNew.GetVSCPlanLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                            ProductCodeMultipleDrop.SelectedGuid = LookupListNew.GetIdFromCode(oPlanview, TheProductCode.ProductCode)
                        Else
                            moProductCodeText.Text = .ProductCode
                            moDescriptionText.Text = .Description
                        End If

                        If TheProductCode.PercentOfRetail Is Nothing Then
                            moPercentOfRetailText.Text = GetAmountFormattedDoubleString("0")
                        Else
                            moPercentOfRetailText.Text = GetAmountFormattedDoubleString(TheProductCode.PercentOfRetail.ToString)
                        End If

                        'BindSelectItem(.UpfrontCommissionId.ToString, moUpfrontCommId)
                        BindSelectItem(.MethodOfRepairByPriceId.ToString, moMethodOfRepairByPriceDrop)
                        BindSelectItem(.ProdLiabilityLimitBasedOnId.ToString, moProdLiabilityLimitBasedOnDrop)
                        BindSelectItem(.ProdLiabilityLimitPolicyId.ToString, moProdLiabilityLimitPolicyDrop)

                        'REQ-6289
                        If .ProductLimitApplicableToXCD IsNot Nothing AndAlso Not String.IsNullOrEmpty(.ProductLimitApplicableToXCD) Then
                            BindSelectItem(.ProductLimitApplicableToXCD.ToString, moProdLimitApplicableToXCDDrop)
                        End If
                        'REQ-6289-END

                        BindSelectItem(.IsReInsuredId.ToString, moReInsuredDrop)

                        moCommentsText.Text = .Comments
                        moSpecialServiceText.Text = .SpecialService
                        PopulateControlFromBOProperty(moInstallmentText, .NumberOfInstallments)
                        PopulateControlFromBOProperty(moNumOfReplacementsText, .NumOfClaims)
                        PopulateControlFromBOProperty(moNumOfRepairClaimsText, .NumOfRepairClaims)
                        PopulateControlFromBOProperty(moNumOfReplClaimsText, .NumOfReplacementClaims)
                        PopulateControlFromBOProperty(moDaysToCancel, .FullRefundDays)

                        'pavan REQ-5733
                        PopulateControlFromBOProperty(moAnalysisCode1Text, .AnalysisCode1)
                        PopulateControlFromBOProperty(moAnalysisCode2Text, .AnalysisCode2)
                        PopulateControlFromBOProperty(moAnalysisCode3Text, .AnalysisCode3)
                        PopulateControlFromBOProperty(moAnalysisCode4Text, .AnalysisCode4)
                        PopulateControlFromBOProperty(moAnalysisCode5Text, .AnalysisCode5)
                        PopulateControlFromBOProperty(moAnalysisCode6Text, .AnalysisCode6)
                        PopulateControlFromBOProperty(moAnalysisCode7Text, .AnalysisCode7)
                        PopulateControlFromBOProperty(moAnalysisCode8Text, .AnalysisCode8)
                        PopulateControlFromBOProperty(moAnalysisCode9Text, .AnalysisCode9)
                        PopulateControlFromBOProperty(moAnalysisCode10Text, .AnalysisCode10)
                        PopulateControlFromBOProperty(moMaxAgeOfRegisteredItemText, .MaxAgeOfRegisteredItem)
                        PopulateControlFromBOProperty(moMaxRegistrationsAllowedText, .MaxRegistrationsAllowed)
                        PopulateControlFromBOProperty(moClaimLimitPerRegisteredItemText, .MaxClaimsAllowedPerRegisteredItem)

                        BindSelectItem(.UPGFinanceBalCompMethId.ToString, moUPGFinanceBalCompMethDrop)
                        BindSelectItem(.UpgradeProgramId.ToString, moUpgradeProgramDrop)
                        BindSelectItem(.InstallmentRepricableId.ToString, moInstallmentRepricableDrop)

                        BindSelectItem(.BillingCriteriaId.ToString, moBillingCriteriaDrop)
                        BindSelectItem(.CnlDependencyId.ToString, moCancellationDependencyDrop)
                        BindSelectItem(.PostPrePaidId.ToString, moPostPrePaidDrop)
                        BindSelectItem(.CnlLumpsumBillingId.ToString, moCnlLumpsumBillingDrop)

                        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                        If .UpgradeProgramId.Equals(Guid.Empty) OrElse .UpgradeProgramId.Equals(noId) Then
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                            ControlMgr.SetVisibleControl(Me, moUpgTermUOMLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUpgTermUOMDrop, False)
                            ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireLabel, False)
                            ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireDrop, False)
                            ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethDrop, False)
                            ControlMgr.SetVisibleControl(Me, moUpgFeeLabel, False)
                            ControlMgr.SetVisibleControl(Me, moUpgFeeText, False)
                        Else
                            BindSelectItem(.UpgradeTermUomId.ToString, moUpgTermUOMDrop)
                            BindSelectItem(.UpgFinanceInfoRequireId.ToString, moupgFinanceInfoRequireDrop)
                            If Not .UpgradeTermUomId.Equals(Guid.Empty) Then
                                Dim strUpgradeTermUomCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_UPGRADE_TERM_UNIT_OF_MEASURE, .UpgradeTermUomId)
                                If strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_DAYS) OrElse strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_MONTHS) Then
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, True)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermText, True)
                                    PopulateControlFromBOProperty(moUpgradeTermText, .UpgradeFixedTerm)
                                ElseIf strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_DAYS) OrElse strUpgradeTermUomCode.Equals(Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_MONTHS) Then
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, True)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, True)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, True)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, True)
                                    PopulateControlFromBOProperty(moUpgradeTermFROMText, .UpgradeTermFrom)
                                    PopulateControlFromBOProperty(moUpgradeTermToText, .UpgradeTermTo)
                                Else
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                                    ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                                End If
                            Else
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                            End If
                        End If

                        PopulateControlFromBOProperty(moWaitingPeriod, .ClaimWaitingPeriod)
                        BindSelectItem(.IgnoreWaitingPeriodWsdPsd.ToString, cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD)

                        If .ProdLiabilityLimit Is Nothing OrElse .ProdLiabilityLimit.ToString() = String.Empty Then
                            PopulateControlFromBOProperty(moProdLiabilityLimitText, "0")
                        Else
                            PopulateControlFromBOProperty(moProdLiabilityLimitText, .ProdLiabilityLimit)
                        End If

                        If .PerIncidentLiabilityLimitCap Is Nothing OrElse .PerIncidentLiabilityLimitCap.ToString() = String.Empty Then
                            PopulateControlFromBOProperty(moPerIncidentLiabilityLimitCapText, "0")
                        Else
                            PopulateControlFromBOProperty(moPerIncidentLiabilityLimitCapText, .PerIncidentLiabilityLimitCap)
                        End If

                        PopulateControlFromBOProperty(moProdLiabilityLimitPercentText, .ProdLiabilityLimitPercent)

                        If (GetSelectedItem(moProdLiabilityLimitBasedOnDrop).Equals(Guid.Empty) OrElse GetSelectedItem(moProdLiabilityLimitBasedOnDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) Then
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyLabel, False)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyDrop, False)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitLabel, False)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitText, False)
                            ControlMgr.SetVisibleControl(Me, moPerIncidentLiabilityLimitCapLabel, False)
                            ControlMgr.SetVisibleControl(Me, moPerIncidentLiabilityLimitCapText, False)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentLabel, False)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentText, False)
                            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDLabel, False)
                            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDDrop, False)
                        Else
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyLabel, True)
                            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyDrop, True)
                            If GetSelectedItem(moProdLiabilityLimitPolicyDrop).Equals(Guid.Empty) Then
                                SetSelectedItem(moProdLiabilityLimitPolicyDrop, LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_POLICY_TYPES, PORD_LIAB_LIMIT_POLICY_DEFAULT))
                            End If
                            'REG-6289
                            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDLabel, True)
                            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDDrop, True)
                            If String.IsNullOrEmpty(GetSelectedValue(moProdLimitApplicableToXCDDrop)) Then
                                SetSelectedItem(moProdLimitApplicableToXCDDrop, PRODUCT_LIMIT_APP_TO_XCD_DEFAULT)
                            End If
                            'REG-6289-END
                        End If
                        If .ClaimAutoApprovePsp IsNot Nothing AndAlso .ClaimAutoApprovePsp.Value > 0 Then
                            txtAutoApprovePSP.Text = .ClaimAutoApprovePsp.ToString
                        End If

                        BindSelectItem(.ProductEquipmentValidation, cboProductEquipmentValidation)
                        BindSelectItem(.AllowRegisteredItems, cboAllowRegisteredItems)
                        BindSelectItem(.ListForDeviceGroups.ToString, cboListForDeviceGroup)
                        PopulateControlFromBOProperty(moUpgFeeText, .UpgradeFee)
                        'REQ

                        'REQ-6002
                        BindSelectItem(.UpdateReplaceRegItemsId.ToString, cboUpdateReplaceRegItemsId)
                        PopulateControlFromBOProperty(txtRegisteredItemsLimit, .RegisteredItemsLimit)
                        BindSelectItem(.CancellationWithinTerm, cboCancellationWithinTerm)
                        PopulateControlFromBOProperty(moExpNotificationDaysText, .ExpirationNotificationDays)
                        PopulateControlFromBOProperty(TextBoxFulfillmentReimThresholdValue, .FullillmentReimburesementThreshold)
                        BindSelectItem(TheDepreciationSchedule.DepreciationScheduleId.ToString, ddlDepSchCashReimbursement)
                        BindSelectItem(.ClaimProfile, ddlClaimProfile)
                        BindSelectItem(.PriceMatrixUsesWpXcd, cboPriceMatrixUsesWpXcd)
                        BindSelectItem(.ExpectedPremiumIsWpXcd, cboExpectedPremiumIsWpXcd)
                    End If

                    ShowHideBenefitsControls(TheProductCode.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y)

                End With

                'BindSelectItem(Me.State.DepreciationScdRelationBO.DepreciationScheduleId.ToString, ddlDepSchCashReimbursement)

                If (AttributeValues.ParentBusinessObject Is Nothing) Then
                    AttributeValues.ParentBusinessObject = CType(State.moProductCode, IAttributable)
                    AttributeValues.TranslateHeaders()
                End If

                AttributeValues.DataBind()

                'REQ-5888-START
                'If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) Or Me.State.moProductCode.IsReInsuredId.Equals(Guid.Empty) Then
                '    tab1_moAttributes_WRITE.Enabled = False
                '    AttributeValues.Visible = False
                'End If
                'REQ-5888-END
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PopulatedefaultProdliabilityLimitbasedvalues()
            SetSelectedItem(moProdLiabilityLimitBasedOnDrop, LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP))
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyLabel, False)
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyDrop, False)
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitLabel, False)
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitText, False)
            ControlMgr.SetVisibleControl(Me, moPerIncidentLiabilityLimitCapLabel, False)
            ControlMgr.SetVisibleControl(Me, moPerIncidentLiabilityLimitCapText, False)
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentLabel, False)
            ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentText, False)
            'REG-6289
            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDLabel, False)
            ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDDrop, False)
            SetSelectedItem(moProdLimitApplicableToXCDDrop, PRODUCT_LIMIT_APP_TO_XCD_DEFAULT)
            'REG-6289-END
        End Sub

        Private Sub PopulateAll()
            If State.IsProductCodeNew = True Then
                PopulateDropDowns()
                PopulateDealer()
                PopulateUserControlAvailableSelectedRegions()
                PopulateDeviceType()
                PopulateMyProductPolicyGrid()
                PopulateMyProductRewardsGrid()
                PopulatedefaultProdliabilityLimitbasedvalues()
            Else
                ClearAll()
                PopulateDealer()
                PopulateDropDowns()
                PopulateTexts()
                PopulateUserControlAvailableSelectedRegions()
                PopulateDeviceType()
                PopulateMyProductPolicyGrid()
                PopulateMyProductRewardsGrid()
                PopulateProductEquipmentGrid()
                PopulateProductBenefitsGrid()
                PopulateProductParentGrid()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()

            ' ApplyAllProductPolicyChanges()


            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            With TheProductCode
                .DealerId = TheDealerControl.SelectedGuid
                'Me.PopulateBOProperty(TheProductCode, "DealerId", Me.moDealerDrop)
                PopulateBOProperty(TheProductCode, "RiskGroupId", moRiskGroupDrop)
                PopulateBOProperty(TheProductCode, "MethodOfRepairId", moMethodOfRepairDrop)
                PopulateBOProperty(TheProductCode, "TypeOfEquipmentId", moTypeOfEquipmentDrop)
                PopulateBOProperty(TheProductCode, "PriceMatrixId", moPriceMatrixDrop)
                PopulateBOProperty(TheProductCode, "BenefitEligibleFlagXCD", moBenefitsEligibleFlagDropDownList, isGuidValue:=False, isStringValue:=True)
                PopulateBOProperty(TheProductCode, "BenefitEligibleActionXCD", moBenefitsEligibleActionDropDownList, isGuidValue:=False, isStringValue:=True)
                PopulateBOProperty(TheProductCode, "CalcCovgEndDateBasedOnXCD", ddlCalcCovgEndDateBasedOn, isGuidValue:=False, isStringValue:=True)
                PopulateBOProperty(TheProductCode, "UseDepreciation", moUseDepreciationDrop)
                PopulateBOProperty(TheProductCode, "BundledItemId", moBundledItemId)
                PopulateBOProperty(TheProductCode, "SplitWarrantyId", moSplitWarrantyDrop)


                PopulateBOProperty(TheProductCode, "IsReInsuredId", moReInsuredDrop)


                If moPercentOfRetailText.Text = String.Empty Then
                    moPercentOfRetailText.Text = GetAmountFormattedDoubleString("0")
                End If
                If Not (.PercentOfRetail Is Nothing AndAlso (CType(moPercentOfRetailText.Text, Decimal) = 0)) Then
                    PopulateBOProperty(TheProductCode, "PercentOfRetail", moPercentOfRetailText)
                End If
                If State.DealerTypeID.Equals(State.dealerTypeVSC) Then
                    PopulateBOProperty(TheProductCode, "ProductCode", ProductCodeMultipleDrop.SelectedCode)
                    PopulateBOProperty(TheProductCode, "Description", ProductCodeMultipleDrop.SelectedDesc)
                Else
                    PopulateBOProperty(TheProductCode, "ProductCode", moProductCodeText)
                    PopulateBOProperty(TheProductCode, "Description", moDescriptionText)
                End If
                PopulateBOProperty(TheProductCode, "MethodOfRepairByPriceId", moMethodOfRepairByPriceDrop)
                PopulateBOProperty(TheProductCode, "Comments", moCommentsText)
                PopulateBOProperty(TheProductCode, "SpecialService", moSpecialServiceText)

                If State.isInstallmentOn Then
                    PopulateBOProperty(TheProductCode, "BillingFrequencyId", moBillingFrequencyId)
                    PopulateBOProperty(TheProductCode, "NumberOfInstallments", moInstallmentText)
                End If
                PopulateBOProperty(TheProductCode, "NumOfClaims", moNumOfReplacementsText)
                PopulateBOProperty(TheProductCode, "NumOfRepairClaims", moNumOfRepairClaimsText)
                PopulateBOProperty(TheProductCode, "NumOfReplacementClaims", moNumOfReplClaimsText)

                PopulateBOProperty(TheProductCode, "ClaimWaitingPeriod", moWaitingPeriod)
                PopulateBOProperty(TheProductCode, "FullRefundDays", moDaysToCancel)
                PopulateBOProperty(TheProductCode, "IgnoreWaitingPeriodWsdPsd", cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD)
                'REQ-5586 Start
                PopulateBOProperty(TheProductCode, "ProdLiabilityLimitBasedOnId", moProdLiabilityLimitBasedOnDrop)
                PopulateBOProperty(TheProductCode, "ProdLiabilityLimitPolicyId", moProdLiabilityLimitPolicyDrop)
                PopulateBOProperty(TheProductCode, "ProdLiabilityLimit", moProdLiabilityLimitText)
                PopulateBOProperty(TheProductCode, "ProdLiabilityLimitPercent", moProdLiabilityLimitPercentText)
                'REQ-5586 End

                'REQ-6289
                PopulateBOProperty(TheProductCode, "ProductLimitApplicableToXCD", moProdLimitApplicableToXCDDrop, isGuidValue:=False, isStringValue:=True)
                'REQ-6289-END

                PopulateBOProperty(TheProductCode, "UpgradeFixedTerm", moUpgradeTermText)
                PopulateBOProperty(TheProductCode, "UpgradeTermFrom", moUpgradeTermFROMText)
                PopulateBOProperty(TheProductCode, "UpgradeTermTo", moUpgradeTermToText)
                PopulateBOProperty(TheProductCode, "UPGFinanceBalCompMethId", moUPGFinanceBalCompMethDrop)
                PopulateBOProperty(TheProductCode, "UpgradeProgramId", moUpgradeProgramDrop)
                PopulateBOProperty(TheProductCode, "UpgFinanceInfoRequireId", moupgFinanceInfoRequireDrop)
                PopulateBOProperty(TheProductCode, "UpgradeTermUomId", moUpgTermUOMDrop)
                PopulateBOProperty(TheProductCode, "InstallmentRepricableId", moInstallmentRepricableDrop)
                'REQ-5733
                PopulateBOProperty(TheProductCode, "AnalysisCode1", moAnalysisCode1Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode2", moAnalysisCode2Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode3", moAnalysisCode3Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode4", moAnalysisCode4Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode5", moAnalysisCode5Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode6", moAnalysisCode6Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode7", moAnalysisCode7Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode8", moAnalysisCode8Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode9", moAnalysisCode9Text)
                PopulateBOProperty(TheProductCode, "AnalysisCode10", moAnalysisCode10Text)

                PopulateBOProperty(TheProductCode, "BillingCriteriaId", moBillingCriteriaDrop)
                PopulateBOProperty(TheProductCode, "CnlDependencyId", moCancellationDependencyDrop)
                PopulateBOProperty(TheProductCode, "PostPrePaidId", moPostPrePaidDrop)
                PopulateBOProperty(TheProductCode, "CnlLumpsumBillingId", moCnlLumpsumBillingDrop)
                PopulateBOProperty(TheProductCode, "ProductEquipmentValidation", cboProductEquipmentValidation, False, True)
                PopulateBOProperty(TheProductCode, "UpgradeFee", moUpgFeeText)
                PopulateBOProperty(TheProductCode, "AllowRegisteredItems", cboAllowRegisteredItems, False, True)
                PopulateBOProperty(TheProductCode, "MaxAgeOfRegisteredItem", moMaxAgeOfRegisteredItemText)
                PopulateBOProperty(TheProductCode, "MaxRegistrationsAllowed", moMaxRegistrationsAllowedText)
                PopulateBOProperty(TheProductCode, "ListForDeviceGroups", cboListForDeviceGroup)
                'REQ

                PopulateBOProperty(TheProductCode, "MaxClaimsAllowedPerRegisteredItem", moClaimLimitPerRegisteredItemText)
                'REQ-6002
                PopulateBOProperty(TheProductCode, "UpdateReplaceRegItemsId", cboUpdateReplaceRegItemsId)
                PopulateBOProperty(TheProductCode, "RegisteredItemsLimit", txtRegisteredItemsLimit)
                PopulateBOProperty(TheProductCode, "CancellationWithinTerm", cboCancellationWithinTerm, isGuidValue:=False, isStringValue:=True)
                PopulateBOProperty(TheProductCode, "ExpirationNotificationDays", moExpNotificationDaysText)

                PopulateBOProperty(TheProductCode, FulfillmentReimThresholdProperty, TextBoxFulfillmentReimThresholdValue)
                PopulateBOProperty(TheProductCode, "ClaimProfile", ddlClaimProfile, False, True)
                PopulateBOProperty(TheProductCode, "PerIncidentLiabilityLimitCap", moPerIncidentLiabilityLimitCapText)
                PopulateBOProperty(TheProductCode, "ExpectedPremiumIsWpXcd", cboExpectedPremiumIsWpXcd, False, True)
                PopulateBOProperty(TheProductCode, "PriceMatrixUsesWpXcd", cboPriceMatrixUsesWpXcd, False, True)
            End With
            If Not TheDepreciationSchedule.IsDeleted Then
                PopulateBOProperty(TheDepreciationSchedule, "DepreciationScheduleId", ddlDepSchCashReimbursement)
                TheProductCode.AddProductDepreciationScdChild(TheDepreciationSchedule.DepreciationScheduleId)
            End If

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Sub PopulateUserControlAvailableSelectedDealer()
            State.oDealer = New Dealer(TheDealerControl.SelectedGuid) 'As Guid = Me.GroupCompanyMultipleDrop.SelectedGuid
            State.dealerInstallmentDefCode = LookupListNew.GetCodeFromId(LookupListNew.LK_INSTALLMENT_DEFINITION, State.oDealer.UseInstallmentDefnId)
            If State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE) _
                OrElse State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE_OR_CONTRACT) Then
                ControlMgr.SetVisibleControl(Me, pnlInstallment, True)
                State.isInstallmentOn = True
                Dim billFreq As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_FREQUENCY, Codes.MONTHLY)
                SetSelectedItem(moBillingFrequencyId, billFreq)

                Dim oCode As String = ""
                If State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE) Then
                    oCode = LookupListNew.GetCodeFromId(LookupListNew.LK_BILLING_FREQUENCY, billFreq)

                ElseIf State.dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE_OR_CONTRACT) Then
                    oCode = "0"
                End If

                PopulateControlFromBOProperty(moInstallmentText, oCode)
            Else
                ControlMgr.SetVisibleControl(Me, pnlInstallment, False)
                State.isInstallmentOn = False

            End If
        End Sub

        Sub PopulateUserControlAvailableSelectedRegions()
            UserControlAvailableSelectedRegions.BackColor = "#d5d6e4"
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedRegions, False)
            Dim oDealer As Dealer
            Dim CountryId As Guid

            With TheProductCode
                If Not .Id.Equals(Guid.Empty) Then

                    CountryId = oDealer.GetDealerCountryId(TheDealerControl.SelectedGuid)
                    Dim availableDv As DataView = .GetAvailableRegions(CountryId)
                    Dim selectedDv As DataView = .GetSelectedRegions(CountryId)
                    UserControlAvailableSelectedRegions.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedRegions.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedRegions, True)

                End If
            End With

        End Sub

#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheDealerControl.ChangeEnabledControlProperty(bIsNew)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, bIsNew)
            ControlMgr.SetEnableControl(Me, moProductCodeText, bIsNew)

            If Not bIsNew Then
                ShowHideMethodOfRepairByPriceButton()
            End If
        End Sub

#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheProductCode
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                    If bIsDirty = False Then bIsDirty = IsDirtyProductEquipmentBO()
                    If bIsDirty = False Then bIsDirty = State.moProductCode.IsFamilyDirty
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(PRODUCTCODE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try

                If Not IsNumeric(moPercentOfRetailText.Text) Then
                    ElitaPlusPage.SetLabelError(moPercentOfRetailLabel)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PERCENT_OF_RETAIL_ENTERED_ERR)
                Else
                    If CType(moPercentOfRetailText.Text, Decimal) < 0 OrElse CType(moPercentOfRetailText.Text, Decimal) > 100 Then
                        ElitaPlusPage.SetLabelError(moPercentOfRetailLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PERCENT_OF_RETAIL_ENTERED_ERR)
                    Else
                        If Len((CType(moPercentOfRetailText.Text, Decimal) - System.Math.Floor(CType(moPercentOfRetailText.Text, Decimal))).ToString) > 4 Then
                            ElitaPlusPage.SetLabelError(moPercentOfRetailLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.TWO_DIGITS_BEHIND_DECIMAL_ERR)
                        End If
                    End If
                End If
                'REQ-5586 Start
                If (Not GetSelectedItem(moProdLiabilityLimitBasedOnDrop).Equals(Guid.Empty) AndAlso
                    Not GetSelectedItem(moProdLiabilityLimitBasedOnDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) Then

                    If String.IsNullOrEmpty(moProdLiabilityLimitText.Text) AndAlso String.IsNullOrEmpty(moProdLiabilityLimitPercentText.Text) Then
                        ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                        ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                    ElseIf Not String.IsNullOrEmpty(moProdLiabilityLimitText.Text) AndAlso String.IsNullOrEmpty(moProdLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moProdLiabilityLimitText.Text) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT)
                        ElseIf CType(moProdLiabilityLimitText.Text, Decimal) < 0 Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT)
                        End If
                    ElseIf String.IsNullOrEmpty(moProdLiabilityLimitText.Text) AndAlso Not String.IsNullOrEmpty(moProdLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moProdLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_PERCENT)
                        ElseIf CType(moProdLiabilityLimitPercentText.Text, Decimal) <= 0 OrElse CType(moProdLiabilityLimitPercentText.Text, Decimal) > 100 Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_PERCENT)
                        End If
                    ElseIf Not String.IsNullOrEmpty(moProdLiabilityLimitText.Text) AndAlso Not String.IsNullOrEmpty(moProdLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moProdLiabilityLimitText.Text) AndAlso Not IsNumeric(moProdLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf IsNumeric(moProdLiabilityLimitText.Text) AndAlso Not IsNumeric(moProdLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf Not IsNumeric(moProdLiabilityLimitText.Text) AndAlso IsNumeric(moProdLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf (CType(moProdLiabilityLimitPercentText.Text, Decimal) <= 0 OrElse CType(moProdLiabilityLimitPercentText.Text, Decimal) > 100) Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf CType(moProdLiabilityLimitText.Text, Decimal) > 0 AndAlso CType(moProdLiabilityLimitPercentText.Text, Decimal) > 0 Then
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(moProdLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PROD_LIABILITY_LIMIT_AND_PERCENT)
                        End If
                    End If
                End If
                'REQ-5586 End

                'Per Incident Liability Limit
                If Not (GetSelectedItem(moProdLiabilityLimitBasedOnDrop).Equals(Guid.Empty) OrElse GetSelectedItem(moProdLiabilityLimitBasedOnDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) Then

                    If String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                        ElitaPlusPage.SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    ElseIf Not String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                        If Not IsNumeric(moPerIncidentLiabilityLimitCapText.Text) Then
                            ElitaPlusPage.SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                            Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                        ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) < 0 Then
                            ElitaPlusPage.SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                            Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                        End If
                    ElseIf (CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) <= 0) Then
                        ElitaPlusPage.SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) > 0 Then
                        ElitaPlusPage.SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    End If
                End If
                Dim sMORPriceFlgBefore As String, sMORPriceFlgAfter As String, oPriceRecordsExist As Boolean
                If moMethodOfRepairByPriceDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    sMORPriceFlgBefore = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, TheProductCode.MethodOfRepairByPriceId)
                    sMORPriceFlgAfter = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(moMethodOfRepairByPriceDrop))
                    If sMORPriceFlgBefore = YES AndAlso sMORPriceFlgAfter = NO Then
                        Dim PrdCode As ProductCode
                        oPriceRecordsExist = PrdCode.MethodOfRepairByPriceRecords(TheProductCode.Id)
                        If oPriceRecordsExist = True Then
                            ElitaPlusPage.SetLabelError(moMethodOfRepairByPriceLabel)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.METHOD_OF_REPAIR_BY_PRICE_ERR)
                        End If
                    End If
                End If




                If (Not (State Is Nothing) AndAlso Not (TheDealerControl.SelectedGuid = Guid.Empty)) Then
                    'REQ-5579
                    If State.oDealer.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                        If Not txtAutoApprovePSP.Text.Trim.Equals(String.Empty) Then

                            If Not IsNumeric(txtAutoApprovePSP.Text) Then
                                ElitaPlusPage.SetLabelError(lblAutoApprovePSP)
                                Throw New GUIException(Message.MSG_INVALID_AUTO_APPROVE_PSP, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_AUTO_APPROVE_PSP_ERR)
                            Else
                                If CType(txtAutoApprovePSP.Text, Decimal) <= 0 OrElse CType(txtAutoApprovePSP.Text, Decimal) > 100 Then
                                    ElitaPlusPage.SetLabelError(lblAutoApprovePSP)
                                    Throw New GUIException(Message.MSG_INVALID_AUTO_APPROVE_PSP, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_AUTO_APPROVE_PSP_ERR)
                                Else
                                    If Len((CType(txtAutoApprovePSP.Text, Decimal) - System.Math.Floor(CType(txtAutoApprovePSP.Text, Decimal))).ToString) > 4 Then
                                        ElitaPlusPage.SetLabelError(lblAutoApprovePSP)
                                        Throw New GUIException(Message.MSG_INVALID_AUTO_APPROVE_PSP, Assurant.ElitaPlus.Common.ErrorCodes.TWO_DIGITS_BEHIND_DECIMAL_ERR)
                                    End If
                                End If
                            End If

                            PopulateBOProperty(TheProductCode, "ClaimAutoApprovePsp", txtAutoApprovePSP)
                        Else
                            TheProductCode.ClaimAutoApprovePsp = Nothing
                        End If
                    End If
                End If
                'REQ-5579 end

                If State.IsProductCodeNew = False AndAlso Not TheProductCode.IsReInsuredId.Equals(Guid.Empty) Then
                    If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) Then
                        State.ModeOperation = "D"
                    ElseIf GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                        State.ModeOperation = "I"
                    End If

                ElseIf State.IsProductCodeNew = False AndAlso GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                    State.ModeOperation = "I"
                End If

                Dim UpdCovLiablity As Boolean = False

                If TheProductCode.ProdLiabilityLimitBasedOnId <> GetSelectedItem(moProdLiabilityLimitBasedOnDrop) Then
                    UpdCovLiablity = True
                End If

                PopulateBOsFromForm()

                If (Not GetSelectedItem(moUpgTermUOMDrop).Equals(Guid.Empty) AndAlso (
                    GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_DAYS) OrElse GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_MONTHS))) Then

                    If (String.IsNullOrEmpty(moUpgradeTermFROMText.Text) AndAlso Not String.IsNullOrEmpty(moUpgradeTermToText.Text)) OrElse (Not String.IsNullOrEmpty(moUpgradeTermFROMText.Text) AndAlso String.IsNullOrEmpty(moUpgradeTermToText.Text)) Then


                        ElitaPlusPage.SetLabelError(moUpgradeTermFromLabel)
                        ElitaPlusPage.SetLabelError(moUpgradeTermToLabel)
                        Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.SET_UPG_TERM_FROM_AND_UPG_TERM_TO_FIELDS)

                    ElseIf Not String.IsNullOrEmpty(moUpgradeTermFROMText.Text) AndAlso Not String.IsNullOrEmpty(moUpgradeTermToText.Text) AndAlso
                           CType(moUpgradeTermToText.Text, Decimal) < CType(moUpgradeTermFROMText.Text, Decimal) Then

                        ElitaPlusPage.SetLabelError(moUpgradeTermFromLabel)
                        ElitaPlusPage.SetLabelError(moUpgradeTermToLabel)
                        Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.UPG_TERM_FROM_LESS_THAN_UPG_TERM_TO)
                    End If

                End If

                If Not GetSelectedItem(moReInsuredDrop).Equals(Guid.Empty) Then

                    If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then

                        If TheProductCode.AttributeValues.Count = 0 Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_REQUIRED_WHEN_REINSURED_IS_SET_ERR)
                        End If

                        If TheProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_SHOULD_BE_SAVED_ERR)
                        End If

                    End If
                    'REQ-5888-START
                    If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) Then
                        'AttributeValues.Visible = False
                        If TheProductCode.AttributeValues.Count > 0 AndAlso Not (TheProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing) Then
                            Throw New GUIException(Message.INVALID_ATTRIBUTE, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_CANNOT_BE_SET_WHEN_REINSURED_IS_SET_TO_NO_ERR)
                        End If
                    End If
                    'REQ-5888-END
                Else
                    'REQ-5888-START
                    If TheProductCode.AttributeValues.Count > 0 AndAlso Not (TheProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing) Then
                        Throw New GUIException(Message.INVALID_ATTRIBUTE, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_SET_ATTRIBUTE_WITHOUT_REINSURED_FLAG)
                    End If
                    'REQ-5888-END
                End If

                If TheProductCode.IsDirty() OrElse TheProductCode.IsFamilyDirty Then 'Or Me.State.MyProductPolicyBO.IsDirty Then
                    If TheProductCode.PercentOfRetail Is Nothing Then
                        PopulateBOProperty(TheProductCode, "PercentOfRetail", "0.0")
                    End If

                    TheProductCode.Save()

                    _moDepreciationScdRelation = Nothing


                    If State.IsProductCodeNew = False AndAlso (GetSelectedItem(moProdLiabilityLimitBasedOnDrop).Equals(Guid.Empty) OrElse GetSelectedItem(moProdLiabilityLimitBasedOnDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) AndAlso UpdCovLiablity Then
                        TheProductCode.UpdateCoverageLiability(State.moProductCodeId)
                    End If
                    If State.IsProductCodeNew = False AndAlso Not TheProductCode.IsReInsuredId.Equals(Guid.Empty) Then
                        TheProductCode.UpdateCoverageReinsurance(TheProductCode.Id, State.ModeOperation)
                    End If

                    State.boChanged = True
                    If State.IsProductCodeNew = True Then
                        State.IsProductCodeNew = False
                    End If
                    PopulateAll()
                    EnableDisableFields()
                    SetButtonsState(State.IsProductCodeNew)
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    ' Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Function

        Private Function DeleteProductCode() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheProductCode
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With

            Catch ex As Exception
                If ex.Message = INTEGRITY_CONSTRAINT_VIOLATION_MSG Then
                    MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.DB_INTEGRITY_CONSTRAINT_VIOLATED, True)
                    TheProductCode.RejectChanges()
                Else
                    MasterPage.MessageController.AddError(PRODUCTCODE_FORM002)
                    MasterPage.MessageController.AddError(ex.Message, False)
                End If
                bIsOk = False
                MasterPage.MessageController.Show()
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            If (State.DealerTypeID.Equals(State.dealerTypeVSC)) Then

                BindBOPropertyToLabel(TheProductCode, PRODUCT_CODE_PROPERTY, ProductCodeMultipleDrop.CaptionLabel)
            Else
                BindBOPropertyToLabel(TheProductCode, PRODUCT_CODE_PROPERTY, moProductCodeLabel)
            End If

            BindBOPropertyToLabel(TheProductCode, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(TheProductCode, RISK_GROUP_ID_PROPERTY, moRiskGroupLabel)
            BindBOPropertyToLabel(TheProductCode, METHOD_OF_REPAIR_PROPERTY, moMethodOfRepairLabel)
            BindBOPropertyToLabel(TheProductCode, TYPE_OF_EQUIPMENT_ID_PROPERTY, moTypeOfEquipmentLabel)
            BindBOPropertyToLabel(TheProductCode, PRICE_MATRIX_ID_PROPERTY, moPriceMatrixLabel)
            BindBOPropertyToLabel(TheProductCode, USE_DEPRECIATION_PROPERTY, moUserDescriptionLabel)
            BindBOPropertyToLabel(TheProductCode, PERCENT_OF_RETAIL_PROPERTY, moPercentOfRetailLabel)
            BindBOPropertyToLabel(TheProductCode, METHOD_OF_REPAIR_BY_PRICE_ID_PROPERTY, moMethodOfRepairByPriceLabel)
            BindBOPropertyToLabel(TheProductCode, SPLIT_WARRANTY_ID_PROPERTY, moSplitWarrantyLabel)
            BindBOPropertyToLabel(TheProductCode, COMMENTS_PROPERTY, moCommentsLabel)
            BindBOPropertyToLabel(TheProductCode, SPECIAL_SERVICE_PROPERTY, moSpecialServiceLabel)
            BindBOPropertyToLabel(TheProductCode, BILLING_FREQUENCY_ID_PROPERTY, moBillingFrequencyLabel)
            BindBOPropertyToLabel(TheProductCode, NUMBER_OF_INSTALLMENTS_PROPERTY, moInstallmentsLabel)
            BindBOPropertyToLabel(TheProductCode, NUMBER_OF_CLAIMS_PROPERTY, moNumOfReplacementsLabel)
            BindBOPropertyToLabel(TheProductCode, NUMBER_OF_REPAIR_CLAIMS_PROPERTY, moNumOfRepairClaimsLabel)
            BindBOPropertyToLabel(TheProductCode, NUMBER_OF_REPLACEMENTS_CLAIMS_PROPERTY, moNumOfReplClaimsLabel)
            BindBOPropertyToLabel(TheProductCode, BUNDLED_ITEM_ID_PROPERTY, moBundledItemLabel)
            BindBOPropertyToLabel(TheProductCode, DESCRIPTION_PROPERTY, moDescriptionLabel)
            BindBOPropertyToLabel(TheProductCode, CLAIM_WAITING_PERIOD_PROPERTY, LabelWaitingPeriod)
            BindBOPropertyToLabel(TheProductCode, DAYS_TO_CANCEL_PROPERTY, LabelDaysToCancel)
            BindBOPropertyToLabel(TheProductCode, IGNORE_WAITING_PERIOD_WSDPSD, LabelApplyWaitingPeriod)
            'REQ-5586 Start
            BindBOPropertyToLabel(TheProductCode, PRODUCT_LIABILITY_LIMIT_BASED_ON_PROPERTY, moProdLiabilityLimitBasedOnLabel)
            BindBOPropertyToLabel(TheProductCode, PRODUCT_LIABILITY_LIMIT_POLICY_PROPERTY, moProdLiabilityLimitPolicyLabel)
            BindBOPropertyToLabel(TheProductCode, PRODUCT_LIABILITY_LIMIT_PROPERTY, moProdLiabilityLimitLabel)
            BindBOPropertyToLabel(TheProductCode, PRODUCT_LIABILITY_LIMIT_PERCENT_PROPERTY, moProdLiabilityLimitPercentLabel)
            'REQ-5586 End
            'REQ-6289
            BindBOPropertyToLabel(TheProductCode, UPGRADE_PROGRAM_PROPERTY, moProdLimitApplicableToXCDLabel)
            'REQ-5586 End
            BindBOPropertyToLabel(TheProductCode, CLAIM_AUTO_APPROVE_PSP_PROPERTY, lblAutoApprovePSP)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_FIXED_TERM_PROPERTY, moUpgradeTermLabel)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_TERM_FROM_PROPERTY, moUpgradeTermFromLabel)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_TERM_TO_PROPERTY, moUpgradeTermToLabel)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_TERM_UOM_PROPERTY, moUpgTermUOMLabel)
            BindBOPropertyToLabel(TheProductCode, UPG_FINANCE_INFO_REQUIRE_PROPERTY, moupgFinanceInfoRequireLabel)
            BindBOPropertyToLabel(TheProductCode, UPG_FINANCE_BAL_COMP_METH_PROPERTY, moUPGFinanceBalCompMethLabel)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_PROGRAM_PROPERTY, moUpgradeProgramLabel)

            'pavan REQ-5733
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_1_PROPERTY, moAnalysisCode1Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_2_PROPERTY, moAnalysisCode2Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_3_PROPERTY, moAnalysisCode3Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_4_PROPERTY, moAnalysisCode4Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_5_PROPERTY, moAnalysisCode5Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_6_PROPERTY, moAnalysisCode6Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_7_PROPERTY, moAnalysisCode7Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_8_PROPERTY, moAnalysisCode8Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_9_PROPERTY, moAnalysisCode9Label)
            BindBOPropertyToLabel(TheProductCode, ANALYSIS_CODE_10_PROPERTY, moAnalysisCode10Label)
            BindBOPropertyToLabel(TheProductCode, Is_REINSURED_PROPERTY, moReInsuredLabel)
            BindBOPropertyToLabel(TheProductCode, INSTALLMENT_REPRICABLE_PROPERTY, moInstallmentRepricableLabel)
            BindBOPropertyToLabel(TheProductCode, BILLING_CRITERIA_PROPERTY, moBillingCriteriaLabel)
            BindBOPropertyToLabel(TheProductCode, CANCELLATION_DEPENDENCY_PROPERTY, moCancellationDependencyLabel)
            BindBOPropertyToLabel(TheProductCode, POST_PRE_PAID_PROPERTY, moPostPrePaidLabel)
            BindBOPropertyToLabel(TheProductCode, CNL_LUMPSUM_BILLING_PROPERTY, moCnlLumpsumBillingLabel)
            BindBOPropertyToLabel(TheProductCode, UPGRADE_FEE_PROPERTY, moUpgFeeLabel)
            BindBOPropertyToLabel(TheProductCode, MAX_AGE_OF_REGISTERED_ITEM_PROPERTY, moMaxAgeOfRegisteredItemLabel)
            'LL: Replaced with correct one.
            BindBOPropertyToLabel(TheProductCode, MAX_REGISTRATIONS_ALLOWED_PROPERTY, moMaxRegistrationsAllowedLabel)
            BindBOPropertyToLabel(TheProductCode, ALLOW_REGISTERED_ITEM_PROPERTY, lblAllowRegisteredItems)
            BindBOPropertyToLabel(TheProductCode, LIST_FOR_DEVICE_GROUP_PROPERTY, lblListForDeviceGroup)
            'REQ

            'REQ-6002
            BindBOPropertyToLabel(TheProductCode, UPDATE_REPLACE_REG_ITEMS_ID_PROPERTY, lblUpdateReplaceRegItemsId)
            BindBOPropertyToLabel(TheProductCode, REGISTERED_ITEMS_LIMIT_PROPERTY, lblRegisteredItemsLimit)
            'LL: Adding missing bind from previous requirement.
            BindBOPropertyToLabel(TheProductCode, PRODUCT_EQUIPMENT_VALIDATION_PROPERTY, lblProductEquipmentValidation)
            BindBOPropertyToLabel(TheProductCode, CLAIM_LIMIT_PER_REG_ITEM, moClaimLimitPerRegisteredItemlabel)
            BindBOPropertyToLabel(TheProductCode, CANCELLATION_WITHIN_TERM_PROPERTY, moCancellationWithinTermLabel)
            BindBOPropertyToLabel(TheProductCode, EXPIRATION_NOTIFICATION_DAYS_PROPERTY, moExpNotificationDaysLabel)
            BindBOPropertyToLabel(TheProductCode, FulfillmentReimThresholdProperty, labelFulfillmentReimThresholdValue)


        End Sub

        Private Sub ClearLabelsErrSign()
            If (State.DealerTypeID.Equals(State.dealerTypeVSC)) Then
                ClearLabelErrSign(ProductCodeMultipleDrop.CaptionLabel)
            Else
                ClearLabelErrSign(moProductCodeLabel)
            End If
            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(moProductCodeLabel)
            ClearLabelErrSign(moRiskGroupLabel)
            ClearLabelErrSign(moMethodOfRepairLabel)
            ClearLabelErrSign(moTypeOfEquipmentLabel)
            ClearLabelErrSign(moPriceMatrixLabel)
            ClearLabelErrSign(moUserDescriptionLabel)
            ClearLabelErrSign(moPercentOfRetailLabel)
            ClearLabelErrSign(moMethodOfRepairByPriceLabel)
            ClearLabelErrSign(moCommentsLabel)
            ClearLabelErrSign(moSpecialServiceLabel)
            ClearLabelErrSign(moBillingFrequencyLabel)
            ClearLabelErrSign(moInstallmentsLabel)
            ClearLabelErrSign(moNumOfReplacementsLabel)
            ClearLabelErrSign(moSplitWarrantyLabel)
            ClearLabelErrSign(LabelWaitingPeriod)
            ClearLabelErrSign(LabelDaysToCancel)
            'REQ-5586 Start
            ClearLabelErrSign(moProdLiabilityLimitBasedOnLabel)
            ClearLabelErrSign(moProdLiabilityLimitPolicyLabel)
            ClearLabelErrSign(moProdLiabilityLimitLabel)
            ClearLabelErrSign(moProdLiabilityLimitPercentLabel)
            'REQ-5586 End
            'REQ-6289
            ClearLabelErrSign(moProdLimitApplicableToXCDLabel)
            'REG-6289-END
            ClearLabelErrSign(moUpgradeTermLabel)
            ClearLabelErrSign(moUpgradeTermFromLabel)
            ClearLabelErrSign(moUpgradeTermToLabel)
            ClearLabelErrSign(moUpgTermUOMLabel)
            ClearLabelErrSign(moupgFinanceInfoRequireLabel)
            ClearLabelErrSign(moUPGFinanceBalCompMethLabel)
            ClearLabelErrSign(moUpgradeProgramLabel)
            ClearLabelErrSign(moInstallmentRepricableLabel)
            ClearLabelErrSign(moUpgFeeLabel)
            ClearLabelError(moMaxAgeOfRegisteredItemLabel)
            ClearLabelError(moMaxRegistrationsAllowedLabel)
            'REQ

            ClearLabelError(moClaimLimitPerRegisteredItemlabel)
            ClearLabelErrSign(moCancellationWithinTermLabel)
            ClearLabelErrSign(moExpNotificationDaysLabel)
            ClearLabelErrSign(labelFulfillmentReimThresholdValue)

        End Sub
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub


        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                If (String.IsNullOrEmpty(MasterPage.MessageController.Text)) Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = String.Empty
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            If (State.DealerTypeID.Equals(State.dealerTypeVSC)) Then
                ControlMgr.SetVisibleControl(Me, TRPrdCode, False)
                ControlMgr.SetVisibleControl(Me, TRPlanCode, True)
                ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, True)
                ControlMgr.SetVisibleControl(Me, pnlUPGFields, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                ControlMgr.SetVisibleControl(Me, moReInsuredLabel, False)
                ControlMgr.SetVisibleControl(Me, moReInsuredDrop, False)
                ControlMgr.SetVisibleControl(Me, moInstallmentRepricableLabel, False)
                ControlMgr.SetVisibleControl(Me, moInstallmentRepricableDrop, False)

            Else
                ControlMgr.SetVisibleControl(Me, TRPrdCode, True)
                ControlMgr.SetVisibleControl(Me, TRPlanCode, False)
                ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, True)
                ControlMgr.SetVisibleControl(Me, moReInsuredLabel, True)
                ControlMgr.SetVisibleControl(Me, moReInsuredDrop, True)
            End If

            ShowHideMethodOfRepairByPriceFields()
            ShowHideMethodOfRepairByPriceButton()
            ShowHideProductPolicyGrid()
            ShowHideProductRewardsGrid()
            If (State.oDealer IsNot Nothing) Then
                If State.oDealer.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    ControlMgr.SetVisibleControl(Me, lblAutoApprovePSP_TD, True)
                    ControlMgr.SetVisibleControl(Me, txtAutoApprovePSP_TD, True)
                Else
                    ControlMgr.SetVisibleControl(Me, lblAutoApprovePSP_TD, False)
                    ControlMgr.SetVisibleControl(Me, txtAutoApprovePSP_TD, False)
                End If
            End If
            If TheProductCode.AllowRegisteredItems = "YESNO-Y" Then
                ControlMgr.SetVisibleControl(Me, lblListForDeviceGroup, True)
                ControlMgr.SetVisibleControl(Me, cboListForDeviceGroup, True)
                ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemLabel, True)
                ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemText, True)
                ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedLabel, True)
                ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedText, True)
                ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemlabel, True)
                ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemText, True)
                'REQ-6002
                ControlMgr.SetVisibleControl(Me, trUpdateReplaceRegItemsId, True)
                ControlMgr.SetVisibleControl(Me, cboUpdateReplaceRegItemsId, True)
                ControlMgr.SetVisibleControl(Me, txtRegisteredItemsLimit, True)
            Else
                ControlMgr.SetVisibleControl(Me, lblListForDeviceGroup, False)
                ControlMgr.SetVisibleControl(Me, cboListForDeviceGroup, False)
                ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemLabel, False)
                ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemText, False)
                ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedLabel, False)
                ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedText, False)
                ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemlabel, False)
                ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemText, False)
                'REQ-6002
                ControlMgr.SetVisibleControl(Me, trUpdateReplaceRegItemsId, False)
                ControlMgr.SetVisibleControl(Me, cboUpdateReplaceRegItemsId, False)
                ControlMgr.SetVisibleControl(Me, txtRegisteredItemsLimit, False)
            End If

            'REQ-5888-START
            'If GetSelectedItem(Me.moReInsuredDrop).Equals(Guid.Empty) Then
            '    AttributeValues.Visible = False
            'End If
            'REQ-5888-END
        End Sub

        Protected Sub ShowHideMethodOfRepairByPriceFields()
            Dim oMethodOfRepair As String
            oMethodOfRepair = LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, GetSelectedItem(moMethodOfRepairDrop))
            Select Case oMethodOfRepair
                Case Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__SEND_IN, Codes.METHOD_OF_REPAIR__PICK_UP
                    ControlMgr.SetVisibleControl(Me, pnlMethodOfRepair_Repair, True)
                Case Else
                    Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                    SetSelectedItem(moMethodOfRepairByPriceDrop, noId)
                    ControlMgr.SetVisibleControl(Me, pnlMethodOfRepair_Repair, False)
            End Select

            If oMethodOfRepair = Codes.METHOD_OF_REPAIR__REPLACEMENT _
                OrElse oMethodOfRepair = Codes.METHOD_OF_REPAIR__AT_HOME _
                OrElse oMethodOfRepair = Codes.METHOD_OF_REPAIR__CARRY_IN _
                OrElse oMethodOfRepair = Codes.METHOD_OF_REPAIR__PICK_UP _
                OrElse oMethodOfRepair = Codes.METHOD_OF_REPAIR__SEND_IN Then
                ControlMgr.SetVisibleControl(Me, pnlMethdOfRepair_Replacement, True) 'allow for only replacement and repair
            Else
                moNumOfReplacementsText.Text = String.Empty
                moNumOfRepairClaimsText.Text = String.Empty
                moNumOfReplClaimsText.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, pnlMethdOfRepair_Replacement, False)
            End If
        End Sub

        Protected Sub ShowHideProductPolicyGrid()

            If moProductPolicyDatagrid.Visible = True Then

                ' IsNewProductPolicy = False
                State.ProdPolicyAddNew = False
                PopulateMyProductPolicyGrid()
                '  SetProductPolicyButtonsState(False)
                If Not State.ProductPolicyGridTranslated Then
                    TranslateGridHeader(moProductPolicyDatagrid)
                    'Me.TranslateGridControls(Me.moMerchantCodesDatagrid)
                    State.ProductPolicyGridTranslated = True
                End If

            End If
        End Sub

        Protected Sub ShowHideProductRewardsGrid()

            If ProductRewardsGridView.Visible = True Then

                ' IsNewProductPolicy = False
                State.ProdRewardsAddNew = False
                PopulateMyProductRewardsGrid()
                '  SetProductPolicyButtonsState(False)
                If Not State.ProductRewardsGridTranslated Then
                    TranslateGridHeader(ProductRewardsGridView)
                    'Me.TranslateGridControls(Me.moMerchantCodesDatagrid)
                    State.ProductRewardsGridTranslated = True
                End If

            End If
        End Sub

        Protected Sub ShowHideMethodOfRepairByPriceButton()
            Dim oMethodOfRepairByPrice As String
            oMethodOfRepairByPrice = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(moMethodOfRepairByPriceDrop))
            If oMethodOfRepairByPrice = Codes.YESNO_Y Then
                ControlMgr.SetVisibleControl(Me, btnProdRepairPrice_WRITE, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnProdRepairPrice_WRITE, False)
            End If

        End Sub

        Protected Sub moMethodOfRepairDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moMethodOfRepairDrop.SelectedIndexChanged
            ShowHideMethodOfRepairByPriceFields()
        End Sub
#End Region

#Region "Regions: Attach - Detach Event Handlers"

        Private Sub UserControlAvailableSelectedRegions_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedRegions.Attach
            Try
                If attachedList.Count > 0 Then
                    TheProductCode.AttachRegions(attachedList)
                    'Me.PopulateDetailMfgGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedRegions_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedRegions.Detach
            Try
                If detachedList.Count > 0 Then
                    TheProductCode.DetachRegions(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "ProductPolicy_Handlers_Grid"

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then

                    nIndex = CInt(e.CommandArgument)
                    moProductPolicyDatagrid.EditIndex = nIndex
                    moProductPolicyDatagrid.SelectedIndex = nIndex


                    State.SelectedEquipmentType = CType(moProductPolicyDatagrid.Rows(nIndex).Cells(TYPE_OF_EQUIPMENT).FindControl(TYPE_OF_EQUIPMENT_LABEL_CONTROL_NAME), Label).Text
                    State.SelectedExternalProdCode = CType(moProductPolicyDatagrid.Rows(nIndex).Cells(EXTERNAL_PROD_CODE).FindControl(EXTERNAL_PROD_CODE_LABEL_CONTROL_NAME), Label).Text

                    State.IsProductPolicyEditMode = True
                    State.ProductPolicyId = New Guid(CType(moProductPolicyDatagrid.Rows(nIndex).Cells(PRODUCT_POLICY_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.MyProductPolicyBO = TheProductCode.GetProductPolicyDetailChild(State.ProductPolicyId)

                    PopulateMyProductPolicyGrid()


                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(moProductPolicyDatagrid, False)
                    State.PageIndex = moProductPolicyDatagrid.PageIndex

                    'DEF-3066
                    State.ProdPolicyEdit = True
                    'DEF-3066

                    PopulateProductPolicyFormFromBO(nIndex)
                    SetFocusOnEditableFieldInGrid(moProductPolicyDatagrid, TYPE_OF_EQUIPMENT, TYPE_OF_EQUIPMENT_CONTROL_NAME, nIndex)
                    SetProductPolicyButtonsState(False)

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    nIndex = CInt(e.CommandArgument)

                    PopulateMyProductPolicyGrid()
                    State.PageIndex = moProductPolicyDatagrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moProductPolicyDatagrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.ProductPolicyId = New Guid(CType(moProductPolicyDatagrid.Rows(nIndex).Cells(PRODUCT_POLICY_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ElseIf (e.CommandName = SAVE_COMMAND) Then

                    SaveRecord()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    'Do the delete here
                    CancelRecord()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub moProductPolicyDatagrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles moProductPolicyDatagrid.RowDataBound
            Try
                'Dim foundrow() As DataRow
                'Dim dr As DataRow
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.ProductPolicySearchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(PRODUCT_POLICY_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_PRODUCT_POLICY_ID), Byte()))

                        If (State.IsProductPolicyEditMode = True AndAlso State.ProductPolicyId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_PRODUCT_POLICY_ID), Byte())))) Then
                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.TYPE_OF_EQUIPMENT_ID).FindControl(Me.TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList),
                            'LookupListNew.GetTypeOfEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
                            Dim typeOfEquipmentLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode())
                            CType(e.Row.Cells(TYPE_OF_EQUIPMENT_ID).FindControl(TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList).Populate(typeOfEquipmentLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.EXTERNAL_PROD_CODE).FindControl(Me.EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList),
                            'LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
                            Dim externalProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACSPC", Thread.CurrentPrincipal.GetLanguageCode())
                            CType(e.Row.Cells(EXTERNAL_PROD_CODE).FindControl(EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList).Populate(externalProdCodeLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                            CType(e.Row.Cells(POLICY_NUM).FindControl(POLICY_NUM_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductPolicy.ProductPolicySearchDV.COL_POLICY_NUM).ToString

                        Else

                            'Dim dvEquip As DataView = LookupListNew.GetTypeOfEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                            Dim typeOfEquipmentLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode())
                            'Dim dvExtprodCode As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                            Dim externalProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACSPC", Thread.CurrentPrincipal.GetLanguageCode())

                            'CType(e.Row.Cells(TYPE_OF_EQUIPMENT).FindControl(TYPE_OF_EQUIPMENT_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(dvEquip, GuidControl.ByteArrayToGuid(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID), Byte())))
                            CType(e.Row.Cells(TYPE_OF_EQUIPMENT).FindControl(TYPE_OF_EQUIPMENT_LABEL_CONTROL_NAME), Label).Text = (From lst In externalProdCodeLkl
                                                                                                                                   Where lst.ListItemId = GuidControl.ByteArrayToGuid(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID), Byte()))
                                                                                                                                   Select lst.Translation).FirstOrDefault()
                            'CType(e.Row.Cells(EXTERNAL_PROD_CODE).FindControl(EXTERNAL_PROD_CODE_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(dvExtprodCode, GuidControl.ByteArrayToGuid(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID), Byte())))
                            CType(e.Row.Cells(EXTERNAL_PROD_CODE).FindControl(EXTERNAL_PROD_CODE_LABEL_CONTROL_NAME), Label).Text = (From lst In externalProdCodeLkl
                                                                                                                                     Where lst.ListItemId = GuidControl.ByteArrayToGuid(CType(dvRow(ProductPolicy.ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID), Byte()))
                                                                                                                                     Select lst.Translation).FirstOrDefault()
                            CType(e.Row.Cells(POLICY_NUM).FindControl(POLICY_NUM_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductPolicy.ProductPolicySearchDV.COL_POLICY_NUM).ToString
                        End If
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moProductPolicyDatagrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles moProductPolicyDatagrid.PageIndexChanging
            Try
                If (Not (State.IsProductPolicyEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moProductPolicyDatagrid.PageIndex = State.PageIndex
                    PopulateMyProductPolicyGrid()
                    moProductPolicyDatagrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateMyProductPolicyGrid()

            'Dim oDataView As DataView
            Try
                With TheProductCode
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.ProductPolicySearchDV Is Nothing Then
                            State.ProductPolicySearchDV = GetProductPolicyDV()
                        End If
                    End If
                End With

                If State.ProductPolicySearchDV IsNot Nothing Then

                    Dim dv As ProductPolicy.ProductPolicySearchDV

                    If State.ProductPolicySearchDV.Count = 0 Then
                        dv = State.ProductPolicySearchDV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.ProductPolicyId, moProductPolicyDatagrid, State.PageIndex)
                        moProductPolicyDatagrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.ProductPolicySearchDV, State.ProductPolicyId, moProductPolicyDatagrid, State.PageIndex)
                        moProductPolicyDatagrid.DataSource = State.ProductPolicySearchDV
                    End If

                    State.ProductPolicySearchDV.Sort = State.ProductPolicySortExpression
                    If (State.IsProductPolicyAfterSave) Then
                        State.IsProductPolicyAfterSave = False
                        SetPageAndSelectedIndexFromGuid(State.ProductPolicySearchDV, State.ProductPolicyId, moProductPolicyDatagrid, moProductPolicyDatagrid.PageIndex)
                    ElseIf (State.IsProductPolicyEditMode) Then
                        SetPageAndSelectedIndexFromGuid(State.ProductPolicySearchDV, State.ProductPolicyId, moProductPolicyDatagrid, moProductPolicyDatagrid.PageIndex, State.IsProductPolicyEditMode)
                    Else
                        'In a Delete scenario...
                        SetPageAndSelectedIndexFromGuid(State.ProductPolicySearchDV, Guid.Empty, moProductPolicyDatagrid, moProductPolicyDatagrid.PageIndex, State.IsProductPolicyEditMode)
                    End If

                    moProductPolicyDatagrid.AutoGenerateColumns = False

                    If State.ProductPolicySearchDV.Count = 0 Then
                        SortAndBindGrid(dv)
                    Else
                        SortAndBindGrid(State.ProductPolicySearchDV)
                    End If

                    If State.ProductPolicySearchDV.Count = 0 Then
                        For Each gvRow As GridViewRow In moProductPolicyDatagrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            If State.MyProductPolicyBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.MyProductPolicyBO, "TypeOfEquipmentId", moProductPolicyDatagrid.Columns(TYPE_OF_EQUIPMENT))
                BindBOPropertyToGridHeader(State.MyProductPolicyBO, "ExternalProdCodeId", moProductPolicyDatagrid.Columns(EXTERNAL_PROD_CODE))
                BindBOPropertyToGridHeader(State.MyProductPolicyBO, "Policy", moProductPolicyDatagrid.Columns(POLICY_NUM))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(code)
        End Sub

        Private Sub PopulateProductPolicyFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = moProductPolicyDatagrid.EditIndex

            Dim EquipDV As DataView = LookupListNew.GetTypeOfEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Dim EqiupDesc As String

            Dim filterString As String = ""

            For Each dr As DataRowView In State.ProductPolicySearchDV
                If State.MyProductPolicyBO.Id <> GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_PRODUCT_POLICY_ID), Byte())) Then
                    EqiupDesc = LookupListNew.GetDescriptionFromId(EquipDV, GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID), Byte())))
                    If State.ProdPolicyAddNew Then
                        If filterString = "" Then
                            filterString = ValidateEquipmentDropDown(EqiupDesc)
                        Else
                            If EqiupDesc IsNot String.Empty Then
                                filterString = filterString + "," + ValidateEquipmentDropDown(EqiupDesc)
                            End If
                        End If
                    ElseIf State.ProdPolicyEdit Then
                        If dr.Row(State.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT).ToString() <> State.SelectedEquipmentType Then
                            If filterString = "" Then
                                filterString = ValidateEquipmentDropDown(EqiupDesc)
                            Else
                                If EqiupDesc IsNot String.Empty Then
                                    filterString = filterString + "," + ValidateEquipmentDropDown(EqiupDesc)
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If filterString <> "" Then
                ' Row filter of card data view already contains the language id condition which we are passing below as it is
                EquipDV.RowFilter = String.Format("{0} and DESCRIPTION not in ({1})", EquipDV.RowFilter, filterString)
            End If

            BindListControlToDataView(CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(TYPE_OF_EQUIPMENT_ID).FindControl(TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList), EquipDV)

            'Dim ExtProdCodeDV As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            'BindListControlToDataView(CType(Me.moProductPolicyDatagrid.Rows(gridRowIdx).Cells(EXTERNAL_PROD_CODE_ID).FindControl(EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList), ExtProdCodeDV)
            Dim extProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACSPC", Thread.CurrentPrincipal.GetLanguageCode())
            CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(EXTERNAL_PROD_CODE_ID).FindControl(EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList).Populate(extProdCodeLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

            Try
                With State.MyProductPolicyBO

                    If (Not .Id.Equals(Guid.Empty)) Then
                        Dim cboEquipmentTYpe As DropDownList = CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(TYPE_OF_EQUIPMENT_ID).FindControl(TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList)
                        If (Not .TypeOfEquipmentId.Equals(Guid.Empty)) Then PopulateControlFromBOProperty(cboEquipmentTYpe, .TypeOfEquipmentId)
                    End If

                    If (Not .Id.Equals(Guid.Empty)) Then
                        Dim cboExternalPordCode As DropDownList = CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(EXTERNAL_PROD_CODE_ID).FindControl(EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList)
                        If (Not .ExternalProdCodeId.Equals(Guid.Empty)) Then
                            PopulateControlFromBOProperty(cboExternalPordCode, .ExternalProdCodeId)
                        End If
                    End If

                    Dim txtProductPolicyNum As TextBox = CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(POLICY_NUM).FindControl(POLICY_NUM_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtProductPolicyNum, .Policy)

                    CType(moProductPolicyDatagrid.Rows(gridRowIdx).Cells(PRODUCT_POLICY_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString

                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function ValidateEquipmentDropDown(strEquipRowValue As String) As String

            Dim filterEqpStr As String = ""

            If strEquipRowValue = "New Or Used" Then
                filterEqpStr = "'" + strEquipRowValue + "'"
                filterEqpStr = filterEqpStr + "" + ",'" + "New" + "'"
                filterEqpStr = filterEqpStr + "" + ",'" + "Used" + "'"
            ElseIf strEquipRowValue = "New" Then
                filterEqpStr = "'" + strEquipRowValue + "'"
                filterEqpStr = filterEqpStr + "" + ",'" + "New Or Used" + "'"
            ElseIf strEquipRowValue = "Used" Then
                filterEqpStr = "'" + strEquipRowValue + "'"
                filterEqpStr = filterEqpStr + "" + ",'" + "New Or Used" + "'"
            Else
                filterEqpStr = "'" + strEquipRowValue + "'"
            End If

            Return filterEqpStr
        End Function

        Private Sub SetProductPolicyButtonsState(bIsEdit As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewProductPolicy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
        End Sub

        Private Function GetProductPolicyDV() As ProductPolicy.ProductPolicySearchDV
            Dim dv As ProductPolicy.ProductPolicySearchDV
            dv = GetDataView()
            dv.Sort = moProductPolicyDatagrid.DataMember()
            moProductPolicyDatagrid.DataSource = dv
            Return (dv)
        End Function

        Private Function GetDataView() As ProductPolicy.ProductPolicySearchDV
            Dim dt As DataTable = TheProductCode.ProductPolicyDetailChildren.Table
            Return New ProductPolicy.ProductPolicySearchDV(dt)
        End Function

        Private Sub SortAndBindGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            moProductPolicyDatagrid.DataSource = dvBinding
            HighLightSortColumn(moProductPolicyDatagrid, State.ProductPolicySortExpression)
            moProductPolicyDatagrid.DataBind()
            If Not moProductPolicyDatagrid.BottomPagerRow.Visible Then moProductPolicyDatagrid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In moProductPolicyDatagrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = State.ProductPolicySearchDV.Count
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moProductPolicyDatagrid)
        End Sub

        Private Sub AddNew()
            State.MyProductPolicyBO = TheProductCode.GetNewProductPolicyDetailChild
            State.ProductPolicySearchDV = GetProductPolicyDV()
            State.PreviousProductPolicySearchDV = State.ProductPolicySearchDV
            State.ProductPolicyId = State.MyProductPolicyBO.Id
            moProductPolicyDatagrid.DataSource = State.ProductPolicySearchDV
            SetPageAndSelectedIndexFromGuid(State.ProductPolicySearchDV, State.ProductPolicyId, moProductPolicyDatagrid, State.PageIndex, State.IsProductPolicyEditMode)
            moProductPolicyDatagrid.AutoGenerateColumns = False

            SortAndBindGrid(State.ProductPolicySearchDV)
            SetGridControls(moProductPolicyDatagrid, False)
            State.ProdPolicyAddNew = True
            PopulateProductPolicyFormFromBO()
        End Sub

        Private Sub ReturnProductPolicyFromEditing()

            moProductPolicyDatagrid.EditIndex = NO_ROW_SELECTED_INDEX

            If moProductPolicyDatagrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, True)
            End If

            SetGridControls(moProductPolicyDatagrid, True)
            State.IsProductPolicyEditMode = False
            PopulateMyProductPolicyGrid()
            State.PageIndex = moProductPolicyDatagrid.PageIndex
            SetProductPolicyButtonsState(True)

        End Sub

        Private Sub PopulateBOFromForm()
            Dim cboEquipmentType As DropDownList = CType(moProductPolicyDatagrid.Rows(moProductPolicyDatagrid.EditIndex).Cells(TYPE_OF_EQUIPMENT_ID).FindControl(TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList)
            Dim EquipmentTypeId As Guid = GetSelectedItem(cboEquipmentType)
            Dim EquipmemtTypedesc As String = LookupListNew.GetDescriptionFromId("TEQP", EquipmentTypeId)

            Dim cboExtProdCode As DropDownList = CType(moProductPolicyDatagrid.Rows(moProductPolicyDatagrid.EditIndex).Cells(EXTERNAL_PROD_CODE_ID).FindControl(EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList)
            Dim ExtProdCodeId As Guid = GetSelectedItem(cboExtProdCode)

            Dim ExtProdCodeDV As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Dim ExtProdDesc As String = LookupListNew.GetDescriptionFromId(ExtProdCodeDV, ExtProdCodeId)

            Dim txtProductPolicy As TextBox = CType(moProductPolicyDatagrid.Rows(moProductPolicyDatagrid.EditIndex).Cells(POLICY_NUM).FindControl(POLICY_NUM_TEXTBOX_CONTROL_NAME), TextBox)

            PopulateBOProperty(State.MyProductPolicyBO, "ProductCodeId", TheProductCode.Id)
            PopulateBOProperty(State.MyProductPolicyBO, "TypeOfEquipmentId", EquipmentTypeId)
            PopulateBOProperty(State.MyProductPolicyBO, "TypeOfEquipment", EquipmemtTypedesc)
            PopulateBOProperty(State.MyProductPolicyBO, "ExternalProdCodeId", ExtProdCodeId)
            PopulateBOProperty(State.MyProductPolicyBO, "ExternalProdCode", ExtProdDesc)
            PopulateBOProperty(State.MyProductPolicyBO, "Policy", txtProductPolicy)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub
#End Region

#Region "ProductPolicyHandlers_buttons"

        Private Sub BtnNewProductPolicy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewProductPolicy_WRITE.Click
            Try
                If Not TheProductCode.Id.Equals(Guid.Empty) Then
                    State.IsProductPolicyEditMode = True
                    State.ProductPolicySearchDV = Nothing
                    State.PreviousProductPolicySearchDV = Nothing
                    AddNew()
                    SetProductPolicyButtonsState(False)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecord()
            Try
                SetGridControls(moProductPolicyDatagrid, True)
                If State.ProdPolicyAddNew Then

                    TheProductCode.RemoveProductPolicyDetailChild(State.ProductPolicyId)
                    State.ProductPolicySearchDV = Nothing
                    State.PreviousProductPolicySearchDV = Nothing
                    State.ProdPolicyAddNew = False
                Else
                    State.ProdPolicyEdit = False
                End If
                ReturnProductPolicyFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecord()
            Try

                BindBoPropertiesToGridHeaders()
                PopulateBOFromForm()
                If (State.MyProductPolicyBO.IsDirty) Then

                    State.MyProductPolicyBO.Save()
                    State.IsProductPolicyAfterSave = True
                    State.AddingProductPolicyNewRow = False
                    State.MyProductPolicyBO.EndEdit()
                    State.IsEditMode = False
                    State.Action = ""
                    State.ProductPolicySearchDV = Nothing
                    State.PreviousProductPolicySearchDV = Nothing
                    State.ProdPolicyAddNew = False
                    State.ProdPolicyEdit = False
                    ReturnProductPolicyFromEditing()

                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnProductPolicyFromEditing()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DoDelete()

            State.MyProdPolicyDetailChildBO = TheProductCode.GetProductPolicyDetailChild(State.ProductPolicyId)
            Try
                State.MyProdPolicyDetailChildBO.Delete()
                State.MyProdPolicyDetailChildBO.Save()
                State.MyProdPolicyDetailChildBO.EndEdit()
                ' Me.State.MyProductPolicyBO.Id = Guid.Empty
                State.MyProdPolicyDetailChildBO = Nothing
                State.ProductPolicySearchDV = Nothing

            Catch ex As Exception
                TheProductCode.RejectChanges()
                Throw ex
            End Try

            ReturnProductPolicyFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                ReturnProductPolicyFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region


#Region "ProductRewards_Handlers_Grid"

        Protected Sub ProductRewardsGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then

                    nIndex = CInt(e.CommandArgument)
                    ProductRewardsGridView.EditIndex = nIndex
                    ProductRewardsGridView.SelectedIndex = nIndex
                    State.IsProductRewardsEditMode = True
                    State.ProductRewardsId = New Guid(CType(ProductRewardsGridView.Rows(nIndex).Cells(PRODUCT_REWARDS_ID).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.MyProductRewardsBO = TheProductCode.GetProductRewardsDetailChild(State.ProductRewardsId)

                    PopulateMyProductRewardsGrid()


                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(ProductRewardsGridView, False)
                    State.PageIndex = ProductRewardsGridView.PageIndex

                    'DEF-3066
                    State.ProdRewardsEdit = True
                    'DEF-3066

                    PopulateProductRewardsFormFromBO(nIndex)
                    SetFocusOnEditableFieldInRewardGrid(ProductRewardsGridView, REWARD_TYPE, REWARD_TYPE_CONTROL_NAME, nIndex)
                    SetProductRewardsButtonsState(False)

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    nIndex = CInt(e.CommandArgument)

                    PopulateMyProductRewardsGrid()
                    State.PageIndex = ProductRewardsGridView.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    ProductRewardsGridView.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.ProductRewardsId = New Guid(CType(ProductRewardsGridView.Rows(nIndex).Cells(PRODUCT_REWARDS_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    State.IsProductRewardsDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then

                    SaveRecordRewards()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    'Do the delete here
                    CancelRecordRewards()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ProductRewardsGridView_RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub ProductBenefitsGridView_RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ProductRewardsGridView.PageIndexChanging
            Try
                If (Not (State.IsProductRewardsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    ProductRewardsGridView.PageIndex = State.PageIndex
                    PopulateMyProductRewardsGrid()
                    ProductRewardsGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProductRewardsGridView.RowDataBound
            Try
                Dim foundrow() As DataRow
                Dim dr As DataRow
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.ProductRewardsSearchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(PRODUCT_REWARDS_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ProductRewards.ProductRewardsSearchDV.COL_PRODUCT_REWARD_ID), Byte()))

                        If (State.IsProductRewardsEditMode = True AndAlso State.ProductRewardsId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ProductRewards.ProductRewardsSearchDV.COL_PRODUCT_REWARD_ID), Byte())))) Then

                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.REWARD_TYPE).FindControl(Me.REWARD_TYPE_CONTROL_NAME), DropDownList),
                            '                           LookupListNew.GetProdRewardNameLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                            Dim productRewardLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRDREWARDNAME", Thread.CurrentPrincipal.GetLanguageCode())
                            CType(e.Row.Cells(REWARD_TYPE).FindControl(REWARD_TYPE_CONTROL_NAME), DropDownList).Populate(productRewardLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                            CType(e.Row.Cells(REWARD_NAME).FindControl(REWARD_NAME_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_REWARD_NAME).ToString
                            CType(e.Row.Cells(REWARD_AMOUNT).FindControl(REWARD_AMOUNT_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_REWARD_AMOUNT).ToString
                            CType(e.Row.Cells(MIN_PURCHASE_PRICE).FindControl(MIN_PURCHASE_PRICE_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_MIN_PURCHASE_PRICE).ToString
                            CType(e.Row.Cells(DAYS_TO_REDEEM).FindControl(DAYS_TO_REDEEM_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_DAYS_TO_REDEEM).ToString
                            CType(e.Row.Cells(FROM_RENEWAL).FindControl(FROM_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_FROM_RENEWAL).ToString
                            CType(e.Row.Cells(TO_RENEWAL).FindControl(TO_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_TO_RENEWAL).ToString
                            If Not DBNull.Value.Equals(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE)) Then
                                CType(e.Row.Cells(START_DATE).FindControl(START_DATE_TEXTBOX), TextBox).Text = GetDateFormattedString(CType(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE), Date))
                            End If
                            If Not DBNull.Value.Equals(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EXPIRATION_DATE)) Then
                                CType(e.Row.Cells(END_DATE).FindControl(END_DATE_TEXTBOX), TextBox).Text = GetDateFormattedString(CType(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EXPIRATION_DATE), Date))
                            End If
                        Else

                            Dim dvRewardType As DataView = LookupListNew.GetProdRewardTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                            CType(e.Row.Cells(REWARD_NAME).FindControl(REWARD_NAME_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_REWARD_NAME).ToString
                            CType(e.Row.Cells(REWARD_TYPE).FindControl(REWARD_TYPE_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_REWARD_TYPE)
                            CType(e.Row.Cells(REWARD_AMOUNT).FindControl(REWARD_AMOUNT_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_REWARD_AMOUNT).ToString
                            CType(e.Row.Cells(MIN_PURCHASE_PRICE).FindControl(MIN_PURCHASE_PRICE_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_MIN_PURCHASE_PRICE).ToString
                            CType(e.Row.Cells(DAYS_TO_REDEEM).FindControl(DAYS_TO_REDEEM_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_DAYS_TO_REDEEM).ToString
                            CType(e.Row.Cells(FROM_RENEWAL).FindControl(FROM_RENEWAL_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_FROM_RENEWAL).ToString
                            CType(e.Row.Cells(TO_RENEWAL).FindControl(TO_RENEWAL_LABEL_CONTROL_NAME), Label).Text = dvRow(ProductRewards.ProductRewardsSearchDV.COL_NAME_TO_RENEWAL).ToString
                            CType(e.Row.Cells(START_DATE).FindControl(STRAT_DATE_LABEL_CONTROL_NAME), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE), Date))
                            CType(e.Row.Cells(END_DATE).FindControl(END_DATE_LABEL_CONTROL_NAME), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EXPIRATION_DATE), Date))

                            Dim oUpgradeDateImage As ImageButton = CType(e.Row.FindControl("DeleteButton_WRITE"), ImageButton)
                            If dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE) IsNot DBNull.Value Then
                                If CType(dvRow.Row(ProductRewards.ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE), Date) > DateTime.Now.Date Then
                                    oUpgradeDateImage.Visible = True
                                Else
                                    oUpgradeDateImage.Visible = False
                                End If
                            End If

                        End If
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateMyProductRewardsGrid()

            'Dim oDataView As DataView
            Try

                With TheProductCode
                    If Not .Id.Equals(Guid.Empty) Then

                        If State.ProductRewardsSearchDV Is Nothing Then
                            State.ProductRewardsSearchDV = GetProductRewardsDV()
                        End If
                    End If
                End With

                If State.ProductRewardsSearchDV IsNot Nothing Then

                    Dim dv As ProductRewards.ProductRewardsSearchDV

                    If State.ProductRewardsSearchDV.Count = 0 Then
                        dv = State.ProductRewardsSearchDV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.ProductRewardsId, ProductRewardsGridView, State.PageIndex)
                        ProductRewardsGridView.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.ProductRewardsSearchDV, State.ProductRewardsId, ProductRewardsGridView, State.PageIndex)
                        ProductRewardsGridView.DataSource = State.ProductRewardsSearchDV
                    End If

                    State.ProductRewardsSearchDV.Sort = State.ProductRewardsSortExpression
                    If (State.IsProductRewardsAfterSave) Then
                        State.IsProductRewardsAfterSave = False
                        SetPageAndSelectedIndexFromGuid(State.ProductRewardsSearchDV, State.ProductRewardsId, ProductRewardsGridView, ProductRewardsGridView.PageIndex)
                    ElseIf (State.IsProductRewardsEditMode) Then
                        SetPageAndSelectedIndexFromGuid(State.ProductRewardsSearchDV, State.ProductRewardsId, ProductRewardsGridView, ProductRewardsGridView.PageIndex, State.IsProductRewardsEditMode)
                    Else
                        'In a Delete scenario...
                        SetPageAndSelectedIndexFromGuid(State.ProductRewardsSearchDV, Guid.Empty, ProductRewardsGridView, ProductRewardsGridView.PageIndex, State.IsProductRewardsEditMode)
                    End If

                    ProductRewardsGridView.AutoGenerateColumns = False

                    If State.ProductRewardsSearchDV.Count = 0 Then
                        SortAndBindRewardsGrid(dv)
                    Else
                        SortAndBindRewardsGrid(State.ProductRewardsSearchDV)
                    End If

                    If State.ProductRewardsSearchDV.Count = 0 Then
                        For Each gvRow As GridViewRow In ProductRewardsGridView.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToRewardsGridHeaders()
            If State.MyProductRewardsBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "RewardName", ProductRewardsGridView.Columns(REWARD_NAME))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "RewardType", ProductRewardsGridView.Columns(REWARD_TYPE))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "RewardAmount", ProductRewardsGridView.Columns(REWARD_AMOUNT))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "MinPurchasePrice", ProductRewardsGridView.Columns(MIN_PURCHASE_PRICE))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "DaysToRedeem", ProductRewardsGridView.Columns(DAYS_TO_REDEEM))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "FromRenewal", ProductRewardsGridView.Columns(FROM_RENEWAL))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "ToRenewal", ProductRewardsGridView.Columns(TO_RENEWAL))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "EffectiveDate", ProductRewardsGridView.Columns(START_DATE))
                BindBOPropertyToGridHeader(State.MyProductRewardsBO, "ExpirationDate", ProductRewardsGridView.Columns(END_DATE))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub BindBoPropertiesToBenefitsGridHeaders()
            If State.MyProductBenefitsBO IsNot Nothing Then
                BindBOPropertyToGridHeader(State.MyProductBenefitsBO, "EquipmentId", ProductBenefitsGridView.Columns(BENEFIT_CELL_EQUIPMENT_ID))
                BindBOPropertyToGridHeader(State.MyProductBenefitsBO, "EffectiveDateProductEquip", ProductBenefitsGridView.Columns(BENEFIT_CELL_START_DATE))
                BindBOPropertyToGridHeader(State.MyProductBenefitsBO, "ExpirationDateProductEquip", ProductBenefitsGridView.Columns(BENEFIT_CELL_END_DATE))
            End If
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub SetFocusOnEditableFieldInRewardGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(code)
        End Sub

        Private Sub PopulateProductRewardsFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)
            Dim btn As ImageButton
            Dim btn1 As ImageButton

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = ProductRewardsGridView.EditIndex

            'Dim RewardTypeDV As DataView = LookupListNew.GetProdRewardTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'BindListControlToDataView(CType(Me.ProductRewardsGridView.Rows(gridRowIdx).Cells(REWARD_TYPE).FindControl(REWARD_TYPE_CONTROL_NAME), DropDownList), RewardTypeDV)
            Dim rewardTypesLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRDREWARDTYPE", Thread.CurrentPrincipal.GetLanguageCode())
            CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(REWARD_TYPE).FindControl(REWARD_TYPE_CONTROL_NAME), DropDownList).Populate(rewardTypesLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Try
                With State.MyProductRewardsBO

                    If (Not .Id.Equals(Guid.Empty)) Then
                        Dim cboRewardType As DropDownList = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(REWARD_TYPE).FindControl(REWARD_TYPE_CONTROL_NAME), DropDownList)
                        If (Not .IsNew AndAlso Not .RewardType.Equals(String.Empty)) Then
                            Dim RewardTypeId As Guid = LookupListNew.GetIdFromDescription(LookupListNew.LK_PROD_REWARD_TYPES, .RewardType)
                            PopulateControlFromBOProperty(cboRewardType, RewardTypeId)
                        End If
                    End If

                    Dim txtRewardName As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(REWARD_NAME).FindControl(REWARD_NAME_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtRewardName, .RewardName)

                    Dim txtRewardAmount As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(REWARD_AMOUNT).FindControl(REWARD_AMOUNT_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtRewardAmount, .RewardAmount)

                    Dim txtMinPurchasePrice As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(MIN_PURCHASE_PRICE).FindControl(MIN_PURCHASE_PRICE_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtMinPurchasePrice, .MinPurchasePrice)

                    Dim txtDaysToRedeem As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(DAYS_TO_REDEEM).FindControl(DAYS_TO_REDEEM_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtDaysToRedeem, .DaysToRedeem)

                    Dim txtFromRenewal As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(FROM_RENEWAL).FindControl(FROM_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtFromRenewal, .FromRenewal)

                    Dim txtToRenewal As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(TO_RENEWAL).FindControl(TO_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtToRenewal, .ToRenewal)

                    Dim txtStartDate As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(START_DATE).FindControl(START_DATE_TEXTBOX), TextBox)
                    PopulateControlFromBOProperty(txtStartDate, .EffectiveDate)

                    Dim txtEndDate As TextBox = CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(END_DATE).FindControl(END_DATE_TEXTBOX), TextBox)
                    PopulateControlFromBOProperty(txtEndDate, .ExpirationDate)

                    CType(ProductRewardsGridView.Rows(gridRowIdx).Cells(PRODUCT_REWARDS_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString

                    btn = DirectCast(ProductRewardsGridView.Rows(gridRowIdx).Cells(START_DATE).FindControl(START_DATE_IMAGE_BUTTON), ImageButton)
                    btn1 = DirectCast(ProductRewardsGridView.Rows(gridRowIdx).Cells(END_DATE).FindControl(END_DATE_IMAGE_BUTTON), ImageButton)

                    If txtStartDate IsNot Nothing Then
                        AddCalendar_New(btn, txtStartDate, , txtStartDate.Text)
                    End If
                    If txtEndDate IsNot Nothing Then
                        AddCalendar_New(btn1, txtEndDate, , txtEndDate.Text)
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateProductBenefitsFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)
            Dim btnStartDate As ImageButton
            Dim btnEndDate As ImageButton

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = ProductBenefitsGridView.EditIndex

            Try
                With State.MyProductBenefitsBO

                    Dim txtProdBenefitsEffectiveDate As TextBox = CType(ProductBenefitsGridView.Rows(gridRowIdx).Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_TEXTBOX), TextBox)
                    PopulateControlFromBOProperty(txtProdBenefitsEffectiveDate, .EffectiveDateProductEquip)

                    Dim txtProdBenefitsExpirationDate As TextBox = CType(ProductBenefitsGridView.Rows(gridRowIdx).Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_TEXTBOX), TextBox)
                    PopulateControlFromBOProperty(txtProdBenefitsExpirationDate, .ExpirationDateProductEquip)

                    CType(ProductBenefitsGridView.Rows(gridRowIdx).Cells(BENEFIT_CELL_ID).FindControl(PROD_BENEFITS_ID_LABEL), Label).Text = .Id.ToString

                    btnStartDate = DirectCast(ProductBenefitsGridView.Rows(gridRowIdx).Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_IMAGE_BUTTON), ImageButton)
                    btnEndDate = DirectCast(ProductBenefitsGridView.Rows(gridRowIdx).Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_IMAGE_BUTTON), ImageButton)

                    If txtProdBenefitsEffectiveDate IsNot Nothing Then
                        AddCalendar_New(btnEndDate, txtProdBenefitsEffectiveDate, , txtProdBenefitsEffectiveDate.Text)
                    End If
                    If txtProdBenefitsExpirationDate IsNot Nothing Then
                        AddCalendar_New(btnEndDate, txtProdBenefitsExpirationDate, , txtProdBenefitsExpirationDate.Text)
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SetProductRewardsButtonsState(bIsEdit As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewProductRewards_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, bIsEdit)
        End Sub

        Private Sub SetProductBenefitsButtonsState(bIsEdit As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewProductBenefits_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnBack, bIsEdit)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, bIsEdit)
        End Sub

        Private Function GetProductRewardsDV() As ProductRewards.ProductRewardsSearchDV

            Dim dv As ProductRewards.ProductRewardsSearchDV

            dv = GetRewardsDataView()
            dv.Sort = ProductRewardsGridView.DataMember()
            ProductRewardsGridView.DataSource = dv
            Return (dv)

        End Function

        Private Function GetRewardsDataView() As ProductRewards.ProductRewardsSearchDV
            Dim dt As DataTable = TheProductCode.ProductRewardsDetailChildren.Table
            Return New ProductRewards.ProductRewardsSearchDV(dt)
        End Function
        Private Function GetProductBenefitsDV() As ProductEquipment.ProductBenefitsSearchDV

            Dim dv As ProductEquipment.ProductBenefitsSearchDV

            dv = GetBenefitsDataView()
            dv.Sort = ProductBenefitsGridView.DataMember()
            ProductBenefitsGridView.DataSource = dv
            Return (dv)

        End Function
        Private Function GetBenefitsDataView() As ProductEquipment.ProductBenefitsSearchDV
            Dim dt As DataTable = TheProductCode.ProductBenefitsDetailChildren.Table
            Return New ProductEquipment.ProductBenefitsSearchDV(dt)
        End Function
        Private Sub SortAndBindRewardsGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            ProductRewardsGridView.DataSource = dvBinding
            HighLightSortColumn(ProductRewardsGridView, State.ProductRewardsSortExpression)
            ProductRewardsGridView.DataBind()
            If Not ProductRewardsGridView.BottomPagerRow.Visible Then ProductRewardsGridView.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In ProductRewardsGridView.Rows
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = State.ProductRewardsSearchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, ProductRewardsGridView)
        End Sub

        Private Sub SortAndBindBenefitsGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            ProductBenefitsGridView.DataSource = dvBinding
            HighLightSortColumn(ProductBenefitsGridView, State.ProductBenefitsSortExpression)
            ProductBenefitsGridView.DataBind()
            If Not ProductBenefitsGridView.BottomPagerRow.Visible Then ProductBenefitsGridView.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In ProductBenefitsGridView.Rows
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = State.ProductBenefitsSearchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, ProductBenefitsGridView)
        End Sub

        Private Sub AddNewReward()

            State.MyProductRewardsBO = TheProductCode.GetNewProductRewardsDetailChild
            State.ProductRewardsSearchDV = GetProductRewardsDV()
            State.PreviousProductRewardsSearchDV = State.ProductRewardsSearchDV
            State.ProductRewardsId = State.MyProductRewardsBO.Id
            ProductRewardsGridView.DataSource = State.ProductRewardsSearchDV
            SetPageAndSelectedIndexFromGuid(State.ProductRewardsSearchDV, State.ProductRewardsId, ProductRewardsGridView, State.PageIndex, State.IsProductRewardsEditMode)
            ProductRewardsGridView.AutoGenerateColumns = False

            SortAndBindRewardsGrid(State.ProductRewardsSearchDV)
            SetGridControls(ProductRewardsGridView, False)
            State.ProdRewardsAddNew = True
            PopulateProductRewardsFormFromBO()
        End Sub

        Private Sub AddNewBenefit()

            State.MyProductBenefitsBO = TheProductCode.GetNewProductBenefitsDetailChild
            State.ProductBenefitsSearchDV = GetProductBenefitsDV()
            State.PreviousProductBenefitsSearchDV = State.ProductBenefitsSearchDV
            State.ProductBenefitsId = State.MyProductBenefitsBO.Id
            ProductBenefitsGridView.DataSource = State.ProductBenefitsSearchDV
            SetPageAndSelectedIndexFromGuid(State.ProductBenefitsSearchDV, State.ProductBenefitsId, ProductBenefitsGridView, State.PageIndex, State.IsProductBenefitsEditMode)
            ProductBenefitsGridView.AutoGenerateColumns = False

            SortAndBindBenefitsGrid(State.ProductBenefitsSearchDV)
            SetGridControls(ProductBenefitsGridView, False)
            State.ProdBenefitsAddNew = True
            PopulateProductBenefitsFormFromBO()
            ScrollTo(topTabs)

        End Sub

        Private Sub ReturnProductRewardsFromEditing()

            ProductRewardsGridView.EditIndex = NO_ROW_SELECTED_INDEX

            If ProductRewardsGridView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, ProductRewardsGridView, False)
            Else
                ControlMgr.SetVisibleControl(Me, ProductRewardsGridView, True)
            End If

            SetGridControls(ProductRewardsGridView, True)
            State.IsProductRewardsEditMode = False
            PopulateMyProductRewardsGrid()
            State.PageIndex = ProductRewardsGridView.PageIndex
            SetProductRewardsButtonsState(True)

        End Sub

        Private Sub ReturnProductBenefitsFromEditing()

            ProductBenefitsGridView.EditIndex = NO_ROW_SELECTED_INDEX

            If ProductBenefitsGridView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, ProductBenefitsGridView, False)
            Else
                ControlMgr.SetVisibleControl(Me, ProductBenefitsGridView, True)
            End If

            SetGridControls(ProductBenefitsGridView, True)
            State.IsProductBenefitsEditMode = False
            PopulateProductBenefitsGrid()
            State.PageIndex = ProductBenefitsGridView.PageIndex
            SetProductBenefitsButtonsState(True)

        End Sub

        Private Sub PopulateRewardsBOFromForm()

            Dim cboRewardType As DropDownList = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(REWARD_TYPE).FindControl(REWARD_TYPE_CONTROL_NAME), DropDownList)
            Dim RewardType As Guid = GetSelectedItem(cboRewardType)
            Dim RewardTypeDV As DataView = LookupListNew.GetProdRewardTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim RewardTypeDesc As String = LookupListNew.GetDescriptionFromId(RewardTypeDV, RewardType)

            Dim txtRewardName As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(REWARD_NAME).FindControl(REWARD_NAME_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtRewardAmount As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(REWARD_AMOUNT).FindControl(REWARD_AMOUNT_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtMinPurchasePrice As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(MIN_PURCHASE_PRICE).FindControl(MIN_PURCHASE_PRICE_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtDaysToRedeem As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(DAYS_TO_REDEEM).FindControl(DAYS_TO_REDEEM_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtFromRenewal As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(FROM_RENEWAL).FindControl(FROM_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtToRenewal As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(TO_RENEWAL).FindControl(TO_RENEWAL_TEXTBOX_CONTROL_NAME), TextBox)
            Dim txtStartDate As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(START_DATE).FindControl(START_DATE_TEXTBOX), TextBox)
            Dim txtEndDate As TextBox = CType(ProductRewardsGridView.Rows(ProductRewardsGridView.EditIndex).Cells(END_DATE).FindControl(END_DATE_TEXTBOX), TextBox)

            PopulateBOProperty(State.MyProductRewardsBO, "ProductCodeId", TheProductCode.Id)
            PopulateBOProperty(State.MyProductRewardsBO, "RewardName", txtRewardName)
            PopulateBOProperty(State.MyProductRewardsBO, "RewardType", RewardTypeDesc)
            PopulateBOProperty(State.MyProductRewardsBO, "RewardAmount", txtRewardAmount)
            PopulateBOProperty(State.MyProductRewardsBO, "MinPurchasePrice", txtMinPurchasePrice)
            PopulateBOProperty(State.MyProductRewardsBO, "DaysToRedeem", txtDaysToRedeem)
            PopulateBOProperty(State.MyProductRewardsBO, "FromRenewal", txtFromRenewal)
            PopulateBOProperty(State.MyProductRewardsBO, "ToRenewal", txtToRenewal)
            PopulateBOProperty(State.MyProductRewardsBO, "EffectiveDate", txtStartDate)
            PopulateBOProperty(State.MyProductRewardsBO, "ExpirationDate", txtEndDate)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub
        Private Sub PopulateBenefitsBOFromForm()
            Try
                Dim equipmentID As Guid
                Dim equipmentMake As String = String.Empty
                Dim equipmentModel As String = String.Empty

                Dim cboMakeModelType As DropDownList = CType(ProductBenefitsGridView.Rows(ProductBenefitsGridView.EditIndex).Cells(BENEFIT_CELL_EQUIPMENT_ID).FindControl(PROD_BENEFITS_MAKE_DROPDOWNLIST), DropDownList)
                If cboMakeModelType IsNot Nothing Then
                    equipmentID = GetSelectedItem(cboMakeModelType)

                    Dim equipmentMakeModelDV As DataView = LookupListNew.GetEquipmentMakeAndModel(equipmentID)
                    If equipmentMakeModelDV IsNot Nothing AndAlso equipmentMakeModelDV.Count > 0 Then
                        equipmentMake = equipmentMakeModelDV(0)("EQUIPMENT_MAKE")
                        equipmentModel = equipmentMakeModelDV(0)("EQUIPMENT_MODEL")
                    End If
                End If

                Dim txtStartDate As TextBox = CType(ProductBenefitsGridView.Rows(ProductBenefitsGridView.EditIndex).Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_TEXTBOX), TextBox)
                Dim txtEndDate As TextBox = CType(ProductBenefitsGridView.Rows(ProductBenefitsGridView.EditIndex).Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_TEXTBOX), TextBox)

                PopulateBOProperty(State.MyProductBenefitsBO, "EquipmentId", equipmentID)
                PopulateBOProperty(State.MyProductBenefitsBO, "EffectiveDateProductEquip", txtStartDate)
                PopulateBOProperty(State.MyProductBenefitsBO, "ExpirationDateProductEquip", txtEndDate)

                PopulateBOProperty(State.MyProductBenefitsBO, "EquipmentMake", equipmentMake)
                PopulateBOProperty(State.MyProductBenefitsBO, "EquipmentModel", equipmentModel)

                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "ProductRewardsHandlers_buttons"

        Private Sub btnNewProductRewards_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewProductRewards_WRITE.Click
            Try
                If Not TheProductCode.Id.Equals(Guid.Empty) Then
                    State.IsProductRewardsEditMode = True
                    State.ProductRewardsSearchDV = Nothing
                    State.PreviousProductRewardsSearchDV = Nothing
                    AddNewReward()
                    SetProductRewardsButtonsState(False)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecordRewards()
            Try
                SetGridControls(ProductRewardsGridView, True)
                If State.ProdRewardsAddNew Then

                    TheProductCode.RemoveProductRewardsDetailChild(State.ProductRewardsId)
                    State.ProductRewardsSearchDV = Nothing
                    State.PreviousProductRewardsSearchDV = Nothing
                    State.ProdRewardsAddNew = False
                Else
                    State.ProdRewardsEdit = False
                End If
                ReturnProductRewardsFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecordBenefits()
            Try
                SetGridControls(ProductBenefitsGridView, True)
                If State.ProdBenefitsAddNew Then

                    TheProductCode.RemoveProductBenefitsDetailChild(State.ProductBenefitsId)
                    State.ProductBenefitsSearchDV = Nothing
                    State.PreviousProductBenefitsSearchDV = Nothing
                    State.ProdBenefitsAddNew = False
                Else
                    State.ProdBenefitsEdit = False
                End If
                ReturnProductBenefitsFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecordRewards()
            Try

                BindBoPropertiesToRewardsGridHeaders()
                PopulateRewardsBOFromForm()
                If (State.MyProductRewardsBO.IsDirty) Then

                    State.MyProductRewardsBO.Save()
                    State.IsProductRewardsAfterSave = True
                    State.AddingProductRewardsNewRow = False
                    State.MyProductRewardsBO.EndEdit()
                    State.IsEditMode = False
                    State.Action = ""
                    State.ProductRewardsSearchDV = Nothing
                    State.PreviousProductRewardsSearchDV = Nothing
                    State.ProdRewardsAddNew = False
                    State.ProdRewardsEdit = False
                    ReturnProductRewardsFromEditing()

                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnProductRewardsFromEditing()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecordBenefits()
            Try

                BindBoPropertiesToBenefitsGridHeaders()
                PopulateBenefitsBOFromForm()
                If (State.MyProductBenefitsBO.IsDirty) Then

                    State.MyProductBenefitsBO.Save()
                    State.IsProductBenefitsAfterSave = True
                    State.AddingProductBenefitsNewRow = False
                    State.MyProductBenefitsBO.EndEdit()
                    State.IsEditMode = False
                    State.Action = ""
                    State.ProductBenefitsSearchDV = Nothing
                    State.PreviousProductBenefitsSearchDV = Nothing
                    State.ProdBenefitsAddNew = False
                    State.ProdBenefitsEdit = False
                    ReturnProductBenefitsFromEditing()

                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnProductBenefitsFromEditing()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DoDeleteRewards()

            State.MyProdRewardsDetailChildBO = TheProductCode.GetProductRewardsDetailChild(State.ProductRewardsId)
            Try
                State.MyProdRewardsDetailChildBO.Delete()
                State.MyProdRewardsDetailChildBO.Save()
                State.MyProdRewardsDetailChildBO.EndEdit()

                State.MyProdRewardsDetailChildBO = Nothing
                State.ProductRewardsSearchDV = Nothing

            Catch ex As Exception
                TheProductCode.RejectChanges()
                Throw ex
            End Try

            ReturnProductRewardsFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsEditMode = False
        End Sub

        Private Sub DoDeleteBenefits()

            State.MyProdBenefitsDetailChildBO = TheProductCode.GetProductBenefitsDetailChild(State.ProductBenefitsId)
            Try
                State.MyProdBenefitsDetailChildBO.Delete()
                State.MyProdBenefitsDetailChildBO.Save()
                State.MyProdBenefitsDetailChildBO.EndEdit()

                State.MyProdBenefitsDetailChildBO = Nothing
                State.ProductBenefitsSearchDV = Nothing

            Catch ex As Exception
                TheProductCode.RejectChanges()
                Throw ex
            End Try

            ReturnProductBenefitsFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirmRewards()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDeleteRewards()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                ReturnProductRewardsFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirmBenefits()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDeleteBenefits()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                ReturnProductBenefitsFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region
        'REQ-5586 
        Private Sub moProdLiabilityLimitBasedOnDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moProdLiabilityLimitBasedOnDrop.SelectedIndexChanged
            Try
                If (GetSelectedItem(moProdLiabilityLimitBasedOnDrop).Equals(Guid.Empty) OrElse GetSelectedItem(moProdLiabilityLimitBasedOnDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) Then
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyDrop, False)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitText, False)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentText, False)
                    'REQ-6289
                    ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDLabel, False)
                    ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDDrop, False)

                    ' When "Claim/Liability limit Based On" is set to "Not Applicable" or empty, all the following fileds should
                    ' be empty" 
                    ' - Claim/Liability limit Policy
                    ' - Claim/Liability limit Applied To
                    ClearList(moProdLimitApplicableToXCDDrop)
                    ' - Claim/Liability Limit
                    moProdLiabilityLimitText.Text = Nothing
                    ' - Claim/Liability Limit %
                    moProdLiabilityLimitPercentText.Text = Nothing

                    'REQ-6289-END
                    ClearList(moProdLiabilityLimitPolicyDrop)
                Else
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyLabel, True)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPolicyDrop, True)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitLabel, True)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitText, True)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentLabel, True)
                    ControlMgr.SetVisibleControl(Me, moProdLiabilityLimitPercentText, True)
                    'BindListControlToDataView(moProdLiabilityLimitPolicyDrop, LookupListNew.GetProdLiabilityLimitPolicyList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                    Dim productLimitPolicyLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRODLILIMPOLICY", Thread.CurrentPrincipal.GetLanguageCode())
                    moProdLiabilityLimitPolicyDrop.Populate(productLimitPolicyLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                    SetSelectedItem(moProdLiabilityLimitPolicyDrop, LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_POLICY_TYPES, PORD_LIAB_LIMIT_POLICY_DEFAULT))

                    'REQ-6289
                    ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDLabel, True)
                    ControlMgr.SetVisibleControl(Me, moProdLimitApplicableToXCDDrop, True)
                    BindCodeToListControl(moProdLimitApplicableToXCDDrop, LookupListNew.GetProdLimitAppToXCDList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                    SetSelectedItem(moProdLimitApplicableToXCDDrop, PRODUCT_LIMIT_APP_TO_XCD_DEFAULT)
                    'REQ-6289-END

                    moProdLiabilityLimitText.Text = "0"
                    moProdLiabilityLimitPercentText.Text = ""
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub moReInsuredDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moReInsuredDrop.SelectedIndexChanged

            If State.IsProductCodeNew = True Then
                'REQ-5888-START
                'If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) OrElse GetSelectedItem(Me.moReInsuredDrop).Equals(Guid.Empty) Then
                '    tab1_moAttributes_WRITE.Enabled = False
                '    AttributeValues.Visible = False
                'Else
                '    tab1_moAttributes_WRITE.Enabled = True
                '    AttributeValues.Visible = True
                'End If
                'REQ-5888-END
            Else
                If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) OrElse GetSelectedItem(moReInsuredDrop).Equals(Guid.Empty) Then
                    'REQ-5888-START
                    'tab1_moAttributes_WRITE.Enabled = False
                    'AttributeValues.Visible = False
                    'If Not TheProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                    '    Dim attributevaluebo As AttributeValue = TheProductCode.AttributeValues.First
                    '    attributevaluebo.Delete()
                    '    attributevaluebo.Save()
                    '    attributevaluebo = Nothing
                    'End If
                    'REQ-5888-END
                ElseIf GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                    'REQ-5888-START
                    'tab1_moAttributes_WRITE.Enabled = True
                    'AttributeValues.Visible = True
                    'REQ-5888-END
                End If
            End If
        End Sub

        Private Sub moUpgTermUOMDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moUpgTermUOMDrop.SelectedIndexChanged
            If (GetSelectedItem(moUpgTermUOMDrop).Equals(Guid.Empty)) Then
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                moUpgradeTermFROMText.Text = Nothing
                moUpgradeTermToText.Text = Nothing
                moUpgradeTermText.Text = Nothing

            ElseIf GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_DAYS) OrElse GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_MONTHS) Then
                moUpgradeTermFROMText.Text = Nothing
                moUpgradeTermToText.Text = Nothing
                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
            ElseIf GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_DAYS) OrElse GetSelectedItem(moUpgTermUOMDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Codes.UPG_UNIT_OF_MEASURE__RANGE_IN_MONTHS) Then
                moUpgradeTermText.Text = Nothing
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, True)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
            End If
        End Sub

        Private Sub moUpgradeProgramDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moUpgradeProgramDrop.SelectedIndexChanged
            If (GetSelectedItem(moUpgradeProgramDrop).Equals(Guid.Empty) OrElse GetSelectedItem(moUpgradeProgramDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFromLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermFROMText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermToText, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgradeTermText, False)
                ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireLabel, False)
                ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireDrop, False)
                ControlMgr.SetVisibleControl(Me, moUpgTermUOMLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgTermUOMDrop, False)
                ControlMgr.SetVisibleControl(Me, moUpgFeeLabel, False)
                ControlMgr.SetVisibleControl(Me, moUpgFeeText, False)
                moUpgradeTermFROMText.Text = Nothing
                moUpgradeTermToText.Text = Nothing
                moUpgradeTermText.Text = Nothing
                ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethLabel, False)
                ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethDrop, False)
                ClearList(moupgFinanceInfoRequireDrop)
                ClearList(moUpgTermUOMDrop)
                ClearList(moUPGFinanceBalCompMethDrop)
            ElseIf GetSelectedItem(moUpgradeProgramDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y) Then
                moUpgradeTermText.Text = Nothing
                ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireLabel, True)
                ControlMgr.SetVisibleControl(Me, moupgFinanceInfoRequireDrop, True)
                ControlMgr.SetVisibleControl(Me, moUpgTermUOMLabel, True)
                ControlMgr.SetVisibleControl(Me, moUpgTermUOMDrop, True)
                ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethLabel, True)
                ControlMgr.SetVisibleControl(Me, moUPGFinanceBalCompMethDrop, True)
                ControlMgr.SetVisibleControl(Me, moUpgFeeLabel, True)
                ControlMgr.SetVisibleControl(Me, moUpgFeeText, True)
            End If
        End Sub

        Protected Sub mo_ParentsGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs)
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dv As New DataView
                If e.Row.RowType = DataControlRowType.DataRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        Dim gvChilds As GridView = TryCast(e.Row.FindControl("gvChilds"), GridView)
                        dv = State.childsearchDV

                        State.Parent_Product_code_id = GuidControl.ByteArrayToGuid(mo_ParentsGrid.DataKeys(e.Row.RowIndex).Values(0))

                        dv.RowFilter = "product_code_parent_id = '" & GuidControl.GuidToHexString(State.Parent_Product_code_id) & "'"
                        gvChilds.DataSource = dv
                        gvChilds.DataBind()

                        e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(CType(dvRow(ProductCodeParent.ProductCodeSearchDV.COL_EFFECTIVE), Date))
                        e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(CType(dvRow(ProductCodeParent.ProductCodeSearchDV.COL_EXPIRATION), Date))

                        ' manage Payment Split Rule Field edition
                        Dim paymentSplitRuleText As String = dvRow(ProductCodeParent.ProductCodeSearchDV.COL_PAYMENT_SPLIT_RULE_ID).ToString()

                        Dim lblLocalPaymentSplitRule As Label = TryCast(e.Row.FindControl("lblPaymentSplitRule"), Label)
                        If lblLocalPaymentSplitRule IsNot Nothing Then
                            ' read mode
                            lblLocalPaymentSplitRule.Text = paymentSplitRuleText
                        End If

                        ' edit mode
                        Dim ddlLocalPaymentSplitRule As DropDownList = CType(e.Row.FindControl("ddlPaymentSplitRule"), DropDownList)
                        If ddlLocalPaymentSplitRule IsNot Nothing Then
                            ddlLocalPaymentSplitRule.Populate(State.PaymentSplitRuleLkl, New PopulateOptions() With
                                                         {
                                                         .AddBlankItem = True,
                                                         .BlankItemValue = String.Empty
                                                         })

                            Dim listItem As WebControls.ListItem = ddlLocalPaymentSplitRule.Items.FindByText(paymentSplitRuleText)
                            If listItem IsNot Nothing Then
                                ddlLocalPaymentSplitRule.SelectedValue = listItem.Value
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub mo_ParentsGrid_OnRowCommand(sender As Object, e As GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    mo_ParentsGrid.EditIndex = nIndex
                    PopulateProductParentGrid()
                ElseIf (e.CommandName = SAVE_COMMAND) Then
                    mo_ParentsGrid.EditIndex = NO_ROW_SELECTED_INDEX
                    SaveRecordParent(nIndex)
                    PopulateProductParentGrid()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    mo_ParentsGrid.EditIndex = NO_ROW_SELECTED_INDEX
                    PopulateProductParentGrid()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecordParent(nIndex As Integer)
            Dim gvRow As GridViewRow = mo_ParentsGrid.Rows(nIndex)

            If gvRow IsNot Nothing Then
                ' Get the ProductCodeParentBo
                Dim productCodeParentId As Guid = GuidControl.ByteArrayToGuid(mo_ParentsGrid.DataKeys(gvRow.RowIndex).Values(0))
                Dim productCodeParentBo As ProductCodeParent = New ProductCodeParent(productCodeParentId)

                Dim ddlLocalPaymentSplitRule As DropDownList = TryCast(gvRow.FindControl("ddlPaymentSplitRule"), DropDownList)

                Dim paymentSplitRuleId As Guid = Nothing
                ' Get the list Item Id
                Guid.TryParse(ddlLocalPaymentSplitRule.SelectedValue, paymentSplitRuleId)

                ' Save if only there are changes.
                If productCodeParentBo.PaymentSplitRuleId <> paymentSplitRuleId Then
                    productCodeParentBo.PaymentSplitRuleId = paymentSplitRuleId
                    productCodeParentBo.Save()
                End If
                ' update the dataSource
                State.parentsearchDV = ProductCodeParent.GetList(State.oDealer.Id, State.moProductCode.Id)
            End If
        End Sub

        Protected Sub gvChilds_RowDataBound(sender As Object, e As GridViewRowEventArgs)
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If e.Row.RowType = DataControlRowType.DataRow Then

                    e.Row.Cells(CHILD_GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(ProductCodeDetail.ProductCodeSearchDV.COL_EFFECTIVE)))
                    e.Row.Cells(CHILD_GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(ProductCodeDetail.ProductCodeSearchDV.COL_EXPIRATION)))

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#Region "Product Equipment"
#Region "Button Management Product Equipment "
        Private Sub EnableEditRateButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnSaveProdEquip_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, BtnCancelProdEquip, bIsReadWrite)

        End Sub
        Private Sub EnableEditBenefitsButtons(bIsReadWrite As Boolean)


        End Sub

#End Region
#Region "Handlers-ProductEquipment-Buttons"
        Private Sub BtnCancelProdEquip_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelProdEquip.Click
            Try
                PopulateProductEquipmentGrid(ACTION_CANCEL)
            Catch ex As Exception
                HandleErrors(ex, ErrorControllerProductEquipment)
            End Try
        End Sub
        Private Sub BtnSaveProdEquip_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSaveProdEquip_WRITE.Click
            Try
                If ApplyProductEquipmentChanges() = True Then
                    PopulateProductEquipmentGrid(ACTION_SAVE)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorControllerProductEquipment)
            End Try
        End Sub
#End Region
#Region "Handlers-ProductEquipment-DataGrid"
        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)
            Dim effectiveDateImageButton As ImageButton
            Dim effectiveDateText As TextBox
            Dim oGridViewrow As GridViewRow

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    ProductEquipmentGridView.EditIndex = nIndex
                    ProductEquipmentGridView.SelectedIndex = nIndex
                    PopulateProductEquipmentGrid(ACTION_EDIT)
                    SetGridControls(ProductEquipmentGridView, False)
                    SetFocusInGrid(ProductEquipmentGridView, nIndex, GRID_COL_PE_EXPIRATION_IDX)

                    'Date Calendars
                    oGridViewrow = ProductEquipmentGridView.Rows(nIndex)
                    effectiveDateImageButton = CType(oGridViewrow.FindControl(PE_EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
                    effectiveDateText = CType(oGridViewrow.FindControl(PE_EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                    AddCalendar_New(effectiveDateImageButton, effectiveDateText)

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorControllerProductEquipment)
            End Try
        End Sub
#End Region
#Region "Populate Product Equipment"
        Public Sub PopulateProductEquipmentGrid(Optional ByVal oAction As String = ACTION_NONE)
            Dim oProductEquipment As ProductEquipment
            Dim oDataView As DataView

            Try
                oDataView = oProductEquipment.GetList(State.moProductCodeId)

                Select Case oAction
                    Case ACTION_NONE
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, ProductEquipmentGridView, 0)
                        EnableEditRateButtons(False)
                        SetProductPolicyButtonsState(True)
                    Case ACTION_EDIT
                        State.ProductEquipmentId = GetGuidFromString(GetSelectedGridText(ProductEquipmentGridView, GRID_COL_PE_ID_IDX))
                        EnableEditRateButtons(True)
                        SetProductPolicyButtonsState(False)
                    Case ACTION_CANCEL
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, ProductEquipmentGridView, ProductEquipmentGridView.PageIndex)
                        EnableEditRateButtons(False)
                        SetProductPolicyButtonsState(True)
                    Case ACTION_SAVE
                        SetPageAndSelectedIndexFromGuid(oDataView, State.ProductEquipmentId, ProductEquipmentGridView,
                                    ProductEquipmentGridView.PageIndex)
                        EnableEditRateButtons(False)
                        SetProductPolicyButtonsState(True)
                End Select

                ProductEquipmentGridView.AutoGenerateColumns = False
                ProductEquipmentGridView.DataSource = oDataView
                ProductEquipmentGridView.DataBind()
                ControlMgr.SetVisibleControl(Me, ProductEquipmentGridView, True)

                If Not State.ProductEquipmentGridTranslated Then
                    TranslateGridHeader(ProductEquipmentGridView)
                    State.ProductEquipmentGridTranslated = True
                End If

            Catch ex As Exception
                ErrorControllerProductEquipment.AddError(PRODUCT_EQUIPMENT_TAB001)
                ErrorControllerProductEquipment.AddError(ex.Message, False)
                ErrorControllerProductEquipment.Show()
            End Try

        End Sub
#End Region
#Region "Other Function - Product Equipment"
        Private Function ApplyProductEquipmentChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If ProductEquipmentGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' not in edit mode

            BindBoPropertiesToGridHeader()
            With TheProductEquipment
                PopulateProductEquipmentBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableEditRateButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
        Private Sub PopulateProductEquipmentBOFromForm()
            With TheProductEquipment
                PopulateBOProperty(TheProductEquipment, EXPIRATION_DATE_PRODUCT_EQUIP_PROPERTY, CType(GetSelectedGridControl(ProductEquipmentGridView, GRID_COL_PE_EXPIRATION_IDX), TextBox))
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Function IsDirtyProductEquipmentBO() As Boolean
            Dim bIsDirty As Boolean = True
            If ProductEquipmentGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' not in edit mode
            Try
                With TheProductEquipment
                    PopulateProductEquipmentBOFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                ErrorControllerProductEquipment.AddError(PRODUCT_EQUIPMENT_TAB001)
                ErrorControllerProductEquipment.AddError(ex.Message, False)
                ErrorControllerProductEquipment.Show()
            End Try
            Return bIsDirty
        End Function
        Private Sub BindBoPropertiesToGridHeader()
            BindBOPropertyToGridHeader(TheProductEquipment, EXPIRATION_DATE_PRODUCT_EQUIP_PROPERTY, ProductEquipmentGridView.Columns(GRID_COL_PE_EXPIRATION_IDX))
        End Sub

        Private Sub cboAllowRegisteredItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAllowRegisteredItems.SelectedIndexChanged
            Try
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                If cboAllowRegisteredItems.SelectedValue = "YESNO-Y" Then
                    ControlMgr.SetVisibleControl(Me, lblListForDeviceGroup, True)
                    ControlMgr.SetVisibleControl(Me, cboListForDeviceGroup, True)
                    ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemLabel, True)
                    ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemText, True)
                    ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedLabel, True)
                    ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedText, True)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemlabel, True)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemText, True)
                    'REQ-6002
                    ControlMgr.SetVisibleControl(Me, trUpdateReplaceRegItemsId, True)
                    ControlMgr.SetVisibleControl(Me, cboUpdateReplaceRegItemsId, True)
                    ControlMgr.SetVisibleControl(Me, txtRegisteredItemsLimit, True)

                    'BindListControlToDataView(cboListForDeviceGroup, LookupListNew.GetDeviceGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                    Dim deviceGroupList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DeviceGroups", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    cboListForDeviceGroup.Populate(deviceGroupList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })
                    'BindSelectItem(TheProductCode.ListForDeviceGroups.ToString, cboListForDeviceGroup)
                Else
                    If State.AttachChildCount > 0 Then
                        'Me.cboAllowRegisteredItems.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                        cboListForDeviceGroup.Populate(yesNoLkl.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = False,
                                            .TextFunc = AddressOf .GetDescription,
                                            .ValueFunc = AddressOf .GetExtendedCode
                                        })
                        BindSelectItem(TheProductCode.AllowRegisteredItems, cboAllowRegisteredItems)
                        MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                        Throw New GUIException("", "")
                    End If

                    ControlMgr.SetVisibleControl(Me, lblListForDeviceGroup, False)
                    ControlMgr.SetVisibleControl(Me, cboListForDeviceGroup, False)
                    ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemLabel, False)
                    ControlMgr.SetVisibleControl(Me, moMaxAgeOfRegisteredItemText, False)
                    ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedLabel, False)
                    ControlMgr.SetVisibleControl(Me, moMaxRegistrationsAllowedText, False)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemlabel, False)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitPerRegisteredItemText, False)

                    'REQ-6002
                    ControlMgr.SetVisibleControl(Me, trUpdateReplaceRegItemsId, False)
                    ControlMgr.SetVisibleControl(Me, cboUpdateReplaceRegItemsId, False)
                    ControlMgr.SetVisibleControl(Me, txtRegisteredItemsLimit, False)
                    cboUpdateReplaceRegItemsId.ClearSelection()
                    txtRegisteredItemsLimit.Text = Nothing

                    moMaxAgeOfRegisteredItemText.Text = Nothing
                    moMaxRegistrationsAllowedText.Text = Nothing
                    moClaimLimitPerRegisteredItemText.Text = Nothing
                    cboListForDeviceGroup.ClearSelection()
                    DisabledTabsList.Add(tab_DeviceTypeDetails)
                End If
            Catch ex As GUIException

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboListForDeviceGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboListForDeviceGroup.SelectedIndexChanged
            Try
                If State.AttachChildCount > 0 Then
                    'BindListControlToDataView(cboListForDeviceGroup, LookupListNew.GetDeviceGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                    Dim deviceGroupList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DeviceGroups", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    cboListForDeviceGroup.Populate(deviceGroupList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                    BindSelectItem(TheProductCode.ListForDeviceGroups.ToString, cboListForDeviceGroup)
                    MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                    Throw New GUIException("", "")
                End If

                If cboListForDeviceGroup.SelectedIndex > 0 Then
                    If DisabledTabsList.Contains(tab_DeviceTypeDetails) = True Then
                        DisabledTabsList.Remove(tab_DeviceTypeDetails)
                    End If
                    Dim DeviceGroupDV As DataView = LookupListNew.GetDeviceGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Dim DeviceGroupCode As String = DeviceGroupDV.Table.Rows(0)("code")
                    PopulateDeviceType(DeviceGroupCode)
                Else
                    DisabledTabsList.Add(tab_DeviceTypeDetails)
                End If

            Catch ex As GUIException

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


#Region "Handlers Benefits Buttons"
        Private Sub btnNewProductBenefits_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewProductBenefits_WRITE.Click
            Try
                If Not TheProductCode.Id.Equals(Guid.Empty) Then
                    State.IsProductBenefitsEditMode = True
                    State.ProductBenefitsSearchDV = Nothing
                    State.PreviousProductBenefitsSearchDV = Nothing
                    AddNewBenefit()
                    SetProductBenefitsButtonsState(False)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        'Private Sub BtnCancelProdEquipBenefits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelProdEquipBenef.Click
        '    Try
        '        Me.PopulateProductBenefitsGrid(ACTION_CANCEL)
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, ErrorControllerProductEquipment)
        '    End Try
        'End Sub
        'Private Sub BtnSaveProdEquipBenefits_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveProdEquipBenef_WRITE.Click
        '    Try
        '        If ApplyProductEquipmentBenefitsChanges() = True Then
        '            PopulateProductBenefitsGrid(ACTION_SAVE)
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, ErrorControllerProductEquipment)
        '    End Try
        'End Sub
#End Region
#Region "DataGrid Benefits"

        Protected Sub ProductBenefitsGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then

                    nIndex = CInt(e.CommandArgument)
                    ProductBenefitsGridView.EditIndex = nIndex
                    ProductBenefitsGridView.SelectedIndex = nIndex
                    State.IsProductBenefitsEditMode = True
                    State.ProductBenefitsId = New Guid(CType(ProductBenefitsGridView.Rows(nIndex).Cells(BENEFIT_CELL_ID).FindControl(PROD_BENEFITS_ID_LABEL), Label).Text)

                    State.MyProductBenefitsBO = TheProductCode.GetProductBenefitsDetailChild(State.ProductBenefitsId)

                    'PopulateMyProductBenefitsGrid()
                    PopulateProductBenefitsGrid()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(ProductBenefitsGridView, False)
                    State.PageIndex = ProductBenefitsGridView.PageIndex

                    'DEF-3066
                    State.ProdBenefitsEdit = True
                    'DEF-3066

                    PopulateProductBenefitsFormFromBO(nIndex)
                    SetFocusOnEditableFieldInRewardGrid(ProductBenefitsGridView, BENEFIT_CELL_MAKE, PROD_BENEFITS_MAKE_DROPDOWNLIST, nIndex)
                    SetProductBenefitsButtonsState(False)
                    ScrollTo(topTabs)

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    nIndex = CInt(e.CommandArgument)

                    PopulateProductBenefitsGrid()
                    State.PageIndex = ProductBenefitsGridView.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    ProductBenefitsGridView.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.ProductBenefitsId = New Guid(CType(ProductBenefitsGridView.Rows(nIndex).Cells(BENEFIT_CELL_ID).FindControl(PROD_BENEFITS_ID_LABEL), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    State.IsProductBenefitsDelete = True
                ElseIf (e.CommandName = SAVE_COMMAND) Then

                    SaveRecordBenefits()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    'Do the delete here
                    CancelRecordBenefits()
                    ScrollTo(topTabs)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub BenefitsItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)
            Dim effectiveDateImageButton As ImageButton
            Dim effectiveDateText As TextBox
            Dim oGridViewrow As GridViewRow

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    ProductBenefitsGridView.EditIndex = nIndex
                    ProductBenefitsGridView.SelectedIndex = nIndex
                    PopulateProductBenefitsGrid(ACTION_EDIT)
                    SetGridControls(ProductBenefitsGridView, False)
                    SetFocusInGrid(ProductBenefitsGridView, nIndex, GRID_COL_PE_EXPIRATION_IDX)

                    'Date Calendars
                    oGridViewrow = ProductBenefitsGridView.Rows(nIndex)
                    effectiveDateImageButton = CType(oGridViewrow.FindControl(PE_EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
                    effectiveDateText = CType(oGridViewrow.FindControl(PE_EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                    AddCalendar_New(effectiveDateImageButton, effectiveDateText)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Populate Benefits"
        Public Sub PopulateProductBenefitsGrid(Optional ByVal oAction As String = ACTION_NONE)

            Try

                With TheProductCode
                    If Not .Id.Equals(Guid.Empty) Then
                        If State.ProductBenefitsSearchDV Is Nothing Then
                            State.ProductBenefitsSearchDV = GetProductBenefitsDV()
                        End If
                    End If
                End With

                If State.ProductBenefitsSearchDV IsNot Nothing Then

                    Dim dv As ProductEquipment.ProductBenefitsSearchDV

                    If State.ProductBenefitsSearchDV.Count = 0 Then
                        dv = State.ProductBenefitsSearchDV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, State.ProductBenefitsId, ProductBenefitsGridView, State.PageIndex)
                        ProductBenefitsGridView.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(State.ProductBenefitsSearchDV, State.ProductBenefitsId, ProductBenefitsGridView, State.PageIndex)
                        ProductBenefitsGridView.DataSource = State.ProductBenefitsSearchDV
                    End If


                    State.ProductBenefitsSearchDV.Sort = State.ProductBenefitsSortExpression
                    If (State.IsProductBenefitsAfterSave) Then
                        State.IsProductBenefitsAfterSave = False
                        SetPageAndSelectedIndexFromGuid(State.ProductBenefitsSearchDV, State.ProductBenefitsId, ProductBenefitsGridView, ProductBenefitsGridView.PageIndex)
                    ElseIf (State.IsProductBenefitsEditMode) Then
                        SetPageAndSelectedIndexFromGuid(State.ProductBenefitsSearchDV, State.ProductBenefitsId, ProductBenefitsGridView, ProductBenefitsGridView.PageIndex, State.IsProductBenefitsEditMode)
                    Else
                        'In a Delete scenario...
                        SetPageAndSelectedIndexFromGuid(State.ProductBenefitsSearchDV, Guid.Empty, ProductBenefitsGridView, ProductBenefitsGridView.PageIndex, State.IsProductBenefitsEditMode)
                    End If

                    ProductBenefitsGridView.AutoGenerateColumns = False

                    If State.ProductBenefitsSearchDV.Count = 0 Then
                        SortAndBindBenefitsGrid(dv)
                    Else
                        SortAndBindBenefitsGrid(State.ProductBenefitsSearchDV)
                    End If

                    If State.ProductBenefitsSearchDV.Count = 0 Then
                        For Each gvRow As GridViewRow In ProductRewardsGridView.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If

                If Not State.ProductEquipmentBenefitsGridTranslated Then
                    TranslateGridHeader(ProductBenefitsGridView)
                    State.ProductEquipmentBenefitsGridTranslated = True
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try


        End Sub
#End Region
#Region "Other benefits methods"
        Private Function ApplyProductEquipmentBenefitsChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If ProductEquipmentGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' not in edit mode

            BindBenefitsBoPropertiesToGridHeader()
            With TheProductEquipment
                PopulateProductBenefitsBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableEditBenefitsButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
        Private Sub PopulateProductBenefitsBOFromForm()
            With TheProductEquipment
                PopulateBOProperty(TheProductEquipment, EXPIRATION_DATE_PRODUCT_EQUIP_PROPERTY, CType(GetSelectedGridControl(ProductEquipmentGridView, GRID_COL_PE_EXPIRATION_IDX), TextBox))
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Function IsDirtyProductBenefitsBO() As Boolean
            Dim bIsDirty As Boolean = True
            If ProductBenefitsGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' not in edit mode
            Try
                With TheProductBenefits
                    PopulateProductBenefitsBOFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            Return bIsDirty
        End Function
        Private Sub BindBenefitsBoPropertiesToGridHeader()
            BindBOPropertyToGridHeader(TheProductBenefits, EXPIRATION_DATE_PRODUCT_EQUIP_PROPERTY, ProductBenefitsGridView.Columns(GRID_COL_PE_EXPIRATION_IDX))
        End Sub

        Private Sub GridBenefits_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProductBenefitsGridView.RowDataBound
            Try
                'Dim foundrow() As DataRow
                'Dim dr As DataRow
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso State.ProductBenefitsSearchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then

                        CType(e.Row.Cells(BENEFIT_CELL_ID).FindControl(PROD_BENEFITS_ID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte()))

                        If (State.IsProductBenefitsEditMode = True AndAlso State.ProductBenefitsId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte())))) Then

                            Dim dropDownListMake As DropDownList = CType(e.Row.Cells(BENEFIT_CELL_MAKE).FindControl(PROD_BENEFITS_MAKE_DROPDOWNLIST), DropDownList)
                            'BindListControlToDataView(dropDownListMake, Equipment.GetEquipmentForBenefitsList(),, "EQUIPMENT_ID")

                            Dim oListContext As New ListContext
                            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                            Dim EquipmentList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="BenefitsEquipmentByCompanyGroup", context:=oListContext)
                            dropDownListMake.Populate(EquipmentList,
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .TextFunc = Function(x)
                                                    Return x.Translation + " / " + x.Code
                                                End Function
                                })

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EQUIPMENT_ID) IsNot DBNull.Value Then
                                Dim makeID As Guid = GuidControl.ByteArrayToGuid(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EQUIPMENT_ID))
                                If Not Guid.Empty.Equals(makeID) Then
                                    BindSelectItem(makeID.ToString, dropDownListMake)
                                End If
                            End If

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODIFIED_DATE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_CREATED_DATE).FindControl(PROD_BENEFITS_CREATEDDATE_LABEL), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODIFIED_DATE), Date))
                            End If

                            Dim txtProdBenefitsEffectiveDate As TextBox = CType(e.Row.Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_TEXTBOX), TextBox)
                            Dim txtProdBenefitsExpirationDate As TextBox = CType(e.Row.Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_TEXTBOX), TextBox)

                            If Not DBNull.Value.Equals(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE)) Then
                                txtProdBenefitsEffectiveDate.Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE), Date))
                            End If
                            If Not DBNull.Value.Equals(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EXPIRATION_DATE)) Then
                                txtProdBenefitsExpirationDate.Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EXPIRATION_DATE), Date))
                            End If

                            Dim btnStartDate As ImageButton = DirectCast(e.Row.Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_IMAGE_BUTTON), ImageButton)
                            Dim btnEndDate As ImageButton = DirectCast(e.Row.Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_IMAGE_BUTTON), ImageButton)

                            If txtProdBenefitsEffectiveDate IsNot Nothing Then
                                AddCalendar_New(btnStartDate, txtProdBenefitsEffectiveDate, , txtProdBenefitsEffectiveDate.Text)
                            End If
                            If txtProdBenefitsExpirationDate IsNot Nothing Then
                                AddCalendar_New(btnEndDate, txtProdBenefitsExpirationDate, , txtProdBenefitsExpirationDate.Text)
                            End If

                        Else

                            Dim dvRewardType As DataView = LookupListNew.GetProdRewardTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MAKE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_MAKE).FindControl(PROD_BENEFITS_MAKE_LABEL), Label).Text = CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MAKE), String)
                            End If
                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODEL) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_MODEL).FindControl(PROD_BENEFITS_MODEL_LABEL), Label).Text = CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODEL), String)
                            End If

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_START_DATE).FindControl(PROD_BENEFITS_EFFECTIVE_DATE_LABEL), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE), Date))
                            End If

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EXPIRATION_DATE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_END_DATE).FindControl(PROD_BENEFITS_EXPIRATION_DATE_LABEL), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EXPIRATION_DATE), Date))
                            End If

                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODIFIED_DATE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_CREATED_DATE).FindControl(PROD_BENEFITS_CREATEDDATE_LABEL), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_MODIFIED_DATE), Date))
                            ElseIf dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_CREATED_DATE) IsNot DBNull.Value Then
                                CType(e.Row.Cells(BENEFIT_CELL_CREATED_DATE).FindControl(PROD_BENEFITS_CREATEDDATE_LABEL), Label).Text = GetDateFormattedString(CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_CREATED_DATE), Date))
                            End If

                            Dim editDateImageButton As ImageButton = CType(e.Row.FindControl("EditButton_WRITE"), ImageButton)
                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE) IsNot DBNull.Value Then
                                If CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE), Date) > DateTime.Today Then
                                    editDateImageButton.Visible = True
                                Else
                                    editDateImageButton.Visible = False
                                End If
                            End If

                            Dim deleteDateImageButton As ImageButton = CType(e.Row.FindControl("DeleteButton_WRITE"), ImageButton)
                            If dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE) IsNot DBNull.Value Then
                                If CType(dvRow.Row(ProductEquipment.ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE), Date) > DateTime.Today Then
                                    deleteDateImageButton.Visible = True
                                Else
                                    deleteDateImageButton.Visible = False
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region ""

#End Region
#End Region
#Region "Device Types Tab"
        Public Const MerGridColDeviceType As Integer = 0
        Public Const MerGridColMer As Integer = 1

        Public Const DeviceTypeNone As Integer = 0
        Public Const DeviceTypeAdd As Integer = 1
        Public Const DeviceTypeEdit As Integer = 2
        Public Const DeviceTypeDelete As Integer = 3

        Private Sub GridViewDeviceTypesDetails_RowCommand(source As Object, e As GridViewCommandEventArgs) Handles GridViewDeviceTypesDetails.RowCommand
            Dim nIndex As Integer
            Dim guidTemp As Guid
            Try
                If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                    guidTemp = New Guid(e.CommandArgument.ToString)
                    State.ProductEquipmentWorkingItem = TheProductCode.GetDeviceTypesProdEquip(guidTemp)
                End If

                If e.CommandName = EDIT_COMMAND_NAME Then
                    'GridViewDeviceTypesDetails.EditIndex = nIndex
                    'GridViewDeviceTypesDetails.SelectedIndex = nIndex
                    State.DeviceTypeAction = DeviceTypeEdit
                    PopulateDeviceType(State.DeviceGroupCode)
                    EnableDisableForDeviceType(False)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    'GridViewDeviceTypesDetails.EditIndex = nIndex
                    'GridViewDeviceTypesDetails.SelectedIndex = nIndex
                    State.DeviceTypeAction = DeviceTypeDelete
                    DeleteRecordDeviceType()
                ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                    SaveRecordDeviceType()
                    EnableDisableForDeviceType(True)
                ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                    CancelRecordDeviceType()
                    EnableDisableForDeviceType(True)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub GridViewDeviceTypesDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewDeviceTypesDetails.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As ProductEquipment

                If e.Row.DataItem IsNot Nothing Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        dvRow = CType(e.Row.DataItem, ProductEquipment)
                        If Not dvRow.DeviceTypeId.Equals(Guid.Empty) Then
                            Dim lblDeviceType As Label
                            lblDeviceType = CType(e.Row.Cells(MerGridColDeviceType).FindControl("labelDeviceType"), Label)
                            lblDeviceType.Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetListItemId(dvRow.DeviceTypeId, Authentication.LangId), dvRow.DeviceTypeId)
                        End If

                        'edit item and set value
                        If (State.DeviceTypeAction = DeviceTypeAdd OrElse State.DeviceTypeAction = DeviceTypeEdit) _
                            AndAlso State.ProductEquipmentWorkingItem.Id = dvRow.Id Then
                            Dim ddlMethodOfRepair As DropDownList
                            ddlMethodOfRepair = CType(e.Row.Cells(MerGridColMer).FindControl("ddlMethodOfRepair"), DropDownList)
                            ddlMethodOfRepair.Items.Clear()

                            'ddlMethodOfRepair.PopulateOld("METHR", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

                            Dim MethodOfRpair As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="METHR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                            ddlMethodOfRepair.Populate(MethodOfRpair,
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .BlankItemValue = String.Empty,
                                    .ValueFunc = AddressOf .GetExtendedCode
                                })

                            BindSelectItem(dvRow.MethodOfRepairXcd, ddlMethodOfRepair)
                        Else
                            Dim lblMethodOfRepair As Label
                            lblMethodOfRepair = CType(e.Row.Cells(MerGridColMer).FindControl("labelMethodOfRepair"), Label)
                            lblMethodOfRepair.Text = LookupListNew.GetDescriptionFromExtCode("METHR", Authentication.LangId, dvRow.MethodOfRepairXcd)
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PopulateDeviceTypesDetailsGrid(ds As List(Of ProductEquipment))

            Dim blnEmptyList As Boolean = False, mySource As List(Of ProductEquipment)
            Dim nIndex As Integer = 0
            If ds Is Nothing OrElse ds.Count = 0 Then
                mySource = New List(Of ProductEquipment)
                mySource.Add(New ProductEquipment())
                blnEmptyList = True
                If (State.DeviceTypeAction = DeviceTypeAdd AndAlso State.ProductEquipmentWorkingItem IsNot Nothing) Then
                    GridViewDeviceTypesDetails.SelectedIndex = nIndex
                    GridViewDeviceTypesDetails.EditIndex = nIndex
                End If
                GridViewDeviceTypesDetails.DataSource = mySource
            Else
                If (State.DeviceTypeAction = DeviceTypeAdd OrElse State.DeviceTypeAction = DeviceTypeEdit) AndAlso (State.ProductEquipmentWorkingItem IsNot Nothing) Then
                    nIndex = ds.FindIndex(Function(r) r.Id = State.ProductEquipmentWorkingItem.Id)
                    GridViewDeviceTypesDetails.SelectedIndex = nIndex
                    GridViewDeviceTypesDetails.EditIndex = nIndex
                End If
                GridViewDeviceTypesDetails.DataSource = ds
            End If

            GridViewDeviceTypesDetails.DataBind()

            If blnEmptyList Then
                GridViewDeviceTypesDetails.Rows(0).Visible = False
            End If

            If Not State.DeviceTypesDetailsGridTranslated Then
                SetGridItemStyleColor(GridViewDeviceTypesDetails)
                TranslateGridHeader(GridViewDeviceTypesDetails)
                State.DeviceTypesDetailsGridTranslated = True
            End If
        End Sub

        Sub PopulateDeviceType(Optional ByVal strDeviceGroupCode As String = Nothing)

            With TheProductCode
                If Not .Id.Equals(Guid.Empty) Then
                    If strDeviceGroupCode Is Nothing Then
                        Dim deviceGroupDv As DataView = LookupListNew.GetDeviceGroupsLookupList(Authentication.LangId)

                        If cboListForDeviceGroup.SelectedItem IsNot Nothing Then
                            Dim idDeviceGroup = New Guid(cboListForDeviceGroup.SelectedValue.ToString())
                            strDeviceGroupCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEVICE_GROUPS, idDeviceGroup)
                            State.DeviceGroupCode = strDeviceGroupCode
                        Else
                            Return
                        End If
                    End If

                    PopulateAvailableDeviceType(strDeviceGroupCode)
                    PopulateDeviceTypesDetailsGrid(.GetDeviceTypesProdEquipList(strDeviceGroupCode))
                End If
            End With

        End Sub
        Sub PopulateAvailableDeviceType(deviceGroupCode As String)
            With TheProductCode
                If Not .Id.Equals(Guid.Empty) AndAlso deviceGroupCode IsNot Nothing Then
                    Dim availableDv As DataView = .GetAvailableDeviceTypes(deviceGroupCode)
                    ddlDeviceTypeAvailable.Items.Clear()
                    BindListControlToDataView(ddlDeviceTypeAvailable, availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
                Else
                    ddlDeviceTypeAvailable.Items.Clear()
                End If
            End With
        End Sub
        Private Sub btnAddDeviceType_Click(sender As System.Object, e As EventArgs) Handles btnAddDeviceType.Click
            Try
                If ddlDeviceTypeAvailable.SelectedItem Is Nothing Then Exit Sub

                State.DeviceTypeAction = DeviceTypeAdd
                State.ProductEquipmentWorkingItem = TheProductCode.AddDeviceTypesProdEquip(New Guid(ddlDeviceTypeAvailable.SelectedItem.Value))

                PopulateDeviceType(State.DeviceGroupCode)

                EnableDisableForDeviceType(False)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CancelRecordDeviceType()
            Try

                If State.DeviceTypeAction = DeviceTypeAdd Then
                    TheProductCode.RemoveDeviceTypesProdEquip(State.ProductEquipmentWorkingItem.DeviceTypeId)
                End If

                State.DeviceTypeAction = DeviceTypeNone
                State.ProductEquipmentWorkingItem = Nothing

                GridViewDeviceTypesDetails.SelectedIndex = -1
                GridViewDeviceTypesDetails.EditIndex = GridViewDeviceTypesDetails.SelectedIndex
                PopulateDeviceType(State.DeviceGroupCode)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecordDeviceType()
            Try
                Dim ddlMethodOfRepair As DropDownList
                ddlMethodOfRepair = CType(GridViewDeviceTypesDetails.Rows(GridViewDeviceTypesDetails.EditIndex).Cells(MerGridColMer).FindControl("ddlMethodOfRepair"), DropDownList)
                PopulateBOProperty(State.ProductEquipmentWorkingItem, "MethodOfRepairXcd", ddlMethodOfRepair.SelectedValue.ToString())

                If State.ProductEquipmentWorkingItem.IsDirty Then 'Save the changes
                    If State.IsProductCodeNew = False AndAlso State.ProductEquipmentWorkingItem.IsDirty Then 'existing contract, save to DB directly
                        TheProductCode.SaveDeviceTypesProdEquip(State.ProductEquipmentWorkingItem)
                    End If
                End If

                State.DeviceTypeAction = DeviceTypeNone
                GridViewDeviceTypesDetails.SelectedIndex = -1
                GridViewDeviceTypesDetails.EditIndex = GridViewDeviceTypesDetails.SelectedIndex
                PopulateDeviceType(State.DeviceGroupCode)
                State.ProductEquipmentWorkingItem = Nothing
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DeleteRecordDeviceType()
            TheProductCode.RemoveDeviceTypesProdEquip(State.ProductEquipmentWorkingItem.DeviceTypeId)
            State.DeviceTypeAction = DeviceTypeNone
            State.ProductEquipmentWorkingItem = Nothing
            GridViewDeviceTypesDetails.SelectedIndex = -1
            GridViewDeviceTypesDetails.EditIndex = GridViewDeviceTypesDetails.SelectedIndex
            PopulateDeviceType(State.DeviceGroupCode)
        End Sub
        Private Sub EnableDisableForDeviceType(blnFlag As Boolean)

            ControlMgr.SetEnableControl(Me, ddlDeviceTypeAvailable, blnFlag)
            ControlMgr.SetEnableControl(Me, btnAddDeviceType, blnFlag)

            SetGridControls(GridViewDeviceTypesDetails, blnFlag)

            ControlMgr.SetEnableControl(Me, btnNew_WRITE, blnFlag)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, blnFlag)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, blnFlag)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, blnFlag)
            ControlMgr.SetEnableControl(Me, btnBack, blnFlag)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, blnFlag)
        End Sub

        Protected Sub moBenefitsEligibleFlagDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs)

            If moBenefitsEligibleFlagDropDownList.SelectedItem IsNot Nothing Then
                PopulateBOsFromForm()
                If TheProductCode.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y Then
                    ShowHideBenefitsControls(True)
                Else
                    TheProductCode.BenefitEligibleActionXCD = String.Empty
                    ShowHideBenefitsControls(False)
                End If
            End If
        End Sub

        Sub ShowHideBenefitsControls(show As Boolean)
            ControlMgr.SetVisibleControl(Me, moBenefitsEligibleActionLabel, show)
            ControlMgr.SetVisibleControl(Me, moBenefitsEligibleActionDropDownList, show)
            If show Then
                If DisabledTabsList.Contains(tab_moProductBenefits) = True Then
                    DisabledTabsList.Remove(tab_moProductBenefits)
                End If
            Else
                If DisabledTabsList.Contains(tab_moProductBenefits) = False Then
                    DisabledTabsList.Add(tab_moProductBenefits)
                End If
            End If

        End Sub

#End Region

        Public Shared Sub ScrollTo(control As HtmlAnchor)
            Dim js = <script type='text/javascript'>
                        $(document).ready(function() {{ 
                            var element = document.getElementById('{0}'); 
                            element.scrollIntoView(); 
                            element.focus(); 
                        }}); 
                     </script>.ToString
            control.Page.RegisterClientScriptBlock("ScrollTo", String.Format(js, control.ClientID))
        End Sub
    End Class
End Namespace
