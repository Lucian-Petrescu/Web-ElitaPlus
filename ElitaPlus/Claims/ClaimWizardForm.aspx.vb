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
    Public Shared Function LoadSku(ByVal manufacturerId As String, ByVal model As String, ByVal dealerId As String) As String

        Dim dealer As New Dealer(New Guid(dealerId))
        Dim equipmentId As Guid = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Now,
                                                                          New Guid(manufacturerId), model)
        If equipmentId.Equals(Guid.Empty) Then Return Nothing


        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim lstSkuNumbers As List(Of String)
        Dim skuNumberJSONArray As String

        Dim dv As DataView = CertItem.LoadSku(equipmentId, dealer.Id)

        If Not dv Is Nothing Then
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
    Public Const GRID_COL_PROCESSED_DATE_IDX As Integer = 6
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
            Session(Me.SESSION_KEY_CLAIM_WIZARD_BACKUP_STATE) = retState
            Return retState
        End Get
    End Property

    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try
            If callFromUrl.Contains(ClaimRecordingForm.Url2) Then
                MyBase.SetPageOutOfNavigation()
            End If
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InputParameters = CType(Me.CallingParameters, Parameters)
                Me.State.StepName = Me.State.InputParameters.StepNumber
                Me.State.EntryStep = Me.State.StepName
                Me.State.IsCallerAuthenticated = Me.State.InputParameters.IsCallerAuthenticated
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        If (Me.CalledUrl = ClaimIssueDetailForm.URL) Then

            If (Not Me.State.ClaimBO.Id.Equals(Guid.Empty)) Then
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.ClaimBO.Id)
                If (Not Me.State.ClaimBO Is Nothing) Then

                    If (Me.State.ClaimBO.Status = BasicClaimStatus.Active OrElse Me.State.ClaimBO.Status = BasicClaimStatus.Denied) Then
                        '//TO-DO: Navigate to Claim Details page (ClaimForm.aspx)                        
                        Me.callPage(ClaimForm.URL, New ClaimForm.Parameters(Me.State.ClaimBO.Id, Me.State.IsCallerAuthenticated))
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
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

        Public Sub New(ByVal stepNumber As ClaimWizardSteps, ByVal certificateId As Guid, ByVal claimId As Guid, ByVal claim As ClaimBase, Optional ByVal showWizard As Boolean = False, Optional ByVal comingFromDenyClaim As Boolean = False, Optional IsCallerAuthenticated As Boolean = False)
            Me.StepNumber = stepNumber
            Me.CertificateId = certificateId
            Me.ShowWizard = showWizard
            Me.ComingFromDenyClaim = comingFromDenyClaim
            Me.ClaimBo = claim
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
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
                moUserControlAddress = CType(moUserControlContactInfo.FindControl("moAddressController"), UserControlAddress_New)

            End If
            Return moUserControlAddress
        End Get
    End Property

    Public ReadOnly Property UserControlContactInfo() As UserControlContactInfo_New
        Get
            If moUserControlContactInfo Is Nothing Then
                moUserControlContactInfo = CType(Me.Master.FindControl("BodyPlaceHolder").FindControl("moUserControlContactInfo"), UserControlContactInfo_New)
            End If
            Return moUserControlContactInfo
        End Get
    End Property

    Protected WithEvents MessageController As MessageController
    Public ReadOnly Property UserControlMessageController() As MessageController
        Get
            If MessageController Is Nothing Then
                MessageController = DirectCast(Me.MasterPage.MessageController, MessageController)
            End If
            Return MessageController
        End Get
    End Property
#End Region

