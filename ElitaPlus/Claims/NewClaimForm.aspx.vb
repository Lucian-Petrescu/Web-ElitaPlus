'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (12/1/2004)  ********************
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports System.IO
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService
Imports Assurant.Elita.ClientIntegration

Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Imports System.Threading
Partial Class NewClaimForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents MessageController As MessageController
    Protected WithEvents moUserControlPoliceReport As UserControlPoliceReport_New
    '' REQ-784
    Protected WithEvents moUserControlAddress As UserControlAddress_New


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~\Claims\NewClaimForm.aspx"
    Public Const CONTINUE_DENY_CLAIM As String = "Next"
    Public Const COL_NAME_LOW_MONTH As String = "low_month"
    Public Const COL_NAME_HIGH_MONTH As String = "high_month"
    Public Const COL_NAME_PERCENT As String = "percent"
    Public Const COL_NAME_AMOUNT As String = "amount"

    Public isODeductibleByPercent As Boolean = False
    Public nDeductiblePercent As Decimal
    Public nDIscountPercent As LongType
    Public MethodOfRepairCode As String
    Public dvDeprSchedule As New DataView
    Public nLiabilityLimit As Decimal
    Public dProductSalesDate As String
    Public strLowMonth As String = ""
    Public strHighMonth As String = ""
    Public strPercent As String = ""
    Public strAmount As String = ""
    Public DeprSchCount As Integer = 0
    Public curContractId As String = ""
    Public curCertId As String = ""
    Public curCertItemCoverageId As String = ""
    Public curMethodOfRepairCode As String = ""
    Public Const PRICE_FIELD_CARRY_IN_PRICE As String = "CarryInPrice"
    Public Const PRICE_FIELD_HOME_PRICE As String = "HomePrice"
    Public Const PRICE_FIELD_CLEANING_PRICE As String = "CleaningPrice"
    Public Const PRICE_FIELD_ESTIMATE_PRICE As String = "EstimatePrice"
    Public Const PRICE_FIELD_OTHER_PRICE As String = "OtherPrice"


    ''REQ-784
    Public Const TABLE_ADDRESS As String = "ELP_ADDRESS"
    Public Const TABLE_CONTACT_INFO As String = "ELP_CONTACT_INTO"
    Protected Const NO_DATA As String = " - "

    Public Const PDF_URL As String = "DisplayPdf.aspx?ImageId="

    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 5
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const SELECT_ACTION_IMAGE As String = "SelectActionImage"
    Public Const ATTRIB_SRC As String = "src"

    Public Const EQUIPMENT_VERIFIED As String = "EQUIPMENT_VERIFIED"

    Public Const COL_PRICE_DV As String = "Price"
    Public Const ISSUE_CODE_CR_DEVICE_MIS As String = "CR_DEVICE_MIS"

#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Claim
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Claim)
            LastOperation = LastOp
            EditingBo = curEditingBo
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As Claim
        Public ServiceCenterId As Guid
        Public CertItemCoverageId As Guid
        Public ClaimMasterNumber As String
        Public DateOfLoss As DateType
        Public DateOfReport As DateType
        Public RecoveryButtonClick As Boolean
        Public ComingFromDenyClaim As Boolean
        Public CallerName As String
        Public ProblemDescription As String
        Public claimedEquipment As ClaimEquipment
        Public Sub New(claimBO As Claim, serviceCenterId As Guid, certItemCoverageId As Guid, Optional ByVal ClaimMasterNumber As String = Nothing, Optional ByVal DateOfLoss As DateType = Nothing, Optional ByVal DateOfReport As DateType = Nothing, Optional ByVal RecoveryButtonClick As Boolean = False, Optional ByVal ComingFromDenyClaim As Boolean = False, Optional ByVal ComingFromCert As Boolean = False, Optional ByVal CallerName As String = Nothing, Optional ByVal ProbDesc As String = Nothing)
            Me.ClaimBO = claimBO
            Me.ServiceCenterId = serviceCenterId
            Me.CertItemCoverageId = certItemCoverageId
            Me.ClaimMasterNumber = ClaimMasterNumber
            Me.DateOfLoss = DateOfLoss
            Me.DateOfReport = DateOfReport
            Me.RecoveryButtonClick = RecoveryButtonClick
            Me.ComingFromDenyClaim = ComingFromDenyClaim
            Me.CallerName = CallerName
            ProblemDescription = ProbDesc
        End Sub

        Public Sub New(_ClaimedEquipment As ClaimEquipment)
            claimedEquipment = _ClaimedEquipment
        End Sub
    End Class
#End Region

#Region "Enums"
    Public Enum InternalStates
        Regular
        ConfirmCreateWithAuthorizationRequired
        ConfirmAcknowledgementForClaimAdded
        ConfirmBackOnError
    End Enum
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As Claim
        Public PoliceReportBO As PoliceReport
        Public ScreenSnapShotBO As Claim
        Public InputParameters As Parameters
        Public ShippingInfoBO As ShippingInfo
        Public LastState As InternalStates = InternalStates.Regular
        Public LastErrMsg As String
        Public isSalutation As Boolean = False
        Public isDeductibleByPercent As Boolean = False
        Public oDeductiblePercent As DecimalType
        Public original_Deductible As String
        Public original_Discount As String
        Public original_AssurantPays As String
        Public original_ConsumerPays As String
        Public original_DueToSCFromAssurant As String
        Public original_OtherPrice As String
        Public original_LiabilityLimit As String
        Public original_AuthorizedAmount As String
        Public original_ReplacementCost As String
        Public isMethodPricePanel_Visible As Boolean
        Public SplSvc_value As Boolean
        Public prev_SplSvc_value As Boolean
        Public prev_MethodOfRepairCode As String
        Public prev_ClaimActivityCode As String
        Public yesId As Guid
        Public noId As Guid
        Public maxReplacementExceed_MessageDisplayed As Boolean = False
        Public claimNotReportedWithinPeriod_MessageDisplayed As Boolean = False
        Public mDealer As Dealer = Nothing
        '5623
        Public claimNotReportedWithinGracePeriod_MessageDisplayed As Boolean = False
        Public coverageTypeforclaimMissingfromCertificate_MessageDisplayed As Boolean = False
        'Public claimEquipmentBO As ClaimEquipment
        Public oGrossAmtReceived As Decimal
        Public oBillingTotalAmount As Decimal
        Public oOutstandingPremAmt As Decimal
        Public PayOutstandingPremium As Boolean

        '' REQ-784
        Public ContactInfoBO As ContactInfo
        Public AddressBO As Address

        'Public isAuthAmtSetBySKU As Boolean = False
        Public authAmtBySku As Decimal
        Public CommentsBO As Comment
        Public CertItemCoverageBO As CertItemCoverage
        Public CertItemBO As CertItem

        'REQ-1057
        Public IsGridVisible As Boolean = True
        Public SortExpression As String = Claim.ClaimIssuesView.COL_CREATED_DATE & " DESC"
        Public PageIndex As Integer = 0
        Public SelectedClaimIssueId As Guid
        Public PageSize As Integer = 5
        Public ClaimIssuesView As Claim.ClaimIssuesView
        Public ClaimImagesView As Claim.ClaimImagesView

        'REQ 1157
        Public DEDUCTIBLE_BASED_ON As String

        Public CaseQuestionAnswerListDV As CaseQuestionAnswer.CaseQuestionAnswerDV = Nothing
        Public ClaimActionListDV As CaseAction.CaseActionDV = Nothing

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()

        If NavController.CurrentNavState.Name = "NEW_CLAIM_DETAIL" Then
            State.InputParameters = CType(NavController.ParametersPassed, Parameters)
            Exit Sub
        End If
        Dim newClaim As Claim = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
        Dim servCenter As ServiceCenter = CType(NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)
        Dim coverage As CertItemCoverage = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE), CertItemCoverage)
        Dim strMasterClaimNumber As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_MSTR_CLAIM_NUMB), String)
        Dim objDateOfLoss As DateType = Nothing
        Dim objDateOfReport As DateType = Nothing
        Dim strDateOfLoss As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)
        Dim strDateOfReport As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
        Dim blnComingfromDenyClaim As Boolean = CType(NavController.FlowSession(FlowSessionKeys.SESSION_COMING_FROM_DENY_CLAIM), Boolean)
        Dim blnRecoveryButtonClick As Boolean = CType(NavController.FlowSession(FlowSessionKeys.SESSION_RECOVERY_BUTTON_CLICK), Boolean)
        Dim callerName As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME), String)
        Dim problemDescription As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION), String)

        If strDateOfLoss IsNot Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) Then
            objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))
        End If

        If strDateOfReport IsNot Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
            objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
        End If

        Try
            If CallingParameters IsNot Nothing Then
                State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)
            End If

            If State.InputParameters IsNot Nothing Then
                If State.InputParameters.ClaimBO IsNot Nothing Then
                    State.MyBO = State.InputParameters.ClaimBO
                Else
                    If (newClaim IsNot Nothing) Then
                        State.InputParameters = New Parameters(newClaim, Nothing, Nothing)
                    Else
                        State.InputParameters = New Parameters(newClaim, servCenter.Id, coverage.Id, strMasterClaimNumber, objDateOfLoss, objDateOfReport, , , blnComingfromDenyClaim, callerName, problemDescription)
                    End If
                    If State.InputParameters.ClaimBO IsNot Nothing Then
                        'Get the id from the parent
                        State.MyBO = State.InputParameters.ClaimBO
                    End If
                End If

            Else
                If (newClaim IsNot Nothing) Then
                    State.InputParameters = New Parameters(newClaim, Nothing, Nothing)
                Else
                    State.InputParameters = New Parameters(newClaim, servCenter.Id, coverage.Id, strMasterClaimNumber, objDateOfLoss, objDateOfReport, , , blnComingfromDenyClaim, callerName, problemDescription)
                End If
                If State.InputParameters.ClaimBO IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = State.InputParameters.ClaimBO
                End If
            End If

            '#REQ 1106 start
            If State.InputParameters IsNot Nothing Then
                Dim param As LocateServiceCenterDetailForm.Parameters = TryCast(NavController.ParametersPassed, LocateServiceCenterDetailForm.Parameters)
                If param IsNot Nothing Then
                    State.InputParameters.claimedEquipment = param.objClaimedEquipment
                End If
            End If
            '#Req 1106 end

            'shippingInfo
            State.ShippingInfoBO = CType(NavController.FlowSession(FlowSessionKeys.SESSION_SHIPPING_INFO), ShippingInfo)

            'Thunder User story 13 - Task - 199011
            If (State.MyBO.Certificate.Dealer.UseClaimAuthorizationId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)) Then
                If (Not String.IsNullOrWhiteSpace(NavController.PrevNavState.Name)) Then
                    'when the control comes back from Claim Issue Detail form
                    If (NavController.PrevNavState.Name = "CLAIM_ISSUE_DETAIL") Then
                        If (Not State.MyBO.Id.Equals(Guid.Empty)) Then
                            State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                            If (State.MyBO IsNot Nothing) Then
                                If (Me.State.MyBO.Status = BasicClaimStatus.Active OrElse Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
                                    If (NavController.CurrentFlow.Name = "AUTHORIZE_PENDING_CLAIM") Then
                                        NavController.Navigate(Me, "claim_issue_approved_from_claim", New ClaimForm.Parameters(State.MyBO.Id))
                                    ElseIf (NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE") Then
                                        NavController.Navigate(Me, "claim_issue_approved_from_Cert", New ClaimForm.Parameters(State.MyBO.Id))
                                    End If

                                End If
                            End If
                        End If
                        'when the control comes back from Claim Detail form after the above scenario
                        'ElseIf (Me.NavController.PrevNavState.Name = "CLAIM_ISSUE_APPROVED") Then
                        '    If (Not Me.State.MyBO.Id.Equals(Guid.Empty)) Then
                        '        Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                        '    End If
                    End If

                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            State.InputParameters = CType(CallingPar, Parameters)
            If State.InputParameters.ClaimBO IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = State.InputParameters.ClaimBO
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum(state As String)

        Select Case state
            Case "CREATE_CLAIM_FROM_CERTIFICATE"
                If (Me.State IsNot Nothing) Then
                    If (Me.State.MyBO IsNot Nothing) Then
                        Dim pageName As String = "File New Claim"
                        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & "Certificate " & Me.State.MyBO.CertificateNumber & ElitaBase.Sperator & pageName
                    End If
                End If

            Case "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM DETAILS") & ElitaBase.Sperator & MasterPage.PageTab
            Case "AUTHORIZE_PENDING_CLAIM", "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH", "AUTHORIZE_AGENT_PENDING_CLAIM"
                MasterPage.BreadCrum = MasterPage.PageTab
        End Select

    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        moModalCollectDivMsgController.Clear_Hide()

        ''REQ-784
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cboUseShipAddress.SelectedValue = YesId.ToString Then
            SetContactInfoLabelColor()
            SetAddressLabelColor()
        End If

        lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUES")
        lblFileNewIssue.Text = TranslationBase.TranslateLabelOrMessage("FILE_NEW_CLAIM_ISSUE")

        Try

            If NavController.CurrentNavState.Name <> "CREATE_NEW_CLAIM" Then
                If NavController.CurrentNavState.Name <> "NEW_CLAIM_DETAIL" Then
                    Return
                End If
            End If

            SetDecimalSeparatorSymbol()

            If Not IsPostBack Then
                'Date Calendars

                AddCalendar_New(ImageButtonLossDate, TextboxLossDate)
                AddCalendar_New(ImageButtonReportDate, TextboxReportDate)
                AddCalendar_New(ImageButtonRepairDate, TextboxRepairdate)
                AddCalendar_New(ImageButtonPickUpDate, TextboxPickUpDate)
                '   Me.MenuEnabled = False

                CheckIfComingFromRecoveryButtonClick()

                If State.MyBO Is Nothing Then

                    State.MyBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
                    With State.InputParameters
                        If .ComingFromDenyClaim = True Then
                            State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss, , , .ComingFromDenyClaim)
                        Else
                            If NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE" Then
                                State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss, , , , True, .CallerName, .ProblemDescription, State.InputParameters.claimedEquipment)

                            Else
                                State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss)
                            End If

                        End If

                    End With
                End If

                Trace(Me, "Claim Id=" & GuidControl.GuidToHexString(State.MyBO.Id))

                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")

                State.yesId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
                State.noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

                'REQ-918  Populate the Make,Model,Serial Number, SKU and List Price from the current item 
                'of the Certificate to the Claim Equipment
                If (State.mDealer Is Nothing) Then
                    State.mDealer = New Dealer(State.MyBO.CompanyId, State.MyBO.DealerCode)
                End If

                State.ClaimIssuesView = State.MyBO.GetClaimIssuesView()
                PopulateDropdowns()
                PopulateServiceCenterSelected()

                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                TranslateGridHeader(Grid)


                'Populate Extended Status Aging Grid
                PopulateExtendedStatusAging()

                'Work Queue
                TranslateGridHeader(GridClaimImages)
                State.ClaimImagesView = State.MyBO.GetClaimImagesView()
                PopulateClaimImagesGrid()
                ' Me.BindListControlToDataView(Me.DocumentTypeDropDown, LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                Dim documentType As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                DocumentTypeDropDown.Populate(documentType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False
                                               })
                ClearForm()

                PopulateFormFromBOs()

                MasterPage.MessageController.Clear()

                CheckBoxCarryInPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxCarryInPrice.ClientID))
                CheckBoxHomePrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxHomePrice.ClientID))
                CheckBoxCleaningPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxCleaningPrice.ClientID))
                CheckBoxEstimatePrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxEstimatePrice.ClientID))
                CheckBoxOtherPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxOtherPrice.ClientID))
                btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", ButtonCancel_Write.UniqueID))

                nDIscountPercent = State.MyBO.DiscountPercent
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                    State.isDeductibleByPercent = True
                    isODeductibleByPercent = True
                    State.oDeductiblePercent = State.MyBO.DeductiblePercent
                    nDeductiblePercent = State.MyBO.DeductiblePercent.Value
                End If
                MethodOfRepairCode = State.MyBO.MethodOfRepairCode

                ''check set auth amount by sku
                'State.isAuthAmtSetBySKU = Me.setAuthAmtBySKU(State.authAmtBySku)
                'If State.isAuthAmtSetBySKU Then
                '    Me.TextboxAuthorizedAmount.Text = State.authAmtBySku.ToString
                'End If

                InitialEnableDisableFields()
                DisableButtonsForClaimSystem()
                If TextboxCallerName.Text = "" Then SetFocus(TextboxCallerName)

                ClaimLogisticalInfo.claimId = State.MyBO.Id
                ClaimLogisticalInfo.PopulateGrid()


                TranslateGridHeader(CaseQuestionAnswerGrid)
                TranslateGridHeader(ClaimActionGrid)
                PopulateQuestionAnswerGrid()
                PopulateClaimActionGrid()


            Else
                If Not HiddenCallerTaxNumber.Value = "" Then TextboxCALLER_TAX_NUMBER.Enabled = CType(HiddenCallerTaxNumber.Value, Boolean)

                isODeductibleByPercent = State.isDeductibleByPercent

                If (State.oDeductiblePercent Is Nothing) Then
                    nDeductiblePercent = 0D
                Else
                    nDeductiblePercent = State.oDeductiblePercent.Value
                End If
            End If

            PopulateClaimedEnrolledDetails()

            BindBoPropertiesToLabels()
            If (CheckIfComingFromCreateClaimConfirm()) Then
                Exit Sub
            End If
            GetDepreciationSchedule()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
                If State.MyBO.EnrolledEquipment IsNot Nothing Then
                    MyBase.AddLabelDecorations(State.MyBO.EnrolledEquipment)
                End If
                If State.MyBO.ClaimedEquipment IsNot Nothing Then
                    MyBase.AddLabelDecorations(State.MyBO.ClaimedEquipment)
                End If
                If Not LabelReplacementCost.Text.EndsWith(":") Then
                    LabelReplacementCost.Text = LabelReplacementCost.Text & ":"
                End If
                ''REQ-784
                If Not LabelUseShipAddress.Text.EndsWith(":") Then
                    LabelUseShipAddress.Text = LabelUseShipAddress.Text & ":"
                End If

                If Not LabelOutstandingPremAmt.Text.EndsWith(":") Then
                    LabelOutstandingPremAmt.Text = LabelOutstandingPremAmt.Text & ":"
                End If

                'REQ-5467
                If (IsNewUI) Then
                    If State.MyBO.Dealer.IsLawsuitMandatoryId = State.yesId Then
                        LabelIsLawsuitId.Text = "<span class=""mandatory"">*</span> " & LabelIsLawsuitId.Text
                    End If
                End If

            End If


            'Set dummy fields to value of real field
            TextboxAuthorizedAmountShadow.Text = TextboxAuthorizedAmount.Text
            If IsPostBack Then
                TextboxDeductible_WRITE.Text = TextboxDeductibleShadow.Text
                TextBoxDiscount.Text = TextBoxDiscountShadow.Text
                TextboxAssurantPays.Text = TextboxAssurantPaysShadow.Text
                TextboxConsumerPays.Text = TextboxConsumerPaysShadow.Text
                TextboxDueToSCFromAssurant.Text = TextboxDueToSCFromAssurantShadow.Text
                TextBoxOtherPrice.Text = TextBoxOtherPriceShadow.Text
                TextboxLiabilityLimit.Text = TextboxLiabilityLimitShadow.Text

                Dim myContractId As Guid = Contract.GetContractID(State.MyBO.CertificateId)
                curContractId = GuidControl.GuidToHexString(myContractId)
                curCertId = GuidControl.GuidToHexString(State.MyBO.CertificateId)
                curCertItemCoverageId = GuidControl.GuidToHexString(State.MyBO.CertItemCoverageId)
                curMethodOfRepairCode = State.MyBO.MethodOfRepairCode

            Else
                TextboxDeductibleShadow.Text = TextboxDeductible_WRITE.Text
                TextBoxDiscountShadow.Text = TextBoxDiscount.Text
                TextboxAssurantPaysShadow.Text = TextboxAssurantPays.Text
                TextboxConsumerPaysShadow.Text = TextboxConsumerPays.Text
                TextboxDueToSCFromAssurantShadow.Text = TextboxDueToSCFromAssurant.Text
                TextBoxOtherPriceShadow.Text = TextBoxOtherPrice.Text
                TextboxLiabilityLimitShadow.Text = TextboxLiabilityLimit.Text
                TextboxDeductibleShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextBoxDiscountShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextBoxOtherPriceShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextboxAssurantPaysShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextboxConsumerPaysShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextboxDueToSCFromAssurantShadow.Style.Add(HtmlTextWriterStyle.Display, "none")
                TextboxLiabilityLimitShadow.Style.Add(HtmlTextWriterStyle.Display, "none")

                TextBoxOtherPriceShadow.ReadOnly = False

                State.original_Deductible = TextboxDeductible_WRITE.Text
                State.original_Discount = TextBoxDiscount.Text
                State.original_AssurantPays = TextboxAssurantPays.Text
                State.original_ConsumerPays = TextboxConsumerPays.Text
                State.original_DueToSCFromAssurant = TextboxDueToSCFromAssurant.Text
                State.original_OtherPrice = TextBoxOtherPrice.Text
                State.original_LiabilityLimit = TextboxLiabilityLimit.Text

                If TextBoxReplacementCost.Visible = True Then
                    State.original_AuthorizedAmount = TextBoxReplacementCost.Text
                    State.isMethodPricePanel_Visible = False
                Else
                    State.original_AuthorizedAmount = TextboxAuthorizedAmount.Text
                    State.isMethodPricePanel_Visible = True
                End If
                State.prev_MethodOfRepairCode = State.MyBO.MethodOfRepairCode
                State.prev_ClaimActivityCode = State.MyBO.ClaimActivityCode
            End If

            'Def 1859 start
            If Not IsPostBack Then
                If State.MyBO.GetSpecialServiceValue = State.yesId Then
                    EnableDisablePriceFieldsforSplSvc()
                End If

                ' Commenting code to resolve DEF 3187
                ' ''Lock the claim  - work queue
                'If Me.State.MyBO.IsLocked = Codes.YESNO_Y Then    'if the claim is  locked
                '    If Me.State.MyBO.LockedBy <> ElitaPlusIdentity.Current.ActiveUser.Id Then
                '        'if not locked by the same user then editing not permitted
                '        Me.DisableButtonsForClaimSystem()
                '        Dim strLockedbyuser As String = (New User(Me.State.MyBO.LockedBy)).UserName
                '        Me.moMessageController.Clear()
                '        Me.moMessageController.AddWarning(String.Format("{0}: {1}", _
                '                                      TranslationBase.TranslateLabelOrMessage("CLAIM_LOCK"), _
                '                                      strLockedbyuser, False))
                '    End If
                'Else
                '    Me.State.MyBO.Lock()
                'End If

                If State.MyBO.LockedBy = ElitaPlusIdentity.Current.ActiveUser.Id Then
                    btnUnlock.Visible = True
                    'ElseIf ElitaPlusIdentity.Current.ActiveUser Then
                End If
            End If
            'def 1859 end

            If Not IsPostBack Then

                Dim objDateOfLoss As DateType = Nothing
                Dim strDateOfLoss As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)
                Dim objDateOfReport As DateType = Nothing
                Dim strDateOfReport As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)

                If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified AndAlso State.MyBO.IsNew Then
                    'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                    If strDateOfReport IsNot Nothing AndAlso Not strDateOfReport.Equals(String.Empty) AndAlso strDateOfLoss IsNot Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) AndAlso
                         Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then

                        objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                        objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))


                        If Not State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, False)
                            MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.coverageTypeforclaimMissingfromCertificate_MessageDisplayed = True
                            'Set claim status to Denied
                            State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        If Not State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, False)
                            MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.claimNotReportedWithinGracePeriod_MessageDisplayed = True
                            'Set claim status to Denied
                            State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        If (State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value)) And
                         (State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value)) Then
                            'Display warning Message if the Maximum number of Replacements have been exceeded
                            If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                                Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                                MasterPage.MessageController.AddWarning(denialMessage)
                                'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                                State.maxReplacementExceed_MessageDisplayed = True
                                'Set claim status to Denied
                                State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                            End If
                        End If

                    End If
                Else
                    If State.MyBO.LossDate IsNot Nothing AndAlso State.MyBO.ReportedDate IsNot Nothing AndAlso
                         Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                        'Display warning Message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days 
                        If Not State.MyBO.IsClaimReportedWithinPeriod(State.MyBO.CertificateId, State.MyBO.LossDate.Value, State.MyBO.ReportedDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, False)
                            MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.claimNotReportedWithinPeriod_MessageDisplayed = True
                            'Set claim status to Denied
                            'Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        'Display warning Message if the Maximum number of Replacements have been exceeded
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                            MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.maxReplacementExceed_MessageDisplayed = True
                            'Set claim status to Denied
                            State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If
                    End If
                End If

            End If

            PopulateGrid()

            'REQ-860
            Select Case NavController.CurrentFlow.Name
                Case "CREATE_CLAIM_FROM_CERTIFICATE"
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificate")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    FillWizard(NavController.CurrentFlow.Name)
                    PopulateProtectionAndEventDetail()
                Case "AUTHORIZE_PENDING_CLAIM", "WORK_ON_QUEUE", "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH", "AUTHORIZE_AGENT_PENDING_CLAIM"
                    MasterPage.PageTab = String.Format("{0} {1}", TranslationBase.TranslateLabelOrMessage("PENDING"), TranslationBase.TranslateLabelOrMessage("CLAIM"))
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    PopulateProtectionAndEventDetail()
                Case "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    PopulateProtectionAndEventDetail()

            End Select
            'REQ 860
            If Not IsPostBack Then
                IsMaxSvcWrtyClaimsReached()  'developed for Req-5921 - Google OOW 
            End If

            MasterPage.UsePageTabTitleInBreadCrum = False
            UpdateBreadCrum(NavController.CurrentFlow.Name)


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ' Clean Popup Input
            CleanPopupInput()
            'Me.State.LastState = InternalStates.Regular
            'Me.HiddenSaveChangesPromptResponse.Value = ""
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property UserCtrPoliceReport() As UserControlPoliceReport_New
        Get
            If moUserControlPoliceReport Is Nothing Then
                moUserControlPoliceReport = CType(Master.FindControl("BodyPlaceHolder").FindControl("mcUserControlPoliceReport"), UserControlPoliceReport_New)
            End If
            Return moUserControlPoliceReport
        End Get
    End Property

    '' REQ-784
    Public ReadOnly Property UserControlAddress() As UserControlAddress_New
        Get
            If moUserControlAddress Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    '' REQ-784
    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
            End If
            Return moUserControlContactInfo
        End Get
    End Property

    Public ReadOnly Property UserControlMessageController() As MessageController
        Get
            If MessageController Is Nothing Then
                MessageController = DirectCast(MasterPage.MessageController, MessageController)
            End If
            Return MessageController
        End Get
    End Property


