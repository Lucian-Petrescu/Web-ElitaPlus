Imports System.Linq
Imports System.Reflection
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.DataAccess
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.Common

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
        Dataset = New DataSet
        Load()
    End Sub

    Protected Sub New(isDSCreator As Boolean)
        MyBase.New(isDSCreator)
        Dataset = New DataSet
        Load()
    End Sub

    'New BO attaching to a BO family
    Protected Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    'Existing BO
    Protected Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        OriginalFollowUpDate = GetShortDate(FollowupDate.Value)
    End Sub

    'Existing BO attaching to a BO family
    Friend Sub New(id As Guid, familyDS As DataSet, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(False)
        Dataset = familyDS
        Load(id, blnMustReload)
        If (FollowupDate IsNot Nothing) Then
            OriginalFollowUpDate = GetShortDate(FollowupDate.Value)
        End If

    End Sub

    Protected Sub New(claimNumber As String, compId As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(claimNumber, compId)
        OriginalFollowUpDate = GetShortDate(FollowupDate.Value)
    End Sub

    Protected Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Overridable Sub Load()
        Try
            Dim dal As New ClaimDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
            ReportedDate = Date.Today
            AuthorizedAmount = New Decimal(0)
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Overridable Sub Load(id As Guid, Optional ByVal blnMustReload As Boolean = False)
        Try
            Dim dal As New ClaimDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing

            If blnMustReload AndAlso Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
            End If

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

    Protected Overridable Sub Load(claimNumber As String, compId As Guid)
        Try
            Dim dal As New ClaimDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(claimNumber, dal.COL_NAME_CLAIM_NUMBER, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, claimNumber, compId)
                Row = FindRow(claimNumber, dal.COL_NAME_CLAIM_NUMBER, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(ClaimDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CertItemCoverageId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            If (Not CertItemCoverageId.Equals(Value)) Then
                SetValue(ClaimDAL.COL_NAME_CERT_ITEM_COVERAGE_ID, Value)
                CertificateItemCoverage = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property RiskType As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_RISK_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_RISK_TYPE), String)
            End If
        End Get

    End Property

    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    Public Overridable Property ClaimActivityId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID, Value)
        End Set
    End Property

    Public Overridable Property ReasonClosedId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REASON_CLOSED_ID, Value)
        End Set
    End Property

    Public Overridable Property RepairCodeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPAIR_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_REPAIR_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REPAIR_CODE_ID, Value)
        End Set
    End Property

    Public Overridable Property CauseOfLossId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property

    Public Property ServiceCenterId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property MethodOfRepairId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property

    <ValidLawsuitId("")>
    Public Property IsLawsuitId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_LAWSUIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_IS_LAWSUIT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_IS_LAWSUIT_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    Public Property LoanerCenterId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID), Byte()))
            End If
        End Get
        Private Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LOANER_CENTER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=500)>
    Public Property SpecialInstruction As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1), ValidReasonClosed(""),
     Obsolete("Backward Compatability - Replace this with ClaimBase.Status - Action - Change to Private")>
    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property ContactName As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CONTACT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CONTACT_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property CallerName As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CALLER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CALLER_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=500)>
    Public Property ProblemDescription As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property
    Public Property DeniedReasons As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DENIED_REASONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DENIED_REASONS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DENIED_REASONS, Value)
        End Set
    End Property

    Public Function EvaluatePreConditions(preConditionList As List(Of String)) As Boolean

        For Each preCondition As String In preConditionList

            Select Case preCondition.ToUpperInvariant()
                Case "DemandsClaimsImage".ToUpperInvariant()
                    If (ClaimImagesList.Count = 0) Then
                        Return False
                    End If
                Case Else
                    If (preCondition.ToUpper.StartsWith("DemandsPermission_".ToUpper())) Then
                        Dim permissionCode As String = preCondition.Substring(18)
                        If (ElitaPlusIdentity.Current.ActiveUser.UserPermission.Where(Function(up) up.PermissionId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_USER_ROLE_PERMISSION, ElitaPlusIdentity.Current.ActiveUser.LanguageId), permissionCode)).Count() = 0) Then
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
    Public Property TrackingNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    Public Property EmployeeNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER, Value)
        End Set
    End Property

    Public Property DeviceActivationDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE, Value)
        End Set
    End Property

    Public Property FulfilmentMethod As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD, Value)
        End Set
    End Property
    Public Property FulfillmentProviderType As FulfillmentProviderType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP) Is DBNull.Value Then
                Return Nothing
            Else
                Dim result As FulfillmentProviderType
                If [Enum].TryParse(Of FulfillmentProviderType)(Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP).ToString().GetXcdEnum(Of FulfillmentProviderType)(), result) Then
                    Return result
                Else
                    Return FulfillmentProviderType.Elita
                End If
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP, Value)
        End Set
    End Property
    Public Property BankInfoId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property


    Public Property LiabilityLimit As DecimalType
        Get
            CheckDeleted()
            Dim lLimit As Decimal = 0D

            If Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT) IsNot DBNull.Value Then
                lLimit = CType(Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT), Decimal)
            End If

            If (StatusCode.ToString <> Codes.CLAIM_STATUS__CLOSED AndAlso lLimit = 0 AndAlso (CDec(Certificate.ProductLiabilityLimit.ToString) > 0D OrElse CDec(CertificateItemCoverage.CoverageLiabilityLimit.ToString) > 0D)) Then
                Dim al As ArrayList = CalculateLiabilityLimit(CertificateId, Contract.Id, CertItemCoverageId, LossDate)
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
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LIABILITY_LIMIT, Value)
        End Set
    End Property

    Public Property Deductible As DecimalType
        Get
            CheckDeleted()
            Dim deduct As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return New DecimalType(deduct)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    Public Overridable Property ClaimClosedDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIM_CLOSED_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Overridable Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property ContactSalutationID As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_CONTACT_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_CONTACT_SALUTATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_CONTACT_SALUTATION_ID, Value)
        End Set
    End Property

    Public Property CallerSalutationID As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_CALLER_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_CALLER_SALUTATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_CALLER_SALUTATION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20), ValueMandatoryConditionally(""), CPF_TaxNumberValidation("")>
    Public Property CallerTaxNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CALLER_TAX_NUMBER, Value)
        End Set
    End Property

    Public Property DeductiblePercent As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT, Value)
        End Set
    End Property

    Public Property DeductiblePercentID As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_PERCENT_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property PolicyNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_POLICY_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_POLICY_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_POLICY_NUMBER, Value)
        End Set
    End Property

    Public Property Fraudulent As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FRAUDULENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_FRAUDULENT), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_FRAUDULENT, Value)
        End Set
    End Property

    Public Property DealerReference As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEALER_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DEALER_REFERENCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEALER_REFERENCE, Value)
        End Set
    End Property

    Public Property Pos As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_POS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_POS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_POS, Value)
        End Set
    End Property

    Public Property DeniedReasonId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DENIED_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DENIED_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DENIED_REASON_ID, Value)
        End Set
    End Property

    Public Property Complaint As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_COMPLAINT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_COMPLAINT), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_COMPLAINT, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ClaimAuthorizationTypeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID), Byte()))
            End If
        End Get
        Friend Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property MethodOfRepairCode As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListCache.LK_METHODS_OF_REPAIR, New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte())))
            End If
        End Get

    End Property

    <ValidStringLength("", Max:=20)>
    Public Property MasterClaimNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidDateOfLoss("")>
    Public Property LossDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_LOSS_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LOSS_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property DefaultFollowUpDays As LongType
        Get
            Return Company.DefaultFollowupDays
        End Get

    End Property

    Private ReadOnly Property MfgDeductible(ds As DataSet) As DecimalType
        Get
            Dim dr As DataRow
            If ds.Tables(ClaimDAL.TABLE_NAME_MFG_DEDUCT).Rows.Count > 0 Then
                dr = ds.Tables(ClaimDAL.TABLE_NAME_MFG_DEDUCT).Rows(0)
            End If
            CheckDeleted()
            Dim deduct As Decimal = 0D
            If dr IsNot Nothing AndAlso dr(ClaimDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return New DecimalType(deduct)
            Else
                If dr IsNot Nothing Then
                    Return New DecimalType(CType(dr(ClaimDAL.COL_NAME_DEDUCTIBLE), Decimal))
                Else
                    Return Nothing
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property DeductibleByMfgFlag As Boolean
        Get
            Try
                If Contract.DeductibleByManufacturerId.Equals(Guid.Empty) Then
                    Return False
                Else
                    Dim code As String = LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Contract.DeductibleByManufacturerId)
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

    Public Property DedCollectionMethodID As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DED_COLLECTION_METHOD_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=6)>
    Public Property DedCollectionCCAuthCode As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DED_COLLECTION_CC_AUTH_CODE, Value)
        End Set
    End Property

    Public Property IsComingFromDenyClaim As Boolean
        Get
            Return moIsComingFromDenyClaim
        End Get
        Set
            moIsComingFromDenyClaim = Value
        End Set
    End Property

    Public Overridable Property IsRequiredCheckLossDateForCancelledCert As Boolean
        Get
            Return _IsLossDateCheckforCancelledCert
        End Get
        Set
            _IsLossDateCheckforCancelledCert = Value
        End Set
    End Property

    Public Property DeductibleCollected As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DEDUCTIBLE_COLLECTED, Value)
        End Set
    End Property

    Public Property AuthorizedAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal))
            End If
        End Get
        Protected Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, Value)
        End Set
    End Property

    Public Property PoliceReport As PoliceReport
        Get
            Return _policeReport
        End Get
        Set
            _policeReport = value
        End Set
    End Property

    <ValidReportedDate("")>
    Public Property ReportedDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPORTED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_REPORTED_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REPORTED_DATE, Value)
        End Set
    End Property

    <ValidFollowupDate(""), ValueMandatory("")>
    Public Property FollowupDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FOLLOWUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_FOLLOWUP_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_FOLLOWUP_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ClaimActivityCode As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListCache.LK_CLAIM_ACTIVITIES, New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ACTIVITY_ID), Byte())))
            End If
        End Get

    End Property

    Public ReadOnly Property ClaimActivityDescription As String
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

    Public ReadOnly Property CauseOfLoss As String
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

    Public ReadOnly Property MethodOfRepairDescription As String
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
    Public Property NewDeviceSku As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_NEW_DEVICE_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_NEW_DEVICE_SKU), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_NEW_DEVICE_SKU, Value)
        End Set
    End Property

    Public Property SalvageAmount As DecimalType
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
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_SALVAGE_AMOUNT, Value)
        End Set
    End Property

    Public Property DiscountAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DISCOUNT_AMOUNT, Value)
        End Set
    End Property

    Public Property DiscountPercent As LongType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DISCOUNT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimDAL.COL_NAME_DISCOUNT_PERCENT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_DISCOUNT_PERCENT, Value)
        End Set
    End Property

    Public ReadOnly Property OutstandingPremiumAmount As DecimalType
        Get
            _outstandingAmount = CalculateOutstandingPremiumAmount()
        End Get
    End Property

    Public ReadOnly Property AuthorizationLimit As DecimalType
        Get
            Return (ElitaPlusIdentity.Current.ActiveUser.AuthorizationLimit(CompanyId))
        End Get

    End Property

    Public ReadOnly Property IsAuthorizationLimitExceeded As Boolean
        Get
            If AuthorizedAmount IsNot Nothing AndAlso AuthorizedAmount.Value > AuthorizationLimit.Value Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property IsCoverageForTheft As Boolean
        Get
            Dim flag As Boolean = False
            If CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                  OrElse CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
                  OrElse CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper Then

                flag = True
            End If
            Return flag
        End Get
    End Property

    Public Property IsUpdatedComment As Boolean
        Get
            Return _isUpdatedComment
        End Get
        Set
            _isUpdatedComment = Value
        End Set
    End Property

    Public Property IsLocked As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_LOCKED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_IS_LOCKED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_IS_LOCKED, Value)
        End Set
    End Property

    Public Property lockedOn As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOCKED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LOCKED_ON), DateType)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LOCKED_ON, Value)
        End Set
    End Property

    Public Property LockedBy As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOCKED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_LOCKED_BY), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LOCKED_BY, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ClaimsAdjuster As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER, Value)
        End Set
    End Property

    Public Property ClaimsAdjusterName As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CLAIMS_ADJUSTER_NAME, Value)
        End Set
    End Property

    Public Property AuthDetailDataHasChanged As Boolean
        Get
            Return _AuthDetailDataHasChanged
        End Get
        Set
            _AuthDetailDataHasChanged = Value
        End Set
    End Property

    Public moGalaxyClaimNumberList As ArrayList = Nothing

    Public ReadOnly Property CurrentShippingInfo As ShippingInfo
        Get
            Return objCurrentShippingInfo
        End Get
    End Property

    Public ReadOnly Property MaxFollowUpDays As LongType
        Get
            Return (Company.MaxFollowupDays)
        End Get
    End Property

    Public Property OriginalFollowUpDate As Date
        Get
            Return _originalFollowUpDate
        End Get
        Set
            _originalFollowUpDate = Value
        End Set
    End Property

    Public Property ContactInfoId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CONTACT_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CONTACT_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_CONTACT_INFO_ID, Value)
        End Set
    End Property

    Public Property RepairCode As String
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_REPAIR_CODE) Is DBNull.Value) OrElse (RepairCodeId.Equals(Guid.Empty))) Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REPAIR_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REPAIR_CODE, Value)
        End Set
    End Property

    Public Property RepairShortDesc As String
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC) Is DBNull.Value) OrElse (RepairCodeId.Equals(Guid.Empty))) Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REPAIR_SHORT_DESC, Value)
        End Set

    End Property

    Public ReadOnly Property NotificationTypeId As Guid
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID) Is DBNull.Value)) Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_NOTIFICATION_TYPE_ID), Byte()))
            End If
        End Get

    End Property

    Public ReadOnly Property NotificationTypeDescription As String
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

    Public Property LastOperatorName As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME) Is DBNull.Value Then
                Return (UserName)
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_LAST_OPERATOR_NAME, Value)
        End Set

    End Property

    Public ReadOnly Property UserName As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_USER_NAME), String)
            End If
        End Get

    End Property

    Public Property MobileNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MOBILE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MOBILE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_MOBILE_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property SerialNumber As String
        Get
            If ClaimedEquipment Is Nothing Then
                Return Nothing
            Else
                Return ClaimedEquipment.SerialNumber
            End If
        End Get
    End Property

    Public ReadOnly Property ClaimStatusesCount As Integer
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_STATUSES_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIM_STATUSES_COUNT), Integer)
            End If
        End Get

    End Property

    Public ReadOnly Property LatestClaimStatus As ClaimStatus
        Get
            If ClaimStatusesCount > 0 Then
                Return ClaimStatus.GetLatestClaimStatus(Id)
            Else
                Return Nothing
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property MgrAuthAmountFlag As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()

            If ((Value = Nothing) OrElse (Value.ToUpper <> "Y")) Then
                'Added logic to default to "N"  if value is nothing value is different than "Y", "y"
                SetValue(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG, "N")
            Else
                SetValue(ClaimDAL.COL_NAME_MGR_AUTH_AMOUNT_FLAG, Value)
            End If
        End Set
    End Property

    Public ReadOnly Property ReasonClosed As String
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

    Public ReadOnly Property ReasonClosedCode As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListCache.LK_REASONS_CLOSED, New Guid(CType(Row(ClaimDAL.COL_NAME_REASON_CLOSED_ID), Byte())))
            End If
        End Get
    End Property

    Public ReadOnly Property AssurantPays As DecimalType
        Get

            Dim assurPays As Decimal = 0D
            Dim liabLimit As Decimal = LiabilityLimit.Value

            If DiscountPercent IsNot Nothing Then
                DiscountAmount = AuthorizedAmount * (CType(DiscountPercent, Decimal) / 100)
            End If

            If (liabLimit = 0D AndAlso CType(Certificate.ProductLiabilityLimit.ToString, Decimal) = 0 AndAlso CType(CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0) Then
                liabLimit = 999999999.99
            End If

            Trace("AA_DEBUG", "Claim Detail", "Claim Id =" & GuidControl.GuidToHexString(Id) &
                          "@Claim = " & ClaimNumber)

            If (AuthorizedAmount > liabLimit) Then
                assurPays = liabLimit - IIf(Deductible Is Nothing, New Decimal(0D), Deductible) - IIf(SalvageAmount, New Decimal(0D), SalvageAmount)
            Else
                assurPays = AuthorizedAmount.Value - IIf(Deductible Is Nothing, New Decimal(0D), Deductible) - IIf(SalvageAmount Is Nothing, New Decimal(0D), SalvageAmount)

            End If
            If MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
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
            Dim authValue As Decimal = AuthorizedAmount.Value
            Dim assurPays As Decimal = Decimal.Subtract(AssurantPays.Value, BonusAmount)
            Dim ded As Decimal = Deductible.Value
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

    Public ReadOnly Property ConsumerPays As DecimalType
        Get
            Dim cPays As Decimal = 0D
            Dim aPays As Decimal = AssurantPays.Value
            Dim sal As Decimal = SalvageAmount.Value
            If (CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) > aPays) Then
                cPays = CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) - aPays - sal
            End If

            Return New DecimalType(cPays)
        End Get
    End Property

    Public ReadOnly Property AboveLiability As DecimalType
        Get
            ' Dim liabLimit As Decimal = CType(Row(ClaimDAL.COL_NAME_LIABILITY_LIMIT), Decimal)
            Dim liabLimit As Decimal = LiabilityLimit.Value
            Dim abovLiability As Decimal = 0D


            If (liabLimit = 0D AndAlso CType(Certificate.ProductLiabilityLimit.ToString, Decimal) = 0 AndAlso CType(CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0) Then
                liabLimit = 999999999.99
            End If

            If (CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) > liabLimit) Then
                abovLiability = CType(Row(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal) - liabLimit
            End If
            Return New DecimalType(abovLiability)
        End Get

    End Property

    Public ReadOnly Property getSalutationDescription(salutationid As Guid) As String
        Get
            Dim dv As DataView = LookupListNew.GetSalutationLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return LookupListNew.GetDescriptionFromId(dv, salutationid)
        End Get

    End Property

    Private _NumberOfDisbursements As Nullable(Of Integer)
    Private _TotalPaid As DecimalType = New DecimalType(0)

    Private Sub PopulateDisbursementSummary()
        If Not _NumberOfDisbursements.HasValue AndAlso Not IsNew Then
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

    Public ReadOnly Property TotalPaid As DecimalType
        Get
            PopulateDisbursementSummary()
            Return _TotalPaid
        End Get
    End Property

    Private _TotalPaidForCert As DecimalType = New DecimalType(0)
    Public ReadOnly Property TotalPaidForCert As DecimalType
        Get
            If _TotalPaidForCert.Equals(New DecimalType(0)) Then
                _TotalPaidForCert = getTotalPaidForCert(CertificateId)
                Return _TotalPaidForCert
            Else
                Return _TotalPaidForCert
            End If
        End Get
    End Property

    Protected Property PickUpDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PICKUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_PICKUP_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_PICKUP_DATE, Value)
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

    Protected Property RepairDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ClaimDAL.COL_NAME_REPAIR_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property DealerTypeCode As String
        Get
            If _DealerTypeCode Is Nothing Then
                _DealerTypeCode = LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, Dealer.DealerTypeId)
            End If
            Return _DealerTypeCode
        End Get
    End Property

    Protected Property Bonus As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Protected Property BonusTax As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property

    Public ReadOnly Property IsClaimChild As String
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
    Public Property Purchase_Price As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property

    'REQ-6230
    Public Property IndixId As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_INDIX_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(ClaimDAL.COL_NAME_INDIX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_INDIX_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=1)>
    Public ReadOnly Property IsClaimReadOnly As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_IS_CLAIM_READ_ONLY) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimDAL.COL_NAME_IS_CLAIM_READ_ONLY), String)
            End If
        End Get
    End Property

    Public Property RemAuthNumber As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REM_AUTH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_REM_AUTH_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REM_AUTH_NUMBER, Value)
        End Set
    End Property
