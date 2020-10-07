Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew

Namespace Claims
    <ServiceBehavior(Namespace:="http://elita.assurant.com/Claim")> _
    Public Class ClaimDocumentServiceV2
        Implements IClaimDocumentServiceV2

        Public Function AttachDocument(request As AttachDocumentRequest) As AttachDocumentResponse Implements IClaimDocumentServiceV2.AttachDocument

            ExtensionMethods.Validate(request)

            Dim oClaim As ClaimBase = ClaimServiceHelper.GetClaim(request.ClaimsSearch)

            Dim documentTypeId As Nullable(Of Guid) = Nothing
            If ((Not request.DocumentType Is Nothing) AndAlso (request.DocumentType.Trim.Length > 0)) Then
                documentTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), request.DocumentType.Trim.ToUpperInvariant())
            End If

            Dim response As New AttachDocumentResponse

            Try

                response.ImageId =
                    oClaim.AttachImage(pDocumentTypeId:=documentTypeId, _
                                       pImageStatusId:=Nothing, _
                                       pScanDate:=request.ScanDate, _
                                       pFileName:=request.FileName, _
                                       pComments:=request.Comments, _
                                       pUserName:=request.UserName, _
                                       pImageData:=request.ImageData)

            Catch ex As BOValidationException
                Throw New FaultException(Of ValidationFault)(ex.ToValidationFault())
            Catch ex As DocumentFormatNotFound
                Throw New FaultException(Of FileTypeNotValidFault)(New FileTypeNotValidFault)
            Catch ex As ImageRepositoryNotFound
                Throw New FaultException(Of ImageRepositoryNotFoundFault)(New ImageRepositoryNotFoundFault() With {.RepositoryCode = ex.RepositoryName})
            Catch ex As FileIntegrityFailedException
                Throw New FaultException(Of FileIntegrityFailedFault)(New FileIntegrityFailedFault() With _
                                                                      { _
                                                                          .AbsoluteFileName = ex.AbsoluteFileName, _
                                                                          .ComputedHash = ex.ComputedHash, _
                                                                          .RepositoryCode = ex.RepositoryCode, _
                                                                          .StoragePath = ex.StoragePath, _
                                                                          .StoredHash = ex.StoredHash _
                                                                      })
            End Try

            oClaim.Save()

            Return response

        End Function

        Public Function DownloadDocument(request As DownloadDocumentRequest) As DownloadDocumentResponse Implements IClaimDocumentServiceV2.DownloadDocument
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


