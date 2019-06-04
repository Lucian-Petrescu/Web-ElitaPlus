Imports System.Runtime.Serialization

<DataContract(Name:="PreInvoiceDetailsFaults", Namespace:="http://elita.assurant.com/SNMPortal")> _
Public Class SNMPortalFaultDC

#Region "DataMembers"

    Private _FaultReason As String
    <DataMember(Name:="FaultReason")> _
    Public Property FaultReason() As String
        Get
            Return _FaultReason
        End Get
        Set(ByVal value As String)
            _FaultReason = value
        End Set
    End Property

#End Region
End Class
