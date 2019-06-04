Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace SpecializedServices.Abag

     <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetPriceList", Name:="GetPriceListDetailResponse")>
    Public Class GetPriceListDetailResponse

        <DataMember>
        Public Property PricesDetails As IEnumerable(Of PriceDetailInfo)

    End Class
End Namespace