'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/29/2020)********************
Imports System.Collections.Generic

Public Class ArInvoiceHeaderDal
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AR_INVOICE_HEADER"
    Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_HEADER_ID
	
    Public Const COL_NAME_INVOICE_HEADER_ID As String = "invoice_header_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOCE_DUE_DATE As String = "invoce_due_date"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REFERENCE As String = "reference"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_BILL_TO_ADDRESS_ID As String = "bill_to_address_id"
    Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
    Public Const COL_NAME_CURRENCY_CODE As String = "currency_code"
    Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
    Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
    Public Const COL_NAME_INVOICE_OPEN_AMOUNT As String = "invoice_open_amount"
    Public Const COL_NAME_ACCT_PERIOD As String = "acct_period"
    Public Const COL_NAME_DISTRIBUTED As String = "distributed"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DOC_TYPE As String = "doc_type"
    Public Const COL_NAME_POSTED As String = "posted"
    Public Const COL_NAME_NO_OF_TIMES_PYMT_REJECTED As String = "no_of_times_pymt_rejected"
    Public Const COL_NAME_PAYMENT_METHOD_XCD As String = "payment_method_xcd"
    Public Const COL_NAME_CREDIT_MEMO_AMOUNT As String = "credit_memo_amount"
    Public Const COL_NAME_STATUS_XCD As String = "status_xcd"
    Public Const COL_NAME_DOC_UNIQUE_IDENTIFIER As String = "doc_unique_identifier"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_COMMENTS As String = "comments"
    
    Public Const PAR_I_NAME_INVOICE_HEADER_ID As String = "pi_invoice_header_id"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_INVOICE_DATE As String = "pi_invoice_date"
    Public Const PAR_I_NAME_INVOCE_DUE_DATE As String = "pi_invoce_due_date"
    Public Const PAR_I_NAME_INSTALLMENT_NUMBER As String = "pi_installment_number"
    Public Const PAR_I_NAME_SOURCE As String = "pi_source"
    Public Const PAR_I_NAME_REFERENCE As String = "pi_reference"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_BILL_TO_ADDRESS_ID As String = "pi_bill_to_address_id"
    Public Const PAR_I_NAME_SHIP_TO_ADDRESS_ID As String = "pi_ship_to_address_id"
    Public Const PAR_I_NAME_CURRENCY_CODE As String = "pi_currency_code"
    Public Const PAR_I_NAME_EXCHANGE_RATE As String = "pi_exchange_rate"
    Public Const PAR_I_NAME_INVOICE_AMOUNT As String = "pi_invoice_amount"
    Public Const PAR_I_NAME_INVOICE_OPEN_AMOUNT As String = "pi_invoice_open_amount"
    Public Const PAR_I_NAME_ACCT_PERIOD As String = "pi_acct_period"
    Public Const PAR_I_NAME_DISTRIBUTED As String = "pi_distributed"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_COMPANY_ID As String = "pi_company_id"
    Public Const PAR_I_NAME_DOC_TYPE As String = "pi_doc_type"
    Public Const PAR_I_NAME_POSTED As String = "pi_posted"
    Public Const PAR_I_NAME_NO_OF_TIMES_PYMT_REJECTED As String = "pi_no_of_times_pymt_rejected"
    Public Const PAR_I_NAME_PAYMENT_METHOD_XCD As String = "pi_payment_method_xcd"
    Public Const PAR_I_NAME_CREDIT_MEMO_AMOUNT As String = "pi_credit_memo_amount"
    Public Const PAR_I_NAME_STATUS_XCD As String = "pi_status_xcd"
    Public Const PAR_I_NAME_DOC_UNIQUE_IDENTIFIER As String = "pi_doc_unique_identifier"
    Public Const PAR_I_NAME_BANK_INFO_ID As String = "pi_bank_info_id"
    Public Const PAR_I_NAME_COMMENTS As String = "pi_comments"

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

    Public Sub Load(familyDs As DataSet, id As Guid)
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TABLE_NAME, familyDs)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function    
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
		If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            Update(ds.Tables(TABLE_NAME), CType(transaction, OracleTransaction), changesFilter)
        End If
    End Sub
    
    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException() 
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOCE_DUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOCE_DUE_DATE)
            .AddParameter(PAR_I_NAME_INSTALLMENT_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INSTALLMENT_NUMBER)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_BILL_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_BILL_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_SHIP_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIP_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_CURRENCY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_CODE)
            .AddParameter(PAR_I_NAME_EXCHANGE_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXCHANGE_RATE)
            .AddParameter(PAR_I_NAME_INVOICE_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_AMOUNT)
            .AddParameter(PAR_I_NAME_INVOICE_OPEN_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_OPEN_AMOUNT)
            .AddParameter(PAR_I_NAME_ACCT_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCT_PERIOD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_COMPANY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_DOC_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOC_TYPE)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_NO_OF_TIMES_PYMT_REJECTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NO_OF_TIMES_PYMT_REJECTED)
            .AddParameter(PAR_I_NAME_PAYMENT_METHOD_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_METHOD_XCD)
            .AddParameter(PAR_I_NAME_CREDIT_MEMO_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_CREDIT_MEMO_AMOUNT)
            .AddParameter(PAR_I_NAME_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter(PAR_I_NAME_DOC_UNIQUE_IDENTIFIER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOC_UNIQUE_IDENTIFIER)
            .AddParameter(PAR_I_NAME_BANK_INFO_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_BANK_INFO_ID)
            .AddParameter(PAR_I_NAME_COMMENTS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMENTS)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_INVOICE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_DATE)
            .AddParameter(PAR_I_NAME_INVOCE_DUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOCE_DUE_DATE)
            .AddParameter(PAR_I_NAME_INSTALLMENT_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INSTALLMENT_NUMBER)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_BILL_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_BILL_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_SHIP_TO_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SHIP_TO_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_CURRENCY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_CODE)
            .AddParameter(PAR_I_NAME_EXCHANGE_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXCHANGE_RATE)
            .AddParameter(PAR_I_NAME_INVOICE_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_AMOUNT)
            .AddParameter(PAR_I_NAME_INVOICE_OPEN_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INVOICE_OPEN_AMOUNT)
            .AddParameter(PAR_I_NAME_ACCT_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCT_PERIOD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_COMPANY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID)
            .AddParameter(PAR_I_NAME_DOC_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOC_TYPE)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_NO_OF_TIMES_PYMT_REJECTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NO_OF_TIMES_PYMT_REJECTED)
            .AddParameter(PAR_I_NAME_PAYMENT_METHOD_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_METHOD_XCD)
            .AddParameter(PAR_I_NAME_CREDIT_MEMO_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_CREDIT_MEMO_AMOUNT)
            .AddParameter(PAR_I_NAME_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter(PAR_I_NAME_DOC_UNIQUE_IDENTIFIER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOC_UNIQUE_IDENTIFIER)
            .AddParameter(PAR_I_NAME_BANK_INFO_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_BANK_INFO_ID)
            .AddParameter(PAR_I_NAME_COMMENTS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_COMMENTS)
        End With
        
    End Sub

#End Region

#Region "Invoice processing methods"
    Public Sub UpdateReviewDecisions(ByVal invoiceIds As List(Of Guid), 
                                     ByVal strReviewDecision As String, 
                                     ByVal strReviewComments As String,
                                     ByVal strUserId As String,
                                     ByRef errCode As Integer, 
                                     ByRef errMsg As String)

        Dim strStmt As String = Config("/SQL/UPDATE_REVIEW_DECISIONS")

        errCode = 0
        errMsg = String.Empty
        Try
            Using conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
                conn.Open
                Using cmd As OracleCommand = CType(conn.CreateCommand(), OracleCommand)
                    Using tran As OracleTransaction = CType(conn.BeginTransaction(IsolationLevel.ReadCommitted), OracleTransaction)
                        cmd.Transaction = tran

                        cmd.CommandText = strStmt
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.BindByName = True

                        'input parameters
                        cmd.Parameters.Add("pi_review_decision", OracleDbType.Varchar2, 100).Value = strReviewDecision
                        cmd.Parameters.Add("pi_review_comments", OracleDbType.Varchar2, 500).Value = strReviewComments
                        cmd.Parameters.Add("pi_user", OracleDbType.Varchar2, 10).Value = strUserId 

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

                        Dim paramIds As OracleParameter = cmd.Parameters.Add("pi_invoice_header_ids", OracleDbType.Varchar2)
                        paramIds.Direction = ParameterDirection.Input
                        paramIds.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                        paramIds.Value = arrayIds
                        paramIds.Size = invoiceIds.Count
                        paramIds.ArrayBindSize = arrayIdsSize

                        'output parameters
                        Dim paramErrCode As OracleParameter = cmd.Parameters.Add("po_error_code", OracleDbType.Int32, ParameterDirection.Output)
                        paramErrCode.Size = 25

                        Dim paramErrMsg As OracleParameter = cmd.Parameters.Add("po_error_msg", OracleDbType.Varchar2, ParameterDirection.Output)
                        paramErrMsg.Size = 3000

                        cmd.ExecuteNonQuery

                        errCode = CType(paramErrCode.Value.ToString, Integer)
                        errMsg = paramErrMsg.Value.ToString

                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Data query methods"
    Private Function DB_OracleCommand(ByVal pSqlStatement As String, ByVal pCommandType As CommandType) As OracleCommand
        Dim conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
        Dim cmd As OracleCommand = CType(conn.CreateCommand(), OracleCommand)

        cmd.CommandText = pSqlStatement
        cmd.CommandType = pCommandType

        Return cmd
    End Function

    Public sub SearchArInvoices(ByVal companyId As Guid, ByVal dealerId As Guid?, 
                                ByVal invoiceNum As String, ByVal source As String, 
                                ByVal invoiceDate As Date?, ByVal reference As String, 
                                ByVal referenceNumber As String, ByVal documentType As String,
                                ByVal documentUniqueId As String, ByVal statusXcd As String,
                                ByVal rowCountReturn As Integer, ByVal userId As Guid,
                                ByRef searchResult As DataSet
                                )

        Dim selectStmt As String = Config("/SQL/SEARCH_AR_INVOICES")
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            'output parameters
            cmd.Parameters.Add("po_results", OracleDbType.RefCursor, ParameterDirection.Output)

            'input parameters
            cmd.Parameters.Add("pi_company_id", OracleDbType.Raw).Value = companyId.ToByteArray
            cmd.Parameters.Add("pi_user_id", OracleDbType.Raw).Value = userId.ToByteArray
            cmd.Parameters.Add("pi_rowcount", OracleDbType.Int32).Value = rowCountReturn

            If dealerId.HasValue Then
                cmd.Parameters.Add("pi_dealer_id", OracleDbType.Raw).Value = dealerId.value.ToByteArray
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

            If Not String.IsNullOrWhiteSpace(reference) AndAlso Not String.IsNullOrWhiteSpace(referenceNumber) Then
                cmd.Parameters.Add("pi_reference", OracleDbType.Varchar2).Value = reference.Trim
                cmd.Parameters.Add("pi_reference_number", OracleDbType.Varchar2).Value = referenceNumber.Trim
            End If

            If (String.IsNullOrWhiteSpace(documentType) = False) Then
                cmd.Parameters.Add("pi_doc_type", OracleDbType.Varchar2).Value = documentType.Trim
            End If

            If (String.IsNullOrWhiteSpace(documentUniqueId) = False) Then
                cmd.Parameters.Add("pi_doc_unique_id", OracleDbType.Varchar2).Value = documentUniqueId.Trim
            End If

            If (String.IsNullOrWhiteSpace(statusXcd) = False) Then
                cmd.Parameters.Add("pi_status_xcd", OracleDbType.Varchar2).Value = statusXcd.Trim
            End If

            da = New OracleDataAdapter(cmd)
            da.Fill(searchResult, "SEARCH_RESULT")
            searchResult.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub

    Public sub GetArInvoiceLinesByHeaderId(ByVal invoiceHeaderId As Guid, ByVal languageId As Guid, ByRef invoiceLines As DataSet)
        Dim selectStmt As String = Config("/SQL/GET_AR_INVOICE_LINES")
        Dim da As OracleDataAdapter
        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            'output parameters
            cmd.Parameters.Add("po_cur_lines", OracleDbType.RefCursor, ParameterDirection.Output)

            'input parameters
            cmd.Parameters.Add("pi_invoice_header_id", OracleDbType.Raw).Value = invoiceHeaderId.ToByteArray
            cmd.Parameters.Add("pi_language_id", OracleDbType.Raw).Value = languageId.ToByteArray

            da = New OracleDataAdapter(cmd)
            da.Fill(invoiceLines, "INVOICE_LINES")
            invoiceLines.Locale = Globalization.CultureInfo.InvariantCulture

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End sub

#End Region
End Class


