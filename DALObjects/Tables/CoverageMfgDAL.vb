Public Class CoverageMfgDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MFG_COVERAGE"
    Public Const TABLE_KEY_NAME As String = "mfg_coverage_id"

    Public Const COL_NAME_DEDUCT_BY_MFG_ID As String = "mfg_coverage_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_MFG_WARRANTY As String = "mfg_warranty"
    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Public Const COL_NAME_DEALER_NAME = "dealer_name"
    Public Const COL_DEDUCTIBLE = "Deductible"


    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("mfg_coverage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compGroupId As Guid, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim inCausecondition As String = ""
        'Dim bIsLikeClause As Boolean = False

        'bIsLikeClause = IsThereALikeClause(descriptionMask, codeMask)

        'If bIsLikeClause = True Then
        '    ' hextoraw
        '    inCausecondition &= MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_GROUP_ID, compGroupIds, True)
        'Else
        '    ' not HextoRaw
        '    inCausecondition &= MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_GROUP_ID, compGroupIds, False)
        'End If
        ' descriptionMask = GetFormattedSearchStringForSQL(descriptionMask)
        ' codeMask = GetFormattedSearchStringForSQL(codeMask)
        parameters = New OracleParameter() _
                                    {New OracleParameter("language_id", languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_COMPANY_GROUP_ID, compGroupId.ToByteArray)}

        'selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
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
