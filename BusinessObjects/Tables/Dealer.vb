'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/19/2004)  ********************
Imports System.Collections.Generic

Public Class Dealer
    Inherits BusinessObjectBase
    Implements IAttributable

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal Company_id As Guid, ByVal Dealer As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(Company_id, Dealer)
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
            Dim dal As New DealerDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Dim noGuid As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, "N")
            SetValue(dal.COL_NAME_USE_EQUIPMENT_ID, noGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New DealerDAL
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

    Protected Sub Load(ByVal Company_id As Guid, ByVal Dealer As String)
        Try
            Dim dal As New DealerDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(Company_id, dal.COL_NAME_COMPANY_ID, Dealer, dal.COL_NAME_DEALER, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, Company_id, Dealer)
                Me.Row = Me.FindRow(Company_id, dal.COL_NAME_COMPANY_ID, Dealer, dal.COL_NAME_DEALER, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Address"

    Private _address As Address = Nothing
    Public ReadOnly Property Address() As Address
        Get
            If Me._address Is Nothing Then
                If Me.AddressId.Equals(Guid.Empty) Then
                    Me._address = New Address(Me.Dataset, Nothing)
                    _address.CountryId = Company.BusinessCountryId
                    '   Me.AddressId = Me._address.Id
                Else
                    Me._address = New Address(Me.AddressId, Me.Dataset, Nothing)
                End If
            End If
            Return Me._address
        End Get
    End Property

    Private _MailingAddress As Address = Nothing
    Public ReadOnly Property MailingAddress() As Address
        Get
            If Me._MailingAddress Is Nothing Then
                If Me.MailingAddressId.Equals(Guid.Empty) Then
                    'If Me.IsNew Then
                    Me._MailingAddress = New Address(Me.Dataset, Nothing)
                    _MailingAddress.CountryId = Company.BusinessCountryId
                    '   Me.MailingAddressId = Me._MailingAddress.Id
                    'End If
                Else
                    Me._MailingAddress = New Address(Me.MailingAddressId, Me.Dataset, Nothing)
                End If
            End If
            Return Me._MailingAddress
        End Get
    End Property

    'Private _svcOrdersAddressId As Guid = Guid.Empty
    'Public Property SvcOrdersAddressId() As Guid
    '    Get
    '        Return _svcOrdersAddressId
    '    End Get
    '    Set(ByVal value As Guid)
    '        Me._svcOrdersAddressId = value
    '    End Set
    'End Property

    Private _SvcOrdersAddress As ServiceOrdersAddress = Nothing
    Public ReadOnly Property SvcOrdersAddress() As ServiceOrdersAddress
        Get
            If Me._SvcOrdersAddress Is Nothing Then
                Me._SvcOrdersAddress = New ServiceOrdersAddress(Me.Dataset, Me.Id)
                If Me._SvcOrdersAddress.Row Is Nothing Then
                    Me._SvcOrdersAddress = New ServiceOrdersAddress(Me.Dataset)
                End If
            End If

            Return Me._SvcOrdersAddress

        End Get
    End Property
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        'default the STAT compute mthd to GAAP and that for LAE to NO while creating a new dealer
        Me.STATIBNRComputationMethodId = LookupListNew.GetIdFromCode(LookupListNew.LK_STAT_IBNR_COMPUTE_METHODS, STATCOMPUTMTHD)
        Me.LAEIBNRComputationMethodId = LookupListNew.GetIdFromCode(LookupListNew.LK_LAE_IBNR_COMPUTE_METHODS, LAECOMPUTMTHD)
        Me.CertCancelById = LookupListNew.GetIdFromCode(LookupListNew.LK_CERT_CANCEL_BY, Codes.CCANBY_CERTNO)
        Me.ClaimSystemId = LookupListNew.GetIdFromDescription(LookupListNew.LK_CLAIM_SYSTEM, CLMSYS_ELITA)
        Me.ValidateBillingCycleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.ValidateSerialNumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.UseNewBillForm = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        'REQ-1294
        'Me.CustInfoMandatoryId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.BankInfoMandatoryId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.DeductibleCollectionId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.ClaimExtendedStatusEntryId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EXTENDED_STATUS_ENTRY, USER_SYSTEM_SELECT)

        'New device sku required added for TIMS
        Me.NewDeviceSkuRequiredId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.UseClaimAuthorizationId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Me.ReuseSerialNumberId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
        Me.AutoGenerateRejectedPaymentFileId = LookupListNew.GetIdFromCode(LookupListNew.LK_AUTO_GEN_REJ_PYMT_FILE, Codes.AUOT_GEN_REJ_PYMT_FILE__NONE)
        Me.PaymentRejectedRecordReconcileId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Dim DvClaimRecording As DataView = LookupListNew.DropdownLookupList("CLMREC", Authentication.LangId)
        Me.ClaimRecordingXcd = "CLMREC-" + LookupListNew.GetCodeFromDescription(DvClaimRecording, CLMREC_ELITA)
    End Sub
#End Region

#Region "CONSTANTS"

    Private Const DEALER_NAME_REQUIRED As String = "DEALER_NAME_REQUIRED"
    Private Const MIN_DEALER_NAME_LENGTH As String = "1"
    Private Const MAX_DEALER_NAME_LENGTH As String = "50"

    Private Const MIN_DEALER_CODE_LENGTH As String = "1"
    Private Const MAX_DEALER_CODE_LENGTH As String = "5"
    Private Const DEALER_CODE_REQUIRED As String = "DEALER_CODE_REQUIRED"

    Public Const COL_DEALER_ID As String = "DEALER_ID"
    Public Const COL_COMPANY As String = "COMPANY"
    Public Const COL_DEALER As String = "DEALER_CODE"
    Public Const COL_DEALER_NAME As String = "DEALER_NAME"
    Public Const COL_DEALER_GROUP As String = "DEALER_GROUP"
    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    Private Const DSNAME As String = "LIST"
    Public Const DEALER_TYPE_DESC As String = "VSC"
    Public Const DEALER_TYPE_DESC_WEPP As String = "WEPP"
    Public const DEALER_TYPE_BENEFIT = "BENEFIT"
    Private Const DOC_TYPE As String = "CMPJ"
    Private Const DEALER_IBNRFACTOR As String = "IBNR_FACTOR_DECIMAL" ' Only 6 Digits Allowed After Decimal Point
    Private Const MIM_DECIMAL_NUMBERS As Integer = 6

    Public Const STATCOMPUTMTHD As String = "GAAP"
    Public Const LAECOMPUTMTHD As String = "NO"
    Public Const CLMSYS_ELITA As String = "ELITA"
    Public Const USER_SYSTEM_SELECT As String = "FLEX"
    Public Const CLMREC_ELITA As String = "ELITA"

#End Region

#Region "Variables"
    Private moServiceCenterIDs As ArrayList
    Private moDealerClaimTypes As ArrayList
    Private htDealerClaimTypes As Hashtable
    Private _dealerClaimTypeSelectionCount As Integer
    Private _dealerCoverageTypeSelectionCount As Integer
    Private _company As Company = Nothing
#End Region

#Region "Properties"

    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Me.Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return DealerDAL.TABLE_NAME.ToUpper()
        End Get
    End Property

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(DealerDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
    End Property
    Public ReadOnly Property ServiceCenterID() As Guid
        Get
            If Row(DealerDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=5)>
    Public Property Dealer() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ClientDealerCode() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLIENT_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CLIENT_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLIENT_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property DealerName() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_DEALER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER_NAME, Value)
        End Set
    End Property


    <MandatoryForVscAttribute(""), ValidStringLength("", Max:=15), CNPJ_TaxIdValidation("")>
    Public Property TaxIdNumber() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_TAX_ID_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_TAX_ID_NUMBER, MiscUtil.ConvertToUpper(Value))
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property AddressId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    <MandatoryForVscAttribute(""), ValidStringLength("", Max:=30)>
    Public Property ContactName() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTACT_NAME, Value)
        End Set
    End Property


    <MandatoryForVscAttribute(""), ValidStringLength("", Max:=15)>
    Public Property ContactPhone() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTACT_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTACT_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTACT_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property ContactExt() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTACT_EXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTACT_EXT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTACT_EXT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property ContactFax() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTACT_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTACT_FAX), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTACT_FAX, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property ContactEmail() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTACT_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTACT_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTACT_EMAIL, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RetailerId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_RETAILER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_RETAILER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_RETAILER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReuseSerialNumberId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REUSE_SERIAL_NUMBER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_REUSE_SERIAL_NUMBER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REUSE_SERIAL_NUMBER_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property ActiveFlag() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property

    Public Property ServiceNetworkId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SERVICE_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SERVICE_NETWORK_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DealerTypeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEALER_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER_TYPE_ID, Value)
        End Set
    End Property

    Public Property PayDeductibleId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PAY_DEDUCTIBLE_ID) Is DBNull.Value Then
                Return LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_PAY_DEDUCTIBLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PAY_DEDUCTIBLE_ID, Value)
        End Set
    End Property

    'REQ-1294
    '<ValueMandatory("")> _
    'Public Property CustInfoMandatoryId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(DealerDAL.COL_NAME_CUST_INFO_MANDATORY_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(DealerDAL.COL_NAME_CUST_INFO_MANDATORY_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(DealerDAL.COL_NAME_CUST_INFO_MANDATORY_ID, Value)
    '    End Set
    'End Property

    <ValueMandatory("")>
    Public Property BankInfoMandatoryId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BANK_INFO_MANDATORY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_BANK_INFO_MANDATORY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BANK_INFO_MANDATORY_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property ValidateSKUId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_VALIDATE_SKU_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_VALIDATE_SKU_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_VALIDATE_SKU_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IBNRComputationMethodId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IBNR_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_IBNR_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IBNR_COMPUTE_METHOD_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property STATIBNRComputationMethodId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_STATIBNR_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_STATIBNR_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_STATIBNR_COMPUTE_METHOD_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property LAEIBNRComputationMethodId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_LAEIBNR_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_LAEIBNR_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_LAEIBNR_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), Valid_IBNR_Factor(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=DEALER_IBNRFACTOR)>
    Public Property IBNRFactor() As DoubleType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IBNR_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DoubleType(CType(Row(DealerDAL.COL_NAME_IBNR_FACTOR), Double))
            End If
        End Get
        Set(ByVal Value As DoubleType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IBNR_FACTOR, Value)
        End Set
    End Property
    <ValueMandatory(""), Valid_STAT_IBNR_Factor(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=DEALER_IBNRFACTOR)>
    Public Property STATIBNRFactor() As DoubleType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_STAT_IBNR_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DoubleType(CType(Row(DealerDAL.COL_NAME_STAT_IBNR_FACTOR), Double))
            End If
        End Get
        Set(ByVal Value As DoubleType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_STAT_IBNR_FACTOR, Value)
        End Set
    End Property
    <ValueMandatory(""), Valid_LAE_IBNR_Factor(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=DEALER_IBNRFACTOR)>
    Public Property LAEIBNRFactor() As DoubleType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_LAE_IBNR_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DoubleType(CType(Row(DealerDAL.COL_NAME_LAE_IBNR_FACTOR), Double))
            End If
        End Get
        Set(ByVal Value As DoubleType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_LAE_IBNR_FACTOR, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ConvertProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONVERT_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CONVERT_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONVERT_PRODUCT_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), BranchIdValidation("")>
    Public Property BranchValidationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BRANCH_VALIDATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_BRANCH_VALIDATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BRANCH_VALIDATION_ID, Value)
        End Set
    End Property

    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property BusinessName() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BUSINESS_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_BUSINESS_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BUSINESS_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)>
    Public Property StateTaxIdNumber() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_STATE_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_STATE_TAX_ID_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_STATE_TAX_ID_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)>
    Public Property CityTaxIdNumber() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CITY_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CITY_TAX_ID_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CITY_TAX_ID_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property WebAddress() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_WEB_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_WEB_ADDRESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_WEB_ADDRESS, Value)
        End Set
    End Property

    Public Property MailingAddressId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MAILING_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_MAILING_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MAILING_ADDRESS_ID, Value)
        End Set
    End Property

    Public Property NumberOfOtherLocations() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_NUMBER_OF_OTHER_LOCATIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_NUMBER_OF_OTHER_LOCATIONS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_NUMBER_OF_OTHER_LOCATIONS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PriceMatrixUsesWpId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PRICE_MATRIX_USES_WP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_PRICE_MATRIX_USES_WP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PRICE_MATRIX_USES_WP_ID, Value)
        End Set
    End Property
    <ValueMandatory(""), ExpectedPremiumIsWPValidation("")>
    Public Property ExpectedPremiumIsWPId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_ID, Value)
        End Set
    End Property

    Dim _DealerGroupName As String
    Public ReadOnly Property DealerGroupName() As String
        Get
            If Not (Me.DealerGroupId.Equals(Guid.Empty)) Then
                Me._DealerGroupName = LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEALER_GROUPS, Me.DealerGroupId)
            Else
                Me._DealerGroupName = String.Empty
            End If
            Return Me._DealerGroupName
        End Get
    End Property

    Dim _DealerTypeDesc As String
    Public ReadOnly Property DealerTypeDesc() As String
        Get
            Me._DealerTypeDesc = LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEALER_TYPE, Me.DealerTypeId)
            Return Me._DealerTypeDesc
        End Get
    End Property

    Private _constrVoilation As Boolean
    Public Property ConstrVoilation() As Boolean
        Get
            Return _constrVoilation
        End Get
        Set(ByVal Value As Boolean)
            Me._constrVoilation = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InvoiceByBranchId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_INVOICE_BY_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_INVOICE_BY_BRANCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_INVOICE_BY_BRANCH_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property SeparatedCreditNotesId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SEPARATED_CREDIT_NOTES_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_SEPARATED_CREDIT_NOTES_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SEPARATED_CREDIT_NOTES_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property CertCancelById() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CERT_CANCEL_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CERT_CANCEL_BY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CERT_CANCEL_BY_ID, Value)
        End Set
    End Property


    Private _UseSvcOrderAddress As Boolean
    Public Property UseSvcOrderAddress() As Boolean
        Get
            Return _UseSvcOrderAddress
        End Get
        Set(ByVal Value As Boolean)
            Me._UseSvcOrderAddress = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ManualEnrollmentAllowedId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MANUAL_ENROLLMENT_ALLOWED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_MANUAL_ENROLLMENT_ALLOWED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MANUAL_ENROLLMENT_ALLOWED_ID, Value)
        End Set
    End Property
    'REQ-5761
    Public Property UseNewBillForm() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_NEWBILLFORM) Is DBNull.Value Then
                Return LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_NEWBILLFORM), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_NEWBILLFORM, Value)
        End Set
    End Property

    'REQ-5932
    <ValueMandatory("")>
    Public Property ShareCustomers() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SHARE_CUSTOMERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SHARE_CUSTOMERS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SHARE_CUSTOMERS, Value)
        End Set
    End Property

    Public Property CustomerLookup() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CUSTOMER_IDENTITY_LOOKUP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CUSTOMER_IDENTITY_LOOKUP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CUSTOMER_IDENTITY_LOOKUP, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property EditBranchId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_EDIT_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_EDIT_BRANCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_EDIT_BRANCH_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property OlitaSearch() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_OLITA_SEARCH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_OLITA_SEARCH), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_OLITA_SEARCH, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DelayFactorFlagId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DELAY_FACTOR_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DELAY_FACTOR_FLAG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DELAY_FACTOR_FLAG_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InstallmentFactorFlagId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_INSTALLMENT_FACTOR_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_INSTALLMENT_FACTOR_FLAG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_INSTALLMENT_FACTOR_FLAG_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RegistrationProcessFlagId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REGISTRATION_PROCESS_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_REGISTRATION_PROCESS_FLAG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REGISTRATION_PROCESS_FLAG_ID, Value)
        End Set
    End Property

    <MandatoryForRegistration(""), ValidStringLength("", Max:=50), EmailAddress("")>
    Public Property RegistrationEmailFrom() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REGISTRATION_EMAIL_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_REGISTRATION_EMAIL_FROM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REGISTRATION_EMAIL_FROM, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property UseWarrantyMasterID() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_WARRANTY_MASTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_WARRANTY_MASTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_WARRANTY_MASTER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InsertIfMakeNotExists() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_INSERT_MAKE_IF_NOT_EXISTS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_INSERT_MAKE_IF_NOT_EXISTS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_INSERT_MAKE_IF_NOT_EXISTS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property UseIncomingSalesTaxID() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_INCOMING_SALES_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_INCOMING_SALES_TAX_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_INCOMING_SALES_TAX_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property AutoProcessFileID() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_AUTO_PROCESS_FILE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_AUTO_PROCESS_FILE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_AUTO_PROCESS_FILE_ID, Value)
        End Set
    End Property

    Public Property AutoRejErrTypeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_AUTO_REJ_ERR_TYPE_ID, Value)
        End Set
    End Property

    Public Property ReconRejRecTypeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_RECON_REJ_REC_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_RECON_REJ_REC_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_RECON_REJ_REC_TYPE_ID, Value)
        End Set
    End Property

    Public Property DealerExtractPeriodId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER_EXTRACT_PERIOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEALER_EXTRACT_PERIOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER_EXTRACT_PERIOD_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RoundCommFlagId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ROUND_COMM_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ROUND_COMM_FLAG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ROUND_COMM_FLAG_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseInstallmentDefnId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_INSTALLMENT_DEFN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_INSTALLMENT_DEFN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_INSTALLMENT_DEFN_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property ProgramName() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PROGRAM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_PROGRAM_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PROGRAM_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property ServiceLinePhone() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SERVICE_LINE_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SERVICE_LINE_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SERVICE_LINE_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property ServiceLineFax() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SERVICE_LINE_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SERVICE_LINE_FAX), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SERVICE_LINE_FAX, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property ServiceLineEmail() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SERVICE_LINE_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SERVICE_LINE_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SERVICE_LINE_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property EscInsuranceLabel() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ESC_INSURANCE_LABEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_ESC_INSURANCE_LABEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ESC_INSURANCE_LABEL, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property ClaimSystemId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_SYSTEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CLAIM_SYSTEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_SYSTEM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property AssurantIsObligorId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ASSURANT_IS_OBLIGOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ASSURANT_IS_OBLIGOR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ASSURANT_IS_OBLIGOR_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=999, Min:=1)>
    Public Property MaxManWarr() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MAX_MAN_WARR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_MAX_MAN_WARR), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MAX_MAN_WARR, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=99, Min:=0)>
    Public Property MinManWarr() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MIN_MAN_WARR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_MIN_MAN_WARR), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MIN_MAN_WARR, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_EQUIPMENT_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property CancellationRequestFlagId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CANCEL_REQUEST_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CANCEL_REQUEST_FLAG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CANCEL_REQUEST_FLAG_ID, Value)
        End Set
    End Property

    Public Property BestReplacementGroupId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MIGRATION_PATH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_MIGRATION_PATH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MIGRATION_PATH_ID, Value)
        End Set
    End Property

    Public Property ValidateBillingCycleId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_VALIDATE_BILLING_CYCLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_VALIDATE_BILLING_CYCLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_VALIDATE_BILLING_CYCLE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ValidateSerialNumberId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_VALIDATE_SERIAL_NUMBER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_VALIDATE_SERIAL_NUMBER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_VALIDATE_SERIAL_NUMBER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DeductibleCollectionId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_DEDUCTIBLE_COLLECTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_DEDUCTIBLE_COLLECTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_DEDUCTIBLE_COLLECTION_ID, Value)
        End Set
    End Property

    Public Property EquipmentListCode() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_EQUIPMENT_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(DealerDAL.COL_NAME_EQUIPMENT_LIST_CODE)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_EQUIPMENT_LIST_CODE, value)
        End Set
    End Property

    '<ValueMandatory("")> _
    'Public Property AuthAmtBasedOnId() As Guid
    '    Get
    '        CheckDeleted()
    '        If row(DealerDAL.COL_NAME_AUTH_AMT_BASED_ON_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(row(DealerDAL.COL_NAME_AUTH_AMT_BASED_ON_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(DealerDAL.COL_NAME_AUTH_AMT_BASED_ON_ID, Value)
    '    End Set
    'End Property

    'REQ-860 Elita Buildout - Issues/Adjudication
    Public Property QuestionListCode() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_QUESTION_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(DealerDAL.COL_NAME_QUESTION_LIST_CODE)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_QUESTION_LIST_CODE, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ProductByRegionId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PRODUCT_BY_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_PRODUCT_BY_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PRODUCT_BY_REGION_ID, Value)
        End Set
    End Property

    Public Property ClaimVerfificationNumLength() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH, Value)
        End Set
    End Property

    'Req-1297
    <ValueMandatoryConditionally("")>
    Public Property MaxNCRecords() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MAX_NC_RECORDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_MAX_NC_RECORDS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MAX_NC_RECORDS, Value)
        End Set
    End Property
    'Req-1297 End

    'REQ-1032
    <ValueMandatory("")>
    Public Property ClaimExtendedStatusEntryId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_EXTENDED_STATUS_ENTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CLAIM_EXTENDED_STATUS_ENTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_EXTENDED_STATUS_ENTRY_ID, Value)
        End Set
    End Property

    'Req-1000
    <ValueMandatory("")>
    Public Property AllowUpdateCancellationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ALLOW_UPDATE_CANCELLATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ALLOW_UPDATE_CANCELLATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ALLOW_UPDATE_CANCELLATION_ID, Value)
        End Set
    End Property
    'Req-1000
    <ValueMandatory("")>
    Public Property RejectAfterCancellationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REJECT_AFTER_CANCELLATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_REJECT_AFTER_CANCELLATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REJECT_AFTER_CANCELLATION_ID, Value)
        End Set
    End Property

    'Req-1000
    <ValueMandatory("")>
    Public Property AllowFutureCancelDateId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ALLOW_FUTURE_CANCEL_DATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ALLOW_FUTURE_CANCEL_DATE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ALLOW_FUTURE_CANCEL_DATE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IsLawsuitMandatoryId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IS_LAWSUIT_MANDATORY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_IS_LAWSUIT_MANDATORY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IS_LAWSUIT_MANDATORY_ID, Value)
        End Set
    End Property

    'REQ-1153 begin
    'This field is required in the DB table, but there is a defualt value in the trigger.
    Public Property DealerSupportWebClaimsId() As Guid

        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEALER_SUPPORT_WEB_CLAIMS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEALER_SUPPORT_WEB_CLAIMS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEALER_SUPPORT_WEB_CLAIMS_ID, Value)
        End Set
    End Property

    'REQ-1153 end
    'REQ-1142 start
    <MandatoryForVscAttribute("")>
    Public Property LicenseTagValidationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_LICENSE_TAG_VALIDATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_LICENSE_TAG_VALIDATION), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_LICENSE_TAG_VALIDATION, Value)
        End Set
    End Property  'REQ-1142 end

    <MandatoryForVscAttribute("")>
    Public Property VINRestrictMandatoryId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_VSC_VIN_RESTRIC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_VSC_VIN_RESTRIC_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_VSC_VIN_RESTRIC_ID, Value)
        End Set
    End Property

    <MandatoryForVscAttribute("")>
    Public Property PlanCodeInQuote() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PLAN_CODE_IN_QUOTE_OUTPUT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_PLAN_CODE_IN_QUOTE_OUTPUT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PLAN_CODE_IN_QUOTE_OUTPUT_ID, Value)
        End Set
    End Property
    'REQ-5723 END

    <ValueMandatory("")>
    Public Property UseClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USE_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    'This field is required in the DB table, but there is a defualt value in the trigger.
    Public Property ClaimStatusForExtSystemId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_STATUS_FOR_EXT_SYSTEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CLAIM_STATUS_FOR_EXT_SYSTEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_STATUS_FOR_EXT_SYSTEM_ID, Value)
        End Set
    End Property
    'REQ-1153 end

    'REQ 1157
    Public Property NewDeviceSkuRequiredId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_NEW_DEVICE_SKU_REQUIRED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_NEW_DEVICE_SKU_REQUIRED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_NEW_DEVICE_SKU_REQUIRED_ID, Value)
        End Set
    End Property
    'REQ 1157


    'REQ-1190 START
    Public Property EnrollfilepreprocessprocId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ENROLLFILEPREPROCESSPROC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_ENROLLFILEPREPROCESSPROC_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ENROLLFILEPREPROCESSPROC_ID, Value)
        End Set
    End Property

    'Req-1297
    Public Property UseFullFileProcessId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USEFULLFILEPROCESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_USEFULLFILEPROCESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USEFULLFILEPROCESS_ID, Value)
        End Set
    End Property
    'Req-1297 End

    <LookupByMandatoryConditionally("")>
    Public Property CertnumlookupbyId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CERTNUMLOOKUPBY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CERTNUMLOOKUPBY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CERTNUMLOOKUPBY_ID, Value)
        End Set
    End Property
    'REQ-1190 END 

    'REQ-1244
    <ValidNumericRange("", Max:=100, Min:=0)>
    Public Property Replaceclaimdedtolerancepct() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REPLACECLAIMDEDTOLERANCEPCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerDAL.COL_NAME_REPLACECLAIMDEDTOLERANCEPCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REPLACECLAIMDEDTOLERANCEPCT, Value)
        End Set
    End Property

    'REQ-1274 start
    Public Property BillingProcessCodeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BILLING_PROCESS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_BILLING_PROCESS_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BILLING_PROCESS_CODE_ID, Value)
        End Set
    End Property

    Public Property BillresultExceptionDestId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BILLRESULT_EXCEPTION_DEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_BILLRESULT_EXCEPTION_DEST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BILLRESULT_EXCEPTION_DEST_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=250)>
    Public Property BillresultNotificationEmail() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BILLRESULT_NOTIFICATION_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_BILLRESULT_NOTIFICATION_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BILLRESULT_NOTIFICATION_EMAIL, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=250)>
    Public Property PolicyEventNotificationEmail() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_POLICY_EVENT_NOTIFY_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_POLICY_EVENT_NOTIFY_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_POLICY_EVENT_NOTIFY_EMAIL, Value)
        End Set
    End Property
    'REQ-1274 end

    <ValidStringLength("", Max:=8), CheckDoublicatePREFIX("")>
    Public Property CertificatesAutonumberPrefix() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_PREFIX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_PREFIX), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_PREFIX, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property CertificatesAutonumberId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CERTIFICATES_AUTONUMBER_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=250)>
    Public Property FileLoadNotificationEmail() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_FILE_LOAD_NOTIFICATION_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_FILE_LOAD_NOTIFICATION_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_FILE_LOAD_NOTIFICATION_EMAIL, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=20), MaxCertNumberLengthAlwdValidation("")>
    Public Property MaximumCertNumberLengthAllowed() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MAX_CERTNUM_LENGTH_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_MAX_CERTNUM_LENGTH_ALLOWED), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MAX_CERTNUM_LENGTH_ALLOWED, Value)
        End Set
    End Property

    Public Property AutoSelectServiceCenter() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_AUTO_SELECT_SERVICE_CENTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_AUTO_SELECT_SERVICE_CENTER), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_AUTO_SELECT_SERVICE_CENTER, Value)
        End Set
    End Property

    Public Property DefaultSalvgeCenterId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_DEF_SALVAGE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_DEF_SALVAGE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_DEF_SALVAGE_CENTER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidClaimTypeSelection(""), ValidCoverageTypeSelection("")>
    Public Property ClaimAutoApproveId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_AUTO_APPROVE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_CLAIM_AUTO_APPROVE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_AUTO_APPROVE_ID, Value)
        End Set
    End Property

    Public Property DealerClaimTypeSelectionCount() As Integer
        Get
            Return _dealerClaimTypeSelectionCount
        End Get
        Set(ByVal Value As Integer)
            _dealerClaimTypeSelectionCount = Value
        End Set
    End Property

    Public Property DealerCoverageTypeSelectionCount() As Integer
        Get
            Return _dealerCoverageTypeSelectionCount
        End Get
        Set(ByVal Value As Integer)
            _dealerCoverageTypeSelectionCount = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RequireCustomerAMLInfoId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_REQUIRE_CUSTOMER_AML_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_REQUIRE_CUSTOMER_AML_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_REQUIRE_CUSTOMER_AML_INFO_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property AutoProcessPymtFileID() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_AUTO_PROCESS_PYMT_FILE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_AUTO_PROCESS_PYMT_FILE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_AUTO_PROCESS_PYMT_FILE_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=100, Min:=0)>
    Public Property MaximumCommissionPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_MAX_COMMISSION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerDAL.COL_NAME_MAX_COMMISSION_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_MAX_COMMISSION_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=9999, Min:=0)>
    Public Property GracePeriodMonths() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_GRACE_PERIOD_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_GRACE_PERIOD_MONTHS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_GRACE_PERIOD_MONTHS, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=9999, Min:=0)>
    Public Property GracePeriodDays() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_GRACE_PERIOD_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_GRACE_PERIOD_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_GRACE_PERIOD_DAYS, Value)
        End Set
    End Property

    Public ReadOnly Property IsGracePeriodSpecified() As Boolean
        Get
            If Not GracePeriodDays Is Nothing Or Not GracePeriodMonths Is Nothing Then
                Return True

            Else
                Return False
            End If
        End Get

    End Property

    Public Property AutoGenerateRejectedPaymentFileId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_AUTO_GEN_REJ_PYMT_FILE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_AUTO_GEN_REJ_PYMT_FILE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_AUTO_GEN_REJ_PYMT_FILE_ID, Value)
        End Set
    End Property

    Public Property PaymentRejectedRecordReconcileId() As Guid
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_PYMT_REJ_REC_RECON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerDAL.COL_NAME_PYMT_REJ_REC_RECON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_PYMT_REJ_REC_RECON_ID, Value)
        End Set
    End Property

    Public ReadOnly Property Company() As Company
        Get
            If Me._company Is Nothing Then
                If Not (Me.CompanyId.Equals(Guid.Empty)) Then
                    Me._company = New Company(Me.CompanyId)
                Else
                    Return Nothing
                End If
            End If
            Return Me._company
        End Get
    End Property
    Public Property IdentificationNumberType() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IDENTIFICATION_NUMBER_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseQuote() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_QUOTE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_USE_QUOTE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_QUOTE, Value)
        End Set
    End Property

    Public Property ContractManualVerification() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CONTRACT_MANUAL_VERIFICATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CONTRACT_MANUAL_VERIFICATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CONTRACT_MANUAL_VERIFICATION, Value)
        End Set
    End Property

    Public Property AcceptPaymentByCheck() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_ACCEPT_PAYMENT_BY_CHECK) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_ACCEPT_PAYMENT_BY_CHECK), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_ACCEPT_PAYMENT_BY_CHECK, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ClaimRecordingXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_RECORDING_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CLAIM_RECORDING_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_RECORDING_XCD, Value)
        End Set
    End Property

    Public Property UseFraudMonitoringXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_USE_FRAUD_MONITORING_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_USE_FRAUD_MONITORING_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_USE_FRAUD_MONITORING_XCD, Value)
        End Set
    End Property

    Public Property ImeiUseXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IMEI_USE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_IMEI_USE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IMEI_USE_XCD, Value)
        End Set
    End Property

    Private _DealerFulfillmentProviderClassCode As String
    Public ReadOnly Property DealerFulfillmentProviderClassCode() As String
        Get
            If Me._DealerFulfillmentProviderClassCode Is Nothing Then
                Dim dal As New DealerDAL
                Me._DealerFulfillmentProviderClassCode = dal.dealerProviderClassCode(Me.Dealer, Codes.PROVIDER_TYPE__FULFILLMENT)
            End If
            Return Me._DealerFulfillmentProviderClassCode
        End Get
    End Property
    <ValueMandatory("")>
    Public Property ClaimRecordingCheckInventoryXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLAIM_RECORDING_CHECK_INVENTORY_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CLAIM_RECORDING_CHECK_INVENTORY_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLAIM_RECORDING_CHECK_INVENTORY_XCD, Value)
        End Set
    End Property

    Public Property SuspendAppliesXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SUSPEND_APPLIES_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SUSPEND_APPLIES_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SUSPEND_APPLIES_XCD, Value)
        End Set
    End Property
 
    Public Property VoidDuration() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_VOID_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_VOID_DURATION), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_VOID_DURATION, value)
        End Set
    End Property

    Public Property SuspendPeriod() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SUSPEND_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_SUSPEND_PERIOD), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SUSPEND_PERIOD, value)
        End Set
    End Property
    <ValidNumericRange("", Max:=28, Min:=1)>
    Public Property InvoiceCutoffDay() As LongType
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_INVOICE_CUTOFF_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DealerDAL.COL_NAME_INVOICE_CUTOFF_DAY), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_INVOICE_CUTOFF_DAY, value)
        End Set
    End Property

    Public Property SourceSystemXcd() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SOURCE_SYSTEM_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SOURCE_SYSTEM_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SOURCE_SYSTEM_XCD, Value)
        End Set
    End Property

    Public Property BenefitCarrierCode() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BENEFIT_CARRIER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_BENEFIT_CARRIER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BENEFIT_CARRIER_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property BenefitSoldToAccount() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_BENEFIT_SOLD_TO_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_BENEFIT_SOLD_TO_ACCOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_BENEFIT_SOLD_TO_ACCOUNT, Value)
        End Set
    End Property

    'KDDI Changes'
    <ValidateBasedOnCancelShipment("")>
    Public Property Is_Cancel_Shipment_Allowed() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IS_CANCEL_SHIPMENT_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_IS_CANCEL_SHIPMENT_ALLOWED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IS_CANCEL_SHIPMENT_ALLOWED, Value)
        End Set
    End Property

    Public Property Show_Previous_Caller_Info() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_SHOW_PREV_CALLER_INFO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_SHOW_PREV_CALLER_INFO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_SHOW_PREV_CALLER_INFO, Value)
        End Set
    End Property
    <ValidateBasedOnCancelShipment("")>
    Public Property Validate_Address() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_VALIDATE_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_VALIDATE_ADDRESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_VALIDATE_ADDRESS, Value)
        End Set
    End Property

    Public Property Is_Reshipment_Allowed() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_IS_RESHIPMENT_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_IS_RESHIPMENT_ALLOWED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_IS_RESHIPMENT_ALLOWED, Value)
        End Set
    End Property



    Public Property Cancel_Shipment_Grace_Period() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.CANCEL_SHIPMENT_GRACE_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.CANCEL_SHIPMENT_GRACE_PERIOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.CANCEL_SHIPMENT_GRACE_PERIOD, Value)
        End Set
    End Property
    Public Property CaseProfileCode() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CASE_PROFILE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CASE_PROFILE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CASE_PROFILE_CODE, Value)
        End Set
    End Property

    Public Property CloseCaseGracePeriodDays() As String
        Get
            CheckDeleted()
            If Row(DealerDAL.COL_NAME_CLOSE_CASE_GRACE_PERIOD_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerDAL.COL_NAME_CLOSE_CASE_GRACE_PERIOD_DAYS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerDAL.COL_NAME_CLOSE_CASE_GRACE_PERIOD_DAYS, Value)
        End Set
    End Property

    


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                    Me._address = Nothing
                    Me._SvcOrdersAddress = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub CreateExternalTable()
        Try
            Dim dal As New DealerDAL
            dal.CreateExternalTable(Me.Id, Me.UseFullFileProcessId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub UpdateClaimsAsyncForLawsuit()
        Try
            Dim dal As New DealerDAL
            dal.UpdateClaimsAsync(Me.Id, ClaimUpdateAsyncTask.MakeLawsuitMandatoryChanged)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Dim blnIsDirty As Boolean = False
            If MyBase.IsDirty OrElse Me.IsChildrenDirty Then
                blnIsDirty = True
            ElseIf (Not Me.Address.IsNew AndAlso Me.Address.IsDirty) OrElse (Me.Address.IsNew AndAlso Not Me.Address.IsEmpty) Then
                blnIsDirty = True
            ElseIf (Not Me.MailingAddress.IsNew AndAlso Me.MailingAddress.IsDirty) OrElse (Me.MailingAddress.IsNew AndAlso Not Me.MailingAddress.IsEmpty) Then
                blnIsDirty = True
            ElseIf (Not _SvcOrdersAddress Is Nothing) Then
                If ((Not Me.SvcOrdersAddress.IsNew) AndAlso Me.SvcOrdersAddress.IsDirty) OrElse (Me.SvcOrdersAddress.IsNew AndAlso Not Me.SvcOrdersAddress.IsEmpty) Then
                    blnIsDirty = True
                End If
            End If

            Return blnIsDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As Dealer)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Dealer")
        End If
        'Copy myself
        Me.CopyFrom(original)

        'copy the children       

        Me.AddressId = Guid.Empty
        Me.Address.CopyFrom(original.Address)
        Me.MailingAddressId = Guid.Empty
        Me.MailingAddress.CopyFrom(original.MailingAddress)
        If original.UseSvcOrderAddress Then
            Me.SvcOrdersAddress.Copy(original.SvcOrdersAddress)
        End If



    End Sub


    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Dim addr As Address = Me.Address
        Dim mailingaddr As Address = Me.MailingAddress

        Me.BeginEdit()
        addr.BeginEdit()
        mailingaddr.BeginEdit()

        If Me.UseSvcOrderAddress Then
            Try
                Dim ServOrderAddress As ServiceOrdersAddress = Me.SvcOrdersAddress
                Dim ServOrdersAddrAddress As Address = SvcOrdersAddress.Address
                ServOrderAddress.Address.BeginEdit()
                ServOrderAddress.BeginEdit()
                ServOrderAddress.Address.Delete()
                ServOrderAddress.Delete()
            Catch ex As Exception
                'ServOrderAddress.cancelEdit()
            End Try
        End If

        Try

            Me.Delete()
            addr.Delete()
            mailingaddr.Delete()
            Me.Save()
        Catch ex As Exception
            If ex.Message = "Integrity Constraint Violation" Then
                Me.ConstrVoilation = True
            End If
            Me.cancelEdit()
            addr.cancelEdit()
            mailingaddr.cancelEdit()
            Throw ex
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    '8/2/06 - ALR Modified function to accept a user guid (as a string) rather than a company id
    Public Shared Function GetDealersWithMonthlyBilling(ByVal userId As String, ByVal todayDate As String) As DataView
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            ds = dal.GetDealersWithMonthlyBilling(userId, todayDate)
            Return (ds.Tables(DealerDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetDealerTypeId(ByVal dealerID As Guid) As Guid
        Dim dal As New DealerDAL

        Dim dealerTypeID As Guid
        Dim ds As DataSet = dal.GetDealerTypeId(dealerID)
        If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            If Not ds.Tables(0).Rows(0)(DealerDAL.COL_NAME_DEALER_TYPE_ID) Is System.DBNull.Value Then
                dealerTypeID = New Guid(CType(ds.Tables(0).Rows(0)(DealerDAL.COL_NAME_DEALER_TYPE_ID), Byte()))
                Return dealerTypeID
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetDealerCertAddEnabled(ByVal companyIds As ArrayList) As DataView
        Dim dal As New DealerDAL
        Dim dsDealer As DataSet
        Return dal.LoadDealerListCertAddEnabled(companyIds).Tables(0).DefaultView
    End Function

    Public Shared Function GetOlitaSearchType(ByVal companyIds As ArrayList, ByVal dealerCode As String) As String
        Dim dealerId As Guid
        Dim dvDealrs As DataView = LookupListNew.GetDealerLookupList(companyIds)
        If Not dvDealrs Is Nothing AndAlso dvDealrs.Count > 0 Then
            dealerId = LookupListNew.GetIdFromCode(dvDealrs, dealerCode)
            If dealerId.Equals(Guid.Empty) Then
                Throw New StoredProcedureGeneratedException("Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
            End If
        End If

        Dim objDealer As New Dealer(dealerId)
        If objDealer Is Nothing Then
            Throw New StoredProcedureGeneratedException("Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
        End If
        Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_OLITA_SEARCH, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Return LookupListNew.GetCodeFromId(dv, objDealer.OlitaSearch)

    End Function

    Public Shared Function GetOlitaSearchType(ByVal dealerId As Guid) As String
        Dim objDealer As New Dealer(dealerId)
        Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_OLITA_SEARCH, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Return LookupListNew.GetCodeFromId(dv, objDealer.OlitaSearch)
    End Function

    Public Shared Function GetDealerCountryId(ByVal dealerId As Guid) As Guid
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet
            Dim countryId As Guid

            ds = dal.GetDealerCountry(dealerId)

            Dim oCompaniesArr = New ArrayList
            If ds.Tables(0).Rows.Count > 0 Then
                ' Create Array
                For index As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If Not ds.Tables(0).Rows(index)("country_id") Is System.DBNull.Value Then
                        countryId = New Guid(CType(ds.Tables(0).Rows(index)("country_id"), Byte()))
                    End If
                Next
            End If


            Return countryId

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Extended Functionality: New Dealer Definitions"
    Public Function GetDealerProductCodesCount() As Integer

        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            ds = dal.GetDealerProductCodesCount(Me.Id)
            Return CType((ds.Tables(DealerDAL.DEALER_PRODUCT_CODES_COUNT_TABLE).Rows(0).Item(DealerDAL.COL_NAME_DEALER_PRODUCT_CODES_COUNT)), Integer)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function GetDealerCoveragesCount() As Integer
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            ds = dal.GetDealerCoveragesCount(Me.Id)
            Return CType((ds.Tables(DealerDAL.DEALER_COVERAGES_COUNT_TABLE).Rows(0).Item(DealerDAL.COL_NAME_DEALER_COVERAGES_COUNT)), Integer)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Function DealerHasValidContract() As Boolean
        Dim dealerContractsView As DataView = Contract.getList(Me.Id)
        Dim blnValidContract As Boolean
        If dealerContractsView.Count <= 0 Then
            Return False
        Else
            Dim todayDate As Date = Date.Today
            Dim drv As System.Data.DataRowView
            For index As Integer = 0 To dealerContractsView.Count - 1
                drv = dealerContractsView.Item(index)
                If (Date.Compare(todayDate, CType(drv.Item(ContractDAL.COL_NAME_EFFECTIVE), Date)) <= 0) _
                   Or ((Date.Compare(todayDate, CType(drv.Item(ContractDAL.COL_NAME_EFFECTIVE), Date)) > 0) And (Date.Compare(todayDate, CType(drv.Item(ContractDAL.COL_NAME_EXPIRATION), Date)) <= 0)) Then
                    blnValidContract = True
                    Exit For
                Else
                    blnValidContract = False
                End If
            Next
        End If
        Return blnValidContract
    End Function
    Public Function IsLastContractPolicyAutoGenerated() As Boolean

        Dim dealerContract As Contract
        dealerContract = Contract.GetCurrentContract(Me.Id)

        ' if no active contract as per current date then get last expired contract.
        If dealerContract Is Nothing Then
            dealerContract = Contract.GetMaxExpirationContract(Me.Id)
        End If

        If dealerContract Is Nothing Then
            Return False
        End If

        If dealerContract?.PolicyTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_TYPE, Codes.CONTRACT_POLTYPE_INDIVIDUAL)) AndAlso
           dealerContract?.PolicyGenerationId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CONTRACT_POLICY_GEN_TYPE, Codes.CONTRACT_POLGEN_AUTOGENERATE)) Then
            Return True
        End If

        Return False

    End Function
    Public Function DealerHasSameRecurringPremiumSetting(ByVal objOtherDealer As Dealer) As Boolean
        Dim dealerContract As Contract = Contract.GetCurrentContract(Me.Id)
        Dim otherDealerContract As Contract = Contract.GetCurrentContract(objOtherDealer.Id)
        Dim blnHasSameRecurringPremiumSetting As Boolean
        If dealerContract.RecurringPremiumId.Equals(otherDealerContract.RecurringPremiumId) Then
            blnHasSameRecurringPremiumSetting = True
        Else
            blnHasSameRecurringPremiumSetting = False
        End If

        Return blnHasSameRecurringPremiumSetting
    End Function

    Public Function EnteredDateWithinContract(ByVal EnteredEffectiveDate As String, ByVal EnteredExpirationDate As String) As Boolean
        ' to check the entered dates for coverage are within the date range of the contract which has highest expiration date for one dealer
        Dim oContract As Contract
        oContract = Contract.GetMaxExpirationContract(Me.Id)
        Dim blnDateWithinContract As Boolean
        Dim contractEffective As Date = oContract.Effective.Value
        Dim contractExpiration As Date = oContract.Expiration.Value
        If (CType(EnteredEffectiveDate, Date) >= contractEffective) And (CType(EnteredEffectiveDate, Date) <= contractExpiration) And
           (CType(EnteredExpirationDate, Date) >= contractEffective) And (CType(EnteredExpirationDate, Date) <= contractExpiration) Then
            blnDateWithinContract = True
        Else
            blnDateWithinContract = False

        End If

        Return blnDateWithinContract
    End Function
    Public Function GetDealerCertificatesCount() As Integer
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            ds = dal.GetDealerCertificatesCount(Me.Id)
            Return CType((ds.Tables(DealerDAL.DEALER_CERTIFICATES_COUNT_TABLE).Rows(0).Item(DealerDAL.COL_NAME_DEALER_CERTIFICATES_COUNT)), Integer)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetDuplicateDealers(ByVal business_country As Guid, ByVal company_type_id As Guid) As Boolean
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            If Not Me.Dealer Is Nothing AndAlso Not business_country.Equals(Guid.Empty) AndAlso company_type_id.Equals(Guid.Empty) Then
                ds = dal.GetDupicateDealerCount(Me.Dealer, business_country, company_type_id)
            Else
                Return False
            End If

            If CType((ds.Tables(DealerDAL.DUPLICATE_DEALER_TABLE).Rows(0).Item(DealerDAL.COL_NAME_DEALER_COUNT)), Integer) > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetDealerCountByCode() As Boolean
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            If Not Me.Dealer Is Nothing Then
                ds = dal.LoadDealerCountByCode(Me.Dealer)
            Else
                Return False
            End If

            If CType((ds.Tables(DealerDAL.TABLE_NAME).Rows(0).Item(DealerDAL.COL_NAME_DEALER_COUNT)), Integer) > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getFirstDealerByDealerGrp(ByVal Dealer_Group_Id As Guid) As DataView
        Try
            Dim dal As New DealerDAL
            Dim dsDealer As DataSet
            Return dal.LoadFirstDealerByDealerGrp(Dealer_Group_Id).Tables(0).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetDealerIDbyCodeAndDealerGroup(ByVal Dealer_Group_Id As Guid, ByVal Dealer As String) As Guid
        Try
            Dim dal As New DealerDAL
            Dim dV As DataView
            Dim DealerID As Guid

            dV = dal.GetDealerIDbyCodeAndDealerGroup(Dealer_Group_Id, Dealer).Tables(0).DefaultView

            If dV.Count > 0 Then
                DealerID = GuidControl.ByteArrayToGuid(CType(dV(0)("ID"), Byte()))
            Else
                Return Guid.Empty
            End If

            Return DealerID
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertAutoGenFlag(ByVal dealerId As Guid) As Boolean
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet
            ds = dal.GetCertAutoGenFlag(dealerId)
            If CType((ds.Tables(0).Rows(0).Item(0)), Integer) = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRejRecReconFlag(ByVal dealerId As Guid) As Boolean
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet
            ds = dal.GetRejRecReconFlag(dealerId)
            If CType((ds.Tables(0).Rows(0).Item(0)), Integer) = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function RenewCoverage(ByVal fromDealerID As Guid, ByVal contractID As Guid, ByVal effdate As Date) As Integer
        Try
            Dim dal As New DealerDAL
            Return dal.RenewCoverage(fromDealerID, contractID, effdate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function CopyDealerDefinitions(ByVal fromDealerID As Guid, ByVal toDealerID As Guid, ByVal intCopyLevel As Integer, ByVal effdate As Date, ByVal expdate As Date) As Integer
        Try
            Dim dal As New DealerDAL
            Return dal.CopyDealerDefinitions(fromDealerID, toDealerID, intCopyLevel, effdate, expdate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function DeleteDealerDefinitions(ByVal fromDealerID As Guid, ByVal intCopyLevel As Integer) As Integer
        Try
            Dim dal As New DealerDAL
            Return dal.DeleteDealerDefinitions(fromDealerID, intCopyLevel)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    'Public Shared Function GetNetworkServiceIDs(ByVal oServiceNetworkId As Guid) As DataView
    '    Dim dal As New ServiceNetworkSvcDAL
    '    Dim ds As Dataset

    '    ds = dal.LoadNetworkServicenterIDs(oServiceNetworkId)
    '    Return ds.Tables(dal.TABLE_NAME).DefaultView
    'End Function

    'Public Shared Function GetAllNetworkServiceIDs() As DataView
    '    Dim dal As New ServiceNetworkSvcDAL
    '    Dim ds As Dataset

    '    ds = dal.LoadAllNetworkServicenterIDs(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
    '    Return ds.Tables(dal.TABLE_NAME).DefaultView
    'End Function

    'Public Function ServiceCentersIDs(ByVal isNetwork As Boolean) As ArrayList
    '    Dim oSvcDv As DataView
    '    If moServiceCenterIDs Is Nothing Then
    '        If isNetwork Then
    '            oSvcDv = GetNetworkServiceIDs(Me.ServiceNetworkId)
    '        Else
    '            oSvcDv = GetAllNetworkServiceIDs()
    '        End If

    '        moServiceCenterIDs = New ArrayList

    '        If oSvcDv.Table.Rows.Count > 0 Then
    '            Dim index As Integer
    '            ' Create Array
    '            For index = 0 To oSvcDv.Table.Rows.Count - 1
    '                If Not oSvcDv.Table.Rows(index)(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID) Is System.DBNull.Value Then
    '                    moServiceCenterIDs.Add(New Guid(CType(oSvcDv.Table.Rows(index)(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID), Byte())))
    '                End If
    '            Next
    '        End If
    '    End If
    '    Return moServiceCenterIDs
    'End Function

    Public Shared Function GetDuplicatePrefixCount(ByVal CompanyId As Guid, Optional ByVal countLevel As Integer = 1, Optional ByVal certificatesAutonumberPrefix As String = "") As Integer
        Try
            Dim dal As New DealerDAL
            Dim ds As DataSet

            ds = dal.GetDuplicatePrefixCount(CompanyId, countLevel, certificatesAutonumberPrefix)
            Return CType((ds.Tables(DealerDAL.DUPLICATE_PREFIX_COUNT_TABLE).Rows(0).Item(DealerDAL.COL_NAME_DUPLICATE_PREFIX_COUNT)), Integer)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Memebers"
    Public Function MyGetFormattedSearchStringForSQL(ByVal str As String) As String
        If (Not IsNothing(str)) Then
            str = str.Trim
            str = str.ToUpper
            If (str.IndexOf(ASTERISK) > -1) Then
                str = str.Replace(ASTERISK, WILDCARD_CHAR)
            End If
        Else
            str &= WILDCARD_CHAR
        End If
        Return (str)
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal descriptionMask As String, ByVal codeMask As String, ByVal dealer_group_id As Guid, ByVal company_groupId As Guid) As DealerSearchDV
        Try
            Dim dal As New DealerDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New DealerSearchDV(dal.LoadList(descriptionMask, codeMask, dealer_group_id, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getList(ByVal DealerId As Guid, ByVal dealer_group_id As Guid, ByVal company_groupId As Guid) As DealerSearchDV
        Try
            Dim dal As New DealerDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New DealerSearchDV(dal.LoadList(DealerId, dealer_group_id, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "DealerSearchDV"
    Public Class DealerSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_DEALER As String = "dealer"
        Public Const COL_DEALER_GROUP As String = "dealer_group"
        Public Const COL_COMPANY As String = "company_code"
        Public Const COL_ACTIVE_FLAG As String = DealerDAL.COL_NAME_ACTIVE_FLAG
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


#Region "Children Related"

    Public Shared Function IsSkipActiveClaim(ByVal dealerID As Guid) As Boolean
        Dim dal As New DealerDAL
        Return dal.IsSkipActiveClaim(dealerID)
    End Function

    Public Shared Function GetAvailablClaimTypes(ByVal dealerID As Guid, ByVal languageId As Guid) As DataSet
        Dim dal As New DealerDAL
        Dim ds As DataSet

        ds = dal.LoadAvailableClaimTypes(dealerID, languageId)
        Return ds
    End Function

    Public Shared Function GetAvailableCoverageTypes(ByVal dealerID As Guid, ByVal languageId As Guid) As DataSet
        Dim dal As New DealerDAL
        Dim ds As DataSet

        ds = dal.LoadAvailableCoverageTypes(dealerID, languageId)
        Return ds
    End Function

    Public Shared Function GetSelectedClaimTypes(ByVal dealerID As Guid, ByVal languageId As Guid) As DataSet
        Dim dal As New DealerDAL
        Dim ds As DataSet

        ds = dal.LoadSelectedClaimTypes(dealerID, languageId)
        Return ds
    End Function

    Public Shared Function GetSelectedCoverageTypes(ByVal dealerID As Guid, ByVal languageId As Guid) As DataSet
        Dim dal As New DealerDAL
        Dim ds As DataSet

        ds = dal.LoadSelectedCoverageTypes(dealerID, languageId)
        Return ds
    End Function

    Public Sub AttachClaimType(ByVal selectedClaimTypeGuidStrCollection As ArrayList)
        Dim dealerClmApproveClmtypeIdStr As String
        For Each dealerClmApproveClmtypeIdStr In selectedClaimTypeGuidStrCollection
            'update to new DealerClmAproveClmtype GUID
            Dim newBO As DealerClmAproveClmtype = New DealerClmAproveClmtype(Me.Dataset)
            If Not newBO Is Nothing Then
                newBO.DealerId = Me.Id
                newBO.ClaimTypeId = New Guid(dealerClmApproveClmtypeIdStr)
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachClaimType(ByVal selectedClaimTypeGuidStrCollection As ArrayList)
        Dim dealerClmApproveClmtypeIdStr As String
        For Each dealerClmApproveClmtypeIdStr In selectedClaimTypeGuidStrCollection
            'update to new DealerClmAproveClmtype GUID
            Dim newBO As DealerClmAproveClmtype = New DealerClmAproveClmtype(Me.Dataset, Me.Id, New Guid(dealerClmApproveClmtypeIdStr))
            If Not newBO Is Nothing Then
                newBO.Delete()
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub AttachCoverageType(ByVal selectedCoverageTypeGuidStrCollection As ArrayList)
        Dim dealerClmAproveCovtypeIdStr As String
        For Each dealerClmAproveCovtypeIdStr In selectedCoverageTypeGuidStrCollection
            'update to new DealerClmAproveCovtype GUID
            Dim newBO As DealerClmAproveCovtype = New DealerClmAproveCovtype(Me.Dataset)
            If Not newBO Is Nothing Then
                newBO.DealerId = Me.Id
                newBO.CoverageTypeId = New Guid(dealerClmAproveCovtypeIdStr)
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachCoverageType(ByVal selectedCoverageTypeGuidStrCollection As ArrayList)
        Dim dealerClmAproveCovtypeIdStr As String
        For Each dealerClmAproveCovtypeIdStr In selectedCoverageTypeGuidStrCollection
            'update to new DealerClmAproveCovtype GUID
            Dim newBO As DealerClmAproveCovtype = New DealerClmAproveCovtype(Me.Dataset, Me.Id, New Guid(dealerClmAproveCovtypeIdStr))
            If Not newBO Is Nothing Then
                newBO.Delete()
                newBO.Save()
            End If
        Next
    End Sub

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class Valid_IBNR_Factor
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_IBNR_FACTOR_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If obj.IBNRFactor Is Nothing Then Return True
            Dim str As String = obj.IBNRFactor.ToString
            If obj.IBNRFactor.Value >= 10 Then 'Or str.Substring(str.LastIndexOf(".") + 1).Length > 4 Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class Valid_STAT_IBNR_Factor
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_IBNR_FACTOR_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If obj.STATIBNRFactor Is Nothing Then Return True
            Dim str As String = obj.STATIBNRFactor.ToString
            If obj.STATIBNRFactor.Value >= 1 Or obj.STATIBNRFactor.Value < 0 Then 'Or str.Substring(str.LastIndexOf(".") + 1).Length > 4 Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class Valid_LAE_IBNR_Factor
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_IBNR_FACTOR_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If obj.LAEIBNRFactor Is Nothing Then Return True
            Dim str As String = obj.LAEIBNRFactor.ToString
            If obj.LAEIBNRFactor.Value >= 1 Or obj.LAEIBNRFactor.Value < 0 Then 'Or str.Substring(str.LastIndexOf(".") + 1).Length > 4 Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ExpectedPremiumIsWPValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.EXPECTED_PREMIUM_PRICE_MATRIX_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If obj.ExpectedPremiumIsWPId.Equals(Guid.Empty) Then Return True
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, obj.ExpectedPremiumIsWPId) = Codes.YESNO_Y Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, obj.PriceMatrixUsesWpId) <> Codes.YESNO_Y Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CNPJ_TaxIdValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CNPJ_DOCUMENT_NUMBER_ERR) 'REQ 1012
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            Dim dal As New DealerDAL
            Dim oErrMess As String

            Try
                If obj.TaxIdNumber Is Nothing Then Return True ' the ValueMandatoryConditionally will catch this validation

                If LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, obj.Company.CompanyTypeId) = obj.Company.COMPANY_TYPE_INSURANCE Then
                    oErrMess = dal.ExecuteSP(Codes.DOCUMENT_TYPE__CNPJ, obj.TaxIdNumber)
                    If Not oErrMess Is Nothing Then
                        MyBase.Message = UCase(oErrMess)
                        Return False
                    End If
                End If

                Return True

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatoryForVscAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)

            If obj.DealerTypeDesc = obj.DEALER_TYPE_DESC Then
                Dim mandatAttr As New ValueMandatoryAttribute(Me.DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class BranchIdValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DEALER_BRANCH_VALIDATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            Dim isclaimselected As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)

            If obj.InvoiceByBranchId = isclaimselected AndAlso obj.BranchValidationId <> isclaimselected Then
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatoryForRegistration
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            Dim sVal As String

            sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, obj.RegistrationProcessFlagId)
            If sVal = Codes.YESNO_Y Then
                Dim mandatAttr As New ValueMandatoryAttribute(Me.DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
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
            Dim obj As Dealer = CType(objectToValidate, Dealer)

            If obj.RegistrationEmailFrom Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.RegistrationEmailFrom)

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MaxCertNumberLengthAlwdValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            Dim sVal As String

            sVal = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, obj.CertificatesAutonumberId)
            If sVal = Codes.YESNO_Y AndAlso obj.MaximumCertNumberLengthAllowed Is Nothing Then
                MyBase.Message = Common.ErrorCodes.ERR_MSG_MAX_CERT_NUMBER_LENGTH_IS_REQUIRED
                Return False
            ElseIf sVal = Codes.YESNO_Y AndAlso (obj.MaximumCertNumberLengthAllowed.Value) - Len(obj.CertificatesAutonumberPrefix) < 6 Then
                MyBase.Message = Common.ErrorCodes.ERR_MSG_MAX_CERT_NUMBER_LENGTH_IS_NOT_VALID
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDoublicatePREFIX
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_UNIQUE_CERTIFICATES_AUTO_NUMBER_PREFIX_IS_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            Dim yesGuid As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
            If Not obj.CertificatesAutonumberId.Equals(Guid.Empty) AndAlso obj.CertificatesAutonumberId.Equals(yesGuid) Then
                If Not obj.Company.UniqueCertificateNumbersId.Equals(Guid.Empty) AndAlso obj.Company.UniqueCertificateNumbersId.Equals(yesGuid) Then
                    If obj.GetDuplicatePrefixCount(obj.CompanyId, 0, obj.CertificatesAutonumberPrefix) > 0 Then
                        Return False
                    End If
                End If
            End If
            Return True


        End Function

    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim DealerObject As Dealer = CType(objectToValidate, Dealer)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, DealerObject.UseFullFileProcessId) <> Codes.FLP_NO Then
                If DealerObject.MaxNCRecords Is Nothing Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidClaimTypeSelection
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_AT_LEAST_ONE_CLAIM_TYPE_SELECTION_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)

            If Not obj.ClaimAutoApproveId.Equals(Guid.Empty) AndAlso obj.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) AndAlso obj.DealerClaimTypeSelectionCount = 0 Then
                Return False
            End If
            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoverageTypeSelection
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_AT_LEAST_ONE_COVERAGE_TYPE_SELECTION_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If Not obj.ClaimAutoApproveId.Equals(Guid.Empty) AndAlso obj.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) AndAlso obj.DealerCoverageTypeSelectionCount = 0 Then
                Return False
            End If
            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class LookupByMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.BO_ERROR_DEALER__IF_AUTO_NUMBER_THEN_LOOKUP_BY_MANDATORY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, obj.CertificatesAutonumberId) = Codes.YESNO_Y AndAlso obj.CertnumlookupbyId.Equals(Guid.Empty) AndAlso obj.Company.CertnumlookupbyId.Equals(Guid.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateBasedOnCancelShipment
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.MSG_CANCEL_SHIPMENT_GRACE_PERIOD_MANDATORY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Dealer = CType(objectToValidate, Dealer)

            If Not obj.Is_Cancel_Shipment_Allowed Is Nothing AndAlso obj.Is_Cancel_Shipment_Allowed = "YESNO-Y" AndAlso String.IsNullOrEmpty(obj.Cancel_Shipment_Grace_Period) Then
                Return False
            ElseIf Not obj.Is_Cancel_Shipment_Allowed Is Nothing AndAlso obj.Is_Cancel_Shipment_Allowed = "YESNO-N" Then
                Return True
            End If

            Return True
        End Function
    End Class
#End Region

End Class

#Region "Enums"

' Add the scenarios when Claim Update (data conversion) is needed with a Configuration Change
Public Enum ClaimUpdateAsyncTask

    MakeLawsuitMandatoryChanged

End Enum

#End Region



