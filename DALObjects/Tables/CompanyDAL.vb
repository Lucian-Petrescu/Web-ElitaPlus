'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/23/2004)********************


Public Class CompanyDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMPANY"
    Public Const TABLE_NAME_COMPANY_COUNTRY As String = "Countries"
    Public Const TABLE_KEY_NAME As String = "company_id"
    Public Const TABLE_NAME_GROUP_COMPANIES As String = "group_companies"
    Public Const TABLE_NAME_DEALER_NOAGENT As String = "DealerWoAgents" ' REQ-1295
    Public Const TABLE_NAME_FLAG_REQAGENT As String = "FlagReqAgents" ' REQ-1295
    
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_CODE = "code"
    Public Const COL_NAME_TAX_ID_NUMBER = "tax_id_number"
    Public Const COL_NAME_BUSINESS_COUNTRY_ID = "business_country_id"
    'Public Const COL_NAME_ADDRESS_ID = "address_id"
    Public Const COL_NAME_PHONE = "phone"
    Public Const COL_NAME_FAX = "fax"
    Public Const COL_NAME_EMAIL = "email"
    Public Const COL_NAME_REFUND_TOLERANCE_AMT = "refund_tolerance_amt"
    Public Const COL_NAME_CLAIM_NUMBER_FORMAT_ID = "claim_number_format_id"
    Public Const COL_NAME_CERT_NUMBER_FORMAT_ID = "cert_number_format_id"
    Public Const COL_NAME_INVOICE_METHOD_ID = "invoice_method_id"
    Public Const COL_NAME_LANGUAGE_ID = "language_id"
    Public Const COL_NAME_DEFAULT_FOLLOWUP_DAYS = "default_followup_days"
    Public Const COL_NAME_MAX_FOLLOWUP_DAYS = "max_followup_days"
    Public Const COL_NAME_LEGAL_DISCLAIMER As String = "legal_disclaimer"
    Public Const COL_NAME_SALUTATION_ID As String = "salutation_id"

    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_POST_CODE As String = "post_code"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "Company_group_id"
    Public Const COL_NAME_COMPANY_TYPE_ID As String = "company_type_id"
    Public Const COL_NAME_UPR_USES_WP_ID As String = "upr_uses_wp_id"
    Public Const COL_NAME_MASTER_CLAIM_PROCESSING_ID As String = "master_claim_processing_id"
    Public Const COL_NAME_USE_ZIP_DISTRICTS_ID As String = "usezipdistrict_id"
    Public Const COL_NAME_AUTH_DETAIL_RQRD_ID As String = "auth_detail_rqrd_id"
    Public Const COL_NAME_ADDL_DAC_ID As String = "addl_dac_id"
    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_AUTO_PROCESS_FILE_ID As String = "auto_process_file_id"
    Public Const COL_NAME_USE_RECOVERIES_ID As String = "use_recoveries_id"
    Public Const COL_NAME_SERVICE_ORDERS_BY_DEALER_ID As String = "service_orders_by_dealer_id"
    Public Const COL_NAME_REQUIRE_ITEM_DESCRIPTION_ID As String = "require_item_description_id"
    Public Const COL_NAME_REQUIRE_AGENT_CODE_ID As String = "require_agent_code_id"
    Public Const COL_NAME_CLIP_METHOD_ID As String = "clip_method_id"
    Public Const COL_NAME_REPORT_COMMISSION_TAX_ID As String = "report_commission_tax_id"
    Public Const COL_NAME_TIME_ZONE_NAME_ID = "time_zone_name_id"
    Public Const COL_NAME_COMPUTE_TAX_BASED_ID = "compute_tax_based_id"
    Public Const COL_NAME_BILLING_BY_DEALER_ID As String = "billing_by_dealer_id"
    Public Const COL_NAME_POLICE_RPT_FOR_LOSS_COV_ID As String = "police_rpt_for_loss_cov_id"
    Public Const COL_NAME_REQ_CUSTOMER_LEGAL_INFO_ID As String = "req_customer_legal_info_id"
    Public Const COL_NAME_USE_TRANSFER_OF_OWNERSHIP As String = "use_transfer_of_ownership"
    Public Const COL_NAME_UNIQUE_CERTIFICATE_NUMBERS_ID As String = "unique_certificate_numbers_id"
    Public Const COL_NAME_OVERRIDE_WARRANTYPRICE_CHECK As String = "override_warrantyprice_check"
    Public Const COL_NAME_UNIQUE_CERT_EFFECTIVE_DATE As String = "unique_cert_effective_date"

    '09/11/2006 - ALR - Added for auto closing claims
    Public Const COL_NAME_DAYS_TO_CLOSE_CLAIM = "days_to_close_claim"
    Public Const COL_NAME_CERTNUMLOOKUPBY_ID As String = "certnumlookupby_id"
    Public Const COL_USE_PRE_INVOICE_PROCESS_ID As String = "use_pre_invoice_process_id"
    Public Const COL_SC_PRE_INV_WAITING_PERIOD As String = "sc_pre_inv_waiting_period"
    Public Const COL_NAME_CLAIM_NUMBER_OFFSET = "claim_number_offset"
    Public Const COL_NAME_EU_MEMBER_ID As String = "eumember_id"
    Public Const COL_NAME_FTP_SITE_ID As String = "ftp_site_id"
   
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"
    'Public Const COL_NAME_ENABLE_PERIOD_MILEAGE_VAL = "enable_period_mileage_val"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub
    Public Sub LoadAccountClosingInfoByCompanyID(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ACCOUNTING_CLOSE_INFO_BY_COMPANY_ID")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("company_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, "ELP_ACCOUNTING_CLOSE_INFO", parameters) 'Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("company_id", id.ToByteArray)}
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
    Public Function LoadList(ByVal description As String, ByVal code As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CODE, code), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, description)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCompanies(ByVal companyGroupId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_GROUP_COMPANIES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_GROUP_COMPANIES, parameters)

    End Function
