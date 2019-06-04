Imports System.Runtime.Serialization
<DataContract(Name:="InvalidUpgradeDateFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidUpgradeDateFault

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup


End Class
