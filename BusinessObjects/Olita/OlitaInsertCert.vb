Imports System.Linq
Imports System.Collections.Generic

Public Class OlitaInsertCert
    Inherits BusinessObjectBase

#Region "Member Variables"

    Private CompanyId As Guid = Guid.Empty
    Private DealerId As Guid = Guid.Empty


#End Region

#Region "Constants"

    Public TABLE_NAME As String = "OlitaInsertCert"
    Public TABLE_NAME_COVERAGE As String = "COVERAGES"
    Public TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"
    Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private _certAddController As CertAddController
    Private _AdditionalCertNums As List(Of String)
    Private _AdditionalCertQuantity As Integer = 0
    Private Const DATASET_NAME As String = "OlitaInsertCert"
    Private Const DATASET_TABLE_NAME As String = "Certificate"
    
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As OlitaInsertCertDs)
        MyBase.New()
        _certAddController = New CertAddController
        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region


#Region "Member Methods"

    Public Function GetDealerID(ByVal originalDealerCode As String) As Guid
        Dim dealerId As Guid = Guid.Empty
        Dim index As Integer
        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
        list.Sort = "CODE"
        index = list.Find(originalDealerCode)
        If index = -1 Then
            Throw New BOValidationException("OlitaInsertCert Error: ", "COMPANY_NOT_ASSIGNED_TO_USER")
        End If

        dealerId = LookupListNew.GetIdFromCode(list, originalDealerCode)

        Return dealerId
    End Function
    Public Function GetSalutationDesc(ByVal originalSalutationCode As String) As String
        Dim salutationDesc As String
        Dim list As DataView = LookupListNew.GetSalutationLookupList (ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        salutationDesc = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_SALUTATION, originalSalutationCode)

        Return salutationDesc
    End Function

    Private Sub PopulateBOFromWebService(ByVal ds As OlitaInsertCertDs)
        Try
            If ds.OlitaInsertCert.Count = 0 Then Exit Sub
            With ds.OlitaInsertCert.Item(0)

                ''''''''''''''''''''''''''' Need to find a way to get the dealer id
                'Me.MyController.RecordType = .RECORD_TYPE
                If Not .IsRECORD_TYPENull Then
                    Me.MyController.RecordType = .RECORD_TYPE
                Else
                    Me.MyController.RecordType = "FC"
                End If

                Me.MyController.DealerID = Me.GetDealerID(.DEALER_CODE) ', .COMPANY_CODE)
                Me.MyController.CertNum = .CERT_NUMBER
                Me.MyController.ProductCode = .PRODUCT_CODE
                Me.MyController.CertDuration = .CERT_DURATION
                Me.MyController.ManufacturerDuration = .MANUFACTURER_DURATION
                Me.MyController.WarrantySalesDate = .WARRANTY_SALES_DATE
                Me.MyController.ProductSalesDate = .PRODUCT_SALES_DATE
                Me.MyController.WarrantyPrice = .WARRANTY_PRICE
                Me.MyController.ProductRetailPrice = .PRODUCT_RETAIL_PRICE
                If Not .IsINVOICE_NUMBERNull Then Me.MyController.InvoiceNumber = .INVOICE_NUMBER
                If Not .IsBRANCH_CODENull Then Me.MyController.BranchCode = .BRANCH_CODE
                If Not .IsSALES_REP_NUMBERNull Then Me.MyController.SalesRepNumber = .SALES_REP_NUMBER
                If Not .IsMAKENull Then Me.MyController.Make = .MAKE
                If Not .IsMODELNull Then Me.MyController.Model = .MODEL
                If Not .IsSERIAL_NUMBERNull Then Me.MyController.SerialNumber = .SERIAL_NUMBER
                If Not .IsITEM_CODENull Then Me.MyController.ItemCode = .ITEM_CODE
                If Not .IsITEM_DESCRIPTIONNull Then Me.MyController.ItemDescription = .ITEM_DESCRIPTION
                If Not .IsSALUTATIONNull Then Me.MyController.Salutation = GetSalutationDesc(.SALUTATION)
                If Not .IsCUSTOMER_NAMENull Then Me.MyController.CustomerName = .CUSTOMER_NAME
                If Not .IsCUSTOMER_TAX_IDNull Then Me.MyController.CustomerTaxID = .CUSTOMER_TAX_ID
                If Not .IsCUSTOMER_HOME_PHONENull Then Me.MyController.CustomerHomePhone = .CUSTOMER_HOME_PHONE
                If Not .IsCUSTOMER_WORK_PHONENull Then Me.MyController.CustomerWorkPhone = .CUSTOMER_WORK_PHONE
                If Not .IsCUSTOMER_EMAILNull Then Me.MyController.CustomerEmail = .CUSTOMER_EMAIL
                If Not .IsCUSTOMER_ADDRESS1Null Then Me.MyController.CustomerAddress1 = .CUSTOMER_ADDRESS1
                If Not .IsCUSTOMER_ADDRESS2Null Then Me.MyController.CustomerAddress2 = .CUSTOMER_ADDRESS2
                If Not .IsCUSTOMER_CITYNull Then Me.MyController.CustomerCity = .CUSTOMER_CITY
                If Not .IsCUSTOMER_STATENull Then Me.MyController.CustomerState = .CUSTOMER_STATE
                If Not .IsCUSTOMER_ZIPNull Then Me.MyController.CustomerZIP = .CUSTOMER_ZIP
                If Not .IsCUSTOMER_COUNTRY_ISO_CODENull Then Me.MyController.CustomerCountryISOCode = .CUSTOMER_COUNTRY_ISO_CODE
                If Not .IsPURCHASE_COUNTRY_ISO_CODENull Then Me.MyController.PurchaseCountryISOCode = .PURCHASE_COUNTRY_ISO_CODE
                If Not .IsCURRENCY_ISO_CODENull Then Me.MyController.CurrencyISOCode = .CURRENCY_ISO_CODE


                ' Populate BUNDLED_ITEMS
                Dim i As Integer
                For i = 0 To ds.BUNDLED_ITEMS.Count - 1
                    Dim bundledItem As New CertAddController.BundledItem
                    If Not ds.BUNDLED_ITEMS(i).IsMAKENull Then bundledItem.Make = ds.BUNDLED_ITEMS(i).MAKE
                    If Not ds.BUNDLED_ITEMS(i).IsMODELNull Then bundledItem.Model = ds.BUNDLED_ITEMS(i).MODEL
                    If Not ds.BUNDLED_ITEMS(i).IsSERIAL_NUMBERNull Then bundledItem.SerialNumber = ds.BUNDLED_ITEMS(i).SERIAL_NUMBER
                    If Not ds.BUNDLED_ITEMS(i).IsDESCRIPTIONNull Then bundledItem.Description = ds.BUNDLED_ITEMS(i).DESCRIPTION
                    If Not ds.BUNDLED_ITEMS(i).IsPRICENull Then bundledItem.Price = ds.BUNDLED_ITEMS(i).PRICE
                    If Not ds.BUNDLED_ITEMS(i).IsMFG_WARRANTYNull Then bundledItem.MfgWarranty = ds.BUNDLED_ITEMS(i).MFG_WARRANTY
                    If Not ds.BUNDLED_ITEMS(i).IsPRODUCT_CODENull Then bundledItem.ProductCode = ds.BUNDLED_ITEMS(i).PRODUCT_CODE
                    Dim Errmsg As String = String.Empty
                    If Not Me.MyController.AddBundledItem(bundledItem, Errmsg) Then Throw New BOValidationException("OlitaInsertCert Error: ", Errmsg)
                Next

                'REQ-201
                If Not .IsPAYMENT_TYPENull Then Me.MyController.PaymentType = .PAYMENT_TYPE
                If Not .IsBILLING_FREQUENCYNull Then Me.MyController.BillingFrequency = .BILLING_FREQUENCY
                If Not .IsNUMBER_OF_INSTALLMENTSNull Then Me.MyController.NumOfInstallments = .NUMBER_OF_INSTALLMENTS
                If Not .IsINSTALLMENT_AMOUNTNull Then Me.MyController.InstallmentAmount = .INSTALLMENT_AMOUNT
                If Not .IsBANK_RTN_NUMBERNull Then Me.MyController.BankRoutingNumber = .BANK_RTN_NUMBER
                If Not .IsBANK_ACCOUNT_NUMBERNull Then Me.MyController.BankAcctNumber = .BANK_ACCOUNT_NUMBER
                If Not .IsBANK_ACCT_OWNER_NAMENull Then Me.MyController.BankAcctOwnerName = .BANK_ACCT_OWNER_NAME

                'REQ-713
                If Not .IsMEMBERSHIP_NUMBERNull Then Me.MyController.MembershipNum = .MEMBERSHIP_NUMBER

                If Not .IsADDITIONAL_CERT_QUANTITYNull Then AdditionalCertQuantity = .ADDITIONAL_CERT_QUANTITY


                For i = 0 To ds.ADDITIONAL_CERT_NUMBER.Count - 1
                    AdditionalCertNums.Add(ds.ADDITIONAL_CERT_NUMBER(i).ADDITIONAL_CERT_NUMBER_Column.Trim.ToUpper)
                Next

                If Not .IsSKUNull Then Me.MyController.SkuNumber = .SKU
                If Not .IsSUBSCRIBER_STATUSNull Then Me.MyController.SubscriberStatus = .SUBSCRIBER_STATUS
                If Not .IsPOST_PRE_PAIDNull Then Me.MyController.PostPrePaid = .POST_PRE_PAID
                If Not .IsBILLING_PLANNull Then Me.MyController.BillingPlan = .BILLING_PLAN
                If Not .IsBILLING_CYCLENull Then Me.MyController.BillingCycle = .BILLING_CYCLE
                If Not .IsMEMBERSHIP_TYPENull Then Me.MyController.MembershipType = .MEMBERSHIP_TYPE
                If Not .IsMARKETING_PROMO_SERNull Then Me.MyController.MarketingPromoSer = .MARKETING_PROMO_SER
                If Not .IsMARKETING_PROMO_NUMNull Then Me.MyController.MarketingPromoNum = .MARKETING_PROMO_NUM
                If Not .IsSALES_CHANNELNull() Then Me.MyController.SalesChannel = .SALES_CHANNEL
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProcessAdditionalCertificates()
        Dim strCertNum As String
        Dim ErrMsg As New Collections.Generic.List(Of String)
        Dim CertID As Guid = Guid.Empty

        For Each strCertNum In AdditionalCertNums
            MyController.CertNum = strCertNum
            MyController.KeepEnrollmentFileWhenErr = True
            'Me.MyController.Save(ErrMsg, CertID)
            Me.MyController.OlitaSave(ErrMsg, CertID)
        Next
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Try
            If Me.AdditionalCertQuantity <> Me.AdditionalCertNums.Count Then
                Throw New BOValidationException("OlitaInsertCert Error: Additional Cert Quantity Not Match additional Cert Num Count", "INVALID_ADDITIONAL_CERT_DATA")
                'Throw New BOValidationException("OlitaInsertCert Enrollment Error: ", Common.ErrorCodes.INVALID_DEALER_CODE)
            End If

            Dim DupCertNums = From str In Me.AdditionalCertNums _
                              Group str By str Into Count() Select str, DupCount = Count
            If DupCertNums.Count <> AdditionalCertNums.Count Then 'Duplicated cert num in the list
                Throw New BOValidationException("OlitaInsertCert Error: Duplicated Additional Cert Num", "INVALID_ADDITIONAL_CERT_DATA")
            End If

            Dim CertNumUsed = From str In Me.AdditionalCertNums Where str = MyController.CertNum.Trim.ToUpper Select str
            If CertNumUsed.Count > 0 Then 'The primary cert number duplicated in additional cert number list
                Throw New BOValidationException("OlitaInsertCert Error: Duplicated Additional Cert Num", "INVALID_ADDITIONAL_CERT_DATA")
            End If

            Dim ErrMsg As New Collections.Generic.List(Of String)
            Dim CertID As Guid = Guid.Empty

            MyController.KeepEnrollmentFileWhenErr = False
            'If Me.MyController.Save(ErrMsg, CertID) Then
            If Me.MyController.OlitaSave(ErrMsg, CertID) Then
                ' SAVE OK
                ' Process additional Certificates with separate thread so that response will be sent back right away.
                Dim t As System.Threading.Thread = New System.Threading.Thread(AddressOf Me.ProcessAdditionalCertificates)
                t.Start()

                Dim cert As New Certificate
                Dim _CertDataSet As DataSet = cert.GetCertNumFromCertId(CertID)
                Dim isAutoGenFlagOn As Boolean = Dealer.GetCertAutoGenFlag(Me.MyController.DealerID)

                If Not isAutoGenFlagOn Then
                    Return XMLHelper.GetXML_OK_Response
                Else
                    _CertDataSet.DataSetName = Me.DATASET_NAME
                    _CertDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = Me.DATASET_TABLE_NAME
                    Return (XMLHelper.FromDatasetToXML(_CertDataSet, Nothing, False, True, Me.DATASET_NAME, False, False)).Replace(Me.DATASET_TABLE_NAME, TABLE_RESULT)
                End If
                
            Else
                'SAVE NOT OK
                'Need to return all error collection
                Throw New BOValidationException("OlitaInsertCert Error: ", ErrMsg.Item(0))
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub MapDataSet(ByVal ds As OlitaInsertCertDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ByVal ds As OlitaInsertCertDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            'Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region


#Region "Properties"

    Public ReadOnly Property MyController() As CertAddController
        Get
            If Me._certAddController Is Nothing Then
                _certAddController = New CertAddController
            End If
            Return _certAddController
        End Get
       
    End Property

    Public ReadOnly Property AdditionalCertNums() As List(Of String)
        Get
            If Me._AdditionalCertNums Is Nothing Then
                _AdditionalCertNums = New List(Of String)
            End If
            Return _AdditionalCertNums
        End Get
    End Property

    Public Property AdditionalCertQuantity() As Integer
        Get
            Return _AdditionalCertQuantity
        End Get
        Set(ByVal value As Integer)
            _AdditionalCertQuantity = value
        End Set
    End Property
#End Region

End Class

