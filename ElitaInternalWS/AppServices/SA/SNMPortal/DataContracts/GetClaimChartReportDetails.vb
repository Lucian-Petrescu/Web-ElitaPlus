Imports System.Runtime.Serialization


Namespace AppServices.SA.SNMPortal.DataContracts
    <DataContract(Namespace:=SnmPortalConstants.DataContractNameSpace, Name:="Claim")> _
    Public Class GetClaimChartReportDetails

#Region "DataMember"

        <DataMember(EmitDefaultValue:=True, Name:="DealerCode", Order:=1)> _
        Public Property DealerCode As String

        <DataMember(EmitDefaultValue:=True, Name:="CertificateNumber", Order:=2)> _
        Public Property CertificateNumber As String

        <DataMember(EmitDefaultValue:=True, Name:="WorkPhone", Order:=3)> _
        Public Property WorkPhone As String

        <DataMember(EmitDefaultValue:=True, Name:="ServiceCenterCode", Order:=4)> _
        Public Property ServiceCenterCode As string

        <DataMember(EmitDefaultValue:=True, Name:="ServiceCenterName", Order:=5)> _
        Public Property ServiceCenterName As String

        <DataMember(EmitDefaultValue:=True, Name:="ClaimNumber", Order:=6)> _
        Public Property ClaimNumber As String

        <DataMember(EmitDefaultValue:=True, Name:="ClaimStatusCode", Order:=7)> _
        Public Property ClaimStatusCode As String

        <DataMember(EmitDefaultValue:=True, Name:="ClaimExtendedStatus", Order:=8)> _
        Public Property ClaimExtendedStatus As String

        <DataMember(EmitDefaultValue:=True, Name:="ClaimExtendedStatusDate", Order:=9)> _
        Public Property ClaimExtendedStatusDate As Date

        <DataMember(EmitDefaultValue:=True, Name:="Make", Order:=10)> _
        Public Property Make As String

        <DataMember(EmitDefaultValue:=True, Name:="Model", Order:=11)> _
        Public Property Model As String

        <DataMember(EmitDefaultValue:=True, Name:="ImeiNumber", Order:=12)> _
        Public Property ImeiNumber As String
        
#End Region

    End Class
End Namespace
