Public Class GuidConversion

    Public Shared Function ConvertToString(oByteArray As Byte()) As String
        Return New Guid(oByteArray).ToString
    End Function

End Class
