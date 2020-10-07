Imports System.Collections.Generic

Namespace Auth
    Partial Public Class ExtendedUser
        Public Function HasPermission(ByVal permission As Permission) As Boolean
            If (Not IsActive) Then
                Return False
            End If

            Dim permissions = GetDistinctPermissions()

            Return permissions.Where(Function(p) p.ResourceType = permission.ResourceType AndAlso p.Resource = permission.Resource AndAlso p.Action = permission.Action).Count() > 0
        End Function

        Public Function GetDistinctPermissions() As IQueryable(Of Auth.Permission)
            Dim permissions = New List(Of Permission)(Me.Permissions)
            For Each grp As Auth.ExtendedGroup In Groups
                permissions.AddRange(grp.Permissions)
            Next
            Return permissions.Distinct(New PermissionComparer()).AsQueryable()
        End Function

        Private Class PermissionComparer
            Implements IEqualityComparer(Of Permission)
            Public Function Equals(ByVal x As Permission, ByVal y As Permission) As Boolean Implements IEqualityComparer(Of Permission).Equals
                Return x.Id = y.Id
            End Function

            Public Function GetHashCode(ByVal obj As Permission) As Integer Implements IEqualityComparer(Of Permission).GetHashCode
                Return MyBase.GetHashCode()
            End Function
        End Class
    End Class
End Namespace
