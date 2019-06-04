Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class InvoiceGroupDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As InvoiceGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
            Return Me.State.InvgrpBO.IsNew
        End Get

    End Property
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InvgrpBO = New InvoiceGroup(CType(Me.CallingParameters, Guid))
                Me.State.InvgrpId = Me.State.InvgrpBO.Id
                Me.State.IsEditMode = True
            End If
            ControlMgr.SetVisibleControl(Me, btnBack, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            ' Populate the header and bredcrumb
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Claims")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Invoice Group Detail")
            Me.UpdateBreadCrum()

            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)


                If Me.State.InvgrpBO Is Nothing Then
                    Me.State.InvgrpBO = New InvoiceGroup
                    Me.State.IsNew = True
                End If


                Me.TranslateGridHeader(InvoicesGrid)
                Me.PopulateFormFromBOs()
                PopulateGrid()
                Me.EnableDisableFields()
                Me.txtgroupcount.Text = CStr(Me.State.groupcount)
                Me.txttotalamount.Text = GetAmountFormattedString(Me.State.totalamount)
                ControlMgr.SetVisibleControl(Me, btnSave, False)
                If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                    If (Me.State.SelectedPageSize = 0) Then
                        Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE
                    End If
                    Me.InvoicesGrid.PageSize = Me.State.SelectedPageSize

                End If

            End If

            Me.CheckIfComingFromSaveConfirm()
            Me.BindBoPropertiesToLabels()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.InvgrpBO)
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub UpdateBreadCrum()

        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("Invoice Group Detail")
        End If

    End Sub
#End Region

