Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ServiceModel.Security
Imports ElitaInternalWS.Security
Imports Assurant.ElitaPlus.Common

Public Class ElitaWcf

#Region "Operations"

    Protected Sub DoWork()
    End Sub

    Public Function Hello(ByVal name As String) As String
        '  Return "Hello, " & name
        Return ElitaService.Hello(name)
    End Function

    Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) As String
        Dim token As String

        'AppConfig.SetModeLog(AppConfig.DB_LOG, True)
        'AppConfig.Debug("Entered LoginBody " & networkID & " @ " & password & " @ " & group)
        'AppConfig.Debug("Env " & AppConfig.CurrentEnvironment)
        '    Reject any requests which are not valid SOAP requests
        If OperationContext.Current.RequestContext Is Nothing Then
            ' AppConfig.Debug("Only SOAP requests are permitted.")
            Throw New ApplicationException("Only SOAP requests are permitted.")
        End If


        Try
            token = ElitaService.VerifyLogin(True, networkID, networkID, password, group)
        Catch ex As Exception
            AppConfig.Debug("LoginBody Exception: " & ex.Message)
            Throw New SecurityAccessDeniedException("Access Denied")
        End Try

        Return token

    End Function

    Public ReadOnly Property GetContextPassword() As String
        Get
            If (GetIsCorrectContextIdentity()) Then
                Return (CType(ServiceSecurityContext.Current.PrimaryIdentity, PasswordIdentity)).Password
            End If

            Return String.Empty
        End Get

    End Property

    Public ReadOnly Property GetContextUserName() As String
        Get
            Return ServiceSecurityContext.Current.PrimaryIdentity.Name
        End Get
    End Property

    Public ReadOnly Property GetIsCorrectContextIdentity() As Boolean
        Get
            Return TypeOf (ServiceSecurityContext.Current.PrimaryIdentity) _
                    Is PasswordIdentity
        End Get
    End Property


    Public Function Login() As String
        Dim token As String
        Dim complexUsername As String
        Dim appPassword As String

        '    Reject any requests which are not valid SOAP requests
        If OperationContext.Current.RequestContext Is Nothing Then Throw New ApplicationException("Only SOAP requests are permitted.")

        Try
            complexUsername = GetContextUserName()
            appPassword = GetContextPassword()
            token = ElitaService.VerifyLogin(True, complexUsername, appPassword)
        Catch ex As Exception
            Throw New SecurityAccessDeniedException("Access Denied")
        End Try

        Return token

    End Function

    Public Function ProcessRequest(ByVal token As String, _
                                               ByVal functionToProcess As String, _
                                               ByVal xmlStringDataIn As String, _
                                               ByVal webServiceName As String) As String

        Return ElitaService.ProcessRequest(True, token, functionToProcess, xmlStringDataIn, _
                                            webServiceName)
    End Function
#End Region

    '#Region "Authentication"

    'Private Function VerifyLogin(ByVal appId As String, ByVal networkId As String, ByVal appPassword As String, ByVal group As String) As String
    '    Dim isValidUser As Boolean = False
    '    Dim token As String

    '    LoginElita(networkId)
    '    isValidUser = Authentication.IsLdapUser(group, appId, appPassword)

    '    If Not isValidUser Then Throw New SecurityAccessDeniedException("Access Denied")

    '    AppConfig.Debug("Login to WebService")
    '    token = Authentication.CreateWSToken(networkId)

    '    Return token
    'End Function

    'Private Sub LoginElita(ByVal networkID As String)
    '    Dim oAuthentication As New Authentication
    '    oAuthentication.CreatePrincipal(networkID)
    '    Authentication.SetCulture()

    'End Sub
    '#End Region

End Class
