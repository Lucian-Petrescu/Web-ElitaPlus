Imports System.Runtime.Serialization

<DataContract(Name:="CertificateNotEligibleForUpgrade", Namespace:="http://elita.assurant.com/Faults")>
Public Class CertificateNotEligibleForUpgrade

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class


