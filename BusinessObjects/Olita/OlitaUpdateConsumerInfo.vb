Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.BusinessObjectsNew.OlitaUpdateConsumerInfoDs

Public Class OlitaUpdateConsumerInfo
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER As String = "dealer"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_FULL_CERT_NUMBER As String = "full_cert_number"

    Public Const DATA_COL_SALUTATION As String = "salutation"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const DATA_COL_NAME_ADDRESS1 As String = "address1"
    Public Const DATA_COL_NAME_ADDRESS2 As String = "address2"
    Public Const DATA_COL_NAME_CITY As String = "city"
    Public Const DATA_COL_NAME_REGION_SHORT_DESCRIPTION As String = "short_desc"
    Public Const DATA_COL_NAME_COUNTRY_CODE As String = "code"
    Public Const DATA_COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const DATA_COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const DATA_COL_NAME_EMAIL As String = "email"
    Public Const DATA_COL_NAME_USER_PASSWORD As String = "user_pw"

    Public Const DATA_COL_NAME_CERT_ID As String = "cert_id"
    Public Const DATA_COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const DATA_COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const DATA_COL_NAME_REGION_ID As String = "region_id"
    Public Const DATA_COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"

    Public Const DATA_COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const DATA_COL_NAME_MEMBERSHIP_NUMBER As String = "membership_number"
    Public Const DATA_COL_NAME_PRIMARY_MEMBER_NAME As String = "primary_member_name"
    Public Const DATA_COL_NAME_MAILING_ADDRESS1 As String = "mailing_address1"
    Public Const DATA_COL_NAME_MAILING_ADDRESS2 As String = "mailing_address2"
    Public Const DATA_COL_NAME_MAILING_CITY As String = "mailing_city"
    Public Const DATA_COL_NAME_MAILING_REGION_SHORT_DESCRIPTION As String = "mailing_region_short_desc"
    Public Const DATA_COL_NAME_MAILING_COUNTRY_CODE As String = "mailing_country_code"
    Public Const DATA_COL_NAME_MAILING_POSTAL_CODE As String = "mailing_postal_code"
    Public Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const DATA_COL_NAME_MEMBERSHIP_TYPE As String = "membership_type"
    Public Const DATA_COL_NAME_PRODUCT_SALES_DATE As String = "product_sales_date"
    Public Const DATA_COL_NAME_SALES_PRICE As String = "sales_price"
    Public Const DATA_COL_NAME_PRODUCT_SERIAL_NUMBER As String = "product_serial_number"
    Public Const DATA_COL_NAME_PRODUCT_DESCRIPTION As String = "product_description"
    Public Const DATA_COL_NAME_PRODUCT_ITEM_CODE As String = "product_item_code"
    Public Const DATA_COL_NAME_PRODUCT_MANUFACTURER As String = "product_manufacturer"
    Public Const DATA_COL_NAME_PRODUCT_MODEL As String = "product_model"
    Public Const DATA_COL_NAME_PRODUCT_ITEM_RETAIL_PRICE As String = "product_item_retail_price"
    Public Const DATA_COL_NAME_VAT_NUM As String = "vat_num"
    Public Const DATA_COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"

    Private Const TABLE_NAME As String = "OlitaUpdateConsumerInfo"
    Private Const TABLE_NAME_PRODUCT_SERIAL_NUMBER As String = "product_serial_numbers"

    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    'Error msgs
    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const COUNTRY_NOT_FOUND As String = "ERR_COUNTRY_NOT_FOUND"
    Private Const REGION_NOT_FOUND As String = "ERR_REGION_NOT_FOUND"
    Private Const MAILING_COUNTRY_NOT_FOUND As String = "ERR_MAILING_COUNTRY_NOT_FOUND"
    Private Const MAILING_REGION_NOT_FOUND As String = "ERR_MAILING_REGION_NOT_FOUND"
    Private Const UPDATE_FAILED As String = "ERR_UPDATE_FAILED"
    Private Const MAILING_INCOMPLETE As String = "ERR_MAILING_INCOMPLETE"
    Private Const MEMBERSHIP_TYPE_NOT_FOUND As String = "ERR_MEMBERSHIP_TYPE_NOT_FOUND"
    Private Const MANUFACTURER_NOT_FOUND As String = "ERR_MANUFACTURER_NOT_FOUND"
#End Region

