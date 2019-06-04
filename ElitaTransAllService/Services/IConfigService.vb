Imports System.ServiceModel

' NOTE: If you change the class name "IConfigService" here, you must also update the reference to "IConfigService" in App.config.
<ServiceContract()> _
Public Interface IConfigService

    <OperationContract()> _
    Sub DoWork()

End Interface