#Region "PageEvents"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ClearMessageControllers()
            If (Not Me.IsPostBack) Then
                btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancel.UniqueID))
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                btnModalCancelClaimYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancelClaim.UniqueID))
                lblModalClaimCancel.Text = TranslationBase.TranslateLabelOrMessage("ARE_YOU_SURE_CANCEL_CLAIM")
                Me.headerDeviceInfo.InnerText = TranslationBase.TranslateLabelOrMessage("DEVICE_INFORMATION")

                step2_ddlClaimedManuf.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", step2_ddlClaimedManuf.ClientID, step2_txtClaimedModel.ClientID, step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))
                step2_txtClaimedModel.Attributes.Add("onchange", String.Format("LoadSKU('{0}','{1}','{2}','{3}');", step2_ddlClaimedManuf.ClientID, step2_txtClaimedModel.ClientID, step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))
                step2_ddlClaimedSku.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", step2_ddlClaimedSku.ClientID, hdnSelectedClaimedSku.ClientID))

                PopulateStepUIandData()

            End If
            BindBoPropertiesToLabels(Me.State.StepName)
            PopulateClaimedEnrolledDetails()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub step1_btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles step1_btnSearch.Click
        Try
            If ValidateInputForStep1BtnSearch() Then
                Me.State.DateOfLoss = DateHelper.GetDateValue(step1_moDateOfLossText.Text)
                Me.State.DateReported = DateHelper.GetDateValue(step1_txtDateReported.Text)
                Me.State.IsEditMode = True
                PopulateDropDowns(Me.State.StepName)
                HandleButtons(Me.State.StepName)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub step1_cboRiskType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles step1_cboRiskType.SelectedIndexChanged
        Try
            If Not step1_cboRiskType.SelectedValue.Equals(Guid.Empty.ToString) Then
                Me.State.RiskTypeId = New Guid(step1_cboRiskType.SelectedValue)
                ' Me.BindListControlToDataView(Me.step1_cboCoverageType, LookupListNew.LoadCoverageTypes(Me.State.CertBO.Id, New Guid(step1_cboRiskType.SelectedValue), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                Dim listcontextForcoveragetypes As ListContext = New ListContext()
                listcontextForcoveragetypes.CertId = Me.State.CertBO.Id
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If (Me.State.StepName = Me.State.EntryStep) Then
                Me.ReturnBackToCallingPage()
            Else
                Me.State.StepName = LastStep()
                PopulateStepUIandData()
                Me.State.IsEditMode = False
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Me.ReturnBackToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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

    Protected Sub btn_Continue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnContinue.Click, btnClaimOverride_Write.Click

        Try
            PopulateBOFromForm(Me.State.StepName)
            Me.ClearMessageControllers()


            If (ValidateInputs(Me.State.StepName)) Then

                Select Case Me.State.StepName
                    Case ClaimWizardSteps.Step1

                    Case ClaimWizardSteps.Step2
                        Dim msg As String
                        If Me.State.CertItemCoverageBO.IsPossibleWarrantyClaim(msg) Then
                            Me.lblServiceWarrantyMessage.Text = TranslationBase.TranslateLabelOrMessage(msg)
                            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceWarranty'); </script>"
                            Me.RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                        If ((Me.State.CertBO.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC Or Me.State.CertBO.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso
                            Me.State.CertItemCoverageBO.GetAllClaims(Me.State.CertItemCoverageBO.Id).Count > 0) Then
                            Me.PopulateMasterClaimGrid()
                            Dim x As String = "<script language='JavaScript'> revealModal('ModalMasterClaim'); </script>"
                            Me.RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                    Case ClaimWizardSteps.Step3
                        CreateClaim()
                        If (Me.State.DealerBO.DeductibleCollectionId = Me.State.yesId) Then
                            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                            Me.RegisterStartupScript("Startup", x)
                            Exit Sub
                        End If
                    Case ClaimWizardSteps.Step4
                        ' REQ- 6156 - Skipping this step of choosing Service Center and copied the required code to step 5

                        For Each claimAuth As ClaimAuthorization In Me.State.ClaimBO.ClaimAuthorizationChildren
                            If Not claimAuth.IsNew Then
                                claimAuth.Void()
                            Else
                                claimAuth.DeleteChildren()
                                claimAuth.Delete()
                            End If
                        Next
                        ' REQ- 6156 - Call Start Fulfillment process web method in Fulfillment Web Service
                        'Me.State.ClaimBO.AddClaimAuthorization(Me.State.SelectedServiceCenterId)

                        Me.State.DoesActiveTradeInExistForIMEI = DoesAcceptedOfferExistForIMEI()
                        If Me.State.ClaimBO.IsNew And Me.State.DoesActiveTradeInExistForIMEI Then
                            Me.State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                            Me.State.CommentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                        End If
                    Case ClaimWizardSteps.Step5
                        ' Bug 178224 - REQ- 6156 
                        Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                        If Not attvalue Is Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                            If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                                For Each claimAuth As ClaimAuthorization In Me.State.ClaimBO.ClaimAuthorizationChildren
                                    If Not claimAuth.IsNew Then
                                        claimAuth.Void()
                                    Else
                                        claimAuth.DeleteChildren()
                                        claimAuth.Delete()
                                    End If
                                Next
                                Me.State.DoesActiveTradeInExistForIMEI = DoesAcceptedOfferExistForIMEI()
                            End If
                        End If
                        If Me.State.ClaimBO.IsNew And Me.State.DoesActiveTradeInExistForIMEI Then
                            Me.State.ClaimBO.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__ACTIVE_TRADEIN_QUOTE_EXISTS)
                            Me.State.ClaimBO.DenyClaim()
                        End If

                        If Me.State.ClaimBO IsNot Nothing AndAlso Me.State.ClaimBO.Status = BasicClaimStatus.Active Then
                            'user story 192764 - Task-199011--Start------
                            Dim dsCaseFields As DataSet = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            If (Not dsCaseFields Is Nothing AndAlso dsCaseFields.Tables.Count > 0 AndAlso dsCaseFields.Tables(0).Rows.Count > 0) Then

                                Dim hasBenefit As DataRow() = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                                Dim benefitCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='BENEFITCHECKERROR'")
                                Dim preCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='PRECHECKERROR'")
                                Dim lossType As DataRow() = dsCaseFields.Tables(0).Select("field_code='LOSSTYPE'")

                                If Not hasBenefit Is Nothing AndAlso hasBenefit.Length > 0 Then
                                    If Not hasBenefit(0)("field_value") Is Nothing AndAlso String.Equals(hasBenefit(0)("field_value").ToString(), Boolean.FalseString, StringComparison.CurrentCultureIgnoreCase) Then
                                        UpdateCaseFieldValues(hasBenefit, lossType)

                                        dsCaseFields = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                        hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                                    End If
                                End If
                                If Not benefitCheckError Is Nothing AndAlso benefitCheckError.Length > 0 Then
                                    If Not benefitCheckError(0)("field_value") Is Nothing AndAlso Not String.Equals(benefitCheckError(0)("field_value").ToString(), "NO ERROR", StringComparison.CurrentCultureIgnoreCase) Then
                                        UpdateCaseFieldValues(benefitCheckError, lossType)

                                        dsCaseFields = CaseBase.GetCaseFieldsList(State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                        hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                                    End If
                                End If

                                If Not preCheckError Is Nothing And preCheckError.Length = 0 Then
                                    If Not hasBenefit Is Nothing AndAlso hasBenefit.Length > 0 Then
                                        If Not hasBenefit(0)("field_value") Is Nothing AndAlso hasBenefit(0)("field_value").ToString().ToUpper() = Boolean.TrueString.ToUpper() Then
                                            RunPreCheck(hasBenefit)
                                        End If
                                    ElseIf Not benefitCheckError Is Nothing AndAlso benefitCheckError.Length > 0 Then
                                        If Not benefitCheckError(0)("field_value") Is Nothing AndAlso benefitCheckError(0)("field_value").ToString().ToUpper() <> "NO ERROR" Then
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
                                    Function(ByVal c As ClaimServiceClient)
                                        Return c.NewClaimEntitled(wsRequest)
                                    End Function)

                                If wsResponse.IsNewClaimEntitled = False Then 'deny the claim with the denial reason returned
                                    State.ClaimBO.Status = BasicClaimStatus.Denied
                                    Me.State.ClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, wsResponse.DenialCodes(0))
                                End If
                            Catch ex As FaultException
                                Log(ex)
                            Catch ex As Exception
                                Log(ex)
                            End Try
                        End If
                        'User Story 186561 Number of active claims allowed under one certificate -- end

                        Me.State.CommentBO.Save()
                        Me.State.ClaimBO.Save()

                        ' Create Authorization
                        If (Me.State.ClaimBO.Status = BasicClaimStatus.Active) Then
                            Dim blnWsSuccess As Boolean = True
                            Try
                                blnWsSuccess = CallStartFulfillmentProcess()
                                If Not blnWsSuccess Then
                                    Me.State.ClaimBO.Status = BasicClaimStatus.Pending
                                    Me.State.ClaimBO.Save()
                                    Exit Sub
                                End If
                            Catch ex As Exception
                                Me.State.ClaimBO.Status = BasicClaimStatus.Pending
                                Me.State.ClaimBO.Save()
                                Me.MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
                                Throw
                            End Try
                        End If

                        'TO DO : Add logic to Send Emails and other stuff
                        Me.ReturnBackToCallingPage()
                        Exit Sub
                End Select

                GoToNextStep()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Shared Sub UpdateCaseFieldValues(ByRef caseFieldRow As DataRow(), ByRef lossType As DataRow())
        Dim caseFieldXcds() As String
        Dim caseFieldValues() As String

        If Not lossType Is Nothing AndAlso lossType.Length > 0 Then
            If Not lossType(0)("field_value") Is Nothing AndAlso (lossType(0)("field_value").ToString().ToUpper() = "ADH1234" Or lossType(0)("field_value").ToString().ToUpper() = "ADH5") Then
                caseFieldXcds = {"CASEFLD-HASBENEFIT", "CASEFLD-ADCOVERAGEREMAINING"}
                caseFieldValues = {Boolean.TrueString.ToUpper(), Boolean.TrueString.ToUpper()}
            ElseIf Not lossType(0)("field_value") Is Nothing AndAlso lossType(0)("field_value").ToString().ToUpper() = "THEFT/LOSS" Then
                caseFieldXcds = {"CASEFLD-HASBENEFIT"}
                caseFieldValues = {Boolean.TrueString.ToUpper()}
            End If
        End If

        CaseBase.UpdateCaseFieldValues(GuidControl.ByteArrayToGuid(caseFieldRow(0)("case_Id")), caseFieldXcds, caseFieldValues)
    End Sub

    Private Sub RunPreCheck(ByVal caseRecord As DataRow())
        Try
            Dim benefitCheckResponse As LegacyBridgeResponse
            Dim client As LegacyBridgeServiceClient = Claim.GetLegacyBridgeServiceClient()

            benefitCheckResponse = WcfClientHelper.Execute(Of LegacyBridgeServiceClient, ILegacyBridgeService, LegacyBridgeResponse)(
                client,
                New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal lc As LegacyBridgeServiceClient)
                    Return lc.BenefitClaimPreCheck(GuidControl.ByteArrayToGuid(caseRecord(0)("case_Id")).ToString())
                End Function)

            If (Not benefitCheckResponse Is Nothing) Then
                Me.State.ClaimBO.Status = If(benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Approve, BasicClaimStatus.Active, BasicClaimStatus.Pending)
                If (benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Deny) Then
                    Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECKFAIL")
                    Dim newClaimIssue As ClaimIssue = CType(Me.State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(Me.State.ClaimBO.Id, issueId, Me.State.ClaimBO.Certificate.Id, True)
                End If
            Else
                Me.State.ClaimBO.Status = BasicClaimStatus.Pending
                Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                Dim newClaimIssue As ClaimIssue = CType(Me.State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                newClaimIssue.SaveNewIssue(Me.State.ClaimBO.Id, issueId, Me.State.ClaimBO.Certificate.Id, True)
            End If
        Catch ex As FaultException
            Log(ex)
        Catch ex As Exception
            Log(ex)
            Me.State.ClaimBO.Status = BasicClaimStatus.Pending
            Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
            Dim newClaimIssue As ClaimIssue = CType(Me.State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            newClaimIssue.SaveNewIssue(Me.State.ClaimBO.Id, issueId, Me.State.ClaimBO.Certificate.Id, True)
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

    Protected Sub btnaddNewMasterClaimNumber_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewMasterClaim.Click
        Try
            GoToNextStep()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDedCollContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDedCollContinue.Click

        If Not step3_cboDedCollMethod.SelectedIndex > BLANK_ITEM_SELECTED Then
            Me.moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DED_COLL_METHD_REQD)
            Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
            Me.RegisterStartupScript("Startup", x)
            Exit Sub
        Else
            If GetSelectedItem(step3_cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) AndAlso
               step3_txtDedCollAuthCode.Text.Length <> CInt(Codes.DED_COLL_CR_AUTH_CODE_LEN) Then 'Allow exact length of Auth Code
                Me.moModalCollectDivMsgController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTH_CODE_FOR_CC)
                Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
                Me.RegisterStartupScript("Startup", x)
                Exit Sub
            End If
        End If

        Dim c As Comment
        Dim oldStatus As String = Me.State.ClaimBO.StatusCode
        Try
            PopulateBOFromForm(Me.State.StepName)
            Me.State.ClaimBO.Validate()
            Me.State.ClaimBO.CheckForRules()
            GoToNextStep()
        Catch ex As Exception
            Me.State.ClaimBO.StatusCode = oldStatus
            Throw ex
        End Try

    End Sub

    Protected Sub cboDedCollMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles step3_cboDedCollMethod.SelectedIndexChanged

        If GetSelectedItem(step3_cboDedCollMethod) = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_CR_CARD) Then
            Me.step3_txtDedCollAuthCode.Enabled = True
        Else
            Me.step3_txtDedCollAuthCode.Text = ""
            Me.step3_txtDedCollAuthCode.Enabled = False
        End If
        Dim x As String = "<script language='JavaScript'> revealModal('modalCollectDeductible') </script>"
        Me.RegisterStartupScript("Startup", x)
    End Sub

    Private Sub btnSearchServiceCenter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles step4_btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedServiceCenterId = Guid.Empty
            Me.PopulateGridOrDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearchServiceCenter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles step4_btnClearSearch.Click
        Try
            Me.step4_TextboxCity.Text = ""
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByZip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles step4_RadioButtonByZip.CheckedChanged
        Try
            If Me.step4_RadioButtonByZip.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByZip
            End If
            Me.EnableDisableWizardControls(ClaimWizardSteps.Step4)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByCity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles step4_RadioButtonByCity.CheckedChanged
        Try
            If Me.step4_RadioButtonByCity.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByCity
            End If
            Me.EnableDisableWizardControls(ClaimWizardSteps.Step4)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles step4_RadioButtonAll.CheckedChanged
        Try
            Me.EnableDisableWizardControls(ClaimWizardSteps.Step4)
            If Me.step4_RadioButtonAll.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All
                Dim address As New Address(Me.State.CertBO.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.GetSelectedItem(step4_moCountryDrop))
                PopulateCountryDropdown(SelectedCountry)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonNO_SVC_OPTION_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles step4_RadioButtonNO_SVC_OPTION.CheckedChanged
        Try
            Me.EnableDisableWizardControls(ClaimWizardSteps.Step4)
            If Me.step4_RadioButtonNO_SVC_OPTION.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.None
                'Dim address As New Address(Me.State.CertBO.AddressId)
                'Dim SelectedCountry As New ArrayList
                'SelectedCountry.Add(Me.GetSelectedItem(step4_moCountryDrop))
                'PopulateCountryDropdown(SelectedCountry)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles step4_moCountryDrop.SelectedIndexChanged
        Try
            If Me.step4_RadioButtonAll.Checked Then
                Me.EnableDisableWizardControls(ClaimWizardSteps.Step4)
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All

                Dim address As New Address(Me.State.CertBO.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.GetSelectedItem(step4_moCountryDrop))

                PopulateCountryDropdown(SelectedCountry)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moMultipleColumnDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles step4_moMultipleColumnDrop.SelectedDropChanged
        Try
            PopulateGridOrDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
        Try
            Me.State.IsEditMode = True
            Me.EnableDisableWizardControls(Me.State.StepName)
            HandleButtons(Me.State.StepName)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.ClearMessageControllers()
            Me.PopulateBOFromForm(Me.State.StepName)
            ' Me.ClearMessageControllers()

            Select Case Me.State.StepName
                Case ClaimWizardSteps.Step2
                    If (Me.ValidateInputs(Me.State.StepName)) Then
                        If Me.State.CertItemBO.IsFamilyDirty OrElse Me.State.CertItemCoverageBO.IsFamilyDirty _
                           OrElse (Not Me.State.step2_claimEquipmentBO Is Nothing And Me.State.step2_claimEquipmentBO.IsDirty) Then
                            If (Me.State.CertItemBO.IsFamilyDirty OrElse Me.State.CertItemCoverageBO.IsFamilyDirty) Then
                                If (Me.State.CertItemCoverageBO.IsFamilyDirty) Then
                                    Me.State.CertItemCoverageBO.Save()
                                End If
                                If (Me.State.CertItemBO.IsFamilyDirty) Then
                                    Me.State.CertItemBO.Save()
                                End If
                                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            End If
                            Me.PopulateFormFromBO(Me.State.StepName)
                            Me.State.IsEditMode = False
                            Me.EnableDisableWizardControls(Me.State.StepName)
                        Else
                            If Not Me.State.CertItemBO.IsEquipmentRequired Then
                                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED, True)
                            End If
                        End If
                    End If
            End Select
            HandleButtons(Me.State.StepName)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            Select Case Me.State.StepName
                Case ClaimWizardSteps.Step2
                    If Not Me.State.CertItemBO.IsNew Then
                        Me.State.CertItemBO = New CertItem(Me.State.CertItemBO.Id)
                    Else
                        Me.State.CertItemBO = New CertItem
                    End If
                    Me.State.IsEditMode = False
                    Me.PopulateFormFromBO(Me.State.StepName)
                    EnableDisableWizardControls(Me.State.StepName)
                    Me.ValidateInputs(Me.State.StepName)
            End Select
            HandleButtons(Me.State.StepName)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ButtonSoftQuestions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSoftQuestions.Click
        Try
            If Me.tvQuestion.Nodes.Count = 0 Then
                Me.PopulateSoftQuestionTree()
            End If
            ControlMgr.SetVisibleControl(Me, moCertificateInfoController, True)
            moCertificateInfoController.InitController(Me.State.CertBO.Id, , Me.State.CompanyBO.Code)
            Dim x As String = "<script language='JavaScript'> revealModal('ModalSoftQuestions') </script>"
            Me.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveClaimIssue(ByVal sender As Object, ByVal Args As EventArgs) Handles step3_modalClaimIssue_btnSave.Click

        Try
            Dim claimIssue As ClaimIssue = CType(Me.State.ClaimBO.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
            claimIssue.SaveNewIssue(Me.State.ClaimBO.Id, New Guid(hdnSelectedIssueCode.Value), Me.State.ClaimBO.CertificateId, False)
            Me.State.ClaimIssuesView = Me.State.ClaimBO.GetClaimIssuesView()
            PopulateClaimIssuesGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaim_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenyClaim.Click
        Try
            Me.PopulateBOFromForm(Me.State.StepName)
            Me.State.ClaimBO.Validate()
            If Me.State.ClaimBO.LossDate.Value < Me.State.ClaimBO.Certificate.WarrantySalesDate.Value Then
                ElitaPlusPage.SetLabelError(Me.step3_LabelLossDate)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
            End If

            Dim x As String = "<script language='JavaScript'> revealModal('ModalDenyClaim') </script>"
            Me.RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Assurant.ElitaPlus.BusinessObjectsNew.BOValidationException
            'Allow bypassing LossDate validation denying an expired item claim
            If ex.ValidationErrorList.Count = 1 And
               ex.ValidationErrorList(0).PropertyName = "LossDate" And
               (Me.State.ClaimBO.LossDate.Value > Me.State.ClaimBO.CertificateItemCoverage.EndDate.Value Or
                Me.State.ClaimBO.LossDate.Value < Me.State.ClaimBO.CertificateItemCoverage.BeginDate.Value) Then
                Dim bypassMdl As String = "<script language='JavaScript'> revealModal('ModalBypassDoL') </script>"
                Me.RegisterStartupScript("BpQtn", bypassMdl)
            Else
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnDenyClaimSave_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenyClaimSave.Click
        Try
            Me.State.IsClaimDenied = True
            Me.PopulateBOFromForm(Me.State.StepName)
            If Me.State.ClaimBO.DeniedReasonId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(Me.step3_lblDeniedReason)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR)
            End If

            Me.State.ClaimBO.Deductible = New DecimalType(0D)
            Me.State.ClaimBO.VoidAuthorizations()
            Me.State.ClaimBO.DenyClaim()
            Me.State.ClaimBO.IsUpdatedComment = True
            GoToNextStep()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnModalBbDolYes_Click(sender As Object, e As EventArgs) Handles btnModalBbDolYes.Click
        Dim x As String = "<script language='JavaScript'>hideModal('ModalBypassDoL'); revealModal('ModalDenyClaim'); </script>"
        Me.RegisterStartupScript("Startup", x)
        Dim DndId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__INCORRECT_DEVICE_SELECTED)
        Me.step3_cboDeniedReason.SelectedIndex = Me.step3_cboDeniedReason.Items.IndexOf(Me.step3_cboDeniedReason.Items.FindByValue(DndId.ToString()))
        Me.step3_cboDeniedReason.Items.FindByValue(Me.step3_cboDeniedReason.SelectedValue).Selected = True
        Me.step3_cboDeniedReason.Enabled = False
    End Sub

    Private Sub ButtonCancelClaim_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelClaim.Click
        Try
            Me.PopulateBOFromForm(Me.State.StepName)
            Me.State.ClaimBO.Cancel()
            Me.State.ClaimBO.IsUpdatedComment = True
            Me.State.ClaimBO.Save()
            Me.GoToNextStep()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboMethodOfRepair_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles step2_cboMethodOfRepair.SelectedIndexChanged
        Try
            PopulateFormDeductibleFormBOs(GetSelectedItem(step2_cboMethodOfRepair))
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboUseShipAddress_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles step3_cboUseShipAddress.SelectedIndexChanged
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If step3_cboUseShipAddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True

            If Me.State.ClaimBO.ContactInfoId.Equals(Guid.Empty) Then
                Me.State.ClaimBO.AddContactInfo(Nothing)
                Me.State.ClaimBO.ContactInfo.Address.CountryId = Me.State.ClaimBO.Company.CountryId
                Me.State.ClaimBO.ContactInfo.SalutationId = Me.State.ClaimBO.Company.SalutationId

                Me.UserControlAddress.NewClaimBind(Me.State.ClaimBO.ContactInfo.Address)
                Me.UserControlContactInfo.NewClaimBind(Me.State.ClaimBO.ContactInfo)
            Else
                Me.UserControlAddress.ClaimDetailsBind(Me.State.ClaimBO.ContactInfo.Address)
                Me.UserControlContactInfo.Bind(Me.State.ClaimBO.ContactInfo)

            End If
        Else
            moUserControlContactInfo.Visible = False

            If Me.State.ClaimBO.ContactInfo.IsNew Then
                If Not Me.State.ClaimBO.ContactInfo Is Nothing Then
                    Me.State.ClaimBO.ContactInfo.Delete()
                End If

                If Not Me.State.ClaimBO.ContactInfo.Address Is Nothing Then
                    Me.State.ClaimBO.ContactInfo.Address.Delete()
                End If

                If Not Me.State.ClaimBO.ContactInfoId = System.Guid.Empty Then
                    Me.State.ClaimBO.ContactInfoId = System.Guid.Empty
                End If
            End If
        End If
    End Sub

    Private Sub tvQuestion_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvQuestion.TreeNodePopulate
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
        Me.RegisterStartupScript("Startup", x)
    End Sub

    Private Sub btnModalServiceWarrantyYes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModalServiceWarrantyYes.Click
        GoToNextStep()
    End Sub

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

            Me.State.ClaimBO.AttachImage(
                New Guid(Me.DocumentTypeDropDown.SelectedValue),
                Nothing,
                DateTime.Now,
                fileName,
                Me.CommentTextBox.Text,
                ElitaPlusIdentity.Current.ActiveUser.UserName,
                fileData)
            If Not Me.State.ClaimBO.IsNew Then

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
#End Region

