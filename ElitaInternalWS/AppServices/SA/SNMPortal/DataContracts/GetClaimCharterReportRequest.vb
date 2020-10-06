Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization


Namespace AppServices.SA.SNMPortal.DataContracts

    <DataContract(Namespace:=SnmPortalConstants.DataContractNameSpace, Name:="GetClaimCharterReportRequest")>
    Public Class GetClaimCharterReportRequest

#Region "DataMember"
        <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1)> _
        Public Property CompanyCode() As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode", Order:=2)> _
        Public Property ServiceCenterCode() As String
           
        <DataMember(IsRequired:=True, Name:="CountryISOCode", Order:=3)> _
        Public Property CountryIsoCode() As String
        
        <DataMember(IsRequired:=true, Name:="FromDate", Order:=4)> 
        Public Property FromDate As date

        <DataMember(IsRequired:=true, Name:="EndDate", Order:=5)> 
        Public Property EndDate As date

        <DataMember(IsRequired:=true, Name:="PageSize", Order:=6)> 
        Public Property PageSize As Integer

        <DataMember(IsRequired:=False, Name:="DealerCode", Order:=7)> 
        Public Property DealerCode As String

        <DataMember(IsRequired:=False, Name:="ExtendedStatus", Order:=8)> 
        Public Property ExtendedStatus As ClaimExentedStatusType
        
#End Region

    End Class

    <DataContract(Namespace:=SnmPortalConstants.DataContractNameSpace)>
    Public  Enum ClaimExentedStatusType
        <EnumMember()>
        RepairedAndIrreparable 
        <EnumMember()>
        Repaired
        <EnumMember()>
        Irreparable        
    End Enum
    
    public Module ClaimExentedStatusTypeExtensions
        <Extension()>
        Function GetCodeString(status As ClaimExentedStatusType) As string
            select case status
                Case ClaimExentedStatusType.Repaired
                    return "REPRD"
                Case ClaimExentedStatusType.Irreparable
                    Return "CHLIRR"
                Case ClaimExentedStatusType.RepairedAndIrreparable 
                    return String.Empty
                Case Else
                    return String.Empty 'default to RepairedAndUnRepairable
            End Select
        End Function
    End Module
                      
End Namespace