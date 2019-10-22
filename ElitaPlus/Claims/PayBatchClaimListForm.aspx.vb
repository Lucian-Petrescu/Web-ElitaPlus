Option Strict Off

Imports Microsoft.VisualBasic
Imports System.Text
Imports AjaxControlToolkit
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class PayBatchClaimListForm
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

    Public Const URL As String = "~/Claims/PayBatchClaimListForm.aspx"

    Public Const GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX As Integer = 0
    Public Const GRID_INV_COL_BATCH_STATUS_IDX As Integer = 1
    Public Const GRID_INV_COL_ADD_IDX As Integer = 2
    Public Const GRID_INV_COL_EDIT_IDX As Integer = 3
    Public Const GRID_INV_COL_DELETE_IDX As Integer = 4
    Public Const GRID_INV_COL_INVOICE_NUMBER_IDX As Integer = 5
    Public Const GRID_INV_COL_SERVICE_CENTER_IDX As Integer = 6
    Public Const GRID_INV_COL_INVOICE_DATE_IDX As Integer = 7
    Public Const GRID_INV_COL_INV_AMOUNT_IDX As Integer = 8
    Public Const GRID_INV_COL_INVOICE_STATUS_IDX As Integer = 9
    Public Const GRID_INV_COL_INVOICE_COMMENTS_IDX As Integer = 10
    Public Const GRID_INV_COL_BATCH_NUMER_IDX As Integer = 11
    Public Const GRID_INV_COL_SERVICE_CENTER_ID_IDX As Integer = 12
    Public Const GRID_INV_COL_INVOICE_STATUS_ID_IDX As Integer = 13
    Public Const GRID_INV_COL_BONUS_PAID_IDX As Integer = 14

    Public Const GRID_INV_TOTAL_COLUMNS As Integer = 15

    Public Const GRID_INV_COM_EDIT As String = "EditClaimsAction"
    Public Const GRID_INV_COM_ADD As String = "AddClaimsAction"
    Public Const GRID_INV_COM_DELETE As String = "DeleteInvoiceAction"

    Public Const GRID_CLAIM_COL_INVOICE_TRANS_NUMBER_IDX As Integer = 0
    Public Const GRID_CLAIM_COL_ADD_IDX As Integer = 1
    Public Const GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX As Integer = 2
    Public Const GRID_CLAIM_COL_CLAIM_ID_IDX As Integer = 3
    Public Const GRID_CLAIM_COL_AUTHORIZATION_NUMBER_IDX As Integer = 4
    Public Const GRID_CLAIM_COL_CLAIM_NUMBER_IDX As Integer = 5
    Public Const GRID_CLAIM_COL_CUSTOMER_NAME_IDX As Integer = 6
    Public Const GRID_CLAIM_COL_SERVICE_CENTER_NAME_IDX As Integer = 7
    Public Const GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX As Integer = 8
    Public Const GRID_CLAIM_COL_RESERVE_AMOUNT_IDX As Integer = 9
    Public Const GRID_CLAIM_COL_TOTAL_BONUS_IDX As Integer = 10
    Public Const GRID_CLAIM_COL_BATCH_NUMBER_IDX As Integer = 11
    Public Const GRID_CLAIM_COL_PAYMENT_AMOUNT_IDX As Integer = 12
    Public Const GRID_CLAIM_COL_REPAIR_DATE_IDX As Integer = 13
    Public Const GRID_CLAIM_COL_PICKUP_DATE_IDX As Integer = 14
    Public Const GRID_CLAIM_COL_SPARE_PARTS_IDX As Integer = 15
    Public Const GRID_CLAIM_COL_SELECTED_IDX As Integer = 16
    Public Const GRID_CLAIM_INVOICE_TRANS_DETAIL_IDX As Integer = 17
    Public Const GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_SELECTED_IDX As Integer = 18
    Public Const GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_NAME_IDX As Integer = 19

    Public Const GRID_CLAIM_TOTAL_COLUMNS As Integer = 17S

    Public Const GRID_CLAIM_ADD_CLAIM_CHECKBOX As String = "chkAddClaim"
    Public Const GRID_CLAIM_CHECK_ALL_CLAIM_CHECKBOX As String = "chkAllClaims"
    Public Const GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX As String = "chkExcludeDeductible"
    Public Const GRID_CLAIM_EXCLUDE_DED_ALL_CHECKBOX As String = "chkExcludeDedAll"

    Public Const BATCH_STATUS_INSERTED As String = "I"
    Public Const BATCH_STATUS_PROCESSING As String = "P"
    Public Const BATCH_STATUS_COMPLETED As String = "C"
    Public Const BATCH_STATUS_FAILED As String = "F"

    Public Const BATCH_STATUS_ICON_FAILED As String = "<img src=""../Navigation/Images/icons/error.gif""/>"
    Public Const BATCH_STATUS_ICON_INSERTED As String = "<img src=""../Navigation/Images/icons/other_icon.gif""/>"
    Public Const BATCH_STATUS_ICON_PROCESSING As String = "<img src=""../Navigation/Images/icons/reset_icon.gif""/>"
    Public Const BATCH_STATUS_ICON_COMPLETED As String = "<img src=""../Navigation/Images/icons/yes_icon.gif""/>"

    Public Const MAX_LIMIT As Integer = 1000

    Public Const CLAIM_STATUS_INSERTED As String = "I"
    Public Const CLAIM_STATUS_DELETED As String = "D"

    Public Const PAGETITLE As String = "PAY_INVOICE"
    Public Const PAGETAB As String = "CLAIM"

    Public Const COL_TAX1_COMPUTE_METHOD As String = "tax1_compute_method"
    Public Const COL_TAX2_COMPUTE_METHOD As String = "tax2_compute_method"
    Public Const COL_TAX3_COMPUTE_METHOD As String = "tax3_compute_method"
    Public Const COL_TAX4_COMPUTE_METHOD As String = "tax4_compute_method"
    Public Const COL_TAX5_COMPUTE_METHOD As String = "tax5_compute_method"
    Public Const COL_TAX6_COMPUTE_METHOD As String = "tax6_compute_method"
    Public COMPUTE_TYPE_MANUALLY As String = "I"

    Public Const MAX_MANUALLY_ENTERED_TAXES As Integer = 2
    Private Const Invoice_Tax_Type As String = "Invoice_Tax"
    Public Const CLAIM_TOOLTIP As String = "Claim can not be paid in Elita"

    Public Const INVOICE_STATUS_PAID As String = "P"
    Public Const INVOICE_STATUS_REJECTED As String = "R"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = InvoiceTrans.InvoiceTransSearchDV.COL_BATCH_STATUS
        Public selectedInvoiceTransId As Guid = Guid.Empty
        Public serviceCenterId As Guid
        Public invoiceNumber As String
        Public batchNumber As String
        Public invoiceAmount As Double
        Public IsInvoiceGridVisible As Boolean = False
        Public IsClaimGridVisible As Boolean = False
        Public searchInvoiceDV As InvoiceTrans.InvoiceTransSearchDV = Nothing
        Public searchClaimDV As Claim.ClaimsForBatchProcessDV = Nothing
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SearchClicked As Boolean
        Public ReturningFromProcessing As Boolean = False
        Public MyBO As InvoiceTrans
        Public bnoRow As Boolean = False
        Public InvoiceTaxType As String
        Public InvoiceTaxTypeID As Guid
        Public invoiceTypeId As Guid
        Public invoiceStatusId As Guid
        Public batchState As Boolean = False

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

#End Region

