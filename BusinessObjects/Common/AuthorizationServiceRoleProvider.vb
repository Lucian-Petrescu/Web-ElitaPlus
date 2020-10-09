Imports System.ServiceModel

Public Class AuthorizationServiceRoleProvider
    Inherits BaseRoleProvider
    Implements IRemoteRoleProvider

    Private Shared oAuthorizationServiceClient As Auth.AuthorizationClient
    Private Shared syncRoot As New Object

    Private Shared ReadOnly Property AuthorizationClientProxy As Auth.AuthorizationClient
        Get
            Dim authClient As Auth.AuthorizationClient
            If (oAuthorizationServiceClient Is Nothing OrElse oAuthorizationServiceClient.State <> CommunicationState.Opened) Then
                SyncLock syncRoot
                    If (oAuthorizationServiceClient Is Nothing OrElse oAuthorizationServiceClient.State <> CommunicationState.Opened) Then
                        oAuthorizationServiceClient = ServiceHelper.CreateAuthorizationClient()
                    End If
                End SyncLock
            End If
            Return oAuthorizationServiceClient
        End Get
    End Property


    Friend Sub New()
        ' Firend Constructor restricts Creation of Class outside assembly
    End Sub

    'Added for DEF:3119
    Function GetRoleId(roleCode As String) As Guid Implements IRemoteRoleProvider.GetRoleId
        Dim remoteRoles As Auth.Group()
        Dim remoteRole As Auth.Group
        Dim count As Integer
        Try
            'Get Roles information using AuthorizationClientProxy
            remoteRoles = AuthorizationClientProxy.GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME)
            'Get ID from Role Name 

            count = (From r In remoteRoles Where r.Name.ToUpper().Trim() = roleCode.ToUpper().Trim() Select r).Count
            If (count > 0) Then ' If data available for passed code 
                remoteRole = (From r In remoteRoles Where r.Name.ToUpper().Trim() = roleCode.ToUpper().Trim() Select r).First()
                If (remoteRole IsNot Nothing) Then
                    Return remoteRole.Id
                End If
            End If

            Return Guid.Empty ' If Roleid not available for passed Code, return GUID.Empty

        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "GetRoleID", ex)
        Catch ex As FaultException(Of Auth.NotFoundFault)
            Throw
        End Try
    End Function
    'DEF:3119 Completed 

#Region "Role"
    Function CreateRole(localSystemRoleId As Guid, roleName As String) As Guid Implements IRemoteRoleProvider.CreateRole
        Dim remoteRole As Auth.Group
        Dim remoteRoles As Auth.Group()
        Dim count As Integer

        ' Check if Role existis on Remote System, if so then Re-Use
        Try
            remoteRoles = AuthorizationClientProxy.GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME)
        Catch ex As FaultException(Of Auth.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "GetGroups", ex)
        End Try

        count = (From r In remoteRoles Where r.Name.ToUpper() = roleName.ToUpper() Select r).Count()
        If (count = 1) Then
            remoteRole = (From r In remoteRoles Where r.Name.ToUpper() = roleName.ToUpper() Select r).First()
            remoteRole.Name = roleName

            Try
                remoteRole = AuthorizationClientProxy.UpdateGroup(remoteRole)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteRole)
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "UpdateGroup", ex)
            End Try

        ElseIf (count = 0) Then
            remoteRole = New Auth.Group
            remoteRole.Name = roleName

            Try
                remoteRole = AuthorizationClientProxy.CreateGroup(remoteRole, ServiceHelper.WORKQUEUE_SERVICE_NAME)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteRole)
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "CreateGroup", ex)
            End Try

        Else
            Throw New BOInvalidOperationException("More than 1 Roles found on Remote System with same Name")
        End If
        Return remoteRole.Id

    End Function

    Function UpdateRole(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String) As Guid Implements IRemoteRoleProvider.UpdateRole
        Dim remoteRole As Auth.Group
        Dim groups As Auth.Group()

        Try
            groups = AuthorizationClientProxy.GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME)
        Catch ex As FaultException(Of Auth.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "GetGroups", ex)
        End Try

        remoteRole = (From grp As Auth.Group In groups Where grp.Id = remoteSystemRoleId Select grp).FirstOrDefault()
        If (remoteRole Is Nothing) Then
            Throw New BOInvalidOperationException("Unable to find Role on Remote System")
        End If
        remoteRole.Name = roleName
        Try
            remoteRole = AuthorizationClientProxy.UpdateGroup(remoteRole)
        Catch ex As FaultException(Of Auth.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.NotFoundFault)
            Throw
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "UpdateGroup", ex)
        End Try

        Return remoteRole.Id
    End Function

    Sub DeleteRole(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String) Implements IRemoteRoleProvider.DeleteRole
        Try
            Dim oGroupExt As Auth.ExtendedGroup
            Try
                oGroupExt = AuthorizationClientProxy.GetGroup(remoteSystemRoleId)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "GetGroup", ex)
            End Try

            If (oGroupExt.Permissions IsNot Nothing AndAlso oGroupExt.Permissions.Length > 0) Then
                Try
                    AuthorizationClientProxy.RemovePermissionsFromGroup((From per In oGroupExt.Permissions Select per.Id).ToArray(), oGroupExt.Id)
                Catch ex As FaultException(Of Auth.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of Auth.AuthorizationFault)
                    Throw New ServiceException("Authorization", "RemovePermissionsFromGroup", ex)
                End Try
            End If
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw
        End Try
    End Sub

    

    Function SupportsCreateRole() As Boolean Implements IRemoteRoleProvider.SupportsCreateRole
        Return True
    End Function

    Function SupportsUpdateRole() As Boolean Implements IRemoteRoleProvider.SupportsUpdateRole
        Return True
    End Function

    Function SupportsDeleteRole() As Boolean Implements IRemoteRoleProvider.SupportsDeleteRole
        Return True
    End Function
