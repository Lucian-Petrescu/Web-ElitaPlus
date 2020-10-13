Imports System.Linq
Imports Assurant.ElitaPlus.DALObjects
Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ComponentModel.DataAnnotations

Namespace Claims
    <ServiceBehavior(Namespace:="http://elita.assurant.com/Claim")>
    Public Class ClaimServiceV1
        Implements IClaimServiceV1

        Public Function GetElitaHeader() As ElitaHeader Implements IClaimServiceV1.GetElitaHeader
            Throw New NotSupportedException()
        End Function

        Public Function UpdateClaim(request As UpdateClaimRequest) As UpdateClaimResponse _
            Implements IClaimServiceV1.UpdateClaim

            ExtensionMethods.Validate(request)

            Dim oClaim As ClaimBase = ClaimServiceHelper.GetClaim(request.ClaimsSearch)

            For Each co As ClaimOperation In request.Operations
                co.Execute(oClaim)
            Next

            oClaim.Save()

            Return New UpdateClaimResponse() With {.ClaimsUpdated = 1}

        End Function

        Public Function GetClaims(request As GetClaimsRequest) As GetClaimsResponse _
            Implements IClaimServiceV1.GetClaims

            Dim returnValue As New GetClaimsResponse

            ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
            ExtensionMethods.Validate(request)

            Dim TypeOfSearch As Type = request.ClaimsSearch.GetType()

            If (TypeOfSearch Is GetType(ClaimSerialNumberLookup)) Then
                Return GetClaimsFromSerialNumberSearch(request)

            ElseIf (TypeOfSearch Is GetType(ClaimImeiNumberLookup)) Then
                Return GetClaimsFromImeiNumberSearch(request)

            End If

            Throw New NotSupportedException()
        End Function

        Public Function GetClaimDetails(request As GetClaimDetailsRequest) As GetClaimDetailsResponse Implements IClaimServiceV1.GetClaimDetails

            Dim response As New GetClaimDetailsResponse

            Dim oClaim As ClaimBase = ClaimServiceHelper.GetClaim(request.ClaimsSearch)

            Dim imageInfos As New List(Of ClaimImageInfo)

            For Each ci As ClaimImage In oClaim.ClaimImagesList
                Dim cii As New ClaimImageInfo

                cii.ImageId = ci.ImageId
                cii.DocumentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, ci.DocumentTypeId)
                cii.ImageStatus = LookupListNew.GetCodeFromId(LookupListNew.LK_CLM_IMG_STATUS, ci.ImageStatusId)
                cii.ScanDate = ci.ScanDate
                cii.FileName = ci.FileName
                cii.Comments = ci.Comments
                cii.FileSizeBytes = ci.FileSizeBytes

                imageInfos.Add(cii)
            Next

            response.Images = imageInfos

            Return response

        End Function

        Private Function GetClaimsFromSerialNumberSearch(request As GetClaimsRequest) As GetClaimsResponse

            Dim lookupParameters As ClaimSerialNumberLookup = DirectCast(request.ClaimsSearch, ClaimSerialNumberLookup)
            lookupParameters.Validate()

            ValidateDealerCode(lookupParameters.DealerCode)

            Dim ClaimsInfo As DataSet = ClaimBase.LoadClaimsBySerialNumber(lookupParameters.CountryCode, lookupParameters.CompanyCode, lookupParameters.DealerCode, lookupParameters.SerialNumber)
            Return ClaimsResponse(ClaimsInfo)

        End Function

        'Note: This function searchs for imei numbers in both the imei and serial number columns (aka - virtual search_imei_serialnumber column)
        Private Function GetClaimsFromImeiNumberSearch(request As GetClaimsRequest) As GetClaimsResponse

            Dim lookupParameters As ClaimImeiNumberLookup = DirectCast(request.ClaimsSearch, ClaimImeiNumberLookup)
            lookupParameters.Validate()

            ValidateDealerCode(lookupParameters.DealerCode)

            Dim ClaimsInfo As DataSet = ClaimBase.LoadClaimsByImeiNumber(lookupParameters.CountryCode, lookupParameters.CompanyCode, lookupParameters.DealerCode, lookupParameters.ImeiNumber)
            Return ClaimsResponse(ClaimsInfo)

        End Function

        Private Sub ValidateDealerCode(DealerCode As String)

            If Not String.IsNullOrEmpty(DealerCode) Then
                Dim dvDealers As DataView = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, DealerCode)

                If (dvDealers.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = DealerCode}, "Dealer Not Found")
                End If
            End If

        End Sub

        Private Function ClaimsResponse(ClaimsInfo As DataSet) As GetClaimsResponse
            Dim returnValue As New GetClaimsResponse

            Dim claimCount As Integer = 0

            returnValue.Claims = New List(Of ClaimInfo)()

            If ClaimsInfo IsNot Nothing AndAlso ClaimsInfo.Tables.Count > 0 Then
                Dim returnClaim = New List(Of ClaimInfo)()

                Dim dtClaimsInfo As List(Of DataTable) = ClaimsInfo.Tables(0).AsEnumerable().GroupBy(Function(i) i.Field(Of System.String)("claim_number")).Select(Function(g) g.CopyToDataTable()).ToList()

                For Each dtClaimInfo As DataTable In dtClaimsInfo
                    Dim extendedStatusList As New List(Of String)
                    Dim fulfillmentDate As DateTime? = Nothing
                    Dim extendedStatusCount As Integer = 0
                    Dim returnClaimStatuses = New List(Of ExtendedStatus)()

                    For Each row As DataRow In dtClaimInfo.Rows
                        extendedStatusCount = extendedStatusCount + 1
                        If row.Field(Of String)("EXTENDED_STATUS") IsNot Nothing AndAlso (Not extendedStatusList.Contains(row.Field(Of String)("EXTENDED_STATUS"))) Then
                            returnClaimStatuses.Add(New ExtendedStatus() With
                                {
                                  .Code = row.Field(Of String)("EXTENDED_STATUS"),
                                  .CreatedDate = row.Field(Of Date?)("CREATED_DATE")
                                })
                            extendedStatusList.Add(row.Field(Of String)("EXTENDED_STATUS"))
                        End If

                        If Not fulfillmentDate.HasValue Then
                            fulfillmentDate = row.Field(Of Date?)("CLAIM_FULLFILLMENT_DATE")
                        End If

                        If extendedStatusCount = dtClaimInfo.Rows.Count Then
                            returnClaim.Add(New ClaimInfo() With
                                {
                                  .ClaimNumber = row.Field(Of String)("CLAIM_NUMBER"),
                                  .CompanyCode = row.Field(Of String)("COMPANY_CODE"),
                                  .CertificateNumber = row.Field(Of String)("CERT_NUMBER"),
                                  .ClaimStatus = row.Field(Of String)("CLAIM_STATUS"),
                                  .DealerCode = row.Field(Of String)("DEALER_CODE"),
                                  .ProblemDescription = row.Field(Of String)("PROBLEM_DESCRIPTION"),
                                  .ClaimFullFillmentDate = fulfillmentDate,
                                  .CoverageType = DirectCast([Enum].Parse(GetType(CoverageTypes), row.Field(Of String)("Coverage_Type").Replace(" ", "")), CoverageTypes),
                                  .ExtendedStatuses = returnClaimStatuses
                                })
                        End If
                    Next

                    returnValue.Claims = returnClaim
                Next

                claimCount = dtClaimsInfo.Count
            End If

            returnValue.ClaimsCount = claimCount
            returnValue.ClaimExists = claimCount > 0

            Return returnValue
        End Function
    End Class
End Namespace

