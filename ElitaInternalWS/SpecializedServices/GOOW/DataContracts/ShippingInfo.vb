Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/ShippingInfo", Name:="ShippingInfo")>
    Public Class ShippingInfo
        <DataMember>
        Public Property Address1 As String

        <DataMember>
        Public Property Address2 As String

        <DataMember>
        Public Property City As String

        <DataMember>
        Public Property State As String

        <DataMember>
        Public Property PostalCode As String

    End Class
End Namespace