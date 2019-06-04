Public Class PaymentDetailDAL
    Inherits DALBase

#Region "Constants"
    Public Const PAYMENT_TABLE_NAME As String = "ELP_CERT_PAYMENT"
    Public Const COLLECTED_TABLE_NAME As String = "ELP_CERT_COLLECTED_PAYMENT"
    
    'ELP_CERT_PAYMENT
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_COVERAGE_SEQ As String = "Coverage_seq"
    Public Const COL_NAME_DATE_PAID_FROM As String = "date_paid_from"
    Public Const COL_NAME_DATE_PAID_FOR As String = "date_paid_for"
    Public Const COL_NAME_DATE_OF_PAYMENT As String = "date_of_payment"
    Public Const COL_NAME_DATE_PROCESSED As String = "date_processed"
    Public Const COL_NAME_SOURCE As String = "Source"
    'Req - 1016 - Start
    Public Const COL_NAME_PAYMENT_DUE_DATE As String = "payment_due_date"
    'Req - 1016 - End
    Public Const COL_NAME_PAYMENT_INFO As String = "payment_info"

    'ELP_CERT_COLLECTED_PAYMENT
    Public Const COL_NAME_CERT_PAYMENT_ID As String = "cert_payment_id"    
    Public Const COL_NAME_COLLECTED_AMOUNT As String = "collected_amount"    
    Public Const COL_NAME_COLLECTED_DATE As String = "collected_date"    
    Public Const COL_NAME_BILLING_START_DATE As String = "billing_start_date"    
    Public Const COL_NAME_INCOMING_AMOUNT As String = "incoming_amount"  
    Public Const COL_NAME_CREATED_DATE As String = "Created_date"

    Public Const COL_NAME_INSTALLMENT_NUM As String = "installment_num"
    Public Const COL_NAME_PAYMENT_REFERENCE_NUMBER As String = "payment_reference_number"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub
#End Region

#Region "Load Methods"

    Public Function LoadPaymentHistList(ByVal certId As Guid, ByVal Sortby As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PAYMENT_HISTORY")

        Try
            selectStmt = OracleDbHelper.ReplaceParameter(selectStmt, COL_NAME_CERT_ID.ToUpper, certId)

            If Sortby Is Nothing OrElse Sortby = "" Then
                Sortby = "to_char(DATE_PAID_FOR,'YYYYMMDD')"
            End If

            If Not Sortby.ToUpper.Contains("DATE_PROCESSED") Then
                Sortby &= ",to_char(cp.date_processed,'YYYYMMDD') desc"
            End If

            selectStmt &= " order by " & Sortby

            Return OracleDbHelper.Fetch(selectStmt, Me.PAYMENT_TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCollectedHistList(ByVal certId As Guid, ByVal Sortby As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COLLECTED_HISTORY")

        Try
            selectStmt = OracleDbHelper.ReplaceParameter(selectStmt, COL_NAME_CERT_ID.ToUpper, certId)

            If Sortby Is Nothing OrElse Sortby = "" Then
                Sortby = "to_char(ccp.collected_date,'YYYYMMDD') desc"
            End If

            If Not Sortby.ToUpper.Contains("COLLECTED_DATE") Then
                Sortby &= ",to_char(ccp.collected_date,'YYYYMMDD') desc"
            End If

            selectStmt &= " order by " & Sortby

            Return OracleDbHelper.Fetch(selectStmt, Me.COLLECTED_TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadPaymentTotals(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/PAYMENT_SUM_AND_COUNT")
        'Dim parameters() As DBHelper.DBHelperParameter

        'parameters = New DBHelper.DBHelperParameter() _
        '                            {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}

        'Dim ds As New DataSet
        Try
            selectStmt = OracleDbHelper.ReplaceParameter(selectStmt, COL_NAME_CERT_ID.ToUpper, certId)

            Return OracleDbHelper.Fetch(selectStmt, Me.PAYMENT_TABLE_NAME)

            'DBHelper.Fetch(ds, selectStmt, Me.PAYMENT_TABLE_NAME, parameters)
            'Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCollectedTotals(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/COLLECTED_SUM_AND_COUNT")
        'Dim parameters() As DBHelper.DBHelperParameter

        'parameters = New DBHelper.DBHelperParameter() _
        '                            {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}

        'Dim ds As New DataSet
        Try
            selectStmt = OracleDbHelper.ReplaceParameter(selectStmt, COL_NAME_CERT_ID.ToUpper, certId)

            Return OracleDbHelper.Fetch(selectStmt, Me.COLLECTED_TABLE_NAME)

            'DBHelper.Fetch(ds, selectStmt, Me.COLLECTED_TABLE_NAME, parameters)
            'Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region
End Class
