Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.BusinessObjectsNew.PolicyService

Public Class CertAddController

#Region "Constants"
    Public Const ERR_NO_DEALER_CONTRACT_FOUND As String = "NO_DEALER_CONTRACT_FOUND"
    Public Const ERR_CONTRACT_NOT_FOUND As String = "CONTRACT_NOT_FOUND"
    Public Const ERR_TOO_MANY_BUNDLED_ITEMS As String = "Too many bundled items. Up to 10 items allowed"
    Public Const ERR_DEALER_CURRENCY_NOT_FOUND As String = "ERR_DEALER_CURRENCY_NOT_FOUND"
    Public Const ERR_DEALER_COUNTRY_NOT_FOUND As String = "ERR_DEALER_COUNTRY_NOT_FOUND"
    Public Const ERR_SALU_MISSING As String = "INVALID_SALUTATION"
    Public Const ERR_CUST_NAME_MISSING As String = "MISSING_CUSTOMER_NAME_ERR"
    Public Const ERR_ADR1_MISSING As String = "ADDRESS1_FIELD_IS_REQUIRED"
    Public Const ERR_ADR2_MISSING As String = "ADDRESS_REQUIRED"
    Public Const ERR_ADR3_MISSING As String = "ADDRESS_REQUIRED"
    Public Const ERR_CITY_MISSING As String = "CITY_REQUIRED"
    Public Const ERR_ZIP_MISSING As String = "ZIP_IS_MISSING"
    Public Const ERR_STATE_MISSING As String = "ERR_INVALID_STATE"
    Public Const ERR_COUNTRY_MISSING As String = "ERR_INVALID_COUNTRY"
    Public Const ERR_EMAIL_MISSING As String = "EMAIL_IS_REQUIRED_ERR"
    Public Const ERR_WORK_PHONE_MISSING As String = "CELL_PHONE_NUMBER_IS_REQUIRED"
    Public Const ERR_HOME_PHONE_MISSING As String = "HOME_PHONE_IS_REQUIRED"
    Public Const ERR_REGION_MISSING As String = "REGION_IS_REQUIRED"
    Public Const MAX_BUNDLED_ITEMS_ALLOWED As Integer = 10
#End Region
#Region "Private members"
    Private _TransID As Guid
    Private _DealerID As Guid
    Private _Salutation As String
    Private _ProductConversion As Boolean

    Private _CertNum As String
    Private _ProdCode As String
    Private _CertDuration As Integer
    Private _MfgWarranty As Integer
    Private _WarrantySalesDate As Date
    Private _ProductSalesDate As Date
    Private _DateOfBirth As Date
    Private _WarrantyPrice As Double
    Private _RetailPrice As Double
    Private _InvoiceNumber As String
    Private _BranchCode As String
    Private _SalesRepNumber As String
    Private _Make As String
    Private _Model As String
    Private _SerialNumber As String
    Private _ItemCode As String
    Private _ItemDesc As String
    Private _CustName As String
    Private _CustTaxID As String
    Private _CustHomePhone As String
    Private _CustWorkPhone As String
    Private _CustEmail As String
    Private _CustAddress1 As String
    Private _CustAddress2 As String
    Private _CustCity As String
    Private _CustState As String
    Private _CustZip As String
    Private _CustCountry As String
    Private _CurrencyCode As String
    Private _PurchaseCountry As String

    Private _BundledItems As Generic.List(Of BundledItem)

    Private _BillingFrequency As Integer
    Private _NumOfInstallments As Integer
    Private _InstallmentAmt As Double
    Private _BankAcctOwnerName As String
    Private _BankAcctNum As String
    Private _BankRoutingNum As String
    Private _PaymentType As String
    Private _Occupation As String
    Private _MembershipNum As String
    Private _MembershipType As String
    Private _SkuNumber As String
    Private _SubscriberStatus As String
    Private _PostPrePaid As String
    Private _BillingPlan As String
    Private _BillingCycle As String
    Private _RecordType As String
    Private _KeepEnrollmentFileWhenErr As Integer
    Private _PersonType As String
    Private _Gender As String
    Private _MaritalStatus As String
    Private _Nationality As String
    Private _PlaceOfBirth As String
    Private _CUIT_CUIL As String
    Private _MarketingPromoSer As String
    Private _MarketingPromoNum As String
    Private _salesChannel As String
    Private _serviceLineNumber As String

    Public SalutationCode As String
    private _CertDealer As Dealer
    Public _FirstName As String
    Public _LastName As String
    Public _MiddleName As String
    Public _UseCustomerProfile As String

#End Region

