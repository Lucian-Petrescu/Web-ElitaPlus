Imports System.Xml
Imports System.Web.Services.WebService
Imports Microsoft.Web.Services3
Imports Microsoft.Web.Services3.Security
Imports System.ServiceModel
'Imports System.ServiceModel.Activation
Imports System.ServiceModel.Security
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Reflection
Imports System.Web.Hosting

Public Class ElitaService


#Region "Services"

    Public Shared Function Hello(name As String) As String
        Return "Hello, " & name
    End Function

    Public Shared Function ProcessRequest(isWcf As Boolean, token As String, _
                                               functionToProcess As String, _
                                               xmlStringDataIn As String, _
                                               webServiceName As String) As String
        Dim _xml, servicePath As String
        Dim params(ElitaWebServiceConstants.PARAM_COUNT_0) As Object
        Dim xmlDataIn As String

        xmlDataIn = xmlStringDataIn

        AppConfig.DebugMessage.Trace("ELITAINTERNALWS", "PROCESS REQUEST", "Wcf=" & isWcf & "_" & Now.ToString)

        ''To Test Timeout
        '    While (True)
        '    System.Threading.Thread.Sleep(10000)
        '    End While


        '    Reject any requests which are not valid SOAP requests
        If isWcf = True Then
            If OperationContext.Current.RequestContext Is Nothing Then Throw New ApplicationException("Only SOAP requests are permitted.")
            servicePath = HostingEnvironment.ApplicationPhysicalPath & ElitaWebServiceConstants.SETTING_CONFIG_NAME

        Else ' Wse
            If RequestSoapContext.Current Is Nothing Then Throw New ApplicationException("Only SOAP requests are permitted.")
            servicePath = System.Web.HttpContext.Current.Server.MapPath( _
             HttpContext.Current.Request.ApplicationPath & "/" & _
             ElitaWebServiceConstants.SETTING_CONFIG_NAME)
        End If

        Try
            ' ''DEF-637:  Deductible amount discrepancy between Galaxy and Elita
            ' ''Putting this trace to capture the Galaxy XML for the "Galaxyxxxxx" function
            'If functionToProcess.ToUpper.Contains("GALAXY") Then

            '    If xmlStringDataIn.Length > 4000 Then
            '        AppConfig.Debug("<![CDATA[" & xmlStringDataIn.Substring(0, 4000) & "]]>")
            '    Else
            '        'AppConfig.Debug(xmlStringDataIn)
            '        AppConfig.Debug("<![CDATA[" & xmlStringDataIn & "]]>")
            '        'AppConfig.DebugMessage.Trace("Galaxy", "P1", "<![CDATA[" & xmlStringDataIn & "]]>")
            '    End If

            'End If

            ElitaService.VerifyToken(isWcf, token, webServiceName, functionToProcess, xmlStringDataIn)
            _xml = XMLHelper.AddDSRoot(xmlStringDataIn)

            ' Read the process request configuration file
            Dim procInfo As XmlNode = XMLHelper.GetNodeByAttribute(XMLHelper.GetXMLDoc(servicePath),
                                                                ElitaWebServiceConstants.WEB_SERVICE_SETTING_TAG,
                                                                ElitaWebServiceConstants.SETTING_TAG,
                                                                ElitaWebServiceConstants.SETTING_TAG_ATTRIBUTE,
                                                                functionToProcess,
                                                                webServiceName)
            ' The request doesn't exists
            If procInfo Is Nothing Then Throw New ElitaWSException(ErrorCodes.WS_INVALID_REQUEST)

            Dim schemaName As String = XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_SCHEMA)
            Dim binding As String = XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_BINDING)
            Dim boClass As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." &
                                    XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_BO_CLASS)
            Dim className As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." &
                                    schemaName.Substring(schemaName.LastIndexOf("/") + 1).Replace(".xsd", "") & "Ds"

            ' Get the DS object for the schema
            Dim inputDS As Object = ObjectFromClassName(ElitaWebServiceConstants.SETTING_BO_PATH, className)

            ' Validate and load XML
            If binding Is Nothing OrElse (binding IsNot Nothing AndAlso binding.Equals(ElitaWebServiceConstants.BINDING_XML)) Then
                ValidateXML(isWcf, _xml, inputDS, schemaName)
            End If

            ' Get the class in the bo that will process the request

            params(0) = inputDS
            ' *******************************************8
            ' TO DELETE BEGIN
            'If ((Not binding Is Nothing) AndAlso (binding = ElitaWebServiceConstants.BINDING_XML)) Then
            '    ReDim params(ElitaWebServiceConstants.PARAM_COUNT_1)
            '    params(1) = _xml
            'End If
            'Dim boObj As Object = ObjectFromClassName(ElitaWebServiceConstants.SETTING_BO_PATH, boClass, params)
            'Dim xmlString As String = CType(boObj, BusinessObjectBase).ProcessWSRequest()
            'If xmlString.Equals("SEND_TO_GAP") Then
            '    xmlString = SendToGVS(xmlStringDataIn, functionToProcess)
            'End If
            '  TO DELETE END
            ' ******************************************
            ' TO KEEP BEGIN
            If binding IsNot Nothing Then
                Select Case binding
                    Case ElitaWebServiceConstants.BINDING_XML
                        ReDim params(ElitaWebServiceConstants.PARAM_COUNT_1)
                        params(1) = _xml
                    Case ElitaWebServiceConstants.BINDING_XML_FUNC
                        ReDim params(ElitaWebServiceConstants.PARAM_COUNT_2)
                        params(1) = _xml
                        params(2) = functionToProcess
                End Select
            End If

            Dim boObj As Object = ObjectFromClassName(ElitaWebServiceConstants.SETTING_BO_PATH, boClass, params)
            Dim xmlString As String = CType(boObj, BusinessObjectBase).ProcessWSRequest()
            ' TO KEEP FINISH
            ' *********************************************************


            Return xmlString

        Catch ex As BOValidationException
            Return HandelBOValidation(ex)
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
                (TypeOf ex.InnerException Is BOValidationException) Then
                Return HandelBOValidation(CType(ex, System.Reflection.TargetInvocationException))
            Else
                Dim retMessage As String = String.Empty
                Dim retCode As String = String.Empty
                ElitaService.HandleErrors(ex, retCode, retMessage, xmlDataIn)
                If retMessage IsNot String.Empty Then
                    Dim userNetworkId As String = String.Empty
                    If Not retCode.Equals(ErrorCodes.WS_ERR_INVALID_TOKEN) Then
                        userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    End If
                    Return XMLHelper.FromErrorCodeToXML(retCode, retMessage, userNetworkId)
                End If
            End If
        End Try

    End Function

