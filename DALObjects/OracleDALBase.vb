''' <summary>
''' Represents a Data Access Layer for specific database table. Provides a base class for Oracle specific implementation of Data Access.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class OracleDALBase : Inherits DALBase

#Region "Constants"
    Public Const PAR_I_NAME_CREATED_DATE As String = "pi_created_date"
    Public Const PAR_I_NAME_MODIFIED_DATE As String = "pi_modified_date"
    Public Const PAR_I_NAME_CREATED_BY As String = "pi_created_by"
    Public Const PAR_I_NAME_MODIFIED_BY As String = "pi_modified_by"
    Public Const PAR_O_NAME_RESULTCURSOR As String = "po_ResultCursor"
    Public Const PAR_I_NAME_LANGUAGE_ID As String = "pi_language_id"
#End Region

#Region "Abstract Methods"

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Update <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected MustOverride Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Insert <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected MustOverride Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)

    ''' <summary>
    ''' Derived class should add <see cref="OracleParameter" /> objects to <see cref="OracleCommand" /> 
    ''' object sent as parameter. <see cref="OracleParameter" /> should have correct Column Mapping. This Mapping is used by 
    ''' <see cref="OracleDataAdapter" /> to Delete <see cref="System.Data.DataSet" /> to Oracle Database.
    ''' </summary>
    ''' <param name="command"><see cref="OracleCommand" /> to be used for adding <see cref="OracleParameter" />.</param>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <remarks></remarks>
    Protected MustOverride Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Default Constructor. Access Specifier "Friend" makes it impossible to use this class as Base Class for any class outside this assembly.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        '' Security Constructor
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Transfers the changes made in <see cref="System.Data.DataTable" /> to Oracle Database.
    ''' </summary>
    ''' <param name="table"><see cref="System.Data.DataTable" /> containing changes to be Updated (Inserts/Updates/Deletes)</param>
    ''' <param name="transaction">Optional. Instance of <see cref="OracleTransaction" />. When the value is provided, the updates 
    ''' will be part of the transaction represented by value of this parameter. Also, the <see cref="OracleConnection" /> used by 
    ''' this <see cref="OracleTransaction" /> will be used for all <see cref="OracleCommand"> objects during
    ''' updates to database.</param>
    ''' <param name="changesFilter">Optional. When value is provided; only rows having one of the flag values will be updated.</param>
    ''' <remarks></remarks>
    Public Overridable Sub Update(ByVal table As DataTable, _
                                  Optional ByVal transaction As OracleTransaction = Nothing, _
                                  Optional ByVal changesFilter As DataRowState = DataRowState.Added Or DataRowState.Deleted Or DataRowState.Modified)

        ' When Filter is blank then do not do anything.
        If (Not (changesFilter Or DataRowState.Added = DataRowState.Added OrElse _
                 changesFilter Or DataRowState.Modified = DataRowState.Modified OrElse _
                 changesFilter Or DataRowState.Deleted = DataRowState.Deleted)) Then
            Return
        End If

        '' Create Data Adapter and Process Request
        Using da As OracleDataAdapter = CreateDataAdapter(table.TableName, transaction, changesFilter)
            ' First Execute Inserts, then Updates and finally Deletes
            If ((changesFilter And DataRowState.Added) = DataRowState.Added) Then
                ' Execute the Inserts
                If (Not table.GetChanges(DataRowState.Added) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Added))
                End If
            End If
            If ((changesFilter And DataRowState.Modified) = DataRowState.Modified) Then
                ' Execute the Inserts
                If (Not table.GetChanges(DataRowState.Modified) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Modified))
                End If
            End If
            If ((changesFilter And DataRowState.Deleted) = DataRowState.Deleted) Then
                ' Execute the Deletes
                If (Not table.GetChanges(DataRowState.Deleted) Is Nothing) Then
                    da.Update(table.GetChanges(DataRowState.Deleted))
                End If
            End If

        End Using

        '''TODO: Check weather explicit AcceptChanegs are Required
        table.AcceptChanges()

        LookupListCache.ClearFromCache(Me.GetType.ToString)
    End Sub

    ''' <summary>
    ''' Transfers the changes made in <see cref="System.Data.DataRow" /> to Oracle Database.
    ''' </summary>
    ''' <param name="table"><see cref="System.Data.DataRow" /> containing changes to be Updated (Inserts/Updates/Deletes)</param>
    ''' <param name="transaction">Optional. Instance of <see cref="OracleTransaction" />. When the value is provided, the updates 
    ''' will be part of the transaction represented by value of this parameter. Also, the <see cref="OracleConnection" /> used by 
    ''' this <see cref="OracleTransaction" /> will be used for all <see cref="OracleCommand"> objects during
    ''' updates to database.</param>
    ''' <param name="changesFilter">Optional. When value is provided; only rows having one of the flag values will be updated.</param>
    ''' <remarks></remarks>
    Public Overridable Sub Update(ByVal row As DataRow, _
                                  Optional ByVal transaction As OracleTransaction = Nothing, _
                                  Optional ByVal changesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)
        ' Check if Row has changes based on RowState
        If (Not ((row.RowState = DataRowState.Added) OrElse _
                 (row.RowState = DataRowState.Deleted) OrElse _
                 ((row.RowState = DataRowState.Modified) AndAlso (DBHelper.CheckRowChanged(row))))) Then
            Return
        End If

        ' Update Changes Filter by Anding with Row State. 
        ' E.g. If row has been Modified and Change Filter is for Adding, the true action will be nothing.
        changesFilter = changesFilter And row.RowState

        ' When Filter is blank then do not do anything.
        If (Not (changesFilter Or DataRowState.Added = DataRowState.Added OrElse _
                 changesFilter Or DataRowState.Modified = DataRowState.Modified OrElse _
                 changesFilter Or DataRowState.Deleted = DataRowState.Deleted)) Then
            Return
        End If

        '' Create Data Adapter and Process Request
        Using da As OracleDataAdapter = CreateDataAdapter(row.Table.TableName, transaction, changesFilter)
            ' Execute the updates
            da.Update(New DataRow() {row})
        End Using

        '''TODO: Check weather explicit AcceptChanegs are Required
        row.AcceptChanges()

        LookupListCache.ClearFromCache(Me.GetType.ToString)
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Creates an instance of new <see cref="OracleDataAdapter" /> and assembles Insert, Update and Delete
    ''' commands, Transaction and Connection Objects.
    ''' </summary>
    ''' <param name="tableName">Name of table for which <see cref="OracleDataAdapter" /> is being built.</param>
    ''' <param name="transaction">Optional. Instance of <see cref="OracleTransaction" />. When the value is provided, the updates 
    ''' will be part of the transaction represented by value of this parameter. Also, the <see cref="OracleConnection" /> used by 
    ''' this <see cref="OracleTransaction" /> will be used for all <see cref="OracleCommand"> objects during
    ''' updates to database.</param>
    ''' <param name="changesFilter">Optional. When value is provided; only rows having one of the flag values will be updated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataAdapter(ByVal tableName As String, _
                                       Optional ByVal transaction As OracleTransaction = Nothing, _
                                       Optional ByVal changesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted)
        Dim da As OracleDataAdapter
        Dim connection As OracleConnection

        If (transaction Is Nothing) Then
            connection = CreateConnection()
        Else
            connection = transaction.Connection
        End If

        da = New OracleDataAdapter()

        If ((changesFilter And DataRowState.Added) = DataRowState.Added) Then
            da.InsertCommand = OracleDbHelper.CreateCommand(Config("/SQL/INSERT"), CommandType.StoredProcedure, connection, transaction)
            ConfigureInsertCommand(da.InsertCommand, tableName)
        End If
        If ((changesFilter And DataRowState.Modified) = DataRowState.Modified) Then
            da.UpdateCommand = OracleDbHelper.CreateCommand(Config("/SQL/UPDATE"), CommandType.StoredProcedure, connection, transaction)
            ConfigureUpdateCommand(da.UpdateCommand, tableName)
        End If
        If ((changesFilter And DataRowState.Deleted) = DataRowState.Deleted) Then
            da.DeleteCommand = OracleDbHelper.CreateCommand(Config("/SQL/DELETE"), CommandType.StoredProcedure, connection, transaction)
            ConfigureDeleteCommand(da.DeleteCommand, tableName)
        End If

        Return da
    End Function

#End Region

End Class
