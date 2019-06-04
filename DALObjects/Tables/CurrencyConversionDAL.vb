'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/31/2006)********************


Public Class CurrencyConversionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CURRENCY_CONVERSION"
    Public Const TABLE_KEY_NAME As String = "currency_conversion_id"

    Public Const COL_NAME_CURRENCY_CONVERSION_ID = "currency_conversion_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CURRENCY1_ID As String = "currency1_id"
    Public Const COL_NAME_CURRENCY2_ID As String = "currency2_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_CURRENCY1_RATE As String = "currency1_rate"
    Public Const COL_NAME_CURRENCY2_RATE As String = "currency2_rate"
    Public Const FILD_NAME_EXPIRATION_DATE As String = "Expiration_Date"

    Public Const RETURN_CODE As String = "return_code"
    Public Const RETURN_MESSAGE As String = "return_message"

    Public Const TOTAL_PARAM_IN As Integer = 6 '2
    Public Const TOTAL_PARAM_OUT As Integer = 1 '1
    Public Const DELETE_PARAM_IN As Integer = 4

    Public Const PARAM_IN_DEALER_ID As Integer = 0      'p-dealer_id in raw
    Public Const PARAM_IN_FROM_DATE As Integer = 1      ' p_from_date in date,
    Public Const PARAM_IN_TO_DATE As Integer = 2        'p_to_date in date, 
    Public Const PARAM_IN_CURRENCY1_ID As Integer = 3   ' p_currency1_id in raw
    Public Const PARAM_IN_CURRENCY2_ID As Integer = 4   ' p_currency2_id in raw
    Public Const PARAM_IN_CURRENCY1_RATE As Integer = 5 ' p_currency1_rate in number	
    Public Const PARAM_IN_CURRENCY2_RATE As Integer = 6 ' p_currency2_rate in number

    Public Const PARAM_OUT_RETURN_CODE As Integer = 0    'p_return_code out number
    Public Const PARAM_OUT_MESSAGE As Integer = 1        'p_error_message out varchar2

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("currency_conversion_id", id.ToByteArray)}
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


    Public Function GetCurrecyRates(ByVal dealerId As Guid, ByVal FromDate As String, ByVal ToDate As String)

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")


        If (Not (FromDate.Equals(String.Empty))) AndAlso (Not (ToDate.Equals(String.Empty))) Then
            selectStmt &= Environment.NewLine & "AND cv.effective_date between to_Date(" & FromDate _
            & ", 'YYYYMMDD') and to_Date(" & ToDate & ", 'YYYYMMDD')"
        End If

        selectStmt &= Environment.NewLine & "ORDER BY cv.effective_date desc"

        Try
            parameters = New OracleParameter() {New OracleParameter(TABLE_KEY_NAME, dealerId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLastRate(ByVal dealerId As Guid, ByVal currency1Id As Guid, ByVal currency2Id As Guid)

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_LAST_RATE")

        Try
            parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                             New OracleParameter(COL_NAME_CURRENCY1_ID, currency1Id.ToByteArray), _
                                             New OracleParameter(COL_NAME_CURRENCY2_ID, currency2Id.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function FindMaxDate(ByVal dealerId As Guid, ByVal currency1Id As Guid, ByVal currency2Id As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/FIND_MAX_DATE")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                            New OracleParameter(COL_NAME_CURRENCY1_ID, currency1Id.ToByteArray), _
                                            New OracleParameter(COL_NAME_CURRENCY2_ID, currency2Id.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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
    'Public Overloads Sub Delete(ByVal dealerId As Guid, ByVal Currency1Id As Guid, ByVal currency2Id As Guid, ByVal effectiveDate As DateType, ByVal expirationDate As DateType, Optional ByVal Transaction As IDbTransaction = Nothing)
    '    Try
    '        Dim selectStmt As String = Me.Config("/SQL/DELETE")
    '        Dim parameters() As DBHelper.DBHelperParameter
    '        Dim tr As IDbTransaction = Transaction
    '        Dim ds As New DataSet
    '        parameters = New DBHelper.DBHelperParameter() _
    '                                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
    '                                     New DBHelper.DBHelperParameter(COL_NAME_CURRENCY1_ID, Currency1Id.ToByteArray), _
    '                                     New DBHelper.DBHelperParameter(COL_NAME_CURRENCY2_ID, currency2Id.ToByteArray), _
    '                                     New DBHelper.DBHelperParameter(COL_NAME_EFFECTIVE_DATE, effectiveDate.Value), _
    '                                     New DBHelper.DBHelperParameter(FILD_NAME_EXPIRATION_DATE, expirationDate.Value)}

    '        'Update(Me.TABLE_NAME, tr, DataRowState.Added Or DataRowState.Modified)

    '        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Sub
#End Region

#Region "StoreProcedures Control"

    ' Insert new Rates
    Public Function ExecuteSP(ByVal dealerID As Guid, ByVal fromdate As DateType, ByVal todate As DateType, _
                              ByVal currency1id As Guid, ByVal currency2id As Guid, ByVal currency1rate As Double, _
                              ByVal currency2rate As Double) As String
        Dim inputParameters(TOTAL_PARAM_IN) As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_PARAM_OUT) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/INSERT_NEW_RATES")
        Dim oErrMess As String

        inputParameters(Me.PARAM_IN_DEALER_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, dealerID)

        If Not fromdate Is Nothing Then
            inputParameters(Me.PARAM_IN_FROM_DATE) = New DBHelper.DBHelperParameter(Me.COL_NAME_EFFECTIVE_DATE, fromdate.Value)
        End If

        If Not todate Is Nothing Then
            inputParameters(Me.PARAM_IN_TO_DATE) = New DBHelper.DBHelperParameter(Me.FILD_NAME_EXPIRATION_DATE, todate.Value)
        End If

        inputParameters(Me.PARAM_IN_CURRENCY1_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY1_ID, currency1id)
        inputParameters(Me.PARAM_IN_CURRENCY2_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY2_ID, currency2id)
        inputParameters(Me.PARAM_IN_CURRENCY1_RATE) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY1_RATE, currency1rate)
        inputParameters(Me.PARAM_IN_CURRENCY2_RATE) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY2_RATE, currency2rate)


        outputParameter(PARAM_OUT_RETURN_CODE) = New DBHelper.DBHelperParameter(RETURN_CODE, GetType(Integer))
        outputParameter(PARAM_OUT_MESSAGE) = New DBHelper.DBHelperParameter(RETURN_MESSAGE, GetType(String), 30)

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(PARAM_OUT_RETURN_CODE).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(PARAM_OUT_MESSAGE).Value)
        End If
    End Function

    'Delete Rates
    Public Function ExecuteSP(ByVal dealerID As Guid, ByVal fromdate As DateType, ByVal todate As DateType, _
                              ByVal currency1id As Guid, ByVal currency2id As Guid) As String
        Dim inputParameters(DELETE_PARAM_IN) As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_PARAM_OUT) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/DELETE_RATES")
        Dim oErrMess As String

        inputParameters(Me.PARAM_IN_DEALER_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, dealerID)

        If Not fromdate Is Nothing Then
            inputParameters(Me.PARAM_IN_FROM_DATE) = New DBHelper.DBHelperParameter(Me.COL_NAME_EFFECTIVE_DATE, fromdate.Value)
        End If

        If Not todate Is Nothing Then
            inputParameters(Me.PARAM_IN_TO_DATE) = New DBHelper.DBHelperParameter(Me.FILD_NAME_EXPIRATION_DATE, todate.Value)
        End If

        inputParameters(Me.PARAM_IN_CURRENCY1_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY1_ID, currency1id)
        inputParameters(Me.PARAM_IN_CURRENCY2_ID) = New DBHelper.DBHelperParameter(Me.COL_NAME_CURRENCY2_ID, currency2id)

        outputParameter(PARAM_OUT_RETURN_CODE) = New DBHelper.DBHelperParameter(RETURN_CODE, GetType(Integer))
        outputParameter(PARAM_OUT_MESSAGE) = New DBHelper.DBHelperParameter(RETURN_MESSAGE, GetType(String), 30)

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(PARAM_OUT_RETURN_CODE).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(PARAM_OUT_MESSAGE).Value)
        End If
    End Function

#End Region
End Class



