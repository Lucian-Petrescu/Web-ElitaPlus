
Public Class SubmitClaim
    Inherits BusinessObjectBase

#Region "Member Variables"

    Private _shippingAddress As Address
    Private isVisitDateNull As Boolean = True
    Private isInvoiceDateNull As Boolean = True

    'Private CertItemCoverageId As Guid = Guid.Empty
    'Private MethodOfRepairId As Guid = Guid.Empty
    'Private FollowupDate As Date
    'Private CreatedByUser As String
    'Private ClaimGroupId As Guid = Guid.Empty
    'Private CompanyId As Guid = Guid.Empty
    'Private MasterClaimNumber As String
    'Private DeductibleByPercentId As Guid = Guid.Empty
    'Private OriginalRiskTypeId As Guid = Guid.Empty
    'Private ClaimActivityId As Guid = Guid.Empty

    'Private RepairCodeId As Guid = Guid.Empty

    'Private DeductiblePercent As Decimal
    'Private dsItemCoverages As DataSet
    'Private dsCoverageInfo As DataSet

    Private _isShippingInfoProvided As Boolean = False
    Private _shippingCountryCode As String
    Private _regionCode As String

    Private _contactNumber As String
    Private _emailAddress As String
    Private _attentionTo As String
    'Private _dealerId As Guid = Guid.Empty

#End Region

#Region "Constants"

    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const TABLE_NAME__CLAIM_INFO As String = "ClaimInfoResponse"
    Private Const DATASET_NAME__SUBMIT_CLAIM_RESPONSE As String = "SubmitClaimResponse"

    Private TABLE_NAME As String = "SubmitClaim"
    Private TABLE_NAME_COVERAGE As String = "COVERAGES"
    Private TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"
    Private Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Private Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const CLAIM_NUMBER As String = "ClaimNumber"
    Private Const VALUE_OK As String = "OK"

    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const INVALID_REPAIR_CODE As String = "INVALID_REPAIR_CODE_ERR"
    Private Const INVALID_CAUSE_OF_LOSS_CODE As String = "INVALID_CAUSE_OF_LOSS_CODE_ERR"
    Private Const INVALID_STATUS_CODE As String = "INVALID_STATUS_CODE"
    Private Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"

    Private Const DATA_COL_NAME_CUSTOMER_IDENTIFIER As String = "CustomerIdentifier"
    Private Const DATA_COL_NAME_IDENTIFIER_TYPE As String = "IdentifierType"
    Private Const SOURCE_COL_DEALER_CODE As String = "DEALERCODE"
    Private Const SOURCE_COL_CONTACT_NAME As String = "CONTACTNAME"
    Private Const SOURCE_COL_CUSTOMER_AUTHENTICATION As String = "CustomerAuthentication"
    Private Const SOURCE_COL_CUSTOMER_AUTHENTICATION_TYPE As String = "CustomerAuthenticationType"
    Private Const SOURCE_COL_CONTACT_NUMBER As String = "ContactNumber"
    Private Const SOURCE_COL_CALLER_NAME As String = "CallerName"
    Private Const SOURCE_COL_EMAIL_ADDRESS As String = "EmailAddress"
    Private Const SOURCE_COL_SERIAL_NUMBER As String = "SerialNumber"
    Private Const SOURCE_COL_MAKE As String = "Make"
    Private Const SOURCE_COL_MODEL As String = "Model"
    Private Const SOURCE_COL_CAUSE_OF_LOSS_CODE As String = "CauseOfLossCode"
    Private Const SOURCE_COL_LOSS_DATE As String = "DateOfLoss"
    Private Const SOURCE_COL_COVERAGE_CODE As String = "CoverageCode"
    Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "ServiceCenterCode"
    Private Const SOURCE_COL_PROBLEM_DESCRIPTION As String = "ProblemDescription"
    Private Const SOURCE_COL_REPAIR_ESTIMATE As String = "RepairEstimate"
    Private Const SOURCE_COL_VISIT_DATE As String = "VisitDate"
    Private Const SOURCE_COL_CALLER_TAX_NUMBER As String = "CallerTaxNumber"
    Private Const SOURCE_COL_SPECIAL_INSTRUCTION As String = "SpecialInstruction"
    Private Const SOURCE_COL_INVOICE_DATE As String = "InvoiceDate"
    Private Const SOURCE_COL_PAYMENT_METHOD As String = "PaymentMethod"
    'ShippingInfo
    Private Const SOURCE_COL_ADDRESS1 As String = "address1"
    Private Const SOURCE_COL_ADDRESS2 As String = "address2"
    Private Const SOURCE_COL_CITY As String = "City"
    Private Const SOURCE_COL_REGION As String = "Region"
    Private Const SOURCE_COL_POSTAL_CODE As String = "PostalCode"
    Private Const SOURCE_COL_COUNTRY_CODE As String = "CountryCode"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As SubmitClaimDs)
        MyBase.New()
        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property CustomerIdentifier() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IdentifierType() As String
        Get
            If Row(Me.DATA_COL_NAME_IDENTIFIER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_IDENTIFIER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_IDENTIFIER_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ContactName() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CONTACT_NAME, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CustomerAuthentication() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CUSTOMER_AUTHENTICATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CUSTOMER_AUTHENTICATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CUSTOMER_AUTHENTICATION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CustomerAuthenticationType() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CUSTOMER_AUTHENTICATION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CUSTOMER_AUTHENTICATION_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CUSTOMER_AUTHENTICATION_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
        Public Property CallerName() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CALLER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CALLER_NAME, Value)
        End Set
    End Property

    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Make() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_MAKE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_MAKE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_MODEL, Value)
        End Set
    End Property

    Public Property CauseOfLossCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CAUSE_OF_LOSS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CAUSE_OF_LOSS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CAUSE_OF_LOSS_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property LossDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_LOSS_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_LOSS_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CoverageCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_COVERAGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_COVERAGE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_COVERAGE_CODE, Value)
        End Set
    End Property

    Public Property ServiceCenterCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property

    Public Property RepairEstimate() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REPAIR_ESTIMATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REPAIR_ESTIMATE), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_REPAIR_ESTIMATE, Value)
        End Set
    End Property

    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_VISIT_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_VISIT_DATE, Value)
        End Set
    End Property

    Public Property CallerTaxNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CALLER_TAX_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CALLER_TAX_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CALLER_TAX_NUMBER, Value)
        End Set
    End Property

    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_INVOICE_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property PaymentMethod() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PAYMENT_METHOD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_PAYMENT_METHOD, Value)
        End Set
    End Property

    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_ADDRESS1, Value)
        End Set
    End Property

    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_ADDRESS2, Value)
        End Set
    End Property

    Public Property City() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CITY, Value)
        End Set
    End Property

    Public Property Region() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_REGION, Value)
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property CountryCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_COUNTRY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_COUNTRY_CODE, Value)
        End Set
    End Property

