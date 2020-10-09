
Public Class GalaxyPayClaim
    Inherits BusinessObjectBase

#Region "Member Variables"
    Private dsItemCoverages As DataSet
    Private dsCoverageInfo As DataSet
    Private RegionId As Guid = Guid.Empty
    Private CountryId As Guid = Guid.Empty
    Private AccountTypeId As Guid = Guid.Empty
    Private PayeeOptionId As Guid = Guid.Empty
    Private PaymentMethodId As Guid = Guid.Empty
    Private PayeeAddress As Address
    Private PayeeBankInfo As BankInfo
    Private company As Company
    Private IsPaymentMethod_CHK_or_CTT As Boolean = False

    Private ClaimInvoiceFamilBO As ClaimInvoice
#End Region

#Region "Constants"

    Public TABLE_NAME As String = "GalaxyPayClaim"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    Private Const SOURCE_COL_PAYEE_OTHER_NAME As String = "PAYEE_OTHER_NAME"
    Private Const SOURCE_COL_ACCOUNT_TYPE As String = "ACCOUNT_TYPE"
    Private Const SOURCE_COL_SWIFT_CODE As String = "SWIFT_CODE"
    Private Const SOURCE_COL_IBAN_NUMBER As String = "IBAN_NUMBER"
    Private Const SOURCE_COL_DOCUMENT_TYPE As String = "DOCUMENT_TYPE"
    Private Const SOURCE_COL_IDENTITY_DOCUMENT_NO As String = "IDENTITY_DOCUMENT_NO"
    Private Const SOURCE_COL_PAYMENT_METHOD As String = "PAYMENT_METHOD"
    Private Const SOURCE_COL_ACCOUNT_NUMBER As String = "ACCOUNT_NUMBER"
    Private Const SOURCE_COL_BANK_ID As String = "BANK_ID"
    Private Const SOURCE_COL_ACCOUNT_NAME As String = "ACCOUNT_NAME"
    Private Const SOURCE_COL_DISBURSEMENT_COUNTRY_CODE As String = "DISBURSEMENT_COUNTRY_CODE"
    Private Const SOURCE_COL_CTC_POSTAL_CODE As String = "CTC_POSTAL_CODE"
    Private Const SOURCE_COL_CTC_REGION_CODE As String = "CTC_REGION_CODE"
    Private Const SOURCE_COL_CTC_CITY As String = "CTC_CITY"
    Private Const SOURCE_COL_CTC_ADDRESS2 As String = "CTC_ADDRESS2"
    Private Const SOURCE_COL_CTC_ADDRESS1 As String = "CTC_ADDRESS1"
    Private Const SOURCE_COL_PAYEE_CODE As String = "PAYEE_CODE"
    Private Const SOURCE_COL_AUTHORIZATION_NUMBER As String = "AUTHORIZATION_NUMBER"
    'Private Const SOURCE_COL_PAYMENT_REASON As String = "PAYMENT_REASON"
    Private Const SOURCE_COL_AMOUNT As String = "AMOUNT"
    Private Const SOURCE_COL_IVA_AMOUNT As String = "IVA_AMOUNT"
    Private Const SOURCE_COL_SVC_CONTROL_NUMBER As String = "SVC_CONTROL_NUMBER"
    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_CERTIFICATE_NUMBER As String = "CERTIFICATE_NUMBER"
    Private Const SOURCE_COL_DEALER_CODE As String = "DEALER_CODE"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_ID As String = "CERT_ITEM_COVERAGE_ID"
    Public TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"
    Public Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    Public Const SOURCE_COL_UNIT_NUMBER As String = "UNIT_NUMBER"
    Public Const SOURCE_COL_REPAIR_DATE As String = "REPAIR_DATE"
    Public Const COL_NAME_CUSTOMER_NAME As String = "CUSTOMER_NAME"
    Private Const CLAIM_NUMBER_OFFSET As Integer = 50

    'Added for Def-1782
    Public Const SOURCE_COL_INVOICE_DATE As String = "INVOICE_DATE"

