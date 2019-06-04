Public Class TimbClaimContext
    Inherits BasedSpecializedClaimContext

    Public Sub New()
        MyBase.New()
    End Sub
    Protected Overrides ReadOnly Property CustomizationName As String
        Get
            Return "TIMB"
        End Get
    End Property
End Class
