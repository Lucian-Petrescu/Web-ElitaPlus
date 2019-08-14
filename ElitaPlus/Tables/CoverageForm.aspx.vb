Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Tables

    Partial Class CoverageForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState

            Public moCoverageId As Guid = Guid.Empty
            Public IsCoverageNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public moCoverage As Coverage
            Public moCoverageRateList() As CoverageRate
            Public moCoverageDeductibleList() As CoverageDeductible
            Public moCoverageConseqDamageList() As CoverageConseqDamage
            Public selectedCoverageTypeId As Guid
            Public selectedProductItemId As Guid
            Public selectedItemId As Guid
            Public selectedOptionalId As Guid
            Public selectedIsClaimAllowedId As Guid
            Public selectedUseCoverageStartDateId As Guid
            Public selectedEffective As String
            Public selectedExpiration As String
            Public selectedLiability As String
            Public selectedLiabilityLimitPercent As String
            Public selectedCertificateDuration As String
            Public selectedCoverageDuration As String
            '            Public selectedOffsetMethodId As Guid
            Public selectedOffsetMethod As String
            Public selectedMarkupDistnPercent As String
            Public selectedOffset As String
            Public selectedOffsetDays As String
            Public selectedDeductible As String
            Public selectedCovDeductible As String
            Public selectedEarningCodeId As Guid
            Public selectedDeductiblePercent As String
            Public selectedRepairDiscountPct As String
            Public selectedReplacementDiscountPct As String
            Public selectedAgentCode As String
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public selectedCoverageLiabilityLimit As String
            Public selectedCoverageLiabilityLimitPercent As String
            Public selectedRecoverDeviceId As Guid
            Public selectedClaimLimitCount As String
            Public selectedPerIncidentLiabilityLimitCap As String
            Public selectedTaxTypeXCD As String
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
            Me.State.moCoverageId = CType(Me.CallingParameters, Guid)
            If Me.State.moCoverageId.Equals(Guid.Empty) Then
                Me.State.IsCoverageNew = True
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheCoverage)
                ClearAll()
                EnableCoverageButtons(False)
                PopulateAll()
                moLiabilityLimitPercentText.Text = "100"
                moCoverageLiabilityLimitText.Text = "0"
                moCoverageLiabilityLimitPercentText.Text = ""
                moPerIncidentLiabilityLimitCapText.Text = "0"
            Else
                Me.State.IsCoverageNew = False
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheCoverage)
                EnableCoverageButtons(True)
                PopulateAll()
            End If
            Me.LoadCurrencyOfCoverage()
            CoverageMarkupDistribution()
        End Sub

#End Region

#Region "Constants"

        Private Const UPDATE_COMMAND As String = "Update"
        Private Const NEW_COMMAND As String = "New"
        Private Const NO_COVERAGE_PRICING As String = "N"
        Private Const FIRST_POS As Integer = 0
        Private Const PRICE_MATRIX_ID As Integer = 0
        Private Const PRICE_MATRIX_CODE As Integer = 1
        Private Const COL_DESCRIPTION_NAME As String = "DESCRIPTION"
        Private Const COL_CODE_NAME As String = "CODE"
        Private Const COVERAGE_LIST As String = "CoverageSearchForm.aspx"
        Public Const URL As String = "CoverageForm.aspx"
        Private Const DEDUCTIBLE_BASED_ON_FIXED As String = "FIXED"
        Private Const COVERAGE_FORM001 As String = "COVERAGE_FORM001" ' Coverage List Exception
        Private Const COVERAGE_FORM002 As String = "COVERAGE_FORM002" ' Coverage Field Exception
        Private Const COVERAGE_FORM003 As String = "COVERAGE_FORM003" ' Coverage Update Exception
        Private Const COVERAGE_FORM004 As String = "COVERAGE_FORM004" ' Coverage-Rate List Exception
        Private Const COVERAGE_FORM005 As String = "COVERAGE_FORM005" ' Coverage-Rate Update Exception
        Private Const COVERAGE_FORM006 As String = "COVERAGE_FORM006" ' Coverage-Deductible Update Exception
        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Public Const COVERAGE_TYPE_ID_PROPERTY As String = "CoverageTypeId"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const OPTIONAL_ID_PROPERTY As String = "OptionalId"
        Public Const CLAIM_ALLOWED_ID_PROPERTY As String = "IsClaimAllowedId"
        Public Const PRODUCT_CODE_ID_PROPERTY As String = "ProductCodeId"
        Public Const PRODUCT_ITEM_ID_PROPERTY As String = "ProductItemId"
        Public Const RISK_TYPE_ID_PROPERTY As String = "RiskTypeId"
        Public Const CERTIFICATE_DURATION_PROPERTY As String = "CertificateDuration"
        Public Const COVERAGE_DURATION_PROPERTY As String = "CoverageDuration"
        Public Const DEDUCTIBLE_PROPERTY As String = "Deductible"
        Public Const EFECTIVE_PROPERTY As String = "Effective"
        Public Const EXPIRATION_PROPERTY As String = "Expiration"
        Public Const LIABILITY_LIMIT_PROPERTY As String = "LiabilityLimit"
        Public Const MARKUP_DISTRIBUTION_PERCENT_PROPERTY As String = "MarkupDistributionPercent"
        Public Const LIABILITY_LIMIT_PERCENT_PROPERTY As String = "LiabilityLimitPercent"
        Public Const DEDUCTIBLE_PERCENT_PROPERTY As String = "DeductiblePercent"
        Public Const OFFSET_TO_START_PROPERTY As String = "OffsetToStart"
        Public Const COL_COVERAGE_RATE_ID As String = "COVERAGE_RATE_ID"
        Public Const COL_LOW_PRICE As String = "LOW_PRICE"
        Public Const COL_HIGH_PRICE As String = "HIGH_PRICE"
        Public Const COL_GROSS_AMT As String = "GROSS_AMT"
        Public Const COL_COMMISSION_PERCENT As String = "COMMISSION_PERCENT"
        Public Const COL_MARKETING_PERCENT As String = "MARKETING_PERCENT"
        Public Const COL_ADMIN_EXPENSE As String = "ADMIN_EXPENSE"
        Public Const COL_PROFIT_EXPENSE As String = "PROFIT_EXPENSE"
        Public Const COL_LOSS_COST_PERCENT As String = "LOSS_COST_PERCENT"
        Public Const COL_GROSS_AMOUNT_PERCENT As String = "GROSS_AMOUNT_PERCENT"
        Public Const COL_RENEWAL_NUMBER As String = "RENEWAL_NUMBER"
        Public Const LABEL_SELECT_DEALERCODE As String = "Dealer"
        Public Const LABEL_REPAIR_DISCOUNT_PCT As String = "RepairDiscountPct"
        Public Const LABEL_REPLACEMENT_DISCOUNT_PCT As String = "ReplacementDiscountPct"
        Public Const USE_COVERAGE_START_DATE As String = "UseCoverageStartDateId"
        Public Const METHOD_OF_REPAIR_ID As String = "MethodOfRepairId"
        Public Const DEDUCTIBLE_BASED_ON_ID As String = "DeductibleBasedOnId"
        Public Const AGENT_CODE As String = "AgentCode"
        Public Const NO As String = "N"
        Public Const YES As String = "Y"
        Public Const LABEL_COVERAGE As String = "COVERAGE"
        Public Const LABEL_CVG_RATE As String = "COVERAGE RATE"
        Public Const PROD_LIAB_BASED_ON_NOT_APP As String = "NOTAPPL"
        Public Const COVERAGE_LIABILITY_LIMIT_PROPERTY As String = "CoverageLiabilityLimit"
        Public Const COVERAGE_LIABILITY_LIMIT_PERCENT_PROPERTY As String = "CoverageLiabilityLimitPercent"
        Public Const RECOVER_DEVICE_ID_PROPERTY As String = "RecoverDeviceId"
        Private Const ATTRIBUTE_VALUE_DROPDOWN_NAME As String = "AttributeValueDropDown"
        Private Const Is_REINSURED_PROPERTY As String = "IsReInsuredId"
        Private Const CLAIM_LIMIT_COUNT_PROPERTY As String = "CoverageClaimLimit"

        Public Const ConfigurationSuperUserRole As String = "CONSU"
        Public Const TAX_TYPE_XCD As String = "tax_type_xcd"

#End Region

#Region "Tabs"
        Public Const Tab_CoverageRate As String = "0"
        Public Const Tab_Deductible As String = "1"
        Public Const Tab_ATTRIBUTES As String = "2"
        Public Const Tab_CoverageConseqDamage As String = "3"

        Dim DisabledTabsList As New List(Of String)()
#End Region
#Region "Coverage-Rate Constants"

        ' DataGrid Elements
        '    Private Const EDIT_BUTTON_NAME As String = "BtnEdit"
        '  COVERAGE_RATE_ID, LOW_PRICE, HIGH_PRICE, GROSS_AMT, COMMISSIONS_PERCENT COMMISSION_PERCENT,
        '				MARKETING_PERCENT, ADMIN_EXPENSE, PROFIT_EXPENSE, LOSS_COST_PERCENT

        Private Const COVERAGE_RATE_ID As Integer = 2
        Private Const LOW_PRICE As Integer = 3
        Private Const HIGH_PRICE As Integer = 4
        Private Const GROSS_AMT As Integer = 5
        Private Const COMMISSIONS_PERCENT As Integer = 6
        Private Const MARKETING_PERCENT As Integer = 7
        Private Const ADMIN_EXPENSE As Integer = 8
        Private Const PROFIT_EXPENSE As Integer = 9
        Private Const LOSS_COST_PERCENT As Integer = 10
        Private Const GROSS_AMOUNT_PERCENT As Integer = 11
        Private Const RENEWAL_NUMBER As Integer = 12

        ' DataView Elements
        Private Const DBCOVERAGE_RATE_ID As Integer = 0

        ' Property Name
        Private Const LOW_PRICE_PROPERTY As String = "LowPrice"
        Private Const HIGH_PRICE_PROPERTY As String = "HighPrice"
        Private Const GROSS_AMT_PROPERTY As String = "GrossAmt"
        Private Const COMMISSIONS_PERCENT_PROPERTY As String = "CommissionsPercent"
        Private Const MARKETING_PERCENT_PROPERTY As String = "MarketingPercent"
        Private Const ADMIN_EXPENSE_PROPERTY As String = "AdminExpense"
        Private Const PROFIT_EXPENSE_PROPERTY As String = "ProfitExpense"
        Private Const LOSS_COST_PERCENT_PROPERTY As String = "LossCostPercent"
        Private Const GROSS_AMOUNT_PERCENT_PROPERTY As String = "GrossAmountPercent"
        Private Const RENEWAL_NUMBER_PROPERTY As String = "RenewalNumber"

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_CANCEL_DELETE As String = "ACTION_CANCEL_DELETE"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

#End Region

#Region "Page Return Type"

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Coverage
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Coverage, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Attributes"
        Private msCommand As String
        Private moCoverage As Coverage
        Private moCoverageRate As CoverageRate
        Private mbIsNewRate As Boolean
        Private moCoverageDeductible As CoverageDeductible
        Private moCoverageConseqDamage As CoverageConseqDamage
        Private _moDepreciationScdRelation As DepreciationScdRelation
#End Region

#Region "Properties"

        Private ReadOnly Property TheCoverage() As Coverage
            Get

                If Me.State.moCoverage Is Nothing Then
                    If Me.State.IsCoverageNew = True Then
                        ' For creating, inserting
                        Me.State.moCoverage = New Coverage
                        Me.State.moCoverageId = Me.State.moCoverage.Id
                    Else
                        ' For updating, deleting
                        Me.State.moCoverage = New Coverage(Me.State.moCoverageId)
                    End If
                End If

                Return Me.State.moCoverage
            End Get
        End Property

        Private ReadOnly Property CoveragePricingCode() As String
            Get
                Dim sCoveragePricingCode As String
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim oProductId As Guid = GetSelectedItem(moProductDrop)
                Dim oPriceMatrixView As DataView = LookupListNew.GetPriceMatrixLookupList(oProductId, oLanguageId)
                If oPriceMatrixView.Count > 0 Then
                    sCoveragePricingCode = oPriceMatrixView.Item(FIRST_POS).Item(COL_CODE_NAME).ToString
                End If
                Return sCoveragePricingCode
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
        Private ReadOnly Property TheDepreciationSchedule() As DepreciationScdRelation
            Get
                If _moDepreciationScdRelation Is Nothing Then
                    _moDepreciationScdRelation = TheCoverage.GetCoverageDepreciationScdChild()
                End If
                Return _moDepreciationScdRelation
            End Get
        End Property

#End Region

#Region "Coverage-Rate Properties"

        Private ReadOnly Property TheCoverageRate() As CoverageRate
            Get

                If moCoverageRate Is Nothing Then
                    If IsNewRate = True Then
                        ' For creating, inserting
                        moCoverageRate = New CoverageRate
                        CoverageRateId = moCoverageRate.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageRateId = "" Then
                            CoverageRateId = Guid.Empty.ToString
                        End If
                        moCoverageRate = New CoverageRate(GetGuidFromString(CoverageRateId))
                    End If
                End If

                Return moCoverageRate
            End Get

        End Property


        Private Property CoverageRateId() As String
            Get
                Return moCoverageRateIdLabel.Text
            End Get
            Set(ByVal Value As String)
                moCoverageRateIdLabel.Text = Value
            End Set
        End Property

        Private Property IsNewRate() As Boolean
            Get
                Return Convert.ToBoolean(moIsNewRateLabel.Text)
            End Get
            Set(ByVal Value As Boolean)
                moIsNewRateLabel.Text = Value.ToString
            End Set
        End Property


#End Region

#Region "Coverage Deductible Constants"

        'TABLE COLUMNS
        Public Const TABLE_KEY_NAME As String = "coverage_ded_id"
        Public Const COVERAGE_DED_ID_PROPERTY As String = "coverage_ded_id"
        Public Const COVERAGE_ID_PROPERTY As String = "coverage_id"
        Public Const METHOD_OF_REPAIR_ID_PROPERTY As String = "MethodOfRepairId"
        Public Const DEDUCTIBLE_BASED_ON_ID_PROPERTY As String = "DeductibleBasedOnId"
        Public Const cOV_DEDUCTIBLE_PROPERTY As String = "Deductible"
        Public Const METHOD_REPAIR_DESC_PROPERTY As String = "METHOD_OF_REPAIR"
        Public Const DEDUCTIBLE_BASED_ON_DESC_PROPERTY As String = "DEDUCTIBLE_BASED_ON"

        'Grid columns sequence
        Private Const COVERAGE_DED_ID As Integer = 2
        Private Const COV_METHOD_OF_REPAIR_ID As Integer = 3
        Private Const COV_DEDUCTIBLE_BASED_ON_id As Integer = 4
        Private Const METHOD_OF_REPAIR_DESC As Integer = 5
        Private Const DEDUCTIBLE_BASED_ON_DESC As Integer = 6
        Private Const DEDUCTIBLE As Integer = 7

        ' DataView Elements
        Private Const DBCOVERAGE_DED_ID As Integer = 0
        Private Const DBMETHOD_OF_REPAIR_ID As Integer = 2
        Private Const DBDEDUCTIBLE_BASED_ON As Integer = 3

        'name of dropdownlist
        Private Const DDL_DEDUCTIBLE_BASED_ON As String = "moddl_DeductibleBasedOn"
        Private Const DDL_METHOD_OF_REPAIR As String = "moddl_MethodOfRepair"
        Private Const TEXT_DEDUCTIBLE As String = "motxt_Deductible"

#End Region

#Region "coverage Deductible Properties"

        Private ReadOnly Property TheCoverageDeductible() As CoverageDeductible
            Get

                If moCoverageDeductible Is Nothing Then
                    If IsNewDeductible = True Then
                        ' For creating, inserting
                        moCoverageDeductible = New CoverageDeductible
                        CoverageDeductibleId = moCoverageDeductible.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageDeductibleId = "" Then
                            CoverageDeductibleId = Guid.Empty.ToString
                        End If
                        moCoverageDeductible = New CoverageDeductible(GetGuidFromString(CoverageDeductibleId))
                    End If
                End If

                Return moCoverageDeductible
            End Get

        End Property

        Private Property CoverageDeductibleId() As String
            Get
                Return moCoverageDeductibleIdLabel.Text
            End Get
            Set(ByVal Value As String)
                moCoverageDeductibleIdLabel.Text = Value
            End Set
        End Property

        Private Property IsNewDeductible() As Boolean
            Get
                Return Convert.ToBoolean(IsNewDeductibleLabel.Text)
            End Get
            Set(ByVal Value As Boolean)
                IsNewDeductibleLabel.Text = Value.ToString
            End Set
        End Property
#End Region
#Region "Coverage Conseq Damage Constants"

        'TABLE COLUMNS
        Public Const CONSEQ_DAMAGE_TYPE_PROPERTY As String = "ConseqDamageTypeXcd"
        Public Const LIABILILTY_LIMIT_BASED_ON_PROPERTY As String = "LiabilityLimitBaseXcd"
        Public Const LIABILILTY_LIMIT_PER_INCIDENT_PROPERTY As String = "LiabilityLimitPerIncident"
        Public Const LIABILILTY_LIMIT_CUMULATIVE_PROPERTY As String = "LiabilityLimitCumulative"
        Public Const CONSEQ_DAMAGE_EFFECTIVE_DATE_PROPERTY As String = "Effective"
        Public Const CONSEQ_DAMAGE_EXPIRATION_DATE_PROPERTY As String = "Expiration"
        Public Const FULFILMENT_METHOD_PROPERTY As String = "FulfilmentMethodXcd"


        'Grid columns sequence
        Private Const COVERAGE_CONSEQ_DAMAGE_ID As Integer = 2
        Private Const CONSEQ_DAMAGE_TYPE As Integer = 3
        Private Const LIABILITY_LIMIT_BASED_ON As Integer = 4
        Private Const LIABLILITY_LIMIT_PER_INCIDENT As Integer = 5
        Private Const LIABLILITY_LIMIT_CUMULATIVE As Integer = 6
        Private Const CONSEQ_DAMAGE_EFFECTIVE_DATE As Integer = 7
        Private Const CONSEQ_DAMAGE_EXPIRATION_DATE As Integer = 8
        Private Const FULFILMENT_METHOD As Integer = 9
        Private Const CONSEQ_DAMAGE_TYPE_Xcd As Integer = 10
        Private Const LIABILITY_LIMIT_BASED_ON_Xcd As Integer = 11
        Private Const FULFILMENT_METHOD_Xcd As Integer = 12

        ' DataView Elements
        Private Const DBCOVERAGE_CONSEQ_DAMAGE_ID As Integer = 0
        Private Const DBCOVERAGE_ID As Integer = 1
        Private Const DBLIABLILITY_LIMIT_PER_INCIDENT As Integer = 6
        Private Const DBLIABLILITY_LIMIT_CUMULATIVE As Integer = 7

        Private Const P_EFFECTIVE_DATE_TEXTBOX_NAME As String = "moConseqDamageEffectiveDateText"
        Private Const P_EFFECTIVE_DATE_IMAGEBUTTON_NAME As String = "btnConseqDamageEffectiveDate"
        Private Const P_EXPIRATION_DATE_TEXTBOX_NAME As String = "moConseqDamageExpirationDateText"
        Private Const P_EXPIRATION_DATE_IMAGEBUTTON_NAME As String = "btnConseqDamageExpirationDate"





