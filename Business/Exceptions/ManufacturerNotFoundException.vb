
Imports System.Runtime.Serialization

<Serializable>
Public Class ManufacturerNotFoundException
    Inherits Exception

    Public ReadOnly ManufacturerCode As String
    Public ReadOnly ManufacturerId As Guid?

    Public Sub New(pManufacturerId As Guid?, pManufacturerCode As String)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Public Sub New(pManufacturerId As Guid?, pManufacturerCode As String, pMessage As String)
        MyBase.New(pMessage)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Public Sub New(pManufacturerId As Guid?, pManufacturerCode As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        ManufacturerCode = pManufacturerCode
        ManufacturerId = pManufacturerId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

