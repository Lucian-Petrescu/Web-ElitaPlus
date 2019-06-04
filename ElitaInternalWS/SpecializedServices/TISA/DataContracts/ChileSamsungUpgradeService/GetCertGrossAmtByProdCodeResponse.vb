Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ChileSamsungUpgradeService/GetCertGrossAmtByProdCode", Name:="GetCertGrossAmtByProdCodeResponse")>
    Public Class GetCertGrossAmtByProdCodeResponse
        <DataMember>
        Public Property GrossAmount As Decimal

        <DataMember>
        Public Property CurrencyCode As String

    End Class
End Namespace
