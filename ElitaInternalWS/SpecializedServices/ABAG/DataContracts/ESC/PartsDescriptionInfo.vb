Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/PartsDescriptionInfo", Name:="PartsDescriptionInfo")>
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