#End Region

#Region "Constructors"

    Public Sub New(ds As GalaxyPayClaimDs)
        MyBase.New()

        dsCoverageInfo = New DataSet
        Dim dt As DataTable = New DataTable(TABLE_NAME_COVERAGE_INFO)
        dt.Columns.Add(SOURCE_COL_CERT_ITEM_COVERAGE_ID, GetType(Guid))
        dt.Columns.Add(SOURCE_COL_AMOUNT, GetType(Decimal))
        dt.Columns.Add(SOURCE_COL_IVA_AMOUNT, GetType(Decimal))
        dt.Columns.Add(SOURCE_COL_CLAIM_NUMBER, GetType(String))
        dt.Columns.Add(COL_NAME_CUSTOMER_NAME, GetType(String))
        dt.Columns.Add(SOURCE_COL_REPAIR_DATE, GetType(String))
        'Added for Def-1782
        dt.Columns.Add(SOURCE_COL_INVOICE_DATE, GetType(String))

        dsCoverageInfo.Tables.Add(dt)

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ds As GalaxyPayClaimDs)
        Try
            If ds.GalaxyPayClaim.Count = 0 Then Exit Sub
            With ds.GalaxyPayClaim.Item(0)
                CertificateNumber = .CERTIFICATE_NUMBER
                DealerCode = .DEALER_CODE
                UnitNumber = .UNIT_NUMBER
                ClaimNumber = .CLAIM_NUMBER
                SvcControlNumber = .SVC_CONTROL_NUMBER
                'If Not .IsPAYMENT_REASONNull Then Me.PaymentReason = .PAYMENT_REASON
                If Not .IsAUTHORIZATION_NUMBERNull Then AuthorizationNumber = .AUTHORIZATION_NUMBER

                PayeeCode = .PAYEE_CODE
                PayeeOptionId = LookupListNew.GetIdFromCode(LookupListCache.LK_PAYEE, PayeeCode)
                If PayeeOptionId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_PAYEE_OPTION_CODE)
                End If

                'Added for Def-1782
                If Not .IsINVOICE_DATENull Then
                    InvoiceDate = .INVOICE_DATE
                End If

                If Not .IsCTC_ADDRESS1Null Then CtcAddress1 = .CTC_ADDRESS1
                If Not .IsCTC_ADDRESS2Null Then CtcAddress2 = .CTC_ADDRESS2
                If Not .IsCTC_CITYNull Then CtcCity = .CTC_CITY

                If Not .IsDISBURSEMENT_COUNTRY_CODENull Then
                    DisbursementCountryCode = .DISBURSEMENT_COUNTRY_CODE
                    CountryId = GetCountryID(DisbursementCountryCode)
                    If CountryId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.BO_ERROR_COUNTRY_ID_NOT_FOUND)
                    End If
                Else
                    CountryId = GetCountryID()
                End If

                If Not .IsCTC_REGION_CODENull Then
                    CtcRegionCode = .CTC_REGION_CODE
                    RegionId = GetRegionID(CtcRegionCode, CountryId)
                    If RegionId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_REGION_CODE)
                    End If
                End If

                If Not .IsCTC_POSTAL_CODENull Then CtcPostalCode = .CTC_POSTAL_CODE

                If PayeeCode = ClaimInvoice.PAYEE_OPTION_OTHER AndAlso .PAYMENT_METHOD = "CHK" Then
                    If CtcAddress1 Is Nothing OrElse CtcCity Is Nothing OrElse CtcRegionCode Is Nothing OrElse CtcPostalCode Is Nothing Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.MISSING_ADDRESS_INFO_ERR)
                    End If
                End If

                If Not .IsACCOUNT_NAMENull Then AccountName = .ACCOUNT_NAME
                If Not .IsBANK_IDNull Then BankId = .BANK_ID
                If Not .IsACCOUNT_NUMBERNull Then AccountNumber = .ACCOUNT_NUMBER
                If Not .IsPAYMENT_METHODNull Then
                    PaymentMethod = .PAYMENT_METHOD
                    PaymentMethodId = LookupListNew.GetIdFromCode(LookupListCache.LK_PAYMENTMETHOD, PaymentMethod)

                    If (PaymentMethodId.Equals(Guid.Empty)) Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_PAYMENT_METHOD)
                    End If
                End If

                If Not .IsIDENTITY_DOCUMENT_NONull Then IdentityDocumentNo = .IDENTITY_DOCUMENT_NO
                If Not .IsDOCUMENT_TYPENull Then DocumentType = .DOCUMENT_TYPE
                If Not .IsIBAN_NUMBERNull Then IbanNumber = .IBAN_NUMBER
                If Not .IsSWIFT_CODENull Then SwiftCode = .SWIFT_CODE
                If Not .IsACCOUNT_TYPENull Then
                    AccountType = .ACCOUNT_TYPE
                    AccountTypeId = GetAccountTypeID(AccountType)
                    If AccountTypeId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANK_ACCOUNT_TYPE)
                    End If
                End If

                If Not .IsPAYEE_OTHER_NAMENull Then PayeeOtherName = .PAYEE_OTHER_NAME

                dsItemCoverages = CertItemCoverage.LoadAllItemCoveragesForGalaxyClaimUpdate(ClaimNumber)
                If dsItemCoverages Is Nothing OrElse dsItemCoverages.Tables.Count <= 0 OrElse dsItemCoverages.Tables(0).Rows.Count <= 0 Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                End If

                Dim claimTmpBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
                Dim i As Integer
                For i = 0 To ds.COVERAGES.Count - 1
                    Dim coverageCode As String = ds.COVERAGES(i).CERT_ITEM_COVERAGE_CODE
                    Dim amount As Decimal
                    Dim ivaAmount As Decimal
                    Dim deductible As Decimal
                    Dim IsDeductibleNull As Boolean = True

                    If CType(ds.COVERAGES(i).AMOUNT, DecimalType) IsNot Nothing Then
                        amount = ds.COVERAGES(i).AMOUNT
                    End If

                    If Not ds.COVERAGES(i).IsIVA_AMOUNTNull Then
                        ivaAmount = ds.COVERAGES(i).IVA_AMOUNT
                    End If

                    Dim certItemCoverageId As Guid = Guid.Empty
                    Dim companyId As Guid = Guid.Empty
                    Dim customerName As String

                    If dsItemCoverages IsNot Nothing AndAlso dsItemCoverages.Tables.Count > 0 AndAlso dsItemCoverages.Tables(0).Rows.Count > 0 Then
                        Dim dr() As DataRow = dsItemCoverages.Tables(0).Select(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_CODE & "='" & coverageCode & "'")
                        If dr IsNot Nothing AndAlso dr.Length > 0 Then
                            certItemCoverageId = New Guid(CType(dr(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
                            customerName = CType(dr(0)(CertItemCoverageDAL.COL_NAME_CUSTOMER_NAME), String)
                            companyId = New Guid(CType(dr(0)(CertItemCoverageDAL.COL_NAME_COMPANY_ID), Byte()))
                            If certItemCoverageId.Equals(Guid.Empty) Then
                                Throw New BOValidationException("GalaxyPayClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                            End If
                        Else
                            Throw New BOValidationException("GalaxyPayClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                        End If
                    Else
                        Throw New BOValidationException("GalaxyPayClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                    End If

                    Dim newRow As DataRow = dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).NewRow()
                    newRow(SOURCE_COL_CERT_ITEM_COVERAGE_ID) = certItemCoverageId
                    newRow(SOURCE_COL_AMOUNT) = amount
                    newRow(SOURCE_COL_IVA_AMOUNT) = ivaAmount
                    newRow(SOURCE_COL_CLAIM_NUMBER) = claimTmpBO.GetExistClaimNumber(companyId, .CLAIM_NUMBER, coverageCode, UnitNumber, True)
                    newRow(COL_NAME_CUSTOMER_NAME) = customerName
                    newRow(SOURCE_COL_REPAIR_DATE) = ds.COVERAGES(i).REPAIR_DATE

                    dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows.Add(newRow)

                Next

            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetClaimInvoiceBO(isFirstBO As Boolean) As ClaimInvoice
        Dim ClaimInvoiceBO As ClaimInvoice

        If isFirstBO Then
            ClaimInvoiceFamilBO = New ClaimInvoice
            Return ClaimInvoiceFamilBO
        Else
            ClaimInvoiceBO = ClaimInvoiceFamilBO.AddClaimInvoice(Guid.Empty)
            Return ClaimInvoiceBO
        End If

    End Function

    Public Overrides Function ProcessWSRequest() As String
        Dim row As DataRow

        Try
            Validate()

            Dim tempBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            Dim isFirstBO As Boolean = True

            For Each row In dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows
                Dim claimID As Guid = tempBO.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, CType(row(SOURCE_COL_CLAIM_NUMBER), String))

                If (claimID.Equals(Guid.Empty)) Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                End If

                Dim ClaimInvoiceBO As ClaimInvoice
                ClaimInvoiceBO = GetClaimInvoiceBO(isFirstBO)
                Dim DisbursementBO As Disbursement = ClaimInvoiceBO.AddNewDisbursement()
                'isFirstBO = False

                Dim claimBO As Claim = ClaimInvoiceBO.AddClaim(claimID)

                If company Is Nothing Then
                    company = New Company(claimBO.CompanyId)
                End If

                If CountryId.Equals(Guid.Empty) Then
                    CountryId = company.CountryId
                End If

                ClaimInvoiceBO.PayeeOptionCode = PayeeCode
                ClaimInvoiceBO.PrepopulateClaimInvoice(claimBO)
                ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = False
                ClaimInvoiceBO.BatchNumber = "1"
                ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                ClaimInvoiceBO.PartTax = New DecimalType(0D)
                ClaimInvoiceBO.RecordCount = New LongType(1)
                ClaimInvoiceBO.Source = Nothing
                ClaimInvoiceBO.CompanyId = company.Id
                ClaimInvoiceBO.RepairDate = CType(row(SOURCE_COL_REPAIR_DATE), DateTime)

                'Added for Def-1782
                ClaimInvoiceBO.InvoiceDate = InvoiceDate

                If ClaimInvoiceBO.PickUpDate Is Nothing Then
                    ClaimInvoiceBO.PickUpDate = ClaimInvoiceBO.RepairDate
                End If

                ClaimInvoiceBO.AuthorizationNumber = AuthorizationNumber
                ClaimInvoiceBO.Amount = New DecimalType(row(SOURCE_COL_AMOUNT))

                If Not (CType(UnitNumber, Long) > 1) Then
                    If claimBO.AssurantPays.Value <> ClaimInvoiceBO.Amount.Value Then
                        Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                    End If
                End If

                ClaimInvoiceBO.IvaAmount = CType(row(SOURCE_COL_IVA_AMOUNT), Decimal)
                ClaimInvoiceBO.SvcControlNumber = SvcControlNumber

                ' payee info
                ClaimInvoiceBO.PaymentMethodCode = PaymentMethod
                ClaimInvoiceBO.PaymentMethodID = PaymentMethodId

                Select Case PayeeCode
                    Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                        SetMasterCenterInfo(ClaimInvoiceBO, DisbursementBO)
                    Case ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
                        SetServiceCenterInfo(ClaimInvoiceBO, DisbursementBO)
                    Case ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                        SetLoanerCenterInfo(ClaimInvoiceBO, DisbursementBO)
                    Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                        SetCustomerInfo(ClaimInvoiceBO, DisbursementBO)
                    Case ClaimInvoice.PAYEE_OPTION_OTHER
                        SetOtherInfo(ClaimInvoiceBO, DisbursementBO)
                End Select

                ClaimInvoiceBO.Validate()

                If ClaimInvoiceBO.PayeeBankInfo IsNot Nothing Then
                    ValidateBankUserControl(ClaimInvoiceBO.PayeeBankInfo)
                End If

                ClaimInvoiceFamilBO.Save()
            Next



            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub SetMasterCenterInfo(ClaimInvoiceBO As ClaimInvoice, DisbursementBO As Disbursement)
        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(ClaimInvoiceBO.Invoiceable.ServiceCenterId)
        If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
            Dim masterServiceCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
            PayeeOtherName = masterServiceCenter.Description
            If ClaimInvoiceBO.IsInsuranceCompany Then
                ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeCode, masterServiceCenter, ClaimInvoiceBO, DisbursementBO, Nothing, Nothing, Nothing)
            End If
            If Not masterServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                ClaimInvoiceBO.PayeeAddress = Nothing
                PayeeAddress = Nothing
                PayeeBankInfo = ClaimInvoiceBO.Add_BankInfo(masterServiceCenter.BankInfoId)
                PayeeBankInfo.SourceCountryID = CountryId
                ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo
            Else
                ClaimInvoiceBO.PayeeBankInfo = Nothing
                PayeeBankInfo = Nothing
                PayeeAddress = ClaimInvoiceBO.Add_Address(masterServiceCenter.AddressId)
                ClaimInvoiceBO.PayeeAddress = PayeeAddress
            End If
        Else
            Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_MASTER_CENTER_NOT_FOUND)
        End If
    End Sub

    Private Sub SetServiceCenterInfo(ClaimInvoiceBO As ClaimInvoice, DisbursementBO As Disbursement)

        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(ClaimInvoiceBO.Invoiceable.ServiceCenterId)
        PayeeOtherName = claimServiceCenter.Description
        If ClaimInvoiceBO.IsInsuranceCompany Then
            ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeCode, claimServiceCenter, ClaimInvoiceBO, DisbursementBO, Nothing, Nothing, Nothing)
        End If
        If Not claimServiceCenter.BankInfoId.Equals(Guid.Empty) Then
            PayeeAddress = Nothing
            PayeeBankInfo = ClaimInvoiceBO.Add_BankInfo(claimServiceCenter.BankInfoId)
            PayeeBankInfo.SourceCountryID = CountryId
            ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo
            ClaimInvoiceBO.PayeeAddress = PayeeAddress
        Else
            PayeeBankInfo = Nothing
            PayeeAddress = ClaimInvoiceBO.Add_Address(claimServiceCenter.AddressId)
            ClaimInvoiceBO.PayeeAddress = PayeeAddress
            ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo
        End If

    End Sub

    Private Sub SetLoanerCenterInfo(ClaimInvoiceBO As ClaimInvoice, DisbursementBO As Disbursement)
        If Not ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
            Dim loanerServiceCenter As ServiceCenter = New ServiceCenter(ClaimInvoiceBO.Invoiceable.LoanerCenterId)
            PayeeOtherName = loanerServiceCenter.Description
            If ClaimInvoiceBO.IsInsuranceCompany Then
                ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeCode, loanerServiceCenter, ClaimInvoiceBO, DisbursementBO, Nothing, Nothing, Nothing)
            End If
            If Not loanerServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                PayeeAddress = Nothing
                PayeeBankInfo = ClaimInvoiceBO.Add_BankInfo(loanerServiceCenter.BankInfoId)
                PayeeBankInfo.SourceCountryID = CountryId
                ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo
                ClaimInvoiceBO.PayeeAddress = PayeeAddress
            Else
                PayeeBankInfo = Nothing
                PayeeAddress = ClaimInvoiceBO.Add_Address(loanerServiceCenter.AddressId)
                ClaimInvoiceBO.PayeeAddress = PayeeAddress
                ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo
            End If
        Else
            Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_LOANER_CENTER_NOT_FOUND)
        End If

    End Sub

    Private Sub SetCustomerInfo(ClaimInvoiceBO As ClaimInvoice, DisbursementBO As Disbursement)
        PayeeOtherName = ClaimInvoiceBO.CustomerName
        If ClaimInvoiceBO.IsInsuranceCompany Then
            Dim objCertificate As Certificate = New Certificate(ClaimInvoiceBO.Invoiceable.CertificateId)
            DocumentType = LookupListNew.GetCodeFromId(LookupListCache.LK_DOCUMENT_TYPES, objCertificate.DocumentTypeID)
            IdentityDocumentNo = objCertificate.IdentificationNumber
            ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeCode, Nothing, ClaimInvoiceBO, DisbursementBO, objCertificate, Nothing, Nothing)
        End If

        PayeeAddress = ClaimInvoiceBO.Add_Address(ClaimInvoiceBO.CustomerAddressID)
        ClaimInvoiceBO.PayeeAddress = PayeeAddress

        SetInfoBasedOnPaymentMethod(ClaimInvoiceBO)
    End Sub

    Private Sub SetOtherInfo(ClaimInvoiceBO As ClaimInvoice, DisbursementBO As Disbursement)

        If PaymentMethod = "CHK" Then
            PayeeAddress = ClaimInvoiceBO.AddressChild()
            PayeeAddress.Address1 = CtcAddress1
            PayeeAddress.Address2 = CtcAddress2
            PayeeAddress.City = CtcCity
            PayeeAddress.PostalCode = CtcPostalCode
            PayeeAddress.RegionId = RegionId
            PayeeAddress.CountryId = CountryId
        End If

        If ClaimInvoiceBO.IsInsuranceCompany Then
            ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeCode, Nothing, ClaimInvoiceBO, DisbursementBO, Nothing, DocumentType, IdentityDocumentNo)
        End If

        SetInfoBasedOnPaymentMethod(ClaimInvoiceBO)
    End Sub

    Private Sub SetInfoBasedOnPaymentMethod(ClaimInvoiceBO As ClaimInvoice)
        Select Case PaymentMethod
            Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                If PayeeBankInfo Is Nothing Then
                    PayeeBankInfo = ClaimInvoiceBO.Add_BankInfo()
                    PayeeBankInfo.Account_Name = AccountName
                    PayeeBankInfo.Account_Number = AccountNumber
                    PayeeBankInfo.Bank_Id = BankId
                    PayeeBankInfo.SwiftCode = SwiftCode
                    PayeeBankInfo.IbanNumber = IbanNumber
                    PayeeBankInfo.AccountTypeId = AccountTypeId
                    PayeeBankInfo.CountryID = CountryId
                    PayeeBankInfo.SourceCountryID = company.CountryId
                End If
                ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo

                If Not ClaimInvoiceBO.PayeeBankInfo.SourceCountryID.Equals(Guid.Empty) Then
                    If ClaimInvoiceBO.PayeeBankInfo.SourceCountryID.Equals(CountryId) Then
                        'Domestic transfer
                        ClaimInvoiceBO.PayeeBankInfo.SwiftCode = String.Empty
                        ClaimInvoiceBO.PayeeBankInfo.IbanNumber = String.Empty

                        ClaimInvoiceBO.PayeeBankInfo.DomesticTransfer = True
                        ClaimInvoiceBO.PayeeBankInfo.InternationalEUTransfer = False
                        ClaimInvoiceBO.PayeeBankInfo.InternationalTransfer = False
                    Else
                        Dim objCountry As New Country(CountryId)
                        If LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                            'International transfer & Destination is European country
                            ClaimInvoiceBO.PayeeBankInfo.Bank_Id = Nothing
                            ClaimInvoiceBO.PayeeBankInfo.Account_Number = Nothing

                            ClaimInvoiceBO.PayeeBankInfo.DomesticTransfer = False
                            ClaimInvoiceBO.PayeeBankInfo.InternationalEUTransfer = True
                            ClaimInvoiceBO.PayeeBankInfo.InternationalTransfer = False

                        Else
                            'International transfer & Destination is not a European country
                            ClaimInvoiceBO.PayeeBankInfo.IbanNumber = String.Empty

                            ClaimInvoiceBO.PayeeBankInfo.DomesticTransfer = False
                            ClaimInvoiceBO.PayeeBankInfo.InternationalEUTransfer = False
                            ClaimInvoiceBO.PayeeBankInfo.InternationalTransfer = True

                        End If

                    End If
                Else
                    'Domestic transfer
                    ClaimInvoiceBO.PayeeBankInfo.SwiftCode = String.Empty
                    ClaimInvoiceBO.PayeeBankInfo.IbanNumber = String.Empty

                    ClaimInvoiceBO.PayeeBankInfo.DomesticTransfer = True
                    ClaimInvoiceBO.PayeeBankInfo.InternationalEUTransfer = False
                    ClaimInvoiceBO.PayeeBankInfo.InternationalTransfer = False
                End If

            Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                ClaimInvoiceBO.PayeeBankInfo = Nothing
                ClaimInvoiceBO.PayeeAddress = Nothing

            Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                'Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)
                If PayeeCode = ClaimInvoice.PAYEE_OPTION_CUSTOMER Then

                    If (CtcAddress1 IsNot Nothing) AndAlso (CtcCity IsNot Nothing) AndAlso (CtcPostalCode IsNot Nothing) AndAlso (Not RegionId.Equals(Guid.Empty)) Then
                        PayeeAddress = ClaimInvoiceBO.Add_Address()
                        PayeeAddress.Address1 = CtcAddress1
                        PayeeAddress.Address2 = CtcAddress2
                        PayeeAddress.City = CtcCity
                        PayeeAddress.PostalCode = CtcPostalCode
                        PayeeAddress.RegionId = RegionId
                        PayeeAddress.CountryId = CountryId
                    ElseIf PayeeAddress Is Nothing Then
                        PayeeAddress = ClaimInvoiceBO.Add_Address(ClaimInvoiceBO.CustomerAddressID)
                    End If
                    ClaimInvoiceBO.PayeeAddress = PayeeAddress

                ElseIf PayeeCode = ClaimInvoice.PAYEE_OPTION_OTHER Then

                    If PayeeAddress Is Nothing Then
                        PayeeAddress = ClaimInvoiceBO.Add_Address()
                        PayeeAddress.Address1 = CtcAddress1
                        PayeeAddress.Address2 = CtcAddress2
                        PayeeAddress.City = CtcCity
                        PayeeAddress.PostalCode = CtcPostalCode
                        PayeeAddress.RegionId = RegionId
                        PayeeAddress.CountryId = CountryId
                    End If
                    ClaimInvoiceBO.PayeeAddress = PayeeAddress

                End If

        End Select

    End Sub

    Private Sub ValidateBankUserControl(payeeBankInfo As BankInfo)
        If payeeBankInfo IsNot Nothing Then
            If payeeBankInfo.DomesticTransfer = True Then
                If payeeBankInfo.Account_Name Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If payeeBankInfo.Bank_Id Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKID_REQD)
                End If

                If payeeBankInfo.Account_Number Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                End If
            End If

            If payeeBankInfo.InternationalEUTransfer = True Then
                If payeeBankInfo.Account_Name Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If payeeBankInfo.SwiftCode Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If

                If payeeBankInfo.IbanNumber Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
                End If
            End If

            If payeeBankInfo.InternationalTransfer = True Then
                If payeeBankInfo.Account_Name Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If payeeBankInfo.Bank_Id Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKID_REQD)
                End If

                If payeeBankInfo.Account_Number Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                End If

                If payeeBankInfo.SwiftCode Is Nothing Then
                    Throw New BOValidationException("GalaxyPayClaim Error: ", Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If
            End If

        End If
    End Sub

    Private Sub MapDataSet(ds As GalaxyPayClaimDs)

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

    Private Sub Load(ds As GalaxyPayClaimDs)
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
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

    Public Function GetCountryID(Optional ByVal countryCode As String = "") As Guid
        Dim countryID As Guid = Guid.Empty
        ' Ticket# 1527761: Galaxy web service:  Default to country of payee when payment is by check
        If countryCode Is Nothing OrElse countryCode.Equals(String.Empty) Then
            Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            countryID = oCountry.Id
        Else
            Dim list As DataView = LookupListNew.GetCountryLookupList()
            countryID = LookupListNew.GetIdFromCode(list, countryCode)
        End If

        Return countryID
    End Function

    Public Function GetRegionID(regionCode As String, countryID As Guid) As Guid
        Dim regionID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetRegionLookupList(countryID)

        regionID = LookupListNew.GetIdFromCode(list, regionCode)

        Return regionID
    End Function

    Public Function GetRegionCode(regionId As Guid, countryID As Guid) As String
        Dim regionCode As String
        Dim list As DataView = LookupListNew.GetRegionLookupList(countryID)

        regionCode = LookupListNew.GetCodeFromId(list, regionId)

        Return regionCode
    End Function

    Public Function GetAccountTypeID(AccountType As String) As Guid
        Dim accountTypeID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetAccountTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        accountTypeID = LookupListNew.GetIdFromCode(list, AccountType)

        Return accountTypeID
    End Function

#End Region


#Region "Properties"


    Public Property CertificateNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    Public Property DealerCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_DEALER_CODE, Value)
        End Set
    End Property

    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property SvcControlNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property

    'Public Property PaymentReason() As String
    '    Get
    '        CheckDeleted()
    '        If Row(SOURCE_COL_PAYMENT_REASON) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(SOURCE_COL_PAYMENT_REASON), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(SOURCE_COL_PAYMENT_REASON, Value)
    '    End Set
    'End Property

    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    Public Property PayeeCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PAYEE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PAYEE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PAYEE_CODE, Value)
        End Set
    End Property

    Public Property CtcAddress1 As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CTC_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CTC_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CTC_ADDRESS1, Value)
        End Set
    End Property

    Public Property CtcAddress2 As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CTC_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CTC_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CTC_ADDRESS2, Value)
        End Set
    End Property

    Public Property CtcCity As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CTC_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CTC_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CTC_CITY, Value)
        End Set
    End Property

    Public Property CtcRegionCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CTC_REGION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CTC_REGION_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CTC_REGION_CODE, Value)
        End Set
    End Property

    Public Property CtcPostalCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CTC_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CTC_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CTC_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property DisbursementCountryCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DISBURSEMENT_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DISBURSEMENT_COUNTRY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_DISBURSEMENT_COUNTRY_CODE, Value)
        End Set
    End Property

    Public Property AccountName As String
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

    Public Property BankId As String
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

    Public Property AccountNumber As String
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

    Public Property IdentityDocumentNo As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_IDENTITY_DOCUMENT_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_IDENTITY_DOCUMENT_NO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_IDENTITY_DOCUMENT_NO, Value)
        End Set
    End Property

    Public Property DocumentType As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DOCUMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_DOCUMENT_TYPE, Value)
        End Set
    End Property

    Public Property IbanNumber As String
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

    Public Property SwiftCode As String
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

    Public Property AccountType As String
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

    Public Property PayeeOtherName As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PAYEE_OTHER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PAYEE_OTHER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PAYEE_OTHER_NAME, Value)
        End Set
    End Property

    Public Property UnitNumber As LongType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_UNIT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(SOURCE_COL_UNIT_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_UNIT_NUMBER, Value)
        End Set
    End Property

    'Added for Def-1782
    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_INVOICE_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_INVOICE_DATE, Value)
        End Set
    End Property

#End Region

End Class


