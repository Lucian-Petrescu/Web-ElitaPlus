'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/27/2012)********************


Public Class IssueQuestionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ISSUE_QUESTION"
    Public Const TABLE_KEY_NAME As String = "issue_question_id"

    Public Const COL_NAME_ISSUE_QUESTION_ID As String = "issue_question_id"
    Public Const COL_NAME_ISSUE_ID As String = "issue_id"
    Public Const COL_NAME_SOFT_QUESTION_ID As String = "soft_question_id"
    Public Const COL_NAME_DISPLAY_ORDER As String = "display_order"
    Public Const COL_NAME_QUESTION_LIST_CODE As String = "question_list_code"
    Public Const COL_NAME_SEARCH_TAGS As String = "search_tags"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_LANGUAGE_ID As String = "languageId"
    Public Const WILDCARD As Char = "%"
    Public Const ELITA_WILDCARD As Char = "*"
    Private Const const_OR As String = " OR "
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_question_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(familyDS As DataSet, issueId As Guid, dealerId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_FILTERED_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_id", issueId.ToByteArray), New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"

    Public Function ExecuteQuestionsFilter(IssueID As Guid, QuestionList As String, SearchTags As String) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/SEARCH_Question_List_detail")
        Dim dynamic_Where_Clause As String = String.Empty
        Dim const_AND As String = " AND "
        Dim const_OR As String = " OR "
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim idx As Integer = parameters.Length
        Dim iParmLabel As Integer = 1

        dynamic_Where_Clause &= const_AND & "ESQ."
        If Not String.IsNullOrEmpty(QuestionList) Then
            dynamic_Where_Clause &= COL_NAME_DESCRIPTION.ToUpper & LIKE_CLAUSE & "'" & WILDCARD_CHAR & QuestionList & WILDCARD_CHAR & "'"
        Else
            dynamic_Where_Clause &= COL_NAME_DESCRIPTION.ToUpper & LIKE_CLAUSE & "'" & WILDCARD_CHAR & "'"
        End If
        If Not String.IsNullOrEmpty(SearchTags) Then
            If SearchTags.Contains(ELITA_WILDCARD) Then SearchTags = SearchTags.Replace(ELITA_WILDCARD, WILDCARD)
            dynamic_Where_Clause &= const_AND & "( "
            If SearchTags.Contains(";") Then
                Dim sTags As String() = SearchTags.Split(";")
                For Each s As String In sTags
                    If iParmLabel > 1 Then
                        dynamic_Where_Clause &= const_OR
                    End If
                    dynamic_Where_Clause &= "UPPER(ESQ."
                    dynamic_Where_Clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags" + iParmLabel.ToString
                    ReDim Preserve parameters(UBound(parameters) + 1)
                    parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS + iParmLabel.ToString, s.Trim().ToUpper())
                    idx += 1
                    iParmLabel += 1
                Next
            Else
                dynamic_Where_Clause &= "UPPER(ESQ."
                dynamic_Where_Clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags"
                ReDim Preserve parameters(UBound(parameters) + 1)
                parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS, SearchTags.ToUpper())
            End If
            dynamic_Where_Clause &= ") "
        End If
        If Not IssueID.Equals(Guid.Empty) Then
            dynamic_Where_Clause &= const_AND & "EI."
            dynamic_Where_Clause &= COL_NAME_ISSUE_ID.ToUpper & " = " & MiscUtil.GetDbStringFromGuid(IssueID)
        End If

        selectStmt &= dynamic_Where_Clause

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function AvailableQuestionListFilter(IssueID As Guid, QuestionList As String, SearchTags As String, ActiveOn As String, languageId As Guid) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/Available_Question_List")
        Dim dynamic_Where_Clause As String = String.Empty
        Dim const_AND As String = " AND "
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
               New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        dynamic_Where_Clause &= const_AND & "Q."
        If Not String.IsNullOrEmpty(QuestionList) Then
            dynamic_Where_Clause &= COL_NAME_DESCRIPTION.ToUpper & LIKE_CLAUSE & "'" & WILDCARD_CHAR & QuestionList & WILDCARD_CHAR & "'"
        Else
            dynamic_Where_Clause &= COL_NAME_DESCRIPTION.ToUpper & LIKE_CLAUSE & "'" & WILDCARD_CHAR & "'"
        End If
        If Not IssueID.Equals(Guid.Empty) Then
            dynamic_Where_Clause &= const_AND & "IQ."
            dynamic_Where_Clause &= COL_NAME_ISSUE_ID.ToUpper & " = " & MiscUtil.GetDbStringFromGuid(IssueID)
        End If
        If Not String.IsNullOrEmpty(ActiveOn) Then
            dynamic_Where_Clause &= const_AND & Environment.NewLine & " trunc(to_date('" & DateHelper.GetDateValue(ActiveOn).ToString("MM/dd/yyyy HH:mm:ss") _
                & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN trunc(Q." & COL_NAME_EFFECTIVE & ")" & " AND trunc(Q." & COL_NAME_EXPIRATION & ")" & ""
        End If
        If Not String.IsNullOrEmpty(SearchTags) Then
            If SearchTags.Contains(ELITA_WILDCARD) Then SearchTags = SearchTags.Replace(ELITA_WILDCARD, WILDCARD)
            Dim idx As Integer = parameters.Length
            dynamic_Where_Clause &= const_AND & "( "
            If SearchTags.Contains(";") Then
                Dim iParmLabel As Integer = 1
                Dim sTags As String() = SearchTags.Split(";")
                For Each s As String In sTags
                    If iParmLabel > 1 Then
                        dynamic_Where_Clause &= const_OR
                    End If
                    dynamic_Where_Clause &= "upper(Q."
                    dynamic_Where_Clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags" + iParmLabel.ToString
                    ReDim Preserve parameters(UBound(parameters) + 1)
                    parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS + iParmLabel.ToString, s.Trim().ToUpper())
                    idx += 1
                    iParmLabel += 1
                Next
            Else
                dynamic_Where_Clause &= "upper(Q."
                dynamic_Where_Clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags"
                ReDim Preserve parameters(UBound(parameters) + 1)
                parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS, SearchTags.ToUpper())
            End If
            dynamic_Where_Clause &= ") "
        End If

        dynamic_Where_Clause &= " ORDER BY DESCRIPTION"
        selectStmt &= dynamic_Where_Clause

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ExecuteDealerFilter(Code As String) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/SEARCH_Dealer_List_detail")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim dynamic_Where_Clause As String = String.Empty

        Try
            dynamic_Where_Clause &= " WHERE " & COL_NAME_QUESTION_LIST_CODE.ToUpper & " = '" & Code & "'"
            selectStmt &= dynamic_Where_Clause
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function GetSelectedDealerList(Code As String) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/SEARCH_Dealer_List_detail")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim dynamic_Where_Clause As String = String.Empty

        Try
            dynamic_Where_Clause &= " WHERE " & COL_NAME_QUESTION_LIST_CODE.ToUpper & " = '" & Code & "'"
            selectStmt &= dynamic_Where_Clause
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function IsChild(SoftQuestionId As Guid, IssueId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim IssueCommentIdParam As DBHelper.DBHelperParameter
        Dim IssueIdParam As DBHelper.DBHelperParameter

        Try
            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_ID, SoftQuestionId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class


