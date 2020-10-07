Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Runtime.Serialization
Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the class name "RegionAndComunaService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select RegionAndComunaService.svc or RegionAndComunaService.svc.vb at the Solution Explorer and start debugging.
Namespace GeographicServices

    <ServiceBehavior(Namespace:="http://elita.assurant.com/GeographicServices")>
    Public Class RegionAndComunaService
        Implements IRegionAndComunaService

        Public Function GetRegionsAndComunasInfo(request As GetRegionsAndComunasInfoRequest) As GetRegionsAndComunasInfoResponse Implements IRegionAndComunaService.GetRegionsAndComunasInfo
            'Get user country
            Dim CountriesDV As DataView
            CountriesDV = LookupListNew.GetUserCountriesLookupList
            Dim RegionsAndComunasDV As DataView
            Dim countryId As Guid

            If (CountriesDV.Count = 0) Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found")
            End If

            countryId = New Guid(CType(CountriesDV.Item(0).Item("ID"), Byte()))
            RegionsAndComunasDV = Region.GetRegionsAndComunas(countryId, request.RegionCode)
            Dim response As GetRegionsAndComunasInfoResponse = New GetRegionsAndComunasInfoResponse(RegionsAndComunasDV)
            Return response

        End Function



    End Class
End Namespace
