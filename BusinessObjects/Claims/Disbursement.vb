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
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal ClaimInvoiceBO As ClaimInvoice)
        MyBase.New(False)
        Me.Dataset = ClaimInvoiceBO.Dataset
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DisbursementDAL
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
            Dim dal As New DisbursementDAL
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
    Private _disbursement As Disbursement = Nothing
    Private _sourceCountryID As Guid
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(DisbursementDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_DISBURSEMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CLAIM_ID, Value)
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
    Public Property PayeeOptionId() As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYEE_OPTION_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property SvcControlNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RecordCount() As LongType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_RECORD_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DisbursementDAL.COL_NAME_RECORD_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_RECORD_COUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property Dealer() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DEALER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property ServiceCenterName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_SERVICE_CENTER_NAME, Value)
        End Set
    End Property

    Public Property CheckNumber() As LongType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CHECK_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DisbursementDAL.COL_NAME_CHECK_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CHECK_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property TrackingNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    'need to be mandatory..............
    <ValueMandatory("")> _
    Public Property AcctStatus() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCT_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCT_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ACCT_STATUS, Value)
        End Set
    End Property

    ' needs to mandatory ................
    <ValueMandatory("")> _
    Public Property StatusDate() As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_STATUS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_STATUS_DATE, Value)
        End Set
    End Property

    Public Property PaymentDate() As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_PAYMENT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYMENT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Payee() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYEE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYEE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property RegionDesc() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_REGION_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_REGION_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_REGION_DESC, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)> _
    Public Property Zip() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ZIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ZIP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ZIP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property PayeeMailingLabel() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYEE_MAILING_LABEL, Value)
        End Set
    End Property


    Public Property DeductibleAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DEDUCTIBLE_AMOUNT, Value)
        End Set
    End Property


    Public Property DeductibleTaxAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, Value)
        End Set
    End Property



    Public Property TaxAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TAX_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_TAX_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_TAX_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PaymentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYMENT_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property Processed() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PROCESSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PROCESSED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PROCESSED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property AccountName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ACCOUNT_NAME, Value)
        End Set
    End Property

    ' <ValidNumericRange("", Min:=0)> _
    <ValidStringLength("", Min:=0, Max:=10)> _
    Public Property BankID() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Min:=0, Max:=29)> _
    Public Property AccountNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property PaymentMethod() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYMENT_METHOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYMENT_METHOD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property Manufacturer() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property DealerName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DEALER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DEALER_NAME, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=40)> _
    Public Property PaymentReason() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAYMENT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_PAYMENT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAYMENT_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property IdentificationNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property DocumentType() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property Country() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_COUNTRY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_COUNTRY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_COUNTRY, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property IbanNumber() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_IBAN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_IBAN_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property SwiftCode() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_SWIFT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_SWIFT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)> _
    Public Property AccountType() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ACCOUNT_TYPE, Value)
        End Set
    End Property

    Public Property AccountTypeId() As Guid

        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_ACCOUNT_TYPE_ID, Value)
        End Set
    End Property

    Public Property VendorNumAcct() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_VENDOR_NUM_ACCT, Value)
        End Set
    End Property
    <ValidIIBBProvince("")> _
    Public Property VendorRegionDesc() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_VENDOR_REGION_DESC, Value)
        End Set
    End Property

    Public Property perceptionIVA() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PERCEPTION_IVA) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PERCEPTION_IVA), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PERCEPTION_IVA, Value)
        End Set
    End Property


    Public Property perceptionIIBB() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PERCEPTION_IIBB) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PERCEPTION_IIBB), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PERCEPTION_IIBB, Value)
        End Set
    End Property

    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DisbursementDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property BranchName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    Public Property BankName() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BANK_NAME, Value)
        End Set
    End Property

    Public Property BankSortCode() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_SORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_SORT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BANK_SORT_CODE, Value)
        End Set
    End Property

    Public Property BankLookupCode() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BANK_LOOKUP_CODE, Value)
        End Set
    End Property

    Public Property BankSubCode() As String
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BANK_SUB_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DisbursementDAL.COL_NAME_BANK_SUB_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BANK_SUB_CODE, Value)
        End Set
    End Property

    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property Bonus() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Public Property BonusTax() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property



    Public Property LaborAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_LABOR_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_LABOR_AMOUNT, Value)
        End Set
    End Property


    Public Property PartAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PART_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property



    Public Property ServiceAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SERVICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_SERVICE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_SERVICE_AMOUNT, Value)
        End Set
    End Property



    Public Property TripAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_TRIP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property



    Public Property ShippingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property



    Public Property DispositionAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DISPOSITION_AMOUNT, Value)
        End Set
    End Property


    Public Property OtherAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_OTHER_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property



    Public Property DiagnosticsAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_DIAGNOSTICS_AMOUNT, Value)
        End Set
    End Property



    Public Property WithholdingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_WITHHOLDING_AMOUNT, Value)
        End Set
    End Property

    Public Property PaytocustomerAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DisbursementDAL.COL_NAME_PAY_TO_CUST_AMOUNT, Value)
        End Set
    End Property

