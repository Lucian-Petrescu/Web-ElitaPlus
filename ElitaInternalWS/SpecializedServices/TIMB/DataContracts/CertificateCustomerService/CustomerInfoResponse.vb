Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.Timb
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Timb/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerInfoResponse")>
    Public Class CustomerInfoResponse

        <DataMember(IsRequired:=True, Name:="CustomerName", Order:=1)>
        Public Property CustomerName As String

        <DataMember(IsRequired:=True, Name:="CustomerPhoneNumber", Order:=2)>
        Public Property PhoneNumber As String

        <DataMember(IsRequired:=True, Name:="IdentificationNumber", Order:=3)>
        Public Property IdentificationNumber As String

        <DataMember(IsRequired:=True, Name:="Address1", Order:=4)>
        Public Property Address1 As String

        <DataMember(IsRequired:=True, Name:="Address2", Order:=5)>
        Public Property Address2 As String

        <DataMember(IsRequired:=True, Name:="City", Order:=6)>
        Public Property City As String

        <DataMember(IsRequired:=False, Name:="StateProvince", Order:=7)>
        Public Property StateProvince As String

        <DataMember(IsRequired:=True, Name:="ZipCode", Order:=8)>
        Public Property ZipCode As String

        <DataMember(IsRequired:=True, Name:="Email", Order:=9)>
        Public Property Email As String

        <DataMember(IsRequired:=True, Name:="Certificates", Order:=10)>
        Public Property Certificates As List(Of CustomerCertificateResponse)

        Public Sub New()

        End Sub

        Public Sub New(CustomerData As DataTable)

            If (CustomerData IsNot Nothing) Then

                CustomerName = CustomerData.Rows(0)("Customer_Name").ToString()
                IdentificationNumber = CustomerData.Rows(0)("Identification_Number").ToString()
                PhoneNumber = CustomerData.Rows(0)("Work_Phone").ToString()
                Address1 = CustomerData.Rows(0)("Address1").ToString()
                Address2 = CustomerData.Rows(0)("Address2").ToString()
                City = CustomerData.Rows(0)("City").ToString()
                StateProvince = CustomerData.Rows(0)("State_Province").ToString()
                ZipCode = CustomerData.Rows(0)("Postal_Code").ToString()
                Email = CustomerData.Rows(0)("Email").ToString()
            End If
        End Sub
    End Class

End Namespace