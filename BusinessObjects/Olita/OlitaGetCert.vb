Imports System.Text.RegularExpressions

Public Class OlitaGetCert
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER As String = "dealer"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_INVOICE_NUMBER As String = "INVOICE_NUMBER"
    Private Const TABLE_NAME As String = "OlitaGetCert"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As OlitaGetCertDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As OlitaGetCertDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New Dataset
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As OlitaGetCertDs)
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
            Throw New ElitaPlusException("Olita GetCertificate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As OlitaGetCertDs)
        Try
            If ds.OlitaGetCert.Count = 0 Then Exit Sub
            With ds.OlitaGetCert.Item(0)
                Dealer = .dealer
                CertNumber = .cert_number
                If Not .Isinvoice_numberNull Then Me.InvoiceNumber = .invoice_number
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub
    Private Function LoadUserCountryList() As DataView
        Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim oCountryList As DataView
        oCountryList = LookupListNew.GetCompanyGroupCountryLookupList(CompanyGroupId)
        Return oCountryList
    End Function
#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property Dealer() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_DEALER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property
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

    Public Property InvoiceNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property
#End Region
#Region "Extended Properties"

    Private ReadOnly Property DealerId() As Guid
        Get
            If Me._dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("OlitagetCertInfo Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                Me._dealerId = LookupListNew.GetIdFromCode(list, Me.Dealer)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return Me._dealerId
        End Get
    End Property


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim _CertListDataSet As DataSet = Certificate.GetOlitaConsumerCertList(Me.CertNumber, Me.DealerId, Me.InvoiceNumber)
            'Dim _CertListDataSet As DataSet = cert.GetCertificatesList(Me.CertNumber, "", "", "", "", Me.Dealer).Table.DataSet
            'remove guids from web service
            _CertListDataSet.Tables(CertificateDAL.TABLE_NAME).Columns.Remove(CertificateDAL.COL_NAME_CERT_ID)
            _CertListDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = Me.TABLE_NAME
            'Return (XMLHelper.FromDatasetToXML(_CertListDataSet))
            If _CertListDataSet.Tables(0).Rows.Count > 0 Then
                'Get elita countries. This will be used by a country dropdown in Olita coonsumer registration screen.
                Dim dvCountries As DataView = LookupListNew.GetCountryLookupList() 'Me.LoadUserCountryList 
                'remove guids from web service
                Dim tableCountries As DataTable = dvCountries.Table.Copy
                tableCountries.Columns.Remove(LookupListNew.COL_ID_NAME)
                _CertListDataSet.Tables.Add(tableCountries)
            End If
            'Return (XMLHelper.FromDatasetToXML_Coded(_CertListDataSet))
            Return XMLHelper.FromDatasetToXML(_CertListDataSet, Nothing, True, True, True, False, True)

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
