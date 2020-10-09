Imports Assurant.ElitaPlus.DALObjects
Public Class ProductCodeParentDAL
    Inherits DALBase

#Region "Constants"

    Public Const TABLE_NAME As String = "ELP_PRODUCT_CODE_PARENT"
    Public Const TABLE_KEY_NAME As String = "product_code_parent_id"
    Public Const COL_NAME_PRODUCT_CODE_PARENT_ID As String = "product_code_parent_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_SMART_BUNDLE_FLAT_AMT As String = "smart_bundle_flat_amt"
    Public Const COL_NAME_SMART_BUNDLE_FLAT_AMT_CURRENCY As String = "smart_bundle_flat_amt_currency"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_PAYMENT_SPLIT_RULE_ID = "payment_split_rule_id"

    Public Const PAR_LANGUAGE_ID As String = "language_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_code_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function LoadList(DealerId As Guid, ProductId As Guid, languageId As Guid) As DataSet
        Try
            Dim selectstmt As String = Config("/SQL/LOAD_LIST")

            Dim whereClauseConditions As String = ""
            Dim ds As New DataSet

            If Not DealerId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "d.dealer_id = " & MiscUtil.GetDbStringFromGuid(DealerId)
            End If

            If Not ProductId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "pcp.product_code_id = " & MiscUtil.GetDbStringFromGuid(ProductId)
            End If

            If Not whereClauseConditions = "" Then
                selectstmt = selectstmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectstmt = selectstmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            selectstmt = selectstmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_PRODUCT_CODE)

            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(PAR_LANGUAGE_ID, languageId.ToByteArray)}

            DBHelper.Fetch(ds, selectstmt, TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception

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
#End Region



End Class
