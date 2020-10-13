'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/11/2007)********************


Public Class VSCCoverageRateDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_COVERAGE_RATE"
    Public Const TABLE_KEY_NAME As String = "vsc_coverage_rate_id"

    Public Const COL_NAME_VSC_COVERAGE_RATE_ID As String = "vsc_coverage_rate_id"
    Public Const COL_NAME_VSC_RATE_VERSION_ID As String = "vsc_rate_version_id"
    Public Const COL_NAME_VSC_COVERAGE_ID As String = "vsc_coverage_id"
    Public Const COL_NAME_CLASS_CODE_ID As String = "class_code_id"
    Public Const COL_NAME_ODOMETER_LOW_RANGE As String = "odometer_low_range"
    Public Const COL_NAME_ODOMETER_HIGH_RANGE As String = "odometer_high_range"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_DISCOUNTED_DEDUCTIBLE_AMT As String = "discounted_deductible_amt"
    Public Const COL_NAME_DISCOUNTED_DEDUCTIBLE_PCNT As String = "discounted_deductible_pcnt"
    Public Const COL_NAME_TERM_MONTHS As String = "term_months"
    Public Const COL_NAME_TERM_KM_MI As String = "term_km_mi"
    Public Const COL_NAME_COMMISSIONS_PERCENT As String = "commissions_percent"
    Public Const COL_NAME_MARKETING_PERCENT As String = "marketing_percent"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_PROFIT_EXPENSE As String = "profit_expense"
    Public Const COL_NAME_LOSS_COST_PERCENT As String = "loss_cost_percent"
    Public Const COL_NAME_WP As String = "wp"
    Public Const COL_NAME_TAXES_PERCENT As String = "taxes_percent"
    Public Const COL_NAME_GWP As String = "gwp"
    Public Const COL_NAME_ENGINE_MANUF_WARR_MONTHS As String = "engine_manuf_warr_months"
    Public Const COL_NAME_ENGINE_MANUF_WARR_KM_MI As String = "engine_manuf_warr_km_mi"
    Public Const COL_NAME_VEHICLE_PURCHASE_PRICE_FROM As String = "vehicle_purchase_price_from"
    Public Const COL_NAME_VEHICLE_PURCHASE_PRICE_TO As String = "vehicle_purchase_price_to"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("vsc_coverage_rate_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(RateVersionID As Guid, PlanID As Guid, EngineWarranty As Guid,
                            Optional ByVal ClassCode As String = "", Optional ByVal TermMon As Integer = -1,
                            Optional ByVal Deductible As Decimal = -1, Optional ByVal Odometer As Integer = -1,
        Optional ByVal VehicleValue As String = "") As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""


        If FormatSearchMask(ClassCode) Then
            whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(cc.CLASS_CODE) " & ClassCode
        End If

        If TermMon >= 0 Then
            whereClauseConditions &= Environment.NewLine & " AND cr." & COL_NAME_TERM_MONTHS & " = " & TermMon
        End If

        If Deductible >= 0 Then
            whereClauseConditions &= Environment.NewLine & " AND cr." & COL_NAME_DEDUCTIBLE & " = " & Deductible
        End If

        If Odometer >= 0 Then
            whereClauseConditions &= Environment.NewLine & " AND " & Odometer & " between cr." & COL_NAME_ODOMETER_LOW_RANGE & " and cr." & COL_NAME_ODOMETER_HIGH_RANGE
        End If

        If EngineWarranty <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and (cr." & COL_NAME_ENGINE_MANUF_WARR_MONTHS & ",cr." & COL_NAME_ENGINE_MANUF_WARR_KM_MI & ") in (select COVERAGE_MONTHS, COVERAGE_KM_MI FROM ELP_VSC_COVERAGE_LIMIT WHERE getcodefromlistitem(coverage_type_id) = '1' and VSC_COVERAGE_LIMIT_ID = '" & GuidToSQLString(EngineWarranty) & "')"
        End If

        If VehicleValue >= 0 Then
            whereClauseConditions &= Environment.NewLine & " And " & VehicleValue & " between cr." & COL_NAME_VEHICLE_PURCHASE_PRICE_FROM & " And cr." & COL_NAME_VEHICLE_PURCHASE_PRICE_TO
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                    New DBHelper.DBHelperParameter("VSC_RATE_VERSION_ID", RateVersionID.ToByteArray),
                    New DBHelper.DBHelperParameter("VSC_PLAN_ID", PlanID.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


