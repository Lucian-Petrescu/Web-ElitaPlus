Imports System.Runtime.Serialization

<DataContract(Name:="GetCertificateDocumentsRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class GetCertificateDocumentsRequest

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup
End Class
