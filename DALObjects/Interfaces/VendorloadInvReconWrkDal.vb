'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (12/22/2017)********************


Public Class VendorloadInvReconWrkDal
    Inherits OracleDALBase

#Region "Constants"
    Private Const SupportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TableNameDal As String = "ELP_VENDORLOAD_INV_RECON_WRK"
    Public Const TableKeyName As String = ColNameVendorloadInvReconWrkId

    Public Const ColNameVendorloadInvReconWrkId As String = "vendorload_inv_recon_wrk_id"
    Public Const ColNameFileProcessedId As String = "file_processed_id"
    Public Const ColNameRecordType As String = "record_type"
    Public Const ColNameRejectCode As String = "reject_code"
    Public Const ColNameRejectReason As String = "reject_reason"
    Public Const ColNameRecordLoaded As String = "record_loaded"
    Public Const ColNameServiceCenterId As String = "service_center_id"
    Public Const ColNameVendorSku As String = "vendor_sku"
    Public Const ColNameInventoryQuantity As String = "inventory_quantity"
    Public Const ColNamePriceListDetailId As String = "price_list_detail_id"
    Public Const ColNameEntireRecord As String = "entire_record"

    Public Const ParINameVendorloadInvReconWrkId As String = "pi_vendorload_inv_recon_wrk_id"
    Public Const ParINameFileProcessedId As String = "pi_file_processed_id"
    Public Const ParINameRecordMode As String = "pi_record_mode"
    Public Const ParINameRecordType As String = "pi_record_type"
    Public Const ParINameRejectCode As String = "pi_reject_code"
    Public Const ParINameRejectReason As String = "pi_reject_reason"
    Public Const ParINameRecordLoaded As String = "pi_record_loaded"
    Public Const ParINameServiceCenterId As String = "pi_service_center_id"
    Public Const ParINameVendorSku As String = "pi_vendor_sku"
    Public Const ParINameInventoryQuantity As String = "pi_inventory_quantity"
    Public Const ParINamePriceListDetailId As String = "pi_price_list_detail_id"
    Public Const ParINameEntireRecord As String = "pi_entire_record"
    Public Const ParINamePageIndex As String = "pi_page_index"
    Public Const ParINamePageSize As String = "pi_page_size"

    Public Const ParINameSortExpression As String = "pi_sort_expression"

    'for reject message translation
    'Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    'Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const ColMsgParameterCount As String = "MSG_PARAMETER_COUNT"
    Public Const ColRejectReason As String = "reject_reason"
    Public Const ColTranslatedMsg As String = "Translated_MSG"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TableKeyName, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TableNameDal, familyDs)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Try
    '        Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
    '            cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
    '            Return Fetch(cmd, TableName)
    '        End Using
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function
    'Public Function LoadList(ByVal fileProcessedID As Guid,
    '                            ByVal languageID As Guid,
    '                            ByVal recordMode As String,
    '                            ByVal recordType As String,
    '                            ByVal rejectCode As String,
    '                            ByVal rejectReason As String,
    '                            ByVal pageindex As Integer,
    '                            ByVal pagesize As Integer,
    '                            ByVal sortExpression As String) As DataSet
    '    Try
    '        rejectReason = FormatWildCard(rejectReason)

    '        Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
    '            cmd.AddParameter(ParINameFileProcessedId, OracleDbType.Raw, fileProcessedID.ToByteArray())
    '            cmd.AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, languageID.ToByteArray())
    '            cmd.AddParameter(ParINameRecordMode, OracleDbType.Varchar2, recordMode)
    '            cmd.AddParameter(ParINameRecordType, OracleDbType.Varchar2, recordType)
    '            cmd.AddParameter(ParINameRejectCode, OracleDbType.Varchar2, rejectCode)
    '            cmd.AddParameter(ParINameRejectReason, OracleDbType.Varchar2, rejectReason)
    '            cmd.AddParameter(ParINamePageIndex, OracleDbType.Int64, value:=pageindex)
    '            cmd.AddParameter(ParINamePageSize, OracleDbType.Int64, value:=pagesize)
    '            cmd.AddParameter(ParINameSortExpression, OracleDbType.Varchar2, value:=sortExpression)
    '            cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
    '            Return OracleDbHelper.Fetch(cmd, TableName)
    '        End Using
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function
    Public Function LoadList(ByVal fileProcessedId As Guid, ByVal languageId As Guid, ByVal recMode As Integer,
                             ByVal recordType As String, ByVal rejectCode As String, ByVal rejectReason As String,
                             ByVal pageindex As Integer, ByVal pagesize As Integer, ByVal sortExpression As String) As DataSet
        Try
            rejectReason = FormatWildCard(rejectReason)
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(ParINameFileProcessedId, OracleDbType.Raw, fileProcessedId.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, languageId.ToByteArray())
                cmd.AddParameter(ParINameRecordMode, OracleDbType.Int64, value:=recMode)
                cmd.AddParameter(ParINameRecordType, OracleDbType.Varchar2, recordType)
                cmd.AddParameter(ParINameRejectCode, OracleDbType.Varchar2, rejectCode)
                cmd.AddParameter(ParINameRejectReason, OracleDbType.Varchar2, rejectReason)
                cmd.AddParameter(ParINamePageIndex, OracleDbType.Int64, value:=pageindex)
                cmd.AddParameter(ParINamePageSize, OracleDbType.Int64, value:=pagesize)
                cmd.AddParameter(ParINameSortExpression, OracleDbType.Varchar2, value:=sortExpression)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TableNameDal)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As OracleTransaction = Nothing, Optional ByVal changesFilter As DataRowState = SupportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (SupportChangesFilter)) <> (SupportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TableNameDal) Is Nothing Then
            Update(ds.Tables(TableNameDal), transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(ParINameVendorloadInvReconWrkId, OracleDbType.Raw, sourceColumn:=ColNameVendorloadInvReconWrkId)
            .AddParameter(ParINameFileProcessedId, OracleDbType.Raw, sourceColumn:=ColNameFileProcessedId)
            .AddParameter(ParINameRecordType, OracleDbType.Varchar2, sourceColumn:=ColNameRecordType)
            .AddParameter(ParINameRejectCode, OracleDbType.Varchar2, sourceColumn:=ColNameRejectCode)
            .AddParameter(ParINameRejectReason, OracleDbType.Varchar2, sourceColumn:=ColNameRejectReason)
            .AddParameter(ParINameRecordLoaded, OracleDbType.Varchar2, sourceColumn:=ColNameRecordLoaded)
            .AddParameter(ParINameServiceCenterId, OracleDbType.Raw, sourceColumn:=ColNameServiceCenterId)
            .AddParameter(ParINameVendorSku, OracleDbType.Varchar2, sourceColumn:=ColNameVendorSku)
            .AddParameter(ParINameInventoryQuantity, OracleDbType.Decimal, sourceColumn:=ColNameInventoryQuantity)
            .AddParameter(ParINamePriceListDetailId, OracleDbType.Raw, sourceColumn:=ColNamePriceListDetailId)
            .AddParameter(ParINameEntireRecord, OracleDbType.Varchar2, sourceColumn:=ColNameEntireRecord)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(ParINameVendorloadInvReconWrkId, OracleDbType.Raw, sourceColumn:=ColNameVendorloadInvReconWrkId)
            .AddParameter(ParINameFileProcessedId, OracleDbType.Raw, sourceColumn:=ColNameFileProcessedId)
            .AddParameter(ParINameRecordType, OracleDbType.Varchar2, sourceColumn:=ColNameRecordType)
            .AddParameter(ParINameRejectCode, OracleDbType.Varchar2, sourceColumn:=ColNameRejectCode)
            .AddParameter(ParINameRejectReason, OracleDbType.Varchar2, sourceColumn:=ColNameRejectReason)
            .AddParameter(ParINameRecordLoaded, OracleDbType.Varchar2, sourceColumn:=ColNameRecordLoaded)
            .AddParameter(ParINameServiceCenterId, OracleDbType.Raw, sourceColumn:=ColNameServiceCenterId)
            .AddParameter(ParINameVendorSku, OracleDbType.Varchar2, sourceColumn:=ColNameVendorSku)
            .AddParameter(ParINameInventoryQuantity, OracleDbType.Decimal, sourceColumn:=ColNameInventoryQuantity)
            .AddParameter(ParINamePriceListDetailId, OracleDbType.Raw, sourceColumn:=ColNamePriceListDetailId)
            .AddParameter(ParINameEntireRecord, OracleDbType.Varchar2, sourceColumn:=ColNameEntireRecord)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region
#Region "Vendor Inventory Recon Work"

    Public Sub ValidateFile(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/VALIDATE_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFile(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/PROCESS_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/DELETE_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
End Class


