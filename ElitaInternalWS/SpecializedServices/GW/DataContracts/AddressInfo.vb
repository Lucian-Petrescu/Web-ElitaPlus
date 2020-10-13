
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Business

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="Address")>
    Public Class AddressInfo
        <DataMember(IsRequired:=False, Name:="StreetAddress1", Order:=1, EmitDefaultValue:=False)>
        Public Property StreetAddress1 As String

        <DataMember(IsRequired:=False, Name:="StreetAddress2", Order:=2, EmitDefaultValue:=False)>
        Public Property StreetAddress2 As String

        <DataMember(IsRequired:=False, Name:="StreetAddress3", Order:=3, EmitDefaultValue:=False)>
        Public Property StreetAddress3 As String

        <DataMember(IsRequired:=False, Name:="City", Order:=4, EmitDefaultValue:=False)>
        Public Property City As String

        <DataMember(IsRequired:=False, Name:="StateRegion", Order:=5, EmitDefaultValue:=False)>
        Public Property StateRegion As String

        <DataMember(IsRequired:=False, Name:="PostalCode", Order:=6, EmitDefaultValue:=False)>
        Public Property PostalCode As String

        <DataMember(IsRequired:=False, Name:="CountryCode", Order:=7, EmitDefaultValue:=False)>
        Public Property CountryCode As String

        Public Sub New()
        End Sub

        Public Sub New(pAddress As Address, pCountryManager As ICountryManager)
            StreetAddress1 = pAddress.Address1
            StreetAddress2 = pAddress.Address2
            StreetAddress3 = pAddress.Address3
            City = pAddress.City
            PostalCode = pAddress.PostalCode

            Dim cty As Country = pCountryManager.GetCountry(pAddress.CountryId)
            If cty IsNot Nothing Then
                CountryCode = cty.Code
                If pAddress.RegionId IsNot Nothing Then
                    Dim reg As Region = cty.Regions.Where(Function(r) r.RegionId = pAddress.RegionId.GetValueOrDefault).FirstOrDefault
                    If reg IsNot Nothing Then
                        StateRegion = reg.Description
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
