Imports System.Runtime.Serialization
<Serializable>
Public Class PriceListNotConfiguredException
    Inherits Exception

    Public ReadOnly ServiceCenterCode As String
    Public ReadOnly PriceListId As Nullable(Of Guid)

    Public Sub New(ByVal pPriceListId As Nullable(Of Guid), ByVal pServiceCenterCode As String)
        ServiceCenterCode = pServiceCenterCode
        PriceListId = pPriceListId
    End Sub
    Public Sub New(ByVal pPriceListId As Nullable(Of Guid), ByVal pServiceCenterCode As String, pMessage As String)
        MyBase.New(pMessage)
        ServiceCenterCode = pServiceCenterCode
        PriceListId = pPriceListId
    End Sub
    Public Sub New(ByVal pPriceListId As Nullable(Of Guid), ByVal pServiceCenterCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ServiceCenterCode = pServiceCenterCode
        PriceListId = pPriceListId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class


