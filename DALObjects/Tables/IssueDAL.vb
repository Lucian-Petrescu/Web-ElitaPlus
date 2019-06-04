'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/27/2012)********************


Public Class IssueDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ISSUE"
    Public Const TABLE_KEY_NAME As String = "issue_id"

    Public Const COL_NAME_ISSUE_ID As String = "issue_id"
    Public Const COL_NAME_RULE_ID As String = "rule_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_ISSUE_TYPE_ID As String = "issue_type_id"
    Public Const COL_NAME_RULE_ISSUE_ID As String = "rule_issue_id"
    Public Const COL_NAME_ISSUE_QUESTION_ID As String = "issue_question_id"
    Public Const COL_NAME_ISSUE_TYPE As String = "issue_type"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_PRE_CONDITIONS As String = "pre_conditions"
    Public Const COL_NAME_ISSUE_PROCESSOR As String = "issue_processor_xcd"
    public  Const COL_NAME_DENIED_REASON As String = "denied_reason_xcd"
    Public Const COL_NAME_SP_CLAIM_TYPE As String = "sp_claim_type_xcd"
    Public Const COL_NAME_SP_CLAIM_VALUE As String = "sp_claim_value"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DATE As String = "nowdate"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, id.ToByteArray)}
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

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim IssueCommentDAL As New IssueCommentDAL
        Dim IssueQuestionDAL As New IssueQuestionDAL
        Dim RuleIssueDAL As New RuleIssueDAL
        Dim CompWrkQIssue As New CompanyWorkQueueIssueDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass - Delete
            CompWrkQIssue.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            IssueCommentDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            IssueQuestionDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            RuleIssueDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass - Add new List
            Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            CompWrkQIssue.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            IssueCommentDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            IssueQuestionDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            RuleIssueDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

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

#End Region