#Region "Variables"

    Private mbIsEndorse As Boolean
    Private moCertEndorse As CertEndorse

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As OlitaUpdateConsumerInfoDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _countryId As Guid = Guid.Empty
    Private _regionId As Guid = Guid.Empty
    Private _mailing_countryId As Guid = Guid.Empty
    Private _mailing_regionId As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As OlitaUpdateConsumerInfoDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New Dataset
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As OlitaUpdateConsumerInfoDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita UpdateConsumerInfo Saving Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub
    Public Function GetSalutationDesc(ByVal originalSalutationCode As String) As String
        Dim salutationDesc As String
        Dim list As DataView = LookupListNew.GetSalutationLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        salutationDesc = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_SALUTATION, originalSalutationCode)

        Return salutationDesc
    End Function

    Private Sub PopulateBOFromWebService(ByVal ds As OlitaUpdateConsumerInfoDs)
        Dim _CertListDataSet As DataSet
        Dim oCert As Certificate
        Dim isProdSalesDateDirty As Boolean = False
        Dim isWarrantySalesDateDirty As Boolean = False
        Dim isSalesPriceDirty As Boolean = False

        Try
            If ds.OlitaUpdateConsumerInfo.Count = 0 Then Exit Sub
            With ds.OlitaUpdateConsumerInfo.Item(0)
                Me.DealerCode = .dealer
                Me.CertNumber = .cert_number
                If Not .IsSALUTATIONNull Then Me.SalutationCode = .SALUTATION
                Me.CustomerName = .customer_name
                Me.Address1 = .address1
                If Not .Isaddress2Null Then Me.Address2 = .address2
                Me.City = .city
                Me.ShortDescription = .short_desc
                Me.CountryCode = .code
                Me.PostalCode = .postal_code
                Me.HomePhone = .home_phone
                Me.Email = .email
                Me.UserPassword = .user_pw
                If Not .Iswork_phoneNull Then Me.WorkPhone = .work_phone
                If Not .Ismembership_numberNull Then Me.MembershipNumber = .membership_number
                If Not .Isprimary_member_nameNull Then Me.PrimaryMemberName = .primary_member_name
                If Not .Ismailing_address1Null Then Me.MailingAddress1 = .mailing_address1
                If Not .Ismailing_address2Null Then Me.MailingAddress2 = .mailing_address2
                If Not .Ismailing_cityNull Then Me.MailingCity = .mailing_city
                If Not .Ismailing_region_short_descNull Then Me.MailingRegionShortDescription = .mailing_region_short_desc
                If Not .Ismailing_country_codeNull Then Me.MailingCountryCode = .mailing_country_code
                If Not .Ismailing_postal_codeNull Then Me.MailingPostalCode = .mailing_postal_code
                If Not .Iswarranty_sales_dateNull Then Me.WarrantySalesDate = .warranty_sales_date
                If Not .Ismembership_typeNull Then Me.MembershipType = .membership_type
                If Not .Isproduct_sales_dateNull Then Me.ProductSalesDate = .product_sales_date
                If Not .Issales_priceNull Then Me.SalesPrice = .sales_price
                If Not .Isvat_numNull Then Me.VATNum = .vat_num            
                If Not .Isidentification_numberNull Then Me.IdentificationNumber = .identification_number

                If ((Not .Isproduct_sales_dateNull) OrElse (Not .Iswarranty_sales_dateNull) OrElse _
                    (Not .Issales_priceNull)) Then
                    _CertListDataSet = Certificate.GetCertificatesList(Me.CertNumber, "", "", "", "", Me.DealerCode).Table.DataSet
                    If (Not _CertListDataSet Is Nothing AndAlso _CertListDataSet.Tables.Count > 0 AndAlso _CertListDataSet.Tables(0).Rows.Count > 0) AndAlso _
                        (Not _CertListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value) Then
                        oCert = New Certificate(New Guid(CType(_CertListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte())))
                    Else
                        Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", CERTIFICATE_NOT_FOUND)
                    End If
                    If ((Not .Isproduct_sales_dateNull) AndAlso oCert.ProductSalesDate <> (New DateType(.product_sales_date))) Then isProdSalesDateDirty = True
                    If ((Not .Iswarranty_sales_dateNull) AndAlso oCert.WarrantySalesDate <> (New DateType(.warranty_sales_date))) Then isWarrantySalesDateDirty = True
                    If ((Not .Issales_priceNull) AndAlso oCert.SalesPrice <> (New DecimalType(.sales_price))) Then isSalesPriceDirty = True

                    If ((isProdSalesDateDirty = True) OrElse _
                        (isWarrantySalesDateDirty = True) OrElse _
                        (isSalesPriceDirty = True)) Then
                        IsEndorse = True
                        TheCertEndorse = New CertEndorse()
                        TheCertEndorse.ProductSalesDatesisDirty = isProdSalesDateDirty
                        TheCertEndorse.WarrantySalesDatesisDirty = isWarrantySalesDateDirty
                        TheCertEndorse.SalesPriceisDirty = isSalesPriceDirty
                    Else
                        IsEndorse = False
                    End If
                End If
            End With

            ' Populate Product serial numbers
            Dim i As Integer
            For i = 0 To ds.product_serial_numbers.Count - 1

                NewProductSerialNumber(ds.product_serial_numbers(i))
            Next

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    

    Private Sub NewProductSerialNumber(ByVal aProdRow As product_serial_numbersRow)
        Dim newRow As DataRow = Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).NewRow
        With aProdRow
            newRow(DATA_COL_NAME_FULL_CERT_NUMBER) = .full_cert_number
            newRow(DATA_COL_NAME_PRODUCT_SERIAL_NUMBER) = .product_serial_number
            If Not .Isproduct_descriptionNull Then newRow(DATA_COL_NAME_PRODUCT_DESCRIPTION) = .product_description
            If Not .Isproduct_item_codeNull Then newRow(DATA_COL_NAME_PRODUCT_ITEM_CODE) = .product_item_code
            If Not .Isproduct_manufacturerNull Then newRow(DATA_COL_NAME_PRODUCT_MANUFACTURER) = .product_manufacturer
            If Not .Isproduct_modelNull Then newRow(DATA_COL_NAME_PRODUCT_MODEL) = .product_model
            If Not .Isproduct_item_retail_priceNull Then newRow(DATA_COL_NAME_PRODUCT_ITEM_RETAIL_PRICE) = .product_item_retail_price
        End With
        'newRow(0) = certNumber
        'newRow(1) = serialNumber
        Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).Rows.Add(newRow)

    End Sub
    Protected Shadows Sub CheckDeleted()
    End Sub
