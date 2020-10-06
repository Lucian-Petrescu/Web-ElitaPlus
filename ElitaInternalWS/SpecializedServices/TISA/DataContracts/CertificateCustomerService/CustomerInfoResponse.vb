Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerInfoResponse")>
    Public Class CustomerInfoResponse

        <DataMember(IsRequired:=True, Name:="CustomerName", Order:=1)>
        Public Property CustomerName As String

        <DataMember(IsRequired:=True, Name:="CustomerPhoneNumber", Order:=2)>
        Public Property PhoneNumber As String

        <DataMember(IsRequired:=True, Name:="IdentificationNumber", Order:=3)>
        Public Property IdentificationNumber As String

        <DataMember(IsRequired:=True, Name:="Certificates", Order:=4)>
        Public Property Certificates As List(Of CustomerCertificateResponse)

        Public Sub New()

        End Sub

        Public Sub New(CustomerData As DataTable)

            If (Not CustomerData Is Nothing) Then

                CustomerName = CustomerData.Rows(0)("Customer_Name").ToString()
                IdentificationNumber = CustomerData.Rows(0)("Identification_Number").ToString()
                PhoneNumber = CustomerData.Rows(0)("Work_Phone").ToString()

            End If
        End Sub
    End Class

End Namespace