Imports System.ServiceModel

' NOTE: If you change the class name "IGvsWcf" here, you must also update the reference to "IWcfTest" in Web.config.


Namespace Gvs

    <ServiceContract(Namespace:="http://elita.assurant.com/gvs")> _
    Public Interface IGvsWcf

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
