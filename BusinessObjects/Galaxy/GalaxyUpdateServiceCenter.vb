Public Class GalaxyUpdateServiceCenter

    Inherits BusinessObjectBase

#Region "Member Variables"

    Private bankInfoSave As Boolean
    Private RegionId As Guid
    Private CountryID As Guid
    Private BankCountryID As Guid
    Private AddressRequiredServCenter As Boolean
    Private OriginalDealerId As Guid
    Private Id As Guid
    'Private ServiceTypeId As Guid    'removed by REQ-263
    Private PaymentMethodId As Guid
    Private AccountTypeId As Guid
    Private ServiceCenterId As Guid

    Private _AddressIsNull As Boolean = True
    Private _CityIsNull As Boolean = True
    Private _RegionCodeIsNull As Boolean = True
    Private _PostalCodeIsNull As Boolean = True
    Private _CountryCodeIsNull As Boolean = True
    'Private _ServiceTypeIsNull As Boolean = True  'removed by REQ-263
    Private _ServiceCenterCodeIsNull As Boolean = True
    Private _ServiceCenterNameIsNull As Boolean = True
    Private _RatingCodeIsNull As Boolean = True
    Private _PhoneIsNull As Boolean = True
    Private _FaxIsNull As Boolean = True
    Private _RegistrationNumberIsNull As Boolean = True
    Private _TaxIdIsNull As Boolean = True
    Private _StatusCodeIsNull As Boolean = True
    Private _CommentsIsNull As Boolean = True
    Private _AccountNameIsNull As Boolean = True
    Private _BankIdIsNull As Boolean = True
    Private _AccountNumberIsNull As Boolean = True
    Private _IBANnumberIsNull As Boolean = True
    Private _SwiftCodeIsNull As Boolean = True
    Private _BankCountryCodeIsNull As Boolean = True
    Private _AccountTypeIsNull As Boolean = True
    Private _AccountTypeIdIsNull As Boolean = True
    Private _PaymentMethodIsNull As Boolean = True
    Private _OriginalDealerCodeIsNull As Boolean = True
    Private _countryIdIsNull As Boolean = True
    Private _RegionIdIsNull As Boolean = True
    Private _DealerIdIsNull As Boolean = True
    'Private _ServiceTypeIdIsNull As Boolean = True   'removed by REQ-263
    Private _BankCountryIdIsNull As Boolean = True
    Private _PaymentMethodIdIsNull As Boolean = True


#End Region

