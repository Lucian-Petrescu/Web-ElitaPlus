'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/10/2007)********************


Public Class CoverageByCompanyGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COVERAGE_BY_COMPANY_GROUP"
    Public Const TABLE_KEY_NAME As String = "coverage_by_company_group_id"

    Public Const COL_NAME_COVERAGE_BY_COMPANY_GROUP_ID As String = "coverage_by_company_group_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_by_company_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal languageid As Guid, ByVal grpID As Guid) As DataSet

        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")


        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpID.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Sub LoadList(ByVal CompanyGroupId As Guid, ByVal familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_COV_COMP_GRP")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)
    End Sub

    Public Function LoadAvailableCoverageType(ByVal grpID As Guid, ByVal languageid As Guid, ByVal coverageTypeId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_AVAILABLE_COVERAGE_TYPE")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray), _
                                            New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpID.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadSelectedCoverageType(ByVal grpId As Guid, ByVal languageid As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_SELECTED_COVERAGE_TYPE")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function
    Public Function GetCoverageTypeInUse(ByVal CovTypeIds As ArrayList, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CAUSE_OF_LOSS_IN_USE")

        Dim inClauseCondition As String
        inClauseCondition &= MiscUtil.BuildListForSql("cl." & "company_id", compIds, True)
        selectStmt = selectStmt.Replace("--dynamic_in_clause_1", inClauseCondition)
        inClauseCondition = ""
        inClauseCondition &= MiscUtil.BuildListForSql("and cl." & Me.COL_NAME_COVERAGE_TYPE_ID, CovTypeIds, True)
        selectStmt = selectStmt.Replace("--dynamic_in_clause_2", inClauseCondition)

        Try
            Dim ds As New DataSet
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
            'Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetUsedCompanyGroups() As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_USED_COMPANY_GROUP")

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
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


