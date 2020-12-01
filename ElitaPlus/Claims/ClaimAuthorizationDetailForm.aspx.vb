Imports System.Collections.Generic
Imports System.Text
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService

Partial Class ClaimAuthorizationDetailForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const NO_DATA As String = " - "
    Public Const URL As String = "~/Claims/ClaimAuthorizationDetailForm.aspx"
    Private Const GridColQuantityIdx As Integer = 5
    Private Const GridColDeviceIdx As Integer = 6
    Private Const GridColDeviceRdoCtrl As String = "rdoDevice"
    Private Const AuthType_SalesOrder As String = "AUTH_TYPE-SALES_ORDER"
    Private Const RESEND_SHIPPING_LABEL As String = "Resend Shipping Label"
    Private Const ADDR_DTL_CERT = "CERT"
#End Region

#Region "Page State"
    Class BaseState
        Public NavCtrl As INavigationController
    End Class
    

    Class MyState
        Public ClaimBO As MultiAuthClaim
        Public MyBO As ClaimAuthorization
        Public oServiceCenter As ServiceCenter
        Public InputParameters As Parameters
        Public IsEditMode As Boolean = False
        public IsUpdateRepairDate as Boolean = False
        Public ShowHistory As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public IsReturnedFromIssueActionAnswer As Boolean = True ' initialize ii as true to populate the data from WS
        Public FulFillmentStatusHistoryTable As DataTable
        Public FulfillmentIssuesTable As DataTable
        Public BestReplacementDeviceSelected As CheckVendorInventoryAndBestReplacementResponse = Nothing
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

    Protected Sub InitializeFromFlowSession()

        State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters IsNot Nothing Then
                State.ClaimBO = State.InputParameters.ClaimBO
                State.ShowHistory = State.InputParameters.ShowHistory
                If (State.ShowHistory) Then
                    Dim claimAuth As ClaimAuthorization = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
                    State.MyBO = CType(claimAuth.ClaimAuthorizationHistoryChildren.GetChild(State.InputParameters.ClaimAuthHistoryId), ClaimAuthHistory).AsClaimAuthorization()
                    State.oServiceCenter = New ServiceCenter(State.MyBO.ServiceCenterId)
                Else
                    State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
                    State.oServiceCenter = New ServiceCenter(State.MyBO.ServiceCenterId)
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.InputParameters = CType(CallingParameters, Parameters)
                State.ClaimBO = State.InputParameters.ClaimBO
                State.ShowHistory = State.InputParameters.ShowHistory
                State.oServiceCenter = New ServiceCenter(State.ClaimBO.ServiceCenterId)
                If (State.ShowHistory) Then
                    Dim claimAuth As ClaimAuthorization = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
                    State.MyBO = CType(claimAuth.ClaimAuthorizationHistoryChildren.GetChild(State.InputParameters.ClaimAuthHistoryId), ClaimAuthHistory).AsClaimAuthorization()
                Else
                    State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(callFromUrl As String, callingPar As Object) Handles MyBase.PageReturn
        If ReturnedValues IsNot Nothing AndAlso ReturnedValues.GetType() Is State.InputParameters.GetType() Then
            State.InputParameters = CType(NavController.ParametersPassed, Parameters)
            State.ClaimBO = State.InputParameters.ClaimBO
            State.ShowHistory = State.InputParameters.ShowHistory
            State.oServiceCenter = New ServiceCenter(State.ClaimBO.ServiceCenterId)
            State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
            If CalledUrl = ClaimIssueActionAnswerForm.Url Then
                State.IsReturnedFromIssueActionAnswer = True
                ' Me.NavController = CType(MyBase.State, BaseState).NavCtrl
            End If

        End If
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As MultiAuthClaim
        Public ClaimAuthorizationId As Guid
        Public ClaimAuthHistoryId As Guid
        Public ShowHistory As Boolean = False
        Public Sub New(claimBO As MultiAuthClaim, claimAuthorizationId As Guid, claimAuthHistoryId As Guid, Optional ByVal showHistory As Boolean = False)
            Me.ClaimBO = claimBO
            Me.ClaimAuthorizationId = claimAuthorizationId
            Me.ShowHistory = showHistory
            Me.ClaimAuthHistoryId = claimAuthHistoryId
        End Sub
    End Class
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Try
            MasterPage.MessageController.Clear()
            If Not IsPostBack Then
                InitializeData()
                InitializeUI()
                CheckReshipmentStatus()
                CheckResendShippingLabelStatus()
                CheckCancelServiceOrderStatus()
                CheckCancelStatus()
                PopulateDropDowns()
                PopulateReshipmentReason()
                PopulateFormFromBO() 
                EnableDisablePageControls()
                InitializeFulfillmentIssueStatusUI()
                TranslateGridHeader(GridViewDeviceSelection)
                TranslateGridHeader(GridViewBestDeviceSelection)
                CheckPayCashStatus()
                HandleRepairCodeProcessButton()
            End If
            BindBoPropertiesToLabels()
            'Bind Claim Auth Item 
            For Each claimAuthItem As ClaimAuthItem In State.MyBO.ClaimAuthorizationItemChildren
                ucClaimAuthItemDetail.BindBOProperties(claimAuthItem)
            Next

            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            lblNewSCError.Visible = False
        Catch ex As Threading.ThreadAbortException
            System.Threading.Thread.ResetAbort()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub ReturnBackToCallingPage()
        NavController.Navigate(Me, "claimForm", New ClaimForm.Parameters(State.MyBO.Claim.Id))
    End Sub


#End Region

