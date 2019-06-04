Public Class ServCenterMethRepairList

    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServCenterMethRepair), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServCenterMethRepair).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Public Function Find(ByVal MthodId As Guid) As ServCenterMethRepair
        Dim bo As ServCenterMethRepair
        For Each bo In Me
            If bo.ServCenterMorId.Equals(MthodId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServCenterMethRepairList)) Then
                Dim dal As New ServCenterMethRepairDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServCenterMethRepairList))
            End If
            Return parent.Dataset.Tables(ServCenterMethRepairDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class


