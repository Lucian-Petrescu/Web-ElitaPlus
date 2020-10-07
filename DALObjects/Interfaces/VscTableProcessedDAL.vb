Imports Assurant.ElitaPlus.DALObjects.DBHelper

#Region "VscTableProcessedData"

Public Class VscTableProcessedData
    Public companyGroupCode, layout As String
    'Public companyGroupCode, xmlData, layout, filename As String
    '  Public allDs As DataSet
    Public interfaceStatus_id As Guid
End Class

#End Region

Public Class VscTableProcessedDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "VSC_LOAD_PROCESSED"
    'Public Const TABLE_KEY_NAME As String = "dealerfile_processed_id"

    'Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    'Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    'Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_FILENAME As String = "filename"
    'Public Const COL_NAME_RECEIVED As String = "received"
    'Public Const COL_NAME_COUNTED As String = "counted"
    'Public Const COL_NAME_REJECTED As String = "rejected"
    'Public Const COL_NAME_VALIDATED As String = "validated"
    'Public Const COL_NAME_LOADED As String = "loaded"
    '  Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_COMPANY_GROUP As String = "company_group"
    '  Public Const COL_NAME_XML_DATA As String = "xmlData"
    '  Public Const COL_NAME_IS_LAST_RECORD As String = "isLastRecord"
    'Public Const COL_NAME_FILE_TYPE_CODE = "file_type_code"
    Public Const COL_NAME_RETURN As String = "return"
    'Public Const COL_NAME_COMPANY_ID As String = "company_id"
    'Private Const DSNAME As String = "LIST"

    'Public Const WILDCARD As Char = "%"

    '' Search Parameters
    'Public Const DEALER_CODE = 0
    'Public Const FILE_TYPE_CODE = 1
    'Public Const TOTAL_PARAM = 1 '2

    ' Store Procedure Parameters
    'Public Const FILENAME = 0
    'Public Const LAYOUT = 1
    'Public Const INTERFACE_STATUS_ID = 2
    'Public Const TOTAL_PARAM_SP = 2 '3

    ' StoreProcedure  Input Parameters
    '   Public Const LAYOUT = 0
    Public Const COMPANY_GROUP = 0
    ' Public Const FILENAME = 1
    '  Public Const XML_DATA = 1
    '  Public Const IS_LAST_RECORD = 2
    Public Const INTERFACE_STATUS_ID = 1
    Public Const TOTAL_PARAM_SP = 1 '2

    ' StoreProcedure  Output Parameters
    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1
    Public Const P_CURSOR = 2
    Public Const TOTAL_OUTPUT_PARAM_SP = 1 '2

    Public Const SP_PARAM_NAME_P_RETURN As String = "v_return"
    Public Const SP_PARAM_NAME_P_EXCEPTION_MSG As String = "v_exception_msg"
    Public Const SP_PARAM_NAME_P_CURSOR As String = "v_ErrorDataCursor"

    'Public Const INTERFACE_STATUS_ID_DOWNLOAD = 0
    'Public Const FILENAME_DOWNLOAD = 1




#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(oVscTableProcessedData As VscTableProcessedData, selectStmt As String)

#End Region

#Region "StoreProcedures Control"

    Private Sub AsyncExecuteSP(oVscTableProcessedData As VscTableProcessedData, selectStmt As String)
        'Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        'Dim outputParameter(0) As DBHelperParameter

        'With oVscTableProcessedData
        '    inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
        '    inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
        '    inputParameters(INTERFACE_STATUS_ID) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
        '    outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
        'End With
        '' Call DBHelper Store Procedure
        'DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        'If outputParameter(0).Value <> 0 Then
        '    Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        'End If
    End Sub

    Private Sub ExecuteSP(oVscTableProcessedData As VscTableProcessedData, selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_SP) As DBHelperParameter
        '   Dim ds As New DataSet

        With oVscTableProcessedData
            inputParameters(COMPANY_GROUP) = New DBHelperParameter(COL_NAME_COMPANY_GROUP, .companyGroupCode)
            '  inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
            inputParameters(INTERFACE_STATUS_ID) = _
                New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
            outputParameter(P_RETURN) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_RETURN, GetType(Integer))
            outputParameter(P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_EXCEPTION_MSG, GetType(String), 50)
            ' outputParameter(Me.P_CURSOR) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_P_CURSOR, GetType(DataSet))
            '  outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
        End With
        ' Call DBHelper Store Procedure
        'DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, Me.TABLE_NAME)
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(P_RETURN).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(P_RETURN).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
        ' Return ds
    End Sub

    'Private Sub ExecuteSP(ByVal oVscTableProcessedData As VscTableProcessedData, ByVal selectStmt As String)
    '    Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
    '    aSyncHandler.BeginInvoke(oVscTableProcessedData, selectStmt, Nothing, Nothing)
    'End Sub

    Public Sub ProcessFileRecords(oVscTableProcessedData As VscTableProcessedData)
        ' Dim ds As DataSet
        Dim selectStmt As String

        selectStmt = Config("/SQL/PROCESS_FILE_" & oVscTableProcessedData.layout)

        Try
            ExecuteSP(oVscTableProcessedData, selectStmt)
            'ds = New DataSet
            'ds.ReadXml(XMLHelper.GetXMLStream(oVscTableProcessedData.xmlData))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    'Public Sub ProcessFileRecords(ByVal oVscTableProcessedData As VscTableProcessedData)
    '    Dim selectStmt As String

    '    selectStmt = Me.Config("/SQL/PROCESS_FILE")

    '    Try
    '        ExecuteSP(oVscTableProcessedData, selectStmt)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

#End Region

End Class
