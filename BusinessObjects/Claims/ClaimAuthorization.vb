'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************

Imports System.Collections.Generic

Public NotInheritable Class ClaimAuthorization
    Inherits BusinessObjectBase : Implements IInvoiceable

#Region "Constants"
    Private Const DEFAULT_AUTH_NUMBER As String = "0000000000"
#End Region
#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.OriginalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

    'New BO
    Friend Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
        Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED)
        Me.AuthorizationNumber = DEFAULT_AUTH_NUMBER
    End Sub

    'Exiting BO attaching to a BO family
    Friend Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        Me.OriginalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

    'New BO attaching to a BO family
    Friend Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED)
        Me.AuthorizationNumber = DEFAULT_AUTH_NUMBER
    End Sub

    Friend Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
        Me.OriginalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimAuthorizationDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimAuthorizationDAL
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

    Private OriginalAuthorizedAmount As Decimal = New Decimal(0D)
#End Region

#Region "Properties"

    'Key Property
    <ValidateDuplicateServiceClassType(""), ValidatePayDeductibleLineItem(""), ValidateMultiplePayDeductibleLineItem(""), ValidatePayDeductibleLineItemForContainDedutibleNo("")>
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ClaimAuthorizationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_CLAIM_ID, Value)
            Me.Claim = Nothing
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ServiceCenterId() As Guid Implements IInvoiceable.ServiceCenterId
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SERVICE_CENTER_ID, Value)
            Me.ServiceCenter = Nothing
        End Set
    End Property

    Public ReadOnly Property ServiceCenterObject() As ServiceCenter Implements IInvoiceable.ServiceCenterObject
        Get
            Return Me.ServiceCenter
        End Get
    End Property

    Public Property LoanerCenterId() As Guid Implements IInvoiceable.LoanerCenterId
        Get
            CheckDeleted()
            Return Me.ServiceCenter.LoanerCenterId
        End Get
        Private Set(ByVal Value As Guid)
            CheckDeleted()
            Me.ServiceCenter.LoanerCenterId = Value
        End Set
    End Property

    Public Property LoanerReturnedDate As DateType Implements IInvoiceable.LoanerReturnedDate


    Public ReadOnly Property CreatedDate() As DateType Implements IInvoiceable.CreatedDate
        Get
            Return MyBase.CreatedDate
        End Get
    End Property

    Public ReadOnly Property CreatedDateTime() As DateTimeType Implements IInvoiceable.CreatedDateTime
        Get
            Return MyBase.CreatedDateTime
        End Get
    End Property

    Public Property ServiceLevelId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_LEVEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_LEVEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Friend Property ClaimAuthorizationStatusId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_STATUS_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ClaimAuthorizationStatusCode() As String
        Get
            If (Not Me.ClaimAuthorizationStatusId.Equals(Guid.Empty)) Then
                Return LookupListNew.GetCodeFromId(Codes.CLAIM_AUTHORIZATION_STATUS, Me.ClaimAuthorizationStatusId)
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=2000)>
    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    <ValidVisitDate("")>
    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_VISIT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_VISIT_DATE, Value)
        End Set
    End Property

    Public Property DeviceReceptionDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_DEVICE_RECEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_DEVICE_RECEPTION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
        End Set
    End Property

    Public Property ExpectedRepairDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_EXPECTED_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_EXPECTED_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_EXPECTED_REPAIR_DATE, Value)
        End Set
    End Property

    Public Property DeliveryDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_DELIVERY_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_DELIVERY_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property WhoPaysId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_WHO_PAYS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_WHO_PAYS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_WHO_PAYS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property DefectReason() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_DEFECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_DEFECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_DEFECT_REASON, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)>
    Public Property TechnicalReport() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_TECHNICAL_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_TECHNICAL_REPORT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_TECHNICAL_REPORT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)>
    Public Property ServiceCenterReferenceNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SVC_REFERENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_SVC_REFERENCE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SVC_REFERENCE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property VerificationNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_VERIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_VERIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_VERIFICATION_NUMBER, Value)
        End Set
    End Property

    Public Property ExternalCreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_EXTERNAL_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_EXTERNAL_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_EXTERNAL_CREATED_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IsSpecialServiceId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_IS_SPECIAL_SERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_IS_SPECIAL_SERVICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_IS_SPECIAL_SERVICE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReverseLogisticsId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_REVERSE_LOGISTICS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_REVERSE_LOGISTICS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_REVERSE_LOGISTICS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property ProblemFound() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_PROBLEM_FOUND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_PROBLEM_FOUND), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_PROBLEM_FOUND, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property
    Public _reversed As boolean = False
    Public Property Reversed() As Boolean
        Get
            Return _reversed
        End Get
        Set(ByVal value As Boolean)
            _reversed = value
        End Set
    End Property

    Public ReadOnly Property AuthorizationAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZAION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZAION_AMOUNT), Decimal))
            End If
        End Get
    End Property

    Public ReadOnly Property ServiceOrderType() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_ORDER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_SERVICE_ORDER_TYPE), String)
            End If
        End Get
    End Property

    Public Property ClaimAuthStatus() As ClaimAuthorizationStatus
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
                Case Codes.CLAIM_AUTHORIZATION_STATUS__VOID
                    Return ClaimAuthorizationStatus.Void
                Case Codes.CLAIM_AUTHORIZATION_STATUS__RECONSILED
                    Return ClaimAuthorizationStatus.Reconsiled
                Case Codes.CLAIM_AUTHORIZATION_STATUS__TO_BE_PAID
                    Return ClaimAuthorizationStatus.ToBePaid
                Case Codes.CLAIM_AUTHORIZATION_STATUS__SENT
                    Return ClaimAuthorizationStatus.Sent
                Case Codes.CLAIM_AUTHORIZATION_STATUS__ONHOLD
                    Return ClaimAuthorizationStatus.OnHold
                Case Codes.CLAIM_AUTHORIZATION_STATUS__CANCELLED
                    Return ClaimAuthorizationStatus.Cancelled
                Case Codes.CLAIM_AUTHORIZATION_STATUS__COLLECTED
                    Return ClaimAuthorizationStatus.Collected
                Case Codes.CLAIM_AUTHORIZATION_STATUS__REVERSED
                    Return ClaimAuthorizationStatus.Reversed
                Case Else
                    Throw New InvalidOperationException
            End Select
        End Get
        Set(ByVal value As ClaimAuthorizationStatus)
            Select Case value
                Case ClaimAuthorizationStatus.Authorized
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED)
                Case ClaimAuthorizationStatus.Fulfilled
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED)
                Case ClaimAuthorizationStatus.Paid
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__PAID)
                Case ClaimAuthorizationStatus.Pending
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__PENDING)
                Case ClaimAuthorizationStatus.Void
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__VOID)
                Case ClaimAuthorizationStatus.Reconsiled
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__RECONSILED)
                Case ClaimAuthorizationStatus.ToBePaid
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__TO_BE_PAID)
                Case ClaimAuthorizationStatus.Sent
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__SENT)
                Case ClaimAuthorizationStatus.OnHold
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__ONHOLD)
                Case ClaimAuthorizationStatus.Cancelled
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__CANCELLED)          
                Case ClaimAuthorizationStatus.Collected
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__COLLECTED)
                Case ClaimAuthorizationStatus.Reversed
                    Me.ClaimAuthorizationStatusId = LookupListNew.GetIdFromCode(Codes.CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__REVERSED)
                Case ClaimAuthorizationStatus.None
                    Throw New NotSupportedException
            End Select
        End Set
    End Property

    Public Property AuthorizedAmount As DecimalType Implements IInvoiceable.AuthorizedAmount
        Get
            Return CalculateAuthorizationAmount()
        End Get
        Private Set(ByVal value As DecimalType)
            AuthorizedAmount = value
        End Set
    End Property
    Private _ReimbursementAmount As DecimalType = Nothing
    Public ReadOnly Property ReimbursementAmount As DecimalType
        Get
            If Me._ReimbursementAmount Is Nothing Then
                Me._ReimbursementAmount = CalculateReimbursementAmount()
            End If
            Return Me._ReimbursementAmount
        End Get
    End Property
    Private _ReplacementAmount As DecimalType = Nothing
    Public ReadOnly Property ReplacementAmount As DecimalType
        Get
            If Me._ReplacementAmount Is Nothing Then
                Me._ReplacementAmount = CalculateReplacementAmount()
            End If
            Return Me._ReplacementAmount
        End Get
    End Property


    Public ReadOnly Property PayDeductibleAmount As DecimalType
        Get
            Dim amount As Decimal = New Decimal(0)
            For Each Item As ClaimAuthItem In Me.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False _
                AndAlso (i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE And i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE))

                amount = amount + If(Item.Amount Is Nothing, New Decimal(0D), Item.Amount.Value)

            Next
            Return amount
        End Get
    End Property

    Public ReadOnly Property IsSupervisorAuthorizationRequired() As Boolean
        Get
            Dim bIsReq As Boolean
            Dim bDaysExceeded As Boolean = Me.IsDaysLimitExceeded
            Dim bAuthorizationExceeded As Boolean = Me.IsAuthorizationLimitExceeded

            If bAuthorizationExceeded Then
                bIsReq = True
            Else
                If (Not bDaysExceeded) Then
                    bIsReq = False
                Else
                    If (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                        ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                        bIsReq = False
                    Else
                        bIsReq = True
                    End If
                End If
            End If
            Return bIsReq
        End Get
    End Property

    Public ReadOnly Property IsAuthorizationLimitExceeded() As Boolean
        Get
            If Not Me.AuthorizedAmount Is Nothing AndAlso Me.Claim.AuthorizedAmount.Value > Me.Claim.AuthorizationLimit.Value Then
                Return True
            End If
            Return False
        End Get
    End Property

    Private ReadOnly Property IsAuthorizedAmountChanged() As Boolean
        Get
            If Me.OriginalAuthorizedAmount = Me.AuthorizedAmount Then Return False Else Return True
        End Get
    End Property

    Public ReadOnly Property IsDaysLimitExceeded() As Boolean
        Get
            'For service warranties
            'Since the RepairDate for the new Service Warranty Claim is Blank, we need to get the 
            'RepairDate for the parent Repair Claim.
            'BEGIN - New logic - Ravi
            'If (Me.parentClaim Is Nothing AndAlso Not Me.ClaimNumber Is Nothing AndAlso Me.ClaimNumber.EndsWith("S")) Then
            '    Me.parentClaim = New Claim(Me.ClaimNumber.TrimEnd("S"), Me.CompanyId)
            'End If
            'If (Me.parentClaim Is Nothing OrElse (Not Me.ClaimNumber Is Nothing AndAlso Not Me.ClaimNumber.EndsWith("S"))) Then
            '    Return False
            'End If
            'If Not Me.ClaimActivityCode Is Nothing AndAlso Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso Not Me.parentClaim.RepairDate Is Nothing AndAlso Not Me.ServiceCenterObject Is Nothing Then
            '    Dim elpasedDaysSinceRepaired As Long
            '    If Not Me.parentClaim.PickUpDate Is Nothing Then
            '        elpasedDaysSinceRepaired = Date.Now.Subtract(Me.parentClaim.PickUpDate.Value).Days
            '    Else
            '        elpasedDaysSinceRepaired = Date.Now.Subtract(Me.parentClaim.RepairDate.Value).Days
            '    End If

            '    Return elpasedDaysSinceRepaired > Me.ServiceCenter.ServiceWarrantyDays.Value
            'Else
            '    Return False
            'End If
            'END - New logic - Ravi
        End Get
    End Property

    Public ReadOnly Property ServiceCenterName() As String
        Get
            Return Me.ServiceCenterObject.Description
        End Get
    End Property

    Public ReadOnly Property CanVoidClaimAuthorization() As Boolean
        Get
            If ClaimAuthStatus = ClaimAuthorizationStatus.Authorized OrElse ClaimAuthStatus = ClaimAuthorizationStatus.Pending OrElse ClaimAuthStatus = ClaimAuthorizationStatus.Sent Then
                return True
            Else 
                Return False
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Private Property ContainsDeductibleId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID, Value)
        End Set
    End Property

    Public Property ContainsDeductible() As Boolean
        Get
            Return If(Me.ContainsDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y"), True, False)
        End Get
        Set(ByVal Value As Boolean)
            Me.ContainsDeductibleId = If(Value, LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y"), LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N"))
        End Set
    End Property

    Public Property SubStatusReason() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_SUB_STATUS_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_SUB_STATUS_REASON), String)
            End If
        End Get

        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_SUB_STATUS_REASON, Value)
        End Set
    End Property

    Public ReadOnly Property LinkedClaimAurthID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_LINKED_CLAIM_AUTH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_LINKED_CLAIM_AUTH_ID), Byte()))
            End If
        End Get

    End Property
    public _revAdjReasonId as Guid         
    Public Property RevAdjustmentReasonId() As Guid
        Get           
            Return me._revAdjReasonId          
        End Get
        Set(ByVal Value As guid)          
            _revAdjReasonId = value        
        End Set

    End Property


    Public Property AuthSubStatus() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_SUB_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_SUB_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_SUB_STATUS_XCD, Value)
        End Set
    End Property

