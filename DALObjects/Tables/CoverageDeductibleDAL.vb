Public Class CoverageDeductibleDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COVERAGE_DED"
    Public Const TABLE_KEY_NAME As String = "coverage_ded_id"
    Public Const COL_NAME_COVERAGE_DED_ID As String = "coverage_ded_id"
    Public Const COL_NAME_COVERAGE_ID As String = "coverage_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = "deductible_based_on_id"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_METHOD_OF_REPAIR As String = "method_of_repair"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON As String = "deductible_based_on"
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = "deductible_expression_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_DED_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(coverageId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim covergeParam As New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_ID, coverageId.ToByteArray)
        Dim languageParam As New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId)
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {languageParam, languageParam, covergeParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(familyDs As DataSet, coverageId As Guid, languageId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim covergeParam As New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_ID, coverageId)
        Dim languageParam As New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId)
        Try
            DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam, languageParam})
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region
End Class
