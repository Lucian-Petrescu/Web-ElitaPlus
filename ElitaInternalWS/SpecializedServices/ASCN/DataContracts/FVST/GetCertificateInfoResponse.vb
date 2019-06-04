Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/FVSTMobileApplicationService/GetCertificateInfo", Name:="GetCertificateInfoResponse")>
Public Class GetCertificateInfoResponse

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Coverages", Order:=1)>
    Public Property Coverages As IEnumerable(Of CertificateDetails)

End Class

<DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/FVSTMobileApplicationService/GetCertificateInfo", Name:="CertificateDetails")>
Public Class CertificateDetails
    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CertificateNumber", Order:=1)>
    Public Property CertificateNumber As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="StatusEnglish", Order:=2)>
    Public Property StatusEnglish As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="StatusChinese", Order:=3)>
    Public Property StatusChinese As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CustomerName", Order:=4)>
    Public Property CustomerName As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CellNumber", Order:=5)>
    Public Property CellNumber As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ItemDescription", Order:=6)>
    Public Property ItemDescription As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="WarrantyPurchaseDate", Order:=7)>
    Public Property WarrantyPurchaseDate As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CoverageEnglish", Order:=8)>
    Public Property CoverageEnglish As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CoverageChinese", Order:=9)>
    Public Property CoverageChinese As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CoverageDuration", Order:=10)>
    Public Property CoverageDuration As Byte
End Class