#Region "Invoiceable properties"

    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property AuthorizationNumber() As String Implements IInvoiceable.AuthorizationNumber
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    <ValidRepairDate("")>
    Public Property RepairDate() As DateType Implements IInvoiceable.RepairDate
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property

    Public Property PickUpDate() As DateType Implements IInvoiceable.PickUpDate
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE, Value)
        End Set
    End Property

    'Public ReadOnly Property SvcControlNumber() As String Implements IInvoiceable.SvcControlNumber
    '    Get
    '        Return Me.AuthorizationNumber
    '    End Get
    'End Property

    Public Property ClaimNumber() As String Implements IInvoiceable.ClaimNumber
        Get
            Return Me.Claim.ClaimNumber
        End Get
        Set(ByVal value As String)
            Me.Claim.ClaimNumber = value
        End Set
    End Property

    Public Property InvoiceProcessDate As DateType Implements IInvoiceable.InvoiceProcessDate


    Public Property InvoiceDate As DateType Implements IInvoiceable.InvoiceDate

    Public Property ClaimAuthfulfillmentTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_AUTH_FULFILLMENT_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_AUTH_FULFILLMENT_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_AUTH_FULFILLMENT_TYPE_XCD, Value)
        End Set
    End Property

    Public Property AuthTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_AUTH_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_AUTH_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_AUTH_TYPE_XCD, Value)
        End Set
    End Property

    Public Property PartyReferenceId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_PARTY_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthorizationDAL.COL_NAME_PARTY_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_PARTY_REFERENCE_ID, Value)
        End Set
    End Property

    Public Property PartyTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimAuthorizationDAL.COL_NAME_PARTY_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthorizationDAL.COL_NAME_PARTY_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_PARTY_TYPE_XCD, Value)
        End Set
    End Property

    Public Readonly Property CashPymtMethodXcd() As String
        Get
            CheckDeleted()
            If row(ClaimAuthorizationDAL.COL_NAME_CASH_PYMT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthorizationDAL.COL_NAME_CASH_PYMT_METHOD_XCD), String)
            End If
        End Get        
    End Property
