
Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace SpecializedServices.SFR
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/SFRPolicyService/SearchCertificate", Name:="SearchCertificateResponse")> _
    Public Class SearchCertificateResponse
#Region "DataMembers"

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CertInfo", Order:=1)> _
        Public Property CertificateInfo As IEnumerable(Of CertificateDC)       

#End Region
    End Class
End Namespace