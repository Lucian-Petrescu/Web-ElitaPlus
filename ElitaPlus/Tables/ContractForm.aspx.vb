Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

Partial Class ContractForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents radioListStatus As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents lblAssignedTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblWebsite As System.Web.UI.WebControls.Label
    Protected WithEvents txtWebsite As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblIsActive As System.Web.UI.WebControls.Label
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhone As System.Web.UI.WebControls.Label
    Protected WithEvents lnkViewAuditInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Textbox5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox15 As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataGrid1 As DataGrid
    Protected WithEvents ID_VALIDATION As System.Web.UI.WebControls.Label

    'Protected WithEvents btnTNC As System.Web.UI.WebControls.Button

    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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
    Public Const URL As String = "ContractForm.aspx"
    Private Const DIRECT As String = "3"
    Private Const CEDED As String = "1"
    Private Const ASSUMED As String = "2"
    Private Const COMPANY_TYPE_SERVICES As String = "2"
    Private Const IDVALIDATION_NONE As String = "3"
    Private Const NOTHING_SELECTED As Int16 = -1
    Private Const COMPANY_TYPE_SERVICE As String = "2"
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const YES As String = "Y"
    Private Const NO As String = "N"
    Private Const CONTRACT_TYPE_EXTENSION As String = "3"
    Private Const COLLECTION_CYCLE_TYPE_FIX As String = "FIX"
    Private Const ZERO_STRING As String = "0"
    Private Const ONE_STRING As String = "1"
    Private Const VARIABLE_CYCLE_TYPE_CODE As String = "VAR"



#End Region

