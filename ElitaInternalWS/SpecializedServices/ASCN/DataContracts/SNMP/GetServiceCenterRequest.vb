Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace SpecializedServices.Ascn

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenter", Name:="GetServiceCenterRequest")>
    Public Class GetServiceCenterRequest

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"), Required()>
        Public Property ServiceCenterCode As String

    End Class
End Namespace