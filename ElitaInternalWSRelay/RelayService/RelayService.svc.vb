Imports System.ServiceModel
Imports System.ServiceModel.Channels

Public Class RelayService
    Implements IRelayService


    Public Function CallThis(ByVal message As Message) As Message Implements IRelayService.CallThis
        ' Get Last Segment of Listen Uri
        'Dim serviceChannel As ServiceChannel
        'DirectCast(OperationContext.Current.Channel, System.ServiceModel.Channels.ServiceChannel).ListenUri()

        Dim buffer As MessageBuffer = message.CreateBufferedCopy(Integer.Parse(ConfigurationManager.AppSettings("BufferSize")))
        Dim output As Message = buffer.CreateMessage()

        ' Get Last Segment from Listen Uri
        Dim originalToUri As Uri = output.Headers.[To]
        Dim lastSegmentName As String = originalToUri.Segments(originalToUri.Segments.Length - 1)

        Dim internalServiceUri As String = ConfigurationManager.AppSettings("ServiceUri-" & lastSegmentName)

        If (String.IsNullOrEmpty(internalServiceUri)) Then
            Throw New ConfigurationErrorsException("Destination Service Url is not configured")
        End If

        RemoveSecurityHeaders(output)
        output.Headers.[To] = New Uri(internalServiceUri)
        Dim binding As New CustomBinding("ClientBindingConfiguration")
        Dim factory As New ChannelFactory(Of IRequestChannel)(binding, internalServiceUri)

        factory.Credentials.UserName.UserName = ConfigurationManager.AppSettings("UserName")
        factory.Credentials.UserName.Password = ConfigurationManager.AppSettings("Password")

        factory.Open()
        Dim channel As IRequestChannel = factory.CreateChannel(New EndpointAddress(internalServiceUri))
        channel.Open()

        Dim result As Message = channel.Request(output)
        message.Close()
        RemoveSecurityHeaders(result)
        output.Close()
        factory.Close()
        channel.Close()
        Return result
    End Function

    Private Shared Sub RemoveSecurityHeaders(ByVal output As Message)
        For i As Integer = (output.Headers.Count - 1) To 1 Step -1
            If output.Headers(i).Name.ToUpperInvariant().Equals("SECURITY") Then
                output.Headers.RemoveAt(i)
            End If
        Next
    End Sub
End Class
