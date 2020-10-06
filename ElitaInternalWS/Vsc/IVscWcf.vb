Imports System.ServiceModel

' NOTE: If you change the class name "IVscWcf" here, you must also update the reference to "IVscWcf" in Web.config.
Namespace Vsc

    <ServiceContract(Namespace:="http://elita.assurant.com/vsc")> _
    Public Interface IVscWcf

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

    End Interface

End Namespace
