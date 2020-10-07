Imports System.Text.RegularExpressions

Public Class GetCert
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DATA_COL_NAME_CERTIFICATE_NUMBER As String = "CertificateNumber"
    Public Const DATA_COL_NAME_DEALER As String = "dealerCode"
    Public Const DATA_COL_NAME_FOR_CANCELLATION As String = "ForCancellation"

    Private Const TABLE_NAME As String = "GetCert"
    Private Const DATASET_NAME As String = "GetCert"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetCertDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetCertDs)

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

    Private Sub Load(ByVal ds As GetCertDs)
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
            Throw New ElitaPlusException("GetCert Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetCertDs)
        Try
            If ds.GetCert.Count = 0 Then Exit Sub
            With ds.GetCert.Item(0)
                DealerCode = .DealerCode
                CertificateNumber = .CertificateNumber
                If Not .IsForCancellationNull Then
                    ForCancellation = .ForCancellation
                Else
                    ForCancellation = "N" 'default
                End If
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCert Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    Public Property CertificateNumber() As String
        Get
            If Row(DATA_COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ForCancellation() As String
        Get
            If Row(DATA_COL_NAME_FOR_CANCELLATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_FOR_CANCELLATION), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_FOR_CANCELLATION, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim cert As New Certificate
            Dim _CertDataSet As DataSet = cert.GetCertificateForCancellation(DealerId, CertificateNumber, ForCancellation)
            _CertDataSet.DataSetName = DATASET_NAME
            _CertDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = DATASET_TABLE_NAME
            'Return (XMLHelper.FromDatasetToXML_Std(_CertDataSet))
            Return (XMLHelper.FromDatasetToXML(_CertDataSet, Nothing, True, True, True, False, True))

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

    Private ReadOnly Property DealerId() As Guid
        Get
            If _dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("GetCert Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetCert Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return _dealerId
        End Get
    End Property


#End Region

End Class
