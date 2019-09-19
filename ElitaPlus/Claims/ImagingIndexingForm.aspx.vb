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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimImage, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
        Public WorkQueueItem As WorkQueueItem
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.WorkQueueItem = CType(Me.NavController.ParametersPassed, WorkQueueItem)
    End Sub

#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(WORK_QUEUE) _
                                        & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(IMAGING_INDEXING)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            Me.moMessageController.Clear_Hide()
            If Not Me.IsPostBack Then
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(IMAGING_INDEXING)
                Me.UpdateBreadCrum()

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ClaimImage
                End If
                Me.BindBOPropertiesToLabel()

                'Populate Grid View fields in ajax pop up
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.PopulateSortByDropDown()
                Me.PopulateClaimStatusDropDown()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        ClaimSearchGridView.PageSize = Me.State.selectedPageSize
                    End If
                End If
                Me.TranslateGridHeader(Me.ClaimSearchGridView)
                Me.SetGridItemStyleColor(Me.ClaimSearchGridView)
                SetFocus(Me.TextBoxSearchClaimNumber)

                'Load the Image and Attributes to the iFrame
                LoadDocumentBeforeIndexing()

                btnSave.Enabled = False
                'btnRedirect.Enabled = False
                PopulateRedirectForm()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        Finally
        End Try

    End Sub

    Private Sub LoadDocumentBeforeIndexing()
        Dim oDoc As Doc.Document = New Doc.Document
        Dim xid As Guid
        Dim oDocInfo As Doc.DocumentInfo = New Doc.DocumentInfo
        Try
            xid = Me.State.WorkQueueItem.WorkQueueItem.ImageId
            oDoc = DocumentImaging.Doc.DownloadDocument(xid)
            oDocInfo = CType(oDoc, DocumentInfo)
            Me.State.DocInfo = New Doc.DocumentInfo
            'Using the Reflection Extension method directly on the source object
            oDocInfo.CopyProperties(Me.State.DocInfo)
            Me.State.ImageName = Me.State.WorkQueueItem.WorkQueueItem.ImageId.ToString
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        txtImage.Text = Me.State.ImageName
        txtScanDate.Text = If(Me.State.WorkQueueItem.ImageScanDate.HasValue, GetDateFormattedString(Me.State.WorkQueueItem.ImageScanDate.Value), String.Empty)
        'Sridhar txtScanDate.Text = If(Me.State.WorkQueueItem.ImageScanDate.HasValue, Me.State.WorkQueueItem.ImageScanDate.Value.ToString(DATE_FORMAT), String.Empty)
        pdfIframe.Attributes(ATTRIB_SRC) = PDF_URL + xid.ToString
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBOPropertiesToLabel()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimId", lblClaimNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ImageId", Me.lblImage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentTypeId", Me.lblDocumentType)
    End Sub

    Protected Sub PopulateBOsFromForm()
        With Me.State.MyBO
            Me.State.MyBO.ClaimId = Me.State.selectedClaimId
            If Me.State.MyBO.ImageId = Nothing Or Me.State.MyBO.ImageId = Guid.Empty Then
                Me.State.MyBO.ImageId = Me.State.WorkQueueItem.WorkQueueItem.ImageId
            End If
            Me.PopulateBOProperty(Me.State.MyBO, "DocumentTypeId", Me.ddlDocumentType)
            Me.State.MyBO.ScanDate = Me.State.WorkQueueItem.ImageScanDate.Value
            Me.PopulateBOProperty(Me.State.MyBO, "ImageStatusId", LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PENDING))
        End With
    End Sub

    Private Sub SortSvc(ByVal oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateSortByDropDown()
        Dim oSortDv As DataView
        Try
            'oSortDv = LookupListNew.GetClaimSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'SortSvc(oSortDv)
            ' Me.BindListControlToDataView(Me.cboSortBy, oSortDv, , , False

            Dim claimSearchFieldsLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CSEDR", Thread.CurrentPrincipal.GetLanguageCode())
            Me.cboSortBy.Populate(claimSearchFieldsLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    })

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If

            'Me.BindListControlToDataView(ddlDocumentType, LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            Dim documentTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlDocumentType.Populate(documentTypeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    })



        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateClaimStatusDropDown()
        Try
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(""))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_PENDING))
            cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_CLOSED))
            If Not Me.State.claimStatus Is String.Empty Then
                Dim setClaimStatusText As String
                If Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_ACTIVE
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__PENDING Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_PENDING
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_DENIED
                ElseIf Me.State.claimStatus = Codes.CLAIM_STATUS__CLOSED Then
                    setClaimStatusText = CLAIM_STATUS_DESCRIPTION_CLOSED
                End If
                Me.SetSelectedItemByText(Me.cboClaimStatus, setClaimStatusText)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "PopUp Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If (Me.TextBoxSearchClaimNumber.Text = "" AndAlso
                Me.TextBoxSearchCustomerName.Text = "" AndAlso
                Me.moServiceCenterText.Text = "" AndAlso
                Me.TextBoxSearchAuthorizationNumber.Text = "" AndAlso
                Me.TextBoxSearchAuthorizedAmount.Text = "") Then
                Me.moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)

                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                Me.mdlPopup.Show()
                Exit Sub
            End If
            Me.State.PageIndex = 0
            Me.State.searchDV = Nothing
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.PopulateGrid()

            If Me.State.searchDV Is Nothing Then
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
                Me.mdlPopup.Show()
            Else ' If Search Results are retrieved
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, True)
                Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"
                Me.mdlPopup.Show()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            clearSearch()
            Me.ClaimSearchGridView.DataSource = Me.State.searchDV
            Me.ClaimSearchGridView.DataBind()
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub clearSearch()
        Me.TextBoxSearchClaimNumber.Text = String.Empty
        Me.TextBoxSearchCustomerName.Text = String.Empty
        Me.moServiceCenterText.Text = String.Empty
        Me.TextBoxSearchAuthorizationNumber.Text = String.Empty
        Me.TextBoxSearchAuthorizedAmount.Text = String.Empty
    End Sub

    Private Sub btnCancelSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSearch.Click
        Try
            Me.mdlPopup.Hide()
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text
            Me.State.serviceCenterName = moServiceCenterText.Text
            Me.State.authorizationNumber = Me.TextBoxSearchAuthorizationNumber.Text
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            'Set the ClaimStatus
            If cboClaimStatus.SelectedItem.Text = "" Then
                Me.State.claimStatus = String.Empty
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_PENDING Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__PENDING
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_CLOSED Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__CLOSED
            End If

            If Not Me.TextBoxSearchAuthorizedAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.TextBoxSearchAuthorizedAmount.Text, dblAmount) Then
                    Me.moMessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR, True)
                    Return False
                Else
                    Me.State.authorizedAmountCulture = Me.TextBoxSearchAuthorizedAmount.Text
                    Me.State.authorizedAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                Me.State.authorizedAmount = Me.TextBoxSearchAuthorizedAmount.Text
            End If

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function

    Public Sub PopulateGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)

                Me.State.searchDV = Claim.getClaimListForImageIndexing(Me.State.claimNumber,
                                                                       Me.State.claimStatus,
                                                                       Me.State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode,
                                                                       Me.State.customerName,
                                                                       Me.State.serviceCenterName,
                                                                       Me.State.authorizationNumber,
                                                                       Me.State.authorizedAmount,
                                                                       Nothing,
                                                                       sortBy)

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.ClaimSearchGridView.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.ClaimSearchGridView, Me.State.PageIndex)
            'Me.State.PageIndex = Me.ClaimSearchGridView.CurrentPageIndex
            Me.ClaimSearchGridView.DataSource = Me.State.searchDV
            'PutInvisibleSvcColumns(Me.ClaimSearchGridView)
            Me.ClaimSearchGridView.AllowSorting = False
            Me.ClaimSearchGridView.DataBind()
            ControlMgr.SetVisibleControl(Me, ClaimSearchGridView, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.ClaimSearchGridView.Visible)
            Session("recCount") = Me.State.searchDV.Count
            If Me.State.searchDV.Count > 0 Then
                If Me.ClaimSearchGridView.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                Me.lblRecordCount.ForeColor = Color.Black
                If Me.ClaimSearchGridView.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PutInvisibleSvcColumns(ByVal oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZATION_NUMBER_IDX).Visible = False
                oGrid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).Visible = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimSearchGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ClaimSearchGridView.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.ClaimSearchGridView.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
            Me.ClaimSearchGridView.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            'Me.dvBottom.Visible = True
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "Block"
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimSearchGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimSearchGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_STATUS_CODE))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(Claim.ClaimSearchDV.COL_CUSTOMER_NAME))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimSearchDV.COL_SERVICE_CENTER_NAME))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_AUTHORIZED_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_CLAIM_ID))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub btnNewItemAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewItemAdd.Click
        Dim oClaim As Claim
        Dim oCompany As Company
        Try
            For Each gvr As GridViewRow In ClaimSearchGridView.Rows
                If gvr.RowType <> DataControlRowType.DataRow Then Continue For
                If TryCast(gvr.Cells(0).FindControl("radioSelect"), RadioButton).Checked Then
                    Me.txtClaimNumber.Text = gvr.Cells(IDX_CLAIM_NUMBER).Text
                    Me.State.selectedClaimId = New Guid(gvr.Cells(IDX_CLAIM_ID).Text)
                    Me.hfClaimId.Value = gvr.Cells(IDX_CLAIM_ID).Text
                    oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.selectedClaimId)
                    Me.txtCertificateNumber.Text = oClaim.CertificateNumber
                    Me.txtMobileNumber.Text = oClaim.MobileNumber
                    Me.State.SelectedClaimNumber = oClaim.ClaimNumber
                    oCompany = New Company(oClaim.CompanyId)
                    Me.State.SelectedCompanyCode = oCompany.Code
                    Exit For
                End If
            Next
            btnSave.Enabled = True
            btnRedirect.Enabled = True
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            Me.mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            'Update the Document Service with the Claim Info
            UpdateDocumentAfterIndexing()

            Me.PopulateBOsFromForm()
            'Save Claim Image Mapping
            Me.State.MyBO.Save()

            'Update the WorkQueue Item as Processed
            Me.State.WorkQueueItem.Process()
            ' Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            Me.NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_WKQ, WorkQueueActionForm.ItemActionType.Process)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateDocumentAfterIndexing()
        Dim oFindReq As FindRequest = New FindRequest
        Dim oMataData As DocumentMetadata
        Dim oDocInfo() As DocumentInfo = New DocumentInfo() {New DocumentInfo()}
        'oFindReq.Name = Me.State.ImageName
        'oDocInfo = DocumentImaging.Doc.FindDocument(oFindReq)
        oDocInfo(0) = Me.State.DocInfo
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
            oMataData.Value = Me.State.SelectedClaimNumber
            oDocInfo(IDX_DOC_INFO).MetadataList(IDX_ATTB_TWO) = oMataData
            '#Update Metadata - Company Code
            oMataData = New DocumentMetadata
            oMataData.Id = Guid.NewGuid
            oMataData.Name = ATTB_COMPANYCODE
            oMataData.Value = Me.State.SelectedCompanyCode
            oDocInfo(IDX_DOC_INFO).MetadataList(IDX_ATTB_THREE) = oMataData
            oDocInfo(IDX_DOC_INFO).UpdatedBy = ElitaPlusIdentity.Current.ActiveUser.UniqueId
            DocumentImaging.Doc.UpdateDocument(oDocInfo(IDX_DOC_INFO))
        Else
            Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.ERR_SAVING_DATA)
        End If

    End Sub

    Protected Sub imgBtnClaimLookup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles imgBtnClaimLookup.Click
        Try
            clearSearch()
            Me.lblRecordCount.Text = ""
            Me.State.searchDV = Nothing
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            Me.mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemCancel.Click
        Try
            Me.mdlPopup.Hide()
            Me.dvBottom.Style(HtmlTextWriterStyle.Display) = "None"
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnRedirect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRedirect.Click
        Try
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            Me.mdlPopupRedirect.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlWorkQueueList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWorkQueueList.SelectedIndexChanged

        Dim wq As WorkQueue
        Dim wqReasonList As System.Web.UI.WebControls.ListItem()
        If (Not String.IsNullOrEmpty(ddlWorkQueueList.SelectedItem.Value)) Then
            wq = New WorkQueue(New Guid(ddlWorkQueueList.SelectedItem.Value))
            wqReasonList = (From wqr In wq.ReDirectReasons Select New System.Web.UI.WebControls.ListItem(If(wqr.Description Is Nothing, wqr.ItemStatusReason.Reason, wqr.Description), wqr.ItemStatusReason.Id.ToString())).ToArray()
            Me.BindListControlToArray(rdbtRedirectRsn, wqReasonList, False, False, Guid.Empty.ToString())
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

        Dim wqList As System.Web.UI.WebControls.ListItem()
        Dim wqReasonList As ListItem()
        wqList = (From wq In GetWorkQueueList(Me.State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode, Me.State.WorkQueueItem.WorkQueue.WorkQueue.ActionCode) Select New System.Web.UI.WebControls.ListItem(wq.Name, wq.Id.ToString())).ToArray()
        Me.BindListControlToArray(ddlWorkQueueList, wqList, False, True, Guid.Empty.ToString())
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

    Private Function GetWorkQueueList(ByVal companyCode As String, ByVal actionCode As String) As WrkQueue.WorkQueue()
        Dim wkQList As WrkQueue.WorkQueue() = WorkQueue.GetList("*", companyCode, actionCode, Date.Now.UtcNow, False)
        wkQList = (From wq In wkQList Where wq.Id <> Me.State.WorkQueueItem.WorkQueue.Id Select wq).ToArray()
        Return wkQList
    End Function

    Protected Sub btnRedirectContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedirectContinue.Click
        If Not ddlWorkQueueList.SelectedIndex > BLANK_ITEM_SELECTED Then
            msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_WORK_QUEUE")
            modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: block")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            ControlMgr.SetVisibleControl(Me, pdfIframe, False)
            Me.mdlPopupRedirect.Show()
            Exit Sub
        Else
            If Not rdbtRedirectRsn.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REDIRECT_REASON")
                modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
                modalMessageBoxRedirect.Attributes.Add("style", "display: block")
                imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
                ControlMgr.SetVisibleControl(Me, pdfIframe, False)
                Me.mdlPopupRedirect.Show()
                Exit Sub
            End If
        End If


        Me.State.WorkQueueItem.ReDirect(ddlWorkQueueList.SelectedItem.Text, New Guid(rdbtRedirectRsn.SelectedItem.Value), rdbtRedirectRsn.SelectedItem.Text)
        Me.NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_WKQ, WorkQueueActionForm.ItemActionType.Redirect)

    End Sub

    Protected Sub btnRedirectCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRedirectCancel.Click
        Try
            Me.mdlPopupRedirect.Hide()
            ControlMgr.SetVisibleControl(Me, pdfIframe, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
