Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetServiceCenterClaims", Name:="GetServiceCenterClaimsResponse")>
    Public Class GetServiceCenterClaimsResponse

        <DataMember>
        Public Property ServiceCenterClaims As IEnumerable(Of ServiceCenterClaimsInfo)

        <DataMember>
        Public Property TotalRecordCount As String

    End Class

End Namespace