#End Region

#Region "Controlling Logic"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function CalculateLiability(lossDate As String, myCertId As String,
        myContractId As String, myCertItemCoverageId As String, myMethodOfRepairCode As String) As String

        Dim liabilityStr As String = ""
        Dim myClaimBo As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()

        If myMethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
            Dim al As ArrayList = myClaimBo.CalculateLiabilityLimit(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(myCertId)),
                GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(myContractId)),
                GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(myCertItemCoverageId)),
                DateHelper.GetDateValue(lossDate)) 'CType(lossDate, DateType)

            If CType(al(1), Integer) = 0 Then
                liabilityStr = CType(CType(al(0), Decimal), String)
            End If
        End If

        Return liabilityStr
    End Function
    Protected Sub DisableButtonsForClaimSystem()
        If Not State.MyBO.CertificateId.Equals(Guid.Empty) Then
            Dim oCert As New Certificate(State.MyBO.CertificateId)
            Dim oDealer As New Dealer(oCert.DealerId)
            Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
            Dim bAllowSvcWarrantyClaims As Boolean = False
            ' Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)

            If oClmSystem.Code = "PORTAL" Then
                bAllowSvcWarrantyClaims = True
            End If

            If (oClmSystem.NewClaimId.Equals(State.noId) AndAlso bAllowSvcWarrantyClaims = False) Or State.MyBO.IsClaimChild = Codes.YESNO_Y Then

                MyBase.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_UPDATE_NOT_ALLOWED, True)

                If btnCreateClaim_WRITE.Visible And btnCreateClaim_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnCreateClaim_WRITE, False)
                End If
                If ButtonOverride_Write.Visible And ButtonOverride_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, ButtonOverride_Write, False)
                End If
                If ButtonUpdateClaim_Write.Visible And ButtonUpdateClaim_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, ButtonUpdateClaim_Write, False)
                End If
                If ButtonCancel_Write.Visible And ButtonCancel_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, ButtonCancel_Write, False)
                End If
                If btnCancelClaim.Visible And btnCancelClaim.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnCancelClaim, False)
                End If
                If btnComment.Visible And btnComment.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnComment, False)
                End If
                If btnDenyClaim_Write.Visible And btnDenyClaim_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnDenyClaim_Write, False)
                End If
            End If
        End If
    End Sub
    Protected Sub InitialEnableDisableFields()
        'read only fields
        'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        ChangeEnabledProperty(TextboxCertificateNumber, False)
        ChangeEnabledProperty(TextboxClaimNumber, False)
        ChangeEnabledProperty(TextboxServiceCenter, False)
        ChangeEnabledProperty(TextboxLiabilityLimit, False)
        ChangeEnabledProperty(TextboxDeductible_WRITE, True)
        ChangeEnabledProperty(TextBoxDiscount, False)
        ChangeEnabledProperty(TextboxAssurantPays, False)
        ChangeEnabledProperty(TextboxConsumerPays, False)
        ChangeEnabledProperty(TextboxDueToSCFromAssurant, False)
        ChangeEnabledProperty(CheckBoxLoanerTaken, False)
        ChangeEnabledProperty(TextBoxCarryInPrice, False)
        ChangeEnabledProperty(TextBoxHomePrice, False)
        ' Authorized needs to be disabled. In .NET 2.0 a readOnly field can not be modified on the client
        ' A dummy field was created to show the user the value, while hiding the true field which
        ' remains enabled but invisible
        'JLR==> Me.ChangeEnabledProperty(Me.TextboxAuthorizedAmount, False)
        ' Comment the next line for troubleshooting (to see the field on the screen)
        If Not (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AUTOMOTIVE _
        Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__GENERAL Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__LEGAL Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY) Then
            TextboxAuthorizedAmount.Style.Add(HtmlTextWriterStyle.Display, "none") ' Set true field to invisible
        Else
            TextboxAuthorizedAmountShadow.Style.Add(HtmlTextWriterStyle.Display, "none") ' Set Shadow field to invisible
        End If


        'invisible by default
        ControlMgr.SetVisibleControl(Me, ButtonCancel_Write, False)
        ControlMgr.SetVisibleControl(Me, btnCancelClaim, False)
        ControlMgr.SetVisibleControl(Me, ButtonOverride_Write, False)
        ControlMgr.SetVisibleControl(Me, ButtonUpdateClaim_Write, False)

        '  Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, False)
        SetEnabledForControlFamily(PanelMethodPrice, False)

        If NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE" Then
            ControlMgr.SetVisibleControl(Me, ButtonCancel_Write, True)
        End If

        If NavController.CurrentFlow.Name = "WORK_ON_QUEUE" Then
            ControlMgr.SetVisibleControl(Me, btnBack, False)
        End If

        If Not State.MyBO.IsNew Then
            ' Pending Claim
            ControlMgr.SetVisibleControl(Me, btnCancelClaim, True)
            ControlMgr.SetVisibleControl(Me, ButtonCancel_Write, False)
            ControlMgr.SetVisibleControl(Me, ButtonUpdateClaim_Write, True)
            ControlMgr.SetVisibleControl(Me, btnCreateClaim_WRITE, False)
        End If

        If Not State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING Then
            ControlMgr.SetVisibleControl(Me, btnComment, False)
        End If

        'Make Invisible for Service Warranty
        If State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            'Service Warranty Claims
            ControlMgr.SetVisibleControl(Me, TextboxLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, TextboxDeductible_WRITE, False)
            ControlMgr.SetVisibleControl(Me, TextBoxDiscount, False)
            ControlMgr.SetVisibleControl(Me, TextboxAssurantPays, False)
            ControlMgr.SetVisibleControl(Me, TextboxConsumerPays, False)
            ControlMgr.SetVisibleControl(Me, TextboxDueToSCFromAssurant, False)

            ControlMgr.SetVisibleControl(Me, LabelLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, LabelDeductible, False)
            ControlMgr.SetVisibleControl(Me, LabelDiscount, False)
            ControlMgr.SetVisibleControl(Me, LabelAssurantPays, False)
            ControlMgr.SetVisibleControl(Me, LabelConsumerPays, False)
            ControlMgr.SetVisibleControl(Me, LabelDueToSCFromAssurant, False)
            If Not State.MyBO.IsNew Then
                ' Pending Claim
                If State.MyBO.IsDaysLimitExceeded Then
                    If Not (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                        ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                        ' No (Claim Manager or IHQ Support), It is not authorized to update
                        ControlMgr.SetEnableControl(Me, ButtonUpdateClaim_Write, False)
                    End If
                End If
            End If

        End If

        'Loaner
        If (State.MyBO.ClaimActivityCode Is Nothing OrElse State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT) _
            AndAlso Not State.MyBO.ServiceCenterObject.LoanerCenterId.Equals(Guid.Empty) Then
            ChangeEnabledProperty(CheckBoxLoanerTaken, True)
        End If

        If State.MyBO.LossDate IsNot Nothing Then  'Me.State.MyBO.IsFirstClaimRecordFortheIncident Then
            'When editing a claim record created from an existing one
            ChangeEnabledProperty(TextboxLossDate, False)
            ControlMgr.SetVisibleControl(Me, ImageButtonLossDate, False)
            'Me.ChangeEnabledProperty(Me.cboCauseOfLossId, False)
        End If

        If State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            ChangeEnabledProperty(TextboxAuthorizedAmount, True)
            ControlMgr.SetVisibleControl(Me, PanelRepair, False)
            'ElseIf State.isAuthAmtSetBySKU Then
            '    ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)
        Else
            Select Case State.MyBO.MethodOfRepairCode
                Case Codes.METHOD_OF_REPAIR__AUTOMOTIVE, Codes.METHOD_OF_REPAIR__GENERAL, Codes.METHOD_OF_REPAIR__LEGAL, Codes.METHOD_OF_REPAIR__RECOVERY

                    ChangeEnabledProperty(TextboxAuthorizedAmount, True)
                    ControlMgr.SetVisibleControl(Me, PanelRepair, False)


                Case Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__PICK_UP, Codes.METHOD_OF_REPAIR__SEND_IN
                    'This a repair Claim 
                    'Me.SetEnabledForControlFamily(Me.PanelMethodOfRepair, True)
                    ControlMgr.SetVisibleControl(Me, PanelRepair, True)
                    ChangeEnabledProperty(CheckBoxEstimatePrice, True)
                    ChangeEnabledProperty(CheckBoxOtherPrice, True)
                    ChangeEnabledProperty(CheckBoxCleaningPrice, True)
                    ChangeEnabledProperty(CheckBoxCarryInPrice(), True)
                    ChangeEnabledProperty(CheckBoxHomePrice, True)
                    If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
                        ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, False)
                        ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, False)
                        ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, False)

                        ControlMgr.SetVisibleControl(Me, LabelHomePrice, True)
                        ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, True)
                        ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, LabelHomePrice, False)
                        ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, False)
                        ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, False)

                        ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, True)
                        ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, True)
                        ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, True)

                    End If

                    If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then
                        LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("SEND_IN_PRICE")
                    ElseIf State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then
                        LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("PICK_UP_PRICE")
                    End If
            End Select
        End If


        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, LabelPolicyNumber, False)
        End If

        InitialEnableDisableReplacement()

        Dim objCompany As New Company(State.MyBO.CompanyId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_SERVICES Then
            ControlMgr.SetVisibleControl(Me, TextboxCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, LabelCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, LabelConditionalReqquired, False)
        Else
            ControlMgr.SetVisibleControl(Me, TextboxCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, LabelCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, LabelConditionalReqquired, True)
            TextboxCallerName.Attributes.Add("onchange", "ClearCallerTaxNumber();")
            If State.MyBO.CallerTaxNumber Is Nothing Or (State.MyBO.CallerTaxNumber IsNot Nothing AndAlso State.MyBO.CallerTaxNumber.Equals(String.Empty)) Then
                ControlMgr.SetEnableControl(Me, TextboxCALLER_TAX_NUMBER, True)
            Else
                ControlMgr.SetEnableControl(Me, TextboxCALLER_TAX_NUMBER, False)
            End If
            HiddenCallerTaxNumber.Value = TextboxCALLER_TAX_NUMBER.Enabled.ToString
        End If

        ' initially disable them which are needed only for backend claim or deny claim
        ControlMgr.SetVisibleControl(Me, LabelRepairDate, False)
        ControlMgr.SetVisibleControl(Me, TextboxRepairdate, False)
        ControlMgr.SetVisibleControl(Me, ImageButtonRepairDate, False)
        ControlMgr.SetVisibleControl(Me, LabelPickUpDate, False)
        ControlMgr.SetVisibleControl(Me, TextboxPickUpDate, False)
        ControlMgr.SetVisibleControl(Me, ImageButtonPickUpDate, False)

        ControlMgr.SetVisibleControl(Me, TextboxInvoiceNumber, True)
        ControlMgr.SetVisibleControl(Me, LabelInvoiceNumber, True)

        ControlMgr.SetVisibleControl(Me, btnDenyClaim_Write, False)

        Dim certificateBO As New Certificate(State.MyBO.CertificateId)

        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, State.MyBO.CoverageTypeId, certificateBO.DealerId, Authentication.LangId, certificateBO.ProductCode, False) Then
            ChangeEnabledProperty(cboCauseOfLossId, True)
        End If
        If State.MyBO.ClaimSpecialServiceId = State.yesId Then
            EnableDisablePriceFieldsforSplSvc()
        End If
        EnableDisableFields()

    End Sub

    Private Sub InitialEnableDisableReplacement()
        ControlMgr.SetVisibleControl(Me, lblNewDeviceSKU, False)
        ControlMgr.SetVisibleControl(Me, txtNewDeviceSKU, False)
        If State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
            ' It is a Replacement
            '    Me.TextboxAuthorizedAmount.
            '  ControlMgr.SetVisibleControl(Me, Me.TextboxAuthorizedAmount, False)
            TextboxAuthorizedAmountShadow.Style.Add(HtmlTextWriterStyle.Display, "none") ' Visible = False
            TextboxAuthorizedAmount.ReadOnly = False
            ControlMgr.SetVisibleControl(Me, LabelAuthorizedAmount, False)
            ' Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, True)
            ' Me.ChangeEnabledProperty(Me.CheckBoxReplacement, True)
            ChangeEnabledProperty(TextBoxReplacementCost, True)
            ControlMgr.SetVisibleControl(Me, PanelRepair, False)

            ' Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(State.MyBO.CertItemCoverageId, State.MyBO.MethodOfRepairId)
            State.DEDUCTIBLE_BASED_ON = oDeductible.DeductibleBasedOn
            'req 1157 added additional condition
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.mDealer.NewDeviceSkuRequiredId) = Codes.YESNO_Y Or (State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                ControlMgr.SetVisibleControl(Me, lblNewDeviceSKU, True)
                ControlMgr.SetVisibleControl(Me, txtNewDeviceSKU, True)
            End If
        Else
            'Make Replacement Price Invisible
            '  Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, False)
            ControlMgr.SetVisibleControl(Me, LabelReplacementCost, False)
            ControlMgr.SetVisibleControl(Me, TextBoxReplacementCost, False)
            StartNoReplacementClient()
        End If

    End Sub


    Private Sub IsMaxSvcWrtyClaimsReached()
        '''''check if max service warranty count reached (developed for Google OOW project Req-5921)
        If State.MyBO.IsMaxSvcWrtyClaimsReached Then
            MasterPage.MessageController.Clear()
            MasterPage.MessageController.AddErrorAndShow("MAX_NUM_SVC_WRTY_CLAIMS_REACHED", True)
            ControlMgr.SetEnableControl(Me, btnCreateClaim_WRITE, False)
        End If
    End Sub

    Protected Sub EnableDisableFields()
        Dim oContractId As Guid
        oContractId = Contract.GetContractID(State.MyBO.CertificateId)
        Dim ContractBO As New Contract(oContractId)

        If State.isSalutation Then
            ControlMgr.SetVisibleControl(Me, cboContactSalutationId, True)
            ControlMgr.SetVisibleControl(Me, cboCallerSalutationId, True)
        Else
            ControlMgr.SetVisibleControl(Me, cboContactSalutationId, False)
            ControlMgr.SetVisibleControl(Me, cboCallerSalutationId, False)
        End If

        If State.MyBO.ClaimNumber Is Nothing Then
            ControlMgr.SetVisibleControl(Me, LabelClaimNumber, False)
            ControlMgr.SetVisibleControl(Me, TextboxClaimNumber, False)
        End If

        If State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ' show the dates when its NOT service warranty
            ControlMgr.SetVisibleControl(Me, btnDenyClaim_Write, True)
            If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME _
                Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN _
                Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                ' show the controls only when its method of repair is at home or carry in or replacement


                ' when backend claim, display repair date and pick up date else hide them

                If ContractBO.BackEndClaimsAllowedId.Equals(State.yesId) Then
                    ControlMgr.SetVisibleControl(Me, LabelRepairDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxRepairdate, True)
                    ControlMgr.SetVisibleControl(Me, ImageButtonRepairDate, True)
                    ControlMgr.SetVisibleControl(Me, LabelPickUpDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxPickUpDate, True)
                    ControlMgr.SetVisibleControl(Me, ImageButtonPickUpDate, True)
                Else
                    ControlMgr.SetVisibleControl(Me, LabelRepairDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxRepairdate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonRepairDate, False)
                    ControlMgr.SetVisibleControl(Me, LabelPickUpDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxPickUpDate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonPickUpDate, False)
                End If
            End If
        End If

        If State.MyBO.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then
            LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
        Else
            LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
        End If

        If State.MyBO.IsDaysLimitExceeded Then
            MasterPage.MessageController.Clear()
            MasterPage.MessageController.AddWarning("DAYS_LIMIT_EXCEEDED")
        End If

        If State.MyBO.IsAuthorizationLimitExceeded Then
            moMessageController.Clear()
            moMessageController.AddWarning(String.Format("{0}: {1}",
                                          TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                          TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If
        HiddenUserAuthorization.Value = State.MyBO.AuthorizationLimit.ToString

        If State.InputParameters.ComingFromDenyClaim = True Then
            btnDenyClaim_Write.Text = CONTINUE_DENY_CLAIM
            ControlMgr.SetEnableControl(Me, btnCreateClaim_WRITE, False)
        Else
            ControlMgr.SetEnableControl(Me, btnCreateClaim_WRITE, True)
        End If

        ''req-784 (DEF-1607)
        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, LabelUseShipAddress, True)
            ControlMgr.SetVisibleControl(Me, cboUseShipAddress, True)
        End If

        If ContractBO.PayOutstandingPremiumId.Equals(State.yesId) Then
            State.PayOutstandingPremium = True
            ControlMgr.SetVisibleControl(Me, LabelOutstandingPremAmt, True)
            ControlMgr.SetVisibleControl(Me, TextboxOutstandingPremAmt, True)
        Else
            State.PayOutstandingPremium = False
            ControlMgr.SetVisibleControl(Me, LabelOutstandingPremAmt, False)
            ControlMgr.SetVisibleControl(Me, TextboxOutstandingPremAmt, False)
        End If

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
    Public Sub EnableDisableMethodOfPriceFields(priceType As String, pricegrpCode As String)

        Dim ctlCheckBox As CheckBox
        Dim ctlTextBox As TextBox
        Dim nOtherPrice As New DecimalType(0)
        Dim ctlTextBoxChanged As TextBox
        Dim nZeroValue As New DecimalType(0)

        Dim ctlList As New ArrayList()
        ctlList.Add(PRICE_FIELD_CARRY_IN_PRICE)
        ctlList.Add(PRICE_FIELD_CLEANING_PRICE)
        ctlList.Add(PRICE_FIELD_ESTIMATE_PRICE)
        ctlList.Add(PRICE_FIELD_HOME_PRICE)
        ctlList.Add(PRICE_FIELD_OTHER_PRICE)
        Dim i As Integer
        Dim MasterPagectl As Control = MasterPage.FindControl("BodyPlaceHolder")
        ControlMgr.SetVisibleControl(Me, PanelRepair, True)
        PopulatePrices()
        UncheckAllMethodPriceCheckBoxes()

        For i = 0 To ctlList.Count - 1
            If priceType = ctlList.Item(i).ToString Then
                ctlCheckBox = CType(MasterPagectl.FindControl("CheckBox" + ctlList.Item(i).ToString), CheckBox)
                ctlTextBox = CType(MasterPagectl.FindControl("TextBox" + ctlList.Item(i).ToString), TextBox)
                ctlTextBoxChanged = ctlTextBox
                ctlCheckBox.Checked = True
                TextboxAuthorizedAmount.Text = ctlTextBox.Text
                ControlMgr.SetVisibleControl(Me, TextboxAuthorizedAmount, True)
                ControlMgr.SetVisibleControl(Me, ctlCheckBox, True)
                ControlMgr.SetVisibleControl(Me, ctlTextBox, True)
                ChangeEnabledProperty(ctlTextBox, False)
                ChangeEnabledProperty(ctlCheckBox, False)
                If priceType = PRICE_FIELD_OTHER_PRICE Then
                    ChangeEnabledProperty(ctlCheckBox, True)
                    ChangeEnabledProperty(ctlTextBox, True)
                    ctlTextBox.ReadOnly = False
                End If
            Else
                ctlCheckBox = CType(MasterPagectl.FindControl("CheckBox" + ctlList.Item(i).ToString), CheckBox)
                ctlTextBox = CType(MasterPagectl.FindControl("TextBox" + ctlList.Item(i).ToString), TextBox)
                ControlMgr.SetVisibleControl(Me, ctlCheckBox, True)
                ControlMgr.SetVisibleControl(Me, ctlTextBox, True)
                ChangeEnabledProperty(ctlTextBox, False)
                ChangeEnabledProperty(ctlCheckBox, False)
                ctlCheckBox.Checked = False
            End If
        Next

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            TextBoxReplacementCost.Text = TextboxAuthorizedAmount.Text
            TextboxAuthorizedAmountShadow.Text = TextboxAuthorizedAmount.Text
        Else
            TextboxAuthorizedAmountShadow.Text = TextboxAuthorizedAmount.Text
        End If

        If priceType = PRICE_FIELD_HOME_PRICE Then
            ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, False)

            ControlMgr.SetVisibleControl(Me, LabelHomePrice, True)
            ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, True)
            ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
        Else
            ControlMgr.SetVisibleControl(Me, LabelHomePrice, False)
            ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, False)
            ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, False)

            ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
        End If

        Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + ctlTextBoxChanged.ID + "') </script>"
        RegisterStartupScript("Startup", x)
        'Pratap Def 2061
        'Dim x As String = "<script language='JavaScript'> UpdateDetailCheck('" + ctlTextBoxChanged.ID + "') </script>"
        'Me.RegisterStartupScript("Startup", x)

    End Sub

    Protected Sub EnableDisablePriceFieldsforSplSvc()
        Dim splSvcPriceGrp As String
        Dim nOtherPrice As New DecimalType(0)
        With State.MyBO
            If .ClaimSpecialServiceId = State.yesId Then
                State.SplSvc_value = True
                splSvcPriceGrp = .SpecialServiceServiceType
                If splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_CARRY_IN Then 'carry in price
                    LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
                    EnableDisableMethodOfPriceFields(PRICE_FIELD_CARRY_IN_PRICE, Codes.PRICEGROUP_SPL_SVC_CARRY_IN)
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_CLEANING Then 'cleaning price
                    EnableDisableMethodOfPriceFields(PRICE_FIELD_CLEANING_PRICE, Codes.PRICEGROUP_SPL_SVC_CLEANING)
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_ESTIMATE Then 'estimate price
                    EnableDisableMethodOfPriceFields(PRICE_FIELD_ESTIMATE_PRICE, Codes.PRICEGROUP_SPL_SVC_ESTIMATE)
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_HOME Then 'home price
                    EnableDisableMethodOfPriceFields(PRICE_FIELD_HOME_PRICE, Codes.PRICEGROUP_SPL_SVC_HOME)
                Else 'manual  
                    EnableDisableMethodOfPriceFields(PRICE_FIELD_OTHER_PRICE, Codes.PRICEGROUP_SPL_SVC_OTHER)
                End If
            Else
                If State.prev_SplSvc_value Then
                    State.SplSvc_value = False
                    AssignOriginalValuestoPriceFields()
                End If
            End If
        End With
        State.prev_SplSvc_value = State.SplSvc_value
    End Sub
    Sub AssignOriginalValuestoPriceFields()

        With State
            TextboxDeductibleShadow.Text = .original_Deductible
            TextBoxDiscountShadow.Text = .original_Discount
            TextboxAssurantPaysShadow.Text = .original_AssurantPays
            TextboxConsumerPaysShadow.Text = .original_ConsumerPays
            TextboxDueToSCFromAssurantShadow.Text = .original_DueToSCFromAssurant
            TextBoxOtherPriceShadow.Text = .original_OtherPrice
            TextboxLiabilityLimitShadow.Text = .original_LiabilityLimit

            TextboxDeductible_WRITE.Text = .original_Deductible
            TextBoxDiscount.Text = .original_Discount
            TextboxAssurantPays.Text = .original_AssurantPays
            TextboxConsumerPays.Text = .original_ConsumerPays
            TextboxDueToSCFromAssurant.Text = .original_DueToSCFromAssurant
            TextBoxOtherPrice.Text = .original_OtherPrice
            TextboxLiabilityLimit.Text = .original_LiabilityLimit
        End With

        If Not State.isMethodPricePanel_Visible Then
            TextBoxReplacementCost.Text = State.original_AuthorizedAmount
            TextboxAuthorizedAmount.Text = State.original_AuthorizedAmount
            TextboxAuthorizedAmountShadow.Text = State.original_AuthorizedAmount
            ControlMgr.SetVisibleControl(Me, PanelRepair, False)
        Else
            TextboxAuthorizedAmount.Text = State.original_AuthorizedAmount
            TextboxAuthorizedAmountShadow.Text = State.original_AuthorizedAmount
        End If

        UncheckAllMethodPriceCheckBoxes()
        EnableAllMethodPricePanelContols()
        'PopulatePrices()
        'InitialEnableDisableFields()
        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then

            ControlMgr.SetVisibleControl(Me, LabelHomePrice, False)
            ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, False)
            ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, False)

            ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
            CheckBoxCarryInPrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxCarryInPrice.ID + "') </script>"
            RegisterStartupScript("Startup", x)

        ElseIf State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
            ControlMgr.SetVisibleControl(Me, LabelCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, CheckBoxCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, TextBoxCarryInPrice, False)

            ControlMgr.SetVisibleControl(Me, LabelHomePrice, True)
            ControlMgr.SetVisibleControl(Me, CheckBoxHomePrice, True)
            ControlMgr.SetVisibleControl(Me, TextBoxHomePrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
            CheckBoxHomePrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxHomePrice.ID + "') </script>"
            RegisterStartupScript("Startup", x)

        End If

        If ((TextboxAuthorizedAmount.Text <> TextBoxCarryInPrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxCleaningPrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxEstimatePrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxHomePrice.Text)) Then
            PopulateControlFromBOProperty(TextBoxOtherPrice, TextboxAuthorizedAmount.Text)

            CheckBoxOtherPrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxHomePrice.ID + "') </script>"
            RegisterStartupScript("Startup", x)

        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("SEND_IN_PRICE")
        ElseIf State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("PICK_UP_PRICE")
        End If



    End Sub

    Sub UncheckAllMethodPriceCheckBoxes()
        CheckBoxCarryInPrice.Checked = False
        CheckBoxEstimatePrice.Checked = False
        CheckBoxHomePrice.Checked = False
        CheckBoxOtherPrice.Checked = False
        CheckBoxCleaningPrice.Checked = False
    End Sub

    Sub EnableAllMethodPricePanelContols()
        ChangeEnabledProperty(TextBoxCarryInPrice, True)
        ChangeEnabledProperty(TextBoxCleaningPrice, True)
        ChangeEnabledProperty(TextBoxEstimatePrice, True)
        ChangeEnabledProperty(TextBoxHomePrice, True)
        ChangeEnabledProperty(TextBoxOtherPrice, True)
        ChangeEnabledProperty(CheckBoxCarryInPrice, True)
        ChangeEnabledProperty(CheckBoxCleaningPrice, True)
        ChangeEnabledProperty(CheckBoxEstimatePrice, True)
        ChangeEnabledProperty(CheckBoxHomePrice, True)
        ChangeEnabledProperty(CheckBoxOtherPrice, True)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "CauseOfLossId", LabelCauseOfLossId)
        BindBOPropertyToLabel(State.MyBO, "ContactName", LabelContactName)
        BindBOPropertyToLabel(State.MyBO, "CallerName", LabelCallerName)
        BindBOPropertyToLabel(State.MyBO, "ProblemDescription", LabelProblemDescription)
        BindBOPropertyToLabel(State.MyBO, "SpecialInstruction", LabelSpecialInstruction)
        BindBOPropertyToLabel(State.MyBO, "AuthorizedAmount", LabelAuthorizedAmount)
        BindBOPropertyToLabel(State.MyBO, "LiabilityLimit", LabelLiabilityLimit)
        BindBOPropertyToLabel(State.MyBO, "Deductible", LabelDeductible)
        BindBOPropertyToLabel(State.MyBO, "DiscountAmount", LabelDiscount)
        BindBOPropertyToLabel(State.MyBO, "LossDate", LabelLossDate)
        BindBOPropertyToLabel(State.MyBO, "ReportedDate", LabelReportDate)
        BindBOPropertyToLabel(State.MyBO, "CertificateNumber", LabelCertificateNumber)
        'BEGIN - RC - ClaimNumber is never entered by the user, so it should NOT be a Required field
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimNumber", Me.LabelClaimNumber)
        'END - RC - ClaimNumber is never entered by the user, so it should NOT be a Required field
        BindBOPropertyToLabel(State.MyBO, "ServiceCenter", LabelServiceCenter)
        BindBOPropertyToLabel(State.MyBO, "AssurantPays", LabelAssurantPays)
        BindBOPropertyToLabel(State.MyBO, "ConsumerPays", LabelConsumerPays)
        BindBOPropertyToLabel(State.MyBO, "DueToSCFromAssurant", LabelDueToSCFromAssurant)
        BindBOPropertyToLabel(State.MyBO, "IsLawsuitId", LabelIsLawsuitId)

        BindBOPropertyToLabel(State.MyBO, "LoanerTaken", LabelLoaner)
        BindBOPropertyToLabel(State.MyBO, "PolicyNumber", LabelPolicyNumber)
        BindBOPropertyToLabel(State.MyBO, "CallerTaxNumber", LabelCALLER_TAX_NUMBER)

        BindBOPropertyToLabel(State.MyBO, "RepairDate", LabelRepairDate)
        BindBOPropertyToLabel(State.MyBO, "PickUpDate", LabelPickUpDate)
        BindBOPropertyToLabel(State.MyBO, "AuthorizationNumber", LabelInvoiceNumber)
        BindBOPropertyToLabel(State.MyBO, "NewDeviceSKU", lblNewDeviceSKU)
        BindBOPropertyToLabel(State.MyBO, "LoanerRequestedXcd", lblLoanerRequested)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim companyBO As New Company(State.MyBO.CompanyId)
        Dim sSalutation As String = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), companyBO.SalutationId)
        If sSalutation = "Y" Then State.isSalutation = True Else State.isSalutation = False

        Dim certificateBO As New Certificate(State.MyBO.CertificateId)


        Dim listcontextForCauseOfLoss As ListContext = New ListContext()
        listcontextForCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        listcontextForCauseOfLoss.CoverageTypeId = State.MyBO.CoverageTypeId
        listcontextForCauseOfLoss.DealerId = certificateBO.DealerId
        listcontextForCauseOfLoss.ProductCode = certificateBO.ProductCode
        listcontextForCauseOfLoss.LanguageId = Authentication.LangId

        Dim listCauseOfLoss As ListItem() = CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss)
        cboCauseOfLossId.Populate(listCauseOfLoss, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })


        If State.isSalutation Then
            'Me.BindListControlToDataView(Me.cboContactSalutationId, LookupListNew.GetSalutationLookupList(Authentication.LangId))
            'Me.BindListControlToDataView(Me.cboCallerSalutationId, LookupListNew.GetSalutationLookupList(Authentication.LangId))

            Dim salutation As ListItem() = CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode())
            cboContactSalutationId.Populate(salutation, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

            cboCallerSalutationId.Populate(salutation, New PopulateOptions() With
                                         {
                                           .AddBlankItem = True
                                          })

        End If

        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        'Me.BindListControlToDataView(Me.cboLawsuitId, yesNoLkL)


        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        cboLawsuitId.Populate(yesNoLkL, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

        ''REQ-784
        'Me.BindListControlToDataView(Me.cboUseShipAddress, LookupListNew.GetYesNoLookupList(Authentication.LangId, False), , , False)
        cboUseShipAddress.Populate(yesNoLkL, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })


        ' DEF-1622, set default value to No to avoid unexpected default value by by language sorting order
        Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        SetSelectedItem(cboUseShipAddress, NoId)

        BindListControlToDataView(ddlIssueCode, State.MyBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
        BindListControlToDataView(ddlIssueDescription, State.MyBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)

        ' Me.BindListControlToDataView(Me.cboDedCollMethod, LookupListNew.GetDedCollMethodLookupList(Authentication.LangId))

        Dim dedCollMethod As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEDCOLLMTHD", Thread.CurrentPrincipal.GetLanguageCode())
        cboDedCollMethod.Populate(dedCollMethod, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True 'Fix for Bug 221324 - Adding blank Item
                                           })

        Dim selDedCollMethod As String = LookupListNew.GetCodeFromId(LookupListCache.LK_DED_COLL_METHOD, State.MyBO.DedCollectionMethodID)
        If selDedCollMethod IsNot Nothing Then
            SetSelectedItem(cboDedCollMethod, LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, selDedCollMethod))
        End If

        Dim listcontextForMgList As ListContext = New ListContext()
        listcontextForMgList.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim manufacturerList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMgList)

    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            Dim oCoverageType As New CoverageType(State.MyBO.CoverageTypeId)
            Dim oTranslate As Boolean = True
            Dim oCauseOfLoss As String

            Dim objDateOfReport As DateType = Nothing
            Dim strDateOfReport As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)

            Dim objDateOfLoss As DateType = Nothing
            Dim strDateOfLoss As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)

            Try
                If Not .CauseOfLossId.Equals(Guid.Empty) Then
                    Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindById(.CauseOfLossId)
                    If oCoverageLoss IsNot Nothing Then
                        SetSelectedItem(cboCauseOfLossId, .CauseOfLossId)
                    Else
                        SetSelectedItem(cboCauseOfLossId, Guid.Empty)
                        oCauseOfLoss = LookupListNew.GetCodeFromId(LookupListNew.LK_CAUSES_OF_LOSS, .CauseOfLossId)
                        oTranslate = False
                        Dim strErrorMess As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE)
                        Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE, oCauseOfLoss & "  " & strErrorMess)
                    End If

                Else
                    ' REQ-1153: The commented code below is moved to the claim BO and replaced by one line.
                    'Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindDefault

                    'If Not oCoverageLoss Is Nothing Then
                    '    Me.SetSelectedItem(Me.cboCauseOfLossId, oCoverageLoss.CauseOfLossId)
                    'Else
                    '    Me.SetSelectedItem(Me.cboCauseOfLossId, Guid.Empty)
                    'End If
                    SetSelectedItem(cboCauseOfLossId, State.MyBO.GetCauseOfLossID(State.MyBO.CoverageTypeId))
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController, oTranslate)
            End Try

            ''if cause of loss is theft, bring the police report control
            'Try
            '    If .CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
            '        OrElse .CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
            '        OrElse (.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper _
            '                AndAlso ((New Company(.CompanyId)).PoliceRptForLossCovId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)))) Then
            '        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            '            Me.PanelPoliceReport.Visible = False
            '            Me.State.PoliceReportBO = Nothing
            '        Else
            '            Me.State.PoliceReportBO = Me.State.MyBO.AddNewPoliceReport(True)
            '            Me.UserCtrPoliceReport.Bind(Me.State.PoliceReportBO, UserControlMessageController)
            '            Me.PanelPoliceReport.Visible = True
            '        End If
            '    Else
            '        Me.PanelPoliceReport.Visible = False
            '        Me.State.PoliceReportBO = Nothing
            '    End If
            'Catch dnfex As BN.DataNotFoundException
            '    ' its a valid scenario, there may or may NOT be a police report data for that claim id,
            '    ' so do NOT throw exception, just get a new object !
            '    Me.State.PoliceReportBO = Me.State.MyBO.AddNewPoliceReport(False)
            '    Me.UserCtrPoliceReport.Bind(Me.State.PoliceReportBO, UserControlMessageController)
            '    Me.PanelPoliceReport.Visible = True
            'Catch ex As Exception
            '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
            'End Try

            'REQ- 1057 - The Police BO will be populated from Police Report Claim Issue 

            If State.MyBO.PoliceReport IsNot Nothing Then
                State.PoliceReportBO = State.MyBO.PoliceReport
                UserCtrPoliceReport.Bind(State.PoliceReportBO, UserControlMessageController)
                PanelPoliceReport.Visible = True
                moUserControlPoliceReport.ChangeEnabledControlProperty(False)
            Else
                State.PoliceReportBO = Nothing
                PanelPoliceReport.Visible = False
            End If

            txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
            txtCreatedDate.Text = DateTime.Now.ToString(LocalizationMgr.CurrentCulture)

            Dim myContractId As Guid = Contract.GetContractID(State.MyBO.CertificateId)
            curContractId = GuidControl.GuidToHexString(myContractId)
            curCertId = GuidControl.GuidToHexString(State.MyBO.CertificateId)
            curCertItemCoverageId = GuidControl.GuidToHexString(State.MyBO.CertItemCoverageId)
            curMethodOfRepairCode = State.MyBO.MethodOfRepairCode

            PopulateControlFromBOProperty(TextboxLossDate, .LossDate)
            PopulateControlFromBOProperty(TextboxReportDate, .ReportedDate)
            State.MyBO.RecalcDeductibleForExpOrAuthAmountPercent()

            PopulatePrices()


            PopulateControlFromBOProperty(TextboxContactName, .ContactName)
            PopulateControlFromBOProperty(TextboxCallerName, .CallerName)
            PopulateControlFromBOProperty(TextboxProblemDescription, .ProblemDescription)
            PopulateControlFromBOProperty(TextboxSpecialInstruction, .SpecialInstruction)
            PopulateControlFromBOProperty(TextboxAuthorizedAmount, .AuthorizedAmount)
            PopulateControlFromBOProperty(TextboxLiabilityLimit, .LiabilityLimit)
            SetSelectedItem(cboLawsuitId, State.MyBO.IsLawsuitId)

            Dim moCertItemCvg As New CertItemCoverage(.CertItemCoverageId)
            Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
            Dim moCert As New Certificate(moCertItem.CertId)
            Dim moDealer As New Dealer(moCert.DealerId)

            PopulateControlFromBOProperty(TextboxDeductible_WRITE, .Deductible)

            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified AndAlso .IsNew Then
                'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                If strDateOfLoss IsNot Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) Then
                    objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))
                    PopulateControlFromBOProperty(TextboxLossDate, objDateOfLoss)
                End If
            Else
                PopulateControlFromBOProperty(TextboxLossDate, .LossDate)

            End If
            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified AndAlso .IsNew Then
                'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                If strDateOfReport IsNot Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                    objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                    PopulateControlFromBOProperty(TextboxReportDate, objDateOfReport)
                End If
            Else
                PopulateControlFromBOProperty(TextboxReportDate, .ReportedDate)

            End If
            PopulateControlFromBOProperty(TextboxCertificateNumber, .CertificateNumber)
            PopulateControlFromBOProperty(TextboxClaimNumber, .ClaimNumber)
            PopulateControlFromBOProperty(TextboxServiceCenter, .ServiceCenter)
            PopulateControlFromBOProperty(TextboxAssurantPays, .AssurantPays)
            PopulateControlFromBOProperty(TextboxConsumerPays, .ConsumerPays)
            PopulateControlFromBOProperty(TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
            If (moDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (moDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                ControlMgr.SetVisibleControl(Me, LabelDueToSCFromAssurant, False)
                ControlMgr.SetVisibleControl(Me, TextboxDueToSCFromAssurant, False)
                ControlMgr.SetVisibleControl(Me, TextboxDueToSCFromAssurantShadow, False)
            End If
            PopulateControlFromBOProperty(TextBoxDiscount, .DiscountAmount)
            PopulateControlFromBOProperty(TextboxPolicyNumber, .PolicyNumber)

            PopulateControlFromBOProperty(TextboxRepairdate, .RepairDate)
            PopulateControlFromBOProperty(TextboxPickUpDate, .PickUpDate)

            CheckBoxCarryInPrice.Checked = (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP)
            CheckBoxHomePrice.Checked = State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME

            If State.isSalutation Then
                Dim certificateBO As New Certificate(State.MyBO.CertificateId)
                If State.MyBO.CallerSalutationID.Equals(Guid.Empty) Then
                    SetSelectedItem(cboCallerSalutationId, certificateBO.SalutationId)
                Else
                    SetSelectedItem(cboCallerSalutationId, State.MyBO.CallerSalutationID)
                End If
                If State.MyBO.ContactSalutationID.Equals(Guid.Empty) Then
                    SetSelectedItem(cboContactSalutationId, certificateBO.SalutationId)
                Else
                    SetSelectedItem(cboContactSalutationId, State.MyBO.ContactSalutationID)
                End If
            End If

            Dim ContractBO As New Contract(myContractId)

            If ContractBO.PayOutstandingPremiumId.Equals(State.yesId) Then
                Dim dv As DataView
                dv = Certificate.PremiumTotals(State.MyBO.CertificateId)
                State.oGrossAmtReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)
                Dim dvBilling As DataView

                If State.mDealer.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                    dvBilling = BillingPayDetail.getBillPayTotals(State.MyBO.CertificateId)
                    State.oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
                Else
                    dvBilling = BillingDetail.getBillingTotals(State.MyBO.CertificateId)
                    State.oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                End If


                State.oOutstandingPremAmt = State.oGrossAmtReceived - State.oBillingTotalAmount
                PopulateControlFromBOProperty(TextboxOutstandingPremAmt, State.oOutstandingPremAmt)
            End If

            CheckBoxLoanerTaken.Checked = State.MyBO.LoanerTaken

            If State.MyBO.LoanerRquestedXcd = Codes.EXT_YESNO_Y Then
                ChkLoanerRequested.Checked = True
            Else
                ChkLoanerRequested.Checked = False
            End If

            PopulateControlFromBOProperty(TextboxCALLER_TAX_NUMBER, .CallerTaxNumber)

            PopulateControlFromBOProperty(TextboxInvoiceNumber, .AuthorizationNumber)
            PopulateControlFromBOProperty(txtNewDeviceSKU, .NewDeviceSku)
            'REQ-1153
            If Not .ContactInfoId.Equals(Guid.Empty) Then
                Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
                SetSelectedItem(cboUseShipAddress, YesId)
                moUserControlContactInfo.Visible = True

                UserControlAddress.ClaimDetailsBind(State.MyBO.ContactInfo.Address)
                UserControlContactInfo.Bind(State.MyBO.ContactInfo)
            End If
            hdnDealerId.Value = State.MyBO.Dealer.Id.ToString
        End With

    End Sub

    Private Sub TextboxLossDate_TextChanged(sender As Object, e As System.EventArgs) Handles TextboxLossDate.TextChanged
        CalcDeductibleBasedOnPercentOfListPrice()
    End Sub

    Private Sub txtNewDeviceSKU_TextChanged(sender As Object, e As System.EventArgs) Handles txtNewDeviceSKU.TextChanged
        If State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE Then
            CalcDeductibleBasedOnPercentOfListPrice()
        End If
    End Sub

    Private Sub CalcDeductibleBasedOnPercentOfListPrice()
        If (DateHelper.IsDate(TextboxLossDate.Text)) Then
            Dim moCertItemCvg As New CertItemCoverage(State.MyBO.CertItemCoverageId)
            Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
            Dim moCert As New Certificate(moCertItem.CertId)

            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(State.MyBO.CertItemCoverageId, State.MyBO.MethodOfRepairId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                Dim deductible As DecimalType, strSKU As String, dtLossDate As Date

                dtLossDate = DateHelper.GetDateValue(TextboxLossDate.Text)
                strSKU = txtNewDeviceSKU.Text.Trim
                If strSKU = String.Empty Or (Not ListPrice.IsSKUValid(moCert.DealerId, strSKU, dtLossDate, deductible)) Then
                    strSKU = moCertItem.SkuNumber
                    deductible = ListPrice.GetListPrice(moCert.DealerId, strSKU, dtLossDate.ToString("yyyyMMdd"))
                End If

                If (deductible <> Nothing) Then
                    State.MyBO.Deductible = deductible.Value * oDeductible.DeductiblePercentage.Value / 100
                Else
                    Me.State.MyBO.Deductible = 0
                End If

                PopulateControlFromBOProperty(TextboxDeductible_WRITE, State.MyBO.Deductible)
                TextboxDeductibleShadow.Text = TextboxDeductible_WRITE.Text
            End If
        End If
    End Sub

    Protected Sub PopulatePoliceReportBOFromUserCtr(blnExcludePoliceReportSave As Boolean)
        With State.PoliceReportBO
            .ClaimId = State.MyBO.Id
            ' determine validate or dont validate
            UserCtrPoliceReport.PopulateBOFromControl(blnExcludePoliceReportSave)
        End With
    End Sub

    Sub PopulateClaimedEnrolledDetails()
        Dim allowEnrolledDeviceUpdate As AttributeValue = State.MyBO.Dealer.AttributeValues.FirstOrDefault(Function(attributeValue) attributeValue.Attribute.UiProgCode = Codes.DLR_ATTR_ALLOW_MODIFY_CLAIMED_DEVICE)
        With State.MyBO
            If .EnrolledEquipment IsNot Nothing Or .ClaimedEquipment IsNot Nothing Then
                With ucClaimDeviceInfo
                    .thisPage = Me
                    .ClaimBO = CType(State.MyBO, ClaimBase)
                    If allowEnrolledDeviceUpdate IsNot Nothing AndAlso allowEnrolledDeviceUpdate.Value = Codes.YESNO_Y Then
                        For Each i As ClaimIssue In State.MyBO.ClaimIssuesList
                            If i.IssueCode = ISSUE_CODE_CR_DEVICE_MIS and i.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Then
                                    .ShowDeviceEditImg = True
                                    Exit For
                             Else 
                                .ShowDeviceEditImg = False
                            End If
                        Next
                    Else 
                        .ShowDeviceEditImg = False
                    End If
                End With
            End If
        End With
    End Sub

    Private Sub PopulateServiceCenterSelected()

        With State.MyBO.ServiceCenterObject
            Try

                PopulateControlFromBOProperty(lblServiceCenterCode, .Code)
                PopulateControlFromBOProperty(lblServiceCenterContactName, .ContactName)

                PopulateControlFromBOProperty(lblServiceCenterName, .Description)
                PopulateControlFromBOProperty(lblServiceCenterPhone1, .Phone1)

                PopulateControlFromBOProperty(lblServiceCenterAddress1, .Address.Address1)
                PopulateControlFromBOProperty(lblServiceCenterPhone2, .Phone2)

                PopulateControlFromBOProperty(lblServiceCenterAddress2, .Address.Address2)
                PopulateControlFromBOProperty(lblServiceCenterFax, .Fax)

                PopulateControlFromBOProperty(lblServiceCenterCity, .Address.City)
                PopulateControlFromBOProperty(lblServiceCenterBussHours, .BusinessHours)

                PopulateControlFromBOProperty(lblServiceCenterState, LookupListNew.GetDescriptionFromId(LookupListNew.LK_REGIONS, .Address.RegionId))
                PopulateControlFromBOProperty(lblServiceCenterProcessFee, .ProcessingFee)

                PopulateControlFromBOProperty(lblServiceCenterCountry, .Address.countryBO.Description)
                PopulateControlFromBOProperty(lblServiceCenterEmail, .Email)

                PopulateControlFromBOProperty(lblServiceCenterZip, .Address.ZipLocator)
                PopulateControlFromBOProperty(lblServiceCenterCCEmail, .CcEmail)

                PopulateControlFromBOProperty(lblServiceCenterOrigDealer, State.mDealer.DealerName)
                IIf(.DefaultToEmailFlag, chkServiceCenterDefToEmail.Checked, Not chkServiceCenterDefToEmail.Checked)

                IIf(.Shipping, chkServiceCenterShipping.Checked, Not chkServiceCenterShipping.Checked)
                PopulateControlFromBOProperty(txtServiceCenterComments, .Comments)


            Catch ex As Exception

            End Try
        End With
    End Sub

    Sub PopulatePrices()
        'Dim pgDetail As PriceGroupDetail = Me.State.MyBO.GetCurrentPriceGroupDetail
        Dim dTaxRate As Decimal
        'If Not pgDetail Is Nothing Then
        '    dTaxRate = State.MyBO.GetIVATaxRate()
        'End If
        Dim nCarryInPrice As New DecimalType(0)
        Dim nCleaningPrice As New DecimalType(0)
        Dim nEstimatePrice As New DecimalType(0)
        Dim nHomePrice As New DecimalType(0)
        Dim nOtherPrice As New DecimalType(0)
        Dim nReplacementCost As New DecimalType(0)
        Dim nReplacementPrice As New DecimalType(0)

        'For Req-557 - TMK Blast
        'Start
        ''''Dim lCertItemCoverage As New CertItemCoverage(Me.State.InputParameters.CertItemCoverageId)
        Dim lCertItemCoverage As New CertItemCoverage(State.MyBO.CertItemCoverageId)
        Dim lCertItem As New CertItem(lCertItemCoverage.CertItemId)
        Dim lCertificate As New Certificate(lCertItem.CertId)

        'Dim nDiscountedPrice As New DecimalType(0)

        'Req-557 - check if the certificate has a campaign number 
        'if it has then check if Discounted Price has a value. Assign it accordingly.
        'pgDetail.ReplacementPrice = discountedPrice

        'If Not lCertificate.CampaignNumber Is Nothing Then
        '    If ((Not pgDetail.DiscountedPrice Is Nothing) AndAlso (CType(pgDetail.DiscountedPrice, Decimal) > 0)) Then
        '        pgDetail.ReplacementPrice = pgDetail.DiscountedPrice
        '        nReplacementCost = pgDetail.DiscountedPrice
        '    End If
        'End If
        'Get the price
        Dim dv As DataView

        Dim price As Decimal = 0
        dv = State.MyBO.GetRePairPricesByMethodOfRepair

        If dv IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then
            price = CDec(dv.Table.Rows(0)(COL_PRICE_DV))
            Select Case dv.Table.Rows(0)(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE).ToString()
                Case Codes.METHOD_OF_REPAIR_SEND_IN
                    nCarryInPrice = price 'CDec(dv.Table.Rows(0)(COL_PRICE_DV))
                Case Codes.METHOD_OF_REPAIR_PICK_UP
                    nCarryInPrice = price
                Case Codes.METHOD_OF_REPAIR_CARRY_IN
                    nCarryInPrice = price
                Case Codes.METHOD_OF_REPAIR_CLEANING
                    nCleaningPrice = price
                Case Codes.METHOD_OF_REPAIR_AT_HOME
                    nHomePrice = price
                Case Codes.METHOD_OF_REPAIR_DISCOUNTED
                    nReplacementCost = price
                Case Codes.METHOD_OF_REPAIR_REPLACEMENT
                    nReplacementPrice = price
            End Select
        End If

        'End - Req 557

        PopulateControlFromBOProperty(TextBoxCarryInPrice, nCarryInPrice)
        PopulateControlFromBOProperty(TextBoxCleaningPrice, nCleaningPrice)
        PopulateControlFromBOProperty(TextBoxEstimatePrice, nEstimatePrice)
        PopulateControlFromBOProperty(TextBoxHomePrice, nHomePrice)
        PopulateControlFromBOProperty(TextBoxOtherPrice, nOtherPrice)
        PopulateControlFromBOProperty(TextBoxReplacementCost, nReplacementCost)


        If ((State.MyBO.ClaimActivityCode Is Nothing OrElse State.MyBO.ClaimActivityCode.Trim.Length = 0) AndAlso dv IsNot Nothing) Or State.MyBO.ClaimSpecialServiceId = State.yesId Then
            ''''REQ_1106 - Price list integration

            'calculating the estimate price
            price = 0
            Dim dvEstimate As DataView = State.MyBO.GetPricesForServiceType(Codes.SERVICE_CLASS__REPAIR, Codes.SERVICE_TYPE__ESTIMATE_PRICE)
            If dvEstimate IsNot Nothing AndAlso dvEstimate.Count > 0 Then
                price = CDec(dvEstimate(0)(COL_PRICE_DV))
                nEstimatePrice = price 'CDec(dvEstimate.Table.Rows(0)(COL_PRICE_DV))
            End If

            PopulateControlFromBOProperty(TextBoxCarryInPrice, nCarryInPrice)
            PopulateControlFromBOProperty(TextBoxCleaningPrice, nCleaningPrice)
            PopulateControlFromBOProperty(TextBoxEstimatePrice, nEstimatePrice)
            PopulateControlFromBOProperty(TextBoxHomePrice, nHomePrice)
            ''''

            'BEGIN - If the AuthorizedAmount is NOT Equal to any of the PriceGroupDetail prices, then set the 
            '        "OtherPrice" = AuthorizedAmount
            ''''set the authorized amount REQ-1106
            'Me.State.MyBO.SetAuthorizedAmount()

            If ((State.MyBO.AuthorizedAmount.Value <> nCarryInPrice.Value) AndAlso
                 (State.MyBO.AuthorizedAmount.Value <> nCleaningPrice.Value) AndAlso
                 (State.MyBO.AuthorizedAmount.Value <> nEstimatePrice.Value) AndAlso
                 (State.MyBO.AuthorizedAmount.Value <> nHomePrice.Value)) Then
                PopulateControlFromBOProperty(TextBoxOtherPrice, State.MyBO.AuthorizedAmount)

                UncheckAllMethodPriceCheckBoxes()
                CheckBoxOtherPrice.Checked = True
            End If
            'END - If the AuthorizedAmount is NOT Equal to any of the PriceGroupDetail prices, then set the 
            '        "OtherPrice" = AuthorizedAmount

        End If

        If (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT _
       And (State.MyBO.ClaimSpecialServiceId.Equals(Guid.Empty) Or
            State.MyBO.ClaimSpecialServiceId = State.noId)) Then
            ' Replacement Price
            If State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                TextBoxReplacementCost.Text = TextboxAuthorizedAmount.Text
            Else
                'If Not pgDetail Is Nothing Then
                '    pgDetail.ReplacementPrice = pgDetail.ReplacementPrice.Value * (1 + dTaxRate)
                '    Me.PopulateControlFromBOProperty(Me.TextBoxReplacementCost, pgDetail.ReplacementPrice)
                'End If
                PopulateControlFromBOProperty(TextBoxReplacementCost, nReplacementPrice)
                TextboxAuthorizedAmount.Text = TextBoxReplacementCost.Text
                NotifyAuthorizedAmountHasChanged()
            End If

        End If
        EnableDisableFields()
    End Sub

    '' REQ-784
    Protected Sub PopulateNewClaimContactInfoBOsFromForm()
        State.MyBO.ContactInfo.Address.InforceFieldValidation = True
        UserControlContactInfo.PopulateBOFromControl(True)
        State.MyBO.ContactInfo.Save()
    End Sub

    Protected Sub PopulateBOsFromForm()

        ' Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)

        With State.MyBO

            PopulateBOProperty(State.MyBO, "CauseOfLossId", cboCauseOfLossId)
            PopulateBOProperty(State.MyBO, "IsLawsuitId", cboLawsuitId)
            PopulateBOProperty(State.MyBO, "ContactName", TextboxContactName)
            PopulateBOProperty(State.MyBO, "CallerName", TextboxCallerName)
            PopulateBOProperty(State.MyBO, "CallerTaxNumber", TextboxCALLER_TAX_NUMBER)
            PopulateBOProperty(State.MyBO, "ProblemDescription", TextboxProblemDescription)
            PopulateBOProperty(State.MyBO, "SpecialInstruction", TextboxSpecialInstruction)
            PopulateBOProperty(State.MyBO, "AuthorizedAmount", TextboxAuthorizedAmount)
            PopulateBOProperty(State.MyBO, "Deductible", TextboxDeductible_WRITE)
            PopulateBOProperty(State.MyBO, "DiscountAmount", TextBoxDiscount)
            PopulateBOProperty(State.MyBO, "PolicyNumber", TextboxPolicyNumber)
            PopulateBOProperty(State.MyBO, "LossDate", TextboxLossDate)
            PopulateBOProperty(State.MyBO, "ReportedDate", TextboxReportDate)
            PopulateBOProperty(State.MyBO, "NewDeviceSku", txtNewDeviceSKU)

            'following logic is only when its displayed - Backend Claim
            If TextboxRepairdate.Visible = True Then
                PopulateBOProperty(State.MyBO, "RepairDate", TextboxRepairdate)
            End If
            'following logic is only when its displayed - Backend Claim
            If TextboxPickUpDate.Visible = True Then
                PopulateBOProperty(State.MyBO, "PickUpDate", TextboxPickUpDate)
            End If
            If TextboxInvoiceNumber.Visible = True Then
                PopulateBOProperty(State.MyBO, "AuthorizationNumber", TextboxInvoiceNumber)
            End If

            If State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                'CalLiabLimitUsingLossDateAndDeprSchedule()
                PopulateBOProperty(State.MyBO, "LiabilityLimit", TextboxLiabilityLimit)
            End If

            If State.isSalutation Then
                PopulateBOProperty(State.MyBO, "ContactSalutationID", cboContactSalutationId)
                PopulateBOProperty(State.MyBO, "CallerSalutationID", cboCallerSalutationId)
            End If

            State.MyBO.LoanerTaken = CheckBoxLoanerTaken.Checked

            If ChkLoanerRequested.Checked Then
                PopulateBOProperty(State.MyBO, "LoanerRquestedXcd", Codes.EXT_YESNO_Y)
            Else
                PopulateBOProperty(State.MyBO, "LoanerRquestedXcd", Codes.EXT_YESNO_N)
            End If

            If PanelPoliceReport.Visible = True Then
                'Dim blnExcludePoliceReportSave As Boolean
                'If Me.moUserControlPoliceReport.isempty Then
                'End If
                'Exclude the validation for the policereport BO from here, so excludesave shld be set to true
                PopulatePoliceReportBOFromUserCtr(True)
            Else
                State.PoliceReportBO = Nothing
            End If

            If Not State.MyBO.CauseOfLossId = Guid.Empty Then
                State.MyBO.ClaimSpecialServiceId = State.MyBO.GetSpecialServiceValue()
                If State.MyBO.ClaimSpecialServiceId = State.yesId Then
                    PopulateBOProperty(State.MyBO, "WhoPaysId", AssurantPays)
                End If
            Else
                State.MyBO.ClaimSpecialServiceId = State.noId
            End If

            '' REQ-784 
            If State.MyBO.ContactInfo IsNot Nothing Then
                If State.MyBO.ContactInfo.IsDeleted = False Then
                    State.MyBO.ContactInfoId = State.MyBO.ContactInfo.Id
                End If
            End If

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cboUseShipAddress.SelectedValue = YesId.ToString Then
            PopulateNewClaimContactInfoBOsFromForm()
        End If
    End Sub

    ' Clean Popup Input
    Private Sub CleanPopupInput()
        Try
            If State IsNot Nothing Then
                'Clean after consuming the action
                State.LastState = InternalStates.Regular
                HiddenSaveChangesPromptResponse.Value = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Function CheckIfComingFromCreateClaimConfirm() As Boolean
        Dim returnValue As Boolean = False
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        CleanPopupInput()

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            'Clean after consuming the action
            'Me.State.LastState = InternalStates.Regular
            'Me.HiddenSaveChangesPromptResponse.Value = ""
            CreateClaim()
            returnValue = True
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            'Nothing To Do. Stay on the page
        End If
        Return returnValue
    End Function

    Sub CreateClaim()
        '  If Me.State.MyBO.IsNew AndAlso Me.State.MyBO.ClaimNumber Is Nothing Then Me.State.MyBO.ObtainAndAssignClaimNumber()

        Dim blnExceedMaxReplacements As Boolean = False
        Dim blnClaimReportedWithinPeriod As Boolean = True
        Dim IsCoverageForTheft As Boolean = False
        Dim blnIsDealerDRPTradeInQuoteFlag As Boolean = False
        Dim blnClaimReportedWithinGracePeriod As Boolean = True
        Dim blnCoverageTypeNotMissing As Boolean = True

        'If replacement, check max replacement allowed per calendar year
        With State.MyBO

            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified Then

                If .IsNew Then 'only check the condition for new claim
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then ' Not a Valid Check for Service Warranty Claims
                        blnCoverageTypeNotMissing = .IsClaimReportedWithValidCoverage(.CertificateId, .CertItemCoverageId, .LossDate.Value, .ReportedDate.Value)
                    End If
                End If

                If .IsNew Then 'only check the condition for new claim
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then ' Not a Valid Check for Service Warranty Claims
                        blnClaimReportedWithinGracePeriod = .IsClaimReportedWithinGracePeriod(.CertificateId, .CertItemCoverageId, .LossDate.Value, .ReportedDate.Value)
                    End If
                End If

                If blnClaimReportedWithinGracePeriod And blnCoverageTypeNotMissing Then
                    If .IsNew AndAlso Not (State.MyBO.CheckSvcWrantyClaimControl() And State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then
                        blnExceedMaxReplacements = .IsMaxReplacementExceeded(.CertificateId, .LossDate.Value)
                    End If
                End If

            Else
                If .IsNew AndAlso Not (State.MyBO.CheckSvcWrantyClaimControl() And State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then 'only check the condition for new claim
                    blnExceedMaxReplacements = .IsMaxReplacementExceeded(.CertificateId, .LossDate.Value)
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then ' Not a Valid Check for Service Warranty Claims
                        blnClaimReportedWithinPeriod = .IsClaimReportedWithinPeriod(.CertificateId, .LossDate.Value, .ReportedDate.Value)
                    End If
                End If

            End If



            If .CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                   OrElse .CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
                   OrElse .CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper Then

                IsCoverageForTheft = True
            End If

            blnIsDealerDRPTradeInQuoteFlag = .GetDealerDRPTradeInQuoteFlag(.DealerCode)
            If blnIsDealerDRPTradeInQuoteFlag = True And .IsNew Then
                'call DRP web service to check if active trade-in or quotes exist for the device
                If .VerifyIMEIWithDRPSystem(.SerialNumber) Then
                    Throw New GUIException(Message.MSG_ACTIVE_TRADEIN_QUOTE_EXISTS_ERR, Assurant.ElitaPlus.Common.ErrorCodes.ACTIVE_TRADEIN_QUOTE_EXISTS_ERR)
                End If
            End If
        End With

        'Call the Create Comment Logic
        Dim c As Comment = State.MyBO.AddNewComment()

        'Add comments to indicate that the claim will be closed

        If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified Then

            If Not blnClaimReportedWithinGracePeriod Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__NOT_REPORTED_WITHIN_GRACE_PERIOD)
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_NOT_WITHIN_GRACE_PERIOD)
            End If

            If Not blnCoverageTypeNotMissing Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__COVERAGE_TYPE_MISSING)
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_COVERAGE_TYPE_MISSING)
            End If

            If blnClaimReportedWithinGracePeriod And blnCoverageTypeNotMissing Then

                If blnExceedMaxReplacements Then
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
                End If

                If Not State.MyBO.Certificate.IsSubscriberStatusValid Then
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
                End If

                If (CType(State.MyBO.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Integer) > 0) Then
                    If (State.MyBO.CoverageRemainLiabilityAmount(State.MyBO.CertItemCoverageId, State.MyBO.LossDate) <= 0) Then
                        State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                    End If
                End If
                If (CType(State.MyBO.Certificate.ProductLiabilityLimit.ToString, Integer) > 0) Then
                    If (State.MyBO.ProductRemainLiabilityAmount(State.MyBO.CertificateId, State.MyBO.LossDate) <= 0) Then
                        State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                    End If
                End If
            End If
        Else

            If blnExceedMaxReplacements Then
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
            End If
            If Not blnClaimReportedWithinPeriod Then
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_EXCEED)
            End If
            If Not State.MyBO.Certificate.IsSubscriberStatusValid Then
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
            End If

            If (CType(State.MyBO.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Integer) > 0) Then
                If (State.MyBO.CoverageRemainLiabilityAmount(State.MyBO.CertItemCoverageId, State.MyBO.LossDate) <= 0) Then
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
            If (CType(State.MyBO.Certificate.ProductLiabilityLimit.ToString, Integer) > 0) Then
                If (State.MyBO.ProductRemainLiabilityAmount(State.MyBO.CertificateId, State.MyBO.LossDate) <= 0) Then
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
        End If


        'Call the Create ShippingInfo Logic
        If State.ShippingInfoBO IsNot Nothing Then State.MyBO.AttachShippingInfo(State.ShippingInfoBO)

        Dim oldStatus As String = State.MyBO.StatusCode
        Try
            Dim blnChangeStatustoActive As Boolean = False
            If State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso
                Not State.MyBO.IsSupervisorAuthorizationRequired AndAlso
                (State.PoliceReportBO IsNot Nothing AndAlso Not State.PoliceReportBO.IsEmpty) AndAlso
                State.MyBO.Certificate.IsSubscriberStatusValid Then

                blnChangeStatustoActive = True
            ElseIf State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso
                    Not State.MyBO.IsSupervisorAuthorizationRequired AndAlso
                    State.PoliceReportBO Is Nothing AndAlso
                    State.MyBO.Certificate.IsSubscriberStatusValid Then

                blnChangeStatustoActive = True
            ElseIf State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso State.oOutstandingPremAmt = 0 AndAlso
                     State.PayOutstandingPremium = True Then

                blnChangeStatustoActive = True
            End If

            'set status to pending if auth amount is based on SKU and list price record not found 
            'If State.isAuthAmtSetBySKU And State.authAmtBySku <= 0 Then
            '    c = Me.State.MyBO.AddNewComment()
            '    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
            '    'todo: insert a new error message
            '    c.Comments = TranslationBase.TranslateLabelOrMessage("AUTH_AMT_BY_SKU_NOT_FOUND_ERR")
            '    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            'End If

            Dim bListPriceFound As Boolean = True
            'REQ 1106 change
            'DEF-21716-START
            'Dim cmnt As Comment
            'DEF-21716-END
            State.MyBO.CreateEnrolledEquipment()
            '''''''''''

            'Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(State.MyBO.CertItemCoverageId, State.MyBO.MethodOfRepairId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                Dim lstPrice As DecimalType
                If State.MyBO.LossDate IsNot Nothing Then
                    Dim strSKU As String = String.Empty
                    If State.MyBO.NewDeviceSku IsNot Nothing Then
                        strSKU = State.MyBO.NewDeviceSku.Trim()
                    End If
                    If strSKU = String.Empty Then strSKU = State.MyBO.CertificateItem.SkuNumber
                    If State.MyBO.CertificateItem.IsEquipmentRequired Then strSKU = State.MyBO.ClaimedEquipment.SKU
                    lstPrice = ListPrice.GetListPrice(State.MyBO.Certificate.DealerId, strSKU, State.MyBO.LossDate.Value.ToString(("yyyyMMdd")))
                    'REQ-918 Get the Claim Equipment Object for this Claim and Assign the above Price to it
                    If (lstPrice Is Nothing) Then
                        bListPriceFound = False
                        'DEF-21716-START
                        c = State.MyBO.AddNewComment()
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                        c.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
                        'cmnt = Me.State.MyBO.AddNewComment()
                        'cmnt.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                        'cmnt.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
                        'DEF-21716-END
                    Else
                        If State.MyBO.EnrolledEquipment IsNot Nothing Then
                            State.MyBO.EnrolledEquipment.Price = lstPrice
                            bListPriceFound = True
                        End If
                    End If
                End If
            End If
            If bListPriceFound Then
                If blnChangeStatustoActive Then
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
                End If
            Else
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            State.MyBO.CalculateFollowUpDate()

            State.MyBO.IsUpdatedComment = True
            If State.PoliceReportBO IsNot Nothing AndAlso
              State.PoliceReportBO.IsEmpty Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            'Set the Claim to Pending Status if the Subscriber Status is not Valid 
            If Not State.MyBO.Certificate.IsSubscriberStatusValid Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            If State.PayOutstandingPremium = True Then
                If State.oOutstandingPremAmt > 0 Then
                    c = State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_PAYMENT_ON_OUTSTANDING_PREMIUM)
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                ElseIf State.oOutstandingPremAmt = 0 And blnChangeStatustoActive Then
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
                End If
            End If

            'If Claim has Open\Pending issues attached to it , Save it in Pending state
            Select Case State.MyBO.IssuesStatus
                Case Codes.CLAIMISSUE_STATUS__OPEN
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    c = State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
                Case Codes.CLAIMISSUE_STATUS__REJECTED
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    If State.MyBO.ClaimIssuesList IsNot Nothing Then
                        For Each Item As ClaimIssue In State.MyBO.ClaimIssuesList
                            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                                If Item.Issue.DeniedReason IsNot Nothing Then
                                    State.MyBO.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, Item.Issue.DeniedReason)
                                Else
                                    State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                End If
                                Exit For
                            End If
                        Next
                    Else
                        State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                    End If

                    'Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                    c = State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
            End Select

            If Not State.MyBO.ValidateAndMatchClaimedEnrolledEquipments(c) Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            'Display a Popup Message to Collect the Deductible if the Dealer is set up for Collection and then Save the Claim
            'Make sure the claim is Authorized and has deductible greater than 0
            If State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And State.MyBO.Deductible.Value > 0D Then
                If (State.mDealer Is Nothing) Then
                    State.mDealer = New Dealer(State.MyBO.CompanyId, State.MyBO.DealerCode)
                End If
                If State.mDealer.DeductibleCollectionId = State.yesId Then
                    'The dropdown with the Deductible Collection Methods is already populated
                    Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                    RegisterStartupScript("Startup", x)
                    Exit Sub
                End If
            End If

            'Close the claim if exceeding the max replacements allowed

            If blnExceedMaxReplacements Then
                If SetSvcCenterToDefault() Then
                    State.MyBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__REPLACEMENT_EXCEED)
                    State.MyBO.DenyClaim()
                Else
                    Exit Sub
                End If
            End If

            'Close the Claim if the period between the Date of loss and the Reported Date is more than the allowed Number of Days 
            If Not blnClaimReportedWithinPeriod Then
                If SetSvcCenterToDefault() Then
                    State.MyBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__NOT_REPORTED_WITHIN_PERIOD)
                    State.MyBO.DenyClaim()
                Else
                    Exit Sub
                End If
            End If

            blnIsDealerDRPTradeInQuoteFlag = State.MyBO.GetDealerDRPTradeInQuoteFlag(State.MyBO.DealerCode)
            If blnIsDealerDRPTradeInQuoteFlag = True Then
                If State.MyBO.IsNew Then
                    'call DRP web service to check if active trade-in or quotes exist for the device
                    If State.MyBO.VerifyIMEIWithDRPSystem(State.MyBO.SerialNumber) Then
                        State.MyBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__ACTIVE_TRADEIN_QUOTE_EXISTS)
                        State.MyBO.DenyClaim()
                    End If
                End If
            End If

            If State.MyBO IsNot Nothing AndAlso State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE Then
                'user story 192764 - Task-199011--Start------
                Dim dsCaseFields As DataSet = CaseBase.GetCaseFieldsList(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (dsCaseFields IsNot Nothing AndAlso dsCaseFields.Tables.Count > 0 AndAlso dsCaseFields.Tables(0).Rows.Count > 0) Then

                    Dim hasBenefit As DataRow() = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                    Dim benefitCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='BENEFITCHECKERROR'")
                    Dim preCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='PRECHECKERROR'")
                    Dim lossType As DataRow() = dsCaseFields.Tables(0).Select("field_code='LOSSTYPE'")

                    If hasBenefit IsNot Nothing AndAlso hasBenefit.Length > 0 Then
                        If hasBenefit(0)("field_value") IsNot Nothing AndAlso String.Equals(hasBenefit(0)("field_value").ToString(), Boolean.FalseString, StringComparison.CurrentCultureIgnoreCase) Then
                            UpdateCaseFieldValues(hasBenefit, lossType)

                            dsCaseFields = CaseBase.GetCaseFieldsList(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                        End If
                    End If
                    If benefitCheckError IsNot Nothing AndAlso benefitCheckError.Length > 0 Then
                        If benefitCheckError(0)("field_value") IsNot Nothing AndAlso Not String.Equals(benefitCheckError(0)("field_value").ToString(), "NO ERROR", StringComparison.CurrentCultureIgnoreCase) Then
                            UpdateCaseFieldValues(benefitCheckError, lossType)

                            dsCaseFields = CaseBase.GetCaseFieldsList(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                        End If
                    End If

                    If preCheckError IsNot Nothing And preCheckError.Length = 0 Then
                        If hasBenefit IsNot Nothing AndAlso hasBenefit.Length > 0 Then
                            If hasBenefit(0)("field_value") IsNot Nothing AndAlso hasBenefit(0)("field_value").ToString().ToUpper() = Boolean.TrueString.ToUpper() Then
                                RunPreCheck(hasBenefit)
                            End If
                        ElseIf benefitCheckError IsNot Nothing AndAlso benefitCheckError.Length > 0 Then
                            If benefitCheckError(0)("field_value") IsNot Nothing AndAlso benefitCheckError(0)("field_value").ToString().ToUpper() <> "NO ERROR" Then
                                RunPreCheck(benefitCheckError)
                            End If
                        End If
                    End If
                End If
            End If

            'user story 192764 - Task-199011--End------
            State.MyBO.Validate()
            'save all the information
            State.MyBO.Save()

            If IsCoverageForTheft = True Then
                With State.MyBO
                    PublishedTask.AddEvent(companyGroupId:=Nothing,
                                           companyId:=Nothing,
                                           countryId:=Nothing,
                                           dealerId:= .Dealer.Id,
                                           productCode:=Nothing,
                                           coverageTypeId:=Nothing,
                                           sender:="New Claim Screen",
                                           arguments:="ClaimId:" & DALBase.GuidToSQLString(.Id),
                                           eventDate:=DateTime.UtcNow,
                                           eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLM_EXT_STATUS),
                                           eventArgumentId:=LookupListNew.GetExtendedStatusByGroupId(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, LookupListNew.GetIdFromCode(Codes.EXTENDED_CLAIM_STATUSES, Codes.CLM_STAT__COM_FB_CLM_UPD_THFT)))
                End With
            End If

            If Not State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso State.MyBO.ServiceCenterObject IsNot Nothing AndAlso Not State.MyBO.ServiceCenterObject.IntegratedWithGVS AndAlso
            State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY AndAlso
           (Not blnExceedMaxReplacements) AndAlso blnClaimReportedWithinPeriod AndAlso blnClaimReportedWithinGracePeriod AndAlso blnCoverageTypeNotMissing AndAlso State.MyBO.Certificate.IsSubscriberStatusValid Then
                'Call The Create Service Order Use Case
                If Not State.MyBO.IsSupervisorAuthorizationRequired() AndAlso
                    ((State.PoliceReportBO Is Nothing) OrElse
                     (State.PoliceReportBO IsNot Nothing AndAlso Not State.PoliceReportBO.IsEmpty)) Then

                    If TextboxRepairdate.Visible = True And (State.MyBO.RepairDate IsNot Nothing) _
                        And TextboxPickUpDate.Visible = True And (State.MyBO.PickUpDate IsNot Nothing) Then
                        ' for backend claim
                        NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                    Else
                        If (bListPriceFound) Then
                            If State.PayOutstandingPremium = False Then
                                Dim soController As New ServiceOrderController
                                Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(State.MyBO)
                                NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = so
                            ElseIf (State.oOutstandingPremAmt = 0 AndAlso State.PayOutstandingPremium = True) Then
                                Dim soController As New ServiceOrderController
                                Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(State.MyBO)
                                NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = so
                            Else
                                NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                            End If
                        Else
                            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                        End If
                    End If
                Else
                    NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                End If
            End If


        Catch ex As Exception
            State.MyBO.StatusCode = oldStatus
            Throw ex
        End Try
        NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.MyBO

        If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified Then
            If Not blnCoverageTypeNotMissing Then
                NavController.Navigate(Me, "create_claim", Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE)

            ElseIf Not blnClaimReportedWithinGracePeriod Then
                NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD)

            ElseIf blnExceedMaxReplacements Then
                NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED)

            Else

                If TextboxRepairdate.Visible = True AndAlso (State.MyBO.RepairDate IsNot Nothing) AndAlso TextboxPickUpDate.Visible = True AndAlso (State.MyBO.PickUpDate IsNot Nothing) Then
                    ' for backend claim
                    NavController.Navigate(Me, "create_claim_backend_pay_claim", Message.MSG_CLAIM_ADDED)
                Else
                    NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
                End If
            End If

        ElseIf blnExceedMaxReplacements Then
            NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED)

        ElseIf Not blnClaimReportedWithinPeriod Then
            NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_PERIOD)

        Else

            If TextboxRepairdate.Visible = True AndAlso (State.MyBO.RepairDate IsNot Nothing) AndAlso TextboxPickUpDate.Visible = True AndAlso (State.MyBO.PickUpDate IsNot Nothing) Then
                ' for backend claim
                NavController.Navigate(Me, "create_claim_backend_pay_claim", Message.MSG_CLAIM_ADDED)
            Else
                NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
            End If
        End If
    End Sub

    Private Sub RunPreCheck(caseRecord As DataRow())
        Try
            Dim benefitCheckResponse As LegacyBridgeResponse
            Dim client As LegacyBridgeServiceClient = Claim.GetLegacyBridgeServiceClient()

            benefitCheckResponse = WcfClientHelper.Execute(Of LegacyBridgeServiceClient, ILegacyBridgeService, LegacyBridgeResponse)(
                client,
                New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(lc As LegacyBridgeServiceClient)
                    Return lc.BenefitClaimPreCheck(GuidControl.ByteArrayToGuid(caseRecord(0)("case_Id")).ToString())
                End Function)

                If (benefitCheckResponse IsNot Nothing) Then
                    State.MyBO.Status = If(benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Approve, BasicClaimStatus.Active, BasicClaimStatus.Pending)
                    If (benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Deny) Then
                        Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECKFAIL")
                        Dim newClaimIssue As ClaimIssue = CType(State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                        newClaimIssue.SaveNewIssue(State.MyBO.Id, issueId, State.MyBO.Certificate.Id, True)
                    End If
                Else
                    State.MyBO.Status = BasicClaimStatus.Pending
                    Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                    Dim newClaimIssue As ClaimIssue = CType(State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(State.MyBO.Id, issueId, State.MyBO.Certificate.Id, True)
                End If

            Catch ex As Exception
                Log(ex)
                State.MyBO.Status = BasicClaimStatus.Pending
                Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                Dim newClaimIssue As ClaimIssue = CType(State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                newClaimIssue.SaveNewIssue(State.MyBO.Id, issueId, State.MyBO.Certificate.Id, True)
            End Try
    End Sub

    Private Shared Sub UpdateCaseFieldValues(ByRef caseFieldRow As DataRow(), ByRef lossType As DataRow())
        Dim caseFieldXcds() As String
        Dim caseFieldValues() As String

        If lossType IsNot Nothing AndAlso lossType.Length > 0 Then
            If lossType(0)("field_value") IsNot Nothing AndAlso (lossType(0)("field_value").ToString().ToUpper() = "ADH1234" Or lossType(0)("field_value").ToString().ToUpper() = "ADH5") Then
                caseFieldXcds = { "CASEFLD-HASBENEFIT", "CASEFLD-ADCOVERAGEREMAINING" }
                caseFieldValues = { Boolean.TrueString.ToUpper(), Boolean.TrueString.ToUpper() }
            Else If lossType(0)("field_value") IsNot Nothing AndAlso lossType(0)("field_value").ToString().ToUpper() = "THEFT/LOSS" Then
                caseFieldXcds = { "CASEFLD-HASBENEFIT" }
                caseFieldValues = { Boolean.TrueString.ToUpper() }
            End If
        End If

        CaseBase.UpdateCaseFieldValues(GuidControl.ByteArrayToGuid(caseFieldRow(0)("case_Id")), caseFieldXcds, caseFieldValues)
    End Sub

    'Sets the Service Center to the Default Service Center for Denied Claims.                                   
    Private Function SetSvcCenterToDefault() As Boolean
        Dim result As Boolean = True
        Dim oCountry As Country
        Dim defaultServCenter As ServiceCenter
        Dim errMsg As String
        Dim oCert As New Certificate(State.MyBO.CertificateId)
        oCountry = New Country(oCert.AddressChild.CountryId)
        If Not (oCountry.DefaultScForDeniedClaims.Equals(Guid.Empty)) Then
            defaultServCenter = New ServiceCenter(oCountry.DefaultScForDeniedClaims)
            State.MyBO.ServiceCenterId = defaultServCenter.Id
        Else
            errMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_DEFAULT_SVC_CENTER_NOT_SETUP)
            MasterPage.MessageController.AddErrorAndShow(errMsg)
            result = False
        End If

        Return result
    End Function

    Private Function CreateClaimDenialMessage(showWarning As Boolean, denialMessage As String, showContinue As Boolean) As String
        Dim sbMsg As New System.Text.StringBuilder
        If showWarning = True Then
            sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_DENIAL_WARNING))
            sbMsg.Append(":")
            sbMsg.Append(" ")
        End If

        sbMsg.Append(TranslationBase.TranslateLabelOrMessage(denialMessage))

        If showContinue = True Then
            sbMsg.Append(",")
            sbMsg.Append(" ")
            sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_CONTINUE_WITH_CLAIM))
        End If
        Return sbMsg.ToString
    End Function

    Protected Sub CheckIfComingFromRecoveryButtonClick()
        If State.InputParameters IsNot Nothing Then
            If State.InputParameters.RecoveryButtonClick = True Then
                State.MyBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
                State.MyBO.CauseOfLossId = State.InputParameters.ClaimBO.CauseOfLossId
                State.MyBO.SpecialInstruction = State.InputParameters.ClaimBO.SpecialInstruction
                State.MyBO.ProblemDescription = State.InputParameters.ClaimBO.ProblemDescription
                State.MyBO.MasterClaimNumber = State.InputParameters.ClaimBO.ClaimNumber
                With State.InputParameters
                    State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimBO.ClaimNumber, .DateOfLoss, .RecoveryButtonClick)
                End With
            End If
        End If
    End Sub

    Protected Sub GetDepreciationSchedule()
        Try
            Dim Cert As New Certificate(State.MyBO.CertificateId)
            Dim certItemCoverage As New CertItemCoverage(State.MyBO.CertItemCoverageId)
            Dim oContractId As Guid = Contract.GetContractID(State.MyBO.CertificateId)
            'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
            Dim i As Integer
            dProductSalesDate = GetDateFormattedString(Cert.ProductSalesDate.Value)
            If Cert.UseDepreciation.Equals(State.yesId) Then
                nLiabilityLimit = certItemCoverage.LiabilityLimits.Value
                PopulateControlFromBOProperty(TextBoxNewLiabilityLimit, nLiabilityLimit)
                If nLiabilityLimit > 0 Then
                    dvDeprSchedule = State.MyBO.GetDepreciationSchedule(oContractId)
                    DeprSchCount = dvDeprSchedule.Count
                    If dvDeprSchedule.Count > 0 Then
                        strLowMonth = ""
                        strHighMonth = ""
                        strPercent = ""
                        strAmount = ""
                        For i = 0 To dvDeprSchedule.Count - 1
                            strLowMonth = strLowMonth & dvDeprSchedule(i)(COL_NAME_LOW_MONTH).ToString & ","
                            strHighMonth = strHighMonth & dvDeprSchedule(i)(COL_NAME_HIGH_MONTH).ToString & ","
                            strPercent = strPercent & dvDeprSchedule(i)(COL_NAME_PERCENT).ToString() & ","
                            strAmount = strAmount & dvDeprSchedule(i)(COL_NAME_AMOUNT).ToString() & ","
                        Next
                        strLowMonth = strLowMonth.Substring(0, strLowMonth.Length - 1)
                        strHighMonth = strHighMonth.Substring(0, strHighMonth.Length - 1)
                        strPercent = strPercent.Substring(0, strPercent.Length - 1)
                        strAmount = strAmount.Substring(0, strAmount.Length - 1)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Repair Price from ListPrice is no longer supported. It is now available as a part of Price List 
    'Protected Function setAuthAmtBySKU(ByRef decAuthAmt As Decimal) As Boolean
    '    'only apply for repair claim
    '    Dim blnAuthAmtBySKU As Boolean = False

    '    If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP _
    '        Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME _
    '        Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN _
    '        Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then

    '        Dim oCert As New Certificate(Me.State.MyBO.CertificateId)
    '        Dim oDealer As New Dealer(oCert.DealerId)
    '        Dim moCertItemCvg As New CertItemCoverage(Me.State.MyBO.CertItemCoverageId)
    '        Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
    '        If oDealer.AuthAmtBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_AUTH_AMT_BASED_ON, ElitaPlusIdentity.Current.ActiveUser.LanguageId), "AUTHA") Then
    '            blnAuthAmtBySKU = True
    '            Dim dAuthAmt As DecimalType = ListPrice.GetRepairAuthAmount(oDealer.Id, moCertItem.SkuNumber, Date.Today)
    '            If dAuthAmt Is Nothing Then
    '                decAuthAmt = 0
    '            Else
    '                decAuthAmt = dAuthAmt.Value
    '            End If
    '        End If
    '    End If
    '    Return blnAuthAmtBySKU
    'End Function

    Private Sub PopulateProtectionAndEventDetail()
        moProtectionEvtDtl.Visible = True
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Dim oCerticate As Certificate = New Certificate(State.MyBO.CertificateId)
            Dim certItemCvg As CertItemCoverage = New CertItemCoverage(State.MyBO.CertItemCoverageId)
            Dim certItem As CertItem = New CertItem(certItemCvg.CertItemId)

            moProtectionEvtDtl.CustomerName = State.MyBO.CustomerName
            moProtectionEvtDtl.DealerName = State.MyBO.DealerName
            moProtectionEvtDtl.CallerName = State.MyBO.CallerName
            moProtectionEvtDtl.ClaimNumber = State.MyBO.ClaimNumber
            If State.MyBO.LossDate IsNot Nothing Then
                moProtectionEvtDtl.DateOfLoss = GetDateFormattedString(State.MyBO.LossDate.Value)
            Else
                moProtectionEvtDtl.DateOfLoss = String.Empty
            End If

            moProtectionEvtDtl.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, certItem.RiskTypeId)
            moProtectionEvtDtl.ProtectionStatus = LookupListNew.GetClaimStatusFromCode(langId, oCerticate.StatusCode)
            moProtectionEvtDtl.EnrolledModel = certItem.Model
            If Not certItem.ManufacturerId.Equals(Guid.Empty) Then
                moProtectionEvtDtl.EnrolledMake = New Manufacturer(certItem.ManufacturerId).Description
            End If

            If (oCerticate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            moProtectionEvtDtl.ProtectionStatusCss = cssClassName

            moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(langId, State.MyBO.StatusCode)
            If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            moProtectionEvtDtl.ClaimStatusCss = cssClassName

            If State.MyBO.ClaimedEquipment IsNot Nothing Then
                moProtectionEvtDtl.ClaimedMake = State.MyBO.ClaimedEquipment.Manufacturer
                moProtectionEvtDtl.ClaimedModel = State.MyBO.ClaimedEquipment.Model
            Else
                moProtectionEvtDtl.ClaimedMake = String.Empty
                moProtectionEvtDtl.ClaimedModel = String.Empty
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub FillWizard(flow As String)

        Dim steps As New List(Of StepDefinition)

        Select Case flow

            Case "CREATE_CLAIM_FROM_CERTIFICATE"
                Dim step1 As New StepDefinition()
                step1.StepNumber = 1
                step1.StepName = "Date of Incident"
                steps.Add(step1)

                Dim step2 As New StepDefinition()
                step2.StepNumber = 2
                step2.StepName = "Coverage Details"
                steps.Add(step2)

                Dim step3 As New StepDefinition()
                step3.StepNumber = 3
                step3.StepName = "Locate Service Center"
                steps.Add(step3)

                Dim step4 As New StepDefinition()
                step4.StepNumber = 4
                step4.StepName = "Claim Details"
                step4.IsSelected = True
                steps.Add(step4)

                Dim step5 As New StepDefinition()
                step5.StepNumber = 5
                step5.StepName = "Submit Claim"
                steps.Add(step5)

                ucWizardControl.Steps = steps
                dvStepWizBox.Visible = True

        End Select
    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
            NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.LastState = InternalStates.ConfirmBackOnError
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnCreateClaim_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCreateClaim_WRITE.Click
        Try

            If State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                If CType(TextboxAuthorizedAmount.Text, Decimal) < 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If
            Else
                If CType(TextboxAuthorizedAmount.Text, Decimal) < 0 Then
                    If System.Math.Abs(CType(TextboxAuthorizedAmount.Text, Decimal)) >= 1000000 Then
                        'display error
                        ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    End If
                End If
            End If

            If LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(Authentication.LangId), State.MyBO.ClaimSpecialServiceId) = Codes.YESNO_Y Then
                If CType(TextboxAuthorizedAmount.Text, Decimal) <= 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If
            End If

            PopulateBOsFromForm()

            If TextboxRepairdate.Visible = True And TextboxPickUpDate.Visible = True Then
                If (State.MyBO.RepairDate IsNot Nothing) And (State.MyBO.PickUpDate Is Nothing) Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelPickUpDate)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PICKUP_DATE_IS_REQUIRED_ERR)
                End If

                If (State.MyBO.RepairDate Is Nothing) And (State.MyBO.PickUpDate IsNot Nothing) Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelRepairDate)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5)
                End If

                'if backend claim then invoice number must be entered
                If (State.MyBO.RepairDate IsNot Nothing) And (State.MyBO.PickUpDate IsNot Nothing) Then
                    If (State.MyBO.AuthorizationNumber Is Nothing) Then
                        ElitaPlusPage.SetLabelError(LabelInvoiceNumber)
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                    End If
                End If

            End If

            If (State.MyBO.RepairDate IsNot Nothing) And (State.MyBO.PickUpDate IsNot Nothing) Then
                If (State.MyBO.AuthorizationNumber Is Nothing) Then
                    ElitaPlusPage.SetLabelError(LabelInvoiceNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                End If
            End If

            If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                If State.MyBO.PolicyNumber Is Nothing Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelPolicyNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
                End If
            End If

            State.MyBO.IsRequiredCheckLossDateForCancelledCert = True 'Always check loss date for Cancelled certificate

            'Me.State.MyBO.Validate()

            'Display a warning message if the claim not reported within grace period and coverage missing from certificate
            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified AndAlso State.MyBO.IsNew Then


                If State.coverageTypeforclaimMissingfromCertificate_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, True)
                        DisplayMessage(denialMessage, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If

                If State.claimNotReportedWithinGracePeriod_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, True)
                        DisplayMessage(denialMessage, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If

                If (State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate))) And
                   (State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate))) Then
                    If State.maxReplacementExceed_MessageDisplayed = False Then
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, True)
                            DisplayMessage(denialMessage, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                            Exit Sub
                        End If
                    End If
                End If

            Else

                If State.maxReplacementExceed_MessageDisplayed = False Then
                    If (Not State.MyBO.CheckSvcWrantyClaimControl() AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK) Then
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, True)
                            DisplayMessage(denialMessage, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                            Exit Sub
                        End If
                    End If
                End If

                'Display a warning message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days 
                If State.claimNotReportedWithinPeriod_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithinPeriod(State.MyBO.CertificateId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, True)
                        DisplayMessage(denialMessage, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If
            End If

            If State.MyBO.CertificateItem.IsEquipmentRequired Then
                Dim msgList As List(Of String) = New List(Of String)
                Dim flag As Boolean = True
                flag = flag And State.MyBO.ClaimedEquipment.ValidateForClaimProcess(msgList)
                If State.MyBO.EnrolledEquipment IsNot Nothing Then flag = flag And State.MyBO.EnrolledEquipment.ValidateForClaimProcess(msgList)

                If Not flag Then
                    MasterPage.MessageController.AddError(msgList.ToArray)
                    Exit Sub
                End If
            End If

            CreateClaim()
            'End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDedCollContinue_Click(sender As Object, e As System.EventArgs) Handles btnDedCollContinue.Click

        If Not cboDedCollMethod.SelectedIndex > BLANK_ITEM_SELECTED Then
            moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DED_COLL_METHD_REQD)
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            RegisterStartupScript("Startup", x)
            Exit Sub
        Else
            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) AndAlso
                txtDedCollAuthCode.Text.Length <> CInt(Codes.DED_COLL_CR_AUTH_CODE_LEN) Then 'Allow exact length of Auth Code
                moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTH_CODE_FOR_CC)
                Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                RegisterStartupScript("Startup", x)
                Exit Sub
            End If
        End If

        Dim c As Comment
        Dim oldStatus As String = State.MyBO.StatusCode
        Try
            'Populate the Method of Collection to the Claim and revalidate the Claim Object before Save
            State.MyBO.DedCollectionMethodID = GetSelectedItem(cboDedCollMethod)
            If txtDedCollAuthCode.Enabled = True Then
                PopulateBOProperty(State.MyBO, "DedCollectionCCAuthCode", txtDedCollAuthCode)
            End If

            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_DEFFERED_COLL) Then
                State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                c = State.MyBO.AddNewComment()
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                c.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_NOT_COLLECTED)
            End If

            State.MyBO.Validate()
            State.MyBO.Save()
            'Assumption : No Service Orders are created for this Path
            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.MyBO
            NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            State.MyBO.StatusCode = oldStatus
            ' DEF-25037 Throw ex
            HandleErrors(ex, MasterPage.MessageController)
            modalMessageBox.Attributes.Add("style", "display: none")
        End Try

    End Sub

    Protected Sub cboDedCollMethod_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDedCollMethod.SelectedIndexChanged
        Try
            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) Then
                txtDedCollAuthCode.Enabled = True
            Else
                txtDedCollAuthCode.Text = ""
                txtDedCollAuthCode.Enabled = False
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ButtonOverride_Write_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOverride_Write.Click
        Try
            btnCreateClaim_WRITE_Click(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonUpdateClaim_Write_Click(sender As System.Object, e As System.EventArgs) Handles ButtonUpdateClaim_Write.Click
        Try
            btnCreateClaim_WRITE_Click(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonCancel_Write_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel_Write.Click
        Try
            PopulateBOsFromForm()
            NavController.Navigate(Me, FlowEvents.EVENT_CANCEL)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub NotifyAuthorizedAmountHasChanged()
        Try
            PopulateBOProperty(State.MyBO, "AuthorizedAmount", TextboxAuthorizedAmount)

            If curMethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                If State.MyBO.Deductible.Value = 0 Then
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                        If State.MyBO.DeductiblePercent IsNot Nothing Then
                            If State.MyBO.DeductiblePercent.Value > 0 Then
                                State.MyBO.Calculate_deductible_if_by_percentage()
                            End If
                        End If
                    End If
                End If
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                    If State.MyBO.DeductiblePercent IsNot Nothing Then
                        If State.MyBO.DeductiblePercent.Value > 0 Then
                            State.MyBO.Calculate_deductible_if_by_percentage()
                        End If
                    End If
                End If
            End If
            PopulateControlFromBOProperty(TextboxDeductible_WRITE, State.MyBO.Deductible)
            PopulateControlFromBOProperty(TextBoxDiscount, State.MyBO.DiscountAmount)
            PopulateControlFromBOProperty(TextboxAssurantPays, State.MyBO.AssurantPays)
            PopulateControlFromBOProperty(TextboxConsumerPays, State.MyBO.ConsumerPays)
            PopulateControlFromBOProperty(TextboxDueToSCFromAssurant, State.MyBO.DueToSCFromAssurant)
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
            If State.MyBO.IsAuthorizationLimitExceeded Then
                moMessageController.Clear()
                moMessageController.AddWarning(String.Format("{0}: {1}",
                                          TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                          TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaim_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnDenyClaim_Write.Click
        Try
            Dim cert As New Certificate(State.MyBO.CertificateId)
            PopulateBOsFromForm()
            State.MyBO.Validate()
            If State.MyBO.LossDate.Value < cert.WarrantySalesDate.Value Then
                'display error
                ElitaPlusPage.SetLabelError(LabelLossDate)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
            End If

            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.MyBO
            NavController.Navigate(Me, FlowEvents.EVENT_DENY)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnComment_Click(sender As System.Object, e As System.EventArgs) Handles btnComment.Click
        Try
            'START  DEF-2306 Displaying claim_id on elp_comment table
            NavController.Navigate(Me, FlowEvents.EVENT_COMMENTS, New CommentListForm.Parameters(State.MyBO.CertificateId, State.MyBO.Id))
            'END    DEF-2306 Displaying claim_id on elp_comment table
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonCancelClaim_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelClaim.Click
        Try
            PopulateBOsFromForm()
            NavController.Navigate(Me, FlowEvents.EVENT_CANCEL_CLAIM, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_ARE_YOU_SURE))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Button to unlock the claim if it was 
    ''' originally locked by the same user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUnlock_Click(sender As System.Object, e As System.EventArgs) Handles btnUnlock.Click
        Try
            State.MyBO.UnLock()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveClaimIssue(sender As Object, Args As EventArgs) Handles btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            claimIssue.SaveNewIssue(State.MyBO.Id, New Guid(hdnSelectedIssueCode.Value), State.MyBO.CertificateId, False)
            State.ClaimIssuesView = State.MyBO.GetClaimIssuesView()
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    'Protected Sub vtnVerifyEquipment_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vtnVerifyEquipment.Click
    '    Try
    '        'start this process only if the equipment do not match
    '        'update the enrolled equipment from cert item
    '        'verify now the enrolled equipment and claimed equipment match or not
    '        'if they match then show success message and otherwise show error meesage
    '        If Me.State.MyBO.IsEquipmentMisMatch Then
    '            Me.State.MyBO.UpdateEnrolledEquipment()
    '            If Me.State.MyBO.IsEquipmentMisMatch Then
    '                Me.MasterPage.MessageController.AddError(Codes.EQUIPMENT_NOT_FOUND, True)
    '            Else
    '                Me.MasterPage.MessageController.AddMessage(Codes.EQUIPMENT_VERIFIED, True)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region

#Region "Page Control Events"

#End Region

#Region "JavaScript"

    Protected Sub StartNoReplacementClient()
        Dim sJavaScript As String

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "UpdateMainDetailCheck();" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("SendReportError", sJavaScript)
    End Sub

#End Region

#Region "Navigation Control"
    Public Class StateControllerCancelClaim
        Implements IStateController

        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            Dim claimBO As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
            claimBO.Cancel()
            claimBO.IsUpdatedComment = True
            claimBO.Save()
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class
#End Region

#Region "Handle Dropdown Events"

    Private Sub cboCauseOfLossId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCauseOfLossId.SelectedIndexChanged

        'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

        PopulateBOProperty(State.MyBO, "CauseOfLossId", cboCauseOfLossId)
        With State.MyBO
            If Not .CauseOfLossId = Guid.Empty Then
                .ClaimSpecialServiceId = State.MyBO.GetSpecialServiceValue()
            Else
                .ClaimSpecialServiceId = State.noId
            End If

            EnableDisablePriceFieldsforSplSvc()
        End With

    End Sub

#End Region

#Region "REQ-784 : Use Ship Address"
    Private Sub cboUseShipAddress_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboUseShipAddress.SelectedIndexChanged

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cboUseShipAddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True

            'REQ-1153
            If State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
                State.MyBO.AddContactInfo(Nothing)
                State.MyBO.ContactInfo.Address.CountryId = State.MyBO.Company.CountryId
                State.MyBO.ContactInfo.SalutationId = State.MyBO.Company.SalutationId

                UserControlAddress.NewClaimBind(State.MyBO.ContactInfo.Address)
                UserControlContactInfo.NewClaimBind(State.MyBO.ContactInfo)
            Else
                UserControlAddress.ClaimDetailsBind(State.MyBO.ContactInfo.Address)
                UserControlContactInfo.Bind(State.MyBO.ContactInfo)

            End If

        Else
            moUserControlContactInfo.Visible = False

            'REQ-1153
            If State.MyBO.ContactInfo.IsNew Then
                If State.MyBO.ContactInfo IsNot Nothing Then
                    State.MyBO.ContactInfo.Delete()
                End If

                If State.MyBO.ContactInfo.Address IsNot Nothing Then
                    State.MyBO.ContactInfo.Address.Delete()
                End If

                If Not State.MyBO.ContactInfoId = System.Guid.Empty Then
                    State.MyBO.ContactInfoId = System.Guid.Empty
                End If
            End If
        End If
    End Sub


    Private Sub SetContactInfoLabelColor()
        If UserControlContactInfo Is Nothing Then
            Exit Sub
        End If

        Dim lbl As Label
        lbl = CType(UserControlContactInfo.FindControl("moSalutationLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("Label1"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("moContactNameLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("moHomePhoneLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("moEmailAddressLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("moWorkPhoneLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlContactInfo.FindControl("moCellPhoneLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub SetAddressLabelColor()

        If UserControlAddress Is Nothing Then
            Exit Sub
        End If

        Dim lbl As Label
        lbl = CType(UserControlAddress.FindControl("moAddress1Label"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moAddress2Label"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moAddress3Label"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moCityLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moCountryLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moPostalLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
        lbl = CType(UserControlAddress.FindControl("moRegionLabel"), Label)
        If lbl.ForeColor = Color.Red Then
            If Not UserControlMessageController.Controls.Contains(lbl) Then
                lbl.ForeColor = Color.Black
            End If
        End If
    End Sub

#End Region

#Region "Claim Issues Grid related"

    Public Sub PopulateGrid()

        Grid.AutoGenerateColumns = False
        Grid.PageSize = State.PageSize
        'Me.ValidSearchResultCountNew(Me.State.ClaimIssuesView.Count, True)
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
        ''''Dont consider the section below if the claim status is DENIED
        If Not State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED Then
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
        End If
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedClaimIssueId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(1).FindControl("EditButton_WRITE") IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimIssuesView.COL_CLAIM_ISSUE_ID), Byte()))
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = dvRow(Claim.ClaimIssuesView.COL_ISSUE_DESC).ToString
                End If

                ' Convert short status codes to full description with css
                e.Row.Cells(GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(CLAIM_ISSUE_LIST, dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString)
                If (dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__RESOLVED Or
                          dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                    e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.SelectedClaimIssueId = New Guid(e.CommandArgument.ToString())
                    NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM_ISSUE_ID) = State.SelectedClaimIssueId
                    NavController.Navigate(Me, FlowEvents.EVENT_NEXT, New ClaimIssueDetailForm.Parameters(State.MyBO, State.SelectedClaimIssueId))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
           (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
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

#Region "Claim Case Grid Related Functions"

    Public Sub PopulateClaimActionGrid()

        Try
            If (State.ClaimActionListDV Is Nothing) Then
                State.ClaimActionListDV = CaseAction.GetClaimActionList(State.MyBO.Claim_Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            If State.ClaimActionListDV.Count = 0 Then
                lblClaimActionRecordFound.Text = State.ClaimActionListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                ClaimActionGrid.DataSource = State.ClaimActionListDV
                State.ClaimActionListDV.Sort = State.SortExpression
                HighLightSortColumn(ClaimActionGrid, State.SortExpression, IsNewUI)
                ClaimActionGrid.DataBind()
                lblClaimActionRecordFound.Text = State.ClaimActionListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, ClaimActionGrid, True)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateQuestionAnswerGrid()
        Try

            If (State.CaseQuestionAnswerListDV Is Nothing) Then
                State.CaseQuestionAnswerListDV = CaseQuestionAnswer.getClaimCaseQuestionAnswerList(State.MyBO.Claim_Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            lblQuestionRecordFound.Visible = True

            If State.CaseQuestionAnswerListDV.Count = 0 Then
                lblQuestionRecordFound.Text = State.CaseQuestionAnswerListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                CaseQuestionAnswerGrid.DataSource = State.CaseQuestionAnswerListDV
                State.CaseQuestionAnswerListDV.Sort = State.SortExpression
                HighLightSortColumn(CaseQuestionAnswerGrid, State.SortExpression, IsNewUI)
                CaseQuestionAnswerGrid.DataBind()
                lblQuestionRecordFound.Text = State.CaseQuestionAnswerListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, True)

        Catch ex As Exception
            Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, False)
                lblQuestionRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Claim Extended Status Aging Related Grid"

    Private Sub PopulateExtendedStatusAging()
        Dim claimAgingDetailsDV As ClaimAgingDetails.ClaimAgingDetailsDV
        Dim HandleNoRow As Boolean = False
        claimAgingDetailsDV = New ClaimAgingDetails().Load_List(State.MyBO.Claim_Id, Authentication.LangId)
        If claimAgingDetailsDV.Count <= 0 Then
            ClaimAgingDetails.AddNewRowToSearchDV(claimAgingDetailsDV)
            HandleNoRow = True
        End If
        claimAgingDetailsDV.Table.TableName = "ClaimAgingDetails"
        moExtendedStatusAgingRepeater.DataSource = claimAgingDetailsDV
        moExtendedStatusAgingRepeater.DataBind()
        If HandleNoRow Then
            moExtendedStatusAgingRepeater.Items(0).Visible = False
        End If
    End Sub

#End Region

#Region "Claim Image Related Grid"

    Private Sub AddImageButton_Click(sender As Object, e As EventArgs) Handles AddImageButton.Click
        Try
            Dim valid As Boolean = True
            If (DocumentTypeDropDown.SelectedIndex = -1) Then
                MasterPage.MessageController.AddError("DOCUMENT_TYPE_IS_REQUIRED")
                valid = False
            End If

            If (ImageFileUpload.Value Is Nothing) OrElse
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

            State.MyBO.AttachImage(
                New Guid(DocumentTypeDropDown.SelectedValue),
                Nothing,
                DateTime.Now,
                fileName,
                CommentTextBox.Text,
                ElitaPlusIdentity.Current.ActiveUser.UserName,
                fileData)
            If Not State.MyBO.IsNew Then

            End If
            State.ClaimImagesView = Nothing
            ClearForm()
            PopulateClaimImagesGrid()
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            ' Remove Mandatory Fields Validations for Hash, File Type and File Name
            Dim removeProperties As String() = New String() {"FileType", "FileName", "HashValue"}
            Dim newException As BOValidationException =
                New BOValidationException(
                    ex.ValidationErrorList().Where(Function(ve) (Not ((ve.Message = Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR) AndAlso (removeProperties.Contains(ve.PropertyName))))).ToArray(),
                    ex.BusinessObjectName,
                    ex.UniqueId)
            HandleErrors(newException, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub ClearForm()
        PopulateControlFromBOProperty(DocumentTypeDropDown, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER))
        PopulateControlFromBOProperty(ScanDateTextBox, GetLongDateFormattedString(DateTime.Now))
        CommentTextBox.Text = String.Empty
    End Sub
    Private Sub GridClaimImages_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (e.Row.Cells(0).FindControl("btnImageLink") IsNot Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    ' btnLinkImage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte()))

                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), State.MyBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
                End If

                If (dvRow(Claim.ClaimImagesView.COL_STATUS_CODE).ToString = Codes.CLAIM_IMAGE_PROCESSED) Then
                    e.Row.Cells(3).CssClass = "StatActive"
                Else
                    e.Row.Cells(3).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridClaimImages.RowCommand
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
                'lblClaimImage.Text = String.Format("{0}: {1}", TranslationBase.TranslateLabelOrMessage("CLAIM_IMAGE"), e.CommandArgument.ToString())
                'pdfIframe.Attributes(ATTRIB_SRC) = PDF_URL + e.CommandArgument.ToString()
                Dim x As String = "<script language='JavaScript'> revealModal('modalClaimImages') </script>"
                RegisterStartupScript("Startup", x)
            End If
        End If
    End Sub

    Public Sub PopulateClaimImagesGrid()
        If State.ClaimImagesView Is Nothing Then
            State.ClaimImagesView = State.MyBO.GetClaimImagesView
        End If
        'work queue image
        GridClaimImages.DataSource = State.ClaimImagesView
        GridClaimImages.DataBind()
    End Sub

    Private Sub GridClaimImages_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridClaimImages.Sorting
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
            State.ClaimImagesView.Sort = State.SortExpression
            PopulateClaimImagesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GridClaimImages_PageIndexChanged(sender As Object, e As System.EventArgs) Handles GridClaimImages.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedClaimIssueId = Guid.Empty
            PopulateClaimImagesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridClaimImages.PageIndexChanging
        Try
            GridClaimImages.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaimImages.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Protected Sub moExtendedStatusAgingRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        Select Case e.Item.ItemType
            Case ListItemType.Header
                With DirectCast(e.Item.FindControl("lblhdStageName"), Label)
                    .Text = TranslationBase.TranslateLabelOrMessage("STAGE_NAME")
                End With
                With DirectCast(e.Item.FindControl("lblhdAgingStartStatus"), Label)
                    .Text = TranslationBase.TranslateLabelOrMessage("AGING_START_STATUS")
                End With
                With DirectCast(e.Item.FindControl("lblhdAgingEndStatus"), Label)
                    .Text = TranslationBase.TranslateLabelOrMessage("AGING_END_STATUS")
                End With
                With DirectCast(e.Item.FindControl("lblhdAgingStatus"), Label)
                    .Text = TranslationBase.TranslateLabelOrMessage("STATUS_AGING")
                End With
                With DirectCast(e.Item.FindControl("lblhdAgingSinceClaimInception"), Label)
                    .Text = TranslationBase.TranslateLabelOrMessage("AGING_SINCE_CLAIM_INCEPTION")
                End With

            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim extendedstatusDR As DataRow = DirectCast(e.Item.DataItem, System.Data.DataRowView).Row
                ' AgingStartDateTime
                With DirectCast(e.Item.FindControl("lblitmAgingStartDateTime"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingStartDateTime(extendedstatusDR)
                End With
                ' AgingEndDateTime
                With DirectCast(e.Item.FindControl("lblitmAgingEndDateTime"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingEndDateTime(extendedstatusDR)
                End With
                ' AgingStatusDays
                With DirectCast(e.Item.FindControl("lblitmAgingStatusDays"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingDays(extendedstatusDR)
                End With
                ' AgingStatusClaimInceptionDays
                With DirectCast(e.Item.FindControl("lblitmAgingSinceClaimInceptionDays"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingClaimDays(extendedstatusDR)
                End With
                ' AgingStatusHours
                With DirectCast(e.Item.FindControl("lblitmAgingStatusHours"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingHours(extendedstatusDR)
                End With
                ' AgingStatusClaimInceptionHours
                With DirectCast(e.Item.FindControl("lblitmAgingSinceClaimInceptionHours"), Label)
                    .Text = ClaimAgingDetails.ClaimAgingDetailsDV.AgingClaimHours(extendedstatusDR)
                End With
        End Select
    End Sub

End Class

