'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/3/2006)  ********************

Public Class ProductCode
    Inherits BusinessObjectBase
    Implements IAttributable

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

    Public Sub New(ByVal dealerId As Guid, ByVal productCode As String, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(dealerId, productCode)
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
            Dim dal As New ProductCodeDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal dealerId As Guid, ByVal productCode As String)
        Try
            Dim dal As New ProductCodeDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(Id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByDealerProduct(Dataset, dealerId, productCode)
                Row = FindRow(dealerId, dal.COL_NAME_DEALER_ID, productCode, dal.COL_NAME_PRODUCT_CODE, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductCodeDAL
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

    Public Sub LoadProductBenefits()
        Try
            Dim dal As New ProductEquipmentDAL

            dal.LoadBenefitsList(Dataset, Id)

            AddChildrenCollection(GetType(ProductEquipment.ProductBenefitsDetailList))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        UpgradeProgramId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Inuseflag = "N"
    End Sub
#End Region

#Region "Constants"

    Dim EMPTY_GRID_ID As String = "00000000000000000000000000000000"

#End Region

#Region "Properties"

    'Key Property

    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return ProductCodeDAL.TABLE_NAME.ToUpper()
        End Get
    End Property

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(ProductCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")>
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5)>
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RiskGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_RISK_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_RISK_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_RISK_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property PriceMatrixId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PRICE_MATRIX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_PRICE_MATRIX_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PRICE_MATRIX_ID, Value)
        End Set
    End Property


    Public Property PercentOfRetail() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PERCENT_OF_RETAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.COL_NAME_PERCENT_OF_RETAIL), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PERCENT_OF_RETAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TypeOfEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseDepreciation() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_USE_DEPRECIATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_USE_DEPRECIATION), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_USE_DEPRECIATION, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property BundledItemId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_BUNDLED_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_BUNDLED_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_BUNDLED_ITEM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property MethodOfRepairByPriceId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_BY_PRICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_BY_PRICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_METHOD_OF_REPAIR_BY_PRICE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property SplitWarrantyId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_SPLIT_WARRANTY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_SPLIT_WARRANTY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_SPLIT_WARRANTY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1000)>
    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property SpecialService() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_SPECIAL_SERVICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_SPECIAL_SERVICE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_SPECIAL_SERVICE, Value)
        End Set
    End Property


    '<ValueMandatory("")> _
    <Valid_BillingFrequency("")>
    Public Property BillingFrequencyId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_BILLING_FREQUENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_BILLING_FREQUENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_BILLING_FREQUENCY_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99), Valid_Numb_Of_Installment("")>
    Public Property NumberOfInstallments() As Integer
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_NUMBER_OF_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_NUMBER_OF_INSTALLMENTS), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_NUMBER_OF_INSTALLMENTS, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999), ValidateNumOfClaims("")>
    Public Property NumOfClaims() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_NUM_OF_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_NUM_OF_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_NUM_OF_CLAIMS, Value)
        End Set
    End Property

    ' <Valid_Upfront_Commission("")> _
    '<ValueMandatory("")> _
    'Public Property UpfrontCommissionId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(ProductCodeDAL.COL_NAME_UPFRONT_COMM_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPFRONT_COMM_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(ProductCodeDAL.COL_NAME_UPFRONT_COMM_ID, Value)
    '    End Set
    'End Property
    <ValidNumericRange("", Max:=99)>
    Public Property ClaimWaitingPeriod() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CLAIM_WAITING_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_CLAIM_WAITING_PERIOD), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CLAIM_WAITING_PERIOD, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=999)>
    Public Property FullRefundDays() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_FULL_REFUND_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_FULL_REFUND_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_FULL_REFUND_DAYS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UpgradeProgramId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_PROGRAM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_PROGRAM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_PROGRAM_ID, Value)
        End Set
    End Property

    Public ReadOnly Property UpgradeFinanceBalanceComputationMethod As String
        Get
            If (UPGFinanceBalCompMethId <> Guid.Empty) Then
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_UPG_FINANCE_BAL_COMP_METH, UPGFinanceBalCompMethId)
            Else
                Return Codes.UPG_FINANCE_BAL_COMP_METH__NONE
            End If
        End Get
    End Property

    Public Property UPGFinanceBalCompMethId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPG_FINANCE_BAL_COMP_METH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPG_FINANCE_BAL_COMP_METH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPG_FINANCE_BAL_COMP_METH_ID, Value)
        End Set
    End Property

    Public Property IgnoreWaitingPeriodWsdPsd() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD, Value)
        End Set
    End Property


    Public Property ProdLiabilityLimitBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_BASE_ID, Value)
        End Set
    End Property
    <ValidateProdLiabilityLimitPolicy("")>
    Public Property ProdLiabilityLimitPolicyId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_POLICY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_POLICY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_POLICY_ID, Value)
        End Set
    End Property
    Public Property PerIncidentLiabilityLimitCap() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PER_INCIDENT_LIABILITY_LIMIT_CAP, Value)
        End Set
    End Property

    Public Property ProdLiabilityLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT, Value)
        End Set
    End Property

    Public Property ProdLiabilityLimitPercent() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_PERCENT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PROD_LIABILITY_LIMIT_PERCENT, Value)
        End Set
    End Property

    '<ValidNumericRange("", Min:=1, MinExclusive:=False, Max:=100, MaxExclusive:=False, Message:=Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)> _
    Public Property ClaimAutoApprovePsp() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CLAIM_AUTO_APPROVE_PSP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.COL_NAME_CLAIM_AUTO_APPROVE_PSP), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CLAIM_AUTO_APPROVE_PSP, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999, MaxExclusive:=False)>
    Public Property NumOfRepairClaims() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_NUM_OF_REPAIR_CLAIMS, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999, MaxExclusive:=False)>
    Public Property NumOfReplacementClaims() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_NUM_OF_REPLACEMENT_CLAIMS, Value)
        End Set
    End Property

    'pavan REQ-5733
    ' <ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode1() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_1, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode2() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_2, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode3() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_3, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode4() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_4), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_4, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode5() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_5), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_5, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode6() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_6), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_6, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode7() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_7), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_7, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode8() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_8), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_8, Value)
        End Set
    End Property


    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode9() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_9), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_9, Value)
        End Set
    End Property

    'pavan REQ-5733
    '<ValidNumericRange("", Min:=0, Max:=99, MaxExclusive:=False), ValidNoOfReplacementClaims(""), MandatoryNoOfReplacementClaimsAttribute("")> _
    Public Property AnalysisCode10() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_10), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ANALYSIS_CODE_10, Value)
        End Set
    End Property
    Public Property IsReInsuredId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_IS_REINSURED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_IS_REINSURED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_IS_REINSURED_ID, Value)
        End Set
    End Property

    Public Property UpgFinanceInfoRequireId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPG_FINANCE_INFO_REQUIRE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPG_FINANCE_INFO_REQUIRE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPG_FINANCE_INFO_REQUIRE_ID, Value)
        End Set
    End Property

    Public Property UpgradeTermUomId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_UOM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_UOM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_TERM_UOM_ID, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=9999)>
    Public Property UpgradeTermFrom() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_FROM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_TERM_FROM, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=9999)>
    Public Property UpgradeTermTo() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_TERM_TO), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_TERM_TO, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property UpgradeFixedTerm() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_FIXED_TERM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_FIXED_TERM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_FIXED_TERM, Value)
        End Set
    End Property

    Public Property InstallmentRepricableId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_INSTALLMENT_REPRICABLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_INSTALLMENT_REPRICABLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_INSTALLMENT_REPRICABLE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property Inuseflag() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_INUSEFLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_INUSEFLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_INUSEFLAG, Value)
        End Set
    End Property

    Public Property CnlDependencyId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CNL_DEPENDENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_CNL_DEPENDENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CNL_DEPENDENCY_ID, Value)
        End Set
    End Property

    Public Property BillingCriteriaId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_BILLING_CRITERIA_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_BILLING_CRITERIA_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_BILLING_CRITERIA_ID, Value)
        End Set
    End Property

    Public Property PostPrePaidId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_POST_PRE_PAID_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_POST_PRE_PAID_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_POST_PRE_PAID_ID, Value)
        End Set
    End Property

    Public Property CnlLumpsumBillingId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CNL_LUMPSUM_BILLING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_CNL_LUMPSUM_BILLING_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CNL_LUMPSUM_BILLING_ID, Value)
        End Set
    End Property
    'REQ-5980
    Public ReadOnly Property IsParentProduct() As Boolean
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_IS_PARENT_PRODUCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(ProductCodeDAL.COL_NAME_IS_PARENT_PRODUCT).ToString.ToUpper = "T"
            End If
        End Get
    End Property
    <ValueMandatory("")>
    Public Property ProductEquipmentValidation() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_IS_PRODUCT_EQUIPMENT_VALIDATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_IS_PRODUCT_EQUIPMENT_VALIDATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_IS_PRODUCT_EQUIPMENT_VALIDATION, Value)
        End Set
    End Property
    Public Property UpgradeFee() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPGRADE_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.COL_NAME_UPGRADE_FEE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPGRADE_FEE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property AllowRegisteredItems() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_ALLOW_REGISTERED_ITEMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_ALLOW_REGISTERED_ITEMS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_ALLOW_REGISTERED_ITEMS, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property MaxAgeOfRegisteredItem() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_MAX_AGE_OF_REGISTERED_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_MAX_AGE_OF_REGISTERED_ITEM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_MAX_AGE_OF_REGISTERED_ITEM, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property MaxRegistrationsAllowed() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_MAX_REGISTRATIONS_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_MAX_REGISTRATIONS_ALLOWED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_MAX_REGISTRATIONS_ALLOWED, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=999)>
    Public Property MaxClaimsAllowedPerRegisteredItem() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CLAIM_LIMIT_PER_REG_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_CLAIM_LIMIT_PER_REG_ITEM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CLAIM_LIMIT_PER_REG_ITEM, Value)
        End Set
    End Property
    <ListOfDeviceGroupsValidation("")>
    Public Property ListForDeviceGroups() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUPS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUPS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUPS, Value)
        End Set
    End Property
    Public Property ListForDeviceGroupCode() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUP_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_LIST_FOR_DEVICE_GROUP_CODE, Value)
        End Set
    End Property
    <UpdateReplaceRegItemsIdValidation("")>
    Public Property UpdateReplaceRegItemsId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_UPDATE_REPLACE_REG_ITEMS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeDAL.COL_NAME_UPDATE_REPLACE_REG_ITEMS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_UPDATE_REPLACE_REG_ITEMS_ID, Value)
        End Set
    End Property
    <ValidNumericRange("", Min:=1, Max:=999)>
    Public Property RegisteredItemsLimit() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_REGISTERED_ITEMS_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_REGISTERED_ITEMS_LIMIT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_REGISTERED_ITEMS_LIMIT, Value)
        End Set
    End Property

    Public Property _ClaimRecordingXcd() As String
        Get
            Return mClaimRecordingXcd
        End Get
        Set(ByVal Value As String)
            mClaimRecordingXcd = Value
        End Set
    End Property
    Private mClaimRecordingXcd As String

    'REG-6289
    'COL_NAME_PROD_LIMIT_APPLICABLE_TO_XCD
    REM ValidateClaimLiabilityLimitAppliedTo
    <ValidateClaimLiabilityLimitAppliedTo("")>
    Public Property ProductLimitApplicableToXCD() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PROD_LIMIT_APPLICABLE_TO_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_PROD_LIMIT_APPLICABLE_TO_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PROD_LIMIT_APPLICABLE_TO_XCD, Value)
        End Set
    End Property

    Public Property CancellationWithinTerm() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CANCELLATION_WITHIN_TERM_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_CANCELLATION_WITHIN_TERM_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CANCELLATION_WITHIN_TERM_XCD, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=1, Max:=999)>
    Public Property ExpirationNotificationDays() As LongType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_EXPIRATION_NOTIFICATION_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductCodeDAL.COL_NAME_EXPIRATION_NOTIFICATION_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_EXPIRATION_NOTIFICATION_DAYS, Value)
        End Set
    End Property
    Public Property FullillmentReimburesementThreshold() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.ColNameFulfillmentReimThreshold) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeDAL.ColNameFulfillmentReimThreshold), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ProductCodeDAL.ColNameFulfillmentReimThreshold, value)
        End Set
    End Property

    Public Property BenefitEligibleFlagXCD() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_XCD, Value)
        End Set
    End Property
    <BenefitEligibleActionValidation("")>
    Public Property BenefitEligibleActionXCD() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_ACTION_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_ACTION_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_BENEFIT_ELIGIBLE_ACTION_XCD, Value)
        End Set
    End Property

    Public Property CalcCovgEndDateBasedOnXCD() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_CALC_COVG_END_DATE_BASED_ON_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_CALC_COVG_END_DATE_BASED_ON_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_CALC_COVG_END_DATE_BASED_ON_XCD, Value)
        End Set
    End Property

    Public Property ClaimProfile() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.ColNameClaimProfileCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.ColNameClaimProfileCode), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.ColNameClaimProfileCode, Value)
        End Set
    End Property

    Public Property PriceMatrixUsesWpXcd() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_PRICE_MATRIX_USES_WP_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_PRICE_MATRIX_USES_WP_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_PRICE_MATRIX_USES_WP_XCD, Value)
        End Set
    End Property

    Public Property ExpectedPremiumIsWpXcd() As String
        Get
            CheckDeleted()
            If Row(ProductCodeDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ProductCodeDAL.COL_NAME_EXPECTED_PREMIUM_IS_WP_XCD, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductCodeDAL
                'dal.Update(Me.Row)
                MyBase.UpdateFamily(Dataset)
                dal.UpdateFamily(Dataset)
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

    Public Shared Sub UpdateCoverageLiability(ByVal ProductCodeId As System.Guid)
        If ProductCodeId <> Guid.Empty Then
            Dim dal As New ProductCodeDAL
            dal.UpdateCoverageLiability(ProductCodeId)
        End If
    End Sub
    Public Shared Function UpdateCoverageReinsurance(ByVal ProductCodeId As System.Guid, Optional ByVal ModeOperation As String = Nothing) As Integer
        If ProductCodeId <> Guid.Empty Then
            Dim dal As New ProductCodeDAL
            Return dal.UpdateCoverageReinsurance(ProductCodeId, ModeOperation)
        End If
    End Function
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse IsChildrenDirty

            Return bDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ProductCode)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product")
        End If
        'Copy myself
        CopyFrom(original)

        Dim selREGDv As DataView '= original.GetSelectedMethodOfRepair
        Dim selRegList As New ArrayList
        Dim CountryId As Guid

        'child Regions                    
        CountryId = Dealer.GetDealerCountryId(original.DealerId)
        selREGDv = original.GetSelectedRegions(CountryId)
        For n As Integer = 0 To selREGDv.Count - 1
            selRegList.Add(New Guid(CType(selREGDv(n)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachRegions(selRegList)

        'child Policies
        Dim dr As DataRow
        Dim EquipDV As DataView = LookupListNew.GetTypeOfEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Dim EqiupDesc As String
        Dim ExtProddv As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Dim ExtProdDesc As String
        For Each dr In original.ProductPolicyDetailChildren.Table.Rows
            EqiupDesc = LookupListNew.GetDescriptionFromId(EquipDV, GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID), Byte())))
            ExtProdDesc = LookupListNew.GetDescriptionFromId(ExtProddv, GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID), Byte())))

            CreateProductPolicyDetail(GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID), Byte())),
                                         EqiupDesc, Id,
                                         GuidControl.ByteArrayToGuid(CType(dr(ProductPolicy.ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID), Byte())),
                                         ExtProdDesc, CType(dr(ProductPolicy.ProductPolicySearchDV.COL_POLICY_NUM), Long))

        Next

    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetProductCode(ByVal dealerID As Guid, ByVal ProductCodeDate As Date) As ProductCode

        'Dim ProductCodeID As Guid
        'Dim dv As DataView = getList(dealerID)
        'dv.Sort = ProductCode.ProductCodeSearchDV.COL_EFFECTIVE & " DESC," & ProductCode.ProductCodeSearchDV.COL_EXPIRATION & " DESC"
        'Dim dt As DataTable = dv.Table

        'For Each row As DataRow In dt.Rows
        '    Dim MinEffective As Date = CType(row(ProductCode.ProductCodeSearchDV.COL_EFFECTIVE), Date)
        '    Dim MaxExpiration As Date = CType(row(ProductCode.ProductCodeSearchDV.COL_EXPIRATION), Date)
        '    If (ProductCodeDate >= MinEffective) And (ProductCodeDate < MaxExpiration) Then

        '        ProductCodeID = New Guid(CType(row(ProductCode.ProductCodeSearchDV.COL_ProductCode_ID), Byte()))
        '        Return New ProductCode(ProductCodeID)
        '    End If
        'Next



        Return Nothing
    End Function

    Public Shared Function getList(ByVal compIds As ArrayList, ByVal dealerId As Guid,
                             ByVal RiskGroupId As Guid, ByVal productCodeMask As String, ByVal LanguageId As Guid) As ProductCodeSearchDV
        Try
            Dim dal As New ProductCodeDAL
            Return New ProductCodeSearchDV(dal.LoadList(compIds, dealerId, RiskGroupId, productCodeMask, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetProductCodeIDs(ByVal dealerID As Guid) As ArrayList

        Dim dal As New ProductCodeDAL
        Dim ds As DataSet = dal.LoadProductCodeIDs(dealerID)

        Dim dtProductCodeIDs As DataTable = ds.Tables(dal.TABLE_NAME)

        Dim ProductCodeIDs = New ArrayList

        If dtProductCodeIDs.Rows.Count > 0 Then
            Dim index As Integer
            ' Create Array
            For index = 0 To dtProductCodeIDs.Rows.Count - 1
                If Not dtProductCodeIDs.Rows(index)(dal.COL_NAME_PRODUCT_CODE_ID) Is System.DBNull.Value Then
                    ProductCodeIDs.Add(New Guid(CType(dtProductCodeIDs.Rows(index)(dal.COL_NAME_PRODUCT_CODE_ID), Byte())))
                End If
            Next
        End If

        Return ProductCodeIDs

    End Function

    Public Shared Function getListByDealer(ByVal dealerId As Guid, ByVal LanguageId As Guid, ByVal RiskGroupId As Guid) As ProductCodeSearchByDealerDV
        Try
            Dim dal As New ProductCodeDAL
            Return New ProductCodeSearchByDealerDV(dal.LoadListByDealer(dealerId, LanguageId, RiskGroupId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByDealer(ByVal dealerId As Guid, ByVal LanguageId As Guid) As ProductCodeSearchByDealerDV
        Try
            Dim dal As New ProductCodeDAL
            Return New ProductCodeSearchByDealerDV(dal.LoadListByDealer(dealerId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByDealerForWS(ByVal dealerId As Guid, ByVal WarrSalesDate As Date,
                ByVal sort_by As Integer, ByVal asc_desc_order As String, ByVal productClassCode As String) _
                                As ProductCodeSearchByDealerDVForWS
        Try
            Dim dal As New ProductCodeDAL
            Dim objDealer As New Dealer(dealerId)
            Dim TranslateProductCode_Flag As String = LookupListNew.GetCodeFromId(LookupListNew.LK_TRANSLATE_PRODUCT_CODES, objDealer.ConvertProductCodeId)
            If TranslateProductCode_Flag.Equals(Codes.TPRDC_NO) Then
                Return New ProductCodeSearchByDealerDVForWS(
                    dal.LoadListByDealerForWS(dealerId, WarrSalesDate, sort_by,
                                              asc_desc_order, productClassCode).Tables(0))
            Else
                Return New ProductCodeSearchByDealerDVForWS(
                    dal.LoadListByDealerForWSWithConversion(dealerId, WarrSalesDate, sort_by,
                                            asc_desc_order, productClassCode).Tables(0))
            End If


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Used by Olita Web Service 
    Public Shared Function getDealerProductsInfo(ByRef ds As DataSet, ByVal dealerId As Guid) As DataSet
        Try
            Dim dal As New ProductCodeDAL
            Dim objDealer As New Dealer(dealerId)
            Dim TranslateProductCode_Flag As String = LookupListNew.GetCodeFromId(LookupListNew.LK_TRANSLATE_PRODUCT_CODES, objDealer.ConvertProductCodeId)
            If TranslateProductCode_Flag.Equals(Codes.TPRDC_NO) Then
                Return dal.LoadDealerProductsInfo(ds, dealerId)
            Else
                Return dal.LoadDealerProductsInfoWithConversion(ds, dealerId)
            End If


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetProductCodeId(ByVal DealerId As Guid, ByVal product_code As String) As DataView
        Try
            Dim dal As New ProductCodeDAL
            Return New DataView(dal.GetProductCodeId(DealerId, product_code).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function MethodOfRepairByPriceRecords(ByVal ProductCodeId As Guid) As Boolean
        Try
            Dim dal As New ProductCodeDAL, dv As DataView, oNoOfRecords As Integer
            Dim blnMethodOfRepairByPriceRecords As Boolean = False
            dv = New DataView(dal.MethodOfRepairByPriceRecords(ProductCodeId).Tables(0))

            If dv.Count > 0 Then
                oNoOfRecords = CType(dv(0)(0), Integer)
                If oNoOfRecords > 0 Then
                    blnMethodOfRepairByPriceRecords = True
                Else
                    blnMethodOfRepairByPriceRecords = False
                End If
            End If
            Return blnMethodOfRepairByPriceRecords
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "ProductCodeSearchDV"
    Public Class ProductCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ProductCode_ID As String = "Product_Code_id"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_RISK_GROUP As String = "risk_group"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_COMPANY_CODE As String = "COMPANY_CODE"
        Public Const COL_LAYOUT As String = "layout"


#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "ProductCodeSearchByDealerDV"
    Public Class ProductCodeSearchByDealerDV
        Inherits DataView

        Public Const COL_Product_Code_ID As String = "PRODUCT_CODE_ID"
        Public Const COL_PRODUCT_CODE As String = "PRODUCT_CODE"
        Public Const COL_RISK_GROUP As String = "RISK_GROUP"
        Public Const COL_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_BUNDLED_ITEM As String = "BUNDLED_ITEM"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class ProductCodeSearchByDealerDVForWS
        Inherits DataView
        Public Const COL_PRODUCT_CODE_DESCRIPTION As String = "product_code_description"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_CERTIFICATE_DURATION As String = "certificate_duration"
        Public Const COL_EXTERNAL_PRODUCT_CODE As String = "external_product_code"
        Public Const COL_BUNDLED_FLAG As String = "bundled_flag"
        Public Const COL_PERCENT_OF_RETAIL As String = "percent_of_retail"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#End Region


#Region "Children Related"



    'METHODS ADDED MANUALLY. BEGIN
#Region "ProductCodes"


    Public ReadOnly Property ProductRegionChildren() As ProductRegionList
        Get
            Return New ProductRegionList(Me)
        End Get
    End Property

    Public Sub UpdateRegions(ByVal selectedRegionGuidStrCollection As Hashtable)
        If selectedRegionGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ProductRegion
            For Each bo In ProductRegionChildren
                If Not selectedRegionGuidStrCollection.Contains(bo.RegionId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedRegionGuidStrCollection
                If ProductRegionChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim Prdregion As ProductRegion = ProductRegionChildren.GetNewChild()
                    Prdregion.RegionId = New Guid(entry.Key.ToString)
                    Prdregion.ProductCodeId = Id
                    Prdregion.Save()
                End If
            Next
        End If
    End Sub
    Public Sub AttachRegions(ByVal selectedRegionGuidStrCollection As ArrayList)
        Dim Prdregionstr As String
        For Each Prdregionstr In selectedRegionGuidStrCollection
            Dim Prdregion As ProductRegion = ProductRegionChildren.GetNewChild
            Prdregion.RegionId = New Guid(Prdregionstr)
            Prdregion.ProductCodeId = Id
            Prdregion.Save()
        Next
    End Sub


    Public Function AddRegionsChild(ByVal RegionId As Guid) As ProductRegion
        Dim oPrdRegion As ProductRegion

        oPrdRegion = New ProductRegion(Dataset)
        oPrdRegion.RegionId = RegionId
        oPrdRegion.ProductCodeId = Id
        Return oPrdRegion

    End Function
    Public Sub DetachRegions(ByVal selectedRegionGuidStrCollection As ArrayList)
        Dim Prdregionstr As String
        For Each Prdregionstr In selectedRegionGuidStrCollection
            Dim Prdregion As ProductRegion = ProductRegionChildren.Find(New Guid(Prdregionstr))
            Prdregion.Delete()
            Prdregion.Save()
        Next
    End Sub

    Public Function GetAvailableRegions(ByVal countryId As Guid) As DataView
        'Dim dv As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.'ActiveUser.Companies)
        '       Dim sequenceCondition As String = GetProductCodesLookupListSelectedSequenceFilter(dv, False)
        Dim dv As DataView
        Dim sequenceCondition As String
        If Not GuidControl.GuidToHexString(countryId) = EMPTY_GRID_ID Then
            'dv = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'Else
            dv = Region.GetRegionsByUserCountries(countryId)
            sequenceCondition = GetRegionsLookupListSelectedSequenceFilter(dv, False)

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If

        End If
        Return dv
    End Function

    Public Function GetSelectedRegions(ByVal countryId As Guid) As DataView

        Dim dv As DataView
        Dim sequenceCondition As String

        If Not GuidControl.GuidToHexString(countryId) = EMPTY_GRID_ID Then
            dv = Region.GetRegionsByUserCountries(countryId)
            sequenceCondition = GetRegionsLookupListSelectedSequenceFilter(dv, True)

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If
        End If
        Return dv
    End Function

    Protected Function GetRegionsLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim ProdRegionBO As ProductRegion
        Dim inClause As String = "(-1"
        For Each ProdRegionBO In ProductRegionChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, ProdRegionBO.RegionId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function


#Region "Device Types"


    Public ReadOnly Property ProductDeviceTypeChildren() As ProductDeviceTypeList
        Get
            Return New ProductDeviceTypeList(Me)
        End Get
    End Property

    Public Sub AttachDeviceTypes(ByVal selecteddeviceTypeGuidStrCollection As ArrayList)
        Dim Prddevicetypestr As String
        For Each Prddevicetypestr In selecteddeviceTypeGuidStrCollection
            Dim Prddevicetype As ProductEquipment = ProductDeviceTypeChildren.GetNewChild
            Prddevicetype.DeviceTypeId = New Guid(Prddevicetypestr)
            Prddevicetype.ProductCodeId = Id
            Prddevicetype.EffectiveDateProductEquip = DateTime.Now.Date
            Prddevicetype.ExpirationDateProductEquip = DateTime.MaxValue.Date
            Prddevicetype.Save()
        Next
    End Sub


    Public Sub DetachDeviceTypes(ByVal selecteddeviceTypeGuidStrCollection As ArrayList)
        Dim Prddevicetypestr As String
        For Each Prddevicetypestr In selecteddeviceTypeGuidStrCollection
            Dim Prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(New Guid(Prddevicetypestr))
            ' Prdrisktype.Delete()
            Prddevicetype.ExpirationDateProductEquip = DateTime.Now.Date

            Prddevicetype.Save()
        Next
    End Sub

    Public Function GetAvailableDeviceTypes(ByVal DeviceType As String) As DataView
        'Dim dv As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.'ActiveUser.Companies)
        '       Dim sequenceCondition As String = GetProductCodesLookupListSelectedSequenceFilter(dv, False)
        Dim dv As DataView
        Dim sequenceCondition As String
        If Not DeviceType = String.Empty Then
            'dv = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'Else

            dv = GetDeviceTypes(DeviceType)
            sequenceCondition = GetDeviceTypesLookupListSelectedSequenceFilter(dv, False)

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If

        End If
        Return dv
    End Function

    Public Function GetSelectedDeviceTypes(ByVal DeviceType As String) As DataView

        Dim dv As DataView
        Dim sequenceCondition As String

        If Not DeviceType = String.Empty Then

            dv = GetDeviceTypes(DeviceType)
            sequenceCondition = GetDeviceTypesLookupListSelectedSequenceFilter(dv, True)

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If
        End If
        Return dv
    End Function

    Public Function GetDeviceTypesProdEquipList(ByVal deviceType As String) As Generic.List(Of ProductEquipment)
        Dim dtList As New Generic.List(Of ProductEquipment)

        Dim i As Integer
        Dim dv As DataView
        dv = GetDeviceTypes(deviceType)
        For i = 0 To dv.Count - 1
            Dim prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(New Guid(CType(dv(i)(LookupListNew.COL_ID_NAME), Byte())))
            If Not prddevicetype Is Nothing Then
                dtList.Add(prddevicetype)
            End If
        Next
        Return dtList
    End Function
    Public Function AddDeviceTypesProdEquip(ByVal deviceTypeId As Guid) As ProductEquipment

        Dim prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(deviceTypeId)
        If prddevicetype Is Nothing Then
            prddevicetype = ProductDeviceTypeChildren.GetNewChild
        End If
        prddevicetype.DeviceTypeId = deviceTypeId
        prddevicetype.ProductCodeId = Id
        prddevicetype.EffectiveDateProductEquip = DateTime.Now.Date
        prddevicetype.ExpirationDateProductEquip = DateTime.MaxValue.Date
        'Prddevicetype.MethodOfRepairXCD = DeviceTypeProductEquipment.MethodOfRepairXCD
        prddevicetype.Save()
        Return (prddevicetype)
    End Function
    Public Sub RemoveDeviceTypesProdEquip(ByVal deviceTypeId As Guid)

        Dim prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(deviceTypeId)
        If Not prddevicetype Is Nothing Then
            prddevicetype.Delete()
        End If

    End Sub
    Public Sub SaveDeviceTypesProdEquip(ByVal workingItemPrddevicetype As ProductEquipment)
        Dim prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(workingItemPrddevicetype.DeviceTypeId)
        If Not prddevicetype Is Nothing Then
            prddevicetype.MethodOfRepairXcd = workingItemPrddevicetype.MethodOfRepairXcd
            prddevicetype.Save()
        End If
    End Sub
    Public Function GetDeviceTypesProdEquip(ByVal deviceTypeId As Guid) As ProductEquipment
        Dim prddevicetype As ProductEquipment = ProductDeviceTypeChildren.Find(deviceTypeId)
        Return (prddevicetype)
    End Function

    Protected Function GetDeviceTypesLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim ProdDeviceTypeBO As ProductEquipment
        Dim inClause As String = "(-1"
        For Each ProdDeviceTypeBO In ProductDeviceTypeChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, ProdDeviceTypeBO.DeviceTypeId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function

    Public Function GetDeviceTypes(ByVal DeviceType As String) As DataView
        Try

            Dim dv As DataView
            Dim ds As New DataSet
            Dim pdDal As New ProductCodeDAL

            dv = LookupListNew.GetDeviceTypesLookupList(DeviceType, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            ds = dv.Table.DataSet
            'myAddSequenceColumn(ds.Tables(0))


            Return ds.Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region
#End Region

#Region "Extended Attribute"
    'Public ReadOnly Property ProductParentDetailChildren() As ProductPolicy.ProductPolicyDetailList
    '    Get
    '        Return New ProductPolicy.ProductPolicyDetailList(Me)
    '    End Get
    'End Property
#End Region

#Region "Policy"

    Public ReadOnly Property ProductPolicyDetailChildren() As ProductPolicy.ProductPolicyDetailList
        Get
            Return New ProductPolicy.ProductPolicyDetailList(Me)
        End Get
    End Property


    Public Function GetProductPolicyDetailChild(ByVal childId As Guid) As ProductPolicy
        Dim Prdpolicy As ProductPolicy = ProductPolicyDetailChildren.Find(childId)
        Return Prdpolicy

        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function RemoveProductPolicyDetailChild(ByVal childId As Guid)
        ProductPolicyDetailChildren.Delete(childId)
        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function GetNewProductPolicyDetailChild() As ProductPolicy

        Dim newProdPolicyDetail As ProductPolicy = ProductPolicyDetailChildren.GetNewChild
        newProdPolicyDetail.ProductCodeId = Id
        Return newProdPolicyDetail
    End Function

    'Public Function GetNewParent() As ProductCodeParent

    '    Dim newParentProd As ProductCodeParent = New ProductCodeParent(Me.Id)
    '    'newParentProd.ProductCodeId = Me.Id
    '    Return newParentProd
    'End Function

    Public Sub CreateProductPolicyDetail(ByVal TypeOfEquipId As Guid, ByVal TypeEqip As String, ByVal ProdCodeId As Guid, ByVal ExtProdCodeId As Guid,
                                        ByVal Extprodcode As String, ByVal policy As Long)
        Dim MyPymntGrpDetailChildBO As ProductPolicy

        MyPymntGrpDetailChildBO = GetNewProductPolicyDetailChild()
        MyPymntGrpDetailChildBO.BeginEdit()

        MyPymntGrpDetailChildBO.ProductCodeId = ProdCodeId
        MyPymntGrpDetailChildBO.TypeOfEquipmentId = TypeOfEquipmentId
        MyPymntGrpDetailChildBO.TypeOfEquipment = TypeEqip
        MyPymntGrpDetailChildBO.ExternalProdCodeId = ExtProdCodeId
        MyPymntGrpDetailChildBO.ExternalProdCode = Extprodcode
        MyPymntGrpDetailChildBO.Policy = policy


        MyPymntGrpDetailChildBO.Save()
        MyPymntGrpDetailChildBO.EndEdit()
    End Sub

#End Region

#Region "Product Rewards"

    Public ReadOnly Property ProductRewardsDetailChildren() As ProductRewards.ProductRewardsDetailList
        Get
            Return New ProductRewards.ProductRewardsDetailList(Me)
        End Get
    End Property


    Public Function GetProductRewardsDetailChild(ByVal childId As Guid) As ProductRewards
        Dim Prdrewards As ProductRewards = ProductRewardsDetailChildren.Find(childId)
        Return Prdrewards

        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function RemoveProductRewardsDetailChild(ByVal childId As Guid)
        ProductRewardsDetailChildren.Delete(childId)
        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function GetNewProductRewardsDetailChild() As ProductRewards

        Dim newProdRewardsDetail As ProductRewards = ProductRewardsDetailChildren.GetNewChild
        newProdRewardsDetail.ProductCodeId = Id
        Return newProdRewardsDetail
    End Function
    'Public Function GetNewParent() As ProductCodeParent

    '    Dim newParentProd As ProductCodeParent = New ProductCodeParent(Me.Id)
    '    'newParentProd.ProductCodeId = Me.Id
    '    Return newParentProd
    'End Function

    Public Sub CreateProductRewardsDetail(ByVal ProdCodeId As Guid, ByVal RewardName As String, ByVal RewardType As String, ByVal RewardAmount As Integer, ByVal MinPurchasePrice As Integer,
                                        ByVal DaysToRedeem As Integer, ByVal EffectiveDate As Date, ByVal ExpirationDate As Date)
        Dim MyPymntGrpDetailChildBO As ProductRewards

        MyPymntGrpDetailChildBO = GetNewProductRewardsDetailChild()
        MyPymntGrpDetailChildBO.BeginEdit()

        MyPymntGrpDetailChildBO.ProductCodeId = ProdCodeId
        MyPymntGrpDetailChildBO.RewardName = RewardName
        MyPymntGrpDetailChildBO.RewardType = RewardType
        MyPymntGrpDetailChildBO.RewardAmount = RewardAmount
        MyPymntGrpDetailChildBO.MinPurchasePrice = MinPurchasePrice
        MyPymntGrpDetailChildBO.DaysToRedeem = DaysToRedeem
        MyPymntGrpDetailChildBO.EffectiveDate = EffectiveDate
        MyPymntGrpDetailChildBO.EffectiveDate = ExpirationDate

        MyPymntGrpDetailChildBO.Save()
        MyPymntGrpDetailChildBO.EndEdit()
    End Sub

#End Region

#Region "Product Benefits"
    Public ReadOnly Property ProductBenefitsDetailChildren() As ProductEquipment.ProductBenefitsDetailList
        Get
            Return New ProductEquipment.ProductBenefitsDetailList(Me)
        End Get
    End Property


    Public Function GetProductBenefitsDetailChild(ByVal childId As Guid) As ProductEquipment
        Dim Prdbenefits As ProductEquipment = ProductBenefitsDetailChildren.Find(childId)
        Return Prdbenefits

        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function RemoveProductBenefitsDetailChild(ByVal childId As Guid)
        ProductBenefitsDetailChildren.Delete(childId)
        'Return CType(Me.ProductPolicyDetailChildren.GetChild(childId), ProductPolicy)
    End Function

    Public Function GetNewProductBenefitsDetailChild() As ProductEquipment

        Dim newProdBenefitsDetail As ProductEquipment = ProductBenefitsDetailChildren.CreateNewChild()
        newProdBenefitsDetail.ProductCodeId = Id
        newProdBenefitsDetail.ConfigPurposeXcd = ProductEquipmentDAL.BENEFITS_PURPOSE
        'newProdBenefitsDetail.EffectiveDateProductEquip = DateTime.Today
        'newProdBenefitsDetail.ExpirationDateProductEquip = DateTime.Today

        Return newProdBenefitsDetail
    End Function
#End Region

#Region "Depreciation Schedule"

    Public ReadOnly Property ProductDepreciationScdChildren() As DepreciationScdRelation.ProductDepreciationScdList
        Get
            Return New DepreciationScdRelation.ProductDepreciationScdList(Me)
        End Get
    End Property

    Public Function GetProductDepreciationScdChild() As DepreciationScdRelation
        Dim productDepreciationScd As DepreciationScdRelation = ProductDepreciationScdChildren.Find(Id)
        If productDepreciationScd Is Nothing Then
            productDepreciationScd = ProductDepreciationScdChildren.GetNewChild
            productDepreciationScd.TableReference = TableName
            productDepreciationScd.TableReferenceId = Id
            productDepreciationScd.EffectiveDate = DateTime.Now
            productDepreciationScd.ExpirationDate = DateTime.MaxValue
            productDepreciationScd.DepreciationScheduleUsageXcd = Codes.DEPRECIATION_SCHEDULE_USAGE__CASH_REIMBURSE
            productDepreciationScd.DepreciationScheduleId = Guid.Empty
        End If
        Return productDepreciationScd
    End Function

    Public Sub AddProductDepreciationScdChild(ByVal depreciationScheduleId As Guid)

        Dim productDepreciationScd As DepreciationScdRelation = GetProductDepreciationScdChild()

        If Not depreciationScheduleId.Equals(Guid.Empty) Then
            productDepreciationScd.DepreciationScheduleId = depreciationScheduleId
        Else
            productDepreciationScd.Delete()
        End If

    End Sub

#End Region
#End Region



#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class Valid_BillingFrequency
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BILLING_FREQ_REQD_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If (Not obj.DealerId = Guid.Empty) Then
                Dim oDealer As Dealer = New Dealer(obj.DealerId)
                If LookupListNew.GetCodeFromId(LookupListNew.LK_INSTALLMENT_DEFINITION, oDealer.UseInstallmentDefnId) = Codes.INSTALLMENT_DEFINITION__INCOMING Then
                    Return True
                End If

                If obj.BillingFrequencyId.Equals(Guid.Empty) OrElse obj.BillingFrequencyId = Nothing Then Return False
            End If

            Return True

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class Valid_Numb_Of_Installment
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_NUMBER_OF_INSTALLMENT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If (Not obj.DealerId = Guid.Empty) Then
                Dim oDealer As Dealer = New Dealer(obj.DealerId)
                Dim dealerInstallmentDefCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_INSTALLMENT_DEFINITION, oDealer.UseInstallmentDefnId)

                If dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__INCOMING) Then
                    Return True
                End If

                If obj.NumberOfInstallments = Nothing AndAlso Not obj.NumberOfInstallments.Equals(0) Then Return False

                If dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE) AndAlso obj.NumberOfInstallments < 1 Then
                    Return False
                End If

                If dealerInstallmentDefCode.Equals(Codes.INSTALLMENT_DEFINITION__PRODUCT_CODE_OR_CONTRACT) AndAlso obj.NumberOfInstallments < 0 Then
                    Return False
                End If
            End If
            Return True
        End Function

    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatoryNoOfRepairClaimsAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)
            If Not obj.NumOfClaims Is Nothing And obj.NumOfRepairClaims Is Nothing Then
                Dim mandatAttr As New ValueMandatoryAttribute(DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidNoOfRepairClaims
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_NO_OF_REPAIR_CLAIMS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If obj.NumOfClaims Is Nothing Or obj.NumOfRepairClaims Is Nothing Then
                Return True
            ElseIf Not obj.NumOfClaims Is Nothing And Not obj.NumOfRepairClaims Is Nothing Then
                If obj.NumOfRepairClaims.Value > obj.NumOfClaims.Value Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatoryNoOfReplacementClaimsAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)
            If Not obj.NumOfClaims Is Nothing And obj.NumOfReplacementClaims Is Nothing Then
                Dim mandatAttr As New ValueMandatoryAttribute(DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            Else
                Return True
            End If
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidNoOfReplacementClaims
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_NO_OF_REPLACEMENT_CLAIMS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If obj.NumOfClaims Is Nothing Or obj.NumOfReplacementClaims Is Nothing Then
                Return True
            ElseIf Not obj.NumOfClaims Is Nothing And Not obj.NumOfReplacementClaims Is Nothing Then
                If obj.NumOfReplacementClaims.Value > obj.NumOfClaims.Value Then
                    Return False
                Else
                    Return True
                End If
            End If
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ListOfDeviceGroupsValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.LISTOFDEVICEGRPSVALIDATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If Not obj.AllowRegisteredItems Is Nothing And obj.AllowRegisteredItems = "YESNO-Y" Then
                If obj.ListForDeviceGroups = Guid.Empty Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class UpdateReplaceRegItemsIdValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.UPDATEREPLACEREGITEMSIDVALIDATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            Return (Not obj.AllowRegisteredItems Is Nothing) AndAlso
                ((obj.AllowRegisteredItems = "YESNO-Y" And obj.UpdateReplaceRegItemsId <> Guid.Empty) Or
                (obj.AllowRegisteredItems = "YESNO-N" And obj.UpdateReplaceRegItemsId = Guid.Empty))
        End Function
    End Class



    'REQ-6289
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateProdLiabilityLimitPolicy
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_PRODLIABILITY_LIMIT_POLICY_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            ' When "Claim/Liability Limit Base On" is set to a value other than "Not Applicable", "Claim/Liability Limit Policy" is requied
            If ((Not obj Is Nothing) AndAlso (Not obj.ProdLiabilityLimitBasedOnId = Guid.Empty)) Then

                Dim sProdLiabilityLimitBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, obj.ProdLiabilityLimitBasedOnId)

                If Not String.IsNullOrEmpty(sProdLiabilityLimitBasedOnCode) AndAlso
                    (Not sProdLiabilityLimitBasedOnCode = LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE) Then
                    Return Not (obj.ProdLiabilityLimitPolicyId = Guid.Empty)
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateClaimLiabilityLimitAppliedTo
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_CLAIM_LIABILITY_LIMIT_TO)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            REM When the " Claim/Liability limit Base On" flag is set to any value other than "Not Applicable",
            REM the "Claim/Liability limit Appllied To" is required.

            If ((Not obj Is Nothing) AndAlso (Not obj.ProdLiabilityLimitBasedOnId = Guid.Empty)) Then

                Dim sProdLiabilityLimitBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, obj.ProdLiabilityLimitBasedOnId)

                If Not String.IsNullOrEmpty(sProdLiabilityLimitBasedOnCode) AndAlso
                    (Not sProdLiabilityLimitBasedOnCode = LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE) Then
                    Return Not String.IsNullOrEmpty(obj.ProductLimitApplicableToXCD)
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateProdLiabilityLimitBaseOn
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_PROD_CLAIM_LIABILITY_LIMIT_BASEON_DEP)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If ((Not obj Is Nothing) AndAlso (Not obj.ProdLiabilityLimitBasedOnId = Guid.Empty)) _
                   AndAlso LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE <> LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, obj.ProdLiabilityLimitBasedOnId) Then

                Dim sProdLiabilityLimitBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, obj.ProdLiabilityLimitBasedOnId)

                If String.IsNullOrEmpty(sProdLiabilityLimitBasedOnCode) OrElse
                    (sProdLiabilityLimitBasedOnCode = LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE) Then

                    ' When ProdLiabilityLimitBasedOnId is set to NOTAPPL,
                    ' the following fields should be empty

                    If Not obj.ProdLiabilityLimit Is Nothing Or
                        Not obj.ProdLiabilityLimitPercent Is Nothing Or
                        Not obj.ProdLiabilityLimitPolicyId.Equals(Guid.Empty) Or
                        Not String.IsNullOrEmpty(obj.ProductLimitApplicableToXCD) Then

                        Return False
                    End If
                Else
                    If obj.ProdLiabilityLimitPolicyId.Equals(Guid.Empty) Or
                        String.IsNullOrEmpty(obj.ProductLimitApplicableToXCD) Then

                        Return False
                    End If
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateNumOfClaims
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_NUMOF_CLAIMS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCode = CType(objectToValidate, ProductCode)


            If ((Not obj Is Nothing) AndAlso (Not obj.ProdLiabilityLimitBasedOnId = Guid.Empty)) Then

                Dim dealerBO As Dealer = New Dealer(obj.DealerId)
                If Not dealerBO Is Nothing Then

                    If dealerBO.AttributeValues.Contains(Codes.DLR_ATTRBT__VALIDATE_NUM_OF_CLAIMS) _
                        AndAlso dealerBO.AttributeValues.Value(Codes.DLR_ATTRBT__VALIDATE_NUM_OF_CLAIMS) = Codes.YESNO_Y Then

                        Dim sProdLiabilityLimitBasedOnCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCT_LIABILITY_LIMIT_BASE, obj.ProdLiabilityLimitBasedOnId)

                        If Not String.IsNullOrEmpty(sProdLiabilityLimitBasedOnCode) _
                            AndAlso (sProdLiabilityLimitBasedOnCode <> LookupListNew.LK_COVERAGE_NOT_APPLICABLE_TYPE) Then


                            If Not String.IsNullOrEmpty(obj.ProductLimitApplicableToXCD) _
                                AndAlso (obj.ProductLimitApplicableToXCD = LookupListNew.LK_PROD_LIMIT_APPLICABLE_TO_ALL _
                                Or obj.ProductLimitApplicableToXCD = LookupListNew.LK_PROD_LIMIT_APPLICABLE_TO_CLAIMONLY) Then

                                If obj.NumOfClaims Is Nothing OrElse obj.NumOfClaims.Value = 0 Then
                                    Return False
                                End If
                            End If
                        End If
                    End If

                End If



            End If
            Return True
        End Function
    End Class

    'REQ-6289-END


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class BenefitEligibleActionValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERR_BENEFIT_ELIGIBLE_FLAG_MUST_BE_YES)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As ProductCode = CType(objectToValidate, ProductCode)

            If Not obj Is Nothing AndAlso Not String.IsNullOrEmpty(obj.BenefitEligibleActionXCD) Then
                If obj.BenefitEligibleFlagXCD <> Codes.EXT_YESNO_Y Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

#End Region

    Private Sub CreateProductPolicyDetail(ByVal guid As Guid, ByVal EqiupDesc As String, ByVal guid1 As Guid, ByVal guid2 As Guid)
        Throw New NotImplementedException
    End Sub

End Class























