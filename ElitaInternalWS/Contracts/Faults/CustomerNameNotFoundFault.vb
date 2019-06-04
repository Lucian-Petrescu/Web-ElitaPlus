
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business

<DataContract(Name:="CustomerNameNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class CustomerNameNotFoundFault
    Inherits ElitaFault
    Public Sub New()
        MyBase.New(EnumFaultType.CustomerNameNotFound)
    End Sub

    ' Public Sub New(ByVal pDealerNotfound As DealerNotFoundException)
    'Me.DealerCode = pDealerNotfound.DealerCode
    'End Sub

    <DataMember(Name:="CustomerName", IsRequired:=True)>
    Public Property CustomerName As String

End Class
