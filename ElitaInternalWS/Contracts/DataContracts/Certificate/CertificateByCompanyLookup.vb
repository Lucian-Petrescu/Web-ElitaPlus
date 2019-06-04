

Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

<DataContract(Name:="CertificateByCompanyLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertificateByCompanyLookup
    Inherits CertificateLookup

    <DataMember(Name:="CertificateNumber", IsRequired:=True), StringLength(20, ErrorMessage:="CertificateNumber value exceeded maximum length of 20"),
    Required(ErrorMessage:="CertificateNumber is Required")>
    Public Property CertificateNumber As String


    <DataMember(Name:="CompanyCode", IsRequired:=True),
     Required(ErrorMessage:="CompanyCode is Required")>
    Public Property CompanyCode As String
End Class
