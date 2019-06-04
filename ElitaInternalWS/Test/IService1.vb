Imports System.ServiceModel

' NOTE: If you change the class name "IService1" here, you must also update the reference to "IService1" in Web.config.

Namespace Test

    <ServiceContract()> _
    Public Interface IService1

        <OperationContract()> _
        Function DoWork() As String

    End Interface

End Namespace