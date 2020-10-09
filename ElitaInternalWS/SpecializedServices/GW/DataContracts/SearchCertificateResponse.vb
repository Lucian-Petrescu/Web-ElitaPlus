Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificate", Name:="SearchCertificateResponse")>
    Public Class SearchCertificateResponse
        <DataMember(IsRequired:=False, Name:="Language")>
        Public Property Language As String

        <DataMember(Name:="Certificates")>
        Public Property Certificates As IEnumerable(Of SearchResultCertificateInfo)

        Friend Sub Populate(pSearchResult As Collections.Generic.IEnumerable(Of Certificate),
                            ByRef pCertManager As ICertificateManager,
                            ByRef pCompanyGroupManager As ICompanyGroupManager,
                            ByRef pcompanyManager As ICompanyManager,
                            ByRef pDealerManager As IDealerManager,
                            ByRef pAddressManager As IAddressManager,
                            ByRef pCountryManager As ICountryManager,
                            ByRef pEquipmentManager As EquipmentManager,
                            ByRef pCommonManager As ICommonManager,
                            pLanguage As String,
                            Optional ByVal pCurrency As Currency = Nothing)

            Certificates = New List(Of SearchResultCertificateInfo)()

            For Each cert As Certificate In pSearchResult
                DirectCast(Certificates, IList(Of SearchResultCertificateInfo)).Add(New SearchResultCertificateInfo(cert, pCertManager, pCompanyGroupManager, pcompanyManager, pDealerManager, pCountryManager, pAddressManager, pCommonManager, pEquipmentManager, pLanguage))
            Next

        End Sub
    End Class

End Namespace