#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnExisting.ClientID)
        Me.MasterPage.MessageController.Clear()
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceAmount, Me.btnExisting)
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceNumber, Me.btnExisting)
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceDate, Me.btnExisting)
        Me.SetDefaultButton(Me.txtboxInvRecDt, Me.btnExisting)

        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.AddCalendar(Me.ImageButtonInvoiceDate, Me.TextBoxSearchInvoiceDate)
                Me.AddCalendar(Me.imgBtnInvRecDt, Me.txtboxInvRecDt)
                PopulateServiceCenterDropDown()
                PopulateInvoiceTypeDropDown()
                PopulateInvoiceStatusDropDown()
                ChkInvoiceTaxTypeAndValidate()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                ControlMgr.SetVisibleControl(Me, lblInvCreDt, False)
                ControlMgr.SetVisibleControl(Me, txtboxInvCtdDt, False)
                Me.TranslateGridHeader(Me.GridInvoices)
                Me.TranslateGridControls(Me.GridInvoices)
                Me.TranslateGridHeader(Me.GridClaims)
                Me.TranslateGridControls(Me.GridClaims)
                If Me.IsReturningFromChild = True Then
                    Me.State.MyBO = New InvoiceTrans(Me.State.selectedInvoiceTransId)
                    Me.PopulateSelection()
                End If
                Trace(Me, "")
            End If

            If Me.State.ReturningFromProcessing Then
                ClearAll()
                Me.SearchInvoiceTrans()
                Me.State.ReturningFromProcessing = False
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Me.State.searchClaimDV = Nothing

            Dim retObj As PayBatchClaimForm.ReturnType = CType(Me.ReturnedValues, PayBatchClaimForm.ReturnType)
            If Not retObj Is Nothing Then
                Me.State.selectedInvoiceTransId = retObj.InvoiceTransId
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall

        If Not CallingPar Is Nothing AndAlso CallingPar.ToString = "P" Then
            Me.State.ReturningFromProcessing = True
            Me.AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
            'Me.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)

        ElseIf Not CallingPar Is Nothing AndAlso CallingPar.ToString = "R" Then
            Me.State.ReturningFromProcessing = True
            Me.AddInfoMsg(ElitaPlusWebApp.Message.MSG_INVOICE_REJECTED_SUCCESS)
            'Me.DisplayMessage(Message.MSG_INVOICE_REJECTED_SUCCESS, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)

        End If
    End Sub


#End Region

