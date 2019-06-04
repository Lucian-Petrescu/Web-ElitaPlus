Imports System.Runtime.Serialization

<DataContract(Name:="InvalidImeiNumber", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidImeiNumber
    Inherits ElitaFault

    Public Sub New()
        MyBase.New(EnumFaultType.InvalidImeiNumber)
    End Sub

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

End Class
