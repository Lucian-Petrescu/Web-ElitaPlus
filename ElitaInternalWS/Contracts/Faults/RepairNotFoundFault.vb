Imports System.Runtime.Serialization

<DataContract(Name:="RepairNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class RepairNotFoundFault
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.RepairNotFound)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class
