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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
    Private Const ACCTEVENTTYPE_UPR As String = "UPR"

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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New AcctEvent(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn

        Try
            If Not ReturnPar Is Nothing Then
                'Get the id from the parent
                Dim _ret As ReturnType = CType(ReturnPar, ReturnType)
                Me.State.MyBO = _ret.EditingBo
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As AcctEvent
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As AcctEvent, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(Me.PAGE_TITLE)
                Me.SetFormTab(Me.PAGE_TAB)

                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                PopulateAll()

                If Me.State.IsNew = True Then
                    CreateNew()
                End If

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()

            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateAll()

        Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Me.State.YESNOdv = YESNOdv

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

        Me.moAllowBalTranDDL.Populate(YesNoList.ToArray(), populateOptions)
        Me.moAllowOverBudgetDDL.Populate(YesNoList.ToArray(), populateOptions)
        Me.moAllowPostToSuspendedDDL.Populate(YesNoList.ToArray(), populateOptions)
        Me.moLoadOnlyDDL.Populate(YesNoList.ToArray(), populateOptions)
        Me.moPostProvisionalDDL.Populate(YesNoList.ToArray(), populateOptions)
        Me.moSuppressSubstitutedMessagesDDL.Populate(YesNoList.ToArray(), populateOptions)

        Dim JournalLevel As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="JOURNALLEVEL",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Me.moJournalLevelDDL.Populate(JournalLevel.ToArray(), populateOptions)

        Dim EventType As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTRANSTYP",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Me.moEventTypeDDL.Populate(EventType.ToArray(), populateOptions)

        Dim BalancingOptions As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBO",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Me.moBalancingOptionsDDL.Populate(BalancingOptions.ToArray(), populateOptions)

        Dim PostingType As DataElements.ListItem() =
           CommonConfigManager.Current.ListManager.GetList(listCode:="POSTTYPE",
                                                           languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Me.moPostingTypeDDL.Populate(PostingType.ToArray(), populateOptions)


        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 0 Then
            For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
                Me.moAccountingCompanyDropDown.Items.Add(New ListItem(_acctCo.Description, _acctCo.Id.ToString))
            Next
            If Me.moAccountingCompanyDropDown.Items.Count > 1 Then
                ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, True)
                Me.moAccountingCompanyDropDown.Items.Add(New ListItem("", Me.NOTHING_SELECTED))
                Me.moAccountingCompanyDropDown.SelectedValue = Me.NOTHING_SELECTED
            ElseIf Me.moAccountingCompanyDropDown.Items.Count = 1 Then
                ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, False)
            End If
        End If

    End Sub

    Protected Sub CreateNew()
        Me.moAccountingEventGrid.CurrentPageIndex = 0
        Me.State.ScreenSnapShotBO = Nothing
        Me.State.MyBO = New AcctEvent
    End Sub

    Protected Sub PopulateFormFromBOs()

        Dim YesNodv As DataView = Me.State.YESNOdv

        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.moJournalTypeTEXT, .JournalType)
            Me.PopulateControlFromBOProperty(Me.moPostToHoldTEXT, .PostToHold)
            Me.PopulateControlFromBOProperty(Me.moReportingAccountTEXT, .ReportingAccount)
            Me.PopulateControlFromBOProperty(Me.moSuspenseAccountTEXT, .SuspenseAccount)
            Me.PopulateControlFromBOProperty(Me.moTransactionAmountAccountTEXT, .TransactionAmountAccount)
            Me.PopulateControlFromBOProperty(Me.moLayoutCodeTEXT, .LayoutCode)
            Me.PopulateControlFromBOProperty(Me.moEventDescriptionTEXT, .EventDescription)

            'Fill Drop Down Lists
            Me.PopulateControlFromBOProperty(Me.moSuppressSubstitutedMessagesDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .SuppressSubstitutedMessages))
            Me.PopulateControlFromBOProperty(Me.moPostProvisionalDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .PostProvisional))
            Me.PopulateControlFromBOProperty(Me.moLoadOnlyDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .LoadOnly))
            Me.PopulateControlFromBOProperty(Me.moAllowBalTranDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowBalTran))
            Me.PopulateControlFromBOProperty(Me.moAllowOverBudgetDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowOverBudget))
            Me.PopulateControlFromBOProperty(Me.moAllowPostToSuspendedDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .AllowPostToSuspended))
            Me.PopulateControlFromBOProperty(Me.moBalancingOptionsDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCTBO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .BalancingOptions))
            Me.PopulateControlFromBOProperty(Me.moPostingTypeDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_POSTINGTYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .PostingType))
            Me.PopulateControlFromBOProperty(Me.moJournalLevelDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_JOURNALLEVEL, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .JournalLevel))

            Me.PopulateControlFromBOProperty(Me.moEventTypeDDL, .AcctEventTypeId)

            If Not .AcctCompanyId = Guid.Empty Then

                If Me.moAccountingCompanyDropDown.Items.Count > 1 Then
                    Me.PopulateControlFromBOProperty(Me.moAccountingCompanyDropDown, .AcctCompanyId)
                Else
                    Me.moAccountingCompanyDropDown.SelectedIndex = 0
                End If

            Else

                If Me.State.MyBO.IsNew Then
                    If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 1 Then
                        Me.PopulateBOProperty(Me.State.MyBO, ACCTCOMPANY_PROPERTY, Me.moAccountingCompanyDropDown)
                    Else
                        Me.State.MyBO.AcctCompanyId = ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).Id
                    End If
                End If
            End If

            PopulateEventDetails()

        End With

    End Sub

    Private Sub PopulateEventDetails()

        Dim dv As AcctEventDetail.AcctEventDetailSearchDV
        dv = AcctEventDetail.getList(Me.State.MyBO.Id)
        If Not dv Is Nothing Then
            Me.moAccountingEventGrid.DataSource = dv
            Me.moAccountingEventGrid.DataBind()
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        With Me.State
            Me.BindBOPropertyToLabel(.MyBO, JOURNALTYPE_PROPERTY, Me.moJournalTypeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, LOADONLY_PROPERTY, Me.moLoadOnlyLABEL)
            Me.BindBOPropertyToLabel(.MyBO, POSTINGTYPE_PROPERTY, Me.moPostingTypeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, POSTPROVISIONAL_PROPERTY, Me.moPostProvisionalLABEL)
            Me.BindBOPropertyToLabel(.MyBO, POSTTOHOLD_PROPERTY, Me.moPostToHoldLABEL)
            Me.BindBOPropertyToLabel(.MyBO, REPORTINGACCOUNT_PROPERTY, Me.moReportingAccountLABEL)
            Me.BindBOPropertyToLabel(.MyBO, SUPPRESSSUBSTITUTEDMESSAGES_PROPERTY, Me.moSuppressSubstitutedMessagesLABEL)
            Me.BindBOPropertyToLabel(.MyBO, SUSPENSEACCOUNT_PROPERTY, Me.moSuspenseAccountLABEL)
            Me.BindBOPropertyToLabel(.MyBO, TRANSACTIONAMOUNTACCOUNT_PROPERTY, Me.moTransactionAmountAccountLABEL)
            Me.BindBOPropertyToLabel(.MyBO, JOURNALLEVEL_PROPERTY, Me.moJournalLevelLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ACCTCOMPANY_PROPERTY, Me.moAccountingCompanyLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ALLOWBALTRANSFER_PROPERTY, Me.moAllowBalTranLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ALLOWOVERBUDGET_PROPERTY, Me.moAllowOverBudgetLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ALLOWPOSTTOSUSPENDED_PROPERTY, Me.moAllowPostToSuspendedLABEL)
            Me.BindBOPropertyToLabel(.MyBO, BALANCINGOPTIONS_PROPERTY, Me.moBalancingOptionsLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ACCTEVENTTYPE_PROPERTY, Me.moEventTypeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, LAYOUTCODE_PROPERTY, Me.moLayoutCodeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, EVENTDESCRIPTION_PROPERTY, Me.moEventDescriptionLABEL)

        End With

        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        'Enabled by Default

        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, Me.btnBack, True)
        ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, True)
        ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, True)

        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, True)
        Else
            ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, False)
        End If

        EnableDisableJournalLevelControls

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Redirect_
                    Me.callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(Me.State.SelectedDetailId, Me.State.MyBO))
                Case DetailPageCommand.Delete
                    DeleteEventDetailItem()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.State

            Me.PopulateBOProperty(.MyBO, JOURNALTYPE_PROPERTY, Me.moJournalTypeTEXT)
            Me.PopulateBOProperty(.MyBO, POSTTOHOLD_PROPERTY, Me.moPostToHoldTEXT)
            Me.PopulateBOProperty(.MyBO, REPORTINGACCOUNT_PROPERTY, Me.moReportingAccountTEXT)
            Me.PopulateBOProperty(.MyBO, SUSPENSEACCOUNT_PROPERTY, Me.moSuspenseAccountTEXT)
            Me.PopulateBOProperty(.MyBO, TRANSACTIONAMOUNTACCOUNT_PROPERTY, Me.moTransactionAmountAccountTEXT)
            Me.PopulateBOProperty(.MyBO, LAYOUTCODE_PROPERTY, Me.moLayoutCodeTEXT)
            Me.PopulateBOProperty(.MyBO, EVENTDESCRIPTION_PROPERTY, Me.moEventDescriptionTEXT)

            'Fill Drop Down Lists
            Me.PopulateBOProperty(.MyBO, SUPPRESSSUBSTITUTEDMESSAGES_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moSuppressSubstitutedMessagesDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, JOURNALLEVEL_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_JOURNALLEVEL, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(Me.moJournalLevelDDL.SelectedValue)))

            Me.PopulateBOProperty(.MyBO, POSTPROVISIONAL_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moPostProvisionalDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, LOADONLY_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moLoadOnlyDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, ALLOWBALTRANSFER_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moAllowBalTranDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, ALLOWOVERBUDGET_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moAllowOverBudgetDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, ALLOWPOSTTOSUSPENDED_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moAllowPostToSuspendedDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, BALANCINGOPTIONS_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCTBO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(Me.moBalancingOptionsDDL.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, POSTINGTYPE_PROPERTY, ElitaPlus.BusinessObjectsNew.LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_POSTINGTYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(Me.moPostingTypeDDL.SelectedValue)))

            Me.PopulateBOProperty(.MyBO, ACCTEVENTTYPE_PROPERTY, Me.moEventTypeDDL)

            If .MyBO.IsNew Then
                If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 1 Then
                    Me.PopulateBOProperty(.MyBO, ACCTCOMPANY_PROPERTY, Me.moAccountingCompanyDropDown)
                Else
                    .MyBO.AcctCompanyId = ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).Id
                End If
            End If

        End With


    End Sub

    Private Sub DeleteEventDetailItem()

        Try
            Dim _eventDetail As New AcctEventDetail(Me.State.SelectedDetailId)
            _eventDetail.Delete()
            _eventDetail.Save()
            _eventDetail = Nothing

            Me.moAccountingEventGrid.CurrentPageIndex = 0
            PopulateEventDetails()
            Me.State.SelectedDetailId = Guid.Empty
            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub CreateNewWithCopy()

        Dim newBO As New AcctEvent

        newBO.AcctCompanyId = Me.State.MyBO.AcctCompanyId
        Me.State.MyBO = newBO

        'Reset the source  and business unit fields
        Me.moEventTypeDDL.SelectedIndex = 0

        Me.PopulateBOsFromForm()
        PopulateEventDetails()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New AcctEvent
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New AcctEvent(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
            Else
                Me.callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(Guid.Empty, Me.State.MyBO))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


