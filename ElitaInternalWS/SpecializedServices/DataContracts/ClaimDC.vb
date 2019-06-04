Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/DataContracts", Name:="ClaimDC")> _
    Public Class ClaimDC

#Region "DataMember"

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Id", Order:=1)> _
        Public Property Claim_Id As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Number", Order:=2)> _
        Public Property Claim_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Status", Order:=3)> _
        Public Property Claim_Status As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Created_Date", Order:=4)> _
        Public Property Claim_Created_Date As Nullable(Of Date)

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Type_Code", Order:=5)> _
        Public Property Claim_Type_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Type_Desc", Order:=6)> _
        Public Property Claim_Type_Desc As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Extended_Status_Code", Order:=7)> _
        Public Property Claim_Extended_Status_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Extended_Status_Desc", Order:=8)> _
        Public Property Claim_Extended_Status_Desc As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Authorization_Number", Order:=9)> _
        Public Property Authorization_Number As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Authorized_Amount", Order:=10)> _
        Public Property Authorized_Amount As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Method_Of_Repair", Order:=11)> _
        Public Property Method_Of_Repair As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Loss_Date", Order:=12)> _
        Public Property Loss_Date As Nullable(Of Date)

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Claim_Paid_Amount", Order:=13)> _
        Public Property Claim_Paid_Amount As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Center_Code", Order:=14)> _
        Public Property Service_Center_Code As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Service_Center_Desc", Order:=15)>
        Public Property Service_Center_Desc As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Device_Condition", Order:=16)>
        Public Property Device_Condition As String

        <DataMember(EmitDefaultValue:=False, IsRequired:=False, Name:="Certificate_Number", Order:=17)>
        Public Property Certificate_Number As String
#End Region

    End Class
End Namespace
