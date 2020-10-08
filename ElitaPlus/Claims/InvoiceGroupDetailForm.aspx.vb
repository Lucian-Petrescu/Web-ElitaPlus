Imports System.Collections.Generic
Imports System.Diagnostics
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports System.Web.Script.Services
Imports System.Web.Services
Imports AjaxControlToolkit
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class InvoiceGroupDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub



#End Region
#Region "Constants"
    Public Const URL As String = "InvoiceGroupDetailForm.aspx"
    'Invoice detail grid
    Public Const GRID_COL_SERVICE_CENTERID_ID_IDX As Integer = 0
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_INVOICE_AMOUNT_IDX As Integer = 2
    Public Const GRID_COL_LINE_ITEM_AMOUNT_IDX As Integer = 3
    Public Const GRID_COL_INVOICE_DATE_IDX As Integer = 4
    Public Const GRID_COL_INVOICE_STATUS_IDX As Integer = 5
    Public Const GRID_COL_EDITID_IDX As Integer = 6
    Public Const GRID_COL_DELETEID_IDX As Integer = 7
    Public Const GRID_COL_INVOICEID_IDX As Integer = 8
    Public Const GRID_COL_INVOICE_GRP_DETAIL_ID_IDX As Integer = 9


    Public Const BTN_CONTROL_EDIT_DETAIL_LIST As String = "btn_edit"
    Public Const BTN_CONTROL_DELETE_DETAIL_LIST As String = "btn_delete"
    Public Const BTN_EDIT_LINE_ITEM As String = "btnEditItem"

    'Invoice Reconciliation grid
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_VENDOR_ID_IDX As Integer = 1
    Public Const GRID_COL_INV_NUM_IDX As Integer = 2
    Public Const GRID_COL_INV_AMOUNT_IDX As Integer = 3
    Public Const GRID_COL_INV_DATE_IDX As Integer = 4
    Public Const GRID_COL_INV_STATUS_IDX As Integer = 5
    Public Const GRID_COL_INV_ID_IDX As Integer = 6
    Public Const GRID_TOTAL_COLUMNS As Integer = 7

    'LineiTems grid
    Public Const GRID_COL_LINE_ITEM_TYPE_IDX As Integer = 2
    Public Const GRID_COL_LINE_ITEM_DESCRIPTION_IDX As Integer = 3
    Public Const GRID_COL_LINE_ITEM_AMT_IDX As Integer = 4
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 6
    Public Const GRID_COL_VENDOR_SKU_IDX As Integer = 7
    Public Const GRID_COL_VENDOR_SKU_DESC_IDX As Integer = 8
    'Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 8
    Public Const GRID_COL_LINE_ITEM_ID_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_AUTHORIZATION_ID_IDX As Integer = 1
    'Public Const GRID_COL_CERT_ID_IDX As Integer = 10
    'Public Const GRID_COL_CERT_ITEM_ID_IDX As Integer = 11
    Public Const GRID_COL_LINE_EDIT_IDX As Integer = 9
    Public Const GRID_COL_LINE_DELETE_IDX As Integer = 10


    Public Const SERVICE_CLASS_CONTROL_NAME As String = "ddlserviceclass"
    Public Const SERVICE_TYPE_CONTROL_NAME As String = "ddlservicetype"
    Public Const LINE_AMT_CONTROL_NAME As String = "txtlineitemamt"
    Public Const CLAIM_NUMBER_CONTROL_NAME As String = "txtclaimnumber"
    Public Const AUTH_NUMBER_CONTROL_NAME As String = "txtauthnumber"
    Public Const VENDOR_SKU_CONTROL_NAME As String = "ddlvendorsku"
    Public Const VENDOR_SKU_DESC_CONTROL_NAME As String = "txtvendorskudesc"
    Public Const BTN_EDIT_LINE_ITEM_DETAIL_CTRL As String = "EditRecord"

    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Private Const LABEL_SELECT_COMPANY As String = "COMPANY"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Public Const msServiceClassColumnName As String = "CODE"
    Public Const SERVICE_CLASS_ID As String = "service_class_id"
    Public Const SERVICE_TYPE_ID As String = "service_type_id"
    Public Const CLAIM_NUMBER As String = "claim_number"
    Public Const AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_VENDOR_SKU As String = "vendor_sku"
    Public Const COl_VENDOR_SKU_DESC As String = "vendor_sku_description"

    Public Const dvcount As Integer = 0

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As InvoiceGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As InvoiceGroup, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region



    Class MyState
        'Invoicedetail variables
        Public MyBO As InvoiceGroupDetail
        Public InvoicegrpdetailId As Guid
        Public InvoiceBO As Invoice
        Private Shared objInvoivceBo As Invoice
        Public InvgrpBO As InvoiceGroup
        Public InvgrpId As Guid = Guid.Empty
        Public InvoiceId As Guid
        Public Companyid As Guid
        Public ScreenSnapShotBO As InvoiceGroup
        Public DetailSearchDv As InvoiceGroupDetail.InvoiceGroupDetailSearchDV = Nothing
        Public IsNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean = False
        Public SortExpression As String = InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_NUMBER
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30
        Public modalpgsize As Integer = 5
        Public SelectedGridValueToEdit As Guid
        Public IsGridVisible As Boolean = False
        Public SelectedPageSize As Integer
        Public groupcount As Integer = 0
        Public totalamount As Decimal = 0D
        Public CompanyDV As DataView = Nothing
        Public ISinvoicedelete As Boolean = False

        'Invoice reconcile variables
        Public ServiceCenter As Guid
        Public Invoicenum As String
        Public InvoicestatusId As Guid
        Public Invoicestatus As String
        Public InvoiceAmount As String
        Public InvoiceDate As String
        Public InvoiceAmountCulture As String
        Public ReconInvsearchDV As InvoiceGroupDetail.InvoiceGroupDetailSearchDV = Nothing
        Public SearchClicked As Boolean
        Public selectedReconciliationId As Guid = Guid.Empty

        'LineItems Variables

        Public lineitemsDV As DataView = Nothing
        Public InvoiceItemBO As InvoiceItem
        Public CertBO As Certificate
        Public CertItemBO As CertItem
        Public ClaimauthBO As ClaimAuthorization
        Public ClaimBO As ClaimBase
        Public Invoiceitemid As Guid = Guid.Empty
        Public ClaimAuthorizationId As Guid = Guid.Empty
        Public certid As Guid = Guid.Empty
        Public certitemid As Guid = Guid.Empty
        Public Authorizationid As Guid = Guid.Empty
        Public claimid As Guid = Guid.Empty
        Public serviceclassid As Guid = Guid.Empty
        Public servicetypeid As Guid = Guid.Empty
        Public claimnumber As String
        Public authnumber As String
        Public rownumber As Integer
        Public claimnumberlist As DataView = Nothing
        Public vendorskudv As DataView = Nothing
        Public invitemid As Guid = Guid.Empty

        Sub New()
        End Sub
    End Class
    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
    Private ReadOnly Property IsNewInvoiceGroup() As Boolean
        Get
            Return State.InvgrpBO.IsNew
        End Get

    End Property
    Private Sub Page_PageCall(CallFromUrl As String, CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Me.CallingParameters IsNot Nothing Then
                State.InvgrpBO = New InvoiceGroup(CType(Me.CallingParameters, Guid))
                State.InvgrpId = State.InvgrpBO.Id
                State.IsEditMode = True
            End If
            ControlMgr.SetVisibleControl(Me, btnBack, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Page Events"
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            ' Populate the header and bredcrumb
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Claims")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Invoice Group Detail")
            UpdateBreadCrum()

            If Not IsPostBack Then
                MenuEnabled = False
                AddControlMsg(btnDelete, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)


                If State.InvgrpBO Is Nothing Then
                    State.InvgrpBO = New InvoiceGroup
                    State.IsNew = True
                End If


                TranslateGridHeader(InvoicesGrid)
                PopulateFormFromBOs()
                PopulateGrid()
                EnableDisableFields()
                txtgroupcount.Text = CStr(State.groupcount)
                txttotalamount.Text = GetAmountFormattedString(State.totalamount)
                ControlMgr.SetVisibleControl(Me, btnSave, False)
                If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    If (State.SelectedPageSize = 0) Then
                        State.SelectedPageSize = DEFAULT_PAGE_SIZE
                    End If
                    InvoicesGrid.PageSize = State.SelectedPageSize

                End If

            End If

            CheckIfComingFromSaveConfirm()
            BindBoPropertiesToLabels()

            If Not IsPostBack Then
                AddLabelDecorations(State.InvgrpBO)
            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

    Private Sub UpdateBreadCrum()

        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("Invoice Group Detail")
        End If

    End Sub
#End Region

#Region "Button Clicks In Main Screen"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try

            If State.InvgrpBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.InvgrpBO, State.HasDataChanged))
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            PopulateBOsFromForm()
            If (State.InvgrpBO.IsDirty) Then
                State.HasDataChanged = True
                State.InvgrpBO.Save()
                State.HasDataChanged = False
                PopulateFormFromBOs()
                'Me.EnableDisableFields(True)
                ClearGridViewHeadersAndLabelsErrSign()
                ControlMgr.SetVisibleControl(Me, btnDelete, True)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        Try
            If Not State.InvgrpBO.IsNew Then
                'Reload from the DB
                State.InvgrpBO = New InvoiceGroup(State.InvgrpBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.InvgrpBO.Clone(State.ScreenSnapShotBO)
            Else
                State.InvgrpBO = New InvoiceGroup
            End If
            PopulateFormFromBOs()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            CreateNew()
            State.InvgrpId = Guid.Empty
            ControlMgr.SetVisibleControl(Me, InvoicesGrid, False)
            State.IsNew = False

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            State.InvgrpBO.BeginEdit()
            State.InvgrpBO.Delete()

            State.InvgrpBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(DetailPageCommand.Delete, State.InvgrpBO, State.HasDataChanged))

        Catch ex As ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "INSIDE TAB Button Clicks"
    Private Sub addBtn_Click(sender As Object, e As EventArgs) Handles addBtnNew.Click
        Try
            PopulateModalControls()
            moMessageController.Visible = False

            ControlMgr.SetVisibleControl(Me, btnNewInvCancel, False)
            ControlMgr.SetVisibleControl(Me, btnEditInvSave, False)
            ControlMgr.SetVisibleControl(Me, btnClearSearch, True)
            ControlMgr.SetVisibleControl(Me, btnSearch, True)
            ControlMgr.SetVisibleControl(Me, lblInvoiceStatus, False)
            ControlMgr.SetVisibleControl(Me, ddlInvoicestatus, False)
            ControlMgr.SetVisibleControl(Me, lblRecordCount, True)
            'ControlMgr.SetVisibleControl(Me, btnNewItemAdd, False)

            divpgsize.Visible = True
            ControlMgr.SetVisibleControl(Me, trpgsize, False)
            ControlMgr.SetVisibleControl(Me, btncancelSearch, True)
            ControlMgr.SetVisibleControl(Me, ReconciledInvoiceSearchgv, False)
            AddCalendar_New(ImgInvoiceDate, txtInvoiceDate)
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub btnclick(sender As Object, e As EventArgs) Handles dummybutton.Click
        mdlPopup.Show()
    End Sub

    Protected Sub EnableDisableFields()

        If State.InvgrpBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete, False)
            ControlMgr.SetEnableControl(Me, btnAdd, False)
            ControlMgr.SetEnableControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnUndo, False)
        End If

        If State.IsEditMode Then
            ControlMgr.SetEnableControl(Me, btnUndo, False)



        End If
    End Sub
    Protected Sub PopulateDropdowns()
        Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            BindListControlToDataView(ddlVendorName, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            
            'Me.BindListControlToDataView(ddlInvoicestatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            ddlInvoicestatus.Populate(CommonConfigManager.Current.ListManager.GetList("INV_STAT",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub


    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.InvgrpBO, "ReceiptDate", lblreceiptdate)
        BindBOPropertyToLabel(State.InvgrpBO, "InvoiceGroupNumber", lblgrpnumber)

    End Sub
    Protected Sub PopulateBOsFromForm()
        Try


            Dim outputParameters() As DBHelper.DBHelperParameter
            outputParameters = InvoiceGroupDetail.GetInvoicegroupnumber()

            With State.InvgrpBO

                PopulateBOProperty(State.InvgrpBO, "InvoiceGroupNumber", outputParameters(0).Value.ToString())

            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub PopulateFormFromBOs()

        If State.IsNew Then
            txtReceiptdate.Text = GetDateFormattedString(Date.Today)
            txtgrpnumber.Text = String.Empty
        Else
            With State.InvgrpBO

                PopulateControlFromBOProperty(txtReceiptdate, .ReceiptDate)
                PopulateControlFromBOProperty(txtgrpnumber, .InvoiceGroupNumber)

            End With
        End If
    End Sub
    Protected Sub PopulateModalControls()
        'Initialize Modal form
        Try
            PopulateDropdowns()
            AddCalendarwithTime_New(ImgInvoiceDate, txtInvoiceDate, txtInvoiceDate.Text)
            ddlVendorName.SelectedIndex = 0
            txtInvoiceNumber.Text = String.Empty
            txtInvoiceAmount.Text = String.Empty
            txtInvoiceDate.Text = String.Empty
            ddlInvoicestatus.SelectedIndex = 0

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> DetailPageCommand.Accept AndAlso
                         State.ActionInProgress <> DetailPageCommand.Delete Then

                BindBoPropertiesToLabels()
                State.InvgrpBO.Save()
            End If
            Select Case State.ActionInProgress
                Case DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.InvgrpBO, State.HasDataChanged))
                Case DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNew()
                Case DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNewWithCopy()
                Case DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.InvgrpBO, State.HasDataChanged))
                Case DetailPageCommand.Delete
                    If State.ISinvoicedelete Then
                        Dim invrecondv As DataView = InvoiceGroupDetail.Getinvoicereconids(State.InvoiceId)
                        Dim dal As InvoiceGroupDetailDAL

                        'Loop in dataview
                        For Each row As DataRowView In invrecondv
                            State.InvoicegrpdetailId = New Guid(CType((row)(dal.COL_NAME_INVOICE_GROUP_DETAIL_ID), Byte()))
                            State.MyBO = New InvoiceGroupDetail(CType(State.InvoicegrpdetailId, Guid))
                            'Me.State.InvoiceBO = New Invoice(CType(Me.State.InvoiceId, Guid))
                            State.MyBO.BeginEdit()
                            State.MyBO.Delete()
                            State.MyBO.Save()
                        Next
                        State.totalamount = 0
                        PopulateGrid()

                        txttotalamount.Text = CType(State.totalamount, String)
                        txtgroupcount.Text = CStr(State.groupcount)

                    Else

                        State.InvoiceItemBO.BeginEdit()

                        State.InvoiceItemBO.Delete()

                        State.InvoiceItemBO.Save()
                        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
                        'Reload the main grid
                        PopulateGrid()
                        mdlLineItem.Hide()
                    End If

            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.InvgrpBO, State.HasDataChanged))
                Case DetailPageCommand.New_
                    CreateNew()
                Case DetailPageCommand.NewAndCopy
                    'Me.CreateNewWithCopy()
                Case DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                Case DetailPageCommand.Accept
                    EnableDisableFields()
                Case DetailPageCommand.Delete
                    If State.ISinvoicedelete Then
                    Else
                        PopulateLineitemsGrid()
                        mdlLineItem.Show()
                    End If
            End Select
        End If
        State.ActionInProgress = DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub
    Protected Sub CreateNew()
        'Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.InvgrpBO = New InvoiceGroup
        State.IsNew = True
        State.groupcount = 0
        State.totalamount = 0D
        txtgroupcount.Text = CType(State.groupcount, String)
        txttotalamount.Text = CType(State.totalamount, String)
        PopulateFormFromBOs()
        State.IsEditMode = False
        EnableDisableFields()
    End Sub


    Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

    Private Sub Populateserviceclassdropdown(ddlserviceclass As DropDownList)

        Try
            Dim langid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
           ' Me.BindListControlToDataView(ddlserviceclass, LookupListNew.DropdownLookupList(LookupListNew.LK_SERVICE_CLASS, langid))
           ddlserviceclass.Populate(CommonConfigManager.Current.ListManager.GetList("SVCCLASS",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Populateservicetypedropdown(ddlservicetype As DropDownList)
        Dim langid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
       ' Me.BindListControlToDataView(ddlservicetype, LookupListNew.DropdownLookupList(LookupListNew.LK_SERVICE_TYPE_NEW, langid))
         ddlservicetype.Populate(CommonConfigManager.Current.ListManager.GetList("SVCTYP",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
    End Sub
    Private Sub PopulateLineitemBOFromForm()

        Try
            With State.InvoiceItemBO
                .Amount = CType(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(LINE_AMT_CONTROL_NAME), TextBox).Text.ToString(), Decimal)
                .ServiceClassId = GetSelectedItem(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList))
                .ServiceTypeId = GetSelectedItem(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList))
                .VendorSku = (CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_IDX).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList).SelectedValue)
                .VendorSkuDescription = (CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text.ToString())
                .ClaimAuthorizationId = New Guid(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text.ToString())
                'Me.State.Authorizationid


            End With


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub ReturnFromEditing()

        Lineitemsgv.EditItemIndex = NO_ROW_SELECTED_INDEX

        If Lineitemsgv.PageCount = 0 Then

            ControlMgr.SetVisibleControl(Me, Lineitemsgv, False)
        Else
            ControlMgr.SetVisibleControl(Me, Lineitemsgv, True)
        End If

        SetGridControls(Lineitemsgv, True)
        State.IsEditMode = False
        PopulateLineitemsGrid()
        State.PageIndex = Lineitemsgv.CurrentPageIndex


    End Sub
    Protected Sub ddlServiceClass_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)


            Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
            Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)

            serviceTypeList.Items.Clear()
            'serviceTypeList.Enabled = False


            If (serviceclasslist.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then


                Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(GetSelectedItem(serviceclasslist), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView

                BindListControlToDataView(serviceTypeList, dv, msServiceClassColumnName, , True)
                serviceTypeList.Enabled = True
            End If


            'mdlLineItem.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlvendorsku_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)
            Dim vendorskulist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_IDX).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList)
            Dim vendorskudesc As TextBox = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox)
            vendorskudesc.Text = String.Empty
            'Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(Me.State.InvoiceBO.ServiceCenter.Code)


            If (vendorskulist.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then

                State.vendorskudv.RowFilter = "vendor_sku = '" & vendorskulist.SelectedValue & "'"
                vendorskudesc.Text = CType(State.vendorskudv(0)(COl_VENDOR_SKU_DESC), String)

            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub



#End Region

#Region "Detail GRid"


    ''' <summary>
    ''' Populate the main detail lits grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub PopulateGrid()
        Try
            If State.IsNew Then
                State.DetailSearchDv = Nothing

                lblRecordCounts.Text = dvcount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Else

                State.DetailSearchDv = State.MyBO.getInvoicegroupdetailList(State.InvgrpId)
                State.DetailSearchDv.Sort = State.SortExpression
                InvoicesGrid.AutoGenerateColumns = False
                State.groupcount = State.DetailSearchDv.Count

                InvoicesGrid.PageSize = State.PageSize

                SetPageAndSelectedIndexFromGuid(State.DetailSearchDv, State.InvoiceId, InvoicesGrid, State.PageIndex)
                InvoicesGrid.DataSource = State.DetailSearchDv
                InvoicesGrid.DataBind()
                State.PageIndex = InvoicesGrid.PageIndex


                If State.DetailSearchDv.Count > 0 Then
                    InvoicesGrid.Visible = True
                    For Each dvrow As GridViewRow In InvoicesGrid.Rows
                        State.totalamount = CType((State.totalamount + CType(dvrow.Cells(GRID_COL_INVOICE_AMOUNT_IDX).Text, Decimal)), Decimal)

                    Next
                End If
                HighLightSortColumn(InvoicesGrid, State.SortExpression, IsNewUI)
                ControlMgr.SetVisibleControl(Me, trPageSize, InvoicesGrid.Visible)
                ControlMgr.SetVisibleControl(Me, cboPageSize, InvoicesGrid.Visible)
                ControlMgr.SetVisibleControl(Me, lblRecordCounts, True)

                Session("recCount") = State.DetailSearchDv.Count
                lblRecordCounts.Text = State.DetailSearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles InvoicesGrid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
            HighLightSortColumn(InvoicesGrid, State.SortExpression, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles InvoicesGrid.PageIndexChanged
        Try
            State.PageIndex = InvoicesGrid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles InvoicesGrid.PageIndexChanging
        Try
            InvoicesGrid.PageIndex = e.NewPageIndex
            State.PageIndex = InvoicesGrid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles InvoicesGrid.RowCommand
        Try
            'Editing Grid populates modal popup with invoice detail info
            If e.CommandName = "selectAction" Then
                State.lineitemsDV = Nothing
                Lineitemsgv.SelectedIndex = NO_ITEM_SELECTED_INDEX
                molineitemmsgcontroller.Visible = False
                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                ControlMgr.SetEnableControl(Me, btnAddstandardLineItems, False)
                btnnew_lineitem.Enabled = False
                ' Me.State.InvoicegrpdetailId = New Guid(e.CommandArgument.ToString())
                State.InvoiceId = New Guid(e.CommandArgument.ToString())
                PopulateLineitemsGrid()

                State.InvoiceItemBO = New InvoiceItem(State.invitemid)
                State.ClaimauthBO = New ClaimAuthorization(State.InvoiceItemBO.ClaimAuthorizationId)
                If (State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED OrElse State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED) Then
                    ControlMgr.SetEnableControl(Me, btnAddstandardLineItems, True)
                    ControlMgr.SetEnableControl(Me, btnnew_lineitem, True)
                End If
                ReturnFromEditing()
                mdlLineItem.Show()
            End If
            If e.CommandName = EDIT_COMMAND_NAME Then
                State.IsEditMode = True

                State.InvoiceId = New Guid(e.CommandArgument.ToString())


                PopulateDropdowns()
                AddCalendarwithTime_New(ImgInvoiceDate, txtInvoiceDate, txtInvoiceDate.Text)


                State.InvoiceBO = New Invoice(CType(State.InvoiceId, Guid))

                Dim servicecenter As New ServiceCenter(State.InvoiceBO.ServiceCenterId)

                PopulateControlFromBOProperty(ddlVendorName, State.InvoiceBO.ServiceCenterId)
                txtInvoiceNumber.Text = State.InvoiceBO.InvoiceNumber.ToString()
                txtInvoiceAmount.Text = State.InvoiceBO.InvoiceAmount.ToString()
                txtInvoiceDate.Text = State.InvoiceBO.InvoiceDate.ToString()
                PopulateControlFromBOProperty(ddlInvoicestatus, State.InvoiceBO.InvoiceStatusId)

                ControlMgr.SetEnableControl(Me, ddlVendorName, False)
                ControlMgr.SetEnableControl(Me, ddlInvoicestatus, False)
                ControlMgr.SetVisibleControl(Me, btnNewInvCancel, True)
                ControlMgr.SetVisibleControl(Me, btnEditInvSave, True)
                ControlMgr.SetVisibleControl(Me, lblInvoiceStatus, True)
                ControlMgr.SetVisibleControl(Me, ddlInvoicestatus, True)
                ControlMgr.SetEnableControl(Me, txtInvoiceNumber, False)
                ControlMgr.SetEnableControl(Me, txtInvoiceDate, False)
                If State.InvoiceBO.IsComplete Then
                    ControlMgr.SetEnableControl(Me, txtInvoiceAmount, False)
                End If
                ControlMgr.SetVisibleControl(Me, btnClearSearch, False)
                ControlMgr.SetVisibleControl(Me, btnSearch, False)
                moMessageController.Visible = False
                btnDiv.Visible = False
                divpgsize.Visible = False
                ControlMgr.SetVisibleControl(Me, ReconciledInvoiceSearchgv, False)
                ControlMgr.SetVisibleControl(Me, btncancelSearch, False)

                mdlPopup.Show()
            ElseIf e.CommandName = DELETE_COMMAND_NAME Then
                State.IsEditMode = False
                State.ISinvoicedelete = True
                State.InvoiceId = New Guid(e.CommandArgument.ToString())
                Dim index As Integer = FindSelectedRowIndexFromGuid(State.DetailSearchDv, State.InvoiceId)

                Try
                    State.InvoiceId = New Guid(InvoicesGrid.Rows(index).Cells(GRID_COL_INVOICEID_IDX).Text)

                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Delete


                Catch ex As Exception
                    State.InvoiceBO.RejectChanges()
                    State.InvoiceItemBO.RejectChanges()
                    MasterPage.MessageController.AddError(Message.ERR_DELETING_DATA)
                    Throw ex
                End Try
                PopulateGrid()

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles InvoicesGrid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles InvoicesGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Assign the detail id to the command agrument
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As ImageButton
            Dim btnDeleteItem As ImageButton
            Dim btnlineitemedit As LinkButton

            If (e.Row.Cells(GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(BTN_EDIT_LINE_ITEM) IsNot Nothing) Then
                'Edit Button argument changed to id
                btnlineitemedit = CType(e.Row.Cells(GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(BTN_EDIT_LINE_ITEM), LinkButton)


                btnlineitemedit.Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_LINE_ITEM_AMOUNT), Decimal))
                btnlineitemedit.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnlineitemedit.CommandName = "selectAction"

            End If


            If (e.Row.Cells(GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) IsNot Nothing) Then
                'Edit Button argument changed to id
                btnEditItem = CType(e.Row.Cells(GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnEditItem.CommandName = EDIT_COMMAND_NAME

            End If

            e.Row.Cells(GRID_COL_SERVICE_CENTERID_ID_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_SERVICE_CENTER), String)
            e.Row.Cells(GRID_COL_INVOICE_NUMBER_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_NUMBER), String)
            e.Row.Cells(GRID_COL_INVOICE_AMOUNT_IDX).Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_AMOUNT), Decimal))
            e.Row.Cells(GRID_COL_INVOICE_DATE_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_DATE), String)
            e.Row.Cells(GRID_COL_INVOICE_STATUS_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_STATUS), String)

            If (e.Row.Cells(GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST) IsNot Nothing) Then
                'Delete Button argument changed to id
                btnDeleteItem = CType(e.Row.Cells(GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                btnDeleteItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnDeleteItem.CommandName = DELETE_COMMAND_NAME

            End If

            'e.Row.Cells(Me.GRID_COL_INVOICE_GRP_DETAIL_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INV_GRP_DETAIL_ID), Byte()))
            e.Row.Cells(GRID_COL_INVOICEID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))


        End If
    End Sub

    Private Sub cboPgSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            If State.DetailSearchDv IsNot Nothing Then
                State.PageIndex = NewCurrentPageIndex(InvoicesGrid, State.DetailSearchDv.Count, State.PageSize)
            Else
                State.PageIndex = NewCurrentPageIndex(InvoicesGrid, dvcount, State.PageSize)
            End If
            InvoicesGrid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Invoices Modal Page"
#Region "Button Clicks"


    Private Sub btninveditSave_Click(sender As Object, e As EventArgs) Handles btnEditInvSave.Click
        Try


            PopulateInvoiceBOsfrommodal()
            If (State.InvoiceBO.IsDirty OrElse State.InvoiceBO.IsFamilyDirty) Then
                State.HasDataChanged = True
                State.InvoiceBO.Save()
                State.HasDataChanged = False

                PopulateGrid()

                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

            Else

                MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNewInvCancel_Click(sender As Object, e As EventArgs) Handles btnNewInvCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btncancelSearch_Click(sender As Object, e As EventArgs) Handles btncancelSearch.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
            ReconciledInvoiceSearchgv.DataSource = State.ReconInvsearchDV
            ReconciledInvoiceSearchgv.DataBind()
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnsearch_click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            moMessageController.Clear()
            If (ddlVendorName.SelectedIndex = 0 AndAlso
                txtInvoiceAmount.Text = "" AndAlso
                txtInvoiceDate.Text = "" AndAlso
                txtInvoiceNumber.Text = "") Then

                moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                mdlPopup.Show()
            Else

                State.PageIndex = 0
                State.ReconInvsearchDV = Nothing
                State.IsGridVisible = True
                ControlMgr.SetVisibleControl(Me, ReconciledInvoiceSearchgv, True)

                PopulateReconciledInvoicesGrid()

                If State.ReconInvsearchDV Is Nothing Then
                    ControlMgr.SetVisibleControl(Me, trpgsize, False)
                    dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                    mdlPopup.Show()
                Else ' If Search Results are retrieved
                    ControlMgr.SetVisibleControl(Me, trpgsize, True)
                    btnDiv.Visible = True
                    dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"


                    mdlPopup.Show()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub

    Public Sub btnNewItemAdd_Click(sender As Object, e As EventArgs) Handles btnNewItemAdd.Click

        Try
            If State.IsNew Then
                State.InvgrpBO.Save()
                ControlMgr.SetVisibleControl(Me, btnSave, True)
            End If
            State.IsNew = False
            Dim dal As InvoiceGroupDetailDAL
            'Loop in grid
            For Each gvr As GridViewRow In ReconciledInvoiceSearchgv.Rows
                'If gvr.RowType <> DataControlRowType.DataRow Then Continue For
                If TryCast(gvr.Cells(0).FindControl("chkbxinvoice"), CheckBox).Checked Then
                    If Not (gvr.Cells(GRID_COL_INV_ID_IDX).Text = Nothing) Then
                        Dim invoiceid As Guid = GetGuidFromString(gvr.Cells(GRID_COL_INV_ID_IDX).Text)
                        Dim invrecondv As DataView = InvoiceGroupDetail.Getinvoicereconids(invoiceid)

                        'Loop in dataview
                        For Each row As DataRowView In invrecondv
                            State.selectedReconciliationId = New Guid(CType((row)(dal.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
                            State.MyBO = New InvoiceGroupDetail()
                            PopulateBOProperty(State.MyBO, "InvoiceGroupId", State.InvgrpBO.Id)
                            PopulateBOProperty(State.MyBO, "InvoiceReconciliationId", State.selectedReconciliationId)

                            State.MyBO.Save()
                        Next
                    End If

                    'Exit For
                End If
            Next
            State.InvgrpId = State.InvgrpBO.Id
            State.totalamount = 0
            PopulateGrid()
            txtgroupcount.Text = CStr(State.groupcount)
            txttotalamount.Text = GetAmountFormattedString(State.totalamount)

            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Modal Grid"
    Protected Sub PopulateReconciledInvoicesGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.ReconInvsearchDV Is Nothing) Then
                Dim sortBy As String

                State.ReconInvsearchDV = InvoiceGroupDetail.getReconciledInvoicesList(State.ServiceCenter,
                                                                       State.Invoicenum,
                                                                       State.InvoiceAmount,
                                                                       State.InvoicestatusId,
                                                                       State.InvoiceDate
                                                                     )

                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.ReconInvsearchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            ReconciledInvoiceSearchgv.AutoGenerateColumns = False
            ReconciledInvoiceSearchgv.PageSize = State.modalpgsize



            SetPageAndSelectedIndexFromGuid(State.ReconInvsearchDV, State.InvoiceId, ReconciledInvoiceSearchgv, State.PageIndex)

            ReconciledInvoiceSearchgv.DataSource = State.ReconInvsearchDV

            ReconciledInvoiceSearchgv.AllowSorting = False
            ReconciledInvoiceSearchgv.DataBind()
            State.PageIndex = InvoicesGrid.PageIndex
            TranslateGridHeader(ReconciledInvoiceSearchgv)

            If State.ReconInvsearchDV.Count > 0 Then
                btnDiv.Visible = True
                divpgsize.Visible = True
                If ReconciledInvoiceSearchgv.Visible Then
                    lblRecordCount.Text = State.ReconInvsearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                moMessageController.AddErrorAndShow(Message.MSG_NO_RECORDS_FOUND)
                lblRecordCount.Text = State.ReconInvsearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub
    Private Sub ReconciledInvoiceSearchgv_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles ReconciledInvoiceSearchgv.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub


    Private Sub ReconciledInvoiceSearchgv_PageIndexChanged(sender As Object, e As EventArgs) Handles ReconciledInvoiceSearchgv.PageIndexChanged
        Try
            State.PageIndex = ReconciledInvoiceSearchgv.PageIndex
            PopulateReconciledInvoicesGrid()
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub

    Private Sub ReconciledInvoiceSearchgv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles ReconciledInvoiceSearchgv.PageIndexChanging
        Try
            ReconciledInvoiceSearchgv.PageIndex = e.NewPageIndex
            State.PageIndex = InvoicesGrid.PageIndex
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub
    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPgSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            State.PageIndex = NewCurrentPageIndex(InvoicesGrid, State.DetailSearchDv.Count, State.PageSize)
            InvoicesGrid.PageIndex = State.PageIndex
            PopulateReconciledInvoicesGrid()
            mdlPopup.Show()

        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub

    Private Sub ReconciledInvoiceSearchgv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ReconciledInvoiceSearchgv.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim radiobtn As RadioButton

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then


                e.Row.Cells(GRID_COL_VENDOR_ID_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_SERVICE_CENTER), String)
                e.Row.Cells(GRID_COL_INV_NUM_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_NUMBER), String)
                e.Row.Cells(GRID_COL_INV_AMOUNT_IDX).Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_AMOUNT), Decimal))
                e.Row.Cells(GRID_COL_INV_DATE_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_DATE), String)
                e.Row.Cells(GRID_COL_INV_STATUS_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_STATUS), String)
                e.Row.Cells(GRID_COL_INV_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))

            End If
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"
    Protected Sub ClearSearch()
        Try
            ddlVendorName.SelectedIndex = 0
            txtInvoiceNumber.Text = String.Empty
            txtInvoiceAmount.Text = String.Empty
            txtInvoiceDate.Text = String.Empty
            ddlInvoicestatus.SelectedIndex = 0
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            State.ServiceCenter = GetSelectedItem(ddlVendorName)
            State.Invoicenum = txtInvoiceNumber.Text
            State.InvoiceAmount = txtInvoiceAmount.Text
            State.InvoicestatusId = GetSelectedItem(ddlInvoicestatus)
            State.InvoiceDate = txtInvoiceDate.Text

            If Not txtInvoiceAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(txtInvoiceAmount.Text, dblAmount) Then
                    moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR, True)
                    Return False
                Else
                    State.InvoiceAmountCulture = txtInvoiceAmount.Text.Trim
                    State.InvoiceAmount = dblAmount.ToString(Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                State.InvoiceAmount = txtInvoiceAmount.Text
            End If

            Return True

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function
    Protected Sub PopulateInvoiceBOsfrommodal()
        Try
            With State.InvoiceBO
                PopulateBOProperty(State.InvoiceBO, "InvoiceStatusId", ddlInvoicestatus)
                PopulateBOProperty(State.InvoiceBO, "ServiceCenterId", ddlVendorName)
                PopulateBOProperty(State.InvoiceBO, "InvoiceNumber", txtInvoiceNumber.Text.Trim())
                PopulateBOProperty(State.InvoiceBO, "InvoiceAmount", txtInvoiceAmount.Text.Trim())
                PopulateBOProperty(State.InvoiceBO, "InvoiceDate", txtInvoiceDate.Text.Trim())

            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#End Region

#Region "Line Items Modal page"

#Region "LineItems Grid"
    Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Lineitemsgv.ItemDataBound

        BaseItemBound(source, e)

    End Sub
    Private Sub Lineitemsgv_RowCreated(sender As Object, e As DataGridItemEventArgs) Handles Lineitemsgv.ItemCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, moMessageController)
        End Try
    End Sub

    Private Sub Lineitemsgv_PageIndexChanging(source As Object, e As DataGridPageChangedEventArgs) Handles Lineitemsgv.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            Lineitemsgv.CurrentPageIndex = State.PageIndex
            PopulateLineitemsGrid()
            Lineitemsgv.SelectedIndex = NO_ITEM_SELECTED_INDEX
            mdlLineItem.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub LIneitemsgv_RowDataBound(sender As Object, e As DataGridItemEventArgs) Handles Lineitemsgv.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)



            If (itemType = ListItemType.Item OrElse
                itemType = ListItemType.AlternatingItem OrElse
                itemType = ListItemType.SelectedItem) Then

                Dim lineitemdetailedit As ImageButton
                Dim lineitemdelete As ImageButton
                lineitemdetailedit = CType(e.Item.Cells(GRID_COL_LINE_EDIT_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                lineitemdetailedit.Visible = False
                lineitemdelete = CType(e.Item.Cells(GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                lineitemdelete.Visible = False

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_LINE_ITEM_TYPE_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_TYPE))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_DESCRIPTION))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_LINE_ITEM_AMT_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_AMOUNT))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_VENDOR_SKU_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_VENDOR_SKU_DESC_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU_DESC))
                e.Item.Cells(0).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))

                If (dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID) IsNot String.Empty) Then
                    e.Item.Cells(GRID_COL_LINE_ITEM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                    e.Item.Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))
                End If

                Dim invoiceitemBO As InvoiceItem
                Dim claimauthBO As ClaimAuthorization
                State.invitemid = New Guid(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                invoiceitemBO = New InvoiceItem(State.invitemid)
                claimauthBO = New ClaimAuthorization(invoiceitemBO.ClaimAuthorizationId)
                If (claimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED OrElse claimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED) Then
                    lineitemdetailedit.Visible = True
                    lineitemdelete.Visible = True
                End If

            ElseIf (itemType = ListItemType.EditItem) Then
                e.Item.Cells(GRID_COL_LINE_ITEM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                If (dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID) IsNot Nothing AndAlso Not String.IsNullOrEmpty(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID).ToString())) Then
                    e.Item.Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))
                End If

                CType(e.Item.Cells(GRID_COL_LINE_ITEM_AMT_IDX).FindControl(LINE_AMT_CONTROL_NAME), TextBox).Text = If(State.InvoiceItemBO.Amount Is Nothing, String.Empty, GetAmountFormattedString(CType(State.InvoiceItemBO.Amount, Decimal)))
                CType(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(CLAIM_NUMBER_CONTROL_NAME), TextBox).Text = If(State.ClaimBO Is Nothing, String.Empty, State.ClaimBO.ClaimNumber.ToString())
                CType(e.Item.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX).FindControl(AUTH_NUMBER_CONTROL_NAME), TextBox).Text = If(State.ClaimauthBO Is Nothing, String.Empty, State.ClaimauthBO.AuthorizationNumber.ToString())
                'If Not Me.State.InvoiceItemBO.VendorSku Is Nothing Then
                '    CType(e.Item.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), TextBox).Text = Me.State.InvoiceItemBO.VendorSku
                'End If
                CType(e.Item.Cells(GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text = If(State.InvoiceItemBO.VendorSkuDescription Is Nothing, String.Empty, State.InvoiceItemBO.VendorSkuDescription)

                State.InvoiceBO = New Invoice(State.InvoiceId)

                Dim autocompleteext As AutoCompleteExtender = CType(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl("AutoCompleteExtender1"), AutoCompleteExtender)
                autocompleteext.ContextKey = State.InvoiceBO.ServiceCenter.Code



            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles Lineitemsgv.ItemCommand
        Try
            'Editing Grid populates modal popup with invoice line item  details info
            Dim index As Integer = e.Item.ItemIndex
            If e.CommandName = EDIT_COMMAND_NAME Then

                hdnclaimnum.Value = ""
                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                State.IsNew = False

                State.IsEditMode = True

                State.Invoiceitemid = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).Text)
                State.ClaimAuthorizationId = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text)

                State.InvoiceBO = New Invoice(State.InvoiceId)
                State.InvoiceItemBO = New InvoiceItem(State.Invoiceitemid)

                State.ClaimauthBO = New ClaimAuthorization(State.InvoiceItemBO.ClaimAuthorizationId)
                State.claimid = State.ClaimauthBO.ClaimId
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimid)

                If State.InvoiceBO.CanModifyClaimAuthorization(State.InvoiceItemBO.ClaimAuthorizationId) Then

                    PopulateLineitemsGrid()

                    State.PageIndex = Lineitemsgv.CurrentPageIndex

                    Dim editbtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                    editbtn.Visible = False

                    Dim deletebtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                    deletebtn.Visible = False


                    Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                    Populateserviceclassdropdown(serviceclasslist)

                    Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)
                    Populateservicetypedropdown(serviceTypeList)

                    Dim vendorskulist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_IDX).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList)
                    Populatevendorskudropdown(vendorskulist, State.InvoiceItemBO.ServiceClassId, State.InvoiceItemBO.ServiceTypeId, State.ClaimauthBO.ClaimId)

                    'If serviceclasslist.Items.FindByText((LookupListNew.GetCodeFromId("SVCCLASS", Me.State.InvoiceBO.ServiceCenterId)) Then
                    Try
                        SetSelectedItem(serviceclasslist, State.InvoiceItemBO.ServiceClassId)
                        SetSelectedItem(serviceTypeList, State.InvoiceItemBO.ServiceTypeId)
                        SetSelectedItem(vendorskulist, State.InvoiceItemBO.VendorSku)
                    Catch ex As Exception
                    End Try

                    mdlLineItem.Show()
                Else
                    MasterPage.MessageController.AddErrorAndShow(Message.MSG_CANNOT_MODIFY_CLAIM_AUTHORIZATION)

                End If
            ElseIf e.CommandName = DELETE_COMMAND_NAME Then

                State.Invoiceitemid = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).Text)
                State.ClaimAuthorizationId = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text)
                State.InvoiceItemBO = New InvoiceItem(CType(State.Invoiceitemid, Guid))
                State.ClaimauthBO = New ClaimAuthorization(State.InvoiceItemBO.ClaimAuthorizationId)
                Try
                    If (State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__VOID OrElse State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__PENDING) Then

                        DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = DetailPageCommand.Delete
                    Else
                        MasterPage.MessageController.AddErrorAndShow(Message.MSG_LINE_ITEM_CANNOT_BE_DELETED)
                    End If
                Catch ex As Exception
                    State.InvoiceItemBO.RejectChanges()

                    MasterPage.MessageController.AddError(Message.ERR_DELETING_DATA)
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            HandleErrors(ex, molineitemmsgcontroller)
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try
    End Sub

    Protected Sub PopulateLineitemsGrid()
        Try


            Dim sortBy As String
            State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(State.InvoiceId)

            Lineitemsgv.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)
            State.PageIndex = Lineitemsgv.CurrentPageIndex

            Lineitemsgv.DataSource = State.lineitemsDV

            Lineitemsgv.AllowSorting = False
            Lineitemsgv.DataBind()

            Session("recCount") = State.lineitemsDV.Count
            If State.lineitemsDV.Count > 0 Then

                If Lineitemsgv.Visible Then
                    lblRecordCount.Text = State.lineitemsDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else

                If Lineitemsgv.Visible Then
                    lblRecordCount.Text = State.lineitemsDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"
    Protected Sub btnnewlineitem_click(Sender As Object, e As EventArgs) Handles btnnew_lineitem.Click
        Try
            molineitemmsgcontroller.Clear()
            State.InvoiceBO = New Invoice(State.InvoiceId)
            If CType(State.InvoiceBO.CanAddAuthorization, Boolean) Then
                State.IsNew = True
                State.IsEditMode = True
                State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(State.InvoiceId)

                State.InvoiceItemBO = New InvoiceItem()
                State.ClaimBO = Nothing
                State.ClaimauthBO = Nothing

                State.Invoiceitemid = State.InvoiceItemBO.Id

                State.lineitemsDV = State.MyBO.GetNewDataViewRow(State.lineitemsDV, State.InvoiceItemBO)

                Lineitemsgv.DataSource = State.lineitemsDV

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)

                SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)
                Lineitemsgv.DataBind()
                Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Populateserviceclassdropdown(serviceclasslist)
                SetSelectedItem(serviceclasslist, State.InvoiceItemBO.ServiceClassId)

                SetFocusOnEditableFieldInGrid(Lineitemsgv, GRID_COL_LINE_ITEM_AMT_IDX, LINE_AMT_CONTROL_NAME, Lineitemsgv.EditItemIndex)
                SetFocusOnEditableFieldInGrid(Lineitemsgv, GRID_COL_CLAIM_NUMBER_IDX, CLAIM_NUMBER_CONTROL_NAME, Lineitemsgv.EditItemIndex)
                SetFocusOnEditableFieldInGrid(Lineitemsgv, GRID_COL_AUTHORIZATION_NUMBER_IDX, AUTH_NUMBER_CONTROL_NAME, Lineitemsgv.EditItemIndex)
                'Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_VENDOR_SKU_IDX, Me.VENDOR_SKU_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)
                SetFocusOnEditableFieldInGrid(Lineitemsgv, GRID_COL_VENDOR_SKU_DESC_IDX, VENDOR_SKU_DESC_CONTROL_NAME, Lineitemsgv.EditItemIndex)

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                Dim editbtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                editbtn.Visible = False

                Dim deletebtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                deletebtn.Visible = False
                mdlLineItem.Show()

            Else
                molineitemmsgcontroller.AddErrorAndShow(Message.MSG_CANNOT_ADD_CLAIM_AUTHORIZATION)
                PopulateLineitemsGrid()

                mdlLineItem.Show()
            End If

        Catch ex As Exception
            HandleErrors(ex, molineitemmsgcontroller)
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try
    End Sub

    Protected Sub SaveButton_WRITE_Click(sender As Object, e As EventArgs) Handles btnsave_lineitem.Click
        Try
            molineitemmsgcontroller.Clear()
            If State.IsNew Then
                Dim lineitemnumber As Integer

                State.InvoiceItemBO = Nothing
                State.CertBO = Nothing
                State.CertItemBO = Nothing
                State.ClaimauthBO = Nothing
                State.ClaimBO = Nothing

                State.InvoiceBO = New Invoice(State.InvoiceId)
                'Me.State.MyBO = New InvoiceGroupDetail(Me.State.InvoicegrpdetailId)
                SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)

                Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)
                Dim vendorsku As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_IDX).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList).SelectedValue

                Dim authnumber As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX).FindControl(AUTH_NUMBER_CONTROL_NAME), TextBox).Text
                Dim claimnumber As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(CLAIM_NUMBER_CONTROL_NAME), TextBox).Text
                Dim amount As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(LINE_AMT_CONTROL_NAME), TextBox).Text
                Dim vendorskudesc As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text
                Dim dal As InvoiceGroupDetailDAL

                Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(State.InvoiceBO.ServiceCenter.Code)

                If Not claimnumber.Equals(String.Empty) Then
                    dv.RowFilter = "claim_number='" & claimnumber & "'"

                    State.Authorizationid = New Guid(CType(dv(0)(dal.COL_NAME_AUTHORIZATION_ID), Byte()))

                    Dim dv1 As DataView = InvoiceGroupDetail.Getmaxlineitemnumber(State.InvoiceId)
                    lineitemnumber = CType(dv1(0)(dal.COL_NAME_LINE_ITEM_NUMBER), Integer)

                    State.ClaimauthBO = New ClaimAuthorization(State.Authorizationid)

                    If State.ClaimauthBO.ServiceCenterId = State.InvoiceBO.ServiceCenterId AndAlso _
                         State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED Then


                        State.InvoiceItemBO = New InvoiceItem()
                        State.InvoiceItemBO.ServiceClassId = GetSelectedItem(serviceclasslist)
                        State.InvoiceItemBO.ServiceTypeId = GetSelectedItem(serviceTypeList)
                        State.InvoiceItemBO.Amount = CType(amount, DecimalType)
                        State.InvoiceItemBO.ClaimAuthorizationId = State.Authorizationid
                        State.InvoiceItemBO.InvoiceId = State.InvoiceId
                        State.InvoiceItemBO.LineItemNumber = lineitemnumber + 1
                        State.InvoiceItemBO.VendorSku = vendorsku
                        State.InvoiceItemBO.VendorSkuDescription = vendorskudesc
                        'Adding new invoice item in invoice item table 
                        State.InvoiceItemBO.Save()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

                        'After Adding invoice item, balancing the invoice
                        State.InvoiceBO.Balance.Execute()
                        'Reload the main grid
                        PopulateGrid()
                        mdlLineItem.Hide()

                    Else

                        'Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_AUTHORIZATION_SERVICE_CENTER, True)
                        'Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_AUTHORIZATION_SERVICE_CENTER, True)
                        Throw New GUIException(Message.MSG_AUTHORIZATION_SERVICE_CENTER, Message.MSG_AUTHORIZATION_SERVICE_CENTER)
                        mdlLineItem.Show()
                    End If
                Else
                    PopulateLineitemsGrid()
                    'Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
                    MasterPage.MessageController.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
                    mdlLineItem.Show()
                End If

            Else
                PopulateLineitemBOFromForm()


                If (State.InvoiceItemBO.IsDirty) Then
                    State.InvoiceItemBO.Save()

                    molineitemmsgcontroller.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    mdlLineItem.Show()

                    ReturnFromEditing()
                    ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                    ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                    ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                    mdlLineItem.Show()
                    State.lineitemsDV = Nothing
                Else
                    molineitemmsgcontroller.AddError(Message.MSG_RECORD_NOT_SAVED)

                    ReturnFromEditing()
                    ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                    ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                    ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                    mdlLineItem.Show()
                End If
                'Reload the main grid
                PopulateGrid()
            End If
        Catch ex As Exception

            HandleErrors(ex, molineitemmsgcontroller)
            hdnclaimnum.Value = ""
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try

    End Sub
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles btnundo_lineitem.Click

        Try
            molineitemmsgcontroller.Clear()
            Lineitemsgv.SelectedIndex = NO_ITEM_SELECTED_INDEX

            ReturnFromEditing()
            ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
            ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
            ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
            mdlLineItem.Show()
        Catch ex As Exception
            HandleErrors(ex, molineitemmsgcontroller)
        End Try

    End Sub
    Private Sub standardlineitems_click(sender As Object, e As EventArgs) Handles btnAddstandardLineItems.Click
        Try
            molineitemmsgcontroller.Clear()
            State.InvoiceBO = New Invoice(State.InvoiceId)
            If CType(State.InvoiceBO.CanAddAuthorization, Boolean) Then
                State.IsNew = True
                State.IsEditMode = True
                State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(State.InvoiceId)

                State.InvoiceItemBO = New InvoiceItem()
                State.Invoiceitemid = State.InvoiceItemBO.Id
                State.ClaimBO = Nothing
                State.ClaimauthBO = Nothing

                State.lineitemsDV = State.MyBO.GetNewDataViewRow(State.lineitemsDV, State.InvoiceItemBO)

                Lineitemsgv.DataSource = State.lineitemsDV

                SetPageAndSelectedIndexFromGuid(State.lineitemsDV, State.Invoiceitemid, Lineitemsgv, State.PageIndex, State.IsEditMode)
                Lineitemsgv.DataBind()

                Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).Text = State.Invoiceitemid.ToString()
                Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = State.ClaimAuthorizationId.ToString()

                Dim dal As New InvoiceGroupDetailDAL

                Dim editbtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                editbtn.Visible = False

                Dim deletebtn As ImageButton = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                deletebtn.Visible = False

                Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim ds As DataSet = State.MyBO.Addstandardlineitems()
                Populateserviceclassdropdown(serviceclasslist)
                SetSelectedItem(serviceclasslist, New Guid(CType(ds.Tables(0).Rows(0)(SERVICE_CLASS_ID), Byte())))
                serviceclasslist.Enabled = False

                Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)

                Populateservicetypedropdown(serviceTypeList)
                SetSelectedItem(serviceTypeList, New Guid(CType(ds.Tables(0).Rows(0)(SERVICE_TYPE_ID), Byte())))
                serviceTypeList.Enabled = False

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                mdlLineItem.Show()
            Else
                molineitemmsgcontroller.AddErrorAndShow(Message.MSG_CANNOT_ADD_CLAIM_AUTHORIZATION)
                PopulateLineitemsGrid()
                mdlLineItem.Show()

            End If
        Catch ex As Exception
            HandleErrors(ex, molineitemmsgcontroller)
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try
    End Sub

