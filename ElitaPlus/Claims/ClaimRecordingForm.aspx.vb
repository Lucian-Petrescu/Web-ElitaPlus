﻿Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net
Imports System.ServiceModel
Imports System.Text
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.Elita.ServiceIntegration.Validation
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Certificates
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimRecordingService
Imports Microsoft.Practices.ObjectBuilder2
Imports Newtonsoft.Json
Imports ClientEventPayLoad = Assurant.ElitaPlus.DataEntities.DFEventPayLoad
Imports System.IO
Imports Assurant.ElitaPlus.ElitaPlusWebApp.UtilityService

Public Class ClaimRecordingForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.    
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        'CODEGEN: This method call is required by the Web Form Designer
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

    Public Const Pagetitle As String = "CLAIM_RECORDING"
    Private Const Certificates As String = "CERTIFICATES"

    Public Const Url As String = "~/Claims/ClaimRecordingForm.aspx"
    Public Const Url2 As String = "/Claims/ClaimRecordingForm.aspx"
    Public Const CertUrl As String = "/Certificates/CertificateForm.aspx"

    Private Const UserName = "CLAIM_RECSERVICE_USERNAME"
    Private Const Password = "CLAIM_RECSERVICE_PASSWORD"
    Private Const ServiceUrl = "CLAIM_SERVICE_URL"
    Private Const EndPointName = "CustomBinding_IClaimRecordingService"
    Private Const UtiliutyEndPointName = "CustomBinding_IUtilityWcf"
    Private Const FlagYes = "YESNO-Y"

    Dim _relationlist As ListItem()

    Private Const ClaimRecordingViewIndexDevice = 1
    Private Const ClaimRecordingViewIndexQuestion = 2
    Private Const ClaimRecordingViewIndexTroubleShooting = 3
    Private Const ClaimRecordingViewIndexBestReplacementDevice = 4
    Private Const ClaimRecordingViewIndexFulfillmentOptions = 5
    Private Const ClaimRecordingViewIndexLogisticsOptions = 6
    Private Const ClaimRecordingViewIndexShippingAddress = 7
    Private Const ClaimRecordingViewIndexDynamicFulfillment = 8
    Private Const gridItemDeviceInfoPurchasedDate = 4

    Private Const DoubleSpaceString As String = "  "
    Private Const NoData As String = " - "

    'US 290829
    Private Const CommandSelectDevice As String = "SelectDeviceAction"
#End Region

#Region "Page State"
    Private _isReturningFromChild As Boolean = False
    Class MyState
#Region "ClaimBo"
        Private _ClaimBo As ClaimBase
        Public Property ClaimBo As ClaimBase
            Get
                Return _ClaimBo
            End Get
            Set(ByVal value As ClaimBase)
                _ClaimBo = value
            End Set
        End Property
#End Region
        Public InputParameters As Parameters
        Friend PageSize As Integer = 5
        Friend IsGridVisible As Boolean = True
        Friend PageIndex As Integer = 0
        Public CertificateId As Guid = Guid.Empty
        Public Property CertificateNumber As String
        Public CaseId As Guid = Guid.Empty
        Public IncomingCasePurpose As String

        Public PolicyAddressBo As BusinessObjectsNew.Address = Nothing
        Public OtherAddressBo As BusinessObjectsNew.Address = Nothing
        Public CertRegisteredItem As CertRegisteredItem = Nothing
        Public IsCallerAuthenticated As Boolean = False
        Public CallerAuthenticationNeeded As Boolean = False

        Public ExclSecFieldsDt As DataTable = Nothing
        Public ExistingUserControlItemSelected As Boolean = True

        Public DeliveryDate As Nullable(Of Date)
        Public DefaultDeliveryDay As DeliveryDay
        Public DeliverySlotTimeSpan As TimeSpan
        Public IsExpeditedBtnClicked As Boolean = False
        Public BankInfoBO As Assurant.ElitaPlus.BusinessObjectsNew.BankInfo

#Region "SubmitWsBaseClaimRecordingResponse"
        Private _mSubmitWsBaseClaimRecordingResponse As BaseClaimRecordingResponse = Nothing
        Public Property SubmitWsBaseClaimRecordingResponse As BaseClaimRecordingResponse
            Get
                Return _mSubmitWsBaseClaimRecordingResponse
            End Get
            Set(value As BaseClaimRecordingResponse)
                _mSubmitWsBaseClaimRecordingResponse = value
            End Set
        End Property
#End Region
        Public ClaimedDevice As DeviceInfo = Nothing
        Public FulfillmentOption As FulfillmentOption = Nothing
        Public BestReplacementDeviceSelected As BestReplacementDeviceInfo = Nothing
        Public LogisticsStage As Integer = 0 ' 0 is first stage
        Public LogisticsOption As LogisticOption = Nothing
        Public EnableModifyClaimedDevice As Boolean = False

        Public serviceCentreCity As String
        Public serviceCentrePostalCode As String

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If  NavController IsNot Nothing _
               AndAlso  NavController.CurrentNavState IsNot Nothing _
               AndAlso NavController.CurrentFlow.Name = "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM" _
               AndAlso (NavController.CurrentNavState.Name = "CLAIM_CONSEQUENTIAL_DAMAGE" OrElse NavController.CurrentNavState.Name = "CHANGE_FULFILLMENT") Then
                ' This is an scenario where Claim Recording is called from Claim form to Add Consequentail Damage or change fulflllment
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)
            Else
                ' rest others
                Return CType(MyBase.State, MyState)
            End If
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()
        Try
            If NavController.ParametersPassed IsNot Nothing Then
                State.InputParameters = CType(NavController.ParametersPassed, Parameters)
                State.CertificateId = State.InputParameters.CertificateId
                State.CaseId = State.InputParameters.CaseId
                State.IncomingCasePurpose = State.InputParameters.CasePurpose
                If Not State.InputParameters.ClaimId.Equals(Guid.Empty) Then
                    State.ClaimBo = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.InputParameters.ClaimId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Variables"
    Dim _makeText As String
    Dim _modelText As String
    Dim _colorText As String
    Dim _memoryText As String
    Dim _skuText As String
    Dim _quantityText As String
    Dim _outOfStockText As String
    Dim _selectText As String
    Dim _currentDeviceText As String
    Dim _deviceShippedText As String
#End Region
#Region "Parameters"
    Public Class Parameters

        Public CertificateId As Guid = Nothing
        Public CaseId As Guid = Nothing
        Public ClaimId As Guid = Guid.Empty
        Public CasePurpose As String = String.Empty
        Public IsCallerAuthenticated As Boolean = False

        Public Sub New(certificateId As Guid,
                       claimId As Guid,
                       caseId As Guid,
                       Optional ByVal casePurpose As String = "",
                       Optional isCallerAuthenticated As Boolean = False)
            Me.CertificateId = certificateId
            Me.CaseId = caseId
            Me.ClaimId = claimId
            Me.CasePurpose = casePurpose
            Me.IsCallerAuthenticated = isCallerAuthenticated
        End Sub

    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public CertificateId As Guid
        Public IsCallerAuthenticated As Boolean = False
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal certId As Guid, Optional ByVal isCallerAuthenticated As Boolean = False)
            LastOperation = lastOp
            CertificateId = certId
            Me.IsCallerAuthenticated = isCallerAuthenticated
        End Sub
        Public Sub New(ByVal lastOp As DetailPageCommand)
            LastOperation = lastOp
        End Sub
    End Class
#End Region

#Region "Properties"
    Protected WithEvents MoUserControlAddress As UserControlAddress_New
    Public ReadOnly Property UserControlAddress() As UserControlAddress_New
        Get
            If MoUserControlAddress Is Nothing Then
                MoUserControlAddress = DirectCast(Master.FindControl("BodyPlaceHolder").FindControl("moAddressController"), UserControlAddress_New)
            End If
            Return MoUserControlAddress
        End Get
    End Property
    Public ReadOnly Property UcExistingCallerInfo() As UserControlCallerInfo
        Get
            Return ucCallerInfo
        End Get
    End Property
    Public ReadOnly Property UcPreviousCallerInfo() As UserControlCallerInfo
        Get
            Return ucPrevCallerInfo
        End Get
    End Property
#End Region



#Region "Page Events"
    Private Sub Page_PageCall() Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.InputParameters = CType(CallingParameters, Parameters)
                State.CertificateId = State.InputParameters.CertificateId
                State.CaseId = State.InputParameters.CaseId
                State.IncomingCasePurpose = State.InputParameters.CasePurpose
                State.IsCallerAuthenticated = State.InputParameters.IsCallerAuthenticated
                If Not State.InputParameters.ClaimId.Equals(Guid.Empty) Then
                    State.ClaimBo = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.InputParameters.ClaimId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(returnFromUrl As String, returnPar As Object) Handles Me.PageReturn
        Try
            _isReturningFromChild = True

            If returnPar IsNot Nothing AndAlso returnPar.GetType() Is GetType(ClaimForm.ReturnType) Then
                Dim returnParInstance As ClaimForm.ReturnType = DirectCast(returnPar, ClaimForm.ReturnType)
                State.IsCallerAuthenticated = returnParInstance.IsCallerAuthenticated
            End If

            State.CertRegisteredItem = Nothing

            If returnPar IsNot Nothing AndAlso returnPar.GetType() Is GetType(CertAddRegisterItemForm.ReturnType) Then
                Dim returnParInstance As CertAddRegisterItemForm.ReturnType = DirectCast(returnPar, CertAddRegisterItemForm.ReturnType)
                If returnParInstance.LastOperation = DetailPageCommand.Save Then
                    State.CertRegisteredItem = returnParInstance.EditingBo
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        If mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexShippingAddress Then
            If RadioButtonBillingAddress.Checked Then
                UserControlAddress.EnableControls(True) 'disable the address controls
            ElseIf RadioButtonOtherAddress.Checked Then
                UserControlAddress.EnableControls(False) 'enable the address controls for editing
            End If
        End If
    End Sub

    Private Sub ReWireUserControl()
        For i As Integer = 0 To GridViewLogisticsOptions.Rows.Count - 1
            Dim rb As RadioButton
            Dim lb As Label
            Dim logisticsStage As LogisticStage
            Dim wsResponse As LogisticStagesResponse
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)

            logisticsStage = wsResponse.Stages(State.LogisticsStage)
            rb = CType(GridViewLogisticsOptions.Rows(i).FindControl("rdoLogisticsOption"), RadioButton)
            lb = CType(GridViewLogisticsOptions.Rows(i).FindControl(GridLoCodeLblCtrl), Label)
            Dim lOption As LogisticOption = wsResponse.Stages(State.LogisticsStage).Options.FirstOrDefault(Function(q) q.Code = lb.Text)
            If rb.Checked AndAlso lOption.Type = LogisticOptionType.ServiceCenter Then
                Dim uc As UserControlServiceCenterSelection
                uc = CType(GridViewLogisticsOptions.Rows(i).FindControl("ucServiceCenterUserControl"), UserControlServiceCenterSelection)

                If uc IsNot Nothing Then
                    ServiceCenterSelectionHandler(uc)
                End If
            End If
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()

        AddHandler UcExistingCallerInfo.GridSelectionHandler, AddressOf UcExistingCallerInfoGridSelectionHandler
        AddHandler UcPreviousCallerInfo.GridSelectionHandler, AddressOf UcPreviousCallerInfoGridSelectionHandler
        ReWireUserControl()

        Try
            If Not (IsPostBack) Then
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                SetReturningFromChildProperty()
                SetUpQuestionUserControl()
                PopulateClaimHeaderDetails()
                UpdateBreadCrum()
                DisplayInitView()
                TranslateGridHeader(GridItems)

                If _isReturningFromChild AndAlso
                   State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso
                   State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ItemSelectionResponse) Then

                    mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexDevice
                    Dim item As ItemSelectionResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ItemSelectionResponse)

                    If State.CertRegisteredItem Is Nothing Then
                        GridItems.DataSource = item.RegisteredItems
                        GridItems.DataBind()
                    Else
                        Dim myList As List(Of DeviceInfo) = New List(Of DeviceInfo)
                        myList.AddRange(item.RegisteredItems)
                        Dim newDevice As DeviceInfo = New DeviceInfo()
                        With newDevice
                            .Model = State.CertRegisteredItem.Model
                            .PurchasedDate = State.CertRegisteredItem.PurchasedDate
                            .PurchasePrice = State.CertRegisteredItem.PurchasePrice
                            .Manufacturer = State.CertRegisteredItem.Manufacturer
                            .SerialNumber = State.CertRegisteredItem.SerialNumber
                            .DeviceType = State.CertRegisteredItem.DeviceType
                            .RegisteredItemName = State.CertRegisteredItem.RegisteredItemName

                        End With
                        If State.CertRegisteredItem.ExpirationDate Is Nothing Then
                            newDevice.ExpirationDate = New Nullable(Of Date)
                        Else
                            newDevice.ExpirationDate = State.CertRegisteredItem.ExpirationDate
                        End If
                        myList.Add(newDevice)
                        GridItems.DataSource = myList
                        GridItems.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    private  sub SetReturningFromChildProperty()
        If Not _isReturningFromChild Then
            PopulateExclSecFields()
            PopulateUserPermission()
        End If
    End sub
#End Region

