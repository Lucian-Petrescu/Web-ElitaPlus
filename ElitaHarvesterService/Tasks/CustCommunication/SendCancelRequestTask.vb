Imports System.Text
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Configuration

Public Class SendCancelRequestTask
    Inherits TaskBase

#Region "Fields"
    Private _certCancelReqId As Guid
    Private _oCertCancelRequest As CertCancelRequest
    Private _certificateId As Guid
    Private _oCertificate As Certificate

#End Region

#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Properties"
    Private Property CertCancelReqId As Guid
        Get
            Return _certCancelReqId
        End Get
        Set(ByVal value As Guid)
            _certCancelReqId = value
        End Set
    End Property

    Private Property oCertCancelRequest As CertCancelRequest
        Get
            Return _oCertCancelRequest
        End Get
        Set(ByVal value As CertCancelRequest)
            _oCertCancelRequest = value
        End Set
    End Property

    Private Property CertificateId As Guid
        Get
            Return _certificateId
        End Get
        Set(ByVal value As Guid)
            _certificateId = value
        End Set
    End Property

    Private Property oCertificate As Certificate
        Get
            Return _oCertificate
        End Get
        Set(ByVal value As Certificate)
            _oCertificate = value
        End Set
    End Property

    Private Const UserName = "OUTBOUND_COMM_USERNAME"
    Private Const Password = "OUTBOUND_COMM_PASSWORD"
    Private Const TriggerKey = "OUTBOUND_COMM_CANCEL_TRIGGER_KEY"
    Private Const ExactTargetUserName = "OUTBOUND_EXACTTARGETUSERNAME"
    Private Const ExactTargetPassword = "OUTBOUND_EXACTTARGETPASSWORD"

#End Region


