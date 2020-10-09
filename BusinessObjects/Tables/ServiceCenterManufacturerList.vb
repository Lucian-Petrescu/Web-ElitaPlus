Public Class ServiceCenterManufacturerList
    Inherits BusinessObjectListBase

    Public Sub New(parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServiceCenterManufacturer), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceCenterManufacturer).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Public Function Find(manufacturerId As Guid) As ServiceCenterManufacturer
        Dim bo As ServiceCenterManufacturer
        For Each bo In Me
            If bo.ManufacturerId.Equals(manufacturerId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceCenterManufacturerList)) Then
                Dim dal As New ServiceCenterManufacturerDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceCenterManufacturerList))
            End If
            Return parent.Dataset.Tables(ServiceCenterManufacturerDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class

