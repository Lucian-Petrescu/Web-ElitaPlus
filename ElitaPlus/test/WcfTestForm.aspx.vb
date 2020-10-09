'Imports System.IdentityModel.Tokens
Imports System.ServiceModel

Partial Public Class WcfTestForm
    ' Inherits System.Web.UI.Page
    Inherits ElitaPlusPage

#Region "Constants"

    Private Const VSC_TEST_SESSION As String = "VSC_TEST_SESSION"
    Private Const CERTIFICATENAME As String = "CN=Elita_Cert"

#End Region

#Region "Variables"

    Class MyState
        Public elitaToken As String
    End Class

    Private moState As MyState

#End Region

#Region "State"

    Private Sub SetState(elitaToken As String)
        moState = New MyState
        moState.elitaToken = elitaToken
        Session(VSC_TEST_SESSION) = moState
    End Sub

    Private Sub GetState()
        moState = CType(Session(VSC_TEST_SESSION), MyState)
    End Sub

#End Region

#Region "Handlers"

#Region "Handlers-Init"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InstallValidateCert()
        End If
    End Sub

#End Region

#Region "Handler-Buttons"

    Protected Sub helloBtn_Click(sender As Object, e As EventArgs) Handles helloBtn.Click
        ' Dim wcfTestServer As New OlitaWcfRef.OlitaWcfClient
        ' Dim wcfTestServer As New GalaxyWcfRef.GalaxyWcfClient
        Dim wcfTestServer As New TestWcfRef.WcfTestClient
        ' Dim wcfTestServer As New ServiceReference1.WcfTestClient
        ' Dim wcfTestServer As New DevWsWcfRef.WcfTestClient
        Dim result As String

        Try
            'System.Net.ServicePointManager.ServerCertificateValidationCallback = _
            '    New System.Net.Security.RemoteCertificateValidationCallback( _
            '                        AddressOf WcfTestForm.validateCertificate)
            'Dim elitaToken As String
            'ConfigureProcHeader(wcfTestServer, elitaToken)
            wcfTestServer.ClientCredentials.UserName.UserName = "Token"
            wcfTestServer.ClientCredentials.UserName.Password = String.Empty
            result = wcfTestServer.Hello("Fresy")
            statusLabel.Text = result
            statusLabel.Text = statusLabel.Text & " From " & wcfTestServer.Endpoint.ListenUri.AbsoluteUri
            wcfTestServer.Close()
        Catch ex As Exception
            statusLabel.Text = "Hello Error, please try again" & Environment.NewLine & ex.Message

        End Try

    End Sub

    Private Shared Function Get_EndPoint(url As String) As EndpointAddress
        Dim eab As EndpointAddressBuilder

        eab = New EndpointAddressBuilder
        eab.Uri = New Uri(url)

        Return eab.ToEndpointAddress
    End Function

    ' LoginBody
    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        ' Dim wcfTestServer As New OlitaWcfRef.OlitaWcfClient
        ' Dim wcfTestServer As New GalaxyWcfRef.GalaxyWcfClient
        Dim wcfTestServer As New TestWcfRef.WcfTestClient

        Dim elitaToken As String
        '  Dim group As String = "Services"
        Dim group As String = "InternalUsers"

        Try
            ' wcfTestServer.Endpoint.Address = Get_EndPoint(wcfTestServer.Endpoint.ListenUri.AbsoluteUri & "?Hub=EU")
            '  wcfTestServer.Endpoint.Address = Get_EndPoint("https://elitaplus.sa.assurant.com/ElitaInternalWS/Galaxy/GalaxyWcf.svc?Hub=NO" & "?Hub=EU")
            elitaToken = wcfTestServer.LoginBody(userTextBox.Text, passwordTextBox.Text, group)
            SetState(elitaToken)
            statusLabel.Text = "Select your request"
            statusLabel.ForeColor = Color.Navy
            statusLabel.Text = statusLabel.Text & " From " & wcfTestServer.Endpoint.ListenUri.AbsoluteUri
            wcfTestServer.Close()
        Catch ex As Exception
            statusLabel.Text = "Login Error, please try again" & Environment.NewLine & ex.Message
        End Try
    End Sub

    ' Login with Header
    'Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginButton.Click
    '    ' Dim wcfTestServer As New OlitaWcfRef.OlitaWcfClient
    '    'Dim wcfTestServer As New GalaxyWcfRef.GalaxyWcfClient
    '    Dim wcfTestServer As New TestWcfRef.WcfTestClient
    '    ' Dim wcfTestServer As New DevWsWcfRef.WcfTestClient

    '    Dim elitaToken As String

    '    Try
    '        ''System.Net.ServicePointManager.ServerCertificateValidationCallback += _
    '        ''  New System.Net.Security.RemoteCertificateValidationCallback(Program.validateCertificate)
    '        'System.Net.ServicePointManager.ServerCertificateValidationCallback = _
    '        '    New System.Net.Security.RemoteCertificateValidationCallback( _
    '        '                        AddressOf WcfTestForm.validateCertificate)
    '        ConfigureLoginHeader(wcfTestServer)
    '        elitaToken = wcfTestServer.Login()
    '        SetState(elitaToken)
    '        statusLabel.Text = "Select your request"
    '        statusLabel.ForeColor = Color.Navy
    '        statusLabel.Text = statusLabel.Text & " From " & wcfTestServer.Endpoint.ListenUri.AbsoluteUri
    '        '  wcfTestServer.Close()
    '    Catch ex As Exception
    '        statusLabel.Text = "Login Error, please try again" & Environment.NewLine & ex.Message
    '    Finally
    '        wcfTestServer.Close()
    '    End Try
    'End Sub


    Protected Sub ProcessReqBtn_Click(sender As Object, e As EventArgs) Handles ProcessReqBtn.Click
        '  Dim wcfTestServer As New OlitaWcfRef.OlitaWcfClient
        '  Dim wcfTestServer As New GalaxyWcfRef.GalaxyWcfClient
        Dim wcfTestServer As New TestWcfRef.WcfTestClient
        Dim elitaToken, funcToProc, dataIn, serviceName, result As String

        GetState()
        elitaToken = moState.elitaToken
        'funcToProc = "GetCertUsingTranNo"
        'dataIn = "<OlitaGetCert><dealer>V0001</dealer><cert_number>*</cert_number></OlitaGetCert>"
        'serviceName = "OLITAWS"
        'funcToProc = "GVS_UPDATE_CLAIM"
        'dataIn = "<OlitaGetCert><dealer>V0001</dealer><cert_number>*</cert_number></OlitaGetCert>"
        'serviceName = "OLITAWS"
        'funcToProc = "GetSalutations"
        'dataIn = "<GetSalutationsDs><GetSalutations></GetSalutations></GetSalutationsDs>"
        funcToProc = "GetMembershipTypes"
        dataIn = "<GetMembershipTypesDs><GetMembershipTypes></GetMembershipTypes></GetMembershipTypesDs>"
        serviceName = "OLITAWS"
        Try
            result = wcfTestServer.ProcessRequest(elitaToken, funcToProc, dataIn)
            statusLabel.Text = result
            statusLabel.Text = statusLabel.Text & " From " & wcfTestServer.Endpoint.ListenUri.AbsoluteUri
            wcfTestServer.Close()
        Catch ex As Exception
            statusLabel.Text = "Hello Error, please try again" & Environment.NewLine & ex.Message

        End Try

    End Sub