#Region "UI Controlling Logic"

    Private Sub PopulateStepUIandData()
        InitializeCommonData()
        InitializeStepUI(Me.State.StepName)
        PopulateFormFromBO(Me.State.StepName)
    End Sub

    Private Sub UpdateBreadCrum(ByVal wizardStep As ClaimWizardSteps)

        If Me.State.EntryStep = ClaimWizardSteps.Step1 Then

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & Me.State.CertBO.CertNumber & ElitaBase.Sperator &
                                      "File New Claim"
        Else
            If (Not Me.State.ClaimBO Is Nothing AndAlso Me.State.ClaimBO.IsNew) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & Me.State.CertBO.CertNumber & ElitaBase.Sperator &
                                          "File New Claim"
            Else
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM DETAILS") & ElitaBase.Sperator & String.Format("{0} {1} {2} {3}", TranslationBase.TranslateLabelOrMessage("UPDATE"), TranslationBase.TranslateLabelOrMessage("PENDING"), TranslationBase.TranslateLabelOrMessage("CLAIM"), Me.State.ClaimBO.ClaimNumber)
            End If
        End If
    End Sub

    Private Sub InitializeStepUI(ByVal wizardStep As ClaimWizardSteps)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.WizardControl.Visible = False
        If (ShowWizard()) Then SetSelectedStep()
        UpdateBreadCrum(wizardStep)
        EnableDisableStepDivs(wizardStep)
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                Me.AddCalendar_New(Me.step1_btnDateOfLoss, Me.step1_moDateOfLossText)
                Me.AddCalendar_New(Me.step1_btnDateReported, Me.step1_txtDateReported)
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
                EnableDisableWizardControls(wizardStep)
            Case ClaimWizardSteps.Step2
                TranslateGridHeader(grdMasterClaim)
                EnableDisableWizardControls(wizardStep)
            Case ClaimWizardSteps.Step3
                Me.AddCalendar_New(Me.step3_ImageButtonLossDate, Me.step3_TextboxLossDate)
                Me.AddCalendar_New(Me.step3_ImageButtonReportDate, Me.step3_TextboxReportDate)
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
                ddlIssueCode.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "D"))
                ddlIssueDescription.Attributes.Add("onchange", String.Format("RefreshDropDownsAndSelect('{0}','{1}',true,'{2}');", ddlIssueCode.ClientID, ddlIssueDescription.ClientID, "C"))
                MessageLiteral.Text = String.Format("{0} : {1}", TranslationBase.TranslateLabelOrMessage("ISSUE_CODE"), TranslationBase.TranslateLabelOrMessage("MSG_SELECT_ISSUE_CODE"))
                If Not Me.step3_LabelUseShipAddress.Text.EndsWith(":") Then
                    Me.step3_LabelUseShipAddress.Text = Me.step3_LabelUseShipAddress.Text & ":"
                End If
                'REQ-5467
                If Not Me.State.ClaimBO Is Nothing Then
                    If Me.State.ClaimBO.Dealer.IsLawsuitMandatoryId = Me.State.yesId Then
                        step3_LabelIsLawsuitId.Text = "<span class=""mandatory"">*</span> " & step3_LabelIsLawsuitId.Text
                    End If
                End If

                'Me.lblInvalidDoLMessage.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_CONFIRM)
                Me.btnModalBbDolYes.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_YES)
                Me.btnModalBbDolNo.Value = TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DOL_BYPASS_NO)

                Me.SetContactInfoLabelColor()
                Me.SetAddressLabelColor()
                TranslateGridHeader(Grid)
                TranslateGridHeader(GridClaimImages)
                TranslateGridHeader(GridClaimAuthorization)
                ucClaimConsequentialDamage.Translate()
                EnableDisableWizardControls(wizardStep)


            Case ClaimWizardSteps.Step4
                Dim showCityFields As Boolean = Me.step4_RadioButtonByCity.Checked
                Dim showAllFields As Boolean = Me.step4_RadioButtonAll.Checked
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

    Private Sub EnableDisableWizardControls(ByVal wizardStep As ClaimWizardSteps)

        'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, False)

        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                ControlMgr.SetVisibleControl(Me, Me.step1_searchResultPanel, Me.State.IsEditMode)
                ControlMgr.SetEnableControl(Me, btnContinue, True)
            Case ClaimWizardSteps.Step2
                MyBase.EnableDisableControls(Me.pnlVehicleInfo, Not Me.State.IsEditMode)
                MyBase.EnableDisableControls(Me.pnlDeviceInfo, Not Me.State.IsEditMode)
                Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)

                If Me.State.CertItemBO.IsEquipmentRequired Then
                    'Claimed Equipment
                    ControlMgr.SetVisibleControl(Me, step2_ddlClaimedManuf, Me.State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedmake, Not Me.State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedModel, True)
                    ControlMgr.SetVisibleControl(Me, step2_ddlClaimedSku, Me.State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_txtClaimedSKu, Not Me.State.IsEditMode)
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
                    step2_txtClaimedModel.ReadOnly = Not Me.State.IsEditMode
                    step2_txtClaimedModel.ReadOnly = Not Me.State.IsEditMode
                    step2_txtClaimedSKu.ReadOnly = Not Me.State.IsEditMode
                    step2_txtClaimedSerialNumber.ReadOnly = Not Me.State.IsEditMode
                    step2_txtClaimedDescription.ReadOnly = True

                    step2_TextboxModel.ReadOnly = True
                    step2_TextboxSerialNumber.ReadOnly = True
                    step2_TextboxDealerItemDesc.ReadOnly = True
                    ControlMgr.SetVisibleControl(Me, btnContinue, Not Me.State.IsEditMode)

                    '' ''If Me.State.CertItemBO.EquipmentId.Equals(Guid.Empty) Then
                    '' ''    'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, True)
                    '' ''    ControlMgr.SetVisibleControl(Me, Me.btnContinue, False)
                    '' ''Else
                    '' ''    ControlMgr.SetVisibleControl(Me, Me.btnContinue, True)
                    '' ''    'ControlMgr.SetVisibleControl(Me, Me.BtnVerifyEquipment_WRITE, False)
                    '' ''End If
                Else
                    ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, Me.State.IsEditMode)
                    ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, Not Me.State.IsEditMode)
                End If

                ControlMgr.SetVisibleControl(Me, step2_cboRiskTypeId, Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_TextboxRiskType, Not Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_cboMethodOfRepair, Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, step2_TextboxMethodOfRepair, Not Me.State.IsEditMode)
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

                step2_TextboxInvNum.ReadOnly = Not Me.State.IsEditMode


                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not Me.State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
                    pnlVehicleInfo.Visible = False
                    Me.step2_LabelYear.Visible = False
                    Me.step2_TextboxYear.Visible = False
                    Me.step2_LabelSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                Else
                    Me.step2_LabelSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("VIN") + ":"
                    ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, False)
                    Me.step2_cboManufacturerId.Enabled = False
                    ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, True)
                    Me.step2_TextboxModel.ReadOnly = True
                End If

                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                If (Not Me.State.CertItemCoverageBO.IsClaimAllowed.Equals(NoId)) Then
                    Dim todayDate As Date
                    If todayDate.Today < Me.State.CertItemCoverageBO.BeginDate.Value And Me.State.CertBO.StatusCode <> CLOSED Then
                        ControlMgr.SetEnableControl(Me, btnDenyClaim, True)

                    End If
                End If
                Me.step2_TextboxBeginDate.Font.Bold = True
                Me.step2_TextboxEndDate.Font.Bold = True

                '5623

                If Me.State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.DealerBO.IsGracePeriodSpecified Then

                    If Not Me.State.CertItemCoverageBO.IsCoverageEffectiveForGracePeriod(step1_txtDateReported.Text) Then
                        Me.step2_TextboxBeginDate.ForeColor = Color.Red
                        Me.step2_TextboxEndDate.ForeColor = Color.Red
                    Else
                        Me.step2_TextboxBeginDate.ForeColor = Color.Green
                        Me.step2_TextboxEndDate.ForeColor = Color.Green
                    End If
                Else
                    If Not Me.State.CertItemCoverageBO.IsCoverageEffective Then
                        Me.step2_TextboxBeginDate.ForeColor = Color.Red
                        Me.step2_TextboxEndDate.ForeColor = Color.Red
                    Else
                        Me.step2_TextboxBeginDate.ForeColor = Color.Green
                        Me.step2_TextboxEndDate.ForeColor = Color.Green
                    End If
                End If

            Case ClaimWizardSteps.Step3
                InitialEnableDisableControlsForStep3()
                EnableDisableControlsForStep3()

            Case ClaimWizardSteps.Step4
                Dim showCityFields As Boolean = Me.step4_RadioButtonByCity.Checked
                Dim showAllFields As Boolean = Me.step4_RadioButtonAll.Checked
                Dim NO_SVC_OPTION As Boolean = Me.step4_RadioButtonNO_SVC_OPTION.Checked
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

                If (Me.State.CompanyBO Is Nothing) Then
                    Me.State.CompanyBO = New Company(Me.State.CertBO.CompanyId)
                End If
                'REQ-5546
                Dim objCountry As New Country(Me.State.CompanyBO.CountryId)
                Me.State.default_service_center_id = objCountry.DefaultSCId

                ControlMgr.SetVisibleControl(Me, step4_RadioButtonNO_SVC_OPTION, Not Me.State.default_service_center_id.Equals(Guid.Empty))

            Case ClaimWizardSteps.Step5
                Me.ChangeEnabledProperty(Me.step5_TextboxCallerName, True)
                Me.ChangeEnabledProperty(Me.step5_TextboxCommentText, True)
                Me.ChangeEnabledProperty(Me.step5_cboCommentType, True)
                Me.btnBack.Enabled = True
                If Me.step5_TextboxDateTime.Text Is Nothing OrElse Me.step5_TextboxDateTime.Text.Trim.Length = 0 Then
                    ControlMgr.SetVisibleControl(Me, Me.step5_LabelDateTime, False)
                    ControlMgr.SetVisibleControl(Me, Me.step5_TextboxDateTime, False)
                End If
        End Select
    End Sub

    Private Function DoesAcceptedOfferExistForIMEI() As Boolean
        Dim blnIsDealerDRPTradeInQuoteFlag As Boolean = False
        blnIsDealerDRPTradeInQuoteFlag = Me.State.ClaimBO.GetDealerDRPTradeInQuoteFlag(Me.State.ClaimBO.DealerCode)
        If blnIsDealerDRPTradeInQuoteFlag Then
            If Me.State.ClaimBO.VerifyIMEIWithDRPSystem(Me.State.ClaimBO.SerialNumber) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function ShowWizard() As Boolean
        If (Not Me.State.InputParameters Is Nothing) Then
            Return Me.State.InputParameters.ShowWizard
        Else
            Return False
        End If
    End Function

    Private Sub EnableDisableStepDivs(ByVal stepName As ClaimWizardSteps)
        Select Case stepName
            Case ClaimWizardSteps.Step1
                Me.dvstep1.Visible = True
                Me.dvStep2.Visible = False
                Me.dvStep3.Visible = False
                Me.dvStep4.Visible = False
                Me.dvStep5.Visible = False
            Case ClaimWizardSteps.Step2
                Me.dvstep1.Visible = False
                Me.dvStep2.Visible = True
                Me.dvStep3.Visible = False
                Me.dvStep4.Visible = False
                Me.dvStep5.Visible = False
            Case ClaimWizardSteps.Step3
                Me.dvstep1.Visible = False
                Me.dvStep2.Visible = False
                Me.dvStep3.Visible = True
                Me.dvStep4.Visible = False
                Me.dvStep5.Visible = False
            Case ClaimWizardSteps.Step4
                Me.dvstep1.Visible = False
                Me.dvStep2.Visible = False
                Me.dvStep3.Visible = False
                Me.dvStep4.Visible = True
                Me.dvStep5.Visible = False
            Case ClaimWizardSteps.Step5
                Me.dvstep1.Visible = False
                Me.dvStep2.Visible = False
                Me.dvStep3.Visible = False
                Me.dvStep4.Visible = False
                Me.dvStep5.Visible = True
        End Select
    End Sub

    Private Overloads Sub SetDefaultButton(ByVal stepName As ClaimWizardSteps)

        Select Case stepName
            Case ClaimWizardSteps.Step1
                MyBase.SetDefaultButton(Me.step1_moDateOfLossText, Me.step1_btnSearch)
            Case ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step4
            Case ClaimWizardSteps.Step5

        End Select

    End Sub

    Private Sub InitializeCommonData()
        'Initialize Certificate BO
        If (Me.State.CertBO Is Nothing) Then
            If Not Me.State.InputParameters Is Nothing AndAlso Not Me.State.InputParameters.CertificateId.Equals(Guid.Empty) Then
                Me.State.CertBO = New Certificate(Me.State.InputParameters.CertificateId)
            End If
        End If
        If (Not Me.State.CertBO Is Nothing) Then
            'Initialize Dealer BO
            If (Me.State.DealerBO Is Nothing) Then
                If (Not Me.State.CertBO Is Nothing) Then
                    Me.State.DealerBO = New Dealer(Me.State.CertBO.DealerId)
                End If
            End If

            'Initialize VSC Model
            If (Me.State.VSCModel Is Nothing) Then
                If Not Me.State.CertBO.ModelId.Equals(Guid.Empty) Then
                    Me.State.VSCModel = New VSCModel(Me.State.CertBO.ModelId)
                End If
            End If

            'Initialize VSC Class Code
            If (Me.State.VSCClassCode Is Nothing) Then
                If Not Me.State.CertBO.ClassCodeId.Equals(Guid.Empty) Then
                    Me.State.VSCClassCode = New VSCClassCode(Me.State.CertBO.ClassCodeId)
                End If
            End If

        End If

        'Initialize Certificate Item Coverage BO
        If (Me.State.CertItemCoverageBO Is Nothing) Then
            If (Not Me.State.CertficateCoverageTypeId.Equals(Guid.Empty)) Then
                Me.State.CertItemCoverageBO = New CertItemCoverage(Me.State.CertficateCoverageTypeId)
            End If
        End If

        'Initialize Certificate Item BO
        If (Me.State.CertItemBO Is Nothing) Then
            If (Not Me.State.CertItemCoverageBO Is Nothing) Then
                Me.State.CertItemBO = New CertItem(Me.State.CertItemCoverageBO.CertItemId)
                Me.State.CertBO = Me.State.CertItemBO.GetCertificate(Me.State.CertBO.Id)
            End If
        End If

        'End If
        'Initialize Claim BO
        If (Me.State.ClaimBO Is Nothing) Then
            If (Not Me.State.InputParameters.claimId.Equals(Guid.Empty)) Then
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(Me.State.InputParameters.claimId)
            ElseIf (Not Me.State.InputParameters.ClaimBo Is Nothing) Then
                Me.State.ClaimBO = CType(Me.State.InputParameters.ClaimBo, MultiAuthClaim)
            Else
                'Load claim Bo if only on step 3 and above
                If (DirectCast([Enum].Parse(GetType(ClaimWizardSteps), CStr(Me.State.StepName)), Integer) > 2) Then
                    Me.State.ClaimBO = ClaimFacade.Instance.CreateClaim(Of MultiAuthClaim)(Me.State.DealerBO.Id)
                    Me.State.ClaimBO.PrePopulate(Me.State.CertItemCoverageBO.Id, Me.State.MasterClaimNumber, Me.State.DateOfLoss, False, False, False, True, Me.State.CallerName, Me.State.ProblemDescription, Me.State.DateReported, Me.State.step2_claimEquipmentBO)
                End If
            End If
        End If

        'Initialize from ClaimBO
        If (Not Me.State.ClaimBO Is Nothing) Then
            Me.State.CertItemCoverageBO = Me.State.ClaimBO.CertificateItemCoverage
            Me.State.CertItemBO = Me.State.ClaimBO.CertificateItem
            Me.State.CertBO = Me.State.ClaimBO.Certificate
            Me.State.DealerBO = Me.State.ClaimBO.Dealer
            If (Not Me.State.ClaimBO.CurrentComment Is Nothing) Then
                Me.State.CommentBO = Me.State.ClaimBO.CurrentComment
            Else
                Me.State.CommentBO = Comment.GetLatestComment(Me.State.ClaimBO)
            End If
            Me.State.step2_claimEquipmentBO = Me.State.ClaimBO.ClaimedEquipment
        End If

        If (Me.State.yesId.Equals(Guid.Empty)) Then
            Me.State.yesId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        End If

        If (Me.State.noId.Equals(Guid.Empty)) Then
            Me.State.noId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        End If

        If (Me.State.CompanyBO Is Nothing) Then
            Me.State.CompanyBO = New Company(Me.State.CertBO.CompanyId)
            Dim sSalutation As String = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.CertBO.SalutationId)
            If sSalutation = "Y" Then Me.State.IsSalutation = True Else Me.State.IsSalutation = False
        End If




    End Sub

    Private Sub HandleButtons(ByVal wizardStep As ClaimWizardSteps)

        ControlMgr.SetVisibleControl(Me, btnBack, True)
        ControlMgr.SetVisibleControl(Me, btnUndo_Write, False)
        ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, False)
        ControlMgr.SetVisibleControl(Me, btnSoftQuestions, False)
        ControlMgr.SetVisibleControl(Me, btnDenyClaim, False)
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
        ControlMgr.SetVisibleControl(Me, btnClaimOverride_Write, False)
        ControlMgr.SetVisibleControl(Me, btnComment, False)
        Me.btnContinue.Text = TranslationBase.TranslateLabelOrMessage("CONTINUE")

        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                ControlMgr.SetVisibleControl(Me, Me.btnContinue, Me.State.IsEditMode)
            Case ClaimWizardSteps.Step2
                ControlMgr.SetVisibleControl(Me, btnUndo_Write, Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnSoftQuestions, Not Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, Not Me.State.IsEditMode)
            Case ClaimWizardSteps.Step3
                If Not Me.State.ClaimBO.IsNew Then
                    ControlMgr.SetVisibleControl(Me, Me.btnCancel, False)
                    ControlMgr.SetVisibleControl(Me, Me.btnCancelClaim, True)
                    ControlMgr.SetVisibleControl(Me, Me.btnClaimOverride_Write, False)
                    ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.btnCancelClaim, False)
                End If
                If Not Me.State.ClaimBO.IsNew Then
                    ' Pending Claim
                    If Me.State.ClaimBO.IsDaysLimitExceeded Then
                        If Not (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                                ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                            ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, False)
                        End If
                    End If
                End If
                ControlMgr.SetVisibleControl(Me, btnDenyClaim, Not Me.State.IsEditMode)
                ControlMgr.SetVisibleControl(Me, Me.btnContinue, Not Me.State.InputParameters.ComingFromDenyClaim)

            Case ClaimWizardSteps.Step4
            Case ClaimWizardSteps.Step5
                Me.btnContinue.Text = TranslationBase.TranslateLabelOrMessage("SUBMIT_CLAIM")
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

        If (Not Me.State.CertBO Is Nothing) Then
            moProtectionAndEventDetails.CustomerName = Me.State.CertBO.CustomerName
            moProtectionAndEventDetails.DealerName = Me.State.CertBO.getDealerDescription

            If Me.State.CallerName Is Nothing Then
                If String.IsNullOrEmpty(Me.State.ClaimBO.CallerName) Then
                    moProtectionAndEventDetails.CallerName = Me.State.CertBO.CustomerName
                Else
                    moProtectionAndEventDetails.CallerName = Me.State.ClaimBO.CallerName
                End If
            ElseIf Not (Me.State.CallerName.Equals(String.Empty)) Then
                moProtectionAndEventDetails.CallerName = Me.State.CallerName
            End If

            If State.DateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(State.DateOfLoss)

            moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromCode("CSTAT", Me.State.CertBO.StatusCode)
            If (Me.State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
        End If

        moProtectionAndEventDetails.ProtectionStatusCss = cssClassName

        If (Me.State.DateOfLoss > Date.MinValue) Then
            moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(Me.State.DateOfLoss)
        End If

        If (Not Me.State.CertItemBO Is Nothing) Then
            moProtectionAndEventDetails.EnrolledModel = Me.State.CertItemBO.Model
            moProtectionAndEventDetails.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, Me.State.CertItemBO.RiskTypeId)
            If (Not (Me.State.CertItemBO.ManufacturerId.Equals(Guid.Empty))) Then
                moProtectionAndEventDetails.EnrolledMake = New Manufacturer(Me.State.CertItemBO.ManufacturerId).Description
            End If
        End If

        If Not Me.State.ClaimBO Is Nothing Then

            moProtectionAndEventDetails.ClaimNumber = Me.State.ClaimBO.ClaimNumber
            moProtectionAndEventDetails.ClaimStatus = LookupListNew.GetClaimStatusFromCode(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.ClaimBO.StatusCode)
            moProtectionAndEventDetails.ClaimStatusCss = If(Me.State.ClaimBO.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
            moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(Me.State.ClaimBO.LossDate.Value)

            If Not Me.State.ClaimBO.ClaimedEquipment Is Nothing Then
                moProtectionAndEventDetails.ClaimedMake = Me.State.ClaimBO.ClaimedEquipment.Manufacturer
                moProtectionAndEventDetails.ClaimedModel = Me.State.ClaimBO.ClaimedEquipment.Model
            End If

        End If

    End Sub

    Private Sub PopulateDropDowns(ByVal wizardStep As ClaimWizardSteps)
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())


        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                Me.EnableDisableWizardControls(wizardStep)
                'Me.BindListControlToDataView(Me.step1_cboRiskType, LookupListNew.LoadRiskTypes(State.CertBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                Dim listcontextForrisktypes As ListContext = New ListContext()
                listcontextForrisktypes.CertId = State.CertBO.Id
                listcontextForrisktypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                listcontextForrisktypes.DateOfLoss = State.DateOfLoss

                Dim RiskTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForrisktypes)
                ' step1_cboCoverageType
                Me.step1_cboRiskType.Populate(RiskTypeList, New PopulateOptions() With
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
                    listcontextForcoveragetypes.CertId = Me.State.CertBO.Id
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

                If Me.State.CertItemBO.IsEquipmentRequired Then
                    If Not String.IsNullOrEmpty(Me.State.DealerBO.EquipmentListCode) Then
                        Dim listcontextForManufacturerByEquipmentCode As ListContext = New ListContext()
                        listcontextForManufacturerByEquipmentCode.EquipmentListCode = Me.State.DealerBO.EquipmentListCode
                        listcontextForManufacturerByEquipmentCode.EffectiveOnDate = Date.Now

                        Dim ManufacturerByEquipmentList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByEquipment", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForManufacturerByEquipmentCode)
                        step2_cboManufacturerId.Populate(ManufacturerByEquipmentList, New PopulateOptions() With
                                                            {
                                                            .AddBlankItem = True
                                                            })
                    Else
                        Me.MasterPage.MessageController.AddWarning("EQUIPMENT_LIST_DOES_NOT_EXIST_FOR_DEALER")
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
                listCauseOfLoss.CoverageTypeId = Me.State.ClaimBO.CoverageTypeId
                listCauseOfLoss.LanguageId = Authentication.LangId
                listCauseOfLoss.DealerId = Me.State.CertBO.DealerId
                listCauseOfLoss.ProductCode = Me.State.CertBO.ProductCode
                Dim coverageList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CauseOfLossByCoverageTypeAndSplSvcLookupList", context:=listCauseOfLoss)
                step3_cboCauseOfLossId.Populate(coverageList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                If Me.State.IsSalutation Then
                    step3_cboContactSalutationId.Populate(CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                             {
                                                             .AddBlankItem = True
                                                             })
                    step3_cboCallerSalutationId.Populate(CommonConfigManager.Current.ListManager.GetList("SLTN", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                            {
                                                            .AddBlankItem = True
                                                            })
                End If

                BindListControlToDataView(ddlIssueCode, Me.State.ClaimBO.Load_Filtered_Issues(), "CODE", "ISSUE_ID", False, , True)
                BindListControlToDataView(ddlIssueDescription, Me.State.ClaimBO.Load_Filtered_Issues(), "DESCRIPTION", "ISSUE_ID", False, , True)

                step3_cboDedCollMethod.Populate(CommonConfigManager.Current.ListManager.GetList("DEDCOLLMTHD", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                Dim selDedCollMethod As String = LookupListNew.GetCodeFromId(LookupListCache.LK_DED_COLL_METHOD, Me.State.ClaimBO.DedCollectionMethodID)

                If Not selDedCollMethod Is Nothing Then
                    SetSelectedItem(Me.step3_cboDedCollMethod, LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, selDedCollMethod))
                End If
                'Me.BindListControlToDataView(Me.step3_cboUseShipAddress, LookupListNew.GetYesNoLookupList(Authentication.LangId, False), , , False)
                Dim yesNoLk As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                step3_cboUseShipAddress.Populate(yesNoLk, New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                SetSelectedItem(Me.step3_cboUseShipAddress, NoId)
                ' Me.BindListControlToDataView(Me.step3_cboLawsuitId, yesNoLkL)
                step3_cboLawsuitId.Populate(yesNoLk, New PopulateOptions() With
                                               {
                                               .AddBlankItem = True
                                               })
                ' Me.BindListControlToDataView(Me.step3_cboDeniedReason, LookupListNew.GetDeniedReasonLookupList(Authentication.LangId))

                'KDDI CHANGES
                Dim listcontextForMgList As ListContext = New ListContext()
                listcontextForMgList.CompanyGroupId = Me.State.CertBO.Company.CompanyGroupId
                listcontextForMgList.DealerId = Me.State.CertBO.Dealer.Id
                listcontextForMgList.CompanyId = Me.State.CertBO.CompanyId
                listcontextForMgList.DealerGroupId = Me.State.CertBO.Dealer.DealerGroupId

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

    Private Sub PopulateFormFromBO(ByVal wizardStep As ClaimWizardSteps)
        PopulateProtectionAndEventDetail()
        Dim flag As Boolean = True
        Dim errMsg As List(Of String) = New List(Of String)
        Dim warningMsg As List(Of String) = New List(Of String)
        Me.hdnDealerId.Value = Me.State.DealerBO.Id.ToString
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                If (Not Page.IsPostBack) Then
                    Me.State.DateReported = Date.Today
                End If
                If State.DateReported > Date.MinValue Then PopulateControlFromBOProperty(step1_txtDateReported, GetDateFormattedStringNullable(New DateType(State.DateReported)))
                If State.DateOfLoss > Date.MinValue Then PopulateControlFromBOProperty(step1_moDateOfLossText, GetDateFormattedStringNullable(New DateType(State.DateOfLoss)))

                If (Me.State.CallerName Is Nothing) Then
                    step1_textCallerName.Text = Me.State.CertBO.CustomerName
                Else
                    step1_textCallerName.Text = Me.State.CallerName
                End If
                If (Not Me.State.RiskTypeId.Equals(Guid.Empty)) Then
                    PopulateDropDowns(wizardStep)
                    step1_cboRiskType.SelectedItem.Value = Me.State.RiskTypeId.ToString()
                    If (Not Me.State.CertficateCoverageTypeId.Equals(Guid.Empty)) Then
                        step1_cboCoverageType.SelectedItem.Value = Me.State.CertficateCoverageTypeId.ToString()
                    End If
                End If
                If (Me.State.CallerName Is Nothing) Then
                    step1_textProblemDescription.Text = Me.State.CallerName
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
                For Each claimAuth As ClaimAuthorization In Me.State.ClaimBO.ClaimAuthorizationChildren
                    If Not claimAuth.IsNew Then
                        Me.MasterPage.MessageController.AddWarning("CLAIM_ALREADY_HAS_AUTHORIZATION_WILL_VOID")
                    End If
                Next
            Case ClaimWizardSteps.Step5
                PopulateDropDowns(wizardStep)
                Me.PopulateControlFromBOProperty(Me.step5_TextboxCallerName, Me.State.ClaimBO.CallerName)
                Me.PopulateControlFromBOProperty(Me.step5_TextboxCertificate, Me.State.ClaimBO.Certificate.CertNumber)
                Me.PopulateControlFromBOProperty(Me.step5_TextboxCommentText, Me.State.CommentBO.Comments)
                Me.PopulateControlFromBOProperty(Me.step5_TextboxDealer, Me.State.ClaimBO.Dealer.Dealer)
                Me.PopulateControlFromBOProperty(Me.step5_cboCommentType, Me.State.CommentBO.CommentTypeId)

                If Not Me.State.CommentBO.CreatedDate Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.step5_TextboxDateTime, GetLongDateFormattedString(Me.State.CommentBO.CreatedDate.Value))
                Else
                    Me.PopulateControlFromBOProperty(Me.step5_TextboxDateTime, Nothing)
                End If

                Me.ClearMessageControllers()

        End Select

        If errMsg.Count > 0 Then Me.MasterPage.MessageController.AddError(errMsg.ToArray)
        If warningMsg.Count > 0 Then Me.moMessageController.AddWarning(warningMsg.ToArray)

    End Sub

    Private Sub PopulateBOFromForm(ByVal wizardStep As ClaimWizardSteps)
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                Me.State.RiskTypeId = New Guid(step1_cboRiskType.SelectedItem.Value)
                Me.State.CertficateCoverageTypeId = New Guid(step1_cboCoverageType.SelectedItem.Value)
                Me.State.CallerName = step1_textCallerName.Text
                Me.State.ProblemDescription = step1_textProblemDescription.Text
            Case ClaimWizardSteps.Step2
                With Me.State.CertItemBO
                    Me.PopulateBOProperty(Me.State.CertItemBO, "RiskTypeId", Me.step2_cboRiskTypeId)
                    Me.PopulateBOProperty(Me.State.CertItemBO, "ManufacturerId", Me.step2_cboManufacturerId)
                    Me.PopulateBOProperty(Me.State.CertItemBO, "SerialNumber", Me.step2_TextboxSerialNumber)
                    Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                    If (Not Me.State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
                        Me.PopulateBOProperty(Me.State.CertItemBO, "Model", Me.step2_TextboxModel)
                    End If
                    Me.PopulateBOProperty(Me.State.CertItemCoverageBO, "MethodOfRepairId", Me.step2_cboMethodOfRepair)
                    Me.PopulateBOProperty(Me.State.CertItemBO, "ItemDescription", Me.step2_TextboxDealerItemDesc)
                    Me.PopulateBOProperty(Me.State.CertBO, "InvoiceNumber", Me.step2_TextboxInvNum)
                    Me.PopulateBOProperty(Me.State.CertItemBO, "SkuNumber", Me.step2_TextboxSKU)
                End With
                'check for equipment flag and then populate rest from claim equipment bo of tyep claimed Equipment
                If Not Me.State.step2_claimEquipmentBO Is Nothing AndAlso
                   (Me.State.DealerBO.UseEquipmentId.Equals(Me.State.yesId)) Then
                    With Me.State.step2_claimEquipmentBO
                        Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "ClaimEquipmentDate", Me.State.CertItemBO.CreatedDate.ToString)
                        Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "ManufacturerId", Me.GetSelectedItem(Me.step2_ddlClaimedManuf))
                        Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "Model", Me.step2_txtClaimedModel.Text)
                        'Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "SKU", Me.step2_txtClaimedSKu)

                        Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "SerialNumber", Me.step2_txtClaimedSerialNumber.Text)
                        'get equipment id
                        If (Not Me.State.step2_claimEquipmentBO.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.State.step2_claimEquipmentBO.Model)) Then
                            Me.State.step2_claimEquipmentBO.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(Me.State.DealerBO.EquipmentListCode, DateTime.Now, Me.State.step2_claimEquipmentBO.ManufacturerId, Me.State.step2_claimEquipmentBO.Model)
                        End If
                        If Me.State.step2_claimEquipmentBO.EquipmentId.Equals(Guid.Empty) Then
                            Me.MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")
                            Me.State.step2_claimEquipmentBO.SKU = String.Empty
                            hdnSelectedClaimedSku.Value = String.Empty
                        Else
                            Me.State.step2_claimEquipmentBO.SKU = hdnSelectedClaimedSku.Value
                        End If


                        Me.PopulateBOProperty(Me.State.step2_claimEquipmentBO, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED))
                    End With
                End If

                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Case ClaimWizardSteps.Step3
                PopulateBOFromFormForStep3()
            Case ClaimWizardSteps.Step4

                If Me.step4_RadioButtonNO_SVC_OPTION.Checked Then
                    'REQ-5546
                    'Default SC might not null
                    If Not Me.State.default_service_center_id.Equals(Guid.Empty) Then
                        Me.State.SelectedServiceCenterId = Me.State.default_service_center_id
                    Else
                        Me.MasterPage.MessageController.AddError("TEMP SERVICE CENTER NOT FOUND")
                        Exit Sub
                    End If
                Else
                    If (Not hdnSelectedServiceCenterId.Value = "XXXX") Then Me.State.SelectedServiceCenterId = ServiceCenter.GetServiceCenterID(hdnSelectedServiceCenterId.Value)
                End If
                ' Bug 178224 - REQ- 6156
                If Not State.SelectedServiceCenterId.Equals(Guid.Empty) Then
                    PopulateBOProperty(State.ClaimBO, "ServiceCenterId", State.SelectedServiceCenterId)
                End If
            Case ClaimWizardSteps.Step5
                Me.PopulateBOProperty(Me.State.CommentBO, "CallerName", Me.step5_TextboxCallerName)
                Me.PopulateBOProperty(Me.State.CommentBO, "Comments", Me.step5_TextboxCommentText)
                Me.PopulateBOProperty(Me.State.CommentBO, "CommentTypeId", Me.step5_cboCommentType)
        End Select

    End Sub

    Private Function LastStep() As ClaimWizardSteps
        Select Case Me.State.StepName
            Case ClaimWizardSteps.Step2
                Return ClaimWizardSteps.Step1
            Case ClaimWizardSteps.Step3
                Return ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step4
                Return ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step5
                ' Bug 178224 - REQ- 6156 - if Dealer attribute is Yes, then Skipping the step 4 (choosing Service Center) and move to Step 3
                Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                If Not attvalue Is Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
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
        Select Case Me.State.StepName
            Case ClaimWizardSteps.Step1
                Return ClaimWizardSteps.Step2
            Case ClaimWizardSteps.Step2
                Return ClaimWizardSteps.Step3
            Case ClaimWizardSteps.Step3
                ' Bug 178224 - REQ- 6156 -  REQ- 6156 - if Dealer attribute is Yes, then Skipping the step 4 (choosing Service Center) and move to Step 5
                Dim attvalue As AttributeValue = State.ClaimBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN).FirstOrDefault
                If Not attvalue Is Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
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
        Me.WizardControl.Visible = True
        Dim lstWizardSteps As List(Of StepDefinition) = Me.WizardControl.Steps

        For Each stepDef As StepDefinition In lstWizardSteps
            If (stepDef.StepNumber = DirectCast([Enum].Parse(GetType(ClaimWizardSteps), CStr(Me.State.StepName)), Integer)) Then
                stepDef.IsSelected = True
            Else
                stepDef.IsSelected = False
            End If
        Next
    End Sub

    Private Sub ReturnBackToCallingPage()
        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.CertBO,, State.IsCallerAuthenticated)
        MyBase.ReturnToCallingPage(retObj)
    End Sub

    Private Sub PopulateFormDeductibleFormBOs(ByVal methodOfRepairId As Guid)
        Dim oDeductible As CertItemCoverage.DeductibleType
        oDeductible = CertItemCoverage.GetDeductible(Me.State.CertItemCoverageBO.Id, methodOfRepairId)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDeductibleBasedOn, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, oDeductible.DeductibleBasedOnId))
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDeductible, oDeductible.DeductibleAmount)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDeductiblePercent, oDeductible.DeductiblePercentage)
    End Sub

    Private Sub PopulateFormfromClaimedEquipmentBO()
        If Me.State.CertItemBO.IsEquipmentRequired AndAlso
           Not Me.State.step2_claimEquipmentBO Is Nothing Then
            'Use Equipment is Yes
            Me.PopulateControlFromBOProperty(Me.step2_txtClaimedmake, Me.State.step2_claimEquipmentBO.Manufacturer)
            Me.PopulateControlFromBOProperty(Me.step2_ddlClaimedManuf, Me.State.step2_claimEquipmentBO.ManufacturerId)
            Me.PopulateControlFromBOProperty(Me.step2_txtClaimedModel, Me.State.step2_claimEquipmentBO.Model)
            Me.PopulateControlFromBOProperty(Me.step2_txtClaimedDescription, Me.State.step2_claimEquipmentBO.EquipmentDescription)
            'Me.PopulateControlFromBOProperty(Me.step2_txtClaimedSKu, Me.State.step2_claimEquipmentBO.SKU)
            Me.PopulateControlFromBOProperty(Me.step2_txtClaimedSerialNumber, Me.State.step2_claimEquipmentBO.SerialNumber)

            '***********************Pre populate Claimed SKU based on the Equipment and Model
            Me.step2_ddlClaimedSku.Items.Clear()
            If Not Me.State.step2_claimEquipmentBO.EquipmentId.Equals(Guid.Empty) Then
                Dim dv As DataView = Me.State.CertItemBO.LoadSku(Me.State.step2_claimEquipmentBO.EquipmentId, Me.State.DealerBO.Id)
                If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
                    Me.step2_ddlClaimedSku.DataSource = dv
                    Me.step2_ddlClaimedSku.DataTextField = "SKU_NUMBER"
                    Me.step2_ddlClaimedSku.DataValueField = "SKU_NUMBER"
                    Me.step2_ddlClaimedSku.DataBind()

                    If Not dv Is Nothing AndAlso dv.FindRows(Me.State.step2_claimEquipmentBO.SKU.ToString).Length > 0 Then
                        Me.PopulateControlFromBOProperty(Me.step2_txtClaimedSKu, Me.State.step2_claimEquipmentBO.SKU)
                        Me.step2_ddlClaimedSku.SelectedItem.Value = Me.State.step2_claimEquipmentBO.SKU
                        Me.hdnSelectedClaimedSku.Value = Me.State.step2_claimEquipmentBO.SKU
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

        Me.SetSelectedItem(Me.step2_cboRiskTypeId, Me.State.CertItemBO.RiskTypeId)
        riskTypeDesc = Me.GetSelectedDescription(Me.step2_cboRiskTypeId)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxRiskType, riskTypeDesc)
        Me.step2_TextboxRiskType.ReadOnly = True

        Dim dvEnrolled As DataView
        If Me.State.CertItemBO.IsEquipmentRequired Then
            dvEnrolled = LookupListNew.GetManufacturerbyEquipmentList(Me.State.DealerBO.EquipmentListCode, Date.Now)
        Else
            dvEnrolled = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        End If

        If Not Me.State.CertItemBO.ManufacturerId.Equals(Guid.Empty) Then
            If Not dvEnrolled Is Nothing AndAlso Not BusinessObjectBase.FindRow(Me.State.CertItemBO.ManufacturerId, LookupListNew.COL_ID_NAME, dvEnrolled.Table) Is Nothing Then
                Me.PopulateControlFromBOProperty(Me.step2_cboManufacturerId, Me.State.CertItemBO.ManufacturerId)
            Else
                If Me.State.CertItemBO.IsEquipmentRequired Then
                    Me.MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
                Else
                    Me.MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.INVALID_MAKE_MODEL_ERR)
                End If
            End If
        End If

        'Me.SetSelectedItem(Me.step2_cboManufacturerId, Me.State.CertItemBO.ManufacturerId)
        manufacturerDesc = Me.GetSelectedDescription(Me.step2_cboManufacturerId)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxManufacturer, manufacturerDesc)
        ControlMgr.SetVisibleControl(Me, step2_cboManufacturerId, False)
        ControlMgr.SetVisibleControl(Me, step2_TextboxManufacturer, True)
        Me.step2_TextboxManufacturer.ReadOnly = True

        If (Me.State.CertItemCoverageBO.MethodOfRepairId = Guid.Empty) Then
            Me.SetSelectedItem(Me.step2_cboMethodOfRepair, Me.State.CertBO.MethodOfRepairId)
            MethodOfRepairId = Me.State.CertBO.MethodOfRepairId
        Else
            Me.SetSelectedItem(Me.step2_cboMethodOfRepair, Me.State.CertItemCoverageBO.MethodOfRepairId)
            MethodOfRepairId = Me.State.CertItemCoverageBO.MethodOfRepairId
        End If
        methodOfRepairDesc = Me.GetSelectedDescription(Me.step2_cboMethodOfRepair)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxMethodOfRepair, methodOfRepairDesc)
        ControlMgr.SetVisibleControl(Me, step2_cboMethodOfRepair, False)
        ControlMgr.SetVisibleControl(Me, step2_TextboxMethodOfRepair, True)
        Me.step2_TextboxMethodOfRepair.ReadOnly = True

        Me.PopulateControlFromBOProperty(Me.step2_TextboxSerialNumber, Me.State.CertItemBO.SerialNumber)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxBeginDate, GetDateFormattedStringNullable(Me.State.CertItemCoverageBO.BeginDate))
        Me.PopulateControlFromBOProperty(Me.step2_TextboxEndDate, GetDateFormattedStringNullable(Me.State.CertItemCoverageBO.EndDate))

        Me.PopulateControlFromBOProperty(Me.step2_TextboxLiabilityLimit, Me.State.CertItemCoverageBO.LiabilityLimits, Me.DECIMAL_FORMAT)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxCoverageType, Me.State.CertItemBO.GetCoverageTypeDescription(Me.State.CertItemCoverageBO.CoverageTypeId))
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDateAdded, GetDateFormattedStringNullable(Me.State.CertItemCoverageBO.CreatedDate))
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDealerItemDesc, Me.State.CertItemBO.ItemDescription)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxInvNum, Me.State.CertBO.InvoiceNumber)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxProductCode, Me.State.CertItemBO.CertProductCode)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxYear, Me.State.CertBO.VehicleYear)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxOdometer, Me.State.CertBO.Odometer)
        Me.PopulateControlFromBOProperty(Me.step2_cboApplyDiscount, Me.State.CertItemCoverageBO.IsDiscount)
        Me.SetSelectedItem(Me.step2_cboCalimAllowed, Me.State.CertItemCoverageBO.IsClaimAllowed)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDiscountAmt, Me.State.CertItemCoverageBO.DealerDiscountAmt)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxDiscountPercent, Me.State.CertItemCoverageBO.DealerDiscountPercent)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxSKU, Me.State.CertItemBO.SkuNumber)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxRepairDiscountPct, Me.State.CertItemCoverageBO.RepairDiscountPct)
        Me.PopulateControlFromBOProperty(Me.step2_TextboxReplacementDiscountPct, Me.State.CertItemCoverageBO.ReplacementDiscountPct)
        PopulateFormDeductibleFormBOs(MethodOfRepairId)

        If (Not Me.State.DealerBO.DealerTypeId.Equals(dealerTypeVSC)) Then
            Me.PopulateControlFromBOProperty(Me.step2_TextboxModel, Me.State.CertItemBO.Model)

        Else
            If (Not Me.State.VSCModel Is Nothing) Then
                Me.PopulateControlFromBOProperty(Me.step2_TextboxModel, Me.State.VSCModel.Model)

            Else
                Me.PopulateControlFromBOProperty(Me.step2_TextboxModel, String.Empty)
            End If
        End If

        If (Not Me.State.VSCClassCode Is Nothing) Then
            Me.PopulateControlFromBOProperty(Me.step2_TextboxClassCode, Me.State.VSCClassCode.Code)
        Else
            Me.PopulateControlFromBOProperty(Me.step2_TextboxClassCode, String.Empty)
        End If
        ''''New Equipment Flow starts here
        Dim msgList As New List(Of String)

        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
            If (Me.State.step2_claimEquipmentBO Is Nothing) Then
                If Not Me.State.CertItemBO.CreateClaimedEquipmentFromEnrolledEquipment(Me.State.step2_claimEquipmentBO, msgList) Then
                    'if Manual entry for Claimed Equipment is required
                    Me.MasterPage.MessageController.AddError(msgList.ToArray)
                    ControlMgr.SetVisibleControl(Me, Me.btnContinue, False)
                Else
                    If msgList.Count > 0 Then Me.MasterPage.MessageController.AddWarning(msgList.ToArray)
                End If
            End If

            PopulateFormfromClaimedEquipmentBO()
            SetCtlsForEquipmentMgmt(True) 'REQ 1106

        End If

    End Sub

    Private Sub PopulateFormForClaim()

        Me.State.ClaimBO.RecalculateDeductibleForChanges()
        Me.State.ClaimIssuesView = Me.State.ClaimBO.GetClaimIssuesView()
        Me.State.ClaimImagesView = Me.State.ClaimBO.GetClaimImagesView()

        PopulateClaimIssuesGrid()
        PopulateClaimImagesGrid()
        PopulateClaimAuthorizationGrid()
        PopulateClaimActionGrid()
        PopulateQuestionAnswerGrid()
        PopulateConsequentialDamageGrid()


        Dim oTranslate As Boolean = True
        Dim oCauseOfLoss As String
        Dim oCoverageType As New CoverageType(Me.State.ClaimBO.CoverageTypeId)

        If Not Me.State.ClaimBO.CauseOfLossId.Equals(Guid.Empty) Then
            Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindById(Me.State.ClaimBO.CauseOfLossId)
            If Not oCoverageLoss Is Nothing Then
                Me.SetSelectedItem(Me.step3_cboCauseOfLossId, Me.State.ClaimBO.CauseOfLossId)
            Else
                Me.SetSelectedItem(Me.step3_cboCauseOfLossId, Guid.Empty)
                oCauseOfLoss = LookupListNew.GetCodeFromId(LookupListNew.LK_CAUSES_OF_LOSS, Me.State.ClaimBO.CauseOfLossId)
                oTranslate = False
                Dim strErrorMess As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE)
                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_NOT_AVAILABLE, oCauseOfLoss & "  " & strErrorMess)
            End If

        Else
            Me.SetSelectedItem(Me.step3_cboCauseOfLossId, Me.State.ClaimBO.GetCauseOfLossID(Me.State.ClaimBO.CoverageTypeId))
        End If

        If Not Me.State.ClaimBO.PoliceReport Is Nothing Then
            Me.State.PoliceReportBO = Me.State.ClaimBO.PoliceReport
            Me.mcUserControlPoliceReport.Bind(Me.State.PoliceReportBO, Me.moMessageController)
            Me.PanelPoliceReport.Visible = True
            Me.mcUserControlPoliceReport.ChangeEnabledControlProperty(False)
        Else
            Me.State.PoliceReportBO = Nothing
            Me.PanelPoliceReport.Visible = False
        End If

        txtCreatedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName
        txtCreatedDate.Text = DateTime.Now.ToString(LocalizationMgr.CurrentCulture)

        Me.PopulateControlFromBOProperty(Me.step3_TextboxLossDate, Me.State.ClaimBO.LossDate)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxContactName, Me.State.ClaimBO.ContactName)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxCallerName, Me.State.ClaimBO.CallerName)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxProblemDescription, Me.State.ClaimBO.ProblemDescription)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxLiabilityLimit, Me.State.ClaimBO.LiabilityLimit)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxDeductible_WRITE, Me.State.ClaimBO.Deductible)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxLossDate, Me.State.ClaimBO.LossDate)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxReportDate, Me.State.ClaimBO.ReportedDate)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxCertificateNumber, Me.State.ClaimBO.CertificateNumber)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxClaimNumber, Me.State.ClaimBO.ClaimNumber)
        Me.PopulateControlFromBOProperty(Me.step3_TextBoxDiscount, Me.State.ClaimBO.DiscountAmount)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxPolicyNumber, Me.State.ClaimBO.PolicyNumber)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxOutstandingPremAmt, Me.State.ClaimBO.OutstandingPremiumAmount)
        Me.PopulateControlFromBOProperty(Me.step3_TextboxCALLER_TAX_NUMBER, Me.State.ClaimBO.CallerTaxNumber)
        Me.PopulateControlFromBOProperty(Me.step3_txtNewDeviceSKU, Me.State.ClaimBO.NewDeviceSku)
        Me.PopulateControlFromBOProperty(Me.step3_txtPickupDate, Me.State.ClaimBO.PickUpDate)
        Me.PopulateControlFromBOProperty(Me.step3_txtVisitDate, Me.State.ClaimBO.VisitDate)

        Me.SetSelectedItem(Me.step3_cboLawsuitId, Me.State.ClaimBO.IsLawsuitId)
        Me.PopulateControlFromBOProperty(Me.step3_TxtSpecialInstruction, Me.State.ClaimBO.SpecialInstruction)

        If Me.State.IsSalutation Then
            If Me.State.ClaimBO.CallerSalutationID.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.step3_cboCallerSalutationId, Me.State.CertBO.SalutationId)
            Else
                Me.SetSelectedItem(Me.step3_cboCallerSalutationId, Me.State.ClaimBO.CallerSalutationID)
            End If

            If Me.State.ClaimBO.ContactSalutationID.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.step3_cboContactSalutationId, Me.State.CertBO.SalutationId)
            Else
                Me.SetSelectedItem(Me.step3_cboContactSalutationId, Me.State.ClaimBO.ContactSalutationID)
            End If
        End If

        Dim isNewFulfillment = (Not String.IsNullOrWhiteSpace(Me.State.CertItemCoverageBO.FulfillmentProfileCode))


        If Not Me.State.ClaimBO.ContactInfoId.Equals(Guid.Empty) Then
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            SetSelectedItem(Me.step3_cboUseShipAddress, YesId)
            moUserControlContactInfo.Visible = True

            Me.UserControlAddress.ClaimDetailsBind(Me.State.ClaimBO.ContactInfo.Address)
            Me.UserControlContactInfo.Bind(Me.State.ClaimBO.ContactInfo)
            'This makes all child controls inside UserControlAddress as ReadOnly 
            If isNewFulfillment = True Then
                Me.UserControlAddress.EnableControl(False)
                Me.UserControlContactInfo.EnableControl(False)
            End If

        End If

        If Not State.ClaimBO.LossDate Is Nothing AndAlso Not State.ClaimBO.ReportedDate Is Nothing Then
            'Display warning Message if the period between the Date of loss and the Reported Date is more than the allowed Number of Days
            If Me.State.CertBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.ClaimBO.Dealer.IsGracePeriodSpecified AndAlso Me.State.ClaimBO.IsNew Then


                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithValidCoverage(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_COVERAGE_TYPE_FOR_CLAIM_MISSING_FROM_CERTIFICATE, False)
                    Me.MasterPage.MessageController.AddWarning(denialMessage)
                End If
                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithinGracePeriod(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_GRACE_PERIOD, False)
                    Me.MasterPage.MessageController.AddWarning(denialMessage)
                End If

                If (State.ClaimBO.IsClaimReportedWithinGracePeriod(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value)) And
                   (State.ClaimBO.IsClaimReportedWithValidCoverage(State.ClaimBO.CertificateId, State.ClaimBO.CertItemCoverageId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value)) Then
                    If State.ClaimBO.IsMaxReplacementExceeded(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value) Then
                        Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                        Me.MasterPage.MessageController.AddWarning(denialMessage)
                    End If
                End If

            Else
                If Not State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso
                   Not State.ClaimBO.IsClaimReportedWithinPeriod(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value, State.ClaimBO.ReportedDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_NOT_REPORTED_WITHIN_PERIOD, False)
                    Me.MasterPage.MessageController.AddWarning(denialMessage)
                End If
                'Display warning Message if the Maximum number of Replacements have been exceeded
                If State.ClaimBO.IsMaxReplacementExceeded(State.ClaimBO.CertificateId, State.ClaimBO.LossDate.Value) Then
                    Dim denialMessage As String = CreateClaimDenialMessage(True, Message.MSG_CLAIM_MAX_REPLACEMENT_EXCEEDED, False)
                    Me.MasterPage.MessageController.AddWarning(denialMessage)
                End If
            End If

        End If

        If Me.State.ClaimBO.CertificateItem.IsEquipmentRequired Then

            'populate best replacement record
            With Me.step3_ReplacementOption
                .thisPage = Me
                .ClaimBO = CType(Me.State.ClaimBO, ClaimBase)
                If (Me.State.ClaimBO.Dealer.UseEquipmentId.Equals(Me.State.yesId)) Then
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With
        End If

        If Me.State.ClaimBO.CertificateItem.IsEquipmentRequired Then
            'populate best replacement record
            With Me.step3_ReplacementOption
                .thisPage = Me
                .ClaimBO = CType(Me.State.ClaimBO, ClaimBase)
                If (Me.State.ClaimBO.Dealer.UseEquipmentId.Equals(Me.State.yesId)) Then
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
            If Not .EnrolledEquipment Is Nothing Or Not .ClaimedEquipment Is Nothing Then
                With ucClaimDeviceInfo
                    .thisPage = Me
                    .ClaimBO = CType(State.ClaimBO, ClaimBase)
                    If Not allowEnrolledDeviceUpdate Is Nothing AndAlso allowEnrolledDeviceUpdate.Value = Codes.YESNO_Y Then
                        For Each i As ClaimIssue In State.ClaimBO.ClaimIssuesList
                            If i.IssueCode = ISSUE_CODE_CR_DEVICE_MIS and i.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Then
                                .ShowDeviceEditImg = True
                                Exit For
                            Else 
                                .ShowDeviceEditImg = False
                            End If
                        Next
                    else
                       .ShowDeviceEditImg = False
                    End If
                    
                End With
            End If
        End With
    End Sub

    Private Sub PopulateBOFromFormForStep3()

        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)

        With Me.State.ClaimBO

            Me.PopulateBOProperty(Me.State.ClaimBO, "CauseOfLossId", Me.step3_cboCauseOfLossId)
            Me.PopulateBOProperty(Me.State.ClaimBO, "ContactName", Me.step3_TextboxContactName)
            Me.PopulateBOProperty(Me.State.ClaimBO, "CallerName", Me.step3_TextboxCallerName)
            Me.PopulateBOProperty(Me.State.ClaimBO, "CallerTaxNumber", Me.step3_TextboxCALLER_TAX_NUMBER)
            Me.PopulateBOProperty(Me.State.ClaimBO, "ProblemDescription", Me.step3_TextboxProblemDescription)
            Me.PopulateBOProperty(Me.State.ClaimBO, "Deductible", Me.step3_TextboxDeductible_WRITE)
            Me.PopulateBOProperty(Me.State.ClaimBO, "DiscountAmount", Me.step3_TextBoxDiscount)
            Me.PopulateBOProperty(Me.State.ClaimBO, "PolicyNumber", Me.step3_TextboxPolicyNumber)
            Me.PopulateBOProperty(Me.State.ClaimBO, "LossDate", Me.step3_TextboxLossDate)
            Me.PopulateBOProperty(Me.State.ClaimBO, "ReportedDate", Me.step3_TextboxReportDate)
            Me.PopulateBOProperty(Me.State.ClaimBO, "NewDeviceSku", Me.step3_txtNewDeviceSKU)
            Me.PopulateBOProperty(Me.State.ClaimBO, "IsLawsuitId", Me.step3_cboLawsuitId)
            Me.PopulateBOProperty(Me.State.ClaimBO, "SpecialInstruction", Me.step3_TxtSpecialInstruction)

            If Me.State.ClaimBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                Me.PopulateBOProperty(Me.State.ClaimBO, "LiabilityLimit", Me.step3_TextboxLiabilityLimit)
            End If

            If Me.State.IsSalutation Then
                Me.PopulateBOProperty(Me.State.ClaimBO, "ContactSalutationID", Me.step3_cboContactSalutationId)
                Me.PopulateBOProperty(Me.State.ClaimBO, "CallerSalutationID", Me.step3_cboCallerSalutationId)
            End If

            If Me.PanelPoliceReport.Visible = True Then
                Me.PopulatePoliceReportBOFromUserCtr(True)
            Else
                Me.State.PoliceReportBO = Nothing
            End If

            If Not Me.State.ClaimBO.ContactInfo Is Nothing Then
                If Me.State.ClaimBO.ContactInfo.IsDeleted = False Then
                    Me.State.ClaimBO.ContactInfoId = Me.State.ClaimBO.ContactInfo.Id
                End If
            End If

            If Me.State.IsClaimDenied Then
                Me.PopulateBOProperty(Me.State.ClaimBO, "DeniedReasonId", Me.step3_cboDeniedReason)
                Me.PopulateBOProperty(Me.State.ClaimBO, "Fraudulent", Me.step3_cboFraudulent)
                Me.PopulateBOProperty(Me.State.ClaimBO, "Complaint", Me.step3_cboComplaint)
            End If

            If Me.State.IsClaimDenied Then
                Me.PopulateBOProperty(Me.State.ClaimBO, "DeniedReasonId", Me.step3_cboDeniedReason)
                Me.PopulateBOProperty(Me.State.ClaimBO, "Fraudulent", Me.step3_cboFraudulent)
                Me.PopulateBOProperty(Me.State.ClaimBO, "Complaint", Me.step3_cboComplaint)
            End If

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        If step3_cboUseShipAddress.SelectedValue = Me.State.yesId.ToString Then
            Me.State.ClaimBO.ContactInfo.Address.InforceFieldValidation = True
            UserControlContactInfo.PopulateBOFromControl(True)
            Me.State.ClaimBO.ContactInfo.Save()
        End If

    End Sub

    Private Sub InitialEnableDisableReplacementForStep3()
        ControlMgr.SetVisibleControl(Me, Me.step3_lblNewDeviceSKU, False)
        ControlMgr.SetVisibleControl(Me, Me.step3_txtNewDeviceSKU, False)
        If Me.State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
            ' Check if Deductible Calculation Method is List and SKU Price is resolved
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(Me.State.ClaimBO.CertItemCoverageId, Me.State.ClaimBO.MethodOfRepairId)
            Me.State.DEDUCTIBLE_BASED_ON = oDeductible.DeductibleBasedOn
            'req 1157 added additional condition
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.DealerBO.NewDeviceSkuRequiredId) = Codes.YESNO_Y Or (Me.State.DEDUCTIBLE_BASED_ON = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                ControlMgr.SetVisibleControl(Me, Me.step3_lblNewDeviceSKU, True)
                ControlMgr.SetVisibleControl(Me, Me.step3_txtNewDeviceSKU, True)
            End If
        End If

    End Sub

    Private Sub InitialEnableDisableControlsForStep3()

        Me.ChangeEnabledProperty(Me.step3_TextboxCertificateNumber, False)
        Me.ChangeEnabledProperty(Me.step3_TextboxClaimNumber, False)
        Me.ChangeEnabledProperty(Me.step3_TextboxLiabilityLimit, False)
        Me.ChangeEnabledProperty(Me.step3_TextboxDeductible_WRITE, True)
        Me.ChangeEnabledProperty(Me.step3_TextBoxDiscount, False)
        Me.ChangeEnabledProperty(Me.step3_txtPickupDate, False)
        Me.ChangeEnabledProperty(Me.step3_txtVisitDate, False)

        'Make Invisible for Service Warranty
        If Me.State.ClaimBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
            'Service Warranty Claims
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxLiabilityLimit, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelDeductible, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxDeductible_WRITE, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelDiscount, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextBoxDiscount, False)
        End If

        If Not Me.State.ClaimBO.LossDate Is Nothing Then
            'When editing a claim record created from an existing one
            Me.ChangeEnabledProperty(Me.step3_TextboxLossDate, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_ImageButtonLossDate, False)
        End If

        If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelPolicyNumber, False)
        End If

        InitialEnableDisableReplacementForStep3()

        Dim objCompany As New Company(Me.State.ClaimBO.CompanyId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_SERVICES Then
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxCALLER_TAX_NUMBER, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelCALLER_TAX_NUMBER, False)
        Else
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxCALLER_TAX_NUMBER, True)
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelCALLER_TAX_NUMBER, True)
        End If

        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.ClaimBO.CoverageTypeId, Me.State.ClaimBO.Certificate.DealerId, Authentication.LangId, Me.State.ClaimBO.Certificate.ProductCode, False) Then
            Me.ChangeEnabledProperty(Me.step3_cboCauseOfLossId, True)
        End If
    End Sub

    Private Sub EnableDisableControlsForStep3()
        If Me.State.IsSalutation Then
            ControlMgr.SetVisibleControl(Me, Me.step3_cboContactSalutationId, True)
            ControlMgr.SetVisibleControl(Me, Me.step3_cboCallerSalutationId, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.step3_cboContactSalutationId, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_cboCallerSalutationId, False)
        End If

        If Me.State.ClaimBO.ClaimNumber Is Nothing Then
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelClaimNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxClaimNumber, False)
        End If

        If Me.State.ClaimBO.IsDaysLimitExceeded Then
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.MessageController.AddWarning("DAYS_LIMIT_EXCEEDED")
        End If
        If Me.State.ClaimBO.IsAuthorizationLimitExceeded Then
            Me.moMessageController.Clear()
            Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                                            TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                            TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If

        If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelUseShipAddress, True)
            ControlMgr.SetVisibleControl(Me, Me.step3_cboUseShipAddress, True)
        End If

        If Me.State.ClaimBO.Contract.PayOutstandingPremiumId.Equals(Me.State.yesId) Then
            Me.State.PayOutstandingPremium = True
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelOutstandingPremAmt, True)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxOutstandingPremAmt, True)
        Else
            Me.State.PayOutstandingPremium = False
            ControlMgr.SetVisibleControl(Me, Me.step3_LabelOutstandingPremAmt, False)
            ControlMgr.SetVisibleControl(Me, Me.step3_TextboxOutstandingPremAmt, False)
        End If

        'If No issues to Add to claim hide the Save and Cancel Button
        If (Me.State.ClaimBO.Load_Filtered_Issues().Count = 0) Then
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
        If Me.State.ClaimBO.CertificateItem.IsEquipmentRequired Then
            Me.step3_ReplacementOption.Visible = True
            'Me.dvClaimEquipment.Visible = True
        Else
            Me.step3_ReplacementOption.Visible = False
            'Me.dvClaimEquipment.Visible = False
        End If

    End Sub

    Private Sub BindBoPropertiesToLabels(ByVal wizardStep As ClaimWizardSteps)

        Select Case wizardStep
            Case ClaimWizardSteps.Step2
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "RiskTypeId", Me.step2_LabelRiskTypeId)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "ManufacturerId", Me.step2_LabelMakeId)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "SerialNumber", Me.step2_LabelSerialNumber)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "Model", Me.step2_LabelModel)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "DealerItemDesc", Me.step2_LabelDealerItemDesc)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "SkuNumber", Me.step2_labelSKU)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "BeginDate", Me.step2_LabelBeginDate)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "EndDate", Me.step2_LabelEndDate)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "CreatedDate", Me.step2_LabelDateAdded)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "GetCoverageTypeDescription", Me.step2_LabelCoverageType)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "MethodOfRepairId", Me.step2_LabelMethodOfRepair)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "DealerDiscountAmt", Me.step2_LabelDeductible)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "DealerDiscountPercent", Me.step2_LabelDeductiblePercent)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "LiabilityLimits", Me.step2_LabelLiabilityLimit)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "ProductCode", Me.step2_LabelProductCode)
                Me.BindBOPropertyToLabel(Me.State.CertItemBO, "InvoiceNumber", Me.step2_LabelInvNum)
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
                    Me.BindBOPropertyToLabel(Me.State.step2_claimEquipmentBO, "ManufacturerId", Me.step2_lblClaimedMake)
                    Me.BindBOPropertyToLabel(Me.State.step2_claimEquipmentBO, "Model", Me.step2_lblClaimedModel)
                    Me.BindBOPropertyToLabel(Me.State.step2_claimEquipmentBO, "SKU", Me.step2_lblClaimedSKU)
                    Me.BindBOPropertyToLabel(Me.State.step2_claimEquipmentBO, "EquipmentDescription", Me.step2_LabelClaimDesc)
                    Me.BindBOPropertyToLabel(Me.State.step2_claimEquipmentBO, "SerialNumber", Me.step2_LabelClaimSerialNumber)
                End If
            Case ClaimWizardSteps.Step3
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "CauseOfLossId", Me.step3_LabelCauseOfLossId)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "ContactName", Me.step3_LabelContactName)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "CallerName", Me.step3_LabelCallerName)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "ProblemDescription", Me.step3_LabelProblemDescription)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "LiabilityLimit", Me.step3_LabelLiabilityLimit)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "Deductible", Me.step3_LabelDeductible)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "DiscountAmount", Me.step3_LabelDiscount)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "LossDate", Me.step3_LabelLossDate)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "ReportedDate", Me.step3_LabelReportDate)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "CertificateNumber", Me.step3_LabelCertificateNumber)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "PolicyNumber", Me.step3_LabelPolicyNumber)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "CallerTaxNumber", Me.step3_LabelCALLER_TAX_NUMBER)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "NewDeviceSKU", Me.step3_lblNewDeviceSKU)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "IsLawsuitId", Me.step3_LabelIsLawsuitId)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "DeniedReasonId", Me.step3_lblDeniedReason)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "Fraudulent", Me.step3_lblPotFraudulent)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "Complaint", Me.step3_lblComplaint)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "PickUpDate", Me.step3_lblPickupDate)
                Me.BindBOPropertyToLabel(Me.State.ClaimBO, "VisitDate", Me.step3_lblVisitDate)


            Case ClaimWizardSteps.Step5
                Me.BindBOPropertyToLabel(Me.State.CommentBO, "CreatedDate", Me.step5_LabelDateTime)
                Me.BindBOPropertyToLabel(Me.State.CommentBO, "CallerName", Me.step5_LabelCallerName)
                Me.BindBOPropertyToLabel(Me.State.CommentBO, "CommentTypeId", Me.step5_LabelCommentType)
        End Select
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

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

    Private Sub PopulatePoliceReportBOFromUserCtr(ByVal blnExcludePoliceReportSave As Boolean)
        With Me.State.PoliceReportBO
            .ClaimId = Me.State.ClaimBO.Id
            ' determine validate or dont validate
            Me.mcUserControlPoliceReport.PopulateBOFromControl(blnExcludePoliceReportSave)
        End With
    End Sub

    Private Sub GoToNextStep()
        Me.State.StepName = NextStep()
        Me.State.IsEditMode = False
        PopulateStepUIandData()
        BindBoPropertiesToLabels(Me.State.StepName)
        AddStepLabelDecorations(Me.State.StepName)
    End Sub

    Private Function IsUserAuthorisedToProceed(ByVal wizardStep As ClaimWizardSteps) As Boolean
        Select Case wizardStep
            Case ClaimWizardSteps.Step2

        End Select
    End Function

    Private Sub CreateClaim()

        Dim oldStatus As BasicClaimStatus = Me.State.ClaimBO.Status
        Try
            Me.State.ClaimBO.CreateClaim()
            If Not State.DealerBO.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
                Me.State.ClaimBO.PrepopulateDeductible()
            End If
        Catch ex As Exception
            Me.State.ClaimBO.Status = oldStatus
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
            Select Case Me.State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    Me.step4_RadioButtonByZip.Checked = True
                Case LocateServiceCenterSearchType.ByCity
                    Me.step4_RadioButtonByCity.Checked = True
                Case LocateServiceCenterSearchType.All
                    Me.step4_RadioButtonAll.Checked = True
                Case LocateServiceCenterSearchType.None
                    Me.step4_RadioButtonNO_SVC_OPTION.Checked = True
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateCountryDropdown()
        Dim address As New Address(Me.State.CertBO.AddressId)
        Dim dv As ServiceCenter.ServiceCenterSearchDV
        Dim CountryId As Guid
        CountryId = address.CountryId
        dv = ServiceCenter.getUserCustCountries(CountryId)
        Me.BindListControlToDataView(step4_moCountryDrop, dv, , , False)
        Me.SetSelectedItem(Me.step4_moCountryDrop, CountryId)
    End Sub

    Public Sub PopulateGridOrDropDown()
        'get the customer countryId
        Try
            Dim ServNetworkSvc As New ServiceNetworkSvc
            Dim address As New Address(Me.State.CertBO.AddressId)
            Dim objCompany As New Company(Me.State.CertBO.CompanyId)
            Dim UseZipDistrict As Boolean = True
            Dim DealerType As String
            Dim MethodOfRepairType As String, FlagMethodOfRepairRecovery As Boolean = False

            If objCompany.UseZipDistrictId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")) Then
                UseZipDistrict = False
            End If

            Dim SelectedCountry As New ArrayList
            Dim isNetwork As Boolean
            SelectedCountry.Add(Me.GetSelectedItem(step4_moCountryDrop))
            'Dim srvCenterdIDs As ArrayList = Nothing
            If Not Me.State.DealerBO.ServiceNetworkId.Equals(Guid.Empty) Then
                ServNetworkSvc.ServiceNetworkId = Me.State.DealerBO.ServiceNetworkId
                isNetwork = True
            Else
                isNetwork = False
            End If

            'srvCenterdIDs = ServNetworkSvc.ServiceCentersIDs(isNetwork, cert.MethodOfRepairId)
            DealerType = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.DealerBO.DealerTypeId)  '= Codes.DEALER_TYPES__VSC


            If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Or
               Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__GENERAL Or
               Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__LEGAL Then
                FlagMethodOfRepairRecovery = True
            End If

            Dim dv As ServiceCenter.LocateServiceCenterResultsDv
            Select Case Me.State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    dv = ServiceCenter.LocateServiceCenterByZip(Me.State.CertBO.DealerId, Me.State.CertBO.AddressChild.ZipLocator, Me.State.ClaimBO.RiskTypeId, Me.State.CertItemBO.ManufacturerId, Me.State.CertItemCoverageBO.CoverageTypeCode, SelectedCountry, Me.State.DealerBO.ServiceNetworkId, isNetwork, Me.State.ClaimBO.MethodOfRepairId, UseZipDistrict, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.ByCity
                    dv = ServiceCenter.LocateServiceCenterByCity(Me.State.CertBO.DealerId, Me.State.CertBO.AddressChild.ZipLocator, Me.step4_TextboxCity.Text, Me.State.ClaimBO.RiskTypeId, Me.State.CertItemBO.ManufacturerId, Me.State.CertItemCoverageBO.CoverageTypeCode, SelectedCountry, Me.State.DealerBO.ServiceNetworkId, isNetwork, Me.State.ClaimBO.MethodOfRepairId, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.All
                    Dim selectedSCIds As New ArrayList
                    selectedSCIds.Add(step4_moMultipleColumnDrop.SelectedGuid)
                    Me.State.SelectedServiceCenterId = step4_moMultipleColumnDrop.SelectedGuid
                    PopulateCountryDropdown(SelectedCountry)
                    PopulateServiceCenterGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, Me.State.CertBO.DealerId, Me.State.CertItemBO.ManufacturerId).Tables(0).DefaultView)

                    'REQ-5546
                Case LocateServiceCenterSearchType.None
                    Dim selectedSCIds As New ArrayList

                    'Default SC might not null
                    If Not Me.State.default_service_center_id.Equals(Guid.Empty) Then
                        Me.State.SelectedServiceCenterId = Me.State.default_service_center_id
                    Else
                        Me.MasterPage.MessageController.AddError("TEMP SERVICE CENTER NOT FOUND")
                        Exit Sub
                    End If

                    selectedSCIds.Add(Me.State.SelectedServiceCenterId)
                    PopulateCountryDropdown(SelectedCountry)
                    PopulateServiceCenterGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, Me.State.CertBO.DealerId, Me.State.CertItemBO.ManufacturerId).Tables(0).DefaultView)
            End Select

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateServiceCenterGrid(ByRef dv As DataView)
        Dim MethodOfRepairType As String
        Dim dvCount As Integer = 0

        'cache the search results for multiple grid pages
        Me.State.ServiceCenterView = dv

        If Not Me.State.ServiceCenterView Is Nothing Then
            dvCount = Me.State.ServiceCenterView.Count
        End If

        ShowServiceCenterGridData(Me.State.ServiceCenterView)
        Me.HandleGridMessages(dvCount, True)
    End Sub

    Private Sub PopulateCountryDropdown(ByVal customerCountry As ArrayList)
        Try
            'clear results from intelligent search to reduce the cache storage
            State.ServiceCenterView = Nothing

            Dim dv As DataView, strFilter As String

            dv = ServiceCenter.GetAllServiceCenter(customerCountry, Me.State.ClaimBO.MethodOfRepairId, IsUserCompaniesWithAcctSettings())

            Dim overRideSingularity As Boolean
            If dv.Count > 0 Then
                overRideSingularity = True
            Else
                overRideSingularity = False
            End If

            step4_moMultipleColumnDrop.SetControl(True, step4_moMultipleColumnDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER), overRideSingularity)
            If Not Me.State.SelectedServiceCenterId.Equals(Guid.Empty) Then
                step4_moMultipleColumnDrop.SelectedGuid = Me.State.SelectedServiceCenterId
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function IsUserCompaniesWithAcctSettings() As Boolean
        Dim objAC As AcctCompany
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 0 OrElse (ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 1 AndAlso ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).IsNew = True) Then
            Return False
        Else
            For Each objAC In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies()
                Dim certCompany As New Company(Me.State.CertBO.CompanyId)
                If certCompany.AcctCompanyId.Equals(objAC.Id) Then
                    If objAC.UseAccounting = "Y" AndAlso objAC.AcctSystemId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), FelitaEngine.FELITA_PREFIX) Then Return True
                End If
            Next
            Return False
        End If

    End Function

    Private Sub ShowServiceCenterGridData(ByVal dv As DataView)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearMessageControllers()
        Me.MasterPage.MessageController.Clear()
        Me.moMessageController.Clear()
        Me.moModalCollectDivMsgController.Clear()
        Me.mcIssueStatus.Clear()
    End Sub

    Private Sub AddStepLabelDecorations(ByVal wizardStep As ClaimWizardSteps)
        Select Case wizardStep
            Case ClaimWizardSteps.Step2
                MyBase.AddLabelDecorations(Me.State.CertItemBO)
                If Not Me.State.step2_claimEquipmentBO Is Nothing Then MyBase.AddLabelDecorations(Me.State.step2_claimEquipmentBO)
            Case ClaimWizardSteps.Step3
                MyBase.AddLabelDecorations(Me.State.ClaimBO)
                If Not Me.State.ClaimBO.EnrolledEquipment Is Nothing Then
                    MyBase.AddLabelDecorations(Me.State.ClaimBO.EnrolledEquipment)
                End If
                If Not Me.State.ClaimBO.ClaimedEquipment Is Nothing Then
                    MyBase.AddLabelDecorations(Me.State.ClaimBO.ClaimedEquipment)
                End If
            Case ClaimWizardSteps.Step5
                MyBase.AddLabelDecorations(Me.State.CommentBO)
        End Select
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


    Private Sub PopulateSoftQuestionTree()
        Dim rootNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(TranslationBase.TranslateLabelOrMessage("Soft Questions"), Guid.Empty.ToString)

        Dim softQuestDV As SoftQuestion.SoftQuestionDV, blnFromCert As Boolean = False
        If Me.State.RiskTypeId.Equals(Guid.Empty) Then
            softQuestDV = SoftQuestion.getSoftQuestionGroups(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            softQuestDV = SoftQuestion.getSoftQuestionGroupForRiskType(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.RiskTypeId)
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
        Me.tvQuestion.Nodes.Add(rootNode)
    End Sub
#End Region

#Region "Validation Functions"
    Private Function IsValidDate(ByVal ctrl As TextBox, ByVal ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
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

    Private Function IsDateGreaterThan(ByVal firstDate As Date, ByVal secondDate As Date) As Boolean
        If (firstDate > secondDate) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsTextBoxFilled(ByVal ctrl As TextBox, ByVal ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
        If (ctrl.Text = String.Empty) Then
            AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR), errMsg)
            Return False
        End If
        Return True
    End Function

    Private Function IsDropDownSelected(ByVal ctrl As DropDownList, ByVal ctrlLabel As Label, ByRef errMsg As List(Of String)) As Boolean
        If (ctrl.SelectedIndex < 1) Then
            AddError(ctrlLabel, TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR), errMsg)
            Return False
        End If
        Return True
    End Function

    Private Sub AddError(ByVal labelControl As Label, ByVal message As String, ByRef errMsg As List(Of String))
        errMsg.Add(labelControl.Text & " : " & message)
    End Sub

    Public Function ValidateCertItem(ByRef errMsg As List(Of String), ByRef warningMsg As List(Of String)) As Boolean
        Dim flag As Boolean = True
        flag = Me.State.CertItemCoverageBO.IsCoverageValidToOpenClaim(errMsg, warningMsg, step1_txtDateReported.Text)
        If (Me.GetSelectedItem(step2_cboCalimAllowed).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N))) Then
            flag = flag And False
        End If
        Return flag
    End Function

    Private Sub SetCtlsForEquipmentMgmt(ByVal toggleVisible As Boolean)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.DealerBO.UseEquipmentId) = Codes.YESNO_Y Then
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
            Me.step2_TextboxYear.ReadOnly = True  'force to be read only

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
            Me.MasterPage.MessageController.AddError(errMsg.ToArray, False)
        End If

        Return flag
    End Function

    Private Function ValidateInputs(ByVal wizardStep As ClaimWizardSteps) As Boolean
        Dim errMsg As List(Of String) = New List(Of String)
        Dim warningMsg As List(Of String) = New List(Of String)
        Dim flag As Boolean = True
        Select Case wizardStep
            Case ClaimWizardSteps.Step1
                flag = ValidateInputForStep1BtnSearch()
                If (flag And IsDropDownSelected(step1_cboRiskType, Me.step1_riskTypeLabel, errMsg) And
                    IsDropDownSelected(step1_cboCoverageType, Me.step1_coverageTypeLabel, errMsg) And
                    IsTextBoxFilled(step1_textCallerName, Me.step1_callerNameLabel, errMsg) And
                    IsTextBoxFilled(step1_textProblemDescription, Me.step1_problemDescriptionLabel, errMsg)) Then
                    flag = True
                Else
                    flag = False
                End If
            Case ClaimWizardSteps.Step2
                flag = ValidateCertItem(errMsg, warningMsg)
                If Me.State.CertItemBO.IsEquipmentRequired Then
                    'Validate Claimed and Enrolled Equipment - New Equipment Flow
                    If Not Me.State.step2_claimEquipmentBO Is Nothing Then
                        Dim msgList As New List(Of String)
                        If Not Me.State.step2_claimEquipmentBO.ValidateForClaimProcess(msgList) Then
                            Me.MasterPage.MessageController.AddError(msgList.ToArray, True)
                            flag = flag And False
                        End If
                    End If
                End If
            Case ClaimWizardSteps.Step3
                Dim reportDate As Date
                reportDate = step3_TextboxReportDate.Text
                flag = Me.State.CertItemCoverageBO.IsCoverageValidToOpenClaim(errMsg, warningMsg, reportDate)

                If Me.State.CertItemBO.IsEquipmentRequired Then
                    Dim msgList As New List(Of String)
                    If Not Me.State.ClaimBO.ClaimedEquipment.ValidateForClaimProcess(msgList) Then
                        Me.MasterPage.MessageController.AddError(msgList.ToArray, True)
                        flag = flag And False
                    End If

                    'check for Enrolled equipment
                    If Not Me.State.ClaimBO.EnrolledEquipment Is Nothing Then
                        If Not Me.State.ClaimBO.EnrolledEquipment.ValidateForClaimProcess(msgList) Then
                            Me.MasterPage.MessageController.AddError(msgList.ToArray, True)
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

        If errMsg.Count > 0 Then Me.MasterPage.MessageController.AddError(errMsg.ToArray, True)
        If warningMsg.Count > 0 Then Me.MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)

        Return flag
    End Function

    Private Sub CheckForPossibleWarrantyClaim(ByRef warningMsg As List(Of String))
        Dim claimsDV As DataView = Me.State.CertItemCoverageBO.GetAllClaims(Me.State.CertItemCoverageBO.CoverageTypeId)
        Dim oClaim As Claim
        Dim isDaysLimitExceeded As Boolean = True
        Dim elpasedDaysSinceRepaired As Long

        For Each row As DataRow In claimsDV.Table.Rows
            Dim clmId As Guid = New Guid(CType(row.Item(0), Byte()))
            oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(clmId)
            elpasedDaysSinceRepaired = oClaim.ServiceCenterObject.ServiceWarrantyDays.Value

            If Not oClaim.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                If Not oClaim.PickUpDate Is Nothing Then
                    elpasedDaysSinceRepaired = Date.Now.Subtract(oClaim.PickUpDate.Value).Days
                ElseIf Not oClaim.RepairDate Is Nothing Then
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

        oContract = Contract.GetContract(Me.State.DealerBO.Id, Me.State.CertBO.WarrantySalesDate.Value)
        CoverageType = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), Me.State.CertItemCoverageBO.CoverageTypeId)

        Dim ClaimControl As Boolean = False

        If Not oContract Is Nothing Then
            If LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oContract.ClaimControlID) = "Y" Then
                ClaimControl = True
            End If
        End If

        If CoverageType <> Codes.COVERAGE_TYPE__MANUFACTURER Then
            If Not Me.State.CertBO.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) _
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
        If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            If Me.State.ClaimBO.PolicyNumber Is Nothing Then
                flag = flag And False
                errMsg.Add(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
            End If
        End If

        If Me.State.ClaimBO.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso Me.State.DealerBO.IsGracePeriodSpecified Then
            Return True
        Else
            Me.State.ClaimBO.IsRequiredCheckLossDateForCancelledCert = True
            Me.State.ClaimBO.Validate()
            Return flag
        End If
    End Function

#End Region

#Region "Data Grids Logic"

#Region "MasterClaimGrid"
    Private Sub PopulateMasterClaimGrid()
        Dim allowDifferentCoverage As Boolean = False
        Dim oContract As Contract = Contract.GetContract(Me.State.CertBO.DealerId, Me.State.CertBO.WarrantySalesDate.Value)
        If Not oContract Is Nothing _
           AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then allowDifferentCoverage = True

        Dim dv As Claim.MaterClaimDV = Claim.getList(Me.State.CertItemCoverageBO.Id, allowDifferentCoverage, Me.State.CertBO.getMasterclaimProcFlag, Me.State.DateOfLoss)
        Me.grdMasterClaim.DataSource = dv
        Me.grdMasterClaim.DataBind()
    End Sub

    Private Sub GrdMasterClaimGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMasterClaim.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GrdMasterClaimGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMasterClaim.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnSelectClaim As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(0).FindControl("btnSelectClaim") Is Nothing) Then
                    btnSelectClaim = CType(e.Row.Cells(0).FindControl("btnSelectClaim"), LinkButton)
                    btnSelectClaim.CommandArgument = CType(dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER), String)
                    btnSelectClaim.CommandName = SELECT_ACTION_COMMAND
                    btnSelectClaim.Text = CType(dvRow(Claim.MaterClaimDV.COL_NAME_MASTER_CLAIM_NUMBER), String)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GrdMasterClaimGrid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdMasterClaim.RowCommand

        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
                    Me.State.MasterClaimNumber = e.CommandArgument.ToString
                    Me.State.DateOfLoss = DateHelper.GetDateValue(CType(row.Cells(2), DataControlFieldCell).Text)
                    GoToNextStep()
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Claim Issues Grid related"

    Public Sub PopulateClaimIssuesGrid()

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

        If (Me.State.ClaimBO.HasIssues) Then
            Select Case Me.State.ClaimBO.IssuesStatus()
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
            Me.PopulateClaimIssuesGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.SelectedClaimIssueId = Guid.Empty
            PopulateClaimIssuesGrid()
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
                If String.IsNullOrWhiteSpace(dvRow(Claim.ClaimIssuesView.COL_CREATED_DATE).ToString) = False Then
                    e.Row.Cells(Me.GRID_COL_CREATED_DATE_IDX).Text = GetLongDate12FormattedStringNullable(dvRow(Claim.ClaimIssuesView.COL_CREATED_DATE).ToString)
                End If
                If String.IsNullOrWhiteSpace(dvRow(Claim.ClaimIssuesView.COL_PROCESSED_DATE).ToString) = False Then
                    e.Row.Cells(Me.GRID_COL_PROCESSED_DATE_IDX).Text = GetLongDate12FormattedStringNullable(dvRow(Claim.ClaimIssuesView.COL_PROCESSED_DATE).ToString)
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
                    Me.callPage(ClaimIssueDetailForm.URL, New ClaimIssueDetailForm.Parameters(CType(Me.State.ClaimBO, ClaimBase), Me.State.SelectedClaimIssueId))
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
            PopulateClaimIssuesGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Claim Image Related Grid"

    Private Sub GridClaimImages_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimImages.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnLinkImage As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                'Link to the image 
                If (Not e.Row.Cells(0).FindControl("btnImageLink") Is Nothing) Then
                    btnLinkImage = CType(e.Row.Cells(0).FindControl("btnImageLink"), LinkButton)
                    btnLinkImage.Text = CType(dvRow(Claim.ClaimImagesView.COL_FILE_NAME), String)
                    'btnLinkImage.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte()))
                    btnLinkImage.CommandArgument = String.Format("{0};{1};{2}", GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimImagesView.COL_IMAGE_ID), Byte())), Me.State.ClaimBO.Id, CType(dvRow(Claim.ClaimImagesView.COL_IS_LOCAL_REPOSITORY), String))
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
            Me.State.ClaimImagesView = Me.State.ClaimBO.GetClaimImagesView
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

#Region "Claim Authorization Grid related"

    Public Sub PopulateClaimAuthorizationGrid()
        Me.GridClaimAuthorization.AutoGenerateColumns = False
        Me.GridClaimAuthorization.DataSource = Me.State.ClaimBO.ClaimAuthorizationChildren.OrderBy(Function(x) (x.AuthorizationNumber)).ToList()
        Me.GridClaimAuthorization.DataBind()
        If (Me.State.ClaimBO.ClaimAuthorizationChildren.Count > 0) Then
            Me.State.IsGridVisible = True
        Else
            Me.State.IsGridVisible = False
        End If
        ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, Me.State.IsGridVisible)
    End Sub

    Private Sub GridClaimAuthorization_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound

        Try
            Dim claimAuth As ClaimAuthorization = CType(e.Row.DataItem, ClaimAuthorization)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                ' Convert short status codes to full description with css
                e.Row.Cells(Me.GRIDCLA_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, claimAuth.ClaimAuthorizationStatusCode)
                e.Row.Cells(Me.GRIDCLA_COL_AMOUNT_IDX).Text = Me.GetAmountFormattedString(claimAuth.AuthorizedAmount.Value)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Claim Case Grid Related Functions"

    Public Sub PopulateClaimActionGrid()

        Try
            If (Me.State.ClaimActionListDV Is Nothing) Then
                Me.State.ClaimActionListDV = CaseAction.GetClaimActionList(Me.State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
                Me.State.CaseQuestionAnswerListDV = CaseQuestionAnswer.getClaimCaseQuestionAnswerList(Me.State.ClaimBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
    Private Sub CaseQuestionAnswerGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CaseQuestionAnswerGrid.RowDataBound
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
            ucClaimConsequentialDamage.UpdateConsequentialDamagestatus(CType(Me.State.ClaimBO, ClaimBase))
        End If
        ucClaimConsequentialDamage.PopulateConsequentialDamage(CType(Me.State.ClaimBO, ClaimBase))
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
            If (Not String.IsNullOrWhiteSpace(Me.State.CertItemCoverageBO.FulfillmentProfileCode)) Then

                wsResponseObject = WcfClientHelper.Execute(Of Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGateway, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse)(
                                    GetClaimFulfillmentWebAppGatewayClient(),
                                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                Function(ByVal c As Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.WebAppGatewayClient)
                                    Return c.BeginFulfillment(wsCBFRequest)
                                End Function)

            Else

                wsResponseObject = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                                GetClient(),
                            New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                            Function(ByVal c As FulfillmentServiceClient)
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
                    Me.MasterPage.MessageController.AddError(wsResponseList.Error.ErrorCode & " - " & wsResponseList.Error.ErrorMessage, False)
                    blnSuccess = False
                End If
            End If
            If wsResponseObject.GetType() Is GetType(Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse) Then
                Dim wsResponseList As Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse = DirectCast(wsResponseObject, Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService.BeginFulfillmentResponse)
                If IsNothing(wsResponseList.FulfillmentNumber) Or IsNothing(wsResponseList.CompanyCode) Then
                    Me.MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, False)
                    blnSuccess = False
                End If
            End If
        End If
        Return blnSuccess
    End Function

    Private Sub btnClaimDeductibleRefund_Click(sender As Object, e As EventArgs) Handles btnClaimDeductibleRefund.Click
        Try
            Dim FlowName As String = CLAIM_WIZARD_FROM_EVENT_DETAILS
            Dim nav As New ElitaPlusNavigation
            Me.NavController = New NavControllerBase(nav.Flow(FlowName))
            Me.NavController.State = New MyState
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_DEDUCTIBLE_REFUND, New ClaimDeductibleRefundForm.Parameters(CType(Me.State.ClaimBO, ClaimBase)))

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


End Class



