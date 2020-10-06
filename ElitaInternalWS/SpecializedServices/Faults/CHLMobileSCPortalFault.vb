Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Name:="CHLMobileSCPortalFault", Namespace:="http://elita.assurant.com/SpecializedServices/Faults")> _
    Public Class CHLMobileSCPortalFault

#Region "DataMembers"
        Private _EnglishReason As String

        <DataMember(Name:="EnglishReason", IsRequired:=False)> _
        Public Property EnglishReason() As String
            Get
                Return _EnglishReason
            End Get
            Set(value As String)
                _EnglishReason = value
            End Set
        End Property

#End Region

    End Class
End Namespace

