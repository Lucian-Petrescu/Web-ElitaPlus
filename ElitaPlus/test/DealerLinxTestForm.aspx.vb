Imports System.Xml
Imports System.IO
Imports Microsoft.Web.Services3.Security
Imports Microsoft.Web.Services3.Security.Tokens
Imports System.Web.Services.Protocols
Partial Class DealerLinxTestForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents xmlDataOut As System.Web.UI.WebControls.TextBox
    Protected WithEvents xmlUpdateOut As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtCountryCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCertNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorCtrl As ErrorController
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "const"
    Private Const MANUFACTURER_DESCRIPTION_COL_NAME As String = "CODE"
    Private Const VSC_TEST_SESSION As String = "VSC_TEST_SESSION"
#End Region
#Region "Variables"


    Class MyState
        Public oToken As String
    End Class

    Private moState As MyState

#End Region

#Region "State"

    Private Sub SetState(ByVal oToken As String)
        moState = New MyState
        moState.oToken = oToken
        Session(VSC_TEST_SESSION) = moState
    End Sub

    Private Sub GetState()
        moState = CType(Session(VSC_TEST_SESSION), MyState)
    End Sub

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ErrorCtrl.Clear_Hide()
        If Not IsPostBack Then
            'OptainToken()
            'PopulateMakeDropDown()
            Me.AddCalendar(Me.ImageButtonISDate, Me.txtISD)
        End If


    End Sub

    Private Sub OptainToken()
        Dim vscWS As New VscWSRef.VscWSWse

        Dim oToken As String

        'lblError.Text = String.Empty

        Try
            Dim token As UsernameToken
            Dim user As String = "os08rp"
            Dim pw As String = ""
            Dim passwordEquivalent As String = pw
            token = New UsernameToken(user, passwordEquivalent, PasswordOption.SendPlainText)
            Dim remoteHost As String = ConfigurationSettings.AppSettings("remoteHost")
            If Not (remoteHost Is Nothing) Then
                Dim remoteHostUri As New Uri(remoteHost)
                Dim protocolUrl As New Uri(vscWS.Url)
                Dim newUri As New Uri(remoteHostUri, protocolUrl.AbsolutePath)

                If TypeOf vscWS Is Microsoft.Web.Services3.WebServicesClientProtocol Then
                    CType(vscWS, Microsoft.Web.Services3.WebServicesClientProtocol).Url = newUri.AbsoluteUri
                Else
                    vscWS.Url = newUri.AbsoluteUri
                End If
            End If

            vscWS.RequestSoapContext.Security.Tokens.Add(token)
            vscWS.RequestSoapContext.Security.Elements.Add(New MessageSignature(token))

            oToken = vscWS.Login
            vscWS.RequestSoapContext.Security.Elements.Remove(vscWS.RequestSoapContext.Security.Elements.Item(0))
            vscWS.RequestSoapContext.Security.Tokens.Clear()

            SetState(oToken)


        Catch ex As Exception

            'lblError.Text = "Login Error: " & Environment.NewLine & ex.Message

        End Try
        'lblError.Text = lblError.Text & " From " & vscWS.Url

    End Sub
    Private Sub PopulateMakeDropDown()
        'Dim dv As DataView = LookupListNew.GetManufacturerLookupList(Authentication.CurrentUser.CompanyGroup.Id)
        'Me.BindListControlToDataView(Me.MakeDrop, dv, MANUFACTURER_DESCRIPTION_COL_NAME, , True)


    End Sub

    Private Sub btnGetQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetQuote.Click
        Try

            'Dim ds As New VSCQuoteDs
            'Dim dt As VSCQuoteDs.VSCQuoteDataTable
            'Dim dr As VSCQuoteDs.VSCQuoteRow

            'dt = CType(ds.Tables(0), VSCQuoteDs.VSCQuoteDataTable)
            'dr = dt.NewVSCQuoteRow

            'dr.Make = "Honda"
            'dr.Model = "Accord"
            'dr.EngineVersion = "Accord EX 2Dr Manual"
            'dr.VIN = ""
            'dr.Year = 2007
            'dr.Mileage = 12
            'dr.NewUsed = "new"
            'dr.InServiceDate = New Date(2007, 4, 13)
            'dr.DealerCode = "V001"
            'dr.VehicleLicenseTag = "2345678"


            'dt.AddVSCQuoteRow(dr)


            Dim oQuoteEngineData As New QuoteEngineData1
            oQuoteEngineData.QEData.CompanyGroupID = Authentication.CurrentUser.CompanyGroup.Id 'ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            'oQuoteEngineData.QEData.Manufacturer = "TOYOTA"
            'oQuoteEngineData.QEData.Model = "COROLLA"
            'oQuoteEngineData.QEData.EngineVersion = "FIELDER S 1.8 16V AUT. "
            'oQuoteEngineData.QEData.VIN = ""
            'oQuoteEngineData.QEData.Year = 2007
            'oQuoteEngineData.QEData.Odometer = 12
            'oQuoteEngineData.QEData.NewUsed = "new"
            'oQuoteEngineData.QEData.InServiceDate = New Date(2006, 4, 20)
            'oQuoteEngineData.QEData.Dealer = "V001"
            'oQuoteEngineData.QEData.VehicleLicenseTag = "2345678"

            oQuoteEngineData.QEData.Manufacturer = Me.txtMake.Text
            oQuoteEngineData.QEData.Model = Me.txtModel.Text
            oQuoteEngineData.QEData.EngineVersion = Me.txtEV.Text
            oQuoteEngineData.QEData.VIN = Me.txtVIN.Text
            oQuoteEngineData.QEData.Year = CType(Me.txtYear.Text, Integer)
            oQuoteEngineData.QEData.Odometer = CType(Me.txtKM.Text, Integer)
            If Me.rdbNew.Checked Then
                oQuoteEngineData.QEData.NewUsed = "new"
            Else
                oQuoteEngineData.QEData.NewUsed = "used"
            End If
            oQuoteEngineData.QEData.InServiceDate = CType(Me.txtISD.Text, Date)
            oQuoteEngineData.QEData.Dealer = Me.txtDealerCode.Text
            oQuoteEngineData.QEData.VehicleLicenseTag = Me.txtTag.Text



            Dim ratesDS As DataSet = VSCQuote.GetQuote(oQuoteEngineData.QEData)

            Dim grdT2 As New System.Web.UI.WebControls.DataGrid
            Dim grdT1 As New System.Web.UI.WebControls.DataGrid

            grdT1.DataSource = ratesDS.Tables(0)
            grdT1.DataBind()

            grdT2.DataSource = ratesDS.Tables(1)
            grdT2.DataBind()
            'Panel1.Controls.Add(grdT1)
            'Panel1.Controls.Add(grdT2)

            scroller1.Controls.Add(grdT1)
            scroller1.Controls.Add(grdT2)
        Catch ex As Exception
            Dim x As Integer = 1
            'lblError.Text = ex.Message & " :"
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnroll.Click
        Try
            Dim olitaWebServer As New OlitaWSRef.OlitaWSWse
            Dim xmlOut As String
            Dim xmlAuthentication As String = "<root>" & _
                                                "<userid>os08rp</userid>" & _
                                        "<password>l234</password>" & _
                                        "</root>"
            Dim xmlString As String = "<root>" & _
                                            "<dealer>SUNC</dealer>" & _
                                            "<cert_number>78722001969624</cert_number>" & _
                                            "<customer_name>Abdullah by WS</customer_name>" & _
                                            "<address1>1111 NW OlitaWS</address1>" & _
                                            "<address2>STE 211</address2>" & _
                                            "<city>Miami</city>" & _
                                            "<short_desc>PR</short_desc>" & _
                                            "<code>PR</code>" & _
                                            "<postal_code>1236</postal_code>" & _
                                            "<home_phone>333-555-2214</home_phone>" & _
                                            "<email>aa@aa.com</email>" & _
                                            "<password>123456</password>" & _
                                        "</root>"




            'xmlOut = CType(olitaWebServer.ProcessRequest("getCertWithDealerCertnumber", "XXXX", xmlIn), System.Xml.XmlNode)
            xmlOut = CType(olitaWebServer.ProcessRequest("UPDATECONSUMERINFO", xmlAuthentication, xmlString), String)
            'xmlOut = CType(olitaWebServer.ProcessRequest("GETCERTUSINGTRANNO", xmlAuthentication, xmlString), XmlDataDocument)
            xmlUpdateOut.Text = xmlOut


        Catch ex As Exception
            Dim x As Integer = 1
        End Try
    End Sub

