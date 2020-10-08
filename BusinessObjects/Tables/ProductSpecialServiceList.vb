Public Class ProductSpecialServiceList
    Inherits BusinessObjectListBase

    Public Sub New(parent As SpecialService)
        MyBase.New(LoadTable(parent), GetType(ProductSpecialService), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ProductSpecialService).SpecialServiceId.Equals(CType(Parent, SpecialService).Id)
    End Function

    Public Function Find(ProductCodeId As Guid) As ProductSpecialService
        Dim bo As ProductSpecialService
        For Each bo In Me
            If bo.ProductCodeId.Equals(ProductCodeId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As SpecialService) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ProductSpecialServiceList)) Then
                Dim dal As New ProductSpecialServiceDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ProductSpecialServiceList))
            End If
            Return parent.Dataset.Tables(ProductSpecialServiceDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


End Class
