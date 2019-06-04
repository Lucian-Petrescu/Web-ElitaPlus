'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/28/2006)********************


Public Class MfgStandardizationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MFG_STANDARDIZATION"
    Public Const TABLE_KEY_NAME As String = "MFG_standardization_id"

    Public Const COL_NAME_MFG_STANDARDIZATION_ID As String = "MFG_standardization_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_GROUP_NAME = "company_group_name"
    Public Const COL_NAME_MFG As String = "MFG_description"
    Public Const COL_NAME_MFG_ID As String = "Manufacturer_id"

    Public Const WILDCARD As Char = "%"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Mfg_standardization_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function
    'Public Function LoadList(ByVal description As String, ByVal dealerId As Guid, _
    '                         ByVal manufacturerId As Guid, ByVal CompanyGroupId As Guid) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Dim parameters() As OracleParameter

    '    description = GetFormattedSearchStringForSQL(description)

    '   If (manufacturerId.Equals(Guid.Empty)) Then
    '        parameters = New OracleParameter() _
    '                                        {New OracleParameter(COL_NAME_MFG_ID, WILDCARD), _
    '                                         New OracleParameter(COL_NAME_DESCRIPTION, description), _
    '                                         New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
    '    End If

    '    Try
    '        Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

    Public Function GetMfgAliasList(ByVal description As String, _
                                    ByVal MfgId As Guid, _
                                    ByVal company_groupId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet

        'description = GetFormattedSearchStringForSQL(description)

        If (Not (description.Equals(String.Empty))) AndAlso (Me.FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND mfgstand.DESCRIPTION " & description.ToUpper
        End If

        If Not MfgId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND mfgstand.Manufacturer_id = '" & Me.GuidToSQLString(MfgId) & "'"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(mfgstand.DESCRIPTION)"

        'Return (DBHelper.Fetch(selectStmt, Me.RISK_TYPE_LIST))
        Dim param As New DBHelper.DBHelperParameter("company_group_id", Me.GuidToSQLString(company_groupId))

        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
                        New DBHelper.DBHelperParameter() {param})
        Return ds

    End Function

    Public Function GetMfgAliasList(ByVal description As String, ByVal MfgId As Guid, ByVal userCountries As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_USER_COUNTRIES")
        Dim inClausecondition As String = ""
        inClausecondition &= "mfgstand." & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_GROUP_ID, userCountries, False)

        If (Not (description.Equals(String.Empty))) AndAlso (Me.FormatSearchMask(description)) Then
            selectStmt &= Environment.NewLine & "AND mfgstand.DESCRIPTION " & description.ToUpper
        End If

        If Not MfgId.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & "AND mfgstand.Manufacturer_id = '" & Me.GuidToSQLString(MfgId) & "'"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY UPPER(c.description), UPPER(mfgstand.description)"

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class
