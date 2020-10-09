Public Class OcMessageAttemptsDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_MESSAGE_ATTEMPS"
    Public Const TABLE_KEY_NAME As String = "oc_message_attemps_id"

    Public Const COL_NAME_OC_MESSAGE_RECIPIENT_ID As String = "oc_message_recipient_id"
    Public Const COL_NAME_OC_MESSAGE_ID As String = "oc_message_id"

    Public Const COL_NAME_OC_MESSAGE_ATTEMPS_ID = "oc_message_attemps_id"
    Public Const COL_NAME_OC_TEMPLATE_ID = "oc_template_id"
    Public Const COL_NAME_RECIPIENT_ADDRESS = "recipient_address"
    Public Const COL_NAME_PROCESS_STATUS_XCD As String = "process_status_xcd"
    Public Const COL_NAME_PROCESS_STATUS_DESCRIPTION As String = "process_status_description"
    Public Const COL_NAME_RECIPIENT_DESCRIPTION As String = "recipient_description"
    Public Const COL_NAME_ERR_MESSAGE As String = "err_message"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Public Methods"
    Public Sub AddMessageAttempt(message_id As Guid, recipient_address As String, description As String, sender_reason As String, ByRef returnCode As Integer, ByRef returnMessage As String)
        Dim selectStmt As String = Config("/SQL/ADD_MESSAGE_ATTEMPT")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(1) As DBHelper.DBHelperParameter

        inputParameters = New DBHelper.DBHelperParameter() _
        {
            New DBHelper.DBHelperParameter("pi_message_id", message_id.ToByteArray),
            New DBHelper.DBHelperParameter("pi_recipient", recipient_address),
            New DBHelper.DBHelperParameter("pi_description", description),
            New DBHelper.DBHelperParameter("p_send_reason", sender_reason)
        }

        outputParameter(0) = New DBHelper.DBHelperParameter("po_return_code", GetType(Integer))
        outputParameter(1) = New DBHelper.DBHelperParameter("po_error_msg", GetType(String), 50)

        Try
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, New DataSet(), TABLE_NAME)

            If outputParameter(0).Value <> 0 Then
                returnCode = CType(outputParameter(0).Value, Integer)
                returnMessage = CType(outputParameter(1).Value, String)
            Else
                returnMessage = "SUCCESS"
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_message_attemps_id", id.ToByteArray)}
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

    Public Function LoadList(oc_message_id As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        LoadList(ds, oc_message_id, languageId)
        Return ds
    End Function

    Public Sub LoadList(ds As DataSet, ocMessageId As Guid, languageId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_MESSAGE_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {
            New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
            New DBHelper.DBHelperParameter(COL_NAME_OC_MESSAGE_ID, ocMessageId.ToByteArray)
            }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Sub
#End Region

End Class