#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        'Dim addressDAL As New addressDAL
        Dim cmpCountryDAL As New CompanyCountryDAL
        Dim cpmAccCloseInfDal As New AccountingCloseInfoDAL
        Dim oAttributeValueDAL As New AttributeValueDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            cmpCountryDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            cpmAccCloseInfDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            oAttributeValueDAL.Update(familyDataset.GetChanges(), tr)
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            cmpCountryDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            cpmAccCloseInfDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "Public Methods"

    Public Sub LoadSelectedCountries(ByVal ds As DataSet, ByVal companyID As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_SELECTED_COUNTRY_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_COMPANY_COUNTRY, parameters)

    End Sub

    Public Sub LoadAvailableCountries(ByVal ds As DataSet, ByVal companyID As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_COUNTRY_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_COMPANY_COUNTRY, parameters)
    End Sub

    Public Sub GetCompanyDealerWithoutAgent(ByVal ds As DataSet, ByVal companyID As Guid)
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_WO_AGENTCODES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_DEALER_NOAGENT, parameters)
    End Sub

    Public Sub GetCompanyAgentFlagForDealer(ByVal ds As DataSet, ByVal dealerID As Guid)
        Dim selectStmt As String = Me.Config("/SQL/CHECK_DEALER_WO_AGENTCODES")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_FLAG_REQAGENT, parameters)
    End Sub

    Public Function GetDealerFromCompany(ByVal CompanyId As Guid, ByVal DealerCode As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_FROM_COMPANY_DEALER")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                   New DBHelper.DBHelperParameter("CompanyId", CompanyId.ToByteArray()),
                   New DBHelper.DBHelperParameter("DealerCode", DealerCode)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "Dealer_Company", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckIfCompanyCodeAlreadyExists(ByVal Code As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/CHECK_IF_COMPANY_CODE_ALREADY_EXISTS")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                   New DBHelper.DBHelperParameter("Code", Code)}
        Try
            Dim ds As New DataSet
            Dim count As Integer
            count = Convert.ToInt16(DBHelper.ExecuteScalar(selectStmt, parameters))
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


