Imports System.Text.RegularExpressions

Public Class GetProductCodes
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_Code"
    Public Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "Warr_Sales_Date"
    Public Const DATA_COL_NAME_SORT_BY As String = "sort_by"
    Public Const DATA_COL_NAME_ASC_DESC_ORDER As String = "asc_desc_order"
    Public Const DATA_COL_NAME_PRODUCT_CLASS_CODE As String = "Product_Class_Code"
    Private Const TABLE_NAME As String = "GetProductCodes"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_REGION_ID = "region_id"



    'error msg
    Private Const DEALER_NOT_FOUND As String = "NO_DEALER_FOUND"
    Private dealerId As Guid = Guid.Empty
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetProductCodesDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetProductCodesDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GetProductCodesDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("WSUtilities GetProductCodes Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetProductCodesDs)
        Try
            If ds.GetProductCodes.Count = 0 Then Exit Sub
            With ds.GetProductCodes.Item(0)
                Me.DealerCode = .Dealer_Code
                Me.WarrantySalesDate = .Warr_Sales_Date
                Me.SortBy = .Sort_By
                Me.AscDescOrder = .Asc_Desc_Order
                If Not .IsProduct_Class_CodeNull Then Me.ProductClassCode = .Product_Class_Code
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

    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    Public Property WarrantySalesDate() As DateTime
        Get
            If Row(Me.DATA_COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_WARRANTY_SALES_DATE), String)
            End If
        End Get
        Set(ByVal Value As Date)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    Public Property SortBy() As String
        Get
            If Row(Me.DATA_COL_NAME_SORT_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_SORT_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SORT_BY, Value)
        End Set
    End Property

    Public Property AscDescOrder() As String
        Get
            If Row(Me.DATA_COL_NAME_ASC_DESC_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_ASC_DESC_ORDER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_ASC_DESC_ORDER, Value)
        End Set
    End Property

    Public Property ProductClassCode() As String
        Get
            If Row(Me.DATA_COL_NAME_PRODUCT_CLASS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_PRODUCT_CLASS_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_PRODUCT_CLASS_CODE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim dealerBO As New Dealer
        Dim productCodesList As ProductCode.ProductCodeSearchByDealerDVForWS

        Try
            Me.Validate()

            Dim dvDealrs As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If Not dvDealrs Is Nothing AndAlso dvDealrs.Count > 0 Then
                dealerId = LookupListNew.GetIdFromCode(dvDealrs, Me.DealerCode)
                If dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetProductCodes Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
                End If
            End If

            productCodesList = ProductCode.getListByDealerForWS(dealerId, Me.WarrantySalesDate, _
                                    Me.SortBy, Me.AscDescOrder, Me.ProductClassCode)

            If productCodesList Is Nothing OrElse productCodesList.Count <= 0 Then
                Throw New BOValidationException("GetProductCodes Error: ", Assurant.ElitaPlus.Common.ErrorCodes.NO_PRODUCTCODES_FOUND_ERR)
            End If
          
            Dim ds As New DataSet("GetProductCodes")
            ds.Tables.Add(productCodesList.ToTable)
            
            'Return XMLHelper.FromDatasetToXML_Coded(ds)
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
