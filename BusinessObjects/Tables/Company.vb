'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/23/2004)  ********************
Public Class Company
    Inherits BusinessObjectBase
    Implements IAttributable

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Dim dal As New CompanyDAL
        If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
            dal.LoadSchema(Dataset)
        End If
        Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
        Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
        Row = newRow
        SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Initialize()
    End Sub

    Protected Sub Load(id As Guid)
        Row = Nothing
        Dim dal As New CompanyDAL
        If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
            Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
        End If
        If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
            dal.Load(Dataset, id)
            dal.LoadAccountClosingInfoByCompanyID(Dataset, id)
            Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
        End If
        If Row Is Nothing Then
            Throw New DataNotFoundException
        End If
    End Sub

    Protected Sub LoadChildren(reloadData As Boolean)
        CountryPostalCodeFormat.LoadList(Dataset, Id, reloadData)
    End Sub

#End Region

#Region "Constants"
    Public Const COMPANY_TYPE_SERVICES As String = "2"
    Public Const COMPANY_TYPE_INSURANCE As String = "1"

    Public Const CLIP_METHOD_NONE As String = "0"
#End Region

#Region "Private Members"
    Private Sub Initialize()
        CompanyTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMPANY_TYPE, COMPANY_TYPE_SERVICES)
        UPR_USES_WP_Id = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "Y")
        SalutationId = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "N")
        ClaimNumberOffset = New LongType(0)
        RequireItemDescriptionId = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "Y")
        ReqCustomerLegalInfoId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")
        UniqueCertificateNumbersId = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "N")
        UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "N")
    End Sub

#End Region

