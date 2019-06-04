Imports System.Runtime.Serialization

<DataContract(Name:="CertificateNotFound", Namespace:="http://elita.assurant.com/Faults")> _
Public Class CertificateNotFound
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.CertificateNotFound)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)> _
    Public Property CertificateSearch As CertificateLookup

End Class
