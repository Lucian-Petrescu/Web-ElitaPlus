Imports System.Collections.Generic
Imports System.ComponentModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimService
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService
Imports System.ServiceModel

Partial Class ClaimIssueActionAnswerForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const Url As String = "~/Claims/ClaimIssueActionAnswerForm.aspx"
    Private Const ResponseStatusFailure As String = "Failure"
#Region "Answer Code - Commonly used across the page"
    Private Const AnswerCodeFiResentsook As String = "FI_RESENTSOOK"
    Private Const AnswerCodeFiRworclsclm As String = "FI_RWORCLSCLM"
    Private Const AnswerCodeFiSwrplactY As String = "FI_SWRPLACT_Y"
    Private Const AnswerCodeFiPyminsBt As String = "FI_PYMINS_BT"
    Private Const AnswerCodeFiPyminsGc As String = "FI_PYMINS_GC"
    Private Const AnswerCodeFiLfldevSel As String = "FI_LFLDEV_SEL"
    Private Const AnswerCodeFiLfldevNa As String = "FI_LFLDEV_NA"
    Private Const AnswerCodeFiBrdevsel As String = "FI_BRDEVSEL"
    Private Const AnswerCodeFiCrBt As String = "FI_CR_BT"
    Private Const AnswerCodeFiCrGc As String = "FI_CR_GC"
    Private Const AnswerCodeFiCrtsnimeiY As String = "FI_CRTSNIMEI_Y"
    Private Const AnswerCodeFiCrtsnimeiN As String = "FI_CRTSNIMEI_N"
    Private Const AnswerCodeFiDisfmipY As String = "FI-DISFMIP_Y"
    Private Const AnswerCodeFiDisfmipN As String = "FI-DISFMIP_N"
    Private Const AnswerCodeFiBerrpl As String = "FI_BERRPL"
    Private Const AnswerCodeFiElgnewclmY As String = "FI_ELGNEWCLM_Y"
    Private Const AnswerCodeFiElgnewclmN As String = "FI_ELGNEWCLM_N"
    Private Const AnswerCodeFiAgrnewclmY As String = "FI_AGRNEWCLM_Y"
    Private Const AnswerCodeFiAgrnewclmN As String = "FI_AGRNEWCLM_N"
    Private Const AnswerCodeFiAgrnewclmUcc As String = "FI_AGRNEWCLM_UCC"
    Private Const AnswerCodeFiReupdatesook As String = "FI_REUPDATESOOK"
    Private Const AnswerCodeFiGetvendorskuok As String = "FI_GETVENDORSKUOK"
    Private Const AnswerCodeFiResendgcreqok As String = "FI_RESENDGCREQOK"
    Private Const AnswerCodeFiSwprobdescok As String = "FI_SWPROBDESCOK"
#End Region
#Region "Constants - Device Selection"

    Private Const GridColMakeIdx As Integer = 0
    Private Const GridColModelIdx As Integer = 1
    Private Const GridColColorIdx As Integer = 2
    Private Const GridColMemoryIdx As Integer = 3
    Private Const GridColItemCodeIdx As Integer = 4
    Private Const GridColInventoryCheckIdx As Integer = 5
    Private Const GridColReplacementCostIdx As Integer = 6
    Private Const GridColNumberOfDeviceIdx As Integer = 7
    Private Const GridColDeviceIdx As Integer = 8
    Private Const GridColShippingFromNameIdx As Integer = 9
    Private Const GridColShippingFromDescriptionIdx As Integer = 10

    Private Const DeviceSelectionTableName As String = "DeviceSelectionTable"
    Private Const DsTableColNameMake As String = "Make"
    Private Const DsTableColNameModel As String = "Model"
    Private Const DsTableColNameColor As String = "Color"
    Private Const DsTableColNameMemory As String = "Memory"
    Private Const DsTableColNameItemCode As String = "ItemCode"
    Private Const DsTableColNameInventoryCheck As String = "CheckInventory"
    Private Const DsTableColNameReplacementCost As String = "ReplacementCost"
    Private Const DsTableColNameNumberOfDevice As String = "NumberOfDevice"
    Private Const DsTableColNameEnableRdoDevice As String = "EnableRdoDevice"
    Private Const DsTableColNameSelectRdoDevice As String = "SelectRdoDevice"
    Private Const DsTableColNameShippingFromName As String = "ShippingFromName"
    Private Const DsTableColNameShippingFromDescription As String = "ShippingFromDescription"

    Private Const GridColInventoryCheckCtrl As String = "CheckBoxInventory"
    Private Const GridColDeviceRdoCtrl As String = "rdoDevice"

    Private Const SearchLimit As Integer = 200 ' number of records to be return in case of SKU search
#End Region
    Private Const DoubleSpace As String = "  "
#End Region
#Region "Enums"
    Public Enum IssueActionCode
        <Description("Select best replacement device")>
        BrDevSel = 1
        <Description("Do cash reimbursement")>
        CashReim = 2
        <Description("Confirm FMIP disabled")>
        CfmFmipDis = 3
        <Description("Contact Customer To Open New Claim")>
        ChkCustNewClm = 4
        <Description("Check Eligible For New Claim")>
        ChkElgForNewClm = 5
        <Description("Convert To Replacement")>
        ConBerRprToRpl = 6
        <Description("Correct Serial/IMEI number")>
        CorrectSn = 7
        <Description("Select like to like device for replacement")>
        LflDevSel = 8
        <Description("Select Payment Instrument")>
        PymtInsSel = 9
        <Description("Resend Service Order")>
        ResendSo = 10
        <Description("Resent Service Order Update")>
        ReUpdateSo = 11
        <Description("Close Claim")>
        RworClsClaim = 12
        <Description("Authorize Replacement")>
        SwRplacp = 13
        <Description("Get Vendor SKU")>
        GetDevSku = 14
        <Description("Resent Gift Card request")>
        ResendGcReq = 15
        <Description("Capture Problem Description For Service Warranty")>
        SwProbDesc = 16
        <Description("Fulfillment Issue")>
        FulfillmentIssue = 17
        <Description("None")>
        None
    End Enum
    Public Enum IssueActionDisplay
        <Description("SerialNumber")>
        DisplaySerialNumber = 1
        <Description("YesNo")>
        DisplayYesNo = 2
        <Description("Device Selection")>
        DisplayDeviceSelection = 3
        <Description("Payment Instrument")>
        DisplayPaymentInstrument = 4
        <Description("Capture Problem Description")>
        DisplayProblemDescription = 5
        <Description("Issue Response Status")>
        DisplayIssueResponse = 6
        <Description("Issue Response Status")>
        DisplayFulfillmentIssue = 7
        <Description("None")>
        None
    End Enum

#End Region
#Region "Page State"

    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
#Region "Commonly used across the page"
        Public IssueActionDisplay As IssueActionDisplay = IssueActionDisplay.None
        Public InputParameters As Parameters
        Public SelectedActionCode As IssueActionCode = IssueActionCode.None
        Public ClaimBo As MultiAuthClaim
        Public ClaimAuthorizationId As Guid
        Public ClaimIssueResponseBo As ClaimIssueResponse = Nothing
#End Region
#Region "Device Selection"
        Public SelectedDeviceManufacture As String
        Public SelectedDeviceModel As String
        Public SelectedDeviceColor As String
        Public SelectedDeviceMemory As String
        Public DeviceSelectionTable As DataTable
        Public AuthorizedAmountAllowed As Decimal = 0
        Public ClaimAuthfulfillmentTypeXcd As String = String.Empty

        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SearchResultCount As Integer = 0
        Public TotalResultCountFound As Integer = 0

#End Region
#Region "Claim Reimbursement"
        Public BankInfoBo As BusinessObjectsNew.BankInfo
        Public ClaimIssueBo As ClaimIssue
#End Region

        Public WsSubmitIssueAnswerRequest As SubmitIssueAnswerRequest

        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New BaseState)
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
#End Region

#Region "Parameters"
    Public Class Parameters
        Public EntityIssueId As Guid = Guid.Empty
        Public ActionCode As String
        Public ClaimBo As MultiAuthClaim
        Public ClaimAuthorizationId As Guid

        Public Sub New(ByVal oClaimBo As MultiAuthClaim, ByVal guEntityIssueId As Guid, ByVal sActionCode As String, ByVal guClaimAuthorizationId As Guid)
            ClaimBo = oClaimBo
            EntityIssueId = guEntityIssueId
            ActionCode = sActionCode
            ClaimAuthorizationId = guClaimAuthorizationId
        End Sub

    End Class
