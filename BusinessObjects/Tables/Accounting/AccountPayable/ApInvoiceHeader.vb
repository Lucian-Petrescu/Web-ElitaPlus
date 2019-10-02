'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApInvoiceHeader
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
            Dim dal As New ApInvoiceHeaderDAL
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
            Dim dal As New ApInvoiceHeaderDAL            
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
            If row(ApInvoiceHeaderDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_AP_INVOICE_HEADER_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property InvoiceNumber() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_NUMBER), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_INVOICE_AMOUNT, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property TermXcd() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_TERM_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_TERM_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_TERM_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property PaidAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_PAID_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceHeaderDAL.COL_NAME_PAID_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_PAID_AMOUNT, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property PaymentStatusXcd() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_PAYMENT_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_PAYMENT_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_PAYMENT_STATUS_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=1000)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property VendorId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property VendorAddressId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_VENDOR_ADDRESS_ID, Value)
        End Set
    End Property
	
	
    
    Public Property ShipToAddressId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_SHIP_TO_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_SHIP_TO_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_SHIP_TO_ADDRESS_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=40)> _
    Public Property CurrencyIsoCode() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_CURRENCY_ISO_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_CURRENCY_ISO_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_CURRENCY_ISO_CODE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property ExchangeRate() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_EXCHANGE_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceHeaderDAL.COL_NAME_EXCHANGE_RATE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_EXCHANGE_RATE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property ApprovedXcd() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_APPROVED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_APPROVED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_APPROVED_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property AccountingPeriod() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_ACCOUNTING_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_ACCOUNTING_PERIOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_ACCOUNTING_PERIOD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Distributed() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_DISTRIBUTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_DISTRIBUTED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_DISTRIBUTED, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Posted() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_POSTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_POSTED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_POSTED, Value)
        End Set
    End Property
	
	
    
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceHeaderDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceHeaderDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceHeaderDAL
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

End Class


