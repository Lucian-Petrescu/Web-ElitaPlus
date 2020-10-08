
#Region "EquipmentListDetailList"

Public Class EquipmentListDetailList
    Inherits BusinessObjectListBase

    Public Sub New(parent As EquipmentList)
        MyBase.New(LoadTable(parent), GetType(EquipmentListDetail), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, EquipmentListDetail).EquipmentListId.Equals(CType(Parent, Equipmentlist).Id)
    End Function

    Private Shared Function LoadTable(parent As EquipmentList) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(EquipmentListDetailList)) Then
                Dim dal As New EquipmentListDetailDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(EquipmentListDetailList))
            End If
            Return parent.Dataset.Tables(EquipmentListDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class
#End Region


