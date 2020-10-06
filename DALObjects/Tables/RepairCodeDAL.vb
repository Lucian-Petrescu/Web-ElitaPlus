'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/15/2004)********************


Public Class RepairCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REPAIR_CODE"
    Public Const TABLE_KEY_NAME As String = "repair_code_id"

    Public Const COL_NAME_REPAIR_CODE_ID = "repair_code_id"
    'Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_COMPANY_GROUP = "company_group_id"
    Public Const COL_NAME_SHORT_DESC = "short_desc"
    Public Const COL_NAME_DESCRIPTION = "description"
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("repair_code_id", id.ToByteArray)}
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

    Public Function LoadList(description As String, code As String, companyGroupID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP, companyGroupID.ToByteArray), _
                                     New OracleParameter(COL_NAME_SHORT_DESC, code), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, description)}

        'Dim inClauseCondition As String = MiscUtil.BuildListForSql("AND c." & Me.COL_NAME_COMPANY_ID, companyIds, True)
        'selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        Try
            'New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _

            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub
#End Region

End Class



