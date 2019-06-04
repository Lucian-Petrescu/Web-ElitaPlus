'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/27/2011)********************


Public Class TransallMappingOutDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANSALL_MAPPING_OUT"
    Public Const TABLE_KEY_NAME As String = "transall_mapping_out_id"

    Public Const COL_NAME_TRANSALL_MAPPING_OUT_ID As String = "transall_mapping_out_id"
    Public Const COL_NAME_TRANSALL_MAPPING_ID As String = "transall_mapping_id"
    Public Const COL_NAME_OUTPUT_MASK As String = "output_mask"
    Public Const COL_NAME_LAYOUT_CODE_ID As String = "layout_code_id"

    Public Const COL_NAME_VIEW_LAYOUT_CODE As String = "layout_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("transall_mapping_out_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal TransallMappingId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_TRANSALL_MAPPING_ID, TransallMappingId.ToByteArray)}
        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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


