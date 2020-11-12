'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/29/2020)  ********************

Public Class ArInvoiceHeader
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub
    
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()             
        Try
            Dim dal As New ArInvoiceHeaderDal
            If Dataset.Tables.IndexOf(ArInvoiceHeaderDal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(ArInvoiceHeaderDal.TABLE_NAME).NewRow
            Dataset.Tables(ArInvoiceHeaderDal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(ArInvoiceHeaderDal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize() 
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal invoiceId As Guid)               
        Try
            Dim dal As New ArInvoiceHeaderDal            
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(ArInvoiceHeaderDal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(ArInvoiceHeaderDal.TABLE_NAME) >= 0 Then
                Row = FindRow(invoiceId, ArInvoiceHeaderDal.TABLE_KEY_NAME, Dataset.Tables(ArInvoiceHeaderDal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the data set, so will bring it from the db
                dal.Load(Dataset, invoiceId)
                Row = FindRow(invoiceId, ArInvoiceHeaderDal.TABLE_KEY_NAME, Dataset.Tables(ArInvoiceHeaderDal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
            If row(ArInvoiceHeaderDal.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_INVOICE_HEADER_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValidStringLength("", Max:=120)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INVOICE_NUMBER, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ArInvoiceHeaderDal.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INVOICE_DATE, value)
        End Set
    End Property
	
	
    
    Public Property InvioceDueDate() As DateType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INVOCE_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ArInvoiceHeaderDal.COL_NAME_INVOCE_DUE_DATE), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INVOCE_DUE_DATE, value)
        End Set
    End Property
	
	
    
    Public Property InstallmentNumber() As LongType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ArInvoiceHeaderDal.COL_NAME_INSTALLMENT_NUMBER), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INSTALLMENT_NUMBER, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_SOURCE, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=120)> _
    Public Property Reference() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_REFERENCE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_REFERENCE, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property ReferenceId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_REFERENCE_ID, value)
        End Set
    End Property
	
	
    
    Public Property BillToAddressId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_BILL_TO_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_BILL_TO_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_BILL_TO_ADDRESS_ID, value)
        End Set
    End Property
	
	
    
    Public Property ShipToAddressId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_SHIP_TO_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_SHIP_TO_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_SHIP_TO_ADDRESS_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=12)> _
    Public Property CurrencyCode() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_CURRENCY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_CURRENCY_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_CURRENCY_CODE, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property ExchangeRate() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_EXCHANGE_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceHeaderDal.COL_NAME_EXCHANGE_RATE), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_EXCHANGE_RATE, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INVOICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceHeaderDal.COL_NAME_INVOICE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INVOICE_AMOUNT, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceOpenAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_INVOICE_OPEN_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceHeaderDal.COL_NAME_INVOICE_OPEN_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_INVOICE_OPEN_AMOUNT, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=24)> _
    Public Property AcctPeriod() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_ACCT_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_ACCT_PERIOD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_ACCT_PERIOD, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Distributed() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_DISTRIBUTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_DISTRIBUTED), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_DISTRIBUTED, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_DEALER_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_COMPANY_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=20)> _
    Public Property DocType() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_DOC_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_DOC_TYPE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_DOC_TYPE, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Posted() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_POSTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_POSTED), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_POSTED, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=4)> _
    Public Property NoOfTimesPymtRejected() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_NO_OF_TIMES_PYMT_REJECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_NO_OF_TIMES_PYMT_REJECTED), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_NO_OF_TIMES_PYMT_REJECTED, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property PaymentMethodXcd() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_PAYMENT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_PAYMENT_METHOD_XCD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_PAYMENT_METHOD_XCD, value)
        End Set
    End Property
	
	
    
    Public Property CreditMemoAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_CREDIT_MEMO_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceHeaderDal.COL_NAME_CREDIT_MEMO_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_CREDIT_MEMO_AMOUNT, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property StatusXcd() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_STATUS_XCD, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property DocUniqueIdentifier() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_DOC_UNIQUE_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_DOC_UNIQUE_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_DOC_UNIQUE_IDENTIFIER, value)
        End Set
    End Property
	
	
    
    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceHeaderDal.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_BANK_INFO_ID, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=2000)> _
    Public Property Comments() As String
        Get
            CheckDeleted()
            If row(ArInvoiceHeaderDal.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceHeaderDal.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceHeaderDal.COL_NAME_COMMENTS, value)
        End Set
    End Property
	
	Private _referenceNumber as String = Nothing
    public Const ReferenceCertificate = "ELP_CERT"
    public Const ReferenceClaim = "ELP_CLAIM"
    public Const ReferenceClaimAuthorization = "ELP_CLAIM_AUTHORIZATION"

    Public ReadOnly Property ReferenceNumber() As String
        Get
            if _referenceNumber is Nothing Then
                Select Case Reference
                    Case ReferenceCertificate
                        _referenceNumber = new Certificate(ReferenceId).CertNumber
                    Case ReferenceClaim
                        _referenceNumber = new Claim(ReferenceId).ClaimNumber
                    Case ReferenceClaimAuthorization
                        _referenceNumber = new ClaimAuthorization(ReferenceId).AuthorizationNumber
                    Case Else
                        _referenceNumber = string.Empty
                End Select
            End If
            Return _referenceNumber
        End Get
    end property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ArInvoiceHeaderDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetArInvoices(ByVal companyId As Guid, ByVal dealerId As Guid?, 
                                         ByVal invoiceNum As String, ByVal source As String, 
                                         ByVal invoiceDate As Date?, ByVal reference As String, 
                                         ByVal referenceNumber As String, ByVal documentType As String,
                                         ByVal documentUniqueId As String, ByVal statusXcd As String,
                                         ByVal rowCountReturn As Integer
                                         ) As ArInvoiceSearchDV

        Dim dal As New ArInvoiceHeaderDAL
        Dim userId As Guid
        Dim invoiceLines As New DataSet

        With Authentication.CurrentUser
            userId = .Id
        End With

        dal.SearchArInvoices(companyId, dealerId, invoiceNum, source, invoiceDate, reference,referenceNumber,
                             documentType, documentUniqueId, statusXcd, rowCountReturn, userId, invoiceLines)
        Return New ArInvoiceSearchDV(invoiceLines.Tables(0))

    End Function
    
    Public  Shared Function GetInvoiceLinesByHeader(ByVal invoiceHeaderId As Guid) As ArInvoiceLinesDv
        Dim dal As New ArInvoiceHeaderDAL
        Dim languageId As Guid
        Dim searchResults As New DataSet

        With Authentication.CurrentUser
            languageId = .LanguageId
        End With

        dal.GetArInvoiceLinesByHeaderId(invoiceHeaderId, languageId, searchResults)
        Return New ArInvoiceLinesDv(searchResults.Tables(0))
    End Function
