Imports System.Runtime.Serialization

<DataContract(Name:="DuplicateCertFound", Namespace:="http://elita.assurant.com/Faults")>
Public Class DuplicateCertFound
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.DuplicateCertificateFound)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class
