'**************************************************************************************
'Public Class InvoiceTransDAL
'UPDATE_BATCH_CLAIMS
'Author: Alan Ranciato
'Create Date: 10/2/2006
'
'
'Description:  Created to access the ELP_INVOICE_TRANS and INVOICE_TRANS_DETAIL tables.
'           Utilizes the ELP_CLAIM_PROCESSING Oracle Package.
'
'
'
'
'**************************************************************************************

Public Class InvoiceTransDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_TRANS"
    Public Const TABLE_KEY_NAME As String = "invoice_trans_id"

    '--Columns
    Public Const COL_NAME_INVOICE_TRANS_ID As String = "invoice_trans_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_INVOICE_TYPE_ID As String = "invoice_type_id"
    Public Const COL_NAME_INVOICE_TYPE_NAME As String = "invoice_type_desc"
    Public Const COL_NAME_INVOICE_STATUS_ID As String = "invoice_status_id"
    Public Const COL_NAME_SERVICE_CENTER_NAME As String = "description"
    Public Const COL_NAME_SVC_CONTROL_NUMBER As String = "svc_control_number"  'Invoice #
    Public Const COL_NAME_SVC_CONTROL_AMOUNT As String = "svc_control_amount"
    Public Const COL_NAME_BATCH_STATUS As String = "batch_status"
    Public Const COL_NAME_BATCH_START_TIME As String = "batch_start_time"
    Public Const COL_NAME_BATCH_PROCESS_TIME As String = "batch_process_time"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_TAX1_AMOUNT As String = "tax1_amount"
    Public Const COL_NAME_TAX2_AMOUNT As String = "tax2_amount"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_RECEIVED_DATE As String = "invoice_received_date"
    Public Const COL_NAME_INVOICE_COMMENTS As String = "invoice_comments"
    Public Const COL_NAME_INVOICE_STATUS_NAME As String = "invoice_status_desc"
    Public Const COL_NAME_INVOICE_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_USER_ID As String = "USER_ID"
    '--Parameters - Input
    Public Const P_INVOICE_TRANS_ID As String = "V_INVOICE_TRANS_ID"
    Public Const P_INVOICE_DATE As String = "V_INVOICE_DATE"
    Public Const P_SERVICE_CENTER_ID As String = "V_SERVICE_CENTER_ID"
    Public Const P_SVC_CONTROL_NUMBER As String = "V_SVC_CONTROL_NUMBER"
    Public Const P_SVC_CONTROL_AMOUNT As String = "V_SVC_CONTROL_AMOUNT"
    Public Const P_BATCH_NUMBER As String = "V_BATCH_NUMBER"
    Public Const P_USER_ID As String = "V_USER_ID"
    Public Const P_CLAIM_XMLDOC As String = "V_CLAIM_XMLDOC"
    Public Const P_INVOICE_TYPE_ID As String = "V_INVOICE_TYPE_ID"
    Public Const P_INVOICE_STATUS_ID As String = "V_INVOICE_STATUS_ID"
    Public Const P_INVOICE_RECEIVED_DATE As String = "V_INVOICE_RECEIVED_DATE"
    Public Const P_LANGUAGE_ID As String = "V_LANGUAGE_ID"
    Public Const P_CLAIMS As String = "V_CLAIMS"

    '-- Parameters - Output
    Public Const P_BATCH_INVOICE As String = "V_BATCH_INVOICE"
    Public Const P_RETURN As String = "P_RETURN"
    Public Const PO_RETURN_CODE As String = "PO_RETURN_CODE"
    Public _UserId As Guid

    Public Const P_TAXTYPE_ID As String = "p_tax_type_id"
    Public Const P_COUNTRY_ID As String = "p_country_id"

    Public Const P_IVATAXAMT As String = "p_ivatax_amt"
    Public Const P_IIBBTAXAMT As String = "p_iibbtax_amt"
    Public Const COL_NAME_INVOICE_TAX_TYPE_ID As String = "tax_type_id"

    'REQ 1150
    Public Const P_DEALER_ID As String = "p_dealer_id"

    'REQ-5565
    Public Const P_BATCHNUMBER As String = "batch_number"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

    Public Sub New(UserId As Guid)
        MyBase.new()
        _UserId = UserId
    End Sub