#End Region
#Region "Fulfillment Web Service - Client Call"
    ''' <summary>
    ''' Gets New Instance of Claim fulfillment Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="FulfillmentServiceClient"/></returns>
    Private Shared Function GetClient() As FulfillmentServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE), False)
        Dim client = New FulfillmentServiceClient("CustomBinding_IFulfillmentService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
#End Region
#Region "Page_Events"

    Protected Sub InitializeFromFlowSession()

        State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)

        Try
            If Not State.InputParameters Is Nothing Then
                State.SelectedActionCode = SetIssueActionCode(State.InputParameters.ActionCode)
                State.ClaimIssueBo = New ClaimIssue(State.InputParameters.EntityIssueId)
                State.ClaimBo = State.InputParameters.ClaimBo
                State.ClaimAuthorizationId = State.InputParameters.ClaimAuthorizationId
                State.WsSubmitIssueAnswerRequest = New SubmitIssueAnswerRequest()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try
            If Not CallingParameters Is Nothing Then
                State.InputParameters = CType(CallingParameters, Parameters)
                State.SelectedActionCode = SetIssueActionCode(State.InputParameters.ActionCode)
                State.ClaimIssueBo = New ClaimIssue(State.InputParameters.EntityIssueId)
                State.ClaimBo = State.InputParameters.ClaimBo
                State.ClaimAuthorizationId = State.InputParameters.ClaimAuthorizationId
                State.WsSubmitIssueAnswerRequest = New SubmitIssueAnswerRequest()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        MasterPage.MessageController.Clear_Hide()
        Try
            SetFocusDefaultButtonforScreen()
            If Not IsPostBack Then
                SetupUi()
                PopulateUi()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region
