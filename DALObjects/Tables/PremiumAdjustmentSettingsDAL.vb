Public Class PremiumAdjustmentSettingsDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PREMIUM_ADJUSTMENT_SETTING"
    Public Const TABLE_KEY_NAME As String = "premium_adjustment_setting_id"

    Public Const COL_NAME_PREMIUM_ADJUSTMENT_SETTING_ID = "premium_adjustment_setting_id"
    Public Const COL_NAME_DEALER_ID = "dealer_id"
    Public Const COL_NAME_DEALER_CODE = "dealer_code"
    Public Const COL_NAME_ADJUSTMENT_BY = "adjustment_by"
    Public Const COL_NAME_ADJUSTMENT_BASED_ON = "adjustment_based_on"
    Public Const COL_NAME_EFFECTIVE_DATE = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE = "expiration_date"
    Public Const COL_NAME_ADJUSTMENT_PERCENTAGE = "adjustment_percentage"
    Public Const COL_NAME_ADJUSTMENT_AMOUNT = "adjustment_amount"
    Private Const DSNAME As String = "LIST"


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("premium_adjustment_setting_id", id.ToByteArray)}
        'Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("cancellation_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal DealerId As Guid, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""


        ' hextoraw
        inCausecondition &= MiscUtil.BuildListForSql("c.company_id", compIds, True)

        If Not Me.IsNothing(DealerId) Then
            whereClauseConditions &= " AND d.dealer_id = '" & Me.GuidToSQLString(DealerId) & "'"
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & Me.COL_NAME_DEALER_CODE)

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function



    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
        LookupListCache.ClearFromCache(Me.GetType.ToString)
    End Sub



#End Region
End Class
