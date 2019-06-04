Imports System.Text.RegularExpressions

Public Class GetManufacturers
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_company_group_code As String = "company_group_code"
    Private Const TABLE_NAME As String = "GetManufacturers"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetManufacturersDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private _companyGroupId As Guid = Guid.Empty
    Private Sub MapDataSet(ByVal ds As GetManufacturersDs)

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

    Private Sub Load(ByVal ds As GetManufacturersDs)
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
            Throw New ElitaPlusException("WSUtilities GetManufacturers Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetManufacturersDs)
        Try
            If ds.GetManufacturers.Count = 0 Then Exit Sub
            With ds.GetManufacturers.Item(0)
                If Not .Iscompany_group_codeNull Then
                    Me.CompanyGroupCode = .company_group_code
                    Me.getCompanyGroupId(.company_group_code)
                Else
                    Me._companyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
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
    Private Sub getCompanyGroupId(ByVal Code As String)
        Dim dvCompanyGroups As DataView = LookupListNew.GetCompanyGroupLookupList()
        If Not dvCompanyGroups Is Nothing AndAlso dvCompanyGroups.Count > 0 Then
            Me._companyGroupId = LookupListNew.GetIdFromCode(dvCompanyGroups, Code)
            If _companyGroupId.Equals(Guid.Empty) Then
                Throw New BOValidationException("GetManufacturers Error: ", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INVALID_COMPANY_GROUP)
            End If
        End If
    End Sub
#End Region

#Region "Properties"

    Public Property CompanyGroupCode() As String
        Get
            If Row(Me.DATA_COL_NAME_company_group_code) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_company_group_code), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_company_group_code, Value)
        End Set
    End Property

    Private ReadOnly Property CompanyGroupId() As Guid
        Get
            Return _companyGroupId
        End Get
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim manufacturerBO As New Manufacturer
        Dim manufacturerList As DataView

        Try
            Me.Validate()

            manufacturerList = manufacturerBO.LoadList(String.Empty, Me.CompanyGroupId)

            If manufacturerList Is Nothing OrElse manufacturerList.Count <= 0 Then
                Throw New BOValidationException("GetManufacturers Error: ", Common.ErrorCodes.BO_ERROR_NO_MANUFACTURER_FOUND)
            End If

            Dim ds As New DataSet
            ds.Tables.Add(manufacturerList.ToTable)
            ds.Tables(0).Columns.Remove(Manufacturer.COL_NAME_MANUFACTURER_ID)
           

            Return XMLHelper.FromDatasetToXML(ds)

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
