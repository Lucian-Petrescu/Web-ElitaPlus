Imports System.Net
Imports System.ServiceModel

Public Class GVSService

#Region "Constants"
    Private Const LOGIN_OK As String = "Ok"
    Private Const PROCESS_REQUEST_ERROR As String = "ERROR"
    Private Const LOGIN_FAILED As String = "FALHA NO LOGIN : USUARIO OU SENHA INVALIDOS"

#End Region

    Public Shared Function SendToGvs(xmlIn As String, functionToProcess As String) As String
        Dim wsGvs As WebDeClaimsServiceReference.WSElitaServiceOrder
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
                If xmlIn IsNot Nothing Then
                    xmlIn = xmlIn.Replace("<?xml version='1.0' encoding='utf-8' ?>", "")
                End If
            End If

            'AppConfig.Debug("SendToGvs Env:" & AppConfig.CurrentEnvironment & " Hub:" & ElitaPlusIdentity.Current.ConnectionType & " CompGroupID:" & MiscUtil.GetDbStringFromGuid(Authentication.CompanyGroupId))

            oWebPasswd = New WebPasswd(Authentication.CompanyGroupId, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SERVICE_NETWORK_GVS), True)
            url = oWebPasswd.Url
            wsGvs = New WebDeClaimsServiceReference.WSElitaServiceOrder()
            wsGvs.Url = url
            Try
                gvsToken = wsGvs.Login(oWebPasswd.UserId, oWebPasswd.Password)
            Catch ex As Exception
                AppConfig.Debug("SendToGvs exception: Error while calling webservice : " & url & "Login Method : Exception Message : " & ex.Message & " | Stack Trace : " & ex.StackTrace)
                Throw
            End Try


            If gvsToken Is Nothing Then
                errMsg = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED)
                If Not (ElitaPlusIdentity.Current Is Nothing OrElse ElitaPlusIdentity.Current.ActiveUser Is Nothing OrElse ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                    userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                xmlOut = XMLHelper.FromErrorCodeToXML(Common.ErrorCodes.WS_ERR_NO_TOKEN_RETURNED, errMsg, userNetworkId)
            ElseIf gvsToken.ToUpperInvariant = LOGIN_FAILED Then
                errMsg = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ACCESS_DENIED)

                If Not (ElitaPlusIdentity.Current Is Nothing OrElse ElitaPlusIdentity.Current.ActiveUser Is Nothing OrElse ElitaPlusIdentity.Current.ActiveUser.NetworkId Is Nothing) Then
                    userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                xmlOut = XMLHelper.FromErrorCodeToXML(Common.ErrorCodes.WS_ACCESS_DENIED, errMsg, userNetworkId)
            Else
                Try
                    xmlOut = wsGvs.ProcessRequest(gvsToken, functionToProcess, xmlIn)
                Catch ex As Exception
                    AppConfig.Debug(xmlIn)
                    AppConfig.Debug("SendToGvs exception: Error while calling webservice : " & url & "ProcessRequest Method : ProcessRequest Exception Message : " & ex.Message & " | Stack Trace : " & ex.StackTrace)
                    Throw
                End Try

                If (xmlOut.ToUpper).Contains(PROCESS_REQUEST_ERROR) Then
                    AppConfig.Log(xmlOut)
                End If
            End If
            Return xmlOut

        Catch ex As Exception
            'xmlOut = Common.ErrorCodes.WS_ACCESS_DENIED
            AppConfig.Debug("SendToGvs exception:" & ex.StackTrace)
            Dim iex As Exception
            iex = ex
            Do While (iex.InnerException IsNot Nothing)
                iex = iex.InnerException
                AppConfig.Debug("SendToGvs exception:" & iex.StackTrace)
            Loop
            Throw ex
        Finally
            wsGvs.Dispose()
        End Try

    End Function

End Class
