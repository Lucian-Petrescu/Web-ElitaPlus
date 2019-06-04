'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/11/2005)********************


Public Class IbnrLossPaidDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_IBNR_LOSS_PAID"
    Public Const TABLE_KEY_NAME As String = "ibnr_loss_paid_id"

    Public Const COL_NAME_IBNR_LOSS_PAID_ID As String = "ibnr_loss_paid_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_COMPANY_KEY As String = "company_key"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_AMOUNT_OF_LOSS As String = "amount_of_loss"
    Public Const COL_NAME_DATE_OF_LOSS As String = "date_of_loss"
    Public Const COL_NAME_DATE_INVOICE_PAID As String = "date_invoice_paid"
    Public Const COL_NAME_PERIOD_IN_MONTHS As String = "period_in_months"
    Public Const COL_NAME_COVERAGE_TYPE As String = "coverage_type"
    Public Const COL_NAME_ACCOUNTING_DATE As String = "accounting_date"
    Public Const COL_NAME_ACCOUNTING_MMYYYY As String = "accounting_mmyyyy"
    Public Const COL_NAME_ACCOUNTING_MMYYYY_PAID As String = "accounting_mmyyyy_paid"


    Public Const COMPANY_ID = 0
    Public Const TOTAL_PARAM = 0 '1
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ibnr_loss_paid_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function GetIBNRLossPaidAccountingDate(ByVal companyId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DATES")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter

        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



