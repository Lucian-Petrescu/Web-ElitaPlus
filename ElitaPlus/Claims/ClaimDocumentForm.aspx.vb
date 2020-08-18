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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimImage, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try

            If Not Me.CallingParameters Is Nothing Then

                'If CallingPar(0).GetType Is GetType(Claim) Then
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(CType(Me.CallingParameters, ArrayList)(0), ClaimBase).Id)
                'End if
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & Me.MasterPage.PageTab
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGES")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")

            UpdateBreadCrum()
            Me.MasterPage.MessageController.Clear()

            lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGES")

            If Not Me.IsPostBack Then
                Me.TranslateGridHeader(Me.ClaimDocumentsGridView)
                PopulateFormFromBO()
                'check if the user has access to delete the images 
                Me.State.AllowDeleteOfImage = Me.CanSetControlEnabled(HiddenIsDeleteImagesAllowed.ID)
                Me.PopulateGrid()
                Dim oDocumentTypeDropDown As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                DocumentTypeDropDown.Populate(oDocumentTypeDropDown, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })
                Me.ClearForm()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateFormFromBO()
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With Me.State.ClaimBO
            Me.PopulateControlFromBOProperty(Me.lblCustomerNameValue, .CustomerName)
            Me.PopulateControlFromBOProperty(Me.lblClaimNumberValue, .ClaimNumber)
            Me.PopulateControlFromBOProperty(Me.lblDealerNameValue, .DealerName)
            Me.PopulateControlFromBOProperty(Me.lblCertificateNumberValue, .CertificateNumber)
            Me.PopulateControlFromBOProperty(Me.lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            Me.PopulateControlFromBOProperty(Me.lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            Me.PopulateControlFromBOProperty(Me.lblSerialNumberImeiValue, .SerialNumber)
            Me.PopulateControlFromBOProperty(Me.lblWorkPhoneNumberValue, .MobileNumber)

            If (Me.State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            ClaimStatusTD.Attributes.Item("Class") = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(Me.State.ClaimBO.CertificateId)
        Dim oDealer As New Dealer(Me.State.ClaimBO.CompanyId, Me.State.ClaimBO.DealerCode)

        Me.PopulateControlFromBOProperty(Me.lblDealerGroupValue, oDealer.DealerGroupName)
        Me.PopulateControlFromBOProperty(Me.lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
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
            If (Me.State.ClaimImagesView Is Nothing) Then
                Me.State.ClaimImagesView = Me.State.ClaimBO.GetClaimImagesView(Me.State.AllowDeleteOfImage)
            End If


            Me.ClaimDocumentsGridView.AutoGenerateColumns = False
            Me.ClaimDocumentsGridView.PageSize = Me.State.PageSize
            Me.ValidSearchResultCountNew(Me.State.claimImagesView.Count, True)
            Me.HighLightSortColumn(Me.ClaimDocumentsGridView, Me.State.SortExpression, Me.IsNewUI)
            Me.SetPageAndSelectedIndexFromGuid(Me.State.claimImagesView, Me.State.selectedImageId, Me.ClaimDocumentsGridView, Me.State.PageIndex)
            Me.ClaimDocumentsGridView.DataSource = Me.State.claimImagesView
            Me.ClaimDocumentsGridView.DataBind()

            If (Me.State.claimImagesView.Count > 0) Then
                Me.State.IsGridVisible = True
                dvGridPager.Visible = True
            Else
                Me.State.IsGridVisible = False
                dvGridPager.Visible = False
            End If
            ControlMgr.SetVisibleControl(Me, ClaimDocumentsGridView, Me.State.IsGridVisible)
            If Me.ClaimDocumentsGridView.Visible Then
                Me.lblRecordCount.Text = Me.State.claimImagesView.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                If Me.State.AllowDeleteOfImage Then
                    Me.ClaimDocumentsGridView.Columns(GRID_COL_DELETE_ACTION_IDX).Visible = True
                Else
                    Me.ClaimDocumentsGridView.Columns(GRID_COL_DELETE_ACTION_IDX).Visible = False
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimDocumentsGridView_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimDocumentsGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClaimDocumentsGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ClaimDocumentsGridView.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            Dim btnAddRemoveImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (Not e.Row.Cells(GRID_COL_IMAGE_ID_IDX).FindControl("btnImageLink") Is Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), Me.State.ClaimBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
                End If

                If (Not e.Row.Cells(GRID_COL_FILE_SIZE_IDX).FindControl("btnImageLink") Is Nothing) AndAlso _
                    (Not dvRow(Claim.ClaimImagesView.COL_FILE_SIZE_BYTES) Is Nothing) Then
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

                If (Not e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text Is Nothing) Then
                    e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text = GetLongDate12FormattedString(e.Row.Cells(GRID_COL_SCAN_DATE_IDX).Text)
                End If

                btnAddRemoveImage = CType(e.Row.Cells(0).FindControl("btnAddRemoveImage"), LinkButton)

                If Me.State.AllowDeleteOfImage Then
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClaimDocumentsGridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ClaimDocumentsGridView.RowCommand
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
                Me.RegisterStartupScript("Startup", x)
            End If
        ElseIf (e.CommandName = DELETE_IMAGE OrElse e.CommandName = UNDO_DELETE_IMAGE) Then
            If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                Dim claimImageIdString As String = CType(e.CommandArgument, String)
                Dim claimImageID As Guid = GetGuidFromString(claimImageIdString)
                Dim oClaimImage As ClaimImage =  DirectCast(Me.State.ClaimBO.ClaimImagesList.GetChild(claimImageID), ClaimImage)  

                ' delete the document or reactivate the document
                If e.CommandName = DELETE_IMAGE Then
                    oClaimImage.DeleteFlag = "Y" 
                Else
                    oClaimImage.DeleteFlag = "N" 
                End If  
                oClaimImage.UpdateDocumentDeleteFlag(ElitaPlusIdentity.Current.ActiveUser.NetworkId)  
                Me.State.ClaimImagesView = Nothing
                Me.ClearForm()
                Me.PopulateGrid()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            End If
        End If
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles ClaimDocumentsGridView.Sorting
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
            Me.State.claimImagesView.Sort = Me.State.SortExpression
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClaimDocumentsGridView.PageIndexChanged
        Try
            Me.State.PageIndex = ClaimDocumentsGridView.PageIndex
            Me.State.selectedImageId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles ClaimDocumentsGridView.PageIndexChanging
        Try
            ClaimDocumentsGridView.PageIndex = e.NewPageIndex
            State.PageIndex = ClaimDocumentsGridView.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(ClaimDocumentsGridView, State.claimImagesView.Count, State.PageSize)
            Me.ClaimDocumentsGridView.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Click"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Dim retType As New ClaimForm.ReturnType(cmd)
        Me.ReturnToCallingPage(retType)
    End Sub

    Private Sub ClearForm()
        Me.PopulateControlFromBOProperty(Me.DocumentTypeDropDown, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER))
        Me.PopulateControlFromBOProperty(Me.ScanDateTextBox, GetLongDateFormattedString(DateTime.Now))
        Me.CommentTextBox.Text = String.Empty
    End Sub

    Protected Sub ClearButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ClearButton.Click
        Try
            Me.ClearForm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub AddImageButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddImageButton.Click
        Try
            Dim valid As Boolean = True
            If (Me.DocumentTypeDropDown.SelectedIndex = -1) Then
                Me.MasterPage.MessageController.AddError("DOCUMENT_TYPE_IS_REQUIRED")
                valid = False
            End If

            If (Me.ImageFileUpload.Value Is Nothing) OrElse _
               (Me.ImageFileUpload.PostedFile.ContentLength = 0) Then
                Me.MasterPage.MessageController.AddError("INVALID_FILE_OR_FILE_NOT_ACCESSABLE")
                valid = False
            End If

            Dim reader As BinaryReader = New BinaryReader(ImageFileUpload.PostedFile.InputStream)
            Dim fileData() As Byte = reader.ReadBytes(ImageFileUpload.PostedFile.ContentLength)
            Dim fileName As String

            Try
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(Me.ImageFileUpload.PostedFile.FileName)
                fileName = file.Name
            Catch ex As Exception
                fileName = String.Empty
            End Try

            Me.State.ClaimBO.AttachImage( _
                New Guid(Me.DocumentTypeDropDown.SelectedValue), _
                Nothing, _
                DateTime.Now, _
                fileName, _
                Me.CommentTextBox.Text, _
                ElitaPlusIdentity.Current.ActiveUser.UserName, _
                fileData)

            Me.State.ClaimBO.Save()
            Me.State.ClaimImagesView = Nothing
            Me.ClearForm()
            Me.PopulateGrid()
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            ' Remove Mandatory Fields Validations for Hash, File Type and File Name
            Dim removeProperties As String() = New String() {"FileType", "FileName", "HashValue"}
            Dim newException As BOValidationException = _
                New BOValidationException( _
                    ex.ValidationErrorList().Where(Function(ve) (Not ((ve.Message = Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR) AndAlso (removeProperties.Contains(ve.PropertyName))))).ToArray(), _
                    ex.BusinessObjectName,
                    ex.UniqueId)
            Me.HandleErrors(newException, Me.MasterPage.MessageController)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

End Class
