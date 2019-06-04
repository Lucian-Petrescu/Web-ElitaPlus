Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.ServiceModel
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Certificates
Imports Assurant.ElitaPlus.ElitaPlusWebApp.CaseManagementWebAppGatewayService
Imports System.ComponentModel
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security

Public Class CaseRecordingForm
    Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "Constants"
    Public Const Url As String = "~/DCM/CaseRecordingForm.aspx"

    Private Const Pagetitle As String = "CASE_RECORDING"
    Private Const CaseRecordingViewIndexCaller = 0
    Private Const CaseRecordingViewIndexQuestion = 1
    Private Const CaseRecordingViewIndexCaseInteraction = 2
#End Region
#Region "Enums"
    Public Enum CasePurpose
        <Description("Certificate inquiry")>
        CertificateInquiry = 1
        <Description("Claim inquiry")>
        ClaimInquiry = 2
        <Description("Case inquiry")>
        CaseInquiry = 3
    End Enum

#End Region
#Region "Page State"
    Class MyState
        Public CasePurpose As CasePurpose
        Public ClaimBo As ClaimBase
        Public InputParameters As Parameters
        Public CertificateId As Guid = Guid.Empty
        Public DealerId As Guid = Guid.Empty
        Public SubmitWsBaseCaseResponse As BaseCaseResponse = Nothing
        Public CaseBo As CaseBase
        Public CertificateBo As Certificate
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
#Region "Parameters"
    Public Class Parameters

        Public EntityId As Guid = Nothing
        Public CasePurpose As CasePurpose

        Public Sub New(ByVal entityId As Guid, ByVal casePurpose As CasePurpose)
            Me.EntityId = entityId
            Me.CasePurpose = casePurpose
        End Sub
    End Class
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public CertificateId As Guid
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal certId As Guid)
            LastOperation = lastOp
            CertificateId = certId
        End Sub
        Public Sub New(ByVal lastOp As DetailPageCommand)
            LastOperation = lastOp
        End Sub
    End Class
#End Region
#Region "Properties"
    Private _isReturningFromChild As Boolean = False
