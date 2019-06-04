'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2013)  ********************

Public Class DailyOutboundFileDetail
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
            Dim dal As New DailyOutboundFileDetailDAL
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
            Dim dal As New DailyOutboundFileDetailDAL
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
            If row(DailyOutboundFileDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DailyOutboundFileDetailDAL.COL_NAME_FILE_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property FileHeaderId() As Guid
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_FILE_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DailyOutboundFileDetailDAL.COL_NAME_FILE_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_FILE_HEADER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property



    Public Property WarrantySalesDate() As DateType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_WARRANTY_SALES_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property



    Public Property CertCreatedDate() As DateType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property



    Public Property ItemRetailPrice() As DecimalType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_ITEM_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_ITEM_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_ITEM_RETAIL_PRICE, Value)
        End Set
    End Property



    Public Property CancellationDate() As DateType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property



    Public Property RefundAmt() As DecimalType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_REFUND_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_REFUND_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_REFUND_AMT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property PaymentInstrument() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_PAYMENT_INSTRUMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_PAYMENT_INSTRUMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_PAYMENT_INSTRUMENT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=116)> _
    Public Property AccountNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=64)> _
    Public Property CreditCardNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CREDIT_CARD_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property



    Public Property InstallmentAmount() As DecimalType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property CessSalesrep() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CESS_SALESREP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CESS_SALESREP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CESS_SALESREP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property CessOffice() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CESS_OFFICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CESS_OFFICE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CESS_OFFICE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property SalesRepNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_SALES_REP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_SALES_REP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_SALES_REP_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property SalesDepartment() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_SALES_DEPARTMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_SALES_DEPARTMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_SALES_DEPARTMENT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Businessline() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_BUSINESSLINE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_BUSINESSLINE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_BUSINESSLINE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property DealerBranchCode() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_DEALER_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_DEALER_BRANCH_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_DEALER_BRANCH_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property DocumentType() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property IdentificationNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property HomePhone() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property MfgDescription() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_MFG_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_MFG_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_MFG_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property Model() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property AdditionalInfo() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_ADDITIONAL_INFO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_ADDITIONAL_INFO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_ADDITIONAL_INFO, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property LinkedCertNumber() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_LINKED_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_LINKED_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_LINKED_CERT_NUMBER, Value)
        End Set
    End Property



    Public Property DatePaidFor() As DateType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_DATE_PAID_FOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_DATE_PAID_FOR), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_DATE_PAID_FOR, Value)
        End Set
    End Property



    Public Property BilledAmount() As DecimalType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_BILLED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_BILLED_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_BILLED_AMOUNT, Value)
        End Set
    End Property



    Public Property InstallmentNumber() As LongType
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_INSTALLMENT_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property CancellationReasonCode() As String
        Get
            CheckDeleted()
            If row(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_REASON_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_REASON_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CANCELLATION_REASON_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DailyOutboundFileDetailDAL
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



