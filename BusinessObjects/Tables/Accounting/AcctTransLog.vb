'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/16/2007)  ********************

Public Class AcctTransLog
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
            Dim dal As New AcctTransLogDAL
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
            Dim dal As New AcctTransLogDAL
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

    Private Shared VENDOR_EVENT As String = "VEND"
    Private Shared EVENT_CLAIMS As String = "CLAIM"
    Private Shared EVENT_REFUNDS As String = "REFUNDS"

    Private Const YES_STRING As String = "Y"
    Private Const NO_STRING As String = "N"

    Public Const COL_EVENT_TYPE As String = "GeneralDescription24"

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(AcctTransLogDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property CommissionEntityId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_COMMISSION_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_COMMISSION_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_COMMISSION_ENTITY_ID, Value)
        End Set
    End Property

    Public Property AcctEventTypeId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCT_EVENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_ACCT_EVENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCT_EVENT_TYPE_ID, Value)
        End Set
    End Property



    Public Property AcctEventFieldId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCT_EVENT_FIELD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_ACCT_EVENT_FIELD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCT_EVENT_FIELD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctCompanyId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCT_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCT_COMPANY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property Country() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_COUNTRY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_COUNTRY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_COUNTRY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property Region() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_REGION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property RegionDescription() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_REGION_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_REGION_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_REGION_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property TaxIdCode() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_TAX_ID_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_TAX_ID_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_TAX_ID_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=48)> _
    Public Property Currency() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_CURRENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_CURRENCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_CURRENCY, Value)
        End Set
    End Property



    Public Property BankId() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property



    Public Property BankAccountNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ACCOUNT_NUMBER), Long)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ACCOUNT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=320)> _
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Payee() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_PAYEE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYEE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Zip() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ZIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_ZIP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ZIP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=640)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property



    Public Property PaymentAmount() As LongType
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT, Value)
        End Set
    End Property



    Public Property PaymentAmountRev() As LongType
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT_REV) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT_REV), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT_REV, Value)
        End Set
    End Property



    Public Property PaymentDate() As DateType
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctTransLogDAL.COL_NAME_PAYMENT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYMENT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=28)> _
    Public Property AcctPeriod() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCT_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_ACCT_PERIOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCT_PERIOD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property CoverageType() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_COVERAGE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_COVERAGE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_COVERAGE_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property NetworkId() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_NETWORK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_NETWORK_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property PaymentNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_PAYMENT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYMENT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1600)> _
    Public Property TransactionIdNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_TRANSACTION_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_TRANSACTION_ID_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_TRANSACTION_ID_NUMBER, Value)
        End Set
    End Property

    Public Property ProcessDate() As DateType
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PROCESS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctTransLogDAL.COL_NAME_PROCESS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PROCESS_DATE, Value)
        End Set
    End Property

    Public Property AcctTransmissionId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCT_TRANSMISSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransLogDAL.COL_NAME_ACCT_TRANSMISSION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCT_TRANSMISSION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property VendorUpdate() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_VENDOR_UPDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_VENDOR_UPDATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_VENDOR_UPDATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property PaymentToCustomer() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_PAYMENT_TO_CUSTOMER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_PAYMENT_TO_CUSTOMER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_PAYMENT_TO_CUSTOMER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankSortCode() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_SORTCODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_SORTCODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_SORTCODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankAddress1() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankAddress2() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_2, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankAddress3() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_3, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankAddress4() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_4), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_ADDRESS_4, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankName1() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_NAME_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_NAME_1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_NAME_1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankName2() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_NAME_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_NAME_2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_NAME_2, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankIBAN() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_IBAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_IBAN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_IBAN, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property BankBranch() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_BANK_BRANCH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_BANK_BRANCH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_BANK_BRANCH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property WarrSalesDate() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_WARR_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_WARR_SALES_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_WARR_SALES_DATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)> _
    Public Property ContractInceptionDate() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_CONTRACT_INCEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_CONTRACT_INCEPTION_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_CONTRACT_INCEPTION_DATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=15)> _
    Public Property AccountCode() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    Public Property PolicyNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransLogDAL.COL_NAME_POLICY_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransLogDAL.COL_NAME_POLICY_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctTransLogDAL.COL_NAME_POLICY_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctTransLogDAL
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

    Public Function GetAccountingInterfaceTables(ByVal oFelitaEngineData As FelitaEngine.FelitaEngineData) As DataSet()


        Dim dal As New AcctTransLogDAL
        Dim strEventType As String
        Dim dsSet As New ArrayList
        Dim ds As DataSet
        Dim _bu As New AcctBusinessUnit
        Dim BusinessUnitDV As AcctBusinessUnit.AcctBusinessUnitSearchDV = _bu.getList(oFelitaEngineData.AccountingCompanyId, Nothing)
        Dim boAcctExecLog As AcctExecLog
        Dim isBalanced As Boolean
        Dim _acctExtension As String
        Dim _AcctCompany As AcctCompany

        Try

            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "GetAccountingInterfaceTables", "Entering procedure to gather data")

            'Check the accounting type to determine the type of grouping if any
            _AcctCompany = New AcctCompany(oFelitaEngineData.AccountingCompanyId)
            _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)

            If BusinessUnitDV.Count > 0 Then
                For Each dvRow As System.Data.DataRowView In BusinessUnitDV
                    ds = New DataSet(dal.DatasetName)
                    If oFelitaEngineData.isVendorFile AndAlso _
                        dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS) = NO_STRING AndAlso _
                         Not _acctExtension = FelitaEngine.SMARTSTREAM_PREFIX Then
                        ds = dal.GetVendorFiles(oFelitaEngineData.CompanyId, ds)
                    End If

                    'Check if the allEvents flag is set.  if so, generate all, else, determine if the eventID exists, and 
                    '  generate just that event.  If not, no events will be generated.
                    If oFelitaEngineData.AllEvents Then
                        Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
                        Dim dsTemp As DataSet
                        Dim dtTemp As DataTable
                        Dim dsTempAP As DataSet
                        Dim dsTempPurge As DataSet

                        For Each dvItem As System.Data.DataRowView In dv
                            If Not dvItem(LookupListNew.COL_CODE_NAME).ToString.Equals(VENDOR_EVENT) Then
                                dsTemp = New DataSet(dal.DatasetName)

                                dsTemp = dal.GetJournalEntries(oFelitaEngineData.CompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())), _
                                GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), dvItem(LookupListNew.COL_CODE_NAME), ElitaPlusIdentity.Current.ActiveUser.NetworkId, False)

                                'Check if items balance before adding to our dataset.
                                isBalanced = MergeDataSets(oFelitaEngineData.CompanyId, _acctExtension, dvItem(LookupListNew.COL_CODE_NAME).ToString, ds, dsTemp)

                                'ALR - added if we are smartstream AND this is claims or refunds
                                If isBalanced AndAlso _
                                    _acctExtension = FelitaEngine.SMARTSTREAM_PREFIX AndAlso _
                                    (dvItem(LookupListNew.COL_CODE_NAME).ToString.Equals(EVENT_REFUNDS) OrElse _
                                     dvItem(LookupListNew.COL_CODE_NAME).ToString.Equals(EVENT_CLAIMS)) Then

                                    dsTempAP = dal.GetJournalEntriesAP(oFelitaEngineData.CompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())), _
                                    GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), dvItem(LookupListNew.COL_CODE_NAME))

                                    If Not dsTempAP Is Nothing AndAlso dsTempAP.Tables.Count > 0 AndAlso dsTempAP.Tables(0).Rows.Count > 0 Then
                                        If ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM) Is Nothing Then
                                            ds.Tables.Add(dsTempAP.Tables(0).Copy)
                                        Else
                                            ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).Merge(dsTempAP.Tables(0))
                                        End If

                                        'TODO - ALR- Remove vendor build once SS load is ready -
                                        If ds.Tables(AcctTransLogDAL.Table_AP_VENDORS) IsNot Nothing Then
                                            ds.Tables.Remove(AcctTransLogDAL.Table_AP_VENDORS)
                                        End If

                                        ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).DefaultView.RowFilter = "PAYMENT_TO_CUSTOMER = 'Y'"
                                        If ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).DefaultView.Count > 0 Then
                                            ds.Tables.Add(BuildVendorTable(ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM)))
                                        End If

                                    End If

                                    'Get Records to purge
                                    dsTempPurge = dal.GetPurgeTable(oFelitaEngineData.CompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())), _
                                    GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), dvItem(LookupListNew.COL_CODE_NAME))
                                    If dsTempPurge IsNot Nothing AndAlso dsTempPurge.Tables(dal.Table_AP_PURGE) IsNot Nothing AndAlso dsTempPurge.Tables(dal.Table_AP_PURGE).Rows.Count > 0 Then
                                        If ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                                            ds.Tables.Add(dsTempPurge.Tables(AcctTransLogDAL.Table_AP_PURGE).Copy)
                                        Else
                                            ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Merge(dsTempPurge.Tables(AcctTransLogDAL.Table_AP_PURGE))
                                        End If
                                    End If
                                End If

                                'Update the exec log table with a success or failure based on balancing
                                boAcctExecLog = New AcctExecLog(oFelitaEngineData.CompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())))
                                boAcctExecLog.UpdateExecStatus(isBalanced)

                                If ((Not dsTemp.Tables(dal.Table_LINEITEM) Is Nothing AndAlso dsTemp.Tables(dal.Table_LINEITEM).Rows.Count > 0) _
                                    OrElse _
                                    (Not dsTemp.Tables(dal.Table_AP_LINEITEM) Is Nothing AndAlso dsTemp.Tables(dal.Table_AP_LINEITEM).Rows.Count > 0)) Then

                                    If ds.Tables(dal.TABLE_POSTINGPARAMETERS) Is Nothing Then
                                        ds.Tables.Add(dal.GetJournalPostingParams(oFelitaEngineData.AccountingCompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())), GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))).Tables(0).Copy)
                                    Else
                                        ds.Tables(dal.TABLE_POSTINGPARAMETERS).Merge(dal.GetJournalPostingParams(oFelitaEngineData.AccountingCompanyId, GuidControl.ByteArrayToGuid(CType(dvItem(LookupListNew.COL_ID_NAME), Byte())), GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))).Tables(0).Copy)
                                    End If

                                    If ds.Relations(dal.REL_JOURNAL_TYPE) Is Nothing AndAlso ds.Tables(dal.Table_LINEITEM) IsNot Nothing Then
                                        ds.Relations.Add(New DataRelation(dal.REL_JOURNAL_TYPE, ds.Tables(dal.TABLE_POSTINGPARAMETERS).Columns(COL_EVENT_TYPE), ds.Tables(dal.Table_LINEITEM).Columns(COL_EVENT_TYPE)))
                                    End If
                                End If
                            End If
                        Next

                    ElseIf Not oFelitaEngineData.EventId = Guid.Empty Then

                        Dim dsTemp As DataSet
                        Dim eventCode As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), oFelitaEngineData.EventId)

                        dsTemp = New DataSet(dal.DatasetName)
                        dsTemp = dal.GetJournalEntries(oFelitaEngineData.CompanyId, oFelitaEngineData.EventId, _
                        GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), eventCode, ElitaPlusIdentity.Current.ActiveUser.NetworkId, False)

                        'Check if items balance before adding to our dataset.
                        isBalanced = MergeDataSets(oFelitaEngineData.CompanyId, _acctExtension, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), oFelitaEngineData.EventId), ds, dsTemp)

                        'Update the exec log table with a success or failure based on balancing
                        boAcctExecLog = New AcctExecLog(oFelitaEngineData.CompanyId, oFelitaEngineData.EventId)
                        boAcctExecLog.UpdateExecStatus(isBalanced)


                        'ALR - added if we are smartstream AND this is claims or refunds
                        If isBalanced AndAlso _
                            _acctExtension = FelitaEngine.SMARTSTREAM_PREFIX AndAlso _
                            (eventCode.Equals(EVENT_REFUNDS) OrElse _
                             eventCode.Equals(EVENT_CLAIMS)) Then

                            Dim dsTempAP As New DataSet

                            dsTempAP = dal.GetJournalEntriesAP(oFelitaEngineData.CompanyId, oFelitaEngineData.EventId, _
                            GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), eventCode)

                            If Not dsTempAP Is Nothing AndAlso dsTempAP.Tables.Count > 0 AndAlso dsTempAP.Tables(0).Rows.Count > 0 Then
                                If ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM) Is Nothing Then
                                    ds.Tables.Add(dsTempAP.Tables(0).Copy)
                                Else
                                    ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).Merge(dsTempAP.Tables(0))
                                End If

                                If ds.Tables(AcctTransLogDAL.Table_AP_VENDORS) IsNot Nothing Then
                                    ds.Tables.Remove(AcctTransLogDAL.Table_AP_VENDORS)
                                End If
                                ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).DefaultView.RowFilter = "PAYMENT_TO_CUSTOMER = 'Y'"
                                If ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).DefaultView.Count > 0 Then
                                    ds.Tables.Add(BuildVendorTable(ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM)))
                                End If

                            End If


                            Dim dsTempPurge As DataSet
                            'Get Records to purge
                            dsTempPurge = dal.GetPurgeTable(oFelitaEngineData.CompanyId, oFelitaEngineData.EventId, _
                                GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())), eventCode)

                            If dsTempPurge IsNot Nothing AndAlso dsTempPurge.Tables(dal.Table_AP_PURGE) IsNot Nothing AndAlso dsTempPurge.Tables(dal.Table_AP_PURGE).Rows.Count > 0 Then
                                If ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                                    ds.Tables.Add(dsTempPurge.Tables(AcctTransLogDAL.Table_AP_PURGE).Copy)
                                Else
                                    ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Merge(dsTempPurge.Tables(AcctTransLogDAL.Table_AP_PURGE))
                                End If
                            End If

                        End If

                        If ((Not dsTemp.Tables(dal.Table_LINEITEM) Is Nothing AndAlso dsTemp.Tables(dal.Table_LINEITEM).Rows.Count > 0) _
                                   OrElse _
                                   (Not dsTemp.Tables(dal.Table_AP_LINEITEM) Is Nothing AndAlso dsTemp.Tables(dal.Table_AP_LINEITEM).Rows.Count > 0)) Then

                            If ds.Tables(dal.TABLE_POSTINGPARAMETERS) Is Nothing Then
                                ds.Tables.Add(dal.GetJournalPostingParams(oFelitaEngineData.AccountingCompanyId, oFelitaEngineData.EventId, GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))).Tables(0).Copy)
                            Else
                                ds.Tables(dal.TABLE_POSTINGPARAMETERS).Merge(dal.GetJournalPostingParams(oFelitaEngineData.AccountingCompanyId, oFelitaEngineData.EventId, GuidControl.ByteArrayToGuid(CType(dvRow(BusinessUnitDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))).Tables(0).Copy)
                            End If

                            If ds.Relations(dal.REL_JOURNAL_TYPE) Is Nothing AndAlso ds.Tables(dal.Table_LINEITEM) IsNot Nothing Then
                                ds.Relations.Add(New DataRelation(dal.REL_JOURNAL_TYPE, ds.Tables(dal.TABLE_POSTINGPARAMETERS).Columns(COL_EVENT_TYPE), ds.Tables(dal.Table_LINEITEM).Columns(COL_EVENT_TYPE)))
                            End If
                        End If

                    End If

                    If isDSValid(ds) Then
                        ds.Tables.Add(dal.GetJournalHeader(oFelitaEngineData.AccountingCompanyId, dvRow(BusinessUnitDV.COL_BUSINESS_UNIT)).Tables(0).Copy)
                    End If

                    AddLineNumbers(_acctExtension, ds)
                    dsSet.Add(ds)

                Next
            Else
                Return Nothing
            End If

            Return dsSet.ToArray(Dataset.GetType)

        Catch ex As Exception
            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "GetAccountingInterfaceTables Error", ex.Message + ex.StackTrace)
            Throw ex
        End Try

    End Function

    'Function to determine whether or not the dataset contains valid tables that will require we gather the header information
    Public Shared Function isDSValid(ByVal ds As DataSet, Optional ByVal CheckVendors As Boolean = True) As Boolean

        If (Not ds.Tables(AcctTransLogDAL.Table_LINEITEM) Is Nothing AndAlso ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Count > 0) _
         OrElse (Not ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM) Is Nothing AndAlso ds.Tables(AcctTransLogDAL.Table_AP_LINEITEM).Rows.Count > 0) Then
            Return True
        End If

        If CheckVendors AndAlso Not ds.Tables(AcctTransLogDAL.Table_VENDOR) Is Nothing AndAlso ds.Tables(AcctTransLogDAL.Table_VENDOR).Rows.Count > 0 Then
            Return True
        End If

        For Each dt As DataTable In ds.Tables
            If dt.TableName.Contains(AcctTransLogDAL.Table_LINEITEM) AndAlso dt.Rows.Count > 0 Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Shared Function PurgeTransLog(ByVal ds As DataSet, ByVal PurgeVendors As Boolean)

        Dim tbl As DataTable
        Dim arrIDs As New ArrayList
        Dim dal As New AcctTransLogDAL

        Try
            'Loop through each table in the dataset and determine if it has an Acct_Trans_log_id column.  
            '  If so, loop through the rows and add the IDs to the arraylist
            For Each tbl In ds.Tables
                If PurgeVendors = True Or Not tbl.TableName = AcctTransLogDAL.Table_VENDOR Then
                    If tbl.Columns.Contains(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID) Then
                        For Each dr As DataRow In tbl.Rows
                            If Not IsDBNull(dr(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID)) Then arrIDs.Add(dr(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID))
                        Next
                    End If
                End If
            Next

            If arrIDs.Count > 0 Then
                dal.PurgeTransLog(arrIDs)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try


    End Function

    Private Function IsJournalBalanced(ByVal ds As DataSet) As Boolean


        Dim crAmount, drAmount As Long

        If Not ds.Tables(AcctTransLogDAL.Table_LINEITEM).Columns.Contains(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT) Then
            ds.Tables(AcctTransLogDAL.Table_LINEITEM).Columns.Add(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT, GetType(Decimal), "transactionAmount")
        End If

        If ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("Count(DebitCredit)", "DebitCredit = 'C'") Is DBNull.Value OrElse ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("Count(DebitCredit)", "DebitCredit = 'C'") = 0 Then
            crAmount = 0
        Else
            crAmount = Math.Round(CType(ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", "DebitCredit = 'C'"), Decimal), 2)
        End If

        If ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("Count(DebitCredit)", "DebitCredit = 'D'") Is DBNull.Value OrElse ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("Count(DebitCredit)", "DebitCredit = 'D'") = 0 Then
            drAmount = 0
        Else
            drAmount = Math.Round(CType(ds.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", "DebitCredit = 'D'"), Decimal), 2)
        End If

        If crAmount <> drAmount Then
            Return False
        End If

        Return True

    End Function

    Public Shared Sub PopulateAccountingEvents(ByVal CompanyCode As String, ByVal AcctEventId As Guid, ByVal IncludeVendors As Boolean)

        Dim _dal As New AcctTransLogDAL

        Try
            Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)

            If AcctEventId = Guid.Empty Then
                For Each dvItem As System.Data.DataRowView In dv
                    If Not dvItem(LookupListNew.COL_CODE_NAME).ToString.Equals(VENDOR_EVENT) Then
                        _dal.PopulateAccountingEvents(dvItem(LookupListNew.COL_CODE_NAME).ToString, CompanyCode)
                    End If
                Next
            Else
                _dal.PopulateAccountingEvents(LookupListNew.GetCodeFromId(dv, AcctEventId), CompanyCode)
            End If

            If IncludeVendors Then
                _dal.PopulateAccountingEvents(VENDOR_EVENT, CompanyCode)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Sub

    Private Function MergeDataSets(ByVal CompanyId As Guid, ByVal ext As String, ByVal eventType As String, ByVal ds As DataSet, ByVal newDs As DataSet) As Boolean

        Dim isBalanced As Boolean = True
        Dim dtTemp As DataTable
        Dim dtLineItem, dtTempLine As DataTable
        Dim dtString As String
        Dim lsCol As Generic.List(Of DataColumn)

        'Need to create grouped tables.
        Dim dsHelp As New DALObjects.DSHelper

        If ext = FelitaEngine.SMARTSTREAM_PREFIX Then

            'SmartStream GL
            If eventType <> "CLAIM" And eventType <> "REFUNDS" Then

                isBalanced = IsJournalBalanced(newDs)
                If isBalanced Then

                    'Create Line table (GL Grouped by dealer)
                    dtTempLine = newDs.Tables(0).Copy
                    If dtTempLine.Rows.Count > 0 Then

                        'Create a purge table since we are omiting the translogId from the lines
                        dtTemp = dtTempLine.Copy
                        dtTemp.TableName = AcctTransLogDAL.Table_AP_PURGE
                        If ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                            ds.Tables.Add(dtTemp.Copy)
                        Else
                            ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Merge(dtTemp)
                        End If
                        If Not ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                            lsCol = New Generic.List(Of DataColumn)
                            For Each col As DataColumn In ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Columns
                                If Not col.ColumnName.ToLower.Equals(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID.ToLower) Then
                                    lsCol.Add(col)
                                End If
                            Next
                            If Not lsCol.Count = 0 Then
                                For Each col As DataColumn In lsCol
                                    ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Columns.Remove(col)
                                Next
                            End If
                        End If


                        dtTemp = dsHelp.GroupBySelectandInsert(AcctTransLogDAL.Table_LINEITEM, _
                                                               dtTempLine, _
                                                               ("SUM(TRANSACTIONAMOUNT) TRANSACTIONAMOUNT,JOURNALTYPE,ACCOUNTINGPERIOD,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,PLACEHOLDER JOURNALSEQUENCE,ACCOUNTCODE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10,BLOCK_OF_BUSINESS TRANSACTIONREFERENCE,DESCRIPTION,DEBITCREDIT,CURRENCYCODE,REV,GENERALDESCRIPTION24,COMPANY_CODE,INVOICE_REGION,ORIGINALACCOUNTCODE,TAXOVERRIDECODE,ACCT_COMPANY_CODE"), _
                                                               "", _
                                                               "JOURNALTYPE,ACCOUNTINGPERIOD,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,PLACEHOLDER,ACCOUNTCODE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10,BLOCK_OF_BUSINESS,DESCRIPTION,DEBITCREDIT,CURRENCYCODE,REV,GENERALDESCRIPTION24,COMPANY_CODE,INVOICE_REGION,ORIGINALACCOUNTCODE,TAXOVERRIDECODE,ACCT_COMPANY_CODE")

                    Else
                        dtTemp = dtTempLine.Copy
                    End If

                    If ds.Tables(AcctTransLogDAL.Table_LINEITEM) Is Nothing Then
                        ds.Tables.Add(dtTemp.Copy)
                    Else
                        ds.Tables(AcctTransLogDAL.Table_LINEITEM).Merge(dtTemp)
                    End If


                    'Create Control Group table for SmartStream
                    dtTemp = Nothing
                    dtTemp = dsHelp.GroupBySelectandInsert(AcctTransLogDAL.Table_CONTROL_GROUP, _
                                                              dtTempLine, _
                                                              String.Format("SUM(TRANSACTIONAMOUNT) TRANSACTIONAMOUNT,JOURNALTYPE,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,COMPANY_CODE,DEBITCREDIT,CREATED_DATE CURRENTDATE "), _
                                                              "", _
                                                             String.Format("JOURNALTYPE,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,COMPANY_CODE,DEBITCREDIT,CREATED_DATE"))

                    If ds.Tables(AcctTransLogDAL.Table_CONTROL_GROUP) Is Nothing Then
                        ds.Tables.Add(dtTemp.Copy)
                    Else
                        ds.Tables(AcctTransLogDAL.Table_CONTROL_GROUP).Merge(dtTemp)
                    End If

                Else
                    dtTemp = New DataTable()
                    dtTemp = newDs.Tables(0).Copy
                    dtTemp.TableName = AcctTransLogDAL.Table_LINEITEM + "_" + eventType
                    ds.Tables.Add(dtTemp)
                End If

                'SmartStream AP
            Else
                If newDs.Tables(0).Rows.Count > 0 Then

                    'Create Line table (GL Grouped by dealer)
                    dtTempLine = newDs.Tables(0).Copy
                    dtTemp = dsHelp.GroupBySelectandInsert(AcctTransLogDAL.Table_LINEITEM, _
                                                               dtTempLine, _
                                                               ("SUM(ORIG_TRANSACTIONAMOUNT) TRANSACTIONAMOUNT,JOURNALTYPE,ACCOUNTINGPERIOD,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,PLACEHOLDER JOURNALSEQUENCE,ACCOUNTCODE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10,BLOCK_OF_BUSINESS TRANSACTIONREFERENCE,DESCRIPTION,ORIG_DEBIT_CREDIT DEBITCREDIT,CURRENCYCODE,REV,GENERALDESCRIPTION24,COMPANY_CODE,INVOICE_REGION,ORIGINALACCOUNTCODE,TAXOVERRIDECODE"), _
                                                               String.Format("{0} = '{1}'", AcctTransLogDAL.COL_NAME_TRANSACTION_ID_NUMBER, YES_STRING), _
                                                               "JOURNALTYPE,ACCOUNTINGPERIOD,JOURNAL_ID_SUFFIX,TRANSACTIONDATE,PLACEHOLDER,ACCOUNTCODE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10,BLOCK_OF_BUSINESS,DESCRIPTION,ORIG_DEBIT_CREDIT,CURRENCYCODE,REV,GENERALDESCRIPTION24,COMPANY_CODE,INVOICE_REGION,ORIGINALACCOUNTCODE,TAXOVERRIDECODE")

                    If ds.Tables(AcctTransLogDAL.Table_LINEITEM) Is Nothing Then
                        ds.Tables.Add(dtTemp.Copy)
                    Else
                        ds.Tables(AcctTransLogDAL.Table_LINEITEM).Merge(dtTemp)
                    End If


                End If
            End If


        Else   'Felita

            isBalanced = IsJournalBalanced(newDs)

            'Logic to Summarize Claims by Invoice
            If eventType = "CLAIM" Then
                dtTempLine = newDs.Tables(0).Copy

                If dtTempLine.Rows.Count > 0 Then

                    'Create a purge table since we are omiting the translogId from the lines
                    dtTemp = dtTempLine.Copy
                    dtTemp.TableName = AcctTransLogDAL.Table_AP_PURGE
                    If ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                        ds.Tables.Add(dtTemp.Copy)
                    Else
                        ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Merge(dtTemp)
                    End If
                    If Not ds.Tables(AcctTransLogDAL.Table_AP_PURGE) Is Nothing Then
                        lsCol = New Generic.List(Of DataColumn)
                        For Each col As DataColumn In ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Columns
                            If Not col.ColumnName.ToLower.Equals(AcctTransLogDAL.COL_NAME_ACCT_TRANS_LOG_ID.ToLower) Then
                                lsCol.Add(col)
                            End If
                        Next
                        If Not lsCol.Count = 0 Then
                            For Each col As DataColumn In lsCol
                                ds.Tables(AcctTransLogDAL.Table_AP_PURGE).Columns.Remove(col)
                            Next
                        End If
                    End If

                    dtTemp = dsHelp.GroupBySelectandInsert(AcctTransLogDAL.Table_LINEITEM, _
                                                                   dtTempLine, _
                                                                   "SUM(TRANSACTIONAMOUNT) TRANSACTIONAMOUNT,ACCOUNTCODE,ACCOUNTINGPERIOD,ALLOCATIONMARKER,CURRENCYCODE,DEBITCREDIT,DESCRIPTION,JOURNALSOURCE,JOURNALTYPE,TRANSACTIONAMOUNTDECIMALPLACES,TRANSACTIONDATE,TRANSACTIONREFERENCE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10, GENERALDESCRIPTION1,GENERALDESCRIPTION2,GENERALDESCRIPTION3,GENERALDESCRIPTION4,GENERALDESCRIPTION5,GENERALDESCRIPTION6,GENERALDESCRIPTION7,GENERALDESCRIPTION8,GENERALDESCRIPTION9,GENERALDESCRIPTION10,GENERALDESCRIPTION12,GENERALDESCRIPTION13,GENERALDESCRIPTION14,GENERALDESCRIPTION24,GENERALDESCRIPTION25,PLACEHOLDER,REV,PAYMENT_TO_CUSTOMER,DEALER_CODE,COMPANY_CODE,INVOICE_REGION,BANK_ACCOUNT_NUMBER,BANK_ID,USE_PAYEE_SETTINGS,ACCT_COMPANY_CODE,ACCT_EVENT_MAPPING,ORIGINALACCOUNTCODE,TAXOVERRIDECODE,INVOICE_DATE,BRANCH_NAME,BANK_NAME,BANK_SORT_CODE,BANK_LOOKUP_CODE,BANK_SUB_CODE,IBAN_NUMBER,ACCOUNT_NAME,PAYMENT_METHOD", _
                                                                    "", _
                                                                    "ACCOUNTCODE,ACCOUNTINGPERIOD,ALLOCATIONMARKER,CURRENCYCODE,DEBITCREDIT,DESCRIPTION,JOURNALSOURCE,JOURNALTYPE,TRANSACTIONAMOUNTDECIMALPLACES,TRANSACTIONDATE,TRANSACTIONREFERENCE,ANALYSISCODE1,ANALYSISCODE2,ANALYSISCODE3,ANALYSISCODE4,ANALYSISCODE5,ANALYSISCODE6,ANALYSISCODE7,ANALYSISCODE8,ANALYSISCODE9,ANALYSISCODE10, GENERALDESCRIPTION1,GENERALDESCRIPTION2,GENERALDESCRIPTION3,GENERALDESCRIPTION4,GENERALDESCRIPTION5,GENERALDESCRIPTION6,GENERALDESCRIPTION7,GENERALDESCRIPTION8,GENERALDESCRIPTION9,GENERALDESCRIPTION10,GENERALDESCRIPTION12,GENERALDESCRIPTION13,GENERALDESCRIPTION14,GENERALDESCRIPTION24,GENERALDESCRIPTION25,PLACEHOLDER,REV,PAYMENT_TO_CUSTOMER,DEALER_CODE,COMPANY_CODE,INVOICE_REGION,BANK_ACCOUNT_NUMBER,BANK_ID,USE_PAYEE_SETTINGS,ACCT_COMPANY_CODE,ACCT_EVENT_MAPPING,ORIGINALACCOUNTCODE,TAXOVERRIDECODE,INVOICE_DATE,BRANCH_NAME,BANK_NAME,BANK_SORT_CODE,BANK_LOOKUP_CODE,BANK_SUB_CODE,IBAN_NUMBER,ACCOUNT_NAME,PAYMENT_METHOD")


                End If

            Else
                dtTemp = newDs.Tables(0).Copy
            End If

            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then

                If ds.Tables(AcctTransLogDAL.Table_LINEITEM) Is Nothing Then
                    ds.Tables.Add(dtTemp.Copy)
                Else
                    ds.Tables(AcctTransLogDAL.Table_LINEITEM).Merge(dtTemp)
                End If

                'If isBalanced Then
                '    If ds.Tables(AcctTransLogDAL.Table_LINEITEM) Is Nothing Then
                '        ds.Tables.Add(dtTemp.Copy)
                '    Else
                '        ds.Tables(AcctTransLogDAL.Table_LINEITEM).Merge(dtTemp)
                '    End If
                'Else
                '    dtTemp.TableName = AcctTransLogDAL.Table_LINEITEM + "_" + eventType
                '    ds.Tables.Add(dtTemp)
                'End If
            End If

        End If

        Return isBalanced

    End Function

    Public Function BuildVendorTable(ByVal dt As DataTable) As DataTable

        Dim dtTemp As DataTable
        Dim dsHelp As New DSHelper

        dtTemp = dsHelp.GroupBySelectandInsert(AcctTransLogDAL.Table_AP_VENDORS, _
                                               dt, _
                                               ("VENDOR_ID,GENERALDESCRIPTION9 PAYEE,GENERALDESCRIPTION1,GENERALDESCRIPTION2,CITY,REGION,ZIP,COUNTRY,GENERALDESCRIPTION24,PAYMENT_TO_CUSTOMER,PAYMENT_METHOD,CURRENCYCODE,COMPANY_CODE"), _
                                               "", _
                                               "VENDOR_ID,GENERALDESCRIPTION9,GENERALDESCRIPTION1,GENERALDESCRIPTION2,CITY,REGION,ZIP,COUNTRY,GENERALDESCRIPTION24,PAYMENT_TO_CUSTOMER,PAYMENT_METHOD,CURRENCYCODE,COMPANY_CODE")
        Return dtTemp

    End Function

    Public Sub AddLineNumbers(ByVal ext As String, ByVal ds As DataSet)

        If ext = FelitaEngine.SMARTSTREAM_PREFIX Then

            Dim x As Integer = 1

            For Each dtTemp As DataTable In ds.Tables

                If dtTemp.TableName = AcctTransLogDAL.Table_LINEITEM Or dtTemp.TableName = AcctTransLogDAL.Table_AP_LINEITEM Then

                    If dtTemp.Columns("LINENUM") Is Nothing Then
                        dtTemp.Columns.Add(New DataColumn("LINENUM", GetType(String)))
                    End If

                    For i As Integer = 0 To dtTemp.Rows.Count - 1
                        If CType(dtTemp.Rows(i)("TRANSACTIONAMOUNT"), Double) <> 0 Then
                            dtTemp.Rows(i)("LINENUM") = x.ToString
                            x += 1
                        End If
                    Next
                End If

                'Reset count between tables
                x = 1
            Next
        End If

    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


