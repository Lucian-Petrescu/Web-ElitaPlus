
Imports System.Runtime.Serialization
Namespace SpecializedServices
    <DataContract(Name:="DatabaseErrorFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class DatabaseErrorFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.Database)
        End Sub
    End Class
End Namespace
