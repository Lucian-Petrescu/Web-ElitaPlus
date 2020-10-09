Public Class SgRtManufacturerList
    Inherits BusinessObjectListBase

    Public Sub New(parent As ServiceGroupRiskType)
        MyBase.New(LoadTable(parent), GetType(SgRtManufacturer), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, SgRtManufacturer).ServiceGroupRiskTypeId.Equals(CType(Parent, ServiceGroupRiskType).Id)
    End Function

    Public Function Find(manufacturerId As Guid) As SgRtManufacturer
        Dim bo As SgRtManufacturer
        For Each bo In Me
            If bo.ManufacturerId.Equals(manufacturerId) Then Return bo
        Next
        Return Nothing
    End Function



    Private Shared Function LoadTable(parent As ServiceGroupRiskType) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(SgRtManufacturerList)) Then
                Dim dal As New SgRtManufacturerDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(SgRtManufacturerList))
            End If
            Return parent.Dataset.Tables(SgRtManufacturerDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


End Class
