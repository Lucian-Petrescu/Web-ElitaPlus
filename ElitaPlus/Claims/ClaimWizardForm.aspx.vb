Imports BO = Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Threading
Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System.Xml.Xsl
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports SysWebUICtls = System.Web.UI.WebControls
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.Elita.ClientIntegration
Imports System.IO
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimService
Imports System.ServiceModel

Public Class ClaimWizardForm
    Inherits ElitaPlusSearchPage
#Region "Web Method"

    <WebMethod(), ScriptMethod()>
    Public Shared Function LoadSku(manufacturerId As String, model As String, dealerId As String) As String

        Dim dealer As New Dealer(New Guid(dealerId))
        Dim equipmentId As Guid = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Now,
                                                                          New Guid(manufacturerId), model)
        If equipmentId.Equals(Guid.Empty) Then Return Nothing


        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim lstSkuNumbers As List(Of String)
        Dim skuNumberJSONArray As String

        Dim dv As DataView = CertItem.LoadSku(equipmentId, dealer.Id)

        If dv IsNot Nothing Then
            lstSkuNumbers = New List(Of String)

            For Each row As DataRowView In dv
                lstSkuNumbers.Add(CType(row(0), String))
            Next
        End If
        skuNumberJSONArray = serializer.Serialize(lstSkuNumbers)

        Return skuNumberJSONArray

    End Function

#End Region
#Region "Constants"
    Private Const NO_DATA As String = " - "
    Public Const URL As String = "~/Claims/ClaimWizardForm.aspx"
    Public Const AbsoluteURL As String = "/ElitaPlus/Claims/ClaimWizardForm.aspx"
    Private Const PROTECTION_AND_EVENT_DETAILS As String = "PROTECTION_AND_EVENT_DETAILS"
    Private Const CLAIM_WIZARD_FROM_EVENT_DETAILS As String = "CLAIM_WIZARD_FROM_EXISTING_CLAIM"
    Private Const CERTIFICATES As String = "CERTIFICATES"
    Private Const VSCCode As String = "2"
    Public Const CLOSED As String = "C"
    Public Const ClaimsManager As String = "CLAMM"
    Public Const OfficeManager As String = "OFICM"
    Public Const IHQSUPPORT As String = "IHQSU"
    Public Const CallCenterAgent As String = "CCA"
    Public Const CallCenterSupervisor As String = "CCS"
    Public Const Claims As String = "CLAIM"
    Public Const ClaimsAnalyst As String = "CLMAN"
    Public Const ClaimSupport As String = "CLMSP"
    Public Const Comments As String = "COMMT"
    Public Const CSR As String = "CSR"
    Public Const CSR2 As String = "CSR2"
    Public Const CountySuperUser As String = "SUSER"
    Public Const Active As String = "A"
    Public Const Manufacture As String = "M"
    Public Const PDF_URL As String = "DisplayPdf.aspx?ImageId="
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 1
    Public Const GRID_COL_PROCESSED_DATE_IDX As Integer = 3
    Public Const GRIDCLA_COL_STATUS_CODE_IDX As Integer = 7
    Public Const GRIDCLA_COL_AMOUNT_IDX As Integer = 2
    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 5
    Public Const CaseQuestionAnswerGridColQuestionIdx As Integer = 2
    Public Const CaseQuestionAnswerGridColAnswerIdx As Integer = 3
    Public Const CaseQuestionAnswerGridColCreationDateIdx As Integer = 4
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const SELECT_ACTION_IMAGE As String = "SelectActionImage"
    Public Const ATTRIB_SRC As String = "src"
    Public Const GRIDMC_COL_MSTR_CLAIM_IDX As Integer = 1
    Public Const GRIDMC_COL_CLAIM_NUMBER_IDX As Integer = 2
    Public Const GRIDMC_COL_DATE_OF_LOSS_IDX As Integer = 3
    Public Const gridClaimCaseDeviceInfoPurchasedDate As Integer = 3
    Private Const LABEL_SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Const SESSION_KEY_CLAIM_WIZARD_BACKUP_STATE As String = "SESSION_KEY_CLAIM_WIZARD_BACKUP_STATE"
    Public Const ISSUE_CODE_CR_DEVICE_MIS As String = "CR_DEVICE_MIS"

#End Region

#Region "Enums"

    Public Enum ClaimWizardSteps
        <Description("Incident Information")>
        Step1 = 1
        <Description("Coverage Infomation")>
        Step2 = 2
        <Description("Claim Details")>
        Step3 = 3
        <Description("Locate Service Center")>
        Step4 = 4
        <Description("Comments")>
        Step5 = 5
        <Description("None")>
        None
    End Enum

#End Region

#Region "Page State"
    Class MyState
        Public StepName As ClaimWizardSteps
        Public EntryStep As ClaimWizardSteps
        Public CertBO As Certificate
        Public CertItemCoverageBO As CertItemCoverage
        Public CertItemBO As CertItem
        Public ClaimBO As MultiAuthClaim
        Public ClaimId As Guid
        Public DealerBO As Dealer
        Public CompanyBO As Company
        Public CommentBO As Comment
        Public VSCModel As VSCModel
        Public VSCClassCode As VSCClassCode
        Public InputParameters As Parameters
        Public DateOfLoss As Date
        Public DateReported As Date
        Public CertficateCoverageTypeId As Guid = Guid.Empty
        Public RiskTypeId As Guid = Guid.Empty
        Public CallerName As String
        Public ProblemDescription As String
        Public IsEditMode As Boolean = False
        Public DoesActiveTradeInExistForIMEI As Boolean = False
        Public MasterClaimNumber As String = String.Empty
        Friend yesId As Guid
        Friend noId As Guid
        Friend IsSalutation As Boolean = False
        Friend PageSize As Integer = 5
        Friend IsGridVisible As Boolean = True
        Friend SortExpression As String = Claim.ClaimIssuesView.COL_CREATED_DATE & " DESC"
        Friend PageIndex As Integer = 0
        Friend SelectedClaimIssueId As Guid
        Friend ClaimIssuesView As Claim.ClaimIssuesView
        Friend ClaimImagesView As Claim.ClaimImagesView
        Friend PoliceReportBO As PoliceReport = Nothing

        Friend DEDUCTIBLE_BASED_ON As String
        Friend PayOutstandingPremium As Boolean
        Friend LocateServiceCenterSearchType As LocateServiceCenterSearchType = ClaimWizardForm.LocateServiceCenterSearchType.ByZip
        Friend SelectedServiceCenterId As Guid
        Friend ServiceCenterView As DataView

        'to be used in step 2 only -> onwards shuld be used from Claimedequipment Property of Claim BO
        Friend step2_claimEquipmentBO As ClaimEquipment = Nothing
        Friend IsClaimDenied As Boolean = False

        Friend default_service_center_id As Guid

        Public CaseQuestionAnswerListDV As CaseQuestionAnswer.CaseQuestionAnswerDV = Nothing
        Public ClaimActionListDV As CaseAction.CaseActionDV = Nothing
        Public ClaimCaseDeviceInfoDV As DataView = Nothing

        Public IsCallerAuthenticated As Boolean = False
    End Class

    Public Enum LocateServiceCenterSearchType
        ByZip
        ByCity
        All
        None
    End Enum

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Dim retState As MyState = CType(MyBase.State, MyState)
            Session(SESSION_KEY_CLAIM_WIZARD_BACKUP_STATE) = retState
            Return retState
        End Get
    End Property

    Private Sub Page_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
        Try
            If callFromUrl.Contains(ClaimRecordingForm.Url2) Then
                MyBase.SetPageOutOfNavigation()
            End If
            If CallingParameters IsNot Nothing Then
                State.InputParameters = CType(CallingParameters, Parameters)
                State.StepName = State.InputParameters.StepNumber
                State.EntryStep = State.StepName
                State.IsCallerAuthenticated = State.InputParameters.IsCallerAuthenticated
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        If (CalledUrl = ClaimIssueDetailForm.URL OrElse CalledUrl = ClaimDeductibleRefundForm.URL) Then

            If (Not State.ClaimBO.Id.Equals(Guid.Empty)) Then
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.ClaimBO.Id)
                If (State.ClaimBO IsNot Nothing) Then

                    If (Me.State.ClaimBO.Status = BasicClaimStatus.Active OrElse Me.State.ClaimBO.Status = BasicClaimStatus.Denied) Then
                        '//TO-DO: Navigate to Claim Details page (ClaimForm.aspx)                        
                        callPage(ClaimForm.URL, New ClaimForm.Parameters(State.ClaimBO.Id, State.IsCallerAuthenticated))
                    End If
                End If
            End If
        End If

    End Sub
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Certificate
        Public BoChanged As Boolean = False
        Public IsCallerAuthenticated As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Parameters"
    Public Class Parameters

        Public StepNumber As ClaimWizardSteps = ClaimWizardSteps.None
        Public CertificateId As Guid = Nothing
        Public ShowWizard As Boolean = False
        Public ComingFromDenyClaim As Boolean = False
        Public ClaimBo As ClaimBase = Nothing
        Public claimId As Guid = Guid.Empty
        Public IsCallerAuthenticated As Boolean = False

        Public Sub New(stepNumber As ClaimWizardSteps, certificateId As Guid, claimId As Guid, claim As ClaimBase, Optional ByVal showWizard As Boolean = False, Optional ByVal comingFromDenyClaim As Boolean = False, Optional IsCallerAuthenticated As Boolean = False)
            Me.StepNumber = stepNumber
            Me.CertificateId = certificateId
            Me.ShowWizard = showWizard
            Me.ComingFromDenyClaim = comingFromDenyClaim
            ClaimBo = claim
            Me.claimId = claimId
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub

    End Class
#End Region

#Region "Properties"
    Protected WithEvents moUserControlAddress As UserControlAddress_New
    Public ReadOnly Property UserControlAddress() As UserControlAddress_New
        Get
            If moUserControlAddress Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
            End If
            Return moUserControlContactInfo
        End Get
    End Property

    Protected WithEvents MessageController As MessageController
    Public ReadOnly Property UserControlMessageController() As MessageController
        Get
            If MessageController Is Nothing Then
                MessageController = DirectCast(MasterPage.MessageController, MessageController)
            End If
            Return MessageController
        End Get
    End Property
#End Region