#End Region
#Region "coverage Conseq Damage Properties"

        Private ReadOnly Property TheCoverageConseqDamage() As CoverageConseqDamage
            Get

                If moCoverageConseqDamage Is Nothing Then
                    If IsNewConseqDamage = True Then
                        ' For creating, inserting
                        moCoverageConseqDamage = New CoverageConseqDamage
                        CoverageConseqDamageId = moCoverageConseqDamage.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageConseqDamageId = "" Then
                            CoverageConseqDamageId = Guid.Empty.ToString
                        End If
                        moCoverageConseqDamage = New CoverageConseqDamage(GetGuidFromString(CoverageConseqDamageId))
                    End If
                End If

                Return moCoverageConseqDamage
            End Get

        End Property



        Private Property CoverageConseqDamageId() As String
            Get
                Return moCoverageConseqDamageIdLabel.Text
            End Get
            Set(ByVal Value As String)
                moCoverageConseqDamageIdLabel.Text = Value
            End Set
        End Property

        Private Property IsNewConseqDamage() As Boolean
            Get
                Return Convert.ToBoolean(moIsNewCoverageConseqDamageLabel.Text)
            End Get
            Set(ByVal Value As Boolean)
                moIsNewCoverageConseqDamageLabel.Text = Value.ToString
            End Set
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init, page events"

        'Protected WithEvents moErrorController As ErrorController
        'Protected WithEvents moErrorControllerRate As ErrorController
        'Protected WithEvents moErrorControllerConseqDamage As ErrorController

        Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents moRateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents BtnDeleteRate As System.Web.UI.WebControls.Button
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents moCoverageEditPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try

                'Preserve values during postbacks
                Me.PreserveValues()
                moMsgControllerRate.Clear()
                Me.MasterPage.MessageController.Clear_Hide()
                moMsgControllerDeductible.Clear()
                moMsgControllerConseqDamage.Clear()

                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LABEL_COVERAGE)
                Me.UpdateBreadCrum()
                ClearLabelsErrSign()
                ClearGridHeaders(moGridView)
                ClearGridHeaders(dedGridView)
                ClearGridHeaders(moGridViewConseqDamage)

                If Not Page.IsPostBack Then
                    Me.SetGridItemStyleColor(moGridView)
                    Me.SetGridItemStyleColor(dedGridView)
                    Me.SetGridItemStyleColor(moGridViewConseqDamage)
                    Me.TranslateGridHeader(Me.moGridView)
                    Me.TranslateGridHeader(Me.dedGridView)
                    Me.TranslateGridHeader(Me.moGridViewConseqDamage)
                    Me.TranslateGridControls(moGridView)
                    Me.TranslateGridControls(dedGridView)
                    Me.TranslateGridControls(moGridViewConseqDamage)

                    Me.SetStateProperties()
                    ' Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate, Me.moEffectiveText)
                    Me.AddCalendar(Me.BtnExpirationDate, Me.moExpirationText)
                    AttributeValues.ParentBusinessObject = CType(TheCoverage, IAttributable)
                    AttributeValues.TranslateHeaders()
                Else
                    AttributeValues.ParentBusinessObject = CType(TheCoverage, IAttributable)
                    GetDisabledTabs()
                End If

                AttributeValues.BindBoProperties()
                EnableTabsCoverageConseqDamage()
                BindBoPropertiesToLabels()
                'BindBoPropertiesToDeductibleGridHeader()
                CheckIfComingFromConfirm()
                Me.moUseCoverageStartDateLable.ForeColor = Me.moReplacementDiscountPrcLabel.ForeColor
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub GetDisabledTabs()
            Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(DisabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(LABEL_COVERAGE)
            End If
        End Sub

        Private Sub CoverageForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            If Me.State.IsNewWithCopy Then
                hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
                Exit Sub
            End If

            If State.moCoverage.Inuseflag = "Y" Then ' The coverage record is in use and should not allow changes except Configuration Super User Roles
                'Display a warning of this record is in use when opening the page first time
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.AddWarning("RECORD_IN_USE")
                End If

                If ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                    'diable the save button to prevent any change to the coverage record
                    Me.btnApply_WRITE.Enabled = False
                    Me.btnDelete_WRITE.Enabled = False
                End If
            End If
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region

#Region "Handlers-DropDown"
        Private Sub cboDeductibleBasedOn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDeductibleBasedOn.SelectedIndexChanged
            Try
                EnableDisableDeductible(GetSelectedItem(Me.cboDeductibleBasedOn), True)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDropControl.SelectedDropChanged
            Try
                ClearForDealer()
                TheCoverage.DealerId = TheDealerControl.SelectedGuid
                'PopulateDealer()
                PopulateDepreciationScheduleDropdown()
                If TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moProductDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moProductDrop.SelectedIndexChanged
            Try
                ClearForProduct()
                If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateRiskType()
                    EnableDisableCoverageLiabilityLimits()
                End If
                If Me.State.IsCoverageNew = True Then
                    TheCoverage.ClearAttributeValues()
                    Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    Dim ReInsStatusDataView As DataView = LookupListNew.GetReInsStatusesWithoutPartialStatuesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
                    'Dim oYesNoDataView As DataView = LookupListNew.GetYesNoLookupList(oLanguageId)
                    Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    Dim oProductCode As New ProductCode(New Guid(moProductDrop.SelectedValue))
                    If oProductCode.IsReInsuredId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                        'BindListControlToDataView(moReInsuredDrop, oYesNoDataView, , , True)
                        moReInsuredDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                        BindSelectItem(oProductCode.IsReInsuredId.ToString, Me.moReInsuredDrop)
                        Dim cav As IEnumerable(Of AttributeValue) = TheCoverage.AttributeValues
                        For Each av As AttributeValue In cav
                        Next

                        Dim attributevalueBo As AttributeValue = TheCoverage.AttributeValues.GetNewAttributeChild()
                        Dim productattributevalue As String = oProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS)

                        attributevalueBo.Value = LookupListNew.GetCodeFromId(ReInsStatusDataView, LookupListNew.GetIdFromCode(ReInsStatusDataView, productattributevalue))
                        attributevalueBo.AttributeId = TheCoverage.AttributeValues.Attribues.Where(Function(a) a.UiProgCode = Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS).FirstOrDefault().Id
                        Dim avl As New List(Of AttributeValue)
                        avl.Add(attributevalueBo)
                        Dim attrValues As IEnumerable(Of AttributeValue) = TheCoverage.AttributeValues
                        attrValues = avl
                        AttributeValues.PopulateAttributeValuesGrid(avl)
                        attributevalueBo.EndEdit()
                        attributevalueBo.Save()
                        attributevalueBo = Nothing
                        AttributeValues.Visible = True
                    Else
                        DisabledTabsList.Add(Tab_ATTRIBUTES)
                        AttributeValues.Visible = False
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moRiskDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moRiskDrop.SelectedIndexChanged
            Try
                ClearForRisk()
                If moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateCoverageType()
                End If
                PopulateItemNumber()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCoverageTypeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCoverageTypeDrop.SelectedIndexChanged
            Try
                ClearForCoverageType()
                If Me.moCoverageTypeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateRestCoverage()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PreserveValues()
            Me.State.selectedCoverageTypeId = GetSelectedItem(moCoverageTypeDrop)
            Me.State.selectedProductItemId = GetSelectedItem(moProductItemDrop)
            Me.State.selectedOptionalId = GetSelectedItem(moOptionalDrop)
            Me.State.selectedIsClaimAllowedId = GetSelectedItem(moIsClaimAllowedDrop)
            Me.State.selectedUseCoverageStartDateId = GetSelectedItem(UseCoverageStartDateId)
            Me.State.selectedOffsetMethod = GetSelectedValue(moOffsetMethodDrop)
            Me.State.selectedOffset = Me.moOffsetText.Text
            Me.State.selectedMarkupDistnPercent = Me.txtMarkupDistPercent.Text
            Me.State.selectedOffsetDays = Me.txtOffsetDays.Text
            Me.State.selectedEffective = Me.moEffectiveText.Text
            Me.State.selectedExpiration = Me.moExpirationText.Text
            Me.State.selectedCertificateDuration = Me.moCertificateDurationText.Text
            Me.State.selectedCoverageDuration = Me.moCoverageDurationText.Text
            Me.State.selectedLiability = Me.moLiabilityText.Text
            Me.State.selectedLiabilityLimitPercent = Me.moLiabilityLimitPercentText.Text
            Me.State.selectedDeductible = Me.moDeductibleText.Text
            Me.State.selectedDeductiblePercent = Me.moDeductiblePercentText.Text
            Me.State.selectedEarningCodeId = GetSelectedItem(moEarningCodeDrop)
            Me.State.selectedCovDeductible = Me.moCovDeductibleText.Text
            Me.State.selectedRepairDiscountPct = Me.moRepairDiscountPctText.Text
            Me.State.selectedReplacementDiscountPct = Me.moReplacementDiscountPctText.Text
            Me.State.selectedAgentCode = Me.moAgentcodeText.Text
            Me.State.selectedCoverageLiabilityLimit = Me.moCoverageLiabilityLimitText.Text
            Me.State.selectedCoverageLiabilityLimitPercent = Me.moCoverageLiabilityLimitPercentText.Text
            Me.State.selectedRecoverDeviceId = GetSelectedItem(moRecoverDeciveDrop)
            Me.State.selectedClaimLimitCount = Me.moClaimLimitCountText.Text
            Me.State.selectedPerIncidentLiabilityLimitCap = Me.moPerIncidentLiabilityLimitCapText.Text
            Me.State.selectedTaxTypeXCD = GetSelectedValue(moTaxTypeDrop)

        End Sub


#End Region

#Region "Handlers-TextBox"

        Private Sub moEffectiveText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moEffectiveText.TextChanged
            EnableDisableDeductible(GetSelectedItem(Me.cboDeductibleBasedOn), True)
        End Sub

        Private Sub EnableDisableDeductible(ByVal pDeductibleBasedOnId As Guid, ByVal pClearValues As Boolean)
            Dim sCoverageDeductibleCode As String
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If (TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX) And (Len(moEffectiveText.Text) > 0) Then
                Dim oDealerId As Guid = TheDealerControl.SelectedGuid
                Dim oEffectiveDate As String
                Dim tempdate As Date
                tempdate = DateHelper.GetDateValue(moEffectiveText.Text)
                oEffectiveDate = tempdate.ToString("yyyyMMdd")
                Try
                    Dim oCDView As DataView = TheCoverage.GetCoverageDeductable(oDealerId, oEffectiveDate, oLanguageId)
                    If oCDView.Count > 0 Then
                        moCovDeductibleText.Text = oCDView.Item(FIRST_POS).Item(COL_DESCRIPTION_NAME).ToString
                    Else
                        moCovDeductibleText.Text = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_YESNO, NO)
                    End If
                    sCoverageDeductibleCode = moCovDeductibleText.Text
                Catch ex As Exception
                    Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
                End Try
            Else
                sCoverageDeductibleCode = Nothing
            End If

            sCoverageDeductibleCode = LookupListNew.GetCodeFromDescription(LookupListNew.DropdownLookupList("YESNO", oLanguageId, True), sCoverageDeductibleCode)

            Select Case sCoverageDeductibleCode
                Case YES
                    ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, True)
                    Dim sDeductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, pDeductibleBasedOnId)

                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                        ' OrElse  (String.IsNullOrWhiteSpace(sDeductibleBasedOnCode))) Then
                        ControlMgr.SetEnableControl(Me, Me.moDeductibleText, True)
                        ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, False)
                        If (pClearValues) Then
                            Me.moDeductiblePercentText.Text = "0"
                        End If
                    ElseIf ((sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then
                        ControlMgr.SetEnableControl(Me, Me.moDeductibleText, False)
                        ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, True)
                        If (pClearValues) Then
                            Me.moDeductibleText.Text = "0"
                        End If

                    Else
                        ControlMgr.SetEnableControl(Me, Me.moDeductibleText, False)
                        ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, False)
                        ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, True)
                        If (pClearValues) Then
                            Me.moDeductibleText.Text = "0"
                            Me.moDeductiblePercentText.Text = "0"
                        End If

                    End If
                Case Else
                    ControlMgr.SetEnableControl(Me, Me.moDeductibleText, False)
                    ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, False)
                    ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, False)
                    If (pClearValues) Then
                        Me.moDeductibleText.Text = "0"
                        Me.moDeductiblePercentText.Text = "0"
                        ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                    End If
            End Select


            'If (sDeductibleBasedOnCode = DEDUCTIBLE_BASED_ON_FIXED) Then
            '            ControlMgr.SetEnableControl(Me, Me.moDeductibleText, True)
            '            ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, False)
            '            If (pClearValues) Then
            '                Me.moDeductiblePercentText.Text = "0"
            '            End If
            '        Else
            '            ControlMgr.SetEnableControl(Me, Me.moDeductibleText, False)
            '            ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, True)
            '            If (pClearValues) Then
            '                Me.moDeductibleText.Text = "0"
            '            End If
            '        End If
            '    Case Else
            '        ControlMgr.SetEnableControl(Me, Me.moDeductibleText, False)
            '        ControlMgr.SetEnableControl(Me, Me.moDeductiblePercentText, False)
            '        ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, False)
            '        'disable the deductible tab
            '        DeductibleTab.Enabled = False

            '        Me.moDeductibleText.Text = "0"
            '        Me.moDeductiblePercentText.Text = "0"
            '        SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
            'End Select
        End Sub
#End Region


