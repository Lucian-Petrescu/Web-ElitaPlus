Imports System.Text.RegularExpressions

Public Class GetDealerProductsInfo
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_Code"
    Private Const TABLE_NAME As String = "GetDealerProductsInfo"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_REGION_ID = "region_id"



    'error msg
    Private Const DEALER_NOT_FOUND As String = "NO_DEALER_FOUND"
    Private dealerId As Guid = Guid.Empty
#End Region

#Region "Constructors"

    Public Sub New(ds As GetDealerProductsInfoDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    'Private Sub BuildRelatedTables(ByVal ds As DataSet)


    '    'create the product-item relation
    '    Dim ProductsToItemsRel As New DataRelation("PRODUCTS_ITEMS_RELATION", _
    '                                                     ds.Tables("PRODUCT_CODES").Columns("PRODUCT_CODE_ID"), _
    '                                                     ds.Tables("ITEMS").Columns("ITEM_ID"))
    '    ProductsToItemsRel.Nested = True
    '    ds.Relations.Add(ProductsToItemsRel)

    '    'create the item-Coverage relation
    '    Dim ItemsToCoveragesRel As New DataRelation("ITEMS_COVERAGES_RELATION", _
    '                                                     ds.Tables("ITEMS").Columns("ITEM_ID"), _
    '                                                     ds.Tables("COVERAGES").Columns("COVERAGE_ID"))
    '    ItemsToCoveragesRel.Nested = True
    '    ds.Relations.Add(ItemsToCoveragesRel)

    '    'create the Coverage-coverageRate relation
    '    Dim CoveragesToCoverageRatesRel As New DataRelation("COVERAGES_COVERGAE_RATES_RELATION", _
    '                                                     ds.Tables("COVERAGES").Columns("COVERAGE_ID"), _
    '                                                     ds.Tables("COVERAGE_RATES").Columns("COVERAGE_RATE_ID"))
    '    CoveragesToCoverageRatesRel.Nested = True
    '    ds.Relations.Add(CoveragesToCoverageRatesRel)

    'End Sub

    Private Function RemoveExcessData(xmlStringOutput As String) As String
        xmlStringOutput = Regex.Replace(xmlStringOutput, "<PRODUCT_CODE_ID>[^>]+</PRODUCT_CODE_ID>|<ITEM_ID>[^>]+</ITEM_ID>|<COVERAGE_ID>[^>]+</COVERAGE_ID>|<COVERAGE_RATE_ID>[^>]+</COVERAGE_RATE_ID>", String.Empty)
        Return xmlStringOutput
    End Function

    Private Sub MapDataSet(ds As GetDealerProductsInfoDs)

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

    Private Sub Load(ds As GetDealerProductsInfoDs)
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
            Throw New ElitaPlusException("WSUtilities GetDealerProductsInfo Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetDealerProductsInfoDs)
        Try
            If ds.GetDealerProductsInfo.Count = 0 Then Exit Sub
            With ds.GetDealerProductsInfo.Item(0)
                DealerCode = .Dealer_Code
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


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim dealerBO As New Dealer

        Try
            Validate()

            Dim dvDealrs As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If Not dvDealrs Is Nothing AndAlso dvDealrs.Count > 0 Then
                dealerId = LookupListNew.GetIdFromCode(dvDealrs, DealerCode)
                If dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetDealerProductsInfo Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
                End If
            End If

            Dim ds As New DataSet("DEALER_PRODUCTS_INFO")

            'Get Products
            ds = ProductCode.getDealerProductsInfo(ds, dealerId)
            If ds.Tables.Count <= 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("GetDealerProductsInfo Error: ", Assurant.ElitaPlus.Common.ErrorCodes.NO_PRODUCTCODES_FOUND_ERR)
            End If

            ''get Items :table count should be 2
            'ds = Item.getDealerItemsInfo(ds, dealerId)
            'If ds.Tables.Count <= 1 OrElse ds.Tables(1).Rows.Count = 0 Then
            '    Throw New BOValidationException("GetDealerProductsInfo Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_NO_ITEMS_FOUND_ERR)
            'End If

            ''get Coverages :table count should be 3
            'ds = Coverage.getDealerCoveragesInfo(ds, dealerId, Me.WarrantySalesDate)
            'If ds.Tables.Count <= 2 OrElse ds.Tables(2).Rows.Count = 0 Then
            '    Throw New BOValidationException("GetDealerProductsInfo Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_NO_COVERAGES_FOUND_ERR)
            'End If

            ''get Coverage rates :table count should be 4
            'ds = CoverageRate.getDealerCoverageRatesInfo(ds, dealerId, Me.WarrantySalesDate)
            'If ds.Tables.Count <= 3 OrElse ds.Tables(3).Rows.Count = 0 Then
            '    Throw New BOValidationException("GetDealerProductsInfo Error: ", Assurant.ElitaPlus.Common.ErrorCodes.WS_NO_COVERAGE_RATES_FOUND_ERR)
            'End If


            'BuildRelatedTables(ds)
            Return XMLHelper.FromDatasetToXML(ds, Nothing, True)

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

#End Region

End Class
