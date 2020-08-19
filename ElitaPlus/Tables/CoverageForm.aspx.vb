Imports System.Collections.Generic
Imports System.Globalization
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic

Namespace Tables

    Partial Class CoverageForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState

            Public CoverageId As Guid = Guid.Empty
            Public IsCoverageNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public Coverage As Coverage
            Public CoverageRateList() As CoverageRate
            Public CoverageDeductibleList() As CoverageDeductible
            Public CoverageConseqDamageList() As CoverageConseqDamage
            Public SelectedCoverageTypeId As Guid
            Public SelectedProductItemId As Guid
            Public SelectedItemId As Guid
            Public SelectedOptionalId As Guid
            Public SelectedIsClaimAllowedId As Guid
            Public SelectedUseCoverageStartDateId As Guid
            Public SelectedEffective As String
            Public SelectedExpiration As String
            Public SelectedLiability As String
            Public SelectedLiabilityLimitPercent As String
            Public SelectedCertificateDuration As String
            Public SelectedCoverageDuration As String
            '            Public selectedOffsetMethodId As Guid
            Public SelectedOffsetMethod As String
            Public SelectedMarkupDistnPercent As String
            Public SelectedOffset As String
            Public SelectedOffsetDays As String
            Public SelectedDeductible As String
            Public SelectedCovDeductible As String
            Public SelectedEarningCodeId As Guid
            Public SelectedDeductiblePercent As String
            Public SelectedRepairDiscountPct As String
            Public SelectedReplacementDiscountPct As String
            Public SelectedAgentCode As String
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public StateChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public SelectedCoverageLiabilityLimit As String
            Public SelectedCoverageLiabilityLimitPercent As String
            Public SelectedRecoverDeviceId As Guid
            Public SelectedClaimLimitCount As String
            Public SelectedPerIncidentLiabilityLimitCap As String
            Public SelectedTaxTypeXcd As String

            'US 521697
            Public IsDiffSelectedTwice As Boolean
            Public IsDiffNotSelectedOnce As Boolean
            Public IsBucketIncomingSelected As Boolean
            Public IsDealerConfiguredForSourceXcd As Boolean = False
            Public IsIgnorePremiumSetYesForContract As Boolean = False
            Public IsProductConfiguredForRenewalNo As Boolean = False

            'US 489838
            Public IsRateLimitAndPercentBothPresent As Boolean
            Public IsRateRenewalNoNotInSequence As Boolean
            Public IsRateFirstRenewalNoIsNotZero As Boolean
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
            State.CoverageId = CType(CallingParameters, Guid)
            If State.CoverageId.Equals(Guid.Empty) Then
                State.IsCoverageNew = True
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheCoverage)
                ClearAll()
                EnableCoverageButtons(False)
                PopulateAll()
                moLiabilityLimitPercentText.Text = OneHundredNumber
                moCoverageLiabilityLimitText.Text = ZeroNumber
                moCoverageLiabilityLimitPercentText.Text = String.Empty
                moPerIncidentLiabilityLimitCapText.Text = ZeroNumber
            Else
                State.IsCoverageNew = False
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheCoverage)
                EnableCoverageButtons(True)
                PopulateAll()
            End If
            LoadCurrencyOfCoverage()
            CoverageMarkupDistribution()
        End Sub

#End Region

#Region "Constants"
        Const OneHundredNumber As String = "100"
        Const ZeroNumber As String = "0"
        Private Const NoCoveragePricing As String = "N"
        Private Const FirstPos As Integer = 0
        Private Const ColDescriptionName As String = "DESCRIPTION"
        Private Const ColCodeName As String = "CODE"
        Public Const CoverageFormUrl As String = "CoverageForm.aspx"
        Private Const CoverageForm002 As String = "COVERAGE_FORM002" ' Coverage Field Exception
        Private Const CoverageForm003 As String = "COVERAGE_FORM003" ' Coverage Update Exception
        Private Const CoverageForm004 As String = "COVERAGE_FORM004" ' Coverage-Rate List Exception
        Private Const CoverageForm005 As String = "COVERAGE_FORM005" ' Coverage-Rate Update Exception
        Private Const CoverageForm006 As String = "COVERAGE_FORM006" ' Coverage-Deductible Update Exception
        Protected Const ConfirmMsg As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const LabelSelectDealerCode As String = "Dealer"
        Private Const LookupNo As String = "N"
        Private Const LookupYes As String = "Y"
        Private Const LabelCoverage As String = "COVERAGE"
        Private Const ProdLiabBasedOnNotApp As String = "NOTAPPL"
        Private Const PosTaxTypeXcd As String = "TTYP-1"
        Public Const ConfigurationSuperUserRole As String = "CONSU"

#End Region

#Region "Tabs"
        Public Const TabCoverageRate As String = "0"
        Public Const TabDeductible As String = "1"
        Public Const TabAttributes As String = "2"
        Public Const TabCoverageConseqDamage As String = "3"

        Dim DisabledTabsList As New List(Of String)()
#End Region
#Region "Coverage-Rate Constants"
        Private Const ColIndexCoverageRateId As Integer = 2
        Private Const ColIndexLowPrice As Integer = 3
        Private Const ColIndexHighPrice As Integer = 4
        Private Const ColIndexGrossAmt As Integer = 5
        Private Const ColIndexCommissionsPercent As Integer = 6
        Private Const ColIndexCommissionsPercentXcd As Integer = 7
        Private Const ColIndexMarketingPercent As Integer = 8
        Private Const ColIndexMarketingPercentXcd As Integer = 9
        Private Const ColIndexAdminExpense As Integer = 10
        Private Const ColIndexAdminExpenseXcd As Integer = 11
        Private Const ColIndexProfitExpense As Integer = 12
        Private Const ColIndexProfitExpenseXcd As Integer = 13
        Private Const ColIndexLossCostPercent As Integer = 14
        Private Const ColIndexLossCostPercentXcd As Integer = 15
        Private Const ColIndexGrossAmountPercent As Integer = 16
        Private Const ColIndexRenewalNumber As Integer = 17
        Private Const ColIndexRegionId As Integer = 18
        Private Const ColIndexCovLiabilityLimit As Integer = 19
        Private Const ColIndexCovLiabilityLimitPercent As Integer = 20

        ' DataView Elements
        Private Const DbCoverageRateId As Integer = 0

        'Actions
        Private Const ActionNone As String = "ACTION_NONE"
        Private Const ActionSave As String = "ACTION_SAVE"
        Private Const ActionCancelDelete As String = "ACTION_CANCEL_DELETE"
        Private Const ActionEdit As String = "ACTION_EDIT"
        Private Const ActionNew As String = "ACTION_NEW"

#End Region

#Region "Page Return Type"

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Coverage
            Public HasDataChanged As Boolean
            Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As Coverage, ByVal dataChanged As Boolean)
                LastOperation = lastOp
                EditingBo = curEditingBo
                HasDataChanged = dataChanged
            End Sub
        End Class

#End Region

#Region "Attributes"
        Private _moCoverageRate As CoverageRate
        Private _moCoverageDeductible As CoverageDeductible
        Private _moCoverageConseqDamage As CoverageConseqDamage
        Private _moDepreciationScdRelation As DepreciationScdRelation
#End Region

#Region "Properties"

        Private ReadOnly Property TheCoverage() As Coverage
            Get

                If State.Coverage Is Nothing Then
                    If State.IsCoverageNew = True Then
                        ' For creating, inserting
                        State.Coverage = New Coverage
                        State.CoverageId = State.Coverage.Id
                    Else
                        ' For updating, deleting
                        State.Coverage = New Coverage(State.CoverageId)
                    End If
                End If

                Return State.Coverage
            End Get
        End Property

        Private ReadOnly Property CoveragePricingCode() As String
            Get
                Dim sCoveragePricingCode As String
                Dim oLanguageId As Guid = GetLanguageId()
                Dim oProductId As Guid = GetSelectedItem(moProductDrop)
                Dim oPriceMatrixView As DataView = LookupListNew.GetPriceMatrixLookupList(oProductId, oLanguageId)
                If oPriceMatrixView.Count > 0 Then
                    sCoveragePricingCode = oPriceMatrixView.Item(FirstPos).Item(ColCodeName).ToString
                End If
                Return sCoveragePricingCode
            End Get
        End Property

        Private ReadOnly Property HasDealerConfigeredForSourceXcd() As Boolean
            Get
                Dim isDealerConfiguredForSourceXcd As Boolean

                If (State.Coverage.DealerId <> Guid.Empty) Then
                    Dim oDealer As New Dealer(State.Coverage.DealerId)

                    If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                        If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                            isDealerConfiguredForSourceXcd = True
                        Else
                            isDealerConfiguredForSourceXcd = False
                        End If
                    Else
                        isDealerConfiguredForSourceXcd = False
                    End If
                Else
                    isDealerConfiguredForSourceXcd = False
                End If
                Return isDealerConfiguredForSourceXcd
            End Get
        End Property

        Private ReadOnly Property HasIgnorePremiumSetForContractForSIncomingSource() As Boolean
            Get
                Dim isIgnorePremiumYesForContract As Boolean
                With State.Coverage
                    If (.DealerId <> Guid.Empty) Then
                        Dim oContract As Contract = New Contract

                        If Not (.Effective Is Nothing And .Expiration Is Nothing) Then
                            oContract = Contract.GetContract(.DealerId, .Effective.Value, .Expiration.Value)
                        End If

                        If Not oContract Is Nothing Then
                            If oContract.IgnoreIncomingPremiumID = Guid.Empty Then
                                Dim str As String
                                str = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IgnoreIncomingPremiumID)

                                If str.Equals(Codes.YESNO_Y) Then
                                    isIgnorePremiumYesForContract = True
                                Else
                                    isIgnorePremiumYesForContract = False
                                End If

                            Else
                                isIgnorePremiumYesForContract = False
                            End If
                        Else
                            isIgnorePremiumYesForContract = False
                        End If
                    Else
                        isIgnorePremiumYesForContract = False
                    End If
                End With
                Return isIgnorePremiumYesForContract
            End Get
        End Property

        Private ReadOnly Property HasProductConfiguredForSequentialNoValidation() As Boolean
            Get
                Dim isProductConfiguredForSequentialNoValidation As Boolean = False

                If Not oProduct Is Nothing AndAlso oProduct.AttributeValues.Contains(Codes.SEQUENTIAL_RENEWAL_NUMBER_VALIDATION) Then
                    If oProduct.AttributeValues.Value(Codes.SEQUENTIAL_RENEWAL_NUMBER_VALIDATION) = Codes.YESNO_Y Then
                        isProductConfiguredForSequentialNoValidation = True
                    End If
                End If

                Return isProductConfiguredForSequentialNoValidation
            End Get
        End Property
        Private _Product As ProductCode
        Public ReadOnly Property oProduct() As ProductCode
            Get
                If _Product Is Nothing Then
                    _Product = New ProductCode(State.Coverage.ProductCodeId)
                End If
                Return _Product
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

                If _moCoverageRate Is Nothing Then
                    If IsNewRate = True Then
                        ' For creating, inserting
                        _moCoverageRate = New CoverageRate
                        CoverageRateId = _moCoverageRate.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageRateId = "" Then
                            CoverageRateId = Guid.Empty.ToString
                        End If
                        _moCoverageRate = New CoverageRate(GetGuidFromString(CoverageRateId))
                    End If
                End If

                Return _moCoverageRate
            End Get

        End Property


        Private Property CoverageRateId() As String
            Get
                Return moCoverageRateIdLabel.Text
            End Get
            Set(ByVal value As String)
                moCoverageRateIdLabel.Text = value
            End Set
        End Property

        Private Property IsNewRate() As Boolean
            Get
                Return Convert.ToBoolean(moIsNewRateLabel.Text)
            End Get
            Set(ByVal value As Boolean)
                moIsNewRateLabel.Text = value.ToString
            End Set
        End Property


#End Region

#Region "Coverage Deductible Constants"

        'TABLE COLUMNS
        Public Const TableKeyName As String = "coverage_ded_id"
        Public Const CoverageDedIdProperty As String = "coverage_ded_id"
        Public Const CoverageIdProperty As String = "coverage_id"

        'Grid columns sequence
        Private Const ColSeqCoverageDeductibleId As Integer = 2
        Private Const ColSeqMethodOfRepairId As Integer = 3
        Private Const ColSeqDeductibleBasedOnId As Integer = 4
        Private Const ColSeqMethodOfRepairDescription As Integer = 5
        Private Const ColSeqDeductibleBasedOnDesc As Integer = 6
        Private Const ColSeqDeductible As Integer = 7

        ' DataView Elements
        Private Const DbCoverageDeductibleId As Integer = 0
        Private Const DbMethodOfRepairId As Integer = 2
        Private Const DbDeductibleBasedOn As Integer = 3

        'name of drop downlist
        Private Const DeductibleBasedOnDropDownList As String = "moddl_DeductibleBasedOn"
        Private Const MethodOfRepairDropDownList As String = "moddl_MethodOfRepair"
        Private Const DeductibleText As String = "motxt_Deductible"

#End Region

#Region "coverage Deductible Properties"

        Private ReadOnly Property TheCoverageDeductible() As CoverageDeductible
            Get

                If _moCoverageDeductible Is Nothing Then
                    If IsNewDeductible = True Then
                        ' For creating, inserting
                        _moCoverageDeductible = New CoverageDeductible
                        CoverageDeductibleId = _moCoverageDeductible.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageDeductibleId = "" Then
                            CoverageDeductibleId = Guid.Empty.ToString
                        End If
                        _moCoverageDeductible = New CoverageDeductible(GetGuidFromString(CoverageDeductibleId))
                    End If
                End If

                Return _moCoverageDeductible
            End Get

        End Property

        Private Property CoverageDeductibleId() As String
            Get
                Return moCoverageDeductibleIdLabel.Text
            End Get
            Set(ByVal value As String)
                moCoverageDeductibleIdLabel.Text = value
            End Set
        End Property

        Private Property IsNewDeductible() As Boolean
            Get
                Return Convert.ToBoolean(IsNewDeductibleLabel.Text)
            End Get
            Set(ByVal value As Boolean)
                IsNewDeductibleLabel.Text = value.ToString
            End Set
        End Property
#End Region
#Region "Coverage Conseq Damage Constants"


        'Grid columns sequence
        Private Const ColSeqCoverageConseqDamageId As Integer = 2
        Private Const ColSeqConseqDamageType As Integer = 3
        Private Const ColSeqLiabilityLimitBasedOn As Integer = 4
        Private Const ColSeqLiabilityLimitPerIncident As Integer = 5
        Private Const ColSeqLiabilityLimitCumulative As Integer = 6
        Private Const ColSeqConseqDamageEffectiveDate As Integer = 7
        Private Const ColSeqConseqDamageExpirationDate As Integer = 8
        Private Const ColSeqFulfillmentMethod As Integer = 9
        Private Const ColSeqConseqDamageTypeXcd As Integer = 10
        Private Const ColSeqLiabilityLimitBasedOnXcd As Integer = 11
        Private Const ColSeqFulfillmentMethodXcd As Integer = 12

        ' DataView Elements
        Private Const DbCoverageConseqDamageId As Integer = 0
        Private Const DbCoverageId As Integer = 1
        Private Const DbLiablilityLimitPerIncident As Integer = 6
        Private Const DbLiablilityLimitCumulative As Integer = 7

        Private Const ConseqDamageEffectiveDateText As String = "moConseqDamageEffectiveDateText"
        Private Const ConseqDamageEffectiveDateButton As String = "btnConseqDamageEffectiveDate"
        Private Const ConseqDamageExpirationDateText As String = "moConseqDamageExpirationDateText"
        Private Const ConseqDamageExpirationDateButton As String = "btnConseqDamageExpirationDate"





#End Region
#Region "coverage Conseq Damage Properties"

        Private ReadOnly Property TheCoverageConseqDamage() As CoverageConseqDamage
            Get

                If _moCoverageConseqDamage Is Nothing Then
                    If IsNewConseqDamage = True Then
                        ' For creating, inserting
                        _moCoverageConseqDamage = New CoverageConseqDamage
                        CoverageConseqDamageId = _moCoverageConseqDamage.Id.ToString
                    Else
                        ' For updating, deleting
                        If CoverageConseqDamageId = "" Then
                            CoverageConseqDamageId = Guid.Empty.ToString
                        End If
                        _moCoverageConseqDamage = New CoverageConseqDamage(GetGuidFromString(CoverageConseqDamageId))
                    End If
                End If

                Return _moCoverageConseqDamage
            End Get

        End Property

        Private Property CoverageConseqDamageId() As String
            Get
                Return moCoverageConseqDamageIdLabel.Text
            End Get
            Set(ByVal value As String)
                moCoverageConseqDamageIdLabel.Text = value
            End Set
        End Property

        Private Property IsNewConseqDamage() As Boolean
            Get
                Return Convert.ToBoolean(moIsNewCoverageConseqDamageLabel.Text)
            End Get
            Set(ByVal value As Boolean)
                moIsNewCoverageConseqDamageLabel.Text = value.ToString
            End Set
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init, page events"

        Protected WithEvents moCoverageEditPanel As Panel
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try

                'Preserve values during Post Backs
                PreserveValues()
                moMsgControllerRate.Clear()
                MasterPage.MessageController.Clear_Hide()
                moMsgControllerDeductible.Clear()
                moMsgControllerConseqDamage.Clear()

                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LabelCoverage)
                UpdateBreadCrum()
                ClearLabelsErrSign()
                ClearGridHeaders(moGridView)
                ClearGridHeaders(dedGridView)
                ClearGridHeaders(moGridViewConseqDamage)

                If Not Page.IsPostBack Then
                    SetGridItemStyleColor(moGridView)
                    SetGridItemStyleColor(dedGridView)
                    SetGridItemStyleColor(moGridViewConseqDamage)
                    TranslateGridHeader(moGridView)
                    TranslateGridHeader(dedGridView)
                    TranslateGridHeader(moGridViewConseqDamage)
                    TranslateGridControls(moGridView)
                    TranslateGridControls(dedGridView)
                    TranslateGridControls(moGridViewConseqDamage)

                    SetStateProperties()
                    AddCalendar(BtnEffectiveDate, moEffectiveText)
                    AddCalendar(BtnExpirationDate, moExpirationText)
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
                moUseCoverageStartDateLable.ForeColor = moReplacementDiscountPrcLabel.ForeColor

                'US-521697
                If Not IsPostBack Then
                    State.IsDealerConfiguredForSourceXcd = HasDealerConfigeredForSourceXcd()
                    State.IsIgnorePremiumSetYesForContract = HasIgnorePremiumSetForContractForSIncomingSource()
                    SetSequenceFlag()
                Else
                    SetGridSourceXcdTextBoxFromBo()
                End If

                DisplayHideCovLiabilityColumn()
                DisplayHideSourceColumn()
                SetGridSourceXcdLabelFromBo()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Private Sub SetSequenceFlag()
            State.IsProductConfiguredForRenewalNo = HasProductConfiguredForSequentialNoValidation()
        End Sub
        Private Sub GetDisabledTabs()
            Dim disabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If disabledTabs.Length > 0 AndAlso disabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(disabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub
        Private Sub UpdateBreadCrum()
            If (Not State Is Nothing) Then
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(LabelCoverage)
            End If
        End Sub

        Private Sub CoverageForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            If State.IsNewWithCopy Then
                hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
                Exit Sub
            End If

            If State.Coverage.Inuseflag = "Y" Then ' The coverage record is in use and should not allow changes except Configuration Super User Roles
                'Display a warning of this record is in use when opening the page first time
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.AddWarning("RECORD_IN_USE")
                End If

                If ElitaPlusPrincipal.Current.IsInRole(ConfigurationSuperUserRole) = False Then
                    'diable the save button to prevent any change to the coverage record
                    btnApply_WRITE.Enabled = False
                    btnDelete_WRITE.Enabled = False
                End If
            End If
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region

#Region "Handlers-DropDown"
        Private Sub cboDeductibleBasedOn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboDeductibleBasedOn.SelectedIndexChanged
            Try
                EnableDisableDeductible(GetSelectedItem(cboDeductibleBasedOn), True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moProductDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moProductDrop.SelectedIndexChanged
            Try
                ClearForProduct()
                If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateRiskType()
                    EnableDisableCoverageLiabilityLimits()
                End If
                If State.IsCoverageNew = True Then
                    TheCoverage.ClearAttributeValues()
                    Dim reInsStatusDataView As DataView = LookupListNew.GetReInsStatusesWithoutPartialStatuesLookupList(GetLanguageId(), False)
                    'Dim oYesNoDataView As DataView = LookupListNew.GetYesNoLookupList(oLanguageId)
                    Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    Dim oProductCode As New ProductCode(New Guid(moProductDrop.SelectedValue))
                    If oProductCode.IsReInsuredId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupYes) Then
                        'BindListControlToDataView(moReInsuredDrop, oYesNoDataView, , , True)
                        moReInsuredDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                        BindSelectItem(oProductCode.IsReInsuredId.ToString, moReInsuredDrop)

                        Dim attributeValueBo As AttributeValue = TheCoverage.AttributeValues.GetNewAttributeChild()
                        Dim productAttributeValue As String = oProductCode.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS)

                        attributeValueBo.Value = LookupListNew.GetCodeFromId(reInsStatusDataView, LookupListNew.GetIdFromCode(reInsStatusDataView, productAttributeValue))
                        attributeValueBo.AttributeId = TheCoverage.AttributeValues.Attribues.FirstOrDefault(Function(a) a.UiProgCode = Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS).Id
                        Dim avl As New List(Of AttributeValue)
                        avl.Add(attributeValueBo)
                        '
                        AttributeValues.PopulateAttributeValuesGrid(avl)
                        attributeValueBo.EndEdit()
                        attributeValueBo.Save()
                        attributeValueBo = Nothing
                        AttributeValues.Visible = True

                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moRiskDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moRiskDrop.SelectedIndexChanged
            Try
                ClearForRisk()
                If moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateCoverageType()
                End If
                PopulateItemNumber()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCoverageTypeDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moCoverageTypeDrop.SelectedIndexChanged
            Try
                ClearForCoverageType()
                If moCoverageTypeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateRestCoverage()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PreserveValues()
            State.SelectedCoverageTypeId = GetSelectedItem(moCoverageTypeDrop)
            State.SelectedProductItemId = GetSelectedItem(moProductItemDrop)
            State.SelectedOptionalId = GetSelectedItem(moOptionalDrop)
            State.SelectedIsClaimAllowedId = GetSelectedItem(moIsClaimAllowedDrop)
            State.SelectedUseCoverageStartDateId = GetSelectedItem(UseCoverageStartDateId)
            State.SelectedOffsetMethod = GetSelectedValue(moOffsetMethodDrop)
            State.SelectedOffset = moOffsetText.Text
            State.SelectedMarkupDistnPercent = txtMarkupDistPercent.Text
            State.SelectedOffsetDays = txtOffsetDays.Text
            State.SelectedEffective = moEffectiveText.Text
            State.SelectedExpiration = moExpirationText.Text
            State.SelectedCertificateDuration = moCertificateDurationText.Text
            State.SelectedCoverageDuration = moCoverageDurationText.Text
            State.SelectedLiability = moLiabilityText.Text
            State.SelectedLiabilityLimitPercent = moLiabilityLimitPercentText.Text
            State.SelectedDeductible = moDeductibleText.Text
            State.SelectedDeductiblePercent = moDeductiblePercentText.Text
            State.SelectedEarningCodeId = GetSelectedItem(moEarningCodeDrop)
            State.SelectedCovDeductible = moCovDeductibleText.Text
            State.SelectedRepairDiscountPct = moRepairDiscountPctText.Text
            State.SelectedReplacementDiscountPct = moReplacementDiscountPctText.Text
            State.SelectedAgentCode = moAgentcodeText.Text
            State.SelectedCoverageLiabilityLimit = moCoverageLiabilityLimitText.Text
            State.SelectedCoverageLiabilityLimitPercent = moCoverageLiabilityLimitPercentText.Text
            State.SelectedRecoverDeviceId = GetSelectedItem(moRecoverDeciveDrop)
            State.SelectedClaimLimitCount = moClaimLimitCountText.Text
            State.SelectedPerIncidentLiabilityLimitCap = moPerIncidentLiabilityLimitCapText.Text
            State.SelectedTaxTypeXcd = GetSelectedValue(moTaxTypeDrop)

        End Sub