#Region "Button Clicks In Main Screen"
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try

            If Me.State.InvgrpBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvgrpBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            Me.PopulateBOsFromForm()
            If (Me.State.InvgrpBO.IsDirty) Then
                Me.State.HasDataChanged = True
                Me.State.InvgrpBO.Save()
                Me.State.HasDataChanged = False
                Me.PopulateFormFromBOs()
                'Me.EnableDisableFields(True)
                Me.ClearGridViewHeadersAndLabelsErrSign()
                ControlMgr.SetVisibleControl(Me, btnDelete, True)
                Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        Try
            If Not Me.State.InvgrpBO.IsNew Then
                'Reload from the DB
                Me.State.InvgrpBO = New InvoiceGroup(Me.State.InvgrpBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.InvgrpBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.InvgrpBO = New InvoiceGroup
            End If
            Me.PopulateFormFromBOs()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.CreateNew()
            Me.State.InvgrpId = Guid.Empty
            ControlMgr.SetVisibleControl(Me, InvoicesGrid, False)
            Me.State.IsNew = False

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            Me.State.InvgrpBO.BeginEdit()
            Me.State.InvgrpBO.Delete()

            Me.State.InvgrpBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.InvgrpBO, Me.State.HasDataChanged))

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "INSIDE TAB Button Clicks"
    Private Sub addBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addBtnNew.Click
        Try
            Me.PopulateModalControls()
            Me.moMessageController.Visible = False

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
            Me.AddCalendar_New(Me.ImgInvoiceDate, Me.txtInvoiceDate)
            mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub btnclick(ByVal sender As Object, ByVal e As EventArgs) Handles dummybutton.Click
        mdlPopup.Show()
    End Sub

    Protected Sub EnableDisableFields()

        If Me.State.InvgrpBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete, False)
            ControlMgr.SetEnableControl(Me, btnAdd, False)
            ControlMgr.SetEnableControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnUndo, False)
        End If

        If Me.State.IsEditMode Then
            ControlMgr.SetEnableControl(Me, btnUndo, False)



        End If
    End Sub
    Protected Sub PopulateDropdowns()
        Dim langID As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Me.BindListControlToDataView(ddlVendorName, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            
            'Me.BindListControlToDataView(ddlInvoicestatus, LookupListNew.DropdownLookupList(LookupListNew.LK_INVOICE_STATUS, langID))
            ddlInvoicestatus.Populate(CommonConfigManager.Current.ListManager.GetList("INV_STAT",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.InvgrpBO, "ReceiptDate", Me.lblreceiptdate)
        Me.BindBOPropertyToLabel(Me.State.InvgrpBO, "InvoiceGroupNumber", Me.lblgrpnumber)

    End Sub
    Protected Sub PopulateBOsFromForm()
        Try


            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            outputParameters = InvoiceGroupDetail.GetInvoicegroupnumber()

            With Me.State.InvgrpBO

                Me.PopulateBOProperty(Me.State.InvgrpBO, "InvoiceGroupNumber", outputParameters(0).Value.ToString())

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub PopulateFormFromBOs()

        If Me.State.IsNew Then
            Me.txtReceiptdate.Text = Date.Today.ToString()
            Me.txtgrpnumber.Text = String.Empty
        Else
            With Me.State.InvgrpBO

                Me.PopulateControlFromBOProperty(Me.txtReceiptdate, .ReceiptDate)
                Me.PopulateControlFromBOProperty(Me.txtgrpnumber, .InvoiceGroupNumber)

            End With
        End If
    End Sub
    Protected Sub PopulateModalControls()
        'Initialize Modal form
        Try
            PopulateDropdowns()
            AddCalendarwithTime_New(ImgInvoiceDate, txtInvoiceDate, txtInvoiceDate.Text)
            Me.ddlVendorName.SelectedIndex = 0
            Me.txtInvoiceNumber.Text = String.Empty
            Me.txtInvoiceAmount.Text = String.Empty
            Me.txtInvoiceDate.Text = String.Empty
            Me.ddlInvoicestatus.SelectedIndex = 0

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try


    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept AndAlso
                         Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete Then

                Me.BindBoPropertiesToLabels()
                Me.State.InvgrpBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvgrpBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.InvgrpBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    If Me.State.ISinvoicedelete Then
                        Dim invrecondv As DataView = InvoiceGroupDetail.Getinvoicereconids(Me.State.InvoiceId)
                        Dim dal As InvoiceGroupDetailDAL

                        'Loop in dataview
                        For Each row As DataRowView In invrecondv
                            Me.State.InvoicegrpdetailId = New Guid(CType((row)(dal.COL_NAME_INVOICE_GROUP_DETAIL_ID), Byte()))
                            Me.State.MyBO = New InvoiceGroupDetail(CType(Me.State.InvoicegrpdetailId, Guid))
                            'Me.State.InvoiceBO = New Invoice(CType(Me.State.InvoiceId, Guid))
                            Me.State.MyBO.BeginEdit()
                            Me.State.MyBO.Delete()
                            Me.State.MyBO.Save()
                        Next
                        Me.State.totalamount = 0
                        PopulateGrid()

                        Me.txttotalamount.Text = CType(Me.State.totalamount, String)
                        Me.txtgroupcount.Text = CStr(Me.State.groupcount)

                    Else

                        Me.State.InvoiceItemBO.BeginEdit()

                        Me.State.InvoiceItemBO.Delete()

                        Me.State.InvoiceItemBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.DELETE_RECORD_CONFIRMATION)
                        'Reload the main grid
                        PopulateGrid()
                        Me.mdlLineItem.Hide()
                    End If

            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvgrpBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.EnableDisableFields()
                Case ElitaPlusPage.DetailPageCommand.Delete
                    If Me.State.ISinvoicedelete Then
                    Else
                        PopulateLineitemsGrid()
                        mdlLineItem.Show()
                    End If
            End Select
        End If
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub
    Protected Sub CreateNew()
        'Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.InvgrpBO = New InvoiceGroup
        Me.State.IsNew = True
        Me.State.groupcount = 0
        Me.State.totalamount = 0D
        Me.txtgroupcount.Text = CType(Me.State.groupcount, String)
        Me.txttotalamount.Text = CType(Me.State.totalamount, String)
        Me.PopulateFormFromBOs()
        Me.State.IsEditMode = False
        Me.EnableDisableFields()
    End Sub


    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

    Private Sub Populateserviceclassdropdown(ByVal ddlserviceclass As DropDownList)

        Try
            Dim langid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
           ' Me.BindListControlToDataView(ddlserviceclass, LookupListNew.DropdownLookupList(LookupListNew.LK_SERVICE_CLASS, langid))
           ddlserviceclass.Populate(CommonConfigManager.Current.ListManager.GetList("SVCCLASS",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Populateservicetypedropdown(ByVal ddlservicetype As DropDownList)
        Dim langid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
       ' Me.BindListControlToDataView(ddlservicetype, LookupListNew.DropdownLookupList(LookupListNew.LK_SERVICE_TYPE_NEW, langid))
         ddlservicetype.Populate(CommonConfigManager.Current.ListManager.GetList("SVCTYP",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
    End Sub
    Private Sub PopulateLineitemBOFromForm()

        Try
            With Me.State.InvoiceItemBO
                .Amount = CType(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(Me.LINE_AMT_CONTROL_NAME), TextBox).Text.ToString(), Decimal)
                .ServiceClassId = Me.GetSelectedItem(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList))
                .ServiceTypeId = Me.GetSelectedItem(CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(Me.SERVICE_TYPE_CONTROL_NAME), DropDownList))
                .VendorSku = (CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), DropDownList).SelectedValue)
                .VendorSkuDescription = (CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(Me.VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text.ToString())
                .ClaimAuthorizationId = New Guid(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text.ToString())
                'Me.State.Authorizationid


            End With


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Private Sub ReturnFromEditing()

        Lineitemsgv.EditItemIndex = NO_ROW_SELECTED_INDEX

        If Me.Lineitemsgv.PageCount = 0 Then

            ControlMgr.SetVisibleControl(Me, Lineitemsgv, False)
        Else
            ControlMgr.SetVisibleControl(Me, Lineitemsgv, True)
        End If

        SetGridControls(Lineitemsgv, True)
        Me.State.IsEditMode = False
        Me.PopulateLineitemsGrid()
        Me.State.PageIndex = Lineitemsgv.CurrentPageIndex


    End Sub
    Protected Sub ddlServiceClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Me.SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)


            Dim serviceclasslist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList)
            Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(Me.SERVICE_TYPE_CONTROL_NAME), DropDownList)

            serviceTypeList.Items.Clear()
            'serviceTypeList.Enabled = False


            If (serviceclasslist.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then


                Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(serviceclasslist), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView

                ElitaPlusPage.BindListControlToDataView(serviceTypeList, dv, msServiceClassColumnName, , True)
                serviceTypeList.Enabled = True
            End If


            'mdlLineItem.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlvendorsku_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)
            Dim vendorskulist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), DropDownList)
            Dim vendorskudesc As TextBox = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(Me.VENDOR_SKU_DESC_CONTROL_NAME), TextBox)
            vendorskudesc.Text = String.Empty
            'Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(Me.State.InvoiceBO.ServiceCenter.Code)


            If (vendorskulist.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then

                Me.State.vendorskudv.RowFilter = "vendor_sku = '" & vendorskulist.SelectedValue & "'"
                vendorskudesc.Text = CType(Me.State.vendorskudv(0)(COl_VENDOR_SKU_DESC), String)

            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            If Me.State.IsNew Then
                Me.State.DetailSearchDv = Nothing

                Me.lblRecordCounts.Text = Me.dvcount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Else

                Me.State.DetailSearchDv = Me.State.MyBO.getInvoicegroupdetailList(Me.State.InvgrpId)
                Me.State.DetailSearchDv.Sort = Me.State.SortExpression
                Me.InvoicesGrid.AutoGenerateColumns = False
                Me.State.groupcount = Me.State.DetailSearchDv.Count

                Me.InvoicesGrid.PageSize = Me.State.PageSize

                SetPageAndSelectedIndexFromGuid(Me.State.DetailSearchDv, Me.State.InvoiceId, Me.InvoicesGrid, Me.State.PageIndex)
                Me.InvoicesGrid.DataSource = Me.State.DetailSearchDv
                Me.InvoicesGrid.DataBind()
                Me.State.PageIndex = Me.InvoicesGrid.PageIndex


                If Me.State.DetailSearchDv.Count > 0 Then
                    Me.InvoicesGrid.Visible = True
                    For Each dvrow As GridViewRow In InvoicesGrid.Rows
                        Me.State.totalamount = CType((Me.State.totalamount + CType(dvrow.Cells(GRID_COL_INVOICE_AMOUNT_IDX).Text, Decimal)), Decimal)

                    Next
                End If
                HighLightSortColumn(InvoicesGrid, Me.State.SortExpression, Me.IsNewUI)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.InvoicesGrid.Visible)
                ControlMgr.SetVisibleControl(Me, cboPageSize, Me.InvoicesGrid.Visible)
                ControlMgr.SetVisibleControl(Me, lblRecordCounts, True)

                Session("recCount") = Me.State.DetailSearchDv.Count
                Me.lblRecordCounts.Text = Me.State.DetailSearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles InvoicesGrid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
            Me.HighLightSortColumn(InvoicesGrid, Me.State.SortExpression, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InvoicesGrid.PageIndexChanged
        Try
            Me.State.PageIndex = InvoicesGrid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles InvoicesGrid.PageIndexChanging
        Try
            InvoicesGrid.PageIndex = e.NewPageIndex
            State.PageIndex = InvoicesGrid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles InvoicesGrid.RowCommand
        Try
            'Editing Grid populates modal popup with invoice detail info
            If e.CommandName = "selectAction" Then
                Me.State.lineitemsDV = Nothing
                Me.Lineitemsgv.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.molineitemmsgcontroller.Visible = False
                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                ControlMgr.SetEnableControl(Me, btnAddstandardLineItems, False)
                Me.btnnew_lineitem.Enabled = False
                ' Me.State.InvoicegrpdetailId = New Guid(e.CommandArgument.ToString())
                Me.State.InvoiceId = New Guid(e.CommandArgument.ToString())
                PopulateLineitemsGrid()

                Me.State.InvoiceItemBO = New InvoiceItem(Me.State.invitemid)
                Me.State.ClaimauthBO = New ClaimAuthorization(Me.State.InvoiceItemBO.ClaimAuthorizationId)
                If (Me.State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED Or
                             Me.State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED) Then
                    ControlMgr.SetEnableControl(Me, btnAddstandardLineItems, True)
                    ControlMgr.SetEnableControl(Me, btnnew_lineitem, True)
                End If
                ReturnFromEditing()
                mdlLineItem.Show()
            End If
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                Me.State.IsEditMode = True

                Me.State.InvoiceId = New Guid(e.CommandArgument.ToString())


                PopulateDropdowns()
                AddCalendarwithTime_New(ImgInvoiceDate, txtInvoiceDate, txtInvoiceDate.Text)


                Me.State.InvoiceBO = New Invoice(CType(Me.State.InvoiceId, Guid))

                Dim servicecenter As New ServiceCenter(Me.State.InvoiceBO.ServiceCenterId)

                Me.PopulateControlFromBOProperty(ddlVendorName, Me.State.InvoiceBO.ServiceCenterId)
                txtInvoiceNumber.Text = Me.State.InvoiceBO.InvoiceNumber.ToString()
                txtInvoiceAmount.Text = Me.State.InvoiceBO.InvoiceAmount.ToString()
                txtInvoiceDate.Text = Me.State.InvoiceBO.InvoiceDate.ToString()
                Me.PopulateControlFromBOProperty(ddlInvoicestatus, Me.State.InvoiceBO.InvoiceStatusId)

                ControlMgr.SetEnableControl(Me, ddlVendorName, False)
                ControlMgr.SetEnableControl(Me, ddlInvoicestatus, False)
                ControlMgr.SetVisibleControl(Me, btnNewInvCancel, True)
                ControlMgr.SetVisibleControl(Me, btnEditInvSave, True)
                ControlMgr.SetVisibleControl(Me, lblInvoiceStatus, True)
                ControlMgr.SetVisibleControl(Me, ddlInvoicestatus, True)
                ControlMgr.SetEnableControl(Me, txtInvoiceNumber, False)
                ControlMgr.SetEnableControl(Me, txtInvoiceDate, False)
                If Me.State.InvoiceBO.IsComplete Then
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
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                Me.State.IsEditMode = False
                Me.State.ISinvoicedelete = True
                Me.State.InvoiceId = New Guid(e.CommandArgument.ToString())
                Dim index As Integer = FindSelectedRowIndexFromGuid(Me.State.DetailSearchDv, Me.State.InvoiceId)

                Try
                    Me.State.InvoiceId = New Guid(InvoicesGrid.Rows(index).Cells(Me.GRID_COL_INVOICEID_IDX).Text)

                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete


                Catch ex As Exception
                    Me.State.InvoiceBO.RejectChanges()
                    Me.State.InvoiceItemBO.RejectChanges()
                    Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                    Throw ex
                End Try
                PopulateGrid()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles InvoicesGrid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles InvoicesGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Assign the detail id to the command agrument
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As ImageButton
            Dim btnDeleteItem As ImageButton
            Dim btnlineitemedit As LinkButton

            If (Not e.Row.Cells(Me.GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(BTN_EDIT_LINE_ITEM) Is Nothing) Then
                'Edit Button argument changed to id
                btnlineitemedit = CType(e.Row.Cells(Me.GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(BTN_EDIT_LINE_ITEM), LinkButton)


                btnlineitemedit.Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_LINE_ITEM_AMOUNT), Decimal))
                btnlineitemedit.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnlineitemedit.CommandName = "selectAction"

            End If


            If (Not e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) Is Nothing) Then
                'Edit Button argument changed to id
                btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnEditItem.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME

            End If

            e.Row.Cells(Me.GRID_COL_SERVICE_CENTERID_ID_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_SERVICE_CENTER), String)
            e.Row.Cells(Me.GRID_COL_INVOICE_NUMBER_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_NUMBER), String)
            e.Row.Cells(Me.GRID_COL_INVOICE_AMOUNT_IDX).Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_AMOUNT), Decimal))
            e.Row.Cells(Me.GRID_COL_INVOICE_DATE_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_DATE), String)
            e.Row.Cells(Me.GRID_COL_INVOICE_STATUS_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_STATUS), String)

            If (Not e.Row.Cells(Me.GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST) Is Nothing) Then
                'Delete Button argument changed to id
                btnDeleteItem = CType(e.Row.Cells(Me.GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                btnDeleteItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))
                btnDeleteItem.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME

            End If

            'e.Row.Cells(Me.GRID_COL_INVOICE_GRP_DETAIL_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INV_GRP_DETAIL_ID), Byte()))
            e.Row.Cells(Me.GRID_COL_INVOICEID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))


        End If
    End Sub

    Private Sub cboPgSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            If Not Me.State.DetailSearchDv Is Nothing Then
                Me.State.PageIndex = NewCurrentPageIndex(InvoicesGrid, Me.State.DetailSearchDv.Count, Me.State.PageSize)
            Else
                Me.State.PageIndex = NewCurrentPageIndex(InvoicesGrid, Me.dvcount, Me.State.PageSize)
            End If
            Me.InvoicesGrid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Invoices Modal Page"
