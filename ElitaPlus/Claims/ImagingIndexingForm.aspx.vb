'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
Imports Assurant.ElitaPlus.BusinessObjectsNew.Doc
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.BusinessObjectsNew.DocumentImaging
Imports Assurant.ElitaPlus.BusinessObjectsNew.WrkQueue

Public Class ImagingIndexingForm
    Inherits ElitaPlusSearchPage

#Region "Const"

    Public Const ADMIN As String = "ADMIN"
    Public Const WORK_QUEUE As String = "WORK_QUEUE"
    Public Const IMAGING_INDEXING As String = "IMAGING_INDEXING"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_STATUS_IDX As Integer = 2
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 3
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 4
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 7
    Public Const GRID_TOTAL_COLUMNS As Integer = 8
    Public Const URL As String = "MaintainQueueUser.aspx"

    Public Const IDX_RADIO_BUTTON As Integer = 0
    Public Const IDX_CLAIM_NUMBER As Integer = 1
    Public Const IDX_CLAIM_ID As Integer = 7

    Public Const ATTRIB_SRC As String = "src"
    Public Const PDF_URL As String = "DisplayPdf.aspx?ImageId="
    Public Const ATTB_DOCUMENTTYPE As String = "DocumentType"
    Public Const ATTB_CLAIMNUMBER As String = "ClaimNumber"
    Public Const ATTB_COMPANYCODE As String = "CompanyCode"

    Public Const IDX_DOC_INFO As Integer = 0
    Public Const IDX_ATTB_ONE As Integer = 0
    Public Const IDX_ATTB_TWO As Integer = 1
    Public Const IDX_ATTB_THREE As Integer = 2

    Public Const CLAIM_STATUS_DESCRIPTION_ACTIVE As String = "Active"
    Public Const CLAIM_STATUS_DESCRIPTION_PENDING As String = "Pending"
    Public Const CLAIM_STATUS_DESCRIPTION_DENIED As String = "Denied"
    Public Const CLAIM_STATUS_DESCRIPTION_CLOSED As String = "Closed"

#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimImage
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimImage, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public MyBO As ClaimImage
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public SortExpression As String = Claim.ClaimSearchDV.COL_CLAIM_NUMBER
        Public RspositoryId As Guid
        Public ImageName As String
        Public DocInfo As DocumentInfo
        Public WorkQueueItem As BusinessObjectsNew.WorkQueueItem
        Public Action As BaseActionProvider
        Public claimStatus As String = String.Empty
        'Public InputParameters As WorkQueueItem
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String
        Public SelectedClaimNumber As String
        Public SelectedCompanyCode As String
        Public customerName As String
        Public serviceCenterName As String = String.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = 5 'DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = True
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        State.WorkQueueItem = CType(NavController.ParametersPassed, BusinessObjectsNew.WorkQueueItem)
    End Sub

