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
 Global.System.Xml.Serialization.XmlRootAttribute("GetComunasDs"), _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")> _
Partial Public Class GetComunasDs
    Inherits Global.System.Data.DataSet

    Private tableGetComunas As GetComunasDataTable

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
            If (Not (ds.Tables("GetComunas")) Is Nothing) Then
                MyBase.Tables.Add(New GetComunasDataTable(ds.Tables("GetComunas")))
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
    Public ReadOnly Property GetComunas() As GetComunasDataTable
        Get
            Return Me.tableGetComunas
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
        Dim cln As GetComunasDs = CType(MyBase.Clone, GetComunasDs)
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
            If (Not (ds.Tables("GetComunas")) Is Nothing) Then
                MyBase.Tables.Add(New GetComunasDataTable(ds.Tables("GetComunas")))
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
        Me.tableGetComunas = CType(MyBase.Tables("GetComunas"), GetComunasDataTable)
        If (initTable = True) Then
            If (Not (Me.tableGetComunas) Is Nothing) Then
                Me.tableGetComunas.InitVars()
            End If
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitClass()
        Me.DataSetName = "GetComunasDs"
        Me.Prefix = ""
        Me.EnforceConstraints = True
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableGetComunas = New GetComunasDataTable
        MyBase.Tables.Add(Me.tableGetComunas)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Function ShouldSerializeGetComunas() As Boolean
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
        Dim ds As GetComunasDs = New GetComunasDs
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

    Public Delegate Sub GetComunasRowChangeEventHandler(ByVal sender As Object, ByVal e As GetComunasRowChangeEvent)

    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
     Global.System.Serializable(), _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")> _
    Partial Public Class GetComunasDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable

        Private columncountry_code As Global.System.Data.DataColumn

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New()
            MyBase.New()
            Me.TableName = "GetComunas"
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
        Public ReadOnly Property country_codeColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columncountry_code
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
        Default Public ReadOnly Property Item(ByVal index As Integer) As GetComunasRow
            Get
                Return CType(Me.Rows(index), GetComunasRow)
            End Get
        End Property

        Public Event GetComunasRowChanging As GetComunasRowChangeEventHandler

        Public Event GetComunasRowChanged As GetComunasRowChangeEventHandler

        Public Event GetComunasRowDeleting As GetComunasRowChangeEventHandler

        Public Event GetComunasRowDeleted As GetComunasRowChangeEventHandler

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Sub AddGetComunasRow(ByVal row As GetComunasRow)
            Me.Rows.Add(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Function AddGetComunasRow(ByVal country_code As String) As GetComunasRow
            Dim rowGetComunasRow As GetComunasRow = CType(Me.NewRow, GetComunasRow)
            Dim columnValuesArray() As Object = New Object() {country_code}
            rowGetComunasRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowGetComunasRow)
            Return rowGetComunasRow
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As GetComunasDataTable = CType(MyBase.Clone, GetComunasDataTable)
            cln.InitVars()
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New GetComunasDataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub InitVars()
            Me.columncountry_code = MyBase.Columns("country_code")
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitClass()
            Me.columncountry_code = New Global.System.Data.DataColumn("country_code", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columncountry_code)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function NewGetComunasRow() As GetComunasRow
            Return CType(Me.NewRow, GetComunasRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New GetComunasRow(builder)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(GetComunasRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.GetComunasRowChangedEvent) Is Nothing) Then
                RaiseEvent GetComunasRowChanged(Me, New GetComunasRowChangeEvent(CType(e.Row, GetComunasRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.GetComunasRowChangingEvent) Is Nothing) Then
                RaiseEvent GetComunasRowChanging(Me, New GetComunasRowChangeEvent(CType(e.Row, GetComunasRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.GetComunasRowDeletedEvent) Is Nothing) Then
                RaiseEvent GetComunasRowDeleted(Me, New GetComunasRowChangeEvent(CType(e.Row, GetComunasRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.GetComunasRowDeletingEvent) Is Nothing) Then
                RaiseEvent GetComunasRowDeleting(Me, New GetComunasRowChangeEvent(CType(e.Row, GetComunasRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub RemoveGetComunasRow(ByVal row As GetComunasRow)
            Me.Rows.Remove(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As GetComunasDs = New GetComunasDs
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
            attribute2.FixedValue = "GetComunasDataTable"
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
    Partial Public Class GetComunasRow
        Inherits Global.System.Data.DataRow

        Private tableGetComunas As GetComunasDataTable

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableGetComunas = CType(Me.Table, GetComunasDataTable)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property country_code() As String
            Get
                Try
                    Return CType(Me(Me.tableGetComunas.country_codeColumn), String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'country_code' in table 'GetComunas' is DBNull.", e)
                End Try
            End Get
            Set(ByVal value As String)
                Me(Me.tableGetComunas.country_codeColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function Iscountry_codeNull() As Boolean
            Return Me.IsNull(Me.tableGetComunas.country_codeColumn)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub Setcountry_codeNull()
            Me(Me.tableGetComunas.country_codeColumn) = Global.System.Convert.DBNull
        End Sub
    End Class

    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")> _
    Public Class GetComunasRowChangeEvent
        Inherits Global.System.EventArgs

        Private eventRow As GetComunasRow

        Private eventAction As Global.System.Data.DataRowAction

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New(ByVal row As GetComunasRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New()
            Me.eventRow = row
            Me.eventAction = action
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property Row() As GetComunasRow
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
