'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransDtlPartApprovalDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_DTL_PART_APPROVAL"
    Public Const TABLE_KEY_NAME As String = "trans_dtl_parts_appr_id"

    Public Const COL_NAME_TRANS_DTL_PARTS_APPR_ID As String = "trans_dtl_parts_appr_id"
    Public Const COL_NAME_TRANS_DTL_CLAIM_UPDATE_2GVS_ID As String = "trans_dtl_claim_update_2gvs_id"
    Public Const COL_NAME_XML_PART_DESCRIPTION_CODE As String = "xml_part_description_code"
    Public Const COL_NAME_XML_PART_COST As String = "xml_part_cost"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_dtl_parts_appr_id", id.ToByteArray)}
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


