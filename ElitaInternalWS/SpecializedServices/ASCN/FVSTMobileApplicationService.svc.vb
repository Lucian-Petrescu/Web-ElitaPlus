Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Public Class FVSTMobileApplicationService
    Implements IFVSTMobileApplicationService

    Private Const DateFormat As String = "yyyy/MM/dd"

    Private Const DealerCode As String = "FVST"
    Private ReadOnly CertificateManager As ICertificateManager
    Private ReadOnly CommonManager As ICommonManager


    Public Sub New(pCertificateManager As ICertificateManager, pCommonManager As ICommonManager)

        If (pCertificateManager Is Nothing) Then
            Throw New ArgumentNullException("pCertificateManager")
        End If

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        CertificateManager = pCertificateManager
        CommonManager = pCommonManager
    End Sub

    Public Function GetCertificateInfo(request As GetCertificateInfoRequest) As GetCertificateInfoResponse Implements IFVSTMobileApplicationService.GetCertificateInfo

        request.Validate("request").HandleFault()
        Dim cert As Certificate
        Try
            cert = CertificateManager.GetCertificate(DealerCode, request.CertificateNumber)
        Catch dnfe As DealerNotFoundException
            Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer not found")
        End Try


        If (cert Is Nothing) Then
            Throw New FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)(New ElitaInternalWS.SpecializedServices.CertificateNotFoundFault(), "对不起，没有找到有效的保单，请重新输入保单号查询或者直接拨打服务热线")
        End If

        Dim response As New GetCertificateInfoResponse()
        Dim certDetails As New Collections.Generic.List(Of CertificateDetails)
        certDetails.AddRange((From cic As CertificateItemCoverage In cert.ItemCoverages.Where(Function(icic) icic.Item.ItemNumber = 1)
                              Select New CertificateDetails() With
                {
                    .CertificateNumber = cert.CertificateNumber,
                    .CellNumber = cert.WorkPhone,
                    .StatusChinese = cert.StatusCode.ToDescription(CommonManager, ListCodes.CertificateStatus, LanguageCodes.Chinese),
                    .StatusEnglish = cert.StatusCode.ToDescription(CommonManager, ListCodes.CertificateStatus, LanguageCodes.USEnglish),
                    .ItemDescription = cic.Item.ItemDescription,
                    .CustomerName = cert.CustomerName,
                    .CoverageChinese = cic.CoverageTypeId.ToDescription(CommonManager, ListCodes.CoverageType, LanguageCodes.Chinese),
                    .CoverageEnglish = cic.CoverageTypeId.ToDescription(CommonManager, ListCodes.CoverageType, LanguageCodes.USEnglish),
                    .CoverageDuration = cic.BeginDate.GetFVSTMonths(cic.EndDate),
                    .WarrantyPurchaseDate = IIf(cert.WarrantySalesDate.HasValue, cert.WarrantySalesDate.Value.ToString(DateFormat), String.Empty)
                }))
        response.Coverages = certDetails
        Return response

    End Function

End Class
