Imports System.Collections.Generic
Imports System.Runtime.Serialization


Namespace SpecializedServices.Abag
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetParts", Name:="GetPartsResponse")>
    Public Class GetPartsResponse

        <DataMember>
        Public Property PartInfo As IEnumerable(Of PartsDescriptionInfo)

    End Class
End Namespace