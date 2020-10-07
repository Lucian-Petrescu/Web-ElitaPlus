Imports System.Text

Public MustInherit Class FileLoadBase(Of THeader As IFileLoadHeaderWork, TRecon As IFileLoadReconWork)

#Region "Events"
    Public Event LogInfo As AddMessageDelegate
    Public Event LogWarning As AddMessageDelegate
    Public Event LogDebugLog As AddMessageDelegate
    Public Event LogError As AddErrorDelegate
    Public Event LogException As AddExceptionDelegate
#End Region

#Region "Constants"
    Private Const DEFAULT_THREAD_COUNT As Integer = 1
    Private Const DEFAULT_TRANSACTION_SIZE As Integer = 0
#End Region

#Region "Constructors"

    Friend Sub New(ByVal useCustomSave As Boolean)
        Me.New(IIf(useCustomSave, 1, DEFAULT_THREAD_COUNT), IIf(useCustomSave, 1, DEFAULT_TRANSACTION_SIZE), useCustomSave)
    End Sub

    Friend Sub New(ByVal threadCount As Integer, ByVal transactionSize As Integer)
        Me.New(threadCount, transactionSize, False)
    End Sub

    Private Sub New(ByVal threadCount As Integer, ByVal transactionSize As Integer, ByVal useCustomSave As Boolean)
        _threadCount = Math.Min(threadCount, MaximumThreadCount)
        If (transactionSize = 0) Then
            _transactionSize = 0
        Else
            _transactionSize = Math.Min(transactionSize, MaximumTransactionSize)
        End If
        _useCustomSave = useCustomSave
        _transactionSize = transactionSize
        _familyDs = New DataSet
        _syncRoot = New Object
        _trace = New StringBuilder()
        _interfaceStatusId = Nothing
    End Sub

    Friend Sub New()
        Me.New(DEFAULT_THREAD_COUNT, DEFAULT_TRANSACTION_SIZE)
    End Sub
#End Region

#Region "Delegate"
    Private Delegate Sub ProcessDelegate(ByVal id As Guid)
#End Region

#Region "Fields"
    Private ReadOnly _threadCount As Integer
    Private ReadOnly _transactionSize As Integer
    Private ReadOnly _useCustomSave As Boolean
    Private _familyDs As DataSet
    Private _header As THeader = Nothing
    Private _syncRoot As Object
    Private _recordsProcessed As Long
    Private _interfaceStatusId As Nullable(Of Guid)
    Private _trace As StringBuilder
#End Region

#Region "Properties"
    Protected ReadOnly Property MaximumThreadCount As Integer
        Get
            Return DEFAULT_THREAD_COUNT
        End Get
    End Property

    Protected ReadOnly Property MaximumTransactionSize As Integer
        Get
            Return DEFAULT_TRANSACTION_SIZE
        End Get
    End Property

    Protected ReadOnly Property ThreadCount As Integer
        Get
            Return _threadCount
        End Get
    End Property

    Protected ReadOnly Property TransactionSize As Integer
        Get
            Return _transactionSize
        End Get
    End Property

    Protected ReadOnly Property FamilyDs As DataSet
        Get
            Return _familyDs
        End Get
    End Property

    Protected Property Header As THeader
        Get
            Return _header
        End Get
        Private Set
            _header = value
        End Set
    End Property

    Protected Property RecordsProcessed As Long
        Get
            Return _recordsProcessed
        End Get
        Private Set
            _recordsProcessed = value
        End Set
    End Property

    Private Property InterfaceStatusId As Nullable(Of Guid)
        Get
            Return _interfaceStatusId
        End Get
        Set
            _interfaceStatusId = value
        End Set
    End Property

    Private ReadOnly Property InterfaceStatus As InterfaceStatusWrk
        Get
            If (InterfaceStatusId.HasValue) Then
                Return New InterfaceStatusWrk(InterfaceStatusId.Value)
            Else
                Return Nothing
            End If
        End Get
    End Property
#End Region

