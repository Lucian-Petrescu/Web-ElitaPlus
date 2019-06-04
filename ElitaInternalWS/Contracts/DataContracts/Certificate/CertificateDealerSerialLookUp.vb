Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem

<DataContract(Name:="CertificateDealerSerialLookUp", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertificateDealerSerialLookUp
    Inherits CertificateLookup

    <DataMember(Name:="SerialNumber", IsRequired:=True), StringLength(30, ErrorMessage:="SerialNumber value exceeded maximum length of 30"),
        Required(ErrorMessage:="SerialNumber is Required")>
    Public Property SerialNumber As String

    <DataMember(Name:="DealerCode", IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5"),
   Required(ErrorMessage:="DealerCode is Required")>
    Public Property DealerCode As String

End Class
