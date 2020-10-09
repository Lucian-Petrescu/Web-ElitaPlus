'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/13/2004)  ********************

Public Class PriceGroupDetail
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PriceGroupDetailDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New PriceGroupDetailDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
        CarryInPrice = New DecimalType(0)
        CleaningPrice = New DecimalType(0)
        EffectiveDate = New DateType(Date.Parse(Date.Now.ToShortDateString))
        EstimatePrice = New DecimalType(0)
        HomePrice = New DecimalType(0)
        SendInPrice = New DecimalType(0)
        PickUpPrice = New DecimalType(0)
        HourlyRate = New DecimalType(0)
        ReplacementPrice = New DecimalType(0)
        DiscountedPrice = New DecimalType(0)
    End Sub
#End Region

#Region "Constants"
    Private Const COVERAGE_RATE_FORM001 As String = "COVERAGE_RATE_FORM001" ' 0<= LowPrice <1*10^6
    Private Const COVERAGE_RATE_FORM002 As String = "COVERAGE_RATE_FORM002" ' 0<= HighPrice <1*10^6
    Private Const COVERAGE_RATE_FORM009 As String = "COVERAGE_RATE_FORM009" ' LowPrice Must be less or equal than HighPrice
    Private Const COVERAGE_RATE_FORM011 As String = "COVERAGE_RATE_FORM011" ' There should be no overlaps (low/high)
    Private Const MIN_DOUBLE As Double = 0.0
    Private Const MAX_DOUBLE As Double = 999999.99

    Private Const THRESHOLD As Double = 0.01

    Private Const PRICE_GROUP_DETAIL_ID As Integer = 0
    Private Const LOW_PRICE As Integer = 1
    Private Const HIGH_PRICE As Integer = 2

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(PriceGroupDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property PriceGroupId As Guid
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PRICE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_PRICE_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_ID, Value)
            'Set RiskType Description
            Dim dv As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim riskTypeDesc As String = LookupListNew.GetDescriptionFromId(dv, Value)
            SetValue(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_DESC, riskTypeDesc)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PriceGroupDetailDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property HomePrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_HOME_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_HOME_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_HOME_PRICE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CarryInPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_CARRY_IN_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_CARRY_IN_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_CARRY_IN_PRICE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property SendInPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_SEND_IN_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_SEND_IN_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_SEND_IN_PRICE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PickUpPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PICK_UP_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_PICK_UP_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_PICK_UP_PRICE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CleaningPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_CLEANING_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_CLEANING_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_CLEANING_PRICE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property HourlyRate As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_HOURLY_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_HOURLY_RATE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_HOURLY_RATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EstimatePrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_ESTIMATE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_ESTIMATE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_ESTIMATE_PRICE, Value)
        End Set
    End Property

    Public ReadOnly Property RiskTypeDescription As String
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PriceGroupDetailDAL.COL_NAME_RISK_TYPE_DESC), String)
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ReplacementPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_PRICE, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidNumericRange("LowPrice", MIN:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM001), ValidPriceBandRange("")> _
    Public Property PriceBandRangeFrom As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_FROM, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM002)> _
    Public Property PriceBandRangeTo As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_PRICE_BAND_RANGE_TO, Value)
        End Set
    End Property

    Public Property ReplacementTaxType As Guid
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_REPLACEMENT_TAX_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DiscountedPrice As DecimalType
        Get
            CheckDeleted()
            If Row(PriceGroupDetailDAL.COL_NAME_DISCOUNTED_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PriceGroupDetailDAL.COL_NAME_DISCOUNTED_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PriceGroupDetailDAL.COL_NAME_DISCOUNTED_PRICE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PriceGroupDetailDAL
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

    Public Sub Copy(original As PriceGroupDetail)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Group")
        End If
        CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(PriceGroupId As Guid, RiskTypeId As Guid, EffectiveDate As Date) As DataView
        Try
            Dim dal As New PriceGroupDetailDAL
            Dim ds As New DataSet, dv As DataView
            ds = dal.GetList(PriceGroupId, RiskTypeId, EffectiveDate)
            dv = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateRecord
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PRICE_GROUP_DETAIL_DUPLICATE_RECORD)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PriceGroupDetail = CType(objectToValidate, PriceGroupDetail)
            Return Not obj.CheckForDuplicateRiskTypeEffectiveDateRangeFromCombination
        End Function
    End Class

    Protected Function CheckForDuplicateRiskTypeEffectiveDateRangeFromCombination() As Boolean
        Dim row As DataRow
        For Each row In Dataset.Tables(PriceGroupDetailDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New PriceGroupDetail(row)
                If Not bo.Id.Equals(Id) AndAlso bo.RiskTypeId.Equals(RiskTypeId) AndAlso bo.EffectiveDate.Equals(EffectiveDate) AndAlso bo.PriceBandRangeFrom.Equals(PriceBandRangeFrom) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPriceBandRange
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM009)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PriceGroupDetail = CType(objectToValidate, PriceGroupDetail)

            Dim bValid As Boolean = True

            If obj.PriceBandRangeFrom IsNot Nothing And obj.PriceBandRangeTo IsNot Nothing Then
                If Convert.ToSingle(obj.PriceBandRangeFrom.Value) > Convert.ToSingle(obj.PriceBandRangeTo.Value) Then
                    Message = COVERAGE_RATE_FORM009
                    bValid = False
                ElseIf ValidateRange(obj.PriceBandRangeFrom, obj.PriceBandRangeTo, obj) = False Then
                    Message = COVERAGE_RATE_FORM011
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        ' It validates that the price range falls within the previous and next range +- THRESHOLD
        Private Function ValidateRange(sNewLow As Assurant.Common.Types.DecimalType, sNewHigh As Assurant.Common.Types.DecimalType, oPriceGroupDetail As PriceGroupDetail) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oPriceGroupDetailId As Guid = oPriceGroupDetail.Id
            Dim oLow, oHigh As Double
            Dim prevLow As Double = MIN_DOUBLE - THRESHOLD
            Dim prevHigh As Double = MIN_DOUBLE - THRESHOLD
            Dim oPriceBands As DataView = oPriceGroupDetail.GetList(oPriceGroupDetail.PriceGroupId, oPriceGroupDetail.RiskTypeId, oPriceGroupDetail.EffectiveDate)
            Dim oRows As DataRowCollection = oPriceBands.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oPriceGroupDetailId = New Guid(CType(oRow(PRICE_GROUP_DETAIL_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oCount = oCount + 1
                    If oPriceGroupDetail.Id.Equals(oPriceGroupDetailId) Then
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




End Class


