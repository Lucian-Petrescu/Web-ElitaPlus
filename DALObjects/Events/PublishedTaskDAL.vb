Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects.DBHelper

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/4/2013)********************


Public Class PublishedTaskDAL
    Inherits DALBase

    Public Class PublishTaskData(Of TType)
        Public Property CompanyGroupId As Guid
        Public Property CompanyId As Guid
        Public Property CountryId As Guid
        Public Property DealerId As Guid
        Public Property ProductCode As String
        Public Property CoverageTypeId As Guid
        Public Property Data As TType
    End Class


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PUBLISHED_TASK"
    Public Const TABLE_KEY_NAME As String = "published_task_id"

    Public Const COL_NAME_PUBLISHED_TASK_ID As String = "published_task_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_EVENT_TYPE_ID As String = "event_type_id"
    Public Const COL_NAME_EVENT_DATE As String = "event_date"
    Public Const COL_NAME_SENDER As String = "sender"
    Public Const COL_NAME_ARGUMENTS As String = "arguments"
    Public Const COL_NAME_TASK_ID As String = "task_id"
    Public Const COL_NAME_LOCK_DATE As String = "lock_date"
    Public Const COL_NAME_TASK_STATUS_ID As String = "task_status_id"
    Public Const COL_NAME_RETRY_COUNT As String = "retry_count"
    Public Const COL_NAME_LAST_ATTEMPT_DATE As String = "last_attempt_date"
    Public Const COL_NAME_MACHINE_NAME As String = "machine_name"
    Public Const COL_NAME_FAIL_REASON As String = "fail_reason"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const PAR_NAME_SUBSCRIBER_ID As String = "p_subscriber_id"
    Public Const PAR_NAME_MACHINE_NAME As String = "p_machine_name"
    Public Const PAR_NAME_PROCESS_THREAD_NAME As String = "p_process_thread_name"
    Public Const PAR_NAME_FAIL_REASON As String = "p_fail_reason"
    Public Const PAR_NAME_PUBLISHED_TASK_ID As String = "p_published_task_id"
    Public Const PAR_NAME_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const PAR_NAME_COMPANY_ID As String = "p_company_id"
    Public Const PAR_NAME_COUNTRY_ID As String = "p_country_id"
    Public Const PAR_NAME_DEALER_ID As String = "p_dealer_id"
    Public Const PAR_NAME_EVENT_ARGUMENT_ID As String = "p_event_argument_id"
    Public Const PAR_NAME_PRODUCT_CODE As String = "p_product_code"
    Public Const PAR_NAME_COVERAGE_TYPE_ID As String = "p_coverage_code_id"
    Public Const PAR_NAME_SENDER As String = "p_sender"
    Public Const PAR_NAME_ARGUMENTS As String = "p_arguments"
    Public Const PAR_NAME_EVENT_DATE As String = "p_event_date"
    Public Const PAR_NAME_EVENT_TYPE_ID As String = "p_event_type_id"
    Public Const PAR_NAME_DEALER_GROUP_ID As String = "p_dealer_group_id"

    Public Const MAX_NUMBER_OF_ROWS As Int32 = 101
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
        Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("published_task_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(languageId As Guid _
                        , companyGroupId As Guid _
                        , companyId As Guid _
                        , dealerId As Guid _
                        , countryId As Guid _
                        , product As String _
                        , coverageTypeId As Guid _
                        , eventTypeId As Guid _
                        , task As String _
                        , statusId As Guid _
                        , sender As String _
                        , arguments As String _
                        , machineName As String _
                        , startDate As String _
                        , endDate As String _
                        , LimitResultset As Integer) As DataSet


        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim languageId1Param As DBHelperParameter
        Dim languageId2Param As DBHelperParameter
        Dim languageId3Param As DBHelperParameter
        Dim rowNumParam As DBHelperParameter
        Dim DateParam As Date


        If companyGroupId <> Guid.Empty Then
            '***** Need to chnage to the code bellow
            whereClauseConditions &= " AND elp_published_task.company_group_id = " & "hextoraw( '" & GuidToSQLString(companyGroupId) & "')"
            'whereClauseConditions &= " AND elp_company_group.company_group_id = " & "hextoraw( '" & Me.GuidToSQLString(companyGroupId) & "')"
        End If

        If companyId <> Guid.Empty Then
            '***** Need to chnage to the code bellow
            whereClauseConditions &= " AND elp_published_task.company_id = " & "hextoraw( '" & GuidToSQLString(companyId) & "')"
            'whereClauseConditions &= " AND elp_company.company_id = " & "hextoraw( '" & Me.GuidToSQLString(companyId) & "')"
        End If

        If dealerId <> Guid.Empty Then
            '***** Need to chnage to the code bellow
            whereClauseConditions &= " AND elp_published_task.dealer_id = " & "hextoraw( '" & GuidToSQLString(dealerId) & "')"
            'whereClauseConditions &= " AND elp_dealer.dealer_id = " & "hextoraw( '" & Me.GuidToSQLString(dealerId) & "')"
        End If

        If product <> String.Empty Then
            product = product.Replace("*", "%") & "%"
            whereClauseConditions &= " AND elp_published_task.product_code Like " & "'" & product & "'"
        End If

        If coverageTypeId <> Guid.Empty Then
            whereClauseConditions &= " AND elp_published_task.coverage_type_id = " & "hextoraw( '" & GuidToSQLString(coverageTypeId) & "')"
        End If

        If eventTypeId <> Guid.Empty Then
            whereClauseConditions &= " AND elp_published_task.event_type_id = " & "hextoraw( '" & GuidToSQLString(eventTypeId) & "')"
        End If

        If statusId <> Guid.Empty Then
            whereClauseConditions &= " AND elp_published_task.task_status_id = " & "hextoraw( '" & GuidToSQLString(statusId) & "')"
        End If

        '************ Need to chnage to the correct table ********************
        If startDate <> String.Empty Then
            startDate = DateHelper.GetEnglishDate(DateHelper.GetDateValue(startDate))
            whereClauseConditions &= " AND elp_published_task.event_date >= " & "to_date('" & startDate & "')"
            '            whereClauseConditions &= " AND elp_dealer.MODIFIED_DATE >= " & "to_date('" & DateParam.ToString("dd-MMM-yy") & "')"
        End If

        If endDate <> String.Empty Then
            endDate = DateHelper.GetEnglishDate(DateHelper.GetDateValue(endDate))
            whereClauseConditions &= " AND elp_published_task.event_date <= " & "(to_date('" & endDate & "') + 1)"
            '               whereClauseConditions &= " AND elp_dealer.MODIFIED_DATE <= " & "(to_date('" & DateParam.ToString("dd-MMM-yy") & "') + 1)"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            languageId1Param = New DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            languageId2Param = New DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            languageId3Param = New DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())

            rowNumParam = New DBHelperParameter(PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelperParameter() {languageId1Param, languageId2Param, languageId3Param, rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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

#Region "Stored Procedures Calls"

    Public Sub AddEvent(companyGroupId As Guid,
                        companyId As Guid,
                        countryId As Guid,
                        dealerId As Guid,
                        productCode As String,
                        coverageTypeId As Guid,
                        sender As String,
                        arguments As String,
                        eventDate As DateTime,
                        eventTypeId As Guid,
                        eventArgumentId As Guid,
                        Optional ByVal dealergroupId As Guid = Nothing)

        Dim selectStmt As String = Config("/SQL/ADD_EVENT")
        Dim inputParameters(11) As DBHelperParameter

        If (companyGroupId.Equals(Guid.Empty)) Then
            inputParameters(0) = New DBHelperParameter(PAR_NAME_COMPANY_GROUP_ID, Nothing)
        Else
            inputParameters(0) = New DBHelperParameter(PAR_NAME_COMPANY_GROUP_ID, companyGroupId, GetType(Guid))
        End If
        If (companyId.Equals(Guid.Empty)) Then
            inputParameters(1) = New DBHelperParameter(PAR_NAME_COMPANY_ID, Nothing)
        Else
            inputParameters(1) = New DBHelperParameter(PAR_NAME_COMPANY_ID, companyId, GetType(Guid))
        End If
        If (countryId.Equals(Guid.Empty)) Then
            inputParameters(2) = New DBHelperParameter(PAR_NAME_COUNTRY_ID, Nothing)
        Else
            inputParameters(2) = New DBHelperParameter(PAR_NAME_COUNTRY_ID, countryId, GetType(Guid))
        End If
        If (dealerId.Equals(Guid.Empty)) Then
            inputParameters(3) = New DBHelperParameter(PAR_NAME_DEALER_ID, Nothing)
        Else
            inputParameters(3) = New DBHelperParameter(PAR_NAME_DEALER_ID, dealerId, GetType(Guid))
        End If
        If (productCode Is Nothing OrElse Trim(productCode) = String.Empty) Then
            inputParameters(4) = New DBHelperParameter(PAR_NAME_PRODUCT_CODE, Nothing)
        Else
            inputParameters(4) = New DBHelperParameter(PAR_NAME_PRODUCT_CODE, productCode, GetType(String))
        End If
        If (coverageTypeId.Equals(Guid.Empty)) Then
            inputParameters(5) = New DBHelperParameter(PAR_NAME_COVERAGE_TYPE_ID, Nothing)
        Else
            inputParameters(5) = New DBHelperParameter(PAR_NAME_COVERAGE_TYPE_ID, coverageTypeId, GetType(Guid))
        End If
        inputParameters(6) = New DBHelperParameter(PAR_NAME_SENDER, sender, GetType(String))
        inputParameters(7) = New DBHelperParameter(PAR_NAME_ARGUMENTS, arguments, GetType(String))
        inputParameters(8) = New DBHelperParameter(PAR_NAME_EVENT_DATE, eventDate, GetType(Date))
        inputParameters(9) = New DBHelperParameter(PAR_NAME_EVENT_TYPE_ID, eventTypeId, GetType(Guid))

        If (eventArgumentId.Equals(Guid.Empty)) Then
            inputParameters(10) = New DBHelperParameter(PAR_NAME_EVENT_ARGUMENT_ID, Nothing)
        Else
            inputParameters(10) = New DBHelperParameter(PAR_NAME_EVENT_ARGUMENT_ID, eventArgumentId, GetType(Guid))
        End If
        If (dealergroupId.Equals(Guid.Empty)) Then
            inputParameters(11) = New DBHelperParameter(PAR_NAME_DEALER_GROUP_ID, Nothing)
        Else
            inputParameters(11) = New DBHelperParameter(PAR_NAME_DEALER_GROUP_ID, eventArgumentId, GetType(Guid))
        End If
        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetNextTaskId(subscriberId As Guid, machineName As String, processThreadName As String) As Guid

        Dim selectStmt As String = Config("/SQL/GET_NEXT_TASK")
        Dim inputParameters(2) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter
        Dim pub_Task_Id As Guid

        inputParameters(0) = New DBHelperParameter(PAR_NAME_SUBSCRIBER_ID, subscriberId, GetType(Guid))
        inputParameters(1) = New DBHelperParameter(PAR_NAME_MACHINE_NAME, machineName, GetType(String))
        inputParameters(2) = New DBHelperParameter(PAR_NAME_PROCESS_THREAD_NAME, processThreadName, GetType(String))
        outputParameter(0) = New DBHelperParameter(PAR_NAME_PUBLISHED_TASK_ID, GetType(String), 32)

        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, outputParameter)
            Dim pub_Task_Id_string As String
            pub_Task_Id_string = DirectCast(outputParameter(0).Value, String)
            If (Not String.IsNullOrEmpty(pub_Task_Id_string)) Then
                pub_Task_Id = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(pub_Task_Id_string))
            End If
            Return pub_Task_Id
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub CompleteTask(publishedTaskId As Guid, machineName As String, processThreadName As String)
        Dim selectStmt As String = Config("/SQL/COMPLETE_TASK")
        Dim inputParameters(2) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(PAR_NAME_PUBLISHED_TASK_ID, publishedTaskId, GetType(Guid))
        inputParameters(1) = New DBHelperParameter(PAR_NAME_MACHINE_NAME, machineName, GetType(String))
        inputParameters(2) = New DBHelperParameter(PAR_NAME_PROCESS_THREAD_NAME, processThreadName, GetType(String))

        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub FailedTask(publishedTaskId As Guid, machineName As String, processThreadName As String, failReason As String)
        Dim selectStmt As String = Config("/SQL/FAILED_TASK")
        Dim inputParameters(3) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(PAR_NAME_PUBLISHED_TASK_ID, publishedTaskId, GetType(Guid))
        inputParameters(1) = New DBHelperParameter(PAR_NAME_MACHINE_NAME, machineName, GetType(String))
        inputParameters(2) = New DBHelperParameter(PAR_NAME_PROCESS_THREAD_NAME, processThreadName, GetType(String))
        inputParameters(3) = New DBHelperParameter(PAR_NAME_FAIL_REASON, failReason, GetType(String))

        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ResetTask(publishedTaskId As Guid)
        Dim selectStmt As String = Config("/SQL/RESET_TASK")
        Dim inputParameters(0) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(PAR_NAME_PUBLISHED_TASK_ID, publishedTaskId, GetType(Guid))

        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteTask(publishedTaskId As Guid)
        Dim selectStmt As String = Config("/SQL/DELETE_TASK")
        Dim inputParameters(0) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(PAR_NAME_PUBLISHED_TASK_ID, publishedTaskId, GetType(Guid))

        Try
            ' Call DBHelper Store Procedure
            ExecuteSp(selectStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public SUB GetOutBoundMessageDetails(publishedTaskId As Guid, ByRef oErrCode As Integer, ByRef oErrMsg As String,
                                         ByRef oMessageId As Guid, ByRef oTemplateCode As String, ByRef oWhiteList as string, 
                                         ByRef oTemplateUserName As string, ByRef oTemplatePassword As String,                      
                                         byref oRecipients As List(Of String),
                                         ByRef oTemplateParams as Dictionary(Of String, string))
        Dim selectStmt As String = Config("/SQL/GET_OUTBOUND_EMAIL_DETAILS")
        Dim strTemp as String, strKey as String, strValue as string
        oErrCode = 0
        oErrMsg = string.Empty

        
        Using connection As New OracleConnection(ConnectString)
            Using command as OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection) 
                command.BindByName = True
                command.AddParameter("pi_published_task_id", OracleDbType.Raw, 16, publishedTaskId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("po_return_code", OracleDbType.Int64, 10, nothing, ParameterDirection.Output)
                command.AddParameter("po_error_msg", OracleDbType.Varchar2, 1000, nothing, ParameterDirection.Output)
                command.AddParameter("po_message_id", OracleDbType.Varchar2, 100, Nothing, ParameterDirection.Output)
                command.AddParameter("po_template_code", OracleDbType.Varchar2, 100, nothing, ParameterDirection.Output)
                command.AddParameter("po_template_user_name", OracleDbType.Varchar2, 100, nothing, ParameterDirection.Output)
                command.AddParameter("po_template_password", OracleDbType.Varchar2, 100, nothing, ParameterDirection.Output)
                command.AddParameter("po_white_list", OracleDbType.Varchar2, 4000, nothing, ParameterDirection.Output)                
                command.AddParameter("po_dest_address_table", OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                command.AddParameter("po_dest_param_table", OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                
                Try
                    connection.Open()
                    dim dr As OracleDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
                    strTemp = command.Parameters("po_return_code").Value.ToString()
                    Integer.TryParse(strTemp, oErrCode)
                    oErrMsg = command.Parameters("po_error_msg").Value.ToString()
                    strTemp = command.Parameters("po_message_id").Value.tostring()
                    If (Not String.IsNullOrEmpty(strTemp)) Then
                        oMessageId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(strTemp))
                    else
                        oMessageId = guid.Empty
                    End If
                    
                    oTemplateCode = command.Parameters("po_template_code").Value.ToString()
                    oTemplateUserName = command.Parameters("po_template_user_name").Value.ToString()
                    oTemplatePassword = command.Parameters("po_template_password").Value.ToString()
                    oWhiteList = command.Parameters("po_white_list").Value.ToString()

                    while dr.Read()
                        strTemp = dr("recipient_address").ToString()
                        oRecipients.Add(strTemp)
                    End While

                    dr.NextResult()
                    while dr.Read()
                        strKey = dr("param_name").ToString()                        
                        if string.IsNullOrEmpty(dr("param_value").ToString()) Then
                            strValue = string.Empty
                        else
                            strValue = dr("param_value").ToString()
                        End If
                        oTemplateParams.Add(strKey, strValue)
                    End While

                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            end using            
        End Using
    End Sub
    Public Sub CheckSLAClaimStatus(ClaimId As Guid, ByRef oErrCode As Integer, ByRef strErrMsg As String)
        Dim selectStmt As String = Config("/SQL/CHECK_CLAIM_SLA_STATUS")
        oErrCode = 0
        strErrMsg = String.Empty
        Dim strTemp As String

        Using connection As New OracleConnection(ConnectString)
            Using command As OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection)
                command.BindByName = True
                command.AddParameter("pi_claim_id", OracleDbType.Raw, 16, ClaimId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("po_return_code", OracleDbType.Int64, 10, Nothing, ParameterDirection.Output)
                command.AddParameter("po_error_msg", OracleDbType.Varchar2, 1000, Nothing, ParameterDirection.Output)
                Try
                    connection.Open()
                    Dim dr As OracleDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
                    strTemp = command.Parameters("po_return_code").Value.ToString()
                    Integer.TryParse(strTemp, oErrCode)
                    strErrMsg = command.Parameters("po_error_msg").Value.ToString()
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End Using
        End Using
    End Sub
    Public SUB UpdateOutBoundMessageStatus(guidMsgId As guid, strRecipient as String, strProcessStatus as string,
                                           strCommReferenceId as String, strErrMsg as String, strProcessComments as String,
                                           ByRef oErrCode As Integer)
        Dim selectStmt As String = Config("/SQL/UPDATE_OUTBOUND_EMAIL_STATUS")
        Dim strTemp as String
        oErrCode = 0
        
        
        Using connection As New OracleConnection(ConnectString)
            Using command as OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection) 
                command.BindByName = True
                command.AddParameter("pi_message_id", OracleDbType.Raw, 16, guidMsgId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_recipient", OracleDbType.Varchar2, 500, strRecipient, ParameterDirection.Input)
                command.AddParameter("pi_process_status", OracleDbType.Varchar2, 100, strProcessStatus, ParameterDirection.Input)
                command.AddParameter("pi_comm_reference_Id", OracleDbType.Varchar2, 100, strCommReferenceId, ParameterDirection.Input)
                command.AddParameter("pi_err_message", OracleDbType.Varchar2, 3000, strErrMsg, ParameterDirection.Input)
                command.AddParameter("pi_process_comment", OracleDbType.Varchar2, 500, strProcessComments, ParameterDirection.Input)
                command.AddParameter("po_return_code", OracleDbType.Int64, 10, nothing, ParameterDirection.Output)                
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    strTemp = command.Parameters("po_return_code").Value.ToString()
                    Integer.TryParse(strTemp, oErrCode)   
                    connection.Close()                 
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            end using            
        End Using
    End SUB

    Public SUB UpdateResendMessageStatus(guidMsgRecipientId As guid, strProcessStatus as string,
                                           strErrMsg as String, strProcessComments as String,
                                           ByRef oErrCode As Integer)
        Dim selectStmt As String = Config("/SQL/UPDATE_RESEND_STATUS")
        Dim strTemp as String
        oErrCode = 0
        
        
        Using connection As New OracleConnection(ConnectString)
            Using command as OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection) 
                command.BindByName = True
                command.AddParameter("pi_msg_recipient_id", OracleDbType.Raw, 16, guidMsgRecipientId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_process_status", OracleDbType.Varchar2, 100, strProcessStatus, ParameterDirection.Input)
                command.AddParameter("pi_err_message", OracleDbType.Varchar2, 3000, strErrMsg, ParameterDirection.Input)
                command.AddParameter("pi_process_comment", OracleDbType.Varchar2, 500, strProcessComments, ParameterDirection.Input)
                command.AddParameter("po_return_code", OracleDbType.Int64, 10, nothing, ParameterDirection.Output)                
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    strTemp = command.Parameters("po_return_code").Value.ToString()
                    Integer.TryParse(strTemp, oErrCode) 
                    connection.Close()                   
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)                
                End Try
            end using            
        End Using
    End Sub


#End Region


#Region "Gift Card Info"
    Public Sub InsertGiftCardInfo(giftCardNumber As String, serialNumber As String,
                                   securityCode1 As String, securityCode2 As String, expirationDate As Date,
                                   giftCardRequestId As Guid, encryptedGiftCardLink As String)
        Dim selectStmt As String = Config("/SQL/INSERT_GIFT_CARD_INFO")
        Dim strTemp As String


        Using connection As New OracleConnection(ConnectString)
            Using command As OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection)
                command.BindByName = True
                command.AddParameter("pi_gift_card_number", OracleDbType.Varchar2, 100, giftCardNumber, ParameterDirection.Input)
                command.AddParameter("pi_serial_number", OracleDbType.Varchar2, 100, serialNumber, ParameterDirection.Input)
                command.AddParameter("pi_security_code1", OracleDbType.Varchar2, 100, securityCode1, ParameterDirection.Input)
                command.AddParameter("pi_security_code2", OracleDbType.Varchar2, 100, securityCode2, ParameterDirection.Input)
                command.AddParameter("pi_expiration_date", OracleDbType.Date, 30, expirationDate, ParameterDirection.Input)
                command.AddParameter("pi_gift_card_req_id", OracleDbType.Raw, 16, giftCardRequestId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_gift_card_url_encrypted", OracleDbType.Varchar2, encryptedGiftCardLink, ParameterDirection.Input)
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()

                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End Using
        End Using
    End Sub

    Public sub UpdateGiftCardStatus(giftCardRequestId As guid, status as string)

        Dim selectStmt As String = Config("/SQL/UPDATE_GIFT_CARD_STATUS")
        Dim strTemp as String
     
        
        Using connection As New OracleConnection(ConnectString)
            Using command as OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, connection) 
                command.BindByName = True
                command.AddParameter("pi_gift_card_request_id", OracleDbType.Raw,16, giftCardRequestId.ToByteArray, ParameterDirection.Input)
                command.AddParameter("pi_gift_card_status", OracleDbType.Varchar2, 100, status, ParameterDirection.Input)
                             
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()                   

                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)                
                End Try
            end using            
        End Using
    End sub
    #End Region

End Class