#End Region

#Region "Handlers-TextBox"

        Private Sub moEffectiveText_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moEffectiveText.TextChanged
            EnableDisableDeductible(GetSelectedItem(cboDeductibleBasedOn), True)
        End Sub

        Private Sub EnableDisableDeductible(ByVal pDeductibleBasedOnId As Guid, ByVal pClearValues As Boolean)
            Dim sCoverageDeductibleCode As String
            If (TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX) And (Len(moEffectiveText.Text) > 0) Then
                Dim oDealerId As Guid = TheDealerControl.SelectedGuid
                Dim oEffectiveDate As String
                Dim tempDate As Date
                tempDate = DateHelper.GetDateValue(moEffectiveText.Text)
                oEffectiveDate = tempDate.ToString("yyyyMMdd")
                Try
                    Dim oCoverageDeductibleView As DataView = Coverage.GetCoverageDeductable(oDealerId, oEffectiveDate, GetLanguageId())
                    If oCoverageDeductibleView.Count > 0 Then
                        moCovDeductibleText.Text = oCoverageDeductibleView.Item(FirstPos).Item(ColDescriptionName).ToString
                    Else
                        moCovDeductibleText.Text = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_YESNO, LookupNo)
                    End If
                    sCoverageDeductibleCode = moCovDeductibleText.Text
                Catch ex As Exception
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
                End Try
            Else
                sCoverageDeductibleCode = Nothing
            End If

            sCoverageDeductibleCode = LookupListNew.GetCodeFromDescription(LookupListNew.DropdownLookupList("YESNO", GetLanguageId(), True), sCoverageDeductibleCode)

            Select Case sCoverageDeductibleCode
                Case LookupYes
                    ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, True)
                    Dim sDeductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, pDeductibleBasedOnId)

                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                        ' OrElse  (String.IsNullOrWhiteSpace(sDeductibleBasedOnCode))) Then
                        ControlMgr.SetEnableControl(Me, moDeductibleText, True)
                        ControlMgr.SetEnableControl(Me, moDeductiblePercentText, False)
                        If (pClearValues) Then
                            moDeductiblePercentText.Text = ZeroNumber
                        End If
                    ElseIf ((sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                           (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then
                        ControlMgr.SetEnableControl(Me, moDeductibleText, False)
                        ControlMgr.SetEnableControl(Me, moDeductiblePercentText, True)
                        If (pClearValues) Then
                            moDeductibleText.Text = ZeroNumber
                        End If

                    Else
                        ControlMgr.SetEnableControl(Me, moDeductibleText, False)
                        ControlMgr.SetEnableControl(Me, moDeductiblePercentText, False)
                        ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, True)
                        If (pClearValues) Then
                            moDeductibleText.Text = ZeroNumber
                            moDeductiblePercentText.Text = ZeroNumber
                        End If

                    End If
                Case Else
                    ControlMgr.SetEnableControl(Me, moDeductibleText, False)
                    ControlMgr.SetEnableControl(Me, moDeductiblePercentText, False)
                    ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, False)
                    If (pClearValues) Then
                        moDeductibleText.Text = ZeroNumber
                        moDeductiblePercentText.Text = ZeroNumber
                        SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                    End If
            End Select

        End Sub
#End Region


#Region "Handlers-Buttons"

        Private Sub SaveChanges()
            If ApplyChanges() = True Then
                State.StateChanged = True
                ClearCoverageRate()
                ClearCoverageDeductible()
                ClearCoverageConseqDamage()
                If State.IsCoverageNew = True Then
                    State.IsCoverageNew = False
                End If
                If State.IsNewWithCopy Then
                    State.IsNewWithCopy = False
                End If
                PopulateAll()
            End If
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply_WRITE.Click
            Try
                Dim sVal As String
                Dim langId As Guid = GetLanguageId()
                Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
                Dim selectedTaxType As String = GetSelectedValue(moTaxTypeDrop)
                sVal = LookupListNew.GetCodeFromDescription(yesNoLkL, moCovDeductibleText.Text)
                If sVal = LookupYes Then
                    If IsNumeric(moDeductiblePercentText.Text) And IsNumeric(moDeductibleText.Text) Then
                        If CType(moDeductiblePercentText.Text, Decimal) > 0 And CType(moDeductibleText.Text, Decimal) > 0 Then
                            'display error
                            SetLabelError(moDeductiblePercentLabel)
                            SetLabelError(moDeductibleLabel)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_AMOUNT_ERR)
                        End If
                    End If
                End If

                If (moCoverageLiabilityLimitText.Enabled And moCoverageLiabilityLimitPercentText.Enabled _
                    And moCoverageLiabilityLimitText.Visible And moCoverageLiabilityLimitPercentText.Visible) Then
                    If String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        SetLabelError(moCoverageLiabilityLimitLabel)
                        SetLabelError(moCoverageLiabilityLimitPercentLabel)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                    ElseIf Not String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitText.Text) Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT)
                        ElseIf CType(moCoverageLiabilityLimitText.Text, Decimal) < 0 Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT)
                        End If
                    ElseIf String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And Not String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_PERCENT)
                        ElseIf CType(moCoverageLiabilityLimitPercentText.Text, Decimal) = 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) < 0 Or
                                CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 100 Then
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_PERCENT)
                        End If
                    ElseIf Not String.IsNullOrEmpty(moCoverageLiabilityLimitText.Text) And Not String.IsNullOrEmpty(moCoverageLiabilityLimitPercentText.Text) Then
                        If Not IsNumeric(moCoverageLiabilityLimitText.Text) And Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf IsNumeric(moCoverageLiabilityLimitText.Text) And Not IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf Not IsNumeric(moCoverageLiabilityLimitText.Text) And IsNumeric(moCoverageLiabilityLimitPercentText.Text) Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf (CType(moCoverageLiabilityLimitPercentText.Text, Decimal) = 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) < 0 Or CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 100) Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        ElseIf CType(moCoverageLiabilityLimitText.Text, Decimal) > 0 And CType(moCoverageLiabilityLimitPercentText.Text, Decimal) > 0 Then
                            SetLabelError(moCoverageLiabilityLimitLabel)
                            SetLabelError(moCoverageLiabilityLimitPercentLabel)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_LIABILITY_LIMIT_AND_PERCENT)
                        End If
                    End If
                End If

                'Per Incident Liability Limit

                If String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                    SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                ElseIf Not String.IsNullOrEmpty(moPerIncidentLiabilityLimitCapText.Text) Then
                    If Not IsNumeric(moPerIncidentLiabilityLimitCapText.Text) Then
                        SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) < 0 Then
                        SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                        Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                    End If
                ElseIf (CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) <= 0) Then
                    SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                ElseIf CType(moPerIncidentLiabilityLimitCapText.Text, Decimal) > 0 Then
                    SetLabelError(moPerIncidentLiabilityLimitCapLabel)
                    Throw New GUIException(Message.MSG_INVALID_PER_INCIDENT_LIABILITY_LIMIT, ElitaPlus.Common.ErrorCodes.INVALID_PER_INCIDENT_LIABILITY_LIMIT)
                End If

                If Not GetSelectedItem(moReInsuredDrop).Equals(Guid.Empty) Then

                    If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupYes) Then
                        If TheCoverage.AttributeValues.Count = 0 Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_REQUIRED_WHEN_REINSURED_IS_SET_ERR)
                        End If
                        If TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                            Throw New GUIException(Message.ATTRIBUTE_VALUE_REQUIRED, ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_SHOULD_BE_SAVED_ERR)
                        End If
                    End If

                    If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupNo) Then

                        If TheCoverage.AttributeValues.Count > 0 Then
                            Throw New GUIException(Message.INVALID_ATTRIBUTE, ElitaPlus.Common.ErrorCodes.ATTRIBUTE_VALUE_CANNOT_BE_SET_WHEN_REINSURED_IS_SET_TO_NO_ERR)
                        End If
                    End If
                Else
                    If TheCoverage.AttributeValues.Count > 0 Then
                        Throw New GUIException(Message.INVALID_ATTRIBUTE, ElitaPlus.Common.ErrorCodes.CANNOT_SET_ATTRIBUTE_WITHOUT_REINSURED_FLAG)
                    End If
                End If

                If Not IsNothing(TheCoverage.DealerMarkupId) AndAlso TheCoverage.DealerMarkupId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupYes) Then
                    If Not selectedTaxType.Equals(Guid.Empty.ToString) And Not selectedTaxType.Equals(PosTaxTypeXcd) Then
                        Throw New GUIException(Message.MSG_ERR_WHEN_DEALER_MARKUP_ALLOWED_TAX_TYPE_SHOULD_BE_EMPTY_OR_POS, ElitaPlus.Common.ErrorCodes.INVALID_TAX_TYPE_FOR_DEALER)
                    End If

                End If


                ValidateCoverage()
                SaveChanges()
                SetLabelColor(moDeductiblePercentLabel)
                SetLabelColor(moDeductibleLabel)
                SetLabelColor(moIsClaimAllowedLabel)
                SetLabelColor(moAgentCodeLabel)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                'State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                'State.LastErrMsg = moErrorController.Text
            End Try
        End Sub

        Private Sub GoBack()
            Dim retType As New CoverageSearchForm.ReturnType(DetailPageCommand.Back, State.CoverageId, State.StateChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Function IsEditAllowed() As Boolean
            If State.Coverage.Inuseflag = "Y" AndAlso ElitaPlusPrincipal.Current.IsInRole(ConfigurationSuperUserRole) = False Then
                Return False
            Else
                Return True
            End If
        End Function
        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBo() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.Back
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.IsUndo = True
                If Not State.IsCoverageNew Then
                    State.Coverage = New Coverage(State.CoverageId)
                End If
                AttributeValues.ParentBusinessObject = CType(TheCoverage, IAttributable)
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            ClearForNew()
            ClearAll()
            EnableCoverageButtons(False)
            PopulateAll()
            ControlMgr.SetVisibleControl(Me, currLabelDiv, False)
            ControlMgr.SetVisibleControl(Me, currTextBoxDiv, False)
            moLiabilityLimitPercentText.Text = OneHundredNumber
            moCoverageLiabilityLimitText.Text = ZeroNumber
            moPerIncidentLiabilityLimitCapText.Text = ZeroNumber

        End Sub

        Private Sub ClearForNew()
            State.CoverageId = Guid.Empty
            State.IsCoverageNew = True
            State.Coverage = Nothing
            State.SelectedCoverageTypeId = Guid.Empty
            State.SelectedProductItemId = Guid.Empty
            State.SelectedItemId = Guid.Empty
            State.SelectedOptionalId = Guid.Empty
            State.SelectedIsClaimAllowedId = Guid.Empty
            State.SelectedUseCoverageStartDateId = Guid.Empty
            'State.selectedOffsetMethodId = Guid.Empty
            State.SelectedOffsetMethod = Nothing
            State.SelectedOffset = Nothing
            State.SelectedMarkupDistnPercent = Nothing
            State.SelectedOffsetDays = Nothing
            State.SelectedEffective = Nothing
            State.SelectedExpiration = Nothing
            State.SelectedCertificateDuration = Nothing
            State.SelectedCoverageDuration = Nothing
            State.SelectedLiability = Nothing
            State.SelectedLiabilityLimitPercent = Nothing
            State.SelectedDeductible = Nothing
            State.SelectedDeductiblePercent = Nothing
            State.SelectedCovDeductible = Nothing
            State.SelectedEarningCodeId = Guid.Empty
            State.SelectedCoverageLiabilityLimit = Nothing
            State.SelectedCoverageLiabilityLimitPercent = Nothing
            State.SelectedRecoverDeviceId = Guid.Empty
            State.SelectedClaimLimitCount = Nothing
            State.SelectedTaxTypeXcd = Nothing
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBo() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.New_
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If
                    CreateNew()
                    EnableTabsCoverageConseqDamage()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.New_
            End Try
        End Sub

        Private Sub CreateNewCopy()

            'Clear Coverage BO
            Dim newObj As New Coverage
            Dim attrVals As New List(Of AttributeValue)
            If (newObj.AttributeValues.Count() = 0 AndAlso Not String.IsNullOrEmpty(TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS))) Then
                For Each attrVal As AttributeValue In TheCoverage.AttributeValues
                    Dim attributeValueBo As AttributeValue = newObj.AttributeValues.GetNewAttributeChild()
                    attributeValueBo.Value = attrVal.Value
                    attributeValueBo.AttributeId = attrVal.AttributeId
                    attrVals.Add(attributeValueBo)
                    attributeValueBo.Save()
                Next
            End If
            State.CoverageId = newObj.Id
            State.Coverage = newObj
            State.IsCoverageNew = True
            State.Coverage.Inuseflag = "N"
            EnableUniqueFields()

            'ClearCoverageRate()
            State.IsNewWithCopy = True
            SetGridControls(moGridView, True)

            ClearCoverageRateGrid()
            'REQ
            SetGridControls(moGridViewConseqDamage, True)



            EnableCoverageButtons(False)
            TheDealerControl.ChangeEnabledControlProperty(True)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, True)
            ControlMgr.SetEnableControl(Me, moProductDrop, True)
            ControlMgr.SetEnableControl(Me, moRiskDrop, True)
            ControlMgr.SetVisibleControl(Me, currLabelDiv, False)
            ControlMgr.SetVisibleControl(Me, currTextBoxDiv, False)
            SetLabelColor(moAgentCodeLabel)
        End Sub

        Private Sub ClearCoverageRateGrid()
            Me.moGridView.DataSource = Nothing
            Me.moGridView.DataBind()
        End Sub

        Private Sub LoadCoverageRateList()

            If moGridView.Rows.Count > 0 Then
                Dim i As Integer
                Dim oCoverageRate(moGridView.Rows.Count - 1) As CoverageRate

                For i = 0 To moGridView.Rows.Count - 1
                    oCoverageRate(i) = New CoverageRate
                    oCoverageRate(i).CoverageId = TheCoverage.Id
                    If TypeOf moGridView.Rows(i).Cells(ColIndexLowPrice).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LowPrice), CType(moGridView.Rows(i).Cells(ColIndexLowPrice).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LowPrice), CType(moGridView.Rows(i).Cells(ColIndexLowPrice).Controls(1), TextBox).Text)
                    End If
                    If TypeOf moGridView.Rows(i).Cells(ColIndexHighPrice).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.HighPrice), CType(moGridView.Rows(i).Cells(ColIndexHighPrice).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.HighPrice), CType(moGridView.Rows(i).Cells(ColIndexHighPrice).Controls(1), TextBox).Text)
                    End If
                    If TypeOf moGridView.Rows(i).Cells(ColIndexGrossAmt).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.GrossAmt), CType(moGridView.Rows(i).Cells(ColIndexGrossAmt).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.GrossAmt), CType(moGridView.Rows(i).Cells(ColIndexGrossAmt).Controls(1), TextBox).Text)
                    End If

                    PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.GrossAmountPercent), GetAmountFormattedDoubleString("0"))
                    If TypeOf moGridView.Rows(i).Cells(ColIndexGrossAmountPercent).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.GrossAmountPercent), CType(moGridView.Rows(i).Cells(ColIndexGrossAmountPercent).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.GrossAmountPercent), CType(moGridView.Rows(i).Cells(ColIndexGrossAmountPercent).Controls(1), TextBox).Text)
                    End If
                    If CoveragePricingCode = NoCoveragePricing Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CommissionsPercent), GetAmountFormattedDoubleString("0"))
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.MarketingPercent), GetAmountFormattedDoubleString("0"))
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.AdminExpense), GetAmountFormattedDoubleString("0"))
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.ProfitExpense), GetAmountFormattedDoubleString("0"))
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LossCostPercent), GetAmountFormattedDoubleString("0"))
                    Else
                        If TypeOf moGridView.Rows(i).Cells(ColIndexCommissionsPercent).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CommissionsPercent), CType(moGridView.Rows(i).Cells(ColIndexCommissionsPercent).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CommissionsPercent), CType(moGridView.Rows(i).Cells(ColIndexCommissionsPercent).Controls(1), TextBox).Text)
                        End If
                        If TypeOf moGridView.Rows(i).Cells(ColIndexMarketingPercent).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.MarketingPercent), CType(moGridView.Rows(i).Cells(ColIndexMarketingPercent).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.MarketingPercent), CType(moGridView.Rows(i).Cells(ColIndexMarketingPercent).Controls(1), TextBox).Text)
                        End If
                        If TypeOf moGridView.Rows(i).Cells(ColIndexAdminExpense).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.AdminExpense), CType(moGridView.Rows(i).Cells(ColIndexAdminExpense).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.AdminExpense), CType(moGridView.Rows(i).Cells(ColIndexAdminExpense).Controls(1), TextBox).Text)
                        End If
                        If TypeOf moGridView.Rows(i).Cells(ColIndexProfitExpense).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.ProfitExpense), CType(moGridView.Rows(i).Cells(ColIndexProfitExpense).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.ProfitExpense), CType(moGridView.Rows(i).Cells(ColIndexProfitExpense).Controls(1), TextBox).Text)
                        End If
                        If TypeOf moGridView.Rows(i).Cells(ColIndexLossCostPercent).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LossCostPercent), CType(moGridView.Rows(i).Cells(ColIndexLossCostPercent).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LossCostPercent), CType(moGridView.Rows(i).Cells(ColIndexLossCostPercent).Controls(1), TextBox).Text)
                        End If

                        'US-521697
                        If State.IsDealerConfiguredForSourceXcd Then
                            If TypeOf moGridView.Rows(i).Cells(ColIndexCommissionsPercentXcd).Controls(0) Is Label Then
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CommissionsPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexCommissionsPercentXcd).Controls(0), Label).Text)
                            Else
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CommissionsPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexCommissionsPercentXcd).Controls(1), DropDownList).SelectedValue)
                            End If

                            If TypeOf moGridView.Rows(i).Cells(ColIndexMarketingPercentXcd).Controls(0) Is Label Then
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.MarketingPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexMarketingPercentXcd).Controls(0), Label).Text)
                            Else
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.MarketingPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexMarketingPercentXcd).Controls(1), DropDownList).SelectedValue)
                            End If

                            If TypeOf moGridView.Rows(i).Cells(ColIndexAdminExpenseXcd).Controls(0) Is Label Then
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.AdminExpenseSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexAdminExpenseXcd).Controls(0), Label).Text)
                            Else
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.AdminExpenseSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexAdminExpenseXcd).Controls(1), DropDownList).SelectedValue)
                            End If

                            If TypeOf moGridView.Rows(i).Cells(ColIndexProfitExpenseXcd).Controls(0) Is Label Then
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.ProfitPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexProfitExpenseXcd).Controls(0), Label).Text)
                            Else
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.ProfitPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexProfitExpenseXcd).Controls(1), DropDownList).SelectedValue)
                            End If

                            If TypeOf moGridView.Rows(i).Cells(ColIndexLossCostPercentXcd).Controls(0) Is Label Then
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LossCostPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexLossCostPercentXcd).Controls(0), Label).Text)
                            Else
                                PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.LossCostPercentSourceXcd), CType(moGridView.Rows(i).Cells(ColIndexLossCostPercentXcd).Controls(1), DropDownList).SelectedValue)
                            End If
                        End If
                    End If
                    If TypeOf moGridView.Rows(i).Cells(ColIndexRenewalNumber).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.RenewalNumber), CType(moGridView.Rows(i).Cells(ColIndexRenewalNumber).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.RenewalNumber), CType(moGridView.Rows(i).Cells(ColIndexRenewalNumber).Controls(1), TextBox).Text)
                    End If

                    If TypeOf moGridView.Rows(i).Cells(ColIndexRegionId).Controls(1) Is Label Then
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.TaxRegion), CType(moGridView.Rows(i).Cells(ColIndexRegionId).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.TaxRegion), CType(moGridView.Rows(i).Cells(ColIndexRegionId).Controls(1), DropDownList).SelectedValue)
                    End If

                    If State.IsProductConfiguredForRenewalNo Then
                        If TypeOf moGridView.Rows(i).Cells(ColIndexCovLiabilityLimit).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CovLiabilityLimit), CType(moGridView.Rows(i).Cells(ColIndexCovLiabilityLimit).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CovLiabilityLimit), CType(moGridView.Rows(i).Cells(ColIndexCovLiabilityLimit).Controls(1), TextBox).Text)
                        End If

                        If TypeOf moGridView.Rows(i).Cells(ColIndexCovLiabilityLimitPercent).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CovLiabilityLimitPercent), CType(moGridView.Rows(i).Cells(ColIndexCovLiabilityLimitPercent).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageRate(i), NameOf(CoverageRate.CovLiabilityLimitPercent), CType(moGridView.Rows(i).Cells(ColIndexCovLiabilityLimitPercent).Controls(1), TextBox).Text)
                        End If
                    End If
                Next
                State.CoverageRateList = oCoverageRate
            End If
        End Sub

        Public Function SaveCoverageRateList() As Boolean
            Dim i As Integer = 0
            Try
                If State.IsNewWithCopy = True And Not State.CoverageRateList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To State.CoverageRateList.Length - 1
                        State.CoverageRateList(i).CoverageId = TheCoverage.Id
                        State.CoverageRateList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    State.CoverageRateList(j).delete()
                    State.CoverageRateList(j).Save()
                Next
                HandleErrors(ex, moMsgControllerRate)
                Return False
            End Try
            Return True
        End Function

        Public Function SaveCoverageDeductibleList() As Boolean
            Dim i As Integer = 0
            Try
                If State.IsNewWithCopy = True And Not State.CoverageDeductibleList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To State.CoverageDeductibleList.Length - 1
                        State.CoverageDeductibleList(i).CoverageId = TheCoverage.Id
                        State.CoverageDeductibleList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    State.CoverageDeductibleList(j).Delete()
                    State.CoverageDeductibleList(j).Save()
                Next
                HandleErrors(ex, MasterPage.MessageController)
                Return False
            End Try
            Return True
        End Function
        Public Function SaveCoverageConseqDamageList() As Boolean
            Dim i As Integer = 0
            Try
                If State.IsNewWithCopy = True And Not State.CoverageConseqDamageList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To State.CoverageConseqDamageList.Length - 1
                        State.CoverageConseqDamageList(i).CoverageId = TheCoverage.Id
                        State.CoverageConseqDamageList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    State.CoverageConseqDamageList(j).Delete()
                    State.CoverageConseqDamageList(j).Save()
                Next
                HandleErrors(ex, MasterPage.MessageController)
                Return False
            End Try
            Return True
        End Function

        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBo() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.NewAndCopy
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If

                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Dim retType As New CoverageSearchForm.ReturnType(DetailPageCommand.Delete, State.CoverageId)
                If DeleteCoverage() = True Then
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CoverageTypeId), moCoverageTypeLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.DealerId), TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.OptionalId), moOptionalLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.ProductCodeId), moProductLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.ProductItemId), moProductItemLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.RiskTypeId), moRiskLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CertificateDuration), moCertificateDurationLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CoverageDuration), moCoverageDurationLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.Deductible), moDeductibleLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.Effective), moEffectiveLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.Expiration), moExpirationLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.LiabilityLimit), moLiabilityLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.MarkupDistributionPercent), lblMarkupDistPercent)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.LiabilityLimitPercent), moLiabilityLimitPercentLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.OffsetToStart), moOffsetLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.DeductiblePercent), moDeductiblePercentLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.RepairDiscountPct), moRepairDiscountPctLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.ReplacementDiscountPct), moReplacementDiscountPrcLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.ReplacementDiscountPct), moReplacementDiscountPrcLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.IsClaimAllowedId), moIsClaimAllowedLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.UseCoverageStartDateId), moUseCoverageStartDateLable)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.MethodOfRepairId), moMethodOfRepairLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.FulfillmentProfileCode), moFulfillmentProfileLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.FulfillmentProviderXcd), moFulfillmentProviderLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.DeductibleBasedOnId), moDeductibleBasedOnLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.IsReInsuredId), moReInsuredLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CoverageClaimLimit), moClaimLimitCountLabel)
            'Def-26342: Added following condition to display mandatory notation for Agent code if RequiresAgentCodeId at company level is set to yes.

            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                Dim objCompany = New Company()
                Dim ds As DataSet = objCompany.GetCompanyAgentFlagForDealer(TheDealerControl.SelectedGuid)

                If (Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0) Then
                    Dim requiresAgentCodeId As Guid = New Guid(CType(ds.Tables(0).Rows(0)(0), Byte()))
                    Dim mandatory As String = "<span class=""mandatory"">*&nbsp;</span>"
                    If (requiresAgentCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
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

            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.AgentCode), moAgentCodeLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CoverageLiabilityLimit), moCoverageLiabilityLimitLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.CoverageLiabilityLimitPercent), moCoverageLiabilityLimitPercentLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.RecoverDeviceId), moRecoverDeciveLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.OffsetToStartDays), moOffsetLabel)
            BindBOPropertyToLabel(TheCoverage, NameOf(TheCoverage.OffsetMethodId), moOffsetMethodLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(moCoverageTypeLabel)
            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(moOptionalLabel)
            ClearLabelErrSign(moProductLabel)
            ClearLabelErrSign(moProductItemLabel)
            ClearLabelErrSign(moRiskLabel)
            ClearLabelErrSign(moCertificateDurationLabel)
            ClearLabelErrSign(moCoverageDurationLabel)
            ClearLabelErrSign(moDeductibleLabel)
            ClearLabelErrSign(moEffectiveLabel)
            ClearLabelErrSign(moExpirationLabel)
            ClearLabelErrSign(moLiabilityLabel)
            ClearLabelErrSign(moLiabilityLimitPercentLabel)
            ClearLabelErrSign(moOffsetLabel)
            ClearLabelErrSign(moOffsetMethodLabel)
            ClearLabelErrSign(moMethodOfRepairLabel)
            ClearLabelErrSign(moFulfillmentProfileLabel)
            ClearLabelErrSign(moFulfillmentProviderLabel)
            ClearLabelErrSign(moDeductibleBasedOnLabel)
            ClearLabelErrSign(moCoverageLiabilityLimitLabel)
            ClearLabelErrSign(moCoverageLiabilityLimitPercentLabel)
            ClearLabelErrSign(moRecoverDeciveLabel)
            ClearLabelErrSign(moAgentCodeLabel)
            ClearLabelErrSign(moClaimLimitCountLabel)

        End Sub

        Private Sub BindBoPropertiesToGridHeader()
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.LowPrice), moGridView.Columns(ColIndexLowPrice))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.HighPrice), moGridView.Columns(ColIndexHighPrice))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.GrossAmt), moGridView.Columns(ColIndexGrossAmt))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.CommissionsPercent), moGridView.Columns(ColIndexCommissionsPercent))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.MarketingPercent), moGridView.Columns(ColIndexMarketingPercent))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.AdminExpense), moGridView.Columns(ColIndexAdminExpense))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.ProfitExpense), moGridView.Columns(ColIndexProfitExpense))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.LossCostPercent), moGridView.Columns(ColIndexLossCostPercent))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.GrossAmountPercent), moGridView.Columns(ColIndexGrossAmountPercent))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.RenewalNumber), moGridView.Columns(ColIndexRenewalNumber))
            BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.RegionId), moGridView.Columns(ColIndexRegionId))

            If State.IsProductConfiguredForRenewalNo Then
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.CovLiabilityLimit), moGridView.Columns(ColIndexCovLiabilityLimit))
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.CovLiabilityLimitPercent), moGridView.Columns(ColIndexCovLiabilityLimitPercent))
            End If

            If State.IsDealerConfiguredForSourceXcd Then
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.CommissionsPercentSourceXcd), moGridView.Columns(ColIndexCommissionsPercentXcd))
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.MarketingPercentSourceXcd), moGridView.Columns(ColIndexMarketingPercentXcd))
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.AdminExpenseSourceXcd), moGridView.Columns(ColIndexAdminExpenseXcd))
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.ProfitPercentSourceXcd), moGridView.Columns(ColIndexProfitExpenseXcd))
                BindBOPropertyToGridHeader(TheCoverageRate, NameOf(TheCoverageRate.LossCostPercentSourceXcd), moGridView.Columns(ColIndexLossCostPercentXcd))
            End If
        End Sub
        Private Sub BindBoPropertiesToDeductibleGridHeader()
            BindBOPropertyToGridHeader(TheCoverageDeductible, CoverageDedIdProperty, dedGridView.Columns(ColSeqCoverageDeductibleId))
            BindBOPropertyToGridHeader(TheCoverageDeductible, NameOf(TheCoverageDeductible.MethodOfRepairId), dedGridView.Columns(ColSeqMethodOfRepairDescription))
            BindBOPropertyToGridHeader(TheCoverageDeductible, NameOf(TheCoverageDeductible.DeductibleBasedOnId), dedGridView.Columns(ColSeqDeductibleBasedOnDesc))
            BindBOPropertyToGridHeader(TheCoverageDeductible, NameOf(TheCoverageDeductible.Deductible), dedGridView.Columns(ColSeqDeductible))
        End Sub

        Private Sub BindBoPropertiesToConseqDamageGridHeader()
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.ConseqDamageTypeXcd), moGridViewConseqDamage.Columns(ColSeqConseqDamageType))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitBaseXcd), moGridViewConseqDamage.Columns(ColSeqLiabilityLimitBasedOn))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitPerIncident), moGridViewConseqDamage.Columns(ColSeqLiabilityLimitPerIncident))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitCumulative), moGridViewConseqDamage.Columns(ColSeqLiabilityLimitCumulative))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.Effective), moGridViewConseqDamage.Columns(ColSeqConseqDamageEffectiveDate))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.Expiration), moGridViewConseqDamage.Columns(ColSeqConseqDamageExpirationDate))
            BindBOPropertyToGridHeader(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.FulfilmentMethodXcd), moGridViewConseqDamage.Columns(ColSeqFulfillmentMethod))
        End Sub
        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub


