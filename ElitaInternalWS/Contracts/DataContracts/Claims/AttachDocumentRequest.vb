Imports System.Runtime.Serialization
Imports Assurant.Common.Validation
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="AttachDocumentRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class AttachDocumentRequest

    <DataMember(Name:="ClaimsSearch", IsRequired:=True)> _
    Public Property ClaimsSearch As ClaimLookup

    <DataMember(Name:="DocumentType", IsRequired:=False)> _
    Public Property DocumentType As String

    <DataMember(Name:="Comments", IsRequired:=False), _
    StringLength(4000, ErrorMessage:="The Value Is Either Too Short Or Too Long")> _
    Public Property Comments As String

    <DataMember(Name:="FileName", IsRequired:=True), _
    Required(ErrorMessage:="The Value Is Required"), StringLength(100, ErrorMessage:="The Value Is Either Too Short Or Too Long")> _
    Public Property FileName As String

    <DataMember(Name:="ScanDate", IsRequired:=False)> _
    Public Property ScanDate As Nullable(Of Date)

    <DataMember(Name:="UserName", IsRequired:=True), _
    Required(ErrorMessage:="The Value Is Required"), StringLength(100, ErrorMessage:="The Value Is Either Too Short Or Too Long")> _
    Public Property UserName As String

    <DataMember(Name:="ImageData", IsRequired:=True), _
     ValidateImageDataLength(1, 1024 * 1024 * 10, ErrorMessage:="File Size should be between 1 Byte and 10 MB")> _
    Public Property ImageData As Byte()

    Public Class ValidateImageDataLength
        Inherits ValidationAttribute

        Private ReadOnly _minimumLength As Integer
        Private ReadOnly _maximumLength As Integer

        Public Sub New(ByVal pMinimumLength As Integer, ByVal pMaximumLength As Integer)
            Me._maximumLength = pMinimumLength
            Me._maximumLength = pMaximumLength
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

        Public Overrides Function IsValid(ByVal value As Object) As Boolean
            If (value Is Nothing) Then Return False

            Dim imageData() As Byte

            Try
                imageData = DirectCast(value, Byte())
            Catch ex As Exception
                Return False
            End Try

            If (imageData.Length < Me.MinimumLength) Then Return False

            If (imageData.Length > Me.MaximumLength) Then Return False

            Return True

        End Function
    End Class

End Class
