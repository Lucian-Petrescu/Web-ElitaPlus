'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/30/2013)  ********************

Public Class ClaimAuthHistory
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        UniqueId = Me.Id.ToString
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
        UniqueId = Me.Id.ToString
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
        UniqueId = Id.ToString
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimAuthHistoryDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ClaimAuthHistoryDAL
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
            If row(ClaimAuthHistoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTH_HISTORY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property



    Public Property ServiceLevelId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_SERVICE_LEVEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_SERVICE_LEVEL_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimAuthorizationStatusId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2000)> _
    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property



    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_VISIT_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_VISIT_DATE, Value)
        End Set
    End Property



    Public Property DeviceReceptionDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_DEVICE_RECEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_DEVICE_RECEPTION_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
        End Set
    End Property



    Public Property ExpectedRepairDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_EXPECTED_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_EXPECTED_REPAIR_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_EXPECTED_REPAIR_DATE, Value)
        End Set
    End Property



    Public Property RepairDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property



    Public Property PickUpDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_PICK_UP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_PICK_UP_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_PICK_UP_DATE, Value)
        End Set
    End Property



    Public Property DeliveryDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_DELIVERY_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_DELIVERY_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property WhoPaysId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_WHO_PAYS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_WHO_PAYS_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_WHO_PAYS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property DefectReason() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_DEFECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_DEFECT_REASON), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_DEFECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=500)>
    Public Property TechnicalReport() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_TECHNICAL_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_TECHNICAL_REPORT), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_TECHNICAL_REPORT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property ServiceCenterReferenceNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthHistoryDAL.COL_NAME_SVC_REFERENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthHistoryDAL.COL_NAME_SVC_REFERENCE_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_SVC_REFERENCE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property VerificationNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthHistoryDAL.COL_NAME_VERIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthHistoryDAL.COL_NAME_VERIFICATION_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_VERIFICATION_NUMBER, Value)
        End Set
    End Property

    Public Property ExternalCreatedDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_EXTERNAL_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_EXTERNAL_CREATED_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_EXTERNAL_CREATED_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property IsSpecialServiceId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_IS_SPECIAL_SERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_IS_SPECIAL_SERVICE_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_IS_SPECIAL_SERVICE_ID, Value)
        End Set
    End Property



    Public Property ReverseLogisticsId() As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_REVERSE_LOGISTICS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthHistoryDAL.COL_NAME_REVERSE_LOGISTICS_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_REVERSE_LOGISTICS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property ProblemFound() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_PROBLEM_FOUND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_PROBLEM_FOUND), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_PROBLEM_FOUND, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property HistCreatedBy() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_BY), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_BY, Value)
        End Set
    End Property

    Public ReadOnly Property HistoryCreatedByName() As String
        Get
            Try
                Return New User(HistCreatedBy).UserName
            Catch ex As Exception
                Return (HistCreatedBy)
            End Try
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property HistCreatedDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property HistModifiedBy() As String
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_BY), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_BY, Value)
        End Set
    End Property



    Public Property HistModifiedDate() As DateType
        Get
            CheckDeleted()
            If row(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceCenterName() As String
        Get
            Return ServiceCenter.Description
        End Get
    End Property

    Private _serviceCenter As ServiceCenter = Nothing
    Public ReadOnly Property ServiceCenter As ServiceCenter
        Get
            If (_serviceCenter Is Nothing) Then
                If Not ServiceCenterId.Equals(Guid.Empty) Then
                    _serviceCenter = New ServiceCenter(ServiceCenterId, Dataset)
                End If
            End If
            Return _serviceCenter
        End Get
    End Property

    Public ReadOnly Property AuthorizedAmount As DecimalType
        Get
            Return CalculateAuthorizationAmount()
        End Get
    End Property

    Public ReadOnly Property ClaimAuthorizationItemChildren() As ClaimAuthorizationItemList
        Get
            Return New ClaimAuthorizationItemList(AsClaimAuthorization)
        End Get
    End Property

    Public ReadOnly Property ClaimAuthStatus() As ClaimAuthorizationStatus
        Get
            Select Case ClaimAuthorizationStatusCode
                Case Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED
                    Return ClaimAuthorizationStatus.Authorized
                Case Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED
                    Return ClaimAuthorizationStatus.Fulfilled
                Case Codes.CLAIM_AUTHORIZATION_STATUS__PAID
                    Return ClaimAuthorizationStatus.Paid
                Case Codes.CLAIM_AUTHORIZATION_STATUS__PENDING
                    Return ClaimAuthorizationStatus.Pending
                Case Codes.CLAIM_AUTHORIZATION_STATUS__TO_BE_PAID
                    Return ClaimAuthorizationStatus.ToBePaid
                Case Codes.CLAIM_AUTHORIZATION_STATUS__VOID
                    Return ClaimAuthorizationStatus.Void
                Case Codes.CLAIM_AUTHORIZATION_STATUS__RECONSILED
                    Return ClaimAuthorizationStatus.Reconsiled
                Case Codes.CLAIM_AUTHORIZATION_STATUS__SENT
                    Return ClaimAuthorizationStatus.Sent
                Case Codes.CLAIM_AUTHORIZATION_STATUS__ONHOLD
                    Return ClaimAuthorizationStatus.OnHold
                Case Else
                    Throw New InvalidOperationException
            End Select
        End Get
    End Property

    Public ReadOnly Property ClaimAuthorizationStatusCode() As String
        Get
            If (Not ClaimAuthorizationStatusId.Equals(Guid.Empty)) Then
                Return LookupListNew.GetCodeFromId(Codes.CLAIM_AUTHORIZATION_STATUS, ClaimAuthorizationStatusId)
            End If
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimAuthHistoryDAL
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

    Public Function AsClaimAuthorization() As ClaimAuthorization
        Dim claimAuth As ClaimAuthorization = New ClaimAuthorization(ClaimAuthorizationId)
        claimAuth.ClaimId = ClaimId
        claimAuth.AuthorizationNumber = AuthorizationNumber
        claimAuth.ServiceCenterId = ServiceCenterId
        claimAuth.ServiceLevelId = ServiceLevelId
        claimAuth.ClaimAuthorizationStatusId = ClaimAuthorizationStatusId
        claimAuth.SpecialInstruction = SpecialInstruction
        claimAuth.VisitDate = VisitDate
        claimAuth.DeviceReceptionDate = DeviceReceptionDate
        claimAuth.RepairDate = RepairDate
        claimAuth.PickUpDate = PickUpDate
        claimAuth.DeliveryDate = DeliveryDate
        claimAuth.WhoPaysId = WhoPaysId
        claimAuth.DefectReason = DefectReason
        claimAuth.BatchNumber = BatchNumber
        claimAuth.ServiceCenterReferenceNumber = ServiceCenterReferenceNumber
        claimAuth.ExternalCreatedDate = ExternalCreatedDate
        claimAuth.IsSpecialServiceId = IsSpecialServiceId
        claimAuth.ReverseLogisticsId = ReverseLogisticsId
        claimAuth.ProblemFound = ProblemFound
        claimAuth.Source = Source

        Return claimAuth
    End Function

    Private Function CalculateAuthorizationAmount() As DecimalType
        Dim amount As Decimal = New Decimal(0)
        For Each Item As ClaimAuthItem In ClaimAuthorizationItemChildren
            amount = amount + If(Item.Amount Is Nothing, New Decimal(0D), Item.Amount.Value)
        Next
        Return amount
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


Public Class ClaimAuthorizationHistoryList
    Inherits BusinessObjectListEnumerableBase(Of ClaimAuthorization, ClaimAuthHistory)

    Public Sub New(parent As ClaimAuthorization)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ClaimAuthHistory).ClaimAuthorizationId.Equals(CType(Parent, ClaimAuthorization).Id)
    End Function

    Private Shared Function LoadTable(parent As ClaimAuthorization) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ClaimAuthorizationHistoryList)) Then
                Dim dal As New ClaimAuthHistoryDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ClaimAuthorizationHistoryList))
            End If
            Return parent.Dataset.Tables(ClaimAuthHistoryDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class