#Region "Common Page - Sub & Function"
    Private Function SetIssueActionCode(ByVal actionCode As String) As IssueActionCode
        Dim selectActionCode As IssueActionCode
        Select Case actionCode
            Case "BRDEVSEL"
                selectActionCode = IssueActionCode.BrDevSel
            Case "CASHREIM"
                selectActionCode = IssueActionCode.CashReim
            Case "CFMFMIPDIS"
                selectActionCode = IssueActionCode.CfmFmipDis
            Case "CHKCUSTNEWCLM"
                selectActionCode = IssueActionCode.ChkCustNewClm
            Case "CHKELGFORNEWCLM"
                selectActionCode = IssueActionCode.ChkElgForNewClm
            Case "CONBERRPRTORPL"
                selectActionCode = IssueActionCode.ConBerRprToRpl
            Case "CORRECTSN"
                selectActionCode = IssueActionCode.CorrectSn
            Case "LFLDEVSEL"
                selectActionCode = IssueActionCode.LflDevSel
            Case "PYMTINSSEL"
                selectActionCode = IssueActionCode.PymtInsSel
            Case "RESENDSO"
                selectActionCode = IssueActionCode.ResendSo
            Case "REUPDATESO"
                selectActionCode = IssueActionCode.ReUpdateSo
            Case "RWORCLSCLAIM"
                selectActionCode = IssueActionCode.RworClsClaim
            Case "SWRPLACP"
                selectActionCode = IssueActionCode.SwRplacp
            Case "GETDEVSKU"
                selectActionCode = IssueActionCode.GetDevSku
            Case "RESENDGCREQ"
                selectActionCode = IssueActionCode.ResendGcReq
            Case "SWPROBDESC"
                selectActionCode = IssueActionCode.SwProbDesc
            Case "CFI_OIS_OOW"
                selectActionCode = IssueActionCode.FulfillmentIssue
            Case Else
                ' If the there is any other value consider it as new fulfillment issue
                ' TODO: Enhance to support other answer type and also multiple question/answer for the same issue
                selectActionCode = IssueActionCode.FulfillmentIssue
        End Select
        Return selectActionCode
    End Function
    Private Sub SetupUi()
        SetIssueActionDisplay()
        InitializeIssueActionUi()
    End Sub
    Private Sub SetIssueActionDisplay()
        Select Case State.SelectedActionCode
            Case IssueActionCode.BrDevSel
                State.IssueActionDisplay = IssueActionDisplay.DisplayDeviceSelection
            Case IssueActionCode.CashReim
                State.IssueActionDisplay = IssueActionDisplay.DisplayPaymentInstrument
            Case IssueActionCode.CfmFmipDis
                State.IssueActionDisplay = IssueActionDisplay.DisplayYesNo
            Case IssueActionCode.ChkCustNewClm
                State.IssueActionDisplay = IssueActionDisplay.DisplayYesNo
            Case IssueActionCode.ChkElgForNewClm
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.ConBerRprToRpl
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.CorrectSn
                State.IssueActionDisplay = IssueActionDisplay.DisplaySerialNumber
            Case IssueActionCode.LflDevSel
                State.IssueActionDisplay = IssueActionDisplay.DisplayDeviceSelection
            Case IssueActionCode.PymtInsSel
                State.IssueActionDisplay = IssueActionDisplay.DisplayPaymentInstrument
            Case IssueActionCode.ResendSo
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.ReUpdateSo
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.RworClsClaim
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.SwRplacp
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.GetDevSku
                State.IssueActionDisplay = IssueActionDisplay.DisplayDeviceSelection
            Case IssueActionCode.ResendGcReq
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
            Case IssueActionCode.SwProbDesc
                State.IssueActionDisplay = IssueActionDisplay.DisplayProblemDescription
            Case IssueActionCode.FulfillmentIssue
                State.IssueActionDisplay = IssueActionDisplay.DisplayFulfillmentIssue
        End Select
    End Sub
    Private Sub UpdateBreadCrum()
        Dim pageTab As String = TranslationBase.TranslateLabelOrMessage("CLAIMS")
        Dim pageTitle As String = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION")
        Dim breadCrum As String = String.Empty

        Select Case State.SelectedActionCode
            Case IssueActionCode.BrDevSel
                breadCrum = TranslationBase.TranslateLabelOrMessage("DEVICE_SELECTION")
            Case IssueActionCode.CashReim
                breadCrum = TranslationBase.TranslateLabelOrMessage("PAYMENT_INSTRUMENT")
            Case IssueActionCode.CfmFmipDis
                breadCrum = TranslationBase.TranslateLabelOrMessage("CONFIRM_FMIP_DISABLED")
            Case IssueActionCode.ChkCustNewClm
                breadCrum = TranslationBase.TranslateLabelOrMessage("CONTACT_CUSTOMER")
            Case IssueActionCode.ChkElgForNewClm
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.ConBerRprToRpl
                breadCrum = TranslationBase.TranslateLabelOrMessage("REPAIR_ORDER_BER")
            Case IssueActionCode.CorrectSn
                breadCrum = TranslationBase.TranslateLabelOrMessage("SERIAL_NUMBER")
            Case IssueActionCode.LflDevSel
                breadCrum = TranslationBase.TranslateLabelOrMessage("DEVICE_SELECTION")
            Case IssueActionCode.PymtInsSel
                breadCrum = TranslationBase.TranslateLabelOrMessage("PAYMENT_INSTRUMENT")
            Case IssueActionCode.ResendSo
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.ReUpdateSo
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.RworClsClaim
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.SwRplacp
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.GetDevSku
                breadCrum = TranslationBase.TranslateLabelOrMessage("DEVICE_SELECTION")
            Case IssueActionCode.ResendGcReq
                breadCrum = TranslationBase.TranslateLabelOrMessage("ISSUE_ACTION_STATUS")
            Case IssueActionCode.SwProbDesc
                breadCrum = TranslationBase.TranslateLabelOrMessage("CAPTURE_PROBLEM_DESCRIPTION")
            Case IssueActionCode.FulfillmentIssue
                breadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ISSUE")
            Case IssueActionCode.None
        End Select
        MasterPage.UsePageTabTitleInBreadCrum = True
        MasterPage.PageTab = pageTab
        MasterPage.PageTitle = pageTitle
        MasterPage.BreadCrum = breadCrum
    End Sub
    Private Sub InitializeIssueActionUi()
        UpdateBreadCrum()

        divSerialNumber.Visible = False
        divYesNo.Visible = False
        divDeviceSelection.Visible = False
        divPaymentInstrument.Visible = False
        divProblemDescription.Visible = False
        divIssueResponse.Visible = False
        divClaimFulfillmentIssue.Visible = False

        Select Case State.IssueActionDisplay
            Case IssueActionDisplay.DisplaySerialNumber
                divSerialNumber.Visible = True
            Case IssueActionDisplay.DisplayYesNo
                divYesNo.Visible = True
            Case IssueActionDisplay.DisplayDeviceSelection
                divDeviceSelection.Visible = True
            Case IssueActionDisplay.DisplayPaymentInstrument
                divPaymentInstrument.Visible = True
            Case IssueActionDisplay.DisplayProblemDescription
                divProblemDescription.Visible = True
            Case IssueActionDisplay.DisplayIssueResponse
                divIssueResponse.Visible = True
            Case IssueActionDisplay.DisplayFulfillmentIssue
                divClaimFulfillmentIssue.Visible = True
            Case IssueActionDisplay.None
        End Select
    End Sub

    Private Sub PopulateUi()
        Select Case State.SelectedActionCode
            Case IssueActionCode.BrDevSel
                PopulateManufactureDropDown()
                ClearDeviceSelectionSearch()
                ClearDeviceSelectionState()
                PopulateDeviceSelectionState()
                PopulateSearchFieldsFromState()
                TranslateGridHeader(GridViewDeviceSelection)
                SetGridItemStyleColor(GridViewDeviceSelection)
                ControlMgr.SetVisibleControl(Me, btnDeviceNotSelected, False)
                ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            Case IssueActionCode.CashReim
                If IsReimbursementAmountAvailable() Then
                    PopulatePaymentMethodDropdown()
                Else
                    ControlMgr.SetEnableControl(Me, ddlPaymentList, False)
                    MasterPage.MessageController.AddInformation("ISSUE_NOT_RESOLVED_REIM_AMT_NOT_AVAILABLE", True)
                End If
                EnableDisablePaymentMethodFields()
            Case IssueActionCode.CfmFmipDis
                LabelQuestion.Text = TranslationBase.TranslateLabelOrMessage("CFMFMIPDIS")
                rdoYes.Text = DoubleSpace & TranslationBase.TranslateLabelOrMessage("YES")
                rdoNo.Text = DoubleSpace & TranslationBase.TranslateLabelOrMessage("NO")
                ControlMgr.SetVisibleControl(Me, rdoNoUCC, False)
            Case IssueActionCode.ChkCustNewClm
                LabelQuestion.Text = TranslationBase.TranslateLabelOrMessage("CUSTAGRNEWCLM")
                rdoYes.Text = DoubleSpace & TranslationBase.TranslateLabelOrMessage("YES")
                rdoNo.Text = DoubleSpace & TranslationBase.TranslateLabelOrMessage("NO")
                rdoNoUCC.Text = TranslationBase.TranslateLabelOrMessage("FI_AGRNEWCLM_UCC")
            Case IssueActionCode.ChkElgForNewClm
                Dim blnIsSuccess As Boolean
                blnIsSuccess = CallNewClaimEntitledWs()
                If blnIsSuccess Then
                    CreateClaimIssueResponse(AnswerCodeFiElgnewclmY)
                Else
                    CreateClaimIssueResponse(AnswerCodeFiElgnewclmN)
                End If
                CallSubmitFulfillmentIssueAnswerWs()
            Case IssueActionCode.ConBerRprToRpl
                CreateClaimIssueResponse(AnswerCodeFiBerrpl)
                CallSubmitFulfillmentIssueAnswerWs()
            Case IssueActionCode.CorrectSn
                PopulateImeiSerialNumber()
            Case IssueActionCode.LflDevSel
                PopulateManufactureDropDown()
                ClearDeviceSelectionSearch()
                ClearDeviceSelectionState()
                PopulateDeviceSelectionState()
                PopulateSearchFieldsFromState()
                TranslateGridHeader(GridViewDeviceSelection)
                SetGridItemStyleColor(GridViewDeviceSelection)
                ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            Case IssueActionCode.PymtInsSel
                If IsReimbursementAmountAvailable() Then
                    PopulatePaymentMethodDropdown()
                Else
                    ControlMgr.SetEnableControl(Me, ddlPaymentList, False)
                    MasterPage.MessageController.AddInformation("ISSUE_NOT_RESOLVED_REIM_AMT_NOT_AVAILABLE", True)
                End If
                EnableDisablePaymentMethodFields()
            Case IssueActionCode.ResendSo
                Dim blnIsSuccess As Boolean
                blnIsSuccess = CallReSendFailedServiceOrderWs()
                If blnIsSuccess Then
                    CreateClaimIssueResponse(AnswerCodeFiResentsook)
                    CallSubmitFulfillmentIssueAnswerWs()
                End If
            Case IssueActionCode.ReUpdateSo
                Dim blnIsSuccess As Boolean
                blnIsSuccess = CallReSendFailedServiceOrderWs()
                If blnIsSuccess Then
                    CreateClaimIssueResponse(AnswerCodeFiReupdatesook)
                    CallSubmitFulfillmentIssueAnswerWs()
                End If
            Case IssueActionCode.RworClsClaim
                CreateClaimIssueResponse(AnswerCodeFiRworclsclm)
                CallSubmitFulfillmentIssueAnswerWs()
            Case IssueActionCode.SwRplacp
                CreateClaimIssueResponse(AnswerCodeFiSwrplactY)
                CallSubmitFulfillmentIssueAnswerWs()
            Case IssueActionCode.GetDevSku
                PopulateManufactureDropDown()
                ClearDeviceSelectionSearch()
                ClearDeviceSelectionState()
                PopulateDeviceSelectionState()
                PopulateSearchFieldsFromState()
                TranslateGridHeader(GridViewDeviceSelection)
                SetGridItemStyleColor(GridViewDeviceSelection)
                ControlMgr.SetVisibleControl(Me, btnDeviceNotSelected, False)
                ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            Case IssueActionCode.ResendGcReq
                Dim blnIsSuccess As Boolean
                blnIsSuccess = CallReSendFailedServiceOrderWs()
                If blnIsSuccess Then
                    CreateClaimIssueResponse(AnswerCodeFiResendgcreqok)
                    CallSubmitFulfillmentIssueAnswerWs()
                End If
            Case IssueActionCode.FulfillmentIssue
                Dim dvQuestions As DataView = State.ClaimIssueBo.ClaimIssueQuestionListByDealer(State.ClaimBo.Dealer.Id).Table.DefaultView
                If dvQuestions.Count > 0 Then
                    GridQuestions.DataSource = dvQuestions
                    GridQuestions.DataBind()
                End If
            Case IssueActionDisplay.None

        End Select
    End Sub

    Private Sub ReturnBackToCallingPage()
        NavController.Navigate(Me, FlowEvents.EVENT_BACK)
    End Sub
    Private Sub SetFocusDefaultButtonforScreen()
        Select Case State.SelectedActionCode
            Case IssueActionCode.BrDevSel, IssueActionCode.LflDevSel, IssueActionCode.GetDevSku
                SetDefaultButton(TextBoxDeviceModel, btnSkuSearch)
                SetDefaultButton(TextBoxDeviceColor, btnSkuSearch)
                SetDefaultButton(TextBoxDeviceMemory, btnSkuSearch)
                SetFocus(ddlDeviceMake)
                Form.DefaultButton = btnSkuSearch.UniqueID
        End Select
    End Sub
    Private Sub CreateClaimIssueResponse(ByVal answerCode As String)
        Dim oClaimAuthorization As ClaimAuthorization
        oClaimAuthorization = CType(State.ClaimBo.ClaimAuthorizationChildren.GetChild(State.ClaimAuthorizationId), ClaimAuthorization)

        State.WsSubmitIssueAnswerRequest.AuthorizationNumber = oClaimAuthorization.AuthorizationNumber
        State.WsSubmitIssueAnswerRequest.CompanyCode = State.ClaimBo.Company.Code
        State.WsSubmitIssueAnswerRequest.ClaimNumber = State.ClaimBo.ClaimNumber
        State.WsSubmitIssueAnswerRequest.IssueCode = State.ClaimIssueBo.IssueCode
        State.WsSubmitIssueAnswerRequest.AnswerCode = answerCode
    End Sub
    Private Sub DisplayWsErrorMessage(ByVal errCode As String, ByVal errDescription As String)
        MasterPage.MessageController.AddError(errCode & " - " & errDescription, False)
    End Sub
    Private Sub CallSubmitFulfillmentIssueAnswerWs()
        Dim wsRequest As SubmitIssueAnswerRequest
        Dim wsResponse As SubmitIssueAnswerResponse
        wsRequest = State.WsSubmitIssueAnswerRequest
        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, SubmitIssueAnswerResponse)(
                                                       GetClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As FulfillmentServiceClient)
                                                           Return c.SubmitFulfillmentIssueAnswer(wsRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try

        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(SubmitIssueAnswerResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                Else
                    DisplayIssueResponse(wsResponse)
                End If
            End If
        End If
        State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
        InitializeIssueActionUi()
    End Sub
#End Region
#Region "WS Call To claim Fulfillment WebAppGateway - Customer Decision"
    ''' <summary>
    ''' Gets New Instance of Claim Fulfillment WebAppGateway Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="WebAppGatewayClient"/></returns>
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Sub CallSubmitCustomerDecisionWs(ByVal answerCode As String)
        Dim wsRequest As CustomerDecisionRequest = new CustomerDecisionRequest()
        Dim wsResponse As CustomerDecisionResponse
        wsRequest.CompanyCode = State.ClaimBo.Company.Code
        wsRequest.AuthorizationNumber = State.ClaimBo.ClaimAuthorizationChildren.FirstOrDefault(Function(a) a.Id = State.ClaimAuthorizationId).AuthorizationNumber
        wsRequest.CustomerDecision = answerCode
        Try
            wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, CustomerDecisionResponse)(
                                                       GetClaimFulfillmentWebAppGatewayClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As WebAppGatewayClient)
                                                           Return c.CustomerDecision(wsRequest)
                                                       End Function)
        Catch fex As FaultException
            ShowFaultException(fex)
        Catch ex As Exception
            Throw
        End Try

        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(CustomerDecisionResponse) Then
                MasterPage.MessageController.AddSuccess("ISSUE_ANSWER_RESOLVED", True)
                State.IssueActionDisplay = IssueActionDisplay.DisplayIssueResponse
                InitializeIssueActionUi()
            End If
        End If
    End Sub
    Private Sub ShowFaultException(ByVal fex As FaultException)
        If fex IsNot Nothing Then
            If fex.Code IsNot Nothing OrElse Not String.IsNullOrEmpty(fex.Message) Then
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_WEB_APP_GATEWAY_SERVICE_ERR) & " - " & fex.Message, False)
            Else
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_WEB_APP_GATEWAY_SERVICE_ERR) & " - " & fex.Message, False)
            End If
        End If
    End Sub