#Region "Properties"

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Contract
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Contract, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As Contract
        Public ScreenSnapShotBO As Contract
        Public DealerBO As Dealer
        Public CountryBO As Country
        Public DealerID As Guid = Guid.Empty
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public Company_Type_ID As Guid = Guid.Empty
        Public IsACopy As Boolean
        'REQ:5773 start
        Public use_comm_entity_type_id As Guid = Guid.Empty
        'REQ:5773 end
        Public PageIndex As Integer = 0
        Public Company_ID As Guid = Guid.Empty

        'Req-5372
        Public RepPolicyAction As Integer = RepPolicy_None
        Public RepPolicyList As Collections.Generic.List(Of ReppolicyClaimCount)
        Public RepPolicyWorkingItem As ReppolicyClaimCount
        Public RepPolicyWorkingOrig As ReppolicyClaimCount

        Public DepreciationScheduleAction As Integer = DepreciationScheduleNone
        Public DepreciationScheduleList As Collections.Generic.List(Of DepreciationScdRelation)
        Public DepreciationScheduleWorkingItem As DepreciationScdRelation
        Public DepreciationScheduleOrig As DepreciationScdRelation

        'US 489857
        Public IsDiffSelectedTwice As Boolean
        Public IsDiffNotSelectedOnce As Boolean
        Public IsBucketIncomingSelected As Boolean
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Public Property DealerID() As Guid
        Get
            Return DealerID
        End Get
        Set(Value As Guid)
            DealerID = Value
        End Set
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Contract(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        ErrorControllerDS.Clear_Hide()

        Dim oCompany As Company = Nothing
        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                'Me.btnSave_WRITE.Attributes.Add("onclick", "return ClearErrorCenter();")
                'Date Calendars
                AddCalendar_New(ImageButtonEndDate, TextboxEndDate_WRITE)
                'Me.AddCalendar(Me.ImageButtonLastReconDate, Me.TextboxLastReconDate)
                AddCalendar_New(ImageButtonStartDate, TextboxStartDate_WRITE)
                '  Me.MenuEnabled = False
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                If State.MyBO Is Nothing Then
                    State.MyBO = New Contract
                Else

                    If State.DealerBO Is Nothing AndAlso Not State.MyBO.DealerId.Equals(Guid.Empty) Then
                        State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
                    End If

                    oCompany = New Company(State.DealerBO.CompanyId)
                    State.CountryBO = New Country(oCompany.BusinessCountryId)
                    State.MyBO.CountryId = State.CountryBO.Id

                    State.Company_Type_ID = oCompany.CompanyTypeId
                    State.Company_ID = oCompany.Id
                End If

                Dim oCompanyGroup As CompanyGroup
                If (oCompany Is Nothing) Then
                    oCompany = New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                End If
                If (Not oCompany.CompanyGroupId.Equals(Guid.Empty)) Then
                    If (oCompanyGroup Is Nothing) Then
                        oCompanyGroup = New CompanyGroup(oCompany.CompanyGroupId)
                    End If
                    State.use_comm_entity_type_id = oCompanyGroup.UseCommEntityTypeId
                End If
                PopulateDropdowns()
                PopulateProducer()
                TranslateGridHeader(moGridView)
                SetGridItemStyleColor(GridViewDepreciationSchedule)
                TranslateGridHeader(GridViewDepreciationSchedule)
                PopulateFormFromBOs()
                EnableDisableFields()
            End If

            If IsPostBack Then
                PopulateSourcePercentageBucketValues()
            End If

            CheckCoinsuranceDropDown()
            BindBoPropertiesToLabels()

            BindBoPropertiesToDepreciationScheduleGridHeaders()
            ClearGridViewHeadersAndLabelsErrorSign()

            CheckIfComingFromSaveConfirm()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            SetDecimalSeparatorSymbol()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        'If Not State.MyBO Is Nothing Then
        '    If State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) OrElse _
        '        State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then

        '    Else
        '        Me.tsHoriz.Visible = False
        '    End If
        'End If
    End Sub
#End Region

#Region "Controlling Logic"
    Public Function GetYesID() As Guid
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        Return yesId
    End Function

    Public Function GetExtCovebyPaymentId() As Guid
        Dim byPaymentId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COVERAGE_EXTENSIONS, Codes.COV_EXT_BYPYMT)
        Return byPaymentId
    End Function

    Public Function GetListItemIDFromCode(strListCode As String, strItemCode As String) As Guid
        Dim resultID As Guid
        resultID = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(strListCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId), strItemCode)
        Return resultID
    End Function

    Private Sub SetID_Validation_DDandAcsel_Prod_Code()

        If State.DealerBO Is Nothing AndAlso Not State.MyBO.DealerId.Equals(Guid.Empty) Then
            State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
        End If

        Dim oCompany As Company = New Company(State.DealerBO.CompanyId)

        If oCompany.CompanyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, COMPANY_TYPE_SERVICES)) Then
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, False)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, False)
            SetSelectedItem(cboID_VALIDATION, LookupListNew.GetIdFromCode(LookupListNew.LK_VALIDATION_TYPES, IDVALIDATION_NONE))
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, False)
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, False)
            SetSelectedItem(cboAcselProdCode, System.Guid.Empty)
        Else
            cboID_VALIDATION.SelectedIndex = NOTHING_SELECTED
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, True)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, True)
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, True)
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, True)
            cboAcselProdCode.SelectedIndex = NOTHING_SELECTED
        End If

    End Sub
    Protected Sub EnableDisableFields(Optional ByVal blnComingFromMonthlyBillingOrDealerMarkup As Boolean = False)
        Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
        Dim lkCovExtNoneId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COVERAGE_EXTENSIONS, Codes.COV_EXT_NONE)

        'Further filter by CODE - Should return only one row
        lkYesNo.RowFilter += " and code='Y'"
        Dim yesId As Guid = New Guid(CType(lkYesNo.Item(0).Item("ID"), Byte()))
        If Not blnComingFromMonthlyBillingOrDealerMarkup Then
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, (Not State.MyBO.IsNew) _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, (Not State.MyBO.IsNew) _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)

            'Acsel/x Product Code for Insurance company
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, (Not State.MyBO.IsNew) _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, (Not State.MyBO.IsNew) _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
        End If
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        ControlMgr.SetVisibleControl(Me, btnTNC, True)

        'Req-1016 - Start
        Dim emptyGuid As Guid = Guid.Empty
        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
        'Req-1016 - end
        'Monthly billing
        Dim currRecurringPremiumId As Guid = GetSelectedItem(cboRecurringPremium)
        Dim recurringPremiumIsYes As Boolean = False
        Dim currInstallmentPaymentId As Guid = GetSelectedItem(cboInstallmentPayment)
        Dim InstallmentPaymentIsYes As Boolean = False
        If currInstallmentPaymentId.Equals(yesId) Then
            InstallmentPaymentIsYes = True
        End If
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        'Req-1016 - Start
        'If currMonthlyBillingId.Equals(yesId) Then 
        If ((Not currRecurringPremiumId.Equals(emptyGuid)) AndAlso (Not currRecurringPremiumId.Equals(singlePremiumId))) Then
            recurringPremiumIsYes = True
        End If
        ControlMgr.SetVisibleControl(Me, LabelDaysToSuspend, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, LabelDaysToCancel, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, TextboxDaysToSuspend, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, TextboxDaysToCancel, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, LabelIncludeFirstPmt, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, cboIncludeFirstPmt, recurringPremiumIsYes)
        If Not recurringPremiumIsYes Then
            cboIncludeFirstPmt.SelectedIndex = NOTHING_SELECTED
            '  Me.TextboxDaysToSuspend.Text = Nothing
        End If
        ControlMgr.SetVisibleControl(Me, lblExtendCoverage, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, cboExtendCoverage, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, lblExtendCoverageBy, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, lblExtendCoverageByExtraDays, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, lblExtendCoverageByExtraMonths, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, txtExtendCoverageByExtraDays, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, txtExtendCoverageByExtraMonths, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, lblAllowPymtSkipMonths, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, ddlAllowPymtSkipMonths, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, lblBillingCycleType, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, ddlBillingCycleType, recurringPremiumIsYes)

        'Req-1016 Start        
        ControlMgr.SetVisibleControl(Me, lblPeridiocBillingWarntyPeriod, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, txtPeridiocBillingWarntyPeriod, recurringPremiumIsYes)
        'Req-1016 End

        If Not recurringPremiumIsYes AndAlso InstallmentPaymentIsYes Then
            ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, True)
            ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, True)
        Else
            ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, False)
            ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, False)
            cboPayOutstandingAmount.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End If

        'Dealer Markup
        Dim currDealerMarkup As Guid = GetSelectedItem(cboDealerMarkup_WRITE)
        Dim dealerMarkupIsYes As Boolean = False
        Dim currRestrictMarkup As Guid = GetSelectedItem(cboRestrictMarkup_WRITE)
        Dim currCoverageMarkupDistribution As Guid = GetSelectedItem(ddlAllowCoverageMarkupDistribution)

        If (recurringPremiumIsYes) Then
            If currDealerMarkup.Equals(yesId) Then
                dealerMarkupIsYes = True
                'Dim RestrictMarkupIsYes As Boolean = False
                If currRestrictMarkup.Equals(noId) Then
                    SetSelectedItem(cboRestrictMarkup_WRITE, noId)
                End If
                If currCoverageMarkupDistribution.Equals(noId) Then
                    SetSelectedItem(ddlAllowCoverageMarkupDistribution, noId)
                End If
            Else
                SetSelectedItem(cboRestrictMarkup_WRITE, noId)
                SetSelectedItem(ddlAllowCoverageMarkupDistribution, noId)
            End If
        Else
            If currDealerMarkup.Equals(yesId) Then
                dealerMarkupIsYes = True
            Else
                SetSelectedItem(cboRestrictMarkup_WRITE, noId)
                SetSelectedItem(ddlAllowCoverageMarkupDistribution, noId)
            End If
        End If
        ControlMgr.SetEnableControl(Me, cboRestrictMarkup_WRITE, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, LabelRestrictMarkup_WRITE, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, ddlAllowCoverageMarkupDistribution, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, lblAllowCoverageMarkupDistribution, dealerMarkupIsYes)

        Dim FDAF_noneId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_FUTURE_DATE_ALLOW_FOR, Codes.FDAF_NONE)
        If State.MyBO.IsNew AndAlso Not State.IsACopy Then
            SetSelectedItem(cboIgnoreCovAmt, noId)
            SetSelectedItem(cboBackEndClaimAllowed, noId)
            SetSelectedItem(cboExtendCoverage, lkCovExtNoneId)
            'REQ-490 - changed list from YES/NO to the EDIT MFG Term list.  Removing the auto set.  The field is required, so the user will need to choose a value.
            'Me.SetSelectedItem(Me.cboEDIT_MFG_TERM, noId)

            SetSelectedItem(cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, noId)
            SetSelectedItem(cboInstallmentPayment, noId)
            SetSelectedItem(cboDeductible_By_Manufacturer, noId)
            SetSelectedItem(cboIsCommPCodeId, noId)
            SetSelectedItem(cboFutureDateAllowFor, FDAF_noneId)

            'Me.PopulateControlFromBOProperty(Me.TextboxDaysOfFirstPymt, ZERO_STRING)
            PopulateControlFromBOProperty(TextboxDaysToCancelCert, ZERO_STRING)
            PopulateControlFromBOProperty(TextboxDaysToSendLetter, ZERO_STRING)
            PopulateControlFromBOProperty(txtExtendCoverageByExtraMonths, ZERO_STRING)
            PopulateControlFromBOProperty(txtExtendCoverageByExtraDays, ZERO_STRING)
        End If

        ShowInstPymtFields()
        EnableFirstPaymentMonthsField()

        TheDealerControl.ChangeEnabledControlProperty(State.MyBO.IsNew)
        'ControlMgr.SetEnableControl(Me, TheDealerControl.ClearMultipleDrop, Me.State.MyBO.IsNew)
        ' ControlMgr.SetEnableControl(Me, cboDealer_WRITE, Me.State.MyBO.IsNew)

        'Start and End Dates
        ControlMgr.SetEnableControl(Me, TextboxStartDate_WRITE, True)
        ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
        ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
        ControlMgr.SetEnableControl(Me, TextboxEndDate_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        If Not State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, TextboxStartDate_WRITE, False)
            ControlMgr.SetEnableControl(Me, ImageButtonStartDate, False)
            If Not State.MyBO.IsLastContract Then
                ControlMgr.SetEnableControl(Me, TextboxEndDate_WRITE, False)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, False)
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            End If
            'Req - 1016 Start
            'Disable Periodic Renew drop-down if the contract record is not new
            ControlMgr.SetEnableControl(Me, cboRecurringPremium, False)
            'Req - 1016 End
        Else
            'Req - 1016 Start
            'Enable Periodic Renew drop-down if the contract record is new
            ControlMgr.SetEnableControl(Me, cboRecurringPremium, True)
            'Req - 1016 End
        End If

        'Req - 1016 Start
        'Enable Periodic Renew drop-down for copied contract
        If State.IsACopy Then
            ControlMgr.SetEnableControl(Me, cboRecurringPremium, True)
        End If
        'Req - 1016 End

        'New With Copy Button
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnTNC, False)
        End If

        If Not State.MyBO.CanItBeDeleted Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        End If

        'REQ-756 - hide the recollection if not VSC
        If State.DealerBO Is Nothing AndAlso Not State.MyBO.DealerId.Equals(Guid.Empty) Then
            State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
        End If

        If State.DealerBO IsNot Nothing AndAlso State.DealerBO.DealerTypeDesc = State.DealerBO.DEALER_TYPE_DESC Then
            ControlMgr.SetVisibleControl(Me, txtCollectionReAttempts, True)
            ControlMgr.SetVisibleControl(Me, LabelCollectionReAttempts, True)
            ControlMgr.SetVisibleControl(Me, txtPastDueMonthsAllowed, True)
            ControlMgr.SetVisibleControl(Me, lblPastDueMonthsAllowed, True)
            ControlMgr.SetEnableControl(Me, ddlAllowCoverageMarkupDistribution, False)
            ControlMgr.SetEnableControl(Me, lblAllowCoverageMarkupDistribution, False)
        Else
            txtCollectionReAttempts.Text = String.Empty
            txtPastDueMonthsAllowed.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, txtCollectionReAttempts, False)
            ControlMgr.SetVisibleControl(Me, LabelCollectionReAttempts, False)
            ControlMgr.SetVisibleControl(Me, txtPastDueMonthsAllowed, False)
            ControlMgr.SetVisibleControl(Me, lblPastDueMonthsAllowed, False)
        End If
        'REQ-5773 Start
        If State.use_comm_entity_type_id.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) And Codes.DEALER_TYPES__VSC Then
            ControlMgr.SetVisibleControl(Me, lblPaymentProcessingTypeId, True)
            ControlMgr.SetVisibleControl(Me, ddlPaymentProcessingTypeId, True)
            ControlMgr.SetVisibleControl(Me, lblRdoName, True)
            ControlMgr.SetVisibleControl(Me, txtRdoName, True)
            ControlMgr.SetVisibleControl(Me, lblRdoTaxId, True)
            ControlMgr.SetVisibleControl(Me, txtRdoTaxId, True)
            ControlMgr.SetVisibleControl(Me, lblRdoPercent, True)
            ControlMgr.SetVisibleControl(Me, txtRdoPercent, True)
        Else
            ControlMgr.SetVisibleControl(Me, lblPaymentProcessingTypeId, False)
            ControlMgr.SetVisibleControl(Me, ddlPaymentProcessingTypeId, False)
            ControlMgr.SetVisibleControl(Me, lblRdoName, False)
            ControlMgr.SetVisibleControl(Me, txtRdoName, False)
            ControlMgr.SetVisibleControl(Me, lblRdoTaxId, False)
            ControlMgr.SetVisibleControl(Me, txtRdoTaxId, False)
            ControlMgr.SetVisibleControl(Me, lblRdoPercent, False)
            ControlMgr.SetVisibleControl(Me, txtRdoPercent, False)
        End If
        If State.MyBO.PaymentProcessingTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.THIRD_PARTY_PAYMENT)) Then
            ControlMgr.SetVisibleControl(Me, lblThirdPartyName, True)
            ControlMgr.SetVisibleControl(Me, txtThirdPartyName, True)
            ControlMgr.SetVisibleControl(Me, lblThirdPartyTaxId, True)
            ControlMgr.SetVisibleControl(Me, txtThirdPartyTaxId, True)
        Else
            txtThirdPartyName.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, lblThirdPartyName, False)
            ControlMgr.SetVisibleControl(Me, txtThirdPartyName, False)
            txtThirdPartyTaxId.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, lblThirdPartyTaxId, False)
            ControlMgr.SetVisibleControl(Me, txtThirdPartyTaxId, False)
        End If

        ' if new or a copy or collective and manualy entered, then enable policy.
        If State.MyBO.IsNew Then

            If Not blnComingFromMonthlyBillingOrDealerMarkup Then
                cboLineOfBusiness.Items.Clear()
                SetSelectedItem(cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))

                ' if Dealer is not selected in case of New then disable ind policy dropdowns.
                ControlMgr.SetEnableControl(Me, cboPolicyType, Not State.MyBO.DealerId.Equals(Guid.Empty))
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, Not State.MyBO.DealerId.Equals(Guid.Empty))

                ControlMgr.SetEnableControl(Me, cboLineOfBusiness, True)

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
            End If

        End If

        ' if Edit then Disable Policy type related dropdowns/controls.
        If Not State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, cboPolicyType, False)
            ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, False)
            ControlMgr.SetEnableControl(Me, cboLineOfBusiness, False)
            ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, False)

            If State.MyBO.PolicyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) AndAlso
                State.MyBO.PolicyGenerationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then
                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
            End If

        End If

        'REQ-5773 End
        ControlMgr.SetVisibleControl(Me, lblExtendCoverage, recurringPremiumIsYes)

        ControlMgr.SetVisibleControl(Me, BtnNewDepreciationSchedule, Not State.MyBO.IsNew)

        EnableDisableCycleDay()

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "AdminExpense", LabelAdminExpense)
        BindBOPropertyToLabel(State.MyBO, "AutoMfgCoverageId", LabelAutoMFG)
        BindBOPropertyToLabel(State.MyBO, "CancellationDays", LabelDaysToCancel)
        BindBOPropertyToLabel(State.MyBO, "CommissionsPercent", LabelCommPercent)
        BindBOPropertyToLabel(State.MyBO, "ContractTypeId", LabelContractType)
        BindBOPropertyToLabel(State.MyBO, "CurrencyId", LabelCurrency)
        BindBOPropertyToLabel(State.MyBO, "DealerId", TheDealerControl.CaptionLabel)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.LabelDealer_WRITE)
        BindBOPropertyToLabel(State.MyBO, "DealerMarkupId", LabelDealerMarkup)
        BindBOPropertyToLabel(State.MyBO, "DeductibleBasedOnId", LabelDeductibleBasedOn)
        BindBOPropertyToLabel(State.MyBO, "Deductible", LabelDeductible)
        BindBOPropertyToLabel(State.MyBO, "EditModelId", LabelEditModel)
        BindBOPropertyToLabel(State.MyBO, "Effective", LabelStartDate)
        BindBOPropertyToLabel(State.MyBO, "FixedEscDurationFlag", LabelFixedEscDurationFlag)
        BindBOPropertyToLabel(State.MyBO, "Expiration", LabelEndDate)
        BindBOPropertyToLabel(State.MyBO, "FundingSourceId", LabelFundingSource)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "LastRecon", Me.LabelLastReconDate)
        BindBOPropertyToLabel(State.MyBO, "Layout", LabelLayout)
        BindBOPropertyToLabel(State.MyBO, "FullRefundDays", LabelFullRefundDays)
        BindBOPropertyToLabel(State.MyBO, "LossCostPercent", LabelLossCostPercent)
        BindBOPropertyToLabel(State.MyBO, "MarketingPercent", LabelMarketingExpense)
        BindBOPropertyToLabel(State.MyBO, "MinReplacementCost", LabelMinReplCost)
        'Req-1016 - Start
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "MonthlyBillingId", Me.LabelMonthlyBilling)
        BindBOPropertyToLabel(State.MyBO, "RecurringPremiumId", LabelRecurringPremium)
        BindBOPropertyToLabel(State.MyBO, "RecurringWarrantyPeriod", lblPeridiocBillingWarntyPeriod)
        'Req-1016 - End
        BindBOPropertyToLabel(State.MyBO, "NetCommissionsId", LabelNetCommission)
        BindBOPropertyToLabel(State.MyBO, "NetMarketingId", LabelNetMarketing)
        BindBOPropertyToLabel(State.MyBO, "NetTaxesId", LabelNetTaxes)
        BindBOPropertyToLabel(State.MyBO, "Policy", LabelPolicy)
        BindBOPropertyToLabel(State.MyBO, "ProfitPercent", LabelProfitExpense)
        BindBOPropertyToLabel(State.MyBO, "RestrictMarkupId", LabelRestrictMarkup_WRITE)
        BindBOPropertyToLabel(State.MyBO, "AllowCoverageMarkupDistribution", lblAllowCoverageMarkupDistribution)
        BindBOPropertyToLabel(State.MyBO, "SuspenseDays", LabelDaysToSuspend)
        BindBOPropertyToLabel(State.MyBO, "TypeOfEquipmentId", LabelTypeOfEquipment)
        BindBOPropertyToLabel(State.MyBO, "TypeOfInsuranceId", LabelTypeOfIns)
        BindBOPropertyToLabel(State.MyBO, "TypeOfMarketingId", LabelTypeOfMarketing)
        BindBOPropertyToLabel(State.MyBO, "WaitingPeriod", LabelWaitingPeriod)
        BindBOPropertyToLabel(State.MyBO, "WarrantyMaxDelay", LabelWarrantyMaxDelay)

        BindBOPropertyToLabel(State.MyBO, "ReplacementPolicyId", LabelREPLACEMENT_POLICY)
        'REQ-1333
        BindBOPropertyToLabel(State.MyBO, "ReplacementPolicyClaimCount", lblReplacementPolicyClaimCount)
        BindBOPropertyToLabel(State.MyBO, "CancellationReasonId", LabelCancellationReason)
        BindBOPropertyToLabel(State.MyBO, "CoinsuranceId", LabelCOINSURANCE)
        BindBOPropertyToLabel(State.MyBO, "ParticipationPercent", LabelPARTICIPATION_PERCENT)
        BindBOPropertyToLabel(State.MyBO, "RatingPlan", LabelRatingPlan)
        BindBOPropertyToLabel(State.MyBO, "ID_Validation_Id", LabelID_VALIDATION)
        BindBOPropertyToLabel(State.MyBO, "Acsel_Prod_Code_Id", LabelAcselProdCode)
        BindBOPropertyToLabel(State.MyBO, "claim_control_id", LabelClaimControl)
        BindBOPropertyToLabel(State.MyBO, "IgnoreIncomingPremiumID", LabelIgnorePremium)
        BindBOPropertyToLabel(State.MyBO, "RemainingMFGDays", LabelRemainingMFGDays)
        BindBOPropertyToLabel(State.MyBO, "IgnoreCoverageAmtId", LabelIgnoreCovAmt)
        BindBOPropertyToLabel(State.MyBO, "BackEndClaimsAllowedId", LabelBackEndClaimAllowed)
        BindBOPropertyToLabel(State.MyBO, "InstallmentPaymentId", LabelInstallmentPayment)
        BindBOPropertyToLabel(State.MyBO, "CollectionReAttempts", LabelCollectionReAttempts)
        BindBOPropertyToLabel(State.MyBO, "PastDueMonthsAllowed", lblPastDueMonthsAllowed)
        BindBOPropertyToLabel(State.MyBO, "DaysOfFirstPymt", LabelDaysOfFirstPymt)
        BindBOPropertyToLabel(State.MyBO, "DaysToSendLetter", LabelDaysToSendLetter)
        BindBOPropertyToLabel(State.MyBO, "DaysToCancelCert", LabelDaysToCancelCert)
        BindBOPropertyToLabel(State.MyBO, "DeductibleByManufacturerId", LabelDeductibleByMfg)
        BindBOPropertyToLabel(State.MyBO, "ProRataMethodId", LabelProRataMethodId)

        BindBOPropertyToLabel(State.MyBO, "CurrencyConversionId", Label_CURRENCY_CONVERSION)
        BindBOPropertyToLabel(State.MyBO, "CurrencyOfCoveragesId", LabelCURRENCY_OF_COVERAGES)
        BindBOPropertyToLabel(State.MyBO, "AutoSetLiabilityId", LabelAutoSetLiability)
        BindBOPropertyToLabel(State.MyBO, "CoverageDeductibleId", LabelCovDeductible)
        BindBOPropertyToLabel(State.MyBO, "DeductiblePercent", LabelDeductiblePercent)
        BindBOPropertyToLabel(State.MyBO, "IgnoreWaitingPeriodWsdPsd", LabelApplyWaitingPeriod)
        BindBOPropertyToLabel(State.MyBO, "RepairDiscountPct", LabelRepairDiscountPct)
        BindBOPropertyToLabel(State.MyBO, "ReplacementDiscountPct", LabelReplacementDiscountPrc)

        BindBOPropertyToLabel(State.MyBO, "NumOfClaims", LabelNumOfClaims)
        BindBOPropertyToLabel(State.MyBO, "NumOfRepairClaims", LabelNumOfRepairClaims)
        BindBOPropertyToLabel(State.MyBO, "NumOfReplacementClaims", LabelNumOfReplClaims)

        BindBOPropertyToLabel(State.MyBO, "PenaltyPct", LabelPenaltyPct)

        BindBOPropertyToLabel(State.MyBO, "EditMFGTermId", LabelEditMfgTerm)
        BindBOPropertyToLabel(State.MyBO, "AcctBusinessUnitId", LabelAcctBusinessUnit)
        BindBOPropertyToLabel(State.MyBO, "ClipPercent", lblCLIPct)
        BindBOPropertyToLabel(State.MyBO, "IsCommPCodeId", LabelIsCommPCodeId)

        BindBOPropertyToLabel(State.MyBO, "BaseInstallments", LabelBaseInstallments)
        BindBOPropertyToLabel(State.MyBO, "BillingCycleFrequency", LabelBillingCycleFrequency)
        BindBOPropertyToLabel(State.MyBO, "InstallmentsBaseReducer", LabelInstallmentsBaseReducer)
        BindBOPropertyToLabel(State.MyBO, "MaxInstallments", LabelMaxNumofInstallments)
        BindBOPropertyToLabel(State.MyBO, "IncludeFirstPmt", LabelIncludeFirstPmt)
        BindBOPropertyToLabel(State.MyBO, "CollectionCycleTypeId", LabelCollectionCycleType)
        BindBOPropertyToLabel(State.MyBO, "CycleDay", LabelCycleDay)
        BindBOPropertyToLabel(State.MyBO, "OffsetBeforeDueDate", LabelOffsetBeforeDueDate)
        BindBOPropertyToLabel(State.MyBO, "InsPremiumFactor", lblInsPremFactor)

        BindBOPropertyToLabel(State.MyBO, "ExtendCoverageId", lblExtendCoverage)
        BindBOPropertyToLabel(State.MyBO, "ExtraDaysToExtendCoverage", lblExtendCoverageBy)
        BindBOPropertyToLabel(State.MyBO, "ExtraMonsToExtendCoverage", lblExtendCoverageBy)
        BindBOPropertyToLabel(State.MyBO, "ClaimLimitBasedOnId", LabelReplacementBasedOn)
        BindBOPropertyToLabel(State.MyBO, "AllowDifferentCoverage", labelAllowDifferentCoverage)
        BindBOPropertyToLabel(State.MyBO, "CustmerAddressRequiredId", labelCustAddressRequired)
        BindBOPropertyToLabel(State.MyBO, "FutureDateAllowForID", lblFutureDateAllowFor)
        BindBOPropertyToLabel(State.MyBO, "FirstPymtMonths", labelFirstPymtMonths)
        BindBOPropertyToLabel(State.MyBO, "PayOutstandingPremiumId", LabelPayOutstandingAmount)
        BindBOPropertyToLabel(State.MyBO, "AuthorizedAmountMaxUpdates", lblAuthorizedAmountMaxUpdates)
        BindBOPropertyToLabel(State.MyBO, "AllowPymtSkipMonths", lblAllowPymtSkipMonths)
        BindBOPropertyToLabel(State.MyBO, "BillingCycleTypeId", lblBillingCycleType)
        BindBOPropertyToLabel(State.MyBO, "DailyRateBasedOnId", lblDailyRateBasedOn)
        BindBOPropertyToLabel(State.MyBO, "AllowBillingAfterCancellation", lblAllowBillingAfterCancellation)
        BindBOPropertyToLabel(State.MyBO, "AllowCollectionAfterCancellation", lblAllowCollectionAfterCancellation)

        'Added for Req-635
        BindBOPropertyToLabel(State.MyBO, "AllowNoExtended", lblAllowNoExtended)

        BindBOPropertyToLabel(State.MyBO, "DaysToReportClaim", lblDaysToReportClaim)
        'Req-703 Start
        BindBOPropertyToLabel(State.MyBO, "MarketingPromotionId", lblMarketingPromo)
        'Req-703 End

        'REQ-794
        'This line of code is commented for the def-1861
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "IgnoreCoverageRateId", Me.LabelIgnoreCovRate)

        BindBOPropertyToLabel(State.MyBO, "AllowMultipleRejectionsId", lblAllowMultipleRejections)

        'REQ-1050 start
        BindBOPropertyToLabel(State.MyBO, "DaysToReactivate", lblDaysToReactivate)
        'REQ-1050 END
        'REQ-5773 Start
        BindBOPropertyToLabel(State.MyBO, "PaymentProcessingTypeId", lblPaymentProcessingTypeId)
        BindBOPropertyToLabel(State.MyBO, "ThirdPartyName", lblThirdPartyName)
        BindBOPropertyToLabel(State.MyBO, "ThirdPartyTaxId", lblThirdPartyTaxId)
        BindBOPropertyToLabel(State.MyBO, "RdoName", lblRdoName)
        BindBOPropertyToLabel(State.MyBO, "RdoTaxId", lblRdoTaxId)
        BindBOPropertyToLabel(State.MyBO, "RdoPercent", lblRdoPercent)
        'REQ-5773 End

        BindBOPropertyToLabel(State.MyBO, "OverrideEditMfgTerm", LabelOverrideEditMfgTerm)

        ' Ind Policy
        BindBOPropertyToLabel(State.MyBO, "PolicyTypeId", LabelPolicyType)
        BindBOPropertyToLabel(State.MyBO, "PolicyGenerationId", LabelCollectivePolicyGeneration)
        BindBOPropertyToLabel(State.MyBO, "ProducerId", lblProducer)

        ClearGridHeadersAndLabelsErrSign()
    End Sub
    Protected Sub populateDealer()
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
        TheDealerControl.SelectedGuid = State.MyBO.DealerId
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Contract")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Contract")
            End If
        End If
    End Sub

    Protected Sub PopulateDropdowns()

        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Dim populateOptions As PopulateOptions = New PopulateOptions() With
            {
                .AddBlankItem = True
            }
        Dim populateOptions1 As PopulateOptions = New PopulateOptions() With
            {
                .AddBlankItem = False
            }

        'Dim covExtVW As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_COV_EXT_CODE, langId, True)
        'Dim currencyVW As DataView = LookupListNew.DataView(LookupListNew.LK_CURRENCIES)

        'ElitaPlusPage.BindListControlToDataView(Me.cboAutoMFG, yesNoLkL)
        cboAutoMFG.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboContractType, LookupListNew.DropdownLookupList("CONTP", langId, True))
        cboContractType.Populate(CommonConfigManager.Current.ListManager.GetList("CONTP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        cboPolicyType.Populate(CommonConfigManager.Current.ListManager.GetList("POLTYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

        cboCollectivePolicyGeneration.Populate(CommonConfigManager.Current.ListManager.GetList("POLGEN", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)


        'ElitaPlusPage.BindListControlToDataView(Me.cboCurrency, currencyVW)
        cboCurrency.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboID_VALIDATION, LookupListNew.DropdownLookupList("IDVAL", langId, True))
        cboID_VALIDATION.Populate(CommonConfigManager.Current.ListManager.GetList("IDVAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAcselProdCode, LookupListNew.DropdownLookupList("ACSPC", langId, True))
        cboAcselProdCode.Populate(CommonConfigManager.Current.ListManager.GetList("ACSPC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        populateDealer()
        'ElitaPlusPage.BindListControlToDataView(Me.cboDealerMarkup_WRITE, yesNoLkL)
        cboDealerMarkup_WRITE.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboEditModel, yesNoLkL)
        cboEditModel.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFixedEscDurationFlag, yesNoLkL)
        cboFixedEscDurationFlag.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFundingSource, LookupListNew.DropdownLookupList("FUNDS", langId, True))
        cboFundingSource.Populate(CommonConfigManager.Current.ListManager.GetList("FUNDS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'Req -1016 - Start
        'Dim periodRenewSortedByCode As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_PERIOD_RENEW, langId, True)
        'periodRenewSortedByCode.Sort = "code"
        'ElitaPlusPage.BindListControlToDataView(Me.cboRecurringPremium, periodRenewSortedByCode, , , , False)
        cboRecurringPremium.Populate(CommonConfigManager.Current.ListManager.GetList("PERIODRENEW", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

        'Req -1016 - end
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetCommission, yesNoLkL)
        cboNetCommission.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetMarketing, yesNoLkL)
        cboNetMarketing.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetTaxes, yesNoLkL)
        cboNetTaxes.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCovDeductible, yesNoLkL)
        cboCovDeductible.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCustAddressRequired, yesNoLkL)
        cboCustAddressRequired.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboPayOutstandingAmount, yesNoLkL)
        cboPayOutstandingAmount.Populate(oYesNoList, populateOptions)

        'Dim futureDateAllowForLkL As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_FUTURE_DATE_ALLOW_FOR_CODE, langId, True)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFutureDateAllowFor, futureDateAllowForLkL)
        cboFutureDateAllowFor.Populate(CommonConfigManager.Current.ListManager.GetList("FDAF", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboRestrictMarkup_WRITE, yesNoLkL, , , False)
        cboRestrictMarkup_WRITE.Populate(oYesNoList, populateOptions1)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowCoverageMarkupDistribution, yesNoLkL, , , False)
        ddlAllowCoverageMarkupDistribution.Populate(oYesNoList, populateOptions1)

        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfEquipment, LookupListNew.DropdownLookupList("TEQP", langId, True))
        cboTypeOfEquipment.Populate(CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfIns, LookupListNew.DropdownLookupList("TINS", langId, True))
        cboTypeOfIns.Populate(CommonConfigManager.Current.ListManager.GetList("TINS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfMarketing, LookupListNew.DropdownLookupList("TMKT", langId, True))
        cboTypeOfMarketing.Populate(CommonConfigManager.Current.ListManager.GetList("TMKT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboReplacementPolicy, LookupListNew.GetReplacementPolicyLookupList(langId), "DESCRIPTION", "ID", False)
        cboReplacementPolicy.Populate(CommonConfigManager.Current.ListManager.GetList("REPP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCollectionCycleType, LookupListNew.GetCollectionCycleTypeLookupList(langId), "DESCRIPTION", "ID", True)
        cboCollectionCycleType.Populate(CommonConfigManager.Current.ListManager.GetList("COLLCYCLE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboReplacementBasedOn, LookupListNew.GetReplacementBasedOnLookupList(langId), "DESCRIPTION", "ID", True)
        cboReplacementBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("REPLOG", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboDeductibleBasedOn, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId),,, False)
        Dim olistcontext As ListContext = New ListContext()
        olistcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        cboDeductibleBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=olistcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                  })

        If Not State.MyBO.IsNew Then
            'ElitaPlusPage.BindListControlToDataView(Me.cboCancellationReason, LookupListNew.GetCancellationReasonByDealerIdLookupList(Me.State.MyBO.DealerId), "DESCRIPTION", "ID", True)
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = State.MyBO.DealerId
            cboCancellationReason.Populate(CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByDealer", Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = textFun
                                                  })
        End If

        'ElitaPlusPage.BindListControlToDataView(Me.cboClaimControlID, yesNoLkL)
        cboClaimControlID.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnorePremium, yesNoLkL)
        cboIgnorePremium.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnoreCovAmt, yesNoLkL)
        cboIgnoreCovAmt.Populate(oYesNoList, populateOptions)

        'REQ-490 - changed list from YES/NO to the EDIT MFG Term list
        'ElitaPlusPage.BindListControlToDataView(Me.cboEDIT_MFG_TERM, LookupListNew.DropdownLookupList(LookupListCache.LK_EDTMFGTRM, langId, True))
        cboEDIT_MFG_TERM.Populate(CommonConfigManager.Current.ListManager.GetList("EDTMFGTRM", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboCOINSURANCE, LookupListNew.GetCoinsuranceLookupList(langId), , , True)
        cboCOINSURANCE.Populate(CommonConfigManager.Current.ListManager.GetList("COINS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'Dim CoinsuranceLookupListSortedByCode As DataView = LookupListNew.GetCoinsuranceLookupList(langId)
        'CoinsuranceLookupListSortedByCode.Sort = "CODE"
        'ElitaPlusPage.BindListControlToDataView(Me.cboCOINSURANCE_Code, CoinsuranceLookupListSortedByCode, "CODE", , True)
        cboCOINSURANCE_Code.Populate(CommonConfigManager.Current.ListManager.GetList("COINS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

        'ElitaPlusPage.BindListControlToDataView(Me.cboCURRENCY_CONVERSION, yesNoLkL)
        cboCURRENCY_CONVERSION.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCURRENCY_OF_COVERAGES, currencyVW)
        cboCURRENCY_OF_COVERAGES.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAutoSetLiability, yesNoLkL)
        cboAutoSetLiability.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboBackEndClaimAllowed, yesNoLkL)
        cboBackEndClaimAllowed.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, yesNoLkL)
        cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboInstallmentPayment, yesNoLkL)
        cboInstallmentPayment.Populate(oYesNoList, populateOptions)
        'Dim IncludeFirstPayment As DataView = LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_INCLUDE_FIRST_PAYMENT, langId)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIncludeFirstPmt, IncludeFirstPayment, , , True)
        cboIncludeFirstPmt.Populate(CommonConfigManager.Current.ListManager.GetList("INCFIRSTPYMT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboExtendCoverage, covExtVW)
        cboExtendCoverage.Populate(CommonConfigManager.Current.ListManager.GetList("COVEXT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowPymtSkipMonths, yesNoLkL)
        ddlAllowPymtSkipMonths.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboDeductible_By_Manufacturer, yesNoLkL)
        cboDeductible_By_Manufacturer.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIsCommPCodeId, yesNoLkL)
        cboIsCommPCodeId.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboBaseInstallments, LookupListNew.DropdownLookupList("BINSTAL", langId, True))
        cboBaseInstallments.Populate(CommonConfigManager.Current.ListManager.GetList("BINSTAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboBillingCycleFrequency, LookupListNew.DropdownLookupList("BCYCLE", langId, True))
        cboBillingCycleFrequency.Populate(CommonConfigManager.Current.ListManager.GetList("BCYCLE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowDifferentCoverage, yesNoLkL)
        cboAllowDifferentCoverage.Populate(oYesNoList, populateOptions)
        'Added from Req-635
        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowNoExtended, yesNoLkL)
        cboAllowNoExtended.Populate(oYesNoList, populateOptions)

        'Req-703 Start
        'ElitaPlusPage.BindListControlToDataView(Me.cboMarketingPromo, yesNoLkL)
        cboMarketingPromo.Populate(oYesNoList, populateOptions)
        'Req-703 End

        'Dim allowCCRejectsList As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ALLOW_CC_REJECTIONS_CODE, langId, True)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowMultipleRejections, allowCCRejectsList)
        cboAllowMultipleRejections.Populate(CommonConfigManager.Current.ListManager.GetList("ACCR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'Dim proRataMethodVW As DataView = LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_PRO_RATA_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        'ElitaPlusPage.BindListControlToDataView(Me.cboProRataMethodId, proRataMethodVW)
        cboProRataMethodId.Populate(CommonConfigManager.Current.ListManager.GetList("PRMETHOD", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)


        'REQ-1073
        'ElitaPlusPage.BindListControlToDataView(Me.ddlBillingCycleType, LookupListNew.DropdownLookupList("BCT", langId))
        ddlBillingCycleType.Populate(CommonConfigManager.Current.ListManager.GetList("BCT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlDailyRateBasedOn, LookupListNew.DropdownLookupList("DRBO", langId))
        ddlDailyRateBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("DRBO", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'REQ-1005
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowBillingAfterCancellation, yesNoLkL)
        ddlAllowBillingAfterCancellation.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowCollectionAfterCancellation, yesNoLkL)
        ddlAllowCollectionAfterCancellation.Populate(oYesNoList, populateOptions)

        'REQ-5773 Start
        'BindListControlToDataView(Me.ddlPaymentProcessingTypeId, LookupListNew.DropdownLookupList(LookupListNew.LK_PPT, langId, True))
        ddlPaymentProcessingTypeId.Populate(CommonConfigManager.Current.ListManager.GetList("PPT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'REQ-5773 End

        'Me.cboOverrideEditMfgTerm.PopulateOld("OVERMFGTERM", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        cboOverrideEditMfgTerm.Populate(CommonConfigManager.Current.ListManager.GetList("OVERMFGTERM", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .TextFunc = AddressOf .GetDescription,
                    .ValueFunc = AddressOf .GetExtendedCode,
                    .SortFunc = AddressOf .GetDescription
                })

        'US489857
        BindSourceOptionDropdownlist()
    End Sub

    Private Sub PopulateAccountBusinessUnit(dealerId As Guid)
        'Get the accounting company associated with this contract / dealer / company to full out the business units
        'Dim dv As DataView

        If (Not dealerId.Equals(Guid.Empty)) Then

            If State.DealerBO Is Nothing Then
                State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
            End If

            State.Company_ID = State.DealerBO.CompanyId

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = State.DealerBO.CompanyId
            Dim AcctCompanyList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AccountingCompanyByCompany", context:=listcontext)

            Dim listcontext1 As ListContext = New ListContext()
            listcontext1.AccountingCompanyId = AcctCompanyList.FirstOrDefault().ListItemId
            Dim BusinessUnitList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessUnitByAcctCompany", context:=listcontext1)

            If BusinessUnitList.Count = 1 Then
                cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
                        {
                            .AddBlankItem = False,
                            .TextFunc = AddressOf .GetCode,
                            .SortFunc = AddressOf .GetCode
                        })
            Else
                cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .TextFunc = AddressOf .GetCode,
                            .SortFunc = AddressOf .GetCode
                        })
            End If

            'Dim arrCompanies As New ArrayList
            'arrCompanies.Add(Me.State.Company_ID)
            'If AcctCompany.GetAccountingCompanies(arrCompanies).Count = 1 Then
            '    dv = AcctBusinessUnit.getList(AcctCompany.GetAccountingCompanies(arrCompanies)(0).Id, Nothing)
            '    If dv.Count = 1 Then
            '        Me.BindListControlToDataView(Me.cboAcctBusinessUnit, dv, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID, False)
            '    Else
            '        Me.BindListControlToDataView(Me.cboAcctBusinessUnit, dv, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID)
            '    End If
            'End If
        Else
            'Me.BindListControlToDataView(Me.cboAcctBusinessUnit, AcctBusinessUnit.getList(Guid.NewGuid(), Nothing), AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID)
            Dim AcctCompanyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AcctCompany")
            Dim BusinessUnitList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext
            For Each _acctCompany As DataElements.ListItem In AcctCompanyList
                oListContext.AccountingCompanyId = _acctCompany.ListItemId
                Dim BusinessUnitListForAcctCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessUnitByAcctCompany", context:=oListContext)
                If BusinessUnitListForAcctCompany.Count > 0 Then
                    If BusinessUnitList IsNot Nothing Then
                        BusinessUnitList.AddRange(BusinessUnitListForAcctCompany)
                    Else
                        BusinessUnitList = BusinessUnitListForAcctCompany.Clone()
                    End If
                End If
            Next
            cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .TextFunc = AddressOf .GetCode,
                        .SortFunc = AddressOf .GetCode
                    })
        End If
    End Sub
    Private Sub PopulateProducer()

        If State.DealerBO IsNot Nothing Then
            Dim dealerCompany As New ArrayList
            dealerCompany.Add(State.DealerBO.CompanyId)

            Dim oProducerview As DataView = LookupListNew.GetProducerLookupList(dealerCompany)
            ElitaPlusPage.BindListControlToDataView(ddlProducer, oProducerview)
        Else
            Dim oProducerview As DataView = LookupListNew.GetProducerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            ElitaPlusPage.BindListControlToDataView(ddlProducer, oProducerview)
        End If

    End Sub
    Protected Sub PopulateFormFromBOs()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim policyType As Guid = Guid.Empty

        With State.MyBO

            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If (oDealer.DealerTypeDesc = "VSC") Then
                    If (.IsNew) Then
                        .CoverageDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                        .DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId), Codes.DEDUCTIBLE_BASED_ON__FIXED)
                    End If
                    ChangeEnabledProperty(cboCovDeductible, False)
                    EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, .IsNew, False)
                    ChangeEnabledProperty(cboDeductibleBasedOn, False)
                End If
            End If

            PopulateAccountBusinessUnit(State.MyBO.DealerId)

            PopulateControlFromBOProperty(TextboxAdminExpense, .AdminExpense, PERCENT_FORMAT)
            PopulateControlFromBOProperty(TextboxCommPercent, .CommissionsPercent, PERCENT_FORMAT)
            PopulateControlFromBOProperty(TextboxDaysToCancel, .CancellationDays)
            PopulateControlFromBOProperty(TextboxDaysToSuspend, .SuspenseDays)
            PopulateControlFromBOProperty(TextboxDeductible, .Deductible)
            PopulateControlFromBOProperty(TextboxDeductiblePercent, .DeductiblePercent)
            PopulateControlFromBOProperty(TextboxEndDate_WRITE, .Expiration)
            PopulateControlFromBOProperty(TextboxLayout, .Layout)
            PopulateControlFromBOProperty(TextboxFullRefundDays, .FullRefundDays)
            PopulateControlFromBOProperty(TextboxLossCostPercent, .LossCostPercent, PERCENT_FORMAT)
            PopulateControlFromBOProperty(TextboxMarketingExpense, .MarketingPercent, PERCENT_FORMAT)
            PopulateControlFromBOProperty(TextboxMinRepCost, .MinReplacementCost)
            PopulateControlFromBOProperty(TextboxPolicyNumber, .Policy)
            PopulateControlFromBOProperty(TextboxProfitExpense, .ProfitPercent, PERCENT_FORMAT)
            PopulateControlFromBOProperty(TextboxStartDate_WRITE, .Effective)
            PopulateControlFromBOProperty(TextboxWaitingPeriod, .WaitingPeriod)
            PopulateControlFromBOProperty(TextboxWarrantyMaxDelay, .WarrantyMaxDelay)
            PopulateControlFromBOProperty(TextBoxRemainingMFGDays, .RemainingMFGDays)
            PopulateControlFromBOProperty(TextboxDaysOfFirstPymt, .DaysOfFirstPymt)
            PopulateControlFromBOProperty(TextboxDaysToSendLetter, .DaysToSendLetter)
            PopulateControlFromBOProperty(TextboxDaysToCancelCert, .DaysToCancelCert)
            PopulateControlFromBOProperty(txtCLIPPct, .ClipPercent)
            PopulateControlFromBOProperty(txtInsPremFactor, .InsPremiumFactor, PERCENT_FORMAT)
            PopulateControlFromBOProperty(txtExtendCoverageByExtraMonths, .ExtraMonsToExtendCoverage)
            PopulateControlFromBOProperty(txtExtendCoverageByExtraDays, .ExtraDaysToExtendCoverage)
            PopulateControlFromBOProperty(cboCustAddressRequired, .CustmerAddressRequiredId)
            PopulateControlFromBOProperty(txtFirstPaymentMonths, .FirstPymtMonths)
            'Req - 1016 Start
            PopulateControlFromBOProperty(txtPeridiocBillingWarntyPeriod, .RecurringWarrantyPeriod)
            'Req - 1016 End
            SetSelectedItem(cboAutoMFG, .AutoMfgCoverageId)
            SetSelectedItem(cboContractType, .ContractTypeId)

            ' Individual Policy related controls.
            If State.MyBO.IsNew Then
                ' Default setting to CP/ME
                SetSelectedItem(cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))
                cboLineOfBusiness.Items.Clear()
            Else
                SetSelectedItem(cboPolicyType, .PolicyTypeId)
                SetSelectedItem(cboCollectivePolicyGeneration, .PolicyGenerationId)
                PopulateLineOfBusinessDropDown(.PolicyTypeId, True)

                If .PolicyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) AndAlso
                .PolicyGenerationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then

                    ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                    ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                    ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
                End If

                If Not .LineOfBusinessId.Equals(Guid.Empty) Then
                    SetSelectedItem(cboLineOfBusiness, .LineOfBusinessId)
                End If

            End If

            SetSelectedItem(cboCurrency, .CurrencyId)
            TheDealerControl.SelectedGuid = .DealerId
            SetDealerCode()

            SetSelectedItem(cboDealerMarkup_WRITE, .DealerMarkupId)
            SetSelectedItem(cboEditModel, .EditModelId)
            SetSelectedItem(cboFixedEscDurationFlag, .FixedEscDurationFlag)
            SetSelectedItem(cboFundingSource, .FundingSourceId)
            'Req-1016 Start
            'Me.SetSelectedItem(Me.cboMonthlyBilling, .MonthlyBillingId)
            SetSelectedItem(cboRecurringPremium, .RecurringPremiumId)
            'Req-1016 end
            SetSelectedItem(cboNetCommission, .NetCommissionsId)
            SetSelectedItem(cboNetMarketing, .NetMarketingId)
            SetSelectedItem(cboNetTaxes, .NetTaxesId)
            SetSelectedItem(cboRestrictMarkup_WRITE, .RestrictMarkupId)

            'Def-25991:Added condition to check null value for .AllowCoverageMarkupDistribution.
            If (.AllowCoverageMarkupDistribution <> Guid.Empty) Then
                SetSelectedItem(ddlAllowCoverageMarkupDistribution, .AllowCoverageMarkupDistribution)
            End If

            SetSelectedItem(cboTypeOfEquipment, .TypeOfEquipmentId)
            SetSelectedItem(cboTypeOfIns, .TypeOfInsuranceId)
            SetSelectedItem(cboTypeOfMarketing, .TypeOfMarketingId)
            SetSelectedItem(cboAutoSetLiability, .AutoSetLiabilityId)
            SetSelectedItem(cboCovDeductible, .CoverageDeductibleId)
            SetSelectedItem(cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, .IgnoreWaitingPeriodWsdPsd)
            SetSelectedItem(cboInstallmentPayment, .InstallmentPaymentId)
            SetSelectedItem(cboDeductible_By_Manufacturer, .DeductibleByManufacturerId)

            Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)
            If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__FIXED))
            ElseIf deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION Then
                SetSelectedItem(cboDeductibleBasedOn, .DeductibleExpressionId)
            Else
                SetSelectedItem(cboDeductibleBasedOn, .DeductibleBasedOnId)
            End If

            SetSelectedItem(cboPayOutstandingAmount, .PayOutstandingPremiumId)

            EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, False, False)

            If Not State.MyBO.IsNew Then
                SetSelectedItem(cboReplacementPolicy, .ReplacementPolicyId)
            End If

            If cboCancellationReason.Items.Count > 1 Then
                SetSelectedItem(cboCancellationReason, .CancellationReasonId)
            End If

            SetSelectedItem(cboID_VALIDATION, .ID_Validation_Id)
            SetSelectedItem(cboClaimControlID, .ClaimControlID)
            SetSelectedItem(cboIgnorePremium, .IgnoreIncomingPremiumID)
            SetSelectedItem(cboAcselProdCode, .Acsel_Prod_Code_Id)
            SetSelectedItem(cboIgnoreCovAmt, .IgnoreCoverageAmtId)
            SetSelectedItem(cboEDIT_MFG_TERM, .EditMFGTermId)

            If .EditMFGTermId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE)) Then
                ControlMgr.SetVisibleControl(Me, LabelOverrideEditMfgTerm, False)
                ControlMgr.SetVisibleControl(Me, cboOverrideEditMfgTerm, False)
            Else
                ControlMgr.SetVisibleControl(Me, LabelOverrideEditMfgTerm, True)
                ControlMgr.SetVisibleControl(Me, cboOverrideEditMfgTerm, True)
            End If

            BindSelectItem(.OverrideEditMfgTerm, cboOverrideEditMfgTerm)

            SetSelectedItem(cboBackEndClaimAllowed, .BackEndClaimsAllowedId)

            SetSelectedItem(cboCOINSURANCE, .CoinsuranceId)
            PopulateControlFromBOProperty(TextboxPARTICIPATION_PERCENT, .ParticipationPercent)
            PopulateControlFromBOProperty(TextboxRatingPlan, .RatingPlan)

            PopulateControlFromBOProperty(TextboxRepairDiscountPct, .RepairDiscountPct)
            PopulateControlFromBOProperty(TextboxReplacementDiscountPct, .ReplacementDiscountPct)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_COINSURANCE, .CoinsuranceId).Equals(DIRECT) Then
                TextboxPARTICIPATION_PERCENT.Enabled = False
                PopulateControlFromBOProperty(TextboxPARTICIPATION_PERCENT, New DecimalType(100), DECIMAL_FORMAT)
            Else
                TextboxPARTICIPATION_PERCENT.Enabled = True
            End If
            HiddenPARTICIPATION_PERCENTAG.Value = TextboxPARTICIPATION_PERCENT.Text

            SetSelectedItem(cboCURRENCY_CONVERSION, .CurrencyConversionId)
            SetSelectedItem(cboCURRENCY_OF_COVERAGES, .CurrencyOfCoveragesId)

            PopulateControlFromBOProperty(txtNumOfClaims, .NumOfClaims)
            PopulateControlFromBOProperty(txtNumOfRepairClaims, .NumOfRepairClaims)
            PopulateControlFromBOProperty(txtNumOfReplClaims, .NumOfReplacementClaims)

            PopulateControlFromBOProperty(txtPenaltyPct, .PenaltyPct)
            PopulateControlFromBOProperty(txtAuthorizedAmountMaxUpdates, .AuthorizedAmountMaxUpdates)
            ' DEF-24575 Start
            Dim arrCompanies As New ArrayList
            Dim dv As DataView
            arrCompanies.Add(State.Company_ID)
            dv = AcctBusinessUnit.getList(AcctCompany.GetAccountingCompanies(arrCompanies)(0).Id, Nothing)
            If dv.Count > 1 Then
                'DEF-25009 Start.
                'Added condition to show the text selected as ALL in the Accounting Business Unit Drop down of Contract Screen.
                If (cboAcctBusinessUnit.Items(0).Text = String.Empty) Then
                    cboAcctBusinessUnit.Items.RemoveAt(0)
                    cboAcctBusinessUnit.Items.Insert(0, New ListItem("ALL", Guid.Empty.ToString))
                End If
                If (.AcctBusinessUnitId <> Guid.Empty) Then
                    SetSelectedItem(cboAcctBusinessUnit, .AcctBusinessUnitId)
                Else
                    SetSelectedItem(cboAcctBusinessUnit, Guid.Empty.ToString)
                End If
                'DEF-25009 End
            End If
            ' DEF-24575 End
            SetSelectedItem(cboIsCommPCodeId, .IsCommPCodeId)

            SetSelectedItem(cboBaseInstallments, .BaseInstallments)
            SetSelectedItem(cboBillingCycleFrequency, .BillingCycleFrequency)
            PopulateControlFromBOProperty(TextboxInstallmentsBaseReducer, .InstallmentsBaseReducer)
            PopulateControlFromBOProperty(TextboxMaxNumofInstallments, .MaxInstallments)
            PopulateControlFromBOProperty(txtCollectionReAttempts, .CollectionReAttempts)
            PopulateControlFromBOProperty(txtPastDueMonthsAllowed, .PastDueMonthsAllowed)
            PopulateControlFromBOProperty(TextboxCycleDay, .CycleDay)
            PopulateControlFromBOProperty(TextboxOffsetBeforeDueDate, .OffsetBeforeDueDate)
            SetSelectedItem(cboCollectionCycleType, .CollectionCycleTypeId)
            SetSelectedItem(cboIncludeFirstPmt, .IncludeFirstPmt)
            SetSelectedItem(cboExtendCoverage, .ExtendCoverageId)
            SetSelectedItem(ddlAllowPymtSkipMonths, .AllowPymtSkipMonths)
            SetSelectedItem(cboReplacementBasedOn, .ClaimLimitBasedOnId)
            SetSelectedItem(cboAllowDifferentCoverage, .AllowDifferentCoverage)
            'Added from Req-635
            SetSelectedItem(cboAllowNoExtended, .AllowNoExtended)
            'Req-703 Start
            SetSelectedItem(cboMarketingPromo, .MarketingPromotionId)
            'Req-703 End

            SetSelectedItem(cboProRataMethodId, .ProRataMethodId)

            SetSelectedItem(cboAllowMultipleRejections, .AllowMultipleRejectionsId)
            'Req-703 End
            PopulateControlFromBOProperty(txtDaysToReportClaim, .DaysToReportClaim)

            ''REQ-794
            'The below lines of code is commented for 1861 defect.
            'If Not Me.State.MyBO.IsNew Then
            '    Me.PopulateControlFromBOProperty(Me.cboIgnoreCovRate, .IgnoreCoverageRateId)
            'End If
            'REQ-5773 Start
            PopulateControlFromBOProperty(ddlPaymentProcessingTypeId, .PaymentProcessingTypeId)
            PopulateControlFromBOProperty(txtThirdPartyName, .ThirdPartyName)
            PopulateControlFromBOProperty(txtThirdPartyTaxId, .ThirdPartyTaxId)
            PopulateControlFromBOProperty(txtRdoName, .RdoName)
            PopulateControlFromBOProperty(txtRdoTaxId, .RdoTaxId)
            PopulateControlFromBOProperty(txtRdoPercent, .RdoPercent, PERCENT_FORMAT)
            'REQ-5773 End

            'REQ-1050 start
            PopulateControlFromBOProperty(txtbxDaysToReactivate, .DaysToReactivate)
            'REQ-1050 END

            'REQ-1073
            SetSelectedItem(ddlBillingCycleType, .BillingCycleTypeId)
            SetSelectedItem(ddlDailyRateBasedOn, .DailyRateBasedOnId)

            'REQ-1005
            SetSelectedItem(ddlAllowBillingAfterCancellation, .AllowBillingAfterCancellation)
            SetSelectedItem(ddlAllowCollectionAfterCancellation, .AllowCollectionAfterCancellation)

            'REQ-1333
            PopulateControlFromBOProperty(txtReplacementPolicyCliamCount, .ReplacementPolicyClaimCount)

            'REQ-1198
            PopulateControlFromBOProperty(cboFutureDateAllowFor, .FutureDateAllowForID)

            'REQ-5372
            If State.RepPolicyList Is Nothing Then
                State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(State.MyBO.Id)
            End If
            PopulateReplacementPolicyGrid(State.RepPolicyList)
            If State.DepreciationScheduleList Is Nothing Then
                State.DepreciationScheduleList = DepreciationScdRelation.GetDepreciationScheduleList(State.MyBO.Id)
            End If
            PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)

            SetSelectedItem(ddlProducer, .ProducerId)

            ' US - 489857
            PopulateSourceDropdownBucketFromBOs()
        End With

    End Sub

    Private Sub EnableDisableDeductible(pCoverageDeductibleId As Guid, pDeductibleBasedOnId As Guid,
        pClearValues As Boolean, pClearDeductibleBasedOnId As Boolean)
        Dim sCoverageDeductibleCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, pCoverageDeductibleId)
        Dim sDeductibleBasedOnCode As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Select Case sCoverageDeductibleCode
            Case YES
                ControlMgr.SetEnableControl(Me, TextboxDeductible, False)
                ControlMgr.SetEnableControl(Me, TextboxDeductiblePercent, False)
                ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, False)
                If (pClearValues) Then
                    TextboxDeductible.Text = "0"
                    TextboxDeductiblePercent.Text = "0"
                    ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End If
            Case NO
                ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, True)
                If (pClearDeductibleBasedOnId) Then
                    ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End If
                sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, pDeductibleBasedOnId)
                If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                    ' orelse (String.IsNullOrWhiteSpace(sDeductibleBasedOnCode)))

                    ControlMgr.SetEnableControl(Me, TextboxDeductible, True)
                    ControlMgr.SetEnableControl(Me, TextboxDeductiblePercent, False)
                    If (pClearValues) Then
                        TextboxDeductiblePercent.Text = "0"
                    End If
                ElseIf ((sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then
                    ControlMgr.SetEnableControl(Me, TextboxDeductible, False)
                    ControlMgr.SetEnableControl(Me, TextboxDeductiblePercent, True)
                    If (pClearValues) Then
                        TextboxDeductible.Text = "0"
                    End If
                Else
                    ControlMgr.SetEnableControl(Me, TextboxDeductible, False)
                    ControlMgr.SetEnableControl(Me, TextboxDeductiblePercent, False)
                    ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, True)
                    If (pClearValues) Then
                        TextboxDeductible.Text = "0"
                        TextboxDeductiblePercent.Text = "0"
                    End If
                End If
            Case Else
                ControlMgr.SetEnableControl(Me, TextboxDeductible, False)
                ControlMgr.SetEnableControl(Me, TextboxDeductiblePercent, False)
                ControlMgr.SetEnableControl(Me, cboDeductibleBasedOn, False)
                If (pClearValues) Then
                    TextboxDeductible.Text = "0"
                    TextboxDeductiblePercent.Text = "0"
                    ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End If
        End Select
    End Sub

    Private Sub EnableFirstPaymentMonthsField()
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        Dim includeFirstPremiumNoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_INCLUDE_FIRST_PAYMENT, Codes.INCLUDE_FIRST_PAYMENT_NO)

        'Req - 1016 - Start
        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
        'If Me.GetSelectedItem(Me.cboRecurringPremium).Equals(YesId) And Me.GetSelectedItem(Me.cboInstallmentPayment).Equals(YesId) Then
        If ((Not GetSelectedItem(cboRecurringPremium).Equals(Guid.Empty) AndAlso Not GetSelectedItem(cboRecurringPremium).Equals(singlePremiumId)) _
                    AndAlso GetSelectedItem(cboInstallmentPayment).Equals(YesId)) Then
            'Req - 1016 - end
            If GetSelectedItem(cboIncludeFirstPmt).Equals(includeFirstPremiumNoId) Then
                ControlMgr.SetEnableControl(Me, txtFirstPaymentMonths, True)
            Else
                ControlMgr.SetEnableControl(Me, txtFirstPaymentMonths, False)
                txtFirstPaymentMonths.Text = ONE_STRING
            End If
        Else
            ControlMgr.SetEnableControl(Me, txtFirstPaymentMonths, False)
            txtFirstPaymentMonths.Text = ZERO_STRING
        End If
    End Sub

    Private Sub ShowInstPymtFields()
        Dim sval As String
        sval = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboInstallmentPayment))
        'Req - 1016 Start
        'Dim currMonthlyBilling As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.GetSelectedItem(Me.cboRecurringPremium))
        Dim currMonthlyBilling As String
        Dim sPeriodRenew As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, GetSelectedItem(cboRecurringPremium))
        If sPeriodRenew Is Nothing OrElse sPeriodRenew = "0" Then
            currMonthlyBilling = "N"
        End If
        'Req - 1016 End
        Select Case sval
            Case YES
                ControlMgr.SetVisibleControl(Me, TextboxDaysOfFirstPymt, True)
                ControlMgr.SetVisibleControl(Me, LabelDaysOfFirstPymt, True)
                moInstallmentBillingInformation0.Attributes("style") = ""
                moInstallmentBillingInformation4.Attributes("style") = ""
                moInstallmentBillingInformation5.Attributes("style") = ""

                If State.DealerBO Is Nothing Then
                    State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
                End If

                If State.DealerBO.DealerTypeDesc = State.DealerBO.DEALER_TYPE_DESC OrElse State.MyBO.InstallmentPaymentId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)) Then
                    moInstallmentBillingInformation1.Attributes("style") = ""
                    moInstallmentBillingInformation2.Attributes("style") = ""
                    moInstallmentBillingInformation3.Attributes("style") = ""

                    If State.MyBO.IsNew Then
                        TextboxInstallmentsBaseReducer.Text = ZERO_STRING
                        TextboxMaxNumofInstallments.Text = ONE_STRING
                        If State.DealerBO.DealerTypeDesc = State.DealerBO.DEALER_TYPE_DESC Then
                            txtCollectionReAttempts.Text = ZERO_STRING
                        End If
                        txtPastDueMonthsAllowed.Text = ZERO_STRING
                    Else
                        If State.MyBO.InstallmentsBaseReducer Is Nothing OrElse Not State.MyBO.InstallmentsBaseReducer.Value > 0 Then
                            TextboxInstallmentsBaseReducer.Text = ZERO_STRING
                        Else
                            PopulateControlFromBOProperty(TextboxInstallmentsBaseReducer, State.MyBO.InstallmentsBaseReducer)
                        End If
                        If State.MyBO.MaxInstallments Is Nothing OrElse Not State.MyBO.MaxInstallments.Value > 0 Then
                            TextboxMaxNumofInstallments.Text = ONE_STRING
                        Else
                            PopulateControlFromBOProperty(TextboxMaxNumofInstallments, State.MyBO.MaxInstallments)
                        End If
                        If State.MyBO.CollectionReAttempts Is Nothing OrElse Not State.MyBO.CollectionReAttempts.Value > 0 Then
                            txtCollectionReAttempts.Text = ZERO_STRING
                        Else
                            PopulateControlFromBOProperty(txtCollectionReAttempts, State.MyBO.CollectionReAttempts)
                        End If
                        If State.MyBO.PastDueMonthsAllowed Is Nothing OrElse Not State.MyBO.PastDueMonthsAllowed.Value > 0 Then
                            txtPastDueMonthsAllowed.Text = ZERO_STRING
                        Else
                            PopulateControlFromBOProperty(txtPastDueMonthsAllowed, State.MyBO.PastDueMonthsAllowed)
                        End If
                    End If
                Else

                    moInstallmentBillingInformation1.Attributes("style") = "display: none"
                    moInstallmentBillingInformation2.Attributes("style") = "display: none"
                    moInstallmentBillingInformation3.Attributes("style") = "display: none"

                    txtCollectionReAttempts.Text = Nothing
                    txtPastDueMonthsAllowed.Text = Nothing
                    TextboxInstallmentsBaseReducer.Text = Nothing
                    TextboxMaxNumofInstallments.Text = Nothing

                End If

                If currMonthlyBilling = NO Then
                    ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, True)
                    ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, True)
                End If
            Case Else
                ControlMgr.SetVisibleControl(Me, TextboxDaysOfFirstPymt, False)
                ControlMgr.SetVisibleControl(Me, LabelDaysOfFirstPymt, False)
                moInstallmentBillingInformation0.Attributes("style") = "display: none"
                moInstallmentBillingInformation1.Attributes("style") = "display: none"
                moInstallmentBillingInformation2.Attributes("style") = "display: none"
                moInstallmentBillingInformation3.Attributes("style") = "display: none"
                moInstallmentBillingInformation4.Attributes("style") = "display: none"
                moInstallmentBillingInformation5.Attributes("style") = "display: none"
                txtCollectionReAttempts.Text = Nothing
                txtPastDueMonthsAllowed.Text = Nothing
                TextboxInstallmentsBaseReducer.Text = Nothing
                TextboxMaxNumofInstallments.Text = Nothing
                TextboxOffsetBeforeDueDate.Text = Nothing
                TextboxCycleDay.Text = Nothing
                cboCollectionCycleType.SelectedIndex = NO_ITEM_SELECTED_INDEX
                ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, False)
                ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, False)
                cboPayOutstandingAmount.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Select
    End Sub

    Protected Sub PopulateBOsFormFrom()
        Dim sval As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With State.MyBO
            PopulateBOProperty(State.MyBO, "AdminExpense", TextboxAdminExpense)
            PopulateBOProperty(State.MyBO, "AutoMfgCoverageId", cboAutoMFG)
            PopulateBOProperty(State.MyBO, "CancellationDays", TextboxDaysToCancel)
            '.CertificatesAutonumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            PopulateBOProperty(State.MyBO, "CommissionsPercent", TextboxCommPercent)
            PopulateBOProperty(State.MyBO, "ContractTypeId", cboContractType)

            ' Individual Policy Req fields
            PopulateBOProperty(State.MyBO, "PolicyTypeId", cboPolicyType)
            PopulateBOProperty(State.MyBO, "PolicyGenerationId", cboCollectivePolicyGeneration)
            PopulateBOProperty(State.MyBO, "LineOfBusinessId", cboLineOfBusiness)
            '-----------------------------------------------------------

            PopulateBOProperty(State.MyBO, "CurrencyId", cboCurrency)
            PopulateBOProperty(State.MyBO, "DealerId", TheDealerControl.SelectedGuid)
            ' Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.cboDealer_WRITE)
            PopulateBOProperty(State.MyBO, "DealerMarkupId", cboDealerMarkup_WRITE)
            PopulateBOProperty(State.MyBO, "Deductible", TextboxDeductible)
            PopulateBOProperty(State.MyBO, "DeductiblePercent", TextboxDeductiblePercent)

            Dim deductibleBasedOnId As Guid = GetSelectedItem(cboDeductibleBasedOn)

            If (deductibleBasedOnId = Guid.Empty) Then
                PopulateBOProperty(State.MyBO, "DeductibleBasedOnId", cboDeductibleBasedOn)
                PopulateBOProperty(State.MyBO, "DeductibleExpressionId", Guid.Empty)
            Else

                Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)

                If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then

                    PopulateBOProperty(State.MyBO, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                    PopulateBOProperty(State.MyBO, "DeductibleExpressionId", deductibleBasedOnId)

                ElseIf (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__COMPUTED_EXTERNALLY) Then

                    PopulateBOProperty(State.MyBO, "DeductibleBasedOnId", cboDeductibleBasedOn)
                    PopulateBOProperty(State.MyBO, "DeductibleExpressionId", Guid.Empty)
                Else
                    PopulateBOProperty(State.MyBO, "DeductibleBasedOnId", cboDeductibleBasedOn)
                    PopulateBOProperty(State.MyBO, "DeductibleExpressionId", deductibleBasedOnId)
                End If

            End If

            PopulateBOProperty(State.MyBO, "EditModelId", cboEditModel)
            PopulateBOProperty(State.MyBO, "Effective", TextboxStartDate_WRITE)
            PopulateBOProperty(State.MyBO, "FixedEscDurationFlag", cboFixedEscDurationFlag)
            PopulateBOProperty(State.MyBO, "Expiration", TextboxEndDate_WRITE)
            PopulateBOProperty(State.MyBO, "FundingSourceId", cboFundingSource)
            'Me.PopulateBOProperty(Me.State.MyBO, "LastRecon", Me.TextboxLastReconDate)
            PopulateBOProperty(State.MyBO, "Layout", TextboxLayout)
            PopulateBOProperty(State.MyBO, "FullRefundDays", TextboxFullRefundDays)
            PopulateBOProperty(State.MyBO, "LossCostPercent", TextboxLossCostPercent)
            PopulateBOProperty(State.MyBO, "MarketingPercent", TextboxMarketingExpense)
            PopulateBOProperty(State.MyBO, "MinReplacementCost", TextboxMinRepCost)
            'Req-1016 - Start
            'Me.PopulateBOProperty(Me.State.MyBO, "MonthlyBillingId", Me.cboMonthlyBilling)
            PopulateBOProperty(State.MyBO, "RecurringPremiumId", cboRecurringPremium)
            PopulateBOProperty(State.MyBO, "RecurringWarrantyPeriod", txtPeridiocBillingWarntyPeriod)
            'Req-1016 - End
            PopulateBOProperty(State.MyBO, "NetCommissionsId", cboNetCommission)
            PopulateBOProperty(State.MyBO, "NetMarketingId", cboNetMarketing)
            PopulateBOProperty(State.MyBO, "NetTaxesId", cboNetTaxes)
            PopulateBOProperty(State.MyBO, "Policy", TextboxPolicyNumber)
            PopulateBOProperty(State.MyBO, "ProfitPercent", TextboxProfitExpense)
            PopulateBOProperty(State.MyBO, "RestrictMarkupId", cboRestrictMarkup_WRITE)
            PopulateBOProperty(State.MyBO, "AllowCoverageMarkupDistribution", ddlAllowCoverageMarkupDistribution)
            PopulateBOProperty(State.MyBO, "SuspenseDays", TextboxDaysToSuspend)
            PopulateBOProperty(State.MyBO, "TypeOfEquipmentId", cboTypeOfEquipment)
            PopulateBOProperty(State.MyBO, "TypeOfInsuranceId", cboTypeOfIns)
            PopulateBOProperty(State.MyBO, "TypeOfMarketingId", cboTypeOfMarketing)
            PopulateBOProperty(State.MyBO, "WaitingPeriod", TextboxWaitingPeriod)
            PopulateBOProperty(State.MyBO, "WarrantyMaxDelay", TextboxWarrantyMaxDelay)
            PopulateBOProperty(State.MyBO, "RemainingMFGDays", TextBoxRemainingMFGDays)

            PopulateBOProperty(State.MyBO, "ReplacementPolicyId", cboReplacementPolicy)
            'REQ-1333
            PopulateBOProperty(State.MyBO, "ReplacementPolicyClaimCount", txtReplacementPolicyCliamCount)

            PopulateBOProperty(State.MyBO, "CancellationReasonId", cboCancellationReason)
            PopulateBOProperty(State.MyBO, "ID_Validation_Id", cboID_VALIDATION)
            PopulateBOProperty(State.MyBO, "Acsel_Prod_Code_Id", cboAcselProdCode)
            PopulateBOProperty(State.MyBO, "ClaimControlID", cboClaimControlID)
            PopulateBOProperty(State.MyBO, "IgnoreIncomingPremiumID", cboIgnorePremium)
            PopulateBOProperty(State.MyBO, "IgnoreCoverageAmtId", cboIgnoreCovAmt)
            PopulateBOProperty(State.MyBO, "EditMFGTermId", cboEDIT_MFG_TERM)
            PopulateBOProperty(State.MyBO, "BackEndClaimsAllowedId", cboBackEndClaimAllowed)
            PopulateBOProperty(State.MyBO, "IgnoreWaitingPeriodWsdPsd", cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD)
            PopulateBOProperty(State.MyBO, "InstallmentPaymentId", cboInstallmentPayment)
            PopulateBOProperty(State.MyBO, "CustmerAddressRequiredId", cboCustAddressRequired)
            PopulateBOProperty(State.MyBO, "FirstPymtMonths", txtFirstPaymentMonths)

            PopulateBOProperty(State.MyBO, "DeductibleByManufacturerId", cboDeductible_By_Manufacturer)
            PopulateBOProperty(State.MyBO, "ClipPercent", txtCLIPPct)
            PopulateBOProperty(State.MyBO, "IncludeFirstPmt", cboIncludeFirstPmt)
            PopulateBOProperty(State.MyBO, "ExtendCoverageId", cboExtendCoverage)
            PopulateBOProperty(State.MyBO, "ExtraDaysToExtendCoverage", txtExtendCoverageByExtraDays)
            PopulateBOProperty(State.MyBO, "ExtraMonsToExtendCoverage", txtExtendCoverageByExtraMonths)

            sval = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, .InstallmentPaymentId)
            If sval = YES Then
                PopulateBOProperty(State.MyBO, "DaysOfFirstPymt", TextboxDaysOfFirstPymt)
                PopulateBOProperty(State.MyBO, "DaysToSendLetter", TextboxDaysToSendLetter)
                PopulateBOProperty(State.MyBO, "DaysToCancelCert", TextboxDaysToCancelCert)
                PopulateBOProperty(State.MyBO, "PastDueMonthsAllowed", txtPastDueMonthsAllowed)
                If State.DealerBO.DealerTypeDesc = State.DealerBO.DEALER_TYPE_DESC Then
                    PopulateBOProperty(State.MyBO, "CollectionReAttempts", txtCollectionReAttempts)
                End If
            Else
                PopulateBOProperty(State.MyBO, "DaysOfFirstPymt", "0")
                PopulateBOProperty(State.MyBO, "DaysToSendLetter", "0")
                PopulateBOProperty(State.MyBO, "DaysToCancelCert", "0")
                State.MyBO.PastDueMonthsAllowed = Nothing
                State.MyBO.CollectionReAttempts = Nothing
            End If

            PopulateBOProperty(State.MyBO, "FutureDateAllowForID", cboFutureDateAllowFor)

            PopulateBOProperty(State.MyBO, "CoinsuranceId", cboCOINSURANCE)
            PopulateBOProperty(State.MyBO, "ParticipationPercent", TextboxPARTICIPATION_PERCENT)
            PopulateBOProperty(State.MyBO, "RatingPlan", TextboxRatingPlan)

            PopulateBOProperty(State.MyBO, "CurrencyConversionId", cboCURRENCY_CONVERSION)
            PopulateBOProperty(State.MyBO, "CurrencyOfCoveragesId", cboCURRENCY_OF_COVERAGES)
            PopulateBOProperty(State.MyBO, "AutoSetLiabilityId", cboAutoSetLiability)
            PopulateBOProperty(State.MyBO, "CoverageDeductibleId", cboCovDeductible)
            PopulateBOProperty(State.MyBO, "PayOutstandingPremiumId", cboPayOutstandingAmount)
            PopulateBOProperty(State.MyBO, "AuthorizedAmountMaxUpdates", txtAuthorizedAmountMaxUpdates)

            PopulateBOProperty(State.MyBO, "RepairDiscountPct", TextboxRepairDiscountPct)
            PopulateBOProperty(State.MyBO, "ReplacementDiscountPct", TextboxReplacementDiscountPct)

            PopulateBOProperty(State.MyBO, "NumOfClaims", txtNumOfClaims)
            PopulateBOProperty(State.MyBO, "NumOfRepairClaims", txtNumOfRepairClaims)
            PopulateBOProperty(State.MyBO, "NumOfReplacementClaims", txtNumOfReplClaims)

            PopulateBOProperty(State.MyBO, "PenaltyPct", txtPenaltyPct)
            PopulateBOProperty(State.MyBO, "AcctBusinessUnitId", cboAcctBusinessUnit)
            PopulateBOProperty(State.MyBO, "IsCommPCodeId", cboIsCommPCodeId)
            PopulateBOProperty(State.MyBO, "ProducerId", ddlProducer)
            PopulateBOProperty(State.MyBO, "BaseInstallments", cboBaseInstallments)
            PopulateBOProperty(State.MyBO, "BillingCycleFrequency", cboBillingCycleFrequency)
            PopulateBOProperty(State.MyBO, "InstallmentsBaseReducer", TextboxInstallmentsBaseReducer)
            PopulateBOProperty(State.MyBO, "MaxInstallments", TextboxMaxNumofInstallments)

            PopulateBOProperty(State.MyBO, "CollectionCycleTypeId", cboCollectionCycleType)
            PopulateBOProperty(State.MyBO, "CycleDay", TextboxCycleDay)
            PopulateBOProperty(State.MyBO, "OffsetBeforeDueDate", TextboxOffsetBeforeDueDate)
            PopulateBOProperty(State.MyBO, "InsPremiumFactor", txtInsPremFactor)
            PopulateBOProperty(State.MyBO, "ClaimLimitBasedOnId", cboReplacementBasedOn)
            PopulateBOProperty(State.MyBO, "AllowDifferentCoverage", cboAllowDifferentCoverage)
            'Added for Req-635
            PopulateBOProperty(State.MyBO, "AllowNoExtended", cboAllowNoExtended)

            PopulateBOProperty(State.MyBO, "DaysToReportClaim", txtDaysToReportClaim)

            'Req-703 Start
            PopulateBOProperty(State.MyBO, "MarketingPromotionId", cboMarketingPromo)
            'Req-703 End

            PopulateBOProperty(State.MyBO, "ProRataMethodId", cboProRataMethodId)

            PopulateBOProperty(State.MyBO, "AllowMultipleRejectionsId", cboAllowMultipleRejections)

            PopulateBOProperty(State.MyBO, "AllowPymtSkipMonths", ddlAllowPymtSkipMonths)
            ''REQ-794
            'The below line of is commented for the def1861
            'Me.PopulateBOProperty(Me.State.MyBO, "IgnoreCoverageRateId", Me.cboIgnoreCovRate)

            'REQ-1050 start
            PopulateBOProperty(State.MyBO, "DaysToReactivate", txtbxDaysToReactivate)
            'REQ-1050 END

            'REQ-1073
            PopulateBOProperty(State.MyBO, "DailyRateBasedOnId", ddlDailyRateBasedOn)
            PopulateBOProperty(State.MyBO, "BillingCycleTypeId", ddlBillingCycleType)

            'REQ-1005
            PopulateBOProperty(State.MyBO, "AllowBillingAfterCancellation", ddlAllowBillingAfterCancellation)
            PopulateBOProperty(State.MyBO, "AllowCollectionAfterCancellation", ddlAllowCollectionAfterCancellation)

            'REQ-5773 Start
            PopulateBOProperty(State.MyBO, "PaymentProcessingTypeId", ddlPaymentProcessingTypeId)
            PopulateBOProperty(State.MyBO, "ThirdPartyName", txtThirdPartyName)
            PopulateBOProperty(State.MyBO, "ThirdPartyTaxId", txtThirdPartyTaxId)
            PopulateBOProperty(State.MyBO, "RdoName", txtRdoName)
            PopulateBOProperty(State.MyBO, "RdoTaxId", txtRdoTaxId)
            PopulateBOProperty(State.MyBO, "RdoPercent", txtRdoPercent)
            'REQ-5773 End

            PopulateBOProperty(State.MyBO, "OverrideEditMfgTerm", cboOverrideEditMfgTerm, False, True)

            ''# US 489857
            'PoupulateBOsFromSourceDropDownBucket()

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub SetDefaultDates()
        Dim selectedDealerId As Guid = TheDealerControl.SelectedGuid 'GetSelectedItem(Me.cboDealer_WRITE)
        State.DealerID = selectedDealerId
        If Not selectedDealerId.Equals(Guid.Empty) Then
            Dim defaultDates As Contract.StartEndDates = Contract.GetNewDefaultDates(selectedDealerId)
            With defaultDates
                PopulateControlFromBOProperty(TextboxStartDate_WRITE, New DateType(.StartDate))
                PopulateControlFromBOProperty(TextboxEndDate_WRITE, New DateType(.EndDate))
            End With
            SetID_Validation_DDandAcsel_Prod_Code() 'SetID_Validation_DD()
        End If

    End Sub

    Protected Sub SetDealerCode()
        Dim selectedDealerId As Guid = TheDealerControl.SelectedGuid
        '    Me.State.DealerID = selectedDealerId
        If Not selectedDealerId.Equals(Guid.Empty) Then
            Dim oDealer As New Dealer(selectedDealerId)
            'set the default currency
            If State.MyBO.IsNew Then
                Dim objCompany As New Company(oDealer.CompanyId)
                Dim objCountry As New Country(objCompany.CountryId)
                State.MyBO.CurrencyOfCoveragesId = objCountry.PrimaryCurrencyId
                SetSelectedItem(cboCURRENCY_OF_COVERAGES, objCountry.PrimaryCurrencyId)
                State.Company_Type_ID = objCompany.CompanyTypeId
                State.Company_ID = objCompany.Id
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New Contract
        PopulateDropdowns()
        ClearReplacementPolicyState()
        PopulateFormFromBOs()
        EnableDisableFields()
        ClearDepreciationScheduleState()

    End Sub

    Protected Sub CreateNewWithCopy()

        ClearReplacementPolicyState()
        PopulateBOsFormFrom()
        '# US 489857
        PoupulateBOsFromSourceDropDownBucket()
        cboID_VALIDATION.SelectedIndex = NOTHING_SELECTED
        cboAcselProdCode.SelectedIndex = NOTHING_SELECTED
        SetID_Validation_DDandAcsel_Prod_Code()
        ClearDepreciationScheduleState()
        'create the backup copy
        State.ScreenSnapShotBO = New Contract
        State.ScreenSnapShotBO.Clone(State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Dim confAutoGenResponse As String = HiddenCertificateAuoNumGenConfirmation.Value

        HiddenCertificateAuoNumGenConfirmation.Value = ""
        If confAutoGenResponse IsNot Nothing AndAlso confAutoGenResponse = MSG_VALUE_YES Then
            State.DealerBO.CertificatesAutonumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
            State.DealerBO.CertificatesAutonumberPrefix = TextboxPolicyNumber.Text
            SaveContractRecord(State.MyBO.IsNew)
        End If

        If confAutoGenResponse IsNot Nothing AndAlso confAutoGenResponse = MSG_VALUE_NO Then
            Throw New GUIException(Message.MSG_CERT_AUTO_NUM_GEN_YES_IND_POL, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CERT_AUTO_NUM_GEN_YES_IND_POL)
        End If

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If

    End Sub
    Protected Sub SaveContractRecord(isNewContract As Boolean)
        If State.MyBO.IsDirty Then

            State.MyBO.Save()
            SaveReplacementPolicy(isNewContract) 'new contract, save Child records in memory
            State.HasDataChanged = True

            PopulateFormFromBOs()
            EnableDisableFields(True)
            cboCOINSURANCE_Code.Visible = False
            If State.IsACopy = True Then
                State.IsACopy = False
            End If
            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
        Else
            cboCOINSURANCE_Code.Visible = False
            MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
        End If
    End Sub
    Protected Sub PopulateLineOfBusinessDropDown(PolicyType? As Guid, populateNotInUsedLoB As Boolean)

        Try

            If State.MyBO.DealerId.Equals(Guid.Empty) AndAlso State.DealerBO Is Nothing Then
                Throw New GUIException(Message.MSG_GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND)
                Return
            End If

            ' Get the country Id to get LoB
            If State.MyBO.CountryId.Equals(Guid.Empty) AndAlso State.CountryBO IsNot Nothing Then
                State.MyBO.CountryId = State.CountryBO.Id
            End If

            ' if country BO is not populated then populate contry BO
            If State.MyBO.CountryId.Equals(Guid.Empty) Then
                Dim oCompany As Company = New Company(State.DealerBO.CompanyId)
                State.CountryBO = New Country(oCompany.BusinessCountryId)
                State.MyBO.CountryId = State.CountryBO.Id
            End If

            Dim textFun1 As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                         Return li.Code + " - " + li.Translation
                                                                     End Function
            Dim listcontextLoB As ListContext = New ListContext()

            listcontextLoB.CountryId = State.MyBO.CountryId
            listcontextLoB.PolicyBusinessTypeId = PolicyType

            Dim lobListItems As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PolicyLineOfBusiness", Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextLoB)
            ' Dim inUseLoBListItems As DataElements.ListItem()

            ' Filter to only In Use Line Of Business
            If Not populateNotInUsedLoB Then
                lobListItems = lobListItems.ToList().Where(Function(n) n.ExtendedCode = "Y").ToArray()
            End If

            cboLineOfBusiness.Populate(lobListItems, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = textFun1
                                                  })

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            State.MyBO.DealerId = TheDealerControl.SelectedGuid
            populateDealer()

            'Reload the Dealer on new selection...
            State.DealerBO = Nothing
            State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
            '-------------------------------
            
            'Rebind dropdown when dealer is changed
            BindSourceOptionDropdownlist()            
            PopulateSourcePercentageBucketValues()            
            '--------------------------------------

            SetDefaultDates()
            PopulateAccountBusinessUnit(State.MyBO.DealerId)

            PopulateProducer()
            If (State.MyBO.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(State.MyBO.DealerId)
                If (oDealer.DealerTypeDesc = "VSC") Then
                    With State.MyBO
                        .CoverageDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                        .DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId), Codes.DEDUCTIBLE_BASED_ON__FIXED)
                        ChangeEnabledProperty(cboCovDeductible, False)
                        EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, True, False)
                        ChangeEnabledProperty(cboDeductibleBasedOn, False)

                        PopulateControlFromBOProperty(TextboxDeductible, .Deductible)
                        PopulateControlFromBOProperty(TextboxDeductiblePercent, .DeductiblePercent)
                        SetSelectedItem(cboCovDeductible, .CoverageDeductibleId)
                        SetSelectedItem(cboDeductibleBasedOn, .DeductibleBasedOnId)
                    End With
                Else
                    ChangeEnabledProperty(cboCovDeductible, True)
                    ChangeEnabledProperty(cboDeductibleBasedOn, True)
                End If

                ' Reset Ind. Policy control if dealer is re-selected in between. Move to method later.
                cboLineOfBusiness.Items.Clear()
                SetSelectedItem(cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))

                ' if Dealer is not selected in case of New then disable ind policy dropdowns.
                ControlMgr.SetEnableControl(Me, cboPolicyType, Not State.MyBO.DealerId.Equals(Guid.Empty))
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, Not State.MyBO.DealerId.Equals(Guid.Empty))

                ControlMgr.SetEnableControl(Me, cboLineOfBusiness, True)

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                TextboxPolicyNumber.Text = ""
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
                '------------------------------------------

            Else
                ChangeEnabledProperty(cboCovDeductible, True)
                ChangeEnabledProperty(cboDeductibleBasedOn, True)
            End If

            'Me.BindListControlToDataView(Me.cboCancellationReason, LookupListNew.GetCancellationReasonByDealerIdLookupList(Me.State.MyBO.DealerId), "DESCRIPTION", "ID", True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = State.MyBO.DealerId
            Dim CancellationReasonList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CancellationReasonsByDealer", context:=listcontext)
            cboCancellationReason.Populate(CancellationReasonList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Handlers-DropDown"

    Protected Sub cboCovDeductible_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCovDeductible.SelectedIndexChanged
        Try
            Dim coverageDeductibleId As Guid = Guid.Empty
            Dim deductibleBasedOnId As Guid = Guid.Empty
            If cboCovDeductible.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                coverageDeductibleId = GetSelectedItem(cboCovDeductible)
            End If
            If cboDeductibleBasedOn.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                deductibleBasedOnId = GetSelectedItem(cboDeductibleBasedOn)
            End If
            EnableDisableDeductible(coverageDeductibleId, deductibleBasedOnId, True, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboDeductibleBasedOn_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDeductibleBasedOn.SelectedIndexChanged
        Try
            Dim coverageDeductibleId As Guid = Guid.Empty
            Dim deductibleBasedOnId As Guid = Guid.Empty
            If cboCovDeductible.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                coverageDeductibleId = GetSelectedItem(cboCovDeductible)
            End If
            If cboDeductibleBasedOn.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                deductibleBasedOnId = GetSelectedItem(cboDeductibleBasedOn)
            End If
            EnableDisableDeductible(coverageDeductibleId, deductibleBasedOnId, True, False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCollectionCycleType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCollectionCycleType.SelectedIndexChanged

        EnableDisableCycleDay()

    End Sub
    'REQ-5773 Start
    Protected Sub moPaymentProcessingTypeId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentProcessingTypeId.SelectedIndexChanged

        If ddlPaymentProcessingTypeId.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            If GetSelectedItem(ddlPaymentProcessingTypeId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.THIRD_PARTY_PAYMENT)) Then
                ControlMgr.SetVisibleControl(Me, lblThirdPartyName, True)
                ControlMgr.SetVisibleControl(Me, txtThirdPartyName, True)
                ControlMgr.SetVisibleControl(Me, lblThirdPartyTaxId, True)
                ControlMgr.SetVisibleControl(Me, txtThirdPartyTaxId, True)
            Else
                If GetSelectedItem(ddlPaymentProcessingTypeId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.DEALER_PAYMENT)) Then
                    ControlMgr.SetVisibleControl(Me, lblThirdPartyName, False)
                    ControlMgr.SetVisibleControl(Me, txtThirdPartyName, False)
                    ControlMgr.SetVisibleControl(Me, lblThirdPartyTaxId, False)
                    ControlMgr.SetVisibleControl(Me, txtThirdPartyTaxId, False)
                End If
            End If
        End If
    End Sub
    'REQ-5773 End

    Private Sub EnableDisableCycleDay()

        If cboCollectionCycleType.SelectedValue = LookupListNew.GetIdFromCode(LookupListNew.GetCollectionCycleTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), VARIABLE_CYCLE_TYPE_CODE).ToString Then

            TextboxCycleDay.Text = String.Empty
            TextboxCycleDay.Enabled = False
        Else
            TextboxCycleDay.Enabled = True

        End If

    End Sub
    Private Sub cboEDIT_MFG_TERM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEDIT_MFG_TERM.SelectedIndexChanged
        If cboEDIT_MFG_TERM.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            If GetSelectedItem(cboEDIT_MFG_TERM).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE)) Then
                ControlMgr.SetVisibleControl(Me, LabelOverrideEditMfgTerm, False)
                ControlMgr.SetVisibleControl(Me, cboOverrideEditMfgTerm, False)
                cboOverrideEditMfgTerm.SelectedIndex = NOTHING_SELECTED
            Else
                ControlMgr.SetVisibleControl(Me, LabelOverrideEditMfgTerm, True)
                ControlMgr.SetVisibleControl(Me, cboOverrideEditMfgTerm, True)
            End If
        End If
    End Sub

    Protected Sub cboPolicyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPolicyType.SelectedIndexChanged

        If cboPolicyType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

            Dim colPolicyTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)
            Dim indPolicyTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_INDIVIDUAL)

            If GetSelectedItem(cboPolicyType).Equals(indPolicyTypeId) Then
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE))
                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, True)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, True)
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, False)

                PopulateLineOfBusinessDropDown(indPolicyTypeId, False)
            End If

            If GetSelectedItem(cboPolicyType).Equals(colPolicyTypeId) Then
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, True)
                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
                TextboxPolicyNumber.Text = ""

                PopulateLineOfBusinessDropDown(colPolicyTypeId, False)
            End If

        End If

    End Sub
    Protected Sub cboCollectivePolicyGeneration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCollectivePolicyGeneration.SelectedIndexChanged

        If cboCollectivePolicyGeneration.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

            TextboxPolicyNumber.Text = ""
            ' Auto Generated
            If GetSelectedItem(cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE)) Then

                If cboLineOfBusiness.Items.Count = 0 Then ' for new contract when LoB is not populated in case of default to CP
                    PopulateLineOfBusinessDropDown(GetSelectedItem(cboPolicyType), False)
                End If

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, True)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, True)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, False)
            End If

            ' Manually Entered
            If GetSelectedItem(cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)

                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)

            End If

        End If
    End Sub
    Protected Sub cboLineOfBusiness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLineOfBusiness.SelectedIndexChanged

        If cboLineOfBusiness.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

            ClearLabelError(LabelLineOfBusiness)

            Dim subRamoCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRY_LINE_OF_BUSINESS, GetSelectedItem(cboLineOfBusiness))

            TextboxPolicyNumber.Text = subRamoCode + DateTime.Today.Year.ToString().Substring(2, 2)

            'If it's collective policy then generate on Save with auto generated sequence no.
            If GetSelectedItem(cboPolicyType).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) Then
                TextboxPolicyNumber.Text = "ToBeCreated"
            End If

            ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, False)

        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            '# US 489857
            'PoupulateBOsFromSourceDropDownBucket()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            ''REQ-796
            'Dim sIncomingPremium As String, sCoverageRate As String
            'sIncomingPremium = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboIgnorePremium))
            'sCoverageRate = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboIgnoreCovRate))
            'If sIncomingPremium = YES And sCoverageRate = YES Then
            '    Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            '    Exit Sub
            'End If

            Dim sVal As String, sContractType As String, sCollCycleType As String

            If cboCovDeductible.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboCovDeductible))
                If sVal = NO Then
                    If IsNumeric(TextboxDeductible.Text) AndAlso IsNumeric(TextboxDeductiblePercent.Text) Then
                        If CType(TextboxDeductible.Text, Decimal) > 0 AndAlso CType(TextboxDeductiblePercent.Text, Decimal) > 0 Then
                            'display error
                            ElitaPlusPage.SetLabelError(LabelDeductible)
                            ElitaPlusPage.SetLabelError(LabelDeductiblePercent)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_AMOUNT_ERR)
                        End If
                    End If
                End If
            End If

            If cboFixedEscDurationFlag.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboFixedEscDurationFlag))
                sContractType = LookupListNew.GetCodeFromId(LookupListNew.LK_CONTRACT_TYPES, GetSelectedItem(cboContractType))
                If sContractType = CONTRACT_TYPE_EXTENSION Then
                    If sVal = YES Then
                        ElitaPlusPage.SetLabelError(LabelFixedEscDurationFlag)
                        ElitaPlusPage.SetLabelError(LabelContractType)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.FIXED_ESC_FLAG_CONTRACT_TYPE_ERR)
                    End If
                End If
            End If

            txtCLIPPct.Text = txtCLIPPct.Text.Trim
            txtInsPremFactor.Text = txtInsPremFactor.Text.Trim

            If cboCollectionCycleType.SelectedIndex > BLANK_ITEM_SELECTED Then
                sCollCycleType = LookupListNew.GetCodeFromId(LookupListNew.LK_COLLECTION_CYCLE_TYPE, GetSelectedItem(cboCollectionCycleType))
                If sCollCycleType = COLLECTION_CYCLE_TYPE_FIX Then
                    If Trim(TextboxCycleDay.Text) = String.Empty Then
                        ElitaPlusPage.SetLabelError(LabelCycleDay)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.COLLECTION_CYCLE_DAY_ERR)
                    End If
                End If
            End If

            PopulateBOsFormFrom()
            '# US 489857
            PoupulateBOsFromSourceDropDownBucket()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If State.MyBO.DealerMarkupId.Equals(yesId) Then
                If State.MyBO.IgnoreCoverageAmtId.Equals(yesId) OrElse State.MyBO.IgnoreIncomingPremiumID.Equals(yesId) Then
                    ElitaPlusPage.SetLabelError(LabelIgnoreCovAmt)
                    ElitaPlusPage.SetLabelError(LabelIgnorePremium)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.SET_IGNORE_FLAGS_TO_NO_FOR_DEALERMARKUP_ERR)
                End If
            End If

            If State.MyBO.IgnoreCoverageAmtId.Equals(yesId) AndAlso State.MyBO.IgnoreCoverageAmtId.Equals(State.MyBO.IgnoreIncomingPremiumID) Then
                ElitaPlusPage.SetLabelError(LabelIgnoreCovAmt)
                ElitaPlusPage.SetLabelError(LabelIgnorePremium)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.BOTH_IGNORE_FLAGS_CANNOT_BE_YES_ERR)
            End If

            If cboEDIT_MFG_TERM.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If (Not GetSelectedItem(cboEDIT_MFG_TERM).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE))) AndAlso cboOverrideEditMfgTerm.SelectedIndex <= 0 Then
                    ElitaPlusPage.SetLabelError(LabelOverrideEditMfgTerm)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.OVERRIDE_EDIT_MFG_TERM_REQD_ERR)
                End If
            End If

            ' We need Dealer BO to update Certificate Auto number generation ID and prefix For IP
            If State.DealerBO Is Nothing AndAlso Not State.MyBO.DealerId.Equals(Guid.Empty) Then
                State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
            End If

            ' Existing code
            If State.MyBO.InstallmentPaymentId.Equals(yesId) Then
                If State.DealerBO.DealerTypeDesc = State.DealerBO.DEALER_TYPE_DESC Then

                End If
            Else
                If TextboxDaysToSendLetter.Text.Trim = String.Empty Then
                    TextboxDaysToSendLetter.Text = ZERO_STRING
                End If
            End If


            ''REQ-794
            'The below lines of code is commented for the def1861
            'If Me.State.MyBO.IgnoreIncomingPremiumID.Equals(yesId) And Me.State.MyBO.IgnoreCoverageRateId.Equals(yesId) Then
            '    ElitaPlusPage.SetLabelError(Me.LabelIgnorePremium)
            '    ElitaPlusPage.SetLabelError(Me.LabelIgnoreCovRate)
            '    Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.IGNORE_FLAGS_PREMIUM_COVERAGERATE_CANNOT_BE_YES_ERR, Assurant.ElitaPlus.Common.ErrorCodes.IGNORE_FLAGS_PREMIUM_COVERAGERATE_CANNOT_BE_YES_ERR)
            'End If

            If CType(TextboxEndDate_WRITE.Text, Date) < Date.Today Then
                Throw New GUIException(Message.MSG_END_DATE_CANNOT_BE_PAST_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_END_DATE_ERR)
            End If

            ' if individual and autogenrated and dealer certificate auto number is Set to NO then alert user.
            If GetSelectedItem(cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE)) Then

                If GetSelectedItem(cboPolicyType).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_INDIVIDUAL)) Then

                    If State.DealerBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
                        DisplayMessage(Message.MSG_CERT_AUTO_NUM_GEN_IS_NO, "Certificate Auto Generate Prefix", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenCertificateAuoNumGenConfirmation)
                        Return

                    ElseIf State.DealerBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                        State.DealerBO.CertificatesAutonumberPrefix = TextboxPolicyNumber.Text
                    End If

                End If

            End If

            SaveContractRecord(State.MyBO.IsNew) ' Save contract and other child records.

        Catch ex As Exception
            If cboCovDeductible.SelectedItem.Text = NO Then
                TextboxDeductible.Enabled = True
                TextboxDeductiblePercent.Enabled = True
            End If

            '#US 489857
            SetSourceBucketValues()

            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Contract(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Contract
            End If
            PopulateDropdowns()
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DeleteReplacementPolicy() 'delete Child records first
            DeleteDepreciationSchedule()
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            '# US 489857
            'PoupulateBOsFromSourceDropDownBucket()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            ' this should happen during post back before dialog message whether record is dirty or not.
            PopulateBOsFormFrom()
            '# US 489857
            PoupulateBOsFromSourceDropDownBucket()
            TheDealerControl.NothingSelected = True
            populateDealer()
            TheDealerControl.SelectedGuid = State.MyBO.DealerId
            SetDealerCode()

            If Not State.MyBO.IsDirty Then
                State.MyBO = New Contract
                State.IsACopy = True
                State.MyBO.DealerId = TheDealerControl.SelectedGuid
                EnableDisableFields()
                CreateNewWithCopy()
            Else
                State.ScreenSnapShotBO = New Contract()
                State.ScreenSnapShotBO.Clone(State.MyBO) ' cloning original BO to save later if user say yes.
                State.MyBO = New Contract
                State.IsACopy = True
                State.MyBO.DealerId = TheDealerControl.SelectedGuid
                EnableDisableFields()
                State.MyBO.Clone(State.ScreenSnapShotBO)

                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End If

            ' recapture the Dealer BO after new cloned contract BO.
            If Not State.MyBO.DealerId.Equals(Guid.Empty) Then
                State.DealerBO = State.MyBO.AddDealer(State.MyBO.DealerId)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnTNC_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnTNC.Click
        Try
            callPage(Tables.TermAndConditionsForm.URL, State.MyBO.Id)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Control Events"

    Private Sub cboInstallmentPayment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboInstallmentPayment.SelectedIndexChanged
        Try
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If State.MyBO.DealerId.Equals(Guid.Empty) Then
                SetSelectedItem(cboInstallmentPayment, noId)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Exit Sub
            End If
            If cboInstallmentPayment.SelectedValue = YesId.ToString Then
                TextboxDaysOfFirstPymt.Text = ""
            End If
            EnableFirstPaymentMonthsField()
            ShowInstPymtFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboRecurringPremium_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboRecurringPremium.SelectedIndexChanged
        Try
            EnableDisableFields(True)
            'Req-1016 Start
            Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, GetSelectedItem(cboRecurringPremium))

            If sVal = Codes.PERIOD_RENEW__SINGLE_PREMIUM OrElse sVal = "" Then
                txtPeridiocBillingWarntyPeriod.Text = Nothing
            End If
            'Req-1016 End
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboIncludeFirstPmt_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboIncludeFirstPmt.SelectedIndexChanged
        EnableFirstPaymentMonthsField()
    End Sub

    Private Sub cboDealerMarkup_WRITE_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDealerMarkup_WRITE.SelectedIndexChanged
        Try
            EnableDisableFields(True)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
            If GetSelectedItem(cboDealerMarkup_WRITE).Equals(yesId) AndAlso GetSelectedItem(cboCURRENCY_CONVERSION).Equals(yesId) Then
                cboCURRENCY_CONVERSION.SelectedIndex = NOTHING_SELECTED
                DisplayMessage(Message.MSG_DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub cboCURRENCY_CONVERSION_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCURRENCY_CONVERSION.SelectedIndexChanged
        Try
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
            If GetSelectedItem(cboCURRENCY_CONVERSION).Equals(yesId) AndAlso GetSelectedItem(cboDealerMarkup_WRITE).Equals(yesId) Then
                cboDealerMarkup_WRITE.SelectedIndex = NOTHING_SELECTED
                cboRestrictMarkup_WRITE.SelectedIndex = NOTHING_SELECTED
                ddlAllowCoverageMarkupDistribution.SelectedIndex = NOTHING_SELECTED
                DisplayMessage(Message.MSG_DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CheckCoinsuranceDropDown()
        If cboCOINSURANCE.SelectedIndex > 0 AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COINSURANCE, GetSelectedItem(cboCOINSURANCE)).Equals(DIRECT) Then
            TextboxPARTICIPATION_PERCENT.Text = HiddenPARTICIPATION_PERCENTAG.Value
            TextboxPARTICIPATION_PERCENT.Enabled = False
        Else
            TextboxPARTICIPATION_PERCENT.Enabled = True
        End If
    End Sub
#End Region

#Region "Error Handling"

#End Region

#Region "Handlers - Replacement Policy Tab"
    Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 0
    Public Const GRID_COL_COVERATGE_TYPE_IDX As Integer = 1
    Public Const GRID_COL_CERT_DURATION_IDX As Integer = 2
    Public Const GRID_COL_CLAIM_COUNT_IDX As Integer = 3

    Public Const RepPolicy_None As Integer = 0
    Public Const RepPolicy_Add As Integer = 1
    Public Const RepPolicy_Edit As Integer = 2
    Public Const RepPolicy_Delete As Integer = 3

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim nIndex As Integer
        Dim guidTemp As Guid
        Try
            If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                'guidTemp = New Guid(CType(moGridView.Rows(nIndex).FindControl("hiddenRepPolicyID"), HiddenField).Value)
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.RepPolicyList.FindIndex(Function(r) r.Id = guidTemp)
                State.RepPolicyWorkingItem = State.RepPolicyList.Item(nIndex)
            End If

            If e.CommandName = EDIT_COMMAND_NAME Then
                moGridView.EditIndex = nIndex
                moGridView.SelectedIndex = nIndex
                State.RepPolicyAction = RepPolicy_Edit
                PopulateReplacementPolicyGrid(State.RepPolicyList)
                'PopulateCoverageRateList(ACTION_EDIT)
                'PopulateCoverageRate()
                'Me.SetGridControls(moGridView, False)
                SetFocusInGrid(moGridView, nIndex, GRID_COL_CLAIM_COUNT_IDX)
                EnableDisableBtnsForRepPolicyGrid()
                'setbuttons(False)
            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                State.RepPolicyAction = RepPolicy_Delete
                ReplacementPolicyDeleteRecord()
            ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                ReplacementPolicySaveRecord()
            ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                ReplacementPolicyCancelRecord()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridView.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As ReppolicyClaimCount


            If e.Row.DataItem IsNot Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, ReppolicyClaimCount)
                    'edit item, populate dropdown and set value
                    If (State.RepPolicyAction = RepPolicy_Add OrElse State.RepPolicyAction = RepPolicy_Edit) AndAlso State.RepPolicyWorkingItem.Id = dvRow.Id Then
                        If dvRow.ReplacementPolicyClaimCount IsNot Nothing Then
                            CType(e.Row.Cells(GRID_COL_CLAIM_COUNT_IDX).FindControl("txtClaimCount"), TextBox).Text = dvRow.ReplacementPolicyClaimCount.Value.ToString
                        End If

                        Dim objDDL As DropDownList
                        'set product code 
                        objDDL = CType(e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl("ddlProductCode"), DropDownList)

                        'Dim dv As DataView = ProductCode.getListByDealer(State.MyBO.DealerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Guid.Empty)
                        'BindListTextToDataView(objDDL, dv, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, True, False)

                        Dim plistcontext As ListContext = New ListContext()
                        plistcontext.DealerId = State.MyBO.DealerId
                        Dim ProductCodeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", context:=plistcontext)
                        objDDL.Populate(ProductCodeList, New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .BlankItemValue = "0",
                                .TextFunc = AddressOf .GetCode,
                                .ValueFunc = AddressOf .GetCode,
                                .SortFunc = AddressOf .GetCode
                            })

                        If dvRow.ProductCode IsNot Nothing Then
                            SetSelectedItem(objDDL, dvRow.ProductCode)
                        End If

                        'set coverage type
                        objDDL = CType(e.Row.Cells(GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
                        ' dv = ReppolicyClaimCount.GetCoverageTypeListByDealer(State.MyBO.DealerId)
                        'BindListControlToDataView(objDDL, dv)

                        Dim listcontext As ListContext = New ListContext()
                        listcontext.DealerId = State.MyBO.DealerId
                        listcontext.LanguageId = Authentication.CurrentUser.LanguageId

                        objDDL.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

                        If Not dvRow.ConverageTypeId = Guid.Empty Then
                            SetSelectedItem(objDDL, dvRow.ConverageTypeId.ToString)
                        End If

                        'set certificate duration
                        objDDL = CType(e.Row.Cells(GRID_COL_CERT_DURATION_IDX).FindControl("ddlCertDuration"), DropDownList)
                        'dv = ReppolicyClaimCount.GetAvailCertDurationByDealer(State.MyBO.DealerId)
                        'BindListTextToDataView(objDDL, dv, "CERTIFICATE_DURATION", "CERTIFICATE_DURATION")

                        Dim listcontext1 As ListContext = New ListContext()
                        listcontext1.DealerId = State.MyBO.DealerId
                        objDDL.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="AvailableCertDurationByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext1), New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .BlankItemValue = "0",
                                .TextFunc = AddressOf .GetDescription,
                                .ValueFunc = AddressOf .GetDescription
                            })

                        If dvRow.CertDuration IsNot Nothing Then
                            SetSelectedItem(objDDL, dvRow.CertDuration.Value.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub PopulateReplacementPolicyGrid(ds As Collections.Generic.List(Of ReppolicyClaimCount))
        Dim blnEmptyList As Boolean = False, mySource As Collections.Generic.List(Of ReppolicyClaimCount)
        If ds Is Nothing OrElse ds.Count = 0 Then
            mySource = New Collections.Generic.List(Of ReppolicyClaimCount)
            mySource.Add(New ReppolicyClaimCount())
            blnEmptyList = True
            moGridView.DataSource = mySource
        Else
            moGridView.DataSource = ds
        End If

        moGridView.DataBind()

        If blnEmptyList Then
            moGridView.Rows(0).Visible = False
        End If
    End Sub

    Private Sub BtnNewReplacementPolicy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNewReplacementPolicy_WRITE.Click
        Try
            State.RepPolicyAction = RepPolicy_Add
            'Me.State.RepPolicyList = Nothing
            Dim objNew As New ReppolicyClaimCount()
            objNew.ContractId = State.MyBO.Id
            State.RepPolicyWorkingItem = objNew

            If State.RepPolicyList Is Nothing Then
                State.RepPolicyList = New Collections.Generic.List(Of ReppolicyClaimCount)
            End If
            State.RepPolicyList.Add(objNew)

            moGridView.SelectedIndex = State.RepPolicyList.Count - 1
            moGridView.EditIndex = moGridView.SelectedIndex
            PopulateReplacementPolicyGrid(State.RepPolicyList)

            EnableDisableBtnsForRepPolicyGrid()
            SetFocusInGrid(moGridView, moGridView.SelectedIndex, GRID_COL_CLAIM_COUNT_IDX)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ReplacementPolicyCancelRecord()
        Try
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            If State.RepPolicyAction = RepPolicy_Add Then
                State.RepPolicyList.Remove(State.RepPolicyWorkingItem)
            ElseIf State.RepPolicyAction = RepPolicy_Edit AndAlso (State.RepPolicyWorkingOrig IsNot Nothing) Then ' set the object to original status
                CopyReplacementPolicyObject(State.RepPolicyWorkingOrig, State.RepPolicyWorkingItem)
            End If

            State.RepPolicyAction = RepPolicy_None
            State.RepPolicyWorkingItem = Nothing

            PopulateReplacementPolicyGrid(State.RepPolicyList)
            EnableDisableBtnsForRepPolicyGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Function NewReplacementPolicyValid(obj As ReppolicyClaimCount) As Boolean
        Dim blnValid As Boolean = True

        If Not obj.OneAndOnlyOneConfigCriteriaHasValue Then
            blnValid = False
            MasterPage.MessageController.AddError("REPLACEMENT_POLICY_CONFIG_CRITERIA_INVALID")
        End If

        If obj.DuplicateExists(State.RepPolicyList) Then
            blnValid = False
            MasterPage.MessageController.AddError("REPLACEMENT_POLICY_DUPLICATE_CONFIG")
        End If

        Return blnValid

    End Function

    Private Sub CopyReplacementPolicyObject(objSource As ReppolicyClaimCount, objDest As ReppolicyClaimCount)
        With objSource
            objDest.ContractId = .ContractId
            objDest.ProductCode = .ProductCode
            objDest.ConverageTypeId = .ConverageTypeId
            objDest.CertDuration = .CertDuration
            objDest.ReplacementPolicyClaimCount = .ReplacementPolicyClaimCount
        End With
    End Sub

    Private Sub ReplacementPolicySaveRecord()
        Try

            Dim objDDL As DropDownList, objTxt As TextBox
            Dim strTemp As String, intTemp As Integer, boolHasErr As Boolean = False

            If State.RepPolicyAction = RepPolicy_Edit Then 'save the original value
                State.RepPolicyWorkingOrig = New ReppolicyClaimCount
                CopyReplacementPolicyObject(State.RepPolicyWorkingItem, State.RepPolicyWorkingOrig)
            End If

            objDDL = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_PRODUCT_CODE_IDX).FindControl("ddlProductCode"), DropDownList)
            PopulateBOProperty(State.RepPolicyWorkingItem, "ProductCode", objDDL.SelectedItem.Text)
            objDDL = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
            PopulateBOProperty(State.RepPolicyWorkingItem, "ConverageTypeId", objDDL)
            objDDL = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_CERT_DURATION_IDX).FindControl("ddlCertDuration"), DropDownList)
            PopulateBOProperty(State.RepPolicyWorkingItem, "CertDuration", objDDL.SelectedItem.Text)
            objTxt = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_CLAIM_COUNT_IDX).FindControl("txtClaimCount"), TextBox)

            strTemp = objTxt.Text.Trim
            If strTemp = String.Empty Then
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("REPLACEMENT_POLICY_CLAIM_COUNT") + ":" + TranslationBase.TranslateLabelOrMessage("MSG_VALUE_MANDATORY_ERR"), False)
                boolHasErr = True
            End If
            If Integer.TryParse(strTemp, intTemp) Then
                If intTemp < 1 OrElse intTemp > 99 Then
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("REPLACEMENT_POLICY_CLAIM_COUNT") + ":" + TranslationBase.TranslateLabelOrMessage("MSG_VALID_NUMERIC_RANGE_ERR"), False)
                    boolHasErr = True
                Else
                    PopulateBOProperty(State.RepPolicyWorkingItem, "ReplacementPolicyClaimCount", strTemp)
                End If

            Else
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("REPLACEMENT_POLICY_CLAIM_COUNT") + ":" + TranslationBase.TranslateLabelOrMessage("ERROR POPULATING AS BUSINESS PROPERTY"), False)
                boolHasErr = True
            End If

            If boolHasErr Then
                MasterPage.MessageController.Show()
                Exit Sub
            End If

            If State.RepPolicyWorkingItem.IsDirty Then 'Save the changes
                If State.MyBO.IsNew = False AndAlso State.RepPolicyWorkingItem.IsDirty Then 'existing contract, save to DB directly
                    State.RepPolicyWorkingItem.SaveWithoutCheckDSCreator()
                    'reload the list
                    State.RepPolicyList = Nothing
                    State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(State.MyBO.Id)
                Else 'new contract, keep the record in memory after validation and save it with new contract
                    If NewReplacementPolicyValid(State.RepPolicyWorkingItem) Then
                        Dim objInd As Integer = State.RepPolicyList.FindIndex(Function(r) r.Id = State.RepPolicyWorkingItem.Id)
                        State.RepPolicyList.Item(objInd) = State.RepPolicyWorkingItem
                    Else 'Validation error, exit and show errors
                        Exit Sub
                    End If
                End If
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If


            State.RepPolicyAction = RepPolicy_None
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            State.RepPolicyWorkingItem = Nothing
            PopulateReplacementPolicyGrid(State.RepPolicyList)
            EnableDisableBtnsForRepPolicyGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ReplacementPolicyDeleteRecord()
        'Dim objInd As Integer = State.RepPolicyList.FindIndex(Function(r) r.Id = State.RepPolicyWorkingItem.Id)

        If Not State.RepPolicyWorkingItem.IsNew Then
            'if not new object, delete from database
            State.RepPolicyWorkingItem.Delete()
            State.RepPolicyWorkingItem.SaveWithoutCheckDSCreator()
        End If
        'remove from list
        State.RepPolicyList.Remove(State.RepPolicyWorkingItem)

        State.RepPolicyAction = RepPolicy_None
        moGridView.SelectedIndex = -1
        moGridView.EditIndex = moGridView.SelectedIndex

        State.RepPolicyWorkingItem = Nothing
        PopulateReplacementPolicyGrid(State.RepPolicyList)
        EnableDisableBtnsForRepPolicyGrid()
        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
    End Sub

    Private Sub EnableDisableBtnsForRepPolicyGrid()
        If State.RepPolicyAction = RepPolicy_None Then 'enable buttons on main form
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnTNC, True)
            ControlMgr.SetEnableControl(Me, BtnNewReplacementPolicy_WRITE, True)
            EnableDisableFields()
        Else 'disable buttons on main form for RepPolicy grid editing
            ControlMgr.SetEnableControl(Me, btnBack, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnTNC, False)
            ControlMgr.SetEnableControl(Me, BtnNewReplacementPolicy_WRITE, False)
        End If

    End Sub

    Private Sub DeleteReplacementPolicy()
        State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(State.MyBO.Id)
        If State.RepPolicyList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.RepPolicyList.Count - 1
                State.RepPolicyList.Item(i).Delete()
                State.RepPolicyList.Item(i).SaveWithoutCheckDSCreator()
            Next
        End If
        State.RepPolicyList = Nothing
    End Sub
    Private Sub SaveReplacementPolicy(blnNewBO As Boolean)
        If blnNewBO Then
            If State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) OrElse
                State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then
                ' new BO, save the replacement policy records in memory
                If (State.RepPolicyList IsNot Nothing) AndAlso State.RepPolicyList.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To State.RepPolicyList.Count - 1
                        State.RepPolicyList.Item(i).SaveWithoutCheckDSCreator()
                    Next
                    State.RepPolicyList = Nothing
                End If
            End If
        Else 'existing BO, delete replacement policy records in not CNCL and CNCLAF
            If (Not State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL))) AndAlso
                (Not State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF))) Then
                DeleteReplacementPolicy()
            End If
        End If

    End Sub

    Private Sub ClearReplacementPolicyState()
        With State
            .RepPolicyAction = RepPolicy_None
            .RepPolicyList = Nothing
            .RepPolicyWorkingItem = Nothing
            .RepPolicyWorkingOrig = Nothing
            PopulateReplacementPolicyGrid(.RepPolicyList)
        End With

    End Sub
