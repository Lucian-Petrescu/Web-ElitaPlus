'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/16/2004)  ********************

Public Class Country
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CountryDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CountryDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub LoadChildren(ByVal reloadData As Boolean)
        CountryPostalCodeFormat.LoadList(Dataset, Id, reloadData)
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Constants"
    Public Const TABLE_NAME_COUNTRY_REGIONS_RELATIONS As String = "COUNTRY_REGIONS_RELATIONS"

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CountryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property LanguageId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_LANGUAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_LANGUAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property PrimaryCurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_PRIMARY_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_PRIMARY_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_PRIMARY_CURRENCY_ID, Value)
        End Set
    End Property



    Public Property SecondaryCurrencyId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_SECONDARY_CURRENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_SECONDARY_CURRENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_SECONDARY_CURRENCY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property MailAddrFormat() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_MAIL_ADDR_FORMAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_MAIL_ADDR_FORMAT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_MAIL_ADDR_FORMAT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=10)>
    Public Property BankIDLength() As LongType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_BANK_ID_LENGTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CountryDAL.COL_NAME_BANK_ID_LENGTH), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_BANK_ID_LENGTH, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=29)>
    Public Property BankAcctNoLength() As Integer
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_BANK_ACCT_LENGTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_BANK_ACCT_LENGTH), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_BANK_ACCT_LENGTH, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property EuropeanCountryId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_EUROPEAN_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_EUROPEAN_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_EUROPEAN_COUNTRY_ID, value)
        End Set
    End Property

    Public Property ValidateBankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_VALIDATE_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_VALIDATE_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_VALIDATE_BANK_INFO_ID, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property TaxByProductTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_TAX_BY_PRODUCT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_TAX_BY_PRODUCT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_TAX_BY_PRODUCT_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RequireByteCheckId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_REQUIRE_BYTE_CONV_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_REQUIRE_BYTE_CONV_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_REQUIRE_BYTE_CONV_ID, Value)
        End Set
    End Property

    Public Property DefaultScForDeniedClaims() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_DEFAULT_SC_FOR_DENIED_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_DEFAULT_SC_FOR_DENIED_CLAIMS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_DEFAULT_SC_FOR_DENIED_CLAIMS, Value)
        End Set
    End Property

    Public Property DefaultSCId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_DEFAULT_SC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_DEFAULT_SC_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_DEFAULT_SC_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property ContactInfoReqFields() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_CONTACT_INFO_REQ_FIELDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_CONTACT_INFO_REQ_FIELDS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_CONTACT_INFO_REQ_FIELDS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property AddressInfoReqFields() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ADDRESS_INFO_REQ_FIELDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_ADDRESS_INFO_REQ_FIELDS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ADDRESS_INFO_REQ_FIELDS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property UseBankListId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_USE_BANK_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_USE_BANK_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_USE_BANK_LIST_ID, Value)
        End Set
    End Property

    Public Property LastRegulatoryExtractDate() As DateType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_LAST_REGULATORY_EXTRACT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CountryDAL.COL_NAME_LAST_REGULATORY_EXTRACT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_LAST_REGULATORY_EXTRACT_DATE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=100)>
    Public Property CreditScoringPct() As LongType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_CREDIT_SCORING_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_CREDIT_SCORING_PCT), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_CREDIT_SCORING_PCT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=1, Max:=99)>
    Public Property AbnormalCLMFreqNo() As LongType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ABNORMAL_CLM_FRQ_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CountryDAL.COL_NAME_ABNORMAL_CLM_FRQ_NO), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ABNORMAL_CLM_FRQ_NO, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=1, Max:=99)>
    Public Property CertCountSuspOP() As LongType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_CERT_COUNT_SUSP_OP) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CountryDAL.COL_NAME_CERT_COUNT_SUSP_OP), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_CERT_COUNT_SUSP_OP, Value)
        End Set
    End Property

    Public Property RegulatoryReportingId() As Guid
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_REGULATORY_REPORTING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryDAL.COL_NAME_REGULATORY_REPORTING_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_REGULATORY_REPORTING_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200), EmailAddress(""), RequiredConditionally("")>
    Public Property NotifyGroupEmail() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_NOTIFY_GROUP_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_NOTIFY_GROUP_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_NOTIFY_GROUP_EMAIL, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property UseAddressValidationXcd() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_USE_ADDRESS_VALIDATION_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_USE_ADDRESS_VALIDATION_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_USE_ADDRESS_VALIDATION_XCD, Value)
        End Set
    End Property

    <RequiredConditionallyAllowForceAddressXcd("")>
    Public Property AllowForceAddressXcd() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ALLOW_FORCE_ADDRESS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_ALLOW_FORCE_ADDRESS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ALLOW_FORCE_ADDRESS_XCD, Value)
        End Set
    End Property

    Public Property AllowForget() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ALLOW_FORGOTTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_ALLOW_FORGOTTEN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ALLOW_FORGOTTEN, Value)
        End Set
    End Property
    Public Property PriceListApprovalNeeded() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_NEEDED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_NEEDED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_NEEDED, Value)
        End Set
    End Property
    <RequiredConditionallyAddressConfidenceThreshold(""), ValidNumericRange("", Min:=1, Max:=100)>
    Public Property AddressConfidenceThreshold() As LongType
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ADDRESS_CONFIDENCE_THRESHOLD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_ADDRESS_CONFIDENCE_THRESHOLD), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ADDRESS_CONFIDENCE_THRESHOLD, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)>
    Public Property IsoCode() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_ISO_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_ISO_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_ISO_CODE, value)
        End Set
    End Property

    <ValidStringLength("", Max:=1000), PriceListApprovedEmail(""), RequiredConditionally("")>
    Public Property PriceListApprovalEmail() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_EMAIL), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_PRICE_LIST_APPROVAL_EMAIL, value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property FullNameFormat() As String
        Get
            CheckDeleted()
            If Row(CountryDAL.COL_NAME_FULL_NAME_FORMAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryDAL.COL_NAME_FULL_NAME_FORMAT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CountryDAL.COL_NAME_FULL_NAME_FORMAT, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsFamilyDirty()
        End Get
    End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CountryDAL
                'dal.Update(Me.Row)
                dal.UpdateFamily(Dataset) ' changed to take care of join table                
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Load(Id)
                    LoadChildren(True)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub AttachPostalCodeFormat(ByVal postalCodeGuidStr() As String)
        LoadChildren(False)
        If Not postalCodeGuidStr Is Nothing AndAlso postalCodeGuidStr.Length > 0 Then
            Dim i As Integer
            For i = 0 To postalCodeGuidStr.Length - 1
                Dim PosCodeFormatId As Guid = New Guid(postalCodeGuidStr(i))
                'Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Me.Dataset, Me.Id, New Guid(postalCodeGuidStr(i)))
                Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Dataset, Id, PosCodeFormatId)
                If couPosCode Is Nothing Then
                    couPosCode = New CountryPostalCodeFormat(Dataset)
                    couPosCode.PostalCodeFormatId = PosCodeFormatId
                    couPosCode.CountryId = Id
                    couPosCode.Save()
                End If
            Next
        End If
    End Sub

    Public Sub AttachPostalCodeFormat(ByVal postalCodeGuidStr As String)
        LoadChildren(False)
        If Not postalCodeGuidStr Is Nothing AndAlso postalCodeGuidStr.Length > 0 Then
            Dim i As Integer
            Dim PosCodeFormatId As Guid = New Guid(postalCodeGuidStr)
            'Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Me.Dataset, Me.Id, New Guid(postalCodeGuidStr))
            Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Dataset, Id, PosCodeFormatId)
            If couPosCode Is Nothing Then
                couPosCode = New CountryPostalCodeFormat(Dataset)
                couPosCode.PostalCodeFormatId = PosCodeFormatId
                couPosCode.CountryId = Id
                couPosCode.Save()
            End If
        End If
    End Sub

    Public Sub DetachPostalCodeFormat(ByVal postalCodeGuidStr() As String)
        LoadChildren(False)
        If Not postalCodeGuidStr Is Nothing AndAlso postalCodeGuidStr.Length > 0 Then
            Dim i As Integer
            For i = 0 To postalCodeGuidStr.Length - 1
                Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Dataset, Id, New Guid(postalCodeGuidStr(i)))
                couPosCode.Delete()
                couPosCode.Save()
            Next
        End If
    End Sub
    Public Sub DetachPostalCodeFormat(ByVal postalCodeGuidStr As String)
        LoadChildren(False)
        If Not postalCodeGuidStr Is Nothing AndAlso postalCodeGuidStr.Length > 0 Then
            Dim i As Integer
            Dim couPosCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Dataset, Id, New Guid(postalCodeGuidStr))
            couPosCode.Delete()
            couPosCode.Save()
        End If
    End Sub
    'Public Function getPostalCodeList() As DataView
    '    Try
    '        Dim tableIndex As Integer = Me.Dataset.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME)
    '        If tableIndex <> -1 Then
    '            Return New DataView(Me.Dataset.Tables(tableIndex))
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function

    Public Function GetAvailablePostalCode() As DataView
        LoadChildren(False)
        'Dim pCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Me.Dataset, Me.Id)
        'If pCode Is Nothing Then
        '    'Return all
        '    Return LookupListNew.GetPostalCodeFormatLookupList()
        'Else
        Return CountryPostalCodeFormat.GetAvailablePostalCodeFormat(Dataset, Id)
        'End If
    End Function

    Public Function GetSelectedPostalCode() As DataView
        LoadChildren(False)
        'Dim pCode As CountryPostalCodeFormat = CountryPostalCodeFormat.Find(Me.Dataset, Me.Id)
        'If pCode Is Nothing Then
        '    Return Nothing
        'Else
        Return CountryPostalCodeFormat.GetSelectedPostalCodeFormat(Dataset, Id)
        'End If
    End Function

    'Public Sub DeleteChild(ByVal ChildID As System.Guid)
    '    Dim postalcodeCountry As New CountryPostalCodeFormat(ChildID, Me.Dataset)
    '    If Not postalcodeCountry Is Nothing Then
    '        postalcodeCountry.Delete()
    '        postalcodeCountry.Save()
    '    End If
    'End Sub

    'Public Sub DeleteChild_Old(ByVal ChildID As System.Guid)
    '    Try
    '        Dim tableIndex As Integer = Me.Dataset.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME)
    '        If tableIndex <> -1 Then
    '            Dim delRow As DataRow = Me.FindRow(ChildID, CountryPostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID, Me.Dataset.Tables(CountryPostalCodeFormatDAL.TABLE_NAME))
    '            If Not delRow Is Nothing Then
    '                delRow.Delete()
    '            End If
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

    'Public Sub AddChild(ByVal postalCodeFormatID As System.Guid)
    '    Dim postalcodeCountry As New CountryPostalCodeFormat(Me.Dataset)
    '    If Not postalcodeCountry Is Nothing Then
    '        postalcodeCountry.CountryId = Me.Id
    '        postalcodeCountry.PostalCodeFormatId = postalCodeFormatID
    '        postalcodeCountry.Save()
    '    End If
    'End Sub

    'Public Sub AddChild_Old(ByVal countryID As System.Guid, ByVal postalCodeFormatID As System.Guid)
    '    Try
    '        Dim tableIndex As Integer = Me.Dataset.Tables.IndexOf(CountryPostalCodeFormatDAL.TABLE_NAME)
    '        If tableIndex <> -1 Then
    '            Dim newRow As DataRow = Me.Dataset.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).NewRow
    '            Me.Dataset.Tables(CountryPostalCodeFormatDAL.TABLE_NAME).Rows.Add(newRow)
    '            newRow.Item(CountryPostalCodeFormatDAL.TABLE_KEY_NAME) = Guid.NewGuid
    '            newRow.Item(CountryPostalCodeFormatDAL.COL_NAME_COUNTRY_ID) = countryID
    '            newRow.Item(CountryPostalCodeFormatDAL.COL_NAME_POSTAL_CODE_FORMAT_ID) = postalCodeFormatID
    '            newRow.Item(DALBase.COL_NAME_CREATED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal descriptionMask As String, ByVal codeMask As String) As CountrySearchDV
        Try
            Dim dal As New CountryDAL
            Return New CountrySearchDV(dal.LoadList(descriptionMask, codeMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCountriesDv(ByVal oCompanies As ArrayList) As DataView
        Dim dal As New CountryDAL
        Dim ds As DataSet

        ds = dal.LoadCountries(oCompanies)
        Return ds.Tables(CountryDAL.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetCountries(ByVal oCompanies As ArrayList) As ArrayList
        Dim oCountriesDv = GetCountriesDv(oCompanies)
        Dim oCountriesArr = New ArrayList

        If oCountriesDv.Table.Rows.Count > 0 Then
            Dim index As Integer

            ' Create Array
            For index = 0 To oCountriesDv.Table.Rows.Count - 1
                If Not oCountriesDv.Table.Rows(index)(CountryDAL.COL_NAME_COUNTRY_ID) Is System.DBNull.Value Then
                    oCountriesArr.Add(New Guid(CType(oCountriesDv.Table.Rows(index)(CountryDAL.COL_NAME_COUNTRY_ID), Byte())))
                End If
            Next
        End If

        Return oCountriesArr
    End Function

    Public Shared Function GetCountriesRegionsForWS(ByVal oCountriesIds As ArrayList) As DataSet
        Try
            Dim objCountriesRegionsDS As New DataSet("CountriesRegions")

            'Get the Countries
            Dim dalCountry As New CountryDAL
            objCountriesRegionsDS = dalCountry.LoadListForWS(objCountriesRegionsDS, oCountriesIds)

            'Get the regions
            Dim dsRegions As DataSet = Region.LoadListForWS(oCountriesIds)
            objCountriesRegionsDS.Tables.Add(dsRegions.Tables(0).Copy)

            'Rename tables:
            objCountriesRegionsDS.Tables("ELP_COUNTRY").TableName = "COUNTRY"
            objCountriesRegionsDS.Tables("ELP_REGION").TableName = "REGION"

            'create the relation
            Dim CountryToRegionsRel As New DataRelation("COUNTRY_REGIONS_RELATIONS",
                                                             objCountriesRegionsDS.Tables("COUNTRY").Columns("country_id"),
                                                             objCountriesRegionsDS.Tables("REGION").Columns("country_id"))
            CountryToRegionsRel.Nested = True
            objCountriesRegionsDS.Relations.Add(CountryToRegionsRel)

            Return objCountriesRegionsDS
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCountryPostalFormat(ByVal oCountryId As Guid) As DataView

        Try
            Dim dal As New CountryDAL
            Return dal.GetCountryPostalFormat(oCountryId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCountryByteFlag(ByVal oCountryId As Guid) As DataSet
        Try
            Dim dal As New CountryDAL
            Return dal.GetCountryByteFlag(oCountryId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#Region "CountrySearchDV"
    Public Class CountrySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COUNTRY_ID As String = "country_id"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_CODE As String = "code"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#End Region

#Region "CustomValidation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Country = CType(objectToValidate, Country)

            If obj.NotifyGroupEmail Is String.Empty OrElse obj.NotifyGroupEmail Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.NotifyGroupEmail)

        End Function

    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class PriceListApprovedEmail
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Country = CType(objectToValidate, Country)


            If obj.PriceListApprovalNeeded = "YESNO-Y" Then
                If obj.PriceListApprovalEmail Is String.Empty OrElse obj.PriceListApprovalEmail Is Nothing Then
                    Return True
                End If
            Else
                Return True
            End If

            Dim arrayEmailList() As String
            Dim isValidEmail As Boolean
            arrayEmailList = obj.PriceListApprovalEmail.Split(";")

            For Each email As String In arrayEmailList
                isValidEmail = MiscUtil.EmailAddressValidation(email)

                If (Not isValidEmail) Then
                    Exit For
                End If
            Next

            Return isValidEmail

        End Function

    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RequiredConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Country = CType(objectToValidate, Country)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
            If Not obj.RegulatoryReportingId.Equals(Guid.Empty) AndAlso obj.RegulatoryReportingId.Equals(yesId) AndAlso (valueToCheck Is Nothing OrElse valueToCheck.Equals(String.Empty)) Then
                Return False
            End If
            Return True
        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RequiredConditionallyAllowForceAddressXcd
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Country = CType(objectToValidate, Country)
            Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
            lkYesNo.RowFilter += " and code='Y'"
            Dim yesCode As String = Codes.YESNO + "-" + CType(lkYesNo.Item(0).Item("CODE"), String)
            If Not String.IsNullOrEmpty(obj.UseAddressValidationXcd) AndAlso obj.UseAddressValidationXcd.Equals(yesCode) Then
                If (obj.AllowForceAddressXcd Is Nothing OrElse obj.AllowForceAddressXcd.Equals(String.Empty)) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RequiredConditionallyAddressConfidenceThreshold
        Inherits ValidBaseAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Country = CType(objectToValidate, Country)
            Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
            lkYesNo.RowFilter += " and code='Y'"
            Dim yesCode As String = Codes.YESNO + "-" + CType(lkYesNo.Item(0).Item("CODE"), String)
            If Not String.IsNullOrEmpty(obj.UseAddressValidationXcd) AndAlso obj.UseAddressValidationXcd.Equals(yesCode) Then
                If (obj.AddressConfidenceThreshold Is Nothing OrElse obj.AddressConfidenceThreshold.Equals(String.Empty)) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region

End Class


