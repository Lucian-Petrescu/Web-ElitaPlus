Imports System.Runtime.Serialization
Imports Assurant.Common.Validation
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="AttachCertificateDocumentRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class AttachCertificateDocumentRequest

    <DataMember(Name:="CertificateSearch", IsRequired:=True)>
    Public Property CertificateSearch As CertificateLookup

    <DataMember(Name:="DocumentType", IsRequired:=False)>
    Public Property DocumentType As String

    <DataMember(Name:="Comments", IsRequired:=False),
    StringLength(4000, ErrorMessage:="The Value Is Either Too Short Or Too Long")>
    Public Property Comments As String

    <DataMember(Name:="FileName", IsRequired:=True),
    Required(ErrorMessage:="The Value Is Required"), StringLength(100, ErrorMessage:="The Value Is Either Too Short Or Too Long")>
    Public Property FileName As String

    <DataMember(Name:="ScanDate", IsRequired:=False)>
    Public Property ScanDate As Nullable(Of Date)

    <DataMember(Name:="UserName", IsRequired:=True),
    Required(ErrorMessage:="The Value Is Required"), StringLength(100, ErrorMessage:="The Value Is Either Too Short Or Too Long")>
    Public Property UserName As String

    <DataMember(Name:="ImageData", IsRequired:=True),
     ValidateImageDataLength(1, 1024 * 1024 * 10, ErrorMessage:="File Size should be between 1 Byte and 10 MB")>
    Public Property ImageData As Byte()

    Public Class ValidateImageDataLength
        Inherits ValidationAttribute

        Private ReadOnly _minimumLength As Integer
        Private ReadOnly _maximumLength As Integer

        Public Sub New(pMinimumLength As Integer, pMaximumLength As Integer)
            _maximumLength = pMinimumLength
            _maximumLength = pMaximumLength
        End Sub

        Public ReadOnly Property MinimumLength As Integer
            Get
                Return _minimumLength
            End Get
        End Property

        Public ReadOnly Property MaximumLength As Integer
            Get
                Return _maximumLength
            End Get
        End Property

        Public Overrides Function IsValid(value As Object) As Boolean
            If (value Is Nothing) Then Return False

            Dim imageData() As Byte

            Try
                imageData = DirectCast(value, Byte())
            Catch ex As Exception
                Return False
            End Try

            If (imageData.Length < MinimumLength) Then Return False

            If (imageData.Length > MaximumLength) Then Return False

            Return True

        End Function
    End Class

End Class
