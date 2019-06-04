Public Class ProductRegionList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ProductCode)
        MyBase.New(LoadTable(parent), GetType(ProductRegion), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ProductRegion).ProductCodeId.Equals(CType(Parent, ProductCode).Id)
    End Function

    Public Function Find(ByVal RegionId As Guid) As ProductRegion
        Dim bo As ProductRegion
        For Each bo In Me
            If bo.RegionId.Equals(RegionId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ProductCode) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ProductRegionList)) Then
                Dim dal As New ProductRegionDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ProductRegionList))
            End If
            Return parent.Dataset.Tables(ProductRegionDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


End Class