#End Region

#End Region

#Region "Soap Header"

    Private Sub ConfigureLoginHeader(wcfTestServer As TestWcfRef.WcfTestClient)
        Dim complexUsername As String
        ' Dim passwordEquivalent As String
        Dim group As String = "InternalUsers"

        'complexUsername = "oltapr@oltapr@Services"
        'passwordEquivalent = "oltapr"
        complexUsername = userTextBox.Text & "@" & userTextBox.Text & "@" & group
        wcfTestServer.ClientCredentials.UserName.UserName = complexUsername
        wcfTestServer.ClientCredentials.UserName.Password = passwordTextBox.Text
    End Sub

    Private Sub ConfigureProcHeader(wcfTestServer As TestWcfRef.WcfTestClient, _
                                        elitaToken As String)

        wcfTestServer.ClientCredentials.UserName.UserName = "Token"
        wcfTestServer.ClientCredentials.UserName.Password = elitaToken
    End Sub

#End Region

#Region "Process Request"

#End Region

#Region "Certificate"

    Public Sub InstallValidateCert()
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                New System.Net.Security.RemoteCertificateValidationCallback( _
                                    AddressOf WcfTestForm.validateCertificate)
    End Sub

    Public Shared Function validateCertificate(sender As Object, _
        cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
        chain As System.Security.Cryptography.X509Certificates.X509Chain, _
        oError As System.Net.Security.SslPolicyErrors) As Boolean

        Dim isValid As Boolean = False
        If (oError = Net.Security.SslPolicyErrors.None) Then
            isValid = True
        ElseIf (oError = Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch) Then
            ' The Certificate is Trusted
            If (cert.Subject = CERTIFICATENAME) Then
                isValid = True
            End If
        End If
        Return isValid

    End Function

#End Region

End Class