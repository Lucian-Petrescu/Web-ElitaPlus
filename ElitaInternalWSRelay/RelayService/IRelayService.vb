Imports System.ServiceModel
Imports System.ServiceModel.Channels

    <ServiceContract()> _
Public Interface IRelayService
    <OperationContract(Action:="*", ReplyAction:="*", ProtectionLevel:=Net.Security.ProtectionLevel.None)> _
    Function CallThis(ByVal message As Message) As Message
End Interface
