'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/7/2004)  ********************

Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb


Public Class ServiceCenter
    Inherits BusinessObjectBase
    Implements IAttributable
    'Implements Address.IAddressUser
#Region "Constants"
    Public Const DV_NO_ROWS_FOUND As Integer = 0
    Public Const DV_ROWS_LESS_THAT_MIN As Integer = 10
    Private Const SEARCH_EXCEPTION As String = "SERVICECENTERLIST_FORM001" ' Certificate List Search Exception
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
    Public Const ACTIVE_STATUS_CODE As String = "A"

    Friend Const VENDOR_MANAGEMENT001 As String = "VENDOR_MANAGEMENT001"
    Friend Const VENDOR_MANAGEMENT002 As String = "VENDOR_MANAGEMENT002"
    Friend Const ELP_SERVICE_CENTER As String = "ELP_SERVICE_CENTER"
    'START DEF-2818
    Public Const DEFAULT_NET_DAYS As String = "30"
    'END    DEF-2818

#End Region

#Region "Constructors"

    'Existing BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    Public Sub New(ByVal code As String)
        MyBase.New()
        Dataset = New DataSet
        Load(code)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Existing BO attaching to a BO family
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

    'Existing BO 
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ServiceCenterDAL
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
            Dim dal As New ServiceCenterDAL
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

    Protected Sub Load(ByVal code As String)
        Try
            Dim dal As New ServiceCenterDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(Id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim oCountryIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
                dal.Load(Dataset, code, oCountryIds)
                If Not Dataset.Tables(dal.TABLE_NAME) Is Nothing AndAlso Dataset.Tables(dal.TABLE_NAME).Rows.Count > 0 Then
                    Row = FindRow(New Guid(CType(Dataset.Tables(dal.TABLE_NAME).Rows(0)(dal.TABLE_KEY_NAME), Byte())),
                                    dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                Else
                    Throw New StoredProcedureGeneratedException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If
            End If
            If Row Is Nothing Then
                Throw New StoredProcedureGeneratedException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Shared Function GetServiceCenterID(ByVal code As String) As Guid
        Dim dal As New ServiceCenterDAL
        Dim serviceCenterId As Guid = Guid.Empty
        Dim ds As DataSet = New DataSet("ServiceCenterID")

        Try
            Dim oCountryIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            dal.GetServiceCenterID(ds, code, oCountryIds)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                serviceCenterId = New Guid(CType(ds.Tables(dal.TABLE_NAME).Rows(0)(dal.TABLE_KEY_NAME), Byte()))
            End If

            Return serviceCenterId

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        '  Me.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        StatusCode = "A"
        'START  2818
        If NetDays Is Nothing Then
            Me.NetDays = DEFAULT_NET_DAYS
        End If
        'END    2818
        PreInvoiceId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(Codes.LIST__SVCPREINV, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), Codes.LIST_ITEM__SVCPICOMP)
    End Sub
    Private _isBankInfoNeedDeletion As Boolean
    Private _lastPaymentMethodId As Guid = Guid.Empty
    Private _constrVoilation As Boolean
    Private _intMethodOfRepairCount As Integer = -1
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

    Public Property OriginalDealerId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_ORIGINAL_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_ORIGINAL_DEALER), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_ORIGINAL_DEALER, Value)
        End Set
    End Property

    'Key Property
    Public ReadOnly Property Id As Guid Implements IAttributable.Id
        Get
            If Row(ServiceCenterDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property AddressId As Guid 'Implements Address.IAddressUser.AddressId
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property

    '<ValueMandatory("")> _
    'Public Property PriceGroupId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(ServiceCenterDAL.COL_NAME_PRICE_GROUP_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_PRICE_GROUP_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(ServiceCenterDAL.COL_NAME_PRICE_GROUP_ID, Value)
    '    End Set
    'End Property

    <ValueMandatory("")>
    Public Property ServiceGroupId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_SERVICE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_SERVICE_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_SERVICE_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PaymentMethodId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
        End Set
    End Property


    Public Property LoanerCenterId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_LOANER_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_LOANER_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_LOANER_CENTER_ID, Value)
        End Set
    End Property

    Public Property MasterCenterId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_MASTER_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_MASTER_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_MASTER_CENTER_ID, Value)
            If Value = Guid.Empty Then
                PayMaster = False
            End If
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10), ValueMethodOfRepair("")>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50), ValidateByteLength("", Max:=30)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property RatingCode As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_RATING_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_RATING_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_RATING_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property ContactName As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_CONTACT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_CONTACT_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=30)>
    Public Property OwnerName As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_OWNER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_OWNER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_OWNER_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property Phone1 As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PHONE1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_PHONE1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PHONE1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Phone2 As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PHONE2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_PHONE2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PHONE2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property Fax As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_FAX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_FAX, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50), ValidEmail(""), EmailAddress("")>
    Public Property Email As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property FtpAddress As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_FTP_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_FTP_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_FTP_ADDRESS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property TaxId As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=365)>
    Public Property ServiceWarrantyDays As LongType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_SERVICE_WARRANTY_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ServiceCenterDAL.COL_NAME_SERVICE_WARRANTY_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_SERVICE_WARRANTY_DAYS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property BusinessHours As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_BUSINESS_HOURS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_BUSINESS_HOURS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_BUSINESS_HOURS, Value)
        End Set
    End Property
    Public Property ConstrVoilation As Boolean
        Get
            Return _constrVoilation
        End Get
        Set
            _constrVoilation = Value
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property MasterFlag As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_MASTER_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_MASTER_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_MASTER_FLAG, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property LoanerFlag As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_LOANER_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_LOANER_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_LOANER_FLAG, Value)
        End Set
    End Property

    Public Property isBankInfoNeedDeletion As Boolean
        Get
            Return _isBankInfoNeedDeletion
        End Get
        Set
            _isBankInfoNeedDeletion = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DefaultToEmailFlag As Boolean
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_DEFAULT_TO_EMAIL_FLAG) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_DEFAULT_TO_EMAIL_FLAG), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(ServiceCenterDAL.COL_NAME_DEFAULT_TO_EMAIL_FLAG, "Y")
            Else
                SetValue(ServiceCenterDAL.COL_NAME_DEFAULT_TO_EMAIL_FLAG, "N")
            End If
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IvaResponsibleFlag As Boolean
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_IVA_RESPONSIBLE_FLAG) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_IVA_RESPONSIBLE_FLAG), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(ServiceCenterDAL.COL_NAME_IVA_RESPONSIBLE_FLAG, "Y")
            Else
                SetValue(ServiceCenterDAL.COL_NAME_IVA_RESPONSIBLE_FLAG, "N")
            End If
        End Set
    End Property

    Public Property FreeZoneFlag As Boolean
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_FREE_ZONE_FLAG) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_FREE_ZONE_FLAG), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(ServiceCenterDAL.COL_NAME_FREE_ZONE_FLAG, "Y")
            Else
                SetValue(ServiceCenterDAL.COL_NAME_FREE_ZONE_FLAG, DBNull.Value)
            End If
        End Set
    End Property


    Public ReadOnly Property DateAdded As DateType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ServiceCenterDAL.COL_NAME_CREATED_DATE), Date))
            End If
        End Get

    End Property

    Public ReadOnly Property DateModified As DateType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ServiceCenterDAL.COL_NAME_MODIFIED_DATE), Date))
            End If
        End Get

    End Property

    Public ReadOnly Property HasLoanerCenter As Boolean
        Get
            Return Not LoanerCenterId.Equals(Guid.Empty)
        End Get
    End Property

    <ValidStringLength("", Max:=1000)>
    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property


    Public Property BankInfoId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_BANKINFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_BANKINFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_BANKINFO_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property Shipping As Boolean
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_SHIPPING) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_SHIPPING), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(ServiceCenterDAL.COL_NAME_SHIPPING, "Y")
            Else
                SetValue(ServiceCenterDAL.COL_NAME_SHIPPING, "N")
            End If
        End Set
    End Property
    <ValueMandatoryConditionally(""), ValidAmount("")>
    Public Property ProcessingFee As DecimalType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PROCESSING_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ServiceCenterDAL.COL_NAME_PROCESSING_FEE), Decimal))
            End If


        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PROCESSING_FEE, Value)
        End Set
    End Property

    Private Property LastPaymentMethodId As Guid
        Get
            Return _lastPaymentMethodId
        End Get
        Set
            _lastPaymentMethodId = Value
        End Set
    End Property

    <ValidStringLength("", Max:=50), CcEmailAddress("")>
    Public Property CcEmail As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CC_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_CC_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_CC_EMAIL, Value)
        End Set
    End Property

    Public Property RouteId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_ROUTE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_ROUTE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_ROUTE_ID, Value)
        End Set
    End Property

    Public Property IntegratedWithID As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_INTEGRATED_WITH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_INTEGRATED_WITH_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_INTEGRATED_WITH_ID, Value)
        End Set
    End Property

    Public ReadOnly Property IntegratedWithGVS As Boolean
        Get
            Dim ret As Boolean = False

            If Not IntegratedWithID.Equals(Guid.Empty) Then
                Dim code As String = LookupListNew.GetCodeFromId(LookupListNew.GetIntegratedWithLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), IntegratedWithID)

                If Not code Is Nothing AndAlso code = Codes.INTEGRATED_WITH_GVS Then
                    ret = True
                End If
            End If

            Return ret
        End Get
    End Property

    Public Property PayMaster As Boolean
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PAY_MASTER) Is DBNull.Value Then
                Return False
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_PAY_MASTER), String) = "Y"
            End If
        End Get
        Set
            CheckDeleted()
            If Value Then
                SetValue(ServiceCenterDAL.COL_NAME_PAY_MASTER, "Y")
            Else
                SetValue(ServiceCenterDAL.COL_NAME_PAY_MASTER, "N")
            End If

        End Set
    End Property

    Public ReadOnly Property IntegratedAsOf As DateTimeType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_INTEGRATED_AS_OF) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_INTEGRATED_AS_OF), DateTime)
            End If
        End Get

    End Property

    Public Property MethodOfRepairCount As Integer
        Get
            If _intMethodOfRepairCount = -1 Then
                _intMethodOfRepairCount = GetSelectedMethodOfRepair.Count
            End If
            Return _intMethodOfRepairCount
        End Get
        Set
            _intMethodOfRepairCount = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReverseLogisticsId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_REVERSE_LOGISTICS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_REVERSE_LOGISTICS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_REVERSE_LOGISTICS_ID, Value)
        End Set
    End Property

    'Public Property ServiceLevelGroupId() As Guid
    '    Get
    '        CheckDeleted()
    '        If row(ServiceCenterDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(row(ServiceCenterDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(ServiceCenterDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID, Value)
    '    End Set
    'End Property

    Public Property DistributionMethodId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_DISTRIBUTION_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_DISTRIBUTION_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_DISTRIBUTION_METHOD_ID, Value)
        End Set
    End Property



    Public Property FulfillmentTimeZoneId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_FULFILLMENT_TIME_ZONE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_FULFILLMENT_TIME_ZONE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_FULFILLMENT_TIME_ZONE_ID, Value)
        End Set
    End Property

    'DEF-2818 : added the validation for ValidPriceListCode
    <ValueMandatory(""), ValidStringLength("", Max:=40), ValidPriceListCode("")>
    Public Property PriceListCode As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE, Value)
        End Set
    End Property

    Public Property PriceListCode_Current As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PRICE_LIST_CODE, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=100, Min:=0)>
    Public Property DiscountPct As DecimalType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ServiceCenterDAL.COL_NAME_DISCOUNT_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_DISCOUNT_PCT, Value)
        End Set
    End Property


    <ValueIsAnInteger(""), ValidNumericRange("", Min:=0)>
    Public Property DiscountDays As LongType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_DISCOUNT_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ServiceCenterDAL.COL_NAME_DISCOUNT_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_DISCOUNT_DAYS, Value)
        End Set
    End Property


    <ValueIsAnInteger(""), ValidNumericRange("", Min:=0)>
    Public Property NetDays As LongType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_NET_DAYS) Is DBNull.Value Then
                'START  DEF-2818
                Return Nothing
                'END    DEF-2818
            Else
                Return New LongType(CType(Row(ServiceCenterDAL.COL_NAME_NET_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            'START  DEF-2818
            'If Value Is Nothing Then
            '    Value = 30
            'End If
            'END    DEF-2818
            SetValue(ServiceCenterDAL.COL_NAME_NET_DAYS, Value)
        End Set
    End Property

    Public ReadOnly Property MyDataset As DataSet
        Get
            Return Dataset
        End Get
    End Property

    <ValueMandatory("")>
    Public Property PreInvoiceId As Guid
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_PRE_INVOICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceCenterDAL.COL_NAME_PRE_INVOICE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_PRE_INVOICE_ID, Value)
        End Set
    End Property
    Public Property AutoProcessInventoryFileXcd As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_AUTO_PROCESS_INV_FILE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_AUTO_PROCESS_INV_FILE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_AUTO_PROCESS_INV_FILE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30, Message:="ClaimReservedBasedOnXcd should be between 1 to 30 chars.")>
    Public Property ClaimReservedBasedOnXcd As String
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_BASED_ON_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_BASED_ON_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_BASED_ON_XCD, Value)
        End Set
    End Property
    <ValidNumericRange("", Max:=100, Min:=0, MaxExclusive:=False, MinExclusive:=True), RequiredConditionally("")>
    Public Property ClaimReservedPercent As DecimalType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_CLAIM_RESERVED_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=100, Min:=0)>
    Public Property WithholdingRate As DecimalType
        Get
            CheckDeleted()
            If Row(ServiceCenterDAL.COL_NAME_WITHHOLDING_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ServiceCenterDAL.COL_NAME_WITHHOLDING_RATE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceCenterDAL.COL_NAME_WITHHOLDING_RATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Dim blnBankInfoSave As Boolean

        Try
            If Not IsDeleted Then IntegratedWithGVSValidation()
            MyBase.Save()
            If Not IsDeleted Then LastPaymentMethodId = PaymentMethodId
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceCenterDAL '
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, LastPaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    blnBankInfoSave = True
                    isBankInfoNeedDeletion = True
                Else
                    blnBankInfoSave = False
                    If Not IsNew AndAlso isBankInfoNeedDeletion Then
                        CurrentBankInfo.BeginEdit()
                        CurrentBankInfo.Delete()
                    End If
                End If
                MyBase.UpdateFamily(Dataset)
                dal.UpdateFamily(Dataset, ElitaPlusIdentity.Current.ActiveUser.Company.Id, , blnBankInfoSave) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                    _address = Nothing
                    _bankinfo = Nothing
                    _loanerCenter = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            CurrentBankInfo.cancelEdit()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Added manually to the code
    'Public Overrides ReadOnly Property IsDirty() As Boolean
    '    Get
    '        Return MyBase.IsDirty OrElse Me.IsChildrenDirty
    '    End Get
    'End Property

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse IsChildrenDirty OrElse
            (Not Address.IsNew And Address.IsDirty) OrElse
            (Address.IsNew And Not Address.IsEmpty)

            If bDirty = False Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    bDirty = bDirty OrElse
                (Not CurrentBankInfo Is Nothing AndAlso (Not CurrentBankInfo.IsNew And CurrentBankInfo.IsDirty)) OrElse
                (Not CurrentBankInfo Is Nothing AndAlso (CurrentBankInfo.IsNew And Not CurrentBankInfo.IsEmpty))
                End If
            End If

            Return bDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ServiceCenter)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Center")
        End If
        'Copy myself
        MyBase.CopyFrom(original)

        'copy the children       
        'Manufacturers
        Dim selMfrDv As DataView = original.GetSelectedManufacturers
        Dim selMfrList As New ArrayList
        Dim i As Integer = 0
        For i = 0 To selMfrDv.Count - 1
            selMfrList.Add(New Guid(CType(selMfrDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachManufacturers(selMfrList)

        'Dealers
        Dim selDlrDv As DataView = original.GetSelectedDealers
        Dim selDlrList As New ArrayList
        Dim j As Integer = 0
        For j = 0 To selDlrDv.Count - 1
            selDlrList.Add(New Guid(CType(selDlrDv(j)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachDealers(selDlrList)

        'Networks
        Dim selNtwDv As DataView = original.GetSelectedServiceNetworks
        Dim selNtwList As New ArrayList
        Dim l As Integer = 0
        For l = 0 To selNtwDv.Count - 1
            selNtwList.Add(New Guid(CType(selNtwDv(l)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachServiceNetworks(selNtwList)

        'Districts
        Dim selDstDv As DataView = original.GetSelectedDistricts
        Dim selDstList As New ArrayList
        Dim k As Integer = 0
        For k = 0 To selDstDv.Count - 1
            selDstList.Add(New Guid(CType(selDstDv(k)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachDistricts(selDstList)

        AddressId = Guid.Empty
        Address.CopyFrom(original.Address)

        'mthod of  repair

        'Dim selMORDv As DataView = original.GetSelectedMethodOfRepair
        'Dim selMORList As New ArrayList
        'For n As Integer = 0 To selMORDv.Count - 1
        '    selMORList.Add(New Guid(CType(selMORDv(n)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        'Next
        'Me.AttachMethodOfRepair(selMORList)
    End Sub

    Public Sub DeleteAndSave()
        CheckDeleted()
        Dim addr As Address = Address
        Dim binfo As BankInfo = CurrentBankInfo
        BeginEdit()
        addr.BeginEdit()
        If Not binfo Is Nothing Then binfo.BeginEdit()
        Try
            LastPaymentMethodId = PaymentMethodId
            'delete service center reference record first
            Delete()
            'delete address and bank info record
            addr.Delete()
            If Not binfo Is Nothing Then binfo.Delete()
            Save()
        Catch ex As Exception
            If ex.Message = "Integrity Constraint Violation" Then
                ConstrVoilation = True
            End If
            cancelEdit()
            addr.cancelEdit()
            If Not binfo Is Nothing Then binfo.cancelEdit()
            Throw ex
        End Try
    End Sub

#End Region


    'METHODS ADDED MANUALLY. BEGIN
#Region "Manufacturer"


    Public ReadOnly Property ServiceCenterManufacturerChildren As ServiceCenterManufacturerList
        Get
            Return New ServiceCenterManufacturerList(Me)
        End Get
    End Property

    Public Sub UpdateManufacturers(ByVal selectedManufacturerGuidStrCollection As Hashtable)
        If selectedManufacturerGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ServiceCenterManufacturer
            For Each bo In ServiceCenterManufacturerChildren
                If Not selectedManufacturerGuidStrCollection.Contains(bo.ManufacturerId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedManufacturerGuidStrCollection
                If ServiceCenterManufacturerChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim scManBO As ServiceCenterManufacturer = ServiceCenterManufacturerChildren.GetNewChild()
                    scManBO.ManufacturerId = New Guid(entry.Key.ToString)
                    scManBO.ServiceCenterId = Id
                    scManBO.Save()
                End If
            Next
        End If
    End Sub
    Public Sub UpdateMethodOfRepair(ByVal selectedMethodOfRepairGuidStrCollection As Hashtable)
        If selectedMethodOfRepairGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ServCenterMethRepair
            For Each bo In ServiceCenterManufacturerChildren
                If Not selectedMethodOfRepairGuidStrCollection.Contains(bo.ServiceCenterId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedMethodOfRepairGuidStrCollection
                If ServiceCenterMethoOfRepairsChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim scManBO As ServCenterMethRepair = ServiceCenterMethoOfRepairsChildren.GetNewChild()
                    scManBO.ServCenterMorId = New Guid(entry.Key.ToString)
                    scManBO.ServiceCenterId = Id
                    scManBO.Save()
                End If
            Next
        End If
    End Sub

    Public Sub AttachManufacturers(ByVal selectedManufacturerGuidStrCollection As ArrayList)
        Dim scManIdStr As String
        For Each scManIdStr In selectedManufacturerGuidStrCollection
            Dim scManBO As ServiceCenterManufacturer = ServiceCenterManufacturerChildren.GetNewChild
            scManBO.ManufacturerId = New Guid(scManIdStr)
            scManBO.ServiceCenterId = Id
            scManBO.Save()
        Next
    End Sub

    Public Sub DetachManufacturers(ByVal selectedManufacturerGuidStrCollection As ArrayList)
        Dim scManIdStr As String
        For Each scManIdStr In selectedManufacturerGuidStrCollection
            Dim scManBO As ServiceCenterManufacturer = ServiceCenterManufacturerChildren.Find(New Guid(scManIdStr))
            scManBO.Delete()
            scManBO.Save()
        Next
    End Sub

    Public Function GetAvailableManufacturers() As DataView
        Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetManufacturersLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedManufacturers() As DataView
        Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetManufacturersLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetManufacturersLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim scManBO As ServiceCenterManufacturer
        Dim inClause As String = "(-1"
        For Each scManBO In ServiceCenterManufacturerChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, scManBO.ManufacturerId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function


#End Region

#Region "ServiceNetwork"

    Public ReadOnly Property ServiceCenterNetworkChildren As ServiceCenterNetworkList
        Get
            Return New ServiceCenterNetworkList(Me)
        End Get
    End Property

    Public Sub AttachServiceNetworks(ByVal selectedServiceNetworkGuidStrCollection As ArrayList)
        Dim snSrvIdStr As String
        For Each snSrvIdStr In selectedServiceNetworkGuidStrCollection
            Dim snSrvBO As ServiceNetworkSvc = ServiceCenterNetworkChildren.GetNewChild
            snSrvBO.ServiceNetworkId = New Guid(snSrvIdStr)
            snSrvBO.ServiceCenterId = Id
            snSrvBO.Save()
        Next
    End Sub

    Public Sub DetachServiceNetworks(ByVal selectedServiceNetworkGuidStrCollection As ArrayList)
        Dim snSrvIdStr As String
        For Each snSrvIdStr In selectedServiceNetworkGuidStrCollection
            Dim snSrvBO As ServiceNetworkSvc = ServiceCenterNetworkChildren.FindSrvNetwork(New Guid(snSrvIdStr))
            snSrvBO.Delete()
            snSrvBO.Save()
        Next
    End Sub

    Public Function GetAvailableServiceNetworks() As DataView

        Dim dv As DataView = LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetServiceNetworksLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedServiceNetworks() As DataView
        Dim dv As DataView = LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Dim sequenceCondition As String = GetServiceNetworksLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetServiceNetworksLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim snSrvBO As ServiceNetworkSvc
        Dim inClause As String = "(-1"
        For Each snSrvBO In ServiceCenterNetworkChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, snSrvBO.ServiceNetworkId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function


#End Region

#Region "Dealer"

    Public ReadOnly Property ServiceCenterDealerChildren As ServiceCenterDealerList
        Get
            Return New ServiceCenterDealerList(Me)
        End Get
    End Property

    Public Sub UpdateDealers(ByVal selectedDealerGuidStrCollection As Hashtable)
        If selectedDealerGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ServiceCenterDealer
            For Each bo In ServiceCenterDealerChildren
                If Not selectedDealerGuidStrCollection.Contains(bo.DealerId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedDealerGuidStrCollection
                If ServiceCenterDealerChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim scDlrBO As ServiceCenterDealer = ServiceCenterDealerChildren.GetNewChild()
                    scDlrBO.DealerId = New Guid(entry.Key.ToString)
                    scDlrBO.ServiceCenterId = Id
                    'Set the PreferredDealerFlag value to "Y" for now...
                    scDlrBO.PreferredDealerFlag = True
                    scDlrBO.Save()
                End If
            Next
        End If
    End Sub

    Public Sub AttachDealers(ByVal selectedDealerGuidStrCollection As ArrayList)
        Dim scDlrIdStr As String
        For Each scDlrIdStr In selectedDealerGuidStrCollection
            Dim scDlrBO As ServiceCenterDealer = ServiceCenterDealerChildren.GetNewChild
            scDlrBO.DealerId = New Guid(scDlrIdStr)
            scDlrBO.ServiceCenterId = Id
            'Set the PreferredDealerFlag value to "Y" for now...
            scDlrBO.PreferredDealerFlag = True
            scDlrBO.Save()
        Next
    End Sub

    Public Sub DetachDealers(ByVal selectedDealerGuidStrCollection As ArrayList)
        Dim scDlrIdStr As String
        For Each scDlrIdStr In selectedDealerGuidStrCollection
            Dim scDlrBO As ServiceCenterDealer = ServiceCenterDealerChildren.Find(New Guid(scDlrIdStr))
            scDlrBO.Delete()
            scDlrBO.Save()
        Next
    End Sub

    Public Function GetAvailableDealers() As DataView

        Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies(CountryId))
        Dim sequenceCondition As String = GetDealersLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedDealers() As DataView
        Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies(CountryId))
        Dim sequenceCondition As String = GetDealersLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetDealersLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim scDlrBO As ServiceCenterDealer
        Dim inClause As String = "(-1"
        For Each scDlrBO In ServiceCenterDealerChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, scDlrBO.DealerId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter
    End Function


#End Region

#Region "District"

    Public ReadOnly Property ServiceCenterDistrictChildren As ServiceCenterZipDistrictList
        Get
            Return New ServiceCenterZipDistrictList(Me)
        End Get
    End Property

    Public Sub UpdateDistricts(ByVal selectedDistrictGuidStrCollection As Hashtable)
        If selectedDistrictGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ServiceCenterZipDistrict
            For Each bo In ServiceCenterDistrictChildren
                If Not selectedDistrictGuidStrCollection.Contains(bo.ZipDistrictId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedDistrictGuidStrCollection
                If ServiceCenterDistrictChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim scDstBO As ServiceCenterZipDistrict = ServiceCenterDistrictChildren.GetNewChild()
                    scDstBO.ZipDistrictId = New Guid(entry.Key.ToString)
                    scDstBO.ServiceCenterId = Id
                    scDstBO.Save()
                End If
            Next
        End If
    End Sub

    Public Sub AttachDistricts(ByVal selectedDistrictGuidStrCollection As ArrayList)
        Dim scDstIdStr As String
        For Each scDstIdStr In selectedDistrictGuidStrCollection
            Dim scDstBO As ServiceCenterZipDistrict = ServiceCenterDistrictChildren.GetNewChild
            scDstBO.ZipDistrictId = New Guid(scDstIdStr)
            scDstBO.ServiceCenterId = Id
            scDstBO.Save()
        Next
    End Sub

    Public Sub DetachDistricts(ByVal selectedDistrictGuidStrCollection As ArrayList)
        Dim scDstIdStr As String
        For Each scDstIdStr In selectedDistrictGuidStrCollection
            Dim scDstBO As ServiceCenterZipDistrict = ServiceCenterDistrictChildren.Find(New Guid(scDstIdStr))
            scDstBO.Delete()
            scDstBO.Save()
        Next
    End Sub

    Public Function GetAvailableDistricts() As DataView

        ' Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(CountryId)
        Dim sequenceCondition As String = GetDistrictsLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedDistricts() As DataView
        '  Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim dv As DataView = LookupListNew.GetZipDistrictLookupList(CountryId)
        Dim sequenceCondition As String = GetDistrictsLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetDistrictsLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim scDstBO As ServiceCenterZipDistrict
        Dim inClause As String = "(-1"
        For Each scDstBO In ServiceCenterDistrictChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, scDstBO.ZipDistrictId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter
    End Function


#End Region
#Region "MethodOfRepair"

    Public Function GetAvailableMethodOfRepair() As DataView
        Dim dv As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Dim sequenceCondition As String = GetMethodOfRepairsLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv

    End Function
    Protected Function GetMethodOfRepairsLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim scMrBO As ServCenterMethRepair
        Dim inClause As String = "(-1"
        For Each scMrBO In ServiceCenterMethoOfRepairsChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, scMrBO.ServCenterMorId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function
    Public ReadOnly Property ServiceCenterMethoOfRepairsChildren As ServCenterMethRepairList
        Get
            Return New ServCenterMethRepairList(Me)
        End Get
    End Property
    Public Function GetSelectedMethodOfRepair() As DataView

        Dim dv As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Dim sequenceCondition As String = GetMethodOfRepairsLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function
    'Public Sub DetachMethodOfRepair(ByVal selectedMethodGuidStrCollection As ArrayList)
    '    Dim scDlrIdStr As String
    '    For Each scDlrIdStr In selectedMethodGuidStrCollection
    '        Dim scDlrBO As ServCenterMethRepair = Me.ServiceCenterMethoOfRepairsChildren.Find(New Guid(scDlrIdStr))
    '        scDlrBO.Delete()
    '        scDlrBO.Save()
    '        MethodOfRepairCount = MethodOfRepairCount - 1
    '    Next
    'End Sub

    Public Sub AttachMethodOfRepair(ByVal selectedMethodRepairGuidStrCollection As ArrayList)
        Dim snMorIdStr As String
        For Each snMorIdStr In selectedMethodRepairGuidStrCollection
            Dim snMorBO As ServCenterMethRepair = ServiceCenterMethoOfRepairsChildren.GetNewChild
            snMorBO.ServCenterMorId = New Guid(snMorIdStr)
            snMorBO.ServiceCenterId = Id
            snMorBO.ServiceWarrantyDays = 0
            snMorBO.Save()
            MethodOfRepairCount = MethodOfRepairCount + 1
        Next
    End Sub

    Public Sub DeletAllMethodOfRepairs()
        Dim objSCMR As ServCenterMethRepair
        Dim SCMRList As New ServCenterMethRepairList(Me)

        For Each objSCMR In SCMRList
            objSCMR.Delete()
            objSCMR.Save()
            MethodOfRepairCount = MethodOfRepairCount - 1
        Next
    End Sub
    'Public Function GetSelectedMethodOfRepairList() As Collections.Generic.List(Of ServCenterMethRepair)

    '    Dim MorList As New Collections.Generic.List(Of ServCenterMethRepair)
    '    Dim scMrBO As ServCenterMethRepair
    '    For Each scMrBO In Me.ServiceCenterMethoOfRepairsChildren
    '        MorList.Add(scMrBO)
    '    Next
    '    Return MorList
    'End Function
#End Region

#Region "Address"

    Private _address As Address = Nothing
    Public ReadOnly Property Address As Address
        Get
            If _address Is Nothing Then
                If AddressId.Equals(Guid.Empty) Then
                    _address = New Address(Dataset, Nothing)
                    'Me._address.CountryId = Me.CountryId
                    AddressId = _address.Id
                Else
                    _address = New Address(AddressId, Dataset, Nothing)
                End If
            End If
            _address.CountryId = CountryId
            Return _address
        End Get
    End Property

#End Region

#Region "BankInfo"

    Private _bankinfo As BankInfo = Nothing
    'Public ReadOnly Property bankinfo() As BankInfo
    '    Get
    '        'If Me._bankinfo Is Nothing Then
    '        '    If Me.BankInfoId.Equals(Guid.Empty) Then
    '        '        Me._bankinfo = New BankInfo(Me.Dataset)
    '        '        Me.BankInfoId = Me._bankinfo.Id
    '        '    Else
    '        '        Me._bankinfo = New BankInfo(Me.BankInfoId, Me.Dataset)
    '        '    End If
    '        'End If
    '        Return Me._bankinfo
    '    End Get
    'End Property

    Public ReadOnly Property CurrentBankInfo As BankInfo
        Get
            Return _bankinfo
        End Get
    End Property

    Public Function Add_BankInfo() As BankInfo

        If BankInfoId.Equals(Guid.Empty) Then
            _bankinfo = New BankInfo(Dataset)
            'default new Bank Info country to the Service Center's country
            _bankinfo.CountryID = Address.CountryId
        Else
            _bankinfo = New BankInfo(BankInfoId, Dataset)
        End If
        Return _bankinfo
    End Function

#End Region

#Region "Loaner Center"
    Private _loanerCenter As ServiceCenter = Nothing

    Public ReadOnly Property LoanerCenter As ServiceCenter
        Get
            If HasLoanerCenter Then
                If _loanerCenter Is Nothing Then
                    _loanerCenter = New ServiceCenter(LoanerCenterId)
                End If
            End If
            Return _loanerCenter
        End Get
    End Property
#End Region



#Region "ServiceCenter-PL"


    Public Class SVCPlReconSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_SVC_PL_SERVICE_CENTER_ID As String = SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_SVC_PL_PRICE_LIST_ID As String = SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID
        Public Const COL_SVC_PL_STATUS_XCD As String = SvcPriceListReconDAL.COL_NAME_STATUS_XCD
        Public Const COL_SVC_PL_STATUS_DATE As String = SvcPriceListReconDAL.COL_NAME_STATUS_DATE
        Public Const COL_SVC_PL_REQUESTED_BY As String = SvcPriceListReconDAL.COL_NAME_REQUESTED_BY
        Public Const COL_SVC_PL_REQUESTED_DATE As String = SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE
        Public Const COL_SVC_PL_RECON_ID As String = SvcPriceListReconDAL.TABLE_KEY_NAME

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub


        Public Shared ReadOnly Property Id(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_SVC_PL_RECON_ID), Byte()))
            End Get
        End Property
        Public Shared ReadOnly Property ServiceCenterId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_SVC_PL_SERVICE_CENTER_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property PriceListId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_SVC_PL_PRICE_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Status_Xcd(ByVal row As DataRow) As String
            Get
                Return row(COL_SVC_PL_STATUS_XCD).ToString
            End Get
        End Property

        Public Shared ReadOnly Property RequestedBy(ByVal row As DataRow) As String
            Get
                Return row(COL_SVC_PL_REQUESTED_BY).ToString
            End Get
        End Property

    End Class


    Private _SvcPriceListRecon As SvcPriceListRecon = Nothing

    Public Property CurrentSVCPLRecon As SvcPriceListRecon

        Get
            Dim dv As SVCPlReconSearchDV = GetLatestRecbySVC()
            If dv.Table.Rows.Count = 1 Then
                _SvcPriceListRecon = New SvcPriceListRecon(SVCPlReconSearchDV.Id(dv.Table.Rows(0)), Dataset)
                Return _SvcPriceListRecon
            End If
            Return _SvcPriceListRecon

        End Get
        Set
            _SvcPriceListRecon = value
        End Set
    End Property

    Public Function Add_SVCPLRecon() As SvcPriceListRecon
        _SvcPriceListRecon = New SvcPriceListRecon(Dataset)
        _SvcPriceListRecon.ServiceCenterId = Id
        Return _SvcPriceListRecon
    End Function

    Public Function GetLatestRecbySVC() As SVCPlReconSearchDV
        Try

            Return New SVCPlReconSearchDV(SvcPriceListRecon.LoadLatestStatusList(Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#End Region

#Region "DataView Retrieveing Methods"
    'Manually added method
    'If code, description, address, city and zip are empty, it will return all ServiceCenters for the specified Company
    Public Shared Function getList(ByVal code As String, ByVal description As String,
                                   ByVal address As String, ByVal city As String,
                                   ByVal zip As String,
                                   ByVal oCountryId As Guid) As ServiceCenterSearchDV
        Try
            Dim dal As New ServiceCenterDAL
            Dim oCountryIds As ArrayList
            Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(ServiceCenter), Nothing, "Search", Nothing)}

            If DALBase.IsNothing(oCountryId) Then
                ' Get All User Countries
                oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            Else
                oCountryIds = New ArrayList
                oCountryIds.Add(oCountryId)
            End If

            If (code.Equals(String.Empty) AndAlso description.Equals(String.Empty) AndAlso
                address.Equals(String.Empty) AndAlso city.Equals(String.Empty) AndAlso
                zip.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(ServiceCenter).FullName)
            End If

            Return New ServiceCenterSearchDV(dal.LoadList(oCountryIds, code, description,
                                                          address, city, zip).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class ServiceCenterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_SERVICE_CENTER_ID As String = ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_COUNTRY_DESC As String = ServiceCenterDAL.COL_NAME_COUNTRY_DESC
        Public Const COL_CODE As String = ServiceCenterDAL.COL_NAME_CODE
        Public Const COL_DESCRIPTION As String = ServiceCenterDAL.COL_NAME_DESCRIPTION
        Public Const COL_ADDRESS As String = "address"
        Public Const COL_CITY As String = ServiceCenterDAL.COL_NAME_CITY
        Public Const COL_ZIP As String = "zip"
        Public Const COL_SERVICE_GROUP_DESC As String = ServiceCenterDAL.COL_NAME_SERVICE_GROUP_DESC
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ServiceCenterId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_SERVICE_CENTER_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_CODE).ToString
            End Get
        End Property

    End Class

#End Region

#Region "Locate Service Center Functions"
    Public Class LocateServiceCenterResultsDv
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_SERVICE_CENTER_ID As String = ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_NAME_DESCRIPTION As String = ServiceCenterDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = ServiceCenterDAL.COL_NAME_CODE
        Public Const COL_NAME_RATING_CODE As String = ServiceCenterDAL.COL_NAME_RATING_CODE
        Public Const COL_NAME_CITY As String = ServiceCenterDAL.COL_NAME_CITY
        Public Const COL_NAME_ADDRESS1 As String = ServiceCenterDAL.COL_NAME_ADDRESS1
        Public Const COL_NAME_ADDRESS2 As String = ServiceCenterDAL.COL_NAME_ADDRESS2
        Public Const COL_NAME_ZIP_LOCATOR As String = ServiceCenterDAL.COL_NAME_ZIP_LOCATOR
        Public Const COL_NAME_DEALER_PREF_FLAG As String = ServiceCenterDAL.COL_NAME_DEALER_PREF_FLAG
        Public Const COL_NAME_MAN_AUTH_FLAG As String = ServiceCenterDAL.COL_NAME_MAN_AUTH_FLAG
        Public Const COL_NAME_COVER_ZIP_CODE_FLAG As String = ServiceCenterDAL.COL_NAME_COVER_ZIP_CODE_FLAG
        Public Const COL_NAME_COVER_ITEM_FLAG As String = ServiceCenterDAL.COL_NAME_COVER_ITEM_FLAG
        Public Const COL_NAME_DEALERS_SVC_FLAG As String = ServiceCenterDAL.COL_NAME_DEALERS_SVC_FLAG
        Public Const COL_NAME_SAME_ZIP_FLAG As String = "SAME_ZIP_FLAG"
        Public Const COL_NAME_SERVICE_TYPE_CODE As String = "SCT_CODE"

        Public Const COL_NAME_RATING_CODE_SORT As String = ServiceCenterDAL.COL_NAME_RATING_CODE + "_SORT"
        Public Const COL_NAME_ZIP_LOCATOR_SORT As String = ServiceCenterDAL.COL_NAME_ZIP_LOCATOR + "_SORT"
        Public Const COL_NAME_CODE_AND_DESC As String = "COL_NAME_CODE_AND_DESC"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Shared Function GetLocateServiceCenterDetails(ByVal serviceCenterIds As ArrayList, ByVal dealerId As Guid, ByVal manufacturerId As Guid) As DataSet
        Dim dal As New ServiceCenterDAL
        Dim ds As DataSet = dal.GetLocateServiceCenterDetails(serviceCenterIds, dealerId, manufacturerId)
        Return ds
    End Function

    Public Shared Function LocateServiceCenterByZip(ByVal dealerId As Guid,
                                                ByVal zipLocator As String,
                                                ByVal riskTypeId As Guid,
                                                ByVal manufacturerId As Guid,
                                                ByVal covTypeCode As String,
                                                ByVal oCountryIds As ArrayList,
                                                ByVal ServiceNetWrkID As Guid,
                                                ByVal isNetwork As Boolean,
                                                ByVal MethodOfRepairId As Guid,
                                                Optional ByVal UseZipDistrict As Boolean = True,
                                                Optional ByVal dealerType As String = "",
                                                Optional ByVal FlagMethodOfRepairRecovery As Boolean = False,
                                                Optional ByVal MethodOfRepairType As String = Nothing,
                                                Optional ByVal blnCheckAcctSetting As Boolean = False) As LocateServiceCenterResultsDv
        Dim dal As New ServiceCenterDAL
        Dim ds As DataSet = dal.LocateServiceCenter(oCountryIds, dealerId, zipLocator, Nothing, riskTypeId, manufacturerId, ServiceNetWrkID, isNetwork, MethodOfRepairId, False, UseZipDistrict, dealerType, FlagMethodOfRepairRecovery, blnCheckAcctSetting)
        Dim dv As New DataView
        Dim firstTime As Boolean = True
        Dim workZipLocatior As String = zipLocator
        dv = FilterView(ds, covTypeCode, zipLocator, , FlagMethodOfRepairRecovery, MethodOfRepairType)

        If Not UseZipDistrict Then
            If dv.Count > DV_NO_ROWS_FOUND Then
                firstTime = False
            End If
            If dv.Count < DV_ROWS_LESS_THAT_MIN Then
                If Not zipLocator Is Nothing Then
                    Dim oLength As Integer = zipLocator.Length
                    For I As Integer = 1 To oLength - 1
                        zipLocator = zipLocator.Substring(0, zipLocator.Length - 1) + "%"
                        ds = dal.LocateServiceCenter(oCountryIds, dealerId, zipLocator, Nothing, riskTypeId, manufacturerId, ServiceNetWrkID, isNetwork, MethodOfRepairId, False, UseZipDistrict, dealerType, FlagMethodOfRepairRecovery, blnCheckAcctSetting)
                        If (zipLocator.IndexOf("%") > 0) Then zipLocator = zipLocator.Replace("%", "*")
                        dv = FilterView(ds, covTypeCode, zipLocator, , FlagMethodOfRepairRecovery, MethodOfRepairType)
                        If dv.Count > DV_ROWS_LESS_THAT_MIN - 1 Then
                            Exit For
                        End If
                        If dv.Count > DV_NO_ROWS_FOUND And dv.Count < DV_ROWS_LESS_THAT_MIN Then
                            If Not firstTime Then
                                Exit For
                            End If
                            firstTime = False
                        End If
                        zipLocator = zipLocator.Substring(0, zipLocator.Length - 1)
                    Next
                End If
            End If

            If Not dv.Count > 0 Then
                ds = dal.LocateServiceCenter(oCountryIds, dealerId, workZipLocatior, "*", riskTypeId, manufacturerId, ServiceNetWrkID, isNetwork, MethodOfRepairId, False, , dealerType, FlagMethodOfRepairRecovery, blnCheckAcctSetting)
                dv = FilterView(ds, covTypeCode, workZipLocatior, , FlagMethodOfRepairRecovery, MethodOfRepairType)
            End If
        End If

        dv.Sort = GetLocateServiceCenterResultsSortExp()
        Return dv
    End Function
    Public Shared Function FilterView(ByVal ds As DataSet, ByVal covTypeCode As String, ByVal zipLocator As String, Optional ByVal filterByZyp As Boolean = True, Optional ByVal FlagMethodOfRepairRecovery As Boolean = False, Optional ByVal MethodOfRepairType As String = Nothing) As DataView
        AddSpecialColumns(ds.Tables(0), zipLocator)
        Dim dv As New LocateServiceCenterResultsDv(ds.Tables(0))

        'no need to filter out by zip if for General expense/Legal expense/recovery
        If FlagMethodOfRepairRecovery = False Then
            If filterByZyp Then
                dv.RowFilter = dv.COL_NAME_COVER_ITEM_FLAG & " = 'y' AND " & dv.COL_NAME_COVER_ZIP_CODE_FLAG & " = 'y'"
            Else
                dv.RowFilter = dv.COL_NAME_COVER_ITEM_FLAG & " = 'y' "
            End If
            If covTypeCode.ToUpper = "M" Then 'Manufacturer
                dv.RowFilter &= "AND " & dv.COL_NAME_MAN_AUTH_FLAG & " = 'y'"
            End If
        End If
        dv.Sort = GetLocateServiceCenterResultsSortExp()
        Return dv
    End Function
    Public Shared Function LocateServiceCenterByCity(ByVal dealerId As Guid,
                                            ByVal zipLocator As String,
                                            ByVal city As String,
                                            ByVal riskTypeId As Guid,
                                            ByVal manufacturerId As Guid,
                                            ByVal covTypeCode As String,
                                            ByVal oCountryIds As ArrayList,
                                            ByVal ServiceNetWrkID As Guid,
                                            ByVal isNetwork As Boolean,
                                             ByVal MethodOfRepairId As Guid,
                                            Optional ByVal dealerType As String = "",
                                            Optional ByVal FlagMethodOfRepairRecovery As Boolean = False,
                                            Optional ByVal MethodOfRepairType As String = Nothing,
                                            Optional ByVal blnCheckAcctSetting As Boolean = False) As LocateServiceCenterResultsDv
        Dim dal As New ServiceCenterDAL
        Dim dv As New DataView
        Dim ds As DataSet = dal.LocateServiceCenter(oCountryIds, dealerId, zipLocator, city, riskTypeId, manufacturerId, ServiceNetWrkID, isNetwork, MethodOfRepairId, True, , dealerType, FlagMethodOfRepairRecovery, blnCheckAcctSetting)
        dv = FilterView(ds, covTypeCode, zipLocator, False, FlagMethodOfRepairRecovery, MethodOfRepairType)
        Return dv
    End Function
    Public Shared Function GetAllServiceCenter(ByVal oCountryIds As ArrayList, ByVal MethodOfRepairId As Guid,
                                               Optional ByVal blnCheckAcctSetting As Boolean = False) As DataView
        Dim MethodOfRepairType As String = LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, MethodOfRepairId)
        Dim blnMethodOfRepairRLG As Boolean = False
        If MethodOfRepairType = Codes.METHOD_OF_REPAIR__RECOVERY Or
               MethodOfRepairType = Codes.METHOD_OF_REPAIR__GENERAL Or
               MethodOfRepairType = Codes.METHOD_OF_REPAIR__LEGAL Then
            blnMethodOfRepairRLG = True
        End If
        Dim dal As New ServiceCenterDAL
        Dim dv As DataView = dal.LoadAllServiceCenter(oCountryIds, blnMethodOfRepairRLG, MethodOfRepairId, blnCheckAcctSetting).Tables(0).DefaultView
        Return dv
    End Function

    Public Shared Function GetServiceCenterForCountry(ByVal countryId As Guid) As DataView
        Dim dal As New ServiceCenterDAL
        Dim dv As DataView = dal.LoadServiceCenterForCountry(countryId).Tables(0).DefaultView
        Return dv
    End Function

    Public Shared Function GetServiceCenterForWS(ByVal ServiceCenterCode As String, ByVal oCountryId As Guid) As DataSet
        Dim dal As New ServiceCenterDAL
        Return dal.GetServiceCenterForWS(ServiceCenterCode, oCountryId)

    End Function
    Private Shared Function GetLocateServiceCenterResultsSortExp() As String
        Return LocateServiceCenterResultsDv.COL_NAME_DEALERS_SVC_FLAG & " DESC, " &
                LocateServiceCenterResultsDv.COL_NAME_SAME_ZIP_FLAG & " DESC, " &
                LocateServiceCenterResultsDv.COL_NAME_RATING_CODE_SORT & ", " &
                LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR_SORT & ", " &
                LocateServiceCenterResultsDv.COL_NAME_DEALER_PREF_FLAG & " DESC, " &
                LocateServiceCenterResultsDv.COL_NAME_MAN_AUTH_FLAG & " DESC"
    End Function

    Private Shared Sub AddSpecialColumns(ByVal table As DataTable, ByVal zipLocator As String)
        table.Columns.Add(LocateServiceCenterResultsDv.COL_NAME_SAME_ZIP_FLAG, GetType(String))
        Dim row As DataRow
        For Each row In table.Rows
            row(LocateServiceCenterResultsDv.COL_NAME_SAME_ZIP_FLAG) = "n"
            If Not zipLocator Is Nothing AndAlso Not row(LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR) Is DBNull.Value Then
                If CType(row(LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR), String).ToLower = zipLocator.ToLower Then
                    row(LocateServiceCenterResultsDv.COL_NAME_SAME_ZIP_FLAG) = "y"
                End If
            End If
        Next
        table.Columns.Add(LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR_SORT, GetType(String), "IsNull(" & LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR & ",'ZZZZ')")
        table.Columns.Add(LocateServiceCenterResultsDv.COL_NAME_RATING_CODE_SORT, GetType(String), "IsNull(" & LocateServiceCenterResultsDv.COL_NAME_RATING_CODE & ",'ZZZZ')")
    End Sub


    'Public Shared Function GetServiceTypeFromMethodOfRepair(ByVal oRepairId As Guid, ByVal coverageType As String) As ServiceCenterDAL.General_Service_Type
    '    Dim sRepairCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, oRepairId)
    '    Dim oServiceType As ServiceCenterDAL.General_Service_Type

    '    Select Case sRepairCode
    '        Case Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__SEND_IN, Codes.METHOD_OF_REPAIR__PICK_UP
    '            oServiceType = ServiceCenterDAL.General_Service_Type.Repair
    '        Case Codes.METHOD_OF_REPAIR__REPLACEMENT
    '            oServiceType = ServiceCenterDAL.General_Service_Type.Replacement
    '        Case Codes.METHOD_OF_REPAIR__LEGAL
    '            oServiceType = ServiceCenterDAL.General_Service_Type.Legal
    '        Case Codes.METHOD_OF_REPAIR__GENERAL
    '            oServiceType = ServiceCenterDAL.General_Service_Type.General
    '        Case Codes.METHOD_OF_REPAIR__AUTOMOTIVE
    '            oServiceType = ServiceCenterDAL.General_Service_Type.Automotive
    '        Case Codes.METHOD_OF_REPAIR__RECOVERY
    '            oServiceType = ServiceCenterDAL.General_Service_Type.Recovery
    '    End Select

    '    If coverageType = Codes.COVERAGE_TYPE__THEFTLOSS Then
    '        oServiceType = ServiceCenterDAL.General_Service_Type.Replacement
    '    End If
    '    Return oServiceType
    'End Function


    Public Shared Function getUserCustCountries(ByVal CountryId As Guid) As ServiceCenterSearchDV
        Try
            Dim dal As New ServiceCenterDAL
            Dim UserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Return New ServiceCenterSearchDV(dal.GetUserCustCountries(UserId, CountryId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetServiceCenterbyServiceNetwork(ByVal oSVCNetwotkId As Guid, ByVal CountryId As Guid) As DataView
        Dim dal As New ServiceCenterDAL
        Dim dv As DataView = dal.LoadSVCentersBySVNetwork(oSVCNetwotkId, CountryId).Tables(0).DefaultView
        Return dv
    End Function
#End Region

#Region "GVS"
    Private Sub IntegratedWithGVSValidation()
        Dim dvIntegratedWith As DataView = LookupListNew.GetIntegratedWithLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Dim integratedWithCode As String = LookupListNew.GetCodeFromId(dvIntegratedWith, IntegratedWithID)

        If Not integratedWithCode Is Nothing AndAlso integratedWithCode = Codes.INTEGRATED_WITH_GVS _
            AndAlso CheckColumnChanged(ServiceCenterDAL.COL_NAME_INTEGRATED_WITH_ID) Then
            ' Once turn on GVS integration, the following actions will take place:
            ' 1. Integrated With will be saved as Awaiting GVS
            ' 2. Status will be saved as C
            ' 3. Call GVS web service and send the service center integration information in the back-end process
            IntegratedWithID = LookupListNew.GetIdFromCode(dvIntegratedWith, Codes.INTEGRATED_WITH_AGVS)
            StatusCode = Codes.CLAIM_STATUS__CLOSED

            ' Log the GVS integration into the transaction log header
            Dim logHeader As TransactionLogHeader = New TransactionLogHeader(Dataset)
            logHeader.KeyID = Id
            logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_SVC)
            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            logHeader.TransactionXml = GetTransactionXML(logHeader.Id)
        ElseIf Not integratedWithCode Is Nothing AndAlso integratedWithCode = Codes.INTEGRATED_WITH_GVS _
        AndAlso CheckColumnChanged(ServiceCenterDAL.COL_NAME_REVERSE_LOGISTICS_ID) Then
            ' Log the GVS integration into the transaction log header
            Dim logHeader As TransactionLogHeader = New TransactionLogHeader(Dataset)
            logHeader.KeyID = Id
            logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_SVC)
            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            logHeader.TransactionXml = GetTransactionXML(logHeader.Id)
        End If
    End Sub

    Private Function GetTransactionXML(ByVal transactionId As Guid) As String
        Dim xml As String = ""
        Dim itemNumber As String = "1"
        Dim ds As DataSet = New DataSet("GVSUpdateSvc")

        Dim dtHeader As DataTable = New DataTable("TRANSACTION_HEADER")
        dtHeader.Columns.Add("TRANSACTION_ID", GetType(String))
        dtHeader.Columns.Add("FUNCTION_TYPE_CODE", GetType(String))
        Dim rwHeader As DataRow = dtHeader.NewRow
        rwHeader(0) = GuidControl.GuidToHexString(transactionId)
        rwHeader(1) = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_SVC
        dtHeader.Rows.Add(rwHeader)
        ds.Tables.Add(dtHeader)

        Dim dt As DataTable = New DataTable("TRANSACTION_DATA_RECORD")
        dt.Columns.Add("ITEM_NUMBER", GetType(String))
        dt.Columns.Add("SERVICE_CENTER_STATUS_CODE", GetType(String))
        dt.Columns.Add("SERVICE_CENTER_DATA_XML", GetType(String))

        Dim rw As DataRow = dt.NewRow
        rw("ITEM_NUMBER") = itemNumber
        rw("SERVICE_CENTER_STATUS_CODE") = Codes.CLAIM_STATUS__ACTIVE
        rw("SERVICE_CENTER_DATA_XML") = "TO_BE_GENERATED_IN_DB_BY_TRIGGER"

        dt.Rows.Add(rw)
        ds.Tables.Add(dt)

        xml = XMLHelper.FromDatasetToXML(ds, Nothing, False, False, Nothing, False, True)
        xml = "<GVSUpdateSvcDs>" & xml & "</GVSUpdateSvcDs>"
        Return xml

    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidEmail
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)

            If obj.DefaultToEmailFlag = True AndAlso ((obj.Email Is Nothing) OrElse obj.Email.Equals(String.Empty)) Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)

            If obj.Email Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.Email)

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CcEmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)

            If obj.CcEmail Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.CcEmail)

        End Function

    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)
            'If obj.Shipping AndAlso (Not obj.ProcessingFee Is Nothing AndAlso obj.ProcessingFee.Value <= 0) Then
            If obj.Shipping AndAlso (Not obj.ProcessingFee Is Nothing AndAlso obj.ProcessingFee.Value < 0) Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)
            If obj.Shipping AndAlso obj.ProcessingFee Is Nothing Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMethodOfRepair
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_METHOD_OF_REPAIR_MUST_BE_SELECTED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)

            If obj.MethodOfRepairCount <= 0 Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class
    'START  DEF-2818
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidPriceListCode
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PRICELISTCODE_IS_EXPIRED)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As ServiceCenter = CType(context, ServiceCenter)
            Dim Statusdv As DataView = LookupListNew.GetPriceList(obj.CountryId)

            Dim strPriceListCode As String = obj.PriceListCode
            Dim selectedPricelistID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PRICE_LIST, strPriceListCode)
            'check if the price list code is selected and is not in the active price list codes
            If Not String.IsNullOrEmpty(strPriceListCode) And selectedPricelistID = Guid.Empty Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class
    'END    DEF-2818


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RequiredConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)

            If Not obj Is Nothing Then
                If Not String.IsNullOrWhiteSpace(obj.ClaimReservedBasedOnXcd) AndAlso obj.ClaimReservedPercent Is Nothing Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

