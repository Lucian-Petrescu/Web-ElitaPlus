Public NotInheritable Class LookupListDALNew
    Inherits DALBase

#Region " Constants "

    Private Const SQL_NAME_PREFIX As String = "/SQL/LOOKUP_LIST/"
    Private Const COL_DESCRIPTION_NAME As String = "DESCRIPTION"
    Public Const V_RPTNAME As String = "V_RPTNAME"
    Public Const V_COMPANYID As String = "V_COMPANYID"
    Public Const V_PAGECTRL As String = "V_PAGECTRL"
    Public Const DYNAMIC_AND_CLAUSE_PLACE_HOLDER As String = "--dynamic_and_clause"

#End Region

#Region " Constructors "


    Private Sub New()
        'don't want anyone to create an instance, use only static methods
    End Sub


#End Region

#Region " Public Methods "
    ''' <summary>
    ''' This Load is used to populate a dataset based on an entity id, a table name and a column name
    ''' </summary>
    ''' <param name="listName">Node in the XML file</param>
    ''' <param name="id">entity id</param>
    ''' <param name="tableName">table name</param>
    ''' <param name="column">column name</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Load(listName As String, id As Guid, tableName As String, column As String) As DataView
        Dim ds As New DataSet
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName))
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(column, id.ToByteArray)}
        Try
            ds = DBHelper.Fetch(ds, selectStmt, tableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return New DataView(ds.Tables(tableName))
    End Function

    Public Shared Function Load(listName As String) As DataView
        Dim ds As DataSet

        ds = DBHelper.Fetch(ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName)), listName)
        AddSequenceColumn(ds.Tables(listName))

        Return New DataView(ds.Tables(listName))

    End Function



    Public Shared Function Load(listName As String, dataSetName As String, tableName As String) As DataSet
        Dim ds As DataSet

        ds = DBHelper.Fetch(ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName)), dataSetName, tableName)

        Return ds

    End Function

    Public Shared Function Load(listName As String, params() As DBHelper.DBHelperParameter) As DataView
        Dim ds As New DataSet
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName))

        DBHelper.Fetch(ds, selectStmt, listName, params)
        AddSequenceColumn(ds.Tables(listName))
        Return New DataView(ds.Tables(listName))
    End Function

    Public Shared Function Load(listName As String, params() As DBHelper.DBHelperParameter, dynamicClause As String, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim ds As New DataSet
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName))
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, dynamicClause)
        If Not orderByColumn = "" Then selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "Order By " & orderByColumn)

        DBHelper.Fetch(ds, selectStmt, listName, params)
        AddSequenceColumn(ds.Tables(listName))
        Return New DataView(ds.Tables(listName))
    End Function

    Public Shared Function Load(listName As String, dynamicInClause As String) As DataView
        Dim ds As New DataSet
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName(listName))

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, dynamicInClause)
        ds = DBHelper.Fetch(selectStmt, listName)
        AddSequenceColumn(ds.Tables(listName))
        Return New DataView(ds.Tables(listName))
    End Function
    Public Shared Function GET_RPT_RUNDATE_PAGENUM(RptName As String, CompanyId As Guid) As DataSet
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName("REPORT_RUNDATE_PAGENUM"))

        Dim parameters(1) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        parameters(0) = New DBHelper.DBHelperParameter(V_RPTNAME, RptName)
        parameters(1) = New DBHelper.DBHelperParameter(V_COMPANYID, CompanyId.ToByteArray)
        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, "PAGECTRL")
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function LookuplistDealerByAttribute(CompanyId As String, tablename As String, UIProgCode As String)

        Dim andclause As String = ""
        Dim selectStmt As String = ConfigReader.GetNodeValue(GetType(LookupListDALNew), DetermineSQLName("ATTRIBUTE_VALUE"))

        andclause = Environment.NewLine & " AND " & CompanyId
        selectStmt = selectStmt.Replace(DYNAMIC_AND_CLAUSE_PLACE_HOLDER, andclause)
        Dim ds As New DataSet
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("TABLE_NAME", tablename) _
                       , New DBHelper.DBHelperParameter("UI_PROG_CODE", UIProgCode)}
        Try
            DBHelper.Fetch(ds, selectStmt, tablename, params)
            Return New DataView(ds.Tables(0))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region " Private Methods "


    Private Shared Function DetermineSQLName(listName As String) As String
        Dim sqlName As String

        'check if prefix already is set
        If (listName.IndexOf(SQL_NAME_PREFIX) = -1) Then
            sqlName = SQL_NAME_PREFIX & listName
        Else
            sqlName = listName
        End If

        Return sqlName

    End Function


#End Region


End Class
