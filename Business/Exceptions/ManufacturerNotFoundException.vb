
Imports System.Runtime.Serialization

<Serializable>
Public Class ManufacturerNotFoundException
    Inherits Exception

    Public ReadOnly ManufacturerCode As String
    Public ReadOnly ManufacturerId As Nullable(Of Guid)

    Public Sub New(ByVal pManufacturerId As Nullable(Of Guid), ByVal pManufacturerCode As String)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Public Sub New(ByVal pManufacturerId As Nullable(Of Guid), ByVal pManufacturerCode As String, pMessage As String)
        MyBase.New(pMessage)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Public Sub New(ByVal pManufacturerId As Nullable(Of Guid), ByVal pManufacturerCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