#End Region

    Shared Function LoadSelectedServiceCenter(ByVal SCId As Guid, ByVal DealerId As Guid, ByVal RisktypeId As Guid, ByVal ZipLocator As String, ByVal manf_id As Guid) As LocateServiceCenterResultsDv
        Dim ds As New DataSet, dal As New ServiceCenterDAL
        ds = dal.LocateServiceCenterbyId(DealerId, ZipLocator, RisktypeId, manf_id, Guid.Empty, SCId)

        Return FilterView(ds, "", "")
    End Function

#Region "VendorManagement"

#Region "Service Center Detail View"
    Public Class ServiceCenterDetailView
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As PriceList)
            MyBase.New(LoadTable(parent), GetType(ServiceCenter), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ServiceCenter).PriceListCode.Trim().Equals(CType(Parent, PriceList).Code.Trim())
        End Function

        Private Shared Function LoadTable(ByVal parent As PriceList) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ServiceCenterDetailView)) Then
                    Dim dal As New ServiceCenterDAL

                    dal.LoadPriceListServiceCenters(parent.Dataset, parent.Code.ToString())

                    parent.AddChildrenCollection(GetType(ServiceCenterDetailView))
                End If
                Return parent.Dataset.Tables(ServiceCenterDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

#End Region

#Region "Contacts"

#Region "Vendor_Contact"

    Public Function AddVendorContact(ByVal VendorContactID As Guid) As VendorContact
        If VendorContactID.Equals(Guid.Empty) Then
            Dim objVendorContact As New VendorContact(Dataset)
            Return objVendorContact
        Else
            Dim objVendorContact As New VendorContact(VendorContactID, Dataset)
            Return objVendorContact
        End If
    End Function

    Public Class ContactsView
        Inherits DataView
        Public Const COL_NAME_ID As String = VendorContactDAL.COL_NAME_ID
        Public Const COL_NAME_CODE As String = VendorContactDAL.COL_NAME_CODE
        Public Const COL_NAME_NAME As String = VendorContactDAL.COL_NAME_NAME
        Public Const COL_NAME_JOB_TITLE As String = VendorContactDAL.COL_NAME_JOB_TITLE
        Public Const COL_NAME_COMPANY As String = VendorContactDAL.COL_NAME_COMPANY
        Public Const COL_NAME_EMAIL As String = VendorContactDAL.COL_NAME_EMAIL
        Public Const COL_NAME_ADDRESS_TYPE_ID As String = VendorContactDAL.COL_NAME_ADDRESS_TYPE_ID
        Public Const COL_NAME_EFFECTIVE_DATE As String = VendorContactDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION_DATE As String = VendorContactDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_SERVICE_CENTER_ID As String = VendorContactDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_NAME_VENDOR_CONTACT_ID As String = VendorContactDAL.COL_NAME_VENDOR_CONTACT_ID

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CENTER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME, GetType(String))
            t.Columns.Add(COL_NAME_JOB_TITLE, GetType(String))
            t.Columns.Add(COL_NAME_COMPANY, GetType(String))
            t.Columns.Add(COL_NAME_EMAIL, GetType(String))
            t.Columns.Add(COL_NAME_ADDRESS_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EFFECTIVE_DATE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION_DATE, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetContactsSelectionView() As ContactsView
        Dim t As DataTable = ContactsView.CreateTable
        Dim detail As VendorContact
        For Each detail In ContactsChildren
            Dim row As DataRow = t.NewRow
            row(ContactsView.COL_NAME_ID) = detail.Id.ToByteArray
            row(ContactsView.COL_NAME_SERVICE_CENTER_ID) = detail.ServiceCenterId.ToByteArray
            row(ContactsView.COL_NAME_NAME) = detail.Name
            row(ContactsView.COL_NAME_JOB_TITLE) = detail.JobTitle
            row(ContactsView.COL_NAME_COMPANY) = detail.Company
            row(ContactsView.COL_NAME_EMAIL) = detail.Email
            row(ContactsView.COL_NAME_ADDRESS_TYPE_ID) = detail.AddressTypeId.ToByteArray
            row(ContactsView.COL_NAME_EFFECTIVE_DATE) = detail.Effective
            row(ContactsView.COL_NAME_EXPIRATION_DATE) = detail.Expiration
            t.Rows.Add(row)
        Next
        Return New ContactsView(t)
    End Function



    Public Function GetContactsView(ByVal ServiceCenterId As Guid) As DataView
        Dim dal As New ServiceCenterDAL
        Return New DataView(dal.GetContactsView(ServiceCenterId).Tables(0))
    End Function

    Public ReadOnly Property ContactsChildren As IsContactChildrenList
        Get
            Return New IsContactChildrenList(Me)
        End Get
    End Property

    Public Function GeChildContacts(ByVal childId As Guid) As VendorContact
        Return CType(ContactsChildren.GetChild(childId), VendorContact)
    End Function

    Public Function GetNewChildContacts() As VendorContact
        Dim NewContactsList As VendorContact = CType(ContactsChildren.GetNewChild, VendorContact)
        NewContactsList.ServiceCenterId = Id
        Return NewContactsList
    End Function

#End Region

#Region "Contact_Info"

    Public Function AddContactInfo(ByVal ContactInfoID As Guid) As ContactInfo
        If ContactInfoID.Equals(Guid.Empty) Then
            Dim objContactInfo As New ContactInfo(Dataset)
            Return objContactInfo
        Else
            Dim objContactInfo As New ContactInfo(ContactInfoID, Dataset)
            Return objContactInfo
        End If
    End Function

    Public Class ContactInfoView
        Inherits DataView
        Public Const COL_NAME_ID As String = ContactInfoDAL.COL_NAME_ID
        Public Const COL_NAME_ADDRESS_TYPE_ID As String = ContactInfoDAL.COL_NAME_ADDRESS_TYPE_ID
        Public Const COL_NAME_ADDRESS_ID As String = ContactInfoDAL.COL_NAME_ADDRESS_ID
        Public Const COL_NAME_SALUTATION_ID As String = ContactInfoDAL.COL_NAME_SALUTATION_ID
        Public Const COL_NAME_NAME As String = ContactInfoDAL.COL_NAME_NAME
        Public Const COL_NAME_HOME_PHONE As String = ContactInfoDAL.COL_NAME_HOME_PHONE
        Public Const COL_NAME_WORK_PHONE As String = ContactInfoDAL.COL_NAME_WORK_PHONE
        Public Const COL_NAME_CELL_PHONE As String = ContactInfoDAL.COL_NAME_CELL_PHONE
        Public Const COL_NAME_EMAIL As String = ContactInfoDAL.COL_NAME_EMAIL
        Public Const COL_NAME_JOB_TITLE As String = ContactInfoDAL.COL_NAME_JOB_TITLE
        Public Const COL_NAME_COMPANY As String = ContactInfoDAL.COL_NAME_COMPANY

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ADDRESS_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ADDRESS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SALUTATION_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME, GetType(String))
            t.Columns.Add(COL_NAME_HOME_PHONE, GetType(String))
            t.Columns.Add(COL_NAME_WORK_PHONE, GetType(Byte()))
            t.Columns.Add(COL_NAME_CELL_PHONE, GetType(String))
            t.Columns.Add(COL_NAME_EMAIL, GetType(String))
            t.Columns.Add(COL_NAME_JOB_TITLE, GetType(String))
            t.Columns.Add(COL_NAME_COMPANY, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetContactInfoSelectionView() As ContactInfoView
        Dim t As DataTable = ContactsView.CreateTable
        Dim detail As ContactInfo
        For Each detail In ContactsChildren
            Dim row As DataRow = t.NewRow
            row(ContactInfoView.COL_NAME_ID) = detail.Id.ToByteArray
            row(ContactInfoView.COL_NAME_ADDRESS_TYPE_ID) = detail.AddressTypeId.ToByteArray
            row(ContactInfoView.COL_NAME_ADDRESS_ID) = detail.AddressId.ToByteArray
            row(ContactInfoView.COL_NAME_SALUTATION_ID) = detail.SalutationId.ToByteArray
            row(ContactInfoView.COL_NAME_NAME) = detail.Name
            row(ContactInfoView.COL_NAME_HOME_PHONE) = detail.HomePhone
            row(ContactInfoView.COL_NAME_WORK_PHONE) = detail.WorkPhone
            row(ContactInfoView.COL_NAME_CELL_PHONE) = detail.CellPhone
            row(ContactInfoView.COL_NAME_EMAIL) = detail.Email
            row(ContactInfoView.COL_NAME_JOB_TITLE) = detail.JobTitle
            row(ContactInfoView.COL_NAME_COMPANY) = detail.Company
            t.Rows.Add(row)
        Next
        Return New ContactInfoView(t)
    End Function

    Public Function GetContactInfoView(ByVal ContctInfoId As Guid) As DataView
        Dim dal As New VendorContactDAL
        Return New DataView(dal.GetContactInfoView(ContctInfoId).Tables(0))
    End Function

    Public ReadOnly Property ContactInfoChildren(ByVal obj As VendorContact) As IsContactInfoChildrenList
        Get
            Return New IsContactInfoChildrenList(obj)
        End Get
    End Property

    Public Function GeChildContactInfo(ByVal obj As VendorContact, ByVal childId As Guid) As ContactInfo
        Return CType(ContactInfoChildren(obj).GetChild(childId), ContactInfo)
    End Function

    Public Function GetNewChildContactInfo(ByVal obj As VendorContact) As ContactInfo
        Dim NewContactInfoList As ContactInfo = CType(ContactInfoChildren(obj).GetNewChild, ContactInfo)
        obj.ContactInfoId = NewContactInfoList.Id
        Return NewContactInfoList
    End Function
#End Region

#Region "Address"

    Public Function AddAddress(ByVal AddressID As Guid) As Address
        If AddressID.Equals(Guid.Empty) Then
            Dim objAddress As New Address(Dataset)
            Return objAddress
        Else
            Dim objAddress As New Address(AddressID, Dataset)
            Return objAddress
        End If
    End Function

    Public Class AddressView
        Inherits DataView
        Public Const COL_NAME_ID As String = AddressDAL.COL_NAME_ID
        Public Const COL_NAME_ADDRESS1 As String = AddressDAL.COL_NAME_ADDRESS1
        Public Const COL_NAME_ADDRESS2 As String = AddressDAL.COL_NAME_ADDRESS2
        Public Const COL_NAME_CITY As String = AddressDAL.COL_NAME_CITY
        Public Const COL_NAME_REGION_ID As String = AddressDAL.COL_NAME_REGION_ID
        Public Const COL_NAME_POSTAL_CODE As String = AddressDAL.COL_NAME_POSTAL_CODE
        Public Const COL_NAME_COUNTRY_ID As String = AddressDAL.COL_NAME_COUNTRY_ID
        Public Const COL_NAME_ZIP_LOCATOR As String = AddressDAL.COL_NAME_ZIP_LOCATOR
        Public Const COL_NAME_ADDRESS3 As String = AddressDAL.COL_NAME_ADDRESS3

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ADDRESS1, GetType(Byte()))
            t.Columns.Add(COL_NAME_ADDRESS2, GetType(Byte()))
            t.Columns.Add(COL_NAME_CITY, GetType(Byte()))
            t.Columns.Add(COL_NAME_REGION_ID, GetType(String))
            t.Columns.Add(COL_NAME_POSTAL_CODE, GetType(String))
            t.Columns.Add(COL_NAME_COUNTRY_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ZIP_LOCATOR, GetType(String))
            t.Columns.Add(COL_NAME_ADDRESS3, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetAddressSelectionView() As AddressView
        Dim t As DataTable = AddressView.CreateTable
        Dim detail As Address
        For Each detail In ContactsChildren
            Dim row As DataRow = t.NewRow
            row(AddressView.COL_NAME_ID) = detail.Id.ToByteArray
            row(AddressView.COL_NAME_ADDRESS1) = detail.Address1
            row(AddressView.COL_NAME_ADDRESS2) = detail.Address2
            row(AddressView.COL_NAME_CITY) = detail.City
            row(AddressView.COL_NAME_REGION_ID) = detail.RegionId
            row(AddressView.COL_NAME_POSTAL_CODE) = detail.PostalCode
            row(AddressView.COL_NAME_COUNTRY_ID) = detail.CountryId.ToByteArray
            row(AddressView.COL_NAME_ZIP_LOCATOR) = detail.ZipLocator
            row(AddressView.COL_NAME_ADDRESS3) = detail.Address3
            t.Rows.Add(row)
        Next
        Return New AddressView(t)
    End Function

    Public Function GetAddressView(ByVal AddressId As Guid) As DataView
        Dim dal As New ContactInfoDAL
        Return New DataView(dal.GetAddressView(AddressId).Tables(0))
    End Function

    Public ReadOnly Property AddressChildren(ByVal obj As ContactInfo) As IsAddressChildrenList
        Get
            Return New IsAddressChildrenList(obj)
        End Get
    End Property

    Public Function GeChildAddress(ByVal obj As ContactInfo, ByVal childId As Guid) As Address
        Return CType(AddressChildren(obj).GetChild(childId), Address)
    End Function

    Public Function GetNewChildAddress(ByVal obj As ContactInfo) As Address
        Dim NewAddressList As Address = CType(AddressChildren(obj).GetNewChild, Address)
        obj.AddressId = NewAddressList.Id
        Return NewAddressList
    End Function
#End Region

#End Region

#Region "Quantity"

    Public Class QuantityView
        Inherits DataView
        Public Const COL_NAME_ID As String = VendorQuantityDAL.COL_NAME_ID
        Public Const COL_NAME_EQUIPMENT_TYPE_ID As String = VendorQuantityDAL.COL_NAME_EQUIPMENT_TYPE_ID
        Public Const COL_NAME_MANUFACTURER_ID As String = VendorQuantityDAL.COL_NAME_MANUFACTURER_ID
        Public Const COL_NAME_JOB_MODEL As String = VendorQuantityDAL.COL_NAME_JOB_MODEL
        Public Const COL_NAME_REFERENCE_ID As String = VendorQuantityDAL.COL_NAME_REFERENCE_ID
        Public Const COL_NAME_MANUFACTURER_NAME As String = VendorQuantityDAL.COL_NAME_MANUFACTURER_NAME
        Public Const COL_NAME_VENDOR_SKU As String = VendorQuantityDAL.COL_NAME_VENDOR_SKU
        Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = VendorQuantityDAL.COL_NAME_VENDOR_SKU_DESCRIPTION
        Public Const COL_NAME_QUANTITY As String = VendorQuantityDAL.COL_NAME_QUANTITY
        Public Const COL_NAME_PRICE As String = VendorQuantityDAL.COL_NAME_PRICE
        Public Const COL_NAME_CONDITION_ID As String = VendorQuantityDAL.COL_NAME_CONDITION_ID
        Public Const COL_NAME_EFFECTIVE As String = VendorQuantityDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = VendorQuantityDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_PRICE_LIST_DETAIL_ID As String = VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID
        Public Const COL_NAME_VENDOR_QUANTITY_AVALIABLE As String = VendorQuantityDAL.COL_NAME_VENDOR_QUANTITY_AVALIABLE

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EQUIPMENT_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_MANUFACTURER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_REFERENCE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_JOB_MODEL, GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU, GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_QUANTITY, GetType(String))
            t.Columns.Add(COL_NAME_PRICE, GetType(String))
            t.Columns.Add(COL_NAME_CONDITION_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            t.Columns.Add(COL_NAME_MANUFACTURER_NAME, GetType(String))
            t.Columns.Add(COL_NAME_PRICE_LIST_DETAIL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_VENDOR_QUANTITY_AVALIABLE, GetType(Boolean))
            Return t
        End Function
    End Class

    Public Function GetQuantitySelectionView() As QuantityView
        Dim t As DataTable = QuantityView.CreateTable
        Dim detail As VendorQuantity
        For Each detail In QuantityChildren
            Dim row As DataRow = t.NewRow
            'Check if the Vendor_Quantity_id is empty. If yes, then set the vendor quantity id to an valid value
            If detail.Id = Guid.Empty Then
                detail.Id = Guid.NewGuid
            End If
            row(QuantityView.COL_NAME_ID) = detail.Id.ToByteArray
            row(QuantityView.COL_NAME_EQUIPMENT_TYPE_ID) = detail.EquipmentTypeId.ToByteArray
            row(QuantityView.COL_NAME_MANUFACTURER_ID) = detail.ManufacturerId.ToByteArray
            row(QuantityView.COL_NAME_REFERENCE_ID) = detail.ReferenceId.ToByteArray
            row(QuantityView.COL_NAME_JOB_MODEL) = detail.JobModel
            row(QuantityView.COL_NAME_VENDOR_SKU) = detail.Sku
            row(QuantityView.COL_NAME_VENDOR_SKU_DESCRIPTION) = detail.SkuDescription
            row(QuantityView.COL_NAME_QUANTITY) = detail.Quantity
            row(QuantityView.COL_NAME_PRICE) = detail.Price
            row(QuantityView.COL_NAME_CONDITION_ID) = detail.ConditionId.ToByteArray
            row(QuantityView.COL_NAME_EFFECTIVE) = detail.Effective
            row(QuantityView.COL_NAME_EXPIRATION) = detail.Expiration
            row(QuantityView.COL_NAME_MANUFACTURER_NAME) = detail.ManufacturerName
            row(QuantityView.COL_NAME_PRICE_LIST_DETAIL_ID) = detail.PriceListDetailID.ToByteArray
            row(QuantityView.COL_NAME_VENDOR_QUANTITY_AVALIABLE) = detail.VendorQuantityRecordAvaliable
            t.Rows.Add(row)
        Next
        Return New QuantityView(t)
    End Function

    Public Function GetQuantityView(ByVal ServiceCenterId As Guid) As DataView
        Dim dal As New ServiceCenterDAL
        Return New DataView(dal.GetQuantityView(ServiceCenterId).Tables(0))
    End Function

    Public ReadOnly Property QuantityChildren As IsQuantityChildrenList
        Get
            Return New IsQuantityChildrenList(Me)
        End Get
    End Property

    Public Function GeChildQuantity(ByVal childId As Guid) As VendorQuantity
        Return CType(QuantityChildren.GetChild(childId), VendorQuantity)
    End Function

    Public Function GetNewChildQuantity() As VendorQuantity
        Dim NewQuantityList As VendorQuantity = CType(QuantityChildren.GetNewChild, VendorQuantity)
        NewQuantityList.ReferenceId = Id
        Return NewQuantityList
    End Function

#End Region

#Region "Schedule"

#Region "ServiceSchedule"
    Public Class ScheduleView
        Inherits DataView
        Public Const COL_NAME_ID As String = ServiceScheduleDAL.COL_NAME_ID
        Public Const COL_NAME_SERVICE_CLASS_ID As String = ServiceScheduleDAL.COL_NAME_SERVICE_CLASS_ID
        Public Const COL_NAME_SERVICE_TYPE_ID As String = ServiceScheduleDAL.COL_NAME_SERVICE_TYPE_ID
        Public Const COL_NAME_SCHEDULE_ID As String = ServiceScheduleDAL.COL_NAME_SCHEDULE_ID
        Public Const COL_NAME_DAY_OF_WEEK_ID As String = ServiceScheduleDAL.COL_NAME_DAY_OF_WEEK_ID
        Public Const COL_NAME_SERVICE_CENTER_ID As String = ServiceScheduleDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_NAME_FROM_TIME As String = ServiceScheduleDAL.COL_NAME_FROM_TIME
        Public Const COL_NAME_TO_TIME As String = ServiceScheduleDAL.COL_NAME_TO_TIME
        Public Const COL_NAME_EFFECTIVE As String = ServiceScheduleDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = ServiceScheduleDAL.COL_NAME_EXPIRATION

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CLASS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SCHEDULE_ID, GetType(Byte()))
            ' t.Columns.Add(COL_NAME_DAY_OF_WEEK_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CENTER_ID, GetType(Byte()))
            ' t.Columns.Add(COL_NAME_FROM_TIME, GetType(String))
            ' t.Columns.Add(COL_NAME_TO_TIME, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            Return t
        End Function

        Public Shared Function CreateSvcScheduleTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CLASS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SCHEDULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SERVICE_CENTER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            Return t
        End Function

    End Class

    Public Function GetScheduleSelectionView() As ScheduleView
        Dim t As DataTable = ScheduleView.CreateTable
        Dim detail As ServiceSchedule
        For Each detail In scheduleChildren
            Dim row As DataRow = t.NewRow
            row(ScheduleView.COL_NAME_ID) = detail.Id.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_CLASS_ID) = detail.ServiceClassId.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_TYPE_ID) = detail.ServiceTypeId.ToByteArray
            row(ScheduleView.COL_NAME_SCHEDULE_ID) = detail.ScheduleId.ToByteArray
            ' row(ScheduleView.COL_NAME_DAY_OF_WEEK_ID) = detail.DayOfWeekId.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_CENTER_ID) = detail.ServiceCenterId.ToByteArray
            ' row(ScheduleView.COL_NAME_FROM_TIME) = detail.FromTime
            ' row(ScheduleView.COL_NAME_TO_TIME) = detail.ToTime
            row(ScheduleView.COL_NAME_EFFECTIVE) = detail.Effective
            row(ScheduleView.COL_NAME_EXPIRATION) = detail.Expiration
            t.Rows.Add(row)
        Next
        Return New ScheduleView(t)
    End Function

    Public Function GetscheduleView(ByVal ServiceCenterId As Guid) As DataView
        Dim dal As New ServiceCenterDAL
        Return New DataView(dal.GetScheduleView(ServiceCenterId).Tables(0))
    End Function

    Public ReadOnly Property scheduleChildren As IsScheduleChildrenList
        Get
            Return New IsScheduleChildrenList(Me)
        End Get
    End Property

    Public Function GeChildSchedule(ByVal childId As Guid) As ServiceSchedule
        Return CType(scheduleChildren.GetChild(childId), ServiceSchedule)
    End Function

    Public Function GetNewChildSchedule() As ServiceSchedule
        Dim NewScheduleList As ServiceSchedule = CType(scheduleChildren.GetNewChild, ServiceSchedule)
        NewScheduleList.ServiceCenterId = Id
        Return NewScheduleList
    End Function
