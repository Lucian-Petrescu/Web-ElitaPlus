'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/2/2010)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class ClaimloadFileProcessedDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIMLOAD_FILE_PROCESSED"
    Public Const TABLE_KEY_NAME As String = "claimload_file_processed_id"

    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_ACCOUNT_IDENTIFIER As String = "account_identifier"
    Public Const COL_NAME_SC_COUNTRY_CODE As String = "sc_country_code"
    Public Const COL_NAME_FILE_TYPE As String = "file_type"

    Public Const TOTAL_PARAM_SP = 1
    Public Const PARAM_FILENAME = 0
    Public Const PARAM_INTERFACE_STATUS_ID = 1
#End Region

#Region "ClaimLoadFileProcessedData class definition"
    Public Class ClaimLoadFileProcessedData
        Public interfaceStatus_id As Guid
        Public filename As String
    End Class
#End Region

#Region "Delegate Signatures"
    Public Delegate Sub AsyncCaller(ByVal oFileProcessedData As ClaimLoadFileProcessedData, ByVal selectStmt As String)
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claimload_file_processed_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal userId As Guid, ByVal countryCode As String, ByVal fileType As String, ByVal fileName As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        OracleDbHelper.AddParameter(cmd, "pi_user_id", OracleDbType.Raw, userId.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "pi_country_code", OracleDbType.Varchar2, countryCode)
        OracleDbHelper.AddParameter(cmd, "pi_file_type", OracleDbType.Varchar2, fileType)
        OracleDbHelper.AddParameter(cmd, "pi_filename", OracleDbType.Varchar2, fileName)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oInvoiceDAL As New InvoiceDAL
        Dim oClaimDAL As New ClaimDAL
        Dim oInvoiceReconWrkDAL As New InvoiceReconWrkDAL
        Dim oClaimloadReconWrkDAL As New ClaimloadReconWrkDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'updates additions and changes
            If Not familyDataset.Tables(ClaimDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimDAL.TABLE_NAME).Rows.Count > 0 Then
                oClaimDAL.UpdateFamily(familyDataset, True, Nothing, tr)
            End If
            If Not familyDataset.Tables(InvoiceDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(InvoiceDAL.TABLE_NAME).Rows.Count > 0 Then
                oInvoiceDAL.UpdateFamily(familyDataset, tr)
            End If
            oInvoiceReconWrkDAL.Update(familyDataset, tr, DataRowState.Modified)
            oClaimloadReconWrkDAL.Update(familyDataset, tr, DataRowState.Modified)
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
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
#End Region

#Region "StoreProcedures Control"
    Private Sub AsyncExecuteSP(ByVal oFileProcessedData As ClaimLoadFileProcessedData, ByVal selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        With oFileProcessedData
            inputParameters(PARAM_FILENAME) = New DBHelperParameter("p_filename", .filename)
            inputParameters(PARAM_INTERFACE_STATUS_ID) = New DBHelperParameter("p_interface_status_id", .interfaceStatus_id.ToByteArray)
            outputParameter(0) = New DBHelperParameter("p_return", GetType(Integer))
        End With
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub

    Private Sub ExecuteSP(ByVal oFileProcessedData As ClaimLoadFileProcessedData, ByVal selectStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(oFileProcessedData, selectStmt, Nothing, Nothing)
    End Sub

    Public Sub ValidateFile(ByVal oData As ClaimLoadFileProcessedData)
        Dim selectStmt As String
        selectStmt = Me.Config("/SQL/VALIDATE_FILE")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFile(ByVal oData As ClaimLoadFileProcessedData)
        Dim selectStmt As String
        selectStmt = Me.Config("/SQL/PROCESS_FILE")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(ByVal oData As ClaimLoadFileProcessedData)
        Dim selectStmt As String
        selectStmt = Me.Config("/SQL/DELETE_FILE")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
End Class


