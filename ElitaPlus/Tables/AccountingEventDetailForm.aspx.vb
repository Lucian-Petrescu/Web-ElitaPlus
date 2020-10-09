Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

Partial Class AccountingEventDetailForm
    Inherits ElitaPlusSearchPage

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


#Region "Constants"
    Public Shared URL As String = "AccountingEventDetailForm.aspx"
    Public Const ENTITYNAME_PROPERTY As String = "EntityName"

    Public Const ACCOUNTCODE_PROPERTY As String = "AccountCode"
    Public Const ACCOUNTEVENTID_PROPERTY As String = "AcctEventId"
    Public Const ALLOCATIONMARKER_PROPERTY As String = "AllocationMarker"
    Public Const ANALYSISCODE1_PROPERTY As String = "AnalysisCode1"
    Public Const ANALYSISCODE2_PROPERTY As String = "AnalysisCode2"
    Public Const ANALYSISCODE3_PROPERTY As String = "AnalysisCode3"
    Public Const ANALYSISCODE4_PROPERTY As String = "AnalysisCode4"
    Public Const ANALYSISCODE5_PROPERTY As String = "AnalysisCode5"
    Public Const ANALYSISCODE6_PROPERTY As String = "AnalysisCode6"
    Public Const ANALYSISCODE7_PROPERTY As String = "AnalysisCode7"
    Public Const ANALYSISCODE8_PROPERTY As String = "AnalysisCode8"
    Public Const ANALYSISCODE9_PROPERTY As String = "AnalysisCode9"
    Public Const ANALYSISCODE10_PROPERTY As String = "AnalysisCode10"
    Public Const CALCULATION_PROPERTY As String = "Calculation"
    Public Const DEBITCREDIT_PROPERTY As String = "DebitCredit"
    Public Const FIELDTYPEID_PROPERTY As String = "FieldTypeId"
    Public Const JOURNALSOURCE_PROPERTY As String = "JournalSource"
    Public Const JOURNALTYPE_PROPERTY As String = "JournalType"
    Public Const USEPAYEESETTINGS_PROPERTY As String = "UsePayeeSettings"
    Public Const ACCTBUSINESSUNIT_PROPERTY As String = "AcctBusinessUnitId"
    Public Const ACCOUNTTYPE_PROPERTY As String = "AccountType"
    Public Const JOURNALID_PROPERTY As String = "JournalIDSuffix"
    Public Const DESCRIPTION_PROPERTY As String = "Description"
    Public Const BUSINESSENTITY_PROPERTY As String = "BusinessEntityId"
    Public Const ANALYSISSOURCE1ID_PROPERTY As String = "AnalysisSource1Id"
    Public Const ANALYSISSOURCE2ID_PROPERTY As String = "AnalysisSource2Id"
    Public Const ANALYSISSOURCE3ID_PROPERTY As String = "AnalysisSource3Id"
    Public Const ANALYSISSOURCE4ID_PROPERTY As String = "AnalysisSource4Id"
    Public Const ANALYSISSOURCE5ID_PROPERTY As String = "AnalysisSource5Id"
    Public Const ANALYSISSOURCE6ID_PROPERTY As String = "AnalysisSource6Id"
    Public Const ANALYSISSOURCE7ID_PROPERTY As String = "AnalysisSource7Id"
    Public Const ANALYSISSOURCE8ID_PROPERTY As String = "AnalysisSource8Id"
    Public Const ANALYSISSOURCE9ID_PROPERTY As String = "AnalysisSource9Id"
    Public Const ANALYSISSOURCE10ID_PROPERTY As String = "AnalysisSource10Id"
    Public Const DESCRIPTIONSOURCEID_PROPERTY As String = "DescriptionSourceId"

    Public Const COL_CODE As String = "CODE"
    Public Const COL_ID As String = "ID"
    Public Const YES_VALUE As String = "'Y'"
    Public Const NO_VALUE As String = "'N'"
    Private Const YESNO As String = "YESNO"
    Private Const ALLOCMRK As String = "ALLOCMRK"
    Private Const ACCTTYPE_DEF As String = "0"

    Private ACCT_SYSTEM As String = ""

    Public Const PAGETITLE As String = "ACCOUNT_EVENT_DETAIL_FORM"
    Public Const PAGETAB As String = "TABLES"

#End Region

#Region "Tabs"
    Public Const Tabs_CodeConfiguration As String = "0"
    Public Const Tabs_InclusionExclisionConfig As String = "1"

    Dim DisabledTabsList As New List(Of String)()
#End Region

