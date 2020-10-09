
Public Class GalaxyInsertServiceCenter
    Inherits BusinessObjectBase

#Region "Member Variables"

    Private Const ORACLE_UNIQUE_CONSTRAINT_ERR As Integer = 1
    Private Const ORACLE_LENGHT_EXCEEDED_ERR As Integer = 1401
    Private Const ORACLE_INTEGRITY_CONSTRAINT_VIOLATION As Integer = 2292

    Private bankInfoSave As Boolean
    Private RegionId As Guid
    Private CountryID As Guid
    Private BankCountryID As Guid
    Private AddressRequiredServCenter As Boolean
    Private OriginalDealerId As Guid
    Private Id As Guid
    'Private PriceGroupId As Guid ---commented for REQ-1106
    Private PriceListId As Guid
    Private PriceListCode As String
    Private ServiceGroupId As Guid
    'Private ServiceTypeId As Guid 'removed by REQ-263
    Private PaymentMethodId As Guid
    Private AccountTypeID As Guid
    Private ServiceGroup As String
    'Private PriceGroup As String

#End Region

#Region "Constants"

    Public TABLE_NAME As String = "GalaxyInsertServiceCenter"
    Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"


    Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const SOURCE_COL_SERVICE_CENTER_NAME As String = "SERVICE_CENTER_NAME"
    Private Const SOURCE_COL_ADDRESS As String = "ADDRESS"
    Private Const SOURCE_COL_CITY As String = "CITY"
    Private Const SOURCE_COL_REGION_CODE As String = "REGION_CODE"
    Private Const SOURCE_COL_POSTAL_CODE As String = "POSTAL_CODE"
    Private Const SOURCE_COL_COUNTRY_CODE As String = "COUNTRY_CODE"
    Private Const SOURCE_COL_DEFAULT_TO_EMAIL_FLAG As String = "DEFAULT_TO_EMAIL_FLAG"
    Private Const SOURCE_COL_STATUS_CODE As String = "STATUS_CODE"
    Private Const SOURCE_COL_PRICE_GROUP As String = "PRICE_GROUP"
    Private Const SOURCE_COL_TAX_ID As String = "TAX_ID"
    Private Const SOURCE_COL_CREATED_BY As String = "CREATED_BY"
    Private Const SOURCE_COL_SERVICE_TYPE As String = "SERVICE_TYPE"  'removed by REQ-263
    Private Const SOURCE_COL_SERVICE_GROUP As String = "SERVICE_GROUP"
    Private Const SOURCE_COL_PHONE As String = "PHONE"
    Private Const SOURCE_COL_FAX As String = "FAX"
    Private Const SOURCE_COL_COMMENTS As String = "COMMENTS"
    Private Const SOURCE_COL_ORIGINAL_DEALER_CODE As String = "ORIGINAL_DEALER_CODE"
    Private Const SOURCE_COL_RATING_CODE As String = "RATING_CODE"
    Private Const SOURCE_COL_PAYMENT_METHOD As String = "PAYMENT_METHOD"
    Private Const SOURCE_COL_ACCOUNT_NAME As String = "ACCOUNT_NAME"
    Private Const SOURCE_COL_BANK_ID As String = "BANK_ID"
    Private Const SOURCE_COL_ACCOUNT_NUMBER As String = "ACCOUNT_NUMBER"
    Private Const SOURCE_COL_BANK_COUNTRY_CODE As String = "BANK_COUNTRY_CODE"
    Private Const SOURCE_COL_IBAN_NUMBER As String = "IBAN_NUMBER"
    Private Const SOURCE_COL_SWIFT_CODE As String = "SWIFT_CODE"
    Private Const SOURCE_COL_ACCOUNT_TYPE As String = "ACCOUNT_TYPE"
    Private Const SOURCE_COL_IVA_RESPONSIBLE_FLAG As String = "IVA_RESPONSIBLE_FLAG"
    Private Const SOURCE_COL_BUSINESS_HOURS As String = "BUSINESS_HOURS"
    Private Const SOURCE_COL_CONTACT_NAME As String = "CONTACT_NAME"
    Private Const SOURCE_COL_SERVICE_WARRANTY_DAYS As String = "SERVICE_WARRANTY_DAYS"
    Private Const SOURCE_COL_CC_EMAIL As String = "CC_EMAIL"
    Private Const SOURCE_COL_EMAIL As String = "EMAIL"
    Private Const SOURCE_COL_OWNER_NAME As String = "OWNER_NAME"
    Private Const SOURCE_COL_REGISTRATION_NUMBER As String = "REGISTRATION_NUMBER"

#End Region

