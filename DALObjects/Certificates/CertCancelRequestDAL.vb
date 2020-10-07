'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/29/2011)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper
#Region "CertCancelRequestData"

Public Class CertCancelRequestData
    Public certId, commentTypeId As Guid
    Public callerName, comments, created_by As String
    Public cancellationDate, cancelRequestDate As Date
    Public cancellationReasonId, bankInfoId As Guid
    Public certCancelRequestId As Guid
    Public proofOfDocumentation, useExistingBankInfo, ibanNumber, accountNumber, status_description As String
    Public errorExist As Boolean
    Public errorCode As String
End Class

#End Region

Public Class CertCancelRequestDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_CANCEL_REQUEST"
    Public Const TABLE_KEY_NAME As String = "cert_cancel_request_id"

    Public Const COL_NAME_CERT_CANCEL_REQUEST_ID As String = "cert_cancel_request_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CANCELLATION_REASON_ID As String = "cancellation_reason_id"
    Public Const COL_NAME_CANCELLATION_REQUEST_DATE As String = "cancellation_request_date"
    Public Const COL_NAME_CANCELLATION_DATE As String = "cancellation_date"
    Public Const COL_NAME_PROCESSED_DATE As String = "processed_date"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_STATUS_DESCRIPTION As String = "status_description"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_PROOF_OF_DOCUMENTATION As String = "Proof_of_Documentation"
    Public Const COL_NAME_BANK_INFO_ID As String = "Bank_info_id"

    ' Cancel request Store Procedure Input Parameters
    Public Const P_CERT_ID = 0
    Public Const P_CERT_CANCEL_REQUEST_ID = 1
    Public Const P_CANCELLATION_REASON_ID = 2
    Public Const P_CANCELLATION_DATE = 3
    Public Const P_CANCELLATION_REQUEST_DATE = 4
    Public Const P_PROOF_OF_DOCUMENTATION = 5
    Public Const P_USE_EXISTING_BANK_INFO = 6
    Public Const P_IBAN_NUMBER = 7
    Public Const P_ACCOUNT_NUMBER = 8
    Public Const P_BANK_INFO_ID = 9
    Public Const P_COMMENT_TYPE_ID = 10
    Public Const P_CALLER_NAME = 11
    Public Const P_COMMENTS = 12
    Public Const P_CREATED_BY = 13

    Public Const COL_NAME_P_CERT_ID = "p_cert_id"
    Public Const COL_NAME_P_CERT_CANCEL_REQUEST_ID = "p_cert_cancel_request_id"
    Public Const COL_NAME_P_CANCELLATION_REASON_ID = "p_cancellation_Reason_id"
    Public Const COL_NAME_P_CANCELLATION_DATE = "p_cancellation_date"
    Public Const COL_NAME_P_CANCELLATION_REQUEST_DATE = "p_cancellation_request_date"
    Public Const COL_NAME_P_PROOF_OF_DOCUMENTATION = "p_proof_of_documentation"
    Public Const COL_NAME_P_USE_EXISTING_BANK_INFO = "p_use_existing_bank_info"
    Public Const COL_NAME_P_IBAN_NUMBER = "p_iban_number"
    Public Const COL_NAME_P_ACCOUNT_NUMBER = "p_account_number"
    Public Const COL_NAME_P_BANK_INFO_ID = "p_bank_info_id"
    Public Const COL_NAME_P_COMMENT_TYPE_ID = "p_comment_type_id"
    Public Const COL_NAME_P_CALLER_NAME = "p_caller_name"
    Public Const COL_NAME_P_COMMENTS = "p_comments"
    Public Const COL_NAME_P_CREATED_BY = "p_created_by"

    Public Const COL_NAME_P_RETURN As String = "p_return"
    Public Const COL_NAME_P_REFUND_AMOUNT As String = "p_refund_amount"
    Public Const COL_NAME_P_EXCEPTION_MSG As String = "p_exception_msg"

    ' Cancel request Store Procedure Output Parameters
    Public Const P_RETURN = 0
    Public Const P_REFUND_AMOUNT = 1
    Public Const P_EXCEPTION_MSG = 2

    Public Const TOTAL_INPUT_PARAM_CANCEL_REQUEST = 13
    Public Const TOTAL_OUTPUT_PARAM_CANCEL_REQUEST = 2

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_cancel_request_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadByCertId(certId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CERT_ID")
        Try
            parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_KEY_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
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

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteCancelRequestSP(oCertCancelRequestData As CertCancelRequestData, ByRef dblRefundAmount As Double, ByRef strMsg As String) As CertCancelRequestData
        Dim selectStmt As String = Config("/SQL/PROCESS_CANCELLATION_REQUEST")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL_REQUEST) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL_REQUEST) As DBHelperParameter

        With oCertCancelRequestData
            inputParameters(P_CERT_ID) = New DBHelperParameter(COL_NAME_P_CERT_ID, .certId.ToByteArray)
            inputParameters(P_CERT_CANCEL_REQUEST_ID) = New DBHelperParameter(COL_NAME_P_CERT_CANCEL_REQUEST_ID, .certCancelRequestId.ToByteArray)
            inputParameters(P_CANCELLATION_REASON_ID) = New DBHelperParameter(COL_NAME_P_CANCELLATION_REASON_ID, .cancellationReasonId.ToByteArray)
            inputParameters(P_CANCELLATION_DATE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, .cancellationDate)
            inputParameters(P_CANCELLATION_REQUEST_DATE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_REQUEST_DATE, .cancelRequestDate)
            inputParameters(P_PROOF_OF_DOCUMENTATION) = New DBHelperParameter(COL_NAME_P_PROOF_OF_DOCUMENTATION, .proofOfDocumentation)
            inputParameters(P_USE_EXISTING_BANK_INFO) = New DBHelperParameter(COL_NAME_P_USE_EXISTING_BANK_INFO, .useExistingBankInfo)
            inputParameters(P_IBAN_NUMBER) = New DBHelperParameter(P_IBAN_NUMBER, .ibanNumber)
            inputParameters(P_ACCOUNT_NUMBER) = New DBHelperParameter(P_ACCOUNT_NUMBER, .accountNumber)
            inputParameters(P_BANK_INFO_ID) = New DBHelperParameter(COL_NAME_P_BANK_INFO_ID, .bankInfoId.ToByteArray)
            inputParameters(P_COMMENT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_COMMENT_TYPE_ID, .commentTypeId.ToByteArray)
            inputParameters(P_CALLER_NAME) = New DBHelperParameter(P_CALLER_NAME, .callerName)
            inputParameters(P_COMMENTS) = New DBHelperParameter(P_COMMENTS, .comments)
            inputParameters(P_CREATED_BY) = New DBHelperParameter(P_CREATED_BY, .created_by)

            outputParameter(P_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))
            outputParameter(P_REFUND_AMOUNT) = New DBHelperParameter(COL_NAME_P_REFUND_AMOUNT, GetType(Double))
            outputParameter(P_EXCEPTION_MSG) = New DBHelperParameter(COL_NAME_P_EXCEPTION_MSG, GetType(String), 50)

        End With

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(P_RETURN).Value <> 0 Then
            If outputParameter(P_RETURN).Value = 500 Then
                Throw New StoredProcedureGeneratedException("", outputParameter(P_EXCEPTION_MSG).Value)
            Else
                Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(P_EXCEPTION_MSG).Value)
            End If
        Else
            If outputParameter(P_EXCEPTION_MSG).Value = "Success" Then
                strMsg = outputParameter(P_EXCEPTION_MSG).Value
                dblRefundAmount = outputParameter(P_REFUND_AMOUNT).Value
            ElseIf outputParameter(P_EXCEPTION_MSG).Value = "Future" Then
                strMsg = outputParameter(P_EXCEPTION_MSG).Value
            ElseIf Not outputParameter(P_EXCEPTION_MSG).Value is Nothing Then
                strMsg = outputParameter(P_EXCEPTION_MSG).Value
            End If
        End If
        Return oCertCancelRequestData

    End Function

#End Region
End Class


