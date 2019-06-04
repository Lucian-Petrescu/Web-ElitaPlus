Public Class ExcludedLiRolesList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ExcludeListitemByRole)
        MyBase.New(LoadTable(parent), GetType(ExcludedLiRoles), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ExcludedLiRoles).ExcludeListitemRoleId.Equals(CType(Parent, ExcludeListitemByRole).Id)
    End Function

    Public Function Find(ByVal RoleId As Guid) As ExcludedLiRoles
        Dim bo As ExcludedLiRoles
        For Each bo In Me
            If bo.RoleId.Equals(RoleId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ExcludeListitemByRole) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ExcludedLiRolesList)) Then
                Dim dal As New ExcludedLiRolesDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ExcludedLiRolesList))
            End If
            Return parent.Dataset.Tables(ExcludedLiRolesDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


End Class