#Region "CRUD Methods"

    Public Function LoadSearchList(ByVal code As String, _
                                       ByVal description As String, ByVal IssueTypeId As Guid, ByVal activeOn As String, _
                                       ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim issueTypeIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        If IssueTypeId = Guid.Empty Then
            selectStmt = Me.Config("/SQL/LOAD_LIST")
        Else
            selectStmt = Me.Config("/SQL/LOAD_SEARCH_LIST")
        End If

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " And " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            whereClauseConditions &= " And " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If
        If (Not (activeOn Is String.Empty)) Then
            whereClauseConditions &= " And " & Environment.NewLine & " trunc(to_date('" & DateHelper.GetDateValue(activeOn).ToString("MM/dd/yyyy HH:mm:ss") _
                & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN trunc(EI." & Me.COL_NAME_EFFECTIVE & ")" & " AND trunc(EI." & Me.COL_NAME_EXPIRATION & ")" & ""
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        If Not IssueTypeId = Guid.Empty Then
            issueTypeIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_TYPE_ID, IssueTypeId.ToByteArray())
        End If

        Try
            If IssueTypeId = Guid.Empty Then
                DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Else
                DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {issueTypeIdParam})
            End If
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal code As String, _
                                       ByVal description As String, ByVal activeOn As String, _
                                       ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim issueTypeIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If
        If (Not (activeOn Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(to_date('" & DateHelper.GetDateValue(activeOn).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN EI." & Me.COL_NAME_EFFECTIVE & " AND EI." & Me.COL_NAME_EXPIRATION & ""
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAvailableList(ByVal code As String, _
                                       ByVal description As String, ByVal activeOn As String, _
                                       ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter        
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If
        If (Not (activeOn Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(to_date('" & DateHelper.GetDateValue(activeOn).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN EI." & Me.COL_NAME_EFFECTIVE & " AND EI." & Me.COL_NAME_EXPIRATION & ""
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckOverlap(ByVal code As String, _
                                           ByVal effective As String, _
                                           ByVal expiration As String, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_OVERLAP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not (effective Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_ISSUE WHERE UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper & ")"
            bIsWhereClause = True
        End If
        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, listId.ToByteArray)

        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckOverlapToExpire(ByVal code As String, _
                                       ByVal effective As DateType, _
                                       ByVal expiration As DateType, _
                                       ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_LIST_OVERLAP_TO_EXPIRE")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim RowNumParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim EquipmentCodeParam As DBHelper.DBHelperParameter
        Dim EquipmentEffectiveParam As DBHelper.DBHelperParameter
        Dim EquipmentExpirationParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        RowNumParam = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, 101, GetType(Integer))
        EquipmentCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_CODE, code.ToUpper)
        EquipmentEffectiveParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EFFECTIVE, effective)
        EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, listId.ToByteArray)

        Try
            If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
                bIsWhereClause = True
            End If
            If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
                bIsWhereClause = True
            End If
            If (Not (effective Is String.Empty)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_ISSUE WHERE UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper & ")"
                bIsWhereClause = True
            End If
            If bIsWhereClause Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, listId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckDurationOverlap(ByVal code As String, _
                                           ByVal effective As String, _
                                           ByVal expiration As String, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_OVERLAP_DURATION")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim equipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective)) Then
            whereClauseConditions &= " AND ((" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= ") OR (" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION & "))"
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        equipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, listId.ToByteArray)

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {equipmentListIdParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ExpireList(ByVal PrevListDS As DataSet, ByVal effective As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/EXPIRE_LIST")
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim EquipmentExpiration As DBHelper.DBHelperParameter
        Dim index As Integer
        Try
            For index = 0 To PrevListDS.Tables(0).Rows.Count - 1
                EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, PrevListDS.Tables(0).Rows(index)(Me.COL_NAME_ISSUE_ID))
                EquipmentExpiration = New DBHelper.DBHelperParameter(Me.COL_NAME_EXPIRATION, DateHelper.GetDateValue(effective).AddSeconds(-1))
                DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentExpiration, EquipmentListIdParam})
                ExpireListQuestions(PrevListDS.Tables(0).Rows(index)(Me.COL_NAME_ISSUE_ID), DateHelper.GetDateValue(effective).AddSeconds(-1).ToString)
                ExpireListRules(PrevListDS.Tables(0).Rows(index)(Me.COL_NAME_ISSUE_ID), DateHelper.GetDateValue(effective).AddSeconds(-1).ToString)
            Next
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function ExpireListQuestions(ByVal equipmentListId As Byte(), ByVal expiration As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/EXPIRE_ISSUE_QUESTIONS")
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = String.Empty
        Dim dynamicFieldClauseConditions As String = String.Empty
        Dim bIsWhereClause As Boolean = False
        Dim bIsFieldClause As Boolean = False

        If (Not (expiration Is String.Empty)) Then
            dynamicFieldClauseConditions &= Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " =  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsFieldClause = True
        End If

        If (Not (expiration Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " >  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsFieldClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, dynamicFieldClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, "")
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, equipmentListId)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function ExpireListRules(ByVal equipmentListId As Byte(), ByVal expiration As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/EXPIRE_ISSUE_RULES")
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = String.Empty
        Dim dynamicFieldClauseConditions As String = String.Empty
        Dim bIsWhereClause As Boolean = False
        Dim bIsFieldClause As Boolean = False

        If (Not (expiration Is String.Empty)) Then
            dynamicFieldClauseConditions &= Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " =  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsFieldClause = True
        End If

        If (Not (expiration Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " >  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsFieldClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, dynamicFieldClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, "")
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, equipmentListId)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Private Function IsThereALikeClause(ByVal description As String, ByVal code As String) As Boolean
        Dim bIsLikeClause As Boolean
        bIsLikeClause = Me.IsLikeClause(description) OrElse Me.IsLikeClause(code)
        Return bIsLikeClause
    End Function

    Public Function GetCurrentDateTime() As DateTime

        Dim selectStmt As String = Me.Config("/SQL/CURRENT_TIME_STAMP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter

        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {}).Tables(0).Rows(0)("SYSDATE"))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetQuestionExpiration(ByVal IssueId As Guid, ByVal IssueQuestionId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/SOFT_QUESTION_EXPIRATION")
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Dim IssueQuestionIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        Try
            IssueIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, IssueId)
            IssueQuestionIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_QUESTION_ID, IssueQuestionId)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {IssueIdParam, IssueQuestionIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRuleExpiration(ByVal IssueId As Guid, ByVal RuleId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/RULE_EXPIRATION")
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Dim RuleIssueIdParam As DBHelper.DBHelperParameter

        Try
            RuleIssueIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_RULE_ID, RuleId)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {RuleIssueIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetSoftQuestionID(ByVal IssueId As Guid, ByVal IssueQuestionId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/SOFT_QUESTION_ID")
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Dim IssueQuestionIdParam As DBHelper.DBHelperParameter

        Try
            IssueQuestionIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_QUESTION_ID, IssueQuestionId)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {IssueQuestionIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRuleID(ByVal IssueId As Guid, ByVal RuleIssueId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/RULE_ID")
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Dim RuleIssueIdParam As DBHelper.DBHelperParameter

        Try
            RuleIssueIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_RULE_ISSUE_ID, RuleIssueId)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {RuleIssueIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadIssuesByDealer(ByVal dealerId As Guid) As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ISSUES_BY_DEALER")

        Dim parameters() As OracleParameter = _
           New OracleParameter() {New OracleParameter(Me.COL_NAME_DATE, OracleDbType.TimeStamp), _
                                  New OracleParameter(Me.COL_NAME_DATE, OracleDbType.TimeStamp), _
                                  New OracleParameter(Me.COL_NAME_DATE, OracleDbType.TimeStamp), _
                                  New OracleParameter(Me.COL_NAME_DATE, OracleDbType.TimeStamp), _
                                  New OracleParameter(Me.COL_NAME_DATE, OracleDbType.TimeStamp), _
                                  New OracleParameter(Me.COL_NAME_DEALER_ID, OracleDbType.Raw, 16)}


        Dim ds As New DataSet
        Dim nowDateTime As New DateTimeType(DateTime.Now)

        parameters(0).Value = nowDateTime.Value
        parameters(1).Value = nowDateTime.Value
        parameters(2).Value = nowDateTime.Value
        parameters(3).Value = nowDateTime.Value
        parameters(4).Value = nowDateTime.Value
        parameters(5).Value = dealerId.ToByteArray

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return New DataView(ds.Tables(Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Public Method"
    Public Function GetSelectedIssuesList(ByVal IssueID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetSelectedIssuesList")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSelectedQuestionsList(ByVal IssueID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetSelectedQuestionsList")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSelectedRulesList(ByVal IssueID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetSelectedRulesList")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ExecuteRulesFilter(ByVal IssueID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetAvailableRulesList")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function IsAssignedQuestionNoteOrRule(ByVal IssueID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/IS_ISSUE_TO_NOTE_QUESTION_RULE")
        Dim IssueDAL As New IssueDAL
        Dim IssueIdParam As DBHelper.DBHelperParameter
        Dim ds As New DataSet

        Try
            IssueIdParam = New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, IssueID.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {IssueIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Function GetListItembyCode(ByVal ListCode As String, ByVal DropdownId As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GetListItembyCode")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter("item_code", ListCode.ToUpper), _
                     New DBHelper.DBHelperParameter("list_id", DropdownId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, DropdownDAL.TABLE_NAME, parameters)
            If ds.Tables(DropdownDAL.TABLE_NAME).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(DropdownDAL.TABLE_NAME).Rows(0)(DropdownDAL.TABLE_KEY_NAME), Byte()))
            Else
                Return Guid.Empty
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region
End Class



