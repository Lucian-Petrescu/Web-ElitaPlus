Imports System.Text.RegularExpressions

Public Class GetServiceCenter
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COUNTRY_CODE As String = "country_code"
    Public Const DATA_COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Private Const TABLE_NAME As String = "GetServiceCenter"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetServiceCenterDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private _country_id As Guid = Guid.Empty
    Private Sub MapDataSet(ByVal ds As GetServiceCenterDs)

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

    Private Sub Load(ByVal ds As GetServiceCenterDs)
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
            Throw New ElitaPlusException("WSUtilities GetServiceCenter Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetServiceCenterDs)
        Try
            If ds.GetServiceCenter.Count = 0 Then Exit Sub
            With ds.GetServiceCenter.Item(0)

                Me.ServiceCenterCode = .Service_Center_Code

                Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Me._country_id = oCompany.CountryId


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
     Public Property ServiceCenterCode() As String
        Get
            If Row(Me.DATA_COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_SERVICE_CENTER_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Private ReadOnly Property CountryId() As Guid
        Get
            Return _country_id
        End Get
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        'Dim manufacturerBO As New Manufacturer
        Dim PartDiscriptionList As DataTable
        Dim ds As DataSet
        Try
            Me.Validate()
            ds = ServiceCenter.GetServiceCenterForWS(Me.ServiceCenterCode, Me.CountryId)
            If ds Is Nothing OrElse ds.Tables.Count <= 0 OrElse ds.Tables(0).Rows.Count <= 0 Then
                '  Throw New BOValidationException("GetServiceCenter Error: ", Common.ErrorCodes.BO_DATA_NOT_FOUND)
            End If
            ds.Tables(0).Columns.Remove("id")           

            Return XMLHelper.FromDatasetToXML(ds, Nothing, True, True, True, False, True)

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