#Region "Properties"
    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return CompanyDAL.TABLE_NAME.ToUpper()
        End Get
    End Property

    Private _constrVoilation As Boolean
    Public Property ConstrVoilation As Boolean
        Get
            Return _constrVoilation
        End Get
        Set
            _constrVoilation = Value
        End Set
    End Property
    'Key Property
    Public ReadOnly Property Id As Guid Implements IAttributable.Id
        Get
            If Row(CompanyDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=70)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5), CheckIfCompanyCodeAlreadyExists("")>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property TaxIdNumber As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_TAX_ID_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_TAX_ID_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BusinessCountryId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_BUSINESS_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_BUSINESS_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_BUSINESS_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property Phone As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_PHONE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property Fax As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_FAX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_FAX, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50), EmailAddress("")> _
    Public Property Email As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                'Return CType(Row(CompanyDAL.COL_NAME_EMAIL), EmailType)
                Return CType(Row(CompanyDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0)> _
    Public Property RefundToleranceAmt As DecimalType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_REFUND_TOLERANCE_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CompanyDAL.COL_NAME_REFUND_TOLERANCE_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_REFUND_TOLERANCE_AMT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimNumberFormatId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CLAIM_NUMBER_FORMAT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_CLAIM_NUMBER_FORMAT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CLAIM_NUMBER_FORMAT_ID, Value)
        End Set
    End Property
    Public Property CertNumberFormatId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CERT_NUMBER_FORMAT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_CERT_NUMBER_FORMAT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CERT_NUMBER_FORMAT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InvoiceMethodId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_INVOICE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_INVOICE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_INVOICE_METHOD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LanguageId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_LANGUAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_LANGUAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0)> _
    Public Property DefaultFollowupDays As LongType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_DEFAULT_FOLLOWUP_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyDAL.COL_NAME_DEFAULT_FOLLOWUP_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_DEFAULT_FOLLOWUP_DAYS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0), FollowupdaysMax("")> _
    Public Property MaxFollowupDays As LongType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_MAX_FOLLOWUP_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyDAL.COL_NAME_MAX_FOLLOWUP_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_MAX_FOLLOWUP_DAYS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property LegalDisclaimer As String
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_LEGAL_DISCLAIMER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyDAL.COL_NAME_LEGAL_DISCLAIMER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_LEGAL_DISCLAIMER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SalutationId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_SALUTATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_SALUTATION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_CITY, Value)
        End Set
    End Property



    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    <PostalCodeChecker(Common.ErrorCodes.INVALID_POSTALCODEFORMAT_ERR)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyTypeId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_COMPANY_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_COMPANY_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_COMPANY_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UPR_USES_WP_Id As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_UPR_USES_WP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_UPR_USES_WP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_UPR_USES_WP_ID, Value)
        End Set
    End Property

    '09/11/2006 - ALR - Added for auto closing claims
    <ValueMandatory(""), ValidNumericRange("", Min:=0)> _
    Public Property DaysToCloseClaim As LongType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_DAYS_TO_CLOSE_CLAIM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyDAL.COL_NAME_DAYS_TO_CLOSE_CLAIM), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_DAYS_TO_CLOSE_CLAIM, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property MasterClaimProcessingId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_MASTER_CLAIM_PROCESSING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_MASTER_CLAIM_PROCESSING_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_MASTER_CLAIM_PROCESSING_ID, Value)
        End Set
    End Property

    '11/01/2006 - AA - Added for WR:769958_Support claim # offset
    <ValueMandatory(""), ValidNumericRange("", Min:=0), ValidOffsetMax("")> _
    Public Property ClaimNumberOffset As LongType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CLAIM_NUMBER_OFFSET) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyDAL.COL_NAME_CLAIM_NUMBER_OFFSET), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CLAIM_NUMBER_OFFSET, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EUMemberId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_EU_MEMBER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_EU_MEMBER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_EU_MEMBER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UseZipDistrictId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_USE_ZIP_DISTRICTS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_USE_ZIP_DISTRICTS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_USE_ZIP_DISTRICTS_ID, Value)
        End Set
    End Property

    Public Property AcctCompanyId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_ACCT_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_ACCT_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AuthDetailRqrdId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_AUTH_DETAIL_RQRD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_AUTH_DETAIL_RQRD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_AUTH_DETAIL_RQRD_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AddlDACId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_ADDL_DAC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_ADDL_DAC_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_ADDL_DAC_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AutoProcessFileId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_AUTO_PROCESS_FILE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_AUTO_PROCESS_FILE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_AUTO_PROCESS_FILE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UseRecoveriesId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_USE_RECOVERIES_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_USE_RECOVERIES_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_USE_RECOVERIES_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ServiceOrdersByDealerId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_SERVICE_ORDERS_BY_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_SERVICE_ORDERS_BY_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_SERVICE_ORDERS_BY_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RequireItemDescriptionId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_REQUIRE_ITEM_DESCRIPTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_REQUIRE_ITEM_DESCRIPTION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_REQUIRE_ITEM_DESCRIPTION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RequiresAgentCodeId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_REQUIRE_AGENT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_REQUIRE_AGENT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_REQUIRE_AGENT_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClipMethodId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CLIP_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_CLIP_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CLIP_METHOD_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ReportCommissionTaxId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_REPORT_COMMISSION_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_REPORT_COMMISSION_TAX_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_REPORT_COMMISSION_TAX_ID, Value)
        End Set
    End Property

    Public Property TimeZoneNameId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_TIME_ZONE_NAME_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_TIME_ZONE_NAME_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_TIME_ZONE_NAME_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ComputeTaxBasedId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_COMPUTE_TAX_BASED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_COMPUTE_TAX_BASED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_COMPUTE_TAX_BASED_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property BillingByDealerId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_BILLING_BY_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_BILLING_BY_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_BILLING_BY_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PoliceRptForLossCovId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_POLICE_RPT_FOR_LOSS_COV_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_POLICE_RPT_FOR_LOSS_COV_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_POLICE_RPT_FOR_LOSS_COV_ID, Value)
        End Set
    End Property

    '<ValueMandatory("")>
    'Public Property EnablePeriodMileageVal() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CompanyDAL.COL_NAME_ENABLE_PERIOD_MILEAGE_VAL) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CompanyDAL.COL_NAME_ENABLE_PERIOD_MILEAGE_VAL), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CompanyDAL.COL_NAME_ENABLE_PERIOD_MILEAGE_VAL, Value)
    '    End Set
    'End Property

    Public Property FtpSiteId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_FTP_SITE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_FTP_SITE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_FTP_SITE_ID, Value)
        End Set
    End Property
    'REQ-910 new flag
    <ValueMandatory("")> _
    Public Property ReqCustomerLegalInfoId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_REQ_CUSTOMER_LEGAL_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_REQ_CUSTOMER_LEGAL_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_REQ_CUSTOMER_LEGAL_INFO_ID, Value)
        End Set
    End Property

    Public Property UseTransferOfOwnership As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_USE_TRANSFER_OF_OWNERSHIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_USE_TRANSFER_OF_OWNERSHIP), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_USE_TRANSFER_OF_OWNERSHIP, Value)
        End Set
    End Property

    <ValueMandatory(""), CheckDoublicatePREFIX("")> _
    Public Property UniqueCertificateNumbersId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_UNIQUE_CERTIFICATE_NUMBERS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_UNIQUE_CERTIFICATE_NUMBERS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_UNIQUE_CERTIFICATE_NUMBERS_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Override_WarrantyPrice_Check As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_OVERRIDE_WARRANTYPRICE_CHECK) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_OVERRIDE_WARRANTYPRICE_CHECK), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_OVERRIDE_WARRANTYPRICE_CHECK, Value)
        End Set
    End Property

    Public Property UniqueCertEffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_UNIQUE_CERT_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CompanyDAL.COL_NAME_UNIQUE_CERT_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_UNIQUE_CERT_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property CertnumlookupbyId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_NAME_CERTNUMLOOKUPBY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_NAME_CERTNUMLOOKUPBY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_NAME_CERTNUMLOOKUPBY_ID, Value)
        End Set
    End Property

    Public Property UsePreInvoiceProcessId As Guid
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_USE_PRE_INVOICE_PROCESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyDAL.COL_USE_PRE_INVOICE_PROCESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_USE_PRE_INVOICE_PROCESS_ID, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0)> _
    Public Property SCPreInvWaitingPeriod As LongType
        Get
            CheckDeleted()
            If Row(CompanyDAL.COL_SC_PRE_INV_WAITING_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyDAL.COL_SC_PRE_INV_WAITING_PERIOD), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyDAL.COL_SC_PRE_INV_WAITING_PERIOD, Value)
        End Set
    End Property

