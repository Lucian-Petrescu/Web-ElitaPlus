﻿Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Configuration

Public Class ReSendWelcomeEmailTask
    Inherits TaskBase


#Region "Fields"
    Private _certificateId As Guid
    Private _oCertificate As Certificate
    Private _exactTargetId As Guid

#End Region

#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Properties"
    Private Property ExactTargetId As Guid
        Get
            Return _exactTargetId
        End Get
        Set(ByVal value As Guid)
            _exactTargetId = value
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

#End Region

    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()

        Dim obc As OutboundCommunication.CommunicationClient
        Dim strRejectMsg As String
        Dim Status As String
        Dim strXML As New StringWriter
        Dim oWebPasswd As WebPasswd


        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.SEND_WELCOME_EMAIL_RESP_ID))) Then
                ExactTargetId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.SEND_WELCOME_EMAIL_RESP_ID)))
                CertificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))

                If (Not CertificateId.Equals(Guid.Empty)) Then
                    oCertificate = New Certificate(CertificateId)

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
                                .EntityName = Codes.ENTITY_NAME_CERT
                                .EntityId = CertificateId
                                .Direction = Codes.EVNT_COMM_DIR_OUTBOUND
                                .CommunicationChannel = Codes.EVNT_COMM_CHAN_EMAIL
                                .CommunicationFormat = Codes.TASK_RESEND_WELCOME_EMAIL
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
