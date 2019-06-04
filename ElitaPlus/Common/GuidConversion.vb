Public Class GuidConversion

    Public Shared Function ConvertToString(ByVal oByteArray As Byte()) As String
        Return New Guid(oByteArray).ToString
    End Function

End Class
