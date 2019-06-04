Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/PartsListInfo", Name:="PartsListInfo")>
    Public Class PartsListInfo
        <DataMember>
        Public Property Description As String

        <DataMember>
        Public Property InStockDescription As String

        <DataMember>
        Public Property Amount As Double?

        <DataMember>
        Public Property Code As String

        <DataMember>
        Public Property StockCode As String

    End Class

End Namespace