'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/20/2008)********************


Public Class BillingHeaderDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BILLING_HEADER"
    Public Const TABLE_KEY_NAME As String = "billing_header_id"

    Public Const COL_NAME_BILLING_HEADER_ID As String = "billing_header_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DATE_FILE_SENT As String = "date_file_sent"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_TOTAL_BILLED_AMT As String = "total_billed_amt"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_REFERENCE_NUMBER As String = "reference_number"
    Public Const COL_NAME_DISPLAY_TO_USER As String = "display_to_user" 'DEF2262

    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER_A As String = "--dynamic_A_where_clause"

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
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("billing_header_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(companyIds As ArrayList, DealerId As Guid, BeginDate As Date, EndDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        whereClauseConditions = " AND " & MiscUtil.BuildListForSql("d." & COL_NAME_COMPANY_ID, companyIds, False)

        If DealerId <> Guid.Empty Then
            whereClauseConditions &= " AND bh." & COL_NAME_DEALER_ID & " = '" & GuidToSQLString(DealerId) & "'"
        End If

        If BeginDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(bh." & COL_NAME_DATE_FILE_SENT & ") >= TO_DATE('" & BeginDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        If EndDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(bh." & COL_NAME_DATE_FILE_SENT & ") <= TO_DATE('" & EndDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadListByCompany(companyIds As ArrayList, BeginDate As Date, EndDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_COMPANY")
        Dim whereClauseConditions As String = ""
        Dim whereClauseConditionsA As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        whereClauseConditions = " AND " & MiscUtil.BuildListForSql("d." & COL_NAME_COMPANY_ID, companyIds, False) & " AND bh." & COL_NAME_DISPLAY_TO_USER & " is null" 'DEF2262
        whereClauseConditionsA = " AND " & MiscUtil.BuildListForSql("c." & COL_NAME_COMPANY_ID, companyIds, False)

        If BeginDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(bh." & COL_NAME_DATE_FILE_SENT & ") >= TO_DATE('" & BeginDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        If EndDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND TRUNC(bh." & COL_NAME_DATE_FILE_SENT & ") <= TO_DATE('" & EndDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER_A, whereClauseConditionsA)

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class





