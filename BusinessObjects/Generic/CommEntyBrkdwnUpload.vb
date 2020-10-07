'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/28/2016)  ********************

Public Class CommEntyBrkdwnUpload
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
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
            Dim dal As New CommEntyBrkdwnUploadDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CommEntyBrkdwnUploadDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
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
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(CommEntyBrkdwnUploadDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property UploadSessionId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_UPLOAD_SESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_UPLOAD_SESSION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_UPLOAD_SESSION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RecordNumber As LongType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_RECORD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_RECORD_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_RECORD_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2000)>
    Public Property ValidationErrors As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_VALIDATION_ERRORS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_VALIDATION_ERRORS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_VALIDATION_ERRORS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public ReadOnly Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
    End Property

    Public Property Effective As DateType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public Property AllowMarkupPCT As DecimalType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ALLOW_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ALLOW_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ALLOW_MARKUP_PCT, Value)
        End Set
    End Property

    Public Property Tolerance As DecimalType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_TOLERANCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_TOLERANCE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_TOLERANCE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    Public Property Position As Integer
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_POSITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_POSITION), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_POSITION, Value)
        End Set
    End Property

    Public Property EntityId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property EntityName As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ENTITY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ENTITY_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_VALIDATION_ERRORS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)>
    Public Property Phone As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property Email As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property City As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_CITY, Value)
        End Set
    End Property

    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    Public Property DisplayId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_DISPLAY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_DISPLAY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_DISPLAY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property TaxId As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property

    Public Property CommissionEntityTypeid As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMM_ENTY_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMM_ENTY_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_COMM_ENTY_TYPE_ID, Value)
        End Set
    End Property

    Public Property PaymentMethodId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property AccountName As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NAME, Value)
        End Set
    End Property

    Public Property BankCountryId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_COUNTRY_ID, Value)
        End Set
    End Property

    Public Property PaymentReasonId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_PYMT_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_PYMT_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_PYMT_REASON_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property BranchName As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property BankName As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BankSortCode As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SORT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SORT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property IbanNumber As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_IBAN_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_IBAN_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property SwiftCode As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_SWIFT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_SWIFT_CODE, Value)
        End Set
    End Property

    Public Property AccountTypeId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_TYPE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BankId As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=29)>
    Public Property AccountNumber As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BankLookupCode As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_LOOKUP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_LOOKUP_CODE, Value)
        End Set
    End Property

    Public Property TransactionLimit As DecimalType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_TRANSACTION_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_TRANSACTION_LIMIT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_TRANSACTION_LIMIT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property BankSubCode As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SUB_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SUB_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_SUB_CODE, Value)
        End Set
    End Property

    Public Property BranchDigit As LongType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_DIGIT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_DIGIT, Value)
        End Set
    End Property

    Public Property AccountDigit As LongType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_DIGIT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_ACCOUNT_DIGIT, Value)
        End Set
    End Property

    Public Property BranchNumber As LongType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BRANCH_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BankTaxId As String
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_BANK_TAX_ID, Value)
        End Set
    End Property

    Public Property PayeeTypeId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_PAYEE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_PAYEE_TYPE_ID, Value)
        End Set
    End Property

    Public Property MarkupPercent As DecimalType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_MARKUP_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_MARKUP_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_MARKUP_PERCENT, Value)
        End Set
    End Property

    Private _markuppercenttotal As DecimalType
    Public Property MarkupPercentTotal As DecimalType
        Get
            Return _markuppercenttotal
        End Get
        Set
            _markuppercenttotal = value
        End Set
    End Property

    Public Property CommissionPercent As DecimalType
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMMISSION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMMISSION_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_COMMISSION_PERCENT, Value)
        End Set
    End Property

    Private _commissionpercenttotal As DecimalType
    Public Property CommissionPercentTotal As DecimalType
        Get
            Return _commissionpercenttotal
        End Get
        Set
            _commissionpercenttotal = value
        End Set
    End Property

    Public Property ComputeMethodId As Guid
        Get
            CheckDeleted()
            If Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMP_MTHD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommEntyBrkdwnUploadDAL.COL_NAME_COMP_MTHD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommEntyBrkdwnUploadDAL.COL_NAME_COMP_MTHD_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommEntyBrkdwnUploadDAL
                dal.Update(Row)
                'Reload the Data from the DB
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetPreValidatedCommEntyBrkdwnsForDealer(ByVal UploadSessionId As String) As DataSet
        Try
            Dim dal As New CommEntyBrkdwnUploadDAL
            Return dal.LoadPreValidatedCommEntyBrkdwnsForDealer(UploadSessionId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetPreValidatedCommEntyBrkdwnsForUpload(ByVal UploadSessionId As String, ByVal Daler_Id As Guid) As DataSet
        Try
            Dim dal As New CommEntyBrkdwnUploadDAL
            Return dal.LoadPreValidatedCommEntyBrkdwnsForUpload(UploadSessionId, Daler_Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function UpdatePreValidatedCommEntyBrkdwnRecord(preValidatedCommEntyBrkdwnId As Guid, ByVal strValidationErrors As String) As DataSet
        Try
            Dim dal As New CommEntyBrkdwnUploadDAL
            Return dal.UpdatePreValidatedCommEntyBrkdwnRecord(preValidatedCommEntyBrkdwnId, strValidationErrors)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class