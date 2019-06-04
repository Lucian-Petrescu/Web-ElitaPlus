Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetExtendedClaimStatusList", Name:="GetExtendedStatusesRequest")>
    Public Class GetExtendedStatusesRequest

        <DataMember(IsRequired:=True, Name:="CompanyGroupCode"), Required()>
        Public Property CompanyGroupCode As String

    End Class
End Namespace
