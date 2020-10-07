'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/13/2011)  ********************
Imports System.Text

Public Class DealerVscReconWrk
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const COL_NAME_DEALER_VSC_RECON_WRK_ID As String = "dealer_vsc_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_ENROLLMENT_ID As String = "enrollment_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CERTIFICATE_LOADED As String = "certificate_loaded"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_CERTIFICATE_NUMBER As String = "certificate_number"
    Public Const COL_NAME_CUSTOMERS As String = "customers"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_COUNTRY_CODE As String = "country_code"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_MODEL_YEAR As String = "model_year"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_ENGINE_VERSION As String = "engine_version"
    Public Const COL_NAME_EXTERNAL_CAR_CODE As String = "external_car_code"
    Public Const COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const COL_NAME_ODOMETER As String = "odometer"
    Public Const COL_NAME_VIN As String = "vin"
    Public Const COL_NAME_PURCHASE_PRICE As String = "purchase_price"
    Public Const COL_NAME_PURCHASE_DATE As String = "purchase_date"
    Public Const COL_NAME_IN_SERVICE_DATE As String = "in_service_date"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_PLAN_CODE As String = "plan_code"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_TERM_MONTHS As String = "term_months"
    Public Const COL_NAME_TERM_KM_MI As String = "term_km_mi"
    Public Const COL_NAME_AGENT_NUMBER As String = "agent_number"
    Public Const COL_NAME_WARRANTY_SALE_DATE As String = "warranty_sale_date"
    Public Const COL_NAME_PLAN_AMOUNT As String = "plan_amount"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_IDENTITY_DOC_NO As String = "identity_doc_no"
    Public Const COL_NAME_RG_NO As String = "rg_no"
    Public Const COL_NAME_ID_TYPE As String = "id_type"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_QUOTE_NUMBER As String = "quote_number"
    Public Const COL_NAME_QUOTE_ITEM_NUMBER As String = "quote_item_number"
    Public Const COL_NAME_NEW_USED As String = "new_used"
    Public Const COL_NAME_OPTIONAL_COVERAGE As String = "optional_coverage"
    Public Const COL_NAME_BIRTH_DATE As String = "birth_date"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_PAYMENT_TYPE_CODE As String = "payment_type_code"
    Public Const COL_NAME_PAYMENT_INSTRUMENT_CODE As String = "payment_instrument_code"
    Public Const COL_NAME_INSTALLMENTS_NUMBER As String = "installments_number"
    Public Const COL_NAME_CREDIT_CARD_TYPE_CODE As String = "credit_card_type_code"
    Public Const COL_NAME_NAME_ON_CREDIT_CARD As String = "name_on_credit_card"
    Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_NAME_ON_ACCOUNT As String = "name_on_account"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_PLAN_AMOUNT_WITH_MARKUP As String = "plan_amount_with_markup"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_CANCELLATION_DATE As String = "cancellation_date"
    Public Const COL_NAME_CANCELLATION_REASON_CODE As String = "cancellation_reason_code"
    Public Const COL_NAME_CANCEL_COMMENT_TYPE_CODE As String = "cancel_comment_type_code"
    Public Const COL_NAME_CANCELLATION_COMMENT As String = "cancellation_comment"
    Public Const COL_NAME_FINANCING_AGENCY As String = "financing_agency"
    Public Const COL_NAME_NC_PAYMENT_METHOD_CODE As String = "nc_payment_method_code"
    Public Const COL_NAME_ACCOUNT_TYPE_CODE As String = "account_type_code"
    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_BRANCH_DIGIT As String = "branch_digit"
    Public Const COL_NAME_ACCOUNT_DIGIT As String = "account_digit"
    Public Const COL_NAME_REFUND_AMOUNT As String = "refund_amount"
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

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DealerVscReconWrkDAL
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
            Dim dal As New DealerVscReconWrkDAL
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
            If Row(DealerVscReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerVscReconWrkDAL.COL_NAME_DEALER_VSC_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerfileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerVscReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property



    Public Property EnrollmentId As Guid
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ENROLLMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerVscReconWrkDAL.COL_NAME_ENROLLMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ENROLLMENT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property RejectMsgParms As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_REJECT_MSG_PARMS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_REJECT_MSG_PARMS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property CertificateLoaded As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_LOADED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_LOADED, Value)
        End Set
    End Property


    <ValidateDealer("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerVscReconWrkDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property



    Public Property UserId As Guid
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerVscReconWrkDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=8)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property CompanyCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_COMPANY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_COMPANY_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), CertNumberValidation("")>
    Public Property CertificateNumber As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Customers As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CUSTOMERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CUSTOMERS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CUSTOMERS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Region As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_REGION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_REGION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property CountryCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_COUNTRY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_COUNTRY_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property HomePhone As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property



    Public Property ModelYear As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_MODEL_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_MODEL_YEAR), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_MODEL_YEAR, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property EngineVersion As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ENGINE_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_ENGINE_VERSION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ENGINE_VERSION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=480)> _
    Public Property VehicleLicenseTag As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_VEHICLE_LICENSE_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_VEHICLE_LICENSE_TAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_VEHICLE_LICENSE_TAG, Value)
        End Set
    End Property



    Public Property Odometer As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_ODOMETER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ODOMETER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Vin As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_VIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_VIN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_VIN, Value)
        End Set
    End Property



    Public Property PurchasePrice As DecimalType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerVscReconWrkDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property



    Public Property PurchaseDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PURCHASE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_PURCHASE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PURCHASE_DATE, Value)
        End Set
    End Property



    Public Property InServiceDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_IN_SERVICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_IN_SERVICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_IN_SERVICE_DATE, Value)
        End Set
    End Property



    Public Property DeliveryDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_DELIVERY_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DELIVERY_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property PlanCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_PLAN_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PLAN_CODE, Value)
        End Set
    End Property



    Public Property Deductible As DecimalType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerVscReconWrkDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property



    Public Property TermMonths As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_TERM_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_TERM_MONTHS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_TERM_MONTHS, Value)
        End Set
    End Property



    Public Property TermKmMi As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_TERM_KM_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_TERM_KM_MI), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_TERM_KM_MI, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property AgentNumber As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_AGENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_AGENT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_AGENT_NUMBER, Value)
        End Set
    End Property



    Public Property WarrantySaleDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_WARRANTY_SALE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_WARRANTY_SALE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_WARRANTY_SALE_DATE, Value)
        End Set
    End Property



    Public Property PlanAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property DocumentType As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property IdentityDocNo As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_IDENTITY_DOC_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_IDENTITY_DOC_NO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_IDENTITY_DOC_NO, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=32)> _
    Public Property RgNo As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_RG_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_RG_NO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_RG_NO, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property IdType As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_ID_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ID_TYPE, Value)
        End Set
    End Property



    Public Property DocumentIssueDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=240)> _
    Public Property DocumentAgency As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_AGENCY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_DOCUMENT_AGENCY, Value)
        End Set
    End Property



    Public Property QuoteNumber As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_QUOTE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_QUOTE_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_QUOTE_NUMBER, Value)
        End Set
    End Property



    Public Property QuoteItemNumber As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_QUOTE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_QUOTE_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_QUOTE_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=32)> _
    Public Property NewUsed As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_NEW_USED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_NEW_USED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_NEW_USED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property OptionalCoverage As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_OPTIONAL_COVERAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_OPTIONAL_COVERAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_OPTIONAL_COVERAGE, Value)
        End Set
    End Property



    Public Property BirthDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_BIRTH_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_BIRTH_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_BIRTH_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property WorkPhone As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_WORK_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property PaymentTypeCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PAYMENT_TYPE_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property PaymentInstrumentCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_INSTRUMENT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_INSTRUMENT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PAYMENT_INSTRUMENT_CODE, Value)
        End Set
    End Property



    Public Property InstallmentsNumber As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_INSTALLMENTS_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_INSTALLMENTS_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_INSTALLMENTS_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property CreditCardTypeCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_TYPE_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property NameOnCreditCard As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_NAME_ON_CREDIT_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_NAME_ON_CREDIT_CARD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_NAME_ON_CREDIT_CARD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=64)> _
    Public Property CreditCardNumber As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property



    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property BankId As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=29)>
    Public Property AccountNumber As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property NameOnAccount As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_NAME_ON_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_NAME_ON_ACCOUNT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_NAME_ON_ACCOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property Layout As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property BranchCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_BRANCH_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_BRANCH_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property ExternalCarCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_EXTERNAL_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_EXTERNAL_CAR_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_EXTERNAL_CAR_CODE, Value)
        End Set
    End Property

    Public Property PlanAmountWithMarkup As DecimalType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT_WITH_MARKUP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT_WITH_MARKUP), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PLAN_AMOUNT_WITH_MARKUP, Value)
        End Set
    End Property


    Public Property PaymentDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_PAYMENT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_PAYMENT_DATE, Value)
        End Set
    End Property


    Public Property CancellationDate As DateType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CancellationReasonCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_REASON_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_REASON_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_REASON_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CancelCommentTypeCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CANCEL_COMMENT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CANCEL_COMMENT_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CANCEL_COMMENT_TYPE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property CancellationComment As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_COMMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_COMMENT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_CANCELLATION_COMMENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property FinancingAgency As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_FINANCING_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_FINANCING_AGENCY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_FINANCING_AGENCY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property NcPaymentMethodCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_NC_PAYMENT_METHOD_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_NC_PAYMENT_METHOD_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_NC_PAYMENT_METHOD_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property AccountTypeCode As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_TYPE_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property TaxId As String
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerVscReconWrkDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property



    Public Property BranchDigit As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_BRANCH_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_BRANCH_DIGIT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_BRANCH_DIGIT, Value)
        End Set
    End Property



    Public Property AccountDigit As LongType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_DIGIT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_ACCOUNT_DIGIT, Value)
        End Set
    End Property



    Public Property RefundAmount As DecimalType
        Get
            CheckDeleted()
            If Row(DealerVscReconWrkDAL.COL_NAME_REFUND_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerVscReconWrkDAL.COL_NAME_REFUND_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerVscReconWrkDAL.COL_NAME_REFUND_AMOUNT, Value)
        End Set
    End Property


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
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerVscReconWrkDAL
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

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerVscReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(DealerVscReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerVscReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(DealerVscReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region


#Region "Custom Validation"


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateDealer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.WS_INVALID_DEALER_DOESNOT_EXIST_IN_DEALER_GROUP)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DealerVscReconWrk = CType(objectToValidate, DealerVscReconWrk)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            End If

            If obj.DealerId = Guid.Empty Then
                Return False
            End If

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
            Dim obj As DealerVscReconWrk = CType(objectToValidate, DealerVscReconWrk)

            If obj.CertificateNumber Is String.Empty OrElse obj.CertificateNumber Is Nothing Then
                Return True
            End If
            Dim oCompany As New Company(obj.CompanyId(obj.DealerfileProcessedId))
            If (Not oCompany.CertNumberFormatId.Equals(Guid.Empty)) AndAlso (obj.RecordType = RECORD_TYPE_FC) Then
                Dim objRegEx As New System.Text.RegularExpressions.Regex(LookupListNew.getCertNumberFormatDescription(oCompany.CertNumberFormatId), RegularExpressions.RegexOptions.IgnoreCase Or RegularExpressions.RegexOptions.CultureInvariant)
                Dim strToValidate As String = obj.CertificateNumber.Trim(" ".ToCharArray)
                Dim blnValid As Boolean
                blnValid = objRegEx.IsMatch(strToValidate)
                Return blnValid
            Else
                Return True
            End If

        End Function
    End Class
#End Region

End Class


