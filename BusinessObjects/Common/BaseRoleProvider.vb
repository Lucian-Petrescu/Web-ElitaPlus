Public MustInherit Class BaseRoleProvider
    Public Shared Function CreateRoleProvider(roleProviderTypeCode As String) As IRemoteRoleProvider
        Select Case roleProviderTypeCode.ToUpper()
            Case Codes.ROLE_PROVIDER__WORKQUEUE
                Return New AuthorizationServiceRoleProvider()
            Case Else
                Throw New ArgumentException()
        End Select
    End Function

    Friend Shared Function CreateRoleProvider(roleProviderTypeId As Guid) As IRemoteRoleProvider
        Dim roleProviderTypeCode As String
        roleProviderTypeCode = LookupListNew.GetCodeFromId(Codes.ROLE_PROVIDER, roleProviderTypeId)
        Return CreateRoleProvider(roleProviderTypeCode)
    End Function
End Class
