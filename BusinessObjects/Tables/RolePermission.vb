Public Class RolePermission
    Inherits BasePermission(Of RolePermissionDAL, RolePermission)

    Public Sub New(dataSet As DataSet)
        MyBase.New(dataSet)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(row)
    End Sub

End Class

Public Class RolePermissionList
    Inherits PermissionList(Of Role, RolePermissionDAL, RolePermission, RolePermissionList)

    Public Sub New(pRole As Role)
        MyBase.New(pRole)
    End Sub

    Public Overloads Overrides Function GetNewChild(parentId As System.Guid) As BusinessObjectBase
        Return New RolePermission(Parent.Dataset) With {.EntityId = parentId}
    End Function
End Class
