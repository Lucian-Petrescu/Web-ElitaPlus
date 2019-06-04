Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="SearchCertByTaxIdRequest")>
    Public Class SearchCertificateByTaxIdRequest
        <DataMember(IsRequired:=True, Name:="CountryCode")>
        Public Property CountryCodes As String

        <DataMember(IsRequired:=True, Name:="TaxId")>
        Public Property IdentificationNumber As String

        <DataMember(IsRequired:=False, Name:="WorkPhone"), StringLength(15, MinimumLength:=0)>
        Public Property WorkPhone As String

        <DataMember(IsRequired:=False, Name:="SearchResultFilters")>
        Public Property Filters As IEnumerable(Of SearchFilter)

        <DataMember(IsRequired:=False, Name:="Language")>
        Public Property Language As String

        <DataMember(IsRequired:=False, Name:="NumberOfReocrds"), Range(1, 100)>
        Public Property NumberOfReocrds As Nullable(Of Integer)
    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="CertSearchFilter")>
    Public Class SearchFilter
        <DataMember(IsRequired:=False, Name:="CertificateStatus", EmitDefaultValue:=False)>
        Public Property CertificateStatus As String

        <DataMember(IsRequired:=False, Name:="HasActiveCoverage", EmitDefaultValue:=False)>
        Public Property HasActiveCoverage As Nullable(Of Boolean)

        <DataMember(IsRequired:=False, Name:="HasActiveClaims", EmitDefaultValue:=False)>
        Public Property HasActiveClaims As Nullable(Of Boolean)

        <DataMember(IsRequired:=False, Name:="CoverageExpiredAfter", EmitDefaultValue:=False)>
        Public Property CoverageExpiredAfter As Nullable(Of Date)
    End Class
End Namespace