#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(WORK_QUEUE) _
                                        & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(IMAGING_INDEXING)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear_Hide()
            moMessageController.Clear_Hide()
            If Not IsPostBack Then
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(IMAGING_INDEXING)
                UpdateBreadCrum()

                If State.MyBO Is Nothing Then
                    State.MyBO = New ClaimImage
                End If
                BindBOPropertiesToLabel()

                'Populate Grid View fields in ajax pop up
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateSortByDropDown()
                PopulateClaimStatusDropDown()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        ClaimSearchGridView.PageSize = State.selectedPageSize
                    End If
                End If
                TranslateGridHeader(ClaimSearchGridView)
                SetGridItemStyleColor(ClaimSearchGridView)
                SetFocus(TextBoxSearchClaimNumber)

                'Load the Image and Attributes to the iFrame
                LoadDocumentBeforeIndexing()

                btnSave.Enabled = False
                'btnRedirect.Enabled = False
                PopulateRedirectForm()

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        Finally
        End Try

    End Sub

    Private Sub LoadDocumentBeforeIndexing()
        Dim oDoc As Document = New Document
        Dim xid As Guid
        Dim oDocInfo As DocumentInfo = New DocumentInfo
        Try
            xid = State.WorkQueueItem.WorkQueueItem.ImageId
            oDoc = Doc.DownloadDocument(xid)
            oDocInfo = CType(oDoc, DocumentInfo)
            State.DocInfo = New DocumentInfo
            'Using the Reflection Extension method directly on the source object
            oDocInfo.CopyProperties(State.DocInfo)
            State.ImageName = State.WorkQueueItem.WorkQueueItem.ImageId.ToString
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        txtImage.Text = State.ImageName
        txtScanDate.Text = If(State.WorkQueueItem.ImageScanDate.HasValue, GetDateFormattedStringNullable(State.WorkQueueItem.ImageScanDate.Value), String.Empty)
        pdfIframe.Attributes(ATTRIB_SRC) = PDF_URL + xid.ToString
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBOPropertiesToLabel()
        BindBOPropertyToLabel(State.MyBO, "ClaimId", lblClaimNumber)
        BindBOPropertyToLabel(State.MyBO, "ImageId", lblImage)
        BindBOPropertyToLabel(State.MyBO, "DocumentTypeId", lblDocumentType)
    End Sub

    Protected Sub PopulateBOsFromForm()
        With State.MyBO
            State.MyBO.ClaimId = State.selectedClaimId
            If State.MyBO.ImageId = Nothing OrElse State.MyBO.ImageId = Guid.Empty Then
                State.MyBO.ImageId = State.WorkQueueItem.WorkQueueItem.ImageId
            End If
            PopulateBOProperty(State.MyBO, "DocumentTypeId", ddlDocumentType)
            State.MyBO.ScanDate = State.WorkQueueItem.ImageScanDate.Value
            PopulateBOProperty(State.MyBO, "ImageStatusId", LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PENDING))
        End With
    End Sub

    Private Sub SortSvc(oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateSortByDropDown()
        Dim oSortDv As DataView
        Try
            'oSortDv = LookupListNew.GetClaimSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'SortSvc(oSortDv)
            ' Me.BindListControlToDataView(Me.cboSortBy, oSortDv, , , False

            Dim claimSearchFieldsLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode())
            cboSortBy.Populate(claimSearchFieldsLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    })

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (State.selectedSortById.Equals(Guid.Empty)) Then
                SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                State.selectedSortById = defaultSelectedCodeId
            Else
                SetSelectedItem(cboSortBy, State.selectedSortById)
            End If

            'Me.BindListControlToDataView(ddlDocumentType, LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            Dim documentTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
            ddlDocumentType.Populate(documentTypeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    })



        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateClaimStatusDropDown()
        Try
            cboClaimStatus.Items.Add(New WebControls.ListItem(""))
            cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
            cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))
            cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_PENDING))
            cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_CLOSED))
            If State.claimStatus IsNot String.Empty Then
                Dim setClaimStatusText As String
                If State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
                SetSelectedItemByText(cboClaimStatus, setClaimStatusText)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "PopUp Button Clicks "

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If (TextBoxSearchClaimNumber.Text = "" AndAlso
                TextBoxSearchCustomerName.Text = "" AndAlso
                moServiceCenterText.Text = "" AndAlso
                TextBoxSearchAuthorizationNumber.Text = "" AndAlso
                TextBoxSearchAuthorizedAmount.Text = "") Then
                moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)

                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                mdlPopup.Show()
                Exit Sub
            End If
            State.PageIndex = 0
            State.searchDV = Nothing
            State.selectedSortById = New Guid(cboSortBy.SelectedValue)
            PopulateGrid()

            If State.searchDV Is Nothing Then
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                mdlPopup.Show()
            Else ' If Search Results are retrieved
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, True)
                dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"
                mdlPopup.Show()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            clearSearch()
            ClaimSearchGridView.DataSource = State.searchDV
            ClaimSearchGridView.DataBind()
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub clearSearch()
        TextBoxSearchClaimNumber.Text = String.Empty
        TextBoxSearchCustomerName.Text = String.Empty
        moServiceCenterText.Text = String.Empty
        TextBoxSearchAuthorizationNumber.Text = String.Empty
        TextBoxSearchAuthorizedAmount.Text = String.Empty
    End Sub

    Private Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click
        Try
            mdlPopup.Hide()
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.customerName = TextBoxSearchCustomerName.Text
            State.serviceCenterName = moServiceCenterText.Text
            State.authorizationNumber = TextBoxSearchAuthorizationNumber.Text
            State.selectedSortById = GetSelectedItem(cboSortBy)

            'Set the ClaimStatus
            If cboClaimStatus.SelectedItem.Text = "" Then
                State.claimStatus = String.Empty
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING Then
                State.claimStatus = Codes.CLAIM_STATUS__PENDING
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                State.claimStatus = Codes.CLAIM_STATUS__DENIED
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED Then
                State.claimStatus = Codes.CLAIM_STATUS__CLOSED
            End If

            If Not TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR, True)
                    Return False
                Else
                    State.authorizedAmountCulture = TextBoxSearchAuthorizedAmount.Text
                    State.authorizedAmount = dblAmount.ToString(Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                State.authorizedAmount = TextBoxSearchAuthorizedAmount.Text
            End If

            Return True

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

    Public Sub PopulateGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, State.selectedSortById)

                State.searchDV = Claim.getClaimListForImageIndexing(State.claimNumber,
                                                                       State.claimStatus,
                                                                       State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode,
                                                                       State.customerName,
                                                                       State.serviceCenterName,
                                                                       State.authorizationNumber,
                                                                       State.authorizedAmount,
                                                                       Nothing,
                                                                       sortBy)

                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            ClaimSearchGridView.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, ClaimSearchGridView, State.PageIndex)
            'Me.State.PageIndex = Me.ClaimSearchGridView.CurrentPageIndex
            ClaimSearchGridView.DataSource = State.searchDV
            'PutInvisibleSvcColumns(Me.ClaimSearchGridView)
            ClaimSearchGridView.AllowSorting = False
            ClaimSearchGridView.DataBind()
            ControlMgr.SetVisibleControl(Me, ClaimSearchGridView, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, ClaimSearchGridView.Visible)
            Session("recCount") = State.searchDV.Count
            If State.searchDV.Count > 0 Then
                If ClaimSearchGridView.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                lblRecordCount.ForeColor = Color.Black
                If ClaimSearchGridView.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PutInvisibleSvcColumns(oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZATION_NUMBER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).Visible = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles ClaimSearchGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_PageIndexChanging(source As Object, e As GridViewPageEventArgs) Handles ClaimSearchGridView.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            ClaimSearchGridView.PageIndex = State.PageIndex
            PopulateGrid()
            ClaimSearchGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            'Me.dvBottom.Visible = True
            dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ClaimSearchGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub btnNewItemAdd_Click(sender As Object, e As EventArgs) Handles btnNewItemAdd.Click
        Dim oClaim As Claim
        Dim oCompany As Company
        Try
            For Each gvr As GridViewRow In ClaimSearchGridView.Rows
                If gvr.RowType <> DataControlRowType.DataRow Then Continue For
                If TryCast(gvr.Cells(0).FindControl("radioSelect"), RadioButton).Checked Then
                    txtClaimNumber.Text = gvr.Cells(IDX_CLAIM_NUMBER).Text
                    State.selectedClaimId = New Guid(gvr.Cells(IDX_CLAIM_ID).Text)
                    hfClaimId.Value = gvr.Cells(IDX_CLAIM_ID).Text
                    oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimId)
                    txtCertificateNumber.Text = oClaim.CertificateNumber
                    txtMobileNumber.Text = oClaim.MobileNumber
                    State.SelectedClaimNumber = oClaim.ClaimNumber
                    oCompany = New Company(oClaim.CompanyId)
                    State.SelectedCompanyCode = oCompany.Code
                    Exit For
                End If
            Next
            btnSave.Enabled = True
            btnRedirect.Enabled = True
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'Update the Document Service with the Claim Info
            UpdateDocumentAfterIndexing()

            PopulateBOsFromForm()
            'Save Claim Image Mapping
            State.MyBO.Save()

            'Update the WorkQueue Item as Processed
            State.WorkQueueItem.Process()
            ' Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_WKQ, WorkQueueActionForm.ItemActionType.Process)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateDocumentAfterIndexing()
        Dim oFindReq As FindRequest = New FindRequest
        Dim oMataData As DocumentMetadata
        Dim oDocInfo() As DocumentInfo = New DocumentInfo() {New DocumentInfo()}
        'oFindReq.Name = Me.State.ImageName
        'oDocInfo = DocumentImaging.Doc.FindDocument(oFindReq)
        oDocInfo(0) = State.DocInfo
        oDocInfo(0).MetadataList = New DocumentMetadata(2) {}

        If oDocInfo.Length > 0 Then
            '#Update Metadata - Document Type 
            oMataData = New DocumentMetadata
            oMataData.Id = Guid.NewGuid
            oMataData.Name = ATTB_DOCUMENTTYPE
            oMataData.Value = ddlDocumentType.SelectedValue.ToString
            oDocInfo(IDX_DOC_INFO).MetadataList(IDX_ATTB_ONE) = oMataData
            '#Update Metadata - Claim Number 
            oMataData = New DocumentMetadata
            oMataData.Id = Guid.NewGuid
            oMataData.Name = ATTB_CLAIMNUMBER
            oMataData.Value = State.SelectedClaimNumber
            oDocInfo(IDX_DOC_INFO).MetadataList(IDX_ATTB_TWO) = oMataData
            '#Update Metadata - Company Code
            oMataData = New DocumentMetadata
            oMataData.Id = Guid.NewGuid
            oMataData.Name = ATTB_COMPANYCODE
            oMataData.Value = State.SelectedCompanyCode
            oDocInfo(IDX_DOC_INFO).MetadataList(IDX_ATTB_THREE) = oMataData
            oDocInfo(IDX_DOC_INFO).UpdatedBy = ElitaPlusIdentity.Current.ActiveUser.UniqueId
            Doc.UpdateDocument(oDocInfo(IDX_DOC_INFO))
        Else
            MasterPage.MessageController.AddSuccess(Message.ERR_SAVING_DATA)
        End If

    End Sub

    Protected Sub imgBtnClaimLookup_Click(sender As Object, e As EventArgs) Handles imgBtnClaimLookup.Click
        Try
            clearSearch()
            lblRecordCount.Text = ""
            State.searchDV = Nothing
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewItemCancel_Click(sender As Object, e As EventArgs) Handles btnNewItemCancel.Click
        Try
            mdlPopup.Hide()
            dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnRedirect_Click(sender As Object, e As EventArgs) Handles btnRedirect.Click
        Try
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            mdlPopupRedirect.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlWorkQueueList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWorkQueueList.SelectedIndexChanged

        Dim wq As BusinessObjectsNew.WorkQueue
        Dim wqReasonList As WebControls.ListItem()
        If (Not String.IsNullOrEmpty(ddlWorkQueueList.SelectedItem.Value)) Then
            wq = New BusinessObjectsNew.WorkQueue(New Guid(ddlWorkQueueList.SelectedItem.Value))
            wqReasonList = (From wqr In wq.ReDirectReasons Select New WebControls.ListItem(If(wqr.Description Is Nothing, wqr.ItemStatusReason.Reason, wqr.Description), wqr.ItemStatusReason.Id.ToString())).ToArray()
            BindListControlToArray(rdbtRedirectRsn, wqReasonList, False, False, Guid.Empty.ToString())
            ddlWorkQueueList.SelectedValue = ddlWorkQueueList.SelectedItem.Value
            If (wqReasonList.Count = 0) Then
                msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_REDIRECT_REASONS_TO_POPULATE")
                modalMessageBoxRedirect.Attributes.Add("class", "infoMsg")
                modalMessageBoxRedirect.Attributes.Add("style", "display: block")
                imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
                trLblRedirectRsn.Visible = False
                rdbtRedirectRsn.Visible = False
                btnRedirectContinue.Visible = False
                btnRedirectCancel.Visible = False
            Else
                modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
                modalMessageBoxRedirect.Attributes.Add("style", "display: none")
                imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
                trLblRedirectRsn.Visible = True
                rdbtRedirectRsn.Visible = True
                btnRedirectContinue.Visible = True
                btnRedirectCancel.Visible = True
            End If
        Else
            modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: none")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            trLblRedirectRsn.Visible = False
            rdbtRedirectRsn.Visible = False
            btnRedirectContinue.Visible = True
            btnRedirectCancel.Visible = True
        End If

    End Sub


    Private Sub PopulateRedirectForm()

        lblRedirectModalTitle.Text = TranslationBase.TranslateLabelOrMessage("REDIRECT_REASONS")
        lblRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("REDIRECT_REASONS_SELECT")
        lblQueueToRedirect.Text = TranslationBase.TranslateLabelOrMessage("QUEUE_NAME")
        msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REDIRECT_REASON")

        Dim wqList As WebControls.ListItem()
        Dim wqReasonList As ListItem()
        wqList = (From wq In GetWorkQueueList(State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode, State.WorkQueueItem.WorkQueue.WorkQueue.ActionCode) Select New WebControls.ListItem(wq.Name, wq.Id.ToString())).ToArray()
        BindListControlToArray(ddlWorkQueueList, wqList, False, True, Guid.Empty.ToString())
        If (wqList.Count = 0) Then
            msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_WORK_QUEUES_TO_POPULATE")
            modalMessageBoxRedirect.Attributes.Add("class", "infoMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: block")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
            trLblRedirectRsn.Visible = False
            rdbtRedirectRsn.Visible = False
            btnRedirectContinue.Visible = False
            btnRedirectCancel.Visible = True
        Else
            modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: none")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            trLblRedirectRsn.Visible = False
            rdbtRedirectRsn.Visible = False
            btnRedirectContinue.Visible = True
            btnRedirectCancel.Visible = True
        End If

    End Sub

    Private Function GetWorkQueueList(companyCode As String, actionCode As String) As WorkQueue()
        Dim wkQList As WorkQueue() = BusinessObjectsNew.WorkQueue.GetList("*", companyCode, actionCode, Date.Now.UtcNow, False)
        wkQList = (From wq In wkQList Where wq.Id <> State.WorkQueueItem.WorkQueue.Id Select wq).ToArray()
        Return wkQList
    End Function

    Protected Sub btnRedirectContinue_Click(sender As Object, e As EventArgs) Handles btnRedirectContinue.Click
        If Not ddlWorkQueueList.SelectedIndex > BLANK_ITEM_SELECTED Then
            msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_WORK_QUEUE")
            modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: block")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            mdlPopupRedirect.Show()
            Exit Sub
        Else
            If Not rdbtRedirectRsn.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REDIRECT_REASON")
                modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
                modalMessageBoxRedirect.Attributes.Add("style", "display: block")
                imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                mdlPopupRedirect.Show()
                Exit Sub
            End If
        End If


        State.WorkQueueItem.ReDirect(ddlWorkQueueList.SelectedItem.Text, New Guid(rdbtRedirectRsn.SelectedItem.Value), rdbtRedirectRsn.SelectedItem.Text)
        NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_WKQ, WorkQueueActionForm.ItemActionType.Redirect)

    End Sub

    Protected Sub btnRedirectCancel_Click(sender As Object, e As EventArgs) Handles btnRedirectCancel.Click
        Try
            mdlPopupRedirect.Hide()
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Protected Sub btnRedirectClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRedirectClose.Click
    '    Try
    '        Me.mdlPopupRedirect.Hide()
    '        ControlMgr.SetVisibleControl(Me, pdfIframe, True)
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

    'Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
    '    Try
    '        Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
    '        If Not Me.State.searchDV Is Nothing Then
    '            Me.State.PageIndex = NewCurrentPageIndex(ClaimSearchGridView, State.searchDV.Count, Me.State.PageSize)
    '        End If
    '        Me.ClaimSearchGridView.PageIndex = Me.State.PageIndex
    '        Me.PopulateGrid()
    '        ControlMgr.SetVisibleControl(Me, pdfIframe, False)
    '        Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"
    '        Me.mdlPopup.Show()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region


End Class