#Region "Controlling Logic"

    Private Sub PopulateServiceCenterDropDown()

        Try
            'Me.BindListControlToDataView(Me.cboServiceCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                If ServiceCenters.Count > 0 Then
                    If Not ServiceCenterList Is Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            Me.cboServiceCenter.Populate(ServiceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub PopulateInvoiceTypeDropDown()

        Try
            'Me.BindListControlToDataView(Me.ddlInvTyp, LookupListNew.DropdownLookupList(LookupListNew.LK_INVTYP, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), , , True)
            Dim InvoiceType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INVTYP",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlInvTyp.Populate(InvoiceType.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub PopulateInvoiceStatusDropDown()

        Try
            'Me.BindListControlTDataView(Me.ddlInvStat, LookupListNew.DropdownLookupList(LookupListNew.LK_INVSTAT, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), , , True)
            Dim InvoiceStatus As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INVSTAT",
                                                                                                        languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlInvStat.Populate(InvoiceStatus.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Private Sub PopulateBOFromForm()
        Me.PopulateBOProperty(Me.State.MyBO, "SvcControlAmount", Me.TextBoxSearchInvoiceAmount)
        Me.PopulateBOProperty(Me.State.MyBO, "SvcControlNumber", Me.TextBoxSearchInvoiceNumber)
        Me.PopulateBOProperty(Me.State.MyBO, "ServiceCenterId", Me.cboServiceCenter)
        Me.PopulateBOProperty(Me.State.MyBO, "InvoiceTypeId", Me.ddlInvTyp)
        Me.PopulateBOProperty(Me.State.MyBO, "InvoiceStatusId", Me.ddlInvStat)
        Me.PopulateBOProperty(Me.State.MyBO, "BatchNumber", Me.TextBoxBatchNumber)
        Me.PopulateBOProperty(Me.State.MyBO, "InvoiceDate", Me.TextBoxSearchInvoiceDate)
        Me.PopulateBOProperty(Me.State.MyBO, "InvoiceReceivedDate", Me.txtboxInvRecDt)
        Me.PopulateBOProperty(Me.State.MyBO, "InvoiceCreatedDate", Me.txtboxInvCtdDt)

    End Sub

    Private Sub SearchInvoiceTrans(Optional ByVal pIndex As Integer = 0)

        Dim serviceCenterId As Guid
        Dim invoiceTypeId As Guid
        Dim invoiceStatusId As Guid

        Try
            Dim boolErr As Boolean = False

            'Means Error Exists
            If boolErr Then
                Me.MasterPage.MessageController.Show()
                Exit Sub
            End If

            If Not Me.cboServiceCenter.SelectedItem Is Nothing Then
                serviceCenterId = New Guid(Me.cboServiceCenter.SelectedValue)
            End If

            If Not Me.ddlInvTyp.SelectedItem Is Nothing Then
                invoiceTypeId = New Guid(Me.ddlInvTyp.SelectedValue)
            End If

            If Not Me.ddlInvStat.SelectedItem Is Nothing Then
                invoiceStatusId = New Guid(Me.ddlInvStat.SelectedValue)
            End If
            'Get list of invoice Trans records
            Dim strInvoicedt As String = String.Empty
            Dim strInvoicercvdDt As String = String.Empty
            Dim dt As DateTime
            If (Not Me.TextBoxSearchInvoiceDate.Text = String.Empty) Then
                dt = DateHelper.GetDateValue(Me.TextBoxSearchInvoiceDate.Text)
                strInvoicedt = DateHelper.GetEnglishDate(dt) ' For handling user culture specific dates (esp. Argentina spanish cultures) 
            End If

            If (Not Me.txtboxInvRecDt.Text = String.Empty) Then
                dt = DateHelper.GetDateValue(Me.txtboxInvRecDt.Text).ToString()
                strInvoicercvdDt = DateHelper.GetEnglishDate(dt) ' For handling user culture specific dates (esp. Argentina spanish cultures) 
            End If

            Me.State.searchInvoiceDV = InvoiceTrans.GetList(Me.TextBoxSearchInvoiceNumber.Text, serviceCenterId, ElitaPlusIdentity.Current.ActiveUser.Id, Me.TextBoxBatchNumber.Text, Me.TextBoxSearchInvoiceAmount.Text, strInvoicedt, invoiceTypeId, invoiceStatusId, strInvoicercvdDt)

            'If records exist, display the invoice trans grid to allow user to select. 
            ' If  no record, display message to enable user to create new list

            Me.State.serviceCenterId = serviceCenterId
            Me.State.invoiceTypeId = invoiceTypeId
            Me.State.invoiceStatusId = invoiceStatusId
            Me.State.batchNumber = Me.TextBoxBatchNumber.Text.Trim

            Me.State.invoiceNumber = Me.TextBoxSearchInvoiceNumber.Text

            ControlMgr.SetVisibleControl(Me, Me.GridInvoices, True)
            ControlMgr.SetVisibleControl(Me, Me.GridClaims, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, True)
            ControlMgr.SetVisibleControl(Me, Me.btnCancelSearch_WRITE, True)
            ControlMgr.SetVisibleControl(Me, lblInvCreDt, False)
            ControlMgr.SetVisibleControl(Me, txtboxInvCtdDt, False)


            Session("recCount") = Me.State.searchInvoiceDV.Count

            If Me.State.searchInvoiceDV.Count > 0 Then

                If Me.GridInvoices.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchInvoiceDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    Me.State.bnoRow = False
                End If
                Me.GridInvoices.DataSource = Me.State.searchInvoiceDV
                Me.GridInvoices.AllowSorting = False
                Me.GridInvoices.DataBind()
                If Me.State.InvoiceTaxType = Invoice_Tax_Type Then
                    Me.GridInvoices.Columns(GRID_INV_COL_BATCH_NUMER_IDX).Visible = True
                Else
                    Me.GridInvoices.Columns(GRID_INV_COL_BATCH_NUMER_IDX).Visible = False
                End If
                If Not GridInvoices.BottomPagerRow.Visible Then GridInvoices.BottomPagerRow.Visible = True

            Else
                If Me.GridInvoices.Visible Then
                    Me.State.bnoRow = True
                    Me.GridInvoices.DataSource = Me.State.searchInvoiceDV
                    Me.GridInvoices.AllowSorting = False
                    Me.GridInvoices.DataBind()
                    'CreateHeaderForEmptyGrid(GridInvoices, Me.State.SortExpression)
                    Me.lblRecordCount.Text = Me.State.searchInvoiceDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

            'If Me.GridInvoices.Visible Then
            '    Me.lblRecordCount.Text = Me.State.searchInvoiceDV.Count.ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            '    Me.State.bnoRow = False
            'End If

            'If Not Me.State.searchInvoiceDV Is Nothing Then
            '    Me.GridInvoices.PageIndex = pIndex
            '    Me.GridInvoices.DataSource = Me.State.searchInvoiceDV
            '    Me.GridInvoices.DataBind()
            '    If Me.State.searchInvoiceDV.Count = 0 Then
            '        Me.State.bnoRow = True
            '        CreateHeaderForEmptyGrid(GridInvoices, Me.State.SortExpression)
            '        Me.AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND)
            '    End If
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Private Sub ChkInvoiceTaxTypeAndValidate()

        Dim ds As DataSet
        Dim dr As DataRow
        Dim dc As DataColumn
        Dim invoiceTrans As New InvoiceTrans
        Dim taxtypeID As Guid
        Dim boolErr As Boolean = False
        Dim taxcolumnscnt As Integer = 0

        ' taxtype code - 4 = Manual...
        taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
        ds = invoiceTrans.CheckInvoiceTaxType(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, taxtypeID, Guid.Empty)

        If ds.Tables(0).Rows.Count > 0 Then
            For Each dr In ds.Tables(0).Rows

                taxcolumnscnt = CInt(IIf(dr(COL_TAX1_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX2_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX3_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX4_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX5_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX6_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))

                If taxcolumnscnt <> MAX_MANUALLY_ENTERED_TAXES Then
                    boolErr = True
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.ERR_TWO_TAXES_FOR_INVOICE_TAX_TYPE, True)
                    Me.State.InvoiceTaxType = Invoice_Tax_Type
                End If

                'Means Error Exists
                If boolErr Then
                    Me.MasterPage.MessageController.Show()
                    Exit Sub
                End If

                Me.State.InvoiceTaxType = Invoice_Tax_Type
                Me.State.InvoiceTaxTypeID = taxtypeID

            Next
        Else

        End If
    End Sub

    Private Sub FillClaims()

        Dim boolClaimsExist As Boolean = True

        Try

            Me.State.searchClaimDV = Claim.getClaimsForBatchProcess(Me.State.MyBO.ServiceCenterId, Me.State.MyBO.BatchNumber, Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            ControlMgr.SetVisibleControl(Me, Me.GridClaims, True)
            ControlMgr.SetVisibleControl(Me, Me.GridInvoices, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.GridClaims.Visible)
            ControlMgr.SetVisibleControl(Me, btnNEXT_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)

            Me.lblRecordCount.Text = Me.State.searchClaimDV.Count.ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)

            If Not Me.State.searchClaimDV Is Nothing Then

                If Me.State.searchClaimDV.Count = 0 Then
                    boolClaimsExist = False
                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(GridClaims, Me.State.SortExpression)
                    ControlMgr.SetEnableControl(Me, btnNEXT_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)

                Else
                    If Me.State.InvoiceTaxType = Invoice_Tax_Type Then
                        Me.GridClaims.Columns(GRID_CLAIM_COL_BATCH_NUMBER_IDX).Visible = True
                    Else
                        Me.GridClaims.Columns(GRID_CLAIM_COL_BATCH_NUMBER_IDX).Visible = False
                    End If

                    'Loop through the Claims and find if there are any claims that have claim status in 
                    'the'pending for payment' status. If so, display an error Message to the User
                    Dim i As Integer
                    Dim claimId As Guid
                    For i = 0 To Me.State.searchClaimDV.Count - 1
                        claimId = New Guid(CType(Me.State.searchClaimDV(i)(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_ID), Byte()))
                        'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
                        Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(claimId)
                        If Not maxClaimStatus Is Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
                            Me.DisplayMessage(Message.MSG_PAY_INVOICE_ALERT_FOR_CLAIM_PENDING_REVIEW, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                            Exit For
                        End If
                    Next

                    Me.State.bnoRow = False
                    Me.GridClaims.DataSource = Me.State.searchClaimDV
                    Me.GridClaims.DataBind()
                    Me.State.searchClaimDV.RowFilter = Claim.ClaimsForBatchProcessDV.COL_NAME_SELECTED + " = 'true'"

                    Me.State.searchClaimDV.RowFilter = ""
                    ControlMgr.SetEnableControl(Me, btnNEXT_WRITE, boolClaimsExist)
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)

                    If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") AndAlso Not LookupListNew.GetCodeFromId("INVSTAT", Me.State.MyBO.InvoiceStatusId) = "R" Then
                        Me.State.batchState = True
                        AddClaims()
                    End If

                End If

            End If

            If Not GridClaims.BottomPagerRow.Visible Then GridClaims.BottomPagerRow.Visible = True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddClaims()

        Dim dgItem As GridViewRow
        Dim dsBCI As New DALObjects.BatchClaimInvoice
        Dim dt As DataTable = dsBCI.INVOICE_TRANS_DETAIL
        Dim dr As DALObjects.BatchClaimInvoice.INVOICE_TRANS_DETAILRow
        Dim chk, chkExclDeduc As CheckBox


        Try

            'Loop through the visible items in the grid and check the status of the checkbox on each one.
            'If checked, create a new row record in the datatable and then pass to add the items to the set
            For Each dgItem In GridClaims.Rows

                chk = CType(dgItem.FindControl(Me.GRID_CLAIM_ADD_CLAIM_CHECKBOX), CheckBox)
                chkExclDeduc = CType(dgItem.FindControl(Me.GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX), CheckBox)
                If Not chk Is Nothing Then
                    If Boolean.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_SELECTED_IDX).Text) = True Then
                        If chk.Checked = False Then
                            dr = dsBCI.INVOICE_TRANS_DETAIL.NewINVOICE_TRANS_DETAILRow
                            dr.INVOICE_TRANS_DETAIL_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_INVOICE_TRANS_DETAIL_IDX).Text))
                            'dr.CLAIM_EXTENDED_STATUS_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_IDX).Text))
                            'dr.CLAIM_EXTENDED_STATUS = dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_IDX).Text

                            dr.ACTION = Me.CLAIM_STATUS_DELETED
                            dr.CLAIM_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_ID_IDX).Text))
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX).Text) > 0 Then
                                dr.CLAIM_MODIFIED_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX).Text)
                            Else
                                dr.CLAIM_MODIFIED_DATE = Date.MinValue
                            End If

                            dr.INVOICE_TRANS_ID = GuidControl.GuidToHexString(Me.State.selectedInvoiceTransId)
                            dr.PAYMENT_AMOUNT = 0
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).Text) > 0 Then dr.PICKUP_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).Text)
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).Text) > 0 Then dr.REPAIR_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).Text)
                            dr.RESERVE_AMOUNT = Double.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_RESERVE_AMOUNT_IDX).Text)
                            dr.SPARE_PARTS = dgItem.Cells(Me.GRID_CLAIM_COL_SPARE_PARTS_IDX).Text
                            dr.USER_ID = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                            If Not chkExclDeduc Is Nothing And chkExclDeduc.Checked = True Then
                                dr.EXCLUDE_DEDUCTIBLE = "Y"
                            End If
                            dt.Rows.Add(dr)
                        End If
                        If chk.Checked = True Then
                            If (Boolean.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_SELECTED_IDX).Text) = True And chkExclDeduc.Checked = False) Or (Boolean.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_SELECTED_IDX).Text) = False And chkExclDeduc.Checked = True) Then


                                Dim invoiceTransDetailId As Guid = New Guid(dgItem.Cells(Me.GRID_CLAIM_INVOICE_TRANS_DETAIL_IDX).Text)
                                Dim excludeDeductible As String

                                If Not chkExclDeduc Is Nothing And chkExclDeduc.Checked = True Then
                                    excludeDeductible = "Y"
                                Else
                                    excludeDeductible = "N"
                                End If
                                Dim objInvoiceTrans As New InvoiceTrans
                                objInvoiceTrans.UpdateExcludeDeductibleFlag(excludeDeductible, invoiceTransDetailId)
                                objInvoiceTrans.UpdatePaymentAmount(invoiceTransDetailId)
                            End If
                        End If
                    Else
                        If chk.Checked Then
                            dr = dsBCI.INVOICE_TRANS_DETAIL.NewINVOICE_TRANS_DETAILRow
                            dr.INVOICE_TRANS_DETAIL_ID = GuidControl.GuidToHexString(Guid.NewGuid)
                            'dr.CLAIM_EXTENDED_STATUS_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_IDX).Text))
                            'dr.CLAIM_EXTENDED_STATUS = dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_IDX).Text

                            dr.ACTION = Me.CLAIM_STATUS_INSERTED
                            dr.CLAIM_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_ID_IDX).Text))
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX).Text) > 0 Then
                                dr.CLAIM_MODIFIED_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX).Text)
                            Else
                                dr.CLAIM_MODIFIED_DATE = Date.MinValue
                            End If
                            dr.INVOICE_TRANS_ID = GuidControl.GuidToHexString(Me.State.selectedInvoiceTransId)
                            dr.PAYMENT_AMOUNT = 0
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).Text) > 0 Then dr.PICKUP_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).Text)
                            If Len(dgItem.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).Text) > 0 Then dr.REPAIR_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).Text)
                            dr.RESERVE_AMOUNT = Double.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_RESERVE_AMOUNT_IDX).Text)
                            dr.SPARE_PARTS = dgItem.Cells(Me.GRID_CLAIM_COL_SPARE_PARTS_IDX).Text
                            dr.USER_ID = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                            If Not chkExclDeduc Is Nothing And chkExclDeduc.Checked = True Then
                                dr.EXCLUDE_DEDUCTIBLE = "Y"
                            End If
                            dt.Rows.Add(dr)
                        End If
                    End If
                End If
            Next

            If dt.Rows.Count > 0 Then
                Dim _invoiceTrans As New InvoiceTrans
                _invoiceTrans.SaveBatch(dsBCI, Me.State.selectedInvoiceTransId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged

        If GridClaims.Visible Then
            GridClaims.PageIndex = 0
            GridClaims.PageSize = Integer.Parse(cboPageSize.SelectedValue)
            If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
                Me.State.batchState = True
            End If
            FillClaims()
        ElseIf GridInvoices.Visible Then
            GridInvoices.PageIndex = 0
            GridInvoices.PageSize = Integer.Parse(cboPageSize.SelectedValue)
            SearchInvoiceTrans()
        End If

    End Sub

    Private Sub GridInvoices_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridInvoices.PageIndexChanging
        Try
            GridInvoices.PageIndex = e.NewPageIndex
            State.PageIndex = GridInvoices.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaims_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridClaims.PageIndexChanging
        Try
            GridClaims.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaims.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateSelection()

        Me.State.selectedInvoiceTransId = Me.State.MyBO.Id

        'Fill & lock search fields
        Me.TextBoxSearchInvoiceAmount.Text = Me.State.MyBO.SvcControlAmount.ToString("0.00")
        Me.PopulateControlFromBOProperty(Me.TextBoxSearchInvoiceDate, Me.State.MyBO.InvoiceDate)
        Me.PopulateControlFromBOProperty(Me.txtboxInvRecDt, Me.State.MyBO.InvoiceReceivedDate)

        Try

            If Not Me.State.MyBO.ServiceCenterId.Equals(Guid.Empty) Then
                'Me.cboServiceCenter.SelectedValue = Me.cboServiceCenter.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), Me.State.MyBO.ServiceCenterId)).Value
                Me.cboServiceCenter.SelectedValue = Me.State.MyBO.ServiceCenterId.ToString()
            End If

            If Not Me.State.MyBO.InvoiceTypeId.Equals(Guid.Empty) Then
                'Me.ddlInvTyp.SelectedValue = Me.ddlInvTyp.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetInvTypList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.MyBO.InvoiceTypeId)).Value
                Me.ddlInvTyp.SelectedValue = Me.State.MyBO.InvoiceTypeId.ToString()
            End If

            If Not Me.State.MyBO.InvoiceStatusId.Equals(Guid.Empty) Then
                'Me.ddlInvStat.SelectedValue = Me.ddlInvStat.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetInvStatList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.MyBO.InvoiceStatusId)).Value
                Me.ddlInvStat.SelectedValue = Me.State.MyBO.InvoiceStatusId.ToString()
            End If

        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_NOT_ACTIVE_ERR, True)
            Me.MasterPage.MessageController.Show()
            Exit Sub
        End Try
        Me.TextBoxSearchInvoiceNumber.Text = Me.State.MyBO.SvcControlNumber
        Me.TextBoxBatchNumber.Text = Me.State.MyBO.BatchNumber
        Me.PopulateControlFromBOProperty(Me.txtboxInvCtdDt, Me.State.MyBO.InvoiceCreatedDate)

        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, False)
        ControlMgr.SetEnableControl(Me, Me.cboServiceCenter, False)
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceNumber, False)
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, False)
        ControlMgr.SetEnableControl(Me, Me.TextBoxBatchNumber, False)

        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, False)
        ControlMgr.SetVisibleControl(Me, Me.btnClear, True)
        ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, True)
        ControlMgr.SetVisibleControl(Me, Me.btnExisting, False)
        ControlMgr.SetVisibleControl(Me, Me.btnNew, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancelSearch_WRITE, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancelEdit_WRITE, True)
        ControlMgr.SetVisibleControl(Me, Me.txtboxInvCtdDt, True)
        ControlMgr.SetVisibleControl(Me, Me.lblInvCreDt, True)

        ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, False)
        ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, False)
        ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, False)
        ControlMgr.SetEnableControl(Me, Me.ddlInvStat, False)
        ControlMgr.SetEnableControl(Me, Me.txtboxInvCtdDt, False)

        GridClaims.PageIndex = 0
        If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
            Me.State.batchState = True
        Else
            Me.State.batchState = False
        End If
        FillClaims()
        If LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_PAID Or LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_REJECTED Then
            ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, Me.btnNEXT_WRITE, False)
        End If
    End Sub

    Private Sub ClearAll()
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, True)
        ControlMgr.SetEnableControl(Me, Me.cboServiceCenter, True)
        ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, True)
        ControlMgr.SetVisibleControl(Me, Me.lblInvStat, True)
        ControlMgr.SetVisibleControl(Me, Me.ddlInvStat, True)
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceNumber, True)
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, True)
        ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, True)
        ControlMgr.SetEnableControl(Me, Me.TextBoxBatchNumber, True)
        ControlMgr.SetEnableControl(Me, Me.ddlInvStat, True)


        Me.TextBoxSearchInvoiceAmount.Text = ""
        Me.TextBoxSearchInvoiceNumber.Text = ""
        Me.cboServiceCenter.SelectedIndex = 0
        Me.ddlInvTyp.SelectedIndex = 0
        Me.ddlInvStat.SelectedIndex = 0
        Me.TextBoxBatchNumber.Text = ""
        Me.TextBoxSearchInvoiceDate.Text = ""
        Me.txtboxInvRecDt.Text = ""
        Me.txtboxInvCtdDt.Text = ""

        Me.lblRecordCount.Text = ""

        ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, False)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, True)
        ControlMgr.SetVisibleControl(Me, Me.btnExisting, True)
        ControlMgr.SetVisibleControl(Me, Me.btnNew, True)
        ControlMgr.SetVisibleControl(Me, Me.GridInvoices, False)
        ControlMgr.SetVisibleControl(Me, Me.GridClaims, False)
        ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancelSearch_WRITE, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancelEdit_WRITE, False)
        ControlMgr.SetVisibleControl(Me, Me.txtboxInvCtdDt, False)
        ControlMgr.SetVisibleControl(Me, Me.lblInvCreDt, False)

        ControlMgr.SetVisibleControl(Me, trPageSize, False)
        ControlMgr.SetVisibleControl(Me, btnNEXT_WRITE, False)
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)

    End Sub

