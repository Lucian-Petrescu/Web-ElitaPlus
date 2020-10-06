Public Class AppleGBIFileReconWrk
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "FILE_PROCESSED_ID"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_FILE_DATE As String = "file_date"
    Public Const COL_NAME_FILE_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_NEW_CLAIMS As String = "new_claims"
    Public Const COL_NAME_CLAIM_UPDATE As String = "claim_update"
    Public Const COL_NAME_PROCESSED As String = "processed"
    Public Const COL_NAME_CANCELLED As String = "CANCELLED"
    Public Const COL_NAME_PENDING_VALIDATION As String = "PENDING_VALIDATION"
    Public Const COL_NAME_FAILED As String = "FAILED"
    Public Const COL_NAME_REPROCESS As String = "REPROCESS"
    Public Const COL_NAME_PENDING_CLAIM_CREATION As String = "PENDING_CLAIM_CREATION"


    Public Const COL_NAME_DET_BEN_GBICLAIM_QUEUE_ID As String = "ben_gbiclaim_queue_id"
    Public Const COL_NAME_DET_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_DET_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_DET_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_DET_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_DET_RECORD_STATUS As String = "record_status"
    Public Const COL_NAME_DET_SEQUENCE As String = "sequence"
    Public Const COL_NAME_DET_FILE_DATE As String = "file_date"
    Public Const COL_NAME_DET_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_DET_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DET_CUSTOMER_ID As String = "customer_id"
    Public Const COL_NAME_DET_SHIP_TO_ID As String = "ship_to_id"
    Public Const COL_NAME_DET_AGREEMENT_ID As String = "agreement_id"
    Public Const COL_NAME_DET_UNIQUE_IDENTIFIER As String = "unique_identifier"
    Public Const COL_NAME_DET_ORIGINAL_SERIAL_NUMBER As String = "original_serial_number"
    Public Const COL_NAME_DET_ORIGINAL_IMEI_NUMBER As String = "original_imei_number"
    Public Const COL_NAME_DET_NEW_SERIAL_NUMBER As String = "new_serial_number"
    Public Const COL_NAME_DET_NEW_IMEI_NUMBER As String = "new_imei_number"
    Public Const COL_NAME_DET_REPAIR_COMPLETION_DATE As String = "repair_completion_date"
    Public Const COL_NAME_DET_CLAIM_TYPE As String = "claim_type"
    Public Const COL_NAME_DET_CHANNEL As String = "channel"
    Public Const COL_NAME_DET_INCIDENT_FEE As String = "incident_fee"
    Public Const COL_NAME_DET_NOTIF_CREATE_DATE As String = "notif_create_date"
    Public Const COL_NAME_DET_REPAIR_COMPLETED As String = "repair_completed"
    Public Const COL_NAME_DET_REPAIR_COMPLETED_DATE As String = "repair_completed_date"
    Public Const COL_NAME_DET_CLAIM_CANCELLED As String = "claim_cancelled"
    Public Const COL_NAME_DET_DESCRIPTION As String = "description"
    Public Const COL_NAME_DET_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_DET_CREATED_BY As String = "created_by"
    Public Const COL_NAME_DET_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_DET_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        'Me.VerifyConcurrency(sModifiedDate)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AppleGBIFileReconWrkDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AppleGBIFileReconWrkDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AppleGBIFileReconWrkDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Shared Function SearchFilesname(ByVal BeginDate As Date,
                                           ByVal EndDate As Date) As DataView
        Dim dal As New AppleGBIFileReconWrkDAL
        Dim ds As DataSet

        Try
            ds = dal.LoadSummary(BeginDate, EndDate)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Shared Function LoadDeatils(ByVal FileProcessedId As Guid,
                                       ByVal Status As String) As DataView
        Dim dal As New AppleGBIFileReconWrkDAL
        Dim ds As DataSet

        Try
            ds = dal.LoadDetail(FileProcessedId, Status, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function
#End Region
#Region "Public Members"
    Public Shared Function ProcessFile(ByVal fileProcessedId As Guid) As Guid
        Dim dal As New AppleGBIFileReconWrkDAL
        Try
            dal.ProcessFile(fileProcessedId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub


#End Region

#Region "Properties"
    Public ReadOnly Property Id() As Guid
        Get
            If Row(AppleGBIFileReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_BEN_GBICLAIM_QUEUE_ID), Byte()))
            End If
        End Get
    End Property
    Public ReadOnly Property FileProcessedId() As Guid
        Get
            If Row(AppleGBIFileReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
    End Property
    Public ReadOnly Property Filename() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_FILE_NAME), String)
            End If
        End Get

    End Property

    Public ReadOnly Property Received() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get

    End Property

    Public ReadOnly Property Counted() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_COUNTED), Long))
            End If
        End Get

    End Property

    Public ReadOnly Property Processed() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_PROCESSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_PROCESSED), Long))
            End If
        End Get

    End Property

    Public ReadOnly Property Cancelled() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_CANCELLED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_CANCELLED), Long))
            End If
        End Get

    End Property
    Public ReadOnly Property PendingValidation() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_PENDING_VALIDATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_PENDING_VALIDATION), Long))
            End If
        End Get

    End Property
    Public ReadOnly Property FailedReprocess() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_FAILED_REPROCESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_FAILED_REPROCESS), Long))
            End If
        End Get

    End Property
    Public ReadOnly Property PendingClaimCreation() As LongType
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_PENDING_CLAIM_CREATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_PENDING_CLAIM_CREATION), Long))
            End If
        End Get

    End Property


    'Public Property RecordType() As String
    '    Get
    '        CheckDeleted()
    '        If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_TYPE) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_TYPE), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_TYPE, Value)
    '    End Set
    'End Property

    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_CODE, Value)
        End Set
    End Property

    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_REJECT_REASON, Value)
        End Set
    End Property

    Public Property CustomerId() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CUSTOMER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CUSTOMER_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_CUSTOMER_ID, Value)
        End Set
    End Property

    Public Property ShipToId() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_SHIP_TO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_SHIP_TO_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_SHIP_TO_ID, Value)
        End Set
    End Property

    Public Property AgreementId() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_AGREEMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_AGREEMENT_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_AGREEMENT_ID, Value)
        End Set
    End Property

    Public Property UniqueIdentifier() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_UNIQUE_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_UNIQUE_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_UNIQUE_IDENTIFIER, Value)
        End Set
    End Property

    Public Property OriginalSerialNumber() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER, Value)
        End Set
    End Property

    Public Property OriginalImeiNumber() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_IMEI_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_ORIGINAL_IMEI_NUMBER, Value)
        End Set
    End Property
    Public Property NewSerialNumber() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_SERIAL_NUMBER, Value)
        End Set
    End Property
    Public Property NewImeiNumber() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_IMEI_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_NEW_IMEI_NUMBER, Value)
        End Set
    End Property
    Public Property RepairCompletionDate() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETION_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETION_DATE, Value)
        End Set
    End Property
    Public Property ClaimType() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_TYPE, Value)
        End Set
    End Property
    Public Property Channel() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CHANNEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CHANNEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_CHANNEL, Value)
        End Set
    End Property
    Public Property IncidentFee() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_INCIDENT_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_INCIDENT_FEE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_INCIDENT_FEE, Value)
        End Set
    End Property
    Public Property NotifCreateDate() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NOTIF_CREATE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_NOTIF_CREATE_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_NOTIF_CREATE_DATE, Value)
        End Set
    End Property
    Public Property RepairCompleted() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED, Value)
        End Set
    End Property
    Public Property RepairCompletedDate() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_REPAIR_COMPLETED_DATE, Value)
        End Set
    End Property
    Public Property ClaimCancelled() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_CANCELLED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_CANCELLED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_CLAIM_CANCELLED, Value)
        End Set
    End Property
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_DESCRIPTION, Value)
        End Set
    End Property

    Public Property RecordStatus() As String
        Get
            CheckDeleted()
            If Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AppleGBIFileReconWrkDAL.COL_NAME_DET_RECORD_STATUS, Value)
        End Set
    End Property
#End Region
End Class