#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_DEALER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property
    Public Property SalutationCode() As String
        Get
            If Row(Me.DATA_COL_SALUTATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_SALUTATION), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_SALUTATION, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property CustomerName() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Address1() As String
        Get
            If Row(Me.DATA_COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_ADDRESS1, Value)
        End Set
    End Property

    Public Property Address2() As String
        Get
            If Row(Me.DATA_COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property City() As String
        Get
            If Row(Me.DATA_COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CITY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ShortDescription() As String
        Get
            If Row(Me.DATA_COL_NAME_REGION_SHORT_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_REGION_SHORT_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_REGION_SHORT_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidUserCountry("")> _
    Public Property CountryCode() As String
        Get
            If Row(Me.DATA_COL_NAME_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_COUNTRY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COUNTRY_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property PostalCode() As String
        Get
            If Row(Me.DATA_COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property HomePhone() As String
        Get
            If Row(Me.DATA_COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_HOME_PHONE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Email() As String
        Get
            If Row(Me.DATA_COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_EMAIL, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UserPassword() As String
        Get
            If Row(Me.DATA_COL_NAME_USER_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_USER_PASSWORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_USER_PASSWORD, Value)
        End Set
    End Property

    Public Property WorkPhone() As String
        Get
            If Row(Me.DATA_COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_WORK_PHONE, Value)
        End Set
    End Property

    Public Property MembershipNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_MEMBERSHIP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MEMBERSHIP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MEMBERSHIP_NUMBER, Value)
        End Set
    End Property

    Public Property PrimaryMemberName() As String
        Get
            If Row(Me.DATA_COL_NAME_PRIMARY_MEMBER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_PRIMARY_MEMBER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_PRIMARY_MEMBER_NAME, Value)
        End Set
    End Property

    Public Property MailingAddress1() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_ADDRESS1, Value)
        End Set
    End Property

    Public Property MailingAddress2() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_ADDRESS2, Value)
        End Set
    End Property

    Public Property MailingCity() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_CITY, Value)
        End Set
    End Property

    Public Property MailingRegionShortDescription() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_REGION_SHORT_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_REGION_SHORT_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_REGION_SHORT_DESCRIPTION, Value)
        End Set
    End Property

    Public Property MailingCountryCode() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_COUNTRY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_COUNTRY_CODE, Value)
        End Set
    End Property

    Public Property MailingPostalCode() As String
        Get
            If Row(Me.DATA_COL_NAME_MAILING_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MAILING_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAILING_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property WarrantySalesDate() As DateType
        Get
            CheckDeleted()
            If Row(Me.DATA_COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(Me.DATA_COL_NAME_WARRANTY_SALES_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    Public Property MembershipType() As String
        Get
            If Row(Me.DATA_COL_NAME_MEMBERSHIP_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_MEMBERSHIP_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MEMBERSHIP_TYPE, Value)
        End Set
    End Property

    Public Property ProductSalesDate() As DateType
        Get
            CheckDeleted()
            If Row(Me.DATA_COL_NAME_PRODUCT_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(Me.DATA_COL_NAME_PRODUCT_SALES_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_PRODUCT_SALES_DATE, Value)
        End Set
    End Property

    Public Property SalesPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(Me.DATA_COL_NAME_SALES_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(Me.DATA_COL_NAME_SALES_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SALES_PRICE, Value)
        End Set
    End Property

    Public Property VATNum() As String
        Get
            If Row(Me.DATA_COL_NAME_VAT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_VAT_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_VAT_NUM, Value)
        End Set
    End Property

    Public Property IdentificationNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property
#End Region

#Region "Extended Properties"

    Private ReadOnly Property CountryId() As Guid
        Get
            If Me._countryId.Equals(Guid.Empty) Then
                Dim objCountryDV As DataView = Country.getList("", Me.CountryCode)
                If objCountryDV Is Nothing Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.ERROR_ACCESSING_DATABASE)
                ElseIf objCountryDV.Count <> 1 Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.COUNTRY_NOT_FOUND)
                End If
                Me._countryId = New Guid(CType(objCountryDV.Table.Rows(0).Item(Me.DATA_COL_NAME_COUNTRY_ID), Byte()))
                objCountryDV = Nothing
            End If

            Return Me._countryId
        End Get
    End Property

    Private ReadOnly Property RegionId() As Guid
        Get
            If Me._regionId.Equals(Guid.Empty) Then
                'Dim alUserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim regionsDV As DataView = Region.LoadList("%", Me.ShortDescription, ElitaPlusIdentity.Current.ActiveUser.Companies)
                If regionsDV Is Nothing Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.ERROR_ACCESSING_DATABASE)
                ElseIf regionsDV.Count <> 1 Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.REGION_NOT_FOUND)
                End If
                Me._regionId = New Guid(CType(regionsDV.Table.Rows(0).Item(Me.DATA_COL_NAME_REGION_ID), Byte()))
                regionsDV = Nothing
            End If
            Return Me._regionId
        End Get
    End Property

    'Private ReadOnly Property MailingCountryId() As Guid
    '    Get
    '        If Me._mailing_countryId.Equals(Guid.Empty) Then
    '            Dim objMailingCountryDV As DataView = Country.getList("", Me.CountryCode)
    '            If objMailingCountryDV Is Nothing Then
    '                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.ERROR_ACCESSING_DATABASE)
    '            ElseIf objMailingCountryDV.Count <> 1 Then
    '                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.MAILING_COUNTRY_NOT_FOUND)
    '            End If
    '            Me._mailing_countryId = New Guid(CType(objMailingCountryDV.Table.Rows(0).Item(Me.DATA_COL_NAME_COUNTRY_ID), Byte()))
    '            objMailingCountryDV = Nothing
    '        End If

    '        Return Me._mailing_countryId
    '    End Get
    'End Property

    Private ReadOnly Property MailingCountryId() As Guid
        Get
            If Me.MailingCountryCode Is Nothing Then
                Return Me._mailing_countryId
            End If
            If Me.MailingCountryCode = String.Empty Then
                Return Me._mailing_countryId
            End If
            If Me._mailing_countryId.Equals(Guid.Empty) Then
                Dim objMailingCountryDV As DataView = Country.getList("", Me.MailingCountryCode)
                If objMailingCountryDV Is Nothing Then
                    Return Me._mailing_countryId
                ElseIf objMailingCountryDV.Count <> 1 Then
                    Return Me._mailing_countryId
                End If
                Me._mailing_countryId = New Guid(CType(objMailingCountryDV.Table.Rows(0).Item(Me.DATA_COL_NAME_COUNTRY_ID), Byte()))
                objMailingCountryDV = Nothing
            End If

            Return Me._mailing_countryId
        End Get
    End Property

    'Private ReadOnly Property MailingRegionId() As Guid
    '    Get
    '        If Me._mailing_regionId.Equals(Guid.Empty) Then
    '            Dim mailing_regionsDV As DataView = Region.LoadList("%", Me.MailingRegionShortDescription, Me.MailingCountryId)
    '            If mailing_regionsDV Is Nothing Then
    '                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.ERROR_ACCESSING_DATABASE)
    '            ElseIf mailing_regionsDV.Count <> 1 Then
    '                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.MAILING_REGION_NOT_FOUND)
    '            End If
    '            Me._mailing_regionId = New Guid(CType(mailing_regionsDV.Table.Rows(0).Item(Me.DATA_COL_NAME_REGION_ID), Byte()))
    '            mailing_regionsDV = Nothing
    '        End If
    '        Return Me._mailing_regionId
    '    End Get
    'End Property

    Private ReadOnly Property MailingRegionId() As Guid
        Get
            If Me.MailingRegionShortDescription Is Nothing Then
                Return Me._mailing_regionId
            End If
            If Me.MailingRegionShortDescription = String.Empty Then
                Return Me._mailing_regionId
            End If
            If Me._mailing_regionId.Equals(Guid.Empty) Then
                Dim mailing_regionsDV As DataView = Region.LoadList("%", Me.MailingRegionShortDescription, Me.MailingCountryId)
                If mailing_regionsDV Is Nothing Then
                    Return Me._mailing_regionId
                ElseIf mailing_regionsDV.Count <> 1 Then
                    Return Me._mailing_regionId
                End If
                Me._mailing_regionId = New Guid(CType(mailing_regionsDV.Table.Rows(0).Item(Me.DATA_COL_NAME_REGION_ID), Byte()))
                mailing_regionsDV = Nothing
            End If
            Return Me._mailing_regionId
        End Get
    End Property

    Private ReadOnly Property ManufacturerId(ByVal description As String) As Guid
        Get
            Dim _manufacturerId As Guid = Guid.Empty

            Dim list As DataView = LookupListNew.GetManufacturerLookupList(Authentication.CompanyGroupId)
            _manufacturerId = LookupListNew.GetIdFromCode(list, description)
            If _manufacturerId.Equals(Guid.Empty) Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.MANUFACTURER_NOT_FOUND)
            End If
            '   _manufacturerId = LookupListNew.GetIdFromDescription(LookupListNew.LK_MANUFACTURERS, description)


            'Dim manufacturersDV As DataView = Manufacturer.LoadList(description, Authentication.CompanyGroupId)
            'If manufacturersDV Is Nothing Then
            '    Return _manufacturerId
            'ElseIf manufacturersDV.Count <> 1 Then
            '    Return _manufacturerId
            'End If
            '_manufacturerId = New Guid(CType(manufacturersDV.Table.Rows(0).Item(Me.DATA_COL_NAME_MANUFACTURER_ID), Byte()))
            'manufacturersDV = Nothing

            Return _manufacturerId
        End Get
    End Property

    Private ReadOnly Property MembershipTypeId(ByVal code As String) As Guid
        Get
            Dim _membershipTypeId As Guid = Guid.Empty
            Dim list As DataView = LookupListNew.GetMembershipTypeLanguageLookupList(Authentication.LangId)
            _membershipTypeId = LookupListNew.GetIdFromCode(list, code)
            If _membershipTypeId.Equals(Guid.Empty) Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.MEMBERSHIP_TYPE_NOT_FOUND)
            End If
            '  _membershipTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_MEMBERSHIP_TYPE_LANGUAGE, code)

            Return _membershipTypeId
        End Get
    End Property
    Private ReadOnly Property SalutationId(ByVal code As String) As Guid
        Get
            Dim _salutationId As Guid = Guid.Empty
            Dim list As DataView = LookupListNew.GetSalutationLanguageLookupList(Authentication.LangId)
            _salutationId = LookupListNew.GetIdFromCode(list, code)
            Return _salutationId
        End Get
    End Property

    Private Property IsEndorse() As Boolean
        Get
            Return mbIsEndorse
        End Get
        Set(ByVal value As Boolean)
            mbIsEndorse = value
        End Set
    End Property

    Private Property TheCertEndorse() As CertEndorse
        Get
            Return moCertEndorse
        End Get
        Set(ByVal value As CertEndorse)
            moCertEndorse = value
        End Set
    End Property