#End Region

#Region "ScheduleTable"
    Public Class ScheduleTableView
        Inherits DataView
        Public Const COL_NAME_ID As String = ScheduleDAL.COL_NAME_ID
        Public Const COL_NAME_CODE As String = ScheduleDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = ScheduleDAL.COL_NAME_DESCRIPTION

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CODE, GetType(String))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetScheduleTableSelectionView() As ScheduleTableView
        Dim t As DataTable = ScheduleTableView.CreateTable
        Dim detail As Schedule
        For Each detail In scheduleChildren
            Dim row As DataRow = t.NewRow
            row(ScheduleTableView.COL_NAME_ID) = detail.Id.ToByteArray
            row(ScheduleTableView.COL_NAME_CODE) = detail.Code
            row(ScheduleTableView.COL_NAME_DESCRIPTION) = detail.Description
            t.Rows.Add(row)
        Next
        Return New ScheduleTableView(t)
    End Function

    Public Function GetscheduleTableView(ByVal ScheduleId As Guid) As DataView
        Dim dal As New ServiceScheduleDAL
        Return New DataView(dal.GetScheduleTableView(ScheduleId).Tables(0))
    End Function

    Public ReadOnly Property scheduleTableChildren(ByVal obj As ServiceSchedule) As IsScheduleTableChildrenList
        Get
            Return New IsScheduleTableChildrenList(obj)
        End Get
    End Property

    Public Function GeChildScheduleTable(ByVal obj As ServiceSchedule, ByVal childId As Guid) As Schedule
        Return CType(scheduleTableChildren(obj).GetChild(childId), Schedule)
    End Function

    Public Function GetNewChildScheduleTable(ByVal obj As ServiceSchedule) As Schedule
        Dim NewScheduleTableList As Schedule = CType(scheduleTableChildren(obj).GetNewChild, Schedule)
        obj.ScheduleId = NewScheduleTableList.Id
        Return NewScheduleTableList
    End Function
