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
 Global.System.Xml.Serialization.XmlRootAttribute("OpenMobileVerifyClaimDs"), _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")> _
Partial Public Class OpenMobileVerifyClaimDs
    Inherits Global.System.Data.DataSet

    Private tableOpenMobileVerifyClaim As OpenMobileVerifyClaimDataTable

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
            If (Not (ds.Tables("OpenMobileVerifyClaim")) Is Nothing) Then
                MyBase.Tables.Add(New OpenMobileVerifyClaimDataTable(ds.Tables("OpenMobileVerifyClaim")))
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
    Public ReadOnly Property OpenMobileVerifyClaim() As OpenMobileVerifyClaimDataTable
        Get
            Return Me.tableOpenMobileVerifyClaim
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
        Dim cln As OpenMobileVerifyClaimDs = CType(MyBase.Clone, OpenMobileVerifyClaimDs)
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
            If (Not (ds.Tables("OpenMobileVerifyClaim")) Is Nothing) Then
                MyBase.Tables.Add(New OpenMobileVerifyClaimDataTable(ds.Tables("OpenMobileVerifyClaim")))
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
        Me.tableOpenMobileVerifyClaim = CType(MyBase.Tables("OpenMobileVerifyClaim"), OpenMobileVerifyClaimDataTable)
        If (initTable = True) Then
            If (Not (Me.tableOpenMobileVerifyClaim) Is Nothing) Then
                Me.tableOpenMobileVerifyClaim.InitVars()
            End If
        End If
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Sub InitClass()
        Me.DataSetName = "OpenMobileVerifyClaimDs"
        Me.Prefix = ""
        Me.EnforceConstraints = True
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableOpenMobileVerifyClaim = New OpenMobileVerifyClaimDataTable
        MyBase.Tables.Add(Me.tableOpenMobileVerifyClaim)
    End Sub

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
    Private Function ShouldSerializeOpenMobileVerifyClaim() As Boolean
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
        Dim ds As OpenMobileVerifyClaimDs = New OpenMobileVerifyClaimDs
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

    Public Delegate Sub OpenMobileVerifyClaimRowChangeEventHandler(ByVal sender As Object, ByVal e As OpenMobileVerifyClaimRowChangeEvent)

    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), _
     Global.System.Serializable(), _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")> _
    Partial Public Class OpenMobileVerifyClaimDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable

        Private columnCell_Number As Global.System.Data.DataColumn

        Private columnESN As Global.System.Data.DataColumn

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New()
            MyBase.New()
            Me.TableName = "OpenMobileVerifyClaim"
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
        Public ReadOnly Property Cell_NumberColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCell_Number
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property ESNColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnESN
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
        Default Public ReadOnly Property Item(ByVal index As Integer) As OpenMobileVerifyClaimRow
            Get
                Return CType(Me.Rows(index), OpenMobileVerifyClaimRow)
            End Get
        End Property

        Public Event OpenMobileVerifyClaimRowChanging As OpenMobileVerifyClaimRowChangeEventHandler

        Public Event OpenMobileVerifyClaimRowChanged As OpenMobileVerifyClaimRowChangeEventHandler

        Public Event OpenMobileVerifyClaimRowDeleting As OpenMobileVerifyClaimRowChangeEventHandler

        Public Event OpenMobileVerifyClaimRowDeleted As OpenMobileVerifyClaimRowChangeEventHandler

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Sub AddOpenMobileVerifyClaimRow(ByVal row As OpenMobileVerifyClaimRow)
            Me.Rows.Add(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overloads Function AddOpenMobileVerifyClaimRow(ByVal Cell_Number As String, ByVal ESN As String) As OpenMobileVerifyClaimRow
            Dim rowOpenMobileVerifyClaimRow As OpenMobileVerifyClaimRow = CType(Me.NewRow, OpenMobileVerifyClaimRow)
            Dim columnValuesArray() As Object = New Object() {Cell_Number, ESN}
            rowOpenMobileVerifyClaimRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowOpenMobileVerifyClaimRow)
            Return rowOpenMobileVerifyClaimRow
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As OpenMobileVerifyClaimDataTable = CType(MyBase.Clone, OpenMobileVerifyClaimDataTable)
            cln.InitVars()
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New OpenMobileVerifyClaimDataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub InitVars()
            Me.columnCell_Number = MyBase.Columns("Cell_Number")
            Me.columnESN = MyBase.Columns("ESN")
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Private Sub InitClass()
            Me.columnCell_Number = New Global.System.Data.DataColumn("Cell_Number", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCell_Number)
            Me.columnESN = New Global.System.Data.DataColumn("ESN", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnESN)
            Me.columnCell_Number.AllowDBNull = False
            Me.columnESN.AllowDBNull = False
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Function NewOpenMobileVerifyClaimRow() As OpenMobileVerifyClaimRow
            Return CType(Me.NewRow, OpenMobileVerifyClaimRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New OpenMobileVerifyClaimRow(builder)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(OpenMobileVerifyClaimRow)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.OpenMobileVerifyClaimRowChangedEvent) Is Nothing) Then
                RaiseEvent OpenMobileVerifyClaimRowChanged(Me, New OpenMobileVerifyClaimRowChangeEvent(CType(e.Row, OpenMobileVerifyClaimRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.OpenMobileVerifyClaimRowChangingEvent) Is Nothing) Then
                RaiseEvent OpenMobileVerifyClaimRowChanging(Me, New OpenMobileVerifyClaimRowChangeEvent(CType(e.Row, OpenMobileVerifyClaimRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.OpenMobileVerifyClaimRowDeletedEvent) Is Nothing) Then
                RaiseEvent OpenMobileVerifyClaimRowDeleted(Me, New OpenMobileVerifyClaimRowChangeEvent(CType(e.Row, OpenMobileVerifyClaimRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.OpenMobileVerifyClaimRowDeletingEvent) Is Nothing) Then
                RaiseEvent OpenMobileVerifyClaimRowDeleting(Me, New OpenMobileVerifyClaimRowChangeEvent(CType(e.Row, OpenMobileVerifyClaimRow), e.Action))
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub RemoveOpenMobileVerifyClaimRow(ByVal row As OpenMobileVerifyClaimRow)
            Me.Rows.Remove(row)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As OpenMobileVerifyClaimDs = New OpenMobileVerifyClaimDs
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
            attribute2.FixedValue = "OpenMobileVerifyClaimDataTable"
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
    Partial Public Class OpenMobileVerifyClaimRow
        Inherits Global.System.Data.DataRow

        Private tableOpenMobileVerifyClaim As OpenMobileVerifyClaimDataTable

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableOpenMobileVerifyClaim = CType(Me.Table, OpenMobileVerifyClaimDataTable)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property Cell_Number() As String
            Get
                Return CType(Me(Me.tableOpenMobileVerifyClaim.Cell_NumberColumn), String)
            End Get
            Set(ByVal value As String)
                Me(Me.tableOpenMobileVerifyClaim.Cell_NumberColumn) = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property ESN() As String
            Get
                Return CType(Me(Me.tableOpenMobileVerifyClaim.ESNColumn), String)
            End Get
            Set(ByVal value As String)
                Me(Me.tableOpenMobileVerifyClaim.ESNColumn) = value
            End Set
        End Property
    End Class

    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")> _
    Public Class OpenMobileVerifyClaimRowChangeEvent
        Inherits Global.System.EventArgs

        Private eventRow As OpenMobileVerifyClaimRow

        Private eventAction As Global.System.Data.DataRowAction

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Sub New(ByVal row As OpenMobileVerifyClaimRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New()
            Me.eventRow = row
            Me.eventAction = action
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public ReadOnly Property Row() As OpenMobileVerifyClaimRow
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
