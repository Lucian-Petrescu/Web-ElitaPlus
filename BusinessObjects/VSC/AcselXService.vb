
Imports System.ServiceModel
Imports Assurant.Common
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class AcselXService

#Region "Constants"
    Private Const LOGIN_OK As String = "Ok"
    Private Const PROCESS_REQUEST_ERROR As String = "ERROR"
    Private Const PROCESS_REQUEST_ERROR_CODE As String = "1"
    Private Const PROCESS_REQUEST_DEFAULT_VALUE As String = "0"

#End Region

    Private Shared Function Bind_WSElitaServiceOrderSoap() As BasicHttpBinding
        Dim bind As New BasicHttpBinding()

        bind.Name = "WSElitaServiceOrderSoap"
        bind.CloseTimeout = TimeSpan.Parse("00:01:00")
        bind.OpenTimeout = TimeSpan.Parse("00:01:00")
        bind.ReceiveTimeout = TimeSpan.Parse("00:10:00")
        bind.SendTimeout = TimeSpan.Parse("00:01:00")
        bind.AllowCookies = False
        bind.BypassProxyOnLocal = False
        bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        bind.MaxBufferSize = 262144
        bind.MaxBufferPoolSize = 524288
        bind.MaxReceivedMessageSize = 262144
        bind.MessageEncoding = WSMessageEncoding.Text
        bind.TextEncoding = Encoding.UTF8
        bind.TransferMode = TransferMode.Buffered
        bind.UseDefaultWebProxy = True
        ' readerQuotas
        bind.ReaderQuotas.MaxDepth = 32
        bind.ReaderQuotas.MaxStringContentLength = 262144
        bind.ReaderQuotas.MaxArrayLength = 262144
        bind.ReaderQuotas.MaxBytesPerRead = 4096
        bind.ReaderQuotas.MaxNameTableCharCount = 16384
        ' Security
        bind.Security.Mode = SecurityMode.Transport
        '   Transport
        bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
        bind.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None
        '   Message
        bind.Security.Message.AlgorithmSuite = ServiceModel.Security.SecurityAlgorithmSuite.Default

        Return bind
    End Function

    Private Shared Function Get_EndPoint(url As String) As EndpointAddress
        Dim eab As EndpointAddressBuilder

        eab = New EndpointAddressBuilder
        eab.Uri = New Uri(url)

        Return eab.ToEndpointAddress
    End Function

    Private Shared Function Get_ServiceClient(url As String) As AcselXWSRef.CancelamentoElita
        Dim bind As BasicHttpBinding
        Dim ea As EndpointAddress
        Dim sc As AcselXWSRef.CancelamentoElita

        bind = Bind_WSElitaServiceOrderSoap()
      
        'ea = Get_EndPoint(url)
        sc = New AcselXWSRef.CancelamentoElita() '(bind, ea)
        sc.Url = url
        sc.UseDefaultCredentials = True

        ' sc.
        '''sc.bind = "WSElitaServiceOrderSoap"

        Return sc
    End Function

    Public Shared Function SendToAcselX(xmlIn As String, functionToProcess As String) As String
        Dim wsAcselX As AcselXWSRef.CancelamentoElita

        Dim AcselXToken As String
        Dim sLoginMsg As String = LOGIN_OK
        Dim xmlOut As AcselXWSRef.retornoCancelamentoElita
        Dim oWebPasswd As WebPasswd
        Dim errMsg As String
        Dim userNetworkId As String = String.Empty
        Dim tempTransId As Guid = Guid.Empty
        Dim url As String
        Dim arrayInput As ArrayList
        Dim xmlOutput As String

        Try
            tempTransId = New Guid(GuidControl.HexToByteArray(xmlIn))
        Catch ex As Exception
            ' if fail, the xml is not a guid but the entire transaction
        End Try

        Try
            If Not tempTransId.Equals(Guid.Empty) Then
                Dim transObj As TransactionLogHeader = New TransactionLogHeader(tempTransId)
                xmlIn = transObj.TransactionXml
                If xmlIn IsNot Nothing Then
                    xmlIn = xmlIn.Replace("<?xml version='1.0' encoding='utf-8' ?>", "")
                End If
            End If

            oWebPasswd = New WebPasswd(Authentication.CompanyGroupId, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__ACSELX), True)
            url = oWebPasswd.Url
            wsAcselX = Get_ServiceClient(url)            
            AcselXToken = GenAcselXToken()

            If AcselXToken Is Nothing Then
                errMsg = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED)
                If Not (ElitaPlusIdentity.Current Is Nothing Or ElitaPlusIdentity.Current.ActiveUser Is Nothing Or ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                    userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                xmlOutput = XMLHelper.FromErrorCodeToXML(PROCESS_REQUEST_ERROR_CODE, errMsg, userNetworkId, PROCESS_REQUEST_DEFAULT_VALUE, PROCESS_REQUEST_DEFAULT_VALUE)
                wsAcselX.Dispose()
            Else
                If Trim(sLoginMsg) = LOGIN_OK Then
                    arrayInput = CreateArrayFromXMLInpout(xmlIn)                    
                    xmlOut = wsAcselX.cancelamentoElita(AcselXToken, _
                                                        arrayInput(0).ToString, _
                                                        arrayInput(1).ToString, _
                                                        arrayInput(2).ToString, _
                                                        arrayInput(3).ToString, _
                                                        arrayInput(4).ToString, _
                                                        arrayInput(5).ToString, _
                                                        arrayInput(6).ToString, _
                                                        arrayInput(7).ToString, _
                                                        arrayInput(8).ToString, _
                                                        arrayInput(9).ToString, _
                                                        arrayInput(10).ToString, _
                                                        arrayInput(11).ToString, _
                                                        arrayInput(12).ToString, _
                                                        arrayInput(13).ToString, _
                                                        arrayInput(14).ToString, _
                                                        arrayInput(15).ToString, _
                                                        arrayInput(16).ToString, _
                                                        arrayInput(17).ToString, _
                                                        arrayInput(18).ToString, _
                                                        arrayInput(19).ToString)

                    xmlOutput = GetResponseXML(xmlOut.retorno, xmlOut.descricaoErro, userNetworkId, xmlOut.valorRestituicao, xmlOut.numParcelasPagas)
                    wsAcselX.Dispose()
                    If (xmlOut.retorno <> 0) Then
                        AppConfig.Log(xmlOutput)
                    End If
                End If
            End If


            Return xmlOutput

        Catch ex As Exception
            'xmlOut = Common.ErrorCodes.WS_ACCESS_DENIED
            Throw ex
        End Try

    End Function

    Private Shared Function CreateArrayFromXMLInpout(xmlinput As String) As ArrayList
        Dim xmlDoc As XmlDocument
        Dim reader As XmlTextReader
        Dim Inputarray As New ArrayList
        Dim UpperXMLInput As String = xmlinput.ToUpper
        
        xmlDoc = New XmlDocument()
        xmlDoc.LoadXml(UpperXMLInput)
        reader = XMLHelper.GetXMLStream(xmlDoc.InnerXml)

        reader.Read()
        reader.ReadStartElement("ACSELXQUOTECANCELCERTDS")
        reader.ReadStartElement("ACSELXQUOTECANCELCERT")        
        Inputarray.Add(reader.ReadElementString("CODIGOPRODUTO"))
        Inputarray.Add(reader.ReadElementString("NUMEROAPOLICE"))
        Inputarray.Add(reader.ReadElementString("NUMEROITEMAPOLICE"))
        Inputarray.Add(reader.ReadElementString("CNPJ"))
        Inputarray.Add(reader.ReadElementString("CODIGOCLIENTE"))
        Inputarray.Add(reader.ReadElementString("TIPOIDENTIFICACAOCLIENTE"))
        Inputarray.Add(reader.ReadElementString("NUMEROIDENTIFICACAOCLIENTE"))
        Inputarray.Add(reader.ReadElementString("TIPOCANCELAMENTO"))
        Inputarray.Add(reader.ReadElementString("DATACANCELAMENTO"))
        Inputarray.Add(reader.ReadElementString("CODIGOCANCELAMENTO"))
        Inputarray.Add(reader.ReadElementString("TIPOPAGAMENTO"))
        Inputarray.Add(reader.ReadElementString("CODIGOBANCO"))
        Inputarray.Add(reader.ReadElementString("NUMEROAGENCIA"))
        Inputarray.Add(reader.ReadElementString("DVAGENCIA"))
        Inputarray.Add(reader.ReadElementString("NUMEROCONTACORRENTE"))
        Inputarray.Add(reader.ReadElementString("DVCONTACORRENTE"))
        Inputarray.Add(reader.ReadElementString("NUMEROCONTRATOCOBRANCA"))
        Inputarray.Add(reader.ReadElementString("CANALCOBRANCA"))
        Inputarray.Add(reader.ReadElementString("VALORRESTITUICAO"))
        Inputarray.Add(reader.ReadElementString("ACAO"))
        reader.ReadEndElement()
        reader.ReadEndElement()
        reader.Close()

        Return Inputarray

    End Function

    Private Shared Function GetResponseXML(strcode As String, strMessage As String, _
                                    struserNetworkID As String, strRefundAmt As String, _
                                    strInstallmentsPaid As String) As String

        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement

        Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)

        objRoot = objDoc.CreateElement("AcselXQuoteCancelResult")
        objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
        objDoc.AppendChild(objRoot)

        objE = objDoc.CreateElement("Code")
        objRoot.AppendChild(objE)
        objE.InnerText = strcode

        objE = objDoc.CreateElement("Message")
        objRoot.AppendChild(objE)
        objE.InnerText = strMessage

        objE = objDoc.CreateElement("Refund_Amt")
        objRoot.AppendChild(objE)
        objE.InnerText = strRefundAmt

        objE = objDoc.CreateElement("Installments_Paid")
        objRoot.AppendChild(objE)
        objE.InnerText = strInstallmentsPaid

        Return objDoc.OuterXml


    End Function

    Private Shared Function GenAcselXToken() As String
        Dim strChave, p_strString, p_strString2, strOriginal, strEncripto, strRetorno, intPosCar As String
        Dim retChar As Char

        Dim timeZoneInfo As TimeZoneInfo
        Dim CurrdateTime As DateTime
        'Set the time zone information to US Mountain Standard Time 
        timeZoneInfo = timeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time")
        'Get date and time in US Mountain Standard Time 
        CurrdateTime = timeZoneInfo.ConvertTime(dateTime.Now, timeZoneInfo)
        'Print out the date and timeConsole.WriteLine(dateTime__2.ToString("yyyy-MM-dd HH-mm-ss"))

        'p_strString = "AssurantBrasil" & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day
        p_strString = "AssurantBrasil" & CurrdateTime.Now.Year & CurrdateTime.Now.Month & CurrdateTime.Now.Day

        ' CStr(Year(Now)) & CStr(Month(Now)) & CStr(Day(Now))

        strOriginal = " ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÃÕÑÂÊÎÔÛÄËÏÖÜÀÈÌÒÙabcdefghijklmnopqrstuvwxyz0123456789áéíóúãõñâêîôûäëïöüàèìòùÇç/"
        strEncripto = "89áéíxyz01ìòùÇç67óúãÒÙabcvw23lmJKLM ABCD45õñâpqrstuêîÜÀÈÌôûäëïöüÃÕÑàèÏÖdeÉÍÔÛÄYZÁÓÚÂfgOPQ/XÊÎËINSTVWhijkGHUnEFoR"
        strRetorno = ""
        For t = 1 To Len(p_strString)
            intPosCar = InStr(1, strOriginal, Mid(p_strString, t, 1))
            If intPosCar > 0 Then
                retChar = Mid(strEncripto, intPosCar, 1)
            Else
                retChar = Mid(p_strString, t, 1)
            End If
            strRetorno = strRetorno & ConvertChartoAsc(retChar, DateTime.Now.Day)
        Next
        Return strRetorno
    End Function

    Private Shared Function ConvertChartoAsc(x As String, inc As Integer)
        Dim vlrAsc As String = ""
        Dim i As Integer = 0
        For i = 0 To x.Length
            vlrAsc = vlrAsc + Math.Abs(Asc(x(i)) + inc).ToString
            i = i + 1
        Next

        Return vlrAsc
    End Function

End Class