#Region "PageEvents"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ClearMessageControllers()
            If (Not IsPostBack) Then
                btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancel.UniqueID))
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                btnModalCancelClaimYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancelClaim.UniqueID))
                lblModalClaimCancel.Text = TranslationBase.TranslateLabelOrMessage("ARE_YOU_SURE_CANCEL_CLAIM")
                headerDeviceInfo.InnerText = TranslationBase.TranslateLabelOrMessage("DEVICE_INFORMATION")

                step2_ddlClaimedManuf.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", step2_ddlClaimedManuf.ClientID, step2_txtClaimedModel.ClientID, step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))
                step2_txtClaimedModel.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", step2_ddlClaimedManuf.ClientID, step2_txtClaimedModel.ClientID, step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))
                step2_ddlClaimedSku.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))

                PopulateStepUIandData()

            End If
            BindBoPropertiesToLabels(State.StepName)
            PopulateClaimedEnrolledDetails()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub step1_btnSearch_Click(sender As Object, e As System.EventArgs) Handles step1_btnSearch.Click
        Try
            If ValidateInputForStep1BtnSearch() Then
                State.DateOfLoss = DateHelper.GetDateValue(step1_moDateOfLossText.Text)
                State.DateReported = DateHelper.GetDateValue(step1_txtDateReported.Text)
                State.IsEditMode = True
                PopulateDropDowns(State.StepName)
                HandleButtons(State.StepName)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub step1_cboRiskType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles step1_cboRiskType.SelectedIndexChanged
        Try
            If Not step1_cboRiskType.SelectedValue.Equals(Guid.Empty.ToString) Then
                State.RiskTypeId = New Guid(step1_cboRiskType.SelectedValue)
                ' Me.BindListControlToDataView(Me.step1_cboCoverageType, LookupListNew.LoadCoverageTypes(Me.State.CertBO.Id, New Guid(step1_cboRiskType.SelectedValue), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                Dim listcontextForcoveragetypes As ListContext = New ListContext()
                listcontextForcoveragetypes.CertId = State.CertBO.Id
                listcontextForcoveragetypes.CertItemId = New Guid(step1_cboRiskType.SelectedValue)
                listcontextForcoveragetypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                listcontextForcoveragetypes.DateOfLoss = State.DateOfLoss

                Dim CoverageTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForcoveragetypes)
                step1_cboCoverageType.Populate(CoverageTypeList, New PopulateOptions() With
                                                  {
                                                  .AddBlankItem = True
                                                  })
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If (State.StepName = State.EntryStep) Then
                ReturnBackToCallingPage()
            Else
                State.StepName = LastStep()
                PopulateStepUIandData()
                State.IsEditMode = False
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ReturnBackToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Shared Function GetClientClaimService() As ClaimServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_SERVICE), False)
        Dim client = New ClaimServiceClient("CustomBinding_IClaimService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password

        'Dim client = New ClaimServiceClient("CustomBinding_IClaimService", "http://localhost/ElitaClaimService/ClaimService.svc")
        'client.ClientCredentials.UserName.UserName = "elita1"
        'client.ClientCredentials.UserName.Password = "elita1"
        Return client
    End Function

    Protected Sub btn_Continue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click, btnClaimOverride_Write.Click

        Try
            PopulateBOFromForm(State.StepName)
            ClearMessageControllers()


            If (ValidateInputs(State.StepName)) Then

                Select Case State.StepName
                    Case ClaimWizardSteps.Step1

                    Case ClaimWizardSteps.Step2
                        Dim msg As String
                        If State.CertItemCoverageBO.IsPossibleWarrantyClaim(msg) Then
                            lblServiceWarrantyMessage.Text = TranslationBase.TranslateLabelOrMessage(msg)
                            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceWarranty'); </script>"
                            RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                        If ((State.CertBO.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC Or State.CertBO.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso
                            State.CertItemCoverageBO.GetAllClaims(State.CertItemCoverageBO.Id).Count > 0) Then
                            PopulateMasterClaimGrid()
                            Dim x As String = "<script language='JavaScript'> revealModal('ModalMasterClaim'); </script>"
                            RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                    Case ClaimWizardSteps.Step3
                        CreateClaim()
                        If (State.DealerBO.DeductibleCollectionId = State.yesId) Then
                            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                            RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                    Case ClaimWizardSteps.Step4
                        ' REQ- 6156 - Skipping this step of choosing Service Center and copied the required code to step 5

                        For Each claimAuth As ClaimAuthorization In State.ClaimBO.ClaimAuthorizationChildren
                            'below line is to skip voiding or deleting authorizations if they are of claim deductible refund related
                            If Not claimAuth.IsAuthorizationDeductibleRefund Then
                                If Not claimAuth.IsNew Then
                                    claimAuth.Void()
                                Else
                                    claimAuth.DeleteChildren()
                                    claimAuth.Delete()
                                End If
                            End If
                        Next
                        ' REQ- 6156 - Call Start Fulfillment process web method in Fulfillment Web Service
                        'Me.State.ClaimBO.AddClaimAuthorization(Me.State.SelectedServiceCenterId)

                        State.DoesActiveTradeInExistForIMEI = DoesAcceptedOfferExistForIMEI()
                        If State.ClaimBO.IsNew And State.DoesActiveTradeInExistForIMEI Then
                            State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                            State.CommentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                        End If
                    Case ClaimWizardSteps.Step5
                        ' Bug 178224 - REQ- 6156 
                        Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                        If attvalue IsNot Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                            If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                                For Each claimAuth As ClaimAuthorization In State.ClaimBO.ClaimAuthorizationChildren
                                    'below line is to skip voiding or deleting authorizations if they are of claim deductible refund related
                                    If Not claimAuth.IsAuthorizationDeductibleRefund Then
                                        If Not claimAuth.IsNew Then
                                            claimAuth.Void()
                                        Else
                                            claimAuth.DeleteChildren()
                                            claimAuth.Delete()
                                        End If
                                    End If
                                Next
                                State.DoesActiveTradeInExistForIMEI = DoesAcceptedOfferExistForIMEI()
                            End If
                        End If
                        If State.ClaimBO.IsNew And State.DoesActiveTradeInExistForIMEI Then
                            State.ClaimBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__ACTIVE_TRADEIN_QUOTE_EXISTS)
                            State.ClaimBO.DenyClaim()
                        End If

                        If State.ClaimBO IsNot Nothing AndAlso Me.State.ClaimBO.Status = BasicClaimStatus.Active Then
                            'user story 192764 - Task-199011--Start------
                            Dim dsCaseFields As DataSet = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            If (dsCaseFields IsNot Nothing AndAlso dsCaseFields.Tables.Count > 0 AndAlso dsCaseFields.Tables(0).Rows.Count > 0) Then

                                Dim hasBenefit As DataRow() = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                                Dim benefitCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='BENEFITCHECKERROR'")
                                Dim preCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='PRECHECKERROR'")
                                Dim lossType As DataRow() = dsCaseFields.Tables(0).Select("field_code='LOSSTYPE'")

                                If hasBenefit IsNot Nothing AndAlso hasBenefit.Length > 0 Then
                                    If hasBenefit(0)("field_value") IsNot Nothing AndAlso String.Equals(hasBenefit(0)("field_value").ToString(), Boolean.FalseString, StringComparison.CurrentCultureIgnoreCase) Then
                                        UpdateCaseFieldValues(hasBenefit, lossType)

                                        dsCaseFields = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                        hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                                    End If
                                End If
                                If benefitCheckError IsNot Nothing AndAlso benefitCheckError.Length > 0 Then
                                    If benefitCheckError(0)("field_value") IsNot Nothing AndAlso Not String.Equals(benefitCheckError(0)("field_value").ToString(), "NO ERROR", StringComparison.CurrentCultureIgnoreCase) Then
                                        UpdateCaseFieldValues(benefitCheckError, lossType)

                                        dsCaseFields = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
                            'user story 192764 - Task-199011--End------
                        End If

                        'User Story 186561 Number of active claims allowed under one certificate -- start
                        Dim blnClaimInProgress As Boolean = False
                        For Each i As ClaimIssue In State.ClaimBO.ClaimIssuesList
                            If i.IssueCode = "TIC_IP" Then
                                blnClaimInProgress = True
                                Exit For
                            End If
                        Next
                        If blnClaimInProgress Then
                            Try
                                ' re-validate the claim limit and claim count
                                Dim wsRequest As NewClaimEntitledRequest = New NewClaimEntitledRequest()

                                wsRequest.DealerCode = State.ClaimBO.Dealer.Dealer
                                wsRequest.CertificateNumber = State.ClaimBO.Certificate.CertNumber
                                wsRequest.LossDate = State.ClaimBO.LossDate.Value
                                wsRequest.CoverageTypeCode = State.ClaimBO.CoverageTypeCode

                                Dim wsResponse As NewClaimEntitledResponse
                                wsResponse = WcfClientHelper.Execute(Of ClaimServiceClient, IClaimService, NewClaimEntitledResponse)(
                                    GetClientClaimService(),
                                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                    Function(c As ClaimServiceClient)
                                        Return c.NewClaimEntitled(wsRequest)
                                    End Function)

                                If wsResponse.IsNewClaimEntitled = False Then 'deny the claim with the denial reason returned
                                    State.ClaimBO.Status = BasicClaimStatus.Denied
                                    State.ClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, wsResponse.DenialCodes(0))
                                End If
                            Catch ex As FaultException
                                Log(ex)
                            Catch ex As Exception
                                Log(ex)
                            End Try
                        End If
                        'User Story 186561 Number of active claims allowed under one certificate -- end

                        State.CommentBO.Save()
                        State.ClaimBO.Save()

                        ' Create Authorization
                        If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                            Dim blnWsSuccess As Boolean = True
                            Try
                                blnWsSuccess = CallStartFulfillmentProcess()
                                If Not blnWsSuccess Then
                                    State.ClaimBO.Status = BasicClaimStatus.Pending
                                    State.ClaimBO.Save()
                                    Exit Sub
                                End If
                            Catch ex As Exception
                                State.ClaimBO.Status = BasicClaimStatus.Pending
                                State.ClaimBO.Save()
                                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
                                Throw
                            End Try
                        End If

                        'TO DO : Add logic to Send Emails and other stuff
                        ReturnBackToCallingPage()
                        Exit Sub
                End Select

                GoToNextStep()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Shared Sub UpdateCaseFieldValues(ByRef caseFieldRow As DataRow(), ByRef lossType As DataRow())
        Dim caseFieldXcds() As String
        Dim caseFieldValues() As String

        If lossType IsNot Nothing AndAlso lossType.Length > 0 Then
            If lossType(0)("field_value") IsNot Nothing AndAlso (lossType(0)("field_value").ToString().ToUpper() = "ADH1234" Or lossType(0)("field_value").ToString().ToUpper() = "ADH5") Then
                caseFieldXcds = {"CASEFLD-HASBENEFIT", "CASEFLD-ADCOVERAGEREMAINING"}
                caseFieldValues = {Boolean.TrueString.ToUpper(), Boolean.TrueString.ToUpper()}
            ElseIf lossType(0)("field_value") IsNot Nothing AndAlso lossType(0)("field_value").ToString().ToUpper() = "THEFT/LOSS" Then
                caseFieldXcds = {"CASEFLD-HASBENEFIT"}
                caseFieldValues = {Boolean.TrueString.ToUpper()}
            End If
        End If

        CaseBase.UpdateCaseFieldValues(GuidControl.ByteArrayToGuid(caseFieldRow(0)("case_Id")), caseFieldXcds, caseFieldValues)
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
                State.ClaimBO.Status = If(benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Approve, BasicClaimStatus.Active, BasicClaimStatus.Pending)
                If (benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Deny) Then
                    Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECKFAIL")
                    Dim newClaimIssue As ClaimIssue = CType(State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(State.ClaimBO.Id, issueId, State.ClaimBO.Certificate.Id, True)
                End If
            Else
                State.ClaimBO.Status = BasicClaimStatus.Pending
                Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                Dim newClaimIssue As ClaimIssue = CType(State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                newClaimIssue.SaveNewIssue(State.ClaimBO.Id, issueId, State.ClaimBO.Certificate.Id, True)
            End If
        Catch ex As FaultException
            Log(ex)
        Catch ex As Exception
            Log(ex)
            State.ClaimBO.Status = BasicClaimStatus.Pending
            Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
            Dim newClaimIssue As ClaimIssue = CType(State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            newClaimIssue.SaveNewIssue(State.ClaimBO.Id, issueId, State.ClaimBO.Certificate.Id, True)
        End Try
    End Sub

    'Protected Sub BtnVerifyEquipment_WRITE_Click(sender As Object, e As EventArgs) Handles BtnVerifyEquipment_WRITE.Click
    '    Try
    '        'verify equipment if found then update description, equipment id and enrolled and claimed equipment
    '        If Me.State.CertItemBO.VerifyEquipment() Then
    '            Me.MasterPage.MessageController.AddSuccess(Codes.EQUIPMENT_VERIFIED, True)
    '            ControlMgr.SetVisibleControl(Me, BtnVerifyEquipment_WRITE, False)
    '            ControlMgr.SetVisibleControl(Me, Me.btnContinue, Not Me.State.IsEditMode)
    '            If Not Me.State.CertItemBO.EquipmentId.Equals(Guid.Empty) Then
    '                Me.State.step2_claimEquipmentBO = Me.State.CertItemBO.CopyEnrolledEquip_into_ClaimedEquip()
    '                Me.PopulateFormfromClaimedEquipmentBO()
    '            End If
    '        Else
    '            Me.moMessageController.AddError(Codes.EQUIPMENT_NOT_FOUND, True)
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

    Protected Sub btnaddNewMasterClaimNumber_Click(sender As Object, e As EventArgs) Handles btnAddNewMasterClaim.Click
        Try
            GoToNextStep()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDedCollContinue_Click(sender As Object, e As System.EventArgs) Handles btnDedCollContinue.Click

        If Not step3_cboDedCollMethod.SelectedIndex > BLANK_ITEM_SELECTED Then
            moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DED_COLL_METHD_REQD)
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            RegisterStartupScript("Startup", x)
            Exit Sub
        Else
            If GetSelectedItem(step3_cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) AndAlso
               step3_txtDedCollAuthCode.Text.Length <> CInt(Codes.DED_COLL_CR_AUTH_CODE_LEN) Then 'Allow exact length of Auth Code
                moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTH_CODE_FOR_CC)
                Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                RegisterStartupScript("Startup", x)
                Exit Sub
            End If
        End If

        Dim c As Comment
        Dim oldStatus As String = State.ClaimBO.StatusCode
        Try
            PopulateBOFromForm(State.StepName)
            State.ClaimBO.Validate()
            State.ClaimBO.CheckForRules()
            GoToNextStep()
        Catch ex As Exception
            State.ClaimBO.StatusCode = oldStatus
            Throw ex
        End Try

    End Sub

    Protected Sub cboDedCollMethod_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles step3_cboDedCollMethod.SelectedIndexChanged

        If GetSelectedItem(step3_cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) Then
            step3_txtDedCollAuthCode.Enabled = True
        Else
            step3_txtDedCollAuthCode.Text = ""
            step3_txtDedCollAuthCode.Enabled = False
        End If
        Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
        RegisterStartupScript("Startup", x)
    End Sub

    Private Sub btnSearchServiceCenter_Click(sender As Object, e As System.EventArgs) Handles step4_btnSearch.Click
        Try
            State.PageIndex = 0
            State.SelectedServiceCenterId = Guid.Empty
            PopulateGridOrDropDown()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearchServiceCenter_Click(sender As Object, e As System.EventArgs) Handles step4_btnClearSearch.Click
        Try
            step4_TextboxCity.Text = ""
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByZip_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles step4_RadioButtonByZip.CheckedChanged
        Try
            If step4_RadioButtonByZip.Checked Then
                State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByZip
            End If
            EnableDisableWizardControls(ClaimWizardSteps.Step4)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByCity_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles step4_RadioButtonByCity.CheckedChanged
        Try
            If step4_RadioButtonByCity.Checked Then
                State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByCity
            End If
            EnableDisableWizardControls(ClaimWizardSteps.Step4)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles step4_RadioButtonAll.CheckedChanged
        Try
            EnableDisableWizardControls(ClaimWizardSteps.Step4)
            If step4_RadioButtonAll.Checked Then
                State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All
                Dim address As New Address(State.CertBO.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(GetSelectedItem(step4_moCountryDrop))
                PopulateCountryDropdown(SelectedCountry)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonNO_SVC_OPTION_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles step4_RadioButtonNO_SVC_OPTION.CheckedChanged
        Try
            EnableDisableWizardControls(ClaimWizardSteps.Step4)
            If step4_RadioButtonNO_SVC_OPTION.Checked Then
                State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.None
                'Dim address As New Address(Me.State.CertBO.AddressId)
                'Dim SelectedCountry As New ArrayList
                'SelectedCountry.Add(Me.GetSelectedItem(step4_moCountryDrop))
                'PopulateCountryDropdown(SelectedCountry)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moCountryDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles step4_moCountryDrop.SelectedIndexChanged
        Try
            If step4_RadioButtonAll.Checked Then
                EnableDisableWizardControls(ClaimWizardSteps.Step4)
                State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All

                Dim address As New Address(State.CertBO.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(GetSelectedItem(step4_moCountryDrop))

                PopulateCountryDropdown(SelectedCountry)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moMultipleColumnDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles step4_moMultipleColumnDrop.SelectedDropChanged
        Try
            PopulateGridOrDropDown()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnEdit(sender As Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
        Try
            State.IsEditMode = True
            EnableDisableWizardControls(State.StepName)
            HandleButtons(State.StepName)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            ClearMessageControllers()
            PopulateBOFromForm(State.StepName)
            ' Me.ClearMessageControllers()

            Select Case State.StepName
                Case ClaimWizardSteps.Step2
                    If (ValidateInputs(State.StepName)) Then
                        If State.CertItemBO.IsFamilyDirty OrElse State.CertItemCoverageBO.IsFamilyDirty _
                           OrElse (State.step2_claimEquipmentBO IsNot Nothing And State.step2_claimEquipmentBO.IsDirty) Then
                            If (State.CertItemBO.IsFamilyDirty OrElse State.CertItemCoverageBO.IsFamilyDirty) Then
                                If (State.CertItemCoverageBO.IsFamilyDirty) Then
                                    State.CertItemCoverageBO.Save()
                                End If
                                If (State.CertItemBO.IsFamilyDirty) Then
                                    State.CertItemBO.Save()
                                End If
                                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            End If
                            PopulateFormFromBO(State.StepName)
                            State.IsEditMode = False
                            EnableDisableWizardControls(State.StepName)
                        Else
                            If Not State.CertItemBO.IsEquipmentRequired Then
                                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED, True)
                            End If
                        End If
                    End If
            End Select
            HandleButtons(State.StepName)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            Select Case State.StepName
                Case ClaimWizardSteps.Step2
                    If Not State.CertItemBO.IsNew Then
                        State.CertItemBO = New CertItem(State.CertItemBO.Id)
                    Else
                        State.CertItemBO = New CertItem
                    End If
                    State.IsEditMode = False
                    PopulateFormFromBO(State.StepName)
                    EnableDisableWizardControls(State.StepName)
                    ValidateInputs(State.StepName)
            End Select
            HandleButtons(State.StepName)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonSoftQuestions_Click(sender As Object, e As System.EventArgs) Handles btnSoftQuestions.Click
        Try
            If tvQuestion.Nodes.Count = 0 Then
                PopulateSoftQuestionTree()
            End If
            ControlMgr.SetVisibleControl(Me, moCertificateInfoController, True)
            moCertificateInfoController.InitController(State.CertBO.Id, , State.CompanyBO.Code)
            Dim x As String = "<script language='JavaScript'> revealModal('ModalSoftQuestions') </script>"
            RegisterStartupScript("Startup", x)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveClaimIssue(sender As Object, Args As EventArgs) Handles step3_modalClaimIssue_btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            claimIssue.SaveNewIssue(State.ClaimBO.Id, New Guid(hdnSelectedIssueCode.Value), State.ClaimBO.CertificateId, False)
            State.ClaimIssuesView = State.ClaimBO.GetClaimIssuesView()
            PopulateClaimIssuesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaim_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnDenyClaim.Click
        Try
            PopulateBOFromForm(State.StepName)
            State.ClaimBO.Validate()
            If State.ClaimBO.LossDate.Value < State.ClaimBO.Certificate.WarrantySalesDate.Value Then
                ElitaPlusPage.SetLabelError(step3_LabelLossDate)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
            End If

            Dim x As String = "<script language='JavaScript'> revealModal('ModalDenyClaim') </script>"
            RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Assurant.ElitaPlus.BusinessObjectsNew.BOValidationException
            'Allow bypassing LossDate validation denying an expired item claim
            If ex.ValidationErrorList.Count = 1 And
               ex.ValidationErrorList(0).PropertyName = "LossDate" And
               (State.ClaimBO.LossDate.Value > State.ClaimBO.CertificateItemCoverage.EndDate.Value Or
                State.ClaimBO.LossDate.Value < State.ClaimBO.CertificateItemCoverage.BeginDate.Value) Then
                Dim bypassMdl As String = "<script language='JavaScript'> revealModal('ModalBypassDoL') </script>"
                RegisterStartupScript("BpQtn", bypassMdl)
            Else
                HandleErrors(ex, MasterPage.MessageController)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaimSave_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnDenyClaimSave.Click
        Try
            State.IsClaimDenied = True
            PopulateBOFromForm(State.StepName)
            If State.ClaimBO.DeniedReasonId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(step3_lblDeniedReason)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR)
            End If

            State.ClaimBO.Deductible = New DecimalType(0D)
            State.ClaimBO.VoidAuthorizations()
            State.ClaimBO.DenyClaim()
            State.ClaimBO.IsUpdatedComment = True
            GoToNextStep()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnModalBbDolYes_Click(sender As Object, e As EventArgs) Handles btnModalBbDolYes.Click
        Dim x As String = "<script language='JavaScript'>hideModal('ModalBypassDoL'); revealModal('ModalDenyClaim'); </script>"
        RegisterStartupScript("Startup", x)
        Dim DndId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__INCORRECT_DEVICE_SELECTED)
        step3_cboDeniedReason.SelectedIndex = step3_cboDeniedReason.Items.IndexOf(step3_cboDeniedReason.Items.FindByValue(DndId.ToString()))
        step3_cboDeniedReason.Items.FindByValue(step3_cboDeniedReason.SelectedValue).Selected = True
        step3_cboDeniedReason.Enabled = False
    End Sub

    Private Sub ButtonCancelClaim_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelClaim.Click
        Try
            PopulateBOFromForm(State.StepName)
            State.ClaimBO.Cancel()
            State.ClaimBO.IsUpdatedComment = True
            State.ClaimBO.Save()
            GoToNextStep()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboMethodOfRepair_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles step2_cboMethodOfRepair.SelectedIndexChanged
        Try
            PopulateFormDeductibleFormBOs(GetSelectedItem(step2_cboMethodOfRepair))
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboUseShipAddress_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles step3_cboUseShipAddress.SelectedIndexChanged
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If step3_cboUseShipAddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True

            If State.ClaimBO.ContactInfoId.Equals(Guid.Empty) Then
                State.ClaimBO.AddContactInfo(Nothing)
                State.ClaimBO.ContactInfo.Address.CountryId = State.ClaimBO.Company.CountryId
                State.ClaimBO.ContactInfo.SalutationId = State.ClaimBO.Company.SalutationId

                UserControlAddress.NewClaimBind(State.ClaimBO.ContactInfo.Address)
                UserControlContactInfo.NewClaimBind(State.ClaimBO.ContactInfo)
            Else
                UserControlAddress.ClaimDetailsBind(State.ClaimBO.ContactInfo.Address)
                UserControlContactInfo.Bind(State.ClaimBO.ContactInfo)

            End If
        Else
            moUserControlContactInfo.Visible = False

            If State.ClaimBO.ContactInfo.IsNew Then
                If State.ClaimBO.ContactInfo IsNot Nothing Then
                    State.ClaimBO.ContactInfo.Delete()
                End If

                If State.ClaimBO.ContactInfo.Address IsNot Nothing Then
                    State.ClaimBO.ContactInfo.Address.Delete()
                End If

                If Not State.ClaimBO.ContactInfoId = System.Guid.Empty Then
                    State.ClaimBO.ContactInfoId = System.Guid.Empty
                End If
            End If
        End If
    End Sub

    Private Sub tvQuestion_TreeNodePopulate(sender As Object, e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvQuestion.TreeNodePopulate
        Dim guidParent As Guid = New Guid(e.Node.Value.Split("|"c)(0))
        Dim softQuestDV As SoftQuestion.SoftQuestionDV = SoftQuestion.getChildren(guidParent)
        Dim intChildCnt As Integer, blnFromCert As Boolean = False
        If Not State.RiskTypeId.Equals(Guid.Empty) Then blnFromCert = True

        'Dim drv As DataRowView
        For Each drv As DataRowView In softQuestDV
            intChildCnt = 0
            Integer.TryParse(drv(SoftQuestion.SoftQuestionDV.COL_NAME_CHILD_COUNT).ToString, intChildCnt)
            Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv(SoftQuestion.SoftQuestionDV.COL_NAME_DESCRIPTION).ToString, GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_ID)).ToString & "||" & intChildCnt)
            If intChildCnt > 0 Then
                With newNode
                    .PopulateOnDemand = True
                    .Expanded = False
                End With
            End If
            If blnFromCert Then
                newNode.SelectAction = TreeNodeSelectAction.None
                newNode.NavigateUrl = "javascript:void(0);"
            Else
                newNode.SelectAction = TreeNodeSelectAction.Select
            End If
            e.Node.ChildNodes.Add(newNode)
        Next
        Dim x As String = "<script language='JavaScript'> revealModal('ModalSoftQuestions') </script>"
        RegisterStartupScript("Startup", x)
    End Sub

    Private Sub btnModalServiceWarrantyYes_Click(sender As Object, e As EventArgs) Handles btnModalServiceWarrantyYes.Click
        GoToNextStep()
    End Sub

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

            State.ClaimBO.AttachImage(
                New Guid(DocumentTypeDropDown.SelectedValue),
                Nothing,
                DateTime.Now,
                fileName,
                CommentTextBox.Text,
                ElitaPlusIdentity.Current.ActiveUser.UserName,
                fileData)
            If Not State.ClaimBO.IsNew Then

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
#End Region