#End Region

#End Region

#Region "Handlers-CoverageRate"

#Region "Handlers-CoverageRate-DataGrid"

        ' Coverage-Rate DataGrid
        Public Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moGridView_PageIndexChanged(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles moGridView.PageIndexChanging
            Try
                ResetIndexes()
                moGridView.PageIndex = e.NewPageIndex
                PopulateCoverageRateList(ActionCancelDelete)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'The pencil was clicked
        Protected Sub ItemCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles moGridView.RowCommand
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    'nIndex = e.Item.ItemIndex
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    '      EnableForEditRateButtons(True)
                    PopulateCoverageRateList(ActionEdit)
                    FillDropdownList()
                    PopulateCoverageRate()

                    'US-521697
                    FillSourceXcdDropdownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()

                    SetGridControls(moGridView, False)
                    SetFocusInGrid(moGridView, nIndex, ColIndexLowPrice)
                    EnableDisableControls(moCoverageEditPanel, True)
                    Setbuttons(False)

                    'US-489838
                    DisableLimitWhenRenewalIsZero()
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    'nIndex = e.Item.ItemIndex
                    CoverageRateId = GetGridText(moGridView, nIndex, ColIndexCoverageRateId)
                    If DeleteSelectedCoverageRate(nIndex) = True Then
                        PopulateCoverageRateList(ActionCancelDelete)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ResetIndexes()
            moGridView.EditIndex = NO_ITEM_SELECTED_INDEX
            moGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Sub

#End Region

#Region "Handlers-CoverageRate-Buttons"

        Private Sub Setbuttons(ByVal enable As Boolean)
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
                PopulateCoverageRateList(ActionSave)
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
            End If
        End Sub

        Private Sub BtnSaveRate_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSaveRate_WRITE.Click
            Try
                If Not TheCoverageRate Is Nothing Then
                    TheCoverageRate.IsProductSetForSequenceRenewalNo = State.IsProductConfiguredForRenewalNo
                End If

                SaveRateChanges()

                'US-521697
                SetGridSourceXcdLabelFromBo()

                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                IsNewRate = False
                EnableForEditRateButtons(False)
                PopulateCoverageRateList(ActionCancelDelete)
                'US-521697
                SetGridSourceXcdLabelFromBo()
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                IsNewRate = True
                CoverageRateId = Guid.Empty.ToString
                PopulateCoverageRateList(ActionNew)
                FillDropdownList()
                
                'US-521697
                FillSourceXcdDropdownList()

                SetGridControls(moGridView, False)
                SetFocusInGrid(moGridView, moGridView.SelectedIndex, ColIndexLowPrice)
                
                'US-521697
                SetGridSourceXcdLabelFromBo()
                SetGridSourceXcdTextBoxForNewCoverage()

                EnableDisableControls(moCoverageEditPanel, True)
                Setbuttons(False)

                'US-489838
                DisableLimitWhenRenewalIsZero()
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerRate)
            End Try
        End Sub

#End Region

#End Region

#Region "Handlers Coverage Deductible"

#Region "Coverage Deductible Button management"
        Private Sub EnableCoverageDeductible(ByVal bIsReadWrite As Boolean)
            dedGridView.Columns(ColSeqMethodOfRepairDescription).Visible = bIsReadWrite
            dedGridView.Columns(ColSeqDeductibleBasedOnDesc).Visible = bIsReadWrite
            dedGridView.Columns(ColSeqDeductible).Visible = bIsReadWrite
        End Sub

        Private Sub ManageDeductibleButtons(ByVal newButton As Boolean, ByVal saveCancelButton As Boolean)

            btnnew_Deductible.Enabled = newButton
            btnSave_Deductible.Enabled = saveCancelButton
            btnCancel_Deductible.Enabled = saveCancelButton

        End Sub

        Private Sub BtnSave_Deductible_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave_Deductible.Click
            Try
                SaveDeductibleChanges()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)

            Catch ex As Exception
                HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub BtnCancel_Deductible_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel_Deductible.Click
            'Pencil button in not in edit mode
            Try
                IsNewDeductible = False
                ManageDeductibleButtons(True, False)
                PopulateCoverageDeductibleList(ActionCancelDelete)
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub BtnNew_Deductible_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnnew_Deductible.Click
            Try
                IsNewDeductible = True
                CoverageDeductibleId = Guid.Empty.ToString
                PopulateCoverageDeductibleList(ActionNew)
                SetGridControls(dedGridView, False)
                EnableDisableControls(moCoverageEditPanel, True)
                If btnnew_Deductible.Enabled = False Then
                    Setbuttons(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerDeductible)
            End Try
        End Sub

        Private Sub SaveDeductibleChanges()
            If ApplyDeductibleChanges() = True Then
                If IsNewDeductible = True Then
                    IsNewDeductible = False
                End If
                PopulateCoverageDeductibleList(ActionSave)
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
            End If
        End Sub

        Private Function ApplyDeductibleChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If dedGridView.EditIndex < 0 Then Return False ' Coverage deductible is not in edit mode
            If State.IsNewWithCopy Then
                LoadCoverageDeductibleList()
                State.CoverageDeductibleList(dedGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewDeductible = False Then
                CoverageDeductibleId = GetSelectedGridText(dedGridView, ColSeqCoverageDeductibleId)
            End If
            BindBoPropertiesToDeductibleGridHeader()
            With TheCoverageDeductible
                PopulateDeductibleBoFromForm()
                bIsDirty = .IsDirty
                .Save()
                ManageDeductibleButtons(True, False)

            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
#End Region

#Region "Coverage Deductible Grid"
        ' Coverage-Rate DataGrid
        Public Sub DeductibleItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'The pencil was clicked
        Protected Sub DeductibleItemCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs)
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    dedGridView.EditIndex = nIndex
                    dedGridView.SelectedIndex = nIndex
                    FillDropDownList(dedGridView.Rows(dedGridView.SelectedIndex))
                    PopulateCoverageDeductibleList(ActionEdit)
                    PopulateCoverageDeductible()
                    SetGridControls(dedGridView, False)
                    EnableDisableControls(moCoverageEditPanel, True)
                    Setbuttons(False)


                    Dim txtDeductible As TextBox = DirectCast(dedGridView.Rows()(nIndex).FindControl("motxt_Deductible"), TextBox)
                    Dim ddlDeductibleBasedOn As DropDownList = DirectCast(dedGridView.Rows()(nIndex).FindControl("moddl_DeductibleBasedOn"), DropDownList)

                    Dim deductibleId As Guid = GetSelectedItem((ddlDeductibleBasedOn))
                    Dim deductibleCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleId)


                    If Not ((deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                            (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__FIXED)) Then


                        txtDeductible.Text = ZeroNumber
                        txtDeductible.Enabled = False
                    End If


                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    CoverageDeductibleId = GetGridText(dedGridView, nIndex, ColSeqCoverageDeductibleId)
                    If DeleteSelectedCoverageDeductible(nIndex) = True Then
                        PopulateCoverageDeductibleList(ActionCancelDelete)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadCoverageDeductibleList()

            If dedGridView.Rows.Count > 0 Then
                Dim i As Integer
                Dim oCoverageDeductible(dedGridView.Rows.Count - 1) As CoverageDeductible

                For i = 0 To dedGridView.Rows.Count - 1
                    oCoverageDeductible(i) = New CoverageDeductible
                    oCoverageDeductible(i).CoverageId = TheCoverage.Id
                    With dedGridView.Rows(i)
                        If TypeOf dedGridView.Rows(i).Cells(ColSeqDeductible).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageDeductible(i), NameOf(CoverageDeductible.Deductible), CType(dedGridView.Rows(i).Cells(ColSeqDeductible).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageDeductible(i), NameOf(CoverageDeductible.Deductible), CType(dedGridView.Rows(i).Cells(ColSeqDeductible).Controls(1), TextBox).Text)
                        End If
                        PopulateBOProperty(oCoverageDeductible(i), NameOf(CoverageDeductible.MethodOfRepairId), GetGuidFromString(CType(dedGridView.Rows(i).Cells(ColSeqMethodOfRepairId).Controls(1), Label).Text))
                        PopulateBOProperty(oCoverageDeductible(i), NameOf(CoverageDeductible.DeductibleBasedOnId), GetGuidFromString(CType(dedGridView.Rows(i).Cells(ColSeqDeductibleBasedOnId).Controls(1), Label).Text))
                    End With

                Next
                State.CoverageDeductibleList = oCoverageDeductible
            End If
        End Sub
        Private Sub dedGridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles dedGridView.RowCreated
            Try
                FillDropDownList(e.Row)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub FillDropDownList(ByVal dtRow As GridViewRow)

            Dim ddlMethodOfRepair As DropDownList = DirectCast(dtRow.FindControl(MethodOfRepairDropDownList), DropDownList)
            Dim ddlDeductibleBasedOn As DropDownList = DirectCast(dtRow.FindControl(DeductibleBasedOnDropDownList), DropDownList)
            'Dim oLanguageId As Guid = GetLanguageId()

            If Not ddlMethodOfRepair Is Nothing Then
                'BindListControlToDataView(ddlMethod_of_repair, LookupListNew.GetMethodOfRepairLookupList(oLanguageId), , , True)
                ddlMethodOfRepair.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            End If
            If Not ddlDeductibleBasedOn Is Nothing Then
                'BindListControlToDataView(ddldedBasedon, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(oLanguageId), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.LanguageId = GetLanguageId()
                ddlDeductibleBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })
            End If

        End Sub
#End Region

#Region "Populate Deductible"
        'Populate Deductible starts here

        Private Sub PopulateCoverageDeductibleList(Optional ByVal oAction As String = ActionNone)
            Dim oDataView As DataView

            If State.IsCoverageNew = True And Not State.IsNewWithCopy Then Return ' We can not have CoverageRates if the coverage is new

            Try

                EnableCoverageDeductible(True)

                If State.IsNewWithCopy Then
                    oDataView = CoverageDeductible.GetList(Guid.Empty, GetLanguageId())
                    If Not oAction = ActionCancelDelete Then LoadCoverageDeductibleList()
                    If Not State.CoverageRateList Is Nothing Then
                        oDataView = GetDataViewFromArray(State.CoverageDeductibleList, oDataView.Table)
                    End If
                Else
                    oDataView = CoverageDeductible.GetList(TheCoverage.Id, GetLanguageId())
                End If

                Select Case oAction
                    Case ActionNone
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, dedGridView, 0)
                        ManageDeductibleButtons(True, False)
                    Case ActionSave
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex)
                        ManageDeductibleButtons(True, False)
                    Case ActionCancelDelete
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, dedGridView,
                                    dedGridView.PageIndex)
                        ManageDeductibleButtons(True, False)
                    Case ActionEdit
                        If State.IsNewWithCopy Then
                            CoverageDeductibleId = State.CoverageRateList(dedGridView.SelectedIndex).Id.ToString
                        Else
                            CoverageDeductibleId = GetSelectedGridText(dedGridView, ColSeqCoverageDeductibleId)
                        End If
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex, True)
                        ManageDeductibleButtons(False, True)
                    Case ActionNew
                        If State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DbCoverageDeductibleId) = TheCoverageDeductible.Id.ToByteArray
                        oRow(DbMethodOfRepairId) = Guid.Empty.ToByteArray
                        oRow(DbDeductibleBasedOn) = Guid.Empty.ToByteArray
                        oDataView.Table.Rows.Add(oRow)
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageDeductibleId), dedGridView,
                                    dedGridView.PageIndex, True)

                        ManageDeductibleButtons(False, True)

                End Select

                dedGridView.DataSource = oDataView
                dedGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, dedGridView)

            Catch ex As Exception
                moMsgControllerDeductible.AddError(CoverageForm006)
                moMsgControllerDeductible.AddError(ex.Message, False)
                'moMsgControllerDeductible.Show()
            End Try
        End Sub

        Private Sub PopulateCoverageDeductible()
            Dim sDeductibleBasedOnCode As String

            If State.IsNewWithCopy Then
                With State.CoverageDeductibleList(moGridView.SelectedIndex)
                    SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqMethodOfRepairDescription).FindControl(MethodOfRepairDropDownList), DropDownList), .MethodOfRepairId)

                    sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)

                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION) Then
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleBasedOnDropDownList), DropDownList), .DeductibleExpressionId)
                        ControlMgr.SetEnableControl(Me, CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleText), TextBox), False)
                    Else
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleBasedOnDropDownList), DropDownList), .DeductibleBasedOnId)
                        SetSelectedGridText(dedGridView, ColSeqDeductible, .Deductible.ToString)
                    End If
                End With
            Else
                With TheCoverageDeductible

                    SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqMethodOfRepairDescription).FindControl(MethodOfRepairDropDownList), DropDownList), .MethodOfRepairId)

                    sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)
                    If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION) Then
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleBasedOnDropDownList), DropDownList), .DeductibleExpressionId)
                        ControlMgr.SetEnableControl(Me, CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleText), TextBox), False)
                    Else
                        SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(ColSeqDeductibleBasedOnDesc).FindControl(DeductibleBasedOnDropDownList), DropDownList), .DeductibleBasedOnId)
                        SetSelectedGridText(dedGridView, ColSeqDeductible, .Deductible.ToString)
                    End If

                    'SetSelectedItem(CType(dedGridView.Rows(dedGridView.SelectedIndex).Cells(DEDUCTIBLE_BASED_ON_DESC).FindControl(DDL_DEDUCTIBLE_BASED_ON), DropDownList), deductiblegridBasedOnId)
                    'SetSelectedGridText(dedGridView, DEDUCTIBLE, .Deductible.ToString)
                End With
            End If

        End Sub
