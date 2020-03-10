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
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

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

    Private Const SVC_PL_RECON_PROCESS_PENDINGAPPROVAL As String = "SVC_PL_RECON_PROCESS-PENDINGAPPROVAL"
    Private Const SVC_PL_RECON_PROCESS_PENDINGSUBMISSION As String = "SVC_PL_RECON_PROCESS-PENDINGSUBMISSION"

#End Region

#Region "Attributes"
    Private moSvcCtr As ServiceCenter
    Private listDisabledTabs As New Collections.Generic.List(Of Integer)
    Private SelectedTabIndex As Integer = 0
#End Region

#Region "Properties"

    Private Property SvcCtrId() As Guid
        Get
            Return Me.State.SvcCtrId
        End Get
        Set(ByVal Value As Guid)
            Me.State.SvcCtrId = Value
        End Set

    End Property


    Private ReadOnly Property IsNewServiceCenter() As Boolean
        Get
            Return Me.State.MyBO.IsNew
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
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ServiceCenter, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public SVCId As Guid
        Public PageDealerId As Guid
        Public mbIsComingFromDealerform As Boolean = False
        Public Sub New(ByVal SVCorDealerId As Guid, Optional ByVal bIsComingFromDealerform As Boolean = False)
            Me.mbIsComingFromDealerform = bIsComingFromDealerform
            If bIsComingFromDealerform Then
                Me.PageDealerId = SVCorDealerId
            Else
                Me.SVCId = SVCorDealerId
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

        Public MySvcBO As SvcPriceListRecon
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
        Public IsCopy As Boolean = False

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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.pageParameters = CType(Me.CallingParameters, Parameters)
                If Not Me.State.pageParameters.mbIsComingFromDealerform Then
                    Me.State.MyBO = New ServiceCenter(Me.State.pageParameters.SVCId)
                    Me.State.MySvcBO = New SvcPriceListRecon(Me.State.pageParameters.SVCId)
                    If Me.State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                        Me.State.MyBO.isBankInfoNeedDeletion = False
                    Else
                        Me.State.MyBO.isBankInfoNeedDeletion = True
                    End If
                    If Not Me.State.MyBO.RouteId.Equals(Guid.Empty) Then
                        Me.State.oRoute = New Route(Me.State.MyBO.RouteId)
                    End If
                    Me.State.stdealerid = System.Guid.Empty
                    'Me.State.stIsComingFromDealerform = False
                Else
                    Me.State.stdealerid = Me.State.pageParameters.PageDealerId
                    Me.State.stIsComingFromDealerform = True
                End If
            Else
                Me.State.stdealerid = System.Guid.Empty
                'Me.State.stIsComingFromDealerform = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TABLES)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
            Me.UpdateBreadCrum()
            Me.UserControlContactInfo.ShowJobTitle = True
            Me.UserControlContactInfo.ShowCompany = True
            CType(moBankInfoController.FindControl("HiddenClassName"), HiddenField).Value = "borderLeft"
            Me.moMessageController.Clear_Hide()

            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ServiceCenter
                    Me.State.IsNew = True
                End If
                'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
                Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
                Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                                Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                                Select Country).ToArray()
                Me.moCountryDrop.Populate(UserCountries.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = False
                                    })
                Me.InitializePage()
                TranslateGridHeader(moContactsGridView)
                TranslateGridHeader(moQuantityGridView)
                TranslateGridHeader(moScheduleGridView)
                TranslateGridHeader(moAddScheduleGridView)
                SetGridItemStyleColor(GridViewMethodOfRepair)
                TranslateGridHeader(GridViewMethodOfRepair)
                'TranslateGridHeader(Me.Grid)
                TranslateGridHeader(Me.DataGridPriceList)
                AttributeValues.TranslateHeaders()
                PopulateChildern()
                PopulateCountry()
                PopulateDropdowns()
                PopulateIntegratedWithDropdowns()
                Me.moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                Me.moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                Me.State.BankInfoBO = Nothing
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Else
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                SelectedTabIndex = hdnSelectedTab.Value
            End If

            BindBoPropertiesToLabels()
            BindBoPropertiesToMethodOfRepairGridHeaders()
            ClearGridViewHeadersAndLabelsErrorSign()
            CheckIfComingFromSaveConfirm()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
            'BindBoPropertiesToGridHeaders()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)


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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If Me.State.MyBO.ConstrVoilation = False Then
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            Else
                Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.State.ForEdit = True
            Me.PopulateBOsFromForm()

            If Me.State.MyBO.IsDirty Then
                'check the Vendor Quantity records, if they are to be added or updated
                Dim detail As VendorQuantity
                For Each detail In Me.State.MyBO.QuantityChildren
                    If Not detail.VendorQuantityRecordAvaliable Then
                        detail.SetRowStateAsAdded()
                    End If
                Next
                If State.MyBO.IsNew Then
                    State.MyBO.Save()
                    'State.MySvcBO.Save()
                    SaveAllRecordsMethodOfRepair(True)
                Else

                    If Not Me.State.IsCopy Or Not Me.State.IsNew Then

                        If Not String.IsNullOrEmpty(Me.State.priceListApprovalflag) And Me.State.priceListApprovalflag = "YESNO-Y" Then
                            'Only after Approval, Price List will be associated to Service Center
                            Me.PopulateBOProperty(Me.State.MyBO, "PriceListCode", Me.State.CurrentPriceListCode)
                        End If

                    End If

                    State.MyBO.Save()
                        'State.MySvcBO.Save()
                    End If

                    Me.State.SvcPriciListDV = Nothing
                PopulateSvcDataGridPriceList()
                Me.State.IsCopy = False
                Me.State.IsNew = False
                Me.State.HasDataChanged = True

                PopulateCountry()

                'Dim dvPriceList As DataView
                'dvPriceList = LookupListNew.GetPriceListLookupList(Me.State.MyBO.CountryId)
                Dim oListContext1 As New ListContext
                oListContext1.CountryId = Me.State.MyBO.CountryId
                Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
                Dim PriceListID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.PriceListCode)

                If Not Me.State.MyBO.IsNew AndAlso PriceListID.ToString() = NOTHING_SELECTED_GUID _
                     AndAlso Not Me.State.MyBO.PriceListCode Is Nothing Then
                    'add svc specific code
                    'Dim drServiceSpecificPriceListCode As DataRow = dvPriceList.Table.NewRow()
                    'drServiceSpecificPriceListCode("CODE") = Me.State.MyBO.PriceListCode
                    'drServiceSpecificPriceListCode("DESCRIPTION") = Me.State.MyBO.PriceListCode
                    'dvPriceList.Table.Rows.Add(drServiceSpecificPriceListCode)
                    Dim _item As DataElements.ListItem = New DataElements.ListItem()
                    _item.Code = Me.State.MyBO.PriceListCode
                    _item.Translation = Me.State.MyBO.PriceListCode
                    PriceList.ToList().Add(_item)
                End If
                'Me.BindListControlToDataView(Me.ddlPriceList, dvPriceList, , , True)
                Me.ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

                Me.PopulateIntegratedWithDropdowns()
                Me.PopulateFormFromBOs(Me.CMD_SAVE)
                Me.EnableDisableFields()

                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                If Me.State.stIsComingFromDealerform Then
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Else
                inpLoanerCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LOANER_CENTERS, Me.State.MyBO.LoanerCenterId)
                If Not inpLoanerCenterDesc.Value = TextBoxLoanerCenter.Text Then
                    inpLoanerCenterId.Value = Me.State.MyBO.LoanerCenterId.ToString
                    TextBoxLoanerCenter.Text = inpLoanerCenterDesc.Value
                End If
                inpMasterCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, Me.State.MyBO.MasterCenterId)
                If Not inpMasterCenterDesc.Value = TextBoxMasterCenter.Text Then
                    inpMasterCenterId.Value = Me.State.MyBO.MasterCenterId.ToString
                    TextBoxMasterCenter.Text = inpMasterCenterDesc.Value
                End If
                Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
            'Make the Date Added label and field visible - for a newly added Service Center
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, True)
            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        Finally
            populateVendorManagementControls()
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ServiceCenter(Me.State.MyBO.Id)
                'Me.State.MySvcBO = New SvcPriceListRecon(Me.State.MySvcBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New ServiceCenter
                'Me.State.MySvcBO = New SvcPriceListRecon
            End If
            PopulateCountry()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.EnableDisableTabs(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            ' Delete all Method of Repair associated with Service Center
            DeleteAllRecordsMethodOfRepair()
            DeleteAllPriceListReconRecords()

            'Delete the Address
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
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
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
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

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            Me.PopulateFormFromBOs(Me.CMD_CHANGE_COUNTRY)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "AUTHORIZED MANUFACTURER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedManufacturers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachManufacturers(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachManufacturers(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "COVERED DISTRICTS: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDistricts_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachDistricts(attachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDistricts_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachDistricts(detachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "PREFERRED DEALER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDealers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachDealers(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachServiceNetworks(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDealers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachDealers(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachServiceNetworks(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Public Shared Function PopulateMasterCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.CountryId)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateLoanerCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetLoanerCenterLookupList(AjaxState.MyBO.CountryId)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

#End Region

#End Region

#Region "Controlling Logic"
    Protected Sub ResolveShippingFeeVisibility()
        Me.CheckBoxShipping.Attributes.Add("onClick", "showProcessingFee(this);")
        If Not Me.CheckBoxShipping.Checked Then
            Me.LabelProcessingFee.Style.Add("Display", "'none'")
            Me.TextboxPROCESSING_FEE.Style.Add("Display", "'none'")
        Else
            Me.LabelProcessingFee.Style.Add("Display", "''")
            Me.TextboxPROCESSING_FEE.Style.Add("Display", "''")
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
            Me.cboOriginalDealer_WRITE.Visible = True
            Me.lblDealer.Visible = True
        Else
            Me.cboOriginalDealer_WRITE.Visible = False
            Me.lblDealer.Visible = False
        End If

        'enable/disable/visible depending on if coming from dealerform
        ControlMgr.SetEnableControl(Me, cboOriginalDealer_WRITE, Me.State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, lblDealer, Me.State.stdealerid.Equals(Guid.Empty))

        'Now disable depending on the object state
        If Me.State.MyBO.IsNew Then
            'Enable and blank out the Service Center Code field
            ControlMgr.SetEnableControl(Me, TextboxCode, True)
            Me.TextboxCode.Text = String.Empty

            'Blank out the Service Center Description field
            Me.TextboxDescription.Text = String.Empty

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
            If Me.State.MyBO.IntegratedAsOf Is Nothing Then
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

        If Not String.IsNullOrEmpty(Me.State.MyBO.PriceListCode) Then
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
        If Not Me.State.myContactsChildBO Is Nothing Then
            If Me.State.myContactsChildBO.IsNew Then
                Me.txtEffective.Enabled = True
                ibtnEffective.Enabled = True
            Else
                Me.txtEffective.Enabled = False
                ibtnEffective.Enabled = False
            End If
        End If

        Me.DisplayBankInfo()

        Me.State.priceListApprovalflag = New Country(Me.State.MyBO.CountryId).PriceListApprovalNeeded.ToString()
        If Not Me.State.priceListApprovalflag = String.Empty And Me.State.priceListApprovalflag = "YESNO-Y" Then

            PL_APPROVE_SEC.Visible = True

            If Not IsNothing(Me.State.MyBO.CurrentSVCPLRecon) Then

                If Not String.IsNullOrEmpty(Me.State.MyBO.CurrentSVCPLRecon.Status_xcd) And Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = SVC_PL_RECON_PROCESS_PENDINGAPPROVAL Then
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

        'disable the controls if user has view only permission for this form
        If Me.PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then
            SetEnabledForControlFamily(Me.EditPanel, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceGroupId", Me.LabelServiceGroupId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LoanerCenterId", Me.LabelLoanerCenterId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MasterCenterId", Me.LabelMasterCenterId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IntegratedAsOf", Me.IntegratedAsOfLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RatingCode", Me.LabelRatingCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactName", Me.LabelContactName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OwnerName", Me.LabelOwnerName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Phone1", Me.LabelPhone1)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Phone2", Me.LabelPhone2)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Fax", Me.LabelFax)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Email", Me.LabelEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CcEmail", Me.LabelCcEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpAddress", Me.LabelFtpAddress)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxId", Me.LabelTaxId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceWarrantyDays", Me.LabelServiceWarrantyDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "StatusCode", Me.LabelStatusCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BusinessHours", Me.LabelBusinessHours)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DateAdded", Me.LabelDateAdded)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DateModified", Me.LabelDateLastMaintained)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "WithholdingRate", Me.lblWithholdingRate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "comments", Me.LabelComment1)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentMethodId", Me.PaymentMethodDrpLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReverseLogisticsId", Me.lblReverseLogistics)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProcessingFee", Me.LabelProcessingFee)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DistributionMethodId", Me.lblDistributionMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FulfillmentTimeZoneId", Me.lblFulFillingTimeZone)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DiscountPct", Me.lblDiscountPercent)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NetDays", Me.lblNetDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DiscountDays", Me.lblDiscountDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListCode", Me.lblPriceList)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListCodeinprogress", Me.lblPriceListPending)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListCodeStatusInProgress", Me.lblPriceListPendingStatus)

        AddressCtr.SetTheRequiredFields()

        'added by Anindita - original dealer id was not added here
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OriginalDealerId", Me.lblDealer)
        Me.ClearGridHeadersAndLabelsErrSign()

        Me.BindBOPropertyToLabel(Me.State.myContactsChildBO, "Effective", Me.lblEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.myContactsChildBO, "Expiration", Me.lblExpirationDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PreInvoiceId", Me.lblPreInvoice)
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If Me.State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            If Not Me.State.BankInfoBO Is Nothing Then
                Me.State.BankInfoBO.SourceCountryID = Me.GetSelectedItem(moCountryDrop)
            End If
        Else
            oCountry = New Country(Me.State.MyBO.CountryId)
            Me.SetSelectedItem(moCountryDrop, Me.State.MyBO.CountryId)
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
            If Not Me.State.BankInfoBO Is Nothing Then
                Me.State.BankInfoBO.SourceCountryID = Me.State.MyBO.CountryId
            End If
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso Me.State.IsNew) Then
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
        Me.State.Statusdv = Statusdv

        With Me.State.MyBO
            'Me.BindListControlToDataView(Me.cboServiceGroupId, LookupListNew.GetServiceGroupLookupList(.CountryId), , , True)
            Dim oListContext2 As New ListContext
            oListContext2.CountryId = .CountryId
            Me.cboServiceGroupId.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceGroupByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext2),
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
            Me.cboPaymentMethodId.Populate(PaymentMethod.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.cboOriginalDealer_WRITE, LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Me.State.MyBO.Id), , , True)
            Dim oListContext3 As New ListContext
            oListContext3.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            oListContext3.ServiceCenterId = Me.State.MyBO.Id
            Me.cboOriginalDealer_WRITE.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="OriginalDealerByCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext3),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

            'Me.BindListControlToDataView(Me.cboStatusCode, Statusdv)
            Me.cboStatusCode.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SCSTAT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .TextFunc = AddressOf .GetCode,
                            .SortFunc = AddressOf .GetCode
                        })
            'Me.BindListControlToDataView(Me.cboReverseLogisticsId, yesNoLkL)
            Me.cboReverseLogisticsId.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.ddlDistributionMethod, DistMethod, , , True)
            Me.ddlDistributionMethod.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="DISTMETH", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'Me.BindListControlToDataView(Me.ddlFulFillingTimeZone, TimeZone, , , True)
            Me.ddlFulFillingTimeZone.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="TZN", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
            'BindListControlToDataView(Me.ddlPreInvoice, PreInvoice, , , False)
            Me.ddlPreInvoice.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCPREINV", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })

            Dim dvPriceList As DataView
            dvPriceList = LookupListNew.GetPriceListLookupList(.CountryId)
            Dim oListContext1 As New ListContext
            oListContext1.CountryId = Me.State.MyBO.CountryId
            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            Dim PriceListID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.PriceListCode)

            If Not Me.State.MyBO.IsNew AndAlso PriceListID.ToString() = NOTHING_SELECTED_GUID _
                 AndAlso Not Me.State.MyBO.PriceListCode Is Nothing Then
                'add svc specific code
                'Dim drServiceSpecificPriceListCode As DataRow = dvPriceList.Table.NewRow()
                'drServiceSpecificPriceListCode("CODE") = Me.State.MyBO.PriceListCode
                'drServiceSpecificPriceListCode("DESCRIPTION") = Me.State.MyBO.PriceListCode
                'dvPriceList.Table.Rows.Add(drServiceSpecificPriceListCode)
                Dim _item As DataElements.ListItem = New DataElements.ListItem()
                _item.Code = Me.State.MyBO.PriceListCode
                _item.Translation = Me.State.MyBO.PriceListCode
                PriceList.ToList().Add(_item)

                Me.moMessageController.AddWarning(String.Format("{0}",
                TranslationBase.TranslateLabelOrMessage("PRICELISTCODE_IS_EXPIRED"), False))
            End If

            'Me.BindListControlToDataView(Me.ddlPriceList, dvPriceList, , , True)
            Me.ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            'ddlAutoProcessInventoryFile.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            Me.ddlAutoProcessInventoryFile.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
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

        With Me.State.MyBO
            'Dim dvIntegratedWith As DataView = LookupListNew.GetIntegratedWithLookupList(langId)
            'Dim integratedWithCode As String = LookupListNew.GetCodeFromId(dvIntegratedWith, .IntegratedWithID)
            Dim IntegratedWith As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="INTR", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim integratedWithCode As String = (From lst In IntegratedWith
                                                Where lst.ListItemId = .IntegratedWithID
                                                Select lst.Code).FirstOrDefault()
            If Not integratedWithCode Is Nothing AndAlso integratedWithCode = Codes.INTEGRATED_WITH_AGVS Then
                'dvIntegratedWith.RowFilter = "code <> '" & Codes.INTEGRATED_WITH_GVS & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                'Me.BindListControlToDataView(Me.cboIntegratedWithId, dvIntegratedWith, , , True)
                Me.cboIntegratedWithId.Populate((From lst In IntegratedWith
                                                 Where lst.Code <> Codes.INTEGRATED_WITH_GVS
                                                 Select lst).ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Else
                'dvIntegratedWith.RowFilter = "code <> '" & Codes.INTEGRATED_WITH_AGVS & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                'Me.BindListControlToDataView(Me.cboIntegratedWithId, dvIntegratedWith, , , True)
                Me.cboIntegratedWithId.Populate((From lst In IntegratedWith
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

        With Me.State.MyBO
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.cboServiceGroupId, "ServiceGroupId")
            inpLoanerCenterId.Value = .LoanerCenterId.ToString
            inpLoanerCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LOANER_CENTERS, .LoanerCenterId)
            TextBoxLoanerCenter.Text = inpLoanerCenterDesc.Value
            inpMasterCenterId.Value = .MasterCenterId.ToString
            inpMasterCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .MasterCenterId)
            TextBoxMasterCenter.Text = inpMasterCenterDesc.Value
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.cboPaymentMethodId, "PaymentMethodId")
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.cboReverseLogisticsId, "ReverseLogisticsId")

            Me.SetSelectedItem(Me.cboIntegratedWithId, .IntegratedWithID)
            Me.PopulateControlFromBOProperty(Me.chkPayMaster, .PayMaster)

            If Me.State.stIsComingFromDealerform Then
                Me.SetSelectedItem(Me.cboOriginalDealer_WRITE, Me.State.stdealerid)
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And
                    Me.State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    Me.cboOriginalDealer_WRITE.SelectedIndex = Me.NOTHING_SELECTED
                ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And
                    Not Me.State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    If Me.State.MyBO.IsNew Then
                        Me.cboOriginalDealer_WRITE.SelectedIndex = Me.NOTHING_SELECTED
                    Else
                        Me.SetSelectedItem(Me.cboOriginalDealer_WRITE, .OriginalDealerId)
                    End If
                Else
                    Me.SetSelectedItem(Me.cboOriginalDealer_WRITE, System.Guid.Empty)
                End If
            End If

            Me.PopulateControlFromBOProperty(Me.TextboxCode, .Code)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.TextboxRatingCode, .RatingCode)
            Me.PopulateControlFromBOProperty(Me.TextboxContactName, .ContactName)
            Me.PopulateControlFromBOProperty(Me.TextboxOwnerName, .OwnerName)
            Me.PopulateControlFromBOProperty(Me.TextBoxWithholdingRate, .WithholdingRate)
            Me.PopulateControlFromBOProperty(Me.TextboxPhone1, .Phone1)
            Me.PopulateControlFromBOProperty(Me.TextboxPhone2, .Phone2)
            Me.PopulateControlFromBOProperty(Me.TextboxFax, .Fax)
            Me.PopulateControlFromBOProperty(Me.TextboxEmail, .Email)
            Me.PopulateControlFromBOProperty(Me.TextboxCcEmail, .CcEmail)
            Me.PopulateControlFromBOProperty(Me.TextboxFtpAddress, .FtpAddress)
            Me.PopulateControlFromBOProperty(Me.TextboxTaxId, .TaxId)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceWarrantyDays, .ServiceWarrantyDays)
            'Me.PopulateControlFromBOProperty(Me.cboStatusCode, LookupListNew.GetIdFromCode(statusdv, .StatusCode))
            Me.PopulateControlFromBOProperty(Me.cboStatusCode, (From lst In Me.State.Statusdv
                                                                Where lst.Code = .StatusCode
                                                                Select lst.ListItemId).FirstOrDefault())
            Me.PopulateControlFromBOProperty(Me.TextboxBusinessHours, .BusinessHours)
            Me.PopulateControlFromBOProperty(Me.CheckBoxDefaultToEmail, .DefaultToEmailFlag)
            Me.PopulateControlFromBOProperty(Me.CheckBoxIvaResponsible, .IvaResponsibleFlag)
            Me.PopulateControlFromBOProperty(Me.CheckBoxFreeZone, .FreeZoneFlag)
            Me.PopulateControlFromBOProperty(Me.TextboxDateAdded, .CreatedDate)
            Me.PopulateControlFromBOProperty(Me.TextboxDateLastMaintained, .ModifiedDate)
            Me.PopulateControlFromBOProperty(Me.TextboxIntegratedAsOf, .IntegratedAsOf)
            Me.PopulateControlFromBOProperty(Me.TextboxComment, .Comments)
            Me.PopulateControlFromBOProperty(Me.CheckBoxShipping, .Shipping)

            If .Shipping Then
                If CheckBoxShipping.Checked Then
                    ControlMgr.SetVisibleControl(Me, LabelProcessingFee, True)
                    ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, True)
                Else
                    ControlMgr.SetVisibleControl(Me, LabelProcessingFee, False)
                    ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, False)
                End If
                Me.PopulateControlFromBOProperty(Me.TextboxPROCESSING_FEE, .ProcessingFee)
            End If

            AddressCtr.Bind(.Address)

            If Me.State.IsNew Then
                If Me.State.BankInfoBO Is Nothing Then
                    Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
                End If
            Else
                Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
            End If

            Me.UserBankInfoCtr.SetTheRequiredFields()
            If Not (cmd = Me.CMD_SAVE) Then
                Me.State.BankInfoBO.SourceCountryID = Me.State.MyBO.CountryId
            Else
                Me.State.BankInfoBO.SourceCountryID = Me.State.MyBO.CountryId
            End If

            Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO)
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.ddlDistributionMethod, "DistributionMethodId")
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.ddlFulFillingTimeZone, "FulfillmentTimeZoneId")
            Me.PopulateControlFromBOProperty(Me.TextBoxDiscountPercent, .DiscountPct)
            Me.PopulateControlFromBOProperty(Me.TextBoxNetDays, .NetDays)
            Me.PopulateControlFromBOProperty(Me.TextBoxDiscountDays, .DiscountDays)
            Me.PopulateControlFromPropertyName(Me.State.MyBO, Me.ddlPreInvoice, "PreInvoiceId")
            ddlAutoProcessInventoryFile.ClearSelection()
            BindSelectItem(State.MyBO.AutoProcessInventoryFileXcd, ddlAutoProcessInventoryFile)
        End With

        Dim list As DataView = LookupListNew.GetPriceListLookupList(Me.State.MyBO.CountryId)
        Dim selectedItemId As Guid = LookupListNew.GetIdFromCode(list, Me.State.MyBO.PriceListCode)

        Me.PopulateControlFromBOProperty(Me.ddlPriceList, selectedItemId)

        Dim oListContext1 As New ListContext
        oListContext1.CountryId = Me.State.MyBO.CountryId

        'If Me.State.IsCopy = False And Me.State.IsNew = False Then

        If Not Me.State.MyBO.CurrentSVCPLRecon() Is Nothing Then

            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            'Dim PriceListDescription As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.CurrentSVCPLRecon.PriceListId)
            'Me.State.MyBO.PriceListCode = PriceListCode

            Me.PopulateControlFromBOProperty(Me.txtPriceListPending, LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.CurrentSVCPLRecon.PriceListId))

            Dim SVC_PL_Process As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

            Dim SVC_PL_Process_Status As String = (From lst In SVC_PL_Process
                                                   Where lst.ExtendedCode = Me.State.MyBO.CurrentSVCPLRecon.Status_xcd
                                                   Select lst.Translation).FirstOrDefault()

            Me.PopulateControlFromBOProperty(Me.txtPriceListPendingStatus, SVC_PL_Process_Status)
        End If
        'End If


        If Not Me.State.MyBO.RouteId.Equals(Guid.Empty) Then
            Me.PopulateControlFromBOProperty(Me.TextboxRoute, Me.State.oRoute.Description)
        End If

        PopulateDetailControls()
        populateVendorManagementControls()

        If Me.State.MethodOfRepairList Is Nothing Then
            Me.State.MethodOfRepairList = ServCenterMethRepair.GetMethodOfRepairList(Me.State.MyBO.Id)
        End If
        PopulateMethodOfRepairGrid(State.MethodOfRepairList)
        'PopulateGrid_PriceList()
        PopulateSvcDataGridPriceList()
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.State.MyBO

            If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And
                Me.cboOriginalDealer_WRITE.SelectedValue.Trim.Length > 0 Then
                Me.PopulateBOProperty(Me.State.MyBO, "OriginalDealerId", Me.cboOriginalDealer_WRITE)
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "ServiceGroupId", Me.cboServiceGroupId)

            If Not inpLoanerCenterId.Value = Guid.Empty.ToString Then
                AjaxController.PopulateBOAutoComplete(TextBoxLoanerCenter, inpLoanerCenterDesc,
                    inpLoanerCenterId, Me.State.MyBO, "LoanerCenterId")
            End If
            If Not inpMasterCenterId.Value = Guid.Empty.ToString Then
                AjaxController.PopulateBOAutoComplete(TextBoxMasterCenter, inpMasterCenterDesc,
                                inpMasterCenterId, Me.State.MyBO, "MasterCenterId")
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "RatingCode", Me.TextboxRatingCode)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactName", Me.TextboxContactName)
            Me.PopulateBOProperty(Me.State.MyBO, "OwnerName", Me.TextboxOwnerName)
            Me.PopulateBOProperty(Me.State.MyBO, "WithholdingRate", Me.TextBoxWithholdingRate)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone1", Me.TextboxPhone1)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone2", Me.TextboxPhone2)
            Me.PopulateBOProperty(Me.State.MyBO, "Fax", Me.TextboxFax)
            Me.PopulateBOProperty(Me.State.MyBO, "Email", Me.TextboxEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "CcEmail", Me.TextboxCcEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "FtpAddress", Me.TextboxFtpAddress)
            Me.PopulateBOProperty(Me.State.MyBO, "TaxId", Me.TextboxTaxId)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceWarrantyDays", Me.TextboxServiceWarrantyDays)
            Me.PopulateBOProperty(Me.State.MyBO, "IntegratedWithID", Me.cboIntegratedWithId)
            'Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", LookupListNew.GetCodeFromId(Me.State.Statusdv, New Guid(Me.cboStatusCode.SelectedValue)))
            Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", (From lst In Me.State.Statusdv
                                                                Where lst.ListItemId = New Guid(Me.cboStatusCode.SelectedValue)
                                                                Select lst.Code).FirstOrDefault())
            Me.PopulateBOProperty(Me.State.MyBO, "BusinessHours", Me.TextboxBusinessHours)
            Me.PopulateBOProperty(Me.State.MyBO, "DefaultToEmailFlag", Me.CheckBoxDefaultToEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "IvaResponsibleFlag", Me.CheckBoxIvaResponsible)
            Me.PopulateBOProperty(Me.State.MyBO, "FreeZoneFlag", Me.CheckBoxFreeZone)
            Me.PopulateBOProperty(Me.State.MyBO, "Comments", Me.TextboxComment)
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)
            Me.PopulateBOProperty(Me.State.MyBO, "ReverseLogisticsId", Me.cboReverseLogisticsId)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                Me.State.MyBO.BankInfoId = Me.State.BankInfoBO.Id
                Me.UserBankInfoCtr.PopulateBOFromControl()
            Else
                Me.State.MyBO.BankInfoId = Nothing
            End If

            'Set the following 2 BO Properties based on whether the Selected Items in 
            'the MASTER CENTER and LOANER CENTER Dropdown Lists are "Nothing Selected" or an actual value
            'If "Nothing Selected", then the corressponding FLAG value should be = "N", else "Y"

            If Me.State.ForEdit = True Then
                Me.State.MyBO.Address.AddressRequiredServCenter = True
            End If

            Me.AddressCtr.PopulateBOFromControl()

            If (Me.State.MyBO.IsDirty) Then
                If AjaxController.IsAutoCompleteEmpty(TextBoxMasterCenter, inpMasterCenterDesc) Then
                    'Nothing selected
                    Me.PopulateBOProperty(Me.State.MyBO, "MasterFlag", "N")
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "MasterFlag", "Y")
                End If

                If AjaxController.IsAutoCompleteEmpty(TextBoxLoanerCenter, inpLoanerCenterDesc) Then
                    'Nothing selected
                    Me.PopulateBOProperty(Me.State.MyBO, "LoanerFlag", "N")
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "LoanerFlag", "Y")
                End If

            End If

            If .MasterFlag = "Y" Then 'Master selected, set pay master flag
                Me.PopulateBOProperty(Me.State.MyBO, "PayMaster", Me.chkPayMaster)
            Else
                .PayMaster = False
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "Shipping", Me.CheckBoxShipping)
            If Me.State.MyBO.Shipping Then
                Me.PopulateBOProperty(Me.State.MyBO, "ProcessingFee", Me.TextboxPROCESSING_FEE)
            Else
                Me.State.MyBO.ProcessingFee = Nothing
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "DistributionMethodId", Me.ddlDistributionMethod)
            Me.PopulateBOProperty(Me.State.MyBO, "FulfillmentTimeZoneId", Me.ddlFulFillingTimeZone)
            Me.PopulateBOProperty(Me.State.MyBO, "DiscountPct", Me.TextBoxDiscountPercent)
            Me.PopulateBOProperty(Me.State.MyBO, "DiscountDays", Me.TextBoxDiscountDays)
            Me.PopulateBOProperty(Me.State.MyBO, "NetDays", Me.TextBoxNetDays)
            Me.PopulateBOProperty(Me.State.MyBO, "PreInvoiceId", Me.ddlPreInvoice)

            Me.State.CurrentPriceListCode = Me.State.MyBO.PriceListCode

            If Me.ddlPriceList.SelectedValue <> LookupListNew.GetIdFromCode(LookupListCache.LK_PRICE_LIST, Me.State.MyBO.PriceListCode).ToString() Then
                Dim strPriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(Me.ddlPriceList.SelectedValue))

                Me.PopulateBOProperty(Me.State.MyBO, "PriceListCode", strPriceListCode)

                Dim oListContext1 As New ListContext
                oListContext1.CountryId = Me.State.MyBO.CountryId
                Dim SVC_PL_Process As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

                Dim SVCPLProcessStatusPending As String = (From lst In SVC_PL_Process
                                                           Where lst.Code = "PENDINGSUBMISSION"
                                                           Select lst.ExtendedCode).FirstOrDefault()

                Dim SVCPLProcessStatusApproved As String = (From lst In SVC_PL_Process
                                                            Where lst.Code = "APPROVED"
                                                            Select lst.ExtendedCode).FirstOrDefault()

                'If Me.State.IsCopy = False And Me.State.IsNew = False Then
                If Not Me.State.MyBO.CurrentSVCPLRecon Is Nothing Then

                    Me.PopulateBOProperty(Me.State.MyBO.CurrentSVCPLRecon, "PriceListId", LookupListNew.GetIdFromCode(LookupListCache.LK_PRICE_LIST, Me.State.MyBO.PriceListCode))

                    If Not Me.State.priceListApprovalflag = String.Empty And Me.State.priceListApprovalflag = "YESNO-Y" Then
                        Me.PopulateBOProperty(Me.State.MyBO.CurrentSVCPLRecon, "Status_xcd", SVCPLProcessStatusPending)
                    Else
                        Me.PopulateBOProperty(Me.State.MyBO.CurrentSVCPLRecon, "Status_xcd", SVCPLProcessStatusApproved)
                    End If

                    Me.PopulateBOProperty(Me.State.MyBO.CurrentSVCPLRecon, "RequestedBy", ElitaPlusIdentity.Current.ActiveUser.NetworkId)

                Else

                    If Not Me.State.priceListApprovalflag = String.Empty And Me.State.priceListApprovalflag = "YESNO-Y" And Not State.MyBO.IsNew Then
                        AddSVRcReconRec(GetSelectedItem(ddlPriceList), SVCPLProcessStatusPending)
                    Else
                        AddSVRcReconRec(GetSelectedItem(ddlPriceList), SVCPLProcessStatusApproved)
                    End If

                End If

                'End If


            End If

            Me.PopulateBOProperty(Me.State.MyBO, "AutoProcessInventoryFileXcd", Me.ddlAutoProcessInventoryFile, False, True)

        End With

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Protected Sub CreateNew()

        'Me.State.MyBO.CurrentSVCPLRecon

        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New ServiceCenter
        Me.State.IsNew = True
        Me.State.SvcPriciListDV = Nothing
        Me.State.MyBO.CurrentSVCPLRecon = Nothing

        ClearMethodOfRepairState()
        PopulateCountry()
        'Me.BindListControlToDataView(Me.ddlPriceList, LookupListNew.GetPriceListLookupList(Me.State.MyBO.CountryId), , , True)
        Dim oListContext1 As New ListContext
        oListContext1.CountryId = Me.State.MyBO.CountryId
        Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
        Me.ddlPriceList.Populate(PriceList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

        ClearPriceListReconState()

        'Me.State.MyBO.CurrentSVCPLRecon = Nothing
        'Me.txtPriceListPending.Text = Nothing
        'Me.txtPriceListPendingStatus.Text = Nothing
        'Me.State.SvcPriciListDV = Nothing

        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.PopulateBOsFromForm()

        Dim newObj As New ServiceCenter
        newObj.Copy(Me.State.MyBO)

        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(Me.State.MyBO.CurrentBankInfo)
            Me.State.BankInfoBO = newObj.CurrentBankInfo
            Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO)
        End If

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Nothing
        Me.State.MyBO.Description = Nothing
        ClearMethodOfRepairState()
        PopulateCountry()

        ClearPriceListReconState()

        'Me.State.MyBO.CurrentSVCPLRecon = Nothing
        'Me.txtPriceListPending.Text = Nothing
        'Me.txtPriceListPendingStatus.Text = Nothing
        'Me.State.SvcPriciListDV = Nothing
        'Me.State.IsCopy = True

        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New ServiceCenter
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                'check the Vendor Quantity records, if they are to be added or updated
                Dim detail As VendorQuantity
                For Each detail In Me.State.MyBO.QuantityChildren
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.populateVendorManagementControls()
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.populateVendorManagementControls()
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#Region "Detail Tabs"

    Sub PopulateUserControlAvailableSelectedManufacturers()
        ' Me.UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableManufacturers()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedManufacturers()
            Me.UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
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
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableDistricts()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedDistricts()
            Me.UserControlAvailableSelectedDistricts.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedDistricts.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDealers()
        ' Me.UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableDealers()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedDealers()
            Me.UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedServiceNetworks()
        'Me.UsercontrolavailableselectedServiceNetworks.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableServiceNetworks()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedServiceNetworks()
            Me.UsercontrolavailableselectedServiceNetworks.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UsercontrolavailableselectedServiceNetworks.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
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

            Me.AddCalendar_New(ibtnEffective, txtEffective, , txtEffective.Text)
            Me.AddCalendar_New(ibtnExpiration, txtExpiration, , txtExpiration.Text)

            Me.UpdateUserControlLookAndFeel()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub UpdateUserControlLookAndFeel()
        CType(moBankInfoController.FindControl("HiddenClassName"), HiddenField).Value = "borderLeft"
    End Sub

    Protected Sub populateVendorManagementControls()
        Try
            ' Populate Attributes
            AttributeValues.DataBind()
            Me.PopulateContacts()
            Me.PopulateQuantity()
            ' Me.PopulateScheduleDetail()
            Me.PopulateSchedule()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Contacts"
    Protected Sub PopulateContacts()
        Try
            Dim dv As ServiceCenter.ContactsView
            dv = Me.State.MyBO.GetContactsSelectionView()
            dv.Sort = Me.State.SortExpressionContactsGrid
            moContactsGridView.DataSource = dv
            Session("recCount") = dv.Count

            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_ID).SortExpression = ServiceCenter.ContactsView.COL_NAME_ID
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_NAME).SortExpression = ServiceCenter.ContactsView.COL_NAME_NAME
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_JOB_TITLE).SortExpression = ServiceCenter.ContactsView.COL_NAME_JOB_TITLE
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_COMPANY_NAME).SortExpression = ServiceCenter.ContactsView.COL_NAME_COMPANY
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_ADDRESS_TYPE).SortExpression = ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_EFFECTIVE_DATE).SortExpression = ServiceCenter.ContactsView.COL_NAME_EFFECTIVE_DATE
            moContactsGridView.Columns(Me.GRID_CONTACTS_COL_EXPIRATION_DATE).SortExpression = ServiceCenter.ContactsView.COL_NAME_EXPIRATION_DATE

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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moContactsGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moContactsGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moContactsGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moContactsGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(Me.GRID_CONTACTS_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ID), Byte())).ToString
                e.Row.Cells(Me.GRID_CONTACTS_COL_NAME).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_NAME).ToString
                e.Row.Cells(Me.GRID_CONTACTS_COL_JOB_TITLE).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_JOB_TITLE).ToString
                e.Row.Cells(Me.GRID_CONTACTS_COL_COMPANY_NAME).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_COMPANY).ToString
                e.Row.Cells(Me.GRID_CONTACTS_COL_EMAIL).Text = dvRow.Row(ServiceCenter.ContactsView.COL_NAME_EMAIL).ToString
                If Not dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID).ToString = Nothing Then
                    Dim addrType As Byte() = CType(dvRow.Row(ServiceCenter.ContactsView.COL_NAME_ADDRESS_TYPE_ID), Byte())

                    If LookupListNew.GetCodeFromId(LookupListNew.GetAddressTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(addrType)) = Codes.METHOD_OF_REPAIR__PICK_UP Then
                        e.Row.Cells(Me.GRID_CONTACTS_COL_ADDRESS_TYPE).Text = TranslationBase.TranslateLabelOrMessage(PICK_UP)
                    ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetAddressTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(addrType)) = Codes.METHOD_OF_REPAIR__SEND_IN Then
                        e.Row.Cells(Me.GRID_CONTACTS_COL_ADDRESS_TYPE).Text = TranslationBase.TranslateLabelOrMessage(SHIPPING)
                    End If
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_CONTACTS_COL_EFFECTIVE_DATE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_CONTACTS_COL_EXPIRATION_DATE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moContactsGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(Me.GRID_CONTACTS_COL_ID).Text)
                Me.State.IsContactsEditing = True
                Me.BeginContactsChildEdit()
                If Not Me.State.myAddressChildBO Is Nothing Then
                    Me.State.myAddressChildBO.CountryId = Me.State.MyBO.CountryId
                    Me.UserControlAddress.Bind(Me.State.myAddressChildBO)
                End If
                If Not Me.State.myContactInfoChildBO Is Nothing Then
                    Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    Me.UserControlContactInfo.Bind(Me.State.myContactInfoChildBO)
                End If
                Me.State.myContactsChildBO.Effective = CType(GetDateFormattedString(CType(Me.State.myContactsChildBO.Effective.ToString, Date)), DateTimeType)
                Me.State.myContactsChildBO.Expiration = CType(GetDateFormattedString(CType(Me.State.myContactsChildBO.Expiration.ToString, Date)), DateTimeType)
                Me.txtEffective.Text = ElitaPlusPage.GetDateFormattedString(CDate(Me.State.myContactsChildBO.Effective))
                Me.txtExpiration.Text = ElitaPlusPage.GetDateFormattedString(CDate(Me.State.myContactsChildBO.Expiration))
                btnNewItemSave.Text = TranslationBase.TranslateLabelOrMessage(UPDATE)
                PopulateContacts()
                Me.EnableDisableFields()
                mdlPopup.Show()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(Me.GRID_CONTACTS_COL_ID).Text)
                Me.State.IsContactsEditing = True
                BeginContactsChildEdit()
                If Not Me.State.myAddressChildBO Is Nothing Then
                    Me.State.myAddressChildBO.CountryId = Me.State.MyBO.CountryId
                    Me.UserControlAddress.Bind(Me.State.myAddressChildBO)
                End If
                If Not Me.State.myContactInfoChildBO Is Nothing Then
                    Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    Me.UserControlContactInfo.Bind(Me.State.myContactInfoChildBO)
                End If
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = SAVE_COMMAND_NAME_CONTCT Then
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = CANCEL_RECORD_NAME_CONTCT Then
                nIndex = CInt(e.CommandArgument)
                Me.State.ContactsSelectedChildId = New Guid(moContactsGridView.Rows(nIndex).Cells(Me.GRID_CONTACTS_COL_ID).Text)
                Me.State.IsContactsEditing = False
                BeginContactsChildEdit()
                EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub populateContactsChildBO()
        With Me.State
            If Not CType(mdlPopup.FindControl("txtEffective"), TextBox).Text.Equals(String.Empty) Then
                .myContactsChildBO.Effective = DateHelper.GetDateValue(CType(mdlPopup.FindControl("txtEffective"), TextBox).Text)
            End If
            If Not CType(mdlPopup.FindControl("txtExpiration"), TextBox).Text.Equals(String.Empty) Then
                .myContactsChildBO.Expiration = DateHelper.GetDateValue(CType(mdlPopup.FindControl("txtExpiration"), TextBox).Text)
            End If
        End With
        UserControlContactInfo.PopulateBOFromControl(True)
        Me.State.myContactsChildBO.Name = Me.State.myContactInfoChildBO.Name
        Me.State.myContactsChildBO.JobTitle = Me.State.myContactInfoChildBO.JobTitle
        Me.State.myContactsChildBO.Company = Me.State.myContactInfoChildBO.Company
        Me.State.myContactsChildBO.Email = Me.State.myContactInfoChildBO.Email
        Me.State.myContactsChildBO.AddressTypeId = Me.State.myContactInfoChildBO.AddressTypeId
    End Sub

    Sub BeginContactsChildEdit()
        Me.State.IsContactsEditing = True
        '#1
        With Me.State
            If Not .ContactsSelectedChildId.Equals(Guid.Empty) Then
                .myContactsChildBO = .MyBO.GeChildContacts(.ContactsSelectedChildId)
            Else
                .myContactsChildBO = .MyBO.GetNewChildContacts
            End If
            .myContactsChildBO.BeginEdit()
        End With
        '#2
        With Me.State
            If Not .myContactsChildBO.ContactInfoId.Equals(Guid.Empty) Then
                .myContactInfoChildBO = .MyBO.GeChildContactInfo(.myContactsChildBO, .myContactsChildBO.ContactInfoId)
            Else
                .myContactInfoChildBO = .MyBO.GetNewChildContactInfo(.myContactsChildBO)
            End If
            .myContactInfoChildBO.BeginEdit()
        End With
        '#3
        With Me.State
            If Not .ContactsSelectedChildId.Equals(Guid.Empty) Then
                .myAddressChildBO = .MyBO.GeChildAddress(.myContactInfoChildBO, .myContactInfoChildBO.AddressId)
            Else
                .myAddressChildBO = .MyBO.GetNewChildAddress(.myContactInfoChildBO)
            End If
            .myAddressChildBO.BeginEdit()
        End With
    End Sub

    Sub EndContactsChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
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
            Me.State.IsContactsEditing = False
            Me.EnableDisableFields()
            Me.populateVendorManagementControls()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            mdlPopup.Show()
        End Try
    End Sub

    Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemCancel.Click
        Try
            Me.mdlPopup.Hide()
            Me.EndContactsChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            Me.State.ActionInProgress = DetailPageCommand.Accept
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewItemSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemSave.Click
        Try
            Me.populateContactsChildBO()
            Me.EndContactsChildEdit(DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewContactInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewContactInfo.Click
        Try
            Me.State.ContactsSelectedChildId = Guid.Empty
            Me.State.IsContactsEditing = True
            Me.BeginContactsChildEdit()
            If Not Me.State.myAddressChildBO Is Nothing Then
                Me.State.myAddressChildBO.CountryId = Me.State.MyBO.CountryId
                Me.UserControlAddress.Bind(Me.State.myAddressChildBO)
            End If
            If Not Me.State.myContactInfoChildBO Is Nothing Then
                Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Me.UserControlContactInfo.Bind(Me.State.myContactInfoChildBO)
            End If
            Me.txtEffective.Text = String.Empty
            Me.txtExpiration.Text = String.Empty
            btnNewItemSave.Text = TranslationBase.TranslateLabelOrMessage("SAVE")
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Quantity"

    Protected Sub PopulateQuantity()

        Try
            Dim dv As ServiceCenter.QuantityView
            dv = Me.State.MyBO.GetQuantitySelectionView()
            dv.Sort = Me.State.SortExpressionQuantityGrid
            moQuantityGridView.DataSource = dv
            Session("recCount") = dv.Count

            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_ID).SortExpression = ServiceCenter.QuantityView.COL_NAME_ID
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_EQUIPMENT_TYPE).SortExpression = ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_MANUFACTURER).SortExpression = ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_ID
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_MODEL).SortExpression = ServiceCenter.QuantityView.COL_NAME_JOB_MODEL
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_SKU).SortExpression = ServiceCenter.QuantityView.COL_NAME_VENDOR_SKU
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_SKU_DESC).SortExpression = ServiceCenter.QuantityView.COL_NAME_VENDOR_SKU_DESCRIPTION
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_QUANTITY).SortExpression = ServiceCenter.QuantityView.COL_NAME_QUANTITY
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_PRICE).SortExpression = ServiceCenter.QuantityView.COL_NAME_PRICE
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_CONDITION).SortExpression = ServiceCenter.QuantityView.COL_NAME_CONDITION_ID
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_EFFECTIVE).SortExpression = ServiceCenter.QuantityView.COL_NAME_EFFECTIVE
            moQuantityGridView.Columns(Me.GRID_QUANTITY_COL_EXPIRATION).SortExpression = ServiceCenter.QuantityView.COL_NAME_EXPIRATION

            If Me.State.IsQuantityEditing Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.QuantitySelectedChildId, moQuantityGridView,
                                        moQuantityGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Me.State.QuantitySelectedChildId, Me.moQuantityGridView, Me.State.QuantityDetailPageIndex)
                Me.State.QuantityDetailPageIndex = Me.moQuantityGridView.PageIndex
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moQuantityGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moQuantityGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moQuantityGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moQuantityGridView.RowDataBound
        Dim EquipmentTypeId As Guid
        Dim ManufacturerId As Guid
        Dim ConditionId As Guid

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(Me.GRID_QUANTITY_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_ID), Byte())).ToString
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID)) Then
                    EquipmentTypeId = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EQUIPMENT_TYPE_ID), Byte()))
                    e.Row.Cells(Me.GRID_QUANTITY_COL_EQUIPMENT_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), EquipmentTypeId)
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_NAME)) Then
                    e.Row.Cells(Me.GRID_QUANTITY_COL_MANUFACTURER).Text = dvRow.Row(ServiceCenter.QuantityView.COL_NAME_MANUFACTURER_NAME).ToString()
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_CONDITION_ID)) Then
                    ConditionId = New Guid(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_CONDITION_ID), Byte()))
                    e.Row.Cells(Me.GRID_QUANTITY_COL_CONDITION).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetConditionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), ManufacturerId)
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE)) Then
                    e.Row.Cells(Me.GRID_QUANTITY_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not DBNull.Value.Equals(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION)) Then
                    e.Row.Cells(Me.GRID_QUANTITY_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
                'check if any row of the grid is in Editable mode, if yes, then disbale the edit icon from other rows
                If (Me.State.IsQuantityEditing) Then
                    SetQuantityGridRowControls(e.Row, False, False, False)
                Else
                    SetQuantityGridRowControls(e.Row, True, False, False)
                End If
                If ((e.Row.RowState And DataControlRowState.Edit) > 0) Then
                    SetQuantityGridRowControls(e.Row, False, True, True)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub SetQuantityGridRowControls(ByVal gridrow As GridViewRow, ByVal enableEdit As Boolean, ByVal enableCancel As Boolean, ByVal enableSave As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim cancel As LinkButton
        Dim save As Button
        Const EDIT_COL As Integer = 11

        edt = CType(gridrow.Cells(EDIT_COL).FindControl("QuantityEditButton_WRITE"), ImageButton)
        If Not edt Is Nothing Then
            edt.Enabled = enableEdit
            edt.Visible = enableEdit
        End If
        cancel = CType(gridrow.Cells(EDIT_COL).FindControl("QuantityCancelButton_WRITE"), LinkButton)
        If Not edt Is Nothing Then
            cancel.Enabled = enableCancel
            cancel.Visible = enableCancel
        End If
        save = CType(gridrow.Cells(EDIT_COL).FindControl("QuantitySaveButton_WRITE"), Button)
        If Not save Is Nothing Then
            save.Enabled = enableSave
            save.Visible = enableSave
        End If
    End Sub

    Protected Sub moQuantityGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.QuantitySelectedChildId = New Guid(moQuantityGridView.Rows(nIndex).Cells(Me.GRID_QUANTITY_COL_ID).Text)
                Me.State.IsQuantityEditing = True
                Me.BeginQuantityChildEdit()
                PopulateQuantity()
                Me.PopulateFromQuantityChildBO()
                ElitaPlusSearchPage.SetGridControls(moQuantityGridView, False)
                Me.EnableDisableFields()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.QuantitySelectedChildId = New Guid(moQuantityGridView.Rows(nIndex).Cells(Me.GRID_QUANTITY_COL_ID).Text)
                Me.State.IsQuantityEditing = True
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateFromQuantityChildBO()
        If moQuantityGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moQuantityGridView.Rows(moQuantityGridView.EditIndex)
        Dim txtCommon As TextBox

        With Me.State.myQuantityChildBO
            txtCommon = CType(gRow.Cells(Me.GRID_QUANTITY_COL_QUANTITY).FindControl(GRID_CONTROL_QUANTITY), TextBox)
            If Not txtCommon Is Nothing Then
                Me.PopulateControlFromBOProperty(txtCommon, .Quantity)
            End If
        End With
    End Sub

    Public Sub populateQuantityChildBO()
        If moQuantityGridView.EditIndex = -1 Then Exit Sub
        Dim gRow As GridViewRow = moQuantityGridView.Rows(moQuantityGridView.EditIndex)
        Dim txtCommon As TextBox

        With Me.State.myQuantityChildBO
            txtCommon = CType(gRow.Cells(Me.GRID_QUANTITY_COL_QUANTITY).FindControl(GRID_CONTROL_QUANTITY), TextBox)
            If Not txtCommon Is Nothing Then
                .Quantity = CType(txtCommon.Text, LongType)
            End If
        End With
    End Sub

    Sub BeginQuantityChildEdit()
        Me.State.IsQuantityEditing = True
        With Me.State
            If Not .QuantitySelectedChildId.Equals(Guid.Empty) Then
                .myQuantityChildBO = .MyBO.GeChildQuantity(.QuantitySelectedChildId)
            Else
                .myQuantityChildBO = .MyBO.GetNewChildQuantity
            End If
            .myQuantityChildBO.BeginEdit()
        End With
        populateQuantityChildBO()
    End Sub

    Sub EndQuantityChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
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
            Me.State.IsQuantityEditing = False
            Me.EnableDisableFields()
            Me.populateVendorManagementControls()
            'Me.PopulateQuantity()
            'Me.PopulateSchedule()
            'Me.PopulateContacts()
            'Me.populateAttributes()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Schedule"

    Protected Sub PopulateScheduleDetail()
        Try
            Dim dv As ServiceCenter.ScheduleView
            dv = Me.State.MyBO.GetScheduleSelectionView()
            dv.Sort = Me.State.SortExpressionScheduleGrid
            moScheduleGridView.DataSource = dv
            Session("recCount") = dv.Count

            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_ID).SortExpression = ServiceCenter.ScheduleView.COL_NAME_ID
            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID
            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID
            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SCHEDULE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID
            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_EFFECTIVE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EFFECTIVE
            moScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_EXPIRATION).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EXPIRATION

            If Me.State.IsScheduleEditing Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.ScheduleSelectedChildId, moScheduleGridView,
                                        moScheduleGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Me.State.ScheduleSelectedChildId, Me.moScheduleGridView, Me.State.ScheduleDetailPageIndex)
                Me.State.ScheduleDetailPageIndex = Me.moScheduleGridView.PageIndex
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub PopulateSchedule()
        Try
            Dim dv As ServiceCenter.ScheduleView
            dv = Me.State.MyBO.GetScheduleSelectionView()
            dv.Sort = Me.State.SortExpressionScheduleGrid
            moAddScheduleGridView.DataSource = dv
            Session("recCount") = dv.Count

            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_ID).SortExpression = ServiceCenter.ScheduleView.COL_NAME_ID
            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID
            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID
            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_SCHEDULE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID
            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_EFFECTIVE).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EFFECTIVE
            moAddScheduleGridView.Columns(Me.GRID_SCHEDULE_COL_EXPIRATION).SortExpression = ServiceCenter.ScheduleView.COL_NAME_EXPIRATION

            If Me.State.IsScheduleEditing Then
                Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.ScheduleSelectedChildId, moAddScheduleGridView,
                                        moAddScheduleGridView.PageIndex, True)
            Else
                SetPageAndSelectedIndexFromGuid(dv, Me.State.ScheduleSelectedChildId, Me.moAddScheduleGridView, Me.State.ScheduleDetailPageIndex)
                Me.State.ScheduleDetailPageIndex = Me.moAddScheduleGridView.PageIndex
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub moScheduleGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moScheduleGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_PageIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles moAddScheduleGridView.PageIndexChanged
        Try
            Me.State.PageIndex = moAddScheduleGridView.PageIndex
            Me.State.ScheduleSelectedChildId = Guid.Empty
            Me.PopulateSchedule()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moAddScheduleGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moScheduleGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moScheduleGridView.RowDataBound
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
                    e.Row.Cells(Me.GRID_SCHEDULE_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_ID), Byte())).ToString
                    serviceClassId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceClassId)
                    serviceTypeId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceTypeId)
                    scheduleId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID), Byte()))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                End If
                If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                    e.Row.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub moScheduleGridView_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moScheduleGridView.PageIndexChanged
        Try
            Me.State.PageIndex = moScheduleGridView.PageIndex
            Me.State.ScheduleSelectedChildId = Guid.Empty
            Me.PopulateScheduleDetail()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moScheduleGridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moScheduleGridView.PageIndexChanging
        Try
            moScheduleGridView.PageIndex = e.NewPageIndex
            State.ScheduleDetailPageIndex = moScheduleGridView.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moAddScheduleGridView.PageIndexChanging
        Try
            moAddScheduleGridView.PageIndex = e.NewPageIndex
            State.ScheduleDetailPageIndex = moAddScheduleGridView.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moAddScheduleGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moAddScheduleGridView.RowDataBound
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
                    If Not ddlServiceClass Is Nothing Then
                        'Me.BindListControlToDataView(ddlServiceClass, LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlServiceClass.Populate(ServiceClassList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If Not ddlServiceType Is Nothing Then
                        'Me.BindListControlToDataView(ddlServiceType, LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlServiceType.Populate(ServiceClassTypeList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If Not ddlDayofWeek Is Nothing Then
                        'Me.BindListControlToDataView(ddlDayofWeek, LookupListNew.GetDayOfWeekLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                        ddlDayofWeek.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="DAYS_OF_WEEK", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    If Not ddlSchedule Is Nothing Then
                        'Me.BindListControlToDataView(ddlSchedule, LookupListNew.GetScheduleList(), , , False)
                        ddlSchedule.Populate(ScheduleList, New PopulateOptions() With
                            {
                                .AddBlankItem = False
                            })
                    End If
                    SetScheduleGridRowControls(e.Row, False, True, True)
                Else
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID).ToString = Nothing Then
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_ID).Text = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_ID), Byte())).ToString
                        serviceClassId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_CLASS_ID), Byte()))
                    End If
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID).ToString = Nothing Then
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceClassId)
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).Text = (From lst In ServiceClassList
                                                                                Where lst.ListItemId = serviceClassId
                                                                                Select lst.Translation).FirstOrDefault()
                        serviceTypeId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SERVICE_TYPE_ID), Byte()))
                    End If
                    If Not dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID).ToString = Nothing Then
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), serviceTypeId)
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).Text = (From lst In ServiceClassTypeList
                                                                               Where lst.ListItemId = serviceTypeId
                                                                               Select lst.Translation).FirstOrDefault()
                        scheduleId = New Guid(CType(dvRow.Row(ServiceCenter.ScheduleView.COL_NAME_SCHEDULE_ID), Byte()))
                        'e.Row.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetScheduleList(), scheduleId)
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).Text = (From lst In ScheduleList
                                                                           Where lst.ListItemId = scheduleId
                                                                           Select lst.Translation).FirstOrDefault()
                    End If
                    If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE).ToString = Nothing Then
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EFFECTIVE), Date))
                    End If
                    If Not dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION).ToString = Nothing Then
                        e.Row.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).Text = GetDateFormattedString(CType(dvRow.Row(ServiceCenter.QuantityView.COL_NAME_EXPIRATION), Date))
                    End If
                    If (Me.State.IsScheduleEditing) Then ' If Grid is in edit mode, hide buttons if this row's rowstate is not edit. As for edited row it is setting info. In above part  
                        SetScheduleGridRowControls(e.Row, False, False, False)
                    Else
                        SetScheduleGridRowControls(e.Row, True, False, False)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub SetScheduleGridRowControls(ByVal gridrow As GridViewRow, ByVal enableEdit As Boolean, ByVal enableCancel As Boolean, ByVal enableSave As Boolean)
        Dim edt As ImageButton
        Dim cancel As LinkButton
        Dim save As Button
        Dim del As ImageButton
        Const EDIT_COL As Integer = 9

        edt = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleEditButton_WRITE"), ImageButton)
        If Not edt Is Nothing Then
            edt.Enabled = enableEdit
            edt.Visible = enableEdit
        End If
        cancel = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleCancelButton_WRITE"), LinkButton)
        If Not edt Is Nothing Then
            cancel.Enabled = enableCancel
            cancel.Visible = enableCancel
        End If
        save = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleSaveButton_WRITE"), Button)
        If Not save Is Nothing Then
            save.Enabled = enableSave
            save.Visible = enableSave
        End If
        del = CType(gridrow.Cells(EDIT_COL).FindControl("ScheduleDeleteButton_WRITE"), ImageButton)
        If Not del Is Nothing Then
            del.Enabled = enableEdit
            del.Visible = enableEdit
        End If

    End Sub

    Protected Sub moAddScheduleGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.moAddScheduleGridView.EditIndex = nIndex
                Me.State.ScheduleSelectedChildId = New Guid(moAddScheduleGridView.Rows(nIndex).Cells(Me.GRID_SCHEDULE_COL_ID).Text)
                Me.State.IsScheduleEditing = True
                Me.BeginScheduleChildEdit()
                Me.PopulateSchedule()
                Me.PopulateFromScheduleChildBO()
                Me.EnableDisableFields()
                Me.EnableDisableTabs(False)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.moAddScheduleGridView.EditIndex = nIndex
                Me.State.ScheduleSelectedChildId = New Guid(moAddScheduleGridView.Rows(nIndex).Cells(Me.GRID_SCHEDULE_COL_ID).Text)
                Me.State.IsScheduleEditing = True
                Me.BeginScheduleChildEdit()
                Me.PopulateFromScheduleChildBO()
                Me.populateScheduleChildBO()
                Me.EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = SAVE_COMMAND_NAME_SCHDL Then
                Me.populateScheduleChildBO()
                Me.EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = CANCEL_RECORD_NAME_SCHDL Then
                Me.moAddScheduleGridView.EditIndex = -1
                Me.EndScheduleChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Function IsDateValid() As Boolean
        Dim gRow As GridViewRow = moScheduleGridView.Rows(moScheduleGridView.EditIndex)
        Dim strFromTime As String
        Dim strToTime As String
        Dim strEffective As String
        Dim strExpiration As String

        Try
            strFromTime = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_FROM).FindControl(GRID_CONTROL_FROM), TextBox).Text
            strToTime = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_DAY).FindControl(GRID_CONTROL_TO), TextBox).Text
            strEffective = GetDateFormattedString(CType(CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox).Text.ToString, Date))
            strExpiration = GetDateFormattedString(CType(CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox).Text.ToString, Date))

            If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX,
                                                            System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}",
                                       TranslationBase.TranslateLabelOrMessage("FROM_TIME"),
                                       TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                Exit Function
            End If
            If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX,
                                                            System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}",
                                       TranslationBase.TranslateLabelOrMessage("TO_TIME"),
                                       TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                Exit Function
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        With Me.State.myScheduleChildBO
            ddlSchedule = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList)
            If Not ddlSchedule Is Nothing Then
                BindSelectItem(.ScheduleId.ToString, ddlSchedule)
            End If
            ddlServiceType = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
            If Not ddlServiceType Is Nothing Then
                BindSelectItem(.ServiceTypeId.ToString, ddlServiceType)
            End If
            ddlServiceClass = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
            If Not ddlServiceClass Is Nothing Then
                BindSelectItem(.ServiceClassId.ToString, ddlServiceClass)
            End If
            txtEffective = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
            If Not txtEffective Is Nothing And Not .Effective Is Nothing Then
                txtEffective.Text = GetDateFormattedString(CType(.Effective.ToString, Date))
            End If
            txtExpiration = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
            If Not txtExpiration Is Nothing And Not .Expiration Is Nothing Then
                txtExpiration.Text = GetDateFormattedString(CType(.Expiration.ToString, Date))
            End If

            btn = DirectCast(gRow.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).FindControl("btnEffectiveDate"), ImageButton)
            btn1 = DirectCast(gRow.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).FindControl("btnExpirationDate"), ImageButton)

            Dim txtcommon As TextBox
            txtcommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).FindControl(Me.GRID_CONTROL_EFFECTIVE), TextBox)
            If Not txtcommon Is Nothing Then
                Me.AddCalendar_New(btn, txtcommon, , txtcommon.Text)
            End If

            txtcommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).FindControl(Me.GRID_CONTROL_EXPIRATION), TextBox)
            If Not txtcommon Is Nothing Then
                Me.AddCalendar_New(btn1, txtcommon, , txtcommon.Text)
            End If
        End With

    End Sub

    Public Function GetScheduleDetailCount(ByVal scheduleId As Guid) As Integer
        Return Me.State.MyBO.GetScheduleDetailCount(scheduleId)
    End Function

    Public Function GetScheduleDetail(ByVal scheduleId As Guid) As DataSet
        Return Me.State.MyBO.GetScheduleDetailInfo(scheduleId)
    End Function

    Protected Function GetScheduleInfo(ByVal scheduleId As Guid) As DataSet
        Return Me.State.MyBO.GetScheduleInfo(scheduleId)
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

        If Not CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList) Is Nothing Then
            schId = New Guid(CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList).SelectedValue)
            cnt = GetScheduleDetailCount(schId)
            dsSchedule = GetScheduleInfo(schId)
            dsScheduleDetail = GetScheduleDetail(schId)
        End If

        For i As Integer = 0 To cnt - 1
            With Me.State.myScheduleChildBO
                ddlCommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SERVICE_CLASS).FindControl(GRID_CONTROL_SERVICE_CLASS), DropDownList)
                If Not ddlCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.myScheduleChildBO, "ServiceClassId", ddlCommon)
                End If
                ddlCommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SERVICE_TYPE).FindControl(GRID_CONTROL_SERVICE_TYPE), DropDownList)
                If Not ddlCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.myScheduleChildBO, "ServiceTypeId", ddlCommon)
                End If
                ddlCommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_SCHEDULE).FindControl(GRID_CONTROL_SCHEDULE), DropDownList)
                If Not ddlCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.myScheduleChildBO, "ScheduleId", ddlCommon)
                End If
                txtCommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EFFECTIVE).FindControl(GRID_CONTROL_EFFECTIVE), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.myScheduleChildBO, "Effective", txtCommon)
                End If
                txtCommon = CType(gRow.Cells(Me.GRID_SCHEDULE_COL_EXPIRATION).FindControl(GRID_CONTROL_EXPIRATION), TextBox)
                If Not txtCommon Is Nothing Then
                    Me.PopulateBOProperty(Me.State.myScheduleChildBO, "Expiration", txtCommon)
                End If
            End With
            If Not dsSchedule Is Nothing Then
                With Me.State.myScheduleTableChildBO
                    .Code = Convert.ToString(dsSchedule.Tables(0).Rows(0)("code"))
                    .Description = Convert.ToString(dsSchedule.Tables(0).Rows(0)("description"))
                End With
            End If
            If cnt > 0 Then
                With Me.State.myScheduleDetailChildBO
                    .ScheduleId = Me.State.myScheduleChildBO.ScheduleId
                    .DayOfWeekId = New Guid(CType(dsScheduleDetail.Tables(0).Rows(i)("day_of_week_id"), Byte()))
                    .FromTime = CType(dsScheduleDetail.Tables(0).Rows(i)("from_time"), DateTime)
                    .ToTime = CType(dsScheduleDetail.Tables(0).Rows(i)("to_time"), DateTime)
                End With
            Else
                With Me.State.myScheduleDetailChildBO
                    .ScheduleId = Me.State.myScheduleChildBO.ScheduleId
                    .DayOfWeekId = Guid.Empty
                    .FromTime = CType("00:00 AM", DateTime)
                    .ToTime = CType("00:00 AM", DateTime)
                End With
            End If
        Next i
    End Sub

    Sub BeginScheduleChildEdit()
        Me.State.IsScheduleEditing = True
        With Me.State
            If Not .ScheduleSelectedChildId.Equals(Guid.Empty) Then
                .myScheduleChildBO = .MyBO.GeChildSchedule(.ScheduleSelectedChildId)
            Else
                .myScheduleChildBO = .MyBO.GetNewChildSchedule
            End If
            .myScheduleChildBO.BeginEdit()
        End With
        With Me.State
            If Not .myScheduleChildBO.ScheduleId.Equals(Guid.Empty) Then
                .myScheduleTableChildBO = .MyBO.GeChildScheduleTable(.myScheduleChildBO, .myScheduleChildBO.ScheduleId)
            Else
                .myScheduleTableChildBO = .MyBO.GetNewChildScheduleTable(.myScheduleChildBO)
            End If
            .myScheduleTableChildBO.BeginEdit()
        End With
        With Me.State
            If Not String.IsNullOrEmpty(.myScheduleTableChildBO.Code) Then
                .myScheduleDetailChildBO = .MyBO.GeChildScheduleDetail(.myScheduleTableChildBO, .myScheduleTableChildBO.Id, .myScheduleChildBO)
            Else
                .myScheduleDetailChildBO = .MyBO.GetNewChildScheduleDetail(.myScheduleTableChildBO, .myScheduleChildBO)
            End If
            .myScheduleDetailChildBO.BeginEdit()
        End With

        populateScheduleChildBO()
    End Sub

    Sub EndScheduleChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
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
            Me.State.IsScheduleEditing = False
            Me.EnableDisableFields()
        Catch ex As Exception
            Throw
        Finally
            Me.populateVendorManagementControls()
        End Try
    End Sub

    Private Sub btnNewSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewSchedule.Click
        Try
            Me.divScheduleDetails.Visible = True
            Me.divAddNewSchedule.Visible = False
            Me.PopulateSchedule()
            btnNewSchedueInfo_Click(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewSchedueInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewSchedule.Click
        Try
            Me.State.ScheduleSelectedChildId = Guid.Empty
            Me.State.IsScheduleEditing = True
            Me.BeginScheduleChildEdit()
            Me.State.ScheduleSelectedChildId = Me.State.myScheduleChildBO.Id
            Me.PopulateSchedule()
            PopulateFromScheduleChildBO()
            Me.divScheduleDetails.Visible = True
            Me.divAddNewSchedule.Visible = False
            btnAddNewSchedule.Enabled = False
            EnableDisableTabs(False) 'As Row is edited in the Grid so Disable Other tab. Enable it back when Save or Cancel in  Grid is clicked
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCancelNewSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelNewSchedule.Click
        Try
            Me.divScheduleDetails.Visible = False
            Me.divAddNewSchedule.Visible = True
            Me.PopulateScheduleDetail()
            Me.State.ActionInProgress = DetailPageCommand.Accept
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

    Protected Sub cboPaymentMethodId_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
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

    Protected Sub CheckBoxShipping_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxShipping.CheckedChanged
        If CheckBoxShipping.Checked Then
            ControlMgr.SetVisibleControl(Me, LabelProcessingFee, True)
            ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, True)
        Else
            ControlMgr.SetVisibleControl(Me, LabelProcessingFee, False)
            ControlMgr.SetVisibleControl(Me, TextboxPROCESSING_FEE, False)
        End If
    End Sub

    Protected Sub IsPriceListCodeValid()
        Dim Statusdv As DataView = LookupListNew.GetPriceList(Me.State.MyBO.CountryId)
        'Check if the selected price code is in the active list
        Dim strPriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(Me.ddlPriceList.SelectedValue))
        If (String.IsNullOrEmpty(strPriceListCode)) Then
            Me.moMessageController.AddWarning(String.Format("{0}",
            TranslationBase.TranslateLabelOrMessage("PRICELISTCODE_IS_EXPIRED"), False))
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
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

        If Not Me.CallingParameters Is Nothing Then
            Select Case SelectedTabIndex
                Case TAB_CONTACT ' Contacts 
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    Me.PopulateQuantity()
                    Me.PopulateSchedule()
                Case TAB_ATTRIBUTE ' Attribute Tab 
                    Me.PopulateContacts()
                    Me.PopulateQuantity()
                    Me.PopulateSchedule()
                Case TAB_QUANTITY  ' Quantity tab
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    Me.PopulateContacts()
                    Me.PopulateSchedule()
                Case TAB_SCHEDULE ' Schedule Tab 
                    ' Populate Attributes
                    AttributeValues.DataBind()
                    Me.PopulateContacts()
                    Me.PopulateQuantity()
            End Select
        End If
    End Sub

    Private Sub EnableDisableTabs(ByVal blnFlag As Boolean)
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

    Private Sub EnableTab(ByVal tabInd As Integer, ByVal blnFlag As Boolean)
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

    Private Sub GridViewMethodOfRepair_RowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewMethodOfRepair.RowCommand
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
    Private Sub GridViewMethodOfRepair_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewMethodOfRepair.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As ServCenterMethRepair

            If Not e.Row.DataItem Is Nothing Then
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
                        AndAlso Not dvRow.ServiceWarrantyDays Is Nothing Then
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
    Private Sub PopulateMethodOfRepairGrid(ByVal ds As Collections.Generic.List(Of ServCenterMethRepair))
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
            For Each scMrBO As ServCenterMethRepair In Me.State.MyBO.ServiceCenterMethoOfRepairsChildren
                SelectedMethodOfRepairList = (From lst In SelectedMethodOfRepairList
                                              Where lst.ListItemId <> scMrBO.ServCenterMorId
                                              Select lst).ToArray()
            Next
            'BindListControlToDataView(ddlMethodOfRepairAvailable, availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, False)
            Me.ddlMethodOfRepairAvailable.Populate(SelectedMethodOfRepairList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = False
                })
        End If
    End Sub
    Private Sub btnAddMethodOfRepair_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddMethodOfRepair.Click
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

        If Me.ErrCollection.Count > 0 Then
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
        If Not State.MethodOfRepairWorkingItem.IsNew And Not State.MyBO.IsNew Then
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

    Private Sub SaveAllRecordsMethodOfRepair(ByVal blnNewBo As Boolean)
        ' new BO, save Depreciation Schedule records in memory
        If blnNewBo Then
            If (Not State.MethodOfRepairList Is Nothing) AndAlso State.MethodOfRepairList.Count > 0 Then
                Dim i As Integer
                For i = 0 To State.MethodOfRepairList.Count - 1
                    State.MethodOfRepairList.Item(i).ServiceCenterId = Me.State.MyBO.Id
                    State.MethodOfRepairList.Item(i).SaveWithoutCheckDsCreator()
                Next
                State.MyBO.MethodOfRepairCount = State.MethodOfRepairList.Count
                State.MethodOfRepairList = Nothing
            Else
                State.MyBO.MethodOfRepairCount = 0
            End If
        End If
    End Sub
    Private Sub EnableDisableForMethodOfRepair(ByVal blnFlag As Boolean)

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
        If Not State.MethodOfRepairWorkingItem Is Nothing Then
            BindBOPropertyToGridHeader(State.MethodOfRepairWorkingItem, "ServiceWarrantyDays", GridViewMethodOfRepair.Columns(MorGridColServiceWarrantyDays))
        End If
    End Sub
