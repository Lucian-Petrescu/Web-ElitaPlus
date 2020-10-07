'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/21/2019)********************
Imports System.Collections.Generic

Public Class ApInvoiceLinesDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AP_INVOICE_LINES"
    Public Const TABLE_KEY_NAME As String = COL_NAME_AP_INVOICE_LINES_ID

    Public Const COL_NAME_AP_INVOICE_LINES_ID As String = "ap_invoice_lines_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_AP_INVOICE_HEADER_ID As String = "ap_invoice_header_id"
    Public Const COL_NAME_LINE_NUMBER As String = "line_number"
    Public Const COL_NAME_LINE_TYPE As String = "line_type"
    Public Const COL_NAME_VENDOR_ITEM_CODE As String = "vendor_item_code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_QUANTITY As String = "quantity"
    Public Const COL_NAME_UOM_XCD As String = "uom_xcd"
    Public Const COL_NAME_MATCHED_QUANTITY As String = "matched_quantity"
    Public Const COL_NAME_PAID_QUANTITY As String = "paid_quantity"
    Public Const COL_NAME_UNIT_PRICE As String = "unit_price"
    Public Const COL_NAME_TOTAL_PRICE As String = "total_price"
    Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
    Public Const COL_NAME_PO_NUMBER As String = "po_number"
    Public Const COL_NAME_PO_DATE As String = "po_date"
    Public Const COL_NAME_BILLING_PERIOD_START_DATE As String = "billing_period_start_date"
    Public Const COL_NAME_BILLING_PERIOD_END_DATE As String = "billing_period_end_date"
    Public Const COL_NAME_REFERENCE_NUMBER As String = "reference_number"
    Public Const COL_NAME_VENDOR_TRANSACTION_TYPE As String = "vendor_transaction_type"
    
    Public Const PAR_I_NAME_AP_INVOICE_LINES_ID As String = "pi_ap_invoice_lines_id"
    Public Const PAR_I_NAME_AP_INVOICE_HEADER_ID As String = "pi_ap_invoice_header_id"
    Public Const PAR_I_NAME_LINE_NUMBER As String = "pi_line_number"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_LINE_TYPE As String = "pi_line_type"
    Public Const PAR_I_NAME_VENDOR_ITEM_CODE As String = "pi_vendor_item_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_QUANTITY As String = "pi_quantity"
    Public Const PAR_I_NAME_UOM_XCD As String = "pi_uom_xcd"
    Public Const PAR_I_NAME_MATCHED_QUANTITY As String = "pi_matched_quantity"
    Public Const PAR_I_NAME_PAID_QUANTITY As String = "pi_paid_quantity"
    Public Const PAR_I_NAME_UNIT_PRICE As String = "pi_unit_price"
    Public Const PAR_I_NAME_TOTAL_PRICE As String = "pi_total_price"
    Public Const PAR_I_NAME_PARENT_LINE_NUMBER As String = "pi_parent_line_number"
    Public Const PAR_I_NAME_PO_NUMBER As String = "pi_po_number"
    Public Const PAR_I_NAME_PO_DATE As String = "pi_po_date"
    Public Const PAR_I_NAME_BILLING_PERIOD_START_DATE As String = "pi_billing_period_start_date"
    Public Const PAR_I_NAME_BILLING_PERIOD_END_DATE As String = "pi_billing_period_end_date"
    Public Const PAR_I_NAME_REFERENCE_NUMBER As String = "pi_reference_number"
    Public Const PAR_I_NAME_VENDOR_TRANSACTION_TYPE As String = "pi_vendor_transaction_type"
    Public Const PAR_I_NAME_SERVICE_CENTER_ID As String = "pi_service_center_id"
    Public Const PAR_I_NAME_CLAIM_NUMBER As String = "pi_claim_number"
    Public Const PAR_I_NAME_AUTHORIZATION_NUMBER As String = "pi_authorization_number"
    Public Const PAR_I_NAME_COMPANY_ID As String = "pi_company_id"
    Public Const PAR_I_NAME_CLAIM_IDS As String = "pi_company_id"
    Public Const PAR_I_NAME_AUTHORIZATION_IDS As String = "pi_claim_authorization_ids"
    Public Const PAR_O_AP_INVOICE_LINES_CUR As String = "po_ResultCursor"

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
    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadList(apInvoiceHeaderId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, apInvoiceHeaderId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PAR_O_AP_INVOICE_LINES_CUR, GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetApInvoiceLines(familyDS As DataSet, apInvoiceHeaderId As Guid) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOADLINES"))
                cmd.AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Raw, apInvoiceHeaderId.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
                Return familyDS
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    public Function GetAuthorization(serviceCenterId As Guid , claimNumber As String, authorizationNumber As string) As Dataset
        Try
            Dim authorizationDataSet As New DataSet
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOADAUTHS"))
                cmd.AddParameter(PAR_I_NAME_SERVICE_CENTER_ID, OracleDbType.Raw, serviceCenterId.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_CLAIM_NUMBER, OracleDbType.Varchar2, claimNumber)
                cmd.AddParameter(PAR_I_NAME_AUTHORIZATION_NUMBER, OracleDbType.Varchar2, authorizationNumber)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, authorizationDataSet)
                Return authorizationDataSet
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    public Function GetPoLines(claimAuthorizationIds As List(Of Guid)) As Dataset
        Try
            Dim authorizationDataSet As New DataSet

            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOADPOLINES"))
               
                Dim authorizationArrayIds(claimAuthorizationIds.Count - 1) As String
                Dim authArrayIdsSize(claimAuthorizationIds.Count - 1) As Integer
                Dim authbatchCnt As Integer = 0
                For Each guidTemp As Guid In claimAuthorizationIds
                    authorizationArrayIds(authbatchCnt) = GuidControl.GuidToHexString(guidTemp)
                    authArrayIdsSize(authbatchCnt) = 50
                    authbatchCnt = authbatchCnt + 1
                Next

                Dim authParamIds As OracleParameter = New OracleParameter()
                authParamIds = cmd.Parameters.Add(PAR_I_NAME_AUTHORIZATION_IDS, OracleDbType.Varchar2)
                authParamIds.Direction = ParameterDirection.Input
                authParamIds.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                authParamIds.Value = authorizationArrayIds
                authParamIds.Size = authorizationArrayIds.Count
                authParamIds.ArrayBindSize = authArrayIdsSize
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, authorizationDataSet)
                Return authorizationDataSet
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
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


    Public Sub DeleteInvoiceLine(row As DataRow)

        Dim sqlStatement As String
        Dim rowState As DataRowState = row.RowState

        Try
            If rowState = DataRowState.Deleted Then

                sqlStatement = Config("/SQL/DELETE")
                Dim inParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {
                           New DBHelper.DBHelperParameter(PAR_I_NAME_AP_INVOICE_LINES_ID.ToLower(), row(COL_NAME_AP_INVOICE_LINES_ID, DataRowVersion.Original))
                       }
                DBHelper.ExecuteSp(sqlstatement, inParameter, Nothing)
                row.AcceptChanges()
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException() 
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_INVOICE_LINES_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_LINES_ID)
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_VENDOR_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_ITEM_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QUANTITY)
            .AddParameter(PAR_I_NAME_UOM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UOM_XCD)
            .AddParameter(PAR_I_NAME_MATCHED_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_MATCHED_QUANTITY)
            .AddParameter(PAR_I_NAME_PAID_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_QUANTITY)
            .AddParameter(PAR_I_NAME_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_TOTAL_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_TOTAL_PRICE)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_PO_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PO_NUMBER)
            .AddParameter(PAR_I_NAME_PO_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PO_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_REFERENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE_NUMBER)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_VENDOR_TRANSACTION_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_TRANSACTION_TYPE)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_INVOICE_LINES_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_LINES_ID)
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_VENDOR_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_ITEM_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QUANTITY)
            .AddParameter(PAR_I_NAME_UOM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UOM_XCD)
            .AddParameter(PAR_I_NAME_MATCHED_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_MATCHED_QUANTITY)
            .AddParameter(PAR_I_NAME_PAID_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_QUANTITY)
            .AddParameter(PAR_I_NAME_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_TOTAL_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_TOTAL_PRICE)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_PO_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PO_NUMBER)
            .AddParameter(PAR_I_NAME_PO_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PO_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_REFERENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE_NUMBER)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_VENDOR_TRANSACTION_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_TRANSACTION_TYPE)
        End With
        
    End Sub
#End Region

End Class


