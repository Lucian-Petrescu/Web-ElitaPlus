﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Data
Imports System.Runtime.Serialization
Imports System.Xml

'
'This source code was auto-generated by xsd, Version=1.1.4322.2032.
'

<Serializable(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Diagnostics.DebuggerStepThrough(),  _
 System.ComponentModel.ToolboxItem(true)>  _
Public Class InterfaceCoverageLimitDs
    Inherits DataSet
    
    Private tableInterfaceCoverageLimit As InterfaceCoverageLimitDataTable
    
    Public Sub New()
        MyBase.New
        Me.InitClass
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(System.String)),String)
        If (Not (strSchema) Is Nothing) Then
            Dim ds As DataSet = New DataSet
            ds.ReadXmlSchema(New XmlTextReader(New System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("InterfaceCoverageLimit")) Is Nothing) Then
                Me.Tables.Add(New InterfaceCoverageLimitDataTable(ds.Tables("InterfaceCoverageLimit")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.InitClass
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    <System.ComponentModel.Browsable(false),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property InterfaceCoverageLimit As InterfaceCoverageLimitDataTable
        Get
            Return Me.tableInterfaceCoverageLimit
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As InterfaceCoverageLimitDs = CType(MyBase.Clone,InterfaceCoverageLimitDs)
        cln.InitVars
        Return cln
    End Function
    
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return false
    End Function
    
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return false
    End Function
    
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As XmlReader)
        Me.Reset
        Dim ds As DataSet = New DataSet
        ds.ReadXml(reader)
        If (Not (ds.Tables("InterfaceCoverageLimit")) Is Nothing) Then
            Me.Tables.Add(New InterfaceCoverageLimitDataTable(ds.Tables("InterfaceCoverageLimit")))
        End If
        Me.DataSetName = ds.DataSetName
        Me.Prefix = ds.Prefix
        Me.Namespace = ds.Namespace
        Me.Locale = ds.Locale
        Me.CaseSensitive = ds.CaseSensitive
        Me.EnforceConstraints = ds.EnforceConstraints
        Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
        Me.InitVars
    End Sub
    
    Protected Overrides Function GetSchemaSerializable() As System.Xml.Schema.XmlSchema
        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        Me.WriteXmlSchema(New XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return System.Xml.Schema.XmlSchema.Read(New XmlTextReader(stream), Nothing)
    End Function
    
    Friend Sub InitVars()
        Me.tableInterfaceCoverageLimit = CType(Me.Tables("InterfaceCoverageLimit"),InterfaceCoverageLimitDataTable)
        If (Not (Me.tableInterfaceCoverageLimit) Is Nothing) Then
            Me.tableInterfaceCoverageLimit.InitVars
        End If
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "InterfaceCoverageLimitDs"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/InterfaceCoverageLimit.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableInterfaceCoverageLimit = New InterfaceCoverageLimitDataTable
        Me.Tables.Add(Me.tableInterfaceCoverageLimit)
    End Sub
    
    Private Function ShouldSerializeInterfaceCoverageLimit() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub InterfaceCoverageLimitRowChangeEventHandler(ByVal sender As Object, ByVal e As InterfaceCoverageLimitRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class InterfaceCoverageLimitDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columncar_code As DataColumn
        
        Private columncoverage_type_code As DataColumn
        
        Private columncoverage_type_description As DataColumn
        
        Private column_coverage_km_mi As DataColumn
        
        Private columncoverage_months As DataColumn
        
        Friend Sub New()
            MyBase.New("InterfaceCoverageLimit")
            Me.InitClass
        End Sub
        
        Friend Sub New(ByVal table As DataTable)
            MyBase.New(table.TableName)
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
            Me.DisplayExpression = table.DisplayExpression
        End Sub
        
        <System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        Friend ReadOnly Property car_codeColumn As DataColumn
            Get
                Return Me.columncar_code
            End Get
        End Property
        
        Friend ReadOnly Property coverage_type_codeColumn As DataColumn
            Get
                Return Me.columncoverage_type_code
            End Get
        End Property
        
        Friend ReadOnly Property coverage_type_descriptionColumn As DataColumn
            Get
                Return Me.columncoverage_type_description
            End Get
        End Property
        
        Friend ReadOnly Property _coverage_km_miColumn As DataColumn
            Get
                Return Me.column_coverage_km_mi
            End Get
        End Property
        
        Friend ReadOnly Property coverage_monthsColumn As DataColumn
            Get
                Return Me.columncoverage_months
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As InterfaceCoverageLimitRow
            Get
                Return CType(Me.Rows(index),InterfaceCoverageLimitRow)
            End Get
        End Property
        
        Public Event InterfaceCoverageLimitRowChanged As InterfaceCoverageLimitRowChangeEventHandler
        
        Public Event InterfaceCoverageLimitRowChanging As InterfaceCoverageLimitRowChangeEventHandler
        
        Public Event InterfaceCoverageLimitRowDeleted As InterfaceCoverageLimitRowChangeEventHandler
        
        Public Event InterfaceCoverageLimitRowDeleting As InterfaceCoverageLimitRowChangeEventHandler
        
        Public Overloads Sub AddInterfaceCoverageLimitRow(ByVal row As InterfaceCoverageLimitRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddInterfaceCoverageLimitRow(ByVal car_code As Long, ByVal coverage_type_code As String, ByVal coverage_type_description As String, ByVal _coverage_km_mi As System.UInt64, ByVal coverage_months As System.UInt64) As InterfaceCoverageLimitRow
            Dim rowInterfaceCoverageLimitRow As InterfaceCoverageLimitRow = CType(Me.NewRow,InterfaceCoverageLimitRow)
            rowInterfaceCoverageLimitRow.ItemArray = New Object() {car_code, coverage_type_code, coverage_type_description, _coverage_km_mi, coverage_months}
            Me.Rows.Add(rowInterfaceCoverageLimitRow)
            Return rowInterfaceCoverageLimitRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As InterfaceCoverageLimitDataTable = CType(MyBase.Clone,InterfaceCoverageLimitDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New InterfaceCoverageLimitDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columncar_code = Me.Columns("car_code")
            Me.columncoverage_type_code = Me.Columns("coverage_type_code")
            Me.columncoverage_type_description = Me.Columns("coverage_type_description")
            Me.column_coverage_km_mi = Me.Columns("coverage_km-mi")
            Me.columncoverage_months = Me.Columns("coverage_months")
        End Sub
        
        Private Sub InitClass()
            Me.columncar_code = New DataColumn("car_code", GetType(System.Int64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columncar_code)
            Me.columncoverage_type_code = New DataColumn("coverage_type_code", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columncoverage_type_code)
            Me.columncoverage_type_description = New DataColumn("coverage_type_description", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columncoverage_type_description)
            Me.column_coverage_km_mi = New DataColumn("coverage_km-mi", GetType(System.UInt64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.column_coverage_km_mi)
            Me.columncoverage_months = New DataColumn("coverage_months", GetType(System.UInt64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columncoverage_months)
            Me.columncar_code.AllowDBNull = false
            Me.columncoverage_type_code.AllowDBNull = false
            Me.columncoverage_type_description.AllowDBNull = false
            Me.column_coverage_km_mi.AllowDBNull = false
            Me.columncoverage_months.AllowDBNull = false
        End Sub
        
        Public Function NewInterfaceCoverageLimitRow() As InterfaceCoverageLimitRow
            Return CType(Me.NewRow,InterfaceCoverageLimitRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New InterfaceCoverageLimitRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(InterfaceCoverageLimitRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.InterfaceCoverageLimitRowChangedEvent) Is Nothing) Then
                RaiseEvent InterfaceCoverageLimitRowChanged(Me, New InterfaceCoverageLimitRowChangeEvent(CType(e.Row,InterfaceCoverageLimitRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.InterfaceCoverageLimitRowChangingEvent) Is Nothing) Then
                RaiseEvent InterfaceCoverageLimitRowChanging(Me, New InterfaceCoverageLimitRowChangeEvent(CType(e.Row,InterfaceCoverageLimitRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.InterfaceCoverageLimitRowDeletedEvent) Is Nothing) Then
                RaiseEvent InterfaceCoverageLimitRowDeleted(Me, New InterfaceCoverageLimitRowChangeEvent(CType(e.Row,InterfaceCoverageLimitRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.InterfaceCoverageLimitRowDeletingEvent) Is Nothing) Then
                RaiseEvent InterfaceCoverageLimitRowDeleting(Me, New InterfaceCoverageLimitRowChangeEvent(CType(e.Row,InterfaceCoverageLimitRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveInterfaceCoverageLimitRow(ByVal row As InterfaceCoverageLimitRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class InterfaceCoverageLimitRow
        Inherits DataRow
        
        Private tableInterfaceCoverageLimit As InterfaceCoverageLimitDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableInterfaceCoverageLimit = CType(Me.Table,InterfaceCoverageLimitDataTable)
        End Sub
        
        Public Property car_code As Long
            Get
                Return CType(Me(Me.tableInterfaceCoverageLimit.car_codeColumn),Long)
            End Get
            Set
                Me(Me.tableInterfaceCoverageLimit.car_codeColumn) = value
            End Set
        End Property
        
        Public Property coverage_type_code As String
            Get
                Return CType(Me(Me.tableInterfaceCoverageLimit.coverage_type_codeColumn),String)
            End Get
            Set
                Me(Me.tableInterfaceCoverageLimit.coverage_type_codeColumn) = value
            End Set
        End Property
        
        Public Property coverage_type_description As String
            Get
                Return CType(Me(Me.tableInterfaceCoverageLimit.coverage_type_descriptionColumn),String)
            End Get
            Set
                Me(Me.tableInterfaceCoverageLimit.coverage_type_descriptionColumn) = value
            End Set
        End Property
        
        Public Property _coverage_km_mi As System.UInt64
            Get
                Return CType(Me(Me.tableInterfaceCoverageLimit._coverage_km_miColumn),System.UInt64)
            End Get
            Set
                Me(Me.tableInterfaceCoverageLimit._coverage_km_miColumn) = value
            End Set
        End Property
        
        Public Property coverage_months As System.UInt64
            Get
                Return CType(Me(Me.tableInterfaceCoverageLimit.coverage_monthsColumn),System.UInt64)
            End Get
            Set
                Me(Me.tableInterfaceCoverageLimit.coverage_monthsColumn) = value
            End Set
        End Property
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class InterfaceCoverageLimitRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As InterfaceCoverageLimitRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As InterfaceCoverageLimitRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As InterfaceCoverageLimitRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        Public ReadOnly Property Action As DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class
