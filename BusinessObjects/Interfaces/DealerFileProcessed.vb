'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/12/2004)  ********************

Public Class DealerFileProcessed
    Inherits BusinessObjectBase

   
#Region "Constants"

    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_REMAINING_REJECTED As String = "remaining_rejected"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_FILE_TYPE_CODE As String = "file_type_code"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_STATUS_DESC As String = "status_desc"

    Private Const DEALERLOADFORM_FORM001 As String = "DEALERLOADFORM_FORM001" ' The filename does not exists or it is empty
    Private Const DEALERLOADFORM_FORM002 As String = "DEALERLOADFORM_FORM002" ' Dealer Contract Not Found

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    Public Sub New(ByVal id As Guid, ByVal parentfile As Boolean)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id, parentfile)
    End Sub
    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
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

    Protected Sub Load()
        Try
            Dim dal As New DealerFileProcessedDAL
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
            Dim dal As New DealerFileProcessedDAL
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

    Protected Sub Load(ByVal id As Guid, ByVal IsParentFile As Boolean)
        Try
            Dim dal As New DealerFileProcessedDAL
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
                dal.Load(Me.Dataset, id, IsParentFile)
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
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(DealerFileProcessedDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerFileProcessedDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerFileProcessedDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Filename() As String
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerFileProcessedDAL.COL_NAME_FILENAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property



    Public Property Received() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_RECEIVED, Value)
        End Set
    End Property



    Public Property Counted() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_COUNTED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_COUNTED, Value)
        End Set
    End Property

    Public Property Bypassed() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_BYPASSED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_BYPASSED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_BYPASSED, Value)
        End Set
    End Property

    Public Property Rejected() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_REJECTED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_REJECTED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_REJECTED, Value)
        End Set
    End Property

    Public Property RemainingRejected() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_REMAINING_REJECTED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_REMAINING_REJECTED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_REMAINING_REJECTED, Value)
        End Set
    End Property

    Public Property Validated() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_VALIDATED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_VALIDATED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_VALIDATED, Value)
        End Set
    End Property



    Public Property Loaded() As LongType
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_LOADED) Is DBNull.Value Then
                Return 0
            Else
                Return New LongType(CType(Row(DealerFileProcessedDAL.COL_NAME_LOADED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Layout() As String
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerFileProcessedDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property

    Public Property FileTypeCode() As String
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_FILE_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerFileProcessedDAL.COL_NAME_FILE_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_FILE_TYPE_CODE, Value)
        End Set
    End Property

    Public Property DealerGroupId() As Guid
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerFileProcessedDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerFileProcessedDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    Public ReadOnly Property Status() As String
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerFileProcessedDAL.COL_NAME_STATUS), String)
            End If
        End Get
    End Property

    Public ReadOnly Property IsChildFile() As Boolean
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_Is_Child_File) Is DBNull.Value Then
                Return Nothing
            Else
                'Return Boolean.Parse(Row(DealerFileProcessedDAL.COL_NAME_Is_Child_File).ToString.ToLower())
                Return Row(DealerFileProcessedDAL.COL_NAME_Is_Child_File).ToString.ToUpper = "T"
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            CheckDeleted()
            If Row(DealerFileProcessedDAL.COL_NAME_STATUS_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerFileProcessedDAL.COL_NAME_STATUS_DESC), String)
            End If
        End Get
    End Property

#Region "Properties External BOs"

    Public ReadOnly Property DealerCode() As String
        Get
            If DealerId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerId)
        End Get
    End Property

    Public ReadOnly Property DealerNameLoad() As String
        Get
            If DealerId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_DEALERS)
            Return LookupListNew.GetDescriptionFromId(dv, DealerId)
        End Get
    End Property

    Public ReadOnly Property DealerGroupCode() As String
        Get
            If DealerGroupId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_GROUPS, DealerGroupId)
        End Get
    End Property

    Public ReadOnly Property DealerGroupNameLoad() As String
        Get
            If DealerGroupId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_DEALER_GROUPS)
            Return LookupListNew.GetDescriptionFromId(dv, DealerGroupId)
        End Get
    End Property

    'Public ReadOnly Property InterfaceStatus(ByVal interfaceStatus_id As Guid) As String
    '    Get
    '        Dim intStatus = New InterfaceStatusWrk(interfaceStatus_id)
    '        Return intStatus.status
    '    End Get
    'End Property

