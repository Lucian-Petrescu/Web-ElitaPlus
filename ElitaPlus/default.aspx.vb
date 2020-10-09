Imports System.Threading
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.OpenIdConnect

Partial Class _default
    Inherits ElitaPlusPage


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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            For Each key As String In Request.ServerVariables
                System.Diagnostics.Debug.WriteLine(key + ":" + Request.ServerVariables(key))
            Next
            Session("AppInitialized") = True
            If Request.QueryString("SameWindow") Is Nothing Then
                sameWindow.Value = "False"
            Else
                sameWindow.Value = Request.QueryString("SameWindow")
            End If
            'Check Security
            'If Session(ELPWebConstants.APPLICATIONUSER) Is Nothing Then
            '    LoginUser()
            'Else
            '    Me.nextPageURL.Value = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
            'End If
            'If Not CType(System.Threading.Thread.CurrentPrincipal, Object).GetType Is GetType(ElitaPlusPrincipal) Then
            '    LoginUser()
            'Else
            '    Me.nextPageURL.Value = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
            'End If

            Try

                If CType(System.Threading.Thread.CurrentPrincipal, Object).GetType IsNot GetType(ElitaPlusPrincipal) Then
                    If EnvironmentContext.Current.Environment = Environments.Development Then
                        '    Dim networkID As String = Assurant.Assurnet.Services.AuthenticationMgr.AuthenticatedUser(HttpContext.Current.Request)
                        '    PopulateUserSession(networkID, Request.UserHostAddress)
                        '    Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx", True)
                        'Else
                        Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/LoginForm.aspx", True)
                    Else
                        HttpContext.Current.GetOwinContext().Authentication.Challenge(
                            New AuthenticationProperties With 
                            {
                                .RedirectUri = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
                            }, 
                            OpenIdConnectAuthenticationDefaults.AuthenticationType)
                    End If
                Else
                    Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx", True)
                End If

            Catch ex As ThreadAbortException
                '' Swallow ThreadAbortException
            End Try
        End If
    End Sub

    'Sub LoginUser()
    '    If EnvironmentContext.Current.Environment <> Environments.Development Then
    '        Dim networkID As String = Assurant.Assurnet.Services.AuthenticationMgr.AuthenticatedUser(HttpContext.Current.Request)
    '        PopulateUserSession(networkID, Request.UserHostAddress)
    '        Me.nextPageURL.Value = ELPWebConstants.APPLICATION_PATH & "/Navigation/MainPage.aspx"
    '    Else
    '        Me.nextPageURL.Value = ELPWebConstants.APPLICATION_PATH & "/LoginForm.aspx"
    '    End If
    'End Sub

End Class
