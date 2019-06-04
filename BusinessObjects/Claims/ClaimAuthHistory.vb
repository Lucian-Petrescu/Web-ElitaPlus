'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/30/2013)  ********************

Public Class ClaimAuthHistory
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.UniqueId = Me.Id.ToString
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
        Me.UniqueId = Me.Id.ToString
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
        Me.UniqueId = Me.Id.ToString
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimAuthHistoryDAL
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
            Dim dal As New ClaimAuthHistoryDAL
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_ID, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_SERVICE_CENTER_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_VISIT_DATE, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_EXPECTED_REPAIR_DATE, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_REPAIR_DATE, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_PICK_UP_DATE, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_DELIVERY_DATE, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_WHO_PAYS_ID, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_DEFECT_REASON, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_TECHNICAL_REPORT, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_BATCH_NUMBER, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_SVC_REFERENCE_NUMBER, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_VERIFICATION_NUMBER, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_EXTERNAL_CREATED_DATE, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_IS_SPECIAL_SERVICE_ID, Value)
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_REVERSE_LOGISTICS_ID, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_PROBLEM_FOUND, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_SOURCE, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_BY, Value)
        End Set
    End Property

    Public ReadOnly Property HistoryCreatedByName() As String
        Get
            Try
                Return New User(Me.HistCreatedBy).UserName
            Catch ex As Exception
                Return (Me.HistCreatedBy)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_CREATED_DATE, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_BY, Value)
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
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthHistoryDAL.COL_NAME_HIST_MODIFIED_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceCenterName() As String
        Get
            Return Me.ServiceCenter.Description
        End Get
    End Property

    Private _serviceCenter As ServiceCenter = Nothing
    Public ReadOnly Property ServiceCenter As ServiceCenter
        Get
            If (_serviceCenter Is Nothing) Then
                If Not Me.ServiceCenterId.Equals(Guid.Empty) Then
                    _serviceCenter = New ServiceCenter(Me.ServiceCenterId, Me.Dataset)
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
            Return New ClaimAuthorizationItemList(Me.AsClaimAuthorization)
        End Get
    End Property

    Public ReadOnly Property ClaimAuthStatus() As ClaimAuthorizationStatus
        Get
            Select Case Me.ClaimAuthorizationStatusCode
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
            If (Not Me.ClaimAuthorizationStatusId.Equals(Guid.Empty)) Then
                Return LookupListNew.GetCodeFromId(Codes.CLAIM_AUTHORIZATION_STATUS, Me.ClaimAuthorizationStatusId)
            End If
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimAuthHistoryDAL
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

    Public Function AsClaimAuthorization() As ClaimAuthorization
        Dim claimAuth As ClaimAuthorization = New ClaimAuthorization(Me.ClaimAuthorizationId)
        claimAuth.ClaimId = Me.ClaimId
        claimAuth.AuthorizationNumber = Me.AuthorizationNumber
        claimAuth.ServiceCenterId = Me.ServiceCenterId
        claimAuth.ServiceLevelId = Me.ServiceLevelId
        claimAuth.ClaimAuthorizationStatusId = Me.ClaimAuthorizationStatusId
        claimAuth.SpecialInstruction = Me.SpecialInstruction
        claimAuth.VisitDate = Me.VisitDate
        claimAuth.DeviceReceptionDate = Me.DeviceReceptionDate
        claimAuth.RepairDate = Me.RepairDate
        claimAuth.PickUpDate = Me.PickUpDate
        claimAuth.DeliveryDate = Me.DeliveryDate
        claimAuth.WhoPaysId = Me.WhoPaysId
        claimAuth.DefectReason = Me.DefectReason
        claimAuth.BatchNumber = Me.BatchNumber
        claimAuth.ServiceCenterReferenceNumber = Me.ServiceCenterReferenceNumber
        claimAuth.ExternalCreatedDate = Me.ExternalCreatedDate
        claimAuth.IsSpecialServiceId = Me.IsSpecialServiceId
        claimAuth.ReverseLogisticsId = Me.ReverseLogisticsId
        claimAuth.ProblemFound = Me.ProblemFound
        claimAuth.Source = Me.Source

        Return claimAuth
    End Function

    Private Function CalculateAuthorizationAmount() As DecimalType
        Dim amount As Decimal = New Decimal(0)
        For Each Item As ClaimAuthItem In Me.ClaimAuthorizationItemChildren
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

    Public Sub New(ByVal parent As ClaimAuthorization)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ClaimAuthHistory).ClaimAuthorizationId.Equals(CType(Parent, ClaimAuthorization).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ClaimAuthorization) As DataTable
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