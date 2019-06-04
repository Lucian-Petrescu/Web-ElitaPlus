
Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="CancelByExternalCertNumResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CancelByExternalCertNumResponse

    <DataMember(IsRequired:=False, Name:="DealerCode", Order:=1)>
    Public Property DealerCode As String

    <DataMember(IsRequired:=False, Name:="CertificateNumber", Order:=2)>
    Public Property CertificateNumber As String

    <DataMember(IsRequired:=False, Name:="ErrorCode", Order:=3, EmitDefaultValue:=False)>
    Public Property ErrorCode As String

    <DataMember(IsRequired:=False, Name:="ErrorMessage", Order:=4, EmitDefaultValue:=False)>
    Public Property ErrorMessage As String
End Class

