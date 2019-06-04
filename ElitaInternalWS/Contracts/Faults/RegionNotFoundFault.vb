
Imports System.Runtime.Serialization


<DataContract(Name:="RegionNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class RegionNotFoundFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.RegionNotFound)
        End Sub

    End Class
