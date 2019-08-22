'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/12/2006)  ********************

Public Class Coverage
    Inherits BusinessObjectBase
    Implements IAttributable

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
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
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CoverageDAL
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
            Dim dal As New CoverageDAL
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
        Me.DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED")
        Me.DeductiblePercent = 0
        Me.Deductible = 0
        Me.Inuseflag = "N"
    End Sub

    Private mbUniqueFieldsChanged As Boolean
    Private mPercentOfRetail As DecimalType

#End Region

#Region "Constants"

    Public Const EXPIRATION_COUNT As String = "EXPIRATION_COUNT"
    Public Const MAX_EXPIRATION As String = "EXPIRATION"
    Public Const MIN_DURATION As Integer = 1
    Public Const MAX_DURATION As Integer = 99
    Public Const MAX_PERCENT As Integer = 100
    Public Const MIN_OFFSET As Integer = 0
    Public Const MAX_LIABILITY As Integer = 99999
    Public Const MIN_OFFSET_LIABLIMIT_PERCENT As Integer = 50
    Public Const MAX_DATE As String = "12/31/2999"
    Public Const MAX_COVERAGE_CLAIM_LIMIT = 999
    Public Const MAN_COV_MAIN_PARTS = "MP"
    Public Const EXT_COV_MAIN_PARTS = "EP"

    Public Const COL_DEALER As String = "DEALER"
    Public Const COL_PRODUCT_CODE As String = "PRODUCT_CODE"
    Public Const COL_RISK_TYPE As String = "RISK_TYPE"
    Public Const COL_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const COL_COVERAGE_TYPE As String = "COVERAGE_TYPE"
    Public Const COL_CERTIFICATE_DURATION As String = "CERTIFICATE_DURATION"
    Public Const COL_COVERAGE_DURATION As String = "COVERAGE_DURATION"
    Public Const COL_EFFECTIVE As String = "EFFECTIVE"
    Public Const COL_EXPIRATION As String = "EXPIRATION"
    Public Const COL_EFFECTIVE_DATE_FORMAT As String = "EFFECTIVE_DATE_FORMAT"
    Public Const COL_EXPIRATION_DATE_FORMAT As String = "EXPIRATION_DATE_FORMAT"
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = CoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID

    Private Const ZERO_LONG As Long = 0
#End Region

