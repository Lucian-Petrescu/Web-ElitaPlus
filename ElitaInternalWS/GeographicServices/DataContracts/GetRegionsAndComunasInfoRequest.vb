Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace GeographicServices
    <DataContract(Namespace:="http://elita.assurant.com/GeographicServices/RegionsAndComunas", Name:="GetRegionsAndComunasInfoRequest")>
    Public Class GetRegionsAndComunasInfoRequest

        <DataMember(IsRequired:=False, Name:="RegionCode"), StringLength(5, MinimumLength:=2)>
        Public Property RegionCode As String

    End Class
End Namespace