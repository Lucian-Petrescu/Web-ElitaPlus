Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/CreateClaim", Name:="CreateServiceWarrantyClaimRequest")>
    Public Class CreateServiceWarrantyClaimRequest
        <DataMember(IsRequired:=True, Name:="ClaimNumber"), Required(), StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

        <DataMember(IsRequired:=True, Name:="CompanyCode"), StringLength(5, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=False, Name:="ExtendedStatuses")>
        Public Property ExtendedStatuses As List(Of ExtendedStatus)
    End Class
End Namespace