#Region "Controlling Logic"
    Private Sub PopulateFormFromBO()
        PopulateControlFromBOProperty(lblClaimNumberValue, State.MyBO.Claim.ClaimNumber)
        PopulateControlFromBOProperty(lblDateOfLossValue, State.MyBO.Claim.LossDate)
        PopulateControlFromBOProperty(lblClaimStatusValue, State.MyBO.Claim.Status)
        PopulateControlFromBOProperty(lblClaimAuthNumberValue, State.MyBO.AuthorizationNumber)
        PopulateControlFromBOProperty(lblClaimAuthStatusValue, State.MyBO.ClaimAuthStatus)
        PopulateControlFromBOProperty(lblServiceCenterValue, State.MyBO.ServiceCenterName)
        PopulateControlFromBOProperty(lblClaimAuthFulfillmentTypeValue, LookupListNew.GetDescriptionFromExtCode(LookupListCache.LK_AUTH_FUL_TYPE, Authentication.LangId, State.MyBO.ClaimAuthfulfillmentTypeXcd))

        PopulateControlFromBOProperty(txtCashPaymentMethod, LookupListNew.GetDescriptionFromExtCode("PMTHD", Authentication.LangId, State.MyBO.CashPymtMethodXcd))

        PopulateControlFromBOProperty(TextboxSource, State.MyBO.Source)
        PopulateControlFromBOProperty(txtPartyReference, State.MyBO.ServiceCenterName)
        PopulateControlFromBOProperty(TextboxVisitDate, State.MyBO.VisitDate)
        PopulateControlFromBOProperty(TextboxRepairDate, State.MyBO.RepairDate)
        PopulateControlFromBOProperty(TextboxInvoiceDate, State.MyBO.ClaimAuthInvoiceDate)
        PopulateControlFromBOProperty(TextboxBatchNumber, State.MyBO.BatchNumber)
        PopulateControlFromBOProperty(TextboxPickupDate, State.MyBO.PickUpDate)
        PopulateControlFromBOProperty(TextboxDefectReason, State.MyBO.DefectReason)
        PopulateControlFromBOProperty(TextboxExpectedRepairDate, State.MyBO.ExpectedRepairDate)
        PopulateControlFromBOProperty(TextboxVerificationNumber, State.MyBO.VerificationNumber)
        PopulateControlFromBOProperty(TextboxSCReferenceNumber, State.MyBO.ServiceCenterReferenceNumber)
        PopulateControlFromBOProperty(TextboxTechnicalReport, State.MyBO.TechnicalReport)
        PopulateControlFromBOProperty(TextboxSpecialInstruction, State.MyBO.SpecialInstruction)
        PopulateControlFromBOProperty(TextboxDefectReason, LookupListNew.GetDescriptionFromCode("CAUSES_OF_LOSS", State.MyBO.DefectReason))
        If Not State.MyBO.WhoPaysId.Equals(Guid.Empty) Then SetSelectedItem(cboWhoPays, State.MyBO.WhoPaysId)
        SetSelectedItem(cboAuthTypeXcd, State.MyBO.AuthTypeXcd)

        ucClaimAuthItemDetail.InitController(State.MyBO, State.IsEditMode)

    End Sub

    Private Sub PopulateBOFromForm()
        PopulateBOProperty(State.MyBO, "Source", TextboxSource)
        PopulateBOProperty(State.MyBO, "PartyReferenceId", txtPartyReference)
        PopulateBOProperty(State.MyBO, "VisitDate", TextboxVisitDate)
        PopulateBOProperty(State.MyBO, "RepairDate", TextboxRepairDate)
        PopulateBOProperty(State.MyBO, "InvoiceDate", TextboxInvoiceDate)
        PopulateBOProperty(State.MyBO, "BatchNumber", TextboxBatchNumber)
        PopulateBOProperty(State.MyBO, "PickUpDate", TextboxPickupDate)
        PopulateBOProperty(State.MyBO, "ServiceCenterReferenceNumber", TextboxSCReferenceNumber)
        PopulateBOProperty(State.MyBO, "VerificationNumber", TextboxVerificationNumber)
        PopulateBOProperty(State.MyBO, "DefectReason", TextboxDefectReason)
        PopulateBOProperty(State.MyBO, "ExpectedRepairDate", TextboxExpectedRepairDate)
        PopulateBOProperty(State.MyBO, "TechnicalReport", TextboxTechnicalReport)
        PopulateBOProperty(State.MyBO, "WhoPaysId", cboWhoPays)
        PopulateBOProperty(State.MyBO, "AuthTypeXcd", cboAuthTypeXcd, False, True)
        PopulateBOProperty(State.MyBO, "PartyTypeXcd", cboPartyTypeXcd, False, True)
        PopulateBOProperty(State.MyBO, "SpecialInstruction", TextboxSpecialInstruction)

    End Sub

    Private Sub EnableDisablePageControls()
        SetEnabledForControlFamily(TextboxDefectReason, False, True)
        SetEnabledForControlFamily(TextboxSource, State.IsEditMode, True)
        'Me.SetEnabledForControlFamily(Me.txtPartyReference, Me.State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxVisitDate, State.IsEditMode, True)
        if state.IsUpdateRepairDate then
            SetEnabledForControlFamily(TextboxRepairDate, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonRepairDate, true, True)
        Else 
            SetEnabledForControlFamily(TextboxRepairDate, state.IsEditMode, True)
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonRepairDate, state.IsEditMode, True)
        End If

        SetEnabledForControlFamily(TextboxInvoiceDate, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxPickupDate, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxSpecialInstruction, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxBatchNumber, State.IsEditMode, True)

        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonVisitDate, State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonPickupDate, State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonVisitDate, State.IsEditMode, True)

        If (State.ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) AndAlso (State.oServiceCenter.DefaultToEmailFlag = False) Then
            ControlMgr.SetVisibleControl(Me, btnPrint, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        If State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_CASH_REIMBURSEMENT Then
            ControlMgr.SetVisibleControl(Me, lblCashPaymemtMethod, True)
            ControlMgr.SetVisibleControl(Me, txtCashPaymentMethod, True)
        Else
            ControlMgr.SetVisibleControl(Me, lblCashPaymemtMethod, False)
            ControlMgr.SetVisibleControl(Me, txtCashPaymentMethod, False)
        End If
        HandleButtons()
    End Sub

    Private Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Source", LabelSource)
        BindBOPropertyToLabel(State.MyBO, "PartyReferenceId", lblPartyReference)
        BindBOPropertyToLabel(State.MyBO, "VisitDate", LabelVisitDate)
        BindBOPropertyToLabel(State.MyBO, "RepairDate", LabelRepairDate)
        BindBOPropertyToLabel(State.MyBO, "InvoiceDate", LabelInvoiceDate)
        BindBOPropertyToLabel(State.MyBO, "BatchNumber", LabelBatchNumber)
        BindBOPropertyToLabel(State.MyBO, "PickUpDate", LabelPickUpDate)
        BindBOPropertyToLabel(State.MyBO, "DefectReason", LabelDefectReason)
        BindBOPropertyToLabel(State.MyBO, "ExpectedRepairDate", LabelExpectedRepairDate)
        BindBOPropertyToLabel(State.MyBO, "VerificationNumber", LabelVerificationNumber)
        BindBOPropertyToLabel(State.MyBO, "ServiceCenterReferenceNumber", LabelSCReferenceNumber)
        BindBOPropertyToLabel(State.MyBO, "TechnicalReport", LabelTechnicalReport)
        BindBOPropertyToLabel(State.MyBO, "WhoPaysId", LabelWhoPays)
        BindBOPropertyToLabel(State.MyBO, "AuthTypeXcd", lblAuthTypeXcd)
        BindBOPropertyToLabel(State.MyBO, "PartyTypeXcd", lblPartyTypeXcd)
        BindBOPropertyToLabel(State.MyBO, "SpecialInstruction", LabelSpecialInstruction)
    End Sub

    Private Sub InitializeData()

    End Sub

    Private Sub InitializeUI()
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL")
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY") & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CERTIFICATE") & " " & State.MyBO.Claim.CertificateNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL") & " " & State.MyBO.AuthorizationNumber
        If State.ShowHistory Then
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY") & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CERTIFICATE") & " " & State.MyBO.Claim.CertificateNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL") & " " & State.MyBO.AuthorizationNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTH_HISTORY") & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL")

        End If
        AddCalendar_New(ImageButtonVisitDate, TextboxVisitDate)
        AddCalendar_New(ImageButtonRepairDate, TextboxRepairDate)
        AddCalendar_New(ImageButtonPickupDate, TextboxPickupDate)
        ucClaimAuthItemDetail.Translate()
    End Sub
    Private Sub InitializeFulfillmentIssueStatusUI()
        ' Transalte grid header value
        TranslateGridHeader(GridViewClaimAuthFulfillmentIssues)
        TranslateGridHeader(GridClaimAuthStatusHistory)

        If State.IsReturnedFromIssueActionAnswer Then
            ' Get Auth data from fulfillment web service
            GetAuthorizationFulfillmentData()
            ' Set is false once data is populated from WS
            State.IsReturnedFromIssueActionAnswer = False
        End If

        ' Populate Grid
        PopulateGridFulfillmentIssues()
        PopulateGridClaimAuthStatusHistory()
    End Sub
    Private Sub HandleButtons()
        btnBack.Visible = Not State.IsEditMode AndAlso Not state.IsUpdateRepairDate
        btnSave_WRITE.Visible = (State.IsEditMode OrElse state.IsUpdateRepairDate) AndAlso Not State.ShowHistory
        btnUndo_Write.Visible = (State.IsEditMode OrElse state.IsUpdateRepairDate) AndAlso Not State.ShowHistory

        if IsUpdateRepairDateAllowed AndAlso Not State.IsUpdateRepairDate Then
            btnEdit_WRITE.Visible = True
        Else 
            btnEdit_WRITE.Visible = Not state.IsEditMode AndAlso state.MyBO.CanVoidClaimAuthorization AndAlso Not state.ShowHistory 
        End If

        PanButtonsHidden.Visible = Not State.IsEditMode AndAlso Not State.ShowHistory AndAlso Not State.IsUpdateRepairDate
        ActionButton.Visible = Not State.IsEditMode AndAlso Not State.ShowHistory AndAlso Not State.IsUpdateRepairDate
        btnNewServiceCenter.Visible = Not State.IsEditMode AndAlso Not State.IsUpdateRepairDate AndAlso State.ClaimBO.Status = BasicClaimStatus.Active AndAlso State.MyBO.CanVoidClaimAuthorization AndAlso Not State.ShowHistory AndAlso Not (State.ClaimBO.Dealer.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS)
        'Me.btnrefundFee.Visible =  Me.State.MyBO.ClaimAuthStatus  =  ClaimAuthorizationStatus.Authorized 'ClaimAuthorizationStatus.Collected  

        btnRefundFee.Visible = Me.State.MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized AndAlso State.MyBO.AuthTypeXcd.Equals(AuthType_SalesOrder)
        btnVoidAuthorization.Visible = State.MyBO.CanVoidClaimAuthorization AndAlso (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                                                                                           ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CSR2) OrElse
                                                                                           ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CSR))
    End Sub
    
    private sub HandleRepairCodeProcessButton
      
        Dim dt As DataTable = New DataTable()
        Dim iServiceClass as Integer = 0 
     
        If State.MyBO.ClaimAuthorizationItemChildren.Any(Function(i) i.ServiceClassCode=  "REPAIR") Then
            iServiceClass = iServiceClass  + 1
        End If

        'CLM_AUTH_SUBSTAT-RQAPT
        Dim iRepairCodeAccepted = State.FulFillmentStatusHistoryTable.AsEnumerable().Any(Function(x) x.Field(Of String)("SubStatusCode").Contains("RQAPT"))
        

        if iServiceClass >= 1 And iRepairCodeAccepted = False And _ 
           (lblClaimAuthStatusValue.Text = "Authorized"  Or lblClaimAuthStatusValue.Text = "Sent") And _ 
           lblClaimStatusValue.Text = "Active"
            btnRepairCodeProcess.Visible = true

        Else
            btnRepairCodeProcess.Visible = False

        End If
    End sub


    Private Sub PopulateDropDowns()
        Dim ocboWhoPays As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("WPAYS", Thread.CurrentPrincipal.GetLanguageCode())
        cboWhoPays.Populate(ocboWhoPays, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })
        Dim authTypeLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTH_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
        cboAuthTypeXcd.Populate(authTypeLst, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False,
                                            .TextFunc = AddressOf .GetDescription,
                                            .ValueFunc = AddressOf .GetExtendedCode
                                           })
        Dim partyTypeLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PARTY_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
        cboPartyTypeXcd.Populate(partyTypeLst, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False,
                                            .TextFunc = AddressOf .GetDescription,
                                            .ValueFunc = AddressOf .GetExtendedCode
                                           })
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                State.MyBO.Save()
                State.ClaimBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnBackToCallingPage()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnBackToCallingPage()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    UndoChanges()
                    ReturnBackToCallingPage()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub UndoChanges()
        State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(State.ClaimBO.Id)
        State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.MyBO.Id), ClaimAuthorization)
    End Sub
