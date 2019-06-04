
Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Namespace Claims
    <ServiceBehavior(Namespace:="http://elita.assurant.com/Claim")>
    Public Class ClaimDocumentServiceV1
        Implements IClaimDocumentServiceV1

        Public Function GetElitaHeader() As ElitaHeader Implements IClaimDocumentServiceV1.GetElitaHeader
            Throw New NotSupportedException()
        End Function

        Public Function AttachDocument(ByVal request As AttachDocumentRequest) As AttachDocumentResponse Implements IClaimDocumentServiceV1.AttachDocument

            ExtensionMethods.Validate(request)

            Dim oClaim As ClaimBase = ClaimServiceHelper.GetClaim(request.ClaimsSearch)

            Dim documentTypeId As Nullable(Of Guid) = Nothing
            If ((Not request.DocumentType Is Nothing) AndAlso (request.DocumentType.Trim.Length > 0)) Then
                documentTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), request.DocumentType.Trim.ToUpperInvariant())
            End If

            Dim response As New AttachDocumentResponse

            Try

                response.ImageId =
                    oClaim.AttachImage(pDocumentTypeId:=documentTypeId,
                                       pImageStatusId:=Nothing,
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

            oClaim.Save()

            Return response

        End Function

        Public Function AttachDocumentMultiThread(request As AttachDocumentRequest) As AttachDocumentResponse Implements IClaimDocumentServiceV1.AttachDocumentMultiThread
            Validate(request)

            Dim oClaim As ClaimBase = GetClaim(request.ClaimsSearch)

            Dim pDocumentTypeId As Guid? = Nothing
            If ((Not request.DocumentType Is Nothing) AndAlso (request.DocumentType.Trim.Length > 0)) Then
                pDocumentTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), request.DocumentType.Trim.ToUpperInvariant())
            End If

            Dim response As New AttachDocumentResponse

            Try

                ' Check if Document Type ID is supplied otherwise default to Other
                If (Not pDocumentTypeId.HasValue) Then
                    pDocumentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Codes.DOCUMENT_TYPE__OTHER)
                End If

                ' Check if Image Status ID is supplied otherwise default to Pending

                Dim pImageStatusId As Guid? = LookupListNew.GetIdFromCode(LookupListNew.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PENDING)

                ' Check if Scan Date is supplied otherwise default to Current Date 
                Dim pScanDate As Date? = request.ScanDate
                If (Not pScanDate.HasValue) Then
                    pScanDate = DateTime.Today
                End If

                Dim oClaimImage As New ClaimImage()
                oClaimImage.ClaimId = oClaim.Id

                Dim pImageData As Byte() = request.ImageData

                With oClaimImage
                    .DocumentTypeId = pDocumentTypeId.Value
                    .ImageStatusId = pImageStatusId.Value
                    .ScanDate = pScanDate.Value
                    .FileName = request.FileName
                    .Comments = request.Comments
                    .UserName = request.UserName
                    .FileSizeBytes = pImageData.Length
                    .ImageId = Guid.NewGuid()
                    .IsLocalRepository = Codes.YESNO_Y
                End With

                Try

                    ' This is to avoid any orphan images because of Elita Validation
                    oClaimImage.Validate()

                    Dim oRepository As Documents.Repository = oClaim.Company.GetClaimImageRepository()
                    Dim doc As Documents.Document = oRepository.NewDocument
                    With doc
                        .Data = pImageData
                        .FileName = request.FileName
                        .FileType = request.FileName.Split(New Char() {"."}).Last()
                        .FileSizeBytes = pImageData.Length
                    End With

                    oRepository.Upload(doc)
                    oClaimImage.ImageId = doc.Id

                    oClaimImage.ImageStatusId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLM_IMG_STATUS, Codes.CLAIM_IMAGE_PROCESSED)

                    oClaimImage.Save()

                    response.ImageId = doc.Id
                Catch ex As Exception
                    oClaimImage.Delete()
                    Throw
                End Try

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

        Public Function DownloadDocument(ByVal request As DownloadDocumentRequest) As DownloadDocumentResponse Implements IClaimDocumentServiceV1.DownloadDocument
            Dim response As New DownloadDocumentResponse()

            Dim oClaim As ClaimBase = ClaimServiceHelper.GetClaim(request.ClaimsSearch)

            For Each ci As ClaimImage In oClaim.ClaimImagesList
                If (ci.ImageId = request.ImageId) Then
                    response.ImageData = oClaim.Company.GetClaimImageRepository().Download(request.ImageId).Data
                    Return response
                End If
            Next

            Throw New FaultException(Of ClaimImageNotFoundFault)(New ClaimImageNotFoundFault() With {.ImageId = request.ImageId, .RepositoryCode = oClaim.Company.ClaimsImageRepositoryCode})

        End Function

    End Class
End Namespace


