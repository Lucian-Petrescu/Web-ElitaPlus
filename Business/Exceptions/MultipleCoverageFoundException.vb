
Imports System.Runtime.Serialization

<Serializable>
Public Class MultipleCoverageFoundException
    Inherits Exception

    Public ReadOnly CoverageTypeCode As String
    Public ReadOnly CoverageTypeId As Nullable(Of Guid)

    Public Sub New(ByVal pCoverageTypeId As Nullable(Of Guid), ByVal pCoverageTypeCode As String)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Public Sub New(ByVal pCoverageTypeId As Nullable(Of Guid), ByVal pCoverageTypeCode As String, pMessage As String)
        MyBase.New(pMessage)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Public Sub New(ByVal pCoverageTypeId As Nullable(Of Guid), ByVal pCoverageTypeCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        CoverageTypeCode = pCoverageTypeCode
        CoverageTypeId = pCoverageTypeId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class


