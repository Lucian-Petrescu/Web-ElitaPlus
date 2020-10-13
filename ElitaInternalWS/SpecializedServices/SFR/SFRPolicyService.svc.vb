Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew
Imports CERTBO = Assurant.ElitaPlus.BusinessObjectsNew.Certificate

Namespace SpecializedServices.SFR
    Public Class SFRPolicyService
        Implements ISFRPolicyService

         Private Property CertificateManager As ICertificateManager
       
        Public Sub New(pCertificateManager As ICertificateManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If
           
            CertificateManager = pCertificateManager
           
        End Sub

        Public Function SearchCertificate(request As SearchCertificateRequest) As SearchCertificateResponse Implements ISFRPolicyService.SearchCertificate
            request.Validate("request").HandleFault()

            Dim response As new SearchCertificateResponse
            Dim searchResult As Collections.Generic.IEnumerable(Of Certificate)                                   
            Dim ds As DataSet                              

            If  (request.CompanyCode Is Nothing OrElse String.IsNullOrEmpty(request.CompanyCode))  Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Code Is Empty")
            End If
            
            If ( (String.IsNullOrEmpty(request.DealerCode) AndAlso String.IsNullOrEmpty(request.DealerGroup)) ) Then
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer Code Or Dealer Group Is Empty")
            End If

            If ( (String.IsNullOrEmpty(request.FirstName) AndAlso Not String.IsNullOrEmpty(request.LastName) ) _
                OrElse (Not String.IsNullOrEmpty(request.FirstName) AndAlso String.IsNullOrEmpty(request.LastName)) ) Then
                Throw New FaultException(Of CustomerNameNotFoundFault)(New CustomerNameNotFoundFault(), "Customer FirstName Or LastName Is Empty")
            End If
           
            If ( (String.IsNullOrEmpty(request.PostalCode) AndAlso Not String.IsNullOrEmpty(request.Email) ) _
                  OrElse ( Not String.IsNullOrEmpty(request.PostalCode) AndAlso String.IsNullOrEmpty(request.Email)) ) Then
                Throw New FaultException(Of EmailandPostalCodeNotFoundFault)(New EmailandPostalCodeNotFoundFault(), "Email Or Postal Code Is Empty")
            End If

            If (String.IsNullOrEmpty(request.FirstName) _
                AndAlso String.IsNullOrEmpty(request.LastName) _
                AndAlso String.IsNullOrEmpty(request.LastName) _
                AndAlso String.IsNullOrEmpty(request.PhoneNumber) _
                AndAlso String.IsNullOrEmpty(request.Email) _
                AndAlso String.IsNullOrEmpty(request.PostalCode) _
                AndAlso String.IsNullOrEmpty(request.IdentificationNumber)
                ) Then
                Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "One Search Criteria is Required ")
            End If

             ds = CertificateManager.SearchCertificateBYCustomerInfo(request.CompanyCode, request.DealerCode, request.DealerGroup, request.FirstName, request.LastName, request.PhoneNumber,request.Email,request.PostalCode, request.IdentificationNumber)

             If (ds.Tables.Count = 0) Then
                Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "Certificate not found")             

             elseif ( ds.Tables(0).Rows.Count > 20) Then
                Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "More than 20 Certificates Found.Please Refine the Search")
             Else            
             response.CertificateInfo = ds _
                .Tables(0) _
                .AsEnumerable() _
                .[Select](Function(row) New CertificateDC() With
                        {
                                .CertificateNumber = row.Field(Of String)("cert_number"),
                                .CustomerName = row.Field(Of String)("customer_name"),
                                .WorkPhone = row.Field(Of String)("work_phone"),
                                .IdentificationNumber = row.Field(Of String)("identification_number"),
                                .Statuscode = row.Field(Of String)("status_Code"),
                                .SerialNumber = row.Field(Of String)("serial_number"),
                                .Manufacturer = row.Field(Of String)("make"),
                                .Model = row.Field(Of String)("model"),
                                .SKUNumber = row.Field(Of String)("sku_number"),
                                .ProductCode = row.Field(Of String)("product_code"),
                                .ProductDescription = row.Field(Of String)("description"),
                                .WarrantySalesDate = row.Field(Of Date?)("warranty_sales_date"),
                                .ServiceLineNumber = row.Field(Of String)("service_line_number"),
                                .DealerCode = row.Field(Of String)("dealer"),
                                .ItemDescription = row.Field(Of String)("item_description")
                            }) _
                .ToList()

            End If                    

            Return response
        End Function

    End Class
End Namespace
