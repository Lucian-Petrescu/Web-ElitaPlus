Public Class ServiceCenterNetworkList
    Inherits BusinessObjectListBase

    Public Sub New(parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServiceNetworkSvc), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceNetworkSvc).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Public Function Find(serviceCenterId As Guid) As ServiceNetworkSvc
        Dim bo As ServiceNetworkSvc
        For Each bo In Me
            If bo.ServiceCenterId.Equals(serviceCenterId) Then Return bo
        Next
        Return Nothing
    End Function


    Public Function FindSrvNetwork(serviceNetworkId As Guid) As ServiceNetworkSvc
        Dim bo As ServiceNetworkSvc
        For Each bo In Me
            If bo.ServiceNetworkId.Equals(serviceNetworkId) Then Return bo
        Next
        Return Nothing
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceCenterNetworkList)) Then
                Dim dal As New ServiceNetworkSvcDAL
                dal.LoadNedtworkList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceCenterNetworkList))
            End If
            Return parent.Dataset.Tables(ServiceNetworkSvcDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


End Class
