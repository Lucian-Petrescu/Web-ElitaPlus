Public Class ClaimDetailsForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const ClaimCaseGridColCaseIdIdx As Integer = 0
    Public Const GridColCaseOpenDateIdx As Integer = 2
    Public Const GridColCaseStatusCodeIdx As Integer = 3
    Public Const GridColCaseCloseDateIdx As Integer = 4
    Public Const GridColCreatedDateIdx As Integer = 4

    Public Const ClaimCaseGridColCaseNumberCtrl As String = "btnSelectCase"
    Public Const SelectActionCommand As String = "SelectAction"
    Public Const NoData As String = " - "
    Public Const Url As String = "~/DCM/ClaimDetailsForm.aspx"
    Private Const OneSpace As String = " "
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CaseBase
        Public BoChanged As Boolean = False
        Public IsCallerAuthenticated As Boolean = False
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As CaseBase, Optional ByVal boChanged As Boolean = False,Optional Byval IsCallerAuthenticated As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
        Public Sub New(ByVal lastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public ClaimBo As ClaimBase
        Public SortExpression As String = "created_date desc"
        Public IsGridVisible As Boolean = True
        Public ClaimCaseListDv As CaseBase.CaseSearchDV = Nothing
        Public ClaimActionListDv As CaseAction.CaseActionDV = Nothing
        Public SelectedCaseId As Guid = Guid.Empty
        Public IsCallerAuthenticated As Boolean = False
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

#Region "Page Events"
    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try

            If Not CallingParameters Is Nothing Then

                'If CallingPar(0).GetType Is GetType(Claim) Then
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(CType(CallingParameters, ArrayList)(0), ClaimBase).Id)
                'End if
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_DETAILS")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")

            UpdateBreadCrum()
            MasterPage.MessageController.Clear()

            If Not IsPostBack Then
                TranslateGridHeader(ClaimCaseListGrid)
                TranslateGridHeader(ClaimActionGrid)
                PopulateFormFromBO()
                PopulateClaimCaseListGrid()
                PopulateClaimActionGrid()

            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
    Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            Dim retObj As CaseDetailsForm.ReturnType = CType(ReturnPar, CaseDetailsForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                State.ClaimCaseListDV = Nothing
                State.ClaimActionListDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.selectedCaseId = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True
                    End If
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"
    Private Sub UpdateBreadCrum()
        If (Not State Is Nothing) Then
            MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & MasterPage.PageTab
        End If
    End Sub
    Private Sub PopulateFormFromBo()
        Dim cssClassName As String
        Dim langId As Guid = Authentication.CurrentUser.LanguageId
        With State.ClaimBO
            PopulateControlFromBOProperty(lblCustomerNameValue, .Certificate.CustomerName)
            PopulateControlFromBOProperty(lblClaimNumberValue, .ClaimNumber)
            PopulateControlFromBOProperty(lblDealerNameValue, .Dealer.DealerName)
            PopulateControlFromBOProperty(lblCertificateNumberValue, .Certificate.CertNumber)
            PopulateControlFromBOProperty(lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            PopulateControlFromBOProperty(lblDateOfLossValue, GetDateFormattedString(.LossDate.Value))
            PopulateControlFromBOProperty(lblSerialNumberImeiValue, .SerialNumber)
            PopulateControlFromBOProperty(lblWorkPhoneNumberValue, .MobileNumber)

            If (State.ClaimBO.Status = BasicClaimStatus.Active) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            ClaimStatusTD.Attributes.Item("Class") = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(State.ClaimBO.Certificate.Id)
        Dim oDealer As New Dealer(State.ClaimBO.CompanyId, State.ClaimBO.Dealer.Dealer)

        PopulateControlFromBOProperty(lblDealerGroupValue, oDealer.DealerGroupName)
        PopulateControlFromBOProperty(lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If
        SubStatusTD.Attributes.Item("Class") = SubStatusTD.Attributes.Item("Class") & " " & cssClassName

    End Sub

    Protected Sub EnableDisableFields()
        btnBack.Enabled = True
        EnableDisableControls(ClaimCaseListTabPanel, True)
        EnableDisableControls(ClaimActionTabPanel, True)
    End Sub
#End Region

#Region "Grid Related Functions"

    Public Sub PopulateClaimActionGrid()

        Try
            If (State.ClaimActionListDV Is Nothing) Then
                State.ClaimActionListDV = CaseAction.GetClaimActionList(State.ClaimBO.Id, Authentication.CurrentUser.LanguageId)
            End If

            If State.ClaimActionListDV.Count = 0 Then
                lblClaimActionRecordFound.Text = State.ClaimActionListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                ClaimActionGrid.DataSource = State.ClaimActionListDV
                State.ClaimActionListDV.Sort = State.SortExpression
                HighLightSortColumn(ClaimActionGrid, State.SortExpression, IsNewUI)
                ClaimActionGrid.DataBind()
                lblClaimActionRecordFound.Text = State.ClaimActionListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, ClaimActionGrid, True)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ClaimActionGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ClaimActionGrid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (String.IsNullOrWhiteSpace(dvRow(CaseAction.CaseActionDV.ColActionCreatedDate).ToString) = False) Then
                    e.Row.Cells(GridColCreatedDateIdx).Text = GetLongDate12FormattedString(dvRow(CaseAction.CaseActionDV.ColActionCreatedDate))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub PopulateClaimCaseListGrid()

        Try
            If (State.ClaimCaseListDV Is Nothing) Then
                State.ClaimCaseListDV = CaseBase.getClaimCaseList(State.ClaimBO.Id, Authentication.CurrentUser.LanguageId)
            End If

            If State.ClaimCaseListDV.Count = 0 Then
                lblClaimCaseRecordFound.Text = State.ClaimCaseListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                ClaimCaseListGrid.DataSource = State.ClaimCaseListDV
                State.ClaimCaseListDV.Sort = State.SortExpression
                HighLightSortColumn(ClaimCaseListGrid, State.SortExpression, IsNewUI)
                ClaimCaseListGrid.DataBind()
                lblClaimCaseRecordFound.Text = State.ClaimCaseListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

            ControlMgr.SetVisibleControl(Me, ClaimCaseListGrid, True)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ClaimCaseListGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ClaimCaseListGrid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnSelectCase As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(ClaimCaseGridColCaseIdIdx).FindControl(ClaimCaseGridColCaseNumberCtrl) Is Nothing) Then
                    btnSelectCase = CType(e.Row.Cells(ClaimCaseGridColCaseIdIdx).FindControl(ClaimCaseGridColCaseNumberCtrl), LinkButton)
                    btnSelectCase.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CaseBase.CaseSearchDV.ColCaseId), Byte()))
                    btnSelectCase.CommandName = SelectActionCommand
                    btnSelectCase.Text = dvRow(CaseBase.CaseSearchDV.ColCaseNumber).ToString
                End If
                If (String.IsNullOrWhiteSpace(dvRow(CaseBase.CaseSearchDv.ColCaseOpenDate).ToString) = False) Then
                    e.Row.Cells(GridColCaseOpenDateIdx).Text = GetLongDate12FormattedString(dvRow(CaseBase.CaseSearchDv.ColCaseOpenDate))
                End If
                If (dvRow(CaseBase.CaseSearchDv.ColCaseStatusCode).ToString = Codes.CASE_STATUS__OPEN) Then
                    e.Row.Cells(GridColCaseStatusCodeIdx).CssClass = "StatActive"
                Else
                    e.Row.Cells(GridColCaseStatusCodeIdx).CssClass = "StatInactive"
                End If
                If (String.IsNullOrWhiteSpace(dvRow(CaseBase.CaseSearchDv.ColCaseCloseDate).ToString) = False) Then
                    e.Row.Cells(GridColCaseCloseDateIdx).Text = GetLongDate12FormattedString(dvRow(CaseBase.CaseSearchDv.ColCaseCloseDate))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ClaimCaseListGrid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ClaimCaseListGrid.RowCommand
        Try
            If e.CommandName = SelectActionCommand Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.selectedCaseId = New Guid(e.CommandArgument.ToString())
                    callPage(CaseDetailsForm.URL, State.selectedCaseId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Click"
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            Back(DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As DetailPageCommand)
        Dim retType As New ClaimForm.ReturnType(cmd, State.ClaimBO)
        ReturnToCallingPage(retType)
    End Sub
#End Region



End Class