#End Region

    






#Region "Public Members"


    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            If Dealer.GetOlitaSearchType(compIds, Me.DealerCode).Equals(Codes.OLITA_SEARCH_GENERIC) Then
                Me.CertNumber += "*"
            End If
            Dim _CertListDataSet As DataSet = Certificate.GetCertificatesList(Me.CertNumber, "", "", "", "", Me.DealerCode).Table.DataSet
            If Not _CertListDataSet Is Nothing AndAlso _CertListDataSet.Tables.Count > 0 AndAlso _CertListDataSet.Tables(0).Rows.Count > 0 Then
                If _CertListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", CERTIFICATE_NOT_FOUND)
                Else
                    'if there are serial numbers to be update, then the Master BO will be the Cert_item, cert will be the child.
                    If Me.Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).Rows.Count > 0 Then
                        Try
                            ' It has Product Serial Number
                            Me.BuildCertItemParentAndCertChild(_CertListDataSet)

                        Catch exV As BOValidationException
                            Dim code As String

                            code = exV.Code
                            If code = String.Empty Then code = Me.UPDATE_FAILED
                            Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", code)
                        Catch ex As Exception
                            Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.UPDATE_FAILED)
                        End Try
                    Else
                        Try
                            ' Not Product Serial Number
                            Me.BuildCertParentAndAddressChildAndSave(_CertListDataSet)
                        Catch exV As BOValidationException
                            Dim code As String

                            code = exV.Code
                            If code = String.Empty Then code = Me.UPDATE_FAILED
                            Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", code)
                        Catch ex As Exception
                            Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.UPDATE_FAILED)
                        End Try
                    End If

                    ' Set the acknoledge OK response
                    Return XMLHelper.GetXML_OK_Response


                End If

            ElseIf _CertListDataSet Is Nothing Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.ERROR_ACCESSING_DATABASE)
            ElseIf Not _CertListDataSet Is Nothing AndAlso _CertListDataSet.Tables.Count > 0 AndAlso _CertListDataSet.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", CERTIFICATE_NOT_FOUND)
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Private Sub PopulateCertItemForProducts(ByVal aCertItem As CertItem, ByVal i As Integer)
        Dim aRow As DataRow = Me.Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).Rows(i)

        aCertItem.SerialNumber = aRow(Me.DATA_COL_NAME_PRODUCT_SERIAL_NUMBER)
        If Not aRow.IsNull(Me.DATA_COL_NAME_PRODUCT_DESCRIPTION) Then aCertItem.ItemDescription = aRow(Me.DATA_COL_NAME_PRODUCT_DESCRIPTION)
        If Not aRow.IsNull(Me.DATA_COL_NAME_PRODUCT_ITEM_CODE) Then aCertItem.ItemCode = _
                aRow(Me.DATA_COL_NAME_PRODUCT_ITEM_CODE)
        If Not aRow.IsNull(Me.DATA_COL_NAME_PRODUCT_MODEL) Then aCertItem.Model = aRow(Me.DATA_COL_NAME_PRODUCT_MODEL)
        If Not aRow.IsNull(Me.DATA_COL_NAME_PRODUCT_ITEM_RETAIL_PRICE) Then aCertItem.ItemRetailPrice = _
                New DecimalType(CType(aRow(Me.DATA_COL_NAME_PRODUCT_ITEM_RETAIL_PRICE), Decimal))

        If Not aRow.IsNull(Me.DATA_COL_NAME_PRODUCT_MANUFACTURER) Then aCertItem.ManufacturerId = ManufacturerId(aRow(Me.DATA_COL_NAME_PRODUCT_MANUFACTURER))

    End Sub

    ' For Product Serial Number
    Private Sub BuildCertItemParentAndCertChild(ByVal certListDataSet As DataSet)
        'build the cert_ids arraylist
        Dim certIds As New ArrayList
        Dim i As Integer
        Dim firstCertItem As CertItem = Nothing

        For i = 0 To certListDataSet.Tables(0).Rows.Count - 1
            certIds.Add(New Guid(CType(certListDataSet.Tables(0).Rows(i).Item(Me.DATA_COL_NAME_CERT_ID), Byte())))
        Next

        'Get the list of cert_item_id(s)
        Dim certItemTable As DataTable = CertItem.GetItemsForWS(certIds)

        'create the parent/childern cert_item object
        Dim certItemParentObj As CertItem
        For i = 0 To Me.Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).Rows.Count - 1
            For Each rowItem As DataRow In certItemTable.Rows
                If rowItem(CertItemDAL.COL_NAME_CERT_NUMBER).Equals(Me.Dataset.Tables(TABLE_NAME_PRODUCT_SERIAL_NUMBER).Rows(i).Item(Me.DATA_COL_NAME_FULL_CERT_NUMBER)) Then
                    If i = 0 Then
                        'first cert_item BO
                        If IsEndorse Then
                            certItemParentObj = New CertItem(New Guid(CType(rowItem.Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())), TheCertEndorse.Dataset)
                        Else
                            certItemParentObj = New CertItem(New Guid(CType(rowItem.Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())))
                        End If
                       PopulateCertItemForProducts(certItemParentObj, i)
                    Else
                        'childern cert_item BOs
                        Dim certItemChildObj As CertItem = New CertItem(New Guid(CType(rowItem.Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())), certItemParentObj.Dataset)
                        PopulateCertItemForProducts(certItemChildObj, i)
                    End If
                End If
            Next
        Next

        'create all the cert BOs as childern
        For i = 0 To certListDataSet.Tables(0).Rows.Count - 1
            Dim certChildObj As Certificate = New Certificate(New Guid(CType(certListDataSet.Tables(0).Rows(i).Item(Me.DATA_COL_NAME_CERT_ID), Byte())), certItemParentObj.Dataset)
            If certItemTable.Rows.Count > 0 Then
                firstCertItem = New CertItem(New Guid(CType(certItemTable.Rows(0).Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())))
            End If
            Me.PopulateCertificateWithConsumerInfo(True, certChildObj, firstCertItem)
            certChildObj.GetValFlag()
        Next
        'Finally save all
        If IsEndorse Then
            TheCertEndorse.Save()
        Else
            certItemParentObj.Save()
        End If

    End Sub

    ' It does not have Product Serial Number
    Private Function BuildCertParentAndAddressChildAndSave(ByVal certListDataSet As DataSet) As Boolean
        Dim certParentObj As Certificate
        ' create the parent cert
        If IsEndorse Then
            certParentObj = New Certificate(New Guid(CType(certListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte())), TheCertEndorse.Dataset)
        Else
            certParentObj = New Certificate(New Guid(CType(certListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte())))
        End If

        Me.PopulateCertificateWithConsumerInfo(False, certParentObj, Nothing)
        certParentObj.GetValFlag()

        For i As Integer = 1 To certListDataSet.Tables(0).Rows.Count - 1
            ' create the childern cert
            Dim certChildObj As Certificate = New Certificate(New Guid(CType(certListDataSet.Tables(0).Rows(i).Item(Me.DATA_COL_NAME_CERT_ID), Byte())), certParentObj.Dataset)
            Me.PopulateCertificateWithConsumerInfo(False, certChildObj, Nothing)
        Next

        certParentObj.GetValFlag()
        If IsEndorse Then
            TheCertEndorse.Save()
        Else
            certParentObj.Save()
        End If

    End Function

    Private Sub UpdateEndorsement(ByVal IsProdSerialN As Boolean, ByVal certBO As Certificate, _
                                                    ByVal certItemBo As CertItem)
        Dim certIds As New ArrayList
        Dim certItemTable As DataTable
        Dim firstCertItem As CertItem = certItemBo

        If (IsEndorse = True) Then
            If (IsProdSerialN = False) Then
                ' NO  Prod Serial Number Therefore, obtain first CertItem
                certIds.Add(certBO.Id)
                certItemTable = CertItem.GetItemsForWS(certIds)
                If certItemTable.Rows.Count > 0 Then
                    firstCertItem = New CertItem(New Guid(CType(certItemTable.Rows(0).Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())))
                End If
            End If
            If (Not firstCertItem Is Nothing) Then
                'Create an Endorsement
                With TheCertEndorse
                    .PopulateWithDefaultValues(certBO.Id, firstCertItem.Id)
                    If Not Me.ProductSalesDate Is Nothing Then .ProductSalesDatePost = Me.ProductSalesDate
                    If Not Me.WarrantySalesDate Is Nothing Then .WarrantySalesDatePost = Me.WarrantySalesDate
                    If Not Me.SalesPrice Is Nothing Then .SalesPricePost = Me.SalesPrice
                End With
            End If


        End If


    End Sub

    Private Sub PopulateCertificateWithConsumerInfo(ByVal IsProdSerialN As Boolean, ByVal certBO As Certificate, _
                                                    ByVal certItemBo As CertItem)

        
        certBO.SalutationId = Me.SalutationId(Me.SalutationCode)
        certBO.CustomerName = Me.CustomerName
        certBO.HomePhone = Me.HomePhone
        certBO.Email = Me.Email
        certBO.Password = Me.UserPassword

        Dim objAddress As Address = certBO.AddressChild
        If certBO.AddressId.Equals(Guid.Empty) Then
            certBO.AddressId = objAddress.Id
        End If
        objAddress.Address1 = Me.Address1
        objAddress.Address2 = Me.Address2
        objAddress.City = Me.City
        objAddress.RegionId = Me.RegionId
        objAddress.PostalCode = Me.PostalCode
        objAddress.CountryId = Me.CountryId
        If Not Me.WorkPhone Is Nothing Then certBO.WorkPhone = Me.WorkPhone
        If Not Me.MembershipNumber Is Nothing Then certBO.MembershipNumber = Me.MembershipNumber
        If Not Me.PrimaryMemberName Is Nothing Then certBO.PrimaryMemberName = Me.PrimaryMemberName
        If Not Me.WarrantySalesDate Is Nothing Then certBO.WarrantySalesDate = Me.WarrantySalesDate
        If Not Me.ProductSalesDate Is Nothing Then certBO.ProductSalesDate = Me.ProductSalesDate
        If Not Me.MembershipType Is Nothing Then certBO.MembershipTypeId = Me.MembershipTypeId(Me.MembershipType)
        If Not Me.SalesPrice Is Nothing Then certBO.SalesPrice = Me.SalesPrice
        If Not Me.VATNum Is Nothing Then certBO.VatNum = Me.VATNum
        If Not Me.IdentificationNumber Is Nothing Then certBO.IdentificationNumber = Me.IdentificationNumber

        If ((Not Me.MailingAddress1 Is Nothing) OrElse (Not Me.MailingAddress2 Is Nothing) OrElse _
            (Not Me.MailingCity Is Nothing) OrElse (Not Me.MailingRegionId.Equals(Guid.Empty)) OrElse _
             (Not Me.MailingPostalCode Is Nothing) OrElse (Not Me.MailingCountryId.Equals(Guid.Empty))) Then
            ' The user put info in one of the mailing fields
            Dim objMailingAddress As Address = certBO.MailingAddress
            If certBO.MailingAddressId.Equals(Guid.Empty) Then
                'New Address Record
                If ((Not Me.MailingAddress1 Is Nothing) AndAlso _
                (Not Me.MailingCity Is Nothing) AndAlso (Not Me.MailingRegionId.Equals(Guid.Empty)) AndAlso _
                 (Not Me.MailingPostalCode Is Nothing) AndAlso (Not Me.MailingCountryId.Equals(Guid.Empty))) Then
                    certBO.MailingAddressId = objMailingAddress.Id
                    objMailingAddress.Address1 = Me.MailingAddress1
                    objMailingAddress.Address2 = Me.MailingAddress2
                    objMailingAddress.City = Me.MailingCity
                    objMailingAddress.RegionId = Me.MailingRegionId
                    objMailingAddress.PostalCode = Me.MailingPostalCode
                    objMailingAddress.CountryId = Me.MailingCountryId
                Else
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Me.MAILING_INCOMPLETE)
                End If
            ElseIf (Not certBO.MailingAddressId.Equals(Guid.Empty)) Then
                ' Current Address Record
                If Not Me.MailingAddress1 Is Nothing Then objMailingAddress.Address1 = Me.MailingAddress1
                objMailingAddress.Address2 = Me.MailingAddress2
                If Not Me.MailingCity Is Nothing Then objMailingAddress.City = Me.MailingCity
                If Not Me.MailingRegionId.Equals(Guid.Empty) Then objMailingAddress.RegionId = Me.MailingRegionId
                If Not Me.MailingPostalCode Is Nothing Then objMailingAddress.PostalCode = Me.MailingPostalCode
                If Not Me.MailingCountryId.Equals(Guid.Empty) Then objMailingAddress.CountryId = Me.MailingCountryId
            End If
        End If

        UpdateEndorsement(IsProdSerialN, certBO, certItemBo)

    End Sub

    


