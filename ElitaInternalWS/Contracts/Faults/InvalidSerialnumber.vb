Imports System.Runtime.Serialization


<DataContract(Name:="InvalidSerialNumber", Namespace:="http://elita.assurant.com/Faults")> _
Public Class InvalidSerialnumber
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.InvalidSerialNumber)
    End Sub
    <DataMember(Name:="CertificateSearch", IsRequired:=True)> _
    Public Property CertificateSearch As CertificateLookup


End Class
