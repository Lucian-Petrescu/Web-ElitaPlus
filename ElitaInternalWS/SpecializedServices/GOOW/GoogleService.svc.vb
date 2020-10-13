' NOTE: You can use the "Rename" command on the context menu to change the class name "GoogleService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select GoogleService.svc or GoogleService.svc.vb at the Solution Explorer and start debugging.
Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccess
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.Common.Types

Namespace SpecializedServices.Goow
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices/Goow")>
    Public Class GoogleService
        Implements IGoogleService

        Private Property CertificateManager As ICertificateManager
        Private Property ClaimManager As IClaimManager
        Private Property DealerManager As IDealerManager
        Private Property CommonManager As ICommonManager
        Private Property CountryManager As ICountryManager
        Private Property CompanyManager As ICompanyManager
        Private Property CompanyGroupManager As ICompanyGroupManager
        Private Property AddressManager As IAddressManager


        Public Sub New(pCertificateManager As ICertificateManager,
                       pDealerManager As IDealerManager,
                       pCommonManager As ICommonManager,
                       pClaimManager As IClaimManager,
                       pCountryManager As ICountryManager,
                       pCompanyManager As ICompanyManager,
                       pCompanyGroupManager As ICompanyGroupManager,
                       pAddressManager As IAddressManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If
            If (pDealerManager Is Nothing) Then
                Throw New ArgumentNullException("pDealerManager")
            End If
            If (pCommonManager Is Nothing) Then
                Throw New ArgumentNullException("pCommonManager")
            End If
            If (pClaimManager Is Nothing) Then
                Throw New ArgumentNullException("pClaimManager")
            End If
            If (pCountryManager Is Nothing) Then
                Throw New ArgumentNullException("pCountryManager")
            End If
            If (pCompanyManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyManager")
            End If
            If (pCompanyGroupManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyGroupManager")
            End If
            If (pAddressManager Is Nothing) Then
                Throw New ArgumentNullException("pAddressManager")
            End If
            CertificateManager = pCertificateManager
            DealerManager = pDealerManager
            CommonManager = pCommonManager
            ClaimManager = pClaimManager
            CountryManager = pCountryManager
            CompanyManager = pCompanyManager
            CompanyGroupManager = pCompanyGroupManager
            AddressManager = pAddressManager
        End Sub
        Public Function CreateClaim(request As CreateClaimRequest) As CreateClaimResponse Implements IGoogleService.CreateClaim

            Dim response As New CreateClaimResponse

            request.Validate("request").HandleFault()

                ''''Locate/Validate Dealer
                Dim oDealer As Dealer
                Dim oCompany As Company
                Try
                    oDealer = DealerManager.GetDealer(request.DealerCode)
                    oCompany = oDealer.GetCompany(CompanyManager)
                Catch dnfe As DealerNotFoundException
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
                End Try

                ''''Locate Certificate
                Dim oCert As Certificate = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CertificateNotFound), "Certificate Not found")
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If

            oCert.SubscriberStatus = SubscriberStatusCodes.Active.ToGuid(ListCodes.SubscriberStatus, CommonManager)

            '''''Locate Coverage
            Dim cic As CertificateItemCoverage
                Try
                    cic = ClaimManager.LocateCoverage(oCert, request.DateOfLoss, request.CoverageTypeCode, Nothing)
                Catch cnf As CoverageNotFoundException
                    Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(), "Coverage Not found")
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CoverageNotFound), "Coverage Not found")
                Catch mcf As MultipleCoverageFoundException
                    Throw New FaultException(Of MultipleCoveragesFoundFault)(New MultipleCoveragesFoundFault(), "Multiple coverages found")
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.MultipleCoveragesFound), "Multiple coverages found")
                End Try


            ''''validate Date of Loss
            '''''GOOW - The time portion of the Date is not compared with DateTime.today as the Claim is being filed on the same Day when the Policy is Enrolled.
            If (request.DateOfLoss.ToShortDateString > DateTime.Today OrElse request.DateOfLoss < cic.BeginDate OrElse
                    request.DateOfLoss > cic.EndDate) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.InvalidDateOfLoss), "Invalid Date Of Loss")
                Throw New FaultException(Of InvalidDateOfLossFault)(New InvalidDateOfLossFault(), "Invalid Date Of Loss")
            End If

            ''''validate service center
            Try
                    If (CountryManager.GetServiceCenterByCode(oDealer.GetCompany(CompanyManager).BusinessCountryId, request.ServiceCenterCode) Is Nothing) Then
                        'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.ServiceCenterNotFound), "Service Center not found")
                        Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), "Service Center not found")
                    End If
                Catch ex As Exception
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.ServiceCenterNotFound), "Service Center not found")
                    Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), "Service Center not found")
                End Try

                ''''Create Claim Shell
                Dim oClaim As Claim = New Claim()
                oClaim.ClaimId = Guid.NewGuid()

                oClaim.ProblemDescription = request.ProblemDescription
                oClaim.LossDate = request.DateOfLoss

                If (request.ContactName = String.Empty) Then
                    oClaim.ContactName = oCert.CustomerName
                    oClaim.CallerName = oCert.CustomerName
                Else
                    oClaim.ContactName = request.ContactName
                    oClaim.CallerName = request.ContactName
                End If

                Try
                oClaim = ClaimManager.CreateClaim(cic,
                                                         oClaim,
                                                         oDealer,
                                                         request.CoverageTypeCode,
                                                         request.ServiceCenterCode,
                                                         request.ClaimType,
                                                         request.Make,
                                                         request.Model,
                                                         request.Comments.ToString(),
                                                         request.CauseOfLoss.ToString())
            Catch ex As PriceListNotConfiguredException
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.PriceListNotConfigured), "Price List not configured for service center " + request.ServiceCenterCode)
                    Throw New FaultException(Of PriceListNotConfiguredFault)(New PriceListNotConfiguredFault, "Price List not configured for service center " & request.ServiceCenterCode)
                Catch mnf As CertificateItemNotFoundException
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.MakeAndModelNotFound), "Make is required")
                    Throw New FaultException(Of MakeAndModelNotFoundFault)(New MakeAndModelNotFoundFault(), "Make is required")
                Catch mnf As ManufacturerNotFoundException
                    'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.InvalidManufacturer), "Invalid Manufacturer:" + request.Make)
                    Throw New FaultException(Of ManufacturerNotFoundFault)(New ManufacturerNotFoundFault(), "Invalid Manufacturer :" & request.Make)
                End Try

                oClaim = ClaimManager.SaveClaim(oClaim)
                response.ClaimNumber = oClaim.ClaimNumber

                Return response


        End Function


        Public Sub DenyClaim(request As DenyClaimRequest) Implements IGoogleService.DenyClaim
            request.Validate("request").HandleFault()
            Dim oCompany As Company
            Dim oClaim As Claim
            Dim oCert As Certificate

            Try
                oCompany = CompanyManager.GetCompany(request.CompanyCode)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

                If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                    Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
                End If
                oCert = CertificateManager.GetCertifcateByItemCoverage(oClaim.CertItemCoverageId)

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found : " & request.CompanyCode)
            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Repair Claim Not Found : " & request.ClaimNumber)
            End Try

            Try
                If (oClaim.RepairDate Is Nothing) Then
                    ClaimManager.DenyClaim(oClaim, oCert, request.Comments.ToString())
                End If
            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim already Fuillfilled")
            End Try
        End Sub

        Public Function ComputeTax(request As ComputeTaxRequest) As ComputeTaxResponse Implements IGoogleService.ComputeTax

            request.Validate("request").HandleFault()

            Dim response As New ComputeTaxResponse()
            Dim oDealer As Dealer
            Dim oCompany As Company
            Dim oCountry As Country

            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = oDealer.GetCompany(CompanyManager)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
            End Try

            Try
                oCountry = CountryManager.GetCountryByCode(request.CountryCode)
            Catch ex As Exception
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Country not found")
            End Try

            If (request.Amount = 0) Then
                Throw New FaultException(Of InvalidTaxAmountFault)(New InvalidTaxAmountFault(), "Invalid Tax Amount: " & request.Amount)
            End If

            Try
                Dim productTaxTypeID As Guid = CommonManager.GetListItems(ListCodes.ProductTaxTypeCode).Where(Function(li) li.Code = ProductTaxTypeCodes.All).FirstOrDefault.ListItemId
                Dim taxTypeID As Guid = CommonManager.GetListItems(ListCodes.TaxTypeCode).Where(Function(li) li.Code = TaxTypeCodes.Pos).FirstOrDefault.ListItemId

                Dim taxAmount As Decimal = ClaimManager.ComputeTax(request.Amount,
                                        oDealer.DealerId,
                                        oCountry.CountryId,
                                        oCompany.CompanyTypeId,
                                        taxTypeID,
                                        Nothing,
                                        oDealer.EXPECTED_PREMIUM_IS_WP_ID,
                                        productTaxTypeID,
                                        DateTime.Today.Date)

                response.TaxAmount = Math.Round(taxAmount, 4)

            Catch ex As DataException
                Throw New FaultException(Of DatabaseErrorFault)(New DatabaseErrorFault(), "Unexpected Database Error")
            End Try
            Return response
        End Function

        Public Sub FulfillClaim(request As FulfillClaimRequest) Implements IGoogleService.FulfillClaim
            request.Validate("request").HandleFault()

            Dim oCompany As Company
            Dim oDealer As Dealer
            Dim oClaim As Claim
            Dim oCert As Certificate


            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = CompanyManager.GetCompany(oDealer.CompanyId)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

                oCert = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Company Not Found : " & request.DealerCode)
            End Try


            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If


            If (request.ClaimType = ClaimTypeCodes.OriginalReplacement) Then
                If (Not request.ClaimNumber.EndsWith("R")) Then
                    Throw New FaultException(Of ReplacementClaimFoundFault)(New ReplacementClaimFoundFault(), "Replacement Claim Not Found")
                End If

            End If

            If (oClaim.RepairDate IsNot Nothing AndAlso (request.RepairDate < oClaim.LossDate OrElse request.RepairDate > DateTime.Today)) Then
                Throw New FaultException(Of InvalidRepairDateFault)(New InvalidRepairDateFault(), "Invalid Repair Date")
            End If

            If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
            End If

            Try
                ClaimManager.FulfillClaim(oClaim,
                                    oCert,
                                    oCompany,
                                    request.DealerCode,
                                    request.Comments.ToString(),
                                    request.RepairDate,
                                    request.ClaimType,
                                    request.SerialNumber,
                                    request.Model,
                                    request.Make,
                                    request.ServiceCenterCode,
                                    request.TrackingNumber)
            Catch rplcf As ReplacementClaimFoundException
                Throw New FaultException(Of ReplacementClaimFoundFault)(New ReplacementClaimFoundFault(),
                                                    rplcf.Message)
            Catch clnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Repair Claim Not Found : " & request.ClaimNumber)
            End Try
        End Sub

        Public Sub ChangeServiceCenter(request As ChangeServiceCenterRequest) Implements IGoogleService.ChangeServiceCenter
            request.Validate("request").HandleFault()

            Dim oCompany As Company
            Dim oDealer As Dealer
            Dim oClaim As Claim
            Dim oCert As Certificate

            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = CompanyManager.GetCompany(oDealer.CompanyId)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

                oCert = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Company Not Found : " & request.DealerCode)
            End Try


            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If

            Try
                If (CountryManager.GetServiceCenterByCode(oDealer.GetCompany(CompanyManager).BusinessCountryId, request.ServiceCenterCode) Is Nothing) Then
                    Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), "Service Center not found")
                End If
            Catch ex As Exception
                Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), "Service Center not found")
            End Try


            If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
            End If

            ClaimManager.ChangeServiceCenter(oClaim,
                                      oCert,
                                      oCompany,
                                      request.Comments.ToString(),
                                      request.ServiceCenterCode)
        End Sub

        Public Sub PayClaim(request As PayClaimRequest) Implements IGoogleService.PayClaim
            request.Validate("request").HandleFault()

            Dim oCompany As Company
            Dim oDealer As Dealer
            Dim oClaim As Claim
            Dim oCert As Certificate
            Dim oCountry As Country
            Dim oClaimInvoice As ClaimInvoice
            Dim oDisbursement As Disbursement

            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = CompanyManager.GetCompany(oDealer.CompanyId)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

                oCert = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Company Not Found : " & request.DealerCode)
            End Try


            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If


            If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
            End If

            If (request.ClaimNumber.ToUpperInvariant.EndsWith("S")) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), " Service Warranty Claim Cannot be Paid : " & request.ClaimNumber)
            End If


            Try
                ClaimManager.PayClaim(oClaim,
                                      oCert,
                                      oCompany,
                                      request.ServiceCenterCode,
                                      request.InvoiceNumber,
                                      request.PaymentAmount)

            Catch ex As InvalidPaymentAmountException
                Throw New FaultException(Of InvalidPaymentAmountFault)(New InvalidPaymentAmountFault(), "Invalid Payment Amount")
            Catch ex As InvalidCauseOfLossException
                Throw New FaultException(Of InvalidCauseOfLossFault)(New InvalidCauseOfLossFault(), "Coverage Type is No Longer Valid")
            End Try

        End Sub


        Public Sub ReturnDamagedDeviceAdvEx(request As ReturnDamagedDeviceAdvExRequest) Implements IGoogleService.ReturnDamagedDeviceAdvEx
            request.Validate("request").HandleFault()

            ''''Locate/Validate Dealer
            Dim oDealer As Dealer
            Dim oCompany As Company
            Dim oClaim As Claim
            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = oDealer.GetCompany(CompanyManager)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Company Not Found : " & request.DealerCode)
            End Try

            ''''Locate Certificate
            Dim oCert As Certificate = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CertificateNotFound), "Certificate Not found")
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If

            If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
            End If

            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If

            ClaimManager.ReturnDamagedDeviceAdvEx(oClaim,
                                                   oCert,
                                                   oCompany,
                                                   oDealer,
                                                   request.Comments.ToString(),
                                                   request.CoverageTypeCode)

        End Sub

        Public Sub MaxDaysElapsedAdvEx(request As MaxDaysElapsedAdvExRequest) Implements IGoogleService.MaxDaysElapsedAdvEx
            request.Validate("request").HandleFault()

            Dim oDealer As Dealer
            Dim oCompany As Company
            Dim oClaim As Claim
            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = oDealer.GetCompany(CompanyManager)
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Company Not Found : " & request.DealerCode)
            End Try

            ''''Locate Certificate
            Dim oCert As Certificate = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CertificateNotFound), "Certificate Not found")
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If

            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If

            If (oClaim.StatusCode = ClaimStatusCodes.Closed OrElse oClaim.StatusCode = ClaimStatusCodes.Denied) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Claim already denied or closed")
            End If

            ClaimManager.MaxDaysElapsedAdEx(oClaim,
                                                   oCert,
                                                   oCompany,
                                                   oDealer,
                                                   request.Comments.ToString())


        End Sub

        Public Function GetClaimInfo(request As GetClaimInfoRequest) As GetClaimInfoResponse Implements IGoogleService.GetClaimInfo

            Dim response As New GetClaimInfoResponse
            request.Validate("request").HandleFault()

            ''''Locate/Validate Dealer
            Dim oDealer As Dealer
            Dim oCompany As Company
            Dim oClaim As Claim

            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = oDealer.GetCompany(CompanyManager)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
            End Try

            ''''Locate Certificate
            Dim oCert As Certificate = CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CertificateNotFound), "Certificate Not found")
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If

            Try
                oClaim = ClaimManager.GetClaim(request.ClaimNumber, oCert.CompanyId)
            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End Try

            If (oCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oClaim.CertItemCoverageId).Count = 0) Then
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End If

            response.CustomerName = oCert.CustomerName
            response.CertificateNumber = request.CertificateNumber
            response.AuthorizedAmount = oClaim.AuthorizedAmount
            Dim shippingAddress As Address = AddressManager.GetAddress(oCert.AddressId)

            response.ShippingInfo = New ShippingInfo()
            response.ShippingInfo.Address1 = shippingAddress.Address1
            response.ShippingInfo.Address2 = shippingAddress.Address2
            response.ShippingInfo.City = shippingAddress.City

            If shippingAddress.RegionId IsNot Nothing Then
                response.ShippingInfo.State = CountryManager.GetRegion(shippingAddress.CountryId, shippingAddress.RegionId).Description
            Else
                response.ShippingInfo.State = String.Empty
            End If

            response.ShippingInfo.PostalCode = shippingAddress.PostalCode

            response.ClaimNumber = request.ClaimNumber
            response.Make = CompanyGroupManager.GetCompanyGroup(oCompany.CompanyGroupId).Manufacturers.Where(Function(m) m.ManufacturerId = oClaim.ClaimEquipments().First.ManufacturerId).FirstOrDefault.Description
            response.Model = oClaim.ClaimEquipments().First.Model
            response.SerialNumber = oClaim.ClaimEquipments().First.SerialNumber
            response.TrackingNumber = oClaim.TrackingNumber

            Return response
        End Function
    End Class
End Namespace