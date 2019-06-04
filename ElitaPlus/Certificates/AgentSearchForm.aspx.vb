Imports System.Reflection
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Certificates
    Partial Public Class AgentSearchForm
        Inherits ElitaPlusSearchPage
        Implements IStateController
#Region "Constants"
        Public Const Pagetab As String = "AGENT"
        Private Const DefaultItem As Integer = 0
        Public Const SelectActionCommand As String = "SelectAction"
        Public Const SelectActionCommandCase As String = "SelectActionCase"
        Public Const SelectActionCommandCert As String = "SelectActionCert"
        Public Const SelectActionCancelCert As String = "SelectActionCancelReq"
        Public Const ActionCommandNewCaseCertificate As String = "NewCaseViewCertificate"
        Public Const ActionCommandNewCaseClaim As String = "NewCaseViewClaim"
        Public Const ActionCommandResumeCaseClaim As String = "ResumeCaseViewClaim"

        Public Const BackupStateSessionKey As String = "SESSION_KEY_BACKUP_STATE_AGENT_SEARCH_FORM"

        Public Const PurposeCancelCertRequest As String = "CERTCANCEL"
        Private Const OneSpace As String = " "
#End Region

#Region "Page State"
        Private _isReturningFromChild As Boolean = False


        Class MyState


            Public SearchDv As CaseBase.AgentSearchDV = Nothing
            Public SearchClick As Boolean = False
            Public CertificateBo As Certificate = Nothing

            Public SelectedId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False

            Public CompanyId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public SelectedCertificateStatus As String = String.Empty
            Public CaseNumber As String = String.Empty
            Public ClaimNumber As String = String.Empty
            Public CertificateNumber As String = String.Empty
            Public CustomerFirstName As String = String.Empty
            Public CustomerLastName As String = String.Empty
            Public PhoneNumber As String = String.Empty
            Public SerialNumber As String = String.Empty
            Public InvoiceNumber As String = String.Empty
            Public Email As String = String.Empty
            Public Zipcode As String = String.Empty
            Public TaxId As String = String.Empty
            Public ServiceLineNumber As String = String.Empty
            Public AccountNumber As String = String.Empty
            Public GlobalCustomerNumber As String = String.Empty

            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        'Protected Shadows ReadOnly Property State() As MyState
        '    Get
        '        Return CType(MyBase.State, MyState)
        '    End Get
        'End Property

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Dim retState As MyState
                'Return CType(MyBase.State, MyState)
                If NavController Is Nothing Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(BackupStateSessionKey), MyState)
                ElseIf NavController.State Is Nothing Then
                    NavController.State = New MyState
                ElseIf (Me.GetType.BaseType.FullName <> NavController.State.GetType.ReflectedType.FullName) Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(BackupStateSessionKey), MyState)
                Else
                    If NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(NavController.State, MyState)
                        StartNavControl()
                        NavController.State = s
                    End If
                End If
                retState = CType(NavController.State, MyState)
                Session(BackupStateSessionKey) = retState
                Return retState
            End Get
        End Property

#End Region

#Region "Page Return Type"

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CaseBase
            Public BoChanged As Boolean = False
            Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As CaseBase, Optional ByVal boChanged As Boolean = False)
                LastOperation = lastOp
                EditingBo = curEditingBo
                boChanged = boChanged
            End Sub
        End Class

#End Region


