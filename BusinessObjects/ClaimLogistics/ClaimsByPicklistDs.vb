﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


'
'This source code was auto-generated by xsd, Version=2.0.50727.1432.
'

'''<summary>
'''Represents a strongly typed in-memory cache of data.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
 Global.System.Serializable(), _
 Global.System.ComponentModel.DesignerCategoryAttribute("code"), _
 Global.System.ComponentModel.ToolboxItem(True), _
 Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema"), _
 Global.System.Xml.Serialization.XmlRootAttribute("ClaimsByPicklistDs"), _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")> _
Partial Public Class ClaimsByPicklistDs
    Inherits Global.System.Data.DataSet

    Private tableClaimsByPicklist As ClaimsByPicklistDataTable

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
            If (Not (ds.Tables("ClaimsByPicklist")) Is Nothing) Then
                MyBase.Tables.Add(New ClaimsByPicklistDataTable(ds.Tables("ClaimsByPicklist")))
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
    Public ReadOnly Property ClaimsByPicklist() As ClaimsByPicklistDataTable
        Get
            Return Me.tableClaimsByPicklist
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
        Dim cln As ClaimsByPicklistDs = CType(MyBase.Clone, ClaimsByPicklistDs)
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
            If (Not (ds.Tables("ClaimsByPicklist")) Is Nothing) Then
                MyBase.Tables.Add(New ClaimsByPicklistDataTable(ds.Tables("ClaimsByPicklist")))
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
        Me.tableClaimsByPicklist = CType(MyBase.Tables("ClaimsByPicklist"), ClaimsByPicklistDataTable)
        If (initTable = True) Then
            If (Not (Me.tableClaimsByPicklist) Is Nothing) Then
                Me.tableClaimsByPicklist.InitVars()
            End If
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitClass()
        Me.DataSetName = "ClaimsByPicklistDs"
        Me.Prefix = ""
        Me.EnforceConstraints = True
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableClaimsByPicklist = New ClaimsByPicklistDataTable
        MyBase.Tables.Add(Me.tableClaimsByPicklist)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Function ShouldSerializeClaimsByPicklist() As Boolean
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
        Dim ds As ClaimsByPicklistDs = New ClaimsByPicklistDs
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

    Public Delegate Sub ClaimsByPicklistRowChangeEventHandler(ByVal sender As Object, ByVal e As ClaimsByPicklistRowChangeEvent)

    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
     Global.System.Serializable(), _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")> _
    Partial Public Class ClaimsByPicklistDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable

        Private columnPICK_LIST_NUMBER As Global.System.Data.DataColumn

        Private columnSTORE_NUMBER As Global.System.Data.DataColumn

        Private columnSERVICE_CENTER_CODE As Global.System.Data.DataColumn

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New()
            MyBase.New()
            Me.TableName = "ClaimsByPicklist"
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
        Public ReadOnly Property PICK_LIST_NUMBERColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnPICK_LIST_NUMBER
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property STORE_NUMBERColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnSTORE_NUMBER
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property SERVICE_CENTER_CODEColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnSERVICE_CENTER_CODE
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
        Default Public ReadOnly Property Item(ByVal index As Integer) As ClaimsByPicklistRow
            Get
                Return CType(Me.Rows(index), ClaimsByPicklistRow)
            End Get
        End Property

        Public Event ClaimsByPicklistRowChanging As ClaimsByPicklistRowChangeEventHandler

        Public Event ClaimsByPicklistRowChanged As ClaimsByPicklistRowChangeEventHandler

        Public Event ClaimsByPicklistRowDeleting As ClaimsByPicklistRowChangeEventHandler

        Public Event ClaimsByPicklistRowDeleted As ClaimsByPicklistRowChangeEventHandler

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Sub AddClaimsByPicklistRow(ByVal row As ClaimsByPicklistRow)
            Me.Rows.Add(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Function AddClaimsByPicklistRow(ByVal PICK_LIST_NUMBER As String, ByVal STORE_NUMBER As String, ByVal SERVICE_CENTER_CODE As String) As ClaimsByPicklistRow
            Dim rowClaimsByPicklistRow As ClaimsByPicklistRow = CType(Me.NewRow, ClaimsByPicklistRow)
            Dim columnValuesArray() As Object = New Object() {PICK_LIST_NUMBER, STORE_NUMBER, SERVICE_CENTER_CODE}
            rowClaimsByPicklistRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowClaimsByPicklistRow)
            Return rowClaimsByPicklistRow
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As ClaimsByPicklistDataTable = CType(MyBase.Clone, ClaimsByPicklistDataTable)
            cln.InitVars()
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New ClaimsByPicklistDataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub InitVars()
            Me.columnPICK_LIST_NUMBER = MyBase.Columns("PICK_LIST_NUMBER")
            Me.columnSTORE_NUMBER = MyBase.Columns("STORE_NUMBER")
            Me.columnSERVICE_CENTER_CODE = MyBase.Columns("SERVICE_CENTER_CODE")
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitClass()
            Me.columnPICK_LIST_NUMBER = New Global.System.Data.DataColumn("PICK_LIST_NUMBER", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnPICK_LIST_NUMBER)
            Me.columnSTORE_NUMBER = New Global.System.Data.DataColumn("STORE_NUMBER", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnSTORE_NUMBER)
            Me.columnSERVICE_CENTER_CODE = New Global.System.Data.DataColumn("SERVICE_CENTER_CODE", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnSERVICE_CENTER_CODE)
            Me.columnPICK_LIST_NUMBER.AllowDBNull = False
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function NewClaimsByPicklistRow() As ClaimsByPicklistRow
            Return CType(Me.NewRow, ClaimsByPicklistRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New ClaimsByPicklistRow(builder)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(ClaimsByPicklistRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.ClaimsByPicklistRowChangedEvent) Is Nothing) Then
                RaiseEvent ClaimsByPicklistRowChanged(Me, New ClaimsByPicklistRowChangeEvent(CType(e.Row, ClaimsByPicklistRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.ClaimsByPicklistRowChangingEvent) Is Nothing) Then
                RaiseEvent ClaimsByPicklistRowChanging(Me, New ClaimsByPicklistRowChangeEvent(CType(e.Row, ClaimsByPicklistRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.ClaimsByPicklistRowDeletedEvent) Is Nothing) Then
                RaiseEvent ClaimsByPicklistRowDeleted(Me, New ClaimsByPicklistRowChangeEvent(CType(e.Row, ClaimsByPicklistRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.ClaimsByPicklistRowDeletingEvent) Is Nothing) Then
                RaiseEvent ClaimsByPicklistRowDeleting(Me, New ClaimsByPicklistRowChangeEvent(CType(e.Row, ClaimsByPicklistRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub RemoveClaimsByPicklistRow(ByVal row As ClaimsByPicklistRow)
            Me.Rows.Remove(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As ClaimsByPicklistDs = New ClaimsByPicklistDs
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
            attribute2.FixedValue = "ClaimsByPicklistDataTable"
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
    Partial Public Class ClaimsByPicklistRow
        Inherits Global.System.Data.DataRow

        Private tableClaimsByPicklist As ClaimsByPicklistDataTable

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableClaimsByPicklist = CType(Me.Table, ClaimsByPicklistDataTable)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property PICK_LIST_NUMBER() As String
            Get
                Return CType(Me(Me.tableClaimsByPicklist.PICK_LIST_NUMBERColumn), String)
            End Get
            Set(ByVal value As String)
                Me(Me.tableClaimsByPicklist.PICK_LIST_NUMBERColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property STORE_NUMBER() As String
            Get
                Try
                    Return CType(Me(Me.tableClaimsByPicklist.STORE_NUMBERColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'STORE_NUMBER' in table 'ClaimsByPicklist' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableClaimsByPicklist.STORE_NUMBERColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property SERVICE_CENTER_CODE() As String
            Get
                Try
                    Return CType(Me(Me.tableClaimsByPicklist.SERVICE_CENTER_CODEColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'SERVICE_CENTER_CODE' in table 'ClaimsByPicklist' is DBNull." & _
                            "", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableClaimsByPicklist.SERVICE_CENTER_CODEColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsSTORE_NUMBERNull() As Boolean
            Return Me.IsNull(Me.tableClaimsByPicklist.STORE_NUMBERColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetSTORE_NUMBERNull()
            Me(Me.tableClaimsByPicklist.STORE_NUMBERColumn) = Global.System.Convert.DBNull
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function IsSERVICE_CENTER_CODENull() As Boolean
            Return Me.IsNull(Me.tableClaimsByPicklist.SERVICE_CENTER_CODEColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub SetSERVICE_CENTER_CODENull()
            Me(Me.tableClaimsByPicklist.SERVICE_CENTER_CODEColumn) = Global.System.Convert.DBNull
        End Sub
    End Class

    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")> _
    Public Class ClaimsByPicklistRowChangeEvent
        Inherits Global.System.EventArgs

        Private eventRow As ClaimsByPicklistRow

        Private eventAction As Global.System.Data.DataRowAction

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New(ByVal row As ClaimsByPicklistRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New()
            Me.eventRow = row
            Me.eventAction = action
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property Row() As ClaimsByPicklistRow
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
