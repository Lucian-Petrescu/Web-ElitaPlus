'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/17/2015)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper
Imports Assurant.ElitaPlus.Common.LookupListCache
Public Class PreInvoiceDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_PRE_INVOICE"
	Public Const TABLE_KEY_NAME As String = "pre_invoice_id"
	
	Public Const COL_NAME_PRE_INVOICE_ID As String = "pre_invoice_id"
	Public Const COL_NAME_PRE_INVOICE_CREATION_DATE As String = "pre_invoice_creation_date"
	Public Const COL_NAME_SC_DISPLAY_DATE As String = "sc_display_date"
	Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
	Public Const COL_NAME_PRE_INVOICE_STATUS_ID As String = "pre_invoice_status_id"
	Public Const COL_NAME_TOTAL_AMOUNT As String = "total_amount"
	Public Const COL_NAME_TOTAL_CLAIMS As String = "total_claims"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const TABLE_NAME_PRE_INVOICE_WS As String = "PREINVOICE_WS"

    Public Const PAR_COMPANY_CODE As String = "pi_company_code"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "po_exception_msg"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pre_invoice_id", id.ToByteArray)}
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

#Region "Public Methods"

    Public Function GeneratePreInvoice(ByVal companyCode As String) As String
        Dim selectStmt As String = Me.Config("/SQL/GENERATE_PRE_INVOICE")
        Dim inParameters(0) As DBHelper.DBHelperParameter

        inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_COMPANY_CODE, companyCode.Trim)

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                               New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 32000)}

        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_NAME

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)

        Return outParameters(0).Value

    End Function

    Public Function LoadPreInvoiceProcess(ByVal companyId As Guid, ByVal statusId As Guid, ByVal batchNumber As String, ByVal createdDateFrom As String, ByVal createdDateTo As String) As DataSet

        Try
            Dim selectStmt As String = Me.Config("/SQL/PRE_INVOICE")
            Dim whereClauseConditions As String = ""

            If Not (companyId = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "pi." & "company_id" & "= " & MiscUtil.GetDbStringFromGuid(companyId)

            End If
            If Not (statusId = Guid.Empty) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "pi." & "pre_invoice_status_id" & "= " & MiscUtil.GetDbStringFromGuid(statusId)
            End If

            If Me.FormatSearchMask(batchNumber) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(pi." & "batch_number" & ") " & batchNumber.ToUpper
            End If

            If Not createdDateFrom = String.Empty Then
                'whereClauseConditions &= " AND " & Environment.NewLine & "pi.pre_invoice_creation_date is not null"
                whereClauseConditions &= " AND " & Environment.NewLine & "trunc(pi.pre_invoice_creation_date) >= '" & createdDateFrom.ToString & "'"
            End If
            If Not createdDateTo = String.Empty Then
                whereClauseConditions &= " AND " & Environment.NewLine & "trunc(pi.pre_invoice_creation_date) <= '" & createdDateTo.ToString & "'"
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, "ELP_PRE_INVOICE")
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetPreInvoiceDAL(ByVal CompanyCode As String, ByVal ServiceCenterCode As String, ByVal SCPreInvoiceDateFrom As DateTime, ByVal SCPreInvoiceDateTo As DateTime) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_PRE_INVOICE_LIST")
        Dim fromDate As Date
        Dim toDate As Date
        Dim whereClauseCondition As String

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_CODE, CompanyCode),
                                                New OracleParameter(COL_NAME_SERVICE_CENTER_CODE, ServiceCenterCode),
                                                New OracleParameter(COL_NAME_COMPANY_CODE, CompanyCode),
                                                New OracleParameter(COL_NAME_SERVICE_CENTER_CODE, ServiceCenterCode)}

        If (SCPreInvoiceDateFrom = Nothing) And (Not SCPreInvoiceDateTo = Nothing) Then
            whereClauseCondition = " AND pi.sc_display_date < to_date('" & SCPreInvoiceDateTo.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseCondition)
        ElseIf (Not SCPreInvoiceDateFrom = Nothing) And (SCPreInvoiceDateTo = Nothing) Then
            whereClauseCondition = " AND pi.sc_display_date between to_date('" & SCPreInvoiceDateFrom.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " and " & " to_date('" & Date.Today.AddDays(-1).ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseCondition)
        ElseIf (Not SCPreInvoiceDateFrom = Nothing) And (Not SCPreInvoiceDateTo = Nothing) Then
            whereClauseCondition = " AND pi.sc_display_date between to_date('" & SCPreInvoiceDateFrom.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " and " & " to_date('" & SCPreInvoiceDateTo.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseCondition)
        Else
            whereClauseCondition = ""
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseCondition)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_PRE_INVOICE_WS, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'For future use
    Public Function GetBonusAmount(ByVal BatchNumber As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TOTAL_BONUS_AMOUNT")

        Dim parameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BATCH_NUMBER, BatchNumber)}

        Try
            Return DBHelper.ExecuteScalar(selectStmt, parameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'For future use
    Public Function GetTotalAmount(ByVal BatchNumber As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TOTAL_AMOUNT")

        Dim parameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BATCH_NUMBER, BatchNumber)}

        Try
            Return DBHelper.ExecuteScalar(selectStmt, parameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetTotalBonusAndAmount(ByVal BatchNumber As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TOTAL_BONUS_AND_AMOUNT")

        Dim parameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BATCH_NUMBER, BatchNumber)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameter)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class


