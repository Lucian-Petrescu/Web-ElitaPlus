'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/12/2004)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

#Region "DealerFileProcessedData"

Public Class DealerFileProcessedData
    Public Enum InterfaceTypeCode
        CERT
        PAYM
        TLMK
        RINS
        PYMT
        INVC
    End Enum
    Public interfaceStatus_id, dealerfile_processed_id As Guid
    Public oSP, desc As Integer
    Public fileTypeCode As InterfaceTypeCode

    Public dealerCode, dealergrpCode, filename, layout, DealerType, parentFile As String
    Public dealerId, dealergroupId As Guid
End Class

#End Region
Public Class DealerFileProcessedDAL
    Inherits DALBase



#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALERFILE_PROCESSED"
    Public Const TABLE_KEY_NAME As String = "dealerfile_processed_id"

    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_DEALER_GROUP_CODE As String = "dealer_group_code"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_REMAINING_REJECTED As String = "remaining_rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_IS_SPLIT_FILE As String = "pi_is_split_file"
    Public Const COL_NAME_FILE_TYPE_CODE = "file_type_code"
    Public Const COL_NAME_RETURN As String = "return"
    Public Const COL_NAME_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_Is_Child_File As String = "Is_Child_File"
    Public Const COL_NAME_STATUS_DESC As String = "status_desc"
    Private Const DSNAME As String = "LIST"
    Public Const DEALERCODE As String = "dealer"
    Private Const DEALERGROUPCODE As String = "code"

    Public Const WILDCARD As Char = "%"
    Public Const ALL As String = "*"
    Public Const YESNO_Y As String = "Y"
    Public Const YESNO_N As String = "N"
    ' Search Parameters
    Public Const DEALER_CODE = 0
    Public Const DEALER_GROUP_CODE = 0
    Public Const FILE_TYPE_CODE = 1
    Public Const TOTAL_PARAM = 1 '2
    Public Const LAYOUT = 1


    ' Store Procedure Parameters
    Public Const FILENAME = 0
    Public Const DEALERFILE_PROCESSED_ID = 1
    Public Const INTERFACE_STATUS_ID = 2
    Public Const IS_SPLIT_FILE = 3
    Public Const TOTAL_PARAM_SP = 3


    'VSC Store Procedure Parameters
    Public Const FILENAME_VSC = 0
    Public Const LAYOUT_VSC = 1
    Public Const DEALERFILE_PROCESSED_ID_VSC = 1
    Public Const INTERFACE_STATUS_ID_VSC = 2
    Public Const TOTAL_PARAM_SP_VSC = 2

    Public Const INTERFACE_STATUS_ID_DOWNLOAD = 0
    Public Const FILENAME_DOWNLOAD = 1

    Private Const SP_VALIDATE As Integer = 0
    Private Const SP_PROCESS As Integer = 1

    Private Const SP_DEALER As Integer = 0
    Private Const SP_DEALERPAY As Integer = 1
    Private Const SP_DEALERRINS As Integer = 2


