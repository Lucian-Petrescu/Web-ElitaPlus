'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/22/2010)********************


Public Class AcctCovEntityDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_COV_ENTITY"
    Public Const TABLE_KEY_NAME As String = "acct_cov_entity_id"

    Public Const COL_NAME_ACCT_COV_ENTITY_ID As String = "acct_cov_entity_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_BUSINESS_ENTITY_ID As String = "business_entity_id"
    Public Const COL_NAME_ACCT_BUSINESS_UNIT_ID As String = "acct_business_unit_id"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_ACCT_COVERAGE_TYPE_CODE As String = "ACCT_COV_TYPE_CODE"

    'For List Search
    Public Const PARAM_NAME_LANGUAGE_ID = "ditBE.language_Id"
    Public Const PARAM_NAME_USER_ID = "user_id"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_cov_entity_id", id.ToByteArray)}
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

    Public Function LoadList(AcctcompanyMask As Guid, RegionIdMask As Guid, CoverageTypeIdMask As Guid, BusinessUnitIdMask As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False
        Dim strAcctcompanyMask, strRegionIdMask, strCoverageTypeIdMask, strBusinessUnitIdMask, languageIdMask As String

        If (Not (AcctcompanyMask.Equals(Guid.Empty))) Then
            strAcctcompanyMask = GuidToSQLString(AcctcompanyMask)
            FormatSearchMask(strAcctcompanyMask)
            whereClauseConditions &= " AND " & AcctCompanyDAL.TABLE_NAME & "." & AcctCompanyDAL.COL_NAME_ACCT_COMPANY_ID & strAcctcompanyMask.ToUpper
        Else
            Return Nothing
        End If

        If (Not (RegionIdMask.Equals(Guid.Empty))) Then
            strRegionIdMask = GuidToSQLString(RegionIdMask)
        End If
        If ((Not (strRegionIdMask Is Nothing)) AndAlso (FormatSearchMask(strRegionIdMask))) Then
            whereClauseConditions &= " AND " & COL_NAME_REGION_ID & strRegionIdMask.ToUpper
        End If

        If (Not (CoverageTypeIdMask.Equals(Guid.Empty))) Then
            strCoverageTypeIdMask = GuidToSQLString(CoverageTypeIdMask)
        End If
        If ((Not (strCoverageTypeIdMask Is Nothing)) AndAlso (FormatSearchMask(strCoverageTypeIdMask))) Then
            whereClauseConditions &= " AND " & COL_NAME_COVERAGE_TYPE_ID & strCoverageTypeIdMask.ToUpper
        End If

        If (Not (BusinessUnitIdMask.Equals(Guid.Empty))) Then
            strBusinessUnitIdMask = GuidToSQLString(BusinessUnitIdMask)
        End If
        If ((Not (strBusinessUnitIdMask Is Nothing)) AndAlso (FormatSearchMask(strBusinessUnitIdMask))) Then
            whereClauseConditions &= " AND  " & COL_NAME_ACCT_BUSINESS_UNIT_ID & strBusinessUnitIdMask.ToUpper
        End If

        If (Not (languageId.Equals(Guid.Empty))) Then
            languageIdMask = GuidToSQLString(languageId)
        End If
        If ((Not (languageIdMask Is Nothing)) AndAlso (FormatSearchMask(languageIdMask))) Then
            whereClauseConditions &= " AND  " & PARAM_NAME_LANGUAGE_ID & languageIdMask.ToUpper
        End If

        If (whereClauseConditions <> "") Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