#Region "Constructors"
    Public Sub New()
        _TransID = New Guid
        _PaymentType = "1"
    End Sub

    Public Sub New(ByVal pDealerID As Guid)
        _TransID = New Guid
        _PaymentType = "1"
        DealerID = pDealerID
        _CertDealer = New Dealer(DealerID)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property TransID() As Guid
        Get
            Return _TransID
        End Get
    End Property

    Public Property DealerID() As Guid
        Get
            Return _DealerID
        End Get
        Set(ByVal Value As Guid)
            _DealerID = Value
            SetProductConversion()
        End Set
    End Property

    Public Readonly Property CertDealer() As Dealer
        Get
            if _CertDealer is Nothing Then
                _CertDealer = New Dealer(DealerID)
            End If
            Return _CertDealer
        End Get
    End Property

    Public ReadOnly Property ProductConversion() As Boolean
        Get
            Return _ProductConversion
        End Get
    End Property

    Public Property CertNum() As String
        Get
            Return _CertNum
        End Get
        Set(ByVal Value As String)
            _CertNum = Left(Value, 20)
        End Set
    End Property

    Public Property ProductCode() As String
        Get
            Return _ProdCode
        End Get
        Set(ByVal Value As String)
            _ProdCode = Left(Value, 20)
        End Set
    End Property

    Public Property CertDuration() As Integer
        Get
            Return _CertDuration
        End Get
        Set(ByVal Value As Integer)
            _CertDuration = Value
        End Set
    End Property

    Public Property ManufacturerDuration() As Integer
        Get
            Return _MfgWarranty
        End Get
        Set(ByVal Value As Integer)
            _MfgWarranty = Value
        End Set
    End Property

    Public Property WarrantySalesDate() As Date
        Get
            Return _WarrantySalesDate
        End Get
        Set(ByVal Value As Date)
            _WarrantySalesDate = Value
        End Set
    End Property

    Public Property ProductSalesDate() As Date
        Get
            Return _ProductSalesDate
        End Get
        Set(ByVal Value As Date)
            _ProductSalesDate = Value
        End Set
    End Property

    Public Property DateOfBirth() As Date
        Get
            Return _DateOfBirth
        End Get
        Set(ByVal Value As Date)
            _DateOfBirth = Value
        End Set
    End Property

    Public Property WarrantyPrice() As Double
        Get
            Return _WarrantyPrice
        End Get
        Set(ByVal Value As Double)
            _WarrantyPrice = Value
        End Set
    End Property

    Public Property ProductRetailPrice() As Double
        Get
            Return _RetailPrice
        End Get
        Set(ByVal Value As Double)
            _RetailPrice = Value
        End Set
    End Property

    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal Value As String)
            _InvoiceNumber = Left(Value, 50)
        End Set
    End Property

    Public Property BranchCode() As String
        Get
            Return _BranchCode
        End Get
        Set(ByVal Value As String)
            _BranchCode = Left(Value, 10)
        End Set
    End Property

    Public Property SalesRepNumber() As String
        Get
            Return _SalesRepNumber
        End Get
        Set(ByVal Value As String)
            _SalesRepNumber = Left(Value, 30)
        End Set
    End Property

    Public Property Make() As String
        Get
            Return _Make
        End Get
        Set(ByVal Value As String)
            _Make = Left(Value, 50)
        End Set
    End Property

    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal Value As String)
            _Model = Left(Value, 30)
        End Set
    End Property

    Public Property SerialNumber() As String
        Get
            Return _SerialNumber
        End Get
        Set(ByVal Value As String)
            _SerialNumber = Left(Value, 30)
        End Set
    End Property

    Public Property ItemCode() As String
        Get
            Return _ItemCode
        End Get
        Set(ByVal Value As String)
            _ItemCode = Left(Value, 10)
        End Set
    End Property

    Public Property ItemDescription() As String
        Get
            Return _ItemDesc
        End Get
        Set(ByVal Value As String)
            _ItemDesc = Left(Value, 50)
        End Set
    End Property
    Public Property Salutation() As String
        Get
            Return _Salutation
        End Get
        Set(ByVal Value As String)
            _Salutation = Value
        End Set
    End Property
    Public Property CustomerName() As String
        Get
            Return _CustName
        End Get
        Set(ByVal Value As String)
            _CustName = Left(Value, 50)
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal Value As String)
            _FirstName = Left(Value, 50)
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return _MiddleName
        End Get
        Set(ByVal Value As String)
            _MiddleName = Left(Value, 50)
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal Value As String)
            _LastName = Left(Value, 50)
        End Set
    End Property
    Public Property CustomerTaxID() As String
        Get
            Return _CustTaxID
        End Get
        Set(ByVal Value As String)
            _CustTaxID = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerHomePhone() As String
        Get
            Return _CustHomePhone
        End Get
        Set(ByVal Value As String)
            _CustHomePhone = Left(Value, 20)
        End Set
    End Property

    Public Property CustomerWorkPhone() As String
        Get
            Return _CustWorkPhone
        End Get
        Set(ByVal Value As String)
            _CustWorkPhone = Left(Value, 20)
        End Set
    End Property

    Public Property CustomerEmail() As String
        Get
            Return _CustEmail
        End Get
        Set(ByVal Value As String)
            _CustEmail = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerAddress1() As String
        Get
            Return _CustAddress1
        End Get
        Set(ByVal Value As String)
            _CustAddress1 = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerAddress2() As String
        Get
            Return _CustAddress2
        End Get
        Set(ByVal Value As String)
            _CustAddress2 = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerCity() As String
        Get
            Return _CustCity
        End Get
        Set(ByVal Value As String)
            _CustCity = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerState() As String
        Get
            Return _CustState
        End Get
        Set(ByVal Value As String)
            _CustState = Left(Value, 50)
        End Set
    End Property

    Public Property CustomerZIP() As String
        Get
            Return _CustZip
        End Get
        Set(ByVal Value As String)
            _CustZip = Left(Value, 10)
        End Set
    End Property

    Public Property CustomerCountryISOCode() As String
        Get
            Return _CustCountry
        End Get
        Set(ByVal Value As String)
            _CustCountry = Left(Value, 2)
        End Set
    End Property

    Public Property PurchaseCountryISOCode() As String
        Get
            Return _PurchaseCountry
        End Get
        Set(ByVal Value As String)
            _PurchaseCountry = Left(Value, 2)
        End Set
    End Property

    Public Property CurrencyISOCode() As String
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal Value As String)
            _CurrencyCode = Left(Value, 3)
        End Set
    End Property

    Public ReadOnly Property BundledItems() As Generic.List(Of BundledItem)
        Get
            Return _BundledItems
        End Get
    End Property

    Public Property BillingFrequency() As Integer
        Get
            Return _BillingFrequency
        End Get
        Set(ByVal Value As Integer)
            _BillingFrequency = Value
        End Set
    End Property

    Public Property NumOfInstallments() As Integer
        Get
            Return _NumOfInstallments
        End Get
        Set(ByVal Value As Integer)
            _NumOfInstallments = Value
        End Set
    End Property

    Public Property InstallmentAmount() As Double
        Get
            Return _InstallmentAmt
        End Get
        Set(ByVal Value As Double)
            _InstallmentAmt = Value
        End Set
    End Property

    Public Property BankAcctOwnerName() As String
        Get
            Return _BankAcctOwnerName
        End Get
        Set(ByVal Value As String)
            _BankAcctOwnerName = Left(Value, 50)
        End Set
    End Property

    Public Property BankAcctNumber() As String
        Get
            Return _BankAcctNum
        End Get
        Set(ByVal Value As String)
            _BankAcctNum = Left(Value, 29)
        End Set
    End Property

    Public Property BankRoutingNumber() As String
        Get
            Return _BankRoutingNum
        End Get
        Set(ByVal Value As String)
            _BankRoutingNum = Left(Value, 10)
        End Set
    End Property

    Public Property PaymentType() As String
        Get
            Return _PaymentType
        End Get
        Set(ByVal Value As String)
            _PaymentType = Left(Value, 1)
        End Set
    End Property
    Public Property Occupation() As String
        Get
            Return _Occupation
        End Get
        Set(ByVal Value As String)
            _Occupation = Left(Value, 80)
        End Set
    End Property
    Public Property MembershipNum() As String
        Get
            Return _MembershipNum
        End Get
        Set(ByVal Value As String)
            _MembershipNum = Left(Value, 30)
        End Set
    End Property

    Public Property MembershipType() As String
        Get
            Return _MembershipType
        End Get
        Set(ByVal Value As String)
            _MembershipType = Left(Value, 50)
        End Set
    End Property

    Public Property SkuNumber() As String
        Get
            Return _SkuNumber
        End Get
        Set(ByVal Value As String)
            _SkuNumber = Left(Value, 50)
        End Set
    End Property

    Public Property SubscriberStatus() As String
        Get
            Return _SubscriberStatus
        End Get
        Set(ByVal Value As String)
            _SubscriberStatus = Left(Value, 5)
        End Set
    End Property

    Public Property PostPrePaid() As String
        Get
            Return _PostPrePaid
        End Get
        Set(ByVal Value As String)
            _PostPrePaid = Left(Value, 10)
        End Set
    End Property

    Public Property BillingPlan() As String
        Get
            Return _BillingPlan
        End Get
        Set(ByVal Value As String)
            _BillingPlan = Left(Value, 10)
        End Set
    End Property

    Public Property RecordType() As String
        Get
            Return _RecordType
        End Get
        Set(ByVal Value As String)
            _RecordType = Left(Value, 2)
        End Set
    End Property

    Public Property BillingCycle() As String
        Get
            Return _BillingCycle
        End Get
        Set(ByVal Value As String)
            _BillingCycle = Left(Value, 10)
        End Set
    End Property

    '_KeepEnrollmentFileWhenErr
    Public Property KeepEnrollmentFileWhenErr() As Boolean
        Get
            Return _KeepEnrollmentFileWhenErr = 1
        End Get
        Set(ByVal Value As Boolean)
            If Value = True Then
                _KeepEnrollmentFileWhenErr = 1
            Else
                _KeepEnrollmentFileWhenErr = 0
            End If
        End Set
    End Property

    Public Property PersonType() As String
        Get
            Return _PersonType
        End Get
        Set(ByVal Value As String)
            _PersonType = Left(Value, 50)
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return _Gender
        End Get
        Set(ByVal Value As String)
            _Gender = Left(Value, 50)
        End Set
    End Property

    Public Property Nationality() As String
        Get
            Return _Nationality
        End Get
        Set(ByVal Value As String)
            _Nationality = Left(Value, 50)
        End Set
    End Property

    Public Property PlaceOfBirth() As String
        Get
            Return _PlaceOfBirth
        End Get
        Set(ByVal Value As String)
            _PlaceOfBirth = Left(Value, 50)
        End Set
    End Property

    Public Property CUIT_CUIL() As String
        Get
            Return _CUIT_CUIL
        End Get
        Set(ByVal Value As String)
            _CUIT_CUIL = Left(Value, 50)
        End Set
    End Property

    Public Property MaritalStatus() As String
        Get
            Return _MaritalStatus
        End Get
        Set(ByVal Value As String)
            _MaritalStatus = Left(Value, 50)
        End Set
    End Property

    Public Property MarketingPromoSer() As String
        Get
            Return _MarketingPromoSer
        End Get
        Set(value As String)
            _MarketingPromoSer = value
        End Set
    End Property

    Public Property MarketingPromoNum() As String
        Get
            Return _MarketingPromoNum
        End Get
        Set(value As String)
            _MarketingPromoNum = value
        End Set
    End Property

    Public Property SalesChannel() As String
        Get
            Return _salesChannel
        End Get
        Set(ByVal Value As String)
            _salesChannel = Left(Value, 50)
        End Set
    End Property

    Public Property ServiceLineNumber() As String
        Get
            Return _serviceLineNumber
        End Get
        Set(ByVal Value As String)
            _serviceLineNumber = Left(Value, 50)
        End Set
    End Property
