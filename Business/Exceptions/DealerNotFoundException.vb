Imports System.Runtime.Serialization

<Serializable>
Public Class DealerNotFoundException
    Inherits Exception

    Public ReadOnly DealerCode As String
    Public ReadOnly DealerId As Guid?

    Public Sub New(pDealerId As Guid?, pDealerCode As String)
        DealerCode = pDealerCode
        DealerId = pDealerId
    End Sub
    Public Sub New(pDealerId As Guid?, pDealerCode As String, pMessage As String)
        MyBase.New(pMessage)
        DealerCode = pDealerCode
        DealerId = pDealerId
    End Sub
    Public Sub New(pDealerId As Guid?, pDealerCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        DealerCode = pDealerCode
        DealerId = pDealerId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
