Public Class CertItemCoverageDeductibleList
    Inherits BusinessObjectListBase

    Public Sub New(parent As CertItemCoverage)
        MyBase.New(LoadTable(parent), GetType(CertItemCoverage), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, CertItemCoverageDeductible).CertItemCoverageId.Equals(CType(Parent, CertItemCoverage).Id)
    End Function

    Private Shared Function LoadTable(parent As CertItemCoverage) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CertItemCoverageDeductibleList)) Then
                Dim dal As New CertItemCoverageDeductibleDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(CertItemCoverageDeductibleList))
            End If
            Return parent.Dataset.Tables(CertItemCoverageDeductibleDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class
