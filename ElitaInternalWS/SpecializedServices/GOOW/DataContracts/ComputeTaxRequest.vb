Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GoogleService/ComputeTax", Name:="ComputeTaxRequest")>
    Public Class ComputeTaxRequest

        <DataMember(IsRequired:=True, Name:="DealerCode", Order:=1), Required()>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CountryCode", Order:=2), Required()>
        Public Property CountryCode As String

        <DataMember(IsRequired:=True, Name:="Amount", Order:=3), Required()>
        Public Property Amount As Decimal

    End Class
End Namespace