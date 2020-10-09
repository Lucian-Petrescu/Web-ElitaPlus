Public Class UserPermission
    Inherits BasePermission(Of UserPermissionDAL, UserPermission)

    Public Sub New(dataSet As DataSet)
        MyBase.New(dataSet)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(row)
    End Sub

End Class

Public Class UserPermissionList
    Inherits PermissionList(Of User, UserPermissionDAL, UserPermission, UserPermissionList)

    Public Sub New(pUser As User)
        MyBase.New(pUser)
    End Sub

    Public Overloads Overrides Function GetNewChild(parentId As System.Guid) As BusinessObjectBase
        Return New UserPermission(Parent.Dataset) With {.EntityId = parentId}
    End Function
End Class