#End Region
#Region "Issue Response Display - Sub & Function"
    Private Sub DisplayIssueResponse(ByVal wsIssueResponse As SubmitIssueAnswerResponse)
        If wsIssueResponse.IsIssueResolved Then
            MasterPage.MessageController.AddSuccess("ISSUE_ANSWER_RESOLVED", True)
        End If
        If wsIssueResponse.IsNewIssueAdded Then
            MasterPage.MessageController.AddWarning("NEW_ISSUE_ADD", True)
        End If
        If wsIssueResponse.IsNewAuthorizationAdded Then
            MasterPage.MessageController.AddWarning("NEW_AUTHORIZATION_ADD", True)
        End If
        If wsIssueResponse.NewAuthorizationPendingWithIssue Then
            MasterPage.MessageController.AddWarning("NEW_AUTHORIZATION_PENDING_WITH_ISSUE", True)
        End If
        If Not String.IsNullOrWhiteSpace(wsIssueResponse.NewCaseNumber) Then
            Dim displayMessage As String = TranslationBase.TranslateLabelOrMessage("NEW_CASE_TO_REPORT_NEW_CLAIM") & " " & wsIssueResponse.NewCaseNumber.ToString() & "."
            MasterPage.MessageController.AddWarning(displayMessage, False)
        End If
    End Sub
    Private Sub btnBackIssueResponse_Click(sender As Object, e As EventArgs) Handles btnBackIssueResponse.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Device Selection Display - Sub & Function"
    Private Sub PopulateManufactureDropDown()
        Try
            '  BindListControlToDataView(ddlDeviceMake, LookupListNew.GetManufacturerLookupList(Authentication.CompanyGroupId), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = Authentication.CurrentUser.CompanyGroup.Id
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            ddlDeviceMake.Populate(manufacturerLkl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })

            If Not State.SelectedDeviceManufacture Is Nothing AndAlso Not String.IsNullOrEmpty(State.SelectedDeviceManufacture.ToString()) Then
                SetSelectedItemByText(ddlDeviceMake, State.SelectedDeviceManufacture)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ClearDeviceSelectionSearch()
        Try
            ddlDeviceMake.SelectedIndex = 0
            TextBoxDeviceModel.Text = String.Empty
            TextBoxDeviceColor.Text = String.Empty
            TextBoxDeviceMemory.Text = String.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ClearDeviceSelectionState()
        Try
            State.SelectedDeviceManufacture = String.Empty
            State.SelectedDeviceModel = String.Empty
            State.SelectedDeviceColor = String.Empty
            State.SelectedDeviceMemory = String.Empty

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateStateWithSearchFields()
        Try
            State.SelectedDeviceManufacture = GetSelectedDescription(ddlDeviceMake)
            State.SelectedDeviceModel = TextBoxDeviceModel.Text
            State.SelectedDeviceColor = TextBoxDeviceColor.Text
            State.SelectedDeviceMemory = TextBoxDeviceMemory.Text
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateSearchFieldsFromState()
        Try
            SetSelectedItemByText(ddlDeviceMake, State.SelectedDeviceManufacture)
            TextBoxDeviceModel.Text = State.SelectedDeviceModel
            TextBoxDeviceColor.Text = State.SelectedDeviceColor
            TextBoxDeviceMemory.Text = State.SelectedDeviceMemory

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateDeviceSelectionState()
        Try
            Select Case State.SelectedActionCode
                Case IssueActionCode.GetDevSku
                    State.SelectedDeviceManufacture = State.ClaimBo.ClaimedEquipment.Manufacturer
                    State.SelectedDeviceModel = State.ClaimBo.ClaimedEquipment.Model
                    State.SelectedDeviceColor = State.ClaimBo.ClaimedEquipment.Color
                    State.SelectedDeviceMemory = State.ClaimBo.ClaimedEquipment.Memory
                Case IssueActionCode.LflDevSel
                    State.SelectedDeviceManufacture = State.ClaimBo.ClaimedEquipment.Manufacturer
                    State.SelectedDeviceModel = State.ClaimBo.ClaimedEquipment.Model
                    State.SelectedDeviceColor = State.ClaimBo.ClaimedEquipment.Color
                    State.SelectedDeviceMemory = State.ClaimBo.ClaimedEquipment.Memory
                    GetAuthorizedAmount()
                Case IssueActionCode.BrDevSel
                    GetAuthorizedAmount()
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Function ValidateSearchFields() As Boolean
        Try
            If String.IsNullOrWhiteSpace(State.SelectedDeviceManufacture) _
                AndAlso String.IsNullOrWhiteSpace(State.SelectedDeviceModel) _
                AndAlso String.IsNullOrWhiteSpace(State.SelectedDeviceColor) _
                AndAlso String.IsNullOrWhiteSpace(State.SelectedDeviceMemory) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ONE_SEARCH_FIELD_MANDATORY_ERR, True)
                Return False
            End If
            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function
    Private Sub LoadDeviceSelectionTable(ByVal skuResponseList As SearchSKUResponse)
        ' Create a new table.
        Dim deviceSelectionTable As New DataTable(DeviceSelectionTableName)

        ' Create the columns.
        deviceSelectionTable.Columns.Add(DsTableColNameItemCode, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameMake, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameModel, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameColor, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameMemory, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameInventoryCheck, GetType(Boolean))
        deviceSelectionTable.Columns.Add(DsTableColNameReplacementCost, GetType(Decimal))
        deviceSelectionTable.Columns.Add(DsTableColNameNumberOfDevice, GetType(Integer))
        deviceSelectionTable.Columns.Add(DsTableColNameEnableRdoDevice, GetType(Boolean))
        deviceSelectionTable.Columns.Add(DsTableColNameSelectRdoDevice, GetType(Boolean))
        deviceSelectionTable.Columns.Add(DsTableColNameShippingFromName, GetType(String))
        deviceSelectionTable.Columns.Add(DsTableColNameShippingFromDescription, GetType(String))

        State.TotalResultCountFound = 0 ' reset the value

        If skuResponseList.Items.Count > 0 Then
            If skuResponseList.TotalFound > SearchLimit Then
                MasterPage.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_GENERIC, True)
                State.TotalResultCountFound = skuResponseList.TotalFound
            End If

            'Add data to the new table.
            For i As Integer = 0 To skuResponseList.Items.Count - 1
                Dim tableRow As DataRow = deviceSelectionTable.NewRow()
                tableRow(DsTableColNameItemCode) = skuResponseList.Items(i).ItemCode
                tableRow(DsTableColNameMake) = skuResponseList.Items(i).Make
                tableRow(DsTableColNameModel) = skuResponseList.Items(i).Model
                tableRow(DsTableColNameColor) = skuResponseList.Items(i).Color
                tableRow(DsTableColNameMemory) = skuResponseList.Items(i).Memory
                tableRow(DsTableColNameInventoryCheck) = False
                tableRow(DsTableColNameReplacementCost) = 0
                tableRow(DsTableColNameNumberOfDevice) = 0

                tableRow(DsTableColNameSelectRdoDevice) = False

                If State.SelectedActionCode = IssueActionCode.GetDevSku Then
                    tableRow(DsTableColNameEnableRdoDevice) = True
                Else
                    tableRow(DsTableColNameEnableRdoDevice) = False
                End If

                tableRow(DsTableColNameShippingFromName) = String.Empty
                tableRow(DsTableColNameShippingFromDescription) = String.Empty

                deviceSelectionTable.Rows.Add(tableRow)
            Next
            'Persist the table in the state object.
            State.DeviceSelectionTable = deviceSelectionTable
        Else
            State.DeviceSelectionTable = Nothing
        End If
    End Sub
    Private Sub UpdateDeviceSelectionTable()
        If Not State.DeviceSelectionTable Is Nothing AndAlso State.DeviceSelectionTable.Rows.Count > 0 Then
            'Update the values.
            For Each dr As GridViewRow In GridViewDeviceSelection.Rows
                State.DeviceSelectionTable.Rows(dr.DataItemIndex).Item(DsTableColNameInventoryCheck) = (CType((dr.Cells(GridColInventoryCheckIdx).FindControl(GridColInventoryCheckCtrl)), CheckBox)).Checked
                State.DeviceSelectionTable.Rows(dr.DataItemIndex).Item(DsTableColNameSelectRdoDevice) = (CType((dr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl)), RadioButton)).Checked
            Next
        End If
    End Sub
    Private Sub DeSelectRdoDeviceSelectionTable()
        If Not State.DeviceSelectionTable Is Nothing AndAlso State.DeviceSelectionTable.Rows.Count > 0 Then
            'Update the values.
            For Each dr As DataRow In State.DeviceSelectionTable.Rows
                dr(DsTableColNameSelectRdoDevice) = False
            Next
        End If
    End Sub
    Private Sub GetAuthorizedAmount()
        Dim oClaimAuthorization As ClaimAuthorization
        oClaimAuthorization = CType(State.ClaimBo.ClaimAuthorizationChildren.GetChild(State.ClaimAuthorizationId), ClaimAuthorization)

        If Not oClaimAuthorization Is Nothing Then
            State.ClaimAuthfulfillmentTypeXcd = oClaimAuthorization.ClaimAuthfulfillmentTypeXcd
            If State.ClaimAuthfulfillmentTypeXcd <> Codes.AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPLACEMENT _
               AndAlso Not oClaimAuthorization.ReplacementAmount Is Nothing _
               AndAlso oClaimAuthorization.ReplacementAmount.Value > 0 Then
                State.AuthorizedAmountAllowed = oClaimAuthorization.ReplacementAmount.Value
            End If
        End If
        ' Add the allowed liability limit for the user
        Dim userLiabilityOverrideLimit As DecimalType = Authentication.CurrentUser.LiabilityOverrideLimit(State.ClaimBo.CompanyId)
        State.AuthorizedAmountAllowed = State.AuthorizedAmountAllowed + If(userLiabilityOverrideLimit Is Nothing,
                                                                            New Decimal(0D), userLiabilityOverrideLimit.Value)

    End Sub
