'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/12/2006)  ********************

Public Class CoverageRate
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
            Dim dal As New CoverageRateDAL
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
            Dim dal As New CoverageRateDAL
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
    Private Const COVERAGE_RATE As String = "COVERAGE RATE"
    Private Const COVERAGE_RATE_FORM001 As String = "COVERAGE_RATE_FORM001" ' 0<= LowPrice <1*10^6
    Private Const COVERAGE_RATE_FORM002 As String = "COVERAGE_RATE_FORM002" ' 0<= HighPrice <1*10^6
    Private Const COVERAGE_RATE_FORM003 As String = "COVERAGE_RATE_FORM003" ' 0<= GrossAmt <1*10^6
    Private Const COVERAGE_RATE_FORM004 As String = "COVERAGE_RATE_FORM004" ' 0<= CommisionPercent <1*10^6
    Private Const COVERAGE_RATE_FORM005 As String = "COVERAGE_RATE_FORM005" ' 0<= MarketingPercent <1*10^6
    Private Const COVERAGE_RATE_FORM006 As String = "COVERAGE_RATE_FORM006" ' 0<= AdminExpense <1*10^6
    Private Const COVERAGE_RATE_FORM007 As String = "COVERAGE_RATE_FORM007" ' 0<= ProfitExpense <1*10^6
    Private Const COVERAGE_RATE_FORM008 As String = "COVERAGE_RATE_FORM008" ' 0<= LossCostPercent <1*10^6
    Private Const COVERAGE_RATE_FORM009 As String = "COVERAGE_RATE_FORM009" ' LowPrice Must be less or equal than HighPrice
    Private Const COVERAGE_RATE_FORM010 As String = "COVERAGE_RATE_FORM010" ' CommissionPercent + MarketingPercent + AdminExpense + ProfitExpense + LossCostPercent Must be less than 100
    Private Const COVERAGE_RATE_FORM011 As String = "COVERAGE_RATE_FORM011" ' There should be no overlaps (low/high)
    Private Const COVERAGE_RATE_FORM012 As String = "COVERAGE_RATE_FORM012" ' Only 4 Digits Allowed After Decimal Point
    Private Const COVERAGE_RATE_FORM013 As String = "COVERAGE_RATE_FORM013" ' 0<= GrossAmountPercent <1*10^6
    Private Const COVERAGE_RATE_FORM014 As String = "COVERAGE_RATE_FORM014" ' 0<= Renewal Number <= 999
    Private Const COVERAGE_RATE_FORM015 As String = "COVERAGE_RATE_FORM015" ' Invalid Renewal Number
    Private Const COVERAGE_RATE_FORM016 As String = "COVERAGE_RATE_FORM016" ' There should be no overlaps or gaps (low/high) and row with 0 renewal number cannot be deleted if there are other row for same low/high price combination
    Private Const COVERAGE_RATE_FORM017 As String = "COVERAGE_RATE_FORM017" ' 0<= LiabilityLimitAmount <1*10^6
    Private Const COVERAGE_RATE_FORM018 As String = "COVERAGE_RATE_FORM018" ' 0<= LiabilityLimitPercent <1*10^6

    Private Const MIN_DOUBLE As Double = 0.0
    Private Const MAX_DOUBLE As Double = 999999.99
    Private Const MIN_PERCENT As Double = 0.0
    Private Const MAX_PERCENT As Double = 99.9999
    Private Const MIM_DECIMAL_NUMBERS As Integer = 4
    Private Const THRESHOLD As Double = 0.01
    Public Const MIN_OFFSET As Integer = 0
    Public Const MAX_LIABILITY As Integer = 99999    
    Public Const MIN_OFFSET_LIABLIMIT_PERCENT As Integer = 50
    Private Const NEW_COVERAGE_LIABILITY_MAX_DOUBLE As Double = 999999999.99
    Private Const NEW_COVERAGE_LIABILITY_PERCENT_MAX_DOUBLE As Double = 99.9999
    

    Private Const COVERAGE_RATE_ID As Integer = 0
    Private Const LOW_PRICE As Integer = 1
    Private Const HIGH_PRICE As Integer = 2
    Private Const GROSS_AMT As Integer = 3
    Private Const COMMISSION_PERCENT As Integer = 4
    Private Const MARKETING_PERCENT As Integer = 5
    Private Const ADMIN_EXPENSE As Integer = 6
    Private Const PROFIT_EXPENSE As Integer = 7
    Private Const LOSS_COST_PERCENT As Integer = 8
    Private Const GROSS_AMOUNT_PERCENT As Integer = 13
    Private Const RENEWAL_NUMBER As Integer = 10
    Private Const REGION_ID As Integer = 11

    Private IsProductCodeSetForSequenceRenewalNo As Boolean
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CoverageRateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageRateDAL.COL_NAME_COVERAGE_RATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CoverageId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageRateDAL.COL_NAME_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_COVERAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("LowPrice", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM001), ValidCoverageRates("")>
    Public Property LowPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_LOW_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_LOW_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_LOW_PRICE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM002)>
    Public Property HighPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_HIGH_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_HIGH_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_HIGH_PRICE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM003)>
    Public Property GrossAmt() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_GROSS_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_GROSS_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_GROSS_AMT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=COVERAGE_RATE_FORM004), ValidCoverageSum(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property CommissionsPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=COVERAGE_RATE_FORM005), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property MarketingPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_MARKETING_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_MARKETING_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_MARKETING_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM006), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property AdminExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM007), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property ProfitExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_PROFIT_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_PROFIT_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_PROFIT_EXPENSE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=COVERAGE_RATE_FORM008), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property LossCostPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=COVERAGE_RATE_FORM013), ValidGrossAmountPercent(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property GrossAmountPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_GROSS_AMOUNT_PERCENT) Is DBNull.Value Then
                Return MIN_DOUBLE
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_GROSS_AMOUNT_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_GROSS_AMOUNT_PERCENT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999, Message:=COVERAGE_RATE_FORM014), ValidateRenewalNumber("")>
    Public Property RenewalNumber() As LongType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_RENEWAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CoverageRateDAL.COL_NAME_RENEWAL_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_RENEWAL_NUMBER, Value)
        End Set
    End Property
    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageRateDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property
    Public Property TaxRegion() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_TAX_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_TAX_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_TAX_REGION, Value)
        End Set
    End Property

    'US 521697
    <ValidStringLength("", Max:=50, Message:="CommissionsPercentSourceXcd should be between 1 to 30 chars.")>
    Public Property CommissionsPercentSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT_SOURCE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50, Message:="MarketingPercentSourceXcd should be between 1 to 30 chars.")>
    Public Property MarketingPercentSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_MARKETING_PERCENT_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_MARKETING_PERCENT_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_MARKETING_PERCENT_SOURCE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50, Message:="AdminExpenseSourceXcd should be between 1 to 30 chars.")>
    Public Property AdminExpenseSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_ADMIN_EXPENSE_SOURCE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50, Message:="ProfitPercentSourceXcd should be between 1 to 30 chars.")>
    Public Property ProfitPercentSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_PROFIT_PERCENT_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_PROFIT_PERCENT_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_PROFIT_PERCENT_SOURCE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50, Message:="LossCostPercentSourceXcd should be between 1 to 30 chars.")>
    Public Property LossCostPercentSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_LOSS_COST_PERCENT_SOURCE_XCD, Value)
        End Set
    End Property
    
    'US-489838    
    <ValidNumericRange("", Min:=MIN_DOUBLE, Max:=NEW_COVERAGE_LIABILITY_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM017)>
    Public Property CovLiabilityLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT, Value)
        End Set
    End Property
    
    'US-489838        
    <ValidNumericRange("", Min:=MIN_DOUBLE, Max:=MAX_DOUBLE, Message:=COVERAGE_RATE_FORM018), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=COVERAGE_RATE_FORM012)>
    Public Property CovLiabilityLimitPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT_PERCENT) Is DBNull.Value Then
                Return MIN_DOUBLE
            Else
                Return New DecimalType(CType(Row(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageRateDAL.COL_NAME_COV_LIABILITY_LIMIT_PERCENT, Value)
        End Set
    End Property
#End Region
    Public Property IsProductSetForSequenceRenewalNo() As Boolean
        Get
            Return IsProductCodeSetForSequenceRenewalNo
        End Get
        Set(ByVal value As Boolean)
            IsProductCodeSetForSequenceRenewalNo = value
        End Set
    End Property
#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageRateDAL
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

    Public Overrides Sub delete()
        Dim deleteOk As Boolean = False
        Dim oCoverageRateId As Guid = Id
        Dim oCoverageRates As DataView = GetList(CoverageId)
        Dim oRows As DataRowCollection = oCoverageRates.Table.Rows
        Dim oRow As DataRow
        Dim oCount As Integer = 0


        Dim oLow, oHigh As Double


        Dim covRateLow As Double = Math.Round(Convert.ToDouble(LowPrice.Value), 2)
        Dim covRateHigh As Double = Math.Round(Convert.ToDouble(HighPrice.Value), 2)
        Dim oLowestVal As Double = covRateLow
        Dim oHighestVal As Double = covRateHigh


        If oRows.Count = 0 Then
            'only one record Exists
            deleteOk = True
        Else
            For Each oRow In oRows
                oCoverageRateId = New Guid(CType(oRow(COVERAGE_RATE_ID), Byte()))
                oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)

                If (oLow = covRateLow And oHigh = covRateHigh) And Not (Id.Equals(oCoverageRateId)) Then
                    oCount = oCount + 1
                End If
                If (oLow < oLowestVal) Then
                    oLowestVal = oLow
                End If
                If (oHigh > oHighestVal) Then
                    oHighestVal = oHigh
                End If
            Next

            If oCount > 0 And RenewalNumber.Value > 0 Then
                ' You can delete if more than one records exists for the target combination
                deleteOk = True
            ElseIf (oCount = 0 And RenewalNumber.Value = 0 And (covRateLow = oLowestVal Or covRateHigh = oHighestVal)) Then
                ' You can delete only the first or last coverage rate to avoid gaps in case only one record exists for the target combination
                deleteOk = True
            End If
        End If

        If deleteOk Then
            MyBase.Delete()
        Else
            Throw New ElitaPlusException("There Should Be No Overlaps Or Gaps (low/high) and row with 0 renewal number cannot be deleted if there are other entry for same low/high price combination", COVERAGE_RATE_FORM016)
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal CoverageId As Guid) As DataView
        Try
            Dim dal As New CoverageRateDAL
            Return New DataView(dal.LoadList(CoverageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetCovRateListForDelete(ByVal CoverageId As Guid) As DataView
        Try
            Dim dal As New CoverageRateDAL
            Return New DataView(dal.LoadCovRateListForDelete(CoverageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetExpectedGWP(ByVal DealerId As Guid, ByVal ProductCode As String, ByVal certificate_duration As Integer,
                                          ByVal WarrSalesDate As Date, ByVal PurchasePrice As Double, ByVal CoverageDuration As Integer,
                                           ByVal ProductPurchaseDate As Date) As Object
        Try
            Dim dal As New CoverageRateDAL
            Return dal.GetExpectedGWP(DealerId, ProductCode, certificate_duration, WarrSalesDate, PurchasePrice, CoverageDuration, ProductPurchaseDate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Used by Olita Web Service 
    Public Shared Function getDealerCoverageRatesInfo(ByRef ds As DataSet, ByVal dealerId As Guid, ByVal WarrSalesDate As Date) As DataSet
        Try
            Dim dal As New CoverageRateDAL
            Return dal.LoadDealerCoverageRatesInfo(ds, dealerId, WarrSalesDate)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

    Public Function HasIgnorePremiumConfiguredForContract(ByVal coverageId As Guid) As Boolean

        Dim pCoverage As New Coverage(coverageId)
        Dim oDealer As New Dealer(pCoverage.DealerId)
        Dim oContract As Contract = New Contract
        Dim isIgnorePremiumYesForContract As Boolean = False

        If HasDealerConfiguredForAcctBucket(coverageId) Then
            With pCoverage
                If Not (.Effective Is Nothing And .Expiration Is Nothing) Then
                    oContract = oContract.GetContract(.DealerId, .Effective.Value, .Expiration.Value)
                End If

                If Not oContract Is Nothing Then
                    If oContract.IgnoreIncomingPremiumID.ToString() <> "00000000-0000-0000-0000-000000000000" Then
                        Dim str As String
                        str = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IgnoreIncomingPremiumID)

                        If str.Equals(Codes.YESNO_Y) Then
                            isIgnorePremiumYesForContract = True
                        Else
                            isIgnorePremiumYesForContract = False
                        End If

                    Else
                        isIgnorePremiumYesForContract = False
                    End If
                Else
                    isIgnorePremiumYesForContract = False
                End If

            End With
        Else
            isIgnorePremiumYesForContract = False
        End If

        Return isIgnorePremiumYesForContract
    End Function

    Public Function HasDealerConfiguredForAcctBucket(ByVal coverageId As Guid) As Boolean

        Dim pCoverage As New Coverage(coverageId)
        Dim oDealer As New Dealer(pCoverage.DealerId)
        Dim isDealerConfiguredForAcctBucket As Boolean = False

        If (pCoverage.DealerId <> Guid.Empty) Then
            If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                If Not String.IsNullOrWhiteSpace(oDealer.AcctBucketsWithSourceXcd) Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        isDealerConfiguredForAcctBucket = True
                    Else
                        isDealerConfiguredForAcctBucket = False
                    End If
                Else
                    isDealerConfiguredForAcctBucket = False
                End If
            Else
                isDealerConfiguredForAcctBucket = False
            End If
        Else
            isDealerConfiguredForAcctBucket = False
        End If

        Return isDealerConfiguredForAcctBucket
    End Function

    Public Function HasProductConfiguredForSequentialRenewalNo(ByVal coverageId As Guid) As Boolean

        Dim pCoverage As New Coverage(coverageId)
        Dim oDealer As New Dealer(pCoverage.DealerId)
        Dim isDealerConfiguredForAcctBucket As Boolean = False

        If (pCoverage.DealerId <> Guid.Empty) Then
            If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                If Not String.IsNullOrWhiteSpace(oDealer.AcctBucketsWithSourceXcd) Then
                    If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                        isDealerConfiguredForAcctBucket = True
                    Else
                        isDealerConfiguredForAcctBucket = False
                    End If
                Else
                    isDealerConfiguredForAcctBucket = False
                End If
            Else
                isDealerConfiguredForAcctBucket = False
            End If
        Else
            isDealerConfiguredForAcctBucket = False
        End If

        Return isDealerConfiguredForAcctBucket
    End Function

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoverageRates
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM009)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageRate = CType(objectToValidate, CoverageRate)

            Dim bValid As Boolean = True

            If Not obj.LowPrice Is Nothing And Not obj.HighPrice Is Nothing Then
                If Convert.ToSingle(obj.LowPrice.Value) > Convert.ToSingle(obj.HighPrice.Value) Then
                    Me.Message = COVERAGE_RATE_FORM009
                    bValid = False
                ElseIf ValidateRange(obj.LowPrice, obj.HighPrice, obj) = False Then
                    Me.Message = COVERAGE_RATE_FORM011
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        ' It validates that the price range falls within the previous and next range +- THRESHOLD
        Private Function ValidateRange(ByVal sNewLow As Assurant.Common.Types.DecimalType, ByVal sNewHigh As Assurant.Common.Types.DecimalType, ByVal oCoverageRate As CoverageRate) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oCoverageRateId As Guid = oCoverageRate.Id
            Dim oLow, oHigh As Double
            Dim prevLow As Double = MIN_DOUBLE - THRESHOLD
            Dim prevHigh As Double = MIN_DOUBLE - THRESHOLD
            Dim oCoverageRates As DataView = oCoverageRate.GetList(oCoverageRate.CoverageId)
            Dim oRows As DataRowCollection = oCoverageRates.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0

            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oCoverageRateId = New Guid(CType(oRow(COVERAGE_RATE_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oCount = oCount + 1
                    If oCoverageRate.Id.Equals(oCoverageRateId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + THRESHOLD = oNewLow Then
                            ' Updating the last record
                            bValid = True
                            Exit For
                        End If
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
                        ElseIf (oLow = oNewLow And oHigh = oNewHigh) Then
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

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidCoverageSum
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM010)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageRate = CType(objectToValidate, CoverageRate)

            'US 521697 check if dealer is configured for Account Bucket, if not then only validate for > 100% calculation
            If obj.HasDealerConfiguredForAcctBucket(obj.CoverageId) = False Then
                If Not obj.CommissionsPercent Is Nothing And Not obj.MarketingPercent Is Nothing And
                        Not obj.AdminExpense Is Nothing And Not obj.ProfitExpense Is Nothing And
                        Not obj.LossCostPercent Is Nothing Then
                    'If (Convert.ToSingle(obj.CommissionsPercent.Value) + Convert.ToSingle(obj.MarketingPercent.Value) + _
                    '        Convert.ToSingle(obj.AdminExpense.Value) + Convert.ToSingle(obj.ProfitExpense.Value) + _
                    '        Convert.ToSingle(obj.LossCostPercent.Value) > Convert.ToSingle(100)) Then

                    If ((obj.CommissionsPercent.Value) + (obj.MarketingPercent.Value) +
                          (obj.AdminExpense.Value) + (obj.ProfitExpense.Value) +
                          (obj.LossCostPercent.Value) > Convert.ToDecimal(100)) Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function

    End Class

    'Req5804 start
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidGrossAmountPercent
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM013)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageRate = CType(objectToValidate, CoverageRate)
            If Not obj.GrossAmountPercent Is Nothing Then
                If ((obj.GrossAmountPercent.Value) > Convert.ToDecimal(100)) Then
                    Return False
                End If
            End If
            Return True

        End Function

    End Class
    'Req5804 end
    'Req5884 start
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateRenewalNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM015)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageRate = CType(objectToValidate, CoverageRate)
            If obj IsNot Nothing AndAlso obj.LowPrice IsNot Nothing AndAlso obj.HighPrice IsNot Nothing AndAlso obj.RenewalNumber IsNot Nothing Then
                If Not obj.IsProductSetForSequenceRenewalNo Then
                    If CheckRenewalNumber(obj.LowPrice, obj.HighPrice, obj.RenewalNumber, obj) = False Then
                        Me.Message = COVERAGE_RATE_FORM015
                        Return False
                    End If                
                End If
            End If
            Return True

        End Function
        Private Function CheckRenewalNumber(ByVal sNewLow As Assurant.Common.Types.DecimalType, ByVal sNewHigh As Assurant.Common.Types.DecimalType, ByVal sNewRenewalNumber As Assurant.Common.Types.LongType, ByVal oCoverageRate As CoverageRate) As Boolean

            Dim bValid As Boolean = True
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oCoverageRateId As Guid = oCoverageRate.Id
            Dim oTaxRegionId As Guid = Guid.Empty
            Dim oLow, oHigh As Double

            Dim oCoverageRates As DataView = oCoverageRate.GetList(oCoverageRate.CoverageId)
            Dim oRows As DataRowCollection = oCoverageRates.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0

            Dim oNewRenewalNo As Integer
            Dim oRenewalNo As Integer

            If sNewRenewalNumber Is Nothing Then
                oNewRenewalNo = 0
            Else
                oNewRenewalNo = Convert.ToInt32(sNewRenewalNumber.Value)
            End If

            If oRows.Count = 0 Then
                'Inserting only one record
                If oNewRenewalNo <> 0 Then
                    bValid = False
                End If
            Else
                For Each oRow In oRows
                    oCoverageRateId = New Guid(CType(oRow(COVERAGE_RATE_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oRenewalNo = Convert.ToInt32(oRow(RENEWAL_NUMBER))
                    oTaxRegionId = If(IsDBNull(oRow(REGION_ID)), Guid.Empty, New Guid(CType(oRow(REGION_ID), Byte())))

                    If (oLow = oNewLow AndAlso oHigh = oNewHigh AndAlso If(IsNothing(oCoverageRate.RegionId), Guid.Empty, oCoverageRate.RegionId).Equals(oTaxRegionId)) Then
                        If oCoverageRate.Id.Equals(oCoverageRateId) Then

                            If (oNewRenewalNo > 0 And oRenewalNo = 0) Then
                                bValid = False
                                Exit For
                            End If
                        ElseIf (oNewRenewalNo = oRenewalNo) Then
                            bValid = False
                            Exit For
                        End If
                    Else
                        oCount = oCount + 1
                    End If
                Next

                If bValid = True And oRows.Count = oCount Then
                    If oNewRenewalNo <> 0 Then
                        bValid = False
                    End If
                End If

            End If
            Return bValid
        End Function

        Private Function CheckRenewalNumberForProduct(ByVal sNewLow As Assurant.Common.Types.DecimalType, ByVal sNewHigh As Assurant.Common.Types.DecimalType, ByVal sNewRenewalNumber As Assurant.Common.Types.LongType, ByVal oCoverageRate As CoverageRate) As Boolean

            Dim bValid As Boolean = True
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oCoverageRateId As Guid = oCoverageRate.Id
            Dim oTaxRegionId As Guid = Guid.Empty
            Dim oLow, oHigh As Double

            Dim oCoverageRates As DataView = oCoverageRate.GetList(oCoverageRate.CoverageId)
            Dim oRows As DataRowCollection = oCoverageRates.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0

            Dim oNewRenewalNo As Integer
            Dim oRenewalNo As Integer

            If sNewRenewalNumber Is Nothing Then
                oNewRenewalNo = 0
            Else
                oNewRenewalNo = Convert.ToInt32(sNewRenewalNumber.Value)
            End If

            If oRows.Count = 0 Then
                'Inserting only one record
                If oNewRenewalNo <> 0 Then
                    bValid = False
                End If
            Else
                For Each oRow In oRows
                    oCoverageRateId = New Guid(CType(oRow(COVERAGE_RATE_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oRenewalNo = Convert.ToInt32(oRow(RENEWAL_NUMBER))
                    oTaxRegionId = If(IsDBNull(oRow(REGION_ID)), Guid.Empty, New Guid(CType(oRow(REGION_ID), Byte())))

                    If (oLow = oNewLow AndAlso oHigh = oNewHigh AndAlso If(IsNothing(oCoverageRate.RegionId), Guid.Empty, oCoverageRate.RegionId).Equals(oTaxRegionId)) Then
                        If oCoverageRate.Id.Equals(oCoverageRateId) Then
                            'If (oNewRenewalNo > 0 And oRenewalNo = 0) Then
                             If (oNewRenewalNo <> oRenewalNo+1 And oRenewalNo = 0) Then
                                bValid = False
                                Exit For
                            End If
                        ElseIf (oNewRenewalNo <> oRenewalNo+1) Then
                            bValid = False
                            Exit For
                        End If
                    Else
                        oCount = oCount + 1
                    End If
                Next

                If bValid = True And oRows.Count = oCount Then
                    If oNewRenewalNo <> 0 Then
                        bValid = False
                    End If
                End If

            End If
            Return bValid
        End Function
    End Class
    'Req5884 end
#End Region

End Class



