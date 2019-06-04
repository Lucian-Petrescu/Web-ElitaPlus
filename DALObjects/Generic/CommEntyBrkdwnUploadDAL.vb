Public Class CommEntyBrkdwnUploadDAL
    Inherits DALBase

#Region "Constants"

    Public Const TABLE_NAME As String = "elp_comm_brkdwn_upload"
    Public Const TABLE_KEY_NAME As String = "comm_brkdwn_upload_id"

    Public Const COL_NAME_UPLOAD_SESSION_ID As String = "upload_session_id"
    Public Const COL_NAME_RECORD_NUMBER As String = "record_number"
    Public Const COL_NAME_VALIDATION_ERRORS As String = "validation_errors"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION As String = "expiration_date"
    Public Const COL_NAME_ALLOW_MARKUP_PCT As String = "allowed_markup_pct"
    Public Const COL_NAME_TOLERANCE As String = "tolerance"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_POSITION As String = "position"
    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_ENTITY_NAME As String = "entity_name"
    Public Const COL_NAME_PHONE As String = "phone"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_DISPLAY_ID As String = "display_id"
    Public Const COL_NAME_TAX_ID As String = "tax_id"
    Public Const COL_NAME_COMM_ENTY_TYPE_ID As String = "commission_entity_type_id"
    Public Const COL_NAME_PAYMENT_METHOD_ID As String = "payment_method_id"
    Public Const COL_NAME_ACCOUNT_NAME As String = "account_name"
    Public Const COL_NAME_BANK_COUNTRY_ID As String = "bank_country_id"
    Public Const COL_NAME_PYMT_REASON_ID As String = "payment_reason_id"
    Public Const COL_NAME_BRANCH_NAME As String = "branch_name"
    Public Const COL_NAME_BANK_NAME As String = "bank_name"
    Public Const COL_NAME_BANK_SORT_CODE As String = "bank_sort_code"
    Public Const COL_NAME_IBAN_NUMBER As String = "iban_number"
    Public Const COL_NAME_SWIFT_CODE As String = "swift_code"
    Public Const COL_NAME_ACCOUNT_TYPE_ID As String = "account_type_id"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_BANK_LOOKUP_CODE As String = "bank_lookup_code"
    Public Const COL_NAME_TRANSACTION_LIMIT As String = "transaction_limit"
    Public Const COL_NAME_BANK_SUB_CODE As String = "bank_sub_code"
    Public Const COL_NAME_BRANCH_DIGIT As String = "branch_digit"
    Public Const COL_NAME_ACCOUNT_DIGIT As String = "account_digit"
    Public Const COL_NAME_BRANCH_NUMBER As String = "branch_number"
    Public Const COL_NAME_BANK_TAX_ID As String = "bank_tax_id"
    Public Const COL_NAME_PAYEE_TYPE_ID As String = "payee_type_id"
    Public Const COL_NAME_MARKUP_PERCENT As String = "markup_percent"
    Public Const COL_NAME_COMMISSION_PERCENT As String = "commission_percent"
    Public Const COL_NAME_COMP_MTHD_ID As String = "compute_method_id"

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_brkdwn_upload_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadPreValidatedCommEntyBrkdwnsForDealer(ByVal UploadSessionId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PREVALIDATED_COMM_ENTY_BRKDWNS_FOR_DEALER")

        Try
            Dim ds As New DataSet

            Dim parameter1 As DBHelper.DBHelperParameter
            parameter1 = New DBHelper.DBHelperParameter(COL_NAME_UPLOAD_SESSION_ID, UploadSessionId)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameter1})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadPreValidatedCommEntyBrkdwnsForUpload(ByVal UploadSessionId As String, ByVal Dealer_Id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PREVALIDATED_COMM_ENTY_BRKDWNS_FOR_UPLOAD")

        Try
            Dim ds As New DataSet

            Dim parameter1 As DBHelper.DBHelperParameter
            parameter1 = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, Dealer_Id)

            Dim parameter2 As DBHelper.DBHelperParameter
            parameter2 = New DBHelper.DBHelperParameter(COL_NAME_UPLOAD_SESSION_ID, UploadSessionId)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameter1, parameter2})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function UpdatePreValidatedCommEntyBrkdwnRecord(preValidatedCommEntyBrkdwnId As Guid, ByVal strValidationErrors As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/UPDATE_PREVALIDATED_COMM_ENTY_BRKDWN_RECORD")

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter

            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.TABLE_KEY_NAME, preValidatedCommEntyBrkdwnId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.COL_NAME_VALIDATION_ERRORS, strValidationErrors)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
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