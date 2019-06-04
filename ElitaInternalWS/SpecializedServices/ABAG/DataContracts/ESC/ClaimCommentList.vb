Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/ClaimCommentList", Name:="ClaimCommentList")>
    Public Class ClaimCommentList

        <DataMember>
        Public Property Comments As String

        <DataMember>
        Public Property CommentDate As Date?

    End Class
End Namespace