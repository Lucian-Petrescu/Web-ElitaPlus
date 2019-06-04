Imports System.Runtime.Serialization


Namespace SpecializedServices.Ascn
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetParts", Name:="GetPartsRequest")>
    Public Class GetPartsRequest

        <DataMember(IsRequired:=True)>
        Public Property RiskGroupCode As String

    End Class

End Namespace