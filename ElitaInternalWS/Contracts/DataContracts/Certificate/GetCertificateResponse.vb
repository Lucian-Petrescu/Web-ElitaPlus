Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="GetCertificateResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class GetCertificateResponse

    <DataMember(Name:="Certificate", IsRequired:=False)> _
    Public Property Certificate As CertificateInfo

End Class
