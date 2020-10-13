'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/14/2017)********************


Public Class SpClaimTypesDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SP_CLAIM_TYPES"
    Public Const TABLE_KEY_NAME As String = "sp_claim_type_id"
    Public Const TABLE_UNIQUE_KEY_NAME As String = "code"

    Public Const COL_NAME_SP_CLAIM_TYPE_ID As String = "sp_claim_type_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_CLAIM_TYPE As String = "claim_type"
    Public Const COL_NAME_SP_CLAIM_CATEGORY As String = "sp_claim_category"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_VALUE_TYPE As String = "value_type"

    Public Const COL_NAME_SP_CLAIM_CODE_DESCRIPTION As String = "sp_claim_code_description"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("sp_claim_type_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub Load(ds As DataSet, code As String)
        Dim selectStmt As String = Config("/SQL/LOAD_BASED_ON_SP_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("code", code)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


End Class


