Imports System.Text.RegularExpressions

Public Class GetCountriesRegions
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COUNTRY_CODE As String = "country_code"
    Private Const TABLE_NAME As String = "GetCountriesRegions"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_REGION_ID = "region_id"
    Private Const DATASET_NAME As String = "GetCountriesRegions"

    'error msg
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const COUNTRY_NOT_FOUND As String = "ERR_COUNTRY_NOT_FOUND"
    Private Const REGION_NOT_FOUND As String = "ERR_REGION_NOT_FOUND"
    Private Const COUNTRY_NOT_ASSIGNED_TO_THIS_USER As String = "ERR_COUNTRY_NOT_ASSIGNED_TO_THIS_USER"

    Private _countryCodesList As ArrayList
    Private _userCountriesIDs As ArrayList

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetCountriesRegionsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetCountriesRegionsDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Sub Load(ByVal ds As GetCountriesRegionsDs)
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
            Throw New ElitaPlusException("GetCountriesRegions Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetCountriesRegionsDs)
        Try
            If ds.GetCountriesRegions.Count = 0 Then Exit Sub
            With ds.GetCountriesRegions.Item(0)
                If ds.country_codes_list.Count = 0 Then Exit Sub
                Dim i As Integer
                For i = 0 To ds.country_codes_list.Count - 1
                    CountryCodesList.Add(ds.country_codes_list(i).country_code.ToString)
                Next

            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCountriesRegions Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property CountryCodesList() As ArrayList
        Get
            If Not _countryCodesList Is Nothing Then
                Return _countryCodesList
            Else
                _countryCodesList = New ArrayList
                Return _countryCodesList
            End If
        End Get
        Set(ByVal Value As ArrayList)
            _countryCodesList = Value
        End Set
    End Property

    Public Property UserCountriesIDs() As ArrayList
        Get
            If Not _userCountriesIDs Is Nothing Then
                Return _userCountriesIDs
            Else
                _userCountriesIDs = New ArrayList
                Return _userCountriesIDs
            End If
        End Get
        Set(ByVal Value As ArrayList)
            _userCountriesIDs = Value
        End Set
    End Property

    Private Sub BuildCountriesIDs()
        'if the country code(s) were not provided, get it from the user object.
        'if the country code(s) were provided, validate them to be within the countries of the user's companies.
        Dim userCountriesDv As DataView = User.GetUserCountries(ElitaPlusIdentity.Current.ActiveUser.Id)

        If Not Me.CountryCodesList Is Nothing AndAlso Me.CountryCodesList.Count > 0 Then

            Dim i, index, intCodesNotFound As Integer
            Dim blnInTheList As Boolean = False
            For i = 0 To CountryCodesList.Count - 1
                blnInTheList = False
                For index = 0 To userCountriesDv.Table.Rows.Count - 1
                    If Not userCountriesDv.Table.Rows(index)("code") Is System.DBNull.Value Then
                        If CountryCodesList.Item(i).ToString.ToUpper.Equals(CType(userCountriesDv.Table.Rows(index)("code"), String).ToUpper) Then
                            blnInTheList = True
                            UserCountriesIDs.Add(New Guid(CType(userCountriesDv.Table.Rows(index)("COUNTRY_ID"), Byte())))
                            Exit For
                        End If
                    End If
                Next
                If Not blnInTheList Then
                    Throw New BOValidationException("GetCountriesRegions Error: ", COUNTRY_NOT_ASSIGNED_TO_THIS_USER)
                End If
            Next
        Else
            UserCountriesIDs = ElitaPlusIdentity.Current.ActiveUser.Countries
        End If

    End Sub
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String

        Try
            BuildCountriesIDs()
            If Me.UserCountriesIDs Is Nothing OrElse UserCountriesIDs.Count = 0 Then
                Throw New BOValidationException("GetCountriesRegions Error: ", COUNTRY_NOT_FOUND)
            Else

                Dim i As Integer
                Dim objCountriesRegionsDS As DataSet = Country.GetCountriesRegionsForWS(UserCountriesIDs)

                'For i = 0 To UserCountriesIDs.Count - 1
                '    Dim country_id As Guid = CType(UserCountriesIDs.Item(i), Guid)
                '    Dim objCountry As New Country(country_id)
                '    Dim objRegionsDS As DataSet = Region.LoadList(country_id)
                '    objRegionsDS.Tables(0).Columns.Remove(Me.COL_NAME_REGION_ID)
                '    objRegionsDS.Tables(0).TableName = objCountry.Code
                '    objCountriesRegionsDS.Tables.Add(objRegionsDS.Tables(0).Copy)
                'Next

                If objCountriesRegionsDS Is Nothing OrElse objCountriesRegionsDS.Tables.Count = 0 Then
                    Throw New BOValidationException("GetCountriesRegions Error: ", ERROR_ACCESSING_DATABASE)
                ElseIf objCountriesRegionsDS.Tables.Count > 0 Then

                    objCountriesRegionsDS.DataSetName = Me.DATASET_NAME

                    Dim excludeTags As ArrayList = New ArrayList()
                    excludeTags.Add("/GetCountriesRegions/COUNTRY/COUNTRY_ID")
                    excludeTags.Add("/GetCountriesRegions/COUNTRY/REGION/COUNTRY_ID")
                    Return XMLHelper.FromDatasetToXML(objCountriesRegionsDS, excludeTags, True)


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
