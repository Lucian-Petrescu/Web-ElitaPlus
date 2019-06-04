Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace SpecializedServices
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/DataContracts", Name:="ClaimInfoDC")> _
    Public Class CertClaimInfoResponseDC
#Region "DataMembers"

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CertInfo", Order:=1)> _
        Public Property CertificateInfo As CertificateDC

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ClaimInfo", Order:=1)> _
        Public Property ClaimInfo As IEnumerable(Of ClaimDC)

        <DataMember(Name:="ClaimExists", IsRequired:=True)> _
        Public Property ClaimExists As Boolean

        <DataMember(Name:="ClaimsCount", IsRequired:=True)> _
        Public Property ClaimsCount As Integer

#End Region
    End Class
End Namespace