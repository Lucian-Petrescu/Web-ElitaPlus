Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class AccountingEventForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents moAccountingCompanyTEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moDebitCreditLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moDebitCreditDROPDOWN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAccountCodeLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moAccountCodeTEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moUsePayeeSettingsLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moUsePayeeSettingsDROPDOWN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAllocationMarkerLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moAllocationMarkerDROPDOWN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moFieldTypeIdLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moFieldTypeDROPDOWN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moCalculationLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moCalculationTEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moJournalSourceLABEL As System.Web.UI.WebControls.Label
    Protected WithEvents moJournalSourceTEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal1LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal1TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal2LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal2TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal3LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal3TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal4LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal4 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal4TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal5LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal5 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal5TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal6LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal6 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal6TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal7LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal7 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal7TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal8LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal8 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal8TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal9LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal9 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal9TEXT As System.Web.UI.WebControls.TextBox
    Protected WithEvents moAcctAnal10LABEL As System.Web.UI.WebControls.Label
    Protected WithEvents cboAcctAnal10 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents moAcctAnal10TEXT As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Shared URL As String = "AccountingEventForm.aspx"
    Public Const PAGE_TAB As String = "TABLES"
    Public Const PAGE_TITLE As String = "ACCOUNTING_EVENT"

    Public Const ENTITYNAME_PROPERTY As String = "EntityName"
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"

    Public Const ACCTCOMPANY_PROPERTY As String = "AcctCompanyId"
    Public Const EVENTNAME_PROPERTY As String = "EventName"
    Public Const JOURNALTYPE_PROPERTY As String = "JournalType"
    Public Const LOADONLY_PROPERTY As String = "LoadOnly"
    Public Const POSTINGTYPE_PROPERTY As String = "PostingType"
    Public Const POSTPROVISIONAL_PROPERTY As String = "PostProvisional"
    Public Const POSTTOHOLD_PROPERTY As String = "PostToHold"
    Public Const REPORTINGACCOUNT_PROPERTY As String = "ReportingAccount"
    Public Const SUPPRESSSUBSTITUTEDMESSAGES_PROPERTY As String = "SuppressSubstitutedMessages"
    Public Const SUSPENSEACCOUNT_PROPERTY As String = "SuspenseAccount"
    Public Const TRANSACTIONAMOUNTACCOUNT_PROPERTY As String = "TransactionAmountAccount"
    Public Const JOURNALLEVEL_PROPERTY As String = "JournalLevel"
    Public Const ALLOWBALTRANSFER_PROPERTY As String = "AllowBalTran"
    Public Const ALLOWOVERBUDGET_PROPERTY As String = "AllowOverBudget"
    Public Const ALLOWPOSTTOSUSPENDED_PROPERTY As String = "AllowPostToSuspended"
    Public Const BALANCINGOPTIONS_PROPERTY As String = "BalancingOptions"
    Public Const ACCTEVENTTYPE_PROPERTY As String = "AcctEventTypeId"
    Public Const LAYOUTCODE_PROPERTY As String = "LayoutCode"
    Public Const EVENTDESCRIPTION_PROPERTY As String = "EventDescription"
    Public Const GRIDCOL_BUSINESS_UNIT As Integer = 2
    Public Const GRIDCOL_DEBITCREDIT As Integer = 4
    Public Const GRIDCOL_ACCOUNTCODE As Integer = 3
    Public Const GRIDCOL_FIELDTYPE As Integer = 5
    Public Const GRIDCOL_EVENTDETAIL_ID As Integer = 6

    Private Const AcctSystemFelita As String = "FEL"
    Private Const ACCTEVENTTYPE_PREMIUM As String = "PREM"

    Private Const YESNO As String = "YESNO"

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As AcctEvent
        Public ScreenSnapShotBO As AcctEvent
        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public YESNOdv As DataView
        Public SelectedDetailId As Guid = Guid.Empty
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New AcctEvent(CType(CallingParameters, Guid))
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn

        Try
            If ReturnPar IsNot Nothing Then
                'Get the id from the parent
                Dim _ret As ReturnType = CType(ReturnPar, ReturnType)
                State.MyBO = _ret.EditingBo
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As AcctEvent
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As AcctEvent, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then

                SetFormTitle(PAGE_TITLE)
                SetFormTab(PAGE_TAB)

                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                PopulateAll()

                If State.IsNew = True Then
                    CreateNew()
                End If

                PopulateFormFromBOs()
                EnableDisableFields()

            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateAll()

        Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        State.YESNOdv = YESNOdv

        'BindListControlToDataView(Me.moAllowBalTranDDL, YESNOdv)
        'BindListControlToDataView(Me.moAllowOverBudgetDDL, YESNOdv)
        'BindListControlToDataView(Me.moAllowPostToSuspendedDDL, YESNOdv)
        'BindListControlToDataView(Me.moLoadOnlyDDL, YESNOdv)
        'BindListControlToDataView(Me.moPostProvisionalDDL, YESNOdv)
        'BindListControlToDataView(Me.moSuppressSubstitutedMessagesDDL, YESNOdv)
        'BindListControlToDataView(Me.moEventTypeDDL, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        'BindListControlToDataView(Me.moBalancingOptionsDDL, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCTBO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        'BindListControlToDataView(Me.moPostingTypeDDL, LookupListNew.DropdownLookupList(LookupListNew.LK_POSTINGTYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))

        Dim populateOptions = New PopulateOptions() With
                                {
                                   .AddBlankItem = True
                                }

        Dim YesNoList As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        moAllowBalTranDDL.Populate(YesNoList.ToArray(), populateOptions)
        moAllowOverBudgetDDL.Populate(YesNoList.ToArray(), populateOptions)
        moAllowPostToSuspendedDDL.Populate(YesNoList.ToArray(), populateOptions)
        moLoadOnlyDDL.Populate(YesNoList.ToArray(), populateOptions)
        moPostProvisionalDDL.Populate(YesNoList.ToArray(), populateOptions)
        moSuppressSubstitutedMessagesDDL.Populate(YesNoList.ToArray(), populateOptions)

        Dim JournalLevel As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="JOURNALLEVEL",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        moJournalLevelDDL.Populate(JournalLevel.ToArray(), populateOptions)

        Dim EventType As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTRANSTYP",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        moEventTypeDDL.Populate(EventType.ToArray(), populateOptions)

        Dim BalancingOptions As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBO",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        moBalancingOptionsDDL.Populate(BalancingOptions.ToArray(), populateOptions)

        Dim PostingType As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="POSTTYPE",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        moPostingTypeDDL.Populate(PostingType.ToArray(), populateOptions)


        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 0 Then
            For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
                moAccountingCompanyDropDown.Items.Add(New ListItem(_acctCo.Description, _acctCo.Id.ToString))
            Next
            If moAccountingCompanyDropDown.Items.Count > 1 Then
                ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, True)
                moAccountingCompanyDropDown.Items.Add(New ListItem("", NOTHING_SELECTED))
                moAccountingCompanyDropDown.SelectedValue = NOTHING_SELECTED
            ElseIf moAccountingCompanyDropDown.Items.Count = 1 Then
                ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, False)
            End If
        End If

    End Sub

    Protected Sub CreateNew()
        moAccountingEventGrid.CurrentPageIndex = 0
        State.ScreenSnapShotBO = Nothing
        State.MyBO = New AcctEvent
    End Sub

    Protected Sub PopulateFormFromBOs()

        Dim YesNodv As DataView = State.YESNOdv

        With State.MyBO

            PopulateControlFromBOProperty(moJournalTypeTEXT, .JournalType)
            PopulateControlFromBOProperty(moPostToHoldTEXT, .PostToHold)
            PopulateControlFromBOProperty(moReportingAccountTEXT, .ReportingAccount)
            PopulateControlFromBOProperty(moSuspenseAccountTEXT, .SuspenseAccount)
            PopulateControlFromBOProperty(moTransactionAmountAccountTEXT, .TransactionAmountAccount)
            PopulateControlFromBOProperty(moLayoutCodeTEXT, .LayoutCode)
            PopulateControlFromBOProperty(moEventDescriptionTEXT, .EventDescription)

            'Fill Drop Down Lists
            PopulateControlFromBOProperty(moSuppressSubstitutedMessagesDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .SuppressSubstitutedMessages))
            PopulateControlFromBOProperty(moPostProvisionalDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .PostProvisional))
            PopulateControlFromBOProperty(moLoadOnlyDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .LoadOnly))
            PopulateControlFromBOProperty(moAllowBalTranDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowBalTran))
            PopulateControlFromBOProperty(moAllowOverBudgetDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowOverBudget))
            PopulateControlFromBOProperty(moAllowPostToSuspendedDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowPostToSuspended))
            PopulateControlFromBOProperty(moBalancingOptionsDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCTBO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .BalancingOptions))
            PopulateControlFromBOProperty(moPostingTypeDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_POSTINGTYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .PostingType))
            PopulateControlFromBOProperty(moJournalLevelDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_JOURNALLEVEL, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .JournalLevel))

            PopulateControlFromBOProperty(moEventTypeDDL, .AcctEventTypeId)

            If Not .AcctCompanyId = Guid.Empty Then

                If moAccountingCompanyDropDown.Items.Count > 1 Then
                    PopulateControlFromBOProperty(moAccountingCompanyDropDown, .AcctCompanyId)
                Else
                    moAccountingCompanyDropDown.SelectedIndex = 0
                End If

            Else

                If State.MyBO.IsNew Then
                    If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 1 Then
                        PopulateBOProperty(State.MyBO, ACCTCOMPANY_PROPERTY, moAccountingCompanyDropDown)
                    Else
                        State.MyBO.AcctCompanyId = ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).Id
                    End If
                End If
            End If

            PopulateEventDetails()

        End With

    End Sub

    Private Sub PopulateEventDetails()

        Dim dv As AcctEventDetail.AcctEventDetailSearchDV
        dv = AcctEventDetail.getList(State.MyBO.Id)
        If dv IsNot Nothing Then
            moAccountingEventGrid.DataSource = dv
            moAccountingEventGrid.DataBind()
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        With State
            BindBOPropertyToLabel(.MyBO, JOURNALTYPE_PROPERTY, moJournalTypeLABEL)
            BindBOPropertyToLabel(.MyBO, LOADONLY_PROPERTY, moLoadOnlyLABEL)
            BindBOPropertyToLabel(.MyBO, POSTINGTYPE_PROPERTY, moPostingTypeLABEL)
            BindBOPropertyToLabel(.MyBO, POSTPROVISIONAL_PROPERTY, moPostProvisionalLABEL)
            BindBOPropertyToLabel(.MyBO, POSTTOHOLD_PROPERTY, moPostToHoldLABEL)
            BindBOPropertyToLabel(.MyBO, REPORTINGACCOUNT_PROPERTY, moReportingAccountLABEL)
            BindBOPropertyToLabel(.MyBO, SUPPRESSSUBSTITUTEDMESSAGES_PROPERTY, moSuppressSubstitutedMessagesLABEL)
            BindBOPropertyToLabel(.MyBO, SUSPENSEACCOUNT_PROPERTY, moSuspenseAccountLABEL)
            BindBOPropertyToLabel(.MyBO, TRANSACTIONAMOUNTACCOUNT_PROPERTY, moTransactionAmountAccountLABEL)
            BindBOPropertyToLabel(.MyBO, JOURNALLEVEL_PROPERTY, moJournalLevelLABEL)
            BindBOPropertyToLabel(.MyBO, ACCTCOMPANY_PROPERTY, moAccountingCompanyLABEL)
            BindBOPropertyToLabel(.MyBO, ALLOWBALTRANSFER_PROPERTY, moAllowBalTranLABEL)
            BindBOPropertyToLabel(.MyBO, ALLOWOVERBUDGET_PROPERTY, moAllowOverBudgetLABEL)
            BindBOPropertyToLabel(.MyBO, ALLOWPOSTTOSUSPENDED_PROPERTY, moAllowPostToSuspendedLABEL)
            BindBOPropertyToLabel(.MyBO, BALANCINGOPTIONS_PROPERTY, moBalancingOptionsLABEL)
            BindBOPropertyToLabel(.MyBO, ACCTEVENTTYPE_PROPERTY, moEventTypeLABEL)
            BindBOPropertyToLabel(.MyBO, LAYOUTCODE_PROPERTY, moLayoutCodeLABEL)
            BindBOPropertyToLabel(.MyBO, EVENTDESCRIPTION_PROPERTY, moEventDescriptionLABEL)

        End With

        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        'Enabled by Default

        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnBack, True)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, True)

        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, True)
        Else
            ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, False)
        End If

        Dim AcctCompanyBO As AcctCompany = New AcctCompany(State.MyBO.AcctCompanyId)
        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM,
                      ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), AcctCompanyBO.AcctSystemId) = AcctSystemFelita AndAlso
                      LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_EVENT_TYPE,
                      ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GetSelectedItem(moEventTypeDDL)) = ACCTEVENTTYPE_PREMIUM Then

            'moJournalLevelDDL.SelectedIndex = -1
            ControlMgr.SetVisibleControl(Me, moJournalLevelDDL, True)
            ControlMgr.SetVisibleControl(Me, moJournalLevelLABEL, True)
        Else
            moJournalLevelDDL.SelectedIndex = -1
            ControlMgr.SetVisibleControl(Me, moJournalLevelDDL, False)
            ControlMgr.SetVisibleControl(Me, moJournalLevelLABEL, False)
        End If

        'If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM,
        '               ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), AcctCompanyBO.AcctSystemId) <> AcctSystemFelita Then
        '    ControlMgr.SetVisibleControl(Me, Me.moJournalLevelDDL, False)
        '    ControlMgr.SetVisibleControl(Me, Me.moJournalLevelLABEL, False)
        'End If

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Redirect_
                    callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(State.SelectedDetailId, State.MyBO))
                Case DetailPageCommand.Delete
                    DeleteEventDetailItem()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Protected Sub PopulateBOsFromForm()

        With State

            PopulateBOProperty(.MyBO, JOURNALTYPE_PROPERTY, moJournalTypeTEXT)
            PopulateBOProperty(.MyBO, POSTTOHOLD_PROPERTY, moPostToHoldTEXT)
            PopulateBOProperty(.MyBO, REPORTINGACCOUNT_PROPERTY, moReportingAccountTEXT)
            PopulateBOProperty(.MyBO, SUSPENSEACCOUNT_PROPERTY, moSuspenseAccountTEXT)
            PopulateBOProperty(.MyBO, TRANSACTIONAMOUNTACCOUNT_PROPERTY, moTransactionAmountAccountTEXT)
            PopulateBOProperty(.MyBO, LAYOUTCODE_PROPERTY, moLayoutCodeTEXT)
            PopulateBOProperty(.MyBO, EVENTDESCRIPTION_PROPERTY, moEventDescriptionTEXT)

            'Fill Drop Down Lists
            PopulateBOProperty(.MyBO, SUPPRESSSUBSTITUTEDMESSAGES_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moSuppressSubstitutedMessagesDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, JOURNALLEVEL_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_JOURNALLEVEL, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(moJournalLevelDDL.SelectedValue)))

            PopulateBOProperty(.MyBO, POSTPROVISIONAL_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moPostProvisionalDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, LOADONLY_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moLoadOnlyDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, ALLOWBALTRANSFER_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moAllowBalTranDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, ALLOWOVERBUDGET_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moAllowOverBudgetDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, ALLOWPOSTTOSUSPENDED_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(moAllowPostToSuspendedDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, BALANCINGOPTIONS_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCTBO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(moBalancingOptionsDDL.SelectedValue)))
            PopulateBOProperty(.MyBO, POSTINGTYPE_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_POSTINGTYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(moPostingTypeDDL.SelectedValue)))

            PopulateBOProperty(.MyBO, ACCTEVENTTYPE_PROPERTY, moEventTypeDDL)

            If .MyBO.IsNew Then
                If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 1 Then
                    PopulateBOProperty(.MyBO, ACCTCOMPANY_PROPERTY, moAccountingCompanyDropDown)
                Else
                    .MyBO.AcctCompanyId = ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).Id
                End If
            End If

        End With


    End Sub

    Private Sub DeleteEventDetailItem()

        Try
            Dim _eventDetail As New AcctEventDetail(State.SelectedDetailId)
            _eventDetail.Delete()
            _eventDetail.Save()
            _eventDetail = Nothing

            moAccountingEventGrid.CurrentPageIndex = 0
            PopulateEventDetails()
            State.SelectedDetailId = Guid.Empty
            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub CreateNewWithCopy()

        Dim newBO As New AcctEvent

        newBO.AcctCompanyId = State.MyBO.AcctCompanyId
        State.MyBO = newBO

        'Reset the source  and business unit fields
        moEventTypeDDL.SelectedIndex = 0

        PopulateBOsFromForm()
        PopulateEventDetails()

        'create the backup copy
        State.ScreenSnapShotBO = New AcctEvent
        State.ScreenSnapShotBO.Clone(State.MyBO)
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New AcctEvent(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
            Else
                callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(Guid.Empty, State.MyBO))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