#End Region

#Region "Coverage Conseq Damage Grid"

        'Public Sub PerilItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        '    Try
        '        BaseItemCreated(sender, e)
        '    Catch ex As Exception
        '        HandleErrors(ex, MasterPage.MessageController)
        '    End Try
        'End Sub

        'The pencil was clicked
        Public Sub ConseqDamageRowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs)

            Dim btnConseqDamageEffectiveDate As ImageButton
            Dim moConseqDamageEffectiveDateText As TextBox
            Dim btnConseqDamageExpirationDate As ImageButton
            Dim moConseqDamageExpirationDateText As TextBox
            Dim oGridViewrow As GridViewRow

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    Dim nIndex As Integer = CInt(e.CommandArgument)
                    moGridViewConseqDamage.EditIndex = nIndex
                    moGridViewConseqDamage.SelectedIndex = nIndex

                    FillConseqDamageDropDownList(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex))
                    PopulateCoverageConseqDamageList(ActionEdit)
                    PopulateCoverageConseqDamage()
                    SetGridControls(moGridViewConseqDamage, False)
                    EnableDisableControls(moCoverageEditPanel, True)
                    Setbuttons(False)

                    'Date Calendars
                    oGridViewrow = moGridViewConseqDamage.Rows(nIndex)
                    btnConseqDamageEffectiveDate = CType(oGridViewrow.FindControl(ConseqDamageEffectiveDateButton), ImageButton)
                    moConseqDamageEffectiveDateText = CType(oGridViewrow.FindControl(ConseqDamageEffectiveDateText), TextBox)
                    AddCalendar_New(btnConseqDamageEffectiveDate, moConseqDamageEffectiveDateText)

                    btnConseqDamageExpirationDate = CType(oGridViewrow.FindControl(ConseqDamageExpirationDateButton), ImageButton)
                    moConseqDamageExpirationDateText = CType(oGridViewrow.FindControl(ConseqDamageExpirationDateText), TextBox)
                    AddCalendar_New(btnConseqDamageExpirationDate, moConseqDamageExpirationDateText)

                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    Dim nIndex As Integer = CInt(e.CommandArgument)
                    CoverageConseqDamageId = GetGridText(moGridViewConseqDamage, nIndex, ColSeqCoverageConseqDamageId)
                    If DeleteSelectedCoverageConseqDamage(nIndex) = True Then
                        PopulateCoverageConseqDamageList(ActionCancelDelete)
                    End If
                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadCoverageConseqDamageList()

            If moGridViewConseqDamage.Rows.Count > 0 Then
                Dim i As Integer
                Dim oCoverageConseqDamage(moGridViewConseqDamage.Rows.Count - 1) As CoverageConseqDamage

                For i = 0 To moGridViewConseqDamage.Rows.Count - 1
                    oCoverageConseqDamage(i) = New CoverageConseqDamage
                    oCoverageConseqDamage(i).CoverageId = TheCoverage.Id
                    With moGridViewConseqDamage.Rows(i)
                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitPerIncident).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitPerIncident), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitPerIncident).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitPerIncident), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitPerIncident).Controls(1), TextBox).Text)
                        End If

                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitCumulative).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitCumulative), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitCumulative).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitCumulative), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitCumulative).Controls(1), TextBox).Text)
                        End If

                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageEffectiveDate).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.Effective), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageEffectiveDate).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.Effective), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageEffectiveDate).Controls(1), TextBox).Text)
                        End If

                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageExpirationDate).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.Expiration), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageExpirationDate).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.Expiration), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageExpirationDate).Controls(1), TextBox).Text)
                        End If

                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageType).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.ConseqDamageTypeXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageTypeXcd).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.ConseqDamageTypeXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqConseqDamageType).Controls(1), DropDownList).SelectedValue)
                        End If

                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitBasedOn).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitBaseXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitBasedOnXcd).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.LiabilityLimitBaseXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqLiabilityLimitBasedOn).Controls(1), DropDownList).SelectedValue)
                        End If


                        If TypeOf moGridViewConseqDamage.Rows(i).Cells(ColSeqFulfillmentMethod).Controls(1) Is Label Then
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.FulfilmentMethodXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqFulfillmentMethodXcd).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oCoverageConseqDamage(i), NameOf(CoverageConseqDamage.FulfilmentMethodXcd), CType(moGridViewConseqDamage.Rows(i).Cells(ColSeqFulfillmentMethod).Controls(1), DropDownList).SelectedValue)
                        End If
                    End With

                Next
                State.CoverageConseqDamageList = oCoverageConseqDamage
            End If
        End Sub
        Private Sub ConseqDamageGridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles moGridViewConseqDamage.RowCreated
            Try
                FillConseqDamageDropDownList(e.Row)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ConseqDamageGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles moGridViewConseqDamage.RowDataBound
            Try
                If (e.Row.RowType = DataControlRowType.DataRow) Then

                    Dim editButtonWrite As ImageButton = DirectCast(e.Row.FindControl("EditButton_WRITE"), ImageButton)
                    Dim deleteButtonWrite As ImageButton = DirectCast(e.Row.FindControl("DeleteButton_WRITE"), ImageButton)
                    Dim moConseqDamageEffectiveDateLabel As Label = DirectCast(e.Row.FindControl("moConseqDamageEffectiveDateLabel"), Label)

                    If Not moConseqDamageEffectiveDateLabel Is Nothing Then
                        If Not String.IsNullOrEmpty(moConseqDamageEffectiveDateLabel.Text) Then
                            If (DateTime.Now >= Convert.ToDateTime(moConseqDamageEffectiveDateLabel.Text)) Then
                                ControlMgr.SetVisibleControl(Me, editButtonWrite, False)
                                ControlMgr.SetVisibleControl(Me, deleteButtonWrite, False)
                            End If
                        End If
                    End If

                    Dim rowState As DataControlRowState = e.Row.RowState
                    If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                        Dim effectiveDateTextBox As TextBox = CType(e.Row.FindControl(ConseqDamageEffectiveDateText), TextBox)
                        Dim effectiveDateImageButton As ImageButton = CType(e.Row.FindControl(ConseqDamageEffectiveDateButton), ImageButton)
                        Dim expirationDateTextBox As TextBox = CType(e.Row.FindControl(ConseqDamageExpirationDateText), TextBox)
                        Dim expirationDateImageButton As ImageButton = CType(e.Row.FindControl(ConseqDamageExpirationDateButton), ImageButton)

                        AddCalendar_New(effectiveDateImageButton, effectiveDateTextBox)
                        AddCalendar_New(expirationDateImageButton, expirationDateTextBox)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try


        End Sub

        Private Sub FillConseqDamageDropDownList(ByVal dtRow As GridViewRow)
            Dim moConseqDamageTypeDropdown As DropDownList = DirectCast(dtRow.FindControl("moConseqDamageTypeDropdown"), DropDownList)
            Dim moLiabilityLimitBasedOnDropdown As DropDownList = DirectCast(dtRow.FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList)
            Dim moFulfilmentMethodDropdown As DropDownList = DirectCast(dtRow.FindControl("moFulfilmentMethodDropdown"), DropDownList)

            If Not moConseqDamageTypeDropdown Is Nothing Then
                'moConseqDamageTypeDropdown.PopulateOld("PERILTYP", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim damageTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PERILTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=New ListContext())
                moConseqDamageTypeDropdown.Populate(damageTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                    })
            End If
            If Not moLiabilityLimitBasedOnDropdown Is Nothing Then
                'moLiabilityLimitBasedOnDropdown.PopulateOld("PRODLILIMBASEDON", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim liabilityLimitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PRODLILIMBASEDON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moLiabilityLimitBasedOnDropdown.Populate(liabilityLimitList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                    })
                BindSelectItem(Codes.COVERAGE_CONSEQ_DAMAGE_LIABILITY_LIMIT_BASED_ON_NOTAPPL, moLiabilityLimitBasedOnDropdown)
            End If
            If Not moFulfilmentMethodDropdown Is Nothing Then
                'moFulfilmentMethodDropdown.PopulateOld("FULFILMETH", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
                Dim fulfilmentMethodList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="FULFILMETH", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moFulfilmentMethodDropdown.Populate(fulfilmentMethodList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .BlankItemValue = String.Empty,
                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
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

#End Region

#Region "Clear"

        Private Sub ClearCoverageRate()
            If Not State.IsNewWithCopy Then
                EnableRateButtons(False)
                moGridView.DataBind()
            End If
        End Sub

        Private Sub ClearCoverageDeductible()
            If Not State.IsNewWithCopy Then
                ManageDeductibleButtons(True, False)

                dedGridView.DataBind()
            End If
        End Sub

        Private Sub ClearTexts()
            If Not State.IsNewWithCopy Then
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
            ClearList(moFulfillmentProviderDrop)
            ClearList(moFulfillmentProfileDrop)
            ClearForProduct()
            ClearList(moProductDrop)
        End Sub

        Private Sub ClearAll()
            ClearForDealer()
            TheDealerControl.ClearMultipleDrop()
            ClearList(UseCoverageStartDateId)
            EnableUniqueFields()
            SetLabelColor(moAgentCodeLabel)
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
                Dim oDealerView As DataView = LookupListNew.GetDealerLookupList(CurrentUser().Companies)
                TheDealerControl.SetControl(True,
                                            MultipleColumnDDLabelControl.MODES.NEW_MODE,
                                            True,
                                            oDealerView,
                                            "* " + TranslationBase.TranslateLabelOrMessage(LabelSelectDealerCode),
                                            True, True,
,
                                            "multipleDropControl_moMultipleColumnDrop",
                                            "multipleDropControl_moMultipleColumnDropDesc",
                                            "multipleDropControl_lb_DropDown",
                                            False,
                                            0)

                If State.IsCoverageNew = True Then
                    TheDealerControl.NothingSelected = True
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.SelectedGuid = TheCoverage.DealerId
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    PopulateProductCode()
                End If


            Catch ex As Exception
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
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
                                                        .TextFunc = AddressOf PopulateOptions.GetCode,
                                                        .SortFunc = AddressOf PopulateOptions.GetCode
                                                       })
            End If
        End Sub
        Private Sub PopulateProductCode()
            If TheDealerControl.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oDealerId As Guid = TheDealerControl.SelectedGuid 'GetSelectedItem(moDealerDrop)
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Try
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                moProductDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf PopulateOptions.GetCode,
                                                    .SortFunc = AddressOf PopulateOptions.GetCode
                                                  })

                moReInsuredDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If State.IsCoverageNew = True Then
                    BindSelectItem(Nothing, moProductDrop)
                    ControlMgr.SetEnableControl(Me, moProductDrop, True)

                    BindSelectItem(Nothing, moReInsuredDrop)
                    DisabledTabsList.Add(TabAttributes)
                    AttributeValues.Visible = False
                Else
                    BindSelectItem(TheCoverage.ProductCodeId.ToString, moProductDrop)
                    ControlMgr.SetEnableControl(Me, moProductDrop, False)

                    BindSelectItem(TheCoverage.IsReInsuredId.ToString, moReInsuredDrop)

                    If TheCoverage.IsReInsuredId.Equals(Guid.Empty) Or LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupNo) = TheCoverage.IsReInsuredId Then
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
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateRiskType()
            If moProductDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                'BindListControlToDataView(moRiskDrop, LookupListNew.GetRiskProductCodeLookupList(oProductId))
                Dim listContext As ListContext = New ListContext()
                listContext.ProductCodeId = oProductId
                moRiskDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ItemRiskTypeByProduct", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listContext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

                If State.IsCoverageNew = True Then
                    BindSelectItem(Nothing, moRiskDrop)
                    ControlMgr.SetEnableControl(Me, moRiskDrop, True)
                Else
                    BindSelectItem(TheCoverage.RiskTypeId.ToString, moRiskDrop)
                    ControlMgr.SetEnableControl(Me, moRiskDrop, False)
                End If
                PopulateCoverageType()
                PopulateItemNumber()
            Catch ex As Exception
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub
        Private Sub EnableDisableCoverageLiabilityLimits()
            If moProductDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Dim oProductLiabilityLimitBaseId As Guid = Coverage.GetProductLiabilityLimitBase(oProductId)
            Try
                Dim notAppicableId = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, ProdLiabBasedOnNotApp)
                If (oProductLiabilityLimitBaseId.Equals(Guid.Empty) Or
                    oProductLiabilityLimitBaseId = notAppicableId) Then
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
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateCoverageType()
            If moRiskDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then Return
            Try
                Dim listContext As ListContext = New ListContext()
                listContext.CompanyGroupId = CurrentUser().CompanyGroup.Id
                moCoverageTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listContext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

                If IsPostBack And Not State.IsUndo Then
                    BindSelectItem(State.SelectedCoverageTypeId.ToString, moCoverageTypeDrop)
                    State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.CoverageTypeId.ToString, moCoverageTypeDrop)
                End If
                PopulateRestCoverage()
            Catch ex As Exception
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateItemNumber()
            Dim oCoverageTypeView As DataView
            If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                oCoverageTypeView = LookupListNew.GetItemRiskTypeLookupList(GetSelectedItem(moProductDrop))
                If oCoverageTypeView.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To oCoverageTypeView.Count - 1
                        If oCoverageTypeView.Item(i).Item(ColDescriptionName).ToString = moRiskDrop.SelectedItem.Text Then
                            moItemNumberText.Text = oCoverageTypeView.Item(i).Item(ColCodeName).ToString
                            State.SelectedItemId = New Guid(CType(oCoverageTypeView.Item(i).Item("id"), Byte()))
                            Exit For
                        End If
                    Next
                Else
                    moItemNumberText.Text = Nothing
                End If
            End If

        End Sub

        Private Sub PopulateEarningCode()
            Try
                Dim listContext As ListContext = New ListContext()
                listContext.CompanyGroupId = CurrentUser().CompanyGroup.Id
                moEarningCodeDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="EarningCodesByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listContext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf PopulateOptions.GetCode,
                                                    .SortFunc = AddressOf PopulateOptions.GetCode
                                                  })

                If IsPostBack And Not State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(State.SelectedEarningCodeId.ToString, moEarningCodeDrop)
                    State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.EarningCodeId.ToString, moEarningCodeDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateTaxTypeCode()
            Try

                moTaxTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                    .AddBlankItem = True,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })

                If IsPostBack And Not State.IsUndo Then
                    ' JLR - Restore Previously Selected Values
                    BindSelectItem(State.SelectedTaxTypeXcd.ToString, moTaxTypeDrop)
                    State.IsUndo = False
                Else

                    If TheCoverage.TaxTypeXCD Is Nothing Then
                        BindSelectItem(String.Empty, moTaxTypeDrop)
                    Else

                        BindSelectItem(TheCoverage.TaxTypeXCD.ToString, moTaxTypeDrop)
                    End If
                End If

            Catch ex As Exception
                MasterPage.MessageController.AddError(CoverageForm002 & " " & ex.Message, True)
            End Try
        End Sub

        Private Sub PopulateProductItem()
            Try
                'BindListControlToDataView(moProductItemDrop, LookupListNew.GetProductItemLookupList(oLanguageId), , , True)
                Dim oProductItemList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PRODI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moProductItemDrop.Populate(oProductItemList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If IsPostBack And Not State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(State.SelectedProductItemId.ToString, moProductItemDrop)
                    State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.ProductItemId.ToString, moProductItemDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateOffsetMethod()
            'Dim oLanguageId As Guid = GetLanguageId()
            Try
                'BindListTextToDataView(moOffsetMethodDrop, LookupListNew.DropdownLookupList("COVERAGE_OFFSET_METHOD", oLanguageId), , "Code", True)
                moOffsetMethodDrop.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="COVERAGE_OFFSET_METHOD", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode
                                                  })

                If IsPostBack And Not State.IsUndo Then
                    'Def-26342: Added condition to check null value for selectedOffsetMethod
                    If Not State.SelectedOffsetMethod Is Nothing Then
                        BindSelectItem(State.SelectedOffsetMethod.ToString, moOffsetMethodDrop)
                        State.IsUndo = False
                    End If

                Else
                    BindSelectItem(TheCoverage.OffsetMethod.ToString, moOffsetMethodDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateOptional()
            'Dim oLanguageId As Guid = GetLanguageId()
            Try
                'BindListControlToDataView(moOptionalDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                moOptionalDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                If IsPostBack And Not State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    BindSelectItem(State.SelectedOptionalId.ToString, moOptionalDrop)
                    State.IsUndo = False
                Else
                    BindSelectItem(TheCoverage.OptionalId.ToString, moOptionalDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateClaimAllowed()
            Try
                Dim oYesString As String
                Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                moIsClaimAllowedDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

                If State.IsCoverageNew = True Then
                    oYesString = (From lst In oYesNoList
                                  Where lst.Code = "Y"
                                  Select lst.Translation).FirstOrDefault()
                    moIsClaimAllowedDrop.Items.FindByText(oYesString).Selected = True
                Else
                    If IsPostBack And Not State.IsUndo Then
                        BindSelectItem(State.SelectedIsClaimAllowedId.ToString, moIsClaimAllowedDrop)
                        State.IsUndo = False
                    Else
                        BindSelectItem(TheCoverage.IsClaimAllowedId.ToString, moIsClaimAllowedDrop)
                    End If
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateDropdown()
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim noId As Guid = (From lst In oYesNoList
                                Where lst.Code = "N"
                                Select lst.ListItemId).FirstOrDefault()
            UseCoverageStartDateId.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False
                                                   })

            moRecoverDeciveDrop.Populate(oYesNoList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False
                                                   })

            If State.IsCoverageNew Then
                SetSelectedItem(UseCoverageStartDateId, noId)
                SetSelectedItem(moRecoverDeciveDrop, noId)
            Else
                SetSelectedItem(UseCoverageStartDateId, TheCoverage.UseCoverageStartDateId)
                SetSelectedItem(moRecoverDeciveDrop, TheCoverage.RecoverDeviceId)
            End If
            PopulateDepreciationScheduleDropdown()
            moMethodOfRepairDrop.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            SetSelectedItem(moMethodOfRepairDrop, TheCoverage.MethodOfRepairId)

            '
            Dim fulfillmentProviderList = CommonConfigManager.Current.ListManager.GetList("FULFILLMENT_PROVIDER", Thread.CurrentPrincipal.GetLanguageCode())


            moFulfillmentProviderDrop.Populate(fulfillmentProviderList, New PopulateOptions() With
                                                 {.AddBlankItem = True, .BlankItemValue = String.Empty, .ValueFunc = AddressOf PopulateOptions.GetExtendedCode, .SortFunc = AddressOf PopulateOptions.GetDescription})

            ControlMgr.SetEnableControl(Me, moFulfillmentProviderDrop, True)
            If Not String.IsNullOrEmpty(TheCoverage.FulfillmentProviderXcd) Then
                SetSelectedItem(moFulfillmentProviderDrop, TheCoverage.FulfillmentProviderXcd)
            End If

            '
            Dim fulfillmentProfileList = CommonConfigManager.Current.ListManager.GetList(ListCodes.FulfillmentProfile, Thread.CurrentPrincipal.GetLanguageCode())

            moFulfillmentProfileDrop.Populate(fulfillmentProfileList, New PopulateOptions() With
                                             {.AddBlankItem = True, .BlankItemValue = String.Empty, .ValueFunc = AddressOf PopulateOptions.GetCode})

            ControlMgr.SetEnableControl(Me, moFulfillmentProfileDrop, True)
            If Not String.IsNullOrEmpty(TheCoverage.FulfillmentProfileCode) Then
                SetSelectedItem(moFulfillmentProfileDrop, TheCoverage.FulfillmentProfileCode)
            End If

            'BindListControlToDataView(cboDeductibleBasedOn, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(oLanguageId), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.LanguageId = GetLanguageId()
            cboDeductibleBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                  })

            Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, TheCoverage.DeductibleBasedOnId)
            If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__FIXED))
            ElseIf deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION Then
                SetSelectedItem(cboDeductibleBasedOn, TheCoverage.DeductibleExpressionId)
            Else
                SetSelectedItem(cboDeductibleBasedOn, TheCoverage.DeductibleBasedOnId)
            End If
        End Sub

        Private Sub PopulateCoveragePricing()
            Dim oLanguageId As Guid = GetLanguageId()
            If moProductDrop.SelectedIndex = -1 Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                Dim oPriceMatrixView As DataView = LookupListNew.GetPriceMatrixLookupList(oProductId, oLanguageId)
                If oPriceMatrixView.Count > 0 Then
                    moCoveragePricingText.Text = oPriceMatrixView.Item(FirstPos).Item(ColDescriptionName).ToString
                End If

            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub PopulateRetail()
            If moProductDrop.SelectedIndex = -1 Then Return
            Dim oProductId As Guid = GetSelectedItem(moProductDrop)
            Try
                Dim oPercentOfRetailDataview As DataView = LookupListNew.GetPercentOfRetailLookup(oProductId)
                If oPercentOfRetailDataview.Count > 0 Then
                    If oPercentOfRetailDataview.Item(FirstPos).Item(ColCodeName) Is DBNull.Value Then
                        TheCoverage.PercentOfRetail = New DecimalType(0)
                    Else
                        TheCoverage.PercentOfRetail = New DecimalType(CType(oPercentOfRetailDataview.Item(FirstPos).Item(ColCodeName), Decimal))
                    End If
                    PopulateControlFromBOProperty(moRetailText, TheCoverage.PercentOfRetail)

                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)

            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                If IsPostBack And Not State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    moOffsetText.Text = State.SelectedOffset
                    txtMarkupDistPercent.Text = State.SelectedMarkupDistnPercent
                    txtOffsetDays.Text = State.SelectedOffsetDays
                    moEffectiveText.Text = State.SelectedEffective
                    moExpirationText.Text = State.SelectedExpiration
                    moCertificateDurationText.Text = State.SelectedCertificateDuration
                    moCoverageDurationText.Text = State.SelectedCoverageDuration
                    moLiabilityText.Text = State.SelectedLiability
                    moLiabilityLimitPercentText.Text = State.SelectedLiabilityLimitPercent
                    moDeductibleText.Text = State.SelectedDeductible
                    moDeductiblePercentText.Text = State.SelectedDeductiblePercent
                    moCovDeductibleText.Text = State.SelectedCovDeductible
                    moRepairDiscountPctText.Text = State.SelectedRepairDiscountPct
                    moReplacementDiscountPctText.Text = State.SelectedReplacementDiscountPct
                    moAgentcodeText.Text = State.SelectedAgentCode
                    moCoverageLiabilityLimitText.Text = State.SelectedCoverageLiabilityLimit
                    moPerIncidentLiabilityLimitCapText.Text = State.SelectedPerIncidentLiabilityLimitCap
                    moCoverageLiabilityLimitPercentText.Text = State.SelectedCoverageLiabilityLimitPercent
                    moClaimLimitCountText.Text = State.SelectedClaimLimitCount
                    If Not TheDepreciationSchedule.IsDeleted Then
                        BindSelectItem(TheDepreciationSchedule.DepreciationScheduleId.ToString, ddlDepSchCashReimbursement)
                    End If
                    State.IsUndo = False
                Else
                    ' JLR - Otherwise load values from BO unless it is new with copy
                    ' In that case, BO has been cleared but we want to preserve the values 
                    ' already in the screen
                    If Not State.IsNewWithCopy Then
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

                        If (oProductLiabilityLimitBaseId.Equals(Guid.Empty) Or oProductLiabilityLimitBaseId = LookupListNew.GetIdFromCode(LookupListNew.LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, ProdLiabBasedOnNotApp)) Then
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
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm002, GetLanguageId()) & " " & ex.Message)
            End Try
        End Sub

        Private Sub LoadCurrencyOfCoverage()
            If Not State.IsCoverageNew Then
                ControlMgr.SetEnableControl(Me, TextBoxCurrencyOfCoverage, False)
                Dim currencyOfCoveragedv As DataView = Coverage.GetCurrencyOfCoverage(TheCoverage.Id)
                If currencyOfCoveragedv.Count > 1 Or currencyOfCoveragedv.Count = 0 Then
                    Throw New GUIException("", ElitaPlus.Common.ErrorCodes.COVERAGE_NONE_OR_MORE_THAN_ONE_CONTRACT_IN_EFFECT_FOUND_ERR)
                Else
                    PopulateControlFromBOProperty(TextBoxCurrencyOfCoverage, currencyOfCoveragedv.Table.Rows(0).Item(0))
                End If
            Else
                ControlMgr.SetVisibleControl(Me, currLabelDiv, False)
                ControlMgr.SetVisibleControl(Me, currTextBoxDiv, False)
            End If

        End Sub

        Private Sub CoverageMarkupDistribution()
            Dim oContract As Contract
            Try
                oContract = Contract.GetMaxExpirationContract(TheCoverage.DealerId)
                If Not oContract Is Nothing And oContract.AllowCoverageMarkupDistribution.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    ControlMgr.SetVisibleControl(Me, lblMarkupDistPercent, True)
                    ControlMgr.SetVisibleControl(Me, txtMarkupDistPercent, True)
                Else
                    ControlMgr.SetVisibleControl(Me, lblMarkupDistPercent, False)
                    ControlMgr.SetVisibleControl(Me, txtMarkupDistPercent, False)
                End If
            Catch ex As Exception
                ControlMgr.SetVisibleControl(Me, lblMarkupDistPercent, False)
                ControlMgr.SetVisibleControl(Me, txtMarkupDistPercent, False)
            End Try
        End Sub