#Region "Properties"

    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Me.Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return CoverageDAL.TABLE_NAME.ToUpper()
        End Get
    End Property
    'Key Property
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(CoverageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_COVERAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DeductibleBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID, value)
        End Set
    End Property

    Public Property DeductibleExpressionId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID, value)
        End Set
    End Property
    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID, value)
        End Set
    End Property

    '<ValueMandatory("")> _
    Public Property ItemId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_ITEM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ProductItemId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_PRODUCT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_PRODUCT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_PRODUCT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DURATION, Max:=999, Message:=Common.ErrorCodes.COVERAGEBO_001), ValidCertificateDuration(""), ValidCertDurationForPerodicRenewable("")>
    Public Property CertificateDuration() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_CERTIFICATE_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_CERTIFICATE_DURATION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_CERTIFICATE_DURATION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DURATION, Max:=999, Message:=Common.ErrorCodes.COVERAGEBO_002), ValidCoverageDurationForPerodicRenewable("")>
    Public Property CoverageDuration() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_COVERAGE_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_COVERAGE_DURATION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_COVERAGE_DURATION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidCoverageDates(""), ValidContract(""), ValidCoveragePeriod("")>
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CoverageDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CoverageDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property OffsetMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_OFFSET_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_OFFSET_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_OFFSET_METHOD_ID, Value)
        End Set
    End Property

    Public Property OffsetMethod() As String
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_OFFSET_METHOD) Is DBNull.Value Then
                Return "FIXED" ' Nothing
            Else
                Return CType(Row(CoverageDAL.COL_NAME_OFFSET_METHOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_OFFSET_METHOD, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=MIN_OFFSET, Max:=MAX_PERCENT, MaxExclusive:=False)>
    Public Property MarkupDistributionPercent() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_MARKUP_DISTRIBUTION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_MARKUP_DISTRIBUTION_PERCENT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_MARKUP_DISTRIBUTION_PERCENT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_OFFSET, Max:=MAX_DURATION, Message:=Common.ErrorCodes.COVERAGEBO_003), ValidOffsetForPerodicRenewable("")>
    Public Property OffsetToStart() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_OFFSET_TO_START) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_OFFSET_TO_START), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_OFFSET_TO_START, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=9999), ValidOffsetDaysNotForPerodicRenewable("")>
    Public Property OffsetToStartDays() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_OFFSET_TO_START_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_OFFSET_TO_START_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_OFFSET_TO_START_DAYS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property OptionalId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_OPTIONAL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_OPTIONAL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_OPTIONAL_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IsClaimAllowedId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_OFFSET, Max:=NEW_MAX_LONG, Message:=Common.ErrorCodes.COVERAGEBO_006)>
    Public Property LiabilityLimit() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_LIABILITY_LIMIT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_LIABILITY_LIMIT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_OFFSET, Max:=NEW_MAX_LONG, Message:=Common.ErrorCodes.COVERAGEBO_006)>
    Public Property Deductible() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    'Additional Properties

    <ValueMandatory("")>
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    Public Property PercentOfRetail() As DecimalType
        Get
            Return mPercentOfRetail
        End Get
        Set(ByVal Value As DecimalType)
            mPercentOfRetail = Value
        End Set
    End Property

    Public Property EarningCodeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_EARNING_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_EARNING_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_EARNING_CODE_ID, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=MIN_OFFSET, Max:=MAX_PERCENT, MaxExclusive:=False)>
    Public Property DeductiblePercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=MIN_OFFSET_LIABLIMIT_PERCENT, Max:=MAX_PERCENT, MaxExclusive:=False)>
    Public Property LiabilityLimitPercent() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_LIABILITY_LIMIT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_LIABILITY_LIMIT_PERCENT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_LIABILITY_LIMIT_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999)>
    Public Property RepairDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999)>
    Public Property ReplacementDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseCoverageStartDateId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_USE_COVERAGE_START_DATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_USE_COVERAGE_START_DATE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_USE_COVERAGE_START_DATE_ID, Value)
        End Set
    End Property
    <ValidateCoverageClaimLimit("")>
    Public Property CoverageClaimLimit() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_COVERAGE_CLAIM_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_COVERAGE_CLAIM_LIMIT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_COVERAGE_CLAIM_LIMIT, Value)
        End Set
    End Property
    Public Property PerIncidentLiabilityLimitCap() As Decimal
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP), Decimal))
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP, Value)
        End Set
    End Property
    Public Property CoverageLiabilityLimit() As Decimal
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT), Long))
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=MAX_PERCENT, MaxExclusive:=False)>
    Public Property CoverageLiabilityLimitPercent() As LongType
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT_PERCENT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT_PERCENT, Value)
        End Set
    End Property

    Public Property ProdLiabilityLimitBaseId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID, Value)
        End Set
    End Property

    Public Property UniqueFieldsChanged() As Boolean
        Get
            Return mbUniqueFieldsChanged
        End Get
        Set(ByVal Value As Boolean)
            mbUniqueFieldsChanged = Value
        End Set
    End Property

    'REQ-1295: Added validation ValidateAgentCodeMandetory
    <ValidStringLengthAttribute("", Max:=15), ValidateAgentCodeMandetory("")>
    Public Property AgentCode() As String
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_AGENT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageDAL.COL_NAME_AGENT_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_AGENT_CODE, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RecoverDeviceId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_RECOVER_DEVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_RECOVER_DEVICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_RECOVER_DEVICE_ID, Value)
        End Set
    End Property
    Public Property IsReInsuredId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_IS_REINSURED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_IS_REINSURED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_IS_REINSURED_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property Inuseflag() As String
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_INUSEFLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageDAL.COL_NAME_INUSEFLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_INUSEFLAG, Value)
        End Set
    End Property

    Public Property TaxTypeXCD() As String
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_TAX_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageDAL.COL_NAME_TAX_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_TAX_TYPE_XCD, Value)
        End Set
    End Property

    Public Property DealerMarkupId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDAL.COL_NAME_DEALER_MARKUP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDAL.COL_NAME_DEALER_MARKUP), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDAL.COL_NAME_DEALER_MARKUP, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Sub ClearAttributeValues()
        _AttributeValueList = Nothing
        Me.Dataset.Tables.Remove(AttributeDAL.TABLE_NAME)
        Me.Dataset.Tables.Remove(AttributeValueDAL.TABLE_NAME)
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageDAL
                dal.UpdateFamily(Me.Dataset)
                'dal.Update(Me.Row)
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal dealerId As Guid, ByVal productCodeId As Guid, ByVal itemId As Guid,
                                   ByVal coverageTypeId As Guid, ByVal certificateDuration As Assurant.Common.Types.LongType,
                                   ByVal coverageDuration As Assurant.Common.Types.LongType) As DataView
        Try
            Dim dal As New CoverageDAL
            Return New DataView(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                                dealerId, productCodeId, itemId, coverageTypeId, certificateDuration,
                                coverageDuration, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function ExpirationCount() As Integer
        Try
            Dim retVal As Integer = 0
            Dim dal As New CoverageDAL
            Dim ds As New DataSet

            If Not (ItemId.Equals(Guid.Empty) Or CoverageTypeId.Equals(Guid.Empty) Or CertificateDuration Is Nothing Or CoverageDuration Is Nothing) Then
                ds = dal.MaxExpiration(ItemId, CoverageTypeId, CertificateDuration, CoverageDuration)
                If ds.Tables(0).Rows.Count = 1 Then
                    retVal = ds.Tables(0).Rows(0).Item(EXPIRATION_COUNT)
                End If
            End If

            Return retVal

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function MaxExpiration() As Date
        Try
            Dim retVal As Date = Nothing
            Dim dal As New CoverageDAL
            Dim ds As New DataSet

            If Not (ItemId.Equals(Guid.Empty) Or CoverageTypeId.Equals(Guid.Empty) Or CertificateDuration Is Nothing Or CoverageDuration Is Nothing) Then
                ds = dal.MaxExpiration(ItemId, CoverageTypeId, CertificateDuration, CoverageDuration)
                If ds.Tables(0).Rows.Count = 1 Then
                    retVal = ds.Tables(0).Rows(0).Item(MAX_EXPIRATION)
                End If
            End If

            Return retVal

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function IsLastCoverage() As Boolean
        Dim obj As Coverage = Me
        Dim ds As DataSet
        Dim dal As New CoverageDAL
        Dim maxExpiration As Date

        If Not (obj.DealerId.Equals(Guid.Empty) Or obj.ProductCodeId.Equals(Guid.Empty) Or obj.RiskTypeId.Equals(Guid.Empty) Or obj.ItemId.Equals(Guid.Empty) Or obj.CoverageTypeId.Equals(Guid.Empty) Or obj.CertificateDuration Is Nothing Or obj.CoverageDuration Is Nothing) Then
            ds = dal.GetCoverageList(obj.DealerId, obj.ProductCodeId, obj.RiskTypeId, obj.ItemId, obj.CoverageTypeId, obj.CertificateDuration, obj.CoverageDuration, obj.Effective, obj.Expiration, Guid.Empty)
            Dim recCount As Integer = 0
            If ds.Tables.Count > 0 Then
                recCount = ds.Tables(0).Rows.Count
                If recCount > 0 Then
                    maxExpiration = ds.Tables(0).Rows(recCount - 1)("COVERAGE_EXPIRATION")
                    If Me.Expiration.Value <> maxExpiration Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True

    End Function

    Public Function IsFirstCoverage() As Boolean
        Dim obj As Coverage = Me
        Dim ds As DataSet
        Dim dal As New CoverageDAL
        Dim minEffective As Date

        If Not (obj.DealerId.Equals(Guid.Empty) Or obj.ProductCodeId.Equals(Guid.Empty) Or obj.RiskTypeId.Equals(Guid.Empty) Or obj.ItemId.Equals(Guid.Empty) Or obj.CoverageTypeId.Equals(Guid.Empty) Or obj.CertificateDuration Is Nothing Or obj.CoverageDuration Is Nothing) Then
            ds = dal.GetCoverageList(obj.DealerId, obj.ProductCodeId, obj.RiskTypeId, obj.ItemId, obj.CoverageTypeId, obj.CertificateDuration, obj.CoverageDuration, obj.Effective, obj.Expiration, Guid.Empty)
            Dim recCount As Integer = 0
            If ds.Tables.Count > 0 Then
                recCount = ds.Tables(0).Rows.Count
                If recCount > 0 Then
                    minEffective = ds.Tables(0).Rows(0)("COVERAGE_EFFECTIVE")
                    If Me.Effective.Value <> minEffective Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True

    End Function

    Public Shared Function GetCurrencyOfCoverage(ByVal coverageId As Guid) As DataView
        Try
            Dim dal As New CoverageDAL
            Return New DataView(dal.GetCurrencyOfCoverage(coverageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetProductLiabilityLimitBase(ByVal productId As Guid) As Guid
        Dim ProductLiabilityLimitBaseId As Guid
        Try
            Dim dal As New CoverageDAL
            Dim dv As DataSet = dal.GetProductLiabilityLimitBaseid(productId)
            If Not IsDBNull(dv.Tables(0).Rows(0)(CoverageDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID)) Then
                ProductLiabilityLimitBaseId = New Guid(CType(dv.Tables(0).Rows(0)(CoverageDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID), Byte()))
                Return ProductLiabilityLimitBaseId
            Else
                Return Guid.Empty
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCoverageDeductable(ByVal dealerId As Guid, ByVal effectiveDate As String, ByVal languageId As Guid) As DataView
        Try
            Dim dal As New CoverageDAL
            Return New DataView(dal.GetCoverageDeductible(dealerId, effectiveDate, languageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Used by Olita Web Service 
    Public Shared Function getDealerCoveragesInfo(ByRef ds As DataSet, ByVal dealerId As Guid, ByVal WarrSalesDate As Date) As DataSet
        Try
            Dim dal As New CoverageDAL
            Return dal.LoadDealerCoveragesInfo(ds, dealerId, WarrSalesDate)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAssociatedCertificateCount(ByVal coverageId As Guid) As UInt64
        Try
            Dim dal As New CoverageDAL
            Dim dsCoverageCertificateCount As DataSet = dal.GetAssociatedCertificateCount(coverageId)
            If Not dsCoverageCertificateCount Is Nothing AndAlso dsCoverageCertificateCount.Tables.Count > 0 AndAlso dsCoverageCertificateCount.Tables(0).Rows.Count > 0 Then
                If dsCoverageCertificateCount.Tables(0).Rows(0)(CoverageDAL.COL_NAME_NUMBER_OF_CERTIFICATES) Is DBNull.Value Then
                    Return 0
                Else
                    Return CType(dsCoverageCertificateCount.Tables(0).Rows(0)(CoverageDAL.COL_NAME_NUMBER_OF_CERTIFICATES), UInt64)
                End If
            Else
                Return 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCertificateDuration
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.COVERAGEBO_014)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            Dim oContract As Contract = New Contract
            'Def-26342: Added condition to check null value for Effective and Expiration.
            If Not (obj.Effective Is Nothing And obj.Expiration Is Nothing) Then
                oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)
            End If

            If Not oContract Is Nothing Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_CONTRACT_TYPES, oContract.ContractTypeId) = "3" Then
                    Return True
                End If
            End If
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, obj.CoverageTypeId) = MAN_COV_MAIN_PARTS) Or (LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, obj.CoverageTypeId) = EXT_COV_MAIN_PARTS) Then
                Return True
            End If

            If Not (obj.CertificateDuration Is Nothing Or obj.CoverageDuration Is Nothing Or obj.OffsetToStart Is Nothing) Then
                If Convert.ToDouble(obj.CoverageDuration.Value) + Convert.ToDouble(obj.OffsetToStart.Value) > Convert.ToDouble(obj.CertificateDuration.Value) Then
                    Return False
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoverageDates
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.COVERAGEBO_015)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)
            If Not (obj.Effective Is Nothing Or obj.Expiration Is Nothing) Then
                If obj.Effective.Value > obj.Expiration.Value Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoveragePeriod
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.COVERAGEBO_016)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As Coverage = CType(objectToValidate, Coverage)
            Dim bValid As Boolean = True
            Dim dal As New CoverageDAL
            Dim ds As New DataSet
            Dim currRow, prevRow, nextRow As DataRow

            If obj.Effective Is Nothing Or obj.Expiration Is Nothing Then
                Return True  ' Skip validation. Rely on mandatory field validation to report exception
            End If
            If Not (obj.DealerId.Equals(Guid.Empty) Or obj.ProductCodeId.Equals(Guid.Empty) Or obj.RiskTypeId.Equals(Guid.Empty) Or obj.ItemId.Equals(Guid.Empty) Or obj.CoverageTypeId.Equals(Guid.Empty) Or obj.CertificateDuration Is Nothing Or obj.CoverageDuration Is Nothing) Then
                ds = dal.GetCoverageList(obj.DealerId, obj.ProductCodeId, obj.RiskTypeId, obj.ItemId, obj.CoverageTypeId, obj.CertificateDuration, obj.CoverageDuration, obj.Effective, obj.Expiration, obj.Id)
                Dim recCount As Integer = 0
                If ds.Tables.Count > 0 Then
                    recCount = ds.Tables(0).Rows.Count
                    Dim lastRowId As Guid
                    Dim currRowPos As Integer = 0
                    If recCount > 0 Then
                        lastRowId = New Guid(CType(ds.Tables(0).Rows(recCount - 1)("COVERAGE_ID"), Byte()))
                        Dim minEffective As Date = ds.Tables(0).Rows(0)("COVERAGE_EFFECTIVE")
                        Dim maxExpiration As Date = ds.Tables(0).Rows(recCount - 1)("COVERAGE_EXPIRATION")
                        ' Inserting at the begining
                        If obj.Expiration.Value.AddDays(1) = minEffective Then
                            Return True
                        End If
                        ' Inserting at the end
                        If obj.Effective.Value.AddDays(-1) = maxExpiration Then
                            Return True
                        End If
                        ' Find a spot in the middle
                        For Each currRow In ds.Tables(0).Rows
                            If obj.Expiration.Value = currRow("COVERAGE_EXPIRATION") And
                            obj.Effective.Value = currRow("COVERAGE_EFFECTIVE") Then
                                ' Trying to insert a Duplicate - Reject!
                                Return False
                            ElseIf Not prevRow Is Nothing Then
                                ' Inserting in the middle (Allow to fix any GAPS)
                                If obj.Effective.Value.AddDays(-1) = prevRow("COVERAGE_EXPIRATION") And
                               obj.Expiration.Value.AddDays(1) = currRow("COVERAGE_EFFECTIVE") Then
                                    Return True
                                End If
                            End If
                            prevRow = currRow
                            currRowPos += 1
                        Next
                        bValid = False
                    End If
                End If
            End If

            Return bValid

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidContract
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.COVERAGEBO_019)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bValid As Boolean = True
            Dim obj As Coverage = CType(objectToValidate, Coverage)
            Try
                If Not obj.Effective Is Nothing And Not obj.Expiration Is Nothing Then
                    Dim oContract As Contract = New Contract
                    oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)
                    If oContract Is Nothing Then
                        bValid = False
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

            Return bValid

        End Function
    End Class

    ' <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    'Public NotInheritable Class ValidLiabilityLimitNoDecimals
    '     Inherits ValidBaseAttribute

    '     Public Sub New(ByVal fieldDisplayName As String)
    '         MyBase.New(fieldDisplayName, Common.ErrorCodes.COVERAGEBO_004)
    '     End Sub

    '     Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
    '         Dim obj As Coverage = CType(objectToValidate, Coverage)

    '         If Not (obj.LiabilityLimit Is Nothing andalso obj.LiabilityLimit.Value.ToString.IndexOf( ) Then
    '             If Convert.ToDouble(obj.CoverageDuration.Value) + Convert.ToDouble(obj.OffsetToStart.Value) > Convert.ToDouble(obj.CertificateDuration.Value) Then
    '                 Return False
    '             End If
    '         End If

    '         Return True

    '     End Function
    ' End Class

    'Req - 1016 Start
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCertDurationForPerodicRenewable
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_CERT_DURATION_FOR_PERODIC_RENEW)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            Try
                If Not obj.Effective Is Nothing And Not obj.Expiration Is Nothing Then
                    Dim oContract As Contract = New Contract
                    oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)

                    If (Not oContract Is Nothing) Then
                        Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, oContract.RecurringPremiumId)

                        If (Not obj.CertificateDuration Is Nothing) And (obj.CertificateDuration <> 0) Then
                            If (Not sVal Is Nothing) And (CType(sVal, Long) <> 0) Then
                                If CType(obj.CertificateDuration, Long) Mod CType(sVal, Long) = 0 Then
                                    Return True
                                Else
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoverageDurationForPerodicRenewable
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COVERAGE_DURATION_FOR_PERODIC_RENEW)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            Try
                If Not obj.Effective Is Nothing And Not obj.Expiration Is Nothing Then
                    Dim oContract As Contract = New Contract
                    oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)

                    If (Not oContract Is Nothing) Then
                        Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, oContract.RecurringPremiumId)

                        If (Not obj.CoverageDuration Is Nothing) And (obj.CoverageDuration <> 0) Then
                            If (Not sVal Is Nothing) And (CType(sVal, Long) <> 0) Then
                                If CType(obj.CoverageDuration, Long) Mod CType(sVal, Long) = 0 Then
                                    Return True
                                Else
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidOffsetForPerodicRenewable
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_OFFSET_FOR_PERODIC_RENEW)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            Try
                If Not obj.Effective Is Nothing And Not obj.Expiration Is Nothing Then
                    Dim oContract As Contract = New Contract
                    oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)

                    If (Not oContract Is Nothing) Then
                        Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, oContract.RecurringPremiumId)

                        If (Not obj.OffsetToStart Is Nothing) And (obj.OffsetToStart <> 0) Then
                            If (Not sVal Is Nothing) And (CType(sVal, Long) <> 0) Then
                                If CType(obj.OffsetToStart, Long) Mod CType(sVal, Long) = 0 Then
                                    Return True
                                Else
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidOffsetDaysNotForPerodicRenewable
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "OFFSET_DAYS_INVALID_FOR_PERODIC_RENEW")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            Try
                If (Not obj.OffsetToStartDays Is Nothing) And (obj.OffsetToStartDays <> 0) Then
                    If Not obj.Effective Is Nothing And Not obj.Expiration Is Nothing Then
                        Dim oContract As Contract = New Contract
                        oContract = oContract.GetContract(obj.DealerId, obj.Effective.Value, obj.Expiration.Value)

                        If (Not oContract Is Nothing) Then
                            Dim sVal As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PERIOD_RENEW, oContract.RecurringPremiumId)
                            If (Not sVal Is Nothing) And (CType(sVal, Long) <> 0) Then 'Offset to start days not allowed for Perodic Renewable contract
                                Return False
                            End If
                        End If
                    End If
                End If

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try

            Return True

        End Function
    End Class
    'Req - 1016 End

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateAgentCodeMandetory
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)
            'Get Dealer Company specific flag settings
            If ((Not obj Is Nothing) And (Not obj.DealerId = Guid.Empty)) Then
                Dim objCompany = New Company()
                Dim ds As DataSet = objCompany.GetCompanyAgentFlagForDealer(obj.DealerId)

                If (Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(CompanyDAL.TABLE_NAME_FLAG_REQAGENT).Rows.Count > 0) Then
                    Dim RequiresAgentCodeId As Guid = New Guid(CType(ds.Tables(CompanyDAL.TABLE_NAME_FLAG_REQAGENT).Rows(0)(CompanyDAL.COL_NAME_REQUIRE_AGENT_CODE_ID), Byte())) 'ds.Tables(0).Rows(0)(0)
                    If (RequiresAgentCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                        Dim mandatAttr As New ValueMandatoryAttribute(Me.DisplayName)
                        Return mandatAttr.IsValid(valueToCheck, objectToValidate)
                    End If

                End If
            End If

            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateCoverageClaimLimit
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COVERAGE_CLAIM_LIMIT)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Coverage = CType(objectToValidate, Coverage)

            REM Only allow to have not empty value when on product code record,
            REM ProdLiabilityLimitBasedOnId is set to a value other than: 
            REM “Not Applicable”,  “Claim/Liability Limit Applied To” 

            If ((Not obj Is Nothing) And (Not obj.ProductCodeId = Guid.Empty)) Then
                Dim objProductCode = New ProductCode(obj.ProductCodeId)
                If Not objProductCode Is Nothing Then

                    Dim sProdLimitBaseOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, objProductCode.ProdLiabilityLimitBasedOnId)

                    If Not String.IsNullOrEmpty(sProdLimitBaseOnCode) And
                        String.Compare(sProdLimitBaseOnCode, LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE, True) <> 0 Then
                        ' On ProductCode "Claim/Liability Limit Base On" is set to value OTHER than "Not Applicable"

                        If String.Equals(objProductCode.ProductLimitApplicableToXCD, LookupListNew.LK_PROD_LIMIT_APPLICABLE_TO_ALL, StringComparison.OrdinalIgnoreCase) Or
                            String.Equals(objProductCode.ProductLimitApplicableToXCD, LookupListNew.LK_PROD_LIMIT_APPLICABLE_TO_CLAIMONLY, StringComparison.OrdinalIgnoreCase) Then
                            ' "Claim/Liability Limit Applied" set to "ALL" or "Claim Limit Only"


                            Return (Not (obj.CoverageClaimLimit Is Nothing) AndAlso obj.CoverageClaimLimit.Value > ZERO_LONG) ' not allow empty value for "Coverage Claim Limit"
                        Else
                            ' for "Liability Limit Only" this field must be empty
                            Return (obj.CoverageClaimLimit Is Nothing)
                        End If

                    Else
                            ' On ProductCode "Claim/Liability Limit Base On" is set to empty or to "Not Applicable"
                            ' in this case "CoverageClaimLimit" must be empty
                            Return (obj.CoverageClaimLimit Is Nothing)
                    End If
                End If
            End If
            Return True
        End Function
    End Class