#End Region

#Region "Grant/Revoke"
    Sub Grant(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String, userId As Guid, networkId As String, userName As String) Implements IRemoteRoleProvider.Grant
        Dim users As Auth.User()
        Dim count As Integer
        Dim remoteUserId As Guid
        Dim userQuery As Auth.User
        userQuery = New Auth.User()
        userQuery.UserId = networkId
        Try
            users = AuthorizationClientProxy.FindUsers(userQuery)
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "FindUsers", ex)
        End Try

        count = users.Length
        If (count = 1) Then
            remoteUserId = users(0).Id
        ElseIf (count = 0) Then
            remoteUserId = CreateUser(userId, networkId, userName, True)
        Else
            Throw New BOInvalidOperationException("More than 1 Users found on Remote System with same Network ID")
        End If

        Try
            AuthorizationClientProxy.AddUsersToGroup(New Guid() {remoteUserId}, remoteSystemRoleId)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "AddUsersToGroup", ex)
        End Try
    End Sub

    Sub Revoke(localSystemRoleId As Guid, remoteSystemRoleId As Guid, roleName As String, userId As Guid, networkId As String, userName As String) Implements IRemoteRoleProvider.Revoke
        Dim users As Auth.User()
        Dim count As Integer
        Dim remoteUserId As Guid
        Dim userQuery As Auth.User
        userQuery = New Auth.User()
        userQuery.UserId = networkId
        Try
            users = AuthorizationClientProxy.FindUsers(userQuery)
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "FindUsers", ex)
        End Try

        count = users.Length
        If (count = 1) Then
            remoteUserId = users(0).Id

            Try
                AuthorizationClientProxy.RemoveUsersFromGroup(New Guid() {remoteUserId}, remoteSystemRoleId)
            Catch ex As FaultException(Of WrkQueue.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "RemoveUsersFromGroup", ex)
            End Try

        ElseIf (count = 0) Then
            Return
        Else
            Throw New BOInvalidOperationException("More than 1 Users found on Remote System with same Network ID")
        End If
    End Sub
#End Region

