'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/30/2004)********************


Public Class DealerGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALER_GROUP"
    Public Const TABLE_KEY_NAME As String = "dealer_group_id"

    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_ACCTING_BY_GROUP_ID As String = "ACCTING_BY_GROUP_ID"
    Public Const COL_NAME_USE_CLIENT_CODE_YNID As String = "use_client_code_ynid"
    Public Const COL_NAME_USE_CLIENT_CODE_YNDESC As String = "use_client_code_yndesc"
    Public Const COL_NAME_AUTO_REJ_ERR_TYPE_ID As String = "auto_rej_err_type_id"
    Public Const COL_NAME_AUTO_REJ_ERR_TYPE_DESC As String = "auto_rej_err_type_desc"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_ACCTING_BY_GROUP_DESC As String = "ACCTING_BY_GROUP_Desc"
    Public Const COL_NAME_USE_CLIENT_CODE_YN As String = "use_client_code_yn"
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"""

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("dealer_group_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
    End Function

    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function

    'Public Function LoadList(ByVal description As String, ByVal code As String, ByVal companyId As Guid) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Dim parameters() As OracleParameter
    '    description = GetFormattedSearchStringForSQL(description)
    '    code = GetFormattedSearchStringForSQL(code)
    '    parameters = New OracleParameter() _
    '                                {New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _
    '                                 New OracleParameter(COL_NAME_CODE, code), _
    '                                 New OracleParameter(COL_NAME_DESCRIPTION, description)}
    '    Try
    '        Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

    Public Function LoadList(descriptionMask As String, codeMask As String, compGroupId As Guid, languageId As Guid) As DataSet

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
        descriptionMask = GetFormattedSearchStringForSQL(descriptionMask)
        codeMask = GetFormattedSearchStringForSQL(codeMask)
        parameters = New OracleParameter() _
                                    {New OracleParameter("language_id", languageId.ToByteArray), _
                                     New OracleParameter("language_id", languageId.ToByteArray), _
                                     New OracleParameter("language_id", languageId.ToByteArray), _
                                     New OracleParameter(COL_NAME_CODE, codeMask), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, descriptionMask), _
                                     New OracleParameter(COL_NAME_COMPANY_GROUP_ID, compGroupId.ToByteArray)}

        'selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function CheckAllDealerObligor(DealerGrpId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CHECK_ALL_DEALER_OBLIGOR")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_GROUP_ID, DealerGrpId.ToByteArray)}


        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetUseClientDealerCodeYN(DealerGrpId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_CLIENT_DEALER_CODE_YN")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_GROUP_ID, DealerGrpId.ToByteArray)}


        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

            Return ds
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

    'Public Overloads Sub Update(ByVal ds As DataSet)
    '    Dim conn As OracleConnection
    '    Dim transaction As OracleTransaction
    '    Try
    '        conn = New OracleConnection(DBHelper.ConnectString)
    '        conn.Open()
    '        transaction = conn.BeginTransaction
    '        Update(ds, transaction)
    '        transaction.Commit()
    '        LookupListCache.ClearFromCache(Me.GetType.ToString)
    '    Catch ex As Exception
    '        transaction.Rollback()
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    Finally
    '        If conn.State = ConnectionState.Open Then conn.Close()
    '    End Try
    'End Sub

    'Public Overloads Sub Update(ByVal ds As DataSet, ByVal transaction As OracleTransaction)
    '    Dim da As OracleDataAdapter = configureDataAdapter(ds, transaction)
    '    da.Update(ds.Tables(TABLE_NAME))
    'End Sub

    'Protected Function configureDataAdapter(ByVal ds As DataSet, ByVal transaction As OracleTransaction) As OracleDataAdapter
    '    Dim da As New OracleDataAdapter
    '    'associate commands to data adapter

    '    da.UpdateCommand = New OracleCommand(Config("/SQL/UPDATE"), transaction.Connection)
    '    AddCommonParameters(da.UpdateCommand)
    '    AddUpdateAuditParameters(da.UpdateCommand)
    '    AddWhereParameters(da.UpdateCommand)

    '    da.InsertCommand = New OracleCommand(Config("/SQL/INSERT"), transaction.Connection)
    '    AddCommonParameters(da.InsertCommand)
    '    AddInsertAuditParameters(da.InsertCommand)
    '    da.InsertCommand.Parameters.Add("dealer_group_id", OracleDbType.Raw, 16, "dealer_group_id")

    '    da.DeleteCommand = New OracleCommand(Config("/SQL/DELETE"), transaction.Connection)
    '    AddWhereParameters(da.DeleteCommand)
    '    Return (da)
    'End Function

    'Protected Sub AddCommonParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("description", OracleDbType.Varchar2, 255, "description")
    '    cmd.Parameters.Add("code", OracleDbType.Varchar2, 5, "code")
    '    'cmd.Parameters.Add("company_id", OracleDbType.Raw, 16, "company_id")
    '    'cmd.Parameters.Add("company_group_id", OracleDbType.Raw, 16, "company_group_id")
    'End Sub

    'Protected Sub AddWhereParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("dealer_group_id", OracleDbType.Raw, 16, "dealer_group_id")
    'End Sub

    'Protected Sub AddUpdateAuditParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("modified_by", OracleDbType.Varchar2, 30, "modified_by")
    'End Sub

    'Protected Sub AddInsertAuditParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("created_by", OracleDbType.Varchar2, 30, "created_by")
    'End Sub

#End Region

End Class