#End Region

#Region " Button Clicks "


    'Search for invoice Trans records based on what is in the grid
    Private Sub btnExisting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExisting.Click
        Dim boolErr As Boolean = False

        'Validate Invoice Amount
        If Not String.IsNullOrEmpty(Me.TextBoxSearchInvoiceAmount.Text) AndAlso Not Microsoft.VisualBasic.IsNumeric(Me.TextBoxSearchInvoiceAmount.Text) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR, True)
            boolErr = True
        End If
        Dim dtInvoiceDate As Date

        'Validate Invoice Date
        If Not String.IsNullOrEmpty(Me.TextBoxSearchInvoiceDate.Text.Trim) AndAlso Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.TextBoxSearchInvoiceDate.Text)) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_DATE_ENTERED_ERR, True)
            boolErr = True
        End If

        'Validate Invoice Recieved Date
        If Not String.IsNullOrEmpty(Me.txtboxInvRecDt.Text.Trim) AndAlso Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.txtboxInvRecDt.Text)) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_RECEIVED_DATE_ENTERED_ERR, True)
            boolErr = True
        End If

        'Means Error Exists
        If boolErr Then
            Me.MasterPage.MessageController.Show()
        Else
            SearchInvoiceTrans()
        End If
    End Sub

    'Create new invoice Trans Record and follow with allowing the user to add claims to it.
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Me.State.MyBO = New InvoiceTrans

        Dim boolErr As Boolean = False

        'Validate Invoice Number
        If Me.TextBoxSearchInvoiceNumber.Text.Trim.Length = 0 Or Me.TextBoxSearchInvoiceNumber.Text = Nothing Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR, True)
            boolErr = True
        End If

        'Validate Invoice Amount
        If Not Microsoft.VisualBasic.IsNumeric(Me.TextBoxSearchInvoiceAmount.Text) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR, True)
            boolErr = True
        End If

        'Validate Invoice Date
        If Me.TextBoxSearchInvoiceDate.Text.Trim <> String.Empty Then
            If Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.TextBoxSearchInvoiceDate.Text)) Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_DATE_ENTERED_ERR, True)
                boolErr = True
            End If
        End If

        'Validate Invoice Recieved Date
        If Me.txtboxInvRecDt.Text.Trim <> String.Empty Then
            If Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.txtboxInvRecDt.Text)) Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_RECEIVED_DATE_ENTERED_ERR, True)
                boolErr = True
            End If
        Else
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_RECIEVED_DATE_MUST_BE_SELECTED_ERR, True)
            boolErr = True
        End If

        'Validate Service Center or batch number Selection
        If (Me.cboServiceCenter.SelectedValue = Nothing Or Me.cboServiceCenter.SelectedValue = "" Or Me.cboServiceCenter.SelectedValue = Guid.Empty.ToString) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR, True)
            boolErr = True
        End If

        If (Me.ddlInvTyp.SelectedValue = Nothing Or Me.ddlInvTyp.SelectedValue = "" Or Me.ddlInvTyp.SelectedValue = Guid.Empty.ToString) Then
            Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_TYPE_MUST_BE_SELECTED_ERR, True)
            boolErr = True
        End If

        'Means Error Exists
        If boolErr Then
            Me.MasterPage.MessageController.Show()
            Exit Sub
        End If

        Try

            PopulateBOFromForm()
            Me.State.MyBO.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Me.State.MyBO.Save()

            Me.State.selectedInvoiceTransId = Me.State.MyBO.Id

            'lock search fields
            ControlMgr.SetVisibleControl(Me, Me.lblInvCreDt, True)
            ControlMgr.SetVisibleControl(Me, Me.txtboxInvCtdDt, True)
            ControlMgr.SetVisibleControl(Me, Me.lblInvStat, True)
            ControlMgr.SetVisibleControl(Me, Me.ddlInvStat, True)

            ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, False)
            ControlMgr.SetEnableControl(Me, Me.cboServiceCenter, False)
            ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceNumber, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, False)
            ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxBatchNumber, False)
            ControlMgr.SetEnableControl(Me, Me.txtboxInvCtdDt, False)
            ControlMgr.SetEnableControl(Me, Me.ddlInvStat, False)

            ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, False)
            ControlMgr.SetVisibleControl(Me, Me.btnClear, True)
            ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, True)
            ControlMgr.SetVisibleControl(Me, Me.btnExisting, False)
            ControlMgr.SetVisibleControl(Me, Me.btnNew, False)
            ControlMgr.SetVisibleControl(Me, Me.btnCancelSearch_WRITE, False)
            ControlMgr.SetVisibleControl(Me, Me.btnCancelEdit_WRITE, True)
            ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, False)

            Me.PopulateControlFromBOProperty(Me.ddlInvStat, Me.State.MyBO.InvoiceStatusId)
            Me.PopulateControlFromBOProperty(Me.txtboxInvCtdDt, Me.State.MyBO.InvoiceCreatedDate)

            Try
                'After adding the invoice trans records, fill the claims grid
                If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
                    Me.State.batchState = True
                Else
                    Me.State.batchState = False
                End If
                FillClaims()
            Catch ex As Exception
                Me.MasterPage.MessageController.AddErrorAndShow(ex.Message, False)
            End Try
        Catch ex As Exception
            'START  DEF-1855 this line of code is added for def1855
            If ex.Message = Assurant.ElitaPlus.Common.ErrorCodes.INVOICE_NUM_ALREADY_EXISTS Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVOICE_NUM_ALREADY_EXISTS, True)
            Else
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End If
            'END  DEF-1855 this line of code is added for def1855
            'Me.MasterPage.MessageController.AddErrorAndShow(ex.Message)

        End Try

    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click

        Me.SetDefaultButton(Me.TextBoxSearchInvoiceAmount, Me.btnSaveBatch_Write)
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceNumber, Me.btnSaveBatch_Write)
        If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
            Me.State.batchState = True
        Else
            Me.State.batchState = False
        End If
        AddClaims()

        'Refill Grid to refresh

        FillClaims()
        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
    End Sub

    Private Sub btnNEXT_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNEXT_WRITE.Click
        If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
            Me.State.batchState = True
        Else
            Me.State.batchState = False
        End If
        AddClaims()

        Me.callPage(PayBatchClaimForm.URL, Me.State.selectedInvoiceTransId.ToString)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        ClearAll()

    End Sub

    Private Sub btnEditBatch_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditBatch_Write.Click
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, Me.btnClear, False)
        ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.btnSaveBatch_Write, True)
        ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, True)
        ControlMgr.SetVisibleControl(Me, Me.txtboxInvCtdDt, True)
        ControlMgr.SetVisibleControl(Me, Me.lblInvCreDt, True)
        ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, True)
        ControlMgr.SetVisibleControl(Me, Me.ddlInvStat, True)
        ControlMgr.SetVisibleControl(Me, Me.lblInvStat, True)

        Dim oCompaniesDv As DataView, oUser As New User
        oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
        If oCompaniesDv.Count > 0 Then
            ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, False)
        End If

        Me.SetDefaultButton(Me.TextBoxSearchInvoiceAmount, Me.btnSaveBatch_Write)
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceNumber, Me.btnSaveBatch_Write)
        Me.SetDefaultButton(Me.TextBoxSearchInvoiceDate, Me.btnSaveBatch_Write)
        Me.SetDefaultButton(Me.txtboxInvRecDt, Me.btnSaveBatch_Write)
        Me.SetDefaultButton(Me.ddlInvTyp, Me.btnSaveBatch_Write)
        Dim invStatusCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, New Guid(Me.ddlInvStat.SelectedValue))
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, IIf(invStatusCode = "IP", True, False))
        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, IIf(invStatusCode = "IP", True, False))
        ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, IIf(invStatusCode = "IP", True, False))
        ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, IIf(invStatusCode = "IP", True, False))
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, IIf(invStatusCode = "IP", True, False))
        ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, IIf(invStatusCode = "IP", True, False))

        ControlMgr.SetEnableControl(Me, Me.ddlInvStat, False)
        ControlMgr.SetEnableControl(Me, Me.txtboxInvCtdDt, False)
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, False)
        ControlMgr.SetVisibleControl(Me, Me.btnClear, True)
        ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, True)
        ControlMgr.SetVisibleControl(Me, Me.btnSaveBatch_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, False)
        ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, False)

        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, False)
        ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, False)
        ControlMgr.SetEnableControl(Me, Me.ddlInvStat, False)
        ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, False)
        ControlMgr.SetEnableControl(Me, Me.txtboxInvCtdDt, False)

        ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, False)

        Me.PopulateControlFromBOProperty(Me.TextBoxSearchInvoiceAmount, Me.State.MyBO.SvcControlAmount)
        Me.PopulateControlFromBOProperty(Me.TextBoxSearchInvoiceNumber, Me.State.MyBO.SvcControlNumber)
        Me.PopulateControlFromBOProperty(Me.TextBoxSearchInvoiceDate, Me.State.MyBO.InvoiceDate)
        Me.PopulateControlFromBOProperty(Me.txtboxInvRecDt, Me.State.MyBO.InvoiceReceivedDate)
        Me.PopulateControlFromBOProperty(Me.TextBoxBatchNumber, Me.State.MyBO.BatchNumber)
        'Me.PopulateControlFromBOProperty(Me.txtboxInvCtdDt, Me.State.MyBO.InvoiceCreatedDate)

    End Sub

    Private Sub btnSaveBatch_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBatch_Write.Click

        Dim boolErr As Boolean = False

        Try

            PopulateBOFromForm()
            Me.State.MyBO.UserId = ElitaPlusIdentity.Current.ActiveUser.Id

            'Validate Invoice Number
            If Me.TextBoxSearchInvoiceNumber.Text.Trim.Length = 0 Or Me.TextBoxSearchInvoiceNumber.Text = Nothing Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR, True)
                boolErr = True
            End If

            'Validate Invoice Amount
            If Not Microsoft.VisualBasic.IsNumeric(Me.TextBoxSearchInvoiceAmount.Text) Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR, True)
                boolErr = True
            End If

            'Validate Invoice Date
            If Me.TextBoxSearchInvoiceDate.Text.Trim <> String.Empty Then
                If Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.TextBoxSearchInvoiceDate.Text)) Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_DATE_ENTERED_ERR, True)
                    boolErr = True
                End If
            End If

            'Validate Invoice Received Date
            If Me.txtboxInvRecDt.Text.Trim <> String.Empty Then
                If Not Microsoft.VisualBasic.IsDate(DateHelper.GetDateValue(Me.txtboxInvRecDt.Text)) Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_RECEIVED_DATE_ENTERED_ERR, True)
                    boolErr = True
                End If
            End If

            'Validate Service Center Selection
            If Me.cboServiceCenter.SelectedValue = Nothing Or Me.cboServiceCenter.SelectedValue = "" Or Me.cboServiceCenter.SelectedValue = Guid.Empty.ToString Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR, True)
                boolErr = True
            End If

            'Validate Invoice Type Selection
            If Me.ddlInvTyp.SelectedValue = Nothing Or Me.ddlInvTyp.SelectedValue = "" Or Me.ddlInvTyp.SelectedValue = Guid.Empty.ToString Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_TYPE_MUST_BE_SELECTED_ERR, True)
                boolErr = True
            End If

            'Validate Invoice Status Selection
            'If Me.ddlInvStat.SelectedValue = Nothing Or Me.ddlInvStat.SelectedValue = "" Or Me.ddlInvStat.SelectedValue = Guid.Empty.ToString Then
            '    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_STATUS_MUST_BE_SELECTED_ERR, True)
            '    boolErr = True
            'End If

            'Means Error Exists
            If boolErr Then
                Me.MasterPage.MessageController.Show()
                Exit Sub
            End If

            Me.State.MyBO.Save()

            ControlMgr.SetVisibleControl(Me, Me.btnClear, True)
            ControlMgr.SetVisibleControl(Me, Me.btnEditBatch_Write, True)
            ControlMgr.SetVisibleControl(Me, Me.btnSaveBatch_Write, False)
            ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, False)

            ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceDate, False)
            ControlMgr.SetEnableControl(Me, Me.txtboxInvRecDt, False)
            ControlMgr.SetEnableControl(Me, Me.ddlInvTyp, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxSearchInvoiceAmount, False)
            ControlMgr.SetEnableControl(Me, Me.ddlInvStat, False)
            ControlMgr.SetEnableControl(Me, Me.txtboxInvCtdDt, False)

            ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, False)
            ControlMgr.SetVisibleControl(Me, Me.imgBtnInvRecDt, False)

            'Me.AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
        Catch ex As Exception
            'Me.AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnCancelSearch_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSearch_WRITE.Click

        ClearAll()

    End Sub

    Private Sub btnCancelEdit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelEdit_WRITE.Click

        ClearAll()

        If Not Me.State.serviceCenterId.Equals(Guid.Empty) Then
            'Me.cboServiceCenter.SelectedValue = Me.cboServiceCenter.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), Me.State.serviceCenterId)).Value
            Me.cboServiceCenter.SelectedValue = Me.State.serviceCenterId.ToString()
        End If

        If Not Me.State.invoiceTypeId.Equals(Guid.Empty) Then
            'Me.ddlInvTyp.SelectedValue = Me.ddlInvTyp.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetInvTypList(Authentication.LangId), Me.State.invoiceTypeId)).Value
            Me.ddlInvTyp.SelectedValue = Me.State.invoiceTypeId.ToString()
        End If

        If Not Me.State.invoiceStatusId.Equals(Guid.Empty) Then
            'Me.ddlInvStat.SelectedValue = Me.ddlInvStat.Items.FindByText(LookupListNew.GetDescriptionFromId(LookupListNew.GetInvStatList(Authentication.LangId), Me.State.invoiceStatusId)).Value
            Me.ddlInvStat.SelectedValue = Me.State.invoiceStatusId.ToString()
        End If


        If Not Me.State.invoiceNumber Is Nothing AndAlso Me.State.invoiceNumber.Trim.Length > 0 Then
            Me.TextBoxSearchInvoiceNumber.Text = Me.State.invoiceNumber
        End If

        If Not Me.State.batchNumber Is Nothing AndAlso Me.State.batchNumber.Trim.Length > 0 Then
            Me.TextBoxBatchNumber.Text = Me.State.batchNumber
        End If

        SearchInvoiceTrans()

    End Sub

