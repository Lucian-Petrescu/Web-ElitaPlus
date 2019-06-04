
Imports System.Runtime.Serialization


<DataContract(Name:="EmailandPostalCodeNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class EmailandPostalCodeNotFoundFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.EmailandPostalCodeNotFoundFault)
        End Sub

    End Class