#Region "Derived Properties"

    Public ReadOnly Property ClaimAuthInvoiceDate() As DateType
        Get
            If Me.ClaimInvoiceId.Equals(Guid.Empty) Then
                Return Nothing
            End If
            Dim oClaimInvoice As New ClaimInvoice(Me.ClaimInvoiceId, Me.Dataset)
            Return oClaimInvoice.InvoiceDate
        End Get
    End Property

    Public ReadOnly Property ClaimAuthInvoiceProcessDate() As DateType
        Get
            If Me.ClaimInvoiceId.Equals(Guid.Empty) Then
                Return Nothing
            End If
            Dim oClaimInvoice As New ClaimInvoice(Me.ClaimInvoiceId, Me.Dataset)
            Return oClaimInvoice.CreatedDate
        End Get
    End Property

    Public ReadOnly Property ClaimInvoiceId() As Guid
        Get
            Dim row As DataRow = Nothing
            Dim dal As New ClaimInvoiceDAL
            If Me.Dataset.Tables.IndexOf(ClaimInvoiceDAL.TABLE_NAME) >= 0 Then
                If Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Columns.Contains(ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Then
                    row = FindRow(Me.Id, ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME))
                End If
            End If
            If row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByClaimAuthId(Me.Dataset, Me.Claim.Id, Me.Id)
                row = Me.FindRow(Me.Id, ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME))
            End If
            If (row Is Nothing) Then
                Return Guid.Empty
            Else
                Return New ClaimInvoice(row).Id
            End If
        End Get
    End Property

