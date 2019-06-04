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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Public Sub New(ByVal srvCenterId As Guid,
                       Optional ByVal showAcceptButton As Boolean = True,
                       Optional ByVal whenAcceptGoToCreateClaim As Boolean = True)
            Me.SrvCenterId = srvCenterId
            Me.ShowAcceptButton = showAcceptButton
            Me.whenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
        End Sub
        Public Sub New(ByVal srvCenterId As Guid,
                       ByVal certItemCoverageId As Guid,
                       ByVal ClaimId As Guid,
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
            Me.objClaimedEquipment = _claimedEquipment
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceCenter
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ServiceCenter)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
        Me.State.MyBO = New ServiceCenter(Me.State.InputParameters.SrvCenterId)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(Me.CallingParameters, Parameters)
                Me.State.MyBO = New ServiceCenter(pageParameters.SrvCenterId)
                Me.State.InputParameters = pageParameters
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ServiceCenter
                End If
                Trace(Me, "ServiceCenter=" & Me.State.MyBO.Code)
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        With Me.State
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
            With Me.State
                If Not .InputParameters.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCov As New CertItemCoverage(.InputParameters.CertItemCoverageId)
                    Dim certItem As New CertItem(certItemCov.CertItemId)
                    Dim cert As New Certificate(certItemCov.CertId)
                    Dim companyBO As Company = New Company(cert.CompanyId)
                    Dim dv As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                    Dim riskTypeDesc As String = LookupListNew.GetDescriptionFromId(dv, certItem.RiskTypeId)
                    Me.UserControlCertificateInfo1.InitController(certItem.CertId, riskTypeDesc, companyBO.Code)
                    If Not cert.MethodOfRepairId.Equals(Guid.Empty) AndAlso
                       cert.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                        Me.State.MethodOfRepairIsReplacement = True
                    Else
                        Me.State.MethodOfRepairIsReplacement = False
                    End If
                End If
                Me.UserControlServiceCenterInfo.Bind(.MyBO, Me.ErrorCtrl)
                If .MyBO.HasLoanerCenter Then
                    Me.UserControlLoanerCenterInfo.Bind(.MyBO.LoanerCenter, Me.ErrorCtrl)
                End If
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



#End Region



#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO))
        Try
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Me.State.MyBO
            Me.NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAccept_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept_Write.Click
        Dim ClaimBO As Claim
        Try
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = Me.State.MyBO

            If (Me.State.InputParameters.RecoveryButtonClick = True) AndAlso (Not Me.State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
                ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.InputParameters.ClaimId)
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = ClaimBO
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_RECOVERY_BUTTON_CLICK) = True
            End If

            If Me.State.InputParameters.ComingFromDenyClaim = True Then
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_COMING_FROM_DENY_CLAIM) = True
            End If
            If Me.State.MyBO.Shipping AndAlso Me.State.MethodOfRepairIsReplacement AndAlso Me.State.InputParameters.whenAcceptGoToCreateClaim Then
                Me.NavController.Navigate(Me, "shipping_info")
            Else
                If (Me.State.InputParameters.RecoveryButtonClick = True) AndAlso (Not Me.State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
                    Me.NavController.Navigate(Me, "accept", New NewClaimForm.Parameters(ClaimBO, Me.State.InputParameters.SrvCenterId, Me.State.InputParameters.CertItemCoverageId, , ClaimBO.LossDate, ClaimBO.ReportedDate, True, False))
                Else
                    Me.NavController.Navigate(Me, "accept")
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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



