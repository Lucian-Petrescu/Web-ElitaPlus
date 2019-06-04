Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="ClaimInfo", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class ClaimInfo

    <DataMember(Name:="CompanyCode", IsRequired:=True)> _
    Public Property CompanyCode As String

    <DataMember(Name:="ClaimNumber", IsRequired:=True)> _
    Public Property ClaimNumber As String

    <DataMember(Name:="CertificateNumber", IsRequired:=True)> _
    Public Property CertificateNumber As String

    <DataMember(Name:="DealerCode", IsRequired:=True)> _
    Public Property DealerCode As String

    <DataMember(Name:="ClaimStatus", IsRequired:=True)> _
    Public Property ClaimStatus As String

    <DataMember(Name:="ProblemDescription", IsRequired:=True)> _
    Public Property ProblemDescription As String

    <DataMember(Name:="ClaimFullFillmentDate", IsRequired:=True)>
    Public Property ClaimFullFillmentDate As Nullable(Of Date)

    <DataMember(Name:="ExtendedStatuses", IsRequired:=False)>
    Public Property ExtendedStatuses As IEnumerable(Of ExtendedStatus)

    <DataMember(Name:="CoverageType", IsRequired:=False)>
    Public Property CoverageType As CoverageTypes

End Class

<DataContract(Name:="ExtendedStatus", Namespace:="http://elita.assurant.com/DataContracts/Claim")>
Public Class ExtendedStatus
    <DataMember(Name:="Code", IsRequired:=True)>
    Public Property Code As String

    <DataMember(Name:="CreatedDate", IsRequired:=False)>
    Public Property CreatedDate As Date
End Class