#End Region


    Dim _isComingFromPayClaim As Boolean = False 
    Public Property IsComingFromPayClaim() As Boolean Implements IInvoiceable.IsComingFromPayClaim
        Get
            Return _isComingFromPayClaim
        End Get
        Set(ByVal Value As Boolean)
            _isComingFromPayClaim = Value
        End Set
    End Property

    Public ReadOnly Property ClaimActivityCode() As String Implements IInvoiceable.ClaimActivityCode
        Get
            Return Me.Claim.ClaimActivityCode
        End Get
    End Property

    Public Property ClaimActivityId() As Guid Implements IInvoiceable.ClaimActivityId
        Get
            Return Me.Claim.ClaimActivityId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.ClaimActivityId = value
        End Set
    End Property

    Public Property ReasonClosedId() As Guid Implements IInvoiceable.ReasonClosedId
        Get
            Return Me.Claim.ReasonClosedId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.ReasonClosedId = value
        End Set
    End Property

    Public Property ClaimClosedDate() As DateType Implements IInvoiceable.ClaimClosedDate
        Get
            Return Me.Claim.ClaimClosedDate
        End Get
        Set(ByVal value As DateType)
            Me.Claim.ClaimClosedDate = value
        End Set
    End Property

    Public ReadOnly Property Claim_Id() As Guid Implements IInvoiceable.Claim_Id
        Get
            Return Me.ClaimId
        End Get
    End Property

    Public ReadOnly Property IsDirty() As Boolean Implements IInvoiceable.IsDirty
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public ReadOnly Property PayDeductibleId() As Guid Implements IInvoiceable.PayDeductibleId
        Get
            Return Me.Claim.Dealer.PayDeductibleId
        End Get
    End Property

    Public Property Deductible() As DecimalType Implements IInvoiceable.Deductible
        Get
            If Me.ContainsDeductible Then
                Return Me.Claim.Deductible
            Else
                Return New DecimalType(0D)
            End If
        End Get
        Private Set(ByVal value As DecimalType)
            Me.Claim.Deductible = value
        End Set
    End Property

    Public Property DiscountAmount() As DecimalType Implements IInvoiceable.DiscountAmount
        Get
            Return Me.Claim.DiscountAmount
        End Get
        Set(ByVal value As DecimalType)
            Me.Claim.DiscountAmount = value
        End Set
    End Property

    Public Property LiabilityLimit() As DecimalType Implements IInvoiceable.LiabilityLimit
        Get
            Return Me.Claim.LiabilityLimit
        End Get
        Set(ByVal value As DecimalType)
            Me.Claim.LiabilityLimit = value
        End Set
    End Property

    Public ReadOnly Property AboveLiability() As DecimalType Implements IInvoiceable.AboveLiability
        Get
            Return Me.Claim.AboveLiability
        End Get

    End Property

    Public ReadOnly Property ClaimAuthorizationId() As Guid Implements IInvoiceable.ClaimAuthorizationId
        Get
            Return Me.Id
        End Get
    End Property

    Public ReadOnly Property RiskType() As String Implements IInvoiceable.RiskType
        Get
            CheckDeleted()
            Return Me.Claim.RiskType
        End Get
    End Property

    Public Property RiskTypeId() As Guid Implements IInvoiceable.RiskTypeId
        Get
            Return Me.Claim.RiskTypeId
        End Get
        Set(ByVal Value As Guid)
            Me.Claim.RiskTypeId = Value
        End Set
    End Property

    Public Sub VerifyConcurrency(ByVal sModifiedDate As String) Implements IInvoiceable.VerifyConcurrency
        Me.Claim.VerifyConcurrency(sModifiedDate)
    End Sub

    Public ReadOnly Property MethodOfRepairCode() As String Implements IInvoiceable.MethodOfRepairCode
        Get
            Return Me.Claim.MethodOfRepairCode
        End Get
    End Property

    Public Property RepairEstimate() As DecimalType Implements IInvoiceable.RepairEstimate
        Get
            Return 0D
        End Get
        Set(ByVal value As DecimalType)

        End Set
    End Property

    Public Property RepairCodeId() As Guid Implements IInvoiceable.RepairCodeId
        Get
            Return Me.Claim.RepairCodeId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.RepairCodeId = value
        End Set
    End Property

    Public Property StatusCode() As String Implements IInvoiceable.StatusCode
        Get
            Return Me.Claim.StatusCode
        End Get
        Set(ByVal value As String)
            Me.Claim.StatusCode = value
        End Set
    End Property

    Public Property CauseOfLossId() As Guid Implements IInvoiceable.CauseOfLossId
        Get
            Return Me.Claim.CauseOfLossId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.CauseOfLossId = value
        End Set
    End Property

    Public Property CompanyId() As Guid Implements IInvoiceable.CompanyId
        Get
            Return Me.Claim.CompanyId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.CompanyId = value
        End Set
    End Property

    Public ReadOnly Property CertificateId() As Guid Implements IInvoiceable.CertificateId
        Get
            Return Me.Claim.CertificateId
        End Get
    End Property

    Public ReadOnly Property CustomerName() As String Implements IInvoiceable.CustomerName
        Get
            Return Me.Claim.Certificate.CustomerName
        End Get
    End Property

    Public Property CertItemCoverageId() As Guid Implements IInvoiceable.CertItemCoverageId
        Get
            Return Me.Claim.CertItemCoverageId
        End Get
        Set(ByVal value As Guid)
            Me.Claim.CertItemCoverageId = value
        End Set
    End Property

    Public Property IsRequiredCheckLossDateForCancelledCert() As Boolean Implements IInvoiceable.IsRequiredCheckLossDateForCancelledCert
        Get
            Return Me.Claim.IsRequiredCheckLossDateForCancelledCert
        End Get
        Set(ByVal value As Boolean)
            Me.Claim.IsRequiredCheckLossDateForCancelledCert = value
        End Set
    End Property

    Public ReadOnly Property CanDisplayVisitAndPickUpDates() As Boolean Implements IInvoiceable.CanDisplayVisitAndPickUpDates
        Get
            Return False
        End Get
    End Property

    Public Sub SetPickUpDateFromLoanerReturnedDate() Implements IInvoiceable.SetPickUpDateFromLoanerReturnedDate
        If Not Me.LoanerReturnedDate Is Nothing Then Me.PickUpDate = Me.LoanerReturnedDate
    End Sub

    'Salvage Amount is currently not supported for Multi Auth Claims
    Public Property SalvageAmount() As DecimalType Implements IInvoiceable.SalvageAmount
        Get
            Return 0D
        End Get
        Private Set(ByVal value As DecimalType)
            Throw New NotSupportedException
        End Set
    End Property

    'Implementation of Assurant Pays Amount for Multi Auth Claims
    Public ReadOnly Property AssurantPays() As DecimalType Implements IInvoiceable.AssurantPays
        Get
            Dim assurPays As Decimal = 0D
            Dim liabLimit As Decimal = Me.LiabilityLimit.Value

            If Not Me.Claim.DiscountPercent Is Nothing Then
                Me.DiscountAmount = AuthorizedAmount * (CType(Me.Claim.DiscountPercent, Decimal) / 100)
            End If

            If (liabLimit = 0D And CType(Me.Claim.Certificate.ProductLiabilityLimit.ToString, Decimal) = 0 And CType(Me.Claim.CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0) Then
                liabLimit = 999999999.99
            End If

            If (Me.AuthorizedAmount > liabLimit) Then
                If Me.ContainsDeductible Then
                    assurPays = liabLimit - IIf(Me.Deductible Is Nothing, New Decimal(0D), Me.Deductible) - IIf(Me.SalvageAmount, New Decimal(0D), Me.SalvageAmount)
                Else
                    assurPays = liabLimit - IIf(Me.SalvageAmount, New Decimal(0D), Me.SalvageAmount)
                End If
            Else
                If Me.ContainsDeductible Then
                    assurPays = Me.AuthorizedAmount.Value - IIf(Me.Deductible Is Nothing, New Decimal(0D), Me.Deductible) - IIf(Me.SalvageAmount Is Nothing, New Decimal(0D), Me.SalvageAmount)
                Else
                    assurPays = Me.AuthorizedAmount.Value - IIf(Me.SalvageAmount Is Nothing, New Decimal(0D), Me.SalvageAmount)
                End If
            End If
            If Me.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                If (assurPays < 0D) Then
                    assurPays = 0D
                End If
            End If

            Return New DecimalType(assurPays)
        End Get
    End Property

    'Implementation of Consumer Pays Amount for Multi Auth Claims
    Public ReadOnly Property ConsumerPays() As DecimalType Implements IInvoiceable.ConsumerPays
        Get
            Dim cPays As Decimal = 0D
            Dim aPays As Decimal = Me.AssurantPays.Value
            Dim sal As Decimal = Me.SalvageAmount.Value
            If Me.AuthorizedAmount.Value > aPays Then
                cPays = Me.AuthorizedAmount.Value - aPays - sal
            End If

            Return New DecimalType(cPays)
        End Get
    End Property

    'KDDI Changes
    Public ReadOnly Property IsReshipmentAllowed() As String Implements IInvoiceable.IsReshipmentAllowed
        Get
            Return Me.Claim.Dealer.Is_Reshipment_Allowed
        End Get
    End Property
    Public ReadOnly Property IsCancelShipmentAllowed() As String Implements IInvoiceable.IsCancelShipmentAllowed
        Get
            Return Me.Claim.Dealer.Is_Cancel_Shipment_Allowed
        End Get
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property Locator() As String
        Get
            CheckDeleted()
            If row(ClaimAuthorizationDAL.COL_NAME_LOCATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimAuthorizationDAL.COL_NAME_LOCATOR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimAuthorizationDAL.COL_NAME_LOCATOR, Value)
        End Set
    End Property
#End Region

#Region "Invoiceable Methods"

    Public Sub SaveClaim(Optional ByVal Transaction As IDbTransaction = Nothing) Implements IInvoiceable.SaveClaim
        Me.Claim.Save(Transaction)
    End Sub

    Public Sub CloseTheClaim() Implements IInvoiceable.CloseTheClaim
        Me.Claim.CloseTheClaim()
    End Sub

    Public Sub CalculateFollowUpDate() Implements IInvoiceable.CalculateFollowUpDate
        Me.Claim.CalculateFollowUpDate()
    End Sub

    Public Sub HandleGVSTransactionCreation(ByVal commentId As Guid, ByVal pIsNew As Nullable(Of Boolean)) Implements IInvoiceable.HandleGVSTransactionCreation
        'APR 10th 2013 : GVS transactions have not been handled for Multi Auth Claims at this point of time
        Throw New NotImplementedException
    End Sub

    Private _claimStatusBO As ClaimStatus
    Public Function AddExtendedClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus Implements IInvoiceable.AddExtendedClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            _claimStatusBO = New ClaimStatus(claimStatusId, Me.Dataset)
        Else
            _claimStatusBO = New ClaimStatus(Me.Dataset)
        End If

        Return _claimStatusBO
    End Function

#End Region

#End Region
    
#Region "Instance Methods"
    Public Sub Save()
        'Check Claim AuthorizationItem list should have atleast one Auth item
        If (Me.ClaimAuthorizationItemChildren.Count = 0) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "CLAIM_AUTH_ITEM_COUNT_CANNOT_BE_ZERO")
        End If

        For Each authItem As ClaimAuthItem In Me.ClaimAuthorizationItemChildren
            authItem.Validate()
        Next
        Me.Validate()
        Me.Claim.Validate()

        If (Not Me.IsNew And (Me.IsAuthorizedAmountChanged Or Me.ClaimAuthorizationItemChildren.HasCollectionChanged) And
            (Me.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized Or Me.ClaimAuthStatus = ClaimAuthorizationStatus.Pending)) Then
            'Create New Auth with line Items in original Auth
            Dim claimAuth As ClaimAuthorization = CType(Me.Claim.ClaimAuthorizationChildren().GetNewChild(), ClaimAuthorization)
            claimAuth.CopyClaimAuthFromExiting(Me)
            'Void the existingAuth
            Me.ClaimAuthStatus = ClaimAuthorizationStatus.Void
            claimAuth.Save()
        End If
        If Not Me.ClaimAuthStatus = ClaimAuthorizationStatus.Void Then
            If Me.ClaimAuthStatus = ClaimAuthorizationStatus.Pending AndAlso Not Me.IsSupervisorAuthorizationRequired Then
                Me.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized
            End If
            If Me.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized AndAlso Me.IsSupervisorAuthorizationRequired Then
                Me.ClaimAuthStatus = ClaimAuthorizationStatus.Pending
            End If

            If (Me.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized AndAlso Not Me.RepairDate Is Nothing) Then
                
                    Me.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled
            End If
        End If        

        Try

           MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimAuthorizationDAL
                dal.UpdateFamily(Me.Dataset, Me.Claim.CompanyId)
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

    Public Sub Prepopulate(ByVal serviceCenterId As Guid, ByVal claimid As Guid)
        Me.ServiceCenterId = serviceCenterId
        Me.ClaimId = claimid
        Me.WhoPaysId = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Me.IsSpecialServiceId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        Me.ReverseLogisticsId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        Me.ContainsDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        Try
            Dim dv As PriceListDetail.PriceListResultsDV = Me.GetRepairPricesforMethodofRepair(Me.ServiceCenterId)
            PopulateClaimAuthItems(dv)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, ex.InnerException, Messages.PRICE_LIST_NOT_FOUND)
        End Try

        If (Me.IsSupervisorAuthorizationRequired) Then Me.ClaimAuthStatus = ClaimAuthorizationStatus.Pending

    End Sub

    Friend Sub Prepopulate(ByVal serviceCenterId As Guid, ByVal claimid As Guid, ByVal dv As PriceListDetail.PriceListResultsDV)
        Me.ServiceCenterId = serviceCenterId
        Me.ClaimId = claimid
        Me.WhoPaysId = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Me.IsSpecialServiceId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
        Me.ReverseLogisticsId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)

        Try
            PopulateClaimAuthItems(dv)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, ex.InnerException, Messages.PRICE_LIST_NOT_FOUND)
        End Try

        If (Me.IsSupervisorAuthorizationRequired) Then Me.ClaimAuthStatus = ClaimAuthorizationStatus.Pending

    End Sub

    Friend Sub PopulateClaimAuthItems(ByVal priceListDetailDV As PriceListDetail.PriceListResultsDV)
        If (priceListDetailDV Is Nothing Or priceListDetailDV.Count = 0) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
        End If
        Me.ContainsDeductible = False

        For Each item As DataRowView In priceListDetailDV
            Dim claimAuthItem As ClaimAuthItem = CType(Me.ClaimAuthorizationItemChildren.GetNewChild(Me.Id), ClaimAuthItem)
            claimAuthItem.ClaimAuthorizationId = Me.Id
            claimAuthItem.ServiceClassId = New Guid(CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_CLASS_ID), Byte()))
            claimAuthItem.ServiceTypeId = New Guid(CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_TYPE_ID), Byte()))
            claimAuthItem.VendorSku = CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_VENDOR_SKU), String)
            claimAuthItem.VendorSkuDescription = CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_VENDOR_SKU_DESC), String)
            claimAuthItem.Amount = CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal)
            If LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y") = New Guid(CType(item.Row(PriceListDetail.PriceListResultsDV.COL_NAME_CONTAINS_DEDUCTIBLE_ID), Byte())) Then
                Me.ContainsDeductible = True
            End If
        Next
    End Sub

    Public Sub Void()
        Me.ClaimAuthStatus = ClaimAuthorizationStatus.Void
        Me.Save()

    End Sub

    Private Function CalculateAuthorizationAmount() As DecimalType
        Dim amount As Decimal = New Decimal(0)
        For Each Item As ClaimAuthItem In Me.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
            If Item.AdjustmentReasonId.Equals(Guid.Empty) And
                Not (Item.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE Or
                 Item.ServiceClassCode = Codes.SERVICE_CLASS__MISCELLANEOUS) Then
                amount = amount + If(Item.Amount Is Nothing, New Decimal(0D), Item.Amount.Value)
            End If
        Next
        Return amount
    End Function
    Private Function CalculateReimbursementAmount() As DecimalType
        Dim amount As Decimal = New Decimal(0)
        Dim Item As ClaimAuthItem
        Item = Me.ClaimAuthorizationItemChildren.Where(Function(i) (i.IsDeleted = False) _
                                                                         AndAlso (i.AdjustmentReasonId.IsEmpty()) _
                                                                         AndAlso i.ServiceClassCode = Codes.SERVICE_CLASS__REIMBURSEMENT).FirstOrDefault

        amount = If(Item Is Nothing OrElse Item.Amount Is Nothing, New Decimal(0D), Item.Amount.Value)
        Return amount

    End Function
    Private Function CalculateReplacementAmount() As DecimalType
        Dim amount As Decimal = New Decimal(0)
        Dim Item As ClaimAuthItem
        Item = Me.ClaimAuthorizationItemChildren.Where(Function(i) (i.IsDeleted = False) _
                                                                         AndAlso (i.AdjustmentReasonId.IsEmpty()) _
                                                                         AndAlso (i.ServiceClassCode = Codes.SERVICE_CLASS__REPLACEMENT) _
                                                                         AndAlso (i.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE)).FirstOrDefault

        amount = If(Item Is Nothing OrElse Item.Amount Is Nothing, New Decimal(0D), Item.Amount.Value)
        Return amount

    End Function

    Friend Sub AddDeductibleLineItem()
        Dim serviceClassType As ServiceClassType = ServiceCLassTypeList.Instance.GetDetails(Codes.SERVICE_CLASS__DEDUCTIBLE, Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)
        Dim claimAuthItem As ClaimAuthItem = CType(Me.ClaimAuthorizationItemChildren.GetNewChild(Me.Id), ClaimAuthItem)
        claimAuthItem.ClaimAuthorizationId = Me.Id
        claimAuthItem.ServiceClassId = serviceClassType.ServiceClassId
        claimAuthItem.ServiceTypeId = serviceClassType.ServiceTypeId
        claimAuthItem.VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
        claimAuthItem.VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
        claimAuthItem.Amount = Me.Claim.Deductible

    End Sub

    Friend Sub CopyClaimAuthFromExiting(ByVal claimauth As ClaimAuthorization)
        For Each item As ClaimAuthItem In claimauth.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
            If Not item.IsDeleted Then
                Dim claimAuthItem As ClaimAuthItem = CType(Me.ClaimAuthorizationItemChildren.GetNewChild(Me.Id), ClaimAuthItem)
                claimAuthItem.CopyFrom(item)
                claimAuthItem.ClaimAuthorizationId = Me.Id
                claimAuthItem.Amount = item.Amount
                claimAuthItem.Save()
                item.Amount = item.OrginalAmount
                If (item.IsNew) Then
                    item.Delete()
                End If
            End If
        Next
        Me.CopyFrom(claimauth)

    End Sub

    Friend function ReverseClaimAuthFromExiting(ByVal claimauth As ClaimAuthorization) As guid
       dim newAuthItemId As Guid
       dim oClaimAuthItem As New ClaimAuthItem
       oClaimAuthItem = (From item As ClaimAuthItem In Me.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False) Select item Order By item.CreatedDate Descending).FirstOrDefault()

       ' For Each item As ClaimAuthItem In claimauth.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
            If Not oClaimAuthItem.IsDeleted Then
                Dim newclaimAuthItem As ClaimAuthItem = me.GetNewAuthorizationItemChild() 
                newAuthItemId = newclaimAuthItem.Id
                'CType(Me.ClaimAuthorizationItemChildren.GetNewChild(Me.Id), ClaimAuthItem)
                newclaimAuthItem.CopyFrom(oClaimAuthItem)                
               newclaimAuthItem.LineItemNumber = ctype(oClaimAuthItem.LineItemNumber,Integer) + 1
                newclaimAuthItem.ClaimAuthorizationId = Me.Id
                newclaimAuthItem.Amount = oClaimAuthItem.Amount * -1D
                newclaimAuthItem.AdjustmentReasonId = me.RevAdjustmentReasonId
                newclaimAuthItem.Save()
                oClaimAuthItem.Amount = oClaimAuthItem.OrginalAmount
                If (oClaimAuthItem.IsNew) Then
                    oClaimAuthItem.Delete()
                End If
            End If
        'Next
        Me.CopyFrom(claimauth)  
        Return newAuthItemId

    End function



    Public Sub RejectInvoiceChanges()
        Me.Row(ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE) = Me.Row(ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE, DataRowVersion.Original)
        Me.Row(ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE) = Me.Row(ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE, DataRowVersion.Original)
    End Sub

    Public Function GetRepairPricesforMethodofRepair(ByVal serviceCenterId As Guid) As DataView

        Dim equipmentId As Guid, equipmentclassId As Guid, conditionId As Guid

        Dim ServiceCenter As ServiceCenter = New ServiceCenter(serviceCenterId)

        If (Me.Claim.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If Not Me.Claim.ClaimedEquipment Is Nothing Or Not Me.Claim.ClaimedEquipment.EquipmentBO Is Nothing Then
                equipmentId = Me.Claim.ClaimedEquipment.EquipmentId
                equipmentclassId = Me.Claim.ClaimedEquipment.EquipmentBO.EquipmentClassId
                conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)
            Else
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "NO_CLAIMED_EQUIPMENT_FOUND")
            End If
        End If


        'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Claim.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
        '    conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)

        '    If Not Claim.ClaimedEquipment Is Nothing AndAlso Not Claim.ClaimedEquipment.EquipmentBO Is Nothing Then
        '        equipmentId = Claim.ClaimedEquipment.EquipmentBO.Id
        '        equipmentclassId = Claim.ClaimedEquipment.EquipmentBO.EquipmentClassId

        '    ElseIf Not Claim.ClaimedEquipment Is Nothing Then
        '        equipmentId = Equipment.FindEquipment(Claim.Dealer.Dealer, Claim.ClaimedEquipment.Manufacturer, Claim.ClaimedEquipment.Model, Date.Today)
        '        If (Not equipmentId = Guid.Empty) Then
        '            equipmentclassId = New Equipment(equipmentId).EquipmentClassId
        '        End If

        '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Claim.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
        '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "NO_CLAIMED_EQUIPMENT_FOUND")
        '    End If
        'End If

        Dim dv As PriceListDetail.PriceListResultsDV
        dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.Claim.MethodOfRepairId, Me.Claim.CompanyId,
                                                              ServiceCenter.Code, Me.Claim.RiskTypeId, Me.Claim.LossDate,
                                                              Me.Claim.Certificate.SalesPrice.Value, equipmentclassId, equipmentId, conditionId,
                                                              Me.Claim.Dealer.Id, String.Empty)
        Return dv

    End Function

    Public Function IsPriceListConfigured(ByVal serviceCenterId As Guid) As Boolean
        Dim flag As Boolean = True

        Dim dv As PriceListDetail.PriceListResultsDV = Me.GetRepairPricesforMethodofRepair(serviceCenterId)
        If dv Is Nothing OrElse dv.Count = 0 Then flag = False

        Return flag
    End Function

    Public Function ContainsDeductibleLineItem() As Boolean
        Dim flag As Boolean = False

        For Each item As ClaimAuthItem In Me.ClaimAuthorizationItemChildren.Where(Function(i As ClaimAuthItem) i.IsDeleted = False)
            If ServiceCLassTypeList.Instance.IsDeductibleApplicable(item.ServiceClassId, item.ServiceTypeId) Then
                flag = True
                Exit For
            End If
        Next

        Return flag

    End Function

    Friend Sub EvaluateContainsDeductible()
        Me.ContainsDeductible = Me.ContainsDeductibleLineItem
        If Not Me.ContainsDeductible Then
            Dim item As ClaimAuthItem = Me.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False AndAlso (i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE And
                                                                  i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)).FirstOrDefault()
            If Not item Is Nothing Then item.IsDeleted = True
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Claim Authorization Item"
    Public ReadOnly Property ClaimAuthorizationItemChildren() As ClaimAuthorizationItemList
        Get
            Return New ClaimAuthorizationItemList(Me)
        End Get
    End Property

    Public Function GetAuthorizationItemChild(ByVal childId As Guid) As ClaimAuthItem
        Return CType(Me.ClaimAuthorizationItemChildren.GetChild(childId), ClaimAuthItem)
    End Function

    Public Function GetNewAuthorizationItemChild() As ClaimAuthItem
        Dim newAuthorizationItem As ClaimAuthItem = CType(Me.ClaimAuthorizationItemChildren.GetNewChild, ClaimAuthItem)
        newAuthorizationItem.ClaimAuthorizationId = Me.Id
        newAuthorizationItem.LineItemNumber = newAuthorizationItem.GetNewLineItemNumber()
        Return newAuthorizationItem
    End Function

