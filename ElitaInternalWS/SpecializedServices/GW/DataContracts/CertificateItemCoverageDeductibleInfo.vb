Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="ItemCoverageDeductibleInfo")>
    Public Class CertificateItemCoverageDeductibleInfo
        <DataMember(IsRequired:=True, Name:="MethodOfRepairCode")>
        Public Property MethodOfRepairCode As String

        <DataMember(IsRequired:=True, Name:="MethodOfRepairDesc")>
        Public Property MethodOfRepairDesc As String

        <DataMember(IsRequired:=True, Name:="DeductibleBasedOnCode")>
        Public Property DeductibleBasedOnCode As String

        <DataMember(IsRequired:=True, Name:="DeductibleBasedOnDesc")>
        Public Property DeductibleBasedOnDesc As String

        <DataMember(IsRequired:=True, Name:="Deductible")>
        Public Property Deductible As Nullable(Of Decimal)


        Public Sub New()

        End Sub

        Public Sub New(pCertficateItemCoverageDed As CertificateItemCoverageDeductible,
                       pCommonManager As CommonManager,
                       pLangauge As String)
            With Me
                .Deductible = pCertficateItemCoverageDed.Deductible
                .MethodOfRepairCode = pCertficateItemCoverageDed.MethodOfRepairId.ToCode(pCommonManager, ListCodes.MethodOfRepair, pLangauge)
                .MethodOfRepairDesc = pCertficateItemCoverageDed.MethodOfRepairId.ToDescription(pCommonManager, ListCodes.MethodOfRepair, pLangauge)
                .DeductibleBasedOnCode = pCertficateItemCoverageDed.DeductibleBasedOnId.ToCode(pCommonManager, ListCodes.DeductibleBasedOn, pLangauge)
                .DeductibleBasedOnDesc = pCertficateItemCoverageDed.DeductibleBasedOnId.ToDescription(pCommonManager, ListCodes.DeductibleBasedOn, pLangauge)
            End With
        End Sub
    End Class
End Namespace