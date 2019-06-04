Imports System.Runtime.Serialization

Namespace SpecializedServices.SFR
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/SFRPolicyService/CertificateDC", Name:="CertificateDC")>
    Public Class CertificateDC

#Region "DataMembers"
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CertificateNumber", Order:=1)> _
        Public Property CertificateNumber As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="DealerCode", Order:=2)> _
        Public Property DealerCode As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Statuscode", Order:=3)> _
        Public Property Statuscode As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CustomerName", Order:=4)> _
        Public Property CustomerName As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="IdentificationNumber", Order:=5)> _
        Public Property IdentificationNumber As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="WorkPhone", Order:=6)> _
        Public Property WorkPhone As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="WarrantySalesDate", Order:=7)> _
        Public Property WarrantySalesDate As DateTime

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ProductCode", Order:=8)> _
        Public Property ProductCode As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="SerialNumber", Order:=9)> _
        Public Property SerialNumber As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ItemDescription", Order:=10)> _
        Public Property ItemDescription As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Manufacturer", Order:=11)> _
        Public Property Manufacturer As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Model", Order:=12)> _
        Public Property Model As String
        
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="SKUNumber", Order:=13)> _
        Public Property SKUNumber As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ServiceLineNumber", Order:=14)> _
        Public Property ServiceLineNumber As String

         <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="ProductDescription", Order:=15)> _
        Public Property ProductDescription As String

#End Region
    End Class
End Namespace
