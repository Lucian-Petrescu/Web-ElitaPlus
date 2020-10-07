Public Class EquipmentReconWrk
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const DSNAME As String = "LIST"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "equipment_recon_wrk_id"
    Public Const COL_NAME_EQUIPMENT_RECON_WRK_ID As String = "equipment_recon_wrk_id"
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_IS_MASTER As String = "is_master"
    Public Const COL_NAME_MASTER_EQUIPMENT_DESCRIPTION As String = "master_equipment_description"
    Public Const COL_NAME_EQUIPMENT_CLASS As String = "equipment_class"
    Public Const COL_NAME_EQUIPMENT_TYPE As String = "equipment_type"
    Public Const COL_NAME_MANUFACTURER_WARRANTY As String = "manufacturer_warranty"
    Public Const COL_NAME_ATTRIBUTES As String = "attributes"
    Public Const COL_NAME_NOTE1 As String = "note1"
    Public Const COL_NAME_NOTE2 As String = "note2"
    Public Const COL_NAME_NOTE3 As String = "note3"
    Public Const COL_NAME_NOTE4 As String = "note4"
    Public Const COL_NAME_NOTE5 As String = "note5"
    Public Const COL_NAME_NOTE6 As String = "note6"
    Public Const COL_NAME_NOTE7 As String = "note7"
    Public Const COL_NAME_NOTE8 As String = "note8"
    Public Const COL_NAME_NOTE9 As String = "note9"
    Public Const COL_NAME_NOTE10 As String = "note10"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_date"
    Public Const COL_NAME_LOAD_STATUS As String = "load_status"
    Public Const COL_NAME_REPAIRABLE As String = "repairable"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
        VerifyConcurrency(sModifiedDate)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
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
#End Region
#Region "DataView Retrieveing Methods"
    Protected Sub Load()
        Try
            Dim dal As New EquipmentReconDAL
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
            Dim dal As New EquipmentReconDAL
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
    Public Shared Function LoadList(ByVal fileProcessedID As Guid) As DataView
        Try
            Dim dal As New EquipmentReconDAL
            Dim ds As DataSet

            ds = dal.LoadList(fileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(EquipmentReconDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function LoadRejectList(ByVal fileProcessedID As Guid) As DataView()
        'Try
        '    Dim dal As New EquipmentReconDAL
        '    Dim ds As DataSet

        '    ds = dal.LoadRejectList(fileProcessedID)
        '    Return (ds.Tables(EquipmentReconDAL.TABLE_NAME).DefaultView)

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
            If Row(EquipmentReconDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentReconDAL.COL_NAME_EQUIPMENT_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property FileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_FILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentReconDAL.COL_NAME_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_FILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property IsMaster As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_IS_MASTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_IS_MASTER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_IS_MASTER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property MasterEquipmentDescription As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MASTER_EQUIPMENT_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_MASTER_EQUIPMENT_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MASTER_EQUIPMENT_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property EquipmentClass As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_EQUIPMENT_CLASS) Is DBNull.Value Then

                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_EQUIPMENT_CLASS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_EQUIPMENT_CLASS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property EquipmentType As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_EQUIPMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_EQUIPMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_EQUIPMENT_TYPE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)> _
    Public Property ManufacturerWarranty As LongType
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MANUFACTURER_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EquipmentReconDAL.COL_NAME_MANUFACTURER_WARRANTY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MANUFACTURER_WARRANTY, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Attributes As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_ATTRIBUTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_ATTRIBUTES), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_ATTRIBUTES, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note1 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE1, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note2 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE2, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note3 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE3, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note4 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE4, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note5 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE5, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note6 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE6, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note7 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE7, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note8 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE8, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note9 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE9, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4000)> _
    Public Property Note10 As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_NOTE10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_NOTE10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_NOTE10, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=200)> _
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=12)> _
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=1)> _
    Public Property LoadStatus As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_LOAD_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_LOAD_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_LOAD_STATUS, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)> _
    Public Property Reject_Msg_Parms As DecimalType
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then

                Return Nothing
            Else
                Return New DecimalType(CType(Row(EquipmentReconDAL.COL_NAME_REJECT_MSG_PARMS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_REJECT_MSG_PARMS, Value)
        End Set
    End Property
    Public Property ModifiedDate As DateType
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(EquipmentReconDAL.COL_NAME_MODIFIED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MODIFIED_DATE, Value)
        End Set
    End Property
    Public Property ModifiedBy As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_MODIFIED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_MODIFIED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_MODIFIED_BY, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=255)> _
    Public Property Repairable As String
        Get
            CheckDeleted()
            If Row(EquipmentReconDAL.COL_NAME_REPAIRABLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentReconDAL.COL_NAME_REPAIRABLE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentReconDAL.COL_NAME_REPAIRABLE, Value)
        End Set
    End Property
#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(ByVal fileProcessedId As Guid) As Guid
        Get
            '    Dim oDealerfileProcessed As New FileProcessed(fileProcessedId)
            '    Dim oDealer As New Dealer(oDealerfileProcessed.FileProcessId)
            '    Dim oCompanyId As Guid = oDealer.CompanyId
            '    Return oCompanyId
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Shared Function ValidateFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New EquipmentReconDAL
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New EquipmentReconDAL
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
            dal.ProcessFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function DeleteFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New EquipmentReconDAL
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
                Dim dal As New EquipmentReconDAL
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
