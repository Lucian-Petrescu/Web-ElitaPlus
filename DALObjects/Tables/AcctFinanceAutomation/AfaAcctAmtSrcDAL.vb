'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/9/2015)********************


Public Class AfaAcctAmtSrcDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_ACCT_AMT_SRC"
    Public Const TABLE_KEY_NAME As String = "acct_amt_src_id"

    Public Const COL_NAME_ACCT_AMT_SRC_ID As String = "acct_amt_src_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_ACCT_AMT_SRC_FIELD_TYPE_ID As String = "acct_amt_src_field_type_id"
    Public Const COL_NAME_ENTITY_BY_REGION As String = "entity_by_region"
    Public Const COL_NAME_ENTITY_BY_REGION_COVERAGE_TYPE As String = "entity_by_region_coverage_type"
    Public Const COL_NAME_RECONCIL_WITH_INVOICE As String = "reconcil_with_invoice"
    Public Const COL_NAME_USE_FORMULA_FOR_CLIP As String = "use_formula_for_clip"

    Public Const TABLE_NAME_LIST_MAPPED As String = "AMT_SRC_LIST_MAPPED"
    Public Const TABLE_NAME_LIST_NOT_MAPPED As String = "AMT_SRC_LIST_NOT_MAPPED"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_amt_src_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerID As Guid, languageID As Guid) As DataSet
        Dim selectStmt As String
        Dim paraMapped() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}

        Dim paraNotMapped() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                              New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray)}

        Dim ds As New DataSet

        Try
            selectStmt = Config("/SQL/LOAD_LIST_BY_DEALER_MAPPED")
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME_LIST_MAPPED, paraMapped)

            selectStmt = Config("/SQL/LOAD_LIST_BY_DEALER_NOT_MAPPED")
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME_LIST_NOT_MAPPED, paraNotMapped)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadDuplicateClipFormula(dealerID As Guid, AmtSourceID As Guid) As DataSet
        Dim selectStmt As String
        Dim paraMapped() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("acct_amt_src_field_type_id", AmtSourceID.ToByteArray)}


        Dim ds As New DataSet

        Try
            selectStmt = Config("/SQL/CheckDuplicateCLIPFormula")
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, paraMapped)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
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


