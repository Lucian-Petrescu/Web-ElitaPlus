'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/4/2008)********************


Public Class SvcNotificationReconWrkDAL
    Inherits DALBase


#Region "Constants"

    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_SVC_NOTIFICATION_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "svc_notification_recon_wrk_id"

    Public Const COL_NAME_SVC_NOTIFICATION_RECON_WRK_ID As String = "svc_notification_recon_wrk_id"
    Public Const COL_NAME_SVC_NOTIFICATION_PROCESSED_ID As String = "svc_notification_processed_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CLAIM_LOADED As String = "claim_loaded"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_SVC_NOTIFICATION_NUMBER As String = "svc_notification_number"
    Public Const COL_NAME_SVC_NOTIFICATION_TYPE As String = "svc_notification_type"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CREATED_ON As String = "created_on"
    Public Const COL_NAME_CHANGED_ON As String = "changed_on"
    Public Const COL_NAME_REQUIRED_START_DATE As String = "required_start_date"
    Public Const COL_NAME_REQUIRED_START_TIME As String = "required_start_time"
    Public Const COL_NAME_REQUIRED_END_DATE As String = "required_end_date"
    Public Const COL_NAME_REQUESTED_END_TIME As String = "requested_end_time"
    Public Const COL_NAME_ARTICLE_NUMBER As String = "article_number"
    Public Const COL_NAME_CUST_ACCT_NUMBER As String = "cust_acct_number"
    Public Const COL_NAME_CUST_NAME_1 As String = "cust_name_1"
    Public Const COL_NAME_CUST_NAME_2 As String = "cust_name_2"
    Public Const COL_NAME_CUST_CITY As String = "cust_city"
    Public Const COL_NAME_CUST_POSTAL_CODE As String = "cust_postal_code"
    Public Const COL_NAME_CUST_REGION As String = "cust_region"
    Public Const COL_NAME_CUST_ADDRESS As String = "cust_address"
    Public Const COL_NAME_CUST_PHONE_NUMBER As String = "cust_phone_number"
    Public Const COL_NAME_CUST_FAX_NUMBER As String = "cust_fax_number"
    Public Const COL_NAME_EQUIPMENT As String = "equipment"
    Public Const COL_NAME_MFG_NAME As String = "mfg_name"
    Public Const COL_NAME_MODEL_NUMBER As String = "model_number"
    Public Const COL_NAME_MFG_PART_NUMBER As String = "mfg_part_number"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SVC_NOTIFICATION_STATUS As String = "svc_notification_status"
    Public Const COL_NAME_SEQ_TASK_NUMBER As String = "seq_task_number"
    Public Const COL_NAME_SEQ_TASK_DESCRIPTION As String = "seq_task_description"
    Public Const COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER As String = "consecutive_activity_number"
    Public Const COL_NAME_ACTIVITY_TEXT As String = "activity_text"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_SITE As String = "site"
    Public Const COL_NAME_PRP_COD_AMT As String = "prp_cod_amt"
    Public Const COL_NAME_OP_INDICATOR As String = "op_indicator"
    Public Const COL_NAME_TRANSACTION_NUMBER As String = "transaction_number"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("svc_notification_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal notificationfileProcessedID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_SVC_NOTIFICATION_PROCESSED_ID, notificationfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function



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


End Class



