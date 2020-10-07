'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/23/2007)********************


Public Class VSCCoverageLimitDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_COVERAGE_LIMIT"
    Public Const TABLE_KEY_NAME As String = "vsc_coverage_limit_id"

    Public Const COL_NAME_VSC_COVERAGE_LIMIT_ID As String = "vsc_coverage_limit_id"
    Public Const COL_NAME_COVERAGE_KM_MI As String = "coverage_km_mi"
    Public Const COL_NAME_COVERAGE_MONTHS As String = "coverage_months"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_COVERAGE_LIMIT_CODE As String = "coverage_limit_code"

    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_COVERAGE_OPT = "Cov_optional"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("vsc_coverage_limit_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(LimitCodeMask As String, CoverageTypeMask As Guid, MonthMask As String, KmMask As String, company_group_id As Guid, LanguageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        '  inCausecondition &= "AND " & MiscUtil.BuildListForSql(Me.COL_COMPANY_GROUP_ID, compIds, False)

        If Not CoverageTypeMask.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C." & COL_NAME_COVERAGE_TYPE_ID & ") ='" & GuidToSQLString(CoverageTypeMask) & "'"
        End If
        If ((Not (LimitCodeMask Is Nothing)) AndAlso (FormatSearchMask(LimitCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_COVERAGE_LIMIT_CODE & ")" & LimitCodeMask.ToUpper
        End If
        If ((Not (MonthMask Is Nothing)) AndAlso (FormatSearchMask(MonthMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_COVERAGE_MONTHS & ")" & MonthMask.ToUpper
        End If
        If ((Not (KmMask Is Nothing)) AndAlso (FormatSearchMask(KmMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_COVERAGE_KM_MI & ")" & KmMask.ToUpper
        End If


        If Not inCausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If



        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        parameters = New DBHelper.DBHelperParameter() _
                                              {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray), _
                                            New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray)}



        '  selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & Me.COL_NAME_POLICE_STATION_NAME & ", " & Me.COL_NAME_POLICE_STATION_CODE)
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Function GetOptionalCoverage(coveragelimitid As Guid, company_group_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_COVERAGE_OPT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray), _
                                                                                            New DBHelper.DBHelperParameter(COL_NAME_VSC_COVERAGE_LIMIT_ID, coveragelimitid.ToByteArray), _
                                                                                            New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray)}
        Try
            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


