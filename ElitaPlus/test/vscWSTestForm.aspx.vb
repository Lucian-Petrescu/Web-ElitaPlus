Imports Microsoft.Web.Services3.Security
Imports Microsoft.Web.Services3.Security.Tokens
Imports System.Web.Services.Protocols

'Option Strict Off
Partial Class vscWSTestForm
    ' Inherits System.Web.UI.Page
    Inherits ElitaPlusPage

#Region "Constants"

    Private Const VSC_TEST_SESSION As String = "VSC_TEST_SESSION"

#End Region

#Region "Variables"


    Class MyState
        Public elitaToken As String
    End Class

    Private moState As MyState

#End Region

#Region "State"

    Private Sub SetState(ByVal elitaToken As String)
        moState = New MyState
        moState.elitaToken = elitaToken
        Session(VSC_TEST_SESSION) = moState
    End Sub

    Private Sub GetState()
        moState = CType(Session(VSC_TEST_SESSION), MyState)
    End Sub

#End Region

#Region "Handlers"


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

#Region "Handlers-Init"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

#End Region

#Region "Handler-Buttons"

    Private Sub HelloBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelloBtn.Click
        Dim helloWS As New MyTestWSRef.MyTestWse
        Dim result As String
        CreateQuoteDs()
        Try
            result = helloWS.HelloWorld
            LabelError.Text = result
        Catch ex As Exception
            LabelError.Text = "Hello Error, please try again" & Environment.NewLine & ex.Message
        End Try
        LabelError.Text = LabelError.Text & " From " & helloWS.Url

    End Sub

    'Private Sub HelloBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelloBtn.Click
    '    Dim helloWS As New VscWSRef.VscWSWse
    '    Dim result As String

    '    Try
    '        result = helloWS.HelloWorld
    '        LabelError.Text = result
    '    Catch ex As Exception
    '        LabelError.Text = "Hello Error, please try again"
    '    End Try


    'End Sub

    ' VSC
    'Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
    '    Dim vscWebServer As New VscWSRef.VscWSWse
    '    Dim elitaToken As String

    '    LabelError.Text = String.Empty
    '    Try
    '        ConfigureLoginWS(vscWebServer)
    '        elitaToken = vscWebServer.Login
    '        ClearLogin(vscWebServer)
    '        ConfigureLoginWS(vscWebServer)
    '        elitaToken = vscWebServer.Login
    '        '  ClearLogin(olitaWebServer)
    '        SetState(elitaToken)

    '        LabelError.Text = "Select your request"
    '        LabelError.ForeColor = Color.Navy

    '    Catch ex As Exception

    '        LabelError.Text = "Login Error, please try again" & Environment.NewLine & ex.Message

    '    End Try
    '    '    LabelError.Text = LabelError.Text & " From " & vscWebServer.Url
    '    LabelError.Text = LabelError.Text & " From " & vscWebServer.Url

    'End Sub

    ' Olita
    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Dim olitaWebServer As New OlitaWSRef.OlitaWSWse
        Dim elitaToken As String

        LabelError.Text = String.Empty
        Try

            ConfigureOlitaLoginWS(olitaWebServer)
            elitaToken = olitaWebServer.Login
            ClearOlitaLogin(olitaWebServer)
            SetState(elitaToken)

            LabelError.Text = "Select your request"
            LabelError.ForeColor = Color.Navy

        Catch ex As Exception

            LabelError.Text = "Login Error, please try again" & Environment.NewLine & ex.Message

        End Try
        LabelError.Text = LabelError.Text & " From " & olitaWebServer.Url

    End Sub

    Private Sub LoginBodyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginBodyButton.Click
        Dim vscWebServer As New VscWSRef.VscWSWse
        Dim elitaToken As String
        '  Dim group As String = "Services"
        Dim group As String = "InternalUsers"

        Try
            elitaToken = vscWebServer.LoginBody(userTextBox.Text, passwordTextBox.Text, Group)
            SetState(elitaToken)
            LabelError.Text = "Select your request"
            LabelError.ForeColor = Color.Navy
        Catch ex As Exception
            LabelError.Text = "Login Error, please try again" & Environment.NewLine & ex.Message
        End Try
        LabelError.Text = LabelError.Text & " From " & vscWebServer.Url
    End Sub

    'Vsc
    'Private Sub GetVscButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetVscButton.Click
    '    Dim vscWebServer As New VscWSRef.VscWSWse
    '    '  Dim elitaAuth As New VscWSRef.WSAuthenticationData
    '    Dim result As String

    '    Try
    '        GetState()
    '        '      vscWebServer.Timeout = 1000 * 60 * 60
    '        result = vscWebServer.ProcessRequest(moState.elitaToken, "GetQuote", _
    '                "<VSCQuote><Make>FORD</Make><Model>TAURUS</Model><VIN>1234567890</VIN><Year>1987</Year><Mileage>246544</Mileage><New_Used>USED</New_Used><In_Service_Date>2007-01-01</In_Service_Date><Dealer_Code>12345</Dealer_Code></VSCQuote>")
    '        LabelError.Text = result
    '    Catch ex As SoapException

    '        LabelError.Text = ex.Message
    '        LabelError.ForeColor = Color.Red

    '    End Try

    'End Sub

    'Olita
    Private Sub GetVscButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetVscButton.Click
        Dim olitaWebServer As New OlitaWSRef.OlitaWSWse
        '  Dim elitaAuth As New VscWSRef.WSAuthenticationData
        Dim result As String

        Try
            GetState()
            '      vscWebServer.Timeout = 1000 * 60 * 60
            result = olitaWebServer.ProcessRequest(moState.elitaToken, "GETREGIONS", _
                    "<OlitaGetRegions><country_code>PR</country_code></OlitaGetRegions>")
            LabelError.Text = result
        Catch ex As SoapException

            LabelError.Text = ex.Message
            LabelError.ForeColor = Color.Red

        End Try

    End Sub

