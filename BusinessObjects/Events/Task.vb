'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2012)  ********************

Public Class Task
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New TaskDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New TaskDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"

    Private _syncRoot As New Object
    Private _arguments As KeyValueDictionary

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(TaskDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TaskDAL.COL_NAME_TASK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TaskDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TaskDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0, MinExclusive:=False, Max:=99, MaxExclusive:=False, Message:=Assurant.ElitaPlus.Common.ErrorCodes.GUI_TASK_RETRY_COUNT_RANGE)> _
    Public Property RetryCount() As LongType
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_RETRY_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TaskDAL.COL_NAME_RETRY_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_RETRY_COUNT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0, MinExclusive:=False, Max:=999999, MaxExclusive:=False, Message:=Assurant.ElitaPlus.Common.ErrorCodes.GUI_TASK_RETRY_DELAY_RANGE)> _
    Public Property RetryDelaySeconds() As LongType
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_RETRY_DELAY_SECONDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TaskDAL.COL_NAME_RETRY_DELAY_SECONDS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_RETRY_DELAY_SECONDS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0, MinExclusive:=False, Max:=999999, MaxExclusive:=False, Message:=Assurant.ElitaPlus.Common.ErrorCodes.GUI_TASK_TIMEOUT_RANGE)> _
    Public Property TimeoutSeconds() As LongType
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_TIMEOUT_SECONDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TaskDAL.COL_NAME_TIMEOUT_SECONDS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_TIMEOUT_SECONDS, Value)
        End Set
    End Property

    Default Public ReadOnly Property TaskParameter(ByVal key As String) As String
        Get
            Dim returnValue As String = String.Empty
            If (_arguments Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_arguments Is Nothing) Then
                        _arguments = New KeyValueDictionary(Me.TaskParameters)
                    End If
                End SyncLock
            End If
            If (_arguments.ContainsKey(key)) Then
                returnValue = _arguments(key)
            End If
            Return returnValue
        End Get
    End Property

    <ValidStringLength("", Max:=4000)> _
    Public Property TaskParameters() As String
        Get
            CheckDeleted()
            If row(TaskDAL.COL_NAME_TASK_PARAMETERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TaskDAL.COL_NAME_TASK_PARAMETERS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TaskDAL.COL_NAME_TASK_PARAMETERS, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TaskDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal strCode As String, ByVal strDesc As String) As TaskSearchDV
        Try
            Dim dal As New TaskDAL
            Return New TaskSearchDV(dal.LoadList(strCode, strDesc).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"
    Public Class TaskSearchDV
        Inherits DataView

        Public Const COL_TASK_ID As String = "task_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_RETRY_COUNT As String = "retry_count"
        Public Const COL_RETRY_DELAY As String = "retry_delay_seconds"
        Public Const COL_TASK_PARAMS As String = "task_parameters"
        Public Const COL_TIMEOUT As String = "timeout_seconds"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As TaskSearchDV, ByVal NewBO As Task)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(TaskSearchDV.COL_TASK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(TaskSearchDV.COL_CODE, GetType(String))
                dt.Columns.Add(TaskSearchDV.COL_DESCRIPTION, GetType(String))
                dt.Columns.Add(TaskSearchDV.COL_RETRY_COUNT, GetType(Long))
                dt.Columns.Add(TaskSearchDV.COL_RETRY_DELAY, GetType(Long))
                dt.Columns.Add(TaskSearchDV.COL_TIMEOUT, GetType(Long))
                dt.Columns.Add(TaskSearchDV.COL_TASK_PARAMS, GetType(String))

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(TaskSearchDV.COL_TASK_ID) = NewBO.Id.ToByteArray
            row(TaskSearchDV.COL_CODE) = NewBO.Code
            row(TaskSearchDV.COL_DESCRIPTION) = NewBO.Description
            If Not NewBO.RetryCount Is Nothing Then
                row(TaskSearchDV.COL_RETRY_COUNT) = NewBO.RetryCount.Value
            End If

            If Not NewBO.RetryDelaySeconds Is Nothing Then
                row(TaskSearchDV.COL_RETRY_DELAY) = NewBO.RetryDelaySeconds.Value
            End If

            If Not NewBO.TimeoutSeconds Is Nothing Then
                row(TaskSearchDV.COL_TIMEOUT) = NewBO.TimeoutSeconds.Value
            End If

            row(TaskSearchDV.COL_TASK_PARAMS) = NewBO.TaskParameters
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New TaskSearchDV(dt)
        End If
    End Sub

    
#End Region
End Class


