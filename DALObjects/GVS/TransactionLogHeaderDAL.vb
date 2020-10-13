'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransactionLogHeaderDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANSACTION_LOG_HEADER"
    Public Const TABLE_KEY_NAME As String = "transaction_log_header_id"
    Public Const STATUS_TABLE_NAME As String = "TRANSACTION_STATUS"
    Public Const PART_TABLE_NAME As String = "TRANSACTION_PART"
    Public Const FOLLOWUP_TABLE_NAME As String = "TRANSACTION_FOLLOWUP"
    Public Const TRANSACTION_DATA_TABLE_NAME As String = "TRANSACTION_DATA"

    Public Const COL_NAME_TRANSACTION_LOG_HEADER_ID As String = "transaction_log_header_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_FUNCTION_TYPE_ID As String = "function_type_id"
    Public Const COL_NAME_TRANSACTION_XML As String = "transaction_xml"
    Public Const COL_NAME_TRANSACTION_PROCESSED_DATE As String = "transaction_processed_date"
    Public Const COL_NAME_TRANSACTION_STATUS_ID As String = "transaction_status_id"
    Public Const COL_NAME_GVS_ORIGINAL_TRANS_NO As String = "gvs_original_trans_no"
    Public Const COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID As String = "original_trans_log_hdr_id"
    Public Const COL_NAME_KEY_ID As String = "key_id"
    Public Const COL_NAME_HIDE As String = "hide"
    Public Const COL_NAME_RESEND As String = "resend"
    Public Const COL_NAME_OUT_TRANS_GVS_RESPONSED As String = "out_trans_gvs_response"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_TRANSACTION_DATE As String = "transaction_date"
    Public Const COL_NAME_ERROR_CODE As String = "error_code"


    Public Const TRANSACTION_STATUS_NEW As String = "N"
    Public Const FUNCTION_TYPE_CODE_GVS_NEW_CLAIM As String = "1"
    Public Const FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM As String = "2"
    Public Const FUNCTION_TYPE_CODE_GVS_UPDATE_SVC As String = "3"
    Public Const FUNCTION_TYPE_CODE_GVS_TRANSACTION_UPDATE As String = "4"
    Public Const FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM As String = "5"
    Public Const FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION As String = "6"
    Public Const FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE As String = "7"
    Public Const FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM As String = "8"

    Public Const FUNCTION_TYPE_NEW_CLAIM As String = "NEW_CLAIM"
    Public Const FUNCTION_TYPE_UPDATE_CLAIM As String = "UPDATE_CLAIM"
    Public Const FUNCTION_TYPE_UPDATE_SVC As String = "UPDATE_SVC"
    Public Const FUNCTION_TYPE_TRANSACTION_UPDATE As String = "TRANSACTION_UPDATE"

    Public Const CMD_RESEND As String = "1"
    Public Const CMD_HIDE As String = "2"
    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const PAR_NAME_CMD As String = "p_cmd"
    Public Const PAR_NAME_TRANS_ID As String = "p_trans_log_header_id"
    Public Const PAR_NAME_NEW_COMUNA As String = "p_new_comuna"
    Public Const PAR_NAME_NEW_SERVICE_TYPE As String = "p_new_service_type"

    Public Const PAR_NAME_TRANS_IDS As String = "p_trans_log_header_ids"
    Public Const PAR_NAME_LANGUAGE_ID As String = "p_language_id"
    Public Const PAR_NAME_TRANS_DETAIL_REC As String = "p_trans_detail_rec"
    Public Const PAR_NAME_TRANS_LIST As String = "p_trans_list"
    Public Const PAR_NAME_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const PAR_NAME_CLAIM_NUMBER As String = "p_claim_number"
    Public Const PAR_NAME_AUTH_NUMBER As String = "p_auth_number"
    Public Const PAR_NAME_SVC_CODE As String = "p_svc_code"
    Public Const PAR_NAME_TRANS_DATE_FROM As String = "p_trans_date_from"
    Public Const PAR_NAME_TRANS_DATE_TO As String = "p_trans_date_to"
    Public Const PAR_NAME_ERROR_CODE As String = "p_error_code"
    Public Const PAR_NAME_NUM_REC As String = "p_num_rec"
    Public Const PAR_NAME_ROW_NUMBER As String = "row_num"

    Public Const COL_NAME_TRANSACTION_STATUS_SYSTEM As String = "System"
    Public Const COL_NAME_TRANSACTION_STATUS_DATE As String = "transmission_date"

    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("transaction_log_header_id", id.ToByteArray)}
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


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            'MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
            MyBase.UpdateWithParam(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Function IsTransactionExist(gvsOriginalTransID As String) As DataSet
        Dim ds As DataSet = New DataSet("IS_TRANSACTION_EXIST")
        Dim selectStmt As String = Config("/SQL/IS_TRANSACTION_EXIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("gvs_original_trans_no", gvsOriginalTransID)}
        Try

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetTransactionExceptionHeader(compGroupId As Guid) As DataSet
        Dim ds As DataSet = New DataSet("TRANSACTION_EXCEPTION_MANAGEMENT_HEADER")
        Dim selectStmt As String = Config("/SQL/TRANSACTION_EXCEPTION_MANAGEMENT_HEADER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, compGroupId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLastSuccessfulTransmissionTime(compGroupId As Guid) As DateTimeType
        Dim ds As DataSet
        Dim selectStmt As String = Config("/SQL/LAST_SUCCESSFUL_TRANSMISSION_TIME")
        Try
            selectStmt = selectStmt.Replace("--replace_company_group_id", " AND h.company_group_id = hextoraw('" & GuidControl.GuidToHexString(compGroupId) & "') ")
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 _
                AndAlso ds.Tables(0).Rows.Count > 0 _
                AndAlso ds.Tables(0).Rows(0)(0) IsNot DBNull.Value Then
                Return CType(ds.Tables(0).Rows(0)(0), DateTime)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLastSuccessfulTransmissionTimeByType(compGroupId As Guid) As DataView
        Dim ds As DataSet
        Dim selectStmt As String = Config("/SQL/LAST_SUCCESSFUL_TRANSMISSION_TIME_BY_TYPE")
        Try
            selectStmt = selectStmt.Replace("--replace_company_group_id", " AND h.company_group_id = hextoraw('" & GuidControl.GuidToHexString(compGroupId) & "') ")
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Update(familyDataset, tr, DataRowState.Deleted)
            Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Sub InsertCustom(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim conn As OracleConnection = DBHelper.GetConnection
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction(conn)
        Dim selectStmt As String = Config("/SQL/INSERT")

        Try
            For Each dr As DataRow In familyDataset.Tables(0).Rows

                Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CType(dr(COL_NAME_COMPANY_GROUP_ID), Byte())), _
                                    New DBHelper.DBHelperParameter(COL_NAME_FUNCTION_TYPE_ID, CType(dr(COL_NAME_FUNCTION_TYPE_ID), Byte())), _
                                    New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_XML, dr(COL_NAME_TRANSACTION_XML).ToString, GetType(System.Text.StringBuilder)), _
                                    New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_PROCESSED_DATE, IIf(IsDate(dr(COL_NAME_TRANSACTION_PROCESSED_DATE)), dr(COL_NAME_TRANSACTION_PROCESSED_DATE), Date.MinValue)), _
                                    New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_STATUS_ID, CType(dr(COL_NAME_TRANSACTION_STATUS_ID), Byte())), _
                                    New DBHelper.DBHelperParameter(COL_NAME_CREATED_BY, CType(dr(COL_NAME_CREATED_BY), String)), _
                                    New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_LOG_HEADER_ID, CType(dr(COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte())), _
                                    New DBHelper.DBHelperParameter(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID, IIf(IsDBNull(dr(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID)), Guid.Empty, dr(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID))), _
                                    New DBHelper.DBHelperParameter(COL_NAME_GVS_ORIGINAL_TRANS_NO, IIf(IsDBNull(dr(COL_NAME_GVS_ORIGINAL_TRANS_NO)), "", dr(COL_NAME_GVS_ORIGINAL_TRANS_NO))), _
                                    New DBHelper.DBHelperParameter(COL_NAME_KEY_ID, IIf(IsDBNull(dr(COL_NAME_KEY_ID)), Guid.Empty, dr(COL_NAME_KEY_ID))), _
                                    New DBHelper.DBHelperParameter(COL_NAME_HIDE, IIf(IsDBNull(dr(COL_NAME_HIDE)), "", dr(COL_NAME_HIDE))), _
                                    New DBHelper.DBHelperParameter(COL_NAME_RESEND, IIf(IsDBNull(dr(COL_NAME_RESEND)), "", dr(COL_NAME_RESEND)))}

                DBHelper.ExecuteWithParam(selectStmt, parameters, tr)
            Next

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Sub UpdateCustom(dr As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim conn As OracleConnection = DBHelper.GetConnection
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction(conn)
        Dim selectStmt As String = Config("/SQL/UPDATE")

        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CType(dr(COL_NAME_COMPANY_GROUP_ID), Byte())), _
                                New DBHelper.DBHelperParameter(COL_NAME_FUNCTION_TYPE_ID, CType(dr(COL_NAME_FUNCTION_TYPE_ID), Byte())), _
                                New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_XML, dr(COL_NAME_TRANSACTION_XML).ToString, GetType(System.Text.StringBuilder)), _
                                New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_PROCESSED_DATE, IIf(IsDate(dr(COL_NAME_TRANSACTION_PROCESSED_DATE)), dr(COL_NAME_TRANSACTION_PROCESSED_DATE), Date.MinValue)), _
                                New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_STATUS_ID, CType(dr(COL_NAME_TRANSACTION_STATUS_ID), Byte())), _
                                New DBHelper.DBHelperParameter(COL_NAME_MODIFIED_BY, CType(dr(COL_NAME_MODIFIED_BY), String)), _
                                New DBHelper.DBHelperParameter(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID, IIf(IsDBNull(dr(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID)), Guid.Empty, dr(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID))), _
                                New DBHelper.DBHelperParameter(COL_NAME_GVS_ORIGINAL_TRANS_NO, IIf(IsDBNull(dr(COL_NAME_GVS_ORIGINAL_TRANS_NO)), "", dr(COL_NAME_GVS_ORIGINAL_TRANS_NO))), _
                                New DBHelper.DBHelperParameter(COL_NAME_KEY_ID, IIf(IsDBNull(dr(COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID)), Guid.Empty, dr(COL_NAME_KEY_ID))), _
                                New DBHelper.DBHelperParameter(COL_NAME_HIDE, IIf(IsDBNull(dr(COL_NAME_HIDE)), "", dr(COL_NAME_HIDE))), _
                                New DBHelper.DBHelperParameter(COL_NAME_RESEND, IIf(IsDBNull(dr(COL_NAME_RESEND)), "", dr(COL_NAME_RESEND))), _
                                New DBHelper.DBHelperParameter(COL_NAME_OUT_TRANS_GVS_RESPONSED, IIf(IsDBNull(dr(COL_NAME_OUT_TRANS_GVS_RESPONSED)), "", dr(COL_NAME_OUT_TRANS_GVS_RESPONSED))), _
                                New DBHelper.DBHelperParameter(COL_NAME_TRANSACTION_LOG_HEADER_ID, CType(dr(COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte()))}

            DBHelper.ExecuteWithParam(selectStmt, parameters, tr)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
            End If

        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Function GetExceptionList(compGroupId As Guid, claimNumber As String, authNumber As String, svcCode As String, transDateFrom As Date, transDateTo As Date, errorCode As String, Optional ByVal transLogHeaderId As String = "") As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As DataSet = New DataSet
        Dim whereClauseConditions As String = ""

        Dim parameters(1) As DBHelper.DBHelperParameter


        parameters(0) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, compGroupId.ToByteArray, GetType(Byte()))
        parameters(1) = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, 101, GetType(Integer))

        If FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_CLAIM_NUMBER & ") " & claimNumber.ToUpper
        End If

        If FormatSearchMask(authNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_AUTHORIZATION_NUMBER & ") " & authNumber.ToUpper
        End If

        If FormatSearchMask(svcCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_SERVICE_CENTER_CODE & ") " & svcCode.ToUpper
        End If

        If FormatSearchMask(errorCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_ERROR_CODE & ") " & errorCode.ToUpper
        End If

        If Not (Date.MinValue.Equals(transDateFrom)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(" & COL_NAME_TRANSACTION_DATE & ")" & " >=  to_date('" & transDateFrom.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        End If

        If Not (Date.MinValue.Equals(transDateTo)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(" & COL_NAME_TRANSACTION_DATE & ")" & " <=  to_date('" & transDateTo.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            DBHelper.Fetch(ds, selectStmt, TRANSACTION_DATA_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetStatusList(transactionLogHeaderId As String, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/TRANSACTION_STATUS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter(DALBase.PAR_NAME_ROW_NUMBER, DALBase.MAX_NUMBER_OF_ROWS, GetType(Integer))}
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet = New DataSet

        selectStmt = selectStmt.Replace("--dynamic_language_id", "'" & GuidControl.GuidToHexString(languageId) & "'")
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND transaction_log_header_id = hextoraw('" & transactionLogHeaderId & "') ")

        Try
            DBHelper.Fetch(ds, selectStmt, STATUS_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetPartList(transactionLogHeaderId As String) As DataSet
        Dim selectStmt As String = Config("/SQL/TRANSACTION_PART")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(DALBase.PAR_NAME_ROW_NUMBER, DALBase.MAX_NUMBER_OF_ROWS, GetType(Integer))}
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet = New DataSet

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND transaction_log_header_id = hextoraw('" & transactionLogHeaderId & "') ")

        Try
            DBHelper.Fetch(ds, selectStmt, PART_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetFollowUpList(transactionLogHeaderId As String, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/TRANSACTION_FOLLOWUP")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(DALBase.PAR_NAME_ROW_NUMBER, DALBase.MAX_NUMBER_OF_ROWS, GetType(Integer))}
        Dim whereClauseConditions As String = ""
        Dim ds As DataSet = New DataSet

        selectStmt = selectStmt.Replace("--dynamic_language_id", "'" & GuidControl.GuidToHexString(languageId) & "'")
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND transaction_log_header_id = hextoraw('" & transactionLogHeaderId & "') ")

        Try
            DBHelper.Fetch(ds, selectStmt, FOLLOWUP_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetTransactionData(transactionLogHeaderId As String, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/TRANSACTION_DATA")
        Dim ds As DataSet = New DataSet

        Dim parameters(1) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PAR_NAME_TRANS_DETAIL_REC, GetType(DataSet))}

        parameters(0) = New DBHelper.DBHelperParameter(PAR_NAME_TRANS_ID, GuidControl.HexToByteArray(transactionLogHeaderId))
        parameters(1) = New DBHelper.DBHelperParameter(PAR_NAME_LANGUAGE_ID, languageId.ToByteArray)

        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, TRANSACTION_DATA_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD")
    'End Sub

    Public Function ResendOrHideTransaction(cmd As String, transLogHeaderId As Guid, Optional ByVal newComunaValue As String = Nothing, Optional ByVal newServiceTypeValue As String = Nothing) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Config("/SQL/RESEND_OR_HIDE_TRANSACTION")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_CMD, cmd), _
                            New DBHelper.DBHelperParameter(PAR_NAME_TRANS_ID, transLogHeaderId.ToByteArray), _
                            New DBHelper.DBHelperParameter(PAR_NAME_NEW_COMUNA, newComunaValue), _
                            New DBHelper.DBHelperParameter(PAR_NAME_NEW_SERVICE_TYPE, newServiceTypeValue)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(PAR_NAME_EXCEPTION_MSG, GetType(String))}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Return outputParameters

    End Function

    Public Function ProcessRecords(cmd, transLogHeaderIds) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Config("/SQL/PROCESS_RECORDS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_CMD, cmd), _
                            New DBHelper.DBHelperParameter(PAR_NAME_TRANS_IDS, transLogHeaderIds)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(PAR_NAME_EXCEPTION_MSG, GetType(String))}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Return outputParameters
    End Function

    Public Function GetRejectionMessage(compGroupId As Guid, transLogHeaderId As Guid) As String
        Dim retVal As String = String.Empty
        Dim ds As DataSet = New DataSet

        Try
            ds = GetExceptionList(compGroupId, Nothing, Nothing, Nothing, Date.MinValue, Date.MinValue, Nothing, GuidControl.GuidToHexString(transLogHeaderId))

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                retVal = If(ds.Tables(0).Rows(0)("error_code") Is DBNull.Value, "", CType(ds.Tables(0).Rows(0)("error_code"), String))
            End If

            Return retVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'Public Function UpdateComuna(ByVal transLogHeaderId As Guid, ByVal newComunaValue As String) As Boolean
    '    Dim selectStmt As String = Me.Config("/SQL/UPDATE_COMUNA")
    '    Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    '                                                    New DBHelper.DBHelperParameter(PAR_NAME_TRANS_ID, transLogHeaderId.ToByteArray), _
    '                                                    New DBHelper.DBHelperParameter(PAR_NAME_NEW_COMUNA, newComunaValue)}

    '    Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    '                        New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)), _
    '                        New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String))}

    '    ' Call DBHelper Store Procedure
    '    DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

    '    If outputParameters(Me.P_RETURN).Value <> 0 Then
    '        Throw New StoredProcedureGeneratedException("ELP_GVS_TRANSACTION.UpdateComuna Error: ", outputParameters(Me.P_EXCEPTION_MSG).Value)
    '    Else
    '        Return True
    '    End If

    'End Function
#End Region


End Class