#Region "Constructors"

    Public Sub New(ds As GalaxyInsertServiceCenterDs)
        MyBase.New()
        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim serviceCenterBO As New ServiceCenter
            serviceCenterBO.ReverseLogisticsId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
            serviceCenterBO.Code = Code
            serviceCenterBO.Description = Description
            serviceCenterBO.Phone1 = Phone1
            serviceCenterBO.Fax = Fax
            serviceCenterBO.IvaResponsibleFlag = False
            serviceCenterBO.ServiceWarrantyDays = 0
            serviceCenterBO.StatusCode = StatusCode
            serviceCenterBO.TaxId = TaxId
            serviceCenterBO.Comments = Comments
            serviceCenterBO.OriginalDealerId = OriginalDealerId
            serviceCenterBO.RatingCode = RatingCode
            serviceCenterBO.PaymentMethodId = PaymentMethodId
            'serviceCenterBO.ServiceTypeId = Me.ServiceTypeId 'removed by REQ-263
            serviceCenterBO.AttachMethodOfRepair(GetMethodOfRepairID(ServiceType)) 'Added by REQ-452
            serviceCenterBO.ServiceGroupId = ServiceGroupId
            'serviceCenterBO.PriceGroupId = Me.PriceGroupId ' Commented for REQ-1106; PricegroupId column will be dropped from ELP_SERVICE_CENTER table
            serviceCenterBO.PriceListCode = PriceListCode
            serviceCenterBO.CountryId = CountryID
            serviceCenterBO.LoanerFlag = "N"
            serviceCenterBO.MasterFlag = "N"
            serviceCenterBO.FtpAddress = FtpAddress
            serviceCenterBO.ContactName = "."
            serviceCenterBO.OwnerName = "."
            serviceCenterBO.DefaultToEmailFlag = False
            serviceCenterBO.Shipping = False

            serviceCenterBO.Address.AddressRequiredServCenter = AddressRequiredServCenter
            serviceCenterBO.Address.Address1 = Address
            serviceCenterBO.Address.City = City
            serviceCenterBO.Address.PostalCode = PostalCode
            serviceCenterBO.Address.RegionId = RegionId
            serviceCenterBO.Address.CountryId = CountryID
            serviceCenterBO.Address.Save()

            If (bankInfoSave = True) Then
                serviceCenterBO.Add_BankInfo()
                serviceCenterBO.CurrentBankInfo.Account_Number = Account_Number
                serviceCenterBO.CurrentBankInfo.Bank_Id = Bank_Id
                serviceCenterBO.CurrentBankInfo.Account_Name = Account_Name
                serviceCenterBO.CurrentBankInfo.SourceCountryID = CountryID
                serviceCenterBO.CurrentBankInfo.CountryID = CountryID 'Me.BankCountryID
                serviceCenterBO.CurrentBankInfo.SwiftCode = SWIFT_CODE
                serviceCenterBO.CurrentBankInfo.IbanNumber = IBAN_NUMBER
                serviceCenterBO.CurrentBankInfo.AccountTypeId = AccountTypeID
                serviceCenterBO.CurrentBankInfo.Save()
                serviceCenterBO.BankInfoId = serviceCenterBO.CurrentBankInfo.Id
            End If

            serviceCenterBO.Save()


            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As DALConcurrencyAccessException
            Throw ex
        Catch ex As DataBaseUniqueKeyConstraintViolationException
            Throw ex
        Catch ex As Exception
            Throw New StoredProcedureGeneratedException("GalaxyInsertServiceCenter Error: ", Common.ErrorCodes.BO_INVALID_DATA)
        End Try
    End Function

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ds As GalaxyInsertServiceCenterDs)

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

    Private Sub Load(ds As GalaxyInsertServiceCenterDs)
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

    Public Function GetRegionID(regionCode As String, countryID As Guid) As Guid
        Dim regionID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetRegionLookupList(countryID)

        regionID = LookupListNew.GetIdFromCode(list, regionCode)

        Return regionID
    End Function

    Public Sub SetAddress(address1 As String, address2 As String, city As String, _
    regionCode As String, postalCode As String, countryID As Guid)

        Address = address1
        Me.City = city
        Me.PostalCode = postalCode
        RegionId = GetRegionID(regionCode, countryID)
        Me.CountryId = countryID
        AddressRequiredServCenter = True


    End Sub

    Public Function GetYesOrNo(yesOrNoStr As String) As Boolean
        Dim yesOrNo As Boolean = False

        If (yesOrNoStr IsNot Nothing AndAlso yesOrNoStr.Equals("Y")) Then
            yesOrNo = True
        End If

        Return yesOrNo
    End Function

    Public Function GetDealerID(originalDealerCode As String) As Guid
        Dim dealerId As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        dealerId = LookupListNew.GetIdFromCode(list, originalDealerCode)

        Return dealerId
    End Function

    Private Function GetPaymentMethodID(paymentMethod As String) As Guid
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

    'Added for REQ-452
    Public Function GetMethodOfRepairID(serviceType As String) As ArrayList
        Dim MethodOfRepairID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        MethodOfRepairID = LookupListNew.GetIdFromCode(list, serviceType)
        Dim al As New ArrayList
        al.Add(MethodOfRepairID.ToString)

        Return al
    End Function
    'Public Function GetPriceGroupID(ByVal countryId As Guid, ByVal priceGroup As String) As Guid
    '    Dim priceGroupID As Guid = Guid.Empty
    '    Dim list As DataView = LookupListNew.GetPriceGroupLookupList(countryId)

    'priceGroupID = LookupListNew.GetIdFromCode(list, priceGroup)

    '    Return priceGroupID
    'End Function

    Public Function GetPriceListID(countryId As Guid, priceList As String) As Guid
        Dim priceListID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetPriceListLookupList(countryId)

        priceListID = LookupListNew.GetIdFromCode(list, PriceListCode)

        Return priceListID
    End Function

    Public Function GetServiceGroupID(countryId As Guid, serviceGroup As String) As Guid
        Dim serviceGroupID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetServiceGroupLookupList(countryId)

        serviceGroupID = LookupListNew.GetIdFromCode(list, serviceGroup)

        Return serviceGroupID
    End Function

    Public Function GetCountryID(countryCode As String) As Guid
        Dim countryID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetCountryLookupList()

        countryID = LookupListNew.GetIdFromCode(list, countryCode)

        Return countryID
    End Function

    Public Function SetDataToNotNull(data As String, replaceWith As String) As String
        Dim retStr As String

        If (data Is Nothing OrElse data = "") Then
            retStr = replaceWith
        Else
            retStr = data
        End If

        Return retStr
    End Function
    Public Function GetAccountTypeID(AccountType As String) As Guid
        Dim accountTypeID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetAccountTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        accountTypeID = LookupListNew.GetIdFromCode(list, AccountType)

        Return accountTypeID
    End Function

    Private Sub PopulateBOFromWebService(ds As GalaxyInsertServiceCenterDs)
        Try
            If ds.GalaxyInsertServiceCenter.Count = 0 Then Exit Sub
            With ds.GalaxyInsertServiceCenter.Item(0)
                CountryCode = .COUNTRY_CODE
                CountryID = GetCountryID(.COUNTRY_CODE)
                If CountryID.Equals(Guid.Empty) Then
                    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND)
                End If

                Code = .SERVICE_CENTER_CODE
                Description = .SERVICE_CENTER_NAME
                Address = .ADDRESS
                City = .CITY
                PostalCode = .POSTAL_CODE
                RegionCode = .REGION_CODE

                RegionId = GetRegionID(.REGION_CODE, CountryID)
                If RegionId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_REGION_CODE)
                End If

                AddressRequiredServCenter = True

                If Not .IsPHONENull Then
                    Phone1 = .PHONE
                Else
                    Phone1 = "."
                End If

                If Not .IsFAXNull Then
                    Fax = .FAX
                Else
                    Fax = "."
                End If

                If Not .IsCOMMENTSNull Then
                    Comments = .COMMENTS
                Else
                    Comments = "."
                End If

                If Not .IsRATING_CODENull Then RatingCode = .RATING_CODE

                StatusCode = .STATUS_CODE
                TaxId = .TAX_ID

                If Not .IsORIGINAL_DEALER_CODENull Then
                    OriginalDealerCode = .ORIGINAL_DEALER_CODE
                    OriginalDealerId = GetDealerID(.ORIGINAL_DEALER_CODE)
                    If OriginalDealerId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_ORIGINAL_DEALER_CODE)
                    Else
                        ' this logic is based on the Service Center form. dealer code can only be assigned to one service center.
                        Dim dv As DataView = LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Guid.Empty)
                        Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, OriginalDealerId)
                        If dealerCode Is Nothing OrElse dealerCode = "" Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.ERR_ORIGINAL_DEALER_CODE_IN_USE)
                        End If
                    End If
                End If

                If Not .IsREGISTRATION_NUMBERNull Then FtpAddress = .REGISTRATION_NUMBER
                PaymentMethod = .PAYMENT_METHOD
                PaymentMethodId = GetPaymentMethodID(.PAYMENT_METHOD)
                If .PAYMENT_METHOD = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    'If .IsACCOUNT_NAMENull OrElse .IsACCOUNT_NUMBERNull OrElse .IsBANK_IDNull OrElse .IsBANK_COUNTRY_CODENull Then
                    '    Throw New ElitaPlusException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_BANK_INFO_REQUIRED)
                    'End If

                    If Not .IsACCOUNT_NAMENull Then Account_Name = .ACCOUNT_NAME
                    If Not .IsACCOUNT_NUMBERNull Then Account_Number = .ACCOUNT_NUMBER
                    If Not .IsBANK_IDNull Then Bank_Id = .BANK_ID
                    If Not .IsIBAN_NUMBERNull Then IBAN_NUMBER = .IBAN_NUMBER
                    If Not .IsSWIFT_CODENull Then SWIFT_CODE = .SWIFT_CODE

                    If Not .IsBANK_COUNTRY_CODENull Then
                        BankCountryCode = .BANK_COUNTRY_CODE
                        BankCountryID = GetCountryID(.BANK_COUNTRY_CODE)
                        If BankCountryID.Equals(Guid.Empty) Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND)
                        End If
                    End If

                    If Not .IsACCOUNT_TYPENull Then
                        ACCOUNT_TYPE = .ACCOUNT_TYPE
                        AccountTypeID = GetAccountTypeID(.ACCOUNT_TYPE)
                        If AccountTypeID.Equals(Guid.Empty) Then
                            Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_BANK_ACCOUNT_TYPE)
                        End If
                    End If
                    bankInfoSave = True
                Else
                    If Not .IsACCOUNT_NAMENull OrElse Not .IsACCOUNT_NUMBERNull OrElse Not .IsBANK_IDNull OrElse Not .IsBANK_COUNTRY_CODENull OrElse Not .IsIBAN_NUMBERNull OrElse Not .IsSWIFT_CODENull Then
                        Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_BANK_INFO_NOT_REQUIRED_ERR)
                    End If

                    bankInfoSave = False
                End If
                ServiceGroup = "VSC01" 'VSC Default value
                'Me.PriceGroup = "VSC01" 'VSC Default value
                PriceListCode = "VSC01"
                'removed by REQ-263 (No change was made to the schema so that Galaxy does not have to do any change at their end; currently Galaxy
                'is hard coding this value because it was required in Elita.
                ServiceType = .SERVICE_TYPE  ' keep this line so that the xml row does not break.
                'Me.ServiceTypeId = GetServiceTypeID(.SERVICE_TYPE)
                'If Me.ServiceTypeId.Equals(Guid.Empty) Then
                '    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_TYPE)
                'End If

                'Me.PriceGroupId = GetPriceGroupID(CountryID, Me.PriceGroup)
                'If Me.PriceGroupId.Equals(Guid.Empty) Then
                '    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_PRICE_GROUP)
                'End If
                PriceListId = GetPricelistID(CountryID, PriceListCode)
                If PriceListId.Equals(String.Empty) Then
                    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_PRICE_LIST)
                End If
                ServiceGroupId = GetServiceGroupID(CountryID, ServiceGroup)
                If ServiceGroupId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("Service Center Invalid Parameters Error", Common.ErrorCodes.INVALID_SERVICE_GROUP)
                End If
            End With

        Catch ex As Exception
            Throw ex
            'Throw New ElitaPlusException("Service Center Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

