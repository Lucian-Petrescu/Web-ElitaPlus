Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports System.Collections.Generic

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificate", Name:="ItemCoverage")>
    Public Class SearchResultCertificateItemCoverageInfo
        <DataMember(IsRequired:=True, Name:="CoverageTypeCode")>
        Public Property CoverageTypeCode As String

        <DataMember(IsRequired:=True, Name:="CoverageName")>
        Public Property CoverageName As String

        <DataMember(IsRequired:=True, Name:="EffectiveStartingOn")>
        Public Property BeginDate As Date

        <DataMember(IsRequired:=True, Name:="EffectiveEndingOn")>
        Public Property EndDate As Date

        Public Sub New(pCertficateItemCoverage As CertificateItemCoverage,
                       pCommonManager As CommonManager,
                       pLangauge As String)
            ' Copy properties from Certificate Item to current instance
            With Me
                .CoverageName = pCertficateItemCoverage.CoverageTypeId.ToDescription(pCommonManager, ListCodes.CoverageType, pLangauge)
                .CoverageTypeCode = pCertficateItemCoverage.CoverageTypeId.ToCode(pCommonManager, ListCodes.CoverageType, pLangauge)
                .BeginDate = pCertficateItemCoverage.BeginDate
                .EndDate = pCertficateItemCoverage.EndDate
            End With
        End Sub
    End Class
End Namespace