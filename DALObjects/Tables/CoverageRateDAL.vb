'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2006)********************


Public Class CoverageRateDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COVERAGE_RATE"
    Public Const TABLE_KEY_NAME As String = "coverage_rate_id"

    Public Const COL_NAME_COVERAGE_RATE_ID As String = "coverage_rate_id"
    Public Const COL_NAME_COVERAGE_ID As String = "coverage_id"
    Public Const COL_NAME_LOW_PRICE As String = "low_price"
    Public Const COL_NAME_HIGH_PRICE As String = "high_price"
    Public Const COL_NAME_GROSS_AMT As String = "gross_amt"
    Public Const COL_NAME_COMMISSIONS_PERCENT As String = "commissions_percent"
    Public Const COL_NAME_MARKETING_PERCENT As String = "marketing_percent"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_PROFIT_EXPENSE As String = "profit_expense"
    Public Const COL_NAME_LOSS_COST_PERCENT As String = "loss_cost_percent"
    Public Const COL_NAME_GROSS_AMOUNT_PERCENT As String = "gross_amount_percent"
    Public Const COL_NAME_RENEWAL_NUMBER As String = "renewal_number"


    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CERTIFICATE_DURATION As String = "certificate_duration"
    Public Const TABLE_NAME_PRICE_BANDS As String = "PRICE_BANDS"
    Public Const COL_NAME_PURCHASE_PRICE As String = "purchase_price"

    Public Const PARAM_NAME_DEALER_ID As String = "p_dealer_id"
    Public Const PARAM_NAME_PRODUCT_CODE As String = "p_product_code"
    Public Const PARAM_NAME_CERTIFICATE_DURATION As String = "p_cert_duration"
    Public Const PARAM_NAME_MANUFACTURE_DURATION As String = "p_man_duration"
    Public Const PARAM_NAME_PURCHASE_PRICE As String = "p_product_price"
    Public Const PARAM_NAME_WARRANTY_SALES_DATE As String = "p_warranty_sales_date"
    Public Const PARAM_NAME_PRODUCT_SALES_DATE As String = "p_product_sales_date"
    Public Const PARAM_NAME_CONTRACT_RECORD As String = "p_contract_record"

    Public Const PARAM_NAME_EXPECTED_GWP As String = "p_assurant_gwp"
    Public Const PARAM_NAME_RETURN As String = "p_return"
    Public Const PARAM_NAME_EXCEPTION_MSG As String = "p_exception_msg"

    Public Const P_EXPECTED_GWP As Integer = 0
    Public Const P_RETURN As Integer = 1
    Public Const P_EXCEPTION_MSG As Integer = 2



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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_rate_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal coverageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_ID, coverageId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam})
        Return ds
    End Function
    Public Function LoadCovRateListForDelete(ByVal coverageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COVRATE_LIST_DELETE")
        Dim ds As New DataSet
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_ID, coverageId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam})
        Return ds
    End Function

    Public Function GetExpectedGWP(ByVal DealerId As Guid, ByVal ProductCode As String, ByVal certificate_duration As Integer, _
                                   ByVal WarrSalesDate As Date, ByVal PurchasePrice As Double, ByVal CoverageDuration As Integer, _
                                   ByVal ProductPurchaseDate As Date) As Object
        Dim selectStmt As String = Me.Config("/SQL/LOAD_EXPECTED_GWP")

        ' Input parameters
        Dim inputParameters() As DBHelper.DBHelperParameter
        inputParameters = New DBHelper.DBHelperParameter() _
                                   {New DBHelper.DBHelperParameter(PARAM_NAME_DEALER_ID, DealerId.ToByteArray), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_PRODUCT_CODE, ProductCode), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_CERTIFICATE_DURATION, certificate_duration), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_MANUFACTURE_DURATION, IIf(CoverageDuration = -1, System.DBNull.Value, CoverageDuration)), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_PURCHASE_PRICE, PurchasePrice, GetType(Double)), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_WARRANTY_SALES_DATE, WarrSalesDate, GetType(Date)), _
                                    New DBHelper.DBHelperParameter(PARAM_NAME_PRODUCT_SALES_DATE, IIf(ProductPurchaseDate = Date.MinValue, System.DBNull.Value, ProductPurchaseDate), GetType(Date))}


        ' Output parameters
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                        {New DBHelper.DBHelperParameter(PARAM_NAME_EXPECTED_GWP, GetType(Double)), _
                                                         New DBHelper.DBHelperParameter(PARAM_NAME_RETURN, GetType(Integer)), _
                                                         New DBHelper.DBHelperParameter(PARAM_NAME_EXCEPTION_MSG, GetType(String), 200)}
        ' Call the stored procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If outputParameters(P_RETURN).Value <> 0 Then _
            Throw New StoredProcedureGeneratedException("GetExpectedGWP Error: ", outputParameters(P_EXCEPTION_MSG).Value)

        Return outputParameters(P_EXPECTED_GWP).Value

    End Function

    Public Function LoadDealerCoverageRatesInfo(ByRef ds As DataSet, ByVal dealerId As Guid, ByVal WarrSalesDate As Date) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_COVERAGE_RATES_INFO_FOR_WS")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""

        Dim strWarrSalesDate As String = WarrSalesDate.ToString("MM/dd/yyyy")

        parameters = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                New DBHelper.DBHelperParameter(COL_NAME_WARRANTY_SALES_DATE, WarrSalesDate, GetType(Date)) _
               }

        If ds Is Nothing Then ds = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "COVERAGE_RATES", parameters)

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