#End Region

#Region "Token"

    Public Shared Sub VerifyToken(isWcf As Boolean, token As String, Optional ByVal webServiceName As String = Nothing,
                                   Optional ByVal webServiceFunctionName As String = Nothing, Optional ByVal xmlStringDataIn As String = Nothing)
        Dim networkID As String
        Dim Token_Env As String
        Dim Token_Hub As String
        Dim bDisplayError As Boolean = False
        Dim Url As String
        If isWcf = True Then
            Url = Convert.ToString(OperationContext.Current.RequestContext.RequestMessage.Headers.To)
        Else
            Url = RequestSoapContext.Current.Actor.ToString()
        End If
        Dim Web_UserLog_id As New Guid
        Web_UserLog_id = Guid.NewGuid()

        Try

            If webServiceName Is Nothing OrElse webServiceFunctionName Is Nothing OrElse xmlStringDataIn Is Nothing Then

                Dim strIncomingString As String = OperationContext.Current.RequestContext.RequestMessage.ToString()
                Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
                doc.LoadXml(strIncomingString)

                Dim xmlData As System.Xml.XmlNodeList = doc.GetElementsByTagName("*") ' Read Elements
                Dim xmlAction As String = String.Empty
                Dim xmlBody As String = String.Empty

                For Each node As System.Xml.XmlNode In xmlData
                    If (node.Name.ToString().ToUpper().IndexOf("BODY") >= 0) Then
                        xmlBody = node.InnerXml
                    End If

                    If (node.Name.ToString().ToUpper().IndexOf("ACTION") >= 0) Then
                        xmlAction = node.InnerText
                        If (xmlAction.IndexOf("/") >= 0) Then
                            xmlAction = xmlAction.Substring(xmlAction.LastIndexOf("/") + 1)
                        End If
                    End If
                Next

                If xmlBody = String.Empty Then
                    Throw New Exception("Invalid Input XML.")
                End If

                If xmlAction = String.Empty Then
                    Throw New Exception("Invalid XML, No Function Name provided.")
                End If

                webServiceFunctionName = xmlAction
                xmlStringDataIn = xmlBody

            End If

        Catch ex As Exception
            Dim retMessage As String = ex.Message
            Dim retCode As String = String.Empty

            Try
                'Code to add LOG in Aps_Publishing table
                ElitaService.HandleErrors(ex, retCode, retMessage, xmlStringDataIn, False)
            Catch Log_Err As Exception
                Log_Err = Nothing
            End Try

        End Try

        Try

            If Authentication.VerifyWSToken(token, networkID, Token_Env, Token_Hub) = True Then
                LoginElita(isWcf, networkID, webServiceName, webServiceFunctionName)

                If Token_Env.ToUpper.Trim <> EnvironmentContext.Current.EnvironmentShortName.ToUpper.Trim OrElse Token_Hub.ToUpper.Trim <> AppConfig.ConnType.ToUpper.Trim Then
                    bDisplayError = True
                End If
            Else
                bDisplayError = True
            End If
            Webservices.WebUserLog(Web_UserLog_id, Url, webServiceFunctionName, networkID, Token_Env, Token_Hub, xmlStringDataIn, DateTime.Now)
            If bDisplayError = True Then
                AppConfig.Debug("WebService nToken Error" & vbCrLf &
                                " webServiceName: " & webServiceName & vbCrLf &
                                " webServiceFunctionName: " & webServiceFunctionName & vbCrLf &
                                " Token: " & token & vbCrLf &
                                " Date: " & Now & vbCrLf &
                                " Env: " & EnvironmentContext.Current.EnvironmentShortName & vbCrLf &
                                " Hub: " & AppConfig.ConnType)

                If isWcf = True Then
                    Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
                Else ' Wse
                    Throw New SecurityAccessDeniedException("VerifyToken Display Err Access Denied")
                End If
            End If

        Catch ex As ElitaWSException
            Throw ex

        Catch ex As Exception
            If isWcf = True Then
                Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
            Else ' Wse
                Throw New SecurityAccessDeniedException("VerifyToken Exception Access Denied")
            End If

        End Try

    End Sub


