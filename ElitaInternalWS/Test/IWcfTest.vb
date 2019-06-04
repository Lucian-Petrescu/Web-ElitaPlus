Imports System.ServiceModel

' NOTE: If you change the class name "IWcfTest" here, you must also update the reference to "IWcfTest" in Web.config.

Namespace Test

    <ServiceContract(Namespace:="http://elita.assurant.com/test")> _
    Public Interface IWcfTest

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