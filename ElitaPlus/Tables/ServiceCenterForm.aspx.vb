'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (10/7/2004)  ********************

Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class ServiceCenterForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents moAddressController As UserControlAddress_New
    Protected WithEvents UserControlAvailableSelectedManufacturers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New
    Protected WithEvents UserControlAvailableSelectedDistricts As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New
    Protected WithEvents UserControlAvailableSelectedDealers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New
    Protected WithEvents UsercontrolavailableselectedServiceNetworks As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New
    'Protected WithEvents UserControlAvailableSelectedMethodOfRepair As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New

    Protected WithEvents DataGridDetailMfg As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataGridDetailDst As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataGridDetailDlr As System.Web.UI.WebControls.DataGrid

    Protected WithEvents LabelLastReconDate As System.Web.UI.WebControls.Label
    Protected WithEvents TextboxLastReconDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImageButtonLastReconDate As System.Web.UI.WebControls.ImageButton
    Protected WithEvents LabelMinReplCost As System.Web.UI.WebControls.Label
    Protected WithEvents cboTypeOfMarketing As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LabelEmpty1 As System.Web.UI.WebControls.Label
    Protected WithEvents LabelEmpty2 As System.Web.UI.WebControls.Label

    Protected WithEvents LabelEmpty3 As System.Web.UI.WebControls.Label
    Protected WithEvents moBankInfoController As UserControlBankInfo_New
    Protected WithEvents moUserControlAddress As UserControlAddress_New


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "ServiceCenterForm.aspx"
    Public Const GRID_COL_CODE As Integer = 0
    Public Const GRID_COL_NAME As Integer = 1
    Public Const NOTHING_SELECTED As Integer = 0
    Public Const BankInfoStartIndex As Int16 = 28
    Public Const AddressInfoStartIndex As Int16 = 32
    Private Const NOTHING_SELECTED_GUID As String = "00000000-0000-0000-0000-000000000000"
    Public Const CMD_OTHER As Integer = 0
    Public Const CMD_CHANGE_COUNTRY As Integer = 1
    Public Const CMD_SAVE As Integer = 2
    Public Const TABLES As String = "Tables"
    Public Const SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Const ADDRESS_TYPE As String = "ATYPE"
    Public Const PICK_UP As String = "Pick-Up"
    Public Const SHIPPING As String = "Shipping"
    Public Const UPDATE As String = "UPDATE"

    Public Const GRID_CONTACTS_COL_ID As Integer = 0
    Public Const GRID_CONTACTS_COL_NAME As Integer = 1
    Public Const GRID_CONTACTS_COL_JOB_TITLE As Integer = 2
    Public Const GRID_CONTACTS_COL_COMPANY_NAME As Integer = 3
    Public Const GRID_CONTACTS_COL_EMAIL As Integer = 4
    Public Const GRID_CONTACTS_COL_ADDRESS_TYPE As Integer = 5
    Public Const GRID_CONTACTS_COL_EFFECTIVE_DATE As Integer = 6
    Public Const GRID_CONTACTS_COL_EXPIRATION_DATE As Integer = 7

    Public Const GRID_QUANTITY_COL_ID As Integer = 0
    Public Const GRID_QUANTITY_COL_EQUIPMENT_TYPE As Integer = 1
    Public Const GRID_QUANTITY_COL_MANUFACTURER As Integer = 2
    Public Const GRID_QUANTITY_COL_MODEL As Integer = 3
    Public Const GRID_QUANTITY_COL_SKU As Integer = 4
    Public Const GRID_QUANTITY_COL_SKU_DESC As Integer = 5
    Public Const GRID_QUANTITY_COL_QUANTITY As Integer = 6
    Public Const GRID_QUANTITY_COL_PRICE As Integer = 7
    Public Const GRID_QUANTITY_COL_CONDITION As Integer = 8
    Public Const GRID_QUANTITY_COL_EFFECTIVE As Integer = 9
    Public Const GRID_QUANTITY_COL_EXPIRATION As Integer = 10

    Public Const GRID_SCHEDULE_COL_ID As Integer = 0
    Public Const GRID_SCHEDULE_COL_SERVICE_CLASS As Integer = 1
    Public Const GRID_SCHEDULE_COL_SERVICE_TYPE As Integer = 2
    Public Const GRID_SCHEDULE_COL_SCHEDULE As Integer = 3
    Public Const GRID_SCHEDULE_COL_DAY As Integer = 4
    Public Const GRID_SCHEDULE_COL_FROM As Integer = 5
    Public Const GRID_SCHEDULE_COL_TO As Integer = 6
    Public Const GRID_SCHEDULE_COL_EFFECTIVE As Integer = 7
    Public Const GRID_SCHEDULE_COL_EXPIRATION As Integer = 8

    Public Const GRID_ATTRIBUTE_COL_ID As Integer = 0
    Public Const GRID_ATTRIBUTE_COL_ATTRIBUTE_NAME As Integer = 1
    Public Const GRID_ATTRIBUTE_COL_VALUE As Integer = 2
    Public Const GRID_ATTRIBUTE_COL_DATA_TYPE As Integer = 3

    Public Const SAVE_COMMAND_NAME_QNTY As String = "QuantitySaveRecord"
    Public Const CANCEL_RECORD_NAME_QNTY As String = "QuantityCancelRecord"
    Public Const SAVE_COMMAND_NAME_SCHDL As String = "ScheduleSaveRecord"
    Public Const CANCEL_RECORD_NAME_SCHDL As String = "ScheduleCancelRecord"
    Public Const SAVE_COMMAND_NAME_CONTCT As String = "ContactSaveRecord"
    Public Const CANCEL_RECORD_NAME_CONTCT As String = "ContactCancelRecord"
    Public Const SAVE_COMMAND_NAME_ATTR As String = "AttributeSaveRecord"
    Public Const CANCEL_RECORD_NAME_ATTR As String = "AttributeCancelRecord"

    Public Const GRID_CONTROL_QUANTITY As String = "txtQuantity"
    Public Const GRID_CONTROL_SERVICE_CLASS As String = "cboServiceClass"
    Public Const GRID_CONTROL_SERVICE_TYPE As String = "cboServiceType"
    Public Const GRID_CONTROL_SCHEDULE As String = "cboSchedule"
    Public Const GRID_CONTROL_DAY_OF_WEEK As String = "cboDayOfWeek"
    Public Const GRID_CONTROL_FROM As String = "txFrom"
    Public Const GRID_CONTROL_TO As String = "txtTo"
    Public Const GRID_CONTROL_EFFECTIVE As String = "txtEffective"
    Public Const GRID_CONTROL_EXPIRATION As String = "txtExpiration"

    Private Const TAB_TOTAL_COUNT As Integer = 11
    Private Const TAB_ADDRESS As Integer = 0
    Private Const TAB_MFG_AUTH_SVC_CTR As Integer = 1
    Private Const TAB_COVERED_DISTRICT As Integer = 2
    Private Const TAB_DEALER_PREFERRED As Integer = 3
    Private Const TAB_SERVICE_NETWORK As Integer = 4
    Private Const TAB_COMMENTS As Integer = 5
    Private Const TAB_METHOD_OF_REPAIR As Integer = 6
    Private Const TAB_CONTACT As Integer = 7
    Private Const TAB_ATTRIBUTE As Integer = 8
    Private Const TAB_QUANTITY As Integer = 9
    Private Const TAB_SCHEDULE As Integer = 10
    Private Const TAB_PRICE_LIST As Integer = 11

    Private Const SVC_PRICE_LIST_RECON_ID_COL As Integer = 0
    Private Const SVC_PRICE_LIST_ID_COL As Integer = 1
    Private Const SVC_REQUESTED_BY_COL As Integer = 2
    Private Const SVC_REQUESTED_DATE_COL As Integer = 3
    Private Const SVC_STATUS_XCD_COL As Integer = 4
    Private Const SVC_STATUS_MODIFIED_by_COL As Integer = 5
    Private Const SVC_STATUS_DATE_COL As Integer = 6

    Private Const PRICE_LIST_RECON_ID_LABEL As String = "lblColPriceListReconId"
    Private Const PRICE_LIST_ID_LABEL As String = "lblColPriceListId"
    Private Const APPROVED_BY_LABEL As String = "lblColApprovedBy"
    Private Const STATUS_XCD_LABEL As String = "lblColStatusXcd"
    Private Const REQUESTED_BY_LABEL As String = "lblColRequestedBy"
    Private Const REQUESTED_DATE_LABEL As String = "lblColRequestedDate"

    Public Const STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL As String = "SVC_PL_RECON_PROCESS-PENDINGAPPROVAL"
    Public Const STATUS_SVC_PL_RECON_PROCESS_APPROVED As String = "SVC_PL_RECON_PROCESS-APPROVED"
    Public Const STATUS_SVC_PL_RECON_PROCESS_REJECTED As String = "SVC_PL_RECON_PROCESS-REJECTED"
    Public Const STATUS_SVC_PL_RECON_PROCESS As String = "SVC_PL_RECON_PROCESS"
    Public Const STATUS_SVC_PL_RECON_PROCESS_PENDINGSUBMISSION As String = "SVC_PL_RECON_PROCESS-PENDINGSUBMISSION"


#End Region

#Region "Attributes"
    Private moSvcCtr As ServiceCenter
    Private listDisabledTabs As New Collections.Generic.List(Of Integer)
    Private SelectedTabIndex As Integer = 0
#End Region

#Region "Properties"

    Private Property SvcCtrId() As Guid
        Get
            Return State.SvcCtrId
        End Get
        Set(Value As Guid)
            State.SvcCtrId = Value
        End Set

    End Property


    Private ReadOnly Property IsNewServiceCenter() As Boolean
        Get
            Return State.MyBO.IsNew
        End Get

    End Property


    Public ReadOnly Property AddressCtr() As UserControlAddress_New
        Get
            Return moAddressController
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

    Public ReadOnly Property UserControlAddress() As UserControlAddress_New
        Get
            If moUserControlAddress Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
            End If
            Return moUserControlContactInfo
        End Get
    End Property

#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceCenter
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceCenter, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public SVCId As Guid
        Public PageDealerId As Guid
        Public mbIsComingFromDealerform As Boolean = False
        Public Sub New(SVCorDealerId As Guid, Optional ByVal bIsComingFromDealerform As Boolean = False)
            mbIsComingFromDealerform = bIsComingFromDealerform
            If bIsComingFromDealerform Then
                PageDealerId = SVCorDealerId
            Else
                SVCId = SVCorDealerId
            End If
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public MyBO As ServiceCenter
        Public PageIndex As Integer = 0
        Public ScreenSnapShotBO As ServiceCenter
        Public BankInfoBO As BankInfo
        Public oRoute As Route
        Public pageParameters As Parameters
        Public IsNew As Boolean = False
        Public stIsComingFromDealerform As Boolean = False
        Public SvcCtrId, stdealerid As Guid
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public ForEdit As Boolean = False
        'Public Statusdv As DataView
        Public Statusdv As DataElements.ListItem()
        Public callGVS As Boolean = False
        Public ContactsSelectedChildId As Guid
        Public QuantitySelectedChildId As Guid
        Public ScheduleSelectedChildId As Guid
        Public IsQuantityEditing As Boolean
        Public IsContactsEditing As Boolean
        Public IsScheduleEditing As Boolean
        Public myContactsChildBO As VendorContact
        Public myQuantityChildBO As VendorQuantity
        Public myScheduleChildBO As ServiceSchedule
        Public myScheduleTableChildBO As Schedule
        Public myScheduleDetailChildBO As ScheduleDetail
        Public myContactInfoChildBO As ContactInfo
        Public myAddressChildBO As Address
        Public ContactsDetailPageIndex As Integer = 0
        Public QuantityDetailPageIndex As Integer = 0
        Public ScheduleDetailPageIndex As Integer = 0
        Public SortExpressionContactsGrid As String = ServiceCenter.ContactsView.COL_NAME_NAME
        Public SortExpressionQuantityGrid As String = ServiceCenter.QuantityView.COL_NAME_VENDOR_SKU
        Public SortExpressionScheduleGrid As String = ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID

        Public MethodOfRepairList As Collections.Generic.List(Of ServCenterMethRepair)
        Public MethodOfRepairAction As Integer = MethodOfRepairNone
        Public MethodOfRepairWorkingItem As ServCenterMethRepair
        Public priceListApprovalflag As String

        Public MySvcCompany As ArrayList = New ArrayList
        Public SvcPriceReconIDMask As String
        Public StatusXcdMask As String
        Public SvcPriceListMask As String
        Public CompanyGroupId As Guid
        Public SvcPriceListReconID As Guid   'Id As Guid
        Public SvcServiceCenterID As Guid   'Id As Guid
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public searchDV As DataView = Nothing
        Public YESNOdv As DataView = Nothing
        Public editRowIndex As Integer
        'Public SortExpression As String = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public bnoRow As Boolean = False
        Public PriceListGridSelectedIndex As Integer = 0
        'Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public selectedSvcPriceListReconId As Guid = Guid.Empty
        Public PageSize As Integer = 5

        Public SvcPriciListDV As DataView = Nothing
        'Public IsCopy As Boolean = False

        Public CurrentPriceListCode As String = Nothing
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
                State.pageParameters = CType(CallingParameters, Parameters)
                If Not State.pageParameters.mbIsComingFromDealerform Then
                    State.MyBO = New ServiceCenter(State.pageParameters.SVCId)
                    If State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                        State.MyBO.isBankInfoNeedDeletion = False
                    Else
                        State.MyBO.isBankInfoNeedDeletion = True
                    End If
                    If Not State.MyBO.RouteId.Equals(Guid.Empty) Then
                        State.oRoute = New Route(State.MyBO.RouteId)
                    End If
                    State.stdealerid = System.Guid.Empty
                    'Me.State.stIsComingFromDealerform = False
                Else
                    State.stdealerid = State.pageParameters.PageDealerId
                    State.stIsComingFromDealerform = True
                End If
            Else
                State.stdealerid = System.Guid.Empty
                'Me.State.stIsComingFromDealerform = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#Region "Ajax State"

    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get

    End Property

#End Region

#End Region

#Region "Handlers"