#End Region

#Region "Button Handlers"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try

            Try

                'Me.PopulateBOFromForm()
                If State.MyBO.IsDirty Or State.MyBO.ClaimAuthorizationItemChildren.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnBackToCallingPage()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            State.IsEditMode = False
            State.IsUpdateRepairDate = False
            PopulateBOFromForm()

            If State.MyBO.IsFamilyDirty Then
                State.MyBO.Save()
                State.ClaimBO.Save()
              
                If (Me.State.MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Void) Then
                    ReturnBackToCallingPage()
                Else
                    State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.MyBO.Id), ClaimAuthorization)
                    PopulateFormFromBO()
                    EnableDisablePageControls()
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                End If
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Function IsUpdateRepairDateAllowed() As Boolean
        With State
            If .MyBO IsNot Nothing AndAlso .MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Paid AndAlso .MyBO.RepairDate is Nothing Then
                Return True
            Else 
                Return False
            End If
        End With
    End Function

    Private Sub btnEdit__Click(sender As System.Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
        Try
            if IsUpdateRepairDateAllowed then
                state.IsUpdateRepairDate = True
            else
                state.IsEditMode = True
            End If
            EnableDisablePageControls()
            PopulateFormFromBO()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub btnUndo__Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            State.IsEditMode = False
            state.IsUpdateRepairDate = False
            UndoChanges()
            PopulateFormFromBO()
            EnableDisablePageControls()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub btnServiceCenterInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnServiceCenterInfo.Click
        Try
            NavController.Navigate(Me, "service_center_info_multi_auth", New ServiceCenterInfoForm.Parameters(State.MyBO.ServiceCenterId))
            'Me.callPage(ServiceCenterInfoForm.URL, New ServiceCenterInfoForm.Parameters(Me.State.MyBO.ServiceCenterId))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub btnRefundFee_Click(sender As System.Object, e As System.EventArgs) Handles btnRefundFee.Click
        Try

            Dim refundReason As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("ADJ_RESN", Thread.CurrentPrincipal.GetLanguageCode())
            cboRefundReason.Populate(refundReason, New PopulateOptions() With
                                        {
                                        .AddBlankItem = True
                                        })

            'Me.PopulateBOProperty(Me.State.MyBO, "DeniedReasonId", Me.cboRefundReason)
            Dim x As String = "<script language='JavaScript'> revealModal('ModalRefundFee') </script>"
            RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClaimAuthHistory_Click(sebder As System.Object, e As System.EventArgs) Handles btnClaimAuthHistory.Click
        Try
            NavController.Navigate(Me, "claim_auth_hist", New ClaimAuthHistoryForm.Parameters(State.ClaimBO, State.MyBO.Id))
            ' Me.callPage(ClaimAuthHistoryForm.URL, New ClaimAuthHistoryForm.Parameters(Me.State.ClaimBO, Me.State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            If (Not (State.ClaimBO.GetLatestServiceOrder(State.InputParameters.ClaimAuthorizationId) Is Nothing)) Then
                NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = State.ClaimBO.GetLatestServiceOrder(State.InputParameters.ClaimAuthorizationId)
                NavController.Navigate(Me, "soprint")
            Else
                Try
                    Dim _SOC As New ServiceOrderController()
                    _SOC.GenerateServiceOrder(CType(State.ClaimBO, ClaimBase), State.InputParameters.ClaimAuthorizationId)
                    NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = State.ClaimBO.GetLatestServiceOrder(State.InputParameters.ClaimAuthorizationId)
                    NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = State.ClaimBO.GetLatestServiceOrder(State.InputParameters.ClaimAuthorizationId)
                    NavController.Navigate(Me, "soprint")
                Catch ex As Exception
                    'There is NO Service Order associated with this Claim
                    DisplayMessage(Message.MSG_SERVICE_ORDER_RECORD_NOT_FOUND, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Try
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnCancelShipment_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelShipment.Click
        Try
            If (State.MyBO IsNot Nothing AndAlso Me.State.MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Paid) Then
                DisplayMessage(Message.MSG_PROMPT_CANCEL_SHIPMENT_NOT_ALLOWED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                State.MyBO.CancelShipmentRequest(State.MyBO.ClaimAuthorizationId)
                DisplayMessage(Message.MSG_PROMPT_FOR_CANCEL_SHIPMENT, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
            UndoChanges()
            PopulateFormFromBO()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub CheckCancelStatus()

        If (State.MyBO IsNot Nothing AndAlso (State.MyBO.IsCancelShipmentAllowed = Codes.EXT_YESNO_N Or State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_CANCEL_COMPLETE Or State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_CANCEL_REQUESTED Or State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_CANCEL_FAILD Or State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_CANCEL_PENDING Or State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_CANCEL_SENT)) Then
            ControlMgr.SetVisibleControl(Me, btnCancelShipment, False)
        Else
            ControlMgr.SetVisibleControl(Me, btnCancelShipment, True)

        End If
    End Sub

#End Region

#Region "Claim Authorization - Fulfillment Authorization Data"
    Private Shared Function GetClient() As FulfillmentServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE), False)
        Dim client = New FulfillmentServiceClient("CustomBinding_IFulfillmentService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password

        Return client
    End Function
    Private Sub GetAuthorizationFulfillmentData()
        
        Dim wsRequest As GetAuthorizationDetailsRequest = New GetAuthorizationDetailsRequest()

        wsRequest.CompanyCode = State.ClaimBO.Company.Code
        wsRequest.ClaimNumber = State.MyBO.ClaimNumber
        wsRequest.CultureCode = Threading.Thread.CurrentThread.CurrentCulture.Name
        wsRequest.AuthorizationNumber = State.MyBO.AuthorizationNumber
        
        Dim wsAuthDetailOptions() As AuthDetailOptions = {AuthDetailOptions.HistoryFlat, AuthDetailOptions.Issues}
        wsRequest.AuthDetails = wsAuthDetailOptions

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, GetAuthorizationDetailsResponse)(
                                                        GetClient(),
                                                        New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(c As FulfillmentServiceClient)
                                                            Return c.GetAuthorizationDetails(wsRequest)
                                                        End Function)
            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(GetAuthorizationDetailsResponse) Then
                    Dim wsResponseList As GetAuthorizationDetailsResponse = DirectCast(wsResponse, GetAuthorizationDetailsResponse)
                    If wsResponseList.ResponseStatus.Equals("Failure") Then
                        MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                        Exit Sub
                    End If
                    LoadFulfillmentIssues(wsResponseList)
                    LoadFulFillmentStatusHistory(wsResponseList)
                End If
            End If
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try

    End Sub

#End Region

#Region "Claim Authorization - Resend Shipping Label"
    Private Sub ResendShippingLabel()
        Dim wsRequest As UpdateServiceOrderRequest = New UpdateServiceOrderRequest()
        Dim ordInfo As OrderInfo = New OrderInfo()
        wsRequest.CompanyCode = State.ClaimBO.Company.Code
        wsRequest.ClaimNumber = State.ClaimBO.ClaimNumber
        ordInfo.OperationInstruction = RESEND_SHIPPING_LABEL
        ordInfo.OperationInstructionReason = RESEND_SHIPPING_LABEL
        ordInfo.AuthorizationNumber = State.MyBO.AuthorizationNumber
        ordInfo.ExternalOrderNumber = State.MyBO.ServiceCenterReferenceNumber
        wsRequest.OrderUpdate = ordInfo
        wsRequest.addrDetails = ADDR_DTL_CERT

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, ProcessServiceOrderResponse)(
                                                        GetClient(),
                                                        New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(c As FulfillmentServiceClient)
                                                            Return c.UpdateServiceOrder(wsRequest)
                                                        End Function)
            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(ProcessServiceOrderResponse) Then
                    Dim wsResponseList As ProcessServiceOrderResponse = DirectCast(wsResponse, ProcessServiceOrderResponse)
                    If wsResponseList.ResponseStatus.Equals("Failure") Then
                        'MasterPage.MessageController.MessageType.Error
                        MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try
    End Sub
    Private Sub CancelServiceOrder()
        Dim wsRequest As CancelServiceOrderRequest = New CancelServiceOrderRequest()
        Dim ordInfo As OrderInfo = New OrderInfo()
        wsRequest.CompanyCode = State.ClaimBO.Company.Code
        wsRequest.ClaimNumber = State.ClaimBO.ClaimNumber
        ordInfo.AuthorizationNumber = State.MyBO.AuthorizationNumber
        ordInfo.ExternalOrderNumber = State.MyBO.ServiceCenterReferenceNumber
        wsRequest.OrderUpdate = ordInfo
        wsRequest.AuthorizationId = State.MyBO.ClaimAuthorizationId

        Try
            Dim wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, ProcessServiceOrderResponse)(
                                                        GetClient(),
                                                        New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(c As FulfillmentServiceClient)
                                                            Return c.CancelServiceOrderAndUpdateAuthorization(wsRequest)
                                                        End Function)
            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(ProcessServiceOrderResponse) Then
                    Dim wsResponseList As ProcessServiceOrderResponse = DirectCast(wsResponse, ProcessServiceOrderResponse)
                    If wsResponseList.ResponseStatus.Equals("Failure") Then
                        MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try
    End Sub