#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()
            With TheCoverage
                If TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "DealerId", TheDealerControl.SelectedGuid)
                If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "ProductCodeId", moProductDrop)
                If moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "RiskTypeId", moRiskDrop)
                If moCoverageTypeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "CoverageTypeId", moCoverageTypeDrop)
                If moProductItemDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "ProductItemId", moProductItemDrop)
                If moOptionalDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "OptionalId", moOptionalDrop)
                If moIsClaimAllowedDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "IsClaimAllowedId", moIsClaimAllowedDrop)

                'REQ-5358
                TheCoverage.OffsetMethod = GetSelectedValue(moOffsetMethodDrop)
                TheCoverage.OffsetMethodId = LookupListNew.GetIdFromCode("COVERAGE_OFFSET_METHOD", TheCoverage.OffsetMethod)

                If TheCoverage.OffsetMethod = "FIXED" Then
                    PopulateBOProperty(TheCoverage, "OffsetToStart", moOffsetText)
                    PopulateBOProperty(TheCoverage, "OffsetToStartDays", txtOffsetDays)
                Else
                    TheCoverage.OffsetToStart = 0
                    TheCoverage.OffsetToStartDays = 0
                End If

                If moMethodOfRepairDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverage, "MethodOfRepairId", moMethodOfRepairDrop)
                Else
                    TheCoverage.MethodOfRepairId = Nothing
                End If

                If moFulfillmentProviderDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverage, NameOf(TheCoverage.FulfillmentProviderXcd), moFulfillmentProviderDrop, isGuidValue:=False, isStringValue:=True)
                Else
                    TheCoverage.FulfillmentProviderXcd = Nothing
                End If

                If moFulfillmentProfileDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverage, NameOf(TheCoverage.FulfillmentProfileCode), moFulfillmentProfileDrop, isGuidValue:=False, isStringValue:=True)
                Else
                    TheCoverage.FulfillmentProfileCode = Nothing
                End If

                .ItemId = State.SelectedItemId
                PopulateBOProperty(TheCoverage, "CertificateDuration", moCertificateDurationText)
                PopulateBOProperty(TheCoverage, "CoverageDuration", moCoverageDurationText)
                PopulateBOProperty(TheCoverage, "LiabilityLimit", moLiabilityText)
                PopulateBOProperty(TheCoverage, "MarkupDistributionPercent", txtMarkupDistPercent)
                PopulateBOProperty(TheCoverage, "LiabilityLimitPercent", moLiabilityLimitPercentText)
                PopulateBOProperty(TheCoverage, "Deductible", moDeductibleText)
                PopulateBOProperty(TheCoverage, "DeductiblePercent", moDeductiblePercentText)

                'REG-6289
                PopulateBOProperty(TheCoverage, "CoverageClaimLimit", moClaimLimitCountText)

                Dim deductibleBasedOnId As Guid = GetSelectedItem(cboDeductibleBasedOn)
                If (deductibleBasedOnId = Guid.Empty) Then
                    PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", cboDeductibleBasedOn)
                    PopulateBOProperty(TheCoverage, "DeductibleExpressionId", Guid.Empty)
                Else
                    Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)
                    If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                        PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                        PopulateBOProperty(TheCoverage, "DeductibleExpressionId", deductibleBasedOnId)
                    Else
                        PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", cboDeductibleBasedOn)
                        PopulateBOProperty(TheCoverage, "DeductibleExpressionId", Guid.Empty)
                    End If
                End If

                'PopulateBOProperty(TheCoverage, "DeductibleBasedOnId", cboDeductibleBasedOn)
                PopulateBOProperty(TheCoverage, "RepairDiscountPct", moRepairDiscountPctText)
                PopulateBOProperty(TheCoverage, "ReplacementDiscountPct", moReplacementDiscountPctText)
                PopulateBOProperty(TheCoverage, "UseCoverageStartDateId", UseCoverageStartDateId)
                PopulateBOProperty(TheCoverage, "AgentCode", moAgentcodeText)
                PopulateBOProperty(TheCoverage, "CoverageLiabilityLimit", moCoverageLiabilityLimitText)
                PopulateBOProperty(TheCoverage, "CoverageLiabilityLimitPercent", moCoverageLiabilityLimitPercentText)

                PopulateBOProperty(TheCoverage, "IsReInsuredId", moReInsuredDrop)

                If Len(moEffectiveText.Text) > 0 Then
                    PopulateBOProperty(TheCoverage, "Effective", DateHelper.GetDateValue(moEffectiveText.Text).ToString)
                Else
                    PopulateBOProperty(TheCoverage, "Effective", "")
                End If

                If Len(moExpirationText.Text) > 0 Then
                    PopulateBOProperty(TheCoverage, "Expiration", DateHelper.GetDateValue(moExpirationText.Text).ToString)
                Else
                    PopulateBOProperty(TheCoverage, "Expiration", "")
                End If

                If Not TheDepreciationSchedule.IsDeleted Then
                    PopulateBOProperty(TheDepreciationSchedule, "DepreciationScheduleId", ddlDepSchCashReimbursement)
                    TheCoverage.AddCoverageDepreciationScdChild(TheDepreciationSchedule.DepreciationScheduleId)
                End If

                PopulateBOProperty(TheCoverage, "PerIncidentLiabilityLimitCap", moPerIncidentLiabilityLimitCapText)

                If moEarningCodeDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "EarningCodeId", moEarningCodeDrop)
                If moRecoverDeciveDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then PopulateBOProperty(TheCoverage, "RecoverDeviceId", moRecoverDeciveDrop)
                PopulateBOProperty(TheCoverage, "TaxTypeXCD", moTaxTypeDrop, False, True)

                'US-489839
                PopulateCoverageRateLiabilityLimitBOFromForm()

                'US-521697
                CommonSourceOptionLogic()

                'US-489839
                ValidateRateRenewalNo()
                ValidateRateLimitAndPercent()
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Private Function IsDirtyBo() As Boolean
            Dim bIsDirty As Boolean
            Try
                TheCoverage.UniqueFieldsChanged = UniqueFieldsChanged()
                With TheCoverage
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                    If bIsDirty = False Then bIsDirty = IsDirtyRateBo()
                End With

            Catch ex As Exception
                Throw New PopulateBOErrorException
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False
            Dim isFamilyDirty As Boolean = False

            With TheCoverage
                If UniqueFieldsChanged() And .IsLastCoverage() = False And .IsFirstCoverage = False Then
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.COVERAGEBO_019, GetLanguageId()))
                    .cancelEdit()
                    bIsOk = False
                Else
                    bIsDirty = IsDirtyBo()
                    isFamilyDirty = .IsFamilyDirty
                    .Save()
                    EnableUniqueFields()

                    LoadCoverageRateList()
                    If Not bIsDirty Then
                        If Not State.CoverageRateList Is Nothing Then
                            For Each coverageRate As CoverageRate In State.CoverageRateList
                                If coverageRate.IsDirty Then
                                    bIsDirty = coverageRate.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    LoadCoverageDeductibleList()
                    If Not bIsDirty Then
                        If Not State.CoverageDeductibleList Is Nothing Then
                            For Each coverageDeductible As CoverageDeductible In State.CoverageDeductibleList
                                If coverageDeductible.IsDirty Then
                                    bIsDirty = coverageDeductible.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    LoadCoverageConseqDamageList()
                    If Not bIsDirty Then
                        If Not State.CoverageConseqDamageList Is Nothing Then
                            For Each coverageConseqDamage As CoverageConseqDamage In State.CoverageConseqDamageList
                                If coverageConseqDamage.IsDirty Then
                                    bIsDirty = coverageConseqDamage.IsDirty
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If SaveCoverageRateList() Then
                        EnableCoverageButtons(True)
                        If SaveCoverageDeductibleList() Then
                            ManageDeductibleButtons(True, False)
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
                If bIsDirty = True OrElse isFamilyDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
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
                    If Coverage.GetAssociatedCertificateCount(.Id) > 0 Then
                        MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.DELETE_COVERAGE_WITH_CERT_PRESENT, GetLanguageId()))
                        .cancelEdit()
                        bIsOk = False
                    ElseIf .IsLastCoverage() = False And .IsFirstCoverage = False Then
                        MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.COVERAGEBO_017, GetLanguageId()))
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
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm003, GetLanguageId()))
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
                If State.IsCoverageNew Then
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
            moGridView.Columns(ColIndexCommissionsPercent).Visible = bIsReadWrite
            moGridView.Columns(ColIndexMarketingPercent).Visible = bIsReadWrite
            moGridView.Columns(ColIndexAdminExpense).Visible = bIsReadWrite
            moGridView.Columns(ColIndexProfitExpense).Visible = bIsReadWrite
            moGridView.Columns(ColIndexLossCostPercent).Visible = bIsReadWrite

            'US-521697
            If State.IsDealerConfiguredForSourceXcd Then
                moGridView.Columns(ColIndexCommissionsPercentXcd).Visible = bIsReadWrite
                moGridView.Columns(ColIndexMarketingPercentXcd).Visible = bIsReadWrite
                moGridView.Columns(ColIndexAdminExpenseXcd).Visible = bIsReadWrite
                moGridView.Columns(ColIndexProfitExpenseXcd).Visible = bIsReadWrite
                moGridView.Columns(ColIndexLossCostPercentXcd).Visible = bIsReadWrite
            Else
                moGridView.Columns(ColIndexCommissionsPercentXcd).Visible = False
                moGridView.Columns(ColIndexMarketingPercentXcd).Visible = False
                moGridView.Columns(ColIndexAdminExpenseXcd).Visible = False
                moGridView.Columns(ColIndexProfitExpenseXcd).Visible = False
                moGridView.Columns(ColIndexLossCostPercentXcd).Visible = False
            End If
        End Sub
        Private Sub DisplayHideSourceColumn()
            'US-521697
            If State.IsDealerConfiguredForSourceXcd Then
                moGridView.Columns(ColIndexCommissionsPercentXcd).Visible = True
                moGridView.Columns(ColIndexMarketingPercentXcd).Visible = True
                moGridView.Columns(ColIndexAdminExpenseXcd).Visible = True
                moGridView.Columns(ColIndexProfitExpenseXcd).Visible = True
                moGridView.Columns(ColIndexLossCostPercentXcd).Visible = True
            Else
                moGridView.Columns(ColIndexCommissionsPercentXcd).Visible = False
                moGridView.Columns(ColIndexMarketingPercentXcd).Visible = False
                moGridView.Columns(ColIndexAdminExpenseXcd).Visible = False
                moGridView.Columns(ColIndexProfitExpenseXcd).Visible = False
                moGridView.Columns(ColIndexLossCostPercentXcd).Visible = False
            End If
        End Sub

        Private Sub DisplayHideCovLiabilityColumn()
            'US-489838
            If State.IsProductConfiguredForRenewalNo Then
                moGridView.Columns(ColIndexCovLiabilityLimit).Visible = True
                moGridView.Columns(ColIndexCovLiabilityLimitPercent).Visible = True
            Else
                moGridView.Columns(ColIndexCovLiabilityLimit).Visible = False
                moGridView.Columns(ColIndexCovLiabilityLimitPercent).Visible = False
            End If
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateCoverageRateList(Optional ByVal oAction As String = ActionNone)
            Dim oDataView As DataView

            If State.IsCoverageNew = True And Not State.IsNewWithCopy Then Return ' We can not have CoverageRates if the coverage is new

            Try

                If CoveragePricingCode = NoCoveragePricing Then
                    EnableCoveragePricing(False)
                Else
                    EnableCoveragePricing(True)
                End If

                If State.IsNewWithCopy Then
                    oDataView = CoverageRate.GetList(Guid.Empty)
                    If Not oAction = ActionCancelDelete Then LoadCoverageRateList()
                    If Not State.CoverageRateList Is Nothing Then
                        oDataView = GetDataViewFromArray(State.CoverageRateList, oDataView.Table)
                    End If
                Else
                    oDataView = CoverageRate.GetList(TheCoverage.Id)
                End If

                Select Case oAction
                    Case ActionNone
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView, 0)
                        EnableForEditRateButtons(False)
                    Case ActionSave
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ActionCancelDelete
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ActionEdit
                        If State.IsNewWithCopy Then
                            CoverageRateId = State.CoverageRateList(moGridView.SelectedIndex).Id.ToString
                        Else
                            CoverageRateId = GetSelectedGridText(moGridView, ColIndexCoverageRateId)
                        End If
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)
                    Case ActionNew
                        If State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DbCoverageRateId) = TheCoverageRate.Id.ToByteArray
                        oDataView.Table.Rows.Add(oRow)
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)

                End Select

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

                'US 489838
                SetSequenceFlag()

                If State.IsProductConfiguredForRenewalNo Then
                    moGridView.Columns(ColIndexCovLiabilityLimit).Visible = True
                    moGridView.Columns(ColIndexCovLiabilityLimitPercent).Visible = True
                Else
                    moGridView.Columns(ColIndexCovLiabilityLimit).Visible = False
                    moGridView.Columns(ColIndexCovLiabilityLimitPercent).Visible = False
                End If

            Catch ex As Exception
                moMsgControllerRate.AddError(CoverageForm004)
                moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
            End Try
        End Sub

        Private Function GetDataViewFromArray(ByVal oArray() As CoverageDeductible, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageDeductible As CoverageDeductible
            Dim languageId As Guid = GetLanguageId()
            Dim dvMethodOfRepairList As DataView = LookupListNew.GetMethodOfRepairLookupList(languageId)
            Dim deductibleBasedOn As DataView = LookupListNew.GetComputeDeductibleBasedOnLookupList(languageId)
            If Not oArray Is Nothing Then
                For Each oCoverageDeductible In oArray
                    If Not oCoverageDeductible Is Nothing Then
                        oRow = oDtable.NewRow
                        oRow(CoverageDeductible.COL_NAME_COVERAGE_DED_ID) = oCoverageDeductible.Id.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_METHOD_OF_REPAIR_ID) = oCoverageDeductible.MethodOfRepairId.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_METHOD_OF_REPAIR) = LookupListNew.GetDescriptionFromId(dvMethodOfRepairList, oCoverageDeductible.MethodOfRepairId)
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE_BASED_ON_ID) = oCoverageDeductible.DeductibleBasedOnId.ToByteArray
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE_BASED_ON) = LookupListNew.GetDescriptionFromId(deductibleBasedOn, oCoverageDeductible.DeductibleBasedOnId)
                        oRow(CoverageDeductible.COL_NAME_DEDUCTIBLE) = oCoverageDeductible.Deductible.Value
                        oDtable.Rows.Add(oRow)
                    End If
                Next
            End If
            Return oDtable.DefaultView

        End Function
        Private Function GetDataViewFromArray(ByVal oArray() As CoverageConseqDamage, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageConseqDamage As CoverageConseqDamage

            If Not oArray Is Nothing Then
                For Each oCoverageConseqDamage In oArray
                    If Not oCoverageConseqDamage Is Nothing Then
                        oRow = oDtable.NewRow
                        oRow(CoverageConseqDamage.COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID) = oCoverageConseqDamage.Id.ToByteArray
                        oRow(CoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_PER_INCIDENT) = oCoverageConseqDamage.LiabilityLimitPerIncident.Value
                        oRow(CoverageConseqDamage.COL_NAME_CONSEQ_DAMAGE_TYPE) = oCoverageConseqDamage.ConseqDamageTypeXcd
                        oRow(CoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_CUMULATIVE) = oCoverageConseqDamage.LiabilityLimitCumulative.Value
                        oRow(CoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_BASED_ON) = oCoverageConseqDamage.LiabilityLimitBaseXcd
                        oRow(CoverageConseqDamage.COL_NAME_FULFILMENT_METHOD_XCD) = oCoverageConseqDamage.FulfilmentMethodXcd
                        If Not oCoverageConseqDamage.Effective Is Nothing Then
                            oRow(CoverageConseqDamage.COL_NAME_EFFECTIVE) = oCoverageConseqDamage.Effective.Value
                        End If
                        If Not oCoverageConseqDamage.Expiration Is Nothing Then
                            oRow(CoverageConseqDamage.COL_NAME_EXPIRATION) = oCoverageConseqDamage.Expiration.Value
                        End If
                        oRow(CoverageConseqDamage.COL_NAME_CONSEQ_DAMAGE_TYPE_DESC) = LookupListNew.GetDescrionFromListCode("PERILTYP", oCoverageConseqDamage.ConseqDamageTypeXcd.Replace("PERILTYP-", ""))
                        oRow(CoverageConseqDamage.COL_NAME_LIABILITY_LIMIT_BASED_ON_DESC) = LookupListNew.GetDescrionFromListCode("PRODLILIMBASEDON", oCoverageConseqDamage.LiabilityLimitBaseXcd.Replace("PRODLILIMBASEDON-", ""))
                        oRow(CoverageConseqDamage.COL_NAME_FULFILMENT_METHOD_DESC) = LookupListNew.GetDescrionFromListCode("FULFILMETH", oCoverageConseqDamage.FulfilmentMethodXcd.Replace("FULFILMETH-", ""))

                        oDtable.Rows.Add(oRow)
                    End If
                Next
            End If
            Return oDtable.DefaultView

        End Function
        Private Function GetDataViewFromArray(ByVal oArray() As CoverageRate, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oCoverageRate As CoverageRate
            For Each oCoverageRate In oArray
                If Not oCoverageRate Is Nothing Then
                    oRow = oDtable.NewRow

                    oRow(CoverageRateDAL.COL_NAME_COVERAGE_RATE_ID) = oCoverageRate.Id.ToByteArray
                    oRow(CoverageRateDAL.COL_NAME_LOW_PRICE) = oCoverageRate.LowPrice.Value
                    oRow(CoverageRateDAL.COL_NAME_HIGH_PRICE) = oCoverageRate.HighPrice.Value
                    oRow(CoverageRateDAL.COL_NAME_GROSS_AMT) = oCoverageRate.GrossAmt.Value
                    oRow(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT) = oCoverageRate.CommissionsPercent.Value
                    oRow(CoverageRateDAL.COL_NAME_MARKETING_PERCENT) = oCoverageRate.MarketingPercent.Value
                    oRow(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE) = oCoverageRate.AdminExpense.Value
                    oRow(CoverageRateDAL.COL_NAME_PROFIT_EXPENSE) = oCoverageRate.ProfitExpense.Value
                    oRow(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT) = oCoverageRate.LossCostPercent.Value
                    oRow(CoverageRateDAL.COL_NAME_GROSS_AMOUNT_PERCENT) = oCoverageRate.GrossAmountPercent.Value
                    oRow(CoverageRateDAL.COL_NAME_RENEWAL_NUMBER) = oCoverageRate.RenewalNumber.Value

                    oDtable.Rows.Add(oRow)
                End If
            Next
            oDtable.DefaultView.Sort() = CoverageRateDAL.COL_NAME_LOW_PRICE
            Return oDtable.DefaultView

        End Function

        Private Sub ValidateCoverage()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)

            If yesId = GetSelectedItem(UseCoverageStartDateId) Then
                Dim oCoverageRate As CoverageRate
                LoadCoverageRateList()
                If (Not State.CoverageRateList Is Nothing) Then
                    If State.CoverageRateList.Count > 1 Then
                        PopulateBOProperty(TheCoverage, "UseCoverageStartDateId", UseCoverageStartDateId)
                        moUseCoverageStartDateLable.ForeColor = Color.Red
                        Throw New GUIException(Message.MSG_INVALID_COVERAGE, ElitaPlus.Common.ErrorCodes.INVALID_MULTIPLE_COVERAGES_NOT_ALLOWED)
                    Else
                        For Each oCoverageRate In State.CoverageRateList
                            If Not oCoverageRate Is Nothing Then
                                If oCoverageRate.GrossAmt.Value > 0 Then
                                    moUseCoverageStartDateLable.ForeColor = Color.Red
                                    Throw New GUIException(Message.MSG_INVALID_COVERAGE, ElitaPlus.Common.ErrorCodes.INVALID_COVERAGE_GROSS_AMT)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

        Private Sub PopulateCoverageRate()
            If State.IsNewWithCopy Then
                With State.CoverageRateList(moGridView.SelectedIndex)
                    SetSelectedGridText(moGridView, ColIndexLowPrice, .LowPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexHighPrice, .HighPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexGrossAmt, .GrossAmt.ToString)
                    SetSelectedGridText(moGridView, ColIndexCommissionsPercent, .CommissionsPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexMarketingPercent, .MarketingPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexAdminExpense, .AdminExpense.ToString)
                    SetSelectedGridText(moGridView, ColIndexProfitExpense, .ProfitExpense.ToString)
                    SetSelectedGridText(moGridView, ColIndexLossCostPercent, .LossCostPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexLowPrice, .LowPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexGrossAmountPercent, .GrossAmountPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexRenewalNumber, .RenewalNumber.ToString)
                End With
            Else
                With TheCoverageRate
                    SetSelectedGridText(moGridView, ColIndexLowPrice, .LowPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexHighPrice, .HighPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexGrossAmt, .GrossAmt.ToString)
                    SetSelectedGridText(moGridView, ColIndexCommissionsPercent, .CommissionsPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexMarketingPercent, .MarketingPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexAdminExpense, .AdminExpense.ToString)
                    SetSelectedGridText(moGridView, ColIndexProfitExpense, .ProfitExpense.ToString)
                    SetSelectedGridText(moGridView, ColIndexLossCostPercent, .LossCostPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexLowPrice, .LowPrice.ToString)
                    SetSelectedGridText(moGridView, ColIndexGrossAmountPercent, .GrossAmountPercent.ToString)
                    SetSelectedGridText(moGridView, ColIndexRenewalNumber, .RenewalNumber.ToString)
                End With
            End If
            PopulateCoverageRateLiabilityLimitFromBO()
            PopulateTaxRegionFromCoverageRateBo()
        End Sub

        Private Sub PopulateTaxRegionFromCoverageRateBo()
            'ensure that grid's edit index is set before this gets a call
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim ddlTaxRegion As DropDownList = CType(gRow.Cells(ColIndexRegionId).FindControl("ddlTax_Region"), DropDownList)

            If State.IsNewWithCopy Then
                With State.CoverageRateList(moGridView.SelectedIndex)
                    If Not .RegionId = Guid.Empty Then
                        PopulateControlFromBOProperty(ddlTaxRegion, .RegionId)
                    End If
                End With
            Else
                With TheCoverageRate
                    If Not .RegionId = Guid.Empty Then
                        PopulateControlFromBOProperty(ddlTaxRegion, .RegionId)
                    End If
                End With
            End If
        End Sub

        Private Sub FillDropdownList()

            'fill the drop downs
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim moIsCoveredDrop As DropDownList = DirectCast(gRow.Cells(ColIndexRegionId).FindControl("ddlTax_Region"), DropDownList)

            With State.CoverageRateList
                If Not moIsCoveredDrop Is Nothing Then
                    PopulateRegionDropdown(moIsCoveredDrop)

                End If
            End With
        End Sub

        Private Sub PopulateRegionDropdown(ByVal oDropDownList As DropDownList)
            Try
                Dim regionList As New List(Of DataElements.ListItem)

                For Each countryId As Guid In CurrentUser().Countries
                    Dim regions As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.RegionsByCountry,
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .CountryId = countryId
                                                                            })

                    If regions.Count > 0 Then
                        If Not regionList Is Nothing Then
                            regionList.AddRange(regions)
                        Else
                            regionList = regions.Clone()
                        End If
                    End If
                Next

                oDropDownList.Populate(regionList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

            Catch ex As Exception
            End Try
        End Sub


#End Region

#Region "Populate Conseq Damage"

        Private Sub PopulateCoverageConseqDamageList(Optional ByVal oAction As String = ActionNone)
            Dim oDataView As DataView

            If State.IsCoverageNew = True And Not State.IsNewWithCopy Then Return ' We can not have CoverageConseqDamages if the coverage is new

            Try

                If State.IsNewWithCopy Then
                    oDataView = CoverageConseqDamage.GetList(Guid.Empty, GetLanguageId())

                    If Not oAction = ActionCancelDelete Then LoadCoverageConseqDamageList()

                    If Not State.CoverageConseqDamageList Is Nothing Then
                        oDataView = GetDataViewFromArray(State.CoverageConseqDamageList, oDataView.Table)
                    End If
                Else
                    oDataView = CoverageConseqDamage.GetList(TheCoverage.Id, GetLanguageId())
                End If

                Select Case oAction
                    Case ActionNone
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridViewConseqDamage, 0)
                        EnableForEditConseqDamageButtons(False)
                    Case ActionSave
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex)
                        EnableForEditConseqDamageButtons(False)
                    Case ActionCancelDelete
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex)
                        EnableForEditConseqDamageButtons(False)
                    Case ActionEdit
                        If State.IsNewWithCopy Then
                            CoverageConseqDamageId = State.CoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex).Id.ToString
                        Else
                            CoverageConseqDamageId = GetSelectedGridText(moGridViewConseqDamage, ColSeqCoverageConseqDamageId)
                        End If
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex, True)
                        EnableForEditConseqDamageButtons(True)
                    Case ActionNew
                        If State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DbCoverageConseqDamageId) = TheCoverageConseqDamage.Id.ToByteArray
                        oRow(DbCoverageId) = TheCoverage.Id.ToByteArray
                        oRow(DbLiablilityLimitPerIncident) = "0"
                        oRow(DbLiablilityLimitCumulative) = "0"
                        oDataView.Table.Rows.Add(oRow)
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageConseqDamageId), moGridViewConseqDamage,
                                    moGridViewConseqDamage.PageIndex, True)
                        EnableForEditConseqDamageButtons(True)
                End Select

                moGridViewConseqDamage.DataSource = oDataView
                moGridViewConseqDamage.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridViewConseqDamage)

            Catch ex As Exception
                moMsgControllerConseqDamage.AddError(CoverageForm006)
                moMsgControllerConseqDamage.AddError(ex.Message, False)
                'moMsgControllerConseqDamage.Show()
            End Try
        End Sub

        Private Sub PopulateCoverageConseqDamage()
            If State.IsNewWithCopy Then
                With State.CoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex)
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqLiabilityLimitPerIncident, .LiabilityLimitPerIncident.ToString)
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqLiabilityLimitCumulative, .LiabilityLimitCumulative.ToString)
                    If Not .Effective Is Nothing Then
                        SetSelectedGridText(moGridViewConseqDamage, ColSeqConseqDamageEffectiveDate, .Effective.Value.ToString)
                    End If
                    If Not .Expiration Is Nothing Then
                        SetSelectedGridText(moGridViewConseqDamage, ColSeqConseqDamageExpirationDate, .Expiration.Value.ToString)
                    End If
                    If Not .ConseqDamageTypeXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqConseqDamageType).FindControl("moConseqDamageTypeDropdown"), DropDownList), .ConseqDamageTypeXcd)
                    End If
                    If Not .LiabilityLimitBaseXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqLiabilityLimitBasedOn).FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList), .LiabilityLimitBaseXcd)
                    End If
                    If Not .FulfilmentMethodXcd Is Nothing Then
                        SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqFulfillmentMethod).FindControl("moFulfilmentMethodDropdown"), DropDownList), .FulfilmentMethodXcd)
                    End If

                End With
            Else
                With TheCoverageConseqDamage
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqLiabilityLimitPerIncident, .LiabilityLimitPerIncident.ToString)
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqLiabilityLimitCumulative, .LiabilityLimitCumulative.ToString)
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqConseqDamageEffectiveDate, .Effective.ToString)
                    SetSelectedGridText(moGridViewConseqDamage, ColSeqConseqDamageExpirationDate, .Expiration.ToString)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqConseqDamageType).FindControl("moConseqDamageTypeDropdown"), DropDownList), .ConseqDamageTypeXcd)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqLiabilityLimitBasedOn).FindControl("moLiabilityLimitBasedOnDropdown"), DropDownList), .LiabilityLimitBaseXcd)
                    SetSelectedItem(CType(moGridViewConseqDamage.Rows(moGridViewConseqDamage.SelectedIndex).Cells(ColSeqFulfillmentMethod).FindControl("moFulfilmentMethodDropdown"), DropDownList), .FulfilmentMethodXcd)
                End With
            End If
        End Sub