#Region "Handlers-Page Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TABLES)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
            UpdateBreadCrum()
            UserControlContactInfo.ShowJobTitle = True
            UserControlContactInfo.ShowCompany = True
            CType(moBankInfoController.FindControl("HiddenClassName"), HiddenField).Value = "borderLeft"
            moMessageController.Clear_Hide()

            If Not IsPostBack Then
                MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ServiceCenter
                    State.IsNew = True
                End If
                'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
                Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
                Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                                Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                                Select Country).ToArray()
                moCountryDrop.Populate(UserCountries.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = False
                                    })
                InitializePage()
                TranslateGridHeader(moContactsGridView)
                TranslateGridHeader(moQuantityGridView)
                TranslateGridHeader(moScheduleGridView)
                TranslateGridHeader(moAddScheduleGridView)
                SetGridItemStyleColor(GridViewMethodOfRepair)
                TranslateGridHeader(GridViewMethodOfRepair)
                'TranslateGridHeader(Me.Grid)
                TranslateGridHeader(DataGridPriceList)
                AttributeValues.TranslateHeaders()
                PopulateChildern()
                PopulateCountry()
                PopulateDropdowns()
                PopulateIntegratedWithDropdowns()
                moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                State.BankInfoBO = Nothing
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                PopulateFormFromBOs()
                EnableDisableFields()
            Else
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                SelectedTabIndex = hdnSelectedTab.Value
            End If

            BindBoPropertiesToLabels()
            BindBoPropertiesToMethodOfRepairGridHeaders()
            ClearGridViewHeadersAndLabelsErrorSign()
            CheckIfComingFromSaveConfirm()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
            'BindBoPropertiesToGridHeaders()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)


    End Sub

    'Private Sub ddlPriceList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPriceList.SelectedIndexChanged
    '    Try

    '        Dim strPriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(Me.ddlPriceList.SelectedValue))
    '        ' Me.PopulateBOProperty(Me.State.MyBO, "PriceListCode", strPriceListCode)
    '        Me.PopulateBOProperty(Me.State.MyBO, "PriceListCodeinprogress", strPriceListCode)

    '        Dim oListContext1 As New ListContext
    '        oListContext1.CountryId = Me.State.MyBO.CountryId
    '        Dim SVC_PL_Process As DataElements.ListItem() =
    '                            CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

    '        Dim SVC_PL_Process_Status As String = (From lst In SVC_PL_Process
    '                                               Where lst.Code = "PENDINGSUBMISSION"
    '                                               Select lst.Translation).FirstOrDefault()

    '        Me.PopulateBOProperty(Me.State.MyBO, "PriceListCodeStatusInProgress", SVC_PL_Process_Status)

    '        If Not String.IsNullOrEmpty(Me.State.MyBO.PriceListCode) Then
    '            EnableTab(TAB_SCHEDULE, True)
    '            EnableTab(TAB_QUANTITY, True)
    '            'refresh the quantity based on the newly selected price list, if valid price list is selected
    '            Me.PopulateQuantity()
    '        Else
    '            EnableTab(TAB_SCHEDULE, False)
    '            EnableTab(TAB_QUANTITY, False)
    '            'Set focus to Address tab, since the quantity and schedule tab is disabled
    '            SelectedTabIndex = 0
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If State.MyBO.ConstrVoilation = False Then
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            Else
                ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            State.ForEdit = True
            PopulateBOsFromForm()

            If State.MyBO.IsDirty Then
                'check the Vendor Quantity records, if they are to be added or updated
                Dim detail As VendorQuantity
                For Each detail In State.MyBO.QuantityChildren
                    If Not detail.VendorQuantityRecordAvaliable Then
                        detail.SetRowStateAsAdded()
                    End If
                Next
                If State.MyBO.IsNew Then
                    State.MyBO.Save()
                    'State.MySvcBO.Save()
                    SaveAllRecordsMethodOfRepair(True)
                Else

                    If Not State.IsNew Then

                        If Not String.IsNullOrEmpty(State.priceListApprovalflag) AndAlso State.priceListApprovalflag = Codes.EXT_YESNO_Y Then
                            'Only after Approval, Price List will be associated to Service Center
                            PopulateBOProperty(State.MyBO, "PriceListCode", State.CurrentPriceListCode)
                        End If

                    End If

                    State.MyBO.Save()
                    'State.MySvcBO.Save()
                End If

                State.SvcPriciListDV = Nothing
                PopulateSvcDataGridPriceList()
                'Me.State.IsCopy = False
                State.IsNew = False
                State.HasDataChanged = True

                PopulateCountry()

                'Dim dvPriceList As DataView
                'dvPriceList = LookupListNew.GetPriceListLookupList(Me.State.MyBO.CountryId)
                Dim oListContext1 As New ListContext
                oListContext1.CountryId = State.MyBO.CountryId
                Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
                Dim PriceListID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PRICE_LIST, State.MyBO.PriceListCode)

                If Not State.MyBO.IsNew AndAlso PriceListID.ToString() = NOTHING_SELECTED_GUID _
                     AndAlso State.MyBO.PriceListCode IsNot Nothing Then
                    'add svc specific code
                    'Dim drServiceSpecificPriceListCode As DataRow = dvPriceList.Table.NewRow()
                    'drServiceSpecificPriceListCode("CODE") = Me.State.MyBO.PriceListCode
                    'drServiceSpecificPriceListCode("DESCRIPTION") = Me.State.MyBO.PriceListCode
                    'dvPriceList.Table.Rows.Add(drServiceSpecificPriceListCode)
                    Dim _item As DataElements.ListItem = New DataElements.ListItem()
                    _item.Code = State.MyBO.PriceListCode
                    _item.Translation = State.MyBO.PriceListCode
                    PriceList.ToList().Add(_item)
                End If
                'Me.BindListControlToDataView(Me.ddlPriceList, dvPriceList, , , True)
                ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

                PopulateIntegratedWithDropdowns()
                PopulateFormFromBOs(CMD_SAVE)
                EnableDisableFields()

                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                If State.stIsComingFromDealerform Then
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, State.MyBO, State.HasDataChanged))
                End If
            Else
                inpLoanerCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LOANER_CENTERS, State.MyBO.LoanerCenterId)
                If Not inpLoanerCenterDesc.Value = TextBoxLoanerCenter.Text Then
                    inpLoanerCenterId.Value = State.MyBO.LoanerCenterId.ToString
                    TextBoxLoanerCenter.Text = inpLoanerCenterDesc.Value
                End If
                inpMasterCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, State.MyBO.MasterCenterId)
                If Not inpMasterCenterDesc.Value = TextBoxMasterCenter.Text Then
                    inpMasterCenterId.Value = State.MyBO.MasterCenterId.ToString
                    TextBoxMasterCenter.Text = inpMasterCenterDesc.Value
                End If
                MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
            'Make the Date Added label and field visible - for a newly added Service Center
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, True)
            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        Finally
            populateVendorManagementControls()
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ServiceCenter(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New ServiceCenter
            End If
            PopulateCountry()
            PopulateFormFromBOs()
            EnableDisableFields()
            EnableDisableTabs(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            ' Delete all Method of Repair associated with Service Center
            DeleteAllRecordsMethodOfRepair()
            DeleteAllPriceListReconRecords()

            'Delete the Address
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
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
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
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

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            PopulateFormFromBOs(CMD_CHANGE_COUNTRY)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "AUTHORIZED MANUFACTURER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedManufacturers_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachManufacturers(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachManufacturers(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "COVERED DISTRICTS: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDistricts_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachDistricts(attachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDistricts_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachDistricts(detachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "PREFERRED DEALER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDealers_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachDealers(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachServiceNetworks(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDealers_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachDealers(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachServiceNetworks(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Client"

    Private Sub cboPaymentMethodId_Client()
        Dim scriptContent As String
        Dim moreIds As New ArrayList
        Dim oPaymentMethodId As Guid = LookupListNew.GetIdFromCode(
            LookupListNew.LK_PAYMENTMETHOD, Codes.PAYMENT_METHOD__BANK_TRANSFER)

        ' if PaymentType = BankTransfer Then return true  Else return false
        scriptContent = "var payMDrop = document.getElementById('<%=cboPaymentMethodId.ClientID%>');" & Environment.NewLine
        scriptContent &= "if ( payMDrop.options[payMDrop.selectedIndex].value == '" & oPaymentMethodId.ToString & "' ) { " & Environment.NewLine
        ' Me.moBankInfoController.Visible = True
        scriptContent &= " return true; " & Environment.NewLine
        scriptContent &= " } " & Environment.NewLine
        scriptContent &= " else { " & Environment.NewLine
        scriptContent &= " return false; " & Environment.NewLine
        scriptContent &= " } " & Environment.NewLine

        moreIds.Add("bankInfo_hr1")
        moreIds.Add("bankInfo_hr2")
        moreIds.Add("bankInfo_hr3")
        moreIds.Add("bankInfo_hr4")
        moreIds.Add("bankInfo_hr5")
        moreIds.Add("bankInfo_hr6")
        moreIds.Add("bankInfo_hr7")
        moreIds.Add("bankInfo_hr8")
        moreIds.Add("bankInfo_hr9")
        moreIds.Add("bankInfo_hr10")
        ControlMgr.ClientRegister(Me, cboPaymentMethodId, ControlMgr.SourceEvent.onchange,
                "PaymentTypeChanged", scriptContent, moBankInfoController, ControlMgr.TargetAction.Visible,
                moreIds)
    End Sub

#End Region

#Region "Ajax"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateMasterCenterDrop(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.CountryId)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateLoanerCenterDrop(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetLoanerCenterLookupList(AjaxState.MyBO.CountryId)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

#End Region

#End Region

#Region "Controlling Logic"
    Protected Sub ResolveShippingFeeVisibility()
        CheckBoxShipping.Attributes.Add("onClick", "showProcessingFee(this);")
        If Not CheckBoxShipping.Checked Then
            LabelProcessingFee.Style.Add("Display", "'none'")
            TextboxPROCESSING_FEE.Style.Add("Display", "'none'")
        Else
            LabelProcessingFee.Style.Add("Display", "''")
            TextboxPROCESSING_FEE.Style.Add("Display", "''")
        End If
    End Sub

    Protected Sub EnableDisableFields()
        'Disabled by Default
        ControlMgr.SetEnableControl(Me, TextboxCode, False)
        ControlMgr.SetEnableControl(Me, TextboxIntegratedAsOf, False)

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        ' do NOT show original dealer id if users company is NOT VSC
        If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR Then
            cboOriginalDealer_WRITE.Visible = True
            lblDealer.Visible = True
        Else
            cboOriginalDealer_WRITE.Visible = False
            lblDealer.Visible = False
        End If

        'enable/disable/visible depending on if coming from dealerform
        ControlMgr.SetEnableControl(Me, cboOriginalDealer_WRITE, State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, lblDealer, State.stdealerid.Equals(Guid.Empty))

        'Now disable depending on the object state
        If State.MyBO.IsNew Then
            'Enable and blank out the Service Center Code field
            ControlMgr.SetEnableControl(Me, TextboxCode, True)
            TextboxCode.Text = String.Empty

            'Blank out the Service Center Description field
            TextboxDescription.Text = String.Empty

            'Hide the DateAdded and DateLastMaintained Labels and TextBox fields
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, False)
            ControlMgr.SetVisibleControl(Me, LabelDateLastMaintained, False)
            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, False)
            ControlMgr.SetVisibleControl(Me, TextboxDateLastMaintained, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetVisibleControl(Me, IntegratedAsOfLabel, False)
            ControlMgr.SetVisibleControl(Me, TextboxIntegratedAsOf, False)
        Else
            If State.MyBO.IntegratedAsOf Is Nothing Then
                ControlMgr.SetVisibleControl(Me, IntegratedAsOfLabel, False)
                ControlMgr.SetVisibleControl(Me, TextboxIntegratedAsOf, False)
            Else
                ControlMgr.SetVisibleControl(Me, IntegratedAsOfLabel, True)
                ControlMgr.SetVisibleControl(Me, TextboxIntegratedAsOf, True)
            End If
        End If

        'WRITE YOUR OWN CODE HERE

        'Disable the DateAdded and DateLastMaintained fields
        ControlMgr.SetEnableControl(Me, TextboxDateAdded, False)
        ControlMgr.SetEnableControl(Me, TextboxDateLastMaintained, False)
        AddressCtr.EnableControls(False)

        If Not String.IsNullOrEmpty(State.MyBO.PriceListCode) Then
            EnableTab(TAB_SCHEDULE, True)
            EnableTab(TAB_QUANTITY, True)
        Else
            EnableTab(TAB_SCHEDULE, False)
            EnableTab(TAB_QUANTITY, False)
            'Set the Address tab as selected in case the Schedule or Quantity tab is disabled
            SelectedTabIndex = 0
        End If

        If CheckBoxShipping.Checked Then
            LabelProcessingFee.Visible = True
            TextboxPROCESSING_FEE.Visible = True
        Else
            LabelProcessingFee.Visible = False
            TextboxPROCESSING_FEE.Visible = False
        End If

        'if the contact info is new then effective date should be editable or else disabled
        If State.myContactsChildBO IsNot Nothing Then
            If State.myContactsChildBO.IsNew Then
                txtEffective.Enabled = True
                ibtnEffective.Enabled = True
            Else
                txtEffective.Enabled = False
                ibtnEffective.Enabled = False
            End If
        End If

        DisplayBankInfo()

        Try

            State.priceListApprovalflag = New Country(State.MyBO.CountryId).PriceListApprovalNeeded.ToString()
        Catch
            State.priceListApprovalflag = Codes.EXT_YESNO_N
        End Try

        If State.priceListApprovalflag IsNot Nothing AndAlso Not State.priceListApprovalflag = String.Empty AndAlso State.priceListApprovalflag = Codes.EXT_YESNO_Y Then

            PL_APPROVE_SEC.Visible = True

            If Not IsNothing(State.MyBO.CurrentSVCPLRecon) Then

                If Not String.IsNullOrEmpty(State.MyBO.CurrentSVCPLRecon.Status_xcd) AndAlso State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL Then
                    ddlPriceList.Enabled = False
                Else
                    ddlPriceList.Enabled = True
                End If
            Else
                ddlPriceList.Enabled = True
                PL_APPROVE_SEC.Visible = False
            End If

        Else
            ddlPriceList.Enabled = True
            PL_APPROVE_SEC.Visible = False
        End If

        If Not IsNothing(State.MyBO.ClaimReservedBasedOnXcd) then
            ControlMgr.SetVisibleControl(Me, lblclaimreservedPercent, True)
            ControlMgr.SetVisibleControl(Me, txtclaimreservedPercent, True)
        else
            txtclaimreservedPercent.text = string.empty
            ControlMgr.SetVisibleControl(Me, lblclaimreservedPercent, False)
            ControlMgr.SetVisibleControl(Me, txtclaimreservedPercent, False)
        End If

        'disable the controls if user has view only permission for this form
        If Me.PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then
            SetEnabledForControlFamily(EditPanel, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ServiceGroupId", LabelServiceGroupId)
        BindBOPropertyToLabel(State.MyBO, "LoanerCenterId", LabelLoanerCenterId)
        BindBOPropertyToLabel(State.MyBO, "MasterCenterId", LabelMasterCenterId)
        BindBOPropertyToLabel(State.MyBO, "Code", LabelCode)
        BindBOPropertyToLabel(State.MyBO, "IntegratedAsOf", IntegratedAsOfLabel)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        BindBOPropertyToLabel(State.MyBO, "RatingCode", LabelRatingCode)
        BindBOPropertyToLabel(State.MyBO, "ContactName", LabelContactName)
        BindBOPropertyToLabel(State.MyBO, "OwnerName", LabelOwnerName)
        BindBOPropertyToLabel(State.MyBO, "Phone1", LabelPhone1)
        BindBOPropertyToLabel(State.MyBO, "Phone2", LabelPhone2)
        BindBOPropertyToLabel(State.MyBO, "Fax", LabelFax)
        BindBOPropertyToLabel(State.MyBO, "Email", LabelEmail)
        BindBOPropertyToLabel(State.MyBO, "CcEmail", LabelCcEmail)
        BindBOPropertyToLabel(State.MyBO, "FtpAddress", LabelFtpAddress)
        BindBOPropertyToLabel(State.MyBO, "TaxId", LabelTaxId)
        BindBOPropertyToLabel(State.MyBO, "ServiceWarrantyDays", LabelServiceWarrantyDays)
        BindBOPropertyToLabel(State.MyBO, "StatusCode", LabelStatusCode)
        BindBOPropertyToLabel(State.MyBO, "BusinessHours", LabelBusinessHours)
        BindBOPropertyToLabel(State.MyBO, "DateAdded", LabelDateAdded)
        BindBOPropertyToLabel(State.MyBO, "DateModified", LabelDateLastMaintained)
        BindBOPropertyToLabel(State.MyBO, "WithholdingRate", lblWithholdingRate)
        BindBOPropertyToLabel(State.MyBO, "comments", LabelComment1)
        BindBOPropertyToLabel(State.MyBO, "PaymentMethodId", PaymentMethodDrpLabel)
        BindBOPropertyToLabel(State.MyBO, "ReverseLogisticsId", lblReverseLogistics)
        BindBOPropertyToLabel(State.MyBO, "ProcessingFee", LabelProcessingFee)
        BindBOPropertyToLabel(State.MyBO, "DistributionMethodId", lblDistributionMethod)
        BindBOPropertyToLabel(State.MyBO, "FulfillmentTimeZoneId", lblFulFillingTimeZone)
        BindBOPropertyToLabel(State.MyBO, "DiscountPct", lblDiscountPercent)
        BindBOPropertyToLabel(State.MyBO, "NetDays", lblNetDays)
        BindBOPropertyToLabel(State.MyBO, "DiscountDays", lblDiscountDays)
        BindBOPropertyToLabel(State.MyBO, "PriceListCode", lblPriceList)

        BindBOPropertyToLabel(State.MyBO, "PriceListCodeinprogress", lblPriceListPending)
        BindBOPropertyToLabel(State.MyBO, "PriceListCodeStatusInProgress", lblPriceListPendingStatus)

        AddressCtr.SetTheRequiredFields()

        'added by Anindita - original dealer id was not added here
        BindBOPropertyToLabel(State.MyBO, "OriginalDealerId", lblDealer)
        ClearGridHeadersAndLabelsErrSign()

        BindBOPropertyToLabel(State.myContactsChildBO, "Effective", lblEffectiveDate)
        BindBOPropertyToLabel(State.myContactsChildBO, "Expiration", lblExpirationDate)
        BindBOPropertyToLabel(State.MyBO, "PreInvoiceId", lblPreInvoice)

        BindBOPropertyToLabel(State.MyBO, "ClaimReservedPercent", lblclaimreservedPercent)
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
            If State.BankInfoBO IsNot Nothing Then
                State.BankInfoBO.SourceCountryID = GetSelectedItem(moCountryDrop)
            End If
        Else
            oCountry = New Country(State.MyBO.CountryId)
            SetSelectedItem(moCountryDrop, State.MyBO.CountryId)
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
            If State.BankInfoBO IsNot Nothing Then
                State.BankInfoBO.SourceCountryID = State.MyBO.CountryId
            End If
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso State.IsNew) Then
            ' Multiple Countries
            ControlMgr.SetVisibleControl(Me, moCountryDrop, True)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
        Else
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, True)
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        'Dim DistMethod As DataView = LookupListNew.DropdownLookupList("DISTMETH", langId, True)
        'Dim TimeZone As DataView = LookupListNew.DropdownLookupList("TZN", langId, True)
        'Dim Statusdv As DataView = LookupListNew.DataView(LookupListNew.LK_SERVICE_CENTER_STATUS)
        Dim Statusdv As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="SCSTAT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        'Dim PreInvoice As DataView = LookupListNew.DropdownLookupList(Codes.LIST__SVCPREINV, langId, True)
        State.Statusdv = Statusdv

        With State.MyBO
            'Me.BindListControlToDataView(Me.cboServiceGroupId, LookupListNew.GetServiceGroupLookupList(.CountryId), , , True)
            Dim oListContext2 As New ListContext
            oListContext2.CountryId = .CountryId
            cboServiceGroupId.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceGroupByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext2),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.cboPaymentMethodId, LookupListNew.GetPaymentMethodLookupList(langId, UseElitaBankInfo), , , True)
            Dim PaymentMethod As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PMTHD", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            If Not UseElitaBankInfo() Then
                PaymentMethod = (From lst In PaymentMethod
                                 Where lst.Code <> "CTT"
                                 Select lst).ToArray()
            End If
            cboPaymentMethodId.Populate(PaymentMethod.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.cboOriginalDealer_WRITE, LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Me.State.MyBO.Id), , , True)
            Dim oListContext3 As New ListContext
            oListContext3.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            oListContext3.ServiceCenterId = State.MyBO.Id
            cboOriginalDealer_WRITE.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="OriginalDealerByCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext3),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

            'Me.BindListControlToDataView(Me.cboStatusCode, Statusdv)
            cboStatusCode.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SCSTAT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .TextFunc = AddressOf .GetCode,
                            .SortFunc = AddressOf .GetCode
                        })
            'Me.BindListControlToDataView(Me.cboReverseLogisticsId, yesNoLkL)
            cboReverseLogisticsId.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.ddlDistributionMethod, DistMethod, , , True)
            ddlDistributionMethod.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="DISTMETH", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.ddlFulFillingTimeZone, TimeZone, , , True)
            ddlFulFillingTimeZone.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="TZN", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'BindListControlToDataView(Me.ddlPreInvoice, PreInvoice, , , False)
            ddlPreInvoice.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCPREINV", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })

            Dim dvPriceList As DataView
            dvPriceList = LookupListNew.GetPriceListLookupList(.CountryId)
            Dim oListContext1 As New ListContext
            oListContext1.CountryId = State.MyBO.CountryId
            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            Dim PriceListID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PRICE_LIST, State.MyBO.PriceListCode)

            If Not State.MyBO.IsNew AndAlso PriceListID.ToString() = NOTHING_SELECTED_GUID _
                 AndAlso State.MyBO.PriceListCode IsNot Nothing Then
                'add svc specific code
                'Dim drServiceSpecificPriceListCode As DataRow = dvPriceList.Table.NewRow()
                'drServiceSpecificPriceListCode("CODE") = Me.State.MyBO.PriceListCode
                'drServiceSpecificPriceListCode("DESCRIPTION") = Me.State.MyBO.PriceListCode
                'dvPriceList.Table.Rows.Add(drServiceSpecificPriceListCode)
                Dim _item As DataElements.ListItem = New DataElements.ListItem()
                _item.Code = State.MyBO.PriceListCode
                _item.Translation = State.MyBO.PriceListCode
                PriceList.ToList().Add(_item)

                moMessageController.AddWarning(String.Format("{0}",
                TranslationBase.TranslateLabelOrMessage("PRICELISTCODE_IS_EXPIRED"), False))
            End If

            'Me.BindListControlToDataView(Me.ddlPriceList, dvPriceList, , , True)
            ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            'ddlAutoProcessInventoryFile.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            ddlAutoProcessInventoryFile.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .BlankItemValue = String.Empty,
                            .ValueFunc = AddressOf .GetExtendedCode
                        })

            ddlClaimReservedbasedon.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CLAIMRESERVED", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                                                    New PopulateOptions() With
                                                       {
                                                       .AddBlankItem = True,
                                                       .BlankItemValue = String.Empty,
                                                       .ValueFunc = AddressOf .GetExtendedCode
                                                       })
        End With
    End Sub

    Protected Sub PopulateIntegratedWithDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With State.MyBO
            'Dim dvIntegratedWith As DataView = LookupListNew.GetIntegratedWithLookupList(langId)
            'Dim integratedWithCode As String = LookupListNew.GetCodeFromId(dvIntegratedWith, .IntegratedWithID)
            Dim IntegratedWith As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="INTR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim integratedWithCode As String = (From lst In IntegratedWith
                                                Where lst.ListItemId = .IntegratedWithID
                                                Select lst.Code).FirstOrDefault()
            If integratedWithCode IsNot Nothing AndAlso integratedWithCode = Codes.INTEGRATED_WITH_AGVS Then
                'dvIntegratedWith.RowFilter = "code <> '" & Codes.INTEGRATED_WITH_GVS & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                'Me.BindListControlToDataView(Me.cboIntegratedWithId, dvIntegratedWith, , , True)
                cboIntegratedWithId.Populate((From lst In IntegratedWith
                                                 Where lst.Code <> Codes.INTEGRATED_WITH_GVS
                                                 Select lst).ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Else
                'dvIntegratedWith.RowFilter = "code <> '" & Codes.INTEGRATED_WITH_AGVS & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                'Me.BindListControlToDataView(Me.cboIntegratedWithId, dvIntegratedWith, , , True)
                cboIntegratedWithId.Populate((From lst In IntegratedWith
                                                 Where lst.Code <> Codes.INTEGRATED_WITH_AGVS
                                                 Select lst).ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            End If

        End With
    End Sub

    Private Function UseElitaBankInfo() As Boolean
        'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        Dim noId As Guid = (From lst In (CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()))
                            Where lst.Code = "N"
                            Select lst.ListItemId).FirstOrDefault()
        Dim _UseElitaBankInfo As Boolean = True

        For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
            If _acctCo.UseElitaBankInfoId = noId Then
                _UseElitaBankInfo = False
            Else
                Return True
            End If
        Next

        Return _UseElitaBankInfo
    End Function

    Protected Sub PopulateFormFromBOs(Optional ByVal cmd As Integer = 0)
        'Dim statusdv As DataView = Me.State.Statusdv

        With State.MyBO
            PopulateControlFromPropertyName(State.MyBO, cboServiceGroupId, "ServiceGroupId")
            inpLoanerCenterId.Value = .LoanerCenterId.ToString
            inpLoanerCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LOANER_CENTERS, .LoanerCenterId)
            TextBoxLoanerCenter.Text = inpLoanerCenterDesc.Value
            inpMasterCenterId.Value = .MasterCenterId.ToString
            inpMasterCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .MasterCenterId)
            TextBoxMasterCenter.Text = inpMasterCenterDesc.Value
            PopulateControlFromPropertyName(State.MyBO, cboPaymentMethodId, "PaymentMethodId")
            PopulateControlFromPropertyName(State.MyBO, cboReverseLogisticsId, "ReverseLogisticsId")

            SetSelectedItem(cboIntegratedWithId, .IntegratedWithID)
            PopulateControlFromBOProperty(chkPayMaster, .PayMaster)

            If State.stIsComingFromDealerform Then
                SetSelectedItem(cboOriginalDealer_WRITE, State.stdealerid)
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    cboOriginalDealer_WRITE.SelectedIndex = NOTHING_SELECTED
                ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso Not State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    If State.MyBO.IsNew Then
                        cboOriginalDealer_WRITE.SelectedIndex = NOTHING_SELECTED
                    Else
                        SetSelectedItem(cboOriginalDealer_WRITE, .OriginalDealerId)
                    End If
                Else
                    SetSelectedItem(cboOriginalDealer_WRITE, System.Guid.Empty)
                End If
            End If

            PopulateControlFromBOProperty(TextboxCode, .Code)
            PopulateControlFromBOProperty(TextboxDescription, .Description)
            PopulateControlFromBOProperty(TextboxRatingCode, .RatingCode)
            PopulateControlFromBOProperty(TextboxContactName, .ContactName)
            PopulateControlFromBOProperty(TextboxOwnerName, .OwnerName)
            PopulateControlFromBOProperty(TextBoxWithholdingRate, .WithholdingRate)
            PopulateControlFromBOProperty(TextboxPhone1, .Phone1)
            PopulateControlFromBOProperty(TextboxPhone2, .Phone2)
            PopulateControlFromBOProperty(TextboxFax, .Fax)
            PopulateControlFromBOProperty(TextboxEmail, .Email)
            PopulateControlFromBOProperty(TextboxCcEmail, .CcEmail)
            PopulateControlFromBOProperty(TextboxFtpAddress, .FtpAddress)
            PopulateControlFromBOProperty(TextboxTaxId, .TaxId)
            PopulateControlFromBOProperty(TextboxServiceWarrantyDays, .ServiceWarrantyDays)
            'Me.PopulateControlFromBOProperty(Me.cboStatusCode, LookupListNew.GetIdFromCode(statusdv, .StatusCode))
            PopulateControlFromBOProperty(cboStatusCode, (From lst In State.Statusdv
                                                                Where lst.Code = .StatusCode
                                                                Select lst.ListItemId).FirstOrDefault())
            PopulateControlFromBOProperty(TextboxBusinessHours, .BusinessHours)
            PopulateControlFromBOProperty(CheckBoxDefaultToEmail, .DefaultToEmailFlag)
            PopulateControlFromBOProperty(CheckBoxIvaResponsible, .IvaResponsibleFlag)
            PopulateControlFromBOProperty(CheckBoxFreeZone, .FreeZoneFlag)
            PopulateControlFromBOProperty(TextboxDateAdded, .CreatedDate)
            PopulateControlFromBOProperty(TextboxDateLastMaintained, .ModifiedDate)
            PopulateControlFromBOProperty(TextboxIntegratedAsOf, .IntegratedAsOf)
            PopulateControlFromBOProperty(TextboxComment, .Comments)
            PopulateControlFromBOProperty(CheckBoxShipping, .Shipping)

            If .Shipping Then
                If CheckBoxShipping.Checked Then
                    ControlMgr.SetVisibleControl(Me, LabelProcessingFee, True)
                    ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, True)
                Else
                    ControlMgr.SetVisibleControl(Me, LabelProcessingFee, False)
                    ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, False)
                End If
                PopulateControlFromBOProperty(TextboxPROCESSING_FEE, .ProcessingFee)
            End If

            AddressCtr.Bind(.Address)

            If State.IsNew Then
                If State.BankInfoBO Is Nothing Then
                    State.BankInfoBO = State.MyBO.Add_BankInfo
                End If
            Else
                State.BankInfoBO = State.MyBO.Add_BankInfo
            End If

            UserBankInfoCtr.SetTheRequiredFields()
            If Not (cmd = CMD_SAVE) Then
                State.BankInfoBO.SourceCountryID = State.MyBO.CountryId
            Else
                State.BankInfoBO.SourceCountryID = State.MyBO.CountryId
            End If

            UserBankInfoCtr.Bind(State.BankInfoBO)
            PopulateControlFromPropertyName(State.MyBO, ddlDistributionMethod, "DistributionMethodId")
            PopulateControlFromPropertyName(State.MyBO, ddlFulFillingTimeZone, "FulfillmentTimeZoneId")
            PopulateControlFromBOProperty(TextBoxDiscountPercent, .DiscountPct)
            PopulateControlFromBOProperty(TextBoxNetDays, .NetDays)
            PopulateControlFromBOProperty(TextBoxDiscountDays, .DiscountDays)
            PopulateControlFromPropertyName(State.MyBO, ddlPreInvoice, "PreInvoiceId")
            ddlAutoProcessInventoryFile.ClearSelection()
            BindSelectItem(State.MyBO.AutoProcessInventoryFileXcd, ddlAutoProcessInventoryFile)

            ddlClaimReservedbasedon.ClearSelection()
            BindSelectItem(.ClaimReservedBasedOnXcd, ddlClaimReservedbasedon)
            If .ClaimReservedPercent IsNot nothing
                PopulateControlFromBOProperty(txtclaimreservedPercent, GetAmountFormattedPercentString(.ClaimReservedPercent))
            End If
            
        End With

        Dim list As DataView = LookupListNew.GetPriceListLookupList(State.MyBO.CountryId)
        Dim selectedItemId As Guid = LookupListNew.GetIdFromCode(list, State.MyBO.PriceListCode)

        PopulateControlFromBOProperty(ddlPriceList, selectedItemId)

        Dim oListContext1 As New ListContext
        oListContext1.CountryId = State.MyBO.CountryId

        'If Me.State.IsCopy = False And Me.State.IsNew = False Then

        If State.MyBO.CurrentSVCPLRecon() IsNot Nothing Then

            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            'Dim PriceListDescription As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.CurrentSVCPLRecon.PriceListId)
            'Me.State.MyBO.PriceListCode = PriceListCode

            PopulateControlFromBOProperty(txtPriceListPending, LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, State.MyBO.CurrentSVCPLRecon.PriceListId))

            Dim SVC_PL_Process As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

            Dim SVC_PL_Process_Status As String = (From lst In SVC_PL_Process
                                                   Where lst.ExtendedCode = State.MyBO.CurrentSVCPLRecon.Status_xcd
                                                   Select lst.Translation).FirstOrDefault()

            PopulateControlFromBOProperty(txtPriceListPendingStatus, SVC_PL_Process_Status)
        End If
        'End If


        If Not State.MyBO.RouteId.Equals(Guid.Empty) Then
            PopulateControlFromBOProperty(TextboxRoute, State.oRoute.Description)
        End If

        PopulateDetailControls()
        populateVendorManagementControls()

        If State.MethodOfRepairList Is Nothing Then
            State.MethodOfRepairList = ServCenterMethRepair.GetMethodOfRepairList(State.MyBO.Id)
        End If
        PopulateMethodOfRepairGrid(State.MethodOfRepairList)
        'PopulateGrid_PriceList()
        PopulateSvcDataGridPriceList()
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Protected Sub PopulateBOsFromForm()

        With State.MyBO

            If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso cboOriginalDealer_WRITE.SelectedValue.Trim.Length > 0 Then
                PopulateBOProperty(State.MyBO, "OriginalDealerId", cboOriginalDealer_WRITE)
            End If

            PopulateBOProperty(State.MyBO, "ServiceGroupId", cboServiceGroupId)

            If Not inpLoanerCenterId.Value = Guid.Empty.ToString Then
                AjaxController.PopulateBOAutoComplete(TextBoxLoanerCenter, inpLoanerCenterDesc,
                    inpLoanerCenterId, State.MyBO, "LoanerCenterId")
            End If
            If Not inpMasterCenterId.Value = Guid.Empty.ToString Then
                AjaxController.PopulateBOAutoComplete(TextBoxMasterCenter, inpMasterCenterDesc,
                                inpMasterCenterId, State.MyBO, "MasterCenterId")
            End If

            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
            PopulateBOProperty(State.MyBO, "Code", TextboxCode)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "RatingCode", TextboxRatingCode)
            PopulateBOProperty(State.MyBO, "ContactName", TextboxContactName)
            PopulateBOProperty(State.MyBO, "OwnerName", TextboxOwnerName)
            PopulateBOProperty(State.MyBO, "WithholdingRate", TextBoxWithholdingRate)
            PopulateBOProperty(State.MyBO, "Phone1", TextboxPhone1)
            PopulateBOProperty(State.MyBO, "Phone2", TextboxPhone2)
            PopulateBOProperty(State.MyBO, "Fax", TextboxFax)
            PopulateBOProperty(State.MyBO, "Email", TextboxEmail)
            PopulateBOProperty(State.MyBO, "CcEmail", TextboxCcEmail)
            PopulateBOProperty(State.MyBO, "FtpAddress", TextboxFtpAddress)
            PopulateBOProperty(State.MyBO, "TaxId", TextboxTaxId)
            PopulateBOProperty(State.MyBO, "ServiceWarrantyDays", TextboxServiceWarrantyDays)
            PopulateBOProperty(State.MyBO, "IntegratedWithID", cboIntegratedWithId)
            'Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", LookupListNew.GetCodeFromId(Me.State.Statusdv, New Guid(Me.cboStatusCode.SelectedValue)))
            PopulateBOProperty(State.MyBO, "StatusCode", (From lst In State.Statusdv
                                                                Where lst.ListItemId = New Guid(cboStatusCode.SelectedValue)
                                                                Select lst.Code).FirstOrDefault())
            PopulateBOProperty(State.MyBO, "BusinessHours", TextboxBusinessHours)
            PopulateBOProperty(State.MyBO, "DefaultToEmailFlag", CheckBoxDefaultToEmail)
            PopulateBOProperty(State.MyBO, "IvaResponsibleFlag", CheckBoxIvaResponsible)
            PopulateBOProperty(State.MyBO, "FreeZoneFlag", CheckBoxFreeZone)
            PopulateBOProperty(State.MyBO, "Comments", TextboxComment)
            PopulateBOProperty(State.MyBO, "PaymentMethodId", cboPaymentMethodId)
            PopulateBOProperty(State.MyBO, "ReverseLogisticsId", cboReverseLogisticsId)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                State.MyBO.BankInfoId = State.BankInfoBO.Id
                UserBankInfoCtr.PopulateBOFromControl()
            Else
                State.MyBO.BankInfoId = Nothing
            End If

            'Set the following 2 BO Properties based on whether the Selected Items in 
            'the MASTER CENTER and LOANER CENTER Dropdown Lists are "Nothing Selected" or an actual value
            'If "Nothing Selected", then the corressponding FLAG value should be = "N", else "Y"

            If State.ForEdit = True Then
                State.MyBO.Address.AddressRequiredServCenter = True
            End If

            AddressCtr.PopulateBOFromControl()

            If (State.MyBO.IsDirty) Then
                If AjaxController.IsAutoCompleteEmpty(TextBoxMasterCenter, inpMasterCenterDesc) Then
                    'Nothing selected
                    PopulateBOProperty(State.MyBO, "MasterFlag", "N")
                Else
                    PopulateBOProperty(State.MyBO, "MasterFlag", "Y")
                End If

                If AjaxController.IsAutoCompleteEmpty(TextBoxLoanerCenter, inpLoanerCenterDesc) Then
                    'Nothing selected
                    PopulateBOProperty(State.MyBO, "LoanerFlag", "N")
                Else
                    PopulateBOProperty(State.MyBO, "LoanerFlag", "Y")
                End If

            End If

            If .MasterFlag = "Y" Then 'Master selected, set pay master flag
                PopulateBOProperty(State.MyBO, "PayMaster", chkPayMaster)
            Else
                .PayMaster = False
            End If

            PopulateBOProperty(State.MyBO, "Shipping", CheckBoxShipping)
            If State.MyBO.Shipping Then
                PopulateBOProperty(State.MyBO, "ProcessingFee", TextboxPROCESSING_FEE)
            Else
                State.MyBO.ProcessingFee = Nothing
            End If

            PopulateBOProperty(State.MyBO, "DistributionMethodId", ddlDistributionMethod)
            PopulateBOProperty(State.MyBO, "FulfillmentTimeZoneId", ddlFulFillingTimeZone)
            PopulateBOProperty(State.MyBO, "DiscountPct", TextBoxDiscountPercent)
            PopulateBOProperty(State.MyBO, "DiscountDays", TextBoxDiscountDays)
            PopulateBOProperty(State.MyBO, "NetDays", TextBoxNetDays)
            PopulateBOProperty(State.MyBO, "PreInvoiceId", ddlPreInvoice)

            State.CurrentPriceListCode = State.MyBO.PriceListCode

            If ddlPriceList.SelectedValue <> LookupListNew.GetIdFromCode(LookupListCache.LK_PRICE_LIST, State.MyBO.PriceListCode).ToString() Then
                Dim strPriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(ddlPriceList.SelectedValue))

                PopulateBOProperty(State.MyBO, "PriceListCode", strPriceListCode)

                Dim oListContext1 As New ListContext
                oListContext1.CountryId = State.MyBO.CountryId
                Dim SVC_PL_Process As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

                Dim SVCPLProcessStatusPending As String = (From lst In SVC_PL_Process
                                                           Where lst.Code = "PENDINGSUBMISSION"
                                                           Select lst.ExtendedCode).FirstOrDefault()

                Dim SVCPLProcessStatusApproved As String = (From lst In SVC_PL_Process
                                                            Where lst.Code = "APPROVED"
                                                            Select lst.ExtendedCode).FirstOrDefault()

                'If Me.State.IsCopy = False And Me.State.IsNew = False Then
                If State.MyBO.CurrentSVCPLRecon IsNot Nothing Then

                    PopulateBOProperty(State.MyBO.CurrentSVCPLRecon, "PriceListId", LookupListNew.GetIdFromCode(LookupListCache.LK_PRICE_LIST, State.MyBO.PriceListCode))

                    If Not State.priceListApprovalflag = String.Empty AndAlso State.priceListApprovalflag = Codes.EXT_YESNO_Y Then
                        PopulateBOProperty(State.MyBO.CurrentSVCPLRecon, "Status_xcd", SVCPLProcessStatusPending)
                    Else
                        PopulateBOProperty(State.MyBO.CurrentSVCPLRecon, "Status_xcd", SVCPLProcessStatusApproved)
                    End If

                    PopulateBOProperty(State.MyBO.CurrentSVCPLRecon, "RequestedBy", ElitaPlusIdentity.Current.ActiveUser.NetworkId)

                Else

                    If Not State.priceListApprovalflag = String.Empty AndAlso State.priceListApprovalflag = Codes.EXT_YESNO_Y AndAlso Not State.MyBO.IsNew Then
                        AddSVRcReconRec(GetSelectedItem(ddlPriceList), SVCPLProcessStatusPending)
                    Else
                        AddSVRcReconRec(GetSelectedItem(ddlPriceList), SVCPLProcessStatusApproved)
                    End If

                End If

                'End If


            End If

            PopulateBOProperty(State.MyBO, "AutoProcessInventoryFileXcd", ddlAutoProcessInventoryFile, False, True)

            PopulateBOProperty(State.MyBO, "ClaimReservedBasedOnXcd", ddlClaimReservedbasedon, False, True)
            PopulateBOProperty(State.MyBO, "ClaimReservedPercent", txtclaimreservedPercent)

        End With

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Protected Sub CreateNew()


        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New ServiceCenter
        State.IsNew = True

        ClearMethodOfRepairState()
        PopulateCountry()
        'Me.BindListControlToDataView(Me.ddlPriceList, LookupListNew.GetPriceListLookupList(Me.State.MyBO.CountryId), , , True)
        Dim oListContext1 As New ListContext
        oListContext1.CountryId = State.MyBO.CountryId
        Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
        ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

        ClearPriceListReconState()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        PopulateBOsFromForm()

        Dim newObj As New ServiceCenter
        newObj.Copy(State.MyBO)

        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(State.MyBO.CurrentBankInfo)
            State.BankInfoBO = newObj.CurrentBankInfo
            UserBankInfoCtr.Bind(State.BankInfoBO)
        End If

        State.MyBO = newObj
        State.MyBO.Code = Nothing
        State.MyBO.Description = Nothing
        ClearMethodOfRepairState()
        PopulateCountry()

        ClearPriceListReconState()
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New ServiceCenter
        State.ScreenSnapShotBO.Copy(State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                'check the Vendor Quantity records, if they are to be added or updated
                Dim detail As VendorQuantity
                For Each detail In State.MyBO.QuantityChildren
                    If Not detail.VendorQuantityRecordAvaliable Then
                        detail.SetRowStateAsAdded()
                    End If
                Next
                If State.MyBO.IsNew Then
                    State.MyBO.Save()
                    'State.MySvcBO.Save()
                    SaveAllRecordsMethodOfRepair(True)
                Else
                    State.MyBO.Save()
                    'State.MySvcBO.Save()
                End If
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    populateVendorManagementControls()
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    populateVendorManagementControls()
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#Region "Detail Tabs"

    Sub PopulateUserControlAvailableSelectedManufacturers()
        ' Me.UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableManufacturers()
            Dim selectedDv As DataView = State.MyBO.GetSelectedManufacturers()
            UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, True)
        End If
    End Sub
    'Sub PopulateUserControlAvailableSelectedMethodOfRepair()
    '    ' Me.UserControlAvailableSelectedMethodOfRepair.BackColor = "#d5d6e4"
    '    ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedMethodOfRepair, False)
    '    If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
    '        Dim availableDv As DataView = Me.State.MyBO.GetAvailableMethodOfRepair()
    '        Dim selectedDv As DataView = Me.State.MyBO.GetSelectedMethodOfRepair()
    '        State.MyBO.MethodOfRepairCount = selectedDv.Count
    '        Me.UserControlAvailableSelectedMethodOfRepair.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
    '        Me.UserControlAvailableSelectedMethodOfRepair.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
    '        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedMethodOfRepair, True)
    '    End If
    'End Sub

    Sub PopulateUserControlAvailableSelectedDistricts()
        ' Me.UserControlAvailableSelectedDistricts.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableDistricts()
            Dim selectedDv As DataView = State.MyBO.GetSelectedDistricts()
            UserControlAvailableSelectedDistricts.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedDistricts.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDealers()
        ' Me.UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableDealers()
            Dim selectedDv As DataView = State.MyBO.GetSelectedDealers()
            UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedServiceNetworks()
        'Me.UsercontrolavailableselectedServiceNetworks.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableServiceNetworks()
            Dim selectedDv As DataView = State.MyBO.GetSelectedServiceNetworks()
            UsercontrolavailableselectedServiceNetworks.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UsercontrolavailableselectedServiceNetworks.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, True)
        End If
    End Sub

    Sub PopulateDetailControls()

        PopulateUserControlAvailableSelectedManufacturers()
        PopulateUserControlAvailableSelectedDistricts()
        PopulateUserControlAvailableSelectedDealers()
        PopulateUserControlAvailableSelectedServiceNetworks()
        'PopulateUserControlAvailableSelectedMethodOfRepair()

        PopulateAvailableMethodOfRepair()

    End Sub

