'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/2/2004)  ********************
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Configuration
Imports Assurant.Elita.ExternalKeyHandler.DynamicFulfillment
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports ClientEventPayLoad = Assurant.ElitaPlus.DataEntities.DFEventPayLoad
Imports Codes = Assurant.ElitaPlus.BusinessObjectsNew.Codes

Partial Class ClaimForm
    Inherits ElitaPlusSearchPage

#Region "Variables"
    Private mbIsFirstPass As Boolean = True
    Public Shared Save_Ok As Boolean = False
#End Region

#Region "Constants"

    Public Const URL As String = "~/Claims/ClaimForm.aspx"
    Public Const AbsoluteUrl As String = "/ElitaPlus/Claims/ClaimForm.aspx"
    Public Const MASTER_CLAIM_URL As String = "~/Claims/MasterClaimListForm.aspx"
    Public Const MASTER_CLAIM_DETAIL_URL As String = "~/Claims/MasterClaimForm.aspx"
    Public Const NEW_CLAIM_URL As String = "~/Claims/NewClaimForm.aspx"
    Public Const COVERAGE_TYPE_URL As String = "~/Claims/CoverageTypeList.aspx"
    Public Const CLAIM_STATUS_DETAIL_FORM As String = "~/Claims/ClaimStatusDetailForm.aspx"
    Private Const ACTIVE_STATUS As String = "A"
    Public Const NULL_VALUE As String = "0"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 1
    Public Const GRID_COL_AMOUNT_IDX As Integer = 2
    Public Const GRID_COL_CREATED_DATETIME_IDX As Integer = 6
    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 7
    Public params As New ArrayList

    'Req-784
    Protected WithEvents moUserControlAddress As UserControlAddress_New

    'REQ 1106
    Private Const TAB_Claim_authorization As Integer = 0
    Private Const TAB_Device_Information As Integer = 1
    Public Const COL_PRICE_DV As String = "Price"
    'REQ-5615
    Public Const YES As String = "Y"
    Public Const COMP_ATTR_BLOCK_PAY_INVOICE As String = "BLOCK_PAY_INVOICE"

    'REQ-6230
    Public Const RETAIL_PRICE_SEARCH As String = "RETAIL_PRICE_SEARCH"
    Public Const VALIDATE_SERVICE_WARRANTY As String = "VALIDATE_SERVICE_WARRANTY"

#End Region
#Region "Tabs"
    Public Const Tab_DeviceInfo As String = "0"
    Public Const Tab_ShippingInfo As String = "1"
    Public Const Tab_ClaimAuthorization As String = "2"
    Public Const Tab_ConsequentialDamage As String = "3"

    Dim DisabledTabsList As New List(Of String)()

#End Region

#Region "Property"

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

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public IsCallerAuthenticated As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
        Public Sub New(LastOp As DetailPageCommand, Optional ByVal IsCallerAuthenticated As Boolean = False)
            LastOperation = LastOp
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
    End Class
#End Region