#End Region

#Region "Claim Authorization History Items"
    Public ReadOnly Property ClaimAuthorizationHistoryChildren() As ClaimAuthorizationHistoryList
        Get
            Return New ClaimAuthorizationHistoryList(Me)
        End Get
    End Property
#End Region

#Region "Lazy Initialize Fields"
    Private _claim As MultiAuthClaim = Nothing
    Private _serviceCenter As ServiceCenter = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Claim As MultiAuthClaim
        Get
            If (_claim Is Nothing) Then
                If Not Me.ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(Me.ClaimId, Me.Dataset)
                End If
            End If
            Return _claim
        End Get
        Private Set(ByVal value As MultiAuthClaim)
            _claim = value
        End Set
    End Property

    Public Property ServiceCenter As ServiceCenter
        Get
            If (_serviceCenter Is Nothing) Then
                If Not Me.ServiceCenterId.Equals(Guid.Empty) Then
                    Me.ServiceCenter = New ServiceCenter(Me.ServiceCenterId, Me.Dataset)
                End If
            End If
            Return _serviceCenter
        End Get
        Private Set(ByVal value As ServiceCenter)
            _serviceCenter = value
        End Set
    End Property
#End Region

#Region "Derived Properties"
    Public ReadOnly Property IsAddedToInvoiceGroup As Boolean
        Get
            Return (New ClaimAuthorizationDAL()).IsAddedToInvoiceGroup(Me.ClaimAuthorizationId)
        End Get
    End Property