#End Region
#Region "Page Events"
    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try
            If Not CallingParameters Is Nothing Then
                State.InputParameters = CType(CallingParameters, Parameters)
                State.CasePurpose = State.InputParameters.CasePurpose
                Select Case State.CasePurpose
                    Case CasePurpose.CertificateInquiry
                        State.CertificateId = State.InputParameters.EntityId
                    Case CasePurpose.ClaimInquiry
                        State.ClaimBo = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.InputParameters.EntityId)
                    Case CasePurpose.CaseInquiry
                        State.CaseBO = New CaseBase(State.InputParameters.EntityId)
                End Select
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Page_PageReturn(returnFromUrl As String, returnPar As Object) Handles Me.PageReturn
        Try
            _isReturningFromChild = True
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not (IsPostBack) Then
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                InitialSetUp()

                If _isReturningFromChild Then
                    ShowCaseInteractionView()
                Else
                    ShowCallerView()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Other Functions"
    Private Sub InitialSetUp()
        Dim oCertificate As Certificate
        Dim entityNumber As String = String.Empty

        Select Case State.CasePurpose
            Case CasePurpose.CertificateInquiry
                oCertificate = New Certificate(State.CertificateId)
                entityNumber = oCertificate.CertNumber
            Case CasePurpose.ClaimInquiry
                State.CertificateId = State.ClaimBo.Certificate.Id
                oCertificate = State.ClaimBo.Certificate
                entityNumber = State.ClaimBo.ClaimNumber
            Case CasePurpose.CaseInquiry
                State.CertificateId = State.CaseBO.CertId
                oCertificate = New Certificate(State.CertificateId)
                state.CertificateBo = oCertificate
                entityNumber = State.CaseBO.CaseNumber
                LoadCaseInformationInHeader(entityNumber, oCertificate.Company.Code)
        End Select
        
        'Set the value needed in next steps
        State.DealerId = oCertificate.Dealer.Id

        UpdateBreadCrum(entityNumber)
        CaseHeaderInformation(oCertificate)
    End Sub
    Private Sub UpdateBreadCrum(ByVal entityNumber As String)
        MasterPage.UsePageTabTitleInBreadCrum = False
        Select Case State.CasePurpose
            Case CasePurpose.CertificateInquiry
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(Pagetitle) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("CERTIFICATE") + " " + entityNumber
            Case CasePurpose.ClaimInquiry
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(Pagetitle) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("CLAIM") + " " + entityNumber
            Case CasePurpose.CaseInquiry
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(Pagetitle) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage("CASE") + " " + entityNumber
        End Select
    End Sub
    Private Sub CaseHeaderInformation(ByVal oCert As Certificate)
        ucCaseHeaderInformation.CustomerName = oCert.CustomerName
        ucCaseHeaderInformation.CompanyName = oCert.Company.Code
        ucCaseHeaderInformation.DealerName = oCert.Dealer.Dealer
        ucCaseHeaderInformation.CertificateNumber = oCert.CertNumber
        If State.ClaimBo IsNot Nothing Then
            ucCaseHeaderInformation.ClaimNumber = State.ClaimBo.ClaimNumber
        End If
        ucCaseHeaderInformation.CasePurpose = CommonConfigManager.Current.ListManager.GetList("CASEPUR", Authentication.CurrentUser.LanguageCode).FirstOrDefault(Function(p) p.Code = "INQUIRY").Translation
    End Sub
    Private Sub LoadCaseInformationInHeader(ByVal caseNumber As String, ByVal companyCode As String)
        Dim oCase As CaseBase = New CaseBase(caseNumber, companyCode)
        ucCaseHeaderInformation.CaseNumber = oCase.CaseNumber
        ucCaseHeaderInformation.CaseOpenDate = GetDateFormattedStringNullable(oCase.CaseOpenDate)
        ucCaseHeaderInformation.CaseStatus = CommonConfigManager.Current.ListManager.GetList("CASESTAT", Authentication.CurrentUser.LanguageCode).FirstOrDefault(Function(p) p.Code = oCase.CaseStatusCode).Translation

        ucCaseHeaderInformation.SetCaseStatusCssClass = If(oCase.CaseStatusCode = Codes.CASE_STATUS__OPEN, "StatActive", "StatClosed")
        ucCaseHeaderInformation.CallerName = oCase.InitialCallerName
    End Sub

    Private Sub LoadCaseInformationInHeader(ByVal oCase As CaseBase)
        ucCaseHeaderInformation.CaseNumber = oCase.CaseNumber
        ucCaseHeaderInformation.CaseOpenDate = GetDateFormattedStringNullable(oCase.CaseOpenDate)
        ucCaseHeaderInformation.CaseStatus = CommonConfigManager.Current.ListManager.GetList("CASESTAT", Authentication.CurrentUser.LanguageCode).FirstOrDefault(Function(p) p.Code = oCase.CaseStatusCode).Translation

        ucCaseHeaderInformation.SetCaseStatusCssClass = If(oCase.CaseStatusCode = Codes.CASE_STATUS__OPEN, "StatActive", "StatClosed")
        ucCaseHeaderInformation.CallerName = oCase.InitialCallerName
    End Sub
    ''' <summary>
    ''' Gets New Instance of Case Management WebAppGateway Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="WebAppGatewayClient"/></returns>
    Private Shared Function GetCaseWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CASE_MANAGEMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_CaseManagementWebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Sub DisplayNextView()
        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            Select Case State.SubmitWsBaseCaseResponse.GetType()
                Case GetType(AuthenticationCaseResponse)
                    ' Authentication case response object
                    ShowCallerAuthenticationView()
                Case GetType(AuthenticationResponse)
                    ' Authentication response object
                    AuthenticationAction()
                Case Else
                    ReturnBackToCallingPage()
            End Select
        End If
    End Sub
    Private Sub MoveToNextPage()
        Try
            If State.CasePurpose = CasePurpose.CertificateInquiry Then
                callPage(CertificateForm.URL, State.CertificateId)
            ElseIf State.CasePurpose = CasePurpose.ClaimInquiry Then
                'redirect to new page with claims information
                If State.ClaimBo.Status = BasicClaimStatus.Pending Then
                    If State.ClaimBo.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                        NavController = Nothing
                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, State.ClaimBo.Id, Nothing))
                    Else
                        NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.ClaimBo
                        NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                    End If
                Else
                    ' for others
                    callPage(ClaimForm.URL, State.ClaimBo.Id)
                End If
            ElseIf State.CasePurpose = CasePurpose.CaseInquiry Then
                ShowCaseInteractionView()
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ReturnBackToCallingPage()
        ReturnToCallingPage()
    End Sub
    Private Sub ThrowWsFaultExceptions(fex As FaultException)
        Log(fex)
        MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CASE_MANAGEMENT_WEBAPPGATEWAY_SERVICE_ERR) & " - " & fex.Message, False)
    End Sub