#Region "User"
    Function CreateUser(localSystemUserId As Guid, networkId As String, UserName As String, isActive As Boolean) As Guid Implements IRemoteRoleProvider.CreateUser
        Dim remoteUser As Auth.User
        Dim remoteUsers As Auth.User()
        Dim count As Integer
        Dim userQuery As Auth.User
        userQuery = New Auth.User()
        userQuery.UserId = networkId
        ' Check if User existis on Remote System, if so then Re-Use
        Try
            remoteUsers = AuthorizationClientProxy.FindUsers(userQuery)
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "FindUsers", ex)
        End Try
        count = remoteUsers.Length
        If (count = 1) Then
            remoteUser = remoteUsers(0)
            remoteUser.UserId = networkId
            remoteUser.FirstName = UserName
            remoteUser.LastName = UserName
            remoteUser.Email = "elitaUser@assurant.com"
            remoteUser.IsActive = isActive
            Try
                remoteUser = AuthorizationClientProxy.UpdateUser(remoteUser)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteUser)
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "UpdateUser", ex)
            End Try
        ElseIf (count = 0) Then
            remoteUser = New Auth.User
            remoteUser.UserId = networkId
            remoteUser.FirstName = UserName
            remoteUser.LastName = UserName
            remoteUser.Email = "elitaUser@assurant.com"
            remoteUser.IsActive = isActive
            Try
                remoteUser = AuthorizationClientProxy.CreateUser(remoteUser)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteUser)
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "CreateUser", ex)
            End Try
        Else
            Throw New BOInvalidOperationException("More than 1 Users found on Remote System with same Network ID")
        End If
        Return remoteUser.Id
    End Function

    Function UpdateUser(localSystemUserId As Guid, oldNetworkId As String, networkId As String, UserName As String, isActive As Boolean) As Guid Implements IRemoteRoleProvider.UpdateUser
        Dim remoteUser As Auth.User
        Dim remoteUsers As Auth.User()
        Dim count As Integer
        Dim userQuery As Auth.User
        userQuery = New Auth.User()
        userQuery.UserId = oldNetworkId

        Try
            remoteUsers = AuthorizationClientProxy.FindUsers(userQuery)
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "FindUsers", ex)
        End Try

        count = remoteUsers.Length
        If (count = 1) Then
            remoteUser = remoteUsers(0)
            remoteUser.UserId = networkId
            remoteUser.FirstName = UserName
            remoteUser.LastName = UserName
            remoteUser.Email = "elitaUser@assurant.com"
            remoteUser.IsActive = isActive

            Try
                remoteUser = AuthorizationClientProxy.UpdateUser(remoteUser)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteUser)
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "UpdateUser", ex)
            End Try

        ElseIf (count = 0) Then

            remoteUser = New Auth.User()
            remoteUser.Id = CreateUser(localSystemUserId, networkId, UserName, isActive)

            'Throw New BOInvalidOperationException("User not found on Remote System with Network ID specified")
        Else
            Throw New BOInvalidOperationException("More than 1 Users found on Remote System with same Network ID")
        End If
        Return remoteUser.Id
    End Function

    Sub DeleteUser(localSystemUserId As Guid, networkId As String, UserName As String) Implements IRemoteRoleProvider.DeleteUser
        Dim remoteUser As Auth.User
        Dim remoteUserExt As Auth.ExtendedUser
        Dim remoteUsers As Auth.User()
        Dim count As Integer
        Dim userQuery As Auth.User
        userQuery = New Auth.User()
        userQuery.UserId = networkId

        Try
            remoteUsers = AuthorizationClientProxy.FindUsers(userQuery)
        Catch ex As FaultException(Of Auth.AuthorizationFault)
            Throw New ServiceException("Authorization", "FindUsers", ex)
        End Try

        count = remoteUsers.Length
        If (count = 1) Then
            remoteUser = remoteUsers(0)
            remoteUser.IsActive = False

            Try
                remoteUser = AuthorizationClientProxy.UpdateUser(remoteUser)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteUser)
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "UpdateUser", ex)
            End Try

            Try
                remoteUserExt = AuthorizationClientProxy.GetUserForServiceById(remoteUser.Id, ServiceHelper.WORKQUEUE_SERVICE_NAME)
            Catch ex As FaultException(Of Auth.ValidationFault)
                Throw ex.AsBOValidationException(remoteUser)
            Catch ex As FaultException(Of Auth.NotFoundFault)
                Throw
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "GetUserForServiceById", ex)
            End Try

            If (remoteUserExt.Permissions IsNot Nothing AndAlso remoteUserExt.Permissions.Length > 0) Then
                Try
                    AuthorizationClientProxy.RemovePermissionsFromUser((From per In remoteUserExt.Permissions Select per.Id).ToArray(), remoteUserExt.Id)
                Catch ex As FaultException(Of WrkQueue.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of Auth.AuthorizationFault)
                    Throw New ServiceException("Authorization", "RemovePermissionsFromUser", ex)
                End Try
            End If
            For Each grp As Auth.Group In remoteUserExt.Groups
                Try
                    AuthorizationClientProxy.RemoveUsersFromGroup(New Guid() {remoteUserExt.Id}, grp.Id)
                Catch ex As FaultException(Of WrkQueue.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of Auth.AuthorizationFault)
                    Throw New ServiceException("Authorization", "RemoveUsersFromGroup", ex)
                End Try
            Next
        ElseIf (count = 0) Then
            Throw New BOInvalidOperationException("User not found on Remote System with Network ID specified")
        Else
            Throw New BOInvalidOperationException("More than 1 Users found on Remote System with same Network ID")
        End If
    End Sub

    Function SupportsCreateUser() As Boolean Implements IRemoteRoleProvider.SupportsCreateUser
        Return True
    End Function

    Function SupportsUpdateUser() As Boolean Implements IRemoteRoleProvider.SupportsUpdateUser
        Return True
    End Function

    Function SupportsDeleteUser() As Boolean Implements IRemoteRoleProvider.SupportsDeleteUser
        Return True
    End Function
#End Region

End Class

