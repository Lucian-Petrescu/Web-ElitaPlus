'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/30/2013)********************


Public Class ClaimAuthHistoryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_AUTH_HISTORY"
    Public Const TABLE_KEY_NAME As String = "claim_auth_history_id"

    Public Const COL_NAME_CLAIM_AUTH_HISTORY_ID As String = "claim_auth_history_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_level_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID As String = "claim_authorization_status_id"
    Public Const COL_NAME_SPECIAL_INSTRUCTION As String = "special_instruction"
    Public Const COL_NAME_VISIT_DATE As String = "visit_date"
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = "device_reception_date"
    Public Const COL_NAME_EXPECTED_REPAIR_DATE As String = "expected_repair_date"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_PICK_UP_DATE As String = "pick_up_date"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_WHO_PAYS_ID As String = "who_pays_id"
    Public Const COL_NAME_DEFECT_REASON As String = "defect_reason"
    Public Const COL_NAME_TECHNICAL_REPORT As String = "technical_report"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_SVC_REFERENCE_NUMBER As String = "svc_reference_number"
    Public Const COL_NAME_EXTERNAL_CREATED_DATE As String = "external_created_date"
    Public Const COL_NAME_IS_SPECIAL_SERVICE_ID As String = "is_special_service_id"
    Public Const COL_NAME_REVERSE_LOGISTICS_ID As String = "reverse_logistics_id"
    Public Const COL_NAME_PROBLEM_FOUND As String = "problem_found"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_HIST_CREATED_BY As String = "hist_created_by"
    Public Const COL_NAME_HIST_CREATED_DATE As String = "hist_created_date"
    Public Const COL_NAME_HIST_MODIFIED_BY As String = "hist_modified_by"
    Public Const COL_NAME_HIST_MODIFIED_DATE As String = "hist_modified_date"
    Public Const COL_NAME_VERIFICATION_NUMBER As String = "verification_number"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_auth_history_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDS As DataSet, claim_authorization_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_authorization_id", claim_authorization_id.ToByteArray)}
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


