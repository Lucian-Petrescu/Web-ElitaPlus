Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect

Partial Class LogOutForm
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        'Put user code to initialize the page here
        CleanUp()
        Me.ReconnectToTheSite()
        '   AppConfig.Debug("User Logged Out")
    End Sub

#Region "Private Methods"
    Private Sub DeleteAllSiteMinderCookies()
        Dim cookie As HttpCookie
        Dim i As Integer
        For i = 0 To Me.Request.Cookies.Count - 1
            cookie = Me.Request.Cookies.Item(i)
            'If cookie.Name.ToUpper.StartsWith("SM") OrElse cookie.Name.ToUpper.StartsWith("FORMCRED") Then
            'remove the cookie
            cookie.Expires = DateTime.Now.AddYears(-30)
            Response.Cookies.Add(cookie)
            'End If
        Next
    End Sub

    Private Sub CleanUp()
        Session.Clear()
        Session.Abandon()

        If (TypeOf System.Threading.Thread.CurrentPrincipal Is ElitaPlusPrincipal) Then
            Dim idToken As String = ElitaPlusPrincipal.Current.IdToken
            If Not String.IsNullOrEmpty(idToken) Then
                ' delete okta authentification cookies
                Context.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType,
                    OpenIdConnectAuthenticationDefaults.AuthenticationType
                )
            End If
        End If
    End Sub

    Private Sub ReconnectToTheSite()
        '  Dim newLocation As String = """http://" & Me.Request.Url.Host & Me.Request.ApplicationPath() & "/" & "default.aspx" & """"
        'Dim newLocation As String = """http://" & Me.Request.Url.Host & Me.Request.ApplicationPath() & "/" & "default.aspx"
        Dim newLocation As String = """" & ELPWebConstants.APPLICATION_PATH & "/default.aspx"
        Dim sJavaScript As String

        newLocation &= "?SameWindow=True" & """"
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "SMLogout();" & Environment.NewLine
        '    sJavaScript &= " window.parent.close(); " & Environment.NewLine
        sJavaScript &= "window.location.href = " & newLocation & ";" & Environment.NewLine
        '    sJavaScript &= "window.open(" & newLocation & ");window.close();" & Environment.NewLine

        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("LogOut", sJavaScript)
    End Sub
#End Region

End Class
