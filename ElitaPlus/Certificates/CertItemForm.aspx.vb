'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/9/2004)  ********************

Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements


Namespace Certificates

    Partial Class CertItemForm
        Inherits ElitaPlusPage
        Protected WithEvents ErrorCtrl As ErrorController

        Protected WithEvents LabelCoverageTypeId As System.Web.UI.WebControls.Label
        Protected WithEvents cboCoverageTypeId As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ManufacturerMissinsLabel As System.Web.UI.WebControls.Label
        Protected WithEvents ButtonPolicyInvoiceInfo As System.Web.UI.WebControls.Button
        Protected WithEvents moWarrantySoldOnLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moDealerNameLabel As System.Web.UI.WebControls.Label
        Protected WithEvents TableVSC As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents TDYear As System.Web.UI.HtmlControls.HtmlTableCell


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
        Private Const NO_DATA As String = " - "
        Public Const URL As String = "CertItemForm.aspx"
        Public Const Active As String = "A"
        Public Const Manufacture As String = "M"
        Public Const Extended As String = "E"
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
        Public Const VSCCode As String = "2"
        Public Const CLOSED As String = "C"
        Private Const PROTECTION_AND_EVENT_DETAILS As String = "PROTECTION_AND_EVENT_DETAILS"
        Private Const CERTIFICATES As String = "CERTIFICATES"
        Public Const CALLED_FROM_GRID_LINK As String = "FromLink"
        Public Const CALLED_FROM_NEW_CLAIM As String = "NEW_CLAIM"
#End Region

#Region "Variables"

        Private mbIsFirstPass As Boolean = True
        Private ErrorMessages() As String       'to hold all error message
#End Region

#Region "Page Parameters"
        Public Class Parameters
            Public DealerId As Guid
            Public ZipLocator As String
            Public RiskTypeId As Guid
            Public ManufacturerId As Guid
            Public ShowAcceptButton As Boolean = True
            Public CovTypeCode As String
            Public CertItemCoverageId As Guid
            Public ClaimId As Guid
            Public WhenAcceptGoToCreateClaim As Boolean
            Public ComingFromDenyClaim As Boolean = False
            Public RecoveryButtonClick As Boolean = False
            Public ClaimedModel As String
            Public ClaimedManufacturerId As Guid
            Public sCallingObject As String
            Public Sub New(ByVal dealerId As Guid, ByVal zipLocator As String, _
                            ByVal riskTypeId As Guid, ByVal ManufacturerId As Guid, _
                            ByVal covTypeCode As String, _
                            ByVal certItemCoverageId As Guid, _
                            ByVal ClaimId As Guid, _
                            Optional ByVal showAcceptButton As Boolean = True, _
                            Optional ByVal whenAcceptGoToCreateClaim As Boolean = True, _
                            Optional ByVal ComingFromDenyClaim As Boolean = False, _
                            Optional ByVal RecoveryButtonClick As Boolean = False)
                Me.DealerId = dealerId
                Me.ZipLocator = zipLocator
                Me.RiskTypeId = riskTypeId
                Me.ManufacturerId = ManufacturerId
                Me.CovTypeCode = covTypeCode
                Me.ShowAcceptButton = showAcceptButton
                Me.WhenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
                Me.CertItemCoverageId = certItemCoverageId
                Me.ClaimId = ClaimId
                Me.ComingFromDenyClaim = ComingFromDenyClaim
                Me.RecoveryButtonClick = RecoveryButtonClick
            End Sub
            Public Sub New(ByVal callingObject As String)
                Me.sCallingObject = callingObject
            End Sub
        End Class
#End Region

#Region "Attributes"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CertItem
            Public BoChanged As Boolean = False
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CertItem, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.BoChanged = boChanged
            End Sub
        End Class
#End Region

#Region "Properties"



        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(Me.State.certificateCompanyId)

                Return companyBO.Code
            End Get

        End Property


#End Region

