Imports System.Xml
Imports System.IO
Partial Class OlitaWSTestForm
    Inherits ElitaPlusPage

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


    End Sub
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

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim olitaWebServer As New OlitaWSRef.OlitaWSWse
            'Dim xmlIn As XmlDocument = Me.loadXMLFile
            'Dim xmlOut As XmlDataDocument
            'Dim xmlOut As System.Xml.XmlNode
            Dim xmlOut As String
            Dim xmlAuthentication As String = "<root>" & _
                                                "<userid>os08rp</userid>" & _
                                        "<password>l234</password>" & _
                                        "</root>"
            Dim xmlString As String = "<root>" & _
                                            "<dealer>" & Me.txtDealer.Text & "</dealer>" & _
                                            "<cert_number>" & Me.txtCertNumber.Text & "</cert_number>" & _
                                        "</root>"

            'xmlOut = CType(olitaWebServer.ProcessRequest("getCertWithDealerCertnumber", "XXXX", xmlIn), System.Xml.XmlNode)
            xmlOut = CType(olitaWebServer.ProcessRequest("GETCERTUSINGTRANNO", xmlAuthentication, xmlString), String)
            '  xmlOut = CType(olitaWebServer.ProcessRequest("GETCERTUSINGTRANNO", xmlAuthentication, xmlString), XmlDataDocument)

            SaveTextToFile(xmlOut)



        Catch ex As Exception
            Dim x As Integer = 1
        End Try
    End Sub

    Private Sub btnGetRegions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRegions.Click
        'txtCountryCode
        Try
            Dim olitaWebServer As New OlitaWSRef.OlitaWSWse
            'Dim xmlIn As XmlDocument = Me.loadXMLFile
            'Dim xmlOut As XmlDataDocument
            'Dim xmlOut As System.Xml.XmlNode
            Dim xmlOut As String
            Dim xmlAuthentication As String = "<root>" & _
                                                "<userid>os08rp</userid>" & _
                                        "<password>l234</password>" & _
                                        "</root>"
            Dim xmlString As String = "<root>" & _
                                            "<country>" & Me.txtCountryCode.Text & "</country>" & _
                                        "</root>"

            'xmlOut = CType(olitaWebServer.ProcessRequest("getCertWithDealerCertnumber", "XXXX", xmlIn), System.Xml.XmlNode)
            xmlOut = CType(olitaWebServer.ProcessRequest("GETREGIONS", xmlAuthentication, xmlString), String)
            'xmlOut = CType(olitaWebServer.ProcessRequest("GETCERTUSINGTRANNO", xmlAuthentication, xmlString), XmlDataDocument)

            SaveTextToFile(xmlOut)

        Catch ex As Exception
            Dim x As Integer = 1
        End Try
    End Sub




    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
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

    Private Sub btnGetQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetQuote.Click
        Dim ds As New VSCQuoteDs

        Dim objQuoteEngine As New QuoteEngine(ds)
        objQuoteEngine.Manufacturer = "Honda"
        objQuoteEngine.Model = "Accord"
        objQuoteEngine.EngineVersion = "Accord EX 2Dr Manual"
        objQuoteEngine.VIN = ""
        objQuoteEngine.Year = 2007
        objQuoteEngine.Odometer = 12
        objQuoteEngine.Condition = "new"
        objQuoteEngine.InServiceDate = New Date(2007, 4, 12)
        objQuoteEngine.DealerCode = "V001"
        objQuoteEngine.VehicleLicenseTag = "2345678"

        Dim ratesXML As String = objQuoteEngine.ProcessWSRequest()
    End Sub
End Class
