Public Class ServiceCenterZipDistrictList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ServiceCenter)
        MyBase.New(LoadTable(parent), GetType(ServiceCenterZipDistrict), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ServiceCenterZipDistrict).ServiceCenterId.Equals(CType(Parent, ServiceCenter).Id)
    End Function

    Public Function Find(ByVal zipDistrictId As Guid) As ServiceCenterZipDistrict
        Dim bo As ServiceCenterZipDistrict
        For Each bo In Me
            If bo.ZipDistrictId.Equals(zipDistrictId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ServiceCenter) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ServiceCenterZipDistrictList)) Then
                Dim dal As New ServiceCenterZipDistrictDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ServiceCenterZipDistrictList))
            End If
            Return parent.Dataset.Tables(ServiceCenterZipDistrictDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class