#End Region

#Region "Policy Service Enum values"
    Public ReadOnly Property PolicyServicePaymentInstrument() As PaymentInstrumentTypes
        Get
            If PaymentType = "1" Then
                Return PaymentInstrumentTypes.C
            ElseIf PaymentType = "6" Then
                Return PaymentInstrumentTypes.D
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PolicyServicePaymentType() As PaymentTypes
        Get
            If PaymentType = "1" Then
                Return PaymentTypes.DealerCollects
            ElseIf PaymentType = "6" Then
                Return PaymentTypes.AssurantCollects
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PolicyServiceSalution() As Salutations
        Get
            Select Case SalutationCode
                Case "1"
                    Return Salutations.Mr
                Case "2"
                    Return Salutations.Ms
                Case "3"
                    Return Salutations.MrPHD
                Case "4"
                    Return Salutations.MsPHD
                Case "5"
                    Return Salutations.MrProf
                Case "6"
                    Return Salutations.MsProf
                Case "7"
                    Return Salutations.Company
                Case "8"
                    Return Salutations.Mrs
                Case "11"
                    Return Salutations.Miss
                Case "13"
                    Return Salutations.Mr
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    Public ReadOnly Property PolicyServiceMaritalStatus() As PolicyService.MaritalStatus
        Get
            Select Case MaritalStatus
                Case "DIVORCED"
                    Return PolicyService.MaritalStatus.Divorced
                Case "02"
                    Return PolicyService.MaritalStatus.Divorced
                Case "MARRIED"
                    Return PolicyService.MaritalStatus.Married
                Case "01"
                    Return PolicyService.MaritalStatus.Married
                Case "SINGLE"
                    Return PolicyService.MaritalStatus.Single
                Case "03"
                    Return PolicyService.MaritalStatus.Single
                Case "WIDOWER"
                    Return PolicyService.MaritalStatus.Widower
                Case "04"
                    Return PolicyService.MaritalStatus.Widower
                Case "OTHERS"
                    Return PolicyService.MaritalStatus.Others
                Case "00"
                    Return PolicyService.MaritalStatus.Others
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    Public ReadOnly Property PolicyServiceGender() As PolicyService.Gender
        Get
            Select Case Gender
                Case "F"
                    Return PolicyService.Gender.Female
                Case "M"
                    Return PolicyService.Gender.Male
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    Public ReadOnly Property PolicyServiceSubscribeStatus() As SubscriberStatusType
        Get
            Select Case SubscriberStatus
                Case "A"
                    Return SubscriberStatusType.Active
                Case "C"
                    Return SubscriberStatusType.Cancelled
                Case "P"
                    Return SubscriberStatusType.PastDue
                Case "PA"
                    Return SubscriberStatusType.PastDueClaimsAllowed
                Case "S"
                    Return SubscriberStatusType.Suspended
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property
    Public ReadOnly Property UseCustomerProfile As String
        Get
            If _UseCustomerProfile Is Nothing Then
                _UseCustomerProfile = SetCustomerProfile()
            End If
            Return _UseCustomerProfile
        End Get
    End Property
