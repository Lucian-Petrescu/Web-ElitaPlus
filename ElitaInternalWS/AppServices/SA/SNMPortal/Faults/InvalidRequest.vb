Imports System.Runtime.Serialization

Namespace AppServices.SA.SNMPortal.Faults

    <DataContract(Name:="InvalidRequestFault", Namespace:=SnmPortalConstants.ContractNameSpace)>
    Public Class InvalidRequestFault
        <DataMember(IsRequired:=True, Name:="FaultCode")>
        Public Property FaultCode As String

        <DataMember(IsRequired:=True, Name:="FaultMessage")>
        Public Property FaultMessage As String

        Public Sub New(ByVal faultCode As String, faultMsg As string)            
            Me.FaultCode = faultCode
            FaultMessage = faultMsg         
        End Sub       
    End Class
End Namespace