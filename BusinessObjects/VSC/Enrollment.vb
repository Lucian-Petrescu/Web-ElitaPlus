Imports System.ServiceModel
Imports System.Threading
Imports Assurant.Elita
Imports Assurant.Elita.PciSecure.Attributes

Public Class Enrollment
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DATA_COL_NAME_CERTIFICATE As String = "CERTIFICATE_NUMBER"
    Public Const DATA_COL_NAME_ADDRESS1 As String = "ADDRESS1"
    Public Const DATA_COL_NAME_CITY As String = "CITY"
    Public Const DATA_COL_NAME_REGION As String = "REGION"
    Public Const DATA_COL_NAME_POSTAL_CODE As String = "POSTAL_CODE"
    Public Const DATA_COL_NAME_COUNTRY_CODE As String = "COUNTRY_CODE"
    Public Const DATA_COL_NAME_HOME_PHONE As String = "HOME_PHONE"
    Public Const DATA_COL_NAME_WORK_PHONE As String = "WORK_PHONE"
    Public Const DATA_COL_NAME_MODEL_YEAR As String = "MODEL_YEAR"
    Public Const DATA_COL_NAME_MODEL As String = "MODEL"
    Public Const DATA_COL_NAME_MANUFACTURER As String = "MANUFACTURER"
    Public Const DATA_COL_NAME_VERSION As String = "ENGINE_VERSION"
    Public Const DATA_COL_NAME_TAG As String = "VEHICLE_LICENSE_TAG"
    Public Const DATA_COL_NAME_ODOMETER As String = "ODOMETER"
    Public Const DATA_COL_NAME_VIN As String = "VIN"
    Public Const DATA_COL_NAME_PURCHASE_PRICE As String = "PURCHASE_PRICE"
    Public Const DATA_COL_NAME_PURCHASE_DATE As String = "PURCHASE_DATE"
    Public Const DATA_COL_NAME_IN_SERVICE_DATE As String = "IN_SERVICE_DATE"
    Public Const DATA_COL_NAME_DELIVERY_DATE As String = "DELIVERY_DATE"
    Public Const DATA_COL_NAME_PLAN_CODE As String = "PLAN_CODE"
    Public Const DATA_COL_NAME_QUOTE_ITEM_NUMBER As String = "QUOTE_ITEM_NUMBER"
    Public Const DATA_COL_NAME_DEDUCTIBLE As String = "DEDUCTIBLE"
    Public Const DATA_COL_NAME_TERM_MONTHS As String = "TERM_MONTHS"
    Public Const DATA_COL_NAME_TERM_KI_MI As String = "TERM_KI_MI"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_AGENT_NAME As String = "AGENT_NUMBER"
    Public Const DATA_COL_NAME_WARRANTY_SALE_DATE As String = "WARRANTY_SALE_DATE"
    Public Const DATA_COL_NAME_PLAN_AMOUNT As String = "PLAN_AMOUNT"
    Public Const DATA_COL_NAME_DOC_TYPE As String = "DOCUMENT_TYPE"
    Public Const DATA_COL_NAME_IDENTITY_DOC_NO As String = "IDENTITY_DOCUMENT_NO"
    Public Const DATA_COL_NAME_RG_NO As String = "RG_NO"
    Public Const DATA_COL_NAME_ID_TYPE As String = "ID_TYPE"
    Public Const DATA_COL_NAME_DOC_ISSUE_DATE As String = "DOCUMENT_ISSUE_DATE"
    Public Const DATA_COL_NAME_DATE_OF_BIRTH As String = "BIRTH_DATE"
    Public Const DATA_COL_NAME_DOC_AGENCY As String = "DOCUMENT_AGENCY"
    Public Const DATA_COL_NAME_QUOTE As String = "QUOTE_NUMBER"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "APPLICANT_NAME"
    Public Const DATA_COL_NAME_IS_CREDITCARD_AUTHREQ As String = "IS_CREDITCARD_AUTHREQ"

    Public Const DATA_COL_NAME_COLLECTION_METHOD_CODE As String = "Collection_Method_Code"
    Public Const DATA_COL_NAME_PAYMENT_INSTRUMENT_CODE As String = "Payment_Instrument_Code"
    Public Const DATA_COL_NAME_INSTALLMENTS_NUMBER As String = "Installments_Number"
    Public Const DATA_COL_NAME_CREDIT_CARD_TYPE_CODE As String = "Credit_Card_Type_Code"
    Public Const DATA_COL_NAME_NAME_ON_CREDIT_CARD As String = "Name_On_Credit_Card"
    Public Const DATA_COL_NAME_CREDIT_CARD_NUMBER As String = "Credit_Card_Number"
    Public Const DATA_COL_NAME_EXPIRATION_DATE As String = "Expiration_Date"
    Public Const DATA_COL_NAME_SECURITY_CODE As String = "Security_Code"
    Public Const DATA_COL_NAME_BANK_ID As String = "Bank_ID"
    Public Const DATA_COL_NAME_ACCOUNT_NUMBER As String = "Account_Number"
    Public Const DATA_COL_NAME_NAME_ON_ACCOUNT As String = "Name_On_Account"
    Public Const DATA_COL_NAME_EXTERNAL_CAR_CODE As String = "External_Car_Code"
    Public Const DATA_COL_NAME_CARD_SECURITY_CODE As String = "Card_Security_Code"
    Public Const DATA_COL_NAME_EMAIL As String = "Email"
    Public Const DATA_COL_NAME_SALES_TAX As String = "Sales_Tax"
    Public Const DATA_COL_NAME_VALIDATE_ONLY As String = "Validate_Only"
    Public Const DATA_COL_NAME_E_BUSINESS_USER_CONSENT As String = "E_Business_User_Consent"

    Private Const SOURCE_COL_ADDRESS As String = "Address"
    Private Const SOURCE_COL_YEAR As String = "Vehicle_Year"
    Private Const SOURCE_COL_MAKE As String = "Vehicle_Make"
    Private Const SOURCE_COL_MODEL As String = "Vehicle_Model"
    Private Const SOURCE_COL_MILEAGE As String = "Vehicle_Mileage"
    Private Const SOURCE_COL_PURCHASE_PRICE As String = "Vehicle_Purchase_Price"
    Private Const SOURCE_COL_PURCHASE_DATE As String = "Vehicle_Purchase_Date"
    Private Const SOURCE_COL_SERVICE_DATE As String = "Vehicle_In_Service_Date"
    Private Const SOURCE_COL_DELIVERY_DATE As String = "Vehicle_Delivery_Date"
    Private Const SOURCE_COL_TERM_MILES As String = "Term_Miles"
    Private Const SOURCE_COL_DOC_AGENCY As String = "Issuing_agency"
    Private Const SOURCE_COL_CUSTOMER_NAME As String = "Customer_Name"

    Private Const TABLE_NAME As String = "VSCEnrollment"
    Private Const TABLE_NAME_CUSTOMER As String = "Customer"
    Private Const TABLE_NAME_OPTIONS As String = "Optional_Coverage"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const OUTPUT_TABLE_NAME As String = "VSC_ENROLLMENT"
    Private Const VALUE_OK As String = "OK"

    Private Const DOC_TYPE_CPF As String = "CPF"

    Private Const END_OF_LINE As String = "^"
    Private Const END_OF_FIELD As String = "|"
    Private Const IS_CREDITCARD_AUTHREQ_Y As String = "Y"
    Private Const IS_CREDITCARD_AUTHREQ_N As String = "N"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As VSCEnrollmentDs)
        MyBase.New()

        MapDataSet(ds)
        ValidateInput(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ByVal ds As VSCEnrollmentDs)

        ' Mapping column names
        Dim schema As String = ds.GetXmlSchema
        schema = schema.Replace(SOURCE_COL_ADDRESS, DATA_COL_NAME_ADDRESS1)
        schema = schema.Replace(SOURCE_COL_YEAR, DATA_COL_NAME_MODEL_YEAR)
        schema = schema.Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER)
        schema = schema.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER)
        schema = schema.Replace(SOURCE_COL_MODEL, DATA_COL_NAME_MODEL)
        schema = schema.Replace(SOURCE_COL_PURCHASE_PRICE, DATA_COL_NAME_PURCHASE_PRICE)
        schema = schema.Replace(SOURCE_COL_PURCHASE_DATE, DATA_COL_NAME_PURCHASE_DATE)
        schema = schema.Replace(SOURCE_COL_SERVICE_DATE, DATA_COL_NAME_IN_SERVICE_DATE)
        schema = schema.Replace(SOURCE_COL_DELIVERY_DATE, DATA_COL_NAME_DELIVERY_DATE)
        schema = schema.Replace(SOURCE_COL_TERM_MILES, DATA_COL_NAME_TERM_KI_MI)
        schema = schema.Replace(SOURCE_COL_DOC_AGENCY, DATA_COL_NAME_DOC_AGENCY)
        schema = schema.Replace(SOURCE_COL_CUSTOMER_NAME, DATA_COL_NAME_CUSTOMER_NAME)

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

    Private Sub ValidateInput(ByVal ds As VSCEnrollmentDs)

        ' No customer
        If ds.Customer.Count = 0 Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)

        With ds.VSCEnrollment.Item(0)
            Dim objDealer As New Dealer(ElitaPlusIdentity.Current.ActiveUser.CompanyId, .Dealer_Code.ToUpper)
            If objDealer Is Nothing Then
                Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.INVALID_DEALER_CODE)
            End If

            Dim oContract As Contract = Contract.GetContract(objDealer.Id, .Warranty_Sale_Date)
            If oContract Is Nothing Then
                Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.DEALER_DOES_NOT_HAVE_CURRENT_CONTRACT)
            End If

            'Ticket # 1,412,358 
            Dim objCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            If LookupListNew.GetCodeFromId("COMPANY_TYPE", objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE AndAlso
                LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATION_TYPES, oContract.ID_Validation_Id) <> Codes.ID_VALIDATION_FULL Then
                Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
            End If

            If LookupListNew.GetCodeFromId("COMPANY_TYPE", objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE AndAlso
                LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATION_TYPES, oContract.ID_Validation_Id) = Codes.ID_VALIDATION_FULL Then
                ' Verify RG_No, ID_Type, Issuing Agency, Document_Issue_Date if document type is CPF
                If (.IsDocument_TypeNull) OrElse (.Document_Type = DOC_TYPE_CPF AndAlso
                  ((.IsRG_NoNull OrElse .RG_No.Trim.Length = 0) _
                    OrElse (.IsIssuing_agencyNull OrElse .Issuing_agency.Trim.Length = 0) _
                    OrElse (.IsID_TypeNull OrElse .ID_Type.Trim.Length = 0) _
                    OrElse (.IsDocument_Issue_DateNull))) Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)

            End If

            'Validate Bank Info 
            If (.Collection_Method_Code.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS) And .Payment_Instrument_Code.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT)) Then
                If ds.Bank_Account_Info.Count > 0 Then
                    With ds.Bank_Account_Info(0)
                        If (.Name_On_Account Is Nothing OrElse .Name_On_Account.Trim.Length = 0) _
                           OrElse (.Account_Number Is Nothing OrElse .Account_Number.Trim.Length = 0) _
                           OrElse (.Bank_ID Is Nothing OrElse .Bank_ID.Trim.Length = 0) Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
                    End With
                Else
                    Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
                End If

            ElseIf (.Collection_Method_Code.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS) And .Payment_Instrument_Code.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__CREDIT_CARD)) Or
                   (.Collection_Method_Code.ToUpper.Equals(Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS) And .Payment_Instrument_Code.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD)) Then
                'Validate credit card - XML level
                If ds.Credit_Card_Info.Count > 0 Then
                    With ds.Credit_Card_Info(0)
                        If (.Credit_Card_Type_Code Is Nothing OrElse .Credit_Card_Type_Code.Trim.Length = 0) _
                               OrElse (.Credit_Card_Number Is Nothing OrElse .Credit_Card_Number.Trim.Length = 0) _
                               OrElse (.Name_On_Credit_Card Is Nothing OrElse .Name_On_Credit_Card.Trim.Length = 0) _
                               OrElse (.Expiration_Date Is Nothing OrElse .Expiration_Date.Trim.Length = 0) Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
                    End With
                Else
                    Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
                End If

                'Validate credit card Type
                If ds.Credit_Card_Info.Count > 0 Then
                    Dim CompanyPaymentTypesView As DataView = CompanyCreditCard.LoadList()
                    Dim index As Integer
                    Dim blnValidPaymentTypeCOde As Boolean = False
                    For index = 0 To CompanyPaymentTypesView.Table.Rows.Count - 1
                        If ds.Credit_Card_Info(0).Credit_Card_Type_Code.ToUpper.Equals(CompanyPaymentTypesView.Table.Rows(index).Item("CODE")) Then
                            blnValidPaymentTypeCOde = True
                            Exit For
                        End If
                    Next
                    If Not blnValidPaymentTypeCOde Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_INVALID_CREDIT_CARD_CODE)
                End If
            End If

            'AA REQ-910 adding new fields BEGIN
            Dim i As Integer
            For i = 0 To ds.Customer.Count - 1
                If ds.Customer.Count > 0 Then
                    With ds.Customer(i)
                        If Not .IsIncome_Range_CodeNull Then
                            Dim IncomeRangeId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_INCOME_RANGE, .Income_Range_Code)
                            If IncomeRangeId.Equals(Guid.Empty) Then Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_INVALID_INCOME_RANGE_CODE)
                        End If
                    End With
                End If
            Next

            'AA REQ-910 adding new fields END
        End With

    End Sub

    Private Sub Load(ByVal ds As VSCEnrollmentDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Exception loading the data", Common.ErrorCodes.UNEXPECTED_ERROR, ex)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As VSCEnrollmentDs)

        Try

            If ds.VSCEnrollment.Count = 0 Then Exit Sub
            With ds.VSCEnrollment(0)

                If Not .IsCertificate_NumberNull Then CertificateNumber = .Certificate_Number 'REQ-5463
                Address = .Address
                City = .City
                Region = ._Region
                PostalCode = .Postal_Code
                CountryCode = .Country_Code
                HomePhone = .Home_Phone

                If Not .IsVehicle_MakeNull Then Manufacturer = .Vehicle_Make
                If Not .IsVehicle_ModelNull Then Model = .Vehicle_Model
                ModelYear = .Vehicle_Year
                If Not .IsEngine_VersionNull Then EngineVersion = .Engine_Version

                'Manufacturer = .Vehicle_Make
                'Model = .Vehicle_Model
                'ModelYear = .Vehicle_Year
                'EngineVersion = .Engine_Version
                If Not .IsVINNull Then VIN = .VIN
                Mileage = .Vehicle_Mileage
                If Not .IsVehicle_License_TagNull Then Tag = .Vehicle_License_Tag
                PurchasePrice = .Vehicle_Purchase_Price
                PurchaseDate = .Vehicle_Purchase_Date
                InServiceDate = .Vehicle_In_Service_Date
                DeliveryDate = .Vehicle_Delivery_Date
                PlanCode = .Plan_Code
                QuoteItemNumber = .Quote_Item_Number
                Deductible = .Deductible
                TermMonths = .Term_Months
                TermMiles = .Term_Miles
                DealerCode = .Dealer_Code
                If Not .IsAgent_NumberNull Then AgentNumber = .Agent_Number
                WarrantyDate = .Warranty_Sale_Date
                PlanAmount = .Plan_Amount
                If Not .IsDocument_TypeNull Then DocType = .Document_Type
                IdentityDocNo = .Identity_document_No
                If Not .IsRG_NoNull Then RgNo = .RG_No
                If Not .IsID_TypeNull Then IdType = .ID_Type
                If Not .IsDocument_Issue_DateNull Then
                    DocIssueDate = .Document_Issue_Date
                Else
                    DocIssueDate = Date.MinValue
                End If
                If Not .IsIssuing_agencyNull Then DocAgency = .Issuing_agency
                Quote = .Quote_Number
                If Not .IsBirth_DateNull Then DateOfBirth = .Birth_Date
                If Not .IsWork_PhoneNull Then WorkPhone = .Work_Phone

                PaymentInstrumentCode = .Payment_Instrument_Code
                CollectionMethodCode = .Collection_Method_Code

                InstallmentsNumber = .Installments_Number
                If ds.Credit_Card_Info.Count > 0 Then
                    With ds.Credit_Card_Info(0)
                        CreditCardTypeCode = .Credit_Card_Type_Code
                        NameOnCreditCard = .Name_On_Credit_Card
                        CreditCardNumber = .Credit_Card_Number
                        ExpirationDate = .Expiration_Date
                        If Not .IsCard_Security_CodeNull Then CardSecurityCode = .Card_Security_Code
                    End With
                End If

                If ds.Bank_Account_Info.Count > 0 Then
                    With ds.Bank_Account_Info(0)
                        If Not .IsBank_IDNull Then BankID = .Bank_ID
                        AccountNumber = .Account_Number
                        NameOnAccount = .Name_On_Account
                    End With
                End If

                ' Populate customers
                Dim i As Integer
                'AA REQ-910 adding new fields BEGIN
                Dim CustomerOccupation As String = Nothing
                Dim IncomeRangeCode As String = Nothing
                Dim PEP As String = Nothing

                For i = 0 To ds.Customer.Count - 1
                    With ds.Customer(i)
                        If Not .IsCustomer_OccupationNull Then CustomerOccupation = .Customer_Occupation
                        If Not .IsIncome_Range_CodeNull Then IncomeRangeCode = .Income_Range_Code
                        If Not .IsPEPNull Then PEP = .PEP
                    End With

                    NewApplicant(ds.Customer(i).Customer_Name, CustomerOccupation, PEP, IncomeRangeCode)

                    CustomerOccupation = Nothing
                    IncomeRangeCode = Nothing
                    PEP = Nothing
                Next

                ' Populate Options
                For i = 0 To ds.Optional_Coverage.Count - 1
                    NewCoverage(ds.Optional_Coverage(i).Optional_Coverage_Code,
                                ds.Optional_Coverage(i).Optional_Coverage_Price,
                                ds.Optional_Coverage(i).OptionalCoverageQuote_Item_Number)

                Next

                If Not .IsPayment_Authoriztion_NumNull Then PaymentAuthoriztionNum = .Payment_Authoriztion_Num
                If Not .IsExternal_Car_CodeNull Then ExternalCarCode = .External_Car_Code
                If Not .IsIs_CreditCard_AuthReqNull Then Is_CreditCard_AuthReq = .Is_CreditCard_AuthReq.ToUpper

                'REQ-5463 Begin
                If Not .IsSales_TaxNull Then
                    SalesTax = .Sales_Tax
                    _salesTaxPresent = True
                Else
                    _salesTaxPresent = False
                End If

                If Not .IsValidate_OnlyNull Then
                    ValidateOnly = .Validate_Only
                Else
                    ValidateOnly = "N"
                End If

                'REQ-5463 end
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("VSC Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try

    End Sub

    Private Sub NewApplicant(ByVal name As String, ByVal CustomerOccupation As String, ByVal PEP As String, ByVal IncomeRangeCode As String)

        Dim newRow As DataRow = Dataset.Tables(TABLE_NAME_CUSTOMER).NewRow
        newRow(0) = name
        newRow(1) = CustomerOccupation
        newRow(2) = PEP
        newRow(3) = IncomeRangeCode
        Dataset.Tables(TABLE_NAME_CUSTOMER).Rows.Add(newRow)

    End Sub

    Private Sub NewCoverage(ByVal code As String, ByVal price As Double, ByVal quote_item_number As Integer)

        Dim newRow As DataRow = Dataset.Tables(TABLE_NAME_OPTIONS).NewRow
        newRow(0) = code
        newRow(1) = price
        newRow(2) = quote_item_number
        Dataset.Tables(TABLE_NAME_OPTIONS).Rows.Add(newRow)

    End Sub

    Private Sub Certificate()
        Dim dal As New VSCEnrollmentDAL
        dal.Certificate(enrollmentId)
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private enrollmentId As Guid
    Private _salesTaxPresent As Boolean

#End Region

#Region "Properties"

    <ValidStringLength("", Max:=20)>
    Public Property CertificateNumber As String
        Get
            If Row(DATA_COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CERTIFICATE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERTIFICATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property Address As String
        Get
            If Row(DATA_COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_ADDRESS1), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_ADDRESS1, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property City As String
        Get
            If Row(DATA_COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CITY), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CITY, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property Region As String
        Get
            If Row(DATA_COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_REGION), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_REGION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property PostalCode As String
        Get
            If Row(DATA_COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_POSTAL_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property CountryCode As String
        Get
            If Row(DATA_COL_NAME_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COUNTRY_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COUNTRY_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=60)>
    Public Property HomePhone As String
        Get
            If Row(DATA_COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_HOME_PHONE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_HOME_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property Manufacturer As String
        Get
            If Row(DATA_COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MANUFACTURER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MANUFACTURER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property Model As String
        Get
            If Row(DATA_COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MODEL), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ModelYear As Integer
        Get
            If Row(DATA_COL_NAME_MODEL_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MODEL_YEAR), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MODEL_YEAR, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property EngineVersion As String
        Get
            If Row(DATA_COL_NAME_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_VERSION), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VERSION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=480)>
    Public Property Tag As String
        Get
            If Row(DATA_COL_NAME_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_TAG), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_TAG, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property Mileage As Integer
        Get
            If Row(DATA_COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_ODOMETER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_ODOMETER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property VIN As String
        Get
            If Row(DATA_COL_NAME_VIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_VIN), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VIN, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PurchasePrice As Double
        Get
            If Row(DATA_COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PURCHASE_PRICE), Double))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PurchaseDate As DateTime
        Get
            If Row(DATA_COL_NAME_PURCHASE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PURCHASE_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PURCHASE_DATE, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InServiceDate As DateTime
        Get
            If Row(DATA_COL_NAME_IN_SERVICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_IN_SERVICE_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_IN_SERVICE_DATE, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DeliveryDate As DateTime
        Get
            If Row(DATA_COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DELIVERY_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DELIVERY_DATE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property PlanCode As String
        Get
            If Row(DATA_COL_NAME_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PLAN_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PLAN_CODE, Value)
        End Set
    End Property

    Public Property QuoteItemNumber As Integer
        Get
            If Row(DATA_COL_NAME_QUOTE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_QUOTE_ITEM_NUMBER), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_QUOTE_ITEM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property Deductible As Double
        Get
            If Row(DATA_COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DEDUCTIBLE), Double))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property TermMonths As Integer
        Get
            If Row(DATA_COL_NAME_TERM_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_TERM_MONTHS), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_TERM_MONTHS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property TermMiles As Integer
        Get
            If Row(DATA_COL_NAME_TERM_KI_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_TERM_KI_MI), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_TERM_KI_MI, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=120)>
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property AgentNumber As String
        Get
            If Row(DATA_COL_NAME_AGENT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_AGENT_NAME), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_AGENT_NAME, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property WarrantyDate As DateTime
        Get
            If Row(DATA_COL_NAME_WARRANTY_SALE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_WARRANTY_SALE_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_WARRANTY_SALE_DATE, value)
        End Set
    End Property

    Public Property DocIssueDate As DateTime
        Get
            If Row(DATA_COL_NAME_DOC_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DOC_ISSUE_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DOC_ISSUE_DATE, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property PlanAmount As Double
        Get
            If Row(DATA_COL_NAME_PLAN_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PLAN_AMOUNT), Double))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PLAN_AMOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=16)>
    Public Property DocType As String
        Get
            If Row(DATA_COL_NAME_DOC_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DOC_TYPE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DOC_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=80)>
    Public Property IdentityDocNo As String
        Get
            If Row(DATA_COL_NAME_IDENTITY_DOC_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_IDENTITY_DOC_NO), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_IDENTITY_DOC_NO, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=32)>
    Public Property RgNo As String
        Get
            If Row(DATA_COL_NAME_RG_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_RG_NO), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_RG_NO, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=160)>
    Public Property IdType As String
        Get
            If Row(DATA_COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_ID_TYPE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_ID_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=240)>
    Public Property DocAgency As String
        Get
            If Row(DATA_COL_NAME_DOC_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DOC_AGENCY), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DOC_AGENCY, Value)
        End Set
    End Property

    Public Property Quote As String
        Get

            If Row(DATA_COL_NAME_QUOTE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_QUOTE), String))
            End If
        End Get
        Set

            Try

                Value = Convert.ToInt32(Value).ToString

            Catch ex As Exception

                Throw New Exception("QUOTE NOT NUMERIC")

            End Try

            CheckDeleted()
            SetValue(DATA_COL_NAME_QUOTE, Value)
        End Set
    End Property

    Public Property DateOfBirth As DateTime
        Get
            If Row(DATA_COL_NAME_DATE_OF_BIRTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DATE_OF_BIRTH), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DATE_OF_BIRTH, value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property WorkPhone As String
        Get
            If Row(DATA_COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_WORK_PHONE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_WORK_PHONE, Value)
        End Set
    End Property



    <ValueMandatory(""), ValidStringLength("", Max:=4)>
    Public Property CollectionMethodCode As String
        Get
            If Row(DATA_COL_NAME_COLLECTION_METHOD_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COLLECTION_METHOD_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COLLECTION_METHOD_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4)>
    Public Property PaymentInstrumentCode As String
        Get
            If Row(DATA_COL_NAME_PAYMENT_INSTRUMENT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PAYMENT_INSTRUMENT_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PAYMENT_INSTRUMENT_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property InstallmentsNumber As Integer
        Get
            If Row(DATA_COL_NAME_INSTALLMENTS_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_INSTALLMENTS_NUMBER), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_INSTALLMENTS_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4)>
    Public Property CreditCardTypeCode As String
        Get
            Return _CreditCardTypeCode
        End Get
        Set
            CheckDeleted()
            _CreditCardTypeCode = Value
        End Set
    End Property
    Private _CreditCardTypeCode As String
    Private _NameOnCreditCard As String
    Private _CreditCardNumber As String
    Private _ExpirationDate As String
    Private _SecurityCode As Integer
    Private _BankID As String
    Private _AccountNumber As String
    Private _NameOnAccount As String
    Private _PaymentAuthoriztionNum As String
    Private _CardSecurityCode As String

    <ValidStringLength("", Max:=50)>
    Public Property NameOnCreditCard As String
        Get
            Return _NameOnCreditCard
        End Get
        Set
            CheckDeleted()
            _NameOnCreditCard = Value
        End Set
    End Property

    <ValidStringLength("", Max:=16), ValidCreditCardNumber("")>
    <PciProtect(PciDataType.CreditCardNumber)>
    Public Property CreditCardNumber As String
        Get
            Return _CreditCardNumber
        End Get
        Set
            CheckDeleted()
            _CreditCardNumber = Value
        End Set
    End Property

    <ValidStringLength("", Max:=7)>
    Public Property ExpirationDate As String
        Get
            Return _ExpirationDate
        End Get
        Set
            CheckDeleted()
            _ExpirationDate = Value
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property BankID As String
        Get
            Return _BankID
        End Get
        Set
            CheckDeleted()
            _BankID = Value
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property CardSecurityCode As String
        Get
            Return _CardSecurityCode
        End Get
        Set
            CheckDeleted()
            _CardSecurityCode = Value
        End Set
    End Property
    <ValidStringLength("", Max:=29)>
    Public Property AccountNumber As String
        Get
            Return _AccountNumber
        End Get
        Set
            CheckDeleted()
            _AccountNumber = Value
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property NameOnAccount As String
        Get
            Return _NameOnAccount
        End Get
        Set
            CheckDeleted()
            _NameOnAccount = Value
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property PaymentAuthoriztionNum As String
        Get
            Return _PaymentAuthoriztionNum
        End Get
        Set
            CheckDeleted()
            _PaymentAuthoriztionNum = Value
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ExternalCarCode As String
        Get
            If Row(DATA_COL_NAME_EXTERNAL_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_EXTERNAL_CAR_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_EXTERNAL_CAR_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property Is_CreditCard_AuthReq As String
        Get
            If Row(DATA_COL_NAME_IS_CREDITCARD_AUTHREQ) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_IS_CREDITCARD_AUTHREQ), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_IS_CREDITCARD_AUTHREQ, Value)
        End Set
    End Property

    Public Property SalesTax As Double
        Get
            If Row(DATA_COL_NAME_SALES_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SALES_TAX), Double))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SALES_TAX, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property ValidateOnly As String
        Get
            If Row(DATA_COL_NAME_VALIDATE_ONLY) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_VALIDATE_ONLY), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VALIDATE_ONLY, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String

        Dim row As DataRow

        Dim oEnrollmentData As New EnrollmentData
        With oEnrollmentData

            .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            .UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            .Address = Address
            .AgentNumber = AgentNumber
            .Certificate = CertificateNumber
            .City = City
            .DealerCode = DealerCode
            .Deductible = Deductible
            .DocAgency = DocAgency
            .DocDate = DocIssueDate
            .DocIdentityNo = IdentityDocNo
            .DocType = DocType
            .EngineVersion = EngineVersion
            .HomePhone = HomePhone
            .IdType = IdType
            .InServiceDate = InServiceDate
            .DeliveryDate = DeliveryDate
            .Manufacturer = Manufacturer
            .Mileage = Mileage
            .Model = Model
            .PlanAmount = PlanAmount
            .PlanCode = PlanCode
            .QuoteItemNumber = QuoteItemNumber
            .PostalCode = PostalCode
            .CountryCode = CountryCode
            .PurchaseDate = PurchaseDate
            .PurchasePrice = PurchasePrice
            .Region = Region
            .RgNo = RgNo
            .Tag = Tag
            .TermMiles = TermMiles
            .TermMonths = TermMonths
            .VIN = VIN
            .WarrantyDate = WarrantyDate
            .Year = ModelYear
            .Quote = Quote
            .DateOfBirth = DateOfBirth
            .WorkPhone = WorkPhone

            .PaymentInstrumentCode = PaymentInstrumentCode
            .CollectionMethodCode = CollectionMethodCode

            .InstallmentsNumber = InstallmentsNumber
            .CreditCardTypeCode = CreditCardTypeCode
            .NameOnCreditCard = NameOnCreditCard
            .CreditCardNumber = CreditCardNumber
            .ExpirationDate = ExpirationDate
            .BankID = BankID
            .AccountNumber = AccountNumber
            .NameOnAccount = NameOnAccount

            .Customers = String.Empty
            .Options = String.Empty

            ' Add the customers
            For Each row In Dataset.Tables(TABLE_NAME_CUSTOMER).Rows
                If .Customers.Trim.Length > 0 Then .Customers &= END_OF_LINE
                .Customers &= row(0) & END_OF_FIELD & row(1) & END_OF_FIELD & row(2) & END_OF_FIELD & row(3)
            Next

            ' Add the options
            Dim objCulture As New System.Globalization.CultureInfo("en-Us")
            For Each row In Dataset.Tables(TABLE_NAME_OPTIONS).Rows
                If .Options.Trim.Length > 0 Then .Options &= END_OF_LINE
                .Options &= row(2) & END_OF_FIELD & row(0) & END_OF_FIELD & CType(row(1), Decimal).ToString(objCulture)
            Next

            .PymtAuthNum = PaymentAuthoriztionNum
            .ExternalCarCode = ExternalCarCode
            .CardSecurityCode = CardSecurityCode
            If Is_CreditCard_AuthReq = IS_CREDITCARD_AUTHREQ_Y Then
                .Is_CreditCard_AuthReq = Is_CreditCard_AuthReq
            Else
                .Is_CreditCard_AuthReq = IS_CREDITCARD_AUTHREQ_N
            End If

            .SalesTax = SalesTax
            .ValidateOnly = ValidateOnly
            .SalesTaxPresent = _salesTaxPresent
        End With

        Try

            If Not CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__DEALER_COLLECTS) Then

                'Validate Bank Info 
                If CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS) And PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__DEBIT_ACCOUNT) Then
                    Dim objBankInfo As New BankInfo
                    objBankInfo.Account_Name = NameOnAccount
                    objBankInfo.Account_Number = AccountNumber
                    objBankInfo.Bank_Id = BankID
                    objBankInfo.CountryID = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId).Id
                    objBankInfo.Validate()
                ElseIf (CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS) AndAlso PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__CREDIT_CARD)) OrElse
                       (CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__THRID_PARTY_COLLECTS) AndAlso (PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD) Or PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY))) OrElse
                       (CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH) AndAlso PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD)) Then
                    'Validate credit card
                    'If Not CreditCardFormat.IsCreditCardValid(CreditCardTypeCode, CreditCardNumber) Then
                    '    Throw New StoredProcedureGeneratedException("Credit Card Error", Common.ErrorCodes.WS_INVALID_CREDIT_CARD_NUMBER)
                    'End If
                    'Validate Experation date
                    If Not ExpirationDate.Length = 7 Then
                        Throw New StoredProcedureGeneratedException("Credit Card Error", Common.ErrorCodes.WS_INVALID_EXPIRATION_DATE)
                    Else
                        Try
                            Dim intMonth As Integer = CType(ExpirationDate.Substring(0, 2), Integer)
                            Dim intYear As Integer = CType(ExpirationDate.Substring(3, 4), Integer)
                            Dim current_Month As Integer = Now.Month
                            Dim current_Year As Integer = Now.Year

                            If (intMonth < 1 Or intMonth > 12) OrElse (intYear < current_Year) OrElse (intYear = current_Year AndAlso intMonth < current_Month) Then
                                Throw New StoredProcedureGeneratedException("Credit Card Error", Common.ErrorCodes.WS_INVALID_EXPIRATION_DATE)
                            End If

                        Catch ex As Exception
                            Throw New StoredProcedureGeneratedException("Credit Card Error", Common.ErrorCodes.WS_INVALID_EXPIRATION_DATE)
                        End Try

                    End If
                End If
            End If

            Validate()

            If IsNew AndAlso Not CreditCardNumber Is Nothing Then
                Try
                    ' secure the credit card number
                    Secure()

                Catch ex As Exception
                    'If ex.InnerException.GetType() Is GetType(FaultException) Then
                    '    Dim faultExcep As FaultException
                    '    faultExcep = DirectCast(ex.InnerException,FaultException)
                    '    Throw New ElitaPlusException(faultExcep.Message, faultExcep.Code.Name, ex.InnerException)
                    'End If
                    Throw New ElitaPlusException(ex.Message, ElitaPlus.Common.ErrorCodes.PCI_SECURE_ERR, ex)
                End Try

                oEnrollmentData.CreditCardNumber = CreditCardNumber
            End If

            Dim dal As VSCEnrollmentDAL = New VSCEnrollmentDAL
            ' Enroll
            enrollmentId = dal.Enroll(oEnrollmentData)

            ' Certificate
            If Not oEnrollmentData.ValidateOnly.Equals("Y") Then

                If (CollectionMethodCode.ToUpper.Equals(Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH) AndAlso PaymentInstrumentCode.ToUpper.Equals(Codes.PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD)) Then
                    dal.Certificate(enrollmentId, CollectionMethodCode, PaymentInstrumentCode)
                Else
                    Dim t As Thread = New Thread(AddressOf Certificate)
                    t.Start()
                End If

                If oEnrollmentData.Certificate Is Nothing OrElse oEnrollmentData.Certificate.Equals(String.Empty) Then
                    ' Set the acknoledge OK response using the auto generated certificate number
                    Return XMLHelper.GetXML_OK_Response(DATA_COL_NAME_CERTIFICATE, oEnrollmentData.AutoGeneratedCertificateNumber, OUTPUT_TABLE_NAME.ToUpper)
                Else
                    ' Set the acknoledge OK response
                    Return XMLHelper.GetXML_OK_Response
                End If

            Else
                ' Set the acknoledge OK response
                Return XMLHelper.GetXML_OK_Response
            End If

        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Exception processing the request", Common.ErrorCodes.UNEXPECTED_ERROR, ex)
        End Try

    End Function

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCreditCardNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.WS_INVALID_CREDIT_CARD_NUMBER)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Enrollment = CType(objectToValidate, Enrollment)

            If Not obj.CreditCardNumber Is Nothing AndAlso Not CreditCardFormat.IsCreditCardValid(obj.CreditCardTypeCode, obj.CreditCardNumber) Then
                Return False
            End If
            Return True
        End Function
    End Class
    '<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    'Public NotInheritable Class EmailAddress
    '    Inherits ValidBaseAttribute

    '    Public Sub New(ByVal fieldDisplayName As String)
    '        MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
    '    End Sub

    '    Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
    '        Dim obj As Enrollment = CType(objectToValidate, Enrollment)

    '        If obj.Email Is Nothing Then
    '            Return True
    '        End If

    '        Return MiscUtil.EmailAddressValidation(obj.Email)

    '    End Function

    'End Class

#End Region
End Class