#End Region

#Region "Claim Authorization - Fulfillment Authorization Status History"
    Private Function CreateTableFulFillmentStatusHistory() As DataTable
        ' Create a new table.
        Dim dtFulFillmentStatusHistory As New DataTable("FulFillmentStatusHistoryList")
        ' Create the columns.
        dtFulFillmentStatusHistory.Columns.Add("StatusDescription", GetType(String))
        dtFulFillmentStatusHistory.Columns.Add("SubStatusDescription", GetType(String))
        dtFulFillmentStatusHistory.Columns.Add("SubStatusDate", GetType(String))
        dtFulFillmentStatusHistory.Columns.Add("SubStatusReasonDescription", GetType(String))

        dtFulFillmentStatusHistory.Columns.Add("SubStatusCode", GetType(String))
        Return dtFulFillmentStatusHistory
    End Function
    Private Sub LoadFulFillmentStatusHistory(wsResponseList As GetAuthorizationDetailsResponse)
        State.FulFillmentStatusHistoryTable = CreateTableFulFillmentStatusHistory()
        For Each itemStatusHistory As ClaimFulfillmentService.AuthorizationInfo In wsResponseList.Authorization.StatusHistoryFlat
            Dim tableRow As DataRow = State.FulFillmentStatusHistoryTable.NewRow()
            tableRow("StatusDescription") = itemStatusHistory.StatusDescription
            tableRow("SubStatusDescription") = itemStatusHistory.SubStatusDescription
            tableRow("SubStatusCode") = itemStatusHistory.SubStatusCode
            tableRow("SubStatusDate") = GetLongDateFormattedStringNullable(If(itemStatusHistory.SubStatusDate Is Nothing, itemStatusHistory.StatusDate, itemStatusHistory.SubStatusDate))
            Dim builder As New StringBuilder
            For Each SubStatusReason As ClaimFulfillmentService.Reason In itemStatusHistory.SubStatusReasons
                builder.Append(SubStatusReason.Description).AppendLine()
            Next
            tableRow("SubStatusReasonDescription") = builder.ToString
            State.FulFillmentStatusHistoryTable.Rows.Add(tableRow)
        Next
    End Sub
    Private Sub BindGridClaimAuthStatusHistory()
        Try
            If State.FulFillmentStatusHistoryTable Is Nothing OrElse State.FulFillmentStatusHistoryTable.Rows.Count = 0 Then
                If State.FulFillmentStatusHistoryTable Is Nothing Then
                    State.FulFillmentStatusHistoryTable = CreateTableFulFillmentStatusHistory()
                End If
                State.FulFillmentStatusHistoryTable.Rows.InsertAt(State.FulFillmentStatusHistoryTable.NewRow(), 0)
                GridClaimAuthStatusHistory.DataSource = State.FulFillmentStatusHistoryTable
                GridClaimAuthStatusHistory.DataBind()
                GridClaimAuthStatusHistory.Rows(0).Visible = False
            Else
                GridClaimAuthStatusHistory.DataSource = State.FulFillmentStatusHistoryTable
                GridClaimAuthStatusHistory.DataBind()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateGridClaimAuthStatusHistory()
        Try
            BindGridClaimAuthStatusHistory()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
#Region "Claim Authorization - Fulfillment Authorization Issues"
#Region "Constants"
    Private Const GridColFulfillmentIssueAction As Integer = 4