#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()
        Dim obc As OutboundCommunication.CommunicationClient
        Dim oWebPasswd As WebPasswd
        Dim strRejectMsg As String
        Dim CancelMessage = String.Empty
        Dim Status As String
        Dim strXML As New StringWriter

        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERT_CANCEL_REQUEST_ID))) Then
                CertCancelReqId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERT_CANCEL_REQUEST_ID)))
                If (Not CertCancelReqId.Equals(Guid.Empty)) Then
                    oCertCancelRequest = New CertCancelRequest(CertCancelReqId)
                    oCertificate = New Certificate(oCertCancelRequest.CertId)

                    Dim oCompany As New Company(oCertificate.CompanyId)
                    Dim oCountry As New Country(oCompany.BusinessCountryId)
                    Dim oLanguage As New Language(oCountry.LanguageId)

                    Select Case MyBase.PublishedTask(PublishedTask.Request_Action)
                        Case "D"
                            If MyBase.PublishedTask(PublishedTask.Action_Code) = "PREPAID" Then
                                CancelMessage = TranslationBase.TranslateLabelOrMessage("CANCEL_REQ_REJECT_MSG_PREPAID", oCountry.LanguageId)
                            ElseIf MyBase.PublishedTask(PublishedTask.Action_Code) = "FULLPAID_12" Then
                                CancelMessage = TranslationBase.TranslateLabelOrMessage("CANCEL_REQ_REJECT_MSG_FULLPAID_12", oCountry.LanguageId)
                            Else
                                CancelMessage = TranslationBase.TranslateLabelOrMessage("CANCEL_REQ_REJECT_MSG_LAWFUL", oCountry.LanguageId)
                            End If
                        Case "A"
                            If MyBase.PublishedTask(PublishedTask.Action_Code) = "BFULL_REFUND" Then
                                CancelMessage = TranslationBase.TranslateLabelOrMessage("CANCEL_REQ_ACCPT_MSG_BFULL_REFUND", oCountry.LanguageId)
                            ElseIf MyBase.PublishedTask(PublishedTask.Action_Code) = "AFULL_REFUND" Then
                                CancelMessage = TranslationBase.TranslateLabelOrMessage("CANCEL_REQ_ACCPT_MSG_AFULL_REFUND", oCountry.LanguageId)
                            End If
                    End Select


                    oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

                    obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
                    obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
                    obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

                    Dim request = New OutboundCommunication.ExactTargetRequest()
                    request.Attributes = New NameValue(8) {}
                    request.Attributes.SetValue(New NameValue() With {.Name = "LastName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerLastName), String.Empty, oCertificate.CustomerLastName)}, 0)
                    request.Attributes.SetValue(New NameValue() With {.Name = "FirstName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerFirstName), String.Empty, oCertificate.CustomerFirstName)}, 1)
                    request.Attributes.SetValue(New NameValue() With {.Name = "Language", .Value = oLanguage.Code}, 2)
                    request.Attributes.SetValue(New NameValue() With {.Name = "PolicyNumber", .Value = If(String.IsNullOrEmpty(oCertificate.CertNumber), String.Empty, oCertificate.CertNumber)}, 3)
                    request.Attributes.SetValue(New NameValue() With {.Name = "InvoiceNo", .Value = If(String.IsNullOrEmpty(oCertificate.InvoiceNumber), String.Empty, oCertificate.InvoiceNumber)}, 4)
                    request.Attributes.SetValue(New NameValue() With {.Name = "ProductCode", .Value = If(String.IsNullOrEmpty(oCertificate.ProductCode), String.Empty, oCertificate.ProductCode)}, 5)
                    request.Attributes.SetValue(New NameValue() With {.Name = "ProductDescription", .Value = If(String.IsNullOrEmpty(oCertificate.GetProdCodeDesc), String.Empty, oCertificate.GetProdCodeDesc)}, 6)
                    request.Attributes.SetValue(New NameValue() With {.Name = "CancelReason", .Value = ""}, 7)
                    request.Attributes.SetValue(New NameValue() With {.Name = "CancelMessage", .Value = CancelMessage}, 8)

                    request.ExactTargetPassword = ConfigurationManager.AppSettings(ExactTargetPassword)
                    request.ExactTargetUserName = ConfigurationManager.AppSettings(ExactTargetUserName)
                    request.Email = oCertificate.Email
                    request.FromEmail = String.Empty
                    request.FromName = String.Empty
                    request.TriggerKey = ConfigurationManager.AppSettings(TriggerKey)
                    Dim response = obc.SendExactTarget(request)

                    If response.Id <> Guid.Empty Then

                            If response.SendSuccessful = True Then
                                Status = "SUCCESS"
                            Else
                                Status = "FAILURE"
                            End If

                            Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
                            eb.Serialize(strXML, response)

                            Try
                                Dim objTrans As New CustCommunication
                                With objTrans
                                .CustomerId = oCertificate.CustomerId
                                .EntityName = Codes.ENTITY_NAME_CERT_CANC_REQ
                                .EntityId = CertCancelReqId
                                .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
                                .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
                                .CommunicationFormat = Codes.TASK_SEND_CANC_REQ_REJ_EMAIL
                                .CustContactId = Guid.Empty
                                .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
                                .ComponentRefId = PublishedTask.Id
                                .CommunicationStatus = Status
                                .CommResponseId = response.Id
                                .CommResponseXml = strXML.ToString()
                                .IsRetryable = Codes.YESNO_N
                                .RetryCompoReference = String.Empty
                                .Save()
                                End With
                            Catch ex As Exception
                                strRejectMsg = ex.Message
                            End Try
                        End If
                    End If
                End If
            Logger.AddDebugLogExit()
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
    End Sub
#End Region

#Region "Private Methods"
    Public Shared Function SerializeObj(obj As Object) As String
        Dim xs As New System.Xml.Serialization.XmlSerializer(obj.GetType)
        Dim w As New IO.StringWriter
        xs.Serialize(w, obj)
        Return w.ToString
    End Function

#End Region

End Class
