'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2006)********************


Public Class PremiumAdjustmentDetailsDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PREMIUM_ADJUSTMENT_DETAIL"
    Public Const TABLE_KEY_NAME As String = "premium_adjustment_id"
    Public Const COL_NAME_PREMIUM_ADJUSTMENT_ID As String = "premium_adjustment_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_PROCESS_DATE As String = "process_date"
    Public Const COL_NAME_ADJUSTED_GROSS_AMT_RECEIVED As String = "adjusted_gross_amt_received"
    Public Const COL_NAME_ADJUSTED_PREMIUM As String = "adjusted_premium"
    Public Const COL_NAME_ADJUSTED_COMMISSION As String = "adjusted_commission"
    Public Const COL_NAME_ADJUSTED_PREM_TAX1 As String = "adjusted_prem_tax1"
    Public Const COL_NAME_ADJUSTED_PREM_TAX2 As String = "adjusted_prem_tax2"
    Public Const COL_NAME_ADJUSTED_PREM_TAX3 As String = "adjusted_prem_tax3"
    Public Const COL_NAME_ADJUSTED_PREM_TAX4 As String = "adjusted_prem_tax4"
    Public Const COL_NAME_ADJUSTED_PREM_TAX5 As String = "adjusted_prem_tax5"
    Public Const COL_NAME_ADJUSTED_PREM_TAX6 As String = "adjusted_prem_tax6"



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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("premium_adjustment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(DealerId As Guid, CompanyId As Guid, compIds As ArrayList) As DataSet 

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        ' hextoraw
        inCausecondition &= MiscUtil.BuildListForSql("c.company_id", compIds, True)


        If Not IsNothing(CompanyId) Then
            whereClauseConditions &= " AND c.company_id = '" & GuidToSQLString(CompanyId) & "'"
        End If

        If Not IsNothing(DealerId) Then
            whereClauseConditions &= " AND d.dealer_id = '" & GuidToSQLString(DealerId) & "'"
        End If

       

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_DEALER_CODE)
        Try

            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub Delete(premiumAdjustmentId As Guid)
        Try
            Dim deleteStatement As String = Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PREMIUM_ADJUSTMENT_ID, premiumAdjustmentId.ToByteArray)}
            DBHelper.Execute(deleteStatement, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region




End Class


