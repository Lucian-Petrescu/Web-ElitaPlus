Imports System.Runtime.Serialization

Namespace SpecializedServices.Ascn

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/PartsListInfo", Name:="PartsListInfo")>
    Public Class PartsListInfo

        <IgnoreDataMember>
        Public Property Id As Guid

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