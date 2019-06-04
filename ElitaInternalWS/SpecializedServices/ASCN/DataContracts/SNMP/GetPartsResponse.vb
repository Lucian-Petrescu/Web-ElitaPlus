Imports System.Collections.Generic
Imports System.Runtime.Serialization


Namespace SpecializedServices.Ascn
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetParts", Name:="GetPartsResponse")>
    Public Class GetPartsResponse

        <DataMember>
        Public Property PartInfo As IEnumerable(Of PartsDescriptionInfo)

    End Class
End Namespace