#Region "UI Controlling Logic"

    Private Sub PopulateStepUIandData()
        InitializeCommonData()
        InitializeStepUI(State.StepName)
        PopulateFormFromBO(State.StepName)
    End Sub

    Private Sub UpdateBreadCrum(wizardStep As ClaimWizardSteps)

        If Me.State.EntryStep = ClaimWizardSteps.Step1 Then

            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & State.CertBO.CertNumber & ElitaBase.Sperator &
                                      "File New Claim"
        Else
            If (State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.IsNew) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & State.CertBO.CertNumber & ElitaBase.Sperator &
                                          "File New Claim"
            Else
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM DETAILS") & ElitaBase.Sperator & String.Format("{0} {1} {2} {3}", TranslationBase.TranslateLabelOrMessage("UPDATE"), TranslationBase.TranslateLabelOrMessage("PENDING"), TranslationBase.TranslateLabelOrMessage("CLAIM"), State.ClaimBO.ClaimNumber)
            End If
        End If
    End Sub

    Private Sub InitializeStepUI(wizardStep As ClaimWizardSteps)
        MasterPage.UsePageTabTitleInBreadCrum = False
        WizardControl.Visible = False
        If (ShowWizard()) Then SetSelectedStep()
        UpdateBreadCrum(wizardStep)
        EnableDisableStepDivs(wizardStep)
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                AddCalendar_New(step1_btnDateOfLoss, step1_moDateOfLossText)
                AddCalendar_New(step1_btnDateReported, step1_txtDateReported)
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
                EnableDisableWizardControls(wizardStep)
            Case ClaimWizardSteps.Step2
                TranslateGridHeader(grdMasterClaim)
                EnableDisableWizardControls(wizardStep)
            Case ClaimWizardSteps.Step3
                AddCalendar_New(step3_ImageButtonLossDate, step3_TextboxLossDate)
                AddCalendar_New(step3_ImageButtonReportDate, step3_TextboxReportDate)
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                If Not step3_LabelUseShipAddress.Text.EndsWith(":") Then
                    step3_LabelUseShipAddress.Text = step3_LabelUseShipAddress.Text & ":"
                End If
                'REQ-5467
                If State.ClaimBO IsNot Nothing Then
                    If State.ClaimBO.Dealer.IsLawsuitMandatoryId = State.yesId Then
                        step3_LabelIsLawsuitId.Text = "<span class=""mandatory"">*</span> " & step3_LabelIsLawsuitId.Text
                    End If
                End If

                'Me.lblInvalidDoLMessage.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_CONFIRM)
                btnModalBbDolYes.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_YES)
                btnModalBbDolNo.Value = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_NO)

                SetContactInfoLabelColor()
                SetAddressLabelColor()
                TranslateGridHeader(Grid)
                TranslateGridHeader(GridClaimImages)
                TranslateGridHeader(GridClaimAuthorization)
                ucClaimConsequentialDamage.Translate()
                EnableDisableWizardControls(wizardStep)


            Case ClaimWizardSteps.Step4
                Dim showCityFields As Boolean = step4_RadioButtonByCity.Checked
                Dim showAllFields As Boolean = step4_RadioButtonAll.Checked
                ' City
                ControlMgr.SetVisibleControl(Me, step4_tdCityLabel, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_moCityLabel, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_tdCityTextBox, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_TextboxCity, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_tdClearButton, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_btnClearSearch, showCityFields)

                ' All
                ControlMgr.SetVisibleForControlFamily(Me, step4_moMultipleColumnDrop, showAllFields)
                ControlMgr.SetVisibleControl(Me, step4_btnSearch, Not showAllFields)
            Case ClaimWizardSteps.Step5
        End Select

        HandleButtons(wizardStep)

    End Sub

    Private Sub EnableDisableWizardControls(wizardStep As ClaimWizardSteps)

        'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, False)

        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                ControlMgr.SetVisibleControl(Me, step1_searchResultPanel, State.IsEditMode)
                ControlMgr.SetEnableControl(Me, btnContinue, True)
            Case ClaimWizardSteps.Step2
                MyBase.EnableDisableControls(pnlVehicleInfo, Not State.IsEditMode)
                MyBase.EnableDisableControls(pnlDeviceInfo, Not State.IsEditMode)
                Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)

                If State.CertItemBO.IsEquipmentRequired Then
                    'Claimed Equipment
                    ControlMgr.SetVisibleControl(Me, step2_ddlClaimedManuf, State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedmake, Not State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedModel, True)
                    ControlMgr.SetVisibleControl(Me, step2_ddlClaimedSku, State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedSKu, Not State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedSerialNumber, True)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedDescription, True)

                    ControlMgr.SetVisibleControl(Me, step2_lblClaimedEquipment, True)
                    ControlMgr.SetVisibleControl(Me, step2_lblClaimedMake, True)
                    ControlMgr.SetVisibleControl(Me, step2_lblClaimedModel, True)
                    ControlMgr.SetVisibleControl(Me, step2_lblClaimedSKU, True)
                    ControlMgr.SetVisibleControl(Me, step2_LabelClaimSerialNumber, True)
                    ControlMgr.SetVisibleControl(Me, step2_LabelClaimDesc, True)

                    ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, False)
                    ControlMgr.SetEnableControl(Me, step2_TextboxManufacturer, False)
                    ControlMgr.SetEnableControl(Me, step2_TextboxModel, False)
                    ControlMgr.SetEnableControl(Me, step2_TextboxDealerItemDesc, False)
                    ControlMgr.SetEnableControl(Me, step2_TextboxSKU, False)
                    ControlMgr.SetEnableControl(Me, step2_TextboxSerialNumber, False)

                    step2_txtClaimedmake.ReadOnly = True
                    step2_txtClaimedModel.ReadOnly = Not State.IsEditMode
                    step2_txtClaimedModel.ReadOnly = Not State.IsEditMode
                    step2_txtClaimedSKu.ReadOnly = Not State.IsEditMode
                    step2_txtClaimedSerialNumber.ReadOnly = Not State.IsEditMode
                    step2_txtClaimedDescription.ReadOnly = True

                    step2_TextboxModel.ReadOnly = True
                    step2_TextboxSerialNumber.ReadOnly = True
                    step2_TextboxDealerItemDesc.ReadOnly = True
                    ControlMgr.SetVisibleControl(Me, btnContinue, Not State.IsEditMode)

                    '' ''If Me.State.CertItemBO.EquipmentId.Equals(Guid.Empty) Then
                    '' ''    'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, True)
                    '' ''    ControlMgr.SetVisibleControl(Me, Me.btnContinue, False)
                    '' ''Else
                    '' ''    ControlMgr.SetVisibleControl(Me, Me.btnContinue, True)
                    '' ''    'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, False)
                    '' ''End If
                Else
                    ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, Not State.IsEditMode)
                End If

                ControlMgr.SetVisibleControl(Me, step2_cboRiskTypeId, State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_TextboxRiskType, Not State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_cboMethodOfRepair, State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_TextboxMethodOfRepair, Not State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_lblEnrolledDeviceInfo, True)

                step2_TextboxSKU.ReadOnly = True
                step2_TextboxRiskType.ReadOnly = True
                step2_TextboxRiskType.ReadOnly = True
                step2_TextboxManufacturer.ReadOnly = True
                step2_TextboxMethodOfRepair.ReadOnly = True
                step2_TextboxBeginDate.ReadOnly = True
                step2_TextboxEndDate.ReadOnly = True
                step2_TextboxDeductibleBasedOn.ReadOnly = True
                step2_TextboxDateAdded.ReadOnly = True
                step2_TextboxDeductible.ReadOnly = True
                step2_TextboxDeductiblePercent.ReadOnly = True
                step2_TextboxCoverageType.ReadOnly = True
                step2_TextboxProductCode.ReadOnly = True
                step2_TextboxLiabilityLimit.ReadOnly = True
                step2_TextboxRepairDiscountPct.ReadOnly = True
                step2_TextboxReplacementDiscountPct.ReadOnly = True
                step2_TextboxClassCode.ReadOnly = True
                step2_TextboxDiscountAmt.ReadOnly = True
                step2_TextboxDiscountPercent.ReadOnly = True
                step2_TextboxOdometer.ReadOnly = True
                step2_TextboxYear.ReadOnly = True

                step2_TextboxInvNum.ReadOnly = Not State.IsEditMode


                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
                    pnlVehicleInfo.Visible = False
                    step2_LabelYear.Visible = False
                    step2_TextboxYear.Visible = False
                    step2_LabelSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                Else
                    step2_LabelSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("VIN") + ":"
                    ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, False)
                    step2_cboManufacturerId.Enabled = False
                    ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, True)
                    step2_TextboxModel.ReadOnly = True
                End If

                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                If (Not State.CertItemCoverageBO.IsClaimAllowed.Equals(NoId)) Then
                    Dim todayDate As Date
                    If todayDate.Today < State.CertItemCoverageBO.BeginDate.Value And State.CertBO.StatusCode <> CLOSED Then
                        ControlMgr.SetEnableControl(Me, btnDenyClaim, True)

                    End If
                End If
                step2_TextboxBeginDate.Font.Bold = True
                step2_TextboxEndDate.Font.Bold = True

                '5623

                If State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.DealerBO.IsGracePeriodSpecified Then

                    If Not State.CertItemCoverageBO.IsCoverageEffectiveForGracePeriod(step1_txtDateReported.Text) Then
                        step2_TextboxBeginDate.ForeColor = Color.Red
                        step2_TextboxEndDate.ForeColor = Color.Red
                    Else
                        step2_TextboxBeginDate.ForeColor = Color.Green
                        step2_TextboxEndDate.ForeColor = Color.Green
                    End If
                Else
                    If Not State.CertItemCoverageBO.IsCoverageEffective Then
                        step2_TextboxBeginDate.ForeColor = Color.Red
                        step2_TextboxEndDate.ForeColor = Color.Red
                    Else
                        step2_TextboxBeginDate.ForeColor = Color.Green
                        step2_TextboxEndDate.ForeColor = Color.Green
                    End If
                End If

            Case ClaimWizardSteps.Step3
                InitialEnableDisableControlsForStep3()
                EnableDisableControlsForStep3()

            Case ClaimWizardSteps.Step4
                Dim showCityFields As Boolean = step4_RadioButtonByCity.Checked
                Dim showAllFields As Boolean = step4_RadioButtonAll.Checked
                Dim NO_SVC_OPTION As Boolean = step4_RadioButtonNO_SVC_OPTION.Checked
                ' City
                ControlMgr.SetVisibleControl(Me, step4_tdCityLabel, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_moCityLabel, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_tdCityTextBox, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_TextboxCity, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_tdClearButton, showCityFields)
                ControlMgr.SetVisibleControl(Me, step4_btnClearSearch, showCityFields)

                ' All
                ControlMgr.SetVisibleForControlFamily(Me, step4_moMultipleColumnDrop, showAllFields)
                ControlMgr.SetVisibleControl(Me, step4_btnSearch, Not showAllFields)

                'REQ-5546
                'NO_SVC_OPTION
                ControlMgr.SetVisibleControl(Me, step4_tdClearButton, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_btnSearch, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_tdCityLabel, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_moCityLabel, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_tdCityTextBox, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_TextboxCity, Not NO_SVC_OPTION)
                ControlMgr.SetVisibleControl(Me, step4_tdCountryLabel, Not NO_SVC_OPTION)

                If (State.CompanyBO Is Nothing) Then
                    State.CompanyBO = New Company(State.CertBO.CompanyId)
                End If
                'REQ-5546
                Dim objCountry As New Country(State.CompanyBO.CountryId)
                State.default_service_center_id = objCountry.DefaultSCId

                ControlMgr.SetVisibleControl(Me, step4_RadioButtonNO_SVC_OPTION, Not State.default_service_center_id.Equals(Guid.Empty))

            Case ClaimWizardSteps.Step5
                ChangeEnabledProperty(step5_TextboxCallerName, True)
                ChangeEnabledProperty(step5_TextboxCommentText, True)
                ChangeEnabledProperty(step5_cboCommentType, True)
                btnBack.Enabled = True
                If step5_TextboxDateTime.Text Is Nothing OrElse step5_TextboxDateTime.Text.Trim.Length = 0 Then
                    ControlMgr.SetVisibleControl(Me, step5_LabelDateTime, False)
                    ControlMgr.SetVisibleControl(Me, step5_TextboxDateTime, False)
                End If
        End Select
    End Sub

    Private Function DoesAcceptedOfferExistForIMEI() As Boolean
        Dim blnIsDealerDRPTradeInQuoteFlag As Boolean = False
        blnIsDealerDRPTradeInQuoteFlag = State.ClaimBO.GetDealerDRPTradeInQuoteFlag(State.ClaimBO.DealerCode)
        If blnIsDealerDRPTradeInQuoteFlag Then
            If State.ClaimBO.VerifyIMEIWithDRPSystem(State.ClaimBO.SerialNumber) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function ShowWizard() As Boolean
        If (State.InputParameters IsNot Nothing) Then
            Return State.InputParameters.ShowWizard
        Else
            Return False
        End If
    End Function

    Private Sub EnableDisableStepDivs(stepName As ClaimWizardSteps)
        Select Case stepName
            Case ClaimWizardSteps.Step1
                dvstep1.Visible = True
                dvStep2.Visible = False
                dvStep3.Visible = False
                dvStep4.Visible = False
                dvStep5.Visible = False
            Case ClaimWizardSteps.Step2
                dvstep1.Visible = False
                dvStep2.Visible = True
                dvStep3.Visible = False
                dvStep4.Visible = False
                dvStep5.Visible = False
            Case ClaimWizardSteps.Step3
                dvstep1.Visible = False
                dvStep2.Visible = False
                dvStep3.Visible = True
                dvStep4.Visible = False
                dvStep5.Visible = False
            Case ClaimWizardSteps.Step4
                dvstep1.Visible = False
                dvStep2.Visible = False
                dvStep3.Visible = False
                dvStep4.Visible = True
                dvStep5.Visible = False
            Case ClaimWizardSteps.Step5
                dvstep1.Visible = False
                dvStep2.Visible = False
                dvStep3.Visible = False
                dvStep4.Visible = False
                dvStep5.Visible = True
        End Select
    End Sub

    Private Overloads Sub SetDefaultButton(stepName As ClaimWizardSteps)

        Select Case stepName
            Case ClaimWizardSteps.Step1
                MyBase.SetDefaultButton(step1_moDateOfLossText, step1_btnSearch)
            Case ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step4
            Case ClaimWizardSteps.Step5

        End Select

    End Sub

    Private Sub InitializeCommonData()
        'Initialize Certificate BO
        If (State.CertBO Is Nothing) Then
            If State.InputParameters IsNot Nothing AndAlso Not State.InputParameters.CertificateId.Equals(Guid.Empty) Then
                State.CertBO = New Certificate(State.InputParameters.CertificateId)
            End If
        End If
        If (State.CertBO IsNot Nothing) Then
            'Initialize Dealer BO
            If (State.DealerBO Is Nothing) Then
                If (State.CertBO IsNot Nothing) Then
                    State.DealerBO = New Dealer(State.CertBO.DealerId)
                End If
            End If

            'Initialize VSC Model
            If (State.VSCModel Is Nothing) Then
                If Not State.CertBO.ModelId.Equals(Guid.Empty) Then
                    State.VSCModel = New VSCModel(State.CertBO.ModelId)
                End If
            End If

            'Initialize VSC Class Code
            If (State.VSCClassCode Is Nothing) Then
                If Not State.CertBO.ClassCodeId.Equals(Guid.Empty) Then
                    State.VSCClassCode = New VSCClassCode(State.CertBO.ClassCodeId)
                End If
            End If

        End If

        'Initialize Certificate Item Coverage BO
        If (State.CertItemCoverageBO Is Nothing) Then
            If (Not State.CertficateCoverageTypeId.Equals(Guid.Empty)) Then
                State.CertItemCoverageBO = New CertItemCoverage(State.CertficateCoverageTypeId)
            End If
        End If

        'Initialize Certificate Item BO
        If (State.CertItemBO Is Nothing) Then
            If (State.CertItemCoverageBO IsNot Nothing) Then
                State.CertItemBO = New CertItem(State.CertItemCoverageBO.CertItemId)
                State.CertBO = State.CertItemBO.GetCertificate(State.CertBO.Id)
            End If
        End If

        'End If
        'Initialize Claim BO
        If (State.ClaimBO Is Nothing) Then
            If (Not State.InputParameters.claimId.Equals(Guid.Empty)) Then
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(State.InputParameters.claimId)
            ElseIf (State.InputParameters.ClaimBo IsNot Nothing) Then
                State.ClaimBO = CType(State.InputParameters.ClaimBo, MultiAuthClaim)
            Else
                'Load claim Bo if only on step 3 and above
                If (DirectCast([Enum].Parse(GetType(ClaimWizardSteps), CStr(State.StepName)), Integer) > 2) Then
                    State.ClaimBO = ClaimFacade.Instance.CreateClaim(Of MultiAuthClaim)(State.DealerBO.Id)
                    State.ClaimBO.PrePopulate(State.CertItemCoverageBO.Id, State.MasterClaimNumber, State.DateOfLoss, False, False, False, True, State.CallerName, State.ProblemDescription, State.DateReported, State.step2_claimEquipmentBO)
                End If
            End If
        End If

        'Initialize from ClaimBO
        If (State.ClaimBO IsNot Nothing) Then
            State.CertItemCoverageBO = State.ClaimBO.CertificateItemCoverage
            State.CertItemBO = State.ClaimBO.CertificateItem
            State.CertBO = State.ClaimBO.Certificate
            State.DealerBO = State.ClaimBO.Dealer
            If (State.ClaimBO.CurrentComment IsNot Nothing) Then
                State.CommentBO = State.ClaimBO.CurrentComment
            Else
                State.CommentBO = Comment.GetLatestComment(State.ClaimBO)
            End If
            State.step2_claimEquipmentBO = State.ClaimBO.ClaimedEquipment
        End If

        If (State.yesId.Equals(Guid.Empty)) Then
            State.yesId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        End If

        If (State.noId.Equals(Guid.Empty)) Then
            State.noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        End If

        If (State.CompanyBO Is Nothing) Then
            State.CompanyBO = New Company(State.CertBO.CompanyId)
            Dim sSalutation As String = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.CertBO.SalutationId)
            If sSalutation = "Y" Then State.IsSalutation = True Else State.IsSalutation = False
        End If




    End Sub

    Private Sub HandleButtons(wizardStep As ClaimWizardSteps)

        ControlMgr.SetVisibleControl(Me, btnBack, True)
        ControlMgr.SetVisibleControl(Me, btnUndo_Write, False)
        ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, False)
        ControlMgr.SetVisibleControl(Me, btnSoftQuestions, False)
        ControlMgr.SetVisibleControl(Me, btnDenyClaim, False)
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
        ControlMgr.SetVisibleControl(Me, btnClaimOverride_Write, False)
        ControlMgr.SetVisibleControl(Me, btnComment, False)
        ControlMgr.SetVisibleControl(Me, btnClaimDeductibleRefund, False)
        btnContinue.Text = TranslationBase.TranslateLabelOrMessage("CONTINUE")

        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                ControlMgr.SetVisibleControl(Me, btnContinue, State.IsEditMode)
            Case ClaimWizardSteps.Step2
                ControlMgr.SetVisibleControl(Me, btnUndo_Write, State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnSoftQuestions, Not State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, Not State.IsEditMode)
            Case ClaimWizardSteps.Step3
                If Not State.ClaimBO.IsNew Then
                    ControlMgr.SetVisibleControl(Me, btnCancel, False)
                    ControlMgr.SetVisibleControl(Me, btnCancelClaim, True)
                    ControlMgr.SetVisibleControl(Me, btnClaimOverride_Write, False)
                    ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, btnCancelClaim, False)
                End If
                If Not State.ClaimBO.IsNew Then
                    ' Pending Claim
                    If State.ClaimBO.IsDaysLimitExceeded Then
                        If Not (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                                ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                            ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
                        End If
                    End If
                End If
                ControlMgr.SetVisibleControl(Me, btnDenyClaim, Not State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnContinue, Not State.InputParameters.ComingFromDenyClaim)

                If (State.ClaimBO.IsDeductibleRefundAllowed AndAlso
                            Not State.ClaimBO.IsDeductibleRefundExist) Then
                    ControlMgr.SetVisibleControl(Me, btnClaimDeductibleRefund, True)
                End If
            Case ClaimWizardSteps.Step4
            Case ClaimWizardSteps.Step5
                btnContinue.Text = TranslationBase.TranslateLabelOrMessage("SUBMIT_CLAIM")
        End Select

        SetDefaultButton(wizardStep)
    End Sub

    Private Sub PopulateProtectionAndEventDetail()
        Dim cssClassName As String

        moProtectionAndEventDetails.CustomerName = NO_DATA
        moProtectionAndEventDetails.DealerName = NO_DATA
        moProtectionAndEventDetails.CallerName = NO_DATA
        moProtectionAndEventDetails.ProtectionStatus = NO_DATA
        moProtectionAndEventDetails.ClaimedMake = NO_DATA
        moProtectionAndEventDetails.EnrolledModel = NO_DATA
        moProtectionAndEventDetails.ClaimStatus = NO_DATA
        moProtectionAndEventDetails.EnrolledMake = NO_DATA
        moProtectionAndEventDetails.ClaimNumber = NO_DATA
        moProtectionAndEventDetails.ClaimedModel = NO_DATA
        moProtectionAndEventDetails.TypeOfLoss = NO_DATA
        moProtectionAndEventDetails.DateOfLoss = NO_DATA

        If (State.CertBO IsNot Nothing) Then
            moProtectionAndEventDetails.CustomerName = State.CertBO.CustomerName
            moProtectionAndEventDetails.DealerName = State.CertBO.getDealerDescription

            If State.CallerName Is Nothing Then
                If String.IsNullOrEmpty(State.ClaimBO.CallerName) Then
                    moProtectionAndEventDetails.CallerName = State.CertBO.CustomerName
                Else
                    moProtectionAndEventDetails.CallerName = State.ClaimBO.CallerName
                End If
            ElseIf Not (State.CallerName.Equals(String.Empty)) Then
                moProtectionAndEventDetails.CallerName = State.CallerName
            End If

            If State.DateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(State.DateOfLoss)

            moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromCode("CSTAT", State.CertBO.StatusCode)
            If (State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
        End If

        moProtectionAndEventDetails.ProtectionStatusCss = cssClassName

        If (State.DateOfLoss > Date.MinValue) Then
            moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(State.DateOfLoss)
        End If

        If (State.CertItemBO IsNot Nothing) Then
            moProtectionAndEventDetails.EnrolledModel = State.CertItemBO.Model
            moProtectionAndEventDetails.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, State.CertItemBO.RiskTypeId)
            If (Not (State.CertItemBO.ManufacturerId.Equals(Guid.Empty))) Then
                moProtectionAndEventDetails.EnrolledMake = New Manufacturer(State.CertItemBO.ManufacturerId).Description
            End If
        End If

        If State.ClaimBO IsNot Nothing Then

            moProtectionAndEventDetails.ClaimNumber = State.ClaimBO.ClaimNumber
            moProtectionAndEventDetails.ClaimStatus = LookupListNew.GetClaimStatusFromCode(ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.ClaimBO.StatusCode)
            moProtectionAndEventDetails.ClaimStatusCss = If(Me.State.ClaimBO.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
            moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(State.ClaimBO.LossDate.Value)

            If State.ClaimBO.ClaimedEquipment IsNot Nothing Then
                moProtectionAndEventDetails.ClaimedMake = State.ClaimBO.ClaimedEquipment.Manufacturer
                moProtectionAndEventDetails.ClaimedModel = State.ClaimBO.ClaimedEquipment.Model
            End If

        End If

    End Sub

    Private Sub PopulateDropDowns(wizardStep As ClaimWizardSteps)
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())


        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                EnableDisableWizardControls(wizardStep)
                'Me.BindListControlToDataView(Me.step1_cboRiskType, LookupListNew.LoadRiskTypes(State.CertBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                Dim listcontextForrisktypes As ListContext = New ListContext()
                listcontextForrisktypes.CertId = State.CertBO.Id
                listcontextForrisktypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                listcontextForrisktypes.DateOfLoss = State.DateOfLoss

                Dim RiskTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForrisktypes)
                ' step1_cboCoverageType
                step1_cboRiskType.Populate(RiskTypeList, New PopulateOptions() With
                                                 {
                                                 .AddBlankItem = True
                                                 })
                If step1_cboRiskType.Items.Count <= 1 Then
                    step1_riskTypeTR.Visible = False
                ElseIf step1_cboRiskType.Items.Count = 2 Then
                    step1_cboRiskType.SelectedIndex = 1
                    step1_riskTypeTR.Visible = False
                Else
                    step1_riskTypeTR.Visible = True
                End If
                If Not step1_cboRiskType.SelectedIndex = -1 Then
                    ' Me.BindListControlToDataView(Me.step1_cboCoverageType, LookupListNew.LoadCoverageTypes(State.CertBO.Id, New Guid(step1_cboRiskType.SelectedValue), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                    Dim listcontextForcoveragetypes As ListContext = New ListContext()
                    listcontextForcoveragetypes.CertId = State.CertBO.Id
                    listcontextForcoveragetypes.CertItemId = New Guid(step1_cboRiskType.SelectedValue)
                    listcontextForcoveragetypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    listcontextForcoveragetypes.DateOfLoss = State.DateOfLoss

                    Dim CoverageTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForcoveragetypes)
                    step1_cboCoverageType.Populate(CoverageTypeList, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True
                                                      })
                End If
            Case ClaimWizardSteps.Step2
                Dim oListContext As New ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim riskList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", context:=oListContext)
                step2_cboRiskTypeId.Populate(riskList, New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })

                If State.CertItemBO.IsEquipmentRequired Then
                    If Not String.IsNullOrEmpty(State.DealerBO.EquipmentListCode) Then
                        Dim listcontextForManufacturerByEquipmentCode As ListContext = New ListContext()
                        listcontextForManufacturerByEquipmentCode.EquipmentListCode = State.DealerBO.EquipmentListCode
                        listcontextForManufacturerByEquipmentCode.EffectiveOnDate = Date.Now

                        Dim ManufacturerByEquipmentList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByEquipment", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForManufacturerByEquipmentCode)
                        step2_cboManufacturerId.Populate(ManufacturerByEquipmentList, New PopulateOptions() With
                                                            {
                                                            .AddBlankItem = True
                                                            })
                    Else
                        MasterPage.MessageController.AddWarning("EQUIPMENT_LIST_DOES_NOT_EXIST_FOR_DEALER")
                    End If
                    Dim listContext As New ListContext
                    listContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    Dim claimedList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", context:=listContext)
                    step2_ddlClaimedManuf.Populate(claimedList, New PopulateOptions() With
                                                      {
                                                      .AddBlankItem = True
                                                      })
                Else
                    Dim listContext As New ListContext
                    listContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    Dim cboList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", context:=listContext)
                    step2_cboManufacturerId.Populate(cboList, New PopulateOptions() With
                                                        {
                                                        .AddBlankItem = True
                                                        })
                End If

                step2_cboMethodOfRepair.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
                ' Me.BindListControlToDataView(Me.step2_cboCalimAllowed, yesNoLkL, , , True)
                step2_cboCalimAllowed.Populate(yesNoLkL, New PopulateOptions() With
                                                  {
                                                  .AddBlankItem = True
                                                  })
                ' Me.BindListControlToDataView(Me.step2_cboApplyDiscount, yesNoLkL, , , True)
                step2_cboApplyDiscount.Populate(yesNoLkL, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Case ClaimWizardSteps.Step3

                'Me.BindListControlToDataView(Me.step3_cboCauseOfLossId, LookupListNew.GetCauseOfLossByCoverageTypeAndSplSvcLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.ClaimBO.CoverageTypeId, Me.State.CertBO.DealerId, Authentication.LangId, Me.State.CertBO.ProductCode, , False))

                Dim listCauseOfLoss As ListContext = New ListContext()
                listCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listCauseOfLoss.CoverageTypeId = State.ClaimBO.CoverageTypeId
                listCauseOfLoss.LanguageId = Authentication.LangId
                listCauseOfLoss.DealerId = State.CertBO.DealerId
                listCauseOfLoss.ProductCode = State.CertBO.ProductCode
                Dim coverageList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CauseOfLossByCoverageTypeAndSplSvcLookupList", context:=listCauseOfLoss)
                step3_cboCauseOfLossId.Populate(coverageList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                If State.IsSalutation Then
                    step3_cboContactSalutationId.Populate(CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                             {
                                                             .AddBlankItem = True
                                                             })
                    step3_cboCallerSalutationId.Populate(CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                            {
                                                            .AddBlankItem = True
                                                            })
                End If

                BindListControlToDataView(ddlIssueCode, State.ClaimBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
                BindListControlToDataView(ddlIssueDescription, State.ClaimBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)

                step3_cboDedCollMethod.Populate(CommonConfigManager.Current.ListManager.GetList("DEDCOLLMTHD", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                Dim selDedCollMethod As String = LookupListNew.GetCodeFromId(LookupListCache.LK_DED_COLL_METHOD, State.ClaimBO.DedCollectionMethodID)

                If selDedCollMethod IsNot Nothing Then
                    SetSelectedItem(step3_cboDedCollMethod, LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, selDedCollMethod))
                End If
                'Me.BindListControlToDataView(Me.step3_cboUseShipAddress, LookupListNew.GetYesNoLookupList(Authentication.LangId, False), , , False)
                Dim yesNoLk As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                step3_cboUseShipAddress.Populate(yesNoLk, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                SetSelectedItem(step3_cboUseShipAddress, NoId)
                ' Me.BindListControlToDataView(Me.step3_cboLawsuitId, yesNoLkL)
                step3_cboLawsuitId.Populate(yesNoLk, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })
                ' Me.BindListControlToDataView(Me.step3_cboDeniedReason, LookupListNew.GetDeniedReasonLookupList(Authentication.LangId))

                'KDDI CHANGES
                Dim listcontextForMgList As ListContext = New ListContext()
                listcontextForMgList.CompanyGroupId = State.CertBO.Company.CompanyGroupId
                listcontextForMgList.DealerId = State.CertBO.Dealer.Id
                listcontextForMgList.CompanyId = State.CertBO.CompanyId
                listcontextForMgList.DealerGroupId = State.CertBO.Dealer.DealerGroupId

                Dim deniedReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("DNDREASON", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMgList)
                'If Not (deniedReason Is Nothing) Then
                '    If Not deniedReason.Length > 0 Then
                '        deniedReason = CommonConfigManager.Current.ListManager.GetList("DNDREASON", Thread.CurrentPrincipal.GetLanguageCode())
                '    End If
                'End If
                step3_cboDeniedReason.Populate(deniedReason, New PopulateOptions() With
                                                  {
                                                  .AddBlankItem = True
                                                  })

                ' Me.BindListControlToDataView(Me.step3_cboFraudulent, yesNoLkL)
                step3_cboFraudulent.Populate(yesNoLk, New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })
                'Me.BindListControlToDataView(Me.step3_cboComplaint, yesNoLkL)
                step3_cboComplaint.Populate(yesNoLk, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })

                ' Me.BindListControlToDataView(Me.DocumentTypeDropDown, LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                DocumentTypeDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                 {
                                                 .AddBlankItem = True
                                                 })
                ClearForm()
            Case ClaimWizardSteps.Step4
            Case ClaimWizardSteps.Step5
                ' Dim commentTypeLk As DataView = LookupListNew.GetCommentTypeLookupList(Authentication.LangId)
                ' Me.BindListControlToDataView(Me.step5_cboCommentType, commentTypeLk)
                Dim commentTypeLk As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMT", Thread.CurrentPrincipal.GetLanguageCode())
                step5_cboCommentType.Populate(commentTypeLk, New PopulateOptions() With
                                                 {
                                                 .AddBlankItem = True
                                                 })
        End Select

    End Sub

    Private Sub PopulateFormFromBO(wizardStep As ClaimWizardSteps)
        PopulateProtectionAndEventDetail()
        Dim flag As Boolean = True
        Dim errMsg As List(Of String) = New List(Of String)
        Dim warningMsg As List(Of String) = New List(Of String)
        hdnDealerId.Value = State.DealerBO.Id.ToString
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                If (Not Page.IsPostBack) Then
                    State.DateReported = Date.Today
                End If
                If State.DateReported > Date.MinValue Then PopulateControlFromBOProperty(step1_txtDateReported, GetDateFormattedStringNullable(New DateType(State.DateReported)))
                If State.DateOfLoss > Date.MinValue Then PopulateControlFromBOProperty(step1_moDateOfLossText, GetDateFormattedStringNullable(New DateType(State.DateOfLoss)))

                If (State.CallerName Is Nothing) Then
                    step1_textCallerName.Text = State.CertBO.CustomerName
                Else
                    step1_textCallerName.Text = State.CallerName
                End If
                If (Not State.RiskTypeId.Equals(Guid.Empty)) Then
                    PopulateDropDowns(wizardStep)
                    step1_cboRiskType.SelectedItem.Value = State.RiskTypeId.ToString()
                    If (Not State.CertficateCoverageTypeId.Equals(Guid.Empty)) Then
                        step1_cboCoverageType.SelectedItem.Value = State.CertficateCoverageTypeId.ToString()
                    End If
                End If
                If (State.CallerName Is Nothing) Then
                    step1_textProblemDescription.Text = State.CallerName
                End If
            Case ClaimWizardSteps.Step2
                PopulateDropDowns(wizardStep)
                PopulateFormForCertItem()
                ValidateCertItem(errMsg, warningMsg)
                CheckForPossibleWarrantyClaim(warningMsg)
            Case ClaimWizardSteps.Step3
                PopulateDropDowns(wizardStep)
                PopulateFormForClaim()
            Case ClaimWizardSteps.Step4
                PopulateFormForServiceCenter()
                For Each claimAuth As ClaimAuthorization In State.ClaimBO.ClaimAuthorizationChildren
                    If Not claimAuth.IsNew Then
                        MasterPage.MessageController.AddWarning("CLAIM_ALREADY_HAS_AUTHORIZATION_WILL_VOID")
                    End If
                Next
            Case ClaimWizardSteps.Step5
                PopulateDropDowns(wizardStep)
                PopulateControlFromBOProperty(step5_TextboxCallerName, State.ClaimBO.CallerName)
                PopulateControlFromBOProperty(step5_TextboxCertificate, State.ClaimBO.Certificate.CertNumber)
                PopulateControlFromBOProperty(step5_TextboxCommentText, State.CommentBO.Comments)
                PopulateControlFromBOProperty(step5_TextboxDealer, State.ClaimBO.Dealer.Dealer)
                PopulateControlFromBOProperty(step5_cboCommentType, State.CommentBO.CommentTypeId)

                If State.CommentBO.CreatedDate IsNot Nothing Then
                    PopulateControlFromBOProperty(step5_TextboxDateTime, GetLongDateFormattedString(State.CommentBO.CreatedDate.Value))
                Else
                    PopulateControlFromBOProperty(step5_TextboxDateTime, Nothing)
                End If

                ClearMessageControllers()

        End Select

        If errMsg.Count > 0 Then MasterPage.MessageController.AddError(errMsg.ToArray)
        If warningMsg.Count > 0 Then moMessageController.AddWarning(warningMsg.ToArray)

    End Sub

    Private Sub PopulateBOFromForm(wizardStep As ClaimWizardSteps)
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                State.RiskTypeId = New Guid(step1_cboRiskType.SelectedItem.Value)
                State.CertficateCoverageTypeId = New Guid(step1_cboCoverageType.SelectedItem.Value)
                State.CallerName = step1_textCallerName.Text
                State.ProblemDescription = step1_textProblemDescription.Text
            Case ClaimWizardSteps.Step2
                With State.CertItemBO
                    PopulateBOProperty(State.CertItemBO, "RiskTypeId", step2_cboRiskTypeId)
                    PopulateBOProperty(State.CertItemBO, "ManufacturerId", step2_cboManufacturerId)
                    PopulateBOProperty(State.CertItemBO, "SerialNumber", step2_TextboxSerialNumber)
                    Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                    If (Not State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
                        PopulateBOProperty(State.CertItemBO, "Model", step2_TextboxModel)
                    End If
                    PopulateBOProperty(State.CertItemCoverageBO, "MethodOfRepairId", step2_cboMethodOfRepair)
                    PopulateBOProperty(State.CertItemBO, "ItemDescription", step2_TextboxDealerItemDesc)
                    PopulateBOProperty(State.CertBO, "InvoiceNumber", step2_TextboxInvNum)
                    PopulateBOProperty(State.CertItemBO, "SkuNumber", step2_TextboxSKU)
                End With
                'check for equipment flag and then populate rest from claim equipment bo of tyep claimed Equipment
                If State.step2_claimEquipmentBO IsNot Nothing AndAlso
                   (State.DealerBO.UseEquipmentId.Equals(State.yesId)) Then
                    With State.step2_claimEquipmentBO
                        PopulateBOProperty(State.step2_claimEquipmentBO, "ClaimEquipmentDate", State.CertItemBO.CreatedDate.ToString)
                        PopulateBOProperty(State.step2_claimEquipmentBO, "ManufacturerId", GetSelectedItem(step2_ddlClaimedManuf))
                        PopulateBOProperty(State.step2_claimEquipmentBO, "Model", step2_txtClaimedModel.Text)
                        'Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "SKU", Me.step2_txtClaimedSKu)

                        PopulateBOProperty(State.step2_claimEquipmentBO, "SerialNumber", step2_txtClaimedSerialNumber.Text)
                        'get equipment id
                        If (Not State.step2_claimEquipmentBO.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(State.step2_claimEquipmentBO.Model)) Then
                            State.step2_claimEquipmentBO.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(State.DealerBO.EquipmentListCode, DateTime.Now, State.step2_claimEquipmentBO.ManufacturerId, State.step2_claimEquipmentBO.Model)
                        End If
                        If State.step2_claimEquipmentBO.EquipmentId.Equals(Guid.Empty) Then
                            MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")
                            State.step2_claimEquipmentBO.SKU = String.Empty
                            hdnSelectedClaimedSku.Value = String.Empty
                        Else
                            State.step2_claimEquipmentBO.SKU = hdnSelectedClaimedSku.Value
                        End If


                        PopulateBOProperty(State.step2_claimEquipmentBO, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED))
                    End With
                End If

                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Case ClaimWizardSteps.Step3
                PopulateBOFromFormForStep3()
            Case ClaimWizardSteps.Step4

                If step4_RadioButtonNO_SVC_OPTION.Checked Then
                    'REQ-5546
                    'Default SC might not null
                    If Not State.default_service_center_id.Equals(Guid.Empty) Then
                        State.SelectedServiceCenterId = State.default_service_center_id
                    Else
                        MasterPage.MessageController.AddError("TEMP SERVICE CENTER NOT FOUND")
                        Exit Sub
                    End If
                Else
                    If (Not hdnSelectedServiceCenterId.Value = "XXXX") Then State.SelectedServiceCenterId = ServiceCenter.GetServiceCenterID(hdnSelectedServiceCenterId.Value)
                End If
                ' Bug 178224 - REQ- 6156
                If Not State.SelectedServiceCenterId.Equals(Guid.Empty) Then
                    PopulateBOProperty(State.ClaimBO, "ServiceCenterId", State.SelectedServiceCenterId)
                End If
            Case ClaimWizardSteps.Step5
                PopulateBOProperty(State.CommentBO, "CallerName", step5_TextboxCallerName)
                PopulateBOProperty(State.CommentBO, "Comments", step5_TextboxCommentText)
                PopulateBOProperty(State.CommentBO, "CommentTypeId", step5_cboCommentType)
        End Select

    End Sub

    Private Function LastStep() As ClaimWizardSteps
        Select Case State.StepName
            Case ClaimWizardSteps.Step2
                Return ClaimWizardSteps.Step1
            Case ClaimWizardSteps.Step3
                Return ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step4
                Return ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step5
                ' Bug 178224 - REQ- 6156 - if Dealer attribute is Yes, then Skipping the step 4 (choosing Service Center) and move to Step 3
                Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                If attvalue IsNot Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                    Return ClaimWizardSteps.Step3
                Else
                    If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                        Return ClaimWizardSteps.Step4
                    Else
                        Return ClaimWizardSteps.Step3
                    End If
                End If
        End Select
    End Function

    Private Function NextStep() As ClaimWizardSteps
        Select Case State.StepName
            Case ClaimWizardSteps.Step1
                Return ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step2
                Return ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step3
                ' Bug 178224 - REQ- 6156 -  REQ- 6156 - if Dealer attribute is Yes, then Skipping the step 4 (choosing Service Center) and move to Step 5
                Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                If attvalue IsNot Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                    Return ClaimWizardSteps.Step5
                Else
                    If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                        Return ClaimWizardSteps.Step4
                    Else
                        Return ClaimWizardSteps.Step5
                    End If
                End If
            Case ClaimWizardSteps.Step4
                Return ClaimWizardSteps.Step5
        End Select
    End Function

    Private Sub SetSelectedStep()
        WizardControl.Visible = True
        Dim lstWizardSteps As List(Of StepDefinition) = WizardControl.Steps

        For Each stepDef As StepDefinition In lstWizardSteps
            If (stepDef.StepNumber = DirectCast([Enum].Parse(GetType(ClaimWizardSteps), CStr(State.StepName)), Integer)) Then
                stepDef.IsSelected = True
            Else
                stepDef.IsSelected = False
            End If
        Next
    End Sub

    Private Sub ReturnBackToCallingPage()
        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.CertBO,, State.IsCallerAuthenticated)
        MyBase.ReturnToCallingPage(retObj)
    End Sub

    Private Sub PopulateFormDeductibleFormBOs(methodOfRepairId As Guid)
        Dim oDeductible As CertItemCoverage.DeductibleType
        oDeductible = CertItemCoverage.GetDeductible(State.CertItemCoverageBO.Id, methodOfRepairId)
        PopulateControlFromBOProperty(step2_TextboxDeductibleBasedOn, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, oDeductible.DeductibleBasedOnId))
        PopulateControlFromBOProperty(step2_TextboxDeductible, oDeductible.DeductibleAmount)
        PopulateControlFromBOProperty(step2_TextboxDeductiblePercent, oDeductible.DeductiblePercentage)
    End Sub

    Private Sub PopulateFormfromClaimedEquipmentBO()
        If State.CertItemBO.IsEquipmentRequired AndAlso
