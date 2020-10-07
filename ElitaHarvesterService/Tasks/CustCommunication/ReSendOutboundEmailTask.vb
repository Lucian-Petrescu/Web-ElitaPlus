Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO

Public Class ReSendOutboundEmailTask
    Inherits TaskBase

#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Try
            Dim strStatus As String, strXml As New StringWriter, strComments as String
            Dim obc As OutboundCommunication.CommunicationClient
            Dim oWebPasswd As WebPasswd

            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

            Dim strUrl as String
            strUrl = oWebPasswd.Url
            obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", strUrl)
            obc.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            obc.ClientCredentials.UserName.Password = oWebPasswd.Password

            Dim strExactTargetId as string, guidMsgRecipientId as Guid
            strExactTargetId = MyBase.PublishedTask("CommReferenceId")
            guidMsgRecipientId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask("OCMsgRCPTId")))
            strComments = PublishedTask.Sender

            Try                        
                Dim response = obc.ResendExactTargetById(New Guid(strExactTargetId))
                                  
                If response.Id <> Guid.Empty Then
                    If response.SendSuccessful = True Then
                        strStatus = "SUCCESS"
                    Else
                        strStatus = "FAILURE"
                        Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
                        eb.Serialize(strXML, response)
                    End If                       
                End If
            Catch ex As Exception
                strStatus = "FAILURE"
                strXml.WriteLine("Exception message:")
                strXml.WriteLine(ex.Message)
                strXml.WriteLine("Stack Trace:")
                strXml.WriteLine(ex.StackTrace)
            End Try

            'Update Message process results    
            PublishedTask.UpdateResendMessageStatus(guidMsgRecipientId, strStatus, strXml.ToString(), strComments)
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub
#End Region

End Class
