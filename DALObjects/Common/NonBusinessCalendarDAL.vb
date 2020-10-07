'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/28/2007)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class NonBusinessCalendarDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_NONBUSINESS_CALENDAR"
    Public Const TABLE_KEY_NAME As String = "nonbusiness_calendar_id"

    Public Const COL_NAME_NONBUSINESS_CALENDAR_ID As String = "nonbusiness_calendar_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_NONBUSINESS_DATE As String = "nonbusiness_date"

    Public Const P_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const P_COL_NAME_DEFAULT_FOLLOWUP As String = "default_followup"
    Public Const P_COL_NAME_NEXT_BUSINESS_DATE As String = "p_NextBusinessDate"

    Public Const COL_NAME_NONBUSINESS_DAY_COUNT As String = "NonBusiness_Day_Count"
    Public Const COL_NAME_SAMEBUSINESS_DAY_COUNT As String = "SameBusiness_Date_Count"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("nonbusiness_calendar_id", id.ToByteArray)}
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

    Public Function LoadList(companyGroupID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, GuidToSQLString(companyGroupID))}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetNonBusinessDaysCount(defaultFollowUp As Integer, companyGroupID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GetNonBusinessDaysCount")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupID.ToByteArray), _
                                            New OracleParameter("default_followup", defaultFollowUp)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Function GetSameBusinessDaysCount(followupDate As Date, companyGroupID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GetSameBusinessDaysCount")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupID.ToByteArray), _
                                            New OracleParameter(COL_NAME_NONBUSINESS_DATE, followupDate)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Function GetNextBusinessDate(defaultFollowUp As Integer, companyGroupID As Guid) As Date
        Dim ds As New DataSet
        Dim nextBusinessDate As DateType
        Dim selectStmt As String = Config("/SQL/GET_NEXT_BUSINESS_DATE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(P_COMPANY_GROUP_ID, companyGroupID.ToByteArray) _
        , New DBHelper.DBHelperParameter(P_COL_NAME_DEFAULT_FOLLOWUP, defaultFollowUp)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            nextBusinessDate = New DateType(Convert.ToDateTime(ds.Tables(0).Rows(0)(0)))

            Return nextBusinessDate
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

    Public Function GetNonBusinessDates(companyGroupCode As String, dtStart As Date, dtEnd As Date) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GetNonBusinessDateByCompanyGroupCode")

        parameters = New OracleParameter() {New OracleParameter("company_group_code", companyGroupCode), _
                                            New OracleParameter("start_date", dtStart), _
                                            New OracleParameter("end_date", dtEnd)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