#End Region

#Region "Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidVisitDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_VISIT_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)
            If obj.VisitDate Is Nothing Then Return True
            Dim visitDate As Date = obj.GetShortDate(obj.VisitDate.Value)
            Dim createdDate As Date = Today
            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' Visit Date:
            ' Must Not be GT today. 
            ' Must be GT or EQ to Date Of Loss. 
            ' Must be LT or EQ to Repair Date.
            ' Must be LT or EQ to Pick-Up Date if not NULL. 

            If visitDate > Today Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR1 ' "Visit Date Must Be Less Than Or Equal To Today."
                Return False
            End If
            If Not obj.Claim.LossDate Is Nothing AndAlso visitDate < obj.GetShortDate(obj.Claim.LossDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR4 ' "Visit Date Must Be Greater Than Or Equal To Date Of Loss."
                Return False
            End If
            If Not obj.RepairDate Is Nothing AndAlso visitDate > obj.GetShortDate(obj.RepairDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR2 ' "Visit Date Must Be Less Than Or Equal To Repair Date."
                Return False
            End If
            If Not obj.PickUpDate Is Nothing AndAlso visitDate > obj.GetShortDate(obj.PickUpDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR3 ' "Visit Date Must Be Less Than Or Equal To Pick-Up Date."
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidRepairDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)


            If obj.RepairDate Is Nothing Then Return True

            'if backend claim then both dates will be not null...then skip validation
            '' ''DEF-3873
            '' ''If Not obj.RepairDate Is Nothing And Not obj.PickUpDate Is Nothing Then
            '' ''    Return True
            '' ''End If

            Dim repairDate As Date = obj.GetShortDate(obj.RepairDate.Value)


            Dim createdDate As Date = Today

            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 757668: For a claim that is added from a claim interface:
            ' Repair Date is EQ to or LT the current date and EQ to or GT the Date of Loss
            ' A claim is originated from an interface when the Source field in the Claim record is not null
            If Not obj.Source Is Nothing Then
                If ((repairDate >= obj.GetShortDate(obj.Claim.LossDate.Value)) AndAlso
                (repairDate <= obj.GetShortDate(Today))) Then
                    Return True
                End If
            Else
                If ((repairDate >= obj.GetShortDate(createdDate)) AndAlso
                (repairDate <= obj.GetShortDate(Today))) Then
                    Return True
                End If
            End If
            If Not obj.LoanerReturnedDate Is Nothing AndAlso repairDate <= obj.GetShortDate(obj.LoanerReturnedDate.Value) Then
                Return True
            End If

            Return False

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidPickUpDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)
            If obj.PickUpDate Is Nothing Then Return True
            Dim pickUpDate As Date = obj.GetShortDate(obj.PickUpDate.Value)
            Dim createdDate As Date = Today
            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' PickUp Date:
            ' Must be LT or EQ today. 
            ' Must be GT or EQ to Repair Date. 
            ' Must be GT or EQ to Visit Date if not NULL. 
            ' Must be GT or EQ to Loaner Returned Date if not NULL. 

            'If obj.LoanerTaken Then Return True

            If pickUpDate > Today Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR1 '"Pick-Up Date Must Be Less Than Or Equal To Today."
                Return False
                'End If
            ElseIf Not obj.RepairDate Is Nothing Then
                If pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                    Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2  '"Pick-Up Date Must Be Greater Than Or Equal To Repair Date."
                    Return False
                End If
                'ElseIf pickUpDate > Today AndAlso pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                '    Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR6 '"Pick-Up Date Must Be Between Repair Date and Today."
                '    Return False
            Else
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5  '"Pick-Up Date Requires The Entry Of A Repair Date."
                Return False
            End If

            If Not obj.LoanerReturnedDate Is Nothing AndAlso pickUpDate < obj.GetShortDate(obj.LoanerReturnedDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR4 '"Pick-Up Date Must Be Greater Than Or Equal To Loaner Returned Date."
                Return False
            End If
            If Not obj.VisitDate Is Nothing AndAlso pickUpDate < obj.GetShortDate(obj.VisitDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR3 '"Pick-Up Date Must Be Greater Than Or Equal To Visit Date."
                Return False
            End If

            Return True

        End Function
    End Class

    Public NotInheritable Class ValidateDuplicateServiceClassType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_SERVICE_CLASS_TYPE_IN_CLAIM_AUTHORIZATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)
            For Each oClaimAuthItem As ClaimAuthItem In obj.ClaimAuthorizationItemChildren.Where(Function(item) item.IsDeleted = False AndAlso item.AdjustmentReasonId.Equals(Guid.Empty))
                If (obj.ClaimAuthorizationItemChildren.Where(Function(item) _
                                                      oClaimAuthItem.ServiceClassId.Equals(item.ServiceClassId) AndAlso
                                                      oClaimAuthItem.ServiceTypeId.Equals(item.ServiceTypeId) AndAlso
                                                      item.AdjustmentReasonId.Equals(Guid.Empty) AndAlso
                                                      item.IsDeleted = False AndAlso
                                                      oClaimAuthItem.AdjLineItemNumber = item.AdjLineItemNumber AndAlso
                                                      oClaimAuthItem.VendorSku = item.VendorSku AndAlso
                                                      Not oClaimAuthItem.Id.Equals(item.Id)).Count() > 0) Then
                    Return False
                End If
            Next
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidatePayDeductibleLineItem
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_AUTH_SHOULD_HAVE_PAY_DEDUCTIBLE_LINE_ITEM)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)
            If obj.ClaimAuthStatus <> ClaimAuthorizationStatus.Void Then
                obj.ContainsDeductible = obj.ContainsDeductibleLineItem
                If obj.ContainsDeductible AndAlso obj.Claim.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y) Then
                    Dim hasDeductibleLineItem As Boolean = obj.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False AndAlso
                                                                                                       (i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE And
                                                                                                        i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)).Count > 0
                    If Not hasDeductibleLineItem Then Return False
                End If
            End If
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidateMultiplePayDeductibleLineItem
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_AUTH_CANNOT_HAVE__MULTIPLE_PAY_DEDUCTIBLE_LINE_ITEM)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)

            If obj.ClaimAuthStatus <> ClaimAuthorizationStatus.Void Then
                obj.ContainsDeductible = obj.ContainsDeductibleLineItem
                If obj.ContainsDeductible AndAlso obj.Claim.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y) Then
                    Dim flag As Boolean = obj.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False AndAlso
                                                                                                       (i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE And
                                                                                                        i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE) _
                                                                                                        AndAlso i.AdjustmentReasonId.Equals(Guid.Empty)).Count > 1
                    If flag Then Return False
                End If
            End If
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidatePayDeductibleLineItemForContainDedutibleNo
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_AUTH_CANNOT_HAVE_PAY_DEDUCTIBLE_LINE_ITEM)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimAuthorization = CType(objectToValidate, ClaimAuthorization)
            If obj.ClaimAuthStatus <> ClaimAuthorizationStatus.Void Then
                obj.ContainsDeductible = obj.ContainsDeductibleLineItem
                If Not obj.ContainsDeductible Then
                    Dim hasDeductibleLineItem As Boolean = obj.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False AndAlso
                                                                                                       (i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE And
                                                                                                        i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)).Count > 0
                    If hasDeductibleLineItem Then Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region

