Imports System.Runtime.Serialization
<DataContract(Name:="InvalidCertificateNumber", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidCertificateNumber

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class
