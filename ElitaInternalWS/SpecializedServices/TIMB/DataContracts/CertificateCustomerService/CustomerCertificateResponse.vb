Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.Timb

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Timb/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerCertificateResponse")>
    Public Class CustomerCertificateResponse

        <DataMember(IsRequired:=True, Name:="CertificateNumber", Order:=1)>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="CertificateStatus", Order:=2)>
        Public Property CertificateStatus As String

        <DataMember(IsRequired:=True, Name:="WarrantySalesDate", Order:=3)>
        Public Property WarrantySalesDate As Date

        <DataMember(IsRequired:=True, Name:="ServiceLineNumber", Order:=4)>
        Public Property ServiceLineNumber As String

        <DataMember(IsRequired:=True, Name:="DealerCode", Order:=5)>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="Items", Order:=6)>
        Public Property Items As List(Of CustomerCertificateItemResponse)

        <DataMember(IsRequired:=True, Name:="UpgradeProgram", Order:=7)>
        Public Property UpgradeProgram As Boolean

        <DataMember(IsRequired:=True, Name:="UpgradeTerm", Order:=8)>
        Public Property UpgradeTerm As String

        <DataMember(IsRequired:=True, Name:="UpgradeType", Order:=9)>
        Public Property UpgradeType As String

        <DataMember(IsRequired:=True, Name:="MembershipNumber", Order:=10)>
        Public Property MembershipNumber As String






        Public Sub New()

        End Sub
        Public Sub New(Certificates As DataTable)
            If (Certificates IsNot Nothing) Then
                CertificateNumber = Certificates.Rows(0)("cert_number").ToString()
                CertificateStatus = Certificates.Rows(0)("status_code").ToString()
                WarrantySalesDate = Certificates.Rows(0)("warranty_Sales_date")
                ServiceLineNumber = Certificates.Rows(0)("service_line_number").ToString()
                DealerCode = Certificates.Rows(0)("dealer").ToString()
                UpgradeProgram = (Certificates.Rows(0)("upgrade_program").ToString().ToUpper() = "Y")
                UpgradeTerm = Certificates.Rows(0)("upgrade_term").ToString().ToString()
                UpgradeType = Certificates.Rows(0)("upgrade_type").ToString().ToString()
                MembershipNumber = Certificates.Rows(0)("membership_number").ToString()
            End If
        End Sub
    End Class

End Namespace