#Region "Methods"
    Protected Sub AppendTrace(ByVal trace As String)
        _trace.Append(trace)
    End Sub

    Protected Sub AppendTraceLine(ByVal trace As String)
        _trace.AppendLine(trace)
    End Sub

    Protected Function DecimalTypeToString(ByVal decimalType As Nullable(Of Decimal)) As String
        If (decimalType Is Nothing) Then
            Return "N/A"
        Else
            Return decimalType.Value.ToString()
        End If
    End Function

    Public Function ProcessAsync(ByVal id As Guid) As Guid
        Dim oProcessDelegate As New ProcessDelegate(AddressOf Process)
        InterfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
        oProcessDelegate.BeginInvoke(id, Nothing, Nothing)
        Return InterfaceStatusId.Value
    End Function

    Public Sub Process(ByVal id As Guid)
        Try
            Dim beforeRecord As TRecon = Nothing
            Dim keyChangedResult As KeyChangeReturnType
            ' Update Interface Status to Running
            If (InterfaceStatusId.HasValue) Then
                With InterfaceStatus
                    .Status = InterfaceStatusWrkDAL.STATUS_RUNNING
                    .Save()
                End With
            End If

            BeforeCreateFileLoadHeader()
            Header = CreateFileLoadHeader(id)
            AfterCreateFileLoadHeader()

            ' Update Interface Status Active File Name
            If (InterfaceStatusId.HasValue) Then
                With InterfaceStatus
                    .Active_Filename = Header.FileName.Trim().PadRight(50).Substring(0, 50)
                    .Save()
                End With
            End If

            _recordsProcessed = 0
            ' Read Recon Records
            For Each item As IFileLoadReconWork In Header.Children
                Dim reconRecord As IFileLoadReconWork
                Dim response As ProcessResult
                Dim familyDataSet As DataSet
                Try
                    reconRecord = CreateFileLoadDetail(item.Id, Header)

                    If (reconRecord.Loaded = "V") Then
                        If (_useCustomSave) Then
                            familyDataSet = Nothing
                        Else
                            familyDataSet = reconRecord.FamilyDataSet
                        End If
                        keyChangedResult = IsKeyChanged(beforeRecord, reconRecord, familyDataSet)
                        If (keyChangedResult.IsChanged) Then
                            KeyChanged(keyChangedResult.Key, beforeRecord, reconRecord, familyDataSet)
                        End If

                        response = ProcessDetailRecord(DirectCast(reconRecord, TRecon), familyDataSet)

                    Else
                        response = ProcessResult.NotProcessed
                    End If
                Catch ex As Exception
                    Common.AppConfig.Log(ex)
                    RaiseEvent LogException(ex)
                    response = ProcessResult.None
                Finally
                    beforeRecord = reconRecord
                End Try

                Select Case response
                    Case ProcessResult.Rejected
                        IncrementRejected()
                        With DirectCast(reconRecord, TRecon)
                            .Loaded = "R"
                            If (.RejectCode Is Nothing OrElse .RejectCode.Trim().Length = 0) Then .RejectCode = "000"
                            If (.RejectReason Is Nothing OrElse .RejectReason.Trim().Length = 0) Then .RejectCode = "Unknown"
                        End With
                    Case ProcessResult.Loaded
                        IncrementLoaded()
                        DirectCast(reconRecord, TRecon).Loaded = "L"
                    Case ProcessResult.NotProcessed
                    Case ProcessResult.None
                        IncrementRejected()
                        With DirectCast(reconRecord, TRecon)
                            .Loaded = "R"
                            .RejectCode = "000"
                            .RejectReason = "Unknown"
                        End With
                End Select


                If ((_useCustomSave) OrElse (TransactionSize <> 0 AndAlso RecordsProcessed >= TransactionSize)) Then
                    Save(familyDataSet, reconRecord)
                    RecordsProcessed = 0
                End If
            Next
            If (RecordsProcessed > 0) Then
                '' The FamilyDataSet argumnet will always be Header because if the code is running with UseCustomSave = True then RecordsProcessed will always be 0
                Save(Header.FamilyDataSet, Nothing)
            End If

            ' Update Interface Status to Success
            If (InterfaceStatusId.HasValue) Then
                With InterfaceStatus
                    .Status = InterfaceStatusWrkDAL.STATUS_SUCCESS
                    .Save()
                End With
            End If
        Catch ex As Exception
            Common.AppConfig.Log(ex)
            ' Update Interface Status to Failed
            If (InterfaceStatusId.HasValue) Then
                With InterfaceStatus
                    .Status = InterfaceStatusWrkDAL.STATUS_FAILURE
                    .Save()
                End With
            End If
            Throw
        End Try
    End Sub

    Protected Sub Save(ByVal familyDataSet As DataSet, ByVal reconRecord As TRecon)
        Dim argument As Object
        argument = BeforeSave(familyDataSet)
        If (_useCustomSave) Then
            If (reconRecord.Loaded = "L") Then
                CustomSave(Header)
            Else
                Header.Save()
            End If
        Else
            Header.Save()
        End If
            AfterSave(argument, familyDataSet)
    End Sub

    Private Sub IncrementRejected()
        SyncLock (_syncRoot)
            Header.Rejected = Header.Rejected.Value + 1
            Header.Validated = Header.Validated.Value - 1
            RecordsProcessed = RecordsProcessed + 1
        End SyncLock
    End Sub

    Private Sub IncrementLoaded()
        SyncLock (_syncRoot)
            Header.Loaded = Header.Loaded.Value + 1
            Header.Validated = Header.Validated.Value - 1
            RecordsProcessed = RecordsProcessed + 1
        End SyncLock
    End Sub

    Public Overridable Sub BeforeCreateFileLoadHeader()

    End Sub

    Protected MustOverride Function CreateFileLoadHeader(ByVal fileLoadHeaderId As Guid) As THeader

    Protected MustOverride Function CreateFileLoadDetail(ByVal fileLoadDetailId As Guid, ByVal headerRecord As THeader) As TRecon


    Public Overridable Sub AfterCreateFileLoadHeader()

    End Sub

    Protected MustOverride Function ProcessDetailRecord(ByVal reconRecord As TRecon, ByVal familyDataSet As DataSet) As ProcessResult

    Public Overridable Function BeforeSave(ByVal familyDataSet As DataSet) As Object
        Return Nothing
    End Function

    Public Overridable Sub AfterSave(ByVal argument As Object, ByVal familyDataSet As DataSet)

    End Sub

    Protected Overridable Function IsKeyChanged(ByVal beforeReconRecord As TRecon, ByVal afterReconRecord As TRecon, ByVal familyDataSet As DataSet) As KeyChangeReturnType
        Dim returnValue As KeyChangeReturnType
        returnValue.IsChanged = False
        Return returnValue
    End Function

    Protected Overridable Sub KeyChanged(ByVal key As String, ByVal beforeReconRecord As TRecon, ByVal afterReconRecord As TRecon, ByVal familyDataSet As DataSet)

    End Sub

    Protected Overridable Sub CustomSave(ByVal headerRecord As THeader)

    End Sub

#End Region

End Class

Public Structure KeyChangeReturnType
    Public Key As String
    Public IsChanged As Boolean
End Structure

#Region "Enums"
Public Enum ProcessResult
    None
    Rejected
    Loaded
    NotProcessed
End Enum
#End Region