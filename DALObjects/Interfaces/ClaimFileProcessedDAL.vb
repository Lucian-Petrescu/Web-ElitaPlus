'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/12/2004)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

#Region "ClaimFileProcessedData"

Public Class ClaimFileProcessedData
    Public Enum InterfaceTypeCode
        NEW_CLAIM
        NEW_CLAIM_HP
        CLOSE_CLAIM
        CLOSE_CLAIM_SUNCOM
        CLAIM_SUSPENSE
        CLAIM_LOAD_COMMON
        INVOICE_LOAD_COMMON
    End Enum
    Public interfaceStatus_id As Guid
    Public splitSystemId As Guid
    Public fileTypeCode As InterfaceTypeCode
    Public filename, layout, isInRole As String
End Class

#End Region

Public Class ClaimFileProcessedDAL
    Inherits DALBase



#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIMFILE_PROCESSED"
    Public Const TABLE_KEY_NAME As String = "claimfile_processed_id"

    ' Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_CLAIMFILE_PROCESSED_ID As String = "claimfile_processed_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_SPLIT_SYSTEM_ID As String = "split_system_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_FILE_TYPE_CODE = "file_type_code"
    Public Const COL_NAME_RETURN As String = "return_code"
    Public Const COL_NAME_PROCESSED_AMOUNT As String = "processed_amount"
    Private Const DSNAME As String = "LIST"
    Public Const COL_NAME_IS_IN_ROLE As String = "is_in_role"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"

    Public Const WILDCARD As Char = "%"

    ' Search Parameters
    Public Const SPLIT_SYSTEM_ID = 0
    Public Const DEALER_CODE = 1
    Public Const IS_IN_ROLE_2 = 2
    Public Const IS_IN_ROLE_1 = 1
    Public Const TOTAL_PARAM = 1
    Public Const TOTAL_PARAM2 = 2
    ' Store Procedure Parameters
    Public Const FILENAME = 0
    Public Const LAYOUT = 1
    Public Const INTERFACE_STATUS_ID = 2
    Public Const TOTAL_PARAM_SP = 2 '3




#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(oClaimFileProcessedData As ClaimFileProcessedData, selectStmt As String)

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
        Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("claimfile_processed_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(oClaimFileProcessedData As ClaimFileProcessedData) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters(TOTAL_PARAM) As DBHelperParameter
        Dim sFileTypeCode As String

        With oClaimFileProcessedData
            If .splitSystemId.Equals(Guid.Empty) Then
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
            Else
                parameters(SPLIT_SYSTEM_ID) = New DBHelperParameter(COL_NAME_SPLIT_SYSTEM_ID, .splitSystemId.ToByteArray)
            End If
            parameters(IS_IN_ROLE_1) = New DBHelperParameter(COL_NAME_IS_IN_ROLE, .isInRole)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListForADealer(oClaimFileProcessedData As ClaimFileProcessedData, dealerCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_A_DEALER")
        Dim parameters(TOTAL_PARAM2) As DBHelperParameter
        Dim sFileTypeCode As String

        With oClaimFileProcessedData
            If .splitSystemId.Equals(Guid.Empty) Then
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
            Else
                parameters(SPLIT_SYSTEM_ID) = New DBHelperParameter(COL_NAME_SPLIT_SYSTEM_ID, .splitSystemId.ToByteArray)
            End If
            parameters(DEALER_CODE) = New DBHelperParameter(COL_NAME_DEALER_CODE, dealerCode)
            parameters(IS_IN_ROLE_2) = New DBHelperParameter(COL_NAME_IS_IN_ROLE, .isInRole)
        End With

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
        MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "StoreProcedures Control"

    '' Execute Store Procedure
    'Private Sub ExecuteSP(ByVal oClaimFileProcessedData As ClaimFileProcessedData, ByVal selectStmt As String)
    '    Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
    '    Dim outputParameter(0) As DBHelperParameter

    '    With oClaimFileProcessedData
    '        inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
    '        inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
    '        inputParameters(INTERFACE_STATUS_ID) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
    '        outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
    '    End With
    '    ' Call DBHelper Store Procedure
    '    DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
    '    If outputParameter(0).Value <> 0 Then
    '        Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
    '    End If
    'End Sub

    ' Execute Store Procedure
    Private Sub AsyncExecuteSP(oClaimFileProcessedData As ClaimFileProcessedData, selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        With oClaimFileProcessedData
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

    Private Sub ExecuteSP(oClaimFileProcessedData As ClaimFileProcessedData, selectStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(oClaimFileProcessedData, selectStmt, Nothing, Nothing)
    End Sub

    Public Sub ValidateFile(oData As Object)
        Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
        Dim selectStmt As String

        Select Case oClaimFileProcessedData.fileTypeCode
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                selectStmt = Config("/SQL/VALIDATE_NEW_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                selectStmt = Config("/SQL/VALIDATE_NEW_CLAIM_HP")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                selectStmt = Config("/SQL/VALIDATE_CLOSE_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                selectStmt = Config("/SQL/VALIDATE_CLOSE_CLAIM_SUNCOM")
        End Select

        Try
            ExecuteSP(oClaimFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFileRecords(oData As Object)
        Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
        Dim selectStmt As String

        Select Case oClaimFileProcessedData.fileTypeCode
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                selectStmt = Config("/SQL/PROCESS_NEW_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                selectStmt = Config("/SQL/PROCESS_NEW_CLAIM_HP")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                selectStmt = Config("/SQL/PROCESS_CLOSE_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                selectStmt = Config("/SQL/PROCESS_CLOSE_CLAIM_SUNCOM")
        End Select

        Try
            ExecuteSP(oClaimFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(oData As Object)
        Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
        Dim selectStmt As String

        Select Case oClaimFileProcessedData.fileTypeCode
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM
                selectStmt = Config("/SQL/DELETE_NEW_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                selectStmt = Config("/SQL/DELETE_NEW_CLAIM_HP")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM
                selectStmt = Config("/SQL/DELETE_CLOSE_CLAIM_FILE")
            Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                selectStmt = Config("/SQL/DELETE_CLOSE_CLAIM_SUNCOM")
        End Select

        Try
            ExecuteSP(oClaimFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

End Class