#End Region

#Region "MustOverride & overridable  Properties"
    Public MustOverride ReadOnly Property IsDaysLimitExceeded As Boolean
    Public MustOverride ReadOnly Property IsMaxSvcWrtyClaimsReached As Boolean
#End Region

#Region "Non-Persistent Properties"

#Region "Work Queue"
    Private _wqItem As WorkQueueItem
    Public Property CurrentWorkQueueItem As WorkQueueItem
        Get
            Return _wqItem
        End Get
        Set
            _wqItem = value
        End Set
    End Property
#End Region

#End Region

#Region "Derived Properties"
    Public Property Status As BasicClaimStatus
        Get
            Select Case StatusCode
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
        Set
            Select Case value
                Case BasicClaimStatus.Pending
                    StatusCode = Codes.CLAIM_STATUS__PENDING
                Case BasicClaimStatus.Active
                    StatusCode = Codes.CLAIM_STATUS__ACTIVE
                Case BasicClaimStatus.Closed
                    StatusCode = Codes.CLAIM_STATUS__CLOSED
                Case BasicClaimStatus.Denied
                    StatusCode = Codes.CLAIM_STATUS__DENIED
                Case Else
                    Throw New NotSupportedException()
            End Select
        End Set
    End Property

    Public ReadOnly Property ClaimAuthorizationTypeCode As String
        Get
            If (ClaimAuthorizationTypeId.Equals(Guid.Empty)) Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(LookupListCache.LK_CLAIM_AUTHORIZATION_TYPE, ClaimAuthorizationTypeId)
            End If
        End Get
    End Property

    Public Property ClaimAuthorizationType As ClaimAuthorizationType
        Get
            Select Case ClaimAuthorizationTypeCode
                Case Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE
                    Return ClaimAuthorizationType.Single
                Case Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE
                    Return ClaimAuthorizationType.Multiple
                Case Else
                    Return BasicClaimStatus.None
            End Select
        End Get
        Private Set
            Select Case value
                Case ClaimAuthorizationType.Single
                    ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE)
                Case ClaimAuthorizationType.Multiple
                    ClaimAuthorizationTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE)
                Case Else
                    Throw New NotSupportedException()
            End Select
        End Set
    End Property

#Region "Claim Authorization"
    Public ReadOnly Property ClaimAuthorizationChildren As ClaimAuthorizationList
        Get
            Return New ClaimAuthorizationList(Me)
        End Get
    End Property

