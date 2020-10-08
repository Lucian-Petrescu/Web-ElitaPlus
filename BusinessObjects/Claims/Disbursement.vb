'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/15/2004)  ********************

Public Class Disbursement
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const REVERSE_MULTIPLIER As Integer = -1
    Public Const PAYEE_INFO_TYPE__SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Const PAYEE_INFO_TYPE__CUSTOMER As String = "CUSTOMER"
    Public Const PAYEE_INFO_TYPE__OTHER As String = "OTHER"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ClaimInvoiceBO As ClaimInvoice)
        MyBase.New(False)
        Dataset = ClaimInvoiceBO.Dataset
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DisbursementDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New DisbursementDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    Private _disbursement As Disbursement = Nothing
    Private _sourceCountryID As Guid
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(DisbursementDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_DISBURSEMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property
    'Commented out by AA for WR761620
    '<ValueMandatory("")> _
    'Public Property PayeeAddressID() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(DisbursementDAL.COL_NAME_PAYEE_ADDRESS_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(DisbursementDAL.COL_NAME_PAYEE_ADDRESS_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(DisbursementDAL.COL_NAME_PAYEE_ADDRESS_ID, Value)
    '    End Set
    'End Property

    '<ValueMandatory(""), ValidStringLength("", Max:=1)> _
    'Public Property PayeeOption() As String
    '    Get
    '        CheckDeleted()
    '        If Row(DisbursementDAL.COL_NAME_PAYEE_OPTION) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(DisbursementDAL.COL_NAME_PAYEE_OPTION), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(DisbursementDAL.COL_NAME_PAYEE_OPTION, Value)
    '    End Set
    'End Property


    <ValueMandatory("")> _
    Public Property PayeeOptionId As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property SvcControlNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RecordCount As LongType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_RECORD_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DisbursementDAL.COL_NAME_RECORD_COUNT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_RECORD_COUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property CustomerName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property Dealer As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DEALER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DEALER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Certificate As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property ServiceCenterName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME, Value)
        End Set
    End Property

    Public Property CheckNumber As LongType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CHECK_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DisbursementDAL.COL_NAME_CHECK_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CHECK_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property TrackingNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    'need to be mandatory..............
    <ValueMandatory("")> _
    Public Property AcctStatus As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCT_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCT_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ACCT_STATUS, Value)
        End Set
    End Property

    ' needs to mandatory ................
    <ValueMandatory("")> _
    Public Property StatusDate As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_STATUS_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_STATUS_DATE, Value)
        End Set
    End Property

    Public Property PaymentDate As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_PAYMENT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYMENT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Payee As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYEE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYEE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property RegionDesc As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_REGION_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_REGION_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_REGION_DESC, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)> _
    Public Property Zip As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ZIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ZIP), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ZIP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property PayeeMailingLabel As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL, Value)
        End Set
    End Property


    Public Property DeductibleAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT, Value)
        End Set
    End Property


    Public Property DeductibleTaxAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, Value)
        End Set
    End Property



    Public Property TaxAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TAX_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_TAX_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_TAX_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PaymentAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property Processed As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PROCESSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PROCESSED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PROCESSED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property AccountName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ACCOUNT_NAME, Value)
        End Set
    End Property

    ' <ValidNumericRange("", Min:=0)> _
    <ValidStringLength("", Min:=0, Max:=10)> _
    Public Property BankID As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Min:=0, Max:=29)> _
    Public Property AccountNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property PaymentMethod As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYMENT_METHOD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYMENT_METHOD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property DealerName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DEALER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DEALER_NAME, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property PaymentReason As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYMENT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAYMENT_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property IdentificationNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property DocumentType As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property Country As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_COUNTRY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_COUNTRY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_COUNTRY, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property IbanNumber As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_IBAN_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_IBAN_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property SwiftCode As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SWIFT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_SWIFT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)> _
    Public Property AccountType As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ACCOUNT_TYPE, Value)
        End Set
    End Property

    Public Property AccountTypeId As Guid

        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID, Value)
        End Set
    End Property

    Public Property VendorNumAcct As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT, Value)
        End Set
    End Property
    <ValidIIBBProvince("")> _
    Public Property VendorRegionDesc As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC, Value)
        End Set
    End Property

    Public Property perceptionIVA As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PERCEPTION_IVA) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PERCEPTION_IVA), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PERCEPTION_IVA, Value)
        End Set
    End Property


    Public Property perceptionIIBB As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PERCEPTION_IIBB) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PERCEPTION_IIBB), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PERCEPTION_IIBB, Value)
        End Set
    End Property

    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property BranchName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    Public Property BankName As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BANK_NAME, Value)
        End Set
    End Property

    Public Property BankSortCode As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_SORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_SORT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BANK_SORT_CODE, Value)
        End Set
    End Property

    Public Property BankLookupCode As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE, Value)
        End Set
    End Property

    Public Property BankSubCode As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_SUB_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_SUB_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BANK_SUB_CODE, Value)
        End Set
    End Property

    Public Property ClaimAuthorizationId As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property Bonus As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Public Property BonusTax As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property



    Public Property LaborAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_LABOR_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_LABOR_AMOUNT, Value)
        End Set
    End Property


    Public Property PartAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PART_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property



    Public Property ServiceAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SERVICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_SERVICE_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_SERVICE_AMOUNT, Value)
        End Set
    End Property



    Public Property TripAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_TRIP_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property



    Public Property ShippingAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property



    Public Property DispositionAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT, Value)
        End Set
    End Property


    Public Property OtherAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_OTHER_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property



    Public Property DiagnosticsAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT, Value)
        End Set
    End Property



    Public Property WithholdingAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT, Value)
        End Set
    End Property

    Public Property PaytocustomerAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT, Value)
        End Set
    End Property