#Region "Device Selection Display - Call Web Service"
    Private Sub GetDeviceData()
        Dim skuRequest As SearchSKURequest = New SearchSKURequest()

        skuRequest.DealerCode = State.ClaimBo.Dealer.Dealer
        skuRequest.Make = State.SelectedDeviceManufacture
        skuRequest.Model = State.SelectedDeviceModel
        skuRequest.Color = State.SelectedDeviceColor
        skuRequest.Memory = State.SelectedDeviceMemory
        skuRequest.SearchLimit = CType(SearchLimit, String)

        Dim skuResponse As SearchSKUResponse

        Try
            skuResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, SearchSKUResponse)(
                                                       GetClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As FulfillmentServiceClient)
                                                           Return c.SearchVendorSKU(skuRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try


        If skuResponse IsNot Nothing Then
            If skuResponse.GetType() Is GetType(SearchSKUResponse) Then
                If skuResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(skuResponse.Error.ErrorCode, skuResponse.Error.ErrorMessage)
                    State.DeviceSelectionTable = Nothing
                    Exit Sub
                End If
                LoadDeviceSelectionTable(skuResponse)
            End If
        End If
    End Sub

    Private Sub GetDeviceInventoryData()

        'Update the values.
        For Each dr As DataRow In State.DeviceSelectionTable.Rows
            If dr(DsTableColNameInventoryCheck) Then

                Dim inventoryRequest As CheckInventoryRequest = New CheckInventoryRequest()
                inventoryRequest.DealerCode = State.ClaimBo.Dealer.Dealer
                inventoryRequest.ItemCode = dr(DsTableColNameItemCode)

                Dim inventoryResponse As ItemInventoryResponse '= GetClient().InventoryCheck(inventoryRequest)
                Try

                    inventoryResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, ItemInventoryResponse)(
                                                        GetClient(),
                                                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(ByVal c As FulfillmentServiceClient)
                                                            Return c.InventoryCheck(inventoryRequest)
                                                        End Function)

                Catch ex As Exception
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
                    Throw
                End Try
                If inventoryResponse IsNot Nothing Then
                    If inventoryResponse.GetType() Is GetType(ItemInventoryResponse) Then
                        If inventoryResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                            DisplayWsErrorMessage(inventoryResponse.Error.ErrorCode, inventoryResponse.Error.ErrorMessage)
                            Exit Sub
                        End If
                        If Not inventoryResponse.Item Is Nothing Then
                            dr(DsTableColNameReplacementCost) = inventoryResponse.Item.ReplacementCost
                            dr(DsTableColNameNumberOfDevice) = inventoryResponse.Item.StockQuantity
                            dr(DsTableColNameSelectRdoDevice) = False
                            If inventoryResponse.Item.StockQuantity > 0 Then
                                If State.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPLACEMENT Then
                                    dr(DsTableColNameEnableRdoDevice) = True
                                ElseIf inventoryResponse.Item.ReplacementCost <= State.AuthorizedAmountAllowed Then
                                    dr(DsTableColNameEnableRdoDevice) = True
                                Else
                                    ' this should not be disable if claim authorization is Service Warranty
                                    dr(DsTableColNameEnableRdoDevice) = False
                                End If
                            Else
                                dr(DsTableColNameEnableRdoDevice) = False
                            End If
                            dr(DsTableColNameShippingFromName) = inventoryResponse.Item.ShippingFromName
                            dr(DsTableColNameShippingFromDescription) = inventoryResponse.Item.ShippingFromDescription
                        Else
                            dr(DsTableColNameReplacementCost) = 0
                            dr(DsTableColNameNumberOfDevice) = 0
                            dr(DsTableColNameSelectRdoDevice) = False
                            dr(DsTableColNameEnableRdoDevice) = False
                        End If
                    End If
                End If
            Else
                dr(DsTableColNameReplacementCost) = 0
                dr(DsTableColNameNumberOfDevice) = 0
                dr(DsTableColNameSelectRdoDevice) = False
                dr(DsTableColNameEnableRdoDevice) = False
            End If
        Next
    End Sub