#End Region

#End Region


#Region "Helper Functions"
    <WebMethod(), ScriptMethod()> _
    Public Shared Function GetCompletionList(prefixText As String, count As Integer, contextKey As String) As List(Of String)


        Dim listitems As List(Of String) = New List(Of String)
        ' Dim listItems As New ArrayList


        Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(contextKey)

        If dv IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then
            dv.RowFilter = "Claim_Number like '" & prefixText & "%'"
            For Each dr As DataRow In dv.Table.Rows
                listitems.Add(dr(CLAIM_NUMBER).ToString())
            Next
        End If

        Return (listitems)
    End Function

    Protected Sub populatevendorsku(sender As Object, e As EventArgs)

        If Not String.IsNullOrEmpty(hdnclaimnum.Value) Then
            If Not State.claimnumber = hdnclaimnum.Value Then
                State.rownumber = CType(hdnrowNumber.Value, Integer)
                Dim ddlServiceClass As DropDownList = CType(Lineitemsgv.Items(State.rownumber - 3).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim ddlServicetype As DropDownList = CType(Lineitemsgv.Items(State.rownumber - 3).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)
                Dim vendorsku As DropDownList = CType(Lineitemsgv.Items(State.rownumber - 3).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList)
                Dim authnumber As TextBox = CType(Lineitemsgv.Items(State.rownumber - 3).FindControl(AUTH_NUMBER_CONTROL_NAME), TextBox)
                Dim skuDescription As TextBox = CType(Lineitemsgv.Items(State.rownumber - 3).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox)
                vendorsku.Items.Clear()
                authnumber.Text = String.Empty
                skuDescription.Text = String.Empty

                State.claimnumber = hdnclaimnum.Value
                State.serviceclassid = New Guid(ddlServiceClass.SelectedValue)
                State.servicetypeid = New Guid(ddlServicetype.SelectedValue)

                Dim dv As DataView = State.MyBO.Getlineiteminsertvalues(State.InvoiceBO.ServiceCenter.Code)

                dv.RowFilter = "claim_number = '" & State.claimnumber & "'"

                State.claimid = GuidControl.ByteArrayToGuid(dv(0)("claim_id"))
                authnumber.Text = dv(0)(AUTHORIZATION_NUMBER).ToString()
                State.Authorizationid = GuidControl.ByteArrayToGuid(dv(0)("claim_authorization_id"))

                Populatevendorskudropdown(vendorsku, State.serviceclassid, State.servicetypeid, State.claimid)


                hdnclaimnum.Value = ""
            End If
        End If
    End Sub

    Private Sub Populatevendorskudropdown(vendorsku As DropDownList, serviceclassid As Guid, servicetypeid As Guid, Claimid As Guid)

        State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Claimid)
        State.InvoiceBO = New Invoice(State.InvoiceId)
        Dim servcenter As New ServiceCenter(State.InvoiceBO.ServiceCenterId)
        Dim equipmentId As Guid, equipmentclassId As Guid, conditionId As Guid
        If (State.ClaimBO.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If (State.ClaimBO.ClaimedEquipment IsNot Nothing) Then
                equipmentId = State.ClaimBO.ClaimedEquipment.EquipmentId
                equipmentclassId = State.ClaimBO.ClaimedEquipment.EquipmentBO.EquipmentClassId
                conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)

            End If
        End If

        Dim dv1 As PriceListDetail.PriceListResultsDV = PriceListDetail.GetPricesForServiceType(State.ClaimBO.CompanyId, servcenter.Code, _
                                                                                                   State.ClaimBO.RiskTypeId, State.ClaimBO.LossDate.Value, _
                                                                                                   State.ClaimBO.Certificate.SalesPrice.Value, _
                                                                                                   serviceclassid, _
                                                                                                    servicetypeid, _
                                                                                                 equipmentclassId, equipmentId, conditionId, State.ClaimBO.Dealer.Id, String.Empty)


        State.vendorskudv = dv1
        If State.vendorskudv IsNot Nothing Then
            BindListTextToDataView(vendorsku, State.vendorskudv, COL_VENDOR_SKU, COL_VENDOR_SKU, True)

        Else
            MasterPage.MessageController.AddErrorAndShow(Message.MSG_VENDOR_SKU_LIST_NOT_FOUND)
        End If

    End Sub

#End Region

End Class
