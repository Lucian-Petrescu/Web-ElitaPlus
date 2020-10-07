Imports System.Text.RegularExpressions

Public Class GetExpectedGWP
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATE_COL_NAME_PRODUCT_CODE_ID As String = "Product_Code_Id"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_Code"
    Public Const DATA_COL_NAME_PRODUCT_CODE As String = "product_Code"
    Public Const DATA_COL_NAME_CERT_DURATION As String = "cert_Duration"
    Public Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const DATA_COL_NAME_PURCHASE_PRICE As String = "purchase_price"
    Public Const DATA_COL_NAME_MANUFACTURER_DURATION As String = "manufacturer_duration"
    Public Const DATA_COL_NAME_PRODUCT_PURCHASE_DATE As String = "product_purchase_date"
    Private Const TABLE_NAME As String = "GetExpectedGWP"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"

    'error msg
    Private Const DEALER_NOT_FOUND As String = "NO_DEALER_FOUND"
    Private dealerId As Guid = Guid.Empty
    'Private ProductId As Guid = Guid.Empty
    Public Const MIN_DURATION As Integer = 0
    Public Const MAX_DURATION As Integer = 999
    Private dvProductCodeID As DataView
    Private ProductCodeID As Guid = Guid.Empty
    Private decExpectedGWP As DecimalType
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetExpectedGWPDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetExpectedGWPDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GetExpectedGWPDs)
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
            Throw New ElitaPlusException("WSUtilities GetExpectedGWP Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetExpectedGWPDs)
        Try
            If ds.GetExpectedGWP.Count = 0 Then Exit Sub
            With ds.GetExpectedGWP.Item(0)
                DealerCode = .dealer_code
                ProductCode = .product_code
                CertificateDuration = .cert_duration
                WarrantySalesDate = .warranty_sales_date
                PurchasePrice = .purchase_price
                If Not .Ismanufacturer_durationNull Then
                    CoverageDuration = .manufacturer_duration
                Else
                    CoverageDuration = -1
                End If
                
                If Not .Isproduct_purchase_dateNull Then
                    ProductPurchaseDate = .product_purchase_date
                Else
                    ProductPurchaseDate = Date.MinValue
                End If
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("WSUtilities Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ProductCode As String
        Get
            If Row(DATA_COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PRODUCT_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertificateDuration As Integer
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_CERT_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERT_DURATION), Integer)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_DURATION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property WarrantySalesDate As DateTime
        Get
            If Row(DATA_COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_WARRANTY_SALES_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    Public Property PurchasePrice As Double
        Get
            If Row(DATA_COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_PURCHASE_PRICE), Double))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property


    Public Property CoverageDuration As Integer
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_MANUFACTURER_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MANUFACTURER_DURATION), Integer)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MANUFACTURER_DURATION, Value)
        End Set
    End Property

    Public Property ProductPurchaseDate As DateTime
        Get
            If Row(DATA_COL_NAME_PRODUCT_PURCHASE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_PRODUCT_PURCHASE_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_PRODUCT_PURCHASE_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim dealerBO As New Dealer
        Dim expectedGWPList As DataView
        Dim productBO As New ProductCode

        Try
            Validate()

            Dim dvDealrs As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If Not dvDealrs Is Nothing AndAlso dvDealrs.Count > 0 Then
                dealerId = LookupListNew.GetIdFromCode(dvDealrs, DealerCode)
                If dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
                End If
            End If


            dvProductCodeID = productBO.GetProductCodeId(dealerId, ProductCode)

            If Not dvProductCodeID Is Nothing AndAlso dvProductCodeID.Count > 0 Then
                If Not dvProductCodeID.Item(0)(DATE_COL_NAME_PRODUCT_CODE_ID).Equals(Guid.Empty) Then
                    ProductCodeID = GuidControl.ByteArrayToGuid(dvProductCodeID.Item(0)(DATE_COL_NAME_PRODUCT_CODE_ID))
                Else
                    Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PRODUCT_CODE)
                End If
            Else
                Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PRODUCT_CODE)
            End If

            If Len((CertificateDuration.ToString)) > 3 Or CertificateDuration = MIN_DURATION Or CertificateDuration > MAX_DURATION Then
                Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CERTIFICATE_DURATION)
            End If


            Dim ds As New DataSet("GetExpectedGWP")
            Dim objExpectedGWPTable As DataTable = New DataTable("GetExpectedGWP")
            ' Define the columns of the table.
            objExpectedGWPTable.Columns.Add(New DataColumn("ExpectedGWP", GetType(Double)))
            Dim row As DataRow = objExpectedGWPTable.NewRow

            If PercentageOfRetail(ProductCodeID).Value > 0 Then
                row("ExpectedGWP") = decExpectedGWP.Value
            Else
                'Me.ValidateDealerFlags() ' this validation has been moved to the DB "ELP_EXPECTED_PREMIUM.GetExpectedGWP"
                Dim expcetedGWP As Object = CoverageRate.GetExpectedGWP(dealerId, ProductCode, CertificateDuration, WarrantySalesDate, PurchasePrice, _
                                                                 CoverageDuration, ProductPurchaseDate)

                If Not expcetedGWP Is Nothing Then
                    row("ExpectedGWP") = Convert.ToDouble(expcetedGWP)
                Else
                    row("ExpectedGWP") = System.DBNull.Value
                End If
            End If

            objExpectedGWPTable.Rows.Add(row)
            ds.Tables.Add(objExpectedGWPTable)

            Return XMLHelper.FromDatasetToXML_Coded(ds)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Private Function PercentageOfRetail(ByVal oProductCodeID As Guid) As DecimalType

        Dim percentOfRetail As DecimalType

        Try
            Dim oPercentOfRetailDataview As DataView = LookupListNew.GetPercentOfRetailLookup(oProductCodeID)
            If oPercentOfRetailDataview.Count > 0 Then
                If oPercentOfRetailDataview.Item(0).Item("CODE") Is System.DBNull.Value Then
                    percentOfRetail = New DecimalType(0)
                Else
                    percentOfRetail = New DecimalType(CType(oPercentOfRetailDataview.Item(0).Item("CODE"), Decimal))
                End If
            End If
            decExpectedGWP = Math.Round((PurchasePrice * percentOfRetail.Value / 100), 2)

            Return decExpectedGWP
        Catch ex As Exception
            Throw New StoredProcedureGeneratedException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_PERCENT_OF_RETAIL_COMPUTE_ERROR)
        End Try

    End Function
    'Private Sub ValidateDealerFlags()

    '    'ITEMS 1, 2, 3, 4, 5 are now supported by requirment REQ-101 in the database : "ELP_EXPECTED_PREMIUM.GetExpectedGWP".

    '    Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)

    '    '1- Dealer-contract is flagged to ignore the definitions.
    '    Dim oContract As Contract
    '    oContract = Contract.GetContract(Me.dealerId, Me.WarrantySalesDate)
    '    If Not oContract Is Nothing Then
    '        If oContract.IgnoreCoverageAmtId.Equals(yesId) Then
    '            Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_DEALER_IS_FLAGGED_TO_IGNORE_DEFINITIONS_ERROR)
    '        End If
    '    Else
    '        Throw New DataNotFoundException(Common.ErrorCodes.NO_CONTRACT_FOUND)
    '    End If

    '    '2- Dealer is flagged to use delay.  To do so we would also need the product purchase date, in addition to the warranty sales date.
    '    Dim objDealer As New Dealer(Me.dealerId)
    '    If Not objDealer Is Nothing Then
    '        If objDealer.DelayFactorFlagId.Equals(yesId) Then
    '            Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_DEALER_IS_FLAGGED_TO_USE_DELAY_FACTOR_ERROR)
    '        End If
    '    Else
    '        Throw New DataNotFoundException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
    '    End If

    '    '3- Dealer-contract is flagged to locate the appropriate entries based on the extended warranty coverage.  To do so we would also need to take as input the extended coverage duration.
    '    'Fixed ESC duration flag
    '    If oContract.FixedEscDurationFlag.Equals(yesId) Then
    '        Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_DEALER_IS_FLAGGED_FOR_FIXED_ESC_DURATION_ERROR)
    '    End If

    '    '4- Dealer is flagged to allow installments.  To do so we would also need the number of installments.    This will be supported by us by end of June.
    '    If objDealer.InstallmentFactorFlagId.Equals(yesId) Then
    '        Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_DEALER_IS_FLAGGED_FOR_INSTALLMENTS_FACTOR_ERROR)
    '    End If

    '    '5- Dealer-contract is flagged for monthly billing.
    '    'Fixed ESC duration flag
    '    If oContract.MonthlyBillingId.Equals(yesId) Then
    '        Throw New BOValidationException("GetExpectedGWP Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_DEALER_IS_FLAGGED_FOR_MONTHLY_BILLING_ERROR)
    '    End If

    'End Sub
#End Region

End Class
