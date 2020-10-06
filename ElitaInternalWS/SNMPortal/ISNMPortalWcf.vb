Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Collections.Generic

' NOTE: If you change the class name "ISNMPortalWcf" here, you must also update the reference to "IWcfTest" in Web.config.


Namespace SNMPortal

    <ServiceContract(Namespace:="http://elita.assurant.com/SNMPortal")> _
    Public Interface ISNMPortalWcf

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

        <OperationContract(Name:="GetPreInvoice"), FaultContract(GetType(SNMPortalFaultDC))> _
        Function GetPreInvoice(PreInvoice As PreInvoiceSearchDC) As List(Of PreInvoiceDC)

        <OperationContract(Name:="GetPriceList"), FaultContract(GetType(SNMPortalFaultDC))> _
        Function GetPriceList(PriceList As PriceListSearchDC) As List(Of PriceListDC)

    End Interface

End Namespace
