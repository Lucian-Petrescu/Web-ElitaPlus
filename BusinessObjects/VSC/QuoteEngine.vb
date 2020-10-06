Imports System.Text.RegularExpressions

Public Class QuoteEngine
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DATA_COL_NAME_MANUFACTURER As String = "MANUFACTURER"
    Public Const DATA_COL_NAME_MODEL As String = "MODEL"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_VIN As String = "VIN"
    Public Const DATA_COL_NAME_VERSION As String = "ENGINE_VERSION"
    Public Const DATA_COL_NAME_YEAR As String = "YEAR"
    Public Const DATA_COL_NAME_ODOMETER As String = "ODOMETER"
    Public Const DATA_COL_NAME_CONDITION As String = "CONDITION"
    Public Const DATA_COL_NAME_IN_SERVICE_DATE As String = "IN_SERVICE_DATE"
    Public Const DATA_COL_NAME_TAG As String = "VEHICLE_LICENSE_TAG"
    Public Const DATA_COL_WARRANTY_DATE As String = "WARRANTY_DATE"
    Public Const DATA_COL_EXTERNAL_CAR_CODE As String = "EXTERNAL_CAR_CODE"
    Public Const DATA_COL_VEHICLE_VALUE As String = "VEHICLE_VALUE"
    Private Const TABLE_NAME As String = "VSCQuote"
    Private Const TABLE_NAME_OPTIONS As String = "Optional"
    Private Const SOURCE_COL_MAKE As String = "Make"
    Private Const SOURCE_COL_MILEAGE As String = "Mileage"
    Private Const SOURCE_COL_NEWUSED As String = "New_Used"

    Private Const END_OF_LINE As String = "^"
    Private Const END_OF_FIELD As String = "|"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As VSCQuoteDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dt As DataTable
    Private _modelObject As VSCModel
    Private _DealerObject As Dealer
    Private _ContractObject As Contract
    Private _dealerPlansIDs As ArrayList
    Private _coveragesIDs As ArrayList

    Private Sub MapDataSet(ByVal ds As VSCQuoteDs)

        Dim schema As String = ds.GetXmlSchema.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New Dataset
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As VSCQuoteDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Quote Engine Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As VSCQuoteDs)
        Try
            If ds.VSCQuote.Count = 0 Then Exit Sub
            With ds.VSCQuote.Item(0)

                If Not .IsMakeNull Then Manufacturer = .Make
                If Not .IsModelNull Then Model = .Model
                If Not .IsVINNull Then VIN = .VIN
                If Not .IsEngine_VersionNull Then EngineVersion = .Engine_Version
                Year = Convert.ToInt32(.Year)
                Odometer = Convert.ToInt32(.Mileage)
                Condition = .New_Used
                InServiceDate = .In_Service_Date
                DealerCode = .Dealer_Code
                WarrantyDate = .Warranty_Date
                If Not .IsVehicle_License_TagNull Then VehicleLicenseTag = .Vehicle_License_Tag
                If Not .IsExternal_Car_CodeNull Then ExternalCarCode = .External_Car_Code
                If Not .IsVehicle_ValueNull Then VehicleValue = .Vehicle_Value

                If Not .IsMakeNull AndAlso Not .IsModelNull AndAlso Not .IsEngine_VersionNull AndAlso Not .IsExternal_Car_CodeNull Then
                    Throw New BOValidationException("Quote Engine Error: ", Common.ErrorCodes.INVALID_VSC_QUOTE_INPUT_PARAMETERS)
                End If

            End With

            ' Populate Options
            Dim i As Integer
            For i = 0 To ds._Optional.Count - 1
                NewOptional(ds._Optional(i).Optional_Code)
            Next

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("VSC Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Private Sub NewOptional(ByVal code As String)

        Dim newRow As DataRow = Dataset.Tables(TABLE_NAME_OPTIONS).NewRow
        newRow(0) = code
        Dataset.Tables(TABLE_NAME_OPTIONS).Rows.Add(newRow)

    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub BuildRelatedTables(ByVal ds As Dataset)
        Dim dr As DataRow
        ds.Tables(1).Columns.Add(New DataColumn("Quote_Item_id_String"))

        For Each dr In ds.Tables(1).Rows
            dr("Quote_Item_id_String") = New Guid(CType(dr("QUOTE_ITEM_ID"), Byte())).ToString
        Next

        Dim t As DataTable = ds.Tables(1).Clone
        Dim arrRows() As DataRow
        arrRows = ds.Tables(1).Select("QUOTE_ITEM_PARENT_ID is not null")
        t.TableName = "Covered_Optional_Coverages"
        ds.Tables.Add(t)

        Dim arr1(0) As DataColumn
        arr1(0) = t.Columns("QUOTE_ITEM_NUMBER")
        t.PrimaryKey = arr1

        Dim arr2(0) As DataColumn
        arr2(0) = ds.Tables(1).Columns("Quote_Item_id_String")
        ds.Tables(1).PrimaryKey = arr2

        For Each dr In arrRows
            t.ImportRow(dr)
            ds.Tables(1).Rows.Remove(dr)
        Next

        For Each dr In t.Rows
            dr("Quote_Item_id_String") = New Guid(CType(dr("QUOTE_ITEM_PARENT_ID"), Byte())).ToString
        Next

        t.Columns.Remove("QUOTE_ITEM_ID")
        Dim rela As New DataRelation("Plan_Optional", ds.Tables(1).Columns("Quote_Item_id_String"), t.Columns("Quote_Item_id_String"))
        rela.Nested = True
        ds.Relations.Add(rela)

    End Sub


#End Region

#Region "Properties"

    Public Property Manufacturer() As String
        Get
            If Row(DATA_COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MANUFACTURER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_MANUFACTURER, Value)
        End Set
    End Property

    Public Property Model() As String
        Get
            If Row(DATA_COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_MODEL, Value)
        End Set
    End Property

    Public Property EngineVersion() As String
        Get
            If Row(DATA_COL_NAME_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_VERSION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_VERSION, Value)
        End Set
    End Property

    Public Property VIN() As String
        Get
            If Row(DATA_COL_NAME_VIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_VIN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_VIN, Value)
        End Set
    End Property

    Public Property Year() As Integer
        Get
            If Row(DATA_COL_NAME_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_YEAR), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            SetValue(DATA_COL_NAME_YEAR, Value)
        End Set
    End Property

    Public Property DealerCode() As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    Public Property WarrantyDate() As Date
        Get
            If Row(DATA_COL_WARRANTY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_WARRANTY_DATE), String)
            End If
        End Get
        Set(ByVal Value As Date)
            CheckDeleted()
            SetValue(DATA_COL_WARRANTY_DATE, Value)
        End Set
    End Property

    Public Property Odometer() As Integer
        Get
            If Row(DATA_COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_ODOMETER), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            SetValue(DATA_COL_NAME_ODOMETER, Value)
        End Set
    End Property

    Public Property Condition() As String
        Get
            If Row(DATA_COL_NAME_CONDITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CONDITION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CONDITION, Value)
        End Set
    End Property

    Public Property InServiceDate() As Date
        Get
            If Row(DATA_COL_NAME_IN_SERVICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_IN_SERVICE_DATE), Date)
            End If
        End Get
        Set(ByVal Value As Date)
            CheckDeleted()
            SetValue(DATA_COL_NAME_IN_SERVICE_DATE, Value)
        End Set
    End Property

    Public Property VehicleLicenseTag() As String
        Get
            If Row(DATA_COL_NAME_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_TAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_TAG, Value)
        End Set
    End Property

    Public Property ExternalCarCode() As String
        Get
            If Row(DATA_COL_EXTERNAL_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_EXTERNAL_CAR_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_EXTERNAL_CAR_CODE, Value)
        End Set
    End Property

    Public Property VehicleValue() As Decimal
        Get
            If Row(DATA_COL_VEHICLE_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_VEHICLE_VALUE), String)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            SetValue(DATA_COL_VEHICLE_VALUE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Function IsValidQuoteInput() As Boolean
        Try

            Return True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function GetModel() As Boolean
        Try

            Return True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Overrides Function ProcessWSRequest() As String
        Dim oRow As DataRow

        Dim oQuoteEngineData As New QuoteEngineData
        With oQuoteEngineData
            .CompanyGroupID = Authentication.CurrentUser.CompanyGroup.Id
            .UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            .Dealer = DealerCode
            .EngineVersion = EngineVersion
            .InServiceDate = InServiceDate

            If WarrantyDate > GetShortDate(Today) Then
                Throw New BOValidationException("Quote Engine Error: ", Common.ErrorCodes.INVALID_WARRANTY_SALES_DATE)
            Else
                .WarrantyDate = WarrantyDate
            End If

            .Manufacturer = Manufacturer
            .Model = Model
            .NewUsed = Condition
            .Odometer = Odometer
            .VehicleLicenseTag = VehicleLicenseTag
            .VIN = VIN
            .Year = Year
            .ExternalCarCode = ExternalCarCode
            .VehicleValue = VehicleValue
            .Options = String.Empty

            ' Add the options
            For Each oRow In Dataset.Tables(TABLE_NAME_OPTIONS).Rows
                If .Options.Trim.Length > 0 Then .Options &= END_OF_LINE
                .Options &= oRow(0)
            Next
        End With

        Try
            Validate()
            Dim dal As New VSCQuoteDAL
            Dim ds As Dataset
            ds = dal.GetQuote(oQuoteEngineData)

            '' Get Collection Methods List
            '' Dim paymentTypesDS As DataSet
            'PaymentType.getCollectionMethodsList(ds, oQuoteEngineData.CompanyGroupID, Authentication.CurrentUser.LanguageId)
            ''paymentTypesDS.DataSetName = "PAYMENT_TYPES"

            '' Get Payment Instruments List
            'PaymentType.getPaymentInstrumentsList(ds, oQuoteEngineData.CompanyGroupID, Authentication.CurrentUser.LanguageId)


            'ds.Tables(PaymentTypeDAL.TABLE_NAME_COLLECTIONS).Columns("collection_method_id").Unique = True
            'ds.Tables(PaymentTypeDAL.TABLE_NAME_COLLECTIONS).PrimaryKey = New DataColumn() {ds.Tables(PaymentTypeDAL.TABLE_NAME_COLLECTIONS).Columns("collection_method_id")}
            ''ds.Tables(PaymentTypeDAL.TABLE_NAME_PAYMENT_INSTRUMENTS).p = New DataColumn() {ds.Tables(PaymentTypeDAL.TABLE_NAME_COLLECTIONS).Columns("payment_type_id")}

            'Dim relationObj As New DataRelation("", ds.Tables(PaymentTypeDAL.TABLE_NAME_COLLECTIONS).Columns("collection_method_id"), ds.Tables(PaymentTypeDAL.TABLE_NAME_PAYMENT_INSTRUMENTS).Columns("collection_method_id"))
            'ds.Relations.Add(relationObj)

            'Get Payment Types List
            PaymentType.getPaymentTypesList(ds, oQuoteEngineData.CompanyGroupID, Authentication.CurrentUser.LanguageId)

            ' Get Credit Card Types available for the User's companies
            Dim CreditCardTypesDV As DataView = CompanyCreditCard.LoadList()
            Dim CreditCardTypesTable As DataTable = CreditCardTypesDV.Table.Copy
            CreditCardTypesTable.Columns.Remove("company_credit_card_id")
            CreditCardTypesTable.Columns.Remove("company_id")
            CreditCardTypesTable.Columns.Remove("company_code")
            CreditCardTypesTable.Columns.Remove("credit_card_format_id")
            CreditCardTypesTable.Columns.Remove("Billing_Date")

            CreditCardTypesTable.TableName = "CREDIT_CARD_TYPES"
            CreditCardTypesTable.Columns("Credit_Card_Type").ColumnName = "Description"
            ds.Tables.Add(CreditCardTypesTable)


            BuildRelatedTables(ds)
            Return RemoveExcessData(XMLHelper.FromDatasetToXML(ds, Nothing, True))

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Private Function RemoveExcessData(ByVal xmlStringOutput As String) As String
        xmlStringOutput = Regex.Replace(xmlStringOutput, "<QUOTE_ITEM_ID>[^>]+</QUOTE_ITEM_ID>|<QUOTE_ITEM_PARENT_ID>[^>]+</QUOTE_ITEM_PARENT_ID>|<Quote_Item_id_String>[^>]+</Quote_Item_id_String>|<QUOTE_ID>[^>]+</QUOTE_ID>", String.Empty)
        Return xmlStringOutput
    End Function

    'Public Function GetQuote(ByVal oQuoteEngineData As QuoteEngineData) As Dataset

    '    'ds = Me.Dataset.Clone

    '    'Dim i As Integer
    '    'For i = ds.Tables(0).Columns.Count - 1 To 3 Step -1

    '    '    ds.Tables(0).Columns.RemoveAt(i)

    '    'Next

    '    'Dim dr As DataRow = ds.Tables(0).NewRow

    '    'For i = 0 To 2

    '    '    dr(i) = Me.Row(i)

    '    'Next

    '    'ds.Tables(0).Rows.Add(dr)

    '    'Catch ex1 As BOValidationException
    '    '    Dim x As Integer = ex1.ValidationErrorList.Length()

    '    '    Return Nothing

    '    'End Try

    '    'Return ds

    'End Function
#End Region

#Region "Extended Properties"

    Public ReadOnly Property ModelObject() As VSCModel
        Get
            If _modelObject Is Nothing Then
                If Not Manufacturer Is Nothing AndAlso Not Model Is Nothing AndAlso Not Year = 0 Then
                    _modelObject = New VSCModel(Manufacturer, Model, Year)
                End If
            End If
            Return _modelObject
        End Get
    End Property

    Public ReadOnly Property DealerObject() As Dealer
        Get
            If _DealerObject Is Nothing Then
                If Not DealerCode Is Nothing Then
                    _DealerObject = New Dealer(Authentication.CurrentUser.CompanyId, DealerCode)
                End If
            End If
            Return _DealerObject
        End Get
    End Property

    Public ReadOnly Property DealerContract() As Contract
        Get
            If _ContractObject Is Nothing Then
                _ContractObject = Contract.GetContract(DealerObject.Id, Today)
                'Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.DEALER_DOES_NOT_HAVE_CURRENT_CONTRACT, GetType(QuoteEngine), Nothing, Nothing, Nothing)}
                'Throw New BOValidationException(errors, GetType(QuoteEngine).FullName)
            End If
            Return _ContractObject
        End Get
    End Property

    Public ReadOnly Property DealerPlansIDs() As ArrayList
        Get
            If _dealerPlansIDs Is Nothing Then
                If Not DealerCode Is Nothing AndAlso Not DealerObject Is Nothing Then
                    _dealerPlansIDs = ProductCode.GetProductCodeIDs(DealerObject.Id)
                End If
            End If
            Return _dealerPlansIDs
        End Get
    End Property

    Public ReadOnly Property CoveragesIDs() As ArrayList
        Get
            If _coveragesIDs Is Nothing Then
                If Not DealerCode Is Nothing AndAlso Not DealerObject Is Nothing Then
                    _dealerPlansIDs = ProductCode.GetProductCodeIDs(DealerObject.Id)
                End If
            End If
            Return _dealerPlansIDs
        End Get
    End Property
#End Region


End Class