#End Region

#Region "Button Management ConseqDamage"

#End Region

#Region "Business Part"

        Private Sub PopulateRateBoFromForm()
            With TheCoverageRate
                .CoverageId = TheCoverage.Id
                PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.LowPrice), CType(GetSelectedGridControl(moGridView, ColIndexLowPrice), TextBox))
                PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.HighPrice), CType(GetSelectedGridControl(moGridView, ColIndexHighPrice), TextBox))
                ''Gross Amount Percent is set to 0


                If hdnGrossAmtOrPercent.Value = "Percent" Then
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmt), hdnGrossAmtOrPercentValue.Value)
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmountPercent), "0.00")
                ElseIf hdnGrossAmtOrPercent.Value = "Amount" Then
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmountPercent), hdnGrossAmtOrPercentValue.Value)
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmt), "0.00")
                Else
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmountPercent), CType(GetSelectedGridControl(moGridView, ColIndexGrossAmountPercent), TextBox))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.GrossAmt), CType(GetSelectedGridControl(moGridView, ColIndexGrossAmt), TextBox))
                End If
                hdnGrossAmtOrPercent.Value = String.Empty
                hdnGrossAmtOrPercentValue.Value = String.Empty
                If CoveragePricingCode = NoCoveragePricing Then
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CommissionsPercent), GetAmountFormattedDoubleString("0"))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.MarketingPercent), GetAmountFormattedDoubleString("0"))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.AdminExpense), GetAmountFormattedDoubleString("0"))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.ProfitExpense), GetAmountFormattedDoubleString("0"))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.LossCostPercent), GetAmountFormattedDoubleString("0"))
                Else
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CommissionsPercent), CType(GetSelectedGridControl(moGridView, ColIndexCommissionsPercent), TextBox))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.MarketingPercent), CType(GetSelectedGridControl(moGridView, ColIndexMarketingPercent), TextBox))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.AdminExpense), CType(GetSelectedGridControl(moGridView, ColIndexAdminExpense), TextBox))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.ProfitExpense), CType(GetSelectedGridControl(moGridView, ColIndexProfitExpense), TextBox))
                    PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.LossCostPercent), CType(GetSelectedGridControl(moGridView, ColIndexLossCostPercent), TextBox))
                End If
                PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.RenewalNumber), CType(GetSelectedGridControl(moGridView, ColIndexRenewalNumber), TextBox))
                PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.RegionId), CType(GetDropDownControlFromGrid(moGridView, ColIndexRegionId), DropDownList))

                PopulateCoverageRateLiabilityLimitBOFromForm()

                CommonSourceOptionLogic()

                'US-489839            
                ValidateRateRenewalNo()
                ValidateRateLimitAndPercent()
            End With

            ValidateCoverage()

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyRateBo() As Boolean
            Dim bIsDirty As Boolean = True
            If moGridView.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' Coverage Rate is not in edit mode
            Try
                With TheCoverageRate
                    PopulateRateBoFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                moMsgControllerRate.AddError(CoverageForm004)
                moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyRateChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridView.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode
            If State.IsNewWithCopy Then
                LoadCoverageRateList()
                State.CoverageRateList(moGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewRate = False Then
                CoverageRateId = GetSelectedGridText(moGridView, ColIndexCoverageRateId)
            End If
            BindBoPropertiesToGridHeader()
            With TheCoverageRate
                PopulateRateBoFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditRateButtons(False)
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

        ' The user selected a specific CoverageRate to Delete
        Private Function DeleteSelectedCoverageRate(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If State.IsNewWithCopy Then
                    If State.CoverageRateList Is Nothing Then LoadCoverageRateList()
                    State.CoverageRateList(nIndex) = Nothing
                Else
                    With TheCoverageRate()
                        .delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerRate.AddError(CoverageForm005)
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
                Dim oCoverageRate As CoverageRate = New CoverageRate(New Guid(CType(oRow(DbCoverageRateId), Byte())))
                oCoverageRate.delete()
                oCoverageRate.Save()
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
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
                oRows = CoverageRate.GetCovRateListForDelete(TheCoverage.Id).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageRate(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
                moMsgControllerRate.AddError(ex.Message)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteSelectedCoverageConseqDamage(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If State.IsNewWithCopy Then
                    If State.CoverageConseqDamageList Is Nothing Then LoadCoverageConseqDamageList()
                    State.CoverageConseqDamageList(nIndex) = Nothing
                Else
                    With TheCoverageConseqDamage()
                        .Delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerConseqDamage.AddError(CoverageForm006)
                moMsgControllerConseqDamage.AddError(ex.Message, False)
                'moMsgControllerConseqDamage.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteACoverageConseqDamage(ByVal oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim oCoverageConseqDamage As CoverageConseqDamage = New CoverageConseqDamage(New Guid(CType(oRow(DbCoverageConseqDamageId), Byte())))
                oCoverageConseqDamage.Delete()
                oCoverageConseqDamage.Save()
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
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
                oRows = CoverageConseqDamage.GetList(TheCoverage.Id, GetLanguageId()).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageConseqDamage(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerRate.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
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
                If State.IsNewWithCopy Then
                    If State.CoverageDeductibleList Is Nothing Then LoadCoverageDeductibleList()
                    State.CoverageDeductibleList(nIndex) = Nothing
                Else
                    With TheCoverageDeductible()
                        .Delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                moMsgControllerDeductible.AddError(CoverageForm006)
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
                Dim oCoverageDeductible As CoverageDeductible = New CoverageDeductible(New Guid(CType(oRow(DbCoverageDeductibleId), Byte())))
                oCoverageDeductible.Delete()
                oCoverageDeductible.Save()
            Catch ex As Exception
                moMsgControllerDeductible.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
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
                oRows = CoverageDeductible.GetList(TheCoverage.Id, GetLanguageId()).Table.Rows
                For Each oRow In oRows
                    If DeleteACoverageDeductible(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moMsgControllerDeductible.AddError(TranslationBase.TranslateLabelOrMessage(CoverageForm005, GetLanguageId()))
                moMsgControllerDeductible.AddError(ex.Message)
                'moMsgControllerDeductible.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Sub PopulateDeductibleBoFromForm()
            With TheCoverageDeductible
                .CoverageId = TheCoverage.Id
                PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.MethodOfRepairId), CType(GetSelectedGridControl(dedGridView, ColSeqMethodOfRepairDescription), DropDownList), True)

                Dim ddlDeductibleBasedOn As DropDownList = CType(GetSelectedGridControl(dedGridView, ColSeqDeductibleBasedOnDesc), DropDownList)

                Dim deductibleBasedOnId As Guid = GetSelectedItem(ddlDeductibleBasedOn)
                If (deductibleBasedOnId = Guid.Empty) Then
                    PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleBasedOnId), ddlDeductibleBasedOn)
                    PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleExpressionId), Guid.Empty)
                Else
                    Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)
                    If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                        PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleBasedOnId), LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                        PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleExpressionId), deductibleBasedOnId)
                    Else
                        PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleBasedOnId), ddlDeductibleBasedOn)
                        PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.DeductibleExpressionId), Guid.Empty)
                    End If
                End If

                Dim result As Decimal
                If (Not (Decimal.TryParse(CType(GetSelectedGridControl(dedGridView, ColSeqDeductible), TextBox).Text, result))) Then
                    Throw New GUIException(ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_VALUE, ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_VALUE)
                End If
                PopulateBOProperty(TheCoverageDeductible, NameOf(CoverageDeductible.Deductible), CType(GetSelectedGridControl(dedGridView, ColSeqDeductible), TextBox).Text)
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function GetDropDownControlFromGrid(ByVal oDataGrid As GridView, ByVal cellPosition As Integer) As Control
            Dim oItem As GridViewRow = oDataGrid.Rows(oDataGrid.SelectedIndex)
            Dim oControl As Control

            For Each gridControl As Control In oItem.Cells(cellPosition).Controls

                If gridControl.GetType().FullName.Equals("System.Web.UI.WebControls.DropDownList") Then
                    oControl = gridControl
                End If
            Next

            Return oControl
        End Function
#End Region

#End Region

#Region "State-Management"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            HiddenSaveChangesPromptResponse.Value = String.Empty
            Dim retType As New CoverageSearchForm.ReturnType(DetailPageCommand.Back, State.CoverageId, State.StateChanged)
            If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then

                If State.ActionInProgress <> DetailPageCommand.BackOnErr Then
                    PopulateBOsFromForm()
                    TheCoverage.Save()
                End If
                Select Case State.ActionInProgress
                    Case DetailPageCommand.Delete
                        State.StateChanged = True
                        retType = New CoverageSearchForm.ReturnType(DetailPageCommand.Delete, State.CoverageId)
                        retType.BoChanged = True
                        ReturnToCallingPage(retType)
                    Case DetailPageCommand.Back
                        ReturnToCallingPage(retType)
                    Case DetailPageCommand.New_
                        'AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        CreateNew()
                    Case DetailPageCommand.NewAndCopy
                        ' AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        CreateNewCopy()
                    Case DetailPageCommand.BackOnErr
                        ' moErrorController.AddErrorAndShow(State.LastErrMsg)
                        MasterPage.MessageController.AddError(State.LastErrMsg, True)
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case DetailPageCommand.Back
                        ReturnToCallingPage(retType)
                    Case DetailPageCommand.New_
                        CreateNew()
                    Case DetailPageCommand.NewAndCopy
                        CreateNewCopy()
                    Case DetailPageCommand.BackOnErr
                        ReturnToCallingPage(retType)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = String.Empty
        End Sub
        Protected Sub CheckIfComingFromConfirm()
            ' ComingFromBack()
            CheckIfComingFromSaveConfirm()
            'Clean after consuming the action
            HiddenSaveChangesPromptResponse.Value = String.Empty
        End Sub


        Protected Sub moddl_DeductibleBasedOn_SelectedIndexChanged(sender As Object, e As EventArgs)
            Dim ddlDeductibleBasedOn As DropDownList = DirectCast(sender, DropDownList)
            Dim deductibleId As Guid = GetSelectedItem((ddlDeductibleBasedOn))
            Dim deductibleCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleId)

            Dim row As GridViewRow = DirectCast(ddlDeductibleBasedOn.NamingContainer, GridViewRow)
            Dim txtDeductible As TextBox = DirectCast(dedGridView.Rows()(row.RowIndex).FindControl("motxt_Deductible"), TextBox)
            If Not ((deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) OrElse
                                           (deductibleCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then

                txtDeductible.Text = ZeroNumber
                txtDeductible.Enabled = False
                txtDeductible.Style.Add("background", "#F0F0F0")

            Else
                txtDeductible.Enabled = True

            End If

        End Sub

        Private Sub moReInsuredDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moReInsuredDrop.SelectedIndexChanged

            If State.IsCoverageNew = True Then
                If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupNo) OrElse GetSelectedItem(moReInsuredDrop).Equals(Guid.Empty) Then
                    If Not TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                        Dim attributeValueBo As AttributeValue = TheCoverage.AttributeValues.First
                        attributeValueBo.Delete()
                        attributeValueBo.Save()
                    End If
                Else
                    AttributeValues.Visible = True
                End If
            Else
                If GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupNo) OrElse GetSelectedItem(moReInsuredDrop).Equals(Guid.Empty) Then

                    If Not TheCoverage.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                        Dim attributeValueBo As AttributeValue = TheCoverage.AttributeValues.First
                        attributeValueBo.Delete()
                        attributeValueBo.Save()
                        AttributeValues.DataBind()
                    End If
                ElseIf GetSelectedItem(moReInsuredDrop) = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, LookupYes) Then
                    TheCoverage.ClearAttributeValues()

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
                PopulateCoverageConseqDamageList(ActionSave)
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
            End If
        End Sub

        Private Sub btnSaveConseqDamage_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveConseqDamage_WRITE.Click
            Try
                SaveConseqDamageChanges()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerConseqDamage)
            End Try
        End Sub

        Private Sub btnCancelConseqDamage_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelConseqDamage_WRITE.Click
            Try
                IsNewConseqDamage = False
                EnableForEditConseqDamageButtons(False)
                PopulateCoverageConseqDamageList(ActionCancelDelete)
                EnableDisableControls(moCoverageEditPanel, False)
                Setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moProductDrop, False)
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerConseqDamage)
            End Try
        End Sub

        Private Sub btnNewConseqDamage_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewConseqDamage_WRITE.Click
            Try
                IsNewConseqDamage = True
                CoverageConseqDamageId = Guid.Empty.ToString
                PopulateCoverageConseqDamageList(ActionNew)
                SetGridControls(moGridViewConseqDamage, False)
                SetFocusInGrid(moGridViewConseqDamage, moGridViewConseqDamage.SelectedIndex, ColSeqLiabilityLimitPerIncident)
                EnableDisableControls(moCoverageEditPanel, True)
                If btnNewConseqDamage_WRITE.Enabled = False Then
                    Setbuttons(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, moMsgControllerConseqDamage)
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

