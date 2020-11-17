'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/16/2004)********************


Public Class CountryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COUNTRY_V"
    Public Const TABLE_KEY_NAME As String = "country_id"

    Public Const COL_NAME_COUNTRY_ID = "country_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_CODE = "code"
    Public Const COL_NAME_LANGUAGE_ID = "language_id"
    Public Const COL_NAME_PRIMARY_CURRENCY_ID = "primary_currency_id"
    Public Const COL_NAME_SECONDARY_CURRENCY_ID = "secondary_currency_id"
    Public Const COL_NAME_MAIL_ADDR_FORMAT = "mail_addr_format"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_BANK_ID_LENGTH As String = "bank_id_length"
    Public Const COL_NAME_BANK_ACCT_LENGTH As String = "bank_acct_length"
    Public Const COL_NAME_EUROPEAN_COUNTRY_ID As String = "european_country_id"
    Public Const COL_NAME_VALIDATE_BANK_INFO_ID As String = "validate_bank_info_id"
    Public Const COL_NAME_TAX_BY_PRODUCT_TYPE_ID As String = "tax_by_product_type_id"
    Public Const COL_NAME_REQUIRE_BYTE_CONV_ID As String = "require_byte_conversion_id"
    Public Const COL_NAME_DEFAULT_SC_FOR_DENIED_CLAIMS As String = "default_sc_for_denied_claims"
    Public Const COL_NAME_DEFAULT_SC_ID As String = "default_sc_id"
    Public Const COL_NAME_CONTACT_INFO_REQ_FIELDS As String = "contact_info_req_fields"
    Public Const COL_NAME_ADDRESS_INFO_REQ_FIELDS As String = "address_req_fields"
    Public Const COL_NAME_USE_BANK_LIST_ID As String = "use_bank_list_id"
    Public Const COL_NAME_LAST_REGULATORY_EXTRACT_DATE As String = "last_regulatory_extract_date"
    Public Const COL_NAME_REGULATORY_REPORTING_ID As String = "regulatory_reporting_id"
    Public Const COL_NAME_NOTIFY_GROUP_EMAIL As String = "notify_group_email"
    Public Const COL_NAME_CREDIT_SCORING_PCT As String = "credit_scoring_pct"
    Public Const COL_NAME_ABNORMAL_CLM_FRQ_NO As String = "abnormal_clm_frq_no"
    Public Const COL_NAME_CERT_COUNT_SUSP_OP As String = "cert_count_susp_op"
    Public Const COL_NAME_ISO_CODE = "iso_code"
    Public Const COL_NAME_ALLOW_FORGOTTEN = "allow_forgotten"
    Public Const COL_NAME_PRICE_LIST_APPROVAL_NEEDED = "price_list_approval_needed"
    Public Const COL_NAME_PRICE_LIST_APPROVAL_EMAIL = "price_list_approval_email"
    Public Const COL_NAME_FULL_NAME_FORMAT = "full_name_format_xcd"
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

    'REQ
    Public Const COL_NAME_USE_ADDRESS_VALIDATION_XCD As String = "use_address_validation_xcd"
    Public Const COL_NAME_ALLOW_FORCE_ADDRESS_XCD As String = "allow_force_address_xcd"
    Public Const COL_NAME_ADDRESS_CONFIDENCE_THRESHOLD As String = "address_confidence_threshold"

    'PBI 604501 - Add a new field to manage the BIC value in SEPA FILE
    Public Const COL_NAME_USE_SEPA_BIC_CUSTOMER As String = "use_sepa_bic_customer"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("country_id", id.ToByteArray)}
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


    Public Function LoadCountries(ByVal compIds As ArrayList) As DataSet
        If compIds.Count = 0 Then Return Nothing
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COUNTRY_LIST")

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet

            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadListForWS(ByVal ds As DataSet, ByVal oCountriesIds As ArrayList) As DataSet
        If oCountriesIds.Count = 0 Then Return Nothing
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COUNTRY_LIST_FOR_WS")

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql(Me.TABLE_KEY_NAME, oCountriesIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    'Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
    '    DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    'End Sub

    Public Function GetCountryPostalFormat(ByVal oCountryId As Guid) As DataView
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_COUNTRY_COMUNAS")
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter
            If oCountryId.Equals(Guid.Empty) Then
                parameter = New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID, GenericConstants.WILDCARD)
            Else
                parameter = New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID, oCountryId.ToByteArray)
            End If
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {parameter})
            Return ds.Tables(0).DefaultView

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCountryByteFlag(ByVal oCountryId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/GET_BYTE_FLAG")
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter
            parameter = New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID, oCountryId.ToByteArray)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
                            New DBHelper.DBHelperParameter() {parameter})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Dim cntryPostalFormatDAL As New CountryPostalCodeFormatDAL
            'First Pass updates Deletions
            cntryPostalFormatDAL.Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)
            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            cntryPostalFormatDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
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
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region


End Class


