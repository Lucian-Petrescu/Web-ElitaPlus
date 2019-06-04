Imports System.ServiceModel
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICustomerRegistration" in both code and config file together.
Namespace CustomerReg

    <ServiceContract(Namespace:="http://elita.assurant.com/CustomerReg")> _
    Public Interface ICustomerRegistration

        <OperationContract(Name:="Hello")> _
        Function Hello(ByVal name As String) As String

        <OperationContract(Name:="Login")> _
        Function Login() As String

        <OperationContract(Name:="LoginBody")> _
        Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) As String

        <OperationContract(Name:="FindRegistration")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function FindRegistration(ByVal customerRegItemSearch As CustRegItemSearchDC) As CustRegistrationDC

        <OperationContract(Name:="CreateRegistration")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function CreateRegistration(ByVal customerRegistration As CustRegistrationDC) As String

        <OperationContract(Name:="UpdateRegistration")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function UpdateRegistration(ByVal customerRegistration As CustRegistrationDC) As String

        <OperationContract(Name:="FindItem")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function FindItem(ByVal customerRegItemSearch As CustRegItemSearchDC) As List(Of CustItemDC)

        <OperationContract(Name:="CreateItem")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function CreateItem(ByVal customerItem As CustItemDC) As String

        <OperationContract(Name:="DeleteItem")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function DeleteItem(ByVal customerItemDelete As CustItemDeleteActivateDC) As List(Of CustItemDC)

        <OperationContract(Name:="ActivateItem")> _
        <FaultContract(GetType(CustServiceFaultDC))> _
        Function ActivateItem(ByVal customerItemActivate As CustItemDeleteActivateDC) As String

    End Interface

End Namespace