#End Region

#Region "Business Part"

        Private Function ApplyConseqDamageChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridViewConseqDamage.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode
            If State.IsNewWithCopy Then
                LoadCoverageConseqDamageList()
                State.CoverageConseqDamageList(moGridViewConseqDamage.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewConseqDamage = False Then
                CoverageConseqDamageId = GetSelectedGridText(moGridViewConseqDamage, ColSeqCoverageConseqDamageId)
            End If
            BindBoPropertiesToConseqDamageGridHeader()
            With TheCoverageConseqDamage
                PopulateConseqDamageBoFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditConseqDamageButtons(False)
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

        Private Sub PopulateConseqDamageBoFromForm()
            With TheCoverageConseqDamage
                .CoverageId = TheCoverage.Id

                PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.ConseqDamageTypeXcd), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqConseqDamageType), DropDownList), False, True)
                PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitBaseXcd), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqLiabilityLimitBasedOn), DropDownList), False, True)
                PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.FulfilmentMethodXcd), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqFulfillmentMethod), DropDownList), False, True)
                PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.Effective), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqConseqDamageEffectiveDate), TextBox))
                PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.Expiration), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqConseqDamageExpirationDate), TextBox))

                If Not String.IsNullOrEmpty(CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqLiabilityLimitCumulative), TextBox).Text) Then
                    PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitCumulative), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqLiabilityLimitCumulative), TextBox))
                Else
                    .LiabilityLimitCumulative = 0.00
                End If

                If Not String.IsNullOrEmpty(CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqLiabilityLimitPerIncident), TextBox).Text) Then
                    PopulateBOProperty(TheCoverageConseqDamage, NameOf(TheCoverageConseqDamage.LiabilityLimitPerIncident), CType(GetSelectedGridControl(moGridViewConseqDamage, ColSeqLiabilityLimitPerIncident), TextBox))
                Else
                    .LiabilityLimitPerIncident = 0.00
                End If
            End With
            'ValidateCoverage()
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EnableTabsCoverageConseqDamage()
            Try
                If (TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX) Then
                    Dim oDealerId As Guid = TheDealerControl.SelectedGuid
                    If Not (oDealerId = Guid.Empty) Then
                        Dim oDealer As Dealer = New Dealer(oDealerId)
                        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                        If oDealer IsNot Nothing AndAlso oDealer.ClaimRecordingXcd IsNot Nothing Then
                            If (oDealer.UseClaimAuthorizationId.Equals(yesId) AndAlso
                            (oDealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) OrElse
                            oDealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH))) Then
                                DisabledTabsList.Remove(TabCoverageConseqDamage)
                            Else
                                If Not (DisabledTabsList.Contains(TabCoverageConseqDamage)) Then
                                    DisabledTabsList.Add(TabCoverageConseqDamage)
                                End If
                            End If
                        Else
                            If Not (DisabledTabsList.Contains(TabCoverageConseqDamage)) Then
                                DisabledTabsList.Add(TabCoverageConseqDamage)
                            End If
                        End If
                    Else
                        If Not (DisabledTabsList.Contains(TabCoverageConseqDamage)) Then
                            DisabledTabsList.Add(TabCoverageConseqDamage)
                        End If
                    End If
                Else
                    If Not (DisabledTabsList.Contains(TabCoverageConseqDamage)) Then
                        DisabledTabsList.Add(TabCoverageConseqDamage)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Clear"
        Private Sub ClearCoverageConseqDamage()
            If Not State.IsNewWithCopy Then
                EnableConseqDamageButtons(False)
                moGridViewConseqDamage.DataBind()
            End If
        End Sub
#End Region


#End Region

#Region "Acct Source Xcd Option Bucket Logic"
        ' US - 521697
        Private Sub CommonSourceOptionLogic()

            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim mocboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
            Dim mocboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
            Dim mocboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
            Dim mocboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

            If State.IsDealerConfiguredForSourceXcd Then

                If mocboLossCostPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverageRate, "LossCostPercentSourceXcd", mocboLossCostPercentSourceXcd, False, True)
                End If

                If mocboAdminExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverageRate, "AdminExpenseSourceXcd", mocboAdminExpenseSourceXcd, False, True)
                End If

                If mocboProfitExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverageRate, "ProfitPercentSourceXcd", mocboProfitExpenseSourceXcd, False, True)
                End If

                If mocboCommPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverageRate, "CommissionsPercentSourceXcd", mocboCommPercentSourceXcd, False, True)
                End If

                If mocboMarketingExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    PopulateBOProperty(TheCoverageRate, "MarketingPercentSourceXcd", mocboMarketingExpenseSourceXcd, False, True)
                End If

                If State.IsIgnorePremiumSetYesForContract Then

                    ValidateIncomingSourceXcd()

                    If State.IsBucketIncomingSelected Then
                        Throw New GUIException(Message.MSG_INCOMING_OPTION_NOT_ALLOWED, ElitaPlus.Common.ErrorCodes.MSG_INCOMING_OPTION_NOT_ALLOWED_WHEN_IGNORE_PREMIUM_IS_YES)
                    End If
                End If

                ValidateDifferenceSourceXcd()

                If State.IsDiffSelectedTwice Then
                    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ALLOWED_ONLY_ONCE, ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_SOURCE_ALLOWED_ONLY_FOR_ONE_BUCKET)
                ElseIf State.IsDiffNotSelectedOnce Then
                    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ATLEAST_ONE, ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_OPTION_SHOULD_PRESENT_ATLEAST_FOR_ONE_BUCKET)
                End If
            End If
        End Sub

        Private Sub ValidateDifferenceSourceXcd()

            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim mocboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
            Dim mocboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
            Dim mocboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
            Dim mocboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

            State.IsDiffSelectedTwice = False
            State.IsDiffNotSelectedOnce = False
            Dim countDiff As Integer = 0

            If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If mocboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If mocboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If mocboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If mocboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If countDiff > 1 Then
                State.IsDiffSelectedTwice = True
            ElseIf countDiff < 1 Then
                State.IsDiffNotSelectedOnce = True
            End If

        End Sub

        Private Sub ValidateIncomingSourceXcd()

            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim cboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim cboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
            Dim cboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
            Dim cboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
            Dim cboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

            State.IsBucketIncomingSelected = False

            If cboCommPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                State.IsBucketIncomingSelected = True
            ElseIf cboMarketingExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                State.IsBucketIncomingSelected = True
            ElseIf cboAdminExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                State.IsBucketIncomingSelected = True
            ElseIf cboProfitExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                State.IsBucketIncomingSelected = True
            ElseIf cboLossCostPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                State.IsBucketIncomingSelected = True
            End If

        End Sub

        Private Sub PopulateSourceOptionDropDownList(ByVal oDropDownList As DropDownList)
            If (State.Coverage.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(State.Coverage.DealerId)

                If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then

                        DisplaySourceXcdFields()

                        Dim oAcctBucketsSourceOption As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE")

                        oDropDownList.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                            {
                                            .AddBlankItem = False,
                                            .TextFunc = AddressOf PopulateOptions.GetDescription,
                                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                            })
                    Else
                        HideSourceScdFields()
                    End If
                Else
                    HideSourceScdFields()
                End If
            Else
                HideSourceScdFields()
            End If
        End Sub

        Private Sub DisplaySourceXcdFields()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim mocboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
            Dim mocboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
            Dim mocboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
            Dim mocboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

            With State.CoverageRateList
                If Not mocboCommPercentSourceXcd Is Nothing Then
                    mocboCommPercentSourceXcd.Visible = True
                End If

                If Not mocboMarketingExpenseSourceXcd Is Nothing Then
                    mocboMarketingExpenseSourceXcd.Visible = True
                End If

                If Not mocboAdminExpenseSourceXcd Is Nothing Then
                    mocboAdminExpenseSourceXcd.Visible = True
                End If

                If Not mocboProfitExpenseSourceXcd Is Nothing Then
                    mocboProfitExpenseSourceXcd.Visible = True
                End If

                If Not mocboLossCostPercentSourceXcd Is Nothing Then
                    mocboLossCostPercentSourceXcd.Visible = True
                End If
            End With
        End Sub

        Private Sub HideSourceScdFields()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim mocboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
            Dim mocboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
            Dim mocboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
            Dim mocboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

            With State.CoverageRateList

                If Not mocboCommPercentSourceXcd Is Nothing Then
                    mocboCommPercentSourceXcd.Visible = False
                End If

                If Not mocboMarketingExpenseSourceXcd Is Nothing Then
                    mocboMarketingExpenseSourceXcd.Visible = False
                End If

                If Not mocboAdminExpenseSourceXcd Is Nothing Then
                    mocboAdminExpenseSourceXcd.Visible = False
                End If

                If Not mocboProfitExpenseSourceXcd Is Nothing Then
                    mocboProfitExpenseSourceXcd.Visible = False
                End If

                If Not mocboLossCostPercentSourceXcd Is Nothing Then
                    mocboLossCostPercentSourceXcd.Visible = False
                End If
            End With

        End Sub

        Private Sub FillSourceXcdDropdownList()

            'fill the drop downs
            If moGridView.EditIndex = -1 Then Exit Sub
            If State.IsDealerConfiguredForSourceXcd Then

                Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
                Dim mocboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
                Dim mocboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
                Dim mocboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
                Dim mocboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

                If Not mocboCommPercentSourceXcd Is Nothing Then
                    PopulateSourceOptionDropDownList(mocboCommPercentSourceXcd)
                End If

                If Not mocboMarketingExpenseSourceXcd Is Nothing Then
                    PopulateSourceOptionDropDownList(mocboMarketingExpenseSourceXcd)
                End If

                If Not mocboAdminExpenseSourceXcd Is Nothing Then
                    PopulateSourceOptionDropDownList(mocboAdminExpenseSourceXcd)
                End If

                If Not mocboProfitExpenseSourceXcd Is Nothing Then
                    PopulateSourceOptionDropDownList(mocboProfitExpenseSourceXcd)
                End If

                If Not mocboLossCostPercentSourceXcd Is Nothing Then
                    PopulateSourceOptionDropDownList(mocboLossCostPercentSourceXcd)
                End If
            End If
        End Sub

        Private Sub SetGridSourceXcdLabelFromBo()
            'If moGridView.EditIndex = -1 Then Exit Sub
            If State.IsDealerConfiguredForSourceXcd Then
                For pageIndexk As Integer = 0 To moGridView.PageCount - 1
                    moGridView.PageIndex = pageIndexk
                    Dim rowNum As Integer = moGridView.Rows.Count
                    For i As Integer = 0 To rowNum - 1
                        Dim gRow As GridViewRow = moGridView.Rows(i)
                        If gRow.RowType = DataControlRowType.DataRow Then
                            Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("lblCommPercentSourceXcd"), Label)
                            Dim mollblMarketingExpenseSourceXcd As Label = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("lblMarketingExpenseSourceXcd"), Label)
                            Dim molblAdminExpenseSourceXcd As Label = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("lblAdminExpenseSourceXcd"), Label)
                            Dim molblProfitExpenseSourceXcd As Label = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("lblProfitExpenseSourceXcd"), Label)
                            Dim molblLossCostPercentSourceXcd As Label = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("lblLossCostPercentSourceXcd"), Label)

                            If Not mollblCommPercentSourceXcd Is Nothing Then
                                If mollblCommPercentSourceXcd.Visible Then
                                    If (Not mollblCommPercentSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(mollblCommPercentSourceXcd.Text)) Then
                                        mollblCommPercentSourceXcd.Text = GetCodeAmtSourceOption(mollblCommPercentSourceXcd.Text)
                                    End If
                                End If
                            End If

                            If Not mollblMarketingExpenseSourceXcd Is Nothing Then
                                If mollblMarketingExpenseSourceXcd.Visible Then
                                    If (Not mollblMarketingExpenseSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(mollblMarketingExpenseSourceXcd.Text)) Then
                                        mollblMarketingExpenseSourceXcd.Text = GetCodeAmtSourceOption(mollblMarketingExpenseSourceXcd.Text)
                                    End If
                                End If
                            End If

                            If Not molblAdminExpenseSourceXcd Is Nothing Then
                                If molblAdminExpenseSourceXcd.Visible Then
                                    If (Not molblAdminExpenseSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblAdminExpenseSourceXcd.Text)) Then
                                        molblAdminExpenseSourceXcd.Text = GetCodeAmtSourceOption(molblAdminExpenseSourceXcd.Text)
                                    End If
                                End If
                            End If

                            If Not molblProfitExpenseSourceXcd Is Nothing Then
                                If molblProfitExpenseSourceXcd.Visible Then
                                    If (Not molblProfitExpenseSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblProfitExpenseSourceXcd.Text)) Then
                                        molblProfitExpenseSourceXcd.Text = GetCodeAmtSourceOption(molblProfitExpenseSourceXcd.Text)
                                    End If
                                End If
                            End If

                            If Not molblLossCostPercentSourceXcd Is Nothing Then
                                If molblLossCostPercentSourceXcd.Visible Then
                                    If (Not molblLossCostPercentSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblLossCostPercentSourceXcd.Text)) Then
                                        molblLossCostPercentSourceXcd.Text = GetCodeAmtSourceOption(molblLossCostPercentSourceXcd.Text)
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End Sub

        Private Sub SetGridSourceXcdDropdownFromBo()
            If moGridView.EditIndex = -1 Then Exit Sub
            If State.IsDealerConfiguredForSourceXcd Then
                Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                Dim cboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
                Dim cboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
                Dim cboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
                Dim cboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
                Dim cboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

                Dim txtCommissionPercent As TextBox = DirectCast(gRow.Cells(ColIndexCommissionsPercent).FindControl("moCommission_PercentText"), TextBox)
                Dim txtMarketingPercent As TextBox = DirectCast(gRow.Cells(ColIndexMarketingPercent).FindControl("moMarketing_PercentText"), TextBox)
                Dim txtAdminExpense As TextBox = DirectCast(gRow.Cells(ColIndexAdminExpense).FindControl("moAdmin_ExpenseText"), TextBox)
                Dim txtProfitExpense As TextBox = DirectCast(gRow.Cells(ColIndexProfitExpense).FindControl("moProfit_ExpenseText"), TextBox)
                Dim txtLossCostPercent As TextBox = DirectCast(gRow.Cells(ColIndexLossCostPercent).FindControl("moLoss_Cost_PercentText"), TextBox)
                Dim diffValue As Decimal = 0.0000

                If State.IsNewWithCopy = False Then
                    With TheCoverageRate

                        If cboLossCostPercentSourceXcd.Visible Then
                            If Not .LossCostPercentSourceXcd Is Nothing And cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboLossCostPercentSourceXcd, .LossCostPercentSourceXcd)

                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(txtLossCostPercent, diffValue, PERCENT_FORMAT)
                                    txtLossCostPercent.Enabled = False
                                Else
                                    txtLossCostPercent.Enabled = True
                                End If
                            End If
                        End If

                        If cboProfitExpenseSourceXcd.Visible Then
                            If Not .ProfitPercentSourceXcd Is Nothing And cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboProfitExpenseSourceXcd, .ProfitPercentSourceXcd)

                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(txtProfitExpense, diffValue, PERCENT_FORMAT)
                                    txtProfitExpense.Enabled = False
                                Else
                                    txtProfitExpense.Enabled = True
                                End If
                            End If
                        End If

                        If cboMarketingExpenseSourceXcd.Visible Then
                            If Not .MarketingPercentSourceXcd Is Nothing And cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboMarketingExpenseSourceXcd, .MarketingPercentSourceXcd)

                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(txtMarketingPercent, diffValue, PERCENT_FORMAT)
                                    txtMarketingPercent.Enabled = False
                                Else
                                    txtMarketingPercent.Enabled = True
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd.Visible Then
                            If Not .AdminExpenseSourceXcd Is Nothing And cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboAdminExpenseSourceXcd, .AdminExpenseSourceXcd)

                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(txtAdminExpense, diffValue, PERCENT_FORMAT)
                                    txtAdminExpense.Enabled = False
                                Else
                                    txtAdminExpense.Enabled = True
                                End If
                            End If
                        End If

                        If cboCommPercentSourceXcd.Visible Then
                            If Not .CommissionsPercentSourceXcd Is Nothing And cboCommPercentSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboCommPercentSourceXcd, .CommissionsPercentSourceXcd)

                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(txtCommissionPercent, diffValue, PERCENT_FORMAT)
                                    txtCommissionPercent.Enabled = False
                                Else
                                    txtCommissionPercent.Enabled = True
                                End If
                            End If
                        End If
                    End With
                End If
            End If
        End Sub

        Private Sub SetGridSourceXcdTextBoxFromBo()
            If moGridView.EditIndex = -1 Then Exit Sub
            With TheCoverageRate
                If State.IsDealerConfiguredForSourceXcd Then
                    Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                    Dim cboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
                    Dim cboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
                    Dim cboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
                    Dim cboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
                    Dim cboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

                    Dim txtCommissionPercent As TextBox = DirectCast(gRow.Cells(ColIndexCommissionsPercent).FindControl("moCommission_PercentText"), TextBox)
                    Dim txtMarketingPercent As TextBox = DirectCast(gRow.Cells(ColIndexMarketingPercent).FindControl("moMarketing_PercentText"), TextBox)
                    Dim txtAdminExpense As TextBox = DirectCast(gRow.Cells(ColIndexAdminExpense).FindControl("moAdmin_ExpenseText"), TextBox)
                    Dim txtProfitExpense As TextBox = DirectCast(gRow.Cells(ColIndexProfitExpense).FindControl("moProfit_ExpenseText"), TextBox)
                    Dim txtLossCostPercent As TextBox = DirectCast(gRow.Cells(ColIndexLossCostPercent).FindControl("moLoss_Cost_PercentText"), TextBox)

                    Dim diffValue As Decimal = 0.0000

                    If State.IsNewWithCopy Then
                        With State.CoverageRateList(moGridView.SelectedIndex)
                            If cboLossCostPercentSourceXcd.Visible Then
                                If Not .LossCostPercent Is Nothing And cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                    If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtLossCostPercent, diffValue, PERCENT_FORMAT)
                                        txtLossCostPercent.Enabled = False
                                    ElseIf cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtLossCostPercent, .LossCostPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboProfitExpenseSourceXcd.Visible Then
                                If Not .ProfitExpense Is Nothing And cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                    If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtProfitExpense, diffValue, PERCENT_FORMAT)
                                        txtProfitExpense.Enabled = False
                                    ElseIf cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtProfitExpense, .ProfitExpense, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboMarketingExpenseSourceXcd.Visible Then
                                If Not .MarketingPercent Is Nothing And cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                    If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtMarketingPercent, diffValue, PERCENT_FORMAT)
                                        txtMarketingPercent.Enabled = False
                                    ElseIf cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtMarketingPercent, .MarketingPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboAdminExpenseSourceXcd.Visible Then
                                If Not .AdminExpense Is Nothing And cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                    If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtAdminExpense, diffValue, PERCENT_FORMAT)
                                        txtAdminExpense.Enabled = False
                                    ElseIf cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtAdminExpense, .AdminExpense, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboCommPercentSourceXcd.Visible Then
                                If Not .CommissionsPercent Is Nothing And cboCommPercentSourceXcd.Items.Count > 0 Then
                                    If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtCommissionPercent, diffValue, PERCENT_FORMAT)
                                        txtCommissionPercent.Enabled = False
                                    ElseIf cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtCommissionPercent, .CommissionsPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If
                        End With
                    Else
                        With TheCoverageRate
                            If cboLossCostPercentSourceXcd.Visible Then
                                If Not .LossCostPercent Is Nothing And cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                    If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtLossCostPercent, diffValue, PERCENT_FORMAT)
                                        txtLossCostPercent.Enabled = False
                                    ElseIf cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtLossCostPercent, .LossCostPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboProfitExpenseSourceXcd.Visible Then
                                If Not .ProfitExpense Is Nothing And cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                    If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtProfitExpense, diffValue, PERCENT_FORMAT)
                                        txtProfitExpense.Enabled = False
                                    ElseIf cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtProfitExpense, .ProfitExpense, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboMarketingExpenseSourceXcd.Visible Then
                                If Not .MarketingPercent Is Nothing And cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                    If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtMarketingPercent, diffValue, PERCENT_FORMAT)
                                        txtMarketingPercent.Enabled = False
                                    ElseIf cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtMarketingPercent, .MarketingPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboAdminExpenseSourceXcd.Visible Then
                                If Not .AdminExpense Is Nothing And cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                    If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtAdminExpense, diffValue, PERCENT_FORMAT)
                                        txtAdminExpense.Enabled = False
                                    ElseIf cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtAdminExpense, .AdminExpense, PERCENT_FORMAT)
                                    End If
                                End If
                            End If

                            If cboCommPercentSourceXcd.Visible Then
                                If Not .CommissionsPercent Is Nothing And cboCommPercentSourceXcd.Items.Count > 0 Then
                                    If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                        PopulateControlFromBOProperty(txtCommissionPercent, diffValue, PERCENT_FORMAT)
                                        txtCommissionPercent.Enabled = False
                                    ElseIf cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
                                        PopulateControlFromBOProperty(txtCommissionPercent, .CommissionsPercent, PERCENT_FORMAT)
                                    End If
                                End If
                            End If
                        End With
                    End If
                End If
            End With

        End Sub

        Private Sub SetGridSourceXcdTextBoxForNewCoverage()
            If moGridView.EditIndex = -1 Then Exit Sub
            With TheCoverageRate
                If State.IsDealerConfiguredForSourceXcd Then
                    Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                    Dim cboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexCommissionsPercentXcd).FindControl("cboCommPercentSourceXcd"), DropDownList)
                    Dim cboMarketingExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexMarketingPercentXcd).FindControl("cboMarketingExpenseSourceXcd"), DropDownList)
                    Dim cboAdminExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexAdminExpenseXcd).FindControl("cboAdminExpenseSourceXcd"), DropDownList)
                    Dim cboProfitExpenseSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexProfitExpenseXcd).FindControl("cboProfitExpenseSourceXcd"), DropDownList)
                    Dim cboLossCostPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(ColIndexLossCostPercentXcd).FindControl("cboLossCostPercentSourceXcd"), DropDownList)

                    Dim txtCommissionPercent As TextBox = DirectCast(gRow.Cells(ColIndexCommissionsPercent).FindControl("moCommission_PercentText"), TextBox)
                    Dim txtMarketingPercent As TextBox = DirectCast(gRow.Cells(ColIndexMarketingPercent).FindControl("moMarketing_PercentText"), TextBox)
                    Dim txtAdminExpense As TextBox = DirectCast(gRow.Cells(ColIndexAdminExpense).FindControl("moAdmin_ExpenseText"), TextBox)
                    Dim txtProfitExpense As TextBox = DirectCast(gRow.Cells(ColIndexProfitExpense).FindControl("moProfit_ExpenseText"), TextBox)
                    Dim txtLossCostPercent As TextBox = DirectCast(gRow.Cells(ColIndexLossCostPercent).FindControl("moLoss_Cost_PercentText"), TextBox)

                    Dim diffValue As Decimal = 0.0000

                    If cboLossCostPercentSourceXcd.Visible Then
                        If cboLossCostPercentSourceXcd.Items.Count > 0 Then
                            If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(txtLossCostPercent, diffValue, PERCENT_FORMAT)
                                txtLossCostPercent.Enabled = False
                            End If
                        End If
                    End If

                    If cboProfitExpenseSourceXcd.Visible Then
                        If cboProfitExpenseSourceXcd.Items.Count > 0 Then
                            If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(txtProfitExpense, diffValue, PERCENT_FORMAT)
                                txtProfitExpense.Enabled = False
                            End If
                        End If
                    End If

                    If cboMarketingExpenseSourceXcd.Visible Then
                        If cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                            If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(txtMarketingPercent, diffValue, PERCENT_FORMAT)
                                txtMarketingPercent.Enabled = False
                            End If
                        End If
                    End If

                    If cboAdminExpenseSourceXcd.Visible Then
                        If cboAdminExpenseSourceXcd.Items.Count > 0 Then
                            If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(txtAdminExpense, diffValue, PERCENT_FORMAT)
                                txtAdminExpense.Enabled = False
                            End If
                        End If
                    End If

                    If cboCommPercentSourceXcd.Visible Then
                        If cboCommPercentSourceXcd.Items.Count > 0 Then
                            If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(txtCommissionPercent, diffValue, PERCENT_FORMAT)
                                txtCommissionPercent.Enabled = False
                            End If
                        End If
                    End If

                End If
            End With

        End Sub

        Public Function GetCodeAmtSourceOption(ByVal desc As String) As String
            Dim sGetCodeSourceOptionDesc As String
            Try
                sGetCodeSourceOptionDesc = String.Empty
                If Not desc Is Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetCodeSourceOptionDesc = LookupListNew.GetDescriptionFromExtCode("ACCTBUCKETSOURCE", GetLanguageId(), desc)
                End If
                Return sGetCodeSourceOptionDesc
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            Return String.Empty
        End Function