#End Region

    #Region "Depreciation Schedule"

    Public ReadOnly Property CoverageDepreciationScdChildren() As DepreciationScdRelation.CoverageDepreciationScdList
        Get
            Return New DepreciationScdRelation.CoverageDepreciationScdList(Me)
        End Get
    End Property

    Public Function GetCoverageDepreciationScdChild() As DepreciationScdRelation
        Dim productDepreciationScd As DepreciationScdRelation = CoverageDepreciationScdChildren.Find(Id)
        If productDepreciationScd Is Nothing Then
            productDepreciationScd = CoverageDepreciationScdChildren.GetNewChild
            productDepreciationScd.TableReference = TableName
            productDepreciationScd.TableReferenceId = Id
            productDepreciationScd.EffectiveDate = DateTime.Now
            productDepreciationScd.ExpirationDate = DateTime.MaxValue
            productDepreciationScd.DepreciationScheduleUsageXcd = Codes.DEPRECIATION_SCHEDULE_USAGE__CASH_REIMBURSE
            productDepreciationScd.DepreciationScheduleId = Guid.Empty
        End If
        Return productDepreciationScd
    End Function

    Public Sub AddCoverageDepreciationScdChild(ByVal depreciationScheduleId As Guid)

        Dim productDepreciationScd As DepreciationScdRelation = GetCoverageDepreciationScdChild()

        If Not depreciationScheduleId.Equals(Guid.Empty) Then
            productDepreciationScd.DepreciationScheduleId = depreciationScheduleId
        Else
            productDepreciationScd.Delete()
        End If

    End Sub

#End Region

End Class


