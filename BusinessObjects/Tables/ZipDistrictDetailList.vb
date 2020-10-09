Public Class ZipDistrictDetailList
    Inherits BusinessObjectListBase

    Public Sub New(parent As ZipDistrict)
        MyBase.New(LoadTable(parent), GetType(ZipDistrictDetail), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ZipDistrictDetail).ZipDistrictId.Equals(CType(Parent, ZipDistrict).Id)
    End Function

    Public Function Find(zipCode As String) As ZipDistrictDetail
        Dim bo As ZipDistrictDetail
        For Each bo In Me
            If bo.ZipCode = zipCode Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As ZipDistrict) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ZipDistrictDetailList)) Then
                Dim dal As New ZipDistrictDetailDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ZipDistrictDetailList))
            End If
            Return parent.Dataset.Tables(ZipDistrictDetailDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

End Class
