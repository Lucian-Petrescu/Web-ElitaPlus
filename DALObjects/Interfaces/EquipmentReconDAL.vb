Imports Assurant.ElitaPlus.DALObjects.DBHelper

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/13/2004)********************


Public Class EquipmentReconDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "equipment_recon_wrk_id"
    Public Const COL_NAME_EQUIPMENT_RECON_WRK_ID As String = "equipment_recon_wrk_id"
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_IS_MASTER As String = "is_master"
    Public Const COL_NAME_MASTER_EQUIPMENT_DESCRIPTION As String = "master_equipment_description"
    Public Const COL_NAME_EQUIPMENT_CLASS As String = "equipment_class"
    Public Const COL_NAME_EQUIPMENT_TYPE As String = "equipment_type"
    Public Const COL_NAME_MANUFACTURER_WARRANTY As String = "manufacturer_warranty"
    Public Const COL_NAME_ATTRIBUTES As String = "attributes"
    Public Const COL_NAME_NOTE1 As String = "note1"
    Public Const COL_NAME_NOTE2 As String = "note2"
    Public Const COL_NAME_NOTE3 As String = "note3"
    Public Const COL_NAME_NOTE4 As String = "note4"
    Public Const COL_NAME_NOTE5 As String = "note5"
    Public Const COL_NAME_NOTE6 As String = "note6"
    Public Const COL_NAME_NOTE7 As String = "note7"
    Public Const COL_NAME_NOTE8 As String = "note8"
    Public Const COL_NAME_NOTE9 As String = "note9"
    Public Const COL_NAME_NOTE10 As String = "note10"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_date"
    Public Const COL_NAME_LOAD_STATUS As String = "load_status"
    Public Const COL_NAME_REPAIRABLE As String = "Repairable"

    'for reject message translation
    Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_REJECT_REASON As String = "reject_reason"
    Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"

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
        Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("equipment_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(fileProcessedID As Guid, languageID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                                            New OracleParameter(COL_NAME_FILE_PROCESSED_ID, fileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadRejectList(fileProcessedID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_REJECT_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_FILE_PROCESSED_ID, fileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Equipment Recon Work"

    Public Sub ValidateFile(fileProcessedId As Guid, interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/VALIDATE_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFile(fileProcessedId As Guid, interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/PROCESS_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(fileProcessedId As Guid, interfaceStatusId As Guid)
        Dim sqlStmt As String = Config("/SQL/DELETE_FILE")
        Dim dal As New FileProcessedDAL
        Try
            dal.ExecuteSP(fileProcessedId, interfaceStatusId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region


End Class
