Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Configuration

Public Class SendWelcomeEmailTask
    Inherits TaskBase

#Region "Fields"
    Private _certificateId As Guid
    Private _oCertificate As Certificate

#End Region

#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Properties"
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
    Private Const TriggerKey = "OUTBOUND_COMM_PURCHASE_TRIGGER_KEY"
    Private Const ExactTargetUserName = "OUTBOUND_EXACTTARGETUSERNAME"
    Private Const ExactTargetPassword = "OUTBOUND_EXACTTARGETPASSWORD"

#End Region


#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()

        Dim obc As OutboundCommunication.CommunicationClient
        Dim strRejectMsg As String
        Dim Status As String
        Dim strXML As New StringWriter
        Dim oWebPasswd As WebPasswd


        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
                CertificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))
                If (Not CertificateId.Equals(Guid.Empty)) Then
                    oCertificate = New Certificate(CertificateId)


                    Dim oCompany As New Company(oCertificate.CompanyId)
                    Dim oCountry As New Country(oCompany.BusinessCountryId)
                    Dim oLanguage As New Language(oCountry.LanguageId)

                    oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

                    obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
                    obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
                    obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

                    Dim request = New OutboundCommunication.ExactTargetRequest()

                    request.Attributes = New NameValue(8) {}
                    request.Attributes.SetValue(New NameValue() With {.Name = "LastName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerLastName), String.Empty, oCertificate.CustomerLastName)}, 0)
                    request.Attributes.SetValue(New NameValue() With {.Name = "FirstName", .Value = If(String.IsNullOrEmpty(oCertificate.CustomerFirstName), String.Empty, oCertificate.CustomerFirstName)}, 1)
                    request.Attributes.SetValue(New NameValue() With {.Name = "Language", .Value = oLanguage.Code}, 2)
                    request.Attributes.SetValue(New NameValue() With {.Name = "Termslink", .Value = ""}, 3)
                    request.Attributes.SetValue(New NameValue() With {.Name = "Soldby", .Value = If(String.IsNullOrEmpty(oCertificate.SalesChannel), String.Empty, oCertificate.SalesChannel)}, 4)
                    request.Attributes.SetValue(New NameValue() With {.Name = "PolicyNumber", .Value = If(String.IsNullOrEmpty(oCertificate.CertNumber), String.Empty, oCertificate.CertNumber)}, 5)
                    request.Attributes.SetValue(New NameValue() With {.Name = "InvoiceNo", .Value = If(String.IsNullOrEmpty(oCertificate.InvoiceNumber), String.Empty, oCertificate.InvoiceNumber)}, 6)
                    request.Attributes.SetValue(New NameValue() With {.Name = "ProductCode", .Value = If(String.IsNullOrEmpty(oCertificate.ProductCode), String.Empty, oCertificate.ProductCode)}, 7)
                    request.Attributes.SetValue(New NameValue() With {.Name = "ProductDescription", .Value = If(String.IsNullOrEmpty(oCertificate.GetProdCodeDesc), String.Empty, oCertificate.GetProdCodeDesc)}, 8)

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
                                .EntityName = Codes.ENTITY_NAME_CERT
                                .EntityId = CertificateId
                                .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
                                .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
                                .CommunicationFormat = Codes.TASK_SEND_WELCOME_EMAIL
                                .CustContactId = Guid.Empty
                                .CommunicationComponent = Codes.SUBSCRIBER_TYPE__HARVESTER
                                .ComponentRefId = PublishedTask.Id
                                .CommunicationStatus = Status
                                .CommResponseId = response.Id
                                .CommResponseXml = strXML.ToString()
                                .IsRetryable = Codes.YESNO_Y
                                .RetryCompoReference = Codes.EVNT_TYP_CRT_RESEND_WELCOME_PACK
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