#Region "Other Functions"

    Private Sub SetUpQuestionUserControl()

        With questionUserControl
            .DateFormat = DATE_FORMAT
            .UserNameSetting = UserName
            .PasswordSetting = Password
            .ServiceUrlSetting = ServiceUrl
            .ServiceEndPointNameSetting = EndPointName
            .HostMessageController = MasterPage.MessageController
        End With


        With fulfillmentOptionQuestions
            .DateFormat = DATE_FORMAT
            .UserNameSetting = UserName
            .PasswordSetting = Password
            .ServiceUrlSetting = ServiceUrl
            .ServiceEndPointNameSetting = EndPointName
            .HostMessageController = MasterPage.MessageController
        End With


    End Sub

    Public Sub UcExistingCallerInfoGridSelectionHandler(ByVal strValue As Object)
        State.ExistingUserControlItemSelected = True
        UcPreviousCallerInfo.DisableGridSelection()
    End Sub

    Public Sub UcPreviousCallerInfoGridSelectionHandler(ByVal strValue As Object)
        State.ExistingUserControlItemSelected = False
        UcExistingCallerInfo.DisableGridSelection()
    End Sub

    Private Sub DisplayEventHandler(sender As Object, e As EventArgs) Handles questionUserControl.DisplayViewEvent

        State.SubmitWsBaseClaimRecordingResponse = questionUserControl.SubmitWsBaseClaimRecordingResponse
        DisplayNextView()

    End Sub

    Private Sub OnProtectionInfoChanged(protectionInfo As UserControlQuestion.ProtectionInfo) Handles questionUserControl.ProtectionInfoChanged

        With moProtectionEvtDtl
            .ClaimNumber = protectionInfo.ClaimNumber
            .ClaimStatus = protectionInfo.ClaimStatus
            .ClaimStatusCss = protectionInfo.ClaimStatusCss
            .DateOfLoss = protectionInfo.DateOfLoss
            .TypeOfLoss = protectionInfo.TypeOfLoss
        End With
    End Sub

    Private Sub PopulateUserPermission()
        Dim oUser As User = New User()
        State.CallerAuthenticationNeeded = oUser.NeedPERMtoViewPrivacyData()
    End Sub

    Private Sub PopulateExclSecFields()
        Try
            Dim certId As Guid
            If (Not State.CertificateId.Equals(Guid.Empty)) Then
                certId = State.CertificateId
            ElseIf (Not State.CaseId.Equals(Guid.Empty)) Then
                Dim oCase As CaseBase = New CaseBase(State.CaseId)
                certId = oCase.CertId
            End If

            Dim oCertificate As Certificate = New Certificate(certId)
            If oCertificate IsNot Nothing Then
                Dim exclSecFieldsDt As DataTable
                Dim objList As List(Of CaseBase.ExclSecFields)
                If State.ExclSecFieldsDt Is Nothing Then
                    objList = CaseBase.LoadExclSecFieldsConfig(Guid.Empty, oCertificate.DealerId)
                    If objList.Count > 0 Then
                        exclSecFieldsDt = ConvertToDataTable(Of CaseBase.ExclSecFields)(objList)
                        If  exclSecFieldsDt IsNot Nothing AndAlso exclSecFieldsDt.Rows.Count > 0 Then
                            State.ExclSecFieldsDt = exclSecFieldsDt
                            'State.IsCallerAuthenticated = False                   
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Public Class ExclSecFields
    '    Public Property Table_Name As String
    '    Public Property Column_Name As String        
    'End Class


    Private Sub UpdateBreadCrum()
        MasterPage.UsePageTabTitleInBreadCrum = False
        'MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(CERTIFICATES) + " " + State.CertificateNumber + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(Certificates) & " " & State.CertificateNumber & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Pagetitle)
    End Sub
    Private Sub PopulateClaimHeaderDetails()
        Dim cssClassName As String
        Dim langId As Guid = Authentication.CurrentUser.LanguageId
        Dim certId As Guid

        moProtectionEvtDtl.CustomerName = NoData
        moProtectionEvtDtl.DealerName = NoData
        moProtectionEvtDtl.CallerName = NoData
        moProtectionEvtDtl.ProtectionStatus = NoData
        moProtectionEvtDtl.ClaimedMake = NoData
        moProtectionEvtDtl.EnrolledModel = NoData
        moProtectionEvtDtl.ClaimStatus = NoData
        moProtectionEvtDtl.EnrolledMake = NoData
        moProtectionEvtDtl.ClaimNumber = NoData
        moProtectionEvtDtl.ClaimedModel = NoData
        moProtectionEvtDtl.TypeOfLoss = NoData
        moProtectionEvtDtl.DateOfLoss = NoData

        If (Not State.CertificateId.Equals(Guid.Empty)) Then
            certId = State.CertificateId
        ElseIf (Not State.CaseId.Equals(Guid.Empty)) Then
            Dim oCase As CaseBase = New CaseBase(State.CaseId)
            certId = oCase.CertId
            moProtectionEvtDtl.CallerName = oCase.InitialCallerName
        End If

        Dim oCertificate As Certificate = New Certificate(certId)

        If  oCertificate IsNot Nothing AndAlso  oCertificate.Product IsNot Nothing Then
            If oCertificate.Product.AllowRegisteredItems = FlagYes Then
                btnNewCertRegItem_WRITE.Visible = True
            Else
                btnNewCertRegItem_WRITE.Visible = False
            End If
        End If

        State.CertificateNumber = oCertificate.CertNumber
        ' If  State.IsCallerAuthenticated = False AndAlso Not State.ExclSecFieldsDt Is Nothing AndAlso (State.ExclSecFieldsDt.AsEnumerable().Where(Function(p) p.Field(Of String)("table_name") = "ELP_CERT" and p.Field(Of String)("column_name") = "CUSTOMER_NAME").Count > 0 ) then
        If Not CaseBase.DisplaySecField(State.ExclSecFieldsDt, State.CallerAuthenticationNeeded, "ELP_CERT", "CUSTOMER_NAME", State.IsCallerAuthenticated) Then
            moProtectionEvtDtl.CustomerName = String.Empty
        Else
            moProtectionEvtDtl.CustomerName = oCertificate.CustomerName
        End If

        moProtectionEvtDtl.DealerName = oCertificate.Dealer.Dealer
        moProtectionEvtDtl.ProtectionStatus = LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode)
        cssClassName = "StatActive"
        moProtectionEvtDtl.ProtectionStatusCss = cssClassName

        SetProtectionEventDetailMakeAndModel(certId)
       
        If State.ClaimBo IsNot Nothing Then
            moProtectionEvtDtl.ClaimNumber = State.ClaimBo.ClaimNumber
            moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(Authentication.CurrentUser.LanguageId,
                                                                                  State.ClaimBo.Status)
            moProtectionEvtDtl.ClaimStatusCss = If(State.ClaimBo.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
            moProtectionEvtDtl.DateOfLoss = GetDateFormattedStringNullable(State.ClaimBo.LossDate.Value)
            moProtectionEvtDtl.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, State.ClaimBo.CertificateItem.RiskTypeId)
        End If

    End Sub

    private sub SetProtectionEventDetailMakeAndModel(ByVal certificateId as Guid)
        Dim dv As CertItem.CertItemSearchDV = CertItem.GetItems(certificateId)
        If dv.Count > 0 Then
            Dim itemsRow As DataRow = dv.Table.Rows(0)
            If  itemsRow.Item(CertItem.CertItemSearchDV.COL_MAKE) IsNot DBNull.Value Then
                moProtectionEvtDtl.EnrolledMake = itemsRow.Item(CertItem.CertItemSearchDV.COL_MAKE)
            End If
            If itemsRow.Item(CertItem.CertItemSearchDV.COL_MODEL) IsNot DBNull.Value Then
                moProtectionEvtDtl.EnrolledModel = itemsRow.Item(CertItem.CertItemSearchDV.COL_MODEL)
            End If
        End If
    end sub

    ''' <summary>
    ''' Gets New Instance of Claim Recording Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="ClaimRecordingServiceClient"/></returns>
    Private Shared Function GetClient() As ClaimRecordingServiceClient
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim client = New ClaimRecordingServiceClient(EndPointName,ConfigurationManager.AppSettings(ServiceUrl)) 
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)
        Return client
    End Function

    Private Shared Function GetMakesAndModels(dealer As String) As Object

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__UTILTY_SERVICE), False)
        Dim client = New UtilityWcfClient(UtiliutyEndPointName, oWebPasswd.Url)
        Dim token = client.LoginBody(oWebPasswd.UserId, oWebPasswd.Password, Codes.SERVICE_TYPE_GROUP_NAME)

        Dim makesAndModels As Object
        If (Not String.IsNullOrEmpty(token)) Then
            Dim requestXmlData = "<GetMakesAndModelsDs><GetMakesAndModels><DealerCode>" & dealer & "</DealerCode></GetMakesAndModels></GetMakesAndModelsDs>"
            makesAndModels = client.ProcessRequest(token, "GetMakesAndModels", requestXmlData.ToString())
        End If

        Return makesAndModels

    End Function

    Private Sub DisplayNextView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then

            questionUserControl.CertificateId = State.CertificateId
            questionUserControl.SubmitWsBaseClaimRecordingResponse = State.SubmitWsBaseClaimRecordingResponse

            Select Case State.SubmitWsBaseClaimRecordingResponse.GetType()
                Case GetType(QuestionResponse)
                    mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexQuestion
                    'questionUserControl.SetEnableSaveExitButton(True)
                    questionUserControl.SetQuestionTitle(TranslationBase.TranslateLabelOrMessage("QUESTION_SET"))
                    ' QuestionResponse response object
                    Dim questionResponse As QuestionResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, QuestionResponse)
                    questionUserControl.QuestionDataSource = questionResponse.Questions.Where(Function(q) q.Applicable = True).ToArray()
                    questionUserControl.QuestionDataBind()
                Case GetType(CallerAuthenticationResponse)
                    ' Caller Authentication response object
                    ShowCallerAuthenticationView()
                Case GetType(TroubleShootingResponse)
                    ' TroubleShooting response object
                    ShowTroubleShootingView()
                Case GetType(FulfillmentOptionsResponse)
                    ' TroubleShooting response object
                    ShowFulfillmentOptionsView()
                Case GetType(LogisticStagesResponse)
                    ShowLogisticsOptionsView()
                Case GetType(BestReplacementResponse)
                    ' BestReplacementResponse response object
                    ShowBestReplacementDeviceView()
                Case GetType(ShippingAddressResponse)
                    'ShippingAddressResponse response object
                    ShowShippingAddressView()
                Case GetType(DecisionResponse)
                    'Decision  response object
                    ShowDecisionView()
                Case GetType(ItemSelectionResponse)
                    mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexDevice
                    Dim item As ItemSelectionResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ItemSelectionResponse)
                    GridItems.DataSource = item.RegisteredItems
                    GridItems.DataBind()

                    ShowHideModifyDeviceInfoControl()
                Case GetType(ActionResponse)
                    MoveToNextPage()
                Case GetType(DynamicFulfillmentResponse)
                    ShowDynamicFulfillmentView()
                Case GetType(ProcessCompleteResponse)
                    MoveToNextPage()
                Case Else
                    ReturnBackToCallingPage()
            End Select
        End If
    End Sub

    Private Sub ShowHideModifyDeviceInfoControl()

        Dim oCertificate As Certificate = New Certificate(State.CertificateId)

        If  oCertificate.Dealer IsNot Nothing Then
            Dim allowEnrolledDeviceUpdate As AttributeValue = oCertificate.Dealer.AttributeValues.FirstOrDefault(Function(attributeValue) attributeValue.Attribute.UiProgCode = Codes.DLR_ATTR_ALLOW_MODIFY_CLAIMED_DEVICE)

            If allowEnrolledDeviceUpdate IsNot Nothing AndAlso allowEnrolledDeviceUpdate.Value = Codes.YESNO_Y Then
                divDeviceInfoContainer.Attributes("style") = "display: block"
                State.EnableModifyClaimedDevice = True
                ModifiedDeviceInfo()
            Else
                divDeviceInfoContainer.Attributes("style") = "display: none"
                State.EnableModifyClaimedDevice = False
            End If

        End If

    End Sub

    Private Sub MoveToNextPage()
        Dim wsResponse = GetWebResponse()
        Try
            If wsResponse IsNot Nothing Then

                Dim claimNumber As String
                Dim companyCode As String
                Dim caseNumber As String
                claimNumber = wsResponse.ClaimNumber
                companyCode = wsResponse.CompanyCode
                caseNumber = wsResponse.CaseNumber

                If claimNumber IsNot Nothing AndAlso companyCode IsNot Nothing AndAlso Not String.IsNullOrEmpty(claimNumber) AndAlso Not String.IsNullOrEmpty(companyCode) Then
                    NavigateToNextPage(companyCode,claimNumber)
                ElseIf caseNumber IsNot Nothing AndAlso companyCode IsNot Nothing AndAlso Not String.IsNullOrEmpty(caseNumber) AndAlso Not String.IsNullOrEmpty(companyCode) Then
                    State.CaseId = ClaimBase.GetCaseIdByCaseNumberAndCompany(caseNumber, companyCode)
                    Dim oCase As CaseBase = New CaseBase(State.CaseId)
                    NavController.Navigate(Me, FlowEvents.EVENT_DENIED_CASE_CRATED, New CaseDetailsForm.Parameters(oCase))
                End If
            Else 
                Return
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private sub NavigateToNextPage(companyCode As string, claimNumber As string)
        Dim oClaimBase As ClaimBase = ClaimFacade.Instance.GetClaimByClaimNumber(Of ClaimBase)(companyCode, claimNumber)
        If oClaimBase IsNot Nothing Then
            'Code to redirect to new page with claims information
            If oClaimBase.Status = BasicClaimStatus.Pending Then
                If (oClaimBase.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                    NavController = Nothing
                    callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, oClaimBase.Id, Nothing,,, State.IsCallerAuthenticated))
                Else
                    NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = oClaimBase
                    NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                End If

            Else
                If NavController IsNot Nothing _
                   AndAlso NavController.PrevNavState IsNot Nothing _
                   AndAlso NavController.PrevNavState.Name = "CLAIM_DETAIL" Then
                    ' This is an scenario where Claim Recording is called from Claim form to Add Consequentail Damage/change fulfillment and return back
                    NavController.Navigate(Me, FlowEvents.EVENT_NEXT, New ClaimForm.Parameters(State.ClaimBo.Id))
                Else
                    ' for others
                    callPage(ClaimForm.URL, New ClaimForm.Parameters(oClaimBase.Id, State.IsCallerAuthenticated))
                End If
            End If
        End If
    End sub

    private Function GetWebResponse() as BaseClaimRecordingResponse
        Dim wsResponse As BaseClaimRecordingResponse
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso
           State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DecisionResponse) Then
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, DecisionResponse)
        ElseIf State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso
               State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ActionResponse) Then
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ActionResponse)
        ElseIf State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso
               State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ProcessCompleteResponse) Then
            Dim completeResponse As ProcessCompleteResponse = State.SubmitWsBaseClaimRecordingResponse
            If completeResponse.ProcessResult = ProcessResults.Failure Then 'if failure, show the messages and stay the same page
                For Each msg As ClaimRecordingMessage In completeResponse.ClaimRecordingMessages
                    MasterPage.MessageController.AddError(msg.MessageText, False)
                Next
            End If
            wsResponse = completeResponse
        End If
        Return wsResponse
    End Function

    Private Sub ReturnBackToCallingPage()
        If (Not State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
            If NavController IsNot Nothing _
               AndAlso  NavController.PrevNavState IsNot Nothing _
               AndAlso NavController.PrevNavState.Name = "CLAIM_DETAIL" Then
                ' This is an scenario where Claim Recording is called from Claim form to Add Consequentail Damage
                NavController.Navigate(Me, FlowEvents.EVENT_BACK, New ClaimForm.Parameters(State.ClaimBo.Id))
            End If
        ElseIf (Not State.InputParameters.CertificateId.Equals(Guid.Empty)) Then
            Dim certId As Guid
            certId = State.CertificateId
            Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Cancel, certId, State.IsCallerAuthenticated)
            NavController = Nothing
            ReturnToCallingPage(retObj)
        ElseIf (Not State.InputParameters.CaseId.Equals(Guid.Empty)) Then
            Dim oCase As CaseBase = New CaseBase(State.InputParameters.CaseId)
            Dim retType As ReturnType = New ReturnType(DetailPageCommand.Cancel, oCase.CertId, State.IsCallerAuthenticated)
            NavController = Nothing
            ReturnToCallingPage(retType)
        End If
    End Sub
    Private Sub ThrowWsFaultExceptions(fex As FaultException)

        Dim errClaimRecordingWs As String = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR)

        If fex.GetType() Is GetType(FaultException(Of ValidationFault)) AndAlso (DirectCast(fex, FaultException(Of ValidationFault)).Detail.Items.Count > 0) Then
            Dim items As List(Of ValidationFaultItem)
            items = DirectCast(fex, FaultException(Of ValidationFault)).Detail.Items.ToList()

            For Each vf As ValidationFaultItem In items
                MasterPage.MessageController.AddError(errClaimRecordingWs & " - " & vf.Message, False)
            Next
        ElseIf fex.GetType() = GetType(FaultException(Of CallerAuthenticationFailedFault)) Then
            Dim errorMessage As String = DirectCast(fex, FaultException(Of CallerAuthenticationFailedFault)).Detail.FaultMessage.ToString()
            MasterPage.MessageController.AddError(errClaimRecordingWs & " - " & errorMessage, False)
        Else
            Log(fex)
            MasterPage.MessageController.AddError(errClaimRecordingWs & " - " & fex.Message, False)
            'Throw New GUIException(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & fex.Message, ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR, If(fex.InnerException, Nothing))
        End If

    End Sub
    Private Sub DeselectRadioButtonGridview(ByVal gridViewTarget As GridView, ByVal rdobuttonName As String)
        'deselect all radiobutton in gridview
        For i As Integer = 0 To gridViewTarget.Rows.Count - 1
            Dim rb As RadioButton
            rb = CType(gridViewTarget.Rows(i).FindControl(rdobuttonName), RadioButton)
            rb.Checked = False
        Next
    End Sub
    'KDDI

    Protected Sub EnableDisableAddressValidation(ByVal moAddressController As UserControlAddress_New)
        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        Dim btnValidateAddress As Button = moAddressController.FindControl(ValidateAddressButton)
        If oCertificate.Dealer IsNot Nothing AndAlso oCertificate.Dealer.Validate_Address = Codes.EXT_YESNO_Y Then
            ControlMgr.SetVisibleControl(Me, btnValidateAddress, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnValidateAddress, False)
        End If
    End Sub

    Private Sub DisplayInitView()
        If State.IncomingCasePurpose = Codes.CasePurposeChangeFulfillment OrElse State.InputParameters.CasePurpose = Codes.CasePurposeChangeFulfillment Then
            Try
                'call change fulfillent operation which return FulfillmentOptionsResponse                
                Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Function(ByVal c As ClaimRecordingServiceClient)
                            Return c.ChangeFulfillment(New ChangeFulfillmentRequest With {.CompanyCode = State.ClaimBo.Company.Code, .ClaimNumber = State.ClaimBo.ClaimNumber})
                        End Function)

                If wsResponse IsNot Nothing Then
                    State.SubmitWsBaseClaimRecordingResponse = wsResponse
                End If

            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
                Return
            End Try

            DisplayNextView()
        Else
            ShowCallerView()
            Dim oDealer As Dealer
            If (Not State.CertificateId.Equals(Guid.Empty)) Then
                oDealer = New Dealer(New Certificate(State.CertificateId).Dealer.Id)
            ElseIf (Not State.CaseId.Equals(Guid.Empty)) Then
                Dim oCase As CaseBase = New CaseBase(State.CaseId)
                oDealer = New Dealer(New Certificate(oCase.CertId).Dealer.Id)
            End If

            If oDealer.Show_Previous_Caller_Info = FlagYes AndAlso Session("PrevCallerFirstName") <> String.Empty Then
                ShowPrevCallerView()
            End If
        End If
    End Sub

#End Region
#Region "Button Clicks"
#Region "Cancel"
    Protected Sub BtnCancel(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelYes.Click
        Try
            ReturnBackToCallingPage()
       Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#End Region
#Region "Caller View"
#Region "Caller View - Load data"

    Private Sub ShowCallerView()

        PopulateDropdowns()

        If (Not State.CertificateId.Equals(Guid.Empty)) Then
            'check and assign the incoming purpose code
            If (Not String.IsNullOrEmpty(State.IncomingCasePurpose)) Then
                SetSelectedItem(moPurposecode, State.IncomingCasePurpose)
                moPurposecode.Enabled = False
            End If
        ElseIf (Not State.CaseId.Equals(Guid.Empty)) Then

            Dim oCase As CaseBase = New CaseBase(State.CaseId)
            SetSelectedItem(moPurposecode, oCase.CasePurposeCode)
            moPurposecode.Enabled = False
        End If

        If Not State.CallerAuthenticationNeeded Then
            UcExistingCallerInfo.PopulateGridViewCaller(State.CertificateId, State.CaseId)
        Else
            UcExistingCallerInfo.PopulateGridViewCaller(State.CertificateId, State.CaseId, State.IsCallerAuthenticated)
        End If

        State.ExistingUserControlItemSelected = True
    End Sub

    Private Sub ShowPrevCallerView()
        ucPrevCallerInfo.PopulateGridViewPrevCaller(State.CertificateId, State.CaseId, State.IsCallerAuthenticated)
    End Sub
    Private Sub PopulateDropdowns()

        Dim purposeList As ListItem()

        purposeList = (From llItem As DataRow In LookupListNew.GetPurposeList(Authentication.CurrentUser.LanguageId).ToTable().AsEnumerable()
                       Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).Distinct().ToArray()
        _relationlist = (From llItem As DataRow In LookupListNew.GetRelationshipList(Authentication.CurrentUser.LanguageId).ToTable().AsEnumerable()
                         Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).Distinct().ToArray()

        BindListControlToArray(moPurposecode, purposeList)
    End Sub


#End Region
#Region "Caller View - Other Function"

    Protected Sub CaseContinue()
        If Not State.CaseId.Equals(Guid.Empty) Then
            Dim oCase As CaseBase = New CaseBase(State.CaseId)
            Dim oCertificate As Certificate = New Certificate(oCase.CertId)
            Dim caseRequest As BeginInteractionRequest = New BeginInteractionRequest()

            caseRequest.CompanyCode = oCertificate.Company.Code
            caseRequest.CaseNumber = oCase.CaseNumber

            Dim callerInfo = GetCallerInformation()
            Session("PrevCallerFirstName") = callerinfo.FirstName
            Session("PrevCallerLastName") = callerinfo.LastName

            Session("PrevCallerWorkPhoneNumber") = callerinfo.PhoneNumber
            Session("PrevCallerEmail") = callerinfo.EmailAddress

            caseRequest.Caller = callerinfo

            caseRequest.PurposeCode = moPurposecode.SelectedValue 'oCase.CasePurposeXcd

            If callerinfo.GetType() Is GetType(PhoneCaller) AndAlso String.IsNullOrEmpty(callerinfo.PhoneNumber) AndAlso String.IsNullOrEmpty(callerinfo.EmailAddress) Then
                    MasterPage.MessageController.AddError(errorMessage:=ElitaPlus.Common.ErrorCodes.GUI_CALLER_PHONE_OR_EMAIL_REQUIRED_ERR, translate:=True)
                    return
            End If

            Try
                Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As ClaimRecordingServiceClient)
                        Return c.BeginInteraction(caseRequest)
                    End Function)
                If wsResponse IsNot Nothing Then
                    State.SubmitWsBaseClaimRecordingResponse = wsResponse
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
                Exit Sub
            End Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                If Not String.IsNullOrEmpty(callerinfo.FirstName) OrElse Not String.IsNullOrEmpty(callerinfo.LastName) Then
                    Dim callerName As String = callerinfo.FirstName & " " & callerinfo.LastName
                    moProtectionEvtDtl.CallerName = callerName.Trim
                End If
                DisplayNextView()
            End If

        End If

    End Sub
    Private Function GetCallerInformation() As PhoneCaller
        Dim callerInfo As New PhoneCaller()
        If State.ExistingUserControlItemSelected  Then
                UcExistingCallerInfo.GetCallerInformation()

                callerinfo.FirstName = UcExistingCallerInfo.FirstName
                callerinfo.LastName = UcExistingCallerInfo.LastName
                callerinfo.RelationshipTypeCode = UcExistingCallerInfo.RelationshipCode

                Session("PrevCallerRelationshipCode") = UcExistingCallerInfo.RelationshipDesc
                callerinfo.ChannelCode = "CSR"
                callerinfo.EmailAddress = UcExistingCallerInfo.Email
                callerinfo.CultureCode = Thread.CurrentThread.CurrentCulture.ToString().ToUpper()
                'For Optus , if Agent is CSR Enable the Authetication Sceen always.
                If  State.ExclSecFieldsDt IsNot Nothing AndAlso State.ExclSecFieldsDt.Rows.Count > 0 Then
                    callerinfo.IsAuthenticated = False
                End If

                callerinfo.PhoneNumber = UcExistingCallerInfo.WorkPhoneNumber

            Else
                UcPreviousCallerInfo.GetCallerInformation()

                callerinfo.FirstName = UcPreviousCallerInfo.FirstName
                callerinfo.LastName = UcPreviousCallerInfo.LastName
                callerinfo.RelationshipTypeCode = UcPreviousCallerInfo.RelationshipCode

                Session("PrevCallerRelationshipCode") = UcPreviousCallerInfo.RelationshipDesc
                callerinfo.ChannelCode = "CSR"
                callerinfo.EmailAddress = UcPreviousCallerInfo.Email
                callerinfo.CultureCode = Thread.CurrentThread.CurrentCulture.ToString().ToUpper()
                callerinfo.IsAuthenticated = State.IsCallerAuthenticated
                'For Optus , if Agent is CSR Enable the Authetication Sceen always.
                If State.ExclSecFieldsDt IsNot Nothing AndAlso State.ExclSecFieldsDt.Rows.Count > 0 Then
                    callerinfo.IsAuthenticated = False
                End If
                callerinfo.PhoneNumber = UcPreviousCallerInfo.WorkPhoneNumber

            End If
        Return callerinfo
    End Function