#Region "Button Clicks"


    Private Sub btninveditSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditInvSave.Click
        Try


            PopulateInvoiceBOsfrommodal()
            If (Me.State.InvoiceBO.IsDirty OrElse Me.State.InvoiceBO.IsFamilyDirty) Then
                Me.State.HasDataChanged = True
                Me.State.InvoiceBO.Save()
                Me.State.HasDataChanged = False

                Me.PopulateGrid()

                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

            Else

                Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNewInvCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewInvCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btncancelSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelSearch.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
            Me.ReconciledInvoiceSearchgv.DataSource = Me.State.ReconInvsearchDV
            Me.ReconciledInvoiceSearchgv.DataBind()
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnsearch_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.moMessageController.Clear()
            If (Me.ddlVendorName.SelectedIndex = 0 AndAlso
                Me.txtInvoiceAmount.Text = "" AndAlso
                Me.txtInvoiceDate.Text = "" AndAlso
                Me.txtInvoiceNumber.Text = "") Then

                Me.moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Me.mdlPopup.Show()
            Else

                Me.State.PageIndex = 0
                Me.State.ReconInvsearchDV = Nothing
                Me.State.IsGridVisible = True
                ControlMgr.SetVisibleControl(Me, ReconciledInvoiceSearchgv, True)

                Me.PopulateReconciledInvoicesGrid()

                If Me.State.ReconInvsearchDV Is Nothing Then
                    ControlMgr.SetVisibleControl(Me, trpgsize, False)
                    Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                    Me.mdlPopup.Show()
                Else ' If Search Results are retrieved
                    ControlMgr.SetVisibleControl(Me, trpgsize, True)
                    btnDiv.Visible = True
                    Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"


                    Me.mdlPopup.Show()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub

    Public Sub btnNewItemAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewItemAdd.Click

        Try
            If Me.State.IsNew Then
                Me.State.InvgrpBO.Save()
                ControlMgr.SetVisibleControl(Me, btnSave, True)
            End If
            Me.State.IsNew = False
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
                            Me.State.selectedReconciliationId = New Guid(CType((row)(dal.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
                            Me.State.MyBO = New InvoiceGroupDetail()
                            PopulateBOProperty(Me.State.MyBO, "InvoiceGroupId", Me.State.InvgrpBO.Id)
                            PopulateBOProperty(Me.State.MyBO, "InvoiceReconciliationId", Me.State.selectedReconciliationId)

                            Me.State.MyBO.Save()
                        Next
                    End If

                    'Exit For
                End If
            Next
            Me.State.InvgrpId = Me.State.InvgrpBO.Id
            Me.State.totalamount = 0
            PopulateGrid()
            Me.txtgroupcount.Text = CStr(Me.State.groupcount)
            Me.txttotalamount.Text = GetAmountFormattedString(Me.State.totalamount)

            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            Me.mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Modal Grid"
    Protected Sub PopulateReconciledInvoicesGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.ReconInvsearchDV Is Nothing) Then
                Dim sortBy As String

                Me.State.ReconInvsearchDV = InvoiceGroupDetail.getReconciledInvoicesList(Me.State.ServiceCenter,
                                                                       Me.State.Invoicenum,
                                                                       Me.State.InvoiceAmount,
                                                                       Me.State.InvoicestatusId,
                                                                       Me.State.InvoiceDate
                                                                     )

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.ReconInvsearchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.ReconciledInvoiceSearchgv.AutoGenerateColumns = False
            Me.ReconciledInvoiceSearchgv.PageSize = Me.State.modalpgsize



            SetPageAndSelectedIndexFromGuid(Me.State.ReconInvsearchDV, Me.State.InvoiceId, Me.ReconciledInvoiceSearchgv, Me.State.PageIndex)

            Me.ReconciledInvoiceSearchgv.DataSource = Me.State.ReconInvsearchDV

            Me.ReconciledInvoiceSearchgv.AllowSorting = False
            Me.ReconciledInvoiceSearchgv.DataBind()
            Me.State.PageIndex = Me.InvoicesGrid.PageIndex
            Me.TranslateGridHeader(ReconciledInvoiceSearchgv)

            If Me.State.ReconInvsearchDV.Count > 0 Then
                Me.btnDiv.Visible = True
                divpgsize.Visible = True
                If Me.ReconciledInvoiceSearchgv.Visible Then
                    Me.lblRecordCount.Text = Me.State.ReconInvsearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                Me.moMessageController.AddErrorAndShow(Message.MSG_NO_RECORDS_FOUND)
                Me.lblRecordCount.Text = Me.State.ReconInvsearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub
    Private Sub ReconciledInvoiceSearchgv_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ReconciledInvoiceSearchgv.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub


    Private Sub ReconciledInvoiceSearchgv_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReconciledInvoiceSearchgv.PageIndexChanged
        Try
            Me.State.PageIndex = ReconciledInvoiceSearchgv.PageIndex
            PopulateReconciledInvoicesGrid()
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub

    Private Sub ReconciledInvoiceSearchgv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ReconciledInvoiceSearchgv.PageIndexChanging
        Try
            ReconciledInvoiceSearchgv.PageIndex = e.NewPageIndex
            State.PageIndex = InvoicesGrid.PageIndex
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub
    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPgSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            Me.State.PageIndex = NewCurrentPageIndex(InvoicesGrid, State.DetailSearchDv.Count, State.PageSize)
            Me.InvoicesGrid.PageIndex = Me.State.PageIndex
            Me.PopulateReconciledInvoicesGrid()
            mdlPopup.Show()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub

    Private Sub ReconciledInvoiceSearchgv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ReconciledInvoiceSearchgv.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim radiobtn As RadioButton

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then


                e.Row.Cells(Me.GRID_COL_VENDOR_ID_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_SERVICE_CENTER), String)
                e.Row.Cells(Me.GRID_COL_INV_NUM_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_NUMBER), String)
                e.Row.Cells(Me.GRID_COL_INV_AMOUNT_IDX).Text = GetAmountFormattedToVariableString(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_AMOUNT), Decimal))
                e.Row.Cells(Me.GRID_COL_INV_DATE_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_DATE), String)
                e.Row.Cells(Me.GRID_COL_INV_STATUS_IDX).Text = CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_STATUS), String)
                e.Row.Cells(Me.GRID_COL_INV_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceGroupDetailSearchDV.COL_INVOICE_ID), Byte()))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"
    Protected Sub ClearSearch()
        Try
            Me.ddlVendorName.SelectedIndex = 0
            Me.txtInvoiceNumber.Text = String.Empty
            Me.txtInvoiceAmount.Text = String.Empty
            Me.txtInvoiceDate.Text = String.Empty
            Me.ddlInvoicestatus.SelectedIndex = 0
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.ServiceCenter = Me.GetSelectedItem(Me.ddlVendorName)
            Me.State.Invoicenum = Me.txtInvoiceNumber.Text
            Me.State.InvoiceAmount = Me.txtInvoiceAmount.Text
            Me.State.InvoicestatusId = Me.GetSelectedItem(Me.ddlInvoicestatus)
            Me.State.InvoiceDate = Me.txtInvoiceDate.Text

            If Not Me.txtInvoiceAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.txtInvoiceAmount.Text, dblAmount) Then
                    Me.moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR, True)
                    Return False
                Else
                    Me.State.InvoiceAmountCulture = Me.txtInvoiceAmount.Text.Trim
                    Me.State.InvoiceAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                Me.State.InvoiceAmount = Me.txtInvoiceAmount.Text
            End If

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function
    Protected Sub PopulateInvoiceBOsfrommodal()
        Try
            With Me.State.InvoiceBO
                Me.PopulateBOProperty(Me.State.InvoiceBO, "InvoiceStatusId", Me.ddlInvoicestatus)
                Me.PopulateBOProperty(Me.State.InvoiceBO, "ServiceCenterId", Me.ddlVendorName)
                Me.PopulateBOProperty(Me.State.InvoiceBO, "InvoiceNumber", Me.txtInvoiceNumber.Text.Trim())
                Me.PopulateBOProperty(Me.State.InvoiceBO, "InvoiceAmount", Me.txtInvoiceAmount.Text.Trim())
                Me.PopulateBOProperty(Me.State.InvoiceBO, "InvoiceDate", Me.txtInvoiceDate.Text.Trim())

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#End Region

