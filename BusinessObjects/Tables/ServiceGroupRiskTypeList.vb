Public Class ServiceGroupRiskTypeList
    Inherits BusinessObjectListBase

    Public Sub New(parent As ServiceGroup)
        MyBase.New(LoadTable(parent), GetType(ServiceGroupRiskType), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceGroupRiskType).ServiceGroupId.Equals(CType(Parent, ServiceGroup).Id)
    End Function

    Public Function Find(riskTypeId As Guid) As ServiceGroupRiskType
        Dim bo As ServiceGroupRiskType
        For Each bo In Me
            If bo.RiskTypeId.Equals(riskTypeId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As ServiceGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceGroupRiskTypeList)) Then
                Dim dal As New ServiceGroupRiskTypeDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceGroupRiskTypeList))
            End If
            Return parent.Dataset.Tables(ServiceGroupRiskTypeDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class
