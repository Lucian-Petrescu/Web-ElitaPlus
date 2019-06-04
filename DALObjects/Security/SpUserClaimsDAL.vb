'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/13/2017)********************


Public Class SpUserClaimsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SP_USER_CLAIMS"
    Public Const TABLE_KEY_NAME As String = "sp_user_claims_id"

    Public Const COL_NAME_SP_USER_CLAIMS_ID As String = "sp_user_claims_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_SP_CLAIM_TYPE_ID As String = "sp_claim_type_id"
    Public Const COL_NAME_SP_CLAIM_VALUE As String = "sp_claim_value"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("sp_user_claims_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadSpUserClaims(ByVal UserId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_SP_USER_CLAIMS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", UserId.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "LoadSpUserClaims", parameters)
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