#Region "Constants"

    Public TABLE_NAME As String = "GalaxyUpdateServiceCenter"
    Public Const Update_Failed As String = "err_Update_Failed"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"


    Private Const Source_Col_Service_Center_Code As String = "service_center_code"
    Private Const Source_Col_Service_Center_Name As String = "service_center_name"
    Private Const Source_Col_Address As String = "address"
    Private Const Source_Col_City As String = "city"
    Private Const Source_Col_Region_Code As String = "region_code"
    Private Const Source_Col_Postal_Code As String = "postal_code"
    Private Const Source_Col_Country_Code As String = "country_code"
    Private Const Source_Col_Status_Code As String = "status_code"
    Private Const Source_Col_Tax_Id As String = "tax_id"
    Private Const Source_Col_Service_Type As String = "service_type"
    Private Const Source_Col_Phone As String = "phone"
    Private Const Source_Col_Fax As String = "fax"
    Private Const Source_Col_Comments As String = "comments"
    Private Const Source_Col_Original_Dealer_Code As String = "original_dealer_code"
    Private Const Source_Col_Rating_Code As String = "rating_code"
    Private Const Source_Col_Payment_Method As String = "payment_method"
    Private Const Source_Col_Account_Name As String = "account_name"
    Private Const Source_Col_Bank_Id As String = "bank_id"
    Private Const Source_Col_Account_Number As String = "account_number"
    Private Const Source_Col_IBAN_Number As String = "iban_number"
    Private Const Source_Col_Bank_Country_Code As String = "bank_country_code"
    Private Const Source_Col_Swift_Code As String = "swift_code"
    Private Const Source_Col_Account_Type As String = "account_type"
    Private Const Source_Col_Registration_Number As String = "registration_number"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GalaxyUpdateServiceCenterDs)
        MyBase.New()
        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String

        Try
            Validate()

            Dim serviceCenterBO As New ServiceCenter(ServiceCenterId)
            Dim SaveAddress As Boolean = False

            serviceCenterBO.Code = ServiceCenterCode

            If Not _AddressIsNull Then
                serviceCenterBO.Address.Address1 = Address
                SaveAddress = True
            End If
            If Not _CityIsNull Then
                serviceCenterBO.Address.City = City
                SaveAddress = True
            End If
            If Not _PostalCodeIsNull Then
                serviceCenterBO.Address.PostalCode = PostalCode
                SaveAddress = True
            End If
            If Not _RegionIdIsNull Then
                serviceCenterBO.Address.RegionId = RegionId
                SaveAddress = True
            End If
            If Not _countryIdIsNull Then
                serviceCenterBO.Address.CountryId = CountryID
                SaveAddress = True
            End If
            'removed by REQ-263
            'If Not Me._ServiceTypeIdIsNull Then serviceCenterBO.ServiceTypeId = Me.ServiceTypeId

            'Added by REQ-452
            If Not ServiceType Is Nothing AndAlso Not ServiceType.Equals(String.Empty) Then

                'delet old MOR if exist
                serviceCenterBO.DeletAllMethodOfRepairs()

                'add as new
                serviceCenterBO.AttachMethodOfRepair(GetMethodOfRepairID(ServiceType))

            End If


            If Not _ServiceCenterNameIsNull Then serviceCenterBO.Description = ServiceCenterName
            If Not _PhoneIsNull Then serviceCenterBO.Phone1 = Phone1
            If Not _FaxIsNull Then serviceCenterBO.Fax = Fax
            If Not _StatusCodeIsNull Then serviceCenterBO.StatusCode = StatusCode
            If Not _TaxIdIsNull Then serviceCenterBO.TaxId = TaxId
            If Not _CommentsIsNull Then serviceCenterBO.Comments = Comments
            If Not _DealerIdIsNull Then serviceCenterBO.OriginalDealerId = OriginalDealerId
            If Not _RatingCodeIsNull Then serviceCenterBO.RatingCode = RatingCode
            If Not _PaymentMethodIdIsNull Then serviceCenterBO.PaymentMethodId = PaymentMethodId

            'if me._servicegroup            serviceCenterBO.ServiceGroupId = Me.ServiceGroupId
            ' serviceCenterBO.PriceGroupId = Me.PriceGroupId

            If Not _countryIdIsNull Then serviceCenterBO.CountryId = CountryID
            If Not _RegistrationNumberIsNull Then serviceCenterBO.FtpAddress = RegistrationNumber

            'serviceCenterBO.Address.AddressRequiredServCenter = Me.AddressRequiredServCenter

            If SaveAddress = True Then
                serviceCenterBO.Address.Save()
            End If

            If bankInfoSave = True Then
                serviceCenterBO.Add_BankInfo()

                If Not _AccountNumberIsNull Then
                    serviceCenterBO.CurrentBankInfo.Account_Number = Account_Number
                Else
                    serviceCenterBO.CurrentBankInfo.Account_Number = Nothing
                End If

                If Not _BankIdIsNull Then
                    serviceCenterBO.CurrentBankInfo.Bank_Id = BankId
                Else
                    serviceCenterBO.CurrentBankInfo.Bank_Id = Nothing
                End If

                If Not _AccountNameIsNull Then serviceCenterBO.CurrentBankInfo.Account_Name = AccountName
                If Not _BankCountryCodeIsNull Then serviceCenterBO.CurrentBankInfo.CountryID = BankCountryID

                If Not _SwiftCodeIsNull Then
                    serviceCenterBO.CurrentBankInfo.SwiftCode = SwiftCode
                Else
                    serviceCenterBO.CurrentBankInfo.SwiftCode = Nothing
                End If

                If Not _IBANnumberIsNull Then
                    serviceCenterBO.CurrentBankInfo.IbanNumber = IBANnumber
                Else
                    serviceCenterBO.CurrentBankInfo.IbanNumber = Nothing
                End If

                If Not _AccountTypeIdIsNull Then serviceCenterBO.CurrentBankInfo.AccountTypeId = AccountTypeId

                serviceCenterBO.CurrentBankInfo.SourceCountryID = serviceCenterBO.CountryId

                serviceCenterBO.CurrentBankInfo.Save()
                serviceCenterBO.BankInfoId = serviceCenterBO.CurrentBankInfo.Id
            Else
                serviceCenterBO.BankInfoId = Nothing
            End If

            serviceCenterBO.Save()

            ' Set the acknoledge dataset
            'Dim ds As DataSet = New DataSet
            'Dim dt As DataTable = New DataTable(TABLE_RESULT)
            'dt.Columns.Add(TABLE_RESULT, GetType(String))
            'Dim rw As DataRow = dt.NewRow
            'rw(0) = VALUE_OK
            'dt.Rows.Add(rw)
            'ds.Tables.Add(dt)

            'Return XMLHelper.FromDatasetToXML(ds)

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            'Dim errList() As ValidationError = ex.ValidationErrorList()
            'If Not errList Is Nothing AndAlso errList.Length > 0 Then
            '    Throw New StoredProcedureGeneratedException("GalaxyInsertServiceCenter Error: ", errList(0).PropertyName & ": " & errList(0).Message)
            'End If
            Throw ex
        Catch ex As Exception
            Throw ex
            ' Throw New StoredProcedureGeneratedException("GalaxyInsertServiceCenter Error: ", Common.ErrorCodes.BO_INVALID_DATA)
        End Try
    End Function

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ByVal ds As GalaxyUpdateServiceCenterDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub Load(ByVal ds As GalaxyUpdateServiceCenterDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Service Center Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Public Function GetRegionID(ByVal regionCode As String, ByVal countryID As Guid) As Guid
        Dim regionID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetRegionLookupList(countryID)

        regionID = LookupListNew.GetIdFromCode(list, regionCode)

        Return regionID
    End Function

    Public Sub SetAddress(ByVal address1 As String, ByVal address2 As String, ByVal city As String, _
    ByVal regionCode As String, ByVal postalCode As String, ByVal countryID As Guid)

        Address = address1
        Me.City = city
        Me.PostalCode = postalCode
        RegionId = GetRegionID(regionCode, countryID)
        Me.CountryId = countryID
        AddressRequiredServCenter = True


    End Sub

    Public Function GetYesOrNo(ByVal yesOrNoStr As String) As Boolean
        Dim yesOrNo As Boolean = False

        If (Not yesOrNoStr Is Nothing And yesOrNoStr.Equals("Y")) Then
            yesOrNo = True
        End If

        Return yesOrNo
    End Function

    Public Function GetDealerID(ByVal originalDealerCode As String) As Guid
        Dim dealerId As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        dealerId = LookupListNew.GetIdFromCode(list, originalDealerCode)

        Return dealerId
    End Function

    Public Function GetPaymentMethodID(ByVal paymentMethod As String) As Guid
        Dim paymentMethodId As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        paymentMethodId = LookupListNew.GetIdFromCode(list, paymentMethod)

        Return paymentMethodId
    End Function

    'removed by REQ-263
    'Public Function GetServiceTypeID(ByVal serviceType As String) As Guid
    '    Dim serviceTypeID As Guid = Guid.Empty
    '    Dim list As DataView = LookupListNew.GetServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    '    serviceTypeID = LookupListNew.GetIdFromCode(list, serviceType)

    '    Return serviceTypeID
    'End Function

    'Public Function GetPriceGroupID(ByVal countryId As Guid, ByVal priceGroup As String) As Guid
    '    Dim priceGroupID As Guid = Guid.Empty
    '    Dim list As DataView = LookupListNew.GetPriceGroupLookupList(countryId)

    '    priceGroupID = LookupListNew.GetIdFromCode(list, priceGroup)

    '    Return priceGroupID
    'End Function

    'Public Function GetServiceGroupID(ByVal countryId As Guid, ByVal serviceGroup As String) As Guid
    '    Dim serviceGroupID As Guid = Guid.Empty
    '    Dim list As DataView = LookupListNew.GetServiceGroupLookupList(countryId)

    '    serviceGroupID = LookupListNew.GetIdFromCode(list, serviceGroup)

    '    Return serviceGroupID
    'End Function

    Public Function GetCountryID(ByVal countryCode As String) As Guid
        Dim countryID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetCountryLookupList()

        countryID = LookupListNew.GetIdFromCode(list, countryCode)

        Return countryID
    End Function

    Private Function GetAccountTypeID(ByVal AccountType As String) As Guid
        Dim accountTypeID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetAccountTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        accountTypeID = LookupListNew.GetIdFromCode(list, AccountType)

        Return accountTypeID
    End Function

    Public Function SetDataToNotNull(ByVal data As String, ByVal replaceWith As String) As String
        Dim retStr As String

        If (data Is Nothing Or data = "") Then
            retStr = replaceWith
        Else
            retStr = data
        End If

        Return retStr
    End Function

    Private Sub PopulateBOFromWebService(ByVal ds As GalaxyUpdateServiceCenterDs)
        Try
            If ds.GalaxyUpdateServiceCenter.Count = 0 Then Exit Sub
            With ds.GalaxyUpdateServiceCenter.Item(0)

                ServiceCenterCode = .SERVICE_CENTER_CODE 'required
                ServiceCenterId = ServiceCenter.GetServiceCenterID(ServiceCenterCode)

                If ServiceCenterId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If

                If Not .IsADDRESSNull Then
                    Address = .ADDRESS
                    _AddressIsNull = False
                End If
                If Not .IsCITYNull Then
                    City = .CITY
                    _CityIsNull = False
                End If
                If Not .IsPOSTAL_CODENull Then
                    PostalCode = .POSTAL_CODE
                    _PostalCodeIsNull = False
                End If
                If Not .IsSERVICE_CENTER_NAMENull Then
                    ServiceCenterName = .SERVICE_CENTER_NAME
                    _ServiceCenterNameIsNull = False
                End If
                If Not .IsRATING_CODENull Then
                    RatingCode = .RATING_CODE
                    _RatingCodeIsNull = False
                End If
                If Not .IsPHONENull Then
                    Phone1 = .PHONE
                    _PhoneIsNull = False
                End If
                If Not .IsFAXNull Then
                    Fax = .FAX
                    _FaxIsNull = False
                End If
                If Not .IsREGISTRATION_NUMBERNull Then
                    RegistrationNumber = .REGISTRATION_NUMBER
                    _RegistrationNumberIsNull = False
                End If
                If Not .IsTAX_IDNull Then
                    TaxId = .TAX_ID
                    _TaxIdIsNull = False
                End If
                If Not .IsSTATUS_CODENull Then
                    StatusCode = .STATUS_CODE
                    _StatusCodeIsNull = False
                End If
                If Not .IsCOMMENTSNull Then
                    Comments = .COMMENTS
                    _CommentsIsNull = False
                End If
                If Not .IsACCOUNT_NAMENull Then
                    AccountName = .ACCOUNT_NAME
                    _AccountNameIsNull = False
                End If
                If Not .IsBANK_IDNull Then
                    BankId = .BANK_ID
                    _BankIdIsNull = False
                End If
                If Not .IsACCOUNT_NUMBERNull Then
                    Account_Number = .ACCOUNT_NUMBER
                    _AccountNumberIsNull = False
                End If
                If Not .IsSWIFT_CODENull Then
                    SwiftCode = .SWIFT_CODE
                    _SwiftCodeIsNull = False
                End If
                If Not .IsIBAN_NUMBERNull Then
                    IBANnumber = .IBAN_NUMBER
                    _IBANnumberIsNull = False
                End If
                If Not .IsBANK_COUNTRY_CODENull Then
                    BankCountryCode = .BANK_COUNTRY_CODE
                    _BankCountryCodeIsNull = False
                End If
                If Not .IsACCOUNT_TYPENull Then
                    AccountType = .ACCOUNT_TYPE
                    _AccountTypeIsNull = False

                    AccountTypeId = GetAccountTypeID(AccountType)
                    _AccountTypeIdIsNull = False
                End If
                If Not .IsCOUNTRY_CODENull Then
                    CountryCode = .COUNTRY_CODE
                    _CountryCodeIsNull = False
                    CountryID = GetCountryID(.COUNTRY_CODE)
                    If CountryID.Equals(Guid.Empty) Then
                        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND)
                    Else
                        _countryIdIsNull = False
                    End If
                End If

                If Not .IsREGION_CODENull Then
                    RegionCode = .REGION_CODE
                    _RegionCodeIsNull = False
                    RegionId = GetRegionID(.REGION_CODE, CountryID)
                    If RegionId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_REGION_CODE)
                    Else
                        _RegionIdIsNull = False
                    End If
                End If

                If Not .IsORIGINAL_DEALER_CODENull Then
                    OriginalDealerCode = .ORIGINAL_DEALER_CODE
                    _OriginalDealerCodeIsNull = False
                    OriginalDealerId = GetDealerID(.ORIGINAL_DEALER_CODE)
                    If OriginalDealerId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_ORIGINAL_DEALER_CODE)
                    Else
                        _OriginalDealerCodeIsNull = False
                        ' this logic is based on the Service Center form. dealer code can only be assigned to one service center.
                        Dim dv As DataView = LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Guid.Empty)
                        Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, OriginalDealerId)
                        If dealerCode Is Nothing Or dealerCode = "" Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.ERR_ORIGINAL_DEALER_CODE_IN_USE)
                        End If
                    End If
                End If

                If Not .IsPAYMENT_METHODNull Then
                    PaymentMethod = .PAYMENT_METHOD
                    _PaymentMethodIdIsNull = False
                    PaymentMethodId = GetPaymentMethodID(.PAYMENT_METHOD)
                    _PaymentMethodIdIsNull = False

                    If .PAYMENT_METHOD = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                        If .IsACCOUNT_NAMENull OrElse .IsBANK_COUNTRY_CODENull Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_BANK_INFO_REQUIRED)
                        End If
                        If Not .IsBANK_COUNTRY_CODENull Then
                            BankCountryCode = .BANK_COUNTRY_CODE
                            _BankCountryCodeIsNull = False
                            BankCountryID = GetCountryID(.BANK_COUNTRY_CODE)
                            If BankCountryID.Equals(Guid.Empty) Then
                                Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND)
                            Else
                                _BankCountryIdIsNull = False
                            End If
                        End If
                        bankInfoSave = True
                    Else

                        If Not .IsACCOUNT_NAMENull OrElse Not .IsACCOUNT_NUMBERNull OrElse Not .IsBANK_IDNull OrElse Not .IsBANK_COUNTRY_CODENull OrElse Not .IsIBAN_NUMBERNull OrElse Not .IsSWIFT_CODENull Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_BANK_INFO_NOT_REQUIRED_ERR)
                        End If

                        bankInfoSave = False
                    End If
                End If

                'removed by REQ-263 (No change was made to the schema so that Galaxy does not have to do any change at their end; currently Galaxy
                'is hard coding this value because it was required in Elita.
                'If Not .IsSERVICE_TYPENull Then
                '    Me.ServiceType = .SERVICE_TYPE
                '    Me._ServiceTypeIsNull = False
                '    Me.ServiceTypeId = GetServiceTypeID(.SERVICE_TYPE)
                '    If Me.ServiceTypeId.Equals(Guid.Empty) Then
                '        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_TYPE)
                '    Else
                '        Me._ServiceTypeIdIsNull = False
                '    End If
                'End If

                'Added by REQ-452
                If Not .IsSERVICE_TYPENull Then ServiceType = .SERVICE_TYPE

            End With

        Catch ex As Exception
            Throw ex
            'Throw New ElitaPlusException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