#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", id.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealerfile_processed_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid, ByVal parentfile As Boolean)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PARENT_CHILDS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", id.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("file_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function DealerFilesLoadBtwnDateRange(ByVal companyId As Guid, ByVal dealercode As String, ByVal BeginDate As String,
                                           ByVal EndDate As String, ByVal FileType As String, ByVal rejectionType As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/DEALER_FILES_LOADED_BETWEEN_DATE_RANGE")

        Dim dealer As String
        If dealercode = ALL Then
            dealer = WILDCARD
        Else
            dealer = dealercode
        End If
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_Id", companyId.ToByteArray),
                                                                                          New DBHelper.DBHelperParameter("dealer", dealer),
                                                                                          New DBHelper.DBHelperParameter("begin_date", BeginDate),
                                                                                          New DBHelper.DBHelperParameter("end_date", EndDate),
                                                                                          New DBHelper.DBHelperParameter("file_type_code", FileType),
                                                                                          New DBHelper.DBHelperParameter("file_type_code", rejectionType)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function PaymentFilesLoadBtwnDateRange(ByVal companyId As Guid, ByVal dealercode As String, ByVal BeginDate As String,
                                           ByVal EndDate As String, ByVal FileType As String, ByVal rejectionType As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/PAYMENT_FILES_LOADED_BETWEEN_DATE_RANGE")

        Dim dealer As String
        If dealercode = ALL Then
            dealer = WILDCARD
        Else
            dealer = dealercode
        End If
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_Id", companyId.ToByteArray),
                                                                                          New DBHelper.DBHelperParameter("dealer", dealer),
                                                                                          New DBHelper.DBHelperParameter("begin_date", BeginDate),
                                                                                          New DBHelper.DBHelperParameter("end_date", EndDate),
                                                                                          New DBHelper.DBHelperParameter("file_type_code", FileType)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal compIds As ArrayList, ByVal oDealerFileProcessedData As DealerFileProcessedData) As DataSet
        Dim selectStmt As String
        'Dim parameters(TOTAL_PARAM) As DBHelperParameter
        Dim parameter() As DBHelper.DBHelperParameter
        Dim sFileTypeCode As String

        'Dim inClauseCondition As String

        'inClauseCondition &= MiscUtil.BuildListForSql("AND D." & Me.COL_NAME_COMPANY_ID, compIds, True)
        '-selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        Dim whereClauseConditions As String

        With oDealerFileProcessedData
            If .parentFile = YESNO_N Then
                selectStmt = Me.Config("/SQL/LOAD_LIST")
                If Not Trim(.dealerCode).Equals(String.Empty) Then
                    ' parameters(DEALER_CODE) = New DBHelperParameter(COL_NAME_DEALER_CODE, .dealerCode)
                    whereClauseConditions &= " AND dfp." + COL_NAME_DEALER_ID + " = '" & GuidControl.GuidToHexString(.dealerId) & "'"

                ElseIf Not Trim(.dealergrpCode).Equals(String.Empty) Then

                    '  parameters(DEALER_GROUP_CODE) = New DBHelperParameter(COL_NAME_DEALER_GROUP_CODE, .dealergrpCode)
                    whereClauseConditions &= " AND dfp." + COL_NAME_DEALER_GROUP_ID + " = '" & GuidControl.GuidToHexString(.dealergroupId) & "'"
                Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
                End If
                sFileTypeCode = .InterfaceTypeCode.GetName(GetType(DealerFileProcessedData.InterfaceTypeCode), .fileTypeCode)
                '  parameters(FILE_TYPE_CODE) = New DBHelperParameter(COL_NAME_FILE_TYPE_CODE, sFileTypeCode)
                ' whereClauseConditions &= " AND dp.file_type_code =' " & sFileTypeCode & "'"

                parameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", oDealerFileProcessedData.dealerfile_processed_id),
                                                             New DBHelper.DBHelperParameter(COL_NAME_FILE_TYPE_CODE, sFileTypeCode)}
            Else
                selectStmt = Me.Config("/SQL/LOAD_PARENT_LIST")
                whereClauseConditions &= MiscUtil.BuildListForSql("AND D." & Me.COL_NAME_COMPANY_ID, compIds, True)
                sFileTypeCode = .InterfaceTypeCode.GetName(GetType(DealerFileProcessedData.InterfaceTypeCode), .fileTypeCode)
                parameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", oDealerFileProcessedData.dealerfile_processed_id),
                                                             New DBHelper.DBHelperParameter(COL_NAME_FILE_TYPE_CODE, sFileTypeCode)}

            End If
        End With


        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameter)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    'Private Sub ExecuteSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
    '    Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
    '    Dim outputParameter(0) As DBHelperParameter

    '    With oDealerFileProcessedData
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

    Private Sub AsyncExecuteSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        If (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM) Then

            Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
            Dim outputParameter(0) As DBHelperParameter

            With oDealerFileProcessedData
                inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
                If .layout Is Nothing Then
                    inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, "")
                Else
                    inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
                End If
                inputParameters(INTERFACE_STATUS_ID) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
                inputParameters(IS_SPLIT_FILE) = New DBHelperParameter(COL_NAME_IS_SPLIT_FILE, .parentFile)

                outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
            End With
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If

        ElseIf (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.CERT) Then
            Dim inputParameters(3) As DBHelperParameter
            Dim outputParameter(1) As DBHelperParameter

            With oDealerFileProcessedData
                inputParameters(0) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)
                inputParameters(1) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)
                inputParameters(2) = New DBHelperParameter("pi_filename", .filename)
                inputParameters(3) = New DBHelperParameter("pi_is_split_file", .parentFile)

                outputParameter(0) = New DBHelperParameter("po_job_name", GetType(String), 30)
                outputParameter(1) = New DBHelperParameter("po_return", GetType(Integer))
            End With
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(1).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(1).Value & " for Job = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If
        ElseIf (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.RINS) Then

            Dim inputParameters(1) As DBHelperParameter
            Dim outputParameter(0) As DBHelperParameter

            With oDealerFileProcessedData
                inputParameters(0) = New DBHelperParameter("pi_filename", .filename)

                inputParameters(1) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)


                outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
            End With
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If
        ElseIf (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.INVC) Then


            Dim outputParameter(0) As DBHelperParameter
            If oDealerFileProcessedData.oSP = SP_VALIDATE Then
                Dim inputParameters(1) As DBHelperParameter
                With oDealerFileProcessedData
                    inputParameters(0) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)
                    inputParameters(1) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)
                    outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
                End With
                DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

                If outputParameter(0).Value <> 0 Then
                    Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
                End If
            Else
                Dim inputParameters(2) As DBHelperParameter
                With oDealerFileProcessedData
                    inputParameters(0) = New DBHelperParameter("pi_filename", .filename)
                    inputParameters(1) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)
                    inputParameters(2) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)
                    outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
                End With
                DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

                If outputParameter(0).Value <> 0 Then
                    Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
                End If
            End If

        ElseIf (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then


            Dim outputParameter(0) As DBHelperParameter
            If oDealerFileProcessedData.oSP = SP_VALIDATE Then
                Dim inputParameters(1) As DBHelperParameter
                With oDealerFileProcessedData
                    inputParameters(0) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)
                    inputParameters(1) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)
                    outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
                End With
                DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

                If outputParameter(0).Value <> 0 Then
                    Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
                End If
            Else
                Dim inputParameters(2) As DBHelperParameter
                With oDealerFileProcessedData
                    inputParameters(0) = New DBHelperParameter("pi_filename", .filename)
                    inputParameters(1) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)
                    inputParameters(2) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)
                    outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
                End With
                DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
                If outputParameter(0).Value <> 0 Then
                    Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
                End If
            End If
        End If

    End Sub

    Private Sub AsyncExecuteSP_VSC(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP_VSC) As DBHelperParameter
        Dim outputParameter(1) As DBHelperParameter


        With oDealerFileProcessedData
            inputParameters(FILENAME_VSC) = New DBHelperParameter(COL_NAME_FILENAME, .filename)

            Select Case oDealerFileProcessedData.oSP
                Case SP_VALIDATE
                    If .layout Is Nothing Then
                        inputParameters(LAYOUT_VSC) = New DBHelperParameter(COL_NAME_LAYOUT, "")
                    Else
                        inputParameters(LAYOUT_VSC) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
                    End If
                Case SP_PROCESS
                    inputParameters(DEALERFILE_PROCESSED_ID) = New DBHelperParameter(COL_NAME_DEALERFILE_PROCESSED_ID, .dealerfile_processed_id.ToByteArray)
            End Select

            inputParameters(INTERFACE_STATUS_ID_VSC) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
            outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
            outputParameter(1) = New DBHelperParameter(COL_NAME_EXCEPTION_MSG, GetType(String), 50)
        End With
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub


    Private Sub AsyncExecuteDelSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        If (oDealerFileProcessedData.fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.RINS) Then
            Dim inputParameters(2) As DBHelperParameter
            Dim outputParameter(0) As DBHelperParameter
            With oDealerFileProcessedData
                inputParameters(0) = New DBHelperParameter("pi_filename", .filename)
                inputParameters(1) = New DBHelperParameter("pi_dealerfile_processed_id", .dealerfile_processed_id.ToByteArray)

                inputParameters(2) = New DBHelperParameter("pi_interface_status_id", .interfaceStatus_id.ToByteArray)

                outputParameter(0) = New DBHelperParameter("po_return", GetType(Integer))
            End With
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If

        Else
            Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
            Dim outputParameter(0) As DBHelperParameter

            With oDealerFileProcessedData
                inputParameters(FILENAME) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
                Select Case oDealerFileProcessedData.oSP
                    Case SP_DEALER
                        If .layout Is Nothing Then
                            inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, "")
                        Else
                            inputParameters(LAYOUT) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
                        End If
                    Case SP_DEALERPAY
                        inputParameters(DEALERFILE_PROCESSED_ID) = New DBHelperParameter(COL_NAME_DEALERFILE_PROCESSED_ID, .dealerfile_processed_id.ToByteArray)
                End Select
                inputParameters(INTERFACE_STATUS_ID) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
                inputParameters(IS_SPLIT_FILE) = New DBHelperParameter(COL_NAME_IS_SPLIT_FILE, .parentFile)
                outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
            End With
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If outputParameter(0).Value <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
            End If
        End If
    End Sub
    Private Sub AsyncExecuteDelSP_VSC(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP_VSC) As DBHelperParameter
        Dim outputParameter(1) As DBHelperParameter

        With oDealerFileProcessedData
            inputParameters(FILENAME_VSC) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
            Select Case oDealerFileProcessedData.oSP
                Case SP_DEALER
                    If .layout Is Nothing Then
                        inputParameters(LAYOUT_VSC) = New DBHelperParameter(COL_NAME_LAYOUT, "")
                    Else
                        inputParameters(LAYOUT_VSC) = New DBHelperParameter(COL_NAME_LAYOUT, .layout)
                    End If
                Case SP_DEALERPAY
                    inputParameters(DEALERFILE_PROCESSED_ID) = New DBHelperParameter(COL_NAME_DEALERFILE_PROCESSED_ID, .dealerfile_processed_id.ToByteArray)
            End Select
            inputParameters(INTERFACE_STATUS_ID_VSC) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
            outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
            outputParameter(1) = New DBHelperParameter(COL_NAME_EXCEPTION_MSG, GetType(String))
        End With
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub

    Private Sub ExecuteSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String, Optional ByVal blnVscPolicyLoad As Boolean = False)
        If oDealerFileProcessedData.DealerType.Equals("VSC") AndAlso oDealerFileProcessedData.oSP.Equals(1) AndAlso blnVscPolicyLoad = True Then
            Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP_VSC)
            aSyncHandler.BeginInvoke(oDealerFileProcessedData, selectStmt, Nothing, Nothing)
        Else
            Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
            aSyncHandler.BeginInvoke(oDealerFileProcessedData, selectStmt, Nothing, Nothing)
        End If

    End Sub

    Private Sub ExecuteDelSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        If oDealerFileProcessedData.DealerType.Equals("VSC") AndAlso oDealerFileProcessedData.oSP.Equals(1) Then
            Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteDelSP_VSC)
            aSyncHandler.BeginInvoke(oDealerFileProcessedData, selectStmt, Nothing, Nothing)
        Else
            Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteDelSP)
            aSyncHandler.BeginInvoke(oDealerFileProcessedData, selectStmt, Nothing, Nothing)
        End If

    End Sub

    Private Sub AsyncExecuteDownloadSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        Dim inputParameters(TOTAL_PARAM) As DBHelperParameter

        With oDealerFileProcessedData
            inputParameters(INTERFACE_STATUS_ID_DOWNLOAD) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, .interfaceStatus_id.ToByteArray)
            inputParameters(FILENAME_DOWNLOAD) = New DBHelperParameter(COL_NAME_FILENAME, .filename)
        End With
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, Nothing)
    End Sub

    Private Sub ExecuteDownloadSP(ByVal oDealerFileProcessedData As DealerFileProcessedData, ByVal selectStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteDownloadSP)
        aSyncHandler.BeginInvoke(oDealerFileProcessedData, selectStmt, Nothing, Nothing)
    End Sub

    Public Sub ValidateFile(ByVal oData As Object)
        Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
        Dim selectStmt As String

        Select Case oDealerFileProcessedData.fileTypeCode
            Case oDealerFileProcessedData.InterfaceTypeCode.CERT
                selectStmt = Me.Config("/SQL/VALIDATE_FILE")
            Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
                selectStmt = Me.Config("/SQL/VALIDATE_PAYMENT")
            Case oDealerFileProcessedData.InterfaceTypeCode.RINS
                selectStmt = Me.Config("/SQL/VALIDATE_REINSURANCE")
            Case oDealerFileProcessedData.InterfaceTypeCode.INVC
                selectStmt = Me.Config("/SQL/VALIDATE_INVOICE")
            Case oDealerFileProcessedData.InterfaceTypeCode.PYMT
                selectStmt = Me.Config("/SQL/VALIDATE_INVOICE_PAYMENT")
        End Select

        Try
            ExecuteSP(oDealerFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub GenerateResponseFile(ByVal oData As Object)
        Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
        Dim selectStmt As String

        Select Case oDealerFileProcessedData.fileTypeCode
            Case oDealerFileProcessedData.InterfaceTypeCode.CERT
                selectStmt = Me.Config("/SQL/GENERATE_RESPONSE_FILE")
            Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
                Throw New NotSupportedException
        End Select

        Try
            ExecuteSP(oDealerFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFileRecords(ByVal oData As Object)
        Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
        Dim selectStmt As String

        Select Case oDealerFileProcessedData.fileTypeCode
            Case oDealerFileProcessedData.InterfaceTypeCode.CERT
                If oDealerFileProcessedData.DealerType.Equals("VSC") Then
                    selectStmt = Me.Config("/SQL/PROCESS_FILE_VSC")
                Else
                    selectStmt = Me.Config("/SQL/PROCESS_FILE")
                End If
            Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
                selectStmt = Me.Config("/SQL/PROCESS_PAYMENT")
            Case oDealerFileProcessedData.InterfaceTypeCode.RINS
                selectStmt = Me.Config("/SQL/PROCESS_REINSURANCE")
            Case oDealerFileProcessedData.InterfaceTypeCode.PYMT
                selectStmt = Me.Config("/SQL/PROCESS_INVOICE_PAYMENT")
            Case oDealerFileProcessedData.InterfaceTypeCode.INVC
                selectStmt = Me.Config("/SQL/PROCESS_INVOICE")

        End Select

        Try
            If oDealerFileProcessedData.DealerType.Equals("VSC") Then
                ExecuteSP(oDealerFileProcessedData, selectStmt, True)
            Else
                ExecuteSP(oDealerFileProcessedData, selectStmt)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(ByVal oData As Object)
        Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
        Dim selectStmt As String

        Select Case oDealerFileProcessedData.fileTypeCode
            Case oDealerFileProcessedData.InterfaceTypeCode.CERT
                selectStmt = Me.Config("/SQL/DELETE_FILE")
                oDealerFileProcessedData.oSP = SP_DEALER
            Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
                selectStmt = Me.Config("/SQL/DELETE_PAYMENT")
                oDealerFileProcessedData.oSP = SP_DEALERPAY
            Case oDealerFileProcessedData.InterfaceTypeCode.RINS
                selectStmt = Me.Config("/SQL/DELETE_REINSURANCE")
        End Select

        Try
            ExecuteDelSP(oDealerFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DownloadFile(ByVal oData As Object)
        Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
        Dim selectStmt As String

        Select Case oDealerFileProcessedData.fileTypeCode
            Case oDealerFileProcessedData.InterfaceTypeCode.CERT
                selectStmt = Me.Config("/SQL/DOWNLOAD_FILE")
            Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
                selectStmt = Me.Config("/SQL/DOWNLOAD_PAYMENT")
        End Select

        Try
            ExecuteDownloadSP(oDealerFileProcessedData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

End Class