#End Region

#Region "Derived Properties"
    Public ReadOnly Property SecondDisbursement() As Disbursement
        Get
            If _disbursement Is Nothing Then
                _disbursement = New Disbursement(Me.Dataset)
            End If
            Return _disbursement
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DisbursementDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function GetList(ByVal svcControlNumber As String, ByVal companyList As ArrayList) As DisbursementSearchDV

        Try
            Dim dal As New DisbursementDAL

            Return New DisbursementSearchDV(dal.LoadPayeeList(svcControlNumber, companyList).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetDisbursement(ByVal id As Guid) As DisbursementSearchDV

        Try
            Dim dal As New DisbursementDAL

            Return New DisbursementSearchDV(dal.LoadDisbursement(id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Sub DeleteNewChildDisbursement(ByVal parentClaim As Claim)
        Dim row As DataRow
        If parentClaim.Dataset.Tables.IndexOf(CommentDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            'For Each row In parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows
            For rowIndex = 0 To parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Count - 1
                row = parentClaim.Dataset.Tables(CommentDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim c As Comment = New Comment(row)
                    If parentClaim.Id.Equals(c.ClaimId) And c.IsNew Then
                        c.Delete()
                    End If
                End If
            Next

        End If
    End Sub

    Public Sub PrepopulateFromClaimInvoice(ByVal ClaimInvoiceBO As ClaimInvoice)
        Me.CompanyId = ClaimInvoiceBO.CompanyId
        Me.ClaimId = ClaimInvoiceBO.ClaimId
        Me.ClaimNumber = ClaimInvoiceBO.ClaimNumber
        Me.SvcControlNumber = ClaimInvoiceBO.SvcControlNumber
        Me.RecordCount = ClaimInvoiceBO.RecordCount

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
        If Not CertItemBO.Model Is Nothing Then
            If CertItemBO.Model.Trim.Length > 40 Then
                Me.Model = CertItemBO.Model.Substring(0, 40)
            Else
                Me.Model = CertItemBO.Model
            End If
        End If

        If ManufacturerDescription.Trim.Length > 40 Then
            Me.Manufacturer = ManufacturerDescription.Substring(0, 40)
        Else
            Me.Manufacturer = ManufacturerDescription
        End If

        ' ----------
        Dim certBO As Certificate
        If Not claimBO.CertificateId.Equals(Guid.Empty) Then
            certBO = New Certificate(claimBO.CertificateId)
        Else
            Throw New Exception("")
        End If

        Me.CustomerName = certBO.CustomerName

        Dim dealerBO As Dealer
        If Not certBO.DealerId.Equals(Guid.Empty) Then
            dealerBO = New Dealer(certBO.DealerId)
        Else
            Throw New Exception("")
        End If

        Me.Dealer = dealerBO.Dealer
        If dealerBO.DealerName.Trim.Length > 40 Then
            Me.DealerName = dealerBO.DealerName.Substring(0, 40)
        Else
            Me.DealerName = dealerBO.DealerName
        End If

        Me.Certificate = certBO.CertNumber
        Me.ServiceCenterName = ClaimInvoiceBO.ServiceCenterName
        Me.CheckNumber = New LongType(0)
        Me.PaymentDate = New DateType(System.DateTime.Now)

        Me.CapturePayeeInfo(ClaimInvoiceBO)

        Me.PaymentAmount = ClaimInvoiceBO.Amount
        If ClaimInvoiceBO.IvaAmount.Value > 0 Then
            Me.TaxAmount = ClaimInvoiceBO.IvaAmount
        Else
            Me.TaxAmount = ClaimInvoiceBO.TotalTaxAmount
        End If

        Me.DeductibleAmount = ClaimInvoiceBO.DeductibleAmount
        Me.DeductibleTaxAmount = ClaimInvoiceBO.DeductibleTaxAmount
        Me.Processed = "N"
        Me.AuthorizationNumber = ClaimInvoiceBO.AuthorizationNumber

        'saving for Felita to elita passback WR
        Me.AcctStatus = Codes.ACCT_STATUS__REQUESTED
        Me.StatusDate = New DateType(System.DateTime.Now)

        If Me.PayeeOptionId.Equals(Guid.Empty) AndAlso ClaimInvoiceBO.PayeeOptionCode <> "" Then
            Me.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoiceBO.PayeeOptionCode)
        End If

        'claim details, withholdings and taxes
        Me.LaborAmount = ClaimInvoiceBO.LaborAmt
        Me.PartAmount = ClaimInvoiceBO.PartAmount
        Me.ServiceAmount = ClaimInvoiceBO.ServiceCharge
        Me.TripAmount = ClaimInvoiceBO.TripAmount
        Me.ShippingAmount = ClaimInvoiceBO.ShippingAmount
        Me.DiagnosticsAmount = ClaimInvoiceBO.DiagnosticsAmount
        Me.DispositionAmount = ClaimInvoiceBO.DispositionAmount
        Me.OtherAmount = ClaimInvoiceBO.OtherAmount
        Me.WithholdingAmount = ClaimInvoiceBO.WithholdingAmount
        Me.PaytocustomerAmount = ClaimInvoiceBO.PaytocustomerAmount

    End Sub

    Public Sub PopulateDisbursement(ByVal objOtherDisbursementBO As Disbursement)
        With objOtherDisbursementBO
            Me.CompanyId = .CompanyId
            Me.ClaimId = .ClaimId
            'Me.PayeeAddressID = .PayeeAddressID 'Commented out by AA for WR761620
            Me.ClaimNumber = .ClaimNumber
            Me.SvcControlNumber = .SvcControlNumber
            Me.RecordCount = .RecordCount
            Me.CustomerName = .CustomerName
            Me.Dealer = .Dealer
            Me.Certificate = .Certificate
            Me.ServiceCenterName = .ServiceCenterName
            Me.CheckNumber = .CheckNumber
            Me.PaymentDate = .PaymentDate
            Me.Payee = .Payee
            Me.Address1 = .Address1
            Me.Address2 = .Address2
            Me.City = .City
            Me.RegionDesc = .RegionDesc
            Me.Zip = .Zip
            Me.Country = .Country
            Me.PayeeMailingLabel = .PayeeMailingLabel
            Me.TaxAmount = .TaxAmount
            Me.PaymentAmount = .PaymentAmount
            Me.Processed = .Processed
            Me.AuthorizationNumber = .AuthorizationNumber
            'Me.PayeeOption = .PayeeOption
            Me.PayeeOptionId = .PayeeOptionId '.CompanyId
            Me.AccountName = .AccountName
            Me.BankID = .BankID
            Me.AccountNumber = .AccountNumber
            Me.SwiftCode = .SwiftCode
            Me.IbanNumber = .IbanNumber
            Me.AccountType = .AccountType
            Me.PaymentMethod = .PaymentMethod
            Me.IdentificationNumber = .IdentificationNumber
            Me.DocumentType = .DocumentType
            Me.Model = .Model
            Me.Manufacturer = .Manufacturer
            Me.PaymentReason = .PaymentReason
            Me.DealerName = .DealerName
            Me.AcctStatus = .AcctStatus ' For felita to elita passback
            Me.StatusDate = .StatusDate ' For felita to elita passback
            Me.TrackingNumber = .TrackingNumber ' For felita to elita passback
            Me.VendorNumAcct = .VendorNumAcct
            Me.VendorRegionDesc = .VendorRegionDesc
            Me.perceptionIVA = .perceptionIVA
            Me.perceptionIIBB = .perceptionIIBB
            Me.BranchName = .BranchName
            Me.BankName = .BankName
            Me.BankSortCode = .BankSortCode
            Me.BankSubCode = .BankSubCode
            Me.BankLookupCode = .BankLookupCode
        End With
    End Sub

    Public Sub ReverseDisbursement(Optional ByVal blnReverseInvoiceTax As Boolean = False, Optional ByVal InvoiceIVA As Decimal = 0, Optional ByVal InvoiceIIBB As Decimal = 0)
        Me.TaxAmount = New DecimalType((Me.TaxAmount).Value * REVERSE_MULTIPLIER)
        Me.PaymentAmount = New DecimalType((Me.PaymentAmount).Value * REVERSE_MULTIPLIER)
        If blnReverseInvoiceTax Then
            Me.perceptionIVA = New DecimalType(InvoiceIVA * REVERSE_MULTIPLIER)
            Me.perceptionIIBB = New DecimalType(InvoiceIIBB * REVERSE_MULTIPLIER)
        Else
            Me.perceptionIVA = 0
            Me.perceptionIIBB = 0
        End If

    End Sub

    Public Shared Sub GetRemainingInvoicePaymentAfterReversal(ByVal strSVCControlNumber As String, ByVal OrigDisbID As Guid, ByVal ClaimID As Guid, _
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

    Public Sub AdjustDisbursementAmounts(ByVal adjustmentPercentage As Decimal)
        Me.TaxAmount = New DecimalType((Me.TaxAmount).Value * adjustmentPercentage)
        Me.PaymentAmount = New DecimalType((Me.PaymentAmount).Value * adjustmentPercentage)
        Me.perceptionIVA = 0
        Me.perceptionIIBB = 0
    End Sub

    Private Sub CapturePayeeInfo(ByVal ClaimInvoiceBO As ClaimInvoice)

        If ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_MASTER_CENTER Or _
           ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_SERVICE_CENTER Or _
           ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_LOANER_CENTER Then
            If Not ClaimInvoiceBO.PayeeBankInfo Is Nothing Then
                'bank info:
                Me.CopyBankInfoData(ClaimInvoiceBO.PayeeBankInfo)
            Else
                'address info:
                Me.CopyAddressData(ClaimInvoiceBO.PayeeAddress)
            End If
            If Not Me.ClaimAuthorizationId.Equals(Guid.Empty) Then
                Me.PaymentMethod = ClaimInvoiceBO.PaymentMethodCode
            End If
        ElseIf ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_CUSTOMER Or _
               ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_OTHER Then
            If ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                'address info:
                Me.CopyAddressData(ClaimInvoiceBO.PayeeAddress)
                Me.PaymentMethod = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                'bank info:
                Me.CopyBankInfoData(ClaimInvoiceBO.PayeeBankInfo)
                Me.PaymentMethod = Codes.PAYMENT_METHOD__BANK_TRANSFER
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__ADMIN_CHECK Then
                'Admin-Check:
                Me.PaymentMethod = Codes.PAYMENT_METHOD__ADMIN_CHECK
            ElseIf ClaimInvoiceBO.PaymentMethodCode = Codes.PAYMENT_METHOD__DARTY_GIFT_CARD Then
                Me.PaymentMethod = Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
            End If
        End If

        If ClaimInvoiceBO.IsInsuranceCompany And (ClaimInvoiceBO.PayeeOptionCode = ClaimInvoiceBO.PAYEE_OPTION_OTHER) Then
            Me.DocumentType = ClaimInvoiceBO.DocumentType
            Me.IdentificationNumber = ClaimInvoiceBO.TaxId
        End If

    End Sub
    Private Sub CopyBankInfoData(ByVal objPayeeBankInfo As BankInfo)
        Me.AccountName = objPayeeBankInfo.Account_Name
        Me.AccountNumber = objPayeeBankInfo.Account_Number
        Me.BankID = objPayeeBankInfo.Bank_Id
        Me.SwiftCode = objPayeeBankInfo.SwiftCode
        Me.IbanNumber = objPayeeBankInfo.IbanNumber
        If Not (objPayeeBankInfo.AccountTypeId.Equals(Guid.Empty)) Then
            Me.AccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, objPayeeBankInfo.AccountTypeId)
        End If
        Me.Country = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, objPayeeBankInfo.CountryID)
        Dim objcompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim dv As DataView = LookupListNew.GetPaymentReasonLookupList(objcompany.LanguageId)
        Me.PaymentReason = LookupListNew.GetDescriptionFromId(dv, objPayeeBankInfo.PaymentReasonID)
        If Not Me.PaymentReason Is Nothing AndAlso Me.PaymentReason.Trim.Length > 40 Then
            Me.PaymentReason = Me.PaymentReason.Substring(0, 40)
        End If

        Me.BranchName = objPayeeBankInfo.BranchName
        Me.BankName = objPayeeBankInfo.BankName
        Me.BankSortCode = objPayeeBankInfo.BankSortCode
        Me.BankSubCode = objPayeeBankInfo.BankSubCode
        Me.BankLookupCode = objPayeeBankInfo.BankLookupCode

        Me.IdentificationNumber = objPayeeBankInfo.TaxId

    End Sub

    Private Sub CopyAddressData(ByVal objPayeeAddress As Address)
        Me.Address1 = objPayeeAddress.Address1
        Me.Address2 = objPayeeAddress.Address2
        Me.City = objPayeeAddress.City
        Me.Zip = objPayeeAddress.PostalCode
        Dim regionBO As Region
        If Not objPayeeAddress.RegionId.Equals(Guid.Empty) Then
            regionBO = New Region(objPayeeAddress.RegionId)
            Me.RegionDesc = regionBO.Description
        End If
        Me.Country = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, objPayeeAddress.CountryId)
        Me.PayeeMailingLabel = objPayeeAddress.MailingAddressLabel
    End Sub

    Public Shared Function GetDisbursementFromClaim(ByVal claimId As Guid) As DataTable

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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidIIBBProvince
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PERCEPTION_REGION_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Disbursement = CType(objectToValidate, Disbursement)

            If (Not obj.perceptionIIBB Is Nothing AndAlso obj.perceptionIIBB.Value > 0) And obj.VendorRegionDesc = Nothing Then Return False

            Return True

        End Function
    End Class
#End Region

#Region "Children"
    'Public Sub LoadAgain()
    '    Me.Load()
    'End Sub
    Public Function CreateSecondDisbursement() As Disbursement
        Return Me.SecondDisbursement
    End Function
#End Region

End Class
