'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/7/2015)********************


Public Class AFAMaintainenceDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "PROCESS_STATUS"

    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PROCESS_TYPE As String = "process_type"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_COMMENTS As String = "comments"
    Public Const COL_NAME_START_DATE_TIME As String = "start_date_time"
    Public Const COL_NAME_COMPLETION_DATE_TIME As String = "completetion_date_time"



    Private Const INPUT_PARAM_NAME_DEALER_ID = "pi_dealer_id"
    Private Const INPUT_PARAM_NAME_BILLING_DATE_START = "pi_billingdtstart"
    Private Const INPUT_PARAM_NAME_BILLING_DATE_END = "pi_billingdtend"
    Private Const INPUT_PARAM_NAME_USER_NAME = "pi_username"
    Private Const INPUT_PARAM_NAME_LANGUAGE_ID = "pi_language_id"
    Private Const OUTPUT_PARAM_NAME_RESULT = "po_Result"


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"


    Public Function GetProcessStatus(ByVal dealerId As Guid, ByVal languageId As Guid, ByVal firstDayOfMonth As Date, ByVal lastDayOfMonth As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PROCESS_STATUS")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                                                                            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_DEALER_ID, dealerId.ToByteArray),
                                                                                            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_START, firstDayOfMonth),
                                                                                            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_END, lastDayOfMonth)}


        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'ReRunReconciliation
    ' Execute Store Procedure
    Public Function ReRunReconciliation(ByVal dealerId As Guid, ByVal firstDayOfMonth As Date, ByVal lastDayOfMonth As Date, ByVal userName As String) As Boolean

        Dim selectStmt As String = Config("/SQL/RERUN_RECONCILIATION")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter() As DBHelper.DBHelperParameter
        Dim result As Boolean
        inputParameters = New DBHelper.DBHelperParameter() {
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_DEALER_ID, dealerId.ToByteArray),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_START, firstDayOfMonth),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_END, lastDayOfMonth),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_USER_NAME, userName)}


        outputParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(OUTPUT_PARAM_NAME_RESULT, GetType(String))}
        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

            result = CType(outputParameter(0).Value, String).Trim = "Y"
            Return result
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    ' Execute Store Procedure
    Public Function ReRunInvoice(ByVal dealerId As Guid, ByVal firstDayOfMonth As Date, ByVal lastDayOfMonth As Date, ByVal userName As String) As Boolean

        Dim selectStmt As String = Config("/SQL/RERUN_INVOICE")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter() As DBHelper.DBHelperParameter
        Dim result As Boolean

        inputParameters = New DBHelper.DBHelperParameter() {
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_DEALER_ID, dealerId.ToByteArray),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_START, firstDayOfMonth),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_BILLING_DATE_END, lastDayOfMonth),
            New DBHelper.DBHelperParameter(INPUT_PARAM_NAME_USER_NAME, userName)}


        outputParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(OUTPUT_PARAM_NAME_RESULT, GetType(String))}
        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

            result = CType(outputParameter(0).Value, String).Trim = "Y"
            Return result
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region



End Class


