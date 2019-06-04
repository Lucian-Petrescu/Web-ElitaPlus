Imports Assurant.ElitaPlus.Common

Friend Module TestUtility
    Friend Sub Login()
        Dim oracleParameter As New Oracle.ManagedDataAccess.Client.OracleParameter
        Dim oAuthentication As Assurant.ElitaPlus.BusinessObjectsNew.Authentication = New Assurant.ElitaPlus.BusinessObjectsNew.Authentication()
        oAuthentication.CreatePrincipalForServices(AppConfig.DevEnvUserId, "NO", "NONE")
        Assurant.ElitaPlus.BusinessObjectsNew.Authentication.SetCulture()
    End Sub

    Friend Function OracleToDotNet(text As String) As String

        Dim bytes() As Byte = ParseHex(text)
        Dim guid As Guid = New Guid(bytes)
        Return guid.ToString("N").ToUpperInvariant()

    End Function

    Friend Function DotNetToOracle(text As String) As String

        Dim guid As Guid = New Guid(text)
        Return BitConverter.ToString(guid.ToByteArray()).Replace("-", String.Empty).Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-")
    End Function


    Friend Function ParseHex(text As String) As Byte()

        Dim ret() As Byte = New Byte(text.Length / 2) {}
        For i As Integer = 0 To ret.Length - 1
            ret(i) = Convert.ToByte(text.Substring(i * 2, 2), 16)
        Next

        Return ret
    End Function
End Module