#End Region

#End Region

#Region "Login"


    Private Sub ClearLogin(ByVal vscWebServer As VscWSRef.VscWSWse)
        vscWebServer.RequestSoapContext.Security.Elements.Remove(vscWebServer.RequestSoapContext.Security.Elements.Item(0))
        vscWebServer.RequestSoapContext.Security.Tokens.Clear()
    End Sub

    Private Sub ClearOlitaLogin(ByVal olitaWebServer As OlitaWSRef.OlitaWSWse)
        olitaWebServer.RequestSoapContext.Security.Elements.Remove(olitaWebServer.RequestSoapContext.Security.Elements.Item(0))
        olitaWebServer.RequestSoapContext.Security.Tokens.Clear()
    End Sub

    ' <summary>
    ' Looks for any configuration settings that may modify
    ' the Web service proxy used and modifies the proxy accordingly.
    ' </summary>
    Protected Sub ConfigureProxy(ByVal protocol As HttpWebClientProtocol)
        Dim remoteHost As String = ConfigurationSettings.AppSettings("remoteHost")
        If Not (remoteHost Is Nothing) Then
            Dim remoteHostUri As New Uri(remoteHost)
            Dim protocolUrl As New Uri(protocol.Url)
            Dim newUri As New Uri(remoteHostUri, protocolUrl.AbsolutePath)

            If TypeOf protocol Is Microsoft.Web.Services3.WebServicesClientProtocol Then
                CType(protocol, Microsoft.Web.Services3.WebServicesClientProtocol).Url = newUri.AbsoluteUri
            Else
                protocol.Url = newUri.AbsoluteUri
            End If
        End If
    End Sub 'ConfigureProxy

    Private Sub ConfigureLoginWS(ByVal vscWebServer As VscWSRef.VscWSWse)
        Dim token As UsernameToken
        Dim complexUsername As String
        Dim passwordEquivalent As String

        complexUsername = Authentication.GetWSComplexUserName

        passwordEquivalent = AppConfig.WebService.Password

        token = New UsernameToken(complexUsername, passwordEquivalent, PasswordOption.SendPlainText)

        ' Configure the proxy
        ConfigureProxy(vscWebServer)
        ' Adds the X.509 certificate to the WS-Security header.
        vscWebServer.RequestSoapContext.Security.Tokens.Add(token)
        ' Signs the SOAP message using the X.509 certifcate.
        vscWebServer.RequestSoapContext.Security.Elements.Add(New MessageSignature(token))
        ' Set the TTL to one minute to prevent reply attacks
        '  vscWebServer.RequestSoapContext.Security.Timestamp.TtlInSeconds = 60
        '  vscWebServer.RequestSoapContext.Security.Timestamp.TtlInSeconds = 600

        ' Call the service
        ' Console.WriteLine("Calling {0}", vscWebServer.Url)
    End Sub

   

    Private Sub ConfigureOlitaLoginWS(ByVal olitaWebServer As OlitaWSRef.OlitaWSWse)
        Dim token As UsernameToken
        Dim complexUsername As String
        Dim passwordEquivalent As String

        '  complexUsername = Authentication.GetWSComplexUserName
        complexUsername = "oltapr@oltapr@Services"
        ' passwordEquivalent = AppConfig.WebService.Password
        passwordEquivalent = "oltapr"
        token = New UsernameToken(complexUsername, passwordEquivalent, PasswordOption.SendPlainText)

        ' Configure the proxy
        ' ConfigureProxy(vscWebServer)
        ' Adds the X.509 certificate to the WS-Security header.
        olitaWebServer.RequestSoapContext.Security.Tokens.Add(token)
        ' Signs the SOAP message using the X.509 certifcate.
        olitaWebServer.RequestSoapContext.Security.Elements.Add(New MessageSignature(token))
        ' Set the TTL to one minute to prevent reply attacks
        '  vscWebServer.RequestSoapContext.Security.Timestamp.TtlInSeconds = 60
        '  vscWebServer.RequestSoapContext.Security.Timestamp.TtlInSeconds = 600

        ' Call the service
        ' Console.WriteLine("Calling {0}", vscWebServer.Url)
    End Sub


#End Region

#Region "Business"

    Public Sub CreateQuoteDs()
        Dim ds As New VSCQuoteDs
        Dim dt As VSCQuoteDs.VSCQuoteDataTable
        Dim dr As VSCQuoteDs.VSCQuoteRow

        dt = CType(ds.Tables(0), VSCQuoteDs.VSCQuoteDataTable)
        dr = dt.NewVSCQuoteRow

        dr.Dealer_Code = "BAZA"
        dr.Engine_Version = "2.3"
        dr.In_Service_Date = System.DateTime.Now
        dr.Make = "Toyota"
        dr.Mileage = 56
        dr.Model = "Corolla"
        dr.New_Used = "New"
        dr.Vehicle_License_Tag = "CR23"
        dr.VIN = "dd"
        dr.Year = 2007

        dt.AddVSCQuoteRow(dr)

    End Sub

#End Region


    'abdullah

End Class
