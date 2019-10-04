'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2019)********************


Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class ApInvoiceHeaderDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AP_INVOICE_HEADER"
    Public Const TABLE_KEY_NAME As String = "ap_invoice_header_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ap_invoice_header_id", id.ToByteArray)}
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

#Region "Data query methods"
    Private Function DB_OracleCommand(ByVal p_SqlStatement As String, ByVal p_CommandType As CommandType) As OracleCommand
        Dim conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
        Dim cmd As OracleCommand = conn.CreateCommand()

        cmd.CommandText = p_SqlStatement
        cmd.CommandType = p_CommandType

        Return cmd
    End Function

    Public sub SearchAPInvoices(ByVal vendorCode As String, ByVal invoiceNum As String,
                                ByVal source As String, ByVal invoiceDate As Date?,
                                ByVal dueDateFrom As Date?, ByVal dueDateTo As Date?,
                                ByVal rowCountReturn As Integer, ByVal userId As Guid,
                                ByRef errCode As Integer, ByRef errMsg As String,
                                ByRef searchResult As DataSet
                                )

        Dim selectStmt As String = Me.Config("/SQL/SEARCH_AP_INVOICES")
        Dim ds As New DataSet
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            'output parameters
            cmd.Parameters.Add("po_error_code", OracleDbType.Int32, ParameterDirection.Output)
            cmd.Parameters.Add("po_error_msg", OracleDbType.Varchar2, 500, nothing, ParameterDirection.Output)
            cmd.Parameters.Add("po_results", OracleDbType.RefCursor, ParameterDirection.Output)
            
            'input parameters
            cmd.Parameters.Add("pi_user_id", OracleDbType.Raw).Value = userId.ToByteArray
            cmd.Parameters.Add("pi_rowcount", OracleDbType.Int32).Value = rowCountReturn
            
            If not String.IsNullOrWhiteSpace(vendorCode) Then
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
            ds.Locale = Globalization.CultureInfo.InvariantCulture
            
            If  String.IsNullOrEmpty(cmd.Parameters("po_error_msg").Value) Then
                errCode = cmd.Parameters("po_error_code").Value
                errMsg = cmd.Parameters("po_error_msg").Value
            Else
                errCode = 0
                errMsg = String.Empty
            End If            

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        
    End sub
#End Region

End Class


