Imports System.ServiceModel
Imports ElitaInternalWS.Security


' ReSharper disable once CheckNamespace
Namespace Accounting
    <ServiceContract(Namespace:="http://elita.assurant.com/Accounting/2.0", Name:="IAccountingService")>
    Public Interface IAccountingServiceV2

        <OperationContract(), ElitaPermission(PermissionCodes.WS_AccountingService_SendFile)>
        Function ResendFile(ByVal accountingTransmissionId As String)

    End Interface
End Namespace