#End Region

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.CertNumber - Action - Remove")>
    Public ReadOnly Property CertificateNumber As String
        Get
            If (Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Certificate.CertNumber
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.CustomerName - Action - Remove")>
    Public ReadOnly Property CustomerName As String
        Get
            If (Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Certificate.CustomerName
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Certificate.Id - Action - Remove")>
    Public Overridable ReadOnly Property CertificateId As Guid
        Get
            If (Certificate Is Nothing) Then
                Return Nothing
            Else
                Return Certificate.Id
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Dealer.Dealer - Action - Remove")>
    Public ReadOnly Property DealerCode As String
        Get
            If (Dealer Is Nothing) Then
                Return Nothing
            Else
                Return Dealer.Dealer
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.Dealer.DealerName - Action - Remove")>
    Public ReadOnly Property DealerName As String
        Get
            If (Dealer Is Nothing) Then
                Return Nothing
            Else
                Return Dealer.DealerName
            End If
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.CertificateItemCoverage.CoverageTypeId - Action - Remove")>
    Public ReadOnly Property CoverageTypeId As Guid
        Get
            Return CertificateItemCoverage.CoverageTypeId
        End Get
    End Property

    Public ReadOnly Property CoverageTypeCode As String
        Get
            Return LookupListNew.GetCodeFromId(LookupListCache.LK_COVERAGE_TYPES, CertificateItemCoverage.CoverageTypeId)
        End Get
    End Property

    Public ReadOnly Property CoverageTypeDescription As String
        Get
            Return LookupListNew.GetDescriptionFromId(LookupListCache.LK_COVERAGE_TYPES, CertificateItemCoverage.CoverageTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get
    End Property

    ''TODO: Remove the code
    <Obsolete("Backward Compatability - Replace this with ClaimBase.CreatedDate - Action - Remove", True)>
    Public ReadOnly Property CreationDate As DateType
        Get
            Return CreatedDate
        End Get
    End Property

    <Obsolete("Backward Compatability - Replace this with ClaimBase.ModifiedDate - Action - Remove")>
    Public ReadOnly Property LastModifiedDate As DateType
        Get
            Return ModifiedDate
        End Get

    End Property

    Private _deductibleType As CertItemCoverage.DeductibleType
    Public ReadOnly Property DeductibleType As CertItemCoverage.DeductibleType
        Get
            If (_deductibleType Is Nothing) Then
                _deductibleType = CertItemCoverage.GetDeductible(CertItemCoverageId, MethodOfRepairId)
            End If
            Return _deductibleType
        End Get
    End Property

    Public Property ShippingInfoId As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SHIPPING_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_SHIPPING_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_SHIPPING_INFO_ID, Value)
        End Set
    End Property

    Public ReadOnly Property BonusAmount As DecimalType
        Get
            CheckDeleted()
            Return New Decimal.Add(Bonus, BonusTax)
        End Get
    End Property

    Public Property CurrentRetailPrice As DecimalType
        Get
            CheckDeleted()
            Dim CurrentRetailPriceVal As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE) Is DBNull.Value Then
                Return New DecimalType(CurrentRetailPriceVal)
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimDAL.COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE, Value)
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
                If Dealer IsNot Nothing Then
                    Me.Company = New Company(Dealer.CompanyId, Dataset)
                ElseIf Certificate IsNot Nothing Then
                    Dealer = New Dealer(Certificate.DealerId, Dataset)
                    Me.Company = New Company(Dealer.CompanyId, Dataset)
                ElseIf CertificateItem IsNot Nothing Then
                    Certificate = New Certificate(CertificateItem.CertId, Dataset)
                    Dealer = New Dealer(Certificate.DealerId, Dataset)
                    Me.Company = New Company(Dealer.CompanyId, Dataset)
                Else
                    CertificateItem = New CertItem(CertificateItemCoverage.CertItemId, Dataset)
                    Certificate = New Certificate(CertificateItem.CertId, Dataset)
                    Dealer = New Dealer(Certificate.DealerId, Dataset)
                    Me.Company = New Company(Dealer.CompanyId, Dataset)
                End If
            End If
            Return _company
        End Get
        Private Set
            _company = value
        End Set
    End Property

    Public Property Dealer As Dealer
        Get
            If (_dealer Is Nothing) Then
                If Certificate IsNot Nothing Then
                    Me.Dealer = New Dealer(Certificate.DealerId, Dataset)
                ElseIf CertificateItem IsNot Nothing Then
                    Certificate = New Certificate(CertificateItem.CertId, Dataset)
                    Me.Dealer = New Dealer(Certificate.DealerId, Dataset)
                Else
                    CertificateItem = New CertItem(CertificateItemCoverage.CertItemId, Dataset)
                    Certificate = New Certificate(CertificateItem.CertId, Dataset)
                    Me.Dealer = New Dealer(Certificate.DealerId, Dataset)
                End If
            End If
            Return _dealer
        End Get
        Private Set
            _dealer = value
            Company = Nothing
        End Set
    End Property

    Public Property Certificate As Certificate
        Get
            If (_certificate Is Nothing) Then
                If CertificateItem IsNot Nothing Then
                    Me.Certificate = New Certificate(CertificateItem.CertId, Dataset)
                Else
                    CertificateItem = New CertItem(CertificateItemCoverage.CertItemId, Dataset)
                    Me.Certificate = New Certificate(CertificateItem.CertId, Dataset)
                End If
            End If
            Return _certificate
        End Get
        Private Set
            _certificate = value
            Dealer = Nothing
            Company = Nothing
        End Set
    End Property

    Public Property CertificateItem As CertItem
        Get
            If (_certItem Is Nothing) Then
                If CertificateItemCoverage IsNot Nothing Then
                    Me.CertificateItem = New CertItem(CertificateItemCoverage.CertItemId, Dataset)
                End If
            End If
            Return _certItem
        End Get
        Private Set
            _certItem = value
            Certificate = Nothing
            Dealer = Nothing
            Company = Nothing
        End Set
    End Property

    Public Function GetCertRegisterItemId(ClaimID As Guid) As Guid
        Try
            Dim dal As New CertRegisteredItemDAL
            Return dal.GetCertRegisterItemId(ClaimID)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetCertRegisterItemIdByMasterNumber(ClaimNumber As String, CompanyId As Guid) As Guid
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
                If Not CertItemCoverageId.Equals(Guid.Empty) Then
                    Me.CertificateItemCoverage = New CertItemCoverage(CertItemCoverageId, Dataset)
                End If
            End If
            Return _certificateItemCoverage
        End Get
        Private Set
            _certificateItemCoverage = value
            CertificateItem = Nothing
            Certificate = Nothing
            Dealer = Nothing
            Company = Nothing
        End Set
    End Property

    Public ReadOnly Property Contract As Contract
        Get
            If (_contract Is Nothing) Then
                _contract = New Contract(Contract.GetContractID(CertificateId))
            End If
            Return _contract
        End Get
    End Property

    Public Property ContactInfo As ContactInfo
        Get
            If (_contactInfo Is Nothing AndAlso Not ContactInfoId.Equals(Guid.Empty)) Then
                _contactInfo = New ContactInfo(ContactInfoId, Dataset)
            End If
            Return _contactInfo
        End Get
        Set
            _contactInfo = value
        End Set
    End Property

    Public ReadOnly Property CaseInfo As CaseBase
        Get
            If (_case Is Nothing) Then
                _case = New CaseBase(CaseBase.LoadCaseByClaimId(Id))
            End If
            Return _case
        End Get
    End Property



#End Region

#Region "Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)

            If claimBaseObject.IsNew _
              AndAlso LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, claimBaseObject.Company.CompanyTypeId) = Company.COMPANY_TYPE_INSURANCE _
              AndAlso claimBaseObject.CallerTaxNumber Is Nothing Then
                Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidLawsuitId
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_NOT_ALLOWED_FOR_MFG_COVERAGE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)

            If claimBaseObject.IsNew _
                AndAlso LookupListNew.GetCodeFromId(LookupListCache.LK_COVERAGE_TYPES, claimBaseObject.CoverageTypeId) = Codes.COVERAGE_TYPE__MANUFACTURER Then
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CPF_DOCUMENT_NUMBER_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim claimBaseObject As ClaimBase = CType(objectToValidate, ClaimBase)
            Dim dal As New ClaimDAL
            Dim oErrMess As String

            Try
                If Not claimBaseObject.IsNew Then Return True

                If Not LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, claimBaseObject.Company.CompanyTypeId) = Company.COMPANY_TYPE_INSURANCE Then
                    Return True
                End If

                If claimBaseObject.CallerTaxNumber Is Nothing Then Return True ' the ValueMandatoryConditionally will catch this validation

                'DEF-1012
                If claimBaseObject.Certificate.DocumentTypeID.Equals(Guid.Empty) Then
                    Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                    Return False
                End If
                Dim docTypeCode = LookupListNew.GetCodeFromId(LookupListCache.LK_DOCUMENT_TYPES, claimBaseObject.Certificate.DocumentTypeID)

                oErrMess = dal.ExecuteSP(docTypeCode, claimBaseObject.CallerTaxNumber)
                If oErrMess IsNot Nothing Then
                    Message = UCase(oErrMess)
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REASON_CLOSED_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_DATE_OF_LOSS_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim objClaim As ClaimBase = CType(objectToValidate, ClaimBase)

            If objClaim.LossDate Is Nothing Then Return True

            'if the claim is closed, then no need to do validations for loss date
            If objClaim.Status = BasicClaimStatus.Closed Then Return True

            'if modifying existing denied claim, then no need to do validation since this date is not editable on the screen
            If Not objClaim.IsComingFromDenyClaim And objClaim.Status = BasicClaimStatus.Denied Then Return True

            'if backend claim then both dates will be not null...these dates must be >=date of loss
            If GetType(Claim).Equals(objClaim.GetType()) Then
                Dim objC = CType(objClaim, Claim)
                If objC.RepairDate IsNot Nothing And objC.PickUpDate IsNot Nothing Then
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
                    Not LookupListNew.GetCodeFromId(LookupListCache.LK_DENIED_REASON, objClaim.DeniedReasonId) = Codes.REASON_DENIED__INCORRECT_DEVICE_SELECTED)) Then
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
        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPORT_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            Dim reportDate As Date
            If obj.LossDate IsNot Nothing Then
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
                    Message = Common.ErrorCodes.INVALID_REPORT_DATE_ERR
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_FOLLOWUP_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "SKU_INVALID_MISSING")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimBase = CType(objectToValidate, ClaimBase)

            If (obj.NewDeviceSku IsNot Nothing) AndAlso (obj.NewDeviceSku.Trim <> String.Empty) Then
                Dim deductible As DecimalType
                If Not ListPrice.IsSKUValid(obj.Dealer.Id, obj.NewDeviceSku.Trim, obj.LossDate.Value, deductible) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class

#End Region

#Region "Claim Equipment"

    Public ReadOnly Property ClaimEquipmentChildren As ClaimEquipment.ClaimEquipmentList
        Get
            Return New ClaimEquipment.ClaimEquipmentList(Me)
        End Get
    End Property

    Public Function GetEquipmentChild(childId As Guid) As ClaimEquipment
        Return CType(ClaimEquipmentChildren.GetChild(childId), ClaimEquipment)
    End Function

    Public Function GetNewEquipmentChild() As ClaimEquipment
        Dim newClaimEquipment As ClaimEquipment = CType(ClaimEquipmentChildren.GetNewChild, ClaimEquipment)
        newClaimEquipment.ClaimId = Id
        Return newClaimEquipment
    End Function
#End Region