#End Region
#Region "Caller View - Button Click"
    Protected Sub BtnCallerContinue(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Caller_Cont.Click
        Try
            'Testing purpose
            'State.caseId = ClaimBase.GetCaseIdByCaseNumberAndCompany("2017000998", "AIF")
            '    Dim oCase As CaseBase = New CaseBase(State.caseId)
            '    NavController.Navigate(Me, FlowEvents.EVENT_DENIED_CASE_CRATED, New CaseDetailsForm.Parameters(oCase))
            Dim errMsg As List(Of String) = New List(Of String)
            If Not State.CertificateId.Equals(Guid.Empty) Then
                Dim policyRequest As CreateCaseRequest = New CreateCaseRequest()
                policyRequest.PurposeCode = moPurposecode.SelectedValue
                If (String.IsNullOrEmpty(policyRequest.PurposeCode)) Then
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CASE_PURPOSE_REQUIRED_ERR, True)
                    return
                End If

                Select Case policyRequest.PurposeCode
                    Case Codes.CASE_PURPOSE__REPORT_CLAIM, Codes.CASE_PURPOSE__CANCELLATION_REQUEST
                        Dim policyReference As New CertificateReference()
                        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
                        policyReference.CompanyCode = oCertificate.Company.Code
                        policyReference.CertificateNumber = oCertificate.CertNumber
                        policyReference.DealerCode = oCertificate.Dealer.Dealer
                        policyRequest.Reference = policyReference
                    Case Codes.CASE_PURPOSE__CONSEQUENTIAL_DAMAGE
                        Dim claimReference As New ClaimReference()
                        claimReference.CompanyCode = State.ClaimBo.Company.Code
                        claimReference.ClaimNumber = State.ClaimBo.ClaimNumber
                        policyRequest.Reference = claimReference
                End Select

                Dim callerInfo = GetCallerInformation()

                Session("PrevCallerFirstName") = callerinfo.FirstName
                Session("PrevCallerLastName") = callerinfo.LastName
                Session("PrevCallerWorkPhoneNumber") = callerinfo.PhoneNumber
                Session("PrevCallerEmail") = callerinfo.EmailAddress

                CheckCallerInfoErrors(callerInfo,errMsg)

                If errMsg.Count > 0 Then
                    MasterPage.MessageController.AddError(errMsg.ToArray, True)
                    return
                End If

                policyRequest.Caller = callerinfo

                Try

                    Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Function(ByVal c As ClaimRecordingServiceClient)
                            Return c.BeginClaim(policyRequest)
                        End Function)

                    If wsResponse IsNot Nothing Then
                        State.SubmitWsBaseClaimRecordingResponse = wsResponse
                    End If

                Catch ex As FaultException
                    ThrowWsFaultExceptions(ex)
                    Return
                End Try

                If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                    If Not String.IsNullOrEmpty(callerinfo.FirstName) OrElse Not String.IsNullOrEmpty(callerinfo.LastName) Then
                        Dim callerName As String = callerinfo.FirstName & " " & callerinfo.LastName
                        moProtectionEvtDtl.CallerName = callerName.Trim
                    End If
                    DisplayNextView()
                End If
            ElseIf Not State.CaseId.Equals(Guid.Empty) Then
                CaseContinue()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub CheckCallerInfoErrors(ByRef callerInfo As PhoneCaller, ByRef errMessage As List(Of string))

        If callerinfo.GetType() Is GetType(PhoneCaller) AndAlso String.IsNullOrEmpty(callerinfo.PhoneNumber) AndAlso String.IsNullOrEmpty(callerinfo.EmailAddress) Then
            errMessage.Add(ElitaPlus.Common.ErrorCodes.GUI_CALLER_PHONE_OR_EMAIL_REQUIRED_ERR)
        End If

        If String.IsNullOrEmpty(callerinfo.FirstName) Then
            errMessage.Add(ElitaPlus.Common.ErrorCodes.GUI_CALLER_FIRST_NAME_REQUIRED_ERR)
        End If

        If String.IsNullOrEmpty(callerinfo.LastName) Then
            errMessage.Add(ElitaPlus.Common.ErrorCodes.GUI_CALLER_LAST_NAME_REQUIRED_ERR)
        End If

    End Sub
#End Region
#End Region
#Region "Caller Authentication View - (Question View)"
    Private Sub ShowCallerAuthenticationView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing  AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(CallerAuthenticationResponse) Then
                Dim callerAuthResponse As CallerAuthenticationResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
                If callerAuthResponse IsNOt Nothing AndAlso  callerAuthResponse.ClaimRecordingMessages IsNot Nothing Then
                    DisplayWsErrorMessage(callerAuthResponse.ClaimRecordingMessages)
                End If
                questionUserControl.QuestionDataSource = callerAuthResponse.Questions.Where(Function(q) q.Applicable = True).ToArray()
                questionUserControl.QuestionDataBind()
                mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexQuestion
                'questionUserControl.SetEnableSaveExitButton(False)
                questionUserControl.SetQuestionTitle(TranslationBase.TranslateLabelOrMessage("CALLER_AUTHENTICATION"))
        End If
    End Sub
#End Region
#Region "Device View"
#Region "Device View - Load Data"
    Protected Sub GridItems_DataBound(sender As Object, e As EventArgs) Handles GridItems.DataBound
        If GridItems IsNot Nothing Then
            SetDisabled()
        End If
    End Sub

    Private Sub SetDisabled()
        Dim rdoDevice As RadioButton
        Dim checked As Boolean = False

        If vDevice.Visible AndAlso GridItems.Rows.Count >= 1 Then
                For Each gridItem As GridViewRow In GridItems.Rows
                    If gridItem.RowType <> DataControlRowType.Header AndAlso gridItem.RowType <> DataControlRowType.Footer Then
                        rdoDevice = DirectCast(gridItem.FindControl("rdoItems"), RadioButton)
                        If rdoDevice IsNot Nothing AndAlso checked = False AndAlso Not IsDeviceExpired(gridItem) Then
                            rdoDevice.Checked = True
                            checked = True
                        End If
                    End If
                Next
           End If
    End Sub
    Private Function IsDeviceExpired(ByRef gridViewRow as GridViewRow ) As Boolean
        Dim isExpired As Boolean
        If DirectCast(GridItems.DataSource, IEnumerable(Of DeviceInfo))(gridViewRow.RowIndex).ExpirationDate IsNot Nothing Then
            If DirectCast(GridItems.DataSource, IEnumerable(Of DeviceInfo))(gridViewRow.RowIndex).ExpirationDate <= Date.Now.Date Then
                gridViewRow.ForeColor = Color.DarkGray
                isExpired = true
            Else 
                isExpired =false
            End If
        End If
        Return isExpired
    End Function
    Protected Sub BindModifiedDeviceInfo(activeRadio As RadioButton)
        Try
            Dim row As GridViewRow
            Dim rdoSelect As RadioButton
            For Each row In GridItems.Rows
                rdoSelect = DirectCast(row.FindControl("rdoItems"), RadioButton)

                If activeRadio IsNot Nothing Then
                    If (rdoSelect IsNot activeRadio) Then
                        rdoSelect.Checked = False
                    Else
                        rdoSelect.Checked = True
                    End If
                End If

                If rdoSelect IsNot Nothing  AndAlso rdoSelect.Checked Then
                    
                        lblDvcMakeValue.Text = DirectCast(row.FindControl("lblManufacturer"), Label).Text
                        lblDvcModelValue.Text = DirectCast(row.FindControl("lblModel"), Label).Text
                        SetDeviceUserInterface(row)
                        txtDvcImei.Text = DirectCast(row.FindControl("lblImeiNo"), Label).Text
                        lblDvcImeiValue.Text = DirectCast(row.FindControl("lblImeiNo"), Label).Text
                        txtDvcSerialNumber.Text = DirectCast(row.FindControl("lblSerialNo"), Label).Text
                        lblDvcSerialNumberValue.Text = DirectCast(row.FindControl("lblSerialNo"), Label).Text
                        txtDvcColor.Text = DirectCast(row.FindControl("lblColor"), Label).Text
                        lblDvcColorValue.Text = DirectCast(row.FindControl("lblColor"), Label).Text
                        txtDvcCapacity.Text = DirectCast(row.FindControl("lblCapacity"), Label).Text
                        lblDvcCapacityValue.Text = DirectCast(row.FindControl("lblCapacity"), Label).Text
                   
                End If

            Next

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SetDeviceUserInterface(ByRef row As GridViewRow)
        If ddlDvcMake.Items.Count > 0 Then
            If ddlDvcMake.Items.FindByText(DirectCast(row.FindControl("lblManufacturer"), Label).Text) IsNot Nothing Then
                ddlDvcMake.SelectedValue = DirectCast(row.FindControl("lblManufacturer"), Label).Text
                txtDvcMake.Text = DirectCast(row.FindControl("lblManufacturer"), Label).Text
                txtDvcMake.Visible = False
            Else
                ddlDvcModel.Items.Clear()
                ddlDvcMake.Visible = False
                txtDvcMake.Visible = True
                txtDvcMake.Text = DirectCast(row.FindControl("lblManufacturer"), Label).Text
            End If
        Else
            ddlDvcMake.Visible = False
            txtDvcMake.Visible = True
            txtDvcMake.Text = DirectCast(row.FindControl("lblManufacturer"), Label).Text
        End If

        If ddlDvcModel.Items.Count > 0 Then
            If ddlDvcModel.Items.FindByText(DirectCast(row.FindControl("lblModel"), Label).Text) IsNot Nothing Then
                ddlDvcModel.SelectedValue = DirectCast(row.FindControl("lblModel"), Label).Text
                txtDvcModel.Text = DirectCast(row.FindControl("lblModel"), Label).Text
                txtDvcModel.Visible = False
            Else
                ddlDvcModel.Visible = False
                ddlDvcModel.Items.Clear()
                txtDvcModel.Visible = True
                txtDvcModel.Text = DirectCast(row.FindControl("lblModel"), Label).Text
            End If
        Else
            ddlDvcModel.Visible = False
            txtDvcModel.Visible = True
            txtDvcModel.Text = DirectCast(row.FindControl("lblModel"), Label).Text
        End If
    End Sub

    Private Sub ModifiedDeviceInfo()

        Try
            Dim oCertificate As Certificate = New Certificate(State.CertificateId)
            Dim dealer = oCertificate.Dealer.Dealer
            Dim makesAndModels = GetMakesAndModels(dealer)

            If (makesAndModels IsNot Nothing) Then
                Dim ds As New DataSet()
                ds.ReadXml(New StringReader(makesAndModels))

                If (ds.Tables.Count > 1) Then
                    If ds.Tables(0).Columns.Contains("Manufacturer")  Then
                        ddlDvcMake.DataSource = ds.Tables(0).DefaultView.ToTable(True, "Manufacturer")
                        ddlDvcMake.DataTextField = "MANUFACTURER"
                        ddlDvcMake.DataValueField = "MANUFACTURER"
                        ddlDvcMake.DataBind()
                    End If
                    If ds.Tables(0).Columns.Contains("Model") Then
                        ddlDvcModel.DataSource = ds.Tables(0).DefaultView.ToTable(True, "Model")
                        ddlDvcModel.DataTextField = "MODEL"
                        ddlDvcModel.DataValueField = "MODEL"
                        ddlDvcModel.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        BindModifiedDeviceInfo(Nothing)
    End Sub
    Protected Sub GridItems_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridItems.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowType <> DataControlRowType.Header AndAlso e.Row.RowType <> DataControlRowType.Footer _
                AndAlso DirectCast(GridItems.DataSource, IEnumerable(Of DeviceInfo))(e.Row.RowIndex).PurchasedDate IsNot Nothing Then

                        Dim purchasedDate = DirectCast(GridItems.DataSource, IEnumerable(Of DeviceInfo))(e.Row.RowIndex).PurchasedDate
                        If Not String.IsNullOrEmpty(purchasedDate.ToString) Then
                            e.Row.Cells(gridItemDeviceInfoPurchasedDate).Text = GetDateFormattedString(purchasedDate)
                        End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub DeviceItemSelectChanged(sender As Object, e As EventArgs)
        Dim rdoActive As RadioButton
        rdoActive = DirectCast(sender, RadioButton)
        BindModifiedDeviceInfo(rdoActive)
    End Sub

#End Region
#Region "Device View - Button Click"

    Protected Sub BtnDeviceContinue(sender As Object, e As EventArgs) Handles btn_Device_Cont.Click
        Dim row As GridViewRow
        Dim rdoSelect As RadioButton
       
        Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ItemSelectionResponse) Then
               
                Dim claimDevice As New DeviceInfo()
                Dim enrolledDevice As New DeviceInfo()
                Dim itemSelectionRequest As DeviceSelectionRequest = New DeviceSelectionRequest()

                For Each row In GridItems.Rows
                    rdoSelect = DirectCast(row.FindControl("rdoItems"), RadioButton)
                    If rdoSelect IsNot Nothing  AndAlso rdoSelect.Checked Then
                        If (State.EnableModifyClaimedDevice) Then
                            claimdevice = GetClaimDevice(row)
                            enrolledDevice = GetEnrolledDevice(row)
                            itemSelectionRequest.ClaimedDevice = claimdevice
                            itemSelectionRequest.EnrolledDevice = enrolleddevice
                        else
                            claimdevice = GetClaimDevice(row)
                            itemSelectionRequest.ClaimedDevice = claimdevice
                            itemSelectionRequest.EnrolledDevice = claimdevice
                        End If
                       
                        If (String.IsNullOrWhiteSpace(claimdevice.Manufacturer)) Then
                            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_MANUFACTURER_NAME_IS_MISSING_ERR, True)
                            return
                        End If

                    End If
                Next
                SubmitDeviceSelectionRequest(itemSelectionRequest)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private sub SubmitDeviceSelectionRequest(itemSelectionRequest As DeviceSelectionRequest)
        Dim deviceSubmitObj As ItemSelectionResponse
        deviceSubmitobj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ItemSelectionResponse)
        If itemSelectionRequest.ClaimedDevice IsNot Nothing Then
            itemSelectionRequest.CompanyCode = deviceSubmitobj.CompanyCode
            itemSelectionRequest.CaseNumber = deviceSubmitobj.CaseNumber
            itemSelectionRequest.InteractionNumber = deviceSubmitobj.InteractionNumber
            Try
                Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As ClaimRecordingServiceClient)
                        Return c.Submit(itemSelectionRequest)
                    End Function)
                If wsResponse IsNot Nothing Then
                    State.ClaimedDevice = itemSelectionRequest.ClaimedDevice
                    State.SubmitWsBaseClaimRecordingResponse = wsResponse
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
                Return
            End Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                moProtectionEvtDtl.ClaimedMake = itemSelectionRequest.ClaimedDevice.Manufacturer
                moProtectionEvtDtl.ClaimedModel = itemSelectionRequest.ClaimedDevice.Model
                DisplayNextView()
            End If
        End If
    End sub

    Private Function GetClaimDevice(ByRef row As GridViewRow) As DeviceInfo
        Dim claimDevice As New DeviceInfo()
        If (State.EnableModifyClaimedDevice) Then

            If ddlDvcMake.Items.Count > 0 Then
                claimdevice.Manufacturer = ddlDvcMake.SelectedItem.Text
            Else
                claimdevice.Manufacturer = txtDvcMake.Text
            End If

            If ddlDvcModel.Items.Count > 0 Then
                claimdevice.Model = ddlDvcModel.SelectedItem.Text
            Else
                claimdevice.Model = txtDvcModel.Text
            End If
        End if
            claimdevice.ImeiNumber = txtDvcImei.Text
            claimdevice.SerialNumber = txtDvcSerialNumber.Text
            claimdevice.Color = txtDvcColor.Text
            claimdevice.Capacity = txtDvcCapacity.Text
            claimdevice.RegisteredItemName = DirectCast(row.FindControl("lblRegisteredItem"), Label).Text
            claimdevice.RiskTypeCode = DirectCast(row.FindControl("HiddenRiskType"), HiddenField).Value
    return claimDevice 
    End Function
    Private Function GetEnrolledDevice(ByRef row As GridViewRow) As DeviceInfo
        Dim enrolledDevice As New DeviceInfo()
            enrolleddevice.Manufacturer = DirectCast(row.FindControl("lblManufacturer"), Label).Text
            enrolleddevice.Model = DirectCast(row.FindControl("lblModel"), Label).Text
            enrolleddevice.ImeiNumber = DirectCast(row.FindControl("lblImeiNo"), Label).Text
            enrolleddevice.SerialNumber = DirectCast(row.FindControl("lblSerialNo"), Label).Text
            enrolleddevice.Color = DirectCast(row.FindControl("lblColor"), Label).Text
            enrolleddevice.Capacity = DirectCast(row.FindControl("lblCapacity"), Label).Text
            enrolleddevice.RegisteredItemName = DirectCast(row.FindControl("lblRegisteredItem"), Label).Text
            enrolleddevice.RiskTypeCode = DirectCast(row.FindControl("HiddenRiskType"), HiddenField).Value
       return enrolledDevice
    End Function
    Protected Sub BtnDeviceSave(sender As Object, e As EventArgs) Handles btn_Device_SaveExit.Click
        Dim row As GridViewRow
        Dim rdoSelect As RadioButton
        Dim deviceSubmitobj As ItemSelectionResponse

        Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ItemSelectionResponse) Then
                deviceSubmitobj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ItemSelectionResponse)
                Dim itemSelectionRequest As DeviceSelectionRequest = New DeviceSelectionRequest()
                Dim claimdevice As New DeviceInfo()
                Dim enrolleddevice As New DeviceInfo()

                For Each row In GridItems.Rows
                    rdoSelect = DirectCast(row.FindControl("rdoItems"), RadioButton)
                    If rdoSelect IsNot Nothing AndAlso rdoSelect.Checked Then

                        claimdevice = GetClaimDevice(row)
                        enrolledDevice = GetEnrolledDevice(row)

                        If (String.IsNullOrWhiteSpace(claimdevice.Manufacturer)) Then

                            claimdevice.Manufacturer = moProtectionEvtDtl.EnrolledMake
                        End If
                       
                        itemSelectionRequest.ClaimedDevice = claimdevice
                        itemSelectionRequest.EnrolledDevice = enrolleddevice

                    End If
                Next

                If (String.IsNullOrWhiteSpace(claimdevice.Manufacturer)) Then
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_MANUFACTURER_NAME_IS_MISSING_ERR, True)
                    return
                End If

                If itemSelectionRequest.ClaimedDevice IsNot Nothing Then
                    itemSelectionRequest.CompanyCode = deviceSubmitobj.CompanyCode
                    itemSelectionRequest.CaseNumber = deviceSubmitobj.CaseNumber
                    itemSelectionRequest.InteractionNumber = deviceSubmitobj.InteractionNumber
                    Try
                        WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService)(
                            GetClient(),
                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                            Sub(ByVal c As ClaimRecordingServiceClient)
                                c.Save(itemSelectionRequest)
                            End Sub)
                    Catch ex As FaultException
                        ThrowWsFaultExceptions(ex)
                        Return
                    End Try
                    ReturnBackToCallingPage()
                End If
            End If
       Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ButtonNewCertRegItemWriteClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewCertRegItem_WRITE.Click

        Try

            Dim par As CertAddRegisterItemForm.Parameters = New CertAddRegisterItemForm.Parameters(State.CertificateId)

            Dim newState As CertAddRegisterItemForm.MyState = New CertAddRegisterItemForm.MyState()
            newState.certificateId = State.CertificateId
            NavController.State = Nothing

            Dim cert As Certificate = New Certificate(State.CertificateId)
            NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = cert
            NavController.State = Nothing

            callPage("~/Certificates/CertAddRegisterItemForm.aspx", par, Nothing)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region
#End Region
#Region "Questions View"
#Region "Questions View - Load Data"

#Region "ReEvaluate Questions Data"

#End Region

