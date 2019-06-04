Imports ElitaInternalWS.Certificates
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.Business

' NOTE: You can use the "Rename" command on the context menu to change the class name "CertCustomerInfoService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select CertCustomerInfoService.svc or CertCustomerInfoService.svc.vb at the Solution Explorer and start debugging.
Public Class CertCustomerInfoService
    Implements ICertCustomerInfoService

    Public Function GetCertCustomerInfo(ByVal request As GetCertificateRequest) As CertCustomerInfoResponse Implements ICertCustomerInfoService.GetCertCustomerInfo

        ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
        Dim req As CertificateNumberLookup = DirectCast(request.CertificateSearch, CertificateNumberLookup)
        request.Validate("request").HandleFault()
        'req.Validate()

        Dim oCertificate As Certificate
        Try
            oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch)

        Catch dnfe As DealerNotFoundException
            Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
        End Try


        If (oCertificate Is Nothing) Then
            Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "对不起，没有找到有效的保单，请重新输入保单号查询或者直接拨打服务热线")
        End If

        Dim response As New CertCustomerInfoResponse()
        Dim oCertCustInfo As New CertCustInfo(oCertificate)

        response.CertCustInfo = oCertCustInfo
        Return response

    End Function

End Class
