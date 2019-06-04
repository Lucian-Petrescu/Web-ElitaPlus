Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaInternalWS.AppServices.SA.SNMPortal.DataContracts

Namespace AppServices.SA.SNMPortal

    Public Class ClaimReportService
        Implements IClaimReportService

        Public Function GetClaimCharterReport(request As GetClaimCharterReportRequest) As GetClaimCharterReportResponse Implements IClaimReportService.GetClaimCharterReport
            
            if string.IsNullOrEmpty(request.CompanyCode) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("COMPANY_CODE_IS_REQUIRED", TranslationBase.TranslateLabelOrMessage("COMPANY_CODE_IS_REQUIRED")) , "Invalid Request")               
            End If

            if string.IsNullOrEmpty(request.ServiceCenterCode) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("SERVICE_CENTER_IS_REQUIRED", TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_IS_REQUIRED")) , "Invalid Request")               
            End If
            
            if string.IsNullOrEmpty(request.CountryIsoCode) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("COUNTRY_ISO_CODE_IS_REQUIRED", TranslationBase.TranslateLabelOrMessage("ERR_INVALID_COUNTRY")) , "Invalid Request")               
            End If

            if request.FromDate >= request.EndDate Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("INVALID_REPORT_PERIOD", TranslationBase.TranslateLabelOrMessage("INVALID_REPORT_PERIOD")), "Invalid Request" ) '"Invalid Report Period"
            End If

            dim queryMaxEndDate as date = request.FromDate.AddMonths(6)
            if queryMaxEndDate < request.EndDate Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("MAX_REPORT_MONTHS_EXCEED", TranslationBase.TranslateLabelOrMessage("MAX_REPORT_MONTHS_EXCEED")),  "Invalid Request" ) '"Report Duration Exceed Maximum Months Allowed"
            End If

            Dim errCode, errMsg As String
            Dim response As New GetClaimCharterReportResponse
            Dim ds As DataSet
            dim batchId as Guid

            ds = ClaimBase.WS_SNMPORTAL_SA_CLAIMREPORT(companyCode := request.CompanyCode,
                                                       serviceCenterCode := request.ServiceCenterCode,
                                                       countryIsoCode := request.CountryIsoCode,
                                                       fromDate := request.FromDate,
                                                       endDate := request.EndDate,
                                                       extendedStatusCode := request.ExtendedStatus.GetCodeString(),
                                                       dealerCode := request.DealerCode,
                                                       pageSize := iif(request.PageSize >= 0 , request.PageSize, 0),
                                                       batchId := batchId,
                                                       totalRecordCount := response.totalRecordCount,
                                                       totalRecordsInQueue := response.QueuedRecordCount,
                                                       errorCode := errCode,
                                                       errorMessage := errMsg)
            If Not String.IsNullOrEmpty(errCode) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault(errCode, errMsg), "Invalid Request" ) 
            End If

            response.CompanyCode = request.CompanyCode

            if response.totalRecordCount > 0 then
                response.BatchId = batchId
            End If
            if response.totalRecordCount > 0 AndAlso ds.Tables("ClaimList").Rows.Count > 0 then
                'response.CompanyCode = ds.Tables("ClaimList").AsEnumerable()(0).Field(Of String)("Company_Code")

                response.ClaimList = ds _
                    .Tables("ClaimList") _
                    .AsEnumerable() _
                    .[Select](Function(row) New GetClaimChartReportDetails() With
                                 {
                                 .DealerCode = row.Field(Of String)("Dealer_Code"),
                                 .CertificateNumber = row.Field(Of String)("cert_number"),
                                 .WorkPhone = row.Field(Of String)("work_phone"),
                                 .ServiceCenterCode = row.Field(Of String)("service_center_code"),
                                 .ServiceCenterName = row.Field(Of String)("service_center_Desc"),
                                 .ClaimNumber = row.Field(Of String)("claim_number"),
                                 .ClaimStatusCode = row.Field(Of String)("claim_status_code"),
                                 .ClaimExtendedStatus = row.Field(Of String)("Extended_Status"),
                                 .ClaimExtendedStatusDate = row.Field(Of date)("Extended_Status_Date"),
                                 .Make = row.Field(Of String)("Make"),
                                 .Model = row.Field(Of String)("Model"),
                                 .ImeiNumber = row.Field(Of String)("imei_number")
                                 }) _
                    .ToList()
            End If
            Return response

        End Function

        Public Function GetClaimCharterReportNextPage(request As GetClaimCharterReportNextPageRequest) As GetClaimCharterReportResponse Implements IClaimReportService.GetClaimCharterReportNextPage
            Dim errCode, errMsg As String
            Dim response As New GetClaimCharterReportResponse
            Dim ds As DataSet

            ds = ClaimBase.WS_SNMPORTAL_SA_CLAIMREPORT_NextPage(batchId := request.BatchId,
                                                       pageSize := request.PageSize,
                                                       totalRecordCount := response.totalRecordCount,
                                                       totalRecordsInQueue := response.QueuedRecordCount,
                                                       errorCode := errCode,
                                                       errorMessage := errMsg)
            If Not String.IsNullOrEmpty(errCode) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault(errCode, errMsg), "Invalid Request" ) 
            End If

            response.BatchId = request.BatchId

            if response.totalRecordCount > 0 AndAlso ds.Tables("ClaimList").Rows.Count > 0 then
                response.CompanyCode = ds.Tables("ClaimList").AsEnumerable()(0).Field(Of String)("Company_Code")

                response.ClaimList = ds _
                    .Tables("ClaimList") _
                    .AsEnumerable() _
                    .[Select](Function(row) New GetClaimChartReportDetails() With
                                 {
                                 .DealerCode = row.Field(Of String)("Dealer_Code"),
                                 .CertificateNumber = row.Field(Of String)("cert_number"),
                                 .WorkPhone = row.Field(Of String)("work_phone"),
                                 .ServiceCenterCode = row.Field(Of String)("service_center_code"),
                                 .ServiceCenterName = row.Field(Of String)("service_center_Desc"),
                                 .ClaimNumber = row.Field(Of String)("claim_number"),
                                 .ClaimStatusCode = row.Field(Of String)("claim_status_code"),
                                 .ClaimExtendedStatus = row.Field(Of String)("Extended_Status"),
                                 .ClaimExtendedStatusDate = row.Field(Of date)("Extended_Status_Date"),
                                 .Make = row.Field(Of String)("Make"),
                                 .Model = row.Field(Of String)("Model"),
                                 .ImeiNumber = row.Field(Of String)("imei_number")
                                 }) _
                    .ToList()
            End If
            Return response
        End Function
    End Class
End Namespace