#Region "Claim Issues"
    Public ReadOnly Property ClaimIssuesList As ClaimIssue.ClaimIssueList
        Get
            Return New ClaimIssue.ClaimIssueList(Me)
        End Get
    End Property

    Public ReadOnly Property IssuesStatus As String
        Get
            Return EvaluateIssues()
        End Get
    End Property

    Public ReadOnly Property HasIssues As Boolean
        Get
            If (ClaimIssuesList.Count > 0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property AllIssuesResolvedOrWaived As Boolean
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

        For Each Item As ClaimIssue In ClaimIssuesList
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
    Public ReadOnly Property IssueDeniedReason As String
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
    Public Property DealerIssues As DataView
        Get
            If (_dealerIssues Is Nothing) Then
                _dealerIssues = LoadIssues()
            End If
            Return _dealerIssues
        End Get
        Set
            _dealerIssues = value
        End Set
    End Property

    Public Function Load_Filtered_Issues() As DataView

        Dim claimIssue As ClaimIssue
        'Issues Already added cannot be added again
        For Each claimIssue In ClaimIssuesList
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
        Dim oDealer As New Dealer(CompanyId, DealerCode)
        Dim dvRules As DataView = Rule.GetRulesByDealerAndCompany(oDealer.Id, CompanyId)
        Dim dealerIssues As DataTable = CreateIssuesDataTable()
        Dim oCompany As New Company(CompanyId)

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
                    If (CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper) Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "RQPRPT", "THFDOCRL"
                    If CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFTLOSS.ToUpper _
                   OrElse CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__THEFT.ToUpper _
                   OrElse (CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__LOSS.ToUpper _
                   AndAlso ((New Company(CompanyId)).PoliceRptForLossCovId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)))) Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "TRBSHTRL"
                    If CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__ACCIDENTAL.ToUpper _
                    OrElse CoverageTypeCode.ToUpper = Codes.COVERAGE_TYPE__MECHANICAL_BREAKDOWN.ToUpper Then
                        addIssue = True
                    Else
                        addIssue = False
                    End If
                Case "CLMDOCRL", "DEDCOLRL"
                    addIssue = True
                Case "UPGRDRL"
                    If LossDate.Value > Certificate.WarrantySalesDate.Value.AddMonths(12) Then
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

        If Not MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
            issueList.RowFilter = condition
        End If
        For Each row As DataRowView In issueList
            Select Case (CType(row("RULE_CODE"), String))
                Case "RQPRPT", "TRBSHTRL", "CLMDOCRL", "UPGRDRL", "DEDCOLRL", "CLMADJ", "CMPLARGRL", "LATAMHOLDCLM", "THFDOCRL"
                    Dim issue As New Issue(GuidControl.ByteArrayToGuid(row("ISSUE_ID")))
                    Dim newClaimIssue As ClaimIssue = CType(ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(Id, issue.Id, CertificateId, True)

            End Select

        Next


    End Sub

    Public Sub LoadResponses()
        For Each claimIssue As ClaimIssue In ClaimIssuesList

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
                            propInfo = [GetType]().GetProperty(entAttr.Entity)
                            obj = propInfo.GetValue(Me, Nothing)
                            objType = propInfo.PropertyType

                            If obj Is Nothing Then
                                obj = objType.GetConstructor(New Type() {GetType(Guid), GetType(DataSet), GetType(Boolean)}).Invoke(New Object() {Id, Dataset, False})
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
        If Not IsNew Then
            WaiveExistingIssues()
        End If

        Dim t As DataTable = ClaimIssuesView.CreateTable
        Dim detail As ClaimIssue
        Dim filteredTable As DataTable

        Try

            For Each detail In ClaimIssuesList
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

        dal.WaiveExistingIssues(Id, Certificate.IdentificationNumber, COMPLIANCE_RULE_CODE, COMPLIANCE_ISSUE_CODE, "Y")

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


        Public Sub New(Table As DataTable)
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

    Public Function AttachImage(pDocumentTypeId As Nullable(Of Guid),
                                pImageStatusId As Nullable(Of Guid),
                                pScanDate As Nullable(Of Date),
                                pFileName As String,
                                pComments As String,
                                pUserName As String,
                                pImageData As Byte()) As Guid

        Dim validationErrors As New List(Of ValidationError)

        ' Check if Document Type ID is supplied otherwise default to Other
        If (Not pDocumentTypeId.HasValue) Then
            pDocumentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER)
        End If

        ' Check if Image Status ID is supplied otherwise default to Pending
        If (Not pImageStatusId.HasValue) Then
            pImageStatusId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PENDING)
        End If

        ' Check if Scan Date is supplied otherwise default to Current Date 
        If (Not pScanDate.HasValue) Then
            pScanDate = DateTime.Today
        End If

        Dim oClaimImage As ClaimImage
        oClaimImage = DirectCast(ClaimImagesList.GetNewChild(Id), ClaimImage)

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

            Dim oRepository As Documents.Repository = Company.GetClaimImageRepository()
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

            PublishedTask.AddEvent(companyGroupId:=Company.CompanyGroupId,
                                   companyId:=Dealer.Company.id,
                                   countryId:=Company.CountryId,
                                   dealerId:=Dealer.Id,
                                   productCode:=String.Empty,
                                   coverageTypeId:=Guid.Empty,
                                   sender:=CLAIM_DOC_UPLD_DETAILS,
                                   arguments:="ClaimId:" & DALBase.GuidToSQLString(Id) & ";DocumentTypeId:" & DALBase.GuidToSQLString(oClaimImage.DocumentTypeId) & "",
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

            For Each detail In ClaimImagesList(loadAllFiles)
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
                If (_ft IsNot Nothing) Then
                    row(ClaimImagesView.COL_FILE_TYPE) = _ft.Description
                End If

                If (detail.FileSizeBytes IsNot Nothing) Then
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
        Public Sub New(Table As DataTable)
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
    Public ReadOnly Property ReplacementPartChildren As ReplacementPartList
        Get
            Return New ReplacementPartList(Me)
        End Get
    End Property

    Public Function GetReplacementPartChild(childId As Guid) As ReplacementPart
        Return CType(ReplacementPartChildren.GetChild(childId), ReplacementPart)
    End Function

    Public Function GetNewReplacementPartChild() As ReplacementPart
        Dim newReplacementPart As ReplacementPart = CType(ReplacementPartChildren.GetNewChild, ReplacementPart)
        newReplacementPart.ClaimId = Id
        Return newReplacementPart
    End Function
#End Region
#Region "Replacement Items"
    Public Function GetReplacementItem(childId As Guid) As ClaimEquipment.ReplacementEquipmentDV
        Return ClaimEquipment.GetReplacementItemInfo(childId)
    End Function

    Public Function GetReplacementItemStatus(childId As Guid, equipmentId As Guid) As ClaimEquipment.ReplacementItemStatusDV
        Return ClaimEquipment.GetReplacementItemStatus(childId, equipmentId)
    End Function

#End Region

#Region "Case Id"
    Public Shared Function GetCaseIdByCaseNumberAndCompany(CaseNumber As String, CompanyCode As String) As Guid
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
    Public ReadOnly Property ClaimShippingList As ClaimShipping.ClaimShippingList
        Get
            Return New ClaimShipping.ClaimShippingList(Me)
        End Get
    End Property
#End Region

#Region "Instance Methods"

    Public Sub PrePopulate(certItemCoverageId As Guid, mstrClaimNumber As String, DateOfLoss As DateType, Optional ByVal RecoveryButtonClick As Boolean = False, Optional ByVal WsUse As Boolean = False, Optional ByVal ComingFromDenyClaim As Boolean = False, Optional ByVal comingFromCert As Boolean = False, Optional ByVal callerName As String = Nothing, Optional ByVal problemDescription As String = Nothing, Optional ByVal ReportedDate As DateType = Nothing, Optional ByVal clmEquipment As ClaimEquipment = Nothing)

        Me.CertItemCoverageId = certItemCoverageId
        Status = BasicClaimStatus.Active
        Me.CertItemCoverageId = certItemCoverageId
        CompanyId = Certificate.CompanyId
        RiskTypeId = CertificateItem.RiskTypeId

        If (CertificateItemCoverage.MethodOfRepairId = Guid.Empty) Then
            MethodOfRepairId = Certificate.MethodOfRepairId
        Else
            MethodOfRepairId = CertificateItemCoverage.MethodOfRepairId
        End If

        If RecoveryButtonClick = True Then
            MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__RECOVERY)
        End If

        If MethodOfRepairId <> LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__RECOVERY) Then
            If CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFTLOSS _
                OrElse CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT _
                OrElse CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__LOSS Then
                MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)
            End If
        End If
        '''''''''''''''''''''''''''''''''''''''
        If LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, Company.CompanyTypeId) = Me.Company.COMPANY_TYPE_INSURANCE Then
            Try
                If LookupListNew.GetCodeFromId(LookupListCache.LK_DOCUMENT_TYPES, Certificate.DocumentTypeID) = Codes.DOCUMENT_TYPE__CNPJ Then
                    Me.CallerName = Nothing
                    CallerTaxNumber = Nothing
                Else
                    Me.CallerName = Certificate.CustomerName
                    CallerTaxNumber = Certificate.IdentificationNumber
                End If
            Catch ex As Exception
                Me.CallerName = Nothing
                CallerTaxNumber = Nothing
            End Try
        Else
            Me.CallerName = Certificate.CustomerName
            CallerTaxNumber = Certificate.IdentificationNumber
        End If

        If callerName IsNot Nothing Then
            Me.CallerName = callerName
        End If

        '''''''''''''''''''''''''''''''''''''''
        ContactName = Certificate.CustomerName

        Select Case MethodOfRepairCode
            Case Codes.METHOD_OF_REPAIR__REPLACEMENT
                ClaimActivityId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
            Case Codes.METHOD_OF_REPAIR__LEGAL, Codes.METHOD_OF_REPAIR__GENERAL
                ClaimActivityId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__LEGAL_GENERAL)
        End Select

        MasterClaimNumber = mstrClaimNumber
        LossDate = DateOfLoss

        'REQ 1106 start
        CreateEnrolledEquipment()
        If clmEquipment IsNot Nothing Then
            CreateClaimedEquipment(clmEquipment)
            CreateReplacementOptions()
        ElseIf (Not CertificateItem.IsEquipmentRequired _
                AndAlso CertificateItem IsNot Nothing _
                AndAlso LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
            'build claimed equipment data from cert Item and create claimed equipment 
            CreateClaimedEquipment(CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
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

        FollowupDate = New DateType(Date.Now.AddDays(DefaultFollowUpDays.Value))

        ClaimClosedDate = Nothing

        If (Not WsUse) Then Calculate_Discounts(CertificateItemCoverage)

        If (ComingFromDenyClaim) Then IsComingFromDenyClaim = True

        If comingFromCert AndAlso DeductibleByMfgFlag Then
            Dim ds As DataSet = LoadMfgDeductible(Me.CertItemCoverageId)
            If MfgDeductible(ds) IsNot Nothing Then
                Deductible = MfgDeductible(ds)
            End If
        End If

        If problemDescription IsNot Nothing Then
            Me.ProblemDescription = problemDescription
        End If

        If ReportedDate IsNot Nothing Then
            Me.ReportedDate = ReportedDate.Value
        End If



        'Attach Issues to Claim based on Rules associated with the dealer
        AttachIssues()
        LoadResponses()

    End Sub
    <Obsolete("Tech Debt - to support deductible calculation for Argentina dealers when deductible is based on expression")>
    Public Sub TechDebtCalculateDeductible()
        If MethodOfRepairCode <> MethodofRepairCodes.Replacement Then
            Deductible = AuthorizedAmount.Value * m_NewClaimRepairDedPercent / 100
        Else
            Deductible = AuthorizedAmount.Value * m_NewClaimOrigReplDedPercent / 100
        End If
    End Sub

    Public Sub PrepopulateDeductible()
        Dim oDeductible As CertItemCoverage.DeductibleType
        Dim moCertItemCvg As CertItemCoverage
        Dim moCertItem As CertItem
        Dim moCert As Certificate
        Dim listPriceDeductible As DecimalType

        oDeductible = CertItemCoverage.GetDeductible(CertItemCoverageId, MethodOfRepairId)

        If (oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT And
            oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
            moCertItemCvg = New CertItemCoverage(CertItemCoverageId)
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
                    DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
                    Deductible = New DecimalType(0D)
                    DeductiblePercent = oDeductible.DeductiblePercentage
                    Calculate_deductible_if_by_percentage()
                Else
                    DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                    Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__FIXED
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                Deductible = oDeductible.Deductible
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                Deductible = GetDecimalValue(moCertItem.ItemRetailPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                Deductible = GetDecimalValue(moCertItem.OriginalRetailPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                Deductible = GetDecimalValue(moCert.SalesPrice) * oDeductible.DeductiblePercentage / 100
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                If LossDate IsNot Nothing Then
                    listPriceDeductible = ListPrice.GetListPrice(moCert.DealerId, If(CertificateItem.IsEquipmentRequired, ClaimedEquipment.SKU, moCertItem.SkuNumber), LossDate.Value.ToString("yyyyMMdd"))
                    If (listPriceDeductible <> Nothing) Then
                        Deductible = listPriceDeductible.Value * oDeductible.DeductiblePercentage / 100
                    Else
                        Deductible = New DecimalType(0D)
                    End If
                Else
                    Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                If LossDate IsNot Nothing Then
                    listPriceDeductible = ListPrice.GetListPrice(moCert.DealerId, If(CertificateItem.IsEquipmentRequired, ClaimedEquipment.SKU, moCertItem.SkuNumber), moCert.WarrantySalesDate.Value.ToString("yyyyMMdd"))
                    If (listPriceDeductible <> Nothing) Then
                        Deductible = listPriceDeductible.Value * oDeductible.DeductiblePercentage / 100
                    Else
                        Deductible = New DecimalType(0D)
                    End If
                Else
                    Deductible = New DecimalType(0D)
                End If
            Case Codes.DEDUCTIBLE_BASED_ON__EXPRESSION
                Dim cachefacade As New CacheFacade()

                Dim manager As New CommonManager(cachefacade)
                Dim listPrice As Nullable(Of Decimal) = Nothing
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
                DeductiblePercent = New DecimalType(0D)

                ''For New claims if deductible is based on expression then deductible is 30% for Repair and 50% REplacement of Auth amount
                If IsNew Then
                    Dim attvalue As AttributeValue = Company.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.COMP_ATTR__TECH_DEBT_DEDUCTIBLE_RULE).FirstOrDefault
                    If attvalue IsNot Nothing AndAlso attvalue.Value = Codes.YESNO_Y Then
                        TechDebtCalculateDeductible()
                    Else
                        Deductible = Serialize(Of BaseExpression)(
                        manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                        Evaluate(Function(variableName)
                                     Select Case variableName.ToUpperInvariant()
                                         Case "ListPrice".ToUpperInvariant()
                                             If (Not listPrice.HasValue) Then
                                                 listPrice = GetListPrice()
                                             End If
                                             Return listPrice.Value
                                         Case "SalesPrice".ToUpperInvariant()
                                             Return Certificate.SalesPrice
                                         Case "OrigRetailPrice".ToUpperInvariant()
                                             Return moCertItem.OriginalRetailPrice
                                         Case "ItemRetailPrice".ToUpperInvariant()
                                             Return moCertItem.ItemRetailPrice
                                         Case "LossType".ToUpperInvariant()
                                             Return String.Empty ''''Loss type is not passed from UI
                                         Case "AuthorizedAmount".ToUpperInvariant()
                                             Return AuthorizedAmount
                                         Case "DeductibleBasePrice".ToUpperInvariant()

                                             Dim dvPrice As DataView = GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                                  ServiceTypeCodes.DeductibleBasePrice)
                                             If dvPrice IsNot Nothing AndAlso dvPrice.Count > 0 Then
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

                    Deductible = Serialize(Of BaseExpression)(
                    manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                    Evaluate(Function(variableName)
                                 Select Case variableName.ToUpperInvariant()
                                     Case "ListPrice".ToUpperInvariant()
                                         If (Not listPrice.HasValue) Then
                                             listPrice = GetListPrice()
                                         End If
                                         Return listPrice.Value
                                     Case "SalesPrice".ToUpperInvariant()
                                         Return Certificate.SalesPrice
                                     Case "OrigRetailPrice".ToUpperInvariant()
                                         Return moCertItem.OriginalRetailPrice
                                     Case "ItemRetailPrice".ToUpperInvariant()
                                         Return moCertItem.ItemRetailPrice
                                     Case "LossType".ToUpperInvariant()
                                         Return String.Empty ''''Loss type is not passed from UI
                                     Case "AuthorizedAmount".ToUpperInvariant()
                                         Return AuthorizedAmount
                                     Case "DeductibleBasePrice".ToUpperInvariant()

                                         Dim dvPrice As DataView = GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                            ServiceTypeCodes.DeductibleBasePrice)
                                         If dvPrice IsNot Nothing AndAlso dvPrice.Count > 0 Then
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
                DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
                Deductible = New DecimalType(0D)
            Case Else
        End Select

    End Sub

    Public Function GetPricesForServiceType(serviceClassCode As String, serviceTypeCode As String) As DataView

        Dim dv As DataView
        Dim servCenter As New ServiceCenter(ServiceCenterId)
        Dim equipConditionid As Guid
        Dim equipmentId As Guid
        Dim equipClassId As Guid

        'get the equipment information'if equipment not used then get the prices based on risktypeid
        If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListCache.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If ClaimedEquipment IsNot Nothing AndAlso ClaimedEquipment.EquipmentBO IsNot Nothing Then
                equipmentId = ClaimedEquipment.EquipmentId
                equipClassId = ClaimedEquipment.EquipmentBO.EquipmentClassId
                dv = PriceListDetail.GetPricesForServiceType(CompanyId, servCenter.Code, RiskTypeId,
                              DateTime.Now, Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Dealer.Id, String.Empty)
            End If
        Else
            dv = PriceListDetail.GetPricesForServiceType(CompanyId, servCenter.Code, RiskTypeId,
                              DateTime.Now, Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Dealer.Id, String.Empty)
        End If

        Return dv

    End Function

    Private Sub Calculate_Discounts(certItemCoverage As CertItemCoverage)

        Me.DiscountPercent = 0
        DiscountAmount = CType(0, DecimalType)

        If MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN OrElse
           MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME Then
            DiscountPercent = certItemCoverage.RepairDiscountPct
        ElseIf MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN Then
            DiscountPercent = certItemCoverage.ReplacementDiscountPct
        End If

    End Sub

    Protected Function LoadMfgDeductible(oCertItemCoverageId As Guid) As DataSet
        Try
            Dim dal As New CertItemDAL
            Dim ds As New DataSet
            dal.LoadMfgDeductible(ds, CertificateItemCoverage.CertItemId, Contract.Id)
            If ds.Tables(CertItemDAL.TABLE_NAME_MFG_DEDUCT).Rows.Count > 1 Then
                Throw New DataNotFoundException
            End If
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Function GetDecimalValue(decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Public Sub Calculate_deductible_if_by_percentage()
        Dim authAmount As Decimal = New Decimal(0D)
        If Me.ClaimAuthorizationType = BusinessObjectsNew.ClaimAuthorizationType.Single Then
            authAmount = AuthorizedAmount.Value
        Else
            authAmount = CalculateAuthorizedAmountForDeductible()
        End If

        If LiabilityLimit.Value > 0 Then
            If LiabilityLimit.Value > authAmount Then
                Deductible = New DecimalType((authAmount * DeductiblePercent.Value) / 100)
            Else
                Deductible = New DecimalType((LiabilityLimit.Value * DeductiblePercent.Value) / 100)
            End If
        End If
        If LiabilityLimit.Value = 0 Then
            Deductible = New DecimalType((authAmount * DeductiblePercent.Value) / 100)
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

    Public Sub Trace(id As String, screen As String, msg As String)
        AppConfig.DebugMessage.Trace(id, screen, msg & "@ SCount = none")
    End Sub

    Private Function CalculateOutstandingPremiumAmount() As Decimal
        Dim outAmt As Decimal = 0
        If Contract.PayOutstandingPremiumId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            Dim dv As DataView
            Dim dvBilling As DataView
            Dim grossAmountReceived As Decimal
            Dim oBillingTotalAmount As Decimal

            dv = Certificate.PremiumTotals(CertificateId)
            grossAmountReceived = CType(dv.Table.Rows(0).Item(Certificate.COL_TOTAL_GROSS_AMT_RECEIVED), Decimal)

            If Dealer.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                dvBilling = BillingPayDetail.getBillPayTotals(CertificateId)
                oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
            Else
                dvBilling = BillingDetail.getBillingTotals(CertificateId)
                oBillingTotalAmount = CType(dvBilling.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
            End If

            outAmt = grossAmountReceived - oBillingTotalAmount
        End If
    End Function

    <Obsolete("TECH_DEBT_AUTH_AMT_PERCENT-to support deductible calculation when it is based on percentage of Auth Amount")>
    Public Sub RecalculateDeductibleForChanges()
        If MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
            Dim al As ArrayList = CalculateLiabilityLimit(CertificateId, Contract.Id, CertItemCoverageId, LossDate)
            If MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                If Deductible.Value = 0 Then
                    If CType(al(1), Integer) = 0 Then
                        LiabilityLimit = CType(al(0), Decimal)
                        If DeductiblePercent IsNot Nothing Then
                            If LiabilityLimit.Value > 0 Then
                                If LiabilityLimit.Value > AuthorizedAmount.Value Then
                                    Deductible = New DecimalType((AuthorizedAmount.Value * DeductiblePercent.Value) / 100)
                                Else
                                    Deductible = New DecimalType((LiabilityLimit.Value * DeductiblePercent.Value) / 100)
                                End If
                            End If
                            If LiabilityLimit.Value = 0 Then
                                Deductible = New DecimalType((AuthorizedAmount.Value * DeductiblePercent.Value) / 100)
                            End If
                        End If
                    End If
                End If
            Else
                If CType(al(1), Integer) = 0 Then
                    LiabilityLimit = CType(al(0), Decimal)
                    If DeductiblePercent IsNot Nothing Then
                        If LiabilityLimit.Value > 0 Then
                            If LiabilityLimit.Value > AuthorizedAmount.Value Then
                                Deductible = New DecimalType((AuthorizedAmount.Value * DeductiblePercent.Value) / 100)
                            Else
                                Deductible = New DecimalType((LiabilityLimit.Value * DeductiblePercent.Value) / 100)
                            End If
                        End If
                        If LiabilityLimit.Value = 0 Then
                            Deductible = New DecimalType((AuthorizedAmount.Value * DeductiblePercent.Value) / 100)
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

        oDeductible = CertItemCoverage.GetDeductible(CertItemCoverageId, MethodOfRepairId)

        If (oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT And
            oDeductible.DeductibleBasedOn <> Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
            moCertItemCvg = New CertItemCoverage(CertItemCoverageId)
            moCertItem = New CertItem(moCertItemCvg.CertItemId)
            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE Or
                oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD) Then
                moCert = New Certificate(moCertItemCvg.CertId)
            End If
        End If

        Dim al As ArrayList = CalculateLiabilityLimit(CertificateId, Contract.Id, CertItemCoverageId, LossDate)

        Select Case oDeductible.DeductibleBasedOn
            Case Codes.DEDUCTIBLE_BASED_ON__EXPRESSION

                If CType(al(1), Integer) = 0 Then
                    LiabilityLimit = CType(al(0), Decimal)
                End If
                If Not IsNew Then
                    Dim cachefacade As New CacheFacade()

                    Dim manager As New CommonManager(cachefacade)
                    Dim listPrice As Nullable(Of Decimal) = Nothing
                    DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
                    DeductiblePercent = New DecimalType(0D)
                    Deductible = Serialize(Of BaseExpression)(
                            manager.GetExpression(oDeductible.ExpressionId.Value).ExpressionXml).
                            Evaluate(Function(variableName)
                                         Select Case variableName.ToUpperInvariant()
                                             Case "ListPrice".ToUpperInvariant()
                                                 If (Not listPrice.HasValue) Then
                                                     listPrice = GetListPrice()
                                                 End If
                                                 Return listPrice.Value
                                             Case "SalesPrice".ToUpperInvariant()
                                                 Return Certificate.SalesPrice
                                             Case "OrigRetailPrice".ToUpperInvariant()
                                                 Return moCertItem.OriginalRetailPrice
                                             Case "ItemRetailPrice".ToUpperInvariant()
                                                 Return moCertItem.ItemRetailPrice
                                             Case "LossType".ToUpperInvariant()
                                                 Return String.Empty ''''Loss type is not passed from UI
                                             Case "AuthorizedAmount".ToUpperInvariant()
                                                 Return AuthorizedAmount
                                             Case "DeductibleBasePrice".ToUpperInvariant()

                                                 Dim dvPrice As DataView = GetPricesForServiceType(ServiceClassCodes.Deductible,
                                                                                      ServiceTypeCodes.DeductibleBasePrice)
                                                 If dvPrice IsNot Nothing AndAlso dvPrice.Count > 0 Then
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

    Public Function IsMaxReplacementExceeded(CertID As Guid, CurrentLossDate As Date, Optional ByVal blnExcludeSelf As Boolean = True) As Boolean
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

        If Contract.ClaimLimitBasedOnId.Equals(Guid.Empty) Then
            Return False
        Else
            Dim strClaimNum As String = String.Empty
            If blnExcludeSelf AndAlso (ClaimNumber IsNot Nothing) Then
                strClaimNum = ClaimNumber
            End If

            Dim ReplacementBasedOn As String
            ReplacementBasedOn = LookupListNew.GetCodeFromId(LookupListCache.LK_REPLACEMENT_BASED_ON, Contract.ClaimLimitBasedOnId)
            If ReplacementBasedOn = Codes.REPLACEMENT_BASED_ON__INSURANCE_ACTIVATION_DATE Then
                If Certificate.InsuranceActivationDate IsNot Nothing Then
                    CurrentLossDate = GetStartDateOf12MonthWindow(Certificate.InsuranceActivationDate.Value, CurrentLossDate)
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

    Private Function GetStartDateOf12MonthWindow(dtStart As Date, dtCurrent As Date) As Date
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
        Dim comment As Comment = AddNewComment()
        'Change status to Active and run the rules
        If Me.Status = BasicClaimStatus.Pending Then
            Status = BasicClaimStatus.Active
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_APPROVED)
        End If

        'Check for Rules for which claim could be denied
        CheckForDenyingRules(comment)

        'Check for Rules for which claim could go to Pending status
        If (Me.Status = BasicClaimStatus.Active) Then CheckForPendingRules(comment)

    End Function

    Public Sub CheckForPendingRules(ByRef comment As Comment)
        Dim flag As Boolean = True
        If (comment Is Nothing) Then comment = AddNewComment

        If Not Certificate.IsSubscriberStatusValid Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID)
        End If

        If Contract.PayOutstandingPremiumId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) AndAlso OutstandingPremiumAmount.Value > 0 Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_PAYMENT_ON_OUTSTANDING_PREMIUM)
        End If

        ''''New Equipment Flow starts here
        If CertificateItem.IsEquipmentRequired Then
            If (Not ValidateAndMatchClaimedEnrolledEquipments(comment)) Then
                flag = flag And False
            End If
        End If
        '''''

        ' Check if Deductible Calculation Method is List and SKU Price is resolved
        If (DeductibleType.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
            Dim lstPrice As DecimalType = GetListPrice()
            If (lstPrice Is Nothing) Then
                flag = flag And False
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
            End If
        End If

        If (Dealer.DeductibleCollectionId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If DedCollectionMethodID = LookupListNew.GetIdFromCode(LookupListCache.LK_DED_COLL_METHOD, Codes.DED_COLL_METHOD_DEFFERED_COLL) Then
                flag = flag And False
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                comment.Comments = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.GUI_DEDUCTIBLE_NOT_COLLECTED)
            End If
        End If

        If (HasIssues AndAlso Not AllIssuesResolvedOrWaived) Then
            flag = flag And False
            comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
            comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
        End If

        If (Not flag) Then Status = BasicClaimStatus.Pending

    End Sub

    Public Sub CheckForDenyingRules(ByRef comment As Comment)
        Dim flag As Boolean = True
        If (comment Is Nothing) Then comment = AddNewComment

        Dim blnExceedMaxReplacements As Boolean = False
        Dim blnClaimReportedWithinPeriod As Boolean = True
        Dim blnClaimReportedWithinGracePeriod As Boolean = True
        Dim blnCoverageTypeNotMissing As Boolean = True

        Dim objCert As New Certificate(CertificateId)
        Dim oDealer As New Dealer(objCert.DealerId)


        If objCert.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso oDealer.IsGracePeriodSpecified Then

            If IsNew Then
                If Not ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                    blnCoverageTypeNotMissing = IsClaimReportedWithValidCoverage(CertificateId, CertItemCoverageId, LossDate.Value, ReportedDate.Value)
                End If
            End If

            If IsNew Then
                If Not ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then
                    blnClaimReportedWithinGracePeriod = IsClaimReportedWithinGracePeriod(CertificateId, CertItemCoverageId, LossDate.Value, ReportedDate.Value)
                End If
            End If

            If blnClaimReportedWithinGracePeriod And blnCoverageTypeNotMissing Then
                If IsNew Then
                    blnExceedMaxReplacements = IsMaxReplacementExceeded(CertificateId, LossDate.Value)
                End If
            End If
        Else
            If IsNew Then 'only check the condition for new claim
                blnExceedMaxReplacements = IsMaxReplacementExceeded(CertificateId, LossDate.Value)
                If Not ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK Then ' Not a Valid Condition for Service Warranty Claims
                    blnClaimReportedWithinPeriod = IsClaimReportedWithinPeriod(CertificateId, LossDate.Value, ReportedDate.Value)
                End If
            End If
        End If

        'Add comments to indicate that the claim will be closed

        If objCert.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso oDealer.IsGracePeriodSpecified Then


            If Not blnCoverageTypeNotMissing Then
                flag = flag And False
                DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__COVERAGE_TYPE_MISSING)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_COVERAGE_TYPE_MISSING)

            ElseIf Not blnClaimReportedWithinGracePeriod Then
                flag = flag And False
                DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__NOT_REPORTED_WITHIN_GRACE_PERIOD)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_NOT_WITHIN_GRACE_PERIOD)


            ElseIf blnExceedMaxReplacements Then
                flag = flag And False
                ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__REPLACEMENT_EXCEED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)

            ElseIf (EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                flag = flag And False
                If IssueDeniedReason IsNot Nothing Then
                    DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListCache.LK_DENIED_REASON, IssueDeniedReason())
                Else
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                End If
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")


            ElseIf (CType(CertificateItemCoverage.CoverageLiabilityLimit.ToString, Decimal) > 0) Then
                If (CoverageRemainLiabilityAmount(CertItemCoverageId, LossDate) <= 0) Then
                    flag = flag And False
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If

            ElseIf (CType(Certificate.ProductLiabilityLimit.ToString, Decimal) > 0) Then
                If (ProductRemainLiabilityAmount(CertificateId, LossDate) <= 0) Then
                    flag = flag And False
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If

        Else

            If blnExceedMaxReplacements Then
                flag = flag And False
                ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__REPLACEMENT_EXCEED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
            End If

            If Not blnClaimReportedWithinPeriod Then
                flag = flag And False
                ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__NOT_REPORTED_WITHIN_PERIOD)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_EXCEED)
            End If
            If (EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                flag = flag And False
                If IssueDeniedReason IsNot Nothing Then
                    DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListCache.LK_DENIED_REASON, IssueDeniedReason())
                Else
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                End If

                'flag = flag And False
                'Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
            End If

            If (CType(CertificateItemCoverage.CoverageLiabilityLimit.ToString, Decimal) > 0) Then
                If (CoverageRemainLiabilityAmount(CertItemCoverageId, LossDate) <= 0) Then
                    flag = flag And False
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
            If (CType(Certificate.ProductLiabilityLimit.ToString, Decimal) > 0) Then
                If (ProductRemainLiabilityAmount(CertificateId, LossDate) <= 0) Then
                    flag = flag And False
                    DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED)
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                End If
            End If
        End If


        If (Not flag) Then
            ClaimClosedDate = New DateType(System.DateTime.Today)
            ClaimActivityId = Guid.Empty
            Status = BasicClaimStatus.Denied
        End If

    End Sub

    Private Function GetListPrice() As DecimalType
        Dim lstPrice As DecimalType
        Dim strSku As String = NewDeviceSku
        If strSku = String.Empty Then strSku = CertificateItem.SkuNumber
        If CertificateItem.IsEquipmentRequired Then strSku = ClaimedEquipment.SKU
        lstPrice = ListPrice.GetListPrice(Certificate.DealerId, strSku, LossDate.Value.ToString("yyyyMMdd"))
        Return lstPrice
    End Function

    Public Sub CalculateFollowUpDate()
        Try
            FollowupDate = New DateType(NonbusinessCalendar.GetNextBusinessDate(DefaultFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        Catch ex As Exception
        End Try
    End Sub

    Public Overridable Sub CreateClaim()
        CheckForRules()
        'REQ-1106
        'If (Me.DeductibleType.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
        '    'REQ-918 Get the Claim Equipment Object for this Claim and Assign the above Price to it
        '    If Not Me.State.claimEquipmentBO Is Nothing Then
        '        Me.State.claimEquipmentBO.Price = lstPrice
        '        Me.State.claimEquipmentBO.Save()
        '    End If

        'End If
        CalculateFollowUpDate()
        IsUpdatedComment = True
    End Sub

    Public Overridable Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oldStatus As String = StatusCode

        Dim triggerEvent As Boolean

        '' Check if Record is New
        If Not Row.HasVersion(DataRowVersion.Original) Then
            '' New Claim Record is being Created
            '' PBI 494630 - Enabled for PENDING status 
            triggerEvent = True
        Else
            If IsClaimStatusChanged() AndAlso (StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE) Or StatusCode.Equals(Codes.CLAIM_STATUS__DENIED)) Then
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
            LockedBy = Guid.Empty
            lockedOn = Nothing
            IsLocked = Codes.YESNO_N
            ''claim lock - End            

            If Not IsNew Then                
                If GetDealerDRPTradeInQuoteFlag(DealerCode) Then
                    If IsStatusChngdFromPendingOrClosedToActive() Then
                        If VerifyIMEIWithDRPSystem(SerialNumber) Then
                            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, Nothing, Common.ErrorCodes.ACTIVE_TRADEIN_QUOTE_EXISTS_ERR)
                        End If
                    End If
                End If
            End If
            MyBase.Save()

            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                HandelTimeZoneForClaimExtStatusDate()
                Dim dal As New ClaimDAL
                UpdateFamily(Dataset)
                UpdateClaimBeforeSave()

                If (Dataset.Tables.Contains(EntityIssueDAL.TABLE_NAME)) Then
                    For Each dr As DataRow In Dataset.Tables(EntityIssueDAL.TABLE_NAME).Rows
                        Dim oClaimIssue As New ClaimIssue(dr)
                        oClaimIssue.ProcessWorkQueueItem(CurrentWorkQueueItem)
                    Next
                End If

                'When Saving Claim Update the status of claim Image
                For Each ClaimImage As ClaimImage In ClaimImagesList
                    ClaimImage.ImageStatusId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PROCESSED)
                Next

                dal.UpdateFamily(Dataset,
                                 IsUpdatedComment,
                                 moGalaxyClaimNumberList,
                                 New PublishedTaskDAL.PublishTaskData(Of ClaimIssueDAL.PublishTaskClaimData) With
                                             {
                                                 .CompanyGroupId = Company.CompanyGroupId,
                                                 .CompanyId = Company.Id,
                                                 .CountryId = Company.CountryId,
                                                 .CoverageTypeId = CertificateItemCoverage.CoverageTypeId,
                                                 .DealerId = Dealer.Id,
                                                 .ProductCode = Certificate.ProductCode,
                                                 .Data = New ClaimIssueDAL.PublishTaskClaimData() With
                                                         {
                                                             .ClaimId = Id,
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
                                 AuthDetailDataHasChanged,
                                 moIsUpdatedMasterClaimComment,
                                 ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If


                If triggerEvent Then
                    Dim eventTypeID As Guid

                    If StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_APPROVED)
                    ElseIf StatusCode.Equals(Codes.CLAIM_STATUS__DENIED) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_DENIED)
                    ElseIf StatusCode.Equals(Codes.CLAIM_STATUS__PENDING) Then
                        eventTypeID = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLAIM_REFERRED)
                    End If

                    PublishedTask.AddEvent(
                        companyGroupId:=Company.CompanyGroupId,
                        companyId:=Company.Id,
                        countryId:=Company.CountryId,
                        dealerId:=Dealer.Id,
                        productCode:=Certificate.ProductCode,
                        coverageTypeId:=CertificateItemCoverage.CoverageTypeId,
                        sender:="Claim Elita UI",
                        arguments:="ClaimId:" & DALBase.GuidToSQLString(Id),
                        eventDate:=DateTime.UtcNow,
                        eventTypeId:=eventTypeID,
                        eventArgumentId:=Guid.Empty)

                End If

            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            StatusCode = oldStatus
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex, ex.Code)
        End Try
    End Sub
    Private Function IsClaimStatusChanged() As Boolean
        If Row.HasVersion(DataRowVersion.Current) AndAlso
          (Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) <> Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current)) Then
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

    Public Shared Function ProductRemainLiabilityAmount(CertId As Guid, lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.ProductRemainLiabilityAmount(CertId, lossDate)
    End Function
    Public Shared Function CoverageRemainLiabilityAmount(CertItemCoverageId As Guid, lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.CoverageRemainLiabilityAmount(CertItemCoverageId, lossDate)
    End Function

    Public Sub Cancel()
        StatusCode = Codes.CLAIM_STATUS__CLOSED
        ClaimClosedDate = New DateType(System.DateTime.Today)
        ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED)
        Dim c As Comment = AddNewComment()
        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PENDING_CLAIM_NOT_APPROVED)
    End Sub

    Public Sub DenyClaim()
        StatusCode = Codes.CLAIM_STATUS__DENIED
        ClaimClosedDate = New DateType(System.DateTime.Today)
        'Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED)
        Dim c As Comment = AddNewComment()
        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
        ClaimActivityId = Guid.Empty
    End Sub

    Public Function GetLatestServiceOrder(Optional claimAuthID As Guid = Nothing) As ServiceOrder
        Dim serviceOrderBO As ServiceOrder = Nothing
        Try
            Dim serviceOrderID As Guid = ServiceOrder.GetLatestServiceOrderID(Id, claimAuthID)
            serviceOrderBO = New ServiceOrder(serviceOrderID)
        Catch ex As Exception
        End Try
        Return serviceOrderBO
    End Function

    Public Overridable Sub ReopenClaim()
        CalculateFollowUpDate()
        ReasonClosedId = Guid.Empty
        Status = BasicClaimStatus.Active
        ClaimClosedDate = Nothing
        If ClaimNumber.ToUpper.EndsWith("R") AndAlso ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)) Then
            ClaimActivityId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
        End If
    End Sub

    Public Overridable Sub CloseTheClaim()

        If ((IsDirty) AndAlso
            (Not (ReasonClosedId.Equals(Guid.Empty)))) Then
            'Close the Claim
            If (Status <> BasicClaimStatus.Closed) Then
                Status = BasicClaimStatus.Closed
            End If
            If (ClaimClosedDate Is Nothing) Then
                ClaimClosedDate = New DateType(System.DateTime.Today)
            End If
        End If

    End Sub

    Public Function IsDeductibleAmountChanged() As Boolean
        If Row.HasVersion(DataRowVersion.Current) AndAlso
           Not (Row.Item(ClaimDAL.COL_NAME_DEDUCTIBLE, DataRowVersion.Original).Equals _
          (Row.Item(ClaimDAL.COL_NAME_DEDUCTIBLE, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsStatusChngdFromPendingOrClosedToActive() As Boolean
        Dim OriginalReasonClosed As String
        If Row.HasVersion(DataRowVersion.Current) AndAlso
          (Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) = Codes.CLAIM_STATUS__PENDING) AndAlso
          (Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current) = Codes.CLAIM_STATUS__ACTIVE) Then
            Return (True)
        Else
            If Row.HasVersion(DataRowVersion.Current) AndAlso
            (Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Original) = Codes.CLAIM_STATUS__CLOSED) AndAlso
            (Row.Item(ClaimDAL.COL_NAME_STATUS_CODE, DataRowVersion.Current) = Codes.CLAIM_STATUS__ACTIVE) Then
                OriginalReasonClosed = LookupListNew.GetCodeFromId(LookupListCache.LK_REASONS_CLOSED, New Guid(CType(Row.Item(ClaimDAL.COL_NAME_REASON_CLOSED_ID, DataRowVersion.Original), Byte())))
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
        If Row.HasVersion(DataRowVersion.Current) AndAlso
           Not (Row.Item(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, DataRowVersion.Original).Equals _
          (Row.Item(ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsProblemDescriptionChanged() As Boolean
        If Row.HasVersion(DataRowVersion.Current) AndAlso
            Not (Row.Item(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, DataRowVersion.Original).Equals _
              (Row.Item(ClaimDAL.COL_NAME_PROBLEM_DESCRIPTION, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function IsSpecialInstructionChanged() As Boolean
        If Row.HasVersion(DataRowVersion.Current) AndAlso
            Not (Row.Item(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, DataRowVersion.Original).Equals _
              (Row.Item(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, DataRowVersion.Current))) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Function UseRecoveries() As Boolean
        Dim dv As DataView
        dv = LookupListNew.GetCompanyLookupList(CompanyId)

        If dv.Count > 0 Then
            If LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, New Guid(CType(dv(0)(ClaimDAL.COL_NAME_USE_RECOVERIES_ID), Byte()))) = Codes.YESNO_Y Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function AddExtendedClaimStatus(claimStatusId As Guid) As ClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            _claimStatusBO = New ClaimStatus(claimStatusId, Dataset)
        Else
            _claimStatusBO = New ClaimStatus(Dataset)
        End If

        Return _claimStatusBO
    End Function

    Private Function getTotalPaid(claimId As Guid) As MultiValueReturn(Of DecimalType, Integer)
        getTotalPaid = New ClaimDAL().LoadTotalPaid(claimId)
    End Function

    Private Function getTotalPaidForCert(CertId As Guid) As DecimalType
        Return New ClaimDAL().LoadTotalPaidForCert(CertId)
    End Function

    Public Shared Function GetOriginalLiabilityLimit(ClaimId As Guid) As Claim.MaterClaimDV
        Dim _dal As New ClaimDAL
        Dim ds As DataSet
        Dim dv As DataView
        Try
            ds = _dal.GetOriginalLiabilityAmount(ClaimId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
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

    Public Shared Function GetCauseOfLossID(CoverageTypeId As Guid) As Guid
        Dim oCoverageType As New CoverageType(CoverageTypeId)
        Dim oCoverageLoss As CoverageLoss = oCoverageType.AssociatedCoveragesLoss.FindDefault

        If oCoverageLoss IsNot Nothing Then
            Return oCoverageLoss.CauseOfLossId
        Else
            Return Guid.Empty
        End If
    End Function

    Public Shared Function CalculateLiabilityLimit(certId As Guid, contractId As Guid, certItemCoverageId As Guid, Optional ByVal lossDate As DateType = Nothing) As ArrayList
        Dim dal As New ClaimDAL
        Return dal.CalculateLiabilityLimit(certId, contractId, certItemCoverageId, lossDate)
    End Function

    Public Shared Function IsClaimReportedWithinPeriod(guidCertID As Guid, dtLossDate As Date, dtDateReported As Date) As Boolean
        Dim objCert As New Certificate(guidCertID)
        Dim oContract As Contract
        oContract = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
        Dim blnResult As Boolean = False
        If oContract IsNot Nothing Then
            If (oContract.DaysToReportClaim IsNot Nothing) AndAlso (oContract.DaysToReportClaim.Value > 0) Then
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
    Public Shared Function IsClaimReportedWithinGracePeriod(guidCertID As Guid, guidCertItemCoverageID As Guid, dtLossDate As Date, dtDateReported As Date) As Boolean
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
    Public Shared Function IsClaimReportedWithValidCoverage(guidCertID As Guid, guidCertItemCoverageID As Guid, dtLossDate As Date, dtDateReported As Date) As Boolean
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

    Public Function GetDealerDRPTradeInQuoteFlag(dealerCode As String) As Boolean
        Dim dv As DataView
        Dim dal As New ClaimDAL

        dv = (dal.GetDealerDRPTradeInQuoteFlag(dealerCode)).Tables(0).DefaultView

        If dv.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GetIndixIdofRegisteredDevice(claimId As Guid) As String
        Dim dv As DataView
        Dim claimDalObj As ClaimDAL = New ClaimDAL()
        Dim IndixId As String = claimDalObj.GetIndixIdofRegisteredDevice(claimId)

        Return IndixId

    End Function

    Public Shared Function VerifyIMEIWithDRPSystem(IMEI As String) As Boolean
        Dim oDRP As New DRP
        Dim Result As Boolean
        Result = oDRP.Get_DoesAcceptedOfferExist(IMEI)
        Return Result
    End Function

    Public Shared Function GetClaimCaseDeviceInfo(claimId As Guid) As DataView
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

    Public Property CurrentComment As Comment
        Get
            Return _comment
        End Get
        Set
            _comment = value
        End Set

    End Property



    Public Function AddNewComment(Optional ByVal loadInCurrentDS As Boolean = True) As Comment
        Comment.DeleteNewChildComment(Me)
        If loadInCurrentDS Then
            _comment = New Comment(Dataset)
        Else
            _comment = New Comment
        End If

        _comment.PopulateWithDefaultValues(CertificateId)
        _comment.CallerName = CallerName
        _comment.ClaimId = Id
        Select Case ClaimActivityCode
            Case Nothing, ""
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__REPLACEMENT_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__REWORK
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__REWORK_RECORD_CREATED)
            Case Codes.CLAIM_ACTIVITY__LEGAL_GENERAL
                _comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__LEGALGENERAL_RECORD_CREATED)
        End Select
        _comment.Comments = ProblemDescription
        Return _comment
    End Function

    Public Sub AttachShippingInfo(objShippingInfo As ShippingInfo)
        ShippingInfo.DeleteNewChildShippingInfo(Me)
        objCurrentShippingInfo = New ShippingInfo(Dataset)
        objCurrentShippingInfo.CopyFromThis(objShippingInfo)
        ShippingInfoId = objCurrentShippingInfo.Id
        objCurrentShippingInfo.ClaimIDHasBeenObtained = True
    End Sub

    Public Function AddClaimAuthDetail(objID As Guid, Optional ByVal blnLoadByClaimID As Boolean = False, Optional ByVal blnMustReload As Boolean = False) As ClaimAuthDetail
        If objID.Equals(Guid.Empty) Then
            Dim objClaimAuthDetail As New ClaimAuthDetail(Dataset, blnMustReload)
            Return objClaimAuthDetail
        Else
            Dim objClaimAuthDetail As New ClaimAuthDetail(objID, Dataset, blnLoadByClaimID, blnMustReload)
            Return objClaimAuthDetail
        End If
    End Function

    Public ReadOnly Property ClaimCommentsList As Comment.ClaimCommentList
        Get
            Return New Comment.ClaimCommentList(Me)
        End Get
    End Property

    Public Sub AddContactInfo(contactInfoID As Guid)
        If contactInfoID.Equals(Guid.Empty) Then
            _contactInfo = New ContactInfo(Dataset)
        Else
            _contactInfo = New ContactInfo(contactInfoID, Dataset)
        End If
    End Sub

    Public ReadOnly Property ClaimHistoryChildren As ClaimHistory.ClaimHistoryList
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

            If _ClaimedEquipment IsNot Nothing Then
                equipmentList.Add(_ClaimedEquipment)
            End If
            If _EnrolledEquipment IsNot Nothing Then
                equipmentList.Add(_EnrolledEquipment)
            End If

            Return equipmentList
        End Get

    End Property

    Public Property ClaimedEquipment As ClaimEquipment
        Get
            If _ClaimedEquipment Is Nothing Then
                _ClaimedEquipment = (From clmEquip As ClaimEquipment In ClaimEquipmentChildren
                                        Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED) _
                                        AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                        Select clmEquip).FirstOrDefault()
            End If
            Return _ClaimedEquipment
        End Get
        Set
            _ClaimedEquipment = value
        End Set
    End Property

    Public ReadOnly Property ReplacementOptions As List(Of ClaimEquipment)
        Get
            Return (From clmEquip As ClaimEquipment In ClaimEquipmentChildren
                    Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT_OPTION) _
                    AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                    Select clmEquip).ToList
        End Get
    End Property

    Private _EnrolledEquipment As ClaimEquipment
    Public Property EnrolledEquipment As ClaimEquipment
        Get
            If _EnrolledEquipment Is Nothing Then
                _EnrolledEquipment = (From clmEquip As ClaimEquipment In ClaimEquipmentChildren
                                         Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__ENROLLED) _
                                         AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                         Select clmEquip).FirstOrDefault
            End If
            Return _EnrolledEquipment
        End Get
        Set
            _EnrolledEquipment = value
        End Set
    End Property

    Private _ReplacementEquipment As ClaimEquipment
    Public Property ReplacementEquipment As ClaimEquipment
        Get
            If _ReplacementEquipment Is Nothing Then
                _ReplacementEquipment = (From clmEquip As ClaimEquipment In ClaimEquipmentChildren
                                            Where clmEquip.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT) _
                                            AndAlso Not clmEquip.Row.RowState = DataRowState.Deleted
                                            Select clmEquip).FirstOrDefault
            End If
            Return _ReplacementEquipment
        End Get
        Set
            _EnrolledEquipment = value
            _ReplacementEquipment = value
        End Set
    End Property
    Public Sub CreateEnrolledEquipment()
        'Only one Claimed Equipment can be created
        If EnrolledEquipment Is Nothing AndAlso
            CertificateItem.IsEquipmentRequired AndAlso
            (Not CertificateItem.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(CertificateItem.Model)) Then
            'Add Enrolled Equipment to the Claimed Equipment Children
            Dim EnrolledEquipmentBO As ClaimEquipment = ClaimEquipmentChildren.GetNewChild
            EnrolledEquipmentBO.BeginEdit()
            EnrolledEquipmentBO.ClaimId = Id
            EnrolledEquipmentBO.ClaimEquipmentDate = CertificateItem.CreatedDate
            EnrolledEquipmentBO.ManufacturerId = CertificateItem.ManufacturerId
            EnrolledEquipmentBO.Model = CertificateItem.Model
            EnrolledEquipmentBO.SKU = CertificateItem.SkuNumber
            EnrolledEquipmentBO.SerialNumber = CertificateItem.SerialNumber
            EnrolledEquipmentBO.IMEINumber = CertificateItem.IMEINumber
            'Price will be poulated at the time of creating Claim when the List price is resolved based on SKU
            If (Not CertificateItem.EquipmentId.Equals(Guid.Empty)) Then
                EnrolledEquipmentBO.EquipmentId = CertificateItem.EquipmentId
            End If
            EnrolledEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__ENROLLED)
            EnrolledEquipmentBO.EndEdit()
            EnrolledEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateClaimedEquipment(_ClaimedEquipment As ClaimEquipment)
        'Only one Claimed equipment can be created
        If ClaimedEquipment Is Nothing AndAlso
_ClaimedEquipment IsNot Nothing AndAlso
               Not (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC) Then
            Dim claimEquipmentBO As ClaimEquipment = ClaimEquipmentChildren.GetNewChild()
            With _ClaimedEquipment
                claimEquipmentBO.BeginEdit()
                claimEquipmentBO.ClaimId = Id
                claimEquipmentBO.ClaimEquipmentDate = CreatedDate
                claimEquipmentBO.ManufacturerId = .ManufacturerId
                claimEquipmentBO.Model = .Model
                claimEquipmentBO.SKU = .SKU
                claimEquipmentBO.SerialNumber = .SerialNumber
                claimEquipmentBO.IMEINumber = .IMEINumber
                claimEquipmentBO.EquipmentId = .EquipmentId

                claimEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED)
                claimEquipmentBO.EndEdit()
            End With
            claimEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateReplaceClaimedEquipment(_ClaimedEquipment As ClaimEquipment)

        If _ClaimedEquipment IsNot Nothing AndAlso
               Not (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC) Then
            Dim claimEquipmentBO As ClaimEquipment = ClaimEquipmentChildren.GetNewChild()
            With _ClaimedEquipment
                claimEquipmentBO.BeginEdit()
                claimEquipmentBO.ClaimId = Id
                claimEquipmentBO.ClaimEquipmentDate = CreatedDate
                claimEquipmentBO.ManufacturerId = .ManufacturerId
                claimEquipmentBO.Model = .Model
                claimEquipmentBO.SKU = .SKU
                claimEquipmentBO.SerialNumber = .SerialNumber
                claimEquipmentBO.IMEINumber = .IMEINumber
                claimEquipmentBO.EquipmentId = .EquipmentId

                claimEquipmentBO.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED)
                claimEquipmentBO.EndEdit()
            End With
            claimEquipmentBO.Save()
        End If
    End Sub

    Public Sub CreateReplacementOptions()
        If LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Dealer.UseEquipmentId) = Codes.YESNO_Y Then
            Dim ClaimedEquipment As ClaimEquipment
            ClaimedEquipment = Me.ClaimedEquipment
            If ClaimedEquipment IsNot Nothing AndAlso Not ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                Dim BROpt As List(Of BestReplacement.BestReplacementOptions) = BestReplacement.GetReplacementEquipments(Dealer.BestReplacementGroupId, ClaimedEquipment.EquipmentId, 4)
                For Each obj As BestReplacement.BestReplacementOptions In BROpt
                    If Not obj.Manufacturer_id.Equals(Guid.Empty) And Not String.IsNullOrEmpty(obj.Model) Then
                        Dim clEquipObj As ClaimEquipment = ClaimEquipmentChildren.GetNewChild
                        clEquipObj.BeginEdit()
                        clEquipObj.EquipmentId = obj.Equipment_id
                        clEquipObj.ManufacturerId = obj.Manufacturer_id
                        clEquipObj.ClaimId = Id
                        clEquipObj.Priority = obj.Priority
                        clEquipObj.Model = obj.Model
                        clEquipObj.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT_OPTION)
                        clEquipObj.ClaimEquipmentDate = CreatedDate
                        clEquipObj.EndEdit()
                        clEquipObj.Save()
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub UpdateEnrolledEquipment()
        If EnrolledEquipment IsNot Nothing AndAlso
            LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Dealer.UseEquipmentId) = Codes.YESNO_Y Then
            Dim _Enrolledequipment As ClaimEquipment = ClaimEquipmentChildren.GetChild(EnrolledEquipment.Id)
            With _Enrolledequipment
                .BeginEdit()
                .ClaimEquipmentDate = CertificateItem.CreatedDate
                .ManufacturerId = CertificateItem.ManufacturerId
                .Model = CertificateItem.Model
                .SKU = CertificateItem.SkuNumber
                .SerialNumber = CertificateItem.SerialNumber
                If (Not CertificateItem.EquipmentId.Equals(Guid.Empty)) Then
                    .EquipmentId = CertificateItem.EquipmentId
                Else
                    If CertificateItem.VerifyEquipment() Then
                        .EquipmentId = CertificateItem.EquipmentId
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
        If CertificateItem.IsEquipmentRequired AndAlso EnrolledEquipment IsNot Nothing AndAlso Not EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
            If LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, Dealer.UseEquipmentId) = Codes.YESNO_Y Then
                If Not EnrolledEquipment.EquipmentBO.Id = ClaimedEquipment.EquipmentBO.Id OrElse
                    Not EnrolledEquipment.SerialNumber.Trim.ToUpper = ClaimedEquipment.SerialNumber.Trim.ToUpper Then
                    retVal = True
                End If
            End If
        End If
        Return retVal
    End Function

    Public Function ValidateAndMatchClaimedEnrolledEquipments(ByRef comment As Comment) As Boolean
        Dim retval As Boolean = True '
        If CertificateItem.IsEquipmentRequired Then
            If comment Is Nothing Then comment = AddNewComment

            'Validate Claimed Equipment
            If ClaimedEquipment.EquipmentId.Equals(Guid.Empty) Then
                comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIMED_EQUIPMENT_NOT_CONFIGURED)
                comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_CLAIMED_EQUIPMENT_NOT_RESOLVED")
                retval = False
                Return retval
            Else
                'Validate Enrolled Equipment
                If EnrolledEquipment Is Nothing OrElse EnrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
                    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__ENROLLED_EQUIPMENT_NOT_CONFIGURED)
                    comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ENROLLED_EQUIPMENT_NOT_RESOLVED")
                    retval = False
                    Return retval
                Else
                    'Match Claimed and Enrolled
                    If IsEquipmentMisMatch() Then
                        comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__MAKE_MODEL_IMEI_MISMATCH)
                        comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.EQUIPMENT_MISMATCH)
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

        Dim objCompanyBO As Company = New Company(CompanyId)
        If _claimStatusBO Is Nothing Then Exit Sub
        If _claimStatusBO.IsTimeZoneForClaimExtStatusDateDone Then Exit Sub
        If objCompanyBO.TimeZoneNameId.Equals(Guid.Empty) Then Exit Sub
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
    Public Shared Function LoadClaimsBySerialNumber(countryCode As String,
                                             companyCode As String,
                                             dealerCode As String,
                                             serialNumber As String) As DataSet

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
    Public Shared Function LoadClaimsByImeiNumber(countryCode As String,
                                             companyCode As String,
                                             dealerCode As String,
                                             imeiNumber As String) As DataSet

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
    Public Shared Function WS_CHLMobileSCPortal_GetCertClaimInfo(CompanyCode As String,
                                                                 SerialNumber As String,
                                                                 PhoneNumber As String,
                                                                 taxId As String,
                                                                 claimStatusCode As String,
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

    Public Shared Function WS_SNMPORTAL_SA_CLAIMREPORT(companyCode As String,
                                                       serviceCenterCode As String,
                                                       countryIsoCode As String,
                                                       fromDate As Date,
                                                       endDate As Date,
                                                       extendedStatusCode As String,
                                                       dealerCode As String,
                                                       pageSize As Integer,
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

    Public Shared Function WS_SNMPORTAL_SA_CLAIMREPORT_NextPage(batchId As Guid,
                                                                pageSize As Integer,
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
    Public Function IsCustomerPresentInUFIList(CountryId As Guid, TaxID As String) As DataView 'REQ-5978
        Dim dal As New ClaimDAL
        Return dal.IsCustomerPresentInUFIList(CountryId, TaxID)
    End Function
#End Region

    Public Function IsCertPaymentExists(certId As Guid) As Boolean 'REQ-6026
        Dim dal As New ClaimDAL
        Return dal.IsCertPaymentExists(certId)
    End Function

    Public Function isConsequentialDamageAllowed(productCodeId As Guid) As Boolean
        Dim dal As New ClaimDAL
        Return dal.isConsequentialDamageAllowed(productCodeId)
    End Function

    Public Function IsServiceWarrantyValid(ClaimId As Guid) As Boolean
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


