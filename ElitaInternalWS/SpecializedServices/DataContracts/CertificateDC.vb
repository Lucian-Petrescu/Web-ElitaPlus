Imports System.Runtime.Serialization

Namespace SpecializedServices

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/DataContracts", Name:="CertificateDC")> _
    Public Class CertificateDC

#Region "DataMembers"
        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Cert_Number", Order:=1)> _
        Public Property Cert_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Dealer_Code", Order:=2)> _
        Public Property Dealer_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Cert_Status", Order:=3)> _
        Public Property Cert_Status As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Enrollment_Date", Order:=4)> _
        Public Property Enrollment_Date As Nullable(Of Date)

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Manufacturer", Order:=5)> _
        Public Property Manufacturer As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Model", Order:=6)> _
        Public Property Model As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Phone_Number", Order:=7)>
        Public Property Phone_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Serial_Number", Order:=8)>
        Public Property Serial_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Related_Company_Code", Order:=9)>
        Public Property Related_Company_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Related_Cert_Number", Order:=10)>
        Public Property Related_Cert_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Related_Dealer_Code", Order:=11)>
        Public Property Related_Dealer_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Relation", Order:=12)>
        Public Property Relation As String
#End Region
    End Class
End Namespace