State.step2_claimEquipmentBO IsNot Nothing Then
            'Use Equipment is Yes
            PopulateControlFromBOProperty(step2_txtClaimedmake, State.step2_claimEquipmentBO.Manufacturer)
            PopulateControlFromBOProperty(step2_ddlClaimedManuf, State.step2_claimEquipmentBO.ManufacturerId)
            PopulateControlFromBOProperty(step2_txtClaimedModel, State.step2_claimEquipmentBO.Model)
            PopulateControlFromBOProperty(step2_txtClaimedDescription, State.step2_claimEquipmentBO.EquipmentDescription)
            'Me.PopulateControlFromBOProperty(Me.step2_txtClaimedSKu, Me.State.step2_claimEquipmentBO.SKU)
            PopulateControlFromBOProperty(step2_txtClaimedSerialNumber, State.step2_claimEquipmentBO.SerialNumber)

            '***********************Pre populate Claimed SKU based on the Equipment and Model
            step2_ddlClaimedSku.Items.Clear()
            If Not State.step2_claimEquipmentBO.EquipmentId.Equals(Guid.Empty) Then
                Dim dv As DataView = State.CertItemBO.LoadSku(State.step2_claimEquipmentBO.EquipmentId, State.DealerBO.Id)
                If dv IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then
                    step2_ddlClaimedSku.DataSource = dv
                    step2_ddlClaimedSku.DataTextField = "SKU_NUMBER"
                    step2_ddlClaimedSku.DataValueField = "SKU_NUMBER"
                    step2_ddlClaimedSku.DataBind()

                    If dv IsNot Nothing AndAlso dv.FindRows(State.step2_claimEquipmentBO.SKU.ToString).Length > 0 Then
                        PopulateControlFromBOProperty(step2_txtClaimedSKu, State.step2_claimEquipmentBO.SKU)
                        step2_ddlClaimedSku.SelectedItem.Value = State.step2_claimEquipmentBO.SKU
                        hdnSelectedClaimedSku.Value = State.step2_claimEquipmentBO.SKU
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub PopulateFormForCertItem()
        Dim riskTypeDesc As String
        Dim manufacturerDesc As String
        Dim methodOfRepairDesc As String
        Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
        Dim MethodOfRepairId As Guid

        SetSelectedItem(step2_cboRiskTypeId, State.CertItemBO.RiskTypeId)
        riskTypeDesc = GetSelectedDescription(step2_cboRiskTypeId)
        PopulateControlFromBOProperty(step2_TextboxRiskType, riskTypeDesc)
        step2_TextboxRiskType.ReadOnly = True

        Dim dvEnrolled As DataView
        If State.CertItemBO.IsEquipmentRequired Then
            dvEnrolled = LookupListNew.GetManufacturerbyEquipmentList(State.DealerBO.EquipmentListCode, Date.Now)
        Else
            dvEnrolled = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        End If

        If Not State.CertItemBO.ManufacturerId.Equals(Guid.Empty) Then
            If dvEnrolled IsNot Nothing AndAlso BusinessObjectBase.FindRow(State.CertItemBO.ManufacturerId, LookupListNew.COL_ID_NAME, dvEnrolled.Table) IsNot Nothing Then
                PopulateControlFromBOProperty(step2_cboManufacturerId, State.CertItemBO.ManufacturerId)
            Else
                If State.CertItemBO.IsEquipmentRequired Then
                    MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
                Else
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.INVALID_MAKE_MODEL_ERR)
                End If
            End If
        End If

        'Me.SetSelectedItem(Me.step2_cboManufacturerId, Me.State.CertItemBO.ManufacturerId)
        manufacturerDesc = GetSelectedDescription(step2_cboManufacturerId)
        PopulateControlFromBOProperty(step2_TextboxManufacturer, manufacturerDesc)
        ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, False)
        ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, True)
        step2_TextboxManufacturer.ReadOnly = True

        If (State.CertItemCoverageBO.MethodOfRepairId = Guid.Empty) Then
            SetSelectedItem(step2_cboMethodOfRepair, State.CertBO.MethodOfRepairId)
            MethodOfRepairId = State.CertBO.MethodOfRepairId
        Else
            SetSelectedItem(step2_cboMethodOfRepair, State.CertItemCoverageBO.MethodOfRepairId)
            MethodOfRepairId = State.CertItemCoverageBO.MethodOfRepairId
        End If
        methodOfRepairDesc = GetSelectedDescription(step2_cboMethodOfRepair)
        PopulateControlFromBOProperty(step2_TextboxMethodOfRepair, methodOfRepairDesc)
        ControlMgr.SetVisibleControl(Me, step2_cboMethodOfRepair, False)
        ControlMgr.SetVisibleControl(Me, step2_TextboxMethodOfRepair, True)
        step2_TextboxMethodOfRepair.ReadOnly = True

        PopulateControlFromBOProperty(step2_TextboxSerialNumber, State.CertItemBO.SerialNumber)
        PopulateControlFromBOProperty(step2_TextboxBeginDate, GetDateFormattedStringNullable(State.CertItemCoverageBO.BeginDate))
        PopulateControlFromBOProperty(step2_TextboxEndDate, GetDateFormattedStringNullable(State.CertItemCoverageBO.EndDate))

        PopulateControlFromBOProperty(step2_TextboxLiabilityLimit, State.CertItemCoverageBO.LiabilityLimits, DECIMAL_FORMAT)
        PopulateControlFromBOProperty(step2_TextboxCoverageType, State.CertItemBO.GetCoverageTypeDescription(State.CertItemCoverageBO.CoverageTypeId))
        PopulateControlFromBOProperty(step2_TextboxDateAdded, GetDateFormattedStringNullable(State.CertItemCoverageBO.CreatedDate))
        PopulateControlFromBOProperty(step2_TextboxDealerItemDesc, State.CertItemBO.ItemDescription)
        PopulateControlFromBOProperty(step2_TextboxInvNum, State.CertBO.InvoiceNumber)
        PopulateControlFromBOProperty(step2_TextboxProductCode, State.CertItemBO.CertProductCode)
        PopulateControlFromBOProperty(step2_TextboxYear, State.CertBO.VehicleYear)
        PopulateControlFromBOProperty(step2_TextboxOdometer, State.CertBO.Odometer)
        PopulateControlFromBOProperty(step2_cboApplyDiscount, State.CertItemCoverageBO.IsDiscount)
        SetSelectedItem(step2_cboCalimAllowed, State.CertItemCoverageBO.IsClaimAllowed)
        PopulateControlFromBOProperty(step2_TextboxDiscountAmt, State.CertItemCoverageBO.DealerDiscountAmt)
        PopulateControlFromBOProperty(step2_TextboxDiscountPercent, State.CertItemCoverageBO.DealerDiscountPercent)
        PopulateControlFromBOProperty(step2_TextboxSKU, State.CertItemBO.SkuNumber)
        PopulateControlFromBOProperty(step2_TextboxRepairDiscountPct, State.CertItemCoverageBO.RepairDiscountPct)
        PopulateControlFromBOProperty(step2_TextboxReplacementDiscountPct, State.CertItemCoverageBO.ReplacementDiscountPct)
        PopulateFormDeductibleFormBOs(MethodOfRepairId)

        If (Not State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
            PopulateControlFromBOProperty(step2_TextboxModel, State.CertItemBO.Model)

        Else
            If (State.VSCModel IsNot Nothing) Then
                PopulateControlFromBOProperty(step2_TextboxModel, State.VSCModel.Model)

            Else
                PopulateControlFromBOProperty(step2_TextboxModel, String.Empty)
            End If
        End If

        If (State.VSCClassCode IsNot Nothing) Then
            PopulateControlFromBOProperty(step2_TextboxClassCode, State.VSCClassCode.Code)
        Else
            PopulateControlFromBOProperty(step2_TextboxClassCode, String.Empty)
        End If
        ''''New Equipment Flow starts here
        Dim msgList As New List(Of String)

        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
            If (State.step2_claimEquipmentBO Is Nothing) Then
                If Not State.CertItemBO.CreateClaimedEquipmentFromEnrolledEquipment(State.step2_claimEquipmentBO, msgList) Then
                    'if Manual entry for Claimed Equipment is required
                    MasterPage.MessageController.AddError(msgList.ToArray)
                    ControlMgr.SetVisibleControl(Me, btnContinue, False)
                Else
                    If msgList.Count > 0 Then MasterPage.MessageController.AddWarning(msgList.ToArray)
                End If
            End If

            PopulateFormfromClaimedEquipmentBO()
            SetCtlsForEquipmentMgmt(True) 'REQ 1106

        End If

    End Sub

    Private Sub PopulateFormForClaim()

        State.ClaimBO.RecalculateDeductibleForChanges()
        State.ClaimIssuesView = State.ClaimBO.GetClaimIssuesView()
        State.ClaimImagesView = State.ClaimBO.GetClaimImagesView()

        PopulateClaimIssuesGrid()
        PopulateClaimImagesGrid()
        PopulateClaimAuthorizationGrid()
        PopulateClaimActionGrid()
        PopulateQuestionAnswerGrid()
        PopulateConsequentialDamageGrid()


        Dim oTranslate As Boolean = True
        Dim oCauseOfLoss As String
        Dim oCoverageType As New CoverageType(State.ClaimBO.CoverageTypeId)

        If Not State.ClaimBO.CauseOfLossId.Equals(Guid.Empty) Then
            Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindById(State.ClaimBO.CauseOfLossId)
            If oCoverageLoss IsNot Nothing Then
                SetSelectedItem(step3_cboCauseOfLossId, State.ClaimBO.CauseOfLossId)
            Else
                SetSelectedItem(step3_cboCauseOfLossId, Guid.Empty)
                oCauseOfLoss = LookupListNew.GetCodeFromId(LookupListNew.LK_CAUSES_OF_LOSS, State.ClaimBO.CauseOfLossId)
                oTranslate = False
                Dim strErrorMess As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE)
                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE, oCauseOfLoss & "  " & strErrorMess)
            End If

        Else
            SetSelectedItem(step3_cboCauseOfLossId, State.ClaimBO.GetCauseOfLossID(State.ClaimBO.CoverageTypeId))
        End If

        If State.ClaimBO.PoliceReport IsNot Nothing Then
            State.PoliceReportBO = State.ClaimBO.PoliceReport
            mcUserControlPoliceReport.Bind(State.PoliceReportBO, moMessageController)
            PanelPoliceReport.Visible = True
            mcUserControlPoliceReport.ChangeEnabledControlProperty(False)
        Else
            State.PoliceReportBO = Nothing
            PanelPoliceReport.Visible = False
        End If

        txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
        txtCreatedDate.Text = DateTime.Now.ToString(LocalizationMgr.CurrentCulture)

        PopulateControlFromBOProperty(step3_TextboxLossDate, State.ClaimBO.LossDate)
        PopulateControlFromBOProperty(step3_TextboxContactName, State.ClaimBO.ContactName)
        PopulateControlFromBOProperty(step3_TextboxCallerName, State.ClaimBO.CallerName)
        PopulateControlFromBOProperty(step3_TextboxProblemDescription, State.ClaimBO.ProblemDescription)
        PopulateControlFromBOProperty(step3_TextboxLiabilityLimit, State.ClaimBO.LiabilityLimit)
        PopulateControlFromBOProperty(step3_TextboxDeductible_WRITE, State.ClaimBO.Deductible)
        PopulateControlFromBOProperty(step3_TextboxLossDate, State.ClaimBO.LossDate)
        PopulateControlFromBOProperty(step3_TextboxReportDate, State.ClaimBO.ReportedDate)
        PopulateControlFromBOProperty(step3_TextboxCertificateNumber, State.ClaimBO.CertificateNumber)
        PopulateControlFromBOProperty(step3_TextboxClaimNumber, State.ClaimBO.ClaimNumber)
        PopulateControlFromBOProperty(step3_TextBoxDiscount, State.ClaimBO.DiscountAmount)
        PopulateControlFromBOProperty(step3_TextboxPolicyNumber, State.ClaimBO.PolicyNumber)
        PopulateControlFromBOProperty(step3_TextboxOutstandingPremAmt, State.ClaimBO.OutstandingPremiumAmount)
        PopulateControlFromBOProperty(step3_TextboxCALLER_TAX_NUMBER, State.ClaimBO.CallerTaxNumber)
        PopulateControlFromBOProperty(step3_txtNewDeviceSKU, State.ClaimBO.NewDeviceSku)
        PopulateControlFromBOProperty(step3_txtPickupDate, State.ClaimBO.PickUpDate)
        PopulateControlFromBOProperty(step3_txtVisitDate, State.ClaimBO.VisitDate)

        SetSelectedItem(step3_cboLawsuitId, State.ClaimBO.IsLawsuitId)
        PopulateControlFromBOProperty(step3_TxtSpecialInstruction, State.ClaimBO.SpecialInstruction)

        If State.IsSalutation Then
            If State.ClaimBO.CallerSalutationID.Equals(Guid.Empty) Then
                SetSelectedItem(step3_cboCallerSalutationId, State.CertBO.SalutationId)
            Else
                SetSelectedItem(step3_cboCallerSalutationId, State.ClaimBO.CallerSalutationID)
            End If

            If State.ClaimBO.ContactSalutationID.Equals(Guid.Empty) Then
                SetSelectedItem(step3_cboContactSalutationId, State.CertBO.SalutationId)
            Else
                SetSelectedItem(step3_cboContactSalutationId, State.ClaimBO.ContactSalutationID)
            End If
        End If

        Dim isNewFulfillment = (Not String.IsNullOrWhiteSpace(State.CertItemCoverageBO.FulfillmentProfileCode))


        If Not State.ClaimBO.ContactInfoId.Equals(Guid.Empty) Then
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            SetSelectedItem(step3_cboUseShipAddress, YesId)
            moUserControlContactInfo.Visible = True

            UserControlAddress.ClaimDetailsBind(State.ClaimBO.ContactInfo.Address)
            UserControlContactInfo.Bind(State.ClaimBO.ContactInfo)
            'This makes all child controls inside UserControlAddress as ReadOnly 
            If isNewFulfillment = True Then
                UserControlAddress.EnableControl(False)
                UserControlContactInfo.EnableControl(False)
            End If

        End If

        If State.ClaimBO.LossDate IsNot Nothing AndAlso State.ClaimBO.ReportedDate IsNot Nothing Then
            'Display warning Message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days
            If State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.ClaimBO.Dealer.IsGracePeriodSpecified AndAlso State.ClaimBO.IsNew Then


                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithValidCoverage(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, False)
                    MasterPage.MessageController.AddWarning(denialMessage)
                End If
                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithinGracePeriod(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, False)
                    MasterPage.MessageController.AddWarning(denialMessage)
                End If

                If (State.ClaimBO.IsClaimReportedWithinGracePeriod(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value)) And
                   (State.ClaimBO.IsClaimReportedWithValidCoverage(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value)) Then
                    If State.ClaimBO.IsMaxReplacementExceeded(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                        MasterPage.MessageController.AddWarning(denialMessage)
                    End If
                End If

            Else
                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithinPeriod(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, False)
                    MasterPage.MessageController.AddWarning(denialMessage)
                End If
                'Display warning Message if the Maximum number of Replacements have been exceeded
                If State.ClaimBO.IsMaxReplacementExceeded(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                    MasterPage.MessageController.AddWarning(denialMessage)
                End If
            End If

        End If

        If State.ClaimBO.CertificateItem.IsEquipmentRequired Then

            'populate best replacement record
            With step3_ReplacementOption
                .thisPage = Me
                .ClaimBO = CType(State.ClaimBO, ClaimBase)
                If (State.ClaimBO.Dealer.UseEquipmentId.Equals(State.yesId)) Then
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With
        End If

        If State.ClaimBO.CertificateItem.IsEquipmentRequired Then
            'populate best replacement record
            With step3_ReplacementOption
                .thisPage = Me
                .ClaimBO = CType(State.ClaimBO, ClaimBase)
                If (State.ClaimBO.Dealer.UseEquipmentId.Equals(State.yesId)) Then
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With
        End If

    End Sub

    'Sub PopulateClaimEquipmentForStep3()
    '    With Me.State.ClaimBO
    '        'enrolled equipment
    '        Dim flag As Boolean = False
    '        If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE_EQUIPMENT_UPDATE) Then flag = True

    '        ControlMgr.SetVisibleControl(Me, Me.step3_ddlEnrolledManuf, flag)
    '        ControlMgr.SetVisibleControl(Me, Me.txtEnrolledMake, Not flag)
    '        ControlMgr.SetVisibleControl(Me, Me.step3_ddlEnrolledSku, flag)
    '        ControlMgr.SetVisibleControl(Me, Me.txtEnrolledSku, Not flag)

    '        Me.SetEnabledForControlFamily(Me.txtEnrolledMake, flag)
    '        Me.SetEnabledForControlFamily(Me.txtEnrolledModel, flag, True)
    '        Me.SetEnabledForControlFamily(Me.txtenrolledSerial, flag, True)
    '        Me.SetEnabledForControlFamily(Me.txtEnrolledSku, flag)

    '        If Not .EnrolledEquipment Is Nothing Then
    '            Dim dvEnrolled As DataView = LookupListNew.GetManufacturerbyEquipmentList(Me.State.DealerBO.EquipmentListCode, Date.Now)
    '            If Not dvEnrolled Is Nothing AndAlso Not BusinessObjectBase.FindRow(.EnrolledEquipment.ManufacturerId, LookupListNew.COL_ID_NAME, dvEnrolled.Table) Is Nothing Then
    '                Me.PopulateControlFromBOProperty(Me.step3_ddlEnrolledManuf, .EnrolledEquipment.ManufacturerId)
    '            Else
    '                Me.MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
    '            End If

    '            'Me.PopulateControlFromBOProperty(Me.step3_ddlEnrolledManuf, .EnrolledEquipment.ManufacturerId)
    '            Me.PopulateControlFromBOProperty(Me.txtEnrolledMake, .EnrolledEquipment.Manufacturer)
    '            Me.PopulateControlFromBOProperty(Me.txtEnrolledModel, .EnrolledEquipment.Model)
    '            Me.PopulateControlFromBOProperty(Me.txtenrolledSerial, .EnrolledEquipment.SerialNumber)
    '            'Me.PopulateControlFromBOProperty(Me.txtEnrolledSku, .EnrolledEquipment.SKU)

    '            '***********************Pre populate Enrolled SKU based on the Equipment and Model
    '            If Not .EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
    '                Dim dv As DataView = Me.State.ClaimBO.CertificateItem.LoadSku(.EnrolledEquipment.EquipmentId, Me.State.ClaimBO.Dealer.Id)
    '                If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
    '                    Me.step3_ddlEnrolledSku.DataSource = dv
    '                    Me.step3_ddlEnrolledSku.DataTextField = "SKU_NUMBER"
    '                    Me.step3_ddlEnrolledSku.DataValueField = "SKU_NUMBER"
    '                    Me.step3_ddlEnrolledSku.DataBind()


    '                    If Not dv Is Nothing AndAlso dv.FindRows(.EnrolledEquipment.SKU).Length > 0 Then
    '                        Me.PopulateControlFromBOProperty(Me.txtEnrolledSku, .EnrolledEquipment.SKU)
    '                        Me.step3_ddlEnrolledSku.SelectedValue = .EnrolledEquipment.SKU
    '                        hdnSelectedEnrolledSku.Value = .EnrolledEquipment.SKU
    '                    End If
    '                End If
    '            End If
    '        End If
    '        '***********************
    '        'claimed equipment
    '        ControlMgr.SetVisibleControl(Me, Me.step3_ddlClaimedManuf, flag)
    '        ControlMgr.SetVisibleControl(Me, Me.txtClaimedMake, Not flag)
    '        ControlMgr.SetVisibleControl(Me, Me.step3_ddlClaimedSku, flag)
    '        ControlMgr.SetVisibleControl(Me, Me.txtClaimedSku, Not flag)

    '        Me.SetEnabledForControlFamily(Me.txtClaimedMake, flag)
    '        Me.SetEnabledForControlFamily(Me.txtClaimedModel, flag, True)
    '        Me.SetEnabledForControlFamily(Me.txtClaimedSerial, flag, True)
    '        Me.SetEnabledForControlFamily(Me.txtClaimedSku, flag)

    '        If Not .ClaimedEquipment Is Nothing Then
    '            Me.PopulateControlFromBOProperty(Me.step3_ddlClaimedManuf, .ClaimedEquipment.ManufacturerId)
    '            Me.PopulateControlFromBOProperty(Me.txtClaimedMake, .ClaimedEquipment.Manufacturer)
    '            Me.PopulateControlFromBOProperty(Me.txtClaimedModel, .ClaimedEquipment.Model)
    '            Me.PopulateControlFromBOProperty(Me.txtClaimedSerial, .ClaimedEquipment.SerialNumber)
    '            'Me.PopulateControlFromBOProperty(Me.txtClaimedSku, .ClaimedEquipment.SKU)

    '            '***********************Pre populate Claimed SKU based on the Equipment and Model
    '            If Not .ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
    '                Dim dv As DataView = Me.State.ClaimBO.CertificateItem.LoadSku(.ClaimedEquipment.EquipmentId, Me.State.ClaimBO.Dealer.Id)
    '                If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
    '                    Me.step3_ddlClaimedSku.DataSource = dv
    '                    Me.step3_ddlClaimedSku.DataTextField = "SKU_NUMBER"
    '                    Me.step3_ddlClaimedSku.DataValueField = "SKU_NUMBER"
    '                    Me.step3_ddlClaimedSku.DataBind()

    '                    If Not dv Is Nothing AndAlso dv.FindRows(.ClaimedEquipment.SKU).Length > 0 Then
    '                        Me.PopulateControlFromBOProperty(Me.txtClaimedSku, .ClaimedEquipment.SKU)
    '                        Me.step3_ddlClaimedSku.SelectedItem.Value = .ClaimedEquipment.SKU
    '                        hdnSelectedClaimedSku.Value = .ClaimedEquipment.SKU
    '                    End If
    '                End If
    '            End If
    '        End If
    '        '***********************
    '    End With

    'End Sub

    Sub PopulateClaimedEnrolledDetails()
        Dim allowEnrolledDeviceUpdate As AttributeValue = State.DealerBO.AttributeValues.FirstOrDefault(Function(attributeValue) attributeValue.Attribute.UiProgCode = Codes.DLR_ATTR_ALLOW_MODIFY_CLAIMED_DEVICE)
        With State.ClaimBO
            If .EnrolledEquipment IsNot Nothing Or .ClaimedEquipment IsNot Nothing Then
                With ucClaimDeviceInfo
                    .thisPage = Me
                    .ClaimBO = CType(State.ClaimBO, ClaimBase)
                    If allowEnrolledDeviceUpdate IsNot Nothing AndAlso allowEnrolledDeviceUpdate.Value = Codes.YESNO_Y Then
                        For Each i As ClaimIssue In State.ClaimBO.ClaimIssuesList
                            If i.IssueCode = ISSUE_CODE_CR_DEVICE_MIS And i.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Then
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

    Private Sub PopulateBOFromFormForStep3()

        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)

        With State.ClaimBO

            PopulateBOProperty(State.ClaimBO, "CauseOfLossId", step3_cboCauseOfLossId)
            PopulateBOProperty(State.ClaimBO, "ContactName", step3_TextboxContactName)
            PopulateBOProperty(State.ClaimBO, "CallerName", step3_TextboxCallerName)
            PopulateBOProperty(State.ClaimBO, "CallerTaxNumber", step3_TextboxCALLER_TAX_NUMBER)
            PopulateBOProperty(State.ClaimBO, "ProblemDescription", step3_TextboxProblemDescription)
            PopulateBOProperty(State.ClaimBO, "Deductible", step3_TextboxDeductible_WRITE)
            PopulateBOProperty(State.ClaimBO, "DiscountAmount", step3_TextBoxDiscount)
            PopulateBOProperty(State.ClaimBO, "PolicyNumber", step3_TextboxPolicyNumber)
            PopulateBOProperty(State.ClaimBO, "LossDate", step3_TextboxLossDate)
            PopulateBOProperty(State.ClaimBO, "ReportedDate", step3_TextboxReportDate)
            PopulateBOProperty(State.ClaimBO, "NewDeviceSku", step3_txtNewDeviceSKU)
            PopulateBOProperty(State.ClaimBO, "IsLawsuitId", step3_cboLawsuitId)
            PopulateBOProperty(State.ClaimBO, "SpecialInstruction", step3_TxtSpecialInstruction)

            If State.ClaimBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                PopulateBOProperty(State.ClaimBO, "LiabilityLimit", step3_TextboxLiabilityLimit)
            End If

            If State.IsSalutation Then
                PopulateBOProperty(State.ClaimBO, "ContactSalutationID", step3_cboContactSalutationId)
                PopulateBOProperty(State.ClaimBO, "CallerSalutationID", step3_cboCallerSalutationId)
            End If

            If PanelPoliceReport.Visible = True Then
                PopulatePoliceReportBOFromUserCtr(True)
            Else
                State.PoliceReportBO = Nothing
            End If

            If State.ClaimBO.ContactInfo IsNot Nothing Then
                If State.ClaimBO.ContactInfo.IsDeleted = False Then
                    State.ClaimBO.ContactInfoId = State.ClaimBO.ContactInfo.Id
                End If
            End If

            If State.IsClaimDenied Then
                PopulateBOProperty(State.ClaimBO, "DeniedReasonId", step3_cboDeniedReason)
                PopulateBOProperty(State.ClaimBO, "Fraudulent", step3_cboFraudulent)
                PopulateBOProperty(State.ClaimBO, "Complaint", step3_cboComplaint)
            End If

            If State.IsClaimDenied Then
                PopulateBOProperty(State.ClaimBO, "DeniedReasonId", step3_cboDeniedReason)
                PopulateBOProperty(State.ClaimBO, "Fraudulent", step3_cboFraudulent)
                PopulateBOProperty(State.ClaimBO, "Complaint", step3_cboComplaint)
            End If

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        If step3_cboUseShipAddress.SelectedValue = State.yesId.ToString Then
            State.ClaimBO.ContactInfo.Address.InforceFieldValidation = True
            UserControlContactInfo.PopulateBOFromControl(True)
            State.ClaimBO.ContactInfo.Save()
        End If

    End Sub

    Private Sub InitialEnableDisableReplacementForStep3()
        ControlMgr.SetVisibleControl(Me, step3_lblNewDeviceSKU, False)
        ControlMgr.SetVisibleControl(Me, step3_txtNewDeviceSKU, False)
        If State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
            ' Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(State.ClaimBO.CertItemCoverageId, State.ClaimBO.MethodOfRepairId)
            State.DEDUCTIBLE_BASED_ON = oDeductible.DeductibleBasedOn
            'req 1157 added additional condition
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.DealerBO.NewDeviceSkuRequiredId) = Codes.YESNO_Y Or (State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                ControlMgr.SetVisibleControl(Me, step3_lblNewDeviceSKU, True)
                ControlMgr.SetVisibleControl(Me, step3_txtNewDeviceSKU, True)
            End If
        End If

    End Sub

    Private Sub InitialEnableDisableControlsForStep3()

        ChangeEnabledProperty(step3_TextboxCertificateNumber, False)
        ChangeEnabledProperty(step3_TextboxClaimNumber, False)
        ChangeEnabledProperty(step3_TextboxLiabilityLimit, False)
        ChangeEnabledProperty(step3_TextboxDeductible_WRITE, True)
        ChangeEnabledProperty(step3_TextBoxDiscount, False)
        ChangeEnabledProperty(step3_txtPickupDate, False)
        ChangeEnabledProperty(step3_txtVisitDate, False)

        'Make Invisible for Service Warranty
        If State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            'Service Warranty Claims
            ControlMgr.SetVisibleControl(Me, step3_LabelLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, step3_TextboxLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, step3_LabelDeductible, False)
            ControlMgr.SetVisibleControl(Me, step3_TextboxDeductible_WRITE, False)
            ControlMgr.SetVisibleControl(Me, step3_LabelDiscount, False)
            ControlMgr.SetVisibleControl(Me, step3_TextBoxDiscount, False)
        End If

        If State.ClaimBO.LossDate IsNot Nothing Then
            'When editing a claim record created from an existing one
            ChangeEnabledProperty(step3_TextboxLossDate, False)
            ControlMgr.SetVisibleControl(Me, step3_ImageButtonLossDate, False)
        End If

        If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, step3_TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, step3_LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, step3_TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, step3_LabelPolicyNumber, False)
        End If

        InitialEnableDisableReplacementForStep3()

        Dim objCompany As New Company(State.ClaimBO.CompanyId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_SERVICES Then
            ControlMgr.SetVisibleControl(Me, step3_TextboxCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, step3_LabelCALLER_TAX_NUMBER, False)
        Else
            ControlMgr.SetVisibleControl(Me, step3_TextboxCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, step3_LabelCALLER_TAX_NUMBER, True)
        End If

        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, State.ClaimBO.CoverageTypeId, State.ClaimBO.Certificate.DealerId, Authentication.LangId, State.ClaimBO.Certificate.ProductCode, False) Then
            ChangeEnabledProperty(step3_cboCauseOfLossId, True)
        End If
    End Sub

    Private Sub EnableDisableControlsForStep3()
        If State.IsSalutation Then
            ControlMgr.SetVisibleControl(Me, step3_cboContactSalutationId, True)
            ControlMgr.SetVisibleControl(Me, step3_cboCallerSalutationId, True)
        Else
            ControlMgr.SetVisibleControl(Me, step3_cboContactSalutationId, False)
            ControlMgr.SetVisibleControl(Me, step3_cboCallerSalutationId, False)
        End If

        If State.ClaimBO.ClaimNumber Is Nothing Then
            ControlMgr.SetVisibleControl(Me, step3_LabelClaimNumber, False)
            ControlMgr.SetVisibleControl(Me, step3_TextboxClaimNumber, False)
        End If

        If State.ClaimBO.IsDaysLimitExceeded Then
            MasterPage.MessageController.Clear()
            MasterPage.MessageController.AddWarning("DAYS_LIMIT_EXCEEDED")
        End If
        If State.ClaimBO.IsAuthorizationLimitExceeded Then
            moMessageController.Clear()
            moMessageController.AddWarning(String.Format("{0}: {1}",
                                                            TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                            TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If

        If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, step3_LabelUseShipAddress, True)
            ControlMgr.SetVisibleControl(Me, step3_cboUseShipAddress, True)
        End If

        If State.ClaimBO.Contract.PayOutstandingPremiumId.Equals(State.yesId) Then
            State.PayOutstandingPremium = True
            ControlMgr.SetVisibleControl(Me, step3_LabelOutstandingPremAmt, True)
            ControlMgr.SetVisibleControl(Me, step3_TextboxOutstandingPremAmt, True)
        Else
            State.PayOutstandingPremium = False
            ControlMgr.SetVisibleControl(Me, step3_LabelOutstandingPremAmt, False)
            ControlMgr.SetVisibleControl(Me, step3_TextboxOutstandingPremAmt, False)
        End If

        'If No issues to Add to claim hide the Save and Cancel Button
        If (State.ClaimBO.Load_Filtered_Issues().Count = 0) Then
            MessageLiteral.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_ISSUES_FOUND")
            modalMessageBox.Attributes.Add("class", "infoMsg")
            modalMessageBox.Attributes.Add("style", "display: block")
            imgIssueMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
            step3_modalClaimIssue_btnSave.Visible = False
            step3_modalClaimIssue_btnCancel.Visible = False
        Else
            step3_modalClaimIssue_btnSave.Visible = True
            step3_modalClaimIssue_btnCancel.Visible = True
            modalMessageBox.Attributes.Add("class", "errorMsg")
            modalMessageBox.Attributes.Add("style", "display: none")
            imgIssueMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
        End If

        'if the use equipment is not Yes then hide the device inforamtion and best replacement options
        If State.ClaimBO.CertificateItem.IsEquipmentRequired Then
            step3_ReplacementOption.Visible = True
            'Me.dvClaimEquipment.Visible = True
        Else
            step3_ReplacementOption.Visible = False
            'Me.dvClaimEquipment.Visible = False
        End If

    End Sub

    Private Sub BindBoPropertiesToLabels(wizardStep As ClaimWizardSteps)

        Select Case wizardStep
            Case ClaimWizardSteps.Step2
                BindBOPropertyToLabel(State.CertItemBO, "RiskTypeId", step2_LabelRiskTypeId)
                BindBOPropertyToLabel(State.CertItemBO, "ManufacturerId", step2_LabelMakeId)
                BindBOPropertyToLabel(State.CertItemBO, "SerialNumber", step2_LabelSerialNumber)
                BindBOPropertyToLabel(State.CertItemBO, "Model", step2_LabelModel)
                BindBOPropertyToLabel(State.CertItemBO, "DealerItemDesc", step2_LabelDealerItemDesc)
                BindBOPropertyToLabel(State.CertItemBO, "SkuNumber", step2_labelSKU)
                BindBOPropertyToLabel(State.CertItemBO, "BeginDate", step2_LabelBeginDate)
                BindBOPropertyToLabel(State.CertItemBO, "EndDate", step2_LabelEndDate)
                BindBOPropertyToLabel(State.CertItemBO, "CreatedDate", step2_LabelDateAdded)
                BindBOPropertyToLabel(State.CertItemBO, "GetCoverageTypeDescription", step2_LabelCoverageType)
                BindBOPropertyToLabel(State.CertItemBO, "MethodOfRepairId", step2_LabelMethodOfRepair)
                BindBOPropertyToLabel(State.CertItemBO, "DealerDiscountAmt", step2_LabelDeductible)
                BindBOPropertyToLabel(State.CertItemBO, "DealerDiscountPercent", step2_LabelDeductiblePercent)
                BindBOPropertyToLabel(State.CertItemBO, "LiabilityLimits", step2_LabelLiabilityLimit)
                BindBOPropertyToLabel(State.CertItemBO, "ProductCode", step2_LabelProductCode)
                BindBOPropertyToLabel(State.CertItemBO, "InvoiceNumber", step2_LabelInvNum)
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
                    BindBOPropertyToLabel(State.step2_claimEquipmentBO, "ManufacturerId", step2_lblClaimedMake)
                    BindBOPropertyToLabel(State.step2_claimEquipmentBO, "Model", step2_lblClaimedModel)
                    BindBOPropertyToLabel(State.step2_claimEquipmentBO, "SKU", step2_lblClaimedSKU)
                    BindBOPropertyToLabel(State.step2_claimEquipmentBO, "EquipmentDescription", step2_LabelClaimDesc)
                    BindBOPropertyToLabel(State.step2_claimEquipmentBO, "SerialNumber", step2_LabelClaimSerialNumber)
                End If
            Case ClaimWizardSteps.Step3
                BindBOPropertyToLabel(State.ClaimBO, "CauseOfLossId", step3_LabelCauseOfLossId)
                BindBOPropertyToLabel(State.ClaimBO, "ContactName", step3_LabelContactName)
                BindBOPropertyToLabel(State.ClaimBO, "CallerName", step3_LabelCallerName)
                BindBOPropertyToLabel(State.ClaimBO, "ProblemDescription", step3_LabelProblemDescription)
                BindBOPropertyToLabel(State.ClaimBO, "LiabilityLimit", step3_LabelLiabilityLimit)
                BindBOPropertyToLabel(State.ClaimBO, "Deductible", step3_LabelDeductible)
                BindBOPropertyToLabel(State.ClaimBO, "DiscountAmount", step3_LabelDiscount)
                BindBOPropertyToLabel(State.ClaimBO, "LossDate", step3_LabelLossDate)
                BindBOPropertyToLabel(State.ClaimBO, "ReportedDate", step3_LabelReportDate)
                BindBOPropertyToLabel(State.ClaimBO, "CertificateNumber", step3_LabelCertificateNumber)
                BindBOPropertyToLabel(State.ClaimBO, "PolicyNumber", step3_LabelPolicyNumber)
                BindBOPropertyToLabel(State.ClaimBO, "CallerTaxNumber", step3_LabelCALLER_TAX_NUMBER)
                BindBOPropertyToLabel(State.ClaimBO, "NewDeviceSKU", step3_lblNewDeviceSKU)
                BindBOPropertyToLabel(State.ClaimBO, "IsLawsuitId", step3_LabelIsLawsuitId)
                BindBOPropertyToLabel(State.ClaimBO, "DeniedReasonId", step3_lblDeniedReason)
                BindBOPropertyToLabel(State.ClaimBO, "Fraudulent", step3_lblPotFraudulent)
                BindBOPropertyToLabel(State.ClaimBO, "Complaint", step3_lblComplaint)
                BindBOPropertyToLabel(State.ClaimBO, "PickUpDate", step3_lblPickupDate)
                BindBOPropertyToLabel(State.ClaimBO, "VisitDate", step3_lblVisitDate)


            Case ClaimWizardSteps.Step5
                BindBOPropertyToLabel(State.CommentBO, "CreatedDate", step5_LabelDateTime)
                BindBOPropertyToLabel(State.CommentBO, "CallerName", step5_LabelCallerName)
                BindBOPropertyToLabel(State.CommentBO, "CommentTypeId", step5_LabelCommentType)
        End Select
        ClearGridHeadersAndLabelsErrSign()
    End Sub

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

    'Private Function CheckIfComingFromCreateClaimConfirm() As Boolean
    '    Dim returnValue As Boolean = False
    '    Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

    '    If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
    '        'Me.CreateClaim()
    '        returnValue = True
    '    ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
    '        'Nothing To Do. Stay on the page
    '    End If
    '    Return returnValue
    'End Function

    Private Sub PopulatePoliceReportBOFromUserCtr(blnExcludePoliceReportSave As Boolean)
        With State.PoliceReportBO
            .ClaimId = State.ClaimBO.Id
            ' determine validate or dont validate
            mcUserControlPoliceReport.PopulateBOFromControl(blnExcludePoliceReportSave)
        End With
    End Sub

    Private Sub GoToNextStep()
        State.StepName = NextStep()
        State.IsEditMode = False
        PopulateStepUIandData()
        BindBoPropertiesToLabels(State.StepName)
        AddStepLabelDecorations(State.StepName)
    End Sub

    Private Function IsUserAuthorisedToProceed(wizardStep As ClaimWizardSteps) As Boolean
        Select Case wizardStep
            Case ClaimWizardSteps.Step2

        End Select
    End Function

    Private Sub CreateClaim()

        Dim oldStatus As BasicClaimStatus = State.ClaimBO.Status
        Try
            State.ClaimBO.CreateClaim()
            If Not State.DealerBO.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                State.ClaimBO.PrepopulateDeductible()
            End If
        Catch ex As Exception
            State.ClaimBO.Status = oldStatus
            Throw ex
        End Try

    End Sub

    Private Sub PopulateFormForServiceCenter()
        PopulateCurrentSearchType()
        PopulateCountryDropdown()
        PopulateGridOrDropDown()
    End Sub

    Sub PopulateCurrentSearchType()
        Try
            Select Case State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    step4_RadioButtonByZip.Checked = True
                Case LocateServiceCenterSearchType.ByCity
                    step4_RadioButtonByCity.Checked = True
                Case LocateServiceCenterSearchType.All
                    step4_RadioButtonAll.Checked = True
                Case LocateServiceCenterSearchType.None
                    step4_RadioButtonNO_SVC_OPTION.Checked = True
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateCountryDropdown()
        Dim address As New Address(State.CertBO.AddressId)
        Dim dv As ServiceCenter.ServiceCenterSearchDV
        Dim CountryId As Guid
        CountryId = address.CountryId
        dv = ServiceCenter.getUserCustCountries(CountryId)
        BindListControlToDataView(step4_moCountryDrop, dv, , , False)
        SetSelectedItem(step4_moCountryDrop, CountryId)
    End Sub

    Public Sub PopulateGridOrDropDown()
        'get the customer countryId
        Try
            Dim ServNetworkSvc As New ServiceNetworkSvc
            Dim address As New Address(State.CertBO.AddressId)
            Dim objCompany As New Company(State.CertBO.CompanyId)
            Dim UseZipDistrict As Boolean = True
            Dim DealerType As String
            Dim MethodOfRepairType As String, FlagMethodOfRepairRecovery As Boolean = False

            If objCompany.UseZipDistrictId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")) Then
                UseZipDistrict = False
            End If

            Dim SelectedCountry As New ArrayList
            Dim isNetwork As Boolean
            SelectedCountry.Add(GetSelectedItem(step4_moCountryDrop))
            'Dim srvCenterdIDs As ArrayList = Nothing
            If Not State.DealerBO.ServiceNetworkId.Equals(Guid.Empty) Then
                ServNetworkSvc.ServiceNetworkId = State.DealerBO.ServiceNetworkId
                isNetwork = True
            Else
                isNetwork = False
            End If

            'srvCenterdIDs = ServNetworkSvc.ServiceCentersIDs(isNetwork, cert.MethodOfRepairId)
            DealerType = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.DealerBO.DealerTypeId)  '= Codes.DEALER_TYPES__VSC


            If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Or
               State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__GENERAL Or
               State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__LEGAL Then
                FlagMethodOfRepairRecovery = True
            End If

            Dim dv As ServiceCenter.LocateServiceCenterResultsDv
            Select Case State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    dv = ServiceCenter.LocateServiceCenterByZip(State.CertBO.DealerId, State.CertBO.AddressChild.ZipLocator, State.ClaimBO.RiskTypeId, State.CertItemBO.ManufacturerId, State.CertItemCoverageBO.CoverageTypeCode, SelectedCountry, State.DealerBO.ServiceNetworkId, isNetwork, State.ClaimBO.MethodOfRepairId, UseZipDistrict, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.ByCity
                    dv = ServiceCenter.LocateServiceCenterByCity(State.CertBO.DealerId, State.CertBO.AddressChild.ZipLocator, step4_TextboxCity.Text, State.ClaimBO.RiskTypeId, State.CertItemBO.ManufacturerId, State.CertItemCoverageBO.CoverageTypeCode, SelectedCountry, State.DealerBO.ServiceNetworkId, isNetwork, State.ClaimBO.MethodOfRepairId, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.All
                    Dim selectedSCIds As New ArrayList
                    selectedSCIds.Add(step4_moMultipleColumnDrop.SelectedGuid)
                    State.SelectedServiceCenterId = step4_moMultipleColumnDrop.SelectedGuid
                    PopulateCountryDropdown(SelectedCountry)
                    PopulateServiceCenterGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, State.CertBO.DealerId, State.CertItemBO.ManufacturerId).Tables(0).DefaultView)

                    'REQ-5546
                Case LocateServiceCenterSearchType.None
                    Dim selectedSCIds As New ArrayList

                    'Default SC might not null
                    If Not State.default_service_center_id.Equals(Guid.Empty) Then
                        State.SelectedServiceCenterId = State.default_service_center_id
                    Else
                        MasterPage.MessageController.AddError("TEMP SERVICE CENTER NOT FOUND")
                        Exit Sub
                    End If

                    selectedSCIds.Add(State.SelectedServiceCenterId)
                    PopulateCountryDropdown(SelectedCountry)
                    PopulateServiceCenterGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, State.CertBO.DealerId, State.CertItemBO.ManufacturerId).Tables(0).DefaultView)
            End Select

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateServiceCenterGrid(ByRef dv As DataView)
        Dim MethodOfRepairType As String
        Dim dvCount As Integer = 0

        'cache the search results for multiple grid pages
        State.ServiceCenterView = dv

        If State.ServiceCenterView IsNot Nothing Then
            dvCount = State.ServiceCenterView.Count
        End If

        ShowServiceCenterGridData(State.ServiceCenterView)
        HandleGridMessages(dvCount, True)
    End Sub

    Private Sub PopulateCountryDropdown(customerCountry As ArrayList)
        Try
            'clear results from intelligent search to reduce the cache storage
            State.ServiceCenterView = Nothing

            Dim dv As DataView, strFilter As String

            dv = ServiceCenter.GetAllServiceCenter(customerCountry, State.ClaimBO.MethodOfRepairId, IsUserCompaniesWithAcctSettings())

            Dim overRideSingularity As Boolean
            If dv.Count > 0 Then
                overRideSingularity = True
            Else
                overRideSingularity = False
            End If

            step4_moMultipleColumnDrop.SetControl(True, step4_moMultipleColumnDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER), overRideSingularity)
            If Not State.SelectedServiceCenterId.Equals(Guid.Empty) Then
                step4_moMultipleColumnDrop.SelectedGuid = State.SelectedServiceCenterId
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function IsUserCompaniesWithAcctSettings() As Boolean
        Dim objAC As AcctCompany
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 0 OrElse (ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 1 AndAlso ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).IsNew = True) Then
            Return False
        Else
            For Each objAC In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies()
                Dim certCompany As New Company(State.CertBO.CompanyId)
                If certCompany.AcctCompanyId.Equals(objAC.Id) Then
                    If objAC.UseAccounting = "Y" AndAlso objAC.AcctSystemId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), FelitaEngine.FELITA_PREFIX) Then Return True
                End If
            Next
            Return False
        End If

    End Function

    Private Sub ShowServiceCenterGridData(dv As DataView)
        Try
            Dim dataSet As New DataSet
            hdnSelectedServiceCenterId.Value = "XXXX"
            Dim xdoc As New System.Xml.XmlDocument
            dataSet.Tables.Add(dv.ToTable())
            xdoc.LoadXml(dataSet.GetXml())
            xmlSource.TransformSource = HttpContext.Current.Server.MapPath("~/Certificates/LocateServiceCenter.xslt")
            xmlSource.TransformArgumentList = New XsltArgumentList
            xmlSource.TransformArgumentList.AddParam("recordCount", String.Empty, 30)
            xmlSource.Document = xdoc
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearMessageControllers()
        MasterPage.MessageController.Clear()
        moMessageController.Clear()
        moModalCollectDivMsgController.Clear()
        mcIssueStatus.Clear()
    End Sub

    Private Sub AddStepLabelDecorations(wizardStep As ClaimWizardSteps)
        Select Case wizardStep
            Case ClaimWizardSteps.Step2
                MyBase.AddLabelDecorations(State.CertItemBO)
                If State.step2_claimEquipmentBO IsNot Nothing Then MyBase.AddLabelDecorations(State.step2_claimEquipmentBO)
            Case ClaimWizardSteps.Step3
                MyBase.AddLabelDecorations(State.ClaimBO)
                If State.ClaimBO.EnrolledEquipment IsNot Nothing Then
                    MyBase.AddLabelDecorations(State.ClaimBO.EnrolledEquipment)
                End If
                If State.ClaimBO.ClaimedEquipment IsNot Nothing Then
                    MyBase.AddLabelDecorations(State.ClaimBO.ClaimedEquipment)
                End If
            Case ClaimWizardSteps.Step5
                MyBase.AddLabelDecorations(State.CommentBO)
        End Select
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


    Private Sub PopulateSoftQuestionTree()
        Dim rootNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(TranslationBase.TranslateLabelOrMessage("Soft Questions"), Guid.Empty.ToString)

        Dim softQuestDV As SoftQuestion.SoftQuestionDV, blnFromCert As Boolean = False
        If State.RiskTypeId.Equals(Guid.Empty) Then
            softQuestDV = SoftQuestion.getSoftQuestionGroups(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            softQuestDV = SoftQuestion.getSoftQuestionGroupForRiskType(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, State.RiskTypeId)
            blnFromCert = True
        End If

        Dim intChildCnt As Integer
        'Dim drv As DataRowView
        For Each drv As DataRowView In softQuestDV
            intChildCnt = 0
            Integer.TryParse(drv(SoftQuestion.SoftQuestionDV.COL_NAME_CHILD_COUNT).ToString, intChildCnt)
            Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv(SoftQuestion.SoftQuestionDV.COL_NAME_DESCRIPTION).ToString, GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_ID)).ToString & "|" & GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_GROUP_ID)).ToString & "|" & intChildCnt)
            If intChildCnt > 0 Then
                With newNode
                    .PopulateOnDemand = True
                    .Expanded = False
                End With
            End If
            If blnFromCert Then
                newNode.SelectAction = TreeNodeSelectAction.None
                newNode.NavigateUrl = "javascript:void(0);"
            Else
                newNode.SelectAction = TreeNodeSelectAction.Select
            End If
            rootNode.ChildNodes.Add(newNode)
        Next

        If blnFromCert Then
            rootNode.SelectAction = TreeNodeSelectAction.None
        Else
            rootNode.SelectAction = TreeNodeSelectAction.Select
            rootNode.Selected = True
        End If
        tvQuestion.Nodes.Add(rootNode)
    End Sub
#End Region

#Region "Validation Functions"
    Private Function IsValidDate(ctrl As TextBox, ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
        If (Not String.IsNullOrEmpty(ctrl.Text)) Then
            If (DateHelper.IsDate(ctrl.Text) = False) Then
                AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE), errMsg)
                Return False
            End If
        Else
            AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR), errMsg)
            Return False
        End If
        Return True

    End Function

    Private Function IsDateGreaterThan(firstDate As Date, secondDate As Date) As Boolean
        If (firstDate > secondDate) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsTextBoxFilled(ctrl As TextBox, ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
        If (ctrl.Text = String.Empty) Then
            AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR), errMsg)
            Return False
        End If
        Return True
    End Function

    Private Function IsDropDownSelected(ctrl As DropDownList, ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
        If (ctrl.SelectedIndex < 1) Then
            AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR), errMsg)
            Return False
        End If
        Return True
    End Function

    Private Sub AddError(labelControl As Label, message As String, ByRef errMsg As List(Of String))
        errMsg.Add(labelControl.Text & " : " & message)
    End Sub

    Public Function ValidateCertItem(ByRef errMsg As List(Of String), ByRef warningMsg As List(Of String)) As Boolean
        Dim flag As Boolean = True
        flag = State.CertItemCoverageBO.IsCoverageValidToOpenClaim(errMsg, warningMsg, step1_txtDateReported.Text)
        If (GetSelectedItem(step2_cboCalimAllowed).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N))) Then
            flag = flag And False
        End If
        Return flag
    End Function

    Private Sub SetCtlsForEquipmentMgmt(toggleVisible As Boolean)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
            'text box and drop down list
            ControlMgr.SetVisibleControl(Me, step2_ddlClaimedManuf, Not toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_txtClaimedModel, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_txtClaimedSKu, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_txtClaimedDescription, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_txtClaimedSerialNumber, toggleVisible)

            'lables
            ControlMgr.SetVisibleControl(Me, step2_lblClaimedEquipment, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_lblClaimedMake, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_lblClaimedModel, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_LabelClaimDesc, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_lblClaimedSKU, toggleVisible)
            ControlMgr.SetVisibleControl(Me, step2_LabelClaimSerialNumber, toggleVisible)

            'for enrolled equipment
            ControlMgr.SetVisibleControl(Me, step2_LabelYear, False)
            ControlMgr.SetVisibleControl(Me, step2_TextboxYear, False)
            step2_TextboxYear.ReadOnly = True  'force to be read only

            ''verify equipment
            'If Me.State.CertItemBO.EquipmentId.Equals(Guid.Empty) Then
            '    ControlMgr.SetVisibleControl(Me, BtnVerifyEquipment_WRITE, Not Me.State.IsEditMode)
            'Else
            '    ControlMgr.SetVisibleControl(Me, BtnVerifyEquipment_WRITE, False)
            'End If
        End If
    End Sub


    Private Function ValidateInputForStep1BtnSearch() As Boolean
        Dim errMsg As List(Of String) = New List(Of String)
        Dim flag As Boolean = True
        If (IsValidDate(step1_moDateOfLossText, step1_lblDateOfLoss, errMsg) And
            IsValidDate(step1_txtDateReported, step1_lblDateReported, errMsg)) Then
            If (IsDateGreaterThan(DateHelper.GetDateValue(step1_moDateOfLossText.Text), DateHelper.GetDateValue(step1_txtDateReported.Text))) Then
                AddError(step1_lblDateOfLoss, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_INCIDENT_ERR), errMsg)
                flag = False
            End If
        Else
            flag = False
        End If

        If flag = False Then
            MasterPage.MessageController.AddError(errMsg.ToArray, False)
        End If

        Return flag
    End Function

    Private Function ValidateInputs(wizardStep As ClaimWizardSteps) As Boolean
        Dim errMsg As List(Of String) = New List(Of String)
        Dim warningMsg As List(Of String) = New List(Of String)
        Dim flag As Boolean = True
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                flag = ValidateInputForStep1BtnSearch()
                If (flag And IsDropDownSelected(step1_cboRiskType, step1_riskTypeLabel, errMsg) And
                    IsDropDownSelected(step1_cboCoverageType, step1_coverageTypeLabel, errMsg) And
                    IsTextBoxFilled(step1_textCallerName, step1_callerNameLabel, errMsg) And
                    IsTextBoxFilled(step1_textProblemDescription, step1_problemDescriptionLabel, errMsg)) Then
                    flag = True
                Else
                    flag = False
                End If
            Case ClaimWizardSteps.Step2
                flag = ValidateCertItem(errMsg, warningMsg)
                If State.CertItemBO.IsEquipmentRequired Then
                    'Validate Claimed and Enrolled Equipment - New Equipment Flow
                    If State.step2_claimEquipmentBO IsNot Nothing Then
                        Dim msgList As New List(Of String)
                        If Not State.step2_claimEquipmentBO.ValidateForClaimProcess(msgList) Then
                            MasterPage.MessageController.AddError(msgList.ToArray, True)
                            flag = flag And False
                        End If
                    End If
                End If
            Case ClaimWizardSteps.Step3
                Dim reportDate As Date
                reportDate = step3_TextboxReportDate.Text
                flag = State.CertItemCoverageBO.IsCoverageValidToOpenClaim(errMsg, warningMsg, reportDate)

                If State.CertItemBO.IsEquipmentRequired Then
                    Dim msgList As New List(Of String)
                    If Not State.ClaimBO.ClaimedEquipment.ValidateForClaimProcess(msgList) Then
                        MasterPage.MessageController.AddError(msgList.ToArray, True)
                        flag = flag And False
                    End If

                    'check for Enrolled equipment
                    If State.ClaimBO.EnrolledEquipment IsNot Nothing Then
                        If Not State.ClaimBO.EnrolledEquipment.ValidateForClaimProcess(msgList) Then
                            MasterPage.MessageController.AddError(msgList.ToArray, True)
                            flag = flag And False
                        End If
                    End If
                End If
                flag = ValidateClaim(errMsg)
            Case ClaimWizardSteps.Step4
                If (hdnSelectedServiceCenterId.Value = "XXXX") Then
                    flag = flag And False
                    errMsg.Add("SELECT SERVICE CENTER")
                End If
            Case ClaimWizardSteps.Step5
        End Select

        If errMsg.Count > 0 Then MasterPage.MessageController.AddError(errMsg.ToArray, True)
        If warningMsg.Count > 0 Then MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)

        Return flag
    End Function

    Private Sub CheckForPossibleWarrantyClaim(ByRef warningMsg As List(Of String))
        Dim claimsDV As DataView = State.CertItemCoverageBO.GetAllClaims(State.CertItemCoverageBO.CoverageTypeId)
        Dim oClaim As Claim
        Dim isDaysLimitExceeded As Boolean = True
        Dim elpasedDaysSinceRepaired As Long

        For Each row As DataRow In claimsDV.Table.Rows
            Dim clmId As Guid = New Guid(CType(row.Item(0), Byte()))
            oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(clmId)
            elpasedDaysSinceRepaired = oClaim.ServiceCenterObject.ServiceWarrantyDays.Value

            If Not oClaim.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                If oClaim.PickUpDate IsNot Nothing Then
                    elpasedDaysSinceRepaired = Date.Now.Subtract(oClaim.PickUpDate.Value).Days
                ElseIf oClaim.RepairDate IsNot Nothing Then
                    elpasedDaysSinceRepaired = Date.Now.Subtract(oClaim.RepairDate.Value).Days
                End If
                If elpasedDaysSinceRepaired < oClaim.ServiceCenterObject.ServiceWarrantyDays.Value Then
                    isDaysLimitExceeded = False
                    Exit For
                End If
            End If
        Next

        Dim oContract As Contract
        Dim CoverageType As String

        oContract = Contract.GetContract(State.DealerBO.Id, State.CertBO.WarrantySalesDate.Value)
        CoverageType = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), State.CertItemCoverageBO.CoverageTypeId)

        Dim ClaimControl As Boolean = False

        If oContract IsNot Nothing Then
            If LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oContract.ClaimControlID) = "Y" Then
                ClaimControl = True
            End If
        End If

        If CoverageType <> Codes.COVERAGE_TYPE__MANUFACTURER Then
            If Not State.CertBO.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) _
               And ClaimControl Then
                warningMsg.Add(Message.MSG_DEALER_USER_CLAIM_INTERFACES)
            End If
            If Not isDaysLimitExceeded And
               Not ClaimControl Then
                warningMsg.Add(Message.MSG_POTENTIAL_SERVICE_WARRANTY)
            End If
        End If

    End Sub

    Private Function ValidateClaim(ByRef errMsg As List(Of String)) As Boolean
        Dim flag As Boolean = True
        If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            If State.ClaimBO.PolicyNumber Is Nothing Then
                flag = flag And False
                errMsg.Add(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
            End If
        End If

        If State.ClaimBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.DealerBO.IsGracePeriodSpecified Then
            Return True
        Else
            State.ClaimBO.IsRequiredCheckLossDateForCancelledCert = True
            State.ClaimBO.Validate()
            Return flag
        End If
    End Function

#End Region

#Region "Data Grids Logic"

#Region "MasterClaimGrid"
    Private Sub PopulateMasterClaimGrid()
        Dim allowDifferentCoverage As Boolean = False
        Dim oContract As Contract = Contract.GetContract(State.CertBO.DealerId, State.CertBO.WarrantySalesDate.Value)
        If oContract IsNot Nothing _
           AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then allowDifferentCoverage = True

        Dim dv As Claim.MaterClaimDV = Claim.getList(State.CertItemCoverageBO.Id, allowDifferentCoverage, State.CertBO.getMasterclaimProcFlag, State.DateOfLoss)
        grdMasterClaim.DataSource = dv
        grdMasterClaim.DataBind()
    End Sub

    Private Sub GrdMasterClaimGrid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMasterClaim.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GrdMasterClaimGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMasterClaim.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnSelectClaim As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(0).FindControl("btnSelectClaim") IsNot Nothing) Then
                    btnSelectClaim = CType(e.Row.Cells(0).FindControl("btnSelectClaim"), LinkButton)
                    btnSelectClaim.CommandArgument = CType(dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER), String)
                    btnSelectClaim.CommandName = SELECT_ACTION_COMMAND
                    btnSelectClaim.Text = CType(dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER), String)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GrdMasterClaimGrid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdMasterClaim.RowCommand

        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
                    State.MasterClaimNumber = e.CommandArgument.ToString
                    State.DateOfLoss = DateHelper.GetDateValue(CType(row.Cells(2), DataControlFieldCell).Text)
                    GoToNextStep()
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Claim Issues Grid related"

    Public Sub PopulateClaimIssuesGrid()

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

        If (State.ClaimBO.HasIssues) Then
            Select Case State.ClaimBO.IssuesStatus()
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
            PopulateClaimIssuesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedClaimIssueId = Guid.Empty
            PopulateClaimIssuesGrid()
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
                If String.IsNullOrWhiteSpace(dvRow(Claim.ClaimIssuesView.COL_CREATED_DATE).ToString) = False Then
                    e.Row.Cells(GRID_COL_CREATED_DATE_IDX).Text = GetLongDate12FormattedStringNullable(dvRow(Claim.ClaimIssuesView.COL_CREATED_DATE).ToString)
                End If
                If String.IsNullOrWhiteSpace(dvRow(Claim.ClaimIssuesView.COL_PROCESSED_DATE).ToString) = False Then
                    e.Row.Cells(GRID_COL_PROCESSED_DATE_IDX).Text = GetLongDate12FormattedStringNullable(dvRow(Claim.ClaimIssuesView.COL_PROCESSED_DATE).ToString)
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
                    callPage(ClaimIssueDetailForm.URL, New ClaimIssueDetailForm.Parameters(CType(State.ClaimBO, ClaimBase), State.SelectedClaimIssueId))
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
            PopulateClaimIssuesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Claim Image Related Grid"

    Private Sub GridClaimImages_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (e.Row.Cells(0).FindControl("btnImageLink") IsNot Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    'btnLinkImage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte()))
                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), State.ClaimBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
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
            State.ClaimImagesView = State.ClaimBO.GetClaimImagesView
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

#Region "Claim Authorization Grid related"

    Public Sub PopulateClaimAuthorizationGrid()
        GridClaimAuthorization.AutoGenerateColumns = False
        GridClaimAuthorization.DataSource = State.ClaimBO.ClaimAuthorizationChildren.OrderBy(Function(x) (x.AuthorizationNumber)).ToList()
        GridClaimAuthorization.DataBind()
        If (State.ClaimBO.ClaimAuthorizationChildren.Count > 0) Then
            State.IsGridVisible = True
        Else
            State.IsGridVisible = False
        End If
        ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, State.IsGridVisible)
    End Sub

    Private Sub GridClaimAuthorization_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound

        Try
            Dim claimAuth As ClaimAuthorization = CType(e.Row.DataItem, ClaimAuthorization)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                ' Convert short status codes to full description with css
                e.Row.Cells(GRIDCLA_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, claimAuth.ClaimAuthorizationStatusCode)
                e.Row.Cells(GRIDCLA_COL_AMOUNT_IDX).Text = GetAmountFormattedString(claimAuth.AuthorizedAmount.Value)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Claim Case Grid Related Functions"

    Public Sub PopulateClaimActionGrid()

        Try
            If (State.ClaimActionListDV Is Nothing) Then
                State.ClaimActionListDV = CaseAction.GetClaimActionList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
                State.CaseQuestionAnswerListDV = CaseQuestionAnswer.getClaimCaseQuestionAnswerList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
    Private Sub CaseQuestionAnswerGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CaseQuestionAnswerGrid.RowDataBound
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                Dim strCreationDate As String = Convert.ToString(e.Row.Cells(CaseQuestionAnswerGridColCreationDateIdx).Text)
                strCreationDate = strCreationDate.Replace("&nbsp;", "")
                If String.IsNullOrWhiteSpace(strCreationDate) = False Then
                    Dim tempCreationDate = Convert.ToDateTime(e.Row.Cells(CaseQuestionAnswerGridColCreationDateIdx).Text.Trim())
                    Dim formattedCreationDate = GetLongDate12FormattedString(tempCreationDate)
                    e.Row.Cells(CaseQuestionAnswerGridColCreationDateIdx).Text = Convert.ToString(formattedCreationDate)
                End If

                Dim answerValue = e.Row.Cells(CaseQuestionAnswerGridColAnswerIdx).Text
                If String.IsNullOrWhiteSpace(answerValue) = False Then
                    If (CheckDateAnswer(answerValue) = True) Then
                        e.Row.Cells(CaseQuestionAnswerGridColAnswerIdx).Text = GetDateFormattedStringNullable(e.Row.Cells(CaseQuestionAnswerGridColAnswerIdx).Text)
                    End If
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function CheckDateAnswer(strInput As String) As Boolean

        Dim formatProvider = LocalizationMgr.CurrentFormatProvider
        Dim strOutput As String = Nothing
        If IsDate(strInput) Then
            CheckDateAnswer = True
        Else
            CheckDateAnswer = False
        End If

        Return CheckDateAnswer

    End Function
#End Region

#Region "Claim Consequential Damage Related Functions"
    Public Sub PopulateConsequentialDamageGrid()
        If CalledUrl = ClaimIssueDetailForm.URL Then
            ucClaimConsequentialDamage.UpdateConsequentialDamagestatus(CType(State.ClaimBO, ClaimBase))
        End If
        ucClaimConsequentialDamage.PopulateConsequentialDamage(CType(State.ClaimBO, ClaimBase))
    End Sub
#End Region
#End Region

#Region "Claim Authorization - Fulfillment Authorization Data"
    Private Shared Function GetClient() As FulfillmentServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE), False)
        Dim client = New FulfillmentServiceClient("CustomBinding_IFulfillmentService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function

    Private Function CallStartFulfillmentProcess() As Boolean
        Dim wsRequest As BaseFulfillmentRequest = New BaseFulfillmentRequest()
        Dim wsCBFRequest As New Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentRequest()
        Dim blnSuccess As Boolean = True
        wsRequest.CompanyCode = State.ClaimBO.Company.Code
        wsRequest.ClaimNumber = State.ClaimBO.ClaimNumber
        wsCBFRequest.CompanyCode = State.ClaimBO.Company.Code
        wsCBFRequest.ClaimNumber = State.ClaimBO.ClaimNumber
        Dim wsResponseObject As Object


        Try
            if Me.State.ClaimBO?.FulfillmentProviderType = FulfillmentProviderType.DynamicFulfillment then

                return True
            
            else If (Not String.IsNullOrWhiteSpace(State.CertItemCoverageBO.FulfillmentProfileCode)) Then

                wsResponseObject = WcfClientHelper.Execute(Of Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGateway, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse)(
                                    GetClaimFulfillmentWebAppGatewayClient(),
                                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                Function(c As Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient)
                                    Return c.BeginFulfillment(wsCBFRequest)
                                End Function)

            Else

                wsResponseObject = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                                GetClient(),
                            New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                            Function(c As FulfillmentServiceClient)
                                Return c.StartFulfillmentProcess(wsRequest)
                            End Function)

            End If

        Catch ex As FaultException
            Log(ex)
        Catch ex As Exception
            Log(ex)
        End Try
        If wsResponseObject IsNot Nothing Then
            If wsResponseObject.GetType() Is GetType(BaseFulfillmentResponse) Then
                Dim wsResponseList As BaseFulfillmentResponse = DirectCast(wsResponseObject, BaseFulfillmentResponse)
                If wsResponseList.ResponseStatus.Equals("Failure") Then
                    MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                    blnSuccess = False
                End If
            End If
            If wsResponseObject.GetType() Is GetType(Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse) Then
                Dim wsResponseList As Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse = DirectCast(wsResponseObject, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse)
                If IsNothing(wsResponseList.FulfillmentNumber) Or IsNothing(wsResponseList.CompanyCode) Then
                    MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, False)
                    blnSuccess = False
                End If
            End If
        End If
        Return blnSuccess
    End Function

    Private Sub btnClaimDeductibleRefund_Click(sender As Object, e As EventArgs) Handles btnClaimDeductibleRefund.Click
        Try
            callPage(ClaimDeductibleRefundForm.URL, New ClaimDeductibleRefundForm.Parameters(CType(State.ClaimBO, ClaimBase)))
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


End Class



