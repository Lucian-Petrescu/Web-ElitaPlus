Imports BO = Assurant.ElitaPlus.BusinessObjectsNew

Public Class ClaimIssueForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const NO_DATA As String = " - "
    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 5
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimBase, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As ClaimBase
        Public IsGridVisible As Boolean = True
        Public SortExpression As String = Claim.ClaimIssuesView.COL_CREATED_DATE & " DESC"
        Public PageIndex As Integer = 0
        Public SelectedClaimIssueId As Guid
        Public PageSize As Integer = 5
        Public ClaimIssuesView As Claim.ClaimIssuesView
        Public InputParameters As Parameters


    End Class


    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)

        Try
            If Not Me.State.InputParameters Is Nothing Then
                Me.State.MyBO = CType(Me.State.InputParameters.ClaimBO, ClaimBase)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase

        Public Sub New(ByVal claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
      
        Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & Me.MasterPage.PageTab
           
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUES")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")

        UpdateBreadCrum()
        Me.MasterPage.MessageController.Clear()

        lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUES")
        lblFileNewIssue.Text = TranslationBase.TranslateLabelOrMessage("FILE_NEW_CLAIM_ISSUE")
        If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Or Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED) Then
            lblFileNewIssue.Visible = False
        End If

        Try
            If Not Me.IsPostBack Then

                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                TranslateGridHeader(Grid)

                Me.State.ClaimIssuesView = Me.State.MyBO.GetClaimIssuesView()
                PopulateFormFromBO()
                PopulateGrid()

            End If

        Catch ex As Threading.ThreadAbortException
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
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.lblCustomerNameValue, .CustomerName)
            Me.PopulateControlFromBOProperty(Me.lblClaimNumberValue, .ClaimNumber)
            Me.PopulateControlFromBOProperty(Me.lblDealerNameValue, .DealerName)
            Me.PopulateControlFromBOProperty(Me.lblCertificateNumberValue, .CertificateNumber)
            Me.PopulateControlFromBOProperty(Me.lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            'Me.PopulateControlFromBOProperty(Me.lblDateOfLossValue, .LossDate.Value.ToString("dd-MMM-yyyy"))
            Me.PopulateControlFromBOProperty(Me.lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            Me.PopulateControlFromBOProperty(Me.lblSerialNumberImeiValue, .SerialNumber)
            Me.PopulateControlFromBOProperty(Me.lblWorkPhoneNumberValue, .MobileNumber)

            If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            ClaimStatusTD.Attributes.Item("Class") = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(Me.State.MyBO.CertificateId)
        Dim oDealer As New Dealer(Me.State.MyBO.CompanyId, Me.State.MyBO.DealerCode)

        Me.PopulateControlFromBOProperty(Me.lblDealerGroupValue, oDealer.DealerGroupName)
        Me.PopulateControlFromBOProperty(Me.lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If
        SubStatusTD.Attributes.Item("Class") = SubStatusTD.Attributes.Item("Class") & " " & cssClassName

        BindListControlToDataView(ddlIssueCode, Me.State.MyBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
        BindListControlToDataView(ddlIssueDescription, Me.State.MyBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)
        txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
        txtCreatedDate.Text = DateTime.Now.ToString(LocalizationMgr.CurrentCulture)

        'If No issues to Add to claim hide the Save and Cancel Button
        If (Me.State.MyBO.Load_Filtered_Issues().Count = 0) Then
            MessageLiteral.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_ISSUES_FOUND")
            modalMessageBox.Attributes.Add("class", "infoMsg")
            modalMessageBox.Attributes.Add("style", "display: block")
            imgIssueMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
            btnSave.Visible = False
            btnCancel.Visible = False
        Else
            btnSave.Visible = True
            btnCancel.Visible = True
            modalMessageBox.Attributes.Add("class", "errorMsg")
            modalMessageBox.Attributes.Add("style", "display: none")
            imgIssueMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
        End If


    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.NavController.Navigate(Me, FlowEvents.EVENT_CANCEL, New ClaimForm.Parameters(Me.State.MyBO.Id))
    End Sub

    Private Sub SaveClaimIssue(ByVal sender As Object, ByVal Args As EventArgs) Handles btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(Me.State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            claimIssue.SaveNewIssue(Me.State.MyBO.Id, New Guid(hdnSelectedIssueCode.Value), Me.State.MyBO.CertificateId, False)
            Select Case claimIssue.StatusCode
                Case Codes.CLAIMISSUE_STATUS__OPEN, Codes.CLAIMISSUE_STATUS__PENDING
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    Dim c As Comment = Me.State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
            End Select
            Me.State.MyBO.Save()

            PopulateFormFromBO()
            Me.State.ClaimIssuesView = Me.State.MyBO.GetClaimIssuesView()
            PopulateGrid()
            Me.MasterPage.MessageController.AddSuccess(TranslationBase.TranslateLabelOrMessage(Message.RECORD_ADDED_OK))
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try


    End Sub


#End Region

#Region "Grid related"

    Public Sub PopulateGrid()

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.PageSize = Me.State.PageSize
        Me.ValidSearchResultCountNew(Me.State.ClaimIssuesView.Count, True)
        Me.HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)
        Me.SetPageAndSelectedIndexFromGuid(Me.State.ClaimIssuesView, Me.State.SelectedClaimIssueId, Me.Grid, Me.State.PageIndex)
        Me.Grid.DataSource = Me.State.ClaimIssuesView
        Me.Grid.DataBind()
        If (Me.State.ClaimIssuesView.Count > 0) Then
            Me.State.IsGridVisible = True
            dvGridPager.Visible = True
        Else
            Me.State.IsGridVisible = False
            dvGridPager.Visible = False
        End If
        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        If Me.Grid.Visible Then
            Me.lblRecordCount.Text = Me.State.ClaimIssuesView.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
        If (Me.State.MyBO.HasIssues) Then
            Select Case Me.State.MyBO.IssuesStatus()
                Case Codes.CLAIMISSUE_STATUS__OPEN
                    mcIssueStatus.Clear()
                    mcIssueStatus.AddError(Message.MSG_CLAIM_ISSUES_PENDING)
                Case Codes.CLAIMISSUE_STATUS__REJECTED
                    mcIssueStatus.Clear()
                    mcIssueStatus.AddError(Message.MSG_CLAIM_ISSUES_REJECTED)
                Case Codes.CLAIMISSUE_STATUS__RESOLVED
                    mcIssueStatus.Clear()
                    mcIssueStatus.AddSuccess(Message.MSG_CLAIM_ISSUES_RESOLVED)

            End Select
        End If

    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.ClaimIssuesView.Sort = Me.State.SortExpression
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.SelectedClaimIssueId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(1).FindControl("EditButton_WRITE") Is Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimIssuesView.COL_CLAIM_ISSUE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(Claim.ClaimIssuesView.COL_ISSUE_DESC).ToString
                End If

                ' Convert short status codes to full description with css
                e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(CLAIM_ISSUE_LIST, dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString)
                If (dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__RESOLVED Or _
                          dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                    e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.SelectedClaimIssueId = New Guid(e.CommandArgument.ToString())
                   Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT, New ClaimIssueDetailForm.Parameters(Me.State.MyBO, Me.State.SelectedClaimIssueId))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso _
           (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.ClaimIssuesView.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class