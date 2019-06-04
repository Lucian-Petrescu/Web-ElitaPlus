
Public Class WorkQueueAction
    Public Enum TargetPage
        NewClaimForm
        ClaimDetails
        ImageIndex
    End Enum

    Property Target() As TargetPage
        Get

        End Get
        Set(ByVal value As TargetPage)

        End Set
    End Property

End Class
