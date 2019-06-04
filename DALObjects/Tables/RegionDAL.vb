'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/19/2004)********************


Public Class RegionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REGION"
    Public Const TABLE_KEY_NAME As String = "region_id"

    Public Const COL_NAME_REGION_ID = "region_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_COUNTRY_ID = "country_id"
    Public Const COL_NAME_COUNTRY_NAME = "country_name"
    Public Const COL_NAME_SHORT_DESC = "short_desc"
    Public Const COL_NAME_ACCOUNTING_CODE = "accounting_code"
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_INVOICE_TAX_GL = "invoicetax_gl_acct"

    Private Const DSNAME As String = "LIST"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("region_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal countryID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_COUNTRY_ID, countryID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetRegionsAndComunas(ByVal countryID As Guid, Optional regionCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_REGIONS_AND_COMUNAS")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_COUNTRY_ID, countryID.ToByteArray)}

        Dim whereClauseConditions As String = ""

        If Not regionCode = "" Then
            whereClauseConditions = " and upper(r.short_desc) = '" & regionCode.ToUpper & "'"
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal description As String, ByVal code As String, ByVal countryID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COUNTRY_ID, countryID.ToByteArray), _
                                     New OracleParameter(COL_NAME_SHORT_DESC, code), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, description)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadList(ByVal description As String, ByVal code As String, ByVal userCountries As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_USER_COUNTRIES")
        Dim inClausecondition As String = ""
        inClausecondition &= "And r." & MiscUtil.BuildListForSql(Me.COL_NAME_COUNTRY_ID, userCountries, False)

        Dim parameters() As OracleParameter
        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DESCRIPTION, description), _
                                     New OracleParameter(COL_NAME_SHORT_DESC, code)}

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListForWS(ByVal oCountriesIds As ArrayList) As DataSet
        If oCountriesIds.Count = 0 Then Return Nothing
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_WS")

        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql(Me.COL_NAME_COUNTRY_ID, oCountriesIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.ExecuteWithParam(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

    Public Function LoadRegionsByUserCountries(ByVal CountryId As Guid) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/REGION_BY_COUNTRY")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COUNTRY_ID, CountryId.ToByteArray)}
        Try
            
                ds = DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters)
                AddSequenceColumn(ds.Tables(TABLE_NAME))
                Return ds
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try

    End Function

    Public Function IsRegionCodeUnique(ByVal CountryId As Guid, ByVal ShortDesc As String, ByVal RegionId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IS_REGION_CODE_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Country_Id", CountryId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("Short_Desc", ShortDesc), _
                                                                                           New DBHelper.DBHelperParameter("region_Id", RegionId.ToByteArray)}
        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count >= 1 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsRegionDescriptionUnique(ByVal CountryId As Guid, ByVal Description As String, ByVal RegionId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IS_REGION_DESCRIPTION_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Country_Id", CountryId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("Description", Description), _
                                                                                            New DBHelper.DBHelperParameter("region_Id", RegionId.ToByteArray)}
        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count >= 1 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class


