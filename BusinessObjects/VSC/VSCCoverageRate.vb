'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/11/2007)  ********************

Public Class VscCoverageRate
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New VscCoverageRateDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New VscCoverageRateDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(VscCoverageRateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscCoverageRateDAL.COL_NAME_VSC_COVERAGE_RATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property VscRateVersionId As Guid
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_VSC_RATE_VERSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscCoverageRateDAL.COL_NAME_VSC_RATE_VERSION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_VSC_RATE_VERSION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property VscCoverageId As Guid
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_VSC_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscCoverageRateDAL.COL_NAME_VSC_COVERAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_VSC_COVERAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClassCodeId As Guid
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_CLASS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscCoverageRateDAL.COL_NAME_CLASS_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_CLASS_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OdometerLowRange As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_ODOMETER_LOW_RANGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_ODOMETER_LOW_RANGE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_ODOMETER_LOW_RANGE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OdometerHighRange As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_ODOMETER_HIGH_RANGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_ODOMETER_HIGH_RANGE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_ODOMETER_HIGH_RANGE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Deductible As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DiscountedDeductibleAmt As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_AMT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DiscountedDeductiblePcnt As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_PCNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_PCNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_DISCOUNTED_DEDUCTIBLE_PCNT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TermMonths As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_TERM_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_TERM_MONTHS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_TERM_MONTHS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TermKmMi As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_TERM_KM_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_TERM_KM_MI), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_TERM_KM_MI, Value)
        End Set
    End Property



    Public Property CommissionsPercent As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_COMMISSIONS_PERCENT, Value)
        End Set
    End Property



    Public Property MarketingPercent As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_MARKETING_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_MARKETING_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_MARKETING_PERCENT, Value)
        End Set
    End Property



    Public Property AdminExpense As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_ADMIN_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_ADMIN_EXPENSE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_ADMIN_EXPENSE, Value)
        End Set
    End Property



    Public Property ProfitExpense As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_PROFIT_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_PROFIT_EXPENSE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_PROFIT_EXPENSE, Value)
        End Set
    End Property



    Public Property LossCostPercent As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_LOSS_COST_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_LOSS_COST_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_LOSS_COST_PERCENT, Value)
        End Set
    End Property



    Public Property Wp As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_WP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_WP), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_WP, Value)
        End Set
    End Property



    Public Property TaxesPercent As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_TAXES_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_TAXES_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_TAXES_PERCENT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Gwp As DecimalType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_GWP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(VscCoverageRateDAL.COL_NAME_GWP), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_GWP, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EngineManufWarrMonths As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_MONTHS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_MONTHS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EngineManufWarrKmMi As LongType
        Get
            CheckDeleted()
            If row(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_KM_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_KM_MI), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscCoverageRateDAL.COL_NAME_ENGINE_MANUF_WARR_KM_MI, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property VehicleValuerangefrom As LongType
        Get
            CheckDeleted()
            If Row(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_FROM), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_FROM, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property VehicleValuerangeto As LongType
        Get
            CheckDeleted()
            If Row(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_TO), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageRateDAL.COL_NAME_VEHICLE_PURCHASE_PRICE_TO, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VscCoverageRateDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetCoverageRateList(ByVal RateVersionID As Guid, ByVal PlanID As Guid, ByVal EngineWarranty As Guid,
                            Optional ByVal ClassCode As String = "", Optional ByVal TermMon As Integer = -1,
                            Optional ByVal Deductible As Decimal = -1, Optional ByVal Odometer As Integer = -1,
                            Optional ByVal Vehiclevalue As Decimal = -1) As DataView
        Try
            Dim dal As New VSCCoverageRateDAL
            Return dal.LoadList(RateVersionID, PlanID, EngineWarranty, ClassCode, TermMon, Deductible, Odometer, Vehiclevalue).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


