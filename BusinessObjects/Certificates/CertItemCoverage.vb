'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/13/2004)  ********************
Imports System.Collections.Generic
Imports Assurant.Common.Validation

Public Class CertItemCoverage
    Inherits BusinessObjectBase

#Region "Deductible Type"
    Public Class DeductibleType
        Private m_deductible As DecimalType
        Private m_deductibleBasedOnId As Guid
        Private m_deductibleBasedOn As String
        Private m_expressionId As Nullable(Of Guid)

        Public Property ExpressionId As Nullable(Of Guid)
            Get
                Return m_expressionId
            End Get
            Set(ByVal value As Nullable(Of Guid))
                m_expressionId = value
            End Set
        End Property

        Public ReadOnly Property DeductibleAmount As DecimalType
            Get
                If (Me.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                    Return Me.Deductible
                Else
                    Return New DecimalType(0D)
                End If
            End Get
        End Property

        Public ReadOnly Property DeductiblePercentage As DecimalType
            Get
                If (Me.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                    Return New DecimalType(0D)
                Else
                    Return Me.Deductible
                End If
            End Get
        End Property

        Public Property DeductibleBasedOnId As Guid
            Get
                Return m_deductibleBasedOnId
            End Get
            Set(ByVal value As Guid)
                m_deductibleBasedOnId = value
                m_deductibleBasedOn = LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, Me.DeductibleBasedOnId)
            End Set
        End Property

        Public ReadOnly Property DeductibleBasedOn As String
            Get
                Return m_deductibleBasedOn
            End Get
        End Property

        Public Property Deductible As DecimalType
            Get
                If (m_deductible Is Nothing) Then
                    Return New DecimalType(0D)
                Else
                    Return m_deductible
                End If
            End Get
            Set(ByVal value As DecimalType)
                m_deductible = value
            End Set
        End Property
    End Class
#End Region

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

    Public Sub New(ByVal CertEndorseBO As CertEndorse)
        MyBase.New(False)
        Me.Dataset = CertEndorseBO.Dataset
        Me.Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CertItemCoverageDAL
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
            Dim dal As New CertItemCoverageDAL
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
            If Row(CertItemCoverageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertItemId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DeductibleBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(ContractDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(ContractDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(ContractDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID, value)
        End Set
    End Property

    <ValueMandatory("")> _
        Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property CoverageTypeCode() As String
        Get
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, Me.CoverageTypeId)
        End Get
    End Property


    Public Property OriginalRegionId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_ORIGINAL_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_ORIGINAL_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_ORIGINAL_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidCoverageDates("")> _
    Public Property BeginDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_BEGIN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemCoverageDAL.COL_NAME_BEGIN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_BEGIN_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EndDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemCoverageDAL.COL_NAME_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_END_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LiabilityLimits() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_LIABILITY_LIMITS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_LIABILITY_LIMITS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_LIABILITY_LIMITS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Deductible() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    Public Property DeductiblePercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property GrossAmtReceived() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_GROSS_AMT_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_GROSS_AMT_RECEIVED), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_GROSS_AMT_RECEIVED, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PremiumWritten() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_PREMIUM_WRITTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_PREMIUM_WRITTEN), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_PREMIUM_WRITTEN, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OriginalPremium() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_ORIGINAL_PREMIUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_ORIGINAL_PREMIUM), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_ORIGINAL_PREMIUM, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LossCost() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_LOSS_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_LOSS_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_LOSS_COST, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Commission() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_COMMISSION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AdminExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_ADMIN_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_ADMIN_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_ADMIN_EXPENSE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MarketingExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_MARKETING_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_MARKETING_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_MARKETING_EXPENSE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Other() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_OTHER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_OTHER), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_OTHER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property SalesTax() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_SALES_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_SALES_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_SALES_TAX, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax1() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX1) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX1), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX1, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax2() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX2) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX2), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX2, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax3() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX3) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX3), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX3, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax4() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX4) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX4), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX4, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax5() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX5) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX5), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX5, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tax6() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_TAX6) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_TAX6), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_TAX6, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MtdPayments() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_MTD_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_MTD_PAYMENTS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_MTD_PAYMENTS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property YtdPayments() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_YTD_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_YTD_PAYMENTS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_YTD_PAYMENTS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AssurantGwp() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_ASSURANT_GWP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_ASSURANT_GWP), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_ASSURANT_GWP, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MarkupCommission() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION, Value)
        End Set
    End Property

    Public Property DealerDiscountAmt() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_AMT, Value)
        End Set
    End Property

    Public Property DealerDiscountPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_DEALER_DISCOUNT_PERCENT, Value)
        End Set
    End Property

    Public Property IsClaimAllowed() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED, Value)
        End Set
    End Property

    Public Property IsDiscount() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_IS_DISCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_IS_DISCOUNT), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_IS_DISCOUNT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
      Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property
    <ValidNumericRange("", Max:=9999)> _
    Public Property RepairDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertItemCoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=9999)> _
    Public Property ReplacementDiscountPct() As LongType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertItemCoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT, Value)
        End Set
    End Property

    Public Property MarkupCommissionVat() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION_VAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION_VAT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_MARKUP_COMMISSION_VAT, Value)
        End Set
    End Property

    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property
    Public Property CoverageRemainLiabilityLimit() As String
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COVERAGE_REMAIN_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertItemCoverageDAL.COL_NAME_COVERAGE_REMAIN_LIABILITY_LIMIT), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property
    Public Property CoverageLiabilityLimit() As String
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertItemCoverageDAL.COL_NAME_COVERAGE_LIABILITY_LIMIT), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=999)> _
    Public Property CoverageDuration() As LongType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_COVERAGE_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertItemCoverageDAL.COL_NAME_COVERAGE_DURATION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_COVERAGE_DURATION, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=999)> _
    Public Property NoOfRenewals() As LongType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_NO_OF_RENEWALS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertItemCoverageDAL.COL_NAME_NO_OF_RENEWALS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_NO_OF_RENEWALS, Value)
        End Set
    End Property

    Public Property RenewalDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_RENEWAL_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemCoverageDAL.COL_NAME_RENEWAL_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_RENEWAL_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property IsCoverageEffective As Boolean
        Get
            If (Not Me.BeginDate Is Nothing AndAlso Not Me.EndDate Is Nothing) Then
                If Date.Today >= Me.BeginDate.Value AndAlso Date.Today <= Me.EndDate.Value Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    '5623
    Public Function IsCoverageEffectiveForGracePeriod(ByVal ReportedDate As DateType) As Boolean

        Dim oDealer As New Dealer(Certificate.DealerId)
        Dim ClaimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()

        Dim gracePeriodDays As Integer = If(oDealer.GracePeriodDays, 0)
        Dim gracePeriodMonths As Integer = If(oDealer.GracePeriodMonths, 0)

        Dim dtGracePeriodEndDate As Date = EndDate.Value.AddMonths(gracePeriodMonths).AddDays(gracePeriodDays)
        If (Not Me.BeginDate Is Nothing AndAlso Not Me.EndDate Is Nothing) Then

            If ReportedDate >= Me.BeginDate.Value AndAlso ReportedDate <= dtGracePeriodEndDate Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public ReadOnly Property ReinsuranceStatusId() As Guid
        Get
            If Row(CertItemCoverageDAL.COL_NAME_REINSURANCE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_REINSURANCE_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property ReinsuranceRejectReason() As String
        Get
            If Row(CertItemCoverageDAL.COL_NAME_REINSURANCE_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertItemCoverageDAL.COL_NAME_REINSURANCE_REJECT_REASON), String))
            End If
        End Get
    End Property

    Public Property DeductibleExpressionId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID, Value)
        End Set
    End Property

