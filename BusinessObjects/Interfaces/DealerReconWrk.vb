'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/13/2004)  ********************
Imports System.Text
Public Class DealerReconWrk
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_NAME_DEALER_RECON_WRK_ID As String = "dealer_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_CERTIFICATE_LOADED As String = "certificate_loaded"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_ITEM As String = "item"
    Public Const COL_NAME_SKU_NUMBER As String = "Sku_Number"
    Public Const COL_NAME_PRODUCT_PRICE As String = "product_price"
    Public Const COL_NAME_MAN_WARRANTY As String = "man_warranty"
    Public Const COL_NAME_EXT_WARRANTY As String = "ext_warranty"
    Public Const COL_NAME_PRICE_POL As String = "price_pol"
    Public Const COL_NAME_SR As String = "sr"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_NEW_BRANCH_CODE As String = "new_branch_code"
    Public Const COL_NAME_NUMBER_COMP As String = "number_comp"
    Public Const COL_NAME_DATE_COMP As String = "date_comp"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const COL_NAME_SALUTATION As String = "salutation"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_LANGUAGE_PREF As String = "language_pref"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_ZIP As String = "zip"
    Public Const COL_NAME_STATE_PROVINCE As String = "state_province"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_CURRENCY As String = "currency"
    Public Const COL_NAME_EXTWARR_SALEDATE As String = "extwarr_saledate"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_TYPE_PAYMENT As String = "type_payment"
    Public Const COL_NAME_NUMBER_MONTHLY As String = "number_monthly"
    Public Const COL_NAME_MONTHLY_PAYMENT As String = "monthly_payment"
    Public Const COL_NAME_COEF_FIN As String = "coef_fin"
    Public Const COL_NAME_CURRENCY_PAYMENT As String = "currency_payment"
    Public Const COL_NAME_FINANCED_VALUE As String = "financed_value"
    Public Const COL_NAME_CREDIT_CARD_TYPE As String = "credit_card_type"
    Public Const COL_NAME_CREDIT_CARD As String = "credit_card"
    Public Const COL_NAME_CREDIT_AUTHORIZATION_NUMBER As String = "credit_authorization_number"
    Public Const COL_NAME_CANCELLATION_CODE As String = "cancellation_code"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_NEW_PRODUCT_CODE As String = "new_product_code"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_RG_NUMBER As String = "RG_number"
    Public Const COL_NAME_ID_TYPE As String = "ID_type"
    Public Const COL_NAME_MULTIPLE_DURATIONS As String = "multiple_durations"

    Public Const COL_NAME_SALES_TAX As String = "sales_tax"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CUST_COUNTRY As String = "cust_country"
    Public Const COL_NAME_COUNTRY_PURCH As String = "country_purch"
    Public Const COL_NAME_ISO_CODE As String = "iso_code"
    Public Const COL_NAME_BILLING_FREQUENCY As String = "billing_frequency"
    Public Const COL_NAME_NUMBER_OF_INSTALLMENTS As String = "number_of_installments"
    Public Const COL_NAME_INSTALLMENT_AMOUNT As String = "installment_amount"
    Public Const COL_NAME_BANK_RTN_NUMBER As String = "bank_rtn_number"
    Public Const COL_NAME_BANK_ACCOUNT_NUMBER As String = "bank_account_number"
    Public Const COL_NAME_BANK_BRANCH_NUMBER As String = "bank_branch_number"
    Public Const COL_NAME_BANK_ACCT_OWNER_NAME As String = "bank_acct_owner_name"
    Public Const COL_NAME_POST_PRE_PAID As String = "post_pre_paid"
    Public Const COL_NAME_DATE_PAID_FOR As String = "Date_paid_for"
    Public Const COL_NAME_MEMBERSHIP_NUMBER As String = "membership_number"
    Public Const COL_NAME_ADDRESS3 As String = "address3"
    Public Const COL_NAME_ORIGINAL_RETAIL_PRICE As String = "original_retail_price"
    Public Const COL_NAME_BILLING_PLAN As String = "billing_plan"
    Public Const COL_NAME_BILLING_CYCLE As String = "billing_cycle"
    Public Const COL_NAME_SUBSCRIBER_STATUS As String = "subscriber_status"
    Public Const COL_NAME_SUSPENDED_REASON As String = "suspended_reason"
    Public Const COL_NAME_MOBILE_TYPE As String = "mobile_type"
    Public Const COL_NAME_FIRST_USE_DATE As String = "first_use_date"
    Public Const COL_NAME_LAST_USE_DATE As String = "last_use_date"
    Public Const COL_NAME_SIM_CARD_NUMBER As String = "sim_card_number"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_MEMBERSHIP_TYPE As String = "membership_type"
    Public Const COL_NAME_CESS_OFFICE As String = "cess_office"
    Public Const COL_NAME_CESS_SALESREP As String = "cess_salesrep"
    Public Const COL_NAME_BUSINESSLINE As String = "businessline"
    Public Const COL_NAME_SALES_DEPARTMENT As String = "sales_department"
    Public Const COL_NAME_LINKED_CERT_NUMBER As String = "linked_cert_number"
    Public Const COL_NAME_ADDITIONAL_INFO As String = "additional_info"
    Public Const COL_CREDITCARD_LAST_FOUR_DIGIT As String = "creditcard_last_four_digit" 'REQ-1169

    Public Const COL_NAME_FINANCED_AMOUNT As String = "financed_amount"
    Public Const COL_NAME_FINANCED_FREQUENCY As String = "financed_frequency"
    Public Const COL_NAME_FINANCED_INSTALLMENT_NUMBER As String = "financed_installment_number"
    Public Const COL_NAME_FINANCED_INSTALLMENT_AMOUNT As String = "financed_installment_amount"
    Public Const COL_NAME_GENDER As String = "Gender"
    Public Const COL_NAME_MARITAL_STATUS As String = "Marital_Status"
    Public Const COL_NAME_NATIONALITY As String = "Nationality"
    Public Const COL_NAME_BIRTH_DATE As String = "Birth_Date"

    Public Const COL_NAME_PLACE_OF_BIRTH As String = "place_of_birth"
    Public Const COL_NAME_PERSON_TYPE As String = "person_type"
    Public Const COL_NAME_CUIT_CUIL As String = "cuit_cuil"
    Public Const COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS As String = "num_of_consecutive_payments"

    Public Const COL_NAME_DEALER_CURRENT_PLAN_CODE As String = "dealer_current_plan_code"
    Public Const COL_NAME_DEALER_SCHEDULED_PLAN_CODE As String = "dealer_scheduled_plan_code"
    Public Const COL_NAME_DEALER_UPDATE_REASON As String = "dealer_update_reason"

    Public Const COL_NAME_UPGRADE_DATE As String = "upgrade_date"
    Public Const COL_NAME_VOUCHER_NUMBER As String = "voucher_number"
    Public Const COL_NAME_OUTSTANDING_BALANCE_AMOUNT As String = "outstanding_balance_amount"
    Public Const COL_NAME_OUTSTANDING_BALANCE_DUE_DATE As String = "outstanding_balance_due_date"
    Public Const COL_NAME_REFUND_AMOUNT As String = "refund_amount"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"

    Public Const COL_NAME_RMA As String = "RMA"
    Public Const COL_NAME_APPLECARE_FEE As String = "AppleCare_Fee"
    Public Const COL_NAME_OCCUPATION As String = "Occupation"

    'REQ-5657 - Start
    Public Const COL_NAME_FINANCED_DATE As String = "finance_date"
    Public Const COL_NAME_DOWN_PAYMENT As String = "down_payment"
    Public Const COL_NAME_ADVANCE_PAYMENT As String = "advance_payment"
    Public Const COL_NAME_UPGRADE_TERM As String = "upgrade_term"
    Public Const COL_NAME_BILLING_ACCOUNT_NUMBER As String = "billing_account_number"
    'REQ-5657 - End

    Public Const COL_NAME_LOAN_CODE As String = "loan_code"
    Public Const COL_NAME_PAYMENT_SHIFT_NUMBER As String = "payment_shift_number"
    Public Const COL_NAME_IS_RECON_RECORD_PARENT As String = "is_recon_record_parent"

    Public Const COL_NAME_SERVICE_LINE_NUMBER As String = "service_line_number"
    Public Const RECORD_TYPE_FC As String = "FC"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        VerifyConcurrency(sModifiedDate)
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

    Protected Sub Load()
        Try
            Dim dal As New DealerReconWrkDAL
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
            Dim dal As New DealerReconWrkDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(DealerReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerReconWrkDAL.COL_NAME_DEALER_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DealerfileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=3)>
    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property SkuNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SKU_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SKU_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SKU_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property CertificateLoaded() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CERTIFICATE_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CERTIFICATE_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CERTIFICATE_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2)>
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property ItemCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ITEM_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property Item() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ITEM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ITEM, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property ProductPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PRODUCT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_PRODUCT_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PRODUCT_PRICE, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=99999)>
    Public Property ManWarranty() As LongType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MAN_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerReconWrkDAL.COL_NAME_MAN_WARRANTY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MAN_WARRANTY, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=99999)>
    Public Property ExtWarranty() As LongType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_EXT_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerReconWrkDAL.COL_NAME_EXT_WARRANTY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_EXT_WARRANTY, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=-999999999.99, Max:=NEW_MAX_DOUBLE)>
    Public Property PricePol() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PRICE_POL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_PRICE_POL), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PRICE_POL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property Sr() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SR, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property BranchCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BRANCH_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BRANCH_CODE, Value)
        End Set
    End Property
    'REQ-976 changed max value from 10 to 40
    <ValidStringLength("", Max:=100)>
    Public Property NewBranchCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NEW_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_NEW_BRANCH_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NEW_BRANCH_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property NumberComp() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NUMBER_COMP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_NUMBER_COMP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NUMBER_COMP, Value)
        End Set
    End Property

    Public Property DateComp() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DATE_COMP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_DATE_COMP), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DATE_COMP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), CertNumberValidation("")>
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property IdentificationNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property LanguagePref() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_LANGUAGE_PREF) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_LANGUAGE_PREF), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_LANGUAGE_PREF, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)>
    Public Property Zip() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ZIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ZIP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ZIP, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property StateProvince() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_STATE_PROVINCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_STATE_PROVINCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_STATE_PROVINCE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property HomePhone() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property WorkPhone() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_WORK_PHONE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=80)>
    Public Property Occupation() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_OCCUPATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_OCCUPATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_OCCUPATION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property Currency() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CURRENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CURRENCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CURRENCY, Value)
        End Set
    End Property

    Public Property ExtwarrSaledate() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_EXTWARR_SALEDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_EXTWARR_SALEDATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_EXTWARR_SALEDATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property TypePayment() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_TYPE_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_TYPE_PAYMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_TYPE_PAYMENT, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=99999)>
    Public Property NumberMonthly() As LongType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NUMBER_MONTHLY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerReconWrkDAL.COL_NAME_NUMBER_MONTHLY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NUMBER_MONTHLY, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property MonthlyPayment() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MONTHLY_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_MONTHLY_PAYMENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MONTHLY_PAYMENT, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=999.9999)>
    Public Property CoefFin() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_COEF_FIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_COEF_FIN), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_COEF_FIN, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property CurrencyPayment() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CURRENCY_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CURRENCY_PAYMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CURRENCY_PAYMENT, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property FinancedValue() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCED_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_FINANCED_VALUE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCED_VALUE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2)>
    Public Property CreditCardType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)>
    Public Property CreditCard() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CREDIT_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CREDIT_CARD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CREDIT_CARD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property CreditAuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CREDIT_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CREDIT_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CREDIT_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property CancellationCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CANCELLATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CANCELLATION_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CANCELLATION_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property Manufacturer() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property IMEINumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_IMEI_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_IMEI_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property Layout() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property NewProductCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4)>
    Public Property DocumentType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property DocumentAgency() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_AGENCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DOCUMENT_AGENCY, Value)
        End Set
    End Property
    Public Property DocumentIssueDate() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property RGNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_RG_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_RG_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_RG_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property IDType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ID_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ID_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property MultipleDurations() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MULTIPLE_DURATIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MULTIPLE_DURATIONS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MULTIPLE_DURATIONS, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property SalesTax() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SALES_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_SALES_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SALES_TAX, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property Address3() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ADDRESS3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ADDRESS3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ADDRESS3, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)>
    Public Property Email() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2)>
    Public Property CustCountry() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CUST_COUNTRY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CUST_COUNTRY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CUST_COUNTRY, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2)>
    Public Property CountryPurch() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_COUNTRY_PURCH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_COUNTRY_PURCH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_COUNTRY_PURCH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=3)>
    Public Property IsoCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ISO_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ISO_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ISO_CODE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)>
    Public Property BillingFrequency() As LongType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BILLING_FREQUENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerReconWrkDAL.COL_NAME_BILLING_FREQUENCY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BILLING_FREQUENCY, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)>
    Public Property NumberOfInstallments() As LongType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NUMBER_OF_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerReconWrkDAL.COL_NAME_NUMBER_OF_INSTALLMENTS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NUMBER_OF_INSTALLMENTS, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property InstallmentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999999999)>
    Public Property BankRtnNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BANK_RTN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BANK_RTN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BANK_RTN_NUMBER, Value)
        End Set
    End Property

    <ValidAcctNumber(""), ValidStringLength("", Max:=29)>
    Public Property BankAccountNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BANK_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BANK_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BANK_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BankBranchNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BANK_BRANCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BANK_BRANCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BANK_BRANCH_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property BankAcctOwnerName() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BANK_ACCT_OWNER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BANK_ACCT_OWNER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BANK_ACCT_OWNER_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property PostPrePaid() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_POST_PRE_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_POST_PRE_PAID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_POST_PRE_PAID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property Salutation() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SALUTATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SALUTATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SALUTATION, Value)
        End Set
    End Property

    Public Property DatePaidFor() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DATE_PAID_FOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_DATE_PAID_FOR), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DATE_PAID_FOR, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property MembershipNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_NUMBER, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property OriginalRetailPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ORIGINAL_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_ORIGINAL_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ORIGINAL_RETAIL_PRICE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BillingPlan() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BILLING_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BILLING_PLAN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BILLING_PLAN, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BillingCycle() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BILLING_CYCLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BILLING_CYCLE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BILLING_CYCLE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property SubscriberStatus() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SUBSCRIBER_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SUBSCRIBER_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SUBSCRIBER_STATUS, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=250)>
    Public Property SuspendedReason() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SUSPENDED_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SUSPENDED_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SUSPENDED_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property MobileType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MOBILE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MOBILE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MOBILE_TYPE, Value)
        End Set
    End Property

    Public Property FirstUseDate() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FIRST_USE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_FIRST_USE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FIRST_USE_DATE, Value)
        End Set
    End Property

    Public Property LastUseDate() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_LAST_USE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_LAST_USE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_LAST_USE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property SimCardNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SIM_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SIM_CARD_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SIM_CARD_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=25)>
    Public Property Region() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_REGION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property MembershipType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MEMBERSHIP_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property CessOffice() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CESS_OFFICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CESS_OFFICE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CESS_OFFICE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property CessSalesrep() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CESS_SALESREP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CESS_SALESREP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CESS_SALESREP, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property Businessline() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BUSINESSLINE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BUSINESSLINE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BUSINESSLINE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property SalesDepartment() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SALES_DEPARTMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SALES_DEPARTMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SALES_DEPARTMENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property LinkedCertNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_LINKED_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_LINKED_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_LINKED_CERT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property AdditionalInfo() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ADDITIONAL_INFO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_ADDITIONAL_INFO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ADDITIONAL_INFO, Value)
        End Set
    End Property
    'REQ-1169
    ''' <summary>
    ''' Property to get and set the value for field CreditCardLast4digit.  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <ValidStringLength("", Max:=4, Min:=4)>
    Public Property CreditCardLastFourDigit() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CREDITCARD_LAST_FOUR_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CREDITCARD_LAST_FOUR_DIGIT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CREDITCARD_LAST_FOUR_DIGIT, Value)
        End Set
    End Property
    'REQ-1169

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property Financed_Amount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_FINANCED_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCED_AMOUNT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)>
    Public Property FinancedFrequency() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCED_FREQUENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_FINANCED_FREQUENCY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCED_FREQUENCY, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property FinancedInstallmentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)>
    Public Property FinancedInstallmentNumber() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_NUMBER), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCED_INSTALLMENT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property Gender() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_GENDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_GENDER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_GENDER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property Marital_Status() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_MARITAL_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_MARITAL_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_MARITAL_STATUS, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property Nationality() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NATIONALITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_NATIONALITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NATIONALITY, Value)
        End Set
    End Property

    Public Property Birth_Date() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BIRTH_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_BIRTH_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BIRTH_DATE, Value)
        End Set
    End Property
    'REQ-5657 - Start

    <ValidStringLength("", Max:=8)>
    Public Property FinanceDate() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_FINANCE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_FINANCE_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_FINANCE_DATE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property DownPayment() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DOWN_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_DOWN_PAYMENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DOWN_PAYMENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property AdvancePayment() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_ADVANCE_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_ADVANCE_PAYMENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_ADVANCE_PAYMENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property UpgradeTerm() As Nullable(Of Integer)
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_UPGRADE_TERM) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DealerReconWrkDAL.COL_NAME_UPGRADE_TERM), Integer))
            End If
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_UPGRADE_TERM, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property BillingAccountNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_BILLING_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_BILLING_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_BILLING_ACCOUNT_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property dealer_current_plan_code() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DEALER_CURRENT_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DEALER_CURRENT_PLAN_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DEALER_CURRENT_PLAN_CODE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property dealer_scheduled_plan_code() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DEALER_SCHEDULED_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DEALER_SCHEDULED_PLAN_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DEALER_SCHEDULED_PLAN_CODE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property dealer_update_reason() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DEALER_UPDATE_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DEALER_UPDATE_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DEALER_UPDATE_REASON, Value)
        End Set
    End Property
    'REQ-5657 - End
    'REQ-5681 Begin
    <ValidStringLength("", Max:=2)>
    Public Property Place_Of_Birth() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PLACE_OF_BIRTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_PLACE_OF_BIRTH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PLACE_OF_BIRTH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property Person_Type() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PERSON_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_PERSON_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PERSON_TYPE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property Num_Of_Consecutive_Payments() As Nullable(Of Integer)
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS), Integer)
            End If
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property CUIT_CUIL() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_CUIT_CUIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_CUIT_CUIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_CUIT_CUIL, Value)
        End Set
    End Property
    'REQ-5681 End

    <ValidStringLength("", Max:=50)>
    Public Property ServiceLineNumber() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property LoanCode() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_LOAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_LOAN_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_LOAN_CODE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property PaymentShiftNumber() As Nullable(Of Integer)
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_PAYMENT_SHIFT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_PAYMENT_SHIFT_NUMBER), Integer)
            End If
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_PAYMENT_SHIFT_NUMBER, Value)
        End Set
    End Property

    Public Property Upgrade_Date() As DateType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_UPGRADE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerReconWrkDAL.COL_NAME_UPGRADE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_UPGRADE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property Voucher_Number() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_VOUCHER_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_VOUCHER_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_VOUCHER_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property RMA() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_RMA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_RMA), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_RMA, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property RefundAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_REFUND_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_REFUND_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_REFUND_AMOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property DeviceType() As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_DEVICE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReconWrkDAL.COL_NAME_DEVICE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_DEVICE_TYPE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property AppleCareFee() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_APPLECARE_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerReconWrkDAL.COL_NAME_APPLECARE_FEE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(DealerReconWrkDAL.COL_NAME_APPLECARE_FEE, Value)
        End Set
    End Property
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAcctNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_ACCOUNT_NUMBER)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DealerReconWrk = CType(objectToValidate, DealerReconWrk)
            Dim i As Integer

            If obj.BankAccountNumber Is Nothing Then
                Return True
            End If

            If obj.BankAccountNumber.ToString.Trim.Length > 29 Then
                MyBase.Message = Common.ErrorCodes.INVALID_ACCOUNT_NUMBER
                Return False
            End If

            For i = 0 To obj.BankAccountNumber.Length - 1
                If Not IsNumeric(obj.BankAccountNumber.Chars(i)) Then
                    MyBase.Message = Common.ErrorCodes.INVALID_ACCOUNT_NUMBER
                    Return False
                End If
            Next

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CertNumberValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CERT_NUMBER_FORMAT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DealerReconWrk = CType(objectToValidate, DealerReconWrk)

            If obj.Certificate Is String.Empty OrElse obj.Certificate Is Nothing Then
                Return True
            End If
            Dim oCompany As New Company(obj.CompanyId(obj.DealerfileProcessedId))
            If (Not oCompany.CertNumberFormatId.Equals(Guid.Empty)) AndAlso (obj.RecordType = RECORD_TYPE_FC) Then
                Dim objRegEx As New System.Text.RegularExpressions.Regex(LookupListNew.getCertNumberFormatDescription(oCompany.CertNumberFormatId), RegularExpressions.RegexOptions.IgnoreCase Or RegularExpressions.RegexOptions.CultureInvariant)
                Dim strToValidate As String = obj.Certificate.Trim(" ".ToCharArray)
                Dim blnValid As Boolean
                blnValid = objRegEx.IsMatch(strToValidate)
                Return blnValid
            Else
                Return True
            End If

        End Function
    End Class
#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(ByVal DealerfileProcessedId As Guid) As Guid
        Get
            Dim oDealerfileProcessed As New DealerFileProcessed(DealerfileProcessedId)
            Dim oDealer As New Dealer(oDealerfileProcessed.DealerId)
            Dim oCompanyId As Guid = oDealer.CompanyId
            Return oCompanyId
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Shared Sub UpdateHeaderCount(ByVal dealerFileProcessedId As Guid)
        Dim dal As New DealerReconWrkDAL
        dal.UpdateHeaderCount(dealerFileProcessedId)
    End Sub


    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerReconWrkDAL
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

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid,
                                        ByVal recordMode As String,
                                        ByVal recordType As String,
                                        ByVal rejectCode As String,
                                        ByVal rejectReason As String,
                                        ByVal parentFile As String,
                                        ByVal PageIndex As Integer,
                                        ByVal Pagesize As Integer,
                                        ByVal SortExpression As String) As DataView
        Try
            Dim dal As New DealerReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                recordMode, recordType, rejectCode, rejectReason, parentFile, PageIndex, Pagesize, SortExpression)

            Return (ds.Tables(DealerReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(DealerReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region
End Class