#Region "Page State"

    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
        Public MyBO As ClaimBase
        Public AssociatedClaimBO As ClaimBase
        Public IsEditMode As Boolean = False
        Public AuthorizedAmountChanged As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public AuthDetailEnabled As Boolean = False
        Public HasAuthDetailDataChanged As Boolean = False
        Public HasClaimStatusBOChanged As Boolean = False
        Public searchDV As CertItemCoverage.CertItemCoverageSearchDV = Nothing
        Public IsSVCCUserEdit As Boolean = False
        Public PartsInfoDV As DataView
        Public ClaimAuthDetailBO As ClaimAuthDetail
        Public PrevAuthorizedAmt As DecimalType
        'Public AuthorizedAmount As DecimalType
        Public PrevDeductible As DecimalType
        Public InputParameters As Parameters
        Public IsMultiAuthClaim As Boolean = False
        Public YesId As Guid
        Public NoId As Guid
        Public IsGridVisible As Boolean = False
        Public SortExpression As String
        Public claimAuthList As List(Of ClaimAuthorization)
        Public IsTEMP_SVC As Boolean = False
        Public ClaimShippingBO As ClaimShipping.ClaimShippingDV
        Public ClaimEquipmentBO As ClaimEquipment.ClaimEquipmentDV
        Public RefurbReplaceClaimEquipmentId As Guid
        Public InboundClaimShippingId As Guid
        Public OutboundClaimShippingId As Guid
        Public FulfilmentBankinfoBo As BusinessObjectsNew.BankInfo
        Public IsCallerAuthenticated As Boolean = False
        Public FulfillmentDetailsResponse As FulfillmentDetails = Nothing

    End Class

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            Else
                If NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(NavController.State, MyState)
                    StartNavControl()
                    NavController.State = s
                End If
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallFromUrl.Contains(ClaimRecordingForm.Url2) Then
                ' Remove the Claim Recording page from the stack(return path flow)
                MyBase.SetPageOutOfNavigation()
            End If

            If CallingParameters IsNot Nothing Then
                StartNavControl()
                Try
                    State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(CallingParameters, Guid))
                Catch ex As Exception
                    State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(CallingParameters, Parameters).claimId)
                    State.IsCallerAuthenticated = CType(CallingParameters, Parameters).IsCallerAuthenticated
                End Try
                If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then State.IsMultiAuthClaim = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Protected Sub InitializeFromFlowSession()

        State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters IsNot Nothing Then
                State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(State.InputParameters.claimId, Guid))
                If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then State.IsMultiAuthClaim = True
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public claimId As Guid
        Public updatedClaimAuthDetail As ClaimAuthDetailForm.ReturnType 'DEF-17426
        Public IsCallerAuthenticated As Boolean = True

        Public Sub New(claimId As Guid)
            Me.claimId = claimId
        End Sub

        'DEF-17426
        Public Sub New(claimId As Guid, updatedClaimAuthDetail As ClaimAuthDetailForm.ReturnType, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.claimId = claimId
            Me.updatedClaimAuthDetail = updatedClaimAuthDetail
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub

        Public Sub New(claimId As Guid, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.claimId = claimId
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub

    End Class
#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State.MyBO IsNot Nothing) Then
                MasterPage.BreadCrum = String.Format("{0}{1}{2} {3}", MasterPage.PageTab, ElitaBase.Sperator, TranslationBase.TranslateLabelOrMessage("CLAIM"), State.MyBO.ClaimNumber)
            End If
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        MasterPage.MessageController.Clear()
        Try
            If NavController.CurrentNavState.Name <> "CLAIM_DETAIL" Then
                If NavController.CurrentNavState.Name <> "DENIED_CLAIM_CREATED" AndAlso NavController.CurrentNavState.Name <> "CLAIM_ISSUE_APPROVED_FROM_CLAIM" _
                   AndAlso NavController.CurrentNavState.Name <> "CLAIM_ISSUE_APPROVED_FROM_CERT" Then
                    Return
                End If
            End If

            'DEF-17426
            'If Me.NavController.Context = "CLAIM_DETAIL-AUTH_DETAIL-CLAIM_DETAIL" Then
            If (NavController IsNot Nothing) AndAlso (NavController.PrevNavState IsNot Nothing) Then
                If (NavController.PrevNavState.Name = "AUTH_DETAIL") Then
                    Dim retObj As ClaimAuthDetailForm.ReturnType = CType(NavController.ParametersPassed, Parameters).updatedClaimAuthDetail
                    If NavController.ParametersPassed IsNot Nothing Then
                        If retObj.HasDataChanged Or retObj.HasClaimStatusBOChanged Then
                            State.MyBO = retObj.ClaimBO
                            State.HasAuthDetailDataChanged = retObj.HasDataChanged
                            State.HasClaimStatusBOChanged = retObj.HasClaimStatusBOChanged
                            CType(State.MyBO, Claim).UpdateClaimAuthorizedAmount(retObj.ClaimAuthDetailBO)
                            State.ClaimAuthDetailBO = retObj.ClaimAuthDetailBO
                            State.PartsInfoDV = retObj.PartsInfoDV
                        Else
                            State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                        End If
                    Else
                        State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                    End If
                ElseIf NavController.PrevNavState.Url = ClaimIssueDetailForm.URL AndAlso Not IsPostBack Then
                    ucClaimConsequentialDamage.UpdateConsequentialDamagestatus(State.MyBO)
                    State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                    'ElseIf NavController.PrevNavState.Url = NewClaimForm.URL AndAlso Not Me.IsPostBack Then
                    '    If(Not NavController.ParametersPassed Is nothing AndAlso Not CType(NavController.ParametersPassed,Claim) is Nothing)
                    '        State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(NavController.ParametersPassed,Claim).Id)
                    '    End If
                End If
            End If

            If Not IsPostBack Then
                InitializeData()
                'Return to the calling screen if status is pending
                If Me.State.MyBO.Status = BasicClaimStatus.Pending Then
                    Dim myBo As ClaimBase = State.MyBO
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, State.IsCallerAuthenticated)
                    NavController = Nothing
                    ReturnToCallingPage(retObj)
                End If
                InitializeUI()
                NotifyChanges(NavController)
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisablePageControls()
                'Me.State.AuthorizedAmount = GetPrice()
                State.PrevAuthorizedAmt = If(State.IsMultiAuthClaim, CType(State.MyBO, MultiAuthClaim).AuthorizedAmount, CType(State.MyBO, Claim).AuthorizedAmount)
                State.PrevDeductible = State.MyBO.Deductible
                If (State.IsMultiAuthClaim) Then
                    TranslateGridHeader(GridClaimAuthorization)
                    PopulateGrid()
                    dvClaimAuthorizationDetails.Visible = True
                    SetEnabledForControlFamily(dvClaimAuthorizationDetails, True, True)
                Else
                    dvClaimAuthorizationDetails.Visible = False
                    DisabledTabsList.Add(Tab_ClaimAuthorization)
                    DisabledTabsList.Add(Tab_ConsequentialDamage)
                End If

                If State.MyBO.CertificateItem.IsEquipmentRequired Then
                    PopulateClaimEquipment()
                    dvClaimEquipment.Visible = True
                Else
                    dvClaimEquipment.Visible = False
                End If

                If Not dvClaimEquipment.Visible And Not dvClaimAuthorizationDetails.Visible Then
                    dvClaimEquipment.Visible = False
                    dvClaimAuthorizationDetails.Visible = False
                    ucClaimConsequentialDamage.Visible = False
                    'ViewPanel_READ1.Visible = False
                Else
                    ViewPanel_READ1.Visible = True
                End If

                dvRefurbReplaceEquipment.Visible = True

                'REQ-5467
                If (IsNewUI) Then
                    If State.MyBO.Dealer.IsLawsuitMandatoryId = State.YesId Then
                        LabelIsLawsuitId.Text = "<span class=""mandatory"">*</span> " & LabelIsLawsuitId.Text
                    End If
                End If
            Else
                GetDisabledTabs()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromCreateClaimConfirm()
            CheckIfComingFromSaveConfirm()
            BindclaimFulfillmentDetails()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
                If State.MyBO.EnrolledEquipment IsNot Nothing Then
                    AddLabelDecorations(State.MyBO.EnrolledEquipment)
                End If
                If State.MyBO.ClaimedEquipment IsNot Nothing Then
                    AddLabelDecorations(State.MyBO.ClaimedEquipment)
                End If
                CheckIfComingFromAuthDetailForm()
                CheckClaimPaymentInProgress()
            End If


        Catch ex As Threading.ThreadAbortException
            System.Threading.Thread.ResetAbort()
        Catch ex As Exception
            CleanPopupInput()
            CleanHiddenLimitExceededInput()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub GetDisabledTabs()
        Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
        If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
            DisabledTabsList.AddRange(DisabledTabs)
            hdnDisabledTab.Value = String.Empty
        End If
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            If CalledUrl = Certificates.CertificateForm.URL Then
                NavController = CType(MyBase.State, BaseState).NavCtrl
            End If

            'if coming from ClaimAuthDetail form reload claim
            If CalledUrl = ClaimAuthDetailForm.URL Then
                Dim retObj As ClaimAuthDetailForm.ReturnType = CType(ReturnPar, ClaimAuthDetailForm.ReturnType)
                If retObj IsNot Nothing AndAlso (retObj.HasDataChanged Or retObj.HasClaimStatusBOChanged) Then
                    State.MyBO = retObj.ClaimBO
                    State.HasAuthDetailDataChanged = retObj.HasDataChanged
                    State.HasClaimStatusBOChanged = retObj.HasClaimStatusBOChanged
                    CType(State.MyBO, Claim).UpdateClaimAuthorizedAmount(retObj.ClaimAuthDetailBO)
                    State.ClaimAuthDetailBO = retObj.ClaimAuthDetailBO
                    State.PartsInfoDV = retObj.PartsInfoDV
                Else
                    State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                End If
            ElseIf CalledUrl = MasterClaimForm.URL Then
                Dim retObj As MasterClaimForm.ReturnType = CType(ReturnPar, MasterClaimForm.ReturnType)
                If retObj IsNot Nothing Then
                    StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.ClaimBO.Id, Guid))
                    State.MyBO = claimTmp
                End If
            ElseIf CalledUrl = COVERAGE_TYPE_URL Then
                Dim retObj As Claims.CoverageTypeList.ReturnType = CType(ReturnPar, Claims.CoverageTypeList.ReturnType)
                If retObj IsNot Nothing Then
                    StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.ClaimBO.Id, Guid))
                    State.MyBO = claimTmp
                End If
            ElseIf CalledUrl = CLAIM_STATUS_DETAIL_FORM Then
                Dim retObj As ClaimStatusDetailForm.ReturnType = CType(ReturnPar, ClaimStatusDetailForm.ReturnType)
                If retObj IsNot Nothing Then
                    StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.claimId, Guid))
                    State.MyBO = claimTmp
                End If
            ElseIf CalledUrl = ClaimAuthorizationDetailForm.URL Then
                State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
            ElseIf CalledUrl = ClaimDetailsForm.Url Then
                Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                If retObj IsNot Nothing Then
                    StartNavControl()
                    State.MyBO = retObj.EditingBo
                    State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                End If
            ElseIf CalledUrl = ClaimRecordingForm.Url Then
                Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                If retObj IsNot Nothing Then
                    StartNavControl()
                    State.MyBO = retObj.EditingBo
                    State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                End If
            End If
            If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then State.IsMultiAuthClaim = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Controlling Logic"

    Private Sub EnableDisablePageControls()

        ControlMgr.SetVisibleControl(Me, TextboxClaimStatus, True)

        If State.MyBO.Company.MasterClaimProcessingId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_MASTERCLAIMPROC, Codes.MasterClmProc_NONE)) Then
            ControlMgr.SetVisibleForControlFamily(Me, LabelMasterClaimNumber, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxMasterClaimNumber, False, True)
        End If

        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, State.MyBO.Company.CompanyTypeId) = State.MyBO.Company.COMPANY_TYPE_INSURANCE Then
            ControlMgr.SetVisibleControl(Me, LabelCaller_Tax_Number, True)
            ControlMgr.SetVisibleControl(Me, TextboxCaller_Tax_Number, True)
        Else
            ControlMgr.SetVisibleControl(Me, LabelCaller_Tax_Number, False)
            ControlMgr.SetVisibleControl(Me, TextboxCaller_Tax_Number, False)
        End If

        If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            ControlMgr.SetVisibleControl(Me, TextboxFraudulent, True)
            ControlMgr.SetVisibleControl(Me, lblPotFraudulent, True)
            ControlMgr.SetVisibleControl(Me, TextboxComplaint, True)
            ControlMgr.SetVisibleControl(Me, lblComplaint, True)
        Else
            ControlMgr.SetVisibleControl(Me, cboDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, LabelDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, LabelDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, TextboxDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, TextboxFraudulent, False)
            ControlMgr.SetVisibleControl(Me, lblPotFraudulent, False)
            ControlMgr.SetVisibleControl(Me, TextboxComplaint, False)
            ControlMgr.SetVisibleControl(Me, lblComplaint, False)

        End If


        If State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
            cbousershipaddress.SelectedValue = State.NoId.ToString()
            moUserControlContactInfo.Visible = False
        Else
            cbousershipaddress.SelectedValue = State.YesId.ToString
            moUserControlContactInfo.Visible = True
        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, Lbluseshipaddress, True)
            ControlMgr.SetVisibleControl(Me, cbousershipaddress, True)
        End If

        SetEnabledForControlFamily(ViewPanel_READ, False)
        SetEnabledForControlFamily(ViewPanel_READ2, False)

        SetEnabledForControlFamily(txtRefurbReplaceComments, State.IsEditMode, True)
        SetEnabledForControlFamily(txtShipToSC, State.IsEditMode, True)
        SetEnabledForControlFamily(txtShipToCust, State.IsEditMode, True)

        If (Me.State.MyBO.Status = BasicClaimStatus.Closed) Then
            SetEnabledForControlFamily(LabelReasonClosed, State.IsEditMode, True)
            SetEnabledForControlFamily(cboReasonClosed, State.IsEditMode, True)
            SetEnabledForControlFamily(cbousershipaddress, State.IsEditMode, True)

        Else
            EnableEditableFieldsByDefault()
            EnableDisableEditableFieldsForActiveClaims()

            If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
                SetEnabledForControlFamily(LabelDeniedReason, State.IsEditMode, True)
                SetEnabledForControlFamily(cboDeniedReason, State.IsEditMode, True)
                SetEnabledForControlFamily(TextboxAuthorizationNumber, State.IsEditMode, True)
            End If
        End If

        'Handling of Who Pays, Notification Type and Store Number
        If (State.MyBO.NotificationTypeId.Equals(Guid.Empty)) Then
            ControlMgr.SetVisibleControl(Me, TextboxStoreNumber, False)
            ControlMgr.SetVisibleControl(Me, TextboxNotificationType, False)
            ControlMgr.SetVisibleControl(Me, cboWhoPays, False)
            ControlMgr.SetVisibleControl(Me, LabelStoreNumber, False)
            ControlMgr.SetVisibleControl(Me, LabelNotificationType, False)
            ControlMgr.SetVisibleControl(Me, LabelWhoPays, False)
        End If

        SetEnabledForControlFamily(LabelFulfilmentMethod, State.IsEditMode, True)
        SetEnabledForControlFamily(cboFulfilmentMethod, State.IsEditMode, True)

        EnableButtonsByDefault()
        'Now Enable/Disable make Visible/Invisible buttons based on the BO Properties and Business Rules
        EnableDisableButtonsConditionally()

        DisableButtonsForClaimSystem()

        'Overide the visiblity setting for MultiAuthClaim
        EnableDisableControlsForMultiAuthClaim()

        ' If claim is readonly then disable all action button
        DisableButtonsForReadonlyClaim()

        If Not State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
            ControlMgr.SetVisibleForControlFamily(Me, LabelCurrentOdometer, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxCurrentOdometer, False, True)
        End If
    End Sub

    Private Sub EnableDisableControlsForMultiAuthClaim()
        ' Show Hide controls on basis of Old and New Claim
        ControlMgr.SetVisibleControl(Me, LabelServiceCenter, LabelServiceCenter.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxServiceCenter, TextboxServiceCenter.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelLoanerCenter, LabelLoanerCenter.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxLoanerCenter, TextboxLoanerCenter.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelAuthorizationNumber, LabelAuthorizationNumber.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxAuthorizationNumber, TextboxAuthorizationNumber.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelSource, LabelSource.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxSource, TextboxSource.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelVisitDate, LabelVisitDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxVisitDate, TextboxVisitDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, ImageButtonVisitDate, ImageButtonVisitDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelInvoiceProcessDate, LabelInvoiceProcessDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxInvoiceProcessDate, TextboxInvoiceProcessDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, ImageButtonInvoiceProcessDate, ImageButtonInvoiceProcessDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelRepairDate, LabelRepairDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxRepairDate, TextboxRepairDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, ImageButtonRepairDate, ImageButtonRepairDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelInvoiceDate, LabelInvoiceDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxInvoiceDate, TextboxInvoiceDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelBatchNumber, LabelBatchNumber.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxBatchNumber, TextboxBatchNumber.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelPickUpDate, LabelPickUpDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxPickupDate, TextboxPickupDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, ImageButtonPickupDate, ImageButtonPickupDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelLoanerReturnedDate, LabelLoanerReturnedDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxLoanerReturnedDate, TextboxLoanerReturnedDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, ImageButtonLoanerReturnedDate, ImageButtonLoanerReturnedDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelDefectReason, LabelDefectReason.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxDefectReason, TextboxDefectReason.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelExpectedRepairDate, LabelExpectedRepairDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxExpectedRepairDate, TextboxExpectedRepairDate.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelTechnicalReport, LabelTechnicalReport.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxTechnicalReport, TextboxTechnicalReport.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelSpecialInstruction, LabelSpecialInstruction.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxSpecialInstruction, TextboxSpecialInstruction.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, LabelDeductibleCollected, LabelDeductibleCollected.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, TextboxDeductibleCollected, TextboxDeductibleCollected.Visible And Not State.IsMultiAuthClaim)

        'Disable Buttons for MultiAuthClaim
        ControlMgr.SetVisibleControl(Me, btnPrint, btnPrint.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnServiceCenterInfo, btnServiceCenterInfo.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnNewServiceCenter, btnNewServiceCenter.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnShipping, btnShipping.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnAuthDetail, btnAuthDetail.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnUseRecoveries, btnUseRecoveries.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, btnClaimDeniedInformation.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnRedo, btnRedo.Visible And Not State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, btnPartsInfo, btnPartsInfo.Visible And Not State.IsMultiAuthClaim)

    End Sub

    Private Sub EnableEditableFieldsByDefault()

        SetEnabledForControlFamily(LabelReasonClosed, State.IsEditMode, True)
        SetEnabledForControlFamily(cboReasonClosed, State.IsEditMode, True)
        SetEnabledForControlFamily(cbousershipaddress, State.IsEditMode, True)
        SetEnabledForControlFamily(LabelAuthorizedAmount, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxAuthorizedAmount, State.IsEditMode, True)
        SetEnabledForControlFamily(LabelDeductible, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxDeductible, State.IsEditMode, True)
        SetEnabledForControlFamily(LabelPolicyNumber, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxPolicyNumber, State.IsEditMode, True)
        SetEnabledForControlFamily(LabelIsLawsuitId, State.IsEditMode, True)
        SetEnabledForControlFamily(cboLawsuitId, State.IsEditMode, True)

        SetEnabledForControlFamily(LabelFollowupDate, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxFollowupDate, State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonFollowupDate, State.IsEditMode, True)
        SetEnabledForControlFamily(ImageButtonFollowupDate, State.IsEditMode, True)

        SetEnabledForControlFamily(LabelRepairDate, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxRepairDate, State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonRepairDate, State.IsEditMode, True)
        SetEnabledForControlFamily(ImageButtonRepairDate, State.IsEditMode, True)

        SetEnabledForControlFamily(LabelDEVICE_ACTIVATION_DATE, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxDEVICE_ACTIVATION_DATE, State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonDeviceActivationDate, State.IsEditMode, True)
        SetEnabledForControlFamily(ImageButtonDeviceActivationDate, State.IsEditMode, True)

        SetEnabledForControlFamily(labelEMPLOYEE_NUMBER, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxEMPLOYEE_NUMBER, State.IsEditMode, True) 'Make it editable

        SetEnabledForControlFamily(LabelLoanerReturnedDate, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxLoanerReturnedDate, State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, ImageButtonLoanerReturnedDate, State.IsEditMode, True)
        SetEnabledForControlFamily(ImageButtonLoanerReturnedDate, State.IsEditMode, True)

        SetEnabledForControlFamily(TextboxProblemDescription, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxSpecialInstruction, State.IsEditMode, True)
        '5921
        SetEnabledForControlFamily(TextboxTrackingNumber, State.IsEditMode, True)
        'REQ-5565
        If (State.MyBO.Company.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")) Then
            SetEnabledForControlFamily(TextboxBatchNumber, State.IsEditMode, True)
        Else
            SetEnabledForControlFamily(TextboxBatchNumber, State.IsEditMode, False)
        End If

        If Me.State.MyBO.Status = BasicClaimStatus.Active And State.MyBO.ClaimClosedDate Is Nothing Then
            SetEnabledForControlFamily(LabelLiabilityLimit, State.IsEditMode, True)
            SetEnabledForControlFamily(TextboxLiabilityLimit, State.IsEditMode, True)
        End If

        SetEnabledForControlFamily(LabelCurrentOdometer, State.IsEditMode, True)
        SetEnabledForControlFamily(TextboxCurrentOdometer, State.IsEditMode, True)

        If Not (State.MyBO.NotificationTypeId.Equals(Guid.Empty)) Then
            ControlMgr.SetVisibleControl(Me, TextboxStoreNumber, Not State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, TextboxNotificationType, Not State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, cboWhoPays, True)
            ControlMgr.SetVisibleControl(Me, LabelStoreNumber, Not State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, LabelNotificationType, Not State.IsEditMode)
            'ControlMgr.SetVisibleControl(Me, Me.LabelWhoPays, False)

            SetEnabledForControlFamily(LabelWhoPays, State.IsEditMode, True)
            SetEnabledForControlFamily(cboWhoPays, State.IsEditMode, True)
        End If

        SetEnabledForControlFamily(txtRefurbReplaceComments, State.IsEditMode, True)

        SetEnabledForControlFamily(txtShipToSC, State.IsEditMode, True)
        SetEnabledForControlFamily(txtShipToCust, State.IsEditMode, True)

        ControlMgr.SetVisibleControl(Me, LabelLoanerRequested, True)
        ControlMgr.SetVisibleControl(Me, TextboxLoanerRequested, True)
    End Sub

    Private Sub EnableDisableEditableFieldsForActiveClaims()

        If (Not State.IsMultiAuthClaim) Then
            Dim claim As Claim = CType(State.MyBO, Claim)
            If (Not (claim.LoanerCenterId.Equals(Guid.Empty))) Then
                'Loaner has been taken
                If (claim.LoanerReturnedDate Is Nothing) Then
                    'Loaner has NOT been returned
                    'For an Active Claim, when a Loaner has been taken and has NOT been Returned
                    'Disable the ReasonClosed field
                    'All other editable fields stay enabled

                    SetEnabledForControlFamily(LabelReasonClosed, False, True)
                    SetEnabledForControlFamily(cboReasonClosed, False, True) 'Disable it
                    SetEnabledForControlFamily(cbousershipaddress, False, True)

                End If
                If ((claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
                    (Not (claim.ReasonClosedId.Equals(Guid.Empty)))) Then
                    'This is a Replacement Claim
                    'Change the Label for the RepairDate field to "REPLACED_ON"
                    LabelRepairDate.Text = Claim.REPLACED_ON
                End If
            Else
                'There is NO Loaner taken
                'Disable the LoanerReturnedDate field

                SetEnabledForControlFamily(LabelLoanerReturnedDate, False, True)
                SetEnabledForControlFamily(TextboxLoanerReturnedDate, False, True) 'Disable it
                ControlMgr.SetVisibleForControlFamily(Me, ImageButtonLoanerReturnedDate, False, True)
                SetEnabledForControlFamily(ImageButtonLoanerReturnedDate, False, True)
            End If
            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode, True)
            SetEnabledForControlFamily(ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode, True)
            SetEnabledForControlFamily(TextboxVisitDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode, True)

            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode And Not claim.LoanerTaken, True)
            SetEnabledForControlFamily(ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode And Not claim.LoanerTaken, True)
            SetEnabledForControlFamily(TextboxPickupDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode And Not claim.LoanerTaken, True)

            'For an Active Claim, Disable the AuthorizedAmount field:
            '1. If there is an Invoice OR
            '2. (If the Item has been Repaired And (.MgrAuthAmountFlag = "Y")) OR
            '3. If ClaimActivity = REWORK
            If ((Not (claim.RepairDate Is Nothing) AndAlso (State.MyBO.MgrAuthAmountFlag = "Y")) _
                OrElse (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) _
                OrElse (Not (claim.InvoiceProcessDate Is Nothing))) _
                Then
                If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__OFFICE_MANAGER) _
                   OrElse ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) _
                   OrElse ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT) Then
                    'By pass for this roles
                Else
                    SetEnabledForControlFamily(LabelAuthorizedAmount, False, True)
                    SetEnabledForControlFamily(TextboxAuthorizedAmount, False, True) 'Disable it
                End If
            End If
            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            ControlMgr.SetVisibleControl(Me, ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, TextboxVisitDate, claim.CanDisplayVisitAndPickUpDates)
            ControlMgr.SetVisibleControl(Me, LabelVisitDate, claim.CanDisplayVisitAndPickUpDates)

            'when loaner is taken, Pick-Up Date should be visible in Display mode, disabled in Edit mode
            ControlMgr.SetVisibleControl(Me, ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, TextboxPickupDate, claim.CanDisplayVisitAndPickUpDates)
            ControlMgr.SetVisibleControl(Me, LabelPickUpDate, claim.CanDisplayVisitAndPickUpDates)

            SetEnabledForControlFamily(ImageButtonPickupDate, State.IsEditMode And Not claim.LoanerTaken)
            SetEnabledForControlFamily(TextboxPickupDate, State.IsEditMode And Not claim.LoanerTaken)
            SetEnabledForControlFamily(LabelReasonClosed, State.IsEditMode And Not claim.LoanerTaken)

        Else
            SetEnabledForControlFamily(LabelLoanerReturnedDate, False, True)
            SetEnabledForControlFamily(TextboxLoanerReturnedDate, False, True) 'Disable it
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonLoanerReturnedDate, False, True)
            SetEnabledForControlFamily(ImageButtonLoanerReturnedDate, False, True)
            SetEnabledForControlFamily(LabelAuthorizedAmount, False, True)
            SetEnabledForControlFamily(TextboxAuthorizedAmount, False, True) 'Disable it
            ControlMgr.SetVisibleControl(Me, LabelFulfilmentMethod, False)
            ControlMgr.SetVisibleControl(Me, cboFulfilmentMethod, False)
        End If


        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id,
                                                State.MyBO.CoverageTypeId, State.MyBO.Certificate.DealerId,
                                                Authentication.LangId, State.MyBO.Certificate.ProductCode, False) _
           AndAlso Me.State.MyBO.Status = BasicClaimStatus.Active Then
            SetEnabledForControlFamily(LabelCauseOfLoss, True, True)
            SetEnabledForControlFamily(cboCauseOfLossId, True, True)
        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, LabelPolicyNumber, False)
        End If

        If (State.MyBO.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (State.MyBO.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
            trDueToSCFromAssurant.Visible = False
        End If

        If (State.MyBO.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
            ControlMgr.SetVisibleControl(Me, trUseEquipment1, False)
            ControlMgr.SetVisibleControl(Me, trUseEquipment2, False)
            ControlMgr.SetVisibleControl(Me, trUseEquipment3, False)
        Else
            ControlMgr.SetVisibleControl(Me, trUseEquipment1, True)
            ControlMgr.SetVisibleControl(Me, trUseEquipment2, True)
            ControlMgr.SetVisibleControl(Me, trUseEquipment3, True)
        End If

        If State.MyBO.Dealer.DeductibleCollectionId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y) Then
            ControlMgr.SetVisibleControl(Me, trDedCollection, True)
        Else
            ControlMgr.SetVisibleControl(Me, trDedCollection, False)
        End If

        If (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then
            'This is a Service Warranty Claim, so HIDE the Amount fields and their associated Labels:
            ControlMgr.SetVisibleForControlFamily(Me, TextboxLiabilityLimit, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelLiabilityLimit, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxDeductible, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelDeductible, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxAboveLiability, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelAboveLiability, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxSlavageAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelSalvageAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxAssurantPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelAssurantPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxConsumerPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelDueToSCFromAssurant, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxDueToSCFromAssurant, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelConsumerPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxDiscount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelDiscount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxBonusAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelBonusAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, TextboxDeductibleCollected, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, LabelDeductibleCollected, False, True)

            'For Service Warranty claim the authorization amount should not be editable 
            SetEnabledForControlFamily(TextboxAuthorizedAmount, False, True)
        End If

    End Sub

    Protected Sub EnableButtonsByDefault()

        ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, Not State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, btnSave_WRITE, State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, btnUndo_WRITE, State.IsEditMode, True)

        If (Not (State.IsEditMode)) Then
            If State.MyBO.IsClaimChild = Codes.YESNO_N Then
                'ReplaceItem button
                ControlMgr.SetVisibleControl(Me, btnReplaceItem, True)
                btnReplaceItem.Enabled = True

                'ServiceWarranty button
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, True)
                btnServiceWarranty.Enabled = True

                'PoliceReport button
                ControlMgr.SetVisibleControl(Me, btnPoliceReport, True)
                btnPoliceReport.Enabled = True

                'Item button 
                '    Andres ControlMgr.SetVisibleControl(Me, btnItem, True)
                btnItem.Enabled = True

                'NewCenter button
                ControlMgr.SetVisibleForControlFamily(Me, btnNewServiceCenter, True, True)
                SetEnabledForControlFamily(btnNewServiceCenter, True, True)

                'ServiceCenterInfo button
                ControlMgr.SetVisibleForControlFamily(Me, btnServiceCenterInfo, True, True)
                SetEnabledForControlFamily(btnServiceCenterInfo, True, True)

                'Auth Detail button
                ControlMgr.SetVisibleForControlFamily(Me, btnAuthDetail, True, True)
                SetEnabledForControlFamily(btnAuthDetail, True, True)

                'PartsInfo button
                ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, True, True)
                SetEnabledForControlFamily(btnPartsInfo, True, True)

                'Certificate button
                ControlMgr.SetVisibleForControlFamily(Me, btnCertificate, True, True)
                SetEnabledForControlFamily(btnCertificate, True, True)

                'RePrint button
                ControlMgr.SetVisibleForControlFamily(Me, btnPrint, True, True)
                SetEnabledForControlFamily(btnPrint, True, True)

                'NewItemInfo button
                ControlMgr.SetVisibleForControlFamily(Me, btnNewItemInfo, True, True)
                SetEnabledForControlFamily(btnNewItemInfo, True, True)

                'Comments button
                ControlMgr.SetVisibleForControlFamily(Me, btnComment, True, True)
                SetEnabledForControlFamily(btnComment, True, True)

                'Reopen button
                ControlMgr.SetVisibleForControlFamily(Me, btnReopen_WRITE, False, True)
                SetEnabledForControlFamily(btnReopen_WRITE, True, True)

                ''AuthDetail button
                'ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, False, True)
                'Me.SetEnabledForControlFamily(Me.btnAuthDetail, True, True)

                'Master Claim button
                ControlMgr.SetVisibleForControlFamily(Me, btnMasterClaim, True, True)
                SetEnabledForControlFamily(btnMasterClaim, True, True)

                'Change Coverage
                ControlMgr.SetVisibleForControlFamily(Me, btnChangeCoverage, True, True)
                SetEnabledForControlFamily(btnChangeCoverage, True, True)

                'Extended Status button
                ControlMgr.SetVisibleForControlFamily(Me, btnStatusDetail, True, True)
                SetEnabledForControlFamily(btnStatusDetail, True, True)

                'Claim Denied Information Button
                ControlMgr.SetVisibleForControlFamily(Me, btnClaimDeniedInformation, True, True)
                SetEnabledForControlFamily(btnClaimDeniedInformation, True, True)

                'Repair and Logistics button
                ControlMgr.SetVisibleForControlFamily(Me, btnRepairLogistics, True, True)
                SetEnabledForControlFamily(btnRepairLogistics, True, True)

                ControlMgr.SetVisibleControl(Me, btnClaimImages, True)
                btnClaimImages.Enabled = True

            End If
        End If

    End Sub

    Private Sub EnableDisableButtonsForSingleAuthClaim()
        Dim claim As Claim = CType(State.MyBO, Claim)


        'For the ServiceWarranty button
        If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < State.MyBO.GetShortDate(State.MyBO.CreatedDate.Value))) OrElse
            (((State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
              (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) OrElse
              (State.MyBO.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
              (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) AndAlso Not claim.CheckSvcWrantyClaimControl())) Then

            btnServiceWarranty.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
        End If

        ' For Redo
        If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT) OrElse
            (claim.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) OrElse
            (claim.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
            (claim.InvoiceProcessDate IsNot Nothing)) Then

            btnRedo.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnRedo, False)
        Else
            Dim claimwithDtExists As Boolean = claim.NumberOfAvailableClaims(State.MyBO.CertItemCoverageId, State.MyBO.CreatedDate.Value, State.MyBO.Id)

            If Not claimwithDtExists Then
                btnRedo.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnRedo, False)
            End If
        End If


        'For the Deny Claim button
        If ((Me.State.MyBO.Status = BasicClaimStatus.Active) OrElse (Me.State.MyBO.Status = BasicClaimStatus.Pending)) And
           (State.MyBO.TotalPaid.Value.Equals(0D)) And
           (Not (State.MyBO.ClaimNumber.ToUpper.EndsWith("S"))) Then
            btnDenyClaim.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnDenyClaim, True)
        End If

        'For the ReplaceItem button
        If ((State.MyBO.Status <> BasicClaimStatus.Active) OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            ((Me.State.MyBO.Status = BasicClaimStatus.Active) And ((Not (claim.RepairDate Is Nothing)) AndAlso (claim.RepairDate.Value >= State.MyBO.GetShortDate(State.MyBO.CreatedDate.Value)))) OrElse
            ((Not (claim.InvoiceProcessDate Is Nothing)) AndAlso (claim.InvoiceProcessDate.Value >= claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make ReplaceItem button Invisible and Disabled
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)

            'disable Reprint button for recovery claim
            btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'if status is denied then display denied reason else hide it
        If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            'disable Reprint button for recovery claim
            btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'Disable Reprint button for backend claims
        If (claim.RepairDate IsNot Nothing) AndAlso (claim.PickUpDate IsNot Nothing) Then
            btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'For the NewCenter button
        If ((Not (claim.LoanerCenterId.Equals(Guid.Empty))) OrElse
            ((Not (claim.InvoiceProcessDate Is Nothing)) AndAlso (claim.InvoiceProcessDate.Value >= claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (claim.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse
            ((Not (claim.RepairDate Is Nothing)) AndAlso (claim.RepairDate.Value >= claim.GetShortDate(claim.CreatedDate.Value)))) Then
            'Make the NewCenter button Invisible and Disabled

            ControlMgr.SetVisibleForControlFamily(Me, btnNewServiceCenter, False, True)
            SetEnabledForControlFamily(btnNewServiceCenter, False, True)
        End If

        'For the RePrint button
        If ((claim.Source <> String.Empty) AndAlso
            ((claim.ClaimActivityCode = String.Empty) OrElse (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK))) Then

            'Make the RePrint button Invisible and Disabled
            ControlMgr.SetVisibleForControlFamily(Me, btnPrint, False, True)
            SetEnabledForControlFamily(btnPrint, False, True)
        End If


        'For the NewItemInfo button
        If ((State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REPLACED AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make the NewItemInfo button Invisible and Disabled
            ControlMgr.SetVisibleForControlFamily(Me, btnNewItemInfo, False, True)
            SetEnabledForControlFamily(btnNewItemInfo, False, True)
        End If

        'For the NewItemInfo, PartsInfo and Shipping buttons
        If (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            'Make the Replace Item button hidden
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
            'Make the PartsInfo button hidden
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, False, True)
            'Make the AuthDetail button hidden
            ControlMgr.SetVisibleForControlFamily(Me, btnAuthDetail, False, True)
            'Make the NewItemInfo button Visible
            ControlMgr.SetVisibleForControlFamily(Me, btnNewItemInfo, True, True)
            'Make the Shipping button Visible if applicable

            If Not State.MyBO.ShippingInfoId.Equals(Guid.Empty) AndAlso Not claim.ServiceCenterId.Equals(Guid.Empty) Then
                Dim objServiceCenter As New ServiceCenter(claim.ServiceCenterId)
                If objServiceCenter.Shipping Then
                    ControlMgr.SetVisibleControl(Me, btnShipping, True)
                    SetEnabledForControlFamily(btnShipping, True, True)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnShipping, False)
            End If
        Else
            ControlMgr.SetVisibleForControlFamily(Me, btnNewItemInfo, False, True)
            ControlMgr.SetVisibleControl(Me, btnShipping, False)
        End If

        'Due to lack of space in the form, the Auth Detail button is placed next to shipping button. This is done since
        'BR is not using the shipping info today. Countries that uses shipping today do not use the auth detail feature.
        'Make the AuthDetail/PartsInfo button visible
        If State.AuthDetailEnabled AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ControlMgr.SetVisibleForControlFamily(Me, btnAuthDetail, btnAuthDetail.Visible And True, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, False, True)
        Else
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnAuthDetail, btnAuthDetail.Visible And False, True)
        End If

        If State.AuthDetailEnabled AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            btnPartsInfo.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPartsInfo, False)
            SetEnabledForControlFamily(TextboxAuthorizedAmount, False, True)
        Else
            btnAuthDetail.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnAuthDetail, btnAuthDetail.Visible And False)
            'Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, True, True)
        End If

        State.searchDV = CertItemCoverage.GetClaimCoverageType(State.MyBO.CertificateId, State.MyBO.CertItemCoverageId,
                                                                  CType(State.MyBO.LossDate, Date), State.MyBO.StatusCode,
                                                                  If(claim.InvoiceProcessDate Is Nothing, Nothing, claim.InvoiceProcessDate.Value))
        If Not State.MyBO.Id.Equals(Guid.Empty) AndAlso
           (State.searchDV IsNot Nothing) AndAlso (State.searchDV.Count > 0) AndAlso
           (claim.RepairDate Is Nothing) AndAlso (claim.PickUpDate Is Nothing) Then
            ControlMgr.SetVisibleControl(Me, btnChangeCoverage, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnChangeCoverage, False)
        End If

        If State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And
           State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK And
           (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Or
            State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP) And
           claim.InvoiceProcessDate Is Nothing And
           claim.RepairDate Is Nothing _
            Then
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, False)
        End If


        If State.MyBO.Dealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
            'Make the ClaimCaseList button Visible
            ControlMgr.SetVisibleForControlFamily(Me, btnClaimCaseList, True, True)

        ElseIf State.MyBO.Dealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH) Then
            Dim dsCaseInfo As DataSet = New CaseBase().LoadCaseByClaimId(State.MyBO.Id)
            If (dsCaseInfo IsNot Nothing AndAlso dsCaseInfo.Tables.Count > 0 AndAlso dsCaseInfo.Tables(0).Rows.Count > 0) Then
                If (Not dsCaseInfo.Tables(0).Rows(0)("Case_id").Equals(Guid.Empty) And dsCaseInfo.Tables(0).Rows(0)("Case_Purpose_Code") = Codes.CASE_PURPOSE__REPORT_CLAIM) Then
                    'Make the ClaimCaseList button Visible when the claim recording is set to Both and if the claim is created from DCM
                    ControlMgr.SetVisibleForControlFamily(Me, btnClaimCaseList, True, True)
                Else
                    'Make the ClaimCaseList button hidden
                    ControlMgr.SetVisibleForControlFamily(Me, btnClaimCaseList, False, True)
                End If
            End If

        Else
            'Make the ClaimCaseList button hidden
            ControlMgr.SetVisibleForControlFamily(Me, btnClaimCaseList, False, True)
        End If


    End Sub

    Private Sub EnableDisableButtonsforMultiAuthCLaim()
        Dim claim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
        Dim strValidateSvcWty As String

        'For the Deny Claim button
        If ((Me.State.MyBO.Status = BasicClaimStatus.Active)) And
           (Not (State.MyBO.ClaimNumber.ToUpper.EndsWith("S"))) And
           (State.MyBO.TotalPaid.Value.Equals(0D)) And
           claim.HasNoReconsiledAuthorizations Then
            btnDenyClaim.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnDenyClaim, True)
        End If

        'For the ServiceWarranty button --check the flag at Company level
        If (State.MyBO.Company.AttributeValues.Contains(VALIDATE_SERVICE_WARRANTY)) Then
            strValidateSvcWty = State.MyBO.Company.AttributeValues.Value(VALIDATE_SERVICE_WARRANTY)
        End If

        If Not String.IsNullOrEmpty(strValidateSvcWty) And strValidateSvcWty = YES Then
            If Not State.MyBO.IsServiceWarrantyValid(State.MyBO.Id) Then
                btnServiceWarranty.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
            End If
        Else
            If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < State.MyBO.GetShortDate(State.MyBO.CreatedDate.Value))) OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT AndAlso State.MyBO.Dealer.DealerFulfillmentProviderClassCode <> Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS) OrElse
            (State.MyBO.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

                btnServiceWarranty.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
            End If
        End If


        'For the ReplaceItem button
        If ((State.MyBO.Status <> BasicClaimStatus.Active) OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            Not claim.HasActiveAuthorizations OrElse
            (State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make ReplaceItem button Invisible and Disabled
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        'Start - REQ- 6156 - Disable for all Multi Auth Claim
        If State.MyBO.Dealer.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS Then
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        'End - REQ- 6156 - Disable for all Multi Auth Claim

        If (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            'Make the PartsInfo button hidden
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, False, True)
        End If

        'Due to lack of space in the form, the Auth Detail button is placed next to shipping button. This is done since
        'BR is not using the shipping info today. Countries that uses shipping today do not use the auth detail feature.
        'Make the AuthDetail/PartsInfo button visible
        If State.AuthDetailEnabled AndAlso State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, False, True)
        Else
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, True, True)
        End If

        'Start - REQ- 6156 - Disable for all Multi Auth Claim
        'If claim.CanChangeCoverage Then
        '    ControlMgr.SetVisibleControl(Me, btnChangeCoverage, True)
        'Else
        '    ControlMgr.SetVisibleControl(Me, btnChangeCoverage, False)
        'End If
        btnChangeCoverage.Enabled = False
        ControlMgr.SetVisibleControl(Me, btnChangeCoverage, False)
        'End - REQ- 6156 - Disable for all Multi Auth Claim

        If State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And
           State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK And
           (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Or
            State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Or State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP) And
           claim.HasActiveAuthorizations _
            Then
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, False)
        End If

        If Not Me.State.MyBO.Status = BasicClaimStatus.Denied Then
            Dim isConsequentialDamageConfigured As Boolean = State.MyBO.isConsequentialDamageAllowed(State.MyBO.Certificate.Product.Id)
            If isConsequentialDamageConfigured Then
                ' Add Consequentail Damage button to be enabled only for Multi Auth Claim
                btnAddConseqDamage.Enabled = True
                ControlMgr.SetVisibleControl(Me, btnAddConseqDamage, True)
            End If
        End If

        'logic to enable the change fulfillment button
        If String.IsNullOrWhiteSpace(State.MyBO.CertificateItemCoverage.FulfillmentProfileCode) = False AndAlso State.MyBO.Status = BasicClaimStatus.Active Then
            btnChangeFulfillment.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnChangeFulfillment, True)
            'disable the replace item button if change fulfillmen button is enabled
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        'For Deductible refund button
        If (claim.IsDeductibleRefundAllowed AndAlso
            Not claim.IsDeductibleRefundExist) Then
            'Make Deductible refund enabled
            btnClaimDeductibleRefund.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnClaimDeductibleRefund, True)
        End If
    End Sub

    Private Sub EnableDisableButtonsConditionally()

        If (Not (State.IsEditMode)) Then

            If (Not State.IsMultiAuthClaim) Then
                EnableDisableButtonsForSingleAuthClaim()
            Else
                EnableDisableButtonsforMultiAuthCLaim()
            End If

            'For PoliceReport button - Invisible and Disabled when cause of loss is not Theft
            If Not (State.MyBO.CoverageTypeId.Equals(Guid.Empty)) Then
                If State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                   AndAlso State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__THEFT.ToUpper _
                   AndAlso (State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__LOSS.ToUpper _
                            OrElse (State.MyBO.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper _
                                    AndAlso (State.MyBO.Company.PoliceRptForLossCovId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)))) Then
                    btnPoliceReport.Enabled = False
                    ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
                Else
                    If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                        btnPoliceReport.Enabled = False
                        ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
                    End If
                End If
            Else
                btnPoliceReport.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
            End If

            If State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Then
                If State.MyBO.LossDate.Value >= State.MyBO.Certificate.WarrantySalesDate.Value AndAlso
                   State.MyBO.LossDate.Value < State.MyBO.CertificateItemCoverage.BeginDate.Value Then
                    'its a denied claim opened before the coverage effective date, so can not be reopened
                    ControlMgr.SetVisibleForControlFamily(Me, btnReopen_WRITE, False, True)
                Else
                    ControlMgr.SetVisibleForControlFamily(Me, btnReopen_WRITE, True, True)
                End If
            End If

            If State.MyBO.UseRecoveries = True Then
                btnUseRecoveries.Enabled = True
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, True)
            Else
                btnUseRecoveries.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, False)
            End If

            If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                btnUseRecoveries.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, False)
            End If
        End If

        If (State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            ControlMgr.SetVisibleForControlFamily(Me, btnPartsInfo, False, True)
        End If


        CheckRetailPriceSearchVisibility()

        'EnableDisableResendGiftcard()

    End Sub

    'REQ-6230
    Protected Sub CheckRetailPriceSearchVisibility()
        Try
            Dim retailPriceSearch As String

            'Check the flag at Company level
            If (State.MyBO.Dealer.AttributeValues.Contains(RETAIL_PRICE_SEARCH)) Then
                retailPriceSearch = State.MyBO.Dealer.AttributeValues.Value(RETAIL_PRICE_SEARCH)
            End If

            If (Not String.IsNullOrEmpty(retailPriceSearch) And retailPriceSearch = YES) Then
                ControlMgr.SetVisibleForControlFamily(Me, btnPriceRetailSearch, True, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ActionButtonOptionEnableDisableVisible(isFlag As Boolean)
        btnDenyClaim.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnDenyClaim, isFlag)
        btnAuthDetail.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnAuthDetail, isFlag)
        btnChangeCoverage.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnChangeCoverage, isFlag)
        btnClaimHistoryDetails.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnClaimHistoryDetails, isFlag)
        btnOutboundCommHistory.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnOutboundCommHistory, isFlag)
        btnCertificate.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnCertificate, isFlag)
        btnComment.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnComment, isFlag)
        btnServiceCenterInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnServiceCenterInfo, isFlag)

        btnClaimDeniedInformation.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, isFlag)
        btnStatusDetail.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnStatusDetail, isFlag)
        btnItem.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnItem, isFlag)
        btnMasterClaim.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnMasterClaim, isFlag)
        btnNewServiceCenter.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnNewServiceCenter, isFlag)
        btnNewItemInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnNewItemInfo, isFlag)
        btnPoliceReport.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnPoliceReport, isFlag)
        btnPartsInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnPartsInfo, isFlag)

        btnPrint.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnPrint, isFlag)
        btnUseRecoveries.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnUseRecoveries, isFlag)
        btnRedo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnRedo, isFlag)
        btnReplaceItem.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnReplaceItem, isFlag)
        btnReopen_WRITE.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnReopen_WRITE, isFlag)
        btnShipping.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnShipping, isFlag)
        btnServiceWarranty.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnServiceWarranty, isFlag)
        btnRepairLogistics.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnRepairLogistics, isFlag)

        btnClaimIssues.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnClaimIssues, isFlag)
        btnClaimImages.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnClaimImages, isFlag)
        btnClaimCaseList.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnClaimCaseList, isFlag)
        btnAddConseqDamage.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnAddConseqDamage, isFlag)
        btnPriceRetailSearch.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnPriceRetailSearch, isFlag)

        btnChangeFulfillment.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnChangeFulfillment, isFlag)

        btnReplacementQuote.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, btnReplacementQuote, isFlag)
    End Sub
    Protected Sub DisableButtonsForClaimSystem()
        If Not State.MyBO.CertificateId.Equals(Guid.Empty) Or State.MyBO.IsClaimChild = Codes.YESNO_Y Then
            Dim oClmSystem As New ClaimSystem(State.MyBO.Dealer.ClaimSystemId)
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If oClmSystem.MaintainClaimId.Equals(noId) Or State.MyBO.IsClaimChild = Codes.YESNO_Y Then
                If ActionButton.Visible And ActionButton.Enabled Then
                    ControlMgr.SetEnableControl(Me, ActionButton, False)
                End If
                If btnEdit_WRITE.Visible And btnEdit_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                End If
                If btnSave_WRITE.Visible And btnSave_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                End If
                If btnUndo_WRITE.Visible And btnUndo_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                End If
            End If
        End If
    End Sub
    Protected Sub DisableButtonsForReadonlyClaim()
        If State.MyBO.IsClaimReadOnly = Codes.YESNO_Y Then
            If ActionButton.Visible And ActionButton.Enabled Then
                If btnCertificate.Visible And btnCertificate.Enabled Then
                    ActionButtonOptionEnableDisableVisible(False)
                    btnCertificate.Enabled = True
                    ControlMgr.SetVisibleControl(Me, btnCertificate, True)
                Else
                    ActionButtonOptionEnableDisableVisible(False)
                End If
            End If
            If btnEdit_WRITE.Visible And btnEdit_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
            End If
            If btnSave_WRITE.Visible And btnSave_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            End If
            If btnUndo_WRITE.Visible And btnUndo_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub HandleCloseClaimLogic()
        'Try to Close the Claim
        Select Case State.MyBO.ClaimAuthorizationType
            Case ClaimAuthorizationType.Single
                CType(State.MyBO, Claim).CloseTheClaim()
            Case ClaimAuthorizationType.Multiple
                Dim claim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
                If Not (claim.Status = BasicClaimStatus.Closed Or claim.Status = BasicClaimStatus.Denied) Then
                    If Not claim.ReasonClosedId.Equals(Guid.Empty) Then
                        If claim.ClaimAuthorizationChildren.Where(Function(item) item.ClaimAuthStatus = ClaimAuthorizationStatus.Paid Or
                                                                                 item.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid Or
                                                                                 item.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled).Count > 0 Then
                            Throw New GUIException("CLAIM_CANNOT_BE_CLOSED_CONTAINS_RECONSILED_PAID_AUTH", "CLAIM_CANNOT_BE_CLOSED_CONTAINS_RECONSILED_PAID_AUTH")
                        End If
                    End If

                End If
                claim.CloseTheClaim()
                If claim.Status = BasicClaimStatus.Closed Then
                    claim.VoidAuthorizations()
                End If
            Case ClaimAuthorizationType.None
                Throw New NotSupportedException
        End Select

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "CertificateNumber", LabelCertificateNumber)
        BindBOPropertyToLabel(State.MyBO, "UserName", LabelUserName)
        BindBOPropertyToLabel(State.MyBO, "DealerName", LabelDealerName)
        BindBOPropertyToLabel(State.MyBO, "RiskTypeId", LabelRiskType)
        BindBOPropertyToLabel(State.MyBO, "ClaimActivityId", LabelClaimActivity)
        BindBOPropertyToLabel(State.MyBO, "ReasonClosedId", LabelReasonClosed)
        BindBOPropertyToLabel(State.MyBO, "DeniedReasonId", LabelDeniedReason)
        BindBOPropertyToLabel(State.MyBO, "RepairCodeId", LabelRepairCode)
        BindBOPropertyToLabel(State.MyBO, "CauseOfLossId", LabelCauseOfLoss)
        BindBOPropertyToLabel(State.MyBO, "MethodOfRepairId", LabelMethodOfRepair)
        BindBOPropertyToLabel(State.MyBO, "CoverageTypeDescription", LabelCoverageType)
        BindBOPropertyToLabel(State.MyBO, "SpecialInstruction", LabelSpecialInstruction)
        BindBOPropertyToLabel(State.MyBO, "LoanerRquestedXcd", LabelLoanerRequested)
        BindBOPropertyToLabel(State.MyBO, "AuthorizedAmount", LabelAuthorizedAmount)
        BindBOPropertyToLabel(State.MyBO, "LiabilityLimit", LabelLiabilityLimit)
        BindBOPropertyToLabel(State.MyBO, "Deductible", LabelDeductible)
        BindBOPropertyToLabel(State.MyBO, "TotalPaid", LabelTotalPaid)
        BindBOPropertyToLabel(State.MyBO, "AssurantPays", LabelAssurantPays)
        BindBOPropertyToLabel(State.MyBO, "DueToSCFromAssurant", LabelDueToSCFromAssurant)
        BindBOPropertyToLabel(State.MyBO, "ConsumerPays", LabelConsumerPays)
        BindBOPropertyToLabel(State.MyBO, "AboveLiability", LabelAboveLiability)
        BindBOPropertyToLabel(State.MyBO, "ClaimsAdjusterName", LabelClaimsAdjuster)
        BindBOPropertyToLabel(State.MyBO, "ClaimClosedDate", LabelClaimClosedDate)
        BindBOPropertyToLabel(State.MyBO, "FollowupDate", LabelFollowupDate)
        BindBOPropertyToLabel(State.MyBO, "AuthorizationNumber", LabelAuthorizationNumber)
        BindBOPropertyToLabel(State.MyBO, "Source", LabelSource)
        BindBOPropertyToLabel(State.MyBO, "AddedDate", LabelAddedDate)
        BindBOPropertyToLabel(State.MyBO, "LastOperatorName", LabelLastModifiedBy)
        BindBOPropertyToLabel(State.MyBO, "LastModifiedDate", LabelLastModifiedDate)
        BindBOPropertyToLabel(State.MyBO, "MasterClaimNumber", LabelMasterClaimNumber)
        BindBOPropertyToLabel(State.MyBO, "CallerTaxNumber", LabelCaller_Tax_Number)
        BindBOPropertyToLabel(State.MyBO, "DiscountAmount", LabelDiscount)
        BindBOPropertyToLabel(State.MyBO, "PolicyNumber", LabelPolicyNumber)
        BindBOPropertyToLabel(State.MyBO, "StoreNumber", LabelStoreNumber)
        BindBOPropertyToLabel(State.MyBO, "NotificationTypeDescription", LabelNotificationType)
        BindBOPropertyToLabel(State.MyBO, "DedCollectionMethodID", LabelDedCollMethod)
        BindBOPropertyToLabel(State.MyBO, "DedCollectionCCAuthCode", LabelCCAuthCode)
        BindBOPropertyToLabel(State.MyBO, "IsLawsuitId", LabelIsLawsuitId)
        BindBOPropertyToLabel(State.MyBO, "ProblemDescription", LabelProblemDescription)
        BindBOPropertyToLabel(State.MyBO, "BonusAmount", LabelBonusAmount)
        BindBOPropertyToLabel(State.MyBO, "DeductibleCollected", LabelDeductibleCollected)
        '5921
        BindBOPropertyToLabel(State.MyBO, "TrackingNumber", LabelTrackingNumber)
        BindBOPropertyToLabel(State.MyBO, "FulfilmentMethod", LabelFulfilmentMethod)
        BindBOPropertyToLabel(State.MyBO, "AccountNumber", LabelAccountNumber)

        BindBOPropertyToLabel(State.MyBO, "EmployeeNumber", labelEMPLOYEE_NUMBER)
        BindBOPropertyToLabel(State.MyBO, "DeviceActivationDate", LabelDEVICE_ACTIVATION_DATE)

        If State.MyBO.EnrolledEquipment IsNot Nothing Then
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "Manufacturer", LBLeNROLLEDmAKE)
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "Model", lblEnrolledModel)
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "SerialNumber", lblEnrolledSerialNumber)
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "SKU", lblEnrolledSKu)
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "IMEINumber", lblEnrolledIMEI)
            BindBOPropertyToLabel(State.MyBO.EnrolledEquipment, "Comments", lblEnrolledIMEI)
        End If

        If State.MyBO.ClaimedEquipment IsNot Nothing Then
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "Manufacturer", lblClaimedMake)
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "Model", lblClaimedModel)
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "SerialNumber", lblClaimedSerialNumber)
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "SKU", lblClaimedSKu)
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "IMEINumber", lblClaimedIMEI)
            BindBOPropertyToLabel(State.MyBO.ClaimedEquipment, "Comments", lblClaimedIMEI)
        End If


        If Not State.IsMultiAuthClaim Then
            Dim claimBo As Claim = CType(State.MyBO, Claim)
            BindBOPropertyToLabel(claimBo, "ServiceCenterId", LabelServiceCenter)
            BindBOPropertyToLabel(claimBo, "LoanerCenterId", LabelLoanerCenter)
            BindBOPropertyToLabel(claimBo, "RepairDate", LabelRepairDate)
            BindBOPropertyToLabel(claimBo, "InvoiceProcessDate", LabelInvoiceProcessDate)
            BindBOPropertyToLabel(claimBo, "InvoiceDate", LabelInvoiceDate)
            BindBOPropertyToLabel(claimBo, "LoanerReturnedDate", LabelLoanerReturnedDate)
            BindBOPropertyToLabel(claimBo, "VisitDate", LabelVisitDate)
            BindBOPropertyToLabel(claimBo, "PickUpDate", LabelPickUpDate)
            BindBOPropertyToLabel(claimBo, "WhoPaysId", LabelWhoPays)
            BindBOPropertyToLabel(claimBo, "DefectReason", LabelDefectReason)
            BindBOPropertyToLabel(claimBo, "TechnicalReport", LabelTechnicalReport)
            BindBOPropertyToLabel(claimBo, "ExpectedRepairDate", LabelExpectedRepairDate)
            BindBOPropertyToLabel(claimBo, "ClaimSpecialServiceId", LabelSpecialService)
            BindBOPropertyToLabel(claimBo, "BatchNumber", LabelBatchNumber)
        End If
        ClearGridHeadersAndLabelsErrSign()

        If State.IsTEMP_SVC Then
            LabelServiceCenter.ForeColor = System.Drawing.ColorTranslator.FromHtml("red") 'Color.Red
        End If


    End Sub

    'Function GetPrice() As Decimal
    '    Dim equipConditionid As Guid
    '    Dim equipmentId As Guid
    '    Dim equipClassId As Guid
    '    Dim price As Decimal = 0
    '    Dim dv As DataView
    '    Dim servCenter As ServiceCenter

    '    'Dim claim As Claim = CType(Me.State.MyBO, Claim)
    '    'Dim claim As Claim = If(Me.State.IsMultiAuthClaim, CType(Me.State.MyBO, MultiAuthClaim).ServiceCenterObject, CType(Me.State.MyBO, Claim))

    '    'Dim servCenter As New ServiceCenter(claim.ServiceCenterObject.Code)
    '    If (Not Me.State.MyBO.ServiceCenterId = Guid.Empty) Then
    '        servCenter = New ServiceCenter(Me.State.MyBO.ServiceCenterId)
    '    Else
    '        Return 0
    '    End If

    '    If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.MyBO.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
    '        equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'

    '        If Not State.MyBO.ClaimedEquipment Is Nothing AndAlso Not State.MyBO.ClaimedEquipment.EquipmentBO Is Nothing Then
    '            equipmentId = State.MyBO.ClaimedEquipment.EquipmentBO.Id
    '            equipClassId = State.MyBO.ClaimedEquipment.EquipmentBO.EquipmentClassId

    '        ElseIf Not State.MyBO.ClaimedEquipment Is Nothing Then
    '            equipmentId = Equipment.FindEquipment(State.MyBO.Dealer.Dealer, State.MyBO.ClaimedEquipment.Manufacturer, State.MyBO.ClaimedEquipment.Model, Date.Today)
    '            'equipmentId = New Guid("7a8c8b43-efdc-484e-b19d-c1abef1696a6")
    '            If (Not equipmentId = Guid.Empty) Then
    '                equipClassId = New Equipment(equipmentId).EquipmentClassId
    '            End If
    '        End If
    '    End If

    '    dv = PriceListDetail.GetRepairPricesforMethodofRepair(State.MyBO.MethodOfRepairId, Me.State.MyBO.CompanyId, servCenter.Code, Me.State.MyBO.RiskTypeId,
    '                        DateTime.Now, Me.State.MyBO.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.State.MyBO.Dealer.Id, String.Empty)

    '    If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
    '        price = CDec(dv.Table.Rows(0)(COL_PRICE_DV))
    '    End If

    '    Return price
    'End Function

    Sub PopulateAuthorizedAmountFromPGPrices()
        Dim claim As Claim = CType(State.MyBO, Claim)

        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Dim dTaxRate As Decimal
        Dim nCarryInPrice As New DecimalType(0)
        Dim nCleaningPrice As New DecimalType(0)
        Dim nEstimatePrice As New DecimalType(0)
        Dim nHomePrice As New DecimalType(0)
        Dim nOtherPrice As New DecimalType(0)
        Dim nReplacementCost As New DecimalType(0)
        Dim nReplacementPrice As New DecimalType(0)
        Dim nZeroValue As New DecimalType(0)
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        Dim splSvcPriceGrp As String

        'get the equipment information
        'Get the price
        Dim dv As DataView
        Dim servCenter As New ServiceCenter(claim.ServiceCenterObject.Code)
        Dim equipConditionid As Guid
        Dim equipmentId As Guid
        Dim equipClassId As Guid

        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If State.MyBO.ClaimedEquipment Is Nothing Then
                Throw New GUIException(Codes.EQUIPMENT_NOT_FOUND, Codes.EQUIPMENT_NOT_FOUND)
            Else
                If State.MyBO.ClaimedEquipment.EquipmentBO Is Nothing Then
                    Throw New GUIException(Codes.EQUIPMENT_NOT_FOUND, Codes.EQUIPMENT_NOT_FOUND)
                End If
            End If

            equipmentId = State.MyBO.ClaimedEquipment.EquipmentBO.Id
            equipClassId = State.MyBO.ClaimedEquipment.EquipmentBO.EquipmentClassId
        End If

        'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.MyBO.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
        '    equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'

        '    If Not State.MyBO.ClaimedEquipment Is Nothing AndAlso Not State.MyBO.ClaimedEquipment.EquipmentBO Is Nothing Then
        '        equipmentId = State.MyBO.ClaimedEquipment.EquipmentBO.Id
        '        equipClassId = State.MyBO.ClaimedEquipment.EquipmentBO.EquipmentClassId

        '    ElseIf Not State.MyBO.ClaimedEquipment Is Nothing Then
        '        equipmentId = Equipment.FindEquipment(State.MyBO.Dealer.Dealer, State.MyBO.ClaimedEquipment.Manufacturer, State.MyBO.ClaimedEquipment.Model, Date.Today)
        '        If (Not equipmentId = Guid.Empty) Then
        '            equipClassId = New Equipment(equipmentId).EquipmentClassId
        '        End If

        '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
        '        Throw New GUIException(Codes.EQUIPMENT_NOT_FOUND, Codes.EQUIPMENT_NOT_FOUND)
        '    End If
        'End If

        Dim price As Decimal = 0
        dv = PriceListDetail.GetRepairPricesforMethodofRepair(State.MyBO.MethodOfRepairId, State.MyBO.CompanyId, servCenter.Code, State.MyBO.RiskTypeId,
                                                              DateTime.Now, State.MyBO.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, State.MyBO.Dealer.Id, String.Empty)

        If dv IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then
            price = CDec(dv.Table.Rows(0)(COL_PRICE_DV))
            Select Case dv.Table.Rows(0)(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE).ToString()
                Case Codes.METHOD_OF_REPAIR_SEND_IN
                    nCarryInPrice = price
                Case Codes.METHOD_OF_REPAIR_PICK_UP
                    nCarryInPrice = price
                Case Codes.METHOD_OF_REPAIR_CLEANING
                    nCleaningPrice = price
                Case Codes.METHOD_OF_REPAIR_AT_HOME
                    nHomePrice = price
                Case Codes.METHOD_OF_REPAIR_DISCOUNTED
                    nReplacementCost = price ' CDec(dv.Table.Rows(0)(COL_PRICE_DV))
                Case Codes.METHOD_OF_REPAIR_REPLACEMENT
                    nReplacementPrice = price
            End Select
        End If

        'calculating the estimate price
        Dim dvEstimate As DataView = PriceListDetail.GetPricesForServiceType(State.MyBO.CompanyId, servCenter.Code, State.MyBO.RiskTypeId,
                                                                             DateTime.Now, State.MyBO.Certificate.SalesPrice.Value,
                                                                             LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR),
                                                                             LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, Codes.SERVICE_TYPE__ESTIMATE_PRICE), equipClassId, equipmentId,
                                                                             equipConditionid, State.MyBO.Dealer.Id, String.Empty)
        price = 0
        If dvEstimate IsNot Nothing AndAlso dvEstimate.Count > 0 Then
            price = CDec(dvEstimate(0)(COL_PRICE_DV))
            nEstimatePrice = price ' CDec(dvEstimate.Table.Rows(0)(COL_PRICE_DV))
        End If

        With claim
            If claim.ClaimSpecialServiceId = yesId Then
                splSvcPriceGrp = claim.SpecialServiceServiceType
                If splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__CARRY_IN_PRICE Then 'carry in price  
                    PopulateControlFromBOProperty(TextboxAuthorizedAmount, nCarryInPrice)
                ElseIf splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__CLEANING_PRICE Then 'cleaning price
                    PopulateControlFromBOProperty(TextboxAuthorizedAmount, nCleaningPrice)
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_ESTIMATE Then 'estimate price
                    PopulateControlFromBOProperty(TextboxAuthorizedAmount, nEstimatePrice)
                ElseIf splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__HOME_PRICE Then 'home price
                    PopulateControlFromBOProperty(TextboxAuthorizedAmount, nHomePrice)
                Else 'Manual
                    PopulateControlFromBOProperty(TextboxAuthorizedAmount, .AuthorizedAmount)
                End If

                PopulateControlFromBOProperty(TextboxAuthorizedAmount, .AuthorizedAmount)
                .Deductible = nZeroValue

                PopulateControlFromBOProperty(TextboxDeductible, .Deductible)
                PopulateControlFromBOProperty(TextboxSlavageAmount, .SalvageAmount)
                PopulateControlFromBOProperty(TextboxAssurantPays, .AssurantPays)
                PopulateControlFromBOProperty(TextboxConsumerPays, .ConsumerPays)
                PopulateControlFromBOProperty(TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
                PopulateControlFromBOProperty(TextboxBonusAmount, .BonusAmount)
                SetSelectedItem(cboWhoPays, AssurantPays)

            Else

                .AuthorizedAmount = State.PrevAuthorizedAmt
                .Deductible = State.PrevDeductible
                PopulateControlFromBOProperty(TextboxDeductible, .Deductible)
                PopulateControlFromBOProperty(TextboxAuthorizedAmount, .AuthorizedAmount)
                PopulateControlFromBOProperty(TextboxSlavageAmount, .SalvageAmount)
                PopulateControlFromBOProperty(TextboxAssurantPays, .AssurantPays)
                PopulateControlFromBOProperty(TextboxConsumerPays, .ConsumerPays)
                PopulateControlFromBOProperty(TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
                PopulateControlFromBOProperty(TextboxBonusAmount, .BonusAmount)
                SetSelectedItem(cboWhoPays, .WhoPaysId)

            End If

            If ((CType(TextboxAuthorizedAmount.Text, Decimal) > State.MyBO.AuthorizationLimit.Value) AndAlso
                (CType(TextboxAuthorizedAmount.Text, Decimal) > State.MyBO.AuthorizationLimit.Value)) Then
                moMessageController.Clear()
                moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

            End If

        End With
    End Sub

    Protected Sub PopulateDropdowns()

        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        Dim lstResonsClosed As ListItem() = CommonConfigManager.Current.ListManager.GetList("RESCL", Thread.CurrentPrincipal.GetLanguageCode())
        cboReasonClosed.Populate(lstResonsClosed, New PopulateOptions() With
                                       {
                                       .AddBlankItem = True
                                       })

        'KDDI CHANGES
        'Dim listcontextForMgList As ListContext = New ListContext()
        'listcontextForMgList.CompanyGroupId = Me.State.MyBO.Company.CompanyGroupId
        'listcontextForMgList.DealerId = Me.State.MyBO.Dealer.Id
        'listcontextForMgList.CompanyId = Me.State.MyBO.CompanyId
        'listcontextForMgList.DealerGroupId = Me.State.MyBO.Dealer.DealerGroupId

        Dim lstDeniedReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("DNDREASON", Thread.CurrentPrincipal.GetLanguageCode()) ', listcontextForMgList)
        cboDeniedReason.Populate(lstDeniedReason, New PopulateOptions() With
                                       {
                                       .AddBlankItem = True
                                       })

        'If Not (lstDeniedReason Is Nothing) Then

        '    If Not lstDeniedReason.Length > 0 Then
        '        lstDeniedReason = CommonConfigManager.Current.ListManager.GetList("DNDREASON", Thread.CurrentPrincipal.GetLanguageCode())
        '    End If
        '    Me.cboDeniedReason.Populate(lstDeniedReason, New PopulateOptions() With
        '                                {
        '                                  .AddBlankItem = True
        '                                 })

        'End If

        Dim listcontextForCauseOfLoss As ListContext = New ListContext()
        listcontextForCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        listcontextForCauseOfLoss.CoverageTypeId = State.MyBO.CoverageTypeId
        listcontextForCauseOfLoss.DealerId = State.MyBO.Certificate.DealerId
        listcontextForCauseOfLoss.ProductCode = State.MyBO.Certificate.ProductCode
        listcontextForCauseOfLoss.LanguageId = Authentication.LangId


        Dim listCauseOfLoss As ListItem() = CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss)
        cboCauseOfLossId.Populate(listCauseOfLoss, New PopulateOptions() With
                                        {
                                        .AddBlankItem = True
                                        })

        cbousershipaddress.Populate(yesNoLkL, New PopulateOptions() With
                                       {
                                       .AddBlankItem = False
                                       })

        cboLawsuitId.Populate(yesNoLkL, New PopulateOptions() With
                                 {
                                 .AddBlankItem = True
                                 })


        Dim listFulfillmentMethod As ListItem() = CommonConfigManager.Current.ListManager.GetList("FULFILMETH", Thread.CurrentPrincipal.GetLanguageCode())
        cboFulfilmentMethod.Populate(listFulfillmentMethod, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True,
                                           .TextFunc = AddressOf .GetDescription,
                                           .ValueFunc = AddressOf .GetExtendedCode
                                           })

        If (Not State.IsMultiAuthClaim) Then
            Dim lstWhoPays As ListItem() = CommonConfigManager.Current.ListManager.GetList("WPAYS", Thread.CurrentPrincipal.GetLanguageCode())
            cboWhoPays.Populate(lstWhoPays, New PopulateOptions() With
                                      {
                                      .AddBlankItem = False
                                      })


        End If
    End Sub
    Sub PopulateRefurbReplaceClaimEquipment()

        Try
            'refurbished replacement equipment
            ChangeEnabledProperty(txtRefurbReplaceMake, False)
            ChangeEnabledProperty(txtRefurbReplaceModel, False)
            ChangeEnabledProperty(txtRefurbReplaceSerial, False)
            ChangeEnabledProperty(txtRefurbReplaceSku, False)
            ChangeEnabledProperty(txtRefurbReplaceIMEI, False)
            ChangeEnabledProperty(txtRefurbReplaceComments, False)

            'If (Me.State.ClaimEquipmentBO Is Nothing) Then
            State.ClaimEquipmentBO = ClaimEquipment.GetLatestClaimEquipmentInfo(State.MyBO.Id, LookupListNew.GetIdFromCode("CLAIM_EQUIP_TYPE", "RR"))
            If State.ClaimEquipmentBO IsNot Nothing Then

                State.RefurbReplaceClaimEquipmentId = New Guid(CType(State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_CLAIM_EQUIPMENT_ID), Byte()))

                PopulateControlFromBOProperty(txtRefurbReplaceMake, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_MAKE).ToString())
                PopulateControlFromBOProperty(txtRefurbReplaceModel, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_MODEL).ToString())
                PopulateControlFromBOProperty(txtRefurbReplaceSerial, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_SERIAL_NUMBER).ToString())
                PopulateControlFromBOProperty(txtRefurbReplaceSku, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_SKU).ToString())
                PopulateControlFromBOProperty(txtRefurbReplaceIMEI, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_IMEI_NUMBER).ToString())
                PopulateControlFromBOProperty(txtRefurbReplaceComments, State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_COMMENTS).ToString())
            End If

        Catch ex As Exception
            PopulateControlFromBOProperty(txtRefurbReplaceMake, "")
            PopulateControlFromBOProperty(txtRefurbReplaceModel, "")
            PopulateControlFromBOProperty(txtRefurbReplaceSerial, "")
            PopulateControlFromBOProperty(txtRefurbReplaceSku, "")
            PopulateControlFromBOProperty(txtRefurbReplaceIMEI, "")
            PopulateControlFromBOProperty(txtRefurbReplaceComments, "")
        End Try
    End Sub
    Sub PopulateClaimEquipment()
        With State.MyBO
            'enrolled equipment
            ChangeEnabledProperty(txtEnrolledMake, False)
            ChangeEnabledProperty(txtEnrolledModel, False)
            ChangeEnabledProperty(txtenrolledSerial, False)
            ChangeEnabledProperty(txtEnrolledSku, False)
            ChangeEnabledProperty(txtEnrolledIMEI, False)
            ChangeEnabledProperty(txtEnrolledComments, False)
            If .EnrolledEquipment IsNot Nothing Then
                PopulateControlFromBOProperty(txtEnrolledMake, .EnrolledEquipment.Manufacturer)
                PopulateControlFromBOProperty(txtEnrolledModel, .EnrolledEquipment.Model)
                PopulateControlFromBOProperty(txtenrolledSerial, .EnrolledEquipment.SerialNumber)
                PopulateControlFromBOProperty(txtEnrolledSku, .EnrolledEquipment.SKU)
                PopulateControlFromBOProperty(txtEnrolledIMEI, .EnrolledEquipment.IMEINumber)
                PopulateControlFromBOProperty(txtEnrolledComments, .EnrolledEquipment.Comments)
            End If
            'claimed equipment
            ChangeEnabledProperty(txtClaimedMake, False)
            ChangeEnabledProperty(txtClaimedModel, False)
            ChangeEnabledProperty(txtClaimedSerial, False)
            ChangeEnabledProperty(txtClaimedSku, False)
            ChangeEnabledProperty(txtClaimedIMEI, False)
            ChangeEnabledProperty(txtClaimedComments, False)
            If .ClaimedEquipment IsNot Nothing Then
                PopulateControlFromBOProperty(txtClaimedMake, .ClaimedEquipment.Manufacturer)
                PopulateControlFromBOProperty(txtClaimedModel, .ClaimedEquipment.Model)
                PopulateControlFromBOProperty(txtClaimedSerial, .ClaimedEquipment.SerialNumber)
                PopulateControlFromBOProperty(txtClaimedSku, .ClaimedEquipment.SKU)
                PopulateControlFromBOProperty(txtClaimedIMEI, .ClaimedEquipment.IMEINumber)
                PopulateControlFromBOProperty(txtClaimedComments, .ClaimedEquipment.Comments)
            End If

        End With
    End Sub

    Sub PopulateClaimShipping()
        Try
            ChangeEnabledProperty(txtShipperToSC, False)
            ChangeEnabledProperty(txtShipperToCust, False)
            ChangeEnabledProperty(txtShipToSC, False)
            ChangeEnabledProperty(txtShipToCust, False)

            State.ClaimShippingBO = ClaimShipping.GetLatestClaimShippingInfo(State.MyBO.Id, LookupListNew.GetIdFromCode("SHIPPING_TYPES", "SHIP_TO_SC"))
            If State.ClaimShippingBO IsNot Nothing AndAlso State.ClaimShippingBO.Table.Rows.Count > 0 Then
                State.InboundClaimShippingId = New Guid(CType(State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID), Byte()))
                PopulateControlFromBOProperty(txtShipperToSC, State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CARRIER_NAME).ToString())
                PopulateControlFromBOProperty(txtShipToSC, State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_TRACKING_NUMBER).ToString())
            End If
            State.ClaimShippingBO = ClaimShipping.GetLatestClaimShippingInfo(State.MyBO.Id, LookupListNew.GetIdFromCode("SHIPPING_TYPES", "SHIP_TO_CUST"))
            If State.ClaimShippingBO IsNot Nothing AndAlso State.ClaimShippingBO.Table.Rows.Count > 0 Then
                State.OutboundClaimShippingId = New Guid(CType(State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID), Byte()))
                PopulateControlFromBOProperty(txtShipperToCust, State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CARRIER_NAME).ToString())
                PopulateControlFromBOProperty(txtShipToCust, State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_TRACKING_NUMBER).ToString())
            End If
        Catch ex As Exception
            PopulateControlFromBOProperty(txtShipperToSC, "")
            PopulateControlFromBOProperty(txtShipperToCust, "")
            PopulateControlFromBOProperty(txtShipToSC, "")
            PopulateControlFromBOProperty(txtShipToCust, "")
        End Try
    End Sub

    Sub PopulateClaimFulfillmentDetails()
        If State.FulfillmentDetailsResponse IsNot Nothing AndAlso
            State.FulfillmentDetailsResponse.LogisticStages IsNot Nothing AndAlso
            State.FulfillmentDetailsResponse.LogisticStages.Length > 0 Then

            Dim logisticStage As SelectedLogisticStage = State.FulfillmentDetailsResponse.LogisticStages.Where(Function(item) item.Code = Codes.FULFILLMENT_FW_LOGISTIC_STAGE).First()

            If logisticStage IsNot Nothing AndAlso logisticStage.Code = Codes.FULFILLMENT_FW_LOGISTIC_STAGE Then

                PopulateControlFromBOProperty(txtOptionDescription, logisticStage.OptionDescription)

                PopulateControlFromBOProperty(txtExpectedDeliveryDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ExpectedDeliveryDate, DATE_TIME_FORMAT))
                PopulateControlFromBOProperty(txtActualDeliveryDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ActualDeliveryDate, DATE_TIME_FORMAT))
                PopulateControlFromBOProperty(txtShippingDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ShippingDate, DATE_TIME_FORMAT))
                PopulateControlFromBOProperty(txtExpectedShippingDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ExpectedShippingDate, DATE_TIME_FORMAT))
                PopulateControlFromBOProperty(txtTrackingNumber, logisticStage.Shipping.TrackingNumber)

                PopulateControlFromBOProperty(txtAddress1, logisticStage.Address.Address1)
                PopulateControlFromBOProperty(txtAddress2, logisticStage.Address.Address2)
                PopulateControlFromBOProperty(txtAddress3, logisticStage.Address.Address3)
                PopulateControlFromBOProperty(txtCity, logisticStage.Address.City)
                PopulateControlFromBOProperty(txtServiceCenterCode, $"{logisticStage.ServiceCenterCode}")
                PopulateControlFromBOProperty(txtServiceCenter, $"{logisticStage.ServiceCenterDescription}")
                PopulateControlFromBOProperty(txtPostalCode, logisticStage.Address.PostalCode)
                PopulateControlFromBOProperty(txtState, LookupListNew.GetDescriptionFromCode(
                                                 LookupListNew.DataView(LookupListNew.LK_REGIONS),
                                                 logisticStage.Address.State))
                PopulateControlFromBOProperty(txtCountry, LookupListNew.GetDescriptionFromCode(
                                                 LookupListNew.DataView(LookupListNew.LK_COUNTRIES),
                                                 logisticStage.Address.Country))

                PopulateControlFromBOProperty(txtStoreCode, logisticStage.HandlingStore.StoreCode)
                PopulateControlFromBOProperty(txtStoreName, logisticStage.HandlingStore.StoreName)

                Dim storeTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList(Codes.HND_STORE_TYPE, Thread.CurrentPrincipal.GetLanguageCode())
                Dim storeTypeItem = storeTypeList.Where(
                    Function(item) item.ExtendedCode = logisticStage.HandlingStore.StoreTypeXcd).FirstOrDefault()
                PopulateControlFromBOProperty(txtStoreType, storeTypeItem.Translation)


                If logisticStage.Shipping.TrackingNumber IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(logisticStage.Shipping.TrackingNumber.ToString()) AndAlso
                    Not String.IsNullOrEmpty(storeTypeItem.Translation) Then
                    Dim PasscodeResponse = GetPasscode(logisticStage.Shipping.TrackingNumber.ToString())
                    PopulateControlFromBOProperty(txtPasscode, PasscodeResponse)
                Else
                    PopulateControlFromBOProperty(txtPasscode, "")
                End If
                dvClaimFulfillmentDetails.Visible = True
            End If
        Else
            ClearClaimFulfillmentDetails()
        End If
    End Sub

    Sub ClearClaimFulfillmentDetails()
        PopulateControlFromBOProperty(txtOptionDescription, "")
        PopulateControlFromBOProperty(txtExpectedDeliveryDate, "")
        PopulateControlFromBOProperty(txtActualDeliveryDate, "")
        PopulateControlFromBOProperty(txtShippingDate, "")
        PopulateControlFromBOProperty(txtExpectedShippingDate, "")
        PopulateControlFromBOProperty(txtTrackingNumber, "")
        PopulateControlFromBOProperty(txtAddress1, "")
        PopulateControlFromBOProperty(txtAddress2, "")
        PopulateControlFromBOProperty(txtAddress3, "")
        PopulateControlFromBOProperty(txtCity, "")
        PopulateControlFromBOProperty(txtState, "")
        PopulateControlFromBOProperty(txtCountry, "")
        PopulateControlFromBOProperty(txtPostalCode, "")
        PopulateControlFromBOProperty(txtStoreCode, "")
        PopulateControlFromBOProperty(txtStoreName, "")
        PopulateControlFromBOProperty(txtStoreType, "")
    End Sub


    Protected Sub PopulateFormFromBOs()

        moClaimInfoController = moClaimInfoController
        moClaimInfoController.InitController(State.MyBO)

        SetSelectedItem(cboReasonClosed, State.MyBO.ReasonClosedId)
        SetSelectedItem(cboCauseOfLossId, State.MyBO.CauseOfLossId)
        SetSelectedItem(cboLawsuitId, State.MyBO.IsLawsuitId)
        If Not State.MyBO.DeniedReasonId.Equals(Guid.Empty) Then SetSelectedItem(cboDeniedReason, State.MyBO.DeniedReasonId)

        If Not State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
            UserControlAddress.ClaimDetailsBind(State.MyBO.ContactInfo.Address)
            UserControlContactInfo.Bind(State.MyBO.ContactInfo)
        End If

        PopulateControlFromBOProperty(TextboxCertificateNumber, State.MyBO.CertificateNumber)
        PopulateControlFromBOProperty(TextboxUserName, State.MyBO.UserName)
        PopulateControlFromBOProperty(TextboxDealerName, State.MyBO.DealerName)
        PopulateControlFromBOProperty(TextboxRiskType, State.MyBO.RiskType)
        PopulateControlFromBOProperty(TextboxClaimActivity, State.MyBO.ClaimActivityDescription)
        PopulateControlFromBOProperty(TextboxCoverageType, State.MyBO.CoverageTypeDescription)
        PopulateControlFromBOProperty(TextboxNotificationType, State.MyBO.NotificationTypeDescription)
        PopulateControlFromBOProperty(TextboxMethodOfRepair, State.MyBO.MethodOfRepairDescription)
        PopulateControlFromBOProperty(TextboxClaimNumber, State.MyBO.ClaimNumber)
        PopulateControlFromBOProperty(TextboxStatusCode, State.MyBO.StatusCode)
        PopulateControlFromBOProperty(TextboxContactName, getSalutation(State.MyBO.ContactSalutationID) & State.MyBO.ContactName)
        PopulateControlFromBOProperty(TextboxCallerName, getSalutation(State.MyBO.CallerSalutationID) & State.MyBO.CallerName)
        PopulateControlFromBOProperty(TextboxProblemDescription, State.MyBO.ProblemDescription)
        PopulateControlFromBOProperty(TextboxLiabilityLimit, State.MyBO.LiabilityLimit)
        PopulateControlFromBOProperty(TextboxDeductible, State.MyBO.Deductible)
        PopulateControlFromBOProperty(TextboxTotalPaid, State.MyBO.TotalPaid)
        PopulateControlFromBOProperty(TextboxDedCollMethod,
                                         LookupListNew.GetDescriptionFromId(LookupListNew.GetDedCollMethodLookupList(Authentication.LangId),
                                                                            State.MyBO.DedCollectionMethodID))
        PopulateControlFromBOProperty(TextboxCCAuthCode, State.MyBO.DedCollectionCCAuthCode)
        PopulateControlFromBOProperty(TextboxDeductibleCollected, State.MyBO.DeductibleCollected)
        PopulateControlFromBOProperty(TextboxSlavageAmount, State.MyBO.SalvageAmount)
        PopulateControlFromBOProperty(TextboxClaimsAdjuster, State.MyBO.ClaimsAdjusterName)
        PopulateControlFromBOProperty(TextboxReportedDate, State.MyBO.ReportedDate)
        PopulateControlFromBOProperty(TextboxClaimClosedDate, State.MyBO.ClaimClosedDate)
        PopulateControlFromBOProperty(TextboxFollowupDate, State.MyBO.FollowupDate)
        PopulateControlFromBOProperty(TextboxAddedDate, State.MyBO.CreatedDate)
        PopulateControlFromBOProperty(TextboxLastModifiedBy, State.MyBO.LastOperatorName)
        PopulateControlFromBOProperty(TextboxLastModifiedDate, State.MyBO.LastModifiedDate)
        PopulateControlFromBOProperty(TextboxDiscount, State.MyBO.DiscountAmount)
        PopulateControlFromBOProperty(TextboxPolicyNumber, State.MyBO.PolicyNumber)
        PopulateControlFromBOProperty(TextboxLossDate, State.MyBO.LossDate)
        PopulateControlFromBOProperty(TextboxMobileNumber, State.MyBO.MobileNumber)
        PopulateControlFromBOProperty(TextboxMasterClaimNumber, State.MyBO.MasterClaimNumber)
        PopulateControlFromBOProperty(TextboxCaller_Tax_Number, State.MyBO.CallerTaxNumber)
        PopulateControlFromBOProperty(TextboxFraudulent, LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList("YESNO", Authentication.LangId, True), State.MyBO.Fraudulent))
        PopulateControlFromBOProperty(TextboxComplaint, LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList("YESNO", Authentication.LangId, True), State.MyBO.Complaint))
        PopulateControlFromBOProperty(TextboxTrackingNumber, State.MyBO.TrackingNumber)
        PopulateControlFromBOProperty(TextboxEMPLOYEE_NUMBER, State.MyBO.EmployeeNumber)
        PopulateControlFromBOProperty(TextboxDEVICE_ACTIVATION_DATE, State.MyBO.DeviceActivationDate)
        BindSelectItem(State.MyBO.FulfilmentMethod, cboFulfilmentMethod)
        If Not State.MyBO.BankInfoId.Equals(Guid.Empty) Then
            State.FulfilmentBankinfoBo = New BusinessObjectsNew.BankInfo(State.MyBO.BankInfoId)
            PopulateControlFromBOProperty(TextboxAccountNumber, State.FulfilmentBankinfoBo.Account_Number)
            ControlMgr.SetVisibleControl(Me, LabelAccountNumber, True)
            ControlMgr.SetVisibleControl(Me, TextboxAccountNumber, True)
        End If
        If State.MyBO.DeniedReasons IsNot Nothing Then
            'Me.PopulateControlFromBOProperty(Me.TextboxDeniedReasons, Me.State.MyBO.DeniedReasons)
            Dim deniedReasonsString As String = State.MyBO.DeniedReasons
            Dim dv As DataView = LookupListNew.GetDeniedReasonLookupList(Authentication.LangId)
            Dim translationSplitString As String = Nothing
            If (deniedReasonsString.IndexOf(";") > 0) Then
                For Each extendedcode As String In deniedReasonsString.Split(";")
                    Try
                        translationSplitString = translationSplitString + dv.ToTable().Select("extended_code ='" & extendedcode & "'").First()("description") + Environment.NewLine
                    Catch ex As Exception

                    End Try

                Next
                PopulateControlFromBOProperty(TextboxDeniedReasons, translationSplitString)
            Else
                Try
                    PopulateControlFromBOProperty(TextboxDeniedReasons, dv.ToTable().Select("extended_code ='" & State.MyBO.DeniedReasons & "'").First()("description"))
                Catch ex As Exception
                    PopulateControlFromBOProperty(TextboxDeniedReasons, "")
                End Try
            End If
            'Me.TextboxDeniedReasons.Text = Me.TextboxDeniedReasons.Text.Replace(";",Environment.NewLine)
            ControlMgr.SetVisibleControl(Me, LabelDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, cboDeniedReason, False)
        Else
            ControlMgr.SetVisibleControl(Me, LabelDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, TextboxDeniedReasons, False)
        End If
        If State.MyBO.ClaimedEquipment IsNot Nothing Then
            With State.MyBO.ClaimedEquipment
                PopulateControlFromBOProperty(TextboxCurrentDeviceSKU, .SKU)
                PopulateControlFromBOProperty(TextboxManufacturer, .Manufacturer)
                PopulateControlFromBOProperty(TextboxModel, .Model)
                PopulateControlFromBOProperty(TextboxPrice, .Price)
                PopulateControlFromBOProperty(TextboxSerialNumber, .SerialNumber)
            End With
        End If

        If State.MyBO.RepairShortDesc Is Nothing Then
            TextboxRepairCode.Text = String.Empty
        Else
            PopulateControlFromBOProperty(TextboxRepairCode, State.MyBO.RepairShortDesc & "-" & State.MyBO.RepairCode)
        End If
        If State.MyBO.ClaimStatusesCount > 0 Then
            If State.MyBO.LatestClaimStatus.StatusDescription IsNot Nothing Then
                TextboxClaimStatus.Text = State.MyBO.LatestClaimStatus.StatusDescription
            End If

            If State.MyBO.LatestClaimStatus.Owner IsNot Nothing Then
                TextboxClaimStatus.Text = String.Format("{0} {1}", TextboxClaimStatus.Text, State.MyBO.LatestClaimStatus.Owner)
            End If

        End If
        PopulateControlFromBOProperty(TextboxAssurantPays, State.MyBO.AssurantPays)
        PopulateControlFromBOProperty(TextboxConsumerPays, State.MyBO.ConsumerPays)
        PopulateControlFromBOProperty(TextboxDueToSCFromAssurant, State.MyBO.DueToSCFromAssurant)
        PopulateControlFromBOProperty(TextboxAboveLiability, State.MyBO.AboveLiability)
        PopulateControlFromBOProperty(TextboxBonusAmount, State.MyBO.BonusAmount)

        PopulateControlFromBOProperty(txtCurrentRetailPrice, State.MyBO.CurrentRetailPrice)
        Dim certObj As Certificate = New Certificate(State.MyBO.Certificate.Id)
        PopulateControlFromBOProperty(txtPaymentPassedDue, certObj.GetCertPaymentPassedDueExtInfo(State.MyBO.Certificate.Id))
        'If CType(Me.State.MyBO.CurrentRetailPrice, Decimal) > 0 Then
        '    Me.PopulateControlFromBOProperty(Me.txtCurrentRetailPrice, Me.State.MyBO.CurrentRetailPrice)
        'Else
        '    Me.PopulateControlFromBOProperty(Me.txtCurrentRetailPrice, GetDeviceCurrentRetailValue(Me.State.MyBO.Id))
        'End If

        If (Not State.IsMultiAuthClaim) Then
            Dim claimBo As Claim = CType(State.MyBO, Claim)

            If Not claimBo.WhoPaysId.Equals(Guid.Empty) Then
                SetSelectedItem(cboWhoPays, claimBo.WhoPaysId)
            End If
            If State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                PopulateControlFromBOProperty(TextboxCurrentOdometer, claimBo.CurrentOdometer)
            End If

            PopulateControlFromBOProperty(TextboxServiceCenter, claimBo.ServiceCenter)
            PopulateControlFromBOProperty(TextboxLoanerCenter, claimBo.LoanerCenter)
            PopulateControlFromBOProperty(TextboxRepairDate, claimBo.RepairDate)
            'Me.PopulateControlFromBOProperty(Me.TextboxDEVICE_ACTIVATION_DATE, claimBo.DeviceActivationDate)
            'Me.PopulateControlFromBOProperty(Me.TextboxEMPLOYEE_NUMBER, claimBo.EmployeeNumber)
            PopulateControlFromBOProperty(TextboxInvoiceProcessDate, claimBo.InvoiceProcessDate)
            PopulateControlFromBOProperty(TextboxInvoiceDate, claimBo.InvoiceDate)
            PopulateControlFromBOProperty(TextboxLoanerReturnedDate, claimBo.LoanerReturnedDate)
            PopulateControlFromBOProperty(TextboxAuthorizationNumber, claimBo.AuthorizationNumber)
            PopulateControlFromBOProperty(TextboxSource, claimBo.Source)
            PopulateControlFromBOProperty(TextboxTechnicalReport, claimBo.TechnicalReport)
            PopulateControlFromBOProperty(TextboxDefectReason, LookupListNew.GetDescriptionFromCode("CAUSES_OF_LOSS", claimBo.DefectReason))
            PopulateControlFromBOProperty(TextboxExpectedRepairDate, claimBo.ExpectedRepairDate)
            PopulateControlFromBOProperty(TextboxBatchNumber, claimBo.BatchNumber)
            PopulateControlFromBOProperty(TextboxSpecialService, claimBo.SpecialService)
            PopulateControlFromBOProperty(TextboxStoreNumber, claimBo.StoreNumber)
            PopulateControlFromBOProperty(TextboxSpecialInstruction, claimBo.SpecialInstruction)
            If claimBo.CanDisplayVisitAndPickUpDates Then
                PopulateControlFromBOProperty(TextboxVisitDate, claimBo.VisitDate)
                PopulateControlFromBOProperty(TextboxPickupDate, claimBo.PickUpDate)
            End If

            If State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
                LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                LabelExpectedRepairDate.Text = TranslationBase.TranslateLabelOrMessage("EXPECTED_REPLACEMENT_DATE") + ":"
            End If
            PopulateControlFromBOProperty(TextboxAuthorizedAmount, claimBo.AuthorizedAmount)

            If Not String.IsNullOrEmpty(claimBo.LoanerRquestedXcd) Then
                TextboxLoanerRequested.Text = LookupListNew.GetDescriptionFromExtCode(LookupListNew.LK_YESNO_EXT, ElitaPlusIdentity.Current.ActiveUser.LanguageId, claimBo.LoanerRquestedXcd)
            End If

            'Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, State.AuthorizedAmount)
            Dim objCountry As New Country(State.MyBO.Company.CountryId)
            If claimBo.ServiceCenterObject IsNot Nothing AndAlso claimBo.ServiceCenterObject.Id.Equals(objCountry.DefaultSCId) Then
                State.IsTEMP_SVC = True
            Else
                State.IsTEMP_SVC = False
            End If
        Else
            Dim claim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
            PopulateControlFromBOProperty(TextboxAuthorizedAmount, claim.AuthorizedAmount)
            'Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, State.AuthorizedAmount)
            State.claimAuthList = CType(State.MyBO, MultiAuthClaim).ClaimAuthorizationChildren.OrderBy(Function(i) i.AuthorizationNumber).ToList
            ucClaimConsequentialDamage.PopulateConsequentialDamage(State.MyBO)
        End If

        ControlMgr.SetVisibleControl(Me, LabelMethodOfRepair, Not IsDfFulfillment())
        ControlMgr.SetVisibleControl(Me, TextboxMethodOfRepair, Not IsDfFulfillment())

        If (State.MyBO.DeductibleCollected IsNot Nothing AndAlso State.MyBO.DeductibleCollected.Value > 0) Then

            ControlMgr.SetVisibleControl(Me, TextboxDeductibleCollected, True)
            ControlMgr.SetVisibleControl(Me, LabelDeductibleCollected, True)
        Else
            ControlMgr.SetVisibleControl(Me, TextboxDeductibleCollected, False)
            ControlMgr.SetVisibleControl(Me, LabelDeductibleCollected, False)
        End If

        PopulateRefurbReplaceClaimEquipment()
        PopulateClaimShipping()

    End Sub

    Private Function IsDfFulfillment() As Boolean
        Return Me.State.MyBO.FulfillmentProviderType = FulfillmentProviderType.DynamicFulfillment
    End Function

    Protected Sub PopulateClaimDetailContactInfoBOsFromForm()

        State.MyBO.ContactInfo.Address.InforceFieldValidation = True
        UserControlContactInfo.PopulateBOFromControl(True)
        State.MyBO.ContactInfo.Save()

    End Sub

    Private Sub PopulateBOForMultiAuthClaim()
        Dim claim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
        With claim
            PopulateBOProperty(claim, "ReasonClosedId", cboReasonClosed)
            PopulateBOProperty(claim, "IsLawsuitId", cboLawsuitId)
            PopulateBOProperty(claim, "TrackingNumber", TextboxTrackingNumber)
            PopulateBOProperty(claim, "DeniedReasonId", cboDeniedReason)
            PopulateBOProperty(claim, "ProblemDescription", TextboxProblemDescription)
            PopulateBOProperty(claim, "LiabilityLimit", TextboxLiabilityLimit)
            PopulateBOProperty(claim, "Deductible", TextboxDeductible)
            PopulateBOProperty(claim, "DiscountAmount", TextboxDiscount)
            PopulateBOProperty(claim, "PolicyNumber", TextboxPolicyNumber)
            PopulateBOProperty(claim, "CauseOfLossId", cboCauseOfLossId)
            PopulateBOProperty(claim, "FollowupDate", TextboxFollowupDate)

            PopulateBOProperty(claim, "DeviceActivationDate", TextboxDEVICE_ACTIVATION_DATE)
            PopulateBOProperty(claim, "EmployeeNumber", TextboxEMPLOYEE_NUMBER)

            HandleCloseClaimLogic()
            'if user tries to close denied claim, remove denied reason
            If (TextboxStatusCode.Text.Trim = Codes.CLAIM_STATUS__DENIED) Then
                If Not .ReasonClosedId.Equals(Guid.Empty) Then
                    PopulateBOProperty(claim, "DeniedReasonId", Guid.Empty)
                End If
            End If

            Dim YesIdd As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesIdd.ToString And claim.ContactInfo IsNot Nothing Then
                claim.ContactInfoId = claim.ContactInfo.Id
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            PopulateClaimDetailContactInfoBOsFromForm()
        End If
        PopulateBOProperty(claim, "FulfilmentMethod", cboFulfilmentMethod, False, True)

    End Sub

    Private Sub PopulateBOForSingleAuthClaim()
        Dim claim As Claim = CType(State.MyBO, Claim)

        With claim
            PopulateBOProperty(claim, "ReasonClosedId", cboReasonClosed)
            PopulateBOProperty(claim, "IsLawsuitId", cboLawsuitId)
            PopulateBOProperty(claim, "DeniedReasonId", cboDeniedReason)
            PopulateBOProperty(claim, "ProblemDescription", TextboxProblemDescription)
            PopulateBOProperty(claim, "SpecialInstruction", TextboxSpecialInstruction)
            PopulateBOProperty(claim, "AuthorizedAmount", TextboxAuthorizedAmount)
            PopulateBOProperty(claim, "LiabilityLimit", TextboxLiabilityLimit)
            PopulateBOProperty(claim, "Deductible", TextboxDeductible)
            PopulateBOProperty(claim, "DiscountAmount", TextboxDiscount)
            PopulateBOProperty(claim, "PolicyNumber", TextboxPolicyNumber)
            PopulateBOProperty(claim, "CauseOfLossId", cboCauseOfLossId)
            PopulateBOProperty(claim, "FollowupDate", TextboxFollowupDate)
            PopulateBOProperty(claim, "TrackingNumber", TextboxTrackingNumber)

            If State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                PopulateBOProperty(claim, "CurrentOdometer", TextboxCurrentOdometer)
            End If


            PopulateBOProperty(claim, "WhoPaysId", cboWhoPays)
            PopulateBOProperty(claim, "RepairDate", TextboxRepairDate)
            PopulateBOProperty(claim, "DeviceActivationDate", TextboxDEVICE_ACTIVATION_DATE)
            PopulateBOProperty(claim, "EmployeeNumber", TextboxEMPLOYEE_NUMBER)
            PopulateBOProperty(claim, "LoanerReturnedDate", TextboxLoanerReturnedDate)
            PopulateBOProperty(claim, "BatchNumber", TextboxBatchNumber)

            PopulateBOProperty(claim, "FulfilmentMethod", cboFulfilmentMethod, False, True)
            If IsNothing(State.FulfilmentBankinfoBo) Then
                State.FulfilmentBankinfoBo = New BusinessObjectsNew.BankInfo
            End If
            If GetSelectedValue(cboFulfilmentMethod) = Codes.FULFILMENT_METHOD_REIMBURSEMENT And TextboxAccountNumber.Text IsNot Nothing Then
                PopulateBOProperty(State.FulfilmentBankinfoBo, "CountryID", State.MyBO.Company.CountryId)
                PopulateBOProperty(State.FulfilmentBankinfoBo, "Account_Number", TextboxAccountNumber)
                State.FulfilmentBankinfoBo.ValidateBankFields = False
                State.FulfilmentBankinfoBo.Save()
                PopulateBOProperty(claim, "BankInfoId", State.FulfilmentBankinfoBo.Id)
            End If

            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            If claim IsNot Nothing AndAlso claim.CanDisplayVisitAndPickUpDates Then
                PopulateBOProperty(claim, "VisitDate", TextboxVisitDate)
                If Not claim.LoanerTaken Then
                    PopulateBOProperty(claim, "PickUpDate", TextboxPickupDate)
                Else
                    .SetPickUpDateFromLoanerReturnedDate()
                End If
            End If

            HandleCloseClaimLogic()

            'if user tries to close denied claim, remove denied reason
            If (TextboxStatusCode.Text.Trim = Codes.CLAIM_STATUS__DENIED) Then
                If Not .ReasonClosedId.Equals(Guid.Empty) Then
                    PopulateBOProperty(claim, "DeniedReasonId", Guid.Empty)
                End If
                PopulateBOProperty(claim, "AuthorizationNumber", TextboxAuthorizationNumber)
            End If

            Dim YesIdd As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesIdd.ToString And claim.ContactInfo IsNot Nothing Then
                claim.ContactInfoId = claim.ContactInfo.Id
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            PopulateClaimDetailContactInfoBOsFromForm()
        End If

        PopulateBOProperty(claim, "CurrentRetailPrice", txtCurrentRetailPrice)

    End Sub

    Protected Sub PopulateBOsFromForm()
        If (State.IsMultiAuthClaim) Then
            PopulateBOForMultiAuthClaim()
        Else
            PopulateBOForSingleAuthClaim()
        End If
    End Sub

    Protected Sub CreateNewServiceWarrantyClaimFromExistingClaim()

        State.AssociatedClaimBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
        State.AssociatedClaimBO.Clone(State.MyBO)

    End Sub

    Private Sub SaveClaim()

        State.MyBO.IsUpdatedComment = False
        Dim commentBO As Comment = State.MyBO.AddNewComment(False)
        commentBO.CallerName = State.MyBO.CallerName
        commentBO.CommentTypeId = Guid.Empty
        commentBO.Comments = Nothing

        'Check for Pending rules while reopening a claim
        If Not (Me.State.MyBO.Status = BasicClaimStatus.Closed Or Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            State.MyBO.CheckForPendingRules(commentBO)
        End If


        If (State.IsMultiAuthClaim) Then
            CType(State.MyBO, MultiAuthClaim).Save()
        Else
            CType(State.MyBO, Claim).Save()
        End If

        NotifyChanges(NavController)
        PopulateFormFromBOs()
        State.IsEditMode = False
        EnableDisablePageControls()





        NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = State.MyBO
        NavController.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT) = commentBO
        NavController.Navigate(Me, FlowEvents.EVENT_SAVE)

    End Sub

    Private Sub ServiceWarranty()

        If State.IsMultiAuthClaim Then
            CType(State.MyBO, MultiAuthClaim).CreateServiceWarranty()
            PopulateFormFromBOs()
            EnableDisablePageControls()
        Else
            NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = State.MyBO
            NavController.Navigate(Me, FlowEvents.EVENT_SERVICE_WARRANTY)
        End If


    End Sub

    ' Clean Popup Input
    Private Sub CleanPopupInput()
        Try
            If State IsNot Nothing Then
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CleanHiddenLimitExceededInput()
        HiddenLimitExceeded.Value = ""
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Dim actionInProgress As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
        CleanPopupInput()


        'if the user has clicked on the back button, then Yes means that leave the page, so stay on the page should be no, since logic below is relative to the page
        If confResponse IsNot Nothing And (actionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr Or actionInProgress = ElitaPlusPage.DetailPageCommand.Back) Then
            If confResponse = MSG_VALUE_YES Then
                confResponse = MSG_VALUE_NO
            Else
                confResponse = MSG_VALUE_YES
            End If
        End If

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                'If (Me.State.IsMultiAuthClaim) Then
                '    CType(Me.State.MyBO, MultiAuthClaim).Save()
                'Else
                '    CType(Me.State.MyBO, Claim).Save()
                'End If

                If ((State.MyBO.IsAuthorizedAmountChanged) AndAlso (State.MyBO.IsAuthorizationLimitExceeded)) Then
                    moMessageController.Clear()
                    moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                    TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                    TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

                    DisplayMessage(Message.MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenLimitExceeded)
                    Exit Sub
                End If

                If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) AndAlso
                   (State.MyBO.IsAuthorizedAmountChanged AndAlso Not (State.MyBO.IsAuthorizationLimitExceeded) Or (State.MyBO.IsProblemDescriptionChanged) _
                    Or (State.MyBO.IsSpecialInstructionChanged) Or (State.MyBO.IsDeductibleAmountChanged)) Then
                    'Assumption no service order for MultiAuthClaim
                    If (Not State.IsMultiAuthClaim) Then
                        NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                        NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing

                        'Do not create service order for GVS integrated service center
                        Dim objServiceCenter As New ServiceCenter(CType(State.MyBO, Claim).ServiceCenterId)
                        If Not objServiceCenter.IntegratedWithGVS Then
                            Dim soController As New ServiceOrderController
                            Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(CType(State.MyBO, Claim))
                            'Save this ServiceOrder in the FlowSession
                            NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                        End If
                    End If

                    'SaveClaim()
                    Exit Sub
                Else
                    State.MyBO.IsUpdatedComment = False
                    'Me.State.MyBO.Save()
                    NotifyChanges(NavController)
                    Select Case actionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Back(ElitaPlusPage.DetailPageCommand.Back)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Back(ElitaPlusPage.DetailPageCommand.BackOnErr)
                        Case ElitaPlusPage.DetailPageCommand.Save
                            SaveClaim()
                        Case ElitaPlusPage.DetailPageCommand.Redirect_
                            'pm 06-07-06 '
                            ServiceWarranty()
                    End Select
                End If
            End If
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Back(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)

            End Select
        End If
    End Sub

    Protected Sub CheckClaimPaymentInProgress()
        Try
            Dim blockInvoice As String
            Dim oCompany As New Company(State.MyBO.Company.Id)

            'Check the flag at Company level
            If (oCompany.AttributeValues.Contains(COMP_ATTR_BLOCK_PAY_INVOICE)) Then
                blockInvoice = oCompany.AttributeValues.Value(COMP_ATTR_BLOCK_PAY_INVOICE)
            End If

            If (blockInvoice = YES) Then
                If Claim.CheckClaimPaymentInProgress(State.MyBO.Id, State.MyBO.Company.CompanyGroupId) Then
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                    ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_CLAIM_PROCESS_IN_PROGRESS_ERR, Assurant.ElitaPlus.Common.ErrorCodes.CLAIM_PROCESS_IN_PROGRESS_ERR)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CheckIfComingFromAuthDetailForm()
        If State.HasAuthDetailDataChanged Then
            State.MyBO.AuthDetailDataHasChanged = State.HasAuthDetailDataChanged
            State.HasAuthDetailDataChanged = False
            btnEdit_WRITE_Click(Nothing, Nothing)
        ElseIf State.HasClaimStatusBOChanged Then
            btnEdit_WRITE_Click(Nothing, Nothing)
        End If

    End Sub

    Protected Sub CheckIfComingFromCreateClaimConfirm()
        Dim confResponse As String = HiddenLimitExceeded.Value
        CleanHiddenLimitExceededInput()
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            SaveClaim()
            System.Threading.Thread.CurrentThread.Abort()
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            'Nothing To Do. Stay on the page
        End If
    End Sub

    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        Dim retObj As ReturnType = New ReturnType(cmd, State.MyBO, CheckForChanges(NavController), State.IsCallerAuthenticated)
        NavController = Nothing
        ReturnToCallingPage(retObj)
    End Sub

    Private Function getSalutation(salutaionid As Guid) As String
        Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company =
                New Assurant.ElitaPlus.BusinessObjectsNew.Company(State.MyBO.CompanyId)

        If LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), companyBO.SalutationId) = "Y" Then
            Dim oSalutation As String = State.MyBO.getSalutationDescription(salutaionid) & " "
            Return oSalutation.TrimStart
        End If

        Return Nothing

    End Function


    'Private Function GetDeviceCurrentRetailValue(ByVal claimId As Guid) As Decimal

    '    ' Dim CompCountryObj As Company = New Company(Me.State.MyBO.CompanyId).BusinessCountryId
    '    Dim objCountry As Country = New Country(New Company(Me.State.MyBO.CompanyId).BusinessCountryId)
    '    Dim objClaimDAL As ClaimDAL = New ClaimDAL()
    '    Dim IndixId As String = objClaimDAL.GetIndixIdofRegisteredDevice(claimId)

    '    'IndixId = "abf65b259a6c0d9a3537a8e0687f6be9"
    '    'objCountry.Code = "US"
    '    Try

    '        If Not (IndixId Is Nothing) Then

    '            Dim newRequest As New ProductDetailsRequest() With {
    '                .mpId = IndixId,
    '                .countryCode = objCountry.Code
    '            }

    '            Dim Indix As New IndixServiceManager()

    '            Dim response As ProductDetailsResponse = Indix.GetProductDetails(newRequest)

    '            Dim salesPrice As Decimal = Convert.ToDecimal(response.Product.MaxSalePrice)

    '            Return salesPrice
    '        Else
    '            Return New Decimal(0)
    '        End If

    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try

    'End Function
    Private Sub InitializeUI()
        btnReopen_WRITE.Attributes.Add("onclick", "return revealModal('ModalReopenClaim');")
        btnModalReopenClaimYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", btnReopen_WRITE.UniqueID))
        lblReopenMessage.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_CONFIRM_REOPEN)

        AddCalendar_New(ImageButtonFollowupDate, TextboxFollowupDate)

        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")
        MasterPage.PageTitle = String.Format("{0} (<strong> #{1}</strong> )", TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY"), State.MyBO.ClaimNumber)
        UpdateBreadCrum()

        If Not State.IsMultiAuthClaim Then AddCalendar_New(ImageButtonRepairDate, TextboxRepairDate)
        If Not State.IsMultiAuthClaim Then AddCalendar_New(ImageButtonLoanerReturnedDate, TextboxLoanerReturnedDate)

        AddCalendar_New(ImageButtonDeviceActivationDate, TextboxDEVICE_ACTIVATION_DATE)

        If (Not State.IsMultiAuthClaim) Then
            If State.MyBO IsNot Nothing AndAlso CType(State.MyBO, Claim).CanDisplayVisitAndPickUpDates Then
                AddCalendar_New(ImageButtonVisitDate, TextboxVisitDate)
                AddCalendar_New(ImageButtonPickupDate, TextboxPickupDate)
            End If
        End If

        If (State.MyBO.AuthorizedAmount.Value > State.MyBO.AuthorizationLimit.Value) Then
            moMessageController.Clear()
            moMessageController.AddWarning(String.Format("{0}: {1}",
                                                            TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                            TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If

        If (State.IsMultiAuthClaim) Then ucClaimConsequentialDamage.Translate()

    End Sub

    Private Sub InitializeData()

        'Trace(Me, "Claim Id =" & GuidControl.GuidToHexString(Me.State.MyBO.Id) &
        '          "@Claim = " & Me.State.MyBO.ClaimNumber)

        If State.MyBO Is Nothing Then
            State.MyBO = ClaimFacade.Instance.CreateClaim(Of ClaimBase)()
        End If

        State.YesId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        State.NoId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)

        'Claim Auth Detail logic
        If State.MyBO.Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
            State.AuthDetailEnabled = True
        End If

    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Dim myBo As ClaimBase = State.MyBO
                'for single auth claims coming from claim search form and (certificate search and then to claim form)
                If (NavController.CurrentNavState.Name = "CLAIM_ISSUE_APPROVED_FROM_CLAIM" OrElse NavController.CurrentNavState.Name = "CLAIM_ISSUE_APPROVED_FROM_CERT") Then
                    NavController.Navigate(Me, "back", New ClaimForm.Parameters(State.MyBO.Id, State.IsCallerAuthenticated))
                ElseIf (NavOriginURL = ClaimWizardForm.AbsoluteURL) Then
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, State.IsCallerAuthenticated)
                    NavController = Nothing
                    ReturnToMaxCallingPage(retObj)
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, State.IsCallerAuthenticated)
                    NavController = Nothing
                    ReturnToCallingPage(retObj)
                End If
                'DirectCast(Me.NavController, Assurant.Common.AppNavigationControl.NavControllerBase).CurrentFlow.StartState.Url
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If cbousershipaddress.SelectedValue = NoId.ToString Then
                If Not State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
                    State.MyBO.ContactInfoId = Nothing
                End If
                If State.MyBO.ContactInfo IsNot Nothing Then
                    If State.MyBO.ContactInfo.Address IsNot Nothing Then State.MyBO.ContactInfo.Address.Delete()
                    State.MyBO.ContactInfo.Delete()
                End If
            End If

            If TextboxAuthorizedAmount.Text = String.Empty Or Not IsNumeric(TextboxAuthorizedAmount.Text) Then
                'display error
                ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
            End If

            If State.IsSVCCUserEdit Then
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    Select Case State.MyBO.ClaimAuthorizationType
                        Case ClaimAuthorizationType.Multiple
                            CType(State.MyBO, MultiAuthClaim).Save()
                        Case ClaimAuthorizationType.Single
                            CType(State.MyBO, Claim).Save()
                    End Select

                End If
                State.IsSVCCUserEdit = False
                PopulateFormFromBOs()
                State.IsEditMode = False
                EnableDisablePageControls()
                Exit Sub
            End If

            If State.IsEditMode = True Then
                If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                    If CType(TextboxLiabilityLimit.Text, Decimal) <> 0 Then
                        ElitaPlusPage.SetLabelError(LabelLiabilityLimit)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If
                End If
                If Not IsNumeric(TextboxLiabilityLimit.Text) Then
                    ElitaPlusPage.SetLabelError(LabelLiabilityLimit)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                End If

                If CType(TextboxLiabilityLimit.Text, Decimal) < 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelLiabilityLimit)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                End If

                'if entered amount > existing liability limit then
                If CType(TextboxLiabilityLimit.Text, Decimal) > State.MyBO.LiabilityLimit.Value Then
                    ' check if entered amount>oldest liability limit
                    Dim dv As DataView, oClm As ClaimBase
                    dv = oClm.GetOriginalLiabilityLimit(State.MyBO.Id)
                    Dim dLiabilityLimit As Decimal
                    If dv.Count > 0 AndAlso Not IsDBNull(dv(0)(0)) Then
                        dLiabilityLimit = CType(dv(0)(0), Decimal)
                        If CType(TextboxLiabilityLimit.Text, Decimal) > dLiabilityLimit Then
                            'display error
                            ElitaPlusPage.SetLabelError(LabelLiabilityLimit)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                        End If
                    Else
                        'if no history record raise error since entered amount is >existing amount
                        ElitaPlusPage.SetLabelError(LabelLiabilityLimit)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If
                End If
            End If

            PopulateBOsFromForm()
            If State.RefurbReplaceClaimEquipmentId <> Guid.Empty Then
                ClaimEquipment.UpdateClaimEquipmentInfo(State.RefurbReplaceClaimEquipmentId, txtRefurbReplaceComments.Text)
            End If

            If State.InboundClaimShippingId <> Guid.Empty Then
                ClaimShipping.UpdateClaimShippingInfo(State.InboundClaimShippingId, txtShipToSC.Text)
            End If
            If State.OutboundClaimShippingId <> Guid.Empty Then
                ClaimShipping.UpdateClaimShippingInfo(State.OutboundClaimShippingId, txtShipToCust.Text)
            End If

            If State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                If State.MyBO.PolicyNumber Is Nothing Then
                    'display error
                    ElitaPlusPage.SetLabelError(LabelPolicyNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
                End If
            End If

            Dim authorizedAmountChanged As Boolean = State.MyBO.IsAuthorizedAmountChanged
            Dim deductibleAmountChanged As Boolean = State.MyBO.IsDeductibleAmountChanged
            Dim problemDescriptionChanged As Boolean = State.MyBO.IsProblemDescriptionChanged
            Dim specialInstructionChanged As Boolean = State.MyBO.IsSpecialInstructionChanged

            If (State.IsMultiAuthClaim) Then CType(State.MyBO, MultiAuthClaim).Validate() Else CType(State.MyBO, Claim).PreValidate()

            'If the AuthorizedAmount has been modified, then Generate and Send a Service Order for this Claim
            'Make sure it is NOT a Pending Claim - since, we do NOT generate and send a Service Order for a Pending Claim
            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
            NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing
            If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) AndAlso
               (authorizedAmountChanged AndAlso Not (State.MyBO.IsAuthorizationLimitExceeded) Or (problemDescriptionChanged) Or (specialInstructionChanged) Or (deductibleAmountChanged)) Then
                'AuthorizedAmount has been changed for an Active Claim
                'Generate and Send a Service Order for this Active Claim 

                'First check the AuthorizedAmount validation
                If State.MyBO.AuthorizedAmount Is Nothing Then
                    ElitaPlusPage.SetLabelError(LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If

                If State.MyBO.IsDirty Then
                    If State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                        If ((authorizedAmountChanged) AndAlso (Not (State.MyBO.IsAuthorizationLimitExceeded))) Then
                            '' Auth amt is being changed, calculate the new deductible amt if by percent
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                                If State.MyBO.DeductiblePercent IsNot Nothing Then
                                    If State.MyBO.DeductiblePercent.Value > 0 Then
                                        If (Not deductibleAmountChanged) Then
                                            State.MyBO.Calculate_deductible_if_by_percentage()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'Assumption : No service Order for MultiAuthClaim
                    If (Not State.IsMultiAuthClaim) Then
                        'Do not create service order for GVS integrated service center
                        Dim objServiceCenter As New ServiceCenter(CType(State.MyBO, Claim).ServiceCenterId)
                        If Not objServiceCenter.IntegratedWithGVS Then
                            'Reqs-784
                            CType(State.MyBO, Claim).Save()
                            Dim soController As New ServiceOrderController
                            Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(CType(State.MyBO, Claim))

                            'Save this ServiceOrder in the FlowSession
                            NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                        End If
                    End If



                End If



                If ((authorizedAmountChanged) AndAlso (Not (State.MyBO.IsAuthorizationLimitExceeded)) OrElse
                    (Not (authorizedAmountChanged))) Then

                    If cboReasonClosed.SelectedIndex > 0 Then
                        DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Save
                    Else
                        SaveClaim()
                    End If


                ElseIf ((authorizedAmountChanged) AndAlso (State.MyBO.IsAuthorizationLimitExceeded)) Then
                    moMessageController.Clear()
                    moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                    TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                    TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

                    DisplayMessage(Message.MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenLimitExceeded)
                End If
            ElseIf State.MyBO.IsFamilyDirty And State.HasClaimStatusBOChanged Then
                SaveClaim()
            Else
                SaveClaim()
            End If
            Save_Ok = True
        Catch ex As Exception
            'Flush out the ServiceOrder from the FlowSession
            If NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) IsNot Nothing Then
                NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing
            End If

            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        'Get out of Edit mode
        Try
            UndoChanges()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub UndoChanges()
        If Not State.MyBO.IsNew Then
            'Reload from the DB
            State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
        End If
        State.IsEditMode = False
        PopulateFormFromBOs()
        EnableDisablePageControls()
        MasterPage.MessageController.Clear()

    End Sub

    Private Sub btnEdit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
        Try
            State.IsEditMode = True
            'Service center role update
            If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__SERVICE_CENTER) Then
                State.IsSVCCUserEdit = True
            End If
            EnableDisablePageControls()
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesId.ToString Then
                moUserControlContactInfo.Visible = True
                UserControlContactInfo.EnableDisablecontrol(False)
                UserControlAddress.EnableDisablecontrol(False)
            Else
                moUserControlContactInfo.Visible = False

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnNewItemInfo_Click(sender As Object, e As System.EventArgs) Handles btnNewItemInfo.Click
        Try
            callPage(Claims.ReplacementForm.URL, New Claims.ReplacementForm.Parameters(True, State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnPartsInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnPartsInfo.Click
        Try
            If State.MyBO IsNot Nothing Then
                PopulateBOsFromForm()
                NavController.Navigate(Me, "parts_info", BuildPartsInfoParameters)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Function BuildPartsInfoParameters() As PartsInfoForm.Parameters
        Dim claimBO As ClaimBase = State.MyBO
        Return New PartsInfoForm.Parameters(claimBO)
    End Function

    Private Sub btnServiceCenterInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnServiceCenterInfo.Click
        Try
            If (Not (CType(State.MyBO, Claim).ServiceCenterId.Equals(Guid.Empty))) Then
                NavController.Navigate(Me, "service_center_info", New ServiceCenterInfoForm.Parameters(CType(State.MyBO, Claim).ServiceCenterId))
                'Me.callPage(ServiceCenterInfoForm.URL, New ServiceCenterInfoForm.Parameters(CType(Me.State.MyBO, Claim).ServiceCenterId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnReplaceItemInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnReplaceItem.Click
        Try
            If (Not State.IsMultiAuthClaim) Then
                NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Nothing
                NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = State.MyBO
                NavController.Navigate(Me, FlowEvents.EVENT_REPLACE_ITEM, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_FOR_KEEPING_SAME_LOCATION))
            Else
                If CType(State.MyBO, MultiAuthClaim).HasMultipleServiceCenters Then
                    ucSelectServiceCenter.Populate(State.MyBO, Guid.Empty)
                    ucSelectServiceCenter.moMessageController.AddError("CLAIM_HAS_MULTIPLE_SERVICE_CENTERS")
                    Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
                    RegisterStartupScript("Startup", x)
                Else
                    lblChangeServiceCenterMessage.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_PROMPT_FOR_KEEPING_SAME_LOCATION)
                    Dim x As String = "<script language='JavaScript'> revealModal('ModalChangeServiceCenter') </script>"
                    RegisterStartupScript("Startup", x)
                End If

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnServiceWarranty_Click(sender As System.Object, e As System.EventArgs) Handles btnServiceWarranty.Click

        Dim oContract As Contract
        Dim CoverageType As String
        Dim ClaimControl As Boolean = False

        Try
            oContract = Contract.GetContract(State.MyBO.Certificate.DealerId, State.MyBO.Certificate.WarrantySalesDate.Value)

            If oContract IsNot Nothing Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oContract.ClaimControlID) = "Y" Then
                    ClaimControl = True
                End If
            End If


            If ClaimControl Then
                DisplayMessage(Message.MSG_DEALER_USER_CLAIM_INTERFACES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
            Else
                If State.IsMultiAuthClaim Then
                    If CallAddServiceWarrantyWs() Then
                        State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                        PopulateFormFromBOs()
                        PopulateGrid()
                        EnableDisablePageControls()
                    End If
                Else
                    NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = State.MyBO
                    NavController.Navigate(Me, FlowEvents.EVENT_SERVICE_WARRANTY)
                End If

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub
#Region "Fulfillment Web Service - Add Service Warranty Authorization"
    Private Const ResponseStatusFailure As String = "Failure"
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
    Private Sub DisplayWsErrorMessage(errCode As String, errDescription As String)
        MasterPage.MessageController.AddError(errCode & " - " & errDescription, False)
    End Sub
    Private Function CallAddServiceWarrantyWs() As Boolean
        Dim wsRequest As BaseFulfillmentRequest = New BaseFulfillmentRequest()
        Dim wsResponse As BaseFulfillmentResponse
        Dim blnsuccess As Boolean = True

        wsRequest.ClaimNumber = State.MyBO.ClaimNumber
        wsRequest.CompanyCode = State.MyBO.Company.Code
        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(c As FulfillmentServiceClient)
                    Return c.AddServiceWarranty(wsRequest)
                End Function)
        Catch ex As Exception
            blnsuccess = False
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try

        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(BaseFulfillmentResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                    blnsuccess = False
                Else
                    MasterPage.MessageController.AddSuccess("SERVICE_WARRANTY_AUTH_ADDED", True)
                    blnsuccess = True
                End If
            End If
        End If

        Return blnsuccess

    End Function
#End Region

    Private Sub btnNewServiceCenter_Click(sender As System.Object, e As System.EventArgs) Handles btnNewServiceCenter.Click
        Try
            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Nothing
            Dim claim As Claim = CType(State.MyBO, Claim)

            Dim coverageType As String = claim.GetCoverageTypeCodeForServiceCenter
            'Def-23747 - Added new parameter CoountryId to locateServCenterParameters.
            Dim locateServCenterParameters As New LocateServiceCenterForm.Parameters(State.MyBO.Certificate.DealerId,
                                                                                     State.MyBO.Certificate.AddressChild.ZipLocator,
                                                                                     State.MyBO.RiskTypeId,
                                                                                     claim.GetManufacturerIdForServiceCenter,
                                                                                     claim.GetCoverageTypeCodeForServiceCenter, State.MyBO.CertItemCoverageId, Guid.Empty, True, False,
                                                                                     , , , State.MyBO.Company.CountryId)
            NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = State.MyBO
            NavController.Navigate(Me, FlowEvents.EVENT_NEW_CENTER, locateServCenterParameters)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnPoliceReport_Click(sender As System.Object, e As System.EventArgs) Handles btnPoliceReport.Click
        Try

            If (Not (State.MyBO.Id.Equals(Guid.Empty))) Then
                NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_INFO, New PoliceReportForm.Parameters(State.MyBO.Id))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnCertificate_Click(sender As System.Object, e As System.EventArgs) Handles btnCertificate.Click
        Try
            If (Not (State.MyBO.CertificateId.Equals(Guid.Empty))) Then
                CType(MyBase.State, BaseState).NavCtrl = NavController
                'Me.callPage(Certificates.CertificateForm.URL, Me.State.MyBO.CertificateId)
                callPage(Certificates.CertificateForm.URL, New Certificates.CertificateForm.Parameters(State.MyBO.CertificateId, State.IsCallerAuthenticated))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            If (Not (State.MyBO.GetLatestServiceOrder Is Nothing)) Then
                NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = State.MyBO.GetLatestServiceOrder
                NavController.Navigate(Me, FlowEvents.EVENT_REPRINT)
            Else
                Try
                    Dim _SOC As New ServiceOrderController()
                    _SOC.GenerateServiceOrder(CType(State.MyBO, Claim))
                    NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = State.MyBO.GetLatestServiceOrder
                    NavController.Navigate(Me, FlowEvents.EVENT_REPRINT)
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

    Private Sub btnComment_Click(sender As System.Object, e As System.EventArgs) Handles btnComment.Click
        Try
            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.MyBO
            NavController.Navigate(Me, FlowEvents.EVENT_COMMENTS, New CommentListForm.Parameters(State.MyBO.CertificateId, State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnItem_Click(sender As System.Object, e As System.EventArgs) Handles btnItem.Click
        Try
            callPage(Claims.ClaimItemForm.URL, New Claims.ClaimItemForm.Parameters(State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnClaimDeniedInformation_Click(sender As System.Object, e As System.EventArgs) Handles btnClaimDeniedInformation.Click
        Try
            Select Case State.MyBO.ClaimAuthorizationType
                Case ClaimAuthorizationType.Multiple
                    CType(State.MyBO, MultiAuthClaim).Save()
                Case ClaimAuthorizationType.Single
                    CType(State.MyBO, Claim).Save()
            End Select
            NavController.Navigate(Me, "claim_denied_information", New Claims.ClaimDeniedInformationForm.Parameters(State.MyBO, State.MyBO.Id, State.MyBO.CertificateId))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnReopen_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnReopen_WRITE.Click

        Try
            If (Not State.IsMultiAuthClaim) Then
                CType(State.MyBO, Claim).ReopenClaim()
                If (CType(State.MyBO, Claim).IsSupervisorAuthorizationRequired) Then
                    State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                End If
            Else
                CType(State.MyBO, MultiAuthClaim).ReopenClaim()
            End If
            SaveClaim()
            Save_Ok = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnShipping_Click(sender As System.Object, e As System.EventArgs) Handles btnShipping.Click
        Try
            If (Not (State.MyBO.ShippingInfoId.Equals(Guid.Empty))) Then
                NavController.Navigate(Me, FlowEvents.EVENT_SHIPPING_INFO, New ShippingInfoForm.Parameters(State.MyBO.ShippingInfoId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnAuthDetail_Click(sender As System.Object, e As System.EventArgs) Handles btnAuthDetail.Click
        Try
            If State.MyBO IsNot Nothing Then
                PopulateBOsFromForm()
                'DEF-17426
                'Me.callPage(ClaimAuthDetailForm.URL, New ClaimAuthDetailForm.Parameters(CType(Me.State.MyBO, Claim), Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV))
                NavController.Navigate(Me, "auth_detail", BuildClaimAuthDetailParameters)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'DEF-17426
    Function BuildClaimAuthDetailParameters() As ClaimAuthDetailForm.Parameters
        Return New ClaimAuthDetailForm.Parameters(CType(State.MyBO, Claim), State.ClaimAuthDetailBO, State.PartsInfoDV)
    End Function

    Private Sub btnMasterClaim_Click(sender As System.Object, e As System.EventArgs) Handles btnMasterClaim.Click
        Try
            If (State.MyBO.MasterClaimNumber IsNot Nothing AndAlso Not (State.MyBO.Id.Equals(Guid.Empty))) Then
                CType(MyBase.State, BaseState).NavCtrl = NavController
                params.Add(State.MyBO.MasterClaimNumber)
                params.Add(State.MyBO.Id)
                params.Add(State.MyBO.CertificateId)
                callPage(ClaimForm.MASTER_CLAIM_DETAIL_URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUseRecoveries_Click(sender As Object, e As System.EventArgs) Handles btnUseRecoveries.Click
        Try
            If (Not State.MyBO.CertItemCoverageId.Equals(Guid.Empty) AndAlso Not (CType(State.MyBO, Claim).ServiceCenterId.Equals(Guid.Empty))) Then
                NavController.Navigate(Me, "recovery_locate_sc", BuildServiceCenterParametersForRecovery)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    Function BuildServiceCenterParametersForRecovery() As LocateServiceCenterForm.Parameters
        Dim showAcceptButton As Boolean = True
        Dim claim As Claim = CType(State.MyBO, Claim)
        Return New LocateServiceCenterForm.Parameters(State.MyBO.Certificate.DealerId, State.MyBO.Certificate.AddressChild.ZipLocator, State.MyBO.RiskTypeId, claim.GetManufacturerIdForServiceCenter, CType(State.MyBO, Claim).GetCoverageTypeCodeForServiceCenter, State.MyBO.CertItemCoverageId, State.MyBO.Id, showAcceptButton, True, False, True)
    End Function

    Private Sub btnChangeCoverage_Click(sender As System.Object, e As System.EventArgs) Handles btnChangeCoverage.Click
        Try
            If (Not (State.MyBO.Id.Equals(Guid.Empty))) Then
                params.Add(CType(State.MyBO.LossDate, String))
                params.Add(State.MyBO.Id)
                params.Add(State.MyBO.CertificateId)
                params.Add(State.MyBO.CertItemCoverageId)
                params.Add(State.MyBO.StatusCode)

                Dim invoiceProcessDate As DateType = If(State.IsMultiAuthClaim, Nothing, CType(State.MyBO, Claim).InvoiceProcessDate)
                If invoiceProcessDate IsNot Nothing Then
                    params.Add(CType(invoiceProcessDate, String))
                Else
                    params.Add(Nothing)
                End If
                callPage(ClaimForm.COVERAGE_TYPE_URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnStatusDetail_Click(sender As Object, e As System.EventArgs) Handles btnStatusDetail.Click
        Try
            If State.MyBO IsNot Nothing Then
                PopulateBOsFromForm()
                callPage(ClaimStatusDetailForm.URL, State.MyBO.Id)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnClaimHistoryDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnClaimHistoryDetails.Click
        Try
            Dim URL As String = "~/Claims/ClaimHistoryForm.aspx"
            params.Add(State.MyBO.ClaimNumber)
            params.Add(State.MyBO.Id)

            callPage(URL, params)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Protected Sub btnOutboundCommHistory_Click(sender As Object, e As EventArgs) Handles btnOutboundCommHistory.Click
        Try
            callPage(Tables.OcMessageSearchForm.URL, New Tables.OcMessageSearchForm.CallType("claim_number", State.MyBO.ClaimNumber, State.MyBO.Id, State.MyBO.Dealer.Id))
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRepairLogistics_Click(sender As System.Object, e As System.EventArgs) Handles btnRepairLogistics.Click
        Dim param As RepairAndLogisticsForm.Parameters = New RepairAndLogisticsForm.Parameters()

        Try
            Dim URL As String = "~/Claims/RepairAndLogisticsForm.aspx"

            param.ClaimId = State.MyBO.Id

            param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Claim
            callPage(URL, param)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnDenyClaim_Click(sender As System.Object, e As System.EventArgs) Handles btnDenyClaim.Click
        Try
            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = State.MyBO
            NavController.Navigate(Me, FlowEvents.EVENT_DENY)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnRedo_Click(sender As Object, e As System.EventArgs) Handles btnRedo.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_REDO, State.MyBO)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub
    Protected Sub btnClaimIssues_Click(sender As Object, e As System.EventArgs) Handles btnClaimIssues.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_ISSUES, New ClaimIssueForm.Parameters(State.MyBO))

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnClaimImages_Click(sender As System.Object, e As System.EventArgs) Handles btnClaimImages.Click
        Try
            Dim URL As String = "~/Claims/ClaimDocumentForm.aspx"
            params.Add(State.MyBO)
            callPage(URL, params)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub
    Protected Sub btnClaimCaseList_Click(sender As Object, e As System.EventArgs) Handles btnClaimCaseList.Click
        Try
            params.Add(State.MyBO)
            callPage(ClaimDetailsForm.Url, params)
        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnAddConseqDamage_Click(sender As Object, e As System.EventArgs) Handles btnAddConseqDamage.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_RECORDING_CONSEQUENTIAL_DAMAGE, New ClaimRecordingForm.Parameters(State.MyBO.Certificate.Id, State.MyBO.Id, Nothing, Codes.CASE_PURPOSE__CONSEQUENTIAL_DAMAGE))
        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub SelectServiceCenter(sender As System.Object, e As System.EventArgs) Handles ucSelectServiceCenter.SelectServiceCenter
        Try
            Dim selectedServiceCenterId As Guid = ucSelectServiceCenter.SelectedServiceCenterId
            Dim multiAuthClaim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
            multiAuthClaim.CreateReplacementFromRepair(selectedServiceCenterId)
            PopulateFormFromBOs()
            PopulateGrid()
            EnableDisablePageControls()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnChangeServiceCenterYes_Click(sender As System.Object, e As System.EventArgs) Handles btnModalSelectServiceCenterNo.Click
        Try
            ucSelectServiceCenter.Populate(State.MyBO, Guid.Empty)
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'REQ-6230
    Private Sub btnPriceRetailSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnPriceRetailSearch.Click
        Try
            Dim URL As String = "~/Claims/ClaimIndixSearchForm.aspx"
            params.Add(State.MyBO.Id)
            params.Add(State.MyBO.ClaimedEquipment.Model)
            params.Add(State.MyBO.CompanyId)
            params.Add(State.MyBO.Purchase_Price)
            params.Add(State.MyBO.IndixId)

            callPage(URL, params)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

    End Sub



    Private Sub btnChangeServiceCenterNo_Click(sender As System.Object, e As System.EventArgs) Handles btnModalSelectServiceCenterYes.Click
        Try
            Dim multiAuthClaim As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
            multiAuthClaim.CreateReplacementFromRepair(multiAuthClaim.NonVoidClaimAuthorizationList.FirstOrDefault().ServiceCenterId)
            PopulateFormFromBOs()
            EnableDisablePageControls()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnChangeFulfillment_Click(sender As Object, e As EventArgs) Handles btnChangeFulfillment.Click

        ChangeFulfillmentAction(State.MyBO.Certificate.Id,
                                State.MyBO.Id,
                                Nothing,
                                Codes.CasePurposeChangeFulfillment,
                                State.IsCallerAuthenticated)
    End Sub

    Private Sub btnReplacementQuote_Click(sender As Object, e As EventArgs) Handles btnReplacementQuote.Click
        Try
            callPage(Claims.ClaimReplacementQuoteForm.URL, New Claims.ClaimReplacementQuoteForm.Parameters(State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClaimDeductibleRefund_Click(sender As Object, e As EventArgs) Handles btnClaimDeductibleRefund.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_DEDUCTIBLE_REFUND, New ClaimDeductibleRefundForm.Parameters(State.MyBO))

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

    Private Sub cboReasonClosed_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboReasonClosed.SelectedIndexChanged
        Try
            If (TextboxStatusCode.Text <> Codes.CLAIM_STATUS__CLOSED) Then
                'For a Claim that is NOT CLOSED
                If (cboReasonClosed.SelectedIndex > 0) Then
                    If (Not State.IsMultiAuthClaim) Then
                        If (((Not (CType(State.MyBO, Claim).LoanerCenterId.Equals(Guid.Empty))) AndAlso (Not (CType(State.MyBO, Claim).LoanerReturnedDate Is Nothing))) OrElse
                            (CType(State.MyBO, Claim).LoanerCenterId.Equals(Guid.Empty))) Then
                            'When a Loaner has been taken and HAS been Returned OrElse_
                            'When a Loaner has NOT been taken
                            'The Claim CAN be Closed

                            State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED
                            'Me.AddControlMsg(Me.btnSave_WRITE, Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                        End If
                    End If
                Else
                    'The Claim CANNOT be Closed
                    State.MyBO.StatusCode = TextboxStatusCode.Text
                    btnSave_WRITE.Attributes.Remove("onclick")
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub


#End Region

#Region "Error Handling"

#End Region

#Region "Navigation Control"

    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow("CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"))
    End Sub

    Public Class StateControllerCreateNewReplacementClaim
        Implements IStateController


        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            Dim oldClaim As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)
            Dim newServCenter As ServiceCenter = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)

            If (oldClaim.IsDirty) Then
                oldClaim.CalculateFollowUpDate()
            End If

            Dim newClaim As Claim = oldClaim.CreateNewClaim(oldClaim.Id)
            oldClaim.PopulateNewReplacementClaim(newClaim)
            If (oldClaim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then
                'This is an existing Service Warranty Claim
                'Close this Claim regardless of the estimate price for the Service Center
                If Not oldClaim.PreserveAuthAmount() Then
                    oldClaim.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPLACED)
                    oldClaim.CloseTheClaim()
                End If
            Else
                'This is NOT an existing Service Warranty Claim
                oldClaim.ProcessExistingClaim()
            End If

            'Change if new service center selected
            If newServCenter IsNot Nothing Then
                newClaim.ServiceCenterId = newServCenter.Id
            End If

            If (oldClaim.Source Is Nothing AndAlso newServCenter IsNot Nothing AndAlso Not newServCenter.Id.Equals(oldClaim.ServiceCenterId)) Then
                'Do not create service order for GVS integrated service center
                Dim objServiceCenter As New ServiceCenter(oldClaim.ServiceCenterId)
                If Not objServiceCenter.IntegratedWithGVS Or oldClaim.NotificationTypeId.Equals(Guid.Empty) Then
                    'Generate Service Order for the Existing Claim
                    Dim soController As New ServiceOrderController
                    Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(oldClaim)

                    'Save this ServiceOrder in the FlowSession
                    navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                End If
            Else
                navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing
            End If

            'The Following line of code added by Ravi
            NotifyChanges(navCtrl)

            navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM) = newClaim
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class

    Public Class StateControllerCreateNewServiceWarrantyClaim
        Implements IStateController

        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            Dim oldClaim As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)
            'Remove the Old Claim BO from the session
            navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Nothing
            Dim newClaim As Claim = oldClaim.CreateNewClaim()
            oldClaim.PopulateNewServiceWarrantyClaim(newClaim)

            'The Following line of code added by Ravi
            NotifyChanges(navCtrl)

            navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM) = newClaim
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class

    Public Class StateControllerRollbackCreateNewClaim
        Implements IStateController

        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            navCtrl.FlowSession.Clear()
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class

    Public Class StateControllerChangeServiceCenter
        Implements IStateController

        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            'Prepare to Generate a Service Order
            Dim oldClaim As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)
            Dim newServiceCenter As ServiceCenter = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)
            If ((newServiceCenter Is Nothing) OrElse (oldClaim.ServiceCenterId.Equals(newServiceCenter.Id))) Then
                'The user did NOT change the Service Center
                'Do NOT generate a Service Order
                'Just go back to the ClaimDetailForm
                navCtrl.Navigate(callingPage, FlowEvents.EVENT_BACK)
            Else
                'Keep a copy of the Original ServiceCenterID and the Original AuthorizedAmount
                navCtrl.FlowSession(FlowSessionKeys.SESSION_ORIGINAL_SERVICE_CENTER_ID) = oldClaim.ServiceCenterId
                navCtrl.FlowSession(FlowSessionKeys.SESSION_ORIGINAL_AUTHORIZED_AMOUNT) = oldClaim.AuthorizedAmount
                'The user HAS picked a NEW Service Center
                oldClaim.ServiceCenterId = newServiceCenter.Id
                'Calculate the Authorized Amount
                If (oldClaim.ClaimActivityId.Equals(Guid.Empty)) Then
                    'It is a Repair Claim
                    oldClaim.SetAuthorizedAmount()
                End If
                'Throw the oldClaim object back in the Session

                navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = oldClaim
                navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
            End If

        End Sub
    End Class

    Public Class StateControllerCheckAuthorizedAmount
        Implements IStateController

        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            Dim oldClaim As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)

            'Check if the EstimatePrice for this ServiceCenter EXCEEDS the AuthorizationLimit for the User
            If (oldClaim.IsAuthorizationLimitExceeded) Then

                'Restore the original ServiceCenterId and AuthorizedAmount for the oldClaim
                oldClaim.ServiceCenterId = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_ORIGINAL_SERVICE_CENTER_ID), Guid)
                oldClaim.AuthorizedAmount = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_ORIGINAL_AUTHORIZED_AMOUNT), DecimalType)

                'Save this Claim in the FlowSession
                navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = oldClaim

                'Display a DialogBox to the user showing that the AuthorizationLimit has been exceeded
                navCtrl.Navigate(callingPage, FlowEvents.EVENT_AUTHORIZATION_LIMIT_EXCEEDED, Message.MSG_AUTH_LIMIT_EXCEEDED_PICK_OTHER_SVC_CTR)

                'navCtrl.Navigate() should be the last line of code in your logic... otherwise you must issue a Return() from the Sub() as demonstrated here: 
                Return
            End If

            'Do NOT Generate a Service Order for a Pending Claim and Denied Claim
            If (oldClaim.StatusCode <> Codes.CLAIM_STATUS__PENDING) And (oldClaim.StatusCode <> Codes.CLAIM_STATUS__DENIED) Then
                'Do not create service order for GVS integrated service center
                Dim objServiceCenter As New ServiceCenter(oldClaim.ServiceCenterId)
                If Not objServiceCenter.IntegratedWithGVS Then
                    'Call The Create Service Order Use Case
                    Dim soController As New ServiceOrderController
                    Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(oldClaim)

                    'Save this ServiceOrder in the FlowSession
                    navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                End If




            End If

            If (oldClaim.IsDirty) Then
                oldClaim.CalculateFollowUpDate()
            End If

            'Save all the BO's to the database now
            oldClaim.IsUpdatedComment = False
            oldClaim.Save()
            NotifyChanges(navCtrl)

            'We need to generate and send a Service Order for this Claim
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)

        End Sub

    End Class

    Public Class StateControllerNewLocation
        Implements IStateController


        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, BuidlLocateServiceCenterParameters(navCtrl))
        End Sub

        Function BuidlLocateServiceCenterParameters(navController As INavigationController) As LocateServiceCenterForm.Parameters
            Dim myBo As Claim = CType(navController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)

            Dim cert As Certificate = New Certificate(myBo.CertificateId)
            Return New LocateServiceCenterForm.Parameters(cert.DealerId,
                                                          cert.AddressChild.ZipLocator,
                                                          myBo.RiskTypeId,
                                                          myBo.GetManufacturerIdForServiceCenter,
                                                          myBo.GetCoverageTypeCodeForServiceCenter, myBo.CertItemCoverageId, Guid.Empty, True, False, True)
        End Function
    End Class

    Public Class StateControllerSaveClaimConfirmation
        Implements IStateController

