Imports System.Runtime.Serialization
<Serializable>
Public Class InvalidCauseOfLossException
    Inherits Exception

    Public ReadOnly Claim As String
    Public ReadOnly Certificate As String
    Public ReadOnly CompanyId As Guid?

    Public Sub New(pClaimNumber As String, pCertificate As String, pCompanyId As Guid?)
        Claim = pClaimNumber
        Certificate = pCertificate
        CompanyId = pCompanyId
    End Sub
    Public Sub New(pClaimNumber As String, pCertificate As String, pCompanyId As Guid?, pMessage As String)
        MyBase.New(pMessage)
        Claim = pClaimNumber
        Certificate = pCertificate
        CompanyId = pCompanyId
    End Sub
    Public Sub New(pClaimNumber As String, pCertificate As String, pCompanyId As Guid?, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        Claim = pClaimNumber
        Certificate = pCertificate
        CompanyId = pCompanyId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class


