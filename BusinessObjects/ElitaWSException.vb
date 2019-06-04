Imports System.xml
Imports System.text
Imports System.Web.Services.Protocols
Imports Assurant.ElitaPlus.Common
Imports System.Security.Principal


Public Class ElitaWSException
    Inherits ElitaPlusException

#Region " Properties"

    Private _source As Xml.XmlQualifiedName = SoapException.ServerFaultCode

    Private Property SourceException() As Xml.XmlQualifiedName
        Get
            Return _source
        End Get
        Set(ByVal Value As Xml.XmlQualifiedName)
            _source = Value
        End Set
    End Property

#End Region

#Region " Constructors"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="message">The English Version of the Exception Message</param>
    ''' <param name="code">the unique code identifying this class of exception (e.g.: "BO_VALIDATION_EXCEPTION")</param>
    ''' <param name="innerException">The lower level exception caught</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal message As String, ByVal innerException As Exception)

        'MyBase.New("Elita Web Service Error: " & innerException.Message, message, innerException)
        MyBase.New(innerException.Message, message, innerException)

        If message = ErrorCodes.WS_INVALID_REQUEST Or _
           message = ErrorCodes.WS_XML_INVALID Then

            SourceException = SoapException.ClientFaultCode

        Else

            SourceException = SoapException.ServerFaultCode

        End If

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor for client message exceptions
    ''' </summary>
    ''' <param name="message">The English Version of the Exception Message</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	9/19/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal sErrMsg As String, ByVal sErrCode As String)

        MyBase.New(sErrMsg, sErrCode, Nothing)
        SourceException = SoapException.ClientFaultCode

    End Sub

    Public Sub New(ByVal sErrMsg As String)

        MyBase.New(sErrMsg, sErrMsg, Nothing)
        SourceException = SoapException.ClientFaultCode

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor for unhandled exceptions
    ''' </summary>
    ''' <param name="innerException">Inner exception (if exists)</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(Optional ByVal innerException As Exception = Nothing)

        MyBase.New("Unexpected Elita Web Service Error: " & innerException.Message, ErrorTypes.ERROR_UNEXPECTED, innerException)
        SourceException = SoapException.ServerFaultCode

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor for unhandled exceptions
    ''' </summary>
    ''' <param name="innerException">Inner exception (if exists)</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal ex As ElitaPlusException)

        MyBase.New("Elita Web Service Error:" & ex.Message, ex.Code, ex.InnerException)
        SourceException = SoapException.ServerFaultCode

    End Sub

#End Region

#Region " Public Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a ElitaWSException in a SOAPException
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Raise(Optional ByVal bTranslate As Boolean = True) As SoapException

        Dim err As String = String.Empty
        If Not InnerException Is Nothing Then err = InnerException.Source
        Throw New SoapException(IIf(bTranslate, TranslationBase.TranslateLabelOrMessage(Me.Code), Me.Code), _
                                SourceException, _
                                SourceApplicationName, _
                                BuilErrorXML(Me.Code, err, InnerException, bTranslate), _
                                InnerException)
    End Function


#End Region

