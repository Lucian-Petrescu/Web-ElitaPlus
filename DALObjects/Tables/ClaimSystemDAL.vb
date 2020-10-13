'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/23/2010)********************


Public Class ClaimSystemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_SYSTEM"
    Public Const TABLE_KEY_NAME As String = "claim_system_id"

    Public Const COL_NAME_CLAIM_SYSTEM_ID As String = "claim_system_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_NEW_CLAIM_ID As String = "new_claim_id"
    Public Const COL_NAME_PAY_CLAIM_ID As String = "pay_claim_id"
    Public Const COL_NAME_MAINTAIN_CLAIM_ID As String = "maintain_claim_id"
    Public Const COL_NAME_NEW_CLAIM As String = "new_claim"
    Public Const COL_NAME_PAY_CLAIM As String = "pay_claim"
    Public Const COL_NAME_MAINTAIN_CLAIM As String = "maintain_claim"

    Private Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_system_id", id.ToByteArray)}
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

    Public Function LoadList(description As String, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DESCRIPTION, description)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
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



