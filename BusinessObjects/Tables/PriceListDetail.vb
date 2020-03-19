'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/31/2012)  ********************
Imports Assurant.Common.Types
Imports System.Linq
Imports System.Collections.Generic
Imports System.Text

Public Class PriceListDetail
    Inherits BusinessObjectBase
    Implements IExpirable


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
            Dim dal As New PriceListDetailDAL
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
            Dim dal As New PriceListDetailDAL
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

#Region "Constants"
    Private Const COVERAGE_RATE_FORM001 As String = "COVERAGE_RATE_FORM001" ' 0<= LowPrice <1*10^6
    Private Const COVERAGE_RATE_FORM002 As String = "COVERAGE_RATE_FORM002" ' 0<= HighPrice <1*10^6
    Private Const COVERAGE_RATE_FORM009 As String = "COVERAGE_RATE_FORM009" ' LowPrice Must be less or equal than HighPrice
    Private Const COVERAGE_RATE_FORM011 As String = "COVERAGE_RATE_FORM011" ' There should be no overlaps (low/high)
    Private Const MIN_DOUBLE As Double = 0.0
    Private Const MAX_DOUBLE As Double = 999999.99
    Public Const Risk_Type As String = "Any"

    Private Const THRESHOLD As Double = 0.01

    Private Const PRICE_LIST_DETAIL_ID As Integer = 0
    Private Const LOW_PRICE As Integer = 1
    Private Const HIGH_PRICE As Integer = 2
    Private Const ONLY_NUMBERS_ALLOWED_FOR_PRICE As String = "ONLY_DIGITS_ALLOWED_FOR_PRICE"

    'US 224089
    Private Const PART_INFO_XML_NODE As String = "<Part><PartDescriptionId>{0}</PartDescriptionId><Cost>{1}</Cost></Part>"

#End Region

#Region "Properties"

    'Following is a dummy property just implemented to handle interface constraint
    Public Property Code() As String Implements IExpirable.Code
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
            'do nothing
        End Set
    End Property

    'Key Property
    <ValidateRiskTypeEquipClassEquipment("")>
    Public ReadOnly Property Id() As Guid Implements IExpirable.ID
        Get
            If Row(PriceListDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)>
    Public Property Effective() As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Date.Now
            Else
                Return New DateTimeType(CType(Row(PriceListDetailDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)>
    Public Property Expiration() As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(PriceListDetailDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    'Following is a dummy property just implemented to handle interface constraint
    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set(ByVal value As Guid)
            'do nothing
        End Set
    End Property

    Public Overrides ReadOnly Property IsNew() As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property


    <ValueMandatory("")>
    Public Property PriceListId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PRICE_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PRICE_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PRICE_LIST_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ServiceClassId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property

    Public Property ServiceTypeId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property

    Public Property ServiceLevelId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50), ValidatePriceListSKU("")>
    Public Property VendorSku() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_VENDOR_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_VENDOR_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_VENDOR_SKU, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property VendorSkuDescription() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidNumericRange("LowPrice", Min:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM001), ValidPriceBandRange("")>
    Public Property PriceBandRangeFrom() As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM002), ValidatePriceBandRangeTo("")>
    Public Property PriceBandRangeTo() As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO, Value)
        End Set
    End Property

    Public Property EquipmentClassId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_CLASS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_EQUIPMENT_CLASS_ID, Value)
        End Set
    End Property

    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property
    <ValidateConditionType("")>
    Public Property ConditionId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_CONDITION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_CONDITION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_CONDITION_ID, Value)
        End Set
    End Property
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=ONLY_NUMBERS_ALLOWED_FOR_PRICE)>
    Public Property Price() As DecimalType
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceListDetailDAL.COL_NAME_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PRICE, Value)
        End Set
    End Property
    <ValueMandatory("Currency")>
    Public Property CurrencyId() As Guid
        Get
            CheckDeleted()
            If (Row(PriceListDetailDAL.COL_NAME_CURRENCY) Is DBNull.Value) Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_CURRENCY), Byte()))
            End If
        End Get
        Set(value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_CURRENCY, value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property PriceListDetailTypeId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_TYPE), Byte()))
            End If
        End Get
        Set(value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_TYPE, value)
        End Set
    End Property
    Public Property PriceWithSymbol() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_CURRENCY_SYMBOL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_CURRENCY_SYMBOL), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_CURRENCY_SYMBOL, value)
        End Set
    End Property
    Public Property PriceLowWithSymbol() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PRICE_LOW_RANGE_WITH_SYMBOL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PRICE_LOW_RANGE_WITH_SYMBOL), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PRICE_LOW_RANGE_WITH_SYMBOL, value)
        End Set
    End Property
    Public Property PriceHighWithSymbol() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PRICE_HIGH_RANGE_WITH_SYMBOL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PRICE_HIGH_RANGE_WITH_SYMBOL), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PRICE_HIGH_RANGE_WITH_SYMBOL, value)
        End Set
    End Property
    Public ReadOnly Property CalculationPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_CALCULATION_PERCENTAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceListDetailDAL.COL_NAME_CALCULATION_PERCENTAGE), Decimal))
            End If
        End Get
    End Property

    Public Property ReplacementTaxType() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE, Value)
        End Set
    End Property

    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
    Public Property MakeId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_MAKE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_MAKE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_MAKE_ID, Value)
        End Set
    End Property
    Public Property Make() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_MAKE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_MAKE, Value)
        End Set
    End Property

    Public Property ServiceClassCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_CLASS_CODE, Value)
        End Set
    End Property

    Public Property ServiceTypeCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE, Value)
        End Set
    End Property

    Public Property ServiceLevelCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_CODE, Value)
        End Set
    End Property

    Public Property RiskTypeCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_RISK_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_RISK_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_RISK_TYPE_CODE, Value)
        End Set
    End Property

    Public Property EquipmentCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_EQUIPMENT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_EQUIPMENT_CODE, Value)
        End Set
    End Property

    Public Property ConditionTypeCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_CONDITION_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_CONDITION_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_CONDITION_TYPE_CODE, Value)
        End Set
    End Property

    Public Property VendorQuantity() As Integer
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_VENDOR_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_VENDOR_QUANTITY), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_VENDOR_QUANTITY, Value)
        End Set
    End Property
    Public ReadOnly Property PriceList As PriceList
        Get
            If (_priceList Is Nothing) Then
                _priceList = New PriceList(Me.PriceListId, Me.Dataset)
            End If
            Return _priceList
        End Get
    End Property

    <ValidatePartsServiceClassAndType("")>
    Public Property PartId() As Guid
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PART_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PART_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PART_ID, Value)
        End Set
    End Property

    Public Property PartCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PART_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PART_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PART_CODE, Value)
        End Set
    End Property

    Public Property PartDescription() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_PART_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PART_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_PART_DESC, Value)
        End Set
    End Property

    Public Property ManufacturerOriginCode() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN, Value)
        End Set
    End Property

    Public Property ManufacturerOriginDescription() As String
        Get
            CheckDeleted()
            If Row(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PriceListDetailDAL.COL_NAME_MANUFACTURER_ORIGIN_DESC, Value)
        End Set
    End Property

    'US 255424 - Adding properties for Parent object
    Public Property Parent_ConditionId() As Guid
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_ID, Value)
            End If
        End Set
    End Property


    Public Property Parent_ConditionTypeCode() As String
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)

            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_CONDITION_TYPE_CODE, Value)
            End If

        End Set
    End Property

    Public Property Parent_Model() As String
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MODEL) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PARENT_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MODEL) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_MODEL, Value)
            End If
        End Set
    End Property
    Public Property Parent_MakeId() As Guid
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_MAKE_ID, Value)
            End If
        End Set
    End Property


    Public Property Parent_Make() As String
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MAKE) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceListDetailDAL.COL_NAME_PARENT_MAKE), String)
            End If
        End Get
        Set(ByVal Value As String)
            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_MAKE) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_MAKE, Value)
            End If

        End Set
    End Property

    Public Property Parent_EquipmentId() As Guid
        Get
            CheckDeleted()
            If Not Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID) OrElse Row(PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)

            If Row.Table.Columns.Contains(PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID) Then
                CheckDeleted()
                Me.SetValue(PriceListDetailDAL.COL_NAME_PARENT_EQUIPMENT_ID, Value)
            End If

        End Set
    End Property
