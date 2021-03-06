﻿Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.External.Falabella
Imports Assurant.ElitaPlus.External.Interfaces

Public Class FalabellaIntegration
    Inherits TaskBase

    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
        Logger.AddInfo("In Falabella Integration class ")
    End Sub

#Region "Fields"
    Private _certificateId As Guid
    Private _claimAuthorizationId As Guid
    Private Const EXT_STAT_COM_FB_CLM_UPD_THFT = "COM_FB_CLM_UPD_THFT"
    Private Const EXT_STAT_COM_FB_CLM_UPD_DMG = "COM_FB_CLM_UPD_DMG"
    Private Const EXT_STAT_COMPENSATION_PROCESS_INITIATED = "INDSTA"



#End Region


#Region "Properties"
    Private Property OClaimId As Guid
    Private Property OCertificate As Certificate
    Private Property OClaim As ClaimBase
#End Region


    Protected Friend Overrides Sub Execute()
        Try
            InvokeFalabellaService()
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try

    End Sub

    Private Sub InvokeFalabellaService()
        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_ID))) Then
            OClaimId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_ID)))
        End If

        Logger.AddInfo("InvokeFalabellaService ClaimID: " + PublishedTask.CLAIM_ID)

        If (Not OClaimId.Equals(Guid.Empty)) Then
            OClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(OClaimId)
            OCertificate = OClaim.Certificate
        End If

        'Retrieve Work Order Number for Theft Claim
        If (OClaim.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT AndAlso String.IsNullOrEmpty(OClaim.RemAuthNumber)) Then
            Logger.AddInfo("InvokeFalabellaService New claim: " + PublishedTask.CLAIM_ID)
            If (Not RetrieveWorkOrderFromFalabella() = String.Empty) Then
                Return
            End If
        End If

        '''Call the second service if the claim is approved or denied
        If (OClaim.Status = BasicClaimStatus.Active OrElse OClaim.Status = BasicClaimStatus.Denied) Then

            'check whether the update has already happened from the extended status history
            Dim dvClaimStatuses = ClaimStatus.GetClaimStatusHistoryOnly(OClaim.Id)

            If (Not dvClaimStatuses Is Nothing) Then

                'check whether the SLA extended status has already updated
                If (OClaim.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT) Then
                    dvClaimStatuses.RowFilter = "code= '" & EXT_STAT_COM_FB_CLM_UPD_THFT & "'"
                Else
                    dvClaimStatuses.RowFilter = "code= '" & EXT_STAT_COM_FB_CLM_UPD_DMG & "'"
                End If

                'if no SLA status found
                If (dvClaimStatuses.Count = 0) Then
                    'get the status change date when the status is approved or denied
                    'If denied , get the latest  modified date from the claim
                    'if approved, get the extended status date 
                    Dim statusChangeDate = If(OClaim.ModifiedDate = Nothing, OClaim.CreatedDate, OClaim.ModifiedDate)

                    If (OClaim.Status = BasicClaimStatus.Denied) Then
                        Logger.AddInfo("InvokeFalabellaService denied statusChangeDate: " + statusChangeDate.ToString())

                        If (Not UpdateFalabella(statusChangeDate) = String.Empty) Then
                            Return
                        Else
                            'update the final extended status for SLA
                            UpdateClaimExtendedStatus(OClaim)
                        End If

                    ElseIf (OClaim.Status = BasicClaimStatus.Active) Then
                        Logger.AddInfo("InvokeFalabellaService active statusChangeDate: " + statusChangeDate.ToString())

                        'if compensation process initiated extended status found then call 2nd web service
                        dvClaimStatuses.RowFilter = "code= '" & EXT_STAT_COMPENSATION_PROCESS_INITIATED & "'"
                        If (dvClaimStatuses.Count > 0) Then
                            Dim tDate As Date
                            If (Not dvClaimStatuses(0)("status_date") Is Nothing And Not dvClaimStatuses(0)("status_date") Is DBNull.Value) Then
                                'If (dvClaimStatuses(0)("status_date") Is Nothing Or Not dvClaimStatuses(0)("status_date") Is DBNull.Value) Then
                                tDate = CType(dvClaimStatuses(0)("status_date"), Date)
                            Else
                                tDate = DateTime.MinValue

                            End If

                            If (Not UpdateFalabella(tDate) = String.Empty) Then
                                Return
                            Else
                                'update the final extended status for SLA
                                UpdateClaimExtendedStatus(OClaim)
                                Logger.AddInfo("InvokeFalabellaService UpdateClaimExtendedStatus: " + statusChangeDate.ToString())

                            End If
                        End If

                    End If

                End If

            End If

        End If

    End Sub
    Private Function RetrieveWorkOrderFromFalabella() As String

        Dim falabellaManager As IFalabellaServiceManager = New FalabellaServiceManager()
        Dim request As GetWorkOrderNumberRequest = New GetWorkOrderNumberRequest()
        Dim response As GetWorkOrderNumberResponse = New GetWorkOrderNumberResponse()

        If (Not OClaim Is Nothing) Then

            If (OClaim.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT AndAlso String.IsNullOrEmpty(OClaim.RemAuthNumber)) Then
                Logger.AddInfo("InvokeFalabellaService new claim Fill claim request: " + PublishedTask.CLAIM_ID)
                Dim oCountry As New Country(OClaim.Company.CountryId)
                Dim name As String

                With OClaim
                    request.CertificateNumber = .Certificate.CertNumber
                    request.Address1 = .Certificate.AddressChild.Address1
                    request.City = .Certificate.AddressChild.City
                    request.ClaimNumber = .ClaimNumber
                    request.ComunaCode = .Certificate.AddressChild.PostalCode
                    request.Country = oCountry.Code
                    request.DateOfLoss = Format(.LossDate.Value, "MM/dd/yyyy") '.LossDate.Value.ToShortDateString()
                    request.Email = .Certificate.Email
                    request.FirstName = .Certificate.CustomerFirstName
                    request.IdentificationNumber = If(.Certificate.IdentificationNumber.Length > 0, .Certificate.IdentificationNumber.Substring(0, Len(.Certificate.IdentificationNumber) - 1), String.Empty)
                    request.LastName = .Certificate.CustomerLastName
                    request.Nationality = GetNationalityFromCounty(oCountry.Code)
                    request.ProblemDescription = .ProblemDescription
                    request.ProductDescription = .Certificate.Product.Description
                    request.SerialNumber = .ClaimedEquipment.SerialNumber
                    request.VerificationNumber = If(.Certificate.IdentificationNumber.Length > 0, .Certificate.IdentificationNumber.Substring(Len(.Certificate.IdentificationNumber) - 1), String.Empty)
                    request.WarrantySalesDate = Format(.Certificate.WarrantySalesDate.Value, "MM/dd/yyyy") '.Certificate.WarrantySalesDate.Value.ToShortDateString()
                    request.WorkPhone = .Certificate.WorkPhone

                    name = .Certificate.CustomerName

                    If (Not name Is Nothing And Not name Is DBNull.Value And name.IndexOf(" ") > 0) Then

                        request.FirstName = name.Substring(0, name.IndexOf(" "))
                        request.LastName = name.Substring(name.IndexOf(" "))

                    Else
                        request.FirstName = ""
                        request.LastName = ""
                    End If

                End With

            End If

            Try
                response = falabellaManager.ServicioTecnicoDatosCrearOp(request)

            Catch ex As Exception
                'Save any unhandled exception to claim comments 
                Logger.AddError("InvokeFalabellaService new claim claim request error ", ex)
                AddCommentToClaim(ex.Message.ToString(), OClaim)
                Return ex.Message.ToString()
            End Try


            'Save error code and error message to claim comments 
            If (Not String.IsNullOrEmpty(response.ErrorCode) And Not Convert.ToString(response.ErrorCode) = "0") Then
                Logger.AddInfo("InvokeFalabellaService new claim Fill error code " + response.ErrorCode + "   " + response.ErrorMessage)
                AddCommentToClaim(response.ErrorCode + ";" + response.ErrorMessage, OClaim)
                Return response.ErrorMessage

            ElseIf (Not String.IsNullOrEmpty(response.WorkOrderNumber)) Then
                Logger.AddInfo("InvokeFalabellaService new claim Fill Work order: " + response.WorkOrderNumber)
                OClaim.RemAuthNumber = response.WorkOrderNumber
                OClaim.Save()
            End If


        End If

        Return String.Empty


    End Function

    Private Function UpdateFalabella(StatusChangeDate As Date) As String
        Logger.AddInfo("UpdateFalabella Status date: " + StatusChangeDate.ToString() + " Claim ID " + PublishedTask.CLAIM_ID)

        Dim falabellaManager As IFalabellaServiceManager = New FalabellaServiceManager()
        Dim request As UpdateClaimInfoRequest = New UpdateClaimInfoRequest()
        Dim response As UpdateClaimInfoResponse = New UpdateClaimInfoResponse()

        If (Not OClaim Is Nothing) Then

            With OClaim
                request.AuthorizedAmount = If(OClaim.Status.ToString() = BasicClaimStatus.Denied.ToString(), "0", (CType(OCertificate.SalesPrice, Decimal) - CType(.Deductible, Decimal)).ToString())
                request.ClaimNumber = OClaim.ClaimNumber
                request.DenialReason = If(OClaim.Status.ToString() = BasicClaimStatus.Denied.ToString(), LookupListNew.GetDescriptionFromId(LookupListCache.LK_DENIED_REASON, .DeniedReasonId, True), String.Empty)
                request.StatusChangeDate = StatusChangeDate
                'request.WorkOrderFor = If(OClaim.Status.ToString() = BasicClaimStatus.Denied.ToString(), "Anulada", "Reemplazo Autorizado")
                request.WorkOrderNumber = OClaim.RemAuthNumber
                request.ClaimStatus = OClaim.Status.ToString()
            End With

            Try
                response = falabellaManager.OrdenTrabajoEstadoModificarOp(request)

            Catch ex As Exception
                'Save any unhandled exception to claim comments 
                AddCommentToClaim(ex.Message.ToString(), OClaim)
                Return ex.Message.ToString()
            End Try

            'Save error code and error message to claim comments 
            If (Not String.IsNullOrEmpty(response.ErrorCode) And Not Convert.ToString(response.ErrorCode) = "0") Then
                AddCommentToClaim(response.ErrorCode + ";" + response.ErrorMessage, OClaim)
                Return response.ErrorMessage
            End If

        End If

        Return String.Empty

    End Function
    Private Sub AddCommentToClaim(comments As String, oclaim As ClaimBase)
        If (Not oclaim Is Nothing) Then
            Dim comment As Comment = oclaim.AddNewComment()

            With comment
                .ClaimId = oclaim.Id
                .CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__OTHER)
                .CertId = oclaim.Certificate.Id
                .Comments = comments
                .Save()
            End With

        End If
    End Sub

    Private Sub UpdateClaimExtendedStatus(oClaim As ClaimBase)
        Dim oClaimStatus As ClaimStatus
        Dim ClaimStatusByGroupID As Guid

        Dim companyList = ClaimStatusByGroup.LoadListByCompanyGroup(oClaim.Company.CompanyGroupId).Tables(0).AsDataView

        If (oClaim.Status = BasicClaimStatus.Pending AndAlso oClaim.CertificateItemCoverage.CoverageTypeCode = Codes.COVERAGE_TYPE__THEFT) Then
            companyList.RowFilter = "code= '" & EXT_STAT_COM_FB_CLM_UPD_THFT & "'"
            ClaimStatusByGroupID = GuidControl.ByteArrayToGuid(companyList(0)("claim_status_by_group_id"))
        Else
            companyList.RowFilter = "code= '" & EXT_STAT_COM_FB_CLM_UPD_DMG & "'"
            ClaimStatusByGroupID = GuidControl.ByteArrayToGuid(companyList(0)("claim_status_by_group_id"))
        End If

        oClaimStatus = oClaim.AddExtendedClaimStatus(Guid.Empty)
        oClaimStatus.ClaimId = oClaim.Id
        oClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroupID
        oClaimStatus.StatusDate = DateTime.UtcNow
        oClaimStatus.Comments = "Completed Updating Falabella"
        oClaimStatus.Save()

    End Sub
    Private Function GetNationalityFromCounty(countryCode As String) As String
        If (countryCode = "CL") Then
            Return "Chilena"
        ElseIf (countryCode = "AR") Then
            Return "Argentina"
        ElseIf (countryCode = "CO") Then
            Return "Colombiana"
        ElseIf (countryCode = "PE") Then
            Return "Peruana"
        Else
            Return "Otros"
        End If
    End Function

End Class
