Imports System.Text.RegularExpressions

Public Class GalaxySearchCertificate
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const DATA_COL_NAME_CUSTOMER_PHONE As String = "customer_phone"
    Public Const DATA_COL_NAME_VIN_LOCATOR As String = "vin_locator"
    Public Const DATA_COL_NAME_DEALER As String = "dealer_code"
    Public Const DATA_COL_NAME_DEALER_NAME As String = "dealer_name"
    Private _recordsToRreturn As Integer = 100
    Private Const TABLE_NAME As String = "GalaxyCertificateSearch"
    Private Const DATASET_NAME As String = "GalaxyCertificateSearch"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ds As GalaxyCertificateSearchDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"


    Private Sub MapDataSet(ds As GalaxyCertificateSearchDs)

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

    Private Sub Load(ds As GalaxyCertificateSearchDs)
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
            Throw New ElitaPlusException("Galaxy GetCertificate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GalaxyCertificateSearchDs)
        Try
            If ds.GalaxyCertificateSearch.Count = 0 Then Exit Sub
            With ds.GalaxyCertificateSearch.Item(0)

                If .IsIdentification_NumberNull AndAlso .IsVehicle_License_TagNull AndAlso _
                    .IsCert_NumberNull AndAlso .IsVIN_LocatorNull AndAlso _
                    .IsCustomer_NameNull AndAlso .IsDealer_CodeNull AndAlso .IsDealer_NameNull AndAlso .IsCustomer_PhoneNull Then
                    Throw New BOValidationException("Galaxy Invalid Parameters Error", Common.ErrorCodes.WS_MISSING_SEARCH_CRITERION)
                End If

                If Not .IsIdentification_NumberNull Then IdentificationNumber = .Identification_Number
                If Not .IsVehicle_License_TagNull Then VehicleLicenseTag = .Vehicle_License_Tag
                If Not .IsCert_NumberNull Then CertNumber = .Cert_Number
                If Not .IsVIN_LocatorNull Then VinLocator = .VIN_Locator
                If Not .IsCustomer_NameNull Then CustomerName = .Customer_Name
                If Not .IsDealer_CodeNull Then DealerCode = .Dealer_Code
                If Not .IsDealer_NameNull Then DealerName = .Dealer_Name
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
            Throw New ElitaPlusException("Galaxy Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValidateParameters("")> _
    Public Property IdentificationNumber As String
        Get
            If Row(DATA_COL_NAME_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_IDENTIFICATION_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property
    <ValidateParameters("")> _
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


    <ValidateParameters("")> _
    Public Property VehicleLicenseTag As String
        Get
            If Row(DATA_COL_NAME_VEHICLE_LICENSE_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_VEHICLE_LICENSE_TAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VEHICLE_LICENSE_TAG, Value)
        End Set
    End Property


    <ValidateParameters("")> _
    Public Property CustomerName As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    <ValidateParameters("")> _
     Public Property VinLocator As String
        Get
            If Row(DATA_COL_NAME_VIN_LOCATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_VIN_LOCATOR), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VIN_LOCATOR, Value)
        End Set
    End Property


    <ValidateParameters("")> _
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    <ValidateParameters("")> _
    Public Property DealerName As String
        Get
            If Row(DATA_COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_NAME, Value)
        End Set
    End Property

    <ValidateParameters("")> _
    Public Property CustomerPhone As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CUSTOMER_PHONE, Value)
        End Set
    End Property


    Public Property RecordsToReturn As Integer
        Get
            Return _recordsToRreturn
        End Get
        Set
            _recordsToRreturn = Value
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim cert As New Certificate
            Dim _CertListDataSet As DataSet = cert.GetGalaxyCertificatesList(CertNumber, CustomerName, IdentificationNumber, VehicleLicenseTag, VinLocator, DealerCode, DealerName, CustomerPhone, , RecordsToReturn)
            _CertListDataSet.DataSetName = DATASET_NAME
            _CertListDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = DATASET_TABLE_NAME
            Return (XMLHelper.FromDatasetToXML_Std(_CertListDataSet))

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

        Public Sub New(fieldDisplayName As String)
            'fields can not simultaneously be set at zero
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As GalaxySearchCertificate = CType(objectToValidate, GalaxySearchCertificate)


            If ((Not (obj.CertNumber Is Nothing) AndAlso obj.CertNumber.Equals(String.Empty)) AndAlso _
                (Not (obj.IdentificationNumber Is Nothing) AndAlso obj.IdentificationNumber.Equals(String.Empty)) AndAlso _
                (Not (obj.VehicleLicenseTag Is Nothing) AndAlso obj.VehicleLicenseTag.Equals(String.Empty)) AndAlso _
                (Not (obj.VinLocator Is Nothing) AndAlso obj.VinLocator.Equals(String.Empty)) AndAlso _
                (Not (obj.DealerCode Is Nothing) AndAlso obj.DealerCode.Equals(String.Empty)) AndAlso _
                (Not (obj.CustomerName Is Nothing) AndAlso obj.CustomerName.Equals(String.Empty))) Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class

#End Region
End Class
