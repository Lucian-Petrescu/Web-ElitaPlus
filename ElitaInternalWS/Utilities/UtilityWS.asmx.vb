Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System
Imports System.Xml
Imports Microsoft.Web.Services3
Imports Microsoft.Web.Services3.Security
Imports Microsoft.Web.Services3.Security.Tokens
Imports RMEncryption
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Web.Script.Services

<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/ElitaInternalWS/Utilities/UtilityWS")> _
Public Class UtilityWS
    Inherits ElitaWebService

    Private _xml As String
    Private _xsd As String

    <WebMethod(EnableSession:=True)> _
    Public Overrides Function ProcessRequest(ByVal token As String, _
                                               ByVal functionToProcess As String, _
                                               ByVal xmlStringDataIn As String) As String

        Try

            ElitaService.VerifyToken(False, token)

            _xml = xmlStringDataIn

            ' Read the process request configuration file
            Dim procInfo As XmlNode = XMLHelper.GetNodeByAttribute(XMLHelper.GetXMLDoc(Server.MapPath(ElitaWebServiceConstants.SETTING_CONFIG_PATH)), _
                                                                ElitaWebServiceConstants.WEB_SERVICE_SETTING_TAG, _
                                                                ElitaWebServiceConstants.SETTING_TAG, _
                                                                ElitaWebServiceConstants.SETTING_TAG_ATTRIBUTE, _
                                                                functionToProcess, _
                                                                Me.GetType.Name().ToUpper)
            ' Throw exception if request doesn't exist
            If procInfo Is Nothing Then Throw New ElitaWSException("Method " & functionToProcess & " Does Not Exist.", ErrorCodes.WS_INVALID_REQUEST)

            Dim schemaName As String = XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_SCHEMA)
            'Dim boClass As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." & XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_BO_CLASS)
            'Dim className As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." & schemaName.Replace(".xsd", "") & "InputDs"
            Dim boClass As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." & _
            XMLHelper.GetValue(procInfo, ElitaWebServiceConstants.SETTING_TAG_BO_CLASS)
            Dim className As String = ElitaWebServiceConstants.SETTING_BO_PATH & "." & _
                                    schemaName.Substring(schemaName.LastIndexOf("/") + 1).Replace(".xsd", "") & "InputDs"

            ' Get the input params DS object 
            Dim inputDS As Object = ObjectFromClassName(ElitaWebServiceConstants.SETTING_BO_PATH, className)

            ' Validate XML
            ValidateXML(ElitaWebServiceConstants.SETTING_SCHEMA_PATH & schemaName)

            ' Load XML
            inputDS.ReadXml(XMLHelper.GetXMLStream(_xml))

            ' Get the object that will process the request
            Dim params() As Object = {inputDS}
            Dim boObj As Object = ObjectFromClassName(ElitaWebServiceConstants.SETTING_BO_PATH, boClass)

            ' Get the method in the object to invoke 
            Dim xmlString As String
            Dim miMethodInfo As System.Reflection.MethodInfo = boObj.GetType.GetMethod(functionToProcess)

            ' Invoke method, passing the params as one dataset
            If Not miMethodInfo Is Nothing Then
                xmlString = miMethodInfo.Invoke(boObj, params)
            Else
                Throw New ElitaWSException("Method " & functionToProcess & " Does Not Exist.", ErrorCodes.WS_INVALID_REQUEST)
            End If

            Return xmlString

        Catch ex As Exception
            Return ElitaService.CreateErrorMsg(ex)
        End Try

    End Function

    Private Function ObjectFromClassName(ByVal PartialAssemblyName As String, _
                                     ByVal QualifiedClassName As String, _
                                     Optional ByVal params As Object() = Nothing) As Object

        Try

            Dim tClassType As Type

            Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.LoadWithPartialName(PartialAssemblyName)

            If Not asm Is Nothing Then
                tClassType = asm.GetType(QualifiedClassName)

                If Not tClassType Is Nothing Then
                    Return System.Activator.CreateInstance(tClassType, params)
                Else
                    Throw New ApplicationException("Class " & QualifiedClassName & " could not be loaded.")
                End If
            Else
                Throw New ApplicationException("Assembly " & PartialAssemblyName & " could not be loaded.")
            End If

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try

    End Function

    Public Sub ValidateXML(ByVal schemaPath As String)

        Try

            _xsd = XMLHelper.ReadXML(Server.MapPath(schemaPath))
            If Not XMLHelper.ValidateXML(_xml, _xsd) Then Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID)

        Catch ex As Exception
            Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID, ex)
        End Try

    End Sub

End Class