#End Region

#Region " Price List Recon Related "

#Region "Button event for upadating the price list update"
    Private Sub btnSubmitApproval_Click(sender As Object, e As EventArgs) Handles btnSubmitApproval.Click

        Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL
        Me.State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        SavePriceAuthRecord(False)
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_APPROVED
            Me.State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId

            Dim oListContext1 As New ListContext
            oListContext1.CountryId = Me.State.MyBO.CountryId
            Dim PriceList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            Dim PriceListCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, Me.State.MyBO.CurrentSVCPLRecon.PriceListId)
            Me.State.MyBO.PriceListCode = PriceListCode
            SavePriceAuthRecord(True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_REJECTED '"SVC_PL_RECON_PROCESS"
        Me.State.MyBO.CurrentSVCPLRecon.StatusChangedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        SavePriceAuthRecord(True)
    End Sub

    Private Sub SavePriceAuthRecord(ByVal refreshSVCPLReconBO As Boolean)
        Try
            Me.State.MyBO.Save()
            If refreshSVCPLReconBO = True Then
                Me.State.MyBO.CurrentSVCPLRecon = Nothing
            End If
            Me.PopulateFormFromBOs(Me.CMD_SAVE)
            Me.EnableDisableFields()
            EnableDisableSVCPLControls()


        Catch ex As Exception
            'Me.State.MySvcBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Throw ex
        End Try

        'Me.State.PageIndex = Grid.PageIndex
        Me.State.SvcPriciListDV = Nothing
        Me.InitializePage()
        'PopulateGrid_PriceList()
        PopulateSvcDataGridPriceList()
        'Me.State.PageIndex = DataGridPriceList.CurrentPageIndex
    End Sub

#End Region
#Region "Style"

    Public Overloads Sub SetGridItemStyleColor(ByVal oDataGrid As DataGrid)
        oDataGrid.SelectedItemStyle.BackColor = Color.LightSteelBlue
        oDataGrid.EditItemStyle.BackColor = Color.LightSteelBlue
        'oDataGrid.SelectedItemStyle.ForeColor = Color.LimeGreen
        'oDataGrid.HeaderStyle.ForeColor = Color.Magenta
    End Sub

    Public Overloads Sub SetGridItemStyleColor(ByVal oDataGrid As GridView)
        oDataGrid.SelectedRowStyle.BackColor = Color.LightSteelBlue
        oDataGrid.EditRowStyle.BackColor = Color.LightSteelBlue
        'oDataGrid.SelectedItemStyle.ForeColor = Color.LimeGreen
        'oDataGrid.HeaderStyle.ForeColor = Color.Magenta
    End Sub

    Public Overloads Shared Sub ClearGridHeaders(ByVal oDataGrid As DataGrid)
        Dim oGridCol As DataGridColumn

        For Each oGridCol In oDataGrid.Columns
            oGridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub

    Public Overloads Shared Sub ClearGridHeaders(ByVal oDataGrid As GridView)
        Dim oGridCol As DataControlField

        For Each oGridCol In oDataGrid.Columns
            oGridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub
#End Region

#Region "New DataGrid"
    Private Sub InitializePage()
        Me.SetGridItemStyleColor(Me.DataGridPriceList)

        Me.LoadSvcPriceListReconDetails()

        If Me.State.IsGridVisible Then

            DataGridPriceList.PageIndex = NewCurrentPageIndex(DataGridPriceList, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.PageSize = DataGridPriceList.PageSize
            'cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
            'Grid.PageSize = Me.State.PageSize
        End If

        Me.SetGridItemStyleColor(Me.DataGridPriceList)
        Me.MenuEnabled = True
    End Sub

    Private Sub LoadSvcPriceListReconDetails()
        Try
            Me.State.PageIndex = 0
            Me.State.selectedSvcPriceListReconId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.SvcPriciListDV = Nothing
            'Me.PopulateSvcDataGridPriceList()

            'If (Not State.SvcPriciListDV Is Nothing) AndAlso State.SvcPriciListDV.Count > 0 Then
            '    Dim guidCI As Guid = New Guid(CType(State.SvcPriciListDV.Item(0)(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID), Byte()))
            'End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Function GetSvcDV() As DataView

        Me.State.SvcPriciListDV = GetDataGridDataView()
        Me.State.SvcPriciListDV.Sort = DataGridPriceList.DataMember()

        Return (Me.State.SvcPriciListDV)

    End Function

    Private Function GetDataGridDataView() As DataView

        With State
            'Return (SvcPriceListRecon.LoadList(Me.State.MyBO.Code, Me.State.MyBO.PriceListCode, Me.State.MyBO.CountryId))
            'Return (SvcPriceListRecon.LoadListBySvc(Me.State.MyBO.Id))
            Return (SvcPriceListRecon.LoadListBySvc(Me.State.MyBO.Id))
        End With

    End Function
    Private Sub PopulateSvcDataGridPriceList()
        Dim foundLabel As String

        Try

            If (Me.State.SvcPriciListDV Is Nothing) Then
                Me.State.SvcPriciListDV = GetSvcDV()
            End If

            Me.DataGridPriceList.DataSource = Me.State.SvcPriciListDV
            Me.DataGridPriceList.AllowSorting = False
            'Me.DataGridPriceList.DataBind()
            Session("PLrecCount") = Me.State.SvcPriciListDV.Count

            'DataGridPriceList.Columns(Me.SVC_PRICE_LIST_RECON_ID_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID
            DataGridPriceList.Columns(Me.SVC_PRICE_LIST_ID_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID
            DataGridPriceList.Columns(Me.SVC_REQUESTED_BY_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE
            DataGridPriceList.Columns(Me.SVC_STATUS_XCD_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD
            DataGridPriceList.Columns(Me.SVC_STATUS_MODIFIED_by_COL).SortExpression = SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_BY

            If Me.State.SvcPriciListDV.Count > 0 Then
                DataGridPriceList.DataSource = Me.State.SvcPriciListDV
                DataGridPriceList.AutoGenerateColumns = False
                DataGridPriceList.DataBind()
                EnableDisableSVCPLControls()
            Else
                Me.State.SvcPriciListDV.AddNew()
                Me.State.SvcPriciListDV(0)(0) = Guid.Empty.ToByteArray
                DataGridPriceList.DataSource = Me.State.SvcPriciListDV
                DataGridPriceList.DataBind()
                DataGridPriceList.Rows(0).Visible = False
                DataGridPriceList.Rows(0).Controls.Clear()
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
                ControlMgr.SetEnableControl(Me, btnApprove, False)
                ControlMgr.SetEnableControl(Me, btnReject, False)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#Region " Datagrid Related "

    'The Binding Logic is here
    Private Sub DataGridPriceList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridPriceList.RowDataBound

        'Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        'Dim chkRow As CheckBox
        Dim oListContext1 As New ListContext
        oListContext1.CountryId = Me.State.MyBO.CountryId
        Dim PriceList As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

        Dim SVC_PL_Process As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_PL_RECON_PROCESS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)

        If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
            'If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

            If dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID) IsNot Nothing Then
                e.Row.Cells(Me.SVC_PRICE_LIST_RECON_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_SVC_PRICE_LIST_RECON_ID), Byte()))
            End If


            If Not dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID) Is Nothing And Not dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID) Is DBNull.Value Then
                e.Row.Cells(Me.SVC_PRICE_LIST_ID_COL).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PRICE_LIST, GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_PRICE_LIST_ID), Byte()))))
            End If

            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SVC_REQUESTED_BY_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_BY))


            If Not dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE).ToString = Nothing Then
                e.Row.Cells(Me.SVC_REQUESTED_DATE_COL).Text = GetDateFormattedString(CType(dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_REQUESTED_DATE), Date))
            End If

            If dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD) IsNot Nothing Then
                e.Row.Cells(Me.SVC_STATUS_XCD_COL).Text = (From lst In SVC_PL_Process
                                                           Where lst.ExtendedCode = dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD).ToString
                                                           Select lst.Translation).FirstOrDefault()
            End If

            'Me.PopulateControlFromBOProperty(e.Item.Cells(Me.SVC_STATUS_XCD_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_XCD))
            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SVC_STATUS_MODIFIED_by_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_BY))

            If Not dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE).ToString = Nothing Then
                e.Row.Cells(Me.SVC_STATUS_DATE_COL).Text = GetDateFormattedString(CType(dvRow.Row(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE), Date))
            End If

            'Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SVC_STATUS_DATE_COL), dvRow(SvcPriceListRecon.SvcPriceListReconSearchDV.COL_STATUS_DATE))

        End If
    End Sub

    'Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
    '    Try
    '        If e.CommandName = "SelectAction" AndAlso DataGridPriceList.Enabled Then
    '            Me.DataGridPriceList.SelectedIndex = e.Item.ItemIndex
    '            Me.State.PriceListGridSelectedIndex = e.Item.ItemIndex
    '            Me.State.selectedSvcPriceListReconId = New Guid(e.Item.Cells(Me.SVC_PRICE_LIST_RECON_ID_COL).Text)

    '            Me.State.MySvcBO = New SvcPriceListRecon(Me.State.selectedSvcPriceListReconId)
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try

    'End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridPriceList_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridPriceList.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

    Protected Sub AddSVRcReconRec(ByVal PriceListId As Guid, ByVal processStatus As String)

        Dim _SvcPriceListRecon As SvcPriceListRecon = State.MyBO.Add_SVCPLRecon
        _SvcPriceListRecon.PriceListId = PriceListId
        _SvcPriceListRecon.Status_xcd = processStatus
        _SvcPriceListRecon.RequestedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId

    End Sub

    Private Sub EnableDisableSVCPLControls()

        If Not Me.State.MyBO.CurrentSVCPLRecon Is Nothing Then

            If Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_PROCESS_PENDINGAPPROVAL Then
                'Me.State.selectedSvcPriceListReconId = New Guid(e.Item.Cells(Me.SVC_PRICE_LIST_RECON_ID_COL).Text)
                'Me.State.MySvcBO = New SvcPriceListRecon(Me.State.selectedSvcPriceListReconId)
                ControlMgr.SetEnableControl(Me, btnSubmitApproval, False)
                ControlMgr.SetEnableControl(Me, btnApprove, True)
                ControlMgr.SetEnableControl(Me, btnReject, True)
            ElseIf Me.State.MyBO.CurrentSVCPLRecon.Status_xcd = STATUS_SVC_PL_RECON_PROCESS_PENDINGSUBMISSION Then
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

    Private Sub Grid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridPriceList.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.DataGridPriceList.PageIndex = Me.State.PageIndex
            PopulateSvcDataGridPriceList()
            Me.DataGridPriceList.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            DataGridPriceList.PageIndex = NewCurrentPageIndex(DataGridPriceList, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateSvcDataGridPriceList()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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
        Me.State.MyBO.CurrentSVCPLRecon = Nothing
        Me.txtPriceListPending.Text = Nothing
        Me.txtPriceListPendingStatus.Text = Nothing
        Me.State.SvcPriciListDV = Nothing
        'Me.State.IsCopy = True
    End Sub


#End Region

#End Region

End Class


