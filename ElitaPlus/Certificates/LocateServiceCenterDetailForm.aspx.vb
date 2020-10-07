'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (10/7/2004)  ********************


Partial Class LocateServiceCenterDetailForm
    Inherits ElitaPlusPage



    Protected WithEvents ErrorCtrl As ErrorController


    Protected WithEvents UserControlServiceCenterInfo As UserControlServiceCenterInfo
    Protected WithEvents UserControlLoanerCenterInfo As UserControlServiceCenterInfo
    Protected WithEvents UserControlCertificateInfo1 As Certificates.UserControlCertificateInfo


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
    Public Const URL As String = "~\Certificates\LocateServiceCenterDetailForm.aspx"
    Public Const GRID_COL_CODE As Integer = 0
    Public Const GRID_COL_NAME As Integer = 1
    Public Const NOTHING_SELECTED As Integer = 0
#End Region

#Region "Attributes"

    Private moSvcCtr As ServiceCenter

#End Region

#Region "Properties"


#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public SrvCenterId As Guid
        Public ShowAcceptButton As Boolean = True
        Public whenAcceptGoToCreateClaim As Boolean
        Public CertItemCoverageId As Guid
        Public ClaimId As Guid = Guid.Empty
        Public ComingFromDenyClaim As Boolean = False
        Public RecoveryButtonClick As Boolean = False
        Public objClaimedEquipment As ClaimEquipment

        Public Sub New(srvCenterId As Guid,
                       Optional ByVal showAcceptButton As Boolean = True,
                       Optional ByVal whenAcceptGoToCreateClaim As Boolean = True)
            Me.SrvCenterId = srvCenterId
            Me.ShowAcceptButton = showAcceptButton
            Me.whenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
        End Sub
        Public Sub New(srvCenterId As Guid,
                       certItemCoverageId As Guid,
                       ClaimId As Guid,
                       Optional ByVal showAcceptButton As Boolean = True,
                       Optional ByVal whenAcceptGoToCreateClaim As Boolean = True,
                       Optional ByVal ComingFromDenyClaim As Boolean = False,
                       Optional ByVal RecoveryButtonClick As Boolean = False,
                       Optional ByVal _claimedEquipment As ClaimEquipment = Nothing)
            Me.SrvCenterId = srvCenterId
            Me.CertItemCoverageId = certItemCoverageId
            Me.ClaimId = ClaimId
            Me.ShowAcceptButton = showAcceptButton
            Me.whenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
            Me.ComingFromDenyClaim = ComingFromDenyClaim
            Me.RecoveryButtonClick = RecoveryButtonClick
            objClaimedEquipment = _claimedEquipment
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceCenter
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceCenter)
            LastOperation = LastOp
            EditingBo = curEditingBo
        End Sub
    End Class

#End Region

#Region "Page State"


    Class MyState
        Public InputParameters As Parameters
        Public MyBO As ServiceCenter
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public MethodOfRepairIsReplacement As Boolean = False
        Public objClaimEquipment As ClaimEquipment
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
        State.InputParameters = CType(NavController.ParametersPassed, Parameters)
        State.MyBO = New ServiceCenter(State.InputParameters.SrvCenterId)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(CallingParameters, Parameters)
                State.MyBO = New ServiceCenter(pageParameters.SrvCenterId)
                State.InputParameters = pageParameters
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                If State.MyBO Is Nothing Then
                    State.MyBO = New ServiceCenter
                End If
                Trace(Me, "ServiceCenter=" & State.MyBO.Code)
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        With State
            ControlMgr.SetVisibleControl(Me, btnAccept_Write, .InputParameters.ShowAcceptButton)
            ControlMgr.SetVisibleControl(Me, UserControlLoanerCenterInfo, .MyBO.HasLoanerCenter)
            ControlMgr.SetVisibleControl(Me, LabelLoanerCenterHeader, .MyBO.HasLoanerCenter)
            If Not .InputParameters.CertItemCoverageId.Equals(Guid.Empty) Then
                ControlMgr.SetVisibleControl(Me, UserControlCertificateInfo1, True)
            Else
                ControlMgr.SetVisibleControl(Me, UserControlCertificateInfo1, False)
            End If
        End With
    End Sub

    Protected Sub PopulateFormFromBOs()
        Try
            With State
                If Not .InputParameters.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCov As New CertItemCoverage(.InputParameters.CertItemCoverageId)
                    Dim certItem As New CertItem(certItemCov.CertItemId)
                    Dim cert As New Certificate(certItemCov.CertId)
                    Dim companyBO As Company = New Company(cert.CompanyId)
                    Dim dv As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                    Dim riskTypeDesc As String = LookupListNew.GetDescriptionFromId(dv, certItem.RiskTypeId)
                    UserControlCertificateInfo1.InitController(certItem.CertId, riskTypeDesc, companyBO.Code)
                    If Not cert.MethodOfRepairId.Equals(Guid.Empty) AndAlso
                       cert.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                        State.MethodOfRepairIsReplacement = True
                    Else
                        State.MethodOfRepairIsReplacement = False
                    End If
                End If
                UserControlServiceCenterInfo.Bind(.MyBO, ErrorCtrl)
                If .MyBO.HasLoanerCenter Then
                    UserControlLoanerCenterInfo.Bind(.MyBO.LoanerCenter, ErrorCtrl)
                End If
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub



#End Region



#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
        Try
            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = State.MyBO
            NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAccept_Write_Click(sender As Object, e As System.EventArgs) Handles btnAccept_Write.Click
        Dim ClaimBO As Claim
        Try
            NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = State.MyBO

            If (State.InputParameters.RecoveryButtonClick = True) AndAlso (Not State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
                ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(State.InputParameters.ClaimId)
                NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = ClaimBO
                NavController.FlowSession(FlowSessionKeys.SESSION_RECOVERY_BUTTON_CLICK) = True
            End If

            If State.InputParameters.ComingFromDenyClaim = True Then
                NavController.FlowSession(FlowSessionKeys.SESSION_COMING_FROM_DENY_CLAIM) = True
            End If
            If State.MyBO.Shipping AndAlso State.MethodOfRepairIsReplacement AndAlso State.InputParameters.whenAcceptGoToCreateClaim Then
                NavController.Navigate(Me, "shipping_info")
            Else
                If (State.InputParameters.RecoveryButtonClick = True) AndAlso (Not State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
                    NavController.Navigate(Me, "accept", New NewClaimForm.Parameters(ClaimBO, State.InputParameters.SrvCenterId, State.InputParameters.CertItemCoverageId, , ClaimBO.LossDate, ClaimBO.ReportedDate, True, False))
                Else
                    NavController.Navigate(Me, "accept")
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        'If Me.State.InputParameters.WhenAcceptGoToCreateClaim Then
        '    'Me.callPage(NewClaimForm.URL, New NewClaimForm.Parameters(Nothing, Me.State.InputParameters.SrvCenterId, Me.State.InputParameters.CertItemCoverageId))
        '    Me.NavController.FlowSession("NewClaimForm.Parameters") = New NewClaimForm.Parameters(Nothing, Me.State.InputParameters.SrvCenterId, Me.State.InputParameters.CertItemCoverageId)
        '    Me.NavController.Navigate(Me, "accept")
        'Else
        '    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Accept, Me.State.MyBO), 2)
        'End If
    End Sub

#End Region



End Class