#End Region
#Region "Question View - Button Click"
    Protected Sub BtnQuestContinue(sender As Object, e As EventArgs) Handles btn_Quest_Cont.Click

        Dim questionSubmitObj

        Try
                If State.SubmitWsBaseClaimRecordingResponse Is Nothing Then Return

                If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(QuestionResponse) Then
                    questionSubmitObj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, QuestionResponse)

                ElseIf State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(CallerAuthenticationResponse) Then
                    questionSubmitObj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
                Else
                    Return
                End If
                   
                ValidateQuestionAnswers()

                Dim wsRequest
                If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                    If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(QuestionResponse) Then
                        wsRequest = New QuestionRequest()
                        wsRequest.QuestionSetCode = questionSubmitObj.QuestionSetCode
                        wsRequest.Version = questionSubmitObj.Version
                    ElseIf State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(CallerAuthenticationResponse) Then
                        wsRequest = New CallerAuthenticationRequest()
                    End If
                End If

                wsRequest.CaseNumber = questionSubmitObj.CaseNumber
                wsRequest.CompanyCode = questionSubmitObj.CompanyCode
                wsRequest.InteractionNumber = questionSubmitObj.InteractionNumber
                wsRequest.Questions = questionSubmitObj.Questions

                Try
                    Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Function(ByVal c As ClaimRecordingServiceClient)
                            Return c.Submit(wsRequest)
                        End Function)

                    ProcessQuestionResponse(wsResponse)
                    
                Catch ex As FaultException(Of PolicyCancelRequestDeniedFault)
                    Dim oCase As CaseBase = New CaseBase(wsRequest.CaseNumber.ToString(), wsRequest.CompanyCode.ToString())
                    callPage(CaseDetailsForm.Url, oCase.Id)

                Catch ex As FaultException
                    ThrowWsFaultExceptions(ex)
                    return
                End Try

                If questionSubmitObj.GetType() Is GetType(CallerAuthenticationResponse) Then
                    SetProtectionEventDetails()
                End If

                DisplayNextView()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ValidateQuestionAnswers()
        questionUserControl.GetQuestionAnswer()

        If (Not String.IsNullOrEmpty(questionUserControl.ErrAnswerMandatory.ToString())) Then
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_IS_REQUIRED_ERR, True)
            Return
        ElseIf (Not String.IsNullOrEmpty(questionUserControl.ErrorQuestionCodes.ToString())) Then
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
            Return
        ElseIf (Not String.IsNullOrEmpty(questionUserControl.ErrTextAnswerLength.ToString())) Then
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_LENGTH_TO_QUESTION_TOO_LONG_ERR, True)
            Return
        ElseIf questionUserControl.ErrorQuestionValidation IsNot Nothing AndAlso Not String.IsNullOrEmpty(questionUserControl.ErrorQuestionValidation.ToString()) Then
            MasterPage.MessageController.AddError(questionUserControl.ErrorQuestionValidation.ToString(), false)
            Return
        End If
    End Sub
    Private Sub ProcessQuestionResponse(ByVal wsResponse As BaseClaimRecordingResponse)
        If wsResponse IsNot Nothing Then
            State.SubmitWsBaseClaimRecordingResponse = wsResponse

            If State.ClaimBo Is Nothing _
               AndAlso State.SubmitWsBaseClaimRecordingResponse.ClaimNumber IsNot Nothing _
               AndAlso State.SubmitWsBaseClaimRecordingResponse.CompanyCode IsNot Nothing _
               AndAlso Not String.IsNullOrEmpty(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber) _
               AndAlso Not String.IsNullOrEmpty(State.SubmitWsBaseClaimRecordingResponse.CompanyCode) Then
                Dim oClaimBase As ClaimBase = ClaimFacade.Instance.GetClaimByClaimNumber(Of ClaimBase)(State.SubmitWsBaseClaimRecordingResponse.CompanyCode, State.SubmitWsBaseClaimRecordingResponse.ClaimNumber)
                If oClaimBase IsNot Nothing Then
                    State.ClaimBo = oClaimBase
                    moProtectionEvtDtl.ClaimNumber = oClaimBase.ClaimNumber
                    moProtectionEvtDtl.ClaimStatus =
                        LookupListNew.GetClaimStatusFromCode(Authentication.CurrentUser.LanguageId,
                                                             oClaimBase.Status)
                    moProtectionEvtDtl.ClaimStatusCss = If(oClaimBase.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
                    moProtectionEvtDtl.DateOfLoss = GetDateFormattedStringNullable(oClaimBase.LossDate.Value)
                    moProtectionEvtDtl.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, oClaimBase.CertificateItem.RiskTypeId)
                End If
            End If
        End If
    End Sub
    Private Sub SetProtectionEventDetails()
        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        State.IsCallerAuthenticated = True
        'If  State.IsCallerAuthenticated = False AndAlso Not State.ExclSecFieldsDt Is Nothing AndAlso (State.ExclSecFieldsDt.AsEnumerable().Where(Function(p) p.Field(Of String)("table_name") = "ELP_CERT" and p.Field(Of String)("column_name") = "CUSTOMER_NAME").Count > 0 ) then
        If Not CaseBase.DisplaySecField(State.ExclSecFieldsDt, State.CallerAuthenticationNeeded, "ELP_CERT", "CUSTOMER_NAME", State.IsCallerAuthenticated) Then
            moProtectionEvtDtl.CustomerName = String.Empty
        Else
            moProtectionEvtDtl.CustomerName = oCertificate.CustomerName
        End If
        'If  State.IsCallerAuthenticated = False AndAlso Not State.ExclSecFieldsDt Is Nothing AndAlso (State.ExclSecFieldsDt.AsEnumerable().Where(Function(p) p.Field(Of String)("table_name") = "ELP_ADDRESS" and p.Field(Of String)("column_name") = "ADDRESS1").Count > 0 ) then
        If Not CaseBase.DisplaySecField(State.ExclSecFieldsDt, State.CallerAuthenticationNeeded, "ELP_ADDRESS", "ADDRESS1", State.IsCallerAuthenticated) Then
            moProtectionEvtDtl.ShowCustomerAddress = False
            moProtectionEvtDtl.CustomerAddress = String.Empty
        Else
            moProtectionEvtDtl.ShowCustomerAddress = True
            moProtectionEvtDtl.CustomerAddress = getCustomerAddress(oCertificate.AddressChild)
        End If
    End Sub

    Private Function GetCustomerAddress(ByVal customerAddress As BusinessObjectsNew.Address) As String
        Dim sbCustomerAddress As New StringBuilder
            If customerAddress Is Nothing Then
             return sbCustomerAddress.ToString()
            End if
            If Not String.IsNullOrWhiteSpace(customerAddress.Address1) Then
                sbCustomerAddress.Append(customerAddress.Address1).Append(", ")
            End If
            If Not String.IsNullOrWhiteSpace(customerAddress.Address2) Then
                sbCustomerAddress.Append(customerAddress.Address2).Append(", ")
            End If
            If Not String.IsNullOrWhiteSpace(customerAddress.Address3) Then
                sbCustomerAddress.Append(customerAddress.Address3).Append(", ")
            End If
            If Not String.IsNullOrWhiteSpace(customerAddress.City) Then
                sbCustomerAddress.Append(customerAddress.City).Append(", ")
            End If
            If Not String.IsNullOrWhiteSpace(customerAddress.PostalCode) Then
                sbCustomerAddress.Append(customerAddress.PostalCode).Append(", ")
            End If
            If Not customerAddress.RegionId.Equals(Guid.Empty) Then
                Dim oRegion As New BusinessObjectsNew.Region(customerAddress.RegionId)
                If Not String.IsNullOrWhiteSpace(oRegion.Description) Then
                    sbCustomerAddress.Append(oRegion.Description).Append(", ")
                End If
            End If
            If Not customerAddress.CountryId.Equals(Guid.Empty) Then
                Dim country As String = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, customerAddress.CountryId)
                If Not String.IsNullOrWhiteSpace(country) Then
                    sbCustomerAddress.Append(country).Append(", ")
                End If
            End If

            If sbCustomerAddress IsNot Nothing AndAlso sbCustomerAddress.Length > 0 Then
                sbCustomerAddress.Length = sbCustomerAddress.Length - 2
            End If

        Return sbCustomerAddress.ToString()

    End Function

    Protected Sub BtnQuestSave(sender As Object, e As EventArgs) Handles btn_Quest_SaveExit.Click

        Dim questionSubmitobj

        Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(QuestionResponse) Then
                    questionSubmitobj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, QuestionResponse)
                ElseIf State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(CallerAuthenticationResponse) Then
                    questionSubmitobj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
                Else
                    Return
                End If

                questionUserControl.GetQuestionAnswer()

                Dim wsRequest
                If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
                    If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(QuestionResponse) Then
                        wsRequest = New QuestionRequest()
                        wsRequest.QuestionSetCode = questionSubmitobj.QuestionSetCode
                        wsRequest.Version = questionSubmitobj.Version
                    ElseIf State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(CallerAuthenticationResponse) Then
                        wsRequest = New CallerAuthenticationRequest()
                    End If
                End If

                wsRequest.CaseNumber = questionSubmitobj.CaseNumber
                wsRequest.CompanyCode = questionSubmitobj.CompanyCode
                wsRequest.InteractionNumber = questionSubmitobj.InteractionNumber
                wsRequest.Questions = questionSubmitobj.Questions

                Try
                    WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Sub(ByVal c As ClaimRecordingServiceClient)
                            c.Save(wsRequest)
                        End Sub)
                Catch ex As FaultException
                    ThrowWsFaultExceptions(ex)
                    Return
                End Try
                ReturnBackToCallingPage()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#End Region
#Region "Shipping Address View"
#Region "Shipping Address View - Load Data"

    Private Sub ShowShippingAddressView()
        mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexShippingAddress
        PopulateBoPolicyAddress()
        If Not UserControlAddress Is Nothing AndAlso State.PolicyAddressBo IsNot Nothing Then
            UserControlAddress.Bind(State.PolicyAddressBo)
            UserControlAddress.EnableControls(True)
        End If

        RadioButtonBillingAddress.Checked = True
        moAddressController.Visible = True

        ShowDeliveryDates()
    End Sub

    Private Sub ShowDeliveryDates()
        trDeliveryDates.Visible = False
        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        If  oCertificate.Dealer IsNot Nothing Then
            Dim allowDeliveryDateSelection As AttributeValue = oCertificate.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_ALLOW_DELIVERY_DATE_SELECTION).FirstOrDefault
            If allowDeliveryDateSelection IsNot Nothing AndAlso allowDeliveryDateSelection.Value = Codes.YESNO_Y _
               AndAlso State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ShippingAddressResponse) _
               AndAlso DirectCast(State.SubmitWsBaseClaimRecordingResponse, ShippingAddressResponse).AllowDeliveryDateSelection Then
                trDeliveryDates.Visible = True
            End If
        End If
    End Sub

    Private Sub PopulateBoPolicyAddress()
        Dim policyAddress As ShippingAddressResponse

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(ShippingAddressResponse) Then
            policyAddress = DirectCast(State.SubmitWsBaseClaimRecordingResponse, ShippingAddressResponse)

            If State.PolicyAddressBo Is Nothing Then
                State.PolicyAddressBo = New BusinessObjectsNew.Address()
            End If
            With State.PolicyAddressBo
                .CountryId = LookupListNew.GetIdFromCode(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), policyAddress.PolicyAddress.Country)
                .Address1 = policyAddress.PolicyAddress.Address1
                .Address2 = policyAddress.PolicyAddress.Address2
                .Address3 = policyAddress.PolicyAddress.Address3
                .City = policyAddress.PolicyAddress.City
                .PostalCode = policyAddress.PolicyAddress.PostalCode
                .RegionId = LookupListNew.GetIdFromDescription(LookupListNew.DataView(LookupListNew.LK_REGIONS), policyAddress.PolicyAddress.State)
            End With

        End If
    End Sub

    Private Function PopulateShippingAddress() As AddressInfo
        Dim shippingAdd As AddressInfo = New AddressInfo()
        Dim txt As TextBox
        Dim txtPostalCode As TextBox
        Dim ddl As DropDownList

        If (RadioButtonOtherAddress.Checked) Then

            txt = CType(UserControlAddress.FindControl("moAddress1Text"), TextBox)
            shippingAdd.Address1 = txt.Text

            txt = CType(UserControlAddress.FindControl("moAddress2Text"), TextBox)
            shippingAdd.Address2 = txt.Text

            txt = CType(UserControlAddress.FindControl("moAddress3Text"), TextBox)
            shippingAdd.Address3 = txt.Text

            txt = CType(UserControlAddress.FindControl("moCityText"), TextBox)
            shippingAdd.City = txt.Text

            ' Country value
            ddl = CType(UserControlAddress.FindControl("moCountryDrop_WRITE"), DropDownList)
            If (ddl.Items.Count > 0 AndAlso ddl.SelectedIndex > -1) Then
                shippingAdd.Country = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRIES, New Guid(ddl.SelectedValue.ToString()))
            End If

            txtPostalCode = CType(UserControlAddress.FindControl("moPostalText"), TextBox)
            ''validate zip code
            Dim zd As New ZipDistrict()
            zd.CountryId = New Guid(ddl.SelectedValue.ToString())
            zd.ValidateZipCode(txtPostalCode.Text)
            shippingAdd.PostalCode = txtPostalCode.Text

            ' Region/ State Drop down value
            ddl = CType(UserControlAddress.FindControl("moRegionDrop_WRITE"), DropDownList)
            If (ddl.Items.Count > 0 AndAlso ddl.SelectedIndex > -1) Then
                shippingAdd.State = ddl.SelectedItem.ToString()
            End If
        Else
            shippingAdd.Address1 = State.PolicyAddressBo.Address1
            shippingAdd.Address2 = State.PolicyAddressBo.Address2
            shippingAdd.Address3 = State.PolicyAddressBo.Address3
            shippingAdd.City = State.PolicyAddressBo.City
            shippingAdd.PostalCode = State.PolicyAddressBo.PostalCode
            If Not State.PolicyAddressBo.CountryId.IsEmpty Then
                Dim countryBo As New Country(State.PolicyAddressBo.CountryId)
                shippingAdd.Country = countryBo.Code
            End If
            shippingAdd.State = UserControlAddress.RegionText

        End If

        Return shippingAdd
    End Function
#End Region
#Region "Shipping Address View - Check Button Events"
    Protected Sub RadioButtonOtherAddress_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonOtherAddress.CheckedChanged
        If RadioButtonOtherAddress.Checked Then
            If UserControlAddress IsNot Nothing Then
                EnableDisableAddressValidation(moAddressController)

                If State.OtherAddressBo Is Nothing Then
                    State.OtherAddressBo = New BusinessObjectsNew.Address()

                    Dim countryDrop As DropDownList = moAddressController.FindControl("moCountryDrop_WRITE")
                    State.OtherAddressBo.CountryId = New Guid(countryDrop.SelectedValue)

                End If

                Dim oCertificate As Certificate = New Certificate(State.CertificateId)
                UserControlAddress.Bind(State.OtherAddressBo, oCertificate.Product.ClaimProfile)
                UserControlAddress.EnableControls(False, True)
            End If
            UserControlDeliverySlot.Visible = False
        End If
    End Sub

    Protected Sub RadioButtonBillingAddress_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonBillingAddress.CheckedChanged
        If RadioButtonBillingAddress.Checked Then
            If UserControlAddress IsNot Nothing Then
                'save the current values on other address
                If State.OtherAddressBo IsNot Nothing Then
                    UserControlAddress.PopulateBOFromAddressControl(State.OtherAddressBo)
                End If
                UserControlAddress.Bind(State.PolicyAddressBo)
                UserControlAddress.EnableControls(True)
            End If
            UserControlDeliverySlot.Visible = False
        End If
    End Sub


#End Region
#Region "Shipping Address View - Button Click"
    Protected Sub ButtonShippingAddressContinue_Click(sender As Object, e As EventArgs) Handles ButtonShippingAddressContinue.Click
        Try

            Dim shippingAddRequest As ShippingAddressRequest = New ShippingAddressRequest()
            Dim userSelectedShippingAddress As AddressInfo

            If State.SubmitWsBaseClaimRecordingResponse Is Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() IsNot GetType(ShippingAddressResponse) Then
                return
            End If
                shippingAddRequest.CompanyCode = State.SubmitWsBaseClaimRecordingResponse.CompanyCode
                shippingAddRequest.CaseNumber = State.SubmitWsBaseClaimRecordingResponse.CaseNumber
                shippingAddRequest.InteractionNumber = State.SubmitWsBaseClaimRecordingResponse.InteractionNumber

                ' Populate user selected address
                userSelectedShippingAddress = PopulateShippingAddress()
                ' Validate user selected address
                Dim errMsgs As List(Of String) = New List(Of String)
                If Not ValidateShippingAddress(userSelectedShippingAddress, errMsgs) Then
                    'MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_COMPLETE_SHIPPING_ADDRESS_REQUIRED_ERR, True)
                    For Each err As String In errMsgs
                        MasterPage.MessageController.AddError(err, False)
                    Next
                    Return
                End If

                shippingAddRequest.ShippingAddress = userSelectedShippingAddress


                If UserControlDeliverySlot.Visible Then
                    If userSelectedShippingAddress.Country <> UserControlDeliverySlot.CountryCode OrElse userSelectedShippingAddress.PostalCode <> UserControlDeliverySlot.DeliveryAddress.PostalCode Then
                        MasterPage.MessageController.AddError(Message.MSG_ERR_GET_DELIVERY_DATE, True)
                        Return
                    End If
                    If UserControlDeliverySlot.DeliveryDate.HasValue Then
                        shippingAddRequest.DesiredDeliveryDate = UserControlDeliverySlot.DeliveryDate
                        shippingAddRequest.DesiredDeliveryTimeSlot = UserControlDeliverySlot.DeliverySlot
                    End If
                End If
                Try
                    Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Function(ByVal c As ClaimRecordingServiceClient)
                            Return c.Submit(shippingAddRequest)
                        End Function)
                    UpdateStateAfterShippingAddressResponse(wsResponse,userSelectedshippingAddress.City,userSelectedshippingAddress.PostalCode)
                    
                Catch ex As FaultException
                    ThrowWsFaultExceptions(ex)
                    Return
                End Try
                DisplayNextView()
           
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub UpdateStateAfterShippingAddressResponse( webResponse as BaseClaimRecordingResponse,city as string ,postalCode as string)
        If webResponse IsNot Nothing Then
            State.SubmitWsBaseClaimRecordingResponse = webResponse
            State.serviceCentreCity = city
            State.serviceCentrePostalCode = PostalCode
        End If
    End Sub

    <Obsolete>
    Protected Sub btnGetDeliveryDate_Click(sender As Object, e As EventArgs) Handles btnGetDeliveryDate.Click
        Try

            Dim userSelectedShippingAddress As AddressInfo
            ' Populate user selected address
            userSelectedShippingAddress = PopulateShippingAddress()

            Dim countryBo As New Country(LookupListNew.GetIdFromCode(LookupListNew.LK_COUNTRIES, userSelectedShippingAddress.Country))
            If countryBo Is Nothing OrElse String.IsNullOrWhiteSpace(countryBo.Code) OrElse String.IsNullOrWhiteSpace(userSelectedShippingAddress.PostalCode) Then
                MasterPage.MessageController.AddError(Message.MSG_ERR_COUNTRY_POSTAL_MANDATORY, True)
                return
            End If

            If countryBo.DefaultSCId.IsEmpty Then
                MasterPage.MessageController.AddError(Message.MSG_ERR_DEFAULT_SERVICE_CENTER, True)
                return
            End If


            Dim defaultServiceCenter As ServiceCenter
            If State.ClaimBo Is Nothing AndAlso Not String.IsNullOrEmpty(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber) Then
                Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId

                For Each cid As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies()
                    Dim comobj As New Company(cid)
                    If comobj.Code = State.SubmitWsBaseClaimRecordingResponse.CompanyCode Then
                        companyId = cid
                    End If
                Next

                State.ClaimBo = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber, companyId)
                defaultServiceCenter = New ServiceCenter(State.ClaimBo.ServiceCenterId)
            Else
                defaultServiceCenter = New ServiceCenter(countryBo.DefaultSCId)
            End If

            With UserControlDeliverySlot
                .CountryCode = userSelectedShippingAddress.Country
                .ServiceCenter = defaultServiceCenter.Code
                .CourierCode = State.LogisticsOption.DeliveryOptions.CourierCode
                .CourierProductCode = State.LogisticsOption.DeliveryOptions.CourierProductCode
                .DeliveryAddress = New UserControlDeliverySlot.DeliveryAddressInfo() With {
                    .CountryCode = userSelectedShippingAddress.Country,
                    .RegionShortDesc = userSelectedShippingAddress.State,
                    .PostalCode = userSelectedShippingAddress.PostalCode,
                    .City = userSelectedShippingAddress.City,
                    .Address1 = userSelectedShippingAddress.Address1,
                    .Address2 = userSelectedShippingAddress.Address2,
                    .Address3 = userSelectedShippingAddress.Address3
                    }
                .PopulateDeliveryDate()
            End With

            UserControlDeliverySlot.Visible = True

        Catch ex As ThreadAbortException
            HandleErrors(ex, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region
#Region "Shipping Address View - Other Function"
    Private Function ValidateShippingAddress(shippingAdd As AddressInfo, ByRef errMsgs As List(Of String)) As Boolean
        Dim isValidAddress As Boolean = True

        If (shippingAdd.GetType() Is GetType(AddressInfo)) Then
            If (String.IsNullOrEmpty(shippingAdd.Address1)) Then
                isValidAddress = False
                errMsgs.Add(TranslationBase.TranslateLabelOrMessage("GUI_COMPLETE_SHIPPING_ADDRESS_REQUIRED_ERR"))
            ElseIf shippingAdd.Address1.Length > 100 Then
                isValidAddress = False
                errMsgs.Add(TranslationBase.TranslateLabelOrMessage("ADDRESS1") & ":" & TranslationBase.TranslateLabelOrMessage("MSG_VALID_STRING_LENGTH_ERR"))
            End If

            If Not String.IsNullOrEmpty(shippingAdd.Address2) AndAlso shippingAdd.Address2.Length > 100 Then
                isValidAddress = False
                errMsgs.Add(TranslationBase.TranslateLabelOrMessage("ADDRESS2") & ":" & TranslationBase.TranslateLabelOrMessage("MSG_VALID_STRING_LENGTH_ERR"))
            End If

            If Not String.IsNullOrEmpty(shippingAdd.Address3)  AndAlso shippingAdd.Address3.Length > 100 Then
                isValidAddress = False
                errMsgs.Add(TranslationBase.TranslateLabelOrMessage("ADDRESS3") & ":" & TranslationBase.TranslateLabelOrMessage("MSG_VALID_STRING_LENGTH_ERR"))
            End If
        End If

        Return isValidAddress
    End Function



#End Region
#End Region
#Region "TroubleShooting View"
#Region "TroubleShooting -  Other Functions"
    Private Sub ShowTroubleShootingView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing  AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(TroubleShootingResponse) Then
                Dim tbShooting As TroubleShootingResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, TroubleShootingResponse)
                If  tbShooting IsNot Nothing AndAlso tbShooting.ClaimRecordingMessages IsNot Nothing Then
                    DisplayWsErrorMessage(tbShooting.ClaimRecordingMessages)
                End If
                rdoTbYes.Text = DoubleSpaceString & TranslationBase.TranslateLabelOrMessage("YES")
                rdoTbNo.Text = DoubleSpaceString & TranslationBase.TranslateLabelOrMessage("NO")
                rdoTbSkipped.Text = DoubleSpaceString & TranslationBase.TranslateLabelOrMessage("SKIPPED")
                ShowTroubleShootingOption(tbShooting.TroubleShootingOption)
                mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexTroubleShooting
                ControlMgr.SetEnableControl(Me, ButtonTBcontinue, True)
            End If
    End Sub
    Private Sub ShowTroubleShootingOption(ByVal tbOption As TroubleShootingOptionFlag)
        If tbOption = TroubleShootingOptionFlag.Troubleshootingoptional Then
            ControlMgr.SetVisibleControl(Me, rdoTbSkipped, True)
        Else
            ControlMgr.SetVisibleControl(Me, rdoTbSkipped, False)
        End If
    End Sub
