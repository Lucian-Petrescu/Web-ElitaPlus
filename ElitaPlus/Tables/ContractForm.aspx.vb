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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Contract, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
        Set(ByVal Value As Guid)
            DealerID = Value
        End Set
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Contract(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()
        ErrorControllerDS.Clear_Hide()

        Dim oCompany As Company = Nothing
        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                'Me.btnSave_WRITE.Attributes.Add("onclick", "return ClearErrorCenter();")
                'Date Calendars
                Me.AddCalendar_New(Me.ImageButtonEndDate, Me.TextboxEndDate_WRITE)
                'Me.AddCalendar(Me.ImageButtonLastReconDate, Me.TextboxLastReconDate)
                Me.AddCalendar_New(Me.ImageButtonStartDate, Me.TextboxStartDate_WRITE)
                '  Me.MenuEnabled = False
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Contract
                Else

                    If Me.State.DealerBO Is Nothing AndAlso Not Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
                        Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
                    End If

                    oCompany = New Company(Me.State.DealerBO.CompanyId)
                    Me.State.CountryBO = New Country(oCompany.BusinessCountryId)
                    Me.State.MyBO.CountryId = Me.State.CountryBO.Id

                    Me.State.Company_Type_ID = oCompany.CompanyTypeId
                    Me.State.Company_ID = oCompany.Id
                End If

                Dim oCompanyGroup As CompanyGroup
                If (oCompany Is Nothing) Then
                    oCompany = New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                End If
                If (Not oCompany.CompanyGroupId.Equals(Guid.Empty)) Then
                    If (oCompanyGroup Is Nothing) Then
                        oCompanyGroup = New CompanyGroup(oCompany.CompanyGroupId)
                    End If
                    Me.State.use_comm_entity_type_id = oCompanyGroup.UseCommEntityTypeId
                End If
                PopulateDropdowns()
                PopulateProducer()
                Me.TranslateGridHeader(moGridView)
                SetGridItemStyleColor(GridViewDepreciationSchedule)
                TranslateGridHeader(GridViewDepreciationSchedule)
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If

            If Me.IsPostBack Then
                PopulateSourcePercentageBucketValues()
            End If

            CheckCoinsuranceDropDown()
            BindBoPropertiesToLabels()

            BindBoPropertiesToDepreciationScheduleGridHeaders()
            ClearGridViewHeadersAndLabelsErrorSign()

            CheckIfComingFromSaveConfirm()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

            Me.SetDecimalSeparatorSymbol()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
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

    Public Function GetListItemIDFromCode(ByVal strListCode As String, ByVal strItemCode As String) As Guid
        Dim resultID As Guid
        resultID = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(strListCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId), strItemCode)
        Return resultID
    End Function

    Private Sub SetID_Validation_DDandAcsel_Prod_Code()

        If Me.State.DealerBO Is Nothing AndAlso Not Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
            Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
        End If

        Dim oCompany As Company = New Company(Me.State.DealerBO.CompanyId)

        If oCompany.CompanyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, COMPANY_TYPE_SERVICES)) Then
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, False)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, False)
            Me.SetSelectedItem(Me.cboID_VALIDATION, LookupListNew.GetIdFromCode(LookupListNew.LK_VALIDATION_TYPES, IDVALIDATION_NONE))
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, False)
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, False)
            Me.SetSelectedItem(Me.cboAcselProdCode, System.Guid.Empty)
        Else
            Me.cboID_VALIDATION.SelectedIndex = Me.NOTHING_SELECTED
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, True)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, True)
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, True)
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, True)
            Me.cboAcselProdCode.SelectedIndex = Me.NOTHING_SELECTED
        End If

    End Sub
    Protected Sub EnableDisableFields(Optional ByVal blnComingFromMonthlyBillingOrDealerMarkup As Boolean = False)
        Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
        Dim lkCovExtNoneId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COVERAGE_EXTENSIONS, Codes.COV_EXT_NONE)

        'Further filter by CODE - Should return only one row
        lkYesNo.RowFilter += " and code='Y'"
        Dim yesId As Guid = New Guid(CType(lkYesNo.Item(0).Item("ID"), Byte()))
        If Not blnComingFromMonthlyBillingOrDealerMarkup Then
            ControlMgr.SetEnableControl(Me, cboID_VALIDATION, (Not Me.State.MyBO.IsNew) _
                And LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
            ControlMgr.SetEnableControl(Me, LabelID_VALIDATION, (Not Me.State.MyBO.IsNew) _
                And LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)

            'Acsel/x Product Code for Insurance company
            ControlMgr.SetVisibleControl(Me, cboAcselProdCode, (Not Me.State.MyBO.IsNew) _
                And LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
            ControlMgr.SetVisibleControl(Me, LabelAcselProdCode, (Not Me.State.MyBO.IsNew) _
                And LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.State.Company_Type_ID) <> COMPANY_TYPE_SERVICE)
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
        Dim currRecurringPremiumId As Guid = Me.GetSelectedItem(Me.cboRecurringPremium)
        Dim recurringPremiumIsYes As Boolean = False
        Dim currInstallmentPaymentId As Guid = Me.GetSelectedItem(Me.cboInstallmentPayment)
        Dim InstallmentPaymentIsYes As Boolean = False
        If currInstallmentPaymentId.Equals(yesId) Then
            InstallmentPaymentIsYes = True
        End If
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        'Req-1016 - Start
        'If currMonthlyBillingId.Equals(yesId) Then 
        If ((Not currRecurringPremiumId.Equals(emptyGuid)) And (Not currRecurringPremiumId.Equals(singlePremiumId))) Then
            recurringPremiumIsYes = True
        End If
        ControlMgr.SetVisibleControl(Me, LabelDaysToSuspend, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, LabelDaysToCancel, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, TextboxDaysToSuspend, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, TextboxDaysToCancel, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, LabelIncludeFirstPmt, recurringPremiumIsYes)
        ControlMgr.SetVisibleControl(Me, cboIncludeFirstPmt, recurringPremiumIsYes)
        If Not recurringPremiumIsYes Then
            Me.cboIncludeFirstPmt.SelectedIndex = Me.NOTHING_SELECTED
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

        If Not recurringPremiumIsYes And InstallmentPaymentIsYes Then
            ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, True)
            ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, True)
        Else
            ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, False)
            ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, False)
            Me.cboPayOutstandingAmount.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
        End If

        'Dealer Markup
        Dim currDealerMarkup As Guid = Me.GetSelectedItem(Me.cboDealerMarkup_WRITE)
        Dim dealerMarkupIsYes As Boolean = False
        Dim currRestrictMarkup As Guid = Me.GetSelectedItem(Me.cboRestrictMarkup_WRITE)
        Dim currCoverageMarkupDistribution As Guid = Me.GetSelectedItem(Me.ddlAllowCoverageMarkupDistribution)

        If (recurringPremiumIsYes) Then
            If currDealerMarkup.Equals(yesId) Then
                dealerMarkupIsYes = True
                'Dim RestrictMarkupIsYes As Boolean = False
                If currRestrictMarkup.Equals(noId) Then
                    Me.SetSelectedItem(Me.cboRestrictMarkup_WRITE, noId)
                End If
                If currCoverageMarkupDistribution.Equals(noId) Then
                    Me.SetSelectedItem(Me.ddlAllowCoverageMarkupDistribution, noId)
                End If
            Else
                Me.SetSelectedItem(Me.cboRestrictMarkup_WRITE, noId)
                Me.SetSelectedItem(Me.ddlAllowCoverageMarkupDistribution, noId)
            End If
        Else
            If currDealerMarkup.Equals(yesId) Then
                dealerMarkupIsYes = True
            Else
                Me.SetSelectedItem(Me.cboRestrictMarkup_WRITE, noId)
                Me.SetSelectedItem(Me.ddlAllowCoverageMarkupDistribution, noId)
            End If
        End If
        ControlMgr.SetEnableControl(Me, cboRestrictMarkup_WRITE, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, LabelRestrictMarkup_WRITE, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, ddlAllowCoverageMarkupDistribution, dealerMarkupIsYes)
        ControlMgr.SetEnableControl(Me, lblAllowCoverageMarkupDistribution, dealerMarkupIsYes)

        Dim FDAF_noneId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_FUTURE_DATE_ALLOW_FOR, Codes.FDAF_NONE)
        If Me.State.MyBO.IsNew And Not Me.State.IsACopy Then
            Me.SetSelectedItem(Me.cboIgnoreCovAmt, noId)
            Me.SetSelectedItem(Me.cboBackEndClaimAllowed, noId)
            Me.SetSelectedItem(Me.cboExtendCoverage, lkCovExtNoneId)
            'REQ-490 - changed list from YES/NO to the EDIT MFG Term list.  Removing the auto set.  The field is required, so the user will need to choose a value.
            'Me.SetSelectedItem(Me.cboEDIT_MFG_TERM, noId)

            Me.SetSelectedItem(Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, noId)
            Me.SetSelectedItem(Me.cboInstallmentPayment, noId)
            Me.SetSelectedItem(Me.cboDeductible_By_Manufacturer, noId)
            Me.SetSelectedItem(Me.cboIsCommPCodeId, noId)
            Me.SetSelectedItem(Me.cboFutureDateAllowFor, FDAF_noneId)

            'Me.PopulateControlFromBOProperty(Me.TextboxDaysOfFirstPymt, ZERO_STRING)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToCancelCert, ZERO_STRING)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToSendLetter, ZERO_STRING)
            Me.PopulateControlFromBOProperty(Me.txtExtendCoverageByExtraMonths, ZERO_STRING)
            Me.PopulateControlFromBOProperty(Me.txtExtendCoverageByExtraDays, ZERO_STRING)
        End If

        ShowInstPymtFields()
        EnableFirstPaymentMonthsField()

        TheDealerControl.ChangeEnabledControlProperty(Me.State.MyBO.IsNew)
        'ControlMgr.SetEnableControl(Me, TheDealerControl.ClearMultipleDrop, Me.State.MyBO.IsNew)
        ' ControlMgr.SetEnableControl(Me, cboDealer_WRITE, Me.State.MyBO.IsNew)

        'Start and End Dates
        ControlMgr.SetEnableControl(Me, TextboxStartDate_WRITE, True)
        ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
        ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
        ControlMgr.SetEnableControl(Me, TextboxEndDate_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        If Not Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, TextboxStartDate_WRITE, False)
            ControlMgr.SetEnableControl(Me, ImageButtonStartDate, False)
            If Not Me.State.MyBO.IsLastContract Then
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
        If Me.State.IsACopy Then
            ControlMgr.SetEnableControl(Me, cboRecurringPremium, True)
        End If
        'Req - 1016 End

        'New With Copy Button
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnTNC, False)
        End If

        If Not Me.State.MyBO.CanItBeDeleted Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
        End If

        'REQ-756 - hide the recollection if not VSC
        If Me.State.DealerBO Is Nothing AndAlso Not Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
            Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
        End If

        If Not Me.State.DealerBO Is Nothing AndAlso Me.State.DealerBO.DealerTypeDesc = Me.State.DealerBO.DEALER_TYPE_DESC Then
            ControlMgr.SetVisibleControl(Me, Me.txtCollectionReAttempts, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelCollectionReAttempts, True)
            ControlMgr.SetVisibleControl(Me, Me.txtPastDueMonthsAllowed, True)
            ControlMgr.SetVisibleControl(Me, Me.lblPastDueMonthsAllowed, True)
            ControlMgr.SetEnableControl(Me, Me.ddlAllowCoverageMarkupDistribution, False)
            ControlMgr.SetEnableControl(Me, Me.lblAllowCoverageMarkupDistribution, False)
        Else
            Me.txtCollectionReAttempts.Text = String.Empty
            Me.txtPastDueMonthsAllowed.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, Me.txtCollectionReAttempts, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelCollectionReAttempts, False)
            ControlMgr.SetVisibleControl(Me, Me.txtPastDueMonthsAllowed, False)
            ControlMgr.SetVisibleControl(Me, Me.lblPastDueMonthsAllowed, False)
        End If
        'REQ-5773 Start
        If Me.State.use_comm_entity_type_id.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) And Codes.DEALER_TYPES__VSC Then
            ControlMgr.SetVisibleControl(Me, Me.lblPaymentProcessingTypeId, True)
            ControlMgr.SetVisibleControl(Me, Me.ddlPaymentProcessingTypeId, True)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoName, True)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoName, True)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoTaxId, True)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoTaxId, True)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoPercent, True)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoPercent, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.lblPaymentProcessingTypeId, False)
            ControlMgr.SetVisibleControl(Me, Me.ddlPaymentProcessingTypeId, False)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoName, False)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoName, False)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoTaxId, False)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoTaxId, False)
            ControlMgr.SetVisibleControl(Me, Me.lblRdoPercent, False)
            ControlMgr.SetVisibleControl(Me, Me.txtRdoPercent, False)
        End If
        If Me.State.MyBO.PaymentProcessingTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.THIRD_PARTY_PAYMENT)) Then
            ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyName, True)
            ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyName, True)
            ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyTaxId, True)
            ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyTaxId, True)
        Else
            txtThirdPartyName.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyName, False)
            ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyName, False)
            txtThirdPartyTaxId.Text = String.Empty
            ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyTaxId, False)
            ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyTaxId, False)
        End If

        ' if new or a copy or collective and manualy entered, then enable policy.
        If Me.State.MyBO.IsNew Then

            If Not blnComingFromMonthlyBillingOrDealerMarkup Then
                Me.cboLineOfBusiness.Items.Clear()
                Me.SetSelectedItem(Me.cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                Me.SetSelectedItem(Me.cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))

                ' if Dealer is not selected in case of New then disable ind policy dropdowns.
                ControlMgr.SetEnableControl(Me, cboPolicyType, Not Me.State.MyBO.DealerId.Equals(Guid.Empty))
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, Not Me.State.MyBO.DealerId.Equals(Guid.Empty))

                ControlMgr.SetEnableControl(Me, cboLineOfBusiness, True)

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
            End If

        End If

        ' if Edit then Disable Policy type related dropdowns/controls.
        If Not Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, Me.cboPolicyType, False)
            ControlMgr.SetEnableControl(Me, Me.cboCollectivePolicyGeneration, False)
            ControlMgr.SetEnableControl(Me, Me.cboLineOfBusiness, False)
            ControlMgr.SetEnableControl(Me, Me.TextboxPolicyNumber, False)

            If Me.State.MyBO.PolicyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) AndAlso
                Me.State.MyBO.PolicyGenerationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AdminExpense", Me.LabelAdminExpense)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoMfgCoverageId", Me.LabelAutoMFG)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CancellationDays", Me.LabelDaysToCancel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CommissionsPercent", Me.LabelCommPercent)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContractTypeId", Me.LabelContractType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CurrencyId", Me.LabelCurrency)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.TheDealerControl.CaptionLabel)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.LabelDealer_WRITE)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerMarkupId", Me.LabelDealerMarkup)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeductibleBasedOnId", Me.LabelDeductibleBasedOn)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Deductible", Me.LabelDeductible)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EditModelId", Me.LabelEditModel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.LabelStartDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FixedEscDurationFlag", Me.LabelFixedEscDurationFlag)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.LabelEndDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FundingSourceId", Me.LabelFundingSource)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "LastRecon", Me.LabelLastReconDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Layout", Me.LabelLayout)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FullRefundDays", Me.LabelFullRefundDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LossCostPercent", Me.LabelLossCostPercent)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MarketingPercent", Me.LabelMarketingExpense)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MinReplacementCost", Me.LabelMinReplCost)
        'Req-1016 - Start
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "MonthlyBillingId", Me.LabelMonthlyBilling)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RecurringPremiumId", Me.LabelRecurringPremium)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RecurringWarrantyPeriod", Me.lblPeridiocBillingWarntyPeriod)
        'Req-1016 - End
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NetCommissionsId", Me.LabelNetCommission)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NetMarketingId", Me.LabelNetMarketing)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NetTaxesId", Me.LabelNetTaxes)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Policy", Me.LabelPolicy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProfitPercent", Me.LabelProfitExpense)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RestrictMarkupId", Me.LabelRestrictMarkup_WRITE)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowCoverageMarkupDistribution", Me.lblAllowCoverageMarkupDistribution)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SuspenseDays", Me.LabelDaysToSuspend)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TypeOfEquipmentId", Me.LabelTypeOfEquipment)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TypeOfInsuranceId", Me.LabelTypeOfIns)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TypeOfMarketingId", Me.LabelTypeOfMarketing)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "WaitingPeriod", Me.LabelWaitingPeriod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "WarrantyMaxDelay", Me.LabelWarrantyMaxDelay)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReplacementPolicyId", Me.LabelREPLACEMENT_POLICY)
        'REQ-1333
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReplacementPolicyClaimCount", Me.lblReplacementPolicyClaimCount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CancellationReasonId", Me.LabelCancellationReason)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CoinsuranceId", Me.LabelCOINSURANCE)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ParticipationPercent", Me.LabelPARTICIPATION_PERCENT)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RatingPlan", Me.LabelRatingPlan)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ID_Validation_Id", Me.LabelID_VALIDATION)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Acsel_Prod_Code_Id", Me.LabelAcselProdCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "claim_control_id", Me.LabelClaimControl)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IgnoreIncomingPremiumID", Me.LabelIgnorePremium)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RemainingMFGDays", Me.LabelRemainingMFGDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IgnoreCoverageAmtId", Me.LabelIgnoreCovAmt)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BackEndClaimsAllowedId", Me.LabelBackEndClaimAllowed)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InstallmentPaymentId", Me.LabelInstallmentPayment)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CollectionReAttempts", Me.LabelCollectionReAttempts)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PastDueMonthsAllowed", Me.lblPastDueMonthsAllowed)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysOfFirstPymt", Me.LabelDaysOfFirstPymt)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysToSendLetter", Me.LabelDaysToSendLetter)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysToCancelCert", Me.LabelDaysToCancelCert)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeductibleByManufacturerId", Me.LabelDeductibleByMfg)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProRataMethodId", Me.LabelProRataMethodId)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "CurrencyConversionId", Me.Label_CURRENCY_CONVERSION)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CurrencyOfCoveragesId", Me.LabelCURRENCY_OF_COVERAGES)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoSetLiabilityId", Me.LabelAutoSetLiability)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageDeductibleId", Me.LabelCovDeductible)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeductiblePercent", Me.LabelDeductiblePercent)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IgnoreWaitingPeriodWsdPsd", Me.LabelApplyWaitingPeriod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RepairDiscountPct", Me.LabelRepairDiscountPct)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReplacementDiscountPct", Me.LabelReplacementDiscountPrc)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "NumOfClaims", Me.LabelNumOfClaims)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NumOfRepairClaims", Me.LabelNumOfRepairClaims)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NumOfReplacementClaims", Me.LabelNumOfReplClaims)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "PenaltyPct", Me.LabelPenaltyPct)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "EditMFGTermId", Me.LabelEditMfgTerm)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AcctBusinessUnitId", Me.LabelAcctBusinessUnit)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClipPercent", Me.lblCLIPct)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsCommPCodeId", Me.LabelIsCommPCodeId)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "BaseInstallments", Me.LabelBaseInstallments)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BillingCycleFrequency", Me.LabelBillingCycleFrequency)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InstallmentsBaseReducer", Me.LabelInstallmentsBaseReducer)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxInstallments", Me.LabelMaxNumofInstallments)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IncludeFirstPmt", Me.LabelIncludeFirstPmt)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CollectionCycleTypeId", Me.LabelCollectionCycleType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CycleDay", Me.LabelCycleDay)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OffsetBeforeDueDate", Me.LabelOffsetBeforeDueDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InsPremiumFactor", Me.lblInsPremFactor)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "ExtendCoverageId", Me.lblExtendCoverage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ExtraDaysToExtendCoverage", Me.lblExtendCoverageBy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ExtraMonsToExtendCoverage", Me.lblExtendCoverageBy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimLimitBasedOnId", Me.LabelReplacementBasedOn)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowDifferentCoverage", Me.labelAllowDifferentCoverage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CustmerAddressRequiredId", Me.labelCustAddressRequired)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FutureDateAllowForID", Me.lblFutureDateAllowFor)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FirstPymtMonths", Me.labelFirstPymtMonths)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PayOutstandingPremiumId", Me.LabelPayOutstandingAmount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizedAmountMaxUpdates", Me.lblAuthorizedAmountMaxUpdates)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowPymtSkipMonths", Me.lblAllowPymtSkipMonths)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BillingCycleTypeId", Me.lblBillingCycleType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DailyRateBasedOnId", Me.lblDailyRateBasedOn)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowBillingAfterCancellation", Me.lblAllowBillingAfterCancellation)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowCollectionAfterCancellation", Me.lblAllowCollectionAfterCancellation)

        'Added for Req-635
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowNoExtended", Me.lblAllowNoExtended)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysToReportClaim", Me.lblDaysToReportClaim)
        'Req-703 Start
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MarketingPromotionId", Me.lblMarketingPromo)
        'Req-703 End

        'REQ-794
        'This line of code is commented for the def-1861
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "IgnoreCoverageRateId", Me.LabelIgnoreCovRate)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowMultipleRejectionsId", Me.lblAllowMultipleRejections)

        'REQ-1050 start
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysToReactivate", Me.lblDaysToReactivate)
        'REQ-1050 END
        'REQ-5773 Start
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentProcessingTypeId", Me.lblPaymentProcessingTypeId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ThirdPartyName", Me.lblThirdPartyName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ThirdPartyTaxId", Me.lblThirdPartyTaxId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RdoName", Me.lblRdoName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RdoTaxId", Me.lblRdoTaxId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RdoPercent", Me.lblRdoPercent)
        'REQ-5773 End

        Me.BindBOPropertyToLabel(Me.State.MyBO, "OverrideEditMfgTerm", Me.LabelOverrideEditMfgTerm)

        ' Ind Policy
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PolicyTypeId", Me.LabelPolicyType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PolicyGenerationId", Me.LabelCollectivePolicyGeneration)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProducerId", Me.lblProducer)

        Me.ClearGridHeadersAndLabelsErrSign()
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
        TheDealerControl.SelectedGuid = Me.State.MyBO.DealerId
    End Sub

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Contract")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Contract")
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
        Me.cboAutoMFG.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboContractType, LookupListNew.DropdownLookupList("CONTP", langId, True))
        Me.cboContractType.Populate(CommonConfigManager.Current.ListManager.GetList("CONTP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        Me.cboPolicyType.Populate(CommonConfigManager.Current.ListManager.GetList("POLTYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

        Me.cboCollectivePolicyGeneration.Populate(CommonConfigManager.Current.ListManager.GetList("POLGEN", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)


        'ElitaPlusPage.BindListControlToDataView(Me.cboCurrency, currencyVW)
        Me.cboCurrency.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboID_VALIDATION, LookupListNew.DropdownLookupList("IDVAL", langId, True))
        Me.cboID_VALIDATION.Populate(CommonConfigManager.Current.ListManager.GetList("IDVAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAcselProdCode, LookupListNew.DropdownLookupList("ACSPC", langId, True))
        Me.cboAcselProdCode.Populate(CommonConfigManager.Current.ListManager.GetList("ACSPC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        Me.populateDealer()
        'ElitaPlusPage.BindListControlToDataView(Me.cboDealerMarkup_WRITE, yesNoLkL)
        Me.cboDealerMarkup_WRITE.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboEditModel, yesNoLkL)
        Me.cboEditModel.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFixedEscDurationFlag, yesNoLkL)
        Me.cboFixedEscDurationFlag.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFundingSource, LookupListNew.DropdownLookupList("FUNDS", langId, True))
        Me.cboFundingSource.Populate(CommonConfigManager.Current.ListManager.GetList("FUNDS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'Req -1016 - Start
        'Dim periodRenewSortedByCode As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_PERIOD_RENEW, langId, True)
        'periodRenewSortedByCode.Sort = "code"
        'ElitaPlusPage.BindListControlToDataView(Me.cboRecurringPremium, periodRenewSortedByCode, , , , False)
        Me.cboRecurringPremium.Populate(CommonConfigManager.Current.ListManager.GetList("PERIODRENEW", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

        'Req -1016 - end
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetCommission, yesNoLkL)
        Me.cboNetCommission.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetMarketing, yesNoLkL)
        Me.cboNetMarketing.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboNetTaxes, yesNoLkL)
        Me.cboNetTaxes.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCovDeductible, yesNoLkL)
        Me.cboCovDeductible.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCustAddressRequired, yesNoLkL)
        Me.cboCustAddressRequired.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboPayOutstandingAmount, yesNoLkL)
        Me.cboPayOutstandingAmount.Populate(oYesNoList, populateOptions)

        'Dim futureDateAllowForLkL As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_FUTURE_DATE_ALLOW_FOR_CODE, langId, True)
        'ElitaPlusPage.BindListControlToDataView(Me.cboFutureDateAllowFor, futureDateAllowForLkL)
        Me.cboFutureDateAllowFor.Populate(CommonConfigManager.Current.ListManager.GetList("FDAF", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboRestrictMarkup_WRITE, yesNoLkL, , , False)
        Me.cboRestrictMarkup_WRITE.Populate(oYesNoList, populateOptions1)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowCoverageMarkupDistribution, yesNoLkL, , , False)
        Me.ddlAllowCoverageMarkupDistribution.Populate(oYesNoList, populateOptions1)

        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfEquipment, LookupListNew.DropdownLookupList("TEQP", langId, True))
        Me.cboTypeOfEquipment.Populate(CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfIns, LookupListNew.DropdownLookupList("TINS", langId, True))
        Me.cboTypeOfIns.Populate(CommonConfigManager.Current.ListManager.GetList("TINS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboTypeOfMarketing, LookupListNew.DropdownLookupList("TMKT", langId, True))
        Me.cboTypeOfMarketing.Populate(CommonConfigManager.Current.ListManager.GetList("TMKT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboReplacementPolicy, LookupListNew.GetReplacementPolicyLookupList(langId), "DESCRIPTION", "ID", False)
        Me.cboReplacementPolicy.Populate(CommonConfigManager.Current.ListManager.GetList("REPP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCollectionCycleType, LookupListNew.GetCollectionCycleTypeLookupList(langId), "DESCRIPTION", "ID", True)
        Me.cboCollectionCycleType.Populate(CommonConfigManager.Current.ListManager.GetList("COLLCYCLE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboReplacementBasedOn, LookupListNew.GetReplacementBasedOnLookupList(langId), "DESCRIPTION", "ID", True)
        Me.cboReplacementBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("REPLOG", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboDeductibleBasedOn, LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId),,, False)
        Dim olistcontext As ListContext = New ListContext()
        olistcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        cboDeductibleBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ComputeDeductibleBasedOnAndExpressions", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=olistcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                  })

        If Not Me.State.MyBO.IsNew Then
            'ElitaPlusPage.BindListControlToDataView(Me.cboCancellationReason, LookupListNew.GetCancellationReasonByDealerIdLookupList(Me.State.MyBO.DealerId), "DESCRIPTION", "ID", True)
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = Me.State.MyBO.DealerId
            Me.cboCancellationReason.Populate(CommonConfigManager.Current.ListManager.GetList("CancellationReasonsByDealer", Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = textFun
                                                  })
        End If

        'ElitaPlusPage.BindListControlToDataView(Me.cboClaimControlID, yesNoLkL)
        Me.cboClaimControlID.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnorePremium, yesNoLkL)
        Me.cboIgnorePremium.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnoreCovAmt, yesNoLkL)
        Me.cboIgnoreCovAmt.Populate(oYesNoList, populateOptions)

        'REQ-490 - changed list from YES/NO to the EDIT MFG Term list
        'ElitaPlusPage.BindListControlToDataView(Me.cboEDIT_MFG_TERM, LookupListNew.DropdownLookupList(LookupListCache.LK_EDTMFGTRM, langId, True))
        Me.cboEDIT_MFG_TERM.Populate(CommonConfigManager.Current.ListManager.GetList("EDTMFGTRM", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboCOINSURANCE, LookupListNew.GetCoinsuranceLookupList(langId), , , True)
        Me.cboCOINSURANCE.Populate(CommonConfigManager.Current.ListManager.GetList("COINS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'Dim CoinsuranceLookupListSortedByCode As DataView = LookupListNew.GetCoinsuranceLookupList(langId)
        'CoinsuranceLookupListSortedByCode.Sort = "CODE"
        'ElitaPlusPage.BindListControlToDataView(Me.cboCOINSURANCE_Code, CoinsuranceLookupListSortedByCode, "CODE", , True)
        Me.cboCOINSURANCE_Code.Populate(CommonConfigManager.Current.ListManager.GetList("COINS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                  })

        'ElitaPlusPage.BindListControlToDataView(Me.cboCURRENCY_CONVERSION, yesNoLkL)
        Me.cboCURRENCY_CONVERSION.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboCURRENCY_OF_COVERAGES, currencyVW)
        Me.cboCURRENCY_OF_COVERAGES.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAutoSetLiability, yesNoLkL)
        Me.cboAutoSetLiability.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboBackEndClaimAllowed, yesNoLkL)
        Me.cboBackEndClaimAllowed.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, yesNoLkL)
        Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboInstallmentPayment, yesNoLkL)
        Me.cboInstallmentPayment.Populate(oYesNoList, populateOptions)
        'Dim IncludeFirstPayment As DataView = LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_INCLUDE_FIRST_PAYMENT, langId)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIncludeFirstPmt, IncludeFirstPayment, , , True)
        Me.cboIncludeFirstPmt.Populate(CommonConfigManager.Current.ListManager.GetList("INCFIRSTPYMT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboExtendCoverage, covExtVW)
        Me.cboExtendCoverage.Populate(CommonConfigManager.Current.ListManager.GetList("COVEXT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowPymtSkipMonths, yesNoLkL)
        Me.ddlAllowPymtSkipMonths.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboDeductible_By_Manufacturer, yesNoLkL)
        Me.cboDeductible_By_Manufacturer.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboIsCommPCodeId, yesNoLkL)
        Me.cboIsCommPCodeId.Populate(oYesNoList, populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboBaseInstallments, LookupListNew.DropdownLookupList("BINSTAL", langId, True))
        Me.cboBaseInstallments.Populate(CommonConfigManager.Current.ListManager.GetList("BINSTAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.cboBillingCycleFrequency, LookupListNew.DropdownLookupList("BCYCLE", langId, True))
        Me.cboBillingCycleFrequency.Populate(CommonConfigManager.Current.ListManager.GetList("BCYCLE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowDifferentCoverage, yesNoLkL)
        Me.cboAllowDifferentCoverage.Populate(oYesNoList, populateOptions)
        'Added from Req-635
        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowNoExtended, yesNoLkL)
        Me.cboAllowNoExtended.Populate(oYesNoList, populateOptions)

        'Req-703 Start
        'ElitaPlusPage.BindListControlToDataView(Me.cboMarketingPromo, yesNoLkL)
        Me.cboMarketingPromo.Populate(oYesNoList, populateOptions)
        'Req-703 End

        'Dim allowCCRejectsList As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ALLOW_CC_REJECTIONS_CODE, langId, True)
        'ElitaPlusPage.BindListControlToDataView(Me.cboAllowMultipleRejections, allowCCRejectsList)
        Me.cboAllowMultipleRejections.Populate(CommonConfigManager.Current.ListManager.GetList("ACCR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'Dim proRataMethodVW As DataView = LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_PRO_RATA_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        'ElitaPlusPage.BindListControlToDataView(Me.cboProRataMethodId, proRataMethodVW)
        Me.cboProRataMethodId.Populate(CommonConfigManager.Current.ListManager.GetList("PRMETHOD", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)


        'REQ-1073
        'ElitaPlusPage.BindListControlToDataView(Me.ddlBillingCycleType, LookupListNew.DropdownLookupList("BCT", langId))
        Me.ddlBillingCycleType.Populate(CommonConfigManager.Current.ListManager.GetList("BCT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlDailyRateBasedOn, LookupListNew.DropdownLookupList("DRBO", langId))
        Me.ddlDailyRateBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("DRBO", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

        'REQ-1005
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowBillingAfterCancellation, yesNoLkL)
        Me.ddlAllowBillingAfterCancellation.Populate(oYesNoList, populateOptions)
        'ElitaPlusPage.BindListControlToDataView(Me.ddlAllowCollectionAfterCancellation, yesNoLkL)
        Me.ddlAllowCollectionAfterCancellation.Populate(oYesNoList, populateOptions)

        'REQ-5773 Start
        'BindListControlToDataView(Me.ddlPaymentProcessingTypeId, LookupListNew.DropdownLookupList(LookupListNew.LK_PPT, langId, True))
        Me.ddlPaymentProcessingTypeId.Populate(CommonConfigManager.Current.ListManager.GetList("PPT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
        'REQ-5773 End

        'Me.cboOverrideEditMfgTerm.PopulateOld("OVERMFGTERM", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        Me.cboOverrideEditMfgTerm.Populate(CommonConfigManager.Current.ListManager.GetList("OVERMFGTERM", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
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

    Private Sub PopulateAccountBusinessUnit(ByVal dealerId As Guid)
        'Get the accounting company associated with this contract / dealer / company to full out the business units
        'Dim dv As DataView

        If (Not dealerId.Equals(Guid.Empty)) Then

            If Me.State.DealerBO Is Nothing Then
                Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
            End If

            Me.State.Company_ID = Me.State.DealerBO.CompanyId

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = Me.State.DealerBO.CompanyId
            Dim AcctCompanyList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AccountingCompanyByCompany", context:=listcontext)

            Dim listcontext1 As ListContext = New ListContext()
            listcontext1.AccountingCompanyId = AcctCompanyList.FirstOrDefault().ListItemId
            Dim BusinessUnitList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessUnitByAcctCompany", context:=listcontext1)

            If BusinessUnitList.Count = 1 Then
                Me.cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
                        {
                            .AddBlankItem = False,
                            .TextFunc = AddressOf .GetCode,
                            .SortFunc = AddressOf .GetCode
                        })
            Else
                Me.cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
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
                    If Not BusinessUnitList Is Nothing Then
                        BusinessUnitList.AddRange(BusinessUnitListForAcctCompany)
                    Else
                        BusinessUnitList = BusinessUnitListForAcctCompany.Clone()
                    End If
                End If
            Next
            Me.cboAcctBusinessUnit.Populate(BusinessUnitList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .TextFunc = AddressOf .GetCode,
                        .SortFunc = AddressOf .GetCode
                    })
        End If
    End Sub
    Private Sub PopulateProducer()

        If Not Me.State.DealerBO Is Nothing Then
            Dim dealerCompany As New ArrayList
            dealerCompany.Add(Me.State.DealerBO.CompanyId)

            Dim oProducerview As DataView = LookupListNew.GetProducerLookupList(dealerCompany)
            ElitaPlusPage.BindListControlToDataView(Me.ddlProducer, oProducerview)
        Else
            Dim oProducerview As DataView = LookupListNew.GetProducerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            ElitaPlusPage.BindListControlToDataView(Me.ddlProducer, oProducerview)
        End If

    End Sub
    Protected Sub PopulateFormFromBOs()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim policyType As Guid = Guid.Empty

        With Me.State.MyBO

            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If (oDealer.DealerTypeDesc = "VSC") Then
                    If (.IsNew) Then
                        .CoverageDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                        .DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId), Codes.DEDUCTIBLE_BASED_ON__FIXED)
                    End If
                    Me.ChangeEnabledProperty(Me.cboCovDeductible, False)
                    EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, .IsNew, False)
                    Me.ChangeEnabledProperty(Me.cboDeductibleBasedOn, False)
                End If
            End If

            PopulateAccountBusinessUnit(Me.State.MyBO.DealerId)

            Me.PopulateControlFromBOProperty(Me.TextboxAdminExpense, .AdminExpense, Me.PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.TextboxCommPercent, .CommissionsPercent, Me.PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToCancel, .CancellationDays)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToSuspend, .SuspenseDays)
            Me.PopulateControlFromBOProperty(Me.TextboxDeductible, .Deductible)
            Me.PopulateControlFromBOProperty(Me.TextboxDeductiblePercent, .DeductiblePercent)
            Me.PopulateControlFromBOProperty(Me.TextboxEndDate_WRITE, .Expiration)
            Me.PopulateControlFromBOProperty(Me.TextboxLayout, .Layout)
            Me.PopulateControlFromBOProperty(Me.TextboxFullRefundDays, .FullRefundDays)
            Me.PopulateControlFromBOProperty(Me.TextboxLossCostPercent, .LossCostPercent, Me.PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.TextboxMarketingExpense, .MarketingPercent, Me.PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.TextboxMinRepCost, .MinReplacementCost)
            Me.PopulateControlFromBOProperty(Me.TextboxPolicyNumber, .Policy)
            Me.PopulateControlFromBOProperty(Me.TextboxProfitExpense, .ProfitPercent, Me.PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.TextboxStartDate_WRITE, .Effective)
            Me.PopulateControlFromBOProperty(Me.TextboxWaitingPeriod, .WaitingPeriod)
            Me.PopulateControlFromBOProperty(Me.TextboxWarrantyMaxDelay, .WarrantyMaxDelay)
            Me.PopulateControlFromBOProperty(Me.TextBoxRemainingMFGDays, .RemainingMFGDays)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysOfFirstPymt, .DaysOfFirstPymt)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToSendLetter, .DaysToSendLetter)
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToCancelCert, .DaysToCancelCert)
            Me.PopulateControlFromBOProperty(Me.txtCLIPPct, .ClipPercent)
            Me.PopulateControlFromBOProperty(Me.txtInsPremFactor, .InsPremiumFactor, PERCENT_FORMAT)
            Me.PopulateControlFromBOProperty(Me.txtExtendCoverageByExtraMonths, .ExtraMonsToExtendCoverage)
            Me.PopulateControlFromBOProperty(Me.txtExtendCoverageByExtraDays, .ExtraDaysToExtendCoverage)
            Me.PopulateControlFromBOProperty(Me.cboCustAddressRequired, .CustmerAddressRequiredId)
            Me.PopulateControlFromBOProperty(Me.txtFirstPaymentMonths, .FirstPymtMonths)
            'Req - 1016 Start
            Me.PopulateControlFromBOProperty(Me.txtPeridiocBillingWarntyPeriod, .RecurringWarrantyPeriod)
            'Req - 1016 End
            Me.SetSelectedItem(Me.cboAutoMFG, .AutoMfgCoverageId)
            Me.SetSelectedItem(Me.cboContractType, .ContractTypeId)

            ' Individual Policy related controls.
            If Me.State.MyBO.IsNew Then
                ' Default setting to CP/ME
                Me.SetSelectedItem(Me.cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                Me.SetSelectedItem(Me.cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))
                Me.cboLineOfBusiness.Items.Clear()
            Else
                Me.SetSelectedItem(Me.cboPolicyType, .PolicyTypeId)
                Me.SetSelectedItem(Me.cboCollectivePolicyGeneration, .PolicyGenerationId)
                PopulateLineOfBusinessDropDown(.PolicyTypeId, True)

                If .PolicyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) AndAlso
                .PolicyGenerationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then

                    ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                    ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                    ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
                End If

                If Not .LineOfBusinessId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.cboLineOfBusiness, .LineOfBusinessId)
                End If

            End If

            Me.SetSelectedItem(Me.cboCurrency, .CurrencyId)
            Me.TheDealerControl.SelectedGuid = .DealerId
            SetDealerCode()

            Me.SetSelectedItem(Me.cboDealerMarkup_WRITE, .DealerMarkupId)
            Me.SetSelectedItem(Me.cboEditModel, .EditModelId)
            Me.SetSelectedItem(Me.cboFixedEscDurationFlag, .FixedEscDurationFlag)
            Me.SetSelectedItem(Me.cboFundingSource, .FundingSourceId)
            'Req-1016 Start
            'Me.SetSelectedItem(Me.cboMonthlyBilling, .MonthlyBillingId)
            Me.SetSelectedItem(Me.cboRecurringPremium, .RecurringPremiumId)
            'Req-1016 end
            Me.SetSelectedItem(Me.cboNetCommission, .NetCommissionsId)
            Me.SetSelectedItem(Me.cboNetMarketing, .NetMarketingId)
            Me.SetSelectedItem(Me.cboNetTaxes, .NetTaxesId)
            Me.SetSelectedItem(Me.cboRestrictMarkup_WRITE, .RestrictMarkupId)

            'Def-25991:Added condition to check null value for .AllowCoverageMarkupDistribution.
            If (.AllowCoverageMarkupDistribution <> Guid.Empty) Then
                Me.SetSelectedItem(Me.ddlAllowCoverageMarkupDistribution, .AllowCoverageMarkupDistribution)
            End If

            Me.SetSelectedItem(Me.cboTypeOfEquipment, .TypeOfEquipmentId)
            Me.SetSelectedItem(Me.cboTypeOfIns, .TypeOfInsuranceId)
            Me.SetSelectedItem(Me.cboTypeOfMarketing, .TypeOfMarketingId)
            Me.SetSelectedItem(Me.cboAutoSetLiability, .AutoSetLiabilityId)
            Me.SetSelectedItem(Me.cboCovDeductible, .CoverageDeductibleId)
            Me.SetSelectedItem(Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD, .IgnoreWaitingPeriodWsdPsd)
            Me.SetSelectedItem(Me.cboInstallmentPayment, .InstallmentPaymentId)
            Me.SetSelectedItem(Me.cboDeductible_By_Manufacturer, .DeductibleByManufacturerId)

            Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, .DeductibleBasedOnId)
            If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then
                Me.SetSelectedItem(Me.cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__FIXED))
            ElseIf deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION Then
                Me.SetSelectedItem(Me.cboDeductibleBasedOn, .DeductibleExpressionId)
            Else
                Me.SetSelectedItem(Me.cboDeductibleBasedOn, .DeductibleBasedOnId)
            End If

            Me.SetSelectedItem(Me.cboPayOutstandingAmount, .PayOutstandingPremiumId)

            EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, False, False)

            If Not Me.State.MyBO.IsNew Then
                Me.SetSelectedItem(Me.cboReplacementPolicy, .ReplacementPolicyId)
            End If

            If cboCancellationReason.Items.Count > 1 Then
                Me.SetSelectedItem(Me.cboCancellationReason, .CancellationReasonId)
            End If

            Me.SetSelectedItem(Me.cboID_VALIDATION, .ID_Validation_Id)
            Me.SetSelectedItem(Me.cboClaimControlID, .ClaimControlID)
            Me.SetSelectedItem(Me.cboIgnorePremium, .IgnoreIncomingPremiumID)
            Me.SetSelectedItem(Me.cboAcselProdCode, .Acsel_Prod_Code_Id)
            Me.SetSelectedItem(Me.cboIgnoreCovAmt, .IgnoreCoverageAmtId)
            Me.SetSelectedItem(Me.cboEDIT_MFG_TERM, .EditMFGTermId)

            If .EditMFGTermId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE)) Then
                ControlMgr.SetVisibleControl(Me, Me.LabelOverrideEditMfgTerm, False)
                ControlMgr.SetVisibleControl(Me, Me.cboOverrideEditMfgTerm, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.LabelOverrideEditMfgTerm, True)
                ControlMgr.SetVisibleControl(Me, Me.cboOverrideEditMfgTerm, True)
            End If

            BindSelectItem(.OverrideEditMfgTerm, Me.cboOverrideEditMfgTerm)

            Me.SetSelectedItem(Me.cboBackEndClaimAllowed, .BackEndClaimsAllowedId)

            Me.SetSelectedItem(Me.cboCOINSURANCE, .CoinsuranceId)
            Me.PopulateControlFromBOProperty(Me.TextboxPARTICIPATION_PERCENT, .ParticipationPercent)
            Me.PopulateControlFromBOProperty(Me.TextboxRatingPlan, .RatingPlan)

            Me.PopulateControlFromBOProperty(Me.TextboxRepairDiscountPct, .RepairDiscountPct)
            Me.PopulateControlFromBOProperty(Me.TextboxReplacementDiscountPct, .ReplacementDiscountPct)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_COINSURANCE, .CoinsuranceId).Equals(Me.DIRECT) Then
                Me.TextboxPARTICIPATION_PERCENT.Enabled = False
                Me.PopulateControlFromBOProperty(Me.TextboxPARTICIPATION_PERCENT, New DecimalType(100), Me.DECIMAL_FORMAT)
            Else
                Me.TextboxPARTICIPATION_PERCENT.Enabled = True
            End If
            Me.HiddenPARTICIPATION_PERCENTAG.Value = Me.TextboxPARTICIPATION_PERCENT.Text

            Me.SetSelectedItem(Me.cboCURRENCY_CONVERSION, .CurrencyConversionId)
            Me.SetSelectedItem(Me.cboCURRENCY_OF_COVERAGES, .CurrencyOfCoveragesId)

            Me.PopulateControlFromBOProperty(Me.txtNumOfClaims, .NumOfClaims)
            Me.PopulateControlFromBOProperty(Me.txtNumOfRepairClaims, .NumOfRepairClaims)
            Me.PopulateControlFromBOProperty(Me.txtNumOfReplClaims, .NumOfReplacementClaims)

            Me.PopulateControlFromBOProperty(Me.txtPenaltyPct, .PenaltyPct)
            Me.PopulateControlFromBOProperty(Me.txtAuthorizedAmountMaxUpdates, .AuthorizedAmountMaxUpdates)
            ' DEF-24575 Start
            Dim arrCompanies As New ArrayList
            Dim dv As DataView
            arrCompanies.Add(Me.State.Company_ID)
            dv = AcctBusinessUnit.getList(AcctCompany.GetAccountingCompanies(arrCompanies)(0).Id, Nothing)
            If dv.Count > 1 Then
                'DEF-25009 Start.
                'Added condition to show the text selected as ALL in the Accounting Business Unit Drop down of Contract Screen.
                If (Me.cboAcctBusinessUnit.Items(0).Text = String.Empty) Then
                    Me.cboAcctBusinessUnit.Items.RemoveAt(0)
                    Me.cboAcctBusinessUnit.Items.Insert(0, New ListItem("ALL", Guid.Empty.ToString))
                End If
                If (.AcctBusinessUnitId <> Guid.Empty) Then
                    Me.SetSelectedItem(Me.cboAcctBusinessUnit, .AcctBusinessUnitId)
                Else
                    Me.SetSelectedItem(Me.cboAcctBusinessUnit, Guid.Empty.ToString)
                End If
                'DEF-25009 End
            End If
            ' DEF-24575 End
            Me.SetSelectedItem(Me.cboIsCommPCodeId, .IsCommPCodeId)

            Me.SetSelectedItem(Me.cboBaseInstallments, .BaseInstallments)
            Me.SetSelectedItem(Me.cboBillingCycleFrequency, .BillingCycleFrequency)
            Me.PopulateControlFromBOProperty(Me.TextboxInstallmentsBaseReducer, .InstallmentsBaseReducer)
            Me.PopulateControlFromBOProperty(Me.TextboxMaxNumofInstallments, .MaxInstallments)
            Me.PopulateControlFromBOProperty(Me.txtCollectionReAttempts, .CollectionReAttempts)
            Me.PopulateControlFromBOProperty(Me.txtPastDueMonthsAllowed, .PastDueMonthsAllowed)
            Me.PopulateControlFromBOProperty(Me.TextboxCycleDay, .CycleDay)
            Me.PopulateControlFromBOProperty(Me.TextboxOffsetBeforeDueDate, .OffsetBeforeDueDate)
            Me.SetSelectedItem(Me.cboCollectionCycleType, .CollectionCycleTypeId)
            Me.SetSelectedItem(Me.cboIncludeFirstPmt, .IncludeFirstPmt)
            Me.SetSelectedItem(Me.cboExtendCoverage, .ExtendCoverageId)
            Me.SetSelectedItem(Me.ddlAllowPymtSkipMonths, .AllowPymtSkipMonths)
            Me.SetSelectedItem(Me.cboReplacementBasedOn, .ClaimLimitBasedOnId)
            Me.SetSelectedItem(Me.cboAllowDifferentCoverage, .AllowDifferentCoverage)
            'Added from Req-635
            Me.SetSelectedItem(Me.cboAllowNoExtended, .AllowNoExtended)
            'Req-703 Start
            Me.SetSelectedItem(Me.cboMarketingPromo, .MarketingPromotionId)
            'Req-703 End

            Me.SetSelectedItem(Me.cboProRataMethodId, .ProRataMethodId)

            Me.SetSelectedItem(Me.cboAllowMultipleRejections, .AllowMultipleRejectionsId)
            'Req-703 End
            Me.PopulateControlFromBOProperty(Me.txtDaysToReportClaim, .DaysToReportClaim)

            ''REQ-794
            'The below lines of code is commented for 1861 defect.
            'If Not Me.State.MyBO.IsNew Then
            '    Me.PopulateControlFromBOProperty(Me.cboIgnoreCovRate, .IgnoreCoverageRateId)
            'End If
            'REQ-5773 Start
            Me.PopulateControlFromBOProperty(Me.ddlPaymentProcessingTypeId, .PaymentProcessingTypeId)
            Me.PopulateControlFromBOProperty(Me.txtThirdPartyName, .ThirdPartyName)
            Me.PopulateControlFromBOProperty(Me.txtThirdPartyTaxId, .ThirdPartyTaxId)
            Me.PopulateControlFromBOProperty(Me.txtRdoName, .RdoName)
            Me.PopulateControlFromBOProperty(Me.txtRdoTaxId, .RdoTaxId)
            Me.PopulateControlFromBOProperty(Me.txtRdoPercent, .RdoPercent, Me.PERCENT_FORMAT)
            'REQ-5773 End

            'REQ-1050 start
            Me.PopulateControlFromBOProperty(Me.txtbxDaysToReactivate, .DaysToReactivate)
            'REQ-1050 END

            'REQ-1073
            Me.SetSelectedItem(Me.ddlBillingCycleType, .BillingCycleTypeId)
            Me.SetSelectedItem(Me.ddlDailyRateBasedOn, .DailyRateBasedOnId)

            'REQ-1005
            Me.SetSelectedItem(Me.ddlAllowBillingAfterCancellation, .AllowBillingAfterCancellation)
            Me.SetSelectedItem(Me.ddlAllowCollectionAfterCancellation, .AllowCollectionAfterCancellation)

            'REQ-1333
            Me.PopulateControlFromBOProperty(Me.txtReplacementPolicyCliamCount, .ReplacementPolicyClaimCount)

            'REQ-1198
            Me.PopulateControlFromBOProperty(Me.cboFutureDateAllowFor, .FutureDateAllowForID)

            'REQ-5372
            If Me.State.RepPolicyList Is Nothing Then
                Me.State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(Me.State.MyBO.Id)
            End If
            PopulateReplacementPolicyGrid(Me.State.RepPolicyList)
            If State.DepreciationScheduleList Is Nothing Then
                State.DepreciationScheduleList = DepreciationScdRelation.GetDepreciationScheduleList(State.MyBO.Id)
            End If
            PopulateDepreciationScheduleGrid(State.DepreciationScheduleList)

            Me.SetSelectedItem(Me.ddlProducer, .ProducerId)

            ' US - 489857
            PopulateSourceDropdownBucketFromBOs()
        End With

    End Sub

    Private Sub EnableDisableDeductible(ByVal pCoverageDeductibleId As Guid, ByVal pDeductibleBasedOnId As Guid,
        ByVal pClearValues As Boolean, ByVal pClearDeductibleBasedOnId As Boolean)
        Dim sCoverageDeductibleCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, pCoverageDeductibleId)
        Dim sDeductibleBasedOnCode As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Select Case sCoverageDeductibleCode
            Case YES
                ControlMgr.SetEnableControl(Me, Me.TextboxDeductible, False)
                ControlMgr.SetEnableControl(Me, Me.TextboxDeductiblePercent, False)
                ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, False)
                If (pClearValues) Then
                    Me.TextboxDeductible.Text = "0"
                    Me.TextboxDeductiblePercent.Text = "0"
                    ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End If
            Case NO
                ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, True)
                If (pClearDeductibleBasedOnId) Then
                    ElitaPlusPage.SetSelectedItem(cboDeductibleBasedOn, LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End If
                sDeductibleBasedOnCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, pDeductibleBasedOnId)
                If (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                    ' orelse (String.IsNullOrWhiteSpace(sDeductibleBasedOnCode)))

                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductible, True)
                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductiblePercent, False)
                    If (pClearValues) Then
                        Me.TextboxDeductiblePercent.Text = "0"
                    End If
                ElseIf ((sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                        (sDeductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD)) Then
                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductible, False)
                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductiblePercent, True)
                    If (pClearValues) Then
                        Me.TextboxDeductible.Text = "0"
                    End If
                Else
                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductible, False)
                    ControlMgr.SetEnableControl(Me, Me.TextboxDeductiblePercent, False)
                    ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, True)
                    If (pClearValues) Then
                        Me.TextboxDeductible.Text = "0"
                        Me.TextboxDeductiblePercent.Text = "0"
                    End If
                End If
            Case Else
                ControlMgr.SetEnableControl(Me, Me.TextboxDeductible, False)
                ControlMgr.SetEnableControl(Me, Me.TextboxDeductiblePercent, False)
                ControlMgr.SetEnableControl(Me, Me.cboDeductibleBasedOn, False)
                If (pClearValues) Then
                    Me.TextboxDeductible.Text = "0"
                    Me.TextboxDeductiblePercent.Text = "0"
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
        If ((Not Me.GetSelectedItem(Me.cboRecurringPremium).Equals(Guid.Empty) And
                Not Me.GetSelectedItem(Me.cboRecurringPremium).Equals(singlePremiumId)) _
                    And Me.GetSelectedItem(Me.cboInstallmentPayment).Equals(YesId)) Then
            'Req - 1016 - end
            If Me.GetSelectedItem(Me.cboIncludeFirstPmt).Equals(includeFirstPremiumNoId) Then
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
        sval = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.GetSelectedItem(Me.cboInstallmentPayment))
        'Req - 1016 Start
        'Dim currMonthlyBilling As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.GetSelectedItem(Me.cboRecurringPremium))
        Dim currMonthlyBilling As String
        Dim sPeriodRenew As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, Me.GetSelectedItem(Me.cboRecurringPremium))
        If sPeriodRenew Is Nothing Or sPeriodRenew = "0" Then
            currMonthlyBilling = "N"
        End If
        'Req - 1016 End
        Select Case sval
            Case YES
                ControlMgr.SetVisibleControl(Me, Me.TextboxDaysOfFirstPymt, True)
                ControlMgr.SetVisibleControl(Me, Me.LabelDaysOfFirstPymt, True)
                Me.moInstallmentBillingInformation0.Attributes("style") = ""
                Me.moInstallmentBillingInformation4.Attributes("style") = ""
                Me.moInstallmentBillingInformation5.Attributes("style") = ""

                If Me.State.DealerBO Is Nothing Then
                    Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
                End If

                If Me.State.DealerBO.DealerTypeDesc = Me.State.DealerBO.DEALER_TYPE_DESC OrElse Me.State.MyBO.InstallmentPaymentId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)) Then
                    Me.moInstallmentBillingInformation1.Attributes("style") = ""
                    Me.moInstallmentBillingInformation2.Attributes("style") = ""
                    Me.moInstallmentBillingInformation3.Attributes("style") = ""

                    If Me.State.MyBO.IsNew Then
                        Me.TextboxInstallmentsBaseReducer.Text = ZERO_STRING
                        Me.TextboxMaxNumofInstallments.Text = ONE_STRING
                        If Me.State.DealerBO.DealerTypeDesc = Me.State.DealerBO.DEALER_TYPE_DESC Then
                            Me.txtCollectionReAttempts.Text = ZERO_STRING
                        End If
                        Me.txtPastDueMonthsAllowed.Text = ZERO_STRING
                    Else
                        If Me.State.MyBO.InstallmentsBaseReducer Is Nothing OrElse Not Me.State.MyBO.InstallmentsBaseReducer.Value > 0 Then
                            Me.TextboxInstallmentsBaseReducer.Text = ZERO_STRING
                        Else
                            Me.PopulateControlFromBOProperty(Me.TextboxInstallmentsBaseReducer, Me.State.MyBO.InstallmentsBaseReducer)
                        End If
                        If Me.State.MyBO.MaxInstallments Is Nothing OrElse Not Me.State.MyBO.MaxInstallments.Value > 0 Then
                            Me.TextboxMaxNumofInstallments.Text = ONE_STRING
                        Else
                            Me.PopulateControlFromBOProperty(Me.TextboxMaxNumofInstallments, Me.State.MyBO.MaxInstallments)
                        End If
                        If Me.State.MyBO.CollectionReAttempts Is Nothing OrElse Not Me.State.MyBO.CollectionReAttempts.Value > 0 Then
                            Me.txtCollectionReAttempts.Text = ZERO_STRING
                        Else
                            Me.PopulateControlFromBOProperty(Me.txtCollectionReAttempts, Me.State.MyBO.CollectionReAttempts)
                        End If
                        If Me.State.MyBO.PastDueMonthsAllowed Is Nothing OrElse Not Me.State.MyBO.PastDueMonthsAllowed.Value > 0 Then
                            Me.txtPastDueMonthsAllowed.Text = ZERO_STRING
                        Else
                            Me.PopulateControlFromBOProperty(Me.txtPastDueMonthsAllowed, Me.State.MyBO.PastDueMonthsAllowed)
                        End If
                    End If
                Else

                    Me.moInstallmentBillingInformation1.Attributes("style") = "display: none"
                    Me.moInstallmentBillingInformation2.Attributes("style") = "display: none"
                    Me.moInstallmentBillingInformation3.Attributes("style") = "display: none"

                    Me.txtCollectionReAttempts.Text = Nothing
                    Me.txtPastDueMonthsAllowed.Text = Nothing
                    Me.TextboxInstallmentsBaseReducer.Text = Nothing
                    Me.TextboxMaxNumofInstallments.Text = Nothing

                End If

                If currMonthlyBilling = NO Then
                    ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, True)
                    ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, True)
                End If
            Case Else
                ControlMgr.SetVisibleControl(Me, Me.TextboxDaysOfFirstPymt, False)
                ControlMgr.SetVisibleControl(Me, Me.LabelDaysOfFirstPymt, False)
                Me.moInstallmentBillingInformation0.Attributes("style") = "display: none"
                Me.moInstallmentBillingInformation1.Attributes("style") = "display: none"
                Me.moInstallmentBillingInformation2.Attributes("style") = "display: none"
                Me.moInstallmentBillingInformation3.Attributes("style") = "display: none"
                Me.moInstallmentBillingInformation4.Attributes("style") = "display: none"
                Me.moInstallmentBillingInformation5.Attributes("style") = "display: none"
                Me.txtCollectionReAttempts.Text = Nothing
                Me.txtPastDueMonthsAllowed.Text = Nothing
                Me.TextboxInstallmentsBaseReducer.Text = Nothing
                Me.TextboxMaxNumofInstallments.Text = Nothing
                Me.TextboxOffsetBeforeDueDate.Text = Nothing
                Me.TextboxCycleDay.Text = Nothing
                Me.cboCollectionCycleType.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                ControlMgr.SetVisibleControl(Me, LabelPayOutstandingAmount, False)
                ControlMgr.SetVisibleControl(Me, cboPayOutstandingAmount, False)
                Me.cboPayOutstandingAmount.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
        End Select
    End Sub

    Protected Sub PopulateBOsFormFrom()
        Dim sval As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "AdminExpense", Me.TextboxAdminExpense)
            Me.PopulateBOProperty(Me.State.MyBO, "AutoMfgCoverageId", Me.cboAutoMFG)
            Me.PopulateBOProperty(Me.State.MyBO, "CancellationDays", Me.TextboxDaysToCancel)
            '.CertificatesAutonumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            Me.PopulateBOProperty(Me.State.MyBO, "CommissionsPercent", Me.TextboxCommPercent)
            Me.PopulateBOProperty(Me.State.MyBO, "ContractTypeId", Me.cboContractType)

            ' Individual Policy Req fields
            Me.PopulateBOProperty(Me.State.MyBO, "PolicyTypeId", cboPolicyType)
            Me.PopulateBOProperty(Me.State.MyBO, "PolicyGenerationId", cboCollectivePolicyGeneration)
            Me.PopulateBOProperty(Me.State.MyBO, "LineOfBusinessId", cboLineOfBusiness)
            '-----------------------------------------------------------

            Me.PopulateBOProperty(Me.State.MyBO, "CurrencyId", Me.cboCurrency)
            Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.TheDealerControl.SelectedGuid)
            ' Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.cboDealer_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "DealerMarkupId", Me.cboDealerMarkup_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "Deductible", Me.TextboxDeductible)
            Me.PopulateBOProperty(Me.State.MyBO, "DeductiblePercent", Me.TextboxDeductiblePercent)

            Dim deductibleBasedOnId As Guid = GetSelectedItem(Me.cboDeductibleBasedOn)

            If (deductibleBasedOnId = Guid.Empty) Then
                Me.PopulateBOProperty(Me.State.MyBO, "DeductibleBasedOnId", Me.cboDeductibleBasedOn)
                Me.PopulateBOProperty(Me.State.MyBO, "DeductibleExpressionId", Guid.Empty)
            Else

                Dim deductibleBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, deductibleBasedOnId)

                If (String.IsNullOrWhiteSpace(deductibleBasedOnCode)) Then

                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Codes.DEDUCTIBLE_BASED_ON__EXPRESSION))
                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleExpressionId", deductibleBasedOnId)

                ElseIf (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__FIXED) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) OrElse
                        (deductibleBasedOnCode = Codes.DEDUCTIBLE_BASED_ON__COMPUTED_EXTERNALLY) Then

                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleBasedOnId", Me.cboDeductibleBasedOn)
                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleExpressionId", Guid.Empty)
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleBasedOnId", Me.cboDeductibleBasedOn)
                    Me.PopulateBOProperty(Me.State.MyBO, "DeductibleExpressionId", deductibleBasedOnId)
                End If

            End If

            Me.PopulateBOProperty(Me.State.MyBO, "EditModelId", Me.cboEditModel)
            Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.TextboxStartDate_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "FixedEscDurationFlag", Me.cboFixedEscDurationFlag)
            Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.TextboxEndDate_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "FundingSourceId", Me.cboFundingSource)
            'Me.PopulateBOProperty(Me.State.MyBO, "LastRecon", Me.TextboxLastReconDate)
            Me.PopulateBOProperty(Me.State.MyBO, "Layout", Me.TextboxLayout)
            Me.PopulateBOProperty(Me.State.MyBO, "FullRefundDays", Me.TextboxFullRefundDays)
            Me.PopulateBOProperty(Me.State.MyBO, "LossCostPercent", Me.TextboxLossCostPercent)
            Me.PopulateBOProperty(Me.State.MyBO, "MarketingPercent", Me.TextboxMarketingExpense)
            Me.PopulateBOProperty(Me.State.MyBO, "MinReplacementCost", Me.TextboxMinRepCost)
            'Req-1016 - Start
            'Me.PopulateBOProperty(Me.State.MyBO, "MonthlyBillingId", Me.cboMonthlyBilling)
            Me.PopulateBOProperty(Me.State.MyBO, "RecurringPremiumId", Me.cboRecurringPremium)
            Me.PopulateBOProperty(Me.State.MyBO, "RecurringWarrantyPeriod", Me.txtPeridiocBillingWarntyPeriod)
            'Req-1016 - End
            Me.PopulateBOProperty(Me.State.MyBO, "NetCommissionsId", Me.cboNetCommission)
            Me.PopulateBOProperty(Me.State.MyBO, "NetMarketingId", Me.cboNetMarketing)
            Me.PopulateBOProperty(Me.State.MyBO, "NetTaxesId", Me.cboNetTaxes)
            Me.PopulateBOProperty(Me.State.MyBO, "Policy", Me.TextboxPolicyNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "ProfitPercent", Me.TextboxProfitExpense)
            Me.PopulateBOProperty(Me.State.MyBO, "RestrictMarkupId", Me.cboRestrictMarkup_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "AllowCoverageMarkupDistribution", Me.ddlAllowCoverageMarkupDistribution)
            Me.PopulateBOProperty(Me.State.MyBO, "SuspenseDays", Me.TextboxDaysToSuspend)
            Me.PopulateBOProperty(Me.State.MyBO, "TypeOfEquipmentId", Me.cboTypeOfEquipment)
            Me.PopulateBOProperty(Me.State.MyBO, "TypeOfInsuranceId", Me.cboTypeOfIns)
            Me.PopulateBOProperty(Me.State.MyBO, "TypeOfMarketingId", Me.cboTypeOfMarketing)
            Me.PopulateBOProperty(Me.State.MyBO, "WaitingPeriod", Me.TextboxWaitingPeriod)
            Me.PopulateBOProperty(Me.State.MyBO, "WarrantyMaxDelay", Me.TextboxWarrantyMaxDelay)
            Me.PopulateBOProperty(Me.State.MyBO, "RemainingMFGDays", Me.TextBoxRemainingMFGDays)

            Me.PopulateBOProperty(Me.State.MyBO, "ReplacementPolicyId", Me.cboReplacementPolicy)
            'REQ-1333
            Me.PopulateBOProperty(Me.State.MyBO, "ReplacementPolicyClaimCount", Me.txtReplacementPolicyCliamCount)

            Me.PopulateBOProperty(Me.State.MyBO, "CancellationReasonId", Me.cboCancellationReason)
            Me.PopulateBOProperty(Me.State.MyBO, "ID_Validation_Id", Me.cboID_VALIDATION)
            Me.PopulateBOProperty(Me.State.MyBO, "Acsel_Prod_Code_Id", Me.cboAcselProdCode)
            Me.PopulateBOProperty(Me.State.MyBO, "ClaimControlID", Me.cboClaimControlID)
            Me.PopulateBOProperty(Me.State.MyBO, "IgnoreIncomingPremiumID", Me.cboIgnorePremium)
            Me.PopulateBOProperty(Me.State.MyBO, "IgnoreCoverageAmtId", Me.cboIgnoreCovAmt)
            Me.PopulateBOProperty(Me.State.MyBO, "EditMFGTermId", Me.cboEDIT_MFG_TERM)
            Me.PopulateBOProperty(Me.State.MyBO, "BackEndClaimsAllowedId", Me.cboBackEndClaimAllowed)
            Me.PopulateBOProperty(Me.State.MyBO, "IgnoreWaitingPeriodWsdPsd", Me.cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD)
            Me.PopulateBOProperty(Me.State.MyBO, "InstallmentPaymentId", Me.cboInstallmentPayment)
            Me.PopulateBOProperty(Me.State.MyBO, "CustmerAddressRequiredId", Me.cboCustAddressRequired)
            Me.PopulateBOProperty(Me.State.MyBO, "FirstPymtMonths", Me.txtFirstPaymentMonths)

            Me.PopulateBOProperty(Me.State.MyBO, "DeductibleByManufacturerId", Me.cboDeductible_By_Manufacturer)
            Me.PopulateBOProperty(Me.State.MyBO, "ClipPercent", Me.txtCLIPPct)
            Me.PopulateBOProperty(Me.State.MyBO, "IncludeFirstPmt", Me.cboIncludeFirstPmt)
            Me.PopulateBOProperty(Me.State.MyBO, "ExtendCoverageId", Me.cboExtendCoverage)
            Me.PopulateBOProperty(Me.State.MyBO, "ExtraDaysToExtendCoverage", Me.txtExtendCoverageByExtraDays)
            Me.PopulateBOProperty(Me.State.MyBO, "ExtraMonsToExtendCoverage", Me.txtExtendCoverageByExtraMonths)

            sval = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, .InstallmentPaymentId)
            If sval = YES Then
                Me.PopulateBOProperty(Me.State.MyBO, "DaysOfFirstPymt", Me.TextboxDaysOfFirstPymt)
                Me.PopulateBOProperty(Me.State.MyBO, "DaysToSendLetter", Me.TextboxDaysToSendLetter)
                Me.PopulateBOProperty(Me.State.MyBO, "DaysToCancelCert", Me.TextboxDaysToCancelCert)
                Me.PopulateBOProperty(Me.State.MyBO, "PastDueMonthsAllowed", Me.txtPastDueMonthsAllowed)
                If Me.State.DealerBO.DealerTypeDesc = Me.State.DealerBO.DEALER_TYPE_DESC Then
                    Me.PopulateBOProperty(Me.State.MyBO, "CollectionReAttempts", Me.txtCollectionReAttempts)
                End If
            Else
                Me.PopulateBOProperty(Me.State.MyBO, "DaysOfFirstPymt", "0")
                Me.PopulateBOProperty(Me.State.MyBO, "DaysToSendLetter", "0")
                Me.PopulateBOProperty(Me.State.MyBO, "DaysToCancelCert", "0")
                Me.State.MyBO.PastDueMonthsAllowed = Nothing
                Me.State.MyBO.CollectionReAttempts = Nothing
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "FutureDateAllowForID", Me.cboFutureDateAllowFor)

            Me.PopulateBOProperty(Me.State.MyBO, "CoinsuranceId", Me.cboCOINSURANCE)
            Me.PopulateBOProperty(Me.State.MyBO, "ParticipationPercent", Me.TextboxPARTICIPATION_PERCENT)
            Me.PopulateBOProperty(Me.State.MyBO, "RatingPlan", Me.TextboxRatingPlan)

            Me.PopulateBOProperty(Me.State.MyBO, "CurrencyConversionId", Me.cboCURRENCY_CONVERSION)
            Me.PopulateBOProperty(Me.State.MyBO, "CurrencyOfCoveragesId", Me.cboCURRENCY_OF_COVERAGES)
            Me.PopulateBOProperty(Me.State.MyBO, "AutoSetLiabilityId", Me.cboAutoSetLiability)
            Me.PopulateBOProperty(Me.State.MyBO, "CoverageDeductibleId", Me.cboCovDeductible)
            Me.PopulateBOProperty(Me.State.MyBO, "PayOutstandingPremiumId", Me.cboPayOutstandingAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "AuthorizedAmountMaxUpdates", Me.txtAuthorizedAmountMaxUpdates)

            Me.PopulateBOProperty(Me.State.MyBO, "RepairDiscountPct", Me.TextboxRepairDiscountPct)
            Me.PopulateBOProperty(Me.State.MyBO, "ReplacementDiscountPct", Me.TextboxReplacementDiscountPct)

            Me.PopulateBOProperty(Me.State.MyBO, "NumOfClaims", Me.txtNumOfClaims)
            Me.PopulateBOProperty(Me.State.MyBO, "NumOfRepairClaims", Me.txtNumOfRepairClaims)
            Me.PopulateBOProperty(Me.State.MyBO, "NumOfReplacementClaims", Me.txtNumOfReplClaims)

            Me.PopulateBOProperty(Me.State.MyBO, "PenaltyPct", Me.txtPenaltyPct)
            Me.PopulateBOProperty(Me.State.MyBO, "AcctBusinessUnitId", Me.cboAcctBusinessUnit)
            Me.PopulateBOProperty(Me.State.MyBO, "IsCommPCodeId", Me.cboIsCommPCodeId)
            Me.PopulateBOProperty(Me.State.MyBO, "ProducerId", Me.ddlProducer)
            Me.PopulateBOProperty(Me.State.MyBO, "BaseInstallments", Me.cboBaseInstallments)
            Me.PopulateBOProperty(Me.State.MyBO, "BillingCycleFrequency", Me.cboBillingCycleFrequency)
            Me.PopulateBOProperty(Me.State.MyBO, "InstallmentsBaseReducer", Me.TextboxInstallmentsBaseReducer)
            Me.PopulateBOProperty(Me.State.MyBO, "MaxInstallments", Me.TextboxMaxNumofInstallments)

            Me.PopulateBOProperty(Me.State.MyBO, "CollectionCycleTypeId", Me.cboCollectionCycleType)
            Me.PopulateBOProperty(Me.State.MyBO, "CycleDay", Me.TextboxCycleDay)
            Me.PopulateBOProperty(Me.State.MyBO, "OffsetBeforeDueDate", Me.TextboxOffsetBeforeDueDate)
            Me.PopulateBOProperty(Me.State.MyBO, "InsPremiumFactor", Me.txtInsPremFactor)
            Me.PopulateBOProperty(Me.State.MyBO, "ClaimLimitBasedOnId", Me.cboReplacementBasedOn)
            Me.PopulateBOProperty(Me.State.MyBO, "AllowDifferentCoverage", Me.cboAllowDifferentCoverage)
            'Added for Req-635
            Me.PopulateBOProperty(Me.State.MyBO, "AllowNoExtended", Me.cboAllowNoExtended)

            Me.PopulateBOProperty(Me.State.MyBO, "DaysToReportClaim", Me.txtDaysToReportClaim)

            'Req-703 Start
            Me.PopulateBOProperty(Me.State.MyBO, "MarketingPromotionId", Me.cboMarketingPromo)
            'Req-703 End

            Me.PopulateBOProperty(Me.State.MyBO, "ProRataMethodId", Me.cboProRataMethodId)

            Me.PopulateBOProperty(Me.State.MyBO, "AllowMultipleRejectionsId", Me.cboAllowMultipleRejections)

            Me.PopulateBOProperty(Me.State.MyBO, "AllowPymtSkipMonths", Me.ddlAllowPymtSkipMonths)
            ''REQ-794
            'The below line of is commented for the def1861
            'Me.PopulateBOProperty(Me.State.MyBO, "IgnoreCoverageRateId", Me.cboIgnoreCovRate)

            'REQ-1050 start
            Me.PopulateBOProperty(Me.State.MyBO, "DaysToReactivate", Me.txtbxDaysToReactivate)
            'REQ-1050 END

            'REQ-1073
            Me.PopulateBOProperty(Me.State.MyBO, "DailyRateBasedOnId", Me.ddlDailyRateBasedOn)
            Me.PopulateBOProperty(Me.State.MyBO, "BillingCycleTypeId", Me.ddlBillingCycleType)

            'REQ-1005
            Me.PopulateBOProperty(Me.State.MyBO, "AllowBillingAfterCancellation", Me.ddlAllowBillingAfterCancellation)
            Me.PopulateBOProperty(Me.State.MyBO, "AllowCollectionAfterCancellation", Me.ddlAllowCollectionAfterCancellation)

            'REQ-5773 Start
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentProcessingTypeId", Me.ddlPaymentProcessingTypeId)
            Me.PopulateBOProperty(Me.State.MyBO, "ThirdPartyName", Me.txtThirdPartyName)
            Me.PopulateBOProperty(Me.State.MyBO, "ThirdPartyTaxId", Me.txtThirdPartyTaxId)
            Me.PopulateBOProperty(Me.State.MyBO, "RdoName", Me.txtRdoName)
            Me.PopulateBOProperty(Me.State.MyBO, "RdoTaxId", Me.txtRdoTaxId)
            Me.PopulateBOProperty(Me.State.MyBO, "RdoPercent", Me.txtRdoPercent)
            'REQ-5773 End

            Me.PopulateBOProperty(Me.State.MyBO, "OverrideEditMfgTerm", Me.cboOverrideEditMfgTerm, False, True)

            ''# US 489857
            'PoupulateBOsFromSourceDropDownBucket()

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub SetDefaultDates()
        Dim selectedDealerId As Guid = Me.TheDealerControl.SelectedGuid 'GetSelectedItem(Me.cboDealer_WRITE)
        Me.State.DealerID = selectedDealerId
        If Not selectedDealerId.Equals(Guid.Empty) Then
            Dim defaultDates As Contract.StartEndDates = Contract.GetNewDefaultDates(selectedDealerId)
            With defaultDates
                Me.PopulateControlFromBOProperty(Me.TextboxStartDate_WRITE, .StartDate)
                Me.PopulateControlFromBOProperty(Me.TextboxEndDate_WRITE, .EndDate)
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
            If Me.State.MyBO.IsNew Then
                Dim objCompany As New Company(oDealer.CompanyId)
                Dim objCountry As New Country(objCompany.CountryId)
                Me.State.MyBO.CurrencyOfCoveragesId = objCountry.PrimaryCurrencyId
                Me.SetSelectedItem(Me.cboCURRENCY_OF_COVERAGES, objCountry.PrimaryCurrencyId)
                Me.State.Company_Type_ID = objCompany.CompanyTypeId
                Me.State.Company_ID = objCompany.Id
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New Contract
        PopulateDropdowns()
        ClearReplacementPolicyState()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        ClearDepreciationScheduleState()

    End Sub

    Protected Sub CreateNewWithCopy()

        ClearReplacementPolicyState()
        Me.PopulateBOsFormFrom()
        '# US 489857
        PoupulateBOsFromSourceDropDownBucket()
        Me.cboID_VALIDATION.SelectedIndex = Me.NOTHING_SELECTED
        Me.cboAcselProdCode.SelectedIndex = Me.NOTHING_SELECTED
        SetID_Validation_DDandAcsel_Prod_Code()
        ClearDepreciationScheduleState()
        'create the backup copy
        Me.State.ScreenSnapShotBO = New Contract
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Dim confAutoGenResponse As String = Me.HiddenCertificateAuoNumGenConfirmation.Value

        Me.HiddenCertificateAuoNumGenConfirmation.Value = ""
        If Not confAutoGenResponse Is Nothing AndAlso confAutoGenResponse = Me.MSG_VALUE_YES Then
            Me.State.DealerBO.CertificatesAutonumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
            Me.State.DealerBO.CertificatesAutonumberPrefix = Me.TextboxPolicyNumber.Text
            SaveContractRecord(Me.State.MyBO.IsNew)
        End If

        If Not confAutoGenResponse Is Nothing AndAlso confAutoGenResponse = Me.MSG_VALUE_NO Then
            Throw New GUIException(Message.MSG_CERT_AUTO_NUM_GEN_YES_IND_POL, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CERT_AUTO_NUM_GEN_YES_IND_POL)
        End If

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If

    End Sub
    Protected Sub SaveContractRecord(isNewContract As Boolean)
        If Me.State.MyBO.IsDirty Then

            Me.State.MyBO.Save()
            SaveReplacementPolicy(isNewContract) 'new contract, save Child records in memory
            Me.State.HasDataChanged = True

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields(True)
            cboCOINSURANCE_Code.Visible = False
            If Me.State.IsACopy = True Then
                Me.State.IsACopy = False
            End If
            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
        Else
            cboCOINSURANCE_Code.Visible = False
            Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
        End If
    End Sub
    Protected Sub PopulateLineOfBusinessDropDown(PolicyType? As Guid, populateNotInUsedLoB As Boolean)

        Try

            If Me.State.MyBO.DealerId.Equals(Guid.Empty) AndAlso Me.State.DealerBO Is Nothing Then
                Throw New GUIException(Message.MSG_GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_ID_FOR_LOB_NOT_FOUND)
                Return
            End If

            ' Get the country Id to get LoB
            If Me.State.MyBO.CountryId.Equals(Guid.Empty) AndAlso Not Me.State.CountryBO Is Nothing Then
                Me.State.MyBO.CountryId = Me.State.CountryBO.Id
            End If

            ' if country BO is not populated then populate contry BO
            If Me.State.MyBO.CountryId.Equals(Guid.Empty) Then
                Dim oCompany As Company = New Company(Me.State.DealerBO.CompanyId)
                Me.State.CountryBO = New Country(oCompany.BusinessCountryId)
                Me.State.MyBO.CountryId = Me.State.CountryBO.Id
            End If

            Dim textFun1 As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                         Return li.Code + " - " + li.Translation
                                                                     End Function
            Dim listcontextLoB As ListContext = New ListContext()

            listcontextLoB.CountryId = Me.State.MyBO.CountryId
            listcontextLoB.PolicyBusinessTypeId = PolicyType

            Dim lobListItems As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PolicyLineOfBusiness", Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextLoB)
            ' Dim inUseLoBListItems As DataElements.ListItem()

            ' Filter to only In Use Line Of Business
            If Not populateNotInUsedLoB Then
                lobListItems = lobListItems.ToList().Where(Function(n) n.ExtendedCode = "Y").ToArray()
            End If

            Me.cboLineOfBusiness.Populate(lobListItems, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .TextFunc = textFun1
                                                  })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Me.State.MyBO.DealerId = TheDealerControl.SelectedGuid
            populateDealer()

            'Reload the Dealer on new selection...
            Me.State.DealerBO = Nothing
            Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
            '-------------------------------
            
            'Rebind dropdown when dealer is changed
            BindSourceOptionDropdownlist()            
            PopulateSourcePercentageBucketValues()            
            '--------------------------------------

            SetDefaultDates()
            PopulateAccountBusinessUnit(Me.State.MyBO.DealerId)

            PopulateProducer()
            If (Me.State.MyBO.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(Me.State.MyBO.DealerId)
                If (oDealer.DealerTypeDesc = "VSC") Then
                    With Me.State.MyBO
                        .CoverageDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                        .DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.GetComputeDeductibleBasedOnAndExpressions(langId), Codes.DEDUCTIBLE_BASED_ON__FIXED)
                        Me.ChangeEnabledProperty(Me.cboCovDeductible, False)
                        EnableDisableDeductible(.CoverageDeductibleId, .DeductibleBasedOnId, True, False)
                        Me.ChangeEnabledProperty(Me.cboDeductibleBasedOn, False)

                        Me.PopulateControlFromBOProperty(Me.TextboxDeductible, .Deductible)
                        Me.PopulateControlFromBOProperty(Me.TextboxDeductiblePercent, .DeductiblePercent)
                        Me.SetSelectedItem(Me.cboCovDeductible, .CoverageDeductibleId)
                        Me.SetSelectedItem(Me.cboDeductibleBasedOn, .DeductibleBasedOnId)
                    End With
                Else
                    Me.ChangeEnabledProperty(Me.cboCovDeductible, True)
                    Me.ChangeEnabledProperty(Me.cboDeductibleBasedOn, True)
                End If

                ' Reset Ind. Policy control if dealer is re-selected in between. Move to method later.
                Me.cboLineOfBusiness.Items.Clear()
                Me.SetSelectedItem(Me.cboPolicyType, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE))
                Me.SetSelectedItem(Me.cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER))

                ' if Dealer is not selected in case of New then disable ind policy dropdowns.
                ControlMgr.SetEnableControl(Me, cboPolicyType, Not Me.State.MyBO.DealerId.Equals(Guid.Empty))
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, Not Me.State.MyBO.DealerId.Equals(Guid.Empty))

                ControlMgr.SetEnableControl(Me, cboLineOfBusiness, True)

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)
                TextboxPolicyNumber.Text = ""
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)
                '------------------------------------------

            Else
                Me.ChangeEnabledProperty(Me.cboCovDeductible, True)
                Me.ChangeEnabledProperty(Me.cboDeductibleBasedOn, True)
            End If

            'Me.BindListControlToDataView(Me.cboCancellationReason, LookupListNew.GetCancellationReasonByDealerIdLookupList(Me.State.MyBO.DealerId), "DESCRIPTION", "ID", True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = Me.State.MyBO.DealerId
            Dim CancellationReasonList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CancellationReasonsByDealer", context:=listcontext)
            Me.cboCancellationReason.Populate(CancellationReasonList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Handlers-DropDown"

    Protected Sub cboCovDeductible_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCovDeductible.SelectedIndexChanged
        Try
            Dim coverageDeductibleId As Guid = Guid.Empty
            Dim deductibleBasedOnId As Guid = Guid.Empty
            If Me.cboCovDeductible.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                coverageDeductibleId = GetSelectedItem(cboCovDeductible)
            End If
            If Me.cboDeductibleBasedOn.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                deductibleBasedOnId = GetSelectedItem(cboDeductibleBasedOn)
            End If
            EnableDisableDeductible(coverageDeductibleId, deductibleBasedOnId, True, True)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboDeductibleBasedOn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDeductibleBasedOn.SelectedIndexChanged
        Try
            Dim coverageDeductibleId As Guid = Guid.Empty
            Dim deductibleBasedOnId As Guid = Guid.Empty
            If Me.cboCovDeductible.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                coverageDeductibleId = GetSelectedItem(cboCovDeductible)
            End If
            If Me.cboDeductibleBasedOn.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                deductibleBasedOnId = GetSelectedItem(cboDeductibleBasedOn)
            End If
            EnableDisableDeductible(coverageDeductibleId, deductibleBasedOnId, True, False)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCollectionCycleType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCollectionCycleType.SelectedIndexChanged

        EnableDisableCycleDay()

    End Sub
    'REQ-5773 Start
    Protected Sub moPaymentProcessingTypeId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentProcessingTypeId.SelectedIndexChanged

        If Me.ddlPaymentProcessingTypeId.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            If GetSelectedItem(Me.ddlPaymentProcessingTypeId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.THIRD_PARTY_PAYMENT)) Then
                ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyName, True)
                ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyName, True)
                ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyTaxId, True)
                ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyTaxId, True)
            Else
                If GetSelectedItem(Me.ddlPaymentProcessingTypeId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PPT, Codes.DEALER_PAYMENT)) Then
                    ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyName, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyName, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblThirdPartyTaxId, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtThirdPartyTaxId, False)
                End If
            End If
        End If
    End Sub
    'REQ-5773 End

    Private Sub EnableDisableCycleDay()

        If Me.cboCollectionCycleType.SelectedValue = LookupListNew.GetIdFromCode(LookupListNew.GetCollectionCycleTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.VARIABLE_CYCLE_TYPE_CODE).ToString Then

            Me.TextboxCycleDay.Text = String.Empty
            Me.TextboxCycleDay.Enabled = False
        Else
            Me.TextboxCycleDay.Enabled = True

        End If

    End Sub
    Private Sub cboEDIT_MFG_TERM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEDIT_MFG_TERM.SelectedIndexChanged
        If Me.cboEDIT_MFG_TERM.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            If GetSelectedItem(Me.cboEDIT_MFG_TERM).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE)) Then
                ControlMgr.SetVisibleControl(Me, Me.LabelOverrideEditMfgTerm, False)
                ControlMgr.SetVisibleControl(Me, Me.cboOverrideEditMfgTerm, False)
                cboOverrideEditMfgTerm.SelectedIndex = Me.NOTHING_SELECTED
            Else
                ControlMgr.SetVisibleControl(Me, Me.LabelOverrideEditMfgTerm, True)
                ControlMgr.SetVisibleControl(Me, Me.cboOverrideEditMfgTerm, True)
            End If
        End If
    End Sub

    Protected Sub cboPolicyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPolicyType.SelectedIndexChanged

        If Me.cboPolicyType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

            Dim colPolicyTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)
            Dim indPolicyTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_INDIVIDUAL)

            If GetSelectedItem(Me.cboPolicyType).Equals(indPolicyTypeId) Then
                SetSelectedItem(cboCollectivePolicyGeneration, LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE))
                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, True)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, True)
                ControlMgr.SetEnableControl(Me, cboCollectivePolicyGeneration, False)

                PopulateLineOfBusinessDropDown(indPolicyTypeId, False)
            End If

            If GetSelectedItem(Me.cboPolicyType).Equals(colPolicyTypeId) Then
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

        If Me.cboCollectivePolicyGeneration.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then

            TextboxPolicyNumber.Text = ""
            ' Auto Generated
            If GetSelectedItem(Me.cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE)) Then

                If cboLineOfBusiness.Items.Count = 0 Then ' for new contract when LoB is not populated in case of default to CP
                    PopulateLineOfBusinessDropDown(GetSelectedItem(Me.cboPolicyType), False)
                End If

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, True)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, True)
                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, False)
            End If

            ' Manually Entered
            If GetSelectedItem(Me.cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_MANUALENTER)) Then

                ControlMgr.SetVisibleControl(Me, LabelLineOfBusiness, False)
                ControlMgr.SetVisibleControl(Me, cboLineOfBusiness, False)

                ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, True)

            End If

        End If
    End Sub
    Protected Sub cboLineOfBusiness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLineOfBusiness.SelectedIndexChanged

        If Me.cboLineOfBusiness.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then

            Me.ClearLabelError(Me.LabelLineOfBusiness)

            Dim subRamoCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRY_LINE_OF_BUSINESS, GetSelectedItem(Me.cboLineOfBusiness))

            TextboxPolicyNumber.Text = subRamoCode + DateTime.Today.Year.ToString().Substring(2, 2)

            'If it's collective policy then generate on Save with auto generated sequence no.
            If GetSelectedItem(Me.cboPolicyType).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_COLLECTIVE)) Then
                TextboxPolicyNumber.Text = "ToBeCreated"
            End If

            ControlMgr.SetEnableControl(Me, TextboxPolicyNumber, False)

        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            '# US 489857
            'PoupulateBOsFromSourceDropDownBucket()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
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

            If Me.cboCovDeductible.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboCovDeductible))
                If sVal = NO Then
                    If IsNumeric(TextboxDeductible.Text) And IsNumeric(TextboxDeductiblePercent.Text) Then
                        If CType(TextboxDeductible.Text, Decimal) > 0 And CType(TextboxDeductiblePercent.Text, Decimal) > 0 Then
                            'display error
                            ElitaPlusPage.SetLabelError(Me.LabelDeductible)
                            ElitaPlusPage.SetLabelError(Me.LabelDeductiblePercent)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_AMOUNT_ERR)
                        End If
                    End If
                End If
            End If

            If Me.cboFixedEscDurationFlag.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboFixedEscDurationFlag))
                sContractType = LookupListNew.GetCodeFromId(LookupListNew.LK_CONTRACT_TYPES, GetSelectedItem(cboContractType))
                If sContractType = CONTRACT_TYPE_EXTENSION Then
                    If sVal = YES Then
                        ElitaPlusPage.SetLabelError(Me.LabelFixedEscDurationFlag)
                        ElitaPlusPage.SetLabelError(Me.LabelContractType)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.FIXED_ESC_FLAG_CONTRACT_TYPE_ERR)
                    End If
                End If
            End If

            txtCLIPPct.Text = txtCLIPPct.Text.Trim
            txtInsPremFactor.Text = txtInsPremFactor.Text.Trim

            If Me.cboCollectionCycleType.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                sCollCycleType = LookupListNew.GetCodeFromId(LookupListNew.LK_COLLECTION_CYCLE_TYPE, GetSelectedItem(cboCollectionCycleType))
                If sCollCycleType = COLLECTION_CYCLE_TYPE_FIX Then
                    If Trim(TextboxCycleDay.Text) = String.Empty Then
                        ElitaPlusPage.SetLabelError(Me.LabelCycleDay)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.COLLECTION_CYCLE_DAY_ERR)
                    End If
                End If
            End If

            Me.PopulateBOsFormFrom()
            '# US 489857
            PoupulateBOsFromSourceDropDownBucket()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If Me.State.MyBO.DealerMarkupId.Equals(yesId) Then
                If Me.State.MyBO.IgnoreCoverageAmtId.Equals(yesId) Or Me.State.MyBO.IgnoreIncomingPremiumID.Equals(yesId) Then
                    ElitaPlusPage.SetLabelError(Me.LabelIgnoreCovAmt)
                    ElitaPlusPage.SetLabelError(Me.LabelIgnorePremium)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.SET_IGNORE_FLAGS_TO_NO_FOR_DEALERMARKUP_ERR)
                End If
            End If

            If Me.State.MyBO.IgnoreCoverageAmtId.Equals(yesId) And Me.State.MyBO.IgnoreCoverageAmtId.Equals(Me.State.MyBO.IgnoreIncomingPremiumID) Then
                ElitaPlusPage.SetLabelError(Me.LabelIgnoreCovAmt)
                ElitaPlusPage.SetLabelError(Me.LabelIgnorePremium)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.BOTH_IGNORE_FLAGS_CANNOT_BE_YES_ERR)
            End If

            If Me.cboEDIT_MFG_TERM.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If (Not GetSelectedItem(Me.cboEDIT_MFG_TERM).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EDTMFGTRM, Codes.EDIT_MFG_TERM__NONE))) And Me.cboOverrideEditMfgTerm.SelectedIndex <= 0 Then
                    ElitaPlusPage.SetLabelError(Me.LabelOverrideEditMfgTerm)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.OVERRIDE_EDIT_MFG_TERM_REQD_ERR)
                End If
            End If

            ' We need Dealer BO to update Certificate Auto number generation ID and prefix For IP
            If Me.State.DealerBO Is Nothing AndAlso Not Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
                Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
            End If

            ' Existing code
            If Me.State.MyBO.InstallmentPaymentId.Equals(yesId) Then
                If Me.State.DealerBO.DealerTypeDesc = Me.State.DealerBO.DEALER_TYPE_DESC Then

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

            If CType(Me.TextboxEndDate_WRITE.Text, Date) < Date.Today Then
                Throw New GUIException(Message.MSG_END_DATE_CANNOT_BE_PAST_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_END_DATE_ERR)
            End If

            ' if individual and autogenrated and dealer certificate auto number is Set to NO then alert user.
            If GetSelectedItem(Me.cboCollectivePolicyGeneration).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE)) Then

                If Me.GetSelectedItem(Me.cboPolicyType).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_INDIVIDUAL)) Then

                    If Me.State.DealerBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
                        Me.DisplayMessage(Message.MSG_CERT_AUTO_NUM_GEN_IS_NO, "Certificate Auto Generate Prefix", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenCertificateAuoNumGenConfirmation)
                        Return

                    ElseIf Me.State.DealerBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                        Me.State.DealerBO.CertificatesAutonumberPrefix = Me.TextboxPolicyNumber.Text
                    End If

                End If

            End If

            SaveContractRecord(Me.State.MyBO.IsNew) ' Save contract and other child records.

        Catch ex As Exception
            If Me.cboCovDeductible.SelectedItem.Text = NO Then
                Me.TextboxDeductible.Enabled = True
                Me.TextboxDeductiblePercent.Enabled = True
            End If

            '#US 489857
            SetSourceBucketValues()

            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Contract(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Contract
            End If
            PopulateDropdowns()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DeleteReplacementPolicy() 'delete Child records first
            DeleteDepreciationSchedule()
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            '# US 489857
            'PoupulateBOsFromSourceDropDownBucket()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            ' this should happen during post back before dialog message whether record is dirty or not.
            Me.PopulateBOsFormFrom()
            '# US 489857
            PoupulateBOsFromSourceDropDownBucket()
            TheDealerControl.NothingSelected = True
            populateDealer()
            TheDealerControl.SelectedGuid = Me.State.MyBO.DealerId
            SetDealerCode()

            If Not Me.State.MyBO.IsDirty Then
                Me.State.MyBO = New Contract
                Me.State.IsACopy = True
                Me.State.MyBO.DealerId = TheDealerControl.SelectedGuid
                Me.EnableDisableFields()
                Me.CreateNewWithCopy()
            Else
                Me.State.ScreenSnapShotBO = New Contract()
                Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO) ' cloning original BO to save later if user say yes.
                Me.State.MyBO = New Contract
                Me.State.IsACopy = True
                Me.State.MyBO.DealerId = TheDealerControl.SelectedGuid
                Me.EnableDisableFields()
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)

                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End If

            ' recapture the Dealer BO after new cloned contract BO.
            If Not Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
                Me.State.DealerBO = Me.State.MyBO.AddDealer(Me.State.MyBO.DealerId)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnTNC_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTNC.Click
        Try
            Me.callPage(Tables.TermAndConditionsForm.URL, Me.State.MyBO.Id)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Control Events"

    Private Sub cboInstallmentPayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInstallmentPayment.SelectedIndexChanged
        Try
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboInstallmentPayment, noId)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Exit Sub
            End If
            If cboInstallmentPayment.SelectedValue = YesId.ToString Then
                TextboxDaysOfFirstPymt.Text = ""
            End If
            EnableFirstPaymentMonthsField()
            ShowInstPymtFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboRecurringPremium_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRecurringPremium.SelectedIndexChanged
        Try
            Me.EnableDisableFields(True)
            'Req-1016 Start
            Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, Me.GetSelectedItem(Me.cboRecurringPremium))

            If sVal = Codes.PERIOD_RENEW__SINGLE_PREMIUM OrElse sVal = "" Then
                Me.txtPeridiocBillingWarntyPeriod.Text = Nothing
            End If
            'Req-1016 End
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboIncludeFirstPmt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIncludeFirstPmt.SelectedIndexChanged
        EnableFirstPaymentMonthsField()
    End Sub

    Private Sub cboDealerMarkup_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDealerMarkup_WRITE.SelectedIndexChanged
        Try
            Me.EnableDisableFields(True)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
            If Me.GetSelectedItem(Me.cboDealerMarkup_WRITE).Equals(yesId) AndAlso Me.GetSelectedItem(Me.cboCURRENCY_CONVERSION).Equals(yesId) Then
                Me.cboCURRENCY_CONVERSION.SelectedIndex = Me.NOTHING_SELECTED
                Me.DisplayMessage(Message.MSG_DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub cboCURRENCY_CONVERSION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCURRENCY_CONVERSION.SelectedIndexChanged
        Try
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
            If Me.GetSelectedItem(Me.cboCURRENCY_CONVERSION).Equals(yesId) AndAlso Me.GetSelectedItem(Me.cboDealerMarkup_WRITE).Equals(yesId) Then
                Me.cboDealerMarkup_WRITE.SelectedIndex = Me.NOTHING_SELECTED
                Me.cboRestrictMarkup_WRITE.SelectedIndex = Me.NOTHING_SELECTED
                Me.ddlAllowCoverageMarkupDistribution.SelectedIndex = Me.NOTHING_SELECTED
                Me.DisplayMessage(Message.MSG_DEALER_MARKUP_AND_CURRENCY_CONVERSION_CANNOT_BOTH_SET_TO_YES_ONE_WILL_BE_DESELECTED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CheckCoinsuranceDropDown()
        If Me.cboCOINSURANCE.SelectedIndex > 0 AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COINSURANCE, GetSelectedItem(Me.cboCOINSURANCE)).Equals(Me.DIRECT) Then
            Me.TextboxPARTICIPATION_PERCENT.Text = Me.HiddenPARTICIPATION_PERCENTAG.Value
            Me.TextboxPARTICIPATION_PERCENT.Enabled = False
        Else
            Me.TextboxPARTICIPATION_PERCENT.Enabled = True
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

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim nIndex As Integer
        Dim guidTemp As Guid
        Try
            If e.CommandName = Me.EDIT_COMMAND_NAME OrElse e.CommandName = Me.DELETE_COMMAND_NAME Then
                'guidTemp = New Guid(CType(moGridView.Rows(nIndex).FindControl("hiddenRepPolicyID"), HiddenField).Value)
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.RepPolicyList.FindIndex(Function(r) r.Id = guidTemp)
                State.RepPolicyWorkingItem = State.RepPolicyList.Item(nIndex)
            End If

            If e.CommandName = Me.EDIT_COMMAND_NAME Then
                moGridView.EditIndex = nIndex
                moGridView.SelectedIndex = nIndex
                State.RepPolicyAction = RepPolicy_Edit
                PopulateReplacementPolicyGrid(Me.State.RepPolicyList)
                'PopulateCoverageRateList(ACTION_EDIT)
                'PopulateCoverageRate()
                'Me.SetGridControls(moGridView, False)
                Me.SetFocusInGrid(moGridView, nIndex, Me.GRID_COL_CLAIM_COUNT_IDX)
                EnableDisableBtnsForRepPolicyGrid()
                'setbuttons(False)
            ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                State.RepPolicyAction = RepPolicy_Delete
                ReplacementPolicyDeleteRecord()
            ElseIf (e.CommandName = Me.SAVE_COMMAND_NAME) Then
                ReplacementPolicySaveRecord()
            ElseIf (e.CommandName = Me.CANCEL_COMMAND_NAME) Then
                ReplacementPolicyCancelRecord()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridView.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As ReppolicyClaimCount


            If Not e.Row.DataItem Is Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, ReppolicyClaimCount)
                    'edit item, populate dropdown and set value
                    If (State.RepPolicyAction = RepPolicy_Add OrElse State.RepPolicyAction = RepPolicy_Edit) AndAlso State.RepPolicyWorkingItem.Id = dvRow.Id Then
                        If Not dvRow.ReplacementPolicyClaimCount Is Nothing Then
                            CType(e.Row.Cells(Me.GRID_COL_CLAIM_COUNT_IDX).FindControl("txtClaimCount"), TextBox).Text = dvRow.ReplacementPolicyClaimCount.Value.ToString
                        End If

                        Dim objDDL As DropDownList
                        'set product code 
                        objDDL = CType(e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX).FindControl("ddlProductCode"), DropDownList)

                        'Dim dv As DataView = ProductCode.getListByDealer(State.MyBO.DealerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Guid.Empty)
                        'BindListTextToDataView(objDDL, dv, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, True, False)

                        Dim plistcontext As ListContext = New ListContext()
                        plistcontext.DealerId = Me.State.MyBO.DealerId
                        Dim ProductCodeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", context:=plistcontext)
                        objDDL.Populate(ProductCodeList, New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .BlankItemValue = "0",
                                .TextFunc = AddressOf .GetCode,
                                .ValueFunc = AddressOf .GetCode,
                                .SortFunc = AddressOf .GetCode
                            })

                        If Not dvRow.ProductCode Is Nothing Then
                            Me.SetSelectedItem(objDDL, dvRow.ProductCode)
                        End If

                        'set coverage type
                        objDDL = CType(e.Row.Cells(Me.GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
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
                            Me.SetSelectedItem(objDDL, dvRow.ConverageTypeId.ToString)
                        End If

                        'set certificate duration
                        objDDL = CType(e.Row.Cells(Me.GRID_COL_CERT_DURATION_IDX).FindControl("ddlCertDuration"), DropDownList)
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

                        If Not dvRow.CertDuration Is Nothing Then
                            Me.SetSelectedItem(objDDL, dvRow.CertDuration.Value.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub PopulateReplacementPolicyGrid(ByVal ds As Collections.Generic.List(Of ReppolicyClaimCount))
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

    Private Sub BtnNewReplacementPolicy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewReplacementPolicy_WRITE.Click
        Try
            State.RepPolicyAction = RepPolicy_Add
            'Me.State.RepPolicyList = Nothing
            Dim objNew As New ReppolicyClaimCount()
            objNew.ContractId = State.MyBO.Id
            State.RepPolicyWorkingItem = objNew

            If State.RepPolicyList Is Nothing Then
                State.RepPolicyList = New Collections.Generic.List(Of ReppolicyClaimCount)
            End If
            Me.State.RepPolicyList.Add(objNew)

            moGridView.SelectedIndex = State.RepPolicyList.Count - 1
            moGridView.EditIndex = moGridView.SelectedIndex
            PopulateReplacementPolicyGrid(Me.State.RepPolicyList)

            EnableDisableBtnsForRepPolicyGrid()
            Me.SetFocusInGrid(moGridView, moGridView.SelectedIndex, Me.GRID_COL_CLAIM_COUNT_IDX)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ReplacementPolicyCancelRecord()
        Try
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            If State.RepPolicyAction = RepPolicy_Add Then
                State.RepPolicyList.Remove(State.RepPolicyWorkingItem)
            ElseIf State.RepPolicyAction = RepPolicy_Edit AndAlso (Not State.RepPolicyWorkingOrig Is Nothing) Then ' set the object to original status
                CopyReplacementPolicyObject(State.RepPolicyWorkingOrig, State.RepPolicyWorkingItem)
            End If

            State.RepPolicyAction = RepPolicy_None
            State.RepPolicyWorkingItem = Nothing

            PopulateReplacementPolicyGrid(Me.State.RepPolicyList)
            EnableDisableBtnsForRepPolicyGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Function NewReplacementPolicyValid(ByVal obj As ReppolicyClaimCount) As Boolean
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

    Private Sub CopyReplacementPolicyObject(ByVal objSource As ReppolicyClaimCount, ByVal objDest As ReppolicyClaimCount)
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

            objDDL = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_PRODUCT_CODE_IDX).FindControl("ddlProductCode"), DropDownList)
            Me.PopulateBOProperty(State.RepPolicyWorkingItem, "ProductCode", objDDL.SelectedItem.Text)
            objDDL = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
            Me.PopulateBOProperty(State.RepPolicyWorkingItem, "ConverageTypeId", objDDL)
            objDDL = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_CERT_DURATION_IDX).FindControl("ddlCertDuration"), DropDownList)
            Me.PopulateBOProperty(State.RepPolicyWorkingItem, "CertDuration", objDDL.SelectedItem.Text)
            objTxt = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_CLAIM_COUNT_IDX).FindControl("txtClaimCount"), TextBox)

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
                    Me.PopulateBOProperty(State.RepPolicyWorkingItem, "ReplacementPolicyClaimCount", strTemp)
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
                    Me.State.RepPolicyList = Nothing
                    Me.State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(Me.State.MyBO.Id)
                Else 'new contract, keep the record in memory after validation and save it with new contract
                    If NewReplacementPolicyValid(State.RepPolicyWorkingItem) Then
                        Dim objInd As Integer = State.RepPolicyList.FindIndex(Function(r) r.Id = State.RepPolicyWorkingItem.Id)
                        State.RepPolicyList.Item(objInd) = State.RepPolicyWorkingItem
                    Else 'Validation error, exit and show errors
                        Exit Sub
                    End If
                End If
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If


            State.RepPolicyAction = RepPolicy_None
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            State.RepPolicyWorkingItem = Nothing
            PopulateReplacementPolicyGrid(Me.State.RepPolicyList)
            EnableDisableBtnsForRepPolicyGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        PopulateReplacementPolicyGrid(Me.State.RepPolicyList)
        EnableDisableBtnsForRepPolicyGrid()
        Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
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
        Me.State.RepPolicyList = ReppolicyClaimCount.GetReplacementPolicyClaimCntConfigByContract(Me.State.MyBO.Id)
        If State.RepPolicyList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.RepPolicyList.Count - 1
                State.RepPolicyList.Item(i).Delete()
                State.RepPolicyList.Item(i).SaveWithoutCheckDSCreator()
            Next
        End If
        Me.State.RepPolicyList = Nothing
    End Sub
    Private Sub SaveReplacementPolicy(ByVal blnNewBO As Boolean)
        If blnNewBO Then
            If State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) OrElse
                State.MyBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then
                ' new BO, save the replacement policy records in memory
                If (Not State.RepPolicyList Is Nothing) AndAlso State.RepPolicyList.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To State.RepPolicyList.Count - 1
                        State.RepPolicyList.Item(i).SaveWithoutCheckDSCreator()
                    Next
                    Me.State.RepPolicyList = Nothing
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

    Public Sub GridViewDepreciationSchedule_RowCreated(ByVal sender As System.Object, ByVal e As GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub GridViewDepreciationSchedule_RowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs)
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
    Private Sub GridViewDepreciationSchedule_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewDepreciationSchedule.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DepreciationScdRelation


            If Not e.Row.DataItem Is Nothing Then
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
                                                                                           Where lst.ExtendedCode = "YESNO-Y" Or lst.Code = dvRow.DepreciationScheduleCode
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
                        If Not dvRow.EffectiveDate Is Nothing Then
                            CType(e.Row.Cells(DepSchGridColEffective).FindControl("txtEffectiveDate"), TextBox).Text = GetDateFormattedStringNullable(dvRow.EffectiveDate.Value)
                        End If

                        Dim btnExpirationDate As ImageButton
                        Dim txtExpirationDate As TextBox
                        btnExpirationDate = CType(e.Row.Cells(DepSchGridColExpiration).FindControl("btnExpirationDate"), ImageButton)
                        txtExpirationDate = CType(e.Row.Cells(DepSchGridColExpiration).FindControl("txtExpirationDate"), TextBox)
                        AddCalendar_New(btnExpirationDate, txtExpirationDate)
                        If Not dvRow.ExpirationDate Is Nothing Then
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

                        If Not dvRow.DepreciationScheduleUsageXcd Is Nothing AndAlso Not String.IsNullOrEmpty(dvRow.DepreciationScheduleUsageXcd.ToString()) Then
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
                        If Not dvRow.EffectiveDate Is Nothing Then
                            Dim lblEffectiveDate As Label
                            lblEffectiveDate = CType(e.Row.Cells(DepSchGridColEffective).FindControl("lblEffectiveDate"), Label)
                            lblEffectiveDate.Text = GetDateFormattedStringNullable(CType(dvRow.EffectiveDate, Date))
                        End If
                        If Not dvRow.ExpirationDate Is Nothing Then
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
    Private Sub PopulateDepreciationScheduleGrid(ByVal ds As Collections.Generic.List(Of DepreciationScdRelation))

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
            ElseIf State.DepreciationScheduleAction = DepreciationScheduleEdit AndAlso (Not State.DepreciationScheduleOrig Is Nothing) Then ' set the object to original status
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
    Private Sub CopyDepreciationScheduleObject(ByVal objSource As DepreciationScdRelation, ByVal objDest As DepreciationScdRelation)
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
    Private Sub BtnNewDepreciationSchedule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnNewDepreciationSchedule.Click
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
        If (Me.State.MyBO.DealerId <> Guid.Empty) Then
            Dim oDealer As New Dealer(Me.State.MyBO.DealerId)

            If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then

                    If Me.cboLossCostPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        Me.PopulateBOProperty(Me.State.MyBO, "LossCostPercentSourceXcd", Me.cboLossCostPercentSourceXcd, False, True)
                    End If

                    If Me.cboAdminExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        Me.PopulateBOProperty(Me.State.MyBO, "AdminExpenseSourceXcd", Me.cboAdminExpenseSourceXcd, False, True)
                    End If

                    If Me.cboProfitExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        Me.PopulateBOProperty(Me.State.MyBO, "ProfitPercentSourceXcd", Me.cboProfitExpenseSourceXcd, False, True)
                    End If

                    If Me.cboCommPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        Me.PopulateBOProperty(Me.State.MyBO, "CommissionsPercentSourceXcd", Me.cboCommPercentSourceXcd, False, True)
                    End If

                    If Me.cboMarketingExpenseSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        Me.PopulateBOProperty(Me.State.MyBO, "MarketingPercentSourceXcd", Me.cboMarketingExpenseSourceXcd, False, True)
                    End If

                    If cboIgnorePremium.Visible And cboIgnorePremium.Items.Count > 0 And Me.cboIgnorePremium.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboIgnorePremium)).Equals(Codes.YESNO_Y) Then

                            ValidateIncomingSourceXcd()

                            If Me.State.IsBucketIncomingSelected Then
                                ElitaPlusPage.SetLabelError(Me.LabelIgnorePremium)
                                ElitaPlusPage.SetLabelError(Me.LabelLossCostPercent)
                                ElitaPlusPage.SetLabelError(Me.LabelProfitExpense)
                                ElitaPlusPage.SetLabelError(Me.LabelAdminExpense)
                                ElitaPlusPage.SetLabelError(Me.LabelMarketingExpense)
                                ElitaPlusPage.SetLabelError(Me.LabelCommPercent)
                                Throw New GUIException(Message.MSG_INCOMING_OPTION_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INCOMING_OPTION_NOT_ALLOWED_WHEN_IGNORE_PREMIUM_IS_YES)
                            End If
                        End If
                    End If

                    ValidateDifferenceSourceXcd()

                    If Me.State.IsDiffSelectedTwice Then
                        ElitaPlusPage.SetLabelError(Me.LabelLossCostPercent)
                        ElitaPlusPage.SetLabelError(Me.LabelProfitExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelAdminExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelMarketingExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelCommPercent)
                        Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ALLOWED_ONLY_ONCE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_SOURCE_ALLOWED_ONLY_FOR_ONE_BUCKET)
                    ElseIf Me.State.IsDiffNotSelectedOnce Then
                        ElitaPlusPage.SetLabelError(Me.LabelLossCostPercent)
                        ElitaPlusPage.SetLabelError(Me.LabelProfitExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelAdminExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelMarketingExpense)
                        ElitaPlusPage.SetLabelError(Me.LabelCommPercent)
                        Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ATLEAST_ONE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_OPTION_SHOULD_PRESENT_ATLEAST_FOR_ONE_BUCKET)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub PopulateSourceDropdownBucketFromBOs()
        With Me.State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000
                        If cboLossCostPercentSourceXcd.Visible Then
                            If Not .LossCostPercentSourceXcd Is Nothing And Me.cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                Me.SetSelectedItem(Me.cboLossCostPercentSourceXcd, .LossCostPercentSourceXcd)

                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxLossCostPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If cboProfitExpenseSourceXcd.Visible Then
                            If Not .ProfitPercentSourceXcd Is Nothing And Me.cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                Me.SetSelectedItem(Me.cboProfitExpenseSourceXcd, .ProfitPercentSourceXcd)

                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxProfitExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If cboMarketingExpenseSourceXcd.Visible Then
                            If Not .MarketingPercentSourceXcd Is Nothing And Me.cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                Me.SetSelectedItem(Me.cboMarketingExpenseSourceXcd, .MarketingPercentSourceXcd)

                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxMarketingExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If cboAdminExpenseSourceXcd.Visible Then
                            If Not .AdminExpenseSourceXcd Is Nothing And Me.cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                Me.SetSelectedItem(Me.cboAdminExpenseSourceXcd, .AdminExpenseSourceXcd)

                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxAdminExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If cboCommPercentSourceXcd.Visible Then
                            If Not .CommissionsPercentSourceXcd Is Nothing And Me.cboCommPercentSourceXcd.Items.Count > 0 Then
                                Me.SetSelectedItem(Me.cboCommPercentSourceXcd, .CommissionsPercentSourceXcd)

                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxCommPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, True)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub ValidateDifferenceSourceXcd()

        Me.State.IsDiffSelectedTwice = False
        Me.State.IsDiffNotSelectedOnce = False
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
            Me.State.IsDiffSelectedTwice = True
        ElseIf countDiff < 1 Then
            Me.State.IsDiffNotSelectedOnce = True
        End If

    End Sub

    Private Sub ValidateIncomingSourceXcd()

        Me.State.IsBucketIncomingSelected = False

        If cboLossCostPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            Me.State.IsBucketIncomingSelected = True
        ElseIf cboProfitExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            Me.State.IsBucketIncomingSelected = True
        ElseIf cboAdminExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            Me.State.IsBucketIncomingSelected = True
        ElseIf cboCommPercentSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            Me.State.IsBucketIncomingSelected = True
        ElseIf cboMarketingExpenseSourceXcd.SelectedItem.Value.Contains(Codes.ACCT_BUCKETS_SOURCE_OPTION_INCOMING) Then
            Me.State.IsBucketIncomingSelected = True
        End If

    End Sub

    Private Sub BindSourceOptionDropdownlist()
        If (Me.State.MyBO.DealerId <> Guid.Empty) Then
            Dim oDealer As New Dealer(Me.State.MyBO.DealerId)

            If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
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
        With Me.State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        DisplaySourceXcdDropdownFields()
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000

                        If Not cboLossCostPercentSourceXcd Is Nothing Then
                            If (cboLossCostPercentSourceXcd.Visible And cboLossCostPercentSourceXcd.Items.Count > 0) Then
                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxLossCostPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If Not cboProfitExpenseSourceXcd Is Nothing Then
                            If (cboProfitExpenseSourceXcd.Visible And cboProfitExpenseSourceXcd.Items.Count > 0) Then
                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxProfitExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If Not cboMarketingExpenseSourceXcd Is Nothing Then
                            If (cboMarketingExpenseSourceXcd.Visible And cboMarketingExpenseSourceXcd.Items.Count > 0) Then
                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxMarketingExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If Not cboAdminExpenseSourceXcd Is Nothing Then
                            If (cboAdminExpenseSourceXcd.Visible And cboAdminExpenseSourceXcd.Items.Count > 0) Then
                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxAdminExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If Not cboCommPercentSourceXcd Is Nothing Then
                            If (cboCommPercentSourceXcd.Visible And cboCommPercentSourceXcd.Items.Count > 0) Then
                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxCommPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, True)
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
        With Me.State.MyBO
            If (.DealerId <> Guid.Empty) Then
                Dim oDealer As New Dealer(.DealerId)
                If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        Dim diffFixedValue As Decimal
                        diffFixedValue = 0.0000

                        If Not cboLossCostPercentSourceXcd Is Nothing Then
                            If cboLossCostPercentSourceXcd.Visible And cboLossCostPercentSourceXcd.Items.Count > 0 Then
                                If cboLossCostPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxLossCostPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, True)
                                End If
                            End If
                        End If

                        If Not cboProfitExpenseSourceXcd Is Nothing Then
                            If cboProfitExpenseSourceXcd.Visible And cboProfitExpenseSourceXcd.Items.Count > 0 Then
                                If cboProfitExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxProfitExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, True)
                                End If
                            End If
                        End If

                        If Not cboMarketingExpenseSourceXcd Is Nothing Then
                            If cboMarketingExpenseSourceXcd.Visible And cboMarketingExpenseSourceXcd.Items.Count > 0 Then
                                If cboMarketingExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxMarketingExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, True)
                                End If
                            End If
                        End If

                        If Not cboAdminExpenseSourceXcd Is Nothing Then
                            If cboAdminExpenseSourceXcd.Visible And cboAdminExpenseSourceXcd.Items.Count > 0 Then
                                If cboAdminExpenseSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxAdminExpense, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, True)
                                End If
                            End If
                        End If

                        If Not cboAdminExpenseSourceXcd Is Nothing Then
                            If cboCommPercentSourceXcd.Visible And cboCommPercentSourceXcd.Items.Count > 0 Then
                                If cboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_OPTION_DIFFERENCE) Then
                                    Me.PopulateControlFromBOProperty(Me.TextboxCommPercent, diffFixedValue, Me.PERCENT_FORMAT)
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, False)
                                Else
                                    ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, True)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DisplaySourceXcdDropdownFields()
        ControlMgr.SetVisibleControl(Me, Me.cboLossCostPercentSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, Me.cboProfitExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, Me.cboAdminExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, Me.cboMarketingExpenseSourceXcd, True)
        ControlMgr.SetVisibleControl(Me, Me.cboCommPercentSourceXcd, True)
    End Sub

    Private Sub HideSourceXcdDropdownFields()
        ControlMgr.SetVisibleControl(Me, Me.cboLossCostPercentSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, Me.cboProfitExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, Me.cboAdminExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, Me.cboMarketingExpenseSourceXcd, False)
        ControlMgr.SetVisibleControl(Me, Me.cboCommPercentSourceXcd, False)
    End Sub

    Private Sub EnablePercentageTextBox()
        ControlMgr.SetEnableControl(Me, Me.TextboxLossCostPercent, True)
        ControlMgr.SetEnableControl(Me, Me.TextboxProfitExpense, True)
        ControlMgr.SetEnableControl(Me, Me.TextboxMarketingExpense, True)
        ControlMgr.SetEnableControl(Me, Me.TextboxAdminExpense, True)
        ControlMgr.SetEnableControl(Me, Me.TextboxCommPercent, True)
    End Sub

#End Region
End Class

