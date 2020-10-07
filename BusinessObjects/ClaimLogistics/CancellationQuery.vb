Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjects.Tables
Imports Assurant.ElitaPlus.DALObjects

Public Class CancellationQuery
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "CERT_NUMBER"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_CANCELLATION_DATE As String = "CANCELLATION_DATE"
    Public Const DATA_COL_NAME_REFUND_AMOUNT As String = "REFUND_AMOUNT"
    Private Const TABLE_NAME As String = "CancellationQuery"
    Private Const DATASET_NAME As String = "CancellationQuery"
    Private Const DATA_COL_NAME_CERT_ID As String = "cert_id"
    Public Const CODE As String = "Code"
    Public Const DESCRIPTION As String = "Description"
    Public Const FIRST_ROW As Integer = 0

    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const CERTIFICATE_ALREADY_CLOSED As String = "ERR_CERTIFICATE_ALREADY_CLOSED"
    Private Const ACTIVE_CLAIMS_EXIST As String = "ERR_ACTIVE_CLAIMS_EXIST"
    Public Const MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE As String = "MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE"
    Public Const MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE As String = "MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE"
    Public Const MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS As String = "MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS"
    Public Const MSG_INVALID_CANCELLATION_DATE As String = "MSG_INVALID_CANCELLATION_DATE"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As CancellationQueryDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _certId As Guid = Guid.Empty
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As CancellationQueryDs)

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

    Private Sub Load(ByVal ds As CancellationQueryDs)
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
            Throw New ElitaPlusException("CancellationQuery Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As CancellationQueryDs)
        Try
            If ds.CancellationQuery.Count = 0 Then Exit Sub
            With ds.CancellationQuery.Item(0)
                CertNumber = ds.CancellationQuery.Item(0).CERT_NUMBER
                DealerCode = ds.CancellationQuery.Item(0).DEALER_CODE
                CancellationDate = ds.CancellationQuery.Item(0).CANCELLATION_DATE
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("CancellationQuery Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property CertNumber As String
        Get
            If Row(DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CancellationDate As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CANCELLATION_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property

    Private ReadOnly Property CertID As Guid
        Get
            If _certId.Equals(Guid.Empty) Then
                Dim dsCert As DataSet = Certificate.ClaimLogisticsGetCert(CertNumber, DealerCode)

                If Not dsCert Is Nothing AndAlso dsCert.Tables.Count > 0 AndAlso dsCert.Tables(0).Rows.Count = 1 Then
                    If dsCert.Tables(0).Rows(0).Item(DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                        Throw New BOValidationException("CancellationQuery Error: ", CERTIFICATE_NOT_FOUND)
                    Else
                        _certId = New Guid(CType(dsCert.Tables(0).Rows(0).Item(DATA_COL_NAME_CERT_ID), Byte()))

                        If _certId.Equals(Guid.Empty) Then
                            Throw New BOValidationException("CancellationQuery Error: ", CERTIFICATE_NOT_FOUND)
                        End If
                    End If
                Else
                    Throw New BOValidationException("CancellationQuery Error: ", CERTIFICATE_NOT_FOUND)
                End If
            End If

            Return _certId
        End Get
    End Property

#End Region

#Region "Public Members"


    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim oCancelCertificateData As New CertCancellationData

            Dim oCert As New Certificate(CertID)

            ' Check if the certificate is already closed
            If oCert.StatusCode = "C" Then
                Throw New BOValidationException("CancellationQuery Error: ", CERTIFICATE_ALREADY_CLOSED)
            End If

            ' Check if there is any claims that are not closed
            If oCert.TotalClaimsNotClosedForCert(DealerId, oCert.CertNumber) Then
                Throw New BOValidationException("CancellationQuery Error: ", ACTIVE_CLAIMS_EXIST)
            End If

            ' Get cancellation reason code from Contract record
            Dim oContract As Contract
            oContract = Contract.GetContract(DealerId, oCert.WarrantySalesDate.Value)
            If oContract Is Nothing Then
                oContract = Contract.GetMaxExpirationContract(DealerId)
                If oContract Is Nothing Then
                    Throw New BOValidationException("CancellationQuery Error: ", Common.ErrorCodes.NO_CONTRACT_FOUND)
                End If
            End If

            ' create a cancellation BO
            Dim certCancellationBO As New CertCancellation
            certCancellationBO.CancellationReasonId = oContract.CancellationReasonId
            certCancellationBO.CancellationDate = CancellationDate

            'Call QuoteCancellation
            oCert.QuoteCancellation(certCancellationBO, oCancelCertificateData)

            'Check the outcome
            If Not oCancelCertificateData.errorExist Then
                If oCancelCertificateData.errorExist2 = False Then

                    Dim dsResult = New DataSet(DATASET_NAME)
                    Dim dt As DataTable = New DataTable(TABLE_NAME)
                    dt.Columns.Add(DATA_COL_NAME_CERT_NUMBER, GetType(String))
                    dt.Columns.Add(DATA_COL_NAME_REFUND_AMOUNT, GetType(Decimal))
                    dsResult.Tables.Add(dt)

                    Dim newRow As DataRow = dsResult.Tables(TABLE_NAME).NewRow()
                    newRow(DATA_COL_NAME_CERT_NUMBER) = CertNumber
                    newRow(DATA_COL_NAME_REFUND_AMOUNT) = oCancelCertificateData.refundAmount
                    dsResult.Tables(TABLE_NAME).Rows.Add(newRow)
                    Return XMLHelper.FromDatasetToXML(dsResult, Nothing, True)

                Else
                    Throw New BOValidationException("CancellationQuery Error: ", oCancelCertificateData.errorCode)
                End If
            Else
                Throw New BOValidationException("CancellationQuery Error: ", oCancelCertificateData.errorCode)
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function

 
#End Region


#Region "Extended Properties"

    Private ReadOnly Property DealerId As Guid
        Get
            If _dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("SaveSerialNumberByCertNum Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("SaveSerialNumberByCertNum Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return _dealerId
        End Get
    End Property


#End Region

End Class
