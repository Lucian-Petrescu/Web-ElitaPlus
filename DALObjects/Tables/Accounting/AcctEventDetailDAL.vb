'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************


Public Class AcctEventDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_EVENT_DETAIL"
    Public Const TABLE_KEY_NAME As String = "acct_event_detail_id"

    Public Const COL_NAME_ACCT_EVENT_DETAIL_ID As String = "acct_event_detail_id"
    Public Const COL_NAME_ACCT_BUSINESS_UNIT_ID As String = "acct_business_unit_id"
    Public Const COL_NAME_ACCT_EVENT_ID As String = "acct_event_id"
    Public Const COL_NAME_DEBIT_CREDIT As String = "debit_credit"
    Public Const COL_NAME_ACCOUNT_CODE As String = "account_code"
    Public Const COL_NAME_USE_PAYEE_SETTINGS As String = "use_payee_settings"
    Public Const COL_NAME_ANALYSIS_CODE_1 As String = "analysis_code_1"
    Public Const COL_NAME_ANALYSIS_CODE_2 As String = "analysis_code_2"
    Public Const COL_NAME_ANALYSIS_CODE_3 As String = "analysis_code_3"
    Public Const COL_NAME_ANALYSIS_CODE_4 As String = "analysis_code_4"
    Public Const COL_NAME_ANALYSIS_CODE_5 As String = "analysis_code_5"
    Public Const COL_NAME_ANALYSIS_CODE_6 As String = "analysis_code_6"
    Public Const COL_NAME_ANALYSIS_CODE_7 As String = "analysis_code_7"
    Public Const COL_NAME_ANALYSIS_CODE_8 As String = "analysis_code_8"
    Public Const COL_NAME_ANALYSIS_CODE_9 As String = "analysis_code_9"
    Public Const COL_NAME_ANALYSIS_CODE_10 As String = "analysis_code_10"
    Public Const COL_NAME_FIELD_TYPE_ID As String = "field_type_id"
    Public Const COL_NAME_CALCULATION As String = "calculation"
    Public Const COL_NAME_ALLOCATION_MARKER As String = "allocation_marker"
    Public Const COL_NAME_JOURNAL_TYPE As String = "journal_type"
    Public Const COL_NAME_ACCOUNT_TYPE As String = "account_type"
    Public Const COL_NAME_JOURNAL_ID_SUFFIX As String = "journal_id_suffix"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_BUSINESS_ENTITY_ID As String = "business_entity_id"
    Public Const COL_NAME_ANALYSIS_SRC_1_ID As String = "analysis_src_1_id"
    Public Const COL_NAME_ANALYSIS_SRC_2_ID As String = "analysis_src_2_id"
    Public Const COL_NAME_ANALYSIS_SRC_3_ID As String = "analysis_src_3_id"
    Public Const COL_NAME_ANALYSIS_SRC_4_ID As String = "analysis_src_4_id"
    Public Const COL_NAME_ANALYSIS_SRC_5_ID As String = "analysis_src_5_id"
    Public Const COL_NAME_ANALYSIS_SRC_6_ID As String = "analysis_src_6_id"
    Public Const COL_NAME_ANALYSIS_SRC_7_ID As String = "analysis_src_7_id"
    Public Const COL_NAME_ANALYSIS_SRC_8_ID As String = "analysis_src_8_id"
    Public Const COL_NAME_ANALYSIS_SRC_9_ID As String = "analysis_src_9_id"
    Public Const COL_NAME_ANALYSIS_SRC_10_ID As String = "analysis_src_10_id"
    Public Const COL_NAME_DESCRIPTION_SRC_ID As String = "description_src_id"
    Public Const COL_NAME_INCLUDE_EXCLUDE_IND As String = "include_exclude_ind"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_event_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadList(AcctEventMask As Guid, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        whereClauseConditions &= " AND " & COL_NAME_ACCT_EVENT_ID & " =HEXTORAW('" & GuidToSQLString(AcctEventMask) & "')"
        whereClauseConditions &= " AND " & TranslationDAL.COL_NAME_LANGUAGE_ID & " =HEXTORAW('" & GuidToSQLString(LanguageId) & "')"

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
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


End Class


