
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business

<DataContract(Name:="CertificateBelongsToDiffDealer", Namespace:="http://elita.assurant.com/Faults")>
Public Class CertificateBelongsToDiffDealer
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.CertificateBelongsToDiffDealer)
    End Sub

    ' Public Sub New(ByVal pDealerNotfound As DealerNotFoundException)
    'Me.DealerCode = pDealerNotfound.DealerCode
    'End Sub

    <DataMember(Name:="DealerCode", IsRequired:=True)>
    Public Property DealerCode As String

End Class