#Region "Handlers-Buttons"

        Private Sub SaveChanges()
            If ApplyChanges() = True Then
                Me.State.boChanged = True
                ClearCoverageRate()
                ClearCoverageDeductible()
                ClearCoverageConseqDamage()
                If Me.State.IsCoverageNew = True Then
                    Me.State.IsCoverageNew = False
                End If
                If Me.State.IsNewWithCopy Then
                    Me.State.IsNewWithCopy = False
                End If
                PopulateAll()
            End If
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Dim sVal As String
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
                sVal = LookupListNew.GetCodeFromDescription(yesNoLkL, Me.moCovDeductibleText.Text)
                If sVal = YES Then
                    If IsNumeric(moDeductiblePercentText.Text) And IsNumeric(moDeductibleText.Text) Then
                        If CType(moDeductiblePercentText.Text, Decimal) > 0 And CType(moDeductibleText.Text, Decimal) > 0 Then
                            'display error
                            ElitaPlusPage.SetLabelError(Me.moDeductiblePercentLabel)
                            ElitaPlusPage.SetLabelError(Me.moDeductibleLabel)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_AMOUNT_ERR)
                        End If
                    End If
                End If

                If (moCoverageLiabilityLimitText.Enabled And moCoverageLiabilityLimitPercentText.Enabled _
                    And moCoverageLiabilityLimitText.Visible And moCoverageLiabilityLimitPercentText.Visible) Then
                    If String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                        ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                    ElseIf Not String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitText.Text) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT)
                        ElseIf CType(moCoverageLiabilityLimitText.Text, Decimal) < 0 Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT)
                        End If
                    ElseIf String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And Not String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_PERCENT)
                        ElseIf CType(moCoverageLiabilityLimitPercentText.Text, Decimal) = 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) < 0 Or
                                CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 100 Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_PERCENT)
                        End If
                    ElseIf Not String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And Not String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitText.Text) And Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf IsNumeric(moCoverageLiabilityLimitText.Text) And Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf Not IsNumeric(moCoverageLiabilityLimitText.Text) And IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf (CType(moCoverageLiabilityLimitPercentText.Text, Decimal) = 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) < 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 100) Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf CType(moCoverageLiabilityLimitText.Text, Decimal) > 0 And CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 0 Then
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitLabel)
                            ElitaPlusPage.SetLabelError(Me.moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        End If
                    End If
                End If

                'Per Incident Liability Limit

                If String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                    ElitaPlusPage.SetLabelError(Me.moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                ElseIf Not String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                    If Not IsNumeric(moPerIncidentLiabilityLimitCapText.Text) Then
                        ElitaPlusPage.SetLabelError(Me.moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) < 0 Then
                        ElitaPlusPage.SetLabelError(Me.moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    End If
                ElseIf (CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) <= 0) Then
                    ElitaPlusPage.SetLabelError(Me.moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) > 0 Then
                    ElitaPlusPage.SetLabelError(Me.moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                End If

                If Not GetSelectedItem(Me.moReInsuredDrop).Equals(Guid.Empty) Then

                    If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                        If TheCoverage.AttributeValues.Count = 0 Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_REQUIRED_WHEN_REINSURED_IS_SET_ERR)
                        End If
                        If TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_SHOULD_BE_SAVED_ERR)
                        End If
                    End If

                    If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) Then

                        If TheCoverage.AttributeValues.Count > 0 Then
                            Throw New GUIException(Message.INVALID_ATTRIBUTE, Assurant.ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_CANNOT_BE_SET_WHEN_REINSURED_IS_SET_TO_NO_ERR)
                        End If
                    End If
                Else
                    If TheCoverage.AttributeValues.Count > 0 Then
                        Throw New GUIException(Message.INVALID_ATTRIBUTE, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_SET_ATTRIBUTE_WITHOUT_REINSURED_FLAG)
                    End If
                End If
                ValidateCoverage()
                SaveChanges()
                SetLabelColor(Me.moDeductiblePercentLabel)
                SetLabelColor(Me.moDeductibleLabel)
                SetLabelColor(Me.moIsClaimAllowedLabel)
                SetLabelColor(Me.moAgentCodeLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                'Me.State.LastErrMsg = Me.moErrorController.Text
            End Try
        End Sub

        Private Sub GoBack()
            Dim retType As New CoverageSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                Me.State.moCoverageId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Function IsEditAllowed() As Boolean
            If State.moCoverage.Inuseflag = "Y" AndAlso ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                Return False
            Else
                Return True
            End If
        End Function
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                Me.State.IsUndo = True
                If Not Me.State.IsCoverageNew Then
                    Me.State.moCoverage = New Coverage(Me.State.moCoverageId)
                End If
                AttributeValues.ParentBusinessObject = CType(TheCoverage, IAttributable)
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            ClearForNew()
            ClearAll()
            Me.EnableCoverageButtons(False)
            Me.PopulateAll()
            ControlMgr.SetVisibleControl(Me, Me.currLabelDiv, False)
            ControlMgr.SetVisibleControl(Me, Me.currTextBoxDiv, False)
            moLiabilityLimitPercentText.Text = "100"
            moCoverageLiabilityLimitText.Text = "0"
            moPerIncidentLiabilityLimitCapText.Text = "0"

        End Sub

        Private Sub ClearForNew()
            Me.State.moCoverageId = Guid.Empty
            Me.State.IsCoverageNew = True
            Me.State.moCoverage = Nothing
            Me.State.selectedCoverageTypeId = Guid.Empty
            Me.State.selectedProductItemId = Guid.Empty
            Me.State.selectedItemId = Guid.Empty
            Me.State.selectedOptionalId = Guid.Empty
            Me.State.selectedIsClaimAllowedId = Guid.Empty
            Me.State.selectedUseCoverageStartDateId = Guid.Empty
            'Me.State.selectedOffsetMethodId = Guid.Empty
            Me.State.selectedOffsetMethod = Nothing
            Me.State.selectedOffset = Nothing
            Me.State.selectedMarkupDistnPercent = Nothing
            Me.State.selectedOffsetDays = Nothing
            Me.State.selectedEffective = Nothing
            Me.State.selectedExpiration = Nothing
            Me.State.selectedCertificateDuration = Nothing
            Me.State.selectedCoverageDuration = Nothing
            Me.State.selectedLiability = Nothing
            Me.State.selectedLiabilityLimitPercent = Nothing
            Me.State.selectedDeductible = Nothing
            Me.State.selectedDeductiblePercent = Nothing
            Me.State.selectedCovDeductible = Nothing
            Me.State.selectedEarningCodeId = Guid.Empty
            Me.State.selectedCoverageLiabilityLimit = Nothing
            Me.State.selectedCoverageLiabilityLimitPercent = Nothing
            Me.State.selectedRecoverDeviceId = Guid.Empty
            Me.State.selectedClaimLimitCount = Nothing
            Me.State.selectedTaxTypeXCD = Nothing
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        Me.btnApply_WRITE.Enabled = True
                        Me.btnDelete_WRITE.Enabled = True
                    End If
                    CreateNew()
                    EnableTabsCoverageConseqDamage()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            End Try
        End Sub

        Private Sub CreateNewCopy()

            'Clear Coverage BO
            Dim newObj As New Coverage
            Dim attrVals As New List(Of AttributeValue)
            Dim attributeValues As IEnumerable(Of AttributeValue) = newObj.AttributeValues
            If (attributeValues.Count() = 0 AndAlso Not String.IsNullOrEmpty(TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS))) Then
                For Each attrVal As AttributeValue In TheCoverage.AttributeValues
                    Dim attributevalueBo As AttributeValue = newObj.AttributeValues.GetNewAttributeChild()
                    attributevalueBo.Value = attrVal.Value
                    attributevalueBo.AttributeId = attrVal.AttributeId
                    attrVals.Add(attributevalueBo)
                    attributevalueBo.Save()
                Next
            End If
            attributeValues = attributeValues
            Me.State.moCoverageId = newObj.Id
            Me.State.moCoverage = newObj
            Me.State.IsCoverageNew = True
            Me.State.moCoverage.Inuseflag = "N"
            EnableUniqueFields()

            'ClearCoverageRate()
            Me.State.IsNewWithCopy = True
            SetGridControls(moGridView, True)

            'REQ
            SetGridControls(moGridViewConseqDamage, True)



            Me.EnableCoverageButtons(False)
            TheDealerControl.ChangeEnabledControlProperty(True)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, True)
            ControlMgr.SetEnableControl(Me, moProductDrop, True)
            ControlMgr.SetEnableControl(Me, moRiskDrop, True)
            ControlMgr.SetVisibleControl(Me, Me.currLabelDiv, False)
            ControlMgr.SetVisibleControl(Me, Me.currTextBoxDiv, False)
            SetLabelColor(Me.moAgentCodeLabel)
        End Sub

        Private Sub LoadCoverageRateList()

            If moGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oCoverageRate(moGridView.Rows.Count - 1) As CoverageRate

                For i = 0 To moGridView.Rows.Count - 1
                    oCoverageRate(i) = New CoverageRate
                    oCoverageRate(i).CoverageId = TheCoverage.Id
                    If moGridView.Rows(i).Cells(LOW_PRICE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), "LowPrice", CType(moGridView.Rows(i).Cells(LOW_PRICE).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), "LowPrice", CType(moGridView.Rows(i).Cells(LOW_PRICE).Controls(1), TextBox).Text)
                    End If
                    If moGridView.Rows(i).Cells(HIGH_PRICE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), "HighPrice", CType(moGridView.Rows(i).Cells(HIGH_PRICE).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), "HighPrice", CType(moGridView.Rows(i).Cells(HIGH_PRICE).Controls(1), TextBox).Text)
                    End If
                    If moGridView.Rows(i).Cells(GROSS_AMT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), "GrossAmt", CType(moGridView.Rows(i).Cells(GROSS_AMT).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), "GrossAmt", CType(moGridView.Rows(i).Cells(GROSS_AMT).Controls(1), TextBox).Text)
                    End If
                    Me.PopulateBOProperty(oCoverageRate(i), "GrossAmountPercent", GetAmountFormattedDoubleString("0"))
                    If moGridView.Rows(i).Cells(GROSS_AMOUNT_PERCENT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), "GrossAmountPercent", CType(moGridView.Rows(i).Cells(GROSS_AMOUNT_PERCENT).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), "GrossAmountPercent", CType(moGridView.Rows(i).Cells(GROSS_AMOUNT_PERCENT).Controls(1), TextBox).Text)
                    End If
                    If CoveragePricingCode = NO_COVERAGE_PRICING Then
                        Me.PopulateBOProperty(oCoverageRate(i), "CommissionsPercent", GetAmountFormattedDoubleString("0"))
                        Me.PopulateBOProperty(oCoverageRate(i), "MarketingPercent", GetAmountFormattedDoubleString("0"))
                        Me.PopulateBOProperty(oCoverageRate(i), "AdminExpense", GetAmountFormattedDoubleString("0"))
                        Me.PopulateBOProperty(oCoverageRate(i), "ProfitExpense", GetAmountFormattedDoubleString("0"))
                        Me.PopulateBOProperty(oCoverageRate(i), "LossCostPercent", GetAmountFormattedDoubleString("0"))
                    Else
                        If moGridView.Rows(i).Cells(COMMISSIONS_PERCENT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageRate(i), "CommissionsPercent", CType(moGridView.Rows(i).Cells(COMMISSIONS_PERCENT).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageRate(i), "CommissionsPercent", CType(moGridView.Rows(i).Cells(COMMISSIONS_PERCENT).Controls(1), TextBox).Text)
                        End If
                        If moGridView.Rows(i).Cells(MARKETING_PERCENT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageRate(i), "MarketingPercent", CType(moGridView.Rows(i).Cells(MARKETING_PERCENT).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageRate(i), "MarketingPercent", CType(moGridView.Rows(i).Cells(MARKETING_PERCENT).Controls(1), TextBox).Text)
                        End If
                        If moGridView.Rows(i).Cells(ADMIN_EXPENSE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageRate(i), "AdminExpense", CType(moGridView.Rows(i).Cells(ADMIN_EXPENSE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageRate(i), "AdminExpense", CType(moGridView.Rows(i).Cells(ADMIN_EXPENSE).Controls(1), TextBox).Text)
                        End If
                        If moGridView.Rows(i).Cells(PROFIT_EXPENSE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageRate(i), "ProfitExpense", CType(moGridView.Rows(i).Cells(PROFIT_EXPENSE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageRate(i), "ProfitExpense", CType(moGridView.Rows(i).Cells(PROFIT_EXPENSE).Controls(1), TextBox).Text)
                        End If
                        If moGridView.Rows(i).Cells(LOSS_COST_PERCENT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageRate(i), "LossCostPercent", CType(moGridView.Rows(i).Cells(LOSS_COST_PERCENT).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageRate(i), "LossCostPercent", CType(moGridView.Rows(i).Cells(LOSS_COST_PERCENT).Controls(1), TextBox).Text)
                        End If
                    End If
                    If moGridView.Rows(i).Cells(RENEWAL_NUMBER).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), "RenewalNumber", CType(moGridView.Rows(i).Cells(RENEWAL_NUMBER).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), "RenewalNumber", CType(moGridView.Rows(i).Cells(RENEWAL_NUMBER).Controls(1), TextBox).Text)
                    End If

                Next
                Me.State.moCoverageRateList = oCoverageRate
            End If
        End Sub

        Public Function SaveCoverageRateList() As Boolean
            Dim i As Integer = 0
            Try
                If Me.State.IsNewWithCopy = True And Not Me.State.moCoverageRateList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To Me.State.moCoverageRateList.Length - 1
                        Me.State.moCoverageRateList(i).CoverageId = TheCoverage.Id
                        Me.State.moCoverageRateList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    Me.State.moCoverageRateList(j).delete()
                    Me.State.moCoverageRateList(j).Save()
                Next
                Me.HandleErrors(ex, moMsgControllerRate)
                Return False
            End Try
            Return True
        End Function

        Public Function SaveCoverageDeductibleList() As Boolean
            Dim i As Integer = 0
            Try
                If Me.State.IsNewWithCopy = True And Not Me.State.moCoverageDeductibleList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To Me.State.moCoverageDeductibleList.Length - 1
                        Me.State.moCoverageDeductibleList(i).CoverageId = TheCoverage.Id
                        Me.State.moCoverageDeductibleList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    Me.State.moCoverageDeductibleList(j).Delete()
                    Me.State.moCoverageDeductibleList(j).Save()
                Next
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Return False
            End Try
            Return True
        End Function
        Public Function SaveCoverageConseqDamageList() As Boolean
            Dim i As Integer = 0
            Try
                If Me.State.IsNewWithCopy = True And Not Me.State.moCoverageConseqDamageList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To Me.State.moCoverageConseqDamageList.Length - 1
                        Me.State.moCoverageConseqDamageList(i).CoverageId = TheCoverage.Id
                        Me.State.moCoverageConseqDamageList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    Me.State.moCoverageConseqDamageList(j).Delete()
                    Me.State.moCoverageConseqDamageList(j).Save()
                Next
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Return False
            End Try
            Return True
        End Function

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        Me.btnApply_WRITE.Enabled = True
                        Me.btnDelete_WRITE.Enabled = True
                    End If

                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteCoverage() = True Then
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(TheCoverage, COVERAGE_TYPE_ID_PROPERTY, moCoverageTypeLabel)
            Me.BindBOPropertyToLabel(TheCoverage, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(TheCoverage, OPTIONAL_ID_PROPERTY, moOptionalLabel)
            Me.BindBOPropertyToLabel(TheCoverage, PRODUCT_CODE_ID_PROPERTY, moProductLabel)
            Me.BindBOPropertyToLabel(TheCoverage, PRODUCT_ITEM_ID_PROPERTY, moProductItemLabel)
            Me.BindBOPropertyToLabel(TheCoverage, RISK_TYPE_ID_PROPERTY, moRiskLabel)
            Me.BindBOPropertyToLabel(TheCoverage, CERTIFICATE_DURATION_PROPERTY, moCertificateDurationLabel)
            Me.BindBOPropertyToLabel(TheCoverage, COVERAGE_DURATION_PROPERTY, moCoverageDurationLabel)
            Me.BindBOPropertyToLabel(TheCoverage, DEDUCTIBLE_PROPERTY, moDeductibleLabel)
            Me.BindBOPropertyToLabel(TheCoverage, EFECTIVE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(TheCoverage, EXPIRATION_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(TheCoverage, LIABILITY_LIMIT_PROPERTY, moLiabilityLabel)
            Me.BindBOPropertyToLabel(TheCoverage, MARKUP_DISTRIBUTION_PERCENT_PROPERTY, lblMarkupDistPercent)
            Me.BindBOPropertyToLabel(TheCoverage, LIABILITY_LIMIT_PERCENT_PROPERTY, moLiabilityLimitPercentLabel)
            Me.BindBOPropertyToLabel(TheCoverage, OFFSET_TO_START_PROPERTY, moOffsetLabel)
            Me.BindBOPropertyToLabel(TheCoverage, DEDUCTIBLE_PERCENT_PROPERTY, moDeductiblePercentLabel)
            Me.BindBOPropertyToLabel(TheCoverage, LABEL_REPAIR_DISCOUNT_PCT, moRepairDiscountPctLabel)
            Me.BindBOPropertyToLabel(TheCoverage, LABEL_REPLACEMENT_DISCOUNT_PCT, moReplacementDiscountPrcLabel)
            Me.BindBOPropertyToLabel(TheCoverage, LABEL_REPLACEMENT_DISCOUNT_PCT, moReplacementDiscountPrcLabel)
            Me.BindBOPropertyToLabel(TheCoverage, CLAIM_ALLOWED_ID_PROPERTY, moIsClaimAllowedLabel)
            Me.BindBOPropertyToLabel(TheCoverage, USE_COVERAGE_START_DATE, moUseCoverageStartDateLable)
            Me.BindBOPropertyToLabel(TheCoverage, METHOD_OF_REPAIR_ID, moMethodOfRepairLabel)
            Me.BindBOPropertyToLabel(TheCoverage, DEDUCTIBLE_BASED_ON_ID, moDeductibleBasedOnLabel)
            Me.BindBOPropertyToLabel(TheCoverage, Is_REINSURED_PROPERTY, moReInsuredLabel)
            Me.BindBOPropertyToLabel(TheCoverage, CLAIM_LIMIT_COUNT_PROPERTY, moClaimLimitCountLabel)
            'Def-26342: Added following condition to display mandatory notation for Agent code if RequiresAgentCodeId at company level is set to yes.

            If Not TheDealerControl.SelectedGuid.Equals(System.Guid.Empty) Then
                Dim objCompany = New Company()
                Dim ds As DataSet = objCompany.GetCompanyAgentFlagForDealer(TheDealerControl.SelectedGuid)

                If (Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0) Then
                    Dim RequiresAgentCodeId As Guid = New Guid(CType(ds.Tables(0).Rows(0)(0), Byte()))
                    Dim mandatory As String = "<span class=""mandatory"">*&nbsp;</span>"
                    If (RequiresAgentCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                        If Not moAgentCodeLabel.Text.Contains("*") Then
                            moAgentCodeLabel.Text = mandatory & moAgentCodeLabel.Text
                        End If
                    Else
                        If moAgentCodeLabel.Text.Contains("*") Then
                            moAgentCodeLabel.Text = moAgentCodeLabel.Text.Replace(mandatory, String.Empty)
                        End If
                    End If
                End If

            End If
            'Def-26342 - End

            Me.BindBOPropertyToLabel(TheCoverage, AGENT_CODE, moAgentCodeLabel)
            Me.BindBOPropertyToLabel(TheCoverage, COVERAGE_LIABILITY_LIMIT_PROPERTY, moCoverageLiabilityLimitLabel)
            Me.BindBOPropertyToLabel(TheCoverage, COVERAGE_LIABILITY_LIMIT_PERCENT_PROPERTY, moCoverageLiabilityLimitPercentLabel)
            Me.BindBOPropertyToLabel(TheCoverage, RECOVER_DEVICE_ID_PROPERTY, moRecoverDeciveLabel)

            Me.BindBOPropertyToLabel(TheCoverage, "OffsetToStartDays", moOffsetLabel)
            Me.BindBOPropertyToLabel(TheCoverage, "OffsetMethodId", moOffsetMethodLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(moCoverageTypeLabel)
            Me.ClearLabelErrSign(TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(moOptionalLabel)
            Me.ClearLabelErrSign(moProductLabel)
            Me.ClearLabelErrSign(moProductItemLabel)
            Me.ClearLabelErrSign(moRiskLabel)
            Me.ClearLabelErrSign(moCertificateDurationLabel)
            Me.ClearLabelErrSign(moCoverageDurationLabel)
            Me.ClearLabelErrSign(moDeductibleLabel)
            Me.ClearLabelErrSign(moEffectiveLabel)
            Me.ClearLabelErrSign(moExpirationLabel)
            Me.ClearLabelErrSign(moLiabilityLabel)
            Me.ClearLabelErrSign(moLiabilityLimitPercentLabel)
            Me.ClearLabelErrSign(moOffsetLabel)
            Me.ClearLabelErrSign(moOffsetMethodLabel)
            Me.ClearLabelErrSign(moMethodOfRepairLabel)
            Me.ClearLabelErrSign(moDeductibleBasedOnLabel)
            Me.ClearLabelErrSign(moCoverageLiabilityLimitLabel)
            Me.ClearLabelErrSign(moCoverageLiabilityLimitPercentLabel)
            Me.ClearLabelErrSign(moRecoverDeciveLabel)
            Me.ClearLabelErrSign(moAgentCodeLabel)
            Me.ClearLabelErrSign(moClaimLimitCountLabel)

        End Sub

        Private Sub BindBoPropertiesToGridHeader()
            Me.BindBOPropertyToGridHeader(TheCoverageRate, LOW_PRICE_PROPERTY, moGridView.Columns(LOW_PRICE))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, HIGH_PRICE_PROPERTY, moGridView.Columns(HIGH_PRICE))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, GROSS_AMT_PROPERTY, moGridView.Columns(GROSS_AMT))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, COMMISSIONS_PERCENT_PROPERTY, moGridView.Columns(COMMISSIONS_PERCENT))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, MARKETING_PERCENT_PROPERTY, moGridView.Columns(MARKETING_PERCENT))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, ADMIN_EXPENSE_PROPERTY, moGridView.Columns(ADMIN_EXPENSE))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, PROFIT_EXPENSE_PROPERTY, moGridView.Columns(PROFIT_EXPENSE))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, LOSS_COST_PERCENT_PROPERTY, moGridView.Columns(LOSS_COST_PERCENT))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, GROSS_AMOUNT_PERCENT_PROPERTY, moGridView.Columns(GROSS_AMOUNT_PERCENT))
            Me.BindBOPropertyToGridHeader(TheCoverageRate, RENEWAL_NUMBER_PROPERTY, moGridView.Columns(RENEWAL_NUMBER))
        End Sub
        Private Sub BindBoPropertiesToDeductibleGridHeader()
            Me.BindBOPropertyToGridHeader(TheCoverageDeductible, COVERAGE_DED_ID_PROPERTY, dedGridView.Columns(COVERAGE_DED_ID))
            Me.BindBOPropertyToGridHeader(TheCoverageDeductible, METHOD_OF_REPAIR_ID_PROPERTY, dedGridView.Columns(METHOD_OF_REPAIR_DESC))
            Me.BindBOPropertyToGridHeader(TheCoverageDeductible, DEDUCTIBLE_BASED_ON_ID_PROPERTY, dedGridView.Columns(DEDUCTIBLE_BASED_ON_DESC))
            Me.BindBOPropertyToGridHeader(TheCoverageDeductible, cOV_DEDUCTIBLE_PROPERTY, dedGridView.Columns(DEDUCTIBLE))
        End Sub

        Private Sub BindBoPropertiesToConseqDamageGridHeader()
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, CONSEQ_DAMAGE_TYPE_PROPERTY, moGridViewConseqDamage.Columns(CONSEQ_DAMAGE_TYPE))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, LIABILILTY_LIMIT_BASED_ON_PROPERTY, moGridViewConseqDamage.Columns(LIABILITY_LIMIT_BASED_ON))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, LIABILILTY_LIMIT_PER_INCIDENT_PROPERTY, moGridViewConseqDamage.Columns(LIABLILITY_LIMIT_PER_INCIDENT))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, LIABILILTY_LIMIT_CUMULATIVE_PROPERTY, moGridViewConseqDamage.Columns(LIABLILITY_LIMIT_CUMULATIVE))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, CONSEQ_DAMAGE_EFFECTIVE_DATE_PROPERTY, moGridViewConseqDamage.Columns(CONSEQ_DAMAGE_EFFECTIVE_DATE))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, CONSEQ_DAMAGE_EXPIRATION_DATE_PROPERTY, moGridViewConseqDamage.Columns(CONSEQ_DAMAGE_EXPIRATION_DATE))
            Me.BindBOPropertyToGridHeader(TheCoverageConseqDamage, FULFILMENT_METHOD_PROPERTY, moGridViewConseqDamage.Columns(FULFILMENT_METHOD))
        End Sub
        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub


