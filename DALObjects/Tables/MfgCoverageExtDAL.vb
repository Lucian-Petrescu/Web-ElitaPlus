'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/24/2010)********************


Public Class MfgCoverageExtDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MFG_COVERAGE_EXT"
    Public Const TABLE_KEY_NAME As String = "mfg_coverage_ext_id"

    Public Const COL_NAME_MFG_COVERAGE_EXT_ID As String = "mfg_coverage_ext_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MFG_COVERAGE_ID As String = "mfg_coverage_id"
    Public Const COL_NAME_EXT_WARRANTY As String = "ext_warranty"

    Public Const COL_LST_DEALER_NAME As String = "dealer_name"

    Public Const PARAM_NAME_COMPANY_GROUP_ID As String = "company_group_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("mfg_coverage_ext_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal MfgCoverageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(COL_NAME_MFG_COVERAGE_ID, MfgCoverageId.ToByteArray)}

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME, parameters)
    End Function

    Public Function GetAvailableDealers(ByVal MfgCoverageId As Guid, ByVal CompanyGroupId As Guid, ByVal DealerId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_AVAILABLE_DEALERS")
        Dim parameters() As OracleParameter = New OracleParameter() { New OracleParameter(PARAM_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray), _
                                                                    New OracleParameter(COL_NAME_MFG_COVERAGE_ID, MfgCoverageId.ToByteArray)}

        If Not DealerId = Guid.Empty Then
            Dim strQry As String = String.Format(" OR d.dealer_id = HEXTORAW('{0}') ", GuidControl.GuidToHexString(DealerId))
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, strQry)
        End If

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME, parameters)
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


