Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Name:="InvalidRequestFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class InvalidRequestFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.InvalidRequest)
        End Sub
    End Class

End Namespace