#End Region

#End Region

#Region "Handlers-CoverageRate"

#Region "Handlers-CoverageRate-DataGrid"

        ' Coverage-Rate DataGrid
        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moGridView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moGridView.PageIndexChanging
            Try
                ResetIndexes()
                moGridView.PageIndex = e.NewPageIndex
                PopulateCoverageRateList(ACTION_CANCEL_DELETE)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'The pencil was clicked
        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    'nIndex = e.Item.ItemIndex
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    '      EnableForEditRateButtons(True)
                    PopulateCoverageRateList(ACTION_EDIT)
                    PopulateCoverageRate()
                    SetGridControls(moGridView, False)
                    Me.SetFocusInGrid(moGridView, nIndex, LOW_PRICE)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                    setbuttons(False)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    'nIndex = e.Item.ItemIndex
                    CoverageRateId = Me.GetGridText(moGridView, nIndex, COVERAGE_RATE_ID)
                    If DeleteSelectedCoverageRate(nIndex) = True Then
                        PopulateCoverageRateList(ACTION_CANCEL_DELETE)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ResetIndexes()
            moGridView.EditIndex = NO_ITEM_SELECTED_INDEX
            moGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Sub

#End Region

#Region "Handlers-CoverageRate-Buttons"

        Private Sub setbuttons(ByVal enable As Boolean)
            ControlMgr.SetEnableControl(Me, btnBack, enable)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate, enable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate, enable)
        End Sub
        Private Sub SaveRateChanges()
            If ApplyRateChanges() = True Then
                If IsNewRate = True Then
                    IsNewRate = False
                End If
                PopulateCoverageRateList(ACTION_SAVE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
            End If
        End Sub

        Private Sub BtnSaveRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveRate_WRITE.Click
            Try
                SaveRateChanges()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                IsNewRate = False
                EnableForEditRateButtons(False)
                PopulateCoverageRateList(ACTION_CANCEL_DELETE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                IsNewRate = True
                CoverageRateId = Guid.Empty.ToString
                PopulateCoverageRateList(ACTION_NEW)
                SetGridControls(moGridView, False)
                Me.SetFocusInGrid(moGridView, moGridView.SelectedIndex, LOW_PRICE)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                setbuttons(False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

#End Region

#End Region

#Region "Handlers Coverage Deductible"

#Region "Coverage Deductible Button management"
        Private Sub EnableCoverageDeductible(ByVal bIsReadWrite As Boolean)
            dedGridView.Columns(METHOD_OF_REPAIR_DESC).Visible = bIsReadWrite
            dedGridView.Columns(DEDUCTIBLE_BASED_ON_DESC).Visible = bIsReadWrite
            dedGridView.Columns(DEDUCTIBLE).Visible = bIsReadWrite
        End Sub

        Private Sub manageDeductibleButtons(ByVal newButton As Boolean, ByVal SaveCancelButton As Boolean)
            btnnew_Deductible.Enabled = newButton

            btnSave_Deductible.Enabled = SaveCancelButton
            btnCancel_Deductible.Enabled = SaveCancelButton

        End Sub

        Private Sub BtnSave_Deductible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_Deductible.Click
            Try
                SaveDeductibleChanges()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)

            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub BtnCancel_Deductible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel_Deductible.Click
            'Pencil button in not in edit mode
            Try
                IsNewDeductible = False
                manageDeductibleButtons(True, False)
                PopulateCoverageDeductibleList(ACTION_CANCEL_DELETE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub BtnNew_Deductible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew_Deductible.Click
            Try
                IsNewDeductible = True
                CoverageDeductibleId = Guid.Empty.ToString
                PopulateCoverageDeductibleList(ACTION_NEW)
                SetGridControls(dedGridView, False)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                If btnnew_Deductible.Enabled = False Then
                    setbuttons(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub SaveDeductibleChanges()
            If ApplyDeductibleChanges() = True Then
                If IsNewDeductible = True Then
                    IsNewDeductible = False
                End If
                PopulateCoverageDeductibleList(ACTION_SAVE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
            End If
        End Sub

        Private Function ApplyDeductibleChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If dedGridView.EditIndex < 0 Then Return False ' Coverage deductible is not in edit mode
            If Me.State.IsNewWithCopy Then
                Me.LoadCoverageDeductibleList()
                Me.State.moCoverageDeductibleList(dedGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewDeductible = False Then
                CoverageDeductibleId = Me.GetSelectedGridText(dedGridView, COVERAGE_DED_ID)
            End If
            BindBoPropertiesToDeductibleGridHeader()
            With TheCoverageDeductible
                PopulateDeductibleBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                manageDeductibleButtons(True, False)

            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    Me.MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
#End Region

#Region "Coverage Deductible Grid"
        ' Coverage-Rate DataGrid
        Public Sub DeductibleItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'The pencil was clicked
        Protected Sub DeductibleItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    dedGridView.EditIndex = nIndex
                    dedGridView.SelectedIndex = nIndex
                    FillDropDownList(dedGridView.Rows(dedGridView.SelectedIndex))
                    PopulateCoverageDeductibleList(ACTION_EDIT)
                    PopulateCoverageDeductible()
                    SetGridControls(dedGridView, False)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                    setbuttons(False)


                    Dim motxt_Deductible As TextBox = DirectCast(dedGridView.Rows()(nIndex).FindControl("motxt_Deductible"), TextBox)
                    Dim dr As DataRow = DirectCast(dedGridView.DataSource, DataView).Table.Rows(nIndex)
                    Dim moddl_DeductibleBasedOn As DropDownList = DirectCast(dedGridView.Rows()(nIndex).FindControl("moddl_DeductibleBasedOn"), DropDownList)

                    Dim dedid As Guid = GetSelectedItem((moddl_DeductibleBasedOn))
                    Dim deductiblecode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, dedid)


                    If Not ((deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                            (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__FIXED)) Then


                        motxt_Deductible.Text = "0"
                        motxt_Deductible.Enabled = False
                    End If


                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    CoverageDeductibleId = Me.GetGridText(dedGridView, nIndex, COVERAGE_DED_ID)
                    If DeleteSelectedCoverageDeductible(nIndex) = True Then
                        PopulateCoverageDeductibleList(ACTION_CANCEL_DELETE)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadCoverageDeductibleList()

            If dedGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oCoveragedeductible(dedGridView.Rows.Count - 1) As CoverageDeductible

                For i = 0 To dedGridView.Rows.Count - 1
                    oCoveragedeductible(i) = New CoverageDeductible
                    oCoveragedeductible(i).CoverageId = TheCoverage.Id
                    With dedGridView.Rows(i)
                        If dedGridView.Rows(i).Cells(DEDUCTIBLE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoveragedeductible(i), "Deductible", CType(dedGridView.Rows(i).Cells(DEDUCTIBLE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoveragedeductible(i), "Deductible", CType(dedGridView.Rows(i).Cells(DEDUCTIBLE).Controls(1), TextBox).Text)
                        End If
                        PopulateBOProperty(oCoveragedeductible(i), "MethodOfRepairId", GetGuidFromString(CType(dedGridView.Rows(i).Cells(COV_METHOD_OF_REPAIR_ID).Controls(1), Label).Text))
                        PopulateBOProperty(oCoveragedeductible(i), "DeductibleBasedOnId", GetGuidFromString(CType(dedGridView.Rows(i).Cells(COV_DEDUCTIBLE_BASED_ON_id).Controls(1), Label).Text))
                    End With

                Next
                Me.State.moCoverageDeductibleList = oCoveragedeductible
            End If
        End Sub
        Private Sub dedGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dedGridView.RowCreated
            Try
                FillDropDownList(e.Row)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub FillDropDownList(ByVal dtRow As GridViewRow)

            Dim ddlMethod_of_repair As DropDownList = DirectCast(dtRow.FindControl(DDL_METHOD_OF_REPAIR), DropDownList)
            Dim ddldedBasedon As DropDownList = DirectCast(dtRow.FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList)
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            If Not ddlMethod_of_repair Is Nothing Then
                'BindListControlToDataView(ddlMethod_of_repair, LookupListNew.GetMethodOfRepairLookupList(oLanguageId), , , True)
                ddlMethod_of_repair.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            End If
            If Not ddldedBasedon Is Nothing Then
                'BindListControlToDataView(ddldedBasedon, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(oLanguageId), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                ddldedBasedon.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })
            End If

        End Sub
#End Region

#Region "Populate Deductible"
        'Populate Deductible starts here

        Private Sub PopulateCoverageDeductibleList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oCoverageDeductible As CoverageDeductible
            Dim oDataView As DataView

            If Me.State.IsCoverageNew = True And Not Me.State.IsNewWithCopy Then Return ' We can not have CoverageRates if the coverage is new

            Try

                EnableCoverageDeductible(True)

                If Me.State.IsNewWithCopy Then
                    oDataView = oCoverageDeductible.GetList(Guid.Empty, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If Not oAction = ACTION_CANCEL_DELETE Then Me.LoadCoverageDeductibleList()
                    If Not Me.State.moCoverageRateList Is Nothing Then
                        oDataView = getDVFromArray(Me.State.moCoverageDeductibleList, oDataView.Table)
                    End If
                Else
                    oDataView = oCoverageDeductible.GetList(TheCoverage.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If

                Select Case oAction
                    Case ACTION_NONE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, dedGridView, 0)
                        manageDeductibleButtons(True, False)
                    Case ACTION_SAVE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex)
                        manageDeductibleButtons(True, False)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, dedGridView,
                                    dedGridView.PageIndex)
                        manageDeductibleButtons(True, False)
                    Case ACTION_EDIT
                        If Me.State.IsNewWithCopy Then
                            CoverageDeductibleId = Me.State.moCoverageRateList(dedGridView.SelectedIndex).Id.ToString
                        Else
                            CoverageDeductibleId = Me.GetSelectedGridText(dedGridView, COVERAGE_DED_ID)
                        End If
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex, True)
                        manageDeductibleButtons(False, True)
                    Case ACTION_NEW
                        If Me.State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DBCOVERAGE_DED_ID) = TheCoverageDeductible.Id.ToByteArray
                        oRow(DBMETHOD_OF_REPAIR_ID) = Guid.Empty.ToByteArray
                        oRow(DBDEDUCTIBLE_BASED_ON) = Guid.Empty.ToByteArray
                        oDataView.Table.Rows.Add(oRow)
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex, True)

                        manageDeductibleButtons(False, True)

                End Select

                dedGridView.DataSource = oDataView
                dedGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, dedGridView)

            Catch ex As Exception
                moMsgControllerDeductible.AddError(COVERAGE_FORM006)
                moMsgControllerDeductible.AddError(ex.Message, False)
                'moMsgControllerDeductible.Show()
            End Try
        End Sub

        Private Sub ModifyGridHeaderForDeductible()
            dedGridView.Columns(METHOD_OF_REPAIR_DESC).HeaderText = dedGridView.Columns(METHOD_OF_REPAIR_DESC).HeaderText.Replace("%", "<br>%")
            dedGridView.Columns(DEDUCTIBLE_BASED_ON_DESC).HeaderText = dedGridView.Columns(DEDUCTIBLE_BASED_ON_DESC).HeaderText.Replace("%", "<br>%")
            dedGridView.Columns(DEDUCTIBLE).HeaderText = dedGridView.Columns(DEDUCTIBLE).HeaderText.Replace("%", "<br>%")
        End Sub

        Private Sub PopulateCoverageDeductible()
            Dim sDeductibleBasedOnCode As String



            If Me.State.IsNewWithCopy Then
                With Me.State.moCoverageDeductibleList(moGridView.SelectedIndex)
                    SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(METHOD_OF_REPAIR_DESC).FindControl(DDL_METHOD_OF_REPAIR), DropDownList), .MethodOfRepairId)

                    sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)

                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION) Then
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), .DeductibleExpressionId)
                        ControlMgr.SetEnableControl(Me, CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(TEXT_DEDUCTIBLE), TextBox), False)
                    Else
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), .DeductibleBasedOnId)
                        Me.SetSelectedGridText(dedGridView, DEDUCTIBLE, .Deductible.ToString)
                    End If
                End With
            Else
                With TheCoverageDeductible

                    SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(METHOD_OF_REPAIR_DESC).FindControl(DDL_METHOD_OF_REPAIR), DropDownList), .MethodOfRepairId)

                    sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)
                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION) Then
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), .DeductibleExpressionId)
                        ControlMgr.SetEnableControl(Me, CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(TEXT_DEDUCTIBLE), TextBox), False)
                    Else
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), .DeductibleBasedOnId)
                        Me.SetSelectedGridText(dedGridView, DEDUCTIBLE, .Deductible.ToString)
                    End If

                    'SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), deductiblegridBasedOnId)
                    'Me.SetSelectedGridText(dedGridView, DEDUCTIBLE, .Deductible.ToString)
                End With
            End If

        End Sub
#End Region

