Imports System.Runtime.Serialization

<DataContract(Name:="ServiceOrderNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class ServiceOrderNotFoundFault
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.ServiceOrderNotFound)
    End Sub
End Class