#End Region

#Region "Public Properties"

    <ValidStringLength("", Max:=200)> _
  Public Property Address As String
        Get
            CheckDeleted()
            If Row(Source_Col_Address) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Address), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Address, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(Source_Col_City) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_City), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_City, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
Public Property RegionCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REGION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REGION_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_REGION_CODE, MiscUtil.ConvertToUpper(Value))
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Postal_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Postal_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Postal_Code, MiscUtil.ConvertToUpper(Value))
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property CountryCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Country_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Country_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Country_Code, Value)
        End Set
    End Property
    'removed by REQ-263
    'Public Property ServiceType() As String
    '    Get
    '        CheckDeleted()
    '        If Row(SOURCE_COL_SERVICE_TYPE) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(SOURCE_COL_SERVICE_TYPE), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(SOURCE_COL_SERVICE_TYPE, Value)
    '    End Set
    'End Property

    'Added by REQ-452
    Public Property ServiceType As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SERVICE_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property ServiceCenterCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Service_Center_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Service_Center_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Service_Center_Code, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property ServiceCenterName As String
        Get
            CheckDeleted()
            If Row(Source_Col_Service_Center_Name) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Service_Center_Name), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Service_Center_Name, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4)> _
        Public Property RatingCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_RATING_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_RATING_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_RATING_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)> _
    Public Property Phone1 As String
        Get
            CheckDeleted()
            If Row(Source_Col_Phone) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Phone), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Phone, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)> _
    Public Property Fax As String
        Get
            CheckDeleted()
            If Row(Source_Col_Fax) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Fax), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Fax, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120)> _
