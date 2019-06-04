Imports System.Runtime.Serialization

Namespace SpecializedServices.Ascn

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/ClaimCommentList", Name:="ClaimCommentList")>
    Public Class ClaimCommentList

        <DataMember>
        Public Property Comments As String

        <DataMember>
        Public Property CommentDate As Date?

    End Class
End Namespace