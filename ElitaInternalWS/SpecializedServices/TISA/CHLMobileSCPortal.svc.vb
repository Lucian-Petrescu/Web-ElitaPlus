Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Namespace SpecializedServices
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices")> _
    Public Class CHLMobileSCPortal
        Implements ICHLMobileSCPortal

        Public Function GetElitaHeader() As ElitaHeader Implements ICHLMobileSCPortal.GetElitaHeader
            Throw New NotSupportedException()
        End Function

        Public Function GetCertClaimInfo(ByVal request As CertClaimInfoRequestDC) As CertClaimInfoResponseDC _
                Implements ICHLMobileSCPortal.GetCertClaimInfo
            Dim response As New CertClaimInfoResponseDC
            Dim ds As DataSet
            Dim errCode, errMsg As String
            response.CertificateInfo = New CertificateDC

            if string.IsNullOrEmpty(request.Company_Code) Then
                Throw New FaultException(Of CHLMobileSCPortalFault)(New CHLMobileSCPortalFault() With {.EnglishReason = "COMPANY_CODE_IS_REQUIRED"}, errMsg, New FaultCode("COMPANY_CODE_IS_REQUIRED"))
            End If

            if string.IsNullOrEmpty(request.Phone_Number) AndAlso string.IsNullOrEmpty(request.Serial_Number) AndAlso string.IsNullOrEmpty(request.TaxId) Then
                Throw New FaultException(Of CHLMobileSCPortalFault)(New CHLMobileSCPortalFault() With {.EnglishReason = "MUST_PROVIDE_SERIALNO_OR_PHONENO_OR_TAXID"}, errMsg, New FaultCode("MUST_PROVIDE_SERIALNO_OR_PHONENO_OR_TAXID"))
            End If
            
            ds = ClaimBase.WS_CHLMobileSCPortal_GetCertClaimInfo(CompanyCode:=request.Company_Code, _
                                                                    SerialNumber:=request.Serial_Number, _
                                                                    PhoneNumber:=request.Phone_Number, _
                                                                    taxId:= request.TaxId, _
                                                                    claimStatusCode:= request.ClaimStaus.GetCodeString() , _
                                                                    ErrorCode:=errCode, _
                                                                    ErrorMessage:=errMsg)
            If Not String.IsNullOrEmpty(errCode) Then
                Throw New FaultException(Of CHLMobileSCPortalFault)(New CHLMobileSCPortalFault() With {.EnglishReason = errCode}, errMsg, New FaultCode(errCode))
            End If
            With response.CertificateInfo
                .Cert_Number = ds.Tables("CertificateInfo").Rows(0)("Cert_Number").ToString()
                .Dealer_Code = ds.Tables("CertificateInfo").Rows(0)("Dealer_Code").ToString()
                .Cert_Status = ds.Tables("CertificateInfo").Rows(0)("Cert_Status").ToString()
                .Enrollment_Date = ds.Tables("CertificateInfo").Rows(0)("Enrollment_Date")
                .Manufacturer = ds.Tables("CertificateInfo").Rows(0)("Manufacturer").ToString()
                .Model = ds.Tables("CertificateInfo").Rows(0)("Model").ToString()
                .Phone_Number = ds.Tables("CertificateInfo").Rows(0)("Phone_Number").ToString()
                .Serial_Number = ds.Tables("CertificateInfo").Rows(0)("Serial_Number").ToString()
            End With

            response.ClaimInfo = ds _
                .Tables("ClaimInfo") _
                .AsEnumerable() _
                .[Select](Function(row) New ClaimDC() With
                        {
                                .Claim_Id = row.Field(Of String)("claim_id"),
                                .Claim_Number = row.Field(Of String)("claim_number"),
                                .Claim_Status = row.Field(Of String)("claim_status"),
                                .Claim_Created_Date = row.Field(Of Date?)("claim_created_date"),
                                .Claim_Type_Code = row.Field(Of String)("claim_type_code"),
                                .Claim_Type_Desc = row.Field(Of String)("claim_type_desc"),
                                .Claim_Extended_Status_Code = row.Field(Of String)("claim_extended_status_code"),
                                .Claim_Extended_Status_Desc = row.Field(Of String)("claim_extended_status_desc"),
                                .Authorization_Number = row.Field(Of String)("authorization_number"),
                                .Authorized_Amount = row.Field(Of String)("authorized_amount"),
                                .Method_Of_Repair = row.Field(Of String)("method_of_repair"),
                                .Loss_Date = row.Field(Of Date?)("loss_date"),
                                .Claim_Paid_Amount = row.Field(Of String)("claim_paid_amount"),
                                .Service_Center_Code = row.Field(Of String)("service_center_code"),
                                .Service_Center_Desc = row.Field(Of String)("service_center_desc"),
                                .Device_Condition = row.Field(Of String)("device_condition"),
                                .Certificate_Number = row.Field(Of String)("certificate_number")
                            }) _
                .ToList()
            response.ClaimsCount = response.ClaimInfo.Count()
            response.ClaimExists = IIf(response.ClaimsCount > 0, True, False)

            Return response

        End Function

    End Class
End Namespace