#End Region

#Region "Authentication"

    Public Shared Function VerifyLogin(isWcf As Boolean, complexUsername As String, _
                                    appPassword As String, Optional ByVal webServiceName As String = Nothing) As String
        Dim appUsername, networkID, group As String

        appUsername = AppConfig.WebService.AppId(complexUsername)
        networkID = AppConfig.WebService.UserId(complexUsername)
        group = AppConfig.WebService.Group(complexUsername)

        Return VerifyLogin(isWcf, appUsername, networkID, appPassword, group, webServiceName)
    End Function

    Public Shared Function VerifyLogin(isWcf As Boolean, appId As String, networkId As String, _
                                 appPassword As String, group As String, Optional ByVal webServiceName As String = Nothing) As String
        Dim isValidUser As Boolean = False
        Dim token As String

        'AppConfig.Debug("VerifyLogin")
        LoginElita(isWcf, networkId, webServiceName)
        'AppConfig.Debug("After LoginElita")
        isValidUser = Authentication.IsLdapUser(group, appId, appPassword)

        If Not isValidUser Then
            'AppConfig.Debug("LDAP Error")
            If isWcf = True Then
                Throw New SecurityAccessDeniedException("LDAP Access Denied")
            Else ' Wse
                Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
            End If
        End If
        'AppConfig.Debug("After LDAP")
        'AppConfig.Debug("Login to WebService")
        token = Authentication.CreateWSToken(networkId)
        'AppConfig.Debug("Login to WebService ")
        Return token
    End Function

    Private Shared Sub LoginElita(isWcf As Boolean, networkID As String, Optional ByVal webServiceName As String = Nothing, _
                                    Optional ByVal webServiceFunctionName As String = Nothing)
        Dim oAuthentication As New Authentication
        Try
            Dim objElitaPlusPrincipal As ElitaPlusPrincipal = oAuthentication.CreatePrincipal(networkID, webServiceName, webServiceFunctionName)
            Authentication.SetCulture()

            If webServiceName IsNot Nothing AndAlso Not webServiceName.Equals(String.Empty) Then
                If (objElitaPlusPrincipal.WebServiceOffLineMessage IsNot Nothing) AndAlso _
                    (Not objElitaPlusPrincipal.WebServiceOffLineMessage.Equals(String.Empty)) Then
                    Throw New ElitaWSException(objElitaPlusPrincipal.WebServiceOffLineMessage)
                End If
            End If

            If webServiceFunctionName IsNot Nothing AndAlso Not webServiceFunctionName.Equals(String.Empty) Then
                If (objElitaPlusPrincipal.WebServiceFunctionOffLineMessage IsNot Nothing) AndAlso _
                    (Not objElitaPlusPrincipal.WebServiceFunctionOffLineMessage.Equals(String.Empty)) Then
                    Throw New ElitaWSException(objElitaPlusPrincipal.WebServiceFunctionOffLineMessage)
                End If
            End If
        Catch ex As ElitaWSException
            Throw ex
        Catch ex As Exception
            AppConfig.Debug("LoginElita Error" & ex.Message)
            If isWcf = True Then
                Throw New SecurityAccessDeniedException("LoginElita Access Denied")
            Else ' Wse
                Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
            End If
        End Try
    End Sub
