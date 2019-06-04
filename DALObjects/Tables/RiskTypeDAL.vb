''/*-----------------------------------------------------------------------------------------------------------------

''  AA      SSS  SSS  UU  UU RRRRR      AA    NN   NN  TTTTTTTT
''A    A   SS    SS   UU  UU RR   RR  A    A  NNN  NN     TT 
''AAAAAA   SSS   SSS  UU  UU RRRR     AAAAAA  NN N NN     TT
''AA  AA     SS    SS UU  UU RR RR    AA  AA  NN  NNN     TT
''AA  AA  SSSSS SSSSS  UUUU  RR   RR  AA  AA  NN  NNN     TT

''Copyright 2004, Assurant Group Inc..  All Rights Reserved.
''------------------------------------------------------------------------------
''This information is CONFIDENTIAL and for Assurant Group's exclusive use ONLY.
''Any reproduction or use without Assurant Group's explicit, written consent 
''is PROHIBITED.
''------------------------------------------------------------------------------

''Purpose: Provides transactional methods for loading, inserting, updating and 
''         deleting a Risk Type.  Tablename: ELP_RISK_TYPE
''
''Author/s:  Ravi Chillikatil, Rosalba Monterrosas
''
''Date:    06/30/2004     

''MODIFICATION HISTORY:
'' 



''===========================================================================================

Public Class RiskTypeDAL
    Inherits DALBase
#Region "Constants"

    Public Const RISK_TYPE_TABLE_NAME As String = "RISK_TYPE"
    Public Const RISK_TYPE_LIST As String = "RISK_TYPE_LIST"
    Public Const RISK_GROUP_ID_COL As String = "risk_group_id"
    Public Const RISK_TYPE_ID_COL As String = "risk_type_id"
    Public Const DESCRIPTION_COL As String = "description"
    Public Const RISK_TYPE_ENGLISH_COL As String = "risk_type_english"
    Public Const LANGUAGE_ID_COL As String = "language_id"
    'Public Const COMPANY_ID_COL As String = "company_id"
    Public Const COMPANY_GROUP_ID_COL As String = "company_group_id"
    Public Const WILDCARD As Char = "%"
    Public Const DSNAME As String = "RiskTypeGrid"
    Public Const FIRST_POS As Integer = 0
    Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
    Public Const COL_NAME_PRODUCT_TAX_TYPE_ID As String = "product_tax_type_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.new()
    End Sub

#End Region
#Region "DALTestRelated Methods"
    Public Function GetLanguageId() As DataSet

        Dim selectStmt As String = Me.Config("SQL/LOAD_LANGUAGE_ID")

        Return (DBHelper.Fetch(selectStmt, "ELP_LANGUAGE"))

    End Function

    Public Function GetCompanyId() As DataSet

        Dim selectStmt As String = Me.Config("SQL/LOAD_COMPANY_ID")

        Return (DBHelper.Fetch(selectStmt, "ELP_COMPANY"))

    End Function

#End Region


#Region "Public Methods"

    Public Function Load(ByVal ds As DataSet, ByVal id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(RISK_TYPE_ID_COL, id.ToByteArray)}
        'ds = DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters)
        'If (ds.Tables(RISK_TYPE_TABLE_NAME).Rows.Count = 1) Then
        '    Return (ds)
        'Else
        '    Throw New Assurant.ElitaPlus.BusinessObjectData.DataNotFoundException("RiskType:Fetch")
        'End If
        Return (DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters))

    End Function

    Public Function LoadSchema(ByVal ds As DataSet) As DataSet

        Return (Me.Load(ds, Guid.Empty))

    End Function

    Public Function GetRiskTypeListTest(ByVal description As String, ByVal riskTypeEnglish As String, _
                                        ByVal riskGroupId As Guid, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        If (riskGroupId.Equals(Guid.Empty)) Then
            parameters = New OracleParameter() {New OracleParameter(DESCRIPTION_COL, description)}
        Else
            parameters = New OracleParameter() {New OracleParameter(DESCRIPTION_COL, description), _
                                                New OracleParameter(RISK_TYPE_ENGLISH_COL, riskTypeEnglish), _
                                                New OracleParameter(RISK_GROUP_ID_COL, riskGroupId.ToByteArray), _
                                                New OracleParameter(LANGUAGE_ID_COL, languageId.ToByteArray), _
                                                New OracleParameter(LANGUAGE_ID_COL, languageId.ToByteArray)}
        End If

        Return DBHelper.Fetch(selectStmt, DSNAME, Me.RISK_TYPE_LIST, parameters)

    End Function

    Public Function GetRiskTypeList(ByVal description As String, ByVal riskTypeEnglish As String, _
                                    ByVal riskGroupId As Guid, ByVal languageId As Guid, _
                                    ByVal CompanyGroupId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC_SQL")

        If (Not (description.Equals(String.Empty))) AndAlso (Me.FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND upper(rt.description) " & description.ToUpper
        End If

        If (Not (riskTypeEnglish.Equals(String.Empty))) AndAlso (Me.FormatSearchMask(riskTypeEnglish)) Then
            selectStmt &= Environment.NewLine & "AND upper(rt.risk_type_english) " & riskTypeEnglish.ToUpper
        End If

        If Not riskGroupId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND rt.risk_group_id = '" & Me.GuidToSQLString(riskGroupId) & "'"
        End If

        If Not languageId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND trans_ptt.language_id = '" & Me.GuidToSQLString(languageId) & "'"
            selectStmt &= Environment.NewLine & "AND trans_rg.language_id = '" & Me.GuidToSQLString(languageId) & "'"
        End If

        If Not CompanyGroupId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND Company_Group_Id = '" & Me.GuidToSQLString(CompanyGroupId) & "'"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(rt.description)"

        Return (DBHelper.Fetch(selectStmt, Me.RISK_TYPE_LIST))

    End Function

    'Public Overloads Sub Update(ByVal ds As DataSet)
    '    Dim con As New OracleConnection(DBHelper.ConnectString)
    '    Dim da As OracleDataAdapter

    '    Try
    '        con.Open()

    '        'RiskType Table Update
    '        da = Me.ConfigureDataAdapter(con)
    '        da.Update(ds, RISK_TYPE_TABLE_NAME)

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If con.State = ConnectionState.Open Then con.Close()
    '    End Try

    'End Sub

    Public Sub LoadRiskTypeForSoftQuestion(ByVal ds As DataSet, ByVal id As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOADSOFTQUESTIONLIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, id)}
        DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters)
        'Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, id.ToByteArray())}
        'DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters)

    End Sub

    Public Sub LoadAvailableRiskTypeForSoftQuestion(ByVal ds As DataSet, ByVal companyGroupId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/LOADAVAILABLESOFTQUESTIONLIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COMPANY_GROUP_ID_COL, companyGroupId)}
        DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters)
        'Dim parameters() As OracleParameter = Nothing
        'DBHelper.Fetch(ds, selectStmt, Me.RISK_TYPE_TABLE_NAME, parameters)

    End Sub

    Public Function Is_TaxByProductType_Yes(ByVal countryIds As ArrayList) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/TAX_BY_PRODUCT_TYPE__YES__NUM")
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet

        whereClauseConditions &= Environment.NewLine & " AND " & _
                                MiscUtil.BuildListForSql(Me.COL_NAME_COUNTRY_ID, countryIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            ds = DBHelper.Fetch(selectStmt, Me.RISK_TYPE_LIST)
            If (CInt(ds.Tables(Me.RISK_TYPE_LIST).Rows(0).Item(0)) > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

      
    End Function

#End Region


#Region "Private Methods"

#Region "DataAdapter Configuration"

    Protected Function ConfigureDataAdapter(ByVal con As OracleConnection) As OracleDataAdapter
        'create the data adapter and its commands
        Dim da As New OracleDataAdapter
        Dim insertStmt As String = Me.Config("/SQL/INSERT")
        Dim updateStmt As String = Me.Config("/SQL/UPDATE")
        Dim deleteStmt As String = Me.Config("/SQL/DELETE")

        'associate commands to data adapter

        'insert
        da.InsertCommand = New OracleCommand(insertStmt, con)
        Me.AssignCommonParameters(da.InsertCommand)
        Me.AddInsertAuditParameters(da.InsertCommand)
        da.InsertCommand.Parameters.Add(RISK_TYPE_ID_COL, OracleDbType.Raw, 16, "RISK_TYPE_ID")

        'update
        da.UpdateCommand = New OracleCommand(updateStmt, con)
        Me.AssignCommonParameters(da.UpdateCommand)
        Me.AddUpdateAuditParameters(da.UpdateCommand)
        Me.AddWhereParametersForUpdate(da.UpdateCommand)

        'delete
        da.DeleteCommand = New OracleCommand(deleteStmt, con)
        Me.AddWhereParametersForUpdate(da.DeleteCommand)

        Return (da)

    End Function

#End Region

#Region "Parameter Assignment"

    Protected Sub AssignCommonParameters(ByVal cmd As OracleCommand)

        cmd.Parameters.Add(COMPANY_GROUP_ID_COL, OracleDbType.Raw, 16, "COMPANY_GROUP_ID")
        cmd.Parameters.Add(DESCRIPTION_COL, OracleDbType.Varchar2, 60, "DESCRIPTION")
        cmd.Parameters.Add(RISK_TYPE_ENGLISH_COL, OracleDbType.Varchar2, 60, "RISK_TYPE_ENGLISH")
        cmd.Parameters.Add(RISK_GROUP_ID_COL, OracleDbType.Raw, 16, "RISK_GROUP_ID")
        cmd.Parameters.Add(COL_NAME_SOFT_QUESTION_GROUP_ID, OracleDbType.Raw, 16, "SOFT_QUESTION_GROUP_ID")

    End Sub

    Protected Sub AddUpdateAuditParameters(ByVal cmd As OracleCommand)
        cmd.Parameters.Add("modified_by", OracleDbType.Varchar2, 30, "modified_by")
    End Sub

    Protected Sub AddInsertAuditParameters(ByVal cmd As OracleCommand)
        cmd.Parameters.Add("created_by", OracleDbType.Varchar2, 30, "created_by")
    End Sub

    Protected Sub AddWhereParametersForUpdate(ByVal cmd As OracleCommand)
        cmd.Parameters.Add(RISK_TYPE_ID_COL, OracleDbType.Raw, 16, "RISK_TYPE_ID")
    End Sub

#End Region

#End Region

#Region "OVERLOADED METHODS"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.RISK_TYPE_TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.RISK_TYPE_TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

#End Region


End Class

