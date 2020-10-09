'/*-----------------------------------------------------------------------------------------------------------------

'  AA      SSS  SSS  UU  UU RRRRR      AA    NN   NN  TTTTTTTT
'A    A   SS    SS   UU  UU RR   RR  A    A  NNN  NN     TT 
'AAAAAA   SSS   SSS  UU  UU RRRR     AAAAAA  NN N NN     TT
'AA  AA     SS    SS UU  UU RR RR    AA  AA  NN  NNN     TT
'AA  AA  SSSSS SSSSS  UUUU  RR   RR  AA  AA  NN  NNN     TT

'Copyright 2004, Assurant Group Inc..  All Rights Reserved.
'------------------------------------------------------------------------------
'This information is CONFIDENTIAL and for Assurant Group's exclusive use ONLY.
'Any reproduction or use without Assurant Group's explicit, written consent 
'is PROHIBITED.
'------------------------------------------------------------------------------

'Purpose: Provides resuable routines for Oracle database access.
'
'Author:  Project Team
'
'Date:    01/28/2004     

'MODIFICATION HISTORY:
' - 03/09/04 Add an ExecuteScalar method
'
'===========================================================================================
Imports System.Globalization
Imports System.IO
Imports System.Text.RegularExpressions
Imports Assurant.Common.Zip

Public NotInheritable Class DBHelper

    Private Const ORACLE_UNIQUE_CONSTRAINT_ERR As Integer = 1
    Private Const ORACLE_LENGHT_EXCEEDED_ERR As Integer = 1401
    Private Const ORACLE_INTEGRITY_CONSTRAINT_VIOLATION As Integer = 2292
    Private Const DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT As Integer = 20999
    Private Const DB_ERROR_COMUNA_NOT_FOUND As Integer = 20998

#Region " Constructors "


    Private Sub New()
        'can't create instance, only use the static methods
    End Sub


#End Region

#Region " Private Methods "


    Private Shared Sub InternalFetch(ds As DataSet, sql As String, tableName As String)
        Dim da As OracleDataAdapter

        Using conn As New OracleConnection(DBHelper.ConnectString)
            Try
                da = New OracleDataAdapter(sql, conn)
                da.Fill(ds, tableName)
                ds.Locale = CultureInfo.InvariantCulture
            Catch ex As Exception
            Finally
                da.Dispose()
                da = Nothing
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using

    End Sub


    Private Shared Sub InternalFetch(ds As DataSet, sql As String, tableName As String, parms() As OracleParameter)
        Dim da As OracleDataAdapter

        Using conn As New OracleConnection(DBHelper.ConnectString)
            Using cmd As New OracleCommand(sql, conn)
                Try
                    With cmd
                        If Not conn.State = ConnectionState.Open Then
                            conn.Open()
                        End If
                        .Connection = conn
                        .CommandType = CommandType.Text
                        If parms IsNot Nothing Then
                            For currParm As Int32 = 0 To (parms.GetLength(0) - 1)
                                .Parameters.Add(parms(currParm))
                            Next currParm
                        End If
                    End With

                    da = New OracleDataAdapter(cmd)
                    da.Fill(ds, tableName)
                    ds.Locale = CultureInfo.InvariantCulture
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                Finally
                    da.Dispose()
                    da = Nothing
                    If conn.State = ConnectionState.Open Then conn.Close()
                End Try
            End Using
        End Using

    End Sub

    Private Shared Sub InternalFetch(ds As DataSet, sql As String, tableName As String, _
    command As OracleCommand)
        Dim conn As IDbConnection
        Dim da As OracleDataAdapter

        Try
            conn = New OracleConnection(ConnectString)
            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            command.Connection = conn
            da = New OracleDataAdapter(command)
            da.Fill(ds, tableName)
            ds.Locale = CultureInfo.InvariantCulture
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            da.Dispose()
            da = Nothing
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

    End Sub



#End Region