#Region "Claim Authorization - Price List Details"
    Public Function GetPriceListDetails(ByVal serviceClassId As Guid, ByVal serviceTypeId As Guid, ByVal riskTypeId As Guid, ByVal equipmentClassId As Guid, ByVal equipmentId As Guid,
        ByVal conditionId As Guid, ByVal sku As String, ByVal skuDescription As String) As DataSet
        Try
            Dim dal As New ClaimAuthorizationDAL
            Return dal.LoadPriceListDetails(Me.ServiceCenterId, Me.Claim.LossDate, ElitaPlusIdentity.Current.ActiveUser.LanguageId, serviceClassId, serviceTypeId, riskTypeId,
                equipmentClassId, equipmentId, conditionId, sku, skuDescription)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Claim Authorization -Cancel Shipment"
    Public Function CancelShipmentRequest(ByVal claimAutorizationId As Guid) As Boolean
        Try
            Dim dal As New ClaimAuthorizationDAL
            Return dal.CancelShipmentRequest(claimAutorizationId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Claim Authorization -Reshipment"
    Public Function ReShipmentProcessRequest(ByVal claimAutorizationId As Guid, ByVal cancelStatusReason As String) As Boolean
        Try
            Dim dal As New ClaimAuthorizationDAL
            Return dal.ReShipmentProcessRequest(claimAutorizationId, cancelStatusReason)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function CheckLinkedAuthItem(ByVal claimAutorizationId As Guid) As Boolean
        Try
            Dim dal As New ClaimAuthorizationDAL
            Return dal.CheckLinkedAuthItem(claimAutorizationId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Claim Authorization -Reshipment"

    Public Function RefundFee(ByVal claimAutorizationId As Guid, ByVal refundReasonId As Guid, byval claimAuthItemId As Guid, byref errCode as integer, byref errMsg as string) As boolean
        Try
            Dim dal As New ClaimAuthorizationDAL
            return dal.RefundFee(claimAutorizationId,refundReasonId, claimAuthItemId,errCode ,errMsg )
            'If dal.RefundFee(claimAutorizationId,refundReasonId, claimAuthItemId,errCode ,errMsg )
            '    Dim claimAuth As ClaimAuthorization = new ClaimAuthorization(claimAutorizationId)
            '    Me.ClaimAuthStatus = ClaimAuthorizationStatus.Reversed
            '    claimAuth.Save()  
            '    Return True
            'Else
            '    Return False

            'End if  
           
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    #End Region

 #region "Claim Authorization Reversed"

    Public Sub RefundAmount()
        If Me.Reversed = True then
            If (Me.ClaimAuthorizationItemChildren.Count > 1) Then
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "CLAIM_AUTH_ITEM_COUNT_CANNOT_BE_MORE_THAN_ONE")
            End If
            dim newAuthItemId As Guid =  ReverseClaimAuthFromExiting(Me)             
            Me.ClaimAuthStatus = ClaimAuthorizationStatus.Reversed            
            dim errCode, errMsg As String
            If Not RefundFee(Me.Id,Me.RevAdjustmentReasonId, newAuthItemId, errCode, errMsg) then                
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, errMsg)
            End If  
        End if
    
    End Sub

