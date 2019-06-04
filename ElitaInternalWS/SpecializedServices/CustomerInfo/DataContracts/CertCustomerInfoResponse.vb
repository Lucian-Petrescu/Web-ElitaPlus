Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="CertCustomerInfoResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertCustomerInfoResponse

    <DataMember(Name:="CertCustInfo", IsRequired:=False)>
    Public Property CertCustInfo As CertCustInfo

End Class