#Region "Private Atributes"
        Private NavController As INavigationController
        Private CallingPage As ElitaPlusPage
#End Region

#Region "Internal State Management"
        Enum ProcessingStage
            AddingOKPrompt
            CheckingForUserAction
        End Enum

        Class MyState
            Public Stage As ProcessingStage = ProcessingStage.AddingOKPrompt
        End Class

        Public ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property
#End Region
        Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process

            NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)
            Select Case State.Stage
                Case ProcessingStage.AddingOKPrompt
                    AddPrompt(callingPage)
                    State.Stage = ProcessingStage.CheckingForUserAction
                Case ProcessingStage.CheckingForUserAction
                    Dim oldClaim As ClaimBase = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), ClaimBase)
                    Dim newComment As Comment = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT), Comment)
                    navCtrl.Navigate(Me.CallingPage, FlowEvents.EVENT_NEXT, New CommentForm.Parameters(newComment))
            End Select
        End Sub

        Sub AddPrompt(callingPage As System.Web.UI.Page)
            CType(callingPage, ElitaPlusPage).DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_CONFIRM)
        End Sub

    End Class

    Protected Shared Sub NotifyChanges(navCtrl As INavigationController)
        navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) = True
    End Sub

    Protected Shared Function CheckForChanges(navCtrl As INavigationController) As Boolean
        If navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) IsNot Nothing AndAlso
           CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG), Boolean) = True Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Handle Dropdown Events"

    Private Sub cboCauseOfLossId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCauseOfLossId.SelectedIndexChanged
        Try
            With State.MyBO
                PopulateBOProperty(State.MyBO, "CauseOfLossId", cboCauseOfLossId)
                If (Not State.IsMultiAuthClaim) Then
                    Dim claim As Claim = CType(State.MyBO, Claim)
                    If Not .CauseOfLossId = Guid.Empty Then
                        claim.ClaimSpecialServiceId = claim.GetSpecialServiceValue()
                    Else
                        claim.ClaimSpecialServiceId = State.NoId
                    End If
                    PopulateAuthorizedAmountFromPGPrices()
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cbousershipaddress_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbousershipaddress.SelectedIndexChanged

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True
            UserControlContactInfo.EnableDisablecontrol(False)
            UserControlAddress.EnableDisablecontrol(False)
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

    Private Sub cboFulfilmentMethod_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboFulfilmentMethod.SelectedIndexChanged
        If cboFulfilmentMethod.SelectedValue = Codes.FULFILMENT_METHOD_REIMBURSEMENT Then
            ControlMgr.SetVisibleControl(Me, LabelAccountNumber, True)
            ControlMgr.SetVisibleControl(Me, TextboxAccountNumber, True)
            SetEnabledForControlFamily(TextboxAccountNumber, State.IsEditMode, True)
        ElseIf cboFulfilmentMethod.SelectedValue = String.Empty Then
            ControlMgr.SetVisibleControl(Me, LabelAccountNumber, False)
            ControlMgr.SetVisibleControl(Me, TextboxAccountNumber, False)
            TextboxAccountNumber.Text = String.Empty
            SetEnabledForControlFamily(TextboxAccountNumber, State.IsEditMode, False)
        End If

    End Sub