#End Region

#Region "ScheduleDetail"
    Public Class ScheduleDetailView
        Inherits DataView
        Public Const COL_NAME_ID As String = ScheduleDetailDAL.COL_NAME_ID
        Public Const COL_NAME_SCHEDULE_ID As String = ScheduleDetailDAL.COL_NAME_SCHEDULE_ID
        Public Const COL_NAME_DAY_OF_WEEK_ID As String = ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID
        Public Const COL_NAME_FROM_TIME As String = ScheduleDetailDAL.COL_NAME_FROM_TIME
        Public Const COL_NAME_TO_TIME As String = ScheduleDetailDAL.COL_NAME_TO_TIME

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SCHEDULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DAY_OF_WEEK_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_FROM_TIME, GetType(String))
            t.Columns.Add(COL_NAME_TO_TIME, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetScheduleDetailSelectionView() As ScheduleView
        Dim t As DataTable = ScheduleView.CreateTable
        Dim detail As ServiceSchedule
        For Each detail In scheduleChildren
            Dim row As DataRow = t.NewRow
            row(ScheduleView.COL_NAME_ID) = detail.Id.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_CLASS_ID) = detail.ServiceClassId.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_TYPE_ID) = detail.ServiceTypeId.ToByteArray
            row(ScheduleView.COL_NAME_SCHEDULE_ID) = detail.ScheduleId.ToByteArray
            row(ScheduleView.COL_NAME_DAY_OF_WEEK_ID) = detail.DayOfWeekId.ToByteArray
            row(ScheduleView.COL_NAME_SERVICE_CENTER_ID) = detail.ServiceCenterId.ToByteArray
            row(ScheduleView.COL_NAME_FROM_TIME) = detail.FromTime
            row(ScheduleView.COL_NAME_TO_TIME) = detail.ToTime
            row(ScheduleView.COL_NAME_EFFECTIVE) = detail.Effective
            row(ScheduleView.COL_NAME_EXPIRATION) = detail.Expiration
            t.Rows.Add(row)
        Next
        Return New ScheduleView(t)
    End Function

    Public Function GetscheduleDetailView(ByVal ScheduleId As Guid) As DataView
        Dim dal As New ScheduleDAL
        Return New DataView(dal.GetScheduleDetailView(ScheduleId).Tables(0))
    End Function

    Public ReadOnly Property scheduledDtailChildren(ByVal obj As Schedule, ByVal objParent As ServiceSchedule) As IsScheduleDetailChildrenList
        Get
            Return New IsScheduleDetailChildrenList(obj, objParent)
        End Get
    End Property

    Public Function GeChildScheduleDetail(ByVal obj As Schedule, ByVal childId As Guid, ByVal objParent As ServiceSchedule) As ScheduleDetail
        Return CType(scheduledDtailChildren(obj, objParent).GetChildThird(childId, Dataset, "schedule_id"), ScheduleDetail)
    End Function

    Public Function GetNewChildScheduleDetail(ByVal obj As Schedule, ByVal objParent As ServiceSchedule) As ScheduleDetail
        Dim NewScheduleDetailList As ScheduleDetail = CType(scheduledDtailChildren(obj, objParent).GetNewChild, ScheduleDetail)
        NewScheduleDetailList.ScheduleId = obj.Id
        Return NewScheduleDetailList
    End Function

