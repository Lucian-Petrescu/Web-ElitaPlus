Imports System.Text.RegularExpressions

Public Class GalaxyGetCertificateDetail
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const DATA_COL_NAME_VIN_LOCATOR As String = "vin_locator"
    Public Const DATA_COL_NAME_DEALER As String = "dealer_code"
    Private _recordsToRreturn As Integer = 100

    Private Const TABLE_NAME As String = "GalaxyGetCertificateDetail"
    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"

    Public Const DATA_COL_NAME_CERT_ID As String = "cert_id"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GalaxyGetCertificateDetailDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As GalaxyGetCertificateDetailDs)

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

    Private Sub Load(ByVal ds As GalaxyGetCertificateDetailDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Galaxy GetCertificate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GalaxyGetCertificateDetailDs)
        Try
            If ds.GalaxyGetCertificateDetail.Count = 0 Then Exit Sub
            With ds.GalaxyGetCertificateDetail.Item(0)
                If (.Cert_Number Is Nothing OrElse .Cert_Number.Equals(String.Empty)) AndAlso (.Dealer_Code Is Nothing OrElse .Dealer_Code.Equals(String.Empty)) Then
                    Throw New BOValidationException("Galaxy Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA)
                End If
                Me.CertNumber = .Cert_Number
                Me.DealerCode = .Dealer_Code
            End With
        Catch ex As Exception
            Throw New ElitaPlusException("Galaxy Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property CertNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim _CertificateDetailDataSet As DataSet = Certificate.GalaxyGetCertificateDetail(Me.CertNumber, Me.DealerCode)
            If Not _CertificateDetailDataSet Is Nothing AndAlso _CertificateDetailDataSet.Tables.Count > 0 AndAlso _CertificateDetailDataSet.Tables(0).Rows.Count > 0 Then
                If _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                    Throw New BOValidationException("GalaxyGetCertificateDetail Error: ", CERTIFICATE_NOT_FOUND)
                Else
                    _CertificateDetailDataSet.DataSetName = Me.TABLE_NAME
                    Dim cert_id As New Guid(CType(_CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte()))
                    If cert_id.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GalaxyGetCertificateDetail Error: ", CERTIFICATE_NOT_FOUND)
                    End If

                    'Get Cert Item Coverages
                    Dim _CertCoveragesDataSet As DataSet = CertItemCoverage.LoadAllItemCoveragesForGalaxyCertificate(cert_id)
                    If Not _CertCoveragesDataSet Is Nothing AndAlso _CertCoveragesDataSet.Tables.Count > 0 AndAlso _CertCoveragesDataSet.Tables(0).Rows.Count > 0 Then
                        'add the coverages table to the certificate dataset
                        _CertificateDetailDataSet.Tables.Add(_CertCoveragesDataSet.Tables(0).Copy)
                        'remove the cert_id (guid) column from the certificate table
                        _CertificateDetailDataSet.Tables(0).Columns.Remove(Me.DATA_COL_NAME_CERT_ID)
                    Else
                        Throw New BOValidationException("GalaxyGetCertificateDetail Error: ", Me.CERTIFICATE_COVERAGES_NOT_FOUND)
                    End If

                    Return XMLHelper.FromDatasetToXML_Std(_CertificateDetailDataSet)
                End If
            ElseIf _CertificateDetailDataSet Is Nothing Then
                Throw New BOValidationException("GalaxyGetCertificateDetail Error: ", Me.ERROR_ACCESSING_DATABASE)
            End If

        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

End Class
