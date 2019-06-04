Imports System.Runtime.Serialization

Public Class CertificateItemNotFoundException
    Inherits Exception

    Public ReadOnly ItemCode As String
    Public ReadOnly CertificateItemId As Nullable(Of Guid)

    Public Sub New(ByVal pCertificateItemId As Nullable(Of Guid), ByVal pItemCode As String)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Public Sub New(ByVal pCertificateItemId As Nullable(Of Guid), ByVal pItemCode As String, pMessage As String)
        MyBase.New(pMessage)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Public Sub New(ByVal pCertificateItemId As Nullable(Of Guid), ByVal pItemCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ItemCode = pItemCode
        CertificateItemId = pCertificateItemId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
