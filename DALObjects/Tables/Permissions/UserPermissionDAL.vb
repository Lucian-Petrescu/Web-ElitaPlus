Public Class UserPermissionDAL
    Inherits BasePermissionDAL(Of UserPermissionDAL)

    Public Sub New()
        MyBase.New("ELP_USER_PERMISSION", "USER_PERMISSION_ID", "USER_ID")
    End Sub

End Class
