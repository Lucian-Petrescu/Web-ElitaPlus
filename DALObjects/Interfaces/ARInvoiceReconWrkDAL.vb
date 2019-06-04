Public Class ARInvoiceReconWrkDAL
    Inherits OracleDALBase

#Region "Constants"

    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_AR_INVOICE_INTERFACE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_INTERFACE_ID

    Public Const COL_NAME_INVOICE_INTERFACE_ID As String = "invoice_interface_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_PERIOD_START_DATE As String = "invoice_period_start_date"
    Public Const COL_NAME_INVOICE_PERIOD_END_DATE As String = "invoice_period_end_date"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOCE_DUE_DATE As String = "invoce_due_date"
    Public Const COL_NAME_BILL_TO_ADDRESS_ID As String = "bill_to_address_id"
    Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
    Public Const COL_NAME_CURRENCY_CODE As String = "currency_code"
    Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
    Public Const COL_NAME_LINE_TYPE As String = "line_type"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_EARNING_PARTER As String = "earning_parter"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REFERENCE As String = "reference"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_INVOICE_HEADER_ID As String = "invoice_header_id"
    Public Const COL_NAME_INVOICE_LINE_ID As String = "invoice_line_id"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_INVOICE_LOADED As String = "invoice_loaded"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_UI_PROD_CODE As String = "UI_PROG_CODE"
    'Public Const COL_NAME_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_NAME_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    'Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_TRANSLATED_MSG As String = "Translated_MSG"
    Public Const COL_NAME_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_NAME_MOBILE_NUMBER As String = "MobileNumber"
    Public Const COL_NAME_SERIAL_NUMBER As String = "SerialNumber"
    Public Const COL_NAME_MANUFACTURER As String = "Manufacturer"
    Public Const COL_NAME_MODEL As String = "Model"
    Public Const COL_NAME_SUBSCRIBER_NUMBER As String = "SubscriberNumber"
    Public Const COL_NAME_POST_PRE_PAID As String = "PostPrePaid"
    Public Const COL_NAME_PRODUCT_CODE As String = "ProductCode"

    Public Const PAR_I_NAME_INVOICE_INTERFACE_ID As String = "pi_invoice_interface_id"
    Public Const PAR_I_NAME_DEALERFILE_PROCESSED_ID As String = "pi_dealerfile_processed_id"
    Public Const PAR_I_NAME_RECORD_TYPE As String = "pi_record_type"
    Public Const PAR_I_NAME_CERTIFICATE As String = "pi_certificate"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_INVOICE_PERIOD_START_DATE As String = "pi_invoice_period_start_date"
    Public Const PAR_I_NAME_INVOICE_PERIOD_END_DATE As String = "pi_invoice_period_end_date"
    Public Const PAR_I_NAME_INVOICE_DATE As String = "pi_invoice_date"
    Public Const PAR_I_NAME_INVOCE_DUE_DATE As String = "pi_invoce_due_date"
    Public Const PAR_I_NAME_BILL_TO_ADDRESS_ID As String = "pi_bill_to_address_id"
    Public Const PAR_I_NAME_SHIP_TO_ADDRESS_ID As String = "pi_ship_to_address_id"
    Public Const PAR_I_NAME_CURRENCY_CODE As String = "pi_currency_code"
    Public Const PAR_I_NAME_EXCHANGE_RATE As String = "pi_exchange_rate"
    Public Const PAR_I_NAME_LINE_TYPE As String = "pi_line_type"
    Public Const PAR_I_NAME_ITEM_CODE As String = "pi_item_code"
    Public Const PAR_I_NAME_EARNING_PARTER As String = "pi_earning_parter"
    Public Const PAR_I_NAME_SOURCE As String = "pi_source"
    Public Const PAR_I_NAME_REFERENCE As String = "pi_reference"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_INSTALLMENT_NUMBER As String = "pi_installment_number"
    Public Const PAR_I_NAME_AMOUNT As String = "pi_amount"
    Public Const PAR_I_NAME_PARENT_LINE_NUMBER As String = "pi_parent_line_number"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_INVOICE_HEADER_ID As String = "pi_invoice_header_id"
    Public Const PAR_I_NAME_INVOICE_LINE_ID As String = "pi_invoice_line_id"
    Public Const PAR_I_NAME_ENTIRE_RECORD As String = "pi_entire_record"
    Public Const PAR_I_NAME_INVOICE_LOADED As String = "pi_invoice_loaded"
    Public Const PAR_I_NAME_REJECT_CODE As String = "pi_reject_code"
    Public Const PAR_I_NAME_REJECT_REASON As String = "pi_reject_reason"
    Public Const PAR_I_NAME_REJECT_MSG_PARMS As String = "pi_reject_msg_parms"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_MOBILE_NUMBER As String = "pi_mobile_number"
    Public Const PAR_I_NAME_SERIAL_NUMBER As String = "pi_serial_number"
    Public Const PAR_I_NAME_MANUFACTURER As String = "pi_manufacturer"
    Public Const PAR_I_NAME_MODEL As String = "pi_model"
    Public Const PAR_I_NAME_SUBSCRIBER_NUMBER As String = "pi_subscriber_number"
    Public Const PAR_I_NAME_POST_PRE_PAID As String = "pi_post_pre_paid"
    Public Const PAR_I_NAME_PRODUCT_CODE As String = "pi_product_code"

    'Custom parameter
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

        Public Sub LoadSchema(ByVal ds As DataSet)
            Load(ds, Guid.Empty)
        End Sub

        Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
            Try
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                    cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                    cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                    OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
                End Using
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Sub
    Public Function LoadList(ByVal dealerfileProcessedID As Guid,
                             ByVal languageID As Guid,
                             ByVal recordMode As String,
                             ByVal recordType As String,
                             ByVal rejectCode As String,
                             ByVal rejectReason As String,
                             ByVal parentFile As String,
                             ByVal pageindex As Integer,
                             ByVal pagesize As Integer,
                             ByVal sortExpression As String) As DataSet
        Try
            rejectReason = FormatWildCard(rejectReason)

            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
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
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'Public Function CountSearch(ByVal dealerfileProcessedID As Guid,
    '                            ByVal recordMode As String,
    '                            ByVal recordType As String,
    '                            ByVal rejectCode As String,
    '                            ByVal rejectReason As String,
    '                            ByVal fi_record_type As String,
    '                            ByVal fi_reject_code As String,
    '                            ByVal fi_reject_reason As String) As Double
    '        Dim selectStmt As String = Me.Config("/SQL/CountSearch")
    '        Dim recordtypeconstraint As String

    '        Dim familyDS As New DataSet
    '        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_type", recordType),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_type", recordType),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
    '                                                                                       New DBHelper.DBHelperParameter("fi_record_type", fi_record_type),
    '                                                                                       New DBHelper.DBHelperParameter("fi_reject_reason", fi_reject_reason),
    '                                                                                       New DBHelper.DBHelperParameter("fi_reject_code", fi_reject_code)}
    '        Try
    '            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
    '            Return CType(familyDS.Tables(0).Rows(0)(0), Integer)
    '        Catch ex As Exception
    '            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '        End Try

    '    End Function
    'Public Function ParentCount(ByVal dealerfileProcessedID As Guid,
    '                        ByVal recordMode As String,
    '                        ByVal recordType As String,
    '                        ByVal rejectCode As String,
    '                        ByVal rejectReason As String,
    '                        ByVal fi_record_type As String,
    '                        ByVal fi_reject_code As String,
    '                        ByVal fi_reject_reason As String) As Double
    '    Dim selectStmt As String = Me.Config("/SQL/PARENT_COUNT")
    '    Dim recordtypeconstraint As String

    '    Dim familyDS As New DataSet
    '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
    '                                                                                   New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_type", recordType),
    '                                                                                       New DBHelper.DBHelperParameter("pi_record_type", recordType),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
    '                                                                                       New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
    '                                                                                       New DBHelper.DBHelperParameter("fi_record_type", fi_record_type),
    '                                                                                       New DBHelper.DBHelperParameter("fi_reject_reason", fi_reject_reason),
    '                                                                                       New DBHelper.DBHelperParameter("fi_reject_code", fi_reject_code)}
    '    Try
    '        DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
    '        Return CType(familyDS.Tables(0).Rows(0)(0), Integer)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function


    Public Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataSet
            Try
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_REJECT_LIST"))
                    cmd.AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, dealerfileProcessedID.ToByteArray())
                    cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                    Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
                End Using
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