#End Region


#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidUserCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_USER_COUNTRY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As OlitaUpdateConsumerInfo = CType(objectToValidate, OlitaUpdateConsumerInfo)

            Dim valid As Boolean = False

            If obj.CountryCode Is Nothing Then valid = False

            Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oCountryList As DataView
            oCountryList = LookupListNew.GetCompanyGroupCountryLookupList(CompanyGroupId)
            Dim index As Integer
            For index = 0 To oCountryList.Count - 1
                If CType(oCountryList(index)("code"), String).ToUpper.Equals(obj.CountryCode.ToUpper) Then
                    valid = True
                    Exit For
                End If
            Next

            If Not valid Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", _
                        TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.INVALID_USER_COUNTRY))
            End If
            Return valid

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidMailingUserCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_MAILING_COUNTRY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As OlitaUpdateConsumerInfo = CType(objectToValidate, OlitaUpdateConsumerInfo)

            Dim valid As Boolean = False

            If obj.MailingCountryCode Is Nothing Then valid = False

            Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oCountryList As DataView
            oCountryList = LookupListNew.GetCompanyGroupCountryLookupList(CompanyGroupId)
            Dim index As Integer
            For index = 0 To oCountryList.Count - 1
                If CType(oCountryList(index)("code"), String).ToUpper.Equals(obj.MailingCountryCode.ToUpper) Then
                    valid = True
                    Exit For
                End If
            Next

            If Not valid Then
                Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", _
                        TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.INVALID_MAILING_COUNTRY))
            End If
            Return valid

        End Function
    End Class

#End Region

End Class
