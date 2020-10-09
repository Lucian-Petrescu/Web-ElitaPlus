Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Configuration

Public Class ReSendClaimGiftCardEMailTask
    Inherits TaskBase

#Region "Fields"
    Private _claimId As Guid
    Private _claimbase As ClaimBase
    Private _certificateId As Guid
    Private _oCertificate As Certificate
    Private _exactTargetId As Guid

#End Region

#Region "Constructors"

    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region


#Region "Properties"
    Private Property ExactTargetId As Guid
        Get
            Return _exactTargetId
        End Get
        Set(value As Guid)
            _exactTargetId = value
        End Set
    End Property
    Private Property CertificateId As Guid
        Get
            Return _certificateId
        End Get
        Set(value As Guid)
            _certificateId = value
        End Set
    End Property

    Private Property oCertificate As Certificate
        Get
            Return _oCertificate
        End Get
        Set(value As Certificate)
            _oCertificate = value
        End Set
    End Property

    Private Property oClaim As ClaimBase
        Get
            Return _claimbase
        End Get
        Set(value As ClaimBase)
            _claimbase = value
        End Set
    End Property

    Private Const UserName = "OUTBOUND_COMM_USERNAME"
    Private Const Password = "OUTBOUND_COMM_PASSWORD"

#End Region

    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()

        Dim obc As OutboundCommunication.CommunicationClient
        Dim strRejectMsg As String
        Dim Status As String
        Dim strXML As New StringWriter
        Dim oWebPasswd As WebPasswd


        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.SEND_CLM_GIFTCARD_EMAIL_RESP_ID))) Then
                ExactTargetId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.SEND_CLM_GIFTCARD_EMAIL_RESP_ID)))
                If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_ID))) Then
                    _claimId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_ID)))
                    oClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(_claimId)
                    oCertificate = New Certificate(oClaim.Certificate.Id)


                    oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

                    obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", oWebPasswd.Url)
                    obc.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
                    obc.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)

                Dim response = obc.ResendExactTargetById(ExactTargetId)

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
                                .EntityName = Codes.ENTITY_NAME_CLAIM
                                .EntityId = oClaim.Id
                                .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
                                .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
                                .CommunicationFormat = Codes.TASK_RESEND_CLM_GIFTCARD_EMAIL
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

End Class