#Region "Coverage Conseq Damage Grid"

        'Public Sub PerilItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        '    Try
        '        BaseItemCreated(sender, e)
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        'The pencil was clicked
        Public Sub ConseqDamageRowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Dim btnConseqDamageEffectiveDate As ImageButton
            Dim moConseqDamageEffectiveDateText As TextBox
            Dim btnConseqDamageExpirationDate As ImageButton
            Dim moConseqDamageExpirationDateText As TextBox
            Dim oGridViewrow As GridViewRow

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    Dim nIndex As Integer = CInt(e.CommandArgument)
                    nIndex = CInt(e.CommandArgument)
                    moGridViewConseqDamage.EditIndex = nIndex
                    moGridViewConseqDamage.SelectedIndex = nIndex

                    FillConseqDamageDropDownList(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex))
                    PopulateCoverageConseqDamageList(ACTION_EDIT)
                    PopulateCoverageConseqDamage()
                    SetGridControls(moGridViewConseqDamage, False)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                    setbuttons(False)

                    'Date Calendars
                    oGridViewrow = moGridViewConseqDamage.Rows(nIndex)
                    btnConseqDamageEffectiveDate = CType(oGridViewrow.FindControl(P_EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
                    moConseqDamageEffectiveDateText = CType(oGridViewrow.FindControl(P_EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
                    Me.AddCalendar_New(btnConseqDamageEffectiveDate, moConseqDamageEffectiveDateText)

                    btnConseqDamageExpirationDate = CType(oGridViewrow.FindControl(P_EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
                    moConseqDamageExpirationDateText = CType(oGridViewrow.FindControl(P_EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                    Me.AddCalendar_New(btnConseqDamageExpirationDate, moConseqDamageExpirationDateText)

                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    Dim nIndex As Integer = CInt(e.CommandArgument)
                    CoverageConseqDamageId = Me.GetGridText(moGridViewConseqDamage, nIndex, COVERAGE_CONSEQ_DAMAGE_ID)
                    If DeleteSelectedCoverageConseqDamage(nIndex) = True Then
                        PopulateCoverageConseqDamageList(ACTION_CANCEL_DELETE)
                    End If
                End If


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadCoverageConseqDamageList()

            If moGridViewConseqDamage.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oCoverageConseqDamage(moGridViewConseqDamage.Rows.Count - 1) As CoverageConseqDamage

                For i = 0 To moGridViewConseqDamage.Rows.Count - 1
                    oCoverageConseqDamage(i) = New CoverageConseqDamage
                    oCoverageConseqDamage(i).CoverageId = TheCoverage.Id
                    With moGridViewConseqDamage.Rows(i)
                        If moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_PER_INCIDENT).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_PER_INCIDENT_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_PER_INCIDENT).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_PER_INCIDENT_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_PER_INCIDENT).Controls(1), TextBox).Text)
                        End If

                        If moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_CUMULATIVE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_CUMULATIVE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_CUMULATIVE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_CUMULATIVE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABLILITY_LIMIT_CUMULATIVE).Controls(1), TextBox).Text)
                        End If

                        If moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EFFECTIVE_DATE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_EFFECTIVE_DATE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EFFECTIVE_DATE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_EFFECTIVE_DATE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EFFECTIVE_DATE).Controls(1), TextBox).Text)
                        End If

                        If moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EXPIRATION_DATE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_EXPIRATION_DATE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EXPIRATION_DATE).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_EXPIRATION_DATE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_EXPIRATION_DATE).Controls(1), TextBox).Text)
                        End If

                        If moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_TYPE).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_TYPE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_TYPE_Xcd).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), CONSEQ_DAMAGE_TYPE_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(CONSEQ_DAMAGE_TYPE).Controls(1), DropDownList).SelectedValue)
                        End If

                        If moGridViewConseqDamage.Rows(i).Cells(LIABILITY_LIMIT_BASED_ON).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_BASED_ON_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABILITY_LIMIT_BASED_ON_Xcd).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), LIABILILTY_LIMIT_BASED_ON_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(LIABILITY_LIMIT_BASED_ON).Controls(1), DropDownList).SelectedValue)
                        End If


                        If moGridViewConseqDamage.Rows(i).Cells(FULFILMENT_METHOD).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), FULFILMENT_METHOD_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(FULFILMENT_METHOD_Xcd).Controls(1), Label).Text)
                        Else
                            Me.PopulateBOProperty(oCoverageConseqDamage(i), FULFILMENT_METHOD_PROPERTY, CType(moGridViewConseqDamage.Rows(i).Cells(FULFILMENT_METHOD).Controls(1), DropDownList).SelectedValue)
                        End If
                    End With

                Next
                Me.State.moCoverageConseqDamageList = oCoverageConseqDamage
            End If
        End Sub
        Private Sub ConseqDamageGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridViewConseqDamage.RowCreated
            Try
                FillConseqDamageDropDownList(e.Row)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ConseqDamageGridView_RowDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridViewConseqDamage.RowDataBound
            Try
                Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
                If (e.Row.RowType = DataControlRowType.DataRow) Then

                    Dim EditButton_WRITE As ImageButton = DirectCast(e.Row.FindControl("EditButton_WRITE"), ImageButton)
                    Dim DeleteButton_WRITE As ImageButton = DirectCast(e.Row.FindControl("DeleteButton_WRITE"), ImageButton)
                    Dim moConseqDamageEffectiveDateLabel As Label = DirectCast(e.Row.FindControl("moConseqDamageEffectiveDateLabel"), Label)

                    If Not moConseqDamageEffectiveDateLabel Is Nothing Then
                        If Not String.IsNullOrEmpty(moConseqDamageEffectiveDateLabel.Text) Then
                            If (DateTime.Now >= Convert.ToDateTime(moConseqDamageEffectiveDateLabel.Text)) Then
                                ControlMgr.SetVisibleControl(Me, EditButton_WRITE, False)
                                ControlMgr.SetVisibleControl(Me, DeleteButton_WRITE, False)
                            End If
                        End If
                    End If

                    Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)
                    If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                        Dim effectiveDateTextBox As TextBox = CType(e.Row.FindControl(P_EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
                        Dim effectiveDateImageButton As ImageButton = CType(e.Row.FindControl(P_EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
                        Dim expirationDateTextBox As TextBox = CType(e.Row.FindControl(P_EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                        Dim expirationDateImageButton As ImageButton = CType(e.Row.FindControl(P_EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)

                        Me.AddCalendar_New(effectiveDateImageButton, effectiveDateTextBox)
                        Me.AddCalendar_New(expirationDateImageButton, expirationDateTextBox)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try


        End Sub

        Private Sub FillConseqDamageDropDownList(ByVal dtRow As GridViewRow)
            Dim moConseqDamageTypeDropdown As DropDownList = DirectCast(dtRow.FindControl("moConseqDamageTypeDropdown"), DropDownList)
            Dim moLiabilityLimitBasedOnDropdown As DropDownList = DirectCast(dtRow.FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList)
            Dim moFulfilmentMethodDropdown As DropDownList = DirectCast(dtRow.FindControl("moFulfilmentMethodDropdown"), DropDownList)

            If Not moConseqDamageTypeDropdown Is Nothing Then
                'moConseqDamageTypeDropdown.PopulateOld("PERILTYP", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim DamageTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PERILTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=New ListContext())
                moConseqDamageTypeDropdown.Populate(DamageTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf .GetDescription,
                        .ValueFunc = AddressOf .GetExtendedCode
                    })
            End If
            If Not moLiabilityLimitBasedOnDropdown Is Nothing Then
                'moLiabilityLimitBasedOnDropdown.PopulateOld("PRODLILIMBASEDON", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim LiabilityLimitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PRODLILIMBASEDON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moLiabilityLimitBasedOnDropdown.Populate(LiabilityLimitList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf .GetDescription,
                        .ValueFunc = AddressOf .GetExtendedCode
                    })
                BindSelectItem(Codes.COVERAGE_CONSEQ_DAMAGE_LIABILITY_LIMIT_BASED_ON_NOTAPPL, moLiabilityLimitBasedOnDropdown)
            End If
            If Not moFulfilmentMethodDropdown Is Nothing Then
                'moFulfilmentMethodDropdown.PopulateOld("FULFILMETH", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim FulfilmentMethodList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="FULFILMETH", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moFulfilmentMethodDropdown.Populate(FulfilmentMethodList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf .GetDescription,
                        .ValueFunc = AddressOf .GetExtendedCode
                    })
                BindSelectItem(Codes.COVERAGE_CONSEQ_DAMAGE_FULFILMENT_METHOD_REIMB, moFulfilmentMethodDropdown)
            End If
        End Sub
#End Region
#End Region

#Region "Button Management"

        Private Sub EnableEffective(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moEffectiveText, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate, bIsEnable)
        End Sub

        Private Sub EnableExpiration(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moExpirationText, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate, bIsEnable)
        End Sub

        Private Sub EnableUniqueFields()
            EnableEffective(True)
            EnableExpiration(True)
        End Sub

        Private Sub EnableCoverageButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsReadWrite)
        End Sub

        Private Sub enableDisableFields()
            If Me.State.IsCoverageNew Then
                Me.moCertificateDurationText.Enabled = True
                Me.moCoverageDurationText.Enabled = True
                Me.moCoverageTypeDrop.Enabled = True
            Else
                Me.moCertificateDurationText.Enabled = False
                Me.moCoverageDurationText.Enabled = False
                Me.moCoverageTypeDrop.Enabled = False
            End If
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearCoverageRate()
            If Not Me.State.IsNewWithCopy Then
                EnableRateButtons(False)
                moGridView.DataBind()
            End If
        End Sub

        Private Sub ClearCoverageDeductible()
            If Not Me.State.IsNewWithCopy Then
                manageDeductibleButtons(True, False)

                dedGridView.DataBind()
            End If
        End Sub

        Private Sub ClearTexts()
            If Not Me.State.IsNewWithCopy Then
                moCertificateDurationText.Text = Nothing
                moCoverageDurationText.Text = Nothing
                moLiabilityText.Text = Nothing
                moLiabilityLimitPercentText.Text = Nothing
                moDeductibleText.Text = Nothing
                moOffsetText.Text = Nothing
                txtMarkupDistPercent.Text = Nothing
                txtOffsetDays.Text = Nothing
                moEffectiveText.Text = Nothing
                moExpirationText.Text = Nothing
                moCoveragePricingText.Text = Nothing
                moRetailText.Text = Nothing
                moCovDeductibleText.Text = Nothing
                moDeductiblePercentText.Text = Nothing
                moRepairDiscountPctText.Text = Nothing
                moReplacementDiscountPctText.Text = Nothing
                moCoverageLiabilityLimitText.Text = Nothing
                moCoverageLiabilityLimitPercentText.Text = Nothing
                moPerIncidentLiabilityLimitCapText.Text = Nothing
            End If
        End Sub

        Private Sub ClearForCoverageType()
            ClearTexts()
            ClearList(moOptionalDrop)
            ClearList(moIsClaimAllowedDrop)
            ClearList(moProductItemDrop)
            ClearCoverageRate()
            ClearCoverageDeductible()
            ClearCoverageConseqDamage()
        End Sub

        Private Sub ClearForRisk()
            moItemNumberText.Text = Nothing
            ClearForCoverageType()
            ClearList(moCoverageTypeDrop)
        End Sub

        Private Sub ClearForProduct()
            ClearForRisk()
            ClearList(moRiskDrop)
            ClearList(moReInsuredDrop)
        End Sub

        Private Sub ClearForDealer()
            ClearList(moMethodOfRepairDrop)
            ClearForProduct()
            ClearList(moProductDrop)
        End Sub

        Private Sub ClearAll()
            ClearForDealer()
            TheDealerControl.ClearMultipleDrop()
            ClearList(UseCoverageStartDateId)
            EnableUniqueFields()
            SetLabelColor(Me.moAgentCodeLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateAll()
            EnableRateButtons(False)
            EnableConseqDamageButtons(False)
            PopulateDealer()
        End Sub

        Private Sub PopulateRestCoverage()
            PopulateProductItem()
            PopulateEarningCode()
            PopulateOptional()
            PopulateClaimAllowed()
            PopulateDropdown()
            PopulateCoveragePricing()
            PopulateRetail()
            PopulateOffsetMethod()
            PopulateTexts()
            IsNewRate = False
            IsNewDeductible = False
            IsNewConseqDamage = False
            PopulateCoverageRateList()
            PopulateCoverageDeductibleList()
            PopulateCoverageConseqDamageList()
            EnableUniqueFields()
            PopulateTaxTypeCode()
        End Sub

        Private Sub PopulateDealer()

            Try
                Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.SetControl(True,
                                            TheDealerControl.MODES.NEW_MODE,
                                            True,
                                            oDealerview,
                                            "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                            True, True,
,
                                            "multipleDropControl_moMultipleColumnDrop",
                                            "multipleDropControl_moMultipleColumnDropDesc",
                                            "multipleDropControl_lb_DropDown",
                                            False,
                                            0)

                If Me.State.IsCoverageNew = True Then
                    TheDealerControl.NothingSelected = True
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.SelectedGuid = TheCoverage.DealerId
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    PopulateProductCode()
                End If


            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateDepreciationScheduleDropdown()

            Dim objDealer = New Dealer(TheDealerControl.SelectedGuid)


            If Not objDealer Is Nothing And Not TheDepreciationSchedule.IsDeleted Then


                Dim oListContext As New ListContext
                oListContext.CompanyId = objDealer.CompanyId
                Dim depreciationScheduleListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DepreciationScheduleByCompany", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                Dim filteredDepreciationScheduleListForCompany As DataElements.ListItem() = (From lst In depreciationScheduleListForCompany
                                                                                             Where lst.ExtendedCode = "YESNO-Y" Or lst.Code = TheDepreciationSchedule.DepreciationScheduleCode
                                                                                             Select lst).ToArray()
                ddlDepSchCashReimbursement.Populate(filteredDepreciationScheduleListForCompany, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .TextFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })
            End If
        End Sub
        Private Sub PopulateProductCode()
            If TheDealerControl.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oDealerId As Guid = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim oYesNoDataView As DataView = LookupListNew.GetYesNoLookupList(oLanguageId)
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Try
                'BindListControlToDataView(moProductDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE")
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                moProductDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

                'BindListControlToDataView(moReInsuredDrop, oYesNoDataView, , , True)
                moReInsuredDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If Me.State.IsCoverageNew = True Then
                    BindSelectItem(Nothing, moProductDrop)
                    ControlMgr.SetEnableControl(Me, moProductDrop, True)

                    BindSelectItem(Nothing, moReInsuredDrop)
                    DisabledTabsList.Add(Tab_ATTRIBUTES)
                    AttributeValues.Visible = False
                Else
                    BindSelectItem(TheCoverage.ProductCodeId.ToString, moProductDrop)
                    ControlMgr.SetEnableControl(Me, moProductDrop, False)

                    Dim oProductCode As New ProductCode(TheCoverage.ProductCodeId)

                    BindSelectItem(TheCoverage.IsReInsuredId.ToString, moReInsuredDrop)

                    If TheCoverage.IsReInsuredId.Equals(Guid.Empty) Or LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) = TheCoverage.IsReInsuredId Then
                        DisabledTabsList.Add(Tab_ATTRIBUTES)
                        AttributeValues.Visible = False
                    Else
                        AttributeValues.Visible = True
                    End If

                End If

                If (AttributeValues.ParentBusinessObject Is Nothing) Then
                    AttributeValues.ParentBusinessObject = CType(TheCoverage, IAttributable)
                    AttributeValues.TranslateHeaders()
                End If
                AttributeValues.DataBind()
                PopulateRiskType()
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateRiskType()
            If moProductDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                'BindListControlToDataView(moRiskDrop, LookupListNew.GetRiskProductCodeLookupList(oProductId))
                Dim listcontext As ListContext = New ListContext()
                listcontext.ProductCodeId = oProductId
                moRiskDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ItemRiskTypeByProduct", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

                If Me.State.IsCoverageNew = True Then
                    BindSelectItem(Nothing, moRiskDrop)
                    ControlMgr.SetEnableControl(Me, moRiskDrop, True)
                Else
                    BindSelectItem(TheCoverage.RiskTypeId.ToString, moRiskDrop)
                    ControlMgr.SetEnableControl(Me, moRiskDrop, False)
                End If
                PopulateCoverageType()
                PopulateItemNumber()
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub
        Private Sub EnableDisableCoverageLiabilityLimits()
            If moProductDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Dim oproductLiabilityLimitBaseId As Guid = TheCoverage.GetProductLiabilityLimitBase(oProductId)
            Try
                Dim notAppicableId = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)
                If (oproductLiabilityLimitBaseId.Equals(Guid.Empty) Or
                    oproductLiabilityLimitBaseId = notAppicableId) Then
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitText, False)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentLabel, False)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentText, False)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitCountText, False)
                    moCoverageLiabilityLimitPercentText.Text = String.Empty
                Else
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitText, True)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentLabel, True)
                    ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentText, True)
                    ControlMgr.SetVisibleControl(Me, moClaimLimitCountText, True)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateCoverageType()
            If moRiskDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            'Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'Dim oCoverageTypeView As DataView = LookupListNew.GetItemRiskTypeLookupList(oProductId)
                'BindListControlToDataView(moCoverageTypeDrop, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(oLanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False), , , True)

                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                moCoverageTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

                If IsPostBack And Not Me.State.IsUndo Then
                    BindSelectItem(Me.State.selectedCoverageTypeId.ToString, moCoverageTypeDrop)
                    Me.State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.CoverageTypeId.ToString, moCoverageTypeDrop)
                End If
                PopulateRestCoverage()
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateItemNumber()
            Dim oCoverageTypeView As DataView
            If Me.moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                oCoverageTypeView = LookupListNew.GetItemRiskTypeLookupList(GetSelectedItem(moProductDrop))
                If oCoverageTypeView.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To oCoverageTypeView.Count - 1
                        If oCoverageTypeView.Item(i).Item(COL_DESCRIPTION_NAME).ToString = moRiskDrop.SelectedItem.Text Then
                            moItemNumberText.Text = oCoverageTypeView.Item(i).Item(COL_CODE_NAME).ToString
                            Me.State.selectedItemId = New Guid(CType(oCoverageTypeView.Item(i).Item("id"), Byte()))
                            Exit For
                        End If
                    Next
                Else
                    moItemNumberText.Text = Nothing
                End If
            End If

        End Sub

        Private Sub PopulateEarningCode()
            Dim oCompanyGroup As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Try
                'BindListControlToDataView(moEarningCodeDrop, LookupListNew.GetEarningCodeLookupList(oCompanyGroup), "CODE", , True)

                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                moEarningCodeDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="EarningCodesByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

                If IsPostBack And Not Me.State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(Me.State.selectedEarningCodeId.ToString, moEarningCodeDrop)
                    Me.State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.EarningCodeId.ToString, moEarningCodeDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateTaxTypeCode()
            Try

                moTaxTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                    .AddBlankItem = True,
                    .ValueFunc = AddressOf .GetExtendedCode
                })

                If IsPostBack And Not Me.State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(Me.State.selectedTaxTypeXCD.ToString, moTaxTypeDrop)
                    Me.State.IsUndo = False
                Else

                    If TheCoverage.TaxTypeXCD Is Nothing Then
                        BindSelectItem(String.Empty, moTaxTypeDrop)
                    Else

                        BindSelectItem(TheCoverage.TaxTypeXCD.ToString, moTaxTypeDrop)
                    End If
                End If

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COVERAGE_FORM002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateProductItem()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'BindListControlToDataView(moProductItemDrop, LookupListNew.GetProductItemLookupList(oLanguageId), , , True)
                Dim oProductItemList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PRODI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moProductItemDrop.Populate(oProductItemList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If IsPostBack And Not Me.State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(Me.State.selectedProductItemId.ToString, moProductItemDrop)
                    Me.State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.ProductItemId.ToString, moProductItemDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateOffsetMethod()
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'BindListTextToDataView(moOffsetMethodDrop, LookupListNew.DropdownLookupList("COVERAGE_OFFSET_METHOD", oLanguageId), , "Code", True)
                moOffsetMethodDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="COVERAGE_OFFSET_METHOD", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf .GetCode
                                                  })

                If IsPostBack And Not Me.State.IsUndo Then
                    'Def-26342: Added condition to check null value for selectedOffsetMethod
                    If Not Me.State.selectedOffsetMethod Is Nothing Then
                        BindSelectItem(Me.State.selectedOffsetMethod.ToString, moOffsetMethodDrop)
                        Me.State.IsUndo = False
                    End If

                Else
                    BindSelectItem(TheCoverage.OffsetMethod.ToString, moOffsetMethodDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateOptional()
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'BindListControlToDataView(moOptionalDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moOptionalDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                If IsPostBack And Not Me.State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(Me.State.selectedOptionalId.ToString, moOptionalDrop)
                    Me.State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.OptionalId.ToString, moOptionalDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateClaimAllowed()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'Dim i As Integer
                Dim oYes_String As String
                'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", oLanguageId, True)
                Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                'BindListControlToDataView(Me.moIsClaimAllowedDrop, yesNoLkL)
                Me.moIsClaimAllowedDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If Me.State.IsCoverageNew = True Then
                    oYes_String = (From lst In oYesNoList
                                   Where lst.Code = "Y"
                                   Select lst.Translation).FirstOrDefault()
                    'For i = 0 To yesNoLkL.Count - 1
                    '    If CType(yesNoLkL(i)("Code"), String) = YES Then
                    '        oYes_String = CType(yesNoLkL(i)("Description"), String)
                    '        Exit For
                    '    End If
                    'Next

                    Me.moIsClaimAllowedDrop.Items.FindByText(oYes_String).Selected = True
                Else
                    If IsPostBack And Not Me.State.IsUndo Then
                        BindSelectItem(Me.State.selectedIsClaimAllowedId.ToString, moIsClaimAllowedDrop)
                        Me.State.IsUndo = False
                    Else
                        BindSelectItem(TheCoverage.IsClaimAllowedId.ToString, moIsClaimAllowedDrop)
                    End If
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateDropdown()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(oLanguageId), "N")
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", oLanguageId)
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim noId As Guid = (From lst In oYesNoList
                                Where lst.Code = "N"
                                Select lst.ListItemId).FirstOrDefault()
            'BindListControlToDataView(Me.UseCoverageStartDateId, yesNoLkL, , , False)
            Me.UseCoverageStartDateId.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False
                                                   })

            'BindListControlToDataView(Me.moRecoverDeciveDrop, yesNoLkL, , , False)
            Me.moRecoverDeciveDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False
                                                   })

            If Me.State.IsCoverageNew Then
                SetSelectedItem(Me.UseCoverageStartDateId, noId)
                SetSelectedItem(Me.moRecoverDeciveDrop, noId)
            Else
                SetSelectedItem(Me.UseCoverageStartDateId, TheCoverage.UseCoverageStartDateId)
                SetSelectedItem(Me.moRecoverDeciveDrop, TheCoverage.RecoverDeviceId)
            End If
            PopulateDepreciationScheduleDropdown()
            'BindListControlToDataView(moMethodOfRepairDrop, LookupListNew.GetMethodOfRepairLookupList(oLanguageId), , , True)
            moMethodOfRepairDrop.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            SetSelectedItem(Me.moMethodOfRepairDrop, TheCoverage.MethodOfRepairId)

            'BindListControlToDataView(cboDeductibleBasedOn, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(oLanguageId), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            cboDeductibleBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, TheCoverage.DeductibleBasedOnId)
            If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                SetSelectedItem(Me.cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__FIXED))
            ElseIf deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION Then
                SetSelectedItem(Me.cboDeductibleBasedOn, TheCoverage.DeductibleExpressionId)
            Else
                SetSelectedItem(Me.cboDeductibleBasedOn, TheCoverage.DeductibleBasedOnId)
            End If
        End Sub

        Private Sub PopulateCoveragePricing()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If moProductDrop.SelectedIndex = -1 Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                Dim oPriceMatrixView As DataView = LookupListNew.GetPriceMatrixLookupList(oProductId, oLanguageId)
                If oPriceMatrixView.Count > 0 Then
                    moCoveragePricingText.Text = oPriceMatrixView.Item(FIRST_POS).Item(COL_DESCRIPTION_NAME).ToString
                End If

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateRetail()
            If moProductDrop.SelectedIndex = -1 Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                Dim oPercentOfRetailDataview As DataView = LookupListNew.GetPercentOfRetailLookup(oProductId)
                If oPercentOfRetailDataview.Count > 0 Then
                    If oPercentOfRetailDataview.Item(FIRST_POS).Item(COL_CODE_NAME) Is System.DBNull.Value Then
                        TheCoverage.PercentOfRetail = New DecimalType(0)
                    Else
                        TheCoverage.PercentOfRetail = New DecimalType(CType(oPercentOfRetailDataview.Item(FIRST_POS).Item(COL_CODE_NAME), Decimal))
                    End If
                    PopulateControlFromBOProperty(moRetailText, TheCoverage.PercentOfRetail)

                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)

            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                If IsPostBack And Not Me.State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    moOffsetText.Text = Me.State.selectedOffset
                    txtMarkupDistPercent.Text = Me.State.selectedMarkupDistnPercent
                    txtOffsetDays.Text = Me.State.selectedOffsetDays
                    moEffectiveText.Text = Me.State.selectedEffective
                    moExpirationText.Text = Me.State.selectedExpiration
                    moCertificateDurationText.Text = Me.State.selectedCertificateDuration
                    moCoverageDurationText.Text = Me.State.selectedCoverageDuration
                    moLiabilityText.Text = Me.State.selectedLiability
                    moLiabilityLimitPercentText.Text = Me.State.selectedLiabilityLimitPercent
                    moDeductibleText.Text = Me.State.selectedDeductible
                    moDeductiblePercentText.Text = Me.State.selectedDeductiblePercent
                    moCovDeductibleText.Text = Me.State.selectedCovDeductible
                    moRepairDiscountPctText.Text = Me.State.selectedRepairDiscountPct
                    moReplacementDiscountPctText.Text = Me.State.selectedReplacementDiscountPct
                    moAgentcodeText.Text = Me.State.selectedAgentCode
                    moCoverageLiabilityLimitText.Text = Me.State.selectedCoverageLiabilityLimit
                    moPerIncidentLiabilityLimitCapText.Text = Me.State.selectedPerIncidentLiabilityLimitCap
                    moCoverageLiabilityLimitPercentText.Text = Me.State.selectedCoverageLiabilityLimitPercent
                    moClaimLimitCountText.Text = Me.State.selectedClaimLimitCount
                    If Not TheDepreciationSchedule.IsDeleted Then
                        BindSelectItem(TheDepreciationSchedule.DepreciationScheduleId.ToString, ddlDepSchCashReimbursement)
                    End If
                    Me.State.IsUndo = False
                Else
                    ' JLR - Otherwise load values from BO unless it is new with copy
                    ' In that case, BO has been cleared but we want to preserve the values 
                    ' already in the screen
                    If Not Me.State.IsNewWithCopy Then
                        PopulateControlFromBOProperty(moCertificateDurationText, TheCoverage.CertificateDuration)
                        PopulateControlFromBOProperty(moCoverageDurationText, TheCoverage.CoverageDuration)
                        PopulateControlFromBOProperty(moLiabilityText, TheCoverage.LiabilityLimit)
                        PopulateControlFromBOProperty(moLiabilityLimitPercentText, TheCoverage.LiabilityLimitPercent)
                        PopulateControlFromBOProperty(moDeductibleText, TheCoverage.Deductible)
                        PopulateControlFromBOProperty(moDeductiblePercentText, TheCoverage.DeductiblePercent)
                        PopulateControlFromBOProperty(moOffsetText, TheCoverage.OffsetToStart)
                        PopulateControlFromBOProperty(txtMarkupDistPercent, TheCoverage.MarkupDistributionPercent)
                        PopulateControlFromBOProperty(txtOffsetDays, TheCoverage.OffsetToStartDays)
                        PopulateControlFromBOProperty(moRepairDiscountPctText, TheCoverage.RepairDiscountPct)
                        PopulateControlFromBOProperty(moReplacementDiscountPctText, TheCoverage.ReplacementDiscountPct)
                        PopulateControlFromBOProperty(moClaimLimitCountText, TheCoverage.CoverageClaimLimit)

                        If TheCoverage.PerIncidentLiabilityLimitCap.ToString() = String.Empty Then
                            PopulateControlFromBOProperty(moPerIncidentLiabilityLimitCapText, "0")
                        Else
                            PopulateControlFromBOProperty(moPerIncidentLiabilityLimitCapText, TheCoverage.PerIncidentLiabilityLimitCap)
                        End If

                        If TheCoverage.CoverageLiabilityLimit.ToString() = String.Empty Then
                            PopulateControlFromBOProperty(moCoverageLiabilityLimitText, "0")
                        Else
                            PopulateControlFromBOProperty(moCoverageLiabilityLimitText, TheCoverage.CoverageLiabilityLimit)
                        End If

                        PopulateControlFromBOProperty(moCoverageLiabilityLimitPercentText, TheCoverage.CoverageLiabilityLimitPercent)
                        Dim oProductLiabilityLimitBaseId As Guid = TheCoverage.ProdLiabilityLimitBaseId

                        If (oProductLiabilityLimitBaseId.Equals(Guid.Empty) Or oProductLiabilityLimitBaseId = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, PROD_LIAB_BASED_ON_NOT_APP)) Then
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitLabel, False)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitText, False)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentLabel, False)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentText, False)
                            moCoverageLiabilityLimitPercentText.Text = String.Empty
                        Else
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitLabel, True)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitText, True)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentLabel, True)
                            ControlMgr.SetVisibleControl(Me, moCoverageLiabilityLimitPercentText, True)
                        End If

                        If TheCoverage.Effective Is Nothing Then
                            PopulateControlFromBOProperty(moEffectiveText, TheCoverage.Effective)
                        Else
                            PopulateControlFromBOProperty(moEffectiveText, TheCoverage.Effective.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                        End If
                        EnableDisableDeductible(TheCoverage.DeductibleBasedOnId, False)
                        If TheCoverage.Expiration Is Nothing Then
                            PopulateControlFromBOProperty(moExpirationText, TheCoverage.Expiration)
                        Else
                            PopulateControlFromBOProperty(moExpirationText, TheCoverage.Expiration.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                        End If
                        PopulateControlFromBOProperty(moAgentcodeText, TheCoverage.AgentCode)
                        If Not TheDepreciationSchedule.IsDeleted Then
                            BindSelectItem(TheDepreciationSchedule.DepreciationScheduleId.ToString, ddlDepSchCashReimbursement)
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub LoadCurrencyOfCoverage()
            If Not Me.State.IsCoverageNew Then
                ControlMgr.SetEnableControl(Me, Me.TextBoxCurrencyOfCoverage, False)
                Dim currencyOfCoveragedv As DataView = TheCoverage.GetCurrencyOfCoverage(TheCoverage.Id)
                If currencyOfCoveragedv.Count > 1 Or currencyOfCoveragedv.Count = 0 Then
                    Throw New GUIException("", Assurant.ElitaPlus.Common.ErrorCodes.COVERAGE_NONE_OR_MORE_THAN_ONE_CONTRACT_IN_EFFECT_FOUND_ERR)
                Else
                    Me.PopulateControlFromBOProperty(Me.TextBoxCurrencyOfCoverage, currencyOfCoveragedv.Table.Rows(0).Item(0))
                End If
            Else
                ControlMgr.SetVisibleControl(Me, Me.currLabelDiv, False)
                ControlMgr.SetVisibleControl(Me, Me.currTextBoxDiv, False)
            End If

        End Sub

        Private Sub GetCoverageDeductible()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim oDealerId As Guid = TheDealerControl.SelectedGuid
            Dim oEffectiveDate As String
            Dim tempdate As Date
            tempdate = DateHelper.GetDateValue(moEffectiveText.Text)
            oEffectiveDate = tempdate.ToString("yyyyMMdd")
            Try
                Dim oCDView As DataView = TheCoverage.GetCoverageDeductable(oDealerId, oEffectiveDate, oLanguageId)
                If oCDView.Count > 0 Then
                    moCovDeductibleText.Text = oCDView.Item(FIRST_POS).Item(COL_DESCRIPTION_NAME).ToString
                Else
                    moCovDeductibleText.Text = Nothing
                    moDeductiblePercentText.Enabled = False
                    moDeductibleText.Enabled = False
                End If

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " & ex.Message)
            End Try
        End Sub

        Private Sub CoverageMarkupDistribution()
            Dim oContract As Contract
            Try
                oContract = Contract.GetMaxExpirationContract(TheCoverage.DealerId)
                If Not oContract Is Nothing And oContract.AllowCoverageMarkupDistribution.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    ControlMgr.SetVisibleControl(Me, Me.lblMarkupDistPercent, True)
                    ControlMgr.SetVisibleControl(Me, Me.txtMarkupDistPercent, True)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.lblMarkupDistPercent, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtMarkupDistPercent, False)
                End If
            Catch ex As Exception
                ControlMgr.SetVisibleControl(Me, Me.lblMarkupDistPercent, False)
                ControlMgr.SetVisibleControl(Me, Me.txtMarkupDistPercent, False)
            End Try
        End Sub
#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()
            With TheCoverage
                If TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "DealerId", TheDealerControl.SelectedGuid)
                If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "ProductCodeId", moProductDrop)
                If moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "RiskTypeId", moRiskDrop)
                If moCoverageTypeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "CoverageTypeId", moCoverageTypeDrop)
                If moProductItemDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "ProductItemId", moProductItemDrop)
                If moOptionalDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "OptionalId", moOptionalDrop)
                If moIsClaimAllowedDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "IsClaimAllowedId", moIsClaimAllowedDrop)

                'REQ-5358
                TheCoverage.OffsetMethod = GetSelectedValue(moOffsetMethodDrop)
                TheCoverage.OffsetMethodId = LookupListNew.GetIdFromCode("COVERAGE_OFFSET_METHOD", TheCoverage.OffsetMethod)

                If TheCoverage.OffsetMethod = "FIXED" Then
                    Me.PopulateBOProperty(TheCoverage, "OffsetToStart", moOffsetText)
                    Me.PopulateBOProperty(TheCoverage, "OffsetToStartDays", txtOffsetDays)
                Else
                    TheCoverage.OffsetToStart = 0
                    TheCoverage.OffsetToStartDays = 0
                End If

                If moMethodOfRepairDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    Me.PopulateBOProperty(TheCoverage, "MethodOfRepairId", moMethodOfRepairDrop)
                Else
                    TheCoverage.MethodOfRepairId = Nothing
                End If

                .ItemId = Me.State.selectedItemId
                Me.PopulateBOProperty(TheCoverage, "CertificateDuration", moCertificateDurationText)
                Me.PopulateBOProperty(TheCoverage, "CoverageDuration", moCoverageDurationText)
                Me.PopulateBOProperty(TheCoverage, "LiabilityLimit", moLiabilityText)
                Me.PopulateBOProperty(TheCoverage, "MarkupDistributionPercent", txtMarkupDistPercent)
                Me.PopulateBOProperty(TheCoverage, "LiabilityLimitPercent", moLiabilityLimitPercentText)
                Me.PopulateBOProperty(TheCoverage, "Deductible", moDeductibleText)
                Me.PopulateBOProperty(TheCoverage, "DeductiblePercent", moDeductiblePercentText)

                'REG-6289
                Me.PopulateBOProperty(TheCoverage, "CoverageClaimLimit", moClaimLimitCountText)

                Dim deductibleBasedOnId As Guid = GetSelectedItem(Me.cboDeductibleBasedOn)
                If (deductibleBasedOnId = Guid.Empty) Then
                    Me.PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", Me.cboDeductibleBasedOn)
                    Me.PopulateBOProperty(TheCoverage, "DeductibleExpressionId", Guid.Empty)
                Else
                    Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)
                    If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                        Me.PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                        Me.PopulateBOProperty(TheCoverage, "DeductibleExpressionId", deductibleBasedOnId)
                    Else
                        Me.PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", Me.cboDeductibleBasedOn)
                        Me.PopulateBOProperty(TheCoverage, "DeductibleExpressionId", Guid.Empty)
                    End If
                End If

                'Me.PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", cboDeductibleBasedOn)
                Me.PopulateBOProperty(TheCoverage, "RepairDiscountPct", moRepairDiscountPctText)
                Me.PopulateBOProperty(TheCoverage, "ReplacementDiscountPct", moReplacementDiscountPctText)
                Me.PopulateBOProperty(TheCoverage, "UseCoverageStartDateId", UseCoverageStartDateId)
                Me.PopulateBOProperty(TheCoverage, "AgentCode", moAgentcodeText)
                Me.PopulateBOProperty(TheCoverage, "CoverageLiabilityLimit", moCoverageLiabilityLimitText)
                Me.PopulateBOProperty(TheCoverage, "CoverageLiabilityLimitPercent", moCoverageLiabilityLimitPercentText)

                Me.PopulateBOProperty(TheCoverage, "IsReInsuredId", Me.moReInsuredDrop)

                If Len(moEffectiveText.Text) > 0 Then
                    Me.PopulateBOProperty(TheCoverage, "Effective", DateHelper.GetDateValue(moEffectiveText.Text).ToString)
                Else
                    Me.PopulateBOProperty(TheCoverage, "Effective", "")
                End If

                If Len(moExpirationText.Text) > 0 Then
                    Me.PopulateBOProperty(TheCoverage, "Expiration", DateHelper.GetDateValue(moExpirationText.Text).ToString)
                Else
                    PopulateBOProperty(TheCoverage, "Expiration", "")
                End If

                If Not TheDepreciationSchedule.IsDeleted Then
                    PopulateBOProperty(TheDepreciationSchedule, "DepreciationScheduleId", ddlDepSchCashReimbursement)
                    TheCoverage.AddCoverageDepreciationScdChild(TheDepreciationSchedule.DepreciationScheduleId)
                End If

                Me.PopulateBOProperty(TheCoverage, "PerIncidentLiabilityLimitCap", moPerIncidentLiabilityLimitCapText)

                If moEarningCodeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "EarningCodeId", moEarningCodeDrop)
                If moRecoverDeciveDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then Me.PopulateBOProperty(TheCoverage, "RecoverDeviceId", moRecoverDeciveDrop)
                Me.PopulateBOProperty(TheCoverage, "TaxTypeXCD", Me.moTaxTypeDrop, False, True)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                TheCoverage.UniqueFieldsChanged = Me.UniqueFieldsChanged()
                With TheCoverage
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                    If bIsDirty = False Then bIsDirty = IsDirtyRateBO()
                End With

            Catch ex As Exception
                Throw New PopulateBOErrorException
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False
            Dim bisfamilydirty As Boolean = False

            With TheCoverage
                If Me.UniqueFieldsChanged() And .IsLastCoverage() = False And .IsFirstCoverage = False Then
                    Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.COVERAGEBO_019, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    .cancelEdit()
                    bIsOk = False
                Else
                    bIsDirty = IsDirtyBO()
                    bisfamilydirty = .IsFamilyDirty
                    .Save()
                    EnableUniqueFields()

                    Me.LoadCoverageRateList()
                    If Not bIsDirty Then
                        If Not Me.State.moCoverageRateList Is Nothing Then
                            For Each _coveragerate As CoverageRate In Me.State.moCoverageRateList
                                If _coveragerate.IsDirty Then
                                    bIsDirty = _coveragerate.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    Me.LoadCoverageDeductibleList()
                    If Not bIsDirty Then
                        If Not Me.State.moCoverageDeductibleList Is Nothing Then
                            For Each _coveragedeductible As CoverageDeductible In Me.State.moCoverageDeductibleList
                                If _coveragedeductible.IsDirty Then
                                    bIsDirty = _coveragedeductible.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    Me.LoadCoverageConseqDamageList()
                    If Not bIsDirty Then
                        If Not Me.State.moCoverageConseqDamageList Is Nothing Then
                            For Each _coverageconseqdamage As CoverageConseqDamage In Me.State.moCoverageConseqDamageList
                                If _coverageconseqdamage.IsDirty Then
                                    bIsDirty = _coverageconseqdamage.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If SaveCoverageRateList() Then
                        EnableCoverageButtons(True)
                        If SaveCoverageDeductibleList() Then
                            manageDeductibleButtons(True, False)
                        End If
                        If SaveCoverageConseqDamageList() Then
                            'manageDeductibleButtons(True, False)
                        End If
                    Else
                        .RejectChanges()
                        '.Delete()
                        '.Save()
                        bIsOk = False
                    End If
                End If
            End With

            If (bIsOk = True) Then
                If bIsDirty = True OrElse bisfamilydirty = True Then
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If

            Return bIsOk
        End Function

        Private Function DeleteCoverage() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheCoverage
                    PopulateBOsFromForm()
                    'check if there are any certificates associated with the coverage
                    If .GetAssociatedCertificateCount(.Id) > 0 Then
                        Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.DELETE_COVERAGE_WITH_CERT_PRESENT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                        .cancelEdit()
                        bIsOk = False
                    ElseIf .IsLastCoverage() = False And .IsFirstCoverage = False Then
                        Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.COVERAGEBO_017, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                        .cancelEdit()
                        bIsOk = False
                    ElseIf DeleteAllCoverageRate() = False Then
                        .cancelEdit()
                        bIsOk = False
                    ElseIf DeleteAllCoverageDeductible() = False Then
                        .cancelEdit()
                        bIsOk = False
                    ElseIf DeleteAllCoveragConseqDamage() = False Then
                        .cancelEdit()
                        bIsOk = False
                    Else
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM003, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function UniqueFieldsChanged() As Boolean
            Dim moEffectiveDate As Date
            Dim moExpirationDate As Date
            If Len(moEffectiveText.Text) > 0 Then moEffectiveDate = DateHelper.GetDateValue(moEffectiveText.Text)
            If Len(moExpirationText.Text) > 0 Then moExpirationDate = DateHelper.GetDateValue(moExpirationText.Text)

            With TheCoverage
                If Me.State.IsCoverageNew Then
                    Return False
                Else
                    If .CertificateDuration.ToString <> moCertificateDurationText.Text _
                    Or .CoverageDuration.ToString <> moCoverageDurationText.Text _
                    Or Not .CoverageTypeId.Equals(GetSelectedItem(moCoverageTypeDrop)) _
                    Or CType(.Effective.ToString, Date) <> moEffectiveDate _
                    Or CType(.Expiration.ToString, Date) <> moExpirationDate Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End With
        End Function

