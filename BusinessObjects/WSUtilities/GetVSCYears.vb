Imports System.Text.RegularExpressions

Public Class GetVSCYears
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COMPANY_GROUP_CODE As String = "company_group_code"
    Public Const DATA_COL_NAME_MAKE As String = "make"
    Public Const DATA_COL_NAME_MODEL As String = "model"
    Public Const DATA_COL_NAME_ENGINE_VERSION As String = "engine_version"
    Private Const TABLE_NAME As String = "GetVSCYears"


#End Region

#Region "Constructors"

    Public Sub New(ds As GetVSCYearsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ds As GetVSCYearsDs)

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

    Private Sub Load(ds As GetVSCYearsDs)
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
            Throw New ElitaPlusException("WSUtilities VSCGetMakes Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetVSCYearsDs)
        Try
            If ds.GetVSCYears.Count = 0 Then Exit Sub
            With ds.GetVSCYears.Item(0)
                Make = .Make
                Model = .Model
                EngineVersion = .Engine_Version
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

    Public Property CompanyGroupCode As String
        Get
            If Row(DATA_COL_NAME_COMPANY_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COMPANY_GROUP_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COMPANY_GROUP_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Make As String
        Get
            If Row(DATA_COL_NAME_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MAKE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MAKE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Model As String
        Get
            If Row(DATA_COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_MODEL), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EngineVersion As String
        Get
            If Row(DATA_COL_NAME_ENGINE_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_ENGINE_VERSION), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_ENGINE_VERSION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim objCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
            Dim companyGroupID As Guid = objCompanyGroup.Id

            Dim ds As New DataSet
            ds = VSCModel.GetVSCYears(companyGroupID, Make, Model, EngineVersion)
            ds.Tables(0).TableName = "EngineVersion"

            'Return (Assurant.ElitaPlus.Common.XMLHelper.FromDatasetToXML(ds))
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
