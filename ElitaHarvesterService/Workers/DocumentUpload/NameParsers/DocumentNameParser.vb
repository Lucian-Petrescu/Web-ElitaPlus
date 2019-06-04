Imports System.IO
Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Namespace Workers.DocumentUpload.NameParsers
    Public MustInherit Class DocumentNameParser

        Private _fileData As FileNameData

        Public Property CompanyCode As String

        Public Property DealerFileType As String

        Public Property GeneratesOutput As Boolean

        Public Property NameFormat As Regex

        Public Property Document As FileInfo

        Public Property LayoutTuple As Tuple(Of String, String)

        Public Property ErrorTuple As Tuple(Of String, String)

        'Public ReadOnly Property FileData As FileNameData
        '    Get
        '        Return If(_fileData, (__InlineAssignHelper(_fileData, GetFileNameProperties())))
        '    End Get
        'End Property

        Public ReadOnly Property FileData As FileNameData
            Get
                If _fileData Is Nothing Then
                    _fileData = GetFileNameProperties()
                End If
                Return _fileData
            End Get
        End Property

        Protected Overridable Function GetFileNameProperties() As FileNameData
            Dim parsedCheckNumber As Integer
            Dim parsedAmount As Decimal
            Return New FileNameData With {
                .DealerFileType = DealerFileType, 
                .CertificateNumber = NameFormat.Match(Document.Name).Groups("certificate").ToString(), 
                .DocumentType = NameFormat.Match(Document.Name).Groups("docType").ToString(), 
                .CustomerName = NameFormat.Match(Document.Name).Groups("custName").ToString(), 
                .CheckNumber = If(Integer.TryParse(NameFormat.Match(Document.Name).Groups("checkNo").ToString(), parsedCheckNumber), CType(parsedCheckNumber, Integer?), Nothing), 
                .Amount = If(Decimal.TryParse(NameFormat.Match(Document.Name).Groups("amount").ToString(), parsedAmount), CType(parsedAmount, Decimal?), Nothing), 
                .FileDate = DateTime.ParseExact(NameFormat.Match(Document.Name).Groups("date").ToString(), "yyyyMMdd", Nothing), 
                .Status = NameFormat.Match(Document.Name).Groups("status").ToString()}
        End Function

        Public Overridable Function ValidSize(ByVal Optional fileSize As Long = 10485760) As Boolean
            If Document.Length > fileSize Then
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_FileSizeIssue", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, String.Empty)
                Return False
            End If

            Return True
        End Function

        Public Overridable Function ValidProperties() As Boolean
            If String.IsNullOrWhiteSpace(FileData.CertificateNumber) OrElse String.IsNullOrWhiteSpace(FileData.DocumentType) Then
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_EmptyProperties", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, String.Empty)
                Return False
            End If

            Return True
        End Function

        Public Overridable Function ValidCertificate() As Guid
            Dim certificateId As Guid = Guid.Empty
            Try
                certificateId = CertificateFacade.GetCertificatebyCertNumberAndCompanyCode(FileData.CertificateNumber, CompanyCode)
            Catch
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_CertificateNotFound", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, String.Empty)
            End Try

            Return certificateId
        End Function

        Public Overridable Function ValidDocumentType() As Guid
            Dim documentTypeId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_DOCUMENT_TYPES, FileData.DocumentType)
            If documentTypeId = Guid.Empty Then
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_DocumentTypeNotFound", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, String.Empty)
            End If

            Return documentTypeId
        End Function

        Public Overridable Function ValidStatus() As Boolean
            Return True
        End Function

        '<Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        'Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        '    target = value
        '    Return value
        'End Function
    End Class
End Namespace
