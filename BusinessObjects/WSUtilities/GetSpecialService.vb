Imports System.Text.RegularExpressions

Public Class GetSpecialService
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DATA_COL_NAME_CERTIFICATE_NUMBER As String = "CertificateNumber"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "ClaimNumber"
    Public Const DATA_COL_NAME_CERT_ITEM_COVERAGE_CODE As String = "CoverageTypeCode"

    Private Const TABLE_NAME As String = "GetSpecialService"
    Private Const DATASET_NAME As String = "GetSpecialService"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetSpecialServiceDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _CoverageTypeId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetSpecialServiceDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Shared Sub CheckDeleted()
    End Sub

    Private Sub Load(ByVal ds As GetSpecialServiceDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetSpecialService Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetSpecialServiceDs)
        Try
            If ds.GetSpecialService.Count = 0 Then Exit Sub
            With ds.GetSpecialService.Item(0)
                ClaimNumber = .ClaimNumber
                CertificateNumber = .CertificateNumber
                'If String.IsNullOrEmpty(.CoverageTypeCode) Then
                '    Throw New BOValidationException("GetSpecialService Error: ", Common.ErrorCodes.WS_NO_COVERAGES_FOUND_ERR)
                'End If
                CoverageTypeCode = .CoverageTypeCode
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetSpecialService Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub
#End Region

#Region "Properties"

    Public Property ClaimNumber As String
        Get
            If Row(DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property CertificateNumber As String
        Get
            If Row(DATA_COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property


    Public Property CoverageTypeCode As String
        Get
            If Row(DATA_COL_NAME_CERT_ITEM_COVERAGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CERT_ITEM_COVERAGE_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_ITEM_COVERAGE_CODE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim splsvc As New SpecialService
            Dim _specialServiceDs As DataSet = splsvc.getSpecialServices(ClaimNumber, CertificateNumber, CoverageTypeId)
            Return (XMLHelper.FromDatasetToXML(_specialServiceDs, Nothing, True, True, True, False, True))

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

    Private ReadOnly Property CoverageTypeId As Guid
        Get
            If Not String.IsNullOrEmpty(CoverageTypeCode) Then
                Dim list As DataView = LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If list Is Nothing Then
                    Throw New BOValidationException("GetSpecialService Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                _CoverageTypeId = LookupListNew.GetIdFromCode(list, CoverageTypeCode)
                If _CoverageTypeId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetSpecialService Error: ", Common.ErrorCodes.WS_NO_COVERAGES_FOUND_ERR)
                End If
                list = Nothing
            End If
            Return _CoverageTypeId
        End Get
    End Property


#End Region

End Class
