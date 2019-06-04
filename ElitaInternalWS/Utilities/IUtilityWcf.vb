Imports System.ServiceModel

' NOTE: If you change the class name "IUtilityWcf" here, you must also update the reference to "IUtilityWcf" in Web.config.
Namespace Utilities

    <ServiceContract(Namespace:="http://elita.assurant.com/utilities")> _
    Public Interface IUtilityWcf

        <OperationContract()> _
        Function Hello(ByVal name As String) As String

        <OperationContract()> _
        Function Login() As String

        <OperationContract()> _
        Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) As String

        <OperationContract()> _
        Function ProcessRequest(ByVal token As String, _
                                               ByVal functionToProcess As String, _
                                               ByVal xmlStringDataIn As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCMakes(ByVal token As String, ByVal wsConsumer As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCModels(ByVal token As String, ByVal wsConsumer As String, _
                              ByVal make As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCVersions(ByVal token As String, ByVal wsConsumer As String, _
                                ByVal model As String, ByVal make As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCYears(ByVal token As String, ByVal wsConsumer As String, _
                             ByVal trim As String, ByVal model As String, ByVal make As String) As String

    End Interface

End Namespace