Imports System.Runtime.Serialization

<DataContract(Name:="CoverageNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class CoverageNotFoundFault
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.CoverageNotFound)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class
