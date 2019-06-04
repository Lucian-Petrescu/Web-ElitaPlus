'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/20/2015)********************


Public Class AfaInvoiceManaulDataDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_INVOICE_MANAUL_DATA"
    Public Const TABLE_KEY_NAME As String = "afa_invoice_manual_data_id"

    Public Const COL_NAME_AFA_INVOICE_MANUAL_DATA_ID As String = "afa_invoice_manual_data_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_INVOICE_MONTH As String = "invoice_month"
    Public Const COL_NAME_DATA_AMOUNT As String = "data_amount"
    Public Const COL_NAME_AMOUNT_TYPE_CODE As String = "amount_type_code"
    Public Const COL_NAME_DATA_TEXT As String = "data_text"
    Public Const COL_NAME_DATA_TEXT2 As String = "data_text2"
    Public Const COL_NAME_DATA_TEXT3 As String = "data_text3"
    Public Const COL_NAME_DATA_TEXT4 As String = "data_text4"
    Public Const COL_NAME_DATA_DATE As String = "data_date"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_invoice_manual_data_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadListByDealer(ByVal dealerID As Guid, ByVal PeriodYear As String, ByVal PeriodMonth As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}
        Dim ds As New DataSet

        Dim inClauseCondition As String = String.Empty

        If PeriodYear <> String.Empty AndAlso PeriodMonth <> String.Empty Then
            inClauseCondition += " and md.INVOICE_MONTH = '" & PeriodYear & PeriodMonth & "'"
        ElseIf PeriodYear <> String.Empty AndAlso PeriodMonth = String.Empty Then
            inClauseCondition += " and md.INVOICE_MONTH like '" & PeriodYear & "%'"
        ElseIf PeriodYear = String.Empty AndAlso PeriodMonth <> String.Empty Then
            inClauseCondition += " and md.INVOICE_MONTH like '%" & PeriodMonth & "'"
        End If

        Try
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadListByTypeForPeriod(ByVal DealerID As Guid, ByVal ManualDataType As String, ByVal InvoiceMonthStart As String, ByVal InvoiceMonthEnd As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_TYPE")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, DealerID.ToByteArray), _
                                                                                    New DBHelper.DBHelperParameter(COL_NAME_AMOUNT_TYPE_CODE, ManualDataType)}
        Dim ds As New DataSet

        Dim inClauseCondition As String = String.Empty

        If InvoiceMonthStart <> String.Empty Then
            inClauseCondition += " and invoice_month >= '" & InvoiceMonthStart & "'"        
        End If

        If InvoiceMonthEnd <> String.Empty Then
            inClauseCondition += " and invoice_month <= '" & InvoiceMonthEnd & "'"
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadPONumberListByDealer(ByVal dealerID As Guid, ByVal PeriodGreaterThan As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_PONumber")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}
        Dim ds As New DataSet

        Dim inClauseCondition As String = String.Empty

        If PeriodGreaterThan <> String.Empty Then
            inClauseCondition += " and md.INVOICE_MONTH >= '" & PeriodGreaterThan & "'"        
        End If

        Try
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadDealerMonthlyRecords(ByVal dealerID As Guid, ByVal AccountingMonth As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_MONTHLY_AMOUNTS")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
                                                                                     New DBHelper.DBHelperParameter("invoice_month", AccountingMonth)}
        Dim ds As New DataSet

        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function StartInvoiceProcess(ByVal dealerId As Guid, ByVal BillingMonth As String, ByVal userName As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/START_INVOICE_PROCESS")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray), _
                    New DBHelper.DBHelperParameter("pi_BillingMonthYYYYMM", BillingMonth), _
                    New DBHelper.DBHelperParameter("pi_userName", userName)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("po_Result", GetType(String))}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            If CType(outputParameters(0).Value, String).Trim = "N" Then
                Return False
            ElseIf CType(outputParameters(0).Value, String).Trim = "Y" Then
                Return True
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerInvoiceDates(ByVal dealerID As Guid, ByVal AccountingMonth As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_INVOICE_DATES")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_month", AccountingMonth), _
                                                                                     New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}
        Dim ds As New DataSet

        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Sub UpdateInvoiceWithManualDates(ByVal dealerId As Guid, ByVal BillingMonth As String, ByRef strMsg As String)

        Dim selectStmt As String = Me.Config("/SQL/UPDATE_INVOICE_WITH_MANUAL_DATES")


        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("pi_dealerID", dealerId.ToByteArray), _
                    New DBHelper.DBHelperParameter("pi_billingPeriod", BillingMonth)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("po_Message", GetType(String), 1000)}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            strMsg = CType(outputParameters(0).Value, String).Trim
            
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
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