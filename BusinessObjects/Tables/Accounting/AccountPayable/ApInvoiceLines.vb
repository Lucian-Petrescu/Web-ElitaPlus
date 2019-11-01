﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApInvoiceLines
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub
    
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()             
        Try
            Dim dal As New ApInvoiceLinesDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize() 
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)               
        Try
            Dim dal As New ApInvoiceLinesDAL            
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
	'Initialization code for new objects
    Private Sub Initialize()        
    End Sub
#End Region


#Region "Properties"
    
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ApInvoiceLinesDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_LINES_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property ApInvoiceHeaderId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property LineNumber() As LongType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property LineType() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property VendorItemCode() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=1000)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Quantity() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_QUANTITY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property UomXcd() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_UOM_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_UOM_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_UOM_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property MatchedQuantity() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property PaidQuantity() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property UnitPrice() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property TotalPrice() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE, Value)
        End Set
    End Property
	
	
    
    Public Property ParentLineNumber() As LongType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property PoNumber() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER, Value)
        End Set
    End Property
	
	
    
    Public Property PoDate() As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PO_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_PO_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_PO_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property BillingPeriodStartDate() As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property BillingPeriodEndDate() As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property ReferenceNumber() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=1000)> _
    Public Property VendorTransactionType() As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceLinesDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    
#End Region

#Region "APInvoiceLinesDV Dataview"
    Public Class APInvoiceLinesDV
        Inherits DataView

        Public Const COL_INVOICE_lines_ID As String = "ap_invoice_lines_id"
        Public Const COL_LINE_NUMBER As String = "line_number"
        Public Const COL_LINE_TYPE As String = "line_type"
        Public Const COL_VENDOR_ITEM_CODE As String = "vendor_item_code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_QUANTITY As String = "quantity"
        Public Const COL_UNIT_PRICE As String = "unit_price"
        Public Const COL_TOTAL_PRICE As String = "total_price"
        Public Const COL_MATCHED_QUANTITY As String = "matched_quantity"
        Public Const COL_PAID_QUANTITY As String = "paid_quantity"
        Public Const COL_PO_NUMBER As String = "po_number"
        Public Const COL_PO_QUANTITY As String = "po_line_quantity"
        Public Const COL_PAYMENT_STATUS As String = "payment_status"
        Public Const COL_PAYMENT_SOURCE As String = "payment_source"
        Public Const COL_PAYMENT_DATE As String = "payment_date"

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class