#End Region
#Region "Private methods"
    Private Sub SetProductConversion()
        Dim objDealer As Dealer = New Dealer(DealerID)
        Dim strConvertProdCode As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_TRANSLATE_PRODUCT_CODE, Authentication.CurrentUser.LanguageId), objDealer.ConvertProductCodeId)
        If strConvertProdCode = "N" Then 'Get Assurant Product Code
            _ProductConversion = False
        Else 'external product code
            _ProductConversion = True
        End If
    End Sub

    Private Function GetDealerCountry() As Country
        Return New Country(Dealer.GetDealerCountryId(DealerID))
    End Function

    Private Function SetCustomerProfile() As String
        Dim attvalue As AttributeValue = CertDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = "MANUAL_CERT_USE_CUSTOMER_PROFILE").FirstOrDefault
        If Not attvalue Is Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
            return Codes.YESNO_Y
        Else
            return Codes.YESNO_N
        End If
    End Function
#End Region

#Region "Public methods"
    Public Shared Function GetDealerCertAddEnabled(ByVal companyIds As ArrayList) As DataView
        Return Dealer.GetDealerCertAddEnabled(companyIds)
    End Function

    Public Function GetExternalProductCode() As ProductCodeConversion.ExternalProdCodeWithDescDV
        Return ProductCodeConversion.GetExternalProdCodeListWithDesc(DealerID)
    End Function

    Public Shared Sub GetDealerDetailsForCertADD(ByVal DealerID As Guid, ByRef CurrencyCode As String, ByRef CountryCode As String,
                                                    ByRef MailAddressFormat As String, ByRef InstallmentAllowed As String,
                                                    ByRef UseInstallmentDef As String)

        Dim dv As Certificate.CertAddDealerDetailsDV = Certificate.GetDealerDetailsForCertADD(DealerID)

        CurrencyCode = ""
        CountryCode = ""
        MailAddressFormat = ""
        InstallmentAllowed = ""
        UseInstallmentDef = ""

        If dv.Count = 1 Then
            CurrencyCode = dv.Item(0).Item(dv.COL_CURRENCY_CODE)
            CountryCode = dv.Item(0).Item(dv.COL_COUNTRY_CODE)
            MailAddressFormat = dv.Item(0).Item(dv.COL_MAIL_ADDRESS_FORMAT)
            InstallmentAllowed = dv.Item(0).Item("InstallmentAllowed")
            UseInstallmentDef = dv.Item(0).Item("UseInstallmentDefn")
        Else
            Throw New DataNotFoundException(ERR_NO_DEALER_CONTRACT_FOUND)
        End If

        CurrencyCode = CurrencyCode.Trim
        CountryCode = CountryCode.Trim
        MailAddressFormat = MailAddressFormat.Trim
        InstallmentAllowed = InstallmentAllowed.Trim
        UseInstallmentDef = UseInstallmentDef.Trim

        If CurrencyCode = String.Empty Then
            Throw New DataNotFoundException(ERR_DEALER_CURRENCY_NOT_FOUND)
        End If
        If CountryCode = String.Empty Then
            Throw New DataNotFoundException(ERR_DEALER_COUNTRY_NOT_FOUND)
        End If
    End Sub
    Public Function OlitaSave(ByRef ErrMsg As Collections.Generic.List(Of String), ByRef CertID As Guid, Optional ByVal blnValidateFirst As Boolean = True) As Boolean
        Dim blnSuccess As Boolean = True, strErrMsg As String = ""
        Dim strErrMsgUIProgCode As String = "", strErrMsgParamList As String = "", intParamCnt As Integer = 0
        If blnValidateFirst Then
            blnSuccess = Me.Validate(ErrMsg)
        End If
        If blnSuccess Then
            blnSuccess = Certificate.InsertCertificate(Me.TransID, Me.DealerID, Me.CertNum, Me.ProductCode, Me.WarrantySalesDate,
                                    Me.ProductSalesDate, Me.WarrantyPrice, Me.ItemCode,
                                    Me.ItemDescription, Me.ProductRetailPrice, Me.CertDuration, Me.ManufacturerDuration,
                                    Me.SalesRepNumber, Me.BranchCode, Me.InvoiceNumber, Me.CustomerTaxID, Me.Salutation,
                                    Me.CustomerName, Me.CustomerAddress1, Me.CustomerAddress2, Me.CustomerCity,
                                    Me.CustomerZIP, Me.CustomerState, Me.CustomerHomePhone, Me.CustomerWorkPhone,
                                    Me.CustomerEmail, Me.Make, Me.Model, Me.SerialNumber,
                                    Me.CustomerCountryISOCode, Me.PurchaseCountryISOCode, Me.CurrencyISOCode, Me.PaymentType,
                                    Me.BillingFrequency, Me.NumOfInstallments, Me.InstallmentAmount,
                                    Me.BankAcctOwnerName, Me.BankAcctNumber, Me.BankRoutingNumber, Me.MembershipNum, Me._KeepEnrollmentFileWhenErr,
                                    strErrMsg, CertID, strErrMsgUIProgCode, strErrMsgParamList, intParamCnt, Me.BundledItems,
                                    Me.RecordType, Me.SkuNumber, Me.SubscriberStatus, Me.PostPrePaid, Me.MembershipNum, Me.MembershipType,
                                    Me.BillingPlan, Me.BillingCycle, Me.MaritalStatus, Me.PersonType,
                                    Me.Gender, Me.Nationality, Me.PlaceOfBirth, Me.CUIT_CUIL, Me.DateOfBirth,
                                    Me.MarketingPromoSer, Me.MarketingPromoNum, Me.SalesChannel)
            If Not blnSuccess Then
                'Translate the error message
                If strErrMsgUIProgCode <> "" Then
                    Dim strTranslatedMsg As String = TranslationBase.TranslateLabelOrMessage(strErrMsgUIProgCode).Trim
                    If strTranslatedMsg <> String.Empty Then
                        If intParamCnt > 0 Then
                            strTranslatedMsg = TranslationBase.TranslateParameterizedMsg(strTranslatedMsg, intParamCnt, strErrMsgParamList).Trim
                        End If
                        If strTranslatedMsg <> "" Then strErrMsg = strTranslatedMsg
                    End If
                End If
                ErrMsg.Add(strErrMsg)
            End If
        End If
        Return blnSuccess
    End Function

    Public Function Save(ByRef ErrMsg As Collections.Generic.List(Of String), ByRef CertID As Guid, Optional ByVal blnValidateFirst As Boolean = True) As Boolean
        Dim blnSuccess As Boolean = True, strErrMsg As String = ""

        If blnValidateFirst Then
            blnSuccess = Me.Validate(ErrMsg)
        End If

        If blnSuccess Then
            Dim wsRequest As New EnrollRequest
            Dim wsResponse As EnrollResponse

            With wsRequest
                .Dealer = New DealerInfo() With {
                    .DealerCode = CertDealer.Dealer,
                    .BranchCode = BranchCode
                }

                .CertificateNumber = CertNum
                .PricePol = WarrantyPrice
                .PersonType = PersonType
                .SalesRepNumber = SalesRepNumber
                .Occupation = Occupation
                .SalesChannel = SalesChannel
                .CuitCuil = CUIT_CUIL

                .Products = New ProductInfo With {
                    .ProductCode = ProductCode,
                    .WarrantySalesDate = WarrantySalesDate,
                    .ProductSalesDate = ProductSalesDate,
                    .WarrantyDurationMonths = CertDuration,
                    .SalesPrice = ProductRetailPrice
                }

                Dim certitem As New ItemInfo With {
                        .ManufacturerWarrantyMonths = ManufacturerDuration,
                        .Description = ItemDescription,
                        .Manufacturer = Make,
                        .Model = Model,
                        .SerialNumber = SerialNumber,
                        .SkuNumber = SkuNumber,
                        .Code = ItemCode
                }
                Dim certItems As New List(Of ItemInfo)
                certItems.Add(New ItemInfo With {
                                 .ManufacturerWarrantyMonths = ManufacturerDuration,
                                 .Description = ItemDescription,
                                 .Manufacturer = Make,
                                 .Model = Model,
                                 .SerialNumber = SerialNumber,
                                 .SkuNumber = SkuNumber,
                                 .Code = ItemCode
                                 })
                ' Add bundle item
                If Not BundledItems Is Nothing Then
                    For Each o As BundledItem In BundledItems
                        certItems.Add(New ItemInfo With {
                                         .ManufacturerWarrantyMonths = o.MfgWarranty,
                                         .Description = o.Description,
                                         .Manufacturer = o.Make,
                                         .Model = o.Model,
                                         .SerialNumber = o.SerialNumber
                                         })
                    Next
                End If
                .Items = certItems.ToArray()

                .PaymentInfo = New PaymentInfo() With {
                    .InvoiceNumber = InvoiceNumber,
                    .PaymentType = PolicyServicePaymentType,
                    .PaymentInstrumentType = PolicyServicePaymentInstrument,
                    .BillingFrequency = BillingFrequency,
                    .NoOfInstallments = NumOfInstallments,
                    .InstallmentAmount = InstallmentAmount,
                    .PostPrePaid = PostPrePaid,
                    .BillingPlan = BillingPlan,
                    .BillingCycle = BillingCycle
                }

                Dim certCustomer As New CustomerInfo With {
                        .CustomerType = CustomerTypes.Primary,
                        .IdentificationNumber = CustomerTaxID,
                        .FirstName = FirstName,
                        .LastName = LastName,
                        .MiddleName = MiddleName,
                        .Salutation = PolicyServiceSalution,
                        .CustomerName = CustomerName,
                            .Address = New AddressInfo() With {
                            .Address1 = CustomerAddress1,
                            .Address2 = CustomerAddress2,
                            .City = CustomerCity,
                            .PostalCode = CustomerZIP,
                            .State = CustomerState,
                            .Country = CustomerCountryISOCode,
                            .CurrencyCode = CurrencyISOCode
                        },
                        .Nationality = Nationality,
                        .PlaceOfBirth = PlaceOfBirth,
                        .DateOfBirth = DateOfBirth
                }

                If Not String.IsNullOrEmpty(MaritalStatus) Then
                    certCustomer.MaritalStatus = PolicyServiceMaritalStatus
                End If
                If Not String.IsNullOrEmpty(Gender) Then
                    certCustomer.Gender = PolicyServiceGender
                End If

                Dim phonelist As New List(Of PhoneInfo)
                If String.IsNullOrEmpty(CustomerHomePhone) = False Then
                    phonelist.Add(New PhoneInfo() With {.PhoneNumber = CustomerHomePhone, .Type = ContactType.HomePhone})
                End If
                If String.IsNullOrEmpty(CustomerWorkPhone) = False Then
                    phonelist.Add(New PhoneInfo() With {.PhoneNumber = CustomerWorkPhone, .Type = ContactType.WorkPhone})
                End If
                If phonelist.Count > 0 Then
                    certCustomer.Phone = phonelist.ToArray()
                End If

                If String.IsNullOrEmpty(CustomerEmail) = False Then
                    certCustomer.Email = New EMailInfo() With {.EmailAddress = CustomerEmail, .Type = ContactType.EMailAddress}
                End If
                .Customers = New CustomerInfo() {certCustomer}

                If String.IsNullOrEmpty(BankAcctOwnerName) = False OrElse String.IsNullOrEmpty(BankAcctNumber) = False OrElse String.IsNullOrEmpty(BankRoutingNumber) = False Then
                    .BankInfo = New PolicyService.BankInfo() With {
                        .AccountOwnerName = BankAcctOwnerName,
                        .AccountNumber = BankAcctNumber,
                        .RoutingNumber = BankRoutingNumber
                        }
                End If

                If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, CertDealer.DealerTypeId) = "3" Then 'Mobile dealer
                    .DealerReferenceData = New MobileDealerReferenceDataInfo() With {
                        .DealerSourceSystem = "ElitaWeb",
                        .MarketingPromotionNumber = MarketingPromoNum,
                        .MarketingPromotionSerial = MarketingPromoSer,
                        .MembershipNumber = MembershipNum,
                        .ServiceLineNumber = ServiceLineNumber,
                        .MembershipType = MembershipType,
                        .SubscriberStatus = PolicyServiceSubscribeStatus
                    }
                Else
                    .DealerReferenceData = New DealerReferenceDataInfo() With {
                        .DealerSourceSystem = "ElitaWeb",
                        .MarketingPromotionNumber = MarketingPromoNum,
                        .MarketingPromotionSerial = MarketingPromoSer
                        }
                End If
            End With
            Try
                blnSuccess = False

                wsResponse = WcfClientHelper.Execute(Of PolicyServiceClient, IPolicyService, EnrollResponse)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As PolicyServiceClient)
                        Return c.Enroll(wsRequest)
                    End Function)

                If String.IsNullOrEmpty(wsResponse.CertificateNumber) = False Then
                    ErrMsg.Add(TranslationBase.TranslateLabelOrMessage("CERTIFICATE_NUMBER") + wsResponse.CertificateNumber)
                    Dim objCert As Certificate = Certificate.GetCetificateByCertNumber(CertDealer.Id, wsResponse.CertificateNumber)
                    CertID = objCert.Id
                    blnSuccess = True
                End If

                If String.IsNullOrEmpty(wsResponse.ErrorMessage) = False Then
                    ErrMsg.Add(wsResponse.ErrorMessage)
                End If
            Catch e As FaultException(Of PolicyService.EnrollFault)
                strErrMsg = TranslateFaultErrorMessage(e.Detail.FaultCode, e.Detail.FaultMessage)
                ErrMsg.Add(strErrMsg)
                blnSuccess = False
            Catch ex As Exception
                strErrMsg = "Policy Enroll Exception: " + ex.Message
                ErrMsg.Add(strErrMsg)
                blnSuccess = False
            End Try

        End If
        Return blnSuccess
    End Function
    Private Function TranslateFaultErrorMessage(ByVal errCode As String, ByVal errDescription As String) As String
        Dim strErrorMessage As String = String.Empty
        strErrorMessage = TranslationBase.TranslateLabelOrMessage(errCode)
        If StrComp(strErrorMessage,errCode,CompareMethod.Text) = 0 Then
            strErrorMessage = errDescription
        End If
        strErrorMessage = TranslationBase.TranslateLabelOrMessage("POLICY_ENROLL_ERROR") + " " + strErrorMessage
        Return strErrorMessage
    End Function

    Private Shared Function GetClient() As PolicyServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SVC_CERT_ENROLL), False)
        Dim client = New PolicyServiceClient("CustomBinding_IPolicyService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function

    Public Function Validate(ByRef ErrMsg As Collections.Generic.List(Of String)) As Boolean
        Dim isValid As Boolean = True, strErrMsg As String = "", strAddressRequiredFields As String = ""
        'active contract required
        Dim objContract As Contract = Contract.GetContract(Me.DealerID, Me.WarrantySalesDate)
        If objContract Is Nothing Then
            isValid = False
            strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_CONTRACT_NOT_FOUND)
            ErrMsg.Add(strErrMsg)
        End If

        Dim isAutoGenFlagOn As Boolean = Dealer.GetCertAutoGenFlag(Me.DealerID)
        If Not isAutoGenFlagOn Then
            If Me.CertNum.Trim = String.Empty Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CERTIFICATE_NUMBER_IS_REQUIRED_ERRR)
                ErrMsg.Add(strErrMsg)
            End If
        End If

        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        If objContract IsNot Nothing AndAlso objContract.MarketingPromotionId.Equals(yesId) Then
            If String.IsNullOrEmpty(Me.MarketingPromoNum) Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_MARKETING_PROMO_NUM_IS_REQUIRED)
                ErrMsg.Add(strErrMsg)
            End If
            If String.IsNullOrEmpty(Me.MarketingPromoSer) Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_MARKETING_PROMO_SER_IS_REQUIRED)
                ErrMsg.Add(strErrMsg)
            End If
        End If

        If (Not String.IsNullOrEmpty(Me.MarketingPromoNum)) AndAlso Me.MarketingPromoNum.Length > 6 Then
            isValid = False
            strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_INVALID_MARKETING_PROMO_NUM)
            ErrMsg.Add(strErrMsg)
        End If

        If (Not String.IsNullOrEmpty(Me.MarketingPromoSer)) AndAlso Me.MarketingPromoSer.Length > 3 Then
            isValid = False
            strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_INVALID_MARKETING_PROMO_SER)
            ErrMsg.Add(strErrMsg)
        End If

        If Me.ProductCode.Trim = String.Empty Then
            isValid = False
            strErrMsg = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_PRODUCT_CODE_IS_REQUIRED)
            ErrMsg.Add(strErrMsg)
        End If

        If (Not Me.BundledItems Is Nothing) AndAlso (Me.BundledItems.Count) > 10 Then
            isValid = False
            strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_TOO_MANY_BUNDLED_ITEMS)
            ErrMsg.Add(strErrMsg)
        End If

        Dim AddressRequiredFields As String = String.Empty
        Dim objCtry As Country = GetDealerCountry()

        If (Not objCtry Is Nothing AndAlso Not objCtry.AddressInfoReqFields Is Nothing) Then
            AddressRequiredFields = objCtry.AddressInfoReqFields.ToUpper()
        End If

        If AddressRequiredFields.Length > 0 Then
            ' Validating Salutation
            If AddressRequiredFields.Contains("[SALU]") AndAlso Me.Salutation.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_SALU_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Customer Name
            If AddressRequiredFields.Contains("[NAME]") AndAlso Me.CustomerName.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_CUST_NAME_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Address1
            If AddressRequiredFields.Contains("[ADR1]") AndAlso Me.CustomerAddress1.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_ADR1_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Address2
            If AddressRequiredFields.Contains("[ADR2]") AndAlso Me.CustomerAddress2.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_ADR2_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Address3
            If AddressRequiredFields.Contains("[ADR3]") Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_ADR3_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating City
            If AddressRequiredFields.Contains("[CITY]") AndAlso Me.CustomerCity.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_CITY_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Zip
            If AddressRequiredFields.Contains("[ZIP]") AndAlso Me.CustomerZIP.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_ZIP_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating State / Province
            If AddressRequiredFields.Contains("[PRO]") AndAlso Me.CustomerState.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_STATE_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Country
            If AddressRequiredFields.Contains("[COU]") AndAlso Me.CustomerCountryISOCode.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_COUNTRY_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Email
            If AddressRequiredFields.Contains("[EMAIL]") AndAlso Me.CustomerEmail.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_EMAIL_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Work Phone
            If AddressRequiredFields.Contains("[WPHONE]") AndAlso Me.CustomerWorkPhone.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_WORK_PHONE_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Home Phone
            If AddressRequiredFields.Contains("[HPHONE]") AndAlso Me.CustomerHomePhone.Trim().Length <= 0 Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_HOME_PHONE_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
            ' Validating Region
            If AddressRequiredFields.Contains("[RGN]") Then
                isValid = False
                strErrMsg = TranslationBase.TranslateLabelOrMessage(ERR_REGION_MISSING)
                ErrMsg.Add(strErrMsg)
            End If
        End If

        Return isValid
    End Function

    Public Function AddBundledItem(ByVal objBundledItem As BundledItem, ByRef Errmsg As String) As Boolean
        Errmsg = String.Empty
        Dim blnSuccess As Boolean = False
        If _BundledItems Is Nothing Then
            _BundledItems = New Generic.List(Of BundledItem)
        End If

        If _BundledItems.Count = 10 Then
            Errmsg = TranslationBase.TranslateLabelOrMessage(Me.ERR_TOO_MANY_BUNDLED_ITEMS)
        Else
            _BundledItems.Add(objBundledItem)
            blnSuccess = True
        End If
        Return blnSuccess
    End Function

    Public Function DeleteBundledItem(ByVal index As Integer) As BundledItem
        Dim objItem As BundledItem = GetBundledItem(index)
        If Not objItem Is Nothing Then
            BundledItems.RemoveAt(index - 1)
        End If
    End Function

    '1-based index
    Public Function GetBundledItem(ByVal index As Integer) As BundledItem
        If (_BundledItems Is Nothing) OrElse (index > _BundledItems.Count) Then
            Return Nothing
        Else
            Return _BundledItems.Item(index - 1)
        End If
    End Function

    Public Function GetBundledItemCount() As Integer
        If _BundledItems Is Nothing Then
            Return 0
        Else
            Return _BundledItems.Count
        End If
    End Function

    Public Sub InitBundle()
        If _BundledItems Is Nothing Then
            _BundledItems = New Generic.List(Of BundledItem)
        End If
    End Sub