#End Region

#Region "Overloaded Methods"

        Public Sub UpdateHeaderCount(ByVal dealerFileProcessedId As Guid)
            Dim cmd As OracleCommand
            cmd = OracleDbHelper.CreateCommand(Me.Config("/SQL/UPDATE_HEADER_COUNT"), CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
            cmd.AddParameter("pi_dealerfile_processed_id", OracleDbType.Raw, dealerFileProcessedId.ToByteArray())
            cmd.ExecuteNonQuery()
        End Sub

        Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
            If ds Is Nothing Then
                Return
            End If
            If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
                Throw New NotSupportedException()
            End If
            If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
                MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            End If
        End Sub

        Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
            Throw New NotSupportedException()
        End Sub

        Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
            Throw New NotSupportedException()
        End Sub

        Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
            With command
            .AddParameter(PAR_I_NAME_INVOICE_INTERFACE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_INTERFACE_ID)
            .AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALERFILE_PROCESSED_ID)
            .AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RECORD_TYPE)
            .AddParameter(PAR_I_NAME_CERTIFICATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOCE_DUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOCE_DUE_DATE)
            .AddParameter(PAR_I_NAME_BILL_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_BILL_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_SHIP_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIP_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_CURRENCY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_CODE)
            .AddParameter(PAR_I_NAME_EXCHANGE_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXCHANGE_RATE)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM_CODE)
            .AddParameter(PAR_I_NAME_EARNING_PARTER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EARNING_PARTER)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_INSTALLMENT_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INSTALLMENT_NUMBER)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_ENTIRE_RECORD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ENTIRE_RECORD)
            .AddParameter(PAR_I_NAME_INVOICE_LOADED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_LOADED)
            .AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_CODE)
            .AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_REASON)
            .AddParameter(PAR_I_NAME_REJECT_MSG_PARMS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_MSG_PARMS)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_MOBILE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MOBILE_NUMBER)
            .AddParameter(PAR_I_NAME_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MANUFACTURER)
            .AddParameter(PAR_I_NAME_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODEL)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SUBSCRIBER_NUMBER)
            .AddParameter(PAR_I_NAME_POST_PRE_PAID, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POST_PRE_PAID)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)

        End With

        End Sub
#End Region

    End Class
