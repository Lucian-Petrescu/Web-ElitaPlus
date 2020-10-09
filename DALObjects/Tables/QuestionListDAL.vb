'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/27/2012)********************


Public Class QuestionListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_QUESTION_LIST"
    Public Const TABLE_DEALER As String = "ELP_DEALER"
    Public Const TABLE_NAME_COMPANY_COUNTRY As String = "Countries"
    Public Const TABLE_KEY_NAME As String = "question_list_id"
    Public Const TABLE_NAME_GROUP_COMPANIES As String = "group_companies"
    Public Const TABLE_LIST As String = "ELP_LIST"
    Public Const TABLE_LIST_ITEM As String = "ELP_LIST_ITEM"

    Public Const COL_NAME_DEALER_QUESTION_LIST_CODE = "question_list_code"
    Public Const COL_NAME_QUESTION_LIST_ID = "question_list_id"
    Public Const COL_NAME_CODE = "code"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_LIST_ID = "list_id"
    Public Const COL_NAME_LIST_ITEM_ID = "list_item_id"

    Public Const COL_NAME_COMMENTS = "comments"
    Public Const COL_NAME_EFFECTIVE = "effective"
    Public Const COL_NAME_EXPIRATION = "expiration"

    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("question_list_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList(code As String, _
                                           description As String, activeOn As Date, _
                                           companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (FormatSearchMask(description))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If

        If (Not activeOn = Nothing) AndAlso (Not activeOn.ToString() = String.Empty) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(to_date('" & Date.Parse(activeOn).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN " & "trunc(" & COL_NAME_EFFECTIVE & ") AND trunc(" & COL_NAME_EXPIRATION & ")"

            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadUniqueList(code As String, _
                                       description As String, vEffective As String, _
                                       companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not (vEffective Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & COL_NAME_EFFECTIVE & ")" & " =  to_date('" & DateHelper.GetDateValue(vEffective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function CheckOverlap(code As String, _
                                           effective As String, _
                                           expiration As String, _
                                           companyIds As ArrayList, languageId As Guid, listId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/CHECK_OVERLAP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & COL_NAME_EFFECTIVE & " AND " & COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not (effective Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_QUESTION_LIST WHERE UPPER(" & COL_NAME_CODE & ")" & code.ToUpper & ")"
            bIsWhereClause = True
        End If
        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, listId.ToByteArray)

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckOverlapToExpire(code As String, _
                                           effective As DateType, _
                                           expiration As DateType, _
                                           companyIds As ArrayList, languageId As Guid, listId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/CHECK_LIST_OVERLAP_TO_EXPIRE")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim RowNumParam As DBHelper.DBHelperParameter
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim QuestionCodeParam As DBHelper.DBHelperParameter
        Dim QuestionEffectiveParam As DBHelper.DBHelperParameter
        Dim QuestionExpirationParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        RowNumParam = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, 101, GetType(Integer))
        QuestionCodeParam = New DBHelper.DBHelperParameter(COL_NAME_CODE, code.ToUpper)
        QuestionEffectiveParam = New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE, effective)
        QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, listId.ToByteArray)

        Try
            If ((Not (code Is Nothing)) AndAlso (FormatSearchMask(code))) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ")" & code.ToUpper
                bIsWhereClause = True
            End If
            If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & COL_NAME_EFFECTIVE & " AND " & COL_NAME_EXPIRATION
                bIsWhereClause = True
            End If
            If (Not (effective Is String.Empty)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_QUESTION_LIST WHERE UPPER(" & COL_NAME_CODE & ")" & code.ToUpper & ")"
                bIsWhereClause = True
            End If
            If bIsWhereClause Then
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, listId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckDurationOverlap(code As String, _
                                           effective As String, _
                                           expiration As String, _
                                           companyIds As ArrayList, languageId As Guid, listId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/CHECK_OVERLAP_DURATION")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective)) Then
            whereClauseConditions &= " AND ((" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & COL_NAME_EFFECTIVE & " AND " & COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= ") OR (" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & COL_NAME_EFFECTIVE & " AND " & COL_NAME_EXPIRATION & "))"
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, listId.ToByteArray)

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ExpireList(PrevListDS As DataSet, effective As String) As Boolean

        Dim selectStmt As String = Config("/SQL/EXPIRE_LIST")
        Dim ds As New DataSet
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim QuestionExpiration As DBHelper.DBHelperParameter
        Dim index As Integer
        Try
            For index = 0 To PrevListDS.Tables(0).Rows.Count - 1
                QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, PrevListDS.Tables(0).Rows(index)(COL_NAME_QUESTION_LIST_ID))
                QuestionExpiration = New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION, DateHelper.GetDateValue(effective).AddMinutes(-1))
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionExpiration, QuestionListIdParam})
                ExpireListQuestions(PrevListDS.Tables(0).Rows(index)(COL_NAME_QUESTION_LIST_ID), DateHelper.GetDateValue(effective).AddMinutes(-1).ToString)
            Next
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function ExpireListQuestions(QuestionListId As Byte(), expiration As String) As Boolean

        Dim selectStmt As String = Config("/SQL/EXPIRE_QUESTION")
        Dim ds As New DataSet
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = String.Empty
        Dim dynamicFieldClauseConditions As String = String.Empty
        Dim bIsWhereClause As Boolean = False
        Dim bIsFieldClause As Boolean = False

        If (Not (expiration Is String.Empty)) Then
            dynamicFieldClauseConditions &= Environment.NewLine & "(" & COL_NAME_EXPIRATION & ")" & " =  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsFieldClause = True
        End If

        If (Not (expiration Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & COL_NAME_EXPIRATION & ")" & " >  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsFieldClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, dynamicFieldClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, "")
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, QuestionListId)
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function IsQuestionInList(QuestionId As Guid, QuestionListId As Guid, companyIds As ArrayList, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/IS_QUESTION")
        Dim ds As New DataSet
        Dim QuestionIdParam As DBHelper.DBHelperParameter
        Dim QuestionListIdParam As DBHelper.DBHelperParameter

        Try
            QuestionIdParam = New DBHelper.DBHelperParameter("Question_ID", QuestionId)
            QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, QuestionListId)
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionIdParam, QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function InsertQuestion(QuestionId As Guid, companyIds As ArrayList, languageId As Guid) As Boolean

        Dim selectStmt As String = Config("/SQL/INSERT_QUESTION")
        Dim ds As New DataSet
        Dim QuestionIdParam As DBHelper.DBHelperParameter

        Try
            QuestionIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, QuestionId)
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {QuestionIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Private Function IsThereALikeClause(description As String, code As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(description) OrElse IsLikeClause(code)

        Return bIsLikeClause
    End Function

    Public Function IsListToDealer(ListCode As String, QuestionListID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/IS_LIST_ASSIGNED_TO_DEALER")
        Dim QuestionListDAL As New QuestionListDAL
        Dim QuestionListIdParam As DBHelper.DBHelperParameter
        Dim ds As New DataSet

        Try
            QuestionListIdParam = New DBHelper.DBHelperParameter(COL_NAME_QUESTION_LIST_ID, QuestionListID.ToByteArray)
            selectStmt &= " AND UPPER(" & COL_NAME_DEALER_QUESTION_LIST_CODE & ") = '" & ListCode.ToUpper & "'"
            Return DBHelper.Fetch(ds, selectStmt, TABLE_DEALER, New DBHelper.DBHelperParameter() {QuestionListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCurrentDateTime() As DateTime

        Dim selectStmt As String = Config("/SQL/CURRENT_TIME_STAMP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim QuestionIdParam As DBHelper.DBHelperParameter

        Try
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {}).Tables(0).Rows(0)("SYSDATE"))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDropdownId(listCode As String) As Guid
        Dim selectStmt As String = Config("/SQL/DROPDOWN_ID")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim listCodeParam As DBHelper.DBHelperParameter
        Dim id As Byte()
        Try
            listCodeParam = New DBHelper.DBHelperParameter(COL_NAME_CODE, listCode)
            id = DBHelper.Fetch(ds, selectStmt, TABLE_LIST, New DBHelper.DBHelperParameter() {listCodeParam}).Tables(TABLE_LIST).Rows(0)(COL_NAME_LIST_ID)
            If Not id Is Nothing Then
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDropdownItemId(DropdownId As Guid, itemCode As String) As Guid
        Dim selectStmt As String = Config("/SQL/DROPDOWN_ITEM_ID")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim itemCodeParam As DBHelper.DBHelperParameter
        Dim DropdownIdParam As DBHelper.DBHelperParameter
        Dim tempDS As DataSet
        Dim id As Byte()
        Try
            itemCodeParam = New DBHelper.DBHelperParameter(COL_NAME_CODE, itemCode)
            DropdownIdParam = New DBHelper.DBHelperParameter(COL_NAME_LIST_ID, DropdownId.ToByteArray)
            DBHelper.Fetch(ds, selectStmt, TABLE_LIST_ITEM, New DBHelper.DBHelperParameter() {itemCodeParam, DropdownIdParam})
            If ds.Tables(TABLE_LIST_ITEM).Rows.Count > 0 Then
                id = ds.Tables(TABLE_LIST_ITEM).Rows(0)(COL_NAME_LIST_ITEM_ID)
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim QuestionListDAL As New QuestionListDAL
        Dim IssueQuestionListDAL As New IssueQuestionListDAL
        Dim IssueQuestionDAL As New IssueQuestionDAL
        Dim DealerDAL As New DealerDAL
        Dim dropdownBO As DropdownDAL
        Dim retVal As Boolean

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass - Delete
            DealerDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            IssueQuestionListDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass - Add new List
            DealerDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            IssueQuestionListDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub


#End Region

End Class


