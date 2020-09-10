Imports System.Configuration
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.External.Darty
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.External.Interfaces.Darty
Imports ElitaHarvesterService.OutboundCommunication

Public Class SendClaimGiftCardEMailTask
    Inherits TaskBase

    Private Const GiftCardStatusSent As String = "SENT"
    Private Const GiftCardOperationCodeForVoucher = "VOUCHER"
    Private Const GiftCardOperationCodeForClaim = "CLAIM"


#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
        Logger.AddInfo("In SendClaimGiftCardEmailTask class ")
    End Sub

#End Region

#Region "Fields"
    Private _claimId As Guid
    Private _certificate As Certificate
    Private _certificateId As Guid
    Private _claimAuthorizationId As Guid
    Private _claimbase As ClaimBase
    Private _claimAuthorization As ClaimAuthorization
    Private _disbursement As Disbursement
    Private _giftCardRequestId As Guid

#End Region

#Region "Constants"

    Public Const DartyApplicationSource = "APPLICATIONSOURCE"
    Public Const DartyDomiciliation = "DOMICILIATION"
    Private Const AESCryptographyPassword = "AES_CRYPTOGRAPHY_PASSWORD"
    Private Const AESCryptographySalt = "AES_CRYPTOGRAPHY_SALT"
    Private Const AESCryptographyIterations = "AES_CRYPTOGRAPHY_ITERATIONS"

#End Region

#Region "Properties"
    Private Property oClaimId As Guid
        Get
            Return _claimId
        End Get
        Set(ByVal value As Guid)
            _claimId = value
        End Set
    End Property

    Private Property oCertificate As Certificate
        Get
            Return _certificate
        End Get
        Set(ByVal value As Certificate)
            _certificate = value
        End Set
    End Property

    Private Property oClaim As ClaimBase
        Get
            Return _claimbase
        End Get
        Set(ByVal value As ClaimBase)
            _claimbase = value
        End Set
    End Property

    Private Property oClaimAuthorization As ClaimAuthorization
        Get
            Return _claimAuthorization
        End Get
        Set(ByVal value As ClaimAuthorization)
            _claimAuthorization = value
        End Set
    End Property


    Private Property oDisbursement As Disbursement
        Get
            Return _disbursement
        End Get
        Set(ByVal value As Disbursement)
            _disbursement = value
        End Set
    End Property

