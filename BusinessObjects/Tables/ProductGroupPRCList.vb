Public Class ProductGroupPRCList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As ProductGroup)
        MyBase.New(LoadTable(parent), GetType(ProductGroupPrc), parent)
    End Sub


    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ProductGroupPrc).ProductCodeId.Equals(CType(Parent, ProductGroup).Id)
    End Function

    Public Function Find(ByVal productCodeId As Guid) As ProductGroupPrc
        Dim bo As ProductGroupPrc
        For Each bo In Me
            If bo.ProductCodeId.Equals(productCodeId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As ProductGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ProductGroupPRCList)) Then
                Dim dal As New ProductGroupPrcDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ProductGroupPRCList))
            End If
            Return parent.Dataset.Tables(ProductGroupPrcDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region



End Class

