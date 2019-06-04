Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/CertificateCustomerService/GetCustomerCertificate", Name:="GetCustomerResponse")>
    Public Class CustomerResponse
        <DataMember(IsRequired:=True, Name:="Customer", Order:=1)>
        Public Property CustomerInfo As List(Of CustomerInfoResponse)

        '<DataMember(IsRequired:=True, Name:="Certificates", Order:=2)>
        'Public Property Certificates As List(Of CustomerCertificateResponse)


        Public Sub New()

        End Sub
        Public Sub New(CertCustomerData As DataSet)
            ' Me.CustomerInfo = New CustomerInfoResponse(CertCustomerData)
        End Sub
    End Class

End Namespace