#End Region
    Private Function CreateTableFulfillmentIssues() As DataTable
        ' Create a new table.
        Dim dtFulfillmentIssues As New DataTable("FulfillmentIssuesTableList")

        ' Create the columns.
        dtFulfillmentIssues.Columns.Add("EntityIssueId", GetType(Guid))
        dtFulfillmentIssues.Columns.Add("IssueId", GetType(Guid))
        dtFulfillmentIssues.Columns.Add("IssueDescription", GetType(String))
        dtFulfillmentIssues.Columns.Add("IssueStatusDescription", GetType(String))
        dtFulfillmentIssues.Columns.Add("IssueStatusDate", GetType(String))
        dtFulfillmentIssues.Columns.Add("IssueActionDescription", GetType(String))
        dtFulfillmentIssues.Columns.Add("IssueActionCode", GetType(String))
        Return dtFulfillmentIssues
    End Function
    Private Sub LoadFulfillmentIssues(wsResponseList As GetAuthorizationDetailsResponse)

        State.FulfillmentIssuesTable = CreateTableFulfillmentIssues()

        For Each itemIssue As Issue In wsResponseList.Authorization.Issues
            If itemIssue.Actions IsNot Nothing AndAlso itemIssue.Actions.Count > 0 Then
                For Each itemIssueAction As IssueAction In itemIssue.Actions
                    Dim tableRow As DataRow = State.FulfillmentIssuesTable.NewRow()
                    tableRow("EntityIssueId") = itemIssue.EntityIssueId
                    tableRow("IssueId") = itemIssue.IssueId
                    tableRow("IssueDescription") = itemIssue.Description
                    If itemIssue.Status IsNot Nothing Then
                        tableRow("IssueStatusDescription") = itemIssue.Status.Description
                        tableRow("IssueStatusDate") = GetLongDateFormattedStringNullable(itemIssue.Status.Date)
                    End If
                    tableRow("IssueActionDescription") = itemIssueAction.Description
                    tableRow("IssueActionCode") = itemIssueAction.Code
                    State.FulfillmentIssuesTable.Rows.Add(tableRow)
                Next
            Else
                ' if no action exist for the issue
                Dim tableRow As DataRow = State.FulfillmentIssuesTable.NewRow()
                tableRow("EntityIssueId") = itemIssue.EntityIssueId
                tableRow("IssueId") = itemIssue.IssueId
                tableRow("IssueDescription") = itemIssue.Description
                If itemIssue.Status IsNot Nothing Then
                    tableRow("IssueStatusDescription") = itemIssue.Status.Description
                    tableRow("IssueStatusDate") = GetLongDateFormattedStringNullable(itemIssue.Status.Date)
                End If
                State.FulfillmentIssuesTable.Rows.Add(tableRow)
            End If
        Next
    End Sub
    Private Sub BindGridFulfillmentIssues()
        Try
            If State.FulfillmentIssuesTable Is Nothing OrElse State.FulfillmentIssuesTable.Rows.Count = 0 Then
                If State.FulfillmentIssuesTable Is Nothing Then
                    State.FulfillmentIssuesTable = CreateTableFulfillmentIssues()
                End If
                State.FulfillmentIssuesTable.Rows.InsertAt(State.FulfillmentIssuesTable.NewRow(), 0)
                GridViewClaimAuthFulfillmentIssues.DataSource = State.FulfillmentIssuesTable
                GridViewClaimAuthFulfillmentIssues.DataBind()
                GridViewClaimAuthFulfillmentIssues.Rows(0).Visible = False
            Else
                GridViewClaimAuthFulfillmentIssues.DataSource = State.FulfillmentIssuesTable
                GridViewClaimAuthFulfillmentIssues.DataBind()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateGridFulfillmentIssues()
        Try
            BindGridFulfillmentIssues()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewClaimAuthFulfillmentIssues_OnDataBound(sender As Object, e As EventArgs) Handles GridViewClaimAuthFulfillmentIssues.DataBound
        For i As Integer = GridViewClaimAuthFulfillmentIssues.Rows.Count - 1 To 1 Step -1
            Dim row As GridViewRow = GridViewClaimAuthFulfillmentIssues.Rows(i)
            Dim previousRow As GridViewRow = GridViewClaimAuthFulfillmentIssues.Rows(i - 1)
            For j As Integer = 0 To row.Cells.Count - 1
                If row.Cells(j).Text = previousRow.Cells(j).Text Then
                    ' Same issue with multiple action, so merge
                    If j <> GridColFulfillmentIssueAction Then ' do not perform for Issue Action Column
                        If previousRow.Cells(j).RowSpan = 0 Then
                            If row.Cells(j).RowSpan = 0 Then
                                previousRow.Cells(j).RowSpan += 2
                            Else
                                previousRow.Cells(j).RowSpan = row.Cells(j).RowSpan + 1
                            End If
                            row.Cells(j).Visible = False
                        End If
                    End If
                Else
                    ' Different issue, so do not merge
                    Exit For
                End If
            Next
        Next
    End Sub
    Private Sub GridViewClaimAuthFulfillmentIssues_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewClaimAuthFulfillmentIssues.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton

            If dvRow IsNot Nothing Then
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (e.Row.Cells(GridColFulfillmentIssueAction).FindControl("LinkButtonIssueActionDescription") IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GridColFulfillmentIssueAction).FindControl("LinkButtonIssueActionDescription"), LinkButton)
                        btnEditItem.CommandArgument = dvRow("EntityIssueId").ToString
                        btnEditItem.CommandName = dvRow("IssueActionCode").ToString ' "LFLDEVSEL" '
                        btnEditItem.Text = dvRow("IssueActionDescription").ToString
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub GridViewClaimAuthFulfillmentIssues_RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewClaimAuthFulfillmentIssues.RowCommand
        Try
            If Not String.IsNullOrEmpty(e.CommandName) _
                AndAlso Not String.IsNullOrEmpty(e.CommandArgument.ToString()) _
                AndAlso Not String.IsNullOrEmpty(State.ClaimBO.Dealer.Dealer) _
                AndAlso Not State.ClaimBO.Id.Equals(Guid.Empty) Then
                Dim IssueActionCode As String = e.CommandName
                Dim EntityIssueId As Guid = New Guid(e.CommandArgument.ToString())
                State.IsReturnedFromIssueActionAnswer = True
                NavController.Navigate(Me, "claim_issue_action", New ClaimIssueActionAnswerForm.Parameters(State.ClaimBO, EntityIssueId, IssueActionCode, State.MyBO.Id))
                'callPage(ClaimIssueActionAnswerForm.Url, New ClaimIssueActionAnswerForm.Parameters(State.ClaimBO, EntityIssueId, IssueActionCode, State.MyBO.Id))
                Return
            End If
        Catch ex As ThreadAbortException
            ' Do nothing
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Reshipment"
    Private Sub btnReshipment_Click(sender As System.Object, e As System.EventArgs) Handles btnReshipment.Click
        Try
            State.MyBO.SubStatusReason = String.Empty

            ''set the reshipment reason to DOA as default
            'reshipmentReasonDrop.SelectedValue = "DOA"

            If (State.ClaimBO.Dealer.ClaimRecordingCheckInventoryXcd.Equals("YESNO-Y")) Then
                GetInventoryAndBestDeviceData()
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('ModalReshipment') </script>"
            RegisterStartupScript("Startup", x)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnReshipmentProceed_Click(sender As System.Object, e As System.EventArgs) Handles btnReshipmentProceed.Click
        Try
            PopulateBOFromFormForSubStatus()
            Dim oguid As String = "00000000-0000-0000-0000-000000000000"
            If Not State.MyBO.SubStatusReason = oguid Then
                State.MyBO.ReShipmentProcessRequest(State.MyBO.ClaimAuthorizationId, State.MyBO.SubStatusReason)
                ControlMgr.SetVisibleControl(Me, btnReshipment, False)
                DisplayMessage(Message.MSG_PROMPT_FOR_RESHIPMENT, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_PROMPT_RESHIPMENT_REASON, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
            InventoryDeduction()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub InventoryDeduction()
        Dim rowIndex As Int16 = -1
        Dim gvr As GridViewRow
        For Each gvr In GridViewBestDeviceSelection.Rows
            Dim rb As RadioButton
            rb = CType(gvr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl), RadioButton)
            rowIndex = gvr.RowIndex
            If rb.Checked Then
                State.MyBO.SaveClaimReplaceOptions(State.MyBO.ClaimId, State.BestReplacementDeviceSelected.VendorInventoryList(rowIndex).EquipmentId, State.BestReplacementDeviceSelected.VendorInventoryList(rowIndex).Priority, State.BestReplacementDeviceSelected.VendorInventoryList(rowIndex).VendorSku, "Y", State.BestReplacementDeviceSelected.VendorInventoryList(rowIndex).InventoryId, State.MyBO.CreatedById, State.MyBO.ClaimAuthorizationId)
            End If
        Next
    End Sub

    Private Sub PopulateReshipmentReason()
        reshipmentReasonDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RESHIPRSN", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True,
                .ValueFunc = AddressOf .GetCode
            })
    End Sub
    Private Sub CheckReshipmentStatus()

        If (State.MyBO IsNot Nothing AndAlso Me.State.MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled AndAlso State.MyBO.IsReshipmentAllowed = Codes.EXT_YESNO_Y AndAlso (State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_REPLACEMENT Or State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPLACEMENT) AndAlso (Not State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_RESHIPMENT_REQ Or Not State.MyBO.AuthSubStatus = Codes.CLM_AUTH_SUBSTAT_SOSUBMIT) AndAlso State.MyBO.LinkedClaimAurthID = Guid.Empty _
                AndAlso Not State.MyBO.CheckLinkedAuthItem(State.MyBO.ClaimAuthorizationId) = True) Then
            btnReshipment.Visible = True
            ControlMgr.SetVisibleControl(Me, btnReshipment, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnReshipment, False)

        End If
    End Sub
    Private Sub CheckResendShippingLabelStatus()
        'if claim is active and claim auth fulfillment type is 'Repair' or 'service warranty-repair' or 'service warranty-replacement', then display button
        If (State.MyBO IsNot Nothing AndAlso Me.State.ClaimBO.Status = BasicClaimStatus.Active _
            AndAlso (State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_REPAIR _
                     Or State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SWRPR _
                     Or State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SWRPL) _
            AndAlso (State.ClaimBO.Dealer.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS)) Then
            btnResendShippingLabel.Visible = True
            ControlMgr.SetVisibleControl(Me, btnResendShippingLabel, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnResendShippingLabel, False)
        End If
    End Sub
    Private Sub CheckCancelServiceOrderStatus()
        'if claim is active,Claim Authorization Status='Sent',then display button
        If State.MyBO IsNot Nothing AndAlso Me.State.ClaimBO.Status = BasicClaimStatus.Active _
           AndAlso (Me.State.MyBO.ClaimAuthStatus = ClaimAuthorizationStatus.Sent) _
           AndAlso (State.ClaimBO.Dealer.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS) Then
            btnCancelServiceOrder.Visible = True
            ControlMgr.SetVisibleControl(Me, btnCancelServiceOrder, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnCancelServiceOrder, False)
        End If
    End Sub
    Private Sub PopulateBOFromFormForSubStatus()
        PopulateBOProperty(State.MyBO, "SubStatusReason", reshipmentReasonDrop, False, True)
    End Sub

#End Region