#Region " Private Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Build the xml with the error description
    ''' </summary>
    ''' <param name="errorCode">Error Code</param>
    ''' <param name="errorSource">Source</param>
    ''' <param name="ex">Inner Exception</param>
    ''' <returns>An XML node with the error description</returns>
    ''' <remarks>
    ''' 
    '''      XML output example:
    '''
    '''    <detail>
    '''        <Error xmlns=" http://tempuri.org/ElitaInternalWS/Vsc/VscWS /">
    '''          <ErrorMessage>Exception Information</ErrorMessage>
    '''          <ErrorSource>Exception Source</ErrorSource>
    '''          <User>user information</TimeStamp>
    '''          <TimeStamp>Exception date/time</TimeStamp>
    '''          <Detail>Inner Exception detail</Detail>
    '''        </Error>
    '''    </detail>
    ''' 
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function BuilErrorXML(ByVal errorCode As String, _
                                  ByVal errorSource As String, _
                                  ByVal ex As Exception, _
                                  Optional ByVal bTranslate As Boolean = True) As XmlNode

        ' Translate the error 
        Dim errorMessage As String = IIf(bTranslate, TranslationBase.TranslateLabelOrMessage(Me.Code), Me.Code)

        Dim xmlDoc As XmlDocument = New XmlDocument

        ' Create the Detail node
        Dim rootNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace)

        ' Build specific details for the SoapException. Add first child of detail XML element.
        Dim errorNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "Error", SoapException.DetailElementName.Namespace)

        ' Create and set the value for the ErrorMessage node
        Dim errorMessageNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorMessage", SoapException.DetailElementName.Namespace)
        errorMessageNode.InnerText = errorMessage

        ' Create and set the value for the error code node
        Dim errorCodeNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorCode", SoapException.DetailElementName.Namespace)
        errorCodeNode.InnerText = errorCode

        ' Create and set the value for the ErrorSource node
        Dim errorSourceNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorSource", SoapException.DetailElementName.Namespace)
        errorSourceNode.InnerText = errorSource

        ' Create and set the value for timestamp
        Dim errorSTime As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorSource", SoapException.DetailElementName.Namespace)
        errorSTime.InnerText = Date.Now.ToLongDateString & " " & Date.Now.ToLongTimeString

        ' Create and set the value for user
        Dim userInfo As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "User", SoapException.DetailElementName.Namespace)
        userInfo.InnerText = WindowsIdentity.GetCurrent().Name

        ' Create and set the value for the inner exception detail
        Dim detail As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "Detail", SoapException.DetailElementName.Namespace)
        detail.InnerText = GetExceptionInfo(ex)

        ' Append the Error child element nodes to the root detail node.
        errorNode.AppendChild(errorMessageNode)
        errorNode.AppendChild(errorCodeNode)
        errorNode.AppendChild(errorSourceNode)
        errorNode.AppendChild(userInfo)
        errorNode.AppendChild(errorSTime)

        ' Append the Detail node to the root node
        rootNode.AppendChild(errorNode)

        Return rootNode

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Translate a message
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Private Function Translate(ByVal code As String) As String

    '    Try

    '        Dim ds As DataSet = TranslationBase.GetTranslations("'" & code.ToUpper & "')", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    '        code = code.ToUpper

    '        If IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then

    '            Return code

    '        Else

    '            Return ds.Tables(0).Rows(0)("TRANSLATION").ToString

    '        End If

    '    Catch ex As Exception

    '        Return code

    '    End Try

    'End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns details of the inner exception
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/21/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function GetExceptionInfo(ByVal ex As Exception) As String

        If IsNothing(ex) Then Return String.Empty

        Dim strInfo As StringBuilder = New StringBuilder
        strInfo.AppendFormat("Raw Exception {0}", Environment.NewLine)
        strInfo.AppendFormat("   Header: {0}{1}", ex.Message, Environment.NewLine)
        strInfo.AppendFormat("   Source... {0}", Environment.NewLine)

        If (Not IsNothing(ex.TargetSite)) Then

            strInfo.AppendFormat("      Method Name: {0}{1}", ex.TargetSite.Name, Environment.NewLine)
            strInfo.AppendFormat("      Class Name: {0}{1}", ex.TargetSite.DeclaringType.FullName, Environment.NewLine)
            strInfo.AppendFormat("      Assembly Name: {0}{1}", ex.TargetSite.DeclaringType.Assembly.FullName, Environment.NewLine)

        End If

        strInfo.AppendFormat("      Application Name: {0}{1}", ex.Source, Environment.NewLine)
        strInfo.AppendFormat("      Stack trace: {0}{1}", ex.StackTrace, Environment.NewLine)

        Return strInfo.ToString()

    End Function


#End Region

End Class

