Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="ClaimNumberLookup", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class ClaimNumberLookup
    Inherits ClaimLookup

    <DataMember(Name:="CompanyCode", IsRequired:=False),
    StringLength(5, ErrorMessage:="CompanyCode value exceeded maximum length of 5")> _
    Public Property CompanyCode As String

    <DataMember(Name:="ClaimNumber", IsRequired:=True),
    StringLength(20, ErrorMessage:="ClaimNumber value exceeded maximum length of 20"),
    Required(ErrorMessage:="ClaimNumber is Required")>
    Public Property ClaimNumber As String

End Class

