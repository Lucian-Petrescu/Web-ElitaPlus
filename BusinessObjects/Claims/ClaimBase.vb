Imports System.Linq
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Net
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.DataAccess
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService

''' <summary>
''' Claim Base class acts as base class for Creating the Claim Objects. The class encapsulates common properties from SingleAuthorization Claim and 
''' Multiple Authorization Claim.
''' </summary>
''' <remarks></remarks>

Public MustInherit Class ClaimBase
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
    Public Const COMPLIANCE_RULE_CODE As String = "CMPLARGRL"
    Public Const COMPLIANCE_ISSUE_CODE As String = "CMPLARG"
    Public Const m_NewClaimRepairDedPercent As Integer = 30
    Public Const m_NewClaimOrigReplDedPercent As Integer = 50
    Private Const CLAIM_DOC_UPLD_DETAILS As String = "Claim Document Upload Details"

#End Region

#Region "Constructors"

    Shared Sub New()

    End Sub

    Protected Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    Protected Sub New(ByVal isDSCreator As Boolean)
        MyBase.New(isDSCreator)
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'New BO attaching to a BO family
    Protected Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    'Existing BO
    Protected Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.OriginalFollowUpDate = Me.GetShortDate(Me.FollowupDate.Value)
    End Sub

    'Existing BO attaching to a BO family
    Friend Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id, blnMustReload)
        If (Not Me.FollowupDate Is Nothing) Then
            Me.OriginalFollowUpDate = Me.GetShortDate(Me.FollowupDate.Value)
        End If

    End Sub

    Protected Sub New(ByVal claimNumber As String, ByVal compId As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(claimNumber, compId)
        Me.OriginalFollowUpDate = Me.GetShortDate(Me.FollowupDate.Value)
    End Sub

    Protected Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Overridable Sub Load()
        Try
            Dim dal As New ClaimDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
            Me.ReportedDate = Date.Today
            Me.AuthorizedAmount = New Decimal(0)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Overridable Sub Load(ByVal id As Guid, Optional ByVal blnMustReload As Boolean = False)
        Try
            Dim dal As New ClaimDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing

            If blnMustReload AndAlso Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
            End If

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

    Protected Overridable Sub Load(ByVal claimNumber As String, ByVal compId As Guid)
        Try
            Dim dal As New ClaimDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(claimNumber, dal.COL_NAME_CLAIM_NUMBER, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, claimNumber, compId)
                Me.Row = Me.FindRow(claimNumber, dal.COL_NAME_CLAIM_NUMBER, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Private Methods"
    Private Sub Initialize()
    End Sub
#End Region

#Region "Private Variables"
    Private moIsComingFromDenyClaim As Boolean = False
    Private _IsLossDateCheckforCancelledCert As Boolean = False
    Private _policeReport As PoliceReport = Nothing
    Private _outstandingAmount As Decimal = 0
    Private _isUpdatedComment As Boolean = False
    Private _AuthDetailDataHasChanged As Boolean
    Private objCurrentShippingInfo As ShippingInfo
    Private _originalFollowUpDate As Date
    Protected _claimStatusBO As ClaimStatus
    Private _DealerTypeCode As String
    Protected _IssueDeniedReason As String = Nothing
#End Region

#Region "Protected Variables"
    Protected moIsUpdatedMasterClaimComment As Boolean = False
#End Region

#Region "Properties"
    'Key Property
    <ValidNonManufacturingCoverageType("")>
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ClaimDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CertItemCoverageId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            If (Not Me.CertItemCoverageId.Equals(Value)) Then
                Me.SetValue(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID, Value)
                Me.CertificateItemCoverage = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property RiskType() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_RISK_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_RISK_TYPE), String)
            End If
        End Get

    End Property

    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    Public Overridable Property ClaimActivityId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID, Value)
        End Set
    End Property

    Public Overridable Property ReasonClosedId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REASON_CLOSED_ID, Value)
        End Set
    End Property

    Public Overridable Property RepairCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPAIR_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_REPAIR_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPAIR_CODE_ID, Value)
        End Set
    End Property

    Public Overridable Property CauseOfLossId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property

    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property

    <ValidLawsuitId("")>
    Public Property IsLawsuitId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_LAWSUIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_IS_LAWSUIT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_IS_LAWSUIT_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    Public Property LoanerCenterId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID), Byte()))
            End If
        End Get
        Private Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOANER_CENTER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=500)>
    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1), ValidReasonClosed(""),
     Obsolete("Backward Compatability - Replace this with ClaimBase.Status - Action - Change to Private")>
    Public Property StatusCode() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property ContactName() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CONTACT_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property CallerName() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CALLER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CALLER_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=500)>
    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property
    Public Property DeniedReasons() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DENIED_REASONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DENIED_REASONS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DENIED_REASONS, Value)
        End Set
    End Property

    Public Function EvaluatePreConditions(preConditionList As List(Of String)) As Boolean

        For Each preCondition As String In preConditionList

            Select Case preCondition.ToUpperInvariant()
                Case "DemandsClaimsImage".ToUpperInvariant()
                    If (Me.ClaimImagesList.Count = 0) Then
                        Return False
                    End If
                Case Else
                    If (preCondition.ToUpper.StartsWith("DemandsPermission_".ToUpper())) Then
                        Dim permissionCode As String = preCondition.Substring(18)
                        If (ElitaPlusIdentity.Current.ActiveUser.UserPermission.Where(Function(up) up.PermissionId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_USER_ROLE_PERMISSION, ElitaPlusIdentity.Current.ActiveUser.LanguageId), permissionCode)).Count() = 0) Then
                            Return False
                        End If
                    Else
                        Throw New NotSupportedException()
                    End If

            End Select

        Next
        Return True
    End Function

    <ValidStringLength("", Max:=50)>
    Public Property TrackingNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    Public Property EmployeeNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER, Value)
        End Set
    End Property

    Public Property DeviceActivationDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE, Value)
        End Set
    End Property

    Public Property FulfilmentMethod() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD, Value)
        End Set
    End Property
    Public Property FulfillmentProviderType() As FulfillmentProviderType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP) Is DBNull.Value Then
                Return Nothing
            Else
                Dim result As FulfillmentProviderType
                If [Enum].TryParse(Of FulfillmentProviderType)(Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP).ToString().GetXcdEnum(Of FulfillmentProviderType)(), result) Then
                    Return result
                Else
                    Return FulfillmentProviderType.V1
                End If
            End If
        End Get
        Set(ByVal Value As FulfillmentProviderType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP, Value)
        End Set
    End Property
    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property


    Public Property LiabilityLimit() As DecimalType
        Get
            CheckDeleted()
            Dim lLimit As Decimal = 0D

            If Not Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT) Is DBNull.Value Then
                lLimit = CType(Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT), Decimal)
            End If

            If (Me.StatusCode.ToString <> Codes.CLAIM_STATUS__CLOSED And lLimit = 0 And
                (CDec(Me.Certificate.ProductLiabilityLimit.ToString) > 0D Or CDec(Me.CertificateItemCoverage.CoverageLiabilityLimit.ToString) > 0D)) Then
                Dim al As ArrayList = Me.CalculateLiabilityLimit(Me.CertificateId, Me.Contract.Id, Me.CertItemCoverageId, Me.LossDate)
                lLimit = CType(al(0), Decimal)

                Return lLimit
            Else

                If Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT) Is DBNull.Value Then
                    Return New DecimalType(lLimit)
                    'Return Nothing
                Else
                    Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT), Decimal))
                End If
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LIABILITY_LIMIT, Value)
        End Set
    End Property

    Public Property Deductible() As DecimalType
        Get
            CheckDeleted()
            Dim deduct As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return New DecimalType(deduct)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    Public Overridable Property ClaimClosedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Overridable Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property ContactSalutationID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_CONTACT_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_CONTACT_SALUTATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_CONTACT_SALUTATION_ID, Value)
        End Set
    End Property

    Public Property CallerSalutationID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_CALLER_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_CALLER_SALUTATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_CALLER_SALUTATION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20), ValueMandatoryConditionally(""), CPF_TaxNumberValidation("")>
    Public Property CallerTaxNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER, Value)
        End Set
    End Property

    Public Property DeductiblePercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT, Value)
        End Set
    End Property

    Public Property DeductiblePercentID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property PolicyNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_POLICY_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_POLICY_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_POLICY_NUMBER, Value)
        End Set
    End Property

    Public Property Fraudulent() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FRAUDULENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_FRAUDULENT), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_FRAUDULENT, Value)
        End Set
    End Property

    Public Property DealerReference() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEALER_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DEALER_REFERENCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEALER_REFERENCE, Value)
        End Set
    End Property

    Public Property Pos() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_POS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_POS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_POS, Value)
        End Set
    End Property

    Public Property DeniedReasonId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DENIED_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DENIED_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DENIED_REASON_ID, Value)
        End Set
    End Property

    Public Property Complaint() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_COMPLAINT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_COMPLAINT), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_COMPLAINT, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ClaimAuthorizationTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID), Byte()))
            End If
        End Get
        Friend Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property MethodOfRepairCode() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte())))
            End If
        End Get

    End Property

    <ValidStringLength("", Max:=20)>
    Public Property MasterClaimNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidDateOfLoss("")>
    Public Property LossDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_LOSS_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOSS_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property DefaultFollowUpDays() As LongType
        Get
            Return Me.Company.DefaultFollowupDays
        End Get

    End Property

    Private ReadOnly Property MfgDeductible(ByVal ds As DataSet) As DecimalType
        Get
            Dim dr As DataRow
            If ds.Tables(ClaimDAL.TABLE_NAME_MFG_DEDUCT).Rows.Count > 0 Then
                dr = ds.Tables(ClaimDAL.TABLE_NAME_MFG_DEDUCT).Rows(0)
            End If
            CheckDeleted()
            Dim deduct As Decimal = 0D
            If Not dr Is Nothing AndAlso dr(ClaimDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return New DecimalType(deduct)
            Else
                If Not dr Is Nothing Then
                    Return New DecimalType(CType(dr(ClaimDAL.COL_NAME_DEDUCTIBLE), Decimal))
                Else
                    Return Nothing
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property DeductibleByMfgFlag() As Boolean
        Get
            Try
                If Me.Contract.DeductibleByManufacturerId.Equals(Guid.Empty) Then
                    Return False
                Else
                    Dim code As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Contract.DeductibleByManufacturerId)
                    If code.Equals(Codes.YESNO_Y) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Get
    End Property

    Public Property DedCollectionMethodID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=6)>
    Public Property DedCollectionCCAuthCode() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE, Value)
        End Set
    End Property

    Public Property IsComingFromDenyClaim() As Boolean
        Get
            Return moIsComingFromDenyClaim
        End Get
        Set(ByVal Value As Boolean)
            moIsComingFromDenyClaim = Value
        End Set
    End Property

    Public Overridable Property IsRequiredCheckLossDateForCancelledCert() As Boolean
        Get
            Return _IsLossDateCheckforCancelledCert
        End Get
        Set(ByVal Value As Boolean)
            _IsLossDateCheckforCancelledCert = Value
        End Set
    End Property

    Public Property DeductibleCollected() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED, Value)
        End Set
    End Property

    Public Property AuthorizedAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal))
            End If
        End Get
        Protected Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, Value)
        End Set
    End Property

    Public Property PoliceReport() As PoliceReport
        Get
            Return _policeReport
        End Get
        Set(ByVal value As PoliceReport)
            _policeReport = value
        End Set
    End Property

    <ValidReportedDate("")>
    Public Property ReportedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPORTED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_REPORTED_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPORTED_DATE, Value)
        End Set
    End Property

    <ValidFollowupDate(""), ValueMandatory("")>
    Public Property FollowupDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FOLLOWUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_FOLLOWUP_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_FOLLOWUP_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ClaimActivityCode() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_ACTIVITIES, New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID), Byte())))
            End If
        End Get

    End Property

    Public ReadOnly Property ClaimActivityDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView
                dv = LookupListNew.GetClaimActivityLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID), Byte())))
            End If
        End Get

    End Property

    Public ReadOnly Property CauseOfLoss() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView
                dv = LookupListNew.GetCauseOfLossLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte())))
            End If
        End Get
    End Property

    Public ReadOnly Property MethodOfRepairDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView
                dv = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte())))
            End If
        End Get

    End Property

    <ValidStringLength("", Max:=20), ValidNewDeviceSKU("")>
    Public Property NewDeviceSku() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_NEW_DEVICE_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_NEW_DEVICE_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_NEW_DEVICE_SKU, Value)
        End Set
    End Property

    Public Property SalvageAmount() As DecimalType
        Get
            CheckDeleted()
            Dim salvage As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_SALVAGE_AMOUNT) Is DBNull.Value Then
                Return New DecimalType(salvage)
                'Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_SALVAGE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SALVAGE_AMOUNT, Value)
        End Set
    End Property

    Public Property DiscountAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT, Value)
        End Set
    End Property

    Public Property DiscountPercent() As LongType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DISCOUNT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimDAL.COL_NAME_DISCOUNT_PERCENT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DISCOUNT_PERCENT, Value)
        End Set
    End Property

    Public ReadOnly Property OutstandingPremiumAmount As DecimalType
        Get
            _outstandingAmount = CalculateOutstandingPremiumAmount()
        End Get
    End Property

    Public ReadOnly Property AuthorizationLimit() As DecimalType
        Get
            Return (ElitaPlusIdentity.Current.ActiveUser.AuthorizationLimit(Me.CompanyId))
        End Get

    End Property

    Public ReadOnly Property IsAuthorizationLimitExceeded() As Boolean
        Get
            If Not Me.AuthorizedAmount Is Nothing AndAlso Me.AuthorizedAmount.Value > Me.AuthorizationLimit.Value Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property IsCoverageForTheft As Boolean
        Get
            Dim flag As Boolean = False
            If Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                  OrElse Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
                  OrElse Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper Then

                flag = True
            End If
            Return flag
        End Get
    End Property

    Public Property IsUpdatedComment() As Boolean
        Get
            Return _isUpdatedComment
        End Get
        Set(ByVal Value As Boolean)
            _isUpdatedComment = Value
        End Set
    End Property

    Public Property IsLocked() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_LOCKED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_IS_LOCKED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_IS_LOCKED, Value)
        End Set
    End Property

    Public Property lockedOn() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOCKED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LOCKED_ON), DateType)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOCKED_ON, Value)
        End Set
    End Property

    Public Property LockedBy() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOCKED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_LOCKED_BY), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOCKED_BY, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ClaimsAdjuster() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER, Value)
        End Set
    End Property

    Public Property ClaimsAdjusterName() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME, Value)
        End Set
    End Property

    Public Property AuthDetailDataHasChanged() As Boolean
        Get
            Return _AuthDetailDataHasChanged
        End Get
        Set(ByVal Value As Boolean)
            _AuthDetailDataHasChanged = Value
        End Set
    End Property

    Public moGalaxyClaimNumberList As ArrayList = Nothing

    Public ReadOnly Property CurrentShippingInfo() As ShippingInfo
        Get
            Return objCurrentShippingInfo
        End Get
    End Property

    Public ReadOnly Property MaxFollowUpDays() As LongType
        Get
            Return (Me.Company.MaxFollowupDays)
        End Get
    End Property

    Public Property OriginalFollowUpDate() As Date
        Get
            Return _originalFollowUpDate
        End Get
        Set(ByVal Value As Date)
            _originalFollowUpDate = Value
        End Set
    End Property

    Public Property ContactInfoId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CONTACT_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CONTACT_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CONTACT_INFO_ID, Value)
        End Set
    End Property

    Public Property RepairCode() As String
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_REPAIR_CODE) Is DBNull.Value) OrElse (Me.RepairCodeId.Equals(Guid.Empty))) Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REPAIR_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPAIR_CODE, Value)
        End Set
    End Property

    Public Property RepairShortDesc() As String
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC) Is DBNull.Value) OrElse (Me.RepairCodeId.Equals(Guid.Empty))) Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC, Value)
        End Set

    End Property

    Public ReadOnly Property NotificationTypeId() As Guid
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID) Is DBNull.Value)) Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID), Byte()))
            End If
        End Get

    End Property

    Public ReadOnly Property NotificationTypeDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView = LookupListNew.GetNotificationTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID), Byte())))
            End If
        End Get
    End Property

    Public Property LastOperatorName() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME) Is DBNull.Value Then
                Return (Me.UserName)
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME, Value)
        End Set

    End Property

    Public ReadOnly Property UserName() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_USER_NAME), String)
            End If
        End Get

    End Property

    Public Property MobileNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MOBILE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MOBILE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_MOBILE_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property SerialNumber() As String
        Get
            If Me.ClaimedEquipment Is Nothing Then
                Return Nothing
            Else
                Return Me.ClaimedEquipment.SerialNumber
            End If
        End Get
    End Property

    Public ReadOnly Property ClaimStatusesCount() As Integer
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_STATUSES_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIM_STATUSES_COUNT), Integer)
            End If
        End Get

    End Property

    Public ReadOnly Property LatestClaimStatus() As ClaimStatus
        Get
            If Me.ClaimStatusesCount > 0 Then
                Return ClaimStatus.GetLatestClaimStatus(Me.Id)
            Else
                Return Nothing
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property MgrAuthAmountFlag() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()

            If ((Value = Nothing) OrElse (Value.ToUpper <> "Y")) Then
                'Added logic to default to "N"  if value is nothing value is different than "Y", "y"
                Me.SetValue(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG, "N")
            Else
                Me.SetValue(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG, Value)
            End If
        End Set
    End Property

    Public ReadOnly Property ReasonClosed() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView
                dv = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID), Byte())))
            End If
        End Get

    End Property

    Public ReadOnly Property ReasonClosedCode() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_REASONS_CLOSED, New Guid(CType(Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID), Byte())))
            End If
        End Get
    End Property

    Public ReadOnly Property AssurantPays As DecimalType
        Get

            Dim assurPays As Decimal = 0D
            Dim liabLimit As Decimal = Me.LiabilityLimit.Value

            If Not Me.DiscountPercent Is Nothing Then
                Me.DiscountAmount = AuthorizedAmount * (CType(Me.DiscountPercent, Decimal) / 100)
            End If

            If (liabLimit = 0D And CType(Me.Certificate.ProductLiabilityLimit.ToString, Decimal) = 0 And CType(Me.CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0) Then
                liabLimit = 999999999.99
            End If

            Trace("AA_DEBUG", "Claim Detail", "Claim Id =" & GuidControl.GuidToHexString(Me.Id) &
                          "@Claim = " & Me.ClaimNumber)

            If (Me.AuthorizedAmount > liabLimit) Then
                assurPays = liabLimit - IIf(Me.Deductible Is Nothing, New Decimal(0D), Me.Deductible) - IIf(Me.SalvageAmount, New Decimal(0D), Me.SalvageAmount)
            Else
                assurPays = Me.AuthorizedAmount.Value - IIf(Me.Deductible Is Nothing, New Decimal(0D), Me.Deductible) - IIf(Me.SalvageAmount Is Nothing, New Decimal(0D), Me.SalvageAmount)

            End If
            If Me.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                If (assurPays < 0D) Then
                    assurPays = 0D
                End If
            End If

            Try
                Return New DecimalType(Decimal.Add(assurPays, BonusAmount))
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Catch ex As OverflowException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Get
    End Property

    Public ReadOnly Property DueToSCFromAssurant As DecimalType
        Get
            Dim dueToSCFromA As Decimal = 0D
            Dim authValue As Decimal = Me.AuthorizedAmount.Value
            Dim assurPays As Decimal = Decimal.Subtract(Me.AssurantPays.Value, BonusAmount)
            Dim ded As Decimal = Me.Deductible.Value
            If (authValue - assurPays < ded) Then
                dueToSCFromA = authValue - assurPays
            Else
                dueToSCFromA = ded + assurPays
            End If
            Try
                Return New DecimalType(Decimal.Add(dueToSCFromA, BonusAmount))
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Catch ex As OverflowException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Get
    End Property

    Public ReadOnly Property ConsumerPays() As DecimalType
        Get
            Dim cPays As Decimal = 0D
            Dim aPays As Decimal = Me.AssurantPays.Value
            Dim sal As Decimal = Me.SalvageAmount.Value
            If (CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) > aPays) Then
                cPays = CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) - aPays - sal
            End If

            Return New DecimalType(cPays)
        End Get
    End Property

    Public ReadOnly Property AboveLiability() As DecimalType
        Get
            ' Dim liabLimit As Decimal = CType(Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT), Decimal)
            Dim liabLimit As Decimal = Me.LiabilityLimit.Value
            Dim abovLiability As Decimal = 0D


            If (liabLimit = 0D And CType(Me.Certificate.ProductLiabilityLimit.ToString, Decimal) = 0 And CType(Me.CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0) Then
                liabLimit = 999999999.99
            End If

            If (CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) > liabLimit) Then
                abovLiability = CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) - liabLimit
            End If
            Return New DecimalType(abovLiability)
        End Get

    End Property

    Public ReadOnly Property getSalutationDescription(ByVal salutationid As Guid) As String
        Get
            Dim dv As DataView = LookupListNew.GetSalutationLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return LookupListNew.GetDescriptionFromId(dv, salutationid)
        End Get

    End Property

    Private _NumberOfDisbursements As Nullable(Of Integer)
    Private _TotalPaid As DecimalType = New DecimalType(0)

    Private Sub PopulateDisbursementSummary()
        If Not _NumberOfDisbursements.HasValue AndAlso Not Me.IsNew Then
            Dim result As MultiValueReturn(Of DecimalType, Integer) = getTotalPaid(Id)
            _TotalPaid = result.Value1
            _NumberOfDisbursements = result.Value2
        End If
    End Sub

    Public ReadOnly Property NumberOfDisbursements As Integer
        Get
            PopulateDisbursementSummary()
            Return _NumberOfDisbursements.Value
        End Get
    End Property

    Public ReadOnly Property TotalPaid() As DecimalType
        Get
            PopulateDisbursementSummary()
            Return _TotalPaid
        End Get
    End Property

    Private _TotalPaidForCert As DecimalType = New DecimalType(0)
    Public ReadOnly Property TotalPaidForCert() As DecimalType
        Get
            If _TotalPaidForCert.Equals(New DecimalType(0)) Then
                _TotalPaidForCert = getTotalPaidForCert(Me.CertificateId)
                Return _TotalPaidForCert
            Else
                Return _TotalPaidForCert
            End If
        End Get
    End Property

    Protected Property PickUpDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PICKUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_PICKUP_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_PICKUP_DATE, Value)
        End Set
    End Property

    Protected Property VisitDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_VISIT_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_VISIT_DATE, Value)
        End Set
    End Property

    Protected Property RepairDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_REPAIR_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property DealerTypeCode() As String
        Get
            If Me._DealerTypeCode Is Nothing Then
                _DealerTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.Dealer.DealerTypeId)
            End If
            Return Me._DealerTypeCode
        End Get
    End Property

    Protected Property Bonus() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Protected Property BonusTax() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property

    Public ReadOnly Property IsClaimChild() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_CLAIM_CHILD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_IS_CLAIM_CHILD), String)
            End If
        End Get
    End Property

    'REQ-6230
    Public Property Purchase_Price() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property

    'REQ-6230
    Public Property IndixId() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_INDIX_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(ClaimDAL.COL_NAME_INDIX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_INDIX_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=1)>
    Public ReadOnly Property IsClaimReadOnly() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_CLAIM_READ_ONLY) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimDAL.COL_NAME_IS_CLAIM_READ_ONLY), String)
            End If
        End Get
    End Property

    Public Property RemAuthNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REM_AUTH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REM_AUTH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REM_AUTH_NUMBER, Value)
        End Set
    End Property
#End Region

#Region "MustOverride & overridable  Properties"
    Public MustOverride ReadOnly Property IsDaysLimitExceeded() As Boolean
    Public MustOverride ReadOnly Property IsMaxSvcWrtyClaimsReached() As Boolean
#End Region

#Region "Non-Persistent Properties"

#Region "Work Queue"
    Private _wqItem As WorkQueueItem
    Public Property CurrentWorkQueueItem() As WorkQueueItem
        Get
            Return _wqItem
        End Get
        Set(ByVal value As WorkQueueItem)
            _wqItem = value
        End Set
    End Property
#End Region

#End Region

#Region "Derived Properties"
    Public Property Status As BasicClaimStatus
        Get
            Select Case Me.StatusCode
                Case Codes.CLAIM_STATUS__PENDING
                    Return BasicClaimStatus.Pending
                Case Codes.CLAIM_STATUS__ACTIVE
                    Return BasicClaimStatus.Active
                Case Codes.CLAIM_STATUS__CLOSED
                    Return BasicClaimStatus.Closed
                Case Codes.CLAIM_STATUS__DENIED
                    Return BasicClaimStatus.Denied
                Case Else
                    Return BasicClaimStatus.None
            End Select
        End Get
        Set(ByVal value As BasicClaimStatus)
            Select Case value
                Case BasicClaimStatus.Pending
                    Me.StatusCode = Codes.CLAIM_STATUS__PENDING
                Case BasicClaimStatus.Active
                    Me.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                Case BasicClaimStatus.Closed
                    Me.StatusCode = Codes.CLAIM_STATUS__CLOSED
                Case BasicClaimStatus.Denied
                    Me.StatusCode = Codes.CLAIM_STATUS__DENIED
                Case Else
                    Throw New NotSupportedException()
            End Select
        End Set
    End Property

    Public ReadOnly Property ClaimAuthorizationTypeCode() As String
        Get
            If (Me.ClaimAuthorizationTypeId.Equals(Guid.Empty)) Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, Me.ClaimAuthorizationTypeId)
            End If
        End Get
    End Property

    Public Property ClaimAuthorizationType As ClaimAuthorizationType
        Get
            Select Case Me.ClaimAuthorizationTypeCode
                Case Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE
                    Return ClaimAuthorizationType.Single
                Case Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE
                    Return ClaimAuthorizationType.Multiple
                Case Else
                    Return BasicClaimStatus.None
            End Select
        End Get
        Private Set(ByVal value As ClaimAuthorizationType)
            Select Case value
                Case ClaimAuthorizationType.Single
                    Me.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE)
                Case ClaimAuthorizationType.Multiple
                    Me.ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE)
                Case Else
                    Throw New NotSupportedException()
            End Select
        End Set
    End Property

#Region "Claim Authorization"
    Public ReadOnly Property ClaimAuthorizationChildren() As ClaimAuthorizationList
        Get
            Return New ClaimAuthorizationList(Me)
        End Get
    End Property

#End Region

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.CertNumber - Action - Remove")>
    Public ReadOnly Property CertificateNumber() As String
        Get
            If (Me.Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Me.Certificate.CertNumber
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.CustomerName - Action - Remove")>
    Public ReadOnly Property CustomerName() As String
        Get
            If (Me.Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Me.Certificate.CustomerName
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.Id - Action - Remove")>
    Public Overridable ReadOnly Property CertificateId() As Guid
        Get
            If (Me.Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Me.Certificate.Id
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Dealer.Dealer - Action - Remove")>
    Public ReadOnly Property DealerCode() As String
        Get
            If (Me.Dealer Is Nothing) Then
                Return Nothing
            Else
                Return Me.Dealer.Dealer
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Dealer.DealerName - Action - Remove")>
    Public ReadOnly Property DealerName() As String
        Get
            If (Me.Dealer Is Nothing) Then
                Return Nothing
            Else
                Return Me.Dealer.DealerName
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.CertificateItemCoverage.CoverageTypeId - Action - Remove")>
    Public ReadOnly Property CoverageTypeId() As Guid
        Get
            Return Me.CertificateItemCoverage.CoverageTypeId
        End Get
    End Property

    Public ReadOnly Property CoverageTypeCode() As String
        Get
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, Me.CertificateItemCoverage.CoverageTypeId)
        End Get
    End Property

    Public ReadOnly Property CoverageTypeDescription() As String
        Get
            Return LookupListNew.GetDescriptionFromId(LookupListNew.LK_COVERAGE_TYPES, Me.CertificateItemCoverage.CoverageTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get
    End Property

    ''TODO: Remove the code
    <Obsolete("Backward Compatability - Replace this with ClaimBase.CreatedDate - Action - Remove", True)>
    Public ReadOnly Property CreationDate() As DateType
        Get
            Return MyBase.CreatedDate
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.ModifiedDate - Action - Remove")>
    Public ReadOnly Property LastModifiedDate() As DateType
        Get
            Return MyBase.ModifiedDate
        End Get

    End Property

    Private _deductibleType As CertItemCoverage.DeductibleType
    Public ReadOnly Property DeductibleType As CertItemCoverage.DeductibleType
        Get
            If (_deductibleType Is Nothing) Then
                _deductibleType = CertItemCoverage.GetDeductible(Me.CertItemCoverageId, Me.MethodOfRepairId)
            End If
            Return _deductibleType
        End Get
    End Property

    Public Property ShippingInfoId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SHIPPING_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_SHIPPING_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SHIPPING_INFO_ID, Value)
        End Set
    End Property

    Public ReadOnly Property BonusAmount() As DecimalType
        Get
            CheckDeleted()
            Return New Decimal.Add(Bonus, BonusTax)
        End Get
    End Property

    Public Property CurrentRetailPrice() As DecimalType
        Get
            CheckDeleted()
            Dim CurrentRetailPriceVal As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE) Is DBNull.Value Then
                Return New DecimalType(CurrentRetailPriceVal)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE, Value)
        End Set
    End Property

#End Region

#Region "Lazy Initialize Fields"
    Private _company As Company = Nothing
    Private _dealer As Dealer = Nothing
    Private _certificate As Certificate = Nothing
    Private _certItem As CertItem = Nothing
    Private _certRegisteredItem As CertRegisteredItem = Nothing
    Private _certificateItemCoverage As CertItemCoverage = Nothing
    Private _contract As Contract = Nothing
    Private _contactInfo As ContactInfo = Nothing
    Private _case As CaseBase = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Company As Company
        Get
            If (_company Is Nothing) Then
                If Not Me.Dealer Is Nothing Then
                    Me.Company = New Company(Me.Dealer.CompanyId, Me.Dataset)
                ElseIf Not Me.Certificate Is Nothing Then
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                    Me.Company = New Company(Me.Dealer.CompanyId, Me.Dataset)
                ElseIf Not Me.CertificateItem Is Nothing Then
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                    Me.Company = New Company(Me.Dealer.CompanyId, Me.Dataset)
                Else
                    Me.CertificateItem = New CertItem(Me.CertificateItemCoverage.CertItemId, Me.Dataset)
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                    Me.Company = New Company(Me.Dealer.CompanyId, Me.Dataset)
                End If
            End If
            Return _company
        End Get
        Private Set(ByVal value As Company)
            _company = value
        End Set
    End Property

    Public Property Dealer As Dealer
        Get
            If (_dealer Is Nothing) Then
                If Not Me.Certificate Is Nothing Then
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                ElseIf Not Me.CertificateItem Is Nothing Then
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                Else
                    Me.CertificateItem = New CertItem(Me.CertificateItemCoverage.CertItemId, Me.Dataset)
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                End If
            End If
            Return _dealer
        End Get
        Private Set(ByVal value As Dealer)
            _dealer = value
            Me.Company = Nothing
        End Set
    End Property

    Public Property Certificate As Certificate
        Get
            If (_certificate Is Nothing) Then
                If Not Me.CertificateItem Is Nothing Then
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                Else
                    Me.CertificateItem = New CertItem(Me.CertificateItemCoverage.CertItemId, Me.Dataset)
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                End If
            End If
            Return _certificate
        End Get
        Private Set(ByVal value As Certificate)
            _certificate = value
            Me.Dealer = Nothing
            Me.Company = Nothing
        End Set
    End Property

    Public Property CertificateItem As CertItem
        Get
            If (_certItem Is Nothing) Then
                If Not Me.CertificateItemCoverage Is Nothing Then
                    Me.CertificateItem = New CertItem(Me.CertificateItemCoverage.CertItemId, Me.Dataset)
                End If
            End If
            Return _certItem
        End Get
        Private Set(ByVal value As CertItem)
            _certItem = value
            Me.Certificate = Nothing
            Me.Dealer = Nothing
            Me.Company = Nothing
        End Set
    End Property

    Public Function GetCertRegisterItemId(ByVal ClaimID As Guid) As Guid
        Try
            Dim dal As New CertRegisteredItemDAL
            Return dal.GetCertRegisterItemId(ClaimID)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetCertRegisterItemIdByMasterNumber(ByVal ClaimNumber As String, ByVal CompanyId As Guid) As Guid
        Try
            Dim dal As New CertRegisteredItemDAL
            Return dal.GetCertRegisterItemIdByMasterClaimNo(ClaimNumber, CompanyId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Property CertificateItemCoverage As CertItemCoverage
        Get
            If (_certificateItemCoverage Is Nothing) Then
                If Not Me.CertItemCoverageId.Equals(Guid.Empty) Then
                    Me.CertificateItemCoverage = New CertItemCoverage(Me.CertItemCoverageId, Me.Dataset)
                End If
            End If
            Return _certificateItemCoverage
        End Get
        Private Set(ByVal value As CertItemCoverage)
            _certificateItemCoverage = value
            Me.CertificateItem = Nothing
            Me.Certificate = Nothing
            Me.Dealer = Nothing
            Me.Company = Nothing
        End Set
    End Property

    Public ReadOnly Property Contract As Contract
        Get
            If (_contract Is Nothing) Then
                _contract = New Contract(Contract.GetContractID(Me.CertificateId))
            End If
            Return _contract
        End Get
    End Property

    Public Property ContactInfo As ContactInfo
        Get
            If (_contactInfo Is Nothing AndAlso Not Me.ContactInfoId.Equals(Guid.Empty)) Then
                _contactInfo = New ContactInfo(Me.ContactInfoId, Me.Dataset)
            End If
            Return _contactInfo
        End Get
        Set(ByVal value As ContactInfo)
            _contactInfo = value
        End Set
    End Property

    Public ReadOnly Property CaseInfo As CaseBase
        Get
            If (_case Is Nothing) Then
                _case = New CaseBase(CaseBase.LoadCaseByClaimId(Me.Id))
            End If
            Return _case
        End Get
    End Property



#End Region

#Region "Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)

            If claimBaseObject.IsNew _
              AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, claimBaseObject.Company.CompanyTypeId) = Company.COMPANY_TYPE_INSURANCE _
              AndAlso claimBaseObject.CallerTaxNumber Is Nothing Then
                Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidLawsuitId
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), Codes.YESNO_Y)

            'Check if Dealer is configured with Make lawsuit Mandatory, If so make this field mandatory when creating new\editing claim
            If claimBaseObject.Dealer.IsLawsuitMandatoryId = yesId AndAlso claimBaseObject.IsLawsuitId.Equals(Guid.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidNonManufacturingCoverageType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_NOT_ALLOWED_FOR_MFG_COVERAGE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)

            If claimBaseObject.IsNew _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, claimBaseObject.CoverageTypeId) = Codes.COVERAGE_TYPE__MANUFACTURER Then
                Dim oClmSystem As New ClaimSystem(claimBaseObject.Dealer.ClaimSystemId)
                If oClmSystem.Code = "GW" Then
                    Return True 'allow claim under manufacturer warranty if the claim is created by GW
                Else
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CPF_TaxNumberValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CPF_DOCUMENT_NUMBER_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)
            Dim dal As New ClaimDAL
            Dim oErrMess As String

            Try
                If Not claimBaseObject.IsNew Then Return True

                If Not LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, claimBaseObject.Company.CompanyTypeId) = Company.COMPANY_TYPE_INSURANCE Then
                    Return True
                End If

                If claimBaseObject.CallerTaxNumber Is Nothing Then Return True ' the ValueMandatoryConditionally will catch this validation

                'DEF-1012
                If claimBaseObject.Certificate.DocumentTypeID.Equals(Guid.Empty) Then
                    MyBase.Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                    Return False
                End If
                Dim docTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, claimBaseObject.Certificate.DocumentTypeID)

                oErrMess = dal.ExecuteSP(docTypeCode, claimBaseObject.CallerTaxNumber)
                If Not oErrMess Is Nothing Then
                    MyBase.Message = UCase(oErrMess)
                    Return False
                End If

                Return True

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidReasonClosed
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REASON_CLOSED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            If (obj.StatusCode = Codes.CLAIM_STATUS__CLOSED) Then
                If obj.ReasonClosedId.Equals(Guid.Empty) Then Return False
            End If
            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidDateOfLoss
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim objClaim As ClaimBase = CType(objectToValidate, ClaimBase)

            If objClaim.LossDate Is Nothing Then Return True

            'if the claim is closed, then no need to do validations for loss date
            If objClaim.Status = BasicClaimStatus.Closed Then Return True

            'if modifying existing denied claim, then no need to do validation since this date is not editable on the screen
            If Not objClaim.IsComingFromDenyClaim And objClaim.Status = BasicClaimStatus.Denied Then Return True

            'if backend claim then both dates will be not null...these dates must be >=date of loss
            If GetType(Claim).Equals(objClaim.GetType()) Then
                Dim objC = CType(objClaim, Claim)
                If Not objC.RepairDate Is Nothing And Not objC.PickUpDate Is Nothing Then
                    If (objClaim.LossDate.Value > objC.RepairDate.Value) OrElse (objClaim.LossDate.Value > objC.PickUpDate.Value) Then
                        Return False
                    End If
                End If
            End If

            If objClaim.IsComingFromDenyClaim Then
                'this means u r in new claim scrn and came with the flow of creating a "denied claim" before the cov-eff_date, 
                'so should not allow the user to enter a loss date which is now inside the coverage duration
                'because this claim should be prevented from reopning in claim detail
                If ((objClaim.LossDate.Value > Date.Now) OrElse
                    (objClaim.LossDate.Value >= objClaim.CertificateItemCoverage.BeginDate.Value)) Then
                    Return False
                End If
            Else
                If ((objClaim.LossDate.Value > Date.Now) OrElse
                    (((objClaim.LossDate.Value < objClaim.CertificateItemCoverage.BeginDate.Value) OrElse
                    (objClaim.LossDate.Value > objClaim.CertificateItemCoverage.EndDate.Value)) And
                    Not LookupListNew.GetCodeFromId(LookupListNew.LK_DENIED_REASON, objClaim.DeniedReasonId) = Codes.REASON_DENIED__INCORRECT_DEVICE_SELECTED)) Then
                    'User Story 193355 Added specific reason to allow denying expired items
                    Return False
                End If
            End If

            If objClaim.Certificate.StatusCode = Codes.CERT_STATUS_CANCELED Then
                If objClaim.IsRequiredCheckLossDateForCancelledCert Then  'Check loss date for new claim
                    Dim CancelId As Guid = objClaim.Certificate.getCertCancelID
                    Dim certCancel As New CertCancellation(CancelId)
                    If (objClaim.LossDate.Value > certCancel.CancellationDate.Value) Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidReportedDate
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPORT_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            Dim reportDate As Date
            If Not obj.LossDate Is Nothing Then
                Dim lossDate As Date = obj.GetShortDate(obj.LossDate.Value)
                If obj.ReportedDate Is Nothing Then
                    reportDate = obj.GetShortDate(Today)
                    obj.ReportedDate = obj.GetShortDate(Today)
                Else
                    reportDate = obj.GetShortDate(obj.ReportedDate.Value)
                    obj.ReportedDate = obj.GetShortDate(obj.ReportedDate.Value)
                End If
                If reportDate >= lossDate AndAlso reportDate <= Today Then
                    Return True
                Else
                    Me.Message = Common.ErrorCodes.INVALID_REPORT_DATE_ERR
                    Return False
                End If
            Else
                Return False
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidFollowupDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_FOLLOWUP_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            If ((obj.FollowupDate Is Nothing) OrElse
                (obj.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse
                (obj.StatusCode = Codes.CLAIM_STATUS__PENDING) OrElse
                ((obj.FollowupDate.Value >= obj.GetShortDate(Today)) AndAlso
                    (obj.FollowupDate.Value <= NonbusinessCalendar.GetNextBusinessDate(obj.MaxFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)) AndAlso
                    (NonbusinessCalendar.GetSameBusinessDaysCount(obj.FollowupDate.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) <= 0))) Then
                Return True
            End If

            If ((obj.GetShortDate(obj.FollowupDate.Value) = obj.OriginalFollowUpDate) AndAlso
                (obj.ReasonClosedId.Equals(Guid.Empty))) Then
                obj.CalculateFollowUpDate()
                Return True
            End If

            If (obj.FollowupDate.Value < obj.GetShortDate(Today)) Then
                Return False
            End If

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidNewDeviceSKU
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "SKU_INVALID_MISSING")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            If (Not obj.NewDeviceSku Is Nothing) AndAlso (obj.NewDeviceSku.Trim <> String.Empty) Then
                Dim deductible As DecimalType
                If Not ListPrice.IsSKUValid(obj.Dealer.Id, obj.NewDeviceSku.Trim, obj.LossDate.Value, deductible) Then
                    Return False
                End If
            End If
            Return True

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class FulfillmentaddressInfo
        Inherits FulfillmentAddress
        Public Property AddressId As Guid
    End Class

#End Region

#Region "Claim Equipment"

    Public ReadOnly Property ClaimEquipmentChildren() As ClaimEquipment.ClaimEquipmentList
        Get
            Return New ClaimEquipment.ClaimEquipmentList(Me)
        End Get
    End Property

    Public Function GetEquipmentChild(ByVal childId As Guid) As ClaimEquipment
        Return CType(Me.ClaimEquipmentChildren.GetChild(childId), ClaimEquipment)
    End Function

    Public Function GetNewEquipmentChild() As ClaimEquipment
        Dim newClaimEquipment As ClaimEquipment = CType(Me.ClaimEquipmentChildren.GetNewChild, ClaimEquipment)
        newClaimEquipment.ClaimId = Me.Id
        Return newClaimEquipment
    End Function
#End Region

#Region "Claim Issues"
    Public ReadOnly Property ClaimIssuesList() As ClaimIssue.ClaimIssueList
        Get
            Return New ClaimIssue.ClaimIssueList(Me)
        End Get
    End Property

    Public ReadOnly Property IssuesStatus() As String
        Get
            Return EvaluateIssues()
        End Get
    End Property

    Public ReadOnly Property HasIssues() As Boolean
        Get
            If (Me.ClaimIssuesList.Count > 0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property AllIssuesResolvedOrWaived() As Boolean
        Get
            If HasIssues Then
                Dim issueStatus As String = EvaluateIssues()
                If (issueStatus = Codes.CLAIMISSUE_STATUS__RESOLVED Or issueStatus = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                    Return True
                End If
                Return False
            End If
            Return False
        End Get
    End Property

    Private Function EvaluateIssues() As String
        Dim issuesStatus As String = Codes.CLAIMISSUE_STATUS__RESOLVED

        For Each Item As ClaimIssue In Me.ClaimIssuesList
            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                issuesStatus = Codes.CLAIMISSUE_STATUS__REJECTED
                _IssueDeniedReason = Item.Issue.DeniedReason
                Exit For
            End If
            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Or Item.StatusCode = Codes.CLAIMISSUE_STATUS__PENDING) Then
                issuesStatus = Codes.CLAIMISSUE_STATUS__OPEN
            End If
        Next

        Return issuesStatus
    End Function
    Public ReadOnly Property IssueDeniedReason() As String
        Get
            Return _IssueDeniedReason
        End Get

    End Property

    'Private Function IssueDeniedReason() As String
    '    Dim issuesDeniedReason As String = Nothing

    '    For Each Item As ClaimIssue In Me.ClaimIssuesList
    '        If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__REJECTED) Then
    '            issuesDeniedReason= item.Issue.DeniedReason                  
    '            Exit For
    '        End If            
    '    Next

    '    Return issuesDeniedReason
    'End Function

    Private _dealerIssues As DataView
    Public Property DealerIssues() As DataView
        Get
            If (_dealerIssues Is Nothing) Then
                _dealerIssues = LoadIssues()
            End If
            Return _dealerIssues
        End Get
        Set(ByVal value As DataView)
            _dealerIssues = value
        End Set
    End Property

    Public Function Load_Filtered_Issues() As DataView

        Dim claimIssue As ClaimIssue
        'Issues Already added cannot be added again
        For Each claimIssue In Me.ClaimIssuesList
            Dim condition As String = "CODE <> '" & claimIssue.IssueCode & "'"
            If DealerIssues.RowFilter Is Nothing OrElse DealerIssues.RowFilter.Trim.Length = 0 Then
                DealerIssues.RowFilter = condition
            Else
                'DealerIssues.RowFilter = "(" & DealerIssues.RowFilter & ") AND (" & condition & ")"
                If Not DealerIssues.RowFilter.Contains(condition) Then
                    DealerIssues.RowFilter = DealerIssues.RowFilter & " and " & condition
                End If
            End If
        Next

        Return DealerIssues

    End Function

    Private Function LoadIssues() As DataView
        Dim oDealer As New Dealer(Me.CompanyId, Me.DealerCode)
        Dim dvRules As DataView = Rule.GetRulesByDealerAndCompany(oDealer.Id, Me.CompanyId)
        Dim dealerIssues As DataTable = CreateIssuesDataTable()
        Dim oCompany As New Company(Me.CompanyId)

        If (Not Certificate.AddressId = Guid.Empty) Then
            Dim oAddress As New Address(Certificate.AddressId)
        End If

        Dim addIssue As Boolean = True
        Dim UFITaxID, FirstName, LastName As String
        Dim BeginDate, EndDate As DateTime
        Dim dal As New ClaimDAL

        For Each Row As DataRowView In dvRules
            Dim ruleId As New Guid(CType(Row("RULE_ID"), Byte()))
            Dim rule As New Rule(ruleId)
            Select Case rule.Code
                Case "CLMADJ"
                    If (Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper) Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "RQPRPT", "THFDOCRL"
                    If Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                   OrElse Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
                   OrElse (Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper _
                   AndAlso ((New Company(Me.CompanyId)).PoliceRptForLossCovId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)))) Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "TRBSHTRL"
                    If Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__ACCIDENTAL.ToUpper _
                    OrElse Me.CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__MECHANICAL_BREAKDOWN.ToUpper Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "CLMDOCRL", "DEDCOLRL"
                    addIssue = True
                Case "UPGRDRL"
                    If Me.LossDate.Value > Me.Certificate.WarrantySalesDate.Value.AddMonths(12) Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "CMPLARGRL" 'REQ-5978
                    addIssue = False
                    Dim dv As New DataView

                    dv = IsCustomerPresentInUFIList(oCompany.CountryId, Certificate.IdentificationNumber)
                    If dv.Count > 0 Then
                        addIssue = True
                    End If

                Case "LATAMHOLDCLM" 'REQ-6026

                    addIssue = IsCertPaymentExists(Certificate.Id)

            End Select

            If addIssue Then
                For Each ruleIssue As RuleIssue In rule.IssueRuleChildren
                    Dim issue As New Issue(ruleIssue.IssueId)
                    Dim newRow As DataRow = dealerIssues.NewRow
                    newRow("ISSUE_ID") = GuidControl.HexToByteArray(GuidControl.GuidToHexString(issue.Id))
                    newRow("CODE") = issue.Code
                    newRow("DESCRIPTION") = issue.Description
                    newRow("RULE_CODE") = rule.Code
                    dealerIssues.Rows.Add(newRow)

                Next
            End If

            addIssue = True
        Next

        Return New DataView(dealerIssues)
    End Function

    Private Function CreateIssuesDataTable() As DataTable

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("ISSUE_ID", GetType(Byte()))
        dt.Columns.Add("CODE", GetType(String))
        dt.Columns.Add("DESCRIPTION", GetType(String))
        dt.Columns.Add("RULE_CODE", GetType(String))
        Return dt

    End Function

    Public Sub AttachIssues()
        Dim issueList As DataView = Load_Filtered_Issues()
        '' 02/01/2016 - Need to figure out why only hardcoded set of rules can create Issues and not others?
        Dim condition As String = "CODE <> 'CMPLARG'" 'Remove Compliance issue when no replacement claim

        If Not MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            issueList.RowFilter = condition
        End If
        For Each row As DataRowView In issueList
            Select Case (CType(row("RULE_CODE"), String))
                Case "RQPRPT", "TRBSHTRL", "CLMDOCRL", "UPGRDRL", "DEDCOLRL", "CLMADJ", "CMPLARGRL", "LATAMHOLDCLM", "THFDOCRL"
                    Dim issue As New Issue(GuidControl.ByteArrayToGuid(row("ISSUE_ID")))
                    Dim newClaimIssue As ClaimIssue = CType(Me.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(Me.Id, issue.Id, Me.CertificateId, True)

            End Select

        Next


    End Sub

    Public Sub LoadResponses()
        For Each claimIssue As ClaimIssue In Me.ClaimIssuesList

            Dim objType As Type
            For Each issueResponse As ClaimIssueResponse In claimIssue.ClaimIssueResponseList
                Dim answer As New Answer(issueResponse.AnswerId)
                Dim question As New Question(answer.QuestionId)
                If (question.EntityAttributeId <> Nothing) Then
                    Dim obj As BusinessObjectBase
                    Dim entAttr As New EntityAttribute(question.EntityAttributeId)

                    Try

                        Dim propInfo As PropertyInfo
                        If (entAttr.Entity = EntityAttribute.EntityClaim) Then
                            obj = Me
                            objType = GetType(ClaimBase)
                            If obj Is Nothing Then
                                Throw New System.InvalidOperationException("Claim = Null not expected")
                            End If
                        Else
                            propInfo = Me.GetType().GetProperty(entAttr.Entity)
                            obj = propInfo.GetValue(Me, Nothing)
                            objType = propInfo.PropertyType

                            If obj Is Nothing Then
                                obj = objType.GetConstructor(New Type() {GetType(Guid), GetType(DataSet), GetType(Boolean)}).Invoke(New Object() {Me.Id, Me.Dataset, False})
                            End If
                        End If

                        Dim childpropInfo As PropertyInfo = objType.GetProperty(entAttr.Attribute)
                        Dim childpropType As Type = childpropInfo.PropertyType
                        Dim childPropValueObj As Object

                        If childpropType Is GetType(Guid) Then
                            childPropValueObj = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(issueResponse.AnswerValue))
                        ElseIf childpropType Is GetType(String) Then
                            childPropValueObj = issueResponse.AnswerValue
                        End If

                        childpropInfo.SetValue(obj, childPropValueObj, Nothing)

                        If (entAttr.Entity <> EntityAttribute.EntityClaim) Then
                            propInfo.SetValue(Me, obj, Nothing)
                        End If
                    Catch typeLoadException As ReflectionTypeLoadException

                    Catch ex As TargetException

                    End Try
                End If
            Next
        Next

    End Sub

    Public MustOverride Function CanIssuesReopen() As Boolean

#End Region

#Region "Claim Issues Dataview"

    Public Function GetClaimIssuesView() As ClaimIssuesView

        'Waive existing issues if needed
        If Not Me.IsNew Then
            WaiveExistingIssues()
        End If

        Dim t As DataTable = ClaimIssuesView.CreateTable
        Dim detail As ClaimIssue
        Dim filteredTable As DataTable

        Try

            For Each detail In Me.ClaimIssuesList
                Dim row As DataRow = t.NewRow
                row(ClaimIssuesView.COL_CLAIM_ISSUE_ID) = detail.ClaimIssueId.ToByteArray
                row(ClaimIssuesView.COL_ISSUE_DESC) = detail.IssueDescription
                row(ClaimIssuesView.COL_CREATED_BY) = detail.CreatedBy
                row(ClaimIssuesView.COL_CREATED_DATE) = detail.CreatedDate
                row(ClaimIssuesView.COL_PROCESSED_BY) = detail.ProcessedBy
                row(ClaimIssuesView.COL_PROCESSED_DATE) = detail.ProcessedDate
                row(ClaimIssuesView.COL_STATUS) = LookupListNew.GetDescriptionFromId(CLAIM_ISSUE_LIST, detail.StatusId)
                row(ClaimIssuesView.COL_STATUS_CODE) = detail.StatusCode
                t.Rows.Add(row)

            Next

            Return New ClaimIssuesView(t)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Sub WaiveExistingIssues()
        Dim dal As New ClaimDAL

        dal.WaiveExistingIssues(Me.Id, Me.Certificate.IdentificationNumber, Me.COMPLIANCE_RULE_CODE, Me.COMPLIANCE_ISSUE_CODE, "Y")

    End Sub
    Public Class ClaimIssuesView
        Inherits DataView
        Public Const COL_CLAIM_ISSUE_ID As String = "claim_issue_id"
        Public Const COL_ISSUE_DESC As String = "Issue_description"
        Public Const COL_CREATED_DATE As String = "Created_Date"
        Public Const COL_CREATED_BY As String = "Created_By"
        Public Const COL_PROCESSED_DATE As String = "Processed_Date"
        Public Const COL_PROCESSED_BY As String = "Processed_By"
        Public Const COL_STATUS As String = "Status"
        Public Const COL_STATUS_ID As String = "Status_Id"
        Public Const COL_STATUS_CODE As String = "Status_Code"


        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_CLAIM_ISSUE_ID, GetType(Byte()))
            t.Columns.Add(COL_ISSUE_DESC, GetType(String))
            t.Columns.Add(COL_CREATED_DATE, GetType(String))
            t.Columns.Add(COL_CREATED_BY, GetType(String))
            t.Columns.Add(COL_PROCESSED_DATE, GetType(String))
            t.Columns.Add(COL_PROCESSED_BY, GetType(String))
            t.Columns.Add(COL_STATUS, GetType(String))
            t.Columns.Add(COL_STATUS_CODE, GetType(String))
            Return t
        End Function
    End Class
#End Region

#Region "Claim Images"
    Public ReadOnly Property ClaimImagesList(Optional ByVal loadAllFiles As Boolean = False) As ClaimImage.ClaimImagesList
        Get
            Return New ClaimImage.ClaimImagesList(Me, loadAllFiles)
        End Get
    End Property

    Public Function AttachImage(ByVal pDocumentTypeId As Nullable(Of Guid),
                                ByVal pImageStatusId As Nullable(Of Guid),
                                ByVal pScanDate As Nullable(Of Date),
                                ByVal pFileName As String,
                                ByVal pComments As String,
                                ByVal pUserName As String,
                                ByVal pImageData As Byte()) As Guid

        Dim validationErrors As New List(Of ValidationError)

        ' Check if Document Type ID is supplied otherwise default to Other
        If (Not pDocumentTypeId.HasValue) Then
            pDocumentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER)
        End If

        ' Check if Image Status ID is supplied otherwise default to Pending
        If (Not pImageStatusId.HasValue) Then
            pImageStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PENDING)
        End If

        ' Check if Scan Date is supplied otherwise default to Current Date 
        If (Not pScanDate.HasValue) Then
            pScanDate = DateTime.Today
        End If

        Dim oClaimImage As ClaimImage
        oClaimImage = DirectCast(Me.ClaimImagesList.GetNewChild(Me.Id), ClaimImage)

        With oClaimImage
            .DocumentTypeId = pDocumentTypeId.Value
            .ImageStatusId = pImageStatusId.Value
            .ScanDate = pScanDate.Value
            .FileName = pFileName
            .Comments = pComments
            .UserName = pUserName
            .FileSizeBytes = pImageData.Length
            .ImageId = Guid.NewGuid()
            .IsLocalRepository = Codes.YESNO_Y
        End With

        Try

            ' This is to avoid any orphan images because of Elita Validation
            oClaimImage.Validate()

            Dim oRepository As Documents.Repository = Me.Company.GetClaimImageRepository()
            Dim doc As Documents.Document = oRepository.NewDocument
            With doc
                .Data = pImageData
                .FileName = pFileName
                .FileType = pFileName.Split(New Char() {"."}).Last()
                .FileSizeBytes = pImageData.Length
            End With

            oRepository.Upload(doc)
            oClaimImage.ImageId = doc.Id

            oClaimImage.Save()

            PublishedTask.AddEvent(companyGroupId:=Me.Company.CompanyGroupId,
                                   companyId:=Me.Dealer.Company.id,
                                   countryId:=Me.Company.CountryId,
                                   dealerId:=Me.Dealer.Id,
                                   productCode:=String.Empty,
                                   coverageTypeId:=Guid.Empty,
                                   sender:=CLAIM_DOC_UPLD_DETAILS,
                                   arguments:="ClaimId:" & DALBase.GuidToSQLString(Me.Id) & ";DocumentTypeId:" & DALBase.GuidToSQLString(oClaimImage.DocumentTypeId) & "",
                                   eventDate:=DateTime.UtcNow,
                                   eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_DOCUMENT_UPLOAD),
                                   eventArgumentId:=Nothing)

            Return doc.Id
        Catch ex As Exception
            oClaimImage.Delete()
            Throw
        End Try

    End Function

#End Region

#Region "Claim Images Dataview"

    Public Function GetClaimImagesView(Optional ByVal loadAllFiles As Boolean = False) As ClaimImagesView
        Dim t As DataTable = ClaimImagesView.CreateTable
        Dim detail As ClaimImage
        Dim filteredTable As DataTable

        Try

            For Each detail In Me.ClaimImagesList(loadAllFiles)
                Dim row As DataRow = t.NewRow
                row(ClaimImagesView.COL_CLAIM_IMAGE_ID) = detail.Id.ToByteArray
                row(ClaimImagesView.COL_IMAGE_ID) = detail.ImageId.ToByteArray
                row(ClaimImagesView.COL_SCAN_DATE) = detail.ScanDate.ToString()
                row(ClaimImagesView.COL_DOCUMENT_TYPE) = LookupListNew.GetDescriptionFromId(LookupListCache.LK_DOCUMENT_TYPES, detail.DocumentTypeId)
                row(ClaimImagesView.COL_STATUS) = LookupListNew.GetDescriptionFromId(LookupListCache.LK_CLM_IMG_STATUS, detail.ImageStatusId)
                row(ClaimImagesView.COL_STATUS_CODE) = LookupListNew.GetCodeFromId(LookupListCache.LK_CLM_IMG_STATUS, detail.ImageStatusId)
                If (detail.FileName Is Nothing) OrElse (detail.FileName.Trim().Length = 0) Then
                    row(ClaimImagesView.COL_FILE_NAME) = detail.ImageId.ToString()
                Else
                    row(ClaimImagesView.COL_FILE_NAME) = detail.FileName
                End If

                Dim _ft As Documents.FileType = Documents.DocumentManager.Current.FileTypes.Where(Function(ft) ft.Extension = detail.FileName.Split(".".ToCharArray()).Last().ToUpper()).FirstOrDefault()
                If (Not _ft Is Nothing) Then
                    row(ClaimImagesView.COL_FILE_TYPE) = _ft.Description
                End If

                If (Not detail.FileSizeBytes Is Nothing) Then
                    row(ClaimImagesView.COL_FILE_SIZE_BYTES) = detail.FileSizeBytes.Value
                End If
                row(ClaimImagesView.COL_COMMENTS) = detail.Comments
                If (detail.UserName Is Nothing) OrElse (detail.UserName.Trim().Length = 0) Then
                    row(ClaimImagesView.COL_USER_NAME) = detail.CreatedById
                Else
                    row(ClaimImagesView.COL_USER_NAME) = detail.UserName
                End If
                row(ClaimImagesView.COL_IS_LOCAL_REPOSITORY) = detail.IsLocalRepository
                row(ClaimImagesView.COL_DELETE_FLAG) = detail.DeleteFlag
                t.Rows.Add(row)

            Next

            Return New ClaimImagesView(t)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class ClaimImagesView
        Inherits DataView
        Public Const COL_CLAIM_IMAGE_ID As String = "CLAIM_IMAGE_ID"
        Public Const COL_IMAGE_ID As String = "IMAGE_ID"
        Public Const COL_SCAN_DATE As String = "SCAN_DATE"
        Public Const COL_DOCUMENT_TYPE As String = "DOCUMENT_TYPE"
        Public Const COL_STATUS As String = "STATUS"
        Public Const COL_STATUS_CODE As String = "STATUS_CODE"
        Public Const COL_FILE_NAME As String = "FILE_NAME"
        Public Const COL_FILE_TYPE As String = "FILE_TYPE"
        Public Const COL_FILE_SIZE_BYTES As String = "FILE_SIZE_BYTES"
        Public Const COL_COMMENTS As String = "COMMENTS"
        Public Const COL_USER_NAME As String = "USER_NAME"
        Public Const COL_IS_LOCAL_REPOSITORY As String = "IS_LOCAL_REPOSITORY"
        Public Const COL_DELETE_FLAG As String = "DELETE_FLAG"
        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_CLAIM_IMAGE_ID, GetType(Byte()))
            t.Columns.Add(COL_IMAGE_ID, GetType(Byte()))
            t.Columns.Add(COL_SCAN_DATE, GetType(String))
            t.Columns.Add(COL_DOCUMENT_TYPE, GetType(String))
            t.Columns.Add(COL_STATUS, GetType(String))
            t.Columns.Add(COL_STATUS_CODE, GetType(String))
            t.Columns.Add(COL_FILE_NAME, GetType(String))
            t.Columns.Add(COL_FILE_TYPE, GetType(String))
            t.Columns.Add(COL_FILE_SIZE_BYTES, GetType(Long))
            t.Columns.Add(COL_COMMENTS, GetType(String))
            t.Columns.Add(COL_USER_NAME, GetType(String))
            t.Columns.Add(COL_IS_LOCAL_REPOSITORY, GetType(String))
            t.Columns.Add(COL_DELETE_FLAG, GetType(String))
            Return t
        End Function
    End Class
#End Region

#Region "Replacement Parts"
    Public ReadOnly Property ReplacementPartChildren() As ReplacementPartList
        Get
            Return New ReplacementPartList(Me)
        End Get
    End Property

    Public Function GetReplacementPartChild(ByVal childId As Guid) As ReplacementPart
        Return CType(Me.ReplacementPartChildren.GetChild(childId), ReplacementPart)
    End Function

    Public Function GetNewReplacementPartChild() As ReplacementPart
        Dim newReplacementPart As ReplacementPart = CType(Me.ReplacementPartChildren.GetNewChild, ReplacementPart)
        newReplacementPart.ClaimId = Me.Id
        Return newReplacementPart
    End Function
#End Region
#Region "Replacement Items"
    Public Function GetReplacementItem(ByVal childId As Guid) As ClaimEquipment.ReplacementEquipmentDV
        Return ClaimEquipment.GetReplacementItemInfo(childId)
    End Function

    Public Function GetReplacementItemStatus(ByVal childId As Guid, ByVal equipmentId As Guid) As ClaimEquipment.ReplacementItemStatusDV
        Return ClaimEquipment.GetReplacementItemStatus(childId, equipmentId)
    End Function

#End Region

#Region "Case Id"
    Public Shared Function GetCaseIdByCaseNumberAndCompany(ByVal CaseNumber As String, ByVal CompanyCode As String) As Guid
        Dim dal As New ClaimDAL
        Dim caseID As Guid = Guid.Empty
        Dim ds As DataSet = dal.GetCaseIdByCaseNumberAndCompany(CaseNumber, CompanyCode)

        If (ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0) Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            caseID = New Guid(CType(dr(dal.COL_NAME_CASE_ID), Byte()))
        End If
        Return caseID
    End Function
#End Region



#Region "Claim Shipping"
    Public ReadOnly Property ClaimShippingList() As ClaimShipping.ClaimShippingList
        Get
            Return New ClaimShipping.ClaimShippingList(Me)
        End Get
    End Property
#End Region

#Region "Instance Methods"

    Public Sub PrePopulate(ByVal certItemCoverageId As Guid, ByVal mstrClaimNumber As String, ByVal DateOfLoss As DateType, Optional ByVal RecoveryButtonClick As Boolean = False, Optional ByVal WsUse As Boolean = False, Optional ByVal ComingFromDenyClaim As Boolean = False, Optional ByVal comingFromCert As Boolean = False, Optional ByVal callerName As String = Nothing, Optional ByVal problemDescription As String = Nothing, Optional ByVal ReportedDate As DateType = Nothing, Optional ByVal clmEquipment As ClaimEquipment = Nothing)

        Me.CertItemCoverageId = certItemCoverageId
        Me.Status = BasicClaimStatus.Active
        Me.CertItemCoverageId = certItemCoverageId
        Me.CompanyId = Certificate.CompanyId
        Me.RiskTypeId = Me.CertificateItem.RiskTypeId

        If (Me.CertificateItemCoverage.MethodOfRepairId = Guid.Empty) Then
            Me.MethodOfRepairId = Certificate.MethodOfRepairId
        Else
            Me.MethodOfRepairId = Me.CertificateItemCoverage.MethodOfRepairId
        End If

        If RecoveryButtonClick = True Then
            Me.MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__RECOVERY)
        End If

        If Me.MethodOfRepairId <> LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__RECOVERY) Then
            If Me.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFTLOSS _
                OrElse Me.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT _
                OrElse Me.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__LOSS Then
                Me.MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)
            End If
        End If
        '''''''''''''''''''''''''''''''''''''''
        If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, Me.Company.CompanyTypeId) = Me.Company.COMPANY_TYPE_INSURANCE Then
            Try
                If LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, Certificate.DocumentTypeID) = Codes.DOCUMENT_TYPE__CNPJ Then
                    Me.CallerName = Nothing
                    Me.CallerTaxNumber = Nothing
                Else
                    Me.CallerName = Certificate.CustomerName
                    Me.CallerTaxNumber = Certificate.IdentificationNumber
                End If
            Catch ex As Exception
                Me.CallerName = Nothing
                Me.CallerTaxNumber = Nothing
            End Try
        Else
            Me.CallerName = Certificate.CustomerName
            Me.CallerTaxNumber = Certificate.IdentificationNumber
        End If

        If Not callerName Is Nothing Then
            Me.CallerName = callerName
        End If

        '''''''''''''''''''''''''''''''''''''''
        Me.ContactName = Certificate.CustomerName

        Select Case Me.MethodOfRepairCode
            Case Codes.METHOD_OF_REPAIR__REPLACEMENT
                Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
            Case Codes.METHOD_OF_REPAIR__LEGAL, Codes.METHOD_OF_REPAIR__GENERAL
                Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__LEGAL_GENERAL)
        End Select

        Me.MasterClaimNumber = mstrClaimNumber
        Me.LossDate = DateOfLoss

        'REQ 1106 start
        Me.CreateEnrolledEquipment()
        If Not clmEquipment Is Nothing Then
            Me.CreateClaimedEquipment(clmEquipment)
            Me.CreateReplacementOptions()
        ElseIf (Not Me.CertificateItem.IsEquipmentRequired _
                AndAlso Not Me.CertificateItem Is Nothing _
                AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
            'build claimed equipment data from cert Item and create claimed equipment 
            Me.CreateClaimedEquipment(Me.CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
        End If

        'REQ 1106 end

        'If Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
        '    Me.Deductible = New DecimalType(0)
        '    Me.LiabilityLimit = New DecimalType(0)
        '    Me.DeductiblePercent = New DecimalType(0)
        '    Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        'Else
        '    Me.LiabilityLimit = Me.CertificateItemCoverage.LiabilityLimits
        '    PrepopulateDeductible()
        'End If

        Me.FollowupDate = New DateType(Date.Now.AddDays(Me.DefaultFollowUpDays.Value))

        Me.ClaimClosedDate = Nothing

        If (Not WsUse) Then Calculate_Discounts(Me.CertificateItemCoverage)

        If (ComingFromDenyClaim) Then Me.IsComingFromDenyClaim = True

        If comingFromCert AndAlso DeductibleByMfgFlag Then
            Dim ds As DataSet = LoadMfgDeductible(Me.CertItemCoverageId)
            If Not Me.MfgDeductible(ds) Is Nothing Then
                Me.Deductible = Me.MfgDeductible(ds)
            End If
        End If

        If Not problemDescription Is Nothing Then
            Me.ProblemDescription = problemDescription
        End If

        If Not ReportedDate Is Nothing Then
            Me.ReportedDate = ReportedDate.Value
        End If



        'Attach Issues to Claim based on Rules associated with the dealer
        Me.AttachIssues()
        Me.LoadResponses()

    End Sub
    <Obsolete("Tech Debt - to support deductible calculation for Argentina dealers when deductible is based on expression")>
    Public Sub TechDebtCalculateDeductible()
        If Me.MethodOfRepairCode <> MethodofRepairCodes.Replacement Then
            Me.Deductible = Me.AuthorizedAmount.Value * m_NewClaimRepairDedPercent / 100
        Else
            Me.Deductible = Me.AuthorizedAmount.Value * m_NewClaimOrigReplDedPercent / 100
        End If
    End Sub

    Public Sub PrepopulateDeductible()
        Dim oDeductible As CertItemCoverage.DeductibleType
        Dim moCertItemCvg As CertItemCoverage
        Dim moCertItem As CertItem
        Dim moCert As Certificate
        Dim listPriceDeductible As DecimalType

        oDeductible = CertItemCoverage.GetDeductible(Me.CertItemCoverageId, Me.MethodOfRepairId)

        If (oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT And
            oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
            moCertItemCvg = New CertItemCoverage(Me.CertItemCoverageId)
            moCertItem = New CertItem(moCertItemCvg.CertItemId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) Then
                moCert = New Certificate(moCertItemCvg.CertId)
            End If
        End If

        Select Case oDeductible.DeductibleBasedOn
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT
                If (oDeductible.DeductiblePercentage.Value > 0) Then
                    Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                    Me.Deductible = New DecimalType(0D)
                    Me.DeductiblePercent = oDeductible.DeductiblePercentage
                    Calculate_deductible_if_by_percentage()
                Else
                    Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                    Me.Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__FIXED
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                Me.Deductible = oDeductible.Deductible
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                Me.Deductible = GetDecimalValue(moCertItem.ItemRetailPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                Me.Deductible = GetDecimalValue(moCertItem.OriginalRetailPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                Me.Deductible = GetDecimalValue(moCert.SalesPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                If Not Me.LossDate Is Nothing Then
                    listPriceDeductible = ListPrice.GetListPrice(moCert.DealerId, If(Me.CertificateItem.IsEquipmentRequired, Me.ClaimedEquipment.SKU, moCertItem.SkuNumber), Me.LossDate.Value.ToString("yyyyMMdd"))
                    If (listPriceDeductible <> Nothing) Then
                        Me.Deductible = listPriceDeductible.Value * oDeductible.DeductiblePercentage / 100
                    Else
                        Me.Deductible = New DecimalType(0D)
                    End If
                Else
                    Me.Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                If Not Me.LossDate Is Nothing Then
                    listPriceDeductible = ListPrice.GetListPrice(moCert.DealerId, If(Me.CertificateItem.IsEquipmentRequired, Me.ClaimedEquipment.SKU, moCertItem.SkuNumber), moCert.WarrantySalesDate.Value.ToString("yyyyMMdd"))
                    If (listPriceDeductible <> Nothing) Then
                        Me.Deductible = listPriceDeductible.Value * oDeductible.DeductiblePercentage / 100
                    Else
                        Me.Deductible = New DecimalType(0D)
                    End If
                Else
                    Me.Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__EXPRESSION
                Dim cachefacade As New CacheFacade()

                Dim manager As New CommonManager(cachefacade)
                Dim listPrice As Nullable(Of Decimal) = Nothing
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                Me.DeductiblePercent = New DecimalType(0D)

                ''For New claims if deductible is based on expression then deductible is 30% for Repair and 50% REplacement of Auth amount
                If IsNew Then
                    Dim attvalue As AttributeValue = Me.Company.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.COMP_ATTR__TECH_DEBT_DEDUCTIBLE_RULE).FirstOrDefault
                    If Not attvalue Is Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                        TechDebtCalculateDeductible()
                    Else
                        Me.Deductible = SerializationExtensions.Serialize(Of BaseExpression)(
                        manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                        Evaluate(Function(variableName)
                                     Select Case variableName.ToUpperInvariant()
                                         Case "ListPrice".ToUpperInvariant()
                                             If (Not listPrice.HasValue) Then
                                                 listPrice = Me.GetListPrice()
                                             End If
                                             Return listPrice.Value
                                         Case "SalesPrice".ToUpperInvariant()
                                             Return Me.Certificate.SalesPrice
                                         Case "OrigRetailPrice".ToUpperInvariant()
                                             Return moCertItem.OriginalRetailPrice
                                         Case "ItemRetailPrice".ToUpperInvariant()
                                             Return moCertItem.ItemRetailPrice
                                         Case "LossType".ToUpperInvariant()
                                             Return String.Empty ''''Loss type is not passed from UI
                                         Case "AuthorizedAmount".ToUpperInvariant()
                                             Return Me.AuthorizedAmount
                                         Case "DeductibleBasePrice".ToUpperInvariant()

                                             Dim dvPrice As DataView = Me.GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                                  ServiceTypeCodes.DeductibleBasePrice)
                                             If Not dvPrice Is Nothing AndAlso dvPrice.Count > 0 Then
                                                 Return CDec(dvPrice(0)("Price"))
                                             Else
                                                 Return 0
                                             End If

                                         Case Else
                                             Return String.Empty
                                     End Select
                                 End Function)
                    End If

                Else

                    Me.Deductible = SerializationExtensions.Serialize(Of BaseExpression)(
                    manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                    Evaluate(Function(variableName)
                                 Select Case variableName.ToUpperInvariant()
                                     Case "ListPrice".ToUpperInvariant()
                                         If (Not listPrice.HasValue) Then
                                             listPrice = Me.GetListPrice()
                                         End If
                                         Return listPrice.Value
                                     Case "SalesPrice".ToUpperInvariant()
                                         Return Me.Certificate.SalesPrice
                                     Case "OrigRetailPrice".ToUpperInvariant()
                                         Return moCertItem.OriginalRetailPrice
                                     Case "ItemRetailPrice".ToUpperInvariant()
                                         Return moCertItem.ItemRetailPrice
                                     Case "LossType".ToUpperInvariant()
                                         Return String.Empty ''''Loss type is not passed from UI
                                     Case "AuthorizedAmount".ToUpperInvariant()
                                         Return Me.AuthorizedAmount
                                     Case "DeductibleBasePrice".ToUpperInvariant()

                                         Dim dvPrice As DataView = Me.GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                            ServiceTypeCodes.DeductibleBasePrice)
                                         If Not dvPrice Is Nothing AndAlso dvPrice.Count > 0 Then
                                             Return CDec(dvPrice(0)("Price"))
                                         Else
                                             Return 0
                                         End If

                                     Case Else
                                         Return String.Empty
                                 End Select
                             End Function)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__COMPUTED_EXTERNALLY
                Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
                Me.Deductible = New DecimalType(0D)
            Case Else
        End Select

    End Sub

    Public Function GetPricesForServiceType(ByVal serviceClassCode As String, ByVal serviceTypeCode As String) As DataView

        Dim dv As DataView
        Dim servCenter As New ServiceCenter(Me.ServiceCenterId)
        Dim equipConditionid As Guid
        Dim equipmentId As Guid
        Dim equipClassId As Guid

        'get the equipment information'if equipment not used then get the prices based on risktypeid
        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If Not Me.ClaimedEquipment Is Nothing AndAlso Not Me.ClaimedEquipment.EquipmentBO Is Nothing Then
                equipmentId = Me.ClaimedEquipment.EquipmentId
                equipClassId = Me.ClaimedEquipment.EquipmentBO.EquipmentClassId
                dv = PriceListDetail.GetPricesForServiceType(Me.CompanyId, servCenter.Code, Me.RiskTypeId,
                              DateTime.Now, Me.Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
            End If
        Else
            dv = PriceListDetail.GetPricesForServiceType(Me.CompanyId, servCenter.Code, Me.RiskTypeId,
                              DateTime.Now, Me.Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
        End If

        Return dv

    End Function

    Private Sub Calculate_Discounts(ByVal certItemCoverage As CertItemCoverage)

        Me.DiscountPercent = 0
        Me.DiscountAmount = CType(0, DecimalType)

        If Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse
           Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
            Me.DiscountPercent = certItemCoverage.RepairDiscountPct
        ElseIf Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Then
            Me.DiscountPercent = certItemCoverage.ReplacementDiscountPct
        End If

    End Sub

    Protected Function LoadMfgDeductible(ByVal oCertItemCoverageId As Guid) As DataSet
        Try
            Dim dal As New CertItemDAL
            Dim ds As New DataSet
            dal.LoadMfgDeductible(ds, Me.CertificateItemCoverage.CertItemId, Me.Contract.Id)
            If ds.Tables(CertItemDAL.TABLE_NAME_MFG_DEDUCT).Rows.Count > 1 Then
                Throw New DataNotFoundException
            End If
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Function GetDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Public Sub Calculate_deductible_if_by_percentage()
        Dim authAmount As Decimal = New Decimal(0D)
        If Me.ClaimAuthorizationType = BusinessObjectsNew.ClaimAuthorizationType.Single Then
            authAmount = Me.AuthorizedAmount.Value
        Else
            authAmount = CalculateAuthorizedAmountForDeductible()
        End If

        If Me.LiabilityLimit.Value > 0 Then
            If Me.LiabilityLimit.Value > authAmount Then
                Me.Deductible = New DecimalType((authAmount * Me.DeductiblePercent.Value) / 100)
            Else
                Me.Deductible = New DecimalType((Me.LiabilityLimit.Value * Me.DeductiblePercent.Value) / 100)
            End If
        End If
        If Me.LiabilityLimit.Value = 0 Then
            Me.Deductible = New DecimalType((authAmount * Me.DeductiblePercent.Value) / 100)
        End If
        'End If
    End Sub

    ''' <summary>
    ''' For Calculating Authorized Amount for MultiAuthClaims
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CalculateAuthorizedAmountForDeductible()
        Dim amt As Decimal = New Decimal(0D)
        Dim claim As MultiAuthClaim = CType(Me, MultiAuthClaim)

        For Each auth As ClaimAuthorization In claim.NonVoidClaimAuthorizationList

            For Each authDetail As ClaimAuthItem In auth.ClaimAuthorizationItemChildren
                'Check if ServiceClassType contributes to Deductible
                If ServiceCLassTypeList.Instance.IsDeductibleApplicable(authDetail.ServiceClassId, authDetail.ServiceTypeId) Then
                    amt = amt + authDetail.Amount
                End If
            Next

        Next
        Return amt
    End Function

    Public Sub Trace(ByVal id As String, ByVal screen As String, ByVal msg As String)
        AppConfig.DebugMessage.Trace(id, screen, msg & "@ SCount = none")
    End Sub

    Private Function CalculateOutstandingPremiumAmount() As Decimal
        Dim outAmt As Decimal = 0
        If Me.Contract.PayOutstandingPremiumId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            Dim dv As DataView
            Dim dvBilling As DataView
            Dim grossAmountReceived As Decimal
            Dim oBillingTotalAmount As Decimal

            dv = Certificate.PremiumTotals(Me.CertificateId)
            grossAmountReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)

            If Me.Dealer.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                dvBilling = BillingPayDetail.getBillPayTotals(Me.CertificateId)
                oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
            Else
                dvBilling = BillingDetail.getBillingTotals(Me.CertificateId)
                oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
            End If

            outAmt = grossAmountReceived - oBillingTotalAmount
        End If
    End Function

    <Obsolete("TECH_DEBT_AUTH_AMT_PERCENT-to support deductible calculation when it is based on percentage of Auth Amount")>
    Public Sub RecalculateDeductibleForChanges()
        If Me.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
            Dim al As ArrayList = Me.CalculateLiabilityLimit(Me.CertificateId, Me.Contract.Id, Me.CertItemCoverageId, Me.LossDate)
            If Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                If Me.Deductible.Value = 0 Then
                    If CType(al(1), Integer) = 0 Then
                        Me.LiabilityLimit = CType(al(0), Decimal)
                        If Not Me.DeductiblePercent Is Nothing Then
                            If Me.LiabilityLimit.Value > 0 Then
                                If Me.LiabilityLimit.Value > Me.AuthorizedAmount.Value Then
                                    Me.Deductible = New DecimalType((Me.AuthorizedAmount.Value * Me.DeductiblePercent.Value) / 100)
                                Else
                                    Me.Deductible = New DecimalType((Me.LiabilityLimit.Value * Me.DeductiblePercent.Value) / 100)
                                End If
                            End If
                            If Me.LiabilityLimit.Value = 0 Then
                                Me.Deductible = New DecimalType((Me.AuthorizedAmount.Value * Me.DeductiblePercent.Value) / 100)
                            End If
                        End If
                    End If
                End If
            Else
                If CType(al(1), Integer) = 0 Then
                    Me.LiabilityLimit = CType(al(0), Decimal)
                    If Not Me.DeductiblePercent Is Nothing Then
                        If Me.LiabilityLimit.Value > 0 Then
                            If Me.LiabilityLimit.Value > Me.AuthorizedAmount.Value Then
                                Me.Deductible = New DecimalType((Me.AuthorizedAmount.Value * Me.DeductiblePercent.Value) / 100)
                            Else
                                Me.Deductible = New DecimalType((Me.LiabilityLimit.Value * Me.DeductiblePercent.Value) / 100)
                            End If
                        End If
                        If Me.LiabilityLimit.Value = 0 Then
                            Me.Deductible = New DecimalType((Me.AuthorizedAmount.Value * Me.DeductiblePercent.Value) / 100)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub RecalcDeductibleForExpOrAuthAmountPercent()
        Dim oDeductible As CertItemCoverage.DeductibleType
        Dim moCertItemCvg As CertItemCoverage
        Dim moCertItem As CertItem
        Dim moCert As Certificate
        Dim listPriceDeductible As DecimalType

        oDeductible = CertItemCoverage.GetDeductible(Me.CertItemCoverageId, Me.MethodOfRepairId)

        If (oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT And
            oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
            moCertItemCvg = New CertItemCoverage(Me.CertItemCoverageId)
            moCertItem = New CertItem(moCertItemCvg.CertItemId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) Then
                moCert = New Certificate(moCertItemCvg.CertId)
            End If
        End If

        Dim al As ArrayList = Me.CalculateLiabilityLimit(Me.CertificateId, Me.Contract.Id, Me.CertItemCoverageId, Me.LossDate)

        Select Case oDeductible.DeductibleBasedOn
            Case Codes.DEDUCTIBLE_BASED_ON__EXPRESSION

                If CType(al(1), Integer) = 0 Then
                    Me.LiabilityLimit = CType(al(0), Decimal)
                End If
                If Not IsNew Then
                    Dim cachefacade As New CacheFacade()

                    Dim manager As New CommonManager(cachefacade)
                    Dim listPrice As Nullable(Of Decimal) = Nothing
                    Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                    Me.DeductiblePercent = New DecimalType(0D)
                    Me.Deductible = SerializationExtensions.Serialize(Of BaseExpression)(
                            manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                            Evaluate(Function(variableName)
                                         Select Case variableName.ToUpperInvariant()
                                             Case "ListPrice".ToUpperInvariant()
                                                 If (Not listPrice.HasValue) Then
                                                     listPrice = Me.GetListPrice()
                                                 End If
                                                 Return listPrice.Value
                                             Case "SalesPrice".ToUpperInvariant()
                                                 Return Me.Certificate.SalesPrice
                                             Case "OrigRetailPrice".ToUpperInvariant()
                                                 Return moCertItem.OriginalRetailPrice
                                             Case "ItemRetailPrice".ToUpperInvariant()
                                                 Return moCertItem.ItemRetailPrice
                                             Case "LossType".ToUpperInvariant()
                                                 Return String.Empty ''''Loss type is not passed from UI
                                             Case "AuthorizedAmount".ToUpperInvariant()
                                                 Return Me.AuthorizedAmount
                                             Case "DeductibleBasePrice".ToUpperInvariant()

                                                 Dim dvPrice As DataView = Me.GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                                      ServiceTypeCodes.DeductibleBasePrice)
                                                 If Not dvPrice Is Nothing AndAlso dvPrice.Count > 0 Then
                                                     Return CDec(dvPrice(0)("Price"))
                                                 Else
                                                     Return 0
                                                 End If

                                             Case Else
                                                 Return String.Empty
                                         End Select
                                     End Function)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT
                RecalculateDeductibleForChanges()
            Case Else
        End Select
    End Sub

    Public Function IsMaxReplacementExceeded(ByVal CertID As Guid, ByVal CurrentLossDate As Date, Optional ByVal blnExcludeSelf As Boolean = True) As Boolean
        ' only valid for repair and replacement claim
        If Not (MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT _
           OrElse MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME _
           OrElse MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN _
           OrElse MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP _
           OrElse MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN) Then
            Return False
        End If


        Dim TotalClaimsAllowed, RepairsAllowed, ReplAllowed, TotalClaims, Replacements, Repairs As Integer, FirstLossDate As String, dtFirstLossDate As Date, FoundFirstDate As Boolean = False
        Dim dal As New ClaimDAL

        If Me.Contract.ClaimLimitBasedOnId.Equals(Guid.Empty) Then
            Return False
        Else
            Dim strClaimNum As String = String.Empty
            If blnExcludeSelf AndAlso (Not ClaimNumber Is Nothing) Then
                strClaimNum = ClaimNumber
            End If

            Dim ReplacementBasedOn As String
            ReplacementBasedOn = LookupListNew.GetCodeFromId(LookupListNew.LK_REPLACEMENT_BASED_ON, Me.Contract.ClaimLimitBasedOnId)
            If ReplacementBasedOn = Codes.REPLACEMENT_BASED_ON__INSURANCE_ACTIVATION_DATE Then
                If Not Me.Certificate.InsuranceActivationDate Is Nothing Then
                    CurrentLossDate = GetStartDateOf12MonthWindow(Me.Certificate.InsuranceActivationDate.Value, CurrentLossDate)
                End If
            Else
                If ReplacementBasedOn = Codes.REPLACEMENT_BASED_ON__DATE_OF_LOSS Then
                    dal.GetFirstLossDate(CertID, FirstLossDate)
                    If FirstLossDate <> String.Empty Then
                        dtFirstLossDate = DateHelper.GetDateValue(FirstLossDate)
                        CurrentLossDate = GetStartDateOf12MonthWindow(dtFirstLossDate, CurrentLossDate)
                    End If
                End If
            End If
            dal.GetPreviousYearReplacements(CertID, CurrentLossDate, strClaimNum, TotalClaimsAllowed, RepairsAllowed, ReplAllowed, TotalClaims, Replacements, Repairs)
            Return ((Repairs >= RepairsAllowed And (MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Or MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Or MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Or MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN)) _
                         Or (Replacements >= ReplAllowed And MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) _
                         Or (TotalClaims >= TotalClaimsAllowed))
        End If
    End Function

    Private Function GetStartDateOf12MonthWindow(ByVal dtStart As Date, ByVal dtCurrent As Date) As Date
        Dim blnIn As Boolean = False
        If dtCurrent <= dtStart Then
            Return dtStart
        Else
            Do Until (blnIn = True)
                If dtCurrent >= dtStart And dtCurrent <= dtStart.AddMonths(12).AddDays(-1) Then
                    blnIn = True
                    Exit Do
                Else
                    dtStart = dtStart.AddMonths(12)
                End If
            Loop
            Return dtStart
        End If
    End Function

    Public Overridable Function CheckForRules()
        Dim comment As Comment = Me.AddNewComment()
        'Change status to Active and run the rules
        If Me.Status = BasicClaimStatus.Pending Then
            Me.Status = BasicClaimStatus.Active
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
        End If

        'Check for Rules for which claim could be denied
        CheckForDenyingRules(comment)

        'Check for Rules for which claim could go to Pending status
        If (Me.Status = BasicClaimStatus.Active) Then CheckForPendingRules(comment)

    End Function

    Public Sub CheckForPendingRules(ByRef comment As Comment)
        Dim flag As Boolean = True
        If (comment Is Nothing) Then comment = Me.AddNewComment

        If Not Me.Certificate.IsSubscriberStatusValid Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
        End If

        If Me.Contract.PayOutstandingPremiumId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) AndAlso Me.OutstandingPremiumAmount.Value > 0 Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_PAYMENT_ON_OUTSTANDING_PREMIUM)
        End If

        ''''New Equipment Flow starts here
        If Me.CertificateItem.IsEquipmentRequired Then
            If (Not ValidateAndMatchClaimedEnrolledEquipments(comment)) Then
                flag = flag And False
            End If
        End If
        '''''

        ' Check if Deductible Calculation Method is List and SKU Price is resolved
        If (Me.DeductibleType.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
            Dim lstPrice As DecimalType = Me.GetListPrice()
            If (lstPrice Is Nothing) Then
                flag = flag And False
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
            End If
        End If

        If (Me.Dealer.DeductibleCollectionId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If Me.DedCollectionMethodID = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_DEFFERED_COLL) Then
                flag = flag And False
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                comment.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_NOT_COLLECTED)
            End If
        End If

        If (Me.HasIssues AndAlso Not Me.AllIssuesResolvedOrWaived) Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
            comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
        End If

        If (Not flag) Then Me.Status = BasicClaimStatus.Pending

    End Sub

    Public Sub CheckForDenyingRules(ByRef comment As Comment)
        Dim flag As Boolean = True
        If (comment Is Nothing) Then comment = Me.AddNewComment

        Dim blnExceedMaxReplacements As Boolean = False
        Dim blnClaimReportedWithinPeriod As Boolean = True
        Dim blnClaimReportedWithinGracePeriod As Boolean = True
        Dim blnCoverageTypeNotMissing As Boolean = True

        Dim objCert As New Certificate(Me.CertificateId)
        Dim oDealer As New Dealer(objCert.DealerId)


        If objCert.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso oDealer.IsGracePeriodSpecified Then

            If Me.IsNew Then
                If Not Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                    blnCoverageTypeNotMissing = Me.IsClaimReportedWithValidCoverage(Me.CertificateId, Me.CertItemCoverageId, Me.LossDate.Value, Me.ReportedDate.Value)
                End If
            End If

            If Me.IsNew Then
                If Not Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                    blnClaimReportedWithinGracePeriod = Me.IsClaimReportedWithinGracePeriod(Me.CertificateId, Me.CertItemCoverageId, Me.LossDate.Value, Me.ReportedDate.Value)
                End If
            End If

            If blnClaimReportedWithinGracePeriod And blnCoverageTypeNotMissing Then
                If Me.IsNew Then
                    blnExceedMaxReplacements = Me.IsMaxReplacementExceeded(Me.CertificateId, Me.LossDate.Value)
                End If
            End If
        Else
            If Me.IsNew Then 'only check the condition for new claim
                blnExceedMaxReplacements = Me.IsMaxReplacementExceeded(Me.CertificateId, Me.LossDate.Value)
                If Not Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then ' Not a Valid Condition for Service Warranty Claims
                    blnClaimReportedWithinPeriod = Me.IsClaimReportedWithinPeriod(Me.CertificateId, Me.LossDate.Value, Me.ReportedDate.Value)
                End If
            End If
        End If

        'Add comments to indicate that the claim will be closed

        If objCert.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso oDealer.IsGracePeriodSpecified Then


            If Not blnCoverageTypeNotMissing Then
                flag = flag And False
                Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__COVERAGE_TYPE_MISSING)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_COVERAGE_TYPE_MISSING)

            ElseIf Not blnClaimReportedWithinGracePeriod Then
                flag = flag And False
                Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED__NOT_REPORTED_WITHIN_GRACE_PERIOD)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_NOT_WITHIN_GRACE_PERIOD)


            ElseIf blnExceedMaxReplacements Then
                flag = flag And False
                Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__REPLACEMENT_EXCEED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)

            ElseIf (Me.EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                flag = flag And False
                If Not Me.IssueDeniedReason Is Nothing Then
                    Me.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, Me.IssueDeniedReason())
                Else
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                End If
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")


            ElseIf (CType(Me.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Decimal) > 0) Then
                If (CoverageRemainLiabilityAmount(Me.CertItemCoverageId, Me.LossDate) <= 0) Then
                    flag = flag And False
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If

            ElseIf (CType(Me.Certificate.ProductLiabilityLimit.ToString, Decimal) > 0) Then
                If (ProductRemainLiabilityAmount(Me.CertificateId, Me.LossDate) <= 0) Then
                    flag = flag And False
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If

        Else

            If blnExceedMaxReplacements Then
                flag = flag And False
                Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__REPLACEMENT_EXCEED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
            End If

            If Not blnClaimReportedWithinPeriod Then
                flag = flag And False
                Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__NOT_REPORTED_WITHIN_PERIOD)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_EXCEED)
            End If
            If (Me.EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                flag = flag And False
                If Not Me.IssueDeniedReason Is Nothing Then
                    Me.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, Me.IssueDeniedReason())
                Else
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                End If

                'flag = flag And False
                'Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
            End If

            If (CType(Me.CertificateItemCoverage.CoverageLiabilityLimit.ToString, Decimal) > 0) Then
                If (CoverageRemainLiabilityAmount(Me.CertItemCoverageId, Me.LossDate) <= 0) Then
                    flag = flag And False
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
            If (CType(Me.Certificate.ProductLiabilityLimit.ToString, Decimal) > 0) Then
                If (ProductRemainLiabilityAmount(Me.CertificateId, Me.LossDate) <= 0) Then
                    flag = flag And False
                    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
        End If


        If (Not flag) Then
            Me.ClaimClosedDate = New DateType(System.DateTime.Today)
            Me.ClaimActivityId = Guid.Empty
            Me.Status = BasicClaimStatus.Denied
        End If

    End Sub

    Private Function GetListPrice() As DecimalType
        Dim lstPrice As DecimalType
        Dim strSku As String = Me.NewDeviceSku
        If strSku = String.Empty Then strSku = Me.CertificateItem.SkuNumber
        If Me.CertificateItem.IsEquipmentRequired Then strSku = Me.ClaimedEquipment.SKU
        lstPrice = ListPrice.GetListPrice(Me.Certificate.DealerId, strSku, Me.LossDate.Value.ToString("yyyyMMdd"))
        Return lstPrice
    End Function

    Public Sub CalculateFollowUpDate()
        Try
            Me.FollowupDate = New DateType(NonbusinessCalendar.GetNextBusinessDate(Me.DefaultFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        Catch ex As Exception
        End Try
    End Sub

    Public Overridable Sub CreateClaim()
        Me.CheckForRules()
        'REQ-1106
        'If (Me.DeductibleType.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
        '    'REQ-918 Get the Claim Equipment Object for this Claim and Assign the above Price to it
        '    If Not Me.State.claimEquipmentBO Is Nothing Then
        '        Me.State.claimEquipmentBO.Price = lstPrice
        '        Me.State.claimEquipmentBO.Save()
        '    End If

        'End If
        Me.CalculateFollowUpDate()
        Me.IsUpdatedComment = True
    End Sub

    Public Overridable Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oldStatus As String = Me.StatusCode

        Dim triggerEvent As Boolean

        '' Check if Record is New
        If Not Me.Row.HasVersion(DataRowVersion.Original) Then
            '' New Claim Record is being Created
            '' PBI 494630 - Enabled for PENDING status 
            triggerEvent = True
        Else
            If IsClaimStatusChanged() AndAlso (Me.StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE) Or Me.StatusCode.Equals(Codes.CLAIM_STATUS__DENIED)) Then
                '' Existing Claim is being modifed and Claim Status Changed
                triggerEvent = True
            Else
                triggerEvent = False
            End If
        End If

        Try
            'Claim lock - start
            ' ''Step 1 - To prevent saving of claim locked by other, specially via web service
            'If Me.LockedBy <> ElitaPlusIdentity.Current.ActiveUser.Id Then
            '    'need to do something more than just exception so that UI and web service can interpret the problem
            '    Dim strLockedbyuser As String = (New User(Me.LockedBy)).UserName
            '    Throw New Exception(TranslationBase.TranslateLabelOrMessage("CLAIM_LOCK") & " - " & _
            '                                    strLockedbyuser)
            'End If
            ''step 2 - once we determine that current user is the one who locked the claim then we can unlock it
            'unlock the claim
            Me.LockedBy = Guid.Empty
            Me.lockedOn = Nothing
            Me.IsLocked = Codes.YESNO_N
            ''claim lock - End            

            If Not Me.IsNew Then
                If Me.GetDealerDRPTradeInQuoteFlag(Me.DealerCode) Then
                    If IsStatusChngdFromPendingOrClosedToActive() Then
                        If Me.VerifyIMEIWithDRPSystem(Me.SerialNumber) Then
                            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, Nothing, Assurant.ElitaPlus.Common.ErrorCodes.ACTIVE_TRADEIN_QUOTE_EXISTS_ERR)
                        End If
                    End If
                End If
            End If
            MyBase.Save()

            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Me.HandelTimeZoneForClaimExtStatusDate()
                Dim dal As New ClaimDAL
                MyBase.UpdateFamily(Me.Dataset)
                UpdateClaimBeforeSave()

                If (Me.Dataset.Tables.Contains(ClaimIssueDAL.TABLE_NAME)) Then
                    For Each dr As DataRow In Me.Dataset.Tables(ClaimIssueDAL.TABLE_NAME).Rows
                        Dim oClaimIssue As New ClaimIssue(dr)
                        oClaimIssue.ProcessWorkQueueItem(Me.CurrentWorkQueueItem)
                    Next
                End If

                'When Saving Claim Update the status of claim Image
                For Each ClaimImage As ClaimImage In Me.ClaimImagesList
                    ClaimImage.ImageStatusId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PROCESSED)
                Next

                dal.UpdateFamily(Me.Dataset,
                                 IsUpdatedComment,
                                 moGalaxyClaimNumberList,
                                 New PublishedTaskDAL.PublishTaskData(Of ClaimIssueDAL.PublishTaskClaimData) With
                                             {
                                                 .CompanyGroupId = Me.Company.CompanyGroupId,
                                                 .CompanyId = Me.Company.Id,
                                                 .CountryId = Me.Company.CountryId,
                                                 .CoverageTypeId = Me.CertificateItemCoverage.CoverageTypeId,
                                                 .DealerId = Me.Dealer.Id,
                                                 .ProductCode = Me.Certificate.ProductCode,
                                                 .Data = New ClaimIssueDAL.PublishTaskClaimData() With
                                                         {
                                                             .ClaimId = Me.Id,
                                                             .EventTypeLookup = Function(publishTaskStatusId As Guid) As Guid
                                                                                    Dim publishedTaskStatusCode As String = LookupListNew.GetCodeFromId(Codes.CLAIM_ISSUE_STATUS, publishTaskStatusId)
                                                                                    Select Case publishedTaskStatusCode
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__WAIVED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_WAIVED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__RESOLVED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_RESOLVED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__REJECTED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_REJECTED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__PENDING
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_PENDING)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__OPEN
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_OPENED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__CLOSED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_CLOSED)
                                                                                        Case Else
                                                                                            Throw New NotSupportedException()
                                                                                    End Select

                                                                                End Function
                                                             }
                                                 },
                                 Transaction,
                                 Me.AuthDetailDataHasChanged,
                                 Me.moIsUpdatedMasterClaimComment,
                                 ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If


                If triggerEvent Then
                    Dim eventTypeID As Guid

                    If Me.StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_APPROVED)
                    ElseIf Me.StatusCode.Equals(Codes.CLAIM_STATUS__DENIED) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_DENIED)
                    ElseIf Me.StatusCode.Equals(Codes.CLAIM_STATUS__PENDING) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_REFERRED)
                    End If

                    PublishedTask.AddEvent(
                        companyGroupId:=Me.Company.CompanyGroupId,
                        companyId:=Me.Company.Id,
                        countryId:=Me.Company.CountryId,
                        dealerId:=Me.Dealer.Id,
                        productCode:=Me.Certificate.ProductCode,
                        coverageTypeId:=Me.CertificateItemCoverage.CoverageTypeId,
                        sender:="Claim Elita UI",
                        arguments:="ClaimId:" & DALBase.GuidToSQLString(Me.Id),
                        eventDate:=DateTime.UtcNow,
                        eventTypeId:=eventTypeID,
                        eventArgumentId:=Guid.Empty)

                End If

            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.StatusCode = oldStatus
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex, ex.Code)
        End Try
    End Sub
    Private Function IsClaimStatusChanged() As Boolean
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
          (Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) <> Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current)) Then
            Return (True)
        Else
            Return (False)
        End If
    End Function
    Public Sub UpdateClaimBeforeSave()
        If ClaimsAdjuster Is Nothing Then
            ClaimsAdjuster = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            ClaimsAdjusterName = ElitaPlusIdentity.Current.ActiveUser.UserName
        End If

    End Sub

    Public Shared Function ProductRemainLiabilityAmount(ByVal CertId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.ProductRemainLiabilityAmount(CertId, lossDate)
    End Function
    Public Shared Function CoverageRemainLiabilityAmount(ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.CoverageRemainLiabilityAmount(CertItemCoverageId, lossDate)
    End Function

    Public Sub Cancel()
        Me.StatusCode = Codes.CLAIM_STATUS__CLOSED
        Me.ClaimClosedDate = New DateType(System.DateTime.Today)
        Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED)
        Dim c As Comment = Me.AddNewComment()
        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_NOT_APPROVED)
    End Sub

    Public Sub DenyClaim()
        Me.StatusCode = Codes.CLAIM_STATUS__DENIED
        Me.ClaimClosedDate = New DateType(System.DateTime.Today)
        'Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED)
        Dim c As Comment = Me.AddNewComment()
        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
        Me.ClaimActivityId = Guid.Empty
    End Sub

    Public Function GetLatestServiceOrder(Optional claimAuthID As Guid = Nothing) As ServiceOrder
        Dim serviceOrderBO As ServiceOrder = Nothing
        Try
            Dim serviceOrderID As Guid = ServiceOrder.GetLatestServiceOrderID(Me.Id, claimAuthID)
            serviceOrderBO = New ServiceOrder(serviceOrderID)
        Catch ex As Exception
        End Try
        Return serviceOrderBO
    End Function

    Public Overridable Sub ReopenClaim()
        Me.CalculateFollowUpDate()
        Me.ReasonClosedId = Guid.Empty
        Me.Status = BasicClaimStatus.Active
        Me.ClaimClosedDate = Nothing
        If Me.ClaimNumber.ToUpper.EndsWith("R") AndAlso Me.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)) Then
            Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
        End If
    End Sub

    Public Overridable Sub CloseTheClaim()

        If ((Me.IsDirty) AndAlso
            (Not (Me.ReasonClosedId.Equals(Guid.Empty)))) Then
            'Close the Claim
            If (Me.Status <> BasicClaimStatus.Closed) Then
                Me.Status = BasicClaimStatus.Closed
            End If
            If (Me.ClaimClosedDate Is Nothing) Then
                Me.ClaimClosedDate = New DateType(System.DateTime.Today)
            End If
        End If

    End Sub

    Public Function IsDeductibleAmountChanged() As Boolean
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
           Not (Me.Row.Item(ClaimDAL.COL_NAME_DEDUCTIBLE, DataRowVersion.Original).Equals _
          (Me.Row.Item(ClaimDAL.COL_NAME_DEDUCTIBLE, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsStatusChngdFromPendingOrClosedToActive() As Boolean
        Dim OriginalReasonClosed As String
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
          (Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) = Codes.CLAIM_STATUS__PENDING) AndAlso
          (Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current) = Codes.CLAIM_STATUS__ACTIVE) Then
            Return (True)
        Else
            If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
            (Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) = Codes.CLAIM_STATUS__CLOSED) AndAlso
            (Me.Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current) = Codes.CLAIM_STATUS__ACTIVE) Then
                OriginalReasonClosed = LookupListNew.GetCodeFromId(LookupListNew.LK_REASONS_CLOSED, New Guid(CType(Me.Row.Item(ClaimDAL.COL_NAME_REASON_CLOSED_ID, DataRowVersion.Original), Byte())))
                If NumberOfDisbursements > 0 And OriginalReasonClosed <> Codes.REASON_CLOSED__NO_ACTIVITY Then
                    Return (False)
                Else
                    Return (True)
                End If
            Else
                Return (False)
            End If
        End If

    End Function

    Public Function IsAuthorizedAmountChanged() As Boolean
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
           Not (Me.Row.Item(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, DataRowVersion.Original).Equals _
          (Me.Row.Item(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsProblemDescriptionChanged() As Boolean
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
            Not (Me.Row.Item(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, DataRowVersion.Original).Equals _
              (Me.Row.Item(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsSpecialInstructionChanged() As Boolean
        If Me.Row.HasVersion(DataRowVersion.Current) AndAlso
            Not (Me.Row.Item(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, DataRowVersion.Original).Equals _
              (Me.Row.Item(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function UseRecoveries() As Boolean
        Dim dv As DataView
        dv = LookupListNew.GetCompanyLookupList(Me.CompanyId)

        If dv.Count > 0 Then
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(CType(dv(0)(ClaimDAL.COL_NAME_USE_RECOVERIES_ID), Byte()))) = Codes.YESNO_Y Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function AddExtendedClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            _claimStatusBO = New ClaimStatus(claimStatusId, Me.Dataset)
        Else
            _claimStatusBO = New ClaimStatus(Me.Dataset)
        End If

        Return _claimStatusBO
    End Function

    Private Function getTotalPaid(ByVal claimId As Guid) As MultiValueReturn(Of DecimalType, Integer)
        getTotalPaid = New ClaimDAL().LoadTotalPaid(claimId)
    End Function

    Private Function getTotalPaidForCert(ByVal CertId As Guid) As DecimalType
        Return New ClaimDAL().LoadTotalPaidForCert(CertId)
    End Function

    Public Shared Function GetOriginalLiabilityLimit(ByVal ClaimId As Guid) As Claim.MaterClaimDV
        Dim _dal As New ClaimDAL
        Dim ds As DataSet
        Dim dv As DataView
        Try
            ds = _dal.GetOriginalLiabilityAmount(ClaimId)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return New Claim.MaterClaimDV(ds.Tables(0))
            Else
                Return New Claim.MaterClaimDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function


#End Region

#Region "Shared Methods"

    Public Shared Function GetCauseOfLossID(ByVal CoverageTypeId As Guid) As Guid
        Dim oCoverageType As New CoverageType(CoverageTypeId)
        Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindDefault

        If Not oCoverageLoss Is Nothing Then
            Return oCoverageLoss.CauseOfLossId
        Else
            Return Guid.Empty
        End If
    End Function

    Public Shared Function CalculateLiabilityLimit(ByVal certId As Guid, ByVal contractId As Guid, ByVal certItemCoverageId As Guid, Optional ByVal lossDate As DateType = Nothing) As ArrayList
        Dim dal As New ClaimDAL
        Return dal.CalculateLiabilityLimit(certId, contractId, certItemCoverageId, lossDate)
    End Function

    Public Shared Function IsClaimReportedWithinPeriod(ByVal guidCertID As Guid, ByVal dtLossDate As Date, ByVal dtDateReported As Date) As Boolean
        Dim objCert As New Certificate(guidCertID)
        Dim oContract As Contract
        oContract = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
        Dim blnResult As Boolean = False
        If Not oContract Is Nothing Then
            If (Not oContract.DaysToReportClaim Is Nothing) AndAlso (oContract.DaysToReportClaim.Value > 0) Then
                Dim intDaysReported As Integer = dtDateReported.Subtract(dtLossDate).Days
                If intDaysReported <= oContract.DaysToReportClaim.Value Then
                    blnResult = True
                End If
            Else ' always return true if DaysToReportClaim is not specified in contract
                blnResult = True
            End If
        End If
        Return blnResult
    End Function
    Public Shared Function IsClaimReportedWithinGracePeriod(ByVal guidCertID As Guid, ByVal guidCertItemCoverageID As Guid, ByVal dtLossDate As Date, ByVal dtDateReported As Date) As Boolean
        Dim objCert As New Certificate(guidCertID)
        Dim blnResult As Boolean = True

        Dim oDealer As New Dealer(objCert.DealerId)

        Dim gracePeriodDays As Integer = If(oDealer.GracePeriodDays, 0)
        Dim gracePeriodMonths As Integer = If(oDealer.GracePeriodMonths, 0)

        'If oDealer.GracePeriodDays Is Nothing Or Not oDealer.GracePeriodMonths Is Nothing Then
        '    oDealer.GracePeriodDays = 0
        'ElseIf oDealer.GracePeriodMonths Is Nothing And Not oDealer.GracePeriodDays Is Nothing Then
        '    oDealer.GracePeriodMonths = 0
        'End If

        'Dim gracePeriodMonths As Integer = oDealer.GracePeriodMonths
        'Dim gracePeriodDays As Integer = oDealer.GracePeriodDays

        Dim oCertItemCoverage As New CertItemCoverage(guidCertItemCoverageID)
        Dim dtCoverageEndDate As DateType = oCertItemCoverage.EndDate
        If gracePeriodMonths > 0 Or gracePeriodDays > 0 Then
            Dim dtGracePeriodEndDate As Date = dtCoverageEndDate.Value.AddMonths(gracePeriodMonths).AddDays(gracePeriodDays)

            If dtLossDate <= dtCoverageEndDate Then
                If dtDateReported > dtGracePeriodEndDate Then
                    blnResult = False
                End If

            End If
        End If

        'dtCoverageEndDate < dtLossDate Then
        '                If (dtDateReported < dtGracePeriodEndDate Or dtDateReported > dtGracePeriodEndDate) Then
        '                    blnResult = False
        '                End If
        '            End If

        '            If (dtLossDate < dtCoverageEndDate) AndAlso (dtGracePeriodEndDate < dtDateReported) Then
        '                blnResult = False
        '            End If

        '            'If dtLossDate.Subtract(dtCoverageEndDate).Minutes > 0 Or dtDateReported.Subtract(dtGracePeriodEndDate).Minutes > 0 Then
        '            '    blnResult = False
        '            'End If
        '        End If

        Return blnResult
    End Function
    Public Shared Function IsClaimReportedWithValidCoverage(ByVal guidCertID As Guid, ByVal guidCertItemCoverageID As Guid, ByVal dtLossDate As Date, ByVal dtDateReported As Date) As Boolean
        Dim objCert As New Certificate(guidCertID)
        Dim blnResult As Boolean = True

        Dim oDealer As New Dealer(objCert.DealerId)

        Dim gracePeriodDays As Integer = If(oDealer.GracePeriodDays, 0)
        Dim gracePeriodMonths As Integer = If(oDealer.GracePeriodMonths, 0)


        Dim oCertItemCoverage As New CertItemCoverage(guidCertItemCoverageID)
        Dim dtCoverageEndDate As DateType = oCertItemCoverage.EndDate

        If gracePeriodMonths > 0 Or gracePeriodDays > 0 Then

            Dim dtGracePeriodEndDate As Date = dtCoverageEndDate.Value.AddMonths(gracePeriodMonths).AddDays(gracePeriodDays)

            If dtLossDate > dtCoverageEndDate Then
                If (dtDateReported <= dtGracePeriodEndDate Or dtDateReported >= dtGracePeriodEndDate) Then
                    blnResult = False
                End If
            End If
        End If
        Return blnResult

    End Function

    Public Function GetDealerDRPTradeInQuoteFlag(ByVal dealerCode As String) As Boolean
        Dim dv As DataView
        Dim dal As New ClaimDAL

        dv = (dal.GetDealerDRPTradeInQuoteFlag(dealerCode)).Tables(0).DefaultView

        If dv.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GetIndixIdofRegisteredDevice(ByVal claimId As Guid) As String
        Dim dv As DataView
        Dim claimDalObj As ClaimDAL = New ClaimDAL()
        Dim IndixId As String = claimDalObj.GetIndixIdofRegisteredDevice(claimId)

        Return IndixId

    End Function

    Public Shared Function VerifyIMEIWithDRPSystem(ByVal IMEI As String) As Boolean
        Dim oDRP As New DRP
        Dim Result As Boolean
        Result = oDRP.Get_DoesAcceptedOfferExist(IMEI)
        Return Result
    End Function

    Public Shared Function GetClaimCaseDeviceInfo(ByVal claimId As Guid) As DataView
        Dim dal As New ClaimDAL
        Dim ds As DataSet
        Try
            ds = dal.GetClaimCaseDeviceInfo(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return ds.Tables(0).DefaultView
        Catch ex As Exception
            Return New DataView()
        End Try

    End Function
#End Region

#Region "Childeren"

    Private _comment As Comment

    Public Property CurrentComment() As Comment
        Get
            Return _comment
        End Get
        Set(ByVal value As Comment)
            _comment = value
        End Set

    End Property



    Public Function AddNewComment(Optional ByVal loadInCurrentDS As Boolean = True) As Comment
        Comment.DeleteNewChildComment(Me)
        If loadInCurrentDS Then
            _comment = New Comment(Me.Dataset)
        Else
            _comment = New Comment
        End If

        _comment.PopulateWithDefaultValues(Me.CertificateId)
        _comment.CallerName = Me.CallerName
        _comment.ClaimId = Me.Id
        Select Case Me.ClaimActivityCode
            Case Nothing, ""
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__REPLACEMENT_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__REWORK
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__REWORK_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__LEGAL_GENERAL
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__LEGALGENERAL_RECORD_CREATED)
        End Select
        _comment.Comments = Me.ProblemDescription
        Return _comment
    End Function

    Public Sub AttachShippingInfo(ByVal objShippingInfo As ShippingInfo)
        ShippingInfo.DeleteNewChildShippingInfo(Me)
        objCurrentShippingInfo = New ShippingInfo(Me.Dataset)
        objCurrentShippingInfo.CopyFromThis(objShippingInfo)
        Me.ShippingInfoId = objCurrentShippingInfo.Id
        objCurrentShippingInfo.ClaimIDHasBeenObtained = True
    End Sub

    Public Function AddClaimAuthDetail(ByVal objID As Guid, Optional ByVal blnLoadByClaimID As Boolean = False, Optional ByVal blnMustReload As Boolean = False) As ClaimAuthDetail
        If objID.Equals(Guid.Empty) Then
            Dim objClaimAuthDetail As New ClaimAuthDetail(Me.Dataset, blnMustReload)
            Return objClaimAuthDetail
        Else
            Dim objClaimAuthDetail As New ClaimAuthDetail(objID, Me.Dataset, blnLoadByClaimID, blnMustReload)
            Return objClaimAuthDetail
        End If
    End Function

    Public ReadOnly Property ClaimCommentsList() As Comment.ClaimCommentList
        Get
            Return New Comment.ClaimCommentList(Me)
        End Get
    End Property

    Public Sub AddContactInfo(ByVal contactInfoID As Guid)
        If contactInfoID.Equals(Guid.Empty) Then
            _contactInfo = New ContactInfo(Me.Dataset)
        Else
            _contactInfo = New ContactInfo(contactInfoID, Me.Dataset)
        End If
    End Sub

    Public ReadOnly Property ClaimHistoryChildren() As ClaimHistory.ClaimHistoryList
        Get
            Return New ClaimHistory.ClaimHistoryList(Me)
        End Get
    End Property
#End Region

#Region "Equipment Management"
    Private _ClaimedEquipment As ClaimEquipment

    Public ReadOnly Property ClaimedEnrolledEquipments As List(Of ClaimEquipment)
        Get
            Dim equipmentList As New List(Of ClaimEquipment)

            If Not Me._ClaimedEquipment Is Nothing Then
                equipmentList.Add(Me._ClaimedEquipment)
            End If
            If Not Me._EnrolledEquipment Is Nothing Then
                equipmentList.Add(Me._EnrolledEquipment)
            End If

            Return equipmentList
        End Get

    End Property

    Public Property ClaimedEquipment As ClaimEquipment
        Get
            If Me._ClaimedEquipment Is Nothing Then
                Me._ClaimedEquipment = (From clmEquip As ClaimEquipment In Me.ClaimEquipmentChildren
                                        Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED) _
                                        AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                        Select clmEquip).FirstOrDefault()
            End If
            Return Me._ClaimedEquipment
        End Get
        Set(ByVal value As ClaimEquipment)
            _ClaimedEquipment = value
        End Set
    End Property

    Public ReadOnly Property ReplacementOptions As List(Of ClaimEquipment)
        Get
            Return (From clmEquip As ClaimEquipment In Me.ClaimEquipmentChildren
                    Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT_OPTION) _
                    AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                    Select clmEquip).ToList
        End Get
    End Property

    Private _EnrolledEquipment As ClaimEquipment
    Public Property EnrolledEquipment As ClaimEquipment
        Get
            If Me._EnrolledEquipment Is Nothing Then
                Me._EnrolledEquipment = (From clmEquip As ClaimEquipment In Me.ClaimEquipmentChildren
                                         Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__ENROLLED) _
                                         AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                         Select clmEquip).FirstOrDefault
            End If
            Return Me._EnrolledEquipment
        End Get
        Set(ByVal value As ClaimEquipment)
            _EnrolledEquipment = value
        End Set
    End Property

    Private _ReplacementEquipment As ClaimEquipment
    Public Property ReplacementEquipment As ClaimEquipment
        Get
            If Me._ReplacementEquipment Is Nothing Then
                Me._ReplacementEquipment = (From clmEquip As ClaimEquipment In Me.ClaimEquipmentChildren
                                            Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT) _
                                            AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                            Select clmEquip).FirstOrDefault
            End If
            Return Me._ReplacementEquipment
        End Get
        Set(ByVal value As ClaimEquipment)
            _EnrolledEquipment = value
        End Set
    End Property
    Public Sub CreateEnrolledEquipment()
        'Only one Claimed Equipment can be created
        If Me.EnrolledEquipment Is Nothing AndAlso
            Me.CertificateItem.IsEquipmentRequired AndAlso
            (Not Me.CertificateItem.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.CertificateItem.Model)) Then
            'Add Enrolled Equipment to the Claimed Equipment Children
            Dim EnrolledEquipmentBO As ClaimEquipment = Me.ClaimEquipmentChildren.GetNewChild
            EnrolledEquipmentBO.BeginEdit()
            EnrolledEquipmentBO.ClaimId = Me.Id
            EnrolledEquipmentBO.ClaimEquipmentDate = Me.CertificateItem.CreatedDate
            EnrolledEquipmentBO.ManufacturerId = Me.CertificateItem.ManufacturerId
            EnrolledEquipmentBO.Model = Me.CertificateItem.Model
            EnrolledEquipmentBO.SKU = Me.CertificateItem.SkuNumber
            EnrolledEquipmentBO.SerialNumber = Me.CertificateItem.SerialNumber
            EnrolledEquipmentBO.IMEINumber = Me.CertificateItem.IMEINumber
            'Price will be poulated at the time of creating Claim when the List price is resolved based on SKU
            If (Not Me.CertificateItem.EquipmentId.Equals(Guid.Empty)) Then
                EnrolledEquipmentBO.EquipmentId = Me.CertificateItem.EquipmentId
            End If
            EnrolledEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__ENROLLED)
            EnrolledEquipmentBO.EndEdit()
            EnrolledEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateClaimedEquipment(ByVal _ClaimedEquipment As ClaimEquipment)
        'Only one Claimed equipment can be created
        If Me.ClaimedEquipment Is Nothing AndAlso
                Not _ClaimedEquipment Is Nothing AndAlso
               Not (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC) Then
            Dim claimEquipmentBO As ClaimEquipment = Me.ClaimEquipmentChildren.GetNewChild()
            With _ClaimedEquipment
                claimEquipmentBO.BeginEdit()
                claimEquipmentBO.ClaimId = Me.Id
                claimEquipmentBO.ClaimEquipmentDate = Me.CreatedDate
                claimEquipmentBO.ManufacturerId = .ManufacturerId
                claimEquipmentBO.Model = .Model
                claimEquipmentBO.SKU = .SKU
                claimEquipmentBO.SerialNumber = .SerialNumber
                claimEquipmentBO.IMEINumber = .IMEINumber
                claimEquipmentBO.EquipmentId = .EquipmentId

                claimEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED)
                claimEquipmentBO.EndEdit()
            End With
            claimEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateReplaceClaimedEquipment(ByVal _ClaimedEquipment As ClaimEquipment)

        If Not _ClaimedEquipment Is Nothing AndAlso
               Not (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC) Then
            Dim claimEquipmentBO As ClaimEquipment = Me.ClaimEquipmentChildren.GetNewChild()
            With _ClaimedEquipment
                claimEquipmentBO.BeginEdit()
                claimEquipmentBO.ClaimId = Me.Id
                claimEquipmentBO.ClaimEquipmentDate = Me.CreatedDate
                claimEquipmentBO.ManufacturerId = .ManufacturerId
                claimEquipmentBO.Model = .Model
                claimEquipmentBO.SKU = .SKU
                claimEquipmentBO.SerialNumber = .SerialNumber
                claimEquipmentBO.IMEINumber = .IMEINumber
                claimEquipmentBO.EquipmentId = .EquipmentId

                claimEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED)
                claimEquipmentBO.EndEdit()
            End With
            claimEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateReplacementOptions()
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
            Dim ClaimedEquipment As ClaimEquipment
            ClaimedEquipment = Me.ClaimedEquipment
            If Not ClaimedEquipment Is Nothing AndAlso Not ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                Dim BROpt As List(Of BestReplacement.BestReplacementOptions) = BestReplacement.GetReplacementEquipments(Me.Dealer.BestReplacementGroupId, ClaimedEquipment.EquipmentId, 4)
                For Each obj As BestReplacement.BestReplacementOptions In BROpt
                    If Not obj.Manufacturer_id.Equals(Guid.Empty) And Not String.IsNullOrEmpty(obj.Model) Then
                        Dim clEquipObj As ClaimEquipment = Me.ClaimEquipmentChildren.GetNewChild
                        clEquipObj.BeginEdit()
                        clEquipObj.EquipmentId = obj.Equipment_id
                        clEquipObj.ManufacturerId = obj.Manufacturer_id
                        clEquipObj.ClaimId = Me.Id
                        clEquipObj.Priority = obj.Priority
                        clEquipObj.Model = obj.Model
                        clEquipObj.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT_OPTION)
                        clEquipObj.ClaimEquipmentDate = Me.CreatedDate
                        clEquipObj.EndEdit()
                        clEquipObj.Save()
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub UpdateEnrolledEquipment()
        If Not Me.EnrolledEquipment Is Nothing AndAlso
            LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
            Dim _Enrolledequipment As ClaimEquipment = Me.ClaimEquipmentChildren.GetChild(Me.EnrolledEquipment.Id)
            With _Enrolledequipment
                .BeginEdit()
                .ClaimEquipmentDate = Me.CertificateItem.CreatedDate
                .ManufacturerId = Me.CertificateItem.ManufacturerId
                .Model = Me.CertificateItem.Model
                .SKU = Me.CertificateItem.SkuNumber
                .SerialNumber = Me.CertificateItem.SerialNumber
                If (Not Me.CertificateItem.EquipmentId.Equals(Guid.Empty)) Then
                    .EquipmentId = Me.CertificateItem.EquipmentId
                Else
                    If Me.CertificateItem.VerifyEquipment() Then
                        .EquipmentId = Me.CertificateItem.EquipmentId
                    Else
                        'Equipment Not Defined
                    End If
                End If
                .EndEdit()
                .Save()
            End With
        End If
    End Sub

    'Public Sub UpdateClaimedEquipment(_ClaimedEquipment As ClaimEquipment)
    '    If Not Me.ClaimedEquipment Is Nothing AndAlso _
    '    LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
    '        Dim claimEquipmentBO As ClaimEquipment = Me.ClaimEquipmentChildren.GetChild(Me.ClaimedEquipment.Id)
    '        If Not claimEquipmentBO.EquipmentId = _ClaimedEquipment.EquipmentId OrElse _
    '            Not claimEquipmentBO.SerialNumber = _ClaimedEquipment.SerialNumber Then
    '            'we need to update the claimed equipment
    '            With claimEquipmentBO
    '                .BeginEdit()
    '                .ManufacturerId = _ClaimedEquipment.ManufacturerId
    '                .Model = _ClaimedEquipment.Model
    '                .SKU = _ClaimedEquipment.SKU
    '                .SerialNumber = _ClaimedEquipment.SerialNumber
    '                .EquipmentId = _ClaimedEquipment.EquipmentId
    '                .EndEdit()
    '                .Save()
    '            End With
    '        End If
    '    End If

    'End Sub

    'Public Sub UpdateBestReplacementOptions()
    '    If Not Me.ClaimedEquipment Is Nothing Then
    '        If Me.ClaimedEquipment.IsNew Then
    '            Me.CreateReplacementOptions()
    '        End If
    '    End If
    'End Sub

    Public Function IsEquipmentMisMatch() As Boolean
        'when a mismatch found function returns true 
        Dim retVal As Boolean = False
        If Me.CertificateItem.IsEquipmentRequired AndAlso Not Me.EnrolledEquipment Is Nothing AndAlso Not Me.EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Not Me.EnrolledEquipment.EquipmentBO.Id = Me.ClaimedEquipment.EquipmentBO.Id OrElse
                    Not Me.EnrolledEquipment.SerialNumber.Trim.ToUpper = Me.ClaimedEquipment.SerialNumber.Trim.ToUpper Then
                    retVal = True
                End If
            End If
        End If
        Return retVal
    End Function

    Public Function ValidateAndMatchClaimedEnrolledEquipments(ByRef comment As Comment) As Boolean
        Dim retval As Boolean = True '
        If Me.CertificateItem.IsEquipmentRequired Then
            If comment Is Nothing Then comment = Me.AddNewComment

            'Validate Claimed Equipment
            If Me.ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIMED_EQUIPMENT_NOT_CONFIGURED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_CLAIMED_EQUIPMENT_NOT_RESOLVED")
                retval = False
                Return retval
            Else
                'Validate Enrolled Equipment
                If Me.EnrolledEquipment Is Nothing OrElse Me.EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__ENROLLED_EQUIPMENT_NOT_CONFIGURED)
                    comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ENROLLED_EQUIPMENT_NOT_RESOLVED")
                    retval = False
                    Return retval
                Else
                    'Match Claimed and Enrolled
                    If Me.IsEquipmentMisMatch() Then
                        comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__MAKE_MODEL_IMEI_MISMATCH)
                        comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_MISMATCH)
                        retval = False
                        Return retval
                    End If
                End If
            End If
        End If


        Return retval

    End Function

