Imports System.Runtime.Serialization
Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GoogleService/ComputeTax", Name:="ComputeTaxResponse")>
    Public Class ComputeTaxResponse
        <DataMember>
        Public Property TaxAmount As Decimal
    End Class
End Namespace