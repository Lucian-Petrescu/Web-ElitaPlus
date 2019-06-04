Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerCertificateItemResponse")>
Public Class CustomerCertificateItemResponse

    <DataMember(IsRequired:=True, Name:="SerialNumber")>
    Public Property SerialNumber As String

    <DataMember(IsRequired:=True, Name:="Make")>
    Public Property Manufacturer As String

    <DataMember(IsRequired:=True, Name:="Model")>
    Public Property Model As String

    <DataMember(IsRequired:=True, Name:="SKUNumber")>
    Public Property SKUNumber As String

    <DataMember(IsRequired:=True, Name:="ProductCode")>
    Public Property ProductCode As String

    <DataMember(IsRequired:=True, Name:="ProductDescription")>
    Public Property ProductDescription As String

    Public Sub New()

    End Sub


    Public Sub New(Item As DataTable)
        If (Not Item Is Nothing) Then
            Me.Manufacturer = Item.Rows(0)("make").ToString()
            Me.Model = Item.Rows(0)("model").ToString()
            Me.ProductCode = Item.Rows(0)("product_code").ToString()
            Me.ProductDescription = Item.Rows(0)("description").ToString()
            Me.SerialNumber = Item.Rows(0)("serial_number").ToString()
            Me.SKUNumber = Item.Rows(0)("sku_number").ToString()

        End If
    End Sub

End Class
