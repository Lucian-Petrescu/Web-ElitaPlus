Public Class BestReplacementRecon
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_BEST_REPLACEMENT_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "best_replacement_recon_wrk_id"
    Public Const COL_NAME_BEST_REPLACEMENT_RECON_WRK_ID As String = "best_replacement_recon_wrk_id"
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_LOAD_STATUS As String = "load_status"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_REPLACEMENT_MANUFACTURER As String = "replacement_manufacturer"
    Public Const COL_NAME_REPLACEMENT_MODEL As String = "replacement_model"
    Public Const COL_NAME_PRIORITY As String = "priority"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_date"
#End Region
#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(id As Guid, sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        VerifyConcurrency(sModifiedDate)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub
#End Region
#Region "Load"
    Protected Sub Load()
        Try
            Dim dal As New BestReplacementReconDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New BestReplacementReconDAL
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

    Public Shared Function LoadList(fileProcessedID As Guid) As DataView
        Try
            Dim dal As New BestReplacementReconDAL
            Dim ds As DataSet

            ds = dal.LoadList(fileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(BestReplacementReconDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function LoadRejectList(fileProcessedID As Guid) As DataView()
        'Try
        '    Dim dal As New FileProcessedReconDAL
        '    Dim ds As DataSet

        '    ds = dal.LoadRejectList(fileProcessedID)
        '    Return (ds.Tables(DealerReconWrkDAL.TABLE_NAME).DefaultView)

        'Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
        '    Throw New DataBaseAccessException(ex.ErrorType, ex)
        'End Try

    End Function
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(BestReplacementReconDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementReconDAL.COL_NAME_BEST_REPLACEMENT_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property fileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_FILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementReconDAL.COL_NAME_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_FILE_PROCESSED_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=1)> _
    Public Property loadstatus As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_LOAD_STATUS) Is DBNull.Value Then


                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_LOAD_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_LOAD_STATUS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_REJECT_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then


                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999)> _
    Public Property Priority As LongType
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_PRIORITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BestReplacementReconDAL.COL_NAME_PRIORITY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_PRIORITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then

                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property ReplacementManufacturer As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MANUFACTURER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property ReplacementModel As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_REPLACEMENT_MODEL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=12)> _
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then


                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)> _
    Public Property Reject_Msg_Parms As DecimalType
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(BestReplacementReconDAL.COL_NAME_REJECT_MSG_PARMS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_REJECT_MSG_PARMS, Value)
        End Set
    End Property
    Public Property ModifiedDate As DateType
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BestReplacementReconDAL.COL_NAME_MODIFIED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_MODIFIED_DATE, Value)
        End Set
    End Property
    Public Property ModifiedBy As String
        Get
            CheckDeleted()
            If Row(BestReplacementReconDAL.COL_NAME_MODIFIED_BY) Is DBNull.Value Then

                Return Nothing
            Else
                Return CType(Row(BestReplacementReconDAL.COL_NAME_MODIFIED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BestReplacementReconDAL.COL_NAME_MODIFIED_BY, Value)
        End Set
    End Property
#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(fileProcessedId As Guid) As Guid
        Get
            Dim oDealerfileProcessed As New FileProcessed(fileProcessedId)
            Dim oDealer As New Dealer(oDealerfileProcessed.FileProcessId)
            Dim oCompanyId As Guid = oDealer.CompanyId
            Return oCompanyId
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Shared Function ValidateFile(fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New BestReplacementReconDAL
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessFile(fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New BestReplacementReconDAL
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
            dal.ProcessFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function DeleteFile(fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New BestReplacementReconDAL
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DELETE)
            dal.DeleteFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BestReplacementReconDAL
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
#End Region
End Class