#Region "Page event handlers"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then

                    MasterPage.MessageController.Clear()

                    ' Populate the header and bredcrumb
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Agent")
                    UpdateBreadCrum()
                    PopulateSearchControls()

                    If Not _isReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        If Authentication.CurrentUser.IsDealer Then
                            State.DealerId = Authentication.CurrentUser.ScDealerId
                            ControlMgr.SetEnableControl(Me, ddlCompany, False)
                            ControlMgr.SetEnableControl(Me, ddlDealer, False)
                        End If
                    End If

                    GetStateProperties()

                    If _isReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                    End If

                    SetFocus(ddlCompany)

                End If
                DisplayNewProgressBarOnClick(btnSearch, "Loading_Agent")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub


        Private Sub UpdateBreadCrum()
            If (Not State Is Nothing) Then
                If (Not State Is Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("AGENT_SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("AGENT_SEARCH")
                End If
            End If
        End Sub


        Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                _isReturningFromChild = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                SetStateProperties()
                State.SelectedId = Guid.Empty
                State.IsGridVisible = True
                State.SearchClick = True
                State.SearchDv = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                ClearAllSearchOptions()

                ' Update the Bo state properties with the new value
                ClearStateValues()

                SetStateProperties()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ClearStateValues()
            Try
                'clear State
                State.CompanyId = Nothing
                State.DealerId = Nothing
                State.SelectedCertificateStatus = String.Empty
                State.CaseNumber = String.Empty
                State.CertificateNumber = String.Empty
                State.ClaimNumber = String.Empty
                State.CustomerFirstName = String.Empty
                State.CustomerLastName = String.Empty
                State.PhoneNumber = String.Empty
                State.InvoiceNumber = String.Empty
                State.SerialNumber = String.Empty
                State.Email = String.Empty
                State.TaxId = String.Empty
                State.ServiceLineNumber = String.Empty
                State.AccountNumber = String.Empty
                State.GlobalCustomerNumber = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ClearAllSearchOptions()

            If Authentication.CurrentUser.IsDealer Then
                If State.CompanyId <> Guid.Empty And ddlCompany.Items.Count > 0 Then SetSelectedItem(ddlCompany, State.CompanyId)
                If State.DealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.DealerId)
            Else
                ddlCompany.SelectedIndex = DefaultItem
                ddlDealer.SelectedIndex = 0
            End If

            ddlPolicyStatus.SelectedIndex = -1
            TextBoxCaseText.Text = String.Empty
            TextBoxCertificateNumber.Text = String.Empty
            TextBoxClaimNumber.Text = String.Empty
            TextBoxCustomerFirstName.Text = String.Empty
            TextBoxCustomerLastName.Text = String.Empty
            TextBoxPhoneNumber.Text = String.Empty
            TextBoxSerialNumber.Text = String.Empty
            TextBoxInvoiceNumber.Text = String.Empty
            TextBoxEmail.Text = String.Empty
            TextBoxZip.Text = String.Empty
            txtTaxId.Text = String.Empty
            txtServiceLineNumber.Text = String.Empty
            txtAccountNumber.Text = String.Empty
            txtGlobalCustomerNumber.Text = String.Empty
            lblRecordCount.Text = String.Empty
            rep.DataSource = Nothing
            rep.DataBind()

        End Sub



#End Region

#Region "Helper functions"
        Private Sub PopulateSearchControls()
            Try
                PopulateCertificateStatusDropDown()
                'populate dealer list
                PopulateDealerDropdown(ddlDealer, State.DealerId)
                'populate company list
                PopulateCompanyDropdown(ddlCompany, State.CompanyId)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Sub PopulateCertificateStatusDropDown()
            Try
                ddlPolicyStatus.SelectedValue = State.SelectedCertificateStatus
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub PopulateDealerDropdown(ByVal dealerDropDownList As DropDownList, ByVal setvalue As Guid)
            Try
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.Translation + OneSpace + "(" + li.Code + ")"
                                                                               End Function
                dealerDropDownList.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc
                                                   })


                If setvalue <> Guid.Empty Then
                    SetSelectedItem(dealerDropDownList, setvalue)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetDealerListByCompanyForUser() As DataElements.ListItem()
            Dim index As Integer
            Dim oListContext As New ListContext

            Dim userCompanies As ArrayList = Authentication.CurrentUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of DataElements.ListItem)

            For index = 0 To userCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = userCompanies(index)
                Dim oDealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Private Sub PopulateCompanyDropdown(ByVal companyDropDownList As DropDownList, ByVal setvalue As Guid)
            Try
                Dim companyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

                Dim filteredCompanyList As DataElements.ListItem() = (From x In companyList
                                                                      Where Authentication.CurrentUser.Companies.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Dim companyTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                    Return li.Translation + OneSpace + "(" + li.Code + ")"
                                                                                End Function

                companyDropDownList.Populate(filteredCompanyList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False,
                                                    .TextFunc = companyTextFunc
                                                   })

                If setvalue <> Guid.Empty Then
                    SetSelectedItem(companyDropDownList, setvalue)
                Else
                    companyDropDownList.SelectedIndex = DefaultItem
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub GetStateProperties()
            Try
                If State.CompanyId <> Guid.Empty And ddlCompany.Items.Count > 0 Then SetSelectedItem(ddlCompany, State.CompanyId)
                If State.DealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.DealerId)
                ddlPolicyStatus.SelectedValue = State.SelectedCertificateStatus
                TextBoxCaseText.Text = State.CaseNumber
                TextBoxCertificateNumber.Text = State.CertificateNumber
                TextBoxClaimNumber.Text = State.ClaimNumber
                TextBoxCustomerFirstName.Text = State.CustomerFirstName
                TextBoxCustomerLastName.Text = State.CustomerLastName
                TextBoxPhoneNumber.Text = State.PhoneNumber
                TextBoxSerialNumber.Text = State.SerialNumber
                TextBoxInvoiceNumber.Text = State.InvoiceNumber
                TextBoxEmail.Text = State.Email
                TextBoxZip.Text = State.Zipcode
                txtTaxId.Text = State.TaxId
                txtServiceLineNumber.Text = State.ServiceLineNumber
                txtAccountNumber.Text = State.AccountNumber
                txtGlobalCustomerNumber.Text = State.GlobalCustomerNumber
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()

            Try
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                ClearStateValues()

                State.DealerId = GetSelectedItem(ddlDealer)
                State.CompanyId = GetSelectedItem(ddlCompany)
                State.CertificateNumber = TextBoxCertificateNumber.Text.ToUpper.Trim
                State.SelectedCertificateStatus = ddlPolicyStatus.SelectedValue.Trim
                State.CustomerFirstName = TextBoxCustomerFirstName.Text.ToUpper.Trim
                State.CustomerLastName = TextBoxCustomerLastName.Text.ToUpper.Trim
                State.Zipcode = TextBoxZip.Text.ToUpper.Trim
                State.PhoneNumber = TextBoxPhoneNumber.Text.ToUpper.Trim
                State.InvoiceNumber = TextBoxInvoiceNumber.Text.ToUpper.Trim
                State.ClaimNumber = TextBoxClaimNumber.Text.ToUpper.Trim
                State.CaseNumber = TextBoxCaseText.Text.ToUpper.Trim
                State.Email = TextBoxEmail.Text.ToUpper.Trim
                State.SerialNumber = TextBoxSerialNumber.Text.ToUpper.Trim
                State.TaxId = txtTaxId.Text.ToUpper.Trim
                State.ServiceLineNumber = txtServiceLineNumber.Text.ToUpper.Trim
                State.AccountNumber = txtAccountNumber.Text.ToUpper.Trim
                State.GlobalCustomerNumber = txtGlobalCustomerNumber.Text.ToUpper.Trim

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub



        Private Sub PopulateGrid()
            Try
                If (State.SearchDv Is Nothing) Then
                    State.SearchDv = CaseBase.getAgentList(State.CompanyId,
                                                              State.DealerId,
                                                              State.CustomerFirstName,
                                                              State.CustomerLastName,
                                                              State.CaseNumber,
                                                              State.ClaimNumber,
                                                              State.CertificateNumber,
                                                              State.SerialNumber,
                                                              State.InvoiceNumber,
                                                              State.PhoneNumber,
                                                              State.Zipcode,
                                                              State.SelectedCertificateStatus,
                                                              State.Email,
                                                              State.TaxId,
                                                              State.ServiceLineNumber,
                                                              State.AccountNumber,
                                                              State.GlobalCustomerNumber,
                                                              Authentication.CurrentUser.LanguageId)

                    If State.SearchClick Then
                        ValidSearchResultCountNew(State.SearchDv.Count, True)
                        State.SearchClick = False
                    End If
                End If

                rep.DataSource = State.SearchDv
                rep.DataBind()

                ControlMgr.SetVisibleControl(Me, rep, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, rep.Visible)
                Session("recCount") = State.SearchDv.Count

                If rep.Visible Then
                    lblRecordCount.Text = State.SearchDv.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Dim exceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not exceptionType.Equals(String.Empty)) And exceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, rep, False)
                    lblRecordCount.Text = ""
                End If
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region



