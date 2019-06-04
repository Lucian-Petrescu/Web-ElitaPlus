Public Class RolePermissionDAL
    Inherits BasePermissionDAL(Of RolePermissionDAL)

    Public Sub New()
        MyBase.New("ELP_ROLE_PERMISSION", "ROLE_PERMISSION_ID", "ROLE_ID")
    End Sub

End Class