#End Region

#Region "DataGrid Methods"

    Private Sub moAccountingEventGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moAccountingEventGrid.ItemCommand

        Try
            If e.CommandName = "SelectAction" Then
                Try
                    Me.PopulateBOsFromForm()
                    If Me.State.MyBO.IsDirty Then
                        Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
                        Me.State.SelectedDetailId = New Guid(e.Item.Cells(Me.GRIDCOL_EVENTDETAIL_ID).Text)
                    Else
                        Me.callPage(AccountingEventDetailForm.URL, New AccountingEventDetailForm.ReturnType(New Guid(e.Item.Cells(Me.GRIDCOL_EVENTDETAIL_ID).Text), Me.State.MyBO))
                    End If
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.ErrControllerMaster)
                    Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.State.LastErrMsg = Me.ErrControllerMaster.Text
                End Try
            ElseIf e.CommandName = "DeleteRecord" Then
                Me.State.ActionInProgress = DetailPageCommand.Delete
                Me.State.SelectedDetailId = New Guid(e.Item.Cells(Me.GRIDCOL_EVENTDETAIL_ID).Text)
                Me.AddConfirmMsg(Message.DELETE_RECORD_PROMPT, Me.HiddenSaveChangesPromptResponse)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub moAccountingEventGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moAccountingEventGrid.ItemDataBound

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(Me.GRIDCOL_EVENTDETAIL_ID).Text = GetGuidStringFromByteArray(CType(dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_ACCT_EVENT_DETAIL_ID), Byte()))
            e.Item.Cells(Me.GRIDCOL_BUSINESS_UNIT).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_BUSINESS_UNIT).ToString
            e.Item.Cells(Me.GRIDCOL_DEBITCREDIT).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_DEBIT_CREDIT).ToString
            e.Item.Cells(Me.GRIDCOL_FIELDTYPE).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_FIELD_TYPE).ToString
            e.Item.Cells(Me.GRIDCOL_ACCOUNTCODE).Text = dvRow(AcctEventDetail.AcctEventDetailSearchDV.COL_ACCOUNT_CODE).ToString
        End If

    End Sub

    Private Sub moAccountingEventGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moAccountingEventGrid.ItemCreated
        Dim pg As New ElitaPlusSearchPage
        pg.BaseItemCreated(sender, e)
    End Sub

    Private Sub moAccountingEventGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moAccountingEventGrid.PageIndexChanged

        Me.moAccountingEventGrid.CurrentPageIndex = e.NewPageIndex
        PopulateEventDetails()

    End Sub

    Private sub EnableDisableJournalLevelControls()
        Dim acctCompanyBo As AcctCompany = New AcctCompany(Me.State.MyBO.AcctCompanyId)
        dim eventType as String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_EVENT_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GetSelectedItem(moEventTypeDDL))


        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM,
                                                                        ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), AcctCompanyBO.AcctSystemId) = AcctSystemFelita AndAlso
           (eventType= ACCTEVENTTYPE_PREMIUM OrElse eventType= ACCTEVENTTYPE_UPR) Then 'Journal level configuration enabled only for Premium and UPR events

            ControlMgr.SetVisibleControl(Me, Me.moJournalLevelDDL, True)
            ControlMgr.SetVisibleControl(Me, Me.moJournalLevelLABEL, True)

            if State.MyBO isnot Nothing Then
                PopulateControlFromBOProperty(moJournalLevelDDL, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_JOURNALLEVEL, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), State.MyBO.JournalLevel))
            End If
            
        Else
            moJournalLevelDDL.SelectedIndex = -1
            ControlMgr.SetVisibleControl(Me, Me.moJournalLevelDDL, False)
            ControlMgr.SetVisibleControl(Me, Me.moJournalLevelLABEL, False)
        End If
    End sub
    Private Sub moEventTypeDDL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moEventTypeDDL.SelectedIndexChanged
        EnableDisableJournalLevelControls()

    End Sub

#End Region

End Class
