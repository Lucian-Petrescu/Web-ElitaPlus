
Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Name:="InvalidCountryFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class InvalidCountryFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.CountryNotFound)
        End Sub

    End Class
End Namespace