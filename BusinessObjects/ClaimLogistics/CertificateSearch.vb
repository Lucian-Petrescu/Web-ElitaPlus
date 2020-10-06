Imports System.Text.RegularExpressions

Public Class CertificateSearch
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const DATA_COL_NAME_CUSTOMER_PHONE As String = "customer_phone"
        Private _recordsToRreturn As Integer = 100
    Private Const TABLE_NAME As String = "CertificateSearch"
    Private Const DATASET_NAME As String = "CertificateSearch"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As CertificateSearchDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ByVal ds As CertificateSearchDs)

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

    Private Sub Load(ByVal ds As CertificateSearchDs)
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
            Throw New ElitaPlusException("Certificate Search Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As CertificateSearchDs)
        Try
            If ds.CertificateSearch.Count = 0 Then Exit Sub
            With ds.CertificateSearch.Item(0)

                If .IsCert_NumberNull AndAlso .IsCustomer_NameNull AndAlso .IsCustomer_PhoneNull Then
                    Throw New BOValidationException("Invalid Parameters Error", Common.ErrorCodes.WS_MISSING_SEARCH_CRITERION)
                End If

                If Not .IsCert_NumberNull Then CertNumber = .Cert_Number
                If Not .IsCustomer_NameNull Then CustomerName = .Customer_Name
                If Not .IsCustomer_PhoneNull Then CustomerPhone = .Customer_Phone
                If Not .IsRecords_To_ReturnNull Then
                    RecordsToReturn = .Records_To_Return
                Else
                    RecordsToReturn = 100
                End If

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValidateParameters("")> _
    Public Property CertNumber() As String
        Get
            If Row(DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValidateParameters("")> _
    Public Property CustomerName() As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValidateParameters("")> _
    Public Property CustomerPhone() As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CUSTOMER_PHONE, Value)
        End Set
    End Property


    Public Property RecordsToReturn() As Integer
        Get
            Return _recordsToRreturn
        End Get
        Set(ByVal Value As Integer)
            _recordsToRreturn = Value
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim cert As New Certificate
            Dim _CertListDataSet As DataSet = cert.OlitaGetCertificatesList(CertNumber, CustomerName, CustomerPhone, , RecordsToReturn)
            _CertListDataSet.DataSetName = DATASET_NAME
            _CertListDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = DATASET_TABLE_NAME
            'Return (XMLHelper.FromDatasetToXML_Std(_CertListDataSet))
            Return (XMLHelper.FromDatasetToXML(_CertListDataSet, Nothing, True, True, True, False, True))

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

#Region "CustomValidation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateParameters
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            'fields can not simultaneously be set at zero
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertificateSearch = CType(objectToValidate, CertificateSearch)


            If ((Not (obj.CertNumber Is Nothing) AndAlso obj.CertNumber.Equals(String.Empty)) AndAlso _
                (Not (obj.CustomerName Is Nothing) AndAlso obj.CustomerName.Equals(String.Empty)) AndAlso _
                (Not (obj.CustomerPhone Is Nothing) AndAlso obj.CustomerPhone.Equals(String.Empty))) Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class

#End Region
End Class
