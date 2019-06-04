
#Region "IssueQuestionListDetail"

Public Class IssueQuestionListDetail
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As QuestionList)
        MyBase.New(LoadTable(parent), GetType(IssueQuestionList), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, IssueQuestionList).QuestionListId.Equals(CType(Parent, QuestionList).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As QuestionList) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IssueQuestionListDetail)) Then
                Dim dal As New IssueQuestionListDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IssueQuestionListDetail))
            End If
            Return parent.Dataset.Tables(IssueQuestionListDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class
#End Region


