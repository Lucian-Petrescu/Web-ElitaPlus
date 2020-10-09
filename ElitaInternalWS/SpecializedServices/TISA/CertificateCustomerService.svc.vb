' NOTE: You can use the "Rename" command on the context menu to change the class name "CertificateCustomerService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select CertificateCustomerService.svc or CertificateCustomerService.svc.vb at the Solution Explorer and start debugging.
Imports Assurant.ElitaPlus.Business
Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.Tisa
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices/TISA")>
    Public Class CertificateCustomerService
        Implements ICertificateCustomerService
        Private Property CertificateManager As ICertificateManager

        Private Property CompanyManager As ICompanyManager

        Private Property DealerManager As IDealerManager


        Public Sub New(pCertificateManager As ICertificateManager,
                       pCompanyManager As ICompanyManager,
                       pDealerManager As IDealerManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If
            If (pCompanyManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyManager")
            End If
            If (pDealerManager Is Nothing) Then
                Throw New ArgumentNullException("pDealerManager")
            End If

            CertificateManager = pCertificateManager
            CompanyManager = pCompanyManager
            DealerManager = pDealerManager

        End Sub
        Public Function GetCertificateCustomer(request As CustomerByCertificateRequest) As CustomerResponse Implements ICertificateCustomerService.GetCertificateCustomer
            Dim response As New CustomerResponse

            request.Validate("request").HandleFault()

            Dim CertList As DataSet
            Dim oCompany As Company
            Dim oDealer As Dealer

            ''check for companycode
            Try
                oCompany = CompanyManager.GetCompany(request.CompanyCode)

                If (request.DealerCode <> String.Empty) Then
                    oDealer = DealerManager.GetDealer(request.DealerCode)
                End If

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found : " & request.CompanyCode)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found:" & request.DealerCode)
            End Try

            ''check if atleast one of the request fields are provided other than company code (mandatory)
            If (request.CertificateNumber = String.Empty AndAlso
                    request.IdentificationNumber = String.Empty AndAlso
                    request.PhoneNumber = String.Empty AndAlso
                    request.SerialNumber = String.Empty AndAlso
                    request.ServiceLineNumber = String.Empty) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please Provide atleast one value")

            End If

            ''''Locate Certificate
            CertList = CertificateManager.GetCertificate(request.CompanyCode,
                                                                                request.DealerCode,
                                                                                request.CertificateNumber,
                                                                                request.IdentificationNumber,
                                                                                request.PhoneNumber,
                                                                                request.ServiceLineNumber,
                                                                                request.SerialNumber)




            '''''if the certificate number is provided then only one certificate object with multiple items can be expected
            response.CustomerInfo = New List(Of CustomerInfoResponse)

            If Not CertList Is Nothing AndAlso CertList.Tables.Count > 0 Then
                Dim dtCustomers As List(Of DataTable) = CertList.Tables(0).AsEnumerable().GroupBy(Function(i) i.Field(Of String)("customer_name")).Select(Function(g) g.CopyToDataTable()).ToList()
                For Each dt As DataTable In dtCustomers
                    Dim customer As CustomerInfoResponse = New CustomerInfoResponse(dt)
                    response.CustomerInfo.Add(customer)

                    Dim dtCerts As List(Of DataTable) = dt.AsEnumerable().GroupBy(Function(i) i.Field(Of String)("cert_number")).Select(Function(g) g.CopyToDataTable()).ToList()
                    response.CustomerInfo.Where(Function(c) c.CustomerName = customer.CustomerName AndAlso c.IdentificationNumber = customer.IdentificationNumber).First.Certificates = New List(Of CustomerCertificateResponse)

                    For Each dtCert As DataTable In dtCerts
                        Dim cert As CustomerCertificateResponse = New CustomerCertificateResponse(dtCert)
                        response.CustomerInfo.Where(Function(c) c.CustomerName = customer.CustomerName AndAlso c.IdentificationNumber = customer.IdentificationNumber).First.Certificates.Add(cert)

                        Dim dtItems As List(Of DataTable) = dtCert.AsEnumerable().GroupBy(Function(i) i.Field(Of String)("Model")).Select(Function(g) g.CopyToDataTable()).ToList()
                        response.CustomerInfo.Where(Function(c) c.CustomerName = customer.CustomerName AndAlso c.IdentificationNumber = customer.IdentificationNumber).First.Certificates.Where(Function(c) c.CertificateNumber = cert.CertificateNumber).First.Items = New List(Of CustomerCertificateItemResponse)

                        For Each dtItem As DataTable In dtItems
                            Dim item As CustomerCertificateItemResponse = New CustomerCertificateItemResponse(dtItem)
                            response.CustomerInfo.Where(Function(c) c.CustomerName = customer.CustomerName AndAlso c.IdentificationNumber = customer.IdentificationNumber).First.Certificates.Where(Function(c) c.CertificateNumber = cert.CertificateNumber).First.Items.Add(item)
                        Next

                    Next
                Next

            End If


            If (response.CustomerInfo.Count = 0 OrElse response Is Nothing) Then
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If


            Return response
        End Function

    End Class
End Namespace