#End Region

#Region " Datagrid Related "

    Private Sub GridClaims_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaims.RowDataBound

        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim claimId As Guid

        Try

            If (e.Row.RowType = DataControlRowType.Header) Then

                'adding an attribute for onclick event on the check box in the header 
                'and passing the ClientID of the Select All checkbox
                Dim chkAll As CheckBox
                chkAll = CType(e.Row.Cells(Me.GRID_CLAIM_COL_ADD_IDX).FindControl(Me.GRID_CLAIM_CHECK_ALL_CLAIM_CHECKBOX), CheckBox)
                'If Not chkAll Is Nothing Then
                '    chkAll.Attributes("onClick") = "javascript:SelectAll('" & chkAll.ClientID & "')"
                'End If
                If LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_PAID Or LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_REJECTED Then
                    ControlMgr.SetEnableControl(Me, chkAll, False)
                End If

                Dim ctrlbl As New Label
                ctrlbl.Text = TranslationBase.TranslateLabelOrMessage("EXCLUDE_DEDUCTIBLE")
                e.Row.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX).Controls.Add(ctrlbl)

                chkAll = CType(e.Row.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX).FindControl(Me.GRID_CLAIM_EXCLUDE_DED_ALL_CHECKBOX), CheckBox)
                'If Not chkAll Is Nothing Then
                '    chkAll.Attributes("onClick") = "javascript:SelectExcludeDedAll('" & chkAll.ClientID & "')"
                'End If
                If LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_PAID Or LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_REJECTED Then
                    ControlMgr.SetEnableControl(Me, chkAll, False)
                End If
            End If
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_AUTHORIZATION_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_MODIFIED_DATE).ToString)
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CONTACT_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_INVOICE_TRANS_NUMBER_IDX), Me.State.selectedInvoiceTransId)
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_RESERVE_AMOUNT_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_RESERVE_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_TOTAL_BONUS_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_TOTAL_BONUS))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_BATCH_NUMBER_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_BATCH_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_SPARE_PARTS_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_SPARE_PARTS))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_PAYMENT_AMOUNT_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_PAYMENT_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_REPAIR_DATE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_PICKUP_DATE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_SELECTED_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_SELECTED))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_INVOICE_TRANS_DETAIL_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_INVOICE_TRANS_DETAIL_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_SERVICE_CENTER_NAME_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_SERVICE_CENTER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_SELECTED_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_EXCLUDE_DEDUCTIBLE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_EXTENDED_STATUS_NAME_IDX), dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_EXTENDED_STATUS))

                    Dim chk As CheckBox
                    chk = CType(e.Row.Cells(Me.GRID_CLAIM_COL_ADD_IDX).FindControl(Me.GRID_CLAIM_ADD_CLAIM_CHECKBOX), CheckBox)

                    If (Not e.Row.Cells(Me.GRID_CLAIM_COL_BATCH_NUMBER_IDX).Text Is Nothing And Not e.Row.Cells(Me.GRID_CLAIM_COL_BATCH_NUMBER_IDX).Text.Equals("")) AndAlso Me.State.batchState.Equals(True) Then
                        chk.Checked = True
                        dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_SELECTED) = True
                    End If

                    If Not chk Is Nothing Then
                        claimId = New Guid(CType(dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_ID), Byte()))

                        'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
                        Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(claimId)
                        If Not maxClaimStatus Is Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
                            ControlMgr.SetEnableControl(Me, chk, False)
                            chk.ToolTip = CLAIM_TOOLTIP
                        End If

                        If Not claimId.Equals(Guid.Empty) Then
                            Dim ClaimBO As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(claimId)
                            If Not ClaimBO.CertificateId.Equals(Guid.Empty) Then
                                Dim oCert As New Certificate(ClaimBO.CertificateId)
                                Dim oDealer As New Dealer(oCert.DealerId)
                                Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
                                Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                                If oClmSystem.PayClaimId.Equals(noId) Then
                                    ControlMgr.SetEnableControl(Me, chk, False)
                                    chk.ToolTip = CLAIM_TOOLTIP
                                End If
                            End If
                        End If
                        Me.PopulateControlFromBOProperty(chk, dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_SELECTED))
                        chk.Attributes("onClick") = "if (this.checked){ " + Me.btnNEXT_WRITE.ClientID + ".disabled = false;}"
                    End If

                    Dim chk1 As CheckBox
                    chk1 = CType(e.Row.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX).FindControl(Me.GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX), CheckBox)
                    If Not chk1 Is Nothing Then
                        claimId = New Guid(CType(dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_CLAIM_ID), Byte()))
                        If Not claimId.Equals(Guid.Empty) Then
                            Dim ClaimBO As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(claimId)
                            If Not ClaimBO.CertificateId.Equals(Guid.Empty) Then
                                Dim oCert As New Certificate(ClaimBO.CertificateId)
                                Dim oDealer As New Dealer(oCert.DealerId)
                                Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)
                                Dim yFullId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)
                                If oDealer.PayDeductibleId.Equals(noId) Or oDealer.PayDeductibleId.Equals(yFullId) Then
                                    ControlMgr.SetEnableControl(Me, chk1, False)
                                    chk1.ToolTip = "Dealer does not pay deductble"
                                End If
                            End If
                        End If
                        Me.PopulateControlFromBOProperty(chk1, dvRow(Claim.ClaimsForBatchProcessDV.COL_NAME_EXCLUDE_DEDUCTIBLE))
                    End If
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_PAID Or LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId) = INVOICE_STATUS_REJECTED Then
                        ControlMgr.SetEnableControl(Me, chk, False)
                        ControlMgr.SetEnableControl(Me, chk1, False)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GridInvoices_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridInvoices.RowDataBound

        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Dim del As ImageButton = CType(e.Row.Cells(Me.GRID_INV_COL_DELETE_IDX).FindControl("btnDeleteClaims"), ImageButton)
                    Dim ad As ImageButton = CType(e.Row.Cells(Me.GRID_INV_COL_ADD_IDX).FindControl("btnAddClaims"), ImageButton)
                    Dim ed As ImageButton = CType(e.Row.Cells(Me.GRID_INV_COL_ADD_IDX).FindControl("btnEditClaims"), ImageButton)

                    e.Row.Cells(Me.GRID_INV_COL_INVOICE_NUMBER_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_SVC_CONTROL_NUMBER).ToString
                    e.Row.Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_TRANS_ID), Byte()))
                    e.Row.Cells(Me.GRID_INV_COL_SERVICE_CENTER_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_SERVICE_CENTER_NAME).ToString
                    e.Row.Cells(Me.GRID_INV_COL_SERVICE_CENTER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_SERVICE_CENTER_ID), Byte()))
                    If dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_DATE) Is DBNull.Value Then
                        e.Row.Cells(Me.GRID_INV_COL_INVOICE_DATE_IDX).Text = ""
                    Else
                        e.Row.Cells(Me.GRID_INV_COL_INVOICE_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_DATE), Date))
                    End If
                    e.Row.Cells(Me.GRID_INV_COL_INV_AMOUNT_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_SVC_CONTROL_AMOUNT).ToString
                    e.Row.Cells(Me.GRID_INV_COL_INVOICE_STATUS_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_STATUS_NAME).ToString
                    'e.Row.Cells(Me.GRID_INV_COL_INVOICE_STATUS_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_STATUS_ID), Byte()))
                    'e.Row.Cells(Me.GRID_INV_COL_INVOICE_COMMENTS_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_INVOICE_COMMENTS).ToString
                    e.Row.Cells(Me.GRID_INV_COL_BATCH_NUMER_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_BATCH_NUMBER).ToString
                    e.Row.Cells(Me.GRID_INV_COL_BONUS_PAID_IDX).Text = dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_TOTAL_BONUS_PAID).ToString
                    'Add confirmation to the delete button
                    'If Not del Is Nothing Then
                    '    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Nothing, True)

                    '    Me.AddControlMsg(del, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                    '                                                      Me.MSG_TYPE_CONFIRM, True)
                    'End If

                    Select Case dvRow(InvoiceTrans.InvoiceTransSearchDV.COL_BATCH_STATUS)

                        Case Me.BATCH_STATUS_INSERTED
                            e.Row.Cells(Me.GRID_INV_COL_BATCH_STATUS_IDX).Text = Me.BATCH_STATUS_ICON_INSERTED
                        Case Me.BATCH_STATUS_PROCESSING
                            e.Row.Cells(Me.GRID_INV_COL_BATCH_STATUS_IDX).Text = Me.BATCH_STATUS_ICON_PROCESSING
                            If Not del Is Nothing Then del.Visible = False
                            If Not ad Is Nothing Then ad.Visible = False
                            If Not ed Is Nothing Then ed.Visible = False
                        Case Me.BATCH_STATUS_COMPLETED
                            e.Row.Cells(Me.GRID_INV_COL_BATCH_STATUS_IDX).Text = Me.BATCH_STATUS_ICON_COMPLETED
                            If Not ad Is Nothing Then ad.Visible = False
                            If Not ed Is Nothing Then ed.Visible = True
                        Case Me.BATCH_STATUS_FAILED
                            e.Row.Cells(Me.GRID_INV_COL_BATCH_STATUS_IDX).Text = Me.BATCH_STATUS_ICON_FAILED
                    End Select

                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GridInvoices_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridInvoices.RowCommand
        Try
            Dim index As Integer
            Dim invoiceId As Guid
            Dim row As GridViewRow
            Dim RowInd As Integer
            Dim lblCtrl As Label


            Select Case e.CommandName
                Case Me.GRID_INV_COM_DELETE 'On Delete, delete the batch and reload the grid.
                    'index = CInt(e.CommandArgument)
                    Dim _invoiceTrans As New InvoiceTrans

                    row = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    RowInd = row.RowIndex
                    lblCtrl = CType(GridInvoices.Rows(RowInd).Cells(GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).FindControl("invoice_trans_id"), Label)
                    invoiceId = New Guid(Me.GridInvoices.Rows(RowInd).Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text)

                    _invoiceTrans.DeleteBatch(invoiceId)

                    '_invoiceTrans.DeleteBatch(New Guid(Me.GridInvoices.Rows(index).Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text))
                    ' _invoiceTrans.DeleteBatch(New Guid(e.row.Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text))

                    SearchInvoiceTrans()

                Case Me.GRID_INV_COM_ADD, Me.GRID_INV_COM_EDIT 'On Add, Load the claim grid

                    row = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    RowInd = row.RowIndex
                    lblCtrl = CType(GridInvoices.Rows(RowInd).Cells(GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).FindControl("invoice_trans_id"), Label)
                    invoiceId = New Guid(Me.GridInvoices.Rows(RowInd).Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text)
                    'index = CInt(e.CommandArgument)
                    'Me.State.MyBO = New InvoiceTrans(New Guid(Me.GridInvoices.Rows(index).Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text))
                    'Me.State.MyBO = New InvoiceTrans(New Guid(e.row.Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text))
                    Me.State.MyBO = New InvoiceTrans(invoiceId)

                    PopulateSelection()

                    'Case Me.GRID_INV_COM_EDIT 'On Edit, redirect to the pay invoice page to enable the user to edit the associsated claims
                    '    Me.callPage(PayBatchClaimForm.URL, e.Item.Cells(Me.GRID_INV_COL_INVOICE_TRANS_NUMBER_IDX).Text)

            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaims_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridClaims.PageIndexChanged
        If Not Me.State.MyBO.BatchNumber Is Nothing AndAlso Not Me.State.MyBO.BatchNumber.Equals("") Then
            Me.State.batchState = True
        Else
            Me.State.batchState = False
        End If

        AddClaims()
        'Me.State.PageIndex = GridClaims.PageIndex
        'Me.State.selectedCertificateId = Guid.Empty
        GridClaims.PageIndex = GridClaims.PageIndex
        FillClaims()

    End Sub

    Private Sub GridInvoices_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInvoices.PageIndexChanged

        SearchInvoiceTrans(GridClaims.PageIndex)

    End Sub

    Private Sub GridClaims_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaims.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub GridInvoices_IRowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridInvoices.RowCreated
        BaseItemCreated(sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim pce1 As PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender1"), PopupControlExtender)

            Dim behaviorID1 As String = "pce1_" + e.Row.RowIndex.ToString
            pce1.BehaviorID = behaviorID1

            Dim img1 As System.Web.UI.WebControls.Image = CType(e.Row.FindControl("Image1"), System.Web.UI.WebControls.Image)

            Dim OnMouseOverScript1 As String = String.Format("$find('{0}').showPopup();", behaviorID1)
            Dim OnMouseOutScript1 As String = String.Format("$find('{0}').hidePopup();", behaviorID1)


            img1.Attributes.Add("onmouseover", OnMouseOverScript1)
            img1.Attributes.Add("onmouseout", OnMouseOutScript1)
        End If
    End Sub
    Private Sub GridInvoices_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridInvoices.RowCreated


    End Sub
#End Region


    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()>
    Public Shared Function GetInvoiceComments(ByVal contextKey As String) As String

        Dim description As String = InvoiceTrans.GetInvoiceComments(contextKey)

        Dim b As StringBuilder = New StringBuilder()

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ")
        b.Append("width:150px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>")
        b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
        b.Append("<b>Invoice Comments</b>")
        b.Append("</td></tr>")
        b.Append("<tr><td style='width:150px;'><b>")
        b.Append(description)
        b.Append("</b></td></tr>")
        b.Append("</table>")

        Return b.ToString()

    End Function
End Class