#End Region


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertItemCoverageDAL
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
    'Public Shared Function GetPremiumTotals(ByVal certId As Guid) As DataView
    '    Try
    '        Dim dal As New CertItemCoverageDAL
    '        Dim ds As Dataset

    '        ds = dal.LoadPremiumTotals(certId)
    '        Return ds.Tables(CertItemCoverageDAL.TABLE_PREMIUM_TOTALS).DefaultView

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function

    Public Shared Function GetClaims(ByVal certItemCoverageId As Guid) As DataView
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As New DataSet

            ds = dal.LoadClaims(certItemCoverageId)
            Return ds.Tables(CertItemCoverageDAL.TABLE_CLAIMS).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAllClaims(ByVal certItemCoverageId As Guid) As DataView
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As New DataSet

            ds = dal.LoadAllClaims(certItemCoverageId)
            Return ds.Tables(CertItemCoverageDAL.TABLE_CLAIMS).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetItemCoverages(ByVal certId As Guid) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadList(certId, langId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetItemCoveragesWithProdSplitWarr(ByVal certId As Guid) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadCovListWithProdSplitWarr(certId, langId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetEligibleCoverages(ByVal certId As Guid, ByVal dateOfLoss As Date) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadEligibleCoverages(certId, langId, dateOfLoss).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetMainItemCoverages(ByVal certId As Guid) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadList(certId, langId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimCoverageType(ByVal certId As Guid, ByVal certItemCoverageId As Guid, ByVal lossDate As Date, ByVal claimStatus As String, ByVal invoiceProcessDate As Date) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadClaimCoverageType(certId, langId, certItemCoverageId, lossDate, claimStatus, invoiceProcessDate).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCurrentProductCodeCoverages(ByVal certId As Guid) As CertItemCoverageSearchDV
        Try
            Dim dal As New CertItemCoverageDAL
            Dim ds As DataSet
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New CertItemCoverageSearchDV(dal.LoadCurrentProductCodeCoverage(certId, langId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimWaitingPeriod(ByVal certItemCoverageId As Guid, ByRef ignoreWaitingPeriodID As Guid) As Integer
        Try
            Dim dal As New CertItemCoverageDAL
            Dim dv As New DataView

            Return dal.GetClaimWaitingPeriod(certItemCoverageId, ignoreWaitingPeriodID)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Class CertItemCoverageSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_ITEM_COVERAGE_ID As String = "cert_item_coverage_id"
        Public Const COL_RISK_TYPE As String = "risk_type_description"
        Public Const COL_BEGIN_DATE As String = "begin_date"
        Public Const COL_END_DATE As String = "end_date"
        Public Const COL_COVERAGE_TOTAL_PAID_AMOUNT = "Cov_Total_Paid_Amount"
        Public Const COL_COVERAGE_REMAIN_LIABILITY_LIMIT As String = "Cov_Remain_Liability_Limit"
        Public Const COL_COVERAGE_TYPE As String = "coverage_type_description"
        Public Const COL_COVERAGE_TYPE_ID As String = "coverage_type_id"
        Public Const COL_COVERAGE_TYPE_CODE As String = "coverage_type_code"
        Public Const COL_AUTHORIZED_AMOUNT As String = "authorized_amount"
        Public Const COL_CERT_ITEM_COVERAGE_ID_HEX As String = "cert_item_coverage_id_hex"
        Public Const COL_CERT_ITEM_COVERAGE_SEQUENCE As String = "sequence"
        Public Const COL_CERT_ITEM_COVERAGE_COVERAGE_DURATION As String = "Coverage_Duration"
        Public Const COL_CERT_ITEM_COVERAGE_NO_OF_RENEWALS As String = "No_of_Renewals"
        Public Const COL_CERT_ITEM_COVERAGE_RENEWAL_DATE As String = "Renewal_Date"
        'Public Const COL_Ext_Begin_KM_MI As String = "Ext_Begin_KM_MI"
        'Public Const COL_Ext_End_KM_MI As String = "Ext_End_KM_MI"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "Public Methods"
    Public Shared Function GetDeductible(ByVal certItemCoverageId As Guid, ByVal methodOfRepairId As Guid) As DeductibleType
        Try
            Dim returnValue As New DeductibleType
            returnValue.DeductibleBasedOnId = LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED")
            returnValue.Deductible = 0
            returnValue.ExpressionId = Nothing

            If (LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, methodOfRepairId) <> Codes.METHOD_OF_REPAIR__RECOVERY) Then
                Dim oCertItemCoverageDed As CertItemCoverageDeductible
                oCertItemCoverageDed = CertItemCoverageDeductible.GetDeductible(certItemCoverageId, methodOfRepairId)
                If (oCertItemCoverageDed Is Nothing) Then
                    Dim oCertItemCoverage As New CertItemCoverage(certItemCoverageId)
                    If (Not oCertItemCoverage Is Nothing) Then
                        returnValue.DeductibleBasedOnId = oCertItemCoverage.DeductibleBasedOnId

                        If (returnValue.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__FIXED) Then
                            If (oCertItemCoverage.Deductible Is Nothing) Then
                                returnValue.Deductible = New DecimalType(0D)
                            Else
                                returnValue.Deductible = oCertItemCoverage.Deductible
                            End If

                        ElseIf returnValue.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__EXPRESSION Then
                            returnValue.Deductible = 0
                            returnValue.ExpressionId = oCertItemCoverage.DeductibleExpressionId
                        ElseIf returnValue.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__COMPUTED_EXTERNALLY Then
                            returnValue.Deductible = New DecimalType(0D)
                        Else
                            If (oCertItemCoverage.DeductiblePercent Is Nothing) Then
                                returnValue.Deductible = New DecimalType(0D)
                            Else
                                returnValue.Deductible = oCertItemCoverage.DeductiblePercent
                            End If
                        End If
                    End If
                Else
                    returnValue.DeductibleBasedOnId = oCertItemCoverageDed.DeductibleBasedOnId
                    returnValue.Deductible = oCertItemCoverageDed.Deductible
                    returnValue.ExpressionId = oCertItemCoverageDed.DeductibleExpressionId
                End If
            End If
            Return returnValue
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "List Methods"
    Public Class ItemCovList
        Inherits BusinessObjectListEnumerableBase(Of Certificate, CertItemCoverage)
        Public Sub New(ByVal parent As Certificate)
            MyBase.New(parent.Dataset.Tables(CertItemCoverageDAL.TABLE_NAME), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function
    End Class
    Public Shared Function GetItemCovListForCertificate(ByVal certId As Guid, ByVal parent As BusinessObjectBase) As ItemCovList
        If parent.Dataset.Tables.IndexOf(CertItemCoverageDAL.TABLE_NAME) < 0 Then
            Dim dal As New CertItemCoverageDAL
            dal.LoadAllItemCoveragesForCertificate(certId, parent.Dataset)
        End If
        Return New ItemCovList(parent)
    End Function

    Public Shared Function LoadAllItemCoveragesForGalaxyCertificate(ByVal certId As Guid) As DataSet
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim dal As New CertItemCoverageDAL
        Return dal.LoadAllItemCoveragesForGalaxyCertificate(certId, compId)
    End Function

    Public Shared Function LoadAllItemCoveragesForGalaxyClaim(ByVal certId As Guid) As DataSet
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim dal As New CertItemCoverageDAL
        Return dal.LoadAllItemCoveragesForGalaxyClaim(certId, compId)
    End Function

    Public Shared Function LoadAllItemCoveragesForGalaxyClaimUpdate(ByVal masterClaimNumber As String) As DataSet
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim dal As New CertItemCoverageDAL
        Return dal.LoadAllItemCoveragesForGalaxyClaimUpdate(masterClaimNumber, compId)
    End Function


#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidCoverageDates
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertItemCoverage = CType(objectToValidate, CertItemCoverage)
            If Not (obj.BeginDate Is Nothing Or obj.EndDate Is Nothing) Then
                If obj.BeginDate.Value > obj.EndDate.Value Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region

#Region "Lazy Initialize Fields"
    Private _dealer As Dealer = Nothing
    Private _certificate As Certificate = Nothing
    Private _certItem As CertItem = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Dealer As Dealer
        Get
            If (_dealer Is Nothing) Then
                If Not Me.Certificate Is Nothing Then
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                ElseIf Not Me.CertificateItem Is Nothing Then
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                Else
                    Me.CertificateItem = New CertItem(Me.CertItemId, Me.Dataset)
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                    Me.Dealer = New Dealer(Me.Certificate.DealerId, Me.Dataset)
                End If
            End If
            Return _dealer
        End Get
        Private Set(ByVal value As Dealer)
            _dealer = value
        End Set
    End Property

    Public Property Certificate As Certificate
        Get
            If (_certificate Is Nothing) Then
                If Not Me.CertificateItem Is Nothing Then
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                Else
                    Me.CertificateItem = New CertItem(Me.CertItemId, Me.Dataset)
                    Me.Certificate = New Certificate(Me.CertificateItem.CertId, Me.Dataset)
                End If
            End If
            Return _certificate
        End Get
        Private Set(ByVal value As Certificate)
            _certificate = value
            Me.Dealer = Nothing
        End Set
    End Property

    Public Property CertificateItem As CertItem
        Get
            If (_certItem Is Nothing) Then
                Me.CertificateItem = New CertItem(Me.CertItemId, Me.Dataset)
            End If
            Return _certItem
        End Get
        Private Set(ByVal value As CertItem)
            _certItem = value
            Me.Certificate = Nothing
            Me.Dealer = Nothing
        End Set
    End Property
#End Region

#Region "Validation for claims"

    Public Function IsCoverageValidToOpenClaim(ByRef errMsgList As List(Of String), ByRef warningMsgList As List(Of String), Optional ByVal ReportedDate As DateType = Nothing) As Boolean
        If ReportedDate Is Nothing Then ReportedDate = DateTime.Today
        Dim errMsg As List(Of String) = New List(Of String)
        Dim warningMsg As List(Of String) = New List(Of String)
        Dim claimsDV, userRolesDV As DataView
        Dim oContract As Contract
        Dim tempDate As Date
        Dim dDate As Date
        Dim susp As Int32
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        Dim isclaimselected As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Dim isRequiredItemDescription As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
        Dim coverageType, networkId As String
        Dim flag As Boolean = True
        Dim claimsManager, officeManager, IHQSup As Boolean
        Dim callCenterAgent, callCenterSupervisor, claimsRole, claimsAnalyst, claimSupport, commentsRole, csrRole, csr2Role, countySuperUser As Boolean
        Dim otherAllowedRoles As Boolean = False
        claimsManager = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER)
        officeManager = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__OFFICE_MANAGER)
        IHQSup = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)
        callCenterAgent = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CALL_CENTER_AGENT)
        callCenterSupervisor = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CALL_CENTER_SUPERVISOR)
        claimsRole = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS)
        claimsAnalyst = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_ANALYST)
        claimSupport = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIM_SUPPORT)
        commentsRole = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__COMMENTS)
        csrRole = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CSR)
        csr2Role = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CSR2)
        countySuperUser = ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__COUNTY_SUPERUSER)
        otherAllowedRoles = callCenterAgent OrElse callCenterSupervisor OrElse claimsRole OrElse claimsAnalyst OrElse claimSupport OrElse commentsRole OrElse csrRole OrElse csr2Role OrElse countySuperUser

        oContract = Contract.GetContract(Me.Certificate.DealerId, Me.Certificate.WarrantySalesDate.Value)
        Dim oDealer As New Dealer(Certificate.DealerId)

        'Req-1016 - Start
        Dim emptyGuid As Guid = Guid.Empty
        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
        'Req-1016 - end

        If Me.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE OrElse (Me.Certificate.StatusCode <> Codes.CERTIFICATE_STATUS__ACTIVE AndAlso (claimsManager OrElse IHQSup OrElse otherAllowedRoles)) Then

            coverageType = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), Me.CoverageTypeId)

            If Me.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso oDealer.IsGracePeriodSpecified Then
                'If oDealer.IsGracePeriodSpecified Then
                If coverageType <> Codes.COVERAGE_TYPE__MANUFACTURER AndAlso coverageType <> Codes.COVERAGE_TYPE__MANUFACTURER_MAIN_PARTS AndAlso Not (Me.IsCoverageEffectiveForGracePeriod(ReportedDate)) Then
                    If Date.Today > Me.BeginDate.Value Then
                        If ((Not claimsManager) And (Not officeManager) And (Not IHQSup) And (Not otherAllowedRoles)) Then
                            flag = False
                        End If
                    End If
                    'errMsg.Add("COVERAGE IS NOT IN EFFECT")
                    If Not warningMsg.Contains("COVERAGE IS NOT IN EFFECT") Then
                        warningMsg.Add("COVERAGE IS NOT IN EFFECT")
                    End If
                End If
            ElseIf coverageType <> Codes.COVERAGE_TYPE__MANUFACTURER AndAlso coverageType <> Codes.COVERAGE_TYPE__MANUFACTURER_MAIN_PARTS AndAlso Not (Me.IsCoverageEffective) Then
                If Date.Today > Me.BeginDate.Value Then
                    If ((Not claimsManager) And (Not officeManager) And (Not IHQSup) And (Not otherAllowedRoles)) Then
                        flag = False
                    End If
                End If
                'errMsg.Add("COVERAGE IS NOT IN EFFECT")
                If Not warningMsg.Contains("COVERAGE IS NOT IN EFFECT") Then
                    warningMsg.Add("COVERAGE IS NOT IN EFFECT")
                End If

            ElseIf Not ((LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, IsClaimAllowed()) = "Y") Or (IsClaimAllowed = Nothing)) Then
                flag = flag And False
                errMsg.Add("Claim Not Allowed")
            End If

            'Manufacturer missing
            If Me.Certificate.Product.AllowRegisteredItems = Codes.EXT_YESNO_N AndAlso IsManufacturerMissing() Then
                flag = flag And False
                errMsg.Add("MANUFACTURER NAME IS MISSING")
            End If

            'Zip missing
            If IsZipMissing() Then
                flag = flag And False
                errMsg.Add("zip_is_missing")
            End If

            If Date.Today < Me.BeginDate.Value Then
                flag = flag And False
            End If

            If Me.Certificate.IsCompanyTypeInsurance Then
                If IsCustomerNameMissing() OrElse IsIdentificationNumberMissing() Then
                    flag = flag And False
                    errMsg.Add("CUSTOMER_NAME_OR_TAX_ID_MISSING")
                End If
            End If

            ' Check if Compute Deductible by List Price and SKU is Missing
            If (Me.DeductibleBasedOnId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "LIST"))) Then
                If Not Me.CertificateItem.IsEquipmentRequired Then
                    If (Len(Trim(Me.CertificateItem.SkuNumber)) = 0) Then
                        flag = flag And False
                        errMsg.Add("SKU_IS_REQUIRED")
                    End If
                End If

            End If

            'Suspended contract
            'oContract = Contract.GetCurrentContract(Me.moCertificate.DealerId)
            If Not oContract Is Nothing Then
                Dim ignoreWaitingPeriodID As Guid
                Dim intClaimWaitingPeriod As Integer = Me.GetClaimWaitingPeriod(Me.Id, ignoreWaitingPeriodID)
                tempDate = Me.Certificate.WarrantySalesDate.Value
                dDate = tempDate.AddDays(intClaimWaitingPeriod)
                If (Me.Certificate.WarrantySalesDate.Equals(Me.Certificate.ProductSalesDate)) And ignoreWaitingPeriodID.Equals(yesId) Then
                Else
                    If dDate > System.DateTime.Now And Not claimsManager Then
                        flag = flag And False
                        errMsg.Add("IN CLAIM WAITING PERIOD")
                    End If
                End If

                If ((Not oContract.RecurringPremiumId.Equals(emptyGuid)) And (Not oContract.RecurringPremiumId.Equals(singlePremiumId))) Then
                    If Not Me.Certificate.DatePaidFor Is Nothing Then
                        susp = Date.Today.Subtract(Me.Certificate.DatePaidFor.Value).Days
                    Else
                        susp = Date.Today.Subtract(Me.Certificate.CreatedDate.Value).Days
                    End If

                    'Check user roles
                    If susp > oContract.SuspenseDays.Value Then
                        errMsg.Add("SUSPENDED CERTIFICATE")
                        If (Not (claimsManager) AndAlso Not (officeManager)) Then
                            flag = flag And False
                        End If
                    End If
                End If
            End If

            'Invoice Number missing
            If IsInvoiceNumberMissing() Then
                flag = flag And False
                errMsg.Add("INVOICE NUMBER MISSING")
            End If

            'Product Sales Date missing
            If IsProductSalesDateMissing() Then
                flag = flag And False
                errMsg.Add("PRODUCT_SALES_DATE_MISSING")
            End If
        Else
            flag = flag And False
        End If

        If coverageType <> Codes.COVERAGE_TYPE__MANUFACTURER Then
            If Me.Certificate.StatusCode <> Codes.CERTIFICATE_STATUS__ACTIVE Then
                If (IsManufacturerMissing()) Or (IsZipMissing()) Then
                    flag = flag And False
                ElseIf Not claimsManager AndAlso Not officeManager AndAlso Not IHQSup AndAlso Not otherAllowedRoles Then
                    flag = flag And False
                End If
            End If
        End If

        If Not (IsManufacturerMissing()) AndAlso Not (IsZipMissing()) Then
            If Date.Today > Me.BeginDate.Value AndAlso Date.Today > Me.EndDate.Value Then
                If oContract Is Nothing Then
                    flag = flag And False
                    errMsg.Add(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
                ElseIf (oContract.BackEndClaimsAllowedId.Equals(yesId)) Then
                    flag = flag And True
                Else
                    If ((Not claimsManager) AndAlso (Not officeManager) AndAlso (Not IHQSup) AndAlso Not otherAllowedRoles) Then
                        flag = flag And False
                    End If
                End If
            End If
        End If

        If oContract Is Nothing Then
            flag = flag And False
            errMsg.Add(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
        ElseIf IsDepreciationScheduleNotDefined(oContract.Id) Then
            flag = flag And False
            errMsg.Add("DEPRECIATION_SCHEDULE_NOT_DEFINED")
        End If
        If Me.CertificateItem.ItemDescription Is Nothing AndAlso isRequiredItemDescription.Equals((New Company(Me.CompanyId)).RequireItemDescriptionId) Then
            flag = flag And False
            errMsg.Add("ITEM_DESCRIPTION_IS_MISSING")
        End If
        If Me.CertificateItem.IsCustomerAddressRequired Then
            flag = flag And False
            errMsg.Add("CUSTOMER_ADDRESS_REQUIRED")
        End If
        errMsgList = errMsg
        warningMsgList = warningMsg
        Return flag
    End Function

    Public Function IsManufacturerMissing() As Boolean

        If Not Me.CertificateItem.IsEquipmentRequired Then
            If Me.CertificateItem.ManufacturerId.Equals(Guid.Empty) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function IsCustomerNameMissing() As Boolean
        If Me.Certificate.CustomerName Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsIdentificationNumberMissing() As Boolean
        If Me.Certificate.IdentificationNumber Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsZipMissing() As Boolean
        Dim addr As Address

        addr = Me.Certificate.AddressChild(False)
        If Not addr Is Nothing Then
            If Me.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE AndAlso _
                addr.PostalCode = "" Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If


    End Function

    Public Function GetClaims() As Boolean
        Dim claimsDV As DataView

        claimsDV = Me.GetAllClaims(Me.Id)

        If claimsDV.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetCertClaims() As Boolean
        Dim claimsDV As DataView

        Dim ClaimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
        claimsDV = ClaimBO.GetCertClaims(Me.CertId)

        If claimsDV.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsInvoiceNumberMissing() As Boolean
        If Me.Certificate.InvoiceNumber Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsProductSalesDateMissing() As Boolean
        If Me.Certificate.ProductSalesDate Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsDepreciationScheduleNotDefined(ByVal ContractID As Guid) As Boolean

        Dim al As ArrayList = Claim.CalculateLiabilityLimit(Me.CertId, ContractID, Me.Id)
        If CType(al(1), Integer) <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function IsPossibleWarrantyClaim(ByRef msg As String) As Boolean

        Dim claimsDV As DataView = Me.GetAllClaims(Me.Id)

        Dim oClaim As ClaimBase

        Dim isDaysLimitExceeded As Boolean = True
        Dim elpasedDaysSinceRepaired As Long

        For Each row As DataRow In claimsDV.Table.Rows
            Dim clmId As Guid = New Guid(CType(row.Item(0), Byte()))
            oClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(clmId)
            If oClaim.ClaimAuthorizationType = ClaimAuthorizationType.Single Then

                elpasedDaysSinceRepaired = CType(oClaim, Claim).ServiceCenterObject.ServiceWarrantyDays.Value

                If Not oClaim.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                    If Not CType(oClaim, Claim).PickUpDate Is Nothing Then
                        elpasedDaysSinceRepaired = Date.Now.Subtract(CType(oClaim, Claim).PickUpDate.Value).Days
                    ElseIf Not CType(oClaim, Claim).RepairDate Is Nothing Then
                        elpasedDaysSinceRepaired = Date.Now.Subtract(CType(oClaim, Claim).RepairDate.Value).Days
                    End If
                    If elpasedDaysSinceRepaired < CType(oClaim, Claim).ServiceCenterObject.ServiceWarrantyDays.Value Then
                        isDaysLimitExceeded = False
                        Exit For
                    End If
                End If
            End If
            If oClaim.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                Dim flag As Boolean = False
                Dim claim As MultiAuthClaim = CType(oClaim, MultiAuthClaim)

                For Each auth As ClaimAuthorization In claim.NonVoidClaimAuthorizationList
                    elpasedDaysSinceRepaired = auth.ServiceCenterObject.ServiceWarrantyDays.Value

                    If Not claim.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) Then
                        If Not auth.PickUpDate Is Nothing Then
                            elpasedDaysSinceRepaired = Date.Now.Subtract(auth.PickUpDate.Value).Days
                        ElseIf Not auth.RepairDate Is Nothing Then
                            elpasedDaysSinceRepaired = Date.Now.Subtract(auth.RepairDate.Value).Days
                        End If

                        If elpasedDaysSinceRepaired < auth.ServiceCenterObject.ServiceWarrantyDays.Value Then
                            isDaysLimitExceeded = False
                            flag = True
                            Exit For
                        End If
                    End If
                Next
                If flag Then Exit For
            End If

        Next

        Dim oContract As Contract
        Dim CoverageType As String

        oContract = Contract.GetContract(Me.Certificate.DealerId, Me.Certificate.WarrantySalesDate.Value)
        CoverageType = LookupListNew.GetCodeFromId(LookupListNew.GetCoverageTypeLookupList(Authentication.LangId), Me.CoverageTypeId)

        Dim ClaimControl As Boolean = False

        If Not oContract Is Nothing Then
            If LookupListNew.GetCodeFromId(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, oContract.ClaimControlID) = "Y" Then
                ClaimControl = True
            End If
        End If

        If CoverageType <> Codes.COVERAGE_TYPE__MANUFACTURER Then
            If Not Me.Certificate.MethodOfRepairId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)) _
               And ClaimControl Then
                msg = Messages.MSG_DEALER_USER_CLAIM_INTERFACES
                Return True
            End If
            If Not isDaysLimitExceeded And _
               Not ClaimControl Then
                msg = Messages.MSG_POTENTIAL_SERVICE_WARRANTY
                Return True
            End If
        End If

        Return False
    End Function
#End Region

End Class


