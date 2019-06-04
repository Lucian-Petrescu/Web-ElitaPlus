Public Class ProductDeviceTypeList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ProductCode)
        MyBase.New(LoadTable(parent), GetType(ProductEquipment), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ProductEquipment).ProductCodeId.Equals(CType(Parent, ProductCode).Id)
    End Function

    Public Function Find(ByVal DeviceTypeId As Guid) As ProductEquipment
        Dim bo As ProductEquipment
        For Each bo In Me
            If bo.DeviceTypeId.Equals(DeviceTypeId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ProductCode) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ProductDeviceTypeList)) Then
                Dim dal As New ProductEquipmentDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ProductDeviceTypeList))
            End If
            Return parent.Dataset.Tables(ProductEquipmentDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region
End Class
