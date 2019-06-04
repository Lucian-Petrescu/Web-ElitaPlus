Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem

<DataContract(Name:="CertficateDealerPhoneLookUp", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertficateDealerPhoneLookUp
    Inherits CertificateLookup

    <DataMember(Name:="MobilePhone", IsRequired:=True), StringLength(30, ErrorMessage:="MobilePhone value exceeded maximum length of 15"),
        Required(ErrorMessage:="MobilePhone is Required")>
    Public Property MobilePhone As String

    <DataMember(Name:="DealerCode", IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5"),
   Required(ErrorMessage:="DealerCode is Required")>
    Public Property DealerCode As String

End Class