#End Region

#Region "Invoice Lines view class"
    Public Class ArInvoiceLinesDv
        Inherits DataView

        Public Const ColInvoiceLineId As String = "invoice_line_id"
        Public Const ColLineType As String = "line_type"
        Public Const ColItemCode As String = "item_code"
        Public Const ColDescription As String = "description"
        Public Const ColLineNumber As String = "line_number"
        Public Const ColParentLineNumber As String = "parent_line_number"
        Public Const ColAmount As String = "amount"
        Public Const ColInvoicePeriodStartDate As String = "invoice_period_start_date"
        Public Const ColInvoicePeriodEndDate As String = "invoice_period_end_date"
        Public Const ColIncomingAmount As String = "incoming_amount"
        Public Const ColEarningParter As String = "earning_parter"

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region


#Region "Invoice search view class"

    Public Class ArInvoiceSearchDv
        Inherits DataView

        Public Const ColInvoiceHeaderId As String = "invoice_header_id"
        Public Const ColTotalCount As String = "total_count"
        Public Const ColInvoiceNumber As String = "invoice_number"
        Public Const ColInvoiceDate As String = "invoice_date"
        Public Const ColSource As String = "source"
        Public Const ColInvoiceAmount As String = "invoice_amount"

        Public Const ColReference As String = "reference"
        Public Const ColReferenceNumber As String = "reference_number"
        Public Const ColStatusXcd As String = "status_xcd"
        Public Const ColDocumentType As String = "doc_type"
        Public Const ColDocUniqueIdentifier As String = "doc_unique_identifier"
        Public Const ColDealerCode As String = "dealer_code"
        Public Const ColDealerName As String = "dealer_name"

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public ReadOnly Property TotalCount() As Integer
            Get
                If Count > 0 Then
                    Return CType(Me(0)(ColTotalCount), Integer)
                Else
                    Return 0
                End If
            End Get
        End Property
    End Class
#End Region
End Class