#End Region
#Region "Button Clicks"
    Protected Sub button_Cancel_Yes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelYes.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub button_Continue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button_Continue.Click
        Try
            Select Case mvClaimsRecording.ActiveViewIndex
                Case CaseRecordingViewIndexCaller
                    If State.CasePurpose = CasePurpose.CaseInquiry Then
                        ResumeCase()
                    else
                        ContinueCaller()
                    End If
                    
                Case CaseRecordingViewIndexQuestion
                    ContinueQuestion()
                Case CaseRecordingViewIndexCaseInteraction
                    ContinueCaseInteraction()
            End Select
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Caller View"
    Private Sub ShowCallerView()
        LabelPurposeValue.Text = CommonConfigManager.Current.ListManager.GetList("CASEPUR", Authentication.CurrentUser.LanguageCode).FirstOrDefault(Function(p) p.Code = "INQUIRY").Translation
        ucCallerInfo.PopulateGridViewCaller(State.CertificateId)
    End Sub

        Private Sub ResumeCase()

        Dim errMsg As List(Of String) = New List(Of String)
        Dim wsRequest As BeginInteractionRequest = New BeginInteractionRequest()

        wsRequest.PurposeCode = Codes.CASE_PURPOSE__INQUIRY
            wsRequest.CompanyCode = State.CertificateBo.Company.Code
            wsRequest.CaseNumber = State.CaseBo.CaseNumber
        

        If String.IsNullOrEmpty(wsRequest.PurposeCode) Then
            errMsg.Add(lblPurpose.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CASE_PURPOSE_REQUIRED_ERR))
        End If

        Dim callerinfo As New PhoneCaller()
        ucCallerInfo.GetCallerInformation()

        callerinfo.FirstName = ucCallerInfo.FirstName
        callerinfo.LastName = ucCallerInfo.LastName
        callerinfo.PhoneNumber = ucCallerInfo.WorkPhoneNumber
        callerinfo.EmailAddress = ucCallerInfo.Email
        callerinfo.RelationshipTypeCode = ucCallerInfo.RelationshipCode
        callerinfo.CultureCode = Thread.CurrentThread.CurrentCulture.ToString().ToUpper()
        callerinfo.ChannelCode = "CSR"

        If (callerinfo.GetType() Is GetType(PhoneCaller)) Then
            If (String.IsNullOrEmpty(callerinfo.PhoneNumber) And String.IsNullOrEmpty(callerinfo.EmailAddress)) Then
                errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_PHONE_OR_EMAIL_REQUIRED_ERR))
            End If
        End If
        If String.IsNullOrEmpty(callerinfo.FirstName) Then
            errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_FIRST_NAME_REQUIRED_ERR))
        End If
        If String.IsNullOrEmpty(callerinfo.LastName) Then
            errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_LAST_NAME_REQUIRED_ERR))
        End If

        If errMsg.Count > 0 Then
            MasterPage.MessageController.AddError(errMsg.ToArray, False)
            Exit Sub
        End If

            wsRequest.Caller = callerinfo
        Try

            Dim wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, BaseCaseResponse)(
                                                        GetCaseWebAppGatewayClient(),
                                                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(ByVal c As WebAppGatewayClient)
                                                            Return c.BeginInteraction(wsRequest)
                                                        End Function)

            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseCaseResponse = wsResponse
            End If

        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
            Exit Sub
        End Try

        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            DisplayNextView()
        End If

    End Sub
    Private Sub ContinueCaller()

        Dim errMsg As List(Of String) = New List(Of String)
        Dim wsRequest As CreateCaseRequest = New CreateCaseRequest()

        Select Case State.CasePurpose
            Case CasePurpose.CertificateInquiry
                wsRequest.PurposeCode = Codes.CASE_PURPOSE__INQUIRY
                Dim policyReference As New CertificateReference()
                Dim oCertificate As Certificate = New Certificate(State.CertificateId)
                policyReference.CertificateNumber = oCertificate.CertNumber
                policyReference.DealerCode = oCertificate.Dealer.Dealer
                wsRequest.Reference = policyReference
            Case CasePurpose.ClaimInquiry
                wsRequest.PurposeCode = Codes.CASE_PURPOSE__INQUIRY
                Dim claimReference As New ClaimReference()
                claimReference.CompanyCode = State.ClaimBo.Company.Code
                claimReference.ClaimNumber = State.ClaimBo.ClaimNumber
                wsRequest.Reference = claimReference
        End Select

        If String.IsNullOrEmpty(wsRequest.PurposeCode) Then
            errMsg.Add(lblPurpose.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CASE_PURPOSE_REQUIRED_ERR))
        End If

        Dim callerinfo As New PhoneCaller()
        ucCallerInfo.GetCallerInformation()

        callerinfo.FirstName = ucCallerInfo.FirstName
        callerinfo.LastName = ucCallerInfo.LastName
        callerinfo.PhoneNumber = ucCallerInfo.WorkPhoneNumber
        callerinfo.EmailAddress = ucCallerInfo.Email
        callerinfo.RelationshipTypeCode = ucCallerInfo.RelationshipCode
        callerinfo.CultureCode = Thread.CurrentThread.CurrentCulture.ToString().ToUpper()
        callerinfo.ChannelCode = "CSR"

        If (callerinfo.GetType() Is GetType(PhoneCaller)) Then
            If (String.IsNullOrEmpty(callerinfo.PhoneNumber) And String.IsNullOrEmpty(callerinfo.EmailAddress)) Then
                errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_PHONE_OR_EMAIL_REQUIRED_ERR))
            End If
        End If
        If String.IsNullOrEmpty(callerinfo.FirstName) Then
            errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_FIRST_NAME_REQUIRED_ERR))
        End If
        If String.IsNullOrEmpty(callerinfo.LastName) Then
            errMsg.Add(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CALLER_LAST_NAME_REQUIRED_ERR))
        End If

        If errMsg.Count > 0 Then
            MasterPage.MessageController.AddError(errMsg.ToArray, False)
            Exit Sub
        End If

        wsRequest.Caller = callerinfo
        Try

            Dim wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, BaseCaseResponse)(
                                                        GetCaseWebAppGatewayClient(),
                                                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(ByVal c As WebAppGatewayClient)
                                                            Return c.CreateCase(wsRequest)
                                                        End Function)

            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseCaseResponse = wsResponse
            End If

        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
            Exit Sub
        End Try

        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            DisplayNextView()
        End If

    End Sub