#End Region

#Region "TroubleShooting - Button Click"
    Protected Sub ButtonTBcontinue_Click(sender As Object, e As EventArgs) Handles ButtonTBcontinue.Click
        Try
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, ButtonTBcontinue, False)
            Dim tbOptionSelected As TroubleShootingResolutionFlag
            If rdoTbYes.Checked Then
                tbOptionSelected = TroubleShootingResolutionFlag.Yes
            ElseIf rdoTbNo.Checked Then
                tbOptionSelected = TroubleShootingResolutionFlag.No
            Else
                tbOptionSelected = TroubleShootingResolutionFlag.Skipped
            End If
            SendTroubleShootingResponse(tbOptionSelected)
            DisplayNextView()
        Catch ex As Exception
            ControlMgr.SetEnableControl(Me, ButtonTBcontinue, True)
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "TroubleShooting -  Web Service Call"
    Private Sub SendTroubleShootingResponse(ByVal tbOptionSelected As TroubleShootingResolutionFlag)
        Dim wsRequest As TroubleShootingRequest = New TroubleShootingRequest()
        Dim tbShootingResponse As TroubleShootingResponse

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(TroubleShootingResponse) Then
            tbShootingResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, TroubleShootingResponse)
        Else
            return
        End If

        wsRequest.CaseNumber = tbShootingResponse.CaseNumber
        wsRequest.CompanyCode = tbShootingResponse.CompanyCode
        wsRequest.InteractionNumber = tbShootingResponse.InteractionNumber
        wsRequest.IssueResolved = tbOptionSelected


        Try
            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
        End Try

    End Sub
#End Region
#End Region
#Region "Best Replacement Device View"

#Region "Best Replacement Device -  Other Functions"
    Private Sub ShowBestReplacementDeviceView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(BestReplacementResponse) Then
            Dim bestReplacement As BestReplacementResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, BestReplacementResponse)
            If  bestReplacement IsNot Nothing AndAlso  bestReplacement.ClaimRecordingMessages IsNot Nothing Then
                DisplayWsErrorMessage(bestReplacement.ClaimRecordingMessages)
            End If

            PopulateDeviceSelectionGrid()
            mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexBestReplacementDevice
        End If
    End Sub
    Private Sub DisplayWsErrorMessage(ByVal recordingMessages As ClaimRecordingMessage())
        For Each recordingMessage As ClaimRecordingMessage In recordingMessages
            MasterPage.MessageController.AddInformation(recordingMessage.MessageText, True)
        Next
    End Sub
    Private Sub PopulateDeviceSelectionGrid()
        Dim bestReplacement As BestReplacementResponse
        ''GridViewDeviceSelection.AutoGenerateColumns = False
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(BestReplacementResponse) Then
            bestReplacement = DirectCast(State.SubmitWsBaseClaimRecordingResponse, BestReplacementResponse)
        Else
            return
        End If



        If bestReplacement.BestReplacementOptins Is Nothing OrElse bestReplacement.BestReplacementOptins.Count = 0 Then
            ControlMgr.SetVisibleControl(Me, rep, False)

            ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, True)
            lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
        Else


            Dim replacements As IOrderedEnumerable(Of BestReplacementDeviceInfo) = bestReplacement.BestReplacementOptins _
                    .Where(Function(br) br.ClaimedDevice OrElse br.InventoryQuantity > 0).ToArray() _
                    .OrderBy("ClaimedDevice", LinqExtentions.SortDirection.Descending) _
                    .ThenBy(Function(d) d.Priority)


            If replacements Is Nothing OrElse replacements.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, rep, False)
                ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, True)
                lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            Else
                ControlMgr.SetVisibleControl(Me, rep, True)
                ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, False)

                ''US 290829 - Binding Repeater instead of gridview
                rep.DataSource = replacements
                rep.DataBind()

                lblRecordCount.Text = String.Format("{0} {1}", replacements.Count.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If

        ControlMgr.SetVisibleControl(Me, trPageSize, True)

    End Sub


    Private Sub UpdateDeviceSelection(vendorSku As String)
        Dim bestReplacement As BestReplacementResponse

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(BestReplacementResponse) Then
            bestReplacement = DirectCast(State.SubmitWsBaseClaimRecordingResponse, BestReplacementResponse)
        Else
            Return
        End If

        If bestReplacement.BestReplacementOptins IsNot Nothing AndAlso  bestReplacement.BestReplacementOptins.Count <> 0 Then
            State.BestReplacementDeviceSelected = bestReplacement.BestReplacementOptins.FirstOrDefault(Function(q) q.VendorSku = vendorSku)
        End If

    End Sub

#End Region

#Region "Best Replacement Device - Button Click"
    Protected Sub btnBestReplacementNotSelectedContinue_Click(sender As Object, e As EventArgs) Handles btnBestReplacementNotSelectedContinue.Click
        ContinueEmptyReplacementSelection()
    End Sub

    Private Sub ContinueEmptyReplacementSelection()
        ''Dim isEnable As Boolean = btnBestReplacementSelectedContinue.Enabled
        Try
            'Disable all button once response is stored, except back button
            ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, False)
            State.BestReplacementDeviceSelected = Nothing
            SendSelectDeviceData()
            DisplayNextView()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, True)
        End Try
    End Sub

#End Region
#Region "Best Replacement Device -  Web Service Call"
    Private Sub SendSelectDeviceData()
        Dim wsRequest As BestReplacementSelectionRequest = New BestReplacementSelectionRequest()
        Dim bestReplacement As BestReplacementResponse

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(BestReplacementResponse) Then
            bestReplacement = DirectCast(State.SubmitWsBaseClaimRecordingResponse, BestReplacementResponse)
        Else
            Return
        End If

        wsRequest.CaseNumber = bestReplacement.CaseNumber
        wsRequest.CompanyCode = bestReplacement.CompanyCode
        wsRequest.InteractionNumber = bestReplacement.InteractionNumber

        If State.BestReplacementDeviceSelected Is Nothing Then
            wsRequest.IsBestReplacementDeviceSelected = False
            If Not bestReplacement.OriginalDevice.ClaimedDevice Then
                bestReplacement.OriginalDevice.ClaimedDevice = True
            End If

            wsRequest.ReplacementDevice = bestReplacement.OriginalDevice
        Else
            wsRequest.IsBestReplacementDeviceSelected = True
            wsRequest.ReplacementDevice = State.BestReplacementDeviceSelected
        End If

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
        End Try
    End Sub
#End Region
#End Region


#Region "rep related"
    ''US 290829 - Using Repeater

    Protected Sub rep_OnInit(sender As Object, e As EventArgs)
        _makeText = TranslationBase.TranslateLabelOrMessage("MAKE")
        _modelText = TranslationBase.TranslateLabelOrMessage("MODEL")
        _skuText = TranslationBase.TranslateLabelOrMessage("SKU")
        _colorText = TranslationBase.TranslateLabelOrMessage("COLOR")
        _memoryText = TranslationBase.TranslateLabelOrMessage("MEMORY")
        _quantityText = TranslationBase.TranslateLabelOrMessage("QUANTITY")
        _outOfStockText = TranslationBase.TranslateLabelOrMessage("OUT_OF_STOCK")
        _selectText = TranslationBase.TranslateLabelOrMessage("SELECT")
        _deviceShippedText = TranslationBase.TranslateLabelOrMessage("DEVICE_WILL_BE_ORDERED")
        _currentDeviceText = TranslationBase.TranslateLabelOrMessage("CURRENT_DEVICE")
    End Sub


    Protected Sub rep_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If Not e.Item.ItemType = ListItemType.Item OrElse Not e.Item.ItemType = ListItemType.AlternatingItem Then
            Return
        End if
            Dim panelCurrentDevice As Panel = DirectCast(e.Item.FindControl("panelCurrentDevice"), Panel)
            Dim lblDeviceDescription1 As Label = DirectCast(e.Item.FindControl("lblDeviceDescription1"), Label)
            Dim lblDeviceDescription2 As Label = DirectCast(e.Item.FindControl("lblDeviceDescription2"), Label)
            Dim lblQuantity As Label = DirectCast(e.Item.FindControl("lblQuantity"), Label)
            Dim lblDeviceShipped As Label = DirectCast(e.Item.FindControl("lblDeviceShipped"), Label)
            Dim ltlDeviceSeparator As Literal = DirectCast(e.Item.FindControl("ltlDeviceSeparator"), Literal)
            Dim btnSelectDevice As LinkButton = DirectCast(e.Item.FindControl("btnSelectDevice"), LinkButton)
            Dim lblCurrentDevice As Label = DirectCast(e.Item.FindControl("lblCurrentDevice"), Label)


            Dim carrier As Object = DataBinder.Eval(e.Item.DataItem, "Carrier")
            Dim make As Object = DataBinder.Eval(e.Item.DataItem, "Manufacturer")
            Dim model As Object = DataBinder.Eval(e.Item.DataItem, "Model")
            Dim color As Object = DataBinder.Eval(e.Item.DataItem, "Color")
            Dim memory As Object = DataBinder.Eval(e.Item.DataItem, "Capacity")
            Dim quantity As Object = DataBinder.Eval(e.Item.DataItem, "InventoryQuantity")
            Dim modelFamily As Object = DataBinder.Eval(e.Item.DataItem, "ModelFamily")
            Dim sku As Object = DataBinder.Eval(e.Item.DataItem, "VendorSku")
            Dim claimedDevice As Boolean = Boolean.Parse(DataBinder.Eval(e.Item.DataItem, "ClaimedDevice").ToString())

            Dim description1 As String = $"{carrier} {make} {modelFamily} {memory} {color}"
            Dim description2 As String = $"{_makeText}: {make}, {_modelText}: {model}, {_skuText}: {sku}, {_memoryText}: {memory}, {_colorText}: {color}"

            lblDeviceDescription1.Text = description1
            lblDeviceDescription1.CssClass = If(claimedDevice, "successText", "normalText")

            lblDeviceDescription2.Text = description2

            lblQuantity.Text = If(quantity = 0, $"{_outOfStockText}", $"{_quantityText}: {quantity}")
            lblQuantity.CssClass = If(quantity = 0, "failText", "successText")

            lblDeviceShipped.Text = If(quantity = 0, _deviceShippedText, String.Empty)

            ControlMgr.SetVisibleControl(Me, panelCurrentDevice, claimedDevice)
            lblCurrentDevice.Text = If(claimedDevice, _currentDeviceText, String.Empty)

            btnSelectDevice.Text = _selectText
            btnSelectDevice.CommandArgument = If(quantity = 0, String.Empty, sku)

            'Gets the proper class for the hr element --> bolder when claimed device
            Dim separatorClass = If(claimedDevice, "claimedDeviceSeparator", "standardDeviceSeparator")

            ltlDeviceSeparator.Text = $"<hr noshade class=""{separatorClass}"""

    End Sub
    Protected Sub rep_OnItemCommand(sender As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName.Equals(CommandSelectDevice, StringComparison.InvariantCultureIgnoreCase) Then
                If String.IsNullOrEmpty(e.CommandArgument.ToString()) Then
                    ContinueEmptyReplacementSelection()
                Else

                    Dim isEnable As Boolean = btnBestReplacementNotSelectedContinue.Enabled
                    Try
                        UpdateDeviceSelection(e.CommandArgument.ToString())

                        If State.BestReplacementDeviceSelected Is Nothing Then
                            MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_A_DEVICE, True)
                            Return
                        End If
                        'Disable all button once response is stored, except back button
                        ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, False)
                        SendSelectDeviceData()
                        DisplayNextView()
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)

                        ControlMgr.SetEnableControl(Me, btnBestReplacementNotSelectedContinue, isEnable)
                    End Try
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Fulfillment Options View"
    Private Const GridFulfillmentOptionsColFoRdoIdx As Integer = 0
    Private Const GridFulfillmentOptionsRdoFoCtrl As String = "rdoFulfillmentOptions"
    Private Const GridFulfillmentOptionslblCodeCtrl As String = "lblFulfillmentOptionCode"
    Private Const GridViewFulfillmentOptionFeeName As String = "GridViewFulfillmentOptionFee"

    Private Const GridViewFulfillmentOptionFeeColServiceTypeCodeIdx As Integer = 0
    Private Const GridViewFulfillmentOptionFeeColNetPriceIdx As Integer = 1
#Region "Fulfillment Options -  Other Functions"
    Protected Sub rdoFulfillmentOptions_CheckedChanged(sender As Object, e As EventArgs)
        'De-select all radio button in the grid
        DeselectRadioButtonGridview(GridViewFulfillmentOptions, GridFulfillmentOptionsRdoFoCtrl)
        'check the radiobutton which is checked
        Dim senderRb As RadioButton = sender
        senderRb.Checked = True

        'display the question set associated with selected option
        Dim wsResponse As FulfillmentOptionsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, FulfillmentOptionsResponse)
        ShowFulfillmentOptionQuestionSet(wsResponse)

    End Sub

    Private Sub ShowFulfillmentOptionQuestionSet(response As FulfillmentOptionsResponse)

        Dim optionCode As String = GetFulfillmentProfileCode() 'get selected option

        If Not String.IsNullOrWhiteSpace(optionCode) AndAlso  response.QuestionsByStage.ContainsKey(optionCode) Then
                fulfillmentOptionQuestions.SetQuestionTitle(TranslationBase.TranslateLabelOrMessage("QUESTION_SET"))
                fulfillmentOptionQuestions.QuestionDataSource = response.QuestionsByStage(optionCode)
                fulfillmentOptionQuestions.QuestionDataBind()
                ControlMgr.SetVisibleControl(Me, fulfillmentOptionQuestions, True)
                Return
        End If
        ControlMgr.SetVisibleControl(Me, fulfillmentOptionQuestions, False)
    End Sub

    Private Sub ShowFulfillmentOptionsView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
            If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(FulfillmentOptionsResponse) Then
                Dim wsResponse As FulfillmentOptionsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, FulfillmentOptionsResponse)
                If  wsResponse IsNot Nothing AndAlso  wsResponse.ClaimRecordingMessages IsNot Nothing Then
                    DisplayWsErrorMessage(wsResponse.ClaimRecordingMessages)
                End If
                PopulateFulfillmentOptionsGrid()

                'show question set associated with the selected option
                ShowFulfillmentOptionQuestionSet(wsResponse)

                mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexFulfillmentOptions
            End If
        End If
    End Sub

    Private Sub ShowDynamicFulfillmentView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DynamicFulfillmentResponse) Then
                mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexDynamicFulfillment
                Dim wsResponse As DynamicFulfillmentResponse = State.SubmitWsBaseClaimRecordingResponse
                Dim dfControl As DynamicFulfillmentUI = Page.LoadControl("~/Common/DynamicFulfillmentUI.ascx")
                dfControl.SourceSystem = "Elita"
                dfControl.ApiKey = wsResponse.ApiKey
                dfControl.SubscriptionKey = wsResponse.SubscriptionKey
                dfControl.CssUri = wsResponse.CssUri
                dfControl.ScriptUri = wsResponse.ScriptUri
                dfControl.ClaimNumber = wsResponse.ClaimKey
                phDynamicFulfillmentUI.Controls.Add(dfControl)
        End If
    End Sub
    Private Sub PopulateFulfillmentOptionsGrid()
        Dim wsResponse As FulfillmentOptionsResponse
        If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(FulfillmentOptionsResponse) Then
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, FulfillmentOptionsResponse)

            If wsResponse.Options Is Nothing OrElse wsResponse.Options.Count = 0 Then
                lblFulfillmentOptionNoRecordsFound.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
                ControlMgr.SetEnableControl(Me, lblFulfillmentOptionNoRecordsFound, True)
                ControlMgr.SetVisibleControl(Me, GridViewFulfillmentOptions, False)
            Else
                ControlMgr.SetVisibleControl(Me, GridViewFulfillmentOptions, True)
                ControlMgr.SetEnableControl(Me, lblFulfillmentOptionNoRecordsFound, False)
                GridViewFulfillmentOptions.DataSource = wsResponse.Options.OrderBy("Order", LinqExtentions.SortDirection.Ascending)
                GridViewFulfillmentOptions.DataBind()
            End If
        End If
    End Sub
    Private Function GetFulfillmentProfileCode() As String
        Dim gvr As GridViewRow
        Dim fulfillmentProfileCode As String
        For Each gvr In GridViewFulfillmentOptions.Rows
            Dim rb As RadioButton
            rb = CType(gvr.Cells(GridFulfillmentOptionsColFoRdoIdx).FindControl(GridFulfillmentOptionsRdoFoCtrl), RadioButton)
            If rb.Checked Then
                Dim lb As Label
                lb = CType(gvr.Cells(GridFulfillmentOptionsColFoRdoIdx).FindControl(GridFulfillmentOptionslblCodeCtrl), Label)
                fulfillmentProfileCode = lb.Text
                return fulfillmentProfileCode
            End If
        Next
        Return fulfillmentProfileCode
    End Function

    Private Function SendReponseFulfillmentOptions(ByVal fulfillmentProfileCode As String) As Boolean

        Dim wsRequest As FulfillmentOptionsRequest = New FulfillmentOptionsRequest()
        Dim wsPreviousResponse As FulfillmentOptionsResponse

        If State.SubmitWsBaseClaimRecordingResponse.GetType() IsNot GetType(FulfillmentOptionsResponse) Then
         Return False
        End If
        wsPreviousResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, FulfillmentOptionsResponse)
        State.FulfillmentOption = wsPreviousResponse.Options.FirstOrDefault(Function(opt) opt.Code = fulfillmentProfileCode)


        wsRequest.CaseNumber = wsPreviousResponse.CaseNumber
        wsRequest.CompanyCode = wsPreviousResponse.CompanyCode
        wsRequest.InteractionNumber = wsPreviousResponse.InteractionNumber
        wsRequest.OptionCode = fulfillmentProfileCode
        ' for questions
        If fulfillmentOptionQuestions.Visible Then

            fulfillmentOptionQuestions.GetQuestionAnswer()

            If questionUserControl.ErrAnswerMandatory IsNot Nothing AndAlso Not String.IsNullOrEmpty(questionUserControl.ErrAnswerMandatory.ToString()) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_IS_REQUIRED_ERR, True)
                Return False
            ElseIf questionUserControl.ErrorQuestionCodes IsNot Nothing AndAlso Not String.IsNullOrEmpty(questionUserControl.ErrorQuestionCodes.ToString()) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
                Return False
            ElseIf questionUserControl.ErrTextAnswerLength IsNot Nothing AndAlso Not String.IsNullOrEmpty(questionUserControl.ErrTextAnswerLength.ToString()) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_LENGTH_TO_QUESTION_TOO_LONG_ERR, True)
                Return False
            ElseIf fulfillmentOptionQuestions.ErrorQuestionValidation IsNot Nothing AndAlso Not String.IsNullOrEmpty(fulfillmentOptionQuestions.ErrorQuestionValidation.ToString()) Then
                MasterPage.MessageController.AddError(fulfillmentOptionQuestions.ErrorQuestionValidation.ToString(), false)
                Return False
            End If

            wsRequest.QuestionVersion = 1
            wsRequest.QuestionSetCode = wsPreviousResponse.Options.FirstOrDefault(Function(opt) opt.Code = fulfillmentProfileCode).QuestionSetCode
            If wsPreviousResponse.QuestionsByStage IsNot Nothing Then
                wsRequest.Questions = wsPreviousResponse.QuestionsByStage(fulfillmentProfileCode)
            End If

        End If

        Try
            'State.SubmitWsBaseClaimRecordingResponse = LoadLogisticsOptions()
            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
        End Try

        Return True
    End Function
