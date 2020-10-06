'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/14/2016)********************


Public Class ClaimBonusSettingsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_BONUS_SETTINGS"
    Public Const TABLE_KEY_NAME As String = "claim_bonus_settings_id"

    Public Const COL_NAME_CLAIM_BONUS_SETTINGS_ID As String = "claim_bonus_settings_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_BONUS_COMPUTE_METHOD_ID As String = "bonus_compute_method_id"
    Public Const COL_NAME_BONUS_AMOUNT_PERIOD_MONTH As String = "bonus_amount_period_month"
    Public Const COL_NAME_SC_REPLACEMENT_PCT As String = "sc_replacement_pct"
    Public Const COL_NAME_SC_AVG_TAT As String = "sc_avg_tat"
    Public Const COL_NAME_PECORAMOUNT As String = "pecoramount"
    Public Const COL_NAME_PRIORITY As String = "priority"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_SERVICE_CENTER As String = "Service_center"
    Public Const COL_NAME_DEALER_NAME As String = "dealer"
    Public Const COL_NAME_PRODUCT_CODE As String = "product"
    Public Const COL_NAME_BONUS_COMPUTE_METHOD As String = "compute_bonus_method"



#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_bonus_settings_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerId As Guid, servicecenterId As Guid, productcodeId As Guid, companyIds As ArrayList,
                             companyGroupId As Guid) As DataSet



        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        'Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        ' inCausecondition &= MiscUtil.BuildListForSql("elp_dealer.company_id", companyIds, True)


        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND cbs.dealer_id = '" & GuidToSQLString(dealerId) & "'"
        End If
        If Not servicecenterId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND cbs.service_center_id = '" & GuidToSQLString(servicecenterId) & "'"
        End If
        If Not productcodeId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND cbs.product_code_id = '" & GuidToSQLString(productcodeId) & "'"
        End If

        'selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ClaimBonusSettingsCount(dealerId As Guid, servicecenterId As Guid, productcodeId As Guid, BonusSettingsId As Guid) As Object

        Dim selectStmt As String = Config("/SQL/BONUS_SETTINGS_COUNT")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
         New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, servicecenterId.ToByteArray),
            New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, productcodeId.ToByteArray),
            New DBHelper.DBHelperParameter(COL_NAME_CLAIM_BONUS_SETTINGS_ID, BonusSettingsId.ToByteArray)}

        Try
            Return DBHelper.ExecuteScalar(selectStmt, parameters)
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Sub Delete(BonusSettingsId As Guid)
        Try
            Dim deleteStatement As String = Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_BONUS_SETTINGS_ID, BonusSettingsId.ToByteArray)}
            DBHelper.Execute(deleteStatement, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region


End Class