#Region " Public Methods "

    Public Shared Function ConnectString() As String
        Dim objParameters As ElitaPlusParameters = CType(System.Threading.Thread.CurrentPrincipal.Identity, ElitaPlusParameters)
        Dim sStrConnect As String
        sStrConnect = "Validate Connection=true;" & "User ID=" & objParameters.AppUserId & ";Password=" & objParameters.AppPassword & ";Data Source=" & AppConfig.DataBase.Server
        Return sStrConnect

    End Function


    'Public Shared ReadOnly Property ConnectString() As String
    '    Get
    '        Dim strConnect As String

    '        strConnect = AppConfig.DataBase.ConnectionString()

    '        Return strConnect
    '    End Get
    'End Property

    Public Shared Function Fetch(sql As String, tableName As String) As DataSet
        Dim ds As DataSet = New DataSet

        InternalFetch(ds, sql, tableName)

        Return ds
    End Function

    Public Shared Function Fetch(sql As String, dataSetName As String, tableName As String) As DataSet
        Dim ds As DataSet = New DataSet(dataSetName)

        InternalFetch(ds, sql, tableName)

        Return ds
    End Function

    Public Shared Function Fetch(sql As String, dataSetName As String, tableName As String, parms() As OracleParameter) As DataSet
        Dim ds As DataSet = New DataSet(dataSetName)

        InternalFetch(ds, sql, tableName, parms)

        Return ds
    End Function

    Public Shared Function Fetch(ds As DataSet, sql As String, tableName As String, parms() As OracleParameter) As DataSet

        InternalFetch(ds, sql, tableName, parms)

        Return ds
    End Function

    Public Shared Function Fetch(ds As DataSet, sql As String, tableName As String, parms() As DBHelperParameter) As DataSet

        Dim oraParms(parms.Length - 1) As OracleParameter

        Dim i As Integer

        For i = 0 To parms.Length - 1
            oraParms(i) = New OracleParameter(parms(i).Name, parms(i).Value)
        Next

        InternalFetch(ds, sql, tableName, oraParms)

        Return ds
    End Function

    'NOTE: the passed in SQL should only SELECT one column
    Public Shared Function ExecuteScalar(sql As String, parms() As DBHelperParameter) As Object
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Dim cmd As New OracleCommand(sql, con)
        Dim oraParms As OracleParameter()

        Dim i As Integer
        If parms IsNot Nothing Then
            ReDim oraParms(parms.Length - 1)
            For i = 0 To parms.Length - 1
                cmd.Parameters.Add(New OracleParameter(parms(i).Name, parms(i).Value))
            Next
        End If
        Try
            con.Open()
            Return cmd.ExecuteScalar
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Function

    Public Shared Function ExecuteNonQuery(sql As String, inputParameters() As DBHelperParameter) As Object
        Dim command As OracleCommand
        Dim oParameter As DBHelperParameter
        Dim conn As IDbConnection

        Try

            command = New OracleCommand(sql)
            command.CommandType = CommandType.StoredProcedure

            ' return Parameters, this one must the first parameter in the list in order to get the returned value from DB
            command.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue

            ' Input Parameters
            If inputParameters IsNot Nothing Then
                For Each oParameter In inputParameters
                    If Not oParameter.DBType = Nothing Then
                        command.Parameters.Add(oParameter.Name, oParameter.DBType).Value = oParameter.Value
                    Else
                        command.Parameters.Add(oParameter.Name, DBNull.Value)
                    End If
                Next
            End If


            conn = New OracleConnection(ConnectString)

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            command.Connection = conn

            command.ExecuteNonQuery()

            If command.Parameters("result").Value = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    'NOTE: the passed in SQL should only SELECT one column
    Public Shared Function ExecuteScalar(sql As String) As Object
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Dim cmd As New OracleCommand(sql, con)

        Try
            con.Open()
            Return cmd.ExecuteScalar
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Function

    Public Shared Function ReadOracleXmlTypeData(sql As String, additionalParamters As DBHelperParameter()) As String
        'Create a connection.
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Dim rtnString As String

        Try

            Using con
                'Open a connection.
                con.Open()

                'Create a Command
                Dim cmd As OracleCommand
                cmd = con.CreateCommand()
                cmd.CommandText = ReplaceParameterValues(sql, additionalParamters)

                'Reading LOB
                Dim reader As OracleDataReader = cmd.ExecuteReader()

                Using reader
                    'Obtain the row of data.
                    If reader.Read() Then
                        rtnString = reader.GetString(0)
                    Else
                        rtnString = String.Empty
                    End If
                End Using

            End Using

        Catch ex As OracleException
            Select Case ex.Number
                Case ORACLE_LENGHT_EXCEEDED_ERR
                    Throw New DataBaseLengthExceededException(ex)
                Case DB_ERROR_COMUNA_NOT_FOUND
                    Throw New DatabaseComunaException(ex)
                Case Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)

        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

        Return rtnString
    End Function

    Public Shared Function ReadClobWithReader(sql As String, additionalParamters As DBHelperParameter()) As Text.StringBuilder

        'Create a connection.
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Dim rtnString As New Text.StringBuilder(String.Empty)

        Try

            Using con
                'Open a connection.
                con.Open()

                'Create a Command
                Dim cmd As OracleCommand
                cmd = con.CreateCommand()
                cmd.CommandText = ReplaceParameterValues(sql, additionalParamters)

                'Reading LOB
                Dim reader As OracleDataReader = cmd.ExecuteReader()

                Using reader
                    'Obtain the row of data.
                    reader.Read()

                    Dim clob As OracleClob = reader.GetOracleClob(0)
                    Dim streamreader As IO.StreamReader = New IO.StreamReader(clob, Text.Encoding.Unicode)

                    rtnString.Append(streamreader.ReadToEnd())

                End Using

            End Using

        Catch ex As OracleException
            Select Case ex.Number
                Case ORACLE_LENGHT_EXCEEDED_ERR
                    Throw New DataBaseLengthExceededException(ex)
                Case DB_ERROR_COMUNA_NOT_FOUND
                    Throw New DatabaseComunaException(ex)
                Case Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)

        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

        Return rtnString

    End Function

    Public Shared Sub MigrateXML(readSQL As String, readParamters As DBHelperParameter(), updateSQL As String, Modified_By As String)

        'Create a connection.
        Dim con As New OracleConnection(DBHelper.ConnectString)
        Dim actualXML As System.Text.StringBuilder = New System.Text.StringBuilder(String.Empty)

        Try

            Using con
                'Open a connection.
                con.Open()

                'Create a Command
                Dim cmd As OracleCommand
                cmd = con.CreateCommand()
                cmd.CommandText = ReplaceParameterValues(readSQL, readParamters)
                cmd.Connection = con

                Dim reader As OracleDataReader
                reader = cmd.ExecuteReader()

                Using reader
                    While reader.Read()

                        Dim myBytes As Byte() = New Byte(15) {}
                        Dim bytesRead As Long = reader.GetBytes(reader.GetOrdinal("acct_transmission_id"), 0, myBytes, 0, 16) ' acct_transmission_id

                        actualXML = New System.Text.StringBuilder(String.Empty)
                        Dim oraclob As OracleClob = reader.GetOracleClob(1) ' File_Text
                        Dim streamreader As StreamReader = New StreamReader(oraclob, System.Text.Encoding.Unicode)
                        actualXML.Append(streamreader.ReadToEnd())
                        oraclob.Close()

                        Dim filename As String = reader.GetOracleString(2) ' file_name
                        Dim filetype As Integer = CType(reader.GetValue(3), Integer) ' file_type_flag
                        Dim filesubtype As Integer = CType(reader.GetValue(4), Integer) ' file_sub_type_flag
                        Dim Journaltype As String

                        Select Case filetype
                            Case 0 'Journal
                                Select Case filesubtype
                                    Case 5 'CONTROL
                                        If InStr(1, filename, "_") = 0 Then
                                            Journaltype = "-CONTROL"
                                        Else
                                            Journaltype = String.Concat(filename.Substring(InStr(3, filename, "_"), InStr(InStr(3, filename, "_") + 1, filename, "_") - 1 - InStr(3, filename, "_")), "-CONTROL")
                                        End If
                                    Case Else
                                        If actualXML.ToString().IndexOf("<JournalType>") > 0 Then
                                            Journaltype = actualXML.ToString().Substring(actualXML.ToString().IndexOf("<JournalType>") + Len("<JournalType>"), actualXML.ToString().IndexOf("</JournalType>") - (actualXML.ToString().IndexOf("<JournalType>") + Len("<JournalType>")))
                                        Else
                                            Journaltype = String.Empty
                                        End If
                                End Select
                            Case Else
                                Journaltype = "Vendor"
                        End Select

                        ' Compression with IonicZip to Stream
                        Dim outstream As MemoryStream = New MemoryStream()
                        Dim compressionmethod As ICompressionProvider
                        compressionmethod = CompressionProviderFactory.Current.CreateInstance(CompressionProviderType.IonicZip)
                        compressionmethod.Compress(actualXML.ToString(), outstream)
                        outstream.Position = 0

                        ' Updating compressed data
                        Dim cmd1 As OracleCommand
                        cmd1 = con.CreateCommand()

                        cmd1.CommandType = CommandType.Text
                        cmd1.CommandText = updateSQL
                        cmd1.Connection = con

                        ' Creating update parameters
                        Dim para1 As OracleParameter = New OracleParameter("ptext1", OracleDbType.Blob, System.Data.ParameterDirection.Input)
                        para1.Value = outstream.MemoryStreamToByteArray()
                        para1.Size = Convert.ToInt32(outstream.Length)
                        cmd1.Parameters.Add(para1)

                        Dim para2 As OracleParameter = New OracleParameter("ptext2", OracleDbType.Varchar2, System.Data.ParameterDirection.Input)
                        para2.Size = Journaltype.Length
                        para2.Value = Journaltype
                        cmd1.Parameters.Add(para2)

                        Dim para3 As OracleParameter = New OracleParameter("ptext3", OracleDbType.Varchar2, System.Data.ParameterDirection.Input)
                        para3.Size = Modified_By.Length
                        para3.Value = Modified_By
                        cmd1.Parameters.Add(para3)

                        Dim para4 As OracleParameter = New OracleParameter("pid", OracleDbType.Raw, System.Data.ParameterDirection.Input)
                        para4.Value = myBytes
                        cmd1.Parameters.Add(para4)

                        Dim records_updated As Integer = cmd1.ExecuteNonQuery()

                    End While

                End Using

            End Using

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)

        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub

    'Exceptions: 
    '   DALInvalidParameterValueException: If some the SQLStmt parameter is required but passed as nothing
    '   DALConcurrencyAccessException: If the same data is changed in parallel
    '   DataBaseAccessException: If there is an error while trying to acces the DataBase
    Public Shared Sub Execute(table As DataTable, insSQLStmt As String, updSQLStmt As String, delSQLStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If table Is Nothing Then
            Return
        End If
        Dim tr As IDbTransaction = transaction
        Dim rowsToDelete As New ArrayList
        If tr Is Nothing Then
            tr = GetNewTransaction()
        End If
        Try
            Dim rowIdx As Integer
            For rowIdx = 0 To table.Rows.Count - 1
                Dim rowState As DataRowState = table.Rows(rowIdx).RowState
                If (rowState And DataRowState.Deleted) = DataRowState.Deleted Then
                    rowsToDelete.Add(table.Rows(rowIdx))
                Else
                    Execute(table.Rows(rowIdx), insSQLStmt, updSQLStmt, delSQLStmt, additionalParamters, tr, changesFilter)
                End If
            Next
            Dim row As DataRow
            For Each row In rowsToDelete
                Execute(row, insSQLStmt, updSQLStmt, delSQLStmt, additionalParamters, tr, changesFilter)
            Next
            If transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                Commit(tr)
            End If
        Catch ex As Exception
            If transaction Is Nothing Then
                'We are the creator of the transaction we shoul roll it back and close the connection
                RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Shared Sub ExecuteWithParam(table As DataTable, insSQLStmt As String, updSQLStmt As String, delSQLStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If table Is Nothing Then
            Return
        End If
        Dim tr As IDbTransaction = transaction
        Dim rowsToDelete As New ArrayList
        If tr Is Nothing Then
            tr = GetNewTransaction()
        End If
        Try
            Dim rowIdx As Integer
            For rowIdx = 0 To table.Rows.Count - 1
                Dim rowState As DataRowState = table.Rows(rowIdx).RowState
                If (rowState And DataRowState.Deleted) = DataRowState.Deleted Then
                    rowsToDelete.Add(table.Rows(rowIdx))
                Else
                    ExecuteWithParam(table.Rows(rowIdx), insSQLStmt, updSQLStmt, delSQLStmt, additionalParamters, tr, changesFilter)
                End If
            Next
            Dim row As DataRow
            For Each row In rowsToDelete
                ExecuteWithParam(row, insSQLStmt, updSQLStmt, delSQLStmt, additionalParamters, tr, changesFilter)
            Next
            If transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                Commit(tr)
            End If
        Catch ex As Exception
            If transaction Is Nothing Then
                'We are the creator of the transaction we shoul roll it back and close the connection
                RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    'Exceptions: 
    '   DALInvalidParameterValueException: If some the SQLStmt parameter is required but passed as nothing
    '   DALConcurrencyAccessException: If the same data is changed in parallel
    '   DataBaseAccessException: If there is an error while trying to acces the DataBase
    Public Shared Sub Execute(row As DataRow, insSQLStmt As String, updSQLStmt As String, delSQLStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        Dim stmtToExecute As String
        Dim rowState As DataRowState = row.RowState
        If Not changesFilter = Nothing Then
            rowState = rowState And changesFilter
        End If
        Select Case rowState
            Case DataRowState.Added
                'Insert
                If insSQLStmt Is Nothing OrElse insSQLStmt.Trim = "" Then
                    Throw New DALInvalidParameterValueException("insSQLStmt is required to execute this operation")
                End If
                stmtToExecute = insSQLStmt
            Case DataRowState.Deleted
                'delete
                If delSQLStmt Is Nothing OrElse delSQLStmt.Trim = "" Then
                    Throw New DALInvalidParameterValueException("delSQLStmt is required to execute this operation")
                End If
                stmtToExecute = delSQLStmt
            Case DataRowState.Modified
                'update
                If CheckRowChanged(row) Then
                    If updSQLStmt Is Nothing OrElse updSQLStmt.Trim = "" Then
                        Throw New DALInvalidParameterValueException("updSQLStmt is required to execute this operation")
                    End If
                    stmtToExecute = updSQLStmt
                End If
        End Select
        If stmtToExecute IsNot Nothing Then
            stmtToExecute = ReplaceParameterValues(stmtToExecute, row)
            stmtToExecute = ReplaceParameterValues(stmtToExecute, additionalParamters)
        End If
        If stmtToExecute IsNot Nothing Then
            ExecuteCommand(stmtToExecute, transaction)
            row.AcceptChanges()
        End If
    End Sub

    Public Shared Sub ExecuteWithParam(row As DataRow, insSQLStmt As String, updSQLStmt As String, delSQLStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        Dim stmtToExecute As String
        Dim rowState As DataRowState = row.RowState
        If Not changesFilter = Nothing Then
            rowState = rowState And changesFilter
        End If
        Select Case rowState
            Case DataRowState.Added
                'Insert
                If insSQLStmt Is Nothing OrElse insSQLStmt.Trim = "" Then
                    Throw New DALInvalidParameterValueException("insSQLStmt is required to execute this operation")
                End If
                stmtToExecute = insSQLStmt
            Case DataRowState.Deleted
                'delete
                If delSQLStmt Is Nothing OrElse delSQLStmt.Trim = "" Then
                    Throw New DALInvalidParameterValueException("delSQLStmt is required to execute this operation")
                End If
                stmtToExecute = delSQLStmt
            Case DataRowState.Modified
                'update
                If CheckRowChanged(row) Then
                    If updSQLStmt Is Nothing OrElse updSQLStmt.Trim = "" Then
                        Throw New DALInvalidParameterValueException("updSQLStmt is required to execute this operation")
                    End If
                    stmtToExecute = updSQLStmt
                End If
        End Select
        'If Not stmtToExecute Is Nothing Then
        '    stmtToExecute = ReplaceParameterValues(stmtToExecute, row)
        '    stmtToExecute = ReplaceParameterValues(stmtToExecute, additionalParamters)
        'End If
        If stmtToExecute IsNot Nothing Then
            ExecuteCommandWithParam(row, stmtToExecute, additionalParamters, transaction)
            row.AcceptChanges()
        End If
    End Sub

    'ALR - Added additional overload to allow for saving while using parameters.
    Public Shared Sub ExecuteWithParam(insSQLStmt As String, Paramters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing, Optional ByVal ArrayCount As Integer = 0)

        If insSQLStmt Is Nothing OrElse insSQLStmt.Trim = "" Then
            Throw New DALInvalidParameterValueException("insSQLStmt is required to execute this operation")
        Else
            ExecuteCommandWithParam(insSQLStmt, Paramters, transaction, ArrayCount)
        End If

    End Sub

    'Exceptions: 
    '   DALInvalidParameterValueException: If some the SQLStmt parameter is required but passed as nothing
    '   DALConcurrencyAccessException: If the same data is changed in parallel
    '   DataBaseAccessException: If there is an error while trying to acces the DataBase
    Public Shared Sub Execute(execSQLStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal IgnoreZeroRowsAffected As Boolean = False)
        Dim stmtToExecute As String = execSQLStmt
        If stmtToExecute Is Nothing OrElse stmtToExecute.Trim = "" Then
            Throw New DALInvalidParameterValueException("execSQLStmt is required to execute this operation")
        End If
        stmtToExecute = ReplaceParameterValues(stmtToExecute, additionalParamters)
        If stmtToExecute IsNot Nothing Then
            ExecuteCommand(stmtToExecute, transaction, IgnoreZeroRowsAffected)
        End If
    End Sub

    Private Shared Sub GenericExecute(sqlStmt As String, command As OracleCommand, _
            Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal isStoreProc As Boolean = False, Optional ByVal IgnoreZeroRowsAffected As Boolean = False)
        Dim conn As IDbConnection
        Try
            If transaction IsNot Nothing Then
                conn = transaction.Connection
            Else
                conn = New OracleConnection(ConnectString)
            End If
            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            command.Connection = conn

            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            If ((rowsAffected <= 0) AndAlso (Not command.CommandText.ToUpper.StartsWith("DELETE")) AndAlso _
                (isStoreProc = False) AndAlso (IgnoreZeroRowsAffected = False)) Then
                Throw New DALConcurrencyAccessException
            End If
        Catch ex As DALConcurrencyAccessException
            Throw ex
        Catch ex As OracleException
            Select Case ex.Number
                Case ORACLE_UNIQUE_CONSTRAINT_ERR
                    Throw New DataBaseUniqueKeyConstraintViolationException(ex)
                Case ORACLE_LENGHT_EXCEEDED_ERR
                    Throw New DataBaseLengthExceededException(ex)
                Case ORACLE_INTEGRITY_CONSTRAINT_VIOLATION
                    Throw New DataBaseIntegrityConstraintViolation(ex)
                Case DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT
                    Throw New DatabasePostalCodeValidation(ex)
                Case DB_ERROR_COMUNA_NOT_FOUND
                    Throw New DatabaseComunaException(ex)
                Case Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        Finally
            Try
                If transaction Is Nothing AndAlso (conn IsNot Nothing) AndAlso conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Private Shared Function CreateCommandSp(sqlStmt As String, inputParameters() As DBHelperParameter, _
                    ByRef outputParameters() As DBHelperParameter) As OracleCommand
        Dim command As OracleCommand
        Dim oParameter As DBHelperParameter

        command = New OracleCommand(sqlStmt)
        command.CommandType = CommandType.StoredProcedure

        ' Input Parameters
        If inputParameters IsNot Nothing Then
            For Each oParameter In inputParameters
                If Not oParameter.DBType = Nothing Then
                    command.Parameters.Add(oParameter.Name, oParameter.DBType).Value = oParameter.Value
                Else
                    command.Parameters.Add(oParameter.Name, DBNull.Value)
                End If
            Next
        End If
        ' Output Parameters
        If outputParameters IsNot Nothing Then
            For Each oParameter In outputParameters
                'command.Parameters.Add(oParameter.Name, oParameter.DBType).Direction = _
                '                        System.Data.ParameterDirection.Output
                If Not oParameter.DBType = Nothing Then
                    If oParameter.Length > 0 Then
                        ' Length
                        command.Parameters.Add(oParameter.Name, oParameter.DBType, oParameter.Length).Direction = _
                                                                   System.Data.ParameterDirection.Output
                    Else
                        command.Parameters.Add(oParameter.Name, oParameter.DBType).Direction = _
                                                                        System.Data.ParameterDirection.Output
                    End If

                Else
                    command.Parameters.Add(oParameter.Name, DBNull.Value).Direction = _
                            System.Data.ParameterDirection.Output
                End If
            Next
        End If

        Return command
    End Function

    'Public Shared Sub ExecuteSp(ByVal sqlStmt As String, ByVal inputParameters() As DBHelperParameter, ByRef outputParameters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing)
    '    Dim command As OracleCommand
    '    Dim oParameter As DBHelperParameter
    '    Dim oracleType As OracleDbType
    '    Try
    '        command = New OracleCommand(sqlStmt)
    '        command.CommandType = CommandType.StoredProcedure

    '        ' Input Parameters
    '        For Each oParameter In inputParameters
    '            If Not oParameter.DBType = Nothing Then
    '                command.Parameters.Add(oParameter.Name, oParameter.DBType).Value = oParameter.Value
    '            Else
    '                command.Parameters.Add(oParameter.Name, DBNull.Value)
    '            End If
    '        Next

    '        ' Output Parameters
    '        For Each oParameter In outputParameters
    '            'command.Parameters.Add(oParameter.Name, oParameter.DBType).Direction = _
    '            '                        System.Data.ParameterDirection.Output
    '            If Not oParameter.DBType = Nothing Then
    '                If oParameter.Length > 0 Then
    '                    ' Length
    '                    command.Parameters.Add(oParameter.Name, oParameter.DBType, oParameter.Length).Direction = _
    '                                                               System.Data.ParameterDirection.Output
    '                Else
    '                    command.Parameters.Add(oParameter.Name, oParameter.DBType).Direction = _
    '                                                                    System.Data.ParameterDirection.Output
    '                End If

    '            Else
    '                command.Parameters.Add(oParameter.Name, DBNull.Value).Direction = _
    '                        System.Data.ParameterDirection.Output
    '            End If
    '        Next
    '        GenericExecute(sqlStmt, command, transaction)

    '        ' Obtains Return Value
    '        For Each oParameter In outputParameters
    '            oParameter.Value = command.Parameters(oParameter.Name).Value
    '        Next
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    End Try
    'End Sub

    Public Shared Sub ExecuteSp(sqlStmt As String, inputParameters() As DBHelperParameter, ByRef outputParameters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, _
                                 Optional ByVal IgnoreZeroRowsAffected As Boolean = False)
        Dim command As OracleCommand
        Dim oParameter, oParameter2 As DBHelperParameter

        Try
            command = CreateCommandSp(sqlStmt, inputParameters, outputParameters)
            GenericExecute(sqlStmt, command, transaction, True, IgnoreZeroRowsAffected)

            ' Obtains Return Value
            If outputParameters IsNot Nothing Then
                For Each oParameter In outputParameters
                    oParameter.Value = command.Parameters(oParameter.Name).Value
                    oParameter.Value = GetVBValueFromOracleType(oParameter)
                Next
            End If

            ' DBHelper.GetVBValues(outputParameter)
            '  GetVBValueFromOracleType(oParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Sub ExecuteSpParamBindByName(sqlStmt As String, inputParameters() As DBHelperParameter, ByRef outputParameters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, _
                                               Optional ByVal IgnoreZeroRowsAffected As Boolean = False)
        Dim command As OracleCommand
        Dim oParameter, oParameter2 As DBHelperParameter

        Try
            command = CreateCommandSp(sqlStmt, inputParameters, outputParameters)
            command.BindByName = True
            GenericExecute(sqlStmt, command, transaction, True, IgnoreZeroRowsAffected)

            ' Obtains Return Value
            If outputParameters IsNot Nothing Then
                For Each oParameter In outputParameters
                    oParameter.Value = command.Parameters(oParameter.Name).Value
                    oParameter.Value = GetVBValueFromOracleType(oParameter)
                Next
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Sub FetchSp(sqlStmt As String, inputParameters() As DBHelperParameter,
                ByRef outputParameters() As DBHelperParameter,
                dsToLoadDataIn As DataSet, tableNameToLoadIn As String, Optional ByVal paramBindByName As Boolean = False)
        Dim command As OracleCommand
        Dim oParameter As DBHelperParameter
        Try
            command = CreateCommandSp(sqlStmt, inputParameters, outputParameters)
            If paramBindByName = True Then
                command.BindByName = True
            End If
            InternalFetch(dsToLoadDataIn, sqlStmt, tableNameToLoadIn, command)

            ' Obtains Return Value
            For Each oParameter In outputParameters
                oParameter.Value = command.Parameters(oParameter.Name).Value
                oParameter.Value = GetVBValueFromOracleType(oParameter)
            Next
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Sub

    Public Shared Function GetConnection() As OracleConnection
        Dim conn As New OracleConnection(DBHelper.ConnectString)
        conn.Open()
        Return conn
    End Function

    Public Shared Function GetNewTransaction(conn As OracleConnection) As IDbTransaction
        Return conn.BeginTransaction
    End Function

    Public Shared Function GetNewTransaction() As IDbTransaction
        Dim conn As New OracleConnection(DBHelper.ConnectString)
        conn.Open()
        Return conn.BeginTransaction
    End Function

    Public Shared Sub Commit(transaction As IDbTransaction)
        Try
            transaction.Commit()
        Catch ex As Exception
        End Try
        Try
            If (Not IsNothing(transaction.Connection)) Then
                 transaction.Connection.Close()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub RollBack(transaction As IDbTransaction)
        Try
            transaction.Rollback()
        Catch ex As Exception
        End Try
        Try
            transaction.Connection.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Shared Function ReplaceParameterValues(stmt As String, row As DataRow) As String
        Dim table As DataTable = row.Table
        Dim column As DataColumn
        Dim resultStmt As String = stmt

        For Each column In table.Columns
            Dim value As Object
            If row.RowState = DataRowState.Deleted Then
                value = row(column.ColumnName, DataRowVersion.Original)
            ElseIf row.RowState = DataRowState.Detached Then
                Throw New DALInvalidParameterValueException("Cannot save a Detached row")
            Else
                value = row(column.ColumnName)
            End If
            resultStmt = ReplaceOneParameter(resultStmt, New DBHelperParameter(column.ColumnName, value))
        Next
        Return resultStmt
    End Function

    Private Shared Function ReplaceParameterValues(stmt As String, parameters() As DBHelperParameter) As String
        Dim resultStmt As String = stmt

        If parameters IsNot Nothing Then
            Dim p As DBHelperParameter
            For Each p In parameters
                resultStmt = ReplaceOneParameter(resultStmt, p)
            Next
        End If
        Return resultStmt
    End Function

    Private Shared Function ReplaceOneParameter(stmt As String, par As DBHelperParameter) As String
        Dim upperStmt As String = stmt.ToUpper
        Dim resultStmt As String = stmt

        Dim startSearchAt As Integer = 0
        Dim pName As String = par.Name.ToUpper
        'old call commented out....
        'Dim parPosition As Integer = upperStmt.IndexOf(":" & pName, startSearchAt)
        Dim parPosition As Integer = GetParameterPosition(upperStmt, pName, startSearchAt)
        While parPosition >= 0
            Dim sqlValue As String = ValueToSQLString(par.Value)
            upperStmt = upperStmt.Substring(0, parPosition) & sqlValue & upperStmt.Substring(parPosition + pName.Length + 1, upperStmt.Length - (parPosition + pName.Length + 1))
            resultStmt = resultStmt.Substring(0, parPosition) & sqlValue & resultStmt.Substring(parPosition + pName.Length + 1, resultStmt.Length - (parPosition + pName.Length + 1))
            startSearchAt = parPosition + 1
            'old call commented out....
            'parPosition = upperStmt.IndexOf(":" & pName, startSearchAt)
            parPosition = GetParameterPosition(upperStmt, pName, startSearchAt)
        End While
        Return resultStmt
    End Function

    Private Shared Function GetParameterPosition(stmt As String, paramName As String, startPos As Integer) As Integer
        Dim retVal As Integer = -1

        ' find the position of the parameter
        Dim parPosition As Integer = stmt.IndexOf(":" & paramName, startPos)
        retVal = parPosition

        ' then, check if that is the actual parameter. this is done to  
        ' differentiate :credit_card_type  and :credit_card. 
        ' so check for the next character. if it is  a letter,number or underscore(_) then 
        ' it is not our parameter...return -1

        Dim nextCharPos As Integer = parPosition + (":" & paramName).Trim().Length
        If nextCharPos < stmt.ToCharArray.Length - 1 Then
            Dim nextChar As Char = stmt.ToCharArray.GetValue(nextCharPos)
            Dim tempRegEx As Regex = New Regex("^(_{1})|([0-9A-Za-z]{1})$", 
                                               RegexOptions.None, 
                                               TimeSpan.FromMilliseconds(100))
            Dim m As Match = tempRegEx.Match(nextChar)

            If m.Success Then ' if it is letter, number or '_' 
                retVal = -1
            End If
        End If

        Return retVal

    End Function

    Public Shared Function ValueToSQLString(value As Object) As String
        If value Is DBNull.Value OrElse value Is Nothing Then
            Return "NULL"
        Else
            If value.GetType Is GetType(Byte()) AndAlso CType(value, Byte()).Length = 16 Then
                'It must be a guid
                value = New Guid(CType(value, Byte()))
            End If
            If (value.GetType Is GetType(DateType)) Then
                Return GetSQLDate((CType(value, DateType).Value))
            ElseIf (value.GetType Is GetType(Date)) Then
                Return GetSQLDate(value)
            ElseIf (value.GetType Is GetType(String)) Then
                Return "'" & EscapeSQLReservedCharacters(value) & "'"
            ElseIf value.GetType Is GetType(Guid) Then
                If CType(value, Guid).Equals(Guid.Empty) Then
                    Return "NULL"
                Else
                    Return "HEXTORAW('" & DALBase.GuidToSQLString(value) & "')"
                End If
            Else
                'It must be a number
                Dim currentCulture As CultureInfo
                Try
                    currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture
                    Return value.ToString
                Catch ex As Exception
                Finally
                    'Restore the original culture
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture
                End Try
            End If
        End If
    End Function

    Private Shared Function EscapeSQLReservedCharacters(stringToEscape As String) As String
        Return stringToEscape.Replace("'", "''")
    End Function

    Private Shared Function GetSQLDate(dateValue As Date) As String
        Return "to_date('" & dateValue.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy HH24:MI:SS')"
    End Function

    Private Shared Sub ExecuteCommand(sqlStmt As String, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal IgnoreZeroRowsAffected As Boolean = False)
        Dim command As OracleCommand = New OracleCommand(sqlStmt)

        GenericExecute(sqlStmt, command, transaction, False, IgnoreZeroRowsAffected)
    End Sub

    Private Shared Sub ExecuteCommandWithParam(row As DataRow, sqlStmt As String, additionalParamters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing)
        Dim command As OracleCommand = New OracleCommand(sqlStmt)

        Dim conn As IDbConnection
        Try
            If transaction IsNot Nothing Then
                conn = transaction.Connection
            Else
                conn = New OracleConnection(ConnectString)
            End If
            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            command.Connection = conn

            Dim commandList As SortedList = New SortedList

            GetCommandParameters(commandList, sqlStmt, row)
            GetCommandParameters(commandList, sqlStmt, additionalParamters)

            For i As Integer = 0 To commandList.Count - 1
                command.Parameters.Add(CType(commandList.GetByIndex(i), OracleParameter))
            Next

            Dim rowsAffected As Integer = command.ExecuteNonQuery()
            If rowsAffected <= 0 Then
                Throw New DALConcurrencyAccessException
            End If
        Catch ex As DALConcurrencyAccessException
            Throw ex
        Catch ex As OracleException
            Select Case ex.Number
                Case ORACLE_UNIQUE_CONSTRAINT_ERR
                    Throw New DataBaseUniqueKeyConstraintViolationException(ex)
                Case ORACLE_LENGHT_EXCEEDED_ERR
                    Throw New DataBaseLengthExceededException(ex)
                Case ORACLE_INTEGRITY_CONSTRAINT_VIOLATION
                    Throw New DataBaseIntegrityConstraintViolation(ex)
                Case Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        Finally
            Try
                If transaction Is Nothing AndAlso (conn IsNot Nothing) AndAlso conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If
            Catch ex As Exception
            End Try
        End Try
    End Sub

    'ALR - Added to allow for a command to be executed with parameters, without using the datarow.
    Private Shared Sub ExecuteCommandWithParam(sqlStmt As String, Paramters() As DBHelperParameter, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal ArrayCount As Integer = 0)
        Dim command As OracleCommand = New OracleCommand(sqlStmt)

        Dim conn As IDbConnection
        Try
            If transaction IsNot Nothing Then
                conn = transaction.Connection
            Else
                conn = New OracleConnection(ConnectString)
            End If
            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            command.Connection = conn

            If ArrayCount > 0 Then
                command.ArrayBindCount = ArrayCount
            End If

            For i As Integer = 0 To Paramters.Length - 1
                command.Parameters.Add(New OracleParameter(Paramters(i).Name, Paramters(i).DBType, Paramters(i).Length, Paramters(i).Value, ParameterDirection.Input))
            Next

            Dim rowsAffected As Integer = command.ExecuteNonQuery()
            If rowsAffected <= 0 Then
                Throw New DALConcurrencyAccessException
            End If
        Catch ex As DALConcurrencyAccessException
            Throw ex
        Catch ex As OracleException
            Select Case ex.Number
                Case ORACLE_UNIQUE_CONSTRAINT_ERR
                    Throw New DataBaseUniqueKeyConstraintViolationException(ex)
                Case ORACLE_LENGHT_EXCEEDED_ERR
                    Throw New DataBaseLengthExceededException(ex)
                Case ORACLE_INTEGRITY_CONSTRAINT_VIOLATION
                    Throw New DataBaseIntegrityConstraintViolation(ex)
                Case Else
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Select
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        Finally
            Try
                If transaction Is Nothing AndAlso (conn IsNot Nothing) AndAlso conn.State <> ConnectionState.Closed Then
                    conn.Close()
                End If
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Private Shared Sub GetCommandParameters(ByRef commandList As SortedList, stmt As String, row As DataRow)
        Dim table As DataTable = row.Table
        Dim column As DataColumn
        For Each column In table.Columns
            Dim value As Object
            If row.RowState = DataRowState.Deleted Then
                value = row(column.ColumnName, DataRowVersion.Original)
            ElseIf row.RowState = DataRowState.Detached Then
                Throw New DALInvalidParameterValueException("Cannot save a Detached row")
            Else
                value = row(column.ColumnName)
            End If
            If value.GetType Is GetType(DBNull) Then
                ReplaceCommandParameter(commandList, stmt, New DBHelperParameter(column.ColumnName, value, column.DataType))
            Else
                ReplaceCommandParameter(commandList, stmt, New DBHelperParameter(column.ColumnName, value))
            End If
        Next
    End Sub

    Private Shared Sub GetCommandParameters(ByRef commandList As SortedList, stmt As String, parameters() As DBHelperParameter)
        Dim resultStmt As String = stmt

        If parameters IsNot Nothing Then
            Dim p As DBHelperParameter
            For Each p In parameters
                ReplaceCommandParameter(commandList, resultStmt, p)
            Next
        End If
    End Sub

    Private Shared Sub ReplaceCommandParameter(ByRef commandList As SortedList, stmt As String, par As DBHelperParameter)
        Dim upperStmt As String = stmt.ToUpper
        Dim resultStmt As String = stmt

        Dim startSearchAt As Integer = 0
        Dim pName As String = par.Name.ToUpper
        Dim oParam As OracleParameter

        Dim parPosition As Integer = GetParameterPosition(upperStmt, pName, startSearchAt)
        While parPosition >= 0
            If par.DBType = OracleDbType.Raw Then
                oParam = New OracleParameter(pName, par.DBType)
                oParam.Value = New Guid(CType(par.Value, Byte())).ToByteArray()
                commandList.Add(parPosition, oParam)
            ElseIf par.DBType = OracleDbType.Blob Then
                oParam = New OracleParameter(pName, par.DBType, par.Length)
                oParam.Value = CType(par.Value, Byte())
                commandList.Add(parPosition, oParam)
            ElseIf par.Length > 0 Then
                oParam = New OracleParameter(pName, par.DBType, par.Length)
                oParam.Value = par.Value
                commandList.Add(parPosition, oParam)
            Else
                oParam = New OracleParameter(pName, par.DBType)
                oParam.Value = par.Value
                commandList.Add(parPosition, oParam)
            End If

            startSearchAt = parPosition + 1
            parPosition = GetParameterPosition(upperStmt, pName, startSearchAt)
        End While
    End Sub

    Public Shared Function CheckRowChanged(row As DataRow) As Boolean
        Dim column As DataColumn
        For Each column In row.Table.Columns
            If Not column.ColumnName.ToUpper.StartsWith("_SYSTEM") Then
                If CheckColumnChanged(column.ColumnName, row) Then Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function CheckColumnChanged(colName As String, row As DataRow) As Boolean
        Dim col As DataColumn

        col = row.Table.Columns(colName)

        'null checks
        If row.IsNull(col, DataRowVersion.Current) AndAlso row.IsNull(col, DataRowVersion.Original) Then
            Return False
        End If
        If row.IsNull(col, DataRowVersion.Current) AndAlso Not (row.IsNull(col, DataRowVersion.Original)) Then
            Return True
        End If
        If Not (row.IsNull(col, DataRowVersion.Current)) AndAlso row.IsNull(col, DataRowVersion.Original) Then
            Return True
        End If

        'checks for each data type - brute force method - there is probably a better way

        If col.DataType Is GetType(String) Then
            Dim cur As String = CType(row(col, DataRowVersion.Current), String)
            Dim old As String = CType(row(col, DataRowVersion.Original), String)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(DateTime) Then
            Dim cur As DateTime = CType(row(col, DataRowVersion.Current), DateTime)
            Dim old As DateTime = CType(row(col, DataRowVersion.Original), DateTime)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Decimal) Then
            Dim cur As Decimal = CType(row(col, DataRowVersion.Current), Decimal)
            Dim old As Decimal = CType(row(col, DataRowVersion.Original), Decimal)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Guid) Then
            Dim cur As Guid = CType(row(col, DataRowVersion.Current), Guid)
            Dim old As Guid = CType(row(col, DataRowVersion.Original), Guid)
            If old.Equals(cur) Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(System.Byte()) Then
            Dim curB As Byte() = CType(row(col, DataRowVersion.Current), Byte())
            Dim oldB As Byte() = CType(row(col, DataRowVersion.Original), Byte())
            If curB.Length <> 16 Then Throw New ApplicationException("BusinessObjectBase.CheckColumnChanged only supports Byte Arrays that represent GUIDs")
            If oldB.Length <> 16 Then Throw New ApplicationException("BusinessObjectBase.CheckColumnChanged only supports Byte Arrays that represent GUIDs")
            Dim cur As New Guid(curB)
            Dim old As New Guid(oldB)
            If old.Equals(cur) Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Integer) Then
            Dim cur As Integer = CType(row(col, DataRowVersion.Current), Integer)
            Dim old As Integer = CType(row(col, DataRowVersion.Original), Integer)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(System.Int16) Then
            Dim cur As Int16 = CType(row(col, DataRowVersion.Current), Int16)
            Dim old As Int16 = CType(row(col, DataRowVersion.Original), Int16)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Long) Then
            Dim cur As Long = CType(row(col, DataRowVersion.Current), Long)
            Dim old As Long = CType(row(col, DataRowVersion.Original), Long)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Single) Then
            Dim cur As Single = CType(row(col, DataRowVersion.Current), Single)
            Dim old As Single = CType(row(col, DataRowVersion.Original), Single)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Double) Then
            Dim cur As Double = CType(row(col, DataRowVersion.Current), Double)
            Dim old As Double = CType(row(col, DataRowVersion.Original), Double)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        'if we get this far throw an exception
        Throw New ApplicationException("Please modify BusinessObject.CheckColumnChanged to handle the following Datatype: " & col.DataType.ToString)

    End Function

    'OracleDbType

