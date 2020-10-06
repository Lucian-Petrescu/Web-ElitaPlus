'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/26/2012)********************


Public Class ClaimIssueResponseDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_ISSUE_RESPONSE"
    Public Const TABLE_KEY_NAME As String = "claim_issue_response_id"

    Public Const COL_NAME_CLAIM_ISSUE_RESPONSE_ID As String = "claim_issue_response_id"
    Public Const COL_NAME_CLAIM_ISSUE_ID As String = "claim_issue_id"
    Public Const COL_NAME_ANSWER_ID As String = "answer_id"
    Public Const COL_NAME_SUPPORTS_CLAIM_ID As String = "supports_claim_id"
    Public Const COL_NAME_ANSWER_DESCRIPTION As String = "answer_description"
    Public Const COL_NAME_ANSWER_VALUE As String = "answer_value"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_issue_response_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    

    Public Sub LoadList(familyDS As DataSet, ClaimIssueId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                              {New OracleParameter(COL_NAME_CLAIM_ISSUE_ID, OracleDbType.Raw, 16)}
        Try
            parameters(0).Value = ClaimIssueId.ToByteArray

            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


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
#End Region


End Class


