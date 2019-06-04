Public Interface IRemoteRoleProvider
    Function CreateRole(ByVal localSystemRoleId As Guid, ByVal roleName As String) As Guid
    Function UpdateRole(ByVal localSystemRoleId As Guid, ByVal remoteSystemRoleId As Guid, ByVal roleName As String) As Guid
    Sub DeleteRole(ByVal localSystemRoleId As Guid, ByVal remoteSystemRoleId As Guid, ByVal roleName As String)
    Sub Grant(ByVal localSystemRoleId As Guid, ByVal remoteSystemRoleId As Guid, ByVal roleName As String, ByVal userId As Guid, ByVal networkId As String, ByVal userName As String)
    Sub Revoke(ByVal localSystemRoleId As Guid, ByVal remoteSystemRoleId As Guid, ByVal roleName As String, ByVal userId As Guid, ByVal networkId As String, ByVal userName As String)
    Function SupportsCreateRole() As Boolean
    Function SupportsUpdateRole() As Boolean
    Function SupportsDeleteRole() As Boolean
    Function CreateUser(ByVal localSystemUserId As Guid, ByVal networkId As String, ByVal UserName As String, ByVal isActive As Boolean) As Guid
    Function UpdateUser(ByVal localSystemUserId As Guid, ByVal oldNetworkId As String, ByVal networkId As String, ByVal UserName As String, ByVal isActive As Boolean) As Guid
    Sub DeleteUser(ByVal localSystemUserId As Guid, ByVal networkId As String, ByVal UserName As String)
    Function SupportsCreateUser() As Boolean
    Function SupportsUpdateUser() As Boolean
    Function SupportsDeleteUser() As Boolean
    'DEF:3119 : Started
    Function GetRoleId(ByVal roleCode As String) As Guid
    'DEF:3119 : End
End Interface
