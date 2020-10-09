Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions

''' <summary>
''' This module contains various helper and extention methods related to Oracle Data Access.
''' </summary>
''' <remarks></remarks>
Friend Module OracleDbHelper

#Region "Fetch"

    ''' <summary>
    ''' Executes a Fetch (Select) Operation on database and returns result in <see cref="System.Data.DataSet" /> object.
    ''' </summary>
    ''' <param name="sql">SQL String to be executed against database.</param>
    ''' <param name="tableName">Optional. Name of <see cref="System.Data.DataTable" />.</param>
    ''' <param name="familyDs">Optional. Instance of <see cref="System.Data.DataSet" /> to be used for populating data from Database. When the value
    ''' is not supplied or is null then a new instance of <see cref="System.Data.DataSet" /> will be created and used for population
    ''' of data.</param>
    ''' <param name="dataSetName">Optional. Name of <see cref="System.Data.DataSet" />.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter sql is null or contains only white spaces.</exception>
    Public Function Fetch(sql As String, _
                          Optional ByVal tableName As String = Nothing, _
                          Optional ByVal familyDs As DataSet = Nothing, _
                          Optional ByVal dataSetName As String = Nothing) As DataSet

        If (sql Is Nothing OrElse sql.Trim().Length = 0) Then
            Throw New ArgumentNullException("sql")
        End If

        ' Create new Oracle Data Adapter and assemble Connection
        Using command As New OracleCommand(sql)
            Return Fetch(command, tableName, familyDs, dataSetName)
        End Using

    End Function

    ''' <summary>
    ''' Executes a Fetch (Select) Operation on database and returns result in <see cref="System.Data.DataSet" /> object.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" object to be exeucted as fetch opration.</param>
    ''' <param name="tableName">Optional. Name of <see cref="System.Data.DataTable" />.</param>
    ''' <param name="familyDs">Optional. Instance of <see cref="System.Data.DataSet" /> to be used for populating data from Database. When the value
    ''' is not supplied or is null then a new instance of <see cref="System.Data.DataSet" /> will be created and used for population
    ''' of data.</param>
    ''' <param name="dataSetName">Optional. Name of <see cref="System.Data.DataSet" />.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter command is null.</exception>
    Public Function Fetch(command As OracleCommand, _
                          Optional ByVal tableName As String = Nothing, _
                          Optional ByVal familyDs As DataSet = Nothing, _
                          Optional ByVal dataSetName As String = Nothing) As DataSet

        ' Validate Mandatory Parameters
        If (command Is Nothing) Then
            Throw New ArgumentNullException("command")
        End If

        ' Initialize Optional Parameters
        If (familyDs Is Nothing) Then
            familyDs = New DataSet()
            If Not dataSetName Is Nothing Then familyDs.DataSetName = dataSetName
        End If

        If (command.Connection Is Nothing) Then
            ' Create new Oracle Data Adapter and assemble Connection
            Using conn As New OracleConnection(DBHelper.ConnectString)
                command.Connection = conn
                Using dataAdapter As New OracleDataAdapter(command)
                    dataAdapter.Fill(familyDs, tableName)
                End Using
            End Using
        Else
            ' Create new Oracle Data Adapter (Connection and optionally transaction is already setup in Command by caller)
            Using dataAdapter As New OracleDataAdapter(command)
                dataAdapter.Fill(familyDs, tableName)
            End Using
        End If

        ' Set Locale Information on DS
        familyDs.Locale = CultureInfo.InvariantCulture

        Return familyDs

    End Function

    ''' <summary>
    ''' Executes a multiple Fetch (Select) Operation on database and returns result in <see cref="System.Data.DataSet" /> object.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" object to be exeucted as fetch opration.</param>
    ''' <param name="tableNames">Array of <see cref="System.Data.DataTable" /> Name.</param>
    ''' <param name="familyDs">Optional. Instance of <see cref="System.Data.DataSet" /> to be used for populating data from Database. When the value
    ''' is not supplied or is null then a new instance of <see cref="System.Data.DataSet" /> will be created and used for population
    ''' of data.</param>
    ''' <param name="dataSetName">Optional. Name of <see cref="System.Data.DataSet" />.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter command is null.</exception>
    Public Function Fetch(command As OracleCommand, _
                          tableName() As String, _
                          Optional ByVal familyDs As DataSet = Nothing, _
                          Optional ByVal dataSetName As String = Nothing) As DataSet

        ' Validate Mandatory Parameters
        If (command Is Nothing) Then
            Throw New ArgumentNullException("command")
        End If

        ' Initialize Optional Parameters
        If (familyDs Is Nothing) Then
            familyDs = New DataSet()
            If Not dataSetName Is Nothing Then familyDs.DataSetName = dataSetName
        End If

        If (command.Connection Is Nothing) Then
            ' Create new Oracle Data Adapter and assemble Connection
            Using conn As New OracleConnection(DBHelper.ConnectString)
                command.Connection = conn
                Using dataAdapter As New OracleDataAdapter(command)
                    dataAdapter.Fill(familyDs, tableName(0))
                End Using
            End Using
        Else
            ' Create new Oracle Data Adapter (Connection and optionally transaction is already setup in Command by caller)
            Using dataAdapter As New OracleDataAdapter(command)
                dataAdapter.Fill(familyDs, tableName(0))
            End Using
        End If

        ' Update Data Table Names
        For i As Integer = 1 To Math.Min(familyDs.Tables.Count - 1, UBound(tableName) - LBound(tableName))
            familyDs.Tables(i).TableName = tableName(LBound(tableName) + i)
        Next

        ' Set Locale Information on DS
        familyDs.Locale = CultureInfo.InvariantCulture

        Return familyDs

    End Function

#End Region

#Region "Execute Non-Query"

    ''' <summary>
    ''' Executes a SQL or Function represented by command object which does not return any response.
    ''' </summary>
    ''' <param name="command"><see cref="Oracle.DataAccess.Client.OracleCommand" object to be exeucted as execute scalar opration.</param>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter command is null.</exception>
    Public Sub ExecuteNonQuery(command As OracleCommand)

        ' Validate Mandatory Parameters
        If (command Is Nothing) Then
            Throw New ArgumentNullException("command")
        End If

        ' If Command already has connection set-up then use command connection otherwise create and open a new connection.
        If (command.Connection Is Nothing) Then

            ' Create and assemble Connection
            Using conn As New OracleConnection(DBHelper.ConnectString)
                If Not conn.State = ConnectionState.Open Then
                    conn.Open()
                End If
                command.Connection = conn
                ' Execute
                command.ExecuteNonQuery()
            End Using

        Else

            command.ExecuteNonQuery()

        End If
    End Sub

    ''' <summary>
    ''' Executes a SQL or Function represented by SQL String and Executes a SQL which does not return any response
    ''' </summary>
    ''' <param name="sql">SQL String to be executed against database.</param>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter sql is null or contains only white spaces.</exception>
    Public Sub ExecuteNonQuery(sql As String)

        If (sql Is Nothing OrElse sql.Trim().Length = 0) Then
            Throw New ArgumentNullException("sql")
        End If

        ' Create and Assemble Command
        Using command As New OracleCommand(sql)

            ' Execute
            ExecuteNonQuery(command)
        End Using

    End Sub

#End Region

#Region "Execute Scalar"

    ''' <summary>
    ''' Executes a SQL or Function represented by command object and fetches Single Scaler Value.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" object to be exeucted as execute scalar opration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter command is null.</exception>
    Public Function ExecuteScalar(command As OracleCommand) As Object

        ' Validate Mandatory Parameters
        If (command Is Nothing) Then
            Throw New ArgumentNullException("command")
        End If

        ' If Command already has connection set-up then use command connection otherwise create and open a new connection.
        If (command.Connection Is Nothing) Then

            ' Create and assemble Connection
            Using conn As New OracleConnection(DBHelper.ConnectString)
                command.Connection = conn

                ' Execute
                Return command.ExecuteScalar()
            End Using

        Else

            Return command.ExecuteScalar()

        End If
    End Function

    ''' <summary>
    ''' Executes a SQL or Function represented by SQL String and fetches Single Scaler Value.
    ''' </summary>
    ''' <param name="sql">SQL String to be executed against database.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter sql is null or contains only white spaces.</exception>
    Public Function ExecuteScalar(sql As String) As Object

        If (sql Is Nothing OrElse sql.Trim().Length = 0) Then
            Throw New ArgumentNullException("sql")
        End If

        ' Create and Assemble Command
        Using command As New OracleCommand(sql)

            ' Execute
            Return ExecuteScalar(command)
        End Using

    End Function

    ''' <summary>
    ''' Executes a SQL or Function represented by SQL String and fetches Single Scaler Value.
    ''' </summary>
    ''' <typeparam name="T">Expected Value Data Type of Result</typeparam>
    ''' <param name="command"><see cref="OracleCommand" object to be exeucted as execute scalar opration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter command is null.</exception>
    Public Function ExecuteScalar(Of T As Structure)(command As OracleCommand) As Nullable(Of T)
        Return New Nullable(Of T)(DirectCast(ExecuteScalar(command), T))
    End Function

    ''' <summary>
    ''' Executes a SQL or Function represented by SQL String and fetches Single Scaler Value.
    ''' </summary>
    ''' <typeparam name="T">Expected Value Data Type of Result</typeparam>
    ''' <param name="sql">SQL String to be executed against database.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentException">When parameter sql is null or contains only white spaces.</exception>
    Public Function ExecuteScalar(Of T As Structure)(sql As String) As Nullable(Of T)
        Return New Nullable(Of T)(DirectCast(ExecuteScalar(sql), T))
    End Function

#End Region

#Region "Connection Helper Methods"

    ''' <summary>
    ''' Creates a new Instance of <see cref="OracleConnection" /> using application connection string and opens a connection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateConnection() As OracleConnection

        Dim returnValue As OracleConnection

        returnValue = New OracleConnection(DBHelper.ConnectString)
        returnValue.Open()

        Return returnValue

    End Function

    ''' <summary>
    ''' Begins new <see cref="OracleTransaction" /> for connection provided in parameter and for <see cref="System.Data.IsolationLevel" /> provided in parameter
    ''' </summary>
    ''' <param name="connection"><see cref="OracleConnection" /> to be used for Begining a Transaction.</param>
    ''' <returns></returns>
    ''' <remarks>If Connection is null then function creates a new <see cref="OracleConnection" /> and then creates a <see cref="OracleTransaction" /> 
    ''' using newly created connection. </remarks>
    Public Function BeginTransaction(Optional ByVal connection As OracleConnection = Nothing) As OracleTransaction
        If connection Is Nothing Then connection = CreateConnection()
        Return connection.BeginTransaction()
    End Function

    ''' <summary>
    ''' Commits the changes made for <see cref="OracleTransaction" /> to database and then closes <see cref="OracleConnection" />
    ''' </summary>
    ''' <param name="transaction"><see cref="OracleTransaction" /> object to be Committed to Database.</param>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When parameter transaction is null.</exception>
    <DebuggerNonUserCode()> _
    Public Sub Commit(transaction As OracleTransaction)

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")

        Try
            transaction.Commit()
        Catch ex As Exception
        End Try

        Try
            transaction.Connection.Close()
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Rollbacks the changes made for <see cref="OracleTransaction" /> to database and then closes <see cref="OracleConnection" />
    ''' </summary>
    ''' <param name="transaction"><see cref="OracleTransaction" /> object to be Rollbacked from Database.</param>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When parameter transaction is null.</exception>
    <DebuggerNonUserCode()> _
    Public Sub Rollback(transaction As OracleTransaction)

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")

        Try
            transaction.Rollback()
        Catch ex As Exception
        End Try

        Try
            transaction.Connection.Close()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Command Helper Methods"

    ''' <summary>
    ''' Creates new <see cref="OracleCommand" /> object. Assembles connection & transaction objects.
    ''' </summary>
    ''' <param name="sql">The text command to run against the data source</param>
    ''' <param name="commandType">Specifies whether command is SQL Statement or Stored Procedure or Table Name</param>
    ''' <param name="connection">Instance of <see cref="OracleConnection" /> object if existing connection object is to be used.</param>
    ''' <param name="transaction">Instance of <see cref="OracleTransaction" /> object if existing transaction object is to be used.</param>
    ''' <returns></returns>
    ''' <remarks>Transaction and Connection property of command is set to default value (null) when both connection and transaction parameters are nothing. This function does
    ''' not create new instance of connection if the values for connection and/or transaction are not supplied.</remarks>
    ''' <exception cref="System.InvalidOperationException">When both connection & transaction parameters are supplied and transaction.Connection is not same as connection.</exception>
    Public Function CreateCommand(sql As String, _
                                  Optional ByVal commandType As CommandType = CommandType.StoredProcedure, _
                                  Optional ByVal connection As OracleConnection = Nothing, _
                                  Optional ByVal transaction As OracleTransaction = Nothing) As OracleCommand

        Dim returnValue As OracleCommand

        ' Validate that Connection and Transaction.Connection is same object when both Connection and Transaction are supplied.
        If (Not connection Is Nothing AndAlso _
            Not transaction Is Nothing AndAlso _
            Not connection.Equals(transaction.Connection)) Then
            Throw New InvalidOperationException("Transaction does not belong to connection")
        End If

        returnValue = New OracleCommand()

        ' Use connection and transaction parameter values in command if supplied
        If (Not transaction Is Nothing) Then
            returnValue.Connection = connection
            'returnValue.Transaction = transaction
        Else
            If (Not connection Is Nothing) Then returnValue.Connection = connection
        End If

        ' Set Command Type and Text.
        With returnValue
            .CommandType = commandType
            .CommandText = sql
        End With

        Return returnValue

    End Function

    ''' <summary>
    ''' Creates a New <see cref="OracleParameter" /> using command and sets the properties supplied as parameters
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for creating Parameters.</param>
    ''' <param name="parameterName">Name of Parameter</param>
    ''' <param name="dbType"><see cref="OracleDbType" /> of Parameter</param>
    ''' <param name="value">Optional. Value of Parameter. Guid Value should be supplied as array of <see cref="System.Byte" />. Use <see cref="System.Guid"/>.ToByteArray() method to convert Guid to Byte Array.</param>
    ''' <param name="direction">Optional. Direction of Parameter like Input, Output, InputOutput or Return</param>
    ''' <param name="size">Optional. Size of Parameter is one of VARCAHR2, CHAR, NVARCHAR2, NCHAR, TEXT, NTEXT, RAW etc.</param>
    ''' <param name="sourceColumn">Optional. <see cref="System.Data.DataTable" /> Source Column Name in case creating the parameter for <see cref="OracleDataAdapter" /></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When command parameter is null</exception>
    ''' <exception cref="System.ArgumentNullException">When parameterName parameter is null</exception>
    <Extension()> _
    Public Function AddParameter(command As OracleCommand, _
                                 parameterName As String, _
                                 dbType As OracleDbType, _
                                 size As Nullable(Of Integer), _
                                 Optional ByVal value As Object = Nothing, _
                                 Optional ByVal direction As ParameterDirection = ParameterDirection.Input, _
                                 Optional ByVal sourceColumn As String = Nothing) As OracleCommand
        Return AddParameter(command, parameterName, dbType, size, Nothing, Nothing, value, direction, sourceColumn)
    End Function

    ''' <summary>
    ''' Creates a New <see cref="OracleParameter" /> using command and sets the properties supplied as parameters
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for creating Parameters.</param>
    ''' <param name="parameterName">Name of Parameter</param>
    ''' <param name="dbType"><see cref="OracleDbType" /> of Parameter</param>
    ''' <param name="value">Optional. Value of Parameter. Guid Value should be supplied as array of <see cref="System.Byte" />. Use <see cref="System.Guid"/>.ToByteArray() method to convert Guid to Byte Array.</param>
    ''' <param name="direction">Optional. Direction of Parameter like Input, Output, InputOutput or Return</param>
    ''' <param name="scale">Optional. Scale of Parameter if parameter is NUMBER type</param>
    ''' <param name="precision">Optional. Precision of Parameter if parameter is NUMBER type</param>
    ''' <param name="sourceColumn">Optional. <see cref="System.Data.DataTable" /> Source Column Name in case creating the parameter for <see cref="OracleDataAdapter" /></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When command parameter is null</exception>
    ''' <exception cref="System.ArgumentNullException">When parameterName parameter is null</exception>
    <Extension()> _
    Public Function AddParameter(command As OracleCommand, _
                                 parameterName As String, _
                                 dbType As OracleDbType, _
                                 scale As Nullable(Of Byte), _
                                 precision As Nullable(Of Byte), _
                                 Optional ByVal value As Object = Nothing, _
                                 Optional ByVal direction As ParameterDirection = ParameterDirection.Input, _
                                 Optional ByVal sourceColumn As String = Nothing) As OracleCommand
        Return AddParameter(command, parameterName, dbType, Nothing, scale, precision, value, direction, sourceColumn)
    End Function

    ''' <summary>
    ''' Creates a New <see cref="OracleParameter" /> using command and sets the properties supplied as parameters
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for creating Parameters.</param>
    ''' <param name="parameterName">Name of Parameter</param>
    ''' <param name="dbType"><see cref="OracleDbType" /> of Parameter</param>
    ''' <param name="value">Optional. Value of Parameter. Guid Value should be supplied as array of <see cref="System.Byte" />. Use <see cref="System.Guid"/>.ToByteArray() method to convert Guid to Byte Array.</param>
    ''' <param name="direction">Optional. Direction of Parameter like Input, Output, InputOutput or Return</param>
    ''' <param name="sourceColumn">Optional. <see cref="System.Data.DataTable" /> Source Column Name in case creating the parameter for <see cref="OracleDataAdapter" /></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When command parameter is null</exception>
    ''' <exception cref="System.ArgumentNullException">When parameterName parameter is null</exception>
    <Extension()> _
    Public Function AddParameter(command As OracleCommand, _
                                 parameterName As String, _
                                 dbType As OracleDbType, _
                                 Optional ByVal value As Object = Nothing, _
                                 Optional ByVal direction As ParameterDirection = ParameterDirection.Input, _
                                 Optional ByVal sourceColumn As String = Nothing) As OracleCommand
        Return AddParameter(command, parameterName, dbType, Nothing, Nothing, Nothing, value, direction, sourceColumn)
    End Function

    ''' <summary>
    ''' Creates a New <see cref="OracleParameter" /> using command and sets the properties supplied as parameters
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for creating Parameters.</param>
    ''' <param name="parameterName">Name of Parameter</param>
    ''' <param name="dbType"><see cref="OracleDbType" /> of Parameter</param>
    ''' <param name="value">Optional. Value of Parameter. Guid Value should be supplied as array of <see cref="System.Byte" />. Use <see cref="System.Guid"/>.ToByteArray() method to convert Guid to Byte Array.</param>
    ''' <param name="direction">Optional. Direction of Parameter like Input, Output, InputOutput or Return</param>
    ''' <param name="size">Optional. Size of Parameter is one of VARCAHR2, CHAR, NVARCHAR2, NCHAR, TEXT, NTEXT, RAW etc.</param>
    ''' <param name="scale">Optional. Scale of Parameter if parameter is NUMBER type</param>
    ''' <param name="precision">Optional. Precision of Parameter if parameter is NUMBER type</param>
    ''' <param name="sourceColumn">Optional. <see cref="System.Data.DataTable" /> Source Column Name in case creating the parameter for <see cref="OracleDataAdapter" /></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <exception cref="System.ArgumentNullException">When command parameter is null</exception>
    ''' <exception cref="System.ArgumentNullException">When parameterName parameter is null</exception>
    <Extension()> _
    Public Function AddParameter(command As OracleCommand, _
                                 parameterName As String, _
                                 dbType As OracleDbType, _
                                 size As Nullable(Of Integer), _
                                 scale As Nullable(Of Byte), _
                                 precision As Nullable(Of Byte), _
                                 Optional ByVal value As Object = Nothing, _
                                 Optional ByVal direction As ParameterDirection = ParameterDirection.Input, _
                                 Optional ByVal sourceColumn As String = Nothing) As OracleCommand
        Dim param As OracleParameter

        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (parameterName Is Nothing) Then Throw New ArgumentNullException("parameterName")

        param = command.CreateParameter()

        With param
            .ParameterName = parameterName
            .OracleDbType = dbType
            .Direction = direction
            If (scale.HasValue) Then .Scale = scale.Value
            If (precision.HasValue) Then .Precision = precision.Value
            If (size.HasValue) Then .Size = size.Value
            .Value = value
            If (Not sourceColumn Is Nothing) Then .SourceColumn = sourceColumn
        End With

        command.Parameters.Add(param)

        Return command

    End Function

#End Region

#Region "Utility Helper Methods"
    Public Function FormatWildCard(inputString As String) As String
        If (Not inputString Is Nothing) AndAlso (Not (inputString.Equals(String.Empty))) Then
            Dim replacementString As String = "%"
            Dim matchPattern As String = "[*]{1,}"
            Return Regex.Replace(inputString, matchPattern, replacementString)
        Else
            Return inputString
        End If
    End Function
    Public Function ReplaceParameter(inputString As String, inputParameter As String, inputValue As Guid) As String

        Return inputString.Replace(":" & inputParameter, "hextoraw('" & DALBase.GuidToSQLString(inputValue) & "')")
    End Function
    Public Function ReplaceParameter(inputString As String, inputParameter As String, inputValue As String) As String

        Return inputString.Replace(":" & inputParameter, "'" & inputValue & "'")
    End Function

#End Region
End Module
