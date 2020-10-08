Imports System.Text.RegularExpressions
Imports System.ServiceModel
Imports Assurant.Common
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class CancelCreditCardAuthVSCBrazil
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "FUNCTION_TYPE"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "TRANSACTION"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    Private Const LOGIN_OK As String = "Ok"
    Private Const PROCESS_REQUEST_ERROR As String = "ERROR"
    Private Const PROCESS_REQUEST_ERROR_CODE As String = "1"
    Private Const PROCESS_REQUEST_DEFAULT_VALUE As String = "0"
#End Region

#Region "Variables"
    Private msInputXml, msFunctionToProcess As String
#End Region

#Region "Constructors"

    Public Sub New(ds As CancelCreditCardAuthVSCBrazilDs, xml As String, _
                   functionToProcess As String)
        MyBase.New()
        InputXml = xml
        FuncToProc = functionToProcess
    End Sub

#End Region

#Region "Private Members"

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Private Property InputXml As String
        Get
            Return msInputXml
        End Get
        Set
            msInputXml = value
        End Set
    End Property

    Private Property FuncToProc As String
        Get
            Return msFunctionToProcess
        End Get
        Set
            msFunctionToProcess = value
        End Set
    End Property

#End Region

#Region "Public Members"


    Private Shared Function Bind_WSElitaServiceOrderSoap() As BasicHttpBinding
        Dim bind As New BasicHttpBinding()

        bind.Name = "BasicHttpBinding_IServicoCobrancaAssurant"
        bind.CloseTimeout = TimeSpan.Parse("00:01:00")
        bind.OpenTimeout = TimeSpan.Parse("00:01:00")
        bind.ReceiveTimeout = TimeSpan.Parse("00:10:00")
        bind.SendTimeout = TimeSpan.Parse("00:01:00")
        bind.AllowCookies = False
        bind.BypassProxyOnLocal = False
        bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        bind.MaxBufferSize = 65536
        bind.MaxBufferPoolSize = 524288
        bind.MaxReceivedMessageSize = 65536
        bind.MessageEncoding = WSMessageEncoding.Text
        bind.TextEncoding = Text.Encoding.UTF8
        bind.TransferMode = TransferMode.Buffered
        bind.UseDefaultWebProxy = True
        ' readerQuotas
        bind.ReaderQuotas.MaxDepth = 32
        bind.ReaderQuotas.MaxStringContentLength = 8192
        bind.ReaderQuotas.MaxArrayLength = 16384
        bind.ReaderQuotas.MaxBytesPerRead = 4096
        bind.ReaderQuotas.MaxNameTableCharCount = 16384
        ' Security
        bind.Security.Mode = SecurityMode.None
        '   Transport
        bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
        bind.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None
        '   Message
        bind.Security.Message.AlgorithmSuite = ServiceModel.Security.SecurityAlgorithmSuite.Default
        'bind.Security.Message.ClientCredentialType = MessageCredentialType.UserName

        Return bind
    End Function
    Private Shared Function Get_EndPoint(url As String) As EndpointAddress
        Dim eab As EndpointAddressBuilder

        eab = New EndpointAddressBuilder
        eab.Uri = New Uri(url)

        Return eab.ToEndpointAddress
    End Function

    Private Shared Function Get_ServiceClient(url As String) As VSCBrazilCreditCardAuth.ServicoCobrancaAssurantClient
        Dim bind As BasicHttpBinding
        Dim ea As EndpointAddress
        Dim sc As VSCBrazilCreditCardAuth.ServicoCobrancaAssurantClient

        bind = Bind_WSElitaServiceOrderSoap()
        ea = Get_EndPoint(url)
        sc = New VSCBrazilCreditCardAuth.ServicoCobrancaAssurantClient(bind, ea)
        sc.Endpoint.Name = "WSElitaServiceOrderSoap"

        Return sc
    End Function

    Public Overrides Function ProcessWSRequest() As String
        Dim wsCC As VSCBrazilCreditCardAuth.ServicoCobrancaAssurantClient
        Dim strRejectCode As String, strRejectMsg As String, strAuthNum As String, blnSuccess As Boolean
        Dim CCToken As String
        Dim sLoginMsg As String = LOGIN_OK
        Dim xmlOut As VSCBrazilCreditCardAuth.RetornoContratacao
        Dim oWebPasswd As WebPasswd
        Dim errMsg As String
        Dim userNetworkId As String = String.Empty
        Dim tempTransId As Guid = Guid.Empty
        Dim url As String
        Dim arrayInput As ArrayList
        Dim xmlOutput As String

        Try
            tempTransId = New Guid(GuidControl.HexToByteArray(InputXml))
        Catch ex As Exception
            ' if fail, the xml is not a guid but the entire transaction
        End Try

        Try
            If Not tempTransId.Equals(Guid.Empty) Then
                Dim transObj As TransactionLogHeader = New TransactionLogHeader(tempTransId)
                InputXml = transObj.TransactionXml
                If Not InputXml Is Nothing Then
                    InputXml = InputXml.Replace("<?xml version='1.0' encoding='utf-8' ?>", "")
                End If
            End If

            oWebPasswd = New WebPasswd(Authentication.CompanyGroupId, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__BR_CC_AUTH), True)
            url = oWebPasswd.Url
            wsCC = Get_ServiceClient(url)
            wsCC.Open()
            CCToken = oWebPasswd.AuthenticationKey 'wsAcselX..Login(oWebPasswd.UserId, oWebPasswd.Password, sLoginMsg)

            If CCToken Is Nothing Then
                errMsg = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED)
                If Not (ElitaPlusIdentity.Current Is Nothing Or ElitaPlusIdentity.Current.ActiveUser Is Nothing Or ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                    userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                xmlOutput = XMLHelper.FromErrorCodeToXML(PROCESS_REQUEST_ERROR_CODE, errMsg, userNetworkId, PROCESS_REQUEST_DEFAULT_VALUE, PROCESS_REQUEST_DEFAULT_VALUE)
                wsCC.Close()
            Else
                If Trim(sLoginMsg) = LOGIN_OK Then
                    arrayInput = CreateArrayFromXMLInpout(InputXml)
                    xmlOut = wsCC.AnularContrato(CCToken, arrayInput(0).ToString)

                    blnSuccess = xmlOut.Sucesso
                    strAuthNum = xmlOut.NumeroContratoCobranca
                    strRejectCode = xmlOut.CodigoRetorno
                    strRejectMsg = xmlOut.MensagemRetorno

                    xmlOutput = GetResponseXML(blnSuccess, strAuthNum, strRejectCode, strRejectMsg)
                    wsCC.Close()
                    If (strRejectCode <> 0) Then
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
        reader.ReadStartElement("CANCELCREDITCARDAUTHVSCBRAZILDS")
        reader.ReadStartElement("CANCELCREDITCARDAUTHVSCBRAZIL")
        Inputarray.Add(reader.ReadElementString("AUTHNUMBER"))
        Inputarray.Add(reader.ReadElementString("VALUE"))
        reader.ReadEndElement()
        reader.ReadEndElement()
        reader.Close()

        Return Inputarray


    End Function

    Private Function GetResponseXML(blnSuccess As Boolean, strAuthNum As String, _
                                   strRejectCode As String, strRejectMsg As String) As String

        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement

        Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)

        objRoot = objDoc.CreateElement("CreditCardAuthVSCBrazilResult")
        objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
        objDoc.AppendChild(objRoot)

        objE = objDoc.CreateElement("Status")
        objRoot.AppendChild(objE)
        objE.InnerText = blnSuccess.ToString


        objE = objDoc.CreateElement("Reject_code")
        objRoot.AppendChild(objE)
        objE.InnerText = strRejectCode

        objE = objDoc.CreateElement("Reject_message")
        objRoot.AppendChild(objE)
        objE.InnerText = strRejectMsg

        objE = objDoc.CreateElement("Authorization_Num")
        objRoot.AppendChild(objE)
        objE.InnerText = strAuthNum

        Return objDoc.OuterXml


    End Function
#End Region

#Region "Extended Properties"

#End Region

End Class