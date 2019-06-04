Imports System.Runtime.Serialization

<DataContract(Name:="RequestWasNotSuccessFull", Namespace:="http://elita.assurant.com/Faults")>
Public Class RequestWasNotSuccessFull
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.RequestWasNotSuccessFull)
    End Sub
End Class