#End Region

#Region "Address Children"

#End Region

#Region "Public Members"

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsFamilyDirty()
        End Get
    End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompanyDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                    'Me._address = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            'Me._address = Nothing
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub Copy(original As Company)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Company")
        End If
        'Copy myself
        CopyFrom(original)

    End Sub

    Public Sub DeleteAndSave()
        CheckDeleted()
        'Dim addr As Address = Me.Address
        BeginEdit()
        'addr.BeginEdit()
        Try
            Delete()
            'addr.Delete()
            Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            cancelEdit()
            'addr.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        Catch ex As Exception
            If ex.Message = "Integrity Constraint Violation" Then
                ConstrVoilation = True
            End If
            cancelEdit()
            Throw ex
        End Try
    End Sub

    Public ReadOnly Property ClaimsImageRepositoryCode As String
        Get
            Return String.Format("ElitaApp-{0}-{1}", EnvironmentContext.Current.EnvironmentShortName.ToUpperInvariant(), Code)
        End Get
    End Property

    Public Function GetClaimImageRepository() As Documents.Repository
        Dim dr As Documents.Repository
        Try
            dr = Documents.DocumentManager.Current.Repositories.Where(Function(r) r.Code = ClaimsImageRepositoryCode.ToUpper()).FirstOrDefault()
        Catch ex As Exception
            Throw New ImageRepositoryNotFound(ClaimsImageRepositoryCode)
        End Try

        If (dr Is Nothing) Then
            Throw New ImageRepositoryNotFound(ClaimsImageRepositoryCode)
        End If
        Return dr
    End Function


    Public ReadOnly Property CertificatesImageRepositoryCode As String
        Get
            Return String.Format("ElitaApp-Policy-{0}-{1}", EnvironmentContext.Current.EnvironmentShortName.ToUpperInvariant(), Code)
        End Get
    End Property

    Public Function GetCertificateImageRepository() As Documents.Repository
        Dim dr As Documents.Repository
        Try
            dr = Documents.DocumentManager.Current.Repositories.Where(Function(r) r.Code = CertificatesImageRepositoryCode.ToUpper()).FirstOrDefault()
        Catch ex As Exception
            Throw New ImageRepositoryNotFound(CertificatesImageRepositoryCode)
        End Try

        If (dr Is Nothing) Then
            Throw New ImageRepositoryNotFound(CertificatesImageRepositoryCode)
        End If
        Return dr
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(descriptionMask As String, codeMask As String) As CompanySearchDV
        Try
            Dim dal As New CompanyDAL
            Return New CompanySearchDV(dal.LoadList(descriptionMask, codeMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public ReadOnly Property AssociatedAccCloseInfo(cmpId As Guid) As AccountingCloseInfo.AccountingCloseInfoList
        Get
            Return New AccountingCloseInfo.AccountingCloseInfoList(Me, cmpId)
        End Get
    End Property

    Public ReadOnly Property AssociatedAccCloseInfoCount(cmpId As Guid) As Integer
        Get
            Try
                Dim acc As AccountingCloseInfo.AccountingCloseInfoListCount
                acc = New AccountingCloseInfo.AccountingCloseInfoListCount(Me, cmpId)
                Return acc.Count
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


    Public Sub AttachAccCloseInfo(ClosingDate As DateType)

        Dim newBO As AccountingCloseInfo = New AccountingCloseInfo(Dataset)
        If newBO IsNot Nothing Then
            newBO.CompanyId = Id
            newBO.ClosingDate = ClosingDate
            newBO.Save()
        End If

    End Sub

    Public Sub DetachAccCloseInfo(AccCloseInfo As AccountingCloseInfo)

        If AccCloseInfo IsNot Nothing Then
            AccCloseInfo.Delete()
            AccCloseInfo.Save()
        End If

    End Sub

    Public Sub ResetAccountClosingInfoList()
        If Dataset.Tables.Count > 0 Then
            If Dataset.Tables.IndexOf(AccountingCloseInfoDAL.TABLE_NAME) > 0 Then
                Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME).Clear()
            End If
        End If
    End Sub

    Public Shared Function GetComanies(groupId As Guid) As ArrayList

        Dim dal As New CompanyDAL
        Dim ds As DataSet = dal.LoadCompanies(groupId)

        Dim dtCompanyIDs As DataTable = ds.Tables(dal.TABLE_NAME_GROUP_COMPANIES)

        Dim CompanyIDs = New ArrayList

        If dtCompanyIDs.Rows.Count > 0 Then
            Dim index As Integer
            ' Create Array
            For index = 0 To dtCompanyIDs.Rows.Count - 1
                If dtCompanyIDs.Rows(index)(dal.COL_NAME_COMPANY_ID) IsNot DBNull.Value Then
                    CompanyIDs.Add(New Guid(CType(dtCompanyIDs.Rows(index)(dal.COL_NAME_COMPANY_ID), Byte())))
                End If
            Next
        End If

        Return CompanyIDs

    End Function
#Region "CompanySearchDV"
    Public Class CompanySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_CODE As String = "code"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#End Region


#Region "Countries"
    Public Sub AttachCountries(selectedCountyGuidStrCollection As ArrayList)
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In selectedCountyGuidStrCollection
            'update to new CompanyCountry GUID
            Dim newBO As CompanyCountry = New CompanyCountry(Dataset)
            If newBO IsNot Nothing Then
                newBO.CompanyId = Id
                newBO.CountryId = New Guid(cmpCountryIdStr)
                newBO.Save()
            End If
        Next

    End Sub

    Public Sub DetachCountries(selectedCountyGuidStrCollection As ArrayList)
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In selectedCountyGuidStrCollection
            'update to new CompanyCountry GUID
            Dim newBO As CompanyCountry = New CompanyCountry(Dataset, Id, New Guid(cmpCountryIdStr))
            If newBO IsNot Nothing Then
                newBO.Delete()
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachAccountClosingInfo()
        Dim accountingCloseInfo As AccountingCloseInfo
        For index As Integer = 0 To Dataset.Tables("ELP_ACCOUNTING_CLOSE_INFO").Rows.Count - 1
            Dataset.Tables("ELP_ACCOUNTING_CLOSE_INFO").Rows.Item(index).Delete()
            'Me.Dataset.Tables("ELP_ACCOUNTING_CLOSE_INFO").Rows.Item(index).AcceptChanges()
        Next
    End Sub

    Public Shared Function GetAvailableCountries(companyId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As CompanyDAL = New CompanyDAL
        cpDAL.LoadAvailableCountries(ds, companyId)
        Return ds
    End Function


    Public Shared Function GetSelectedCountries(companyId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As CompanyDAL = New CompanyDAL
        cpDAL.LoadSelectedCountries(ds, companyId)
        Return ds
    End Function

    'REQ-1295 : Returns count of Dealers having  company = passed company and at coverage level no Agent 
    Public Shared Function GetCompanyDealerWithoutAgent(companyId As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim cpDAL As CompanyDAL = New CompanyDAL
        cpDAL.GetCompanyDealerWithoutAgent(ds, companyId)
        Return ds
    End Function

    Public Shared Function GetCompanyAgentFlagForDealer(dealerID As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim cpDAL As CompanyDAL = New CompanyDAL
        cpDAL.GetCompanyAgentFlagForDealer(ds, dealerID)
        Return ds
    End Function

    Public Shared Function GetDealerFromCompany(CompanyId As Guid, DealerCode As String) As DataSet
        Try
            Dim dal As New CompanyDAL
            Dim ds As DataSet
            Return dal.GetDealerFromCompany(CompanyId, DealerCode)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function CheckIfCompanyCodeAlreadyExists(Code As String, Id As Guid) As Boolean
        Try
            Dim dal As New CompanyDAL
            Dim ds As DataSet
            Dim result As Boolean
            Dim CompanyID As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCompanyLookupList(), Code)

            If CompanyID = Guid.Empty OrElse Id = CompanyID Then
                Return False
            End If
            'result = dal.CheckIfCompanyCodeAlreadyExists(Code)
            Return True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


End Class
#Region "CustomValidation"

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class PostalCodeChecker
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.BO_ZIP_DISTRICT_INVALID_ZIP_CODE_FORMAT)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)

        If valueToCheck Is Nothing OrElse valueToCheck Is String.Empty Then
            Return True
        End If

        Dim testValidator As PostalCodeValidator = New PostalCodeValidator(obj.CountryId, valueToCheck)
        Dim formatResult As PostalCodeFormatResult = testValidator.IsValid()
        Return formatResult.IsValid

    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class FollowupdaysMax
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_MAX_FOLLOWUP_DAYS)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)

        If obj.MaxFollowupDays Is String.Empty OrElse obj.MaxFollowupDays Is Nothing Then
            Return True
        End If

        If obj.DefaultFollowupDays Is String.Empty OrElse obj.DefaultFollowupDays Is Nothing Then
            Return True
        End If

        If obj.DefaultFollowupDays.Value > obj.MaxFollowupDays.Value Then
            Return False
        End If

        Return True

    End Function

