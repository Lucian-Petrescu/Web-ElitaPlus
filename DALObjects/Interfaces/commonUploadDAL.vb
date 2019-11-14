Imports System.Collections.Generic

Public Class commonUploadDAL
    Inherits DALBase

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region
#Region "Delegate Signatures"
    Public Delegate Sub AsyncCaller(ByVal strUploadType As String, ByVal guidStatusID As Guid, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
#End Region
#Region "Upload File Processing"

    Public Sub InsertUploadFileLines(ByVal strUploadType As String, ByVal FileLines As Generic.List(Of String))
        Dim selectStmt As String = Me.Config("/SQL/LOAD_FILE_LINE")
        Dim intLineNum As Integer
        Dim inClauseCondition As String

        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter("file_line", FileLines(0).GetType),
                                     New DBHelper.DBHelperParameter("line_number", intLineNum.GetType),
                                     New DBHelper.DBHelperParameter("upload_type", strUploadType.GetType)}

        intLineNum = 0


        Try
            parameters(2).Value = strUploadType
            For Each Str As String In FileLines
                parameters(0).Value = FileLines(intLineNum)
                parameters(1).Value = intLineNum + 1
                DBHelper.Execute(selectStmt, parameters)
                intLineNum += 1
            Next
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub InsertUploadFileLinesBulk(ByVal strUploadType As String, ByVal FileLines As Generic.List(Of String), ByVal fileName As String)
        Dim cBatchSize As Integer = 200
        Dim strStmt As String = "INSERT INTO ELP_Upload_File_Lines (file_line, line_number, upload_type) values (:file_line, :line_number, :upload_type)"
        Dim conn As OracleConnection
        Dim command As OracleCommand
        Try
            conn = DBHelper.GetConnection()

            Dim totalLineNum As Long = 0
            Dim batchCnt As Integer = 0
            Dim intLineNums(cBatchSize - 1) As Integer
            Dim strLines(cBatchSize - 1) As String
            Dim strUploadTypes(cBatchSize - 1) As String

            For Each Str As String In FileLines
                If batchCnt = 0 AndAlso totalLineNum > 0 Then
                    Array.Clear(strLines, 0, strLines.Length)
                    Array.Clear(intLineNums, 0, intLineNums.Length)
                    Array.Clear(strUploadTypes, 0, strUploadTypes.Length)
                End If

                strLines(batchCnt) = Str
                intLineNums(batchCnt) = totalLineNum + 1
                strUploadTypes(batchCnt) = strUploadType

                totalLineNum = totalLineNum + 1
                batchCnt = batchCnt + 1

                If batchCnt = cBatchSize Then

                    Dim paramLines As OracleParameter = New OracleParameter("file_line", OracleDbType.Varchar2, 3000)
                    paramLines.Value = strLines

                    Dim paramLineNums As OracleParameter = New OracleParameter("line_number", OracleDbType.Int32)
                    paramLineNums.Value = intLineNums

                    Dim paramUploadTypes As OracleParameter = New OracleParameter("upload_type", OracleDbType.Varchar2, 50)
                    paramUploadTypes.Value = strUploadTypes

                    command = conn.CreateCommand()
                    command.CommandText = strStmt

                    command.ArrayBindCount = strLines.Count()
                    command.Parameters.Add(paramLines)
                    command.Parameters.Add(paramLineNums)
                    command.Parameters.Add(paramUploadTypes)
                    command.ExecuteNonQuery()

                    command = Nothing
                    batchCnt = 0
                End If
            Next

            'remaining lines
            If batchCnt > 0 Then
                ReDim Preserve intLineNums(batchCnt - 1)
                ReDim Preserve strLines(batchCnt - 1)
                ReDim Preserve strUploadTypes(batchCnt - 1)

                command = conn.CreateCommand()
                command.CommandText = strStmt

                Dim paramLines As OracleParameter = New OracleParameter("file_line", OracleDbType.Varchar2, 3000)
                paramLines.Value = strLines

                Dim paramLineNums As OracleParameter = New OracleParameter("line_number", OracleDbType.Int32)
                paramLineNums.Value = intLineNums

                Dim paramUploadTypes As OracleParameter = New OracleParameter("upload_type", OracleDbType.Varchar2, 50)
                paramUploadTypes.Value = strUploadTypes

                command.ArrayBindCount = strLines.Count()
                command.Parameters.Add(paramLines)
                command.Parameters.Add(paramLineNums)
                command.Parameters.Add(paramUploadTypes)
                command.ExecuteNonQuery()

                command = Nothing
            End If

            conn.Close()
        Catch ex As Exception
            Dim selectStmtUpd As String = Me.Config("SQL/UPDATE_FILE_PROCESSED")
            Dim parametersUpd() As DBHelper.DBHelperParameter
            Dim processStatus As String = "DONE"
            parametersUpd = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter("process_status", processStatus.GetType),
                 New DBHelper.DBHelperParameter("filename", fileName.GetType),
                 New DBHelper.DBHelperParameter("upload_type", strUploadType.GetType)}
            parametersUpd(0).Value = processStatus
            parametersUpd(1).Value = fileName
            parametersUpd(2).Value = strUploadType
            Try
                DBHelper.Execute(selectStmtUpd, parametersUpd)
            Catch ex1 As Exception
                ex = ex1
            End Try
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            If Not conn Is Nothing Then
                conn = Nothing
            End If
        End Try
    End Sub

    Public Sub ExtractReportFile(strUploadType As String, ByVal strUserEmailAddress As String, ByVal strCompanyGroupCode As String)
        Dim sqlStmt As String
        Try
            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter
            sqlStmt = Me.Config("/SQL/PROCESS_EXTRACT_REPORT")

            param = New DBHelper.DBHelperParameter("pi_UploadType", strUploadType)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("pi_Useremail", strUserEmailAddress)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("pi_CompanyGroupCode", strCompanyGroupCode)
            inParameters.Add(param)

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Sub InsertUploadFileLinesNew(ByVal strUploadType As String, ByVal FileLines As Generic.List(Of String), ByVal fileName As String)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_FILE_LINE")
        Dim intLineNum As Integer

        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter("file_line", FileLines(0).GetType),
                                     New DBHelper.DBHelperParameter("line_number", intLineNum.GetType),
                                     New DBHelper.DBHelperParameter("upload_type", strUploadType.GetType)}

        intLineNum = 0


        Try
            parameters(2).Value = strUploadType
            For Each Str As String In FileLines
                parameters(0).Value = FileLines(intLineNum)
                parameters(1).Value = intLineNum + 1
                DBHelper.Execute(selectStmt, parameters)
                intLineNum += 1
            Next
        Catch ex As Exception
            Dim selectStmtUpd As String = Me.Config("SQL/ UPDATE_FILE_PROCESSED")
            Dim parametersUpd() As DBHelper.DBHelperParameter
            Dim processStatus As String = "DONE"
            parametersUpd = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter("process_status", processStatus.GetType),
                                     New DBHelper.DBHelperParameter("filename", fileName.GetType),
                                     New DBHelper.DBHelperParameter("upload_type", strUploadType.GetType)}
            parametersUpd(0).Value = processStatus
            parametersUpd(1).Value = fileName
            parametersUpd(2).Value = strUploadType
            Try
                DBHelper.Execute(selectStmtUpd, parametersUpd)
            Catch ex1 As Exception
                ex = ex1
            End Try
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub InitUpload(ByVal strFileName As String, ByVal strUploadType As String,
                          ByVal strUser As String, ByRef strResult As String, ByRef strErrMsg As String)
        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/UPLOAD_INIT")
        strResult = String.Empty
        strErrMsg = String.Empty
        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_InitResult", strResult.GetType, 500),
                            New DBHelper.DBHelperParameter("p_ErrMsg", strErrMsg.GetType, 500)}

            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter

            param = New DBHelper.DBHelperParameter("p_FileName", strFileName)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_Upload_Type", strUploadType)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_User", strUser)
            inParameters.Add(param)

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, outParameters)

            If Not outParameters(0).Value Is Nothing Then
                strResult = outParameters(0).Value.ToString().Trim
                If Not outParameters(1).Value Is Nothing Then
                    strErrMsg = outParameters(1).Value.ToString().Trim
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFile(ByVal strUploadType As String, ByVal guidStatusID As Guid, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
        Dim sqlStmt As String
        Try
            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter
            If String.Equals(strUploadType, "REACTIVATE") Then
                sqlStmt = Me.Config("/SQL/PROCESS_REACTIVATE_FILE")

                param = New DBHelper.DBHelperParameter("p_UploadType", strUploadType)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_User", strUser)
                inParameters.Add(param)


            ElseIf String.Equals(strUploadType, "CANCEL") Then
                sqlStmt = Me.Config("/SQL/PROCESS_CANCEL_FILE")

                param = New DBHelper.DBHelperParameter("pi_Uploadtype", strUploadType)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("pi_User", strUser)
                inParameters.Add(param)

            ElseIf String.Equals(strUploadType, "CANCELRENAME") Then
                sqlStmt = Me.Config("/SQL/PROCESS_CANCEL_AND_RENAME_FILE")


                param = New DBHelper.DBHelperParameter("pi_Uploadtype", strUploadType)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("pi_User", strUser)
                inParameters.Add(param)

            Else
                sqlStmt = Me.Config("/SQL/PROCESS_FILE")

                param = New DBHelper.DBHelperParameter("p_UploadType", strUploadType)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_InterfaceStatusID", guidStatusID.ToByteArray)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_CompanyGroupCode", strCompanyGroupCode)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_UserEmailAddress", strUserEmailAddress)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_User", strUser)
                inParameters.Add(param)
            End If

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, Nothing)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFileAsync(ByVal strUploadType As String, ByVal guidStatusID As Guid, ByVal strCompanyGroupCode As String, ByVal strUserEmailAddress As String, ByVal strUser As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf ProcessFile)
        aSyncHandler.BeginInvoke(strUploadType, guidStatusID, strCompanyGroupCode, strUserEmailAddress, strUser, Nothing, Nothing)
    End Sub

    Public Function LoadProcessingError(ByVal strUploadType As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PROCESSING_ERR")
        selectStmt = selectStmt.Replace(":upload_type", strUploadType)
        Try
            Dim ds As DataSet
            ds = DBHelper.Fetch(selectStmt, "ProcessErrors")
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class