#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters(9) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_BATCH_INVOICE, GetType(DataSet))}

        parameters(0) = New DBHelper.DBHelperParameter(P_USER_ID, _UserId)
        parameters(1) = New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray)
        parameters(2) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, DBNull.Value, GetType(System.Guid))
        parameters(3) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, DBNull.Value, GetType(System.String))
        parameters(4) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, DBNull.Value, GetType(System.String))
        parameters(5) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, DBNull.Value, GetType(System.Decimal))
        parameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, DBNull.Value, GetType(System.DateTime))
        parameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, DBNull.Value, GetType(System.Guid))
        parameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, DBNull.Value, GetType(System.Guid))
        parameters(9) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, DBNull.Value, GetType(System.DateTime))
        
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function Load(id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters(9) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_BATCH_INVOICE, GetType(DataSet))}
        Dim ds As New DataSet

        parameters(0) = New DBHelper.DBHelperParameter(P_USER_ID, _UserId)
        parameters(1) = New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray)
        parameters(2) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, DBNull.Value, GetType(System.Guid))
        parameters(3) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, DBNull.Value, GetType(System.String))
        parameters(4) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, DBNull.Value, GetType(System.String))
        parameters(5) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, DBNull.Value, GetType(System.Decimal))
        parameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, DBNull.Value, GetType(System.DateTime))
        parameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, DBNull.Value, GetType(System.Guid))
        parameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, DBNull.Value, GetType(System.Guid))
        parameters(9) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, DBNull.Value, GetType(System.DateTime))


        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(id As Guid, ServiceCenterId As Guid, InvoiceNumber As String, UserId As Guid, BatchNumber As String, svcControlAmount As String,
                             InvoiceDate As String, invoiceTypeId As Guid, invoiceStatusId As Guid, invoiceReceivedDate As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_BATCH_INVOICE, GetType(DataSet))}
        Dim inParameters(9) As DBHelper.DBHelperParameter

        Dim ds As New DataSet

        inParameters(0) = New DBHelper.DBHelperParameter(P_USER_ID, UserId.ToByteArray)

        If Not id.Equals(Guid.Empty) Then
            inParameters(1) = New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray)
        Else
            inParameters(1) = New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, DBNull.Value, GetType(System.Guid))
        End If

        If Not ServiceCenterId.Equals(Guid.Empty) Then
            inParameters(2) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, ServiceCenterId.ToByteArray)
        Else
            inParameters(2) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, DBNull.Value, GetType(System.Guid))
        End If

        If Not InvoiceNumber.Trim.Equals("") Then
            inParameters(3) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, InvoiceNumber)
        Else
            inParameters(3) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, DBNull.Value, GetType(System.String))
        End If
        If Not BatchNumber.Trim.Equals("") Then
            inParameters(4) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, BatchNumber)
        Else
            inParameters(4) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, DBNull.Value, GetType(System.String))
        End If
        If Not svcControlAmount.Trim.Equals("") Then
            inParameters(5) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, svcControlAmount)
        Else
            inParameters(5) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, DBNull.Value, GetType(System.Decimal))
        End If

        If Not InvoiceDate.Trim.Equals("") Then
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, InvoiceDate)
        Else
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, DBNull.Value, GetType(System.DateTime))
        End If

        If Not invoiceTypeId.Equals(Guid.Empty) Then
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, invoiceTypeId.ToByteArray)
        Else
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, DBNull.Value, GetType(System.Guid))
        End If

        If Not invoiceStatusId.Equals(Guid.Empty) Then
            inParameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, invoiceStatusId.ToByteArray)
        Else
            inParameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, DBNull.Value, GetType(System.Guid))
        End If

        If Not invoiceReceivedDate.Trim.Equals("") Then
            inParameters(9) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, invoiceReceivedDate)
        Else
            inParameters(9) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, DBNull.Value, GetType(System.DateTime))
        End If

        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadbatchDetail(invoiceBatchid As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_DETAIL")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, invoiceBatchid.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_BATCH_INVOICE, GetType(DataSet))}
        Dim ds As New DataSet
        Dim tbl As String = "ELP_INVOICE_TRANS_DETAIL"
        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, tbl)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region " Create Method "

    'Calls the stored procedure CREATE_BATCH_CLAIMS to create elp_invoice_trans record
    Public Function CreateBatch(ServiceCenterId As Guid, InvoiceNumber As String, InvoiceAmount As Double, BatchNumber As String, UserId As Guid, InvoiceDate As DateType, InvoiceStatusId As Guid, InvoiceReceivedDate As DateType, InvoiceTypeId As Guid) As Guid
        Dim sqlStatement As String = Config("/SQL/INSERT")

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Guid))}
        Dim inParameters(8) As DBHelper.DBHelperParameter
        inParameters(0) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, ServiceCenterId.ToByteArray)
        inParameters(1) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, InvoiceNumber)
        inParameters(2) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, InvoiceAmount)
        If Not BatchNumber.Trim.Equals("") Then
            inParameters(3) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, BatchNumber)
        Else
            inParameters(3) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, DBNull.Value, GetType(System.String))
        End If
        inParameters(4) = New DBHelper.DBHelperParameter(P_USER_ID, UserId.ToByteArray)
        If InvoiceDate IsNot Nothing Then
            inParameters(5) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, InvoiceDate.Value)
        Else
            inParameters(5) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, DBNull.Value, GetType(System.String))
        End If

        If Not InvoiceStatusId.Equals(Guid.Empty) Then
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, InvoiceStatusId.ToByteArray)
        Else
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, DBNull.Value, GetType(System.Guid))
        End If

        If InvoiceDate IsNot Nothing Then
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, InvoiceReceivedDate.Value)
        Else
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, DBNull.Value, GetType(System.String))
        End If

        If Not InvoiceTypeId.Equals(Guid.Empty) Then
            inParameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, InvoiceTypeId.ToByteArray)
        Else
            inParameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, DBNull.Value, GetType(System.Guid))
        End If

        Try
            DBHelper.ExecuteSp(sqlStatement, inParameters, outParameters)
            Return outParameters(0).Value
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region " Update Batch Method "

    'Calls the stored procedure CREATE_BATCH_CLAIMS to create elp_invoice_trans record
    Public Function UpdateBatch(Id As Guid, ServiceCenterId As Guid, InvoiceNumber As String, InvoiceAmount As Double, BatchNumber As String, UserId As Guid, InvoiceDate As DateType, InvoiceStatusId As Guid, InvoiceReceivedDate As DateType, InvoiceTypeId As Guid) As Boolean
        Dim sqlStatement As String = Config("/SQL/UPDATE")

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Integer))}
        Dim inParameters(8) As DBHelper.DBHelperParameter
        inParameters(0) = New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, ServiceCenterId.ToByteArray)
        inParameters(1) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, InvoiceNumber)
        inParameters(2) = New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, InvoiceAmount)
        If Not BatchNumber.Trim.Equals("") Then
            inParameters(3) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, BatchNumber)
        Else
            inParameters(3) = New DBHelper.DBHelperParameter(P_BATCH_NUMBER, DBNull.Value, GetType(System.String))
        End If
        inParameters(4) = New DBHelper.DBHelperParameter(P_USER_ID, UserId.ToByteArray)
        inParameters(5) = New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, Id.ToByteArray)
        If InvoiceDate IsNot Nothing Then
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, InvoiceDate.Value)
        Else
            inParameters(6) = New DBHelper.DBHelperParameter(P_INVOICE_DATE, DBNull.Value, GetType(System.String))
        End If
        'inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_STATUS_ID, InvoiceStatusId.ToByteArray)
        If InvoiceReceivedDate IsNot Nothing Then
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, InvoiceReceivedDate.Value)
        Else
            inParameters(7) = New DBHelper.DBHelperParameter(P_INVOICE_RECEIVED_DATE, DBNull.Value, GetType(System.String))
        End If
        inParameters(8) = New DBHelper.DBHelperParameter(P_INVOICE_TYPE_ID, InvoiceTypeId.ToByteArray)
        'Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        '                    New DBHelper.DBHelperParameter(P_SERVICE_CENTER_ID, ServiceCenterId), _
        '                    New DBHelper.DBHelperParameter(P_SVC_CONTROL_NUMBER, InvoiceNumber), _
        '                    New DBHelper.DBHelperParameter(P_SVC_CONTROL_AMOUNT, InvoiceAmount), _
        '                    New DBHelper.DBHelperParameter(P_BATCH_NUMBER, BatchNumber), _
        '                    New DBHelper.DBHelperParameter(P_USER_ID, UserId), _
        '                    New DBHelper.DBHelperParameter(Me.P_INVOICE_TRANS_ID, Id), _
        '                    New DBHelper.DBHelperParameter(Me.P_INVOICE_DATE, InvoiceDate.Value)}

        Try
            DBHelper.ExecuteSp(sqlStatement, inParameters, outParameters)
            If outParameters(0).Value = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region " Delete Methods "

    'Calls the stored procedure CREATE_BATCH_CLAIMS to create elp_invoice_trans record
    Public Function DeleteBatch(id As Guid) As Boolean

        Dim selectStmt As String = Config("/SQL/DELETE_BATCH")
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Integer))}

        Try
            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
            Return IIf(outParameters(0).Value = 1, True, False)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region " Process Method "

    'Calls the stored procedure CREATE_BATCH_CLAIMS to create elp_invoice_trans record
    Public Function ProcessBatch(id As Guid, InvoiceTaxTypeId As Guid) As Boolean

        Dim selectStmt As String = Config("/SQL/PROCESS_BATCH")
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray), _
                                                            New DBHelper.DBHelperParameter(COL_NAME_INVOICE_TAX_TYPE_ID, InvoiceTaxTypeId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Integer))}

        Try
            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
            Select Case outParameters(0).Value
                Case 0
                    Return False
                Case 1
                    Return True
                Case Else
                    Throw New DatabaseException(Common.ErrorCodes.DB_ERROR)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region " Save Claims "

    'Calls the stored procedure CREATE_BATCH_CLAIMS to create elp_invoice_trans record
    Public Sub SaveClaims(ClaimSet As BatchClaimInvoice, id As Guid)

        Dim selectStmt As String = Config("/SQL/SAVE_BATCH_CLAIMS")
        Dim outParameters() As DBHelper.DBHelperParameter ' = New DBHelper.DBHelperParameter   {New DBHelper.DBHelperParameter(P_BATCH_INVOICE, GetType(DataSet))}
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id), _
                 New DBHelper.DBHelperParameter(P_CLAIM_XMLDOC, ClaimSet.GetXml, GetType(Xml.XmlDocument))}
        Dim ds As New DataSet
        Dim tbl As String = "Claims"
        Try
            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function SaveBatchTax(id As Guid, region_id As Guid, batch_number As String, tax1_amt As Decimal, tax2_amt As Decimal, _
        user_id As Guid) As Boolean

        Dim selectStmt As String = Config("/SQL/SAVE_TAX_BATCH")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(P_INVOICE_TRANS_ID, id.ToByteArray), _
                 New DBHelper.DBHelperParameter(COL_NAME_BATCH_NUMBER, batch_number), _
                 New DBHelper.DBHelperParameter(COL_NAME_REGION_ID, region_id.ToByteArray), _
                 New DBHelper.DBHelperParameter(COL_NAME_TAX1_AMOUNT, tax1_amt), _
                 New DBHelper.DBHelperParameter(COL_NAME_TAX2_AMOUNT, tax2_amt), _
                  New DBHelper.DBHelperParameter(P_USER_ID, user_id.ToByteArray)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Integer))}

        Dim ds As New DataSet
        Dim tbl As String = "Claims"
        Try
            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
            If outParameters(0).Value = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region " SHADOW METHODS"

    'Created these methods to override the default methods as we are using stored procedures for the functionality in this 
    '  business object.  

    Shadows Sub Update(row As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        'DBHelper.Execute(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
    End Sub

    Shadows Sub UpdateWithParam(row As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        'DBHelper.ExecuteWithParam(row, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
    End Sub

    'This Method assumes you have the nodes  "/SQL/INSERT", "/SQL/UPDATE", "/SQL/DELETE" in your xml file
    Shadows Sub Update(table As DataTable, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        'DBHelper.Execute(table, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
    End Sub

    Shadows Sub UpdateWithParam(table As DataTable, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        'DBHelper.ExecuteWithParam(table, Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, Transaction, changesFilter)
    End Sub

#End Region

    Public Function GetInvoiceTaxTypeDetails(taxtypeId As Guid, countryId As Guid, dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/INVOICE_TAX_TYPE_DETAILS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        New DBHelper.DBHelperParameter(P_COUNTRY_ID, countryId.ToByteArray), _
        New DBHelper.DBHelperParameter(P_TAXTYPE_ID, taxtypeId.ToByteArray), _
            New DBHelper.DBHelperParameter(P_DEALER_ID, dealerId.ToByteArray)}
        Dim ds As New DataSet
        Dim tbl As String = "ELP_INVOICE_TRANS_DETAIL"
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function UpdateExcludeDeductible(strExcludeDeductible As String, invoiceTransDetailId As Guid) As Boolean
        Dim sqlStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        sqlStmt = Config("/SQL/UPDATE_EXCLUDE_DEDUCTIBLE")
        parameters = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter("EXCLUDE_DEDUCTIBLE", strExcludeDeductible), _
                        New DBHelper.DBHelperParameter("INVOICE_TRANS_DETAIL_ID", invoiceTransDetailId.ToByteArray)}

        Try

            DBHelper.ExecuteWithParam(sqlStmt, parameters)
            Return True

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function UpdatePaymentAmount(invoiceTransDetailId As Guid) As Boolean
        Dim sqlStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        sqlStmt = Config("/SQL/UPDATE_PAYMENT_AMOUNT")
        parameters = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter("INVOICE_TRANS_DETAIL_ID", invoiceTransDetailId.ToByteArray)}

        Try

            DBHelper.ExecuteWithParam(sqlStmt, parameters)
            Return True

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckforPreInvoice(batchNumber As String) As DataSet
        Dim selectStmt As String = Config("/SQL/CHECK_PRE_INVOICE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
        New DBHelper.DBHelperParameter(P_BATCHNUMBER, batchNumber)}
        Dim ds As New DataSet
        Dim tbl As String = "ELP_PRE_INVOICE"
        Try
            DBHelper.Fetch(ds, selectStmt, tbl, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetInvoiceComments(invoice_trans As String) As DataSet

        Dim selectStmt As String = Config("/SQL/INVOICE_COMMENTS")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim invoice_trans_id As Guid = New Guid(invoice_trans)

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_INVOICE_TRANS_ID, invoice_trans_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

    End Function

    Public Function UpdateRejectReason(invoiceTransId As Guid, InvoiceComments As String) As Boolean
        
        Dim selectStmt As String = Config("/SQL/UPDATE_REJECT_REASON")
        Dim inParameters(1) As DBHelper.DBHelperParameter
        Dim intErrCode As Integer

        inParameters(0) = New DBHelper.DBHelperParameter("pi_invoice_comments", InvoiceComments)
        inParameters(1) = New DBHelper.DBHelperParameter("pi_invoice_trans_id", invoiceTransId.ToByteArray)
        
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                New DBHelper.DBHelperParameter(PO_RETURN_CODE, GetType(String))}

        Dim ds As New DataSet
        Dim tbl As String = "UPDATE_REJECT_REASON"

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)

        If outParameters(0).Value IsNot Nothing Then
            Try
                intErrCode = CType(outParameters(0).Value, Integer)
            Catch ex As Exception
                intErrCode = 0
            End Try
        End If

        If intErrCode = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetBatchClosedClaims(serviceCenterId As Guid, batchNumber As String, InvoiceTransId As Guid, userId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_BATCH_CLOSED_CLAIMS")
        Dim inParameters(4) As DBHelper.DBHelperParameter

        inParameters(0) = New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, serviceCenterId.ToByteArray)
        inParameters(1) = New DBHelper.DBHelperParameter(COL_NAME_INVOICE_TRANS_ID, InvoiceTransId.ToByteArray)
        inParameters(2) = New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId.ToByteArray)
        inParameters(3) = New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId)
        inParameters(4) = New DBHelper.DBHelperParameter(COL_NAME_BATCH_NUMBER, batchNumber)

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                New DBHelper.DBHelperParameter(P_CLAIMS, GetType(DataSet))}

        Dim ds As New DataSet
        Dim tbl As String = "BATCH_CLOSED_CLAIMS"

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
        Return ds

    End Function
End Class