#End Region

#Region "Public Members"
    Public _priceList As PriceList
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PriceListDetailDAL
                dal.UpdateFamily(Me.Dataset)

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

    Public Sub viewhistory(ByVal pricelistdetailid As Guid)
        Dim dal As New PriceListDetailDAL
        dal.ViewPriceListDetailHistory(pricelistdetailid, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    End Sub

    Public Sub SaveRow()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PriceListDetailDAL
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty OrElse Me.AnyColumnHasChanged
        End Get
    End Property

    Public Sub Copy(ByVal original As PriceListDetail)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Price List Code.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    'US 224089 - Getting prices for parts
    Public Function GetPriceListForParts(ByVal claimId As Guid, ByVal InForceDate As Date, ByVal listOfParts As IEnumerable(Of PartsInfo)) As DataSet

        Try

            Dim dal As New PriceListDetailDAL
            Return dal.GetPriceListForParts(claimId, FormatPartsListTotXML(listOfParts))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function GetPriceListForParts(ByVal claimId As Guid, ByVal InForceDate As Date, ByVal partsInfoDV As PartsInfo.PartsInfoDV) As DataSet

        Try

            Dim dal As New PriceListDetailDAL
            Return dal.GetPriceListForParts(claimId, FormatPartsListTotXML(partsInfoDV))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Private Function FormatPartsListTotXML(parts As IEnumerable(Of PartsInfo)) As String
        'Return FormatPartsListTotXML(parts.Select(Of Guid)(Function(part As PartsInfo) part.Id))
        Dim sb = New StringBuilder()

        sb.Append("<Parts>")
        sb.Append(String.Join(String.Empty,
                              parts.Select(Of String)(
                                  Function(part As PartsInfo)
                                      Return ((String.Format(PART_INFO_XML_NODE,
                                                                        MiscUtil.GetDbStringFromGuid(part.PartsDescriptionId).Replace("'", String.Empty),
                                                                        part.Cost.Value.ToString()))).ToArray()
                                  End Function
                                )
                              )
                  )

        sb.Append("</Parts>")

        Return sb.ToString()
    End Function

    Private Function FormatPartsListTotXML(parts As PartsInfo.PartsInfoDV) As String
        'Return FormatPartsListTotXML(parts.Select(Of Guid)(Function(part As PartsInfo) part.Id))
        Dim sb = New StringBuilder()

        sb.Append("<Parts>")
        sb.Append(String.Join(String.Empty,
                              parts.Cast(Of DataRowView)().Select(Of String)(
                                  Function(part As DataRowView)
                                      Return ((String.Format(PART_INFO_XML_NODE,
                                                                    MiscUtil.GetDbStringFromGuid(New Guid(CType(part(PartsInfo.PartsInfoDV.COL_NAME_PARTS_DESCRIPTION_ID), Byte()))).Replace("'", String.Empty),
                                                                    part(PartsInfo.PartsInfoDV.COL_NAME_COST).ToString()))).ToArray()
                                  End Function
                                )
                              )
                  )

        sb.Append("</Parts>")

        Return sb.ToString()
    End Function

#End Region

#Region "Expiring Logic"

    Public Function CheckUniqueSKU(ByVal dv As PriceList.PriceListDetailSelectionView, Optional ByVal action As Integer = 0) As Boolean
        Try
            'Dim obj As PriceListDetail = (New PriceList).GetPriceListDetailChild(Me.Id)
            'Dim ds As DataSet = (New PriceListDetailDAL).LoadList(PriceListId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'Dim dsTemp As DataSet = (New PriceListDetailDAL).GetMaxExpirationMinEffectiveDateForPriceList(PriceListId, Id)
            Dim maxExpDate As DateTime
            Dim minEffDate As DateTime
            Dim result As Boolean = False
            '''''''''''''''''
            'If (dsTemp.Tables.Count > 0 And dsTemp.Tables(0).Rows.Count > 0) Then
            ''''to check if the new record is the very first record
            'If dsTemp.Tables(0).Rows(0)(1).ToString() <> "" Then
            '''''check if dv count is > 1 as the first record should be saved without check
            If Not dv Is Nothing And dv.Count > 1 Then

                maxExpDate = CType(dv.ToTable().Compute("Max(Expiration)", String.Empty), DateTime) 'dsTemp.Tables(0).Rows(0)(1)
                minEffDate = CType(dv.ToTable().Compute("Min(Effective)", String.Empty), DateTime) 'dsTemp.Tables(0).Rows(0)(0)

                'For new data
                If action = 5 Then
                    'Dim maxExpDate As DateTime = CType(ds.Tables(0).Compute("Max(Expiration)", String.Empty), DateTime)
                    'Dim minEffDate As DateTime = CType(ds.Tables(0).Compute("Min(Effective)", String.Empty), DateTime)

                    ''''Get the Max Expiration date and check whether the new effective date is greater.
                    'New Effective date should not be less than the Max(Expiration)
                    'New Expiration date should not be less than the Min(Effective)
                    'check the date overlap if the Vendor SKU is same.
                    For Each dr As DataRow In dv.Table.Rows
                        If dr("Vendor_sku").ToString() = Me.VendorSku.ToString() Then
                            If GuidControl.ByteArrayToGuid(dr("price_list_Detail_id")) <> Id Then
                                If ((DateTime.Compare(Me.Effective, DateTime.Parse(dr("Expiration").ToString())) <= 0) And (DateTime.Compare(DateTime.Parse(dr("Effective").ToString()), Me.Expiration) <= 0)) Then
                                    Return True
                                End If
                            End If
                            'If ((DateTime.Compare(Me.Effective, maxExpDate) <= 0) And (DateTime.Compare(Me.Expiration, minEffDate) <= 0)) Then
                            '    result = True
                            'End If
                        End If
                    Next
                    'If ((Me.Effective <> CType(ds.Tables(0).Compute("Max(Expiration)", String.Empty), DateTime).AddSeconds(1) _
                    '     Or (Me.Expiration <> CType(ds.Tables(0).Compute("Min(Expiration)", String.Empty), DateTime).AddSeconds(-1)))) Then
                    '    Return True
                    'End If
                Else
                    For Each dr As DataRow In dv.Table.Rows
                        If dr("vendor_sku").ToString().ToUpper() = Me.VendorSku.ToString().ToUpper() Then
                            If GuidControl.ByteArrayToGuid(dr("price_list_Detail_id")) <> Id Then
                                If ((DateTime.Compare(Me.Effective, DateTime.Parse(dr("Expiration").ToString())) <= 0) And (DateTime.Compare(DateTime.Parse(dr("Effective").ToString()), Me.Expiration) <= 0)) Then
                                    Return True

                                End If
                            End If
                        End If
                    Next
                    'If ((DateTime.Compare(Me.Effective, maxExpDate) < 0) And (DateTime.Compare(Me.Expiration, minEffDate) < 0)) Then
                    '    result = True
                    'End If
                End If
            End If
            'End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function OverlapExists(ByVal CheckSelf As Boolean) As Boolean
        Try
            Dim ds As New DataSet
            If CheckSelf Then
                ds = (New PriceListDetailDAL).GetOverlap(Me.EquipmentClassId, Me.EquipmentId, Me.ConditionId, Me.RiskTypeId, Me.ServiceClassId, Me.ServiceTypeId, Me.VendorSku,
                                                         Me.PriceListId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Id,
                                                         Me.PartId, Me.MakeId, Me.ManufacturerOriginCode,
                                                         Me.Expiration.Value.ToString("MM/dd/yyyy"), Me.Effective.Value.ToString("MM/dd/yyyy"))
            Else
                ds = (New PriceListDetailDAL).GetOverlap(Me.EquipmentClassId, Me.EquipmentId, Me.ConditionId, Me.RiskTypeId, Me.ServiceClassId, Me.ServiceTypeId, Me.VendorSku,
                                                         Me.PriceListId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Nothing,
                                                         Me.PartId, Me.MakeId, Me.ManufacturerOriginCode,
                                                         Me.Expiration.Value.ToString("MM/dd/yyyy"), Me.Effective.Value.ToString("MM/dd/yyyy"))
            End If

            If ds.Tables(0).Rows.Count > 0 Then Return True Else Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

    Public Function ExpireOverLappingList() As Boolean
        Try
            Dim overlap As New PriceListDetailDAL
            Dim ds As New DataSet
            ds = overlap.GetOverlap(Me.EquipmentClassId, Me.EquipmentId, Me.ConditionId, Me.RiskTypeId, Me.ServiceClassId, Me.ServiceTypeId, Me.VendorSku,
                                    Me.PriceListId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Id,
                                    Me.PartId, Me.MakeId, Me.ManufacturerOriginCode,
                                    Me.Expiration.Value.ToString("MM/dd/yyyy"), Me.Effective.Value.ToString("MM/dd/yyyy"))
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dtrow As DataRow In ds.Tables(0).Rows
                    Dim pId As Guid = New Guid(CType(dtrow(PriceListDetailDAL.COL_NAME_PRICE_LIST_DETAIL_ID), Byte()))
                    Dim ExpPlist As New PriceListDetail(pId, Me.Dataset)
                    ExpPlist.BeginEdit()

                    If ExpPlist.Effective > PriceListDetail.GetCurrentDateTime() Then
                        ExpPlist.Expiration = ExpPlist.Effective.Value.AddSeconds(1)
                    Else
                        ExpPlist.Expiration = PriceListDetail.GetCurrentDateTime().AddSeconds(-1)
                    End If
                    ExpPlist.EndEdit()
                    ExpPlist.Save()
                Next
                Return True         'expired successfully
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function CheckForRiskTypeEquipClassEquipment() As Boolean
        Try
            '''Either Risk Type, Equipment Class and Make or model should be selected
            If (RiskTypeId = Guid.Empty AndAlso EquipmentClassId = Guid.Empty AndAlso (MakeId = Guid.Empty Or EquipmentId = Guid.Empty)) Then
                Return False
            End If
            '''All the three: Risk Type, Equipment Class and Make or model should not be selected; Either one should be selected.
            If (RiskTypeId <> Guid.Empty AndAlso EquipmentClassId <> Guid.Empty AndAlso (MakeId <> Guid.Empty Or EquipmentId <> Guid.Empty)) Then
                Return False
            End If
            If (RiskTypeId <> Guid.Empty AndAlso EquipmentClassId <> Guid.Empty) Then
                Return False
            End If
            If (RiskTypeId <> Guid.Empty AndAlso (MakeId <> Guid.Empty Or EquipmentId <> Guid.Empty)) Then
                Return False
            End If
            If (EquipmentClassId <> Guid.Empty AndAlso (MakeId <> Guid.Empty Or EquipmentId <> Guid.Empty)) Then
                Return False
            End If
            '''''
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function CheckForConditionType() As Boolean
        Try
            If EquipmentClassId <> Guid.Empty Or RiskTypeId <> Guid.Empty Then
                If ConditionId <> Guid.Empty Then
                    Return False
                End If
                '''''if "Condition" is selected then Make and Model (Equipment) should not be empty 
            ElseIf (ConditionId <> Guid.Empty) Then
                If (MakeId = Guid.Empty Or EquipmentId = Guid.Empty) Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Visitor"
    ''' <summary>
    ''' Accept member of IElement interface
    ''' </summary>
    ''' <param name="Visitor"></param>
    ''' <returns>Returns True if Overlapping Records are found</returns>
    ''' <remarks></remarks>
    Public Function Accept(ByRef Visitor As IVisitor) As Boolean Implements IElement.Accept
        Try
            Return Visitor.Visit(Me)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Validations"
    Public NotInheritable Class ValidateConditionType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.GUI_INVALID_CONDITION_SELECT_WHEN_EQUIPMENT_SELECTED))
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)
            'US 255424 - Removing validation against RiskTypeId ... when submitting PatId and EquipmentID together then RiskTypeID <> Guid.Empty

            If obj.EquipmentClassId <> Guid.Empty AndAlso
                obj.ConditionId <> Guid.Empty Then
                Return False
            End If

            '''''if "Condition" is selected then Make and Model (Equipment) should be selected 
            If (obj.ConditionId <> Guid.Empty) AndAlso
               (obj.MakeId = Guid.Empty Or obj.EquipmentId = Guid.Empty) Then
                Return False
            End If

            '''''if "Service Class" is "Replacement" and "Service Level" is "Replacement Price" then "Condition" must be selected
            If obj.ServiceClassId <> Guid.Empty AndAlso
                obj.ServiceTypeId <> Guid.Empty AndAlso
                LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS, obj.ServiceClassId) = Codes.SERVICE_CLASS__REPLACEMENT AndAlso
                LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS_TYPE, obj.ServiceTypeId) = Codes.SERVICE_TYPE__REPLACEMENT_PRICE AndAlso
                obj.EquipmentId <> Guid.Empty AndAlso
                obj.ConditionId = Guid.Empty Then
                Return False
            End If

            'If obj.EquipmentClassId <> Guid.Empty Or obj.RiskTypeId <> Guid.Empty Then
            'If obj.EquipmentClassId <> Guid.Empty Then
            '    If obj.ConditionId <> Guid.Empty Then
            '        Return False
            '    End If
            '    '''''if "Condition" is selected then Make and Model (Equipment) should be selected 
            'ElseIf (obj.ConditionId <> Guid.Empty) Then
            '    If (obj.MakeId = Guid.Empty Or obj.EquipmentId = Guid.Empty) Then
            '        Return False
            '    End If
            'End If
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidateRiskTypeEquipClassEquipment
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            'MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.GUI_INVALID_ONLY_RISKTYPE_OR_EQUIPMENTCLASS_OR_EQUIPMENT_SELECTION))
            'US 255424 - Updating Error message

            MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.GUI_ERROR_INVALID_RISKTYPE_EQUIPMENTCLASS_EQUIPMENT_SELECTION))

        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)
            '''All values are empty general service class/type entry
            If (obj.RiskTypeId = Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty AndAlso
                obj.MakeId = Guid.Empty AndAlso
                obj.EquipmentId = Guid.Empty AndAlso
                obj.PartId = Guid.Empty AndAlso
                obj.ManufacturerOriginCode Is Nothing) Then
                Return True
            End If
            '''Risk Type stock item entry
            If (obj.RiskTypeId <> Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty AndAlso
                obj.MakeId = Guid.Empty AndAlso
                obj.EquipmentId = Guid.Empty AndAlso
                obj.PartId = Guid.Empty AndAlso
                obj.ManufacturerOriginCode Is Nothing) Then
                Return True
            End If
            '''Equipment Class stock item entry
            If (obj.RiskTypeId = Guid.Empty AndAlso
                obj.EquipmentClassId <> Guid.Empty AndAlso
                obj.MakeId = Guid.Empty AndAlso
                obj.EquipmentId = Guid.Empty AndAlso
                obj.PartId = Guid.Empty AndAlso
                obj.ManufacturerOriginCode Is Nothing) Then
                Return True
            End If
            '''Equipment stock item entry
            If (obj.RiskTypeId = Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty AndAlso
                obj.MakeId <> Guid.Empty AndAlso
                obj.EquipmentId <> Guid.Empty AndAlso
                obj.PartId = Guid.Empty AndAlso
                obj.ManufacturerOriginCode Is Nothing) Then
                Return True
            End If
            '''Part stock item entry
            If (obj.RiskTypeId <> Guid.Empty AndAlso
                obj.PartId <> Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty AndAlso
                obj.EquipmentId = Guid.Empty AndAlso
                obj.ConditionId = Guid.Empty) Then

                Return True
            End If

            '''US 255424 - PartId and Equipment stock item entry
            If (obj.RiskTypeId <> Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty AndAlso
                obj.MakeId <> Guid.Empty AndAlso
                obj.EquipmentId <> Guid.Empty AndAlso
                obj.PartId <> Guid.Empty AndAlso
                obj.EquipmentClassId = Guid.Empty) Then
                Return True
            End If


            ''''Either Risk Type, Equipment Class and Make or model should be selected
            'If (obj.RiskTypeId = Guid.Empty AndAlso obj.EquipmentClassId = Guid.Empty AndAlso (obj.MakeId = Guid.Empty Or obj.EquipmentId = Guid.Empty)) Then
            '    Return False
            'End If
            ''''All the three: Risk Type, Equipment Class and Make or model should not be selected; Either one should be selected.
            'If (obj.RiskTypeId <> Guid.Empty AndAlso obj.EquipmentClassId <> Guid.Empty AndAlso (obj.MakeId <> Guid.Empty Or obj.EquipmentId <> Guid.Empty)) Then
            '    Return False
            'End If
            'If (obj.RiskTypeId <> Guid.Empty AndAlso obj.EquipmentClassId <> Guid.Empty) Then
            '    Return False
            'End If
            'If (obj.RiskTypeId <> Guid.Empty AndAlso obj.MakeId <> Guid.Empty AndAlso obj.EquipmentId <> Guid.Empty) Then
            '    Return False
            'End If
            'If (obj.EquipmentClassId <> Guid.Empty AndAlso (obj.MakeId <> Guid.Empty Or obj.EquipmentId <> Guid.Empty)) Then
            '    Return False
            'End If
            ''''''

            Return False
        End Function
    End Class

    Public NotInheritable Class ValidatePriceBandRangeTo
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.MIN_VALUE_MUST_BE_LESS_THAN_MAX_VALUE))
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)
            Dim rangeTo As Decimal
            Dim rangeFrom As Decimal

            If obj.PriceBandRangeTo = Nothing Then
                rangeTo = 0
            End If
            If obj.PriceBandRangeFrom = Nothing Then
                rangeFrom = 0
            End If

            If CDec(rangeTo) < CDec(rangeFrom) Then
                Return False
            End If

            Return True
        End Function
    End Class

    Public NotInheritable Class ValidatePriceListSKU
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.UNIQUE_SKU_PER_PRICE_LIST))
        End Sub
        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)
            Dim retVal As Boolean = obj.PriceList.PriceListDetailChildren.Where(Function(i) (i.VendorSku.ToUpper() = obj.VendorSku.ToUpper() And ((i.Effective.Value >= obj.Effective.Value And i.Effective.Value <= obj.Expiration.Value) Or (i.Expiration.Value >= obj.Effective.Value And i.Expiration.Value <= obj.Expiration.Value)))).Count > 1
            Return Not retVal
        End Function
    End Class

    Public NotInheritable Class ValidatePartsServiceClassAndType
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.INVALID_PARTS_SERVICE_CLASS_AND_TYPE))
        End Sub
        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)
            If Not obj.PartId.Equals(Guid.Empty) Then
                Dim retVal As Boolean = LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS, obj.ServiceClassId) <> Codes.SERVICE_CLASS__REPAIR Or
                    LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS_TYPE, obj.ServiceTypeId) <> Codes.SERVICE_TYPE__PARTS_AMOUNT
                Return Not retVal
            End If
            Return True
        End Function
    End Class

#End Region

#Region "Price List Detail View"
    Public Class PriceListDetailChildern
        Inherits BusinessObjectListEnumerableBase(Of PriceList, PriceListDetail)

        Public Sub New(ByVal parent As PriceList)
            MyBase.New(LoadTable(parent), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, PriceListDetail).PriceListId.Equals(CType(Parent, PriceList).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As PriceList) As DataTable
            Try

                If Not parent.IsChildrenCollectionLoaded(GetType(PriceListDetailChildern)) Then
                    Dim dal As New PriceListDetailDAL

                    dal.LoadPriceListDetailsForPriceList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.Id)
                    parent.AddChildrenCollection(GetType(PriceListDetailChildern))


                End If
                Return parent.Dataset.Tables(PriceListDetailDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class



#End Region

#Region "DataView Retrieveing Methods"

    Public Function GetMakeByModel(ByVal ModelId As Guid) As Guid
        Try
            Return New Guid(CType((New PriceListDetailDAL).GetMakeByModel(ModelId).Tables(0).Rows(0).Item(0), Byte()))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

    Public Function GetManufactByEquipmentId(ByVal ModelId As Guid) As Guid
        Try
            Return New Guid(CType((New PriceListDetailDAL).GetMakeByModel(ModelId).Tables(0).Rows(0).Item(0), Byte()))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

    Public Function GetVendorQuantiy() As Guid
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As DataSet
            ds = dal.GetVendorQuantiy(Me.Id)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(0).Rows(0).Item(0), Byte()))
            Else
                Return Guid.Empty
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

    Public Shared Function GetCurrentDateTime() As DateTime
        Try
            Dim dal As New PriceListDetailDAL
            Return dal.GetCurrentDateTime()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetDetailRow(ByVal DetailId As Guid) As PriceListDetail.PriceListDetailSearchDV
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet, dv As DataView
            ds = dal.Load(DetailId)
            Return New PriceListDetail.PriceListDetailSearchDV(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal PriceListId As Guid, ByVal RiskTypeId As Guid, ByVal equipmentId As Guid, ByVal conditionId As Guid, ByVal equipmentclassId As Guid, ByVal EffectiveDate As DateTime, ByVal ServiceClassId As Guid, ByVal ServiceTypeId As Guid) As DataView
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet, dv As DataView
            ds = dal.GetPriceBandList(PriceListId, RiskTypeId, equipmentclassId, equipmentId, conditionId, EffectiveDate.ToString("MM/dd/yyyy"), ServiceClassId, ServiceTypeId)
            dv = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal PriceListId As Guid) As PriceListDetail.PriceListDetailSearchDV
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet, dv As DataView
            ds = dal.LoadList(PriceListId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return New PriceListDetail.PriceListDetailSearchDV(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRepairPrices(ByVal companyId As Guid, ByVal ServiceCenterCode As String, ByVal EffectiveDate As Date,
                                           ByVal RiskTypeId As Guid, ByVal SalesPrice As Double, ByVal equipClassId As Guid,
                                           ByVal equipmentId As Guid, ByVal conditionId As Guid, ByVal dealerId As Guid,
                                           ByVal serviceLevelCode As String) As PriceListDetail.PriceListResultsDV
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet

            ds = dal.GetRepairPrices(companyId, ServiceCenterCode, EffectiveDate, SalesPrice, RiskTypeId, equipClassId, equipmentId, conditionId, dealerId, serviceLevelCode)
            Return New PriceListDetail.PriceListResultsDV(ds.Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRepairPricesforMethodofRepair(ByVal MethodofRepairId As Guid, ByVal companyId As Guid, ByVal ServiceCenterCode As String,
                                                            ByVal RiskTypeId As Guid, ByVal EffectiveDate As Date, ByVal SalesPrice As Double,
                                                            ByVal equipClassId As Guid, ByVal equipmentId As Guid, ByVal conditionId As Guid,
                                                            ByVal dealerId As Guid, ByVal serviceLevelCode As String) As PriceListDetail.PriceListResultsDV
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet
            Dim equipConditionId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            ds = dal.GetPriceForMethodofRepair(MethodofRepairId, companyId, ServiceCenterCode, EffectiveDate, SalesPrice, RiskTypeId,
                                               equipClassId, equipmentId, conditionId, dealerId, serviceLevelCode)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return New PriceListDetail.PriceListResultsDV(ds.Tables(0))
                'Else
                '   Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
            End If
            Return Nothing

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        End Try
    End Function
    Public Shared Function GetMakeModelByEquipmentId(ByVal EquipmentId As Guid, ByVal CompanyGroupId As Guid) As DataSet
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet

            ds = dal.GetMakeModelByEquipmentId(EquipmentId, CompanyGroupId)

            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetModelsByMake(ByVal ManufacturerId As Guid) As DataSet
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet

            ds = dal.GetModelsByMake(ManufacturerId)

            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetPricesForServiceType(ByVal companyId As Guid, ByVal ServiceCenterCode As String, ByVal RiskTypeId As Guid,
                                                   ByVal EffectiveDate As Date, ByVal SalesPrice As Decimal, ByVal serviceClassId As Guid,
                                                   ByVal serviceTypeId As Guid, ByVal equipClassId As Guid, ByVal equipmentId As Guid,
                                                   ByVal conditionId As Guid, ByVal dealerId As Guid, ByVal serviceLevelCode As String) As PriceListDetail.PriceListResultsDV
        Try
            Dim dal As New PriceListDetailDAL
            Dim ds As New DataSet
            Dim dv As New PriceListDetail.PriceListResultsDV
            ds = dal.GetRepairPrices(companyId, ServiceCenterCode, EffectiveDate, SalesPrice, RiskTypeId, equipClassId, equipmentId, conditionId, dealerId, serviceLevelCode)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                dv = New PriceListDetail.PriceListResultsDV(ds.Tables(0))
                Dim condition As String = PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_CLASS_CODE & "='" _
                                          & LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS, serviceClassId) & "' AND " &
                                          PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_TYPE_CODE & "='" _
                                          & LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS_TYPE, serviceTypeId) & "'"
                dv.RowFilter = condition
                Return dv
            Else
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            If ex.ErrorType = DataBaseAccessException.DatabaseAccessErrorType.BusinessErr Then Throw

            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidPriceBandRange
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM009)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PriceListDetail = CType(objectToValidate, PriceListDetail)

            Dim bValid As Boolean = True

            'If Not obj.PriceBandRangeFrom Is Nothing And Not obj.PriceBandRangeTo Is Nothing Then
            '    If Convert.ToSingle(obj.PriceBandRangeFrom.Value) > Convert.ToSingle(obj.PriceBandRangeTo.Value) Then
            '        Me.Message = COVERAGE_RATE_FORM009
            '        bValid = False
            '    ElseIf ValidateRange(obj.PriceBandRangeFrom, obj.PriceBandRangeTo, obj) = False Then
            '        Me.Message = COVERAGE_RATE_FORM011
            '        bValid = False
            '    End If
            'End If

            Return bValid

        End Function



        ' It validates that the price range falls within the previous and next range +- THRESHOLD
        Private Function ValidateRange(ByVal sNewLow As Assurant.Common.Types.DecimalType, ByVal sNewHigh As Assurant.Common.Types.DecimalType, ByVal oPriceListDetail As PriceListDetail) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oPriceListDetailId As Guid = oPriceListDetail.Id
            Dim oLow, oHigh As Double
            Dim prevLow As Double = MIN_DOUBLE - THRESHOLD
            Dim prevHigh As Double = MIN_DOUBLE - THRESHOLD
            Dim oPriceBands As DataView = (New PriceListDetailDAL).FormPriceRangeQuery(oPriceListDetail.EquipmentClassId, oPriceListDetail.EquipmentId,
                                                                                            oPriceListDetail.ConditionId, oPriceListDetail.RiskTypeId, oPriceListDetail.ServiceClassId,
                                                                                            oPriceListDetail.ServiceTypeId, oPriceListDetail.PriceListId,
                                                                                            ElitaPlusIdentity.Current.ActiveUser.LanguageId, oPriceListDetail.Effective.Value.ToShortDateString())
            'oPriceListDetail.GetList(oPriceListDetail.PriceListId, oPriceListDetail.RiskTypeId, oPriceListDetail.EquipmentId, 
            '                                                  oPriceListDetail.ConditionId, oPriceListDetail.EquipmentClassId, oPriceListDetail.Effective, oPriceListDetail.ServiceClassId, oPriceListDetail.ServiceTypeId)
            Dim oRows As DataRowCollection = oPriceBands.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oPriceListDetailId = New Guid(CType(oRow(PRICE_LIST_DETAIL_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oCount = oCount + 1
                    If oPriceListDetail.Id.Equals(oPriceListDetailId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + THRESHOLD = oNewLow Then
                            ' Updating the last record
                            bValid = True
                            Exit For
                        ElseIf prevHigh < MIN_DOUBLE And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + THRESHOLD = oNewLow Then
                            bValid = True
                            Exit For
                        ElseIf prevHigh + THRESHOLD = oNewLow And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                            'ElseIf oNewLow = oLow And oNewHigh = oHigh Then
                            '    'Updating information other than the Low/High Prices
                            '    bValid = True
                            '    Exit For
                        End If

                        prevLow = oLow
                        prevHigh = oHigh

                    Else
                        If prevHigh < MIN_DOUBLE And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + THRESHOLD = oNewLow Then
                            bValid = True
                            Exit For
                        ElseIf prevHigh + THRESHOLD = oNewLow And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                        End If
                        prevLow = oLow
                        prevHigh = oHigh
                    End If
                Next
            End If

            Return bValid
        End Function

    End Class


#End Region



    Public Class PriceListDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRICE_LIST_ID As String = "price_list_id"
        Public Const COL_SERVICE_CLASS_ID As String = "service_class_id"
        Public Const COL_SERVICE_TYPE_ID As String = "service_type_id"
        Public Const COL_RISK_TYPE_ID As String = "risk_type_id"
        Public Const COL_EQUIPMENT_CLASS_ID As String = "equipment_class_id"
        Public Const COL_CONDITION_ID As String = "condition_id"
        Public Const COL_SKU As String = "vendor_sku"
        Public Const COL_SKU_DESCRIPTION As String = "vendor_sku_description"
        Public Const COL_PRICE As String = "price"
        Public Const COL_EFFECTIVE As String = "effective"
        Public Const COL_EXPIRATION As String = "expiration"
        Public Const COL_PRICE_LIST_DETAIL_ID As String = "price_list_detail_id"
        Public Const COL_MODEL As String = "equipment_id"
        Public Const COL_MAKE As String = "make_id"
        Public Const COL_VENDOR_QUANTITY As String = "vendor_quantity"
        Public Const COL_VENDOR_QUANTITY_ID As String = "vendor_quantity_id"

        'US 255424
        Public Const COL_PARENT_MODEL As String = "parent_equipment_id"
        Public Const COL_PARENT_CONDITION_ID As String = "parent_condition_id"
        Public Const COL_PARENT_MAKE As String = "parent_make_id"
        Public Const COL_PARENT_MODEL_DESCRIPTION As String = "parent_model"
        Public Const COL_PARENT_CONDITION_DESCRIPTION As String = "parent_condition_type_code"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class PriceListResultsDV
        Inherits DataView
#Region "Constants"
        Public Const COL_NAME_SERVICE_CLASS_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_CLASS_ID
        Public Const COL_NAME_SERVICE_TYPE_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_TYPE_ID
        Public Const COL_NAME_SERVICE_LEVEL_ID As String = PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_ID
        Public Const COL_NAME_SERVICE_CLASS_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_CLASS_CODE
        Public Const COL_NAME_SERVICE_TYPE_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE
        Public Const COL_NAME_SERVICE_LEVEL_CODE As String = PriceListDetailDAL.COL_NAME_SERVICE_LEVEL_CODE
        Public Const COL_NAME_VENDOR_SKU As String = PriceListDetailDAL.COL_NAME_VENDOR_SKU
        Public Const COL_NAME_VENDOR_SKU_DESC As String = PriceListDetailDAL.COL_NAME_VENDOR_SKU_DESCRIPTION
        Public Const COL_NAME_PRICE As String = PriceListDetailDAL.COL_NAME_PRICE
        Public Const COL_NAME_IS_DEDUCTIBLE_ID As String = PriceListDetailDAL.COL_NAME_IS_DEDUCTIBLE_ID
        Public Const COL_NAME_IS_DEDUCTIBLE_CODE As String = PriceListDetailDAL.COL_NAME_IS_DEDUCTIBLE_CODE
        Public Const COL_NAME_IS_STANDARD_ID As String = PriceListDetailDAL.COL_NAME_IS_STANDARD_ID
        Public Const COL_NAME_IS_STANDARD_CODE As String = PriceListDetailDAL.COL_NAME_IS_STANDARD_CODE
        Public Const COL_NAME_CONTAINS_DEDUCTIBLE_ID As String = PriceListDetailDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID
        Public Const COL_NAME_CONTAINS_DEDUCTIBLE_CODE As String = PriceListDetailDAL.COL_NAME_CONTAINS_DEDUCTIBLE_CODE
        Public Const COL_NAME_RETURN_CODE As String = PriceListDetailDAL.PAR_NAME_RETURN_CODE

#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class


End Class


