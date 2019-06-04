Imports System.Runtime.Serialization
Imports Assurant.Common.Validation
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="DownloadCertificateDocumentRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class DownloadCertificateDocumentRequest

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

    <DataMember(Name:="ImageId", IsRequired:=True)>
    Public Property ImageId As Guid

End Class
