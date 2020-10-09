Imports System.Text.RegularExpressions

Public Class GetDealers
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COMPANY_CODE As String = "company_code"
    Private Const TABLE_NAME As String = "GetDealers"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_REGION_ID = "region_id"

    'error msg
    Private Const DEALER_NOT_FOUND As String = "NO_DEALER_FOUND"
#End Region

#Region "Constructors"

    Public Sub New(ds As GetDealersDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ds As GetDealersDs)

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

    Private Sub Load(ds As GetDealersDs)
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
            Throw New ElitaPlusException("WSUtilities GetDealers Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetDealersDs)
        Try
            If ds.GetDealers.Count = 0 Then Exit Sub
            With ds.GetDealers.Item(0)
                If Not .Iscompany_codeNull Then CompanyCode = .company_code
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

    Public Property CompanyCode As String
        Get
            If Row(DATA_COL_NAME_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COMPANY_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COMPANY_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim dealerBO As New Dealer
        Dim dealerList As Dealer.DealerSearchDV

        Try
            Validate()

            dealerList = dealerBO.getList(Nothing, Nothing, Guid.Empty, Guid.Empty)

            If dealerList IsNot Nothing AndAlso dealerList.Count > 0 Then
                If CompanyCode IsNot Nothing AndAlso Not CompanyCode.Equals(String.Empty) Then
                    dealerList.RowFilter = Dealer.DealerSearchDV.COL_COMPANY & " = '" & CompanyCode & "'"
                    'Sort on Description by default
                    If dealerList.Table.Columns.IndexOf(LookupListNew.COL_DESCRIPTION_NAME) >= 0 Then
                        dealerList.Sort = LookupListNew.COL_DESCRIPTION_NAME
                    End If
                    If dealerList.Count <= 0 Then
                        Throw New BOValidationException("GetDealers Error: ", DEALER_NOT_FOUND)
                    End If
                End If
            Else
                Throw New BOValidationException("GetDealers Error: ", DEALER_NOT_FOUND)
            End If

            Dim ds As New DataSet
            ds.Tables.Add(dealerList.ToTable)
            ds.Tables(0).Columns.Remove(Dealer.DealerSearchDV.COL_DEALER_ID)
            ds.Tables(0).Columns.Remove(Dealer.DealerSearchDV.COL_COMPANY)
            ds.Tables(0).Columns.Remove(Dealer.DealerSearchDV.COL_ACTIVE_FLAG)

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
