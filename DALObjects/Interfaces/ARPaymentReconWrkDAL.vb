Public Class ARPaymentReconWrkDAL
    Inherits OracleDALBase

#Region "Constants"

    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_AR_PAYMENT_INTERFACE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_PAYMENT_INTERFACE_ID

    Public Const COL_NAME_PAYMENT_INTERFACE_ID As String = "payment_interface_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_POST_PRE_PAID As String = "post_pre_paid"
    Public Const COL_NAME_SUBSCRIBER_NUMBER As String = "subscriber_number"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_PERIOD_START_DATE As String = "invoice_period_start_date"
    Public Const COL_NAME_INVOICE_PERIOD_END_DATE As String = "invoice_period_end_date"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_PAYMENT_METHOD As String = "payment_method"
    Public Const COL_NAME_PAYMENT_ENTITY_CODE As String = "payment_entity_code"
    Public Const COL_NAME_APPLICATION_MODE As String = "application_mode"
    Public Const COL_NAME_CURRENTCY_CODE As String = "currentcy_code"
    Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_PAYMENT_ID As String = "payment_id"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REFERENCE As String = "reference"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_PAYMENT_LOADED As String = "payment_loaded"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_MOBILE_NUMBER As String = "mobile_number"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"

    Public Const PAR_I_NAME_PAYMENT_INTERFACE_ID As String = "pi_payment_interface_id"
    Public Const PAR_I_NAME_DEALERFILE_PROCESSED_ID As String = "pi_dealerfile_processed_id"
    Public Const PAR_I_NAME_RECORD_TYPE As String = "pi_record_type"
    Public Const PAR_I_NAME_POST_PRE_PAID As String = "pi_post_pre_paid"
    Public Const PAR_I_NAME_SUBSCRIBER_NUMBER As String = "pi_subscriber_number"
    Public Const PAR_I_NAME_CERTIFICATE As String = "pi_certificate"
    Public Const PAR_I_NAME_INVOICE_DATE As String = "pi_invoice_date"
    Public Const PAR_I_NAME_INVOICE_PERIOD_START_DATE As String = "pi_invoice_period_start_date"
    Public Const PAR_I_NAME_INVOICE_PERIOD_END_DATE As String = "pi_invoice_period_end_date"
    Public Const PAR_I_NAME_PAYMENT_AMOUNT As String = "pi_payment_amount"
    Public Const PAR_I_NAME_PRODUCT_CODE As String = "pi_product_code"
    Public Const PAR_I_NAME_INSTALLMENT_NUMBER As String = "pi_installment_number"
    Public Const PAR_I_NAME_PAYMENT_DATE As String = "pi_payment_date"
    Public Const PAR_I_NAME_PAYMENT_METHOD As String = "pi_payment_method"
    Public Const PAR_I_NAME_PAYMENT_ENTITY_CODE As String = "pi_payment_entity_code"
    Public Const PAR_I_NAME_APPLICATION_MODE As String = "pi_application_mode"
    Public Const PAR_I_NAME_CURRENTCY_CODE As String = "pi_currentcy_code"
    Public Const PAR_I_NAME_CREDIT_CARD_NUMBER As String = "pi_credit_card_number"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_PAYMENT_ID As String = "pi_payment_id"
    Public Const PAR_I_NAME_SOURCE As String = "pi_source"
    Public Const PAR_I_NAME_REFERENCE As String = "pi_reference"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_ENTIRE_RECORD As String = "pi_entire_record"
    Public Const PAR_I_NAME_PAYMENT_LOADED As String = "pi_payment_loaded"
    Public Const PAR_I_NAME_REJECT_CODE As String = "pi_reject_code"
    Public Const PAR_I_NAME_REJECT_REASON As String = "pi_reject_reason"
    Public Const PAR_I_NAME_REJECT_MSG_PARMS As String = "pi_reject_msg_parms"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_MANUFACTURER As String = "pi_manufacturer"
    Public Const PAR_I_NAME_MODEL As String = "pi_model"
    Public Const PAR_I_NAME_MOBILE_NUMBER As String = "pi_mobile_number"
    Public Const PAR_I_NAME_SERIAL_NUMBER As String = "pi_serial_number"
    
    Public Const PAR_I_NAME_RECORD_MODE As String = "pi_record_mode"
    Public Const PAR_I_NAME_PARENT_FILE As String = "pi_is_split_file"
    Public Const PAR_I_PAGE_INDEX As String = "pi_page_index"
    Public Const PAR_I_PAGE_SIZE As String = "pi_page_size"
    Public Const PAR_I_NAME_SORT_EXPRESSION As String = "pi_sort_expression"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

   Public Function LoadList(dealerfileProcessedID As Guid,
                             languageID As Guid,
                             recordMode As String,
                             recordType As String,
                             rejectCode As String,
                             rejectReason As String,
                             parentFile As String,
                             pageindex As Integer,
                             pagesize As Integer,
                             sortExpression As String) As DataSet
        Try
            rejectReason = FormatWildCard(rejectReason)

            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, dealerfileProcessedID.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, languageID.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_RECORD_MODE, OracleDbType.Varchar2, recordMode)
                cmd.AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, recordType)
                cmd.AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, rejectCode)
                cmd.AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, rejectReason)
                cmd.AddParameter(PAR_I_NAME_PARENT_FILE, OracleDbType.Varchar2, parentFile)
                cmd.AddParameter(PAR_I_PAGE_INDEX, OracleDbType.Int64, value:=pageindex)
                cmd.AddParameter(PAR_I_PAGE_SIZE, OracleDbType.Int64, value:=pagesize)
                cmd.AddParameter(PAR_I_NAME_SORT_EXPRESSION, OracleDbType.Varchar2, value:=sortExpression)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadRejectList(dealerfileProcessedID As Guid) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD_REJECT_LIST"))
                cmd.AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, dealerfileProcessedID.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    Public Sub UpdateHeaderCount(dealerFileProcessedId As Guid)
        Dim cmd As OracleCommand
        cmd = OracleDbHelper.CreateCommand(Config("/SQL/UPDATE_HEADER_COUNT"), CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        cmd.AddParameter("pi_dealerfile_processed_id", OracleDbType.Raw, dealerFileProcessedId.ToByteArray())
        cmd.ExecuteNonQuery()
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_Payment_INTERFACE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PAYMENT_INTERFACE_ID)
            .AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALERFILE_PROCESSED_ID)
            .AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RECORD_TYPE)
            .AddParameter(PAR_I_NAME_POST_PRE_PAID, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POST_PRE_PAID)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_SUBSCRIBER_NUMBER)
            .AddParameter(PAR_I_NAME_CERTIFICATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_PAYMENT_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAYMENT_AMOUNT)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_INSTALLMENT_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INSTALLMENT_NUMBER)
            .AddParameter(PAR_I_NAME_PAYMENT_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PAYMENT_DATE)
            .AddParameter(PAR_I_NAME_PAYMENT_METHOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_METHOD)
            .AddParameter(PAR_I_NAME_PAYMENT_ENTITY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_ENTITY_CODE)
            .AddParameter(PAR_I_NAME_APPLICATION_MODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_APPLICATION_MODE)
            .AddParameter(PAR_I_NAME_CURRENTCY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENTCY_CODE)
            .AddParameter(PAR_I_NAME_CREDIT_CARD_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREDIT_CARD_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_Payment_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_Payment_ID)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_ENTIRE_RECORD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ENTIRE_RECORD)
            .AddParameter(PAR_I_NAME_Payment_LOADED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_Payment_LOADED)
            .AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_CODE)
            .AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_REASON)
            .AddParameter(PAR_I_NAME_REJECT_MSG_PARMS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_MSG_PARMS)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MANUFACTURER)
            .AddParameter(PAR_I_NAME_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODEL)
            .AddParameter(PAR_I_NAME_MOBILE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MOBILE_NUMBER)
            .AddParameter(PAR_I_NAME_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERIAL_NUMBER)

        End With

    End Sub
#End Region

End Class
