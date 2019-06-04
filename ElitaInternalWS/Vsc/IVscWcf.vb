Imports System.ServiceModel

' NOTE: If you change the class name "IVscWcf" here, you must also update the reference to "IVscWcf" in Web.config.
Namespace Vsc

    <ServiceContract(Namespace:="http://elita.assurant.com/vsc")> _
    Public Interface IVscWcf

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

    End Interface

End Namespace
