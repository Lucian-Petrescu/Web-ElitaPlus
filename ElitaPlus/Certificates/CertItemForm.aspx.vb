'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/9/2004)  ********************

Imports System.Collections.Generic
Imports System.Diagnostics
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

        Protected WithEvents LabelCoverageTypeId As Label
        Protected WithEvents cboCoverageTypeId As DropDownList
        Protected WithEvents ManufacturerMissinsLabel As Label
        Protected WithEvents ButtonPolicyInvoiceInfo As Button
        Protected WithEvents moWarrantySoldOnLabel As Label
        Protected WithEvents moDealerNameLabel As Label
        Protected WithEvents TableVSC As HtmlTable
        Protected WithEvents TDYear As HtmlTableCell


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
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
            Public Sub New(dealerId As Guid, zipLocator As String, _
                            riskTypeId As Guid, ManufacturerId As Guid, _
                            covTypeCode As String, _
                            certItemCoverageId As Guid, _
                            ClaimId As Guid, _
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
            Public Sub New(callingObject As String)
                sCallingObject = callingObject
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
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CertItem, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.BoChanged = boChanged
            End Sub
        End Class
#End Region

#Region "Properties"



        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State.certificateCompanyId)

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
            State.inputParameters = CType(NavController.ParametersPassed, Parameters)
        End Sub

        Public Property moCertItemCoverage() As CertItemCoverage
            Get
                Return State._moCertItemCoverage
            End Get
            Set(Value As CertItemCoverage)
                State._moCertItemCoverage = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value
            End Set
        End Property

        Public Property moContract() As Contract
            Get
                Return State._moContract
            End Get
            Set(Value As Contract)
                State._moContract = Value
            End Set
        End Property

        Public Property moVSCModel() As VSCModel
            Get
                Return State._moVSCModel
            End Get
            Set(Value As VSCModel)
                State._moVSCModel = Value
            End Set
        End Property

        Public Property moVSCClassCode() As VSCClassCode
            Get
                Return State._moVSCClassCode
            End Get
            Set(Value As VSCClassCode)
                State._moVSCClassCode = Value
            End Set
        End Property

        Public Property IsEdit() As Boolean
            Get
                Return State._IsEdit
            End Get
            Set(Value As Boolean)
                State._IsEdit = Value
            End Set
        End Property

        Public Property moDealer() As Dealer
            Get
                Return State._modealer
            End Get
            Set(Value As Dealer)
                State._modealer = Value
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
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    moCertItemCoverage = New CertItemCoverage(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID), Guid))
                    Me.State.certificateCoverageId = moCertItemCoverage.Id

                    Me.State.MyBO = New CertItem(moCertItemCoverage.CertItemId)
                    Me.State.certificateItemId = State.MyBO.Id

                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                    Me.State.selDateOfLoss = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), Date)
                    moDealer = New Dealer(moCertificate.DealerId)
                    If Not moCertificate.ModelId.Equals(Guid.Empty) Then
                        moVSCModel = New VSCModel(moCertificate.ModelId)
                    End If
                    If Not moCertificate.ClassCodeId.Equals(Guid.Empty) Then
                        moVSCClassCode = New VSCClassCode(moCertificate.ClassCodeId)
                    End If
                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)
                'arf 12-20-04 end
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try

                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    moCertItemCoverage = New CertItemCoverage(CType(CallingParameters, Guid))
                    State.certificateCoverageId = moCertItemCoverage.Id

                    State.MyBO = New CertItem(moCertItemCoverage.CertItemId)
                    State.certificateItemId = State.MyBO.Id

                    moCertificate = State.MyBO.GetCertificate(State.MyBO.CertId)
                    State.certificateId = moCertificate.Id
                    State.boChanged = False

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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Select Case CalledUrl
                    Case LocateServiceCenterForm.URL
                End Select
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Page Events"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State.MyBO IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & State.MyBO.Cert.CertNumber & ElitaBase.Sperator & _
                        "Coverage"
                End If
            End If
        End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If
            ClearGridHeadersAndLabelsErrSign()
            MasterPage.MessageController.Clear()

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
            UpdateBreadCrum()
            headerDeviceInfo.InnerText = TranslationBase.TranslateLabelOrMessage("DEVICE_INFORMATION")
            ddlClaimedManuf.Attributes.Add("onchange", "LoadSKU();")
            txtClaimedModel.Attributes.Add("onchange", "LoadSKU();")
            ddlClaimedSkuNumber.Attributes.Add("onchange", String.Format("FillHiddenField('{0}','{1}');", ddlClaimedSkuNumber.ClientID, hdnSelectedClaimedSku.ClientID))
            'CheckIfComingFromSaveConfirm()
            Try

                If (Not IsEdit) Then
                    EnableDisableControls(EditPanel_WRITE, True)
                    EnableDisableControls(pnlVehicleInfo, True)
                    EnableDisableControls(pnlDeviceInfo, True)
                End If

                'DEF-2560
                'Set SRC iFrame to NULL. This 'll "SoftQuestion.aspx" prevent to load on every request.
                frameSoftQuestions.Attributes.Add("src", "")
                'DEF-2560

                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CertItem
                    End If
                    Trace(Me, GuidControl.GuidToHexString(State.MyBO.Id))
                    State.companyCode = GetCompanyCode

                    PopulateDropdowns()
                    ControlMgr.SetEnableControl(Me, ButtonLocateCenter, Not IsEdit)
                    PopulateFormFromBOs()
                    EnableDisableFields()

                    lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                    btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnCancel.UniqueID))

                    If moCertItemCoverage IsNot Nothing Then
                        Dim errMsg As List(Of String)
                        Dim warningMsg As List(Of String)
                        Dim strDateOfReport As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
                        Dim objDateOfReport As DateType = Nothing
                        If strDateOfReport IsNot Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                            objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                        End If
                        If Not moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg, objDateOfReport) Then
                            MasterPage.MessageController.AddError(errMsg.ToArray, True)
                            ControlMgr.SetEnableControl(Me, ButtonLocateCenter, False)
                            Exit Sub ' no need to anything because we have error on coverage
                        Else
                            If warningMsg.Count > 0 Then MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
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
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                    If State.objClaimedEquipment IsNot Nothing Then AddLabelDecorations(State.objClaimedEquipment)
                End If

                If State.inputParameters IsNot Nothing Then
                    If State.inputParameters.sCallingObject = CALLED_FROM_GRID_LINK Then
                        'START DEF-2531
                        ControlMgr.SetEnableControl(Me, ButtonLocateCenter, False)
                        ControlMgr.SetVisibleControl(Me, ButtonLocateCenter, False)
                        ControlMgr.SetVisibleControl(Me, btnCancel, False)
                        'END    DEF-2531
                        MasterPage.PageTitle = ""
                        moProtectionAndEventDetails.Visible = False
                        WizardControl.Visible = False

                    End If
                End If

            Catch ex As ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub PopulateProtectionAndEventDetail()
            Dim cssClassName As String
            Dim dateOfLoss As Date
            Dim certItemCvg As CertItemCoverage
            Dim certItem As CertItem
            Try
                certItemCvg = New CertItemCoverage(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID), Guid))
                certItem = New CertItem(certItemCvg.CertItemId)
                Dim cert As New Certificate(certItemCvg.CertId)
                dateOfLoss = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), Date)
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
                moProtectionAndEventDetails.CallerName = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME), String)

                If dateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(dateOfLoss)
                moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromId("SUBSTAT", cert.SubscriberStatus)
                If (LookupListNew.GetCodeFromId("SUBSTAT", cert.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If
                moProtectionAndEventDetails.ProtectionStatusCss = cssClassName
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    If State.objClaimedEquipment IsNot Nothing Then
                        moProtectionAndEventDetails.ClaimedModel = State.objClaimedEquipment.Model
                        moProtectionAndEventDetails.ClaimedMake = State.objClaimedEquipment.Manufacturer
                    Else
                        moProtectionAndEventDetails.ClaimedModel = NO_DATA
                        moProtectionAndEventDetails.ClaimedMake = NO_DATA
                    End If
                End If
                moProtectionAndEventDetails.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, certItem.RiskTypeId)
                moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(dateOfLoss)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                btnBack.Enabled = True

                ControlMgr.SetEnableControl(Me, ButtonSoftQuestions, Not IsEdit)
                ControlMgr.SetEnableControl(Me, btnEdit_WRITE, Not IsEdit)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, IsEdit)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, IsEdit)
                ControlMgr.SetEnableControl(Me, btnBack, Not IsEdit)

                'Me.MenuEnabled = not Me.IsEdit
                ControlMgr.SetVisibleControl(Me, cboRiskTypeId, IsEdit)
                'Me.cboRiskTypeId.Enabled = Me.IsEdit
                SetEnabledForControlFamily(LabelRiskTypeId, IsEdit, True)
                SetEnabledForControlFamily(cboRiskTypeId, IsEdit, True)
                ControlMgr.SetVisibleControl(Me, TextboxRiskType, Not IsEdit)
                SetEnabledForControlFamily(TextboxRiskType, IsEdit, True)
                cboManufacturerId.Enabled = True


                ControlMgr.SetVisibleControl(Me, cboMethodOfRepair, IsEdit)
                cboMethodOfRepair.Enabled = True
                ControlMgr.SetVisibleControl(Me, TextboxMethodOfRepair, Not IsEdit)

                TextboxInvNum.ReadOnly = Not IsEdit
                ControlMgr.SetVisibleControl(Me, lblEnrolledDeviceInfo, True)

                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    ControlMgr.SetEnableControl(Me, ddlClaimedManuf, IsEdit)
                    ControlMgr.SetVisibleControl(Me, ddlClaimedManuf, IsEdit)
                    ControlMgr.SetVisibleControl(Me, txtClaimedmake, IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimedModel, IsEdit)
                    ControlMgr.SetVisibleControl(Me, txtClaimedSKu, IsEdit)
                    ControlMgr.SetVisibleControl(Me, ddlClaimedSkuNumber, IsEdit)
                    ControlMgr.SetEnableControl(Me, ddlClaimedSkuNumber, IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimedDescription, IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimSerialNumber, IsEdit)
                    ControlMgr.SetEnableControl(Me, txtClaimIMEINumber, IsEdit)
                    txtClaimedModel.ReadOnly = Not IsEdit
                    txtClaimedSKu.ReadOnly = Not IsEdit
                    txtClaimSerialNumber.ReadOnly = Not IsEdit
                    txtClaimIMEINumber.ReadOnly = Not IsEdit
                Else 'allow the entrolled equipment data to be editable for dealer not using equipment management
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, IsEdit)
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, Not IsEdit)
                    TextboxSerialNumber.ReadOnly = Not IsEdit
                    TextboxIMEINumber.ReadOnly = Not IsEdit
                    TextboxModel.ReadOnly = Not IsEdit
                    'ControlMgr.SetVisibleControl(Me, TextboxDealerItemDesc, Me.IsEdit)
                    TextboxDealerItemDesc.ReadOnly = Not IsEdit
                End If

                'TODO Revisit after speaking with Tito
                'Me.ButtonSoftQuestions.Enabled = False



                TextboxEndDate.Font.Bold = IsEdit
                TextboxBeginDate.Font.Bold = IsEdit

                If moCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso moDealer.IsGracePeriodSpecified Then
                    If Not State.coverageInEffectforGracePeriod Then
                        MasterPage.MessageController.AddWarning("COVERAGE IS NOT IN EFFECT", True)
                        TextboxBeginDate.ForeColor = Color.Red
                        TextboxEndDate.ForeColor = Color.Red
                    Else
                        TextboxBeginDate.ForeColor = Color.Green
                        TextboxEndDate.ForeColor = Color.Green
                    End If
                Else
                    If Not State.coverageInEffect Then
                        MasterPage.MessageController.AddWarning("COVERAGE IS NOT IN EFFECT", True)
                        TextboxBeginDate.ForeColor = Color.Red
                        TextboxEndDate.ForeColor = Color.Red
                    Else
                        TextboxBeginDate.ForeColor = Color.Green
                        TextboxEndDate.ForeColor = Color.Green
                    End If

                End If

                TextboxClassCode.ReadOnly = IsEdit
                TextboxDiscountAmt.ReadOnly = IsEdit
                TextboxDiscountPercent.ReadOnly = IsEdit
                TextboxOdometer.ReadOnly = IsEdit
                cboCalimAllowed.Enabled = Not IsEdit
                cboApplyDiscount.Enabled = Not IsEdit
                'Me.TextboxModel.ReadOnly = Me.IsEdit
                TextboxYear.ReadOnly = IsEdit
                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                    pnlVehicleInfo.Visible = False
                    'TRVSC.Visible = False
                    'TRVSC1.Visible = False
                    'TRVSC2.Visible = False
                    'TRVSC3.Visible = False
                    ''Me.TextboxModel.ReadOnly = False
                    LabelYear.Visible = False
                    TextboxYear.Visible = False
                    If State.objClaimedEquipment IsNot Nothing Then
                        If State.objClaimedEquipment.IMEINumber Is Nothing Then
                            LabelClaimSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                            ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                            ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                        Else
                            LabelClaimSerialNumber.Text = TranslationBase.TranslateLabelOrMessage("Serial_No_label") + ":"
                            ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, True)
                            ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, True)
                        End If
                    Else
                        LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                        ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                        ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                    End If
                Else
                    LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("VIN") + ":"
                    If State.objClaimedEquipment Is Nothing Then
                        ControlMgr.SetVisibleControl(Me, LabelClaimIMEINumber, False)
                        ControlMgr.SetVisibleControl(Me, txtClaimIMEINumber, False)
                    End If
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, False)
                    cboManufacturerId.Enabled = False
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, True)
                    TextboxModel.ReadOnly = True
                    TextboxSKU.ReadOnly = Not IsEdit
                End If

                If State._modealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                    LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_Number") + ":"
                    ControlMgr.SetVisibleControl(Me, LabelIMEINumber, False)
                    ControlMgr.SetVisibleControl(Me, TextboxIMEINumber, False)
                Else
                    LabelSerialNumberIMEI.Text = TranslationBase.TranslateLabelOrMessage("Serial_No_label") + ":"
                    ControlMgr.SetVisibleControl(Me, LabelIMEINumber, True)
                    ControlMgr.SetVisibleControl(Me, TextboxIMEINumber, True)
                End If

                ControlMgr.SetEnableControl(Me, btnDenyClaim, False)
                Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                If (Not moCertItemCoverage.IsClaimAllowed.Equals(NoId)) Then 'Or Me.moCertItemCoverage.IsClaimAllowed.Equals(Guid.Empty) Then
                    Dim todayDate As Date
                    If todayDate.Today < moCertItemCoverage.BeginDate.Value AndAlso moCertificate.StatusCode <> CLOSED Then
                        'If Not Me.State.coverageInEffect And Me.moCertificate.StatusCode <> CLOSED Then
                        ControlMgr.SetEnableControl(Me, btnDenyClaim, True)
                        'Else
                        '    ControlMgr.SetEnableControl(Me, btnDenyClaim, False)
                    End If
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub SetCtlsForEquipmentMgmt(toggleVisible As Boolean)
            '#REQ 1106 Start
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
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
                TextboxSKU.ReadOnly = True  'force to be read only

            End If
        End Sub

        Protected Sub PopulateFormfromClaimedEquipmentBO()
            '#REQ 1106
            Try
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    If State.objClaimedEquipment IsNot Nothing Then
                        With State.objClaimedEquipment
                            PopulateControlFromBOProperty(ddlClaimedManuf, .ManufacturerId)
                            PopulateControlFromBOProperty(txtClaimedmake, .Manufacturer)
                            PopulateControlFromBOProperty(txtClaimedModel, .Model)
                            'Me.PopulateControlFromBOProperty(Me.txtClaimedSKu, .SKU)
                            'Reload the claimed sku dropdown
                            ddlClaimedSkuNumber.Items.Clear()
                            If Not .EquipmentId.Equals(Guid.Empty) Then
                                Dim dv As DataView = State.MyBO.LoadSku(.EquipmentId, State._modealer.Id)
                                ddlClaimedSkuNumber.DataSource = dv
                                ddlClaimedSkuNumber.DataTextField = "SKU_NUMBER"
                                ddlClaimedSkuNumber.DataValueField = "SKU_NUMBER"
                                ddlClaimedSkuNumber.DataBind()

                                If dv IsNot Nothing AndAlso dv.FindRows(.SKU.ToString).Length > 0 Then
                                    PopulateControlFromBOProperty(txtClaimedSKu, .SKU)
                                    ddlClaimedSkuNumber.SelectedValue = .SKU.ToString
                                    hdnSelectedClaimedSku.Value = .SKU.ToString
                                End If

                            End If
                            PopulateControlFromBOProperty(txtClaimedDescription, .EquipmentDescription)
                            PopulateControlFromBOProperty(txtClaimSerialNumber, .SerialNumber)
                            PopulateControlFromBOProperty(txtClaimIMEINumber, .IMEINumber)
                        End With
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateClaimedEquipmentBOFromform()
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                If State.objClaimedEquipment Is Nothing Then State.objClaimedEquipment = New ClaimEquipment()
                PopulateBOProperty(State.objClaimedEquipment, "ManufacturerId", ddlClaimedManuf)
                PopulateBOProperty(State.objClaimedEquipment, "Model", txtClaimedModel)
                PopulateBOProperty(State.objClaimedEquipment, "SerialNumber", txtClaimSerialNumber)
                PopulateBOProperty(State.objClaimedEquipment, "IMEINumber", txtClaimIMEINumber)
                PopulateBOProperty(State.objClaimedEquipment, "ClaimEquipmentTypeId", LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, "C"))
                ' Me.State.objClaimedEquipment.SKU = If(Me.IsEdit, hdnSelectedClaimedSku.Value, Me.txtClaimedSKu.Text)
                If (Not State.objClaimedEquipment.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(State.objClaimedEquipment.Model)) Then
                    State.objClaimedEquipment.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(State._modealer.EquipmentListCode, DateTime.Now, State.objClaimedEquipment.ManufacturerId, State.objClaimedEquipment.Model)
                End If
                If State.objClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                    MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")
                    State.objClaimedEquipment.SKU = String.Empty
                    hdnSelectedClaimedSku.Value = String.Empty
                Else
                    State.objClaimedEquipment.SKU = hdnSelectedClaimedSku.Value
                End If

                'If Me.State.objClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then Me.MasterPage.MessageController.AddWarning("EQUIPMENT_NOT_CONFIGURED")


            End If
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                BindBOPropertyToLabel(State.MyBO, "RiskTypeId", LabelRiskTypeId)
                BindBOPropertyToLabel(State.MyBO, "ManufacturerId", LabelMakeId)
                BindBOPropertyToLabel(State.MyBO, "SerialNumber", LabelSerialNumberIMEI)
                BindBOPropertyToLabel(State.MyBO, "IMEINumber", LabelIMEINumber)
                BindBOPropertyToLabel(State.MyBO, "Model", LabelModel)
                BindBOPropertyToLabel(State.MyBO, "DealerItemDesc", LabelDealerItemDesc)
                BindBOPropertyToLabel(State.MyBO, "SkuNumber", labelSKU)
                'Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerItemDesc", Me.LabelDesciption)

                BindBOPropertyToLabel(State.MyBO, "BeginDate", LabelBeginDate)
                BindBOPropertyToLabel(State.MyBO, "EndDate", LabelEndDate)
                BindBOPropertyToLabel(State.MyBO, "CreatedDate", LabelDateAdded)

                BindBOPropertyToLabel(State.MyBO, "GetCoverageTypeDescription", LabelCoverageType)
                BindBOPropertyToLabel(State.MyBO, "MethodOfRepairId", LabelMethodOfRepair)

                BindBOPropertyToLabel(State.MyBO, "DealerDiscountAmt", LabelDeductible)
                BindBOPropertyToLabel(State.MyBO, "DealerDiscountPercent", LabelDeductiblePercent)
                BindBOPropertyToLabel(State.MyBO, "LiabilityLimits", LabelLiabilityLimit)
                BindBOPropertyToLabel(State.MyBO, "ProductCode", LabelProductCode)
                BindBOPropertyToLabel(State.MyBO, "InvoiceNumber", LabelInvNum)
                '#REQ 1106 start
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                    BindBOPropertyToLabel(State.objClaimedEquipment, "ManufacturerId", lblClaimedMake)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "Manufacturer", lblClaimedMake)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "Model", lblClaimedModel)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "SKU", lblClaimedSKU)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "EquipmentDescription", LabelClaimDesc)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "SerialNumber", LabelClaimSerialNumber)
                    BindBOPropertyToLabel(State.objClaimedEquipment, "IMEINumber", LabelClaimIMEINumber)
                End If
                '#REQ 1106 ned

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                HandleErrors(ex, MasterPage.MessageController)
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
                With State.MyBO
                    SetSelectedItem(cboRiskTypeId, .RiskTypeId)
                    riskTypeDesc = GetSelectedDescription(cboRiskTypeId)
                    PopulateControlFromBOProperty(TextboxRiskType, riskTypeDesc)
                    ControlMgr.SetVisibleControl(Me, cboRiskTypeId, False)
                    ControlMgr.SetVisibleControl(Me, TextboxRiskType, True)
                    TextboxRiskType.ReadOnly = True
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                        If (Not String.IsNullOrEmpty(State._modealer.EquipmentListCode) AndAlso Not .ManufacturerId.Equals(Guid.Empty)) Then
                            Try
                                SetSelectedItem(cboManufacturerId, .ManufacturerId)
                            Catch ex As GUIException
                                MasterPage.MessageController.AddError("MANUFACTURER_ON_CERT_ITEM_NOT_IN_EQUIPMENT_LIST")
                            End Try

                        End If
                    Else
                        SetSelectedItem(cboManufacturerId, .ManufacturerId)
                    End If
                    manufacturerDesc = GetSelectedDescription(cboManufacturerId)
                    PopulateControlFromBOProperty(TextboxManufacturer, manufacturerDesc)
                    ControlMgr.SetVisibleControl(Me, cboManufacturerId, False)
                    ControlMgr.SetVisibleControl(Me, TextboxManufacturer, True)
                    TextboxManufacturer.ReadOnly = True

                    If (moCertItemCoverage.MethodOfRepairId = Guid.Empty) Then
                        SetSelectedItem(cboMethodOfRepair, moCertificate.MethodOfRepairId)
                        MethodOfRepairId = moCertificate.MethodOfRepairId
                    Else
                        SetSelectedItem(cboMethodOfRepair, moCertItemCoverage.MethodOfRepairId)
                        MethodOfRepairId = moCertItemCoverage.MethodOfRepairId
                    End If
                    methodOfRepairDesc = GetSelectedDescription(cboMethodOfRepair)
                    PopulateControlFromBOProperty(TextboxMethodOfRepair, methodOfRepairDesc)
                    ControlMgr.SetVisibleControl(Me, cboMethodOfRepair, False)
                    ControlMgr.SetVisibleControl(Me, TextboxMethodOfRepair, True)
                    TextboxMethodOfRepair.ReadOnly = True
                    PopulateControlFromBOProperty(TextboxSerialNumber, .SerialNumber)
                    PopulateControlFromBOProperty(TextboxIMEINumber, .IMEINumber)
                    PopulateControlFromBOProperty(TextboxBeginDate, GetDateFormattedStringNullable(moCertItemCoverage.BeginDate))
                    PopulateControlFromBOProperty(TextboxEndDate, GetDateFormattedStringNullable(moCertItemCoverage.EndDate))
                    PopulateControlFromBOProperty(TextboxLiabilityLimit, moCertItemCoverage.LiabilityLimits, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(TextboxCoverageType, State.MyBO.GetCoverageTypeDescription(moCertItemCoverage.CoverageTypeId))
                    PopulateControlFromBOProperty(TextboxDateAdded, GetDateFormattedStringNullable(moCertItemCoverage.CreatedDate))
                    PopulateControlFromBOProperty(TextboxDealerItemDesc, State.MyBO.ItemDescription)

                    PopulateFormDeductibleFormBOs(MethodOfRepairId)

                    PopulateControlFromBOProperty(TextboxInvNum, moCertificate.InvoiceNumber)
                    'START  DEF-2930
                    PopulateControlFromBOProperty(TextboxProductCode, .CertProductCode)
                    'Me.PopulateControlFromBOProperty(Me.TextboxProductCode, moCertificate.ProductCode)
                    'END    DEF-2930

                    If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                        PopulateControlFromBOProperty(TextboxModel, .Model)

                    Else
                        If (moVSCModel IsNot Nothing) Then
                            PopulateControlFromBOProperty(TextboxModel, moVSCModel.Model)
                            'Me.PopulateControlFromBOProperty(Me.TextboxDescription, Me.moVSCModel.Description)
                        Else
                            PopulateControlFromBOProperty(TextboxModel, String.Empty)
                            'Me.PopulateControlFromBOProperty(Me.TextboxDescription, String.Empty)
                        End If
                    End If

                    If (moVSCClassCode IsNot Nothing) Then
                        PopulateControlFromBOProperty(TextboxClassCode, moVSCClassCode.Code)
                    Else
                        PopulateControlFromBOProperty(TextboxClassCode, String.Empty)
                    End If

                    PopulateControlFromBOProperty(TextboxYear, moCertificate.VehicleYear)
                    PopulateControlFromBOProperty(TextboxOdometer, moCertificate.Odometer)
                    PopulateControlFromBOProperty(cboApplyDiscount, moCertItemCoverage.IsDiscount)
                    SetSelectedItem(cboCalimAllowed, moCertItemCoverage.IsClaimAllowed)
                    PopulateControlFromBOProperty(TextboxDiscountAmt, moCertItemCoverage.DealerDiscountAmt)
                    PopulateControlFromBOProperty(TextboxDiscountPercent, moCertItemCoverage.DealerDiscountPercent)
                    PopulateControlFromBOProperty(TextboxSKU, State.MyBO.SkuNumber)
                    PopulateControlFromBOProperty(TextboxRepairDiscountPct, moCertItemCoverage.RepairDiscountPct)
                    PopulateControlFromBOProperty(TextboxReplacementDiscountPct, moCertItemCoverage.ReplacementDiscountPct)
                    hdnDealerId.Value = State._modealer.Id.ToString

                    IsEffectiveCoverage()

                    If moCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                        If State._modealer.IsGracePeriodSpecified Then
                            IsEffectiveCoverageForGracePeriod()
                        End If
                    End If

                    If State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                        'when coming from New_Claim button we need to create a new claim equipment
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                            If (State.objClaimedEquipment Is Nothing) Then
                                Dim errlist As New List(Of String)
                                If (Not State.MyBO.CreateClaimedEquipmentFromEnrolledEquipment(State.objClaimedEquipment, errlist)) Then
                                    MasterPage.MessageController.AddError(errlist.ToArray)
                                    ControlMgr.SetEnableControl(Me, ButtonLocateCenter, False)
                                Else
                                    If errlist.Count > 0 Then MasterPage.MessageController.AddWarning(errlist.ToArray)
                                End If
                            End If
                            PopulateFormfromClaimedEquipmentBO()
                            SetCtlsForEquipmentMgmt(True)    'REQ 1106  
                        End If
                    End If
                    If Not moCertItemCoverage.ReinsuranceStatusId.Equals(Guid.Empty) Then
                        ControlMgr.SetVisibleControl(Me, LabelReinsuranceStatus, True)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsuranceStatus, True)
                        PopulateControlFromBOProperty(TextboxReinsuranceStatus, State.MyBO.GetReinsuranceStatusDescription(moCertItemCoverage.ReinsuranceStatusId))
                    Else
                        ControlMgr.SetVisibleControl(Me, LabelReinsuranceStatus, False)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsuranceStatus, False)
                    End If
                    If (Not moCertItemCoverage.ReinsuranceStatusId.Equals(Guid.Empty)) AndAlso moCertItemCoverage.ReinsuranceStatusId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REINSURANCE_STATUSES, LookupListNew.LK_REINS_STATUS_REJECTED)) Then
                        ControlMgr.SetVisibleControl(Me, LabelReinsRejectReason, True)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsRejectReason, True)
                        PopulateControlFromBOProperty(TextboxReinsRejectReason, moCertItemCoverage.ReinsuranceRejectReason)
                    Else
                        ControlMgr.SetVisibleControl(Me, LabelReinsRejectReason, False)
                        ControlMgr.SetVisibleControl(Me, TextboxReinsRejectReason, False)
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateFormDeductibleFormBOs(methodOfRepairId As Guid)
            Dim oDeductible As CertItemCoverage.DeductibleType
            oDeductible = CertItemCoverage.GetDeductible(moCertItemCoverage.Id, methodOfRepairId)
            PopulateControlFromBOProperty(TextboxDeductibleBasedOn, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, oDeductible.DeductibleBasedOnId))
            PopulateControlFromBOProperty(TextboxDeductible, oDeductible.DeductibleAmount)
            PopulateControlFromBOProperty(TextboxDeductiblePercent, oDeductible.DeductiblePercentage)
        End Sub

        Protected Sub IsEffectiveCoverage()
            Dim todayDate As Date

            Try
                If todayDate.Today >= moCertItemCoverage.BeginDate.Value AndAlso todayDate.Today <= moCertItemCoverage.EndDate.Value Then
                    'Me.ImagebuttonEffective.Visible = True
                    'Me.ImagebuttonNoEffective.Visible = False
                    State.coverageInEffect = True
                Else
                    'Me.ImagebuttonEffective.Visible = False
                    'Me.ImagebuttonNoEffective.Visible = True
                    State.coverageInEffect = False
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub IsEffectiveCoverageForGracePeriod()
            Dim todayDate As Date
            Dim strDateOfReport As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED), String)
            Dim objDateOfReport As DateType = Nothing

            If strDateOfReport IsNot Nothing Then

                If strDateOfReport IsNot Nothing AndAlso Not strDateOfReport.Equals(String.Empty) Then
                    objDateOfReport = New DateType(Convert.ToDateTime(strDateOfReport))
                End If
                Try
                    If moCertItemCoverage.IsCoverageEffectiveForGracePeriod(objDateOfReport) Then

                        State.coverageInEffectforGracePeriod = True

                    Else
                        State.coverageInEffectforGracePeriod = False

                    End If
                Catch ex As Exception
                    'Me.HandleErrors(ex, Me.ErrorCtrl)
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            End If

        End Sub
        Protected Sub PopulateBOsFormFrom()

            With State.MyBO
                PopulateBOProperty(State.MyBO, "RiskTypeId", cboRiskTypeId)
                PopulateBOProperty(State.MyBO, "ManufacturerId", cboManufacturerId)
                PopulateBOProperty(State.MyBO, "SerialNumber", TextboxSerialNumber)
                PopulateBOProperty(State.MyBO, "IMEINumber", TextboxIMEINumber)
                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                If (Not moDealer.DealerTypeId.Equals(dealerTypeVSC)) Then
                    PopulateBOProperty(State.MyBO, "Model", TextboxModel)
                    'Else
                    '   Me.PopulateBOProperty(Me.State.MyBO, "Model", String.Empty)
                End If
                PopulateBOProperty(moCertItemCoverage, "MethodOfRepairId", cboMethodOfRepair)
                PopulateBOProperty(State.MyBO, "ItemDescription", TextboxDealerItemDesc)
                PopulateBOProperty(moCertificate, "InvoiceNumber", TextboxInvNum)
                PopulateBOProperty(State.MyBO, "SkuNumber", TextboxSKU)
            End With

            '#REQ 1106 start
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, moDealer.UseEquipmentId) = Codes.YESNO_Y Then
                If State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                    PopulateClaimedEquipmentBOFromform()
                End If
            End If
            '#REQ 1106 end

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CreateNew()
            Try
                State.ScreenSnapShotBO = Nothing 'Reset the backup copy

                State.MyBO = New CertItem
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNewWithCopy()
            Try
                State.MyBO = New CertItem
                PopulateBOsFormFrom()
                EnableDisableFields()

                'create the backup copy
                State.ScreenSnapShotBO = New CertItem
                State.ScreenSnapShotBO.Clone(State.MyBO)
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub LocateServiceCenter()
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = State._moCertItemCoverage
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = State.MyBO
                If (moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC OrElse moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso GetClaims() Then
                    NavController.Navigate(Me, "locate_master_claim", BuildMasterClaimParameters)
                Else
                    NavController.Navigate(Me, "locate_service_center", BuildServiceCenterParameters)
                    'arf 12-20-04 end
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    'Clean after consuming the action
                    State.ActionInProgress = DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
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



            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim actionInProgress As DetailPageCommand = State.ActionInProgress
            CleanPopupInput()
            Try
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If actionInProgress <> DetailPageCommand.BackOnErr Then
                        State.MyBO.Save()
                    End If
                    Select Case actionInProgress
                        Case DetailPageCommand.Back
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case DetailPageCommand.New_
                            'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            CreateNew()
                        Case DetailPageCommand.NewAndCopy
                            'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                            CreateNewWithCopy()
                        Case DetailPageCommand.BackOnErr
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO))
                            Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case DetailPageCommand.Redirect_
                            'pm 06-07-06 '
                            LocateServiceCenter()
                        Case DetailPageCommand.Cancel
                            NavController.Navigate(Me, "cancel", State.certificateId)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case actionInProgress
                        Case DetailPageCommand.Back
                            'arf 12-20-04 'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                            Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case DetailPageCommand.New_
                            CreateNew()
                        Case DetailPageCommand.NewAndCopy
                            CreateNewWithCopy()
                        Case DetailPageCommand.BackOnErr
                            'Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                'Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub LoadEnrolledEquipmentDropdowns()
            'If Use equipment flag is yes , load enrolled equipment manufacturer from dealers Equipment list else load from company group list
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State._modealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Not String.IsNullOrEmpty(State._modealer.EquipmentListCode) Then
                    'BindListControlToDataView(Me.cboManufacturerId, LookupListNew.GetManufacturerbyEquipmentList(Me.State._modealer.EquipmentListCode, Date.Now))
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.EquipmentListCode = State._modealer.EquipmentListCode
                    listcontext.EffectiveOnDate = Date.Now
                    Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByEquipmentCode", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
                    {
                      .AddBlankItem = True
                        })
                Else
                    MasterPage.MessageController.AddWarning("EQUIPMENT_LIST_DOES_NOT_EXIST_FOR_DEALER")
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
        Public Shared Function LoadSku(manufacturerId As String, model As String, dealerId As String) As String

            Dim dealer As New Dealer(New Guid(dealerId))
            Dim equipmentId As Guid = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Now, _
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