#End Region

    Protected Friend Overrides Sub Execute()
        GenerateGiftCard()
    End Sub

    Private Sub GetGiftCardReference()

    End Sub

    Private Sub GenerateGiftCard()
        Dim dsm As IDartyServiceManager = New DartyServiceManager()
        Dim request As Request = Nothing
        Dim giftCadRequest As GenerateGiftCardRequest = Nothing
        Dim response As GenerateGiftCardResponse

        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.GIFT_CARD_REQUEST_ID))) Then
            _giftCardRequestId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.GIFT_CARD_REQUEST_ID)))
        End If

        'if the request is related to a Claim
        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_ID))) Then
            request = GenerateRequest(GiftCardRequestFor.Claim, GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_ID))))
            'if the request is related to a Authorization
        ElseIf (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_AUTHORIZATION_ID))) Then
            request = GenerateRequest(GiftCardRequestFor.ClaimAuthorization, GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_AUTHORIZATION_ID))))
            'if the request is related to a Policy
        ElseIf (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
            request = GenerateRequest(GiftCardRequestFor.Certificate, GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))))
        End If

        Logger.AddInfo("Application Source from app setting for Giftcard:" + ConfigurationManager.AppSettings(DartyApplicationSource))
        Logger.AddInfo("Amount from Arugment:" + PublishedTask.Argument(PublishedTask.GiftCardAmount))
        Logger.AddInfo("Giftcard Type from Argument:" + PublishedTask.Argument(PublishedTask.GiftCardType))
        Logger.AddInfo("Email for Giftcard from certificate:" + oCertificate.Email)
        Logger.AddInfo("Operation For:" + oCertificate.Email)

        Try
            Logger.AddInfo("Begin - calling Darty Giftcard Service")

            response = dsm.ActivateDartyGiftCard(request.GiftcardRequest)

            Logger.AddInfo("End - calling Darty Giftcard Service, response code from Darty :" + response.ReturnCode)
            Logger.AddInfo("End - calling Darty Giftcard Service, BarCode and Exp Date response from Darty :" + response.GiftCardBarCodeNumber + "-" + response.GiftCardSerialNumber + "-" + response.GiftCardExpirationDate.ToShortDateString())

            If (String.IsNullOrEmpty(response.ErrorCode)) Then

                If String.IsNullOrEmpty(response.GiftCardSerialNumber) Then
                    Logger.AddError("GiftCardSerialNumber is blank for the Reference Number and Dealer {0}:{1}:{2}:{3}" + request.GiftcardRequest.ReferenceNumber + ":" + oCertificate.Dealer.DealerName + ":" + response.ErrorCode + ":" + response.ErrorMessage)
                    Me.FailReason = "Gift Card Serial Number is blank"
                    Throw New Exception(FailReason)
                End If

                Logger.AddInfo("Begin - saving the gift card data in elp_gift_Card table")

                Try
                    With response
                        InsertGiftCardInfo(response, _giftCardRequestId)
                    End With
                Catch ex As Exception
                    Logger.AddError("Inserting Gift Card data Failed for the Reference Number and Dealer {0}:{1}:" + request.GiftcardRequest.ReferenceNumber + ":" + oCertificate.Dealer.DealerName, ex)
                End Try

                Dim errorMessage As String = String.Empty
                Dim arguments As String = String.Empty

                Logger.AddInfo("End - saved the gift card data in elp_gift_Card table")

                Logger.AddInfo("Begin - calling Exact Target Service for :" & request.EntityName)

                If (request.EntityName = Codes.ENTITY_NAME_CLAIM) Then
                    arguments = "ClaimId:" & DALBase.GuidToSQLString(oClaim.Id) & ";GiftCardRequestId:" & DALBase.GuidToSQLString(_giftCardRequestId) & ""
                    GeneratePublishedTaskEventForOC(arguments, Codes.EVNT_TYP_SEND_CLAIM_GIFT_CARD, oCertificate.DealerId)

                ElseIf request.EntityName = Codes.ENTITY_NAME_CERT Then
                    arguments = "CertificateId:" & DALBase.GuidToSQLString(oCertificate.Id) & ";GiftCardRequestId:" & DALBase.GuidToSQLString(_giftCardRequestId) & ""
                    GeneratePublishedTaskEventForOC(arguments, Codes.EVNT_TYP_SEND_CERT_VOUCHER, oCertificate.DealerId)
                End If

                Logger.AddInfo("End - calling Exact Target Service")

                Logger.AddInfo("Begin - Updating Gift Card Status")
                UpdateGiftCardStatus(_giftCardRequestId, GiftCardStatusSent)
                Logger.AddInfo("End - Updated Gift Card Status")

            Else
                Logger.AddError("Darty Service Failed for the Reference Number and Dealer {0}:{1}:{2}" + request.GiftcardRequest.ReferenceNumber + ":" + oCertificate.Dealer.DealerName + ":" + response.ErrorMessage)
                Me.FailReason = "Error Received: " + response.ErrorCode
                Throw New Exception(FailReason)
            End If
        Catch ex As Exception
            'Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "Claim Gift Card Failed for the Reference Number and Dealer {0}:{1}:" + request.ReferenceNumber + ":" + oCertificate.Dealer.DealerName)
            Logger.AddError("Claim Gift Card Failed for the Reference Number and Dealer {0}:{1}:" + request.GiftcardRequest.ReferenceNumber + ":" + oCertificate.Dealer.DealerName, ex)
            Throw
        End Try
    End Sub

    Private Function GenerateRequest(requestFor As GiftCardRequestFor, entityId As Guid) As Request
        Dim request As Request = Nothing

        If (requestFor = GiftCardRequestFor.Certificate) Then
            request = GenerateRequestForCertificate(entityId)
        ElseIf (requestFor = GiftCardRequestFor.Claim) Then
            request = GenerateRequestForClaim(entityId)
        ElseIf (requestFor = GiftCardRequestFor.ClaimAuthorization) Then
            request = GenerateRequestForClaimAuthorization(entityId)
        End If

        Return request

    End Function

    Private Enum GiftCardRequestFor
        Certificate = 0

        Claim = 1

        ClaimAuthorization = 2

    End Enum

    Private Function GenerateRequestForCertificate(CertificateId As Guid) As Request

        oCertificate = New Certificate(CertificateId)
        Dim request As New Request

        Dim giftCardRequest As GenerateGiftCardRequest = PopulateCustomerAndGiftCardStaticInfo(oCertificate)
        giftCardRequest.ReferenceNumber = oCertificate.CertNumber
        giftCardRequest.OperationFor = GiftCardOperationCodeForVoucher

        request.GiftcardRequest = giftCardRequest
        request.EntityName = Codes.ENTITY_NAME_CERT
        request.EntityId = CertificateId
        Logger.AddInfo("Task found for Certificate: " + oCertificate.CertNumber + " and Dealer:" + oCertificate.Dealer.Dealer)
        Return request

    End Function

    Private Function GenerateRequestForClaim(ClaimId As Guid) As Request

        oClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId)
        oCertificate = oClaim.Certificate
        Dim request As New Request

        Dim giftCardRequest As GenerateGiftCardRequest = PopulateCustomerAndGiftCardStaticInfo(oCertificate)
        giftCardRequest.ReferenceNumber = oClaim.ClaimNumber
        giftCardRequest.OperationFor = GiftCardOperationCodeForClaim

        request.GiftcardRequest = giftCardRequest
        request.EntityName = Codes.ENTITY_NAME_CLAIM
        request.EntityId = ClaimId
        Logger.AddInfo("Task found for Claim: " + oClaim.ClaimNumber + ":" + oClaim.Dealer.DealerName)
        Return request

    End Function

    Private Function GenerateRequestForClaimAuthorization(ClaimAuthorizationId As Guid) As Request

        oClaimAuthorization = New ClaimAuthorization(ClaimAuthorizationId)
        oClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(oClaimAuthorization.ClaimId)
        oCertificate = oClaim.Certificate
        Dim request As New Request

        Dim giftCardRequest As GenerateGiftCardRequest = PopulateCustomerAndGiftCardStaticInfo(oCertificate)
        giftCardRequest.ReferenceNumber = oClaimAuthorization.AuthorizationNumber
        giftCardRequest.OperationFor = GiftCardOperationCodeForClaim

        request.GiftcardRequest = giftCardRequest
        request.EntityName = Codes.ENTITY_NAME_CLAIM
        request.EntityId = ClaimAuthorizationId
        Logger.AddInfo("Task found for Claim: " + oClaimAuthorization.Claim.ClaimNumber + " and Auth Number :" + oClaimAuthorization.AuthorizationNumber)
        Return request

    End Function

    Private Function PopulateCustomerAndGiftCardStaticInfo(ByVal certificate As Certificate) As GenerateGiftCardRequest

        Dim request As New GenerateGiftCardRequest With {
            .FirstName = certificate.CustomerFirstName,
            .LastName = certificate.CustomerLastName,
            .Email = certificate.Email,
            .PhoneNumber = If(Not String.IsNullOrWhiteSpace(certificate.HomePhone), certificate.HomePhone.Replace(" ", ""), String.Empty),
            .ZipCode = certificate.MailingAddress.PostalCode,
            .ApplicationSource = ConfigurationManager.AppSettings(DartyApplicationSource),
            .GiftCardType = PublishedTask.Argument(PublishedTask.GiftCardType),
            .Domiciliation = ConfigurationManager.AppSettings(DartyDomiciliation),
            .Amount = PublishedTask.Argument(PublishedTask.GiftCardAmount)
        }

        Return request

    End Function

    Private Sub InsertGiftCardInfo(response As GenerateGiftCardResponse, giftCardRequestId As Guid)
        Try
            Dim encryptedGiftcardLink As String = GenerateEncryptedGiftCardLink(response, oCertificate, oClaim)

            PublishedTask.InsertGiftCardInfo(response.GiftCardBarCodeNumber,
                                             response.GiftCardSerialNumber,
                                             response.CodePin1,
                                             response.CodePin2,
                                             response.GiftCardExpirationDate,
                                             giftCardRequestId,
                                             encryptedGiftcardLink)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub UpdateGiftCardStatus(giftCardRequestId As Guid, status As String)
        Try
            PublishedTask.UpdateGiftCardStatus(giftCardRequestId, status)
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub SendClaimGiftCardDataToExactTarget(ByVal certificateId As Guid, ByVal dartyResponse As GenerateGiftCardResponse, entityId As Guid)
    '    Logger.AddDebugLogEnter()
    '    Dim obc As OutboundCommunication.CommunicationClient
    '    Dim oWebPasswd As WebPasswd
    '    Dim strRejectMsg As String
    '    Dim Status As String
    '    Dim strXML As New StringWriter

    '    Try

    '        If (Not certificateId.Equals(Guid.Empty)) Then
    '            oCertificate = New Certificate(certificateId)
    '            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

    '            '            Dim encryptedGiftcardLink As String = GenerateEncryptedGiftCardLink(dartyResponse, oCertificate, oClaim)

    '            Dim oCompany As New Company(oCertificate.CompanyId)
    '            Dim oCountry As New Country(oCompany.BusinessCountryId)
    '            Dim oLanguage As New Language(oCountry.LanguageId)

    '            obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
    '            obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
    '            obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

    '            Dim encryptedGiftcardLink As String = GenerateEncryptedGiftCardLink(dartyResponse, oCertificate, oClaim)

    '            Dim request = New OutboundCommunication.ExactTargetRequest()
    '            request.Attributes = New NameValue(11) {}
    '            request.Attributes.SetValue(New NameValue() With {.Name = "EmailAddress", .Value = If(String.IsNullOrEmpty(oCertificate.Email), String.Empty, oCertificate.Email.Trim())}, 0)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "LastName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerLastName), String.Empty, oCertificate.CustomerLastName.Trim())}, 1)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "FirstName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerFirstName), String.Empty, oCertificate.CustomerFirstName.Trim())}, 2)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "Language", .Value = oLanguage.Code}, 3)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "PolicyNumber", .Value = If(String.IsNullOrEmpty(oCertificate.CertNumber), String.Empty, oCertificate.CertNumber)}, 4)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "InvoiceNo", .Value = If(String.IsNullOrEmpty(oCertificate.InvoiceNumber), String.Empty, oCertificate.InvoiceNumber)}, 5)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "ProductCode", .Value = If(String.IsNullOrEmpty(oCertificate.ProductCode), String.Empty, oCertificate.ProductCode)}, 6)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "ProductDescription", .Value = If(String.IsNullOrEmpty(oCertificate.GetProdCodeDesc), String.Empty, oCertificate.GetProdCodeDesc)}, 7)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "SerialNo", .Value = If(String.IsNullOrEmpty(dartyResponse.GiftCardSerialNumber), String.Empty, dartyResponse.GiftCardSerialNumber)}, 8)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardLink", .Value = encryptedGiftcardLink}, 9)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardAmount", .Value = dartyResponse.Amount}, 10)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "ClaimsNo", .Value = dartyResponse.ClaimNumber}, 11)

    '            Logger.AddInfo("Encrypted Gift card link:" + encryptedGiftcardLink)

    '            'giftCardEncryptedLink = encryptedGiftcardLink

    '            request.ExactTargetPassword = ConfigurationManager.AppSettings(ExactTargetPassword)
    '            request.ExactTargetUserName = ConfigurationManager.AppSettings(ExactTargetUserName)
    '            request.Email = oCertificate.Email
    '            request.FromEmail = String.Empty
    '            request.FromName = String.Empty
    '            request.TriggerKey = ConfigurationManager.AppSettings(Clm_Gift_Card_TriggerKey)
    '            Dim response = obc.SendExactTarget(request)

    '            If response.Id <> Guid.Empty Then

    '                If response.SendSuccessful = True Then
    '                    Status = "SUCCESS"
    '                Else
    '                    Status = "FAILURE"
    '                End If

    '                Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
    '                eb.Serialize(strXML, response)

    '                Try
    '                    Dim objTrans As New CustCommunication
    '                    With objTrans
    '                        .CustomerId = oCertificate.CustomerId
    '                        .EntityName = Codes.ENTITY_NAME_CLAIM
    '                        .EntityId = entityId
    '                        .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
    '                        .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
    '                        .CommunicationFormat = Codes.TASK_SEND_CLM_GIFTCARD_EMAIL
    '                        .CustContactId = Guid.Empty
    '                        .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
    '                        .ComponentRefId = PublishedTask.Id
    '                        .CommunicationStatus = Status
    '                        .CommResponseId = response.Id
    '                        .CommResponseXml = strXML.ToString()
    '                        .IsRetryable = Codes.YESNO_Y
    '                        .RetryCompoReference = Codes.EVNT_TYP__CLM_RESEND_REIMBURSE_INFO
    '                        .Save()
    '                    End With
    '                Catch ex As Exception
    '                    strRejectMsg = ex.Message
    '                End Try
    '            End If
    '        End If

    '        Logger.AddDebugLogExit()
    '    Catch ex As Exception
    '        ' Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "OC Gift Card Failed for the claim and Dealer {0}:{1}:" + oClaim.ClaimNumber + ":" + oClaim.Dealer.DealerName)
    '        Logger.AddError("OC Gift Card Failed for the claim and Dealer {0}:{1}:" + oClaim.ClaimNumber + ":" + oClaim.Dealer.DealerName, ex)
    '        Throw
    '    End Try
    'End Sub
    'Private Sub SendClaimGiftCardDataWithHardCodeEmailToExactTarget(ByVal certificateId As Guid, ByVal dartyResponse As GenerateGiftCardResponse, entityId As Guid)
    '    Logger.AddDebugLogEnter()
    '    Dim obc As OutboundCommunication.CommunicationClient
    '    Dim oWebPasswd As WebPasswd
    '    Dim strRejectMsg As String
    '    Dim Status As String
    '    Dim strXML As New StringWriter

    '    Try

    '        If (Not certificateId.Equals(Guid.Empty)) Then

    '            oCertificate = New Certificate(certificateId)

    '            Dim dealerCode As String
    '            dealerCode = oCertificate.Dealer.Dealer.ToUpper

    '            If dealerCode = "DTGM" OrElse dealerCode = "DTMM" OrElse dealerCode = "DTMS" Then

    '                oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

    '                Dim emailAddress As String
    '                Dim env As String
    '                env = oWebPasswd.Env.ToUpper
    '                If env = "MODL" Then
    '                    emailAddress = "anne.delaulanie@assurant.com"
    '                ElseIf env = "PROD" Then
    '                    emailAddress = "suivi.cartes-cadeaux@cwisas.com"
    '                Else
    '                    Exit Sub
    '                End If

    '                Dim oCompany As New Company(oCertificate.CompanyId)
    '                Dim oCountry As New Country(oCompany.BusinessCountryId)
    '                Dim oLanguage As New Language(oCountry.LanguageId)

    '                obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
    '                obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
    '                obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

    '                Logger.AddInfo("Encrypted Gift card link:" + encryptedGiftcardLink)

    '                'giftCardEncryptedLink = encryptedGiftcardLink

    '                request.ExactTargetPassword = ConfigurationManager.AppSettings(ExactTargetPassword)
    '                request.ExactTargetUserName = ConfigurationManager.AppSettings(ExactTargetUserName)
    '                request.Email = emailAddress
    '                request.FromEmail = String.Empty
    '                request.FromName = String.Empty
    '                request.TriggerKey = ConfigurationManager.AppSettings(Clm_Gift_Card_TriggerKey)
    '                Dim response = obc.SendExactTarget(request)

    '                If response.Id <> Guid.Empty Then

    '                    If response.SendSuccessful = True Then
    '                        Status = "SUCCESS"

    '                    Else
    '                        Status = "FAILURE"
    '                    End If

    '                    Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
    '                    eb.Serialize(strXML, response)

    '                    Try
    '                        Dim objTrans As New CustCommunication
    '                        With objTrans
    '                            .CustomerId = oCertificate.CustomerId
    '                            .EntityName = Codes.ENTITY_NAME_CLAIM
    '                            .EntityId = entityId
    '                            .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
    '                            .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
    '                            .CommunicationFormat = Codes.TASK_SEND_CLM_GIFTCARD_EMAIL
    '                            .CustContactId = Guid.Empty
    '                            .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
    '                            .ComponentRefId = PublishedTask.Id
    '                            .CommunicationStatus = Status
    '                            .CommResponseId = response.Id
    '                            .CommResponseXml = strXML.ToString()
    '                            .IsRetryable = Codes.YESNO_Y
    '                            .RetryCompoReference = Codes.EVNT_TYP__CLM_RESEND_REIMBURSE_INFO
    '                            .Save()
    '                        End With
    '                    Catch ex As Exception
    '                        strRejectMsg = ex.Message
    '                    End Try
    '                End If
    '            End If
    '        End If

    '        Logger.AddDebugLogExit()
    '    Catch ex As Exception
    '        'Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "OC Gift Card Failed for the claim and Dealer {0}:{1}:" + oClaim.ClaimNumber + ":" + oClaim.Dealer.DealerName)
    '        Return ex.Message
    '    End Try
    'End Function

    'Private Function SendCertificateVoucherDataWithHardCodeEmailToExactTarget(ByVal certificateId As Guid, ByVal dartyResponse As GenerateGiftCardResponse, entityId As Guid) As String
    '    Logger.AddDebugLogEnter()
    '    Dim obc As OutboundCommunication.CommunicationClient
    '    Dim oWebPasswd As WebPasswd
    '    Dim strRejectMsg As String
    '    Dim Status As String
    '    Dim strXML As New StringWriter

    '    Try

    '        If (Not certificateId.Equals(Guid.Empty)) Then

    '            oCertificate = New Certificate(certificateId)

    '            Dim dealerCode As String
    '            dealerCode = oCertificate.Dealer.Dealer.ToUpper

    '            If dealerCode = "DTGM" OrElse dealerCode = "DTMM" OrElse dealerCode = "DTMS" Then

    '                oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

    '                Dim emailAddress As String
    '                Dim env As String
    '                env = oWebPasswd.Env.ToUpper
    '                If env = "MODL" Then
    '                    emailAddress = "anne.delaulanie@assurant.com"
    '                ElseIf env = "PROD" Then
    '                    emailAddress = "suivi.cartes-cadeaux@cwisas.com"
    '                Else
    '                    Return Nothing
    '                End If

    '                Dim oCompany As New Company(oCertificate.CompanyId)
    '                Dim oCountry As New Country(oCompany.BusinessCountryId)
    '                Dim oLanguage As New Language(oCountry.LanguageId)

    '                obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
    '                obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
    '                obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

    '                Dim encryptedGiftcardLink As String = GenerateEncryptedGiftCardLink(dartyResponse, oCertificate, oClaim)

    '                Dim request = New OutboundCommunication.ExactTargetRequest()
    '                request.Attributes = New NameValue(11) {}
    '                request.Attributes.SetValue(New NameValue() With {.Name = "SubscriberKey", .Value = If(String.IsNullOrEmpty(oCertificate.Email), String.Empty, oCertificate.Email.Trim())}, 0)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "EmailAddress", .Value = If(String.IsNullOrEmpty(emailAddress), String.Empty, emailAddress.Trim())}, 1)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "FirstName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerFirstName), String.Empty, oCertificate.CustomerFirstName.Trim())}, 2)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "LastName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerLastName), String.Empty, oCertificate.CustomerLastName.Trim())}, 3)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "Language", .Value = oLanguage.Code}, 4)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "PolicyNumber", .Value = If(String.IsNullOrEmpty(oCertificate.CertNumber), String.Empty, oCertificate.CertNumber)}, 5)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "ProductCode", .Value = If(String.IsNullOrEmpty(oCertificate.ProductCode), String.Empty, oCertificate.ProductCode)}, 6)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "ProductDescription", .Value = If(String.IsNullOrEmpty(oCertificate.GetProdCodeDesc), String.Empty, oCertificate.GetProdCodeDesc)}, 7)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "CurrentDate", .Value = If(String.IsNullOrEmpty(dartyResponse.GiftCardSerialNumber), String.Empty, DateTime.Now.ToShortDateString())}, 8)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardLink", .Value = encryptedGiftcardLink}, 9)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardAmount", .Value = dartyResponse.Amount}, 10)
    '                request.Attributes.SetValue(New NameValue() With {.Name = "InvoiceNo", .Value = If(String.IsNullOrEmpty(oCertificate.InvoiceNumber), String.Empty, oCertificate.InvoiceNumber)}, 11)

    '                Logger.AddInfo("Encrypted Gift card link:" + encryptedGiftcardLink)

    '                'giftCardEncryptedLink = encryptedGiftcardLink

    '                request.ExactTargetPassword = ConfigurationManager.AppSettings(ExactTargetPassword)
    '                request.ExactTargetUserName = ConfigurationManager.AppSettings(ExactTargetUserName)
    '                request.Email = emailAddress
    '                request.FromEmail = String.Empty
    '                request.FromName = String.Empty
    '                request.TriggerKey = ConfigurationManager.AppSettings(Cert_Voucher_TriggerKey)
    '                Dim response = obc.SendExactTarget(request)

    '                If response.Id <> Guid.Empty Then

    '                    If response.SendSuccessful = True Then
    '                        Status = "SUCCESS"
    '                    Else
    '                        Status = "FAILURE"
    '                    End If

    '                    Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
    '                    eb.Serialize(strXML, response)

    '                    Try
    '                        Dim objTrans As New CustCommunication
    '                        With objTrans
    '                            .CustomerId = oCertificate.CustomerId
    '                            .EntityName = Codes.ENTITY_NAME_CERT
    '                            .EntityId = entityId
    '                            .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
    '                            .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
    '                            .CommunicationFormat = Codes.TASK_SEND_CLM_GIFTCARD_EMAIL
    '                            .CustContactId = Guid.Empty
    '                            .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
    '                            .ComponentRefId = PublishedTask.Id
    '                            .CommunicationStatus = Status
    '                            .CommResponseId = response.Id
    '                            .CommResponseXml = strXML.ToString()
    '                            .IsRetryable = Codes.YESNO_Y
    '                            .RetryCompoReference = Codes.EVNT_TYP__CLM_RESEND_REIMBURSE_INFO
    '                            .Save()
    '                        End With
    '                    Catch ex As Exception
    '                        strRejectMsg = ex.Message
    '                    End Try
    '                End If
    '            End If
    '        End If

    '        Logger.AddDebugLogExit()
    '    Catch ex As Exception
    '        'Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "SendCertificateVoucherDataWithHardCodeEmailToExactTarget - OC Voucher Failed for the Certificate and Dealer {0}:{1}:" + oCertificate.CertNumber + ":" + oCertificate.Dealer.Dealer)
    '        Return ex.Message
    '    End Try
    'End Function

    'Private Function SendCertificateVoucherDataToExactTarget(ByVal certificateId As Guid, ByVal dartyResponse As GenerateGiftCardResponse, entityId As Guid) As String
    '    Logger.AddDebugLogEnter()
    '    Dim obc As OutboundCommunication.CommunicationClient
    '    Dim oWebPasswd As WebPasswd
    '    Dim strRejectMsg As String
    '    Dim Status As String
    '    Dim strXML As New StringWriter

    '    Try

    '        If (Not certificateId.Equals(Guid.Empty)) Then
    '            oCertificate = New Certificate(certificateId)
    '            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)


    '            Dim oCompany As New Company(oCertificate.CompanyId)
    '            Dim oCountry As New Country(oCompany.BusinessCountryId)
    '            Dim oLanguage As New Language(oCountry.LanguageId)

    '            obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
    '            obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
    '            obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

    '            Dim encryptedGiftcardLink As String = GenerateEncryptedGiftCardLink(dartyResponse, oCertificate, oClaim)

    '            Dim request = New OutboundCommunication.ExactTargetRequest()
    '            request.Attributes = New NameValue(11) {}
    '            request.Attributes.SetValue(New NameValue() With {.Name = "SubscriberKey", .Value = If(String.IsNullOrEmpty(oCertificate.Email), String.Empty, oCertificate.Email.Trim())}, 0)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "EmailAddress", .Value = If(String.IsNullOrEmpty(oCertificate.Email), String.Empty, oCertificate.Email.Trim())}, 1)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "FirstName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerFirstName), String.Empty, oCertificate.CustomerFirstName.Trim())}, 2)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "LastName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerLastName), String.Empty, oCertificate.CustomerLastName.Trim())}, 3)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "Language", .Value = oLanguage.Code}, 4)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "PolicyNumber", .Value = If(String.IsNullOrEmpty(oCertificate.CertNumber), String.Empty, oCertificate.CertNumber)}, 5)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "ProductCode", .Value = If(String.IsNullOrEmpty(oCertificate.ProductCode), String.Empty, oCertificate.ProductCode)}, 6)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "ProductDescription", .Value = If(String.IsNullOrEmpty(oCertificate.GetProdCodeDesc), String.Empty, oCertificate.GetProdCodeDesc)}, 7)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "CurrentDate", .Value = If(String.IsNullOrEmpty(dartyResponse.GiftCardSerialNumber), String.Empty, DateTime.Now.ToShortDateString())}, 8)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardLink", .Value = encryptedGiftcardLink}, 9)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "GiftCardAmount", .Value = dartyResponse.Amount}, 10)
    '            request.Attributes.SetValue(New NameValue() With {.Name = "InvoiceNo", .Value = If(String.IsNullOrEmpty(oCertificate.InvoiceNumber), String.Empty, oCertificate.InvoiceNumber)}, 11)

    '            Logger.AddInfo("Encrypted Gift card link:" + encryptedGiftcardLink)

    '            'giftCardEncryptedLink = encryptedGiftcardLink

    '            request.ExactTargetPassword = ConfigurationManager.AppSettings(ExactTargetPassword)
    '            request.ExactTargetUserName = ConfigurationManager.AppSettings(ExactTargetUserName)
    '            request.Email = oCertificate.Email
    '            request.FromEmail = String.Empty
    '            request.FromName = String.Empty
    '            request.TriggerKey = ConfigurationManager.AppSettings(Cert_Voucher_TriggerKey)
    '            Dim response = obc.SendExactTarget(request)

    '            If response.Id <> Guid.Empty Then

    '                If response.SendSuccessful = True Then
    '                    Status = "SUCCESS"
    '                Else
    '                    Status = "FAILURE"
    '                End If

    '                Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
    '                eb.Serialize(strXML, response)

    '                Try
    '                    Dim objTrans As New CustCommunication
    '                    With objTrans
    '                        .CustomerId = oCertificate.CustomerId
    '                        .EntityName = Codes.ENTITY_NAME_CERT
    '                        .EntityId = entityId
    '                        .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
    '                        .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
    '                        .CommunicationFormat = Codes.TASK_SEND_CLM_GIFTCARD_EMAIL
    '                        .CustContactId = Guid.Empty
    '                        .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
    '                        .ComponentRefId = PublishedTask.Id
    '                        .CommunicationStatus = Status
    '                        .CommResponseId = response.Id
    '                        .CommResponseXml = strXML.ToString()
    '                        .IsRetryable = Codes.YESNO_Y
    '                        .RetryCompoReference = Codes.EVNT_TYP__CLM_RESEND_REIMBURSE_INFO
    '                        .Save()
    '                    End With
    '                Catch ex As Exception
    '                    strRejectMsg = ex.Message
    '                End Try
    '            End If
    '        End If

    '        Logger.AddDebugLogExit()
    '    Catch ex As Exception
    '        'Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "OC Voucher Failed for the Certificate and Dealer {0}:{1}:" + oCertificate.CertNumber + ":" + oCertificate.Dealer.Dealer)
    '        Return ex.Message
    '    End Try
    'End Function

    Private Function GenerateEncryptedGiftCardLink(ByVal response As GenerateGiftCardResponse, ByVal certificate As Certificate, ByVal claim As ClaimBase) As String

        Dim giftcardData As String = String.Empty
        Dim encryptionSource As String = String.Empty
        Dim customerAddress As Assurant.ElitaPlus.BusinessObjectsNew.Address = Nothing
        Try


            If (Not response Is Nothing) Then
                If (Not certificate.AddressId.Equals(Guid.Empty)) Then
                    customerAddress = New Assurant.ElitaPlus.BusinessObjectsNew.Address(certificate.AddressId)
                End If

                With response
                    encryptionSource = "FN:" + certificate.CustomerFirstName + "|"
                    encryptionSource = encryptionSource + "LN:" + certificate.CustomerLastName + "|"
                    encryptionSource = encryptionSource + "Add1:" + customerAddress.Address1 + "|"
                    encryptionSource = encryptionSource + "Add2:" + customerAddress.Address2 + "|"
                    encryptionSource = encryptionSource + "City:" + customerAddress.City + "|"
                    encryptionSource = encryptionSource + "Zip:" + customerAddress.PostalCode + "|"
                    encryptionSource = encryptionSource + "BarCode:" + .GiftCardBarCodeNumber + "|"
                    encryptionSource = encryptionSource + "Pin1:" + .CodePin1 + "|"
                    encryptionSource = encryptionSource + "Pin2:" + .CodePin2 + "|"
                    encryptionSource = encryptionSource + "Amount:" + .Amount + "|"
                    encryptionSource = encryptionSource + "Exp:" + .GiftCardExpirationDate.ToString("MM/dd/yyyy") + "|"
                    encryptionSource = encryptionSource + "SNO:" + .GiftCardSerialNumber + "|"
                    encryptionSource = encryptionSource + "Created:" + DateTime.UtcNow.ToString("MM/dd/yyyy") + "|"
                    encryptionSource = encryptionSource + "ClaimNo:" + If(claim Is Nothing, oCertificate.CertNumber, claim.ClaimNumber) + "|"
                    If (Not claim Is Nothing AndAlso Not claim.ClaimedEquipment Is Nothing) Then
                        encryptionSource = encryptionSource + "Make:" + If(Not String.IsNullOrEmpty(claim.ClaimedEquipment.Manufacturer), claim.ClaimedEquipment.Manufacturer, String.Empty) + "|"
                        encryptionSource = encryptionSource + "Model:" + claim.ClaimedEquipment.Model + "|"
                    End If

                End With

                giftcardData = AES_Encrypt(encryptionSource)
            End If

            Return giftcardData

        Catch ex As Exception
            'Throw New DataBaseAccessException(ErrorTypes.ERROR_GENERAL, ex, "OC Gift Card Encryption Failed for the claim and Dealer {0}:{1}:" + If(claim Is Nothing, oCertificate.CertNumber, claim.ClaimNumber) + ":" + oCertificate.Dealer.DealerName)
            Logger.AddError("OC Gift Card Encryption Failed for the claim and Dealer {0}:{1}:" + If(claim Is Nothing, oCertificate.CertNumber, claim.ClaimNumber) + ":" + oCertificate.Dealer.DealerName, ex)
            Throw
        End Try
    End Function

    Public Function AES_Encrypt(input As String) As String

        Dim bytesToBeEncrypted As Byte() = Nothing
        Dim encryptedBytes As Byte() = Nothing
        Dim passwordBytes As Byte() = Nothing
        Dim encryptedString As String = String.Empty
        Dim passwordString As String = String.Empty
        Dim saltString As String = String.Empty
        Dim encryptIterations As Integer = 0
        Dim saltBytes As Byte() = Nothing


        passwordString = ConfigurationManager.AppSettings(AESCryptographyPassword)
        saltString = ConfigurationManager.AppSettings(AESCryptographySalt)
        encryptIterations = CInt(ConfigurationManager.AppSettings(AESCryptographyIterations))

        If (Not String.IsNullOrEmpty(input)) Then
            bytesToBeEncrypted = Encoding.UTF8.GetBytes(input)
            passwordBytes = Encoding.UTF8.GetBytes(passwordString)
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes)

            ' Set your salt here, change it to meet your flavor:
            saltBytes = Encoding.UTF8.GetBytes(saltString)

            Using ms As New MemoryStream()
                Using AES As Aes = Aes.Create()

                    Dim key = New Rfc2898DeriveBytes(passwordBytes, saltBytes, encryptIterations) '//"http://stackoverflow.com/questions/21145982/rfc2898derivebytes-iterationcount-purpose-and-best-practices"

                    AES.Key = key.GetBytes(AES.KeySize / 8)
                    AES.IV = key.GetBytes(AES.BlockSize / 8)

                    AES.Mode = CipherMode.CBC

                    Using cs = New CryptoStream(ms, AES.CreateEncryptor(AES.Key, AES.IV), CryptoStreamMode.Write)
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length)
                        cs.Close()
                    End Using
                    encryptedBytes = ms.ToArray()
                    encryptedString = Convert.ToBase64String(encryptedBytes)
                End Using
            End Using

        End If

        Return encryptedString

    End Function

    Private Function Decrypt(Input As String) As String
        Dim decryptedBytes As Byte() = Nothing
        Dim decryptedString As String = String.Empty
        Dim saltBytes As Byte() = Nothing
        Dim bytesToBeDecrypted As Byte() = Nothing
        Dim passwordBytes As Byte() = Nothing
        Dim saltString As String = String.Empty
        Dim passwordString As String = String.Empty
        Dim decryptIterations As Integer = 0

        passwordString = ConfigurationManager.AppSettings(AESCryptographyPassword)
        saltString = ConfigurationManager.AppSettings(AESCryptographySalt)
        decryptIterations = CInt(ConfigurationManager.AppSettings(AESCryptographyIterations))

        If (Not String.IsNullOrEmpty(Input)) Then
            passwordBytes = Encoding.UTF8.GetBytes(passwordString)

            bytesToBeDecrypted = Convert.FromBase64String(Input)

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes)

            ' Set your salt here, change it to meet your flavor:
            ' The salt bytes must be at least 8 bytes.

            saltBytes = Encoding.UTF8.GetBytes(saltString)

            Using ms As New MemoryStream()
                Using AES As Aes = Aes.Create()

                    Dim key = New Rfc2898DeriveBytes(passwordBytes, saltBytes, 100000)
                    AES.Key = key.GetBytes(AES.KeySize / 8)
                    AES.IV = key.GetBytes(AES.BlockSize / 8)

                    AES.Mode = CipherMode.CBC

                    Using cs = New CryptoStream(ms, AES.CreateDecryptor(AES.Key, AES.IV), CryptoStreamMode.Write)
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length)
                        cs.Close()
                    End Using
                    decryptedBytes = ms.ToArray()
                    decryptedString = Encoding.UTF8.GetString(decryptedBytes)
                End Using
            End Using

        End If

        Return decryptedString

    End Function

    Private Sub GeneratePublishedTaskEventForOC(arguments As String, TriggerCode As String, dealerId As Guid)

        'argumentsToAddEvent = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimNumber:" & .ClaimNumber & ""
        PublishedTask.AddEvent(companyGroupId:=Guid.Empty,
                                companyId:=Guid.Empty,
                                countryId:=Guid.Empty,
                                dealerId:=dealerId,
                                productCode:=String.Empty,
                                coverageTypeId:=Guid.Empty,
                                sender:="Gift Card Task",
                                arguments:=arguments,
                                eventDate:=DateTime.UtcNow,
                                eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, TriggerCode),
                                eventArgumentId:=Nothing)
    End Sub
End Class
Public Class Request
    Public Property GiftcardRequest As GenerateGiftCardRequest
    Public Property EntityName As String

    Public Property EntityId As Guid
End Class
