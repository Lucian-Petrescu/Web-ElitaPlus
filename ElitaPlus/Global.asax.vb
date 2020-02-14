Imports System.IO.Compression
Imports System.Net
Imports System.Web.Routing

Public Class [Global]
    Inherits System.Web.HttpApplication



#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

    End Sub

#End Region

    Public Function RemoteCertificateValidationCallback(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started        

#If DEBUG Then
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf RemoteCertificateValidationCallback)
#End If
        'Set TimeZone for Application
        Assurant.Elita.ApplicationDateTime.ApplicationTimeZoneFunc = AddressOf GetTimeZoneInfo
    End Sub

    Private applicationTimeZoneInfo As TimeZoneInfo
    Private SyncRoot As Object = New Object

    ''' <summary>
    ''' Reads TimeZone information from web.config
    ''' </summary>
    ''' <returns> returns Instance of <cref="TimezoneInfo"></returns>

    Private Function GetTimeZoneInfo() As TimeZoneInfo
        If (Not applicationTimeZoneInfo Is Nothing) Then
            Return applicationTimeZoneInfo
        End If

        Dim localApplicationTimeZoneInfo As TimeZoneInfo
        Dim timeZoneID As String = ConfigurationManager.AppSettings("Assurant.Elita.TimeZoneId")

        If (String.IsNullOrWhiteSpace(timeZoneID)) Then
            localApplicationTimeZoneInfo = System.TimeZoneInfo.Local
        Else
            Try
                localApplicationTimeZoneInfo = System.TimeZoneInfo.FindSystemTimeZoneById(timeZoneID)
            Catch ex As Exception
                localApplicationTimeZoneInfo = System.TimeZoneInfo.Local
            End Try
        End If

        SyncLock SyncRoot
            If (applicationTimeZoneInfo Is Nothing) Then
                applicationTimeZoneInfo = localApplicationTimeZoneInfo
            End If
        End SyncLock
        Return applicationTimeZoneInfo

    End Function


    Sub Session_OnStart(ByVal sender As Object, ByVal e As EventArgs)

        Dim cookie As HttpCookie = Me.Request.Cookies.Item(ELPWebConstants.ELITA_PLUS_AUTHENTICATION_COOKIE & "-" & Session.SessionID)
        Dim connCookie As HttpCookie
        '   Dim connType As String = ELPWebConstants.CONNECTION_TYPE_DEFAULT

        If cookie Is Nothing Then

            cookie = New HttpCookie(ELPWebConstants.ELITA_PLUS_AUTHENTICATION_COOKIE & "-" & Session.SessionID)
            cookie.Expires = DateTime.Now.AddDays(1.0)
            cookie.Value = "0"
            Me.Response.Cookies.Add(cookie)

            'If Not Request.QueryString("Conn") Is Nothing Then
            '    connType = Request.QueryString("Conn").ToUpper
            'End If
            'connCookie = New HttpCookie(ELPWebConstants.ELITA_PLUS_CONN_COOKIE)
            'connCookie.Value = connType
            'Me.Response.Cookies.Add(connCookie)
        Else
            cookie.Value = "1"
            Me.Response.Cookies.Set(cookie)
        End If
        ' Fires when the session is started

        'ARF 07/15/04 moved this logic to the ElitaPlusPage

        'If Session(ELPWebConstants.APPLICATIONUSER) Is Nothing Then
        '    LoginUser()
        'End If
    End Sub

    'Sub LoginUser()
    '    'Dim networkID As String = Assurant.Common.SiteMinder.AuthenticationMgr.GetAuthenticatedUser(HttpContext.Current.Request)
    '    Dim networkID As String
    '    If AppConfig.CurrentEnvironment = AppConfig.DEFAULT_ENVIRONMENT Then
    '        networkID = AppConfig.DevEnvUserId
    '    Else
    '        networkID = Assurant.AssurNet.Services.AuthenticationMgr.AuthenticatedUser(HttpContext.Current.Request)
    '    End If
    '    PopulateUserSession(networkID, Request.UserHostAddress)
    'End Sub

    'Private Function PopulateUserSession(ByVal networkID As String, ByVal machineName As String) As Boolean

    '    '-------------------------------------
    '    'Name:PopulateUserSession
    '    'Purpose:pass the networkname to the business object to load the 
    '    ' user's company and other info.
    '    'Input Values:
    '    'Uses:
    '    '-------------------------------------

    '    Dim CFG_MAIN_PAGE_NAME As String = ConfigurationMgr.ConfigValue(ELPWebConstants.MAIN_PAGE_NAME)
    '    Dim oApplicationUser As New ApplicationUser(networkID, machineName)
    '    Dim CFG_AUTH_FAILED As String 'TODO Check This with Mark ' = ConfigurationMgr.ConfigValue("AUTHENTICATION_FAILED")
    '    Dim sLoginMessage As String
    '    Dim oLogin_Mgr As New LoginMgr()
    '    Dim bResult As Boolean
    '    Dim nEnglishLanguageID As Guid


    '    If (oApplicationUser.IsValidUser = True) Then

    '        'now check if it's a good login.
    '        bResult = oLogin_Mgr.Login(networkID, Request.UserHostAddress, sLoginMessage, oApplicationUser.LanguageID)

    '        If bResult = True Then
    '            'save the elita user object into session.
    '            Session(ELPWebConstants.APPLICATIONUSER) = oApplicationUser

    '        Else
    '            'if the loginmgr returns false then send to message page.
    '            ' and append the message .
    '            'this means the login mgr failed.
    '            'sLoginMessage was loaded by the login mgr.
    '            Response.Redirect(ELPWebConstants.MESSAGE_PAGE_WITH_ERROR_INFO & sLoginMessage)

    '        End If
    '    Else
    '        'if the applicationmgr returns not valid user then send to message page
    '        ' and set the language to english because it is unknown due to the login failure
    '        Dim translator As New TranslationProcess()
    '        sLoginMessage = ApplicationMessages.GetApplicationMessage(ELPWebConstants.UI_INVALID_LOGIN_ERR_MSG, translator.Get_EnglishLanguageID)
    '        Response.Redirect(ErrorForm.PAGE_NAME & "?Message=" & sLoginMessage & "&")
    '    End If

    'End Function

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request

        'set the localization based on the client's requests

        'ARF 8/16/2004. Commented out this to get the culture of the user selected language instead. 
        'See. Global_AcquireRequestState
        'LocalizationMgr.SetCurrentCulture(Request)
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        '  Response.Redirect(ErrorForm.PAGE_NAME)
    End Sub

    Sub Session_OnEnd(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        'logout the user.

        ' ????? What does this do? ????
        'Dim oApplicationUser As ApplicationUser = CType(Session(ELPWebConstants.APPLICATIONUSER), ApplicationUser)
        'If oApplicationUser Is Nothing = False Then
        '    Dim oLoginMgr As New LoginMgr
        '    oLoginMgr.LogOut(oApplicationUser.NetworkID, oApplicationUser.MachineName)
        'End If
    End Sub

    Sub Logout()
        'Dim AppPath As String = Request.ApplicationPath
        'Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        'Dim url As String = "http://" & ServerName & AppPath & "/Common/"
        Dim url As String = GetHttpUrl() & "/Common/"
        Response.Redirect(url & "LogOutForm.aspx")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    'Private Function GetNoSecureHttpUrl() As String
    '    Dim AppPath As String = Request.ApplicationPath
    '    Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
    '    Dim url As String = "http://" & ServerName & AppPath & "/"

    '    Return url
    'End Function

    'Private Function GetHttpUrl() As String
    '    Dim url As String

    '    url = GetNoSecureHttpUrl()
    '    'If AppConfig.CurrentEnvironment.ToUpper = AppConfig.PRODUCTION_ENVIRONMENT Then
    '    '    ' Production
    '    '    Try
    '    '        If Request.Cookies("SSLCheck").Value = "Yes" Then
    '    '            'SSL Check is yes, it is secure
    '    '            ' url = https://Secure-W1.assurant.com/elitaplus/
    '    '            url = AppConfig.Application.HttpUrl
    '    '        End If
    '    '    Catch ex As Exception
    '    '        ' SSL CHECk is not there, Production no secure
    '    '        url = GetNoSecureHttpUrl()
    '    '    End Try
    '    'Else
    '    '    ' No Production
    '    '    url = GetNoSecureHttpUrl()
    '    'End If

    '    Return url

    'End Function

    'Private Function GetNoSecureHttpUrl() As String
    '    Dim AppPath As String = Request.ApplicationPath
    '    Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
    '    Dim url As String = "http://" & ServerName & AppPath & "/"

    '    Return url
    'End Function

    Private Function GetHttpUrl() As String
        Dim url As String

        url = ELPWebConstants.APPLICATION_PATH

        Return url
    End Function


    Private Sub Global_AcquireRequestState(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.AcquireRequestState
        'here we are not allowing ajax nor asmx(web service) to be processed by this code
        If Me.Request.Url.AbsolutePath.ToLower().IndexOf(".axd") < 0 AndAlso _
            Me.Request.Url.AbsolutePath.ToLower().IndexOf(".asmx") < 0 Then
            If Me.Request.Url.AbsolutePath.ToLower().IndexOf("errorform.aspx") >= 0 Then
                Dim cookie As HttpCookie = Me.Request.Cookies.Item(ELPWebConstants.ELITA_PLUS_ERROR_COOKIE & "-" & Session.SessionID)
                If Not cookie Is Nothing Then Session(ErrorForm.MESSAGE_KEY_NAME) = cookie.Value
                Return
            End If
            System.Threading.Thread.CurrentPrincipal = DirectCast(Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY), ElitaPlusPrincipal)
            HttpContext.Current.User = System.Threading.Thread.CurrentPrincipal
            '  System.Threading.Thread.CurrentPrincipal = DirectCast(Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY), ElitaPlusPrincipal)
            'If CType(System.Threading.Thread.CurrentPrincipal, Object).GetType Is GetType(ElitaPlusPrincipal) Then
            '    'Once the user is logged in we will be refreshing the culture with the user's language culture
            '    Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            '    Dim cultureName As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, langId)
            '    'LocalizationMgr.SetCurrentCulture(cultureName)
            '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(cultureName)
            'End If
            Authentication.SetCulture()
            If Me.Request.Url.AbsolutePath.ToLower().IndexOf("default.aspx") < 0 AndAlso _
               Me.Request.Url.AbsolutePath.ToLower().IndexOf("logoutform.aspx") < 0 AndAlso _
               Session("AppInitialized") Is Nothing Then

                If Me.Request.Url.AbsolutePath.ToLower().IndexOf("downloadreportdata.aspx") > 0 Then
                    Session("RedirectUrl") = Me.Request.Url.AbsoluteUri
                End If

                Dim cookie As HttpCookie = Me.Response.Cookies.Item(ELPWebConstants.ELITA_PLUS_AUTHENTICATION_COOKIE & "-" & Session.SessionID)

                If Not cookie Is Nothing AndAlso cookie.Value = "1" Then
                    Me.Logout()
                Else
                    Dim url As String = GetHttpUrl() & "/"
                    Response.Redirect(url & "default.aspx", True)
                End If
            End If
        End If
    End Sub

    Private Sub Global_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Dim oStArray As Array
        Dim sFrom, sFirstStack, sMessage As String
        Dim nPos As Integer
        Dim Ex As Exception = Server.GetLastError().GetBaseException
        Dim logEx As ElitaPlusException
        If Not Ex.GetType.IsSubclassOf(GetType(ElitaPlusException)) Then
            Try
                Throw New UnHandledException(Ex)
            Catch ex1 As UnHandledException
                logEx = ex1
            End Try
        Else
            logEx = CType(Ex, ElitaPlusException)
        End If
        AppConfig.Log(CType(logEx, Exception))
        If Not CType(System.Threading.Thread.CurrentPrincipal, Object).GetType Is GetType(ElitaPlusPrincipal) Then
            ' The Principal has not been created yet
            sMessage = "CONFIG VALUES CAN NOT BE OBTAINED "
        ElseIf ElitaPlusIdentity.Current.FtpHostname = String.Empty Then
            sMessage = "DATABASE IS DOWN OR ELP_SERVERS RECORD NOT FOUND "
        ElseIf ElitaPlusIdentity.Current.ActiveUser Is Nothing Then
            sMessage = "USER ID NOT FOUND "
        Else
            sMessage = TranslateLabelOrMessage(logEx.Code, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End If

        If EnvironmentContext.Current.Environment = Environments.Development Then
            ' Development
            sMessage &= Environment.NewLine & logEx.ToString
        Else
            ' Test, Model or Production
            Dim cookie As HttpCookie = Me.Request.Cookies.Item(ELPWebConstants.ELITA_PLUS_ERROR_COOKIE & "-" & Session.SessionID)

            If cookie Is Nothing Then
                cookie = New HttpCookie(ELPWebConstants.ELITA_PLUS_ERROR_COOKIE & "-" & Session.SessionID)
                cookie.Expires = DateTime.Now.AddDays(1.0)
                cookie.Value = sMessage
                Me.Response.Cookies.Add(cookie)
            Else
                cookie.Value = sMessage
                Me.Response.Cookies.Set(cookie)
            End If
            Session(ErrorForm.MESSAGE_KEY_NAME) = sMessage
            Response.Redirect(ErrorForm.PAGE_NAME)
        End If

    End Sub

#Region "Translation"
    Public Function TranslateLabelOrMessage(ByVal UIProgCode As String, ByVal LangId As Guid) As String
        Dim TransProcObj As New TranslationProcess
        Dim oTranslationItem As New TranslationItem
        Dim Coll As New TranslationItemArray
        With oTranslationItem
            .TextToTranslate = UIProgCode.ToUpper
        End With
        Coll.Add(oTranslationItem)
        TransProcObj.TranslateList(Coll, LangId)
        Return oTranslationItem.Translation
    End Function

    Private Sub Global_PostAuthenticateRequest(sender As Object, e As EventArgs) Handles Me.PostAuthenticateRequest
        HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required)
    End Sub
#End Region
End Class
