'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/31/2006)  ********************

Public Class CurrencyConversion
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub
    'Exiting BO
    'Public Sub New(ByVal dealerId As Guid, ByVal currency1Id As Guid, ByVal currency2Id As Guid)
    '    MyBase.New()
    '    Me.Dataset = New Dataset
    '    Me.LoadLastRates(dealerId, currency1Id, currency2Id)
    'End Sub
    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
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
            Dim dal As New CurrencyConversionDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New CurrencyConversionDAL
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
#Region "Constants"

    Private Const SEARCH_EXCEPTION As String = "DEALER_IS_REQUIRE" ' Certificate List Search Exception

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(CurrencyConversionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CurrencyConversionDAL.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CurrencyConversionDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidCurrencies("")> _
    Public Property Currency1Id As Guid
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_CURRENCY1_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CurrencyConversionDAL.COL_NAME_CURRENCY1_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_CURRENCY1_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Currency2Id As Guid
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_CURRENCY2_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CurrencyConversionDAL.COL_NAME_CURRENCY2_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_CURRENCY2_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidEffectiveAndExpirationDate(""), ValidEffectiveDate("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CurrencyConversionDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Dim _ExpirationDate As DateType
    <ValueMandatory("")> _
    Public Property ExpirationDate As DateType
        Get
            Return _ExpirationDate
        End Get
        Set
            _ExpirationDate = Value
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Currency1Rate As DoubleType
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_CURRENCY1_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DoubleType(CType(Row(CurrencyConversionDAL.COL_NAME_CURRENCY1_RATE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_CURRENCY1_RATE, Value)
            Currency2Rate = New DoubleType(Math.Round(1 / Currency1Rate.Value, 9))
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Currency2Rate As DoubleType
        Get
            CheckDeleted()
            If Row(CurrencyConversionDAL.COL_NAME_CURRENCY2_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DoubleType(CType(Row(CurrencyConversionDAL.COL_NAME_CURRENCY2_RATE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyConversionDAL.COL_NAME_CURRENCY2_RATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CurrencyConversionDAL
                Select Case Row.RowState
                    Case DataRowState.Added
                        'If Me.IsNew Then
                        SP_Insert()
                        Load(LoadLastRate(DealerId, Currency1Id, Currency2Id))
                    Case DataRowState.Modified
                        dal.Update(Row)
                        Load(Id)
                End Select
                'Reload the Data from the DB
                'If Me.Row.RowState <> DataRowState.Detached Then Me.Load(Me.Id)
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Sub SP_Insert()
        Dim dal As New CurrencyConversionDAL
        Dim oErrMess As String
        oErrMess = dal.ExecuteSP(DealerId, EffectiveDate, ExpirationDate, Currency1Id, Currency2Id, Currency1Rate.Value, Currency2Rate.Value)
        'If Not oErrMess Is Nothing Then
        '    Throw New ApplicationException(oErrMess)
        'End If

    End Sub
    Public Sub SP_Delete()
        Dim dal As New CurrencyConversionDAL
        Dim oErrMess As String
        oErrMess = dal.ExecuteSP(DealerId, EffectiveDate, ExpirationDate, Currency1Id, Currency2Id)
        'If Not oErrMess Is Nothing Then
        '    Throw New ApplicationException(oErrMess)
        'End If

    End Sub
    Public Sub Copy(original As CurrencyConversion)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Object")
        End If
        'Copy myself
        CopyFrom(original)

    End Sub

    Private Sub LoadLastRates(dealerId As Guid, currency1Id As Guid, currency2Id As Guid)
        Dim dal As New CurrencyConversionDAL
        dal.GetLastRate(Me.DealerId, Me.Currency1Id, Me.Currency2Id)
    End Sub

    'Public Sub DeleteAndSave()
    '    Me.CheckDeleted()
    '    Me.BeginEdit()
    '    Try
    '        Me.Delete()
    '        Me.Save()
    '    Catch ex As Exception
    '        Me.cancelEdit()
    '        Throw ex
    '    End Try
    'End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(dealerId As Guid, FromDate As String, ToDate As String) As CurrencyRateDV
        Dim dal As New CurrencyConversionDAL
        Dim ds As Dataset
        Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}

        'If dealerId.Equals(Guid.Empty) Then
        '    Throw New BOValidationException(errors, GetType(CurrencyConversion).FullName)
        'End If
        Return New CurrencyRateDV(dal.GetCurrecyRates(dealerId, FromDate, ToDate).Tables(0))

    End Function

    Public Shared Function FindMaxdate(dealerId As Guid, currency1Id As Guid, currency2Id As Guid) As CurrencyRateDV

        Try
            Dim dal As New CurrencyConversionDAL

            Return New CurrencyRateDV(dal.FindMaxDate(dealerId, currency1Id, currency2Id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadLastRate(dealerId As Guid, currency1Id As Guid, currency2Id As Guid) As Guid

        Try
            Dim dal As New CurrencyConversionDAL

            Dim DV As CurrencyConversion.CurrencyRateDV = New CurrencyRateDV(dal.GetLastRate(dealerId, currency1Id, currency2Id).Tables(0))

            Dim DVRow As DataRow = DV.Table.Rows(0)
            Dim oID As Guid = New Guid(CType(DVRow(DV.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))
            Return oID

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region
#Region "CurrencyRateDV"

    Public Class CurrencyRateDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "DEALER_NAME"
        Public Const COL_EFFECTIVE As String = "effective_date"
        Public Const COL_NAME_CURRENCY_CONVERSION_ID = "currency_conversion_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
       Public NotInheritable Class ValidEffectiveAndExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CurrencyConversion = CType(objectToValidate, CurrencyConversion)
            'If obj.DealerId.Equals(Guid.Empty) Then Return True
            If obj.EffectiveDate Is Nothing OrElse obj.ExpirationDate Is Nothing Then Return True

            If obj.EffectiveDate.Value > obj.ExpirationDate.Value Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
   Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_EFFECTIVE_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CurrencyConversion = CType(objectToValidate, CurrencyConversion)
            'If obj.DealerId.Equals(Guid.Empty) Then Return True
            If obj.EffectiveDate Is Nothing Then Return True

            'REQ-5393 - Allow enter exchange rate of any future date
            'If obj.EffectiveDate.Value > Now.AddDays(5) Then
            'Me.Message = Common.ErrorCodes.GUI_EFFECTIVE_DATE_CANNOT_BE_MORE_THAN_5_DAYS_FROM_TODAY '"Effective date cannot be more than 5 days from today."
            'Return False
            'End If

            Dim Diff As Integer = CType(DateDiff("d", obj.EffectiveDate.Value, DateAdd("d", 1, obj.ExpirationDate.Value)), Integer)

            If Diff > 31 Then
                Message = Common.ErrorCodes.GUI_CANNOT_ADD_MORE_THAN_31_ENTRIES_AT_A_TIME '"Cannot add more than 31 entries at a time."
                Return False
            End If

            Dim DV As CurrencyConversion.CurrencyRateDV = FindMaxdate(obj.DealerId, obj.Currency1Id, obj.Currency2Id)
            Dim DVRow As DataRow = DV.Table.Rows(0)

            If DVRow(DV.COL_EFFECTIVE) Is DBNull.Value Or DVRow(DV.COL_EFFECTIVE) Is Nothing Then
                Return True
            ElseIf CType(DVRow(DV.COL_EFFECTIVE), Date) <> DateAdd("d", -1, obj.EffectiveDate.Value) Then
                Message = Common.ErrorCodes.GUI_EFFECTIVE_DATE_MUST_BE_1_DAY_HIGHER_THAN_PREVIOUS_EFFECTIVE_DATE '" Effective date must be 1 day higher than previous effective date."
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
   Public NotInheritable Class ValidCurrencies
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CURRENCY_COMBINATION)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CurrencyConversion = CType(objectToValidate, CurrencyConversion)

            If obj.Currency1Id.Equals(obj.Currency2Id) Then
                Return False
            End If

            Return True

        End Function
    End Class

#End Region

End Class



