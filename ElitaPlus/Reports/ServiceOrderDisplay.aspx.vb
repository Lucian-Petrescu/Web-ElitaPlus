Imports System.IO

Partial Class ServiceOrderDisplay
    Inherits ElitaPlusPage

#Region "Constants"
    Public Const URL As String = "ServiceOrderDisplay.aspx"
#End Region

    '    Public Enum InternalActions
    '        None
    '        ProcessNextClaim
    '        ProcessingNextClaim
    '    End Enum

    '#Region "State Parameters"
    '    Public Class Parameters
    '        Public ClaimBO As Claim
    '        Public nxtAction As InternalActions
    '        Public Sub New(ByVal oclaim As Claim, Optional ByVal NextAction As InternalActions = InternalActions.None)
    '            Me.ClaimBO = oclaim
    '            Me.nxtAction = NextAction
    '        End Sub
    '    End Class
    '#End Region

    '#Region "Page State"


    '    Class MyState
    '        Public ClaimBO As Claim
    '        Public InputParameters As Parameters
    '        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

    '    End Class

    '    Public Sub New()
    '        MyBase.New(New MyState)
    '    End Sub
    '    Protected Shadows ReadOnly Property State() As MyState
    '        Get

    '            If Me.NavController.State Is Nothing Then
    '                Me.NavController.State = New MyState
    '                If Not Me.NavController.ParametersPassed Is Nothing Then
    '                    Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
    '                    If Not Me.State.InputParameters.ClaimBO Is Nothing Then
    '                        Me.State.ClaimBO = Me.State.InputParameters.ClaimBO
    '                    End If
    '                End If
    '            End If

    '            Return CType(Me.NavController.State, MyState)
    '        End Get
    '    End Property


    '#End Region

#Region " Web Form Designer Generated Code "
    Protected WithEvents ErrorCtrl As ErrorController
    'Protected HiddenSaveChangesPromptResponse As HtmlControls.HtmlInputHidden


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ErrorCtrl.Hide()
    End Sub

#End Region

#Region "Button Handlers"
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click

        Try
            NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region


End Class
