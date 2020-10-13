Public Class ProductRewardsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PROD_REWARDS"
    Public Const TABLE_KEY_NAME As String = "prod_reward_id"

    Public Const COL_NAME_PROD_REWARD_ID As String = "prod_reward_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_REWARD_NAME As String = "reward_name"
    Public Const COL_NAME_REWARD_TYPE As String = "reward_type"
    Public Const COL_NAME_MIN_PURCHASE_PRICE As String = "min_purchase_price"
    Public Const COL_NAME_REWARD_AMOUNT As String = "reward_amount"
    Public Const COL_NAME_DAYS_TO_REDEEM As String = "days_to_redeem"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_FROM_RENEWAL As String = "from_renewal"
    Public Const COL_NAME_TO_RENEWAL As String = "to_renewal"

    Public Const COL_NAME_DEALER_ID As String = "dealer_id"

    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("prod_reward_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
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


#Region "CRUD Methods"

    Public Function LoadList(ProductCodeId As Guid, familyDS As DataSet)

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        ' Dim ds As DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        Try

            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(languageId As Guid, ProductCodeId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                            New OracleParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        Try
            ds = DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)

            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("prod_reward_id")}

            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ValidateUniqueCombination(ProductId As Guid, RewardType As String, EffectiveDate As Date, ExpirationDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/VALIDATE_UNIQUE_COMBINATION")
        Dim ds As New DataSet
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductId),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_REWARD_TYPE, RewardType),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE_DATE, EffectiveDate),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION_DATE, ExpirationDate)
                                                                }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)
        Return ds
    End Function

    Public Function ValidateOverlap(ProductId As Guid, RewardType As String, EffectiveDate As Date, ExpirationDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/VALIDATE_OVERLAPPING")
        Dim ds As New DataSet
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductId),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_REWARD_TYPE, RewardType),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE_DATE, EffectiveDate),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_EXPIRATION_DATE, ExpirationDate)
                                                                }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)
        Return ds
    End Function

    Public Function ValidateRenewalOverlap(ProductId As Guid, ProductRewardId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/VALIDATE_RENEWAL_OVERLAPPING")
        Dim ds As New DataSet
        Dim inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductId),
                                                                 New DBHelper.DBHelperParameter(COL_NAME_PROD_REWARD_ID, ProductRewardId)
                                                                }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, inputparameters)
        Return ds
    End Function
#End Region


End Class
