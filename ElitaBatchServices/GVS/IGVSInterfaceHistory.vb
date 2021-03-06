﻿Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IGVSInterfaceHistory" in both code and config file together.
<ServiceContract(namespace:="http://elita.assurant.com")> _
Public Interface IGVSInterfaceHistoryWS

    <OperationContract()> _
    Function CheckTransactionHistory(ByVal networkID As String, _
                           ByVal password As String, _
                           ByVal LDAPGroup As String, _
                           ByVal hubRegion As String, _
                           Optional ByVal HoursToCheck As Integer = 0) As String


End Interface