#Region "Page State"


        Class MyState
            Public MyBO As CertItem
            Public ScreenSnapShotBO As CertItem
            Public certificateId As Guid
            Public certificateCompanyId As Guid
            Public certificateItemId As Guid
            Public certificateCoverageId As Guid
            Public coverageInEffect As Boolean
            '5623
            Public coverageInEffectforGracePeriod As Boolean
            Public manufacturerMissing As Boolean
            Public inputParameters As Parameters
            'Public companyIsInsurace As Boolean
            Public zipMissing As Boolean
            Public customerNameMissing As Boolean
            Public IdentificationNumber As Boolean
            Public activeOrPendingClaim As Boolean = False
            Public waitingPeriod As Boolean
            Public suspendedCertificate As Boolean
            Public invoiceNumberMissing As Boolean
            Public productSalesDateMissing As Boolean
            Public depreciationScheduleNotDefined As Boolean
            Public IsDaysLimitExceeded As Boolean
            Public boChanged As Boolean = False
            Public companyCode As String
            Public selDateOfLoss As Date
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String

            Public noContractFoundErrorAdded As Boolean = False
            Public _moCertItemCoverage As CertItemCoverage
            Public _moCertificate As Certificate
            Public _moContract As Contract
            Public _moVSCModel As VSCModel
            Public _moVSCClassCode As VSCClassCode
            Public _modealer As Dealer
            Public _IsEdit As Boolean = False
            Public allowdifferentcoverage As Boolean = False

            '#REQ 1106
            Public objClaimedEquipment As ClaimEquipment
        End Class

        Protected Sub InitializeFromFlowSession()
            Me.State.inputParameters = CType(Me.NavController.ParametersPassed, Parameters)
        End Sub

        Public Property moCertItemCoverage() As CertItemCoverage
            Get
                Return Me.State._moCertItemCoverage
            End Get
            Set(ByVal Value As CertItemCoverage)
                Me.State._moCertItemCoverage = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set(ByVal Value As Certificate)
                Me.State._moCertificate = Value
            End Set
        End Property

        Public Property moContract() As Contract
            Get
                Return Me.State._moContract
            End Get
            Set(ByVal Value As Contract)
                Me.State._moContract = Value
            End Set
        End Property

        Public Property moVSCModel() As VSCModel
            Get
                Return Me.State._moVSCModel
            End Get
            Set(ByVal Value As VSCModel)
                Me.State._moVSCModel = Value
            End Set
        End Property

        Public Property moVSCClassCode() As VSCClassCode
            Get
                Return Me.State._moVSCClassCode
            End Get
            Set(ByVal Value As VSCClassCode)
                Me.State._moVSCClassCode = Value
            End Set
        End Property

        Public Property IsEdit() As Boolean
            Get
                Return Me.State._IsEdit
            End Get
            Set(ByVal Value As Boolean)
                Me.State._IsEdit = Value
            End Set
        End Property

        Public Property moDealer() As Dealer
            Get
                Return Me.State._modealer
            End Get
            Set(ByVal Value As Dealer)
                Me.State._modealer = Value
            End Set
        End Property

        Public Sub New()
            'MyBase.New(New MyState)
            MyBase.New(True)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                'Return CType(MyBase.State, MyState) 'arf commented out 12-20-04
                'arf 12-20-04 begin
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    moCertItemCoverage = New CertItemCoverage(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID), Guid))
                    Me.State.certificateCoverageId = moCertItemCoverage.Id

                    Me.State.MyBO = New CertItem(moCertItemCoverage.CertItemId)
                    Me.State.certificateItemId = Me.State.MyBO.Id

                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                    Me.State.selDateOfLoss = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), Date)
                    moDealer = New Dealer(moCertificate.DealerId)
                    If Not moCertificate.ModelId.Equals(Guid.Empty) Then
                        moVSCModel = New VSCModel(moCertificate.ModelId)
                    End If
                    If Not moCertificate.ClassCodeId.Equals(Guid.Empty) Then
                        moVSCClassCode = New VSCClassCode(moCertificate.ClassCodeId)
                    End If
                    InitializeFromFlowSession()
                End If
                Return CType(Me.NavController.State, MyState)
                'arf 12-20-04 end
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try

                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    moCertItemCoverage = New CertItemCoverage(CType(Me.CallingParameters, Guid))
                    Me.State.certificateCoverageId = moCertItemCoverage.Id

                    Me.State.MyBO = New CertItem(moCertItemCoverage.CertItemId)
                    Me.State.certificateItemId = Me.State.MyBO.Id

                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.boChanged = False

                    moDealer = New Dealer(moCertificate.DealerId)
                    If Not moCertificate.ModelId.Equals(Guid.Empty) Then
                        moVSCModel = New VSCModel(moCertificate.ModelId)
                    End If
                    If Not moCertificate.ClassCodeId.Equals(Guid.Empty) Then
                        moVSCClassCode = New VSCClassCode(moCertificate.ClassCodeId)
                    End If

                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Select Case Me.CalledUrl
                    Case LocateServiceCenterForm.URL
                End Select
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Page Events"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State.MyBO Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & Me.State.MyBO.Cert.CertNumber & ElitaBase.Sperator & _
                        "Coverage"
                End If
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If
            Me.ClearGridHeadersAndLabelsErrSign()
            Me.MasterPage.MessageController.Clear()

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
            UpdateBreadCrum()
            Me.headerDeviceInfo.InnerText = TranslationBase.TranslateLabelOrMessage("DEVICE_INFORMATION")
            ddlClaimedManuf.Attributes.Add("onchange", "LoadSKU();")
            txtClaimedModel.Attributes.Add("onchange", "LoadSKU();")
            ddlClaimedSkuNumber.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", ddlClaimedSkuNumber.ClientID, hdnSelectedClaimedSku.ClientID))
            'CheckIfComingFromSaveConfirm()
            Try

                If (Not IsEdit) Then
                    EnableDisableControls(Me.EditPanel_WRITE, True)
                    EnableDisableControls(Me.pnlVehicleInfo, True)
                    EnableDisableControls(Me.pnlDeviceInfo, True)
                End If

                'DEF-2560
                'Set SRC iFrame to NULL. This 'll "SoftQuestion.aspx" prevent to load on every request.
                frameSoftQuestions.Attributes.Add("src", "")
                'DEF-2560

                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CertItem
                    End If
                    Trace(Me, GuidControl.GuidToHexString(Me.State.MyBO.Id))
                    Me.State.companyCode = GetCompanyCode

                    PopulateDropdowns()
                    ControlMgr.SetEnableControl(Me, Me.ButtonLocateCenter, Not Me.IsEdit)
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()

                    lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                    btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancel.UniqueID))

                    If Not moCertItemCoverage Is Nothing Then
                        Dim errMsg As List(Of String)
                        Dim warningMsg As List(Of String)
                        Dim strDateOfReport As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
                        Dim objDateOfReport As DateType = Nothing
                        If Not strDateOfReport Is Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                            objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                        End If
                        If Not moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg, objDateOfReport) Then
                            Me.MasterPage.MessageController.AddError(errMsg.ToArray, True)
                            ControlMgr.SetEnableControl(Me, Me.ButtonLocateCenter, False)
                            Exit Sub ' no need to anything because we have error on coverage
                        Else
                            If warningMsg.Count > 0 Then Me.MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
                        End If
                    End If
                    'lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                    'btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancel.UniqueID))
                    'Page.Validate()
                    'AGL
                    mdlPopup.Hide()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                    If Not Me.State.objClaimedEquipment Is Nothing Then Me.AddLabelDecorations(Me.State.objClaimedEquipment)
                End If

                If Not Me.State.inputParameters Is Nothing Then
                    If Me.State.inputParameters.sCallingObject = CALLED_FROM_GRID_LINK Then
                        'START DEF-2531
                        ControlMgr.SetEnableControl(Me, Me.ButtonLocateCenter, False)
                        ControlMgr.SetVisibleControl(Me, Me.ButtonLocateCenter, False)
                        ControlMgr.SetVisibleControl(Me, Me.btnCancel, False)
                        'END    DEF-2531
                        Me.MasterPage.PageTitle = ""
                        Me.moProtectionAndEventDetails.Visible = False
                        Me.WizardControl.Visible = False

                    End If
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub PopulateProtectionAndEventDetail()
            Dim cssClassName As String
            Dim dateOfLoss As Date
            Dim certItemCvg As CertItemCoverage
            Dim certItem As CertItem
            Try
                certItemCvg = New CertItemCoverage(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID), Guid))
                certItem = New CertItem(certItemCvg.CertItemId)
                Dim cert As New Certificate(certItemCvg.CertId)
                dateOfLoss = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), Date)
                moProtectionAndEventDetails.CustomerName = cert.CustomerName
                If (Not (certItem.ManufacturerId.Equals(Guid.Empty))) Then
                    moProtectionAndEventDetails.EnrolledMake = New Manufacturer(certItem.ManufacturerId).Description
                Else
                    moProtectionAndEventDetails.EnrolledMake = String.Empty
                End If
                moProtectionAndEventDetails.ClaimNumber = NO_DATA
                moProtectionAndEventDetails.DealerName = cert.getDealerDescription
                moProtectionAndEventDetails.EnrolledModel = certItem.Model
                moProtectionAndEventDetails.ClaimStatus = NO_DATA
                moProtectionAndEventDetails.CallerName = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME), String)

                If dateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(dateOfLoss)
                moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromId("SUBSTAT", cert.SubscriberStatus)
                If (LookupListNew.GetCodeFromId("SUBSTAT", cert.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If
                moProtectionAndEventDetails.ProtectionStatusCss = cssClassName
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    If Not Me.State.objClaimedEquipment Is Nothing Then
                        moProtectionAndEventDetails.ClaimedModel = Me.State.objClaimedEquipment.Model
                        moProtectionAndEventDetails.ClaimedMake = Me.State.objClaimedEquipment.Manufacturer
                    Else
                        moProtectionAndEventDetails.ClaimedModel = NO_DATA
                        moProtectionAndEventDetails.ClaimedMake = NO_DATA
                    End If
                End If
                moProtectionAndEventDetails.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, certItem.RiskTypeId)
                moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(dateOfLoss)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                Me.btnBack.Enabled = True

                ControlMgr.SetEnableControl(Me, ButtonSoftQuestions, Not Me.IsEdit)
                ControlMgr.SetEnableControl(Me, btnEdit_WRITE, Not Me.IsEdit)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, Me.IsEdit)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, Me.IsEdit)
                ControlMgr.SetEnableControl(Me, btnBack, Not Me.IsEdit)

                'Me.MenuEnabled = not Me.IsEdit
                ControlMgr.SetVisibleControl(Me, cboRiskTypeId, Me.IsEdit)
                'Me.cboRiskTypeId.Enabled = Me.IsEdit
                Me.SetEnabledForControlFamily(Me.LabelRiskTypeId, Me.IsEdit, True)
                Me.SetEnabledForControlFamily(Me.cboRiskTypeId, Me.IsEdit, True)
                ControlMgr.SetVisibleControl(Me, TextboxRiskType, Not Me.IsEdit)
                Me.SetEnabledForControlFamily(Me.TextboxRiskType, Me.IsEdit, True)
                Me.cboManufacturerId.Enabled = True


                ControlMgr.SetVisibleControl(Me, cboMethodOfRepair, Me.IsEdit)
                Me.cboMethodOfRepair.Enabled = True
                ControlMgr.SetVisibleControl(Me, TextboxMethodOfRepair, Not Me.IsEdit)

                Me.TextboxInvNum.ReadOnly = Not Me.IsEdit
                ControlMgr.SetVisibleControl(Me, lblEnrolledDeviceInfo, True)

                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    ControlMgr.SetEnableControl(Me, ddlClaimedManuf, Me.IsEdit)
                    ControlMgr.SetVisibleControl(Me, ddlClaimedManuf, Me.IsEdit)
                    ControlMgr.SetVisibleControl(Me, txtClaimedmake, Me.IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimedModel, Me.IsEdit)
                    ControlMgr.SetVisibleControl(Me, txtClaimedSKu, Me.IsEdit)
                    ControlMgr.SetVisibleControl(Me, ddlClaimedSkuNumber, Me.IsEdit)
                    ControlMgr.SetEnableControl(Me, ddlClaimedSkuNumber, Me.IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimedDescription, Me.IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimSerialNumber, Me.IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimIMEINumber, Me.IsEdit)
                    txtClaimedModel.ReadOnly = Not Me.IsEdit
                    txtClaimedSKu.ReadOnly = Not Me.IsEdit
                    txtClaimSerialNumber.ReadOnly = Not Me.IsEdit
                    txtClaimIMEINumber.ReadOnly = Not Me.IsEdit
                Else 'allow the entrolled equipment data to be editable for dealer not using equipment management
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, Me.IsEdit)
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, Not Me.IsEdit)
                    Me.TextboxSerialNumber.ReadOnly = Not Me.IsEdit
                    Me.TextboxIMEINumber.ReadOnly = Not Me.IsEdit
                    Me.TextboxModel.ReadOnly = Not Me.IsEdit
                    'ControlMgr.SetVisibleControl(Me, TextboxDealerItemDesc, Me.IsEdit)
                    Me.TextboxDealerItemDesc.ReadOnly = Not Me.IsEdit
                End If

                'TODO Revisit after speaking with Tito
                'Me.ButtonSoftQuestions.Enabled = False



                Me.TextboxEndDate.Font.Bold = Me.IsEdit
                Me.TextboxBeginDate.Font.Bold = Me.IsEdit

                If Me.moCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso moDealer.IsGracePeriodSpecified Then
                    If Not Me.State.coverageInEffectforGracePeriod Then
                        Me.MasterPage.MessageController.AddWarning("COVERAGE IS NOT IN EFFECT", True)
                        Me.TextboxBeginDate.ForeColor = Color.Red
                        Me.TextboxEndDate.ForeColor = Color.Red
                    Else
                        Me.TextboxBeginDate.ForeColor = Color.Green
                        Me.TextboxEndDate.ForeColor = Color.Green
                    End If
                Else
                    If Not Me.State.coverageInEffect Then
                        Me.MasterPage.MessageController.AddWarning("COVERAGE IS NOT IN EFFECT", True)
                        Me.TextboxBeginDate.ForeColor = Color.Red
                        Me.TextboxEndDate.ForeColor = Color.Red
                    Else
                        Me.TextboxBeginDate.ForeColor = Color.Green
                        Me.TextboxEndDate.ForeColor = Color.Green
                    End If

                End If

                Me.TextboxClassCode.ReadOnly = Me.IsEdit
                Me.TextboxDiscountAmt.ReadOnly = Me.IsEdit
                Me.TextboxDiscountPercent.ReadOnly = Me.IsEdit
                Me.TextboxOdometer.ReadOnly = Me.IsEdit
                Me.cboCalimAllowed.Enabled = Not Me.IsEdit
                Me.cboApplyDiscount.Enabled = Not Me.IsEdit
                'Me.TextboxModel.ReadOnly = Me.IsEdit
                Me.TextboxYear.ReadOnly = Me.IsEdit
                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                    pnlVehicleInfo.Visible = False
                    'TRVSC.Visible = False
                    'TRVSC1.Visible = False
                    'TRVSC2.Visible = False
                    'TRVSC3.Visible = False
                    ''Me.TextboxModel.ReadOnly = False
                    Me.LabelYear.Visible = False
                    Me.TextboxYear.Visible = False
                    If Not Me.State.objClaimedEquipment Is Nothing Then
                        If Me.State.objClaimedEquipment.IMEINumber Is Nothing Then
                            Me.LabelClaimSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                            ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                            ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                        Else
                            Me.LabelClaimSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_No_label") + ":"
                            ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, True)
                            ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, True)
                        End If
                    Else
                        Me.LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                        ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                        ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                    End If
                Else
                    Me.LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("VIN") + ":"
                    If Me.State.objClaimedEquipment Is Nothing Then
                        ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                        ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                    End If
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, False)
                    Me.cboManufacturerId.Enabled = False
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, True)
                    Me.TextboxModel.ReadOnly = True
                    Me.TextboxSKU.ReadOnly = Not Me.IsEdit
                End If

                If Me.State._modealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                    Me.LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                    ControlMgr.SetVisibleControl(Me, LabelIMEINumber, False)
                    ControlMgr.SetVisibleControl(Me, TextboxIMEINumber, False)
                Else
                    Me.LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_No_label") + ":"
                    ControlMgr.SetVisibleControl(Me, LabelIMEINumber, True)
                    ControlMgr.SetVisibleControl(Me, TextboxIMEINumber, True)
                End If

                ControlMgr.SetEnableControl(Me, btnDenyClaim, False)
                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                If (Not Me.moCertItemCoverage.IsClaimAllowed.Equals(NoId)) Then 'Or Me.moCertItemCoverage.IsClaimAllowed.Equals(Guid.Empty) Then
                    Dim todayDate As Date
                    If todayDate.Today < Me.moCertItemCoverage.BeginDate.Value And Me.moCertificate.StatusCode <> CLOSED Then
                        'If Not Me.State.coverageInEffect And Me.moCertificate.StatusCode <> CLOSED Then
                        ControlMgr.SetEnableControl(Me, btnDenyClaim, True)
                        'Else
                        '    ControlMgr.SetEnableControl(Me, btnDenyClaim, False)
                    End If
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub SetCtlsForEquipmentMgmt(ByVal toggleVisible As Boolean)
            '#REQ 1106 Start
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                'text box and drop down list
                ControlMgr.SetVisibleControl(Me, ddlClaimedManuf, Not toggleVisible)
                ControlMgr.SetVisibleControl(Me, txtClaimedmake, toggleVisible)
                ControlMgr.SetVisibleControl(Me, txtClaimedModel, toggleVisible)
                ControlMgr.SetVisibleControl(Me, txtClaimedSKu, toggleVisible)
                ControlMgr.SetVisibleControl(Me, txtClaimedDescription, toggleVisible)
                ControlMgr.SetVisibleControl(Me, txtClaimSerialNumber, toggleVisible)

                'lables
                ControlMgr.SetVisibleControl(Me, lblClaimedEquipment, toggleVisible)
                ControlMgr.SetVisibleControl(Me, lblClaimedMake, toggleVisible)
                ControlMgr.SetVisibleControl(Me, lblClaimedModel, toggleVisible)
                ControlMgr.SetVisibleControl(Me, LabelClaimDesc, toggleVisible)
                ControlMgr.SetVisibleControl(Me, lblClaimedSKU, toggleVisible)
                ControlMgr.SetVisibleControl(Me, LabelClaimSerialNumber, toggleVisible)

                'for enrolled equipment
                ControlMgr.SetVisibleControl(Me, LabelYear, False)
                ControlMgr.SetVisibleControl(Me, TextboxYear, False)
                Me.TextboxSKU.ReadOnly = True  'force to be read only

            End If
        End Sub

        Protected Sub PopulateFormfromClaimedEquipmentBO()
            '#REQ 1106
            Try
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    If Not Me.State.objClaimedEquipment Is Nothing Then
                        With Me.State.objClaimedEquipment
                            Me.PopulateControlFromBOProperty(Me.ddlClaimedManuf, .ManufacturerId)
                            Me.PopulateControlFromBOProperty(Me.txtClaimedmake, .Manufacturer)
                            Me.PopulateControlFromBOProperty(Me.txtClaimedModel, .Model)
                            'Me.PopulateControlFromBOProperty(Me.txtClaimedSKu, .SKU)
                            'Reload the claimed sku dropdown
                            Me.ddlClaimedSkuNumber.Items.Clear()
                            If Not .EquipmentId.Equals(Guid.Empty) Then
                                Dim dv As DataView = Me.State.MyBO.LoadSku(.EquipmentId, Me.State._modealer.Id)
                                Me.ddlClaimedSkuNumber.DataSource = dv
                                Me.ddlClaimedSkuNumber.DataTextField = "SKU_NUMBER"
                                Me.ddlClaimedSkuNumber.DataValueField = "SKU_NUMBER"
                                Me.ddlClaimedSkuNumber.DataBind()

                                If Not dv Is Nothing AndAlso dv.FindRows(.SKU.ToString).Length > 0 Then
                                    Me.PopulateControlFromBOProperty(Me.txtClaimedSKu, .SKU)
                                    Me.ddlClaimedSkuNumber.SelectedValue = .SKU.ToString
                                    hdnSelectedClaimedSku.Value = .SKU.ToString
                                End If

                            End If
                            Me.PopulateControlFromBOProperty(Me.txtClaimedDescription, .EquipmentDescription)
                            Me.PopulateControlFromBOProperty(Me.txtClaimSerialNumber, .SerialNumber)
                            Me.PopulateControlFromBOProperty(Me.txtClaimIMEINumber, .IMEINumber)
                        End With
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateClaimedEquipmentBOFromform()
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Me.State.objClaimedEquipment Is Nothing Then Me.State.objClaimedEquipment = New ClaimEquipment()
                Me.PopulateBOProperty(Me.State.objClaimedEquipment, "ManufacturerId", Me.ddlClaimedManuf)
                Me.PopulateBOProperty(Me.State.objClaimedEquipment, "Model", Me.txtClaimedModel)
                Me.PopulateBOProperty(Me.State.objClaimedEquipment, "SerialNumber", Me.txtClaimSerialNumber)
                Me.PopulateBOProperty(Me.State.objClaimedEquipment, "IMEINumber", Me.txtClaimIMEINumber)
                Me.PopulateBOProperty(Me.State.objClaimedEquipment, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, "C"))
                ' Me.State.objClaimedEquipment.SKU = If(Me.IsEdit, hdnSelectedClaimedSku.Value, Me.txtClaimedSKu.Text)
                If (Not Me.State.objClaimedEquipment.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.State.objClaimedEquipment.Model)) Then
                    Me.State.objClaimedEquipment.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(Me.State._modealer.EquipmentListCode, DateTime.Now, Me.State.objClaimedEquipment.ManufacturerId, Me.State.objClaimedEquipment.Model)
                End If
                If Me.State.objClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                    Me.MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")
                    Me.State.objClaimedEquipment.SKU = String.Empty
                    hdnSelectedClaimedSku.Value = String.Empty
                Else
                    Me.State.objClaimedEquipment.SKU = hdnSelectedClaimedSku.Value
                End If

                'If Me.State.objClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then Me.MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")


            End If
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RiskTypeId", Me.LabelRiskTypeId)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerId", Me.LabelMakeId)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SerialNumber", Me.LabelSerialNumberIMEI)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "IMEINumber", Me.LabelIMEINumber)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "Model", Me.LabelModel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerItemDesc", Me.LabelDealerItemDesc)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SkuNumber", Me.labelSKU)
                'Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerItemDesc", Me.LabelDesciption)

                Me.BindBOPropertyToLabel(Me.State.MyBO, "BeginDate", Me.LabelBeginDate)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EndDate", Me.LabelEndDate)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CreatedDate", Me.LabelDateAdded)

                Me.BindBOPropertyToLabel(Me.State.MyBO, "GetCoverageTypeDescription", Me.LabelCoverageType)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "MethodOfRepairId", Me.LabelMethodOfRepair)

                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerDiscountAmt", Me.LabelDeductible)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerDiscountPercent", Me.LabelDeductiblePercent)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "LiabilityLimits", Me.LabelLiabilityLimit)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductCode", Me.LabelProductCode)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceNumber", Me.LabelInvNum)
                '#REQ 1106 start
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "ManufacturerId", Me.lblClaimedMake)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "Manufacturer", Me.lblClaimedMake)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "Model", Me.lblClaimedModel)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "SKU", Me.lblClaimedSKU)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "EquipmentDescription", Me.LabelClaimDesc)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "SerialNumber", Me.LabelClaimSerialNumber)
                    Me.BindBOPropertyToLabel(Me.State.objClaimedEquipment, "IMEINumber", Me.LabelClaimIMEINumber)
                End If
                '#REQ 1106 ned

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateDropdowns()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)

            Try
                'BindListControlToDataView(Me.cboRiskTypeId, LookupListNew.GetRiskTypeLookupList(compGroupId)

                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cboRiskTypeId.Populate(riskTypeLkl, New PopulateOptions() With
                 {
               .AddBlankItem = True
                })
                LoadEnrolledEquipmentDropdowns()

                'BindListControlToDataView(Me.ddlClaimedManuf, LookupListNew.GetManufacturerLookupList(compGroupId))
                Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ddlClaimedManuf.Populate(manufacturerLkl, New PopulateOptions() With
                 {
              .AddBlankItem = True
                })


                cboMethodOfRepair.Populate(CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
                cboCalimAllowed.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                                                                                                                            {
                                                                                                                                                                .AddBlankItem = True
                                                                                                                                                             })
                cboApplyDiscount.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                                                                                                                            {
                                                                                                                                                                .AddBlankItem = True
                                                                                                                                                             })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim riskTypeDesc As String
            Dim manufacturerDesc As String
            Dim methodOfRepairDesc As String
            Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
            Dim MethodOfRepairId As Guid
            Try
                PopulateProtectionAndEventDetail()
                With Me.State.MyBO
                    SetSelectedItem(Me.cboRiskTypeId, .RiskTypeId)
                    riskTypeDesc = GetSelectedDescription(Me.cboRiskTypeId)
                    Me.PopulateControlFromBOProperty(Me.TextboxRiskType, riskTypeDesc)
                    ControlMgr.SetVisibleControl(Me, cboRiskTypeId, False)
                    ControlMgr.SetVisibleControl(Me, TextboxRiskType, True)
                    Me.TextboxRiskType.ReadOnly = True
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                        If (Not String.IsNullOrEmpty(Me.State._modealer.EquipmentListCode) AndAlso Not .ManufacturerId.Equals(Guid.Empty)) Then
                            Try
                                SetSelectedItem(Me.cboManufacturerId, .ManufacturerId)
                            Catch ex As GUIException
                                Me.MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
                            End Try

                        End If
                    Else
                        SetSelectedItem(Me.cboManufacturerId, .ManufacturerId)
                    End If
                    manufacturerDesc = GetSelectedDescription(Me.cboManufacturerId)
                    Me.PopulateControlFromBOProperty(Me.TextboxManufacturer, manufacturerDesc)
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, False)
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, True)
                    Me.TextboxManufacturer.ReadOnly = True

                    If (Me.moCertItemCoverage.MethodOfRepairId = Guid.Empty) Then
                        SetSelectedItem(Me.cboMethodOfRepair, moCertificate.MethodOfRepairId)
                        MethodOfRepairId = moCertificate.MethodOfRepairId
                    Else
                        SetSelectedItem(Me.cboMethodOfRepair, moCertItemCoverage.MethodOfRepairId)
                        MethodOfRepairId = moCertItemCoverage.MethodOfRepairId
                    End If
                    methodOfRepairDesc = GetSelectedDescription(Me.cboMethodOfRepair)
                    Me.PopulateControlFromBOProperty(Me.TextboxMethodOfRepair, methodOfRepairDesc)
                    ControlMgr.SetVisibleControl(Me, cboMethodOfRepair, False)
                    ControlMgr.SetVisibleControl(Me, TextboxMethodOfRepair, True)
                    Me.TextboxMethodOfRepair.ReadOnly = True
                    Me.PopulateControlFromBOProperty(Me.TextboxSerialNumber, .SerialNumber)
                    Me.PopulateControlFromBOProperty(Me.TextboxIMEINumber, .IMEINumber)
                    Me.PopulateControlFromBOProperty(Me.TextboxBeginDate, GetDateFormattedStringNullable(Me.moCertItemCoverage.BeginDate))
                    Me.PopulateControlFromBOProperty(Me.TextboxEndDate, GetDateFormattedStringNullable(Me.moCertItemCoverage.EndDate))
                    Me.PopulateControlFromBOProperty(Me.TextboxLiabilityLimit, Me.moCertItemCoverage.LiabilityLimits, DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.TextboxCoverageType, Me.State.MyBO.GetCoverageTypeDescription(Me.moCertItemCoverage.CoverageTypeId))
                    Me.PopulateControlFromBOProperty(Me.TextboxDateAdded, GetDateFormattedStringNullable(Me.moCertItemCoverage.CreatedDate))
                    Me.PopulateControlFromBOProperty(Me.TextboxDealerItemDesc, Me.State.MyBO.ItemDescription)

                    PopulateFormDeductibleFormBOs(MethodOfRepairId)

                    Me.PopulateControlFromBOProperty(Me.TextboxInvNum, moCertificate.InvoiceNumber)
                    'START  DEF-2930
                    Me.PopulateControlFromBOProperty(Me.TextboxProductCode, .CertProductCode)
                    'Me.PopulateControlFromBOProperty(Me.TextboxProductCode, moCertificate.ProductCode)
                    'END    DEF-2930

                    If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                        Me.PopulateControlFromBOProperty(Me.TextboxModel, .Model)

                    Else
                        If (Not Me.moVSCModel Is Nothing) Then
                            Me.PopulateControlFromBOProperty(Me.TextboxModel, Me.moVSCModel.Model)
                            'Me.PopulateControlFromBOProperty(Me.TextboxDescription, Me.moVSCModel.Description)
                        Else
                            Me.PopulateControlFromBOProperty(Me.TextboxModel, String.Empty)
                            'Me.PopulateControlFromBOProperty(Me.TextboxDescription, String.Empty)
                        End If
                    End If

                    If (Not Me.moVSCClassCode Is Nothing) Then
                        Me.PopulateControlFromBOProperty(Me.TextboxClassCode, Me.moVSCClassCode.Code)
                    Else
                        Me.PopulateControlFromBOProperty(Me.TextboxClassCode, String.Empty)
                    End If

                    Me.PopulateControlFromBOProperty(Me.TextboxYear, moCertificate.VehicleYear)
                    Me.PopulateControlFromBOProperty(Me.TextboxOdometer, moCertificate.Odometer)
                    Me.PopulateControlFromBOProperty(Me.cboApplyDiscount, moCertItemCoverage.IsDiscount)
                    SetSelectedItem(Me.cboCalimAllowed, Me.moCertItemCoverage.IsClaimAllowed)
                    Me.PopulateControlFromBOProperty(Me.TextboxDiscountAmt, Me.moCertItemCoverage.DealerDiscountAmt)
                    Me.PopulateControlFromBOProperty(Me.TextboxDiscountPercent, Me.moCertItemCoverage.DealerDiscountPercent)
                    Me.PopulateControlFromBOProperty(Me.TextboxSKU, Me.State.MyBO.SkuNumber)
                    Me.PopulateControlFromBOProperty(Me.TextboxRepairDiscountPct, Me.moCertItemCoverage.RepairDiscountPct)
                    Me.PopulateControlFromBOProperty(Me.TextboxReplacementDiscountPct, Me.moCertItemCoverage.ReplacementDiscountPct)
                    Me.hdnDealerId.Value = Me.State._modealer.Id.ToString

                    IsEffectiveCoverage()

                    If Me.moCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                        If Me.State._modealer.IsGracePeriodSpecified Then
                            IsEffectiveCoverageForGracePeriod()
                        End If
                    End If

                    If Me.State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                        'when coming from New_Claim button we need to create a new claim equipment
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                            If (Me.State.objClaimedEquipment Is Nothing) Then
                                Dim errlist As New List(Of String)
                                If (Not Me.State.MyBO.CreateClaimedEquipmentFromEnrolledEquipment(Me.State.objClaimedEquipment, errlist)) Then
                                    Me.MasterPage.MessageController.AddError(errlist.ToArray)
                                    ControlMgr.SetEnableControl(Me, Me.ButtonLocateCenter, False)
                                Else
                                    If errlist.Count > 0 Then Me.MasterPage.MessageController.AddWarning(errlist.ToArray)
                                End If
                            End If
                            PopulateFormfromClaimedEquipmentBO()
                            SetCtlsForEquipmentMgmt(True)    'REQ 1106  
                        End If
                    End If
                    If Not moCertItemCoverage.ReinsuranceStatusId.Equals(Guid.Empty) Then
                        ControlMgr.SetVisibleControl(Me, LabelReinsuranceStatus, True)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsuranceStatus, True)
                        Me.PopulateControlFromBOProperty(Me.TextboxReinsuranceStatus, Me.State.MyBO.GetReinsuranceStatusDescription(moCertItemCoverage.ReinsuranceStatusId))
                    Else
                        ControlMgr.SetVisibleControl(Me, LabelReinsuranceStatus, False)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsuranceStatus, False)
                    End If
                    If (Not moCertItemCoverage.ReinsuranceStatusId.Equals(Guid.Empty)) AndAlso moCertItemCoverage.ReinsuranceStatusId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REINSURANCE_STATUSES, LookupListNew.LK_REINS_STATUS_REJECTED)) Then
                        ControlMgr.SetVisibleControl(Me, LabelReinsRejectReason, True)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsRejectReason, True)
                        Me.PopulateControlFromBOProperty(Me.TextboxReinsRejectReason, moCertItemCoverage.ReinsuranceRejectReason)
                    Else
                        ControlMgr.SetVisibleControl(Me, LabelReinsRejectReason, False)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsRejectReason, False)
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateFormDeductibleFormBOs(ByVal methodOfRepairId As Guid)
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(moCertItemCoverage.Id, methodOfRepairId)
            Me.PopulateControlFromBOProperty(Me.TextboxDeductibleBasedOn, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, oDeductible.DeductibleBasedOnId))
            Me.PopulateControlFromBOProperty(Me.TextboxDeductible, oDeductible.DeductibleAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxDeductiblePercent, oDeductible.DeductiblePercentage)
        End Sub

        Protected Sub IsEffectiveCoverage()
            Dim todayDate As Date

            Try
                If todayDate.Today >= Me.moCertItemCoverage.BeginDate.Value AndAlso todayDate.Today <= Me.moCertItemCoverage.EndDate.Value Then
                    'Me.ImagebuttonEffective.Visible = True
                    'Me.ImagebuttonNoEffective.Visible = False
                    Me.State.coverageInEffect = True
                Else
                    'Me.ImagebuttonEffective.Visible = False
                    'Me.ImagebuttonNoEffective.Visible = True
                    Me.State.coverageInEffect = False
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub IsEffectiveCoverageForGracePeriod()
            Dim todayDate As Date
            Dim strDateOfReport As String = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
            Dim objDateOfReport As DateType = Nothing

            If Not strDateOfReport Is Nothing Then

                If Not strDateOfReport Is Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                    objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                End If
                Try
                    If moCertItemCoverage.IsCoverageEffectiveForGracePeriod(objDateOfReport) Then

                        Me.State.coverageInEffectforGracePeriod = True

                    Else
                        Me.State.coverageInEffectforGracePeriod = False

                    End If
                Catch ex As Exception
                    'Me.HandleErrors(ex, Me.ErrorCtrl)
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try
            End If

        End Sub
        Protected Sub PopulateBOsFormFrom()

            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "RiskTypeId", Me.cboRiskTypeId)
                Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Me.cboManufacturerId)
                Me.PopulateBOProperty(Me.State.MyBO, "SerialNumber", Me.TextboxSerialNumber)
                Me.PopulateBOProperty(Me.State.MyBO, "IMEINumber", Me.TextboxIMEINumber)
                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "Model", Me.TextboxModel)
                    'Else
                    '   Me.PopulateBOProperty(Me.State.MyBO, "Model", String.Empty)
                End If
                Me.PopulateBOProperty(Me.moCertItemCoverage, "MethodOfRepairId", Me.cboMethodOfRepair)
                Me.PopulateBOProperty(Me.State.MyBO, "ItemDescription", Me.TextboxDealerItemDesc)
                Me.PopulateBOProperty(Me.moCertificate, "InvoiceNumber", Me.TextboxInvNum)
                Me.PopulateBOProperty(Me.State.MyBO, "SkuNumber", Me.TextboxSKU)
            End With

            '#REQ 1106 start
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, moDealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Me.State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                    PopulateClaimedEquipmentBOFromform()
                End If
            End If
            '#REQ 1106 end

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CreateNew()
            Try
                Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

                Me.State.MyBO = New CertItem
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNewWithCopy()
            Try
                Me.State.MyBO = New CertItem
                Me.PopulateBOsFormFrom()
                Me.EnableDisableFields()

                'create the backup copy
                Me.State.ScreenSnapShotBO = New CertItem
                Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub LocateServiceCenter()
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = Me.State._moCertItemCoverage
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = Me.State.MyBO
                If (Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC Or Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso GetClaims() Then
                    Me.NavController.Navigate(Me, "locate_master_claim", Me.BuildMasterClaimParameters)
                Else
                    Me.NavController.Navigate(Me, "locate_service_center", Me.BuildServiceCenterParameters)
                    'arf 12-20-04 end
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If Not Me.State Is Nothing Then
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            'Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            'Dim myBo As Certificate = Me.State.MyBO
            'Dim lastAction As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
            'Dim lastErrMsg As String = Me.State.LastErrMsg
            ''Clean after consuming the action
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.State.LastErrMsg = ""
            'Me.HiddenSaveChangesPromptResponse.Value = ""
            'If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            '    Select Case lastAction
            '        Case ElitaPlusPage.DetailPageCommand.Back
            '            Me.State.MyBO.Save()
            '            Me.saveCertificate()
            '            Me.State.certificateChanged = True
            '            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, Me.State.certificateChanged)
            '            Me.NavController = Nothing
            '            Me.ReturnToCallingPage(retObj)
            '        Case ElitaPlusPage.DetailPageCommand.BackOnErr
            '            Dim retObj As ReturnType = New ReturnType(lastAction, myBo, Me.State.certificateChanged)
            '            Me.NavController = Nothing
            '            Me.ReturnToCallingPage(retObj)
            '    End Select
            'ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            '    Select Case lastAction
            '        Case ElitaPlusPage.DetailPageCommand.Back
            '            Dim retObj As ReturnType = New ReturnType(lastAction, myBo, Me.State.certificateChanged)
            '            Me.NavController = Nothing
            '            Me.ReturnToCallingPage(retObj)
            '        Case ElitaPlusPage.DetailPageCommand.BackOnErr
            '            Me.ErrorCtrl.AddErrorAndShow(lastErrMsg)
            '    End Select
            'End If



            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Dim actionInProgress As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
            CleanPopupInput()
            Try
                If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Me.State.MyBO.Save()
                    End If
                    Select Case actionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.New_
                            'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO))
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.Redirect_
                            'pm 06-07-06 '
                            Me.LocateServiceCenter()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            Me.NavController.Navigate(Me, "cancel", Me.State.certificateId)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Select Case actionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.New_
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            'Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                'Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadEnrolledEquipmentDropdowns()
            'If Use equipment flag is yes , load enrolled equipment manufacturer from dealers Equipment list else load from company group list
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Not String.IsNullOrEmpty(Me.State._modealer.EquipmentListCode) Then
                    'BindListControlToDataView(Me.cboManufacturerId, LookupListNew.GetManufacturerbyEquipmentList(Me.State._modealer.EquipmentListCode, Date.Now))
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.EquipmentListCode = Me.State._modealer.EquipmentListCode
                    listcontext.EffectiveOnDate = Date.Now
                    Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByEquipmentCode", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
                    {
                      .AddBlankItem = True
                        })
                Else
                    Me.MasterPage.MessageController.AddWarning("EQUIPMENT_LIST_DOES_NOT_EXIST_FOR_DEALER")
                End If
            Else
                ' BindListControlToDataView(Me.cboManufacturerId, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
                Dim listcontext1 As ListContext = New ListContext()
                listcontext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext1)
                cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                    })
            End If
        End Sub

        <WebMethod(), ScriptMethod()> _
        Public Shared Function LoadSku(ByVal manufacturerId As String, ByVal model As String, ByVal dealerId As String) As String

            Dim dealer As New Dealer(New Guid(dealerId))
            Dim equipmentId As Guid = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Now, _
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

