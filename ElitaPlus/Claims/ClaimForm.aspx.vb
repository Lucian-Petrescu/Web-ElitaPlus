'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/2/2004)  ********************
Imports Microsoft.VisualBasic
Imports Codes = Assurant.ElitaPlus.BusinessObjectsNew.Codes
Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService

Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Net

Imports RestSharp
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json.Linq
Imports Assurant.Elita.ExternalKeyHandler.DynamicFulfillment
Imports Assurant.Elita.Configuration

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
    Public Const GRID_COL_CREATED_DATETIME_IDX As Integer = 3
    Public Const GRID_COL_STATUS_CODE_IDX As Integer = 4
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

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public IsCallerAuthenticated As Boolean = False
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.LastOperation = LastOp
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            Else
                If Me.NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(Me.NavController.State, MyState)
                    Me.StartNavControl()
                    Me.NavController.State = s
                End If
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallFromUrl.Contains(ClaimRecordingForm.Url2) Then
                ' Remove the Claim Recording page from the stack(return path flow)
                MyBase.SetPageOutOfNavigation()
            End If

            If Not Me.CallingParameters Is Nothing Then
                Me.StartNavControl()
                Try
                    Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(Me.CallingParameters, Guid))
                Catch ex As Exception
                    Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(Me.CallingParameters, Parameters).claimId)
                    Me.State.IsCallerAuthenticated = CType(Me.CallingParameters, Parameters).IsCallerAuthenticated
                End Try
                If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then Me.State.IsMultiAuthClaim = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Protected Sub InitializeFromFlowSession()

        Me.State.InputParameters = TryCast(Me.NavController.ParametersPassed, Parameters)

        Try
            If Not Me.State.InputParameters Is Nothing Then
                Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(Me.State.InputParameters.claimId, Guid))
                If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then Me.State.IsMultiAuthClaim = True
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public claimId As Guid
        Public updatedClaimAuthDetail As ClaimAuthDetailForm.ReturnType 'DEF-17426
        Public IsCallerAuthenticated As Boolean = True

        Public Sub New(ByVal claimId As Guid)
            Me.claimId = claimId
        End Sub

        'DEF-17426
        Public Sub New(ByVal claimId As Guid, ByVal updatedClaimAuthDetail As ClaimAuthDetailForm.ReturnType, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.claimId = claimId
            Me.updatedClaimAuthDetail = updatedClaimAuthDetail
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub

        Public Sub New(ByVal claimId As Guid, Optional ByVal IsCallerAuthenticated As Boolean = False)
            Me.claimId = claimId
            Me.IsCallerAuthenticated = IsCallerAuthenticated
        End Sub

    End Class
#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State.MyBO Is Nothing) Then
                Me.MasterPage.BreadCrum = String.Format("{0}{1}{2} {3}", Me.MasterPage.PageTab, ElitaBase.Sperator, TranslationBase.TranslateLabelOrMessage("CLAIM"), Me.State.MyBO.ClaimNumber)
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

        Me.MasterPage.MessageController.Clear()
        Try
            If Me.NavController.CurrentNavState.Name <> "CLAIM_DETAIL" Then
                If Me.NavController.CurrentNavState.Name <> "DENIED_CLAIM_CREATED" AndAlso Me.NavController.CurrentNavState.Name <> "CLAIM_ISSUE_APPROVED_FROM_CLAIM" _
                   AndAlso Me.NavController.CurrentNavState.Name <> "CLAIM_ISSUE_APPROVED_FROM_CERT" Then
                    Return
                End If
            End If

            'DEF-17426
            'If Me.NavController.Context = "CLAIM_DETAIL-AUTH_DETAIL-CLAIM_DETAIL" Then
            If (Not Me.NavController Is Nothing) AndAlso (Not Me.NavController.PrevNavState Is Nothing) Then
                If (Me.NavController.PrevNavState.Name = "AUTH_DETAIL") Then
                    Dim retObj As ClaimAuthDetailForm.ReturnType = CType(Me.NavController.ParametersPassed, Parameters).updatedClaimAuthDetail
                    If Not Me.NavController.ParametersPassed Is Nothing Then
                        If retObj.HasDataChanged Or retObj.HasClaimStatusBOChanged Then
                            Me.State.MyBO = retObj.ClaimBO
                            Me.State.HasAuthDetailDataChanged = retObj.HasDataChanged
                            Me.State.HasClaimStatusBOChanged = retObj.HasClaimStatusBOChanged
                            CType(Me.State.MyBO, Claim).UpdateClaimAuthorizedAmount(retObj.ClaimAuthDetailBO)
                            Me.State.ClaimAuthDetailBO = retObj.ClaimAuthDetailBO
                            Me.State.PartsInfoDV = retObj.PartsInfoDV
                        Else
                            Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                        End If
                    Else
                        Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                    End If
                ElseIf NavController.PrevNavState.Url = ClaimIssueDetailForm.URL AndAlso Not Me.IsPostBack Then
                    ucClaimConsequentialDamage.UpdateConsequentialDamagestatus(State.MyBO)
                    State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.MyBO.Id)
                    'ElseIf NavController.PrevNavState.Url = NewClaimForm.URL AndAlso Not Me.IsPostBack Then
                    '    If(Not NavController.ParametersPassed Is nothing AndAlso Not CType(NavController.ParametersPassed,Claim) is Nothing)
                    '        State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(NavController.ParametersPassed,Claim).Id)
                    '    End If
                End If
            End If

            If Not Me.IsPostBack Then
                InitializeData()
                'Return to the calling screen if status is pending
                If Me.State.MyBO.Status = BasicClaimStatus.Pending Then
                    Dim myBo As ClaimBase = Me.State.MyBO
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, Me.State.IsCallerAuthenticated)
                    Me.NavController = Nothing
                    Me.ReturnToCallingPage(retObj)
                End If
                InitializeUI()
                NotifyChanges(Me.NavController)
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisablePageControls()
                'Me.State.AuthorizedAmount = GetPrice()
                Me.State.PrevAuthorizedAmt = If(Me.State.IsMultiAuthClaim, CType(Me.State.MyBO, MultiAuthClaim).AuthorizedAmount, CType(Me.State.MyBO, Claim).AuthorizedAmount)
                Me.State.PrevDeductible = Me.State.MyBO.Deductible
                If (Me.State.IsMultiAuthClaim) Then
                    TranslateGridHeader(GridClaimAuthorization)
                    PopulateGrid()
                    Me.dvClaimAuthorizationDetails.Visible = True
                    Me.SetEnabledForControlFamily(dvClaimAuthorizationDetails, True, True)
                Else
                    Me.dvClaimAuthorizationDetails.Visible = False
                    DisabledTabsList.Add(Tab_ClaimAuthorization)
                    DisabledTabsList.Add(Tab_ConsequentialDamage)
                End If

                If Me.State.MyBO.CertificateItem.IsEquipmentRequired Then
                    PopulateClaimEquipment()
                    dvClaimEquipment.Visible = True
                Else
                    dvClaimEquipment.Visible = False
                End If

                If Not dvClaimEquipment.Visible And Not Me.dvClaimAuthorizationDetails.Visible Then
                    dvClaimEquipment.Visible = False
                    dvClaimAuthorizationDetails.Visible = False
                    ucClaimConsequentialDamage.Visible = False
                    'ViewPanel_READ1.Visible = False
                Else
                    ViewPanel_READ1.Visible = True
                End If

                dvRefurbReplaceEquipment.Visible = True

                'REQ-5467
                If (Me.IsNewUI) Then
                    If Me.State.MyBO.Dealer.IsLawsuitMandatoryId = Me.State.YesId Then
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

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
                If Not Me.State.MyBO.EnrolledEquipment Is Nothing Then
                    Me.AddLabelDecorations(Me.State.MyBO.EnrolledEquipment)
                End If
                If Not Me.State.MyBO.ClaimedEquipment Is Nothing Then
                    Me.AddLabelDecorations(Me.State.MyBO.ClaimedEquipment)
                End If
                CheckIfComingFromAuthDetailForm()
                CheckClaimPaymentInProgress()
            End If


        Catch ex As Threading.ThreadAbortException
            System.Threading.Thread.ResetAbort()
        Catch ex As Exception
            CleanPopupInput()
            CleanHiddenLimitExceededInput()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub GetDisabledTabs()
        Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
        If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
            DisabledTabsList.AddRange(DisabledTabs)
            hdnDisabledTab.Value = String.Empty
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            If Me.CalledUrl = Certificates.CertificateForm.URL Then
                Me.NavController = CType(MyBase.State, BaseState).NavCtrl
            End If

            'if coming from ClaimAuthDetail form reload claim
            If Me.CalledUrl = ClaimAuthDetailForm.URL Then
                Dim retObj As ClaimAuthDetailForm.ReturnType = CType(ReturnPar, ClaimAuthDetailForm.ReturnType)
                If Not retObj Is Nothing AndAlso (retObj.HasDataChanged Or retObj.HasClaimStatusBOChanged) Then
                    Me.State.MyBO = retObj.ClaimBO
                    Me.State.HasAuthDetailDataChanged = retObj.HasDataChanged
                    Me.State.HasClaimStatusBOChanged = retObj.HasClaimStatusBOChanged
                    CType(Me.State.MyBO, Claim).UpdateClaimAuthorizedAmount(retObj.ClaimAuthDetailBO)
                    Me.State.ClaimAuthDetailBO = retObj.ClaimAuthDetailBO
                    Me.State.PartsInfoDV = retObj.PartsInfoDV
                Else
                    Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                End If
            ElseIf Me.CalledUrl = MasterClaimForm.URL Then
                Dim retObj As MasterClaimForm.ReturnType = CType(ReturnPar, MasterClaimForm.ReturnType)
                If Not retObj Is Nothing Then
                    Me.StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.ClaimBO.Id, Guid))
                    Me.State.MyBO = claimTmp
                End If
            ElseIf Me.CalledUrl = Me.COVERAGE_TYPE_URL Then
                Dim retObj As Claims.CoverageTypeList.ReturnType = CType(ReturnPar, Claims.CoverageTypeList.ReturnType)
                If Not retObj Is Nothing Then
                    Me.StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.ClaimBO.Id, Guid))
                    Me.State.MyBO = claimTmp
                End If
            ElseIf Me.CalledUrl = Me.CLAIM_STATUS_DETAIL_FORM Then
                Dim retObj As ClaimStatusDetailForm.ReturnType = CType(ReturnPar, ClaimStatusDetailForm.ReturnType)
                If Not retObj Is Nothing Then
                    Me.StartNavControl()
                    Dim claimTmp As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(retObj.claimId, Guid))
                    Me.State.MyBO = claimTmp
                End If
            ElseIf Me.CalledUrl = ClaimAuthorizationDetailForm.URL Then
                Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
            ElseIf Me.CalledUrl = ClaimDetailsForm.Url Then
                Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                If Not retObj Is Nothing Then
                    Me.StartNavControl()
                    Me.State.MyBO = retObj.EditingBo
                    Me.State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                End If
            ElseIf Me.CalledUrl = ClaimRecordingForm.Url Then
                Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
                If Not retObj Is Nothing Then
                    Me.StartNavControl()
                    Me.State.MyBO = retObj.EditingBo
                    Me.State.IsCallerAuthenticated = retObj.IsCallerAuthenticated
                End If
            End If
            If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then Me.State.IsMultiAuthClaim = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Controlling Logic"

    Private Sub EnableDisablePageControls()

        ControlMgr.SetVisibleControl(Me, Me.TextboxClaimStatus, True)

        If Me.State.MyBO.Company.MasterClaimProcessingId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_MASTERCLAIMPROC, Codes.MasterClmProc_NONE)) Then
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelMasterClaimNumber, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxMasterClaimNumber, False, True)
        End If

        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.State.MyBO.Company.CompanyTypeId) = Me.State.MyBO.Company.COMPANY_TYPE_INSURANCE Then
            ControlMgr.SetVisibleControl(Me, Me.LabelCaller_Tax_Number, True)
            ControlMgr.SetVisibleControl(Me, Me.TextboxCaller_Tax_Number, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.LabelCaller_Tax_Number, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxCaller_Tax_Number, False)
        End If

        If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxFraudulent, True)
            ControlMgr.SetVisibleControl(Me, Me.lblPotFraudulent, True)
            ControlMgr.SetVisibleControl(Me, Me.TextboxComplaint, True)
            ControlMgr.SetVisibleControl(Me, Me.lblComplaint, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.cboDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxFraudulent, False)
            ControlMgr.SetVisibleControl(Me, Me.lblPotFraudulent, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxComplaint, False)
            ControlMgr.SetVisibleControl(Me, Me.lblComplaint, False)

        End If


        If Me.State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
            cbousershipaddress.SelectedValue = Me.State.NoId.ToString()
            moUserControlContactInfo.Visible = False
        Else
            cbousershipaddress.SelectedValue = Me.State.YesId.ToString
            moUserControlContactInfo.Visible = True
        End If

        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            ControlMgr.SetVisibleControl(Me, Me.Lbluseshipaddress, True)
            ControlMgr.SetVisibleControl(Me, Me.cbousershipaddress, True)
        End If

        Me.SetEnabledForControlFamily(Me.ViewPanel_READ, False)
        Me.SetEnabledForControlFamily(Me.ViewPanel_READ2, False)

        Me.SetEnabledForControlFamily(Me.txtRefurbReplaceComments, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.txtShipToSC, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.txtShipToCust, Me.State.IsEditMode, True)

        If (Me.State.MyBO.Status = BasicClaimStatus.Closed) Then
            Me.SetEnabledForControlFamily(Me.LabelReasonClosed, Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.cboReasonClosed, Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.cbousershipaddress, Me.State.IsEditMode, True)

        Else
            EnableEditableFieldsByDefault()
            EnableDisableEditableFieldsForActiveClaims()

            If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
                Me.SetEnabledForControlFamily(Me.LabelDeniedReason, Me.State.IsEditMode, True)
                Me.SetEnabledForControlFamily(Me.cboDeniedReason, Me.State.IsEditMode, True)
                Me.SetEnabledForControlFamily(Me.TextboxAuthorizationNumber, Me.State.IsEditMode, True)
            End If
        End If

        'Handling of Who Pays, Notification Type and Store Number
        If (Me.State.MyBO.NotificationTypeId.Equals(Guid.Empty)) Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxStoreNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxNotificationType, False)
            ControlMgr.SetVisibleControl(Me, Me.cboWhoPays, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelStoreNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelNotificationType, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelWhoPays, False)
        End If

        Me.SetEnabledForControlFamily(Me.LabelFulfilmentMethod, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.cboFulfilmentMethod, Me.State.IsEditMode, True)

        EnableButtonsByDefault()
        'Now Enable/Disable make Visible/Invisible buttons based on the BO Properties and Business Rules
        EnableDisableButtonsConditionally()

        DisableButtonsForClaimSystem()

        'Overide the visiblity setting for MultiAuthClaim
        EnableDisableControlsForMultiAuthClaim()

        ' If claim is readonly then disable all action button
        DisableButtonsForReadonlyClaim()

        If Not Me.State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelCurrentOdometer, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxCurrentOdometer, False, True)
        End If
    End Sub

    Private Sub EnableDisableControlsForMultiAuthClaim()
        ' Show Hide controls on basis of Old and New Claim
        ControlMgr.SetVisibleControl(Me, Me.LabelServiceCenter, Me.LabelServiceCenter.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxServiceCenter, Me.TextboxServiceCenter.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelLoanerCenter, Me.LabelLoanerCenter.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxLoanerCenter, Me.TextboxLoanerCenter.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelAuthorizationNumber, Me.LabelAuthorizationNumber.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxAuthorizationNumber, Me.TextboxAuthorizationNumber.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelSource, Me.LabelSource.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxSource, Me.TextboxSource.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelVisitDate, Me.LabelVisitDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxVisitDate, Me.TextboxVisitDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonVisitDate, Me.ImageButtonVisitDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelInvoiceProcessDate, Me.LabelInvoiceProcessDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxInvoiceProcessDate, Me.TextboxInvoiceProcessDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceProcessDate, Me.ImageButtonInvoiceProcessDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelRepairDate, Me.LabelRepairDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxRepairDate, Me.TextboxRepairDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonRepairDate, Me.ImageButtonRepairDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelInvoiceDate, Me.LabelInvoiceDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxInvoiceDate, Me.TextboxInvoiceDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelBatchNumber, Me.LabelBatchNumber.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxBatchNumber, Me.TextboxBatchNumber.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelPickUpDate, Me.LabelPickUpDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxPickupDate, Me.TextboxPickupDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickupDate, Me.ImageButtonPickupDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelLoanerReturnedDate, Me.LabelLoanerReturnedDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxLoanerReturnedDate, Me.TextboxLoanerReturnedDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.ImageButtonLoanerReturnedDate, Me.ImageButtonLoanerReturnedDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelDefectReason, Me.LabelDefectReason.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxDefectReason, Me.TextboxDefectReason.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelExpectedRepairDate, Me.LabelExpectedRepairDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxExpectedRepairDate, Me.TextboxExpectedRepairDate.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelTechnicalReport, Me.LabelTechnicalReport.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxTechnicalReport, Me.TextboxTechnicalReport.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.LabelSpecialInstruction, Me.LabelSpecialInstruction.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.TextboxSpecialInstruction, Me.TextboxSpecialInstruction.Visible And Not Me.State.IsMultiAuthClaim)

        'Disable Buttons for MultiAuthClaim
        ControlMgr.SetVisibleControl(Me, Me.btnPrint, Me.btnPrint.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnServiceCenterInfo, Me.btnServiceCenterInfo.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnNewServiceCenter, Me.btnNewServiceCenter.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnShipping, Me.btnShipping.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnAuthDetail, Me.btnAuthDetail.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnUseRecoveries, Me.btnUseRecoveries.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnClaimDeniedInformation, Me.btnClaimDeniedInformation.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnRedo, Me.btnRedo.Visible And Not Me.State.IsMultiAuthClaim)
        ControlMgr.SetVisibleControl(Me, Me.btnPartsInfo, Me.btnPartsInfo.Visible And Not Me.State.IsMultiAuthClaim)

    End Sub

    Private Sub EnableEditableFieldsByDefault()

        Me.SetEnabledForControlFamily(Me.LabelReasonClosed, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.cboReasonClosed, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.cbousershipaddress, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.LabelAuthorizedAmount, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.LabelDeductible, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxDeductible, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.LabelPolicyNumber, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxPolicyNumber, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.LabelIsLawsuitId, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.cboLawsuitId, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.LabelFollowupDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxFollowupDate, Me.State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonFollowupDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.ImageButtonFollowupDate, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.LabelRepairDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxRepairDate, Me.State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonRepairDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.ImageButtonRepairDate, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.LabelDEVICE_ACTIVATION_DATE, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxDEVICE_ACTIVATION_DATE, Me.State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonDeviceActivationDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.ImageButtonDeviceActivationDate, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.labelEMPLOYEE_NUMBER, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxEMPLOYEE_NUMBER, Me.State.IsEditMode, True) 'Make it editable

        Me.SetEnabledForControlFamily(Me.LabelLoanerReturnedDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxLoanerReturnedDate, Me.State.IsEditMode, True) 'Make it editable
        ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonLoanerReturnedDate, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.ImageButtonLoanerReturnedDate, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.TextboxProblemDescription, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxSpecialInstruction, Me.State.IsEditMode, True)
        '5921
        Me.SetEnabledForControlFamily(Me.TextboxTrackingNumber, Me.State.IsEditMode, True)
        'REQ-5565
        If (Me.State.MyBO.Company.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")) Then
            Me.SetEnabledForControlFamily(Me.TextboxBatchNumber, Me.State.IsEditMode, True)
        Else
            Me.SetEnabledForControlFamily(Me.TextboxBatchNumber, Me.State.IsEditMode, False)
        End If

        If Me.State.MyBO.Status = BasicClaimStatus.Active And Me.State.MyBO.ClaimClosedDate Is Nothing Then
            Me.SetEnabledForControlFamily(Me.LabelLiabilityLimit, Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.TextboxLiabilityLimit, Me.State.IsEditMode, True)
        End If

        Me.SetEnabledForControlFamily(Me.LabelCurrentOdometer, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.TextboxCurrentOdometer, Me.State.IsEditMode, True)

        If Not (Me.State.MyBO.NotificationTypeId.Equals(Guid.Empty)) Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxStoreNumber, Not Me.State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, Me.TextboxNotificationType, Not Me.State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, Me.cboWhoPays, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelStoreNumber, Not Me.State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, Me.LabelNotificationType, Not Me.State.IsEditMode)
            'ControlMgr.SetVisibleControl(Me, Me.LabelWhoPays, False)

            Me.SetEnabledForControlFamily(Me.LabelWhoPays, Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.cboWhoPays, Me.State.IsEditMode, True)
        End If

        Me.SetEnabledForControlFamily(Me.txtRefurbReplaceComments, Me.State.IsEditMode, True)

        Me.SetEnabledForControlFamily(Me.txtShipToSC, Me.State.IsEditMode, True)
        Me.SetEnabledForControlFamily(Me.txtShipToCust, Me.State.IsEditMode, True)

        ControlMgr.SetVisibleControl(Me, Me.LabelLoanerRequested, True)
        ControlMgr.SetVisibleControl(Me, Me.TextboxLoanerRequested, True)
    End Sub

    Private Sub EnableDisableEditableFieldsForActiveClaims()

        If (Not Me.State.IsMultiAuthClaim) Then
            Dim claim As Claim = CType(Me.State.MyBO, Claim)
            If (Not (claim.LoanerCenterId.Equals(Guid.Empty))) Then
                'Loaner has been taken
                If (claim.LoanerReturnedDate Is Nothing) Then
                    'Loaner has NOT been returned
                    'For an Active Claim, when a Loaner has been taken and has NOT been Returned
                    'Disable the ReasonClosed field
                    'All other editable fields stay enabled

                    Me.SetEnabledForControlFamily(Me.LabelReasonClosed, False, True)
                    Me.SetEnabledForControlFamily(Me.cboReasonClosed, False, True) 'Disable it
                    Me.SetEnabledForControlFamily(Me.cbousershipaddress, False, True)

                End If
                If ((claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
                    (Not (claim.ReasonClosedId.Equals(Guid.Empty)))) Then
                    'This is a Replacement Claim
                    'Change the Label for the RepairDate field to "REPLACED_ON"
                    Me.LabelRepairDate.Text = Claim.REPLACED_ON
                End If
            Else
                'There is NO Loaner taken
                'Disable the LoanerReturnedDate field

                Me.SetEnabledForControlFamily(Me.LabelLoanerReturnedDate, False, True)
                Me.SetEnabledForControlFamily(Me.TextboxLoanerReturnedDate, False, True) 'Disable it
                ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonLoanerReturnedDate, False, True)
                Me.SetEnabledForControlFamily(Me.ImageButtonLoanerReturnedDate, False, True)
            End If
            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode, True)
            Me.SetEnabledForControlFamily(Me.TextboxVisitDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode, True)

            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode And Not claim.LoanerTaken, True)
            Me.SetEnabledForControlFamily(Me.ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode And Not claim.LoanerTaken, True)
            Me.SetEnabledForControlFamily(Me.TextboxPickupDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode And Not claim.LoanerTaken, True)

            'For an Active Claim, Disable the AuthorizedAmount field:
            '1. If there is an Invoice OR
            '2. (If the Item has been Repaired And (.MgrAuthAmountFlag = "Y")) OR
            '3. If ClaimActivity = REWORK
            If ((Not (claim.RepairDate Is Nothing) AndAlso (Me.State.MyBO.MgrAuthAmountFlag = "Y")) _
                OrElse (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) _
                OrElse (Not (claim.InvoiceProcessDate Is Nothing))) _
                Then
                If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__OFFICE_MANAGER) _
                   OrElse ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) _
                   OrElse ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT) Then
                    'By pass for this roles
                Else
                    Me.SetEnabledForControlFamily(Me.LabelAuthorizedAmount, False, True)
                    Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, False, True) 'Disable it
                End If
            End If
            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            ControlMgr.SetVisibleControl(Me, ImageButtonVisitDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, TextboxVisitDate, claim.CanDisplayVisitAndPickUpDates)
            ControlMgr.SetVisibleControl(Me, LabelVisitDate, claim.CanDisplayVisitAndPickUpDates)

            'when loaner is taken, Pick-Up Date should be visible in Display mode, disabled in Edit mode
            ControlMgr.SetVisibleControl(Me, ImageButtonPickupDate, claim.CanDisplayVisitAndPickUpDates And Me.State.IsEditMode)
            ControlMgr.SetVisibleControl(Me, TextboxPickupDate, claim.CanDisplayVisitAndPickUpDates)
            ControlMgr.SetVisibleControl(Me, LabelPickUpDate, claim.CanDisplayVisitAndPickUpDates)

            SetEnabledForControlFamily(Me.ImageButtonPickupDate, Me.State.IsEditMode And Not claim.LoanerTaken)
            SetEnabledForControlFamily(Me.TextboxPickupDate, Me.State.IsEditMode And Not claim.LoanerTaken)
            SetEnabledForControlFamily(Me.LabelReasonClosed, Me.State.IsEditMode And Not claim.LoanerTaken)

        Else
            Me.SetEnabledForControlFamily(Me.LabelLoanerReturnedDate, False, True)
            Me.SetEnabledForControlFamily(Me.TextboxLoanerReturnedDate, False, True) 'Disable it
            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonLoanerReturnedDate, False, True)
            Me.SetEnabledForControlFamily(Me.ImageButtonLoanerReturnedDate, False, True)
            Me.SetEnabledForControlFamily(Me.LabelAuthorizedAmount, False, True)
            Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, False, True) 'Disable it
            ControlMgr.SetVisibleControl(Me, LabelFulfilmentMethod, False)
            ControlMgr.SetVisibleControl(Me, cboFulfilmentMethod, False)
        End If


        If SpecialService.ChkIfSplSvcConfigured(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id,
                                                Me.State.MyBO.CoverageTypeId, Me.State.MyBO.Certificate.DealerId,
                                                Authentication.LangId, Me.State.MyBO.Certificate.ProductCode, False) _
           AndAlso Me.State.MyBO.Status = BasicClaimStatus.Active Then
            Me.SetEnabledForControlFamily(Me.LabelCauseOfLoss, True, True)
            Me.SetEnabledForControlFamily(Me.cboCauseOfLossId, True, True)
        End If

        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            ControlMgr.SetVisibleControl(Me, Me.TextboxPolicyNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelPolicyNumber, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.TextboxPolicyNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelPolicyNumber, False)
        End If

        If (Me.State.MyBO.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (Me.State.MyBO.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
            trDueToSCFromAssurant.Visible = False
        End If

        If (Me.State.MyBO.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
            ControlMgr.SetVisibleControl(Me, trUseEquipment1, False)
            ControlMgr.SetVisibleControl(Me, trUseEquipment2, False)
            ControlMgr.SetVisibleControl(Me, trUseEquipment3, False)
        Else
            ControlMgr.SetVisibleControl(Me, trUseEquipment1, True)
            ControlMgr.SetVisibleControl(Me, trUseEquipment2, True)
            ControlMgr.SetVisibleControl(Me, trUseEquipment3, True)
        End If

        If Me.State.MyBO.Dealer.DeductibleCollectionId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y) Then
            ControlMgr.SetVisibleControl(Me, trDedCollection, True)
        Else
            ControlMgr.SetVisibleControl(Me, trDedCollection, False)
        End If

        If (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK) Then
            'This is a Service Warranty Claim, so HIDE the Amount fields and their associated Labels:
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxLiabilityLimit, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelLiabilityLimit, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxDeductible, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelDeductible, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxAboveLiability, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelAboveLiability, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxSlavageAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelSalvageAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxAssurantPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelAssurantPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxConsumerPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelDueToSCFromAssurant, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxDueToSCFromAssurant, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelConsumerPays, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxDiscount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelDiscount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.TextboxBonusAmount, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.LabelBonusAmount, False, True)

            'For Service Warranty claim the authorization amount should not be editable 
            Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, False, True)
        End If

    End Sub

    Protected Sub EnableButtonsByDefault()

        ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, Not Me.State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, Me.btnSave_WRITE, Me.State.IsEditMode, True)
        ControlMgr.SetVisibleForControlFamily(Me, Me.btnUndo_WRITE, Me.State.IsEditMode, True)

        If (Not (Me.State.IsEditMode)) Then
            If Me.State.MyBO.IsClaimChild = Codes.YESNO_N Then
                'ReplaceItem button
                ControlMgr.SetVisibleControl(Me, btnReplaceItem, True)
                Me.btnReplaceItem.Enabled = True

                'ServiceWarranty button
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, True)
                Me.btnServiceWarranty.Enabled = True

                'PoliceReport button
                ControlMgr.SetVisibleControl(Me, btnPoliceReport, True)
                Me.btnPoliceReport.Enabled = True

                'Item button 
                '    Andres ControlMgr.SetVisibleControl(Me, btnItem, True)
                Me.btnItem.Enabled = True

                'NewCenter button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewServiceCenter, True, True)
                Me.SetEnabledForControlFamily(Me.btnNewServiceCenter, True, True)

                'ServiceCenterInfo button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnServiceCenterInfo, True, True)
                Me.SetEnabledForControlFamily(Me.btnServiceCenterInfo, True, True)

                'Auth Detail button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, True, True)
                Me.SetEnabledForControlFamily(Me.btnAuthDetail, True, True)

                'PartsInfo button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, True, True)
                Me.SetEnabledForControlFamily(Me.btnPartsInfo, True, True)

                'Certificate button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnCertificate, True, True)
                Me.SetEnabledForControlFamily(Me.btnCertificate, True, True)

                'RePrint button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnPrint, True, True)
                Me.SetEnabledForControlFamily(Me.btnPrint, True, True)

                'NewItemInfo button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewItemInfo, True, True)
                Me.SetEnabledForControlFamily(Me.btnNewItemInfo, True, True)

                'Comments button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnComment, True, True)
                Me.SetEnabledForControlFamily(Me.btnComment, True, True)

                'Reopen button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnReopen_WRITE, False, True)
                Me.SetEnabledForControlFamily(Me.btnReopen_WRITE, True, True)

                ''AuthDetail button
                'ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, False, True)
                'Me.SetEnabledForControlFamily(Me.btnAuthDetail, True, True)

                'Master Claim button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnMasterClaim, True, True)
                Me.SetEnabledForControlFamily(Me.btnMasterClaim, True, True)

                'Change Coverage
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnChangeCoverage, True, True)
                Me.SetEnabledForControlFamily(Me.btnChangeCoverage, True, True)

                'Extended Status button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnStatusDetail, True, True)
                Me.SetEnabledForControlFamily(Me.btnStatusDetail, True, True)

                'Claim Denied Information Button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnClaimDeniedInformation, True, True)
                Me.SetEnabledForControlFamily(Me.btnClaimDeniedInformation, True, True)

                'Repair and Logistics button
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnRepairLogistics, True, True)
                Me.SetEnabledForControlFamily(Me.btnRepairLogistics, True, True)

                ControlMgr.SetVisibleControl(Me, btnClaimImages, True)
                Me.btnClaimImages.Enabled = True

            End If
        End If

    End Sub

    Private Sub EnableDisableButtonsForSingleAuthClaim()
        Dim claim As Claim = CType(Me.State.MyBO, Claim)


        'For the ServiceWarranty button
        If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < Me.State.MyBO.GetShortDate(Me.State.MyBO.CreatedDate.Value))) OrElse
            (((Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
              (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) OrElse
              (Me.State.MyBO.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
              (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) AndAlso Not claim.CheckSvcWrantyClaimControl())) Then

            Me.btnServiceWarranty.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
        End If

        ' For Redo
        If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT) OrElse
            (claim.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) OrElse
            (claim.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
            (claim.InvoiceProcessDate IsNot Nothing)) Then

            Me.btnRedo.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnRedo, False)
        Else
            Dim claimwithDtExists As Boolean = claim.NumberOfAvailableClaims(Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.CreatedDate.Value, Me.State.MyBO.Id)

            If Not claimwithDtExists Then
                Me.btnRedo.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnRedo, False)
            End If
        End If


        'For the Deny Claim button
        If ((Me.State.MyBO.Status = BasicClaimStatus.Active) OrElse (Me.State.MyBO.Status = BasicClaimStatus.Pending)) And
           (Me.State.MyBO.TotalPaid.Value.Equals(0D)) And
           (Not (Me.State.MyBO.ClaimNumber.ToUpper.EndsWith("S"))) Then
            Me.btnDenyClaim.Enabled = True
            ControlMgr.SetVisibleControl(Me, Me.btnDenyClaim, True)
        End If

        'For the ReplaceItem button
        If ((Me.State.MyBO.Status <> BasicClaimStatus.Active) OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            ((Me.State.MyBO.Status = BasicClaimStatus.Active) And ((Not (claim.RepairDate Is Nothing)) AndAlso (claim.RepairDate.Value >= Me.State.MyBO.GetShortDate(Me.State.MyBO.CreatedDate.Value)))) OrElse
            ((Not (claim.InvoiceProcessDate Is Nothing)) AndAlso (claim.InvoiceProcessDate.Value >= claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make ReplaceItem button Invisible and Disabled
            Me.btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            Me.btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)

            'disable Reprint button for recovery claim
            Me.btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'if status is denied then display denied reason else hide it
        If (Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            'disable Reprint button for recovery claim
            Me.btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'Disable Reprint button for backend claims
        If (Not claim.RepairDate Is Nothing) AndAlso (Not claim.PickUpDate Is Nothing) Then
            Me.btnPrint.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPrint, False)
        End If

        'For the NewCenter button
        If ((Not (claim.LoanerCenterId.Equals(Guid.Empty))) OrElse
            ((Not (claim.InvoiceProcessDate Is Nothing)) AndAlso (claim.InvoiceProcessDate.Value >= claim.GetShortDate(claim.CreatedDate.Value))) OrElse
            (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (claim.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse
            ((Not (claim.RepairDate Is Nothing)) AndAlso (claim.RepairDate.Value >= claim.GetShortDate(claim.CreatedDate.Value)))) Then
            'Make the NewCenter button Invisible and Disabled

            ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewServiceCenter, False, True)
            Me.SetEnabledForControlFamily(Me.btnNewServiceCenter, False, True)
        End If

        'For the RePrint button
        If ((claim.Source <> String.Empty) AndAlso
            ((claim.ClaimActivityCode = String.Empty) OrElse (claim.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK))) Then

            'Make the RePrint button Invisible and Disabled
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPrint, False, True)
            Me.SetEnabledForControlFamily(Me.btnPrint, False, True)
        End If


        'For the NewItemInfo button
        If ((Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REPLACED AndAlso Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make the NewItemInfo button Invisible and Disabled
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewItemInfo, False, True)
            Me.SetEnabledForControlFamily(Me.btnNewItemInfo, False, True)
        End If

        'For the NewItemInfo, PartsInfo and Shipping buttons
        If (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            'Make the Replace Item button hidden
            ControlMgr.SetVisibleControl(Me, Me.btnReplaceItem, False)
            'Make the PartsInfo button hidden
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, False, True)
            'Make the AuthDetail button hidden
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, False, True)
            'Make the NewItemInfo button Visible
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewItemInfo, True, True)
            'Make the Shipping button Visible if applicable

            If Not Me.State.MyBO.ShippingInfoId.Equals(Guid.Empty) AndAlso Not claim.ServiceCenterId.Equals(Guid.Empty) Then
                Dim objServiceCenter As New ServiceCenter(claim.ServiceCenterId)
                If objServiceCenter.Shipping Then
                    ControlMgr.SetVisibleControl(Me, Me.btnShipping, True)
                    Me.SetEnabledForControlFamily(Me.btnShipping, True, True)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, Me.btnShipping, False)
            End If
        Else
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnNewItemInfo, False, True)
            ControlMgr.SetVisibleControl(Me, Me.btnShipping, False)
        End If

        'Due to lack of space in the form, the Auth Detail button is placed next to shipping button. This is done since
        'BR is not using the shipping info today. Countries that uses shipping today do not use the auth detail feature.
        'Make the AuthDetail/PartsInfo button visible
        If Me.State.AuthDetailEnabled AndAlso Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, Me.btnAuthDetail.Visible And True, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, False, True)
        Else
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnAuthDetail, Me.btnAuthDetail.Visible And False, True)
        End If

        If Me.State.AuthDetailEnabled AndAlso Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            Me.btnPartsInfo.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnPartsInfo, False)
            Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, False, True)
        Else
            Me.btnAuthDetail.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnAuthDetail, Me.btnAuthDetail.Visible And False)
            'Me.SetEnabledForControlFamily(Me.TextboxAuthorizedAmount, True, True)
        End If

        Me.State.searchDV = CertItemCoverage.GetClaimCoverageType(Me.State.MyBO.CertificateId, Me.State.MyBO.CertItemCoverageId,
                                                                  CType(Me.State.MyBO.LossDate, Date), Me.State.MyBO.StatusCode,
                                                                  If(claim.InvoiceProcessDate Is Nothing, Nothing, claim.InvoiceProcessDate.Value))
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) AndAlso
           (Not Me.State.searchDV Is Nothing) AndAlso (Me.State.searchDV.Count > 0) AndAlso
           (claim.RepairDate Is Nothing) AndAlso (claim.PickUpDate Is Nothing) Then
            ControlMgr.SetVisibleControl(Me, btnChangeCoverage, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnChangeCoverage, False)
        End If

        If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And
           Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK And
           (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Or
            Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP) And
           claim.InvoiceProcessDate Is Nothing And
           claim.RepairDate Is Nothing _
            Then
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, False)
        End If


        If Me.State.MyBO.Dealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS) Then
            'Make the ClaimCaseList button Visible
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnClaimCaseList, True, True)

        ElseIf Me.State.MyBO.Dealer.ClaimRecordingXcd.Equals(Codes.DEALER_CLAIM_RECORDING_BOTH) Then
            Dim dsCaseInfo As DataSet = New CaseBase().LoadCaseByClaimId(State.MyBO.Id)
            If (Not dsCaseInfo Is Nothing AndAlso dsCaseInfo.Tables.Count > 0 AndAlso dsCaseInfo.Tables(0).Rows.Count > 0) Then
                If (Not dsCaseInfo.Tables(0).Rows(0)("Case_id").Equals(Guid.Empty) And dsCaseInfo.Tables(0).Rows(0)("Case_Purpose_Code") = Codes.CASE_PURPOSE__REPORT_CLAIM) Then
                    'Make the ClaimCaseList button Visible when the claim recording is set to Both and if the claim is created from DCM
                    ControlMgr.SetVisibleForControlFamily(Me, Me.btnClaimCaseList, True, True)
                Else
                    'Make the ClaimCaseList button hidden
                    ControlMgr.SetVisibleForControlFamily(Me, Me.btnClaimCaseList, False, True)
                End If
            End If

        Else
            'Make the ClaimCaseList button hidden
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnClaimCaseList, False, True)
        End If


    End Sub

    Private Sub EnableDisableButtonsforMultiAuthCLaim()
        Dim claim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
        Dim strValidateSvcWty As String

        'For the Deny Claim button
        If ((Me.State.MyBO.Status = BasicClaimStatus.Active)) And
           (Not (Me.State.MyBO.ClaimNumber.ToUpper.EndsWith("S"))) And
           (Me.State.MyBO.TotalPaid.Value.Equals(0D)) And
           claim.HasNoReconsiledAuthorizations Then
            Me.btnDenyClaim.Enabled = True
            ControlMgr.SetVisibleControl(Me, Me.btnDenyClaim, True)
        End If

        'For the ServiceWarranty button --check the flag at Company level
        If (Me.State.MyBO.Company.AttributeValues.Contains(VALIDATE_SERVICE_WARRANTY)) Then
            strValidateSvcWty = Me.State.MyBO.Company.AttributeValues.Value(VALIDATE_SERVICE_WARRANTY)
        End If

        If Not String.IsNullOrEmpty(strValidateSvcWty) And strValidateSvcWty = YES Then
            If Not Me.State.MyBO.IsServiceWarrantyValid(Me.State.MyBO.Id) Then
                Me.btnServiceWarranty.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
            End If
        Else
            If (((claim.RepairDate Is Nothing) OrElse (claim.RepairDate.Value < Me.State.MyBO.GetShortDate(Me.State.MyBO.CreatedDate.Value))) OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT AndAlso State.MyBO.Dealer.DealerFulfillmentProviderClassCode <> Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS) OrElse
            (Me.State.MyBO.ReasonClosedCode = Codes.REASON_CLOSED__TO_BE_REPLACED) OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

                Me.btnServiceWarranty.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnServiceWarranty, False)
            End If
        End If


        'For the ReplaceItem button
        If ((Me.State.MyBO.Status <> BasicClaimStatus.Active) OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__TO_BE_REPLACED) OrElse
            Not claim.HasActiveAuthorizations OrElse
            (Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then

            'Make ReplaceItem button Invisible and Disabled
            Me.btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            Me.btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        'Start - REQ- 6156 - Disable for all Multi Auth Claim
        If Me.State.MyBO.Dealer.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS Then
            btnReplaceItem.Enabled = False
            ControlMgr.SetVisibleControl(Me, btnReplaceItem, False)
        End If

        'End - REQ- 6156 - Disable for all Multi Auth Claim

        If (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            'Make the PartsInfo button hidden
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, False, True)
        End If

        'Due to lack of space in the form, the Auth Detail button is placed next to shipping button. This is done since
        'BR is not using the shipping info today. Countries that uses shipping today do not use the auth detail feature.
        'Make the AuthDetail/PartsInfo button visible
        If Me.State.AuthDetailEnabled AndAlso Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, False, True)
        Else
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, True, True)
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

        If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE And
           Me.State.MyBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK And
           (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Or
            Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Or Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP) And
           claim.HasActiveAuthorizations _
            Then
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnClaimDeniedInformation, False)
        End If

        If Not Me.State.MyBO.Status = BasicClaimStatus.Denied Then
            Dim isConsequentialDamageConfigured As Boolean = Me.State.MyBO.isConsequentialDamageAllowed(Me.State.MyBO.Certificate.Product.Id)
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
            Me.btnClaimDeductibleRefund.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnClaimDeductibleRefund, True)
        End If
    End Sub

    Private Sub EnableDisableButtonsConditionally()

        If (Not (Me.State.IsEditMode)) Then

            If (Not Me.State.IsMultiAuthClaim) Then
                EnableDisableButtonsForSingleAuthClaim()
            Else
                EnableDisableButtonsforMultiAuthCLaim()
            End If

            'For PoliceReport button - Invisible and Disabled when cause of loss is not Theft
            If Not (Me.State.MyBO.CoverageTypeId.Equals(Guid.Empty)) Then
                If Me.State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                   AndAlso Me.State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__THEFT.ToUpper _
                   AndAlso (Me.State.MyBO.CoverageTypeCode.ToUpper <> Codes.COVERAGE_TYPE__LOSS.ToUpper _
                            OrElse (Me.State.MyBO.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper _
                                    AndAlso (Me.State.MyBO.Company.PoliceRptForLossCovId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)))) Then
                    Me.btnPoliceReport.Enabled = False
                    ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
                Else
                    If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                        Me.btnPoliceReport.Enabled = False
                        ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
                    End If
                End If
            Else
                Me.btnPoliceReport.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnPoliceReport, False)
            End If

            If Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Then
                If Me.State.MyBO.LossDate.Value >= Me.State.MyBO.Certificate.WarrantySalesDate.Value AndAlso
                   Me.State.MyBO.LossDate.Value < Me.State.MyBO.CertificateItemCoverage.BeginDate.Value Then
                    'its a denied claim opened before the coverage effective date, so can not be reopened
                    ControlMgr.SetVisibleForControlFamily(Me, Me.btnReopen_WRITE, False, True)
                Else
                    ControlMgr.SetVisibleForControlFamily(Me, Me.btnReopen_WRITE, True, True)
                End If
            End If

            If Me.State.MyBO.UseRecoveries = True Then
                Me.btnUseRecoveries.Enabled = True
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, True)
            Else
                Me.btnUseRecoveries.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, False)
            End If

            If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                Me.btnUseRecoveries.Enabled = False
                ControlMgr.SetVisibleControl(Me, btnUseRecoveries, False)
            End If
        End If

        If (Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnPartsInfo, False, True)
        End If


        CheckRetailPriceSearchVisibility()

        'EnableDisableResendGiftcard()

    End Sub

    'REQ-6230
    Protected Sub CheckRetailPriceSearchVisibility()
        Try
            Dim retailPriceSearch As String

            'Check the flag at Company level
            If (Me.State.MyBO.Dealer.AttributeValues.Contains(RETAIL_PRICE_SEARCH)) Then
                retailPriceSearch = Me.State.MyBO.Dealer.AttributeValues.Value(RETAIL_PRICE_SEARCH)
            End If

            If (Not String.IsNullOrEmpty(retailPriceSearch) And retailPriceSearch = YES) Then
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnPriceRetailSearch, True, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ActionButtonOptionEnableDisableVisible(ByVal isFlag As Boolean)
        btnDenyClaim.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnDenyClaim, isFlag)
        btnAuthDetail.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnAuthDetail, isFlag)
        btnChangeCoverage.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnChangeCoverage, isFlag)
        btnClaimHistoryDetails.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnClaimHistoryDetails, isFlag)
        btnOutboundCommHistory.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnOutboundCommHistory, isFlag)
        btnCertificate.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnCertificate, isFlag)
        btnComment.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnComment, isFlag)
        btnServiceCenterInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnServiceCenterInfo, isFlag)

        btnClaimDeniedInformation.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnClaimDeniedInformation, isFlag)
        btnStatusDetail.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnStatusDetail, isFlag)
        btnItem.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnItem, isFlag)
        btnMasterClaim.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnMasterClaim, isFlag)
        btnNewServiceCenter.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnNewServiceCenter, isFlag)
        btnNewItemInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnNewItemInfo, isFlag)
        btnPoliceReport.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnPoliceReport, isFlag)
        btnPartsInfo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnPartsInfo, isFlag)

        btnPrint.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnPrint, isFlag)
        btnUseRecoveries.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnUseRecoveries, isFlag)
        btnRedo.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnRedo, isFlag)
        btnReplaceItem.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnReplaceItem, isFlag)
        btnReopen_WRITE.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnReopen_WRITE, isFlag)
        btnShipping.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnShipping, isFlag)
        btnServiceWarranty.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnServiceWarranty, isFlag)
        btnRepairLogistics.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnRepairLogistics, isFlag)

        btnClaimIssues.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnClaimIssues, isFlag)
        btnClaimImages.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnClaimImages, isFlag)
        btnClaimCaseList.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnClaimCaseList, isFlag)
        btnAddConseqDamage.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnAddConseqDamage, isFlag)
        btnPriceRetailSearch.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnPriceRetailSearch, isFlag)

        btnChangeFulfillment.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnChangeFulfillment, isFlag)

        btnReplacementQuote.Enabled = isFlag
        ControlMgr.SetVisibleControl(Me, Me.btnReplacementQuote, isFlag)
    End Sub
    Protected Sub DisableButtonsForClaimSystem()
        If Not Me.State.MyBO.CertificateId.Equals(Guid.Empty) Or Me.State.MyBO.IsClaimChild = Codes.YESNO_Y Then
            Dim oClmSystem As New ClaimSystem(Me.State.MyBO.Dealer.ClaimSystemId)
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If oClmSystem.MaintainClaimId.Equals(noId) Or Me.State.MyBO.IsClaimChild = Codes.YESNO_Y Then
                If Me.ActionButton.Visible And Me.ActionButton.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.ActionButton, False)
                End If
                If Me.btnEdit_WRITE.Visible And Me.btnEdit_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, False)
                End If
                If Me.btnSave_WRITE.Visible And Me.btnSave_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
                End If
                If Me.btnUndo_WRITE.Visible And Me.btnUndo_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnUndo_WRITE, False)
                End If
            End If
        End If
    End Sub
    Protected Sub DisableButtonsForReadonlyClaim()
        If Me.State.MyBO.IsClaimReadOnly = Codes.YESNO_Y Then
            If Me.ActionButton.Visible And Me.ActionButton.Enabled Then
                If Me.btnCertificate.Visible And Me.btnCertificate.Enabled Then
                    ActionButtonOptionEnableDisableVisible(False)
                    btnCertificate.Enabled = True
                    ControlMgr.SetVisibleControl(Me, Me.btnCertificate, True)
                Else
                    ActionButtonOptionEnableDisableVisible(False)
                End If
            End If
            If Me.btnEdit_WRITE.Visible And Me.btnEdit_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, False)
            End If
            If Me.btnSave_WRITE.Visible And Me.btnSave_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
            End If
            If Me.btnUndo_WRITE.Visible And Me.btnUndo_WRITE.Enabled Then
                ControlMgr.SetEnableControl(Me, Me.btnUndo_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub HandleCloseClaimLogic()
        'Try to Close the Claim
        Select Case Me.State.MyBO.ClaimAuthorizationType
            Case ClaimAuthorizationType.Single
                CType(Me.State.MyBO, Claim).CloseTheClaim()
            Case ClaimAuthorizationType.Multiple
                Dim claim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificateNumber", Me.LabelCertificateNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UserName", Me.LabelUserName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerName", Me.LabelDealerName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RiskTypeId", Me.LabelRiskType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimActivityId", Me.LabelClaimActivity)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReasonClosedId", Me.LabelReasonClosed)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeniedReasonId", Me.LabelDeniedReason)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RepairCodeId", Me.LabelRepairCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CauseOfLossId", Me.LabelCauseOfLoss)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MethodOfRepairId", Me.LabelMethodOfRepair)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageTypeDescription", Me.LabelCoverageType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SpecialInstruction", Me.LabelSpecialInstruction)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LoanerRquestedXcd", Me.LabelLoanerRequested)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizedAmount", Me.LabelAuthorizedAmount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LiabilityLimit", Me.LabelLiabilityLimit)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Deductible", Me.LabelDeductible)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TotalPaid", Me.LabelTotalPaid)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AssurantPays", Me.LabelAssurantPays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DueToSCFromAssurant", Me.LabelDueToSCFromAssurant)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ConsumerPays", Me.LabelConsumerPays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AboveLiability", Me.LabelAboveLiability)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimsAdjusterName", Me.LabelClaimsAdjuster)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimClosedDate", Me.LabelClaimClosedDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FollowupDate", Me.LabelFollowupDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizationNumber", Me.LabelAuthorizationNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Source", Me.LabelSource)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AddedDate", Me.LabelAddedDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LastOperatorName", Me.LabelLastModifiedBy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LastModifiedDate", Me.LabelLastModifiedDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MasterClaimNumber", Me.LabelMasterClaimNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CallerTaxNumber", Me.LabelCaller_Tax_Number)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DiscountAmount", Me.LabelDiscount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PolicyNumber", Me.LabelPolicyNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "StoreNumber", Me.LabelStoreNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotificationTypeDescription", Me.LabelNotificationType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DedCollectionMethodID", Me.LabelDedCollMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DedCollectionCCAuthCode", Me.LabelCCAuthCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsLawsuitId", Me.LabelIsLawsuitId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProblemDescription", Me.LabelProblemDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BonusAmount", Me.LabelBonusAmount)
        '5921
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TrackingNumber", Me.LabelTrackingNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FulfilmentMethod", Me.LabelFulfilmentMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AccountNumber", Me.LabelAccountNumber)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "EmployeeNumber", Me.labelEMPLOYEE_NUMBER)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeviceActivationDate", Me.LabelDEVICE_ACTIVATION_DATE)

        If Not Me.State.MyBO.EnrolledEquipment Is Nothing Then
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "Manufacturer", Me.LBLeNROLLEDmAKE)
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "Model", Me.lblEnrolledModel)
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "SerialNumber", Me.lblEnrolledSerialNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "SKU", Me.lblEnrolledSKu)
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "IMEINumber", Me.lblEnrolledIMEI)
            Me.BindBOPropertyToLabel(Me.State.MyBO.EnrolledEquipment, "Comments", Me.lblEnrolledIMEI)
        End If

        If Not Me.State.MyBO.ClaimedEquipment Is Nothing Then
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "Manufacturer", Me.lblClaimedMake)
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "Model", Me.lblClaimedModel)
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "SerialNumber", Me.lblClaimedSerialNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "SKU", Me.lblClaimedSKu)
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "IMEINumber", Me.lblClaimedIMEI)
            Me.BindBOPropertyToLabel(Me.State.MyBO.ClaimedEquipment, "Comments", Me.lblClaimedIMEI)
        End If


        If Not Me.State.IsMultiAuthClaim Then
            Dim claimBo As Claim = CType(Me.State.MyBO, Claim)
            Me.BindBOPropertyToLabel(claimBo, "ServiceCenterId", Me.LabelServiceCenter)
            Me.BindBOPropertyToLabel(claimBo, "LoanerCenterId", Me.LabelLoanerCenter)
            Me.BindBOPropertyToLabel(claimBo, "RepairDate", Me.LabelRepairDate)
            Me.BindBOPropertyToLabel(claimBo, "InvoiceProcessDate", Me.LabelInvoiceProcessDate)
            Me.BindBOPropertyToLabel(claimBo, "InvoiceDate", Me.LabelInvoiceDate)
            Me.BindBOPropertyToLabel(claimBo, "LoanerReturnedDate", Me.LabelLoanerReturnedDate)
            Me.BindBOPropertyToLabel(claimBo, "VisitDate", Me.LabelVisitDate)
            Me.BindBOPropertyToLabel(claimBo, "PickUpDate", Me.LabelPickUpDate)
            Me.BindBOPropertyToLabel(claimBo, "WhoPaysId", Me.LabelWhoPays)
            Me.BindBOPropertyToLabel(claimBo, "DefectReason", Me.LabelDefectReason)
            Me.BindBOPropertyToLabel(claimBo, "TechnicalReport", Me.LabelTechnicalReport)
            Me.BindBOPropertyToLabel(claimBo, "ExpectedRepairDate", Me.LabelExpectedRepairDate)
            Me.BindBOPropertyToLabel(claimBo, "ClaimSpecialServiceId", Me.LabelSpecialService)
            Me.BindBOPropertyToLabel(claimBo, "BatchNumber", Me.LabelBatchNumber)
        End If
        Me.ClearGridHeadersAndLabelsErrSign()

        If Me.State.IsTEMP_SVC Then
            Me.LabelServiceCenter.ForeColor = System.Drawing.ColorTranslator.FromHtml("red") 'Color.Red
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
        Dim claim As Claim = CType(Me.State.MyBO, Claim)

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

        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.MyBO.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If Me.State.MyBO.ClaimedEquipment Is Nothing Then
                Throw New GUIException(Codes.EQUIPMENT_NOT_FOUND, Codes.EQUIPMENT_NOT_FOUND)
            Else
                If Me.State.MyBO.ClaimedEquipment.EquipmentBO Is Nothing Then
                    Throw New GUIException(Codes.EQUIPMENT_NOT_FOUND, Codes.EQUIPMENT_NOT_FOUND)
                End If
            End If

            equipmentId = Me.State.MyBO.ClaimedEquipment.EquipmentBO.Id
            equipClassId = Me.State.MyBO.ClaimedEquipment.EquipmentBO.EquipmentClassId
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
        dv = PriceListDetail.GetRepairPricesforMethodofRepair(State.MyBO.MethodOfRepairId, Me.State.MyBO.CompanyId, servCenter.Code, Me.State.MyBO.RiskTypeId,
                                                              DateTime.Now, Me.State.MyBO.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.State.MyBO.Dealer.Id, String.Empty)

        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
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
        Dim dvEstimate As DataView = PriceListDetail.GetPricesForServiceType(Me.State.MyBO.CompanyId, servCenter.Code, Me.State.MyBO.RiskTypeId,
                                                                             DateTime.Now, Me.State.MyBO.Certificate.SalesPrice.Value,
                                                                             LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR),
                                                                             LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, Codes.SERVICE_TYPE__ESTIMATE_PRICE), equipClassId, equipmentId,
                                                                             equipConditionid, Me.State.MyBO.Dealer.Id, String.Empty)
        price = 0
        If Not dvEstimate Is Nothing AndAlso dvEstimate.Count > 0 Then
            price = CDec(dvEstimate(0)(COL_PRICE_DV))
            nEstimatePrice = price ' CDec(dvEstimate.Table.Rows(0)(COL_PRICE_DV))
        End If

        With claim
            If claim.ClaimSpecialServiceId = yesId Then
                splSvcPriceGrp = claim.SpecialServiceServiceType
                If splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__CARRY_IN_PRICE Then 'carry in price  
                    Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, nCarryInPrice)
                ElseIf splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__CLEANING_PRICE Then 'cleaning price
                    Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, nCleaningPrice)
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_ESTIMATE Then 'estimate price
                    Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, nEstimatePrice)
                ElseIf splSvcPriceGrp = Codes.SPL_SVC_SERVICE_TYPE__HOME_PRICE Then 'home price
                    Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, nHomePrice)
                Else 'Manual
                    Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, .AuthorizedAmount)
                End If

                Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, .AuthorizedAmount)
                .Deductible = nZeroValue

                Me.PopulateControlFromBOProperty(Me.TextboxDeductible, .Deductible)
                Me.PopulateControlFromBOProperty(Me.TextboxSlavageAmount, .SalvageAmount)
                Me.PopulateControlFromBOProperty(Me.TextboxAssurantPays, .AssurantPays)
                Me.PopulateControlFromBOProperty(Me.TextboxConsumerPays, .ConsumerPays)
                Me.PopulateControlFromBOProperty(Me.TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
                Me.PopulateControlFromBOProperty(Me.TextboxBonusAmount, .BonusAmount)
                Me.SetSelectedItem(Me.cboWhoPays, AssurantPays)

            Else

                .AuthorizedAmount = Me.State.PrevAuthorizedAmt
                .Deductible = Me.State.PrevDeductible
                Me.PopulateControlFromBOProperty(Me.TextboxDeductible, .Deductible)
                Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, .AuthorizedAmount)
                Me.PopulateControlFromBOProperty(Me.TextboxSlavageAmount, .SalvageAmount)
                Me.PopulateControlFromBOProperty(Me.TextboxAssurantPays, .AssurantPays)
                Me.PopulateControlFromBOProperty(Me.TextboxConsumerPays, .ConsumerPays)
                Me.PopulateControlFromBOProperty(Me.TextboxDueToSCFromAssurant, .DueToSCFromAssurant)
                Me.PopulateControlFromBOProperty(Me.TextboxBonusAmount, .BonusAmount)
                Me.SetSelectedItem(Me.cboWhoPays, .WhoPaysId)

            End If

            If ((CType(TextboxAuthorizedAmount.Text, Decimal) > Me.State.MyBO.AuthorizationLimit.Value) AndAlso
                (CType(Me.TextboxAuthorizedAmount.Text, Decimal) > Me.State.MyBO.AuthorizationLimit.Value)) Then
                Me.moMessageController.Clear()
                Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

            End If

        End With
    End Sub

    Protected Sub PopulateDropdowns()

        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        Dim lstResonsClosed As ListItem() = CommonConfigManager.Current.ListManager.GetList("RESCL", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboReasonClosed.Populate(lstResonsClosed, New PopulateOptions() With
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
        Me.cboDeniedReason.Populate(lstDeniedReason, New PopulateOptions() With
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
        listcontextForCauseOfLoss.CoverageTypeId = Me.State.MyBO.CoverageTypeId
        listcontextForCauseOfLoss.DealerId = Me.State.MyBO.Certificate.DealerId
        listcontextForCauseOfLoss.ProductCode = Me.State.MyBO.Certificate.ProductCode
        listcontextForCauseOfLoss.LanguageId = Authentication.LangId


        Dim listCauseOfLoss As ListItem() = CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss)
        Me.cboCauseOfLossId.Populate(listCauseOfLoss, New PopulateOptions() With
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
        Me.cboFulfilmentMethod.Populate(listFulfillmentMethod, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True,
                                           .TextFunc = AddressOf .GetDescription,
                                           .ValueFunc = AddressOf .GetExtendedCode
                                           })

        If (Not Me.State.IsMultiAuthClaim) Then
            Dim lstWhoPays As ListItem() = CommonConfigManager.Current.ListManager.GetList("WPAYS", Thread.CurrentPrincipal.GetLanguageCode())
            Me.cboWhoPays.Populate(lstWhoPays, New PopulateOptions() With
                                      {
                                      .AddBlankItem = False
                                      })


        End If
    End Sub
    Sub PopulateRefurbReplaceClaimEquipment()

        Try
            'refurbished replacement equipment
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceMake, False)
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceModel, False)
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceSerial, False)
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceSku, False)
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceIMEI, False)
            Me.ChangeEnabledProperty(Me.txtRefurbReplaceComments, False)

            'If (Me.State.ClaimEquipmentBO Is Nothing) Then
            Me.State.ClaimEquipmentBO = ClaimEquipment.GetLatestClaimEquipmentInfo(Me.State.MyBO.Id, LookupListNew.GetIdFromCode("CLAIM_EQUIP_TYPE", "RR"))
            If Not Me.State.ClaimEquipmentBO Is Nothing Then

                Me.State.RefurbReplaceClaimEquipmentId = New Guid(CType(Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_CLAIM_EQUIPMENT_ID), Byte()))

                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceMake, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_MAKE).ToString())
                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceModel, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_MODEL).ToString())
                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceSerial, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_SERIAL_NUMBER).ToString())
                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceSku, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_SKU).ToString())
                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceIMEI, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_IMEI_NUMBER).ToString())
                Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceComments, Me.State.ClaimEquipmentBO(0)(ClaimEquipment.ClaimEquipmentDV.COL_COMMENTS).ToString())
            End If

        Catch ex As Exception
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceMake, "")
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceModel, "")
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceSerial, "")
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceSku, "")
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceIMEI, "")
            Me.PopulateControlFromBOProperty(Me.txtRefurbReplaceComments, "")
        End Try
    End Sub
    Sub PopulateClaimEquipment()
        With Me.State.MyBO
            'enrolled equipment
            Me.ChangeEnabledProperty(Me.txtEnrolledMake, False)
            Me.ChangeEnabledProperty(Me.txtEnrolledModel, False)
            Me.ChangeEnabledProperty(Me.txtenrolledSerial, False)
            Me.ChangeEnabledProperty(Me.txtEnrolledSku, False)
            Me.ChangeEnabledProperty(Me.txtEnrolledIMEI, False)
            Me.ChangeEnabledProperty(Me.txtEnrolledComments, False)
            If Not .EnrolledEquipment Is Nothing Then
                Me.PopulateControlFromBOProperty(Me.txtEnrolledMake, .EnrolledEquipment.Manufacturer)
                Me.PopulateControlFromBOProperty(Me.txtEnrolledModel, .EnrolledEquipment.Model)
                Me.PopulateControlFromBOProperty(Me.txtenrolledSerial, .EnrolledEquipment.SerialNumber)
                Me.PopulateControlFromBOProperty(Me.txtEnrolledSku, .EnrolledEquipment.SKU)
                Me.PopulateControlFromBOProperty(Me.txtEnrolledIMEI, .EnrolledEquipment.IMEINumber)
                Me.PopulateControlFromBOProperty(Me.txtEnrolledComments, .EnrolledEquipment.Comments)
            End If
            'claimed equipment
            Me.ChangeEnabledProperty(Me.txtClaimedMake, False)
            Me.ChangeEnabledProperty(Me.txtClaimedModel, False)
            Me.ChangeEnabledProperty(Me.txtClaimedSerial, False)
            Me.ChangeEnabledProperty(Me.txtClaimedSku, False)
            Me.ChangeEnabledProperty(Me.txtClaimedIMEI, False)
            Me.ChangeEnabledProperty(Me.txtClaimedComments, False)
            If Not .ClaimedEquipment Is Nothing Then
                Me.PopulateControlFromBOProperty(Me.txtClaimedMake, .ClaimedEquipment.Manufacturer)
                Me.PopulateControlFromBOProperty(Me.txtClaimedModel, .ClaimedEquipment.Model)
                Me.PopulateControlFromBOProperty(Me.txtClaimedSerial, .ClaimedEquipment.SerialNumber)
                Me.PopulateControlFromBOProperty(Me.txtClaimedSku, .ClaimedEquipment.SKU)
                Me.PopulateControlFromBOProperty(Me.txtClaimedIMEI, .ClaimedEquipment.IMEINumber)
                Me.PopulateControlFromBOProperty(Me.txtClaimedComments, .ClaimedEquipment.Comments)
            End If

        End With
    End Sub

    Sub PopulateClaimShipping()
        Try
            Me.ChangeEnabledProperty(Me.txtShipperToSC, False)
            Me.ChangeEnabledProperty(Me.txtShipperToCust, False)
            Me.ChangeEnabledProperty(Me.txtShipToSC, False)
            Me.ChangeEnabledProperty(Me.txtShipToCust, False)

            Me.State.ClaimShippingBO = ClaimShipping.GetLatestClaimShippingInfo(Me.State.MyBO.Id, LookupListNew.GetIdFromCode("SHIPPING_TYPES", "SHIP_TO_SC"))
            If Not Me.State.ClaimShippingBO Is Nothing AndAlso Me.State.ClaimShippingBO.Table.Rows.Count > 0 Then
                Me.State.InboundClaimShippingId = New Guid(CType(Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID), Byte()))
                Me.PopulateControlFromBOProperty(Me.txtShipperToSC, Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CARRIER_NAME).ToString())
                Me.PopulateControlFromBOProperty(Me.txtShipToSC, Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_TRACKING_NUMBER).ToString())
            End If
            Me.State.ClaimShippingBO = ClaimShipping.GetLatestClaimShippingInfo(Me.State.MyBO.Id, LookupListNew.GetIdFromCode("SHIPPING_TYPES", "SHIP_TO_CUST"))
            If Not Me.State.ClaimShippingBO Is Nothing AndAlso Me.State.ClaimShippingBO.Table.Rows.Count > 0 Then
                Me.State.OutboundClaimShippingId = New Guid(CType(Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID), Byte()))
                Me.PopulateControlFromBOProperty(Me.txtShipperToCust, Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_CARRIER_NAME).ToString())
                Me.PopulateControlFromBOProperty(Me.txtShipToCust, Me.State.ClaimShippingBO(0)(ClaimShipping.ClaimShippingDV.COL_TRACKING_NUMBER).ToString())
            End If
        Catch ex As Exception
            Me.PopulateControlFromBOProperty(Me.txtShipperToSC, "")
            Me.PopulateControlFromBOProperty(Me.txtShipperToCust, "")
            Me.PopulateControlFromBOProperty(Me.txtShipToSC, "")
            Me.PopulateControlFromBOProperty(Me.txtShipToCust, "")
        End Try
    End Sub

    Sub PopulateClaimFulfillmentDetails()
        If Me.State.FulfillmentDetailsResponse IsNot Nothing AndAlso
            Me.State.FulfillmentDetailsResponse.LogisticStages IsNot Nothing AndAlso
            Me.State.FulfillmentDetailsResponse.LogisticStages.Length > 0 Then

            Dim logisticStage As SelectedLogisticStage = Me.State.FulfillmentDetailsResponse.LogisticStages.Where(Function(item) item.Code = Codes.FULFILLMENT_FW_LOGISTIC_STAGE).First()

            If logisticStage IsNot Nothing AndAlso logisticStage.Code = Codes.FULFILLMENT_FW_LOGISTIC_STAGE Then

                Me.PopulateControlFromBOProperty(Me.txtOptionDescription, logisticStage.OptionDescription)

                Me.PopulateControlFromBOProperty(Me.txtExpectedDeliveryDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ExpectedDeliveryDate, DATE_TIME_FORMAT))
                Me.PopulateControlFromBOProperty(Me.txtActualDeliveryDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ActualDeliveryDate, DATE_TIME_FORMAT))
                Me.PopulateControlFromBOProperty(Me.txtShippingDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ShippingDate, DATE_TIME_FORMAT))
                Me.PopulateControlFromBOProperty(Me.txtExpectedShippingDate, GetLongDateFormattedStringWithFormat(logisticStage.Shipping.ExpectedShippingDate, DATE_TIME_FORMAT))
                Me.PopulateControlFromBOProperty(Me.txtTrackingNumber, logisticStage.Shipping.TrackingNumber)

                Me.PopulateControlFromBOProperty(Me.txtAddress1, logisticStage.Address.Address1)
                Me.PopulateControlFromBOProperty(Me.txtAddress2, logisticStage.Address.Address2)
                Me.PopulateControlFromBOProperty(Me.txtAddress3, logisticStage.Address.Address3)
                Me.PopulateControlFromBOProperty(Me.txtCity, logisticStage.Address.City)
                Me.PopulateControlFromBOProperty(Me.txtServiceCenterCode, $"{logisticStage.ServiceCenterCode}")
                Me.PopulateControlFromBOProperty(Me.txtServiceCenter, $"{logisticStage.ServiceCenterDescription}")
                Me.PopulateControlFromBOProperty(Me.txtPostalCode, logisticStage.Address.PostalCode)
                Me.PopulateControlFromBOProperty(Me.txtState, LookupListNew.GetDescriptionFromCode(
                                                 LookupListNew.DataView(LookupListNew.LK_REGIONS),
                                                 logisticStage.Address.State))
                Me.PopulateControlFromBOProperty(Me.txtCountry, LookupListNew.GetDescriptionFromCode(
                                                 LookupListNew.DataView(LookupListNew.LK_COUNTRIES),
                                                 logisticStage.Address.Country))

                Me.PopulateControlFromBOProperty(Me.txtStoreCode, logisticStage.HandlingStore.StoreCode)
                Me.PopulateControlFromBOProperty(Me.txtStoreName, logisticStage.HandlingStore.StoreName)

                Dim storeTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList(Codes.HND_STORE_TYPE, Thread.CurrentPrincipal.GetLanguageCode())
                Dim storeTypeItem = storeTypeList.Where(
                    Function(item) item.ExtendedCode = logisticStage.HandlingStore.StoreTypeXcd).FirstOrDefault()
                Me.PopulateControlFromBOProperty(Me.txtStoreType, storeTypeItem.Translation)


                If logisticStage.Shipping.TrackingNumber IsNot Nothing AndAlso
                    Not String.IsNullOrEmpty(logisticStage.Shipping.TrackingNumber.ToString()) AndAlso
                    Not String.IsNullOrEmpty(storeTypeItem.Translation) Then
                    Dim PasscodeResponse = GetPasscode(logisticStage.Shipping.TrackingNumber.ToString())
                    Me.PopulateControlFromBOProperty(Me.txtPasscode, PasscodeResponse)
                Else
                    Me.PopulateControlFromBOProperty(Me.txtPasscode, "")
                End If
                dvClaimFulfillmentDetails.Visible = True
            End If
        Else
            ClearClaimFulfillmentDetails()
        End If
    End Sub

    Sub ClearClaimFulfillmentDetails()
        Me.PopulateControlFromBOProperty(Me.txtOptionDescription, "")
        Me.PopulateControlFromBOProperty(Me.txtExpectedDeliveryDate, "")
        Me.PopulateControlFromBOProperty(Me.txtActualDeliveryDate, "")
        Me.PopulateControlFromBOProperty(Me.txtShippingDate, "")
        Me.PopulateControlFromBOProperty(Me.txtExpectedShippingDate, "")
        Me.PopulateControlFromBOProperty(Me.txtTrackingNumber, "")
        Me.PopulateControlFromBOProperty(Me.txtAddress1, "")
        Me.PopulateControlFromBOProperty(Me.txtAddress2, "")
        Me.PopulateControlFromBOProperty(Me.txtAddress3, "")
        Me.PopulateControlFromBOProperty(Me.txtCity, "")
        Me.PopulateControlFromBOProperty(Me.txtState, "")
        Me.PopulateControlFromBOProperty(Me.txtCountry, "")
        Me.PopulateControlFromBOProperty(Me.txtPostalCode, "")
        Me.PopulateControlFromBOProperty(Me.txtStoreCode, "")
        Me.PopulateControlFromBOProperty(Me.txtStoreName, "")
        Me.PopulateControlFromBOProperty(Me.txtStoreType, "")
    End Sub


    Protected Sub PopulateFormFromBOs()

        moClaimInfoController = Me.moClaimInfoController
        moClaimInfoController.InitController(Me.State.MyBO)

        SetSelectedItem(Me.cboReasonClosed, Me.State.MyBO.ReasonClosedId)
        SetSelectedItem(Me.cboCauseOfLossId, Me.State.MyBO.CauseOfLossId)
        SetSelectedItem(Me.cboLawsuitId, Me.State.MyBO.IsLawsuitId)
        If Not Me.State.MyBO.DeniedReasonId.Equals(Guid.Empty) Then SetSelectedItem(Me.cboDeniedReason, Me.State.MyBO.DeniedReasonId)

        If Not Me.State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
            Me.UserControlAddress.ClaimDetailsBind(Me.State.MyBO.ContactInfo.Address)
            Me.UserControlContactInfo.Bind(Me.State.MyBO.ContactInfo)
        End If

        Me.PopulateControlFromBOProperty(Me.TextboxCertificateNumber, Me.State.MyBO.CertificateNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxUserName, Me.State.MyBO.UserName)
        Me.PopulateControlFromBOProperty(Me.TextboxDealerName, Me.State.MyBO.DealerName)
        Me.PopulateControlFromBOProperty(Me.TextboxRiskType, Me.State.MyBO.RiskType)
        Me.PopulateControlFromBOProperty(Me.TextboxClaimActivity, Me.State.MyBO.ClaimActivityDescription)
        Me.PopulateControlFromBOProperty(Me.TextboxCoverageType, Me.State.MyBO.CoverageTypeDescription)
        Me.PopulateControlFromBOProperty(Me.TextboxNotificationType, Me.State.MyBO.NotificationTypeDescription)
        Me.PopulateControlFromBOProperty(Me.TextboxMethodOfRepair, Me.State.MyBO.MethodOfRepairDescription)
        Me.PopulateControlFromBOProperty(Me.TextboxClaimNumber, Me.State.MyBO.ClaimNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxStatusCode, Me.State.MyBO.StatusCode)
        Me.PopulateControlFromBOProperty(Me.TextboxContactName, getSalutation(Me.State.MyBO.ContactSalutationID) & Me.State.MyBO.ContactName)
        Me.PopulateControlFromBOProperty(Me.TextboxCallerName, getSalutation(Me.State.MyBO.CallerSalutationID) & Me.State.MyBO.CallerName)
        Me.PopulateControlFromBOProperty(Me.TextboxProblemDescription, Me.State.MyBO.ProblemDescription)
        Me.PopulateControlFromBOProperty(Me.TextboxLiabilityLimit, Me.State.MyBO.LiabilityLimit)
        Me.PopulateControlFromBOProperty(Me.TextboxDeductible, Me.State.MyBO.Deductible)
        Me.PopulateControlFromBOProperty(Me.TextboxTotalPaid, Me.State.MyBO.TotalPaid)
        Me.PopulateControlFromBOProperty(Me.TextboxDedCollMethod,
                                         LookupListNew.GetDescriptionFromId(LookupListNew.GetDedCollMethodLookupList(Authentication.LangId),
                                                                            Me.State.MyBO.DedCollectionMethodID))
        Me.PopulateControlFromBOProperty(Me.TextboxCCAuthCode, Me.State.MyBO.DedCollectionCCAuthCode)
        Me.PopulateControlFromBOProperty(Me.TextboxSlavageAmount, Me.State.MyBO.SalvageAmount)
        Me.PopulateControlFromBOProperty(Me.TextboxClaimsAdjuster, Me.State.MyBO.ClaimsAdjusterName)
        Me.PopulateControlFromBOProperty(Me.TextboxReportedDate, Me.State.MyBO.ReportedDate)
        Me.PopulateControlFromBOProperty(Me.TextboxClaimClosedDate, Me.State.MyBO.ClaimClosedDate)
        Me.PopulateControlFromBOProperty(Me.TextboxFollowupDate, Me.State.MyBO.FollowupDate)
        Me.PopulateControlFromBOProperty(Me.TextboxAddedDate, Me.State.MyBO.CreatedDate)
        Me.PopulateControlFromBOProperty(Me.TextboxLastModifiedBy, Me.State.MyBO.LastOperatorName)
        Me.PopulateControlFromBOProperty(Me.TextboxLastModifiedDate, Me.State.MyBO.LastModifiedDate)
        Me.PopulateControlFromBOProperty(Me.TextboxDiscount, Me.State.MyBO.DiscountAmount)
        Me.PopulateControlFromBOProperty(Me.TextboxPolicyNumber, Me.State.MyBO.PolicyNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxLossDate, Me.State.MyBO.LossDate)
        Me.PopulateControlFromBOProperty(Me.TextboxMobileNumber, Me.State.MyBO.MobileNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxMasterClaimNumber, Me.State.MyBO.MasterClaimNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxCaller_Tax_Number, Me.State.MyBO.CallerTaxNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxFraudulent, LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList("YESNO", Authentication.LangId, True), Me.State.MyBO.Fraudulent))
        Me.PopulateControlFromBOProperty(Me.TextboxComplaint, LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList("YESNO", Authentication.LangId, True), Me.State.MyBO.Complaint))
        Me.PopulateControlFromBOProperty(Me.TextboxTrackingNumber, Me.State.MyBO.TrackingNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxEMPLOYEE_NUMBER, Me.State.MyBO.EmployeeNumber)
        Me.PopulateControlFromBOProperty(Me.TextboxDEVICE_ACTIVATION_DATE, Me.State.MyBO.DeviceActivationDate)
        BindSelectItem(Me.State.MyBO.FulfilmentMethod, Me.cboFulfilmentMethod)
        If Not Me.State.MyBO.BankInfoId.Equals(Guid.Empty) Then
            Me.State.FulfilmentBankinfoBo = New BusinessObjectsNew.BankInfo(Me.State.MyBO.BankInfoId)
            Me.PopulateControlFromBOProperty(Me.TextboxAccountNumber, Me.State.FulfilmentBankinfoBo.Account_Number)
            ControlMgr.SetVisibleControl(Me, Me.LabelAccountNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.TextboxAccountNumber, True)
        End If
        If Not Me.State.MyBO.DeniedReasons Is Nothing Then
            'Me.PopulateControlFromBOProperty(Me.TextboxDeniedReasons, Me.State.MyBO.DeniedReasons)
            Dim deniedReasonsString As String = Me.State.MyBO.DeniedReasons
            Dim dv As DataView = LookupListNew.GetDeniedReasonLookupList(Authentication.LangId)
            Dim translationSplitString As String = Nothing
            If (deniedReasonsString.IndexOf(";") > 0) Then
                For Each extendedcode As String In deniedReasonsString.Split(";")
                    Try
                        translationSplitString = translationSplitString + dv.ToTable().Select("extended_code ='" & extendedcode & "'").First()("description") + Environment.NewLine
                    Catch ex As Exception

                    End Try

                Next
                Me.PopulateControlFromBOProperty(Me.TextboxDeniedReasons, translationSplitString)
            Else
                Try
                    Me.PopulateControlFromBOProperty(Me.TextboxDeniedReasons, dv.ToTable().Select("extended_code ='" & Me.State.MyBO.DeniedReasons & "'").First()("description"))
                Catch ex As Exception
                    Me.PopulateControlFromBOProperty(Me.TextboxDeniedReasons, "")
                End Try
            End If
            'Me.TextboxDeniedReasons.Text = Me.TextboxDeniedReasons.Text.Replace(";",Environment.NewLine)
            ControlMgr.SetVisibleControl(Me, Me.LabelDeniedReason, False)
            ControlMgr.SetVisibleControl(Me, Me.cboDeniedReason, False)
        Else
            ControlMgr.SetVisibleControl(Me, Me.LabelDeniedReasons, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxDeniedReasons, False)
        End If
        If Not Me.State.MyBO.ClaimedEquipment Is Nothing Then
            With Me.State.MyBO.ClaimedEquipment
                Me.PopulateControlFromBOProperty(Me.TextboxCurrentDeviceSKU, .SKU)
                Me.PopulateControlFromBOProperty(Me.TextboxManufacturer, .Manufacturer)
                Me.PopulateControlFromBOProperty(Me.TextboxModel, .Model)
                Me.PopulateControlFromBOProperty(Me.TextboxPrice, .Price)
                Me.PopulateControlFromBOProperty(Me.TextboxSerialNumber, .SerialNumber)
            End With
        End If

        If Me.State.MyBO.RepairShortDesc Is Nothing Then
            Me.TextboxRepairCode.Text = String.Empty
        Else
            Me.PopulateControlFromBOProperty(Me.TextboxRepairCode, Me.State.MyBO.RepairShortDesc & "-" & Me.State.MyBO.RepairCode)
        End If
        If Me.State.MyBO.ClaimStatusesCount > 0 Then
            If Not Me.State.MyBO.LatestClaimStatus.StatusDescription Is Nothing Then
                Me.TextboxClaimStatus.Text = Me.State.MyBO.LatestClaimStatus.StatusDescription
            End If

            If Not Me.State.MyBO.LatestClaimStatus.Owner Is Nothing Then
                Me.TextboxClaimStatus.Text = String.Format("{0} {1}", Me.TextboxClaimStatus.Text, Me.State.MyBO.LatestClaimStatus.Owner)
            End If

        End If
        Me.PopulateControlFromBOProperty(Me.TextboxAssurantPays, Me.State.MyBO.AssurantPays)
        Me.PopulateControlFromBOProperty(Me.TextboxConsumerPays, Me.State.MyBO.ConsumerPays)
        Me.PopulateControlFromBOProperty(Me.TextboxDueToSCFromAssurant, Me.State.MyBO.DueToSCFromAssurant)
        Me.PopulateControlFromBOProperty(Me.TextboxAboveLiability, Me.State.MyBO.AboveLiability)
        Me.PopulateControlFromBOProperty(Me.TextboxBonusAmount, Me.State.MyBO.BonusAmount)

        Me.PopulateControlFromBOProperty(Me.txtCurrentRetailPrice, Me.State.MyBO.CurrentRetailPrice)
        Dim certObj As Certificate = New Certificate(Me.State.MyBO.Certificate.Id)
        Me.PopulateControlFromBOProperty(Me.txtPaymentPassedDue, certObj.GetCertPaymentPassedDueExtInfo(Me.State.MyBO.Certificate.Id))
        'If CType(Me.State.MyBO.CurrentRetailPrice, Decimal) > 0 Then
        '    Me.PopulateControlFromBOProperty(Me.txtCurrentRetailPrice, Me.State.MyBO.CurrentRetailPrice)
        'Else
        '    Me.PopulateControlFromBOProperty(Me.txtCurrentRetailPrice, GetDeviceCurrentRetailValue(Me.State.MyBO.Id))
        'End If

        If (Not Me.State.IsMultiAuthClaim) Then
            Dim claimBo As Claim = CType(Me.State.MyBO, Claim)

            If Not claimBo.WhoPaysId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboWhoPays, claimBo.WhoPaysId)
            End If
            If Me.State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                Me.PopulateControlFromBOProperty(Me.TextboxCurrentOdometer, claimBo.CurrentOdometer)
            End If

            Me.PopulateControlFromBOProperty(Me.TextboxServiceCenter, claimBo.ServiceCenter)
            Me.PopulateControlFromBOProperty(Me.TextboxLoanerCenter, claimBo.LoanerCenter)
            Me.PopulateControlFromBOProperty(Me.TextboxRepairDate, claimBo.RepairDate)
            'Me.PopulateControlFromBOProperty(Me.TextboxDEVICE_ACTIVATION_DATE, claimBo.DeviceActivationDate)
            'Me.PopulateControlFromBOProperty(Me.TextboxEMPLOYEE_NUMBER, claimBo.EmployeeNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxInvoiceProcessDate, claimBo.InvoiceProcessDate)
            Me.PopulateControlFromBOProperty(Me.TextboxInvoiceDate, claimBo.InvoiceDate)
            Me.PopulateControlFromBOProperty(Me.TextboxLoanerReturnedDate, claimBo.LoanerReturnedDate)
            Me.PopulateControlFromBOProperty(Me.TextboxAuthorizationNumber, claimBo.AuthorizationNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxSource, claimBo.Source)
            Me.PopulateControlFromBOProperty(Me.TextboxTechnicalReport, claimBo.TechnicalReport)
            Me.PopulateControlFromBOProperty(Me.TextboxDefectReason, LookupListNew.GetDescriptionFromCode("CAUSES_OF_LOSS", claimBo.DefectReason))
            Me.PopulateControlFromBOProperty(Me.TextboxExpectedRepairDate, claimBo.ExpectedRepairDate)
            Me.PopulateControlFromBOProperty(Me.TextboxBatchNumber, claimBo.BatchNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxSpecialService, claimBo.SpecialService)
            Me.PopulateControlFromBOProperty(Me.TextboxStoreNumber, claimBo.StoreNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxSpecialInstruction, claimBo.SpecialInstruction)
            If claimBo.CanDisplayVisitAndPickUpDates Then
                Me.PopulateControlFromBOProperty(Me.TextboxVisitDate, claimBo.VisitDate)
                Me.PopulateControlFromBOProperty(Me.TextboxPickupDate, claimBo.PickUpDate)
            End If

            If Me.State.MyBO.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT Then
                Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                Me.LabelExpectedRepairDate.Text = TranslationBase.TranslateLabelOrMessage("EXPECTED_REPLACEMENT_DATE") + ":"
            End If
            Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, claimBo.AuthorizedAmount)

            If Not String.IsNullOrEmpty(claimBo.LoanerRquestedXcd) Then
                TextboxLoanerRequested.Text = LookupListNew.GetDescriptionFromExtCode(LookupListNew.LK_YESNO_EXT, ElitaPlusIdentity.Current.ActiveUser.LanguageId, claimBo.LoanerRquestedXcd)
            End If

            'Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, State.AuthorizedAmount)
            Dim objCountry As New Country(Me.State.MyBO.Company.CountryId)
            If Not claimBo.ServiceCenterObject Is Nothing AndAlso claimBo.ServiceCenterObject.Id.Equals(objCountry.DefaultSCId) Then
                Me.State.IsTEMP_SVC = True
            Else
                Me.State.IsTEMP_SVC = False
            End If
        Else
            Dim claim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
            Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, claim.AuthorizedAmount)
            'Me.PopulateControlFromBOProperty(Me.TextboxAuthorizedAmount, State.AuthorizedAmount)
            Me.State.claimAuthList = CType(Me.State.MyBO, MultiAuthClaim).ClaimAuthorizationChildren.OrderBy(Function(i) i.AuthorizationNumber).ToList
            ucClaimConsequentialDamage.PopulateConsequentialDamage(Me.State.MyBO)
        End If

        PopulateRefurbReplaceClaimEquipment()
        PopulateClaimShipping()

    End Sub

    Protected Sub PopulateClaimDetailContactInfoBOsFromForm()

        Me.State.MyBO.ContactInfo.Address.InforceFieldValidation = True
        UserControlContactInfo.PopulateBOFromControl(True)
        Me.State.MyBO.ContactInfo.Save()

    End Sub

    Private Sub PopulateBOForMultiAuthClaim()
        Dim claim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
        With claim
            Me.PopulateBOProperty(claim, "ReasonClosedId", Me.cboReasonClosed)
            Me.PopulateBOProperty(claim, "IsLawsuitId", Me.cboLawsuitId)
            Me.PopulateBOProperty(claim, "TrackingNumber", Me.TextboxTrackingNumber)
            Me.PopulateBOProperty(claim, "DeniedReasonId", Me.cboDeniedReason)
            Me.PopulateBOProperty(claim, "ProblemDescription", Me.TextboxProblemDescription)
            Me.PopulateBOProperty(claim, "LiabilityLimit", Me.TextboxLiabilityLimit)
            Me.PopulateBOProperty(claim, "Deductible", Me.TextboxDeductible)
            Me.PopulateBOProperty(claim, "DiscountAmount", Me.TextboxDiscount)
            Me.PopulateBOProperty(claim, "PolicyNumber", Me.TextboxPolicyNumber)
            Me.PopulateBOProperty(claim, "CauseOfLossId", Me.cboCauseOfLossId)
            Me.PopulateBOProperty(claim, "FollowupDate", Me.TextboxFollowupDate)

            Me.PopulateBOProperty(claim, "DeviceActivationDate", Me.TextboxDEVICE_ACTIVATION_DATE)
            Me.PopulateBOProperty(claim, "EmployeeNumber", Me.TextboxEMPLOYEE_NUMBER)

            Me.HandleCloseClaimLogic()
            'if user tries to close denied claim, remove denied reason
            If (Me.TextboxStatusCode.Text.Trim = Codes.CLAIM_STATUS__DENIED) Then
                If Not .ReasonClosedId.Equals(Guid.Empty) Then
                    Me.PopulateBOProperty(claim, "DeniedReasonId", Guid.Empty)
                End If
            End If

            Dim YesIdd As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesIdd.ToString And Not claim.ContactInfo Is Nothing Then
                claim.ContactInfoId = claim.ContactInfo.Id
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            PopulateClaimDetailContactInfoBOsFromForm()
        End If
        Me.PopulateBOProperty(claim, "FulfilmentMethod", Me.cboFulfilmentMethod, False, True)

    End Sub

    Private Sub PopulateBOForSingleAuthClaim()
        Dim claim As Claim = CType(Me.State.MyBO, Claim)

        With claim
            Me.PopulateBOProperty(claim, "ReasonClosedId", Me.cboReasonClosed)
            Me.PopulateBOProperty(claim, "IsLawsuitId", Me.cboLawsuitId)
            Me.PopulateBOProperty(claim, "DeniedReasonId", Me.cboDeniedReason)
            Me.PopulateBOProperty(claim, "ProblemDescription", Me.TextboxProblemDescription)
            Me.PopulateBOProperty(claim, "SpecialInstruction", Me.TextboxSpecialInstruction)
            Me.PopulateBOProperty(claim, "AuthorizedAmount", Me.TextboxAuthorizedAmount)
            Me.PopulateBOProperty(claim, "LiabilityLimit", Me.TextboxLiabilityLimit)
            Me.PopulateBOProperty(claim, "Deductible", Me.TextboxDeductible)
            Me.PopulateBOProperty(claim, "DiscountAmount", Me.TextboxDiscount)
            Me.PopulateBOProperty(claim, "PolicyNumber", Me.TextboxPolicyNumber)
            Me.PopulateBOProperty(claim, "CauseOfLossId", Me.cboCauseOfLossId)
            Me.PopulateBOProperty(claim, "FollowupDate", Me.TextboxFollowupDate)
            Me.PopulateBOProperty(claim, "TrackingNumber", Me.TextboxTrackingNumber)

            If Me.State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                Me.PopulateBOProperty(claim, "CurrentOdometer", Me.TextboxCurrentOdometer)
            End If


            Me.PopulateBOProperty(claim, "WhoPaysId", Me.cboWhoPays)
            Me.PopulateBOProperty(claim, "RepairDate", Me.TextboxRepairDate)
            Me.PopulateBOProperty(claim, "DeviceActivationDate", Me.TextboxDEVICE_ACTIVATION_DATE)
            Me.PopulateBOProperty(claim, "EmployeeNumber", Me.TextboxEMPLOYEE_NUMBER)
            Me.PopulateBOProperty(claim, "LoanerReturnedDate", Me.TextboxLoanerReturnedDate)
            Me.PopulateBOProperty(claim, "BatchNumber", Me.TextboxBatchNumber)

            Me.PopulateBOProperty(claim, "FulfilmentMethod", Me.cboFulfilmentMethod, False, True)
            If IsNothing(Me.State.FulfilmentBankinfoBo) Then
                Me.State.FulfilmentBankinfoBo = New BusinessObjectsNew.BankInfo
            End If
            If GetSelectedValue(cboFulfilmentMethod) = Codes.FULFILMENT_METHOD_REIMBURSEMENT And Not Me.TextboxAccountNumber.Text Is Nothing Then
                PopulateBOProperty(Me.State.FulfilmentBankinfoBo, "CountryID", Me.State.MyBO.Company.CountryId)
                Me.PopulateBOProperty(Me.State.FulfilmentBankinfoBo, "Account_Number", Me.TextboxAccountNumber)
                Me.State.FulfilmentBankinfoBo.ValidateBankFields = False
                Me.State.FulfilmentBankinfoBo.Save()
                PopulateBOProperty(claim, "BankInfoId", Me.State.FulfilmentBankinfoBo.Id)
            End If

            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            If Not claim Is Nothing AndAlso claim.CanDisplayVisitAndPickUpDates Then
                Me.PopulateBOProperty(claim, "VisitDate", Me.TextboxVisitDate)
                If Not claim.LoanerTaken Then
                    Me.PopulateBOProperty(claim, "PickUpDate", Me.TextboxPickupDate)
                Else
                    .SetPickUpDateFromLoanerReturnedDate()
                End If
            End If

            Me.HandleCloseClaimLogic()

            'if user tries to close denied claim, remove denied reason
            If (Me.TextboxStatusCode.Text.Trim = Codes.CLAIM_STATUS__DENIED) Then
                If Not .ReasonClosedId.Equals(Guid.Empty) Then
                    Me.PopulateBOProperty(claim, "DeniedReasonId", Guid.Empty)
                End If
                Me.PopulateBOProperty(claim, "AuthorizationNumber", Me.TextboxAuthorizationNumber)
            End If

            Dim YesIdd As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesIdd.ToString And Not claim.ContactInfo Is Nothing Then
                claim.ContactInfoId = claim.ContactInfo.Id
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            PopulateClaimDetailContactInfoBOsFromForm()
        End If

        Me.PopulateBOProperty(claim, "CurrentRetailPrice", Me.txtCurrentRetailPrice)

    End Sub

    Protected Sub PopulateBOsFromForm()
        If (Me.State.IsMultiAuthClaim) Then
            PopulateBOForMultiAuthClaim()
        Else
            PopulateBOForSingleAuthClaim()
        End If
    End Sub

    Protected Sub CreateNewServiceWarrantyClaimFromExistingClaim()

        Me.State.AssociatedClaimBO = ClaimFacade.Instance.CreateClaim(Of Claim)()
        Me.State.AssociatedClaimBO.Clone(Me.State.MyBO)

    End Sub

    Private Sub SaveClaim()

        Me.State.MyBO.IsUpdatedComment = False
        Dim commentBO As Comment = Me.State.MyBO.AddNewComment(False)
        commentBO.CallerName = Me.State.MyBO.CallerName
        commentBO.CommentTypeId = Guid.Empty
        commentBO.Comments = Nothing

        'Check for Pending rules while reopening a claim
        If Not (Me.State.MyBO.Status = BasicClaimStatus.Closed Or Me.State.MyBO.Status = BasicClaimStatus.Denied) Then
            Me.State.MyBO.CheckForPendingRules(commentBO)
        End If


        If (Me.State.IsMultiAuthClaim) Then
            CType(Me.State.MyBO, MultiAuthClaim).Save()
        Else
            CType(Me.State.MyBO, Claim).Save()
        End If

        NotifyChanges(Me.NavController)
        Me.PopulateFormFromBOs()
        Me.State.IsEditMode = False
        Me.EnableDisablePageControls()





        Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Me.State.MyBO
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT) = commentBO
        Me.NavController.Navigate(Me, FlowEvents.EVENT_SAVE)

    End Sub

    Private Sub ServiceWarranty()

        If Me.State.IsMultiAuthClaim Then
            CType(Me.State.MyBO, MultiAuthClaim).CreateServiceWarranty()
            Me.PopulateFormFromBOs()
            Me.EnableDisablePageControls()
        Else
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, FlowEvents.EVENT_SERVICE_WARRANTY)
        End If


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

    Private Sub CleanHiddenLimitExceededInput()
        Me.HiddenLimitExceeded.Value = ""
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Dim actionInProgress As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
        CleanPopupInput()


        'if the user has clicked on the back button, then Yes means that leave the page, so stay on the page should be no, since logic below is relative to the page
        If Not confResponse Is Nothing And (actionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr Or actionInProgress = ElitaPlusPage.DetailPageCommand.Back) Then
            If confResponse = Me.MSG_VALUE_YES Then
                confResponse = Me.MSG_VALUE_NO
            Else
                confResponse = Me.MSG_VALUE_YES
            End If
        End If

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                'If (Me.State.IsMultiAuthClaim) Then
                '    CType(Me.State.MyBO, MultiAuthClaim).Save()
                'Else
                '    CType(Me.State.MyBO, Claim).Save()
                'End If

                If ((Me.State.MyBO.IsAuthorizedAmountChanged) AndAlso (Me.State.MyBO.IsAuthorizationLimitExceeded)) Then
                    Me.moMessageController.Clear()
                    Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                    TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                    TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenLimitExceeded)
                    Exit Sub
                End If

                If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) AndAlso
                   (Me.State.MyBO.IsAuthorizedAmountChanged AndAlso Not (Me.State.MyBO.IsAuthorizationLimitExceeded) Or (Me.State.MyBO.IsProblemDescriptionChanged) _
                    Or (Me.State.MyBO.IsSpecialInstructionChanged) Or (Me.State.MyBO.IsDeductibleAmountChanged)) Then
                    'Assumption no service order for MultiAuthClaim
                    If (Not Me.State.IsMultiAuthClaim) Then
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
                        Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing

                        'Do not create service order for GVS integrated service center
                        Dim objServiceCenter As New ServiceCenter(CType(Me.State.MyBO, Claim).ServiceCenterId)
                        If Not objServiceCenter.IntegratedWithGVS Then
                            Dim soController As New ServiceOrderController
                            Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(CType(Me.State.MyBO, Claim))
                            'Save this ServiceOrder in the FlowSession
                            Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                        End If
                    End If

                    'SaveClaim()
                    Exit Sub
                Else
                    Me.State.MyBO.IsUpdatedComment = False
                    'Me.State.MyBO.Save()
                    NotifyChanges(Me.NavController)
                    Select Case actionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.Back(ElitaPlusPage.DetailPageCommand.BackOnErr)
                        Case ElitaPlusPage.DetailPageCommand.Save
                            SaveClaim()
                        Case ElitaPlusPage.DetailPageCommand.Redirect_
                            'pm 06-07-06 '
                            Me.ServiceWarranty()
                    End Select
                End If
            End If
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.Back(ElitaPlusPage.DetailPageCommand.Back)
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)

            End Select
        End If
    End Sub

    Protected Sub CheckClaimPaymentInProgress()
        Try
            Dim blockInvoice As String
            Dim oCompany As New Company(Me.State.MyBO.Company.Id)

            'Check the flag at Company level
            If (oCompany.AttributeValues.Contains(COMP_ATTR_BLOCK_PAY_INVOICE)) Then
                blockInvoice = oCompany.AttributeValues.Value(COMP_ATTR_BLOCK_PAY_INVOICE)
            End If

            If (blockInvoice = YES) Then
                If Claim.CheckClaimPaymentInProgress(Me.State.MyBO.Id, Me.State.MyBO.Company.CompanyGroupId) Then
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, False)
                    ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_CLAIM_PROCESS_IN_PROGRESS_ERR, Assurant.ElitaPlus.Common.ErrorCodes.CLAIM_PROCESS_IN_PROGRESS_ERR)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CheckIfComingFromAuthDetailForm()
        If Me.State.HasAuthDetailDataChanged Then
            Me.State.MyBO.AuthDetailDataHasChanged = Me.State.HasAuthDetailDataChanged
            Me.State.HasAuthDetailDataChanged = False
            Me.btnEdit_WRITE_Click(Nothing, Nothing)
        ElseIf Me.State.HasClaimStatusBOChanged Then
            Me.btnEdit_WRITE_Click(Nothing, Nothing)
        End If

    End Sub

    Protected Sub CheckIfComingFromCreateClaimConfirm()
        Dim confResponse As String = Me.HiddenLimitExceeded.Value
        CleanHiddenLimitExceededInput()
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
            SaveClaim()
            System.Threading.Thread.CurrentThread.Abort()
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            'Nothing To Do. Stay on the page
        End If
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Dim retObj As ReturnType = New ReturnType(cmd, Me.State.MyBO, CheckForChanges(Me.NavController), Me.State.IsCallerAuthenticated)
        Me.NavController = Nothing
        Me.ReturnToCallingPage(retObj)
    End Sub

    Private Function getSalutation(ByVal salutaionid As Guid) As String
        Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company =
                New Assurant.ElitaPlus.BusinessObjectsNew.Company(Me.State.MyBO.CompanyId)

        If LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), companyBO.SalutationId) = "Y" Then
            Dim oSalutation As String = Me.State.MyBO.getSalutationDescription(salutaionid) & " "
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

        Me.AddCalendar_New(Me.ImageButtonFollowupDate, Me.TextboxFollowupDate)

        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY")
        Me.MasterPage.PageTitle = String.Format("{0} (<strong> #{1}</strong> )", TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY"), Me.State.MyBO.ClaimNumber)
        UpdateBreadCrum()

        If Not Me.State.IsMultiAuthClaim Then Me.AddCalendar_New(Me.ImageButtonRepairDate, Me.TextboxRepairDate)
        If Not Me.State.IsMultiAuthClaim Then Me.AddCalendar_New(Me.ImageButtonLoanerReturnedDate, Me.TextboxLoanerReturnedDate)

        Me.AddCalendar_New(Me.ImageButtonDeviceActivationDate, Me.TextboxDEVICE_ACTIVATION_DATE)

        If (Not Me.State.IsMultiAuthClaim) Then
            If Not Me.State.MyBO Is Nothing AndAlso CType(Me.State.MyBO, Claim).CanDisplayVisitAndPickUpDates Then
                Me.AddCalendar_New(Me.ImageButtonVisitDate, Me.TextboxVisitDate)
                Me.AddCalendar_New(Me.ImageButtonPickupDate, Me.TextboxPickupDate)
            End If
        End If

        If (Me.State.MyBO.AuthorizedAmount.Value > Me.State.MyBO.AuthorizationLimit.Value) Then
            Me.moMessageController.Clear()
            Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                                            TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                            TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))
        End If

        If (State.IsMultiAuthClaim) Then ucClaimConsequentialDamage.Translate()

    End Sub

    Private Sub InitializeData()

        'Trace(Me, "Claim Id =" & GuidControl.GuidToHexString(Me.State.MyBO.Id) &
        '          "@Claim = " & Me.State.MyBO.ClaimNumber)

        If Me.State.MyBO Is Nothing Then
            Me.State.MyBO = ClaimFacade.Instance.CreateClaim(Of ClaimBase)()
        End If

        Me.State.YesId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        Me.State.NoId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)

        'Claim Auth Detail logic
        If Me.State.MyBO.Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
            Me.State.AuthDetailEnabled = True
        End If

    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Dim myBo As ClaimBase = Me.State.MyBO
                'for single auth claims coming from claim search form and (certificate search and then to claim form)
                If (Me.NavController.CurrentNavState.Name = "CLAIM_ISSUE_APPROVED_FROM_CLAIM" OrElse Me.NavController.CurrentNavState.Name = "CLAIM_ISSUE_APPROVED_FROM_CERT") Then
                    NavController.Navigate(Me, "back", New ClaimForm.Parameters(State.MyBO.Id, State.IsCallerAuthenticated))
                ElseIf (Me.NavOriginURL = ClaimWizardForm.AbsoluteURL) Then
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, Me.State.IsCallerAuthenticated)
                    Me.NavController = Nothing
                    Me.ReturnToMaxCallingPage(retObj)
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo,, Me.State.IsCallerAuthenticated)
                    Me.NavController = Nothing
                    Me.ReturnToCallingPage(retObj)
                End If
                'DirectCast(Me.NavController, Assurant.Common.AppNavigationControl.NavControllerBase).CurrentFlow.StartState.Url
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If cbousershipaddress.SelectedValue = NoId.ToString Then
                If Not Me.State.MyBO.ContactInfoId.Equals(Guid.Empty) Then
                    Me.State.MyBO.ContactInfoId = Nothing
                End If
                If Not Me.State.MyBO.ContactInfo Is Nothing Then
                    If Not Me.State.MyBO.ContactInfo.Address Is Nothing Then Me.State.MyBO.ContactInfo.Address.Delete()
                    Me.State.MyBO.ContactInfo.Delete()
                End If
            End If

            If TextboxAuthorizedAmount.Text = String.Empty Or Not IsNumeric(TextboxAuthorizedAmount.Text) Then
                'display error
                ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
            End If

            If State.IsSVCCUserEdit Then
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Select Case Me.State.MyBO.ClaimAuthorizationType
                        Case ClaimAuthorizationType.Multiple
                            CType(Me.State.MyBO, MultiAuthClaim).Save()
                        Case ClaimAuthorizationType.Single
                            CType(Me.State.MyBO, Claim).Save()
                    End Select

                End If
                State.IsSVCCUserEdit = False
                Me.PopulateFormFromBOs()
                Me.State.IsEditMode = False
                Me.EnableDisablePageControls()
                Exit Sub
            End If

            If Me.State.IsEditMode = True Then
                If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                    If CType(TextboxLiabilityLimit.Text, Decimal) <> 0 Then
                        ElitaPlusPage.SetLabelError(Me.LabelLiabilityLimit)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If
                End If
                If Not IsNumeric(TextboxLiabilityLimit.Text) Then
                    ElitaPlusPage.SetLabelError(Me.LabelLiabilityLimit)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                End If

                If CType(TextboxLiabilityLimit.Text, Decimal) < 0 Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelLiabilityLimit)
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                End If

                'if entered amount > existing liability limit then
                If CType(TextboxLiabilityLimit.Text, Decimal) > Me.State.MyBO.LiabilityLimit.Value Then
                    ' check if entered amount>oldest liability limit
                    Dim dv As DataView, oClm As ClaimBase
                    dv = oClm.GetOriginalLiabilityLimit(Me.State.MyBO.Id)
                    Dim dLiabilityLimit As Decimal
                    If dv.Count > 0 AndAlso Not IsDBNull(dv(0)(0)) Then
                        dLiabilityLimit = CType(dv(0)(0), Decimal)
                        If CType(TextboxLiabilityLimit.Text, Decimal) > dLiabilityLimit Then
                            'display error
                            ElitaPlusPage.SetLabelError(Me.LabelLiabilityLimit)
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                        End If
                    Else
                        'if no history record raise error since entered amount is >existing amount
                        ElitaPlusPage.SetLabelError(Me.LabelLiabilityLimit)
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If
                End If
            End If

            Me.PopulateBOsFromForm()
            If State.RefurbReplaceClaimEquipmentId <> Guid.Empty Then
                ClaimEquipment.UpdateClaimEquipmentInfo(State.RefurbReplaceClaimEquipmentId, Me.txtRefurbReplaceComments.Text)
            End If

            If State.InboundClaimShippingId <> Guid.Empty Then
                ClaimShipping.UpdateClaimShippingInfo(State.InboundClaimShippingId, Me.txtShipToSC.Text)
            End If
            If State.OutboundClaimShippingId <> Guid.Empty Then
                ClaimShipping.UpdateClaimShippingInfo(State.OutboundClaimShippingId, Me.txtShipToCust.Text)
            End If

            If Me.State.MyBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                If Me.State.MyBO.PolicyNumber Is Nothing Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelPolicyNumber)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POLICYNUMBER_REQD)
                End If
            End If

            Dim authorizedAmountChanged As Boolean = Me.State.MyBO.IsAuthorizedAmountChanged
            Dim deductibleAmountChanged As Boolean = Me.State.MyBO.IsDeductibleAmountChanged
            Dim problemDescriptionChanged As Boolean = Me.State.MyBO.IsProblemDescriptionChanged
            Dim specialInstructionChanged As Boolean = Me.State.MyBO.IsSpecialInstructionChanged

            If (Me.State.IsMultiAuthClaim) Then CType(Me.State.MyBO, MultiAuthClaim).Validate() Else CType(Me.State.MyBO, Claim).PreValidate()

            'If the AuthorizedAmount has been modified, then Generate and Send a Service Order for this Claim
            'Make sure it is NOT a Pending Claim - since, we do NOT generate and send a Service Order for a Pending Claim
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) = Nothing
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing
            If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) AndAlso
               (authorizedAmountChanged AndAlso Not (Me.State.MyBO.IsAuthorizationLimitExceeded) Or (problemDescriptionChanged) Or (specialInstructionChanged) Or (deductibleAmountChanged)) Then
                'AuthorizedAmount has been changed for an Active Claim
                'Generate and Send a Service Order for this Active Claim 

                'First check the AuthorizedAmount validation
                If Me.State.MyBO.AuthorizedAmount Is Nothing Then
                    ElitaPlusPage.SetLabelError(Me.LabelAuthorizedAmount)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                End If

                If Me.State.MyBO.IsDirty Then
                    If Me.State.MyBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                        If ((authorizedAmountChanged) AndAlso (Not (Me.State.MyBO.IsAuthorizationLimitExceeded))) Then
                            '' Auth amt is being changed, calculate the new deductible amt if by percent
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.MyBO.DeductiblePercentID) = Codes.YESNO_Y Then
                                If Not Me.State.MyBO.DeductiblePercent Is Nothing Then
                                    If Me.State.MyBO.DeductiblePercent.Value > 0 Then
                                        If (Not deductibleAmountChanged) Then
                                            Me.State.MyBO.Calculate_deductible_if_by_percentage()
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'Assumption : No service Order for MultiAuthClaim
                    If (Not Me.State.IsMultiAuthClaim) Then
                        'Do not create service order for GVS integrated service center
                        Dim objServiceCenter As New ServiceCenter(CType(Me.State.MyBO, Claim).ServiceCenterId)
                        If Not objServiceCenter.IntegratedWithGVS Then
                            'Reqs-784
                            CType(Me.State.MyBO, Claim).Save()
                            Dim soController As New ServiceOrderController
                            Dim so As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = soController.GenerateServiceOrder(CType(Me.State.MyBO, Claim))

                            'Save this ServiceOrder in the FlowSession
                            Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = so
                        End If
                    End If



                End If



                If ((authorizedAmountChanged) AndAlso (Not (Me.State.MyBO.IsAuthorizationLimitExceeded)) OrElse
                    (Not (authorizedAmountChanged))) Then

                    If Me.cboReasonClosed.SelectedIndex > 0 Then
                        Me.DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Save
                    Else
                        SaveClaim()
                    End If


                ElseIf ((authorizedAmountChanged) AndAlso (Me.State.MyBO.IsAuthorizationLimitExceeded)) Then
                    Me.moMessageController.Clear()
                    Me.moMessageController.AddWarning(String.Format("{0}: {1}",
                                                                    TranslationBase.TranslateLabelOrMessage("Authorized_Amount"),
                                                                    TranslationBase.TranslateLabelOrMessage("Authorization_Limit_Exceeded"), False))

                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_CREATING_CLAIM_WITH_PENDING_STATUS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenLimitExceeded)
                End If
            ElseIf Me.State.MyBO.IsFamilyDirty And Me.State.HasClaimStatusBOChanged Then
                SaveClaim()
            Else
                SaveClaim()
            End If
            Save_Ok = True
        Catch ex As Exception
            'Flush out the ServiceOrder from the FlowSession
            If Not Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) Is Nothing Then
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Nothing
            End If

            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        'Get out of Edit mode
        Try
            UndoChanges()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub UndoChanges()
        If Not Me.State.MyBO.IsNew Then
            'Reload from the DB
            Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
        End If
        Me.State.IsEditMode = False
        Me.PopulateFormFromBOs()
        Me.EnableDisablePageControls()
        Me.MasterPage.MessageController.Clear()

    End Sub

    Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
        Try
            Me.State.IsEditMode = True
            'Service center role update
            If ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__SERVICE_CENTER) Then
                State.IsSVCCUserEdit = True
            End If
            Me.EnableDisablePageControls()
            Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If cbousershipaddress.SelectedValue = YesId.ToString Then
                moUserControlContactInfo.Visible = True
                UserControlContactInfo.EnableDisablecontrol(False)
                UserControlAddress.EnableDisablecontrol(False)
            Else
                moUserControlContactInfo.Visible = False

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnNewItemInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemInfo.Click
        Try
            Me.callPage(Claims.ReplacementForm.URL, New Claims.ReplacementForm.Parameters(True, Me.State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnPartsInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartsInfo.Click
        Try
            If Not Me.State.MyBO Is Nothing Then
                Me.PopulateBOsFromForm()
                Me.NavController.Navigate(Me, "parts_info", BuildPartsInfoParameters)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Function BuildPartsInfoParameters() As PartsInfoForm.Parameters
        Dim claimBO As ClaimBase = Me.State.MyBO
        Return New PartsInfoForm.Parameters(claimBO)
    End Function

    Private Sub btnServiceCenterInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServiceCenterInfo.Click
        Try
            If (Not (CType(Me.State.MyBO, Claim).ServiceCenterId.Equals(Guid.Empty))) Then
                Me.NavController.Navigate(Me, "service_center_info", New ServiceCenterInfoForm.Parameters(CType(Me.State.MyBO, Claim).ServiceCenterId))
                'Me.callPage(ServiceCenterInfoForm.URL, New ServiceCenterInfoForm.Parameters(CType(Me.State.MyBO, Claim).ServiceCenterId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnReplaceItemInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplaceItem.Click
        Try
            If (Not Me.State.IsMultiAuthClaim) Then
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Nothing
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Me.State.MyBO
                Me.NavController.Navigate(Me, FlowEvents.EVENT_REPLACE_ITEM, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_FOR_KEEPING_SAME_LOCATION))
            Else
                If CType(Me.State.MyBO, MultiAuthClaim).HasMultipleServiceCenters Then
                    ucSelectServiceCenter.Populate(Me.State.MyBO, Guid.Empty)
                    ucSelectServiceCenter.moMessageController.AddError("CLAIM_HAS_MULTIPLE_SERVICE_CENTERS")
                    Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
                    Me.RegisterStartupScript("Startup", x)
                Else
                    lblChangeServiceCenterMessage.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_PROMPT_FOR_KEEPING_SAME_LOCATION)
                    Dim x As String = "<script language='JavaScript'> revealModal('ModalChangeServiceCenter') </script>"
                    Me.RegisterStartupScript("Startup", x)
                End If

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnServiceWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServiceWarranty.Click

        Dim oContract As Contract
        Dim CoverageType As String
        Dim ClaimControl As Boolean = False

        Try
            oContract = Contract.GetContract(Me.State.MyBO.Certificate.DealerId, Me.State.MyBO.Certificate.WarrantySalesDate.Value)

            If Not oContract Is Nothing Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oContract.ClaimControlID) = "Y" Then
                    ClaimControl = True
                End If
            End If


            If ClaimControl Then
                Me.DisplayMessage(Message.MSG_DEALER_USER_CLAIM_INTERFACES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
            Else
                If Me.State.IsMultiAuthClaim Then
                    If CallAddServiceWarrantyWs() Then
                        Me.State.MyBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.MyBO.Id)
                        Me.PopulateFormFromBOs()
                        PopulateGrid()
                        Me.EnableDisablePageControls()
                    End If
                Else
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Me.State.MyBO
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_SERVICE_WARRANTY)
                End If

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

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
    Private Sub DisplayWsErrorMessage(ByVal errCode As String, ByVal errDescription As String)
        MasterPage.MessageController.AddError(errCode & " - " & errDescription, False)
    End Sub
    Private Function CallAddServiceWarrantyWs() As Boolean
        Dim wsRequest As BaseFulfillmentRequest = New BaseFulfillmentRequest()
        Dim wsResponse As BaseFulfillmentResponse
        Dim blnsuccess As Boolean = True

        wsRequest.ClaimNumber = Me.State.MyBO.ClaimNumber
        wsRequest.CompanyCode = Me.State.MyBO.Company.Code
        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As FulfillmentServiceClient)
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

    Private Sub btnNewServiceCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewServiceCenter.Click
        Try
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Nothing
            Dim claim As Claim = CType(Me.State.MyBO, Claim)

            Dim coverageType As String = claim.GetCoverageTypeCodeForServiceCenter
            'Def-23747 - Added new parameter CoountryId to locateServCenterParameters.
            Dim locateServCenterParameters As New LocateServiceCenterForm.Parameters(Me.State.MyBO.Certificate.DealerId,
                                                                                     Me.State.MyBO.Certificate.AddressChild.ZipLocator,
                                                                                     Me.State.MyBO.RiskTypeId,
                                                                                     claim.GetManufacturerIdForServiceCenter,
                                                                                     claim.GetCoverageTypeCodeForServiceCenter, Me.State.MyBO.CertItemCoverageId, Guid.Empty, True, False,
                                                                                     , , , Me.State.MyBO.Company.CountryId)
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, FlowEvents.EVENT_NEW_CENTER, locateServCenterParameters)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnPoliceReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoliceReport.Click
        Try

            If (Not (Me.State.MyBO.Id.Equals(Guid.Empty))) Then
                Me.NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_INFO, New PoliceReportForm.Parameters(Me.State.MyBO.Id))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnCertificate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCertificate.Click
        Try
            If (Not (Me.State.MyBO.CertificateId.Equals(Guid.Empty))) Then
                CType(MyBase.State, BaseState).NavCtrl = Me.NavController
                'Me.callPage(Certificates.CertificateForm.URL, Me.State.MyBO.CertificateId)
                Me.callPage(Certificates.CertificateForm.URL, New Certificates.CertificateForm.Parameters(Me.State.MyBO.CertificateId, Me.State.IsCallerAuthenticated))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If (Not (Me.State.MyBO.GetLatestServiceOrder Is Nothing)) Then
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Me.State.MyBO.GetLatestServiceOrder
                Me.NavController.Navigate(Me, FlowEvents.EVENT_REPRINT)
            Else
                Try
                    Dim _SOC As New ServiceOrderController()
                    _SOC.GenerateServiceOrder(CType(Me.State.MyBO, Claim))
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) = Me.State.MyBO.GetLatestServiceOrder
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_REPRINT)
                Catch ex As Exception
                    'There is NO Service Order associated with this Claim
                    Me.DisplayMessage(Message.MSG_SERVICE_ORDER_RECORD_NOT_FOUND, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Try
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComment.Click
        Try
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENTS, New CommentListForm.Parameters(Me.State.MyBO.CertificateId, Me.State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItem.Click
        Try
            Me.callPage(Claims.ClaimItemForm.URL, New Claims.ClaimItemForm.Parameters(Me.State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnClaimDeniedInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClaimDeniedInformation.Click
        Try
            Select Case Me.State.MyBO.ClaimAuthorizationType
                Case ClaimAuthorizationType.Multiple
                    CType(Me.State.MyBO, MultiAuthClaim).Save()
                Case ClaimAuthorizationType.Single
                    CType(Me.State.MyBO, Claim).Save()
            End Select
            Me.NavController.Navigate(Me, "claim_denied_information", New Claims.ClaimDeniedInformationForm.Parameters(Me.State.MyBO, Me.State.MyBO.Id, Me.State.MyBO.CertificateId))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnReopen_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReopen_WRITE.Click

        Try
            If (Not Me.State.IsMultiAuthClaim) Then
                CType(Me.State.MyBO, Claim).ReopenClaim()
                If (CType(Me.State.MyBO, Claim).IsSupervisorAuthorizationRequired) Then
                    Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                End If
            Else
                CType(Me.State.MyBO, MultiAuthClaim).ReopenClaim()
            End If
            SaveClaim()
            Save_Ok = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnShipping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShipping.Click
        Try
            If (Not (Me.State.MyBO.ShippingInfoId.Equals(Guid.Empty))) Then
                Me.NavController.Navigate(Me, FlowEvents.EVENT_SHIPPING_INFO, New ShippingInfoForm.Parameters(Me.State.MyBO.ShippingInfoId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnAuthDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthDetail.Click
        Try
            If Not Me.State.MyBO Is Nothing Then
                Me.PopulateBOsFromForm()
                'DEF-17426
                'Me.callPage(ClaimAuthDetailForm.URL, New ClaimAuthDetailForm.Parameters(CType(Me.State.MyBO, Claim), Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV))
                Me.NavController.Navigate(Me, "auth_detail", BuildClaimAuthDetailParameters)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'DEF-17426
    Function BuildClaimAuthDetailParameters() As ClaimAuthDetailForm.Parameters
        Return New ClaimAuthDetailForm.Parameters(CType(Me.State.MyBO, Claim), Me.State.ClaimAuthDetailBO, Me.State.PartsInfoDV)
    End Function

    Private Sub btnMasterClaim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMasterClaim.Click
        Try
            If (Not Me.State.MyBO.MasterClaimNumber Is Nothing AndAlso Not (Me.State.MyBO.Id.Equals(Guid.Empty))) Then
                CType(MyBase.State, BaseState).NavCtrl = Me.NavController
                params.Add(Me.State.MyBO.MasterClaimNumber)
                params.Add(Me.State.MyBO.Id)
                params.Add(Me.State.MyBO.CertificateId)
                Me.callPage(ClaimForm.MASTER_CLAIM_DETAIL_URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUseRecoveries_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUseRecoveries.Click
        Try
            If (Not Me.State.MyBO.CertItemCoverageId.Equals(Guid.Empty) AndAlso Not (CType(Me.State.MyBO, Claim).ServiceCenterId.Equals(Guid.Empty))) Then
                Me.NavController.Navigate(Me, "recovery_locate_sc", Me.BuildServiceCenterParametersForRecovery)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    Function BuildServiceCenterParametersForRecovery() As LocateServiceCenterForm.Parameters
        Dim showAcceptButton As Boolean = True
        Dim claim As Claim = CType(Me.State.MyBO, Claim)
        Return New LocateServiceCenterForm.Parameters(Me.State.MyBO.Certificate.DealerId, Me.State.MyBO.Certificate.AddressChild.ZipLocator, Me.State.MyBO.RiskTypeId, claim.GetManufacturerIdForServiceCenter, CType(Me.State.MyBO, Claim).GetCoverageTypeCodeForServiceCenter, Me.State.MyBO.CertItemCoverageId, Me.State.MyBO.Id, showAcceptButton, True, False, True)
    End Function

    Private Sub btnChangeCoverage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeCoverage.Click
        Try
            If (Not (Me.State.MyBO.Id.Equals(Guid.Empty))) Then
                params.Add(CType(Me.State.MyBO.LossDate, String))
                params.Add(Me.State.MyBO.Id)
                params.Add(Me.State.MyBO.CertificateId)
                params.Add(Me.State.MyBO.CertItemCoverageId)
                params.Add(Me.State.MyBO.StatusCode)

                Dim invoiceProcessDate As DateType = If(Me.State.IsMultiAuthClaim, Nothing, CType(Me.State.MyBO, Claim).InvoiceProcessDate)
                If Not invoiceProcessDate Is Nothing Then
                    params.Add(CType(invoiceProcessDate, String))
                Else
                    params.Add(Nothing)
                End If
                Me.callPage(ClaimForm.COVERAGE_TYPE_URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnStatusDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatusDetail.Click
        Try
            If Not Me.State.MyBO Is Nothing Then
                Me.PopulateBOsFromForm()
                Me.callPage(ClaimStatusDetailForm.URL, Me.State.MyBO.Id)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub btnClaimHistoryDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClaimHistoryDetails.Click
        Try
            Dim URL As String = "~/Claims/ClaimHistoryForm.aspx"
            params.Add(Me.State.MyBO.ClaimNumber)
            params.Add(Me.State.MyBO.Id)

            Me.callPage(URL, params)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Protected Sub btnOutboundCommHistory_Click(sender As Object, e As EventArgs) Handles btnOutboundCommHistory.Click
        Try
            Me.callPage(Tables.OcMessageSearchForm.URL, New Tables.OcMessageSearchForm.CallType("claim_number", Me.State.MyBO.ClaimNumber, Me.State.MyBO.Id, Me.State.MyBO.Dealer.Id))
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRepairLogistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepairLogistics.Click
        Dim param As RepairAndLogisticsForm.Parameters = New RepairAndLogisticsForm.Parameters()

        Try
            Dim URL As String = "~/Claims/RepairAndLogisticsForm.aspx"

            param.ClaimId = Me.State.MyBO.Id

            param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Claim
            Me.callPage(URL, param)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnDenyClaim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenyClaim.Click
        Try
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = Me.State.MyBO
            Me.NavController.Navigate(Me, FlowEvents.EVENT_DENY)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub

    Private Sub btnRedo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedo.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_REDO, Me.State.MyBO)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub
    Protected Sub btnClaimIssues_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClaimIssues.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_ISSUES, New ClaimIssueForm.Parameters(Me.State.MyBO))

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnClaimImages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClaimImages.Click
        Try
            Dim URL As String = "~/Claims/ClaimDocumentForm.aspx"
            params.Add(Me.State.MyBO)
            Me.callPage(URL, params)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub
    Protected Sub btnClaimCaseList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClaimCaseList.Click
        Try
            params.Add(State.MyBO)
            callPage(ClaimDetailsForm.Url, params)
        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnAddConseqDamage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddConseqDamage.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_RECORDING_CONSEQUENTIAL_DAMAGE, New ClaimRecordingForm.Parameters(State.MyBO.Certificate.Id, State.MyBO.Id, Nothing, Codes.CASE_PURPOSE__CONSEQUENTIAL_DAMAGE))
        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub SelectServiceCenter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucSelectServiceCenter.SelectServiceCenter
        Try
            Dim selectedServiceCenterId As Guid = Me.ucSelectServiceCenter.SelectedServiceCenterId
            Dim multiAuthClaim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
            multiAuthClaim.CreateReplacementFromRepair(selectedServiceCenterId)
            Me.PopulateFormFromBOs()
            Me.PopulateGrid()
            Me.EnableDisablePageControls()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnChangeServiceCenterYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModalSelectServiceCenterNo.Click
        Try
            ucSelectServiceCenter.Populate(Me.State.MyBO, Guid.Empty)
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.RegisterStartupScript("Startup", x)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'REQ-6230
    Private Sub btnPriceRetailSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPriceRetailSearch.Click
        Try
            Dim URL As String = "~/Claims/ClaimIndixSearchForm.aspx"
            params.Add(Me.State.MyBO.Id)
            params.Add(Me.State.MyBO.ClaimedEquipment.Model)
            params.Add(Me.State.MyBO.CompanyId)
            params.Add(Me.State.MyBO.Purchase_Price)
            params.Add(Me.State.MyBO.IndixId)

            Me.callPage(URL, params)

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try

    End Sub



    Private Sub btnChangeServiceCenterNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModalSelectServiceCenterYes.Click
        Try
            Dim multiAuthClaim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
            multiAuthClaim.CreateReplacementFromRepair(multiAuthClaim.NonVoidClaimAuthorizationList.FirstOrDefault().ServiceCenterId)
            Me.PopulateFormFromBOs()
            Me.EnableDisablePageControls()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnChangeFulfillment_Click(sender As Object, e As EventArgs) Handles btnChangeFulfillment.Click
        Try
            NavController.Navigate(Me, FlowEvents.EventClaimRecordingChangeFulfillment, New ClaimRecordingForm.Parameters(State.MyBO.Certificate.Id, State.MyBO.Id, Nothing, Codes.CasePurposeChangeFulfillment, Me.State.IsCallerAuthenticated))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnReplacementQuote_Click(sender As Object, e As EventArgs) Handles btnReplacementQuote.Click
        Try
            Me.callPage(Claims.ClaimReplacementQuoteForm.URL, New Claims.ClaimReplacementQuoteForm.Parameters(Me.State.MyBO.Id))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClaimDeductibleRefund_Click(sender As Object, e As EventArgs) Handles btnClaimDeductibleRefund.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_DEDUCTIBLE_REFUND, New ClaimDeductibleRefundForm.Parameters(Me.State.MyBO))

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

    Private Sub cboReasonClosed_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReasonClosed.SelectedIndexChanged
        Try
            If (Me.TextboxStatusCode.Text <> Codes.CLAIM_STATUS__CLOSED) Then
                'For a Claim that is NOT CLOSED
                If (Me.cboReasonClosed.SelectedIndex > 0) Then
                    If (Not Me.State.IsMultiAuthClaim) Then
                        If (((Not (CType(Me.State.MyBO, Claim).LoanerCenterId.Equals(Guid.Empty))) AndAlso (Not (CType(Me.State.MyBO, Claim).LoanerReturnedDate Is Nothing))) OrElse
                            (CType(Me.State.MyBO, Claim).LoanerCenterId.Equals(Guid.Empty))) Then
                            'When a Loaner has been taken and HAS been Returned OrElse_
                            'When a Loaner has NOT been taken
                            'The Claim CAN be Closed

                            Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED
                            'Me.AddControlMsg(Me.btnSave_WRITE, Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                        End If
                    End If
                Else
                    'The Claim CANNOT be Closed
                    Me.State.MyBO.StatusCode = Me.TextboxStatusCode.Text
                    Me.btnSave_WRITE.Attributes.Remove("onclick")
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub


#End Region

#Region "Error Handling"

#End Region

#Region "Navigation Control"

    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow("CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM"))
    End Sub

    Public Class StateControllerCreateNewReplacementClaim
        Implements IStateController


        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
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
            If Not newServCenter Is Nothing Then
                newClaim.ServiceCenterId = newServCenter.Id
            End If

            If (oldClaim.Source Is Nothing AndAlso Not newServCenter Is Nothing AndAlso Not newServCenter.Id.Equals(oldClaim.ServiceCenterId)) Then
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

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
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

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            navCtrl.FlowSession.Clear()
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT)
        End Sub
    End Class

    Public Class StateControllerChangeServiceCenter
        Implements IStateController

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
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

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
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


        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, BuidlLocateServiceCenterParameters(navCtrl))
        End Sub

        Function BuidlLocateServiceCenterParameters(ByVal navController As INavigationController) As LocateServiceCenterForm.Parameters
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
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property
#End Region
        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process

            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)
            Select Case Me.State.Stage
                Case ProcessingStage.AddingOKPrompt
                    Me.AddPrompt(callingPage)
                    Me.State.Stage = ProcessingStage.CheckingForUserAction
                Case ProcessingStage.CheckingForUserAction
                    Dim oldClaim As ClaimBase = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), ClaimBase)
                    Dim newComment As Comment = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT), Comment)
                    navCtrl.Navigate(Me.CallingPage, FlowEvents.EVENT_NEXT, New CommentForm.Parameters(newComment))
            End Select
        End Sub

        Sub AddPrompt(ByVal callingPage As System.Web.UI.Page)
            CType(callingPage, ElitaPlusPage).DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_CONFIRM)
        End Sub

    End Class

    Protected Shared Sub NotifyChanges(ByVal navCtrl As INavigationController)
        navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) = True
    End Sub

    Protected Shared Function CheckForChanges(ByVal navCtrl As INavigationController) As Boolean
        If Not navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) Is Nothing AndAlso
           CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG), Boolean) = True Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Handle Dropdown Events"

    Private Sub cboCauseOfLossId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCauseOfLossId.SelectedIndexChanged
        Try
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "CauseOfLossId", Me.cboCauseOfLossId)
                If (Not Me.State.IsMultiAuthClaim) Then
                    Dim claim As Claim = CType(Me.State.MyBO, Claim)
                    If Not .CauseOfLossId = Guid.Empty Then
                        claim.ClaimSpecialServiceId = claim.GetSpecialServiceValue()
                    Else
                        claim.ClaimSpecialServiceId = Me.State.NoId
                    End If
                    PopulateAuthorizedAmountFromPGPrices()
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cbousershipaddress_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbousershipaddress.SelectedIndexChanged

        Dim YesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        If cbousershipaddress.SelectedValue = YesId.ToString Then
            moUserControlContactInfo.Visible = True
            UserControlContactInfo.EnableDisablecontrol(False)
            UserControlAddress.EnableDisablecontrol(False)
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

    Private Sub cboFulfilmentMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFulfilmentMethod.SelectedIndexChanged
        If cboFulfilmentMethod.SelectedValue = Codes.FULFILMENT_METHOD_REIMBURSEMENT Then
            ControlMgr.SetVisibleControl(Me, Me.LabelAccountNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.TextboxAccountNumber, True)
            Me.SetEnabledForControlFamily(Me.TextboxAccountNumber, Me.State.IsEditMode, True)
        ElseIf cboFulfilmentMethod.SelectedValue = String.Empty Then
            ControlMgr.SetVisibleControl(Me, Me.LabelAccountNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.TextboxAccountNumber, False)
            Me.TextboxAccountNumber.Text = String.Empty
            Me.SetEnabledForControlFamily(Me.TextboxAccountNumber, Me.State.IsEditMode, False)
        End If

    End Sub

