Imports System.ServiceModel

' NOTE: If you change the class name "IUtilityWcf" here, you must also update the reference to "IUtilityWcf" in Web.config.
Namespace Utilities

    <ServiceContract(Namespace:="http://elita.assurant.com/utilities")> _
    Public Interface IUtilityWcf

        <OperationContract()> _
        Function Hello(name As String) As String

        <OperationContract()> _
        Function Login() As String

        <OperationContract()> _
        Function LoginBody(networkID As String, password As String, group As String) As String

        <OperationContract()> _
        Function ProcessRequest(token As String, _
                                               functionToProcess As String, _
                                               xmlStringDataIn As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCMakes(token As String, wsConsumer As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCModels(token As String, wsConsumer As String, _
                              make As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCVersions(token As String, wsConsumer As String, _
                                model As String, make As String) As String

        <OperationContract()> _
        <WebMethod(EnableSession:=True)> _
        Function GetVSCYears(token As String, wsConsumer As String, _
                             trim As String, model As String, make As String) As String

    End Interface

End Namespace