#End Region

#Region "Bundled Item class"
    Class BundledItem
        Private _Make As String
        Private _Model As String
        Private _SerialNumber As String
        Private _Description As String
        Private _Price As Double
        Private _MfgWarranty As Integer
        Private _ProductCode As String

        Public Sub New()
            _Make = String.Empty
            _Model = String.Empty
            _SerialNumber = String.Empty
            _Description = String.Empty
            _Price = 0
            _MfgWarranty = 0
            _ProductCode = String.Empty
        End Sub

        Public Sub New(ByVal strMake As String, ByVal strModel As String, ByVal strSerialNum As String, ByVal strDesc As String, ByVal dblPrice As Double, ByVal intMfgWarranty As Integer, ByVal strProdCode As String)
            _Make = strMake
            _Model = strModel
            _SerialNumber = strSerialNum
            _Description = strDesc
            _Price = dblPrice
            _MfgWarranty = intMfgWarranty
            _ProductCode = strProdCode
        End Sub

        Public Property Make() As String
            Get
                Return _Make
            End Get
            Set(ByVal Value As String)
                _Make = Left(Value, 50)
            End Set
        End Property

        Public Property Model() As String
            Get
                Return _Model
            End Get
            Set(ByVal Value As String)
                _Model = Left(Value, 30)
            End Set
        End Property

        Public Property SerialNumber() As String
            Get
                Return _SerialNumber
            End Get
            Set(ByVal Value As String)
                _SerialNumber = Left(Value, 30)
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal Value As String)
                _Description = Left(Value, 50)
            End Set
        End Property

        Public Property Price() As Double
            Get
                Return _Price
            End Get
            Set(ByVal Value As Double)
                _Price = Value
            End Set
        End Property

        Public Property MfgWarranty() As Integer
            Get
                Return _MfgWarranty
            End Get
            Set(ByVal Value As Integer)
                _MfgWarranty = Value
            End Set
        End Property

        Public Property ProductCode() As String
            Get
                Return _ProductCode
            End Get
            Set(ByVal Value As String)
                _ProductCode = Left(Trim(Value), 5)
            End Set
        End Property
    End Class
#End Region
End Class