#Region "Parameter Class"
    Public Class DBHelperParameter

        Public Name As String
        Public Length As Integer
        Public Value As Object
        Public DBType As OracleDbType

        ' Input Parameter
        Public Sub New(name As String, value As Object, Optional ByVal vbType As Type = Nothing)
            Dim valueLength As Integer
            If (value IsNot Nothing) AndAlso value.GetType Is GetType(Guid) Then
                value = CType(value, Guid).ToByteArray
            End If
            Me.Name = name
            Me.Value = value

            If (value IsNot Nothing) AndAlso (value.GetType IsNot GetType(DBNull)) Then

                valueLength = GetValueLength(value)

                If value.GetType.BaseType.Equals(GetType(Array)) AndAlso Not value.GetType.Equals(GetType(Byte())) Then
                    If value.GetType.Equals(GetType(Byte()())) Then
                        valueLength = 16
                    End If
                End If

                '10/18/2006 - ALR - Added this to force a type if it has been passed.
                If vbType Is Nothing Then
                    DBType = GetOracleTypeFromVB(value, valueLength, Me)
                Else
                    DBType = GetOracleTypeFromVBWithType(vbType, valueLength, Me)
                End If

            Else
                If vbType Is Nothing Then
                    DBType = Nothing
                Else
                    DBType = GetOracleTypeFromVBType(vbType, 0, Me)
                End If
                valueLength = 0
            End If
            Length = valueLength

        End Sub

        ' Output Parameter
        Public Sub New(name As String, vbType As Type, Optional ByVal len As Integer = 16)
            Me.Name = name
            Value = Nothing
            ' Me.Length = len
            Length = 0
            If (vbType IsNot Nothing) AndAlso (vbType IsNot GetType(DBNull)) Then
                DBType = GetOracleTypeFromVBType(vbType, len, Me)
            Else
                DBType = Nothing
            End If

        End Sub
    End Class
