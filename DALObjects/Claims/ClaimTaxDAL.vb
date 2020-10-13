'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/8/2009)********************


Public Class ClaimTaxDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_TAX"
    Public Const TABLE_KEY_NAME As String = "claim_tax_id"

    Public Const COL_NAME_CLAIM_TAX_ID As String = "claim_tax_id"
    Public Const COL_NAME_CLAIM_INVOICE_ID As String = "claim_invoice_id"
    Public Const COL_NAME_DISBURSEMENT_ID As String = "disbursement_id"
    Public Const COL_NAME_TAX_TYPE_ID As String = "tax_type_id"
    Public Const COL_NAME_TAX1_AMOUNT As String = "tax1_amount"
    Public Const COL_NAME_TAX1_DESCRIPTION As String = "tax1_description"
    Public Const COL_NAME_TAX2_AMOUNT As String = "tax2_amount"
    Public Const COL_NAME_TAX2_DESCRIPTION As String = "tax2_description"
    Public Const COL_NAME_TAX3_AMOUNT As String = "tax3_amount"
    Public Const COL_NAME_TAX3_DESCRIPTION As String = "tax3_description"
    Public Const COL_NAME_TAX4_AMOUNT As String = "tax4_amount"
    Public Const COL_NAME_TAX4_DESCRIPTION As String = "tax4_description"
    Public Const COL_NAME_TAX5_AMOUNT As String = "tax5_amount"
    Public Const COL_NAME_TAX5_DESCRIPTION As String = "tax5_description"
    Public Const COL_NAME_TAX6_AMOUNT As String = "tax6_amount"
    Public Const COL_NAME_TAX6_DESCRIPTION As String = "tax6_description"

    Private Const DSNAME As String = "INVOICE_LIST"

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
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CLAIM_INVOICE_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIM_INVOICE_ID, id.ToByteArray)}
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

    Public Function LoadListByClaimInvoice(claimInvoiceId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_CLAIM_INVOICE_ID")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(COL_NAME_CLAIM_INVOICE_ID, claimInvoiceId.ToByteArray)}
        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadClaimTaxCountry(claimId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CLAIM_TAX_COUNTRY")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("claim_id", claimId.ToByteArray)}
        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters)
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



