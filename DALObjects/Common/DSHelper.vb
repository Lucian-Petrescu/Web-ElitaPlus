'************************************************************************************
'
'Portions of this module (the group by functions)
'  were derived from Microsoft Knowledge Base article Q325685
'
'Samples of the method calls and 
'explanations are available at http://support.microsoft.com/kb/325685
'
'
'
'************************************************************************************

Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Text
Imports System.Xml.Xsl

Public Class DSHelper

#Region "Local and Public Variables & Constants"

    Public ds As DataSet

    Private m_FieldList, GroupByFieldList, cDataFields As String
    Private GroupByFieldInfo, m_FieldInfo As ArrayList

    Private Class FieldInfo
        Public RelationName As String
        Public FieldName As String      ' source table field name
        Public FieldAlias As String     ' destination table field name
        Public Aggregate As String
    End Class

    Public Enum XMLOutputMethod As Integer
        xml
        html
    End Enum

    Private Const COL_ROW_NUM As String = "ROWNUMBER"


#End Region

#Region "Constructors"

    Public Sub New(DataSet As DataSet)
        ds = DataSet
    End Sub

    Public Sub New()
        ds = Nothing
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub ParseFieldList(FieldList As String, Optional ByVal AllowRelation As Boolean = False)
        '
        ' Parses FieldList into FieldInfo objects and then adds them to the m_FieldInfo private member
        '
        ' FieldList syntax: [relationname.]fieldname[ alias],...
        '
        If m_FieldList = FieldList Then Exit Sub
        m_FieldInfo = New ArrayList()
        m_FieldList = FieldList
        Dim Field As FieldInfo
        Dim FieldParts() As String
        Dim Fields() As String = FieldList.Split(",")
        Dim I As Integer
        For I = 0 To Fields.Length - 1
            Field = New FieldInfo()
            '
            ' Parse FieldAlias
            '
            FieldParts = Fields(I).Trim().Split(" ")
            Select Case FieldParts.Length
                Case 1
                    ' To be set at the end of the loop
                Case 2
                    Field.FieldAlias = FieldParts(1)
                Case Else
                    Throw New ArgumentException("Too many spaces in field definition: '" & Fields(I) & "'.")
            End Select
            '
            ' Parse FieldName and RelationName
            '
            FieldParts = FieldParts(0).Split(".")
            Select Case FieldParts.Length
                Case 1
                    Field.FieldName = FieldParts(0)
                Case 2
                    If Not AllowRelation Then _
                        Throw New ArgumentException("Relation specifiers not allowed in field list: '" & Fields(I) & "'.")
                    Field.RelationName = FieldParts(0).Trim()
                    Field.FieldName = FieldParts(1).Trim()
                Case Else
                    Throw New ArgumentException("Invalid field definition: '" & Fields(I) & "'.")
            End Select
            If Field.FieldAlias = "" Then Field.FieldAlias = Field.FieldName
            m_FieldInfo.Add(Field)
        Next
    End Sub

    Private Sub ParseGroupByFieldList(FieldList As String)
        '
        ' Parses FieldList into FieldInfo objects and then adds them to the GroupByFieldInfo private member
        '
        ' FieldList syntax: fieldname[ alias]|operatorname(fieldname)[ alias],...
        '
        ' Supported Operators: count,sum,max,min,first,last
        '
        If GroupByFieldList = FieldList Then Exit Sub
        Const OperatorList As String = ",count,sum,max,min,first,last,"
        GroupByFieldInfo = New ArrayList()
        Dim Field As FieldInfo, FieldParts() As String, Fields() As String = FieldList.Split(",")
        Dim I As Integer
        For I = 0 To Fields.Length - 1
            Field = New FieldInfo()
            '
            ' Parse FieldAlias
            '
            FieldParts = Fields(I).Trim().Split(" ")
            Select Case FieldParts.Length
                Case 1
                    ' To be set at the end of the loop
                Case 2
                    Field.FieldAlias = FieldParts(1)
                Case Else
                    Throw New ArgumentException("Too many spaces in field definition: '" & Fields(I) & "'.")
            End Select
            '
            ' Parse FieldName and Aggregate
            '
            FieldParts = FieldParts(0).Split("(")
            Select Case FieldParts.Length
                Case 1
                    Field.FieldName = FieldParts(0)
                Case 2
                    Field.Aggregate = FieldParts(0).Trim().ToLower() ' You will do a case-sensitive comparison later.
                    Field.FieldName = FieldParts(1).Trim(" "c, ")"c)
                Case Else
                    Throw New ArgumentException("Invalid field definition: '" & Fields(I) & "'.")
            End Select
            If Field.FieldAlias = "" Then
                If Field.Aggregate = "" Then
                    Field.FieldAlias = Field.FieldName
                Else
                    Field.FieldAlias = Field.Aggregate & "Of" & Field.FieldName
                End If
            End If
            GroupByFieldInfo.Add(Field)
        Next
        GroupByFieldList = FieldList
    End Sub

    Private Function LocateFieldInfoByName(FieldList As ArrayList, Name As String) As FieldInfo
        '
        ' Looks up a FieldInfo record based on FieldName
        '
        Dim Field As FieldInfo
        For Each Field In FieldList
            If Field.FieldName = Name Then Return Field
        Next
    End Function

    Private Function CreateGroupByTable(TableName As String, _
                                  SourceTable As DataTable, _
                                  FieldList As String) As DataTable
        '
        ' Creates a table based on aggregates of fields of another table
        '
        ' RowFilter affects rows before the GroupBy operation. No HAVING-type support
        ' although this can be emulated by later filtering of the resultant table.
        '
        ' FieldList syntax: fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...
        '
        If FieldList = "" Then
            Throw New ArgumentException("You must specify at least one field in the field list.")
            ' Return CreateTable(TableName, SourceTable)
        Else
            Dim dt As New DataTable(TableName)
            ParseGroupByFieldList(FieldList)
            Dim Field As FieldInfo, dc As DataColumn
            For Each Field In GroupByFieldInfo
                dc = SourceTable.Columns(Field.FieldName)
                If Field.Aggregate = "" Then
                    dt.Columns.Add(Field.FieldAlias, dc.DataType, dc.Expression)
                Else
                    dt.Columns.Add(Field.FieldAlias, dc.DataType)
                End If
            Next
            If dt.Columns(COL_ROW_NUM) Is Nothing Then dt.Columns.Add(COL_ROW_NUM, GetType(String))
            If Not ds Is Nothing Then ds.Tables.Add(dt)
            Return dt
        End If
    End Function

    Private Sub InsertGroupByInto(DestTable As DataTable, _
                             SourceTable As DataTable, _
                             FieldList As String, _
                             Optional ByVal RowFilter As String = "", _
                             Optional ByVal GroupBy As String = "", _
                             Optional ByVal Rollup As Boolean = False)
        '
        ' Copies the selected rows and columns from SourceTable and inserts them into DestTable
        ' FieldList has same format as CreateGroupByTable
        '
        ParseGroupByFieldList(FieldList)  ' parse field list
        ParseFieldList(GroupBy)           ' parse field names to Group By into an arraylist
        Dim Field As FieldInfo
        Dim Rows() As DataRow = SourceTable.Select(RowFilter, GroupBy)
        Dim SourceRow, LastSourceRow As DataRow, SameRow As Boolean, I As Integer, J As Integer, K As Integer
        Dim DestRows(m_FieldInfo.Count) As DataRow, RowCount(m_FieldInfo.Count) As Integer
        '
        ' Initialize Grand total row
        '
        DestRows(0) = DestTable.NewRow()
        '
        ' Process source table rows
        '
        For Each SourceRow In Rows
            '
            ' Determine whether we've hit a control break
            '
            SameRow = False
            If Not (LastSourceRow Is Nothing) Then
                SameRow = True
                For I = 0 To m_FieldInfo.Count - 1 ' fields to Group By
                    Field = m_FieldInfo(I)
                    If ColumnEqual(LastSourceRow(Field.FieldName), SourceRow(Field.FieldName)) = False Then
                        SameRow = False
                        Exit For
                    End If
                Next I
                '
                ' Add previous totals to the destination table
                '
                If Not SameRow Then
                    For J = m_FieldInfo.Count To I + 1 Step -1
                        '
                        ' Make NULL the key values for levels that have been rolled up
                        '
                        For K = m_FieldInfo.Count - 1 To J Step -1
                            Field = LocateFieldInfoByName(GroupByFieldInfo, m_FieldInfo(K).FieldName)
                            If Not (Field Is Nothing) Then   ' Group By field does not have to be in field list
                                DestRows(J)(Field.FieldAlias) = DBNull.Value
                            End If
                        Next K
                        '
                        ' Make NULL any non-aggregate, non-group-by fields in anything other than
                        ' the lowest level.
                        '
                        If J <> m_FieldInfo.Count Then
                            For Each Field In GroupByFieldInfo
                                If Field.Aggregate <> "" Then Exit For
                                If LocateFieldInfoByName(m_FieldInfo, Field.FieldName) Is Nothing Then
                                    DestRows(J)(Field.FieldAlias) = DBNull.Value
                                End If
                            Next
                        End If
                        '
                        ' Add row
                        '
                        DestTable.Rows.Add(DestRows(J))
                        If Rollup = False Then Exit For ' only add most child row if not doing a roll-up
                    Next J
                End If
            End If
            '
            ' create new destination rows
            ' Value of I comes from previous If block
            '
            If Not SameRow Then
                For J = m_FieldInfo.Count To I + 1 Step -1
                    DestRows(J) = DestTable.NewRow()
                    RowCount(J) = 0
                    If Rollup = False Then Exit For
                Next J
            End If
            For J = 0 To m_FieldInfo.Count
                RowCount(J) += 1
                For Each Field In GroupByFieldInfo

                    If DestRows(J) Is Nothing Then Exit For

                    Select Case Field.Aggregate  ' this test is case-sensitive - made lower-case by Build_GroupByFiledInfo
                        Case ""    ' implicit Last
                            DestRows(J)(Field.FieldAlias) = SourceRow(Field.FieldName)
                        Case "last"
                            DestRows(J)(Field.FieldAlias) = SourceRow(Field.FieldName)
                        Case "first"
                            If RowCount(J) = 1 Then DestRows(J)(Field.FieldAlias) = SourceRow(Field.FieldName)
                        Case "count"
                            DestRows(J)(Field.FieldAlias) = RowCount(J)
                        Case "sum"
                            DestRows(J)(Field.FieldAlias) = Add(DestRows(J)(Field.FieldAlias), SourceRow(Field.FieldName))
                        Case "max"
                            DestRows(J)(Field.FieldAlias) = Max(DestRows(J)(Field.FieldAlias), SourceRow(Field.FieldName))
                        Case "min"
                            If RowCount(J) = 1 Then
                                DestRows(J)(Field.FieldAlias) = SourceRow(Field.FieldName)  ' so we get by initial NULL
                            Else
                                DestRows(J)(Field.FieldAlias) = Min(DestRows(J)(Field.FieldAlias), SourceRow(Field.FieldName))
                            End If
                    End Select
                Next
            Next J
            LastSourceRow = SourceRow
        Next
        If Rows.Length > 0 Then
            '
            ' Make NULL the key values for levels that have been rolled up
            '
            For J = m_FieldInfo.Count To 0 Step -1
                For K = m_FieldInfo.Count - 1 To J Step -1
                    Field = LocateFieldInfoByName(GroupByFieldInfo, m_FieldInfo(K).FieldName)
                    If Not (Field Is Nothing) Then  ' Group By field does not have to be in field list
                        DestRows(J)(Field.FieldAlias) = DBNull.Value
                    End If
                Next K
                '
                ' Make NULL any non-aggregate, non-group-by fields in anything other than
                ' the lowest level.
                '
                If J <> m_FieldInfo.Count Then
                    For Each Field In GroupByFieldInfo
                        If Field.Aggregate <> "" Then Exit For
                        If LocateFieldInfoByName(m_FieldInfo, Field.FieldName) Is Nothing Then
                            DestRows(J)(Field.FieldAlias) = DBNull.Value
                        End If
                    Next
                End If
                '
                ' Add row
                '
                DestTable.Rows.Add(DestRows(J))
                If Rollup = False Then Exit For
            Next J

            For I = 0 To DestTable.Rows.Count - 1
                If Not DestTable.Columns(COL_ROW_NUM) Is Nothing Then
                    DestTable.Rows(I)(COL_ROW_NUM) = (I + 1).ToString
                End If
            Next
        End If
    End Sub

    Private Function SelectGroupByInto(TableName As String, _
                                      SourceTable As DataTable, _
                                      FieldList As String, _
                                      Optional ByVal RowFilter As String = "", _
                                      Optional ByVal GroupBy As String = "", _
                                      Optional ByVal Rollup As Boolean = False) As DataTable
        '
        ' Selects data from one DataTable to another and performs various aggregate functions
        ' along the way. See InsertGroupByInto and ParseGroupByFieldList for supported aggregate functions.
        '
        Dim dt As DataTable = CreateGroupByTable(TableName, SourceTable, FieldList)
        InsertGroupByInto(dt, SourceTable, FieldList, RowFilter, GroupBy, Rollup)
        Return dt
    End Function

    Private Function GetTablePath(dt As DataTable, IncludeFullPath As Boolean) As String

        Dim str As String

        str = String.Format("a:{0}", dt.TableName)

        If IncludeFullPath Then

            Dim drColl As DataRelationCollection

            'Start the tag and move backwards
            str = String.Format("a:{0}", dt.TableName)

            drColl = dt.ParentRelations
            While True
                For Each dr As DataRelation In drColl
                    If Not dr.ParentTable.Equals(dt) Then
                        str = String.Format("a:{0}/{1}", dr.ParentTable.TableName, str)
                        dt = dr.ParentTable
                        Exit For
                    End If
                Next

                drColl = dt.ParentRelations
                If drColl Is Nothing OrElse drColl.Count = 0 Then Exit While

            End While

            'Add the dataset name to the front
            str = String.Format("a:{0}/{1}", dt.DataSet.DataSetName, str)
        End If

        Return str

    End Function

    Private Function ParseTable(Dt As DataTable, Nested As Boolean, OmitTags() As String, _
                                CharacterData() As String, IncludeEmptyTags As Boolean, Optional ByVal loopRows As Boolean = False) As String

        Dim strBuilder As New StringBuilder
        Dim strTag As String
        Dim includeFullPath As Boolean = True

        If Dt.Rows.Count > 0 Then
           
            If loopRows Then
                includeFullPath = False
            End If

            If Dt.Rows.Count > 1 Then
                loopRows = True
            Else
                loopRows = False
            End If

            'loop through each row if more than one row exists
            If loopRows Then
                strBuilder.AppendLine(String.Format("<xsl:for-each select=""{0}"">", GetTablePath(Dt, includeFullPath)))
            End If

            'Opening table tag
            strBuilder.AppendLine(String.Format("<{0}>", Dt.TableName))

            For Each dc As DataColumn In Dt.Columns

                If Not OmitTags Is Nothing AndAlso OmitTags.Length > 0 AndAlso Not OmitTags.Contains(dc.ColumnName) Then

                    If Not loopRows Then
                        strTag = String.Format("{0}/a:{1}", GetTablePath(Dt, includeFullPath), dc.ColumnName)
                    Else
                        strTag = String.Format("a:{0}", dc.ColumnName)
                    End If

                    If Not IncludeEmptyTags Then
                        strBuilder.AppendLine(String.Format("<xsl:if test=""{0}"">", strTag))
                    End If

                    If CharacterData.Contains(dc.ColumnName) Then
                        cDataFields += " " & String.Format("{0}/a:{1}", GetTablePath(Dt, True), dc.ColumnName)
                    End If

                    strBuilder.AppendLine(String.Format("  <xsl:element name=""{0}"">", dc.ColumnName))
                    strBuilder.AppendLine(String.Format("    <xsl:value-of select=""{0}""/>", strTag))
                    strBuilder.AppendLine("  </xsl:element>")

                    If Not IncludeEmptyTags Then
                        strBuilder.AppendLine("</xsl:if>")
                    End If

                End If

            Next

            If Nested Then
                Dim drColl As DataRelationCollection
                drColl = Dt.ChildRelations
                For Each dr As DataRelation In drColl
                    strBuilder.AppendLine(ParseTable(dr.ChildTable, True, OmitTags, CharacterData, IncludeEmptyTags, True))
                Next
            End If


            'Closing table tag
            strBuilder.AppendLine(String.Format("</{0}>", Dt.TableName))

            'Close the loop if needed
            If loopRows Then
                strBuilder.AppendLine(String.Format("</xsl:for-each>", GetTablePath(Dt, loopRows)))
            End If


            Return strBuilder.ToString

        End If

    End Function