#End Region

#Region "CRUD"

    Public Function GetScheduleDetailCount(ByVal scheduleId As Guid) As Integer
        Dim ScheduleDAL As New ScheduleDAL
        Return ScheduleDAL.GetScheduleDetailCount(scheduleId)
    End Function

    Public Function GetScheduleDetailInfo(ByVal scheduleId As Guid) As DataSet
        Dim ScheduleDAL As New ScheduleDAL
        Return ScheduleDAL.GetScheduleDetailInfo(scheduleId)
    End Function

    Public Function GetScheduleInfo(ByVal scheduleId As Guid) As DataSet
        Dim ScheduleDAL As New ScheduleDAL
        Return ScheduleDAL.GetScheduleInfo(scheduleId)
    End Function
#End Region

#End Region

#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class DiscountPercentInValidRange
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, VENDOR_MANAGEMENT001)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)
            If IsNumeric(obj.DiscountPct) Then
                If CType(obj.DiscountPct, Integer) >= 0 And CType(obj.DiscountPct, Integer) <= 100 Then
                    Return False
                End If
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueIsAnInteger
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, VENDOR_MANAGEMENT002)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ServiceCenter = CType(objectToValidate, ServiceCenter)
            If IsNumeric(obj.DiscountPct) Then
                If CType(obj.DiscountPct, Integer) >= 0 Then
                    Return False
                End If
            Else
                Return True
            End If
        End Function
    End Class