#End Region
#Region "Fulfillment Options - Button Click"
    Protected Sub btnFulfillmentOptionsContinue_Click(sender As Object, e As EventArgs) Handles btnFulfillmentOptionsContinue.Click
        Try
            Dim fulfillmentProfileCode As String
            fulfillmentProfileCode = GetFulfillmentProfileCode()

            If String.IsNullOrWhiteSpace(fulfillmentProfileCode) Then
                MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_A_FULFILLMENT_OPTIONS, True)
                Return
            End If

            If SendReponseFulfillmentOptions(fulfillmentProfileCode)  Then
                'Disable all button once response is stored, except back button
                ControlMgr.SetEnableControl(Me, btnFulfillmentOptionsContinue, False)
                'display next view
                DisplayNextView()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Fulfillment Options -  Grid Functions"
    Protected Sub GridViewFulfillmentOptions_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewFulfillmentOptions.RowDataBound

        Dim source As IEnumerable(Of FulfillmentOption) = CType(sender, GridView).DataSource
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim fulfillmentOptionItem As FulfillmentOption = source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, FulfillmentOption).Code)

            ' Show Fee Grid
            If  fulfillmentOptionItem IsNot Nothing AndAlso  fulfillmentOptionItem.Prices IsNot Nothing Then
                Dim gridViewFulfillmentOptionFee As GridView = CType(e.Row.FindControl(GridViewFulfillmentOptionFeeName), GridView)
                ControlMgr.SetVisibleControl(Me, gridViewFulfillmentOptionFee, True)
                TranslateGridHeader(gridViewFulfillmentOptionFee)
                gridViewFulfillmentOptionFee.DataSource = fulfillmentOptionItem.Prices
                gridViewFulfillmentOptionFee.DataBind()
            End If
        End If
    End Sub
    Protected Sub GridViewFulfillmentOptionFee_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        Try
            Dim fulfillmentOptionPrice As FulfillmentOptionPrice = CType(e.Row.DataItem, FulfillmentOptionPrice)
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                Dim fee As String = LookupListNew.GetDescrionFromListCode(LookupListNew.LK_SERVICE_TYPE_NEW, fulfillmentOptionPrice.ServiceTypeCode)
                If String.IsNullOrWhiteSpace(fee) Then
                    fee = fulfillmentOptionPrice.ServiceTypeCode
                End If
                e.Row.Cells(GridViewFulfillmentOptionFeeColServiceTypeCodeIdx).Text = fee
                e.Row.Cells(GridViewFulfillmentOptionFeeColNetPriceIdx).Text = GetAmountFormattedString(fulfillmentOptionPrice.NetPrice)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#End Region
#Region "Logistics Options View"
    Private Const GridLogisticsOptionsRdoCtrl As String = "rdoLogisticsOption"
    Private Const GridLoCodeLblCtrl As String = "lblLogisticsOptionCode"
    Private Const GridLoStoreNumberTxtCtrl As String = "txtStoreNumber"
    Private Const GridLoServiceCenterCodeTxtCtrl As String = "txtServiceCenterCode"
    Private Const GridLoStoreNumberLblCtrl As String = "lblStoreNumber"
    Private Const GridLoServiceCenterCodeLblCtrl As String = "moServiceCenterCodeLabel"
    Private Const GridLoServiceCenterNameLblCtrl As String = "moServiceCenterNameLabel"
    Private Const GridLoServiceCenterSelectedLblCtrl As String = "moServiceCenterSelectedLabel"
    Private Const GridLoShippingAddressLblCtrl As String = "lblLoShippingAddress"
    Private Const GridLoAddressCtrl As String = "ucAddressControllerLogisticsOptions"
    Private Const GridLoServiceCenterCtrl As String = "ucServiceCenterUserControl"
    Private Const GridLoDeliveryDateLblCtrl As String = "lblDeliveryDate"
    Private Const GridLoEstimateDeliveryDateBtnCtrl As String = "btnEstimateDeliveryDate"
    Private Const LogisticsOptionsQuestionsCtrl As String = "logisticsOptionsQuestions"
    Private Const LogisticsOptionsEstimateDeliveryDateCtrl As String = "UCDeliverySlotLogisticOptions"
    Private Const LogisticsOptionsBankInfoCtrl As String = "moBankInfoController"
    Private Const LogisticsOptionsServiceCenterCodeTxtCtrl As String = "txtServiceCenterCode"
    Private Const LogisticsOptionsServiceCenterNameTxtCtrl As String = "txtServiceCenterName"
    Private Const GridLoServiceCenterTr As String = "trServiceCenter"
    Private Const GridBankInfoLblCtrl As String = "moBankInfoLabel"
    Private Const GridLoBankInfoTr As String = "trBankInfo"
    Private Const LogisticsOptionsCheckInfoCtrl As String = "moCustomerAddressController"
    Private Const GridLoCheckInfoTr As String = "trChequeInfo"
    Private Const GridPayableToTxtCtrl As String = "moPayableToText"
    Private Const GridPayableToLblCtrl As String = "moPayableToLabel"
    Private Const GridCheckInfoLblCtrl As String = "moChequeInfoLabel"
    Private Const GridLoShippingAddressTr As String = "trShippingAddress"
    Private Const GridLoDeliveryOptionsTr As String = "trDeliveryOptions"
    'KDDI
    Private Const ValidateAddressButton As String = "btnValidate_Address"

    Private Const GridLoColLoRdoIdx As Integer = 0
    Private Const GridLoColLoDetailIdx As Integer = 1
    Private Sub ShowLogisticsOptionsView()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(LogisticStagesResponse) Then
                Dim wsResponse As LogisticStagesResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
                If  wsResponse IsNot Nothing AndAlso wsResponse.ClaimRecordingMessages IsNot Nothing Then
                    DisplayWsErrorMessage(wsResponse.ClaimRecordingMessages)
                End If
                PopulateLogisticsOptionsGrid()
                mvClaimsRecording.ActiveViewIndex = ClaimRecordingViewIndexLogisticsOptions
            
        End If
    End Sub
#Region "Logistics Options -  Other Functions"
    Protected Sub rdoLogisticsOption_CheckedChanged(sender As Object, e As EventArgs)
        'De-select all radio button in the grid
        DeselectRadioButtonGridview(GridViewLogisticsOptions, GridLogisticsOptionsRdoCtrl)
        'check the radiobutton which is checked
        Dim senderRb As RadioButton = sender
        senderRb.Checked = True
        ' get the selected device into the state
        EnableControlinGridview(GridViewLogisticsOptions)
        State.IsExpeditedBtnClicked = False

        ControlMgr.SetEnableControl(Me, btnLogisticsOptionsContinue, True)
    End Sub
    Private Sub EnableControlinGridview(ByVal gridViewTarget As GridView)

        Dim wsResponse As LogisticStagesResponse
        wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
        Dim logisticsStage As LogisticStage
        logisticsStage = wsResponse.Stages(State.LogisticsStage)

        For i As Integer = 0 To gridViewTarget.Rows.Count - 1
            Dim lb As Label
            lb = CType(gridViewTarget.Rows(i).FindControl(GridLoCodeLblCtrl), Label)
            Dim logisticsOptionItem As LogisticOption = logisticsStage.Options.FirstOrDefault(Function(q) q.Code = lb.Text)


            Dim rb As RadioButton
            rb = CType(gridViewTarget.Rows(i).FindControl(GridLogisticsOptionsRdoCtrl), RadioButton)
            ControlMgr.SetEnableControl(Me, rb, True)
            Dim isEnableControl As Boolean = rb.Checked

            Dim moAddressController As UserControlAddress_New = CType(gridViewTarget.Rows(i).FindControl(GridLoAddressCtrl), UserControlAddress_New)

            EnableDisableAddress(isEnableControl)

            ' Enable all control 

            logisticsOptionItem.Selected = isEnableControl

            ' Logistics Options - Address
            If logisticsOptionItem IsNot Nothing _
               AndAlso (logisticsOptionItem.Type = LogisticOptionType.CustomerAddress OrElse logisticsOptionItem.Type = LogisticOptionType.DealerBranchAddress) Then
                moAddressController.EnableControls(Not isEnableControl, True)
                If logisticsStage.Code = "RV" AndAlso (logisticsOptionItem.Code = "ST" OrElse logisticsOptionItem.Code = "E") AndAlso logisticsOptionItem.Type = LogisticOptionType.CustomerAddress Then
                    moAddressController.EnableControls(True, True)
                    Dim btnValidateAddress As Button = moAddressController.FindControl(ValidateAddressButton)
                    ControlMgr.SetVisibleControl(Me, btnValidateAddress, False)
                End If

                If logisticsOptionItem.Type = LogisticOptionType.DealerBranchAddress Then
                    Dim txtStoreNumber As TextBox = CType(gridViewTarget.Rows(i).FindControl(GridLoStoreNumberTxtCtrl), TextBox)
                    ControlMgr.SetEnableControl(Me, txtStoreNumber, isEnableControl)
                End If
            End If

            ' Delivery Options and Service Center
            SetDeliveryAndServiceCenterOption(logisticsOptionItem,gridViewTarget,i,isEnableControl)
        Next
    End Sub
    Private Sub EnableDisableAddress(isEnableControl as Boolean)
        If (isEnableControl) Then
            EnableDisableAddressValidation(moAddressController)
        Else
            Dim btnValidateAddress As Button = moAddressController.FindControl(ValidateAddressButton)
            ControlMgr.SetVisibleControl(Me, btnValidateAddress, False)
        End If
    End Sub
    Private Sub SetDeliveryAndServiceCenterOption(logisticsOptionItem As LogisticOption, gridViewTarget as GridView, gridRowNumber As Integer,isEnableControl As Boolean)

        If logisticsOptionItem IsNot Nothing _
           AndAlso Not logisticsOptionItem.DeliveryOptions Is Nothing _
           AndAlso logisticsOptionItem.DeliveryOptions.DisplayEstimatedDeliveryDate Then

            Dim btnEstimateDeliveryDate As Button = CType(gridViewTarget.Rows(gridRowNumber).FindControl(GridLoEstimateDeliveryDateBtnCtrl), Button)
            ControlMgr.SetEnableControl(Me, btnEstimateDeliveryDate, isEnableControl)

            If isEnableControl Then
                State.LogisticsOption = logisticsOptionItem
            End If
        End If

        ' Service Center
        Dim serviceCenterTableRow As HtmlTableRow = CType(gridViewTarget.Rows(gridRowNumber).FindControl(GridLoServiceCenterTr), HtmlTableRow)
        If logisticsOptionItem IsNot Nothing _
           AndAlso logisticsOptionItem.Type = LogisticOptionType.ServiceCenter Then
            ControlMgr.SetVisibleControl(Me, serviceCenterTableRow, True)
        Else
            ControlMgr.SetVisibleControl(Me, serviceCenterTableRow, False)
        End If
    End Sub

    Private Function ConvertToAddressControllerField(ByVal sourceAddress As ClaimRecordingService.Address) As BusinessObjectsNew.Address

        Dim convertAddress As New BusinessObjectsNew.Address()

        convertAddress.CountryId = LookupListNew.GetIdFromCode(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), sourceAddress.Country)
        convertAddress.Address1 = sourceAddress.Address1
        convertAddress.Address2 = sourceAddress.Address2
        convertAddress.Address3 = sourceAddress.Address3
        convertAddress.City = sourceAddress.City
        convertAddress.PostalCode = sourceAddress.PostalCode
        convertAddress.RegionId = LookupListNew.GetIdFromDescription(LookupListNew.DataView(LookupListNew.LK_REGIONS), sourceAddress.State)
        Return convertAddress
    End Function
    Private Function PopulateAddressFromAddressController(ByVal addressCtrl As UserControlAddress_New) As ClaimRecordingService.Address
        If addressCtrl Is Nothing Then
            Throw New ArgumentNullException(NameOf(addressCtrl))
        End If

        Dim shippingAdd As ClaimRecordingService.Address = New ClaimRecordingService.Address()
        Dim txt As TextBox
        Dim txtPostalCode As TextBox
        Dim ddl As DropDownList
        txt = CType(addressCtrl.FindControl("moAddress1Text"), TextBox)
        shippingAdd.Address1 = txt.Text
        txt = CType(addressCtrl.FindControl("moAddress2Text"), TextBox)
        shippingAdd.Address2 = txt.Text
        txt = CType(addressCtrl.FindControl("moAddress3Text"), TextBox)
        shippingAdd.Address3 = txt.Text
        txt = CType(addressCtrl.FindControl("moCityText"), TextBox)
        shippingAdd.City = txt.Text

        txtPostalCode = CType(addressCtrl.FindControl("moPostalText"), TextBox)

        ' Country value
        ddl = CType(addressCtrl.FindControl("moCountryDrop_WRITE"), DropDownList)
        If (ddl.Items.Count > 0 AndAlso ddl.SelectedIndex > -1) Then
            shippingAdd.Country = LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRIES, New Guid(ddl.SelectedValue.ToString()))
        End If
        ''validate zip code
        Dim zd As New ZipDistrict()
        zd.CountryId = New Guid(ddl.SelectedValue.ToString())
        zd.ValidateZipCode(txtPostalCode.Text)
        ' Region/ State Drop down value
        ddl = CType(addressCtrl.FindControl("moRegionDrop_WRITE"), DropDownList)
        If (ddl.Items.Count > 0 AndAlso ddl.SelectedIndex > -1) Then
            shippingAdd.State = LookupListNew.GetCodeFromId(LookupListNew.LK_REGIONS, New Guid(ddl.SelectedValue.ToString()))
        End If
        shippingAdd.PostalCode = txtPostalCode.Text

        Return shippingAdd
    End Function
    Private Function GetUserSelectedLogisticsData() As Boolean
        Dim islogisticsOptionSelected As Boolean = False
        Dim gvr As GridViewRow

        For Each gvr In GridViewLogisticsOptions.Rows
            Dim rb As RadioButton
            rb = CType(gvr.Cells(GridLoColLoRdoIdx).FindControl(GridLogisticsOptionsRdoCtrl), RadioButton)
            If Not rb.Checked Then
                Continue For
            End if

                Dim lb As Label
                lb = CType(gvr.Cells(GridLoColLoDetailIdx).FindControl(GridLoCodeLblCtrl), Label)

                Dim wsResponse As LogisticStagesResponse
                wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)

                Dim lOption As LogisticOption = wsResponse.Stages(State.LogisticsStage).Options.FirstOrDefault(Function(q) q.Code = lb.Text)
                lOption.Selected = True

                ' Questions
               If Not ValidateFulfillmentQuestionsAnswers(gvr) Then
                   Return False
               End If

                ' Logistics Options
                If Not ProcessUserSelectedLogisticOptions(lOption,gvr) Then
                    Return False
                End If

                ' Service Center
                Dim logisticStage As LogisticStage
                logisticStage = wsResponse.Stages(State.LogisticsStage)
                If Not ValidateServiceCenterSelection(lOption,gvr,logisticStage) Then
                    Return False
                End If
                ' Bank transfer
                If Not ValidateBankTransferSelection(lOption,gvr) Then
                    Return False
                End If

                'Customer Info for Check Payment
                If Not ValidateCheckPaymentSelection(lOption,gvr) Then
                    Return False
                End If
            
                islogisticsOptionSelected = True
                Exit For
           
        Next

        If Not islogisticsOptionSelected Then
            MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_A_LOGISTIC_OPTION, True)
            Return False
        End If

        Return True

    End Function

    Private Function ValidateFulfillmentQuestionsAnswers(gvr As GridViewRow) As Boolean
        Dim logistictUserControlQuestion As UserControlQuestion = CType(gvr.Cells(GridLoColLoRdoIdx).FindControl(LogisticsOptionsQuestionsCtrl), UserControlQuestion)
        If logistictUserControlQuestion IsNot Nothing Then
            'Retrieve the selections from the user
            logistictUserControlQuestion.GetQuestionAnswer()
            If (Not String.IsNullOrEmpty(logistictUserControlQuestion.ErrAnswerMandatory.ToString())) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_IS_REQUIRED_ERR, True)
                Return False
            ElseIf (Not String.IsNullOrEmpty(logistictUserControlQuestion.ErrorQuestionCodes.ToString())) Then
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ProcessUserSelectedLogisticOptions(lOption as LogisticOption,gvr As GridViewRow ) as Boolean
         If lOption IsNot Nothing _
                   AndAlso (lOption.Type = LogisticOptionType.DealerBranchAddress OrElse lOption.Type = LogisticOptionType.CustomerAddress) Then

                    Dim addressSelected As ClaimRecordingService.Address = PopulateAddressFromAddressController(CType(gvr.Cells(GridLoColLoDetailIdx).FindControl("ucAddressControllerLogisticsOptions"), UserControlAddress_New))
                    Dim postalCodeOld As String
                    Dim countryCodeOld As String

                    If addressSelected.Address1 Is Nothing OrElse addressSelected.Address1.Trim() = String.Empty Then
                        MasterPage.MessageController.AddError(Message.MSG_PROMPT_ADDRESS1_FIELD_IS_REQUIRED, True)
                        Return False
                    End If

                    If lOption.Type = LogisticOptionType.DealerBranchAddress Then
                        Dim storeNumber As String = CType(gvr.Cells(GridLoColLoDetailIdx).FindControl(GridLoStoreNumberTxtCtrl), TextBox).Text
                        If String.IsNullOrWhiteSpace(storeNumber) Then
                            MasterPage.MessageController.AddError(Message.MSG_ERR_STORE_NUMBER_MANDATORY, True)
                            Return False
                        End If
                        If lOption.LogisticOptionInfo IsNot Nothing _
                       AndAlso  CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address IsNot Nothing Then
                            postalCodeOld = CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address.PostalCode
                            countryCodeOld = CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address.Country
                        End If
                        CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).BranchCode = storeNumber
                        CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address = addressSelected

                    ElseIf lOption.Type = LogisticOptionType.CustomerAddress Then
                        If  lOption.LogisticOptionInfo IsNot Nothing _
                       AndAlso CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address IsNot Nothing Then
                            postalCodeOld = CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address.PostalCode
                            countryCodeOld = CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address.Country
                        End If

                        CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address = addressSelected
                    End If

                    If  lOption IsNot Nothing _
                   AndAlso  lOption.DeliveryOptions IsNot Nothing _
                   AndAlso lOption.DeliveryOptions.DisplayEstimatedDeliveryDate Then

                        If (Not String.IsNullOrWhiteSpace(countryCodeOld) AndAlso addressSelected.Country <> countryCodeOld) _
                       OrElse (Not String.IsNullOrWhiteSpace(postalCodeOld) AndAlso addressSelected.PostalCode <> postalCodeOld) Then
                            MasterPage.MessageController.AddError(Message.MSG_ERR_GET_DELIVERY_DATE, True)
                            Return False
                        End If

                        Dim ucDeliverySlots As UserControlDeliverySlot = CType(gvr.Cells(GridLoColLoDetailIdx).FindControl(LogisticsOptionsEstimateDeliveryDateCtrl), UserControlDeliverySlot)
                        Dim selectedDeliveryDate As Nullable(Of Date) = ucDeliverySlots.DeliveryDate

                        State.DeliveryDate = ucDeliverySlots.DeliveryDate
                        State.DefaultDeliveryDay = ucDeliverySlots.DefaultDeliveryDay
                        State.DeliverySlotTimeSpan = If(ucDeliverySlots.DeliverySlotTimeSpan Is Nothing, TimeSpan.Zero, ucDeliverySlots.DeliverySlotTimeSpan)

                        If lOption.DeliveryOptions.DesiredDeliveryDateMandatory AndAlso Not selectedDeliveryDate.HasValue Then
                            MasterPage.MessageController.AddError(Message.MSG_ERR_DELIVERY_DATE_MANDATORY, True)
                            Return False
                        Else
                            If  lOption.LogisticOptionInfo IsNot Nothing  AndAlso State.LogisticsOption.Code.ToUpper().Equals("X") Then
                                    If Not State.IsExpeditedBtnClicked Then
                                        MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_EXPEDITED_DELIVERY_BUTTON, True)
                                        Return False
                                    End If
                                lOption.LogisticOptionInfo.EstimatedChangedDeliveryDate = selectedDeliveryDate
                            End If
                        End If
                    End If

                End If
        Return True
    End Function
    Private Function ValidateServiceCenterSelection(lOption As LogisticOption,gvr as GridViewRow, ByRef logisticStage As LogisticStage) As Boolean
        If lOption IsNot Nothing _
           AndAlso lOption.Type = LogisticOptionType.ServiceCenter Then

            Dim uc As UserControlServiceCenterSelection
            uc = CType(gvr.FindControl("ucServiceCenterUserControl"), UserControlServiceCenterSelection)

            If uc IsNot Nothing AndAlso uc.SelectedServiceCenter IsNot Nothing Then
                logisticStage.ServiceCenterCode = uc.SelectedServiceCenter.ServiceCenterCode
            Else
                MasterPage.MessageController.AddError(Message.MSG_ERR_DEFAULT_SERVICE_CENTER, True)
                Return False
            End If
        End If
        Return True
    End Function
    Private Function ValidateBankTransferSelection(lOption as LogisticOption, gvr as GridViewRow) As Boolean
        If  lOption IsNot Nothing _
            AndAlso lOption.Type = LogisticOptionType.BankTransfer Then
            Dim moBankInfoController As UserControlBankInfo_New = CType(gvr.FindControl(LogisticsOptionsBankInfoCtrl), UserControlBankInfo_New)
            Dim logisticOptionInfoBankTransfer As LogisticOptionInfoBankTransfer = CType(lOption.LogisticOptionInfo, LogisticOptionInfoBankTransfer)
            If moBankInfoController IsNot Nothing Then
                moBankInfoController.PopulateBOFromControl()
                logisticOptionInfoBankTransfer.BankInfo = New BankInfo With {
                    .ReferenceId = moBankInfoController.State.myBankInfoBo.Id.ToString
                    }
            Else
                MasterPage.MessageController.AddError(Message.MSG_BANK_NAME_VALUE_REQUIRED, True)
                Return False
            End If
        End If
        Return True
    End Function
    Private Function ValidateCheckPaymentSelection(lOption as LogisticOption, gvr as GridViewRow) as Boolean
        If  lOption IsNot Nothing _
            AndAlso lOption.Type = LogisticOptionType.CheckCustomerMail Then
            Dim moCustomerAddressController As UserControlAddress_New = CType(gvr.FindControl(LogisticsOptionsCheckInfoCtrl), UserControlAddress_New)
            Dim logisticOptionInfoCustomerInfoForCheck As LogisticOptionInfoCheckCustomerMail = CType(lOption.LogisticOptionInfo, LogisticOptionInfoCheckCustomerMail)

            If moCustomerAddressController IsNot Nothing Then
                Dim addressSelected As ClaimRecordingService.Address = PopulateAddressFromAddressController(moCustomerAddressController)
                If addressSelected.Address1 Is Nothing OrElse addressSelected.Address1.Trim() = String.Empty Then
                    MasterPage.MessageController.AddError(Message.MSG_PROMPT_ADDRESS1_FIELD_IS_REQUIRED, True)
                    Return False
                End If
                logisticOptionInfoCustomerInfoForCheck.Address= addressSelected
            Else
                MasterPage.MessageController.AddError(Message.MSG_PROMPT_ADDRESS_REQUIRED, True)
                Return False
            End If
        End If
        Return True
    End Function
