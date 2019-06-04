
Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Name:="InvalidCountryFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")>
    Public Class RequiredFieldMissingFault
        Inherits ElitaFault

        Public Sub New()
            MyBase.New(EnumFaultType.InvalidRequestMissingRequiredField)
        End Sub

    End Class
End Namespace