#Region "Page State"

    Class MyState
        Public ParentBO As AcctEvent
        Public MyBO As AcctEventDetail
        Public ScreenSnapShotBO As AcctEventDetail
        Public AcctCompanyBo As AcctCompany
        Public IsNew As Boolean = False
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        'Public YESNOdv As DataView
        Public YESNOList As DataElements.ListItem()
        'CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        'Public AcctTSrcdv As DataView

        Public IncludeExcludeAction As Integer = INCEXC_None
        Public IncludeExcludeList As Collections.Generic.List(Of AcctEventDetailIncExc)
        Public IncludeExcludeWorkingItem As AcctEventDetailIncExc
        Public IncludeExcludeWorkingOrig As AcctEventDetailIncExc
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
                Dim CalPar As ReturnType = CType(CallingPar, ReturnType)
                State.ParentBO = CalPar.EditingBo
                If Not CalPar.SelectedGuid.Equals(Guid.Empty) Then
                    State.MyBO = New AcctEventDetail(CalPar.SelectedGuid)
                    Exit Sub
                End If
            End If
            State.IsNew = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public EditingBo As AcctEvent
        Public SelectedGuid As Guid = Guid.Empty

        Public Sub New(EventDetailId As Guid, curEditingBo As AcctEvent)
            EditingBo = curEditingBo
            SelectedGuid = EventDetailId
        End Sub

    End Class
#End Region