#End Region

#Region "Logistics Options -  Call Web Service"
    Private Sub SendReponseLogisticsOptions()

        Dim wsRequest As LogisticStagesRequest = New LogisticStagesRequest()
        Dim wsPreviousResponse As LogisticStagesResponse

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(LogisticStagesResponse) Then
            wsPreviousResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
        Else
            Return
        End If

        wsRequest.CaseNumber = wsPreviousResponse.CaseNumber
        wsRequest.CompanyCode = wsPreviousResponse.CompanyCode
        wsRequest.InteractionNumber = wsPreviousResponse.InteractionNumber
        wsRequest.Stages = wsPreviousResponse.Stages

        wsRequest.QuestionVersion = 1
        wsRequest.QuestionSetCode = wsPreviousResponse.Stages(State.LogisticsStage)?.Options.SingleOrDefault(Function(opt) opt.Selected)?.QuestionSetCode
        wsRequest.Questions = wsPreviousResponse.Stages(State.LogisticsStage)?.Options.SingleOrDefault(Function(opt) opt.Selected)?.Questions

        Try
            If State.DeliveryDate IsNot Nothing Then
                Dim estimatedDeliveryDate As Nullable(Of Date) = State.DefaultDeliveryDay.DeliveryDate.Add(TimeSpan.Zero)
                Dim estimatedChangedDeliveryDate As Nullable(Of Date) = If(Not State.DeliveryDate.HasValue, estimatedDeliveryDate, State.DeliveryDate.Value.Add(State.DeliverySlotTimeSpan))
                wsRequest.Stages.Where(Function(s) s.Code = "FW").ForEach(Sub(ls As LogisticStage)
                                                                              ls.Options.Where(Function(lo) TypeOf lo.LogisticOptionInfo Is LogisticOptionInfoCustomerAddress).ForEach(Sub(o As LogisticOption)
                                                                                                                                                                                           o.LogisticOptionInfo.EstimatedDeliveryDate = estimatedDeliveryDate
                                                                                                                                                                                           o.LogisticOptionInfo.EstimatedChangedDeliveryDate = estimatedChangedDeliveryDate
                                                                                                                                                                                       End Sub)
                                                                          End Sub)
            End If

            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
            
        End Try
    End Sub

    <Obsolete>
    Private Sub GetEstimatedDeliveryDate(ByRef ucDeliverySlots As UserControlDeliverySlot, ByVal deliveryAddress As ClaimRecordingService.Address, ByVal deliveryOptions As DeliveryOptions)
        Try
            'get the service center
            Dim serviceCenterCode As String = String.Empty
            Dim countryCode As String = String.Empty

            ''''Begin  - new code to look up service center code and country code from fulfillment profile
            If (deliveryOptions IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(deliveryOptions.ServiceCenterCode) AndAlso Not String.IsNullOrWhiteSpace(deliveryOptions.CountryCode)) Then
                serviceCenterCode = deliveryOptions.ServiceCenterCode
                countryCode = deliveryOptions.CountryCode
                ''''''End
            Else
                ''''if service center and country are not available in Fulfillment Profile then fall back to old logic
                If (serviceCenterCode Is Nothing) Then
                    If State.ClaimBo Is Nothing AndAlso  Not String.IsNullOrEmpty(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber)  Then
                        Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                        SetCompanyId(companyId)
                        'defaultServiceCenter = New ServiceCenter(State.ClaimBo.ServiceCenterId)
                        serviceCenterCode = New ServiceCenter(State.ClaimBo.ServiceCenterId).Code
                        countryCode = deliveryAddress.Country
                    Else
                        Dim ctryBo As New Country(LookupListNew.GetIdFromCode(LookupListNew.LK_COUNTRIES, deliveryAddress.Country))
                        ValidatePostalCodeAndServiceCenter(ctryBo,deliveryAddress.PostalCode)
                        serviceCenterCode = New ServiceCenter(ctryBo.DefaultSCId).Code
                        countryCode = deliveryAddress.Country
                    End If
                End If
            End If


            With ucDeliverySlots
                .CountryCode = countryCode
                .ServiceCenter = serviceCenterCode
                .CourierCode = State.LogisticsOption.DeliveryOptions.CourierCode
                .CourierProductCode = State.LogisticsOption.DeliveryOptions.CourierProductCode
                .DeliveryAddress = New UserControlDeliverySlot.DeliveryAddressInfo() With {
                    .CountryCode = deliveryAddress.Country,
                    .RegionShortDesc = deliveryAddress.State,
                    .PostalCode = deliveryAddress.PostalCode,
                    .City = deliveryAddress.City,
                    .Address1 = deliveryAddress.Address1,
                    .Address2 = deliveryAddress.Address2,
                    .Address3 = deliveryAddress.Address3
                    }
                .PopulateDeliveryDate(blnNotSpecifyCheckInitState:=False, blnEnableNotSpecifyCheck:=False)
            End With

        Catch ex As ThreadAbortException
            HandleErrors(ex, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    <Obsolete>
    Private Sub SetCompanyId (companyId As Guid) 
        If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
            For Each cid As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies()
                Dim comobj As New Company(cid)
                If comobj.Code = State.SubmitWsBaseClaimRecordingResponse.CompanyCode Then
                    ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber, cid)
                    Exit For
                End If
            Next
        Else
                State.ClaimBo = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.SubmitWsBaseClaimRecordingResponse.ClaimNumber, companyId)
        End If
   
    End Sub
    Private Sub ValidatePostalCodeAndServiceCenter(countryBo as Country,postalCode as string)
        
        If countryBo Is Nothing OrElse String.IsNullOrWhiteSpace(countryBo.Code) OrElse String.IsNullOrWhiteSpace(postalCode) Then
            MasterPage.MessageController.AddError(Message.MSG_ERR_COUNTRY_POSTAL_MANDATORY, True)
            Return
        End If

        If countryBo.DefaultSCId.IsEmpty Then
            MasterPage.MessageController.AddError(Message.MSG_ERR_DEFAULT_SERVICE_CENTER, True)
            Return
        End If
    End Sub
