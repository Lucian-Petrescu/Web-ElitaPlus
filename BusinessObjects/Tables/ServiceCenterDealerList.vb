Public Class ServiceCenterDealerList

    Inherits BusinessObjectListBase

    Public Sub New(parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServiceCenterDealer), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceCenterDealer).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Public Function Find(DealerId As Guid) As ServiceCenterDealer
        Dim bo As ServiceCenterDealer
        For Each bo In Me
            If bo.DealerId.Equals(DealerId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceCenterDealerList)) Then
                Dim dal As New ServiceCenterDealerDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceCenterDealerList))
            End If
            Return parent.Dataset.Tables(ServiceCenterDealerDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class


