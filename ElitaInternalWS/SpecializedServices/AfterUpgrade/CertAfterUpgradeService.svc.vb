Imports ElitaInternalWS.Certificates
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DALObjects
Imports System.Linq
Imports ElitaInternalWS.Security
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "CertUpgradeService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select CertUpgradeService.svc or CertUpgradeService.svc.vb at the Solution Explorer and start debugging.
Public Class CertAfterUpgradeService
    Implements ICertAfterUpgradeService

    Public Function GetCertAfterUpgrade(request As GetCertificateRequest) As GetCertificateResponse Implements ICertAfterUpgradeService.GetCertAfterUpgrade
        Dim oCertificate As Certificate

        ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
        Dim req As CertAfterUpgradeLookup = DirectCast(request.CertificateSearch, CertAfterUpgradeLookup)
        request.Validate("request").HandleFault()
        req.Validate()

        Try
        ' Find Certificate based on Request
        oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch)


        Catch ex As CertificateNotFoundException

            If (Not request.CertificateSearch.GetType() Is GetType(CertAfterUpgradeLookup)) Then
                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = ex.CertificateSearch}, "Certificate Not Found")
            End If

        End Try


        Dim response As GetCertificateResponse = CertificateServiceV1.BuildCertificateResponse(request, oCertificate)

        Return response

    End Function

End Class
