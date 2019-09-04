'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/12/2004)  ********************
Imports Assurant.ElitaPlus.Common
Imports System.Math
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem
Imports System.Collections.Generic
Imports System.Globalization

Public Class Certificate
    Inherits BusinessObjectBase
    Implements Address.IAddressUser


#Region "Constants"
    Public Const COL_TOTAL_GROSS_AMT_RECEIVED As String = "total_gross_amt_received"
    Public Const COL_TOTAL_PREMIUM_WRITTEN As String = "Total_Premium_Written"
    Public Const COL_TOTAL_ORIGINAL_PREMIUM As String = "Total_Original_Premium"
    Public Const COL_TOTAL_LOSS_COST As String = "Total_Loss_cost"
    Public Const COL_TOTAL_COMISSION As String = "Total_Comission"
    Public Const COL_TOTAL_ADMIN_EXPENSE As String = "total_admin_expense"
    Public Const COL_TOTAL_MARKETING_EXPENSE As String = "total_marketing_expense"
    Public Const COL_TOTAL_OTHER As String = "total_other"
    Public Const COL_TOTAL_SALES_TAX As String = "total_sales_tax"
    Public Const COL_TOTAL_MTD_PAYMENTS As String = "total_mtd_payments"
    Public Const COL_TOTAL_YTD_PAYMENTS As String = "total_ytd_payments"
    Public Const COL_TOTAL_MTD_INCOMING_PAYMENTS As String = "total_mtd_incoming_pymnts"
    Public Const COL_TOTAL_YTD_INCOMING_PAYMENTS As String = "total_ytd_incoming_pymnts"
    Public Const COL_TOTAL_INCOMING_PAYMENTS As String = "total_incoming_pymnts"

    Public Const DOC_TYPE_CPF As String = "CPF"
    Public Const DOC_TYPE_CNPJ As String = "CNPJ"
    Public Const DOC_TYPE_CON As String = "CON"

    Public Const VALIDATION_FLAG_FULL As String = "1"
    Public Const VALIDATION_FLAG_PARTIAL As String = "2"
    Public Const VALIDATION_FLAG_NONE As String = "3"
    'REQ 1012 for DEF 2576
    Public Const VALIDATION_FLAG_CPF_CNPJ As String = "4"
    'REQ 1070/DEF 2878
    Public Const VALIDATION_FLAG_NO_VALIDATION As String = "5"

    Private Const SEARCH_EXCEPTION As String = "CERTIFICATELIST_FORM001" ' Certificate List Search Exception
    Public Const NO_DEALER_SELECTED = "--"
    Public Const NO_RECORDS_FOUND = "NO RECORDS FOUND."

    Public Const COMPANY_TYPE_SERVICES As String = "2"
    Public Const COMPANY_TYPE_INSURANCE As String = "1"

    Public Const CODE As String = "Code"
    Public Const FIRST_ROW As Integer = 0

    Public Const PAYMENT_BY_DIRECT_DEBIT = "6"
    Public Const CANCELLATION_REASON_CODE_16 = "16"
    Public Const CANCELLATION_REASON_CODE_17 = "17"

    Public Const IDENTIFICATION_NUMBER = "IdentificationNumber"
    Public Const TAX_ID_NUMB = "TaxIDNumb"

    Public Const SEARCH_SORT_DEFAULT = CertificateDAL.SORT_BY_CUSTOMER_NAME
    Public Const SEARCH_MAX_NUMBER_OF_ROWS = CertificateDAL.MAX_NUMBER_OF_ROWS
    Public Const EQUIPMENT_NOT_FOUND As String = "EQUIPMENT_NOT_FOUND" ' Equipment not Found on Equipment Table. Change/Update can not be Made.
    Private _Global_IsSubscriberStatusValid As String

    Public Const CERT_INSTALLMENT_INCOLLECTION_STATUS = "I"
    Public Const PAYMENT_PRE_AUTHORIZED = "8"

    'Def-24283. 
    'Added follosing constant for certificate search screen performance.
    Private Const WILDCARD_CHAR As Char = "%"
    Private Const ASTERISK As Char = "*"
    Private Const SEARCH_EXCEPTION_INFORCE_DATE As String = "SEARCH_EXCEPTION_INFORCE_DATE"
    Public Const SEARCH_REGEX As String = "^[*]|[%]"
    Public Const BILLING_ACCOUNT_NUMBER As String = "BILLING_ACCOUNT_NUMBER"


#End Region

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

    Protected Sub Load()
        Try
            Dim dal As New CertificateDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertificateDAL
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

#Region "Attributes"
    Dim productCodeDescription As String
    Dim productUpgradeProgram As String
    Dim typeOfEquip As String
    Dim paymentTypeDesc As String
    Dim paymentTypeCode As String
    Dim CollectionMethodCode As String
    Dim PaymentInstrumentCode As String
    Dim documentTypeDesc As String
    Dim masterClaimProc As String
    Dim documentTypeCode As String
    Dim ValTypeCode As String
    Dim currencyTypeDesc As String
    Dim dealerDesc As String
    Dim dealerGroupName As String
    Dim creditCardTypeDesc As String
    Dim moCertCancellation As CertCancellation
    Dim moCertCoverageID As Guid
    Dim salutationDesc As String
    Dim langPrefDesc As String
    Dim postPrePaidDesc As String
    Dim UpgradeTermUOM As String
#End Region
#Region "Navigation Properties"

    Private _productCode As ProductCode = Nothing
    Private _company As Company = Nothing

    Public ReadOnly Property Product As ProductCode
        Get
            If (_productCode Is Nothing) Then
                If (Me.DealerId <> Guid.Empty) AndAlso (Not Me.ProductCode Is Nothing) AndAlso (Me.ProductCode.Trim().Length > 0) Then
                    _productCode = New ProductCode(Me.DealerId, Me.ProductCode, Me.Dataset)
                End If
            End If
            Return _productCode
        End Get
    End Property

    Public ReadOnly Property Company As Company
        Get
            If (_company Is Nothing) Then
                If (Me.CompanyId <> Guid.Empty) Then
                    _company = New Company(Me.CompanyId, Me.Dataset)
                End If
            End If
            Return _company
        End Get
    End Property




    Public ReadOnly Property Items As CertItem.ItemList
        Get
            Return CertItem.GetItemListForCertificate(Me.Id, Me)
        End Get
    End Property


    <Obsolete("This method does not use DataSet Cache. Try to replace with Items Property")>
    Public ReadOnly Property CertItems As CertItemSearchDV
        Get
            Return CertItem.GetItems(Me.Id)
        End Get
    End Property


