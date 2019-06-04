Imports System.Runtime.Serialization


Namespace AppServices.SA.SNMPortal.DataContracts

    <DataContract(Namespace:=SnmPortalConstants.DataContractNameSpace, Name:="GetClaimCharterReportNextPageRequest")>
    Public Class GetClaimCharterReportNextPageRequest

        <DataMember(IsRequired:=True, Name:="BatchId", Order:=1)> _
        Public Property BatchId() As Guid

       <DataMember(IsRequired:=true, Name:="PageSize", Order:=2)> 
        Public Property PageSize As Integer

    End Class                      
End Namespace