#End Region
#Region "Handlers - Depreciation Schedule Tab"
    Public Const DepSchGridColCode As Integer = 0
    Public Const DepSchGridColEffective As Integer = 1
    Public Const DepSchGridColExpiration As Integer = 2
    Public Const DepSchGridColDepSchUsage As Integer = 3
    Public Const DepSchGridColEditCancelBtn As Integer = 4
    Public Const DepSchGridColDeleteSaveBtn As Integer = 5

    Public Const DepreciationScheduleNone As Integer = 0
    Public Const DepreciationScheduleAdd As Integer = 1
    Public Const DepreciationScheduleEdit As Integer = 2
    Public Const DepreciationScheduleDelete As Integer = 3
    Public Const DepreciationScheduleUsageDefault As String = Codes.DEPRECIATION_SCHEDULE_USAGE__LIABILITY_LIMIT

#Region "Depreciation Schedule Grid Operation"

    Public Sub GridViewDepreciationSchedule_RowCreated(sender As System.Object, e As GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub GridViewDepreciationSchedule_RowCommand(source As Object, e As GridViewCommandEventArgs)
        Dim nIndex As Integer
        Dim guidTemp As Guid
        Try
            If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.DepreciationScheduleList.FindIndex(Function(r) r.Id = guidTemp)
                State.DepreciationScheduleWorkingItem = State.DepreciationScheduleList.Item(nIndex)
            End If

            If e.CommandName = EDIT_COMMAND_NAME Then
                GridViewDepreciationSchedule.EditIndex = nIndex
                GridViewDepreciationSchedule.SelectedIndex = nIndex
                State.DepreciationScheduleAction = DepreciationScheduleEdit
                PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)
                SetFocusInGrid(GridViewDepreciationSchedule, nIndex, DepSchGridColExpiration)
                EnableDisableBtnsForDepreciationScheduleGrid()
            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                State.DepreciationScheduleAction = DepreciationScheduleDelete
                DepreciationScheduleDeleteRecord()
            ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                DepreciationScheduleSaveRecord()
            ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                DepreciationScheduleCancelRecord()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewDepreciationSchedule_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewDepreciationSchedule.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DepreciationScdRelation


            If e.Row.DataItem IsNot Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, DepreciationScdRelation)


                    'edit item, populate dropdown and set value
                    If (State.DepreciationScheduleAction = DepreciationScheduleAdd OrElse State.DepreciationScheduleAction = DepreciationScheduleEdit) AndAlso State.DepreciationScheduleWorkingItem.Id = dvRow.Id Then

                        'Dim dv As DataView = DepreciationScd.LoadList(State.Company_ID)
                        'dv.RowFilter = " (active_xcd = 'YESNO-Y' Or code = '" & dvRow.DepreciationScheduleCode & "')"
                        'BindListControlToDataView(CType(e.Row.Cells(DepSchGridColCode).FindControl("ddlDepreciationSchedule"), DropDownList), dv, "code", "depreciation_schedule_id")

                        Dim listcontext As ListContext = New ListContext()
                        listcontext.CompanyId = State.Company_ID
                        Dim DepreciationScheduleList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DepreciationScheduleByCompany", context:=listcontext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                        Dim FilteredDepreciationScheduleList As DataElements.ListItem() = (From lst In DepreciationScheduleList
                                                                                           Where lst.ExtendedCode = "YESNO-Y" OrElse lst.Code = dvRow.DepreciationScheduleCode
                                                                                           Select lst).ToArray()

                        CType(e.Row.Cells(DepSchGridColCode).FindControl("ddlDepreciationSchedule"), DropDownList).Populate(FilteredDepreciationScheduleList.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True,
                                        .TextFunc = AddressOf .GetCode,
                                        .SortFunc = AddressOf .GetCode
                                    })

                        If Not dvRow.DepreciationScheduleId = Guid.Empty Then
                            SetSelectedItem(CType(e.Row.Cells(DepSchGridColCode).FindControl("ddlDepreciationSchedule"), DropDownList), dvRow.DepreciationScheduleId)
                        End If

                        Dim btnEffectiveDate As ImageButton
                        Dim txtEffectiveDate As TextBox
                        btnEffectiveDate = CType(e.Row.Cells(DepSchGridColEffective).FindControl("btnEffectiveDate"), ImageButton)
                        txtEffectiveDate = CType(e.Row.Cells(DepSchGridColEffective).FindControl("txtEffectiveDate"), TextBox)
                        AddCalendar_New(btnEffectiveDate, txtEffectiveDate)
                        If dvRow.EffectiveDate IsNot Nothing Then
                            CType(e.Row.Cells(DepSchGridColEffective).FindControl("txtEffectiveDate"), TextBox).Text = GetDateFormattedStringNullable(dvRow.EffectiveDate.Value)
                        End If

                        Dim btnExpirationDate As ImageButton
                        Dim txtExpirationDate As TextBox
                        btnExpirationDate = CType(e.Row.Cells(DepSchGridColExpiration).FindControl("btnExpirationDate"), ImageButton)
                        txtExpirationDate = CType(e.Row.Cells(DepSchGridColExpiration).FindControl("txtExpirationDate"), TextBox)
                        AddCalendar_New(btnExpirationDate, txtExpirationDate)
                        If dvRow.ExpirationDate IsNot Nothing Then
                            CType(e.Row.Cells(DepSchGridColExpiration).FindControl("txtExpirationDate"), TextBox).Text = GetDateFormattedStringNullable(dvRow.ExpirationDate.Value)
                        End If

                        Dim oDDlDepreciationScheduleUsage As DropDownList = CType(e.Row.Cells(DepSchGridColDepSchUsage).FindControl("ddlDepreciationScheduleUsage"), DropDownList)
                        'oDDlDepreciationScheduleUsage.PopulateOld(LookupListNew.LK_DEPRECIATION_SCHEDULE_USAGE, ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                        oDDlDepreciationScheduleUsage.Populate(CommonConfigManager.Current.ListManager.GetList("DEP_SCH_USAGE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                            {
                                .TextFunc = AddressOf .GetDescription,
                                .ValueFunc = AddressOf .GetExtendedCode,
                                .SortFunc = AddressOf .GetDescription
                            })

                        If dvRow.DepreciationScheduleUsageXcd IsNot Nothing AndAlso Not String.IsNullOrEmpty(dvRow.DepreciationScheduleUsageXcd.ToString()) Then
                            SetSelectedItem(oDDlDepreciationScheduleUsage, dvRow.DepreciationScheduleUsageXcd)
                        Else
                            SetSelectedItem(oDDlDepreciationScheduleUsage, DepreciationScheduleUsageDefault)
                        End If

                        ' Do not allow edit
                        If State.DepreciationScheduleAction = DepreciationScheduleEdit Then
                            CType(e.Row.Cells(DepSchGridColCode).FindControl("ddlDepreciationSchedule"), DropDownList).Enabled = False
                            btnEffectiveDate.Visible = False
                            txtEffectiveDate.Enabled = False
                            oDDlDepreciationScheduleUsage.Enabled = False
                        End If
                    Else
                        If dvRow.EffectiveDate IsNot Nothing Then
                            Dim lblEffectiveDate As Label
                            lblEffectiveDate = CType(e.Row.Cells(DepSchGridColEffective).FindControl("lblEffectiveDate"), Label)
                            lblEffectiveDate.Text = GetDateFormattedStringNullable(CType(dvRow.EffectiveDate, Date))
                        End If
                        If dvRow.ExpirationDate IsNot Nothing Then
                            Dim lblExpirationDate As Label
                            lblExpirationDate = CType(e.Row.Cells(DepSchGridColExpiration).FindControl("lblExpirationDate"), Label)
                            lblExpirationDate.Text = GetDateFormattedStringNullable(CType(dvRow.ExpirationDate, Date))
                        End If
                        If Not dvRow.IsDeleteAllowed() Then
                            Dim btnDeleteButton As ImageButton
                            btnDeleteButton = CType(e.Row.Cells(DepSchGridColDeleteSaveBtn).FindControl("DeleteButton_WRITE"), ImageButton)
                            btnDeleteButton.Visible = False
                        End If

                        If Not dvRow.IsExpirationDateEditable() Then
                            Dim btnEditButton As ImageButton
                            btnEditButton = CType(e.Row.Cells(DepSchGridColEditCancelBtn).FindControl("EditButton_WRITE"), ImageButton)
                            btnEditButton.Visible = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region
