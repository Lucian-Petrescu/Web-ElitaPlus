Imports System.Runtime.Serialization

<Serializable>
Public Class CoverageNotFoundException
    Inherits Exception

    Public ReadOnly CoverageTypeCode As String
    Public ReadOnly CoverageTypeId As Guid?

    Public Sub New(pCoverageTypeId As Guid?, pCoverageTypeCode As String)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Public Sub New(pCoverageTypeId As Guid?, pCoverageTypeCode As String, pMessage As String)
        MyBase.New(pMessage)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Public Sub New(pCoverageTypeId As Guid?, pCoverageTypeCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
