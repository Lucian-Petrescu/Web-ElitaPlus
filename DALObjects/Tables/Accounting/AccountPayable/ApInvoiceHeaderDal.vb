'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/21/2019)********************


Imports System.Collections.Generic

Public Class ApInvoiceHeaderDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_AP_INVOICE_HEADER"
    Public Const TABLE_KEY_NAME As String = COL_NAME_AP_INVOICE_HEADER_ID

    Public Const COL_NAME_AP_INVOICE_HEADER_ID As String = "ap_invoice_header_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
    Public Const COL_NAME_TERM_XCD As String = "term_xcd"
    Public Const COL_NAME_PAID_AMOUNT As String = "paid_amount"
    Public Const COL_NAME_PAYMENT_STATUS_XCD As String = "payment_status_xcd"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_VENDOR_ID As String = "vendor_id"
    Public Const COL_NAME_VENDOR_ADDRESS_ID As String = "vendor_address_id"
    Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
    Public Const COL_NAME_CURRENCY_ISO_CODE As String = "currency_iso_code"
    Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
    Public Const COL_NAME_APPROVED_XCD As String = "approved_xcd"
    Public Const COL_NAME_ACCOUNTING_PERIOD As String = "accounting_period"
    Public Const COL_NAME_DISTRIBUTED As String = "distributed"
    Public Const COL_NAME_POSTED As String = "posted"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"

    Public Const PAR_I_NAME_AP_INVOICE_HEADER_ID As String = "pi_ap_invoice_header_id"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_INVOICE_DATE As String = "pi_invoice_date"
    Public Const PAR_I_NAME_INVOICE_AMOUNT As String = "pi_invoice_amount"
    Public Const PAR_I_NAME_TERM_XCD As String = "pi_term_xcd"
    Public Const PAR_I_NAME_PAID_AMOUNT As String = "pi_paid_amount"
    Public Const PAR_I_NAME_PAYMENT_STATUS_XCD As String = "pi_payment_status_xcd"
    Public Const PAR_I_NAME_SOURCE As String = "pi_source"
    Public Const PAR_I_NAME_VENDOR_ID As String = "pi_vendor_id"
    Public Const PAR_I_NAME_VENDOR_ADDRESS_ID As String = "pi_vendor_address_id"
    Public Const PAR_I_NAME_SHIP_TO_ADDRESS_ID As String = "pi_ship_to_address_id"
    Public Const PAR_I_NAME_CURRENCY_ISO_CODE As String = "pi_currency_iso_code"
    Public Const PAR_I_NAME_EXCHANGE_RATE As String = "pi_exchange_rate"
    Public Const PAR_I_NAME_APPROVED_XCD As String = "pi_approved_xcd"
    Public Const PAR_I_NAME_ACCOUNTING_PERIOD As String = "pi_accounting_period"
    Public Const PAR_I_NAME_DISTRIBUTED As String = "pi_distributed"
    Public Const PAR_I_NAME_POSTED As String = "pi_posted"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_COMPANY_ID As String = "pi_company_id"

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

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
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

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_AMOUNT)
            .AddParameter(PAR_I_NAME_TERM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TERM_XCD)
            .AddParameter(PAR_I_NAME_PAID_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_AMOUNT)
            .AddParameter(PAR_I_NAME_PAYMENT_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_STATUS_XCD)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_VENDOR_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ID)
            .AddParameter(PAR_I_NAME_VENDOR_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_SHIP_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIP_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_CURRENCY_ISO_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_ISO_CODE)
            .AddParameter(PAR_I_NAME_EXCHANGE_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXCHANGE_RATE)
            .AddParameter(PAR_I_NAME_APPROVED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_APPROVED_XCD)
            .AddParameter(PAR_I_NAME_ACCOUNTING_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCOUNTING_PERIOD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_COMPANY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_AMOUNT)
            .AddParameter(PAR_I_NAME_TERM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TERM_XCD)
            .AddParameter(PAR_I_NAME_PAID_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_AMOUNT)
            .AddParameter(PAR_I_NAME_PAYMENT_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_STATUS_XCD)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_VENDOR_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ID)
            .AddParameter(PAR_I_NAME_VENDOR_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_SHIP_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIP_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_CURRENCY_ISO_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_ISO_CODE)
            .AddParameter(PAR_I_NAME_EXCHANGE_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXCHANGE_RATE)
            .AddParameter(PAR_I_NAME_APPROVED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_APPROVED_XCD)
            .AddParameter(PAR_I_NAME_ACCOUNTING_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCOUNTING_PERIOD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_COMPANY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub

#End Region


#Region "Data query methods"
    Private Function DB_OracleCommand(ByVal p_SqlStatement As String, ByVal p_CommandType As CommandType) As OracleCommand
        Dim conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
        Dim cmd As OracleCommand = conn.CreateCommand()

        cmd.CommandText = p_SqlStatement
        cmd.CommandType = p_CommandType

        Return cmd
    End Function

    Public Sub SearchAPInvoices(ByVal vendorCode As String, ByVal invoiceNum As String,
                                ByVal source As String, ByVal invoiceDate As Date?,
                                ByVal dueDateFrom As Date?, ByVal dueDateTo As Date?,
                                ByVal rowCountReturn As Integer, ByVal userId As Guid,
                                ByRef searchResult As DataSet
                                )

        Dim selectStmt As String = Config("/SQL/SEARCH_AP_INVOICES")
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            'output parameters
            cmd.Parameters.Add("po_results", OracleDbType.RefCursor, ParameterDirection.Output)

            'input parameters
            cmd.Parameters.Add("pi_user_id", OracleDbType.Raw).Value = userId.ToByteArray
            cmd.Parameters.Add("pi_rowcount", OracleDbType.Int32).Value = rowCountReturn

            If Not String.IsNullOrWhiteSpace(vendorCode) Then
                cmd.Parameters.Add("pi_vendor_code", OracleDbType.Varchar2).Value = vendorCode.Trim
            End If

            If (String.IsNullOrWhiteSpace(invoiceNum) = False) Then
                cmd.Parameters.Add("pi_invoice_number", OracleDbType.Varchar2).Value = invoiceNum.Trim
            End If

            If (String.IsNullOrWhiteSpace(source) = False) Then
                cmd.Parameters.Add("pi_source", OracleDbType.Varchar2).Value = source.Trim
            End If

            If (invoiceDate.HasValue) Then
                cmd.Parameters.Add("pi_invoice_date", OracleDbType.Date).Value = invoiceDate.Value
            End If

            If (dueDateFrom.HasValue) Then
                cmd.Parameters.Add("pi_due_date_from", OracleDbType.Date).Value = dueDateFrom.Value
            End If

            If (dueDateTo.HasValue) Then
                cmd.Parameters.Add("pi_due_date_to", OracleDbType.Date).Value = dueDateTo.Value
            End If

            da = New OracleDataAdapter(cmd)
            da.Fill(searchResult, "SEARCH_RESULT")
            searchResult.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub

    Public Sub SearchAPInvoice(ByVal invoiceNum As String, ByRef searchResult As DataSet)

        Dim selectStmt As String = Config("/SQL/SEARCH_AP_INVOICE")
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("po_results", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("pi_invoice_number", OracleDbType.Varchar2).Value = invoiceNum.Trim
            da = New OracleDataAdapter(cmd)
            da.Fill(searchResult, "SEARCH_RESULT")
            searchResult.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub


    Public Sub LoadAPInvoiceExtendedInfo(ByVal invoiceId As Guid, ByRef searchResult As DataSet)

        Dim selectStmt As String = Config("/SQL/GET_AP_INVOICE_EXTENDED_INFO")
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("po_cur_result", OracleDbType.RefCursor, ParameterDirection.Output)

            'input parameters
            cmd.Parameters.Add("pi_invoice_header_id", OracleDbType.Raw).Value = invoiceId.ToByteArray

            da = New OracleDataAdapter(cmd)
            da.Fill(searchResult, "AP_INVOICE_HEADER_EXT")
            searchResult.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Sub LoadAPInvoiceLines(ByVal invoiceId As Guid,
                                  ByVal minLineNum As Integer,
                                  ByVal maxLineNum As Integer,
                                  ByVal UnMatchedLineOnly As Boolean,
                                  ByVal languageId As Guid,
                                  ByVal rowCountReturn As Integer,
                                  ByRef searchResult As DataSet)

        Dim selectStmt As String = Config("/SQL/GET_AP_INVOICE_LINES")
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("po_cur_lines", OracleDbType.RefCursor, ParameterDirection.Output)

            'input parameters
            cmd.Parameters.Add("pi_invoice_header_id", OracleDbType.Raw).Value = invoiceId.ToByteArray
            cmd.Parameters.Add("pi_rowcount", OracleDbType.Int32).Value = rowCountReturn
            cmd.Parameters.Add("pi_min_line_number", OracleDbType.Int32).Value = minLineNum
            cmd.Parameters.Add("pi_max_line_number", OracleDbType.Int32).Value = maxLineNum
            cmd.Parameters.Add("pi_language_id", OracleDbType.Raw).Value = languageId.ToByteArray

            If UnMatchedLineOnly = True Then
                cmd.Parameters.Add("pi_unmatched_line", OracleDbType.Varchar2).Value = "Y"
            Else
                cmd.Parameters.Add("pi_unmatched_line", OracleDbType.Varchar2).Value = "N"
            End If

            da = New OracleDataAdapter(cmd)
            da.Fill(searchResult, "AP_INVOICE_LINES")
            searchResult.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

#End Region

#Region "Invoice processing methods"
    Public Sub DeleteInvoices(ByVal invoiceIds As Generic.List(Of Guid))
        'Dim strStmt As String = Config("/SQL/DELETE_AP_INVOICE")
        Dim strStmt As String = "begin elita.elp_ap_invoice_processing.delete_invoice(:pi_invoice_header_id); end;"

        Dim tr As OracleTransaction = Nothing
        Try
            Using conn As OracleConnection = DBHelper.GetConnection()
                Using command As OracleCommand = conn.CreateCommand

                    Dim batchCnt As Integer = 0
                    Dim strInvoiceIds(invoiceIds.Count - 1) As String

                    For Each guidTemp As Guid In invoiceIds

                        strInvoiceIds(batchCnt) = GuidControl.GuidToHexString(guidTemp)
                        batchCnt = batchCnt + 1
                    Next

                    Dim paranInvIds As OracleParameter = New OracleParameter("pi_invoice_header_id", OracleDbType.Varchar2, 100)
                    paranInvIds.Value = strInvoiceIds

                    tr = conn.BeginTransaction
                    command.CommandText = strStmt

                    command.ArrayBindCount = strInvoiceIds.Count()

                    command.Parameters.Add(paranInvIds)
                    command.ExecuteNonQuery()
                    tr.Commit
                    conn.Close()
                End Using
            End Using
        Catch ex As Exception
            If Not tr Is Nothing Then
                tr.Rollback
            End If
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub MatchInvoice(ByVal invoiceId As Guid, ByRef matchedCount As Integer)
        Dim strStmt As String = Config("/SQL/MATCH_AP_INVOICE")
        Try
            Using conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
                conn.Open
                Using cmd As OracleCommand = conn.CreateCommand()
                    cmd.CommandText = strStmt
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True

                    'input parameters
                    cmd.Parameters.Add("pi_invoice_header_id", OracleDbType.Raw).Value = invoiceId.ToByteArray
                    cmd.Parameters.Add("pi_commit", OracleDbType.Varchar2, 10).Value = "Y"
                    'output parameters
                    Dim param_match_count As OracleParameter = New OracleParameter()
                    param_match_count = cmd.Parameters.Add("po_matched_count", OracleDbType.Int32, ParameterDirection.Output)
                    param_match_count.Size = 25

                    cmd.ExecuteNonQuery

                    matchedCount = CType(param_match_count.Value.ToString, Integer)
                End Using
                conn.Close
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub PayInvoices(ByVal strBatchNumber As String, ByVal invoiceIds As List(Of Guid), ByRef errCode As Integer, ByRef errMsg As String)
        Dim strStmt As String = Config("/SQL/PAY_AP_INVOICE")

        errCode = 0
        errMsg = String.Empty
        Dim blnSuccess As Boolean = True
        Try
            Using conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
                conn.Open
                Using cmd As OracleCommand = conn.CreateCommand()
                    Using tran As OracleTransaction = conn.BeginTransaction(IsolationLevel.ReadCommitted)
                        cmd.Transaction = tran

                        cmd.CommandText = strStmt
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.BindByName = True

                        'input parameters
                        cmd.Parameters.Add("pi_batch_num", OracleDbType.Varchar2, 100).Value = strBatchNumber
                        cmd.Parameters.Add("pi_close_batch", OracleDbType.Varchar2, 10).Value = "Y" 'Close the payment batch 
                        cmd.Parameters.Add("pi_commit", OracleDbType.Varchar2, 10).Value = "Y" ' Commit changes

                        'id array parameters

                        ' Setup the values for PL/SQL Associative Array
                        Dim arrayIds(invoiceIds.Count - 1) As String
                        Dim arrayIdsSize(invoiceIds.Count - 1) As Integer
                        Dim batchCnt As Integer = 0
                        For Each guidTemp As Guid In invoiceIds
                            arrayIds(batchCnt) = GuidControl.GuidToHexString(guidTemp)
                            arrayIdsSize(batchCnt) = 50
                            batchCnt = batchCnt + 1
                        Next

                        Dim paramIds As OracleParameter = New OracleParameter()
                        paramIds = cmd.Parameters.Add("pi_invoice_header_ids", OracleDbType.Varchar2)
                        paramIds.Direction = ParameterDirection.Input
                        paramIds.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        paramIds.Value = arrayIds
                        paramIds.Size = invoiceIds.Count
                        paramIds.ArrayBindSize = arrayIdsSize

                        'output parameters
                        Dim param_err_code As OracleParameter = New OracleParameter()
                        param_err_code = cmd.Parameters.Add("po_error_code", OracleDbType.Int32, ParameterDirection.Output)
                        param_err_code.Size = 25

                        Dim param_err_msg As OracleParameter = New OracleParameter()
                        param_err_msg = cmd.Parameters.Add("po_error_msg", OracleDbType.Varchar2, ParameterDirection.Output)
                        param_err_msg.Size = 500

                        cmd.ExecuteNonQuery

                        errCode = CType(param_err_code.Value.ToString, Integer)
                        errMsg = param_err_msg.Value.ToString

                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

End Class


