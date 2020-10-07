Imports System.Runtime.Serialization

Public Class CertificateItemNotFoundException
    Inherits Exception

    Public ReadOnly ItemCode As String
    Public ReadOnly CertificateItemId As Guid?

    Public Sub New(pCertificateItemId As Guid?, pItemCode As String)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Public Sub New(pCertificateItemId As Guid?, pItemCode As String, pMessage As String)
        MyBase.New(pMessage)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Public Sub New(pCertificateItemId As Guid?, pItemCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