#Region "GetInventoryAndBestDeviceData"
    Private Sub GetInventoryAndBestDeviceData()
        Dim wsRequest As CheckVendorInventoryAndBestReplacementRequest = New CheckVendorInventoryAndBestReplacementRequest()

        wsRequest.DealerCode = State.ClaimBO.Dealer.Dealer
        wsRequest.Make = State.MyBO.Claim.ClaimedEquipment.Manufacturer
        wsRequest.Model = State.MyBO.Claim.ClaimedEquipment.Model
        wsRequest.ReserveInventory = True
        wsRequest.ReturnBestReplacement = True
        wsRequest.ServiceCenterCode = State.MyBO.ServiceCenter.Code

        Dim wsResponse As CheckVendorInventoryAndBestReplacementResponse

        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, CheckVendorInventoryAndBestReplacementResponse)(
                                                        GetClient(),
                                                        New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                        Function(c As FulfillmentServiceClient)
                                                            Return c.CheckVendorInventoryAndBestReplacement(wsRequest)
                                                        End Function)
            State.BestReplacementDeviceSelected = wsResponse
            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(CheckVendorInventoryAndBestReplacementResponse) Then
                    Dim wsResponseList As CheckVendorInventoryAndBestReplacementResponse = DirectCast(wsResponse, CheckVendorInventoryAndBestReplacementResponse)
                    If wsResponseList.ResponseStatus.Equals("Failure") Then
                        'MasterPage.MessageController.MessageType.Error
                        MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                        Exit Sub
                    End If

                    PopulateDeviceSelectionGrid(wsResponseList)
                    PopulateBestDeviceSelectionGrid(wsResponseList)

                End If
            End If
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try
    End Sub

#End Region


    Private Sub PopulateDeviceSelectionGrid(wsResponseList As CheckVendorInventoryAndBestReplacementResponse)

        GridViewDeviceSelection.AutoGenerateColumns = False
        If wsResponseList.DeviceInventory Is Nothing Then
            ControlMgr.SetVisibleControl(Me, GridViewDeviceSelection, False)
            ControlMgr.SetVisibleControl(Me, lblDeviceSearchResults, False)
        Else
            ControlMgr.SetVisibleControl(Me, GridViewDeviceSelection, True)
            ControlMgr.SetVisibleControl(Me, lblDeviceSearchResults, True)
        End If

        Dim Devicelist As New List(Of VendorInventory)
        Devicelist.Add(wsResponseList.DeviceInventory)

        GridViewDeviceSelection.DataSource = Devicelist
        GridViewDeviceSelection.DataBind()

    End Sub

    Private Sub PopulateBestDeviceSelectionGrid(wsResponseList As CheckVendorInventoryAndBestReplacementResponse)

        GridViewBestDeviceSelection.AutoGenerateColumns = False

        If wsResponseList.VendorInventoryList Is Nothing Then
            ControlMgr.SetVisibleControl(Me, GridViewBestDeviceSelection, False)
            ControlMgr.SetVisibleControl(Me, lblBestReplacementDevice, False)
        Else
            ControlMgr.SetVisibleControl(Me, GridViewBestDeviceSelection, True)
            ControlMgr.SetVisibleControl(Me, lblBestReplacementDevice, True)

            GridViewBestDeviceSelection.DataSource = wsResponseList.VendorInventoryList.ToList()
            GridViewBestDeviceSelection.DataBind()
            GridviewCheckQuantity()
        End If

    End Sub

    Private Sub GridviewCheckQuantity()
        'Select the radiobutton in gridview
        Dim gvr As GridViewRow
        If GridViewBestDeviceSelection.Rows.Count > 0 Then
            For Each gvr In GridViewBestDeviceSelection.Rows
                If Not (gvr.Cells(GridColQuantityIdx).Text).Equals(String.Empty) Then
                    If Convert.ToInt16(gvr.Cells(GridColQuantityIdx).Text) = 0 Then
                        Dim rb As RadioButton
                        rb = CType(gvr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl), RadioButton)
                        rb.Enabled = True
                    End If
                End If
            Next
        End If

    End Sub

    Protected Sub rdoDevice_CheckedChanged(sender As Object, e As EventArgs)
        Dim gvr As GridViewRow
        If GridViewBestDeviceSelection.Rows.Count > 0 Then
            For Each gvr In GridViewBestDeviceSelection.Rows
                Dim rb As RadioButton
                rb = CType(gvr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl), RadioButton)
                rb.Checked = False
            Next
        End If
        Dim x As String = "<script language='JavaScript'> revealModal('ModalReshipment') </script>"
        RegisterStartupScript("Startup", x)

    End Sub

    Protected Sub rdoDevice_CheckedChanged1(sender As Object, e As EventArgs)
        Dim gvr As GridViewRow
        If GridViewDeviceSelection.Rows.Count > 0 Then
            For Each gvr In GridViewDeviceSelection.Rows
                Dim rb As RadioButton
                rb = CType(gvr.Cells(GridColDeviceIdx).FindControl(GridColDeviceRdoCtrl), RadioButton)
                rb.Checked = False
            Next
        End If

        Dim gvr1 As GridViewRow
        Dim i As Int32
        For Each gvr1 In GridViewBestDeviceSelection.Rows
            Dim rb As RadioButton
            rb = CType(GridViewBestDeviceSelection.Rows(i).FindControl(GridColDeviceRdoCtrl), RadioButton)
            rb.Checked = False
            i += 1
        Next
        Dim SenderRB As RadioButton = sender

        SenderRB.Checked = True
        Dim x As String = "<script language='JavaScript'> revealModal('ModalReshipment') </script>"
        RegisterStartupScript("Startup", x)

    End Sub
    Private Sub CheckPayCashStatus()

        Dim blnEnabled As Boolean = False
        Dim dealerBO As Dealer = State.ClaimBO.Dealer

        ' Check dealer attribute
        If dealerBO.AttributeValues.Contains(Codes.DLR_ATTR_MANUAL_CLAIM_CASH_PYMT) AndAlso dealerBO.AttributeValues.Value(Codes.DLR_ATTR_MANUAL_CLAIM_CASH_PYMT) = Codes.YESNO_Y Then
            If (State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_REPLACEMENT OrElse State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_REPAIR OrElse State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPAIR OrElse State.MyBO.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPLACEMENT) Then
                If (State.MyBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED OrElse State.MyBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__PENDING OrElse State.MyBO.ClaimAuthorizationStatusCode = Codes.CLAIM_AUTHORIZATION_STATUS__SENT) Then
                    blnEnabled = True
                End If
            End If
        End If

        If blnEnabled Then
            'check whether pay cash before
            For Each auth As ClaimAuthorization In State.ClaimBO.ClaimAuthorizationChildren
                If auth.LinkedClaimAurthID = State.MyBO.Id AndAlso auth.ClaimAuthfulfillmentTypeXcd = Codes.AUTH_FULFILLMENT_TYPE_CASH_REIMBURSEMENT Then
                    blnEnabled = False
                End If
            Next
        End If

        ControlMgr.SetVisibleControl(Me, btnPayCash, blnEnabled)
    End Sub
    Protected Sub btnPayCash_Click(sender As Object, e As EventArgs) Handles btnPayCash.Click
        Dim errCode As Integer
        Dim errMsg As String
        If State.MyBO.ManualCashpayRequest(State.MyBO.ClaimAuthorizationId, State.ClaimBO.BankInfoId, errCode, errMsg) Then
            MasterPage.MessageController.AddSuccess("NEW_AUTHORIZATION_ADD")
        Else
            MasterPage.MessageController.AddError("Error Code: " & errCode & " - " & errMsg, False)
        End If
    End Sub

    Private Sub btnRefundFeeSave_Click(sender As Object, e As EventArgs) Handles btnRefundFeeSave.Click
        Dim errCode As Integer
        Dim errMsg As String
        If GetSelectedItem(cboRefundReason).Equals(Guid.Empty) Then
            ElitaPlusPage.SetLabelError(lblRefundReason)
            MasterPage.MessageController.AddError("Refund Reason is Required")
            Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR)
        Else
            Try
                State.MyBO.Reversed = True
                State.MyBO.RevAdjustmentReasonId = GetSelectedItem(cboRefundReason)
                State.MyBO.RefundAmount()
                State.MyBO.Reversed = False
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(State.ClaimBO.Id)
                State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.MyBO.Id), ClaimAuthorization)
                PopulateFormFromBO()
                EnableDisablePageControls()
                MasterPage.MessageController.AddSuccess("AMT_REFUNDED_NEW_AUTH_ITEM_ADD")
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_

            Catch ex As Exception
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(State.ClaimBO.Id)
                State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.MyBO.Id), ClaimAuthorization)
                PopulateFormFromBO()
                EnableDisablePageControls()
                MasterPage.MessageController.AddError("AMT_NOT_REFUNDED")
            End Try

        End If


    End Sub
