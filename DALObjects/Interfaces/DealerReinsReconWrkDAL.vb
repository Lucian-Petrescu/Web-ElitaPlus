Public Class DealerReinsReconWrkDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_DEALER_REINS_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = COL_NAME_DEALER_REINS_RECON_WRK_ID

    Public Const COL_NAME_DEALER_REINS_RECON_WRK_ID As String = "dealer_reins_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_REINSURANCE_LOADED As String = "reinsurance_loaded"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_RISK_TYPE_ENGLISH As String = "risk_type_english"
    Public Const COL_NAME_COVERAGE_TYPE_CODE As String = "coverage_type_code"
    Public Const COL_NAME_REINSURANCE_REJECT_REASON As String = "reinsurance_reject_reason"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"

    Public Const PAR_I_NAME_DEALER_REINS_RECON_WRK_ID As String = "pi_dealer_reins_recon_wrk_id"
    Public Const PAR_I_NAME_DEALERFILE_PROCESSED_ID As String = "pi_dealerfile_processed_id"
    Public Const PAR_I_NAME_REINSURANCE_LOADED As String = "pi_reinsurance_loaded"
    Public Const PAR_I_NAME_RECORD_TYPE As String = "pi_record_type"
    Public Const PAR_I_NAME_CERTIFICATE As String = "pi_certificate"
    Public Const PAR_I_NAME_REJECT_REASON As String = "pi_reject_reason"
    Public Const PAR_I_NAME_REJECT_CODE As String = "pi_reject_code"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_PRODUCT_CODE As String = "pi_product_code"
    Public Const PAR_I_NAME_RISK_TYPE_ENGLISH As String = "pi_risk_type_english"
    Public Const PAR_I_NAME_COVERAGE_TYPE_CODE As String = "pi_coverage_type_code"
    Public Const PAR_I_NAME_REINSURANCE_REJECT_REASON As String = "pi_reinsurance_reject_reason"
    Public Const PAR_I_NAME_ENTIRE_RECORD As String = "pi_entire_record"

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
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(dealerfileProcessedID As Guid, recordMode As String
                                      ) As DataSet

        Dim selectStmt As String
        Dim parameters() As OracleParameter

        selectStmt = Config("/SQL/LOAD_LIST")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray),
                                                New OracleParameter("p_recordMode", recordMode)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadRejectList(dealerfileProcessedID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_REJECT_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_DEALER_REINS_RECON_WRK_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_REINS_RECON_WRK_ID)
            .AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALERFILE_PROCESSED_ID)
            .AddParameter(PAR_I_NAME_REINSURANCE_LOADED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REINSURANCE_LOADED)
            .AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RECORD_TYPE)
            .AddParameter(PAR_I_NAME_CERTIFICATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE)
            .AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_REASON)
            .AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_CODE)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_RISK_TYPE_ENGLISH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RISK_TYPE_ENGLISH)
            .AddParameter(PAR_I_NAME_COVERAGE_TYPE_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_COVERAGE_TYPE_CODE)
            .AddParameter(PAR_I_NAME_REINSURANCE_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REINSURANCE_REJECT_REASON)
            .AddParameter(PAR_I_NAME_ENTIRE_RECORD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ENTIRE_RECORD)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_DEALER_REINS_RECON_WRK_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_REINS_RECON_WRK_ID)
            .AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALERFILE_PROCESSED_ID)
            .AddParameter(PAR_I_NAME_REINSURANCE_LOADED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REINSURANCE_LOADED)
            .AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RECORD_TYPE)
            .AddParameter(PAR_I_NAME_CERTIFICATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE)
            .AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_REASON)
            .AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_CODE)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_RISK_TYPE_ENGLISH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RISK_TYPE_ENGLISH)
            .AddParameter(PAR_I_NAME_COVERAGE_TYPE_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_COVERAGE_TYPE_CODE)
            .AddParameter(PAR_I_NAME_REINSURANCE_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REINSURANCE_REJECT_REASON)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region

End Class

