'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/11/2014)********************

Public Class ReppolicyClaimCountDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REPPOLICY_CLAIM_COUNT"
    Public Const TABLE_KEY_NAME As String = "reppolicy_claim_count_id"

    Public Const COL_NAME_REPPOLICY_CLAIM_COUNT_ID As String = "reppolicy_claim_count_id"
    Public Const COL_NAME_CONTRACT_ID As String = "contract_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_CERT_DURATION As String = "cert_duration"
    Public Const COL_NAME_CONVERAGE_TYPE_ID As String = "converage_type_id"
    Public Const COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT As String = "replacement_policy_claim_count"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("reppolicy_claim_count_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadListByContract(ContractID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("contract_id", ContractID.ToByteArray)}
        'Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("contract_id", ContractID)}
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Return ds
    End Function

    Public Function LoadCoverageTypeByDealer(dealerID As Guid, languageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_COVERAGE_TYPE_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
            New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray)}
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, "COVERAGE_TYPE", parameters)
        Return ds
    End Function

    Public Function LoadAvailCertDurationByDealer(dealerID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_AVAILABLE_CERT_DURATION_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, "COVERAGE_TYPE", parameters)
        Return ds
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

#Region "Public methods"
    Public Function LoadReplacementPolicyClaimCntByClaim(ContractID As Guid, ClaimID As Guid) As Long
        Dim sqlStmt As String
        Dim intClaimCount As Long
        sqlStmt = Config("/SQL/GetReplacementPolicyClaimCntByClaim")
        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("p_ClaimCount", intClaimCount.GetType)}

            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                New DBHelper.DBHelperParameter("p_contractid", ContractID.ToByteArray), _
                                                New DBHelper.DBHelperParameter("p_ClaimId", ClaimID.ToByteArray)}

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters, outParameters)

            If outParameters(0).Value IsNot Nothing Then
                intClaimCount = outParameters(0).Value
            End If
            Return intClaimCount
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