#End Region

#End Region

#Region "Type-Mapping"

    Private Shared Function GetOracleTypeFromVB(value As Object, len As Integer, _
                                     oParam As DBHelperParameter) As OracleDbType
        Return GetOracleTypeFromVBType(value.GetType, len, oParam)
    End Function

    Private Shared Function GetOracleTypeFromVBWithType(value As Type, len As Integer, _
                                    oParam As DBHelperParameter) As OracleDbType
        Return GetOracleTypeFromVBType(value, len, oParam)
    End Function

    Private Shared Function GetOracleTypeFromVBType(oType As Type, len As Integer, _
    oParam As DBHelperParameter) As OracleDbType
        Dim oracleType As OracleDbType

        If ((oType Is GetType(Byte())) AndAlso (len = 16)) OrElse (oType Is GetType(Guid)) Then
            oracleType = OracleDbType.Raw
            oParam.Length = len
        ElseIf oType Is GetType(Byte()) Then
            oracleType = OracleDbType.Blob
            oParam.Length = len
        ElseIf oType Is GetType(String) Then
            oracleType = OracleDbType.Varchar2
            oParam.Length = len
        ElseIf oType Is GetType(Char) Then
            oracleType = OracleDbType.Char
        ElseIf oType Is GetType(Short) Then
            oracleType = OracleDbType.Int16
        ElseIf oType Is GetType(Integer) Then
            oracleType = OracleDbType.Int32
        ElseIf (oType Is GetType(Decimal)) OrElse (oType Is GetType(DecimalType)) _
                OrElse (oType Is GetType(Single)) OrElse (oType Is GetType(Double)) _
                OrElse (oType Is GetType(Long)) OrElse (oType Is GetType(LongType)) Then
            oracleType = OracleDbType.Decimal
        ElseIf (oType Is GetType(Date)) OrElse (oType Is GetType(DateTime)) _
                OrElse (oType Is GetType(DateType)) Then
            oracleType = OracleDbType.TimeStamp
        ElseIf oType Is GetType(DataSet) Then
            oracleType = OracleDbType.RefCursor
            '10/3/2006 - ALR - Added XML Type to parameter definition to enable sending XML
        ElseIf oType Is GetType(Xml.XmlDocument) Then
            oracleType = OracleDbType.XmlType
        ElseIf oType Is GetType(System.Text.StringBuilder) Then
            oracleType = OracleDbType.Clob
            oParam.Length = len
        Else
            Throw New ArgumentException
        End If

        Return oracleType
    End Function
    Private Shared Function GetValueLength(value As Object) As Integer
        Dim valueLength As Integer = 0
        Dim oType As Type = value.GetType

        If oType Is GetType(Guid) Then
            valueLength = 16
        ElseIf oType Is GetType(Byte()) Then
            valueLength = CType(value, Byte()).Length
        ElseIf oType Is GetType(String) Then
            valueLength = CType(value, String).Length
        End If
        Return valueLength
    End Function

    Public Shared Function GetVBValues(parameters() As DBHelperParameter) As ArrayList
        Dim oVBValues As New ArrayList
        Dim oParameter As DBHelperParameter

        For Each oParameter In parameters
            oVBValues.Add(GetVBValueFromOracleType(oParameter))
        Next
        Return oVBValues
    End Function

    Private Shared Function GetVBValueFromOracleType(oParameter As DBHelperParameter) As Object
        Dim oValue As Object

        'TODO Blob Types
        If oParameter.Value.GetType Is GetType(OracleBinary) AndAlso Not CType(oParameter.Value, INullable).IsNull Then
            'Guids
            oValue = New Guid(CType(oParameter.Value, OracleBinary).Value)
        ElseIf oParameter.Value.GetType() Is GetType(OracleString) AndAlso Not CType(oParameter.Value, INullable).IsNull Then

            If (oParameter.Value.ToString().ToLowerInvariant().Equals("null")) Then
                oValue = String.Empty
            Else
                oValue = CType(oParameter.Value, OracleString).Value
            End If
            'TODO char, short
        ElseIf oParameter.Value.GetType() Is GetType(Integer) AndAlso Not CType(oParameter.Value, INullable).IsNull Then
            oValue = oParameter.Value
        ElseIf oParameter.Value.GetType() Is GetType(OracleDecimal) AndAlso Not CType(oParameter.Value, INullable).IsNull Then
            oValue = CType(oParameter.Value, OracleDecimal).Value
            'TODO date type
        ElseIf oParameter.Value.GetType() Is GetType(System.DBNull) Then
            ' In order to obtain information from the RefCursor. We need to call fetchSp
            oValue = Nothing
        ElseIf CType(oParameter.Value, INullable) IsNot Nothing AndAlso CType(oParameter.Value, INullable).IsNull Then
            oValue = Nothing
        Else
            Throw New ArgumentException
        End If

        Return oValue
    End Function

    Public Shared Function GetValueOrDBNull(obj As DateType) As Object
        Return If(obj Is Nothing, DBNull.Value, obj.Value)
    End Function

    Public Shared Function GetValueOrDBNull(obj As DecimalType) As Object
        Return If(obj Is Nothing, DBNull.Value, obj.Value)
    End Function
#End Region

End Class
