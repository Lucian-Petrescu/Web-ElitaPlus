Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew



<DataContract(Name:="CertificateLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class CertificateLookup
    Friend Overridable Sub Validate(pCert As Certificate)
    End Sub
    Friend Overridable Sub Validate()
    End Sub

    Friend Overridable Sub ValidateIdentificatonNumer(pCert As Certificate)
    End Sub
End Class