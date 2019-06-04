
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

Namespace Certificates
    <DataContract(Name:="SearchAppleCertificateByImeiRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
    Public Class SearchAppleCertificateByImeiRequest

        <DataMember(IsRequired := True)> _
        public Property CompanyCode as String

        <DataMember()> _
        public Property DealerCode as String

        <DataMember(IsRequired := true)> _
        public Property ImeiNumber as String

        <DataMember(IsRequired := false), StringLength(20, ErrorMessage:="User Details exceeded maximum length of 20")> _
        public Property UserDetails as String

    End Class
End Namespace