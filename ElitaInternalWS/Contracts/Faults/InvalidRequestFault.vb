Imports System.Runtime.Serialization


<DataContract(Name:="InvalidRequestFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidRequestFault
    Inherits ElitaFault

    Public Sub New()
        MyBase.New(EnumFaultType.InvalidRequest2)
    End Sub
End Class