Imports System.Text.RegularExpressions

Public Class OpenMobileVerifyClaim
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_SERIAL_NUMBER As String = "ESN"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "Cell_Number"
    Private Const TABLE_NAME As String = "OpenMobileVerifyClaim"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const DEALER_NOT_FOUND As String = "NO_DEALER_FOUND"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As OpenMobileVerifyClaimDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As OpenMobileVerifyClaimDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New Dataset
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As OpenMobileVerifyClaimDs)
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
            Throw New ElitaPlusException("Olita GetCertificate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As OpenMobileVerifyClaimDs)
        Try
            If ds.OpenMobileVerifyClaim.Count = 0 Then Exit Sub
            With ds.OpenMobileVerifyClaim.Item(0)
                SerialNumber = .ESN
                CertNumber = .Cell_Number & "*"
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
    Public Property SerialNumber As String
        Get
            If Row(DATA_COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SERIAL_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property
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
#End Region
#Region "Extended Properties"



#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim _claimListDataSet As DataSet = Claim.GetClaimNumberForOpenMobile(CertNumber, SerialNumber)
            _claimListDataSet.Tables(0).TableName = TABLE_NAME

            Return (XMLHelper.FromDatasetToXML_Coded(_claimListDataSet))

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
