Public Class DealerInflationDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALER_INFLATION"
    Public Const TABLE_KEY_NAME As String = "DEALER_INFLATION_ID"
	
    Public Const COL_NAME_DEALER_INFLATION_ID As String = "DEALER_INFLATION_ID"
    Public Const COL_NAME_COMPANY_ID As String = "COMPANY_ID"
    Public Const COL_NAME_DEALER_ID As String = "DEALER_ID"
    Public Const COL_NAME_INFLATION_MONTH As String = "INFLATION_MONTH"
    Public Const COL_NAME_INFLATION_YEAR As String = "INFLATION_YEAR"
    Public Const COL_NAME_CLAIM_ISSUE_ID As String = "INFLATION_PCT"
    

    'stored procedure parameter names
    Public Const PAR_NAME_INFLATION_PCT As String = "PI_INFLATION_PCT"
    Public Const PAR_NAME_DEALER As String = "PI_DEALER_ID"
    Public Const PAR_NAME_INFLATION_MONTH As String = "PI_INFLATION_MONTH"
    Public Const PAR_NAME_INFLATION_YEAR As String = "PI_INFLATION_YEAR"
    Public Const PAR_NAME_RETURN_CODE As String = "po_return_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_close_rule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadDealerInflation(ByVal companyId As Guid, ByVal dealerId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_INFLATION")
            Dim inParameters(0) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER, dealerId.ToByteArray)

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inParameters, nothing, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    

#End Region
End Class
