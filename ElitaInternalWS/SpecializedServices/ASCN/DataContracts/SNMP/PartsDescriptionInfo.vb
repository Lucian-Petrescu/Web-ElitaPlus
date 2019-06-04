Imports System.Runtime.Serialization

Namespace SpecializedServices.Ascn

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/PartsDescriptionInfo", Name:="PartsDescriptionInfo")>
    Public Class PartsDescriptionInfo

        <DataMember>
        Public Property RiskGroup As String

        <DataMember>
        Public Property Description As String

        <DataMember>
        Public Property EnglishDescription As String

        <DataMember>
        Public Property Code As String

    End Class
End Namespace