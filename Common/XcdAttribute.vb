Public Class XcdAttribute
    Inherits Attribute
    Private Value As String
    Private List As String

    Public Sub New(ListCode As String, ValueCode As String)
        Value = ValueCode
        List = ListCode
    End Sub
    Public ReadOnly Property XcdValue() As String
        Get
            Return $"{List}-{Value}"
        End Get
    End Property
    Public Function Match(Value As String) As Boolean
        If String.IsNullOrEmpty(Value) Then
            Return False
        Else
            Return String.Compare(Value, Me.Value, StringComparison.InvariantCultureIgnoreCase) = 0
        End If
    End Function

    Public Function MatchXcd(Value As String)
        If String.IsNullOrEmpty(Value) Then
            Return False
        Else
            Return String.Compare(Value, XcdValue, StringComparison.InvariantCultureIgnoreCase) = 0
        End If
    End Function
End Class