#Region "Change Service Center"
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Try
            Dim serviceTypeId As Guid = LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE)
            Dim oWebPassword As WebPasswd = New WebPasswd(Guid.Empty, serviceTypeId, False)
            If oWebPassword Is Nothing Then Throw New ArgumentNullException($"Web Password information for service {Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE} does not exists.")

            Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPassword.Url)
            client.ClientCredentials.UserName.UserName = oWebPassword.UserId
            client.ClientCredentials.UserName.Password = oWebPassword.Password

            Return client
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub InitNewServiceCenterUserControl()
        'return if the control already initialized
        If String.IsNullOrEmpty(ucSelectServiceCenter.MethodOfRepairXcd) = False Then Exit Sub

        'Set up the service center start
        ucSelectServiceCenter.HostMessageController = MasterPage.MessageController

        ucSelectServiceCenter.TranslationFunc = Function(value As String)
                                                    Return TranslationBase.TranslateLabelOrMessage(value)
                                                End Function

        ucSelectServiceCenter.TranslateGridHeaderFunc = Sub(grid As System.Web.UI.WebControls.GridView)
                                                            TranslateGridHeader(grid)
                                                        End Sub

        ucSelectServiceCenter.HighLightSortColumnFunc = Sub(grid As System.Web.UI.WebControls.GridView, sortExp As String)
                                                            HighLightSortColumn(grid, sortExp, False)
                                                        End Sub

        ucSelectServiceCenter.NewCurrentPageIndexFunc = Function(grid As System.Web.UI.WebControls.GridView, intRecordCount As Integer, intNewPageSize As Integer)
                                                            Return NewCurrentPageIndex(grid, intRecordCount, intNewPageSize)
                                                        End Function
        'Set up the service center end

        Dim oCountry As Country = New Country(State.ClaimBO.Company.CountryId)

        ucSelectServiceCenter.PageSize = 30
        ucSelectServiceCenter.CountryId = oCountry.Id
        ucSelectServiceCenter.CountryCode = oCountry.Code
        ucSelectServiceCenter.CompanyCode = State.ClaimBO.Company.Code
        ucSelectServiceCenter.Dealer = State.ClaimBO.Certificate.Dealer.Dealer
        ucSelectServiceCenter.Make = State.ClaimBO.ClaimedEquipment.Manufacturer
        ucSelectServiceCenter.RiskTypeEnglish = State.ClaimBO.RiskType

        If State.ClaimBO.ContactInfo IsNot Nothing Then
            ucSelectServiceCenter.City = State.ClaimBO.ContactInfo.Address.City
            ucSelectServiceCenter.PostalCode = State.ClaimBO.ContactInfo.Address.PostalCode
        End If

        ucSelectServiceCenter.MethodOfRepairXcd = "METHR-" + State.ClaimBO.MethodOfRepairCode
        ucSelectServiceCenter.InitializeComponent()
    End Sub
    Private Sub btnNewServiceCenter_Click(sender As System.Object, e As System.EventArgs) Handles btnNewServiceCenter.Click
        Try
            InitNewServiceCenterUserControl()

            'show the div in Modal mode
            HiddenFieldShowNewSC.Value = "Y"

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRepairCodeProcess_Click(sender As System.Object, e As System.EventArgs) Handles btnRepairCodeProcess.Click
        Try
            InitRepairCodeProcess()
            HiddenFieldRepairCodeProcess.Value = "Y"

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub btnNewSCSave_Click(sender As Object, e As EventArgs) Handles btnNewSCSave.Click
        Dim blnValid As Boolean = True, strErrMsg As String = String.Empty
        Dim decAmount As Decimal = 0, strErrMsgAmt As String = String.Empty

        If ucSelectServiceCenter.SelectedServiceCenter Is Nothing Then
            blnValid = False
            strErrMsg = "SERVICE_CENTER_MUST_BE_SELECTED_ERR"
        Else
            If ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterId = State.MyBO.ServiceCenterId Then
                blnValid = False
                strErrMsg = "EXISTING_SERVICE_CENTER_SELECTED"
            End If
        End If

        If String.IsNullOrEmpty(txtNewSCAmt.Text.Trim()) = False Then
            If Decimal.TryParse(txtNewSCAmt.Text.Trim(), decAmount) = False Then
                blnValid = False
                strErrMsgAmt = "INVALID_AMOUNT_ENTERED"
            End If
        End If

        If blnValid Then
            'dismiss the popup window
            HiddenFieldShowNewSC.Value = "N"
            lblNewSCError.Visible = False
            lblNewSCError.Text = String.Empty

            'call web gateway service to process the change service center request
            Dim wsRequest As New ChangeServiceCenterRequest
            With wsRequest
                .CompanyCode = State.ClaimBO.Company.Code
                .ClaimNumber = State.ClaimBO.ClaimNumber
                .AuthorizationLocator = State.MyBO.Locator
                .NewServiceCenterCountryCode = ucSelectServiceCenter.CountryCode
                .NewServiceCenterCode = ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterCode
                If decAmount <> 0 Then
                    .Amount = decAmount
                End If
            End With
            Try
                Dim wsResponse As ChangeServiceCenterResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, ChangeServiceCenterResponse)(
                    GetClaimFulfillmentWebAppGatewayClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(c As WebAppGatewayClient)
                        Return c.ChangeServiceCenter(wsRequest)
                    End Function)

                'success, close the pop-up windows and refresh the screen and show successful message
                HiddenFieldShowNewSC.Value = "N"
                State.MyBO = New ClaimAuthorization(State.MyBO.Id)
                'Me.State.MyBO = CType(Me.State.ClaimBO.ClaimAuthorizationChildren.GetChild(Me.State.MyBO.Id), ClaimAuthorization)
                PopulateFormFromBO()
                EnableDisablePageControls()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

            Catch ex As Exception
                lblNewSCError.Visible = True
                lblNewSCError.Text = "Calling Web Gateway service ChangeServiceCenter failed. error message: " + ex.Message
            End Try
        Else
            'show error and keep the popup window open
            lblNewSCError.Visible = True
            lblNewSCError.Text = String.Empty

            If String.IsNullOrEmpty(strErrMsg) = False Then
                lblNewSCError.Text = TranslationBase.TranslateLabelOrMessage(strErrMsg) + ". "
            End If
            If String.IsNullOrEmpty(strErrMsgAmt) = False Then
                lblNewSCError.Text = lblNewSCError.Text + TranslationBase.TranslateLabelOrMessage(strErrMsgAmt) + "."
            End If
        End If
    End Sub

