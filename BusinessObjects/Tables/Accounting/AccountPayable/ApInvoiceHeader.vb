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
    Public Sub New(ByVal invoiceNumber As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(invoiceNumber)
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
    Protected Sub Load(ByVal accountPayableInvoiceNumber As String)
        Try
            Dim dal As New ApInvoiceHeaderDAL
            If Me._isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(ApInvoiceHeaderDAL.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Dataset.Tables.IndexOf(ApInvoiceHeaderDAL.TABLE_NAME) >= 0 Then
                Me.Row = FindRow(Id, ApInvoiceHeaderDAL.TABLE_KEY_NAME, Me.Dataset.Tables(ApInvoiceHeaderDAL.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                dal.Load(Me.Dataset, accountPayableInvoiceNumber)
                Me.Row = FindRow(Id, ApInvoiceHeaderDAL.TABLE_KEY_NAME, Me.Dataset.Tables(ApInvoiceHeaderDAL.TABLE_NAME))
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
	
    <ValueMandatory(""),ValidStringLength("Invoice Number", Max:=100)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceHeaderDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=250)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=10)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=50)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=1)> _
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
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=1)> _
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
	<ValueMandatory("")>
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
    Public Sub SaveInvoiceHeader()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso Me.IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceHeaderDAL
                dal.SaveInvoiceHeader(Row)

                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Sub DeleteInvoices(ByVal invoiceIds As Generic.List(Of Guid))
        Dim dal As New ApInvoiceHeaderDAL
        dal.DeleteInvoices(invoiceIds)
    End Sub

    Public Shared Sub PayInvoices(ByVal strBatchNumber As String, ByVal invoiceIds As Generic.List(Of Guid), ByRef errCode As Integer, ByRef errMsg As String)
        Dim dal As New ApInvoiceHeaderDAL
        dal.PayInvoices(strBatchNumber, invoiceIds, errCode, errMsg)
    End Sub

    Public Shared function MatchInvoice(ByVal invoiceId As Guid) As Integer
        Dim dal As New ApInvoiceHeaderDAL, recordsMatched As Integer
        dal.MatchInvoice(invoiceId, recordsMatched)
        Return recordsMatched
    End function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetAPInvoices(ByVal vendorCode As String, ByVal invoiceNum As String,
                                         ByVal source As String, ByVal invoiceDate As Date?,
                                         ByVal dueDateFrom As Date?, ByVal dueDateTo As Date?,
                                         ByVal rowCount As Integer
                                         ) As APInvoiceSearchDV

        Dim dal As New ApInvoiceHeaderDAL
        Dim userId, languageId As Guid
        Dim errCode As Integer
        Dim errMsg As String
        Dim searchResults As New DataSet

        With ElitaPlusIdentity.Current.ActiveUser
            userId = .Id
            languageId = .LanguageId
        End With

        dal.SearchAPInvoices(vendorCode, invoiceNum, source, invoiceDate, dueDateFrom, dueDateTo, rowCount, userId, searchResults)

        Return New APInvoiceSearchDV(searchResults.Tables(0))

    End Function
    Public Function GetInvoiceExtendedInfo() As DataView

        Dim dal As New ApInvoiceHeaderDAL
        Dim dsResults As New DataSet

        dal.LoadAPInvoiceExtendedInfo(Me.Id, dsResults)

        Return dsResults.Tables(0).DefaultView
    End Function

    Public Function GetInvoiceLines(ByVal minLineNum  As Integer,
                                    ByVal maxLineNum  As Integer,
                                    ByVal UnMatchedLineOnly   As Boolean,
                                    ByVal rowCountReturn As Integer) As ApInvoiceLines.APInvoiceLinesDV

        Dim dal As New ApInvoiceHeaderDAL
        Dim dsResults As New DataSet

        dal.LoadAPInvoiceLines(Me.Id, minLineNum, maxLineNum, UnMatchedLineOnly, ElitaPlusIdentity.Current.ActiveUser.LanguageId, rowCountReturn, dsResults)
        
        Return New ApInvoiceLines.APInvoiceLinesDV(dsResults.Tables(0))

    End Function
    Public Function GetApInvoice(ByVal apInvoiceNumber As String, ByVal apInvoiceVendorId As Guid) As Dataview
        
        Dim dal As New ApInvoiceHeaderDAL
        Dim dsResults As New DataSet
        dal.LoadAPInvoice(apInvoiceNumber,apInvoiceVendorId, dsResults)
        Return dsResults.Tables(0).DefaultView

    End Function
#End Region

#Region "Invoice search view class"

    Public Class APInvoiceSearchDV
        Inherits DataView

        Public Const COL_INVOICE_HEADER_ID As String = "ap_invoice_header_id"
        Public Const COL_INVOICE_NUMBER As String = "invoice_number"
        Public Const COL_INVOICE_DATE As String = "invoice_date"
        Public Const COL_DUE_DATE As String = "due_date"
        Public Const COL_SOURCE As String = "source"
        Public Const COL_INVOICE_AMOUNT As String = "invoice_amount"
        Public Const COL_MATCHED_AMOUNT As String = "matched_amount"
        Public Const COL_PAID_AMOUNT As String = "paid_amount"
        Public Const COL_PAYMENT_DATE As String = "payment_date"
        Public Const COL_UNMATCHED_LINES_COUNT As String = "unmatched_line_count"
        Public Const COL_TOTAL_LINE_COUNT As String = "total_line_count"
        Public Const COL_VENDOR As String = "vendor"
        Public Const COL_VENDOR_ADDRESS As String = "vendor_address"
        Public Const COL_VENDOR_ID As String = "vendor_id"
        Public Const COL_PAYMENT_STATUS_XCD As String = "payment_status_xcd"
        Public Const COL_TOTAL_COUNT As String = "total_count"
        Public Const COL_INVOICE_TERM As String = "Term"
        Public Const COL_INVOICE_DELAER As String = "Dealer"
        Public Const MSG_THE_VALUE_REQUIRED_INVOICE_NO As String = "MSG_THE_VALUE_REQUIRED_INVOICE_NO"
        Public Const MSG_THE_VALUE_REQUIRED_INVOICE_AMOUNT As String = "MSG_THE_VALUE_REQUIRED_INVOICE_AMOUNT"
        Public Const MSG_THE_VALUE_REQUIRED_VENDOR As String = "MSG_THE_VALUE_REQUIRED_VENDOR"
        Public Const MSG_THE_VALUE_REQUIRED_DEALER As String = "MSG_THE_VALUE_REQUIRED_DEALER"
        Public Const MSG_THE_VALUE_REQUIRED_INVOICE_DATE As String = "MSG_THE_VALUE_REQUIRED_INVOICE_DATE"
        Public Const MSG_THE_VALUE_REQUIRED_INVOICE_TERM As String = "MSG_THE_VALUE_REQUIRED_INVOICE_TERM"
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public ReadOnly Property TotalCount() As Integer
            Get
                If Count > 0 Then
                    Return Me(0)(COL_TOTAL_COUNT)
                Else
                    Return 0

                End If
            End Get
        End Property
    End Class
#End Region

End Class


