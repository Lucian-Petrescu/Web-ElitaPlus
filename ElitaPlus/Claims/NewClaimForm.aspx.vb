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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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


#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Claim
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Claim)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
        Public Sub New(ByVal claimBO As Claim, ByVal serviceCenterId As Guid, ByVal certItemCoverageId As Guid, Optional ByVal ClaimMasterNumber As String = Nothing, Optional ByVal DateOfLoss As DateType = Nothing, Optional ByVal DateOfReport As DateType = Nothing, Optional ByVal RecoveryButtonClick As Boolean = False, Optional ByVal ComingFromDenyClaim As Boolean = False, Optional ByVal ComingFromCert As Boolean = False, Optional ByVal CallerName As String = Nothing, Optional ByVal ProbDesc As String = Nothing)
            Me.ClaimBO = claimBO
            Me.ServiceCenterId = serviceCenterId
            Me.CertItemCoverageId = certItemCoverageId
            Me.ClaimMasterNumber = ClaimMasterNumber
            Me.DateOfLoss = DateOfLoss
            Me.DateOfReport = DateOfReport
            Me.RecoveryButtonClick = RecoveryButtonClick
            Me.ComingFromDenyClaim = ComingFromDenyClaim
            Me.CallerName = CallerName
            Me.ProblemDescription = ProbDesc
        End Sub

        Public Sub New(ByVal _ClaimedEquipment As ClaimEquipment)
            Me.claimedEquipment = _ClaimedEquipment
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()

        If Me.NavController.CurrentNavState.Name = "NEW_CLAIM_DETAIL" Then
            Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
            Exit Sub
        End If
        Dim newClaim As Claim = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
        Dim servCenter As ServiceCenter = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)
        Dim coverage As CertItemCoverage = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE), CertItemCoverage)
        Dim strMasterClaimNumber As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_MSTR_CLAIM_NUMB), String)
        Dim objDateOfLoss As DateType = Nothing
        Dim objDateOfReport As DateType = Nothing
        Dim strDateOfLoss As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)
        Dim strDateOfReport As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
        Dim blnComingfromDenyClaim As Boolean = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_COMING_FROM_DENY_CLAIM), Boolean)
        Dim blnRecoveryButtonClick As Boolean = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_RECOVERY_BUTTON_CLICK), Boolean)
        Dim callerName As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME), String)
        Dim problemDescription As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION), String)

        If Not strDateOfLoss Is Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) Then
            objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))
        End If

        If Not strDateOfReport Is Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
            objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
        End If

        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InputParameters = TryCast(Me.NavController.ParametersPassed, Parameters)
            End If

            If Not Me.State.InputParameters Is Nothing Then
                If Not Me.State.InputParameters.ClaimBO Is Nothing Then
                    Me.State.MyBO = Me.State.InputParameters.ClaimBO
                Else
                    If (Not newClaim Is Nothing) Then
                        Me.State.InputParameters = New Parameters(newClaim, Nothing, Nothing)
                    Else
                        Me.State.InputParameters = New Parameters(newClaim, servCenter.Id, coverage.Id, strMasterClaimNumber, objDateOfLoss, objDateOfReport, , , blnComingfromDenyClaim, callerName, problemDescription)
                    End If
                    If Not Me.State.InputParameters.ClaimBO Is Nothing Then
                        'Get the id from the parent
                        Me.State.MyBO = Me.State.InputParameters.ClaimBO
                    End If
                End If

            Else
                If (Not newClaim Is Nothing) Then
                    Me.State.InputParameters = New Parameters(newClaim, Nothing, Nothing)
                Else
                    Me.State.InputParameters = New Parameters(newClaim, servCenter.Id, coverage.Id, strMasterClaimNumber, objDateOfLoss, objDateOfReport, , , blnComingfromDenyClaim, callerName, problemDescription)
                End If
                If Not Me.State.InputParameters.ClaimBO Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = Me.State.InputParameters.ClaimBO
                End If
            End If

            '#REQ 1106 start
            If Not Me.State.InputParameters Is Nothing Then
                Dim param As LocateServiceCenterDetailForm.Parameters = TryCast(Me.NavController.ParametersPassed, LocateServiceCenterDetailForm.Parameters)
                If Not param Is Nothing Then
                    Me.State.InputParameters.claimedEquipment = param.objClaimedEquipment
                End If
            End If
            '#Req 1106 end

            'shippingInfo
            Me.State.ShippingInfoBO = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_SHIPPING_INFO), ShippingInfo)

            'Thunder User story 13 - Task - 199011
            If (Me.State.MyBO.Certificate.Dealer.UseClaimAuthorizationId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)) Then
                If (Not String.IsNullOrWhiteSpace(Me.NavController.PrevNavState.Name)) Then
                    'when the control comes back from Claim Issue Detail form
                    If (Me.NavController.PrevNavState.Name = "CLAIM_ISSUE_DETAIL") Then
                        If (Not Me.State.MyBO.Id.Equals(Guid.Empty)) Then
                            Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                            If (Not Me.State.MyBO Is Nothing) Then
                                If (Me.State.MyBO.Status = BasicClaimStatus.Active OrElse Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
                                    If (Me.NavController.CurrentFlow.Name = "AUTHORIZE_PENDING_CLAIM") Then
                                        NavController.Navigate(Me, "claim_issue_approved_from_claim", New ClaimForm.Parameters(State.MyBO.Id))
                                    ElseIf (Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE") Then
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            Me.State.InputParameters = CType(CallingPar, Parameters)
            If Not Me.State.InputParameters.ClaimBO Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = Me.State.InputParameters.ClaimBO
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum(ByVal state As String)

        Select Case state
            Case "CREATE_CLAIM_FROM_CERTIFICATE"
                If (Not Me.State Is Nothing) Then
                    If (Not Me.State.MyBO Is Nothing) Then
                        Dim pageName As String = "File New Claim"
                        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & "Certificate " & Me.State.MyBO.CertificateNumber & ElitaBase.Sperator & pageName
                    End If
                End If

            Case "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM DETAILS") & ElitaBase.Sperator & Me.MasterPage.PageTab
            Case "AUTHORIZE_PENDING_CLAIM", "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH", "AUTHORIZE_AGENT_PENDING_CLAIM"
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab
        End Select

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            If Me.NavController.CurrentNavState.Name <> "CREATE_NEW_CLAIM" Then
                If Me.NavController.CurrentNavState.Name <> "NEW_CLAIM_DETAIL" Then
                    Return
                End If
            End If

            SetDecimalSeparatorSymbol()

            If Not Me.IsPostBack Then
                'Date Calendars

                Me.AddCalendar_New(Me.ImageButtonLossDate, Me.TextboxLossDate)
                Me.AddCalendar_New(Me.ImageButtonReportDate, Me.TextboxReportDate)
                Me.AddCalendar_New(Me.ImageButtonRepairDate, Me.TextboxRepairdate)
                Me.AddCalendar_New(Me.ImageButtonPickUpDate, Me.TextboxPickUpDate)
                '   Me.MenuEnabled = False

                CheckIfComingFromRecoveryButtonClick()

                If Me.State.MyBO Is Nothing Then

                    Me.State.MyBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
                    With Me.State.InputParameters
                        If .ComingFromDenyClaim = True Then
                            Me.State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss, , , .ComingFromDenyClaim)
                        Else
                            If Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE" Then
                                Me.State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss, , , , True, .CallerName, .ProblemDescription, Me.State.InputParameters.claimedEquipment)

                            Else
                                Me.State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimMasterNumber, .DateOfLoss)
                            End If

                        End If

                    End With
                End If

                Trace(Me, "Claim Id=" & GuidControl.GuidToHexString(Me.State.MyBO.Id))

                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")

                Me.State.yesId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
                Me.State.noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")

                'REQ-918  Populate the Make,Model,Serial Number, SKU and List Price from the current item 
                'of the Certificate to the Claim Equipment
                If (Me.State.mDealer Is Nothing) Then
                    Me.State.mDealer = New Dealer(Me.State.MyBO.CompanyId, Me.State.MyBO.DealerCode)
                End If

                Me.State.ClaimIssuesView = Me.State.MyBO.GetClaimIssuesView()
                PopulateDropdowns()
                PopulateServiceCenterSelected()

                'Check if Dealer uses Equipment
                With Me.ReplacementOption
                    .ClaimBO = Me.State.MyBO
                    .thisPage = Me
                    If (Me.State.MyBO.Dealer.UseEquipmentId.Equals(Me.State.yesId)) Then
                        .Visible = True
                        Me.dvClaimEquipment.Visible = True
                        PopulateClaimEquipment()
                    Else
                        .Visible = False
                        Me.dvClaimEquipment.Visible = False
                    End If
                End With
                'REQ-861 


                ddlEnrolledManuf.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", ddlEnrolledManuf.ClientID, txtEnrolledModel.ClientID, ddlEnrolledEquipSKU.ClientID, hdnSelectedEnrolledSku.ClientID))
                txtEnrolledModel.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", ddlEnrolledManuf.ClientID, txtEnrolledModel.ClientID, ddlEnrolledEquipSKU.ClientID, hdnSelectedEnrolledSku.ClientID))
                ddlClaimedManuf.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", ddlClaimedManuf.ClientID, txtClaimedModel.ClientID, ddlClaimedEquipSKU.ClientID, hdnSelectedEnrolledSku.ClientID))
                txtClaimedModel.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", ddlClaimedManuf.ClientID, txtClaimedModel.ClientID, ddlClaimedEquipSKU.ClientID, hdnSelectedEnrolledSku.ClientID))

                ddlEnrolledEquipSKU.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", ddlEnrolledEquipSKU.ClientID, hdnSelectedEnrolledSku.ClientID))
                ddlClaimedEquipSKU.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", ddlClaimedEquipSKU.ClientID, hdnSelectedClaimedSku.ClientID))

                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                TranslateGridHeader(Grid)


                'Populate Extended Status Aging Grid
                PopulateExtendedStatusAging()

                'Work Queue
                TranslateGridHeader(GridClaimImages)
                Me.State.ClaimImagesView = Me.State.MyBO.GetClaimImagesView()
                PopulateClaimImagesGrid()
                ' Me.BindListControlToDataView(Me.DocumentTypeDropDown, LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                Dim documentType As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                DocumentTypeDropDown.Populate(documentType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False
                                               })
                Me.ClearForm()

                Me.PopulateFormFromBOs()

                Me.MasterPage.MessageController.Clear()

                CheckBoxCarryInPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxCarryInPrice.ClientID))
                CheckBoxHomePrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxHomePrice.ClientID))
                CheckBoxCleaningPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxCleaningPrice.ClientID))
                CheckBoxEstimatePrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxEstimatePrice.ClientID))
                CheckBoxOtherPrice.Attributes.Add("onclick", String.Format("UpdateDetailCheck(this,{0});", TextBoxOtherPrice.ClientID))
                btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", ButtonCancel_Write.UniqueID))

                nDIscountPercent = Me.State.MyBO.DiscountPercent
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                    Me.State.isDeductibleByPercent = True
                    isODeductibleByPercent = True
                    Me.State.oDeductiblePercent = Me.State.MyBO.DeductiblePercent
                    nDeductiblePercent = Me.State.MyBO.DeductiblePercent.Value
                End If
                MethodOfRepairCode = Me.State.MyBO.MethodOfRepairCode

                ''check set auth amount by sku
                'State.isAuthAmtSetBySKU = Me.setAuthAmtBySKU(State.authAmtBySku)
                'If State.isAuthAmtSetBySKU Then
                '    Me.TextboxAuthorizedAmount.Text = State.authAmtBySku.ToString
                'End If

                Me.InitialEnableDisableFields()
                DisableButtonsForClaimSystem()
                If Me.TextboxCallerName.Text = "" Then SetFocus(Me.TextboxCallerName)

                ClaimLogisticalInfo.claimId = Me.State.MyBO.Id
                ClaimLogisticalInfo.PopulateGrid()


                Me.TranslateGridHeader(Me.CaseQuestionAnswerGrid)
                Me.TranslateGridHeader(Me.ClaimActionGrid)
                Me.PopulateQuestionAnswerGrid()
                Me.PopulateClaimActionGrid()


            Else
                If Not Me.HiddenCallerTaxNumber.Value = "" Then Me.TextboxCALLER_TAX_NUMBER.Enabled = CType(Me.HiddenCallerTaxNumber.Value, Boolean)

                isODeductibleByPercent = Me.State.isDeductibleByPercent

                If (Me.State.oDeductiblePercent Is Nothing) Then
                    nDeductiblePercent = 0D
                Else
                    nDeductiblePercent = Me.State.oDeductiblePercent.Value
                End If
            End If

            BindBoPropertiesToLabels()
            If (Me.CheckIfComingFromCreateClaimConfirm()) Then
                Exit Sub
            End If
            GetDepreciationSchedule()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
                If Not Me.State.MyBO.EnrolledEquipment Is Nothing Then
                    MyBase.AddLabelDecorations(Me.State.MyBO.EnrolledEquipment)
                End If
                If Not Me.State.MyBO.ClaimedEquipment Is Nothing Then
                    MyBase.AddLabelDecorations(Me.State.MyBO.ClaimedEquipment)
                End If
                If Not Me.LabelReplacementCost.Text.EndsWith(":") Then
                    Me.LabelReplacementCost.Text = Me.LabelReplacementCost.Text & ":"
                End If
                ''REQ-784
                If Not Me.LabelUseShipAddress.Text.EndsWith(":") Then
                    Me.LabelUseShipAddress.Text = Me.LabelUseShipAddress.Text & ":"
                End If

                If Not Me.LabelOutstandingPremAmt.Text.EndsWith(":") Then
                    Me.LabelOutstandingPremAmt.Text = Me.LabelOutstandingPremAmt.Text & ":"
                End If

                'REQ-5467
                If (Me.IsNewUI) Then
                    If Me.State.MyBO.Dealer.IsLawsuitMandatoryId = Me.State.yesId Then
                        LabelIsLawsuitId.Text = "<span class=""mandatory"">*</span> " & LabelIsLawsuitId.Text
                    End If
                End If

            End If


            'Set dummy fields to value of real field
            TextboxAuthorizedAmountShadow.Text = TextboxAuthorizedAmount.Text
            If Me.IsPostBack Then
                TextboxDeductible_WRITE.Text = TextboxDeductibleShadow.Text
                TextBoxDiscount.Text = TextBoxDiscountShadow.Text
                TextboxAssurantPays.Text = TextboxAssurantPaysShadow.Text
                TextboxConsumerPays.Text = TextboxConsumerPaysShadow.Text
                TextboxDueToSCFromAssurant.Text = TextboxDueToSCFromAssurantShadow.Text
                TextBoxOtherPrice.Text = TextBoxOtherPriceShadow.Text
                TextboxLiabilityLimit.Text = TextboxLiabilityLimitShadow.Text

                Dim myContractId As Guid = Contract.GetContractID(Me.State.MyBO.CertificateId)
                curContractId = GuidControl.GuidToHexString(myContractId)
                curCertId = GuidControl.GuidToHexString(Me.State.MyBO.CertificateId)
                curCertItemCoverageId = GuidControl.GuidToHexString(Me.State.MyBO.CertItemCoverageId)
                curMethodOfRepairCode = Me.State.MyBO.MethodOfRepairCode

            Else
                TextboxDeductibleShadow.Text = TextboxDeductible_WRITE.Text
                TextBoxDiscountShadow.Text = Me.TextBoxDiscount.Text
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

                Me.State.original_Deductible = TextboxDeductible_WRITE.Text
                Me.State.original_Discount = Me.TextBoxDiscount.Text
                Me.State.original_AssurantPays = TextboxAssurantPays.Text
                Me.State.original_ConsumerPays = TextboxConsumerPays.Text
                Me.State.original_DueToSCFromAssurant = TextboxDueToSCFromAssurant.Text
                Me.State.original_OtherPrice = TextBoxOtherPrice.Text
                Me.State.original_LiabilityLimit = TextboxLiabilityLimit.Text

                If TextBoxReplacementCost.Visible = True Then
                    Me.State.original_AuthorizedAmount = TextBoxReplacementCost.Text
                    Me.State.isMethodPricePanel_Visible = False
                Else
                    Me.State.original_AuthorizedAmount = TextboxAuthorizedAmount.Text
                    Me.State.isMethodPricePanel_Visible = True
                End If
                Me.State.prev_MethodOfRepairCode = Me.State.MyBO.MethodOfRepairCode
                Me.State.prev_ClaimActivityCode = Me.State.MyBO.ClaimActivityCode
            End If

            'Def 1859 start
            If Not Me.IsPostBack Then
                If Me.State.MyBO.GetSpecialServiceValue = Me.State.yesId Then
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

                If Me.State.MyBO.LockedBy = ElitaPlusIdentity.Current.ActiveUser.Id Then
                    btnUnlock.Visible = True
                    'ElseIf ElitaPlusIdentity.Current.ActiveUser Then
                End If
            End If
            'def 1859 end

            If Not Me.IsPostBack Then

                Dim objDateOfLoss As DateType = Nothing
                Dim strDateOfLoss As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)
                Dim objDateOfReport As DateType = Nothing
                Dim strDateOfReport As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)

                If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified AndAlso Me.State.MyBO.IsNew Then
                    'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                    If Not strDateOfReport Is Nothing AndAlso Not strDateOfReport.Equals(String.Empty) AndAlso Not strDateOfLoss Is Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) AndAlso
                         Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then

                        objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                        objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))


                        If Not State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, False)
                            Me.MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.coverageTypeforclaimMissingfromCertificate_MessageDisplayed = True
                            'Set claim status to Denied
                            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        If Not State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, False)
                            Me.MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.claimNotReportedWithinGracePeriod_MessageDisplayed = True
                            'Set claim status to Denied
                            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        If (State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value)) And
                         (State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, objDateOfLoss.Value, objDateOfReport.Value)) Then
                            'Display warning Message if the Maximum number of Replacements have been exceeded
                            If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                                Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                                Me.MasterPage.MessageController.AddWarning(denialMessage)
                                'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                                State.maxReplacementExceed_MessageDisplayed = True
                                'Set claim status to Denied
                                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                            End If
                        End If

                    End If
                Else
                    If Not State.MyBO.LossDate Is Nothing AndAlso Not State.MyBO.ReportedDate Is Nothing AndAlso
                         Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                        'Display warning Message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days 
                        If Not State.MyBO.IsClaimReportedWithinPeriod(State.MyBO.CertificateId, State.MyBO.LossDate.Value, State.MyBO.ReportedDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, False)
                            Me.MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.claimNotReportedWithinPeriod_MessageDisplayed = True
                            'Set claim status to Denied
                            'Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If

                        'Display warning Message if the Maximum number of Replacements have been exceeded
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                            Me.MasterPage.MessageController.AddWarning(denialMessage)
                            'Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , False)
                            State.maxReplacementExceed_MessageDisplayed = True
                            'Set claim status to Denied
                            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        End If
                    End If
                End If

            End If

            PopulateGrid()

            'REQ-860
            Select Case Me.NavController.CurrentFlow.Name
                Case "CREATE_CLAIM_FROM_CERTIFICATE"
                    Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificate")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    FillWizard(Me.NavController.CurrentFlow.Name)
                    Me.PopulateProtectionAndEventDetail()
                Case "AUTHORIZE_PENDING_CLAIM", "WORK_ON_QUEUE", "AUTHORIZE_PENDING_CLAIM_FROM_PENDING_SEARCH", "AUTHORIZE_AGENT_PENDING_CLAIM"
                    Me.MasterPage.PageTab = String.Format("{0} {1}", TranslationBase.TranslateLabelOrMessage("PENDING"), TranslationBase.TranslateLabelOrMessage("CLAIM"))
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    Me.PopulateProtectionAndEventDetail()
                Case "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"
                    Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("NEW_CLAIM")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
                    Me.PopulateProtectionAndEventDetail()

            End Select
            'REQ 860
            If Not Me.IsPostBack Then
                IsMaxSvcWrtyClaimsReached()  'developed for Req-5921 - Google OOW 
            End If

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.UpdateBreadCrum(Me.NavController.CurrentFlow.Name)


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ' Clean Popup Input
            CleanPopupInput()
            'Me.State.LastState = InternalStates.Regular
            'Me.HiddenSaveChangesPromptResponse.Value = ""
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property UserCtrPoliceReport() As UserControlPoliceReport_New
        Get
            If moUserControlPoliceReport Is Nothing Then
                moUserControlPoliceReport = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("mcUserControlPoliceReport"), UserControlPoliceReport_New)
            End If
            Return moUserControlPoliceReport
        End Get
    End Property

    '' REQ-784
    Public ReadOnly Property UserControlAddress() As UserControlAddress_New
        Get
            If moUserControlAddress Is Nothing Then
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    '' REQ-784
    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
            End If
            Return moUserControlContactInfo
        End Get
    End Property

    Public ReadOnly Property UserControlMessageController() As MessageController
        Get
            If MessageController Is Nothing Then
                MessageController = DirectCast(Me.MasterPage.MessageController, MessageController)
            End If
            Return MessageController
        End Get
    End Property


