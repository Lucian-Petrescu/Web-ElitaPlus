Public Class BestReplacementList
    Inherits BusinessObjectListBase

    Public Sub New(parent As BestReplacementGroup)
        MyBase.New(LoadTable(parent), GetType(BestReplacement), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, BestReplacement).MigrationPathId.Equals(CType(Parent, BestReplacementGroup).Id)
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As BestReplacementGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(BestReplacementList)) Then
                Dim dal As New BestReplacementDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(BestReplacementList))
            End If
            Return parent.Dataset.Tables(BestReplacementDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class
