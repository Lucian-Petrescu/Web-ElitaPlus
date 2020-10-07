'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/24/2008)********************


Public Class ClaimStatusLetterDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_STATUS_LETTER"
    Public Const TABLE_KEY_NAME As String = "status_letter_id"

    Public Const COL_NAME_STATUS_LETTER_ID As String = "status_letter_id"
    Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_LETTER_TYPE As String = "letter_type"
    Public Const COL_NAME_NUMBER_OF_DAYS As String = "number_of_days"
    Public Const COL_NAME_EMAIL_SUBJECT As String = "email_subject"
    Public Const COL_NAME_EMAIL_TEXT As String = "email_text"
    Public Const COL_NAME_EMAIL_FROM As String = "email_from"
    Public Const COL_NAME_USE_SERVICE_CENTER_EMAIL As String = "use_service_center_email"
    Public Const COL_NAME_EMAIL_TO As String = "email_to"
    Public Const COL_NAME_IS_ACTIVE As String = "is_active"
    Public Const COL_NAME_USE_CLAIM_STATUS As String = "use_claim_status"
    Public Const COL_NAME_NOTIFICATION_TYPE_ID As String = "notification_type_id"
    Public Const COL_NAME_GROUP_OWNER_ID As String = "group_owner_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("status_letter_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compIds As ArrayList, dealerId As Guid, claimStatusByGroupId As Guid, companyGroupId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""

        inClausecondition &= "And edealer." & MiscUtil.BuildListForSql(CompanyDAL.COL_NAME_COMPANY_ID, compIds, False)

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND edealer.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not claimStatusByGroupId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND sgroup.claim_status_by_group_id = " & MiscUtil.GetDbStringFromGuid(claimStatusByGroupId)
        End If

        If Not companyGroupId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND sgroup.company_group_id = " & MiscUtil.GetDbStringFromGuid(companyGroupId)
        End If

        If Not languageId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND tran.language_id = " & MiscUtil.GetDbStringFromGuid(languageId)
        End If

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
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