#Region "old code"
    Private Function loadCertXMLFile() As XmlDocument
        Dim stream As StringReader
        Dim reader As XmlTextReader = Nothing

        Try
            stream = New StringReader("<?xml version='1.0'?>" & _
                                        "<root>" & _
                                            "<dealer>CRMZ</dealer>" & _
                                            "<cert_number>A239456TNDXFER9999</cert_number>" & _
                                        "</root>")

            ' Load the XmlTextReader from the stream
            reader = New XmlTextReader(stream)

            Dim myXmlDocument As XmlDocument = New XmlDocument
            myXmlDocument.Load(reader)
            Return myXmlDocument

        Catch ex As Exception
            Return Nothing
        Finally

            If Not reader Is Nothing Then
                reader.Close()
            End If
        End Try

    End Function

    Private Function loadRegionXMLFile() As XmlDocument
        Dim stream As StringReader
        Dim reader As XmlTextReader = Nothing

        Try
            stream = New StringReader("<?xml version='1.0'?>" & _
                                        "<root>" & _
                                            "<country>BR</country>" & _
                                        "</root>")

            ' Load the XmlTextReader from the stream
            reader = New XmlTextReader(stream)

            Dim myXmlDocument As XmlDocument = New XmlDocument
            myXmlDocument.Load(reader)
            Return myXmlDocument

        Catch ex As Exception
            Return Nothing
        Finally

            If Not reader Is Nothing Then
                reader.Close()
            End If
        End Try

    End Function
    Private Sub writexml(ByVal xmlOut As System.Xml.XmlNode)
        Dim objStreamWriter As New StreamWriter("C:\Testfile.xml")
        Dim xmlw As System.xml.XmlWriter

        'Pass the file path and the file name to the StreamWriter constructor.

        'Write a line of text.
        xmlOut.WriteContentTo(xmlw)
        objStreamWriter.WriteLine(xmlw)

        'Close the file.
        objStreamWriter.Close()

    End Sub

    Public Function GetFileContents(ByVal FullPath As String, _
       Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
    End Function

    'Public Sub SaveTextToFile(ByVal xmlOut As System.Xml.XmlNode)
    Public Sub SaveTextToFile(ByVal xmlOut As String)

        Dim Contents As String
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try


            objReader = New StreamWriter("C:\Testfile.xml")

            objReader.Write(xmlOut)
            Me.xmlDataOut.Text = xmlOut
            objReader.Close()

        Catch Ex As Exception


        End Try

    End Sub

#End Region


End Class