#End Region

#Region "Grid related"

    Public Sub PopulateGrid()
        Me.GridClaimAuthorization.AutoGenerateColumns = False
        Me.GridClaimAuthorization.DataSource = Me.State.claimAuthList
        Me.GridClaimAuthorization.DataBind()
        If (Me.State.claimAuthList.Count > 0) Then
            Me.State.IsGridVisible = True
        Else
            Me.State.IsGridVisible = False
        End If
        ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, Me.State.IsGridVisible)
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound

        Try
            Dim claimAuth As ClaimAuthorization = CType(e.Row.DataItem, ClaimAuthorization)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
               OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(1).FindControl("EditButton_WRITE") Is Nothing) Then
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
                e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, claimAuth.ClaimAuthorizationStatusCode)
                e.Row.Cells(Me.GRID_COL_AMOUNT_IDX).Text = Me.GetAmountFormattedString(claimAuth.AuthorizedAmount.Value)
                e.Row.Cells(Me.GRID_COL_CREATED_DATETIME_IDX).Text = Me.GetLongDate12FormattedStringNullable(claimAuth.CreatedDateTime.Value)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridClaimAuthorization.RowCommand
        Dim claimAuthorizationId As Guid
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    claimAuthorizationId = New Guid(e.CommandArgument.ToString())
                    Me.NavController.Navigate(Me, "claim_auth_detail", New ClaimAuthorizationDetailForm.Parameters(CType(Me.State.MyBO, MultiAuthClaim), claimAuthorizationId, Guid.Empty))
                    'Me.callPage(ClaimAuthorizationDetailForm.URL, New ClaimAuthorizationDetailForm.Parameters(CType(Me.State.MyBO, MultiAuthClaim), claimAuthorizationId, Guid.Empty))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridClaimAuthorization.Sorting
        Try
            If (Me.State.claimAuthList.Count > 0) Then
                Me.State.IsGridVisible = True
            Else
                Me.State.IsGridVisible = False
            End If
            ControlMgr.SetVisibleControl(Me, GridClaimAuthorization, Me.State.IsGridVisible)

            Me.GridClaimAuthorization.DataSource = Sort(Me.State.claimAuthList, e.SortExpression, e.SortDirection)
            Me.GridClaimAuthorization.DataBind()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Function Sort(ByVal list As List(Of ClaimAuthorization), ByVal sortBy As String, ByVal sortDirection As WebControls.SortDirection) As List(Of ClaimAuthorization)
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

        wsRequest.CompanyCode = Me.State.MyBO.Company.Code
        wsRequest.ClaimNumber = Me.State.MyBO.ClaimNumber

        Try
            wsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, FulfillmentDetails)(
                                                       GetClaimFulfillmentWebAppGatewayClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(ByVal c As WebAppGatewayClient)
                                                           Return c.GetFulfillmentDetails(wsRequest)
                                                       End Function)

            If wsResponse IsNot Nothing Then
                If wsResponse.GetType() Is GetType(FulfillmentDetails) Then
                    Me.State.FulfillmentDetailsResponse = wsResponse
                    PopulateClaimFulfillmentDetails()
                End If
            End If

        Catch ex As Exception
            ClearClaimFulfillmentDetails()
        End Try
    End Sub

    Private Sub BindExternalClaimFulfillmentDetails()
        Try
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_DF_API_URL), False)
            If String.IsNullOrEmpty(oWebPasswd.Url) Then
                Throw New ArgumentNullException($"Web Password entry not found or Dynamic Fulfillment Api Url not configured for Service Type {Codes.SERVICE_TYPE_DF_API_URL}")
            ElseIf String.IsNullOrEmpty(oWebPasswd.UserId) Or String.IsNullOrEmpty(oWebPasswd.Password) Then
                Throw New ArgumentNullException($"Web Password username or password not configured for Service Type {Codes.SERVICE_TYPE_DF_API_URL}")
            End If

            Dim uri As String = String.Format(oWebPasswd.Url, "Elita", Me.State.MyBO.ClaimNumber)
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
            dfControl.ClaimNumber = getClaimKey(Me.State.MyBO.Company.Code, Me.State.MyBO.ClaimNumber)
            phDynamicFulfillmentUI.Controls.Add(dfControl)
            dvClaimFulfillmentDetails.Visible = False

        Catch ex As Exception

        End Try
    End Sub
    Private Function getClaimKey(ByVal companyCode As String, ByVal claimNumber As String) As String
        Dim handler As New DynamicFulfillmentKeyHandler()
        Dim keys As New Dictionary(Of String, String)
        Dim tenant As String = $"{ElitaConfig.Current.General.Environment}-{ElitaConfig.Current.General.Hub}"
        keys.Add("Tenant", tenant)
        keys.Add("CompanyCode", companyCode)
        keys.Add("ClaimNumber", claimNumber)
        Return handler.Encode(keys)
    End Function

    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function

    Function GetPasscode(ByVal trackingNumber As String) As String
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Return ""
        End Try

    End Function

#End Region
End Class





