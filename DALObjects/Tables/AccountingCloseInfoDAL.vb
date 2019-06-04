'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/9/2005)********************


Public Class AccountingCloseInfoDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCOUNTING_CLOSE_INFO"
    Public Const TABLE_CLOSING_DATE_NAME As String = "ELP_ACCOUNTING_CLOSE_DATE_INFO"
    Public Const TABLE_KEY_NAME As String = "accounting_close_info_id"

    Public Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID As String = "accounting_close_info_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_CLOSING_DATE As String = "closing_date"
    Public Const CLOSING_YEAR As String = "closing_year"
    Public Const COMPANY_ID = 0
    Public Const USER_ID = 0
    Public Const FOR_THIS_DATE = 1
    Public Const GET_CLOSING_YEARS_TOTAL_PARAM = 0
    Public Const GET_CLOSING_DATE_TOTAL_PARAM = 1
    Public Const FIRST_ROW = 0
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("accounting_close_info_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal cmpId As Guid, ByVal familyDataset As DataSet) As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMPANY_ID, cmpId.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_COMPANY_ID, cmpId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)

    End Function


    Public Function GetClosingYears(ByVal companyId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_THE_CLOSING_YEARS")
        Dim parameters(GET_CLOSING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClosingYearsByUser(ByVal userId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_THE_CLOSING_YEARS_BY_USER")
        Dim parameters(GET_CLOSING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter

        parameters(USER_ID) = New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId.ToByteArray)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAccountingCloseDates(ByVal companyId As Guid, ByVal forThisYear As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CLOSING_DATES")
        Dim parameters(GET_CLOSING_DATE_TOTAL_PARAM) As DBHelper.DBHelperParameter
        'Pedro

        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)
        parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(CLOSING_YEAR, forThisYear)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLastClosingDate(ByVal companyId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_LAST_CLOSING_DATES")
        Dim parameters(GET_CLOSING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter

        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAccountingCloseDate(ByVal companyId As Guid, ByVal forThisDate As Date) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/GET_CLOSING_DATE")
        Dim parameters(GET_CLOSING_DATE_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)
        parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(COL_NAME_CLOSING_DATE, forThisDate)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLOSING_DATE_NAME, parameters)


            If ds.Tables(TABLE_CLOSING_DATE_NAME).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds.Tables(TABLE_CLOSING_DATE_NAME).Rows(FIRST_ROW)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function GetMinAccountingCloseDate(ByVal companyId As Guid) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/GET_MIN_CLOSING_DATE")
        Dim parameters(GET_CLOSING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)
        ' parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(COL_NAME_CLOSING_DATE, forThisDate)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLOSING_DATE_NAME, parameters)


            If ds.Tables(TABLE_CLOSING_DATE_NAME).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds.Tables(TABLE_CLOSING_DATE_NAME).Rows(FIRST_ROW)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetAllAccountingCloseDates(ByVal companyId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_CLOSING_DATES")
        Dim parameters(GET_CLOSING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds Is Nothing Then
            'If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


