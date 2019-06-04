
#Region "IssueNotesChildrenList"

Public Class IssueNotesChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As Issue)
        MyBase.New(LoadTable(parent), GetType(IssueComment), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, IssueComment).IssueId.Equals(CType(Parent, Issue).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As Issue) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IssueNotesChildrenList)) Then
                Dim dal As New IssueCommentDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IssueNotesChildrenList))
            End If
            Return parent.Dataset.Tables(IssueCommentDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#End Region

#Region "IssueQuestionsChildrenList"

Public Class IssueQuestionsChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As Issue)
        MyBase.New(LoadTable(parent), GetType(IssueQuestion), parent)
    End Sub

    Public Sub New(ByVal parent As Issue, issueId As Guid, dealerId As Guid)
        MyBase.New(LoadTable(parent, issueId, dealerId), GetType(IssueQuestion), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, IssueQuestion).IssueId.Equals(CType(Parent, Issue).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As Issue) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IssueQuestionsChildrenList)) Then
                Dim dal As New IssueQuestionDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IssueQuestionsChildrenList))
            End If
            Return parent.Dataset.Tables(IssueQuestionDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Shared Function LoadTable(ByVal parent As Issue, issueId As Guid, dealerId As Guid) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IssueQuestionsChildrenList)) Then
                Dim dal As New IssueQuestionDAL
                dal.LoadList(parent.Dataset, issueId, dealerId)
                parent.AddChildrenCollection(GetType(IssueQuestionsChildrenList))
            End If
            Return parent.Dataset.Tables(IssueQuestionDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#End Region

#Region "IssueRulesChildrenList"

Public Class IssueRulesChildrenList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As Issue)
        MyBase.New(LoadTable(parent), GetType(RuleIssue), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, RuleIssue).IssueId.Equals(CType(Parent, Issue).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As Issue) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(IssueRulesChildrenList)) Then
                Dim dal As New RuleIssueDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(IssueRulesChildrenList))
            End If
            Return parent.Dataset.Tables(RuleIssueDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#End Region