#End Region

#Region "Controlling Logic"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function CalculateLiability(ByVal lossDate As String, ByVal myCertId As String,
        ByVal myContractId As String, ByVal myCertItemCoverageId As String, ByVal myMethodOfRepairCode As String) As String

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
        If Not Me.State.MyBO.CertificateId.Equals(Guid.Empty) Then
            Dim oCert As New Certificate(Me.State.MyBO.CertificateId)
            Dim oDealer As New Dealer(oCert.DealerId)
            Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
            Dim bAllowSvcWarrantyClaims As Boolean = False
            ' Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)

            If oClmSystem.Code = "PORTAL" Then
                bAllowSvcWarrantyClaims = True
            End If

            If (oClmSystem.NewClaimId.Equals(Me.State.noId) AndAlso bAllowSvcWarrantyClaims = False) Or Me.State.MyBO.IsClaimChild = Codes.YESNO_Y Then

                MyBase.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_UPDATE_NOT_ALLOWED, True)

                If Me.btnCreateClaim_WRITE.Visible And Me.btnCreateClaim_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnCreateClaim_WRITE, False)
                End If
                If Me.ButtonOverride_Write.Visible And Me.ButtonOverride_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.ButtonOverride_Write, False)
                End If
                If Me.ButtonUpdateClaim_Write.Visible And Me.ButtonUpdateClaim_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.ButtonUpdateClaim_Write, False)
                End If
                If Me.ButtonCancel_Write.Visible And Me.ButtonCancel_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.ButtonCancel_Write, False)
                End If
                If Me.btnCancelClaim.Visible And Me.btnCancelClaim.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnCancelClaim, False)
                End If
                If Me.btnComment.Visible And Me.btnComment.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnComment, False)
                End If
                If Me.btnDenyClaim_Write.Visible And Me.btnDenyClaim_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnDenyClaim_Write, False)
                End If
            End If
        End If
    End Sub
    Protected Sub InitialEnableDisableFields()
        'read only fields
        'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        Me.ChangeEnabledProperty(Me.TextboxCertificateNumber, False)
        Me.ChangeEnabledProperty(Me.TextboxClaimNumber, False)
        Me.ChangeEnabledProperty(Me.TextboxServiceCenter, False)
        Me.ChangeEnabledProperty(Me.TextboxLiabilityLimit, False)
        Me.ChangeEnabledProperty(Me.TextboxDeductible_WRITE, True)
        Me.ChangeEnabledProperty(Me.TextBoxDiscount, False)
        Me.ChangeEnabledProperty(Me.TextboxAssurantPays, False)
        Me.ChangeEnabledProperty(Me.TextboxConsumerPays, False)
        Me.ChangeEnabledProperty(Me.TextboxDueToSCFromAssurant, False)
        Me.ChangeEnabledProperty(Me.CheckBoxLoanerTaken, False)
        Me.ChangeEnabledProperty(Me.TextBoxCarryInPrice, False)
        Me.ChangeEnabledProperty(Me.TextBoxHomePrice, False)
        ' Authorized needs to be disabled. In .NET 2.0 a readOnly field can not be modified on the client
        ' A dummy field was created to show the user the value, while hiding the true field which
        ' remains enabled but invisible
        'JLR==> Me.ChangeEnabledProperty(Me.TextboxAuthorizedAmount, False)
        ' Comment the next line for troubleshooting (to see the field on the screen)
        If Not (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AUTOMOTIVE _
        Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__GENERAL Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__LEGAL Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY) Then
            Me.TextboxAuthorizedAmount.Style.Add(HtmlTextWriterStyle.Display, "none") ' Set true field to invisible
        Else
            Me.TextboxAuthorizedAmountShadow.Style.Add(HtmlTextWriterStyle.Display, "none") ' Set Shadow field to invisible
        End If


        'invisible by default
        ControlMgr.SetVisibleControl(Me, Me.ButtonCancel_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancelClaim, False)
        ControlMgr.SetVisibleControl(Me, Me.ButtonOverride_Write, False)
        ControlMgr.SetVisibleControl(Me, Me.ButtonUpdateClaim_Write, False)

        '  Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, False)
        Me.SetEnabledForControlFamily(Me.PanelMethodPrice, False)

        If Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE" Then
            ControlMgr.SetVisibleControl(Me, Me.ButtonCancel_Write, True)
        End If

        If Me.NavController.CurrentFlow.Name = "WORK_ON_QUEUE" Then
            ControlMgr.SetVisibleControl(Me, Me.btnBack, False)
        End If

        If Not Me.State.MyBO.IsNew Then
            ' Pending Claim
            ControlMgr.SetVisibleControl(Me, Me.btnCancelClaim, True)
            ControlMgr.SetVisibleControl(Me, Me.ButtonCancel_Write, False)
            ControlMgr.SetVisibleControl(Me, Me.ButtonUpdateClaim_Write, True)
            ControlMgr.SetVisibleControl(Me, Me.btnCreateClaim_WRITE, False)
        End If

        If Not Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING Then
            ControlMgr.SetVisibleControl(Me, btnComment, False)
        End If

        'Make Invisible for Service Warranty
        If Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            'Service Warranty Claims
            ControlMgr.SetVisibleControl(Me, Me.TextboxLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxDeductible_WRITE, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxDiscount, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxAssurantPays, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxConsumerPays, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxDueToSCFromAssurant, False)

            ControlMgr.SetVisibleControl(Me, Me.LabelLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelDeductible, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelDiscount, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelAssurantPays, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelConsumerPays, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelDueToSCFromAssurant, False)
            If Not Me.State.MyBO.IsNew Then
                ' Pending Claim
                If Me.State.MyBO.IsDaysLimitExceeded Then
                    If Not (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                        ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                        ' No (Claim Manager or IHQ Support), It is not authorized to update
                        ControlMgr.SetEnableControl(Me, Me.ButtonUpdateClaim_Write, False)
                    End If
                End If
            End If

        End If

        'Loaner
        If (Me.State.MyBO.ClaimActivityCode Is Nothing OrElse Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT) _
            AndAlso Not Me.State.MyBO.ServiceCenterObject.LoanerCenterId.Equals(Guid.Empty) Then
            Me.ChangeEnabledProperty(Me.CheckBoxLoanerTaken, True)
        End If

        If Not Me.State.MyBO.LossDate Is Nothing Then  'Me.State.MyBO.IsFirstClaimRecordFortheIncident Then
            'When editing a claim record created from an existing one
            Me.ChangeEnabledProperty(Me.TextboxLossDate, False)
            ControlMgr.SetVisibleControl(Me, Me.ImageButtonLossDate, False)
            'Me.ChangeEnabledProperty(Me.cboCauseOfLossId, False)
        End If

        If Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            Me.ChangeEnabledProperty(Me.TextboxAuthorizedAmount, True)
            ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)
            'ElseIf State.isAuthAmtSetBySKU Then
            '    ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)
        Else
            Select Case Me.State.MyBO.MethodOfRepairCode
                Case Codes.METHOD_OF_REPAIR__AUTOMOTIVE, Codes.METHOD_OF_REPAIR__GENERAL, Codes.METHOD_OF_REPAIR__LEGAL, Codes.METHOD_OF_REPAIR__RECOVERY

                    Me.ChangeEnabledProperty(Me.TextboxAuthorizedAmount, True)
                    ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)


                Case Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__PICK_UP, Codes.METHOD_OF_REPAIR__SEND_IN
                    'This a repair Claim 
                    'Me.SetEnabledForControlFamily(Me.PanelMethodOfRepair, True)
                    ControlMgr.SetVisibleControl(Me, Me.PanelRepair, True)
                    Me.ChangeEnabledProperty(Me.CheckBoxEstimatePrice, True)
                    Me.ChangeEnabledProperty(Me.CheckBoxOtherPrice, True)
                    Me.ChangeEnabledProperty(Me.CheckBoxCleaningPrice, True)
                    Me.ChangeEnabledProperty(Me.CheckBoxCarryInPrice(), True)
                    Me.ChangeEnabledProperty(Me.CheckBoxHomePrice, True)
                    If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
                        ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, False)
                        ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, False)
                        ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, False)

                        ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, True)
                        ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, True)
                        ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, False)
                        ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, False)
                        ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, False)

                        ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, True)
                        ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, True)
                        ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, True)

                    End If

                    If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then
                        LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("SEND_IN_PRICE")
                    ElseIf State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then
                        LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("PICK_UP_PRICE")
                    End If
            End Select
        End If


        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelPolicyNumber, False)
        End If

        InitialEnableDisableReplacement()

        Dim objCompany As New Company(Me.State.MyBO.CompanyId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_SERVICES Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelConditionalReqquired, False)
        Else
            ControlMgr.SetVisibleControl(Me, Me.TextboxCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelConditionalReqquired, True)
            Me.TextboxCallerName.Attributes.Add("onchange", "ClearCallerTaxNumber();")
            If Me.State.MyBO.CallerTaxNumber Is Nothing Or (Not Me.State.MyBO.CallerTaxNumber Is Nothing AndAlso Me.State.MyBO.CallerTaxNumber.Equals(String.Empty)) Then
                ControlMgr.SetEnableControl(Me, Me.TextboxCALLER_TAX_NUMBER, True)
            Else
                ControlMgr.SetEnableControl(Me, Me.TextboxCALLER_TAX_NUMBER, False)
            End If
            Me.HiddenCallerTaxNumber.Value = Me.TextboxCALLER_TAX_NUMBER.Enabled.ToString
        End If

        ' initially disable them which are needed only for backend claim or deny claim
        ControlMgr.SetVisibleControl(Me, Me.LabelRepairDate, False)
        ControlMgr.SetVisibleControl(Me, Me.TextboxRepairdate, False)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonRepairDate, False)
        ControlMgr.SetVisibleControl(Me, Me.LabelPickUpDate, False)
        ControlMgr.SetVisibleControl(Me, Me.TextboxPickUpDate, False)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickUpDate, False)

        ControlMgr.SetVisibleControl(Me, Me.TextboxInvoiceNumber, True)
        ControlMgr.SetVisibleControl(Me, Me.LabelInvoiceNumber, True)

        ControlMgr.SetVisibleControl(Me, Me.btnDenyClaim_Write, False)

        Dim certificateBO As New Certificate(Me.State.MyBO.CertificateId)

        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.MyBO.CoverageTypeId, certificateBO.DealerId, Authentication.LangId, certificateBO.ProductCode, False) Then
            Me.ChangeEnabledProperty(Me.cboCauseOfLossId, True)
        End If
        If Me.State.MyBO.ClaimSpecialServiceId = Me.State.yesId Then
            EnableDisablePriceFieldsforSplSvc()
        End If
        Me.EnableDisableFields()

    End Sub

    Private Sub InitialEnableDisableReplacement()
        ControlMgr.SetVisibleControl(Me, Me.lblNewDeviceSKU, False)
        ControlMgr.SetVisibleControl(Me, Me.txtNewDeviceSKU, False)
        If Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
            ' It is a Replacement
            '    Me.TextboxAuthorizedAmount.
            '  ControlMgr.SetVisibleControl(Me, Me.TextboxAuthorizedAmount, False)
            Me.TextboxAuthorizedAmountShadow.Style.Add(HtmlTextWriterStyle.Display, "none") ' Visible = False
            Me.TextboxAuthorizedAmount.ReadOnly = False
            ControlMgr.SetVisibleControl(Me, Me.LabelAuthorizedAmount, False)
            ' Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, True)
            ' Me.ChangeEnabledProperty(Me.CheckBoxReplacement, True)
            Me.ChangeEnabledProperty(Me.TextBoxReplacementCost, True)
            ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)

            ' Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.MethodOfRepairId)
            Me.State.DEDUCTIBLE_BASED_ON = oDeductible.DeductibleBasedOn
            'req 1157 added additional condition
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.mDealer.NewDeviceSkuRequiredId) = Codes.YESNO_Y Or (Me.State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                ControlMgr.SetVisibleControl(Me, Me.lblNewDeviceSKU, True)
                ControlMgr.SetVisibleControl(Me, Me.txtNewDeviceSKU, True)
            End If
        Else
            'Make Replacement Price Invisible
            '  Me.ChangeEnabledProperty(Me.CheckboxMethodOfReplacement, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelReplacementCost, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxReplacementCost, False)
            StartNoReplacementClient()
        End If

    End Sub


    Private Sub IsMaxSvcWrtyClaimsReached()
        '''''check if max service warranty count reached (developed for Google OOW project Req-5921)
        If Me.State.MyBO.IsMaxSvcWrtyClaimsReached Then
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.MessageController.AddErrorAndShow("MAX_NUM_SVC_WRTY_CLAIMS_REACHED", True)
            ControlMgr.SetEnableControl(Me, Me.btnCreateClaim_WRITE, False)
        End If
    End Sub

    Protected Sub EnableDisableFields()
        Dim oContractId As Guid
        oContractId = Contract.GetContractID(Me.State.MyBO.CertificateId)
        Dim ContractBO As New Contract(oContractId)

        If Me.State.isSalutation Then
            ControlMgr.SetVisibleControl(Me, Me.cboContactSalutationId, True)
            ControlMgr.SetVisibleControl(Me, Me.cboCallerSalutationId, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.cboContactSalutationId, False)
            ControlMgr.SetVisibleControl(Me, Me.cboCallerSalutationId, False)
        End If

        If Me.State.MyBO.ClaimNumber Is Nothing Then
            ControlMgr.SetVisibleControl(Me, Me.LabelClaimNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxClaimNumber, False)
        End If

        If Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ' show the dates when its NOT service warranty
            ControlMgr.SetVisibleControl(Me, Me.btnDenyClaim_Write, True)
            If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME _
                Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN _
                Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                ' show the controls only when its method of repair is at home or carry in or replacement


                ' when backend claim, display repair date and pick up date else hide them

                If ContractBO.BackEndClaimsAllowedId.Equals(Me.State.yesId) Then
                    ControlMgr.SetVisibleControl(Me, Me.LabelRepairDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxRepairdate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonRepairDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelPickUpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxPickUpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickUpDate, True)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.LabelRepairDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxRepairdate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonRepairDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelPickUpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxPickUpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickUpDate, False)
                End If
            End If
        End If

        If Me.State.MyBO.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then
            Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
        Else
            Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
        End If

        If Me.State.MyBO.IsDaysLimitExceeded Then
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.MessageController.AddWarning("DAYS_LIMIT_EXCEEDED")
        End If

        If Me.State.MyBO.IsAuthorizationLimitExceeded Then
            Me.moMessageController.Clear()
            Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                          TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                          TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If
        HiddenUserAuthorization.Value = Me.State.MyBO.AuthorizationLimit.ToString

        If Me.State.InputParameters.ComingFromDenyClaim = True Then
            btnDenyClaim_Write.Text = CONTINUE_DENY_CLAIM
            ControlMgr.SetEnableControl(Me, Me.btnCreateClaim_WRITE, False)
        Else
            ControlMgr.SetEnableControl(Me, Me.btnCreateClaim_WRITE, True)
        End If

        ''req-784 (DEF-1607)
        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, Me.LabelUseShipAddress, True)
            ControlMgr.SetVisibleControl(Me, Me.cboUseShipAddress, True)
        End If

        If ContractBO.PayOutstandingPremiumId.Equals(Me.State.yesId) Then
            Me.State.PayOutstandingPremium = True
            ControlMgr.SetVisibleControl(Me, Me.LabelOutstandingPremAmt, True)
            ControlMgr.SetVisibleControl(Me, Me.TextboxOutstandingPremAmt, True)
        Else
            Me.State.PayOutstandingPremium = False
            ControlMgr.SetVisibleControl(Me, Me.LabelOutstandingPremAmt, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxOutstandingPremAmt, False)
        End If

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
    Public Sub EnableDisableMethodOfPriceFields(ByVal priceType As String, ByVal pricegrpCode As String)

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
        Dim MasterPagectl As Control = Me.MasterPage.FindControl("BodyPlaceHolder")
        ControlMgr.SetVisibleControl(Me, Me.PanelRepair, True)
        PopulatePrices()
        UncheckAllMethodPriceCheckBoxes()

        For i = 0 To ctlList.Count - 1
            If priceType = ctlList.Item(i).ToString Then
                ctlCheckBox = CType(MasterPagectl.FindControl("CheckBox" + ctlList.Item(i).ToString), CheckBox)
                ctlTextBox = CType(MasterPagectl.FindControl("TextBox" + ctlList.Item(i).ToString), TextBox)
                ctlTextBoxChanged = ctlTextBox
                ctlCheckBox.Checked = True
                Me.TextboxAuthorizedAmount.Text = ctlTextBox.Text
                ControlMgr.SetVisibleControl(Me, TextboxAuthorizedAmount, True)
                ControlMgr.SetVisibleControl(Me, ctlCheckBox, True)
                ControlMgr.SetVisibleControl(Me, ctlTextBox, True)
                Me.ChangeEnabledProperty(ctlTextBox, False)
                Me.ChangeEnabledProperty(ctlCheckBox, False)
                If priceType = PRICE_FIELD_OTHER_PRICE Then
                    Me.ChangeEnabledProperty(ctlCheckBox, True)
                    Me.ChangeEnabledProperty(ctlTextBox, True)
                    ctlTextBox.ReadOnly = False
                End If
            Else
                ctlCheckBox = CType(MasterPagectl.FindControl("CheckBox" + ctlList.Item(i).ToString), CheckBox)
                ctlTextBox = CType(MasterPagectl.FindControl("TextBox" + ctlList.Item(i).ToString), TextBox)
                ControlMgr.SetVisibleControl(Me, ctlCheckBox, True)
                ControlMgr.SetVisibleControl(Me, ctlTextBox, True)
                Me.ChangeEnabledProperty(ctlTextBox, False)
                Me.ChangeEnabledProperty(ctlCheckBox, False)
                ctlCheckBox.Checked = False
            End If
        Next

        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            TextBoxReplacementCost.Text = Me.TextboxAuthorizedAmount.Text
            TextboxAuthorizedAmountShadow.Text = Me.TextboxAuthorizedAmount.Text
        Else
            TextboxAuthorizedAmountShadow.Text = Me.TextboxAuthorizedAmount.Text
        End If

        If priceType = PRICE_FIELD_HOME_PRICE Then
            ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, False)

            ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, True)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
        Else
            ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, False)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, False)

            ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
        End If

        Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + ctlTextBoxChanged.ID + "') </script>"
        Me.RegisterStartupScript("Startup", x)
        'Pratap Def 2061
        'Dim x As String = "<script language='JavaScript'> UpdateDetailCheck('" + ctlTextBoxChanged.ID + "') </script>"
        'Me.RegisterStartupScript("Startup", x)

    End Sub

    Protected Sub EnableDisablePriceFieldsforSplSvc()
        Dim splSvcPriceGrp As String
        Dim nOtherPrice As New DecimalType(0)
        With Me.State.MyBO
            If .ClaimSpecialServiceId = Me.State.yesId Then
                Me.State.SplSvc_value = True
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
                If Me.State.prev_SplSvc_value Then
                    Me.State.SplSvc_value = False
                    AssignOriginalValuestoPriceFields()
                End If
            End If
        End With
        Me.State.prev_SplSvc_value = Me.State.SplSvc_value
    End Sub
    Sub AssignOriginalValuestoPriceFields()

        With Me.State
            Me.TextboxDeductibleShadow.Text = .original_Deductible
            Me.TextBoxDiscountShadow.Text = .original_Discount
            Me.TextboxAssurantPaysShadow.Text = .original_AssurantPays
            Me.TextboxConsumerPaysShadow.Text = .original_ConsumerPays
            Me.TextboxDueToSCFromAssurantShadow.Text = .original_DueToSCFromAssurant
            Me.TextBoxOtherPriceShadow.Text = .original_OtherPrice
            Me.TextboxLiabilityLimitShadow.Text = .original_LiabilityLimit

            Me.TextboxDeductible_WRITE.Text = .original_Deductible
            Me.TextBoxDiscount.Text = .original_Discount
            Me.TextboxAssurantPays.Text = .original_AssurantPays
            Me.TextboxConsumerPays.Text = .original_ConsumerPays
            Me.TextboxDueToSCFromAssurant.Text = .original_DueToSCFromAssurant
            Me.TextBoxOtherPrice.Text = .original_OtherPrice
            Me.TextboxLiabilityLimit.Text = .original_LiabilityLimit
        End With

        If Not Me.State.isMethodPricePanel_Visible Then
            TextBoxReplacementCost.Text = Me.State.original_AuthorizedAmount
            Me.TextboxAuthorizedAmount.Text = Me.State.original_AuthorizedAmount
            Me.TextboxAuthorizedAmountShadow.Text = Me.State.original_AuthorizedAmount
            ControlMgr.SetVisibleControl(Me, Me.PanelRepair, False)
        Else
            TextboxAuthorizedAmount.Text = Me.State.original_AuthorizedAmount
            TextboxAuthorizedAmountShadow.Text = Me.State.original_AuthorizedAmount
        End If

        UncheckAllMethodPriceCheckBoxes()
        EnableAllMethodPricePanelContols()
        'PopulatePrices()
        'InitialEnableDisableFields()
        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then

            ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, False)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, False)

            ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
            Me.CheckBoxCarryInPrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxCarryInPrice.ID + "') </script>"
            Me.RegisterStartupScript("Startup", x)

        ElseIf Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
            ControlMgr.SetVisibleControl(Me, Me.LabelCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxCarryInPrice, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxCarryInPrice, False)

            ControlMgr.SetVisibleControl(Me, Me.LabelHomePrice, True)
            ControlMgr.SetVisibleControl(Me, Me.CheckBoxHomePrice, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxHomePrice, True)
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("CARRY_IN_PRICE")
            Me.CheckBoxHomePrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxHomePrice.ID + "') </script>"
            Me.RegisterStartupScript("Startup", x)

        End If

        If ((TextboxAuthorizedAmount.Text <> TextBoxCarryInPrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxCleaningPrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxEstimatePrice.Text) AndAlso
            (TextboxAuthorizedAmount.Text <> TextBoxHomePrice.Text)) Then
            Me.PopulateControlFromBOProperty(Me.TextBoxOtherPrice, TextboxAuthorizedAmount.Text)

            Me.CheckBoxOtherPrice.Checked = True

            Dim x As String = "<script language='JavaScript'> CallUpdateAuth('" + TextBoxHomePrice.ID + "') </script>"
            Me.RegisterStartupScript("Startup", x)

        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("SEND_IN_PRICE")
        ElseIf State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then
            LabelCarryInPrice.Text = TranslationBase.TranslateLabelOrMessage("PICK_UP_PRICE")
        End If



    End Sub

    Sub UncheckAllMethodPriceCheckBoxes()
        Me.CheckBoxCarryInPrice.Checked = False
        Me.CheckBoxEstimatePrice.Checked = False
        Me.CheckBoxHomePrice.Checked = False
        Me.CheckBoxOtherPrice.Checked = False
        Me.CheckBoxCleaningPrice.Checked = False
    End Sub

    Sub EnableAllMethodPricePanelContols()
        Me.ChangeEnabledProperty(TextBoxCarryInPrice, True)
        Me.ChangeEnabledProperty(TextBoxCleaningPrice, True)
        Me.ChangeEnabledProperty(TextBoxEstimatePrice, True)
        Me.ChangeEnabledProperty(TextBoxHomePrice, True)
        Me.ChangeEnabledProperty(TextBoxOtherPrice, True)
        Me.ChangeEnabledProperty(CheckBoxCarryInPrice, True)
        Me.ChangeEnabledProperty(CheckBoxCleaningPrice, True)
        Me.ChangeEnabledProperty(CheckBoxEstimatePrice, True)
        Me.ChangeEnabledProperty(CheckBoxHomePrice, True)
        Me.ChangeEnabledProperty(CheckBoxOtherPrice, True)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CauseOfLossId", Me.LabelCauseOfLossId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactName", Me.LabelContactName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CallerName", Me.LabelCallerName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProblemDescription", Me.LabelProblemDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SpecialInstruction", Me.LabelSpecialInstruction)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizedAmount", Me.LabelAuthorizedAmount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LiabilityLimit", Me.LabelLiabilityLimit)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Deductible", Me.LabelDeductible)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DiscountAmount", Me.LabelDiscount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LossDate", Me.LabelLossDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReportedDate", Me.LabelReportDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificateNumber", Me.LabelCertificateNumber)
        'BEGIN - RC - ClaimNumber is never entered by the user, so it should NOT be a Required field
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimNumber", Me.LabelClaimNumber)
        'END - RC - ClaimNumber is never entered by the user, so it should NOT be a Required field
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceCenter", Me.LabelServiceCenter)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AssurantPays", Me.LabelAssurantPays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ConsumerPays", Me.LabelConsumerPays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DueToSCFromAssurant", Me.LabelDueToSCFromAssurant)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsLawsuitId", Me.LabelIsLawsuitId)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "LoanerTaken", Me.LabelLoaner)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PolicyNumber", Me.LabelPolicyNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CallerTaxNumber", Me.LabelCALLER_TAX_NUMBER)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "RepairDate", Me.LabelRepairDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PickUpDate", Me.LabelPickUpDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizationNumber", Me.LabelInvoiceNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NewDeviceSKU", Me.lblNewDeviceSKU)
        Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "ManufacturerId", Me.LBLeNROLLEDmAKE)
        Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "ManufacturerId", Me.lblClaimedMake)
        Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "Model", Me.lblEnrolledModel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "Model", Me.lblClaimedModel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "SerialNumber", Me.lblEnrolledSerialNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "SerialNumber", Me.lblClaimedSerialNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "SKU", Me.lblEnrolledSKu)
        Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "SKU", Me.lblClaimedSKu)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim companyBO As New Company(Me.State.MyBO.CompanyId)
        Dim sSalutation As String = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), companyBO.SalutationId)
        If sSalutation = "Y" Then Me.State.isSalutation = True Else Me.State.isSalutation = False

        Dim certificateBO As New Certificate(Me.State.MyBO.CertificateId)


        Dim listcontextForCauseOfLoss As ListContext = New ListContext()
        listcontextForCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        listcontextForCauseOfLoss.CoverageTypeId = Me.State.MyBO.CoverageTypeId
        listcontextForCauseOfLoss.DealerId = certificateBO.DealerId
        listcontextForCauseOfLoss.ProductCode = certificateBO.ProductCode
        listcontextForCauseOfLoss.LanguageId = Authentication.LangId

        Dim listCauseOfLoss As ListItem() = CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss)
        cboCauseOfLossId.Populate(listCauseOfLoss, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })


        If Me.State.isSalutation Then
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
        SetSelectedItem(Me.cboUseShipAddress, NoId)

        BindListControlToDataView(ddlIssueCode, Me.State.MyBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
        BindListControlToDataView(ddlIssueDescription, Me.State.MyBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)

        ' Me.BindListControlToDataView(Me.cboDedCollMethod, LookupListNew.GetDedCollMethodLookupList(Authentication.LangId))

        Dim dedCollMethod As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEDCOLLMTHD", Thread.CurrentPrincipal.GetLanguageCode())
        cboDedCollMethod.Populate(dedCollMethod, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True 'Fix for Bug 221324 - Adding blank Item
                                           })

        Dim selDedCollMethod As String = LookupListNew.GetCodeFromId(LookupListCache.LK_DED_COLL_METHOD, Me.State.MyBO.DedCollectionMethodID)
        If Not selDedCollMethod Is Nothing Then
            SetSelectedItem(Me.cboDedCollMethod, LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, selDedCollMethod))
        End If

        Dim listcontextForMgList As ListContext = New ListContext()
        listcontextForMgList.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim manufacturerList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMgList)

        If Me.State.MyBO.CertificateItem.IsEquipmentRequired Then

            If Not String.IsNullOrEmpty(Me.State.MyBO.Dealer.EquipmentListCode) Then

                Dim listcontextForManufacturerByEquipmentCode As ListContext = New ListContext()
                listcontextForManufacturerByEquipmentCode.EquipmentListCode = Me.State.MyBO.Dealer.EquipmentListCode
                listcontextForManufacturerByEquipmentCode.EffectiveOnDate = Date.Now

                Dim ManufactirereByEquipmentCodeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByEquipmentCode", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForManufacturerByEquipmentCode)
                ddlEnrolledManuf.Populate(ManufactirereByEquipmentCodeList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })


            Else
                Me.MasterPage.MessageController.AddWarning("EQUIPMENT_LIST_DOES_NOT_EXIST_FOR_DEALER")
            End If
            'Me.BindListControlToDataView(Me.ddlClaimedManuf, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            ddlClaimedManuf.Populate(manufacturerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
        Else

            ' Me.BindListControlToDataView(Me.ddlEnrolledManuf, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            ddlEnrolledManuf.Populate(manufacturerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })
        End If

    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Dim oCoverageType As New CoverageType(Me.State.MyBO.CoverageTypeId)
            Dim oTranslate As Boolean = True
            Dim oCauseOfLoss As String

            Dim objDateOfReport As DateType = Nothing
            Dim strDateOfReport As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)

            Dim objDateOfLoss As DateType = Nothing
            Dim strDateOfLoss As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)

            Try
                If Not .CauseOfLossId.Equals(Guid.Empty) Then
                    Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindById(.CauseOfLossId)
                    If Not oCoverageLoss Is Nothing Then
                        Me.SetSelectedItem(Me.cboCauseOfLossId, .CauseOfLossId)
                    Else
                        Me.SetSelectedItem(Me.cboCauseOfLossId, Guid.Empty)
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
                    Me.SetSelectedItem(Me.cboCauseOfLossId, Me.State.MyBO.GetCauseOfLossID(Me.State.MyBO.CoverageTypeId))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController, oTranslate)
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

            If Not Me.State.MyBO.PoliceReport Is Nothing Then
                Me.State.PoliceReportBO = Me.State.MyBO.PoliceReport
                Me.UserCtrPoliceReport.Bind(Me.State.PoliceReportBO, UserControlMessageController)
                Me.PanelPoliceReport.Visible = True
                Me.moUserControlPoliceReport.ChangeEnabledControlProperty(False)
            Else
                Me.State.PoliceReportBO = Nothing
                Me.PanelPoliceReport.Visible = False
            End If

            txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
            txtCreatedDate.Text = DateTime.Now.ToString(LocalizationMgr.CurrentCulture)

            Dim myContractId As Guid = Contract.GetContractID(Me.State.MyBO.CertificateId)
            curContractId = GuidControl.GuidToHexString(myContractId)
            curCertId = GuidControl.GuidToHexString(Me.State.MyBO.CertificateId)
            curCertItemCoverageId = GuidControl.GuidToHexString(Me.State.MyBO.CertItemCoverageId)
            curMethodOfRepairCode = Me.State.MyBO.MethodOfRepairCode

            Me.PopulateControlFromBOProperty(Me.TextboxLossDate, .LossDate)
            Me.PopulateControlFromBOProperty(Me.TextboxReportDate, .ReportedDate)
            Me.State.MyBO.RecalculateDeductibleForChanges()

            Me.PopulatePrices()


            Me.PopulateControlFromBOProperty(Me.TextboxContactName, .ContactName)
            Me.PopulateControlFromBOProperty(Me.TextboxCallerName, .CallerName)
            Me.PopulateControlFromBOProperty(Me.TextboxProblemDescription, .ProblemDescription)
            Me.PopulateControlFromBOProperty(Me.TextboxSpecialInstruction, .SpecialInstruction)
            Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, .AuthorizedAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxLiabilityLimit, .LiabilityLimit)
            Me.SetSelectedItem(Me.cboLawsuitId, Me.State.MyBO.IsLawsuitId)

            Dim moCertItemCvg As New CertItemCoverage(.CertItemCoverageId)
            Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
            Dim moCert As New Certificate(moCertItem.CertId)
            Dim moDealer As New Dealer(moCert.DealerId)

            Me.PopulateControlFromBOProperty(Me.TextboxDeductible_WRITE, .Deductible)

            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified AndAlso .IsNew Then
                'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                If Not strDateOfLoss Is Nothing AndAlso Not strDateOfLoss.Equals(String.Empty) Then
                    objDateOfLoss = New DateType(Convert.ToDateTime(strDateOfLoss))
                    Me.PopulateControlFromBOProperty(Me.TextboxLossDate, objDateOfLoss)
                End If
            Else
                Me.PopulateControlFromBOProperty(Me.TextboxLossDate, .LossDate)

            End If
            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified AndAlso .IsNew Then
                'If Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
                If Not strDateOfReport Is Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                    objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                    Me.PopulateControlFromBOProperty(Me.TextboxReportDate, objDateOfReport)
                End If
            Else
                Me.PopulateControlFromBOProperty(Me.TextboxReportDate, .ReportedDate)

            End If
            Me.PopulateControlFromBOProperty(Me.TextboxCertificateNumber, .CertificateNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxClaimNumber, .ClaimNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceCenter, .ServiceCenter)
            Me.PopulateControlFromBOProperty(Me.TextboxAssurantPays, .AssurantPays)
            Me.PopulateControlFromBOProperty(Me.TextboxConsumerPays, .ConsumerPays)
            Me.PopulateControlFromBOProperty(Me.TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
            If (moDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (moDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                ControlMgr.SetVisibleControl(Me, LabelDueToSCFromAssurant, False)
                ControlMgr.SetVisibleControl(Me, TextboxDueToSCFromAssurant, False)
                ControlMgr.SetVisibleControl(Me, TextboxDueToSCFromAssurantShadow, False)
            End If
            Me.PopulateControlFromBOProperty(Me.TextBoxDiscount, .DiscountAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxPolicyNumber, .PolicyNumber)

            Me.PopulateControlFromBOProperty(Me.TextboxRepairdate, .RepairDate)
            Me.PopulateControlFromBOProperty(Me.TextboxPickUpDate, .PickUpDate)

            Me.CheckBoxCarryInPrice.Checked = (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN OrElse State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP)
            Me.CheckBoxHomePrice.Checked = Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME

            If Me.State.isSalutation Then
                Dim certificateBO As New Certificate(Me.State.MyBO.CertificateId)
                If Me.State.MyBO.CallerSalutationID.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.cboCallerSalutationId, certificateBO.SalutationId)
                Else
                    Me.SetSelectedItem(Me.cboCallerSalutationId, Me.State.MyBO.CallerSalutationID)
                End If
                If Me.State.MyBO.ContactSalutationID.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.cboContactSalutationId, certificateBO.SalutationId)
                Else
                    Me.SetSelectedItem(Me.cboContactSalutationId, Me.State.MyBO.ContactSalutationID)
                End If
            End If

            Dim ContractBO As New Contract(myContractId)

            If ContractBO.PayOutstandingPremiumId.Equals(Me.State.yesId) Then
                Dim dv As DataView
                dv = Certificate.PremiumTotals(Me.State.MyBO.CertificateId)
                Me.State.oGrossAmtReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)
                Dim dvBilling As DataView

                If Me.State.mDealer.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                    dvBilling = BillingPayDetail.getBillPayTotals(Me.State.MyBO.CertificateId)
                    Me.State.oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
                Else
                    dvBilling = BillingDetail.getBillingTotals(Me.State.MyBO.CertificateId)
                    Me.State.oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                End If


                Me.State.oOutstandingPremAmt = Me.State.oGrossAmtReceived - Me.State.oBillingTotalAmount
                Me.PopulateControlFromBOProperty(Me.TextboxOutstandingPremAmt, Me.State.oOutstandingPremAmt)
            End If

            Me.CheckBoxLoanerTaken.Checked = Me.State.MyBO.LoanerTaken

            Me.PopulateControlFromBOProperty(Me.TextboxCALLER_TAX_NUMBER, .CallerTaxNumber)

            Me.PopulateControlFromBOProperty(Me.TextboxInvoiceNumber, .AuthorizationNumber)
            Me.PopulateControlFromBOProperty(Me.txtNewDeviceSKU, .NewDeviceSku)
            'REQ-1153
            If Not .ContactInfoId.Equals(Guid.Empty) Then
                Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
                SetSelectedItem(Me.cboUseShipAddress, YesId)
                moUserControlContactInfo.Visible = True

                Me.UserControlAddress.ClaimDetailsBind(Me.State.MyBO.ContactInfo.Address)
                Me.UserControlContactInfo.Bind(Me.State.MyBO.ContactInfo)
            End If
            hdnDealerId.Value = Me.State.MyBO.Dealer.Id.ToString
        End With

    End Sub

    Private Sub TextboxLossDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextboxLossDate.TextChanged
        CalcDeductibleBasedOnPercentOfListPrice()
    End Sub

    Private Sub txtNewDeviceSKU_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNewDeviceSKU.TextChanged
        If Me.State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE Then
            CalcDeductibleBasedOnPercentOfListPrice()
        End If
    End Sub

    Private Sub CalcDeductibleBasedOnPercentOfListPrice()
        If (DateHelper.IsDate(TextboxLossDate.Text)) Then
            Dim moCertItemCvg As New CertItemCoverage(Me.State.MyBO.CertItemCoverageId)
            Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
            Dim moCert As New Certificate(moCertItem.CertId)

            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.MethodOfRepairId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                Dim deductible As DecimalType, strSKU As String, dtLossDate As Date

                dtLossDate = DateHelper.GetDateValue(TextboxLossDate.Text)
                strSKU = txtNewDeviceSKU.Text.Trim
                If strSKU = String.Empty Or (Not ListPrice.IsSKUValid(moCert.DealerId, strSKU, dtLossDate, deductible)) Then
                    strSKU = moCertItem.SkuNumber
                    deductible = ListPrice.GetListPrice(moCert.DealerId, strSKU, dtLossDate.ToString("yyyyMMdd"))
                End If

                If (deductible <> Nothing) Then
                    Me.State.MyBO.Deductible = deductible.Value * oDeductible.DeductiblePercentage.Value / 100
                Else
                    Me.State.MyBO.Deductible = 0
                End If

                Me.PopulateControlFromBOProperty(Me.TextboxDeductible_WRITE, Me.State.MyBO.Deductible)
                TextboxDeductibleShadow.Text = Me.TextboxDeductible_WRITE.Text
            End If
        End If
    End Sub

    Protected Sub PopulatePoliceReportBOFromUserCtr(ByVal blnExcludePoliceReportSave As Boolean)
        With Me.State.PoliceReportBO
            .ClaimId = Me.State.MyBO.Id
            ' determine validate or dont validate
            Me.UserCtrPoliceReport.PopulateBOFromControl(blnExcludePoliceReportSave)
        End With
    End Sub

    Sub PopulateClaimEquipment()
        With Me.State.MyBO
            'enrolled equipment
            Dim flag As Boolean = False
            If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE_EQUIPMENT_UPDATE) Then flag = True

            ControlMgr.SetVisibleControl(Me, Me.ddlEnrolledManuf, flag)
            ControlMgr.SetVisibleControl(Me, Me.txtEnrolledMake, Not flag)
            ControlMgr.SetVisibleControl(Me, Me.ddlEnrolledEquipSKU, flag)
            ControlMgr.SetVisibleControl(Me, Me.txtEnrolledSku, Not flag)

            Me.SetEnabledForControlFamily(Me.txtEnrolledMake, flag)
            Me.SetEnabledForControlFamily(Me.txtEnrolledModel, flag, True)
            Me.SetEnabledForControlFamily(Me.txtenrolledSerial, flag, True)
            Me.SetEnabledForControlFamily(Me.txtEnrolledSku, flag)

            If Not .EnrolledEquipment Is Nothing Then
                Dim dvEnrolled As DataView = LookupListNew.GetManufacturerbyEquipmentList(Me.State.MyBO.Dealer.EquipmentListCode, Date.Now)
                If Not dvEnrolled Is Nothing AndAlso Not BusinessObjectBase.FindRow(.EnrolledEquipment.ManufacturerId, LookupListNew.COL_ID_NAME, dvEnrolled.Table) Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.ddlEnrolledManuf, .EnrolledEquipment.ManufacturerId)
                Else
                    Me.MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
                End If
                Me.PopulateControlFromBOProperty(Me.txtEnrolledMake, .EnrolledEquipment.Manufacturer)
                Me.PopulateControlFromBOProperty(Me.txtEnrolledModel, .EnrolledEquipment.Model)
                Me.PopulateControlFromBOProperty(Me.txtenrolledSerial, .EnrolledEquipment.SerialNumber)
                'Me.PopulateControlFromBOProperty(Me.txtEnrolledSku, .EnrolledEquipment.SKU)
                If Not .EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
                    Dim dv As DataView = Me.State.MyBO.CertificateItem.LoadSku(.EnrolledEquipment.EquipmentId, Me.State.MyBO.Dealer.Id)
                    Me.ddlEnrolledEquipSKU.DataSource = dv
                    Me.ddlEnrolledEquipSKU.DataTextField = "SKU_NUMBER"
                    Me.ddlEnrolledEquipSKU.DataValueField = "SKU_NUMBER"
                    Me.ddlEnrolledEquipSKU.DataBind()

                    If Not dv Is Nothing AndAlso dv.FindRows(.EnrolledEquipment.SKU).Length > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtEnrolledSku, .EnrolledEquipment.SKU)
                        Me.ddlEnrolledEquipSKU.SelectedValue = .EnrolledEquipment.SKU
                        hdnSelectedEnrolledSku.Value = .EnrolledEquipment.SKU
                    End If

                End If
            End If
            'claimed equipment

            ControlMgr.SetVisibleControl(Me, Me.ddlClaimedManuf, flag)
            ControlMgr.SetVisibleControl(Me, Me.txtClaimedMake, Not flag)
            ControlMgr.SetVisibleControl(Me, Me.ddlClaimedEquipSKU, flag)
            ControlMgr.SetVisibleControl(Me, Me.txtClaimedSku, Not flag)

            Me.SetEnabledForControlFamily(Me.txtClaimedMake, flag)
            Me.SetEnabledForControlFamily(Me.txtClaimedModel, flag, True)
            Me.SetEnabledForControlFamily(Me.txtClaimedSerial, flag, True)
            Me.SetEnabledForControlFamily(Me.txtClaimedSku, flag)
            If Not .ClaimedEquipment Is Nothing Then
                Me.PopulateControlFromBOProperty(Me.ddlClaimedManuf, .ClaimedEquipment.ManufacturerId)
                Me.PopulateControlFromBOProperty(Me.txtClaimedMake, .ClaimedEquipment.Manufacturer)
                Me.PopulateControlFromBOProperty(Me.txtClaimedModel, .ClaimedEquipment.Model)
                Me.PopulateControlFromBOProperty(Me.txtClaimedSerial, .ClaimedEquipment.SerialNumber)
                'Me.PopulateControlFromBOProperty(Me.txtClaimedSku, .ClaimedEquipment.SKU)
                If Not .ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                    Dim dv As DataView = Me.State.MyBO.CertificateItem.LoadSku(.ClaimedEquipment.EquipmentId, Me.State.MyBO.Dealer.Id)
                    Me.ddlClaimedEquipSKU.DataSource = dv
                    Me.ddlClaimedEquipSKU.DataTextField = "SKU_NUMBER"
                    Me.ddlClaimedEquipSKU.DataValueField = "SKU_NUMBER"
                    Me.ddlClaimedEquipSKU.DataBind()

                    If Not dv Is Nothing AndAlso dv.FindRows(.ClaimedEquipment.SKU).Length > 0 Then
                        Me.PopulateControlFromBOProperty(Me.txtClaimedSku, .ClaimedEquipment.SKU)
                        Me.ddlClaimedEquipSKU.SelectedValue = .ClaimedEquipment.SKU
                        hdnSelectedClaimedSku.Value = .ClaimedEquipment.SKU
                    End If

                End If
            End If
        End With
    End Sub

    Private Sub PopulateServiceCenterSelected()

        With Me.State.MyBO.ServiceCenterObject
            Try

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterCode, .Code)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterContactName, .ContactName)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterName, .Description)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterPhone1, .Phone1)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterAddress1, .Address.Address1)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterPhone2, .Phone2)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterAddress2, .Address.Address2)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterFax, .Fax)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterCity, .Address.City)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterBussHours, .BusinessHours)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterState, LookupListNew.GetDescriptionFromId(LookupListNew.LK_REGIONS, .Address.RegionId))
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterProcessFee, .ProcessingFee)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterCountry, .Address.countryBO.Description)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterEmail, .Email)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterZip, .Address.ZipLocator)
                Me.PopulateControlFromBOProperty(Me.lblServiceCenterCCEmail, .CcEmail)

                Me.PopulateControlFromBOProperty(Me.lblServiceCenterOrigDealer, Me.State.mDealer.DealerName)
                IIf(.DefaultToEmailFlag, Me.chkServiceCenterDefToEmail.Checked, Not Me.chkServiceCenterDefToEmail.Checked)

                IIf(.Shipping, Me.chkServiceCenterShipping.Checked, Not Me.chkServiceCenterShipping.Checked)
                Me.PopulateControlFromBOProperty(Me.txtServiceCenterComments, .Comments)


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
        Dim lCertItemCoverage As New CertItemCoverage(Me.State.MyBO.CertItemCoverageId)
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
        dv = Me.State.MyBO.GetRePairPricesByMethodOfRepair

        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
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

        Me.PopulateControlFromBOProperty(Me.TextBoxCarryInPrice, nCarryInPrice)
        Me.PopulateControlFromBOProperty(Me.TextBoxCleaningPrice, nCleaningPrice)
        Me.PopulateControlFromBOProperty(Me.TextBoxEstimatePrice, nEstimatePrice)
        Me.PopulateControlFromBOProperty(Me.TextBoxHomePrice, nHomePrice)
        Me.PopulateControlFromBOProperty(Me.TextBoxOtherPrice, nOtherPrice)
        Me.PopulateControlFromBOProperty(Me.TextBoxReplacementCost, nReplacementCost)


        If ((Me.State.MyBO.ClaimActivityCode Is Nothing OrElse Me.State.MyBO.ClaimActivityCode.Trim.Length = 0) AndAlso Not dv Is Nothing) Or Me.State.MyBO.ClaimSpecialServiceId = Me.State.yesId Then
            ''''REQ_1106 - Price list integration

            'calculating the estimate price
            price = 0
            Dim dvEstimate As DataView = Me.State.MyBO.GetPricesForServiceType(Codes.SERVICE_CLASS__REPAIR, Codes.SERVICE_TYPE__ESTIMATE_PRICE)
            If Not dvEstimate Is Nothing AndAlso dvEstimate.Count > 0 Then
                price = CDec(dvEstimate(0)(COL_PRICE_DV))
                nEstimatePrice = price 'CDec(dvEstimate.Table.Rows(0)(COL_PRICE_DV))
            End If

            Me.PopulateControlFromBOProperty(Me.TextBoxCarryInPrice, nCarryInPrice)
            Me.PopulateControlFromBOProperty(Me.TextBoxCleaningPrice, nCleaningPrice)
            Me.PopulateControlFromBOProperty(Me.TextBoxEstimatePrice, nEstimatePrice)
            Me.PopulateControlFromBOProperty(Me.TextBoxHomePrice, nHomePrice)
            ''''

            'BEGIN - If the AuthorizedAmount is NOT Equal to any of the PriceGroupDetail prices, then set the 
            '        "OtherPrice" = AuthorizedAmount
            ''''set the authorized amount REQ-1106
            'Me.State.MyBO.SetAuthorizedAmount()

            If ((Me.State.MyBO.AuthorizedAmount.Value <> nCarryInPrice.Value) AndAlso
                 (Me.State.MyBO.AuthorizedAmount.Value <> nCleaningPrice.Value) AndAlso
                 (Me.State.MyBO.AuthorizedAmount.Value <> nEstimatePrice.Value) AndAlso
                 (Me.State.MyBO.AuthorizedAmount.Value <> nHomePrice.Value)) Then
                Me.PopulateControlFromBOProperty(Me.TextBoxOtherPrice, Me.State.MyBO.AuthorizedAmount)

                Me.UncheckAllMethodPriceCheckBoxes()
                Me.CheckBoxOtherPrice.Checked = True
            End If
            'END - If the AuthorizedAmount is NOT Equal to any of the PriceGroupDetail prices, then set the 
            '        "OtherPrice" = AuthorizedAmount

        End If

        If (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT _
       And (Me.State.MyBO.ClaimSpecialServiceId.Equals(Guid.Empty) Or
            Me.State.MyBO.ClaimSpecialServiceId = Me.State.noId)) Then
            ' Replacement Price
            If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING Then
                Me.TextBoxReplacementCost.Text = Me.TextboxAuthorizedAmount.Text
            Else
                'If Not pgDetail Is Nothing Then
                '    pgDetail.ReplacementPrice = pgDetail.ReplacementPrice.Value * (1 + dTaxRate)
                '    Me.PopulateControlFromBOProperty(Me.TextBoxReplacementCost, pgDetail.ReplacementPrice)
                'End If
                Me.PopulateControlFromBOProperty(Me.TextBoxReplacementCost, nReplacementPrice)
                Me.TextboxAuthorizedAmount.Text = Me.TextBoxReplacementCost.Text
                NotifyAuthorizedAmountHasChanged()
            End If

        End If
        Me.EnableDisableFields()
    End Sub

    '' REQ-784
    Protected Sub PopulateNewClaimContactInfoBOsFromForm()
        Me.State.MyBO.ContactInfo.Address.InforceFieldValidation = True
        UserControlContactInfo.PopulateBOFromControl(True)
        Me.State.MyBO.ContactInfo.Save()
    End Sub

    Protected Sub PopulateBOsFromForm()

        ' Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)

        With Me.State.MyBO

            Me.PopulateBOProperty(Me.State.MyBO, "CauseOfLossId", Me.cboCauseOfLossId)
            Me.PopulateBOProperty(Me.State.MyBO, "IsLawsuitId", Me.cboLawsuitId)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactName", Me.TextboxContactName)
            Me.PopulateBOProperty(Me.State.MyBO, "CallerName", Me.TextboxCallerName)
            Me.PopulateBOProperty(Me.State.MyBO, "CallerTaxNumber", Me.TextboxCALLER_TAX_NUMBER)
            Me.PopulateBOProperty(Me.State.MyBO, "ProblemDescription", Me.TextboxProblemDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "SpecialInstruction", Me.TextboxSpecialInstruction)
            Me.PopulateBOProperty(Me.State.MyBO, "AuthorizedAmount", Me.TextboxAuthorizedAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "Deductible", Me.TextboxDeductible_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "DiscountAmount", Me.TextBoxDiscount)
            Me.PopulateBOProperty(Me.State.MyBO, "PolicyNumber", Me.TextboxPolicyNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "LossDate", Me.TextboxLossDate)
            Me.PopulateBOProperty(Me.State.MyBO, "ReportedDate", Me.TextboxReportDate)
            Me.PopulateBOProperty(Me.State.MyBO, "NewDeviceSku", Me.txtNewDeviceSKU)

            'following logic is only when its displayed - Backend Claim
            If Me.TextboxRepairdate.Visible = True Then
                Me.PopulateBOProperty(Me.State.MyBO, "RepairDate", Me.TextboxRepairdate)
            End If
            'following logic is only when its displayed - Backend Claim
            If Me.TextboxPickUpDate.Visible = True Then
                Me.PopulateBOProperty(Me.State.MyBO, "PickUpDate", Me.TextboxPickUpDate)
            End If
            If Me.TextboxInvoiceNumber.Visible = True Then
                Me.PopulateBOProperty(Me.State.MyBO, "AuthorizationNumber", Me.TextboxInvoiceNumber)
            End If

            If Me.State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                'CalLiabLimitUsingLossDateAndDeprSchedule()
                Me.PopulateBOProperty(Me.State.MyBO, "LiabilityLimit", Me.TextboxLiabilityLimit)
            End If

            If Me.State.isSalutation Then
                Me.PopulateBOProperty(Me.State.MyBO, "ContactSalutationID", Me.cboContactSalutationId)
                Me.PopulateBOProperty(Me.State.MyBO, "CallerSalutationID", Me.cboCallerSalutationId)
            End If

            Me.State.MyBO.LoanerTaken = Me.CheckBoxLoanerTaken.Checked
            If Me.PanelPoliceReport.Visible = True Then
                'Dim blnExcludePoliceReportSave As Boolean
                'If Me.moUserControlPoliceReport.isempty Then
                'End If
                'Exclude the validation for the policereport BO from here, so excludesave shld be set to true
                Me.PopulatePoliceReportBOFromUserCtr(True)
            Else
                Me.State.PoliceReportBO = Nothing
            End If

            If Not Me.State.MyBO.CauseOfLossId = Guid.Empty Then
                Me.State.MyBO.ClaimSpecialServiceId = Me.State.MyBO.GetSpecialServiceValue()
                If Me.State.MyBO.ClaimSpecialServiceId = Me.State.yesId Then
                    Me.PopulateBOProperty(Me.State.MyBO, "WhoPaysId", AssurantPays)
                End If
            Else
                Me.State.MyBO.ClaimSpecialServiceId = Me.State.noId
            End If

            '' REQ-784 
            If Not Me.State.MyBO.ContactInfo Is Nothing Then
                If Me.State.MyBO.ContactInfo.IsDeleted = False Then
                    Me.State.MyBO.ContactInfoId = Me.State.MyBO.ContactInfo.Id
                End If
            End If

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cboUseShipAddress.SelectedValue = YesId.ToString Then
            PopulateNewClaimContactInfoBOsFromForm()
        End If

        If (Me.State.MyBO.CertificateItem.IsEquipmentRequired) Then
            If (Me.ddlEnrolledManuf.Items.Count > 0 AndAlso Not New Guid(Me.ddlEnrolledManuf.SelectedItem.Value).Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(txtEnrolledModel.Text)) Then
                If Me.State.MyBO.EnrolledEquipment Is Nothing Then Me.State.MyBO.EnrolledEquipment = CType(Me.State.MyBO.ClaimEquipmentChildren.GetNewChild(), ClaimEquipment)
                Me.State.MyBO.EnrolledEquipment.ClaimId = Me.State.MyBO.Id
                Me.PopulateBOProperty(Me.State.MyBO.EnrolledEquipment, "ManufacturerId", Me.ddlEnrolledManuf)
                Me.PopulateBOProperty(Me.State.MyBO.EnrolledEquipment, "Model", Me.txtEnrolledModel)
                Me.PopulateBOProperty(Me.State.MyBO.EnrolledEquipment, "SerialNumber", Me.txtenrolledSerial)
                Me.PopulateBOProperty(Me.State.MyBO.EnrolledEquipment, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, "E"))
                Me.State.MyBO.EnrolledEquipment.SKU = hdnSelectedEnrolledSku.Value
                If (Not Me.State.MyBO.EnrolledEquipment.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.State.MyBO.EnrolledEquipment.Model)) Then
                    Me.State.MyBO.EnrolledEquipment.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(Me.State.MyBO.Dealer.EquipmentListCode, DateTime.Today, Me.State.MyBO.EnrolledEquipment.ManufacturerId, Me.State.MyBO.EnrolledEquipment.Model)
                End If
            End If
            Me.PopulateBOProperty(Me.State.MyBO.ClaimedEquipment, "ManufacturerId", Me.ddlClaimedManuf)
            Me.PopulateBOProperty(Me.State.MyBO.ClaimedEquipment, "Model", Me.txtClaimedModel)
            Me.PopulateBOProperty(Me.State.MyBO.ClaimedEquipment, "SerialNumber", Me.txtClaimedSerial)
            Me.PopulateBOProperty(Me.State.MyBO.ClaimedEquipment, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, "C"))
            Me.State.MyBO.ClaimedEquipment.SKU = hdnSelectedClaimedSku.Value
            If (Not Me.State.MyBO.ClaimedEquipment.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.State.MyBO.ClaimedEquipment.Model)) Then
                Me.State.MyBO.ClaimedEquipment.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(Me.State.MyBO.Dealer.EquipmentListCode, DateTime.Today, Me.State.MyBO.ClaimedEquipment.ManufacturerId, Me.State.MyBO.ClaimedEquipment.Model)
                If Not Me.State.MyBO.ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                    Me.PopulatePrices()
                End If
            End If

        End If
    End Sub

    ' Clean Popup Input
    Private Sub CleanPopupInput()
        Try
            If Not Me.State Is Nothing Then
                'Clean after consuming the action
                Me.State.LastState = InternalStates.Regular
                Me.HiddenSaveChangesPromptResponse.Value = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Function CheckIfComingFromCreateClaimConfirm() As Boolean
        Dim returnValue As Boolean = False
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        CleanPopupInput()

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            'Clean after consuming the action
            'Me.State.LastState = InternalStates.Regular
            'Me.HiddenSaveChangesPromptResponse.Value = ""
            Me.CreateClaim()
            returnValue = True
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
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

            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified Then

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
                    If .IsNew AndAlso Not (Me.State.MyBO.CheckSvcWrantyClaimControl() And State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then
                        blnExceedMaxReplacements = .IsMaxReplacementExceeded(.CertificateId, .LossDate.Value)
                    End If
                End If

            Else
                If .IsNew AndAlso Not (Me.State.MyBO.CheckSvcWrantyClaimControl() And State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then 'only check the condition for new claim
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
        Dim c As Comment = Me.State.MyBO.AddNewComment()

        'Add comments to indicate that the claim will be closed

        If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified Then

            If Not blnClaimReportedWithinGracePeriod Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__NOT_REPORTED_WITHIN_GRACE_PERIOD)
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_NOT_WITHIN_GRACE_PERIOD)
            End If

            If Not blnCoverageTypeNotMissing Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__COVERAGE_TYPE_MISSING)
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_COVERAGE_TYPE_MISSING)
            End If

            If blnClaimReportedWithinGracePeriod And blnCoverageTypeNotMissing Then

                If blnExceedMaxReplacements Then
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
                End If

                If Not Me.State.MyBO.Certificate.IsSubscriberStatusValid Then
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
                End If

                If (CType(Me.State.MyBO.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Integer) > 0) Then
                    If (Me.State.MyBO.CoverageRemainLiabilityAmount(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.LossDate) <= 0) Then
                        Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                    End If
                End If
                If (CType(Me.State.MyBO.Certificate.ProductLiabilityLimit.ToString, Integer) > 0) Then
                    If (Me.State.MyBO.ProductRemainLiabilityAmount(Me.State.MyBO.CertificateId, Me.State.MyBO.LossDate) <= 0) Then
                        Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                        Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
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
            If Not Me.State.MyBO.Certificate.IsSubscriberStatusValid Then
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
            End If

            If (CType(Me.State.MyBO.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Integer) > 0) Then
                If (Me.State.MyBO.CoverageRemainLiabilityAmount(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.LossDate) <= 0) Then
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
            If (CType(Me.State.MyBO.Certificate.ProductLiabilityLimit.ToString, Integer) > 0) Then
                If (Me.State.MyBO.ProductRemainLiabilityAmount(Me.State.MyBO.CertificateId, Me.State.MyBO.LossDate) <= 0) Then
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
        End If


        'Call the Create ShippingInfo Logic
        If Not Me.State.ShippingInfoBO Is Nothing Then Me.State.MyBO.AttachShippingInfo(Me.State.ShippingInfoBO)

        Dim oldStatus As String = Me.State.MyBO.StatusCode
        Try
            Dim blnChangeStatustoActive As Boolean = False
            If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso
                Not Me.State.MyBO.IsSupervisorAuthorizationRequired AndAlso
                (Not Me.State.PoliceReportBO Is Nothing AndAlso Not Me.State.PoliceReportBO.IsEmpty) AndAlso
                Me.State.MyBO.Certificate.IsSubscriberStatusValid Then

                blnChangeStatustoActive = True
            ElseIf Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso
                    Not Me.State.MyBO.IsSupervisorAuthorizationRequired AndAlso
                    Me.State.PoliceReportBO Is Nothing AndAlso
                    Me.State.MyBO.Certificate.IsSubscriberStatusValid Then

                blnChangeStatustoActive = True
            ElseIf Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso Me.State.oOutstandingPremAmt = 0 AndAlso
                     Me.State.PayOutstandingPremium = True Then

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
            Me.State.MyBO.CreateEnrolledEquipment()
            '''''''''''

            'Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.MethodOfRepairId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                Dim lstPrice As DecimalType
                If Not Me.State.MyBO.LossDate Is Nothing Then
                    Dim strSKU As String = String.Empty
                    If Not Me.State.MyBO.NewDeviceSku Is Nothing Then
                        strSKU = Me.State.MyBO.NewDeviceSku.Trim()
                    End If
                    If strSKU = String.Empty Then strSKU = Me.State.MyBO.CertificateItem.SkuNumber
                    If Me.State.MyBO.CertificateItem.IsEquipmentRequired Then strSKU = Me.State.MyBO.ClaimedEquipment.SKU
                    lstPrice = ListPrice.GetListPrice(Me.State.MyBO.Certificate.DealerId, strSKU, Me.State.MyBO.LossDate.Value.ToString(("yyyyMMdd")))
                    'REQ-918 Get the Claim Equipment Object for this Claim and Assign the above Price to it
                    If (lstPrice Is Nothing) Then
                        bListPriceFound = False
                        'DEF-21716-START
                        c = Me.State.MyBO.AddNewComment()
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                        c.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
                        'cmnt = Me.State.MyBO.AddNewComment()
                        'cmnt.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                        'cmnt.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
                        'DEF-21716-END
                    Else
                        If Not Me.State.MyBO.EnrolledEquipment Is Nothing Then
                            Me.State.MyBO.EnrolledEquipment.Price = lstPrice
                            bListPriceFound = True
                        End If
                    End If
                End If
            End If
            If bListPriceFound Then
                If blnChangeStatustoActive Then
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
                End If
            Else
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            Me.State.MyBO.CalculateFollowUpDate()

            Me.State.MyBO.IsUpdatedComment = True
            If Not Me.State.PoliceReportBO Is Nothing AndAlso
              Me.State.PoliceReportBO.IsEmpty Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            'Set the Claim to Pending Status if the Subscriber Status is not Valid 
            If Not Me.State.MyBO.Certificate.IsSubscriberStatusValid Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            If Me.State.PayOutstandingPremium = True Then
                If Me.State.oOutstandingPremAmt > 0 Then
                    c = Me.State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_PAYMENT_ON_OUTSTANDING_PREMIUM)
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                ElseIf Me.State.oOutstandingPremAmt = 0 And blnChangeStatustoActive Then
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
                End If
            End If

            'If Claim has Open\Pending issues attached to it , Save it in Pending state
            Select Case Me.State.MyBO.IssuesStatus
                Case Codes.CLAIMISSUE_STATUS__OPEN
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    c = Me.State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
                Case Codes.CLAIMISSUE_STATUS__REJECTED
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                    If Not Me.State.MyBO.ClaimIssuesList Is Nothing Then
                        For Each Item As ClaimIssue In Me.State.MyBO.ClaimIssuesList
                            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                                If Not Item.Issue.DeniedReason Is Nothing Then
                                    Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, Item.Issue.DeniedReason)
                                Else
                                    Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                End If
                                Exit For
                            End If
                        Next
                    Else
                        Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                    End If

                    'Me.State.MyBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                    c = Me.State.MyBO.AddNewComment()
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                    c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
            End Select

            If Not Me.State.MyBO.ValidateAndMatchClaimedEnrolledEquipments(c) Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            End If

            'Display a Popup Message to Collect the Deductible if the Dealer is set up for Collection and then Save the Claim
            'Make sure the claim is Authorized and has deductible greater than 0
            If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And Me.State.MyBO.Deductible.Value > 0D Then
                If (Me.State.mDealer Is Nothing) Then
                    Me.State.mDealer = New Dealer(Me.State.MyBO.CompanyId, Me.State.MyBO.DealerCode)
                End If
                If Me.State.mDealer.DeductibleCollectionId = Me.State.yesId Then
                    'The dropdown with the Deductible Collection Methods is already populated
                    Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                    Me.RegisterStartupScript("Startup", x)
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

            If Me.State.MyBO IsNot Nothing AndAlso Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE Then
                'user story 192764 - Task-199011--Start------
                Dim dsCaseFields As DataSet = CaseBase.GetCaseFieldsList(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (Not dsCaseFields Is Nothing AndAlso dsCaseFields.Tables.Count > 0 AndAlso dsCaseFields.Tables(0).Rows.Count > 0) Then
                    Dim hasBenefit As DataRow() = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                    If (Not hasBenefit Is Nothing AndAlso hasBenefit.Length > 0) Then
                        If (Not hasBenefit(0)("field_value") Is Nothing AndAlso hasBenefit(0)("field_value").ToString().ToUpper() = Boolean.TrueString.ToUpper()) Then

                            Dim benefitCheckResponse As LegacyBridgeResponse
                            Try
                                Dim client As LegacyBridgeServiceClient = Claim.GetLegacyBridgeServiceClient()
                                benefitCheckResponse = WcfClientHelper.Execute(Of LegacyBridgeServiceClient, ILegacyBridgeService, LegacyBridgeResponse)(
                                                                    client,
                                                                    New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                    Function(ByVal lc As LegacyBridgeServiceClient)
                                                                        Return lc.BenefitClaimPreCheck(GuidControl.ByteArrayToGuid(hasBenefit(0)("case_Id")).ToString())
                                                                    End Function)
                                If (Not benefitCheckResponse Is Nothing) Then
                                    Me.State.MyBO.Status = If(benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Approve, BasicClaimStatus.Active, BasicClaimStatus.Pending)
                                    If (benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Deny) Then
                                        Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECKFAIL")
                                        Dim newClaimIssue As ClaimIssue = CType(Me.State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                                        newClaimIssue.SaveNewIssue(Me.State.MyBO.Id, issueId, Me.State.MyBO.Certificate.Id, True)
                                    End If
                                Else
                                    Me.State.MyBO.Status = BasicClaimStatus.Pending
                                    Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                                    Dim newClaimIssue As ClaimIssue = CType(Me.State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                                    newClaimIssue.SaveNewIssue(Me.State.MyBO.Id, issueId, Me.State.MyBO.Certificate.Id, True)
                                End If

                            Catch ex As Exception
                                Log(ex)
                                Me.State.MyBO.Status = BasicClaimStatus.Pending
                                Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                                Dim newClaimIssue As ClaimIssue = CType(Me.State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                                newClaimIssue.SaveNewIssue(Me.State.MyBO.Id, issueId, Me.State.MyBO.Certificate.Id, True)
                            End Try
                        End If
                    End If
                End If
            End If


            'user story 192764 - Task-199011--End------
            Me.State.MyBO.Validate()
            'save all the information
            Me.State.MyBO.Save()

            If IsCoverageForTheft = True Then
                With Me.State.MyBO
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

            If Not Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING AndAlso Not Me.State.MyBO.ServiceCenterObject Is Nothing AndAlso Not Me.State.MyBO.ServiceCenterObject.IntegratedWithGVS AndAlso
            Me.State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY AndAlso
           (Not blnExceedMaxReplacements) AndAlso blnClaimReportedWithinPeriod AndAlso blnClaimReportedWithinGracePeriod AndAlso blnCoverageTypeNotMissing AndAlso Me.State.MyBO.Certificate.IsSubscriberStatusValid Then
                'Call The Create Service Order Use Case
                If Not Me.State.MyBO.IsSupervisorAuthorizationRequired() AndAlso
                    ((Me.State.PoliceReportBO Is Nothing) OrElse
                     (Not Me.State.PoliceReportBO Is Nothing AndAlso Not Me.State.PoliceReportBO.IsEmpty)) Then

                    If Me.TextboxRepairdate.Visible = True And (Not Me.State.MyBO.RepairDate Is Nothing) _
                        And Me.TextboxPickUpDate.Visible = True And (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                        ' for backend claim
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                    Else
                        If (bListPriceFound) Then
                            If Me.State.PayOutstandingPremium = False Then
                                Dim soController As New ServiceOrderController
                                Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(Me.State.MyBO)
                                Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = so
                            ElseIf (Me.State.oOutstandingPremAmt = 0 AndAlso Me.State.PayOutstandingPremium = True) Then
                                Dim soController As New ServiceOrderController
                                Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(Me.State.MyBO)
                                Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = so
                            Else
                                Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                            End If
                        Else
                            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                        End If
                    End If
                Else
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                End If
            End If


        Catch ex As Exception
            Me.State.MyBO.StatusCode = oldStatus
            Throw ex
        End Try
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = Me.State.MyBO

        If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified Then
            If Not blnCoverageTypeNotMissing Then
                Me.NavController.Navigate(Me, "create_claim", Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE)

            ElseIf Not blnClaimReportedWithinGracePeriod Then
                Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD)

            ElseIf blnExceedMaxReplacements Then
                Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED)

            Else

                If Me.TextboxRepairdate.Visible = True AndAlso (Not Me.State.MyBO.RepairDate Is Nothing) AndAlso Me.TextboxPickUpDate.Visible = True AndAlso (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                    ' for backend claim
                    Me.NavController.Navigate(Me, "create_claim_backend_pay_claim", Message.MSG_CLAIM_ADDED)
                Else
                    Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
                End If
            End If

        ElseIf blnExceedMaxReplacements Then
            Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_REPLACEMENT_EXCEEDED)

        ElseIf Not blnClaimReportedWithinPeriod Then
            Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_DENIED_CLAIM_NOT_REPORTED_WITHIN_PERIOD)

        Else

            If Me.TextboxRepairdate.Visible = True AndAlso (Not Me.State.MyBO.RepairDate Is Nothing) AndAlso Me.TextboxPickUpDate.Visible = True AndAlso (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                ' for backend claim
                Me.NavController.Navigate(Me, "create_claim_backend_pay_claim", Message.MSG_CLAIM_ADDED)
            Else
                Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
            End If
        End If
    End Sub

    'Sets the Service Center to the Default Service Center for Denied Claims.                                   
    Private Function SetSvcCenterToDefault() As Boolean
        Dim result As Boolean = True
        Dim oCountry As Country
        Dim defaultServCenter As ServiceCenter
        Dim errMsg As String
        Dim oCert As New Certificate(Me.State.MyBO.CertificateId)
        oCountry = New Country(oCert.AddressChild.CountryId)
        If Not (oCountry.DefaultScForDeniedClaims.Equals(Guid.Empty)) Then
            defaultServCenter = New ServiceCenter(oCountry.DefaultScForDeniedClaims)
            State.MyBO.ServiceCenterId = defaultServCenter.Id
        Else
            errMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_DEFAULT_SVC_CENTER_NOT_SETUP)
            Me.MasterPage.MessageController.AddErrorAndShow(errMsg)
            result = False
        End If

        Return result
    End Function

    Private Function CreateClaimDenialMessage(ByVal showWarning As Boolean, ByVal denialMessage As String, ByVal showContinue As Boolean) As String
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
        If Not Me.State.InputParameters Is Nothing Then
            If Me.State.InputParameters.RecoveryButtonClick = True Then
                Me.State.MyBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
                Me.State.MyBO.CauseOfLossId = Me.State.InputParameters.ClaimBO.CauseOfLossId
                Me.State.MyBO.SpecialInstruction = Me.State.InputParameters.ClaimBO.SpecialInstruction
                Me.State.MyBO.ProblemDescription = Me.State.InputParameters.ClaimBO.ProblemDescription
                Me.State.MyBO.MasterClaimNumber = Me.State.InputParameters.ClaimBO.ClaimNumber
                With Me.State.InputParameters
                    Me.State.MyBO.PrePopulate(.ServiceCenterId, .CertItemCoverageId, .ClaimBO.ClaimNumber, .DateOfLoss, .RecoveryButtonClick)
                End With
            End If
        End If
    End Sub

    Protected Sub GetDepreciationSchedule()
        Try
            Dim Cert As New Certificate(Me.State.MyBO.CertificateId)
            Dim certItemCoverage As New CertItemCoverage(Me.State.MyBO.CertItemCoverageId)
            Dim oContractId As Guid = Contract.GetContractID(Me.State.MyBO.CertificateId)
            'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
            Dim i As Integer
            dProductSalesDate = Me.GetDateFormattedString(Cert.ProductSalesDate.Value)
            If Cert.UseDepreciation.Equals(Me.State.yesId) Then
                nLiabilityLimit = certItemCoverage.LiabilityLimits.Value
                Me.PopulateControlFromBOProperty(Me.TextBoxNewLiabilityLimit, nLiabilityLimit)
                If nLiabilityLimit > 0 Then
                    dvDeprSchedule = Me.State.MyBO.GetDepreciationSchedule(oContractId)
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
            Dim oCerticate As Certificate = New Certificate(Me.State.MyBO.CertificateId)
            Dim certItemCvg As CertItemCoverage = New CertItemCoverage(Me.State.MyBO.CertItemCoverageId)
            Dim certItem As CertItem = New CertItem(certItemCvg.CertItemId)

            moProtectionEvtDtl.CustomerName = Me.State.MyBO.CustomerName
            moProtectionEvtDtl.DealerName = Me.State.MyBO.DealerName
            moProtectionEvtDtl.CallerName = Me.State.MyBO.CallerName
            moProtectionEvtDtl.ClaimNumber = Me.State.MyBO.ClaimNumber
            If Not Me.State.MyBO.LossDate Is Nothing Then
                moProtectionEvtDtl.DateOfLoss = Me.State.MyBO.LossDate.Value.ToString("dd-MMM-yyyy")
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

            moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(langId, Me.State.MyBO.StatusCode)
            If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            moProtectionEvtDtl.ClaimStatusCss = cssClassName

            If Not Me.State.MyBO.ClaimedEquipment Is Nothing Then
                moProtectionEvtDtl.ClaimedMake = Me.State.MyBO.ClaimedEquipment.Manufacturer
                moProtectionEvtDtl.ClaimedModel = Me.State.MyBO.ClaimedEquipment.Model
            Else
                moProtectionEvtDtl.ClaimedMake = String.Empty
                moProtectionEvtDtl.ClaimedModel = String.Empty
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub FillWizard(ByVal flow As String)

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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
            Me.NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.LastState = InternalStates.ConfirmBackOnError
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnCreateClaim_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateClaim_WRITE.Click
        Try

            If Me.State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                If CType(TextboxAuthorizedAmount.Text, Decimal) < 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If
            Else
                If CType(TextboxAuthorizedAmount.Text, Decimal) < 0 Then
                    If System.Math.Abs(CType(TextboxAuthorizedAmount.Text, Decimal)) >= 1000000 Then
                        'display error
                        ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    End If
                End If
            End If

            If LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(Authentication.LangId), Me.State.MyBO.ClaimSpecialServiceId) = Codes.YESNO_Y Then
                If CType(TextboxAuthorizedAmount.Text, Decimal) <= 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If
            End If

            Me.PopulateBOsFromForm()

            If Me.TextboxRepairdate.Visible = True And Me.TextboxPickUpDate.Visible = True Then
                If (Not Me.State.MyBO.RepairDate Is Nothing) And (Me.State.MyBO.PickUpDate Is Nothing) Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelPickUpDate)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PICKUP_DATE_IS_REQUIRED_ERR)
                End If

                If (Me.State.MyBO.RepairDate Is Nothing) And (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelRepairDate)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5)
                End If

                'if backend claim then invoice number must be entered
                If (Not Me.State.MyBO.RepairDate Is Nothing) And (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                    If (Me.State.MyBO.AuthorizationNumber Is Nothing) Then
                        ElitaPlusPage.SetLabelError(Me.LabelInvoiceNumber)
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                    End If
                End If

            End If

            If (Not Me.State.MyBO.RepairDate Is Nothing) And (Not Me.State.MyBO.PickUpDate Is Nothing) Then
                If (Me.State.MyBO.AuthorizationNumber Is Nothing) Then
                    ElitaPlusPage.SetLabelError(Me.LabelInvoiceNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                End If
            End If

            If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                If Me.State.MyBO.PolicyNumber Is Nothing Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelPolicyNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
                End If
            End If

            Me.State.MyBO.IsRequiredCheckLossDateForCancelledCert = True 'Always check loss date for Cancelled certificate

            'Me.State.MyBO.Validate()

            'Display a warning message if the claim not reported within grace period and coverage missing from certificate
            If State.MyBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.MyBO.Dealer.IsGracePeriodSpecified AndAlso State.MyBO.IsNew Then


                If State.coverageTypeforclaimMissingfromCertificate_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, True)
                        Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If

                If State.claimNotReportedWithinGracePeriod_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, True)
                        Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If

                If (State.MyBO.IsClaimReportedWithinGracePeriod(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate))) And
                   (State.MyBO.IsClaimReportedWithValidCoverage(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate))) Then
                    If State.maxReplacementExceed_MessageDisplayed = False Then
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, True)
                            Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                            Exit Sub
                        End If
                    End If
                End If

            Else

                If State.maxReplacementExceed_MessageDisplayed = False Then
                    If (Not State.MyBO.CheckSvcWrantyClaimControl() AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK) Then
                        If State.MyBO.IsMaxReplacementExceeded(State.MyBO.CertificateId, State.MyBO.LossDate.Value) Then
                            Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, True)
                            Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                            Exit Sub
                        End If
                    End If
                End If

                'Display a warning message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days 
                If State.claimNotReportedWithinPeriod_MessageDisplayed = False Then
                    If Not State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                        Not State.MyBO.IsClaimReportedWithinPeriod(State.MyBO.CertificateId, DateHelper.GetDateValue(State.MyBO.LossDate), DateHelper.GetDateValue(State.MyBO.ReportedDate)) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, True)
                        Me.DisplayMessage(denialMessage, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                        Exit Sub
                    End If
                End If
            End If

            If Me.State.MyBO.CertificateItem.IsEquipmentRequired Then
                Dim msgList As List(Of String) = New List(Of String)
                Dim flag As Boolean = True
                flag = flag And Me.State.MyBO.ClaimedEquipment.ValidateForClaimProcess(msgList)
                If Not Me.State.MyBO.EnrolledEquipment Is Nothing Then flag = flag And Me.State.MyBO.EnrolledEquipment.ValidateForClaimProcess(msgList)

                If Not flag Then
                    Me.MasterPage.MessageController.AddError(msgList.ToArray)
                    Exit Sub
                End If
            End If

            Me.CreateClaim()
            'End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDedCollContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDedCollContinue.Click

        If Not cboDedCollMethod.SelectedIndex > BLANK_ITEM_SELECTED Then
            Me.moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DED_COLL_METHD_REQD)
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            Me.RegisterStartupScript("Startup", x)
            Exit Sub
        Else
            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) AndAlso
                txtDedCollAuthCode.Text.Length <> CInt(Codes.DED_COLL_CR_AUTH_CODE_LEN) Then 'Allow exact length of Auth Code
                Me.moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTH_CODE_FOR_CC)
                Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                Me.RegisterStartupScript("Startup", x)
                Exit Sub
            End If
        End If

        Dim c As Comment
        Dim oldStatus As String = Me.State.MyBO.StatusCode
        Try
            'Populate the Method of Collection to the Claim and revalidate the Claim Object before Save
            Me.State.MyBO.DedCollectionMethodID = GetSelectedItem(cboDedCollMethod)
            If Me.txtDedCollAuthCode.Enabled = True Then
                Me.PopulateBOProperty(Me.State.MyBO, "DedCollectionCCAuthCode", Me.txtDedCollAuthCode)
            End If

            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_DEFFERED_COLL) Then
                Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                c = Me.State.MyBO.AddNewComment()
                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                c.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_NOT_COLLECTED)
            End If

            Me.State.MyBO.Validate()
            Me.State.MyBO.Save()
            'Assumption : No Service Orders are created for this Path
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, "create_claim", Message.MSG_CLAIM_ADDED)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.State.MyBO.StatusCode = oldStatus
            ' DEF-25037 Throw ex
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            modalMessageBox.Attributes.Add("style", "display: none")
        End Try

    End Sub

    Protected Sub cboDedCollMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDedCollMethod.SelectedIndexChanged
        Try
            If GetSelectedItem(cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) Then
                Me.txtDedCollAuthCode.Enabled = True
            Else
                Me.txtDedCollAuthCode.Text = ""
                Me.txtDedCollAuthCode.Enabled = False
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            Me.RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ButtonOverride_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOverride_Write.Click
        Try
            btnCreateClaim_WRITE_Click(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonUpdateClaim_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdateClaim_Write.Click
        Try
            btnCreateClaim_WRITE_Click(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonCancel_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel_Write.Click
        Try
            Me.PopulateBOsFromForm()
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CANCEL)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub NotifyAuthorizedAmountHasChanged()
        Try
            Me.PopulateBOProperty(Me.State.MyBO, "AuthorizedAmount", Me.TextboxAuthorizedAmount)

            If curMethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                If Me.State.MyBO.Deductible.Value = 0 Then
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                        If Not Me.State.MyBO.DeductiblePercent Is Nothing Then
                            If Me.State.MyBO.DeductiblePercent.Value > 0 Then
                                Me.State.MyBO.Calculate_deductible_if_by_percentage()
                            End If
                        End If
                    End If
                End If
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                    If Not Me.State.MyBO.DeductiblePercent Is Nothing Then
                        If Me.State.MyBO.DeductiblePercent.Value > 0 Then
                            Me.State.MyBO.Calculate_deductible_if_by_percentage()
                        End If
                    End If
                End If
            End If
            Me.PopulateControlFromBOProperty(Me.TextboxDeductible_WRITE, Me.State.MyBO.Deductible)
            Me.PopulateControlFromBOProperty(Me.TextBoxDiscount, Me.State.MyBO.DiscountAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxAssurantPays, Me.State.MyBO.AssurantPays)
            Me.PopulateControlFromBOProperty(Me.TextboxConsumerPays, Me.State.MyBO.ConsumerPays)
            Me.PopulateControlFromBOProperty(Me.TextboxDueToSCFromAssurant, Me.State.MyBO.DueToSCFromAssurant)
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
            If Me.State.MyBO.IsAuthorizationLimitExceeded Then
                Me.moMessageController.Clear()
                Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                          TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                          TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaim_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenyClaim_Write.Click
        Try
            Dim cert As New Certificate(Me.State.MyBO.CertificateId)
            Me.PopulateBOsFromForm()
            Me.State.MyBO.Validate()
            If Me.State.MyBO.LossDate.Value < cert.WarrantySalesDate.Value Then
                'display error
                ElitaPlusPage.SetLabelError(Me.LabelLossDate)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
            End If

            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, FlowEvents.EVENT_DENY)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComment.Click
        Try
            'START  DEF-2306 Displaying claim_id on elp_comment table
            Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENTS, New CommentListForm.Parameters(Me.State.MyBO.CertificateId, Me.State.MyBO.Id))
            'END    DEF-2306 Displaying claim_id on elp_comment table
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonCancelClaim_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelClaim.Click
        Try
            Me.PopulateBOsFromForm()
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CANCEL_CLAIM, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_ARE_YOU_SURE))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Button to unlock the claim if it was 
    ''' originally locked by the same user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlock.Click
        Try
            Me.State.MyBO.UnLock()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveClaimIssue(ByVal sender As Object, ByVal Args As EventArgs) Handles btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(Me.State.MyBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            claimIssue.SaveNewIssue(Me.State.MyBO.Id, New Guid(hdnSelectedIssueCode.Value), Me.State.MyBO.CertificateId, False)
            Me.State.ClaimIssuesView = Me.State.MyBO.GetClaimIssuesView()
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Me.RegisterStartupScript("SendReportError", sJavaScript)
    End Sub

#End Region

#Region "Navigation Control"
    Public Class StateControllerCancelClaim
        Implements IStateController

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            Dim claimBO As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
            claimBO.Cancel()
            claimBO.IsUpdatedComment = True
            claimBO.Save()
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class
#End Region

#Region "Handle Dropdown Events"

    Private Sub cboCauseOfLossId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCauseOfLossId.SelectedIndexChanged

        'Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        'Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

        Me.PopulateBOProperty(Me.State.MyBO, "CauseOfLossId", Me.cboCauseOfLossId)
        With Me.State.MyBO
            If Not .CauseOfLossId = Guid.Empty Then
                .ClaimSpecialServiceId = Me.State.MyBO.GetSpecialServiceValue()
            Else
                .ClaimSpecialServiceId = Me.State.noId
            End If

            EnableDisablePriceFieldsforSplSvc()
        End With

    End Sub

#End Region

#Region "REQ-784 : Use Ship Address"
    Private Sub cboUseShipAddress_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUseShipAddress.SelectedIndexChanged

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cboUseShipAddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True

            'REQ-1153
            If Me.State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
                Me.State.MyBO.AddContactInfo(Nothing)
                Me.State.MyBO.ContactInfo.Address.CountryId = Me.State.MyBO.Company.CountryId
                Me.State.MyBO.ContactInfo.SalutationId = Me.State.MyBO.Company.SalutationId

                Me.UserControlAddress.NewClaimBind(Me.State.MyBO.ContactInfo.Address)
                Me.UserControlContactInfo.NewClaimBind(Me.State.MyBO.ContactInfo)
            Else
                Me.UserControlAddress.ClaimDetailsBind(Me.State.MyBO.ContactInfo.Address)
                Me.UserControlContactInfo.Bind(Me.State.MyBO.ContactInfo)

            End If

        Else
            moUserControlContactInfo.Visible = False

            'REQ-1153
            If Me.State.MyBO.ContactInfo.IsNew Then
                If Not Me.State.MyBO.ContactInfo Is Nothing Then
                    Me.State.MyBO.ContactInfo.Delete()
                End If

                If Not Me.State.MyBO.ContactInfo.Address Is Nothing Then
                    Me.State.MyBO.ContactInfo.Address.Delete()
                End If

                If Not Me.State.MyBO.ContactInfoId = System.Guid.Empty Then
                    Me.State.MyBO.ContactInfoId = System.Guid.Empty
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
            If Not Me.UserControlMessageController.Controls.Contains(lbl) Then
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

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.PageSize = Me.State.PageSize
        'Me.ValidSearchResultCountNew(Me.State.ClaimIssuesView.Count, True)
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
        ''''Dont consider the section below if the claim status is DENIED
        If Not Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED Then
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
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
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
                If (dvRow(Claim.ClaimIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__RESOLVED Or
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
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM_ISSUE_ID) = Me.State.SelectedClaimIssueId
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT, New ClaimIssueDetailForm.Parameters(Me.State.MyBO, Me.State.SelectedClaimIssueId))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
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

#Region "Claim Case Grid Related Functions"

    Public Sub PopulateClaimActionGrid()

        Try
            If (Me.State.ClaimActionListDV Is Nothing) Then
                Me.State.ClaimActionListDV = CaseAction.GetClaimActionList(Me.State.MyBO.Claim_Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            If State.ClaimActionListDV.Count = 0 Then
                Me.lblClaimActionRecordFound.Text = Me.State.ClaimActionListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.ClaimActionGrid.DataSource = Me.State.ClaimActionListDV
                Me.State.ClaimActionListDV.Sort = Me.State.SortExpression
                HighLightSortColumn(Me.ClaimActionGrid, Me.State.SortExpression, Me.IsNewUI)
                Me.ClaimActionGrid.DataBind()
                Me.lblClaimActionRecordFound.Text = Me.State.ClaimActionListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, ClaimActionGrid, True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateQuestionAnswerGrid()
        Try

            If (Me.State.CaseQuestionAnswerListDV Is Nothing) Then
                Me.State.CaseQuestionAnswerListDV = CaseQuestionAnswer.getClaimCaseQuestionAnswerList(Me.State.MyBO.Claim_Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            lblQuestionRecordFound.Visible = True

            If State.CaseQuestionAnswerListDV.Count = 0 Then
                Me.lblQuestionRecordFound.Text = Me.State.CaseQuestionAnswerListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.CaseQuestionAnswerGrid.DataSource = Me.State.CaseQuestionAnswerListDV
                Me.State.CaseQuestionAnswerListDV.Sort = Me.State.SortExpression
                HighLightSortColumn(Me.CaseQuestionAnswerGrid, Me.State.SortExpression, Me.IsNewUI)
                Me.CaseQuestionAnswerGrid.DataBind()
                Me.lblQuestionRecordFound.Text = Me.State.CaseQuestionAnswerListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, True)

        Catch ex As Exception
            Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, False)
                lblQuestionRecordFound.Visible = False
            End If
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Me.moExtendedStatusAgingRepeater.DataSource = claimAgingDetailsDV
        Me.moExtendedStatusAgingRepeater.DataBind()
        If HandleNoRow Then
            Me.moExtendedStatusAgingRepeater.Items(0).Visible = False
        End If
    End Sub

#End Region

#Region "Claim Image Related Grid"

    Private Sub AddImageButton_Click(sender As Object, e As EventArgs) Handles AddImageButton.Click
        Try
            Dim valid As Boolean = True
            If (Me.DocumentTypeDropDown.SelectedIndex = -1) Then
                Me.MasterPage.MessageController.AddError("DOCUMENT_TYPE_IS_REQUIRED")
                valid = False
            End If

            If (Me.ImageFileUpload.Value Is Nothing) OrElse
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

            Me.State.MyBO.AttachImage(
                New Guid(Me.DocumentTypeDropDown.SelectedValue),
                Nothing,
                DateTime.Now,
                fileName,
                Me.CommentTextBox.Text,
                ElitaPlusIdentity.Current.ActiveUser.UserName,
                fileData)
            If Not Me.State.MyBO.IsNew Then

            End If
            Me.State.ClaimImagesView = Nothing
            Me.ClearForm()
            Me.PopulateClaimImagesGrid()
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            ' Remove Mandatory Fields Validations for Hash, File Type and File Name
            Dim removeProperties As String() = New String() {"FileType", "FileName", "HashValue"}
            Dim newException As BOValidationException =
                New BOValidationException(
                    ex.ValidationErrorList().Where(Function(ve) (Not ((ve.Message = Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR) AndAlso (removeProperties.Contains(ve.PropertyName))))).ToArray(),
                    ex.BusinessObjectName,
                    ex.UniqueId)
            Me.HandleErrors(newException, Me.MasterPage.MessageController)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Private Sub ClearForm()
        Me.PopulateControlFromBOProperty(Me.DocumentTypeDropDown, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER))
        Me.PopulateControlFromBOProperty(Me.ScanDateTextBox, GetLongDateFormattedString(DateTime.Now))
        Me.CommentTextBox.Text = String.Empty
    End Sub
    Private Sub GridClaimImages_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (Not e.Row.Cells(0).FindControl("btnImageLink") Is Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    ' btnLinkImage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte()))

                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), Me.State.MyBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
                End If

                If (dvRow(Claim.ClaimImagesView.COL_STATUS_CODE).ToString = Codes.CLAIM_IMAGE_PROCESSED) Then
                    e.Row.Cells(3).CssClass = "StatActive"
                Else
                    e.Row.Cells(3).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridClaimImages.RowCommand
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
                Me.RegisterStartupScript("Startup", x)
            End If
        End If
    End Sub

    Public Sub PopulateClaimImagesGrid()
        If Me.State.ClaimImagesView Is Nothing Then
            Me.State.ClaimImagesView = Me.State.MyBO.GetClaimImagesView
        End If
        'work queue image
        Me.GridClaimImages.DataSource = Me.State.ClaimImagesView
        Me.GridClaimImages.DataBind()
    End Sub

    Private Sub GridClaimImages_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridClaimImages.Sorting
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
            Me.State.ClaimImagesView.Sort = Me.State.SortExpression
            Me.PopulateClaimImagesGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GridClaimImages_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridClaimImages.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.SelectedClaimIssueId = Guid.Empty
            PopulateClaimImagesGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimImages_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridClaimImages.PageIndexChanging
        Try
            GridClaimImages.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaimImages.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

