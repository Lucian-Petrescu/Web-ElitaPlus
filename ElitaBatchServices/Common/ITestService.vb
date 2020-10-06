Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITestService" in both code and config file together.
<ServiceContract(namespace:="http://elita.assurant.com")> _
Public Interface ITestService

    <OperationContract()> _
    Function HealthCheck(networkID As String, _
                           password As String, _
                           LDAPGroup As String, _
                           hubRegion As String) As String


End Interface