#End Region

#Region "Aggregate Functions"

    Private Function ColumnEqual(A As Object, B As Object) As Boolean
        '
        ' Compares two values to determine if they are equal. Also compares DBNULL.Value.
        '
        ' NOTE: If your DataTable contains object fields, you must extend this
        ' function to handle them in a meaningful way if you intend to group on them.
        '
        If A Is DBNull.Value And B Is DBNull.Value Then Return True ' Both are DBNull.Value.
        If A Is DBNull.Value Or B Is DBNull.Value Then Return False ' Only one is DbNull.Value.
        Return A = B                                                ' Value type standard comparison
    End Function

    Private Function Min(A As Object, B As Object) As Object
        '
        ' Returns MIN of two values. DBNull is less than all others.
        '
        If A Is DBNull.Value Or B Is DBNull.Value Then Return DBNull.Value
        If A < B Then Return A Else Return B
    End Function

    Private Function Max(A As Object, B As Object) As Object
        '
        ' Returns Max of two values. DBNull is less than all others.
        '
        If A Is DBNull.Value Then Return B
        If B Is DBNull.Value Then Return A
        If A > B Then Return A Else Return B
    End Function

    Private Function Add(A As Object, B As Object) As Object
        '
        ' Adds two values. If one is DBNull, returns the other.
        '
        If A Is DBNull.Value Then Return B
        If B Is DBNull.Value Then Return A
        Return A + B
    End Function