#End Region

#Region " Private Methods"

    Private Shared Sub ValidateXML(isWcf As Boolean, _xml As String, ByRef ds As DataSet, _
                                    schemaName As String)
        Dim _xsd, schemaPath As String
        Try
            If isWcf = True Then
                schemaPath = HostingEnvironment.ApplicationPhysicalPath & _
                    ElitaWebServiceConstants.SETTING_SCHEMA_DIR & schemaName
            Else ' Wse
                schemaPath = System.Web.HttpContext.Current.Server.MapPath( _
                    HttpContext.Current.Request.ApplicationPath & "/" & _
                    ElitaWebServiceConstants.SETTING_SCHEMA_DIR & schemaName)
            End If
            _xsd = XMLHelper.ReadXML(schemaPath)
            If Not XMLHelper.ValidateXML(_xml, _xsd) Then Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID)
            ds.ReadXml(XMLHelper.GetXMLStream(_xml))

        Catch ex As Exception
            'Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID, ErrorCodes.WS_XML_INVALID)
            Throw New BOValidationException(ErrorCodes.WS_XML_INVALID, ErrorCodes.WS_XML_INVALID)
        End Try

    End Sub

    Private Shared Function ObjectFromClassName(PartialAssemblyName As String, _
                                         QualifiedClassName As String, _
                                         Optional ByVal params As Object() = Nothing) As Object

        'ALR : HINT - If you have problems with this, make sure your classes are all specified with the correct case 
        ':  Assembly references are case sensitive

        Return Activator.CreateInstance([Assembly].LoadWithPartialName(PartialAssemblyName).GetType(QualifiedClassName), params)

    End Function

#End Region

