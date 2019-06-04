Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ITestService" in both code and config file together.
<ServiceContract(namespace:="http://elita.assurant.com")> _
Public Interface ITestService

    <OperationContract()> _
    Function HealthCheck(ByVal networkID As String, _
                           ByVal password As String, _
                           ByVal LDAPGroup As String, _
                           ByVal hubRegion As String) As String


End Interface
