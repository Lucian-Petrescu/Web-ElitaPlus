Public Interface IRemoteRoleProvider
    Function CreateRole(localSystemRoleId As Guid, roleName As String) As Guid
    Function UpdateRole(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String) As Guid
    Sub DeleteRole(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String)
    Sub Grant(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String, userId As Guid, networkId As String, userName As String)
    Sub Revoke(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String, userId As Guid, networkId As String, userName As String)
    Function SupportsCreateRole() As Boolean
    Function SupportsUpdateRole() As Boolean
    Function SupportsDeleteRole() As Boolean
    Function CreateUser(localSystemUserId As Guid, networkId As String, UserName As String, isActive As Boolean) As Guid
    Function UpdateUser(localSystemUserId As Guid, oldNetworkId As String, networkId As String, UserName As String, isActive As Boolean) As Guid
    Sub DeleteUser(localSystemUserId As Guid, networkId As String, UserName As String)
    Function SupportsCreateUser() As Boolean
    Function SupportsUpdateUser() As Boolean
    Function SupportsDeleteUser() As Boolean
    'DEF:3119 : Started
    Function GetRoleId(roleCode As String) As Guid
    'DEF:3119 : End
End Interface
