Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GoogleService/ChangeServiceCenter", Name:="ChangeServiceCenterRequest")>
    Public Class ChangeServiceCenterRequest
        Inherits BaseClaimRequest

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"), StringLength(10)>
        Public Property ServiceCenterCode As String

    End Class
End Namespace