#End Region

#Region "Public Functions"

    Public Function SelectDistinct(TableName As String, SourceTable As DataTable, FieldName As String) As DataTable

        Dim dt As New DataTable(TableName)
        dt.Columns.Add(FieldName, SourceTable.Columns(FieldName).DataType)

        Dim LastValue As Object = Nothing

        For Each dr As DataRow In SourceTable.Select("", FieldName)

            If LastValue Is Nothing OrElse Not (ColumnEqual(LastValue, dr(FieldName))) Then

                LastValue = dr(FieldName)
                dt.Rows.Add(New Object() {LastValue})
            End If
        Next

        If (Not ds Is Nothing) Then
            ds.Tables.Add(dt)
        End If

        Return dt

    End Function

    ''' <summary>
    ''' Group by Function.  Creates a new datatable based on group by selection criteria passed
    ''' </summary>
    ''' <param name="TableName">Name to assign the new table</param>
    ''' <param name="SourceTable">Original table containing the data to be grouped</param>
    ''' <param name="FieldList">Grouping field list.  Use the syntax:  fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GroupByWithCreate(TableName As String, _
                                  SourceTable As DataTable, _
                                  FieldList As String) As DataTable

        Return CreateGroupByTable(TableName, SourceTable, FieldList)

    End Function

    ''' <summary>
    ''' Group by Function.  Inserts records into an existing datatable based on group by selection criteria passed
    ''' </summary>
    ''' <param name="DestTable">The destination datatable to insert to</param>
    ''' <param name="SourceTable">Original table containing the data to be grouped</param>
    ''' <param name="FieldList">Grouping field list.  Use the syntax:  fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...</param>
    ''' <param name="RowFilter">A rowfilter to apply to the selection criteria</param>
    ''' <param name="SortBy">Sort criteria</param>
    ''' <param name="Rollup">True / False - whether to rollup results</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupByWithInsert(DestTable As DataTable, _
                             SourceTable As DataTable, _
                             FieldList As String, _
                             Optional ByVal RowFilter As String = "", _
                             Optional ByVal SortBy As String = "", _
                             Optional ByVal Rollup As Boolean = False)


        InsertGroupByInto(DestTable, SourceTable, FieldList, RowFilter, SortBy, Rollup)
        Return DestTable

    End Function


    ''' <summary>
    ''' Group by Function.  Selects records into a new datatable datatable based on group by selection criteria passed
    ''' </summary>
    ''' <param name="DestTable">The destination datatable to insert to</param>
    ''' <param name="SourceTable">Original table containing the data to be grouped</param>
    ''' <param name="FieldList">Grouping field list.  Use the syntax:  fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...</param>
    ''' <param name="RowFilter">A rowfilter to apply to the selection criteria</param>
    ''' <param name="SortBy">Sort criteria</param>
    ''' <param name="Rollup">True / False - whether to rollup results</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupBySelectandInsert(TableName As String, _
                                      SourceTable As DataTable, _
                                      FieldList As String, _
                                      Optional ByVal RowFilter As String = "", _
                                      Optional ByVal SortBy As String = "", _
                                      Optional ByVal Rollup As Boolean = False) As DataTable

        Return SelectGroupByInto(TableName, SourceTable, FieldList, RowFilter, SortBy, Rollup)

    End Function


    ''' <summary>
    ''' Parses an xslt stylesheet with a dataset
    ''' </summary>
    ''' <param name="StyleSheet">The string representation of the stylesheet to use</param>
    ''' <param name="Ds">The dataset to use for data</param>
    ''' <returns>An XML document as a string</returns>
    ''' <remarks></remarks>
    Public Function ParseXSLT(StyleSheet As String, Ds As DataSet) As String

        Dim encode As New UTF8Encoding
        Dim bytes As Byte() = encode.GetBytes(StyleSheet)
        Dim str As MemoryStream = New MemoryStream(bytes)

        Return ParseXSLT(str, Ds)

    End Function

    ''' <summary>
    ''' Parses an xslt stylesheet with a dataset
    ''' </summary>
    ''' <param name="StyleSheet">A memory stream representation of the stylesheet to use</param>
    ''' <param name="Ds">The dataset to use for data</param>
    ''' <returns>An XML document as a string</returns>
    ''' <remarks></remarks>
    Public Function ParseXSLT(StyleSheet As Stream, Ds As DataSet) As String

        Dim xmlr As XmlReader
        Dim xlTransform As New XslCompiledTransform
        Dim buffer As MemoryStream
        Dim sw As StreamWriter
        Dim chrs() As Byte
        Dim txtRead As TextReader
        Dim xmlDoc As New XmlDocument

        xmlr = XmlReader.Create(StyleSheet)

        'Create the transform object to process the stylesheet and data
        xlTransform.Load(xmlr)

        'Create the xmlwriter to accept the output of the transformation
        buffer = New MemoryStream
        sw = New StreamWriter(buffer)

        'Create the Reader for the data after setting the Dataset's namespace
        txtRead = New StringReader(Ds.GetXml)
        xmlDoc = New XmlDocument
        xmlDoc.Load(txtRead)

        xlTransform.Transform(xmlDoc, Nothing, sw)

        'Create the character array to write the buffer to (process of creating the string)
        chrs = buffer.ToArray

        Return Encoding.UTF8.GetString(chrs)

    End Function

    ''' <summary>
    '''  Parses a dataset by creating an XSLT Stlesheet dynamically.
    ''' </summary>
    ''' <param name="Ds">The Source data</param>
    ''' <param name="IncludeEmptyTags">Whether or not to include empty tags in the stylesheet</param>
    ''' <param name="OmitTags">Columns to omit from the output.  Comma Delimited</param>
    ''' <param name="Nested">Whether or not to nest the chile relations.  If false, and no starting table specified, then each table will be written individually to the output document.  If false and starting table exists, then only that table will be written</param>
    ''' <param name="StartingTable">The table at which to start the parsing</param>
    ''' <param name="cDataFields">Optional. A white-space separated list of elements whose text contents should be written as CDATA sections</param>
    ''' <returns>A string value of an xml document</returns>
    ''' <remarks></remarks>
    Public Function ParseXSLT(Ds As DataSet, IncludeEmptyTags As Boolean, Optional ByVal OmitTags As String = "", _
                                Optional ByVal Nested As Boolean = False, Optional ByVal StartingTable As String = "", _
                                Optional ByVal OutputMeth As XMLOutputMethod = XMLOutputMethod.xml, _
                                Optional ByVal charDataFields As String = "") As String

        Dim strBuilder As New StringBuilder
        Dim dt As DataTable
        Dim strDocumentText As String

        If Ds.Namespace = String.Empty Then Ds.Namespace = "http://tempuri.org/Assurant.xsd"

        Try

            Dim omit As String() = OmitTags.Split(New Char() {","})
            Dim cData As String() = charDataFields.Split(New Char() {" "})
            cDataFields = ""

            'If no starting table is specified, loop through each table in dataset, otherwise, set the datatable to the starting table specified
            If StartingTable = String.Empty Then
                For Each dt In Ds.Tables
                    strDocumentText += ParseTable(dt, Nested, omit, cData, IncludeEmptyTags)
                Next
            Else
                If Ds.Tables(StartingTable) Is Nothing Then
                    Throw New DALException("Specified table does not exist for xslt transformation")
                End If
                dt = Ds.Tables(StartingTable)
                strDocumentText = ParseTable(dt, Nested, omit, cData, IncludeEmptyTags)
            End If


            strBuilder.AppendLine("<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""")
            strBuilder.AppendLine(String.Format("xmlns:a=""{0}"" exclude-result-prefixes=""a"">", Ds.Namespace))
            strBuilder.AppendLine(String.Format("<xsl:output method=""{0}"" encoding=""UTF-8"" indent=""no"" {1}/>", _
                                                [Enum].GetName(GetType(XMLOutputMethod), OutputMeth), If(cDataFields.Length > 0, (String.Format("cdata-section-elements=""{0}""", cDataFields)), "").ToString))
            strBuilder.AppendLine("<xsl:template match=""/"">")

            'Open document with the dataset name tag
            strBuilder.AppendLine(String.Format("<{0}>", Ds.DataSetName))

            'Add the document data
            strBuilder.AppendLine(strDocumentText)

            'Close document with the dataset name tag
            strBuilder.AppendLine(String.Format("</{0}>", Ds.DataSetName))

            strBuilder.AppendLine("</xsl:template>")
            strBuilder.AppendLine("</xsl:stylesheet>")

            'Now that stylesheet is built, parse it and return the result
            Return ParseXSLT(strBuilder.ToString, Ds)

        Catch dex As DALException
            Throw dex
        Catch ex As Exception
            Throw New DALException("XSLT Transformation error", ex)
        End Try

    End Function

#End Region


End Class
