Imports System.Runtime.Serialization

<Serializable>
Public Class CompanyNotFoundException
    Inherits Exception

    Public ReadOnly CompanyCode As String
    Public ReadOnly CompanyId As Nullable(Of Guid)

    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pCompanyCode As String)
        CompanyCode = pCompanyCode
        CompanyId = pCompanyId
    End Sub
    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pCompanyCode As String, pMessage As String)
        MyBase.New(pMessage)
        CompanyCode = pCompanyCode
        CompanyId = pCompanyId
    End Sub
    Public Sub New(ByVal pCompanyId As Nullable(Of Guid), ByVal pCompanyCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        CompanyCode = pCompanyCode
        CompanyId = pCompanyId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

