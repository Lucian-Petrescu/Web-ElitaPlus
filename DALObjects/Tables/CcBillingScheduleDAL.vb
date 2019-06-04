'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/27/2011)********************


Public Class CcBillingScheduleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CC_BILLING_SCHEDULE"
    Public Const TABLE_KEY_NAME As String = "cc_billing_schedule_id"

    Public Const COL_NAME_CC_BILLING_SCHEDULE_ID As String = "cc_billing_schedule_id"
    Public Const COL_NAME_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"
    Public Const COL_NAME_BILLING_DATE As String = "billing_date"
    Public Const GET_BILLING_YEARS_TOTAL_PARAM = 0
    Public Const GET_CLOSING_DATE_TOTAL_PARAM = 1
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const FIRST_ROW = 0
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    'Public Const COL_NAME_BILLING_DATE As String = "billed_date"
    Public Const BILLING_YEAR As String = "BILLING_YEAR"
    Public Const PARAM_COMPANY_CREDIT_CARD_ID = 0
    Public Const USER_ID = 0
    Public Const FOR_THIS_DATE = 1

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cc_billing_schedule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal companyCreditCard_Id As Guid, ByVal familyDataset As DataSet) As DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray), _
                                            New OracleParameter(Me.COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)

    End Function
    Public Function GetBillingYears(ByVal companyCreditCard_Id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_THE_BILLING_YEARS")
        Dim parameters(GET_BILLING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetBillingYearsByUser(ByVal userId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_THE_BILLING_YEARS_BY_USER")
        Dim parameters(GET_BILLING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter

        parameters(USER_ID) = New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId.ToByteArray)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCCSchedulingBillDates(ByVal companyCreditCard_Id As Guid, ByVal forThisYear As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_BILLING_DATES")
        Dim parameters(GET_CLOSING_DATE_TOTAL_PARAM) As DBHelper.DBHelperParameter
        'Pedro

        parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)
        parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(BILLING_YEAR, forThisYear)

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLastBillingDate(ByVal companyCreditCard_Id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_LAST_BILLING_DATES")
        Dim parameters(GET_BILLING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter

      parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCCSchedulingBillDate(ByVal companyCreditCard_Id As Guid, ByVal forThisDate As Date) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/GET_BILLING_DATE")
        Dim parameters(GET_CLOSING_DATE_TOTAL_PARAM) As DBHelper.DBHelperParameter

        parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)
        parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(COL_NAME_BILLING_DATE, forThisDate)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)


            If ds.Tables(Me.TABLE_NAME).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds.Tables(Me.TABLE_NAME).Rows(FIRST_ROW)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function GetMinCCSchedulingBillDate(ByVal companyCreditCard_Id As Guid) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/GET_MIN_BILLING_DATE")
        Dim parameters(GET_BILLING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)
        ' parameters(FOR_THIS_DATE) = New DBHelper.DBHelperParameter(COL_NAME_BILLING_DATE, forThisDate)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)


            If ds.Tables(Me.TABLE_NAME).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds.Tables(Me.TABLE_NAME).Rows(FIRST_ROW)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetAllCCSchedulingBillDates(ByVal companyCreditCard_Id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ALL_BILLING_DATES")
        Dim parameters(GET_BILLING_YEARS_TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(PARAM_COMPANY_CREDIT_CARD_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, companyCreditCard_Id.ToByteArray)

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
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