#End Region
#Region " Datagrid Related "
    Private Sub DeselectRadioButtonGridview()
        'deselect all radiobutton in gridview
        For i As Integer = 0 To GridViewDeviceSelection.Rows.Count - 1
            Dim rb As RadioButton
            rb = CType(GridViewDeviceSelection.Rows(i).FindControl(GridColDeviceRdoCtrl), RadioButton)
            rb.Checked = False
        Next
    End Sub
    Private Sub PopulateGrid()
        Try
            GridViewDeviceSelection.AutoGenerateColumns = False

            If State.DeviceSelectionTable Is Nothing OrElse State.DeviceSelectionTable.Rows.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, GridViewDeviceSelection, False)
                lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
                State.SearchResultCount = 0
            Else
                ControlMgr.SetVisibleControl(Me, GridViewDeviceSelection, True)
                GridViewDeviceSelection.PageIndex = State.PageIndex
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                GridViewDeviceSelection.PageSize = State.PageSize
                GridViewDeviceSelection.DataSource = State.DeviceSelectionTable
                GridViewDeviceSelection.DataBind()
                If State.TotalResultCountFound > SearchLimit Then
                    lblRecordCount.Text = String.Format("{0} {1} {2} {3}", State.DeviceSelectionTable.Rows.Count.ToString(), TranslationBase.TranslateLabelOrMessage("OF"), State.TotalResultCountFound.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
                Else
                    lblRecordCount.Text = String.Format("{0} {1}", State.DeviceSelectionTable.Rows.Count.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
                End If
                State.SearchResultCount = State.DeviceSelectionTable.Rows.Count
            End If

            ControlMgr.SetVisibleControl(Me, trPageSize, True)

            If GridViewDeviceSelection.Visible AndAlso GridViewDeviceSelection.Rows.Count > 0 Then
                ControlMgr.SetEnableControl(Me, btnSearchInventory, True)
                If State.SelectedActionCode = IssueActionCode.GetDevSku Then
                    ControlMgr.SetVisibleControl(Me, btnSearchInventory, False)
                End If
            Else
                ControlMgr.SetEnableControl(Me, btnSearchInventory, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewDeviceSelection_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridViewDeviceSelection.PageIndexChanging
        Try
            GridViewDeviceSelection.PageIndex = e.NewPageIndex
            State.PageIndex = GridViewDeviceSelection.PageIndex
            UpdateDeviceSelectionTable()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(GridViewDeviceSelection, GridViewDeviceSelection.Rows.Count, State.PageSize)
            UpdateDeviceSelectionTable()
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewDeviceSelection.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewDeviceSelection_OnDataBound(sender As Object, e As EventArgs) Handles GridViewDeviceSelection.DataBound
        If State.SelectedActionCode = IssueActionCode.GetDevSku Then
            GridViewDeviceSelection.Columns(GridColInventoryCheckIdx).Visible = False
            GridViewDeviceSelection.Columns(GridColReplacementCostIdx).Visible = False
            GridViewDeviceSelection.Columns(GridColNumberOfDeviceIdx).Visible = False
        End If
    End Sub
    Private Sub GridViewDeviceSelection_PageIndexChanged(ByVal source As Object, ByVal e As EventArgs) Handles GridViewDeviceSelection.PageIndexChanged
        Try
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Device Selection Display - Save/Update Data in Elita"
    Private Function GetDeviceInformation() As Boolean
        Dim gvr As GridViewRow
        Dim blnUpdateSuccess As Boolean = True

        For Each gvr In GridViewDeviceSelection.Rows
            Dim rb As RadioButton
            rb = CType(gvr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl), RadioButton)
            If rb.Checked Then
                Dim makeId As Guid = LookupListNew.GetIdFromDescription(LookupListNew.GetManufacturerLookupList(Authentication.CompanyGroupId), gvr.Cells(GridColMakeIdx).Text)
                If makeId.Equals(Guid.Empty) Then
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.BO_ERROR_NO_MANUFACTURER_FOUND, True)
                    blnUpdateSuccess = False
                    Return blnUpdateSuccess
                End If
                Dim deviceData As DeviceInfo = New DeviceInfo
                deviceData.Make = gvr.Cells(GridColMakeIdx).Text
                deviceData.Model = gvr.Cells(GridColModelIdx).Text
                deviceData.Color = gvr.Cells(GridColColorIdx).Text
                deviceData.Memory = gvr.Cells(GridColMemoryIdx).Text
                deviceData.SkuNumber = gvr.Cells(GridColItemCodeIdx).Text
                If State.SelectedActionCode = IssueActionCode.LflDevSel OrElse State.SelectedActionCode = IssueActionCode.BrDevSel Then
                    deviceData.Price = CType(gvr.Cells(GridColReplacementCostIdx).Text, Decimal)
                    deviceData.ShippingFromName = gvr.Cells(GridColShippingFromNameIdx).Text
                    deviceData.ShippingFromDescription = gvr.Cells(GridColShippingFromDescriptionIdx).Text
                End If
                State.WsSubmitIssueAnswerRequest.Device = deviceData
                Exit For
            End If
        Next
        Return blnUpdateSuccess
    End Function
#End Region

#Region " Button Clicks "
    Protected Sub rdoDevice_CheckedChanged(sender As Object, e As EventArgs)
        'De-select all radio button in the grid
        DeselectRadioButtonGridview()
        'Set SelectRdoDevice column to False in DeviceSelectionTable
        DeSelectRdoDeviceSelectionTable()
        'check the radiobutton which is checked
        Dim senderRb As RadioButton = sender
        senderRb.Checked = True

        ControlMgr.SetEnableControl(Me, btnDeviceSelected, True)
    End Sub
    Private Sub btnSkuSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSkuSearch.Click
        Try

            DisplayNewProgressBarOnClick(btnSkuSearch, "Loading_Device")
            PopulateStateWithSearchFields()
            If ValidateSearchFields() Then
                ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
                State.PageIndex = 0
                GetDeviceData()
                PopulateGrid()
                'display the row count message at the top of the screen
                ValidSearchResultCountNew(State.SearchResultCount, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            ClearDeviceSelectionSearch()
            ClearDeviceSelectionState()
            ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            State.DeviceSelectionTable = Nothing
            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSearchInventory_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchInventory.Click
        Try
            DisplayNewProgressBarOnClick(btnSearchInventory, "Loading_Inventory")
            ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            UpdateDeviceSelectionTable()
            GetDeviceInventoryData()
            PopulateGrid()
            MasterPage.MessageController.AddInformation("DEVICE_INVENTORY_SEARCH_COMPLETE", True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnDeviceSelected_Click(sender As Object, e As EventArgs) Handles btnDeviceSelected.Click
        Try
            Dim strAnswerCode As String = String.Empty
            If Not GetDeviceInformation() Then
                Exit Sub
            End If
            If State.SelectedActionCode = IssueActionCode.GetDevSku Then
                strAnswerCode = AnswerCodeFiGetvendorskuok
            ElseIf (State.SelectedActionCode = IssueActionCode.LflDevSel) Then
                strAnswerCode = AnswerCodeFiLfldevSel
            ElseIf (State.SelectedActionCode = IssueActionCode.BrDevSel) Then
                strAnswerCode = AnswerCodeFiBrdevsel
            End If


            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnSkuSearch, False)
            ControlMgr.SetEnableControl(Me, btnSearchInventory, False)
            ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            ControlMgr.SetEnableControl(Me, btnDeviceNotSelected, False)
            CreateClaimIssueResponse(strAnswerCode)
            CallSubmitFulfillmentIssueAnswerWs()
            State.DeviceSelectionTable = Nothing
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDeviceNotSelected_Click(sender As Object, e As EventArgs) Handles btnDeviceNotSelected.Click
        Try

            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnSkuSearch, False)
            ControlMgr.SetEnableControl(Me, btnSearchInventory, False)
            ControlMgr.SetEnableControl(Me, btnDeviceSelected, False)
            ControlMgr.SetEnableControl(Me, btnDeviceNotSelected, False)
            CreateClaimIssueResponse(AnswerCodeFiLfldevNa)
            CallSubmitFulfillmentIssueAnswerWs()
            State.DeviceSelectionTable = Nothing
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnBackDeviceSelection_Click(sender As Object, e As EventArgs) Handles btnBackDeviceSelection.Click
        Try
            State.DeviceSelectionTable = Nothing
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#End Region
#Region "Payment Instrument Display - Sub & Function"
    Private Sub EnableDisablePaymentMethodFields()
        moBankInfoController.Visible = False
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
    End Sub
    Private Function CallGetClaimReimbursementAmountWs() As Boolean
        Dim wsRequest As BaseFulfillmentRequest = New BaseFulfillmentRequest()
        Dim blnSuccess As Boolean = True

        wsRequest.CompanyCode = State.ClaimBo.Company.Code
        wsRequest.ClaimNumber = State.ClaimBo.ClaimNumber

        Dim wsResponse As GetClaimReimburseAmountResponse

        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, GetClaimReimburseAmountResponse)(
                                                       GetClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As FulfillmentServiceClient)
                                                           Return c.GetClaimReimbursementAmount(wsRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try


        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(GetClaimReimburseAmountResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                    blnSuccess = False
                Else
                    TextBoxReimbursementAmount.Text = GetAmountFormattedString(wsResponse.ReimbursementAmount)
                    blnSuccess = True
                End If
            End If
        End If

        Return blnSuccess
    End Function
    Private Function IsReimbursementAmountAvailable() As Boolean
        Dim isReimbursementAmountExist As Boolean = False
        TextBoxReimbursementAmount.Text = String.Empty

        Select Case State.SelectedActionCode
            Case IssueActionCode.CashReim
                isReimbursementAmountExist = CallGetClaimReimbursementAmountWs()
            Case IssueActionCode.PymtInsSel
                Dim oClaimAuthorization As ClaimAuthorization
                oClaimAuthorization = CType(State.ClaimBo.ClaimAuthorizationChildren.GetChild(State.ClaimAuthorizationId), ClaimAuthorization)
                If Not oClaimAuthorization Is Nothing _
                        AndAlso Not oClaimAuthorization.ReimbursementAmount Is Nothing _
                        AndAlso oClaimAuthorization.ReimbursementAmount.Value > 0 Then
                    TextBoxReimbursementAmount.Text = GetAmountFormattedString(oClaimAuthorization.ReimbursementAmount.Value)
                    isReimbursementAmountExist = True
                End If
        End Select
        Return isReimbursementAmountExist
    End Function

    Private Sub PopulatePaymentMethodDropdown()
        Try

            Dim listcontextForMgList As ListContext = New ListContext()
            listcontextForMgList.CompanyGroupId = State.ClaimIssueBo.Claim.Company.CompanyGroupId
            listcontextForMgList.DealerId = State.ClaimIssueBo.Claim.Dealer.Id
            listcontextForMgList.CompanyId = State.ClaimIssueBo.Claim.Dealer.CompanyId
            listcontextForMgList.DealerGroupId = State.ClaimIssueBo.Claim.Dealer.DealerGroupId


            Dim paymentMethod As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTHD", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontextForMgList)

            Dim filterpaymentMethod As DataElements.ListItem() = (From lst In paymentMethod
                                                                  Where lst.Code = "CTT" Or lst.Code = "DGFT"
                                                                  Select lst).ToArray()

            ddlPaymentList.Populate(filterpaymentMethod, New PopulateOptions() With
                       {
                         .AddBlankItem = True
                        })

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ddlPaymentList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentList.SelectedIndexChanged
        Try
            If (ddlPaymentList.SelectedIndex > 0) Then
                If GetSelectedItem(ddlPaymentList).Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_PAYMENTMETHOD, Codes.PAYMENT_METHOD__BANK_TRANSFER)) Then
                    moBankInfoController.Visible = True
                    State.BankInfoBo = New BusinessObjectsNew.BankInfo()
                    moBankInfoController.State.myBankInfoBo = State.BankInfoBo
                    moBankInfoController.Bind(State.BankInfoBo)
                    moBankInfoController.State.myBankInfoBo.SepaEUBankTransfer = True
                    moBankInfoController.SetCountryValue(State.ClaimIssueBo.Claim.Certificate.CountryPurchaseId)

                    moBankInfoController.EnableDisableRequiredControls()
                    ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                Else
                    moBankInfoController.Visible = False
                    ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                End If
            Else
                moBankInfoController.Visible = False
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
        Try
            If (ddlPaymentList.SelectedIndex > 0) Then
                Dim strAnswerCode As String = String.Empty
                If GetSelectedItem(ddlPaymentList).Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_PAYMENTMETHOD, Codes.PAYMENT_METHOD__BANK_TRANSFER)) Then
                    'gather bank info data
                    GetBankInfoResponse()
                    'Get the Answer code based on the issue action code 
                    If (State.SelectedActionCode = IssueActionCode.PymtInsSel) Then
                        strAnswerCode = AnswerCodeFiPyminsBt
                    ElseIf (State.SelectedActionCode = IssueActionCode.CashReim) Then
                        strAnswerCode = AnswerCodeFiCrBt
                    End If
                Else
                    'Get the Answer code based on the issue action code
                    If (State.SelectedActionCode = IssueActionCode.PymtInsSel) Then
                        strAnswerCode = AnswerCodeFiPyminsGc
                    ElseIf (State.SelectedActionCode = IssueActionCode.CashReim) Then
                        strAnswerCode = AnswerCodeFiCrGc
                    End If
                End If
                'Disable all button once response is stored, except back button
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                CreateClaimIssueResponse(strAnswerCode)
                CallSubmitFulfillmentIssueAnswerWs()
            Else
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GetBankInfoResponse()

        Dim oBankInfo As BankInfo = New BankInfo
        Dim accountTypeCode As String
        Dim countryCode As String

        ' Perform Bank Info validation and populate BankInfo BO
        moBankInfoController.PopulateBOFromControl(True)

        accountTypeCode = LookupListNew.GetCodeFromId(LookupListNew.GetAccountTypeLookupList(Authentication.LangId), moBankInfoController.State.myBankInfoBo.AccountTypeId)
        If Not accountTypeCode Is Nothing Then
            If accountTypeCode.Equals(Codes.ACCOUNT_TYPE_CODE__SAVE) Then
                oBankInfo.AccountType = AccountTypes.Saving
            ElseIf accountTypeCode.Equals(Codes.ACCOUNT_TYPE_CODE__CHECK) Then
                oBankInfo.AccountType = AccountTypes.Checking
            End If
        End If

        oBankInfo.AccountNumber = moBankInfoController.State.myBankInfoBo.Account_Number
        oBankInfo.AccountOwnerName = moBankInfoController.State.myBankInfoBo.Account_Name
        oBankInfo.BankLookupCode = moBankInfoController.State.myBankInfoBo.BankLookupCode
        oBankInfo.BankName = moBankInfoController.State.myBankInfoBo.BankName
        oBankInfo.BankSortCode = moBankInfoController.State.myBankInfoBo.BankSortCode
        oBankInfo.BranchName = moBankInfoController.State.myBankInfoBo.BranchName
        If Not moBankInfoController.State.myBankInfoBo.BranchNumber Is Nothing Then
            oBankInfo.BranchNumber = CType(moBankInfoController.State.myBankInfoBo.BranchNumber.Value, Integer)
        End If
        countryCode = LookupListNew.GetCodeFromId(LookupListNew.GetCountryLookupList(), moBankInfoController.State.myBankInfoBo.CountryID)
        If Not countryCode Is Nothing Then
            oBankInfo.CountryCode = countryCode
        End If

        oBankInfo.IbanNumber = moBankInfoController.State.myBankInfoBo.IbanNumber
        oBankInfo.SwiftCode = moBankInfoController.State.myBankInfoBo.SwiftCode

        State.WsSubmitIssueAnswerRequest.BankInfo = oBankInfo
    End Sub

    Private Sub btnBackPaymentInstrument_Click(sender As Object, e As EventArgs) Handles btnBackPaymentInstrument.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Send Service Order Failure - Sub & Function"
    Private Function CallReSendFailedServiceOrderWs() As Boolean
        Dim wsRequest As ReSentServiceOrderRequest = New ReSentServiceOrderRequest()
        Dim blnSuccess As Boolean = True
        wsRequest.AuthorizationIssueId = State.ClaimIssueBo.Id

        Dim wsResponse As BaseFulfillmentResponse

        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                                                       GetClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As FulfillmentServiceClient)
                                                           Return c.ReSendFailedServiceOrder(wsRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try


        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(BaseFulfillmentResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                    blnSuccess = False
                Else
                    blnSuccess = True
                End If
            End If
        End If

        Return blnSuccess
    End Function

#End Region
#Region "Service Warranty rejected, device on hold by service center waiting for instructions - Sub & Function"
    ''' <summary>
    ''' Gets New Instance of Claim Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="FulfillmentServiceClient"/></returns>
    Private Shared Function GetClientClaimService() As ClaimServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_SERVICE), False)
        Dim client = New ClaimServiceClient("CustomBinding_IClaimService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Function CallNewClaimEntitledWs() As Boolean
        Dim wsRequest As NewClaimEntitledRequest = New NewClaimEntitledRequest()

        wsRequest.DealerCode = State.ClaimBo.Dealer.Dealer
        wsRequest.CertificateNumber = State.ClaimBo.Certificate.CertNumber
        wsRequest.LossDate = State.ClaimBo.LossDate.Value
        wsRequest.CoverageTypeCode = State.ClaimBo.CoverageTypeCode

        Dim wsResponse As NewClaimEntitledResponse

        Try
            wsResponse = WcfClientHelper.Execute(Of ClaimServiceClient, IClaimService, NewClaimEntitledResponse)(
                                                       GetClientClaimService(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As ClaimServiceClient)
                                                           Return c.NewClaimEntitled(wsRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_SERVICE_ERR, True)
            Throw
        End Try

        Return wsResponse.IsNewClaimEntitled
    End Function

#End Region
#Region "Yes No Selection Display - Sub & Function"
    Private Sub btnSaveYesNo_Click(sender As Object, e As EventArgs) Handles btnSaveYesNo.Click
        Try
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnSaveYesNo, False)

            Dim strAnswerCode As String
            Select Case State.SelectedActionCode
                Case IssueActionCode.ChkCustNewClm
                    If rdoYes.Checked Then
                        strAnswerCode = AnswerCodeFiAgrnewclmY
                    ElseIf rdoNoUCC.Checked Then
                        strAnswerCode = AnswerCodeFiAgrnewclmUcc
                    Else
                        strAnswerCode = AnswerCodeFiAgrnewclmN
                    End If
                    CreateClaimIssueResponse(strAnswerCode)
                    CallSubmitFulfillmentIssueAnswerWs()
                Case IssueActionCode.CfmFmipDis
                    If rdoYes.Checked Then
                        strAnswerCode = AnswerCodeFiDisfmipY
                    Else
                        strAnswerCode = AnswerCodeFiDisfmipN
                    End If
                    CreateClaimIssueResponse(strAnswerCode)
                    CallSubmitFulfillmentIssueAnswerWs()
            End Select

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnBackYesNo_Click(sender As Object, e As EventArgs) Handles btnBackYesNo.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Serial Number Display - Sub & Function"
    Private Sub PopulateImeiSerialNumber()

        TextBoxSerialNumber.Text = String.Empty
        TextBoxImei.Text = String.Empty

        If Not State.ClaimIssueBo Is Nothing AndAlso Not String.IsNullOrWhiteSpace(State.ClaimIssueBo.EntityIssueData) Then
            ' Sample String value -> (SerialNo: 14527863 , IMEI: 353556084081101)
            ' Split the string with delimiter comma(,)
            Dim splitImeiSerialNumber As String() = State.ClaimIssueBo.EntityIssueData.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
            For Each splitNumbers As String In splitImeiSerialNumber
                ' Split the string with delimiter comma(:)
                Dim splitNumber As String() = splitNumbers.Split(New Char() {":"c}, StringSplitOptions.RemoveEmptyEntries)
                If splitNumber.Length > 1 Then
                    If splitNumber(0).ToLower.Contains("serialno") Then
                        TextBoxSerialNumber.Text = splitNumber(1).Trim
                    ElseIf splitNumber(0).ToLower.Contains("imei") Then
                        TextBoxImei.Text = splitNumber(1).Trim
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnSaveSerialNumber_Click(sender As Object, e As EventArgs) Handles btnSaveSerialNumber.Click
        Try
            Dim strAnswerCode As String = String.Empty

            If (State.SelectedActionCode = IssueActionCode.CorrectSn) Then
                If rdoSerialNumberYes.Checked Then
                    strAnswerCode = AnswerCodeFiCrtsnimeiY
                    Dim strImei As String = TextBoxImei.Text
                    Dim strSerialNumber As String = TextBoxSerialNumber.Text
                    If Not String.IsNullOrWhiteSpace(strImei) OrElse Not String.IsNullOrWhiteSpace(strSerialNumber) Then
                        Dim deviceData As DeviceInfo = New DeviceInfo
                        deviceData.ImeiNumber = strImei
                        deviceData.SerialNumber = strSerialNumber
                        State.WsSubmitIssueAnswerRequest.Device = deviceData
                    End If
                Else
                    strAnswerCode = AnswerCodeFiCrtsnimeiN
                End If
            End If
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnSaveSerialNumber, False)
            CreateClaimIssueResponse(strAnswerCode)
            CallSubmitFulfillmentIssueAnswerWs()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnBackSerialNumber_Click(sender As Object, e As EventArgs) Handles btnBackSerialNumber.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Capture Problem Description Display - Sub & Function"
    Private Sub btnSaveProblemDescription_Click(sender As Object, e As EventArgs) Handles btnSaveProblemDescription.Click
        Try
            Dim strAnswerCode As String = String.Empty
            If (State.SelectedActionCode = IssueActionCode.SwProbDesc) Then
                State.WsSubmitIssueAnswerRequest.AnswerData = TextboxProblemDescription.Text
                If String.IsNullOrWhiteSpace(State.WsSubmitIssueAnswerRequest.AnswerData) Then
                    MasterPage.MessageController.AddError(Message.MSG_ERR_MANDATORY_ISSUE_PROBLEM_DESCRIPTION, True)
                    Exit Sub
                ElseIf State.WsSubmitIssueAnswerRequest.AnswerData.Length > 240 Then
                    MasterPage.MessageController.AddError(Message.MSG_ERR_MANDATORY_ISSUE_PROBLEM_DESCRIPTION_MAX_LENGTH_ERR, True)
                    Exit Sub
                End If
                strAnswerCode = AnswerCodeFiSwprobdescok
            End If
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnSaveProblemDescription, False)
            CreateClaimIssueResponse(strAnswerCode)
            CallSubmitFulfillmentIssueAnswerWs()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnBackProblemDescription_Click(sender As Object, e As EventArgs) Handles btnBackProblemDescription.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Claim Fulfillment Issue Display - Sub & Functions"
    'Constants for Questions Grid'
    Private Const QGridColSoftQuestionIdIdx As Integer = 1
    Private Const QGridColQuestionDescIdx As Integer = 2
    Private Const QGridColAnswerIdx As Integer = 3
    Private Const SoftQuestionId As String = "lblSoftQuestionID"
    Private Const QuestionDesc As String = "lblQuestionDesc"
    Private Const ddlYesNoControl As String = "ddlList"
    Private Const ddlWidthForOptusClaimChoices As String = "200"
    Private Sub GridQuestions_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridQuestions.RowDataBound
        Try

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                'Populate Question Description
                Dim gSoftQuestionId As New Guid(CType(e.Row.Cells(QGridColSoftQuestionIdIdx).FindControl(SoftQuestionId), Label).Text)
                Dim oQuestion As New Question(gSoftQuestionId)

                CType(e.Row.Cells(QGridColQuestionDescIdx).FindControl(QuestionDesc), Label).Text = oQuestion.TranslatedDescription
                'Populate Answer based on Answer Type
                Dim oAnswerType As New AnswerType(oQuestion.AnswerTypeId)
                ' TODO: Enhance to support other answer type and also multiple question/answer for the same issue
                Select Case oAnswerType.Code
                    Case "YES_NO"
                        Dim ddl As DropDownList = CType(e.Row.Cells(QGridColAnswerIdx).FindControl(ddlYesNoControl), DropDownList)
                        ddl.Visible = True
                        Dim dv As DataView = oQuestion.AnswerChildren.Table.AsDataView()
                        BindCodeToListControl(ddl, dv, "ANSWER_VALUE", "CODE", False, False)
                    Case "MULTI_CHOICES"
                        Dim ddl As DropDownList = CType(e.Row.Cells(QGridColAnswerIdx).FindControl(ddlYesNoControl), DropDownList)
                        ddl.Visible = True
                        ddl.Width = ddlWidthForOptusClaimChoices
                        Dim dv As DataView = oQuestion.AnswerChildren.Table.AsDataView()
                        BindCodeToListControl(ddl, dv, "ANSWER_VALUE", "CODE", False, False)
                End Select
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnIssueQuestionSave_Click(sender As Object, e As EventArgs) Handles btnIssueQuestionSave.Click
        Try
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnIssueQuestionSave, False)

            Dim strAnswerCode As String

            For Each row As GridViewRow In GridQuestions.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim gSoftQuestionId As New Guid(CType(row.Cells(QGridColSoftQuestionIdIdx).FindControl(SoftQuestionId), Label).Text)
                    Dim oQuestion As New Question(gSoftQuestionId)
                    Dim oAnswerType As New AnswerType(oQuestion.AnswerTypeId)
                    Select Case oAnswerType.Code
                        Case "YES_NO", "MULTI_CHOICES"
                            strAnswerCode = CType(row.Cells(QGridColAnswerIdx).FindControl(ddlYesNoControl), DropDownList).SelectedValue
                            Exit For
                    End Select
                End If
            Next
            CallSubmitCustomerDecisionWs(strAnswerCode)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnIssueQuestionBack_Click(sender As Object, e As EventArgs) Handles btnIssueQuestionBack.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class



