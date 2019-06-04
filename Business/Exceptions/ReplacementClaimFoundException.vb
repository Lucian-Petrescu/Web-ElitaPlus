Imports System.Runtime.Serialization

<Serializable>
Public Class ReplacementClaimFoundException
    Inherits Exception

    Public ReadOnly ClaimNumber As String
    Public ReadOnly CompanyId As Nullable(Of Guid)


    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pClaimNUmber As String)
        ClaimNumber = pClaimNUmber
        CompanyId = pCompanyId
    End Sub
    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pClaimNUmber As String, pMessage As String)
        MyBase.New(pMessage)
        ClaimNumber = pClaimNUmber
        CompanyId = pCompanyId
    End Sub
    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pClaimNUmber As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ClaimNumber = pClaimNUmber
        CompanyId = pCompanyId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class