#End Region

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerFileProcessedDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Public Function WaitTilDone(ByVal intStatus_id As Guid) As InterfaceStatusWrk.IntError
    '    Dim sleepInterval As Int32 = 5000
    '    Dim maxPollingCycles As Int32 = 20
    '    Dim currentPollingCycles As Integer = 0
    '    Dim intStatus As String = InterfaceStatus(intStatus_id)
    '    Dim moError As InterfaceStatusWrk.IntError

    '    While ((intStatus <> InterfaceStatusWrkDAL.STATUS_SUCCESS) AndAlso _
    '             (intStatus <> InterfaceStatusWrkDAL.STATUS_FAILURE) AndAlso _
    '             (currentPollingCycles < maxPollingCycles))

    '        System.Threading.Thread.CurrentThread.Sleep(sleepInterval)
    '        intStatus = InterfaceStatus(intStatus_id)
    '        currentPollingCycles += 1
    '    End While

    '    If intStatus = InterfaceStatusWrkDAL.STATUS_SUCCESS Then
    '        moError.status = InterfaceStatusWrk.IntStatus.SUCCESS
    '    ElseIf intStatus = InterfaceStatusWrkDAL.STATUS_FAILURE Then
    '        'Failure
    '        moError.status = InterfaceStatusWrk.IntStatus.FAILURE
    '    Else
    '        'Pending or Running
    '        moError.status = InterfaceStatusWrk.IntStatus.PENDING
    '    End If
    '    Return moError
    'End Function

#Region "DealerFile"

    Public Shared Function getDealerFileNamesBwtnDateRange(ByVal CompanyId As Guid, ByVal DealerCode As String, ByVal BeginDate As String, _
                                                           ByVal EndDate As String, ByVal FileType As String, ByVal rejectionType As String) As DataView
        Dim dal As New DealerFileProcessedDAL
        Dim ds As DataSet
        Dim dv As DataView
        If (FileType = "CERT") Then
            dv = New DataView(dal.DealerFilesLoadBtwnDateRange(CompanyId, DealerCode, BeginDate, EndDate, FileType, rejectionType).Tables(0))
        Else
            dv = New DataView(dal.PaymentFilesLoadBtwnDateRange(CompanyId, DealerCode, BeginDate, EndDate, FileType, rejectionType).Tables(0))
        End If

        Return (dv)

    End Function
#End Region
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal compIds As ArrayList, ByVal oData As Object) As DataView
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL
            Dim ds As Dataset

            ds = dal.LoadList(compIds, oDealerFileProcessedData)
            Return (ds.Tables(DealerFileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Structure DealerInfo
        Public dealerID As Guid
        Public layout As String
        Public dealerCode As String
        Public dealerName As String
    End Structure

    Public Shared Function GetDealerLayout(ByVal dealerID As Guid, _
    ByVal oInterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode) As DealerInfo
        Dim retDealerInfo As DealerInfo
        Dim sLayout As String
        Dim oContract As Contract
        Dim oDealer As Dealer

        With retDealerInfo
            .dealerID = Guid.Empty
            .layout = ""
            .dealerCode = ""
            .dealerName = ""
        End With

        oContract = Contract.GetCurrentContract(dealerID)
        If oContract Is Nothing Then
            oContract = Contract.GetMaxExpirationContract(dealerID)
            If oContract Is Nothing Then
                Dim errors() As ValidationError = {New ValidationError(DEALERLOADFORM_FORM002, GetType(DealerFileProcessed), Nothing, Nothing, Nothing)}
                Throw New BOValidationException(errors, GetType(DealerFileProcessed).FullName)
            End If
        End If

        oDealer = New Dealer(oContract.DealerId)
        sLayout = oContract.Layout
        If Not sLayout Is Nothing Then
            If oInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM Then
                sLayout = sLayout.Remove(sLayout.Length - 1, 1) & "p"
            End If
        End If
        With retDealerInfo
            .dealerID = dealerID
            .layout = sLayout
            .dealerCode = oDealer.Dealer
            .dealerName = oDealer.DealerName
        End With

        Return retDealerInfo

    End Function


#End Region

#Region "StoreProcedures Control"

    'Private Shared Sub CreateInterfaceStatus(ByVal desc As String, ByVal oDealerFileProcessedData As DealerFileProcessedData)
    '    Dim intStatus = New InterfaceStatusWrk

    '    oDealerFileProcessedData.interfaceStatus_id = intStatus.id
    '    intStatus.Description = desc
    '    intStatus.Status = InterfaceStatusWrkDAL.STATUS_PENDING
    '    intStatus.Save()
    'End Sub

    Public Shared Sub ValidateFile(ByVal oData As Object)
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL

            oDealerFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(oDealerFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub ProcessFileRecords(ByVal oData As Object)
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL

            oDealerFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
            dal.ProcessFileRecords(oDealerFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub DeleteFile(ByVal oData As Object)
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL

            oDealerFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DELETE)
            dal.DeleteFile(oDealerFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub DownloadFile(ByVal oData As Object)
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL

            oDealerFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DOWNLOAD)
            dal.DownloadFile(oDealerFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub GenerateResponseFile(ByVal oData As Object)
        Try
            Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
            Dim dal As New DealerFileProcessedDAL

            oDealerFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_ENRRES)
            dal.GenerateResponseFile(oDealerFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Validation"

    Public Shared Sub ValidateFileName(ByVal fileLength As Integer)
        If fileLength = 0 Then
            Dim errors() As ValidationError = {New ValidationError(DEALERLOADFORM_FORM001, GetType(DealerFileProcessed), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(DealerFileProcessed).FullName)
        End If

    End Sub

#End Region
End Class