Public Property RegistrationNumber As String
        Get
            CheckDeleted()
            If Row(Source_Col_Registration_Number) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Registration_Number), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Registration_Number, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)> _
    Public Property TaxId As String
        Get
            CheckDeleted()
            If Row(Source_Col_Tax_Id) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Tax_Id), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Tax_Id, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4)> _
    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Status_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Status_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Status_Code, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4000)> _
    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(Source_Col_Comments) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Comments), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Comments, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property AccountName As String
        Get
            CheckDeleted()
            If Row(Source_Col_Account_Name) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Account_Name), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Account_Name, Value)
        End Set
    End Property

    Public Property BankId As String
        Get
            CheckDeleted()
            If Row(Source_Col_Bank_Id) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Bank_Id), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Bank_Id, Value)
        End Set
    End Property

    Public Property Account_Number As String
        Get
            CheckDeleted()
            If Row(Source_Col_Account_Number) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Account_Number), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Account_Number, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=160)> _
       Public Property IBANnumber As String
        Get
            CheckDeleted()
            If Row(Source_Col_IBAN_Number) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_IBAN_Number), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_IBAN_Number, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=160)> _
       Public Property SwiftCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Swift_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Swift_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Swift_Code, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property BankCountryCode As String
        Get
            CheckDeleted()
            If Row(Source_Col_Bank_Country_Code) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Bank_Country_Code), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Bank_Country_Code, Value)
        End Set
    End Property

    Public Property AccountType As String
        Get
            CheckDeleted()
            If Row(Source_Col_Account_Type) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Source_Col_Account_Type), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(Source_Col_Account_Type, Value)
        End Set
    End Property

    Public Property PaymentMethod As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PAYMENT_METHOD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PAYMENT_METHOD, Value)
        End Set
    End Property

    Public Property OriginalDealerCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ORIGINAL_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ORIGINAL_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ORIGINAL_DEALER_CODE, Value)
        End Set
    End Property

    'Added for REQ-452
    Public Function GetMethodOfRepairID(ByVal serviceType As String) As ArrayList
        Dim MethodOfRepairID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        MethodOfRepairID = LookupListNew.GetIdFromCode(list, serviceType)
        Dim al As New ArrayList
        al.Add(MethodOfRepairID.ToString)

        Return al
    End Function
#End Region

End Class


