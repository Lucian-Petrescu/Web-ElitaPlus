'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/27/2012)********************


Public Class IssueQuestionListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ISSUE_QUESTION_LIST"
    Public Const TABLE_KEY_NAME As String = "issue_question_list_id"

    Public Const COL_NAME_ISSUE_QUESTION_LIST_ID As String = "issue_question_list_id"
    Public Const COL_NAME_QUESTION_LIST_ID As String = "question_list_id"
    Public Const COL_NAME_ISSUE_QUESTION_ID As String = "issue_question_id"
    Public Const COL_NAME_DISPLAY_ORDER As String = "display_order"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_QUESTION_LIST_CODE As String = "question_list_code"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_QUESTION_LIST_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Method"

    Public Function IsChild(ByVal QuestionListId As Guid, ByVal IssueQuestionId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet

        Try
            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_QUESTION_ID, IssueQuestionId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_ID, QuestionListId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSelectedDealerList(ByVal QuestionListCode As String) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetSelectedDealerList")

        Dim params() As DBHelper.DBHelperParameter = {}
        Dim dynamic_Where_Clause As String = String.Empty

        Try
            If Not String.IsNullOrEmpty(QuestionListCode) Then
                dynamic_Where_Clause &= " WHERE " & COL_NAME_QUESTION_LIST_CODE.ToUpper & " = '" & QuestionListCode & "'"
            End If
            selectStmt &= dynamic_Where_Clause
            Return DBHelper.Fetch(ds, selectStmt, IssueQuestionDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSelectedQuestionList(ByVal QuestionListID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/SEARCH_Question_List_Selected")
        Dim QuestionListParams As DBHelper.DBHelperParameter
        QuestionListParams = New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_ID, QuestionListID.ToByteArray)

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueQuestionListDAL.TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListParams})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetQuestionList(ByVal questionListId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/QUESTION_IN_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Try
            QuestionListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_ID, questionListId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetQuestionInList(ByVal questionListId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/QUESTION_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Try
            QuestionListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_ID, questionListId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDealerInList(ByVal QuestionListCode As String, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/DEALER_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim QuestionListCodeParam As DBHelper.DBHelperParameter

        Try
            QuestionListCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_CODE, QuestionListCode)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListCodeParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function SaveDealerList(ByVal DealerID As Guid, ByVal QuestionListCode As String, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/SAVE_DEALER_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim QuestionListCodeParam As DBHelper.DBHelperParameter
        Dim DealerIdParam As DBHelper.DBHelperParameter

        Try
            QuestionListCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_QUESTION_LIST_CODE, QuestionListCode)
            DealerIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, DealerID.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListCodeParam, DealerIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetQuestionExpiration(ByVal IssueQuestionListId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/ISSUE_QUESTION_EXPIRATION")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim IssueQuestionListIdParam As DBHelper.DBHelperParameter
        Try
            IssueQuestionListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_QUESTION_ID, IssueQuestionListId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {IssueQuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function UpdateDropdown(ByVal listId As Guid, ByVal code As String, ByVal maintainable_by_user As String, ByVal englishTranslation As String, ByVal userId As String) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_DROPDOWN")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_list_id", listId.ToByteArray), _
                            New DBHelper.DBHelperParameter("p_code", code), _
                            New DBHelper.DBHelperParameter("p_maintainable_by_user", maintainable_by_user), _
                            New DBHelper.DBHelperParameter("p_english_translation", englishTranslation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function UpdateTranslation(ByVal DictItemTranslationId As Guid, ByVal Translation As String, ByVal userId As String) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_TRANSLATION")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_dict_item_translation_id", DictItemTranslationId.ToByteArray), _
                            New DBHelper.DBHelperParameter("p_translation", Translation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

#End Region
End Class


