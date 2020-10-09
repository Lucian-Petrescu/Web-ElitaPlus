'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/11/2004)********************


Public Class QuestionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SOFT_QUESTION"
    Public Const TABLE_KEY_NAME As String = "soft_question_id"

    Public Const COL_NAME_SOFT_QUESTION_ID As String = "soft_question_id"
    Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
    Public Const COL_NAME_PARENT_ID As String = "parent_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_RISKTYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_CHILD_ORDER As String = "child_order"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_QUESTION_TYPE As String = "question_type"
    Public Const COL_NAME_QUESTION_TYPE_ID As String = "question_type_id"
    Public Const COL_NAME_IMPACTS_CLAIM_ID As String = "impacts_claim_id"
    Public Const COL_NAME_ANSWER_TYPE_ID As String = "answer_type_id"
    Public Const COL_NAME_CUSTOMER_MESSAGE As String = "customer_message"
    Public Const COL_NAME_ENTITY_ATTRIBUTE_ID As String = "entity_attribute_id"
    Public Const COL_NAME_SEARCH_TAGS As String = "search_tags"
    Public Const COL_NAME_ISSUE As String = "issue" 'Re-Visit AGL
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COMPANY_GROUP As String = "company_group_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "languageId"
    Public Const COL_NAME_ACTIVEON As String = "activeon"
    Public Const WILDCARD As Char = "%"
    Public Const ELITA_WILDCARD As Char = "*"
    Private Const const_AND As String = " AND "
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function GetMaxChildOrder(parentID As Guid) As Long
        Dim selectStmt As String = Config("/SQL/LOAD_MAX_CHILD_ORDER")
        Dim ds As DataSet = New DataSet
        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("parent_id", parentID.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_CHILD_ORDER)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return 0
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSoftQuestionId(parentID As Guid, ChildOrder As Long) As Guid
        Dim selectStmt As String = Config("/SQL/LOAD_SOFTQUESTIONID")
        Dim ds As DataSet = New DataSet
        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_id", ChildOrder), _
                                                                                               New DBHelper.DBHelperParameter("parent_id", parentID.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_SOFT_QUESTION_ID), Byte()))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Purpose:                    Load QuestionListForm from database
    'Author:                     Arnie Lugo
    'Date:                       03/14/2012
    'Modification History:       REQ-860
    Public Function LoadQuestionList(Code As String, Description As String, QuestionTypeId As Guid, SearchTags As String, Issue As String, ActiveOn As String, languageId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        selectStmt = Config("/SQL/LOAD_QUESTION_LIST")
        Dim dynamic_where_clause As String = String.Empty
        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())}


        If Not String.IsNullOrEmpty(Code) Then
            If Code.Contains(ELITA_WILDCARD) Then Code = Code.Replace(ELITA_WILDCARD, WILDCARD)
            dynamic_where_clause &= const_AND & "upper(q."
            dynamic_where_clause &= COL_NAME_CODE.ToUpper & ") LIKE :code"
            Dim idx As Integer = parameters.Length
            ReDim Preserve parameters(UBound(parameters) + 1)
            parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_CODE, Code.ToUpper())
        End If

        If Not String.IsNullOrEmpty(Description) Then
            If Description.Contains(ELITA_WILDCARD) Then Description = Description.Replace(ELITA_WILDCARD, WILDCARD)
            dynamic_where_clause &= const_AND & "upper(q."
            dynamic_where_clause &= COL_NAME_DESCRIPTION.ToUpper & ") LIKE :description"
            Dim idx As Integer = parameters.Length
            ReDim Preserve parameters(UBound(parameters) + 1)
            parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_DESCRIPTION, Description.ToUpper())
        End If

        If Not QuestionTypeId.Equals(Guid.Empty) Then
            dynamic_where_clause &= const_AND & "upper(q."
            dynamic_where_clause &= COL_NAME_QUESTION_TYPE_ID.ToUpper & ") = :questiontypeid"
            Dim idx As Integer = parameters.Length
            ReDim Preserve parameters(UBound(parameters) + 1)
            parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_TYPE_ID, QuestionTypeId.ToByteArray)
        End If

        If Not String.IsNullOrEmpty(Issue) Then
            If Issue.Contains(ELITA_WILDCARD) Then Issue = Issue.Replace(ELITA_WILDCARD, WILDCARD)
            dynamic_where_clause &= const_AND & "(Select upper(DESCRIPTION) from ELP_ISSUE where ISSUE_ID = iq.ISSUE_ID) LIKE :issue"
            Dim idx As Integer = parameters.Length
            ReDim Preserve parameters(UBound(parameters) + 1)
            parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_ISSUE, Issue.ToUpper())
        End If

        If Not String.IsNullOrEmpty(SearchTags) Then
            If SearchTags.Contains(ELITA_WILDCARD) Then SearchTags = SearchTags.Replace(ELITA_WILDCARD, WILDCARD)
            Dim idx As Integer = parameters.Length
            dynamic_where_clause &= const_AND & "( "
            If SearchTags.Contains(";") Then
                Dim iParmLabel As Integer = 1
                Dim sTags As String() = SearchTags.Split(";")
                For Each s As String In sTags
                    If iParmLabel > 1 Then
                        dynamic_where_clause &= const_OR
                    End If
                    dynamic_where_clause &= "upper(q."
                    dynamic_where_clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags" + iParmLabel.ToString
                    ReDim Preserve parameters(UBound(parameters) + 1)
                    parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS + iParmLabel.ToString, WILDCARD & s.Trim().ToUpper() & WILDCARD)
                    idx += 1
                    iParmLabel += 1
                Next
            Else
                dynamic_where_clause &= "upper(q."
                dynamic_where_clause &= COL_NAME_SEARCH_TAGS.ToUpper & ") LIKE :searchtags"
                ReDim Preserve parameters(UBound(parameters) + 1)
                parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_SEARCH_TAGS, WILDCARD & SearchTags.ToUpper() & WILDCARD)
            End If
            dynamic_where_clause &= ") "
        End If


        If Not String.IsNullOrEmpty(ActiveOn) Then
            dynamic_where_clause &= const_AND & ":activeOn between trunc(q.effective) and trunc(q.expiration)"
            Dim idx As Integer = parameters.Length
            ReDim Preserve parameters(UBound(parameters) + 1)
            parameters(idx) = New DBHelper.DBHelperParameter(COL_NAME_ACTIVEON, ActiveOn)
        End If




        selectStmt &= dynamic_where_clause

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim SoftQGroupDal As New SoftQuestionGroupDAL
        Dim AnswerDAL As New AnswerDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            AnswerDAL.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)
            SoftQGroupDal.Update(familyDataset, tr, DataRowState.Deleted)


            'Second Pass updates additions and changes
            SoftQGroupDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            AnswerDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Function IsQuestionAssignedtoIssue(QuestionId As Guid) As Boolean
        Dim selectStmt As String = Config("/SQL/IsQuestionAssignedtoIssue")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_id", QuestionId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables(TABLE_NAME).Rows.Count > 0 Then Return True Else Return False
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Special function"
    Function GetListItembyCode(ListCode As String, DropdownId As Guid) As Guid
        Dim selectStmt As String = Config("/SQL/GetListItembyCode")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter("item_code", ListCode.ToUpper), _
                     New DBHelper.DBHelperParameter("list_id", DropdownId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, DropdownDAL.TABLE_NAME, parameters)
            If ds.Tables(DropdownDAL.TABLE_NAME).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(DropdownDAL.TABLE_NAME).Rows(0)(DropdownDAL.TABLE_KEY_NAME), Byte()))
            Else                Return Guid.Empty
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region



End Class


