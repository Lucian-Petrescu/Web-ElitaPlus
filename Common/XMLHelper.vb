Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb

Public Class XMLHelper

#Region "Constants"

    '  Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='ISO-8859-1' ?>"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
    Private Const XML_NO_RECORD As String = "<Root><NoRecordsFound/></Root>"
    Private Const XML_ESC_BEGIN As String = "<![CDATA["
    Private Const XML_ESC_END As String = "]]>"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const WEBSERVICES_FILE As String = "/Services.xml"

#End Region

#Region " Public Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Reads an xml from a file and returns the xml string
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/22/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReadXML(ByVal path As String) As String

        Return GetXMLDoc(path).OuterXml

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Reads an xml form a file and return the xml document
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetXMLDoc(ByVal path As String) As XmlDataDocument

        Dim xmlDoc As XmlDataDocument = New XmlDataDocument
        xmlDoc.Load(path)
        Return xmlDoc

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the first node inside xmlParentNode
    ''' </summary>
    ''' <param name="xmlParentNode"></param>
    ''' <param name="nodeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNode(ByVal xmlParentNode As XmlNode, ByVal nodeName As String) As XmlNode

        Return xmlParentNode.SelectSingleNode(nodeName)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the first node in the xml document
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    ''' <param name="nodeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNode(ByVal xmlDoc As XmlDataDocument, ByVal nodeName As String) As XmlNode

        Return xmlDoc.ChildNodes(1).SelectSingleNode(nodeName)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the atribute value
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="attributeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAttribute(ByVal node As XmlNode, ByVal attributeName As String) As String

        Return node.Attributes(attributeName).Value

    End Function
    Public Shared Function GetElement(ByVal node As XmlNode, ByVal elementName As String) As String

        Return node(elementName).Value

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Find the node containing the specified attribute with the specified value
    ''' </summary>
    ''' <param name="parentNode"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="attributeName"></param>
    ''' <param name="attributeValue"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNodeByAttribute(ByVal parentNode As XmlNode, _
                                              ByVal webServiceNodeName As String, _
                                              ByVal nodeName As String, _
                                              ByVal attributeName As String, _
                                              ByVal attributeValue As String, _
                                              ByVal webServiceName As String) As XmlNode
        Dim outerList As XmlNodeList = parentNode.ChildNodes(1).SelectNodes(webServiceNodeName)
        Dim node As XmlNode
        For Each node In outerList
            If node.Attributes(attributeName).Value.ToUpper = webServiceName.ToUpper Then
                'If GetAttribute(node, webServiceName).ToUpper = attributeValue.ToUpper Then
                Dim list As XmlNodeList = node.SelectNodes(nodeName)
                Dim innerNode As XmlNode
                For Each innerNode In list
                    If GetAttribute(innerNode, attributeName).ToUpper = attributeValue.ToUpper Then Return innerNode
                Next
            End If
        Next

        Return Nothing

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Find the node containing the specified attribute with the specified value
    ''' </summary>
    ''' <param name="parentNode"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="attributeName"></param>
    ''' <param name="attributeValue"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Function GetNodeByAttribute(ByVal parentNode As XmlDataDocument, _
    '                                          ByVal nodeName As String, _
    '                                          ByVal attributeName As String, _
    '                                          ByVal attributeValue As String) As XmlNode

    '    Dim list As XmlNodeList = parentNode.ChildNodes(1).SelectNodes(nodeName)
    '    Dim node As XmlNode
    '    For Each node In list

    '        If GetAttribute(node, attributeName).ToUpper = attributeValue.ToUpper Then Return node

    '    Next

    '    Return Nothing

    'End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the value for the node
    ''' </summary>
    ''' <param name="xmlParentNode"></param>
    ''' <param name="nodeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetValue(ByVal xmlParentNode As XmlNode, ByVal nodeName As String) As String
        Dim aNode As XmlNode

        aNode = GetNode(xmlParentNode, nodeName)
        If Not aNode Is Nothing Then
            Return aNode.InnerXml
        Else
            Return Nothing
        End If

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the value for the node
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    ''' <param name="nodeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/29/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetValue(ByVal xmlDoc As XmlDataDocument, ByVal nodeName As String) As String

        Return GetNode(xmlDoc, nodeName).InnerXml

    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Creates a new root in the xml for the dataset
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function AddDSRoot(ByVal xml As String) As String

        Try

            Dim xmlDoc As XmlDocument = New XmlDocument
            xmlDoc.LoadXml(xml)

            Dim rootName As String = xmlDoc.DocumentElement.Name
            If xmlDoc.DocumentElement.Name.EndsWith("Ds") Then
                Return xmlDoc.OuterXml
            Else
                Return "<" & rootName & "Ds>" & xmlDoc.OuterXml & "</" & rootName & "Ds>"
            End If


        Catch ex As Exception

            Return xml

        End Try

    End Function
    Public Shared Function GetWebServiceNames() As DataSet
        
        Return Services.WebServicesNames.GetWebServiceNames

    End Function

    Public Shared Function GetWebServiceFunctionsNames(ByVal strWebServiceName As String) As DataSet

        Return Services.WebServicesNames.GetWebServiceFunctionsNames(strWebServiceName)

    End Function
#End Region

#Region "Conversions"


    Public Shared Function FromStringToXML(ByVal source As String) As String

        Try
            Return XML_VERSION_AND_ENCODING & source

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    ' Only create element if the value exists
    Private Shared Sub AppendToString(ByRef strBuilder As StringBuilder, ByVal tag As String, ByVal value As String)

        If value.Trim.Length > 0 Then strBuilder.Append("<" & tag & ">" & value & "</" & tag & ">")

    End Sub

    Public Shared Function FromErrorCodeToXML(ByVal code As String, ByVal Message As String, ByVal userNetworkID As String) As String

        Try
            Dim errorInfo As String = "User:" & userNetworkID & "; Date:" & Date.Now.ToString("s") & TimeZoneInfo.Local.ToString.Substring(4, 6)
            Dim xml As StringBuilder = New StringBuilder("<Error>")
            AppendToString(xml, "Code", code)
            AppendToString(xml, "Message", Message)
            AppendToString(xml, "ErrorInfo", errorInfo)
            xml.Append("</Error>")
            Return FromStringToXML(xml.ToString)



        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Shared Function FromErrorCodeToXML(ByVal code As String, ByVal Message As String, ByVal userNetworkID As String, ByVal RefundAmt As String, ByVal NoOfInstallmentPaid As String) As String

        Try
            Dim errorInfo As String = "User:" & userNetworkID & "; Date:" & Date.Now.ToString("s") & TimeZoneInfo.Local.ToString.Substring(4, 6)
            Dim xml As StringBuilder = New StringBuilder("<Error>")
            AppendToString(xml, "Code", code)
            AppendToString(xml, "Message", Message)
            AppendToString(xml, "ErrorInfo", errorInfo)
            AppendToString(xml, "RefundAmt", RefundAmt)
            AppendToString(xml, "NoOfInstPaid", NoOfInstallmentPaid)
            xml.Append("</Error>")
            Return FromStringToXML(xml.ToString)



        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    ' The XML does not have neither Carry Return nor Line Feed
    Public Shared Function FromDatasetToXML(ByVal ds As DataSet) As String
        Dim xmlDoc As XmlDataDocument

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            xmlDoc = New XmlDataDocument(ds)
            Return XML_VERSION_AND_ENCODING & xmlDoc.InnerXml
        Else
            Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
        End If

    End Function

    ' Set the acknoledge dataset
    Public Shared Function GetXML_OK_Response() As String

        Dim ds As DataSet = New DataSet("NEWDATASET")
        Dim dt As DataTable = New DataTable("NEWTABLE")
        dt.Columns.Add(TABLE_RESULT, GetType(String))
        Dim rw As DataRow = dt.NewRow
        rw(0) = VALUE_OK
        dt.Rows.Add(rw)
        ds.Tables.Add(dt)

        Dim xmlDoc As XmlDataDocument
        xmlDoc = New XmlDataDocument(ds)
        Dim stOutput As String = XML_VERSION_AND_ENCODING & xmlDoc.InnerXml
        stOutput = stOutput.Replace("NEWDATASET", "")
        stOutput = stOutput.Replace("NEWTABLE", "")
        stOutput = stOutput.Replace("<>", "")
        stOutput = stOutput.Replace("</>", "")
        Return stOutput

    End Function

    ' Set the acknoledge dataset
    Public Shared Function GetXML_OK_Response(ByVal strTagName As String, ByVal strValue As String, Optional ByVal TableName As String = Nothing) As String

        Dim ds As DataSet = New DataSet("NEWDATASET")
        Dim dt As DataTable
        If TableName Is Nothing OrElse TableName.Equals(String.Empty) Then
            dt = New DataTable("NEWTABLE")
        Else
            dt = New DataTable(TableName)
        End If

        dt.Columns.Add(TABLE_RESULT, GetType(String))
        dt.Columns.Add(strTagName, GetType(String))
        Dim rw As DataRow = dt.NewRow
        rw(0) = VALUE_OK
        rw(1) = strValue
        dt.Rows.Add(rw)
        ds.Tables.Add(dt)

        Dim xmlDoc As XmlDataDocument
        xmlDoc = New XmlDataDocument(ds)
        Dim stOutput As String = XML_VERSION_AND_ENCODING & xmlDoc.InnerXml
        stOutput = stOutput.Replace("NEWDATASET", "")
        If TableName Is Nothing OrElse TableName.Equals(String.Empty) Then
            stOutput = stOutput.Replace("NEWTABLE", "")
        End If

        stOutput = stOutput.Replace("<>", "")
        stOutput = stOutput.Replace("</>", "")
        Return stOutput

    End Function

    ' The XML does not have neither Carry Return nor Line Feed
    Public Shared Function FromDatasetToXML(ByVal ds As DataSet, ByVal excludeTags As ArrayList, _
                                            ByVal includeEmptyTag As Boolean, Optional ByVal includeXmlVersion As Boolean = True, _
                                            Optional ByVal removeDsRoot As String = Nothing, _
                                            Optional ByVal xmlCoded As Boolean = False, _
                                            Optional ByVal xmlCData As Boolean = False) As String
        Dim xmlDoc As XmlDataDocument
        Dim retXml As String = ""
        Dim emptyDate As String = ""
        Dim emptyDecimal As String = ""
        Dim emptyInt32 As String = ""
        Dim emptyInt16 As String = ""
        Dim emptyDouble As String = ""
        Dim emptySingle As String = ""
        Dim emptySByte As String = ""

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

            If includeEmptyTag Or xmlCoded Then
                Dim i As Integer

                For Each dt As DataTable In ds.Tables
                    For Each dr As DataRow In dt.Rows
                        For i = 0 To dt.Columns.Count - 1
                            If includeEmptyTag Then
                                If dr(i) Is DBNull.Value Then
                                    If dt.Columns(i).DataType.Equals(System.Type.GetType("System.DateTime")) Then
                                        emptyDate = "0001-01-01T00:00:00-05:00"
                                        dr(i) = emptyDate
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Byte[]")) Then
                                        ' do nothing
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Decimal")) Then
                                        emptyDecimal = "0.00000"
                                        dr(i) = emptyDecimal
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Int32")) Then
                                        emptyInt32 = "32767"
                                        dr(i) = emptyInt32
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Int16")) Then
                                        emptyInt16 = "32767"
                                        dr(i) = emptyInt16
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Double")) Then
                                        emptyDouble = "32767"
                                        dr(i) = emptyDouble
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Single")) Then
                                        emptySingle = "32767"
                                        dr(i) = emptySingle
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.SByte")) Then
                                        emptySByte = "127"
                                        dr(i) = emptySByte
                                    ElseIf dt.Columns(i).DataType.Equals(System.Type.GetType("System.Data.DataTable")) Then
                                        ' do nothing
                                    Else
                                        dr(i) = ""
                                    End If
                                End If
                            End If

                            If xmlCoded Then
                                If Not dr(i) Is DBNull.Value Then
                                    If Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.DateTime")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Byte[]")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Int32")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Int16")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Double")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Single")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.SByte")) AndAlso _
                                        Not dt.Columns(i).DataType.Equals(System.Type.GetType("System.Decimal")) Then

                                        Dim hasRelation As Boolean = False

                                        If ds.Relations.Count > 0 Then
                                            Dim k As Integer
                                            For k = 0 To ds.Relations.Count - 1
                                                If hasRelation = False Then
                                                    hasRelation = ds.Relations(k).ParentColumns().Contains(dt.Columns(i)) Or _
                                                                  ds.Relations(k).ChildColumns().Contains(dt.Columns(i))
                                                End If
                                            Next
                                        End If

                                        If hasRelation = False Then
                                            dr(i) = XML_ESC_BEGIN & dr(i) & XML_ESC_END
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                Next
            End If

            xmlDoc = New XmlDataDocument(ds)

            If Not excludeTags Is Nothing Then
                xmlDoc.DataSet.EnforceConstraints = False

                For Each tag As String In excludeTags
                    Dim nodeList As XmlNodeList = xmlDoc.SelectNodes(tag)

                    For Each node As XmlNode In nodeList
                        Dim docNode As XmlNode = xmlDoc.SelectSingleNode(tag)

                        If (Not docNode Is Nothing) Then
                            If (Not xmlDoc.SelectSingleNode(tag).ParentNode Is Nothing) Then
                                xmlDoc.SelectSingleNode(tag).ParentNode.RemoveChild(docNode)
                            End If
                        End If
                    Next
                Next
            End If

            retXml = xmlDoc.InnerXml

            If Not removeDsRoot Is Nothing Then
                retXml = retXml.Replace("<" & removeDsRoot & ">", "").Replace("</" & removeDsRoot & ">", "")
            End If

            If emptyDate <> "" Then
                retXml = retXml.Replace(emptyDate, "")
            End If

            If emptyDecimal <> "" Then
                retXml = retXml.Replace(emptyDecimal, "")
            End If

            If emptyInt32 <> "" Then
                retXml = retXml.Replace(emptyInt32, "")
            End If

            If emptyInt16 <> "" Then
                retXml = retXml.Replace(emptyInt16, "")
            End If

            If emptyDouble <> "" Then
                retXml = retXml.Replace(emptyDouble, "")
            End If

            If emptySByte <> "" Then
                retXml = retXml.Replace(emptySByte, "")
            End If

            If emptySingle <> "" Then
                retXml = retXml.Replace(emptySingle, "")
            End If

            If xmlCData = True AndAlso xmlCoded = False Then
                retXml = InsertCDATASections(retXml)
            End If

            If includeXmlVersion = True Then
                retXml = XML_VERSION_AND_ENCODING & retXml
            End If

        Else
            retXml = XML_NO_RECORD

            'the following is casuing blanks in the node: "<Root>   <NoRecordsFound /> </Root>"
            'If xmlCData = True AndAlso xmlCoded = False Then
            '    retXml = InsertCDATASections(retXml)
            'End If


            If includeXmlVersion = True Then
                retXml = XML_VERSION_AND_ENCODING & retXml
            End If
        End If

        retXml = retXml.Replace("&lt;![CDATA[", "<![CDATA[")
        retXml = retXml.Replace("]]&gt;</", "]]></")

        Return retXml
    End Function

    Public Shared Function InsertCDATASections(ByVal xmlStr As String) As String
        Dim reader As XmlValidatingReader = Nothing
        Dim writer As XmlTextWriter = Nothing
        Dim sw As StringWriter = Nothing

        Try
            reader = New XmlValidatingReader(xmlStr, XmlNodeType.Document, Nothing)
            sw = New StringWriter()
            writer = New XmlTextWriter(sw)
            writer.Formatting = Formatting.Indented
            reader.ValidationType = ValidationType.None
            reader.EntityHandling = EntityHandling.ExpandCharEntities
            Dim currentElement As String = String.Empty

            While (reader.Read())
                Select Case (reader.NodeType)
                    Case XmlNodeType.Element
                        currentElement = reader.Name
                        writer.WriteStartElement(currentElement)
                        While (reader.MoveToNextAttribute())
                            writer.WriteAttributeString(reader.Name, reader.Value)
                        End While
                    Case XmlNodeType.Text
                        writer.WriteCData(reader.Value)
                    Case XmlNodeType.EndElement
                        writer.WriteEndElement()
                End Select
            End While
        Catch ex As Exception
            Return ex.Message
        Finally
            reader.Close()
            writer.Close()
        End Try

        Return sw.ToString
    End Function


    ' The XML has Carry Return and Line Feed
    Public Shared Function FromDatasetToXMLCrLf(ByVal ds As DataSet) As String
        Dim xmlDoc As String

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            xmlDoc = ds.GetXml
            Return XML_VERSION_AND_ENCODING & Microsoft.VisualBasic.Constants.vbCrLf & xmlDoc
        Else
            Return XML_VERSION_AND_ENCODING & Microsoft.VisualBasic.Constants.vbCrLf & XML_NO_RECORD
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a dataset in a xml string based on the schema
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[os08rp]	6/28/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function FromDatasetToXML_Coded(ByVal ds As DataSet) As String
        Dim xmlString As String = String.Empty
        Dim mDate As DateTime
        Dim row As DataRow

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim i, j, k As Integer
            Dim oItem As Object
            Dim sItem As String

            xmlString &= "<" & ds.DataSetName & ">"
            For i = 0 To ds.Tables.Count - 1
                For j = 0 To ds.Tables(i).Rows.Count - 1
                    xmlString &= "<" & ds.Tables(i).TableName & ">"
                    row = ds.Tables(i).Rows(j)
                    For k = 0 To ds.Tables(i).Columns.Count - 1
                        Dim dc As DataColumn = ds.Tables(i).Columns(k)
                        oItem = row.Item(k)
                        sItem = oItem.ToString
                        xmlString &= "<" & dc.ColumnName & ">" & XML_ESC_BEGIN & sItem & _
                                        XML_ESC_END & "</" & dc.ColumnName & ">"
                    Next
                    xmlString &= "</" & ds.Tables(i).TableName & ">"
                Next
            Next
            xmlString &= "</" & ds.DataSetName & ">"
            If Not xmlString.Equals(String.Empty) Then
                Return XML_VERSION_AND_ENCODING & xmlString
            Else
                Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
            End If
        Else
            Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
        End If

    End Function

    Public Shared Function FromDatasetToXML_Std(ByVal ds As DataSet) As String
        Dim xmlString As String = String.Empty
        Dim mDate As DateTime
        Dim row As DataRow

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim i, j, k As Integer
            Dim oItem As Object
            Dim sItem As String
            xmlString &= "<" & ds.DataSetName & ">"
            For i = 0 To ds.Tables.Count - 1
                For j = 0 To ds.Tables(i).Rows.Count - 1
                    xmlString &= "<" & ds.Tables(i).TableName & ">"
                    row = ds.Tables(i).Rows(j)
                    For k = 0 To ds.Tables(i).Columns.Count - 1
                        Dim dc As DataColumn = ds.Tables(i).Columns(k)
                        oItem = row.Item(k)
                        If (dc.DataType Is GetType(System.DateTime)) AndAlso (Not oItem Is DBNull.Value) Then
                            sItem = CType(oItem, Date).ToString("s") ' SortableDateTimePattern: yyyy'-'MM'-'dd'T'HH':'mm':'ss 2008-01-16T15:12:59 
                        Else
                            sItem = oItem.ToString
                        End If
                        xmlString &= "<" & dc.ColumnName & ">" & XML_ESC_BEGIN & sItem & _
                                    XML_ESC_END & "</" & dc.ColumnName & ">"
                    Next
                    xmlString &= "</" & ds.Tables(i).TableName & ">"
                Next
            Next
            xmlString &= "</" & ds.DataSetName & ">"
            If Not xmlString.Equals(String.Empty) Then
                Return XML_VERSION_AND_ENCODING & xmlString
            Else
                Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
            End If
        Else
            Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
        End If

    End Function

    Public Shared Function FromDatasetToXML_Std(ByVal ds As DataSet, ByVal excludeTags As ArrayList) As String
        Dim xmlString As String = String.Empty
        Dim mDate As DateTime
        Dim row As DataRow

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim i, j, k As Integer
            Dim oItem As Object
            Dim sItem As String
            xmlString &= "<" & ds.DataSetName & ">"
            For i = 0 To ds.Tables.Count - 1
                For j = 0 To ds.Tables(i).Rows.Count - 1
                    xmlString &= "<" & ds.Tables(i).TableName & ">"
                    row = ds.Tables(i).Rows(j)
                    For k = 0 To ds.Tables(i).Columns.Count - 1
                        Dim dc As DataColumn = ds.Tables(i).Columns(k)
                        oItem = row.Item(k)
                        If (dc.DataType Is GetType(System.DateTime)) AndAlso (Not oItem Is DBNull.Value) Then
                            sItem = CType(oItem, Date).ToString("s") ' SortableDateTimePattern: yyyy'-'MM'-'dd'T'HH':'mm':'ss 2008-01-16T15:12:59 
                        Else
                            sItem = oItem.ToString
                        End If
                        If Not excludeTags.Contains(dc.ColumnName.ToUpper) Then
                            xmlString &= "<" & dc.ColumnName & ">" & XML_ESC_BEGIN & sItem & _
                                        XML_ESC_END & "</" & dc.ColumnName & ">"
                        End If
                    Next
                    xmlString &= "</" & ds.Tables(i).TableName & ">"
                Next
            Next
            xmlString &= "</" & ds.DataSetName & ">"
            If Not xmlString.Equals(String.Empty) Then
                Return XML_VERSION_AND_ENCODING & xmlString
            Else
                Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
            End If
        Else
            Return XML_VERSION_AND_ENCODING & XML_NO_RECORD
        End If

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a xml string/path/url in a stream
    ''' </summary>
    ''' <param name="XMLSource">XML string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/22/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetXMLStream(ByVal XMLSource As String) As XmlTextReader

        ' Check for raw XML, XML file, or XML url
        Dim XMLStream As Stream = New MemoryStream

        Try

            Return New XmlTextReader(New StringReader(XMLSource))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Shared Function FromExcelToDataset(ByVal excelFile As String, ByVal tableName As String) As DataSet
        '   Try
        ' Create variables that are used in code sample.
        Dim i, j As Integer
        Dim strConn, strTable As String
        Dim conn As OleDbConnection
        Dim dt As DataTable
        Dim myData As OleDbDataAdapter
        Dim ds As New DataSet

        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFile + ";Extended Properties=""Excel 8.0;HDR=Yes"""
        conn = New OleDbConnection(strConn)
        'Get Table name
        conn.Open()
        dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        If dt.Rows.Count > 0 Then
            strTable = dt.Rows(0).Item("TABLE_NAME")
        Else
            Return Nothing
        End If
        conn.Close()
        conn.Dispose()
        myData = New OleDbDataAdapter("Select * from [" + strTable + "]", strConn)
        myData.Fill(ds)
        ' Set Table Name and Remove Spaces from the Column Name
        ds.DataSetName = tableName & "Ds"
        ds.Tables(0).TableName = tableName
        RemoveColumnNameSuffix(ds)

        Return ds
    End Function

    Private Shared Sub RemoveColumnNameSuffix(ByVal ds As DataSet)
        Dim oCol As DataColumn

        For Each oCol In ds.Tables(0).Columns
            oCol.ColumnName = oCol.ColumnName.Replace(" ", String.Empty)
        Next

    End Sub

    Public Shared Function FromExcelToXml(ByVal excelFile As String, ByVal tableName As String) As String
        Dim ds As DataSet
        Dim xml As String

        ds = XMLHelper.FromExcelToDataset(excelFile, tableName)
        xml = XMLHelper.FromDatasetToXMLCrLf(ds)

        Return xml
    End Function


    Public Shared Function GetXMLElementsToAttributesTransformScript() As String

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">")
        sb.AppendLine("<xsl:template match=""*"">")
        sb.AppendLine("<xsl:copy>")
        sb.AppendLine("<xsl:for-each select=""@*|*[not(* or @*)]"">")
        sb.AppendLine("<xsl:attribute name=""{name(.)}"">")
        sb.AppendLine("<xsl:value-of select="".""/>")
        sb.AppendLine("</xsl:attribute>")
        sb.AppendLine("</xsl:for-each>")
        sb.AppendLine("<xsl:apply-templates select=""*[* or @*]|text()""/>")
        sb.AppendLine("</xsl:copy>")
        sb.AppendLine("</xsl:template>")
        sb.AppendLine("</xsl:stylesheet>")

        Return sb.ToString

    End Function
