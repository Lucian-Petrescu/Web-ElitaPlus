Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetExtendedClaimStatusList", Name:="GetExtendedStatusesResponse")>
    Public Class GetExtendedStatusesResponse


        <DataMember>
        Public Property ExtendedStatuses As IEnumerable(Of ClaimExtendedStatusInfo)

    End Class
End Namespace
