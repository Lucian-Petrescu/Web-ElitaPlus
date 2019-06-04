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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim CalPar As ReturnType = CType(CallingPar, ReturnType)
                Me.State.ParentBO = CalPar.EditingBo
                If Not CalPar.SelectedGuid.Equals(Guid.Empty) Then
                    Me.State.MyBO = New AcctEventDetail(CalPar.SelectedGuid)
                    Exit Sub
                End If
            End If
            Me.State.IsNew = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public EditingBo As AcctEvent
        Public SelectedGuid As Guid = Guid.Empty

        Public Sub New(ByVal EventDetailId As Guid, ByVal curEditingBo As AcctEvent)
            Me.EditingBo = curEditingBo
            SelectedGuid = EventDetailId
        End Sub

    End Class
#End Region

#Region "Page Events"
    Private AcctEventType As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()

        Me.SetFormTitle(PAGETITLE)
        Me.SetFormTab(PAGETAB)

        Try
            If Not Me.IsPostBack Then
                Me.TranslateGridHeader(moGridView)

                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                UpdateBreadCrum()

                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                PopulateAll()

                If Me.State.IsNew = True Then
                    CreateNew()
                End If

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            GetDisabledTabs()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
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
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'enable the inclusion/exclusion configuration tab only for PREM, REFUNDS, UPR, IBNR, claim and claim reserve event types
        If AcctEventType = String.Empty Then
            Dim AcctEventTypedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            AcctEventType = LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId)
        End If

        If AcctEventType = "PREM" OrElse AcctEventType = "UPR" OrElse AcctEventType = "IBNR" OrElse AcctEventType = "REFUNDS" OrElse AcctEventType = "CLAIM" OrElse AcctEventType = "CLAIMRES" Then

        Else
            DisabledTabsList.Add(Tabs_InclusionExclisionConfig)
        End If
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateAll()

        Me.State.YESNOList = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        'Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'Me.State.YESNOdv = YESNOdv

        'Dim AcctTSrcdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_ANALYSIS_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
        'Me.State.AcctTSrcdv = AcctTSrcdv
        Dim AcctTSrList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTCODE", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        'Dim AcctEventTypedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'Me.BindListControlToDataView(Me.moAccountingEventDROPDOWN, AcctEventTypedv)
        'AcctEventType = LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId)

        Dim AcctEventTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTTRANSTYP", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.moAccountingEventDROPDOWN.Populate(AcctEventTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        AcctEventType = (From lst In AcctEventTypeList
                         Where lst.ListItemId = Me.State.ParentBO.AcctEventTypeId
                         Select lst.Code).FirstOrDefault()

        'Dim AcctFieldTypedv As DataView = LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_ACCT_FIELD_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        'AcctFieldTypedv.RowFilter = String.Format("{0}code like '{1}_%'", If(AcctFieldTypedv.RowFilter.Length > 0, String.Format("{0} AND ", AcctFieldTypedv.RowFilter), "").ToString, LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId))
        'Me.BindListControlToDataView(Me.moFieldTypeDROPDOWN, AcctFieldTypedv)

        Dim AcctFieldTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTFIELDTYP", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Dim AcctEventTypeFilter = (From lst In AcctEventTypeList
                                   Where lst.ListItemId = Me.State.ParentBO.AcctEventTypeId
                                   Select lst.Code).FirstOrDefault()

        Dim FilteredAcctFieldTypeList As DataElements.ListItem() = (From lst In AcctFieldTypeList
                                                                    Where lst.Code.StartsWith(AcctEventTypeFilter)
                                                                    Select lst).ToArray()

        Me.moFieldTypeDROPDOWN.Populate(FilteredAcctFieldTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'REQ-5733 Start / Displaying Product_code Drop down Option only for Claimns,Claims Reserve,Premium and Premium Refund 
        'Dim h As String = LookupListNew.GetCodeFromId(AcctEventTypedv, Me.State.ParentBO.AcctEventTypeId)
        Dim h As String = (From lst In AcctEventTypeList
                           Where lst.ListItemId = Me.State.ParentBO.AcctEventTypeId
                           Select lst.Code).FirstOrDefault()

        If (h = "UPR" Or h = "IBNR" Or h = "INV" Or h = "VEND") Then
            AcctTSrList = (From lst In AcctTSrList
                           Where lst.Code <> "PRDCODE"
                           Select lst).ToArray()
            'AcctTSrcdv.RowFilter = String.Format("{0}code <> 'PRDCODE'", If(AcctTSrcdv.RowFilter.Length > 0, String.Format("{0} AND ", AcctTSrcdv.RowFilter), "").ToString)
        End If
        'REQ-5733 END

        'Me.BindListControlToDataView(Me.moUsePayeeSettingsDROPDOWN, YESNOdv)
        Me.moUsePayeeSettingsDROPDOWN.Populate(Me.State.YESNOList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        'Me.BindListControlToDataView(Me.moDebitCreditDROPDOWN, BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim CreditDebitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CRDBT", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.moDebitCreditDROPDOWN.Populate(CreditDebitList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moAccountingCompanyDROPDOWN, LookupListNew.DataView(LookupListNew.LK_ACCTCOMPANY))
        Dim AcctCompanyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AcctCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.moAccountingCompanyDROPDOWN.Populate(AcctCompanyList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moBusinessUnitDDL, AcctBusinessUnit.getList(Me.State.ParentBO.AcctCompanyId, Nothing), AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID)
        Dim olistcontext As ListContext = New ListContext()
        olistcontext.AccountingCompanyId = Me.State.ParentBO.AcctCompanyId
        Dim BusinessUnitList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessUnitByAcctCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=olistcontext)
        Me.moBusinessUnitDDL.Populate(BusinessUnitList, New PopulateOptions() With
            {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
            })

        'Me.BindListControlToDataView(Me.cboAccountType, LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim AcctTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CRDBT_NUM", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.cboAccountType.Populate(AcctTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moBusinessEntityDROPDOWN, BusinessObjectsNew.LookupListNew.getBusinessCoverageEntity(Me.State.ParentBO.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim listcontext As ListContext = New ListContext()
        listcontext.AccountingCompanyId = Me.State.ParentBO.AcctCompanyId
        listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim BusinessEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BusinessEntityByAccountingCompany", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=listcontext)
        Me.moBusinessEntityDROPDOWN.Populate(BusinessEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moAcctAnal1DDL, AcctTSrcdv)
        Me.moAcctAnal1DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal2DDL, AcctTSrcdv)
        Me.moAcctAnal2DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal3DDL, AcctTSrcdv)
        Me.moAcctAnal3DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal4DDL, AcctTSrcdv)
        Me.moAcctAnal4DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal5DDL, AcctTSrcdv)
        Me.moAcctAnal5DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal6DDL, AcctTSrcdv)
        Me.moAcctAnal6DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal7DDL, AcctTSrcdv)
        Me.moAcctAnal7DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal8DDL, AcctTSrcdv)
        Me.moAcctAnal8DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal9DDL, AcctTSrcdv)
        Me.moAcctAnal9DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'Me.BindListControlToDataView(Me.moAcctAnal10DDL, AcctTSrcdv)
        Me.moAcctAnal10DDL.Populate(AcctTSrList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.moDescriptionDDL, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_DESC_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim AcctDescSourceList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTDESC", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.moDescriptionDDL.Populate(AcctDescSourceList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        ddlIncludeExcludeType.Items.Clear()
        ddlIncludeExcludeType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("Exclusion"), "E"))
        ddlIncludeExcludeType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("Inclusion"), "I"))

    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing
        Me.State.MyBO = New AcctEventDetail
        Me.State.MyBO.AcctEventId = Me.State.ParentBO.Id
        'Clear the inclusion/exclusion list
        ClearIncludeExcludeState()
    End Sub

    Protected Sub PopulateFormFromBOs()

        'Dim YesNodv As DataView = Me.State.YESNOdv

        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.moAccountCodeTEXT, .AccountCode)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal1TEXT, .AnalysisCode1)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal2TEXT, .AnalysisCode2)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal3TEXT, .AnalysisCode3)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal4TEXT, .AnalysisCode4)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal5TEXT, .AnalysisCode5)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal6TEXT, .AnalysisCode6)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal7TEXT, .AnalysisCode7)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal8TEXT, .AnalysisCode8)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal9TEXT, .AnalysisCode9)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal10TEXT, .AnalysisCode10)
            Me.PopulateControlFromBOProperty(Me.moCalculationTEXT, .Calculation)
            Me.PopulateControlFromBOProperty(Me.moJournalIdTEXT, .JournalIDSuffix)
            Me.PopulateControlFromBOProperty(Me.moDescriptionTEXT, .Description)

            'Fill drop downs
            Me.PopulateControlFromBOProperty(Me.moFieldTypeDROPDOWN, .FieldTypeId)
            Me.PopulateControlFromBOProperty(Me.moDebitCreditDROPDOWN, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId), .DebitCredit))
            'Me.PopulateControlFromBOProperty(Me.moUsePayeeSettingsDROPDOWN, ElitaPlus.BusinessObjectsNew.LookupListNew.GetIdFromCode(YesNodv, .UsePayeeSettings))
            Me.PopulateControlFromBOProperty(Me.moUsePayeeSettingsDROPDOWN, (From ynList In Me.State.YESNOList
                                                                             Where ynList.Code = .UsePayeeSettings
                                                                             Select ynList.ListItemId).FirstOrDefault())
            Me.PopulateControlFromBOProperty(Me.cboAccountType, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .AccountType))
            Me.PopulateControlFromBOProperty(Me.moBusinessUnitDDL, .AcctBusinessUnitId)
            Me.PopulateControlFromBOProperty(Me.moBusinessEntityDROPDOWN, .BusinessEntityId)

            Me.PopulateControlFromBOProperty(Me.moDescriptionDDL, .DescriptionSourceId)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal1DDL, .AnalysisSource1Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal2DDL, .AnalysisSource2Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal3DDL, .AnalysisSource3Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal4DDL, .AnalysisSource4Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal5DDL, .AnalysisSource5Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal6DDL, .AnalysisSource6Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal7DDL, .AnalysisSource7Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal8DDL, .AnalysisSource8Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal9DDL, .AnalysisSource9Id)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal10DDL, .AnalysisSource10Id)


            'Fill readonly DDLs
            Me.PopulateControlFromBOProperty(Me.moAccountingCompanyDROPDOWN, Me.State.ParentBO.AcctCompanyId)
            Me.PopulateControlFromBOProperty(Me.moAccountingEventDROPDOWN, Me.State.ParentBO.AcctEventTypeId)

            Me.State.AcctCompanyBo = New AcctCompany(Me.State.ParentBO.AcctCompanyId)

            If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Count > 0 Then
                Me.ACCT_SYSTEM = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.AcctCompanyBo.AcctSystemId)
            End If

            If Me.ACCT_SYSTEM = BusinessObjectsNew.FelitaEngine.FELITA_PREFIX Then
                Me.EnableDisableControls(Me.moJournalTypeTEXT, True)
                Me.PopulateControlFromBOProperty(Me.moJournalTypeTEXT, Me.State.ParentBO.JournalType)

                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moJournalIdLABEL, False)
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moJournalIdTEXT, False)
            Else
                Me.EnableDisableControls(Me.moJournalTypeTEXT, False)
                Me.PopulateControlFromBOProperty(Me.moJournalTypeTEXT, .JournalType)

                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moJournalIdLABEL, True)
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moJournalIdTEXT, True)

            End If

            'If LookupListNew.GetCodeFromId(Me.State.YESNOdv, Me.State.AcctCompanyBo.UseCoverageEntityId) = YES_VALUE.Trim(CChar("'")) Then
            If (From ynList In Me.State.YESNOList
                Where ynList.ListItemId = Me.State.AcctCompanyBo.UseCoverageEntityId
                Select ynList.Code).FirstOrDefault() = "Y" Then
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moBusinessEntityLABEL, True)
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moBusinessEntityDROPDOWN, True)
            Else
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moBusinessEntityLABEL, False)
                ControlMgr.SetVisibleControl(CType(Me.Page, ElitaPlusPage), Me.moBusinessEntityDROPDOWN, False)
            End If

            SetSelectedItem(Me.ddlIncludeExcludeType, .IncludeExcludeInd)
        End With

        If Me.State.IncludeExcludeList Is Nothing Then
            Me.State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(Me.State.MyBO.Id)
        End If
        PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)

    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.State

            Me.PopulateBOProperty(.MyBO, ACCOUNTCODE_PROPERTY, Me.moAccountCodeTEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE1_PROPERTY, Me.moAcctAnal1TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE2_PROPERTY, Me.moAcctAnal2TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE3_PROPERTY, Me.moAcctAnal3TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE4_PROPERTY, Me.moAcctAnal4TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE5_PROPERTY, Me.moAcctAnal5TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE6_PROPERTY, Me.moAcctAnal6TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE7_PROPERTY, Me.moAcctAnal7TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE8_PROPERTY, Me.moAcctAnal8TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE9_PROPERTY, Me.moAcctAnal9TEXT)
            Me.PopulateBOProperty(.MyBO, ANALYSISCODE10_PROPERTY, Me.moAcctAnal10TEXT)
            Me.PopulateBOProperty(.MyBO, CALCULATION_PROPERTY, Me.moCalculationTEXT)
            Me.PopulateBOProperty(.MyBO, JOURNALTYPE_PROPERTY, Me.moJournalTypeTEXT)
            Me.PopulateBOProperty(.MyBO, JOURNALID_PROPERTY, Me.moJournalIdTEXT)

            Me.PopulateBOProperty(.MyBO, ACCTBUSINESSUNIT_PROPERTY, Me.moBusinessUnitDDL)
            Me.PopulateBOProperty(.MyBO, Me.ACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CRDBT_NUM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), New Guid(Me.cboAccountType.SelectedItem.Value)))
            Me.PopulateBOProperty(.MyBO, DEBITCREDIT_PROPERTY, BusinessObjectsNew.LookupListNew.GetCodeFromId(BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_CREDIT_DEBIT, ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(Me.moDebitCreditDROPDOWN.SelectedValue)))
            Me.PopulateBOProperty(.MyBO, FIELDTYPEID_PROPERTY, Me.moFieldTypeDROPDOWN)

            Me.PopulateBOProperty(.MyBO, DESCRIPTION_PROPERTY, Me.moDescriptionTEXT)
            Me.PopulateBOProperty(.MyBO, BUSINESSENTITY_PROPERTY, Me.moBusinessEntityDROPDOWN)

            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE1ID_PROPERTY, Me.moAcctAnal1DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE2ID_PROPERTY, Me.moAcctAnal2DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE3ID_PROPERTY, Me.moAcctAnal3DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE4ID_PROPERTY, Me.moAcctAnal4DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE5ID_PROPERTY, Me.moAcctAnal5DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE6ID_PROPERTY, Me.moAcctAnal6DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE7ID_PROPERTY, Me.moAcctAnal7DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE8ID_PROPERTY, Me.moAcctAnal8DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE9ID_PROPERTY, Me.moAcctAnal9DDL)
            Me.PopulateBOProperty(.MyBO, Me.ANALYSISSOURCE10ID_PROPERTY, Me.moAcctAnal10DDL)
            Me.PopulateBOProperty(.MyBO, Me.DESCRIPTIONSOURCEID_PROPERTY, Me.moDescriptionDDL)

            Dim vendSettings As String
            If (New Guid(Me.moUsePayeeSettingsDROPDOWN.SelectedValue)).Equals(Guid.Empty) Then
                Dim c() As Char = New Char() {CChar("'")}
                vendSettings = NO_VALUE.Trim(c)
            Else
                vendSettings = (From ynList In Me.State.YESNOList
                                Where ynList.ListItemId = New Guid(Me.moUsePayeeSettingsDROPDOWN.SelectedValue)
                                Select ynList.Code).FirstOrDefault()
                'vendSettings = BusinessObjectsNew.LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.moUsePayeeSettingsDROPDOWN.SelectedValue))
            End If
            Me.PopulateBOProperty(.MyBO, USEPAYEESETTINGS_PROPERTY, vendSettings)
            Me.PopulateBOProperty(.MyBO, "IncludeExcludeInd", ddlIncludeExcludeType.SelectedValue)

        End With

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        With Me.State
            Me.BindBOPropertyToLabel(.MyBO, ACCOUNTCODE_PROPERTY, Me.moAccountCodeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ACCOUNTTYPE_PROPERTY, Me.moAccountTypeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE1ID_PROPERTY, Me.moAcctAnal1LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE2ID_PROPERTY, Me.moAcctAnal2LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE3ID_PROPERTY, Me.moAcctAnal3LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE4ID_PROPERTY, Me.moAcctAnal4LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE5ID_PROPERTY, Me.moAcctAnal5LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE6ID_PROPERTY, Me.moAcctAnal6LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE7ID_PROPERTY, Me.moAcctAnal7LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE8ID_PROPERTY, Me.moAcctAnal8LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE9ID_PROPERTY, Me.moAcctAnal9LABEL)
            Me.BindBOPropertyToLabel(.MyBO, ANALYSISSOURCE10ID_PROPERTY, Me.moAcctAnal10LABEL)
            Me.BindBOPropertyToLabel(.MyBO, CALCULATION_PROPERTY, Me.moCalculationLABEL)
            Me.BindBOPropertyToLabel(.MyBO, DEBITCREDIT_PROPERTY, Me.moDebitCreditLABEL)
            Me.BindBOPropertyToLabel(.MyBO, FIELDTYPEID_PROPERTY, Me.moFieldTypeIdLABEL)
            Me.BindBOPropertyToLabel(.MyBO, JOURNALTYPE_PROPERTY, Me.moJournalTypeLABEL)
            Me.BindBOPropertyToLabel(.MyBO, USEPAYEESETTINGS_PROPERTY, Me.moUsePayeeSettingsLABEL)
            Me.BindBOPropertyToLabel(.MyBO, ACCTBUSINESSUNIT_PROPERTY, Me.moBusinessUnitLABEL)
            Me.BindBOPropertyToLabel(.MyBO, JOURNALID_PROPERTY, Me.moJournalIdLABEL)
            Me.BindBOPropertyToLabel(.MyBO, DESCRIPTION_PROPERTY, Me.moDescriptionLABEL)
            Me.BindBOPropertyToLabel(.MyBO, BUSINESSENTITY_PROPERTY, Me.moBusinessEntityLABEL)

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
        End If

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Try
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Dim blnNewBO As Boolean = State.MyBO.IsNew
                    If blnNewBO AndAlso State.MyBO.IncludeExcludeInd = "I" AndAlso (State.IncludeExcludeList Is Nothing OrElse State.IncludeExcludeList.Count = 0) Then
                        'new BO, validate at least one configuration exists first
                        MasterPage.MessageController.AddError("AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
                        Try
                            Me.State.MyBO.Validate() 'show other validation error also
                        Catch ex As Exception
                            Me.HandleErrors(ex, Me.MasterPage.MessageController)
                        End Try
                        Exit Sub
                    End If
                    Me.State.MyBO.Save()
                    SaveInclusionExclusionRecords(blnNewBO) 'new line items, save Child records added in memory
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New AccountingEventForm.ReturnType(Me.State.ActionInProgress, Me.State.ParentBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New AccountingEventForm.ReturnType(Me.State.ActionInProgress, Me.State.ParentBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New AccountingEventForm.ReturnType(Me.State.ActionInProgress, Me.State.ParentBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
        Catch ex As Exception
            Throw ex
        Finally
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Try
    End Sub

    Protected Sub CreateNewWithCopy()

        Dim newBO As New AcctEventDetail

        newBO.AcctEventId = Me.State.MyBO.AcctEventId
        Me.State.MyBO = newBO

        'Reset the source field
        Me.moFieldTypeDROPDOWN.SelectedIndex = 0
        Me.moBusinessUnitDDL.SelectedIndex = 0

        'Clear the inclusion/exclusion list
        ClearIncludeExcludeState()

        Me.PopulateBOsFromForm()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New AcctEventDetail
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
    End Sub

    <System.Web.Services.WebMethod()> _
    <Script.Services.ScriptMethod()> _
    Public Shared Function IncludeVendors(ByVal BusinessUnit As String) As String

        Dim c() As Char = New Char() {CChar("'")}

        Try
            Dim g As New Guid(BusinessUnit)

            If Not g.Equals(Guid.Empty) Then
                Dim bu As New AcctBusinessUnit(g)
                If Not bu Is Nothing Then
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

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New AccountingEventForm.ReturnType(Me.State.ActionInProgress, Me.State.ParentBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New AcctEventDetail(Me.State.MyBO.Id)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Dim blnNewBO As Boolean = State.MyBO.IsNew
                If blnNewBO AndAlso State.MyBO.IncludeExcludeInd = "I" AndAlso (State.IncludeExcludeList Is Nothing OrElse State.IncludeExcludeList.Count = 0) Then
                    'new BO, validate at least one configuration exists first
                    MasterPage.MessageController.AddError("AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
                    Try
                        Me.State.MyBO.Validate() 'show other validation error also
                    Catch ex As Exception
                        Me.HandleErrors(ex, Me.MasterPage.MessageController)
                    End Try
                    Exit Sub
                End If
                Me.State.MyBO.Save()
                SaveInclusionExclusionRecords(blnNewBO) 'new line items, save Child records added in memory

                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DeleteInclusionExclusionRecords() 'delete Child records first
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New AccountingEventForm.ReturnType(Me.State.ActionInProgress, Me.State.ParentBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moDealerDropGrid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oDealerDrop, oCovTypeDrop As DropDownList

        Try
            oDealerDrop = CType(Me.GetSelectedGridControl(moGridView, 0), DropDownList)
            oCovTypeDrop = CType(Me.GetSelectedGridControl(moGridView, 1), DropDownList)
            PopulateCovTypeGrid(oDealerDrop, oCovTypeDrop)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateCovTypeGrid(ByVal dealerList As DropDownList, ByVal covTypeList As DropDownList)
        Try
            Dim coverageList As DataElements.ListItem()
            Dim ocListContext As New ListContext

            'Dim dv As DataView
            Dim oDealerId As Guid = Me.GetSelectedItem(dealerList)

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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim nIndex As Integer
        Dim oDealerDrop, oCovTypeDrop As DropDownList
        Dim guidTemp As Guid

        Try
            If e.CommandName = Me.EDIT_COMMAND_NAME OrElse e.CommandName = Me.DELETE_COMMAND_NAME Then
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.IncludeExcludeList.FindIndex(Function(r) r.Id = guidTemp)
                State.IncludeExcludeWorkingItem = State.IncludeExcludeList.Item(nIndex)
            End If

            If e.CommandName = Me.EDIT_COMMAND_NAME Then
                moGridView.EditIndex = nIndex
                moGridView.SelectedIndex = nIndex
                State.IncludeExcludeAction = INCEXC_Edit
                PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)
                EnableDisableBtnsForIncExcGrid()

                'oDealerDrop = CType(Me.GetSelectedGridControl(moGridView, 0), DropDownList)
                'oCovTypeDrop = CType(Me.GetSelectedGridControl(moGridView, 1), DropDownList)

                'PopulateCovTypeGrid(oDealerDrop, oCovTypeDrop)

            ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                State.IncludeExcludeAction = INCEXC_Delete
                IncludeExcludeDeleteRecord()
            ElseIf (e.CommandName = Me.SAVE_COMMAND_NAME) Then
                IncludeExcludeSaveRecord()
            ElseIf (e.CommandName = Me.CANCEL_COMMAND_NAME) Then
                IncludeExcludeCancelRecord()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moGridView.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As AcctEventDetailIncExc


            If Not e.Row.DataItem Is Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, AcctEventDetailIncExc)
                    'edit item, populate dropdown and set value
                    If (State.IncludeExcludeAction = INCEXC_Add OrElse State.IncludeExcludeAction = INCEXC_Edit) AndAlso State.IncludeExcludeWorkingItem.Id = dvRow.Id Then
                        Dim objDDL As DropDownList
                        objDDL = CType(e.Row.Cells(Me.GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlDealer"), DropDownList)

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
                                If Not dealerList Is Nothing Then
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
                            Me.SetSelectedItem(objDDL, dvRow.DealerId.ToString)
                        End If

                        'set coverage type
                        objDDL = CType(e.Row.Cells(Me.GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
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
                            Me.SetSelectedItem(objDDL, dvRow.CoverageTypeId.ToString)
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub IncludeExcludeSaveRecord()
        Try

            Dim objDDL As DropDownList

            If State.IncludeExcludeAction = INCEXC_Edit Then 'save the original value
                State.IncludeExcludeWorkingOrig = New AcctEventDetailIncExc
                CopyIncExcObject(State.IncludeExcludeWorkingItem, State.IncludeExcludeWorkingOrig)
            End If

            objDDL = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_COVERATGE_TYPE_IDX).FindControl("ddlCoverageTYPE"), DropDownList)
            Me.PopulateBOProperty(State.IncludeExcludeWorkingItem, "CoverageTypeId", objDDL)
            objDDL = CType(Me.moGridView.Rows(Me.moGridView.EditIndex).Cells(Me.GRID_COL_DEALER_IDX).FindControl("ddlDealer"), DropDownList)
            Me.PopulateBOProperty(State.IncludeExcludeWorkingItem, "DealerId", objDDL)

            If State.IncludeExcludeWorkingItem.IsDirty Then 'Save the changes
                If State.MyBO.IsNew = False AndAlso State.IncludeExcludeWorkingItem.IsDirty Then 'existing contract, save to DB directly
                    State.IncludeExcludeWorkingItem.SaveWithoutCheckDSCreator()
                    'reload the list
                    Me.State.IncludeExcludeList = Nothing
                    Me.State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(Me.State.MyBO.Id)
                Else 'new BO, keep the record in memory after validation and save it with new BO
                    If NewAcctEventDetailIncExcValid(State.IncludeExcludeWorkingItem) Then
                        Dim objInd As Integer = State.IncludeExcludeList.FindIndex(Function(r) r.Id = State.IncludeExcludeWorkingItem.Id)
                        State.IncludeExcludeList.Item(objInd) = State.IncludeExcludeWorkingItem
                    Else 'Validation error, exit and show errors
                        Exit Sub
                    End If
                End If
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If


            State.IncludeExcludeAction = INCEXC_None
            moGridView.SelectedIndex = -1
            moGridView.EditIndex = moGridView.SelectedIndex

            State.IncludeExcludeWorkingItem = Nothing
            PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)
            EnableDisableBtnsForIncExcGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Function NewAcctEventDetailIncExcValid(ByVal obj As AcctEventDetailIncExc) As Boolean
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
            ElseIf State.IncludeExcludeAction = INCEXC_Edit AndAlso (Not State.IncludeExcludeWorkingItem Is Nothing) Then ' set the object to original status
                CopyIncExcObject(State.IncludeExcludeWorkingOrig, State.IncludeExcludeWorkingItem)
            End If

            State.IncludeExcludeAction = INCEXC_None
            State.IncludeExcludeWorkingItem = Nothing

            PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)
            EnableDisableBtnsForIncExcGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CopyIncExcObject(ByVal objSource As AcctEventDetailIncExc, ByVal objDest As AcctEventDetailIncExc)
        If Not objSource Is Nothing Then
            With objSource
                objDest.AcctEventDetailId = .AcctEventDetailId
                objDest.CoverageTypeId = .CoverageTypeId
                objDest.DealerId = .DealerId
            End With
        End If
    End Sub

    Private Sub PopulateInclusionExclusionGrid(ByVal ds As Collections.Generic.List(Of AcctEventDetailIncExc))
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
        If State.MyBO.IncludeExcludeInd = "I" And State.IncludeExcludeList.Count = 1 Then
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
        PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)
        EnableDisableBtnsForIncExcGrid()
        Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
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
        Me.State.IncludeExcludeList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(Me.State.MyBO.Id)
        If State.IncludeExcludeList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.IncludeExcludeList.Count - 1
                State.IncludeExcludeList.Item(i).Delete()
                State.IncludeExcludeList.Item(i).SaveWithoutCheckDSCreator()
            Next
        End If
        Me.State.IncludeExcludeList = Nothing
    End Sub

    Private Sub SaveInclusionExclusionRecords(ByVal blnNewBO As Boolean)
        If blnNewBO Then
            ' new BO, save the replacement policy records in memory
            If (Not State.IncludeExcludeList Is Nothing) AndAlso State.IncludeExcludeList.Count > 0 Then
                Dim i As Integer
                For i = 0 To State.IncludeExcludeList.Count - 1
                    State.IncludeExcludeList.Item(i).SaveWithoutCheckDSCreator()
                Next
                Me.State.IncludeExcludeList = Nothing
            End If
        End If
    End Sub

    Private Sub BtnNewIncExc_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNewIncExc_WRITE.Click
        Try
            State.IncludeExcludeAction = INCEXC_Add
            Dim objNew As New AcctEventDetailIncExc()
            objNew.AcctEventDetailId = State.MyBO.Id
            State.IncludeExcludeWorkingItem = objNew

            If State.IncludeExcludeList Is Nothing Then
                State.IncludeExcludeList = New Collections.Generic.List(Of AcctEventDetailIncExc)
            End If
            Me.State.IncludeExcludeList.Add(objNew)

            moGridView.SelectedIndex = State.IncludeExcludeList.Count - 1
            moGridView.EditIndex = moGridView.SelectedIndex
            PopulateInclusionExclusionGrid(Me.State.IncludeExcludeList)

            EnableDisableBtnsForIncExcGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AccountingEventDetailForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
    End Sub
#End Region



End Class
