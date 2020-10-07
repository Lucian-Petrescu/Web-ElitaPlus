Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew
Imports CERTBO = Assurant.ElitaPlus.BusinessObjectsNew.Certificate

Namespace SpecializedServices.GW
    Public Class GwPilService
        Implements IGwPilService

        Private Property CertificateManager As ICertificateManager
        Private Property CommonManager As ICommonManager
        Private Property DealerManager As IDealerManager
        Private Property CountryManager As ICountryManager
        Private Property AddressManager As IAddressManager
        Private Property CompanyGroupManager As ICompanyGroupManager
        Private Property EquipmentManager As IEquipmentManager
        Private Property CurrencyManager As ICurrencyManager
        Private Property CompanyManager As ICompanyManager
        Private Property ClaimManager As IClaimManager

        Public Sub New(pCertificateManager As ICertificateManager,
                       pCommonManager As ICommonManager,
                       pDealerManager As IDealerManager,
                       pCountryManager As ICountryManager,
                       pAddressManager As IAddressManager,
                       pCompGroupManager As ICompanyGroupManager,
                       pEquipmentManager As IEquipmentManager,
                       pCurrencyManager As ICurrencyManager,
                       pCompanyManager As ICompanyManager,
                       pClaimManager As IClaimManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If

            If (pCommonManager Is Nothing) Then
                Throw New ArgumentNullException("pCommonManager")
            End If

            If (pDealerManager Is Nothing) Then
                Throw New ArgumentNullException("pDealerManager")
            End If

            If (pCountryManager Is Nothing) Then
                Throw New ArgumentNullException("pCountryManager")
            End If

            If (pAddressManager Is Nothing) Then
                Throw New ArgumentNullException("pAddressManager")
            End If

            If (pCompGroupManager Is Nothing) Then
                Throw New ArgumentNullException("pCompGroupManager")
            End If

            If (pEquipmentManager Is Nothing) Then
                Throw New ArgumentNullException("pEquipmentManager")
            End If

            If (pCurrencyManager Is Nothing) Then
                Throw New ArgumentNullException("pCurrencyManager")
            End If

            If (pCompanyManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyManager")
            End If

            If (pClaimManager Is Nothing) Then
                Throw New ArgumentNullException("pClaimManager")
            End If

            CertificateManager = pCertificateManager
            CommonManager = pCommonManager
            DealerManager = pDealerManager
            CountryManager = pCountryManager
            AddressManager = pAddressManager
            CompanyGroupManager = pCompGroupManager
            EquipmentManager = pEquipmentManager
            CurrencyManager = pCurrencyManager
            CompanyManager = pCompanyManager
            ClaimManager = pClaimManager
        End Sub

        Public Function GetCertificate(request As GetCertificateInfoRequest) As GetCertificateInfoResponse Implements IGwPilService.GetCertificate

            'Dim sw As New Stopwatch()
            'sw.Start()

            request.Validate("request").HandleFault()

            Dim cert As Certificate
            Dim dealer As Dealer
            Dim languageCode As String = IIf(Not String.IsNullOrEmpty(request.Culture), request.Culture, LanguageCodes.USEnglish)
            Try
                dealer = DealerManager.GetDealerForGwPil(request.DealerCode)

            Catch dnfe As DealerNotFoundException
                Dim dnff As New DealerNotFoundFault
                'dnff.DealerCode = dnfe.DealerCode
                Throw New FaultException(Of DealerNotFoundFault)(dnff, "Dealer not found")
            End Try


            cert = CertificateManager.GetCertificateForGwPil(request.DealerCode, request.CertificateNumber)
            If (cert Is Nothing) Then
                Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "Certificate not found")
            End If

            Dim response As New GetCertificateInfoResponse

            Dim company As Company = CompanyManager.GetCompany(dealer.CompanyId)
            Dim product As Product = DealerManager.GetProduct(dealer.DealerCode, cert.ProductCode)
            Dim country As Country = CountryManager.GetCountry(cert.CountryPurchaseId)
            Dim address As Address
            If cert.AddressId.HasValue Then
                address = AddressManager.GetAddress(cert.AddressId)
            End If

            Dim currency As Currency = Nothing
            If Not cert.CURRENCY_CERT_ID Is Nothing Then
                currency = CurrencyManager.GetCurrency(cert.CURRENCY_CERT_ID)
            End If
            Dim companyGroup As CompanyGroup = CompanyGroupManager.GetCompanyGroup(company.CompanyGroupId)

            Dim contract As Contract = Nothing
            contract = DealerManager.GetContract(dealer.DealerCode, cert.WarrantySalesDate)

            response.Populate(cert, dealer, product, company, address, country, contract, companyGroup, EquipmentManager, CommonManager, ClaimManager, CountryManager, CertificateManager, languageCode, currency)
            response.Language = languageCode
            'response.Dealer = New DealerInfo(cert, dealer, CommonManager)

            Dim dealerAddress As Address
            If dealer.AddressId.HasValue Then
                dealerAddress = AddressManager.GetAddress(dealer.AddressId)
            Else
                dealerAddress = Nothing
            End If
            response.Dealer = New DealerInfo(cert, dealer, CommonManager, dealerAddress, CountryManager)

            response.Company = New CompanyInfo(company)

            Dim certContract As Contract = dealer.Contracts.Where(Function(c) cert.WarrantySalesDate >= c.Effective AndAlso cert.WarrantySalesDate <= c.Expiration).FirstOrDefault
            Dim producerAddress As Address
            Dim contractProducer As BO.Producer

            If Not certContract.ProducerId Is Nothing Then
                contractProducer = New BO.Producer(certContract.ProducerId)
                If Not contractProducer.AddressId.Equals(Guid.Empty) Then
                    producerAddress = AddressManager.GetAddress(contractProducer.AddressId)
                End If
            End If
            response.Contract = New ContractInfo(certContract, CommonManager, languageCode, CountryManager, contractProducer, producerAddress)

            'sw.Stop()
            'Dim ts As TimeSpan = sw.Elapsed

            Return response

        End Function


        Public Function SearchCertificate(request As SearchCertificateRequest) As SearchCertificateResponse Implements IGwPilService.SearchCertificate
            request.Validate("request").HandleFault()

            Dim searchResult As Collections.Generic.IEnumerable(Of Certificate)
            Dim intNumOfRecord As Integer = IIf(request.NumberOfRecords.HasValue AndAlso request.NumberOfRecords > 0 AndAlso request.NumberOfRecords < 100, request.NumberOfRecords, 100)
            'Dim languageCode As String = IIf(Not request.Culture Is Nothing AndAlso Not String.IsNullOrEmpty(request.Culture), request.Culture.Trim, LanguageCodes.USEnglish)
            Dim languageCode As String
            If Not request.Culture Is Nothing AndAlso Not String.IsNullOrEmpty(request.Culture) Then
                languageCode = request.Culture.Trim
            Else
                languageCode = LanguageCodes.USEnglish
            End If

            'validate language code
            Dim oLookUpList As DataView = BO.LookupListNew.GetLanguageLookupList()
            oLookUpList.RowFilter = "CODE='" + languageCode + "'"

            If oLookUpList.Count = 0 Then
                languageCode = LanguageCodes.USEnglish
            End If

            Dim searchResultIdList As Collections.Generic.List(Of Guid)

            Dim companylist As String
            Dim oCompany As Company

            If request.CompanyCodes.Count = 0 Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "At Least One Company Is Required")
            End If

            For Each s As String In request.CompanyCodes

                If s.Trim = String.Empty Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Code Is Empty")
                Else
                    Try
                        oCompany = CompanyManager.GetCompanyForGwPil(s)
                    Catch conf As CompanyNotFoundException
                        Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(conf), "Company Not Found")
                    End Try
                End If


                companylist += s
                companylist += ","
            Next

            'valiate wildcard search of Serial number and IMEI number
            If Not request.SerialNumber Is Nothing AndAlso request.SerialNumber.Contains("*") Then
                If request.SerialNumber.Replace("*", "").Trim.Length < 7 Then
                    Throw New FaultException(Of InvalidWildCardSearch)(New InvalidWildCardSearch(request.SerialNumber), "Invalid Serial Number wildcard search, a minimum of seven contiguous alpha-numeric characters required")
                End If
            End If

            If Not request.IMEINumber Is Nothing AndAlso request.IMEINumber.Contains("*") Then
                If request.IMEINumber.Replace("*", "").Trim.Length < 7 Then
                    Throw New FaultException(Of InvalidWildCardSearch)(New InvalidWildCardSearch(request.IMEINumber), "Invalid IMEI wildcard search, a minimum of seven contiguous alpha-numeric characters required")
                End If
            End If

            searchResultIdList = CERTBO.GWSearchCertificate(companylist, request.CertificateNumber, request.CustomerName, request.WorkPhone, request.HomePhone,
                                        request.AccountNumber, request.ServiceLineNumber, request.TaxId, request.Email, request.PurchaseInvoiceNumber,
                                        request.Address, request.Address2, request.Address3, request.Country, request.State, request.City,
                                        request.ZipCode, request.SerialNumber, request.IMEINumber, request.CertStatus, intNumOfRecord)

            searchResult = CertificateManager.GetSearchCertificateResultForGwPil(searchResultIdList)

            If (searchResult Is Nothing OrElse searchResult.Count = 0) Then
                Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "Certificate not found")
            End If

            Dim response As New SearchCertificateResponse

            response.Populate(searchResult, CertificateManager, CompanyGroupManager, CompanyManager, DealerManager, AddressManager, CountryManager, EquipmentManager, CommonManager, languageCode)
            response.Language = languageCode


            Return response
        End Function

        Public Function SearchCertificateByTaxId(request As SearchCertificateByTaxIdRequest) As SearchCertificateByTaxIdResponse Implements IGwPilService.SearchCertificateByTaxId
            'validate request required fields
            If request.CountryCodes.Trim = String.Empty Then
                Throw New FaultException(Of RequiredFieldMissingFault)(New RequiredFieldMissingFault(), "Country Code Required")
            End If
            If request.IdentificationNumber.Trim = String.Empty Then
                Throw New FaultException(Of RequiredFieldMissingFault)(New RequiredFieldMissingFault(), "Tax Id Required")
            End If


            'validate Country, if invalid, return invalid country fault
            Dim dvCountry As DataView = BO.LookupListNew.GetCountryLookupList()
            dvCountry.RowFilter = "CODE='" + request.CountryCodes + "'"
            If dvCountry.Count = 0 Then
                Throw New FaultException(Of InvalidCountryFault)(New InvalidCountryFault(), "Invalid Country Code - " + request.CountryCodes)
            End If

            'validate language code, if invalid, set to default of US
            Dim languageCode As String
            If Not request.Language Is Nothing AndAlso Not String.IsNullOrEmpty(request.Language) Then
                languageCode = request.Language.Trim
            Else
                languageCode = LanguageCodes.USEnglish
            End If
            'Dim languageCode As String = IIf(Not String.IsNullOrEmpty(request.Language), request.Language.Trim, LanguageCodes.USEnglish)
            Dim oLookUpList As DataView = BO.LookupListNew.GetLanguageLookupList()
            oLookUpList.RowFilter = "CODE='" + languageCode + "'"
            If oLookUpList.Count = 0 Then
                languageCode = LanguageCodes.USEnglish
            End If

            'search cert by tax id
            Dim CertList As DataSet, totalrecordFound As Long
            Dim response As New SearchCertificateByTaxIdResponse
            Dim strWorkPhone As String
            If request.WorkPhone Is Nothing Then
                strWorkPhone = String.Empty
            Else
                strWorkPhone = request.WorkPhone
            End If
            CertList = CertificateManager.GetCertificateByTaxId(request.CountryCodes, request.IdentificationNumber, strWorkPhone, 200, totalrecordFound) 'Get max 200 certs before applying filters

            Dim searchResult As Collections.Generic.List(Of DBSearchResultCertRecord) = DBSearchResultCertRecord.GetCertList(CertList)

            Dim dtCertList As New Collections.Generic.List(Of DBSearchResultCertRecord), certCount As Integer
            If request.NumberOfReocrds.HasValue AndAlso request.NumberOfReocrds.Value > 0 AndAlso request.NumberOfReocrds <= 50 Then
                certCount = request.NumberOfReocrds.Value
            Else
                certCount = 50
            End If

            'populate the counts by filters
            If Not request.Filters Is Nothing AndAlso request.Filters.Count > 0 Then
                Dim cntByFilter As Integer, intCnt As Integer
                Dim FilterResults As New Collections.Generic.List(Of SearchFilterResult)
                Dim filteredList As New Collections.Generic.List(Of DBSearchResultCertRecord)

                For Each f As SearchFilter In request.Filters
                    cntByFilter = searchResult.Where(Function(i) (String.IsNullOrEmpty(f.CertificateStatus) OrElse f.CertificateStatus = i.StatusCode) AndAlso (f.CoverageExpiredAfter.HasValue = False OrElse i.MaxCoverageEndDate >= f.CoverageExpiredAfter.Value) AndAlso (f.HasActiveClaims.HasValue = False OrElse (f.HasActiveClaims.Value = True AndAlso i.ActiveClaimCnt > 0) OrElse (f.HasActiveClaims.Value = False AndAlso i.ActiveClaimCnt = 0)) AndAlso (f.HasActiveCoverage.HasValue = False OrElse (f.HasActiveCoverage.Value = True AndAlso i.ActiveCovCount > 0) OrElse (f.HasActiveCoverage.Value = False AndAlso i.ActiveCovCount = 0))).Count()
                    FilterResults.Add(New SearchFilterResult(f, cntByFilter))

                    intCnt = certCount - dtCertList.Count
                    intCnt = IIf(intCnt > cntByFilter, cntByFilter, intCnt)
                    If intCnt > 0 Then
                        filteredList = searchResult.Where(Function(i) (String.IsNullOrEmpty(f.CertificateStatus) OrElse f.CertificateStatus = i.StatusCode) AndAlso (f.CoverageExpiredAfter.HasValue = False OrElse i.MaxCoverageEndDate >= f.CoverageExpiredAfter.Value) AndAlso (f.HasActiveClaims.HasValue = False OrElse (f.HasActiveClaims.Value = True AndAlso i.ActiveClaimCnt > 0) OrElse (f.HasActiveClaims.Value = False AndAlso i.ActiveClaimCnt = 0)) AndAlso (f.HasActiveCoverage.HasValue = False OrElse (f.HasActiveCoverage.Value = True AndAlso i.ActiveCovCount > 0) OrElse (f.HasActiveCoverage.Value = False AndAlso i.ActiveCovCount = 0))).Take(intCnt).ToList()
                        dtCertList.AddRange(filteredList.Where(Function(i) dtCertList.Contains(i) = False))
                    End If
                Next
                response.RecordsCountByFilter = FilterResults
            Else 'No filter, return the list from orginal list
                If certCount > searchResult.Count Then
                    certCount = searchResult.Count
                End If
                dtCertList.AddRange(searchResult.Take(certCount))
            End If


            response.TotalRecordFound = totalrecordFound
            response.Language = languageCode

            response.PopulateCertificateList(dtCertList, CertificateManager, CommonManager, CompanyGroupManager, DealerManager, AddressManager, CountryManager, languageCode)

            Return response
        End Function
    End Class
End Namespace