#End Region
#Region "Properties"


    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertificateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.Dealer = Nothing
            Me.SetValue(CertificateDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    Public Property SalesChannel() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SALES_CHANNEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SALES_CHANNEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SALES_CHANNEL, Value)
        End Set
    End Property


    Public Property PaymentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PAYMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_PAYMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PAYMENT_TYPE_ID, Value)
        End Set
    End Property



    Public Property CommissionBreakdownId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_COMMISSION_BREAKDOWN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_COMMISSION_BREAKDOWN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_COMMISSION_BREAKDOWN_ID, Value)
        End Set
    End Property



    Public Property FinanceCurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_FINANCE_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_FINANCE_CURRENCY_ID, Value)
        End Set
    End Property



    Public Property PurchaseCurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PURCHASE_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_PURCHASE_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PURCHASE_CURRENCY_ID, Value)
        End Set
    End Property
    Public Property ProdLiabilityPolicyCd() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PROD_LIABILITY_POLICY_CD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PROD_LIABILITY_POLICY_CD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PROD_LIABILITY_POLICY_CD, Value)
        End Set
    End Property

    'DEF-1476
    'Public Property CreditcardTypeId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_CREDITCARD_TYPE_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(CertificateDAL.COL_NAME_CREDITCARD_TYPE_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_CREDITCARD_TYPE_ID, Value)
    '    End Set
    'End Property
    'End DEF-1476

    <ValueMandatory("")>
    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property



    Public Property TypeOfEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID, Value)
        End Set
    End Property

    Public Property PostPrePaidId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_POST_PRE_PAID_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_POST_PRE_PAID_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_POST_PRE_PAID_ID, Value)
        End Set
    End Property

    Public Property AddressId() As Guid Implements Address.IAddressUser.AddressId
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property StatusCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property


    Public Property SubscriberStatus() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ProductSalesDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PRODUCT_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_PRODUCT_SALES_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PRODUCT_SALES_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonFutureProductSalesAndWarrantyDate("")>
    Public Property WarrantySalesDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_WARRANTY_SALES_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    Public Property CustCancelDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUST_CANCEL_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CUST_CANCEL_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUST_CANCEL_DATE, Value)
        End Set
    End Property

    Public Property CustReqCancelDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUST_REQ_CANCEL_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CUST_REQ_CANCEL_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUST_REQ_CANCEL_DATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=50)>
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property CustomerFirstName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMER_FIRST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CUSTOMER_FIRST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMER_FIRST_NAME, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=50)>
    Public Property CustomerMiddleName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMER_MIDDLE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CUSTOMER_MIDDLE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMER_MIDDLE_NAME, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=50)>
    Public Property CustomerLastName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMER_LAST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CUSTOMER_LAST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMER_LAST_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property AlternativeLastName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ALTERNATIVE_LAST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_ALTERNATIVE_LAST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_ALTERNATIVE_LAST_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property AlternativeFirstName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ALTERNATIVE_FIRST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_ALTERNATIVE_FIRST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_ALTERNATIVE_FIRST_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property CorporateName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CORPORATE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CORPORATE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CORPORATE_NAME, Value)
        End Set
    End Property
    Public Property CustomerId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_CUSTOMER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMER_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=15)>
    Public Property HomePhone() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property WorkPhone() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_WORK_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255), EmailAddress("")>
    Public Property Email() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property DealerBranchCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_BRANCH_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DEALER_BRANCH_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property SalesRepNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SALES_REP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SALES_REP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SALES_REP_NUMBER, Value)
        End Set
    End Property


    Public Property SalutationId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_SALUTATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SALUTATION_ID, Value)
        End Set
    End Property

    Public Property LanguageId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_LANGUAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_LANGUAGE_ID, Value)
        End Set
    End Property

    Public Property MonthlyPayments() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MONTHLY_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_MONTHLY_PAYMENTS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MONTHLY_PAYMENTS, Value)
        End Set
    End Property
    Public Property Financed_installment_Amount() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property
    Public Property PenaltyFee() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PENALTY_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PENALTY_FEE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PENALTY_FEE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property DealerItem() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_ITEM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DEALER_ITEM, Value)
        End Set
    End Property


    Public Property SalesPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SALES_PRICE) Is DBNull.Value Then 'INC-2686
                Return New DecimalType(0)
            Else
                Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_SALES_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SALES_PRICE, Value)
        End Set
    End Property

    'DEF-1476
    'Public Property CreditCardProcessing() As DecimalType
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_CREDIT_CARD_PROCESSING) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_CREDIT_CARD_PROCESSING), Decimal))
    '        End If
    '    End Get
    '    Set(ByVal Value As DecimalType)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_CREDIT_CARD_PROCESSING, Value)
    '    End Set
    'End Property
    'END DEF-1476


    <ValidStringLength("", Max:=15)>
    Public Property CampaignNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CAMPAIGN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CAMPAIGN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CAMPAIGN_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property DealerProductCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DEALER_PRODUCT_CODE, Value)
        End Set
    End Property

    Public Property VehicleLicenseTag() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_VEHICLE_LICENSE_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_VEHICLE_LICENSE_TAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_VEHICLE_LICENSE_TAG, Value)
        End Set
    End Property

    Public Property DatePaidFor() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DATE_PAID_FOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_DATE_PAID_FOR).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DATE_PAID_FOR, Value)
        End Set
    End Property



    Public Property DatePaid() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DATE_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_DATE_PAID).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DATE_PAID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property Retailer() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_RETAILER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_RETAILER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_RETAILER, Value)
        End Set
    End Property

    Public ReadOnly Property DateAdded() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CREATED_DATE).ToString()))
            End If
        End Get
    End Property

    Public ReadOnly Property LastDateMaintained() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_MODIFIED_DATE).ToString()))
            End If
        End Get
    End Property

    Public ReadOnly Property GetProdCodeDesc() As String
        Get
            Dim ds As DataSet
            Dim cert As New CertificateDAL

            ds = cert.getProductCodeDescription(Me.DealerId, Me.ProductCode)

            If (ds.Tables(CertificateDAL.TABLE_NAME).Rows(0).Item(CertificateDAL.COL_NAME_PRODUCT_CODE_DESCRIPTION)) _
                              Is DBNull.Value Then
                Return Nothing
            Else
                productCodeDescription = ds.Tables(CertificateDAL.TABLE_NAME).Rows(0).Item(CertificateDAL.COL_NAME_PRODUCT_CODE_DESCRIPTION)
                Return productCodeDescription
            End If

        End Get
    End Property

    Public ReadOnly Property GetCertificateItem(ByVal certItemID As Guid) As CertItem
        Get
            Return New CertItem(certItemID, Me.Dataset)
        End Get
    End Property

    Public Property SelectedCoverageId() As Guid
        Get
            Return moCertCoverageID
        End Get
        Set(ByVal Value As Guid)
            moCertCoverageID = Value
        End Set
    End Property

    'PM 2/14/2006 begin
    Public ReadOnly Property AssociatedItemCoverages() As BusinessObjectListBase
        Get
            Return CertItemCoverage.GetItemCovListForCertificate(Me.Id, Me)
        End Get
    End Property
    'PM 2/14/2006 end

    'PM 3/20/2006 begin
    Public ReadOnly Property InsuranceActivationDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_INSURANCE_ACTIVATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_INSURANCE_ACTIVATION_DATE).ToString()))
            End If
        End Get
    End Property

    <ValueMandatoryDocumentType("")>
    Public Property DocumentTypeID() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DOCUMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_DOCUMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DOCUMENT_TYPE_ID, Value)
        End Set
    End Property

    <NewValueMandatory(""), ValidStringLength("", Max:=15)>
    Public Property DocumentAgency() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DOCUMENT_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DOCUMENT_AGENCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DOCUMENT_AGENCY, Value)
        End Set
    End Property


    <NewValueMandatory(""), NonFutureDocumentIssueDate("")>
    Public Property DocumentIssueDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DOCUMENT_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_DOCUMENT_ISSUE_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DOCUMENT_ISSUE_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), NewValueMandatory("")>
    Public Property RgNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_RG_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_RG_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_RG_NUMBER, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=99)>
    Public Property RatingPlan() As LongType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_RATING_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertificateDAL.COL_NAME_RATING_PLAN), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_RATING_PLAN, Value)
        End Set
    End Property

    <NewValueMandatory(""), ValidStringLength("", Max:=10)>
    Public Property IdType() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_ID_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_ID_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50), ValueMustBeBlankForDocumentNumber(""), SPValidationDocumentNumber(IDENTIFICATION_NUMBER), ValueTaxIdLenht("")>
    Public Property IdentificationNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property
    Public Property IdentificationNumberType() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20), ValueMustBeBlankForDocumentNumber(""), SPValidationDocumentNumber(TAX_ID_NUMB), ValueTaxIdLenht("")>
    Public Property TaxIDNumb() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property

    Dim _IsValidationRequire As Boolean
    Public Property IsValidationRequire() As Boolean
        Get
            Return _IsValidationRequire
        End Get
        Set(ByVal Value As Boolean)
            _IsValidationRequire = Value
        End Set
    End Property

    'PM 6/22/06 WR 765029 end
    Public ReadOnly Property OldNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_OLD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_OLD_NUMBER), String)
            End If
        End Get
    End Property

    Public Property CountryPurchaseId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_COUNTRY_OF_PURCHASE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_COUNTRY_OF_PURCHASE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_COUNTRY_OF_PURCHASE_ID, Value)
        End Set
    End Property

    'PM 3/20/2006 end

    Public ReadOnly Property CompanyId() As Guid
        Get
            If Row(CertificateDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property UseDepreciation() As Guid
        Get
            If Row(CertificateDAL.COL_NAME_USE_DEPRECIATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_USE_DEPRECIATION), Byte()))
            End If
        End Get
    End Property

    Dim _ValFlag As String
    Public Property ValFlag() As String
        Get
            Return _ValFlag
        End Get
        Set(ByVal Value As String)
            _ValFlag = Value
        End Set
    End Property

    Public Property CurrencyOfCertId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CURRENCY_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_CURRENCY_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CURRENCY_CERT_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property Password() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PASSWORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PASSWORD, Value)
        End Set
    End Property

    Public Property VehicleYear() As Integer
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_VEHICLE_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CertificateDAL.COL_NAME_VEHICLE_YEAR), Integer))
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_VEHICLE_YEAR, Value)
        End Set
    End Property

    Public Property ModelId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MODEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_MODEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MODEL_ID, Value)
        End Set
    End Property

    Public Property Odometer() As Integer
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CertificateDAL.COL_NAME_ODOMETER), Integer))
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_ODOMETER, Value)
        End Set
    End Property

    Public Property ClassCodeId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CLASS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_CLASS_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CLASS_CODE_ID, Value)
        End Set
    End Property

    <NonFutureDate("")>
    Public Property DateOfBirth() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DATE_OF_BIRTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_DATE_OF_BIRTH).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_DATE_OF_BIRTH, Value)
        End Set
    End Property

    Public Property MailingAddressId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MAILING_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_MAILING_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MAILING_ADDRESS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property MembershipNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MEMBERSHIP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_MEMBERSHIP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MEMBERSHIP_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property ServiceLineNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SERVICE_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SERVICE_LINE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SERVICE_LINE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=25)>
    Public Property Region() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_REGION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BillingPlan() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_BILLING_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_BILLING_PLAN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_BILLING_PLAN, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BillingCycle() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_BILLING_CYCLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_BILLING_CYCLE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_BILLING_CYCLE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property PrimaryMemberName() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PRIMARY_MEMBER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PRIMARY_MEMBER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PRIMARY_MEMBER_NAME, Value)
        End Set
    End Property

    Public Property MembershipTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MEMBERSHIP_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_MEMBERSHIP_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MEMBERSHIP_TYPE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property VatNum() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_VAT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_VAT_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_VAT_NUM, Value)
        End Set
    End Property

    'Added for Req-703 - Start
    Public ReadOnly Property CapitalizationSeries() As String
        Get
            If Row(CertificateDAL.COL_NAME_MARKETING_PROMO_SER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_MARKETING_PROMO_SER), String)
            End If
        End Get
    End Property

    Public ReadOnly Property CapitalizationNumber() As String
        Get
            If Row(CertificateDAL.COL_NAME_MARKETING_PROMO_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_MARKETING_PROMO_NUM), String)
            End If
        End Get
    End Property
    'Added for Req-703 - End

    'if the property 'LinesOnAccount' is changed to be editable then we need to validate it in BO to be non negative
    Public ReadOnly Property LinesOnAccount() As String
        Get
            If Row(CertificateDAL.COL_NAME_LINES_OF_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_LINES_OF_ACCOUNT), String)
            End If
        End Get
    End Property

    Public Property SubStatusChangeDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE).ToString()))
            End If
        End Get
        'REQ-5426 - this is made editable as Movistar Columbia dealer would update this field using web service
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE, Value)
        End Set
    End Property

    'Added for Req-910 - Begin
    <ValidStringLength("", Max:=80)>
    Public Property Occupation() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_OCCUPATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_OCCUPATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_OCCUPATION, Value)
        End Set
    End Property



    Public Property PoliticallyExposedId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_POLITICALLY_EXPOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_POLITICALLY_EXPOSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_POLITICALLY_EXPOSED_ID, Value)
        End Set
    End Property



    Public Property IncomeRangeId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_INCOME_RANGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_INCOME_RANGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_INCOME_RANGE_ID, Value)
        End Set
    End Property
    'Added for Req-910 - END

    'Added for Req-1251
    Public ReadOnly Property Suspended_Reason_Id() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SUSPENDED_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_SUSPENDED_REASON_ID), Byte()))
            End If
        End Get
    End Property

    Public Property ProductTotalPaidAmount() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_PRODUCT_TOTAL_PAID_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_PRODUCT_TOTAL_PAID_AMOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_PRODUCT_TOTAL_PAID_AMOUNT, Value)
        End Set

    End Property

    Public Property ProductRemainLiabilityLimit() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_PRODUCT_REMAIN_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_PRODUCT_REMAIN_LIABILITY_LIMIT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_PRODUCT_REMAIN_LIABILITY_LIMIT, Value)
        End Set

    End Property

    Public Property ProductLiabilityLimit() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_PRODUCT_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_PRODUCT_LIABILITY_LIMIT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_PRODUCT_LIABILITY_LIMIT, Value)
        End Set

    End Property


    Public ReadOnly Property IsSubscriberStatusValid As Boolean

        Get
            Dim _isSubscriberStatusValid As Boolean = True

            If (_Global_IsSubscriberStatusValid Is Nothing) Then
                If (Not Me.SubscriberStatus.Equals(Guid.Empty)) Then
                    _isSubscriberStatusValid = False

                    Dim subStatus As String = LookupListNew.GetCodeFromId("SUBSTAT", Me.SubscriberStatus)

                    If (subStatus = Codes.SUBSCRIBER_STATUS__ACTIVE) OrElse (subStatus = Codes.SUBSCRIBER_STATUS__PAST_DUE_CLAIMS_ALLOWED) Then
                        _isSubscriberStatusValid = True
                        ' REQ-1251 Allow Claim if Suspended Reason code is set to Allow Cliams
                    ElseIf subStatus = Codes.SUBSCRIBER_STATUS__SUSPENDED Then
                        If Not Suspended_Reason_Id.Equals(System.Guid.Empty) Then
                            Dim oSR As New SuspendedReasons(Suspended_Reason_Id)

                            If oSR.Claim_Allowed_True Then
                                _isSubscriberStatusValid = True
                            End If
                        End If
                    End If
                End If

                _Global_IsSubscriberStatusValid = IIf(_isSubscriberStatusValid, "T", "F")
            Else
                _isSubscriberStatusValid = (_Global_IsSubscriberStatusValid = "T")
            End If

            Return _isSubscriberStatusValid
        End Get
    End Property

    Public ReadOnly Property Cert_CreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CERT_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CERT_CREATED_DATE).ToString()))
            End If
        End Get
    End Property

    Public Property MaritalStatus() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_MARITALSTATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_MARITALSTATUS), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_MARITALSTATUS, value)
        End Set
    End Property

    'REQ-1255 - START
    Public Property Nationality() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_NATIONALITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_NATIONALITY), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_NATIONALITY, value)
        End Set
    End Property

    Public Property PlaceOfBirth() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PLACEOFBIRTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_PLACEOFBIRTH), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PLACEOFBIRTH, value)
        End Set
    End Property

    Public Property Gender() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_GENDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_GENDER), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_GENDER, value)
        End Set
    End Property

    Public Property PersonTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PERSON_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_PERSON_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PERSON_TYPE_ID, value)
        End Set
    End Property
    <ValidStringLength("", Max:=11)>
    Public Property CUIT_CUIL() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUIT_CUIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CUIT_CUIL), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUIT_CUIL, value)
        End Set
    End Property

    'REQ-1255 - END

    <ValidStringLength("", Max:=40)>
    Public Property LinkedCertNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_LINKED_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_LINKED_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_LINKED_CERT_NUMBER, Value)
        End Set
    End Property

    Public Property CustomerinfoLastchangeDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CUSTOMERINFO_LASTCHANGE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CUSTOMERINFO_LASTCHANGE_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CUSTOMERINFO_LASTCHANGE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=32)>
    Public Property NewUsed() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_NEW_USED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_NEW_USED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_NEW_USED, Value)
        End Set
    End Property

    'REQ-5478
    Public ReadOnly Property Finance_Tab_Amount() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_TAB_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FINANCE_TAB_AMOUNT), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Finance_Term() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_TERM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FINANCE_TERM), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Finance_Frequency() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_FREQUENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FINANCE_FREQUENCY), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Finance_Installment_Amount() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_AMOUNT), String)
            End If
        End Get
    End Property

    Public Property VinLocator() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_VIN_LOCATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_VIN_LOCATOR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_VIN_LOCATOR, Value)
        End Set
    End Property


    Public ReadOnly Property FinanceDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_FINANCE_DATE).ToString()))
            End If
        End Get
    End Property

    Public ReadOnly Property DownPayment() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DOWN_PAYMENT) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DOWN_PAYMENT), String)
            End If
        End Get
    End Property

    Public ReadOnly Property AdvancePayment() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ADVANCE_PAYMENT) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return CType(Row(CertificateDAL.COL_NAME_ADVANCE_PAYMENT), String)
            End If
        End Get
    End Property

    Public ReadOnly Property BillingAccountNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_BILLING_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_BILLING_ACCOUNT_NUMBER), String)
            End If
        End Get
    End Property

    '5525
    Public ReadOnly Property GetFinancialAmount(ByVal serialNumber As String) As Decimal
        Get
            Dim calculator As ICalculateFinancialBalance = CalculateFinancialBalanceFactory.GetCalculator(Me.Product.UpgradeFinanceBalanceComputationMethod)
            calculator.SerialNumber = serialNumber
            Return calculator.Calculate(Me)
        End Get
    End Property

    Public ReadOnly Property Finance_Installment_Number() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FINANCE_INSTALLMENT_NUMBER), String)
            End If
        End Get
    End Property
    Public Property NumOfConsecutivePayments() As Integer
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS) Is DBNull.Value Then
                Return -99
            Else
                Return (CType(Row(CertificateDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS), Integer))
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS, Value)
        End Set
    End Property

    Public Property UpgradeFixedTerm() As LongType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_FIXED_TERM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertificateDAL.COL_NAME_UPGRADE_FIXED_TERM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_UPGRADE_FIXED_TERM, Value)
        End Set
    End Property

    Public Property UpgradeTermUomId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_TERM_UOM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_UPGRADE_TERM_UOM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_UPGRADE_TERM_UOM_ID, Value)
        End Set
    End Property

    Public Property UpgradeTermFrom() As LongType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_TERM_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertificateDAL.COL_NAME_UPGRADE_TERM_FROM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_UPGRADE_TERM_FROM, Value)
        End Set
    End Property

    Public Property UpgradeTermTo() As LongType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_TERM_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertificateDAL.COL_NAME_UPGRADE_TERM_TO), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_UPGRADE_TERM_TO, Value)
        End Set
    End Property
    Public ReadOnly Property LoanCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_LOAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_LOAN_CODE), String)
            End If
        End Get
    End Property
    Public Property PaymentShiftNumber() As LongType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PAYMENT_SHIFT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertificateDAL.COL_NAME_PAYMENT_SHIFT_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PAYMENT_SHIFT_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property ReinsuranceStatusId() As Guid
        Get
            If Row(CertificateDAL.COL_NAME_REINSURANCE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_REINSURANCE_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property ReinsuredRejectReason() As String
        Get
            If Row(CertificateDAL.COL_NAME_REINSURANCE_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertificateDAL.COL_NAME_REINSURANCE_REJECT_REASON), String))
            End If
        End Get
    End Property

    Public ReadOnly Property DealerCurrentPlanCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_CURRENT_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_CURRENT_PLAN_CODE), String)
            End If
        End Get
    End Property
    Public ReadOnly Property DealerScheduledPlanCode() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_SCHEDULED_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_SCHEDULED_PLAN_CODE), String)
            End If
        End Get
    End Property
    Public ReadOnly Property DealerRewardPoints() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_REWARD_POINTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_REWARD_POINTS), String)
            End If
        End Get
    End Property

    Public ReadOnly Property IsChildCertificate() As Boolean
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_IS_CHILD_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                'Return Boolean.Parse(Row(CertificateDAL.COL_NAME_IS_CHILD_CERTIFICATE).ToString().ToLower())
                Return Row(CertificateDAL.COL_NAME_IS_CHILD_CERTIFICATE).ToString.ToUpper = "T"
            End If
        End Get
    End Property


    Public ReadOnly Property IsParentCertificate() As Boolean
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_IS_PARENT_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                'Return Boolean.Parse(Row(CertificateDAL.COL_NAME_IS_PARENT_CERTIFICATE).ToString.ToLower())
                Return Row(CertificateDAL.COL_NAME_IS_PARENT_CERTIFICATE).ToString.ToUpper = "T"
            End If
        End Get
    End Property

    Public ReadOnly Property OutstandingBalanceAmount() As Decimal
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_OUTSTANDING_BALANCE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Decimal(CType(Row(CertificateDAL.COL_NAME_OUTSTANDING_BALANCE_AMOUNT), Decimal))
            End If
        End Get
    End Property

    Public ReadOnly Property OutstandingBalanceDueDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_OUTSTANDING_BALANCE_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_OUTSTANDING_BALANCE_DUE_DATE).ToString()))
            End If
        End Get
    End Property
    Public Property CertificateSigned() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CERTIFICATE_SIGNED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CERTIFICATE_SIGNED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CERTIFICATE_SIGNED, Value)
        End Set
    End Property
    Public Property SepaMandateSigned() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SEPA_MANDATE_SIGNED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SEPA_MANDATE_SIGNED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SEPA_MANDATE_SIGNED, Value)
        End Set
    End Property

    Public Property CheckSigned() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CHECK_SIGNED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CHECK_SIGNED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CHECK_SIGNED, Value)
        End Set
    End Property

    Public Property CheckVerificationDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CHECK_VERIFICATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CHECK_VERIFICATION_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CHECK_VERIFICATION_DATE, Value)
        End Set
    End Property
    Public Property ServiceID() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_SERVICE_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SERVICE_ID, Value)
        End Set
    End Property

    'Public Property MfgBeginDate() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_mfg_begin_date) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertificateDAL.COL_NAME_mfg_begin_date), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_mfg_begin_date, Value)
    '    End Set
    'End Property
    'Public Property MfgEndDate() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_mfg_end_date) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertificateDAL.COL_NAME_mfg_end_date), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_mfg_end_date, Value)
    '    End Set
    'End Property
    'Public Property MfgBeginKm() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_mfg_begin_km) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertificateDAL.COL_NAME_mfg_begin_km), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_mfg_begin_km, Value)
    '    End Set
    'End Property
    'Public Property MfgEndKm() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CertificateDAL.COL_NAME_mfg_end_km) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CertificateDAL.COL_NAME_mfg_end_km), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CertificateDAL.COL_NAME_mfg_end_km, Value)
    '    End Set
    'End Property

    Public Property ServiceStartDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SERVICE_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_SERVICE_START_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SERVICE_START_DATE, Value)
        End Set
    End Property
    Public Property ContractCheckCompleteDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE_DATE, Value)
        End Set
    End Property
    Public Property CertificateVerificationDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CERTIFICATE_VERIFICATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_CERTIFICATE_VERIFICATION_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CERTIFICATE_VERIFICATION_DATE, Value)
        End Set
    End Property
    Public Property SepaMandateDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_SEPA_MANDATE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_SEPA_MANDATE_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_SEPA_MANDATE_DATE, Value)
        End Set
    End Property
    Public Property ContractCheckComplete() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_CONTRACT_CHECK_COMPLETE, Value)
        End Set
    End Property

    Public ReadOnly Property DealerUpdateReason() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEALER_UPDATE_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEALER_UPDATE_REASON), String)
            End If
        End Get
    End Property

    Public ReadOnly Property BillingDocumentType() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_BILLING_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_BILLING_DOCUMENT_TYPE), String)
            End If
        End Get
    End Property

    Public Property PremiumAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PREMIUM_AMOUNT) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_PREMIUM_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_PREMIUM_AMOUNT, Value)
        End Set
    End Property
    Public Property AppleCareFee() As DecimalType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_APPLECARE_FEE) Is DBNull.Value Then
                Return New DecimalType(0)
            Else
                Return New DecimalType(CType(Row(CertificateDAL.COL_NAME_APPLECARE_FEE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_NAME_APPLECARE_FEE, Value)
        End Set
    End Property

    Public ReadOnly Property InsuranceOrderNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_INSURANCE_ORDER_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_INSURANCE_ORDER_NUMBER), String)
            End If
        End Get
    End Property

    Public ReadOnly Property DeviceOrderNumber() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_DEVICE_ORDER_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_DEVICE_ORDER_NUMBER), String)
            End If
        End Get
    End Property

    Public ReadOnly Property UpgradeType() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_UPGRADE_TYPE), String)
            End If
        End Get
    End Property

    Public ReadOnly Property FulfillmentConsentAction() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_FULFILLMENT_CONSENT_ACTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_FULFILLMENT_CONSENT_ACTION), String)
            End If
        End Get
    End Property

    Public ReadOnly Property PlanType() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PLAN_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_PLAN_TYPE), String)
            End If
        End Get
    End Property

    Public ReadOnly Property WaitingPeriodEndDate() As DateType
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_WAITING_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertificateDAL.COL_NAME_WAITING_PERIOD_END_DATE).ToString()))
            End If
        End Get
    End Property

    Public Property PreviousCertificateId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_PREVIOUS_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_PREVIOUS_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.Dealer = Nothing
            Me.SetValue(CertificateDAL.COL_NAME_PREVIOUS_CERT_ID, Value)
        End Set
    End Property
    Public Property OriginalCertificateId() As Guid
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_ORIGINAL_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertificateDAL.COL_NAME_ORIGINAL_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.Dealer = Nothing
            Me.SetValue(CertificateDAL.COL_NAME_ORIGINAL_CERT_ID, Value)
        End Set
    End Property

    Public ReadOnly Property UpgradeProgram() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_NAME_UPGRADE_PROGRAM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_NAME_UPGRADE_PROGRAM), String)
            End If
        End Get
    End Property