#End Region
#End Region


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateByteLengthAttribute
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Private _minLength As Int32
        Private _maxLength As Int32
        Private _minLengthSet As Int32
        Private _maxLengthSet As Int32

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALID_STRING_LENGTH_ERR)
            _minLength = 0
            _maxLength = 0
            _minLengthSet = False
            _maxLengthSet = False
        End Sub
        Public Property Min As Int32
            Get
                Return _minLength
            End Get
            Set
                _minLength = value
                _minLengthSet = True
            End Set
        End Property

        Public Property Max As Int32
            Get
                Return _maxLength
            End Get
            Set
                _maxLength = value
                _maxLengthSet = True
            End Set
        End Property

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            If (objectToCheck Is Nothing) Then
                Return True
            End If
            Dim retVal As Boolean = True
            Dim CountryID As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId).Id

            If (Not CountryID.Equals(Guid.Empty)) Then
                ' For this Country Get Require Byte Conversion flag value 
                Dim oCountry As Country = New Country()
                Dim dsFlag As DataSet = oCountry.GetCountryByteFlag(CountryID)
                If (Not dsFlag Is Nothing AndAlso dsFlag.Tables.Count > 0 AndAlso dsFlag.Tables(0).Rows.Count > 0) Then
                    Dim RequiresByteConversionId As Guid = New Guid(CType(dsFlag.Tables(CountryDAL.TABLE_NAME).Rows(0)(CountryDAL.COL_NAME_REQUIRE_BYTE_CONV_ID), Byte()))
                    If (RequiresByteConversionId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then ' If Flag = Yes then perform Byte check else not 
                        Dim svcDAL As ServiceCenterDAL = New ServiceCenterDAL()
                        Dim length As Int32 = svcDAL.GetServicecenterDescBytes(objectToCheck.ToString())
                        If _minLengthSet Then
                            If length < _minLength Then
                                retVal = False
                            End If
                        End If
                        If _maxLengthSet Then
                            If length > _maxLength Then
                                retVal = False
                            End If
                        End If
                    End If
                End If
            End If
            Return retVal
        End Function

    End Class


End Class


#Region "Custom Validations"
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
Public NotInheritable Class ValidateByteLengthAttribute
    Inherits ValidBaseAttribute
    Implements IValidatorAttribute
    Private _minLength As Int32
    Private _maxLength As Int32
    Private _minLengthSet As Int32
    Private _maxLengthSet As Int32

    Public Sub New(ByVal fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Messages.VALID_STRING_LENGTH_ERR)
        _minLength = 0
        _maxLength = 0
        _minLengthSet = False
        _maxLengthSet = False
    End Sub
    Public Property Min As Int32
        Get
            Return _minLength
        End Get
        Set
            _minLength = value
            _minLengthSet = True
        End Set
    End Property

    Public Property Max As Int32
        Get
            Return _maxLength
        End Get
        Set
            _maxLength = value
            _maxLengthSet = True
        End Set
    End Property

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
        If (objectToCheck Is Nothing) Then
            Return True
        End If
        Dim retVal As Boolean = True
        Dim CountryID As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId).Id

        If (Not CountryID.Equals(Guid.Empty)) Then
            ' For this Country Get Require Byte Conversion flag value 
            Dim oCountry As Country = New Country()
            Dim dsFlag As DataSet = oCountry.GetCountryByteFlag(CountryID)
            If (Not dsFlag Is Nothing AndAlso dsFlag.Tables.Count > 0 AndAlso dsFlag.Tables(0).Rows.Count > 0) Then
                Dim RequiresByteConversionId As Guid = New Guid(CType(dsFlag.Tables(CountryDAL.TABLE_NAME).Rows(0)(CountryDAL.COL_NAME_REQUIRE_BYTE_CONV_ID), Byte()))
                If (RequiresByteConversionId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then ' If Flag = Yes then perform Byte check else not 
                    Dim svcDAL As ServiceCenterDAL = New ServiceCenterDAL()
                    Dim length As Int32 = svcDAL.GetServicecenterDescBytes(objectToCheck.ToString())
                    If _minLengthSet Then
                        If length < _minLength Then
                            retVal = False
                        End If
                    End If
                    If _maxLengthSet Then
                        If length > _maxLength Then
                            retVal = False
                        End If
                    End If
                End If
            End If
        End If
        Return retVal
    End Function
End Class
#End Region
