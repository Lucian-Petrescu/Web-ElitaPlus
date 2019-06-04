Public Class ServiceNetworkSVCList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceNetwork)
        MyBase.New(LoadTable(parent), GetType(ServiceNetworkSvc), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceNetworkSvc).ServiceNetworkId.Equals(CType(Parent, ServiceNetwork).Id)
    End Function

    Public Function Find(ByVal serviceCenterId As Guid) As ServiceNetworkSvc
        Dim bo As ServiceNetworkSvc
        For Each bo In Me
            If bo.ServiceCenterId.Equals(serviceCenterId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ServiceNetwork) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceNetworkSVCList)) Then
                Dim dal As New ServiceNetworkSvcDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceNetworkSVCList))
            End If
            Return parent.Dataset.Tables(ServiceNetworkSvcDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class
