Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect

Partial Class LoginForm
    Inherits ElitaPlusPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If EnvironmentContext.Current.Environment <> Environments.Development Then 'for Non Local
            If Not Page.IsPostBack Then
                Dim loginError As String = HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE)
                If Not String.IsNullOrEmpty(loginError) Then
                    HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE) = Nothing
                    ' Release the session
                    ForceLogOut()
                    ' show error if needed
                    lblMessage.Text = loginError
                ElseIf Not Request.IsAuthenticated Then
                    ' Try to login with Okta
                    OktaLogin()
                End If
            End If
        Else
            'Put user code to initialize the page here
            'This page should be hit only if we are in the dev environment
            If EnvironmentContext.Current.Environment <> Environments.Development Then
                Me.Response.Redirect("default.aspx")
            End If
            If Not Me.IsPostBack Then
                Me.TextBoxUserId.Text = AppConfig.DevEnvUserId

                ButtonOk.Attributes.Add("onmouseout", "SwapMyImage('ButtonOk','Navigation/images/loginBtnUp.gif')")
                ButtonOk.Attributes.Add("onmouseover", "SwapMyImage('ButtonOk','Navigation/images/loginBtnDown.gif')")

                Page.RegisterHiddenField("__EVENTTARGET", "ButtonOk")
            End If
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ButtonOk.Click
        If EnvironmentContext.Current.Environment <> Environments.Development Then
            OktaLogin()
        Else
            PopulateUserSession(Me.TextBoxUserId.Text, Request.UserHostAddress)
            Dim MainPageUrl As String = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
            Me.Response.Redirect(MainPageUrl)
        End If

    End Sub

    Private Sub OktaLogin()
        HttpContext.Current.GetOwinContext().Authentication.Challenge(New AuthenticationProperties With {
            .RedirectUri = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
        }, OpenIdConnectAuthenticationDefaults.AuthenticationType)

    End Sub

    Private Sub ForceLogOut()
        Session.Clear()
        Session.Abandon()
    End Sub
End Class
