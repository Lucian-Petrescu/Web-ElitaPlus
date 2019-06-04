'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/25/2016)  ********************

Public Class ContractUpload
    Inherits BusinessObjectBase

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
            Dim dal As New ContractUploadDAL
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
            Dim dal As New ContractUploadDAL
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
            If Row(ContractUploadDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CONTRACT_UPLOAD_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property UploadSessionId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_UPLOAD_SESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_UPLOAD_SESSION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_UPLOAD_SESSION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RecordNumber() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RECORD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_RECORD_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RECORD_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2000)>
    Public Property ValidationErrors() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_VALIDATION_ERRORS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_VALIDATION_ERRORS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_VALIDATION_ERRORS, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ContractTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CONTRACT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CONTRACT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CONTRACT_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ContractUploadDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ContractUploadDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CommissionsPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COMMISSIONS_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_COMMISSIONS_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COMMISSIONS_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property MarketingPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_MARKETING_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_MARKETING_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_MARKETING_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property AdminExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ADMIN_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_ADMIN_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ADMIN_EXPENSE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ProfitPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PROFIT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_PROFIT_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PROFIT_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property LossCostPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_LOSS_COST_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_LOSS_COST_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_LOSS_COST_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CURRENCY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TypeOfMarketingId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_TYPE_OF_MARKETING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_TYPE_OF_MARKETING_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_TYPE_OF_MARKETING_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TypeOfEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TypeOfInsuranceId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_TYPE_OF_INSURANCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_TYPE_OF_INSURANCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_TYPE_OF_INSURANCE_ID, Value)
        End Set
    End Property



    Public Property MinReplacementCost() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_MIN_REPLACEMENT_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_MIN_REPLACEMENT_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_MIN_REPLACEMENT_COST, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property WarrantyMaxDelay() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_WARRANTY_MAX_DELAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_WARRANTY_MAX_DELAY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_WARRANTY_MAX_DELAY, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property NetCommissionsId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NET_COMMISSIONS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_NET_COMMISSIONS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NET_COMMISSIONS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property NetMarketingId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NET_MARKETING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_NET_MARKETING_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NET_MARKETING_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property NetTaxesId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NET_TAXES_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_NET_TAXES_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NET_TAXES_ID, Value)
        End Set
    End Property



    Public Property Deductible() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property WaitingPeriod() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_WAITING_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_WAITING_PERIOD), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_WAITING_PERIOD, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property FundingSourceId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_FUNDING_SOURCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_FUNDING_SOURCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_FUNDING_SOURCE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property EditModelId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EDIT_MODEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_EDIT_MODEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EDIT_MODEL_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DealerMarkupId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEALER_MARKUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_DEALER_MARKUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEALER_MARKUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property AutoMfgCoverageId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_AUTO_MFG_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_AUTO_MFG_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_AUTO_MFG_COVERAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RestrictMarkupId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RESTRICT_MARKUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_RESTRICT_MARKUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RESTRICT_MARKUP_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)>
    Public Property Layout() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property



    Public Property SuspenseDays() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_SUSPENSE_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_SUSPENSE_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_SUSPENSE_DAYS, Value)
        End Set
    End Property



    Public Property CancellationDays() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CANCELLATION_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_CANCELLATION_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CANCELLATION_DAYS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property Comment1() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COMMENT1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_COMMENT1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COMMENT1, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property FixedEscDurationFlag() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_FIXED_ESC_DURATION_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_FIXED_ESC_DURATION_FLAG), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_FIXED_ESC_DURATION_FLAG, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Policy() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_POLICY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_POLICY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_POLICY, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ReplacementPolicyId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CoinsuranceId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COINSURANCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_COINSURANCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COINSURANCE_ID, Value)
        End Set
    End Property



    Public Property ParticipationPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PARTICIPATION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_PARTICIPATION_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PARTICIPATION_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property IdValidationId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ID_VALIDATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ID_VALIDATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ID_VALIDATION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ClaimControlId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CLAIM_CONTROL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CLAIM_CONTROL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CLAIM_CONTROL_ID, Value)
        End Set
    End Property



    Public Property RatingPlan() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RATING_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_RATING_PLAN), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RATING_PLAN, Value)
        End Set
    End Property



    Public Property CurrencyConversionId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CURRENCY_CONVERSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CURRENCY_CONVERSION_ID, Value)
        End Set
    End Property



    Public Property CurrencyOfCoveragesId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CURRENCY_OF_COVERAGES_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CURRENCY_OF_COVERAGES_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CURRENCY_OF_COVERAGES_ID, Value)
        End Set
    End Property



    Public Property RemainingMfgDays() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_REMAINING_MFG_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_REMAINING_MFG_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_REMAINING_MFG_DAYS, Value)
        End Set
    End Property



    Public Property AcselProdCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ACSEL_PROD_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ACSEL_PROD_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ACSEL_PROD_CODE_ID, Value)
        End Set
    End Property



    Public Property CancellationReasonId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CANCELLATION_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CANCELLATION_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CANCELLATION_REASON_ID, Value)
        End Set
    End Property



    Public Property FullRefundDays() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_FULL_REFUND_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_FULL_REFUND_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_FULL_REFUND_DAYS, Value)
        End Set
    End Property



    Public Property AutoSetLiabilityId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_AUTO_SET_LIABILITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_AUTO_SET_LIABILITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_AUTO_SET_LIABILITY_ID, Value)
        End Set
    End Property



    Public Property DeductiblePercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEDUCTIBLE_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CoverageDeductibleId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COVERAGE_DEDUCTIBLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_COVERAGE_DEDUCTIBLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COVERAGE_DEDUCTIBLE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property IgnoreIncomingPremiumId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_IGNORE_INCOMING_PREMIUM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_IGNORE_INCOMING_PREMIUM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_IGNORE_INCOMING_PREMIUM_ID, Value)
        End Set
    End Property



    Public Property RepairDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_REPAIR_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_REPAIR_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_REPAIR_DISCOUNT_PCT, Value)
        End Set
    End Property



    Public Property ReplacementDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT, Value)
        End Set
    End Property



    Public Property IgnoreCoverageAmtId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_IGNORE_COVERAGE_AMT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_IGNORE_COVERAGE_AMT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_IGNORE_COVERAGE_AMT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property BackendClaimsAllowedId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_BACKEND_CLAIMS_ALLOWED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_BACKEND_CLAIMS_ALLOWED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_BACKEND_CLAIMS_ALLOWED_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property EditMfgTermId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EDIT_MFG_TERM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_EDIT_MFG_TERM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EDIT_MFG_TERM_ID, Value)
        End Set
    End Property



    Public Property AcctBusinessUnitId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property InstallmentPaymentId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_INSTALLMENT_PAYMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_INSTALLMENT_PAYMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_INSTALLMENT_PAYMENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DaysOfFirstPymt() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DAYS_OF_FIRST_PYMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_DAYS_OF_FIRST_PYMT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DAYS_OF_FIRST_PYMT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DaysToSendLetter() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DAYS_TO_SEND_LETTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_DAYS_TO_SEND_LETTER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DAYS_TO_SEND_LETTER, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DaysToCancelCert() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DAYS_TO_CANCEL_CERT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_DAYS_TO_CANCEL_CERT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DAYS_TO_CANCEL_CERT, Value)
        End Set
    End Property



    Public Property DeductByMfgId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEDUCT_BY_MFG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_DEDUCT_BY_MFG_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEDUCT_BY_MFG_ID, Value)
        End Set
    End Property



    Public Property PenaltyPct() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PENALTY_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_PENALTY_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PENALTY_PCT, Value)
        End Set
    End Property



    Public Property ClipPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CLIP_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_CLIP_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CLIP_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property IsCommPCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_IS_COMM_P_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_IS_COMM_P_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_IS_COMM_P_CODE_ID, Value)
        End Set
    End Property



    Public Property BaseInstallments() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_BASE_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_BASE_INSTALLMENTS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_BASE_INSTALLMENTS, Value)
        End Set
    End Property



    Public Property BillingCycleFrequency() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_BILLING_CYCLE_FREQUENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_BILLING_CYCLE_FREQUENCY), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_BILLING_CYCLE_FREQUENCY, Value)
        End Set
    End Property



    Public Property MaxInstallments() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_MAX_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_MAX_INSTALLMENTS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_MAX_INSTALLMENTS, Value)
        End Set
    End Property



    Public Property InstallmentsBaseReducer() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_INSTALLMENTS_BASE_REDUCER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_INSTALLMENTS_BASE_REDUCER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_INSTALLMENTS_BASE_REDUCER, Value)
        End Set
    End Property



    Public Property PastDueMonthsAllowed() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PAST_DUE_MONTHS_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_PAST_DUE_MONTHS_ALLOWED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PAST_DUE_MONTHS_ALLOWED, Value)
        End Set
    End Property



    Public Property CollectionReAttempts() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COLLECTION_RE_ATTEMPTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_COLLECTION_RE_ATTEMPTS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COLLECTION_RE_ATTEMPTS, Value)
        End Set
    End Property



    Public Property IncludeFirstPmt() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_INCLUDE_FIRST_PMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_INCLUDE_FIRST_PMT), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_INCLUDE_FIRST_PMT, Value)
        End Set
    End Property



    Public Property CollectionCycleTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_COLLECTION_CYCLE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_COLLECTION_CYCLE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_COLLECTION_CYCLE_TYPE_ID, Value)
        End Set
    End Property



    Public Property CycleDay() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CYCLE_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_CYCLE_DAY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CYCLE_DAY, Value)
        End Set
    End Property



    Public Property OffsetBeforeDueDate() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_OFFSET_BEFORE_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_OFFSET_BEFORE_DUE_DATE), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_OFFSET_BEFORE_DUE_DATE, Value)
        End Set
    End Property



    Public Property InsPremiumFactor() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_INS_PREMIUM_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_INS_PREMIUM_FACTOR), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_INS_PREMIUM_FACTOR, Value)
        End Set
    End Property



    Public Property ExtendCoverageId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EXTEND_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_EXTEND_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EXTEND_COVERAGE_ID, Value)
        End Set
    End Property



    Public Property ExtraMonsToExtendCoverage() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EXTRA_MONS_TO_EXTEND_COVERAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_EXTRA_MONS_TO_EXTEND_COVERAGE), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EXTRA_MONS_TO_EXTEND_COVERAGE, Value)
        End Set
    End Property



    Public Property ExtraDaysToExtendCoverage() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_EXTRA_DAYS_TO_EXTEND_COVERAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_EXTRA_DAYS_TO_EXTEND_COVERAGE), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_EXTRA_DAYS_TO_EXTEND_COVERAGE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property AllowDifferentCoverage() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_DIFFERENT_COVERAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_DIFFERENT_COVERAGE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_DIFFERENT_COVERAGE, Value)
        End Set
    End Property



    Public Property AllowNoExtended() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_NO_EXTENDED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_NO_EXTENDED), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_NO_EXTENDED, Value)
        End Set
    End Property



    Public Property NumOfClaims() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NUM_OF_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_NUM_OF_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NUM_OF_CLAIMS, Value)
        End Set
    End Property



    Public Property ClaimLimitBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CLAIM_LIMIT_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CLAIM_LIMIT_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CLAIM_LIMIT_BASED_ON_ID, Value)
        End Set
    End Property



    Public Property DaysToReportClaim() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DAYS_TO_REPORT_CLAIM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_DAYS_TO_REPORT_CLAIM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DAYS_TO_REPORT_CLAIM, Value)
        End Set
    End Property



    Public Property MarketingPromoId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_MARKETING_PROMO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_MARKETING_PROMO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_MARKETING_PROMO_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CustAddressRequiredId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_CUST_ADDRESS_REQUIRED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_CUST_ADDRESS_REQUIRED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_CUST_ADDRESS_REQUIRED_ID, Value)
        End Set
    End Property



    Public Property FirstPymtMonths() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_FIRST_PYMT_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_FIRST_PYMT_MONTHS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_FIRST_PYMT_MONTHS, Value)
        End Set
    End Property



    Public Property AllowMultipleRejectionsId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_MULTIPLE_REJECTIONS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_MULTIPLE_REJECTIONS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_MULTIPLE_REJECTIONS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DeductibleBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID, Value)
        End Set
    End Property



    Public Property ProRataMethodId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PRO_RATA_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_PRO_RATA_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PRO_RATA_METHOD_ID, Value)
        End Set
    End Property



    Public Property PayOutstandingPremiumId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PAY_OUTSTANDING_PREMIUM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_PAY_OUTSTANDING_PREMIUM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PAY_OUTSTANDING_PREMIUM_ID, Value)
        End Set
    End Property



    Public Property AuthorizedAmountMaxUpdates() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_AUTHORIZED_AMOUNT_MAX_UPDATES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_AUTHORIZED_AMOUNT_MAX_UPDATES), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_AUTHORIZED_AMOUNT_MAX_UPDATES, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RecurringPremiumId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RECURRING_PREMIUM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_RECURRING_PREMIUM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RECURRING_PREMIUM_ID, Value)
        End Set
    End Property



    Public Property RecurringWarrantyPeriod() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RECURRING_WARRANTY_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_RECURRING_WARRANTY_PERIOD), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RECURRING_WARRANTY_PERIOD, Value)
        End Set
    End Property



    Public Property AllowPymtSkipMonths() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_PYMT_SKIP_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_PYMT_SKIP_MONTHS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_PYMT_SKIP_MONTHS, Value)
        End Set
    End Property



    Public Property NumberOfDaysToReactivate() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NUMBER_OF_DAYS_TO_REACTIVATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_NUMBER_OF_DAYS_TO_REACTIVATE), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NUMBER_OF_DAYS_TO_REACTIVATE, Value)
        End Set
    End Property



    Public Property BillingCycleTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_BILLING_CYCLE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_BILLING_CYCLE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_BILLING_CYCLE_TYPE_ID, Value)
        End Set
    End Property



    Public Property DailyRateBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_DAILY_RATE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_DAILY_RATE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_DAILY_RATE_BASED_ON_ID, Value)
        End Set
    End Property



    Public Property AllowBillingAfterCncltn() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_BILLING_AFTER_CNCLTN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_BILLING_AFTER_CNCLTN), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_BILLING_AFTER_CNCLTN, Value)
        End Set
    End Property



    Public Property AllowCollctnAfterCncltn() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_COLLCTN_AFTER_CNCLTN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_COLLCTN_AFTER_CNCLTN), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_COLLCTN_AFTER_CNCLTN, Value)
        End Set
    End Property



    Public Property ReplacementPolicyClaimCount() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property FutureDateAllowForId() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_FUTURE_DATE_ALLOW_FOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_FUTURE_DATE_ALLOW_FOR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_FUTURE_DATE_ALLOW_FOR_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property IgnoreWaitingPeriodWsdPsd() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD, Value)
        End Set
    End Property



    Public Property AllowCoverageMarkupDtbn() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_ALLOW_COVERAGE_MARKUP_DTBN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_ALLOW_COVERAGE_MARKUP_DTBN), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_ALLOW_COVERAGE_MARKUP_DTBN, Value)
        End Set
    End Property



    Public Property NumOfRepairClaims() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS, Value)
        End Set
    End Property



    Public Property NumOfReplacementClaims() As LongType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ContractUploadDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS, Value)
        End Set
    End Property

    Public Property PaymentProcessingTypeID() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_PAYMENT_PROCESSING_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_PAYMENT_PROCESSING_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_PAYMENT_PROCESSING_TYPE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property ThirdPartyName() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_THIRD_PARTY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_THIRD_PARTY_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_THIRD_PARTY_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property ThirdPartyTaxID() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_THIRD_PARTY_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_THIRD_PARTY_TAX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_THIRD_PARTY_TAX_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property RDOName() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RDO_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_RDO_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RDO_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property RDOTaxID() As String
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RDO_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContractUploadDAL.COL_NAME_RDO_TAX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RDO_TAX_ID, Value)
        End Set
    End Property

    Public Property RDOPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_RDO_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ContractUploadDAL.COL_NAME_RDO_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_RDO_PERCENT, Value)
        End Set
    End Property



    <ValueMandatory("")>
    Public Property PolicyTypeCode() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_POLICY_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_POLICY_TYPE_CODE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_POLICY_TYPE_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property PolicyGenerationCode() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_POLICY_GENERATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_POLICY_GENERATION_CODE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_POLICY_GENERATION_CODE, Value)
        End Set
    End Property



    Public Property LineOfBusinessCode() As Guid
        Get
            CheckDeleted()
            If Row(ContractUploadDAL.COL_NAME_LINE_OF_BUSINESS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContractUploadDAL.COL_NAME_LINE_OF_BUSINESS_CODE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContractUploadDAL.COL_NAME_LINE_OF_BUSINESS_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ContractUploadDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetPreValidatedContractsForUpload(ByVal UploadSessionId As String) As DataSet
        Try
            Dim dal As New ContractUploadDAL
            Return dal.LoadPreValidatedContractsForUpload(UploadSessionId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function UpdatePreValidatedContractRecord(preValidatedContractId As Guid, ByVal strValidationErrors As String) As DataSet
        Try
            Dim dal As New ContractUploadDAL
            Return dal.UpdatePreValidatedContractRecord(preValidatedContractId, strValidationErrors)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class