'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/22/2013)********************


Public Class InvoiceReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "invoice_recon_wrk_id"

    Public Const COL_NAME_INVOICE_RECON_WRK_ID As String = "invoice_recon_wrk_id"
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_ID As String = "invoice_id"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_ATTRIBUTES As String = "attributes"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_AUTHORIZATION_ID As String = "authorization_id"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
    Public Const COL_NAME_VENDOR_SKU As String = "VENDOR_SKU"
    Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = "VENDOR_sku_description"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_DUE_DATE As String = "due_date"
    Public Const COL_NAME_SERVICE_LEVEL As String = "service_level"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_level_id"
    Public Const COL_NAME_MSG_PARAMETER_COUNT As String = "msg_parameter_count"
    Public Const COL_NAME_Translated_MSG As String = "translated_msg"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal claimloadFileProcessedId As Guid, ByVal languageID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, claimloadFileProcessedId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadByClaimLoadFileProcessedId(ByVal ds As DataSet, ByVal claimLoadFileProcessedId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_CLAIM_LOAD_FILE_PROCESSED_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ClaimloadFileProcessedDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, claimLoadFileProcessedId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