#End Region

#Region "Coverage Rate Liability Limit US-489838"
        Protected Function CheckNull(ByVal objGrid As Object) As String
            If Object.ReferenceEquals(objGrid, DBNull.Value) Then
                Return String.Empty
            ElseIf TypeOf objGrid Is Byte() Then
                Return GetGuidStringFromByteArray(objGrid)
            Else
                If objGrid.ToString().Equals(Guid.Empty.ToString()) Then
                    Return String.Empty
                End If

                Return objGrid.ToString()
            End If
        End Function

        Private Sub PopulateCoverageRateLiabilityLimitBOFromForm()
            If State.IsProductConfiguredForRenewalNo Then
                If moGridView.EditIndex = -1 Then Exit Sub
                Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                Dim TextBoxLiabilityLimit As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimit).FindControl("moLiability_LimitText"), TextBox)
                Dim TextBoxLiabilityLimitPercent As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moLiability_LimitPercentText"), TextBox)

                If Not TextBoxLiabilityLimit Is Nothing Then
                    If (String.IsNullOrWhiteSpace(TextBoxLiabilityLimit.Text)) Then
                        Dim tempTextBoxLiabilityLimit As TextBox = New TextBox
                        tempTextBoxLiabilityLimit.Text = String.Empty
                        Me.PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CovLiabilityLimit), tempTextBoxLiabilityLimit)
                    Else
                        PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CovLiabilityLimit), CType(GetSelectedGridControl(moGridView, ColIndexCovLiabilityLimit), TextBox))
                    End If
                End If

                If Not TextBoxLiabilityLimitPercent Is Nothing Then
                    If (String.IsNullOrWhiteSpace(TextBoxLiabilityLimitPercent.Text)) Then
                        Dim tempTextBoxLiabilityLimitPercent As TextBox = New TextBox
                        tempTextBoxLiabilityLimitPercent.Text = String.Empty
                        Me.PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CovLiabilityLimitPercent), tempTextBoxLiabilityLimitPercent)
                    Else
                        PopulateBOProperty(TheCoverageRate, NameOf(CoverageRate.CovLiabilityLimitPercent), CType(GetSelectedGridControl(moGridView, ColIndexCovLiabilityLimitPercent), TextBox))
                    End If
                End If
            End If
        End Sub

        Private Sub PopulateCoverageRateLiabilityLimitFromBO()
            If State.IsProductConfiguredForRenewalNo Then
                'ensure that grid's edit index is set before this gets a call
                If moGridView.EditIndex = -1 Then Exit Sub
                Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                Dim TextBoxLiabilityLimit As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimit).FindControl("moLiability_LimitText"), TextBox)
                Dim TextBoxLiabilityLimitPercent As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moLiability_LimitPercentText"), TextBox)

                If State.IsNewWithCopy Then
                    With State.CoverageRateList(moGridView.SelectedIndex)
                        If Not .CovLiabilityLimit Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(.CovLiabilityLimit) Then
                                PopulateControlFromBOProperty(TextBoxLiabilityLimit, .CovLiabilityLimit)
                            End If
                        End If

                        If Not .CovLiabilityLimitPercent Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(.CovLiabilityLimitPercent) Then
                                PopulateControlFromBOProperty(TextBoxLiabilityLimitPercent, .CovLiabilityLimitPercent)
                            End If
                        End If
                    End With
                Else
                    With TheCoverageRate
                        If Not .CovLiabilityLimit Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(.CovLiabilityLimit) Then
                                PopulateControlFromBOProperty(TextBoxLiabilityLimit, .CovLiabilityLimit)
                            End If
                        End If

                        If Not .CovLiabilityLimitPercent Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(.CovLiabilityLimitPercent) Then
                                PopulateControlFromBOProperty(TextBoxLiabilityLimitPercent, .CovLiabilityLimitPercent)
                            End If
                        End If
                    End With
                End If
            End If
        End Sub

        Private Sub CheckRenewalNumberNotInSequence()
            If State.IsProductConfiguredForRenewalNo Then
                Dim LastRenewalNo As Decimal
                Dim LastLowPrice As Decimal
                Dim LastHighPrice As Decimal

                Dim enteredRenewalNo As Decimal
                Dim enteredLowPrice As Decimal
                Dim enteredHighPrice As Decimal

                Dim isSameLowHighExist As Boolean = False
                Dim isNotSequence As Boolean = False
                Dim isFirstRenewalNoNotZero As Boolean = False
                Dim isFirstRowForEdit As Boolean = False

                For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                    Me.moGridView.PageIndex = pageIndexk
                    Dim rowNum As Integer = Me.moGridView.Rows.Count
                    For i As Integer = 0 To rowNum - 1
                        Dim gRow As GridViewRow = moGridView.Rows(i)
                        If gRow.RowType = DataControlRowType.DataRow Then

                            Dim lblLowPrice As Label = DirectCast(gRow.Cells(ColIndexLowPrice).FindControl("moLowPriceLabel"), Label)
                            Dim textBoxLowPrice As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moLowPriceText"), TextBox)

                            Dim lblHighPrice As Label = DirectCast(gRow.Cells(ColIndexHighPrice).FindControl("moHigh_PriceLabel"), Label)
                            Dim textBoxHighPrice As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moHigh_PriceText"), TextBox)

                            Dim lblRenewalNo As Label = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moRenewal_NumberLabel"), Label)
                            Dim textBoxRenewalNo As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moRenewal_NumberText"), TextBox)

                            If i <> 0 Then
                                'If label and row is greater than 1
                                If Not lblRenewalNo Is Nothing And Not lblLowPrice Is Nothing And Not lblHighPrice Is Nothing Then
                                    If Not String.IsNullOrWhiteSpace(lblRenewalNo.Text) And Not String.IsNullOrWhiteSpace(lblLowPrice.Text) And Not String.IsNullOrWhiteSpace(lblHighPrice.Text) Then
                                        Dim existingRenewalNo As Decimal = Convert.ToDecimal(lblRenewalNo.Text)
                                        Dim existingLowPrice As Decimal = Convert.ToDecimal(lblLowPrice.Text)
                                        Dim existingHighPrice As Decimal = Convert.ToDecimal(lblHighPrice.Text)

                                        If Not TheCoverageRate Is Nothing Then
                                            If Not TheCoverageRate.LowPrice Is Nothing And Not TheCoverageRate.HighPrice Is Nothing And Not TheCoverageRate.RenewalNumber Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(TheCoverageRate.LowPrice.ToString()) And Not String.IsNullOrWhiteSpace(TheCoverageRate.HighPrice.ToString()) And Not String.IsNullOrWhiteSpace(TheCoverageRate.RenewalNumber.ToString()) Then
                                                    If TheCoverageRate.LowPrice = existingLowPrice And TheCoverageRate.HighPrice = existingHighPrice Then
                                                        isSameLowHighExist = True

                                                        enteredRenewalNo = TheCoverageRate.RenewalNumber
                                                        enteredLowPrice = TheCoverageRate.LowPrice
                                                        enteredHighPrice = TheCoverageRate.HighPrice

                                                        LastRenewalNo = existingRenewalNo
                                                        LastLowPrice = existingLowPrice
                                                        LastHighPrice = existingHighPrice
                                                    End If
                                                End If
                                            End If
                                        End If

                                    End If
                                End If

                                'If textbox and row is greater than 1
                                If Not textBoxRenewalNo Is Nothing And Not textBoxLowPrice Is Nothing And Not textBoxHighPrice Is Nothing Then
                                    If Not String.IsNullOrWhiteSpace(textBoxRenewalNo.Text) And Not String.IsNullOrWhiteSpace(textBoxLowPrice.Text) And Not String.IsNullOrWhiteSpace(textBoxHighPrice.Text) Then
                                        enteredRenewalNo = Convert.ToDecimal(textBoxRenewalNo.Text)
                                        enteredLowPrice = Convert.ToDecimal(textBoxLowPrice.Text)
                                        enteredHighPrice = Convert.ToDecimal(textBoxHighPrice.Text)

                                        If TheCoverageRate.LowPrice = LastLowPrice And TheCoverageRate.HighPrice = LastHighPrice Then
                                            isSameLowHighExist = True

                                            enteredRenewalNo = TheCoverageRate.RenewalNumber
                                            enteredLowPrice = TheCoverageRate.LowPrice
                                            enteredHighPrice = TheCoverageRate.HighPrice

                                            If enteredRenewalNo <> LastRenewalNo + 1 Then
                                                isNotSequence = True
                                                Exit For
                                            End If
                                        Else
                                            If enteredRenewalNo <> 0 Then
                                                isNotSequence = True
                                                Exit For
                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                'If lable and first row
                                If Not lblRenewalNo Is Nothing And Not lblLowPrice Is Nothing And Not lblHighPrice Is Nothing Then
                                    If Not String.IsNullOrWhiteSpace(lblRenewalNo.Text) And Not String.IsNullOrWhiteSpace(lblLowPrice.Text) And Not String.IsNullOrWhiteSpace(lblHighPrice.Text) Then

                                        Dim existingRenewalNo As Decimal = Convert.ToDecimal(lblRenewalNo.Text)
                                        Dim existingLowPrice As Decimal = Convert.ToDecimal(lblLowPrice.Text)
                                        Dim existingHighPrice As Decimal = Convert.ToDecimal(lblHighPrice.Text)

                                        If existingRenewalNo <> 0 Then
                                            isFirstRenewalNoNotZero = True
                                            Exit For
                                        End If

                                        LastRenewalNo = existingRenewalNo
                                        LastLowPrice = existingLowPrice
                                        LastHighPrice = existingHighPrice

                                        If Not TheCoverageRate Is Nothing Then
                                            If Not TheCoverageRate.LowPrice Is Nothing And Not TheCoverageRate.HighPrice Is Nothing And Not TheCoverageRate.RenewalNumber Is Nothing Then
                                                If Not String.IsNullOrWhiteSpace(TheCoverageRate.LowPrice.ToString()) And Not String.IsNullOrWhiteSpace(TheCoverageRate.HighPrice.ToString()) And Not String.IsNullOrWhiteSpace(TheCoverageRate.RenewalNumber.ToString()) Then
                                                    If TheCoverageRate.LowPrice = existingLowPrice And TheCoverageRate.HighPrice = existingHighPrice Then
                                                        isSameLowHighExist = True

                                                        enteredRenewalNo = TheCoverageRate.RenewalNumber
                                                        enteredLowPrice = TheCoverageRate.LowPrice
                                                        enteredHighPrice = TheCoverageRate.HighPrice

                                                        LastRenewalNo = existingRenewalNo
                                                        LastLowPrice = existingLowPrice
                                                        LastHighPrice = existingHighPrice
                                                    End If
                                                End If
                                            End If
                                        End If

                                    End If
                                End If

                                'If textbox and first row
                                If Not textBoxRenewalNo Is Nothing And Not textBoxLowPrice Is Nothing And Not textBoxHighPrice Is Nothing Then
                                    If Not String.IsNullOrWhiteSpace(textBoxRenewalNo.Text) And Not String.IsNullOrWhiteSpace(textBoxLowPrice.Text) And Not String.IsNullOrWhiteSpace(textBoxHighPrice.Text) Then
                                        isFirstRowForEdit = True
                                        enteredRenewalNo = Convert.ToDecimal(textBoxRenewalNo.Text)
                                        enteredLowPrice = Convert.ToDecimal(textBoxLowPrice.Text)
                                        enteredHighPrice = Convert.ToDecimal(textBoxHighPrice.Text)

                                        If enteredRenewalNo <> 0 Then
                                            isFirstRenewalNoNotZero = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next

                If isSameLowHighExist Then
                    If Not isFirstRowForEdit Then
                        If enteredRenewalNo <> LastRenewalNo + 1 Then
                            isNotSequence = True
                        End If
                    End If
                End If

                If isFirstRenewalNoNotZero Then
                    Me.State.IsRateFirstRenewalNoIsNotZero = True
                Else
                    Me.State.IsRateFirstRenewalNoIsNotZero = False
                End If

                If isNotSequence Then
                    Me.State.IsRateRenewalNoNotInSequence = True
                Else
                    Me.State.IsRateRenewalNoNotInSequence = False
                End If
            End If
        End Sub

        Private Sub ValidateRateRenewalNo()
            If State.IsProductConfiguredForRenewalNo Then
                CheckRenewalNumberNotInSequence()

                If Me.State.IsRateFirstRenewalNoIsNotZero Then
                    Me.State.IsRateFirstRenewalNoIsNotZero = False
                    Throw New GUIException(Message.MSG_FIRST_RENEWAL_NOT_ZERO, Assurant.ElitaPlus.Common.ErrorCodes.MSG_FIRST_RENEWAL_SHOULD_BE_ZERO)
                End If

                If Me.State.IsRateRenewalNoNotInSequence Then
                    Me.State.IsRateRenewalNoNotInSequence = False
                    Throw New GUIException(Message.MSG_RENEWAL_NOT_SEQUENCE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_RENEWAL_SHOULD_ALWAYS_BE_IN_SEQUENCE)
                End If
            End If
        End Sub

        Private Sub CheckRateLimitAndPercentBothPresent()
            Dim countLimitPer As Int16 = 0
            Dim countLimit As Int16 = 0

            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim lblLimitPer As Label = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("lblLiabilityLimitPercent"), Label)
                        Dim textBoxLimitPer As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moLiability_LimitPercentText"), TextBox)

                        Dim lblLimit As Label = DirectCast(gRow.Cells(ColIndexCovLiabilityLimit).FindControl("lblLiabilityLimit"), Label)
                        Dim textBoxLimit As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimit).FindControl("moLiability_LimitText"), TextBox)

                        Dim lblRenewalNo As Label = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moRenewal_NumberLabel"), Label)
                        Dim textBoxRenewalNo As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moRenewal_NumberText"), TextBox)

                        If Not textBoxLimitPer Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(textBoxLimitPer.Text) Then
                                'Dim cPer As Decimal = Convert.ToDecimal(textBoxLimitPer.Text)
                                'If cPer > 0 Then
                                If Not textBoxRenewalNo Is Nothing And Not String.IsNullOrWhiteSpace(textBoxRenewalNo.Text) Then
                                    If Convert.ToDecimal(textBoxRenewalNo.Text) > 0 Then
                                        countLimitPer = countLimitPer + 1
                                    End If
                                End If
                                'End If
                            End If
                        End If

                        If Not lblLimitPer Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(lblLimitPer.Text) Then
                                'Dim cPer As Decimal = Convert.ToDecimal(lblLimitPer.Text)
                                If Not lblRenewalNo Is Nothing And Not String.IsNullOrWhiteSpace(lblRenewalNo.Text) Then
                                    If Convert.ToDecimal(lblRenewalNo.Text) > 0 Then
                                        countLimitPer = countLimitPer + 1
                                    End If
                                End If
                            End If
                        End If

                        If Not textBoxLimit Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(textBoxLimit.Text) Then
                                'Dim cAmt As Decimal = Convert.ToDecimal(textBoxLimit.Text)
                                'If cAmt > 0 Then
                                If Not textBoxRenewalNo Is Nothing And Not String.IsNullOrWhiteSpace(textBoxRenewalNo.Text) Then
                                    If Convert.ToDecimal(textBoxRenewalNo.Text) > 0 Then
                                        countLimit = countLimit + 1
                                    End If
                                End If
                                'End If
                            End If
                        End If

                        If Not lblLimit Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(lblLimit.Text) Then
                                'Dim cAmt As Decimal = Convert.ToDecimal(lblLimit.Text)
                                'If cAmt > 0 Then
                                If Not lblRenewalNo Is Nothing And Not String.IsNullOrWhiteSpace(lblRenewalNo.Text) Then
                                    If Convert.ToDecimal(lblRenewalNo.Text) > 0 Then
                                        countLimit = countLimit + 1
                                    End If
                                End If
                                'End If
                            End If
                        End If

                    End If
                Next
            Next

            If ((countLimitPer = 1 Or countLimitPer > 1) And (countLimit = 1 Or countLimit > 1)) Then
                Me.State.IsRateLimitAndPercentBothPresent = True
            Else
                Me.State.IsRateLimitAndPercentBothPresent = False
            End If
        End Sub

        Private Sub ValidateRateLimitAndPercent()
            If State.IsProductConfiguredForRenewalNo Then
                CheckRateLimitAndPercentBothPresent()

                If Me.State.IsRateLimitAndPercentBothPresent Then
                    Me.State.IsRateLimitAndPercentBothPresent = False
                    Throw New GUIException(Message.MSG_EITHER_LIMIT_OR_PERCENT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_ONLY_EITHER_LIMIT_OR_PERCENT_ALLOWED)
                End If
            End If
        End Sub

        Private Sub DisableLimitWhenRenewalIsZero()
            If moGridView.EditIndex = -1 Then Exit Sub
            If State.IsProductConfiguredForRenewalNo Then

                Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                Dim textBoxLimitPer As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moLiability_LimitPercentText"), TextBox)
                Dim textBoxLimit As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimit).FindControl("moLiability_LimitText"), TextBox)
                Dim textBoxRenewalNo As TextBox = DirectCast(gRow.Cells(ColIndexCovLiabilityLimitPercent).FindControl("moRenewal_NumberText"), TextBox)

                If Not textBoxRenewalNo Is Nothing And Not textBoxLimit Is Nothing And Not textBoxLimitPer Is Nothing Then
                    If Not String.IsNullOrWhiteSpace(textBoxRenewalNo.Text) Then
                        If Convert.ToDecimal(textBoxRenewalNo.Text) = 0 Then
                            textBoxLimit.Text = String.Empty
                            textBoxLimitPer.Text = String.Empty

                            textBoxLimit.Enabled = False
                            textBoxLimitPer.Enabled = False
                        Else
                            textBoxLimit.Enabled = True
                            textBoxLimitPer.Enabled = True
                        End If
                    End If
                End If
            End If
        End Sub
#End Region

#Region "Get Current User and Language"
        Private Function CurrentUser() As User
            Return ElitaPlusIdentity.Current.ActiveUser
        End Function

        Private Function GetLanguageId() As Guid
            Return ElitaPlusIdentity.Current.ActiveUser.LanguageId
        End Function
#End Region

    End Class
End Namespace