#Region "Validation"

        Private Function IsManufacturerMissing() As Boolean
            If State.MyBO.ManufacturerId.Equals(Guid.Empty) Then
                State.manufacturerMissing = True
                Return True
            Else
                State.manufacturerMissing = False
                Return False
            End If
        End Function

        Private Function IsCustomerNameMissing() As Boolean
            If moCertificate.CustomerName Is Nothing Then
                State.customerNameMissing = True
                Return True
            Else
                State.customerNameMissing = False
                Return False
            End If
        End Function

        Private Function IsIdentificationNumberMissing() As Boolean
            If moCertificate.IdentificationNumber Is Nothing Then
                State.IdentificationNumber = True
                Return True
            Else
                State.IdentificationNumber = False
                Return False
            End If
        End Function

        Private Function IsZipMissing() As Boolean
            Dim addr As Address

            addr = moCertificate.AddressChild(False)
            If addr IsNot Nothing Then
                If moCertificate.StatusCode = Active AndAlso _
                    addr.PostalCode = "" Then
                    State.zipMissing = True
                    Return True
                Else
                    State.zipMissing = False
                    Return False
                End If
            Else
                State.zipMissing = True
                Return True
            End If


        End Function


        Private Function GetClaims() As Boolean
            Dim claimsDV As DataView

            claimsDV = moCertItemCoverage.GetAllClaims(State.certificateCoverageId)

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
            claimsDV = ClaimBO.GetCertClaims(State.certificateId)

            If claimsDV.Count > 0 Then
                'Me.State.activeOrPendingClaim = True
                Return True
            Else
                'Me.State.activeOrPendingClaim = False
                Return False
            End If
        End Function

        Private Function IsInvoiceNumberMissing() As Boolean
            If moCertificate.InvoiceNumber Is Nothing Then
                State.invoiceNumberMissing = True
                Return True
            Else
                State.invoiceNumberMissing = False
                Return False
            End If
        End Function

        Private Function IsProductSalesDateMissing() As Boolean
            If moCertificate.ProductSalesDate Is Nothing Then
                State.productSalesDateMissing = True
                Return True
            Else
                State.productSalesDateMissing = False
                Return False
            End If
        End Function

        Private Function IsDepreciationScheduleNotDefined(ContractID As Guid) As Boolean

            'Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            Dim al As ArrayList = Claim.CalculateLiabilityLimit(State.certificateId, ContractID, moCertItemCoverage.Id)
            If CType(al(1), Integer) <> 0 Then
                State.depreciationScheduleNotDefined = True
            Else
                State.depreciationScheduleNotDefined = False
            End If

            Return State.depreciationScheduleNotDefined
        End Function

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                moCertificate = State.MyBO.GetCertificate(State.certificateId)
                PopulateBOsFormFrom()
                If State.MyBO.IsFamilyDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Back
                Else
                    'arf 12-20-04  'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_COMMENTADDED) = Nothing                    
                    Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Back, State.MyBO, State.boChanged)
                    If NavController.PrevNavState.Name = "LOCATE_ELIGIBLE_COVERAGES" Then
                        NavController.Navigate(Me, "back_to_locate_eligible_coverage", retObj)
                    Else
                        NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                    End If
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
            Try
                moCertificate = State.MyBO.GetCertificate(State.certificateId)
                moCertItemCoverage = New CertItemCoverage(State.certificateCoverageId)
                PopulateBOsFormFrom()
                Dim flag As Boolean = True
                If moCertItemCoverage IsNot Nothing Then
                    Dim errMsg As List(Of String)
                    Dim warningMsg As List(Of String)
                    flag = flag AndAlso moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg)
                    MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
                    MasterPage.MessageController.AddError(errMsg.ToArray, True)
                End If

                If State.MyBO.IsEquipmentRequired AndAlso State.inputParameters.sCallingObject = CALLED_FROM_NEW_CLAIM Then
                    Dim msgList As New List(Of String)
                    If Not State.objClaimedEquipment.ValidateForClaimProcess(msgList) Then
                        MasterPage.MessageController.AddError(msgList.ToArray, True)
                        Return
                    End If
                End If

                If State.MyBO.IsFamilyDirty OrElse moCertItemCoverage.IsFamilyDirty Then
                    If (moCertItemCoverage.IsFamilyDirty) Then
                        moCertItemCoverage.Save()
                    End If
                    If (State.MyBO.IsFamilyDirty) Then
                        State.MyBO.Save()
                    End If
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    If Not State.MyBO.IsEquipmentRequired Then
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED, True)
                    End If
                End If

                PopulateFormFromBOs()
                IsEdit = False
                State.boChanged = True
                EnableDisableFields()
                ControlMgr.SetEnableControl(Me, ButtonLocateCenter, flag)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As Object, e As EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New CertItem(State.MyBO.Id)
                    moCertItemCoverage = New CertItemCoverage(State.certificateCoverageId)
                    moCertificate = State.MyBO.GetCertificate(State.MyBO.CertId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New CertItem
                End If
                IsEdit = False
                PopulateFormFromBOs()
                EnableDisableControls(EditPanel_WRITE, True)
                EnableDisableControls(pnlVehicleInfo, True)
                EnableDisableControls(pnlDeviceInfo, True)
                EnableDisableFields()
                If moCertItemCoverage IsNot Nothing Then
                    Dim errMsg As List(Of String)
                    Dim warningMsg As List(Of String)
                    If Not moCertItemCoverage.IsCoverageValidToOpenClaim(errMsg, warningMsg) Then
                        MasterPage.MessageController.AddError(errMsg.ToArray, True)
                        Exit Sub ' no need to anything because we have error on coverage
                    Else
                        MasterPage.MessageController.AddWarning(warningMsg.ToArray, True)
                    End If
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub btnEdit_WRITE_Click(sender As Object, e As EventArgs) Handles btnEdit_WRITE.Click
            Try
                IsEdit = True
                EnableDisableFields()
                ButtonSoftQuestions.Enabled = False
                ButtonSoftQuestions.Visible = False
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Function BuildServiceCenterParameters() As LocateServiceCenterForm.Parameters
            Dim cert As Certificate = moCertificate
            Dim coverageType As String = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), moCertItemCoverage.CoverageTypeId)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
            Dim oContract As Contract = Contract.GetContract(moCertificate.DealerId, moCertificate.WarrantySalesDate.Value)
            Dim ComingFromDenyClaim As Boolean = False
            Dim todayDate As Date
            'Req-1016 - Start
            Dim emptyGuid As Guid = Guid.Empty
            Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
            'Req-1016 - end

            Dim showAcceptButton As Boolean = True
            Dim claimsDV As DataView = moCertItemCoverage.GetClaims(State.certificateCoverageId)
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
            claimsRole = ElitaPlusPrincipal.Current.IsInRole(Claims)
            claimsAnalyst = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsAnalyst)
            claimSupport = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimSupport)
            commentsRole = ElitaPlusPrincipal.Current.IsInRole(Comments)
            csrRole = ElitaPlusPrincipal.Current.IsInRole(CSR)
            csr2Role = ElitaPlusPrincipal.Current.IsInRole(CSR2)
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
            ElseIf oContract IsNot Nothing AndAlso
                    ((Not oContract.RecurringPremiumId.Equals(emptyGuid)) AndAlso (Not oContract.RecurringPremiumId.Equals(singlePremiumId))) Then
                'Req-1016 - end
                showAcceptButton = True
            ElseIf Not State.coverageInEffect Then
                showAcceptButton = False
            End If

            'if coverage is expired and claim allowed is yes then show accept button
            If Not coverageType = Manufacture Then
                Try
                    If oContract Is Nothing Then
                        If Not State.noContractFoundErrorAdded Then
                            State.noContractFoundErrorAdded = True
                            Dim errors() As ValidationError = {New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, GetType(Contract), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(Contract).FullName, Nothing)
                        End If
                    ElseIf Not (IsManufacturerMissing()) AndAlso Not (IsZipMissing()) AndAlso Not (IsDepreciationScheduleNotDefined(oContract.Id)) Then
                        If todayDate.Today > moCertItemCoverage.BeginDate.Value AndAlso todayDate.Today > moCertItemCoverage.EndDate.Value Then
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
                    HandleErrors(ex, MasterPage.MessageController)
                End Try

            End If

            Return New LocateServiceCenterForm.Parameters(cert.DealerId, cert.AddressChild.ZipLocator, State.MyBO.RiskTypeId, State.MyBO.ManufacturerId, _
                                                          coverageType, moCertItemCoverage.Id, Guid.Empty, showAcceptButton, , ComingFromDenyClaim, , State.objClaimedEquipment)

        End Function

        Function BuildMasterClaimParameters() As LocateMasterClaimListForm.Parameters
            Dim cert As Certificate = moCertificate
            Dim coverageType As String = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), moCertItemCoverage.CoverageTypeId)
            Dim showAcceptButton As Boolean = True

            If coverageType = "M" OrElse Not State.coverageInEffect Then
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
                claimsRole = ElitaPlusPrincipal.Current.IsInRole(Claims)
                claimsAnalyst = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimsAnalyst)
                claimSupport = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.ClaimSupport)
                commentsRole = ElitaPlusPrincipal.Current.IsInRole(Comments)
                csrRole = ElitaPlusPrincipal.Current.IsInRole(CSR)
                csr2Role = ElitaPlusPrincipal.Current.IsInRole(CSR2)
                countySuperUser = ElitaPlusPrincipal.Current.IsInRole(CertItemForm.CountySuperUser)
                otherAllowedRoles = callCenterAgent OrElse callCenterSupervisor OrElse claimsRole OrElse claimsAnalyst OrElse claimSupport OrElse commentsRole OrElse csrRole OrElse csr2Role OrElse countySuperUser

                If ((claimsManager) OrElse (officeManager) OrElse (IHQSup) OrElse (otherAllowedRoles)) Then
                    showAcceptButton = True
                    'End of DEF-2035
                Else
                    showAcceptButton = False
                End If
            End If

            Return New LocateMasterClaimListForm.Parameters(cert.DealerId, cert.AddressChild.ZipLocator, State.MyBO.RiskTypeId, State.MyBO.ManufacturerId, coverageType, moCertItemCoverage.Id, showAcceptButton, , State.objClaimedEquipment, cert.getMasterclaimProcFlag, State.selDateOfLoss)


        End Function


        Private Sub ButtonLocateCenter_Click(sender As Object, e As EventArgs) Handles ButtonLocateCenter.Click

            Try
                Dim msg As String
                If Not moCertItemCoverage.IsPossibleWarrantyClaim(msg) Then
                    'arf 12-20-04 'Me.callPage(LocateServiceCenterForm.URL, BuildServiceCenterParameters())
                    'arf 12-20-04 begin
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = State._moCertItemCoverage
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = State.MyBO


                    Dim oContract As New Contract
                    Dim objCert As New Certificate(State.MyBO.CertId)
                    oContract = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
                    If oContract IsNot Nothing Then
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.AllowDifferentCoverage) = Codes.YESNO_Y Then
                            State.allowdifferentcoverage = True
                        End If
                    End If

                    If (((moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC OrElse moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso GetClaims()) OrElse (moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_ANYMC OrElse moCertificate.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL) AndAlso State.allowdifferentcoverage _
                             AndAlso GetCertClaims()) Then
                        NavController.Navigate(Me, "locate_master_claim", BuildMasterClaimParameters)
                    Else
                        NavController.Navigate(Me, "locate_service_center", BuildServiceCenterParameters)
                        'arf 12-20-04 end
                    End If
                Else
                    DisplayMessage(msg, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Redirect_
                End If
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ButtonSoftQuestions_Click(sender As Object, e As EventArgs) Handles ButtonSoftQuestions.Click
            Try
                'Me.callPage(SoftQuestionsList.URL, New SoftQuestionsList.Parameters(Me.State.MyBO.RiskTypeId, Me.State.certificateId))

                Dim frameSoftQuestions As HtmlIframe = CType(pnlPopup.FindControl("frameSoftQuestions"), HtmlIframe)
                Dim sFrameSource As String = "../Tables/SoftQuestionsList.aspx?RiskTypeID=" & State.MyBO.RiskTypeId.ToString() & "&CertificateID=" & State.certificateId.ToString() & "&CertificateCompanyID=" & State.certificateCompanyId.ToString() & "&IsComingFromClaimWizard=false"
                frameSoftQuestions.Attributes.Add("src", sFrameSource)
                frameSoftQuestions.Attributes.Add("style", "width: 880px; height: 550px;")

                'Me.NavController.Navigate(Me, FlowEvents.EVENT_SOFT_QUESTIONS, New SoftQuestionsList.Parameters(Me.State.MyBO.RiskTypeId, Me.State.certificateId, Me.State.certificateCompanyId))
                'Dim currentState As MyState = CType(Me.NavController.State, MyState)

                mdlPopup.Show()
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnDenyClaim_Click(sender As Object, e As EventArgs) Handles btnDenyClaim.Click
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = State._moCertItemCoverage
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM) = State.MyBO
                NavController.Navigate(Me, "locate_service_center", BuildServiceCenterParameters)
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                NavController.Navigate(Me, "back")
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
        Private Sub cboMethodOfRepair_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMethodOfRepair.SelectedIndexChanged
            Try
                PopulateFormDeductibleFormBOs(GetSelectedItem(cboMethodOfRepair))
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrorCtrl)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Error Handling"

#End Region


    End Class
End Namespace

