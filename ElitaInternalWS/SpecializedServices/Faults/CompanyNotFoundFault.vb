Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Name:="CompanyNotFoundFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class CompanyNotFoundFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.CompanyNotFound)
        End Sub
    End Class
End Namespace

