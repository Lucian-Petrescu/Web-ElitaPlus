
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ChileSamsungUpgradeService/GetCertGrossAmtByProdCode", Name:="GetCertGrossAmtByProdCodeRequst")>
    Public Class GetCertGrossAmtByProdCodeRequst

        <DataMember(IsRequired:=True)>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True)>
        Public Property DealerCode As String


    End Class
End Namespace