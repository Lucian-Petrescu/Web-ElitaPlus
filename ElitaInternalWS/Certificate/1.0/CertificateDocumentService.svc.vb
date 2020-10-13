Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaInternalWS

Namespace Certificates
    Public Class CertificateDocumentServiceV1
        Implements ICertificateDocumentServiceV1

        Public Function AttachDocument(request As AttachCertificateDocumentRequest) As AttachCertificateDocumentResponse Implements ICertificateDocumentServiceV1.AttachDocument
            ExtensionMethods.Validate(request)

            Dim oCertificate As Certificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch, CertificateDetailTypes.None)

            Dim documentTypeId As Nullable(Of Guid) = Nothing
            If ((request.DocumentType IsNot Nothing) AndAlso (request.DocumentType.Trim.Length > 0)) Then
                documentTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), request.DocumentType.Trim.ToUpperInvariant())
            End If

            Dim response As New AttachCertificateDocumentResponse

            Try

                response.ImageId =
                    oCertificate.AttachImage(pDocumentTypeId:=documentTypeId,
                                       pScanDate:=request.ScanDate,
                                       pFileName:=request.FileName,
                                       pComments:=request.Comments,
                                       pUserName:=request.UserName,
                                       pImageData:=request.ImageData)

            Catch ex As BOValidationException
                Throw New FaultException(Of ValidationFault)(ex.ToValidationFault())
            Catch ex As DocumentFormatNotFound
                Throw New FaultException(Of FileTypeNotValidFault)(New FileTypeNotValidFault)
            Catch ex As ImageRepositoryNotFound
                Throw New FaultException(Of ImageRepositoryNotFoundFault)(New ImageRepositoryNotFoundFault() With {.RepositoryCode = ex.RepositoryName})
            Catch ex As FileIntegrityFailedException
                Throw New FaultException(Of FileIntegrityFailedFault)(New FileIntegrityFailedFault() With
                                                                      {
                                                                          .AbsoluteFileName = ex.AbsoluteFileName,
                                                                          .ComputedHash = ex.ComputedHash,
                                                                          .RepositoryCode = ex.RepositoryCode,
                                                                          .StoragePath = ex.StoragePath,
                                                                          .StoredHash = ex.StoredHash
                                                                      })
            End Try

            Return response
        End Function

        Public Function DownloadDocument(request As DownloadCertificateDocumentRequest) As DownloadCertificateDocumentResponse Implements ICertificateDocumentServiceV1.DownloadDocument
            Dim response As New DownloadCertificateDocumentResponse()

            Dim oCertificate As Certificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch, CertificateDetailTypes.None)

            For Each ci As CertImage In oCertificate.CertificateImagesList
                If (ci.ImageId = request.ImageId) Then
                    response.ImageData = oCertificate.Company.GetCertificateImageRepository().Download(request.ImageId).Data
                    Return response
                End If
            Next

            Throw New FaultException(Of ClaimImageNotFoundFault)(New ClaimImageNotFoundFault() With {.ImageId = request.ImageId, .RepositoryCode = oCertificate.Company.ClaimsImageRepositoryCode})
        End Function

        Public Function GetCertificateDocuments(request As GetCertificateDocumentsRequest) As GetCertificateDocumentsResponse _
            Implements ICertificateDocumentServiceV1.GetCertificateDocuments
            Dim oCertificate As Certificate

            ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
            ExtensionMethods.Validate(request)

            ' Find Certificate based on Request
            Try
                oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch, CertificateDetailTypes.None)
            Catch ex As CertificateNotFoundException
                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = ex.CertificateSearch}, "Certificate Not Found")
            End Try

            Dim response As New GetCertificateDocumentsResponse
            Dim documentInfos As New List(Of CertificateDocumentInfo)

            For Each ci As CertImage In oCertificate.CertificateImagesList
                Dim cii As New CertificateDocumentInfo

                cii.ImageId = ci.ImageId
                cii.DocumentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, ci.DocumentTypeId)
                cii.ScanDate = ci.ScanDate
                cii.FileName = ci.FileName
                cii.Comments = ci.Comments
                cii.FileSizeBytes = ci.FileSizeBytes

                documentInfos.Add(cii)
            Next

            response.Documents = documentInfos

            Return response
        End Function
    End Class
End Namespace