#End Region

#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ByVal ds As SubmitClaimDs)
        Try
            If ds.SubmitClaim.Count = 0 Then Exit Sub
            With ds.SubmitClaim.Item(0)
                Me.CustomerIdentifier = .CustomerIdentifier
                Me.CustomerAuthentication = .CustomerAuthentication
                Me.IdentifierType = .IdentifierType
                Me.DealerCode = .DealerCode
                Me.ContactName = .ContactName
                Me.CustomerAuthentication = .CustomerAuthentication
                Me.CustomerAuthenticationType = .CustomerAuthenticationType                
                Me.CallerName = .CallerName
                If Not .IsSerialNumberNull Then Me.SerialNumber = .SerialNumber
                Me.Model = .Model
                Me.Make = .Make
                If Not .IsCauseOfLossCodeNull Then Me.CauseOfLossCode = .CauseOfLossCode

                Me.LossDate = .DateOfLoss
                Me.CoverageCode = .CoverageCode
                If Not .IsServiceCenterCodeNull Then Me.ServiceCenterCode = .ServiceCenterCode
                Me.ProblemDescription = .ProblemDescription

                If Not .IsRepairEstimateNull Then Me.RepairEstimate = .RepairEstimate
                If Not .IsVisitDateNull Then
                    Me.VisitDate = .VisitDate
                    isVisitDateNull = False
                End If

                If Not .IsCallerTaxNumberNull Then Me.CallerTaxNumber = .CallerTaxNumber
                If Not .IsSpecialInstructionNull Then Me.SpecialInstruction = .SpecialInstruction
                If Not .IsInvoiceDateNull Then
                    Me.InvoiceDate = .InvoiceDate
                    isInvoiceDateNull = False
                End If

                If Not .IsPaymentMethodNull Then Me.PaymentMethod = .PaymentMethod

                Dim i As Integer
                For i = 0 To ds.ShippingInfo.Count - 1
                    _shippingAddress = New Address
                    Me._isShippingInfoProvided = True
                    If Not ds.ShippingInfo(i).IsContactNumberNull Then _contactNumber = ds.ShippingInfo(i).ContactNumber
                    If Not ds.ShippingInfo(i).IsEmailAddressNull Then _emailAddress = ds.ShippingInfo(i).EmailAddress
                    If Not ds.ShippingInfo(i).IsAttentionToNull Then _attentionTo = ds.ShippingInfo(i).AttentionTo
                    If Not ds.ShippingInfo(i).Isaddress1Null Then _shippingAddress.Address1 = ds.ShippingInfo(i).address1
                    If Not ds.ShippingInfo(i).Isaddress2Null Then _shippingAddress.Address2 = ds.ShippingInfo(i).address2
                    If Not ds.ShippingInfo(i).IsCityNull Then _shippingAddress.City = ds.ShippingInfo(i).City
                    If Not ds.ShippingInfo(i).IsRegionCodeNull Then Me._regionCode = ds.ShippingInfo(i).RegionCode
                    If Not ds.ShippingInfo(i).IsPostalCodeNull Then _shippingAddress.PostalCode = ds.ShippingInfo(i).PostalCode
                    Me._shippingCountryCode = ds.ShippingInfo(i).CountryCode
                Next
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Dim row As DataRow
        Try
            Me.Validate()

            'Call Web Submit Claim PreValidation
            Dim oWebSubmitClaimPreValidateInputData As New ClaimDAL.WebSubmitClaimPreValidateInputData
            Dim oWebSubmitClaimPreValidateOutputData As New ClaimDAL.WebSubmitClaimPreValidateOutputData

            oWebSubmitClaimPreValidateInputData.CoverageCode = Me.CoverageCode
            oWebSubmitClaimPreValidateInputData.CustomerIdentifier = Me.CustomerIdentifier
            oWebSubmitClaimPreValidateInputData.DealerCode = Me.DealerCode
            oWebSubmitClaimPreValidateInputData.IdentifierType = Me.IdentifierType
            oWebSubmitClaimPreValidateInputData.ServiceCenterCode = Me.ServiceCenterCode
            oWebSubmitClaimPreValidateInputData.SystemUserId = ElitaPlusIdentity.Current.ActiveUser.Id
            oWebSubmitClaimPreValidateInputData.CauseOfLossCode = Me.CauseOfLossCode
            oWebSubmitClaimPreValidateInputData.CountryCode = Me._shippingCountryCode
            oWebSubmitClaimPreValidateInputData.RegionCode = Me._regionCode
            oWebSubmitClaimPreValidateInputData.PaymentMethod = Me.PaymentMethod
            oWebSubmitClaimPreValidateInputData.Make = Me.Make
            oWebSubmitClaimPreValidateInputData.Model = Me.Model
            oWebSubmitClaimPreValidateInputData.SerialNumber = Me.SerialNumber
            oWebSubmitClaimPreValidateInputData.DateOfLoss = Me.LossDate     '#26061
            If Me._isShippingInfoProvided Then oWebSubmitClaimPreValidateInputData.AddressTypeCode = Codes.ADDRESS_TYPE__SHIPPING

            Dim WebSubmitClaimPreValidatePreValidateDS As DataSet = Claim.WebSubmitClaimPreValidate(oWebSubmitClaimPreValidateInputData, oWebSubmitClaimPreValidateOutputData)

            If oWebSubmitClaimPreValidateOutputData.PreValidateErrorCode <> 0 Then ' There is a preValidation error
                Throw New BOValidationException("Submit Claim Error: ", oWebSubmitClaimPreValidateOutputData.PreValidateError)
            End If

            'Create the claim and prePopulate
            Dim claimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()

            claimBO.CertItemCoverageId = oWebSubmitClaimPreValidateOutputData.CertItemCoverageId
            claimBO.ServiceCenterId = oWebSubmitClaimPreValidateOutputData.ServiceCenterId

            If Not oWebSubmitClaimPreValidateOutputData.ServiceCenterId.Equals(Guid.Empty) Then
                claimBO.ServiceCenterId = oWebSubmitClaimPreValidateOutputData.ServiceCenterId
            End If

            If Not oWebSubmitClaimPreValidateOutputData.CauseOfLossId.Equals(Guid.Empty) Then
                claimBO.CauseOfLossId = oWebSubmitClaimPreValidateOutputData.CauseOfLossId
            Else
                claimBO.CauseOfLossId = claimBO.GetCauseOfLossID(oWebSubmitClaimPreValidateOutputData.CoverageTypeId)
            End If

            If Me._isShippingInfoProvided Then
                claimBO.AddContactInfo(Guid.Empty)
                claimBO.ContactInfo.Address.Address1 = Me._shippingAddress.Address1
                claimBO.ContactInfo.Address.Address2 = Me._shippingAddress.Address2
                claimBO.ContactInfo.Address.City = Me._shippingAddress.City
                claimBO.ContactInfo.Address.PostalCode = Me._shippingAddress.PostalCode
                claimBO.ContactInfo.Address.CountryId = oWebSubmitClaimPreValidateOutputData.CountryId
                claimBO.ContactInfo.Address.RegionId = oWebSubmitClaimPreValidateOutputData.RegionId

                claimBO.ContactInfo.AddressId = claimBO.ContactInfo.Address.Id
                claimBO.ContactInfo.SalutationId = Guid.Empty
                claimBO.ContactInfo.Name = Me._attentionTo
                claimBO.ContactInfo.Email = Me._emailAddress
                claimBO.ContactInfo.CellPhone = Me._contactNumber
                claimBO.ContactInfo.AddressTypeId = oWebSubmitClaimPreValidateOutputData.AddressTypeId

                claimBO.ContactInfo.Validate()
                claimBO.ContactInfo.Address.Validate()
                claimBO.ContactInfoId = claimBO.ContactInfo.Id
            End If

            claimBO.ClaimNumber = Nothing
            claimBO.PrePopulate(claimBO.ServiceCenterId, claimBO.CertItemCoverageId, Nothing, Me.LossDate, False, True, False, False, _
                                Me.CallerName, Me.ProblemDescription)
            claimBO.Deductible = New DecimalType(oWebSubmitClaimPreValidateOutputData.Deductible)
            '--------------------------------------------------------------------------------------
            'Code change for DEF-3463
            'AuthorizedAmount will be get calculated in PrePopulate method of Claim Business Object.
            'so no need to assign it explicitly to 0 (zero)
            '--------------------------------------------------------------------------------------
            'claimBO.AuthorizedAmount = New DecimalType(0D)
            claimBO.ContactName = Me.ContactName
            claimBO.CallerName = Me.CallerName
            claimBO.ProblemDescription = Me.ProblemDescription
            claimBO.SpecialInstruction = Me.SpecialInstruction
            claimBO.RepairEstimate = Me.RepairEstimate
            If isVisitDateNull = True Then
                claimBO.VisitDate = Nothing
            Else
                claimBO.VisitDate = Me.VisitDate
            End If
            claimBO.CallerTaxNumber = Me.CallerTaxNumber
            claimBO.StatusCode = oWebSubmitClaimPreValidateOutputData.DefaultClaimStatusCode 'Codes.CLAIM_STATUS__PENDING

            If isInvoiceDateNull = True Then
                claimBO.InvoiceDate = Nothing
            Else
                claimBO.InvoiceDate = Me.InvoiceDate
            End If

            If Not oWebSubmitClaimPreValidateOutputData.PaymentMethodId.Equals(Guid.Empty) Then
                claimBO.DedCollectionMethodID = oWebSubmitClaimPreValidateOutputData.PaymentMethodId
            End If

            ' Create new claim comment if Make and Model or Serial number (when provided) does not match what is on the enrollment record
            If (Not oWebSubmitClaimPreValidateOutputData.PreValidateMakeModelErrorCode = Nothing AndAlso oWebSubmitClaimPreValidateOutputData.PreValidateMakeModelErrorCode = 1142) _
                OrElse (Not oWebSubmitClaimPreValidateOutputData.PreValidateSerialNumberErrorCode = Nothing AndAlso oWebSubmitClaimPreValidateOutputData.PreValidateSerialNumberErrorCode = 1141) Then
                'Call the Create Comment Logic
                Dim oCommentBO As Comment = claimBO.AddNewComment()
                oCommentBO.Comments = "This web claim was established with a Make, Model, or IMEI that does not match the enrollment record. Please validate that the claimed Make, Model, and IMEI match the enrolled information prior to authorizing this claim."
                oCommentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__MAKE_MODEL_IMEI_MISMATCH)
                oCommentBO.Validate()
            End If

            claimBO.Validate()
            claimBO.Save()

            'Build the OK response with the claim number
            Dim ResponseDataSet As New DataSet(Me.DATASET_NAME__SUBMIT_CLAIM_RESPONSE)

            Dim ResponseStatus As DataTable = Me.BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_NO_ERROR), _
                                                                           Common.ErrorCodes.WS_NO_ERROR, _
                                                                           Codes.WEB_EXPERIENCE__NO_ERROR)

            Dim dtClaimInfo As DataTable = New DataTable(Me.TABLE_NAME__CLAIM_INFO)
            dtClaimInfo.Columns.Add(CLAIM_NUMBER, GetType(String))
            Dim rw As DataRow = dtClaimInfo.NewRow
            rw(0) = claimBO.ClaimNumber
            dtClaimInfo.Rows.Add(rw)
            ResponseDataSet.Tables.Add(dtClaimInfo)
            ResponseDataSet.Tables.Add(ResponseStatus)
            Return (XMLHelper.FromDatasetToXML(ResponseDataSet, Nothing, True, True, True, False, True))

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub MapDataSet(ByVal ds As SubmitClaimDs)

        Dim schema As String = ds.GetXmlSchema

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

    Private Sub Load(ByVal ds As SubmitClaimDs)
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
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region


End Class