#Region "Depreciation Schedule - Others"
    Private Sub PopulateDepreciationScheduleGrid(ds As Collections.Generic.List(Of DepreciationScdRelation))

        Dim blnEmptyList As Boolean = False, mySource As Collections.Generic.List(Of DepreciationScdRelation)
        If ds Is Nothing OrElse ds.Count = 0 Then
            mySource = New Collections.Generic.List(Of DepreciationScdRelation)
            mySource.Add(New DepreciationScdRelation())
            blnEmptyList = True
            GridViewDepreciationSchedule.DataSource = mySource
        Else
            GridViewDepreciationSchedule.DataSource = ds
        End If

        GridViewDepreciationSchedule.DataBind()

        If blnEmptyList Then
            GridViewDepreciationSchedule.Rows(0).Visible = False
        End If
    End Sub
    Protected Sub BindBoPropertiesToDepreciationScheduleGridHeaders()
        BindBOPropertyToGridHeader(State.DepreciationScheduleWorkingItem, "DepreciationScheduleId", GridViewDepreciationSchedule.Columns(DepSchGridColCode))
        BindBOPropertyToGridHeader(State.DepreciationScheduleWorkingItem, "EffectiveDate", GridViewDepreciationSchedule.Columns(DepSchGridColEffective))
        BindBOPropertyToGridHeader(State.DepreciationScheduleWorkingItem, "ExpirationDate", GridViewDepreciationSchedule.Columns(DepSchGridColExpiration))
        BindBOPropertyToGridHeader(State.DepreciationScheduleWorkingItem, "DepreciationScheduleUsageXcd", GridViewDepreciationSchedule.Columns(DepSchGridColDepSchUsage))
    End Sub
    Private Sub DepreciationScheduleCancelRecord()
        Try
            GridViewDepreciationSchedule.SelectedIndex = -1
            GridViewDepreciationSchedule.EditIndex = GridViewDepreciationSchedule.SelectedIndex

            If State.DepreciationScheduleAction = DepreciationScheduleAdd Then
                State.DepreciationScheduleList.Remove(State.DepreciationScheduleWorkingItem)
            ElseIf State.DepreciationScheduleAction = DepreciationScheduleEdit AndAlso (State.DepreciationScheduleOrig IsNot Nothing) Then ' set the object to original status
                CopyDepreciationScheduleObject(State.DepreciationScheduleOrig, State.DepreciationScheduleWorkingItem)
            End If

            State.DepreciationScheduleAction = DepreciationScheduleNone
            State.DepreciationScheduleWorkingItem = Nothing

            EnableDisableBtnsForDepreciationScheduleGrid()
            PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub CopyDepreciationScheduleObject(objSource As DepreciationScdRelation, objDest As DepreciationScdRelation)
        With objSource
            objDest.DepreciationScheduleId = .DepreciationScheduleId
            objDest.DepreciationScheduleCode = .DepreciationScheduleCode
            objDest.EffectiveDate = .EffectiveDate
            objDest.ExpirationDate = .ExpirationDate
            objDest.TableReferenceId = .TableReferenceId
            objDest.TableReference = .TableReference
            objDest.DepreciationScheduleUsageXcd = .DepreciationScheduleUsageXcd
            objDest.DepreciationScheduleUsage = .DepreciationScheduleUsage
        End With
    End Sub
    Private Sub DepreciationScheduleSaveRecord()
        Try
            Dim objDDl As DropDownList, objTxt As TextBox

            objDDl = CType(GridViewDepreciationSchedule.Rows(GridViewDepreciationSchedule.EditIndex).Cells(DepSchGridColCode).FindControl("ddlDepreciationSchedule"), DropDownList)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "DepreciationScheduleId", objDDl)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "DepreciationScheduleCode", objDDl, False)

            objTxt = CType(GridViewDepreciationSchedule.Rows(GridViewDepreciationSchedule.EditIndex).Cells(DepSchGridColEffective).FindControl("txtEffectiveDate"), TextBox)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "EffectiveDate", objTxt.Text)


            objTxt = CType(GridViewDepreciationSchedule.Rows(GridViewDepreciationSchedule.EditIndex).Cells(DepSchGridColExpiration).FindControl("txtExpirationDate"), TextBox)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "ExpirationDate", objTxt.Text)

            objDDl = CType(GridViewDepreciationSchedule.Rows(GridViewDepreciationSchedule.EditIndex).Cells(DepSchGridColDepSchUsage).FindControl("ddlDepreciationScheduleUsage"), DropDownList)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "DepreciationScheduleUsageXcd", objDDl, False, True)
            PopulateBOProperty(State.DepreciationScheduleWorkingItem, "DepreciationScheduleUsage", objDDl, False)

            If State.DepreciationScheduleWorkingItem.IsDirty AndAlso State.MyBO.IsNew = False Then 'Save the changes
                'existing contract, save to DB directly
                State.DepreciationScheduleWorkingItem.SaveWithoutCheckDsCreator()
                'reload the list
                State.DepreciationScheduleList = Nothing
                State.DepreciationScheduleList = DepreciationScdRelation.GetDepreciationScheduleList(State.MyBO.Id)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If

            State.DepreciationScheduleAction = DepreciationScheduleNone
            GridViewDepreciationSchedule.SelectedIndex = -1
            GridViewDepreciationSchedule.EditIndex = GridViewDepreciationSchedule.SelectedIndex

            State.DepreciationScheduleWorkingItem = Nothing
            EnableDisableBtnsForDepreciationScheduleGrid()
            PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)

        Catch ex As Exception
            HandleErrors(ex, ErrorControllerDS)
        End Try
    End Sub

    Private Sub DepreciationScheduleDeleteRecord()
        If Not State.DepreciationScheduleWorkingItem.IsNew Then
            'if not new object, delete from database
            State.DepreciationScheduleWorkingItem.Delete()
            State.DepreciationScheduleWorkingItem.SaveWithoutCheckDsCreator()
        End If
        'remove from list
        State.DepreciationScheduleList.Remove(State.DepreciationScheduleWorkingItem)

        State.DepreciationScheduleAction = DepreciationScheduleNone
        GridViewDepreciationSchedule.SelectedIndex = -1
        GridViewDepreciationSchedule.EditIndex = GridViewDepreciationSchedule.SelectedIndex

        State.DepreciationScheduleWorkingItem = Nothing
        EnableDisableBtnsForDepreciationScheduleGrid()
        PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)

        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
    End Sub
    Private Sub EnableDisableBtnsForDepreciationScheduleGrid()
        If State.DepreciationScheduleAction = DepreciationScheduleNone Then 'enable buttons on main form
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnTNC, True)
            ControlMgr.SetEnableControl(Me, BtnNewDepreciationSchedule, True)
            SetGridControls(GridViewDepreciationSchedule, True)
            EnableDisableFields()
        Else 'disable buttons on main form 
            ControlMgr.SetEnableControl(Me, btnBack, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnTNC, False)
            ControlMgr.SetEnableControl(Me, BtnNewDepreciationSchedule, False)
            SetGridControls(GridViewDepreciationSchedule, False)
        End If

    End Sub
    Private Sub DeleteDepreciationSchedule()
        State.DepreciationScheduleList = DepreciationScdRelation.GetDepreciationScheduleList(State.MyBO.Id)
        If State.DepreciationScheduleList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.DepreciationScheduleList.Count - 1
                State.DepreciationScheduleList.Item(i).Delete()
                State.DepreciationScheduleList.Item(i).SaveWithoutCheckDsCreator()
            Next
        End If
        State.DepreciationScheduleList = Nothing
    End Sub
    Private Sub ClearDepreciationScheduleState()
        With State
            .DepreciationScheduleAction = DepreciationScheduleNone
            .DepreciationScheduleList = Nothing
            .DepreciationScheduleWorkingItem = Nothing
            .DepreciationScheduleOrig = Nothing
            PopulateDepreciationScheduleGrid(.DepreciationScheduleList)
        End With

    End Sub
