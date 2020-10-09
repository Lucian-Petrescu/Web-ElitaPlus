'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/17/2007)********************

Public Class RegionTaxDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REGION_TAX_DETAIL"
    Public Const TABLE_KEY_NAME As String = "region_tax_detail_id"

    Public Const COL_NAME_REGION_TAX_DETAIL_ID As String = "region_tax_detail_id"
    Public Const COL_NAME_REGION_TAX_ID As String = "region_tax_id"
    Public Const COL_NAME_TAX_BUCKET As String = "tax_bucket"
    Public Const COL_NAME_PERCENT As String = "percent"
    Public Const COL_NAME_NON_TAXABLE As String = "non_taxable"
    Public Const COL_NAME_MINIMUM_TAX As String = "minimum_tax"
    Public Const COL_NAME_GL_ACCOUNT_NUMBER As String = "gl_account_number"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("region_tax_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(RegionTaxId As Guid, ByRef ds As DataSet)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters As DBHelper.DBHelperParameter()
        parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Region_Tax_id", RegionTaxId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