#Region "Line Items Modal page"

#Region "LineItems Grid"
    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Lineitemsgv.ItemDataBound

        BaseItemBound(source, e)

    End Sub
    Private Sub Lineitemsgv_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Lineitemsgv.ItemCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moMessageController)
        End Try
    End Sub

    Private Sub Lineitemsgv_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Lineitemsgv.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.Lineitemsgv.CurrentPageIndex = Me.State.PageIndex
            Me.PopulateLineitemsGrid()
            Me.Lineitemsgv.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.mdlLineItem.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub LIneitemsgv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Lineitemsgv.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)



            If (itemType = ListItemType.Item OrElse
                itemType = ListItemType.AlternatingItem OrElse
                itemType = ListItemType.SelectedItem) Then

                Dim lineitemdetailedit As ImageButton
                Dim lineitemdelete As ImageButton
                lineitemdetailedit = CType(e.Item.Cells(Me.GRID_COL_LINE_EDIT_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                lineitemdetailedit.Visible = False
                lineitemdelete = CType(e.Item.Cells(Me.GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                lineitemdelete.Visible = False

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_TYPE))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_DESCRIPTION))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_LINE_ITEM_AMT_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_VENDOR_SKU_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_VENDOR_SKU_DESC_IDX), dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU_DESC))
                e.Item.Cells(0).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))

                If (Not dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID) Is String.Empty) Then
                    e.Item.Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))
                End If

                Dim invoiceitemBO As InvoiceItem
                Dim claimauthBO As ClaimAuthorization
                Me.State.invitemid = New Guid(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                invoiceitemBO = New InvoiceItem(Me.State.invitemid)
                claimauthBO = New ClaimAuthorization(invoiceitemBO.ClaimAuthorizationId)
                If (claimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED Or
                             claimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED) Then
                    lineitemdetailedit.Visible = True
                    lineitemdelete.Visible = True
                End If

            ElseIf (itemType = ListItemType.EditItem) Then
                e.Item.Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID), Byte()))
                If (Not dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID) Is Nothing AndAlso Not String.IsNullOrEmpty(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID).ToString())) Then
                    e.Item.Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_AUTHORIZATION_ID), Byte()))
                End If

                CType(e.Item.Cells(Me.GRID_COL_LINE_ITEM_AMT_IDX).FindControl(Me.LINE_AMT_CONTROL_NAME), TextBox).Text = If(Me.State.InvoiceItemBO.Amount Is Nothing, String.Empty, GetAmountFormattedString(CType(Me.State.InvoiceItemBO.Amount, Decimal)))
                CType(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(Me.CLAIM_NUMBER_CONTROL_NAME), TextBox).Text = If(Me.State.ClaimBO Is Nothing, String.Empty, Me.State.ClaimBO.ClaimNumber.ToString())
                CType(e.Item.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX).FindControl(Me.AUTH_NUMBER_CONTROL_NAME), TextBox).Text = If(Me.State.ClaimauthBO Is Nothing, String.Empty, Me.State.ClaimauthBO.AuthorizationNumber.ToString())
                'If Not Me.State.InvoiceItemBO.VendorSku Is Nothing Then
                '    CType(e.Item.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), TextBox).Text = Me.State.InvoiceItemBO.VendorSku
                'End If
                CType(e.Item.Cells(Me.GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(Me.VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text = If(Me.State.InvoiceItemBO.VendorSkuDescription Is Nothing, String.Empty, Me.State.InvoiceItemBO.VendorSkuDescription)

                Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)

                Dim autocompleteext As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl("AutoCompleteExtender1"), AjaxControlToolkit.AutoCompleteExtender)
                autocompleteext.ContextKey = Me.State.InvoiceBO.ServiceCenter.Code



            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Lineitemsgv.ItemCommand
        Try
            'Editing Grid populates modal popup with invoice line item  details info
            Dim index As Integer = e.Item.ItemIndex
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then

                Me.hdnclaimnum.Value = ""
                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                Me.State.IsNew = False

                Me.State.IsEditMode = True

                Me.State.Invoiceitemid = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).Text)
                Me.State.ClaimAuthorizationId = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text)

                Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)
                Me.State.InvoiceItemBO = New InvoiceItem(Me.State.Invoiceitemid)

                Me.State.ClaimauthBO = New ClaimAuthorization(Me.State.InvoiceItemBO.ClaimAuthorizationId)
                Me.State.claimid = Me.State.ClaimauthBO.ClaimId
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimid)

                If Me.State.InvoiceBO.CanModifyClaimAuthorization(Me.State.InvoiceItemBO.ClaimAuthorizationId) Then

                    PopulateLineitemsGrid()

                    Me.State.PageIndex = Lineitemsgv.CurrentPageIndex

                    Dim editbtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                    editbtn.Visible = False

                    Dim deletebtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                    deletebtn.Visible = False


                    Dim serviceclasslist As DropDownList = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList)
                    Populateserviceclassdropdown(serviceclasslist)

                    Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(Me.SERVICE_TYPE_CONTROL_NAME), DropDownList)
                    Populateservicetypedropdown(serviceTypeList)

                    Dim vendorskulist As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), DropDownList)
                    Populatevendorskudropdown(vendorskulist, Me.State.InvoiceItemBO.ServiceClassId, Me.State.InvoiceItemBO.ServiceTypeId, Me.State.ClaimauthBO.ClaimId)

                    'If serviceclasslist.Items.FindByText((LookupListNew.GetCodeFromId("SVCCLASS", Me.State.InvoiceBO.ServiceCenterId)) Then
                    Try
                        Me.SetSelectedItem(serviceclasslist, Me.State.InvoiceItemBO.ServiceClassId)
                        Me.SetSelectedItem(serviceTypeList, Me.State.InvoiceItemBO.ServiceTypeId)
                        Me.SetSelectedItem(vendorskulist, Me.State.InvoiceItemBO.VendorSku)
                    Catch ex As Exception
                    End Try

                    Me.mdlLineItem.Show()
                Else
                    Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_CANNOT_MODIFY_CLAIM_AUTHORIZATION)

                End If
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then

                Me.State.Invoiceitemid = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).Text)
                Me.State.ClaimAuthorizationId = New Guid(Lineitemsgv.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text)
                Me.State.InvoiceItemBO = New InvoiceItem(CType(Me.State.Invoiceitemid, Guid))
                Me.State.ClaimauthBO = New ClaimAuthorization(Me.State.InvoiceItemBO.ClaimAuthorizationId)
                Try
                    If (Me.State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__VOID Or _
                              Me.State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__PENDING) Then

                        Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    Else
                        Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_LINE_ITEM_CANNOT_BE_DELETED)
                    End If
                Catch ex As Exception
                    Me.State.InvoiceItemBO.RejectChanges()

                    Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.molineitemmsgcontroller)
            PopulateLineitemsGrid()
            Me.mdlLineItem.Show()
        End Try
    End Sub

    Protected Sub PopulateLineitemsGrid()
        Try


            Dim sortBy As String
            Me.State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(Me.State.InvoiceId)

            Me.Lineitemsgv.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)
            Me.State.PageIndex = Me.Lineitemsgv.CurrentPageIndex

            Me.Lineitemsgv.DataSource = Me.State.lineitemsDV

            Me.Lineitemsgv.AllowSorting = False
            Me.Lineitemsgv.DataBind()

            Session("recCount") = Me.State.lineitemsDV.Count
            If Me.State.lineitemsDV.Count > 0 Then

                If Me.Lineitemsgv.Visible Then
                    Me.lblRecordCount.Text = Me.State.lineitemsDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else

                If Me.Lineitemsgv.Visible Then
                    Me.lblRecordCount.Text = Me.State.lineitemsDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"
    Protected Sub btnnewlineitem_click(ByVal Sender As Object, ByVal e As System.EventArgs) Handles btnnew_lineitem.Click
        Try
            Me.molineitemmsgcontroller.Clear()
            Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)
            If CType(Me.State.InvoiceBO.CanAddAuthorization, Boolean) Then
                Me.State.IsNew = True
                Me.State.IsEditMode = True
                Me.State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(Me.State.InvoiceId)

                Me.State.InvoiceItemBO = New InvoiceItem()
                Me.State.ClaimBO = Nothing
                Me.State.ClaimauthBO = Nothing

                Me.State.Invoiceitemid = Me.State.InvoiceItemBO.Id

                Me.State.lineitemsDV = Me.State.MyBO.GetNewDataViewRow(Me.State.lineitemsDV, Me.State.InvoiceItemBO)

                Me.Lineitemsgv.DataSource = Me.State.lineitemsDV

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)

                Me.SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)
                Me.Lineitemsgv.DataBind()
                Dim serviceclasslist As DropDownList = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Populateserviceclassdropdown(serviceclasslist)
                Me.SetSelectedItem(serviceclasslist, Me.State.InvoiceItemBO.ServiceClassId)

                Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_LINE_ITEM_AMT_IDX, Me.LINE_AMT_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)
                Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_CLAIM_NUMBER_IDX, Me.CLAIM_NUMBER_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)
                Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_AUTHORIZATION_NUMBER_IDX, Me.AUTH_NUMBER_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)
                'Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_VENDOR_SKU_IDX, Me.VENDOR_SKU_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)
                Me.SetFocusOnEditableFieldInGrid(Me.Lineitemsgv, Me.GRID_COL_VENDOR_SKU_DESC_IDX, Me.VENDOR_SKU_DESC_CONTROL_NAME, Me.Lineitemsgv.EditItemIndex)

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                Dim editbtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                editbtn.Visible = False

                Dim deletebtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                deletebtn.Visible = False
                Me.mdlLineItem.Show()

            Else
                Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_CANNOT_ADD_CLAIM_AUTHORIZATION)
                PopulateLineitemsGrid()

                mdlLineItem.Show()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.molineitemmsgcontroller)
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try
    End Sub

    Protected Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave_lineitem.Click
        Try
            Me.molineitemmsgcontroller.Clear()
            If Me.State.IsNew Then
                Dim lineitemnumber As Integer

                Me.State.InvoiceItemBO = Nothing
                Me.State.CertBO = Nothing
                Me.State.CertItemBO = Nothing
                Me.State.ClaimauthBO = Nothing
                Me.State.ClaimBO = Nothing

                Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)
                'Me.State.MyBO = New InvoiceGroupDetail(Me.State.InvoicegrpdetailId)
                Me.SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)

                Dim serviceclasslist As DropDownList = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(Me.SERVICE_TYPE_CONTROL_NAME), DropDownList)
                Dim vendorsku As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(Me.VENDOR_SKU_CONTROL_NAME), DropDownList).SelectedValue

                Dim authnumber As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX).FindControl(Me.AUTH_NUMBER_CONTROL_NAME), TextBox).Text
                Dim claimnumber As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(Me.CLAIM_NUMBER_CONTROL_NAME), TextBox).Text
                Dim amount As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_AMOUNT_IDX).FindControl(Me.LINE_AMT_CONTROL_NAME), TextBox).Text
                Dim vendorskudesc As String = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESC_IDX).FindControl(Me.VENDOR_SKU_DESC_CONTROL_NAME), TextBox).Text
                Dim dal As InvoiceGroupDetailDAL

                Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(Me.State.InvoiceBO.ServiceCenter.Code)

                If Not claimnumber.Equals(String.Empty) Then
                    dv.RowFilter = "claim_number='" & claimnumber & "'"

                    Me.State.Authorizationid = New Guid(CType(dv(0)(dal.COL_NAME_AUTHORIZATION_ID), Byte()))

                    Dim dv1 As DataView = InvoiceGroupDetail.Getmaxlineitemnumber(Me.State.InvoiceId)
                    lineitemnumber = CType(dv1(0)(dal.COL_NAME_LINE_ITEM_NUMBER), Integer)

                    Me.State.ClaimauthBO = New ClaimAuthorization(Me.State.Authorizationid)

                    If Me.State.ClaimauthBO.ServiceCenterId = Me.State.InvoiceBO.ServiceCenterId AndAlso _
                         Me.State.ClaimauthBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED Then


                        Me.State.InvoiceItemBO = New InvoiceItem()
                        Me.State.InvoiceItemBO.ServiceClassId = Me.GetSelectedItem(serviceclasslist)
                        Me.State.InvoiceItemBO.ServiceTypeId = Me.GetSelectedItem(serviceTypeList)
                        Me.State.InvoiceItemBO.Amount = CType(amount, DecimalType)
                        Me.State.InvoiceItemBO.ClaimAuthorizationId = Me.State.Authorizationid
                        Me.State.InvoiceItemBO.InvoiceId = Me.State.InvoiceId
                        Me.State.InvoiceItemBO.LineItemNumber = lineitemnumber + 1
                        Me.State.InvoiceItemBO.VendorSku = vendorsku
                        Me.State.InvoiceItemBO.VendorSkuDescription = vendorskudesc
                        'Adding new invoice item in invoice item table 
                        Me.State.InvoiceItemBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)

                        'After Adding invoice item, balancing the invoice
                        Me.State.InvoiceBO.Balance.Execute()
                        'Reload the main grid
                        PopulateGrid()
                        Me.mdlLineItem.Hide()

                    Else

                        'Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_AUTHORIZATION_SERVICE_CENTER, True)
                        'Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_AUTHORIZATION_SERVICE_CENTER, True)
                        Throw New GUIException(Message.MSG_AUTHORIZATION_SERVICE_CENTER, Message.MSG_AUTHORIZATION_SERVICE_CENTER)
                        Me.mdlLineItem.Show()
                    End If
                Else
                    PopulateLineitemsGrid()
                    'Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
                    Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
                    Me.mdlLineItem.Show()
                End If

            Else
                PopulateLineitemBOFromForm()


                If (Me.State.InvoiceItemBO.IsDirty) Then
                    Me.State.InvoiceItemBO.Save()

                    Me.molineitemmsgcontroller.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    mdlLineItem.Show()

                    Me.ReturnFromEditing()
                    ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                    ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                    ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                    Me.mdlLineItem.Show()
                    Me.State.lineitemsDV = Nothing
                Else
                    Me.molineitemmsgcontroller.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)

                    Me.ReturnFromEditing()
                    ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
                    ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
                    ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
                    Me.mdlLineItem.Show()
                End If
                'Reload the main grid
                PopulateGrid()
            End If
        Catch ex As Exception

            Me.HandleErrors(ex, Me.molineitemmsgcontroller)
            hdnclaimnum.Value = ""
            PopulateLineitemsGrid()
            Me.mdlLineItem.Show()
        End Try

    End Sub
    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnundo_lineitem.Click

        Try
            Me.molineitemmsgcontroller.Clear()
            Me.Lineitemsgv.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

            ReturnFromEditing()
            ControlMgr.SetVisibleControl(Me, btnnew_lineitem, True)
            ControlMgr.SetVisibleControl(Me, btnsave_lineitem, False)
            ControlMgr.SetVisibleControl(Me, btnundo_lineitem, False)
            Me.mdlLineItem.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.molineitemmsgcontroller)
        End Try

    End Sub
    Private Sub standardlineitems_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddstandardLineItems.Click
        Try
            Me.molineitemmsgcontroller.Clear()
            Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)
            If CType(Me.State.InvoiceBO.CanAddAuthorization, Boolean) Then
                Me.State.IsNew = True
                Me.State.IsEditMode = True
                Me.State.lineitemsDV = InvoiceGroupDetail.getLineItemsList(Me.State.InvoiceId)

                Me.State.InvoiceItemBO = New InvoiceItem()
                Me.State.Invoiceitemid = Me.State.InvoiceItemBO.Id
                Me.State.ClaimBO = Nothing
                Me.State.ClaimauthBO = Nothing

                Me.State.lineitemsDV = Me.State.MyBO.GetNewDataViewRow(Me.State.lineitemsDV, Me.State.InvoiceItemBO)

                Me.Lineitemsgv.DataSource = Me.State.lineitemsDV

                Me.SetPageAndSelectedIndexFromGuid(Me.State.lineitemsDV, Me.State.Invoiceitemid, Me.Lineitemsgv, Me.State.PageIndex, Me.State.IsEditMode)
                Me.Lineitemsgv.DataBind()

                Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).Text = Me.State.Invoiceitemid.ToString()
                Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_CLAIM_AUTHORIZATION_ID_IDX).Text = Me.State.ClaimAuthorizationId.ToString()

                Dim dal As New InvoiceGroupDetailDAL

                Dim editbtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_ID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                editbtn.Visible = False

                Dim deletebtn As ImageButton = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_DELETE_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                deletebtn.Visible = False

                Dim serviceclasslist As DropDownList = CType(Me.Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_TYPE_IDX).FindControl(Me.SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim ds As DataSet = Me.State.MyBO.Addstandardlineitems()
                Populateserviceclassdropdown(serviceclasslist)
                Me.SetSelectedItem(serviceclasslist, New Guid(CType(ds.Tables(0).Rows(0)(SERVICE_CLASS_ID), Byte())))
                serviceclasslist.Enabled = False

                Dim serviceTypeList As DropDownList = CType(Lineitemsgv.Items(Lineitemsgv.EditItemIndex).Cells(Me.GRID_COL_LINE_ITEM_DESCRIPTION_IDX).FindControl(Me.SERVICE_TYPE_CONTROL_NAME), DropDownList)

                Populateservicetypedropdown(serviceTypeList)
                Me.SetSelectedItem(serviceTypeList, New Guid(CType(ds.Tables(0).Rows(0)(SERVICE_TYPE_ID), Byte())))
                serviceTypeList.Enabled = False

                ControlMgr.SetVisibleControl(Me, btnnew_lineitem, False)
                ControlMgr.SetVisibleControl(Me, btnsave_lineitem, True)
                ControlMgr.SetVisibleControl(Me, btnundo_lineitem, True)

                mdlLineItem.Show()
            Else
                Me.molineitemmsgcontroller.AddErrorAndShow(Message.MSG_CANNOT_ADD_CLAIM_AUTHORIZATION)
                PopulateLineitemsGrid()
                mdlLineItem.Show()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.molineitemmsgcontroller)
            PopulateLineitemsGrid()
            mdlLineItem.Show()
        End Try
    End Sub