#End Region

#Region "Address Children"

    Public ReadOnly Property AddressChild(Optional ByVal Edit_Mode As Boolean = True) As Address
        Get
            Dim newAddress As Address
            If Me.AddressId.Equals(Guid.Empty) Then
                If Edit_Mode = True Then
                    newAddress = New Address(Me.Dataset, Me)
                    Dim oCompany As New Company(Me.CompanyId)
                    newAddress.CountryId = oCompany.BusinessCountryId
                    ' Me.AddressId = newAddress.Id
                    Return newAddress
                Else
                    Return newAddress
                End If
            Else
                Return New Address(Me.AddressId, Me.Dataset, Me)
            End If
        End Get
    End Property

    Private _MailingAddress As Address = Nothing
    Public ReadOnly Property MailingAddress() As Address
        Get
            If Me._MailingAddress Is Nothing Then
                If Me.MailingAddressId.Equals(Guid.Empty) Then
                    'If Me.IsNew Then
                    Me._MailingAddress = New Address(Me.Dataset, Nothing)
                    Dim oCompany As New Company(Me.CompanyId)
                    _MailingAddress.CountryId = oCompany.BusinessCountryId
                    '   Me.MailingAddressId = Me._MailingAddress.Id
                    'End If
                Else
                    Me._MailingAddress = New Address(Me.MailingAddressId, Me.Dataset, Nothing)
                End If
            End If
            Return Me._MailingAddress
        End Get
    End Property


    Public ReadOnly Property CertificateIsRestricted() As Boolean

        Get
            Dim bRetval As Boolean
            If Row(CertificateDAL.COL_IS_RESTRICTED) Is DBNull.Value Then
                bRetval = False
            ElseIf Row(CertificateDAL.COL_IS_RESTRICTED) = "Y" Then
                bRetval = True
            ElseIf Row(CertificateDAL.COL_IS_RESTRICTED) = "N" Then
                bRetval = False
            ElseIf Row(CertificateDAL.COL_IS_RESTRICTED) = "D" Then
                bRetval = False
            Else

                Throw New BOValidationException("Error:", ElitaPlus.Common.ErrorCodes.BO_INVALID_DATA)
            End If
            Return bRetval
        End Get

    End Property
    Public Property IsRestricted() As String
        Get
            CheckDeleted()
            If Row(CertificateDAL.COL_IS_RESTRICTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertificateDAL.COL_IS_RESTRICTED), String)
            End If

        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertificateDAL.COL_IS_RESTRICTED, Value)
        End Set

    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Dim addressDeleted As Boolean = False
        Dim addressObj As Address = Me.AddressChild
        Try
            If addressObj.IsEmpty Then
                addressObj.Delete()
                Me.AddressId = Nothing
                addressDeleted = True
            ElseIf addressObj.IsDirty Then
                Me.CustomerinfoLastchangeDate = Now
            End If

            'check if address has source and source id populated
            If Not addressObj Is Nothing AndAlso addressDeleted = False AndAlso addressObj.Source = String.Empty Then
                addressObj.Source = "CERT"
                addressObj.SourceId = Me.Id
            End If

            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertificateDAL
                Dim modified_by As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                'dal.Update(Me.Row)
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Start: Update customer information in Customer table
                ' Note: In future this should be moved to Customer DAL when Certificate screen is revamped
                If Not Me.CustomerId.Equals(Guid.Empty) Then
                    dal.UpdateCustomerDetails(Me.Id, Me.CustomerId, Me.SalutationId, Me.CustomerFirstName, Me.CustomerMiddleName, Me.CustomerLastName, modified_by, Me.Email, Me.HomePhone, Me.IdentificationNumber, Me.IdentificationNumberType, Me.WorkPhone, Me.MaritalStatus, Me.Nationality, Me.PlaceOfBirth, Me.Gender, Me.CorporateName, Me.AlternativeFirstName, Me.AlternativeLastName, Me.DateOfBirth)
                End If
                'END: Update customer information in Customer table
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If

            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            If addressDeleted Then
                'Rollback adress delete operation
                addressObj.RejectChanges()
                Me.AddressId = addressObj.Id
            End If
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse
            (Not Me.AddressChild.IsNew And Me.AddressChild.IsDirty) OrElse
            (Me.AddressChild.IsNew And Not Me.AddressChild.IsEmpty)
        End Get
    End Property

    Public Sub ValidateCancelRequest(ByVal certCancelReuestBO As CertCancelRequest, ByVal CancReqCommentBO As Comment, ByVal oCertCancelRequestData As CertCancelRequestData, ByVal useExistingBankInfo As String, ByVal cancReqBankInfo As BankInfo)
        Dim oCancellatioReason As Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason
        Dim TodayDate As Date, CancelRulesForSFR As String
        Dim attvalue As AttributeValue = Me.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_CANCEL_RULES_FOR_SFR).FirstOrDefault
        If Not attvalue Is Nothing Then
            CancelRulesForSFR = attvalue.Value
        End If
        oCertCancelRequestData.errorExist = False
        If (IsNothing(certCancelReuestBO.CancellationReasonId) Or certCancelReuestBO.CancellationReasonId.Equals(Guid.Empty)) Then
            oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE
            oCertCancelRequestData.errorExist = True
            Exit Sub
        End If

        If Not oCertCancelRequestData.errorExist Then
            If certCancelReuestBO.CancellationRequestDate Is Nothing Then
                oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE
                oCertCancelRequestData.errorExist = True
                Exit Sub
            End If

            'Invalid cancellation request date
            If (certCancelReuestBO.CancellationRequestDate < Me.WarrantySalesDate.Value) Then
                oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE
                oCertCancelRequestData.errorExist = True
                Exit Sub
            Else
                If (certCancelReuestBO.CancellationRequestDate > TodayDate.Today) Then
                    oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCEL_REQUEST_DATE
                    oCertCancelRequestData.errorExist = True
                    Exit Sub
                End If
            End If
        End If

        If CancelRulesForSFR = Codes.YESNO_N Then
            If Not oCertCancelRequestData.errorExist Then
                If certCancelReuestBO.CancellationDate Is Nothing Then
                    oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
                    oCertCancelRequestData.errorExist = True
                    Exit Sub
                End If

                'Invalid cancellation date
                If Not (certCancelReuestBO.CancellationDate >= Me.WarrantySalesDate.Value AndAlso
                    certCancelReuestBO.CancellationDate <= TodayDate.Today) Then
                    oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
                    oCertCancelRequestData.errorExist = True
                    Exit Sub
                End If
            End If
        End If

        If (Not oCertCancelRequestData.errorExist) And useExistingBankInfo = Codes.YESNO_N Then
            Try
                If cancReqBankInfo.IbanNumber <> String.Empty Then
                    cancReqBankInfo.Validate()
                End If

            Catch ex As ApplicationException
                oCertCancelRequestData.errorCode = Common.ErrorCodes.INVALID_BANKIBANNO_INVALID
                oCertCancelRequestData.errorExist = True
            End Try

        End If


        If Not oCertCancelRequestData.errorExist Then
            If CancReqCommentBO.CommentTypeId.Equals(Guid.Empty) Then
                oCertCancelRequestData.errorCode = Common.ErrorCodes.GUI_JUSTIFICATION_MUST_BE_SELECTED_ERR
                oCertCancelRequestData.errorExist = True
            End If
        End If

        If Not oCertCancelRequestData.errorExist Then
            If CancReqCommentBO.CallerName = String.Empty Then
                oCertCancelRequestData.errorCode = Common.ErrorCodes.MSG_CALLER_NAME_REQUIRED
                oCertCancelRequestData.errorExist = True
            End If
        End If

        If Not oCertCancelRequestData.errorExist Then
            If CancReqCommentBO.Comments = String.Empty Then
                oCertCancelRequestData.errorCode = Common.ErrorCodes.GUI_COMMENTS_ARE_REQUIRED
                oCertCancelRequestData.errorExist = True
            End If
        End If

        If Not oCertCancelRequestData.errorExist Then

            With oCertCancelRequestData
                .certId = Me.Id
            End With
        End If
    End Sub

    Public Sub ProcessCancelRequest(ByVal oCertCancelRequestBO As CertCancelRequest, ByVal useExistingBankInfo As String, ByVal oCRequestBankInfoBO As BankInfo, ByVal oCancReqCommentBO As Comment, ByVal oCertCancelRequestData As CertCancelRequestData, ByRef dblRefundAmount As Double, ByRef strMsg As String)
        CertCancelRequest.SetProcessCancelRequestData(oCertCancelRequestBO, useExistingBankInfo, oCRequestBankInfoBO, oCancReqCommentBO, oCertCancelRequestData)
        oCertCancelRequestBO.CertCancelRequest(oCertCancelRequestData, dblRefundAmount, strMsg)
    End Sub

    Public Sub QuoteCancellation(ByVal certCancellationBO As CertCancellation,
                                      ByVal oCancelCertificateData As CertCancellationData, Optional ByVal ContractBO As Contract = Nothing)
        Dim TodayDate As Date
        Dim oCancellatioReason As Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason
        Dim oCertInstallment As Assurant.ElitaPlus.BusinessObjectsNew.CertInstallment
        Dim refundMDv As DataView
        Dim BillingStatusDv As DataView
        Dim inputAmountRequired As Boolean
        Dim monthlyBilling As Boolean
        refundMDv = LookupListNew.GetRefundComputeMethodLookupList(Authentication.LangId)
        BillingStatusDv = LookupListNew.GetBillingStatusList(Authentication.LangId)
        oCancelCertificateData.certificatestatus = Me.StatusCode
        oCancelCertificateData.errorExist = False



        'Invalid cancellation date
        If (certCancellationBO.CancellationDate Is Nothing AndAlso Not Microsoft.VisualBasic.IsDate(certCancellationBO.CancellationDate)) Then
            oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
            oCancelCertificateData.errorExist = True
        End If

        If Not oCancelCertificateData.errorExist Then
            If Not (certCancellationBO.CancellationReasonId.Equals(Guid.Empty)) Then
                ' Cancellation Reason
                oCancellatioReason = New Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason(certCancellationBO.CancellationReasonId)
                oCancelCertificateData.cancellationCode = oCancellatioReason.Code
                oCancelCertificateData.refund_dest_id = oCancellatioReason.RefundDestinationId
                oCancelCertificateData.def_refund_payment_method_id = oCancellatioReason.DefRefundPaymentMethodId
                oCancelCertificateData.ComputeRefundCode = LookupListNew.GetCodeFromId(refundMDv, oCancellatioReason.RefundComputeMethodId)
                paymentTypeCode = getPaymentTypeCode
                oCancelCertificateData.paymentTypeCode = paymentTypeCode
                oCancelCertificateData.cancellationDate = certCancellationBO.CancellationDate

                If (Me.paymentTypeCode.ToString = PAYMENT_BY_DIRECT_DEBIT Or Me.paymentTypeCode.ToString = PAYMENT_PRE_AUTHORIZED) Then
                    Try
                        oCertInstallment = New CertInstallment(Me.Id, True)
                    Catch ex As Exception
                        oCertInstallment = New CertInstallment()
                    End Try

                    If (oCertInstallment Is Nothing Or oCertInstallment.BillingStatusId.Equals(LookupListNew.GetIdFromCode(BillingStatusDv, Codes.IN_COLLECTION))) And
                            oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__22)) Then
                        'Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.ERR_MSG_CERT_INSTALLMENT_INCOLLECTION_STATUS)
                        oCancelCertificateData.errorCode = Common.ErrorCodes.ERR_MSG_CERT_INSTALLMENT_INCOLLECTION_STATUS
                        oCancelCertificateData.errorExist = True
                    End If
                End If

                If (Not oCancelCertificateData.errorExist And Me.paymentTypeCode.ToString = PAYMENT_PRE_AUTHORIZED And Not oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__22))) Then
                    'Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.ERR_MSG_INVALID_CANCEL_REASON_CODE_FOR_PRE_AUTH_CERTS)
                    oCancelCertificateData.errorCode = Common.ErrorCodes.ERR_MSG_INVALID_CANCEL_REASON_CODE_FOR_PRE_AUTH_CERTS
                    oCancelCertificateData.errorExist = True
                End If

                If (Not oCancelCertificateData.errorExist) And (Not Me.paymentTypeCode Is Nothing) AndAlso (Me.paymentTypeCode.ToString = PAYMENT_BY_DIRECT_DEBIT) Then
                    If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__16))) Then
                        If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__17))) Then
                            If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__20))) Then
                                If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__21))) Then
                                    If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__2))) Then
                                        If Not (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__22))) Then
                                            'If oCancelCertificateData.cancellationCode <> CANCELLATION_REASON_CODE_16 And oCancelCertificateData.cancellationCode <> CANCELLATION_REASON_CODE_17 Then
                                            oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE
                                            oCancelCertificateData.errorExist = True
                                        End If

                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                Dim isClaimOrOfficeManager As Boolean =
                    (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                     ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__OFFICE_MANAGER) OrElse
                     ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT))
                If Not isClaimOrOfficeManager Then
                    Dim dtMaxLossDate As Date = Me.MaxClaimLossDateForCertificate(Me.Id)
                    If Not certCancellationBO.CancellationDate.Value > dtMaxLossDate Then
                        oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE
                        oCancelCertificateData.errorExist = True
                    End If
                End If

                ' Based on Refund Compute Method
                'refundMDv = LookupListNew.GetRefundComputeMethodLookupList(Authentication.LangId)
                If (oCancellatioReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__12))) Then
                    If (Certificate.AreThereClaimsForCancelCert(Me.DealerId, Me.CertNumber) = True) Then
                        oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS
                        oCancelCertificateData.errorExist = True
                    End If
                End If
            Else
                oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE
                oCancelCertificateData.errorExist = True
            End If
        End If


        oCancelCertificateData.errorExist2 = oCancelCertificateData.errorExist
        If Not oCancelCertificateData.errorExist Then

            With oCancelCertificateData
                .companyId = Me.CompanyId
                .dealerId = Me.DealerId
                .certificatestatus = Me.StatusCode
                .certificate = Me.CertNumber
                .source = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                .cancellationDate = certCancellationBO.CancellationDate.Value
                .quote = "Y"
            End With

            Dim oYesList As DataView = LookupListNew.GetListItemId(oCancellatioReason.InputAmtReqId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString

            If oYesNo = "Y" Then
                inputAmountRequired = True
            Else
                inputAmountRequired = False
            End If

            'Invalid cancellation date

            Dim attValueComputeCancellation As AttributeValue = Me.Dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_COMPUTE_CANCELLATION_DATE_AS_EOFMONTH).FirstOrDefault

            If Me.PreviousCertificateId.Equals(Guid.Empty) Then
                If attValueComputeCancellation Is Nothing OrElse attValueComputeCancellation.Value = Codes.YESNO_N Then
                    If Not (oCancelCertificateData.cancellationDate >= Me.WarrantySalesDate.Value AndAlso
                            oCancelCertificateData.cancellationDate <= TodayDate.Today) Then
                        oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
                        oCancelCertificateData.inputAmountRequiredMissing = True
                        oCancelCertificateData.errorExist2 = True
                    End If
                Else
                    If Not (oCancelCertificateData.cancellationDate >= Me.WarrantySalesDate.Value AndAlso
                            certCancellationBO.CancellationRequestedDate <= TodayDate.Today) Then
                        oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
                        oCancelCertificateData.inputAmountRequiredMissing = True
                        oCancelCertificateData.errorExist2 = True
                    End If
                End If
            Else
                If (oCancelCertificateData.cancellationDate < Me.WarrantySalesDate.Value) Then
                    oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_DATE
                    oCancelCertificateData.inputAmountRequiredMissing = True
                    oCancelCertificateData.errorExist2 = True
                End If
            End If

            'Input Amount Required and no monthly payments

            If Not Me.MonthlyPayments Is Nothing Then
                If (inputAmountRequired AndAlso Me.MonthlyPayments.Value = 0) Then
                    oCancelCertificateData.inputAmountRequiredMissing = True
                    oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE
                    oCancelCertificateData.errorExist2 = True

                End If
            Else
                If inputAmountRequired Then
                    oCancelCertificateData.inputAmountRequiredMissing = True
                    oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE
                    oCancelCertificateData.errorExist2 = True

                End If
            End If

            'Invalid entered amount
            If inputAmountRequired And (oCancelCertificateData.customerPaid > Convert.ToDecimal(0)) _
    And (oCancelCertificateData.customerPaid > oCancelCertificateData.grossAmtReceived) Then
                oCancelCertificateData.inputAmountRequiredMissing = True
                oCancelCertificateData.errorCode = Common.ErrorCodes.MSG_INVALID_AMOUNT_ENTERED
                oCancelCertificateData.errorExist2 = True

            End If
            If Not oCancelCertificateData.inputAmountRequiredMissing Then
                certCancellationBO.CancelCertificate(oCancelCertificateData)
                If oCancelCertificateData.refundAmount < 0 Then
                    oCancelCertificateData.refundAmount = 0
                End If
            End If 'Not inputAmountRequiredMissing
        End If 'Not errorExist
    End Sub

    Public Sub ProcessCancellation(ByVal oCertCanc As CertCancellation, ByVal oBankInfo As BankInfo, ByVal oContract As Contract,
                                    Optional ByVal oCertCancelComment As Comment = Nothing, Optional ByVal oCancelCertRstData As CertCancellationData = Nothing,
                                    Optional ByVal oPaymentOrderInfo As PaymentOrderInfo = Nothing)
        Dim oCancelCertificateData As New CertCancellationData
        Dim oBankInfoData As BankInfoData
        Dim oCommentData As CommentData


        CertCancellation.SetProcessCancellationData(oCancelCertificateData, Me, oCertCanc)
        If Not oBankInfo Is Nothing Then
            oBankInfoData = New BankInfoData
            BankInfo.SetProcessCancellationData(oBankInfoData, oBankInfo)
        ElseIf Not oPaymentOrderInfo Is Nothing Then
            oBankInfoData = New BankInfoData
            PaymentOrderInfo.SetProcessCancellationData(oBankInfoData, oPaymentOrderInfo)
        Else
            oBankInfoData = Nothing
        End If
        If Not oCertCancelComment Is Nothing Then
            oCommentData = New CommentData
            Comment.SetProcessCancellationData(oCommentData, oCertCancelComment)
        Else
            oCommentData = Nothing
        End If

        oCertCanc.CancelCertificate(oCancelCertificateData, oBankInfoData, oCommentData)
        ' oCancelCertRstData = oCancelCertificateData

        oCertCanc.ErrorCode = oCancelCertificateData.errorCode
        oCertCanc.ErrorMsg = oCancelCertificateData.ErrorMsg
        oCertCanc.CCAuthorizationNumber = oCancelCertificateData.AuthNumber


    End Sub

    Public Sub SetFutureCancellation(ByVal oCertCanc As CertCancellation, ByVal oBankInfo As BankInfo, ByVal oContract As Contract,
                                    Optional ByVal oCertCancelComment As Comment = Nothing, Optional ByVal oCancelCertRstData As CertCancellationData = Nothing,
                                    Optional ByVal oPaymentOrderInfo As PaymentOrderInfo = Nothing)
        Dim oCancelCertificateData As New CertCancellationData
        Dim oBankInfoData As BankInfoData
        Dim oCommentData As CommentData


        CertCancellation.SetProcessCancellationData(oCancelCertificateData, Me, oCertCanc)

        If Not oBankInfo Is Nothing Then
            oBankInfoData = New BankInfoData
            BankInfo.SetProcessCancellationData(oBankInfoData, oBankInfo)
        ElseIf Not oPaymentOrderInfo Is Nothing Then
            oBankInfoData = New BankInfoData
            PaymentOrderInfo.SetProcessCancellationData(oBankInfoData, oPaymentOrderInfo)
        Else
            oBankInfoData = Nothing
        End If

        If Not oCertCancelComment Is Nothing Then
            oCommentData = New CommentData
            Comment.SetProcessCancellationData(oCommentData, oCertCancelComment)
        Else
            oCommentData = Nothing
        End If

        oCertCanc.SetFutureCancelCertificate(oCancelCertificateData, oBankInfoData, oCommentData, oCertCanc.CancellationRequestedDate)
        ' oCancelCertRstData = oCancelCertificateData

        oCertCanc.ErrorCode = oCancelCertificateData.errorCode
        oCertCanc.ErrorMsg = oCancelCertificateData.ErrorMsg
        oCertCanc.CCAuthorizationNumber = oCancelCertificateData.AuthNumber


    End Sub

    Public Shared Function GetCetificateByCertNumber(ByVal dealerId As Guid, ByVal certNum As String)
        Dim dal As New CertificateDAL
        Dim dsCert As DataSet = dal.GetCertIDWithCertNumAndDealer(certNum, dealerId)

        Dim certId As Guid = Guid.Empty
        Dim certObj As Certificate = Nothing

        If Not dsCert Is Nothing AndAlso dsCert.Tables.Count > 0 AndAlso dsCert.Tables(0).Rows.Count = 1 Then
            If Not dsCert.Tables(0).Rows(0).Item("cert_id") Is DBNull.Value Then
                certId = New Guid(CType(dsCert.Tables(0).Rows(0).Item("cert_id"), Byte()))
                If Not certId.Equals(Guid.Empty) Then
                    certObj = New Certificate(certId)
                End If
            End If
        End If
        Return certObj
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    '5525
    'Public ReadOnly Property 
    'GetFinancialAmountprodcode() As Decimal
    '    Get
    '        Dim oPrd As New ProductCode
    '        Dim OutstandingBalanceCompMethodCode As String
    '        'get the configured dealer code
    '        'configureDealer

    '        OutstandingBalanceCompMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_UPG_FINANCE_BAL_COMP_METH, oPrd.UPGFinanceBalCompMethId)

    '        Return GetFinancialAmount(OutstandingBalanceCompMethodCode)
    '    End Get

    'End Property

    Public ReadOnly Property GetFinancialAmountprodcode() As Decimal
        Get
            Dim serialNumber As String = Me.Items.OrderByDescending(Function(i) i.EffectiveDate).First().SerialNumber
            Return GetFinancialAmount(serialNumber)
        End Get

    End Property

    Public Function GetTotalOverallPayments() As Integer
        Dim dal As New CertificateDAL
        Return dal.GetTotalOverallPayments(Me.Id)
    End Function



    Public Function GetMonthlyGrossAmount(ByVal cert_id As Guid) As Decimal
        Dim Dal As New CertificateDAL
        Return Dal.GetMonthlygrossAmount(cert_id)
    End Function
    '5525
    Public Function GetMonthsPassedForH3GI(PymtActDate As Date) As Integer
        Dim dal As New CertificateDAL
        Dim intMonthsPassed As Integer
        intMonthsPassed = dal.GetMonthsPassedForH3GI(PymtActDate)
        Return intMonthsPassed
    End Function
    Public Function getCertCancellationDate() As Date
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dv As DataView

        ds = dal.getCertCancellationDate(Me.Id)
        dv = ds.Tables(CertificateDAL.TABLE_CANCEL_DATE).DefaultView
        Return CType(dv(0)(0), Date)
    End Function

    Public Shared Function GetCertificatesList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                                ByVal addressMask As String, ByVal postalCodeMask As String,
                                ByVal taxIdMask As String, ByVal dealerName As String,
                                Optional ByVal sortBy As String = CertificateDAL.SORT_BY_CUSTOMER_NAME,
                                Optional ByVal LimitResultset As Int32 = CertificateDAL.MAX_NUMBER_OF_ROWS,
                                Optional ByVal accountNum As String = "",
                                Optional ByVal certStatus As String = "",
                                Optional ByVal invoiceNum As String = "") As CertificateSearchDV
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim dealerGroupCode As String

            'External user by dealer will only allow search by the user's own dealer
            With ElitaPlusIdentity.Current.ActiveUser
                If .IsDealer Then
                    Dim ExternalUserDealerName As String = LookupListNew.GetCodeFromId("DEALERS", .ScDealerId)
                    If dealerName.Trim = String.Empty Then
                        dealerName = ExternalUserDealerName
                    Else
                        If ExternalUserDealerName <> dealerName.Trim Then
                            Dim Errs() As ValidationError = {New ValidationError("INVALID_DEALER", GetType(Certificate), Nothing, "Search", Nothing)}
                            Throw New BOValidationException(Errs, GetType(Certificate).FullName)
                        End If
                    End If
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            If (certNumberMask.Equals(String.Empty) AndAlso customerNameMask.Equals(String.Empty) AndAlso
                addressMask.Equals(String.Empty) AndAlso postalCodeMask.Equals(String.Empty) AndAlso
                taxIdMask.Equals(String.Empty) AndAlso (dealerName Is Nothing) AndAlso
                accountNum = "" AndAlso certStatus = "" AndAlso invoiceNum = "") Then
                Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return New CertificateSearchDV(dal.LoadList(certNumberMask, customerNameMask,
                                     addressMask, postalCodeMask, taxIdMask, dealerName, compIds, sortBy, LimitResultset, accountNum, certStatus, invoiceNum, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    ''' <summary>
    ''' For Req-860.  A combined certificate search screen that returns all the the columns in all the search screens.
    ''' </summary>
    ''' <param name="PhoneTypeMask"></param>
    ''' <param name="PhoneMask"></param>
    ''' <param name="certNumberMask"></param>
    ''' <param name="customerNameMask"></param>
    ''' <param name="addressMask"></param>
    ''' <param name="postalCodeMask"></param>
    ''' <param name="dealerName"></param>
    ''' <param name="certStatus"></param>
    ''' <param name="taxId"></param>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="accountNumber"></param>
    ''' <param name="serialNumber"></param>
    ''' <param name="isVSCSearch"></param>
    ''' <param name="vehicleLicenseNumber"></param>
    ''' <param name="sortBy"></param>
    ''' <param name="LimitResultset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCombinedCertificatesList(ByVal PhoneTypeMask As String,
                                                       ByVal PhoneMask As String,
                                                       ByVal certNumberMask As String,
                                                       ByVal customerNameMask As String,
                                                       ByVal addressMask As String,
                                                       ByVal postalCodeMask As String,
                                                       ByVal dealerName As String,
                                                       ByVal certStatus As String,
                                                       ByVal taxId As String,
                                                       ByVal invoiceNumber As String,
                                                       ByVal accountNumber As String,
                                                       ByVal serialNumber As String,
                                                       ByVal isVSCSearch As Boolean,
                                                       ByVal inforceDate As String,
                                                       Optional ByVal vehicleLicenseNumber As String = "",
                                                       Optional ByVal Service_line_number As String = "",
                                                       Optional ByVal isDPO As Boolean = True,
                                                       Optional ByVal sortBy As String = CertificateDAL.SORT_BY_CUSTOMER_NAME,
                                                       Optional ByVal LimitResultset As Int32 = CertificateDAL.MAX_NUMBER_OF_ROWS
                                                       ) As CombinedCertificateSearchDV

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim dealergroup As String = ""
            Dim dsSearchResult As DataTable

            'External user by dealer will only allow search by the user's own dealer
            With ElitaPlusIdentity.Current.ActiveUser
                If .IsDealer Then
                    Dim ExternalUserDealerName As String = LookupListNew.GetCodeFromId("DEALERS", .ScDealerId)
                    If String.IsNullOrWhiteSpace(dealerName) Then
                        dealerName = ExternalUserDealerName
                    Else
                        If ExternalUserDealerName <> dealerName.Trim Then
                            Dim Errs() As ValidationError = {New ValidationError("INVALID_DEALER", GetType(Certificate), Nothing, "Search", Nothing)}
                            Throw New BOValidationException(Errs, GetType(Certificate).FullName)
                        End If
                    End If
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealergroup = ExternalUserDealergroup
                End If
            End With

            If (certNumberMask.Equals(String.Empty) AndAlso
                customerNameMask.Equals(String.Empty) AndAlso
                taxId.Equals(String.Empty) AndAlso
                invoiceNumber.Equals(String.Empty) AndAlso
                accountNumber.Equals(String.Empty) AndAlso
                (dealerName Is Nothing) AndAlso
                certStatus = "" AndAlso
                PhoneMask.Equals(String.Empty) AndAlso
                serialNumber.Equals(String.Empty) AndAlso
                vehicleLicenseNumber.Equals(String.Empty) AndAlso
                addressMask.Equals(String.Empty) AndAlso
                postalCodeMask.Equals(String.Empty) AndAlso
                Service_line_number.Equals(String.Empty) AndAlso
                vehicleLicenseNumber.Equals(String.Empty)
                    ) Then
                Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Dim isInValidData = False
            ''Def-24283.
            'Show error message if entering wild card "*" in any field in the certificate search screen and keeping other fields blank.

            If (dealerName Is Nothing AndAlso
                   (certNumberMask.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(certNumberMask, SEARCH_REGEX) AndAlso certNumberMask.Length = 1)) AndAlso
                   (customerNameMask.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(customerNameMask, SEARCH_REGEX) AndAlso customerNameMask.Length = 1)) AndAlso
                   (taxId.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(taxId, SEARCH_REGEX) AndAlso taxId.Length = 1)) AndAlso
                   (invoiceNumber.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(invoiceNumber, SEARCH_REGEX) AndAlso invoiceNumber.Length = 1)) AndAlso
                   (accountNumber.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(accountNumber, SEARCH_REGEX) AndAlso accountNumber.Length = 1)) AndAlso
                   (addressMask.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(addressMask, SEARCH_REGEX) AndAlso addressMask.Length = 1)) AndAlso
                   (serialNumber.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(serialNumber, SEARCH_REGEX) AndAlso serialNumber.Length = 1)) AndAlso
                   (Service_line_number.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(Service_line_number, SEARCH_REGEX) AndAlso Service_line_number.Length = 1)) AndAlso
                   (PhoneMask.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(PhoneMask, SEARCH_REGEX) AndAlso PhoneMask.Length = 1)) AndAlso
                   (postalCodeMask.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(postalCodeMask, SEARCH_REGEX) AndAlso postalCodeMask.Length = 1)) AndAlso
                   (vehicleLicenseNumber.Equals(String.Empty) Or (System.Text.RegularExpressions.Regex.IsMatch(vehicleLicenseNumber, SEARCH_REGEX) AndAlso vehicleLicenseNumber.Length = 1))) Then
                isInValidData = True
                Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION_INFORCE_DATE, GetType(Certificate), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Certificate).FullName)

            End If


            If Not inforceDate.Equals(String.Empty) Then
                Dim vardt As Date
                'If Not Date.TryParse(inforceDate, _
                '                System.Threading.Thread.CurrentThread.CurrentCulture, _
                '                System.Globalization.DateTimeStyles.NoCurrentDateDefault, vardt) Then
                If DateHelper.GetDateValue(inforceDate) = DateTime.MinValue Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("INFORCE_DATE") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(Certificate), Nothing, "Inforce Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(Certificate).FullName)
                End If

            End If

            dsSearchResult = dal.LoadCombinedList(
                                                        PhoneTypeMask,
                                                        PhoneMask,
                                                        certNumberMask,
                                                        customerNameMask,
                                                        addressMask,
                                                        postalCodeMask,
                                                        dealerName,
                                                        certStatus,
                                                        taxId,
                                                        invoiceNumber,
                                                        accountNumber,
                                                        serialNumber,
                                                        isVSCSearch,
                                                        compIds,
                                                        sortBy,
                                                        LimitResultset,
                                                        inforceDate,
                                                        vehicleLicenseNumber,
                                                        Service_line_number,
                                                        dealergroup).Tables(0)
            Dim dv As DataView
            dv = New DataView(dsSearchResult)
            'If isDPO = False Then
            '    dv.RowFilter = "is_restricted='N'  or is_restricted='' or is_restricted is null"
            '   End If

            Return New CombinedCertificateSearchDV(dv.ToTable())

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    ''Def-24383
    'Added new function to check wild card "*" in any field in the certificate search screen.
    'Private Shared Function ValidateSearch(ByRef str As String) As Boolean
    '    If (Not str Is Nothing) AndAlso (Not (str.Equals(String.Empty))) Then
    '        If (str.Trim.Equals(WILDCARD_CHAR) OrElse (str.Trim.Equals(ASTERISK))) Then
    '            Return (True)
    '        End If
    '    End If
    '    Return (False)
    'End Function

    Public Shared Function GetCertificatesForCancellationList(ByVal RequestNumber As Int32, ByVal DealerId As Guid, ByVal SortBy As Integer, ByVal SortOrder As Integer, ByVal forCancellation As String,
                                                              Optional ByVal BranchCode As String = Nothing,
                                                              Optional ByVal CertificateNumber As String = Nothing,
                                                              Optional ByVal CustomerName As String = Nothing,
                                                              Optional ByVal Email As String = Nothing) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If Dealer.GetOlitaSearchType(DealerId).Equals(Codes.OLITA_SEARCH_GENERIC) Then
                If Not BranchCode Is Nothing AndAlso Not BranchCode.Equals(String.Empty) Then BranchCode += "*"
                If Not CertificateNumber Is Nothing AndAlso Not CertificateNumber.Equals(String.Empty) Then CertificateNumber += "*"
                If Not CustomerName Is Nothing AndAlso Not CustomerName.Equals(String.Empty) Then CustomerName += "*"
                If Not Email Is Nothing AndAlso Not Email.Equals(String.Empty) Then Email += "*"
            End If

            Dim LimitResultSet_Low, LimitResultSet_High As Integer

            LimitResultSet_Low = CType(RequestNumber.ToString & "00", Integer)
            LimitResultSet_High = CType((RequestNumber + 1).ToString & "01", Integer)


            Return dal.LoadCertificatesForCancellation(LimitResultSet_Low, LimitResultSet_High, DealerId, SortBy, SortOrder, forCancellation, BranchCode, CertificateNumber, CustomerName, Email)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetMarkupAndCommissionList(ByVal RequestNumber As Int16,
                                                      ByVal DealerId As Guid,
                                                      Optional ByVal BeginDate As DateTime = Nothing,
                                                      Optional ByVal EndDate As DateTime = Nothing,
                                                      Optional ByVal CertificateNumber As String = Nothing) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            'If Not CertificateNumber Is Nothing AndAlso Not CertificateNumber.Equals(String.Empty) Then CertificateNumber += "*"



            Dim LimitResultSet_Low, LimitResultSet_High As Integer

            LimitResultSet_Low = CType(RequestNumber.ToString & "00", Integer)
            LimitResultSet_High = CType((RequestNumber + 1).ToString & "01", Integer)


            Return dal.LoadMarkupAndCommissionList(compIds, LimitResultSet_Low, LimitResultSet_High, DealerId, BeginDate, EndDate, CertificateNumber)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertificateForCancellation(ByVal DealerId As Guid, ByVal CertificateNumber As String, ByVal forCancellation As String) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            Return dal.LoadCertificateForCancellation(DealerId, CertificateNumber, forCancellation)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertNumFromCertId(ByVal Id As Guid) As DataSet
        Try
            Dim dal As New CertificateDAL
            Return dal.getCertNum(Id)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertInfoWEPPTMX(ByVal dealerCode As String, ByVal certNumber As String, ByVal PhoneNum As String, ByVal serialNum As String) As DataSet
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim dal As New CertificateDAL
        Return dal.LoadCertInfoWEPPTMX(dealerCode, certNumber, PhoneNum, serialNum, compIds)
    End Function
    Public Shared Function WS_GetCustomerFunctions(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid) As DataSet
        Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim dal As New CertificateDAL

        Return dal.WS_GetCustomerFunctions(CustomerIdentifier, IdentifierType, DealerId, userId)
    End Function

    Public Shared Function WS_GetCoverageInfo(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid, Optional ByVal BillingZipCode As String = "") As DataSet
        Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim dal As New CertificateDAL

        Return dal.WS_GetCoverageInfo(CustomerIdentifier, IdentifierType, DealerId, userId)
    End Function

    Public Shared Function GetCertsWithActiveClaimByCertNumAndPhone(ByVal certNumber As String, ByVal PhoneNum As String) As DataSet
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim dal As New CertificateDAL
        Return dal.LoadCertsWithActiveClaimByCertNumAndPhone(certNumber, PhoneNum, compIds)
    End Function

    Public Shared Function GetCertificatesListByPhoneNum(ByVal PhoneTypeMask As String, ByVal PhoneMask As String, ByVal certNumberMask As String,
                                                         ByVal customerNameMask As String, ByVal addressMask As String,
                                                         ByVal postalCodeMask As String, ByVal dealerName As String,
                                                         Optional ByVal sortBy As String = CertificateDAL.SORT_BY_PHONE_NUMBER,
                                                         Optional ByVal LimitResultset As Int32 = CertificateDAL.MAX_NUMBER_OF_ROWS) As PhoneNumberSearchDV
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet, dealerGroup As String = ""

            'External user by dealer will only allow search by the user's own dealer
            With ElitaPlusIdentity.Current.ActiveUser
                If .IsDealer Then
                    Dim ExternalUserDealerName As String = LookupListNew.GetCodeFromId("DEALERS", .ScDealerId)
                    If dealerName.Trim = String.Empty Then
                        dealerName = ExternalUserDealerName
                    Else
                        If ExternalUserDealerName <> dealerName.Trim Then
                            Dim Errs() As ValidationError = {New ValidationError("INVALID_DEALER", GetType(Certificate), Nothing, "Search", Nothing)}
                            Throw New BOValidationException(Errs, GetType(Certificate).FullName)
                        End If
                    End If
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroup = ExternalUserDealergroup
                End If
            End With

            If (certNumberMask.Equals(String.Empty) AndAlso customerNameMask.Equals(String.Empty) AndAlso
                addressMask.Equals(String.Empty) AndAlso postalCodeMask.Equals(String.Empty) AndAlso
                PhoneMask.Equals(String.Empty) AndAlso (dealerName Is Nothing)) Then
                Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return New PhoneNumberSearchDV(dal.LoadListByPhoneNum(PhoneTypeMask, PhoneMask, certNumberMask, customerNameMask,
                                     addressMask, postalCodeMask, dealerName, compIds, sortBy, LimitResultset, dealerGroup).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetSerialNumberList(ByVal serialNumberMask As String, ByVal companyGroupId As Guid,
                                               ByVal networkId As String) As SerialNumberSearchDV
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If serialNumberMask.Equals(String.Empty) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return New SerialNumberSearchDV(dal.LoadSerialNumberList(serialNumberMask, companyGroupId, networkId
                                                                     ).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetVehicleLicenseFlagList(ByVal vehicleLicenseFlagMask As String) As VehicleLicenseFlagSearchDV
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If vehicleLicenseFlagMask.Equals(String.Empty) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return New VehicleLicenseFlagSearchDV(dal.LoadVehicleLicenseFlagList(vehicleLicenseFlagMask, compIds).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function PremiumTotals(ByVal certId As Guid) As DataView
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        ds = dal.getPremiumTotals(certId)
        Return ds.Tables(CertificateDAL.TABLE_PREMIUM_TOTALS).DefaultView

    End Function
    Public Shared Function SalesTaxDetail(ByVal certId As Guid) As DataView
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        ds = dal.getSalesTaxDetails(certId)
        Return ds.Tables(CertificateDAL.TABLE_SALES_TAX_DETAILS).DefaultView

    End Function

    Public Shared Function ValidateProductForSpecialServices(ByVal DealerId As Guid, ByVal ProdCode As String) As DataView
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim oPrd As New ProductCode
        Dim dvPrdId As DataView
        Dim oPrdCodeId As Guid

        dvPrdId = oPrd.GetProductCodeId(DealerId, ProdCode)
        oPrdCodeId = New Guid(CType(dvPrdId(0)(0), Byte()))
        ds = dal.ValidateProductForSpecialServices(oPrdCodeId)
        Return ds.Tables(CertificateDAL.TABLE_PROD_SS).DefaultView

    End Function

    Public Function ClaimsForCertificate(ByVal certId As Guid, ByVal languageId As Guid) As CertificateClaimsDV
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        Return New CertificateClaimsDV(dal.getClaimsforCertificate(certId, languageId).Tables(0))

    End Function

    Public Function ClaimsWithExtstatus(ByVal certId As Guid, ByVal dealerId As Guid, ByVal SerialImeiNo As String) As CertificateClaimsDV
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        Return New CertificateClaimsDV(dal.getClaimsWithExtstatus(certId, dealerId, SerialImeiNo).Tables(0))

    End Function

    Public Function MaxClaimLossDateForCertificate(ByVal certId As Guid) As Date
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dtMasLoss As Date = Date.MinValue, intClaimCnt As Integer
        ds = dal.getMaxClaimsLossDate(certId)
        If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            If True Then
                intClaimCnt = CType(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_CLAIM_COUNT), Integer)
                If intClaimCnt > 0 Then
                    dtMasLoss = DateHelper.GetDateValue(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_MAX_LOSS_DATE).ToString())
                End If
            End If
        End If
        Return dtMasLoss

    End Function

    Public Shared Function GetCommissionForEntities(ByVal certId As Guid, ByVal langId As Guid, ByVal commAsOfDate As String) As DataView
        Dim ds As DataSet = Nothing
        Dim dal As New CertificateDAL
        Try
            ds = dal.GetCommissionForEntities(certId, langId, commAsOfDate)
            Return ds.Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function IsReverseCancellationEnabled(ByVal certId As Guid) As Boolean
        Dim dal As New CertificateDAL
        Try
            Return dal.IsReverseCancellationEnabled(certId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function AreThereClaimsForCancelCert(ByVal dealerId As Guid, ByVal certNumber As String) As Boolean
        Dim areThereClaims As Boolean = False
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim intClaimCnt As Integer
        '  ds = dal.ClaimsForCancelCert(dealerId, certNumber)
        ' CancelNotClosedCert
        ds = dal.ClaimsForCancelNotClosedCert(dealerId, certNumber)
        If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            intClaimCnt = CType(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_CLAIM_COUNT), Integer)
            If intClaimCnt > 0 Then
                areThereClaims = True
            End If
        End If
        If areThereClaims = False Then
            ' PaidCert
            ds = dal.ClaimsForCancelPaidCert(dealerId, certNumber)
            If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
                intClaimCnt = CType(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_CLAIM_COUNT), Integer)
                If intClaimCnt > 0 Then
                    areThereClaims = True
                End If
            End If
        End If
        Return areThereClaims

    End Function
    Public Shared Function TotalClaimsNotClosedForCert(ByVal dealerId As Guid, ByVal certNumber As String) As Boolean
        Dim areThereActiveClaims As Boolean = False
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim intClaimCnt As Integer
        ds = dal.TotalClaimsNotClosedForCert(dealerId, certNumber)
        If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            intClaimCnt = CType(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_TOTAL_NUMBER_OF_CLAIMS), Integer)
            If intClaimCnt > 0 Then
                areThereActiveClaims = True
            End If
        End If
        Return areThereActiveClaims
    End Function

    Public Shared Function ActiveClaimExist(ByVal dealerId As Guid, ByVal certNumber As String, ByVal cancelDate As DateTime) As Boolean
        Dim areThereActiveClaims As Boolean = False
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim intClaimCnt As Integer
        ds = dal.ActiveClaimExist(dealerId, certNumber, cancelDate)
        If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            intClaimCnt = CType(ds.Tables(0).Rows(0)(CertificateDAL.COL_NAME_TOTAL_NUMBER_OF_CLAIMS), Integer)
            If intClaimCnt > 0 Then
                areThereActiveClaims = True
            End If
        End If
        Return areThereActiveClaims
    End Function

    Public ReadOnly Property getPaymentTypeDescription() As String

        Get
            Dim dv As DataView = LookupListNew.GetPaymentTypeLookupList()
            paymentTypeDesc = LookupListNew.GetDescriptionFromId(dv, Me.PaymentTypeId)
            Return paymentTypeDesc
        End Get

    End Property

    Public ReadOnly Property getPaymentTypeCode() As String

        Get
            Dim dv As DataView = LookupListNew.GetPaymentTypeLookupList()
            paymentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENT_TYPES, Me.PaymentTypeId)
            Return paymentTypeCode
        End Get

    End Property

    Public ReadOnly Property getCollectionMethodCode() As String

        Get
            If CollectionMethodCode Is Nothing Then
                Dim objPaymentType As New PaymentType(Me.PaymentTypeId)
                CollectionMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_COLLECTION_METHODS, objPaymentType.CollectionMethodId)
            End If

            Return CollectionMethodCode
        End Get

    End Property

    Public ReadOnly Property getPaymentInstrumentCode() As String

        Get
            If PaymentInstrumentCode Is Nothing Then
                Dim objPaymentType As New PaymentType(Me.PaymentTypeId)
                PaymentInstrumentCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENT_INSTRUMENT, objPaymentType.PaymentInstrumentId)
            End If

            Return PaymentInstrumentCode
        End Get

    End Property

    Public ReadOnly Property getPurchaseCurrencyDescription() As String

        Get
            Dim dv As DataView = LookupListNew.GetCurrencyTypeLookupList()
            currencyTypeDesc = LookupListNew.GetDescriptionFromId(dv, Me.PurchaseCurrencyId)
            Return currencyTypeDesc
        End Get

    End Property
    'DEF-1476
    'Public ReadOnly Property getCreditcardTypeDescription() As String

    '    Get
    '        Dim dv As DataView = LookupListNew.GetCreditCardTypeLookupList()
    '        creditCardTypeDesc = LookupListNew.GetDescriptionFromId(dv, Me.CreditcardTypeId)
    '        Return creditCardTypeDesc
    '    End Get

    'End Property
    'End DEF-1476

    Public ReadOnly Property getDealerDescription() As String

        Get
            Dim dv As DataView = LookupListNew.GetDealerLookupList(Me.CompanyId)
            dealerDesc = LookupListNew.GetDescriptionFromId(dv, Me.DealerId)
            Return dealerDesc
        End Get

    End Property

    Public ReadOnly Property getDealerGroupName() As String

        Get
            Dim oDealer As New Dealer(Me.DealerId)
            dealerGroupName = oDealer.DealerGroupName
            Return dealerGroupName
        End Get

    End Property

    Public ReadOnly Property getCancelationRequestFlag() As String

        Get
            Dim oDealer As New Dealer(Me.DealerId)
            getCancelationRequestFlag = LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oDealer.CancellationRequestFlagId)
            Return getCancelationRequestFlag
        End Get

    End Property
    Public ReadOnly Property getProdUpgradeProgramCode() As String
        Get
            If Not Me.Product.UpgradeProgramId.Equals(Guid.Empty) Then
                productUpgradeProgram = LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Me.Product.UpgradeProgramId)
                Return productUpgradeProgram
            Else
                Return String.Empty
            End If
        End Get
    End Property
    Public ReadOnly Property getUpgradeTermUOMCode() As String
        Get
            If Not Me.UpgradeTermUomId.Equals(Guid.Empty) Then
                UpgradeTermUOM = LookupListNew.GetCodeFromId(LookupListNew.LK_UPGRADE_TERM_UNIT_OF_MEASURE, Me.UpgradeTermUomId)
                Return UpgradeTermUOM
            Else
                Return String.Empty
            End If

        End Get
    End Property
    Public ReadOnly Property TheCertCancellationBO() As CertCancellation
        Get
            Dim certCancelId As Guid
            certCancelId = Me.getCertCancelID

            If moCertCancellation Is Nothing AndAlso Not (certCancelId.Equals(Guid.Empty)) Then
                moCertCancellation = New CertCancellation(certCancelId)
            End If

            Return moCertCancellation
        End Get
    End Property

    Public Function getCertCancelID() As Guid
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dv As DataView

        ds = dal.getCertCancellationID(Me.Id)
        dv = ds.Tables(CertificateDAL.TABLE_CANCEL_ID).DefaultView
        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            Return New Guid(CType(dv(0)(0), Byte()))
        End If
    End Function

    Public Function getCertCancelRequestID() As Guid
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dv As DataView

        ds = dal.getCertCancelReqestID(Me.Id)
        dv = ds.Tables(0).DefaultView
        If dv.Count > 0 Then
            Return New Guid(CType(dv(0)(0), Byte()))
        Else
            Return Guid.Empty
        End If
    End Function

    Public Function getCertInstalBankInfoID() As Guid
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dv As DataView

        ds = dal.getCertInstalBankInfoID(Me.Id)
        dv = ds.Tables(CertificateDAL.TABLE_CANCEL_ID).DefaultView
        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            Return New Guid(CType(dv(0)(0), Byte()))
        Else
            Return Guid.Empty
        End If
    End Function
    Public Function getCertTerm() As Integer
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim dv As DataView

        ds = dal.getCertTerm(Me.Id)
        dv = ds.Tables(0).DefaultView
        If dv.Count > 0 Then
            Return (CType(dv(0)(0), Integer))
        End If
    End Function

    'Public Function getClaimWaitingPeriod() As Integer
    '    Dim dal As New CertificateDAL
    '    Dim ds As DataSet
    '    Dim dv As DataView

    '    ds = dal.getClaimWaitingPeriod(Me.Id)
    '    dv = ds.Tables(0).DefaultView
    '    If dv.Count > 0 Then
    '        Return (CType(dv(0)(0), Integer))
    '    End If
    'End Function
    Public Function GetValFlag() As String
        If Me.ValFlag Is Nothing OrElse Me.ValFlag.Equals(String.Empty) Then
            Dim oContract As Contract
            oContract = Contract.GetContract(Me.DealerId, Me.WarrantySalesDate.Value)
            If oContract Is Nothing Then
                oContract = Contract.GetMaxExpirationContract(Me.DealerId)
                If oContract Is Nothing Then
                    Throw New DataNotFoundException(Common.ErrorCodes.NO_CONTRACT_FOUND)
                End If
            End If
            Me.ValFlag = Me.getValTypeCode(oContract.ID_Validation_Id)
        End If
        Return Me.ValFlag
    End Function

    Public ReadOnly Property getSalutationDescription() As String

        Get

            Dim dv As DataView = LookupListNew.GetSalutationLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            salutationDesc = LookupListNew.GetDescriptionFromId(dv, Me.SalutationId)
            Return salutationDesc
        End Get

    End Property

    Public ReadOnly Property getLanguagePrefDesc() As String

        Get
            Dim dv As DataView = LookupListNew.GetLanguageLookupList()
            langPrefDesc = LookupListNew.GetDescriptionFromId(dv, Me.LanguageId)
            Return langPrefDesc
        End Get

    End Property

    Public ReadOnly Property getPostPrePaidDesc() As String

        Get
            Dim dv As DataView = LookupListNew.GetPostPrePaidLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            postPrePaidDesc = LookupListNew.GetDescriptionFromId(dv, Me.PostPrePaidId)
            Return postPrePaidDesc
        End Get

    End Property

    Public ReadOnly Property IsCompanyTypeInsurance() As Boolean
        Get
            Dim oCompanyNew As Company = New Company(Me.CompanyId)
            If oCompanyNew.CompanyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, COMPANY_TYPE_INSURANCE)) Then
                Return True
            End If

            Return False

        End Get
    End Property

    Public ReadOnly Property getMasterclaimProcFlag() As String
        Get
            Dim oCompanyNew As Company = New Company(Me.CompanyId)
            masterClaimProc = LookupListNew.GetCodeFromId(LookupListNew.LK_MASTERCLAIMPROC, oCompanyNew.MasterClaimProcessingId)
            Return masterClaimProc
        End Get
    End Property

    Public ReadOnly Property getDocTypeDesc() As String

        Get
            Dim dv As DataView = LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            documentTypeDesc = LookupListNew.GetDescriptionFromId(LookupListNew.LK_DOCUMENT_TYPES, Me.DocumentTypeID)
            Return documentTypeDesc
        End Get

    End Property

    Public ReadOnly Property getDocTypeCode() As String
        Get
            Dim dv As DataView = LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            documentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, Me.DocumentTypeID)
            Return documentTypeCode
        End Get

    End Property

    Public ReadOnly Property getValTypeCode(ByVal ID As Guid) As String
        Get
            Dim dv As DataView = LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ValTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATION_TYPES, ID)
            Return ValTypeCode

        End Get

    End Property

#End Region

#Region "Insert Certificate"

    Public Shared Function GetDealerDetailsForCertADD(ByVal DealerID As Guid) As CertAddDealerDetailsDV
        Dim dal As New CertificateDAL
        Return New CertAddDealerDetailsDV(dal.LoadDealerDetailsForCertADD(DealerID).Tables(0))
    End Function

    Public Shared Function InsertCertificate(ByVal TransID As Guid, ByVal DealerID As Guid, ByVal CertNum As String, ByVal ProdCode As String,
                        ByVal WarrSalesDate As Date, ByVal ProdPurchaseDate As Date, ByVal WarrPrice As Double, ByVal ItemCode As String,
                        ByVal ItemDesc As String, ByVal ProdPrice As Double, ByVal ExtWarranty As Integer, ByVal ManWarranty As Integer,
                        ByVal SalesRepNum As String, ByVal BranchCode As String, ByVal InvoiceNum As String, ByVal CustTaxId As String, ByVal Salutation As String,
                        ByVal CustName As String, ByVal CustAddress1 As String, ByVal CustAddress2 As String, ByVal CustCity As String,
                        ByVal CustZip As String, ByVal CustState As String, ByVal CustHomePhone As String, ByVal CustWorkPhone As String,
                        ByVal CustEmail As String, ByVal Make As String, ByVal Model As String, ByVal SerialNum As String,
                        ByVal CustCountryCode As String, ByVal PurchaseCountryCode As String, ByVal CurrencyCode As String, ByVal PaymentType As String,
                        ByVal BillingFrequency As Integer, ByVal NumOfInstallments As Integer, ByVal InstallmentAmt As Double,
                        ByVal BankAcctOwnerName As String, ByVal BankAcctNumber As String, ByVal BankRoutingNum As String,
                        ByVal MembershipNumer As String, ByVal KeepFileWhenErr As Integer,
                        ByRef ErrMsg As String, ByRef CertID As Guid, ByRef ErrMsgUIProgCode As String,
                        ByRef ErrMsgParamList As String, ByRef ErrMsgParamCnt As Integer,
                        Optional ByVal BundleItems As Generic.List(Of CertAddController.BundledItem) = Nothing,
                        Optional ByVal RecordType As String = "FC", Optional ByVal SkuNumber As String = Nothing, Optional ByVal SubscriberStatus As String = Nothing,
                        Optional ByVal PostPrePaid As String = Nothing, Optional ByVal MembershipNum As String = Nothing, Optional ByVal MembershipType As String = Nothing,
                        Optional ByVal BillingPlan As String = Nothing, Optional ByVal BillingCycle As String = Nothing,
                        Optional ByVal MaritalStatus As String = Nothing, Optional ByVal PersonType As String = Nothing,
                        Optional ByVal Gender As String = Nothing, Optional ByVal Nationality As String = Nothing,
                        Optional ByVal PlaceOfBirth As String = Nothing, Optional ByVal CUIT_CUIL As String = Nothing, Optional DateOfBirth As Date = Nothing,
                        Optional ByVal MarketingPromoSer As String = Nothing, Optional ByVal MarketingPromoNum As String = Nothing,
                        Optional ByVal SalesChannel As String = Nothing) As Boolean
        Dim dal As New CertificateDAL

        Dim strUserName As String = Authentication.CurrentUser.NetworkId

        Dim ItemCount As Integer = 0, BundleItemMake As Generic.List(Of String) = Nothing, BundleItemModel As Generic.List(Of String) = Nothing
        Dim BundleItemSerialNum As Generic.List(Of String) = Nothing, BundleItemDesc As Generic.List(Of String) = Nothing
        Dim BundleItemPrice As Generic.List(Of Double) = Nothing, BundleItemMfgWarranty As Generic.List(Of Integer) = Nothing
        Dim BundleItemProductCode As Generic.List(Of String) = Nothing
        Dim i As Integer, objItem As CertAddController.BundledItem

        If (Not BundleItems Is Nothing) AndAlso BundleItems.Count > 0 Then
            If BundleItems.Count > CertAddController.MAX_BUNDLED_ITEMS_ALLOWED Then
                ErrMsg = TranslationBase.TranslateLabelOrMessage(CertAddController.ERR_TOO_MANY_BUNDLED_ITEMS)
                Return False
            Else
                ItemCount = BundleItems.Count
                BundleItemMake = New Generic.List(Of String)
                BundleItemModel = New Generic.List(Of String)
                BundleItemSerialNum = New Generic.List(Of String)
                BundleItemDesc = New Generic.List(Of String)
                BundleItemPrice = New Generic.List(Of Double)
                BundleItemMfgWarranty = New Generic.List(Of Integer)
                BundleItemProductCode = New Generic.List(Of String)
                For Each objItem In BundleItems
                    BundleItemMake.Add(objItem.Make)
                    BundleItemModel.Add(objItem.Model)
                    BundleItemSerialNum.Add(objItem.SerialNumber)
                    BundleItemDesc.Add(objItem.Description)
                    BundleItemPrice.Add(objItem.Price)
                    BundleItemMfgWarranty.Add(objItem.MfgWarranty)
                    BundleItemProductCode.Add(objItem.ProductCode)
                Next
            End If
        End If

        dal.InsertCertificate(TransID, DealerID, CertNum, ProdCode, WarrSalesDate, ProdPurchaseDate, WarrPrice, ItemCode,
                        ItemDesc, ProdPrice, ExtWarranty, ManWarranty, SalesRepNum, BranchCode, InvoiceNum, CustTaxId, Salutation,
                        CustName, CustAddress1, CustAddress2, CustCity, CustZip, CustState, CustHomePhone, CustWorkPhone,
                        CustEmail, Make, Model, SerialNum, CustCountryCode, PurchaseCountryCode, CurrencyCode, PaymentType,
                        BillingFrequency, NumOfInstallments, InstallmentAmt, BankAcctOwnerName, BankAcctNumber, BankRoutingNum,
                        MembershipNumer, KeepFileWhenErr,
                        ErrMsg, CertID, ErrMsgUIProgCode, ErrMsgParamList, ErrMsgParamCnt, strUserName,
                        ItemCount, BundleItemMake, BundleItemModel, BundleItemSerialNum, BundleItemDesc, BundleItemPrice,
                        BundleItemMfgWarranty, BundleItemProductCode, RecordType, SkuNumber, SubscriberStatus,
                        PostPrePaid, MembershipType, BillingPlan, BillingCycle, MaritalStatus, PersonType,
                        Gender, Nationality, PlaceOfBirth, CUIT_CUIL, DateOfBirth, MarketingPromoSer, MarketingPromoNum, SalesChannel)
        If Trim(ErrMsg) <> String.Empty Then 'Error exists
            Return False
        Else
            Return True
        End If
    End Function

#Region "CertAddDealerDetailsDV"
    Public Class CertAddDealerDetailsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CURRENCY_CODE As String = "CurrencyCode"
        Public Const COL_COUNTRY_CODE As String = "CountryCode"
        Public Const COL_MAIL_ADDRESS_FORMAT As String = "MAIL_ADDR_FORMAT"
#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region

#End Region

#Region "Lazy Initialize Fields"
    Private _dealer As Dealer = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Dealer() As Dealer
        Get
            If (_dealer Is Nothing) Then
                If Not Me.DealerId.Equals(Guid.Empty) Then
                    Me.Dealer = New Dealer(Me.DealerId, Me.Dataset)
                End If
            End If
            Return _dealer
        End Get
        Private Set(ByVal value As Dealer)
            If (value Is Nothing OrElse _dealer Is Nothing OrElse Not _dealer.Equals(value)) Then
                _dealer = value
            End If
        End Set
    End Property
#End Region

#Region "CertificateClaimsDV"
    Public Class CertificateClaimsDV
        Inherits DataView
#Region "Constants"
        Public Const COL_CLAIM_ID As String = "Claim_Id"
        Public Const COL_CLAIM_NUMBER As String = "Claim_Number"
        Public Const COL_CREATED_DATE As String = "Created_Date"
        Public Const COL_STATUS_CODE As String = "Status_Code"
        Public Const COL_AUTHORIZED_AMOUNT As String = "Authorized_Amount"
        Public Const COL_TOTAL_PAID As String = "Total_Paid"
        Public Const COL_EXTENDED_STATUS As String = "Extended_Status"
        Public Const COL_COMMENTS As String = "Comments"
        Public Const COL_Method_Of_Repair_code As String = "Method_Of_Repair_code"
        Public Const COL_Repair_Date As String = "Repair_Date"
#End Region
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region
#Region "History DataViews"
    Public Class CustPersonelHistoryDV
        Inherits DataView
        Public Const COL_SALUTATION As String = "salutation"
        Public Const COL_FIRST_NAME As String = "first_name"
        Public Const COL_MIDDLE_NAME As String = "middle_name"
        Public Const COL_LAST_NAME As String = "last_name"
        Public Const COL_CORPORATE_NAME As String = "corporate_name"
        Public Const COL_GENDER As String = "gender"
        Public Const COL_BIRTH_DATE As String = "birth_date"
        Public Const COL_MARITAL_STATUS As String = "marital_status"
        Public Const COL_NATIONALITY As String = "nationality"
        Public Const COL_PLACE_OF_BIRTH As String = "place_of_birth"
        Public Const COL_DATE_OF_CHANGE As String = "change_date"
        Public Const COL_CUSTOMER_ID As String = "customer_id"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Class CustAddressHistoryDV
        Inherits DataView
        Public Const COL_ADDRESS1 As String = "address1"
        Public Const COL_ADDRESS2 As String = "address2"
        Public Const COL_ADDRESS3 As String = "address3"
        Public Const COL_CITY As String = "city"
        Public Const COL_COUNTRY As String = "country"
        Public Const COL_STATE As String = "state"
        Public Const COL_POSTAL_CODE As String = "postal_code"
        Public Const COL_ZIP_LOCATOR As String = "zip_locator"
        Public Const COL_ADDRESS_ID As String = "address_id"
        Public Const COL_DATE_OF_CHANGE As String = "change_date"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Class CustContactHistoryDV
        Inherits DataView
        Public Const COL_EMAIL As String = "email"
        Public Const COL_HOME_PHONE As String = "home_phone"
        Public Const COL_WORK_PHONE As String = "work_phone"
        Public Const COL_DATE_OF_CHANGE As String = "change_date"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Class CustBankDetailHistoryDV
        Inherits DataView
        Public Const COL_ACCOUNT_NAME As String = "account_name"
        Public Const COL_BANK_NAME As String = "bank_name"
        Public Const COL_BANK_ID As String = "bank_id"
        Public Const COL_ACCOUNT_TYPE_ID As String = "account_type_id"
        Public Const COL_BANK_LOOKUP_CODE As String = "bank_lookup_code"
        Public Const COL_BANK_SORT_CODE As String = "bank_sort_code"
        Public Const COL_BRANCH_NAME As String = "branch_name"
        Public Const COL_BANK_SUB_CODE As String = "bank_sub_code"
        Public Const COL_ACCOUNT_NUMBER As String = "account_number"
        Public Const COL_MANDATE_ID As String = "mandate_id"
        Public Const COL_IBAN_NUMBER As String = "iban_number"
        Public Const COL_CREATED_BY As String = "created_by"
        Public Const COL_CREATED_DATE As String = "created_date"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region
#Region "DataView Retrieveing Methods"

    Public Shared Function GetCustPersonalHistory(ByVal certId As Guid, ByVal language_id As Guid) As DataView
        Try
            Dim dal As New CertificateDAL
            Dim ds As New DataSet

            ds = dal.LoadCustPersonalHistory(certId, language_id)
            Return New CustPersonelHistoryDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function GetCustAddressHistory(ByVal certId As Guid) As DataView
        Try
            Dim dal As New CertificateDAL
            Dim ds As New DataSet

            ds = dal.LoadCustAddressHistory(certId)
            Return New CustAddressHistoryDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetCustContactHistory(ByVal certId As Guid) As DataView
        Try
            Dim dal As New CertificateDAL
            Dim ds As New DataSet

            ds = dal.LoadCustContactHistory(certId)
            Return New CustContactHistoryDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetCustBankDetailHistory(ByVal certId As Guid) As DataView
        Try
            Dim dal As New CertificateDAL
            Dim ds As New DataSet

            ds = dal.LoadCustBankDetailHistory(certId)
            Return New CustBankDetailHistoryDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Web Svc Methods"


    Public Shared Function GetOlitaConsumerCertList(ByVal cert_number As String, ByVal dealerId As Guid, Optional ByVal InvoiceNumberMask As String = Nothing, Optional ByVal LimitResultset As Int32 = CertificateDAL.MAX_NUMBER_OF_ROWS) As DataSet
        Dim dal As New CertificateDAL
        Dim ds As DataSet
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        If Dealer.GetOlitaSearchType(dealerId).Equals(Codes.OLITA_SEARCH_GENERIC) Then
            cert_number += "*"
        End If

        Return dal.GetOlitaConsumerCertList(cert_number, dealerId, compIds, LimitResultset, InvoiceNumberMask)

    End Function


    Public Shared Function GetCertificateBO(ByVal dsParams As GetCertificateListInputDs) As String

        Dim local As New Certificate()
        Dim sWSConsumer As String
        Dim XMLData As String
        Dim dv As DataView

        Try

            dv = local.Dataset.Tables(0).DefaultView
            Dim DS As GetCertificateListInputDs = dsParams

            'sWSConsumer = DS.GetCertificateListInput.Item(0).wsConsumer

            Dim oProps As System.Reflection.PropertyInfo() = GetType(Certificate).GetProperties()
            Dim oProp As System.Reflection.PropertyInfo

            Dim iArrLen As Integer = oProps.Length
            Dim iCnt As Integer

            For iCnt = 0 To iArrLen

                oProps(iCnt).GetCustomAttributes(True)
                Dim oCustAttribs As Array = CType(oProps(iCnt).GetCustomAttributes(True), Array)

                Dim sAttribName As String
                Dim iAttribArrLen As Integer = oCustAttribs.Length

                Dim oArrItem As Object
                For Each oArrItem In oCustAttribs

                    sAttribName = oArrItem.GetType.ToString
                    'this is an example.  to implement, create a new attribute class called WSVisibleLevel with value options : Read, Validate, Return
                    If sAttribName = "Assurant.Common.Validation.WSVisibilityAttribute" Then

                    ElseIf Equals(GetType(Assurant.Common.Validation.ValueMandatoryAttribute), oArrItem.GetType) Then

                    End If
                Next

            Next iCnt

            'if consumer type = "CLIENT" or is empty 
            'If sWSConsumer.ToUpper.Equals(WSUtility.WS_CONSUMER_CLIENT) OrElse sWSConsumer.Trim.Equals("") Then
            '    XMLData = WSUtility.CompactData(dv)
            'Else
            DS.Tables.Add(dv.Table.Copy)
            XMLData = XMLHelper.FromDatasetToXML(DS)
            'End If

            dv.Dispose()
            DS.Dispose()

            Return XMLData

        Catch ex As Exception
            Throw New ElitaWSException(ex.Message)
        End Try
    End Function

    Public Shared Function GetCertificateList(ByVal dsParams As GetCertificateListInputDs) As String
        Try
            Dim sCertNum As String = ""
            Dim sCustName As String = ""
            Dim sAddress As String = ""
            Dim sZIP As String = ""
            Dim sTaxID As String = ""
            Dim sDealerCode As String = ""
            Dim iLimitResultset As Integer
            Dim sSortBy As String = ""
            Dim sWSConsumer As String = ""
            Dim XMLData As String
            Dim dv As DataView

            If Not dsParams.GetCertificateListInput.Count = 0 Then
                With dsParams.GetCertificateListInput.Item(0)
                    sCertNum = .CertificateNumber
                    sCustName = .CustomerName
                    sAddress = .Address
                    sZIP = .ZIP
                    sTaxID = .TaxID
                    sDealerCode = .DealerCode
                    iLimitResultset = .LimitResultset
                    sSortBy = .SortBy
                    'sWSConsumer = .wsConsumer
                End With
            End If

            'for backwards compatibility with GetCertificatesList 
            'where validation check is AndAlso (dealerName Is Nothing) instead of sDealerCode.Equals(String.Empty)
            If sDealerCode.Equals(String.Empty) Then sDealerCode = Nothing

            If WSUtility.IsGuid(sDealerCode) Then
                Dim guidDealerCode As New Guid(sDealerCode)
                sDealerCode = WSUtility.GetDealerCode(guidDealerCode) 'convert GUID to code
            End If

            If WSUtility.IsGuid(sSortBy) Then
                Dim guidSortBy As New Guid(sSortBy)
                sSortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, guidSortBy)
            End If

            dv = GetCertificatesList(sCertNum, sCustName, sAddress, sZIP, sTaxID, sDealerCode, sSortBy, iLimitResultset)

            'if consumer type = "CLIENT" or is empty 
            'If sWSConsumer.ToUpper.Equals(WSUtility.WS_CONSUMER_CLIENT) OrElse sWSConsumer.Trim.Equals("") Then
            '    XMLData = WSUtility.CompactData(dv)
            'Else
            Dim dsRetMsg As New DataSet
            dsRetMsg.DataSetName = "GetCertificateListResultset"
            dsRetMsg.Tables.Add(dv.Table.Copy)
            XMLData = XMLHelper.FromDatasetToXML(dsRetMsg)
            dsRetMsg.Dispose()
            'End If

            dsParams.Dispose()

            If XMLData = String.Empty Then XMLData = XMLHelper.FromStringToXML("<MESSAGE>" & TranslationBase.TranslateLabelOrMessage(NO_RECORDS_FOUND) & "</MESSAGE>")

            Return XMLData

        Catch ex As Exception
            If Not ex.GetType.Equals(GetType(BOValidationException)) Then
                Throw New ElitaWSException(ex)
            Else
                Throw ex 'percolate BO validation type as is. do not convert to type ElitaWSException
            End If
        End Try
    End Function

    Public Shared Function GalaxyGetCertificateDetail(ByVal certNumber As String, ByVal dealerCode As String) As DataSet
        Try
            Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If (certNumber.Equals(String.Empty) AndAlso (dealerCode Is Nothing)) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return dal.GalaxyLoadCertificateDetail(certNumber, dealerCode, compId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ClaimLogisticsGetCert(ByVal certNumber As String, ByVal dealerCode As String) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If (certNumber Is Nothing Or certNumber.Equals(String.Empty) Or dealerCode Is Nothing Or dealerCode.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return dal.ClaimLogisticsGetCert(certNumber, dealerCode, compIds)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertListByInvoiceNumber(ByVal InvoiceNumber As String) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If InvoiceNumber Is Nothing Or InvoiceNumber.Equals(String.Empty) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return dal.GetCertListByInvoiceNumber(InvoiceNumber, compIds)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertListWithInvoiceNumberByCertNUmber(ByVal CertificateNumber As String) As DataSet
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            If CertificateNumber Is Nothing Or CertificateNumber.Equals(String.Empty) Then
                Throw New BOValidationException(errors, GetType(Certificate).FullName)
            End If

            Return dal.GetCertListWithInvoiceNumberByCertNUmber(CertificateNumber, compIds)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetGalaxyCertificatesList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                        ByVal IdentificationNumberMask As String, ByVal VehicleLicenseTagMask As String,
                        ByVal VINLocatorMask As String, ByVal dealerCodeMask As String, ByVal dealerNameMask As String,
                        ByVal CustomerPhoneMask As String, Optional ByVal sortBy As String = CertificateDAL.SORT_BY_CUSTOMER_NAME, Optional ByVal LimitResultset As Int32 = CertificateDAL.GALAXY_MAX_NUMBER_OF_ROWS) As DataSet

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            Return dal.LoadGalaxyList(certNumberMask, customerNameMask,
                                     IdentificationNumberMask, VehicleLicenseTagMask, VINLocatorMask, dealerCodeMask, dealerNameMask, CustomerPhoneMask, compIds, sortBy, LimitResultset)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function OlitaGetCertificatesList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                        ByVal CustomerPhoneMask As String, Optional ByVal sortBy As String = CertificateDAL.SORT_BY_CERT_NUMBER, Optional ByVal LimitResultset As Int32 = CertificateDAL.MAX_NUMBER_OF_ROWS) As DataSet

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New CertificateDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

            Return dal.LoadOlitaList(certNumberMask, customerNameMask, CustomerPhoneMask, compIds, sortBy, LimitResultset)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function SearchCertificateByImeiNumber(ByVal companyCode As String, ByVal dealerCode As String,
                                                         ByVal imeiNumber As String, ByVal certStatus As String,
                                                         ByVal userId As String,
                                                         ByRef oErrCode As Integer, ByRef oErrMsg As String) As DataSet
        Dim dal As New CertificateDAL
        Return dal.GetCertificateByImei(companyCode, dealerCode, imeiNumber, certStatus, userId, oErrCode, oErrMsg)
    End Function

    Public Shared Function UpdateImeiNumberAddEvent(ByVal certItemId As Guid, ByVal imeiNumberCurrent As String, ByVal imeiNumberNew As String, ByVal identificationType As String,
                                                         ByRef oErrCode As Integer, ByRef oErrMsg As String) As Boolean
        Dim dal As New CertificateDAL
        dal.UpdateImeiAddEvent(certItemId, imeiNumberCurrent, imeiNumberNew, identificationType, oErrCode, oErrMsg)
        If oErrCode = 0 Then 'successs
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Sub CancelCertByExternalNumber(ByVal CompanyCode As String, ByVal DealerCode As String,
                                 ByVal ExternalCertNumType As String, ByVal ExternalCertNum As String,
                                 ByVal CancelllationDate As Date, ByVal CancellationReasonCode As String,
                                 ByVal CallerName As String, ByVal User As String,
                                 ByRef oDealerCode As String, ByRef oCertificateNum As String,
                                 ByRef oErrCode As String, ByRef oErrMsg As String)
        Dim dal As New CertificateDAL
        oDealerCode = String.Empty
        oCertificateNum = String.Empty
        oErrCode = String.Empty
        oErrMsg = String.Empty

        dal.CancelCertByExternalNumber(CompanyCode, DealerCode, ExternalCertNumType, ExternalCertNum,
                                              CancelllationDate, CancellationReasonCode, CallerName, User,
                                              oDealerCode, oCertificateNum, oErrCode, oErrMsg)
    End Sub
#End Region

#Region "CertificateSearchDV"

    Public Class CertificateSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERTIFICATE_ID As String = "Cert_Id"
        Public Const COL_CERTIFICATE_NUMBER As String = "CERT_NUMBER"
        Public Const COL_CUSTOMER_NAME As String = "CUSTOMER_NAME"
        Public Const COL_ADDRESS As String = "ADDRESS1"
        Public Const COL_ZIP As String = "POSTAL_CODE"
        Public Const COL_TAX_ID As String = "IDENTIFICATION_NUMBER"
        Public Const COL_DEALER As String = "DEALER"
        Public Const COL_STATUS_CODE As String = "Status_Code"
        Public Const COL_PRODUCT_CODE As String = "Product_Code"
        Public Const COL_MEMBERSHIP_NUM As String = "MEMBERSHIP_NUMBER"
        Public Const COL_INVOICE_NUM As String = "INVOICE_NUMBER"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "CombinedCertificateSearchDV"


    Public Class CombinedCertificateSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERTIFICATE_ID As String = "Cert_Id"
        Public Const COL_CERTIFICATE_NUMBER As String = "CERT_NUMBER"
        Public Const COL_CUSTOMER_NAME As String = "CUSTOMER_NAME"
        Public Const COL_ADDRESS As String = "ADDRESS1"
        Public Const COL_ZIP As String = "POSTAL_CODE"
        Public Const COL_DEALER As String = "DEALER"
        Public Const COL_STATUS_CODE As String = "Status_Code"
        Public Const COL_PRODUCT_CODE As String = "Product_Code"
        Public Const COL_HOME_PHONE As String = "HOME_PHONE"
        'Added for Req-610
        Public Const COL_WORK_PHONE As String = "WORK_PHONE"

        Public Const COL_MEMBERSHIP_NUM As String = "MEMBERSHIP_NUMBER"
        Public Const COL_INVOICE_NUM As String = "INVOICE_NUMBER"
        Public Const COL_TAX_ID As String = "IDENTIFICATION_NUMBER"

        ' for searial number search
        Public Const COL_SERIAL_NUMBER As String = "serial_number"

        ' vor vehicle lic search
        Public Const COL_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
        Public Const COL_IS_RESTRICTED As String = "is_restricted"


#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "PhoneNumberSearchDV"
    Public Class PhoneNumberSearchDV
        Inherits DataView

        Public Const COL_CERTIFICATE_ID As String = "Cert_Id"
        Public Const COL_CERTIFICATE_NUMBER As String = "CERT_NUMBER"
        Public Const COL_CUSTOMER_NAME As String = "CUSTOMER_NAME"
        Public Const COL_ADDRESS As String = "ADDRESS1"
        Public Const COL_ZIP As String = "POSTAL_CODE"
        Public Const COL_DEALER As String = "DEALER"
        Public Const COL_STATUS_CODE As String = "Status_Code"
        Public Const COL_PRODUCT_CODE As String = "Product_Code"
        Public Const COL_HOME_PHONE As String = "HOME_PHONE"
        'Added for Req-610
        Public Const COL_WORK_PHONE As String = "WORK_PHONE"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
        Public Function AddNewRowToEmptyDV() As PhoneNumberSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(PhoneNumberSearchDV.COL_CERTIFICATE_ID) = (New Guid()).ToByteArray
            row(PhoneNumberSearchDV.COL_CERTIFICATE_NUMBER) = ""
            row(PhoneNumberSearchDV.COL_CUSTOMER_NAME) = ""
            row(PhoneNumberSearchDV.COL_ADDRESS) = ""
            row(PhoneNumberSearchDV.COL_ZIP) = ""
            row(PhoneNumberSearchDV.COL_DEALER) = ""
            row(PhoneNumberSearchDV.COL_STATUS_CODE) = ""
            row(PhoneNumberSearchDV.COL_PRODUCT_CODE) = ""
            row(PhoneNumberSearchDV.COL_HOME_PHONE) = ""
            row(PhoneNumberSearchDV.COL_WORK_PHONE) = ""
            dt.Rows.Add(row)
            Return New PhoneNumberSearchDV(dt)
        End Function
    End Class
#End Region

#Region "SerialNumberSearchDV"

    Public Class SerialNumberSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERTIFICATE_ID As String = "cert_id"
        Public Const COL_SERIAL_NUMBER As String = "serial_number"
        Public Const COL_CERTIFICATE_NUMBER As String = "cert_number"
        Public Const COL_STATUS_CODE As String = "status_code"
        Public Const COL_DEALER As String = "dealer"
        Public Const COL_PRODUCT_CODE As String = "product_code"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "VehicleLicenseFlagSearchDV"

    Public Class VehicleLicenseFlagSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERTIFICATE_ID As String = "cert_id"
        Public Const COL_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
        Public Const COL_CERTIFICATE_NUMBER As String = "cert_number"
        Public Const COL_STATUS_CODE As String = "status_code"
        Public Const COL_CUSTOMER_NAME As String = "customer_name"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class NonFutureProductSalesAndWarrantyDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.FUTURE_WARRANTY_PRODUCT_SALE_DATES_NOT_ALLOWED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim oCert As Certificate = CType(objectToValidate, Certificate)
            Dim bIsOk As Boolean = True

            If oCert.ProductSalesDate Is Nothing OrElse oCert.WarrantySalesDate Is Nothing Then Return True

            If (oCert.PreviousCertificateId.Equals(Guid.Empty)) And (oCert.WarrantySalesDate.Value > Date.Today) Then
                bIsOk = False
            End If

            Return bIsOk

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class NewValueMandatory
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)
            Dim bIsOk As Boolean = True
            'START DEF-1986
            Dim DocType As String = obj.getDocTypeCode
            Dim Dealer As Dealer
            Dim DealerTypeCode As Guid
            DealerTypeCode = Dealer.GetDealerTypeId(obj.DealerId)
            'END DEF-1986

            If obj.ValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If obj.ValFlag = VALIDATION_FLAG_FULL Then
                'START DEF-1986
                'DOC_TYPE_CNPJ
                If UCase(DocType) = DOC_TYPE_CNPJ Then
                    If obj.RgNumber Is Nothing And obj.DocumentAgency Is Nothing And obj.DocumentIssueDate Is Nothing And obj.IdType Is Nothing Then
                        Return True
                    Else
                        MyBase.Message = Common.ErrorCodes.GUI_FIELD_MUST_BE_BLANK
                        Return False
                    End If
                End If

                'DOC_TYPE_CPF
                'VSC
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, DealerTypeCode) = "2" And UCase(DocType) = DOC_TYPE_CPF) Then
                    If obj.RgNumber Is Nothing And obj.DocumentAgency Is Nothing And obj.DocumentIssueDate Is Nothing And obj.IdType Is Nothing Then
                        Return False
                    End If
                End If

                'ESC
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, DealerTypeCode) = "1" And UCase(DocType) = DOC_TYPE_CPF) Then
                    Return True
                End If
                'Commented for DEF-1986
                'If obj.RgNumber Is Nothing Then
                '    Return False
                'End If
                'END   DEF-1986
            End If

            Return True

        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryDocumentType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)
            'Dim objCompany As New Company(obj.CompanyId)

            'If objCompany.CompanyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, COMPANY_TYPE_SERVICES)) Then
            '    Return True
            'End If
            If obj.ValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If
            'REQ 1070/DEF 2878-ELCI NO VALIDATION FLAG
            If obj.ValFlag = VALIDATION_FLAG_NO_VALIDATION Then
                Return True
            End If
            'END REQ 1070
            Dim docType As String = obj.getDocTypeCode
            'DEF 2576
            If obj.ValFlag = VALIDATION_FLAG_CPF_CNPJ Then
                If UCase(docType) = DOC_TYPE_CPF _
                    Or UCase(docType) = DOC_TYPE_CNPJ Then
                    Return True
                Else
                    MyBase.Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE_4)
                    Return False
                End If



            End If

            If UCase(docType) = DOC_TYPE_CPF _
               Or UCase(docType) = DOC_TYPE_CON _
               Or UCase(docType) = DOC_TYPE_CNPJ Then
                Return True
            Else
                MyBase.Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE)
                Return False
            End If

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMustBeBlankForDocumentNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FIELD_MUST_BE_BLANK)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)

            If obj.ValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            'DEF 2576
            If obj.ValFlag = VALIDATION_FLAG_CPF_CNPJ Then
                If UCase(obj.getDocTypeCode) = DOC_TYPE_CON Then
                    Return True

                End If
            End If

            If UCase(obj.getDocTypeCode) = DOC_TYPE_CON Then
                'DEF 2576
                If obj.IdentificationNumber <> Nothing AndAlso obj.IdentificationNumber <> String.Empty Then
                    Return False
                End If
            End If

            Return True

        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class NonFutureDocumentIssueDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.FUTURE_DATES_NOT_ALLOWED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)

            If obj.ValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If obj.ValFlag = VALIDATION_FLAG_FULL Then
                If Not obj.DocumentIssueDate Is Nothing Then
                    If obj.DocumentIssueDate.Value > Date.Today Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class SPValidationDocumentNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERROR_FOUND_BY_ORACLE_VALIDATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)
            Dim dal As New CertificateDAL
            Dim oErrMess As String


            Try

                Select Case obj.ValFlag
                    Case VALIDATION_FLAG_NONE
                        Return True
                    'DEF 2576
                    Case VALIDATION_FLAG_FULL
                        If obj.IdentificationNumber Is Nothing Or
                            obj.IdentificationNumber = String.Empty Then
                            If obj.getDocTypeCode = DOC_TYPE_CON Then
                                Return True
                            Else
                                oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.IdentificationNumber)
                                If Not oErrMess Is Nothing Then
                                    MyBase.Message = UCase(oErrMess)
                                    Return False

                                End If
                            End If
                        Else
                            'Return True
                            ' End If
                            oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.IdentificationNumber)
                            If Not oErrMess Is Nothing Then
                                MyBase.Message = UCase(oErrMess)
                                Return False
                            End If
                            'Return True

                        End If

                    Case VALIDATION_FLAG_PARTIAL
                        If Me.DisplayName = TAX_ID_NUMB _
                            AndAlso (obj.getDocTypeCode <> DOC_TYPE_CPF AndAlso obj.getDocTypeCode <> DOC_TYPE_CNPJ) _
                            AndAlso (obj.TaxIDNumb Is Nothing OrElse obj.TaxIDNumb.Trim.Length = 0) Then
                            Return True
                        End If

                        If Me.DisplayName = IDENTIFICATION_NUMBER _
                            AndAlso (Not obj.DocumentIssueDate Is Nothing _
                                Or Not obj.IdType Is Nothing _
                                Or Not obj.DocumentAgency Is Nothing _
                                Or Not obj.RgNumber Is Nothing _
                                Or Not obj.DocumentTypeID.Equals(Guid.Empty)) Then
                            Return True
                        End If

                        oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.IdentificationNumber)
                        If Not oErrMess Is Nothing Then
                            MyBase.Message = UCase(oErrMess)
                            Return False
                        End If

                    'REQ-1012 for DEF-2576
                    Case VALIDATION_FLAG_CPF_CNPJ
                        If obj.IdentificationNumber Is Nothing Then
                            obj.IdentificationNumber = String.Empty
                        End If
                        oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.IdentificationNumber)
                        If Not oErrMess Is Nothing Then
                            MyBase.Message = UCase(oErrMess)
                            Return False
                        End If
                        'END 1012
                    Case Else
                        Return True
                End Select

                Return True

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)

            If obj.Email Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.Email)

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueTaxIdLenht
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_TOO_SHORT_OR_TOO_LONG)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)

            If Not obj.IsCompanyTypeInsurance Then
                Return True
            End If

            If obj.TaxIDNumb Is Nothing Then
                Return True
            End If

            If obj.TaxIDNumb.Length > 15 Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class NonFutureDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.FUTURE_DATE_NOT_ALLOWED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Certificate = CType(objectToValidate, Certificate)

            If obj.DateOfBirth Is Nothing Then
                Return True
            End If


            If obj.DateOfBirth.Value > Date.Today Then
                Return False
            End If

            Return True

        End Function
    End Class
#End Region

#Region "Functions"

    Public Function GetInforceCertsLastestDate() As DataView
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        ds = dal.GetInforceCertsLastestDate()
        Return ds.Tables(0).DefaultView

    End Function

    Public Shared Function ValidateLicenseFlag(ByVal VehicleLicenceFlag As String, ByVal CertNumber As String, ByVal CompanyGroupId As Guid) As DataView
        Dim dal As New CertificateDAL
        Dim ds As DataSet

        ds = dal.ValidateLicenseFlag(VehicleLicenceFlag, CertNumber, CompanyGroupId)
        Return ds.Tables(0).DefaultView

    End Function

    Public Shared Function GetCertHistoryInfo(ByVal certNumber As String, ByVal DealerId As Guid, ByVal PremChanges As String) As CertificateHistoryDV
        'Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim dal As New CertificateDAL

        Return New CertificateHistoryDV(dal.GetCertHistory(certNumber, DealerId, PremChanges).Tables(0))

    End Function

    Public Shared Function GetOtherCustomerInfo(ByVal CertId As Guid, ByVal IdentificationNumberType As String) As OtherCustomerInfoDV
        Dim dal As New CertificateDAL
        Return New OtherCustomerInfoDV(dal.GetOtherCustomerInfo(CertId, IdentificationNumberType).Tables("GetOtherCustomerInfo"))
    End Function
    Public Shared Function GetOtherCustomerDetails(ByVal CustomerId As Guid, ByVal LangId As Guid, ByVal IdentificationNumberType As String) As OtherCustomerInfoDV
        Dim dal As New CertificateDAL
        Return New OtherCustomerInfoDV(dal.GetOtherCustomerDetails(CustomerId, LangId, IdentificationNumberType).Tables("GetOtherCustomerDetails"))
    End Function
    Public Shared Function GetCertInstallmentHistoryInfo(ByVal CertId As Guid) As CertInstallmentHistoryDV
        'Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim dal As New CertificateDAL

        Return New CertInstallmentHistoryDV(dal.GetCertInstallmentHistory(CertId).Tables(0))

    End Function
    Public Shared Function GetCertExtensionsInfo(ByVal CertId As Guid) As CertExtensionsDV

        Dim dal As New CertificateDAL

        Return New CertExtensionsDV(dal.GetCertExtensionsInfo(CertId).Tables(0))

    End Function

    Public Shared Function GetCertUpgradeDataExtensionsInfo(ByVal CertId As Guid) As CertUpgradeExtensionsDV

        Dim dal As New CertificateDAL

        Return New CertUpgradeExtensionsDV(dal.GetCertUpgradeExtensionsInfo(CertId).Tables(0))

    End Function


    Public Shared Function GetFraudulentCertExtensions(ByVal certId As Guid) As CertExtensionsDV

        Dim dal As New CertificateDAL

        Return New CertExtensionsDV(dal.GetFraudulentCertExtensions(certId).Tables(0))

    End Function

    Public Shared Function getCallerListForCert(ByVal CertID As Guid) As DataTable

        Try
            Dim dal As New CertificateDAL

            Return (dal.LoadCallerList(CertID).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getCallerListForCase(ByVal CaseID As Guid) As DataTable

        Try
            Dim dal As New CertificateDAL

            Return (dal.LoadCallerListForCase(CaseID).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function GetCertPaymentPassedDueExtInfo(ByVal CertId As Guid) As Integer

        Dim dal As New CertificateDAL

        Return dal.GetCertPaymentPassedDueExtInfo(CertId)

    End Function

    Public Function MaskDatePart(txtDate As String, Optional noMask As Boolean = True) As String
        If Not (String.IsNullOrEmpty(txtDate)) Then
            If (CultureInfo.CurrentCulture.Name.Equals("ja-JP")) Then
                Dim parsedDate As DateTime
                parsedDate = DateTime.ParseExact(txtDate, "dd-M-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                txtDate = parsedDate.ToString("D", CultureInfo.CurrentCulture)
                If (noMask) Then
                    Return txtDate
                Else
                    Return txtDate.Replace(txtDate.Substring(0, 4), "XXXX")
                End If
            Else
                If (Not noMask) Then
                    Dim dateofbirth As Date = txtDate
                    Return dateofbirth.ToString("dd-MMM-xxxx")
                End If
            End If
                Return txtDate
        End If
    End Function

#End Region

#Region "CertificateHistoryDV"
    Public Class CertificateHistoryDV
        Inherits DataView
#Region "Constants"
        Public Const COL_RECORD_TYPE As String = "Record_type"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_FILENAME As String = "Source"
        Public Const COL_IN_FORCE_DATE As String = "Inforce_Date"
        Public Const COL_PROCESSED_DATE As String = "Processed_date"
        Public Const COL_CUSTOMER_NAME As String = "Customer_name"
        Public Const COL_ADDRESS1 As String = "Address1"
        Public Const COL_CITY As String = "City"
        Public Const COL_STATE As String = "State"
        Public Const COL_ZIP As String = "Zip"
        Public Const COL_MANUFACTURER As String = "Manufacturer"
        Public Const COL_MODEL As String = "Model"
        Public Const COL_SERIAL_NUMBER As String = "Serial_number"
        Public Const COL_SKU_NUMBER As String = "Sku_number"
        Public Const COL_MEMBERSHIP_TYPE As String = "Membership_type"
        Public Const COL_IDENTIFICATION_NUMBER As String = "Identification_number"
        Public Const COL_SUBSCRIBER_STATUS As String = "Subscriber_status"
        Public Const COL_HOME_PHONE As String = "Home_phone"
        Public Const COL_WORK_PHONE As String = "Work_phone"
        Public Const COL_EMAIL As String = "Email"

#End Region
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region

#Region "OtherCustomerInfoDV"
    Public Class OtherCustomerInfoDV
        Inherits DataView
#Region "Constants"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
#End Region
    End Class
#End Region
#Region "CertInstallmentHistoryDV"
    Public Class CertInstallmentHistoryDV
        Inherits DataView
#Region "Constants"
        Public Const COL_START_DATE As String = "start_date"
        Public Const COL_END_DATE As String = "end_date"
        Public Const COL_INSTALLMENT_AMOUNT As String = "installment_amount"
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
#End Region
    End Class
#End Region

#Region "CertExtensionsDV"
    Public Class CertExtensionsDV
        Inherits DataView
#Region "Constants"
        Public Const COL_FIELD_NAME As String = "field_name"
        Public Const COL_FIELD_VALUE As String = "field_value"

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
#End Region
    End Class
#End Region

#Region "CertUpgradeExtensionsDV"
    Public Class CertUpgradeExtensionsDV
        Inherits DataView
#Region "Constants"
        Public Const COL_SEQUENCE_NUMBER As String = "sequence_number"
        Public Const COL_UPGRADE_DATE As String = "upgrade_date"
        Public Const COL_VOUCHER_NUMBER As String = "voucher_number"
        Public Const COL_UPGRADE_FEE As String = "upgrade_fee"
        Public Const COL_RMA As String = "RMA"

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
#End Region
    End Class
#End Region

#Region "GW Related functions"
    Public Shared Function GWSearchCertificate(ByVal pCompanyCodes As String, ByVal pCertificateNumber As String, ByVal pCustomerName As String, ByVal pWorkPhone As String,
                                        ByVal pHomePhone As String, ByVal pAccountNumber As String, ByVal pServiceLineNumber As String, ByVal pTaxId As String,
                                        ByVal pEmail As String, ByVal pPurchaseInvoiceNumber As String, ByVal pAddress As String, ByVal pAddress2 As String,
                                        ByVal pAddress3 As String, ByVal pCountry As String, ByVal pState As String, ByVal pCity As String,
                                        ByVal pZipCode As String, ByVal pSerialNumber As String, ByVal pIMEINumber As String, ByVal pCertStatus As String,
                                        ByVal pNumberOfRecords As Integer) As Collections.Generic.List(Of Guid)
        Dim IdList As New Collections.Generic.List(Of Guid)
        Dim dal As New CertificateDAL, ds As DataSet
        ds = dal.GWSearchCertificate(pCompanyCodes, pCertificateNumber, pCustomerName, pWorkPhone, pHomePhone, pAccountNumber, pServiceLineNumber, pTaxId,
                                     pEmail, pPurchaseInvoiceNumber, pAddress, pAddress2, pAddress3, pCountry, pState, pCity,
                                     pZipCode, pSerialNumber, pIMEINumber, pCertStatus, pNumberOfRecords)
        If ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                IdList.Add(New Guid(CType(r(CertificateDAL.COL_NAME_CERT_ID), Byte())))
            Next
        End If
        Return IdList
    End Function
#End Region


#Region "SFR Related functions"
    Public Shared Function SFRSearchCertificate(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pAccountNumber As String) As DataSet


        Dim dal As New CertificateDAL, ds As DataSet
        ds = dal.SFRSearchCertificate(pCompanyCode, pDealerCode, pDealerGrp, pCustomerFirstName, pCustomerLastName, pWorkPhone, pEmail, pPostalCode, pAccountNumber)

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds
        End If
        Return ds
    End Function
#End Region
#Region "Claim Images"
    Public ReadOnly Property CertificateImagesList() As CertImage.CertImagesList
        Get
            Return New CertImage.CertImagesList(Me)
        End Get
    End Property

    Public Function AttachImage(ByVal pDocumentTypeId As Nullable(Of Guid),
                                ByVal pScanDate As Nullable(Of Date),
                                ByVal pFileName As String,
                                ByVal pComments As String,
                                ByVal pUserName As String,
                                ByVal pImageData As Byte()) As Guid

        Dim validationErrors As New List(Of ValidationError)

        ' Check if Document Type ID is supplied otherwise default to Other
        If (Not pDocumentTypeId.HasValue) Then
            pDocumentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER)
        End If

        ' Check if Scan Date is supplied otherwise default to Current Date 
        If (Not pScanDate.HasValue) Then
            pScanDate = DateTime.Today
        End If

        Dim oCertImage As CertImage
        oCertImage = DirectCast(Me.CertificateImagesList.GetNewChild(Me.Id), CertImage)

        With oCertImage
            .DocumentTypeId = pDocumentTypeId.Value
            .ScanDate = pScanDate.Value
            .FileName = pFileName
            .Comments = pComments
            .UserName = pUserName
            .FileSizeBytes = pImageData.Length
            .ImageId = Guid.NewGuid()
        End With

        Try

            ' This is to avoid any orphan images because of Elita Validation
            oCertImage.Validate()

            Dim oRepository As Documents.Repository = Me.Company.GetCertificateImageRepository()
            Dim doc As Documents.Document = oRepository.NewDocument
            With doc
                .Data = pImageData
                .FileName = pFileName
                .FileType = pFileName.Split(New Char() {"."}).Last()
                .FileSizeBytes = pImageData.Length
            End With

            oRepository.Upload(doc)
            oCertImage.ImageId = doc.Id

            oCertImage.Save()

            Return doc.Id
        Catch ex As Exception
            oCertImage.Delete()
            Throw
        End Try

    End Function

#End Region

#Region "Images/Documents"
    Public Function GetCertificateImagesView() As CertificateImagesView
        Dim t As DataTable = CertificateImagesView.CreateTable
        Dim detail As CertImage
        Dim filteredTable As DataTable

        Try

            For Each detail In Me.CertificateImagesList
                Dim row As DataRow = t.NewRow
                row(CertificateImagesView.COL_CERT_IMAGE_ID) = detail.Id.ToByteArray
                row(CertificateImagesView.COL_IMAGE_ID) = detail.ImageId.ToByteArray
                row(CertificateImagesView.COL_SCAN_DATE) = detail.ScanDate.ToString()
                row(CertificateImagesView.COL_DOCUMENT_TYPE) = LookupListNew.GetDescriptionFromId(LookupListCache.LK_DOCUMENT_TYPES, detail.DocumentTypeId)
                row(CertificateImagesView.COL_FILE_NAME) = detail.FileName

                Dim _ft As Documents.FileType = Documents.DocumentManager.Current.FileTypes.Where(Function(ft) ft.Extension = detail.FileName.Split(".".ToCharArray()).Last().ToUpper()).FirstOrDefault()
                If (Not _ft Is Nothing) Then
                    row(CertificateImagesView.COL_FILE_TYPE) = _ft.Description
                End If

                If (Not detail.FileSizeBytes Is Nothing) Then
                    row(CertificateImagesView.COL_FILE_SIZE_BYTES) = detail.FileSizeBytes.Value
                End If
                row(CertificateImagesView.COL_COMMENTS) = detail.Comments
                If (detail.UserName Is Nothing) OrElse (detail.UserName.Trim().Length = 0) Then
                    row(CertificateImagesView.COL_USER_NAME) = detail.CreatedById
                Else
                    row(CertificateImagesView.COL_USER_NAME) = detail.UserName
                End If

                t.Rows.Add(row)

            Next

            Return New CertificateImagesView(t)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class CertificateImagesView
        Inherits DataView
        Public Const COL_CERT_IMAGE_ID As String = "CERT_IMAGE_ID"
        Public Const COL_IMAGE_ID As String = "IMAGE_ID"
        Public Const COL_SCAN_DATE As String = "SCAN_DATE"
        Public Const COL_DOCUMENT_TYPE As String = "DOCUMENT_TYPE"
        Public Const COL_FILE_NAME As String = "FILE_NAME"
        Public Const COL_FILE_TYPE As String = "FILE_TYPE"
        Public Const COL_FILE_SIZE_BYTES As String = "FILE_SIZE_BYTES"
        Public Const COL_COMMENTS As String = "COMMENTS"
        Public Const COL_USER_NAME As String = "USER_NAME"

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_CERT_IMAGE_ID, GetType(Byte()))
            t.Columns.Add(COL_IMAGE_ID, GetType(Byte()))
            t.Columns.Add(COL_SCAN_DATE, GetType(String))
            t.Columns.Add(COL_DOCUMENT_TYPE, GetType(String))
            t.Columns.Add(COL_FILE_NAME, GetType(String))
            t.Columns.Add(COL_FILE_TYPE, GetType(String))
            t.Columns.Add(COL_FILE_SIZE_BYTES, GetType(Long))
            t.Columns.Add(COL_COMMENTS, GetType(String))
            t.Columns.Add(COL_USER_NAME, GetType(String))

            Return t
        End Function
    End Class


#End Region

End Class