#End Region

    #Region "Void Claim Authorization"
    Private Sub btnVoidAuthorization_Click(sender As System.Object, e As System.EventArgs) Handles btnVoidAuthorization.Click
        Try
            InitVoidAuthorization()
            HiddenFieldVoidAuth.Value = "Y"

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub InitVoidAuthorization()

        divVoidAuthStatus.Visible = False
        divVoidAuthError.Visible = False
        ControlMgr.SetVisibleControl(Me, btnVoidAuthSave, True)
        ControlMgr.SetVisibleControl(Me, btnVoidAuthClose, False)
    End Sub


    Private Sub InitRepairCodeProcess()

        divRepairCodeProcessStatus.Visible = False
        divRepairCodeProcessError.Visible = False
        rdbRepairQuoteStatus.ClearSelection()
        txtRepairQuote.Text = ""
        lblRepairCodeProcessStatus.Text = ""
        lblRepairCodeProcessError.Text = ""

        ControlMgr.SetVisibleControl(Me, btnRepairCodeProcessSave, True)
        ControlMgr.SetVisibleControl(Me, btnRepairCodeProcessClose, False)
    End Sub

    Private sub btnVoidAuthSave_Click (sender As Object, e As EventArgs) Handles btnVoidAuthSave.Click
        Try
           
            divVoidAuthStatus.Visible = False
            divVoidAuthError.Visible = False

            If String.IsNullOrWhiteSpace(txtAuthVoidComment.Text) Then

                lblvoidAuthError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_AUTH_VOID_COMMENT_REQUIRED)
                divVoidAuthError.Visible = True
                return
            End If
            
           If State.MyBO.IsFamilyDirty Then

                lblVoidAuthStatus.Visible = True

                If String.IsNullOrEmpty(State.ClaimBO.ProblemDescription) Then
                    State.ClaimBO.ProblemDescription = "Problem Description Missing"
                End If

                State.MyBO.Void()
                State.ClaimBO.Save()
                AddCommentToClaim(txtAuthVoidComment.Text, State.ClaimBO)

                lblVoidAuthStatus.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_AUTH_VOIDED)
                divVoidAuthStatus.Visible = True

                ControlMgr.SetVisibleControl(Me, btnVoidAuthSave, False)
                ControlMgr.SetVisibleControl(Me, btnVoidAuthCancel, False)
                ControlMgr.SetVisibleControl(Me, btnVoidAuthClose, True)


                If CloseClaimAsPaid() Then
                    State.ClaimBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                ElseIf CloseClaimAsVoid() Then
                    State.ClaimBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED_CLAIM_VOID)
                End If

                If Not State.ClaimBO.ReasonClosedId = Guid.Empty Then
                    State.ClaimBO.CloseTheClaim()
                    State.ClaimBO.Save()
                    lblVoidAuthStatus.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_AUTH_VOIDED_AND_CLAIM_CLOSED)
                End If

            Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            lblvoidAuthError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_AUTH_VOID_FAIED) & ex.Message
            divVoidAuthError.Visible = True
            HandleErrors(ex, MasterPage.MessageController)

        End Try
        
    End Sub


    Private Function CloseClaimAsVoid() As Boolean
       If State.ClaimBO.NonVoidClaimAuthorizationList.Any(Function(i) i.AuthorizationNumber <> State.MyBO.AuthorizationNumber AndAlso i.ClaimAuthStatus <> ClaimAuthorizationStatus.Cancelled) Then
            return false
        Else 
            Return True
        End If
    End Function

    Private Function CloseClaimAsPaid() As Boolean
        If State.ClaimBO.NonVoidClaimAuthorizationList.Any(Function(i) Not i.AuthorizationNumber.Equals(State.MyBO.AuthorizationNumber)) Then
            If State.ClaimBO.NonVoidClaimAuthorizationList.Any(Function(i) i.AuthorizationNumber <> State.MyBO.AuthorizationNumber AndAlso i.ClaimAuthStatus <> ClaimAuthorizationStatus.Paid) Then
                return false
            Else 
                Return True
            End If
        Else
                return False
        End If
    End Function

    Protected Sub btnVoidAuthClose_Click(sender As Object, e As EventArgs) Handles btnVoidAuthClose.Click
        HiddenFieldVoidAuth.Value = "N"
        ReturnBackToCallingPage()
    End Sub
    Protected Sub HandleCloseClaimLogic()
        'Try to Close the Claim
        Select Case State.ClaimBO.ClaimAuthorizationType
            Case ClaimAuthorizationType.Single
                State.ClaimBO.CloseTheClaim()
            Case ClaimAuthorizationType.Multiple
                If Not ( State.ClaimBO.Status = BasicClaimStatus.Closed Or State.ClaimBO.Status = BasicClaimStatus.Denied) Then
                    If Not  State.ClaimBO.ReasonClosedId.Equals(Guid.Empty) Then
                        If  State.ClaimBO.ClaimAuthorizationChildren.Where(Function(item) item.ClaimAuthStatus = ClaimAuthorizationStatus.Paid Or
                                                                                             item.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid Or
                                                                                             item.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled).Count > 0 Then
                            Throw New GUIException("CLAIM_CANNOT_BE_CLOSED_CONTAINS_RECONSILED_PAID_AUTH", "CLAIM_CANNOT_BE_CLOSED_CONTAINS_RECONSILED_PAID_AUTH")
                        End If
                    End If

                End If
                State.ClaimBO.CloseTheClaim()
                If  State.ClaimBO.Status = BasicClaimStatus.Closed Then
                    State.ClaimBO.VoidAuthorizations()
                End If
            Case ClaimAuthorizationType.None
                Throw New NotSupportedException
        End Select

    End Sub
    Private Sub AddCommentToClaim(comments As String, oclaim As ClaimBase)
        If (oclaim IsNot Nothing) Then
            Dim comment As Comment = oclaim.AddNewComment()

            With comment
                .ClaimId = oclaim.Id
                .CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__OTHER)
                .CertId = oclaim.Certificate.Id
                .Comments = comments
                .AddClaimAuthComment()
            End With

        End If
    End Sub

    #End Region

    Private Sub btnResendShippingLabel_Click(sender As Object, e As EventArgs) Handles btnResendShippingLabel.Click
        Try
            ResendShippingLabel()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnCancelServiceOrder_Click(sender As Object, e As EventArgs) Handles btnCancelServiceOrder.Click
        Try
            CancelServiceOrder()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnRepairCodeProcessSave_Click(sender As Object, e As EventArgs) Handles btnRepairCodeProcessSave.Click
        UpdateRepairCodeProcess()
    End Sub

    Private Sub UpdateRepairCodeProcess()
        '      

        If (rdbRepairQuoteStatus.SelectedValue = "RQAPT" Or rdbRepairQuoteStatus.SelectedValue = "RQRJT" Or rdbRepairQuoteStatus.SelectedValue = "") And
            (txtRepairQuote.Text = "" Or txtRepairQuote.Text Is Nothing Or String.IsNullOrWhiteSpace(txtRepairQuote.Text)) Then

            divRepairCodeProcessError.Visible = True
            lblRepairCodeProcessError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_NOT_SELECTED)

        ElseIf CDbl(txtRepairQuote.Text) <= 0 Then


            divRepairCodeProcessError.Visible = True
            lblRepairCodeProcessError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_NOT_SELECTED)

        ElseIf (rdbRepairQuoteStatus.SelectedValue = "RQAPT" Or rdbRepairQuoteStatus.SelectedValue = "RQRJT") And CDbl(txtRepairQuote.Text) > 0 Then

            divRepairCodeProcessStatus.Visible = False
            divRepairCodeProcessError.Visible = False

            Try

                Dim wsRequest As OrderStatusRequest = New OrderStatusRequest()
                Dim costlist As New List(Of costInfo)

                With wsRequest
                    .CompanyCode = State.ClaimBO.Company.Code
                    .ClaimNumber = State.MyBO.ClaimNumber
                    .AuthNumber = State.MyBO.AuthorizationNumber

                    If State.MyBO.ServiceCenterReferenceNumber Is Nothing Then
                        .OrderNumber = State.MyBO.AuthorizationNumber
                        .ExternalOrderNumber = State.MyBO.AuthorizationNumber

                    Else
                        .OrderNumber = State.MyBO.ServiceCenterReferenceNumber
                        .ExternalOrderNumber = State.MyBO.ServiceCenterReferenceNumber

                    End If

                    .OrderStatus = rdbRepairQuoteStatus.SelectedValue

                    costlist.Add(New costInfo() With {.Type = CostType.RepairCost, .Amount = CDbl(txtRepairQuote.Text)})

                    .AdditionalInfo = New ClaimFulfillmentService.AdditionalInfo()
                    .AdditionalInfo.FinancialInfo = costlist.ToArray()

                End With

                Dim wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, OrderStatusResponse)(
                    GetClient(),
                    New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(c As FulfillmentServiceClient)
                        Return c.UpdateOrderStatus(wsRequest)
                    End Function)

                If wsResponse IsNot Nothing Then
                    If wsResponse.GetType() Is GetType(OrderStatusResponse) Then
                        Dim wsResponseList As OrderStatusResponse = DirectCast(wsResponse, OrderStatusResponse)
                        If wsResponseList.ResponseStatus.Equals("Failure") Then
                            MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)

                            lblRepairCodeProcessError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_FAILURE & wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage)
                            divRepairCodeProcessError.Visible = True

                            HiddenFieldRepairCodeProcess.Value = "N"
                            MasterPage.MessageController.AddError(Message.MSG_REPAIR_QUOTE_FAILURE)
                            Exit Sub
                        Else

                            If rdbRepairQuoteStatus.SelectedValue = "RQAPT" Then

                                lblRepairCodeProcessStatus.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_ACCEPT)
                                divRepairCodeProcessStatus.Visible = True


                                MasterPage.MessageController.AddSuccess(Message.MSG_REPAIR_QUOTE_ACCEPT)

                                btnRepairCodeProcess.Visible = False

                            Else
                                lblRepairCodeProcessStatus.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_REJECT)
                                divRepairCodeProcessStatus.Visible = True

                                MasterPage.MessageController.AddSuccess(Message.MSG_REPAIR_QUOTE_REJECT)

                            End If
                        End If

                        HiddenFieldRepairCodeProcess.Value = "N"
                        State.MyBO = New ClaimAuthorization(State.MyBO.Id)
                        State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(State.ClaimBO.Id)

                        PopulateFormFromBO()
                        GetAuthorizationFulfillmentData()
                        InitializeFulfillmentIssueStatusUI()

                    End If
                End If



            Catch ex As Exception
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
                Throw
            End Try

        Else

            divRepairCodeProcessError.Visible = True
            lblRepairCodeProcessError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_REPAIR_QUOTE_NOT_SELECTED)
        End If
    End Sub
End Class