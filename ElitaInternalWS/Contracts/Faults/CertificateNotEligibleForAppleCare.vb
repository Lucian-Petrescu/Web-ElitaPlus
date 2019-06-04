Imports System.Runtime.Serialization

<DataContract(Name:="CertificateNotEligibleForAppleCare", Namespace:="http://elita.assurant.com/Faults")>
Public Class CertificateNotEligibleForAppleCare
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.CertificateNotEligibleForAppleCare)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup
End Class
