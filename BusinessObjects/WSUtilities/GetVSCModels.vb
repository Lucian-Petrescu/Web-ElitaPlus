Imports System.Text.RegularExpressions

Public Class GetVSCModels
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COMPANY_GROUP_CODE As String = "company_group_code"
    Public Const DATA_COL_NAME_MAKE As String = "make"
    Private Const TABLE_NAME As String = "GetVSCModels"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetVSCModelsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GetVSCModelsDs)

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

    Private Sub Load(ByVal ds As GetVSCModelsDs)
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
            Throw New ElitaPlusException("WSUtilities VSCGetMakes Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetVSCModelsDs)
        Try
            If ds.GetVSCModels.Count = 0 Then Exit Sub
            With ds.GetVSCModels.Item(0)
                Me.Make = .Make
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

    Public Property CompanyGroupCode() As String
        Get
            If Row(Me.DATA_COL_NAME_COMPANY_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_COMPANY_GROUP_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COMPANY_GROUP_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Make() As String
        Get
            If Row(Me.DATA_COL_NAME_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_MAKE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_MAKE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()
            Dim objCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
            Dim companyGroupID As Guid = objCompanyGroup.Id

            Dim ds As New DataSet
            ds = VSCModel.GetVSCModels(companyGroupID, Make)
            ds.Tables(0).TableName = "Model"

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
