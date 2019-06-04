Public Class DealerList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As QuestionList)
        MyBase.New(LoadTable(parent), GetType(Dealer), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, Dealer).questionlistcode.Equals(CType(Parent, QuestionList).Code)
    End Function

    Private Shared Function LoadTable(ByVal parent As QuestionList) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(DealerList)) Then
                Dim dal As New DealerDAL
                dal.LoadListChild(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(DealerList))
            End If
            Return parent.Dataset.Tables(DealerDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

End Class
