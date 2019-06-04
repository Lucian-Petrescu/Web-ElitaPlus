Public Class PriceGroupDetailList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As PriceGroup)
        MyBase.New(LoadTable(parent), GetType(PriceGroupDetail), parent)
    End Sub

    Public Sub New(ByVal parent As PriceGroup, ByVal riskTypeId As Guid)
        MyBase.New(LoadTable(parent, riskTypeId), GetType(PriceGroupDetail), parent)
    End Sub
    Public Sub New(ByVal parent As PriceGroup, ByVal company_group_id As Guid, ByVal flag As Boolean)
        MyBase.New(LoadTable(parent, company_group_id, flag), GetType(PriceGroupDetail), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, PriceGroupDetail).PriceGroupId.Equals(CType(Parent, PriceGroup).Id)
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As PriceGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(PriceGroupDetailList)) Then
                Dim dal As New PriceGroupDetailDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(PriceGroupDetailList))
            End If
            Return parent.Dataset.Tables(PriceGroupDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Shared Function LoadTable(ByVal parent As PriceGroup, ByVal riskTypeId As Guid) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(PriceGroupDetailList)) Then
                Dim dal As New PriceGroupDetailDAL
                dal.LoadList(parent.Dataset, parent.Id, riskTypeId)
                parent.AddChildrenCollection(GetType(PriceGroupDetailList))
            End If
            Return parent.Dataset.Tables(PriceGroupDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Private Shared Function LoadTable(ByVal parent As PriceGroup, ByVal company_group_id As Guid, ByVal flag As Boolean) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(PriceGroupDetailList)) Then
                Dim dal As New PriceGroupDetailDAL
                dal.LoadList(parent.Dataset, parent.Id, company_group_id, True)
                parent.AddChildrenCollection(GetType(PriceGroupDetailList))
            End If
            Return parent.Dataset.Tables(PriceGroupDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region


End Class