#End Region

#Region "Coverage-Rate"

#Region "Coverage-Rate Button Management"

        Private Sub EnableEditRateButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnSaveRate_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, BtnCancelRate, bIsReadWrite)
        End Sub

        Private Sub EnableNewRateButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnNewRate_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnableRateButtons(ByVal bIsReadWrite As Boolean)
            EnableNewRateButtons(bIsReadWrite)
            EnableEditRateButtons(bIsReadWrite)
        End Sub

        Private Sub EnableForEditRateButtons(ByVal bIsReadWrite As Boolean)
            EnableNewRateButtons(Not bIsReadWrite)
            EnableEditRateButtons(bIsReadWrite)
        End Sub

        Private Sub EnableCoveragePricing(ByVal bIsReadWrite As Boolean)
            moGridView.Columns(COMMISSIONS_PERCENT).Visible = bIsReadWrite
            moGridView.Columns(MARKETING_PERCENT).Visible = bIsReadWrite
            moGridView.Columns(ADMIN_EXPENSE).Visible = bIsReadWrite
            moGridView.Columns(PROFIT_EXPENSE).Visible = bIsReadWrite
            moGridView.Columns(LOSS_COST_PERCENT).Visible = bIsReadWrite
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCoverageRateList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oCoverageRates As CoverageRate
            Dim oDataView As DataView

            If Me.State.IsCoverageNew = True And Not Me.State.IsNewWithCopy Then Return ' We can not have CoverageRates if the coverage is new

            Try

                If CoveragePricingCode = NO_COVERAGE_PRICING Then
                    EnableCoveragePricing(False)
                Else
                    EnableCoveragePricing(True)
                End If

                If Me.State.IsNewWithCopy Then
                    oDataView = oCoverageRates.GetList(Guid.Empty)
                    If Not oAction = ACTION_CANCEL_DELETE Then Me.LoadCoverageRateList()
                    If Not Me.State.moCoverageRateList Is Nothing Then
                        oDataView = getDVFromArray(Me.State.moCoverageRateList, oDataView.Table)
                    End If
                Else
                    oDataView = oCoverageRates.GetList(TheCoverage.Id)
                End If

                Select Case oAction
                    Case ACTION_NONE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView, 0)
                        EnableForEditRateButtons(False)
                    Case ACTION_SAVE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_EDIT
                        If Me.State.IsNewWithCopy Then
                            CoverageRateId = Me.State.moCoverageRateList(moGridView.SelectedIndex).Id.ToString
                        Else
                            CoverageRateId = Me.GetSelectedGridText(moGridView, COVERAGE_RATE_ID)
                        End If
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)
                    Case ACTION_NEW
                        If Me.State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DBCOVERAGE_RATE_ID) = TheCoverageRate.Id.ToByteArray
                        oDataView.Table.Rows.Add(oRow)
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)

                End Select

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

            Catch ex As Exception
                moMsgControllerRate.AddError(COVERAGE_FORM004)
                moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
            End Try
        End Sub

        Private Function getDVFromArray(ByVal oArray() As CoverageDeductible, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageDeductible As CoverageDeductible
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim dtvw As DataView = LookupListNew.GetMethodOfRepairLookupList(languageid)
            Dim DeducBasedon As DataView = LookupListNew.GetComputeDeductibleBasedOnLookupList(languageid)
            If Not oArray Is Nothing Then
                For Each oCoverageDeductible In oArray
                    If Not oCoverageDeductible Is Nothing Then
                        oRow = oDtable.NewRow
                        oRow(CoverageDeductible.COL_NAME_COVERAGE_DED_ID) = oCoverageDeductible.Id.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_METHOD_OF_REPAIR_ID) = oCoverageDeductible.MethodOfRepairId.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_METHOD_OF_REPAIR) = LookupListNew.GetDescriptionFromId(dtvw, oCoverageDeductible.MethodOfRepairId)
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE_BASED_ON_ID) = oCoverageDeductible.DeductibleBasedOnId.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE_BASED_ON) = LookupListNew.GetDescriptionFromId(DeducBasedon, oCoverageDeductible.DeductibleBasedOnId)
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE) = oCoverageDeductible.Deductible.Value
                        oDtable.Rows.Add(oRow)
                    End If
                Next
            End If
            Return oDtable.DefaultView

        End Function
        Private Function getDVFromArray(ByVal oArray() As CoverageConseqDamage, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageConseqDamage As CoverageConseqDamage
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            If Not oArray Is Nothing Then
                For Each oCoverageConseqDamage In oArray
                    If Not oCoverageConseqDamage Is Nothing Then
                        oRow = oDtable.NewRow
                        oRow(oCoverageConseqDamage.COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID) = oCoverageConseqDamage.Id.ToByteArray
                        oRow(oCoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT) = oCoverageConseqDamage.LiabilityLimitPerIncident.Value
                        oRow(oCoverageConseqDamage.COL_NAME_CONSEQ_DAMAGE_TYPE) = oCoverageConseqDamage.ConseqDamageTypeXcd
                        oRow(oCoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_CUMULATIVE) = oCoverageConseqDamage.LiabilityLimitCumulative.Value
                        oRow(oCoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_BASED_ON) = oCoverageConseqDamage.LiabilityLimitBaseXcd
                        oRow(oCoverageConseqDamage.COL_NAME_FULFILMENT_METHOD_XCD) = oCoverageConseqDamage.FulfilmentMethodXcd
                        If Not oCoverageConseqDamage.Effective Is Nothing Then
                            oRow(oCoverageConseqDamage.COL_NAME_EFFECTIVE) = oCoverageConseqDamage.Effective.Value
                        End If
                        If Not oCoverageConseqDamage.Expiration Is Nothing Then
                            oRow(oCoverageConseqDamage.COL_NAME_EXPIRATION) = oCoverageConseqDamage.Expiration.Value
                        End If
                        oRow(oCoverageConseqDamage.COL_NAME_CONSEQ_DAMAGE_TYPE_DESC) = LookupListNew.GetDescrionFromListCode("PERILTYP", oCoverageConseqDamage.ConseqDamageTypeXcd.Replace("PERILTYP-", ""))
                        oRow(oCoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_BASED_ON_DESC) = LookupListNew.GetDescrionFromListCode("PRODLILIMBASEDON", oCoverageConseqDamage.LiabilityLimitBaseXcd.Replace("PRODLILIMBASEDON-", ""))
                        oRow(oCoverageConseqDamage.COL_NAME_FULFILMENT_METHOD_DESC) = LookupListNew.GetDescrionFromListCode("FULFILMETH", oCoverageConseqDamage.FulfilmentMethodXcd.Replace("FULFILMETH-", ""))

                        oDtable.Rows.Add(oRow)
                    End If
                Next
            End If
            Return oDtable.DefaultView

        End Function
        Private Function getDVFromArray(ByVal oArray() As CoverageRate, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageRate As CoverageRate
            For Each oCoverageRate In oArray
                If Not oCoverageRate Is Nothing Then
                    oRow = oDtable.NewRow
                    oRow(COL_COVERAGE_RATE_ID) = oCoverageRate.Id.ToByteArray
                    oRow(COL_LOW_PRICE) = oCoverageRate.LowPrice.Value
                    oRow(COL_HIGH_PRICE) = oCoverageRate.HighPrice.Value
                    oRow(COL_GROSS_AMT) = oCoverageRate.GrossAmt.Value
                    oRow(COL_COMMISSION_PERCENT) = oCoverageRate.CommissionsPercent.Value
                    oRow(COL_MARKETING_PERCENT) = oCoverageRate.MarketingPercent.Value
                    oRow(COL_ADMIN_EXPENSE) = oCoverageRate.AdminExpense.Value
                    oRow(COL_PROFIT_EXPENSE) = oCoverageRate.ProfitExpense.Value
                    oRow(COL_LOSS_COST_PERCENT) = oCoverageRate.LossCostPercent.Value
                    oRow(COL_GROSS_AMOUNT_PERCENT) = oCoverageRate.GrossAmountPercent.Value
                    oRow(COL_RENEWAL_NUMBER) = oCoverageRate.RenewalNumber.Value

                    oDtable.Rows.Add(oRow)
                End If
            Next
            oDtable.DefaultView.Sort() = COL_LOW_PRICE
            Return oDtable.DefaultView

        End Function

        Private Sub ValidateCoverage()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)

            If yesId = GetSelectedItem(UseCoverageStartDateId) Then
                Dim oCoverageRate As CoverageRate
                LoadCoverageRateList()
                If (Not State.moCoverageRateList Is Nothing) Then
                    If Me.State.moCoverageRateList.Count > 1 Then
                        Me.PopulateBOProperty(TheCoverage, "UseCoverageStartDateId", UseCoverageStartDateId)
                        Me.moUseCoverageStartDateLable.ForeColor = Color.Red
                        Throw New GUIException(Message.MSG_INVALID_COVERAGE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_MULTIPLE_COVERAGES_NOT_ALLOWED)
                    Else
                        For Each oCoverageRate In Me.State.moCoverageRateList
                            If Not oCoverageRate Is Nothing Then
                                If oCoverageRate.GrossAmt.Value > 0 Then
                                    Me.moUseCoverageStartDateLable.ForeColor = Color.Red
                                    Throw New GUIException(Message.MSG_INVALID_COVERAGE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_GROSS_AMT)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

        Private Sub ModifyGridHeader()

            moGridView.Columns(COMMISSIONS_PERCENT).HeaderText = moGridView.Columns(COMMISSIONS_PERCENT).HeaderText.Replace("%", "<br>%")
            moGridView.Columns(MARKETING_PERCENT).HeaderText = moGridView.Columns(MARKETING_PERCENT).HeaderText.Replace("%", "<br>%")
            moGridView.Columns(ADMIN_EXPENSE).HeaderText = moGridView.Columns(ADMIN_EXPENSE).HeaderText.Replace("%", "<br>%")
            moGridView.Columns(PROFIT_EXPENSE).HeaderText = moGridView.Columns(PROFIT_EXPENSE).HeaderText.Replace("%", "<br>%")
            moGridView.Columns(LOSS_COST_PERCENT).HeaderText = moGridView.Columns(LOSS_COST_PERCENT).HeaderText.Replace("%", "<br>%")

        End Sub

        Private Sub PopulateCoverageRate()
            If Me.State.IsNewWithCopy Then
                With Me.State.moCoverageRateList(moGridView.SelectedIndex)
                    Me.SetSelectedGridText(moGridView, LOW_PRICE, .LowPrice.ToString)
                    Me.SetSelectedGridText(moGridView, HIGH_PRICE, .HighPrice.ToString)
                    Me.SetSelectedGridText(moGridView, GROSS_AMT, .GrossAmt.ToString)
                    Me.SetSelectedGridText(moGridView, COMMISSIONS_PERCENT, .CommissionsPercent.ToString)
                    Me.SetSelectedGridText(moGridView, MARKETING_PERCENT, .MarketingPercent.ToString)
                    Me.SetSelectedGridText(moGridView, ADMIN_EXPENSE, .AdminExpense.ToString)
                    Me.SetSelectedGridText(moGridView, PROFIT_EXPENSE, .ProfitExpense.ToString)
                    Me.SetSelectedGridText(moGridView, LOSS_COST_PERCENT, .LossCostPercent.ToString)
                    Me.SetSelectedGridText(moGridView, LOW_PRICE, .LowPrice.ToString)
                    Me.SetSelectedGridText(moGridView, GROSS_AMOUNT_PERCENT, .GrossAmountPercent.ToString)
                    Me.SetSelectedGridText(moGridView, RENEWAL_NUMBER, .RenewalNumber.ToString)
                End With
            Else
                With TheCoverageRate
                    Me.SetSelectedGridText(moGridView, LOW_PRICE, .LowPrice.ToString)
                    Me.SetSelectedGridText(moGridView, HIGH_PRICE, .HighPrice.ToString)
                    Me.SetSelectedGridText(moGridView, GROSS_AMT, .GrossAmt.ToString)
                    Me.SetSelectedGridText(moGridView, COMMISSIONS_PERCENT, .CommissionsPercent.ToString)
                    Me.SetSelectedGridText(moGridView, MARKETING_PERCENT, .MarketingPercent.ToString)
                    Me.SetSelectedGridText(moGridView, ADMIN_EXPENSE, .AdminExpense.ToString)
                    Me.SetSelectedGridText(moGridView, PROFIT_EXPENSE, .ProfitExpense.ToString)
                    Me.SetSelectedGridText(moGridView, LOSS_COST_PERCENT, .LossCostPercent.ToString)
                    Me.SetSelectedGridText(moGridView, LOW_PRICE, .LowPrice.ToString)
                    Me.SetSelectedGridText(moGridView, GROSS_AMOUNT_PERCENT, .GrossAmountPercent.ToString)
                    Me.SetSelectedGridText(moGridView, RENEWAL_NUMBER, .RenewalNumber.ToString)
                End With
            End If

        End Sub


#End Region

#Region "Populate Conseq Damage"
        'Populate ConseqDamage starts here

        Private Sub PopulateCoverageConseqDamageList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oCoverageConseqDamage As CoverageConseqDamage
            Dim oDataView As DataView

            If Me.State.IsCoverageNew = True And Not Me.State.IsNewWithCopy Then Return ' We can not have CoverageConseqDamages if the coverage is new

            Try

                'EnableCoverageConseqDamage(True)

                If Me.State.IsNewWithCopy Then
                    oDataView = oCoverageConseqDamage.GetList(Guid.Empty, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                    If Not oAction = ACTION_CANCEL_DELETE Then Me.LoadCoverageConseqDamageList()

                    If Not Me.State.moCoverageConseqDamageList Is Nothing Then
                        oDataView = getDVFromArray(Me.State.moCoverageConseqDamageList, oDataView.Table)
                    End If
                Else
                    oDataView = oCoverageConseqDamage.GetList(TheCoverage.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If


                Select Case oAction
                    Case ACTION_NONE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridViewConseqDamage, 0)
                        EnableForEditConseqDamageButtons(False)
                    Case ACTION_SAVE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex)
                        EnableForEditConseqDamageButtons(False)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex)
                        EnableForEditConseqDamageButtons(False)
                    Case ACTION_EDIT
                        If Me.State.IsNewWithCopy Then
                            CoverageConseqDamageId = Me.State.moCoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex).Id.ToString
                        Else
                            CoverageConseqDamageId = Me.GetSelectedGridText(moGridViewConseqDamage, COVERAGE_CONSEQ_DAMAGE_ID)
                        End If
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex, True)
                        EnableForEditConseqDamageButtons(True)
                    Case ACTION_NEW
                        If Me.State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DBCOVERAGE_CONSEQ_DAMAGE_ID) = TheCoverageConseqDamage.Id.ToByteArray
                        oRow(DBCOVERAGE_ID) = TheCoverage.Id.ToByteArray
                        oRow(DBLIABLILITY_LIMIT_PER_INCIDENT) = "0"
                        oRow(DBLIABLILITY_LIMIT_CUMULATIVE) = "0"
                        oDataView.Table.Rows.Add(oRow)
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex, True)
                        EnableForEditConseqDamageButtons(True)
                End Select

                moGridViewConseqDamage.DataSource = oDataView
                moGridViewConseqDamage.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridViewConseqDamage)

            Catch ex As Exception
                moMsgControllerConseqDamage.AddError(COVERAGE_FORM006)
                moMsgControllerConseqDamage.AddError(ex.Message, False)
                'moMsgControllerConseqDamage.Show()
            End Try
        End Sub

        Private Sub PopulateCoverageConseqDamage()
            If Me.State.IsNewWithCopy Then
                With Me.State.moCoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex)
                    Me.SetSelectedGridText(moGridViewConseqDamage, LIABLILITY_LIMIT_PER_INCIDENT, .LiabilityLimitPerIncident.ToString)
                    Me.SetSelectedGridText(moGridViewConseqDamage, LIABLILITY_LIMIT_CUMULATIVE, .LiabilityLimitCumulative.ToString)
                    If Not .Effective Is Nothing Then
                        Me.SetSelectedGridText(moGridViewConseqDamage, CONSEQ_DAMAGE_EFFECTIVE_DATE, .Effective.Value.ToString)
                    End If
                    If Not .Expiration Is Nothing Then
                        Me.SetSelectedGridText(moGridViewConseqDamage, CONSEQ_DAMAGE_EXPIRATION_DATE, .Expiration.Value.ToString)
                    End If
                    If Not .ConseqDamageTypeXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(CONSEQ_DAMAGE_TYPE).FindControl("moConseqDamageTypeDropdown"), DropDownList), .ConseqDamageTypeXcd)
                    End If
                    If Not .LiabilityLimitBaseXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(LIABILITY_LIMIT_BASED_ON).FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList), .LiabilityLimitBaseXcd)
                    End If
                    If Not .FulfilmentMethodXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(FULFILMENT_METHOD).FindControl("moFulfilmentMethodDropdown"), DropDownList), .FulfilmentMethodXcd)
                    End If

                End With
            Else
                With TheCoverageConseqDamage
                    Me.SetSelectedGridText(moGridViewConseqDamage, LIABLILITY_LIMIT_PER_INCIDENT, .LiabilityLimitPerIncident.ToString)
                    Me.SetSelectedGridText(moGridViewConseqDamage, LIABLILITY_LIMIT_CUMULATIVE, .LiabilityLimitCumulative.ToString)
                    Me.SetSelectedGridText(moGridViewConseqDamage, CONSEQ_DAMAGE_EFFECTIVE_DATE, .Effective.ToString)
                    Me.SetSelectedGridText(moGridViewConseqDamage, CONSEQ_DAMAGE_EXPIRATION_DATE, .Expiration.ToString)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(CONSEQ_DAMAGE_TYPE).FindControl("moConseqDamageTypeDropdown"), DropDownList), .ConseqDamageTypeXcd)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(LIABILITY_LIMIT_BASED_ON).FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList), .LiabilityLimitBaseXcd)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(FULFILMENT_METHOD).FindControl("moFulfilmentMethodDropdown"), DropDownList), .FulfilmentMethodXcd)
                End With
            End If
        End Sub