#End Region

#Region "Time Zone"
    Public Sub HandelTimeZoneForClaimExtStatusDate()

        Dim objCompanyBO As Company = New Company(Me.CompanyId)
        If Me._claimStatusBO Is Nothing Then Exit Sub
        If Me._claimStatusBO.IsTimeZoneForClaimExtStatusDateDone Then Exit Sub
        If objCompanyBO.TimeZoneNameId.Equals(System.Guid.Empty) Then Exit Sub
        _claimStatusBO.HandelTimeZoneForClaimExtStatusDate(Me)

    End Sub
#End Region

#Region "Finalize"
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "Web Service Methods"
    ''' <summary>
    ''' Gets Claims based on Dealer and Serial Number (IMEI Number)
    ''' </summary>
    ''' <param name="dealer">Dealer Code</param>
    ''' <param name="serialNumber">Serial Number / IMEI Number of Device</param>
    ''' <returns><see cref="DataSet" /></returns>
    ''' <remarks></remarks>
    Public Shared Function LoadClaimsBySerialNumber(ByVal countryCode As String,
                                             ByVal companyCode As String,
                                             ByVal dealerCode As String,
                                             ByVal serialNumber As String) As DataSet

        Dim dal As New ClaimDAL
        Return dal.LoadClaimsBySerialNumber(countryCode, companyCode, dealerCode, serialNumber, ElitaPlusIdentity.Current.ActiveUser.Id)

    End Function

    ''' <summary>
    ''' Gets Claims based on Dealer and Serial Number (IMEI Number)
    ''' </summary>
    ''' <param name="dealer">Dealer Code</param>
    ''' <param name="imeiNumber">Serial Number / IMEI Number of Device</param>
    ''' <returns><see cref="DataSet" /></returns>
    ''' <remarks></remarks>
    Public Shared Function LoadClaimsByImeiNumber(ByVal countryCode As String,
                                             ByVal companyCode As String,
                                             ByVal dealerCode As String,
                                             ByVal imeiNumber As String) As DataSet

        Dim dal As New ClaimDAL
        Return dal.LoadClaimsByImeiNumber(countryCode, companyCode, dealerCode, imeiNumber, ElitaPlusIdentity.Current.ActiveUser.Id)

    End Function

    ''' <summary>
    ''' WS_CHLMobileSCPortal_GetCertClaimInfo : Gets Claims based on Company and Serial Number / Phone Number
    ''' </summary>
    ''' <param name="CompanyCode">Company Code</param>
    ''' <param name="LanguageId">Portal User's language</param>
    ''' <param name="SerialNumber">Serial Number of the certificate found on the certificate item</param>
    ''' <param name="PhoneNumber">Phone Number of the customer the certificate belongs</param>
    ''' <returns>Certificate_Info => Returns Certificate information</returns>
    ''' <returns>Claim_Info => Returns Claim information of the Certificate</returns>
    ''' <returns>Is_Valid => Returns if the data retrival is successfull</returns>
    ''' <remarks></remarks>
    Public Shared Function WS_CHLMobileSCPortal_GetCertClaimInfo(ByVal CompanyCode As String,
                                                                 ByVal SerialNumber As String,
                                                                 ByVal PhoneNumber As String,
                                                                 ByVal taxId As String,
                                                                 ByVal claimStatusCode As String,
                                                                 ByRef ErrorCode As String,
                                                                 ByRef ErrorMessage As String) As DataSet

        Dim dal As New ClaimDAL
        Return dal.WS_CHLMobileSCPortal_GetCertClaimInfo(CompanyCode,
                                                        ElitaPlusIdentity.Current.ActiveUser.Id,
                                                        ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                        SerialNumber,
                                                        PhoneNumber,
                                                        taxId,
                                                        claimStatusCode,
                                                        ErrorCode,
                                                        ErrorMessage)

    End Function

    Public Shared Function WS_SNMPORTAL_SA_CLAIMREPORT(ByVal companyCode As String,
                                                       ByVal serviceCenterCode As String,
                                                       ByVal countryIsoCode As String,
                                                       ByVal fromDate As Date,
                                                       ByVal endDate As Date,
                                                       ByVal extendedStatusCode As String,
                                                       ByVal dealerCode As String,
                                                       ByVal pageSize As Integer,
                                                       ByRef batchId As Guid,
                                                       ByRef totalRecordCount As Integer,
                                                       ByRef totalRecordsInQueue As Integer,
                                                       ByRef errorCode As String,
                                                       ByRef errorMessage As String) As DataSet

        Dim dal As New ClaimDAL
        Return dal.WS_SNMPORTAL_SA_CLAIMREPORT(companyCode,
                                               ElitaPlusIdentity.Current.ActiveUser.Id,
                                               ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                               serviceCenterCode,
                                               countryIsoCode,
                                               fromDate,
                                               endDate,
                                               extendedStatusCode,
                                               dealerCode,
                                               pageSize,
                                               batchId,
                                               totalRecordCount,
                                               totalRecordsInQueue,
                                               errorCode,
                                               errorMessage)

    End Function

    Public Shared Function WS_SNMPORTAL_SA_CLAIMREPORT_NextPage(ByVal batchId As Guid,
                                                                ByVal pageSize As Integer,
                                                                ByRef totalRecordCount As Integer,
                                                                ByRef totalRecordsInQueue As Integer,
                                                                ByRef errorCode As String,
                                                                ByRef errorMessage As String) As DataSet

        Dim dal As New ClaimDAL
        Return dal.WS_SNMPORTAL_SA_CLAIMREPORT_NextPage(batchId,
                                               ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                               pageSize,
                                               totalRecordCount,
                                               totalRecordsInQueue,
                                               errorCode,
                                               errorMessage)

    End Function
#End Region
#Region "UFI List"
    Public Function IsCustomerPresentInUFIList(ByVal CountryId As Guid, ByVal TaxID As String) As DataView 'REQ-5978
        Dim dal As New ClaimDAL
        Return dal.IsCustomerPresentInUFIList(CountryId, TaxID)
    End Function
#End Region

    Public Function IsCertPaymentExists(ByVal certId As Guid) As Boolean 'REQ-6026
        Dim dal As New ClaimDAL
        Return dal.IsCertPaymentExists(certId)
    End Function

    Public Function isConsequentialDamageAllowed(ByVal productCodeId As Guid) As Boolean
        Dim dal As New ClaimDAL
        Return dal.isConsequentialDamageAllowed(productCodeId)
    End Function

    Public Function IsServiceWarrantyValid(ByVal ClaimId As Guid) As Boolean
        Dim dal As New ClaimDAL
        Return dal.IsServiceWarrantyValid(ClaimId) 'checks if service warranty is valid
    End Function

End Class

#Region "Enums"

Public Enum BasicClaimStatus
    None
    Active
    Closed
    Denied
    Pending
End Enum

Public Enum ClaimAuthorizationType
    None
    [Single]
    Multiple
End Enum

#End Region