#End Region
#Region "Caller Authentication View - (Question View)"
    Private Sub ShowCallerAuthenticationView()
        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            If State.SubmitWsBaseCaseResponse.GetType() Is GetType(AuthenticationCaseResponse) Then
                Dim callerAuthResponse As AuthenticationCaseResponse = DirectCast(State.SubmitWsBaseCaseResponse, AuthenticationCaseResponse)

                LoadCaseInformationInHeader(callerAuthResponse.CaseNumber, callerAuthResponse.CompanyCode)
                SetUpQuestionUserControl()

                questionUserControl.QuestionDataSource = callerAuthResponse.Questions().ToArray()
                questionUserControl.QuestionDataBind()
                mvClaimsRecording.ActiveViewIndex = CaseRecordingViewIndexQuestion
                questionUserControl.SetQuestionTitle(TranslationBase.TranslateLabelOrMessage("CALLER_AUTHENTICATION"))
            End If
        ElseIf State.CaseBO IsNot Nothing Then
            LoadCaseInformationInHeader(State.CaseBO)
        End If
    End Sub
    Private Sub SetUpQuestionUserControl()
        With questionUserControl
            .HostMessageController = MasterPage.MessageController
        End With
    End Sub
    Private Sub AuthenticationAction()
        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            If State.SubmitWsBaseCaseResponse.GetType() Is GetType(AuthenticationResponse) Then
                Dim authResponse As AuthenticationResponse = DirectCast(State.SubmitWsBaseCaseResponse, AuthenticationResponse)
                If authResponse.CallerAuthenticationStatus = CallerAuthenticationStatusTypes.Success Then
                    MoveToNextPage()
                Else
                    ReturnBackToCallingPage()
                End If
            End If
        End If
    End Sub
    Private Sub ContinueQuestion()

        If State.SubmitWsBaseCaseResponse IsNot Nothing AndAlso State.SubmitWsBaseCaseResponse.GetType() Is GetType(AuthenticationCaseResponse) Then

            Dim questionSubmitObj As AuthenticationCaseResponse = DirectCast(State.SubmitWsBaseCaseResponse, AuthenticationCaseResponse)

            questionUserControl.GetQuestionAnswer()

            If (Not String.IsNullOrEmpty(questionUserControl.ErrAnswerMandatory.ToString())) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_IS_REQUIRED_ERR, True)
                Exit Sub
            ElseIf (Not String.IsNullOrEmpty(questionUserControl.ErrorQuestionCodes.ToString())) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
                Exit Sub
            End If


            Dim wsRequest As CallerAuthenticationRequest = New CallerAuthenticationRequest()

            wsRequest.CaseNumber = questionSubmitObj.CaseNumber
            wsRequest.CompanyCode = questionSubmitObj.CompanyCode
            wsRequest.InteractionNumber = questionSubmitObj.InteractionNumber
            wsRequest.Questions = questionSubmitObj.Questions

            Try
                Dim wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, BaseCaseResponse)(
                                                                        GetCaseWebAppGatewayClient(),
                                                                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                        Function(ByVal c As WebAppGatewayClient)
                                                                            Return c.Submit(wsRequest)
                                                                        End Function)

                If wsResponse IsNot Nothing Then
                    State.SubmitWsBaseCaseResponse = wsResponse
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
                Exit Sub
            End Try
            DisplayNextView()
        End If
    End Sub
