'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/29/2005)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

#Region "SplitFileProcessedData"

Public Class SplitFileProcessedData
    Public filename, layout As String
    Public interfaceStatus_id As Guid
End Class

#End Region

Public Class SplitfileProcessedDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SPLITFILE_PROCESSED"
    Public Const TABLE_KEY_NAME As String = "splitfile_processed_id"

    Public Const COL_NAME_SPLITFILE_PROCESSED_ID As String = "splitfile_processed_id"
    Public Const COL_NAME_SPLIT_SYSTEM_ID As String = "split_system_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_PROCESS_FLAG As String = "process_flag"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_SPLIT As String = "split"

    'Search Parameters
    Public Const SPLIT_SYSTEM_ID = 0
    Public Const FILE_TYPE_CODE = 1
    Public Const SPLIT_FILE_PROCESSED_ID = 0

    ' Store Procedure Parameters
    Public Const FILENAME = 0
    Public Const LAYOUT = 1
    Public Const INTERFACE_STATUS_ID = 2
    Public Const TOTAL_PARAM_SP = 2 '3

    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_RETURN As String = "return_code"


#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(oSplitFileProcessedData As SplitFileProcessedData, selectStmt As String)

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("splitfile_processed_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(splitSystemId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters(0) As DBHelperParameter
        Dim sFileTypeCode As String

        If splitSystemId.Equals(Guid.Empty) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        Else
            parameters(SPLIT_SYSTEM_ID) = New DBHelperParameter(COL_NAME_SPLIT_SYSTEM_ID, splitSystemId.ToByteArray)
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadTotalRecordsByFile(SplitfileProcessedId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_TOTAL_RECORDS_BY_FILE_LIST")
        Dim parameters(0) As DBHelperParameter
        Dim sFileTypeCode As String

        If SplitfileProcessedId.Equals(Guid.Empty) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        Else
            parameters(SPLIT_FILE_PROCESSED_ID) = New DBHelperParameter(COL_NAME_SPLITFILE_PROCESSED_ID, SplitfileProcessedId.ToByteArray)
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    'Private Sub ExecuteSP(ByVal oSplitFileProcessedData As SplitFileProcessedData, ByVal selectStmt As String)
    '    Dim inputParameters(TOTAL_PARAM) As DBHelperParameter
    '    Dim outputParameter(0) As DBHelperParameter

    '    With oSplitFileProcessedData
    '        inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
    '        inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
    '        outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
    '    End With
    '    ' Call DBHelper Store Procedure
    '    DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
    '    If outputParameter(0).Value <> 0 Then
    '        Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
    '    End If
    'End Sub

    Private Sub AsyncExecuteSP(oSplitFileProcessedData As SplitFileProcessedData, selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        With oSplitFileProcessedData
            inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
            inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
            inputParameters(INTERFACE_STATUS_ID) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
            outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
        End With
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub

    Private Sub ExecuteSP(oSplitFileProcessedData As SplitFileProcessedData, selectStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(oSplitFileProcessedData, selectStmt, Nothing, Nothing)
    End Sub

    Public Sub SplitFile(oData As Object)
        Dim oSplitFileProcessedData As SplitFileProcessedData = CType(oData, SplitFileProcessedData)
        Dim selectStmt As String

        selectStmt = Config("/SQL/SPLIT_FILE")

        Try
            ExecuteSP(oSplitFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(oData As Object)
        Dim oSplitFileProcessedData As SplitFileProcessedData = CType(oData, SplitFileProcessedData)
        Dim selectStmt As String

        selectStmt = Config("/SQL/DELETE_SPLIT_FILE")

        Try
            ExecuteSP(oSplitFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region


End Class