#End Region
#Region "Logistics Options -  Button event"
    Protected Sub btnLogisticsOptionsBack_Click(sender As Object, e As EventArgs) Handles btnLogisticsOptionsBack.Click
        Try
            State.LogisticsStage = State.LogisticsStage - 1
            DisplayNextView()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnLogisticsOptionsContinue_Click(sender As Object, e As EventArgs) Handles btnLogisticsOptionsContinue.Click
        Try
            If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(LogisticStagesResponse) Then
                If Not GetUserSelectedLogisticsData() Then
                    Return
                End If
                Dim wsResponse As LogisticStagesResponse
                wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
                If State.LogisticsStage >= wsResponse.Stages.Count - 1 Then
                    SendReponseLogisticsOptions()
                Else
                    State.LogisticsStage = State.LogisticsStage + 1
                End If
                DisplayNextView()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnEstimateDeliveryDate_OnClick(sender As Object, e As EventArgs)
        Try
            Dim senderBtn As Button = sender
            Dim gvr As GridViewRow = GridViewLogisticsOptions.Rows(senderBtn.CommandArgument)
            Dim wsResponse As LogisticStagesResponse
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
            Dim logisticsStage As LogisticStage
            logisticsStage = wsResponse.Stages(State.LogisticsStage)

            Dim shippingCodeLabel As Label
            shippingCodeLabel = CType(gvr.Cells(GridLoColLoRdoIdx).FindControl(GridLoCodeLblCtrl), Label)

            Dim ucDeliverySlots As UserControlDeliverySlot = CType(gvr.Cells(GridLoColLoDetailIdx).FindControl(LogisticsOptionsEstimateDeliveryDateCtrl), UserControlDeliverySlot)
            ucDeliverySlots.Visible = True
            Dim deliveryAddress As ClaimRecordingService.Address

            Dim lOption As LogisticOption = logisticsStage.Options.FirstOrDefault(Function(q) q.Code = shippingCodeLabel.Text)
            ' Logistics Options
            If lOption.Type = LogisticOptionType.DealerBranchAddress Then
                CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address = PopulateAddressFromAddressController(CType(gvr.Cells(GridLoColLoDetailIdx).FindControl("ucAddressControllerLogisticsOptions"), UserControlAddress_New))
                deliveryAddress = CType(lOption.LogisticOptionInfo, LogisticOptionInfoDealerBranchAddress).Address

            ElseIf lOption.Type = LogisticOptionType.CustomerAddress Then
                CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address = PopulateAddressFromAddressController(CType(gvr.Cells(GridLoColLoDetailIdx).FindControl("ucAddressControllerLogisticsOptions"), UserControlAddress_New))
                deliveryAddress = CType(lOption.LogisticOptionInfo, LogisticOptionInfoCustomerAddress).Address
            End If

            GetEstimatedDeliveryDate(ucDeliverySlots, deliveryAddress, lOption.DeliveryOptions)

            If shippingCodeLabel.Text.ToUpper() = "X" Then
                State.IsExpeditedBtnClicked = True
                If ucDeliverySlots.IsDeliverySlotAvailable Then
                    ControlMgr.SetEnableControl(Me, btnLogisticsOptionsContinue, True)
                    ucDeliverySlots.Visible = True
                Else
                    ControlMgr.SetEnableControl(Me, btnLogisticsOptionsContinue, False)
                    ucDeliverySlots.Visible = False
                End If

            Else
                ControlMgr.SetEnableControl(Me, btnLogisticsOptionsContinue, True)
                ucDeliverySlots.Visible = True
                State.IsExpeditedBtnClicked = False
            End If


        Catch ex As Exception
            State.IsExpeditedBtnClicked = False
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Logistics Options -  Grid Functions"
    Private Sub PopulateLogisticsOptionsGrid()
        Dim wsResponse As LogisticStagesResponse
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(LogisticStagesResponse) Then
            wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)

            If wsResponse.Stages Is Nothing OrElse wsResponse.Stages.Count = 0 Then
                lblLogisticsOptionNoRecordsFound.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
                ControlMgr.SetEnableControl(Me, lblLogisticsOptionNoRecordsFound, True)
                ControlMgr.SetEnableControl(Me, lblLogisticStageName, False)
                ControlMgr.SetEnableControl(Me, lblLogisticStageDescription, False)
                ControlMgr.SetVisibleControl(Me, GridViewLogisticsOptions, False)

            Else
                Dim logisticsStage As LogisticStage
                logisticsStage = wsResponse.Stages(State.LogisticsStage)
                lblLogisticStageName.Text = logisticsStage.Name
                lblLogisticStageDescription.Text = logisticsStage.Description
                GridViewLogisticsOptions.DataSource = logisticsStage.Options.OrderBy("Order", LinqExtentions.SortDirection.Ascending)
                GridViewLogisticsOptions.DataBind()
                If State.LogisticsStage = 0 Then
                    ControlMgr.SetVisibleControl(Me, btnLogisticsOptionsBack, False)
                Else
                    ControlMgr.SetVisibleControl(Me, btnLogisticsOptionsBack, True)
                End If

            End If
        End If
    End Sub

    Protected Sub GridViewLogisticsOptions_RowDataBound(sender As Object, e As GridViewRowEventArgs) _
        Handles GridViewLogisticsOptions.RowDataBound
        Dim source As IEnumerable(Of LogisticOption) = CType(sender, GridView).DataSource

        Dim wsResponse As LogisticStagesResponse
        wsResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, LogisticStagesResponse)
        Dim logisticsStage As LogisticStage
        logisticsStage = wsResponse.Stages(State.LogisticsStage)
        
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim logisticsOptionItem As LogisticOption =
                    source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, LogisticOption).Code)

            Dim isEnableControl As Boolean = logisticsOptionItem.Selected
            Dim rdoLogisticsOptions As RadioButton = CType(e.Row.FindControl(GridLogisticsOptionsRdoCtrl), RadioButton)
            rdoLogisticsOptions.Checked = isEnableControl

            Dim addressDetail As New BusinessObjectsNew.Address()
            Dim trBankInfo As HtmlTableRow = CType(e.Row.FindControl(GridLoBankInfoTr), HtmlTableRow)
            Dim trCheckInfo As HtmlTableRow = CType(e.Row.FindControl(GridLoCheckInfoTr), HtmlTableRow)
            Dim trServiceCenter As HtmlTableRow = CType(e.Row.FindControl(GridLoServiceCenterTr), HtmlTableRow)
            Dim trShippingAddress As HtmlTableRow = CType(e.Row.FindControl(GridLoShippingAddressTr), HtmlTableRow)
            Dim trDeliveryOptions As HtmlTableRow = CType(e.Row.FindControl(GridLoDeliveryOptionsTr), HtmlTableRow)

            If logisticsOptionItem IsNot Nothing then

                Select Case logisticsOptionItem.Type

                    'Logistics Options - Address
                    Case LogisticOptionType.CustomerAddress OrElse logisticsOptionItem.Type = LogisticOptionType.DealerBranchAddress
                        RowDataBoundAddress(e, logisticsOptionItem, rdoLogisticsOptions, isEnableControl, addressDetail, logisticsStage)
                     
                        'Logistic Options - Service Center
                    Case LogisticOptionType.ServiceCenter
                        RowDataBoundServiceCenter(e, isEnableControl, trShippingAddress, trServiceCenter, trDeliveryOptions, trBankInfo, trCheckInfo)

                        'Bank transfer 
                    Case LogisticOptionType.BankTransfer
                        RowDataBoundBankTransfer(e, trShippingAddress, trServiceCenter, trDeliveryOptions, trBankInfo,trCheckInfo)

                        'Customer Information for Check payment
                    Case LogisticOptionType.CheckCustomerMail
                        RowDataBoundCustomerCheckPayment(e, trShippingAddress, trServiceCenter, trDeliveryOptions, trBankInfo, trCheckInfo)

                    Case Else
                        RowDataBoundDisableOptions(trServiceCenter, trShippingAddress, trDeliveryOptions, trBankInfo, trCheckInfo)
                End Select

                'Delivery Options
                RowDataBoundDelivery(e, logisticsOptionItem, isEnableControl, trDeliveryOptions)

                'Questions
                RowDataBoundQuestions(e, logisticsOptionItem)

            Else
                RowDataBoundDisableOptions(trServiceCenter, trShippingAddress, trDeliveryOptions, trBankInfo, trCheckInfo)
            End If
        End If
    End Sub

    Private Sub RowDataBoundDisableOptions(trServiceCenter As HtmlTableRow, trShippingAddress As HtmlTableRow, trDeliveryOptions As HtmlTableRow, trBankInfo As HtmlTableRow, trCheckInfo As HtmlTableRow)

        'Logistics Options - Address
        If trShippingAddress Is Nothing Then Throw New ArgumentNullException("TableRow for Shipping Address not found")
        trShippingAddress.Attributes("style") = "display: none"

        'Logistic Options - Service Center
        If trServiceCenter Is Nothing Then Throw New ArgumentNullException("TableRow for Service Center not found")
        trServiceCenter.Attributes("style") = "display: none"

        ' Delivery Options
        If trDeliveryOptions Is Nothing Then Throw New ArgumentNullException("TableRow for Delivery Options not found")
        trDeliveryOptions.Attributes("style") = "display: none"

        'Bank transfer 
        trBankInfo.Attributes("style") = "display: none"

        'Customer Information for Check payment
        trCheckInfo.Attributes("style") = "display: none"
    End Sub

    Private Sub RowDataBoundQuestions(e As GridViewRowEventArgs, logisticsOptionItem As LogisticOption)

        ' Questions
        Dim logisticsOptionsQuestionsItemCtrl As UserControlQuestion =
                CType(e.Row.FindControl(LogisticsOptionsQuestionsCtrl), UserControlQuestion)
        If logisticsOptionItem.Questions IsNot Nothing AndAlso logisticsOptionItem.Questions.Length > 0 Then

            With logisticsOptionsQuestionsItemCtrl
                .DateFormat = DATE_FORMAT
                .UserNameSetting = UserName
                .PasswordSetting = Password
                .ServiceUrlSetting = ServiceUrl
                .ServiceEndPointNameSetting = EndPointName
                .HostMessageController = MasterPage.MessageController
            End With
            '
            logisticsOptionsQuestionsItemCtrl.SetQuestionTitle(TranslationBase.TranslateLabelOrMessage("QUESTION_SET"))
            '
            logisticsOptionsQuestionsItemCtrl.QuestionDataSource = logisticsOptionItem.Questions
            logisticsOptionsQuestionsItemCtrl.QuestionDataBind()
            ControlMgr.SetVisibleControl(Me, logisticsOptionsQuestionsItemCtrl, True)
        Else
            ControlMgr.SetVisibleControl(Me, logisticsOptionsQuestionsItemCtrl, False)
        End If
    End Sub

    Private Sub RowDataBoundCustomerCheckPayment(e As GridViewRowEventArgs, trShippingAddress as HtmlTableRow, trServiceCenter As HtmlTableRow, trDeliveryOptions As HtmlTableRow, trBankInfo As HtmlTableRow, trCheckInfo As HtmlTableRow)

        Dim moCustomerAddressController As UserControlAddress_New =
                CType(e.Row.FindControl(LogisticsOptionsCheckInfoCtrl), UserControlAddress_New)
        Dim txtPayableToName As TextBox = CType(e.Row.FindControl(GridPayableToTxtCtrl), TextBox)
        Dim btnValidateAddress As Button = moCustomerAddressController.FindControl(ValidateAddressButton)
        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        Dim oCustomerAddress As BusinessObjectsNew.Address = New BusinessObjectsNew.Address(ocertificate.AddressId)
        Dim lblPayableTo As Label = CType(e.Row.FindControl(GridPayableToLblCtrl), Label)
        Dim lblCheckInfo As Label = CType(e.Row.FindControl(GridCheckInfoLblCtrl), Label)

        lblPayableTo.Text = TranslationBase.TranslateLabelOrMessage(lblPayableTo.Text)
        lblCheckInfo.Text = TranslationBase.TranslateLabelOrMessage(lblCheckInfo.Text)

        ControlMgr.SetVisibleControl(Me, trShippingAddress, false)
        ControlMgr.SetVisibleControl(Me, trServiceCenter, False)
        ControlMgr.SetVisibleControl(Me, trDeliveryOptions, false)
        ControlMgr.SetVisibleControl(Me, trBankInfo, False)
        ControlMgr.SetVisibleControl(Me, trCheckInfo, True)
        ControlMgr.SetVisibleControl(Me, btnValidateAddress, False)

        EnableDisableAddressValidation(moCustomerAddressController)
        moCustomerAddressController.Visible = True
        moCustomerAddressController.TranslateAllLabelControl()
        txtPayableToName.Text = oCertificate.CustomerName
        moCustomerAddressController.Bind(oCustomerAddress)
        moCustomerAddressController.EnableControls(False, True)
    End Sub

    Private Sub RowDataBoundBankTransfer(e As GridViewRowEventArgs, trShippingAddress as HtmlTableRow, trServiceCenter As HtmlTableRow, trDeliveryOptions As HtmlTableRow, trBankInfo As HtmlTableRow, trCheckInfo As HtmlTableRow)

        Dim lblBankInfo As Label = CType(e.Row.FindControl(GridBankInfoLblCtrl), Label)
        lblBankInfo.Text = TranslationBase.TranslateLabelOrMessage("CUSTOMER_BANK_INFO")

        ControlMgr.SetVisibleControl(Me, trShippingAddress, False)
        ControlMgr.SetVisibleControl(Me, trServiceCenter, False)
        ControlMgr.SetVisibleControl(Me, trDeliveryOptions, False)
        ControlMgr.SetVisibleControl(Me, trBankInfo, True)
        ControlMgr.SetVisibleControl(Me, trCheckInfo, False)

        Dim moBankInfoController As UserControlBankInfo_New = CType(e.Row.FindControl(LogisticsOptionsBankInfoCtrl),
                                                                    UserControlBankInfo_New)
        moBankInfoController.Visible = True
        moBankInfoController.LabelTranslations()
        Dim ddlCountry As DropDownList = CType(moBankInfoController.FindControl("moCountryDrop_WRITE"), DropDownList)
        Me.State.BankInfoBO = New BusinessObjectsNew.BankInfo With {
            .CountryID = Me.State.ClaimBo.Certificate.CountryPurchaseId
            }
        moBankInfoController.State.myBankInfoBo = Me.State.BankInfoBO
        moBankInfoController.Bind(Me.State.BankInfoBO)
        ddlCountry.Enabled = False
        moBankInfoController.EnableControlsBasedOnCountry(Me.State.BankInfoBO.CountryID)
    End Sub

    Private Sub RowDataBoundDelivery(e As GridViewRowEventArgs, logisticsOptionItem As LogisticOption, isEnableControl As Boolean, trDeliveryOptions As HtmlTableRow)
        If _
            logisticsOptionItem.DeliveryOptions IsNot Nothing AndAlso
            logisticsOptionItem.DeliveryOptions.DisplayEstimatedDeliveryDate Then
            Dim lblDeliveryDate As Label = CType(e.Row.FindControl(GridLoDeliveryDateLblCtrl), Label)
            Dim btnEstimateDeliveryDate As Button = CType(e.Row.FindControl(GridLoEstimateDeliveryDateBtnCtrl), Button)

            lblDeliveryDate.Text = TranslationBase.TranslateLabelOrMessage("EXPECTED_DELIVERY_DATE")
            ControlMgr.SetEnableControl(Me, btnEstimateDeliveryDate, isEnableControl)
            btnEstimateDeliveryDate.Text = TranslationBase.TranslateLabelOrMessage("GET_DELIVERY_DATE")
        Else
            If trDeliveryOptions Is Nothing Then _
                Throw New ArgumentNullException("TableRow for Delivery Options not found")
            trDeliveryOptions.Attributes("style") = "display: none"
        End If
    End Sub

    Private Sub RowDataBoundServiceCenter(e As GridViewRowEventArgs, isEnableControl As Boolean, trShippingAddress as HtmlTableRow, trServiceCenter As HtmlTableRow, trDeliveryOptions As HtmlTableRow, trBankInfo As HtmlTableRow, trCheckInfo As HtmlTableRow)

        ControlMgr.SetVisibleControl(Me, trShippingAddress, false)
        ControlMgr.SetVisibleControl(Me, trDeliveryOptions, False)
        ControlMgr.SetVisibleControl(Me, trServiceCenter, True)
        ControlMgr.SetVisibleControl(Me, trBankInfo, False)
        ControlMgr.SetVisibleControl(Me, trCheckInfo, False)

        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        Dim oCountry As Country = New Country(oCertificate.Company.CountryId)

        Dim moServiceCenterCtrl As UserControlServiceCenterSelection = CType(e.Row.FindControl(GridLoServiceCenterCtrl),
                                                                             UserControlServiceCenterSelection)
        moServiceCenterCtrl.Visible = True


        ServiceCenterSelectionHandler(moServiceCenterCtrl)

        moServiceCenterCtrl.PageSize = 30
        moServiceCenterCtrl.CountryId = oCertificate.Company.CountryId
        moServiceCenterCtrl.CountryCode = oCountry.Code
        moServiceCenterCtrl.CompanyCode = oCertificate.Company.Code
        moServiceCenterCtrl.Dealer = oCertificate.Dealer.Dealer

        If State.ClaimedDevice IsNot Nothing Then
            moServiceCenterCtrl.Make = State.ClaimedDevice.Manufacturer
        ElseIf State.ClaimBo.ClaimedEquipment IsNot Nothing Then
            moServiceCenterCtrl.Make = State.ClaimBo.ClaimedEquipment.Manufacturer
        Else
            moServiceCenterCtrl.Make = String.Empty
        End If

        moServiceCenterCtrl.RiskTypeEnglish = State.ClaimBo.RiskType
        moServiceCenterCtrl.MethodOfRepairXcd = State.FulfillmentOption.StandardCode
        moServiceCenterCtrl.HostMessageController = MasterPage.MessageController
        moServiceCenterCtrl.InitializeComponent()

        Dim lblServiceCenterSelected As Label = CType(e.Row.FindControl(GridLoServiceCenterSelectedLblCtrl), Label)
        lblServiceCenterSelected.Text = TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_SELECTED")

        Dim lblServiceCenterCode As Label = CType(e.Row.FindControl(GridLoServiceCenterCodeLblCtrl), Label)
        lblServiceCenterCode.Text = TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_CODE")

        Dim lblServiceCenterName As Label = CType(e.Row.FindControl(GridLoServiceCenterNameLblCtrl), Label)
        lblServiceCenterName.Text = TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_NAME")

        Dim txtStoreNumber As TextBox = CType(e.Row.FindControl(GridLoServiceCenterCodeTxtCtrl), TextBox)
        ControlMgr.SetEnableControl(Me, txtStoreNumber, isEnableControl)
    End Sub

      Private Sub RowDataBoundAddress(e As GridViewRowEventArgs, logisticsOptionItem As LogisticOption, rdoLogisticsOptions As RadioButton, isEnableControl As Boolean, addressDetail As BusinessObjectsNew.Address, logisticsStage As LogisticStage)

        Dim lblLoShippingAddress As Label = CType(e.Row.FindControl(GridLoShippingAddressLblCtrl), Label)

        Dim addressController As UserControlAddress_New = CType(e.Row.FindControl(GridLoAddressCtrl),
                                                                UserControlAddress_New)
        Dim trBankInfo As HtmlTableRow = CType(e.Row.FindControl(GridLoBankInfoTr), HtmlTableRow)
        Dim trCheckInfo As HtmlTableRow = CType(e.Row.FindControl(GridLoCheckInfoTr), HtmlTableRow)
        Dim trServiceCenter As HtmlTableRow = CType(e.Row.FindControl(GridLoServiceCenterTr), HtmlTableRow)

        addressController.Visible = True
        ControlMgr.SetVisibleControl(Me, trServiceCenter, false)
        ControlMgr.SetVisibleControl(Me, trBankInfo, False)
        ControlMgr.SetVisibleControl(Me, trCheckInfo, False)

        If (rdoLogisticsOptions.Checked) Then
            EnableDisableAddressValidation(addressController)
        Else
            Dim btnValidateAddress As Button = addressController.FindControl(ValidateAddressButton)
            ControlMgr.SetVisibleControl(Me, btnValidateAddress, False)
        End If


        Dim oCertificate As Certificate = New Certificate(State.CertificateId)
        addressDetail = GetCustomerOrDealerBranchAddress(e, logisticsOptionItem, lblLoShippingAddress, oCertificate, isEnableControl, addressDetail)

        addressController.TranslateAllLabelControl()

        'KDDI
        addressController.Bind(addressDetail, oCertificate.Product.ClaimProfile)
        addressController.EnableControls(Not isEnableControl, True)

        If _
            logisticsStage.Code = "RV" AndAlso (logisticsOptionItem.Code = "ST" OrElse logisticsOptionItem.Code = "E") AndAlso
            logisticsOptionItem.Type = LogisticOptionType.CustomerAddress Then
            addressController.EnableControls(True, True)
        End If
    End Sub

      Private Function GetCustomerOrDealerBranchAddress(e As GridViewRowEventArgs, logisticsOptionItem As LogisticOption, lblLoShippingAddress As Label, oCertificate As Certificate, isEnableControl As Boolean, addressDetail As BusinessObjectsNew.Address) As BusinessObjectsNew.Address
        If logisticsOptionItem.Type = LogisticOptionType.CustomerAddress Then
            addressDetail = GetLogisticCustomerAddressDetails(logisticsOptionItem, lblLoShippingAddress, oCertificate, e, addressDetail)
        ElseIf logisticsOptionItem.Type = LogisticOptionType.DealerBranchAddress Then
            addressDetail = GetLogisticDealerBranchAddressDetails(logisticsOptionItem, lblLoShippingAddress, e, addressDetail, isEnableControl)
        End If
        Return addressDetail
    End Function

    Private Function GetLogisticDealerBranchAddressDetails(logisticsOptionItem As LogisticOption, lblLoShippingAddress As Label, e As GridViewRowEventArgs, addressDetail As BusinessObjectsNew.Address, isEnableControl As Boolean) As BusinessObjectsNew.Address

        lblLoShippingAddress.Text = TranslationBase.TranslateLabelOrMessage("DEALER_BRANCH_ADDRESS")

        Dim lblStoreNumber As Label = CType(e.Row.FindControl(GridLoStoreNumberLblCtrl), Label)
        lblStoreNumber.Text = TranslationBase.TranslateLabelOrMessage("STORE_NUMBER")

        Dim txtStoreNumber As TextBox = CType(e.Row.FindControl(GridLoStoreNumberTxtCtrl), TextBox)
        ControlMgr.SetEnableControl(Me, txtStoreNumber, isEnableControl)

        If logisticsOptionItem.LogisticOptionInfo IsNot Nothing Then
            Dim infoStoreAddress As LogisticOptionInfoDealerBranchAddress
            infoStoreAddress = logisticsOptionItem.LogisticOptionInfo
            txtStoreNumber.Text = infoStoreAddress.BranchCode
            If infoStoreAddress.Address IsNot Nothing Then
                addressDetail = ConvertToAddressControllerField(infoStoreAddress.Address)
            End If
        End If
        Return addressDetail
    End Function

    Private Function GetLogisticCustomerAddressDetails(logisticsOptionItem As LogisticOption, lblLoShippingAddress As Label, oCertificate As Certificate, e As GridViewRowEventArgs, addressDetail As BusinessObjectsNew.Address) As BusinessObjectsNew.Address

        lblLoShippingAddress.Text = TranslationBase.TranslateLabelOrMessage("CUSTOMER_ADDRESS")
        If logisticsOptionItem.LogisticOptionInfo Is Nothing Then
            If oCertificate.AddressChild IsNot Nothing Then
                addressDetail = oCertificate.AddressChild
            End If
        Else
            Dim infoCustomerAddress As LogisticOptionInfoCustomerAddress
            infoCustomerAddress = logisticsOptionItem.LogisticOptionInfo
            If infoCustomerAddress.Address IsNot Nothing Then
                addressDetail = ConvertToAddressControllerField(infoCustomerAddress.Address)
            End If
        End If
        Dim trStoreNumber As HtmlTableRow = CType(e.Row.FindControl("trStoreNumber"), HtmlTableRow)
        trStoreNumber.Attributes("style") = "display: none"
        Return addressDetail
    End Function


    Private Sub ServiceCenterSelectionHandler(userControl As UserControlServiceCenterSelection)
        userControl.TranslationFunc = Function(value As String)
                                          Return TranslationBase.TranslateLabelOrMessage(value)
                                      End Function

        userControl.TranslateGridHeaderFunc = Sub(grid As GridView)
                                                  TranslateGridHeader(grid)
                                              End Sub

        userControl.ServiceCenterSelectedFunc = Sub(selected As ServiceCenterSelected)
                                                    LogisticServiceCenterSelected(selected)
                                                End Sub

        userControl.HighLightSortColumnFunc = Sub(grid As GridView, sortExp As String)
                                                  HighLightSortColumn(grid, sortExp, False)
                                              End Sub

        userControl.NewCurrentPageIndexFunc = Function(grid As GridView, ByVal intRecordCount As Integer, ByVal intNewPageSize As Integer)
                                                  Return NewCurrentPageIndex(grid, intRecordCount, intNewPageSize)
                                              End Function

        userControl.City = state.serviceCentreCity
        userControl.PostalCode = state.serviceCentrePostalCode

        userControl.HostMessageController = MasterPage.MessageController
    End Sub

    Private Sub LogisticServiceCenterSelected(selected As ServiceCenterSelected)

        Try
            If selected IsNot Nothing Then
                SetSelectedServiceCenterInfo(selected.ServiceCenterCode, selected.Name)
            Else
                SetSelectedServiceCenterInfo(String.Empty, String.Empty)
            End If
        Catch ex As Exception
            'Kept Intentionaly empty, raising/Logging error not required as this functionality logic is optional
        End Try
    End Sub

    Private Sub SetSelectedServiceCenterInfo(serviceCenterCode As String, serviceCenterName As String)
        For i As Integer = 0 To GridViewLogisticsOptions.Rows.Count - 1
            Dim rdo As RadioButton
            rdo = CType(GridViewLogisticsOptions.Rows(i).FindControl(GridLogisticsOptionsRdoCtrl), RadioButton)
            If rdo IsNot Nothing AndAlso rdo.Checked Then
                Dim codeTxt As TextBox
                codeTxt = CType(GridViewLogisticsOptions.Rows(i).FindControl(LogisticsOptionsServiceCenterCodeTxtCtrl), TextBox)
                If codeTxt IsNot Nothing Then
                    codeTxt.Text = serviceCenterCode
                End If
                Dim nameTxt As TextBox
                nameTxt = CType(GridViewLogisticsOptions.Rows(i).FindControl(LogisticsOptionsServiceCenterNameTxtCtrl), TextBox)
                If nameTxt IsNot Nothing Then
                    nameTxt.Text = serviceCenterName
                End If
            End If
        Next
    End Sub
#End Region
#End Region
#Region "Decision Response"
    Private Sub ShowDecisionView()

        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DecisionResponse) Then
            Dim decision As DecisionResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, DecisionResponse)
            Select Case decision.DecisionType
                Case DecisionTypes.Denied
                    If (moPurposecode.SelectedItem.Value.ToUpper() = Codes.CASE_PURPOSE__CANCELLATION_REQUEST AndAlso (decision.CompanyCode = "AIF")) Then
                        ShowPolicyCancelDecision()
                    End If
            End Select
            MoveToNextPage()
        End If
    End Sub

    Private Sub ShowPolicyCancelDecision()
        If State.SubmitWsBaseClaimRecordingResponse IsNot Nothing AndAlso State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DecisionResponse) Then
            Dim oCase As CaseBase = New CaseBase(State.SubmitWsBaseClaimRecordingResponse.CaseNumber, State.SubmitWsBaseClaimRecordingResponse.CompanyCode)
            NavController.Navigate(Me, FlowEvents.EVENT_DENIED_CERT_CANCEL_CRATED, New CaseDetailsForm.Parameters(oCase.Id))
        End If
    End Sub

    Protected Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Dim wsRequest As DynamicFulfillmentRequest = New DynamicFulfillmentRequest()
        Dim wsPreviousResponse As DynamicFulfillmentResponse

        If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DynamicFulfillmentResponse) Then
            wsPreviousResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, DynamicFulfillmentResponse)
        Else
            Return
        End If

        wsRequest.CaseNumber = wsPreviousResponse.CaseNumber
        wsRequest.CompanyCode = wsPreviousResponse.CompanyCode
        wsRequest.InteractionNumber = wsPreviousResponse.InteractionNumber
        wsRequest.OptionCode = "1"
        wsRequest.FallbackOption = False

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
                MoveToNextPage()
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
        End Try
    End Sub

    Protected Sub btnLegacyContinue_Click(sender As Object, e As EventArgs) Handles btnLegacyContinue.Click
        Dim wsRequest As DynamicFulfillmentRequest = New DynamicFulfillmentRequest()
        Dim wsPreviousResponse As DynamicFulfillmentResponse

        If State.SubmitWsBaseClaimRecordingResponse.GetType() Is GetType(DynamicFulfillmentResponse) Then
            wsPreviousResponse = DirectCast(State.SubmitWsBaseClaimRecordingResponse, DynamicFulfillmentResponse)
        Else
           Return
        End If

        Dim payLoad As ClientEventPayLoad = JsonConvert.DeserializeObject(Of ClientEventPayLoad)(hdnData.Value)

        wsRequest.OptionCode = $"DynamicFulfillment#{payLoad.FulfillmentOption}"
        wsRequest.CaseNumber = wsPreviousResponse.CaseNumber
        wsRequest.CompanyCode = wsPreviousResponse.CompanyCode
        wsRequest.InteractionNumber = wsPreviousResponse.InteractionNumber
        wsRequest.FallbackOption = True

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As ClaimRecordingServiceClient)
                    Return c.Submit(wsRequest)
                End Function)
            If wsResponse IsNot Nothing Then
                State.SubmitWsBaseClaimRecordingResponse = wsResponse
                DisplayNextView()
            End If
        Catch ex As FaultException
            ThrowWsFaultExceptions(ex)
        End Try
    End Sub


#End Region

End Class