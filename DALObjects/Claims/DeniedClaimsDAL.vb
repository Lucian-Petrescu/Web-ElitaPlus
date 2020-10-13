

Public Class DeniedClaimsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DENIED_CLAIMS"
    Public Const TABLE_KEY_NAME As String = "denied_claims_id"

    Public Const COL_NAME_DENIED_CLAIMS_ID As String = "denied_claims_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_DENIED_REASON1_ID As String = "denied_reason1_id"
    Public Const COL_NAME_CONDITION_PROBLEM_1 As String = "condition_problem_1"
    Public Const COL_NAME_CONDITION_PROBLEM_2 As String = "condition_problem_2"
    Public Const COL_NAME_CONDITION_PROBLEM_3 As String = "condition_problem_3"
    Public Const COL_NAME_APPROVER_ID As String = "approver_id"
    Public Const COL_NAME_DENIED_REASON2_ID As String = "denied_reason2_id"
    Public Const COL_NAME_DENIED_REASON3_ID As String = "denied_reason3_id"
    Public Const COL_NAME_DENIED_REASON4_ID As String = "denied_reason4_id"
    Public Const COL_NAME_DENIED_REASON5_ID As String = "denied_reason5_id"
    Public Const COL_NAME_DENIED_REASON6_ID As String = "denied_reason6_id"
    Public Const COL_NAME_DENIED_REASON7_ID As String = "denied_reason7_id"
    Public Const COL_NAME_DENIED_REASON8_ID As String = "denied_reason8_id"
    Public Const COL_NAME_DENIED_REASON9_ID As String = "denied_reason9_id"
    Public Const COL_NAME_DENIED_REASON10_ID As String = "denied_reason10_id"
    Public Const COL_NAME_DENIED_REASON11_ID As String = "denied_reason11_id"
    Public Const COL_NAME_DENIED_REASON12_ID As String = "denied_reason12_id"
    Public Const COL_NAME_DENIED_REASON13_ID As String = "denied_reason13_id"
    Public Const COL_NAME_DENIED_REASON14_ID As String = "denied_reason14_id"
    Public Const COL_NAME_DENIED_REASON15_ID As String = "denied_reason15_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_TRANSLATION As String = "translation"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("denied_claims_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function LoadLetterList(rdClaimId As Guid, rdLangID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LETTER_LIST")


        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                         New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, rdClaimId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, rdLangID.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        'Dim ds As New DataSet
        'Dim selectStmt As String = Me.Config("/SQL/LOAD_LETTER_LIST")
        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_ID, rdClaimId.ToByteArray)}
        'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Return ds
    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function
    Public Sub LoadAuthorizedApprover(ds As DataSet, rdClaimId As Guid)

        Dim selectStmt As String = Config("/SQL/LOAD_AUTHORIZED_APPROVER_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_ID, rdClaimId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

    End Sub

    Public Sub LoadAvailableDRs(ds As DataSet, rdClaimId As Guid, rdLangID As Guid)

        Dim selectStmt As String = Config("/SQL/LOAD_AVAILABLE_DENIED_REASONS_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, rdLangID.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)


        '
    End Sub
    Public Sub LoadSelectedDRs(ds As DataSet, rdClaimId As Guid, rdLangID As Guid)

        Dim selectStmt As String = Config("/SQL/LOAD_SELECTED_DENIED_REASONS_LIST")
        
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, rdLangID.ToByteArray), _
                                               New OracleParameter(COL_NAME_DENIED_CLAIMS_ID, rdClaimId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Sub
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


