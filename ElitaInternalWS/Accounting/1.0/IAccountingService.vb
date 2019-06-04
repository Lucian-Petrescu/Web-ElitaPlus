Imports System.ServiceModel

' ReSharper disable once CheckNamespace
Namespace Accounting
    <ServiceContract(Namespace:="http://elita.assurant.com/Accounting/1.0", Name:="IAccountingService")>
    Public Interface IAccountingServiceV1

        <OperationContract()>
        Function ProcessAccounting(networkId As String, password As String, ldapGroup As String, companyId As String, accountingEventId As String, vendorFiles As String) As String

    End Interface
End Namespace