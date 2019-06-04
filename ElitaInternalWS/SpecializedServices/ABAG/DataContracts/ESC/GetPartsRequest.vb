Imports System.Runtime.Serialization


Namespace SpecializedServices.Abag
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetParts", Name:="GetPartsRequest")>
    Public Class GetPartsRequest

        <DataMember(IsRequired:=True)>
        Public Property RiskGroupCode As String

    End Class

End Namespace