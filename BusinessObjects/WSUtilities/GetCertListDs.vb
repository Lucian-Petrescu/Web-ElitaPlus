﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


'
'This source code was auto-generated by xsd, Version=2.0.50727.3038.
'

'''<summary>
'''Represents a strongly typed in-memory cache of data.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
 Global.System.Serializable(), _
 Global.System.ComponentModel.DesignerCategoryAttribute("code"), _
 Global.System.ComponentModel.ToolboxItem(True), _
 Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema"), _
 Global.System.Xml.Serialization.XmlRootAttribute("GetCertListDs"), _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")> _
Partial Public Class GetCertListDs
    Inherits Global.System.Data.DataSet

    Private tableGetCertList As GetCertListDataTable

    Private _schemaSerializationMode As Global.System.Data.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Sub New()
        MyBase.New()
        Me.BeginInit()
        Me.InitClass()
        Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler MyBase.Relations.CollectionChanged, schemaChangedHandler
        Me.EndInit()
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context, False)
        If (Me.IsBinarySerialized(info, context) = True) Then
            Me.InitVars(False)
            Dim schemaChangedHandler1 As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
            AddHandler Me.Tables.CollectionChanged, schemaChangedHandler1
            AddHandler Me.Relations.CollectionChanged, schemaChangedHandler1
            Return
        End If
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(String)), String)
        If (Me.DetermineSchemaSerializationMode(info, context) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
            Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet
            ds.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("GetCertList")) Is Nothing) Then
                MyBase.Tables.Add(New GetCertListDataTable(ds.Tables("GetCertList")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
            Me.InitVars()
        Else
            Me.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.Browsable(False), _
     Global.System.ComponentModel.DesignerSerializationVisibility(Global.System.ComponentModel.DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property GetCertList() As GetCertListDataTable
        Get
            Return Me.tableGetCertList
        End Get
    End Property

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.BrowsableAttribute(True), _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Visible)> _
    Public Overrides Property SchemaSerializationMode() As Global.System.Data.SchemaSerializationMode
        Get
            Return Me._schemaSerializationMode
        End Get
        Set(ByVal value As Global.System.Data.SchemaSerializationMode)
            Me._schemaSerializationMode = value
        End Set
    End Property

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property Tables() As Global.System.Data.DataTableCollection
        Get
            Return MyBase.Tables
        End Get
    End Property

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property Relations() As Global.System.Data.DataRelationCollection
        Get
            Return MyBase.Relations
        End Get
    End Property

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Overrides Sub InitializeDerivedDataSet()
        Me.BeginInit()
        Me.InitClass()
        Me.EndInit()
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Overrides Function Clone() As Global.System.Data.DataSet
        Dim cln As GetCertListDs = CType(MyBase.Clone, GetCertListDs)
        cln.InitVars()
        cln.SchemaSerializationMode = Me.SchemaSerializationMode
        Return cln
    End Function

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return False
    End Function

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return False
    End Function

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As Global.System.Xml.XmlReader)
        If (Me.DetermineSchemaSerializationMode(reader) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
            Me.Reset()
            Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet
            ds.ReadXml(reader)
            If (Not (ds.Tables("GetCertList")) Is Nothing) Then
                MyBase.Tables.Add(New GetCertListDataTable(ds.Tables("GetCertList")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
            Me.InitVars()
        Else
            Me.ReadXml(reader)
            Me.InitVars()
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Protected Overrides Function GetSchemaSerializable() As Global.System.Xml.Schema.XmlSchema
        Dim stream As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
        Me.WriteXmlSchema(New Global.System.Xml.XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return Global.System.Xml.Schema.XmlSchema.Read(New Global.System.Xml.XmlTextReader(stream), Nothing)
    End Function

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Friend Overloads Sub InitVars()
        Me.InitVars(True)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Friend Overloads Sub InitVars(ByVal initTable As Boolean)
        Me.tableGetCertList = CType(MyBase.Tables("GetCertList"), GetCertListDataTable)
        If (initTable = True) Then
            If (Not (Me.tableGetCertList) Is Nothing) Then
                Me.tableGetCertList.InitVars()
            End If
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitClass()
        Me.DataSetName = "GetCertListDs"
        Me.Prefix = ""
        Me.EnforceConstraints = True
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableGetCertList = New GetCertListDataTable
        MyBase.Tables.Add(Me.tableGetCertList)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Function ShouldSerializeGetCertList() As Boolean
        Return False
    End Function

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As Global.System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = Global.System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars()
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Public Shared Function GetTypedDataSetSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
        Dim ds As GetCertListDs = New GetCertListDs
        Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
        Dim any As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable
        If xs.Contains(dsSchema.TargetNamespace) Then
            Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
            Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
            Try
                Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                dsSchema.Write(s1)
                Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator
                Do While schemas.MoveNext
                    schema = CType(schemas.Current, Global.System.Xml.Schema.XmlSchema)
                    s2.SetLength(0)
                    schema.Write(s2)
                    If (s1.Length = s2.Length) Then
                        s1.Position = 0
                        s2.Position = 0

                        Do While ((s1.Position <> s1.Length) _
                                    AndAlso (s1.ReadByte = s2.ReadByte))


                        Loop
                        If (s1.Position = s1.Length) Then
                            Return type
                        End If
                    End If

                Loop
            Finally
                If (Not (s1) Is Nothing) Then
                    s1.Close()
                End If
                If (Not (s2) Is Nothing) Then
                    s2.Close()
                End If
            End Try
        End If
        xs.Add(dsSchema)
        Return type
    End Function

    Public Delegate Sub GetCertListRowChangeEventHandler(ByVal sender As Object, ByVal e As GetCertListRowChangeEvent)

    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
     Global.System.Serializable(), _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")> _
    Partial Public Class GetCertListDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable

        Private columnDealerCode As Global.System.Data.DataColumn

        Private columnBranchCode As Global.System.Data.DataColumn

        Private columnCertificateNumber As Global.System.Data.DataColumn

        Private columnCustomerName As Global.System.Data.DataColumn

        Private columnEmail As Global.System.Data.DataColumn

        Private columnSortBy As Global.System.Data.DataColumn

        Private columnSortOrder As Global.System.Data.DataColumn

        Private columnForCancellation As Global.System.Data.DataColumn

        Private columnRequestNumber As Global.System.Data.DataColumn

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New()
            MyBase.New()
            Me.TableName = "GetCertList"
            Me.BeginInit()
            Me.InitClass()
            Me.EndInit()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub New(ByVal table As Global.System.Data.DataTable)
            MyBase.New()
            Me.TableName = table.TableName
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
            Me.InitVars()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property DealerCodeColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnDealerCode
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property BranchCodeColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnBranchCode
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property CertificateNumberColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCertificateNumber
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property CustomerNameColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCustomerName
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property EmailColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnEmail
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property SortByColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnSortBy
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property SortOrderColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnSortOrder
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property ForCancellationColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnForCancellation
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property RequestNumberColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnRequestNumber
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.ComponentModel.Browsable(False)> _
        Public ReadOnly Property Count() As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Default Public ReadOnly Property Item(ByVal index As Integer) As GetCertListRow
            Get
                Return CType(Me.Rows(index), GetCertListRow)
            End Get
        End Property

        Public Event GetCertListRowChanging As GetCertListRowChangeEventHandler

        Public Event GetCertListRowChanged As GetCertListRowChangeEventHandler

        Public Event GetCertListRowDeleting As GetCertListRowChangeEventHandler

        Public Event GetCertListRowDeleted As GetCertListRowChangeEventHandler

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Sub AddGetCertListRow(ByVal row As GetCertListRow)
            Me.Rows.Add(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Function AddGetCertListRow(ByVal DealerCode As String, ByVal BranchCode As String, ByVal CertificateNumber As String, ByVal CustomerName As String, ByVal Email As String, ByVal SortBy As Integer, ByVal SortOrder As Integer, ByVal ForCancellation As String, ByVal RequestNumber As Integer) As GetCertListRow
            Dim rowGetCertListRow As GetCertListRow = CType(Me.NewRow, GetCertListRow)
            Dim columnValuesArray() As Object = New Object() {DealerCode, BranchCode, CertificateNumber, CustomerName, Email, SortBy, SortOrder, ForCancellation, RequestNumber}
            rowGetCertListRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowGetCertListRow)
            Return rowGetCertListRow
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As GetCertListDataTable = CType(MyBase.Clone, GetCertListDataTable)
            cln.InitVars()
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New GetCertListDataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub InitVars()
            Me.columnDealerCode = MyBase.Columns("DealerCode")
            Me.columnBranchCode = MyBase.Columns("BranchCode")
            Me.columnCertificateNumber = MyBase.Columns("CertificateNumber")
            Me.columnCustomerName = MyBase.Columns("CustomerName")
            Me.columnEmail = MyBase.Columns("Email")
            Me.columnSortBy = MyBase.Columns("SortBy")
            Me.columnSortOrder = MyBase.Columns("SortOrder")
            Me.columnForCancellation = MyBase.Columns("ForCancellation")
            Me.columnRequestNumber = MyBase.Columns("RequestNumber")
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitClass()
            Me.columnDealerCode = New Global.System.Data.DataColumn("DealerCode", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnDealerCode)
            Me.columnBranchCode = New Global.System.Data.DataColumn("BranchCode", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnBranchCode)
            Me.columnCertificateNumber = New Global.System.Data.DataColumn("CertificateNumber", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCertificateNumber)
            Me.columnCustomerName = New Global.System.Data.DataColumn("CustomerName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCustomerName)
            Me.columnEmail = New Global.System.Data.DataColumn("Email", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnEmail)
            Me.columnSortBy = New Global.System.Data.DataColumn("SortBy", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnSortBy)
            Me.columnSortOrder = New Global.System.Data.DataColumn("SortOrder", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnSortOrder)
            Me.columnForCancellation = New Global.System.Data.DataColumn("ForCancellation", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnForCancellation)
            Me.columnRequestNumber = New Global.System.Data.DataColumn("RequestNumber", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnRequestNumber)
            Me.columnDealerCode.AllowDBNull = False
            Me.columnSortBy.AllowDBNull = False
            Me.columnSortOrder.AllowDBNull = False
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function NewGetCertListRow() As GetCertListRow
            Return CType(Me.NewRow, GetCertListRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New GetCertListRow(builder)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(GetCertListRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.GetCertListRowChangedEvent) Is Nothing) Then
                RaiseEvent GetCertListRowChanged(Me, New GetCertListRowChangeEvent(CType(e.Row, GetCertListRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.GetCertListRowChangingEvent) Is Nothing) Then
                RaiseEvent GetCertListRowChanging(Me, New GetCertListRowChangeEvent(CType(e.Row, GetCertListRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.GetCertListRowDeletedEvent) Is Nothing) Then
                RaiseEvent GetCertListRowDeleted(Me, New GetCertListRowChangeEvent(CType(e.Row, GetCertListRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.GetCertListRowDeletingEvent) Is Nothing) Then
                RaiseEvent GetCertListRowDeleting(Me, New GetCertListRowChangeEvent(CType(e.Row, GetCertListRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub RemoveGetCertListRow(ByVal row As GetCertListRow)
            Me.Rows.Remove(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As GetCertListDs = New GetCertListDs
            Dim any1 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
            any1.Namespace = "http://www.w3.org/2001/XMLSchema"
            any1.MinOccurs = New Decimal(0)
            any1.MaxOccurs = Decimal.MaxValue
            any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any1)
            Dim any2 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
            any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
            any2.MinOccurs = New Decimal(1)
            any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any2)
            Dim attribute1 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute
            attribute1.Name = "namespace"
            attribute1.FixedValue = ds.Namespace
            type.Attributes.Add(attribute1)
            Dim attribute2 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute
            attribute2.Name = "tableTypeName"
            attribute2.FixedValue = "GetCertListDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable
            If xs.Contains(dsSchema.TargetNamespace) Then
                Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
                Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
                Try
                    Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                    dsSchema.Write(s1)
                    Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator
                    Do While schemas.MoveNext
                        schema = CType(schemas.Current, Global.System.Xml.Schema.XmlSchema)
                        s2.SetLength(0)
                        schema.Write(s2)
                        If (s1.Length = s2.Length) Then
                            s1.Position = 0
                            s2.Position = 0

                            Do While ((s1.Position <> s1.Length) _
                                        AndAlso (s1.ReadByte = s2.ReadByte))


                            Loop
                            If (s1.Position = s1.Length) Then
                                Return type
                            End If
                        End If

                    Loop
                Finally
                    If (Not (s1) Is Nothing) Then
                        s1.Close()
                    End If
                    If (Not (s2) Is Nothing) Then
                        s2.Close()
                    End If
                End Try
            End If
            xs.Add(dsSchema)
            Return type
        End Function
    End Class

    '''<summary>
    '''Represents strongly named DataRow class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")> _
    Partial Public Class GetCertListRow
        Inherits Global.System.Data.DataRow

        Private tableGetCertList As GetCertListDataTable

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableGetCertList = CType(Me.Table, GetCertListDataTable)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property DealerCode() As String
            Get
                Return CType(Me(Me.tableGetCertList.DealerCodeColumn), String)
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.DealerCodeColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property BranchCode() As String
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.BranchCodeColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'BranchCode' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.BranchCodeColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property CertificateNumber() As String
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.CertificateNumberColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'CertificateNumber' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.CertificateNumberColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property CustomerName() As String
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.CustomerNameColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'CustomerName' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.CustomerNameColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property Email() As String
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.EmailColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Email' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.EmailColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property SortBy() As Integer
            Get
                Return CType(Me(Me.tableGetCertList.SortByColumn), Integer)
            End Get
            Set(ByVal value As Integer)
                Me(Me.tableGetCertList.SortByColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property SortOrder() As Integer
            Get
                Return CType(Me(Me.tableGetCertList.SortOrderColumn), Integer)
            End Get
            Set(ByVal value As Integer)
                Me(Me.tableGetCertList.SortOrderColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property ForCancellation() As String
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.ForCancellationColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'ForCancellation' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetCertList.ForCancellationColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property RequestNumber() As Integer
            Get
                Try
                    Return CType(Me(Me.tableGetCertList.RequestNumberColumn), Integer)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'RequestNumber' in table 'GetCertList' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As Integer)
                Me(Me.tableGetCertList.RequestNumberColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsBranchCodeNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.BranchCodeColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetBranchCodeNull()
            Me(Me.tableGetCertList.BranchCodeColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsCertificateNumberNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.CertificateNumberColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetCertificateNumberNull()
            Me(Me.tableGetCertList.CertificateNumberColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsCustomerNameNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.CustomerNameColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetCustomerNameNull()
            Me(Me.tableGetCertList.CustomerNameColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsEmailNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.EmailColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetEmailNull()
            Me(Me.tableGetCertList.EmailColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsForCancellationNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.ForCancellationColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetForCancellationNull()
            Me(Me.tableGetCertList.ForCancellationColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsRequestNumberNull() As Boolean
            Return Me.IsNull(Me.tableGetCertList.RequestNumberColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetRequestNumberNull()
            Me(Me.tableGetCertList.RequestNumberColumn) = Global.System.Convert.DBNull
        End Sub
    End Class

    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")> _
    Public Class GetCertListRowChangeEvent
        Inherits Global.System.EventArgs

        Private eventRow As GetCertListRow

        Private eventAction As Global.System.Data.DataRowAction

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New(ByVal row As GetCertListRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New()
            Me.eventRow = row
            Me.eventAction = action
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property Row() As GetCertListRow
            Get
                Return Me.eventRow
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property Action() As Global.System.Data.DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class
