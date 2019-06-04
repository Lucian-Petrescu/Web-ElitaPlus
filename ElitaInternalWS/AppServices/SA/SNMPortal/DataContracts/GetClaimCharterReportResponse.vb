
Imports System.Collections.Generic
Imports System.Runtime.Serialization


Namespace AppServices.SA.SNMPortal.DataContracts

    <DataContract(Namespace:=SnmPortalConstants.DataContractNameSpace, Name:="GetClaimCharterReportResponse")>
    Public Class GetClaimCharterReportResponse

#Region "DataMember"
        <DataMember(Name:="BatchId", Order:=1, EmitDefaultValue := true)> _
        Public Property BatchId() As Guid?

        <DataMember(Name:="CompanyCode", Order:=2, EmitDefaultValue := true)> _
        Public Property CompanyCode() As String
        
        <DataMember(Name:="TotalRecordCount", Order:=3, EmitDefaultValue := true)> _
        Public Property TotalRecordCount() As Integer
        
        <DataMember(Name:="QueuedRecordCount", Order:=4, EmitDefaultValue := true)> 
        Public Property QueuedRecordCount As integer

        <DataMember(Name:="ClaimList", Order:=5, EmitDefaultValue := true)> 
        Public Property ClaimList As IEnumerable(Of GetClaimChartReportDetails)                
#End Region

    End Class

    
End Namespace