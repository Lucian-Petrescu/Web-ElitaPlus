'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransDtlPartDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_DTL_PART"
    Public Const TABLE_KEY_NAME As String = "trans_dtl_part_id"

    Public Const COL_NAME_TRANS_DTL_PART_ID As String = "trans_dtl_part_id"
    Public Const COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID As String = "trans_dtl_clm_updte_2elita_id"
    Public Const COL_NAME_XML_MFG_PART_CODE As String = "xml_mfg_part_code"
    Public Const COL_NAME_XML_PART_DESCRIPTION_CODE As String = "xml_part_description_code"
    Public Const COL_NAME_XML_PART_COST As String = "xml_part_cost"
    Public Const COL_NAME_XML_PART_DEFECT As String = "xml_part_defect"
    Public Const COL_NAME_XML_PART_SOLUTION As String = "xml_part_solution"
    Public Const COL_NAME_XML_IN_STOCK As String = "xml_in_stock"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_dtl_part_id", id.ToByteArray)}
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


