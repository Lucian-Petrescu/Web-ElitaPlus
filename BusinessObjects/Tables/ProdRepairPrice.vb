'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/19/2008)  ********************

Public Class ProdRepairPrice
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
            Dim dal As New ProdRepairPriceDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProdRepairPriceDAL
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

    Private Const THRESHOLD As Double = 0.01

    Private Const PROD_REPAIR_PRICE_ID As Integer = 0
    Private Const LOW_PRICE As Integer = 1
    Private Const HIGH_PRICE As Integer = 2

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ProdRepairPriceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ProdRepairPriceDAL.COL_NAME_PROD_REPAIR_PRICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If row(ProdRepairPriceDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ProdRepairPriceDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProdRepairPriceDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("LowPrice", MIN:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM001), ValidPriceBandRange("")> _
    Public Property PriceRangeFrom() As DecimalType
        Get
            CheckDeleted()
            If Row(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_FROM), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_FROM, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_DOUBLE, Max:=NEW_MAX_DOUBLE, Message:=COVERAGE_RATE_FORM002)> _
     Public Property PriceRangeTo() As DecimalType
        Get
            CheckDeleted()
            If Row(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_TO), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ProdRepairPriceDAL.COL_NAME_PRICE_RANGE_TO, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If row(ProdRepairPriceDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ProdRepairPriceDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProdRepairPriceDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProdRepairPriceDAL
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
    Public Shared Function getList(ByVal ProductCodeId As Guid, ByVal LanguageId As Guid) As ProdRepairPriceSearchDV
        Try
            Dim dal As New ProdRepairPriceDAL
            Return New ProdRepairPriceSearchDV(dal.LoadList(ProductCodeId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "ProdRepairPriceSearchDV"
    Public Class ProdRepairPriceSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PROD_REPAIR_PRICE_ID As String = "prod_repair_price_id"
        Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
        Public Const COL_NAME_PRICE_RANGE_FROM As String = "price_range_from"
        Public Const COL_NAME_PRICE_RANGE_TO As String = "price_range_to"
        Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
        Public Const COL_NAME_METHOD_OF_REPAIR_DESC As String = "method_of_repair_desc"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateRecord
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PRICE_GROUP_DETAIL_DUPLICATE_RECORD)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProdRepairPrice = CType(objectToValidate, ProdRepairPrice)
            Return Not obj.CheckForDuplicateRepairMethodRangeProductCodeRangeFromCombination
        End Function
    End Class

    Protected Function CheckForDuplicateRepairMethodRangeProductCodeRangeFromCombination() As Boolean
        Dim row As DataRow
        For Each row In Me.Dataset.Tables(ProdRepairPriceDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New ProdRepairPrice(row)
                If Not bo.Id.Equals(Me.Id) AndAlso bo.MethodOfRepairId.Equals(Me.MethodOfRepairId) AndAlso bo.ProductCodeId.Equals(Me.ProductCodeId) AndAlso bo.PriceRangeFrom.Equals(Me.PriceRangeFrom) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPriceBandRange
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, COVERAGE_RATE_FORM009)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProdRepairPrice = CType(objectToValidate, ProdRepairPrice)

            Dim bValid As Boolean = True

            If Not obj.PriceRangeFrom Is Nothing And Not obj.PriceRangeTo Is Nothing Then
                If Convert.ToSingle(obj.PriceRangeFrom.Value) > Convert.ToSingle(obj.PriceRangeTo.Value) Then
                    Me.Message = COVERAGE_RATE_FORM009
                    bValid = False
                ElseIf ValidateRange(obj.PriceRangeFrom, obj.PriceRangeTo, obj) = False Then
                    Me.Message = COVERAGE_RATE_FORM011
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        ' It validates that the price range falls within the previous and next range +- THRESHOLD
        Private Function ValidateRange(ByVal sNewLow As Assurant.Common.Types.DecimalType, ByVal sNewHigh As Assurant.Common.Types.DecimalType, ByVal oProdRepairPrice As ProdRepairPrice) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Double = Math.Round(Convert.ToDouble(sNewLow.Value), 2)
            Dim oNewHigh As Double = Math.Round(Convert.ToDouble(sNewHigh.Value), 2)
            Dim oProdRepairPriceId As Guid = oProdRepairPrice.Id
            Dim oLow, oHigh As Double
            Dim prevLow As Double = MIN_DOUBLE - THRESHOLD
            Dim prevHigh As Double = MIN_DOUBLE - THRESHOLD
            Dim oProdRepairPriceBand As DataView = oProdRepairPrice.getList(oProdRepairPrice.ProductCodeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim oRows As DataRowCollection = oProdRepairPriceBand.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oProdRepairPriceId = New Guid(CType(oRow(PROD_REPAIR_PRICE_ID), Byte()))
                    oLow = Math.Round(Convert.ToDouble(oRow(LOW_PRICE)), 2)
                    oHigh = Math.Round(Convert.ToDouble(oRow(HIGH_PRICE)), 2)
                    oCount = oCount + 1
                    If oProdRepairPrice.Id.Equals(oProdRepairPriceId) Then
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