#End Region

#End Region

#Region "Detail Grid Events"


#Region "Authorized Manufacturers"

    'Private Sub DataGridDetailMfg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetailMfg.ItemDataBound
    '    Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    '    Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

    '    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
    '       e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(ServiceCenter.ServiceCenterManufacturerDataView.SERVICE_CENTER_COL_NAME).ToString
    '       e.Item.Cells(Me.GRID_COL_NAME).Text = dvRow(ServiceCenter.ServiceCenterManufacturerDataView.MANUFACTURER_COL_NAME).ToString
    '    End If
    'End Sub

    'Private Sub DataGridDetailMfg_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetailMfg.PageIndexChanged
    '    Me.DataGridDetailMfg.CurrentPageIndex = e.NewPageIndex
    '    Me.PopulateDetailMfgGrid()
    'End Sub

    'Private Sub DataGridDetailMfg_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetailMfg.SortCommand
    'Try
    '    If Me.State.SortExpressionDetailMfgGrid.StartsWith(e.SortExpression) Then
    '        If Me.State.SortExpressionDetailMfgGrid.EndsWith(" DESC") Then
    '            Me.State.SortExpressionDetailMfgGrid = e.SortExpression
    '        Else
    '            Me.State.SortExpressionDetailMfgGrid = e.SortExpression & " DESC"
    '        End If
    '    Else
    '        Me.State.SortExpressionDetailMfgGrid = e.SortExpression
    '    End If
    '    Me.PopulateDetailMfgGrid()
    'Catch ex As Exception
    '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
    'End Try
    'End Sub

