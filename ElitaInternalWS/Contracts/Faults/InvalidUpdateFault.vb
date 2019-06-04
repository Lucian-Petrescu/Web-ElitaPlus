Imports System.Runtime.Serialization

<DataContract(Name:="InvalidUpdateFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidUpdateFault
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.InvalidUpdateAction)
    End Sub
End Class
