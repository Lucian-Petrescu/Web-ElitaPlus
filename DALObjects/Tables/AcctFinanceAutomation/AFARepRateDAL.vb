'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/29/2015)********************


Public Class AFARepRateDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_REPORTING_RATES"
    Public Const TABLE_KEY_NAME As String = "afa_reporting_rate_id"

    Public Const COL_NAME_AFA_REPORTING_RATE_ID As String = "afa_reporting_rate_id"
    Public Const COL_NAME_AFA_INVOICE_RATE_ID As String = "afa_invoice_rate_id"
    Public Const COL_NAME_RISK_FEE As String = "risk_fee"
    Public Const COL_NAME_SPM_COE As String = "spm_coe"
    Public Const COL_NAME_FULLFILLMENT_NOTIFICATION As String = "fullfillment_notification"
    Public Const COL_NAME_MARKETING_EXPENSES As String = "marketing_expenses"
    Public Const COL_NAME_PREMIUM_TAXES As String = "premium_taxes"
    Public Const COL_NAME_LOSS_RESERVE_COST As String = "loss_reserve_cost"
    Public Const COL_NAME_OVERHEAD As String = "overhead"
    Public Const COL_NAME_GENERAL_EXPENSES As String = "general_expenses"
    Public Const COL_NAME_ASSESSMENTS As String = "assessments"
    Public Const COL_NAME_LAE As String = "lae"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_reporting_rate_id", id.ToByteArray)}
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


    Public Function LoadList(ByVal afaInvoiceRateId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/RPTRATE_LOAD_LIST")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_invoice_rate_id", afaInvoiceRateId.ToByteArray)}

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
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class