Imports System.IO
Imports System.Xml
Imports System.Configuration
Imports System.Reflection


Public NotInheritable Class ConfigReader

#Region "CONSTANTS"

    Private Const QUERY_FILE_NOT_FOUND As String = "DATABASE QUERY FILE NOT FOUND"
    Private Const SQL_STATEMENT_NOT_FOUND As String = "SQL STATEMENT NOT FOUND"


#End Region


#Region "Member Variables"
    'this is declared shared because it serves as a global cache for all config
    'xml files that are loaded.  All sessions share this cache (hashtable)
    Private Shared configCollection As New Hashtable
#End Region


#Region " Constructors "

    Private Sub New()

    End Sub

#End Region


    'Retrieves the Node Content in the xml config according the passed
    'xPath paramter.
    'Example of valid values for the xPath Parameter are : 
    '   "/SQL/WORK_REQUEST/LOAD"
    Public Shared Function GetNodeValue(ByVal describedType As Type, ByVal xPath As String) As String

        Dim node As XmlNode = GetNode(describedType, xPath)

        If node Is Nothing Then
            Throw New ConfigAccessException("DAL Config path not found : " & xPath)
        Else
            Return node.InnerText.Trim
        End If

    End Function


    'Retrieves the node in the xml config file according to the  the passed
    'xPath paramter.
    'Example of valid values for the xPath Parameter are : 
    '   "/SQL/WORK_REQUEST/LOAD"
    Public Shared Function GetNode(ByVal describedType As Type, ByVal xPath As String) As XmlNode

        'Load the sql xml embedded resource 
        Dim xmlDoc As XmlDocument = ConfigDocument(describedType)

        Dim node As XmlNode

        'retrieve a node of text with sql and parameters inside it.
        Return xmlDoc.SelectSingleNode(xPath)
    End Function

    'returns an xml document either from the cache (hashtable) or if it is not there
    'then it is loaded.
    Public Shared ReadOnly Property ConfigDocument(ByVal describedType As Type) As XmlDocument
        Get
            'only allow one thread (aspnet session) to access the shared hashtable at a time
            
                Dim _configDocument As XmlDocument = Nothing

                'get from the cache (hashtable)
                _configDocument = configCollection.Item(describedType)
                'if it was not in the cache, then using the type of the class, retrieve
                'the xml file, which is just an embeded resource in the type's (class)
                'assembly.  Use a stream to read it in.
                If _configDocument Is Nothing Then
                    Dim containerAssembly As [Assembly] = describedType.Assembly
                    Dim containerNameSpace As String = describedType.Namespace
                    Dim resName As String = containerNameSpace & "." & describedType.Name & ".xml"
                    Dim resStream As Stream
                    Try
                        'create a stream from embeded resource (xml file)
                        Dim strManiFestResName As String() = containerAssembly.GetManifestResourceNames()
                        resStream = containerAssembly.GetManifestResourceStream(resName)
                        _configDocument = New XmlDocument
                        'load xmlDoc from the stream containing resource
                        _configDocument.Load(resStream)
                        'add to hashtable cache
                        configCollection.Add(describedType, _configDocument)
                    Catch ex As Exception
                        _configDocument = Nothing
                        Throw ex
                    Finally
                        resStream.Close()
                    End Try
                End If
                Return _configDocument
            
        End Get
    End Property

    Public Shared Sub AddNodeAttribute(ByVal describedType As Type, ByVal oNode As Xml.XmlNode, _
    ByVal oAttrName As String, ByVal oAttrContent As String)
        'Load the sql xml embedded resource 
        Dim xmlDoc As XmlDocument = ConfigDocument(describedType)
        Dim oAttr As XmlAttribute = xmlDoc.CreateAttribute(oAttrName)
        oAttr.Value = oAttrContent
        oNode.Attributes.Append(oAttr)
    End Sub


End Class
