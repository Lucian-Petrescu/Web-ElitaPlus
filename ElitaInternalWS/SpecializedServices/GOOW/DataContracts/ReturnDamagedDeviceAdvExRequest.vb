Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization


Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/ReturnDamagedDeviceAdvEx", Name:="ReturnDamagedDeviceAdvExRequest")>
    Public Class ReturnDamagedDeviceAdvExRequest
        Inherits BaseClaimRequest

        <DataMember(IsRequired:=True, Name:="CoverageTypeCode"),
        Required(), StringLength(255, MinimumLength:=1)>
        Public Property CoverageTypeCode As String

    End Class
End Namespace