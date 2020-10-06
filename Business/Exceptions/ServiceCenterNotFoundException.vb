Imports System.Runtime.Serialization

<Serializable>
Public Class ServiceCenterNotFoundException
    Inherits Exception

    Public ReadOnly SvcCenterCode As String
    Public ReadOnly CountryId As Guid?

    Public Sub New(pCountryId As Guid?, pSvcCenterCode As String)
        SvcCenterCode = pSvcCenterCode
        CountryId = pCountryId
    End Sub
    Public Sub New(pCountryId As Guid?, pSvcCenterCode As String, pMessage As String)
        MyBase.New(pMessage)
        SvcCenterCode = pSvcCenterCode
        CountryId = pCountryId
    End Sub
    Public Sub New(pCountryId As Guid?, pSvcCenterCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        SvcCenterCode = pSvcCenterCode
        CountryId = pCountryId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