#End Region

#Region "Grid related"

    Public Sub PopulateGrid()
        GridClaimAuthorization.AutoGenerateColumns = False
        GridClaimAuthorization.DataSource = State.claimAuthList
        GridClaimAuthorization.DataBind()
        If (State.claimAuthList.Count > 0) Then
            State.IsGridVisible = True
        Else
            State.IsGridVisible = False
        End If
        ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, State.IsGridVisible)
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound

        Try
            Dim claimAuth As ClaimAuthorization = CType(e.Row.DataItem, ClaimAuthorization)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(1).FindControl("EditButton_WRITE") IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = claimAuth.Id.ToString
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = claimAuth.AuthorizationNumber
                End If

                ''''supress the service center name if the authorization type is not Purchase Order
                If (Not String.IsNullOrWhiteSpace(claimAuth.AuthTypeXcd) AndAlso claimAuth.AuthTypeXcd <> "AUTH_TYPE-PURCHASE_ORDER") Then
                    e.Row.Cells(GRID_COL_SERVICE_CENTER_NAME_IDX).Text = "NA"
                End If

                ' Convert short status codes to full description with css
                e.Row.Cells(GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, claimAuth.ClaimAuthorizationStatusCode)
                e.Row.Cells(GRID_COL_AMOUNT_IDX).Text = GetAmountFormattedString(claimAuth.AuthorizedAmount.Value)
                e.Row.Cells(GRID_COL_CREATED_DATETIME_IDX).Text = GetLongDate12FormattedStringNullable(claimAuth.CreatedDateTime.Value)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridClaimAuthorization.RowCommand
        Dim claimAuthorizationId As Guid
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    claimAuthorizationId = New Guid(e.CommandArgument.ToString())
                    NavController.Navigate(Me, "claim_auth_detail", New ClaimAuthorizationDetailForm.Parameters(CType(State.MyBO, MultiAuthClaim), claimAuthorizationId, Guid.Empty))
                    'Me.callPage(ClaimAuthorizationDetailForm.URL, New ClaimAuthorizationDetailForm.Parameters(CType(Me.State.MyBO, MultiAuthClaim), claimAuthorizationId, Guid.Empty))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridClaimAuthorization.Sorting
        Try
            If (State.claimAuthList.Count > 0) Then
                State.IsGridVisible = True
            Else
                State.IsGridVisible = False
            End If
            ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, State.IsGridVisible)

            GridClaimAuthorization.DataSource = Sort(State.claimAuthList, e.SortExpression, e.SortDirection)
            GridClaimAuthorization.DataBind()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Function Sort(list As List(Of ClaimAuthorization), sortBy As String, sortDirection As WebControls.SortDirection) As List(Of ClaimAuthorization)
        Dim propInfo As Reflection.PropertyInfo = list.GetType().GetGenericArguments()(0).GetProperty(sortBy)

        If sortDirection = WebControls.SortDirection.Ascending Then
            Return list.OrderBy(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        Else
            Return list.OrderByDescending(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        End If
    End Function

    Private Sub ClaimForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
    End Sub

    'Private Sub btnResendGiftCard_Click(sender As Object, e As EventArgs) Handles btnResendGiftCard.Click
    '    Dim argumentsToAddEvent As String
    '    Dim giftCardType As String


    '    Dim request As New GenerateGiftCardRequest
    '    Dim response As New GenerateGiftCardResponse
    '    request.ApplicationSource = "NOMAD"
    '    request.ClaimNumber = "AVOIR-20170428_081501"
    '    request.GiftCardType = "PSE_GRN"
    '    request.Amount = "12.50"
    '    request.Domiciliation = "907001"
    '    request.FirstName = "SMITH"
    '    request.LastName = "JOHN"
    '    request.ZipCode = "75015"
    '    request.PhoneNumber = "0123456789"
    '    request.Email = "somebody@somewhere.com"

    '    Dim ds As IDartyServiceManager
    '    response = ds.ActivateDartyGiftCard(request)


    '    With Me.State.MyBO
    '        Dim attvalue As AttributeValue = Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).First()

    '        giftCardType = attvalue.Value
    '        'Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE And DateTime.Today >= i.EffectiveDate And DateTime.Today < i.ExpirationDate).First().Value

    '        argumentsToAddEvent = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimNumber:" & .ClaimNumber & ";Amount: " & TextboxAssurantPays.Text & ";GiftCardType:" & giftCardType & ""
    '        PublishedTask.AddEvent(companyGroupId:=Guid.Empty,
    '                                   companyId:=Guid.Empty,
    '                                   countryId:=Guid.Empty,
    '                                   dealerId:= .Dealer.Id,
    '                                   productCode:=String.Empty,
    '                                   coverageTypeId:=Guid.Empty,
    '                                   sender:="Claim Details",
    '                                   arguments:=argumentsToAddEvent,
    '                                   eventDate:=DateTime.UtcNow,
    '                                   eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLM_RESEND_REIMBURSE_INFO),
    '                                   eventArgumentId:=Nothing)



    '    End With
    'End Sub

    'Private Sub EnableDisableResendGiftcard()
    '    ControlMgr.SetVisibleControl(Me, btnResendGiftCard, False)

    '    If (Me.State.MyBO.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
    '        If (Me.State.MyBO.Status = BasicClaimStatus.Closed) Then
    '            Dim dt As DataTable = Disbursement.GetDisbursementFromClaim(Me.State.MyBO.Id)
    '            If (dt.Rows.Count > 0 AndAlso dt.Rows(0)("payment_method") = Codes.PAYMENT_METHOD__DARTY_GIFT_CARD) Then
    '                ControlMgr.SetVisibleControl(Me, btnResendGiftCard, True)
    '            End If
    '        End If
    '    End If
    'End Sub
#End Region

#Region "Call To claim Fulfillment WebAppGateway"

    Public Sub BindclaimFulfillmentDetails()
        If Me.State.MyBO.FulfillmentProviderType = FulfillmentProviderType.DynamicFulfillment Then
            BindExternalClaimFulfillmentDetails()
        Else
            BindElitaClaimFulfillmentDetails()
        End If

    End Sub
    Private Sub BindElitaClaimFulfillmentDetails()
        Dim wsRequest As GetFulfillmentDetailsRequest = New GetFulfillmentDetailsRequest()
        Dim wsResponse As FulfillmentDetails

        wsRequest.CompanyCode = State.MyBO.Company.Code
        wsRequest.ClaimNumber = State.MyBO.ClaimNumber

        Try
            wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, FulfillmentDetails)(
                                                       GetClaimFulfillmentWebAppGatewayClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(c As WebAppGatewayClient)
                                                           Return c.GetFulfillmentDetails(wsRequest)
                                                       End Function)

            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(FulfillmentDetails) Then
                    State.FulfillmentDetailsResponse = wsResponse
                    PopulateClaimFulfillmentDetails()
                End If
            End If

        Catch ex As Exception
            ClearClaimFulfillmentDetails()
        End Try
    End Sub

    Private Sub BindExternalClaimFulfillmentDetails()
        Try
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_DF_API_URL), True)
            If String.IsNullOrEmpty(oWebPasswd.Url) Then
                Throw New ArgumentNullException($"Web Password entry not found or Dynamic Fulfillment Api Url not configured for Service Type {Codes.SERVICE_TYPE_DF_API_URL}")
            ElseIf String.IsNullOrEmpty(oWebPasswd.UserId) Or String.IsNullOrEmpty(oWebPasswd.Password) Then
                Throw New ArgumentNullException($"Web Password username or password not configured for Service Type {Codes.SERVICE_TYPE_DF_API_URL}")
            End If

            Dim uri As String = String.Format(oWebPasswd.Url, "Elita", State.MyBO.ClaimNumber)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim client As HttpClient = New HttpClient()
            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Add(oWebPasswd.UserId, oWebPasswd.Password)
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Dim response As HttpResponseMessage = client.GetAsync(uri).GetAwaiter().GetResult()
            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"There is an error in displaying the DF UI. {response.ReasonPhrase}")
            End If

            Dim userInterfaceSettings = JsonConvert.DeserializeObject(Of JObject)(response.Content.ReadAsStringAsync().GetAwaiter.GetResult())
            Dim dfControl As DynamicFulfillmentUI = Page.LoadControl("~/Common/DynamicFulfillmentUI.ascx")
            dfControl.SourceSystem = "Elita"
            dfControl.SubscriptionKey = oWebPasswd.Password
            dfControl.CssUri = userInterfaceSettings("resourceUris")("cssUri")
            dfControl.ScriptUri = userInterfaceSettings("resourceUris")("scriptUri")
            dfControl.ClaimNumber = getClaimKey(State.MyBO.Company.Code, State.MyBO.ClaimNumber)
            phDynamicFulfillmentUI.Controls.Add(dfControl)
            dvClaimFulfillmentDetails.Visible = False

        Catch ex As Exception

        End Try
    End Sub
    Private Function getClaimKey(companyCode As String, claimNumber As String) As String
        Dim handler As New DynamicFulfillmentKeyHandler()
        Dim keys As New Dictionary(Of String, String)
        Dim tenant As String = $"{GetTenant(ElitaConfig.Current.General.Environment)}-{ElitaConfig.Current.General.Hub.ToLower()}"
        keys.Add("Tenant", tenant)
        keys.Add("CompanyCode", companyCode)
        keys.Add("ClaimNumber", claimNumber)
        Return handler.Encode(keys)
    End Function

    Private Function GetTenant(value As Environments) As String
        Select Case value
            Case Environments.Development
                Return "dev"
            Case Environments.Model
                Return "modl"
            Case Environments.Production
                Return "prod"
            Case Environments.Test
                Return "test"
            Case Else
                Throw New ArgumentException($"Environment value {value}, not implemented")
        End Select
    End Function

    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function

    Function GetPasscode(trackingNumber As String) As String
        Try
            If trackingNumber.Length > 0 Then
                Dim oServiceClient As RestClient
                Dim oServiceRequest As RestRequest
                Dim oServiceResponse As IRestResponse
                Dim jsnResult
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__WEB_API_LOCKER_PASSCODE), True)
                oServiceClient = New RestClient(oWebPasswd.Url)
                oServiceRequest = New RestRequest(Method.POST)

                oServiceRequest.AddHeader(oWebPasswd.UserId, oWebPasswd.Password)
                oServiceRequest.AddHeader("Content-type", "application/json")
                'oServiceRequest.Resource = "api/PolicyEnroll"
                oServiceRequest.RequestFormat = DataFormat.Json
                oServiceRequest.AddJsonBody(New With {Key .trackingNumber = trackingNumber})

                oServiceResponse = oServiceClient.Execute(oServiceRequest)
                jsnResult = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(oServiceResponse.Content)
                If jsnResult IsNot Nothing Then
                    Return jsnResult.Item("passcode").ToString()
                Else
                    Return ""
                End If
            Else
                Return ""

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            Return ""
        End Try

    End Function

#End Region

#Region "Change Fulfillment"
    Protected Sub btnLegacyContinue_Click(sender As Object, e As EventArgs) Handles btnLegacyContinue.Click


        Try
            Dim payLoad As ClientEventPayLoad = JsonConvert.DeserializeObject(Of ClientEventPayLoad)(hdnData.Value)

            ChangeFulfillmentAction(State.MyBO.Certificate.Id,
                                    State.MyBO.Id,
                                    Nothing,
                                    Codes.CasePurposeChangeFulfillment,
                                    State.IsCallerAuthenticated)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ChangeFulfillmentAction(certificateId As Guid,
                                        claimId As Guid,
                                        caseId As Guid,
                                        casePurpose As String,
                                        isCallerAuthenticated As Boolean)
        Try
            Dim claimRecordingParameters = New ClaimRecordingForm.Parameters(certificateId,
                                                                             claimId,
                                                                             caseId,
                                                                             casePurpose,
                                                                             isCallerAuthenticated)

            NavController.Navigate(Me, FlowEvents.EventClaimRecordingChangeFulfillment, claimRecordingParameters)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class





