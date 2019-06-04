Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="GetCertificateRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class GetCertificateRequest

    <DataMember(Name:="CertificateSearch", IsRequired:=True)> _
    Public Property CertificateSearch As CertificateLookup

    <DataMember(Name:="RequestedDetails", IsRequired:=True)> _
    Public Property RequestedDetails As CertificateDetailTypes

    <DataMember(Name:="AppleCareCheck", IsRequired:=false)>
    Public Property AppleCareCheck As Nullable(Of Boolean)

    <DataMember(Name:="AppleCareCheckUserDetails", IsRequired:=false), StringLength(20, ErrorMessage:="AppleCare Check User Details exceeded maximum length of 20")>
    Public Property AppleCareCheckUserDetails As String
End Class