#Region "rep related"
        Protected Sub rep_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim isRestricted As String = String.Empty
                Dim lblStatus As Label = DirectCast(e.Item.FindControl("lblStatus"), Label)
                Dim statusCode As Object = DataBinder.Eval(e.Item.DataItem, "Status_Code")
                Dim itemtype As Object = DataBinder.Eval(e.Item.DataItem, "itemtype")
                
                Dim btnCancelReqAgent As LinkButton = DirectCast(e.Item.FindControl("btnCancelReqAgent"), LinkButton)
                Dim cId As Object = DataBinder.Eval(e.Item.DataItem, "c_id")
                Dim certId As Object = DataBinder.Eval(e.Item.DataItem, "cert_id")
                Dim btnEditCase As LinkButton = DirectCast(e.Item.FindControl("btnEditCase"), LinkButton)
                Dim btnEditCert As LinkButton = DirectCast(e.Item.FindControl("btnEditCert"), LinkButton)

                Dim btnNewCase As LinkButton = DirectCast(e.Item.FindControl("LinkButtonNewCase"), LinkButton)
                btnNewCase.Text = TranslationBase.TranslateLabelOrMessage("DCM_NEW_CASE")

                Dim btnEditAgent As LinkButton = DirectCast(e.Item.FindControl("btnEditAgent"), LinkButton)
                btnEditAgent.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                btnEditAgent.CommandName = SelectActionCommand

                If DataBinder.Eval(e.Item.DataItem, "is_restricted") IsNot DBNull.Value Then
                    isRestricted = DataBinder.Eval(e.Item.DataItem, "is_restricted")
                End If
                If (itemtype.ToString = "cert") Then
                    Dim certAcceptedRequest As Integer = 0
                    lblStatus.Text = LookupListNew.GetDescriptionFromCode("CSTAT", statusCode.ToString, Authentication.CurrentUser.LanguageId)
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_NEW_CLAIM")

                    If isRestricted = Codes.ENTITY_IS__RESTRICTED Then
                        btnEditAgent.Visible = False
                    Else
                        btnEditAgent.Visible = True
                    End If
                    'Display the Cancel Request button only if the Cancel Policy Question Set Code attrbute is configured in the Product Code
                    State.CertificateBo = New Certificate(New Guid(CType(certId, Byte())))
                    Dim cancelQuestionSetCode As String
                    cancelQuestionSetCode = CaseBase.GetQuestionSetCode(Guid.Empty, Guid.Empty, Guid.Empty, State.CertificateBo.Product.Id, Guid.Empty,
                                                                                          Guid.Empty, Guid.Empty, Guid.Empty, PurposeCancelCertRequest)


                    If (Not State.CertificateBo Is Nothing AndAlso State.CertificateBo.StatusCode <> Codes.CERTIFICATE_STATUS__CANCELLED _
                            AndAlso Not String.IsNullOrEmpty(cancelQuestionSetCode)) Then
                        ' check for any existing Accepted Cancel Requests                        
                        Dim dsRequests As DataSet = CertCancelRequest.GetCertCancelRequestData(State.CertificateBo.Id)
                        If (Not dsRequests Is Nothing AndAlso dsRequests.Tables.Count > 0 AndAlso dsRequests.Tables(0).Rows.Count > 0) Then
                            If (dsRequests.Tables(0).Select("status_description = 'Accepted'").Count > 0) Then
                                certAcceptedRequest += 1
                            End If
                        End If
                        'Display the Cancel link when the Certificate is active andalso no "Accepted" cancellation request 
                        If (certAcceptedRequest = 0) Then
                            'andalso not String.IsNullOrWhiteSpace(strQuestionSetCode)) Then

                            btnCancelReqAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_CERT_CANCEL")
                            btnCancelReqAgent.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                            btnCancelReqAgent.CommandName = SelectActionCancelCert

                        End If
                    End If
                    btnEditCert.Visible = True
                    btnEditCert.Text = TranslationBase.TranslateLabelOrMessage("VIEW")
                    btnEditCert.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnEditCert.CommandName = SelectActionCommandCert

                    btnNewCase.Visible = True
                    btnNewCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnNewCase.CommandName = ActionCommandNewCaseCertificate

                ElseIf (itemtype.ToString = "claim") Then
                    lblStatus.Text = LookupListNew.GetDescriptionFromCode("CLSTAT", statusCode.ToString, Authentication.CurrentUser.LanguageId)
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_VIEW_CLAIM")

                    btnNewCase.Visible = True
                    btnNewCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnNewCase.CommandName = ActionCommandNewCaseClaim
                ElseIf (itemtype.ToString = "case") Then
                    'lblStatus.Text = LookupListNew.GetDescriptionFromCode("CASSTAT", Status_Code.ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    lblStatus.Text = statusCode.ToString
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_RESUME_CLAIM")
                    If DataBinder.Eval(e.Item.DataItem, "summary1").ToString.Contains("Inquiry") Then
                        btnEditAgent.CommandName = ActionCommandResumeCaseClaim
                    End If

                    btnEditCase.Visible = True
                    btnEditCase.Text = TranslationBase.TranslateLabelOrMessage("DCM_VIEW_CLAIM")
                    btnEditCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnEditCase.CommandName = SelectActionCommandCase
                End If

                If (statusCode.ToString = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                    lblStatus.CssClass = "StatActive"
                Else
                    lblStatus.CssClass = "StatInactive"
                End If

                

            End If
        End Sub
        Protected Sub rep_OnItemCommand(sender As Object, e As RepeaterCommandEventArgs)
            Try
                If e.CommandName = SelectActionCommand Or e.CommandName = SelectActionCancelCert Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        Dim itemtype As HiddenField = DirectCast(e.Item.FindControl("hfItemType"), HiddenField)
                        If Not itemtype.Value.ToString().Equals(String.Empty) Then
                            If (itemtype.Value.ToString = "cert") Then
                                callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(State.SelectedId, Nothing, Nothing,
                                                                                                   If(e.CommandName = SelectActionCommand, Codes.CASE_PURPOSE__REPORT_CLAIM, Codes.CASE_PURPOSE__CANCELLATION_REQUEST)))
                            ElseIf (itemtype.Value.ToString = "claim") Then
                                Dim claimId As Guid
                                claimId = State.SelectedId
                                Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
                                If claimBo.Status = BasicClaimStatus.Pending Then
                                    If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                                        NavController = Nothing
                                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, claimId, Nothing))
                                    Else
                                        NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                                        NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                                    End If
                                Else
                                    callPage(ClaimForm.URL, State.SelectedId)
                                End If
                            ElseIf (itemtype.Value.ToString = "case") Then
                                callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(Nothing, Nothing, State.SelectedId))
                            End If
                        End If

                    End If
                ElseIf e.CommandName = SelectActionCommandCase Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        callPage(CaseDetailsForm.URL, State.SelectedId)
                    End If
                ElseIf e.CommandName = ActionCommandNewCaseCertificate Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(State.SelectedId, CaseRecordingForm.CasePurpose.CertificateInquiry))
                    End If
                ElseIf e.CommandName = ActionCommandNewCaseClaim Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(State.SelectedId, CaseRecordingForm.CasePurpose.ClaimInquiry))
                    End If
                ElseIf e.CommandName = ActionCommandResumeCaseClaim Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(State.SelectedId, CaseRecordingForm.CasePurpose.CaseInquiry))
                    End If
                ElseIf e.CommandName = SelectActionCommandCert Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.SelectedId = New Guid(e.CommandArgument.ToString())
                        callPage(CertificateForm.URL, State.SelectedId)
                    End If
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClassicView_Click(sender As Object, e As EventArgs) Handles btnClassicView.Click
            Redirect(CaseSearchForm.URL)
        End Sub

#End Region

#Region "Navigation Handling"
        Public Sub Process(ByVal callingPage As Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            Try
                If Not IsPostBack AndAlso navCtrl.CurrentFlow.Name = FlowName AndAlso
                            Not navCtrl.PrevNavState Is Nothing Then
                    _isReturningFromChild = True
                    If navCtrl.IsFlowEnded Then
                        State.SearchDv = Nothing 'This will force a reload
                        'restart the flow
                        Dim savedState As MyState = CType(navCtrl.State, MyState)
                        StartNavControl()
                        NavController.State = savedState
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "State Controller"

        Private Const FlowName As String = "AUTHORIZE_AGENT_PENDING_CLAIM"
        Private Sub StartNavControl()
            Dim nav As New ElitaPlusNavigation
            Me.NavController = New NavControllerBase(nav.Flow(FlowName))
            Me.NavController.State = New MyState
        End Sub

        Function IsFlowStarted() As Boolean
            Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FlowName
        End Function
#End Region

    End Class
End Namespace