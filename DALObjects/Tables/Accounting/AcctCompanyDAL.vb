'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************


Public Class AcctCompanyDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_COMPANY"
    Public Const TABLE_KEY_NAME As String = "acct_company_id"

    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_FTP_DIRECTORY As String = "ftp_directory"
    Public Const COL_NAME_USE_ACCOUNTING As String = "use_accounting"
    Public Const COL_NAME_USE_ELITA_BANK_INFO_ID As String = "use_elita_bank_info_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_RPT_COMMISSION_BREAKDOWN As String = "rpt_commission_breakdown"
    Public Const COL_NAME_ACCT_SYSTEM_ID As String = "acct_system_id"
    Public Const COL_NAME_PROCESS_METHOD_ID As String = "process_method_id"
    Public Const COL_NAME_COV_ENTITY_BY_REGION As String = "cov_entity_by_region"
    Public Const COL_NAME_BALANCE_DIRECTORY As String = "balance_directory"
    Public Const COL_NAME_NOTIFY_EMAIL As String = "notify_email"
    Public Const COL_NAME_USE_COVERAGE_ENTITY As String = "use_coverage_entity"

    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_company_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal description As String, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DESCRIPTION, description)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Get Accounting Companies by User Companies
    Public Function GetByCompanies(ByVal Companies As ArrayList) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_COMPANIES")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(CompanyDAL.COL_NAME_COMPANY_ID, Companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        parameters = New OracleParameter() {New OracleParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

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


End Class


