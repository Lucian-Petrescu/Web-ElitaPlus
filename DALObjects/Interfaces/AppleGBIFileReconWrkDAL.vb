Imports Assurant.ElitaPlus.DALObjects.DBHelper
Public Class AppleGBIFileReconWrkDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "elp_ben_gbiclaim_queue"
    Public Const TABLE_KEY_NAME As String = "ben_gbiclaim_queue_id"

    Public Const PAR_I_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_FROM_DATE As String = "pi_from_date"
    Public Const PAR_I_THRUE_DATE As String = "pi_thru_date"
    Public Const PAR_O_NAME_RESULTCURSOR As String = "po_ResultCursor"

    Public Const PAR_I_FILE_PROCESSED_ID As String = "pi_file_processed_id"
    Public Const PAR_I_STATUS As String = "pi_status"
    Public Const PAR_I_LANGUAGE_ID As String = "pi_language_id"


    Public Const COL_NAME_FILE_PROCESSED_ID As String = "FILE_PROCESSED_ID"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_PROCESSED As String = "processed"
    Public Const COL_NAME_CANCELLED As String = "CANCELLED"
    Public Const COL_NAME_PENDING_VALIDATION As String = "PENDING_VALIDATION"
    Public Const COL_NAME_FAILED_REPROCESS As String = "FAILED_REPROCESS"
    Public Const COL_NAME_PENDING_CLAIM_CREATION As String = "PENDING_CLAIM_CREATION"


    Public Const COL_NAME_DET_BEN_GBICLAIM_QUEUE_ID As String = "ben_gbiclaim_queue_id"
    REM Public Const COL_NAME_DET_FILE_PROCESSED_ID As String = "file_processed_id"
    REM Public Const COL_NAME_DET_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_DET_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_DET_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_DET_RECORD_STATUS As String = "record_status"
    REM Public Const COL_NAME_DET_SEQUENCE As String = "sequence"
    REM Public Const COL_NAME_DET_FILE_DATE As String = "file_date"
    REM Public Const COL_NAME_DET_DEALER_CODE As String = "dealer_code"
    REM Public Const COL_NAME_DET_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DET_CUSTOMER_ID As String = "customer_id"
    Public Const COL_NAME_DET_SHIP_TO_ID As String = "ship_to_id"
    Public Const COL_NAME_DET_AGREEMENT_ID As String = "agreement_id"
    Public Const COL_NAME_DET_UNIQUE_IDENTIFIER As String = "unique_identifier"
    Public Const COL_NAME_DET_ORIGINAL_SERIAL_NUMBER As String = "original_serial_number"
    Public Const COL_NAME_DET_ORIGINAL_IMEI_NUMBER As String = "original_imei_number"
    Public Const COL_NAME_DET_NEW_SERIAL_NUMBER As String = "new_serial_number"
    Public Const COL_NAME_DET_NEW_IMEI_NUMBER As String = "new_imei_number"
    Public Const COL_NAME_DET_REPAIR_COMPLETION_DATE As String = "repair_completion_date"
    Public Const COL_NAME_DET_CLAIM_TYPE As String = "claim_type"
    Public Const COL_NAME_DET_CHANNEL As String = "channel"
    Public Const COL_NAME_DET_INCIDENT_FEE As String = "incident_fee"
    Public Const COL_NAME_DET_NOTIF_CREATE_DATE As String = "notif_create_date"
    Public Const COL_NAME_DET_REPAIR_COMPLETED As String = "repair_completed"
    Public Const COL_NAME_DET_REPAIR_COMPLETED_DATE As String = "repair_completed_date"
    Public Const COL_NAME_DET_CLAIM_CANCELLED As String = "claim_cancelled"
    Public Const COL_NAME_DET_DESCRIPTION As String = "description"
    Public Const COL_NAME_DET_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_DET_CREATED_BY As String = "created_by"
    Public Const COL_NAME_DET_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"

    Public Const COL_NAME_RETURN As String = "po_return"




#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, id.ToByteArray)}
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

    Public Function LoadSummary(FromDate As Date,
                                ThruDate As Date) As DataSet

        Try

            Dim strCommand As String = Config("/SQL/SUMMARY")
            Using connection As New OracleConnection(DBHelper.ConnectString)
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(strCommand, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True
                    cmd.AddParameter(PAR_I_FROM_DATE, OracleDbType.Varchar2, 20, FromDate.ToString("MM/dd/yyyy"))
                    cmd.AddParameter(PAR_I_THRUE_DATE, OracleDbType.Varchar2, 20, ThruDate.ToString("MM/dd/yyyy"))
                    cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                    Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
                End Using
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadDetail(FileProcessedId As Guid,
                               Status As String,
                               LanguageId As Guid) As DataSet

        Try

            Dim strCommand As String = Config("/SQL/DETAIL")
            Using connection As New OracleConnection(DBHelper.ConnectString)
                Using cmd As OracleCommand = OracleDbHelper.CreateCommand(strCommand, CommandType.StoredProcedure, connection)
                    cmd.BindByName = True

                    cmd.AddParameter(PAR_I_FILE_PROCESSED_ID, OracleDbType.Raw, 20, FileProcessedId.ToByteArray)
                    cmd.AddParameter(PAR_I_STATUS, OracleDbType.Varchar2, 20, Status)
                    cmd.AddParameter(PAR_I_LANGUAGE_ID, OracleDbType.Raw, 20, LanguageId.ToByteArray)

                    cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                    Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
                End Using
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub ProcessFile(fileProcessedId As Guid)
        Dim sqlStmt As String = Config("/SQL/PROCESS_CLAIM_FILE")

        Dim inputParameters(0) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter
        Try

            inputParameters(0) = New DBHelperParameter(PAR_I_FILE_PROCESSED_ID, fileProcessedId.ToByteArray())
            outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(sqlStmt, inputParameters, outputParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub
#End Region
#Region "Overloaded Methods"

#End Region
End Class
