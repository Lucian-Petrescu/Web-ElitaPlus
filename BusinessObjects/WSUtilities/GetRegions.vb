Imports System.Text.RegularExpressions

Public Class GetRegions
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COUNTRY_CODE As String = "country_code"
    Private Const TABLE_NAME As String = "GetRegions"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_REGION_ID = "region_id"

    'error msg
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const COUNTRY_NOT_FOUND As String = "ERR_COUNTRY_NOT_FOUND"
    Private Const REGION_NOT_FOUND As String = "ERR_REGION_NOT_FOUND"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetRegionsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetRegionsDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Sub Load(ByVal ds As GetRegionsDs)
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
            Throw New ElitaPlusException("GetRegions Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetRegionsDs)
        Try
            If ds.GetRegions.Count = 0 Then Exit Sub
            With ds.GetRegions.Item(0)
                If Not .Iscountry_codeNull Then CountryCode = .country_code
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetRegions Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property CountryCode As String
        Get
            If Row(DATA_COL_NAME_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COUNTRY_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COUNTRY_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        'if the country code was not provided, get it from the user object.
        If CountryCode Is Nothing OrElse CountryCode.Equals(String.Empty) Then
            Dim objCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            CountryCode = objCountry.Code
        End If
        Dim objCountryDV As DataView = Country.getList("", CountryCode)
        Try
            Validate()

            If objCountryDV Is Nothing Then
                Throw New BOValidationException("GetRegions Error: ", ERROR_ACCESSING_DATABASE)
            ElseIf objCountryDV.Count <> 1 Then
                Throw New BOValidationException("GetRegions Error: ", COUNTRY_NOT_FOUND)
            Else
                Dim country_id As New Guid(CType(objCountryDV.Table.Rows(0).Item(COL_NAME_COUNTRY_ID), Byte()))
                Dim objregionDS As DataSet = Region.LoadList(country_id)
                If objregionDS Is Nothing Then
                    Throw New BOValidationException("GetRegions Error: ", ERROR_ACCESSING_DATABASE)
                ElseIf objregionDS.Tables.Count > 0 AndAlso objregionDS.Tables(0).Rows.Count > 0 Then
                    objregionDS.Tables(0).Columns.Remove(COL_NAME_REGION_ID)
                    Return (XMLHelper.FromDatasetToXML(objregionDS))
                ElseIf objregionDS.Tables.Count > 0 AndAlso objregionDS.Tables(0).Rows.Count = 0 Then
                    Throw New BOValidationException("GetRegions Error: ", REGION_NOT_FOUND)
                End If
            End If

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

#Region "Extended Properties"

#End Region

End Class