#Region " Error Handling"

    Private Shared Sub HandleErrors(exc As Exception, ByRef retCode As String, ByRef retTranslatedMessage As String, xmlDataIn As String, Optional ByVal Translate As Boolean = True)
        Dim exE As Exception
        Try
            retCode = String.Empty
            retTranslatedMessage = String.Empty
            ' DEF-995 remove
            'Try
            '    Throw New UnHandledException(exc)
            'Catch ex1 As UnHandledException
            '    LogXML(xmlDataIn)
            '    xmlDataIn = Nothing
            '    exE = CType(ex1, Exception)
            '    AppConfig.Log(exE)
            'End Try

            ' DEF-995 add
            If exc.GetType Is GetType(UnHandledException) Then

                LogXML(xmlDataIn)
                xmlDataIn = Nothing
                exE = CType(exc, Exception)
                AppConfig.Log(exE)
            End If

            If exc.GetType Is GetType(Threading.ThreadAbortException) OrElse _
                (exc.InnerException IsNot Nothing AndAlso _
                 exc.InnerException.GetType Is GetType(Threading.ThreadAbortException)) Then
                retCode = ErrorCodes.UNEXPECTED_ERROR
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(retCode), retCode)
                Exit Sub
            End If

            'this is needed temporarily to prevent the typecast exception below -- ex = CType(exc, ElitaWSException)
            If exc.GetType Is GetType(System.NullReferenceException) OrElse _
                (exc.InnerException IsNot Nothing AndAlso _
                 exc.InnerException.GetType Is GetType(System.NullReferenceException)) Then
                LogXML(xmlDataIn)
                xmlDataIn = Nothing
                retCode = ErrorCodes.UNEXPECTED_ERROR
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(retCode), retCode)
                Exit Sub
            End If

            If exc.GetType Is GetType(Microsoft.Web.Services3.Security.SecurityFault) Then
                'retTranslatedMessage = exc.Message
                retCode = ErrorCodes.WS_ERR_INVALID_TOKEN
                retTranslatedMessage = exc.Message
                Exit Sub
            End If

            If exc.GetType Is GetType(TargetInvocationException) Then
                If exc.InnerException IsNot Nothing Then exc = exc.InnerException
            End If

            Dim errMsg As String
            Dim errMsgArr() As String
            Dim ex As ElitaWSException

            ' Not one of our exceptions - unhandled exception
            If Not exc.GetType.IsSubclassOf(GetType(ElitaPlusException)) AndAlso exc.GetType IsNot GetType(ElitaPlusException) Then ex = New ElitaWSException(exc)

            ' Elita+ Exception coming from the BO
            If (exc.GetType IsNot GetType(ElitaWSException) AndAlso _
            exc.GetType.IsSubclassOf(GetType(ElitaPlusException))) OrElse exc.GetType Is GetType(ElitaPlusException) Then
                ' exc.GetType.IsSubclassOf(GetType(ElitaPlusException)) Then ex = New ElitaWSException(CType(exc, ElitaPlusException))
                ex = New ElitaWSException(CType(exc, ElitaPlusException))
                errMsg = MiscUtil.CleanseSQLInjectChars(ex.Code)
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(errMsg), errMsg)
                retCode = ex.Code
                Exit Sub
            End If

            'exception is percolated from BO and wrapped in Reflection exception
            If ex IsNot Nothing AndAlso (exc.GetType Is GetType(ElitaWSException) OrElse exc.GetType Is GetType(System.Reflection.TargetInvocationException)) Then
                If exc.GetType Is GetType(System.Reflection.TargetInvocationException) AndAlso
                exc.InnerException.GetType Is GetType(BOValidationException) Then
                    ' Validation exception coming from the BO but wrapped in Reflection exception
                    retTranslatedMessage = WSUtility.FormatAndTranslateErrorsFromBOValidationExc(exc.InnerException, Translate)
                ElseIf exc.GetType Is GetType(System.Reflection.TargetInvocationException) AndAlso
                                exc.InnerException.GetType.IsSubclassOf(GetType(ElitaPlusException)) Then
                    ' is an ElitaWSException
                    errMsg = MiscUtil.CleanseSQLInjectChars(exc.InnerException.Message)
                    retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(errMsg), errMsg)
                    retCode = errMsg
                Else
                    ' Reflection exception
                    errMsg = MiscUtil.CleanseSQLInjectChars(exc.Message)
                    retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(errMsg), errMsg)
                    retCode = errMsg
                End If
            ElseIf ex Is Nothing Then
                errMsg = MiscUtil.CleanseSQLInjectChars(exc.Message)
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(errMsg), errMsg)
                retCode = errMsg
            Else
                ' Raise the exception as a SOAP Exception
                '**** cannot typecast some exception types.  need to fix in Elita Plus also
                LogXML(xmlDataIn)
                xmlDataIn = Nothing
                ex = CType(exc, ElitaWSException)
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(ErrorCodes.UNEXPECTED_ERROR), ErrorCodes.UNEXPECTED_ERROR)
                retCode = ErrorCodes.UNEXPECTED_ERROR
            End If

            Exit Sub

        Catch exHandleErr As Exception
            Try
                retTranslatedMessage = IIf(Translate, TranslationBase.TranslateLabelOrMessage(ErrorCodes.UNEXPECTED_ERROR), ErrorCodes.UNEXPECTED_ERROR)
            Catch exHandleErr2 As Exception
                retTranslatedMessage = ErrorCodes.UNEXPECTED_ERROR
            Finally
                LogXML(xmlDataIn)
                xmlDataIn = Nothing
                retCode = ErrorCodes.UNEXPECTED_ERROR
                Try
                    AppConfig.Log(exHandleErr)
                Catch exHandleErr3 As Exception
                    'handle to prevent sending exception stack trace to client if AppConfig.Log fails
                    'should retain retCode & retTranslatedMessage values
                End Try
            End Try
        End Try
    End Sub

    Public Shared Function HandelBOValidation(ex As BOValidationException) As String
        Dim retMessage As String = String.Empty
        Dim retCode As String = String.Empty
        Dim retPropertyName As String = String.Empty
        Dim userNetworkId As String = String.Empty

        Dim errList() As Object = ex.ValidationErrorList()
        If errList IsNot Nothing AndAlso errList.Length > 0 Then
            retCode = errList(0).Message.ToString
            retPropertyName = errList(0).PropertyName.ToString & ": "
            retMessage = TranslationBase.TranslateLabelOrMessage(retCode)
        Else
            retCode = ex.Code
            retMessage = TranslationBase.TranslateLabelOrMessage(retCode)
        End If

        If Not retCode.Equals(ErrorCodes.WS_ERR_INVALID_TOKEN) Then
            userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        End If

        Return XMLHelper.FromErrorCodeToXML(retCode, retPropertyName & retMessage, userNetworkId)

    End Function

    Private Shared Function HandelBOValidation(ex As System.Reflection.TargetInvocationException) As String

        Dim retMessage As String = String.Empty
        Dim retCode As String = String.Empty
        Dim retPropertyName As String = String.Empty

        retCode = CType(ex.InnerException, BOValidationException).Code
        retMessage = TranslationBase.TranslateLabelOrMessage(retCode)

        Dim userNetworkId As String = String.Empty
        If Not retCode.Equals(ErrorCodes.WS_ERR_INVALID_TOKEN) Then
            userNetworkId = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        End If

        Return XMLHelper.FromErrorCodeToXML(retCode, retMessage, userNetworkId)

    End Function

    Public Shared Function CreateErrorMsg(ex As Exception) As String
        Dim retMessage As String = String.Empty
        Dim retCode As String = String.Empty
        Try

            ElitaService.HandleErrors(ex, retCode, retMessage, "")

            If retMessage IsNot String.Empty Then
                Dim userNetworkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                Return XMLHelper.FromErrorCodeToXML(retCode, retMessage, userNetworkId)
            End If

        Catch exc As Exception
            Try
                retMessage = TranslationBase.TranslateLabelOrMessage(ErrorCodes.UNEXPECTED_ERROR)
            Catch exHandleErr2 As Exception
                retMessage = ErrorCodes.UNEXPECTED_ERROR
            Finally
                retCode = ErrorCodes.UNEXPECTED_ERROR
                Try
                    AppConfig.Log(exc)
                Catch exHandleErr3 As Exception
                    'handle to prevent sending exception stack trace to client if AppConfig.Log fails
                    'should retain retCode & retMessage values
                End Try
            End Try
        End Try

        Return XMLHelper.FromStringToXML("<ERROR><CODE>" & retCode & "</CODE>" & "<MESSAGE>" & retMessage & "</MESSAGE></ERROR>")

    End Function
    Private Shared Sub LogXML(xmlDataIn As String)

        If xmlDataIn IsNot Nothing AndAlso xmlDataIn.Length > 4000 Then
            AppConfig.Debug("<![CDATA[" & xmlDataIn.Substring(0, 4000) & "]]>")
        ElseIf xmlDataIn IsNot Nothing Then
            AppConfig.Debug("<![CDATA[" & xmlDataIn & "]]>")
        End If

    End Sub



#End Region

End Class
