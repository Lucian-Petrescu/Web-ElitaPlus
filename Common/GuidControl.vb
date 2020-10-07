Public Class GuidControl

    Public Shared Function GuidToHexString(Value As Guid) As String
        Dim byteArray As Byte() = Value.ToByteArray
        Dim i As Integer
        Dim result As New System.Text.StringBuilder("")
        For i = 0 To byteArray.Length - 1
            Dim hexStr As String = byteArray(i).ToString("X")
            If hexStr.Length < 2 Then
                hexStr = "0" & hexStr
            End If
            result.Append(hexStr)
        Next
        Return result.ToString
    End Function

    Public Shared Function ByteArrayToGuid(Value As Object) As Guid
        Dim byteArray As Byte() = CType(Value, Byte())
        Return New Guid(CType(byteArray, Byte()))
    End Function

    'Returns a byte Array from an oracle formatted Hex String
    Public Shared Function HexToByteArray(hexString As String) As Byte()

        Dim intNumChars As Integer = hexString.Length
        Dim _bytes((intNumChars / 2) - 1) As Byte

        For i As Integer = 0 To intNumChars - 1 Step 2
            _bytes(i / 2) = Convert.ToByte(hexString.Substring(i, 2), 16)
        Next

        Return _bytes

    End Function
End Class