#End Region

#Region "Derived Properties"
    Public ReadOnly Property SecondDisbursement As Disbursement
        Get
            If _disbursement Is Nothing Then
                _disbursement = New Disbursement(Dataset)
            End If
            Return _disbursement
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DisbursementDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function GetList(svcControlNumber As String, companyList As ArrayList) As DisbursementSearchDV

        Try
            Dim dal As New DisbursementDAL

            Return New DisbursementSearchDV(dal.LoadPayeeList(svcControlNumber, companyList).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetDisbursement(id As Guid) As DisbursementSearchDV

        Try
            Dim dal As New DisbursementDAL

            Return New DisbursementSearchDV(dal.LoadDisbursement(id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Sub DeleteNewChildDisbursement(parentClaim As Claim)
        Dim row As DataRow
        If parentClaim.Dataset.Tables.IndexOf(CommentDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            'For Each row In parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows
            For rowIndex = 0 To parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Count - 1
                row = parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) OrElse (row.RowState = DataRowState.Detached) Then
                    Dim c As Comment = New Comment(row)
                    If parentClaim.Id.Equals(c.ClaimId) AndAlso c.IsNew Then
                        c.Delete()
                    End If
                End If
            Next

        End If
    End Sub

    Public Sub PrepopulateFromClaimInvoice(ClaimInvoiceBO As ClaimInvoice)
        CompanyId = ClaimInvoiceBO.CompanyId
        ClaimId = ClaimInvoiceBO.ClaimId
        ClaimNumber = ClaimInvoiceBO.ClaimNumber
        SvcControlNumber = ClaimInvoiceBO.SvcControlNumber
        RecordCount = ClaimInvoiceBO.RecordCount

        Dim claimBO As ClaimBase
        If Not ClaimInvoiceBO.ClaimId.Equals(Guid.Empty) Then
            claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimInvoiceBO.ClaimId)
        Else
            Throw New Exception("")
        End If

        ' add manufacturer, model and dealer name in disbursement for bank info for Germany - AB
        Dim CertItemCoverageBO As New CertItemCoverage(claimBO.CertItemCoverageId)
        Dim CertItemBO As New CertItem(CertItemCoverageBO.CertItemId)
        Dim ManufacturerDescription As String = String.Empty 'DEF-3565
        If Not CertItemBO.ManufacturerId.Equals(Guid.Empty) Then
            Dim ManufacturerBO As New Manufacturer(CertItemBO.ManufacturerId)
            ManufacturerDescription = ManufacturerBO.Description
        End If
        If CertItemBO.Model IsNot Nothing Then
            If CertItemBO.Model.Trim.Length > 40 Then
                Model = CertItemBO.Model.Substring(0, 40)
            Else
                Model = CertItemBO.Model
            End If
        End If

        If ManufacturerDescription.Trim.Length > 40 Then
            Manufacturer = ManufacturerDescription.Substring(0, 40)
        Else
            Manufacturer = ManufacturerDescription
        End If

        ' ----------
        Dim certBO As Certificate
        If Not claimBO.CertificateId.Equals(Guid.Empty) Then
            certBO = New Certificate(claimBO.CertificateId)
        Else
            Throw New Exception("")
        End If

        CustomerName = certBO.CustomerName

        Dim dealerBO As Dealer
        If Not certBO.DealerId.Equals(Guid.Empty) Then
            dealerBO = New Dealer(certBO.DealerId)
        Else
            Throw New Exception("")
        End If

        Dealer = dealerBO.Dealer
        If dealerBO.DealerName.Trim.Length > 40 Then
            DealerName = dealerBO.DealerName.Substring(0, 40)
        Else
            DealerName = dealerBO.DealerName
        End If

        Certificate = certBO.CertNumber
        ServiceCenterName = ClaimInvoiceBO.ServiceCenterName
        CheckNumber = New LongType(0)
        PaymentDate = New DateType(System.DateTime.Now)

        CapturePayeeInfo(ClaimInvoiceBO)

        PaymentAmount = ClaimInvoiceBO.Amount
        If ClaimInvoiceBO.IvaAmount.Value > 0 Then
            TaxAmount = ClaimInvoiceBO.IvaAmount
        Else
            TaxAmount = ClaimInvoiceBO.TotalTaxAmount
        End If

        DeductibleAmount = ClaimInvoiceBO.DeductibleAmount
        DeductibleTaxAmount = ClaimInvoiceBO.DeductibleTaxAmount
        Processed = "N"
        AuthorizationNumber = ClaimInvoiceBO.AuthorizationNumber

        'saving for Felita to elita passback WR
        AcctStatus = Codes.ACCT_STATUS__REQUESTED
        StatusDate = New DateType(System.DateTime.Now)

        If PayeeOptionId.Equals(Guid.Empty) AndAlso ClaimInvoiceBO.PayeeOptionCode <> "" Then
            PayeeOptionId = LookupListNew.GetIdFromCode(LookupListCache.LK_PAYEE, ClaimInvoiceBO.PayeeOptionCode)
        End If

        'claim details, withholdings and taxes
        LaborAmount = ClaimInvoiceBO.LaborAmt
        PartAmount = ClaimInvoiceBO.PartAmount
        ServiceAmount = ClaimInvoiceBO.ServiceCharge
        TripAmount = ClaimInvoiceBO.TripAmount
        ShippingAmount = ClaimInvoiceBO.ShippingAmount
        DiagnosticsAmount = ClaimInvoiceBO.DiagnosticsAmount
        DispositionAmount = ClaimInvoiceBO.DispositionAmount
        OtherAmount = ClaimInvoiceBO.OtherAmount
        WithholdingAmount = ClaimInvoiceBO.WithholdingAmount
        PaytocustomerAmount = ClaimInvoiceBO.PaytocustomerAmount

    End Sub

    Public Sub PopulateDisbursement(objOtherDisbursementBO As Disbursement)
        With objOtherDisbursementBO
            CompanyId = .CompanyId
            ClaimId = .ClaimId
            'Me.PayeeAddressID = .PayeeAddressID 'Commented out by AA for WR761620
            ClaimNumber = .ClaimNumber
            SvcControlNumber = .SvcControlNumber
            RecordCount = .RecordCount
            CustomerName = .CustomerName
            Dealer = .Dealer
            Certificate = .Certificate
            ServiceCenterName = .ServiceCenterName
            CheckNumber = .CheckNumber
            PaymentDate = .PaymentDate
            Payee = .Payee
            Address1 = .Address1
            Address2 = .Address2
            City = .City
            RegionDesc = .RegionDesc
            Zip = .Zip
            Country = .Country
            PayeeMailingLabel = .PayeeMailingLabel
            TaxAmount = .TaxAmount
            PaymentAmount = .PaymentAmount
            Processed = .Processed
            AuthorizationNumber = .AuthorizationNumber
            'Me.PayeeOption = .PayeeOption
            PayeeOptionId = .PayeeOptionId '.CompanyId
            AccountName = .AccountName
            BankID = .BankID
            AccountNumber = .AccountNumber
            SwiftCode = .SwiftCode
            IbanNumber = .IbanNumber
            AccountType = .AccountType
            PaymentMethod = .PaymentMethod
            IdentificationNumber = .IdentificationNumber
            DocumentType = .DocumentType
            Model = .Model
            Manufacturer = .Manufacturer
            PaymentReason = .PaymentReason
            DealerName = .DealerName
            AcctStatus = .AcctStatus ' For felita to elita passback
            StatusDate = .StatusDate ' For felita to elita passback
            TrackingNumber = .TrackingNumber ' For felita to elita passback
            VendorNumAcct = .VendorNumAcct
            VendorRegionDesc = .VendorRegionDesc
            perceptionIVA = .perceptionIVA
            perceptionIIBB = .perceptionIIBB
            BranchName = .BranchName
            BankName = .BankName
            BankSortCode = .BankSortCode
            BankSubCode = .BankSubCode
            BankLookupCode = .BankLookupCode
        End With
    End Sub

    Public Sub ReverseDisbursement(Optional ByVal blnReverseInvoiceTax As Boolean = False, Optional ByVal InvoiceIVA As Decimal = 0, Optional ByVal InvoiceIIBB As Decimal = 0)
        TaxAmount = New DecimalType((TaxAmount).Value * REVERSE_MULTIPLIER)
        PaymentAmount = New DecimalType((PaymentAmount).Value * REVERSE_MULTIPLIER)
        If blnReverseInvoiceTax Then
            perceptionIVA = New DecimalType(InvoiceIVA * REVERSE_MULTIPLIER)
            perceptionIIBB = New DecimalType(InvoiceIIBB * REVERSE_MULTIPLIER)
        Else
            Me.perceptionIVA = 0
            Me.perceptionIIBB = 0
        End If

    End Sub

    Public Shared Sub GetRemainingInvoicePaymentAfterReversal(strSVCControlNumber As String, OrigDisbID As Guid, ClaimID As Guid, _
                                                              ByRef RemainingPayment As Decimal, ByRef InvoiceIVA As Decimal, ByRef InvoiceIIBB As Decimal)
        Try
            Dim dal As New DisbursementDAL
            Dim ds As DataSet = dal.LoadRemainingInvoicePayment(strSVCControlNumber, OrigDisbID, ClaimID)
            RemainingPayment = CType(ds.Tables(0).Rows(0)(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT), Decimal)
            InvoiceIVA = CType(ds.Tables(0).Rows(0)(DisbursementDAL.COL_NAME_PERCEPTION_IVA), Decimal)
            InvoiceIIBB = CType(ds.Tables(0).Rows(0)(DisbursementDAL.COL_NAME_PERCEPTION_IIBB), Decimal)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Sub AdjustDisbursementAmounts(adjustmentPercentage As Decimal)
        TaxAmount = New DecimalType((TaxAmount).Value * adjustmentPercentage)
        PaymentAmount = New DecimalType((PaymentAmount).Value * adjustmentPercentage)
        Me.perceptionIVA = 0
        Me.perceptionIIBB = 0
    End Sub

    Private Sub CapturePayeeInfo(ClaimInvoiceBO As ClaimInvoice)

        If ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_MASTER_CENTER OrElse ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_SERVICE_CENTER OrElse ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_LOANER_CENTER Then
            If ClaimInvoiceBO.PayeeBankInfo IsNot Nothing Then
                'bank info:
                CopyBankInfoData(ClaimInvoiceBO.PayeeBankInfo)
            Else
                'address info:
                CopyAddressData(ClaimInvoiceBO.PayeeAddress)
            End If
            If Not ClaimAuthorizationId.Equals(Guid.Empty) Then
                PaymentMethod = ClaimInvoiceBO.PaymentMethodCode
            End If
        ElseIf ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_CUSTOMER OrElse ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_OTHER Then
            If ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                'address info:
                CopyAddressData(ClaimInvoiceBO.PayeeAddress)
                PaymentMethod = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                'bank info:
                CopyBankInfoData(ClaimInvoiceBO.PayeeBankInfo)
                PaymentMethod = Codes.PAYMENT_METHOD__BANK_TRANSFER
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__ADMIN_CHECK Then
                'Admin-Check:
                PaymentMethod = Codes.PAYMENT_METHOD__ADMIN_CHECK
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__DARTY_GIFT_CARD Then
                PaymentMethod = Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
            End If
        End If

        If ClaimInvoiceBO.IsInsuranceCompany AndAlso (ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_OTHER) Then
            DocumentType = ClaimInvoiceBO.DocumentType
            IdentificationNumber = ClaimInvoiceBO.TaxId
        End If

    End Sub
    Private Sub CopyBankInfoData(objPayeeBankInfo As BankInfo)
        AccountName = objPayeeBankInfo.Account_Name
        AccountNumber = objPayeeBankInfo.Account_Number
        BankID = objPayeeBankInfo.Bank_Id
        SwiftCode = objPayeeBankInfo.SwiftCode
        IbanNumber = objPayeeBankInfo.IbanNumber
        If Not (objPayeeBankInfo.AccountTypeId.Equals(Guid.Empty)) Then
            AccountType = LookupListNew.GetDescriptionFromId(LookupListCache.LK_ACCOUNT_TYPES, objPayeeBankInfo.AccountTypeId)
        End If
        Country = LookupListNew.GetDescriptionFromId(LookupListCache.LK_COUNTRIES, objPayeeBankInfo.CountryID)
        Dim objcompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim dv As DataView = LookupListNew.GetPaymentReasonLookupList(objcompany.LanguageId)
        PaymentReason = LookupListNew.GetDescriptionFromId(dv, objPayeeBankInfo.PaymentReasonID)
        If PaymentReason IsNot Nothing AndAlso PaymentReason.Trim.Length > 40 Then
            PaymentReason = PaymentReason.Substring(0, 40)
        End If

        BranchName = objPayeeBankInfo.BranchName
        BankName = objPayeeBankInfo.BankName
        BankSortCode = objPayeeBankInfo.BankSortCode
        BankSubCode = objPayeeBankInfo.BankSubCode
        BankLookupCode = objPayeeBankInfo.BankLookupCode

        IdentificationNumber = objPayeeBankInfo.TaxId

    End Sub

    Private Sub CopyAddressData(objPayeeAddress As Address)
        Address1 = objPayeeAddress.Address1
        Address2 = objPayeeAddress.Address2
        City = objPayeeAddress.City
        Zip = objPayeeAddress.PostalCode
        Dim regionBO As Region
        If Not objPayeeAddress.RegionId.Equals(Guid.Empty) Then
            regionBO = New Region(objPayeeAddress.RegionId)
            RegionDesc = regionBO.Description
        End If
        Country = LookupListNew.GetDescriptionFromId(LookupListCache.LK_COUNTRIES, objPayeeAddress.CountryId)
        PayeeMailingLabel = objPayeeAddress.MailingAddressLabel
    End Sub

    Public Shared Function GetDisbursementFromClaim(claimId As Guid) As DataTable

        Try
            Dim dal As New DisbursementDAL

            Return dal.LoadDisbursementFromClaim(claimId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class DisbursementSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DISBURSEMENT_ID As String = DisbursementDAL.COL_NAME_DISBURSEMENT_ID
        Public Const COL_COMPANY_ID As String = DisbursementDAL.COL_NAME_COMPANY_ID
        Public Const COL_CLAIM_ID As String = DisbursementDAL.COL_NAME_CLAIM_ID
        Public Const COL_CLAIM_NUMBER As String = DisbursementDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_SVC_CONTROL_NUMBER As String = DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER
        Public Const COL_RECORD_COUNT As String = DisbursementDAL.COL_NAME_RECORD_COUNT
        Public Const COL_CUSTOMER_NAME As String = DisbursementDAL.COL_NAME_CUSTOMER_NAME
        Public Const COL_DEALER As String = DisbursementDAL.COL_NAME_DEALER
        Public Const COL_CERTIFICATE As String = DisbursementDAL.COL_NAME_CERTIFICATE
        Public Const COL_SERVICE_CENTER_NAME As String = DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME
        Public Const COL_CHECK_NUMBER As String = DisbursementDAL.COL_NAME_CHECK_NUMBER
        Public Const COL_TRACKING_NUMBER As String = DisbursementDAL.COL_NAME_TRACKING_NUMBER
        Public Const COL_PAYMENT_DATE As String = DisbursementDAL.COL_NAME_PAYMENT_DATE
        Public Const COL_PAYEE As String = DisbursementDAL.COL_NAME_PAYEE
        Public Const COL_ADDRESS1 As String = DisbursementDAL.COL_NAME_ADDRESS1
        Public Const COL_ADDRESS2 As String = DisbursementDAL.COL_NAME_ADDRESS2
        Public Const COL_CITY As String = DisbursementDAL.COL_NAME_CITY
        Public Const COL_REGION_DESC As String = DisbursementDAL.COL_NAME_REGION_DESC
        Public Const COL_ZIP As String = DisbursementDAL.COL_NAME_ZIP
        Public Const COL_PAYEE_MAILING_LABEL As String = DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL
        Public Const COL_TAX_AMOUNT As String = DisbursementDAL.COL_NAME_TAX_AMOUNT
        Public Const COL_PAYMENT_AMOUNT As String = DisbursementDAL.COL_NAME_PAYMENT_AMOUNT
        Public Const COL_PROCESSED As String = DisbursementDAL.COL_NAME_PROCESSED
        Public Const COL_AUTHORIZATION_NUMBER As String = DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER
        Public Const COL_ACCT_STATUS As String = DisbursementDAL.COL_NAME_ACCT_STATUS
        Public Const COL_STATUS_DATE As String = DisbursementDAL.COL_NAME_STATUS_DATE
        'Public Const COL_PAYEE_ADDRESS_ID As String = DisbursementDAL.COL_NAME_PAYEE_ADDRESS_ID
        'Public Const COL_PAYEE_OPTION As String = DisbursementDAL.COL_NAME_PAYEE_OPTION
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidIIBBProvince
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PERCEPTION_REGION_REQUIRED)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Disbursement = CType(objectToValidate, Disbursement)

            If (obj.perceptionIIBB IsNot Nothing AndAlso obj.perceptionIIBB.Value > 0) And obj.VendorRegionDesc = Nothing Then Return False

            Return True

        End Function
    End Class
#End Region

#Region "Children"
    'Public Sub LoadAgain()
    '    Me.Load()
    'End Sub
    Public Function CreateSecondDisbursement() As Disbursement
        Return SecondDisbursement
    End Function
#End Region

End Class
