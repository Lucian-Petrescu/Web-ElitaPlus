Imports System.ServiceModel

' NOTE: If you change the class name "IAccountingWS" here, you must also update the reference to "IAccountingWS" in App.config.
<ServiceContract(namespace:="http://elita.assurant.com")> _
Public Interface IAccountingWS

    <OperationContract()> _
    Function ProcessRequest(networkID As String, _
                           password As String, _
                           LDAPGroup As String, _
                           functionToProcess As String, _
                           xmlStringDataIn As String, _
                           hubRegion As String, _
                           Optional ByVal isAsync As String = "false") As String

    <OperationContract()> _
    Function ResendFile(networkID As String, _
                                  password As String, _
                                  LDAPGroup As String, _
                                  hubRegion As String, _
                                  AccountingTransmissionIdString As String) As String

End Interface
