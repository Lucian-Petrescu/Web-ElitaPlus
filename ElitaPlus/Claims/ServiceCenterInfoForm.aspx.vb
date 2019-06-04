Partial Class ServiceCenterInfoForm
    'Inherits System.Web.UI.Page
    Inherits ElitaPlusPage

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents UserControlServiceCenterInfo As UserControlServiceCenterInfo
    Protected WithEvents Label40 As System.Web.UI.WebControls.Label


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

    Public Const URL As String = "~/Claims/ServiceCenterInfoForm.aspx"
    'Public Const SERVICE_CENTER_ID_SESSION_KEY As String = "SERVICE_CENTER_ID_SESSION_KEY"

#End Region


#Region "Page Parameters"
    Public Class Parameters
        Public SrvCenterId As Guid
        Public Sub New(ByVal srvCenterId As Guid, Optional ByVal showAcceptButton As Boolean = True)
            Me.SrvCenterId = srvCenterId
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

    Class BaseState
        Public NavCtrl As INavigationController
    End Class



    Class MyState
        Public MyBO As ServiceCenter
        Public ShowAcceptButton As Boolean = True
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Try
            Dim pageParameters As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
            Me.State.MyBO = New ServiceCenter(pageParameters.SrvCenterId)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
                Me.State.MyBO = New ServiceCenter(pageParameters.SrvCenterId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

    '#Region "Page Return Type"
    '    Public Class ReturnType
    '        Public LastOperation As DetailPageCommand
    '        Public EditingBo As Claim
    '        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Claim)
    '            Me.LastOperation = LastOp
    '            Me.EditingBo = curEditingBo
    '        End Sub
    '    End Class
    '#End Region

    '#Region "Page State"

    '    Class MyState
    '        Public MyBO As ServiceCenter
    '        Public ScreenSnapShotBO As ServiceCenter
    '        Public IsEditMode As Boolean = False

    '        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    '        Public LastErrMsg As String
    '    End Class

    '    Public Sub New()
    '        MyBase.New(True)
    '    End Sub

    '    Protected Shadows ReadOnly Property State() As MyState
    '        Get
    '            Return CType(MyBase.State, MyState)
    '        End Get
    '    End Property

    '    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
    '        Try
    '            If Not Me.CallingParameters Is Nothing Then
    '                'Get the id from the parent
    '                Me.State.MyBO = New ServiceCenter(CType(Me.CallingParameters, Guid))
    '            End If
    '        Catch ex As Exception
    '            Me.HandleErrors(ex, Me.ErrorCtrl)
    '        End Try

    '    End Sub

    '#End Region


#Region "Member Variables"

    'Private ServiceCenterId As Guid

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
                Me.PopulateFormFromBOs()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub


    'Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    'Put user code to initialize the page here

    '    Me.ErrorCtrl.Clear_Hide()
    '    Try
    '        If Not Me.IsPostBack Then
    '            Me.MenuEnabled = False
    '            Me.ServiceCenterId = CType(Session(SERVICE_CENTER_ID_SESSION_KEY), Guid)
    '            'Me.btnBack.Attributes.Add("onclick", "javascript:window.close();")
    '            If Me.State.MyBO Is Nothing Then
    '                Me.State.MyBO = New ServiceCenter
    '            End If
    '            Me.PopulateFormFromBOs()
    '            Me.AddLabelDecorations(Me.State.MyBO)
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try
    '    Me.ShowMissingTranslations(Me.ErrorCtrl)
    'End Sub

#End Region


#Region "Controlling Logic"

    Protected Sub PopulateFormFromBOs()

        Try
            With Me.State
                Me.UserControlServiceCenterInfo.Bind(.MyBO, Me.ErrorCtrl)
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

        'Try
        '    Dim svcCtrBO As ServiceCenter = New ServiceCenter(Me.ServiceCenterId)
        '    Me.UserControlServiceCenterInfo.Bind(svcCtrBO, ErrorCtrl)
        '    'Session.Remove(Me.SERVICE_CENTER_ID_SESSION_KEY)
        'Catch ex As Exception
        '    Me.HandleErrors(ex, Me.ErrorCtrl)
        'End Try

    End Sub

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Error Handling"

#End Region


End Class



