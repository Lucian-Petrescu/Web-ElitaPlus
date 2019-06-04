
Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Name:="CertificateNotFoundFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class CertificateNotFoundFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.CertificateNotFound)
        End Sub

    End Class
End Namespace

