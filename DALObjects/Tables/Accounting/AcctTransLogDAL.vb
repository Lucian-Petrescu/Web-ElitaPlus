'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/16/2007)********************

Public Class AcctTransLogDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_TRANS_LOG"
    Public Const TABLE_KEY_NAME As String = "acct_trans_log_id"

    Public Const COL_NAME_ACCT_TRANS_LOG_ID As String = "acct_trans_log_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_ACCT_EVENT_TYPE_ID As String = "acct_event_type_id"
    Public Const COL_NAME_ACCT_EVENT_FIELD_ID As String = "acct_event_field_id"
    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_COUNTRY As String = "country"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_REGION_DESCRIPTION As String = "region_description"
    Public Const COL_NAME_TAX_ID_CODE As String = "tax_id_code"
    Public Const COL_NAME_CURRENCY As String = "currency"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_BANK_ACCOUNT_NUMBER As String = "bank_account_number"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_PAYEE As String = "payee"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_ZIP As String = "zip"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_PAYMENT_AMOUNT_REV As String = "payment_amount_rev"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_ACCT_PERIOD As String = "acct_period"
    Public Const COL_NAME_COVERAGE_TYPE As String = "coverage_type"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_NETWORK_ID As String = "network_id"
    Public Const COL_NAME_PAYMENT_NUMBER As String = "payment_number"
    Public Const COL_NAME_TRANSACTION_ID_NUMBER As String = "transaction_id_number"
    Public Const COL_NAME_PROCESS_DATE As String = "process_date"
    Public Const COL_NAME_ACCT_TRANSMISSION_ID As String = "acct_transmission_id"

    Public Const COL_NAME_VENDOR_UPDATE As String = "vendor_update"
    Public Const COL_NAME_PAYMENT_TO_CUSTOMER As String = "payment_to_customer"
    Public Const COL_NAME_BANK_SORTCODE As String = "bank_sortcode"
    Public Const COL_NAME_BANK_ADDRESS_1 As String = "bank_address_1"
    Public Const COL_NAME_BANK_ADDRESS_2 As String = "bank_address_2"
    Public Const COL_NAME_BANK_ADDRESS_3 As String = "bank_address_3"
    Public Const COL_NAME_BANK_ADDRESS_4 As String = "bank_address_4"
    Public Const COL_NAME_BANK_NAME_1 As String = "bank_name_1"
    Public Const COL_NAME_BANK_NAME_2 As String = "bank_name_2"
    Public Const COL_NAME_BANK_IBAN As String = "bank_iban"
    Public Const COL_NAME_BANK_BRANCH As String = "bank_branch"
    Public Const COL_NAME_COMMISSION_ENTITY_ID As String = "commission_entity_id"
    Public Const COL_NAME_WARR_SALES_DATE As String = "warr_sales_date"
    Public Const COL_NAME_CONTRACT_INCEPTION_DATE As String = "contract_inception_date"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_POLICY_NUMBER As String = "policy_number"

    'TableNames for the queries
    Public Const DatasetName As String = "SSC"
    Public Const Table_LINEITEM As String = "Line"
    Public Const Table_HEADER As String = "SunSystemsContext"
    Public Const TABLE_POSTINGPARAMETERS As String = "LedgerPostingParameters"
    Public Const Table_VENDOR As String = "VENDOR"
    Public Const Table_LINEITEM_DETAIL = "DetailLad"
    Public Const Table_CONTROL_GROUP = "ControlGroup"

    Public Const Table_AP_LINEITEM As String = "AP_Item"
    'Public Const Table_AP_HEADER As String = "PRCG"
    'Public Const Table_AP_INVOICESUMMARY As String = "PRQT"
    'Public Const Table_AP_LINENAME As String = "PRLN"
    Public Const Table_AP_VENDORS As String = "VMST"
    Public Const Table_AP_PURGE As String = "PURGE_TABLE"
    Public Const Table_AP_INVOICE As String = "Invoice"

    Public Const REL_JOURNAL_TYPE As String = "REL_JournalType"
    Public Const JOURNAL_COL_NAME_JOURNAL_TYPE = "JOURNALTYPE"
    Public Const JOURNAL_COL_NAME_JOURNAL_ID_SUFFIX = "JOURNAL_ID_SUFFIX"

    'Inner Table columns
    Public Const COL_TABLE_HEADER_BUSINESS_UNIT As String = "BusinessUnit"

    'Parameters
    Private Const PARAM_ACCOUNTING_COMPANY_ID As String = "ACCT_COMPANY_ID"
    Private Const PARAM_ACCOUNTING_EVENT_ID As String = "ACCT_EVENT_TYPE_ID"
    Private Const PARAM_ACCOUNTING_PERIOD As String = "ACCT_PERIOD"
    Private Const PARAM_BUSINESS_UNIT As String = "BUSINESS_UNIT"
    Private Const PARAM_PROCESS_DATE As String = "PROCESS_DATE"
    Private Const PARAM_COMPANY_ID As String = "COMPANY_ID"
    Private Const PARAM_COMPANY_KEY As String = "P_COMPANY_KEY"
    Private Const PARAM_INPUT_DATE As String = "VTO_DAY"
    Private Const PARAM_ACCOUNTING_BUSINESS_UNIT_ID As String = "ACCT_BUSINESS_UNIT_ID"
    Private Const PARAM_NETWORK_ID As String = "NETWORK_ID"

    Public Enum VendorFile
        ACCOUNT
        ADDRESS
        SUPPLIER
        BANK_INFO
    End Enum

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_trans_log_id", id.ToByteArray)}
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

    Public Function GetJournalEntries(ByVal CompanyId As Guid, _
                                        ByVal AccountingEventId As Guid, _
                                        ByVal BusinessUnitId As Guid, _
                                        ByVal EventName As String, _
                                        ByVal NetworkId As String, _
                                        Optional ByVal IncludeProcessed As Boolean = False) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/JOURNAL_" + EventName)
        Dim parameters(Microsoft.VisualBasic.Split(selectStmt, ":").Length - 2) As DBHelper.DBHelperParameter


        For i As Integer = 0 To (parameters.Length / 4) - 1
            parameters(i + (3 * i)) = New DBHelper.DBHelperParameter(PARAM_NETWORK_ID, NetworkId)
            parameters(i + 1 + (3 * i)) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_EVENT_ID, AccountingEventId.ToByteArray)
            parameters(i + 2 + (3 * i)) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_BUSINESS_UNIT_ID, BusinessUnitId.ToByteArray)
            parameters(i + 3 + (3 * i)) = New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray)
        Next

        Dim ds As New DataSet(DatasetName)

        'Add Dynamic Query parameter
        If IncludeProcessed Or EventName = "INV" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND ELP_ACCT_TRANS_LOG.PROCESS_DATE Is  null")
        End If

        Try
            ds = (DBHelper.Fetch(ds, selectStmt, Table_LINEITEM, parameters))
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetJournalEntriesAP(ByVal CompanyId As Guid, _
                                            ByVal AccountingEventId As Guid, _
                                            ByVal BusinessUnitId As Guid, _
                                            ByVal EventName As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/JOURNAL_" + EventName + "_AP")
        Dim parameters(2) As DBHelper.DBHelperParameter

        parameters(0) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_EVENT_ID, AccountingEventId.ToByteArray)
        parameters(1) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_BUSINESS_UNIT_ID, BusinessUnitId.ToByteArray)
        parameters(2) = New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray)

        Dim ds As New DataSet(DatasetName)

        'Add Dynamic Query parameter
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND ELP_ACCT_TRANS_LOG.PROCESS_DATE Is  null")

        Try
            ds = (DBHelper.Fetch(ds, selectStmt, Table_AP_LINEITEM, parameters))
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetPurgeTable(ByVal CompanyId As Guid, _
                                            ByVal AccountingEventId As Guid, _
                                            ByVal BusinessUnitId As Guid, _
                                            ByVal EventName As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/JOURNAL_AP_PURGE")
        Dim parameters(2) As DBHelper.DBHelperParameter

        parameters(0) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_EVENT_ID, AccountingEventId.ToByteArray)
        parameters(1) = New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_BUSINESS_UNIT_ID, BusinessUnitId.ToByteArray)
        parameters(2) = New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray)

        Dim ds As New DataSet(DatasetName)

        Try
            ds = (DBHelper.Fetch(ds, selectStmt, Table_AP_PURGE, parameters))
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetVendorFiles(ByVal CompanyId As Guid, ByVal CurrentDataSet As DataSet) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/VENDOR")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PARAM_COMPANY_ID, CompanyId.ToByteArray)}


        If CurrentDataSet Is Nothing Then
            CurrentDataSet = New DataSet(DatasetName)
        End If

        Try
            Return (DBHelper.Fetch(CurrentDataSet, selectStmt, Table_VENDOR, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetJournalPostingParams(ByVal AccountingCompanyId As Guid, ByVal AccountingEventId As Guid, ByVal AccountingBusinessUnitId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/JOURNAL_POSTING_PARAMETERS")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                             {New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_COMPANY_ID, AccountingCompanyId.ToByteArray), _
                              New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_EVENT_ID, AccountingEventId.ToByteArray), _
                              New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_BUSINESS_UNIT_ID, AccountingBusinessUnitId.ToByteArray)}


        Dim ds As New DataSet

        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_POSTINGPARAMETERS, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetJournalHeader(ByVal AccountingCompanyId As Guid, ByVal BusinessUnit As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/HEADER")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(PARAM_ACCOUNTING_COMPANY_ID, AccountingCompanyId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PARAM_BUSINESS_UNIT, BusinessUnit)}

        Dim ds As New DataSet

        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.Table_HEADER, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub PurgeTransLog(ByVal AcctTransLogIds As ArrayList)

        Dim selectStmt As String = Me.Config("/SQL/DELETE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(COL_NAME_ACCT_TRANS_LOG_ID, AcctTransLogIds.ToArray(GetType(Byte())), GetType(Byte()))}

        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters, , , AcctTransLogIds.Count)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Sub PopulateAccountingEvents(ByVal AcctEvent As String, ByVal CompanyCode As String)

        Dim selectStmt As String = Me.Config("/SQL/EVENT_" & AcctEvent)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(Me.PARAM_COMPANY_KEY, CompanyCode), _
                            New DBHelper.DBHelperParameter(Me.PARAM_INPUT_DATE, DBNull.Value)}
        Try
            If Not selectStmt.Trim.Length = 0 Then
                DBHelper.ExecuteSpParamBindByName(selectStmt, parameters, Nothing)
            End If
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


