Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Collections.Generic

' NOTE: If you change the class name "ISNMPortalWcf" here, you must also update the reference to "IWcfTest" in Web.config.


Namespace SNMPortal

    <ServiceContract(Namespace:="http://elita.assurant.com/SNMPortal")> _
    Public Interface ISNMPortalWcf

        <OperationContract()> _
        Function Hello(ByVal name As String) As String

        <OperationContract()> _
        Function Login() As String

        <OperationContract()> _
        Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) As String

        <OperationContract()> _
        Function ProcessRequest(ByVal token As String, _
                                               ByVal functionToProcess As String, _
                                               ByVal xmlStringDataIn As String) As String

        <OperationContract(Name:="GetPreInvoice"), FaultContract(GetType(SNMPortalFaultDC))> _
        Function GetPreInvoice(ByVal PreInvoice As PreInvoiceSearchDC) As List(Of PreInvoiceDC)

        <OperationContract(Name:="GetPriceList"), FaultContract(GetType(SNMPortalFaultDC))> _
        Function GetPriceList(ByVal PriceList As PriceListSearchDC) As List(Of PriceListDC)

    End Interface

End Namespace