End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class EmailAddress
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)

        If obj.Email Is String.Empty OrElse obj.Email Is Nothing Then
            Return True
        End If

        Return MiscUtil.EmailAddressValidation(obj.Email)

    End Function

End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidOffsetMax
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_NUMBER_OFFSET_IS_TOO_LARGE)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)
        Dim d As Date = Today
        Dim yearInteger As Integer = CType(d.Year.ToString.Substring(2, 2), Integer)

        If obj.ClaimNumberOffset Is String.Empty OrElse obj.ClaimNumberOffset Is Nothing Then
            Return True
        End If

        If (obj.ClaimNumberOffset.Value + yearInteger) >= 90 Then
            Return False
        End If

        Return True

    End Function

End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class CheckDoublicatePREFIX
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FOUND_DEALERS_HAVING_DUPLICATE_CERTIFICATES_AUTONUMBER_PREFIX)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)
        Dim yesGuid As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "Y")

        If Not obj.UniqueCertificateNumbersId.Equals(Guid.Empty) AndAlso obj.UniqueCertificateNumbersId.Equals(yesGuid) Then
            If Dealer.GetDuplicatePrefixCount(obj.Id) > 0 Then
                Return False
            End If
        End If
        Return True


    End Function

End Class


<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
Public NotInheritable Class CheckIfCompanyCodeAlreadyExists
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.ERR_MSG_COMPANY_CODE_ALREADY_EXISTS)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As Company = CType(objectToValidate, Company)
        Dim yesGuid As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, "Y")

        If Company.CheckIfCompanyCodeAlreadyExists(obj.Code, obj.Id) Then
            Return False
        End If
        Return True
    End Function

End Class
#End Region