#End Region

#Region "Covered Districts"

    'Private Sub DataGridDetailDst_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetailDst.ItemDataBound
    '    Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    '    Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

    '    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
    '        e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME).ToString
    '        e.Item.Cells(Me.GRID_COL_NAME).Text = dvRow(ServiceGroup.RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME).ToString
    '    End If
    'End Sub

    'Private Sub DataGridDetailDst_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetailDst.PageIndexChanged
    '    Me.DataGridDetailDst.CurrentPageIndex = e.NewPageIndex
    '    Me.PopulateDetailDstGrid()
    'End Sub

    'Private Sub DataGridDetailDst_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetailDst.SortCommand
    '    Try
    'If Me.State.SortExpressionDetailDstGrid.StartsWith(e.SortExpression) Then
    '    If Me.State.SortExpressionDetailDstGrid.EndsWith(" DESC") Then
    '        Me.State.SortExpressionDetailDstGrid = e.SortExpression
    '    Else
    '        Me.State.SortExpressionDetailDstGrid = e.SortExpression & " DESC"
    '    End If
    'Else
    '    Me.State.SortExpressionDetailDstGrid = e.SortExpression
    'End If
    '        Me.PopulateDetailDstGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region

#Region "Preferred Dealers"

#End Region

#End Region

#Region "VendorManagementTabs"

    Protected Sub PopulateChildern()
        Try

            AddCalendar_New(ibtnEffective, txtEffective, , txtEffective.Text)
            AddCalendar_New(ibtnExpiration, txtExpiration, , txtExpiration.Text)

            UpdateUserControlLookAndFeel()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub UpdateUserControlLookAndFeel()
        CType(moBankInfoController.FindControl("HiddenClassName"), HiddenField).Value = "borderLeft"
    End Sub

    Protected Sub populateVendorManagementControls()
        Try
            ' Populate Attributes
            AttributeValues.DataBind()
            PopulateContacts()
            PopulateQuantity()
            ' Me.PopulateScheduleDetail()
            PopulateSchedule()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Contacts"
    Protected Sub PopulateContacts()
        Try
            Dim dv As ServiceCenter.ContactsView
            dv = State.MyBO.GetContactsSelectionView()
            dv.Sort = State.SortExpressionContactsGrid
            moContactsGridView.DataSource = dv
            Session("recCount") = dv.Count

            moContactsGridView.Columns(GRID_CONTACTS_COL_ID).SortExpression = ServiceCenter.ContactsView.COL_NAME_ID
            moContactsGridView.Columns(GRID_CONTACTS_COL_NAME).SortExpression = ServiceCenter.ContactsView.COL_NAME_NAME
            moContactsGridView.Columns(GRID_CONTACTS_COL_JOB_TITLE).SortExpression = ServiceCenter.ContactsView.COL_NAME_JOB_TITLE
            moContactsGridView.Columns(GRID_CONTACTS_COL_COMPANY_NAME).SortExpression = ServiceCenter.ContactsView.COL_NAME_COMPANY
            moContactsGridView.Columns(GRID_CONTACTS_COL_ADDRESS_TYPE).SortExpression = ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID
            moContactsGridView.Columns(GRID_CONTACTS_COL_EFFECTIVE_DATE).SortExpression = ServiceCenter.ContactsView.COL_NAME_EFFECTIVE_DATE
            moContactsGridView.Columns(GRID_CONTACTS_COL_EXPIRATION_DATE).SortExpression = ServiceCenter.ContactsView.COL_NAME_EXPIRATION_DATE

            If dv.Count > 0 Then
                moContactsGridView.DataSource = dv
                moContactsGridView.AutoGenerateColumns = False
                moContactsGridView.DataBind()
            Else
                dv.AddNew()
                dv(0)(0) = Guid.Empty.ToByteArray
                moContactsGridView.DataSource = dv
                moContactsGridView.DataBind()
                moContactsGridView.Rows(0).Visible = False
                moContactsGridView.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moContactsGrid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moContactsGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moContactsGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moContactsGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(GRID_CONTACTS_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ID), Byte())).ToString
                e.Row.Cells(GRID_CONTACTS_COL_NAME).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_NAME).ToString
                e.Row.Cells(GRID_CONTACTS_COL_JOB_TITLE).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_JOB_TITLE).ToString
                e.Row.Cells(GRID_CONTACTS_COL_COMPANY_NAME).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_COMPANY).ToString
                e.Row.Cells(GRID_CONTACTS_COL_EMAIL).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_EMAIL).ToString
                If Not dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID).ToString = Nothing Then
                    Dim addrType As Byte() = CType(dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID), Byte())

                    If LookupListNew.GetCodeFromId(LookupListNew.GetAddressTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(addrType)) = Codes.METHOD_OF_REPAIR__PICK_UP Then
                        e.Row.Cells(GRID_CONTACTS_COL_ADDRESS_TYPE).Text = TranslationBase.TranslateLabelOrMessage(PICK_UP)
                    ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetAddressTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(addrType)) = Codes.METHOD_OF_REPAIR__SEND_IN Then
                        e.Row.Cells(GRID_CONTACTS_COL_ADDRESS_TYPE).Text = TranslationBase.TranslateLabelOrMessage(SHIPPING)
                    End If
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                    e.Row.Cells(GRID_CONTACTS_COL_EFFECTIVE_DATE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                    e.Row.Cells(GRID_CONTACTS_COL_EXPIRATION_DATE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moContactsGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(GRID_CONTACTS_COL_ID).Text)
                State.IsContactsEditing = True
                BeginContactsChildEdit()
                If State.myAddressChildBO IsNot Nothing Then
                    State.myAddressChildBO.CountryId = State.MyBO.CountryId
                    UserControlAddress.Bind(State.myAddressChildBO)
                End If
                If State.myContactInfoChildBO IsNot Nothing Then
                    Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    UserControlContactInfo.Bind(State.myContactInfoChildBO)
                End If
                State.myContactsChildBO.Effective = CType(GetDateFormattedString(CType(State.myContactsChildBO.Effective.ToString, Date)), DateTimeType)
                State.myContactsChildBO.Expiration = CType(GetDateFormattedString(CType(State.myContactsChildBO.Expiration.ToString, Date)), DateTimeType)
                txtEffective.Text = ElitaPlusPage.GetDateFormattedString(CDate(State.myContactsChildBO.Effective))
                txtExpiration.Text = ElitaPlusPage.GetDateFormattedString(CDate(State.myContactsChildBO.Expiration))
                btnNewItemSave.Text = TranslationBase.TranslateLabelOrMessage(UPDATE)
                PopulateContacts()
                EnableDisableFields()
                mdlPopup.Show()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(GRID_CONTACTS_COL_ID).Text)
                State.IsContactsEditing = True
                BeginContactsChildEdit()
                If State.myAddressChildBO IsNot Nothing Then
                    State.myAddressChildBO.CountryId = State.MyBO.CountryId
                    UserControlAddress.Bind(State.myAddressChildBO)
                End If
                If State.myContactInfoChildBO IsNot Nothing Then
                    Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    UserControlContactInfo.Bind(State.myContactInfoChildBO)
                End If
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = SAVE_COMMAND_NAME_CONTCT Then
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = CANCEL_RECORD_NAME_CONTCT Then
                nIndex = CInt(e.CommandArgument)
                State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(GRID_CONTACTS_COL_ID).Text)
                State.IsContactsEditing = False
                BeginContactsChildEdit()
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub populateContactsChildBO()
        With State
            If Not CType(mdlPopup.FindControl("txtEffective"), TextBox).Text.Equals(String.Empty) Then
                .myContactsChildBO.Effective = DateHelper.GetDateValue(CType(mdlPopup.FindControl("txtEffective"), TextBox).Text)
            End If
            If Not CType(mdlPopup.FindControl("txtExpiration"), TextBox).Text.Equals(String.Empty) Then
                .myContactsChildBO.Expiration = DateHelper.GetDateValue(CType(mdlPopup.FindControl("txtExpiration"), TextBox).Text)
            End If
        End With
        UserControlContactInfo.PopulateBOFromControl(True)
        State.myContactsChildBO.Name = State.myContactInfoChildBO.Name
        State.myContactsChildBO.JobTitle = State.myContactInfoChildBO.JobTitle
        State.myContactsChildBO.Company = State.myContactInfoChildBO.Company
        State.myContactsChildBO.Email = State.myContactInfoChildBO.Email
        State.myContactsChildBO.AddressTypeId = State.myContactInfoChildBO.AddressTypeId
    End Sub

    Sub BeginContactsChildEdit()
        State.IsContactsEditing = True
        '#1
        With State
            If Not .ContactsSelectedChildId.Equals(Guid.Empty) Then
                .myContactsChildBO = .MyBO.GeChildContacts(.ContactsSelectedChildId)
            Else
                .myContactsChildBO = .MyBO.GetNewChildContacts
            End If
            .myContactsChildBO.BeginEdit()
        End With
        '#2
        With State
            If Not .myContactsChildBO.ContactInfoId.Equals(Guid.Empty) Then
                .myContactInfoChildBO = .MyBO.GeChildContactInfo(.myContactsChildBO, .myContactsChildBO.ContactInfoId)
            Else
                .myContactInfoChildBO = .MyBO.GetNewChildContactInfo(.myContactsChildBO)
            End If
            .myContactInfoChildBO.BeginEdit()
        End With
        '#3
        With State
            If Not .ContactsSelectedChildId.Equals(Guid.Empty) Then
                .myAddressChildBO = .MyBO.GeChildAddress(.myContactInfoChildBO, .myContactInfoChildBO.AddressId)
            Else
                .myAddressChildBO = .MyBO.GetNewChildAddress(.myContactInfoChildBO)
            End If
            .myAddressChildBO.BeginEdit()
        End With
    End Sub

    Sub EndContactsChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .myContactsChildBO.EndEdit()
                        .myContactsChildBO.Save()
                        .myContactInfoChildBO.EndEdit()
                        .myContactInfoChildBO.Save()
                        .myAddressChildBO.EndEdit()
                        .myAddressChildBO.Save()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .myContactsChildBO.cancelEdit()
                        If .myContactsChildBO.IsSaveNew Then
                            .myContactsChildBO.Delete()
                            .myContactsChildBO.Save()
                        End If
                        .myContactInfoChildBO.cancelEdit()
                        If .myContactInfoChildBO.IsSaveNew Then
                            .myContactInfoChildBO.Delete()
                            .myContactInfoChildBO.Save()
                        End If
                        .myAddressChildBO.cancelEdit()
                        If .myAddressChildBO.IsSaveNew Then
                            .myAddressChildBO.Delete()
                            .myContactInfoChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .myContactsChildBO.cancelEdit()
                        If .myContactsChildBO.IsSaveNew Then
                            .myContactsChildBO.Delete()
                            .myContactsChildBO.Save()
                        End If
                        .myContactInfoChildBO.cancelEdit()
                        If .myContactInfoChildBO.IsSaveNew Then
                            .myContactInfoChildBO.Delete()
                            .myContactInfoChildBO.Save()
                        End If
                        .myAddressChildBO.cancelEdit()
                        If .myAddressChildBO.IsSaveNew Then
                            .myAddressChildBO.Delete()
                            .myAddressChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .myAddressChildBO.Delete()
                        .myAddressChildBO.EndEdit()
                        .myContactInfoChildBO.Delete()
                        .myContactInfoChildBO.EndEdit()
                        .myContactsChildBO.Delete()
                        .myContactsChildBO.EndEdit()
                        .ContactsSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsContactsEditing = False
            EnableDisableFields()
            populateVendorManagementControls()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            mdlPopup.Show()
        End Try
    End Sub

    Private Sub btnNewItemCancel_Click(sender As Object, e As System.EventArgs) Handles btnNewItemCancel.Click
        Try
            mdlPopup.Hide()
            EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            State.ActionInProgress = DetailPageCommand.Accept
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewItemSave_Click(sender As Object, e As System.EventArgs) Handles btnNewItemSave.Click
        Try
            populateContactsChildBO()
            EndContactsChildEdit(DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewContactInfo_Click(sender As Object, e As System.EventArgs) Handles btnNewContactInfo.Click
        Try
            State.ContactsSelectedChildId = Guid.Empty
            State.IsContactsEditing = True
            BeginContactsChildEdit()
            If State.myAddressChildBO IsNot Nothing Then
                State.myAddressChildBO.CountryId = State.MyBO.CountryId
                UserControlAddress.Bind(State.myAddressChildBO)
            End If
            If State.myContactInfoChildBO IsNot Nothing Then
                Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                UserControlContactInfo.Bind(State.myContactInfoChildBO)
            End If
            txtEffective.Text = String.Empty
            txtExpiration.Text = String.Empty
            btnNewItemSave.Text = TranslationBase.TranslateLabelOrMessage("SAVE")
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Quantity"

    Protected Sub PopulateQuantity()

        Try
            Dim dv As ServiceCenter.QuantityView
            dv = State.MyBO.GetQuantitySelectionView()
            dv.Sort = State.SortExpressionQuantityGrid
            moQuantityGridView.DataSource = dv
            Session("recCount") = dv.Count

            moQuantityGridView.Columns(GRID_QUANTITY_COL_ID).SortExpression = ServiceCenter.QuantityView.COL_NAME_ID
            moQuantityGridView.Columns(GRID_QUANTITY_COL_EQUIPMENT_TYPE).SortExpression = ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID
            moQuantityGridView.Columns(GRID_QUANTITY_COL_MANUFACTURER).SortExpression = ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_ID
            moQuantityGridView.Columns(GRID_QUANTITY_COL_MODEL).SortExpression = ServiceCenter.QuantityView.COL_NAME_JOB_MODEL
            moQuantityGridView.Columns(GRID_QUANTITY_COL_SKU).SortExpression = ServiceCenter.QuantityView.COL_NAME_VENDOR_SKU
            moQuantityGridView.Columns(GRID_QUANTITY_COL_SKU_DESC).SortExpression = ServiceCenter.QuantityView.COL_NAME_VENDOR_SKU_DESCRIPTION
            moQuantityGridView.Columns(GRID_QUANTITY_COL_QUANTITY).SortExpression = ServiceCenter.QuantityView.COL_NAME_QUANTITY
            moQuantityGridView.Columns(GRID_QUANTITY_COL_PRICE).SortExpression = ServiceCenter.QuantityView.COL_NAME_PRICE
            moQuantityGridView.Columns(GRID_QUANTITY_COL_CONDITION).SortExpression = ServiceCenter.QuantityView.COL_NAME_CONDITION_ID
            moQuantityGridView.Columns(GRID_QUANTITY_COL_EFFECTIVE).SortExpression = ServiceCenter.QuantityView.COL_NAME_EFFECTIVE
            moQuantityGridView.Columns(GRID_QUANTITY_COL_EXPIRATION).SortExpression = ServiceCenter.QuantityView.COL_NAME_EXPIRATION

            If State.IsQuantityEditing Then
                SetPageAndSelectedIndexFromGuid(dv, State.QuantitySelectedChildId, moQuantityGridView,
                                        moQuantityGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, State.QuantitySelectedChildId, moQuantityGridView, State.QuantityDetailPageIndex)
                State.QuantityDetailPageIndex = moQuantityGridView.PageIndex
            End If


            If dv.Count > 0 Then
                moQuantityGridView.DataSource = dv
                moQuantityGridView.AutoGenerateColumns = False
                moQuantityGridView.DataBind()
            Else
                dv.AddNew()
                dv(0)(0) = Guid.Empty.ToByteArray
                moQuantityGridView.DataSource = dv
                moQuantityGridView.DataBind()
                moQuantityGridView.Rows(0).Visible = False
                moQuantityGridView.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moQuantityGrid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moQuantityGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moQuantityGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moQuantityGridView.RowDataBound
        Dim EquipmentTypeId As Guid
        Dim ManufacturerId As Guid
        Dim ConditionId As Guid

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(GRID_QUANTITY_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_ID), Byte())).ToString
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID)) Then
                    EquipmentTypeId = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID), Byte()))
                    e.Row.Cells(GRID_QUANTITY_COL_EQUIPMENT_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), EquipmentTypeId)
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_NAME)) Then
                    e.Row.Cells(GRID_QUANTITY_COL_MANUFACTURER).Text = dvRow.Row(ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_NAME).ToString()
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_CONDITION_ID)) Then
                    ConditionId = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_CONDITION_ID), Byte()))
                    e.Row.Cells(GRID_QUANTITY_COL_CONDITION).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetConditionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), ManufacturerId)
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE)) Then
                    e.Row.Cells(GRID_QUANTITY_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION)) Then
                    e.Row.Cells(GRID_QUANTITY_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
                'check if any row of the grid is in Editable mode, if yes, then disbale the edit icon from other rows
                If (State.IsQuantityEditing) Then
                    SetQuantityGridRowControls(e.Row, False, False, False)
                Else
                    SetQuantityGridRowControls(e.Row, True, False, False)
                End If
                If ((e.Row.RowState And DataControlRowState.Edit) > 0) Then
                    SetQuantityGridRowControls(e.Row, False, True, True)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub SetQuantityGridRowControls(gridrow As GridViewRow, enableEdit As Boolean, enableCancel As Boolean, enableSave As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim cancel As LinkButton
        Dim save As Button
        Const EDIT_COL As Integer = 11

        edt = CType(gridrow.Cells(EDIT_COL).FindControl("QuantityEditButton_WRITE"), ImageButton)
        If edt IsNot Nothing Then
            edt.Enabled = enableEdit
            edt.Visible = enableEdit
        End If
        cancel = CType(gridrow.Cells(EDIT_COL).FindControl("QuantityCancelButton_WRITE"), LinkButton)
        If edt IsNot Nothing Then
            cancel.Enabled = enableCancel
            cancel.Visible = enableCancel
        End If
        save = CType(gridrow.Cells(EDIT_COL).FindControl("QuantitySaveButton_WRITE"), Button)
        If save IsNot Nothing Then
            save.Enabled = enableSave
            save.Visible = enableSave
        End If
    End Sub

    Protected Sub moQuantityGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.QuantitySelectedChildId = New Guid(moQuantityGridView.Rows(nIndex).Cells(GRID_QUANTITY_COL_ID).Text)
                State.IsQuantityEditing = True
                BeginQuantityChildEdit()
                PopulateQuantity()
                PopulateFromQuantityChildBO()
                ElitaPlusSearchPage.SetGridControls(moQuantityGridView, False)
                EnableDisableFields()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.QuantitySelectedChildId = New Guid(moQuantityGridView.Rows(nIndex).Cells(GRID_QUANTITY_COL_ID).Text)
                State.IsQuantityEditing = True
                BeginQuantityChildEdit()
                EndQuantityChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                'PopulateQuantity()
            ElseIf e.CommandName = SAVE_COMMAND_NAME_QNTY Then
                populateQuantityChildBO()
                EndQuantityChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                ' PopulateQuantity()
            ElseIf e.CommandName = CANCEL_RECORD_NAME_QNTY Then
                nIndex = CInt(e.CommandArgument)
                'Me.State.QuantitySelectedChildId = New Guid(moQuantityGridView.Rows(nIndex).Cells(Me.GRID_QUANTITY_COL_ID).Text)
                'Me.State.IsQuantityEditing = False
                'BeginQuantityChildEdit()
                EndQuantityChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
                'PopulateQuantity()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateFromQuantityChildBO()
        If moQuantityGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moQuantityGridView.Rows(moQuantityGridView.EditIndex)
        Dim txtCommon As TextBox

        With State.myQuantityChildBO
            txtCommon = CType(gRow.Cells(GRID_QUANTITY_COL_QUANTITY).FindControl(GRID_CONTROL_QUANTITY), TextBox)
            If txtCommon IsNot Nothing Then
                PopulateControlFromBOProperty(txtCommon, .Quantity)
            End If
        End With
    End Sub

    Public Sub populateQuantityChildBO()
        If moQuantityGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moQuantityGridView.Rows(moQuantityGridView.EditIndex)
        Dim txtCommon As TextBox

        With State.myQuantityChildBO
            txtCommon = CType(gRow.Cells(GRID_QUANTITY_COL_QUANTITY).FindControl(GRID_CONTROL_QUANTITY), TextBox)
            If txtCommon IsNot Nothing Then
                .Quantity = CType(txtCommon.Text, LongType)
            End If
        End With
    End Sub

    Sub BeginQuantityChildEdit()
        State.IsQuantityEditing = True
        With State
            If Not .QuantitySelectedChildId.Equals(Guid.Empty) Then
                .myQuantityChildBO = .MyBO.GeChildQuantity(.QuantitySelectedChildId)
            Else
                .myQuantityChildBO = .MyBO.GetNewChildQuantity
            End If
            .myQuantityChildBO.BeginEdit()
        End With
        populateQuantityChildBO()
    End Sub

    Sub EndQuantityChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .myQuantityChildBO.Save()
                        .myQuantityChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .myQuantityChildBO.cancelEdit()
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .myQuantityChildBO.cancelEdit()
                        .myQuantityChildBO.Save()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .myQuantityChildBO.Delete()
                        .myQuantityChildBO.Save()
                        .myQuantityChildBO.EndEdit()
                        .QuantitySelectedChildId = Guid.Empty
                End Select
            End With
            State.IsQuantityEditing = False
            EnableDisableFields()
            populateVendorManagementControls()
            'Me.PopulateQuantity()
            'Me.PopulateSchedule()
            'Me.PopulateContacts()
            'Me.populateAttributes()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Schedule"

    Protected Sub PopulateScheduleDetail()
        Try
            Dim dv As ServiceCenter.ScheduleView
            dv = State.MyBO.GetScheduleSelectionView()
            dv.Sort = State.SortExpressionScheduleGrid
            moScheduleGridView.DataSource = dv
            Session("recCount") = dv.Count

            moScheduleGridView.Columns(GRID_SCHEDULE_COL_ID).SortExpression = ServiceCenter.ScheduleView.COL_NAME_ID
            moScheduleGridView.Columns(GRID_SCHEDULE_COL_SERVICE_CLASS).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID
            moScheduleGridView.Columns(GRID_SCHEDULE_COL_SERVICE_TYPE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID
            moScheduleGridView.Columns(GRID_SCHEDULE_COL_SCHEDULE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID
            moScheduleGridView.Columns(GRID_SCHEDULE_COL_EFFECTIVE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EFFECTIVE
            moScheduleGridView.Columns(GRID_SCHEDULE_COL_EXPIRATION).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EXPIRATION

            If State.IsScheduleEditing Then
                SetPageAndSelectedIndexFromGuid(dv, State.ScheduleSelectedChildId, moScheduleGridView,
                                        moScheduleGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, State.ScheduleSelectedChildId, moScheduleGridView, State.ScheduleDetailPageIndex)
                State.ScheduleDetailPageIndex = moScheduleGridView.PageIndex
            End If

            If dv.Count > 0 Then
                moScheduleGridView.DataSource = dv
                moScheduleGridView.AutoGenerateColumns = False
                moScheduleGridView.DataBind()
            Else
                dv.AddNew()
                dv(0)(0) = Guid.Empty.ToByteArray
                moScheduleGridView.DataSource = dv
                moScheduleGridView.DataBind()
                moScheduleGridView.Rows(0).Visible = False
                moScheduleGridView.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub PopulateSchedule()
        Try
            Dim dv As ServiceCenter.ScheduleView
            dv = State.MyBO.GetScheduleSelectionView()
            dv.Sort = State.SortExpressionScheduleGrid
            moAddScheduleGridView.DataSource = dv
            Session("recCount") = dv.Count

            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_ID).SortExpression = ServiceCenter.ScheduleView.COL_NAME_ID
            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_SERVICE_CLASS).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID
            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_SERVICE_TYPE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID
            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_SCHEDULE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID
            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_EFFECTIVE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EFFECTIVE
            moAddScheduleGridView.Columns(GRID_SCHEDULE_COL_EXPIRATION).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EXPIRATION

            If State.IsScheduleEditing Then
                SetPageAndSelectedIndexFromGuid(dv, State.ScheduleSelectedChildId, moAddScheduleGridView,
                                        moAddScheduleGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, State.ScheduleSelectedChildId, moAddScheduleGridView, State.ScheduleDetailPageIndex)
                State.ScheduleDetailPageIndex = moAddScheduleGridView.PageIndex
            End If

            If dv.Count > 0 Then
                moAddScheduleGridView.DataSource = dv
                moAddScheduleGridView.AutoGenerateColumns = False
                moAddScheduleGridView.DataBind()
            Else
                dv.AddNew()
                dv(0)(0) = Guid.Empty.ToByteArray
                moAddScheduleGridView.DataSource = dv
                moAddScheduleGridView.DataBind()
                moAddScheduleGridView.Rows(0).Visible = False
                moAddScheduleGridView.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moScheduleGrid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moScheduleGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_PageIndexChanged1(sender As Object, e As System.EventArgs) Handles moAddScheduleGridView.PageIndexChanged
        Try
            State.PageIndex = moAddScheduleGridView.PageIndex
            State.ScheduleSelectedChildId = Guid.Empty
            PopulateSchedule()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moAddScheduleGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moScheduleGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moScheduleGridView.RowDataBound
        Dim serviceClassId As Guid
        Dim serviceTypeId As Guid
        Dim scheduleId As Guid
        Dim ddlServiceType As DropDownList
        Dim ddlServiceClass As DropDownList
        Dim ddlSchedule As DropDownList
        Dim ddlDayofWeek As DropDownList

        ddlServiceClass = CType(e.Row.FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
        ddlServiceType = CType(e.Row.FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
        ddlDayofWeek = CType(e.Row.FindControl(GRID_CONTROL_DAY_OF_WEEK), DropDownList)
        ddlSchedule = CType(e.Row.FindControl(GRID_CONTROL_SCHEDULE), DropDownList)

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID).ToString = Nothing Then
                    e.Row.Cells(GRID_SCHEDULE_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_ID), Byte())).ToString
                    serviceClassId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID).ToString = Nothing Then
                    e.Row.Cells(GRID_SCHEDULE_COL_SERVICE_CLASS).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceClassId)
                    serviceTypeId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID).ToString = Nothing Then
                    e.Row.Cells(GRID_SCHEDULE_COL_SERVICE_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceTypeId)
                    scheduleId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                    e.Row.Cells(GRID_SCHEDULE_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                    e.Row.Cells(GRID_SCHEDULE_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub moScheduleGridView_PageIndexChanged(sender As Object, e As System.EventArgs) Handles moScheduleGridView.PageIndexChanged
        Try
            State.PageIndex = moScheduleGridView.PageIndex
            State.ScheduleSelectedChildId = Guid.Empty
            PopulateScheduleDetail()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moScheduleGridView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moScheduleGridView.PageIndexChanging
        Try
            moScheduleGridView.PageIndex = e.NewPageIndex
            State.ScheduleDetailPageIndex = moScheduleGridView.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moAddScheduleGridView.PageIndexChanging
        Try
            moAddScheduleGridView.PageIndex = e.NewPageIndex
            State.ScheduleDetailPageIndex = moAddScheduleGridView.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moAddScheduleGridView.RowDataBound
        Dim serviceClassId As Guid
        Dim serviceTypeId As Guid
        Dim scheduleId As Guid
        Dim ddlServiceType As DropDownList
        Dim ddlServiceClass As DropDownList
        Dim ddlSchedule As DropDownList
        Dim ddlDayofWeek As DropDownList

        ddlServiceClass = CType(e.Row.FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
        ddlServiceType = CType(e.Row.FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
        ddlDayofWeek = CType(e.Row.FindControl(GRID_CONTROL_DAY_OF_WEEK), DropDownList)
        ddlSchedule = CType(e.Row.FindControl(GRID_CONTROL_SCHEDULE), DropDownList)

        Dim ServiceClassList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="SVCCLASS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Dim ServiceClassTypeList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="SVCTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Dim ScheduleList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="GetSchedule", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If ((e.Row.RowState And DataControlRowState.Edit) > 0) Then
                    If ddlServiceClass IsNot Nothing Then
                        'Me.BindListControlToDataView(ddlServiceClass, LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlServiceClass.Populate(ServiceClassList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If ddlServiceType IsNot Nothing Then
                        'Me.BindListControlToDataView(ddlServiceType, LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlServiceType.Populate(ServiceClassTypeList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If ddlDayofWeek IsNot Nothing Then
                        'Me.BindListControlToDataView(ddlDayofWeek, LookupListNew.GetDayOfWeekLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlDayofWeek.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="DAYS_OF_WEEK", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If ddlSchedule IsNot Nothing Then
                        'Me.BindListControlToDataView(ddlSchedule, LookupListNew.GetScheduleList(), , , False)
                        ddlSchedule.Populate(ScheduleList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    SetScheduleGridRowControls(e.Row, False, True, True)
                Else
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID).ToString = Nothing Then
                        e.Row.Cells(GRID_SCHEDULE_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_ID), Byte())).ToString
                        serviceClassId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID), Byte()))
                    End If
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID).ToString = Nothing Then
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceClassId)
                        e.Row.Cells(GRID_SCHEDULE_COL_SERVICE_CLASS).Text = (From lst In ServiceClassList
                                                                                Where lst.ListItemId = serviceClassId
                                                                                Select lst.Translation).FirstOrDefault()
                        serviceTypeId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID), Byte()))
                    End If
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID).ToString = Nothing Then
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceTypeId)
                        e.Row.Cells(GRID_SCHEDULE_COL_SERVICE_TYPE).Text = (From lst In ServiceClassTypeList
                                                                               Where lst.ListItemId = serviceTypeId
                                                                               Select lst.Translation).FirstOrDefault()
                        scheduleId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID), Byte()))
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetScheduleList(), scheduleId)
                        e.Row.Cells(GRID_SCHEDULE_COL_SCHEDULE).Text = (From lst In ScheduleList
                                                                           Where lst.ListItemId = scheduleId
                                                                           Select lst.Translation).FirstOrDefault()
                    End If
                    If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                        e.Row.Cells(GRID_SCHEDULE_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                    End If
                    If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                        e.Row.Cells(GRID_SCHEDULE_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                    End If
                    If (State.IsScheduleEditing) Then ' If Grid is in edit mode, hide buttons if this row's rowstate is not edit. As for edited row it is setting info. In above part  
                        SetScheduleGridRowControls(e.Row, False, False, False)
                    Else
                        SetScheduleGridRowControls(e.Row, True, False, False)
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub SetScheduleGridRowControls(gridrow As GridViewRow, enableEdit As Boolean, enableCancel As Boolean, enableSave As Boolean)
        Dim edt As ImageButton
        Dim cancel As LinkButton
        Dim save As Button
        Dim del As ImageButton
        Const EDIT_COL As Integer = 9

        edt = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleEditButton_WRITE"), ImageButton)
        If edt IsNot Nothing Then
            edt.Enabled = enableEdit
            edt.Visible = enableEdit
        End If
        cancel = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleCancelButton_WRITE"), LinkButton)
        If edt IsNot Nothing Then
            cancel.Enabled = enableCancel
            cancel.Visible = enableCancel
        End If
        save = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleSaveButton_WRITE"), Button)
        If save IsNot Nothing Then
            save.Enabled = enableSave
            save.Visible = enableSave
        End If
        del = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleDeleteButton_WRITE"), ImageButton)
        If del IsNot Nothing Then
            del.Enabled = enableEdit
            del.Visible = enableEdit
        End If

    End Sub

    Protected Sub moAddScheduleGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                moAddScheduleGridView.EditIndex = nIndex
                State.ScheduleSelectedChildId = New Guid(moAddScheduleGridView.Rows(nIndex).Cells(GRID_SCHEDULE_COL_ID).Text)
                State.IsScheduleEditing = True
                BeginScheduleChildEdit()
                PopulateSchedule()
                PopulateFromScheduleChildBO()
                EnableDisableFields()
                EnableDisableTabs(False)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                moAddScheduleGridView.EditIndex = nIndex
                State.ScheduleSelectedChildId = New Guid(moAddScheduleGridView.Rows(nIndex).Cells(GRID_SCHEDULE_COL_ID).Text)
                State.IsScheduleEditing = True
                BeginScheduleChildEdit()
                PopulateFromScheduleChildBO()
                populateScheduleChildBO()
                EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = SAVE_COMMAND_NAME_SCHDL Then
                populateScheduleChildBO()
                EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = CANCEL_RECORD_NAME_SCHDL Then
                moAddScheduleGridView.EditIndex = -1
                EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Function IsDateValid() As Boolean
        Dim gRow As GridViewRow = moScheduleGridView.Rows(moScheduleGridView.EditIndex)
        Dim strFromTime As String
        Dim strToTime As String
        Dim strEffective As String
        Dim strExpiration As String

        Try
            strFromTime = CType(gRow.Cells(GRID_SCHEDULE_COL_FROM).FindControl(GRID_CONTROL_FROM), TextBox).Text
            strToTime = CType(gRow.Cells(GRID_SCHEDULE_COL_DAY).FindControl(GRID_CONTROL_TO), TextBox).Text
            strEffective = GetDateFormattedString(CType(CType(gRow.Cells(GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox).Text.ToString, Date))
            strExpiration = GetDateFormattedString(CType(CType(gRow.Cells(GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox).Text.ToString, Date))

            If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX,
                                                            System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                MasterPage.MessageController.Clear()
                MasterPage.MessageController.AddWarning(String.Format("{0}: {1}",
                                       TranslationBase.TranslateLabelOrMessage("FROM_TIME"),
                                       TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                Exit Function
            End If
            If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX,
                                                            System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                MasterPage.MessageController.Clear()
                MasterPage.MessageController.AddWarning(String.Format("{0}: {1}",
                                       TranslationBase.TranslateLabelOrMessage("TO_TIME"),
                                       TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                Exit Function
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        Return True
    End Function

    Sub PopulateFromScheduleChildBO()
        If moAddScheduleGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moAddScheduleGridView.Rows(moAddScheduleGridView.EditIndex)
        Dim ddlServiceType As DropDownList
        Dim ddlServiceClass As DropDownList
        Dim ddlSchedule As DropDownList
        Dim txtEffective As TextBox
        Dim txtExpiration As TextBox
        Dim btn As ImageButton
        Dim btn1 As ImageButton

        With State.myScheduleChildBO
            ddlSchedule = CType(gRow.Cells(GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList)
            If ddlSchedule IsNot Nothing Then
                BindSelectItem(.ScheduleId.ToString, ddlSchedule)
            End If
            ddlServiceType = CType(gRow.Cells(GRID_SCHEDULE_COL_SERVICE_TYPE).FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
            If ddlServiceType IsNot Nothing Then
                BindSelectItem(.ServiceTypeId.ToString, ddlServiceType)
            End If
            ddlServiceClass = CType(gRow.Cells(GRID_SCHEDULE_COL_SERVICE_CLASS).FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
            If ddlServiceClass IsNot Nothing Then
                BindSelectItem(.ServiceClassId.ToString, ddlServiceClass)
            End If
            txtEffective = CType(gRow.Cells(GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
            If txtEffective IsNot Nothing AndAlso .Effective IsNot Nothing Then
                txtEffective.Text = GetDateFormattedString(CType(.Effective.ToString, Date))
            End If
            txtExpiration = CType(gRow.Cells(GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
            If txtExpiration IsNot Nothing AndAlso .Expiration IsNot Nothing Then
                txtExpiration.Text = GetDateFormattedString(CType(.Expiration.ToString, Date))
            End If

            btn = DirectCast(gRow.Cells(GRID_SCHEDULE_COL_EFFECTIVE).FindControl("btnEffectiveDate"), ImageButton)
            btn1 = DirectCast(gRow.Cells(GRID_SCHEDULE_COL_EXPIRATION).FindControl("btnExpirationDate"), ImageButton)

            Dim txtcommon As TextBox
            txtcommon = CType(gRow.Cells(GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
            If txtcommon IsNot Nothing Then
                AddCalendar_New(btn, txtcommon, , txtcommon.Text)
            End If

            txtcommon = CType(gRow.Cells(GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
            If txtcommon IsNot Nothing Then
                AddCalendar_New(btn1, txtcommon, , txtcommon.Text)
            End If
        End With

    End Sub

    Public Function GetScheduleDetailCount(scheduleId As Guid) As Integer
        Return State.MyBO.GetScheduleDetailCount(scheduleId)
    End Function

    Public Function GetScheduleDetail(scheduleId As Guid) As DataSet
        Return State.MyBO.GetScheduleDetailInfo(scheduleId)
    End Function

    Protected Function GetScheduleInfo(scheduleId As Guid) As DataSet
        Return State.MyBO.GetScheduleInfo(scheduleId)
    End Function

    Public Sub populateScheduleChildBO()
        If moAddScheduleGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moAddScheduleGridView.Rows(moAddScheduleGridView.EditIndex)
        Dim ddlCommon As DropDownList
        Dim txtCommon As TextBox
        Dim schId As Guid
        Dim cnt As Integer
        Dim dsSchedule As DataSet
        Dim dsScheduleDetail As DataSet

        If CType(gRow.Cells(GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList) IsNot Nothing Then
            schId = New Guid(CType(gRow.Cells(GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList).SelectedValue)
            cnt = GetScheduleDetailCount(schId)
            dsSchedule = GetScheduleInfo(schId)
            dsScheduleDetail = GetScheduleDetail(schId)
        End If

        For i As Integer = 0 To cnt - 1
            With State.myScheduleChildBO
                ddlCommon = CType(gRow.Cells(GRID_SCHEDULE_COL_SERVICE_CLASS).FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
                If ddlCommon IsNot Nothing Then
                    PopulateBOProperty(State.myScheduleChildBO, "ServiceClassId", ddlCommon)
                End If
                ddlCommon = CType(gRow.Cells(GRID_SCHEDULE_COL_SERVICE_TYPE).FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
                If ddlCommon IsNot Nothing Then
                    PopulateBOProperty(State.myScheduleChildBO, "ServiceTypeId", ddlCommon)
                End If
                ddlCommon = CType(gRow.Cells(GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList)
                If ddlCommon IsNot Nothing Then
                    PopulateBOProperty(State.myScheduleChildBO, "ScheduleId", ddlCommon)
                End If
                txtCommon = CType(gRow.Cells(GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.myScheduleChildBO, "Effective", txtCommon)
                End If
                txtCommon = CType(gRow.Cells(GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
                If txtCommon IsNot Nothing Then
                    PopulateBOProperty(State.myScheduleChildBO, "Expiration", txtCommon)
                End If
            End With
            If dsSchedule IsNot Nothing Then
                With State.myScheduleTableChildBO
                    .Code = Convert.ToString(dsSchedule.Tables(0).Rows(0)("code"))
                    .Description = Convert.ToString(dsSchedule.Tables(0).Rows(0)("description"))
                End With
            End If
            If cnt > 0 Then
                With State.myScheduleDetailChildBO
                    .ScheduleId = State.myScheduleChildBO.ScheduleId
                    .DayOfWeekId = New Guid(CType(dsScheduleDetail.Tables(0).Rows(i)("day_of_week_id"), Byte()))
                    .FromTime = CType(dsScheduleDetail.Tables(0).Rows(i)("from_time"), DateTime)
                    .ToTime = CType(dsScheduleDetail.Tables(0).Rows(i)("to_time"), DateTime)
                End With
            Else
                With State.myScheduleDetailChildBO
                    .ScheduleId = State.myScheduleChildBO.ScheduleId
                    .DayOfWeekId = Guid.Empty
                    .FromTime = CType("00:00 AM", DateTime)
                    .ToTime = CType("00:00 AM", DateTime)
                End With
            End If
        Next i
    End Sub

    Sub BeginScheduleChildEdit()
        State.IsScheduleEditing = True
        With State
            If Not .ScheduleSelectedChildId.Equals(Guid.Empty) Then
                .myScheduleChildBO = .MyBO.GeChildSchedule(.ScheduleSelectedChildId)
            Else
                .myScheduleChildBO = .MyBO.GetNewChildSchedule
            End If
            .myScheduleChildBO.BeginEdit()
        End With
        With State
            If Not .myScheduleChildBO.ScheduleId.Equals(Guid.Empty) Then
                .myScheduleTableChildBO = .MyBO.GeChildScheduleTable(.myScheduleChildBO, .myScheduleChildBO.ScheduleId)
            Else
                .myScheduleTableChildBO = .MyBO.GetNewChildScheduleTable(.myScheduleChildBO)
            End If
            .myScheduleTableChildBO.BeginEdit()
        End With
        With State
            If Not String.IsNullOrEmpty(.myScheduleTableChildBO.Code) Then
                .myScheduleDetailChildBO = .MyBO.GeChildScheduleDetail(.myScheduleTableChildBO, .myScheduleTableChildBO.Id, .myScheduleChildBO)
            Else
                .myScheduleDetailChildBO = .MyBO.GetNewChildScheduleDetail(.myScheduleTableChildBO, .myScheduleChildBO)
            End If
            .myScheduleDetailChildBO.BeginEdit()
        End With

        populateScheduleChildBO()
    End Sub

    Sub EndScheduleChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .myScheduleChildBO.Save()
                        .myScheduleChildBO.EndEdit()
                        .myScheduleTableChildBO.EndEdit()
                        .myScheduleDetailChildBO.EndEdit()
                        EnableDisableTabs(True) ' Enable tabs 
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .myScheduleChildBO.cancelEdit()
                        If .myScheduleChildBO.IsSaveNew Then
                            .myScheduleChildBO.Delete()
                            .myScheduleChildBO.Save()
                        End If
                        .myScheduleTableChildBO.cancelEdit()
                        If .myScheduleTableChildBO.IsSaveNew Then
                            .myScheduleTableChildBO.Delete()
                            .myScheduleTableChildBO.Save()
                        End If
                        .myScheduleDetailChildBO.cancelEdit()
                        If .myScheduleDetailChildBO.IsSaveNew Then
                            .myScheduleDetailChildBO.Delete()
                            .myScheduleDetailChildBO.Save()
                        End If
                        EnableDisableTabs(True) ' Enable tabs once cancel in the grid is clicked
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .myScheduleChildBO.cancelEdit()
                        If .myScheduleChildBO.IsSaveNew Then
                            .myScheduleChildBO.Delete()
                            .myScheduleChildBO.Save()
                        End If
                        .myScheduleTableChildBO.cancelEdit()
                        If .myScheduleTableChildBO.IsSaveNew Then
                            .myScheduleTableChildBO.Delete()
                            .myScheduleTableChildBO.Save()
                        End If
                        .myScheduleDetailChildBO.cancelEdit()
                        If .myScheduleDetailChildBO.IsSaveNew Then
                            .myScheduleDetailChildBO.Delete()
                            .myScheduleDetailChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .myScheduleChildBO.DeleteServiceSchedule()
                        .ScheduleSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsScheduleEditing = False
            EnableDisableFields()
        Catch ex As Exception
            Throw
        Finally
            populateVendorManagementControls()
        End Try
    End Sub

    Private Sub btnNewSchedule_Click(sender As Object, e As System.EventArgs) Handles btnNewSchedule.Click
        Try
            divScheduleDetails.Visible = True
            divAddNewSchedule.Visible = False
            PopulateSchedule()
            btnNewSchedueInfo_Click(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewSchedueInfo_Click(sender As Object, e As System.EventArgs) Handles btnAddNewSchedule.Click
        Try
            State.ScheduleSelectedChildId = Guid.Empty
            State.IsScheduleEditing = True
            BeginScheduleChildEdit()
            State.ScheduleSelectedChildId = State.myScheduleChildBO.Id
            PopulateSchedule()
            PopulateFromScheduleChildBO()
            divScheduleDetails.Visible = True
            divAddNewSchedule.Visible = False
            btnAddNewSchedule.Enabled = False
            EnableDisableTabs(False) 'As Row is edited in the Grid so Disable Other tab. Enable it back when Save or Cancel in  Grid is clicked
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCancelNewSchedule_Click(sender As Object, e As System.EventArgs) Handles btnCancelNewSchedule.Click
        Try
            divScheduleDetails.Visible = False
            divAddNewSchedule.Visible = True
            PopulateScheduleDetail()
            State.ActionInProgress = DetailPageCommand.Accept
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#End Region

#Region "Ajax Common"

    'Private Function IsAutoCompleteEmpty(ByVal oTextBox As TextBox, ByVal oInp As HtmlInputHidden) As Boolean
    '    Return (oTextBox.Text Is String.Empty) OrElse (oTextBox.Text <> oInp.Value)
    'End Function

    'Private Sub PopulateBOAutoComplete(ByVal oTextBox As TextBox, ByVal oInpDesc As HtmlInputHidden, _
    '            ByVal oInpId As HtmlInputHidden, ByVal bo As Object, ByVal propertyName As String)
    '    If IsAutoCompleteEmpty(oTextBox, oInpDesc) = True Then
    '        oInpId.Value = Guid.Empty.ToString
    '    End If

    '    Me.PopulateBOProperty(bo, propertyName, New Guid(oInpId.Value))
    'End Sub

#End Region

    Protected Sub cboPaymentMethodId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
        DisplayBankInfo()
    End Sub

    Protected Sub DisplayBankInfo()
        Dim oPaymentMethodId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENTMETHOD, Codes.PAYMENT_METHOD__BANK_TRANSFER)
        If cboPaymentMethodId.SelectedValue.Equals(oPaymentMethodId.ToString) Then
            moBankInfoController.Visible = True
        Else
            moBankInfoController.Visible = False
        End If
    End Sub

    Protected Sub ddlClaimReservedbasedon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClaimReservedbasedon.SelectedIndexChanged
        'Dim oClaimReservedSalespriceId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIMRESERVED, Codes.CLAIM_RESERVED__SALES_PRICE)
        if Not ddlClaimReservedbasedon.SelectedValue.Equals(string.Empty) then
            ControlMgr.SetVisibleControl(Me, lblclaimreservedPercent, True)
            ControlMgr.SetVisibleControl(Me, txtclaimreservedPercent, True)
        else
            txtclaimreservedPercent.text = string.empty
            ControlMgr.SetVisibleControl(Me, lblclaimreservedPercent, False)
            ControlMgr.SetVisibleControl(Me, txtclaimreservedPercent, False)

        End If
    End Sub


    Protected Sub CheckBoxShipping_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShipping.CheckedChanged
        If CheckBoxShipping.Checked Then
            ControlMgr.SetVisibleControl(Me, LabelProcessingFee, True)
            ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, True)
        Else
            ControlMgr.SetVisibleControl(Me, LabelProcessingFee, False)
            ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, False)
        End If
    End Sub

    Protected Sub IsPriceListCodeValid()
        Dim Statusdv As DataView = LookupListNew.GetPriceList(State.MyBO.CountryId)
        'Check if the selected price code is in the active list
        Dim strPriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(ddlPriceList.SelectedValue))
        If (String.IsNullOrEmpty(strPriceListCode)) Then
            moMessageController.AddWarning(String.Format("{0}",
            TranslationBase.TranslateLabelOrMessage("PRICELISTCODE_IS_EXPIRED"), False))
        End If
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        'Verify information and Set Grid Details
        hdnSelectedTab.Value = SelectedTabIndex
        Dim strTemp As String = String.Empty
        If listDisabledTabs.Count > 0 Then
            For Each i As Integer In listDisabledTabs
                strTemp = strTemp + "," + i.ToString
            Next
            strTemp = strTemp.Substring(1) 'remove the first comma
        End If

        hdnDisabledTabs.Value = strTemp

        If CallingParameters IsNot Nothing Then
            Select Case SelectedTabIndex
                Case TAB_CONTACT ' Contacts 
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    PopulateQuantity()
                    PopulateSchedule()
                Case TAB_ATTRIBUTE ' Attribute Tab 
                    PopulateContacts()
                    PopulateQuantity()
                    PopulateSchedule()
                Case TAB_QUANTITY  ' Quantity tab
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    PopulateContacts()
                    PopulateSchedule()
                Case TAB_SCHEDULE ' Schedule Tab 
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    PopulateContacts()
                    PopulateQuantity()
            End Select
        End If
    End Sub

    Private Sub EnableDisableTabs(blnFlag As Boolean)
        listDisabledTabs.Clear()
        Dim cnt As Integer
        If blnFlag = False Then 'disable tabs
            For cnt = 0 To TAB_TOTAL_COUNT - 1
                If (cnt <> SelectedTabIndex) Then ' Enable - Disable other tabs than the one which is selected 
                    listDisabledTabs.Add(cnt)
                End If
            Next
        End If
        'Also Disable Save , Delete , Clone, New buttons in footer 
        btnSave_WRITE.Enabled = blnFlag
        btnDelete_WRITE.Enabled = blnFlag
        btnCopy_WRITE.Enabled = blnFlag
        btnNew_WRITE.Enabled = blnFlag
        btnAddNewSchedule.Enabled = blnFlag
    End Sub

    Private Sub EnableTab(tabInd As Integer, blnFlag As Boolean)
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

#Region "Method Of Repair Tab"
    Public Const MorGridColMor As Integer = 0
    Public Const MorGridColServiceWarrantyDays As Integer = 1

    Public Const MethodOfRepairNone As Integer = 0
    Public Const MethodOfRepairAdd As Integer = 1
    Public Const MethodOfRepairEdit As Integer = 2
    Public Const MethodOfRepairDelete As Integer = 3

    Private Sub GridViewMethodOfRepair_RowCommand(source As Object, e As GridViewCommandEventArgs) Handles GridViewMethodOfRepair.RowCommand
        Dim nIndex As Integer
        Dim guidTemp As Guid
        Try
            If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.MethodOfRepairList.FindIndex(Function(r) r.Id = guidTemp)
                State.MethodOfRepairWorkingItem = State.MethodOfRepairList.Item(nIndex)
            End If

            If e.CommandName = EDIT_COMMAND_NAME Then
                GridViewMethodOfRepair.EditIndex = nIndex
                GridViewMethodOfRepair.SelectedIndex = nIndex
                State.MethodOfRepairAction = MethodOfRepairEdit
                PopulateMethodOfRepairGrid(State.MethodOfRepairList)
                SetFocusInGrid(GridViewMethodOfRepair, nIndex, MorGridColServiceWarrantyDays)
                EnableDisableForMethodOfRepair(False)
            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                GridViewMethodOfRepair.EditIndex = nIndex
                GridViewMethodOfRepair.SelectedIndex = nIndex
                State.MethodOfRepairAction = MethodOfRepairDelete
                DeleteRecordMethodOfRepair()
            ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                SaveRecordMethodOfRepair()
                EnableDisableForMethodOfRepair(True)
            ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                CancelRecordMethodOfRepair()
                EnableDisableForMethodOfRepair(True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewMethodOfRepair_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewMethodOfRepair.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As ServCenterMethRepair

            If e.Row.DataItem IsNot Nothing Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                    dvRow = CType(e.Row.DataItem, ServCenterMethRepair)
                    If Not dvRow.ServCenterMorId.Equals(Guid.Empty) Then
                        Dim lblMethodOfRepair As Label
                        lblMethodOfRepair = CType(e.Row.Cells(MorGridColMor).FindControl("labelMethodOfRepair"), Label)
                        Dim dvMor As DataView = LookupListNew.GetMethodOfRepairLookupList(Authentication.LangId)
                        lblMethodOfRepair.Text = LookupListNew.GetDescriptionFromId(dvMor, dvRow.ServCenterMorId)
                    End If

                    'edit item and set value
                    If (State.MethodOfRepairAction = MethodOfRepairAdd OrElse State.MethodOfRepairAction = MethodOfRepairEdit) _
                        AndAlso State.MethodOfRepairWorkingItem.Id = dvRow.Id _
                        AndAlso dvRow.ServiceWarrantyDays IsNot Nothing Then
                        Dim txtServiceWarrantyDays As TextBox
                        txtServiceWarrantyDays = CType(e.Row.Cells(MorGridColServiceWarrantyDays).FindControl("TextBoxServiceWarrantyDays"), TextBox)
                        txtServiceWarrantyDays.Text = dvRow.ServiceWarrantyDays.Value.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateMethodOfRepairGrid(ds As Collections.Generic.List(Of ServCenterMethRepair))
        Dim blnEmptyList As Boolean = False, mySource As Collections.Generic.List(Of ServCenterMethRepair)
        If ds Is Nothing OrElse ds.Count = 0 Then
            mySource = New Collections.Generic.List(Of ServCenterMethRepair)
            mySource.Add(New ServCenterMethRepair())
            blnEmptyList = True
            GridViewMethodOfRepair.DataSource = mySource
        Else
            GridViewMethodOfRepair.DataSource = ds
        End If

        GridViewMethodOfRepair.DataBind()

        If blnEmptyList Then
            GridViewMethodOfRepair.Rows(0).Visible = False
        End If
    End Sub
    Private Sub PopulateAvailableMethodOfRepair()
        If Not State.MyBO.Id.Equals(Guid.Empty) AndAlso Not ddlMethodOfRepairAvailable.Items.Count > 0 Then
            'Dim availableDv As DataView = State.MyBO.GetAvailableMethodOfRepair()
            Dim AllMethodOfRepairList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="METHR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim SelectedMethodOfRepairList As DataElements.ListItem() = AllMethodOfRepairList.Clone()
            For Each scMrBO As ServCenterMethRepair In State.MyBO.ServiceCenterMethoOfRepairsChildren
                SelectedMethodOfRepairList = (From lst In SelectedMethodOfRepairList
                                              Where lst.ListItemId <> scMrBO.ServCenterMorId
                                              Select lst).ToArray()
            Next
            'BindListControlToDataView(ddlMethodOfRepairAvailable, availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            ddlMethodOfRepairAvailable.Populate(SelectedMethodOfRepairList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = False
                })
        End If
    End Sub
    Private Sub btnAddMethodOfRepair_Click(sender As System.Object, e As EventArgs) Handles btnAddMethodOfRepair.Click
        Try
            If ddlMethodOfRepairAvailable.SelectedItem Is Nothing Then Exit Sub

            State.MethodOfRepairAction = MethodOfRepairAdd
            Dim objNew As New ServCenterMethRepair()
            objNew.ServiceCenterId = State.MyBO.Id
            objNew.ServCenterMorId = New Guid(ddlMethodOfRepairAvailable.SelectedItem.Value)

            State.MethodOfRepairWorkingItem = objNew

            If State.MethodOfRepairList Is Nothing Then
                State.MethodOfRepairList = New Collections.Generic.List(Of ServCenterMethRepair)
            End If
            State.MethodOfRepairList.Add(objNew)

            GridViewMethodOfRepair.SelectedIndex = State.MethodOfRepairList.Count - 1
            GridViewMethodOfRepair.EditIndex = GridViewMethodOfRepair.SelectedIndex
            PopulateMethodOfRepairGrid(State.MethodOfRepairList)

            ddlMethodOfRepairAvailable.Items.Remove(ddlMethodOfRepairAvailable.SelectedItem)
            EnableDisableForMethodOfRepair(False)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CancelRecordMethodOfRepair()
        Try
            If State.MethodOfRepairAction = MethodOfRepairAdd Then
                ' Add the MOR back to drop down
                Dim lblMethodOfRepair As Label
                Dim oMethodOfRepairListItem As New ListItem
                lblMethodOfRepair = CType(GridViewMethodOfRepair.Rows(GridViewMethodOfRepair.EditIndex).Cells(MorGridColMor).FindControl("labelMethodOfRepair"), Label)
                With oMethodOfRepairListItem
                    .Text = lblMethodOfRepair.Text
                    .Value = State.MethodOfRepairWorkingItem.ServCenterMorId.ToString()
                End With
                ddlMethodOfRepairAvailable.Items.Add(oMethodOfRepairListItem)
                'remove from list
                State.MethodOfRepairList.Remove(State.MethodOfRepairWorkingItem)
            End If

            State.MethodOfRepairAction = MethodOfRepairNone
            State.MethodOfRepairWorkingItem = Nothing

            GridViewMethodOfRepair.SelectedIndex = -1
            GridViewMethodOfRepair.EditIndex = GridViewMethodOfRepair.SelectedIndex
            PopulateMethodOfRepairGrid(State.MethodOfRepairList)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveRecordMethodOfRepair()
        Dim objTxt As TextBox
        objTxt = CType(GridViewMethodOfRepair.Rows(GridViewMethodOfRepair.EditIndex).Cells(MorGridColServiceWarrantyDays).FindControl("TextBoxServiceWarrantyDays"), TextBox)
        PopulateBOProperty(State.MethodOfRepairWorkingItem, "ServiceWarrantyDays", objTxt)

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        If State.MethodOfRepairWorkingItem.IsDirty Then 'Save the changes
            If Not State.MyBO.IsNew AndAlso State.MethodOfRepairWorkingItem.IsDirty Then 'existing contract, save to DB directly
                State.MethodOfRepairWorkingItem.SaveWithoutCheckDsCreator()
                'reload the list
                State.MethodOfRepairList = Nothing
                State.MethodOfRepairList = ServCenterMethRepair.GetMethodOfRepairList(State.MyBO.Id)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else 'new contract, keep the record in memory after validation and save it with new contract

                Dim objInd As Integer = State.MethodOfRepairList.FindIndex(Function(r) r.Id = State.MethodOfRepairWorkingItem.Id)
                State.MethodOfRepairList.Item(objInd) = State.MethodOfRepairWorkingItem
                MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
            End If
            State.MyBO.MethodOfRepairCount = State.MyBO.MethodOfRepairCount + 1
        Else
            MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
        End If

        State.MethodOfRepairAction = MethodOfRepairNone
        GridViewMethodOfRepair.SelectedIndex = -1
        GridViewMethodOfRepair.EditIndex = GridViewMethodOfRepair.SelectedIndex

        State.MethodOfRepairWorkingItem = Nothing
        PopulateMethodOfRepairGrid(State.MethodOfRepairList)
    End Sub

    Private Sub DeleteRecordMethodOfRepair()

        ' Add the MOR back to drop down
        Dim lblMethodOfRepair As Label
        Dim oMethodOfRepairListItem As New ListItem
        lblMethodOfRepair = CType(GridViewMethodOfRepair.Rows(GridViewMethodOfRepair.EditIndex).Cells(MorGridColMor).FindControl("labelMethodOfRepair"), Label)
        With oMethodOfRepairListItem
            .Text = lblMethodOfRepair.Text
            .Value = State.MethodOfRepairWorkingItem.ServCenterMorId.ToString()
        End With
        ddlMethodOfRepairAvailable.Items.Add(oMethodOfRepairListItem)

        'remove from list
        State.MethodOfRepairList.Remove(State.MethodOfRepairWorkingItem)
        State.MethodOfRepairAction = MethodOfRepairNone

        ' Remove it from Database
        If Not State.MethodOfRepairWorkingItem.IsNew AndAlso Not State.MyBO.IsNew Then
            '    'if not new object, delete from database
            State.MethodOfRepairWorkingItem.Delete()
            State.MethodOfRepairWorkingItem.SaveWithoutCheckDsCreator()
        End If

        State.MethodOfRepairWorkingItem = Nothing
        GridViewMethodOfRepair.SelectedIndex = -1
        GridViewMethodOfRepair.EditIndex = GridViewMethodOfRepair.SelectedIndex
        PopulateMethodOfRepairGrid(State.MethodOfRepairList)
        State.MyBO.MethodOfRepairCount = State.MyBO.MethodOfRepairCount - 1
        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
    End Sub
    Private Sub DeleteAllRecordsMethodOfRepair()
        State.MethodOfRepairList = ServCenterMethRepair.GetMethodOfRepairList(State.MyBO.Id)
        If State.MethodOfRepairList.Count > 0 Then
            Dim i As Integer
            For i = 0 To State.MethodOfRepairList.Count - 1
                State.MethodOfRepairList.Item(i).Delete()
                State.MethodOfRepairList.Item(i).SaveWithoutCheckDsCreator()
            Next
        End If
        State.MethodOfRepairList = Nothing
    End Sub

    Private Sub SaveAllRecordsMethodOfRepair(blnNewBo As Boolean)
        ' new BO, save Depreciation Schedule records in memory
        If blnNewBo Then
            If (State.MethodOfRepairList IsNot Nothing) AndAlso State.MethodOfRepairList.Count > 0 Then
                Dim i As Integer
                For i = 0 To State.MethodOfRepairList.Count - 1
                    State.MethodOfRepairList.Item(i).ServiceCenterId = State.MyBO.Id
                    State.MethodOfRepairList.Item(i).SaveWithoutCheckDsCreator()
                Next
                State.MyBO.MethodOfRepairCount = State.MethodOfRepairList.Count
                State.MethodOfRepairList = Nothing
            Else
                State.MyBO.MethodOfRepairCount = 0
            End If
        End If
    End Sub
    Private Sub EnableDisableForMethodOfRepair(blnFlag As Boolean)

        ControlMgr.SetEnableControl(Me, ddlMethodOfRepairAvailable, blnFlag)
        ControlMgr.SetEnableControl(Me, btnAddMethodOfRepair, blnFlag)
        SetGridControls(GridViewMethodOfRepair, blnFlag)

        ' Disable all tab
        EnableDisableTabs(blnFlag)

        ' Disable Undo also
        ControlMgr.SetEnableControl(Me, btnUndo_Write, blnFlag)
    End Sub
    Private Sub ClearMethodOfRepairState()
        With State
            .MethodOfRepairAction = MethodOfRepairNone
            .MethodOfRepairWorkingItem = Nothing
            PopulateMethodOfRepairGrid(.MethodOfRepairList)
        End With

    End Sub


    Protected Sub BindBoPropertiesToMethodOfRepairGridHeaders()
        If State.MethodOfRepairWorkingItem IsNot Nothing Then
            BindBOPropertyToGridHeader(State.MethodOfRepairWorkingItem, "ServiceWarrantyDays", GridViewMethodOfRepair.Columns(MorGridColServiceWarrantyDays))
        End If
    End Sub
#End Region

#Region " Price List Recon Related "

#Region "Button event for upadating the price list update"
    Private Sub btnSubmitApproval_Click(sender As Object, e As EventArgs) Handles btnSubmitApproval.Click

        State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL
        State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        SavePriceAuthRecord(False)
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_APPROVED
            State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId

            Dim oListContext1 As New ListContext
            oListContext1.CountryId = State.MyBO.CountryId
            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            Dim PriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, State.MyBO.CurrentSVCPLRecon.PriceListId)
            State.MyBO.PriceListCode = PriceListCode
            SavePriceAuthRecord(True)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_REJECTED '"SVC_PL_RECON_PROCESS"
        State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        SavePriceAuthRecord(True)
    End Sub

    Private Sub SavePriceAuthRecord(refreshSVCPLReconBO As Boolean)
        Try
            State.MyBO.Save()
            If refreshSVCPLReconBO = True Then
                State.MyBO.CurrentSVCPLRecon = Nothing
            End If
            PopulateFormFromBOs(CMD_SAVE)
            EnableDisableFields()
            EnableDisableSVCPLControls()


        Catch ex As Exception
            'Me.State.MySvcBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
            Throw ex
        End Try

        'Me.State.PageIndex = Grid.PageIndex
        State.SvcPriciListDV = Nothing
        InitializePage()
        'PopulateGrid_PriceList()
        PopulateSvcDataGridPriceList()
        'Me.State.PageIndex = DataGridPriceList.CurrentPageIndex
    End Sub

#End Region

#Region "New DataGrid"
    Private Sub InitializePage()
        SetGridItemStyleColor(DataGridPriceList)

        LoadSvcPriceListReconDetails()

        If State.IsGridVisible Then

            DataGridPriceList.PageIndex = NewCurrentPageIndex(DataGridPriceList, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageSize = DataGridPriceList.PageSize
            'cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
            'Grid.PageSize = Me.State.PageSize
        End If

        SetGridItemStyleColor(DataGridPriceList)
        MenuEnabled = True
    End Sub

    Private Sub LoadSvcPriceListReconDetails()
        Try
            State.PageIndex = 0
            State.selectedSvcPriceListReconId = Guid.Empty
            State.IsGridVisible = True
            State.SvcPriciListDV = Nothing
            'Me.PopulateSvcDataGridPriceList()

            'If (Not State.SvcPriciListDV Is Nothing) AndAlso State.SvcPriciListDV.Count > 0 Then
            '    Dim guidCI As Guid = New Guid(CType(State.SvcPriciListDV.Item(0)(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID), Byte()))
            'End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Function GetSvcDV() As DataView

        State.SvcPriciListDV = GetDataGridDataView()
        State.SvcPriciListDV.Sort = DataGridPriceList.DataMember()

        Return (State.SvcPriciListDV)

    End Function

    Private Function GetDataGridDataView() As DataView

        With State
            'Return (SvcPriceListRecon.LoadList(Me.State.MyBO.Code, Me.State.MyBO.PriceListCode, Me.State.MyBO.CountryId))
            'Return (SvcPriceListRecon.LoadListBySvc(Me.State.MyBO.Id))
            Return (SvcPriceListRecon.LoadListBySvc(State.MyBO.Id))
        End With

    End Function
    Private Sub PopulateSvcDataGridPriceList()
        Dim foundLabel As String

        Try

            If (State.SvcPriciListDV Is Nothing) Then
                State.SvcPriciListDV = GetSvcDV()
            End If

            DataGridPriceList.DataSource = State.SvcPriciListDV
            DataGridPriceList.AllowSorting = False
            'Me.DataGridPriceList.DataBind()
            Session("PLrecCount") = State.SvcPriciListDV.Count

            'DataGridPriceList.Columns(Me.SVC_PRICE_LIST_RECON_ID_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID
            DataGridPriceList.Columns(SVC_PRICE_LIST_ID_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID
            DataGridPriceList.Columns(SVC_REQUESTED_BY_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE
            DataGridPriceList.Columns(SVC_STATUS_XCD_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD
            DataGridPriceList.Columns(SVC_STATUS_MODIFIED_by_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_BY

            If State.SvcPriciListDV.Count > 0 Then
                DataGridPriceList.DataSource = State.SvcPriciListDV
                DataGridPriceList.AutoGenerateColumns = False
                DataGridPriceList.DataBind()
                EnableDisableSVCPLControls()
            Else
                State.SvcPriciListDV.AddNew()
                State.SvcPriciListDV(0)(0) = Guid.Empty.ToByteArray
                DataGridPriceList.DataSource = State.SvcPriciListDV
                DataGridPriceList.DataBind()
                DataGridPriceList.Rows(0).Visible = False
                DataGridPriceList.Rows(0).Controls.Clear()
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
                ControlMgr.SetEnableControl(Me, btnApprove, False)
                ControlMgr.SetEnableControl(Me, btnReject, False)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#Region " Datagrid Related "

    'The Binding Logic is here
    Private Sub DataGridPriceList_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridPriceList.RowDataBound

        'Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        'Dim chkRow As CheckBox
        Dim oListContext1 As New ListContext
        oListContext1.CountryId = State.MyBO.CountryId
        Dim PriceList As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

        Dim SVC_PL_Process As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

        If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
            'If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

            If dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID) IsNot Nothing Then
                e.Row.Cells(SVC_PRICE_LIST_RECON_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID), Byte()))
            End If


            If dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID) IsNot Nothing AndAlso dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID) IsNot DBNull.Value Then
                e.Row.Cells(SVC_PRICE_LIST_ID_COL).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PRICE_LIST, GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID), Byte()))))
            End If

            PopulateControlFromBOProperty(e.Row.Cells(SVC_REQUESTED_BY_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_BY))


            If Not dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE).ToString = Nothing Then
                e.Row.Cells(SVC_REQUESTED_DATE_COL).Text = GetDateFormattedString(CType(dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE), Date))
            End If

            If dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD) IsNot Nothing Then
                e.Row.Cells(SVC_STATUS_XCD_COL).Text = (From lst In SVC_PL_Process
                                                           Where lst.ExtendedCode = dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD).ToString
                                                           Select lst.Translation).FirstOrDefault()
            End If

            'Me.PopulateControlFromBOProperty(e.Item.Cells(Me.SVC_STATUS_XCD_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD))
            PopulateControlFromBOProperty(e.Row.Cells(SVC_STATUS_MODIFIED_by_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_BY))

            If Not dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE).ToString = Nothing Then
                e.Row.Cells(SVC_STATUS_DATE_COL).Text = GetDateFormattedString(CType(dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE), Date))
            End If

            'Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SVC_STATUS_DATE_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE))

        End If
    End Sub


    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridPriceList_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridPriceList.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

    Protected Sub AddSVRcReconRec(PriceListId As Guid, processStatus As String)

        Dim _SvcPriceListRecon As SvcPriceListRecon = State.MyBO.Add_SVCPLRecon
        _SvcPriceListRecon.PriceListId = PriceListId
        _SvcPriceListRecon.Status_xcd = processStatus
        _SvcPriceListRecon.RequestedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId

    End Sub

    Private Sub EnableDisableSVCPLControls()

        If State.MyBO.CurrentSVCPLRecon IsNot Nothing Then

            If State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL Then
                'Me.State.selectedSvcPriceListReconId = New Guid(e.Item.Cells(Me.SVC_PRICE_LIST_RECON_ID_COL).Text)
                'Me.State.MySvcBO = New SvcPriceListRecon(Me.State.selectedSvcPriceListReconId)
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
                ControlMgr.SetEnableControl(Me, btnApprove, True)
                ControlMgr.SetEnableControl(Me, btnReject, True)
            ElseIf State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_PENDINGSUBMISSION Then
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, True)
                ControlMgr.SetEnableControl(Me, btnApprove, False)
                ControlMgr.SetEnableControl(Me, btnReject, False)
            Else
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
                ControlMgr.SetEnableControl(Me, btnApprove, False)
                ControlMgr.SetEnableControl(Me, btnReject, False)

            End If

        Else
            ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
            ControlMgr.SetEnableControl(Me, btnApprove, False)
            ControlMgr.SetEnableControl(Me, btnReject, False)


        End If


    End Sub

    Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridPriceList.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            DataGridPriceList.PageIndex = State.PageIndex
            PopulateSvcDataGridPriceList()
            DataGridPriceList.SelectedIndex = NO_ITEM_SELECTED_INDEX

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            DataGridPriceList.PageIndex = NewCurrentPageIndex(DataGridPriceList, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateSvcDataGridPriceList()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub DeleteAllPriceListReconRecords()

        'Dim ds As DataSet = dal.GetSelectedListMor(serviceCenterId)
        Dim plList As New Collections.Generic.List(Of SvcPriceListRecon)
        For Each dr As DataRow In State.SvcPriciListDV.Table.Rows
            plList.Add(New SvcPriceListRecon(dr))
        Next

        If plList.Count > 0 Then
            Dim i As Integer
            For i = 0 To plList.Count - 1
                plList.Item(i).Delete()
                plList.Item(i).SaveWithoutCheckDsCreator()
            Next
        End If

    End Sub

    Private Sub ClearPriceListReconState()
        State.MyBO.CurrentSVCPLRecon = Nothing
        txtPriceListPending.Text = Nothing
        txtPriceListPendingStatus.Text = Nothing
        State.SvcPriciListDV = Nothing
        'Me.State.IsCopy = True
    End Sub


#End Region

#End Region

End Class