#End Region

#Region "DataGrid Methods"

    Private Sub moAccountingEventGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moAccountingEventGrid.ItemCommand

        Try
            If e.CommandName = "SelectAction" Then
                Try
                    PopulateBOsFromForm()
                    If State.MyBO.IsDirty Then
                        AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
                        State.SelectedDetailId = New Guid(e.Item.Cells(GRIDCOL_EVENTDETAIL_ID).Text)
                    Else
                        callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(New Guid(e.Item.Cells(GRIDCOL_EVENTDETAIL_ID).Text), State.MyBO))
                    End If
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    HandleErrors(ex, ErrControllerMaster)
                    AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                    State.LastErrMsg = ErrControllerMaster.Text
                End Try
            ElseIf e.CommandName = "DeleteRecord" Then
                State.ActionInProgress = DetailPageCommand.Delete
                State.SelectedDetailId = New Guid(e.Item.Cells(GRIDCOL_EVENTDETAIL_ID).Text)
                AddConfirmMsg(Message.DELETE_RECORD_PROMPT, HiddenSaveChangesPromptResponse)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub moAccountingEventGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moAccountingEventGrid.ItemDataBound

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
            e.Item.Cells(GRIDCOL_EVENTDETAIL_ID).Text = GetGuidStringFromByteArray(CType(dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_ACCT_EVENT_DETAIL_ID), Byte()))
            e.Item.Cells(GRIDCOL_BUSINESS_UNIT).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_BUSINESS_UNIT).ToString
            e.Item.Cells(GRIDCOL_DEBITCREDIT).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_DEBIT_CREDIT).ToString
            e.Item.Cells(GRIDCOL_FIELDTYPE).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_FIELD_TYPE).ToString
            e.Item.Cells(GRIDCOL_ACCOUNTCODE).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_ACCOUNT_CODE).ToString
        End If

    End Sub

    Private Sub moAccountingEventGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moAccountingEventGrid.ItemCreated
        Dim pg As New ElitaPlusSearchPage
        pg.BaseItemCreated(sender, e)
    End Sub

    Private Sub moAccountingEventGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moAccountingEventGrid.PageIndexChanged

        moAccountingEventGrid.CurrentPageIndex = e.NewPageIndex
        PopulateEventDetails()

    End Sub

    Private Sub moEventTypeDDL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moEventTypeDDL.SelectedIndexChanged
        Dim AcctCompanyBO As AcctCompany = New AcctCompany(State.MyBO.AcctCompanyId)
        'If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM,
        '              ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), AcctCompanyBO.AcctSystemId) <> AcctSystemFelita Then
        '    ControlMgr.SetVisibleControl(Me, Me.moJournalLevelDDL, False)
        '    ControlMgr.SetVisibleControl(Me, Me.moJournalLevelLABEL, False)

        'Else
        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM,
                      ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), AcctCompanyBO.AcctSystemId) = AcctSystemFelita AndAlso
                      LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_EVENT_TYPE,
                      ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GetSelectedItem(moEventTypeDDL)) = ACCTEVENTTYPE_PREMIUM Then

            moJournalLevelDDL.SelectedIndex = -1
            ControlMgr.SetVisibleControl(Me, moJournalLevelDDL, True)
            ControlMgr.SetVisibleControl(Me, moJournalLevelLABEL, True)
        Else
            moJournalLevelDDL.SelectedIndex = -1
            ControlMgr.SetVisibleControl(Me, moJournalLevelDDL, False)
            ControlMgr.SetVisibleControl(Me, moJournalLevelLABEL, False)
        End If

    End Sub

#End Region

End Class
