'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/22/2006)  ********************

Public Class InterfaceStatusWrk
    Inherits BusinessObjectBase

#Region "Types"

    Public Enum IntStatus
        SUCCESS
        PENDING
        RUNNING
        INTERFACE_DB_FAILED
        INTERFACE_UNKNOWN_PROBLEM
    End Enum

    Public Structure IntError
        Public status As IntStatus
        Public msg As String
    End Structure

#End Region

#Region "Constants"

    Private ReadOnly MAX_SCHED_MINUTES As Integer = Convert.ToInt32(AppConfig.Application.CeTimeout)
    ' Private ReadOnly MAX_SCHED_MINUTES As Integer = 2
    Private ReadOnly MAX_SCHED_POLL As Integer = Convert.ToInt32((MAX_SCHED_MINUTES * 60) / 5)

#End Region
#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New InterfaceStatusWrkDAL
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
            Dim dal As New InterfaceStatusWrkDAL
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
    Public Shared Function IsfileBeingProcessed(ByVal filename As String, Optional ByVal parentFile As Boolean = False) As Boolean

        Try
            Dim dal As New InterfaceStatusWrkDAL
            Dim ds As New DataSet

            If parentFile = False Then
                ds = dal.LoadByActiveFileName(filename)
            Else
                ds = dal.LoadByActiveFileName(filename, parentFile)
            End If

            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function IsStatus_Running(ByVal id As Guid) As Boolean
        Try
            Dim dal As New InterfaceStatusWrkDAL
            Dim ds As New DataSet
            ds = dal.Load_IsStatus_Running(id)
            If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function CreateInterfaceStatus(ByVal desc As String) As Guid
        Dim intStatus = New InterfaceStatusWrk

        intStatus.Description = desc
        intStatus.Status = InterfaceStatusWrkDAL.STATUS_PENDING
        intStatus.Save()

        Return intStatus.id
    End Function

    Public Sub ReLoad(ByVal id As Guid)
        '  Me.Dataset = New Dataset
        Me.Load(id)
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(InterfaceStatusWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID), Byte()))
            End If
        End Get
    End Property
    Public ReadOnly Property Sessionpaddrid() As Guid
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_SESSIONPADDRID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_SESSIONPADDRID), Guid)
            End If
        End Get
        'Set(ByVal Value As Guid)
        '  CheckDeleted()
        '   Me.SetValue(InterfaceStatusWrkDAL.COL_NAME_SESSIONPADDRID, Value)
        ' End Set
    End Property
    <ValidStringLength("", Max:=30)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InterfaceStatusWrkDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Active_Filename() As String
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_ACTIVE_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_ACTIVE_FILENAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InterfaceStatusWrkDAL.COL_NAME_ACTIVE_FILENAME, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=30)> _
    Public Property Status() As String
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InterfaceStatusWrkDAL.COL_NAME_STATUS, Value)
        End Set
    End Property

    Public ReadOnly Property ReLoadStatus() As String
        Get
            ReLoad(Me.Id)

            Return Status
        End Get
    End Property

    Public ReadOnly Property Error_Message() As String
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_ERROR_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_ERROR_MESSAGE), String)
            End If
        End Get
    End Property

    'REQ-1056 
    Public ReadOnly Property Created_Date() As String
        Get
            CheckDeleted()
            If Row(InterfaceStatusWrkDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InterfaceStatusWrkDAL.COL_NAME_CREATED_DATE), String)
            End If
        End Get
    End Property
    'END REQ-1056

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InterfaceStatusWrkDAL
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

    Public Function WaitTilDone() As InterfaceStatusWrk.IntError
        Dim sleepInterval As Int32 = 5000  ' 5 Seconds
        'Dim maxPollingCycles As Int32 = 2 ' * 10 = 50 Seconds
        Dim maxPollingCycles As Int32 = MAX_SCHED_POLL
        Dim currentPollingCycles As Integer = 0
        Dim intStatus As String = Me.Status
        Dim moError As InterfaceStatusWrk.IntError

        While ((intStatus <> InterfaceStatusWrkDAL.STATUS_SUCCESS) AndAlso _
                 (intStatus <> InterfaceStatusWrkDAL.STATUS_FAILURE) AndAlso _
                 (currentPollingCycles < maxPollingCycles))

            System.Threading.Thread.CurrentThread.Sleep(sleepInterval)
            intStatus = ReLoadStatus()
            currentPollingCycles += 1
        End While

        'intStatus = InterfaceStatusWrkDAL.STATUS_SUCCESS  ' To Be Delete just for testing
        'intStatus = InterfaceStatusWrkDAL.STATUS_PENDING  ' To Be Delete just for testing
        'intStatus = InterfaceStatusWrkDAL.STATUS_FAILURE  ' To Be Delete just for testing
        If intStatus = InterfaceStatusWrkDAL.STATUS_SUCCESS Then
            moError.status = InterfaceStatusWrk.IntStatus.SUCCESS
        ElseIf intStatus = InterfaceStatusWrkDAL.STATUS_FAILURE Then
            'Failure
            moError.status = InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED
            moError.msg = Me.Error_Message
        Else
            'Pending or Running
            moError.status = InterfaceStatusWrk.IntStatus.PENDING
        End If
        Return moError
    End Function
#End Region

    'REQ-1056
#Region "InterfaceStatusSearchDV"
    Public Class InterfaceStatusSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_INTERFACE_STATUS_ID As String = "interface_status_id"
        Public Const COL_ACTIVE_FILE As String = "active_filename"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_STATUS As String = "Status"
        Public Const COL_CREATED_DATE As String = "created_date"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region


#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal activefilename As String) As InterfaceStatusSearchDV
        Try
            Dim dal As New InterfaceStatusWrkDAL
            Return New InterfaceStatusSearchDV(dal.LoadList(activefilename).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

    'END REQ-1056

End Class