#End Region


    Public Function ManualCashpayRequest(ByVal claimAutorizationId As Guid, ByVal bankInfoId As Guid, byref errCode as integer, byref errMsg as string) As Boolean
        Try
            Dim dal As New ClaimAuthorizationDAL
            Return dal.ManualCashpayRequest(claimAutorizationId, bankInfoId, errCode, errMsg)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function




#Region "SaveClaimReplaceOptions"
    Public Function SaveClaimReplaceOptions(claimId As Guid, eqipId As Guid,
                                              priority As String, vendorSKU As String, reserveInventory As String,
                                              inventoryId As Guid, createdBy As String, claimAuthorizationId As Guid)
        Try
            Dim dal As New ClaimAuthorizationDAL, oErrCode As Integer = 0
            dal.SaveClaimReplaceOptions(claimId, eqipId, priority, vendorSKU, reserveInventory, inventoryId, createdBy, claimAuthorizationId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class

Public Class ClaimAuthorizationList
    Inherits BusinessObjectListEnumerableBase(Of MultiAuthClaim, ClaimAuthorization)

    Public Sub New(ByVal parent As MultiAuthClaim)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ClaimAuthorization).ClaimId.Equals(CType(Parent, MultiAuthClaim).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As MultiAuthClaim) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ClaimAuthorizationList)) Then
                Dim dal As New ClaimAuthorizationDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ClaimAuthorizationList))
            End If
            Return parent.Dataset.Tables(ClaimAuthorizationDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class

#Region "Enums"

Public Enum ClaimAuthorizationStatus
    None
    Void
    Paid
    Authorized
    Pending
    Fulfilled
    Reconsiled
    ToBePaid
    Sent
    OnHold
    Cancelled
    Collected
    Reversed
End Enum

#End Region



