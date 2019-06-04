Public Class RolePermission
    Inherits BasePermission(Of RolePermissionDAL, RolePermission)

    Public Sub New(ByVal dataSet As DataSet)
        MyBase.New(dataSet)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(row)
    End Sub

End Class

Public Class RolePermissionList
    Inherits PermissionList(Of Role, RolePermissionDAL, RolePermission, RolePermissionList)

    Public Sub New(ByVal pRole As Role)
        MyBase.New(pRole)
    End Sub

    Public Overloads Overrides Function GetNewChild(ByVal parentId As System.Guid) As BusinessObjectBase
        Return New RolePermission(MyBase.Parent.Dataset) With {.EntityId = parentId}
    End Function
End Class
