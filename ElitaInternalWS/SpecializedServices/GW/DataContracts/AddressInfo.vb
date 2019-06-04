
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

        Public Sub New(ByVal pAddress As Address, ByVal pCountryManager As ICountryManager)
            Me.StreetAddress1 = pAddress.Address1
            Me.StreetAddress2 = pAddress.Address2
            Me.StreetAddress3 = pAddress.Address3
            Me.City = pAddress.City
            Me.PostalCode = pAddress.PostalCode

            Dim cty As Country = pCountryManager.GetCountry(pAddress.CountryId)
            If Not cty Is Nothing Then
                Me.CountryCode = cty.Code
                If Not pAddress.RegionId Is Nothing Then
                    Dim reg As Region = cty.Regions.Where(Function(r) r.RegionId = pAddress.RegionId.GetValueOrDefault).FirstOrDefault
                    If Not reg Is Nothing Then
                        Me.StateRegion = reg.Description
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