#End Region

#Region "Validation"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Validates a XML based on the schemas
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="schema">Schemas string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	3/22/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ValidateXML(ByRef xml As String, ByVal schema As String) As Boolean

        ' Verify the schema
        Dim xsd As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(GetXMLStream(schema), Nothing)
        '    xsd.Compile(Nothing)
        xsd.Compile(Nothing)

        If Not xml.StartsWith("<?xml") Then xml = XML_VERSION_AND_ENCODING & xml

        ' Verify the xml
        Dim xmlStream As XmlTextReader = GetXMLStream(xml)
        If Not IsNothing(xmlStream) Then

            Dim validator As XmlValidatingReader = New XmlValidatingReader(xmlStream)
            validator.ValidationType = ValidationType.Schema
            validator.Schemas.Add(xsd)

            ' Go through the whole xml
            While (validator.Read())

            End While

        Else

            Return False

        End If

        Return True

    End Function

    ' Error Handling

    'Public Sub ShowCompileError(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
    '    Console.WriteLine("Validation Error: {0}", e.Message)
    'End Sub


    'Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='ISO-8859-1' ?>"

    ''Display the validation error.
    'Private Sub ValidationEventHandle(ByVal sender As Object, ByVal args As System.Xml.Schema.ValidationEventArgs)
    '    Dim m_success As Boolean = False
    '    Console.WriteLine("Validation error: " & args.Message)
    'End Sub 'ValidationEventHandle


    'Public Function ValidateXML2(ByRef xml As String, ByVal schema As String) As Boolean

    '    ' Verify the schema
    '    Dim xsd As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(XMLHelper.GetXMLStream(schema), Nothing)
    '    '    xsd.Compile(Nothing)
    '    Dim e As System.Xml.Schema.ValidationEventArgs
    '    xsd.Compile(AddressOf ShowCompileError)

    '    If Not xml.StartsWith("<?xml") Then xml = XML_VERSION_AND_ENCODING & xml

    '    ' Verify the xml
    '    Dim xmlStream As XmlTextReader = XMLHelper.GetXMLStream(xml)
    '    If Not Microsoft.VisualBasic.Information.IsNothing(xmlStream) Then

    '        Dim validator As XmlValidatingReader = New XmlValidatingReader(xmlStream)

    '        ' Set the validation event handler
    '        AddHandler validator.ValidationEventHandler, AddressOf Me.ValidationEventHandle

    '        validator.ValidationType = ValidationType.Schema
    '        validator.Schemas.Add(xsd)

    '        ' Go through the whole xml
    '        While (validator.Read())
    '            Select Case validator.NodeType
    '                Case XmlNodeType.Element
    '                    Console.Write("<{0}>", validator.Name)
    '                Case XmlNodeType.Text
    '                    Console.Write(validator.Value)
    '                Case XmlNodeType.CDATA
    '                    Console.Write("<![CDATA[{0}]]>", validator.Value)
    '                Case XmlNodeType.ProcessingInstruction
    '                    Console.Write("<?{0} {1}?>", validator.Name, validator.Value)
    '                Case XmlNodeType.Comment
    '                    Console.Write("<!--{0}-->", validator.Value)
    '                Case XmlNodeType.XmlDeclaration
    '                    Console.Write("<?xml version='1.0'?>")
    '                Case XmlNodeType.Document
    '                Case XmlNodeType.DocumentType
    '                    Console.Write("<!DOCTYPE {0} [{1}]", validator.Name, validator.Value)
    '                Case XmlNodeType.EntityReference
    '                    Console.Write(validator.Name)
    '                Case XmlNodeType.EndElement
    '                    Console.Write("</{0}>", validator.Name)
    '            End Select
    '        End While
    '        'Close the reader.
    '        If Not (validator Is Nothing) Then
    '            validator.Close()
    '        End If
    '    Else

    '        Return False

    '    End If




    '    Return True

    'End Function


#End Region




End Class