#End Region

#End Region


#Region "Helper Functions"
    <System.Web.Services.WebMethod(), System.Web.Script.Services.ScriptMethod()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As System.Collections.Generic.List(Of String)


        Dim listitems As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)
        ' Dim listItems As New ArrayList


        Dim dv As DataView = InvoiceGroupDetail.Getlineiteminsertvalues(contextKey)

        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            dv.RowFilter = "Claim_Number like '" & prefixText & "%'"
            For Each dr As DataRow In dv.Table.Rows
                listitems.Add(dr(CLAIM_NUMBER).ToString())
            Next
        End If

        Return (listitems)
    End Function

    Protected Sub populatevendorsku(ByVal sender As Object, ByVal e As EventArgs)

        If Not String.IsNullOrEmpty(hdnclaimnum.Value) Then
            If Not Me.State.claimnumber = Me.hdnclaimnum.Value Then
                Me.State.rownumber = CType(hdnrowNumber.Value, Integer)
                Dim ddlServiceClass As DropDownList = CType(Lineitemsgv.Items(Me.State.rownumber - 3).FindControl(SERVICE_CLASS_CONTROL_NAME), DropDownList)
                Dim ddlServicetype As DropDownList = CType(Lineitemsgv.Items(Me.State.rownumber - 3).FindControl(SERVICE_TYPE_CONTROL_NAME), DropDownList)
                Dim vendorsku As DropDownList = CType(Lineitemsgv.Items(Me.State.rownumber - 3).FindControl(VENDOR_SKU_CONTROL_NAME), DropDownList)
                Dim authnumber As TextBox = CType(Lineitemsgv.Items(Me.State.rownumber - 3).FindControl(AUTH_NUMBER_CONTROL_NAME), TextBox)
                Dim skuDescription As TextBox = CType(Lineitemsgv.Items(Me.State.rownumber - 3).FindControl(VENDOR_SKU_DESC_CONTROL_NAME), TextBox)
                vendorsku.Items.Clear()
                authnumber.Text = String.Empty
                skuDescription.Text = String.Empty

                Me.State.claimnumber = Me.hdnclaimnum.Value
                Me.State.serviceclassid = New Guid(ddlServiceClass.SelectedValue)
                Me.State.servicetypeid = New Guid(ddlServicetype.SelectedValue)

                Dim dv As DataView = Me.State.MyBO.Getlineiteminsertvalues(Me.State.InvoiceBO.ServiceCenter.Code)

                dv.RowFilter = "claim_number = '" & Me.State.claimnumber & "'"

                Me.State.claimid = GuidControl.ByteArrayToGuid(dv(0)("claim_id"))
                authnumber.Text = dv(0)(AUTHORIZATION_NUMBER).ToString()
                Me.State.Authorizationid = GuidControl.ByteArrayToGuid(dv(0)("claim_authorization_id"))

                Populatevendorskudropdown(vendorsku, Me.State.serviceclassid, Me.State.servicetypeid, Me.State.claimid)


                hdnclaimnum.Value = ""
            End If
        End If
    End Sub

    Private Sub Populatevendorskudropdown(ByVal vendorsku As DropDownList, ByVal serviceclassid As Guid, ByVal servicetypeid As Guid, ByVal Claimid As Guid)

        Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Claimid)
        Me.State.InvoiceBO = New Invoice(Me.State.InvoiceId)
        Dim servcenter As New ServiceCenter(Me.State.InvoiceBO.ServiceCenterId)
        Dim equipmentId As Guid, equipmentclassId As Guid, conditionId As Guid
        If (Me.State.ClaimBO.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If (Not Me.State.ClaimBO.ClaimedEquipment Is Nothing) Then
                equipmentId = Me.State.ClaimBO.ClaimedEquipment.EquipmentId
                equipmentclassId = Me.State.ClaimBO.ClaimedEquipment.EquipmentBO.EquipmentClassId
                conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)

            End If
        End If

        Dim dv1 As PriceListDetail.PriceListResultsDV = PriceListDetail.GetPricesForServiceType(Me.State.ClaimBO.CompanyId, servcenter.Code, _
                                                                                                   Me.State.ClaimBO.RiskTypeId, Me.State.ClaimBO.LossDate.Value, _
                                                                                                   Me.State.ClaimBO.Certificate.SalesPrice.Value, _
                                                                                                   serviceclassid, _
                                                                                                    servicetypeid, _
                                                                                                 equipmentclassId, equipmentId, conditionId, Me.State.ClaimBO.Dealer.Id, String.Empty)


        Me.State.vendorskudv = dv1
        If Not Me.State.vendorskudv Is Nothing Then
            ElitaPlusPage.BindListTextToDataView(vendorsku, Me.State.vendorskudv, COL_VENDOR_SKU, COL_VENDOR_SKU, True)

        Else
            Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_VENDOR_SKU_LIST_NOT_FOUND)
        End If

    End Sub

#End Region

End Class
