Imports System.ServiceModel

' NOTE: If you change the class name "IAccountingWS" here, you must also update the reference to "IAccountingWS" in App.config.
<ServiceContract(namespace:="http://elita.assurant.com")> _
Public Interface IAccountingWS

    <OperationContract()> _
    Function ProcessRequest(ByVal networkID As String, _
                           ByVal password As String, _
                           ByVal LDAPGroup As String, _
                           ByVal functionToProcess As String, _
                           ByVal xmlStringDataIn As String, _
                           ByVal hubRegion As String, _
                           Optional ByVal isAsync As String = "false") As String

    <OperationContract()> _
    Function ResendFile(ByVal networkID As String, _
                                  ByVal password As String, _
                                  ByVal LDAPGroup As String, _
                                  ByVal hubRegion As String, _
                                  ByVal AccountingTransmissionIdString As String) As String

End Interface
