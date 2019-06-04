Imports System.ServiceModel

Public Class GVSService

#Region "Constants"
    Private Const LOGIN_OK As String = "Ok"
    Private Const PROCESS_REQUEST_ERROR As String = "ERROR"

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
        bind.TextEncoding = Text.Encoding.UTF8
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

    Private Shared Function Get_EndPoint(ByVal url As String) As EndpointAddress
        Dim eab As EndpointAddressBuilder

        eab = New EndpointAddressBuilder
        eab.Uri = New Uri(url)

        Return eab.ToEndpointAddress
    End Function

    Private Shared Function Get_ServiceClient(ByVal url As String) As GvsWSRef.WSElitaServiceOrderSoapClient
        Dim bind As BasicHttpBinding
        Dim ea As EndpointAddress
        Dim sc As GvsWSRef.WSElitaServiceOrderSoapClient

        bind = Bind_WSElitaServiceOrderSoap()
        ea = Get_EndPoint(url)
        sc = New GvsWSRef.WSElitaServiceOrderSoapClient(bind, ea)
        sc.Endpoint.Name = "WSElitaServiceOrderSoap"

        Return sc
    End Function

    Public Shared Function SendToGvs(ByVal xmlIn As String, ByVal functionToProcess As String) As String
        Dim wsGvs As GvsWSRef.WSElitaServiceOrderSoapClient
        Dim gvsToken As String
        Dim sLoginMsg As String
        Dim xmlOut As String
        Dim oWebPasswd As WebPasswd
        Dim errMsg As String
        Dim userNetworkId As String = String.Empty
        Dim tempTransId As Guid = Guid.Empty
        Dim url As String

        Try
            tempTransId = New Guid(GuidControl.HexToByteArray(xmlIn))
        Catch ex As Exception
            ' if fail, the xml is not a guid but the entire transaction
        End Try

        Try
            If Not tempTransId.Equals(Guid.Empty) Then
                Dim transObj As TransactionLogHeader = New TransactionLogHeader(tempTransId)
                xmlIn = transObj.TransactionXml
                If Not xmlIn Is Nothing Then
                    xmlIn = xmlIn.Replace("<?xml version='1.0' encoding='utf-8' ?>", "")
                End If
            End If

            'AppConfig.Debug("SendToGvs Env:" & AppConfig.CurrentEnvironment & " Hub:" & ElitaPlusIdentity.Current.ConnectionType & " CompGroupID:" & MiscUtil.GetDbStringFromGuid(Authentication.CompanyGroupId))

            oWebPasswd = New WebPasswd(Authentication.CompanyGroupId, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SERVICE_NETWORK_GVS), True)
            url = oWebPasswd.Url
            wsGvs = Get_ServiceClient(url)
            gvsToken = wsGvs.Login(oWebPasswd.UserId, oWebPasswd.Password, sLoginMsg)

            
            If gvsToken Is Nothing Then
                errMsg = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED)
                If Not (ElitaPlusIdentity.Current Is Nothing Or ElitaPlusIdentity.Current.ActiveUser Is Nothing Or ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                    userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                xmlOut = XMLHelper.FromErrorCodeToXML(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED, errMsg, userNetworkId)
                wsGvs.Close()
            Else
                If Trim(sLoginMsg) = LOGIN_OK Then
                    xmlOut = wsGvs.ProcessRequest(gvsToken, functionToProcess, xmlIn)
                    wsGvs.Close()
                    If (xmlOut.ToUpper).Contains(PROCESS_REQUEST_ERROR) Then
                        AppConfig.Log(xmlOut)
                    End If
                Else
                    wsGvs.Close()
                    'xmlOut = Common.ErrorCodes.WS_ACCESS_DENIED
                    If Not (ElitaPlusIdentity.Current Is Nothing Or ElitaPlusIdentity.Current.ActiveUser Is Nothing Or ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                        userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    End If
                    sLoginMsg = If(sLoginMsg Is Nothing, String.Empty, sLoginMsg)
                    xmlOut = If((sLoginMsg.Contains("</") Or sLoginMsg.Contains("/>")), sLoginMsg, XMLHelper.FromErrorCodeToXML(sLoginMsg, sLoginMsg, userNetworkId))
                End If
            End If

            Return xmlOut

        Catch ex As Exception
            'xmlOut = Common.ErrorCodes.WS_ACCESS_DENIED
            AppConfig.Debug("SendToGvs exception:" & ex.StackTrace)
            Dim iex As Exception
            iex = ex
            Do While (Not iex.InnerException Is Nothing)
                iex = ex.InnerException
                AppConfig.Debug("SendToGvs exception:" & iex.StackTrace)
            Loop
            Throw ex
        End Try

    End Function

End Class
