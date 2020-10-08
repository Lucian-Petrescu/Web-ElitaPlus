Imports System.Reflection
Imports System.Threading
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew

Public Class ClaimIssueForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const NO_DATA As String = " - "
    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 5
    Public Const GRID_COL_PROCESS_DATE_IDX As Integer = 3
    Public Const GRID_COL_CREATE_DATE_IDX As Integer = 1
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimBase, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As ClaimBase
        Public IsGridVisible As Boolean = True
        Public SortExpression As String = ClaimBase.ClaimIssuesView.COL_CREATED_DATE & " DESC"
        Public PageIndex As Integer = 0
        Public SelectedClaimIssueId As Guid
        Public PageSize As Integer = 5
        Public ClaimIssuesView As ClaimBase.ClaimIssuesView
        Public InputParameters As Parameters


    End Class


    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        State.InputParameters = CType(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters IsNot Nothing Then
                State.MyBO = CType(State.InputParameters.ClaimBO, ClaimBase)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase

        Public Sub New(claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
      
        MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & MasterPage.PageTab
           
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUES")
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")

        UpdateBreadCrum()
        MasterPage.MessageController.Clear()

        lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUES")
        lblFileNewIssue.Text = TranslationBase.TranslateLabelOrMessage("FILE_NEW_CLAIM_ISSUE")
        If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED OrElse State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED) Then
            lblFileNewIssue.Visible = False
        End If

        Try
            If Not IsPostBack Then

                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                TranslateGridHeader(Grid)

                State.ClaimIssuesView = State.MyBO.GetClaimIssuesView()
                PopulateFormFromBO()
                PopulateGrid()

            End If

        Catch ex As ThreadAbortException
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
        With State.MyBO
            PopulateControlFromBOProperty(lblCustomerNameValue, .CustomerName)
            PopulateControlFromBOProperty(lblClaimNumberValue, .ClaimNumber)
            PopulateControlFromBOProperty(lblDealerNameValue, .DealerName)
            PopulateControlFromBOProperty(lblCertificateNumberValue, .CertificateNumber)
            PopulateControlFromBOProperty(lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            PopulateControlFromBOProperty(lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            PopulateControlFromBOProperty(lblSerialNumberImeiValue, .SerialNumber)
            PopulateControlFromBOProperty(lblWorkPhoneNumberValue, .MobileNumber)

            If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            ClaimStatusTD.Attributes.Item("Class") = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(State.MyBO.CertificateId)
        Dim oDealer As New Dealer(State.MyBO.CompanyId, State.MyBO.DealerCode)

        PopulateControlFromBOProperty(lblDealerGroupValue, oDealer.DealerGroupName)
        PopulateControlFromBOProperty(lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If
        SubStatusTD.Attributes.Item("Class") = SubStatusTD.Attributes.Item("Class") & " " & cssClassName

        BindListControlToDataView(ddlIssueCode, State.MyBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
        BindListControlToDataView(ddlIssueDescription, State.MyBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)
        txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
        txtCreatedDate.Text = GetLongDate12FormattedString(DateTime.Now)

        'If No issues to Add to claim hide the Save and Cancel Button
        If (State.MyBO.Load_Filtered_Issues().Count = 0) Then
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        NavController.Navigate(Me, FlowEvents.EVENT_CANCEL, New ClaimForm.Parameters(State.MyBO.Id))
    End Sub

    Private Sub SaveClaimIssue(sender As Object, Args As EventArgs) Handles btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(State.MyBO.ClaimIssuesList.GetNewChild, ClaimIssue)
            claimIssue.SaveNewIssue(State.MyBO.Id, New Guid(hdnSelectedIssueCode.Value), State.MyBO.CertificateId, False)
            Select Case claimIssue.StatusCode
                Case Codes.CLAIMISSUE_STATUS__OPEN, Codes.CLAIMISSUE_STATUS__PENDING
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    Dim c As Comment = State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
            End Select
            State.MyBO.Save()

            PopulateFormFromBO()
            State.ClaimIssuesView = State.MyBO.GetClaimIssuesView()
            PopulateGrid()
            MasterPage.MessageController.AddSuccess(TranslationBase.TranslateLabelOrMessage(Message.RECORD_ADDED_OK))
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub


#End Region

#Region "Grid related"

    Public Sub PopulateGrid()

        Grid.AutoGenerateColumns = False
        Grid.PageSize = State.PageSize
        ValidSearchResultCountNew(State.ClaimIssuesView.Count, True)
        HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
        SetPageAndSelectedIndexFromGuid(State.ClaimIssuesView, State.SelectedClaimIssueId, Grid, State.PageIndex)
        Grid.DataSource = State.ClaimIssuesView
        Grid.DataBind()
        If (State.ClaimIssuesView.Count > 0) Then
            State.IsGridVisible = True
            dvGridPager.Visible = True
        Else
            State.IsGridVisible = False
            dvGridPager.Visible = False
        End If
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        If Grid.Visible Then
            lblRecordCount.Text = State.ClaimIssuesView.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
        If (State.MyBO.HasIssues) Then
            Select Case State.MyBO.IssuesStatus()
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

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
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
            State.ClaimIssuesView.Sort = State.SortExpression
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedClaimIssueId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(1).FindControl("EditButton_WRITE") IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(ClaimBase.ClaimIssuesView.COL_CLAIM_ISSUE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(ClaimBase.ClaimIssuesView.COL_ISSUE_DESC).ToString
                End If

                Dim strCreationDate As String = Convert.ToString(e.Row.Cells(GRID_COL_CREATE_DATE_IDX).Text)
                strCreationDate = strCreationDate.Replace("&nbsp;", "")
                If String.IsNullOrWhiteSpace(strCreationDate) = False Then
                    Dim tempCreationDate = Convert.ToDateTime(e.Row.Cells(GRID_COL_CREATE_DATE_IDX).Text.Trim())
                    Dim formattedCreationDate = GetLongDate12FormattedString(tempCreationDate)
                    e.Row.Cells(GRID_COL_CREATE_DATE_IDX).Text = Convert.ToString(formattedCreationDate)
                End If

                Dim strProcessDate As String = Convert.ToString(e.Row.Cells(GRID_COL_PROCESS_DATE_IDX).Text)
                strProcessDate = strProcessDate.Replace("&nbsp;", "")
                If String.IsNullOrWhiteSpace(strProcessDate) = False Then
                    Dim tempProcessDate = Convert.ToDateTime(e.Row.Cells(GRID_COL_PROCESS_DATE_IDX).Text.Trim())
                    Dim formattedProcessDate = GetLongDate12FormattedString(tempProcessDate)
                    e.Row.Cells(GRID_COL_PROCESS_DATE_IDX).Text = Convert.ToString(formattedProcessDate)
                End If

                ' Convert short status codes to full description with css
                e.Row.Cells(GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(CLAIM_ISSUE_LIST, dvRow(ClaimBase.ClaimIssuesView.COL_STATUS_CODE).ToString)
                If (dvRow(ClaimBase.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__RESOLVED OrElse dvRow(ClaimBase.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                    e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.SelectedClaimIssueId = New Guid(e.CommandArgument.ToString())
                   NavController.Navigate(Me, FlowEvents.EVENT_NEXT, New ClaimIssueDetailForm.Parameters(State.MyBO, State.SelectedClaimIssueId))
                End If
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is TargetInvocationException) AndAlso _
           (TypeOf ex.InnerException Is ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.ClaimIssuesView.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class