#Region "Validation"

        Private Function IsManufacturerMissing() As Boolean
            If Me.State.MyBO.ManufacturerId.Equals(Guid.Empty) Then
                Me.State.manufacturerMissing = True
                Return True
            Else
                Me.State.manufacturerMissing = False
                Return False
            End If
        End Function

        Private Function IsCustomerNameMissing() As Boolean
            If Me.moCertificate.CustomerName Is Nothing Then
                Me.State.customerNameMissing = True
                Return True
            Else
                Me.State.customerNameMissing = False
                Return False
            End If
        End Function

        Private Function IsIdentificationNumberMissing() As Boolean
            If Me.moCertificate.IdentificationNumber Is Nothing Then
                Me.State.IdentificationNumber = True
                Return True
            Else
                Me.State.IdentificationNumber = False
                Return False
            End If
        End Function

        Private Function IsZipMissing() As Boolean
            Dim addr As Address

            addr = Me.moCertificate.AddressChild(False)
            If Not addr Is Nothing Then
                If Me.moCertificate.StatusCode = Active AndAlso _
                    addr.PostalCode = "" Then
                    Me.State.zipMissing = True
                    Return True
                Else
                    Me.State.zipMissing = False
                    Return False
                End If
            Else
                Me.State.zipMissing = True
                Return True
            End If


        End Function


        Private Function GetClaims() As Boolean
            Dim claimsDV As DataView

            claimsDV = Me.moCertItemCoverage.GetAllClaims(Me.State.certificateCoverageId)

            If claimsDV.Count > 0 Then
                'Me.State.activeOrPendingClaim = True
                Return True
            Else
                'Me.State.activeOrPendingClaim = False
                Return False
            End If
        End Function

        Private Function GetCertClaims() As Boolean
            Dim claimsDV As DataView

            Dim ClaimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            claimsDV = ClaimBO.GetCertClaims(Me.State.certificateId)

            If claimsDV.Count > 0 Then
                'Me.State.activeOrPendingClaim = True
                Return True
            Else
                'Me.State.activeOrPendingClaim = False
                Return False
            End If
        End Function

        Private Function IsInvoiceNumberMissing() As Boolean
            If Me.moCertificate.InvoiceNumber Is Nothing Then
                Me.State.invoiceNumberMissing = True
                Return True
            Else
                Me.State.invoiceNumberMissing = False
                Return False
            End If
        End Function

        Private Function IsProductSalesDateMissing() As Boolean
            If Me.moCertificate.ProductSalesDate Is Nothing Then
                Me.State.productSalesDateMissing = True
                Return True
            Else
                Me.State.productSalesDateMissing = False
                Return False
            End If
        End Function

        Private Function IsDepreciationScheduleNotDefined(ByVal ContractID As Guid) As Boolean

            'Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            Dim al As ArrayList = Claim.CalculateLiabilityLimit(Me.State.certificateId, ContractID, Me.moCertItemCoverage.Id)
            If CType(al(1), Integer) <> 0 Then
                Me.State.depreciationScheduleNotDefined = True
            Else
                Me.State.depreciationScheduleNotDefined = False
            End If

            Return Me.State.depreciationScheduleNotDefined
        End Function

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                moCertificate = Me.State.MyBO.GetCertificate(Me.State.certificateId)
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsFamilyDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    'arf 12-20-04  'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_COMMENTADDED) = Nothing                    
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                    If Me.NavController.PrevNavState.Name = "LOCATE_ELIGIBLE_COVERAGES" Then
                        Me.NavController.Navigate(Me, "back_to_locate_eligible_coverage", retObj)
                    Else
                        Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                moCertificate = Me.State.MyBO.GetCertificate(Me.State.certificateId)
                moCertItemCoverage = New CertItemCoverage(Me.State.certificateCoverageId)
                Me.PopulateBOsFormFrom()
                Dim flag As Boolean = True
                If Not moCertItemCoverage Is Nothing Then
                    Dim errMsg As List(Of String)
                    Dim warningMsg As List(Of String)
                    flag = flag And moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg)
                    Me.MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
                    Me.MasterPage.MessageController.AddError(errMsg.ToArray, True)
                End If

                If Me.State.MyBO.IsEquipmentRequired AndAlso Me.State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                    Dim msgList As New List(Of String)
                    If Not Me.State.objClaimedEquipment.ValidateForClaimProcess(msgList) Then
                        Me.MasterPage.MessageController.AddError(msgList.ToArray, True)
                        Return
                    End If
                End If

                If Me.State.MyBO.IsFamilyDirty OrElse moCertItemCoverage.IsFamilyDirty Then
                    If (moCertItemCoverage.IsFamilyDirty) Then
                        moCertItemCoverage.Save()
                    End If
                    If (Me.State.MyBO.IsFamilyDirty) Then
                        Me.State.MyBO.Save()
                    End If
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    If Not Me.State.MyBO.IsEquipmentRequired Then
                        Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED, True)
                    End If
                End If

                Me.PopulateFormFromBOs()
                Me.IsEdit = False
                Me.State.boChanged = True
                Me.EnableDisableFields()
                ControlMgr.SetEnableControl(Me, Me.ButtonLocateCenter, flag)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New CertItem(Me.State.MyBO.Id)
                    moCertItemCoverage = New CertItemCoverage(Me.State.certificateCoverageId)
                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New CertItem
                End If
                Me.IsEdit = False
                Me.PopulateFormFromBOs()
                EnableDisableControls(Me.EditPanel_WRITE, True)
                EnableDisableControls(Me.pnlVehicleInfo, True)
                EnableDisableControls(Me.pnlDeviceInfo, True)
                Me.EnableDisableFields()
                If Not moCertItemCoverage Is Nothing Then
                    Dim errMsg As List(Of String)
                    Dim warningMsg As List(Of String)
                    If Not moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg) Then
                        Me.MasterPage.MessageController.AddError(errMsg.ToArray, True)
                        Exit Sub ' no need to anything because we have error on coverage
                    Else
                        Me.MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
                    End If
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                Me.IsEdit = True
                Me.EnableDisableFields()
                ButtonSoftQuestions.Enabled = False
                ButtonSoftQuestions.Visible = False
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Function BuildServiceCenterParameters() As LocateServiceCenterForm.Parameters
            Dim cert As Certificate = Me.moCertificate
            Dim coverageType As String = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), Me.moCertItemCoverage.CoverageTypeId)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
            Dim oContract As Contract = Contract.GetContract(Me.moCertificate.DealerId, Me.moCertificate.WarrantySalesDate.Value)
            Dim ComingFromDenyClaim As Boolean = False
            Dim todayDate As Date
            'Req-1016 - Start
            Dim emptyGuid As Guid = Guid.Empty
            Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
            'Req-1016 - end

            Dim showAcceptButton As Boolean = True
            Dim claimsDV As DataView = Me.moCertItemCoverage.GetClaims(Me.State.certificateCoverageId)
            Dim claimsManager, officeManager, IHQSup As Boolean
            'DEF-2035
            Dim callCenterAgent, callCenterSupervisor, claimsRole, claimsAnalyst, claimSupport, commentsRole, csrRole, csr2Role, countySuperUser As Boolean
            Dim otherAllowedRoles As Boolean = False
            'End of DEF-2035
            claimsManager = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsManager)
            officeManager = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.OfficeManager)
            IHQSup = ElitaPlusPrincipal.Current.IsInRole(IHQSUPPORT)
            'DEF-2035
            callCenterAgent = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CallCenterAgent)
            callCenterSupervisor = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CallCenterSupervisor)
            claimsRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.Claims)
            claimsAnalyst = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsAnalyst)
            claimSupport = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimSupport)
            commentsRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.Comments)
            csrRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CSR)
            csr2Role = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CSR2)
            countySuperUser = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CountySuperUser)
            otherAllowedRoles = callCenterAgent OrElse callCenterSupervisor OrElse claimsRole OrElse claimsAnalyst OrElse claimSupport OrElse commentsRole OrElse csrRole OrElse csr2Role OrElse countySuperUser
            'End of DEF-2035
            If coverageType = Manufacture Then 'OrElse Not Me.State.coverageInEffect Then
                showAcceptButton = False
                'DEF-2035
            ElseIf claimsManager OrElse officeManager OrElse IHQSup OrElse otherAllowedRoles Then
                showAcceptButton = True
                'End of DEF-2035
                'Req-1016 - Start
                'ElseIf Not oContract Is Nothing AndAlso _
                '    oContract.MonthlyBillingId.Equals(yesId) Then
            ElseIf Not oContract Is Nothing AndAlso
                    ((Not oContract.RecurringPremiumId.Equals(emptyGuid)) And (Not oContract.RecurringPremiumId.Equals(singlePremiumId))) Then
                'Req-1016 - end
                showAcceptButton = True
            ElseIf Not Me.State.coverageInEffect Then
                showAcceptButton = False
            End If

            'if coverage is expired and claim allowed is yes then show accept button
            If Not coverageType = Manufacture Then
                Try
                    If oContract Is Nothing Then
                        If Not Me.State.noContractFoundErrorAdded Then
                            Me.State.noContractFoundErrorAdded = True
                            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, GetType(Contract), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(Contract).FullName, Nothing)
                        End If
                    ElseIf Not (IsManufacturerMissing()) AndAlso Not (IsZipMissing()) AndAlso Not (IsDepreciationScheduleNotDefined(oContract.Id)) Then
                        If todayDate.Today > Me.moCertItemCoverage.BeginDate.Value AndAlso todayDate.Today > Me.moCertItemCoverage.EndDate.Value Then
                            If (oContract.BackEndClaimsAllowedId.Equals(yesId)) Then
                                showAcceptButton = True
                            Else
                                'DEF-2035
                                If ((claimsManager) OrElse (officeManager) OrElse (IHQSup) OrElse (otherAllowedRoles)) Then
                                    showAcceptButton = True
                                    'End of DEF-2035
                                Else
                                    showAcceptButton = False
                                End If
                            End If
                        End If
                    End If

                    If btnDenyClaim.Enabled = True Then  ' if deny claim is active, then show accept button
                        showAcceptButton = True
                        ComingFromDenyClaim = True
                    End If
                Catch ex As Exception
                    'Me.HandleErrors(ex, Me.ErrorCtrl)
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try

            End If

            Return New LocateServiceCenterForm.Parameters(cert.DealerId, cert.AddressChild.ZipLocator, Me.State.MyBO.RiskTypeId, Me.State.MyBO.ManufacturerId, _
                                                          coverageType, Me.moCertItemCoverage.Id, Guid.Empty, showAcceptButton, , ComingFromDenyClaim, , Me.State.objClaimedEquipment)

        End Function

        Function BuildMasterClaimParameters() As LocateMasterClaimListForm.Parameters
            Dim cert As Certificate = Me.moCertificate
            Dim coverageType As String = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), Me.moCertItemCoverage.CoverageTypeId)
            Dim showAcceptButton As Boolean = True

            If coverageType = "M" OrElse Not Me.State.coverageInEffect Then
                Dim claimsManager, officeManager, IHQSup As Boolean
                'DEF-2035
                Dim callCenterAgent, callCenterSupervisor, claimsRole, claimsAnalyst, claimSupport, commentsRole, csrRole, csr2Role, countySuperUser As Boolean
                Dim otherAllowedRoles As Boolean = False
                'End of DEF-2035
                claimsManager = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsManager)
                officeManager = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.OfficeManager)
                IHQSup = ElitaPlusPrincipal.Current.IsInRole(IHQSUPPORT)
                'DEF-2035
                callCenterAgent = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CallCenterAgent)
                callCenterSupervisor = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CallCenterSupervisor)
                claimsRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.Claims)
                claimsAnalyst = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsAnalyst)
                claimSupport = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimSupport)
                commentsRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.Comments)
                csrRole = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CSR)
                csr2Role = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CSR2)
                countySuperUser = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CountySuperUser)
                otherAllowedRoles = callCenterAgent OrElse callCenterSupervisor OrElse claimsRole OrElse claimsAnalyst OrElse claimSupport OrElse commentsRole OrElse csrRole OrElse csr2Role OrElse countySuperUser

                If ((claimsManager) OrElse (officeManager) OrElse (IHQSup) OrElse (otherAllowedRoles)) Then
                    showAcceptButton = True
                    'End of DEF-2035
                Else
                    showAcceptButton = False
                End If
            End If

            Return New LocateMasterClaimListForm.Parameters(cert.DealerId, cert.AddressChild.ZipLocator, Me.State.MyBO.RiskTypeId, Me.State.MyBO.ManufacturerId, coverageType, Me.moCertItemCoverage.Id, showAcceptButton, , Me.State.objClaimedEquipment, cert.getMasterclaimProcFlag, Me.State.selDateOfLoss)


        End Function


        Private Sub ButtonLocateCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLocateCenter.Click

            Try
                Dim msg As String
                If Not Me.moCertItemCoverage.IsPossibleWarrantyClaim(msg) Then
                    'arf 12-20-04 'Me.callPage(LocateServiceCenterForm.URL, BuildServiceCenterParameters())
                    'arf 12-20-04 begin
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = Me.State._moCertItemCoverage
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = Me.State.MyBO


                    Dim oContract As New Contract
                    Dim objCert As New Certificate(Me.State.MyBO.CertId)
                    oContract = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
                    If Not oContract Is Nothing Then
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then
                            Me.State.allowdifferentcoverage = True
                        End If
                    End If

                    If (((Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC Or Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso GetClaims()) Or _
                         (Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC Or Me.moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso Me.State.allowdifferentcoverage _
                             AndAlso GetCertClaims()) Then
                        Me.NavController.Navigate(Me, "locate_master_claim", Me.BuildMasterClaimParameters)
                    Else
                        Me.NavController.Navigate(Me, "locate_service_center", Me.BuildServiceCenterParameters)
                        'arf 12-20-04 end
                    End If
                Else
                    Me.DisplayMessage(msg, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ButtonSoftQuestions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSoftQuestions.Click
            Try
                'Me.callPage(SoftQuestionsList.URL, New SoftQuestionsList.Parameters(Me.State.MyBO.RiskTypeId, Me.State.certificateId))

                Dim frameSoftQuestions As HtmlIframe = CType(pnlPopup.FindControl("frameSoftQuestions"), HtmlIframe)
                Dim sFrameSource As String = "../Tables/SoftQuestionsList.aspx?RiskTypeID=" & Me.State.MyBO.RiskTypeId.ToString() & "&CertificateID=" & Me.State.certificateId.ToString() & "&CertificateCompanyID=" & Me.State.certificateCompanyId.ToString() & "&IsComingFromClaimWizard=false"
                frameSoftQuestions.Attributes.Add("src", sFrameSource)
                frameSoftQuestions.Attributes.Add("style", "width: 880px; height: 550px;")

                'Me.NavController.Navigate(Me, FlowEvents.EVENT_SOFT_QUESTIONS, New SoftQuestionsList.Parameters(Me.State.MyBO.RiskTypeId, Me.State.certificateId, Me.State.certificateCompanyId))
                'Dim currentState As MyState = CType(Me.NavController.State, MyState)

                mdlPopup.Show()
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnDenyClaim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenyClaim.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = Me.State._moCertItemCoverage
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = Me.State.MyBO
                Me.NavController.Navigate(Me, "locate_service_center", Me.BuildServiceCenterParameters)
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                Me.NavController.Navigate(Me, "back")
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            'If Me.IsEdit Then
            '    Me.IsEdit = False
            'End If
            'Me.PopulateBOsFormFrom()
            'Me.DisplayMessage(Message.MSG_PROMPT_ARE_YOU_SURE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back

            'Try
            '    moCertificate = Me.State.MyBO.GetCertificate(Me.State.certificateId)
            '    Me.PopulateBOsFormFrom()
            '    If Me.State.MyBO.IsFamilyDirty Then
            '        Me.DisplayMessage(Message.MSG_PROMPT_ARE_YOU_SURE, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            '        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Cancel
            '    Else
            '        Me.NavController.Navigate(Me, "cancel")
            '        'Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
            '        'If Me.NavController.PrevNavState.Name = "LOCATE_ELIGIBLE_COVERAGES" Then
            '        '    Me.NavController.Navigate(Me, "back_to_locate_eligible_coverage", retObj)
            '        'Else
            '        '    Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
            '        'End If
            '    End If
            'Catch ex As Threading.ThreadAbortException
            'Catch ex As Exception
            '    'Me.HandleErrors(ex, Me.ErrorCtrl)
            '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
            '    Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            '    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            '    Me.State.LastErrMsg = Me.ErrorCtrl.Text
            'End Try



        End Sub

#End Region

#Region "Page Control Events"
        Private Sub cboMethodOfRepair_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMethodOfRepair.SelectedIndexChanged
            Try
                PopulateFormDeductibleFormBOs(GetSelectedItem(cboMethodOfRepair))
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Error Handling"

#End Region


    End Class
End Namespace