#End Region

#Region "Public Properties"


    <ValidStringLength("", Max:=30)> _
Public Property FtpAddress As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REGISTRATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REGISTRATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_REGISTRATION_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SERVICE_CENTER_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CENTER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CENTER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SERVICE_CENTER_NAME, Value)
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

    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property Phone1 As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PHONE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property Fax As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_FAX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_FAX, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property TaxId As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_TAX_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_STATUS_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1000)> _
    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_COMMENTS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
Public Property Address As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ADDRESS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CITY, Value)
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

    <ValidStringLength("", Max:=25)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_POSTAL_CODE, MiscUtil.ConvertToUpper(Value))
        End Set
    End Property

    Public Property CountryCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_COUNTRY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_COUNTRY_CODE, Value)
        End Set
    End Property


    Public Property Account_Name As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ACCOUNT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ACCOUNT_NAME, Value)
        End Set
    End Property

    Public Property Bank_Id As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_BANK_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_BANK_ID, Value)
        End Set
    End Property

    Public Property Account_Number As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    Public Property BankCountryCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_BANK_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_BANK_COUNTRY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_BANK_COUNTRY_CODE, Value)
        End Set
    End Property

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

    Public Property IBAN_NUMBER As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_IBAN_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_IBAN_NUMBER, Value)
        End Set
    End Property

    Public Property SWIFT_CODE As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SWIFT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SWIFT_CODE, Value)
        End Set
    End Property

    Public Property ACCOUNT_TYPE As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ACCOUNT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ACCOUNT_TYPE, Value)
        End Set
    End Property
#End Region

End Class

