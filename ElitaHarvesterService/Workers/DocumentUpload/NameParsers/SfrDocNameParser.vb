Imports System.IO
Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Namespace Workers.DocumentUpload.NameParsers

    Public Class SfrDocNameParser
        Inherits DocumentNameParser

        Private Shared ReadOnly DocTypeMapping As IReadOnlyDictionary(Of String, String) = New Dictionary(Of String, String) From {{"CERTIFGAR", "BA"}, {"AUTOPREL", "SEPA"}, {"CHEQUE", "CHECK"}}

        Private Shared ReadOnly FatalStatus As IReadOnlyList(Of String) = New List(Of String) From {"ST05", "CST05"}

        Private Const FatalFolder As String = "Fatal"

        Private Const NotReadableFolder As String = "NotReadable"

        Public Sub New()
            CompanyCode = "AIF"
            DealerFileType = "SFR"
            GeneratesOutput = True
            NameFormat = New Regex("^(?<certificate>SFRPC\d{10})[_](?<docType>[A-Za-z]+)[_]((?<custName>[\p{L}'-]*((\s)*[\p{L}'-]*)+)[_](?<checkNo>\d+)[_](?<amount>\d+(?:.\d{2}))[_])?(?<date>(19|20)\d\d(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01]))[_](?<status>(ST|CST)\d{2})[.]",
                                   RegexOptions.None,
                                   TimeSpan.FromSeconds(.5))
            LayoutTuple = New Tuple(Of String, String)("NA", ";")
        End Sub

        Public Sub New(file As FileInfo)
            Me.New() 
            Document = file
        End Sub

        Protected Overrides Function GetFileNameProperties() As FileNameData
            Dim locData = MyBase.GetFileNameProperties()
            Dim docType = ""
            locData.DocumentType = If(DocTypeMapping.TryGetValue(locData.DocumentType, docType), docType, locData.DocumentType)
            Return locData
        End Function

        Public Overrides Function ValidSize(ByVal Optional fileSize As Long = 10485760) As Boolean
            Dim result As Boolean = MyBase.ValidSize(fileSize)
            If Not result Then ErrorTuple = New Tuple(Of String, String)(ErrorTuple.Item1, NotReadableFolder)
            Return result
        End Function

        Public Overrides Function ValidDocumentType() As Guid
            Dim docType As String = string.Empty
            Dim documentTypeId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_DOCUMENT_TYPES, If(DocTypeMapping.TryGetValue(FileData.DocumentType, docType), docType, FileData.DocumentType))
            If documentTypeId = Guid.Empty Then
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_DocumentTypeNotFound", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, NotReadableFolder)
            End If

            Return documentTypeId
        End Function

        Public Overrides Function ValidCertificate() As Guid
            Dim certificateId = MyBase.ValidCertificate()
            If certificateId = Guid.Empty Then ErrorTuple = New Tuple(Of String, String)(ErrorTuple.Item1, NotReadableFolder)
            Return certificateId
        End Function

        Public Overrides Function ValidStatus() As Boolean
            If FatalStatus.Contains(FileData.Status) Then
                Dim errorFileName As String = String.Format("{0}{1}{2}", Document.Name.Substring(0, Document.Name.Length - Document.Extension.Length), "_InvalidStatus", Document.Extension)
                ErrorTuple = New Tuple(Of String, String)(errorFileName, FatalFolder)
                Return False
            End If

            Return True
        End Function
    End Class
End Namespace
