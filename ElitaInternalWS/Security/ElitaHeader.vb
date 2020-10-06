Imports System.Runtime.Serialization

Namespace Security
    <DataContract(Namespace:=ElitaHeader.NamespaceName)>
    Public Class ElitaHeader

        Public Const NamespaceName As String = "http://elita.assurant.com/ElitaHeader"

        Public Const LocalName As String = "ElitaHeader"

        <DataMember(Name:="NetworkId", IsRequired:=True, Order:=1)> _
        Public Property NetworkId As String

        <DataMember(Name:="Password", IsRequired:=True, Order:=2)> _
        Public Property Password As String

        <DataMember(Name:="Group", IsRequired:=True, Order:=3)> _
        Public Property Group As String

        Friend Property Token As String

        Friend Function GetNetworkId() As String
            Return Decrypt(NetworkId)
        End Function

        Friend Function GetPassword() As String
            Return Decrypt(Password)
        End Function

        Friend Function GetGroup() As String
            Return Decrypt(Group)
        End Function

        Private Function Decrypt(input As String) As String
            Try
                Dim buffer As Byte() = Convert.FromBase64String(input)
                Return System.Text.Encoding.UTF8.GetString(buffer)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function
    End Class
End Namespace
