
Imports System.Runtime.Serialization


<DataContract(Name:="CountryNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class CountryNotFoundFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.CountryNotFound)
        End Sub

    End Class
