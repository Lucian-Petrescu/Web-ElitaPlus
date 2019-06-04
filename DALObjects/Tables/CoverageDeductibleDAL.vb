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

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_DED_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal coverageId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_ID, coverageId.ToByteArray)
        Dim languageParam As New DBHelper.DBHelperParameter(Me.COL_NAME_LANGUAGE_ID, languageId)
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {languageParam, languageParam, covergeParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal familyDs As DataSet, ByVal coverageId As Guid, ByVal languageId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_ID, coverageId)
        Dim languageParam As New DBHelper.DBHelperParameter(Me.COL_NAME_LANGUAGE_ID, languageId)
        Try
            DBHelper.Fetch(familyDs, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam, languageParam})
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