#End Region

#Region "Button Management ConseqDamage"
        Private Sub EnableDisableConseqDamageButtons(ByVal newButton As Boolean, ByVal SaveCancelButton As Boolean)
            btnNewConseqDamage_WRITE.Enabled = newButton

            btnSaveConseqDamage_WRITE.Enabled = SaveCancelButton
            btnCancelConseqDamage_WRITE.Enabled = SaveCancelButton

        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateRateBOFromForm()
            With TheCoverageRate
                .CoverageId = TheCoverage.Id
                Me.PopulateBOProperty(TheCoverageRate, "LowPrice", CType(Me.GetSelectedGridControl(moGridView, LOW_PRICE), TextBox))
                Me.PopulateBOProperty(TheCoverageRate, "HighPrice", CType(Me.GetSelectedGridControl(moGridView, HIGH_PRICE), TextBox))
                ''Gross Amount Percent is set to 0


                If hdnGrossAmtOrPercent.Value = "Percent" Then
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmt", hdnGrossAmtOrPercentValue.Value)
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmountPercent", "0.00")
                ElseIf hdnGrossAmtOrPercent.Value = "Amount" Then
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmountPercent", hdnGrossAmtOrPercentValue.Value)
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmt", "0.00")
                Else
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmountPercent", CType(Me.GetSelectedGridControl(moGridView, GROSS_AMOUNT_PERCENT), TextBox))
                    Me.PopulateBOProperty(TheCoverageRate, "GrossAmt", CType(Me.GetSelectedGridControl(moGridView, GROSS_AMT), TextBox))
                End If
                hdnGrossAmtOrPercent.Value = String.Empty
                hdnGrossAmtOrPercentValue.Value = String.Empty
                If CoveragePricingCode = NO_COVERAGE_PRICING Then
                    Me.PopulateBOProperty(TheCoverageRate, "CommissionsPercent", GetAmountFormattedDoubleString("0"))
                    Me.PopulateBOProperty(TheCoverageRate, "MarketingPercent", GetAmountFormattedDoubleString("0"))
                    Me.PopulateBOProperty(TheCoverageRate, "AdminExpense", GetAmountFormattedDoubleString("0"))
                    Me.PopulateBOProperty(TheCoverageRate, "ProfitExpense", GetAmountFormattedDoubleString("0"))
                    Me.PopulateBOProperty(TheCoverageRate, "LossCostPercent", GetAmountFormattedDoubleString("0"))
                Else
                    Me.PopulateBOProperty(TheCoverageRate, "CommissionsPercent", CType(Me.GetSelectedGridControl(moGridView, COMMISSIONS_PERCENT), TextBox))
                    Me.PopulateBOProperty(TheCoverageRate, "MarketingPercent", CType(Me.GetSelectedGridControl(moGridView, MARKETING_PERCENT), TextBox))
                    Me.PopulateBOProperty(TheCoverageRate, "AdminExpense", CType(Me.GetSelectedGridControl(moGridView, ADMIN_EXPENSE), TextBox))
                    Me.PopulateBOProperty(TheCoverageRate, "ProfitExpense", CType(Me.GetSelectedGridControl(moGridView, PROFIT_EXPENSE), TextBox))
                    Me.PopulateBOProperty(TheCoverageRate, "LossCostPercent", CType(Me.GetSelectedGridControl(moGridView, LOSS_COST_PERCENT), TextBox))
                End If
                Me.PopulateBOProperty(TheCoverageRate, "RenewalNumber", CType(Me.GetSelectedGridControl(moGridView, RENEWAL_NUMBER), TextBox))
            End With

            ValidateCoverage()

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyRateBO() As Boolean
            Dim bIsDirty As Boolean = True
            If moGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' Coverage Rate is not in edit mode
            Dim sCoverageRateId As String = Me.GetSelectedGridText(moGridView, COVERAGE_RATE_ID)
            Try
                With TheCoverageRate
                    PopulateRateBOFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                moMsgControllerRate.AddError(COVERAGE_FORM004)
                moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyRateChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridView.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode
            If Me.State.IsNewWithCopy Then
                Me.LoadCoverageRateList()
                Me.State.moCoverageRateList(moGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewRate = False Then
                CoverageRateId = Me.GetSelectedGridText(moGridView, COVERAGE_RATE_ID)
            End If
            BindBoPropertiesToGridHeader()
            With TheCoverageRate
                PopulateRateBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditRateButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function

        ' The user selected a specific CoverageRate to Delete
        Private Function DeleteSelectedCoverageRate(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If Me.State.IsNewWithCopy Then
                    If Me.State.moCoverageRateList Is Nothing Then Me.LoadCoverageRateList()
                    Me.State.moCoverageRateList(nIndex) = Nothing
                Else
                    With TheCoverageRate()
                        .delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerRate.AddError(COVERAGE_FORM005)
                moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        ' Delete a CoverageRate from a DataView Row
        Private Function DeleteACoverageRate(ByVal oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim oCoverageRate As CoverageRate = New CoverageRate(New Guid(CType(oRow(DBCOVERAGE_RATE_ID), Byte())))
                oCoverageRate.delete()
                oCoverageRate.Save()
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerRate.AddError(ex.Message)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteAllCoverageRate() As Boolean
            Dim bIsOk As Boolean = True
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            Try
                oRows = TheCoverageRate.GetCovRateListForDelete(TheCoverage.Id).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageRate(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerRate.AddError(ex.Message)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteSelectedCoverageConseqDamage(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If Me.State.IsNewWithCopy Then
                    If Me.State.moCoverageConseqDamageList Is Nothing Then Me.LoadCoverageConseqDamageList()
                    Me.State.moCoverageConseqDamageList(nIndex) = Nothing
                Else
                    With TheCoverageConseqDamage()
                        .Delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerConseqDamage.AddError(COVERAGE_FORM006)
                moMsgControllerConseqDamage.AddError(ex.Message, False)
                'moMsgControllerConseqDamage.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteACoverageConseqDamage(ByVal oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim oCoverageConseqDamage As CoverageConseqDamage = New CoverageConseqDamage(New Guid(CType(oRow(DBCOVERAGE_CONSEQ_DAMAGE_ID), Byte())))
                oCoverageConseqDamage.Delete()
                oCoverageConseqDamage.Save()
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerRate.AddError(ex.Message)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function
        Private Function DeleteAllCoveragConseqDamage() As Boolean
            Dim bIsOk As Boolean = True
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            Try
                oRows = TheCoverageConseqDamage.GetList(TheCoverage.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageConseqDamage(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerRate.AddError(ex.Message)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function
        ''business part for Deductible

        ' The user selected a specific Coverage deductible to Delete
        Private Function DeleteSelectedCoverageDeductible(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If Me.State.IsNewWithCopy Then
                    If Me.State.moCoverageDeductibleList Is Nothing Then Me.LoadCoverageDeductibleList()
                    Me.State.moCoverageDeductibleList(nIndex) = Nothing
                Else
                    With TheCoverageDeductible()
                        .Delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerDeductible.AddError(COVERAGE_FORM006)
                moMsgControllerDeductible.AddError(ex.Message, False)
                'moMsgControllerDeductible.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        ' Delete a CoverageDeductible from a DataView Row
        Private Function DeleteACoverageDeductible(ByVal oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim oCoverageDeductible As CoverageDeductible = New CoverageDeductible(New Guid(CType(oRow(DBCOVERAGE_DED_ID), Byte())))
                oCoverageDeductible.Delete()
                oCoverageDeductible.Save()
            Catch ex As Exception
                moMsgControllerDeductible.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerDeductible.AddError(ex.Message)
                'moMsgControllerDeductible.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteAllCoverageDeductible() As Boolean
            Dim bIsOk As Boolean = True
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            Try
                oRows = TheCoverageDeductible.GetList(TheCoverage.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageDeductible(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerDeductible.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_FORM005, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moMsgControllerDeductible.AddError(ex.Message)
                'moMsgControllerDeductible.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Sub PopulateDeductibleBOFromForm()
            With TheCoverageDeductible
                .CoverageId = TheCoverage.Id
                Me.PopulateBOProperty(TheCoverageDeductible, "MethodOfRepairId", CType(Me.GetSelectedGridControl(dedGridView, METHOD_OF_REPAIR_DESC), DropDownList), True)

                Dim cboDeductibleBasedOn As DropDownList = CType(Me.GetSelectedGridControl(dedGridView, DEDUCTIBLE_BASED_ON_DESC), DropDownList)

                Dim deductibleBasedOnId As Guid = GetSelectedItem(cboDeductibleBasedOn)
                If (deductibleBasedOnId = Guid.Empty) Then
                    Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleBasedOnId", cboDeductibleBasedOn)
                    Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleExpressionId", Guid.Empty)
                Else
                    Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)
                    If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                        Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                        Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleExpressionId", deductibleBasedOnId)
                    Else
                        Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleBasedOnId", cboDeductibleBasedOn)
                        Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleExpressionId", Guid.Empty)
                    End If
                End If

                ' Me.PopulateBOProperty(TheCoverageDeductible, "DeductibleBasedOnId", CType(Me.GetSelectedGridControl(dedGridView, DEDUCTIBLE_BASED_ON_DESC), DropDownList), True)

                Dim result As Decimal
                If (Not (Decimal.TryParse(CType(Me.GetSelectedGridControl(dedGridView, DEDUCTIBLE), TextBox).Text, result))) Then
                    Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_VALUE)
                End If
                Me.PopulateBOProperty(TheCoverageDeductible, "Deductible", CType(Me.GetSelectedGridControl(dedGridView, DEDUCTIBLE), TextBox).Text)
            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
#End Region

#End Region

#Region "State-Management"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Dim retType As New CoverageSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                Me.State.moCoverageId, Me.State.boChanged)
            If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then

                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    PopulateBOsFromForm()
                    TheCoverage.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.State.boChanged = True
                        retType = New CoverageSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete,
                                        Me.State.moCoverageId)
                        retType.BoChanged = True
                        Me.ReturnToCallingPage(retType)
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(retType)
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ' Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        Me.CreateNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ' Me.moErrorController.AddErrorAndShow(Me.State.LastErrMsg)
                        Me.MasterPage.MessageController.AddError(Me.State.LastErrMsg, True)
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(retType)
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(retType)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub
        Protected Sub CheckIfComingFromConfirm()
            ' ComingFromBack()
            CheckIfComingFromSaveConfirm()
            'Clean after consuming the action
            Me.HiddenSaveChangesPromptResponse.Value = String.Empty
        End Sub


        Protected Sub moddl_DeductibleBasedOn_SelectedIndexChanged(sender As Object, e As EventArgs)
            Dim moddl_DeductibleBasedOn As DropDownList = DirectCast(sender, DropDownList)
            Dim dedid As Guid = GetSelectedItem((moddl_DeductibleBasedOn))
            Dim deductiblecode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, dedid)

            Dim row As System.Web.UI.WebControls.GridViewRow = DirectCast(moddl_DeductibleBasedOn.NamingContainer, System.Web.UI.WebControls.GridViewRow)
            Dim motxt_Deductible As TextBox = DirectCast(dedGridView.Rows()(row.RowIndex).FindControl("motxt_Deductible"), TextBox)
            If Not ((deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__FIXED) OrElse
                                           (deductiblecode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then

                motxt_Deductible.Text = "0"
                motxt_Deductible.Enabled = False
                motxt_Deductible.Style.Add("background", "#F0F0F0")

            Else
                motxt_Deductible.Enabled = True

            End If

        End Sub

        Private Sub moReInsuredDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moReInsuredDrop.SelectedIndexChanged

            If Me.State.IsCoverageNew = True Then
                If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) OrElse GetSelectedItem(Me.moReInsuredDrop).Equals(Guid.Empty) Then
                    DisabledTabsList.Add(Tab_ATTRIBUTES)
                    AttributeValues.Visible = False
                    If Not TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                        Dim attributevaluebo As AttributeValue = TheCoverage.AttributeValues.First
                        attributevaluebo.Delete()
                        attributevaluebo.Save()
                        attributevaluebo = Nothing
                    End If
                Else
                    AttributeValues.Visible = True
                End If
            Else
                If GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, NO) OrElse GetSelectedItem(Me.moReInsuredDrop).Equals(Guid.Empty) Then
                    DisabledTabsList.Add(Tab_ATTRIBUTES)
                    AttributeValues.Visible = False

                    If Not TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                        Dim attributevaluebo As AttributeValue = TheCoverage.AttributeValues.First
                        attributevaluebo.Delete()
                        attributevaluebo.Save()
                        attributevaluebo = Nothing
                        AttributeValues.DataBind()
                    End If
                ElseIf GetSelectedItem(Me.moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, YES) Then
                    TheCoverage.ClearAttributeValues()
                    Dim cav As IEnumerable(Of AttributeValue) = TheCoverage.AttributeValues
                    For Each av As AttributeValue In cav
                    Next
                    AttributeValues.Visible = True
                    AttributeValues.DataBind()
                End If
            End If
        End Sub


#End Region

#Region "ConseqDamage"

#Region "Handlers-ConseqDamage-Buttons"

        Private Sub SaveConseqDamageChanges()
            If ApplyConseqDamageChanges() = True Then
                If IsNewConseqDamage = True Then
                    IsNewConseqDamage = False
                End If
                PopulateCoverageConseqDamageList(ACTION_SAVE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
            End If
        End Sub

        Private Sub btnSaveConseqDamage_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveConseqDamage_WRITE.Click
            Try
                SaveConseqDamageChanges()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerConseqDamage)
            End Try
        End Sub

        Private Sub btnCancelConseqDamage_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelConseqDamage_WRITE.Click
            Try
                IsNewConseqDamage = False
                EnableForEditConseqDamageButtons(False)
                PopulateCoverageConseqDamageList(ACTION_CANCEL_DELETE)
                EnableDisableControls(Me.moCoverageEditPanel, False)
                setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerConseqDamage)
            End Try
        End Sub

        Private Sub btnNewConseqDamage_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewConseqDamage_WRITE.Click
            Try
                IsNewConseqDamage = True
                CoverageConseqDamageId = Guid.Empty.ToString
                PopulateCoverageConseqDamageList(ACTION_NEW)
                SetGridControls(moGridViewConseqDamage, False)
                Me.SetFocusInGrid(moGridViewConseqDamage, moGridViewConseqDamage.SelectedIndex, LIABLILITY_LIMIT_PER_INCIDENT)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                If btnNewConseqDamage_WRITE.Enabled = False Then
                    setbuttons(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moMsgControllerConseqDamage)
            End Try
        End Sub
#End Region

#Region "ConseqDamage Button Management"

        Private Sub EnableEditConseqDamageButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, btnSaveConseqDamage_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, btnCancelConseqDamage_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnableNewConseqDamageButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewConseqDamage_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnableConseqDamageButtons(ByVal bIsReadWrite As Boolean)
            EnableNewConseqDamageButtons(bIsReadWrite)
            EnableEditConseqDamageButtons(bIsReadWrite)
        End Sub

        Private Sub EnableForEditConseqDamageButtons(ByVal bIsReadWrite As Boolean)
            EnableNewConseqDamageButtons(Not bIsReadWrite)
            EnableEditConseqDamageButtons(bIsReadWrite)
        End Sub

        Private Sub EnableCoverageConseqDamage(ByVal bIsReadWrite As Boolean)
            moGridViewConseqDamage.Columns(CONSEQ_DAMAGE_TYPE).Visible = bIsReadWrite
            moGridViewConseqDamage.Columns(LIABILITY_LIMIT_BASED_ON).Visible = bIsReadWrite
            moGridViewConseqDamage.Columns(LIABLILITY_LIMIT_PER_INCIDENT).Visible = bIsReadWrite
            moGridViewConseqDamage.Columns(LIABLILITY_LIMIT_CUMULATIVE).Visible = bIsReadWrite
            moGridViewConseqDamage.Columns(CONSEQ_DAMAGE_EFFECTIVE_DATE).Visible = bIsReadWrite
            moGridViewConseqDamage.Columns(FULFILMENT_METHOD).Visible = bIsReadWrite
        End Sub
#End Region

#Region "Business Part"

        Private Function ApplyConseqDamageChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridViewConseqDamage.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode
            If Me.State.IsNewWithCopy Then
                Me.LoadCoverageConseqDamageList()
                Me.State.moCoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewConseqDamage = False Then
                CoverageConseqDamageId = Me.GetSelectedGridText(moGridViewConseqDamage, COVERAGE_CONSEQ_DAMAGE_ID)
            End If
            BindBoPropertiesToConseqDamageGridHeader()
            With TheCoverageConseqDamage
                PopulateConseqDamageBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditConseqDamageButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function

        Private Sub PopulateConseqDamageBOFromForm()
            With TheCoverageConseqDamage
                .CoverageId = TheCoverage.Id
                Me.PopulateBOProperty(TheCoverageConseqDamage, CONSEQ_DAMAGE_TYPE_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, CONSEQ_DAMAGE_TYPE), DropDownList), False, True)
                Me.PopulateBOProperty(TheCoverageConseqDamage, LIABILILTY_LIMIT_BASED_ON_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, LIABILITY_LIMIT_BASED_ON), DropDownList), False, True)
                Me.PopulateBOProperty(TheCoverageConseqDamage, FULFILMENT_METHOD_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, FULFILMENT_METHOD), DropDownList), False, True)
                Me.PopulateBOProperty(TheCoverageConseqDamage, CONSEQ_DAMAGE_EFFECTIVE_DATE_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, CONSEQ_DAMAGE_EFFECTIVE_DATE), TextBox))
                Me.PopulateBOProperty(TheCoverageConseqDamage, CONSEQ_DAMAGE_EXPIRATION_DATE_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, CONSEQ_DAMAGE_EXPIRATION_DATE), TextBox))

                If Not String.IsNullOrEmpty(CType(Me.GetSelectedGridControl(moGridViewConseqDamage, LIABLILITY_LIMIT_CUMULATIVE), TextBox).Text) Then
                    Me.PopulateBOProperty(TheCoverageConseqDamage, LIABILILTY_LIMIT_CUMULATIVE_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, LIABLILITY_LIMIT_CUMULATIVE), TextBox))
                Else
                    .LiabilityLimitCumulative = 0.00
                End If

                If Not String.IsNullOrEmpty(CType(Me.GetSelectedGridControl(moGridViewConseqDamage, LIABLILITY_LIMIT_PER_INCIDENT), TextBox).Text) Then
                    Me.PopulateBOProperty(TheCoverageConseqDamage, LIABILILTY_LIMIT_PER_INCIDENT_PROPERTY, CType(Me.GetSelectedGridControl(moGridViewConseqDamage, LIABLILITY_LIMIT_PER_INCIDENT), TextBox))
                Else
                    .LiabilityLimitPerIncident = 0.00
                End If
            End With
            'ValidateCoverage()
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EnableTabsCoverageConseqDamage()
            Try
                If (TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX) Then
                    Dim oDealerId As Guid = TheDealerControl.SelectedGuid
                    If Not (oDealerId = Guid.Empty) Then
                        Dim oDealer As Dealer = New Dealer(oDealerId)
                        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                        If oDealer IsNot Nothing AndAlso oDealer.ClaimRecordingXcd IsNot Nothing Then
                            If (oDealer.UseClaimAuthorizationId.Equals(YesId) AndAlso
                            (oDealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse
                            oDealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH))) Then
                                DisabledTabsList.Remove(Tab_CoverageConseqDamage)
                            Else
                                If Not (DisabledTabsList.Contains(Tab_CoverageConseqDamage)) Then
                                    DisabledTabsList.Add(Tab_CoverageConseqDamage)
                                End If
                            End If
                        Else
                            If Not (DisabledTabsList.Contains(Tab_CoverageConseqDamage)) Then
                                DisabledTabsList.Add(Tab_CoverageConseqDamage)
                            End If
                        End If
                    Else
                        If Not (DisabledTabsList.Contains(Tab_CoverageConseqDamage)) Then
                            DisabledTabsList.Add(Tab_CoverageConseqDamage)
                        End If
                    End If
                Else
                    If Not (DisabledTabsList.Contains(Tab_CoverageConseqDamage)) Then
                        DisabledTabsList.Add(Tab_CoverageConseqDamage)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Clear"
        Private Sub ClearCoverageConseqDamage()
            If Not Me.State.IsNewWithCopy Then
                EnableConseqDamageButtons(False)
                moGridViewConseqDamage.DataBind()
            End If
        End Sub
#End Region


#End Region



    End Class
End Namespace