#Region "Page Events"
    Private AcctEventType As String = String.Empty

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()

        SetFormTitle(PAGETITLE)
        SetFormTab(PAGETAB)

        Try
            If Not IsPostBack Then
                TranslateGridHeader(moGridView)

                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                UpdateBreadCrum()

                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                PopulateAll()

                If State.IsNew = True Then
                    CreateNew()
                End If

                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            GetDisabledTabs()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub GetDisabledTabs()
        Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
        If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
            DisabledTabsList.AddRange(DisabledTabs)
            hdnDisabledTab.Value = String.Empty
        End If
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        'enable the inclusion/exclusion configuration tab only for PREM, REFUNDS, UPR, IBNR, claim and claim reserve event types
        'disabling for Vendor and Invoice
        If AcctEventType = String.Empty Then
            Dim AcctEventTypedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            AcctEventType = LookupListNew.GetCodeFromId(AcctEventTypedv, State.ParentBO.AcctEventTypeId)
        End If

        If AcctEventType = "VEND" OrElse AcctEventType = "INV" Then
            DisabledTabsList.Add(Tabs_InclusionExclisionConfig)
        End If
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateAll()

        State.YESNOList = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        'Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'Me.State.YESNOdv = YESNOdv

        'Dim AcctTSrcdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_ANALYSIS_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
        'Me.State.AcctTSrcdv = AcctTSrcdv
        Dim AcctTSrList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTCODE", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        'Dim AcctEventTypedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'Me.BindListControlToDataView(Me.moAccountingEventDROPDOWN, AcctEventTypedv)
        'AcctEventType = LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId)

        Dim AcctEventTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTRANSTYP", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moAccountingEventDROPDOWN.Populate(AcctEventTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        AcctEventType = (From lst In AcctEventTypeList
                         Where lst.ListItemId = State.ParentBO.AcctEventTypeId
                         Select lst.Code).FirstOrDefault()

        'Dim AcctFieldTypedv As DataView = LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_ACCT_FIELD_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        'AcctFieldTypedv.RowFilter = String.Format("{0}code like '{1}_%'", If(AcctFieldTypedv.RowFilter.Length > 0, String.Format("{0} AND ", AcctFieldTypedv.RowFilter), "").ToString, LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId))
        'Me.BindListControlToDataView(Me.moFieldTypeDROPDOWN, AcctFieldTypedv)

        Dim AcctFieldTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTFIELDTYP", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Dim AcctEventTypeFilter = (From lst In AcctEventTypeList
                                   Where lst.ListItemId = State.ParentBO.AcctEventTypeId
                                   Select lst.Code).FirstOrDefault()

        Dim FilteredAcctFieldTypeList As DataElements.ListItem() = (From lst In AcctFieldTypeList
                                                                    Where lst.Code.StartsWith(AcctEventTypeFilter)
                                                                    Select lst).ToArray()

        moFieldTypeDROPDOWN.Populate(FilteredAcctFieldTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'REQ-5733 Start / Displaying Product_code Drop down Option only for Claimns,Claims Reserve,Premium and Premium Refund 
        'Dim h As String = LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId)
        Dim h As String = (From lst In AcctEventTypeList
                           Where lst.ListItemId = State.ParentBO.AcctEventTypeId
                           Select lst.Code).FirstOrDefault()

        If (h = "UPR" OrElse h = "IBNR" OrElse h = "INV" OrElse h = "VEND") Then
            AcctTSrList = (From lst In AcctTSrList
                           Where lst.Code <> "PRDCODE"
                           Select lst).ToArray()
            'AcctTSrcdv.RowFilter = String.Format("{0}code <> 'PRDCODE'", If(AcctTSrcdv.RowFilter.Length > 0, String.Format("{0} AND ", AcctTSrcdv.RowFilter), "").ToString)
        End If
        'REQ-5733 END

        'Me.BindListControlToDataView(Me.moUsePayeeSettingsDROPDOWN, YESNOdv)
        moUsePayeeSettingsDROPDOWN.Populate(State.YESNOList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        'Me.BindListControlToDataView(Me.moDebitCreditDROPDOWN, BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim CreditDebitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CRDBT", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moDebitCreditDROPDOWN.Populate(CreditDebitList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moAccountingCompanyDROPDOWN, LookupListNew.DataView(LookupListNew.LK_ACCTCOMPANY))
        Dim AcctCompanyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AcctCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moAccountingCompanyDROPDOWN.Populate(AcctCompanyList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moBusinessUnitDDL, AcctBusinessUnit.getList(Me.State.ParentBO.AcctCompanyId, Nothing), AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID)
        Dim olistcontext As ListContext = New ListContext()
        olistcontext.AccountingCompanyId = State.ParentBO.AcctCompanyId
        Dim BusinessUnitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessUnitByAcctCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=olistcontext)
        moBusinessUnitDDL.Populate(BusinessUnitList, New PopulateOptions() With
            {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
            })

        'Me.BindListControlToDataView(Me.cboAccountType, LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim AcctTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CRDBT_NUM", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        cboAccountType.Populate(AcctTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moBusinessEntityDROPDOWN, BusinessObjectsNew.LookupListNew.getBusinessCoverageEntity(Me.State.ParentBO.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim listcontext As ListContext = New ListContext()
        listcontext.AccountingCompanyId = State.ParentBO.AcctCompanyId
        listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim BusinessEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessEntityByAccountingCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=listcontext)
        moBusinessEntityDROPDOWN.Populate(BusinessEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moAcctAnal1DDL, AcctTSrcdv)
        moAcctAnal1DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal2DDL, AcctTSrcdv)
        moAcctAnal2DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal3DDL, AcctTSrcdv)
        moAcctAnal3DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal4DDL, AcctTSrcdv)
        moAcctAnal4DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal5DDL, AcctTSrcdv)
        moAcctAnal5DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal6DDL, AcctTSrcdv)
        moAcctAnal6DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal7DDL, AcctTSrcdv)
        moAcctAnal7DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal8DDL, AcctTSrcdv)
        moAcctAnal8DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal9DDL, AcctTSrcdv)
        moAcctAnal9DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal10DDL, AcctTSrcdv)
        moAcctAnal10DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moDescriptionDDL, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_DESC_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim AcctDescSourceList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTDESC", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moDescriptionDDL.Populate(AcctDescSourceList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        ddlIncludeExcludeType.Items.Clear()
        ddlIncludeExcludeType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("Exclusion"), "E"))
        ddlIncludeExcludeType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("Inclusion"), "I"))

    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing
        State.MyBO = New AcctEventDetail
        State.MyBO.AcctEventId = State.ParentBO.Id
        'Clear the inclusion/exclusion list
        ClearIncludeExcludeState()
    End Sub

    Protected Sub PopulateFormFromBOs()

        'Dim YesNodv As DataView = Me.State.YESNOdv

        With State.MyBO

            PopulateControlFromBOProperty(moAccountCodeTEXT, .AccountCode)
            PopulateControlFromBOProperty(moAcctAnal1TEXT, .AnalysisCode1)
            PopulateControlFromBOProperty(moAcctAnal2TEXT, .AnalysisCode2)
            PopulateControlFromBOProperty(moAcctAnal3TEXT, .AnalysisCode3)
            PopulateControlFromBOProperty(moAcctAnal4TEXT, .AnalysisCode4)
            PopulateControlFromBOProperty(moAcctAnal5TEXT, .AnalysisCode5)
            PopulateControlFromBOProperty(moAcctAnal6TEXT, .AnalysisCode6)
            PopulateControlFromBOProperty(moAcctAnal7TEXT, .AnalysisCode7)
            PopulateControlFromBOProperty(moAcctAnal8TEXT, .AnalysisCode8)
            PopulateControlFromBOProperty(moAcctAnal9TEXT, .AnalysisCode9)
            PopulateControlFromBOProperty(moAcctAnal10TEXT, .AnalysisCode10)
            PopulateControlFromBOProperty(moCalculationTEXT, .Calculation)
            PopulateControlFromBOProperty(moJournalIdTEXT, .JournalIDSuffix)
            PopulateControlFromBOProperty(moDescriptionTEXT, .Description)

            'Fill drop downs
            PopulateControlFromBOProperty(moFieldTypeDROPDOWN, .FieldTypeId)
            PopulateControlFromBOProperty(moDebitCreditDROPDOWN, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId), .DebitCredit))
            'Me.PopulateControlFromBOProperty(Me.moUsePayeeSettingsDROPDOWN, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .UsePayeeSettings))
            PopulateControlFromBOProperty(moUsePayeeSettingsDROPDOWN, (From ynList In State.YESNOList
                                                                             Where ynList.Code = .UsePayeeSettings
                                                                             Select ynList.ListItemId).FirstOrDefault())
            PopulateControlFromBOProperty(cboAccountType, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .AccountType))
            PopulateControlFromBOProperty(moBusinessUnitDDL, .AcctBusinessUnitId)
            PopulateControlFromBOProperty(moBusinessEntityDROPDOWN, .BusinessEntityId)

            PopulateControlFromBOProperty(moDescriptionDDL, .DescriptionSourceId)
            PopulateControlFromBOProperty(moAcctAnal1DDL, .AnalysisSource1Id)
            PopulateControlFromBOProperty(moAcctAnal2DDL, .AnalysisSource2Id)
            PopulateControlFromBOProperty(moAcctAnal3DDL, .AnalysisSource3Id)
            PopulateControlFromBOProperty(moAcctAnal4DDL, .AnalysisSource4Id)
            PopulateControlFromBOProperty(moAcctAnal5DDL, .AnalysisSource5Id)
            PopulateControlFromBOProperty(moAcctAnal6DDL, .AnalysisSource6Id)
            PopulateControlFromBOProperty(moAcctAnal7DDL, .AnalysisSource7Id)
            PopulateControlFromBOProperty(moAcctAnal8DDL, .AnalysisSource8Id)
            PopulateControlFromBOProperty(moAcctAnal9DDL, .AnalysisSource9Id)
            PopulateControlFromBOProperty(moAcctAnal10DDL, .AnalysisSource10Id)


            'Fill readonly DDLs
            PopulateControlFromBOProperty(moAccountingCompanyDROPDOWN, State.ParentBO.AcctCompanyId)
            PopulateControlFromBOProperty(moAccountingEventDROPDOWN, State.ParentBO.AcctEventTypeId)

            State.AcctCompanyBo = New AcctCompany(State.ParentBO.AcctCompanyId)

            If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Count > 0 Then
                ACCT_SYSTEM = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.AcctCompanyBo.AcctSystemId)
            End If

            If ACCT_SYSTEM = BusinessObjectsNew.FelitaEngine.FELITA_PREFIX Then
                EnableDisableControls(moJournalTypeTEXT, True)
                PopulateControlFromBOProperty(moJournalTypeTEXT, State.ParentBO.JournalType)

                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moJournalIdLABEL, False)
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moJournalIdTEXT, False)
            Else
                EnableDisableControls(moJournalTypeTEXT, False)
                PopulateControlFromBOProperty(moJournalTypeTEXT, .JournalType)

                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moJournalIdLABEL, True)
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moJournalIdTEXT, True)

            End If

            'If LookupListNew.GetCodeFromId(Me.State.YESNOdv, Me.State.AcctCompanyBo.UseCoverageEntityId) = YES_VALUE.Trim(CChar("'")) Then
            If (From ynList In State.YESNOList
                Where ynList.ListItemId = State.AcctCompanyBo.UseCoverageEntityId
                Select ynList.Code).FirstOrDefault() = "Y" Then
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moBusinessEntityLABEL, True)
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moBusinessEntityDROPDOWN, True)
            Else
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moBusinessEntityLABEL, False)
                ControlMgr.SetVisibleControl(CType(Page, ElitaPlusPage), moBusinessEntityDROPDOWN, False)
            End If

            SetSelectedItem(ddlIncludeExcludeType, .IncludeExcludeInd)
        End With

        If State.IncludeExcludeList Is Nothing Then
            State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(State.MyBO.Id)
        End If
        PopulateInclusionExclusionGrid(State.IncludeExcludeList)

    End Sub

    Protected Sub PopulateBOsFromForm()

        With State

            PopulateBOProperty(.MyBO, ACCOUNTCODE_PROPERTY, moAccountCodeTEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE1_PROPERTY, moAcctAnal1TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE2_PROPERTY, moAcctAnal2TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE3_PROPERTY, moAcctAnal3TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE4_PROPERTY, moAcctAnal4TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE5_PROPERTY, moAcctAnal5TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE6_PROPERTY, moAcctAnal6TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE7_PROPERTY, moAcctAnal7TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE8_PROPERTY, moAcctAnal8TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE9_PROPERTY, moAcctAnal9TEXT)
            PopulateBOProperty(.MyBO, ANALYSISCODE10_PROPERTY, moAcctAnal10TEXT)
            PopulateBOProperty(.MyBO, CALCULATION_PROPERTY, moCalculationTEXT)
            PopulateBOProperty(.MyBO, JOURNALTYPE_PROPERTY, moJournalTypeTEXT)
            PopulateBOProperty(.MyBO, JOURNALID_PROPERTY, moJournalIdTEXT)

            PopulateBOProperty(.MyBO, ACCTBUSINESSUNIT_PROPERTY, moBusinessUnitDDL)
            PopulateBOProperty(.MyBO, ACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(cboAccountType.SelectedItem.Value)))
            PopulateBOProperty(.MyBO, DEBITCREDIT_PROPERTY, BusinessObjectsNew.LookupListNew.GetCodeFromId(BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(moDebitCreditDROPDOWN.SelectedValue)))
            PopulateBOProperty(.MyBO, FIELDTYPEID_PROPERTY, moFieldTypeDROPDOWN)

            PopulateBOProperty(.MyBO, DESCRIPTION_PROPERTY, moDescriptionTEXT)
            PopulateBOProperty(.MyBO, BUSINESSENTITY_PROPERTY, moBusinessEntityDROPDOWN)

            PopulateBOProperty(.MyBO, ANALYSISSOURCE1ID_PROPERTY, moAcctAnal1DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE2ID_PROPERTY, moAcctAnal2DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE3ID_PROPERTY, moAcctAnal3DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE4ID_PROPERTY, moAcctAnal4DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE5ID_PROPERTY, moAcctAnal5DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE6ID_PROPERTY, moAcctAnal6DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE7ID_PROPERTY, moAcctAnal7DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE8ID_PROPERTY, moAcctAnal8DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE9ID_PROPERTY, moAcctAnal9DDL)
            PopulateBOProperty(.MyBO, ANALYSISSOURCE10ID_PROPERTY, moAcctAnal10DDL)
            PopulateBOProperty(.MyBO, DESCRIPTIONSOURCEID_PROPERTY, moDescriptionDDL)

            Dim vendSettings As String
            If (New Guid(moUsePayeeSettingsDROPDOWN.SelectedValue)).Equals(Guid.Empty) Then
                Dim c() As Char = New Char() {CChar("'")}
                vendSettings = NO_VALUE.Trim(c)
            Else
                vendSettings = (From ynList In State.YESNOList
                                Where ynList.ListItemId = New Guid(moUsePayeeSettingsDROPDOWN.SelectedValue)
                                Select ynList.Code).FirstOrDefault()
                'vendSettings = BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moUsePayeeSettingsDROPDOWN.SelectedValue))
            End If
            PopulateBOProperty(.MyBO, USEPAYEESETTINGS_PROPERTY, vendSettings)
            PopulateBOProperty(.MyBO, "IncludeExcludeInd", ddlIncludeExcludeType.SelectedValue)

        End With

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        With State
            BindBOPropertyToLabel(.MyBO, ACCOUNTCODE_PROPERTY, moAccountCodeLABEL)
            BindBOPropertyToLabel(.MyBO, ACCOUNTTYPE_PROPERTY, moAccountTypeLABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE1ID_PROPERTY, moAcctAnal1LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE2ID_PROPERTY, moAcctAnal2LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE3ID_PROPERTY, moAcctAnal3LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE4ID_PROPERTY, moAcctAnal4LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE5ID_PROPERTY, moAcctAnal5LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE6ID_PROPERTY, moAcctAnal6LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE7ID_PROPERTY, moAcctAnal7LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE8ID_PROPERTY, moAcctAnal8LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE9ID_PROPERTY, moAcctAnal9LABEL)
            BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE10ID_PROPERTY, moAcctAnal10LABEL)
            BindBOPropertyToLabel(.MyBO, CALCULATION_PROPERTY, moCalculationLABEL)
            BindBOPropertyToLabel(.MyBO, DEBITCREDIT_PROPERTY, moDebitCreditLABEL)
            BindBOPropertyToLabel(.MyBO, FIELDTYPEID_PROPERTY, moFieldTypeIdLABEL)
            BindBOPropertyToLabel(.MyBO, JOURNALTYPE_PROPERTY, moJournalTypeLABEL)
            BindBOPropertyToLabel(.MyBO, USEPAYEESETTINGS_PROPERTY, moUsePayeeSettingsLABEL)
            BindBOPropertyToLabel(.MyBO, ACCTBUSINESSUNIT_PROPERTY, moBusinessUnitLABEL)
            BindBOPropertyToLabel(.MyBO, JOURNALID_PROPERTY, moJournalIdLABEL)
            BindBOPropertyToLabel(.MyBO, DESCRIPTION_PROPERTY, moDescriptionLABEL)
            BindBOPropertyToLabel(.MyBO, BUSINESSENTITY_PROPERTY, moBusinessEntityLABEL)

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
        End If

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Try
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Dim blnNewBO As Boolean = State.MyBO.IsNew
                    If blnNewBO AndAlso State.MyBO.IncludeExcludeInd = "I" AndAlso (State.IncludeExcludeList Is Nothing OrElse State.IncludeExcludeList.Count = 0) Then
                        'new BO, validate at least one configuration exists first
                        MasterPage.MessageController.AddError("AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
                        Try
                            State.MyBO.Validate() 'show other validation error also
                        Catch ex As Exception
                            HandleErrors(ex, MasterPage.MessageController)
                        End Try
                        Exit Sub
                    End If
                    State.MyBO.Save()
                    SaveInclusionExclusionRecords(blnNewBO) 'new line items, save Child records added in memory
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New AccountingEventForm.ReturnType(State.ActionInProgress, State.ParentBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New AccountingEventForm.ReturnType(State.ActionInProgress, State.ParentBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New AccountingEventForm.ReturnType(State.ActionInProgress, State.ParentBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
        Catch ex As Exception
            Throw ex
        Finally
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Try
    End Sub

    Protected Sub CreateNewWithCopy()

        Dim newBO As New AcctEventDetail

        newBO.AcctEventId = State.MyBO.AcctEventId
        State.MyBO = newBO

        'Reset the source field
        moFieldTypeDROPDOWN.SelectedIndex = 0
        moBusinessUnitDDL.SelectedIndex = 0

        'Clear the inclusion/exclusion list
        ClearIncludeExcludeState()

        PopulateBOsFromForm()

        'create the backup copy
        State.ScreenSnapShotBO = New AcctEventDetail
        State.ScreenSnapShotBO.Clone(State.MyBO)
    End Sub

    <System.Web.Services.WebMethod()> _
    <Script.Services.ScriptMethod()> _
    Public Shared Function IncludeVendors(BusinessUnit As String) As String

        Dim c() As Char = New Char() {CChar("'")}

        Try
            Dim g As New Guid(BusinessUnit)

            If Not g.Equals(Guid.Empty) Then
                Dim bu As New AcctBusinessUnit(g)
                If bu IsNot Nothing Then
                    If bu.SuppressVendors = YES_VALUE.Trim(c) Then
                        Return String.Format("{0}|{1}", YES_VALUE.Trim(c), LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, NO_VALUE.Trim(c)).ToString)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        Return String.Format("{0}|{1}", NO_VALUE.Trim(c), " ")

    End Function


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New AccountingEventForm.ReturnType(State.ActionInProgress, State.ParentBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New AcctEventDetail(State.MyBO.Id)
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                Dim blnNewBO As Boolean = State.MyBO.IsNew
                If blnNewBO AndAlso State.MyBO.IncludeExcludeInd = "I" AndAlso (State.IncludeExcludeList Is Nothing OrElse State.IncludeExcludeList.Count = 0) Then
                    'new BO, validate at least one configuration exists first
                    MasterPage.MessageController.AddError("AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
                    Try
                        State.MyBO.Validate() 'show other validation error also
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                    End Try
                    Exit Sub
                End If
                State.MyBO.Save()
                SaveInclusionExclusionRecords(blnNewBO) 'new line items, save Child records added in memory

                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DeleteInclusionExclusionRecords() 'delete Child records first
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New AccountingEventForm.ReturnType(State.ActionInProgress, State.ParentBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Handlers - Inclusion Exclusion Tab"
    Public Const GRID_COL_DEALER_IDX As Integer = 0
    Public Const GRID_COL_COVERATGE_TYPE_IDX As Integer = 1

    Public Const INCEXC_None As Integer = 0
    Public Const INCEXC_Add As Integer = 1
    Public Const INCEXC_Edit As Integer = 2
    Public Const INCEXC_Delete As Integer = 3

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moDealerDropGrid_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim oDealerDrop, oCovTypeDrop As DropDownList

        Try
            oDealerDrop = CType(GetSelectedGridControl(moGridView, 0), DropDownList)
            oCovTypeDrop = CType(GetSelectedGridControl(moGridView, 1), DropDownList)
            PopulateCovTypeGrid(oDealerDrop, oCovTypeDrop)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateCovTypeGrid(dealerList As DropDownList, covTypeList As DropDownList)
        Try
            Dim coverageList As DataElements.ListItem()
            Dim ocListContext As New ListContext

            'Dim dv As DataView
            Dim oDealerId As Guid = GetSelectedItem(dealerList)

            If Not oDealerId = Guid.Empty Then
                'dv = ReppolicyClaimCount.GetCoverageTypeListByDealer(oDealerId)
                ocListContext.DealerId = oDealerId
                ocListContext.LanguageId = Authentication.CurrentUser.LanguageId
                coverageList = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByDealer", context:=ocListContext, languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Else
                'dv = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False)
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                coverageList = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", context:=ocListContext, languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            End If

            'BindListControlToDataView(covTypeList, dv)

            covTypeList.Populate(coverageList.ToArray, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim nIndex As Integer
        Dim oDealerDrop, oCovTypeDrop As DropDownList
        Dim guidTemp As Guid

        Try
            If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.IncludeExcludeList.FindIndex(Function(r) r.Id = guidTemp)
                State.IncludeExcludeWorkingItem = State.IncludeExcludeList.Item(nIndex)
            End If

            If e.CommandName = EDIT_COMMAND_NAME Then
                moGridView.EditIndex = nIndex
                moGridView.SelectedIndex = nIndex
                State.IncludeExcludeAction = INCEXC_Edit
                PopulateInclusionExclusionGrid(State.IncludeExcludeList)
                EnableDisableBtnsForIncExcGrid()

                'oDealerDrop = CType(Me.GetSelectedGridControl(moGridView, 0), DropDownList)
                'oCovTypeDrop = CType(Me.GetSelectedGridControl(moGridView, 1), DropDownList)

                'PopulateCovTypeGrid(oDealerDrop, oCovTypeDrop)

            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                State.IncludeExcludeAction = INCEXC_Delete
                IncludeExcludeDeleteRecord()
            ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                IncludeExcludeSaveRecord()
            ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                IncludeExcludeCancelRecord()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridView.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As AcctEventDetailIncExc


            If e.Row.DataItem IsNot Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, AcctEventDetailIncExc)
                    'edit item, populate dropdown and set value
                    If (State.IncludeExcludeAction = INCEXC_Add OrElse State.IncludeExcludeAction = INCEXC_Edit) AndAlso State.IncludeExcludeWorkingItem.Id = dvRow.Id Then
                        Dim objDDL As DropDownList
                        objDDL = CType(e.Row.Cells(GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlDealer"), DropDownList)

                        'Dim dv As DataView
                        'dv = AcctEventDetailIncExc.GetDealerList(State.ParentBO.Id)
                        'BindListControlToDataView(objDDL, dv)

                        'set dealer
                        Dim dealerList As New List(Of DataElements.ListItem)
                        Dim oListContext As New ListContext
                        For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                            oListContext.CompanyId = _company
                            oListContext.AccountingEventId = State.ParentBO.Id
                            Dim oDealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerByAcctEventAndCompany", context:=oListContext)
                            If oDealerListForCompany.Count > 0 Then
                                If dealerList IsNot Nothing Then
                                    dealerList.AddRange(oDealerListForCompany)
                                Else
                                    dealerList = oDealerListForCompany.Clone()
                                End If
                            End If
                        Next

                        objDDL.Populate(dealerList.ToArray, New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

                        If Not dvRow.DealerId = Guid.Empty Then
                            SetSelectedItem(objDDL, dvRow.DealerId.ToString)
                        End If

                        'set coverage type
                        objDDL = CType(e.Row.Cells(GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
                        Dim coverageList As DataElements.ListItem()
                        Dim ocListContext As New ListContext

                        If Not dvRow.DealerId = Guid.Empty Then
                            'dv = ReppolicyClaimCount.GetCoverageTypeListByDealer(dvRow.DealerId)
                            ocListContext.DealerId = dvRow.DealerId
                            ocListContext.LanguageId = Authentication.CurrentUser.LanguageId
                            coverageList = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByDealer", context:=ocListContext, languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        Else
                            'dv = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False)
                            ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                            coverageList = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", context:=ocListContext, languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        End If

                        objDDL.Populate(coverageList.ToArray, New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })

                        'BindListControlToDataView(objDDL, dv)

                        If Not dvRow.CoverageTypeId = Guid.Empty Then
                            SetSelectedItem(objDDL, dvRow.CoverageTypeId.ToString)
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub IncludeExcludeSaveRecord()
        Try

            Dim objDDL As DropDownList

            If State.IncludeExcludeAction = INCEXC_Edit Then 'save the original value
                State.IncludeExcludeWorkingOrig = New AcctEventDetailIncExc
                CopyIncExcObject(State.IncludeExcludeWorkingItem, State.IncludeExcludeWorkingOrig)
            End If

            objDDL = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
            PopulateBOProperty(State.IncludeExcludeWorkingItem, "CoverageTypeId", objDDL)
            objDDL = CType(moGridView.Rows(moGridView.EditIndex).Cells(GRID_COL_DEALER_IDX).FindControl("ddlDealer"), DropDownList)
            PopulateBOProperty(State.IncludeExcludeWorkingItem, "DealerId", objDDL)

            If State.IncludeExcludeWorkingItem.IsDirty Then 'Save the changes
                If State.MyBO.IsNew = False AndAlso State.IncludeExcludeWorkingItem.IsDirty Then 'existing contract, save to DB directly
                    State.IncludeExcludeWorkingItem.SaveWithoutCheckDSCreator()
                    'reload the list
                    State.IncludeExcludeList = Nothing
                    State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(State.MyBO.Id)
                Else 'new BO, keep the record in memory after validation and save it with new BO
                    If NewAcctEventDetailIncExcValid(State.IncludeExcludeWorkingItem) Then
                        Dim objInd As Integer = State.IncludeExcludeList.FindIndex(Function(r) r.Id = State.IncludeExcludeWorkingItem.Id)
                        State.IncludeExcludeList.Item(objInd) = State.IncludeExcludeWorkingItem
                    Else 'Validation error, exit and show errors
                        Exit Sub
                    End If
                End If
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If


            State.IncludeExcludeAction = INCEXC_None
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            State.IncludeExcludeWorkingItem = Nothing
            PopulateInclusionExclusionGrid(State.IncludeExcludeList)
            EnableDisableBtnsForIncExcGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Function NewAcctEventDetailIncExcValid(obj As AcctEventDetailIncExc) As Boolean
        Dim blnValid As Boolean = True

        If Not obj.AtLeastOneFieldHasValue Then
            blnValid = False
            MasterPage.MessageController.AddError("AT_LEAST_ONE_FIELD_REQUIRED")
        End If

        If obj.DuplicateExists(State.IncludeExcludeList) Then
            blnValid = False
            MasterPage.MessageController.AddError("DUPLICATE_RECORD")
        End If

        Return blnValid

    End Function

    Private Sub IncludeExcludeCancelRecord()
        Try
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            If State.IncludeExcludeAction = INCEXC_Add Then
                State.IncludeExcludeList.Remove(State.IncludeExcludeWorkingItem)
            ElseIf State.IncludeExcludeAction = INCEXC_Edit AndAlso (State.IncludeExcludeWorkingItem IsNot Nothing) Then ' set the object to original status
                CopyIncExcObject(State.IncludeExcludeWorkingOrig, State.IncludeExcludeWorkingItem)
            End If

            State.IncludeExcludeAction = INCEXC_None
            State.IncludeExcludeWorkingItem = Nothing

            PopulateInclusionExclusionGrid(State.IncludeExcludeList)
            EnableDisableBtnsForIncExcGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CopyIncExcObject(objSource As AcctEventDetailIncExc, objDest As AcctEventDetailIncExc)
        If objSource IsNot Nothing Then
            With objSource
                objDest.AcctEventDetailId = .AcctEventDetailId
                objDest.CoverageTypeId = .CoverageTypeId
                objDest.DealerId = .DealerId
            End With
        End If
    End Sub

    Private Sub PopulateInclusionExclusionGrid(ds As Collections.Generic.List(Of AcctEventDetailIncExc))
        Dim blnEmptyList As Boolean = False, mySource As Collections.Generic.List(Of AcctEventDetailIncExc)
        If ds Is Nothing OrElse ds.Count = 0 Then
            mySource = New Collections.Generic.List(Of AcctEventDetailIncExc)
            mySource.Add(New AcctEventDetailIncExc())
            blnEmptyList = True
            moGridView.DataSource = mySource
        Else
            moGridView.DataSource = ds
        End If

        moGridView.DataBind()

        If blnEmptyList Then
            moGridView.Rows(0).Visible = False
        End If

        'Me.TranslateGridHeader(moGridView)
    End Sub

    Private Sub EnableDisableBtnsForIncExcGrid()
        If State.IncludeExcludeAction = INCEXC_None Then 'enable buttons on main form
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, BtnNewIncExc_WRITE, True)
            EnableDisableFields()
        Else 'disable buttons on main form for RepPolicy grid editing
            ControlMgr.SetEnableControl(Me, btnBack, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, BtnNewIncExc_WRITE, False)
        End If
    End Sub

    Private Sub IncludeExcludeDeleteRecord()
        If State.MyBO.IncludeExcludeInd = "I" AndAlso State.IncludeExcludeList.Count = 1 Then
            'at least one needed if inclusion config, delete of the last record is not allowed
            MasterPage.MessageController.AddError("AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
            Exit Sub
        End If

        If Not State.IncludeExcludeWorkingItem.IsNew Then
            'if not new object, delete from database
            State.IncludeExcludeWorkingItem.Delete()
            State.IncludeExcludeWorkingItem.SaveWithoutCheckDSCreator()
        End If
        'remove from list
        State.IncludeExcludeList.Remove(State.IncludeExcludeWorkingItem)

        State.IncludeExcludeAction = INCEXC_None
        moGridView.SelectedIndex = -1
        moGridView.EditIndex = moGridView.SelectedIndex

        State.IncludeExcludeWorkingItem = Nothing
        PopulateInclusionExclusionGrid(State.IncludeExcludeList)
        EnableDisableBtnsForIncExcGrid()
        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
    End Sub

    Private Sub ClearIncludeExcludeState()
        With State
            .IncludeExcludeAction = INCEXC_None
            .IncludeExcludeList = Nothing
            .IncludeExcludeWorkingItem = Nothing
            .IncludeExcludeWorkingOrig = Nothing
            PopulateInclusionExclusionGrid(.IncludeExcludeList)
        End With

    End Sub

    Private Sub DeleteInclusionExclusionRecords()
        State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(State.MyBO.Id)
        If State.IncludeExcludeList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.IncludeExcludeList.Count - 1
                State.IncludeExcludeList.Item(i).Delete()
                State.IncludeExcludeList.Item(i).SaveWithoutCheckDSCreator()
            Next
        End If
        State.IncludeExcludeList = Nothing
    End Sub

    Private Sub SaveInclusionExclusionRecords(blnNewBO As Boolean)
        If blnNewBO Then
            ' new BO, save the replacement policy records in memory
            If (State.IncludeExcludeList IsNot Nothing) AndAlso State.IncludeExcludeList.Count > 0 Then
                Dim i As Integer
                For i = 0 To State.IncludeExcludeList.Count - 1
                    State.IncludeExcludeList.Item(i).SaveWithoutCheckDSCreator()
                Next
                State.IncludeExcludeList = Nothing
            End If
        End If
    End Sub

    Private Sub BtnNewIncExc_WRITE_Click(sender As Object, e As System.EventArgs) Handles BtnNewIncExc_WRITE.Click
        Try
            State.IncludeExcludeAction = INCEXC_Add
            Dim objNew As New AcctEventDetailIncExc()
            objNew.AcctEventDetailId = State.MyBO.Id
            State.IncludeExcludeWorkingItem = objNew

            If State.IncludeExcludeList Is Nothing Then
                State.IncludeExcludeList = New Collections.Generic.List(Of AcctEventDetailIncExc)
            End If
            State.IncludeExcludeList.Add(objNew)

            moGridView.SelectedIndex = State.IncludeExcludeList.Count - 1
            moGridView.EditIndex = moGridView.SelectedIndex
            PopulateInclusionExclusionGrid(State.IncludeExcludeList)

            EnableDisableBtnsForIncExcGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AccountingEventDetailForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
    End Sub
#End Region



End Class