#End Region
#Region "Depreciation Schedule - Button Click"
    Private Sub BtnNewDepreciationSchedule_Click(sender As System.Object, e As EventArgs) Handles BtnNewDepreciationSchedule.Click
        Try
            State.DepreciationScheduleAction = DepreciationScheduleAdd
            Dim objNew As New DepreciationScdRelation()
            objNew.TableReferenceId = State.MyBO.Id
            objNew.TableReference = DepreciationScdRelation.ContractTableName
            State.DepreciationScheduleWorkingItem = objNew

            If State.DepreciationScheduleList Is Nothing Then
                State.DepreciationScheduleList = New Collections.Generic.List(Of DepreciationScdRelation)
            End If
            State.DepreciationScheduleList.Add(objNew)

            GridViewDepreciationSchedule.SelectedIndex = State.DepreciationScheduleList.Count - 1
            GridViewDepreciationSchedule.EditIndex = GridViewDepreciationSchedule.SelectedIndex
            PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)

            EnableDisableBtnsForDepreciationScheduleGrid()
            GetSelectedGridDropItem(GridViewDepreciationSchedule, DepSchGridColCode)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#End Region

#Region "Acct Source Xcd Option Bucket Logic"
    ' US - 489857
    Private Sub PoupulateBOsFromSourceDropDownBucket()
        If (State.MyBO.DealerId <> Guid.Empty) Then
            Dim oDealer As New Dealer(State.MyBO.DealerId)

            If oDealer.AcctBucketsWithSourceXcd IsNot Nothing Then
                If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then

                    If cboLossCostPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        PopulateBOProperty(State.MyBO, "LossCostPercentSourceXcd", cboLossCostPercentSourceXcd, False, True)
                    End If

                    If cboAdminExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        PopulateBOProperty(State.MyBO, "AdminExpenseSourceXcd", cboAdminExpenseSourceXcd, False, True)
                    End If

                    If cboProfitExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        PopulateBOProperty(State.MyBO, "ProfitPercentSourceXcd", cboProfitExpenseSourceXcd, False, True)
                    End If

                    If cboCommPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        PopulateBOProperty(State.MyBO, "CommissionsPercentSourceXcd", cboCommPercentSourceXcd, False, True)
                    End If

                    If cboMarketingExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        PopulateBOProperty(State.MyBO, "MarketingPercentSourceXcd", cboMarketingExpenseSourceXcd, False, True)
                    End If

                    If cboIgnorePremium.Visible AndAlso cboIgnorePremium.Items.Count > 0 AndAlso cboIgnorePremium.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboIgnorePremium)).Equals(Codes.YESNO_Y) Then

                            ValidateIncomingSourceXcd()

                            If State.IsBucketIncomingSelected Then
                                ElitaPlusPage.SetLabelError(LabelIgnorePremium)
                                ElitaPlusPage.SetLabelError(LabelLossCostPercent)
                                ElitaPlusPage.SetLabelError(LabelProfitExpense)
                                ElitaPlusPage.SetLabelError(LabelAdminExpense)
                                ElitaPlusPage.SetLabelError(LabelMarketingExpense)
                                ElitaPlusPage.SetLabelError(LabelCommPercent)
                                Throw New GUIException(Message.MSG_INCOMING_OPTION_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INCOMING_OPTION_NOT_ALLOWED_WHEN_IGNORE_PREMIUM_IS_YES)
                            End If
                        End If
                    End If

                    ValidateDifferenceSourceXcd()

                    If State.IsDiffSelectedTwice Then
                        ElitaPlusPage.SetLabelError(LabelLossCostPercent)
                        ElitaPlusPage.SetLabelError(LabelProfitExpense)
                        ElitaPlusPage.SetLabelError(LabelAdminExpense)
                        ElitaPlusPage.SetLabelError(LabelMarketingExpense)
                        ElitaPlusPage.SetLabelError(LabelCommPercent)
                        Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ALLOWED_ONLY_ONCE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_SOURCE_ALLOWED_ONLY_FOR_ONE_BUCKET)
                    ElseIf State.IsDiffNotSelectedOnce Then
                        ElitaPlusPage.SetLabelError(LabelLossCostPercent)
                        ElitaPlusPage.SetLabelError(LabelProfitExpense)
                        ElitaPlusPage.SetLabelError(LabelAdminExpense)
                        ElitaPlusPage.SetLabelError(LabelMarketingExpense)
                        ElitaPlusPage.SetLabelError(LabelCommPercent)
                        Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ATLEAST_ONE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_OPTION_SHOULD_PRESENT_ATLEAST_FOR_ONE_BUCKET)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub PopulateSourceDropdownBucketFromBOs()
        With State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If oDealer.AcctBucketsWithSourceXcd IsNot Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000
                        If cboLossCostPercentSourceXcd.Visible Then
                            If .LossCostPercentSourceXcd IsNot Nothing AndAlso cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboLossCostPercentSourceXcd, .LossCostPercentSourceXcd)

                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxLossCostPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If cboProfitExpenseSourceXcd.Visible Then
                            If .ProfitPercentSourceXcd IsNot Nothing AndAlso cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboProfitExpenseSourceXcd, .ProfitPercentSourceXcd)

                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxProfitExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If cboMarketingExpenseSourceXcd.Visible Then
                            If .MarketingPercentSourceXcd IsNot Nothing AndAlso cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboMarketingExpenseSourceXcd, .MarketingPercentSourceXcd)

                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxMarketingExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd.Visible Then
                            If .AdminExpenseSourceXcd IsNot Nothing AndAlso cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboAdminExpenseSourceXcd, .AdminExpenseSourceXcd)

                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxAdminExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If cboCommPercentSourceXcd.Visible Then
                            If .CommissionsPercentSourceXcd IsNot Nothing AndAlso cboCommPercentSourceXcd.Items.Count > 0 Then
                                SetSelectedItem(cboCommPercentSourceXcd, .CommissionsPercentSourceXcd)

                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxCommPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, True)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub ValidateDifferenceSourceXcd()

        State.IsDiffSelectedTwice = False
        State.IsDiffNotSelectedOnce = False
        Dim countDiff As Integer = 0

        If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
            countDiff = countDiff + 1
        End If

        If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
            countDiff = countDiff + 1
        End If

        If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
            countDiff = countDiff + 1
        End If

        If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
            countDiff = countDiff + 1
        End If

        If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
            countDiff = countDiff + 1
        End If

        If countDiff > 1 Then
            State.IsDiffSelectedTwice = True
        ElseIf countDiff < 1 Then
            State.IsDiffNotSelectedOnce = True
        End If

    End Sub

    Private Sub ValidateIncomingSourceXcd()

        State.IsBucketIncomingSelected = False

        If cboLossCostPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            State.IsBucketIncomingSelected = True
        ElseIf cboProfitExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            State.IsBucketIncomingSelected = True
        ElseIf cboAdminExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            State.IsBucketIncomingSelected = True
        ElseIf cboCommPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            State.IsBucketIncomingSelected = True
        ElseIf cboMarketingExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            State.IsBucketIncomingSelected = True
        End If

    End Sub

    Private Sub BindSourceOptionDropdownlist()
        If (State.MyBO.DealerId <> Guid.Empty) Then
            Dim oDealer As New Dealer(State.MyBO.DealerId)

            If oDealer.AcctBucketsWithSourceXcd IsNot Nothing Then
                If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then

                    DisplaySourceXcdDropdownFields()

                    Dim oAcctBucketsSourceOption As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE")

                    cboLossCostPercentSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                         {
                                         .AddBlankItem = False,
                                         .TextFunc = AddressOf PopulateOptions.GetDescription,
                                         .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                         })

                    cboProfitExpenseSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                        {
                                        .AddBlankItem = False,
                                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                        })

                    cboAdminExpenseSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                        {
                                        .AddBlankItem = False,
                                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                        })

                    cboMarketingExpenseSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                        {
                                        .AddBlankItem = False,
                                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                        })

                    cboCommPercentSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                        {
                                        .AddBlankItem = False,
                                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                        })
                Else
                    HideSourceXcdDropdownFields()
                    EnablePercentageTextBox()
                End If
            Else
                HideSourceXcdDropdownFields()
                EnablePercentageTextBox()
            End If
        Else
            HideSourceXcdDropdownFields()
            EnablePercentageTextBox()
        End If
    End Sub

    Private Sub PopulateSourcePercentageBucketValues()
        With State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If oDealer.AcctBucketsWithSourceXcd IsNot Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        DisplaySourceXcdDropdownFields()
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000

                        If cboLossCostPercentSourceXcd IsNot Nothing Then
                            If (cboLossCostPercentSourceXcd.Visible AndAlso cboLossCostPercentSourceXcd.Items.Count > 0) Then
                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxLossCostPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If cboProfitExpenseSourceXcd IsNot Nothing Then
                            If (cboProfitExpenseSourceXcd.Visible AndAlso cboProfitExpenseSourceXcd.Items.Count > 0) Then
                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxProfitExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If cboMarketingExpenseSourceXcd IsNot Nothing Then
                            If (cboMarketingExpenseSourceXcd.Visible AndAlso cboMarketingExpenseSourceXcd.Items.Count > 0) Then
                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxMarketingExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd IsNot Nothing Then
                            If (cboAdminExpenseSourceXcd.Visible AndAlso cboAdminExpenseSourceXcd.Items.Count > 0) Then
                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxAdminExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If cboCommPercentSourceXcd IsNot Nothing Then
                            If (cboCommPercentSourceXcd.Visible AndAlso cboCommPercentSourceXcd.Items.Count > 0) Then
                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxCommPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, True)
                                End If
                            End If
                        End If
                    Else
                        HideSourceXcdDropdownFields()
                        EnablePercentageTextBox()
                    End If
                Else
                    HideSourceXcdDropdownFields()
                    EnablePercentageTextBox()
                End If
            Else
                HideSourceXcdDropdownFields()
                EnablePercentageTextBox()
            End If
        End With
    End Sub

    Private Sub SetSourceBucketValues()
        With State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If oDealer.AcctBucketsWithSourceXcd IsNot Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000

                        If cboLossCostPercentSourceXcd IsNot Nothing Then
                            If cboLossCostPercentSourceXcd.Visible AndAlso cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxLossCostPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If cboProfitExpenseSourceXcd IsNot Nothing Then
                            If cboProfitExpenseSourceXcd.Visible AndAlso cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxProfitExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If cboMarketingExpenseSourceXcd IsNot Nothing Then
                            If cboMarketingExpenseSourceXcd.Visible AndAlso cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxMarketingExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd IsNot Nothing Then
                            If cboAdminExpenseSourceXcd.Visible AndAlso cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxAdminExpense, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd IsNot Nothing Then
                            If cboCommPercentSourceXcd.Visible AndAlso cboCommPercentSourceXcd.Items.Count > 0 Then
                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    PopulateControlFromBOProperty(TextboxCommPercent, diffFixedValue, PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, TextboxCommPercent, True)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DisplaySourceXcdDropdownFields()
        ControlMgr.SetVisibleControl(Me, cboLossCostPercentSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, cboProfitExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, cboAdminExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, cboMarketingExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, cboCommPercentSourceXcd, True)
    End Sub

    Private Sub HideSourceXcdDropdownFields()
        ControlMgr.SetVisibleControl(Me, cboLossCostPercentSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, cboProfitExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, cboAdminExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, cboMarketingExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, cboCommPercentSourceXcd, False)
    End Sub

    Private Sub EnablePercentageTextBox()
        ControlMgr.SetEnableControl(Me, TextboxLossCostPercent, True)
        ControlMgr.SetEnableControl(Me, TextboxProfitExpense, True)
        ControlMgr.SetEnableControl(Me, TextboxMarketingExpense, True)
        ControlMgr.SetEnableControl(Me, TextboxAdminExpense, True)
        ControlMgr.SetEnableControl(Me, TextboxCommPercent, True)
    End Sub

#End Region
End Class

