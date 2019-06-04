Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Namespace SpecializedServices.SFR
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/SFRPolicyService/SearchCertificate", Name:="SearchCertificateRequest")>
    Public Class SearchCertificateRequest
        <DataMember(IsRequired:=True, Name:="CompanyCode"), Required()>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=False, Name:="DealerGroup")>
        public Property DealerGroup As String
        
        <DataMember(IsRequired:=False, Name:="DealerCode")>
        public Property DealerCode As String

        <DataMember(IsRequired:=False, Name:="FirstName"), StringLength(50, MinimumLength:=0)>
        Public Property FirstName As String

        <DataMember(IsRequired:=False, Name:="LastName"), StringLength(50, MinimumLength:=0)>
        Public Property LastName As String

        <DataMember(IsRequired:=False, Name:="PhoneNumber"), StringLength(15, MinimumLength:=0)>
        Public Property PhoneNumber As String

        <DataMember(IsRequired:=False, Name:="IdentificationNumber"), StringLength(30, MinimumLength:=0)>
        Public Property IdentificationNumber As String
       
        <DataMember(IsRequired:=False, Name:="Email"), StringLength(250, MinimumLength:=0)>
        Public Property Email As String
        
        <DataMember(IsRequired:=False, Name:="PostalCode"), StringLength(25, MinimumLength:=0)>
        Public Property PostalCode As String
        

    End Class
End Namespace