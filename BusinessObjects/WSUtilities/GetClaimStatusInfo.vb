Imports System.Text.RegularExpressions
Imports System.Xml.Linq
Imports System.Collections.Generic

Public Class GetClaimStatusInfo
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CUSTOMER_IDENTIFIER As String = "CustomerIdentifier"
    Public Const DATA_COL_NAME_IDENTIFIER_TYPE As String = "IdentifierType"
    Public Const DATA_COL_NAME_DEALER As String = "CarrierID"
    Public Const DATA_COL_NAME_BILLING_ZIP_CODE As String = "BillingZipCode"
    Public Const DATA_COL_NAME_LANGUAGE As String = "Language"
    Public Const DATA_COL_NAME_CERTIFICATE As String = "CertificateNumber"

    Private Const TABLE_NAME As String = "GetClaimStatusInfo"
    Private Const TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE As String = "GetClaimStatusInfoResponse"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const DATASET_NAME__CLAIM_CHECK_RESPONSE As String = "ClaimCheckResponse"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetClaimStatusInfoDS)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetClaimStatusInfoDS)

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

    Private Sub Load(ByVal ds As GetClaimStatusInfoDS)
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
            Throw New ElitaPlusException("GetClaimStatusInfo Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetClaimStatusInfoDS)
        Try
            If ds.GetClaimStatusInfo.Count = 0 Then Exit Sub
            With ds.GetClaimStatusInfo.Item(0)
                If Not .IsCarrierIDNull Then DealerCode = .CarrierID
                If Not .IsLanguageNull Then LanguageISOCode = .Language
                If Not .IsCertificateNumberNull Then CertificateNumber = .CertificateNumber

                CustomerIdentifier = .CustomerIdentifier
                IdentifierType = .IdentifierType

                If Not .IsBillingZipCodeNull Then BillingZipCode = .BillingZipCode

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimStatusInfo Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Function IsDealerWebEnabled() As Boolean
        Dim objDealer As New Dealer(_dealerId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.DealerSupportWebClaimsId) = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function FindDealer() As String
        If _dealerId.Equals(Guid.Empty) AndAlso (Not DealerCode Is Nothing AndAlso DealerCode.Trim <> String.Empty) Then
            Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If list Is Nothing Then
                Dim ResponseStatus As DataTable = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE), _
                                                                           Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE, _
                                                                           Codes.WEB_EXPERIENCE__FATAL_ERROR)
                Dim _errorDataSet As New DataSet(DATASET_NAME__CLAIM_CHECK_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
            End If
            _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
            If _dealerId.Equals(Guid.Empty) Then
                Dim ResponseStatus As DataTable = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_DEALER_NOT_FOUND), _
                                                           Common.ErrorCodes.WS_DEALER_NOT_FOUND, _
                                                           Codes.WEB_EXPERIENCE__LOOKUP_ERROR)
                Dim _errorDataSet As New DataSet(DATASET_NAME__CLAIM_CHECK_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
            End If
            list = Nothing

        End If
        Return String.Empty
    End Function
#End Region

#Region "Properties"

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

    Public Property LanguageISOCode() As String
        Get
            If Row(DATA_COL_NAME_LANGUAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_LANGUAGE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_LANGUAGE, Value)
        End Set
    End Property
    Public Property CertificateNumber() As String
        Get
            If Row(DATA_COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERTIFICATE, Value)
        End Set
    End Property

    Public Property BillingZipCode() As String
        Get
            If Row(DATA_COL_NAME_BILLING_ZIP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_BILLING_ZIP_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_BILLING_ZIP_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CustomerIdentifier() As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CUSTOMER_IDENTIFIER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IdentifierType() As String
        Get
            If Row(DATA_COL_NAME_IDENTIFIER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_IDENTIFIER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_IDENTIFIER_TYPE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            'Not IdentifierType Is Nothing AndAlso IdentifierType.Trim <> String.Empty
            If Not DealerCode Is Nothing AndAlso DealerCode.Trim <> String.Empty Then
                Dim strErrorFindingDealer As String = FindDealer
                If strErrorFindingDealer <> String.Empty Then
                    Return strErrorFindingDealer
                End If
            End If
            Dim ValidateErrorCode As Integer
            Dim _claimStatusInfoDataSet As DataSet = Claim.WS_GetClaimStatusInfo(CustomerIdentifier, IdentifierType, _dealerId, BillingZipCode, LanguageISOCode, CertificateNumber, ValidateErrorCode)

            If ValidateErrorCode <> 0 Then ' There is a Validation error
                Return (XMLHelper.FromDatasetToXML(_claimStatusInfoDataSet, Nothing, True, True, True, False, True))
            Else
                If Not _claimStatusInfoDataSet.Tables(ClaimDAL.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE) Is Nothing _
                    AndAlso _claimStatusInfoDataSet.Tables(ClaimDAL.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE).Rows.Count = 0 Then
                    Dim ResponseStatus As DataTable = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_NO_RECORDS_FOUND),
                                                           Common.ErrorCodes.WS_NO_RECORDS_FOUND,
                                                           Codes.WEB_EXPERIENCE__LOOKUP_ERROR)
                    Dim _errorDataSet As New DataSet(DATASET_NAME__CLAIM_CHECK_RESPONSE)
                    _errorDataSet.Tables.Add(ResponseStatus)
                    Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
                Else
                    Dim excludeTags As ArrayList = New ArrayList()
                    excludeTags.Add("/ClaimCheckResponse/GetClaimStatusInfoResponse/Extended_Status_History/CLAIMNUMBER")
                    Dim xml As XElement = XElement.Parse(XMLHelper.FromDatasetToXML(_claimStatusInfoDataSet, excludeTags, True, True, True, False, True))
                    Dim ClaimStatusResponses As IEnumerable(Of XElement) = xml.Descendants(ClaimDAL.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE)
                    For Each ClaimStatusResponse As XElement In ClaimStatusResponses
                        Dim ExtendedStatusHistElements = ClaimStatusResponse.Elements(ClaimDAL.TABLE_NAME__GET_CLAIM_EXT_STATUS_INFO_RESPONSE)
                        ClaimStatusResponse.Add(New XElement("Extended_Statuses", ExtendedStatusHistElements))
                        ClaimStatusResponse.Elements(ClaimDAL.TABLE_NAME__GET_CLAIM_EXT_STATUS_INFO_RESPONSE).Remove()
                    Next
                    Return XML_VERSION_AND_ENCODING & xml.ToString()
                End If
            End If

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
