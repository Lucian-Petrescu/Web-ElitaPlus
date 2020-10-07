Imports System
Imports System.Xml
Imports System.Security.Permissions

Imports Microsoft.Web.Services3.Security
Imports Microsoft.Web.Services3.Security.Tokens

'Namespace Security


' <summary>
' By implementing UsernameTokenManager we can verify the signature
' on messages received.
' </summary>
' <remarks>
' This class includes this demand to ensure that any untrusted
' assemblies cannot invoke this code. This helps mitigate
' brute-force discovery attacks.
' </remarks>
<SecurityPermissionAttribute(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
Public Class CustomUsernameTokenManager
    Inherits UsernameTokenManager

    ' <summary>
    ' Constructs an instance of this security token manager.
    ' </summary>
    Public Sub New()

    End Sub

    ' <summary>
    ' Constructs an instance of this security token manager.
    ' </summary>
    ' <param name="nodes">An XmlNodeList containing XML elements from a configuration file.</param>
    Public Sub New(nodes As XmlNodeList)
        MyBase.New(nodes)
    End Sub

    '' <summary>
    '' Returns the password or password equivalent for the username provided.
    '' </summary>
    '' <param name="token">The username token</param>
    '' <returns>The password (or password equivalent) for the username</returns>
    'Protected Overrides Function AuthenticateToken(ByVal token As UsernameToken) As String
    '    ' This is a very simple manager.
    '    ' In most production systems the following code
    '    ' typically consults an external database of (username,password) pairs where
    '    ' the password is often not the real password but a password equivalent
    '    ' (for example, the hash of the password). Provided that both client and
    '    ' server can generate the same value for a particular username, there is
    '    ' no requirement that the password be the actual password for the user.
    '    ' For this sample the password is simply the reverse of the username.
    '    ' Dim password As Byte() = System.Text.Encoding.UTF8.GetBytes(token.Password)
    '    'Dim password As Byte() = System.Text.Encoding.UTF8.GetBytes(token.Username)

    '    'Array.Reverse(password)
    '    ''  Dim hi As String = System.Text.Encoding.UTF8.GetString(password)
    '    'Return Convert.ToBase64String(password)

    '    Dim password As Byte() = System.Text.Encoding.UTF8.GetBytes(token.Username)

    '    Array.Reverse(password)

    '    Return Convert.ToBase64String(password)
    'End Function 'AuthenticateToken

    ' <summary>
    ' Returns the password or password equivalent for the username provided.
    ' </summary>
    ' <param name="token">The username token</param>
    ' <returns>The password (or password equivalent) for the username</returns>
    Protected Overrides Function AuthenticateToken(token As UsernameToken) As String
        ' This is a very simple manager.
        ' In most production systems the following code
        ' typically consults an external database of (username,password) pairs where
        ' the password is often not the real password but a password equivalent
        ' (for example, the hash of the password). Provided that both client and
        ' server can generate the same value for a particular username, there is
        ' no requirement that the password be the actual password for the user.
        ' For this sample the password is simply the reverse of the username.
        ' Dim password As Byte() = System.Text.Encoding.UTF8.GetBytes(token.Password)
        'Dim password As Byte() = System.Text.Encoding.UTF8.GetBytes(token.Username)

        'Array.Reverse(password)
        ''  Dim hi As String = System.Text.Encoding.UTF8.GetString(password)
        'Return Convert.ToBase64String(password)

        Dim password As String = token.Password



        Return password
    End Function 'AuthenticateToken

End Class

'End Namespace


