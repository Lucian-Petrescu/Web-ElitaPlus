' NOTE: You can use the "Rename" command on the context menu to change the class name "CHLSamsungUpgradeService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select CHLSamsungUpgradeService.svc or CHLSamsungUpgradeService.svc.vb at the Solution Explorer and start debugging.
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities


Namespace SpecializedServices.Tisa
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices/TISA")>
    Public Class CHLSamsungUpgradeService
        Implements ICHLSamsungUpgradeService

        Private Property CertificateManager As ICertificateManager
        Private Property DealerManager As IDealerManager

        Public Sub New(ByVal pCertificateManager As ICertificateManager,
                       ByVal pDealerManager As IDealerManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If
            If (pDealerManager Is Nothing) Then
                Throw New ArgumentNullException("pDealerManager")
            End If

            Me.CertificateManager = pCertificateManager
            Me.DealerManager = pDealerManager

        End Sub

        Public Function GetPremiumFromProduct(request As GetCertGrossAmtByProdCodeRequst) As GetCertGrossAmtByProdCodeResponse Implements ICHLSamsungUpgradeService.GetPremiumFromProduct
            Dim response As New GetCertGrossAmtByProdCodeResponse

            request.Validate("request").HandleFault()

            ''''Locate/Validate Dealer
            Dim oDealer As Dealer
            Dim oCompany As Company
            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)

            Catch dnfe As DealerNotFoundException
                ' Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.DealerNotFound), "Dealer Not found")
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer Not Found")
                ' Throw New FaultException(Of ElitaFault)(New DealerNotFoundFault(), "Dealer not found")
            End Try


            ''''Locate Certificate
            Dim oCert As Certificate = Me.CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                'Throw New FaultException(Of ElitaFault)(New ElitaFault(ElitaFault.EnumFaultType.CertificateNotFound), "Certificate Not found")
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            End If


            Dim oGrossAmount As Decimal
            Dim oCurrencyCode As String
            CertificateManager.GetCertificateGrossAmtByProductCode(oCert.CertificateId, oCurrencyCode, oGrossAmount)

            response.GrossAmount = oGrossAmount
            response.CurrencyCode = oCurrencyCode

            Return response
        End Function

    End Class

End Namespace