#End Region

#Region "CaseInteraction View"
    Private Sub ShowCaseInteractionView()
        If State.SubmitWsBaseCaseResponse IsNot Nothing Then

            Dim oCase As CaseBase = New CaseBase(State.SubmitWsBaseCaseResponse.CaseNumber, State.SubmitWsBaseCaseResponse.CompanyCode)
            If oCase.CaseStatusCode.ToUpper() = Codes.CASE_STATUS__CLOSED Then
                ReturnBackToCallingPage()
            End If

            LoadCaseInformationInHeader(State.SubmitWsBaseCaseResponse.CaseNumber, State.SubmitWsBaseCaseResponse.CompanyCode)
            mvClaimsRecording.ActiveViewIndex = CaseRecordingViewIndexCaseInteraction
            ControlMgr.SetVisibleControl(Me, btn_Cancel, False)
            ControlMgr.SetEnableControl(Me, btn_Cancel, False)
            PopulateCaseDispositionReason()
        Else
            ReturnBackToCallingPage()
        End If
    End Sub


    Private Sub ShowCaseInquiryView()

            mvClaimsRecording.ActiveViewIndex = CaseRecordingViewIndexCaseInteraction
            ControlMgr.SetVisibleControl(Me, btn_Cancel, False)
            ControlMgr.SetEnableControl(Me, btn_Cancel, False)
            PopulateCaseDispositionReason()
    End Sub
    Private Sub PopulateCaseDispositionReason()
        Dim listcontextForMgList As ListContext = New ListContext()
        listcontextForMgList.DealerId = State.DealerId
        Dim caseClosedReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("CASECLSRSN", Thread.CurrentPrincipal.GetLanguageCode(),listcontextForMgList)
        caseClosedReason.OrderBy("ListItemId", LinqExtentions.SortDirection.Ascending)
        ddlCaseDispositionReason.Populate(caseClosedReason, New PopulateOptions() With
                                        {
                                        .AddBlankItem = True,
                                        .BlankItemValue = String.Empty,
                                        .TextFunc = AddressOf PopulateOptions.GetDescription,
                                        .ValueFunc = AddressOf PopulateOptions.GetCode
                                        })
    End Sub
    Private Sub ContinueCaseInteraction()
        If State.SubmitWsBaseCaseResponse IsNot Nothing Then
            Dim wsRequest As SaveCaseRequest = New SaveCaseRequest
            Dim errMsg As List(Of String) = New List(Of String)

            If String.IsNullOrEmpty(TextBoxCaseNotes.Text) Then
                errMsg.Add(LabelCaseNote.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If

            wsRequest.CaseNumber = State.SubmitWsBaseCaseResponse.CaseNumber
            wsRequest.CompanyCode = State.SubmitWsBaseCaseResponse.CompanyCode
            wsRequest.CloseReasonCode = GetSelectedValue(ddlCaseDispositionReason)
            Dim caseNote As New Note()
            caseNote.Body = TextBoxCaseNotes.Text
            Dim caseNotes(0) As Note
            caseNotes(0) = caseNote
            wsRequest.Notes = caseNotes

            If errMsg.Count > 0 Then
                MasterPage.MessageController.AddError(errMsg.ToArray, False)
                Exit Sub
            End If

            Try
                Dim wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, BaseCaseResponse)(
                                                                        GetCaseWebAppGatewayClient(),
                                                                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                        Function(ByVal c As WebAppGatewayClient)
                                                                            Return c.SaveCase(wsRequest)
                                                                        End Function)

                If wsResponse IsNot Nothing Then
                    ReturnBackToCallingPage()
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
                Exit Sub
            End Try

        End If
    End Sub
#End Region
End Class