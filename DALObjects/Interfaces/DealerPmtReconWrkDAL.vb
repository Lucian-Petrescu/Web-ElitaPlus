'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/8/2004)********************


Public Class DealerPmtReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_DEALER_PMT_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "dealer_pmt_recon_wrk_id"

    Public Const COL_NAME_DEALER_PMT_RECON_WRK_ID As String = "dealer_pmt_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_PAYMENT_LOADED As String = "payment_loaded"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_DATE_OF_PAYMENT As String = "date_of_payment"
    Public Const COL_NAME_DATE_PAID_FOR As String = "date_paid_for"
    Public Const COL_NAME_CAMPAIGN_NUMBER As String = "campaign_number"
    Public Const COL_NAME_MEMBESHIP_NUMBER As String = "membership_number"
    Public Const COL_NAME_SERVICE_LINE_NUMBER As String = "service_line_number"
    Public Const COL_NAME_PAYMENT_INVOICE_NUMBER As String = "payment_invoice_number"
    Public Const COL_NAME_COLLECTED_AMOUNT As String = "collected_amount"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_NEW_PRODUCT_CODE As String = "new_product_code"
    Public Const COL_NAME_ADJUSTMENT_AMOUNT As String = "adjustment_amount"
    Public Const COL_NAME_TRANSLATED_MSG As String = "Translated_MSG"
    Public Const COL_NAME_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_NAME_REJECT_MSG_PARAMS As String = "REJECT_MSG_PARMS"
    Public Const COL_NAME_INSTALLMENT_NUM As String = "installment_num"
    Public Const COL_NAME_FEE_INCOME As String = "fee_income"
    Public Const COL_NAME_DEALER As String = "dealer"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_pmt_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerfileProcessedID As Guid, languageID As Guid, recordMode As String, _
                                        parentFile As String) As DataSet

        Dim selectStmt As String
        Dim parameters() As OracleParameter
        If parentFile = "N" Then
            selectStmt = Config("/SQL/LOAD_LIST")
            parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                                                New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode)}
        Else
            selectStmt = Config("/SQL/LOAD_LIST_FOR_PARENT")
            parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                                                New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray), _
                                                New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode), _
                                                New OracleParameter("p_recordMode", recordMode)}
        End If
 
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadRejectList(dealerfileProcessedID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_REJECT_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
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
#End Region


End Class



