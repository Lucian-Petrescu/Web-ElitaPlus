Imports System.IO
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Public Class ClaimDocumentForm
    Inherits ElitaPlusSearchPage

#Region "Const"
    Public Const GRID_COL_IMAGE_ID_IDX As Integer = 0
    Public Const GRID_COL_FILE_SIZE_IDX As Integer = 2
    Public Const GRID_COL_IMAGE_STATUS_IDX As Integer = 6
    Public Const GRID_COL_SCAN_DATE_IDX As Integer = 3
    Public Const GRID_COL_DELETE_ACTION_IDX As Integer = 8
    Public Const NO_DATA As String = " - "

    Public Const ATTRIB_SRC As String = "src"
    Public Const PDF_URL As String = "DisplayPdf.aspx?ImageId="
    Public Const SELECT_ACTION_IMAGE As String = "SelectActionImage"
    Public Const DELETE_IMAGE As String = "DeleteImage"
    Public Const UNDO_DELETE_IMAGE As String = "UndoDeleteImage"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimImage
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimImage, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public ClaimBO As ClaimBase
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimImagesView.COL_IMAGE_ID
        Public selectedImageId As Guid = Guid.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public IsGridVisible As Boolean = True
        Public ClaimImagesView As Claim.ClaimImagesView
        Public AllowDeleteOfImage As Boolean = True
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

                'If CallingPar(0).GetType Is GetType(Claim) Then
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(CType(CallingParameters, ArrayList)(0), ClaimBase).Id)
                'End if
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & MasterPage.PageTab
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGES")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")

            UpdateBreadCrum()
            MasterPage.MessageController.Clear()

            lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGES")

            If Not IsPostBack Then
                TranslateGridHeader(ClaimDocumentsGridView)
                PopulateFormFromBO()
                'check if the user has access to delete the images 
                State.AllowDeleteOfImage = CanSetControlEnabled(HiddenIsDeleteImagesAllowed.ID)
                PopulateGrid()

                Dim oDocumentTypeDropDown As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                DocumentTypeDropDown.Populate(oDocumentTypeDropDown, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })
                ClearForm()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateFormFromBO()
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With State.ClaimBO
            PopulateControlFromBOProperty(lblCustomerNameValue, .CustomerName)
            PopulateControlFromBOProperty(lblClaimNumberValue, .ClaimNumber)
            PopulateControlFromBOProperty(lblDealerNameValue, .DealerName)
            PopulateControlFromBOProperty(lblCertificateNumberValue, .CertificateNumber)
            PopulateControlFromBOProperty(lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            PopulateControlFromBOProperty(lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            PopulateControlFromBOProperty(lblSerialNumberImeiValue, .SerialNumber)
            PopulateControlFromBOProperty(lblWorkPhoneNumberValue, .MobileNumber)

            If (State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            ClaimStatusTD.Attributes.Item("Class") = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(State.ClaimBO.CertificateId)
        Dim oDealer As New Dealer(State.ClaimBO.CompanyId, State.ClaimBO.DealerCode)

        PopulateControlFromBOProperty(lblDealerGroupValue, oDealer.DealerGroupName)
        PopulateControlFromBOProperty(lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If
        SubStatusTD.Attributes.Item("Class") = SubStatusTD.Attributes.Item("Class") & " " & cssClassName

    End Sub

#End Region

#Region "Grid Related Functions"

    Public Sub PopulateGrid()

        Try
            If (State.ClaimImagesView Is Nothing) Then
                State.ClaimImagesView = State.ClaimBO.GetClaimImagesView(State.AllowDeleteOfImage)
            End If


            ClaimDocumentsGridView.AutoGenerateColumns = False
            ClaimDocumentsGridView.PageSize = State.PageSize
            ValidSearchResultCountNew(State.claimImagesView.Count, True)
            HighLightSortColumn(ClaimDocumentsGridView, State.SortExpression, IsNewUI)
            SetPageAndSelectedIndexFromGuid(State.claimImagesView, State.selectedImageId, ClaimDocumentsGridView, State.PageIndex)
            ClaimDocumentsGridView.DataSource = State.claimImagesView
            ClaimDocumentsGridView.DataBind()

            If (State.claimImagesView.Count > 0) Then
                State.IsGridVisible = True
                dvGridPager.Visible = True
            Else
                State.IsGridVisible = False
                dvGridPager.Visible = False
            End If
            ControlMgr.SetVisibleControl(Me, ClaimDocumentsGridView, State.IsGridVisible)
            If ClaimDocumentsGridView.Visible Then
                lblRecordCount.Text = State.claimImagesView.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                If State.AllowDeleteOfImage Then
                    ClaimDocumentsGridView.Columns(GRID_COL_DELETE_ACTION_IDX).Visible = True
                Else
                    ClaimDocumentsGridView.Columns(GRID_COL_DELETE_ACTION_IDX).Visible = False
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimDocumentsGridView_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimDocumentsGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimDocumentsGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimDocumentsGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            Dim btnAddRemoveImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (e.Row.Cells(GRID_COL_IMAGE_ID_IDX).FindControl("btnImageLink") IsNot Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), State.ClaimBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
                End If

                If (e.Row.Cells(GRID_COL_FILE_SIZE_IDX).FindControl("btnImageLink") IsNot Nothing) AndAlso _
                    (dvRow(Claim.ClaimImagesView.COL_FILE_SIZE_BYTES) IsNot Nothing) Then
                    Dim fileSize As Long = CType(dvRow(Claim.ClaimImagesView.COL_FILE_SIZE_BYTES), Long)
                    Dim fileSizeLabel As Label = CType(e.Row.Cells(GRID_COL_FILE_SIZE_IDX).FindControl("FileSizeLabel"), Label)
                    If (fileSize > 1048576) Then
                        ' Display in MB
                        fileSizeLabel.Text = String.Format("{0} {1}", Math.Round(fileSize / 1048576, 2).ToString(), " MB")
                    ElseIf (fileSize > 1024) Then
                        ' Display in KB
                        fileSizeLabel.Text = String.Format("{0} {1}", Math.Round(fileSize / 1024, 2).ToString(), " KB")
                    Else
                        ' Display in Bytes
                        fileSizeLabel.Text = String.Format("{0} {1}", fileSize.ToString(), " Byte(s)")
                    End If
                End If

                If (e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text IsNot Nothing) Then
                    e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text = GetLongDate12FormattedString(e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text)
                End If

                btnAddRemoveImage = CType(e.Row.Cells(0).FindControl("btnAddRemoveImage"), LinkButton)

                If State.AllowDeleteOfImage Then
                    btnAddRemoveImage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_CLAIM_IMAGE_ID), Byte()))
                    If CType(dvRow(Claim.ClaimImagesView.COL_DELETE_FLAG), String) = "Y" Then
                        btnAddRemoveImage.Text = TranslationBase.TranslateLabelOrMessage("UNDO_DELETE_IMAGE")
                        btnAddRemoveImage.CommandName = UNDO_DELETE_IMAGE
                    Else
                        btnAddRemoveImage.Text = TranslationBase.TranslateLabelOrMessage("DELETE_IMAGE")
                        btnAddRemoveImage.CommandName = DELETE_IMAGE
                    End If
                Else
                    btnAddRemoveImage.Visible = False
                End If

                If (dvRow(Claim.ClaimImagesView.COL_STATUS_CODE).ToString = Codes.CLAIM_IMAGE_PROCESSED) Then
                        e.Row.Cells(GRID_COL_IMAGE_STATUS_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(GRID_COL_IMAGE_STATUS_IDX).CssClass = "StatInactive"
                    End If
                End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClaimDocumentsGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ClaimDocumentsGridView.RowCommand
        If (e.CommandName = SELECT_ACTION_IMAGE) Then
            If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                Dim args() As String = CType(e.CommandArgument, String).Split(";".ToCharArray())
                Dim claimIdString As String = args(1)
                Dim imageIdString As String = args(0)
                Dim isLocalRepository As String = args(2)

                lblClaimImage.Text = String.Format("{0}: {1}", TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGE"), imageIdString)
                If (isLocalRepository = "Y") Then
                    pdfIframe.Attributes(ATTRIB_SRC) = PDF_URL + String.Format("{0}&ClaimId={1}", imageIdString, claimIdString)
                Else
                    pdfIframe.Attributes(ATTRIB_SRC) = PDF_URL + e.CommandArgument.ToString()
                End If

                Dim x As String = "<script language='JavaScript'> revealModal('modalClaimImages') </script>"
                RegisterStartupScript("Startup", x)
            End If
        ElseIf (e.CommandName = DELETE_IMAGE OrElse e.CommandName = UNDO_DELETE_IMAGE) Then
            If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                Dim claimImageIdString As String = CType(e.CommandArgument, String)
                Dim claimImageID As Guid = GetGuidFromString(claimImageIdString)
                Dim oClaimImage As ClaimImage =  DirectCast(State.ClaimBO.ClaimImagesList.GetChild(claimImageID), ClaimImage)  

                ' delete the document or reactivate the document
                If e.CommandName = DELETE_IMAGE Then
                    oClaimImage.DeleteFlag = "Y" 
                Else
                    oClaimImage.DeleteFlag = "N" 
                End If  
                oClaimImage.UpdateDocumentDeleteFlag(ElitaPlusIdentity.Current.ActiveUser.NetworkId)  
                State.ClaimImagesView = Nothing
                ClearForm()
                PopulateGrid()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            End If
        End If
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles ClaimDocumentsGridView.Sorting
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
            State.claimImagesView.Sort = State.SortExpression
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles ClaimDocumentsGridView.PageIndexChanged
        Try
            State.PageIndex = ClaimDocumentsGridView.PageIndex
            State.selectedImageId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ClaimDocumentsGridView.PageIndexChanging
        Try
            ClaimDocumentsGridView.PageIndex = e.NewPageIndex
            State.PageIndex = ClaimDocumentsGridView.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(ClaimDocumentsGridView, State.claimImagesView.Count, State.PageSize)
            ClaimDocumentsGridView.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Click"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        Dim retType As New ClaimForm.ReturnType(cmd)
        ReturnToCallingPage(retType)
    End Sub

    Private Sub ClearForm()
        PopulateControlFromBOProperty(DocumentTypeDropDown, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER))
        PopulateControlFromBOProperty(ScanDateTextBox, GetLongDateFormattedString(DateTime.Now))
        CommentTextBox.Text = String.Empty
    End Sub

    Protected Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Try
            ClearForm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub AddImageButton_Click(sender As Object, e As EventArgs) Handles AddImageButton.Click
        Try
            Dim valid As Boolean = True
            If (DocumentTypeDropDown.SelectedIndex = -1) Then
                MasterPage.MessageController.AddError("DOCUMENT_TYPE_IS_REQUIRED")
                valid = False
            End If

            If (ImageFileUpload.Value Is Nothing) OrElse _
               (ImageFileUpload.PostedFile.ContentLength = 0) Then
                MasterPage.MessageController.AddError("INVALID_FILE_OR_FILE_NOT_ACCESSABLE")
                valid = False
            End If

            Dim reader As BinaryReader = New BinaryReader(ImageFileUpload.PostedFile.InputStream)
            Dim fileData() As Byte = reader.ReadBytes(ImageFileUpload.PostedFile.ContentLength)
            Dim fileName As String

            Try
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(ImageFileUpload.PostedFile.FileName)
                fileName = file.Name
            Catch ex As Exception
                fileName = String.Empty
            End Try

            State.ClaimBO.AttachImage( _
                New Guid(DocumentTypeDropDown.SelectedValue), _
                Nothing, _
                DateTime.Now, _
                fileName, _
                CommentTextBox.Text, _
                ElitaPlusIdentity.Current.ActiveUser.UserName, _
                fileData)

            State.ClaimBO.Save()
            State.ClaimImagesView = Nothing
            ClearForm()
            PopulateGrid()
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            ' Remove Mandatory Fields Validations for Hash, File Type and File Name
            Dim removeProperties As String() = New String() {"FileType", "FileName", "HashValue"}
            Dim newException As BOValidationException = _
                New BOValidationException( _
                    ex.ValidationErrorList().Where(Function(ve) (Not ((ve.Message = Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR) AndAlso (removeProperties.Contains(ve.PropertyName))))).ToArray(), _
                    ex.BusinessObjectName,
                    ex.UniqueId)
            HandleErrors(newException, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

End Class
