Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports Assurant.ElitaPlus.Business
Imports Microsoft.Practices.Unity
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus


Namespace SpecializedServices.Timb
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices/TIMB")>
    Public Class CertificateClaimService
        Implements ICertificateClaimService

        Protected Const ERROR_NO_CERTIFICATE_FOUND As String = "NO_CERTIFICATE_FOUND"
        Protected Const ERROR_MORE_THAN_ONE_CERTIFICATE_FOUND As String = "MORE_THAN_ONE_CERTIFICATE_FOUND"
        Protected Const ERROR_FATAL As String = "FATAL"
        Protected Const ERROR_FOUND_MSG As String = "Error(s) Found"
        Protected Const ERROR_UNEXPECTED As String = "Unexpected Error"

        Private _faults As Dictionary(Of String, FaultException)

        Private Property ClaimManager() As IClaimManager
        Private Property CompanyManager() As ICompanyManager

        Public Overridable ReadOnly Property UserID() As Guid Implements ICertificateClaimService.UserID
            Get
                Return BusinessObjectsNew.ElitaPlusIdentity.Current.ActiveUser.Id
            End Get
        End Property

        Public Overridable ReadOnly Property LanguageID() As Guid Implements ICertificateClaimService.LanguageID
            Get
                Return BusinessObjectsNew.ElitaPlusIdentity.Current.ActiveUser.LanguageId
            End Get
        End Property

        Public Overridable Function ValidateCompany(CompanyId As Guid) As Boolean Implements ICertificateClaimService.ValidateCompany
            Try
                Dim i As Integer = Nothing
                Dim oUser As New BusinessObjectsNew.User(BusinessObjectsNew.ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(UserID)

                Dim assignedCompany As DataRow = userAssignedCompaniesDv.Table.AsEnumerable().FirstOrDefault(Function(r) Not r("COMPANY_ID") Is Nothing _
                                                                                                                 AndAlso Not r("COMPANY_ID") Is DBNull.Value _
                                                                                                                 AndAlso New Guid(CType(r("COMPANY_ID"), Byte())).Equals(CompanyId))

                Return (Not assignedCompany Is Nothing)

            Catch conf As CompanyNotFoundException
                Return False
            End Try
        End Function


        Public Sub New(<Dependency(ElitaWebServiceConstants.SPECIALIZED_SERVICE_CLAIM_MANAGER)>
                            pClaimManager As IClaimManager,
                       <Dependency(ElitaWebServiceConstants.SPECIALIZED_SERVICE_TIMB_CLAIM_REPOSITORY)>
                            pClaimRepository As IClaimRepository(Of DataEntities.Claim),
                       pCompanyManager As ICompanyManager)

            If (pClaimManager Is Nothing) Then
                Throw New ArgumentNullException("pClaimManager")
            End If

            ClaimManager = pClaimManager

            If (pClaimRepository Is Nothing) Then
                Throw New ArgumentNullException("pClaimRepository")
            End If

            CType(pClaimManager, ISpecializedClaimManager).ClaimRepository = pClaimRepository

            If (pCompanyManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyManager")
            End If

            CompanyManager = pCompanyManager

            _faults = New Dictionary(Of String, FaultException)
            _faults(ERROR_NO_CERTIFICATE_FOUND) = New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(), "Certificate not found")
            _faults(ERROR_MORE_THAN_ONE_CERTIFICATE_FOUND) = New FaultException(Of DuplicateCertFound)(New DuplicateCertFound(), "Duplicate Certificate Found")
            _faults(ERROR_FATAL) = New FaultException(Of DatabaseErrorFault)(New DatabaseErrorFault(), ERROR_UNEXPECTED)

        End Sub

        Public Function GetElitaHeader() As ElitaHeader Implements ICertificateClaimService.GetElitaHeader
            Throw New NotSupportedException()
        End Function

        Public Function GetCertClaimInfo(request As CertificateClaimRequest) As CertClaimInfoResponseDC _
                Implements ICertificateClaimService.GetCertClaimInfo

            request.Validate("request").HandleFault()

            Dim oCompany As DataEntities.Company

            ''check for companycode
            Try
                oCompany = CompanyManager.GetCompany(request.CompanyCode)

                If (Not oCompany Is Nothing AndAlso Not ValidateCompany(oCompany.CompanyId)) Then
                    Throw New Exception("Company Not Found : " & request.CompanyCode)
                End If
            Catch ex As Exception
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found : " & request.CompanyCode)
            End Try


            Dim response As New CertClaimInfoResponseDC
            Dim ds As DataSet
            Dim errCode, errMsg As String
            response.CertificateInfo = New CertificateDC

            ds = ClaimManager.GetCertificateClaimInfo(request.CompanyCode,
                                                         request.CertificateNumber,
                                                         request.SerialNumber,
                                                         request.PhoneNumber,
                                                         UserID,
                                                         LanguageID,
                                                         errCode,
                                                         errMsg)


            If Not String.IsNullOrEmpty(errCode) Then
                If (_faults.ContainsKey(errCode)) Then
                    Throw _faults(errCode)
                Else
                    Dim vResults As Dictionary(Of String, String) = New Dictionary(Of String, String)
                    vResults("Error") = errMsg

                    Throw New FaultException(Of ValidationFault)(New ValidationFault(vResults), ERROR_FOUND_MSG)
                End If
            End If

            With response.CertificateInfo
                .Cert_Number = ds.Tables("CertificateInfo").Rows(0)("Cert_Number").ToString()
                .Dealer_Code = ds.Tables("CertificateInfo").Rows(0)("Dealer_Code").ToString()
                .Cert_Status = ds.Tables("CertificateInfo").Rows(0)("Cert_Status").ToString()
                .Enrollment_Date = ds.Tables("CertificateInfo").Rows(0)("Enrollment_Date")
                .Manufacturer = GetStringValue(ds.Tables("CertificateInfo").Rows(0)("Manufacturer"))
                .Model = GetStringValue(ds.Tables("CertificateInfo").Rows(0)("Model"))
                .Phone_Number = GetStringValue(ds.Tables("CertificateInfo").Rows(0)("Phone_Number"))
                .Serial_Number = GetStringValue(ds.Tables("CertificateInfo").Rows(0)("Serial_Number"))
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
                                .Claim_Type_Code = GetStringValue(row.Field(Of String)("claim_type_code")),
                                .Claim_Type_Desc = GetStringValue(row.Field(Of String)("claim_type_desc")),
                                .Claim_Extended_Status_Code = GetStringValue(row.Field(Of String)("claim_extended_status_code")),
                                .Claim_Extended_Status_Desc = GetStringValue(row.Field(Of String)("claim_extended_status_desc")),
                                .Authorization_Number = GetStringValue(row.Field(Of String)("authorization_number")),
                                .Authorized_Amount = GetStringValue(row.Field(Of String)("authorized_amount")),
                                .Method_Of_Repair = GetStringValue(row.Field(Of String)("method_of_repair")),
                                .Loss_Date = row.Field(Of Date?)("loss_date"),
                                .Claim_Paid_Amount = GetStringValue(row.Field(Of String)("claim_paid_amount")),
                                .Service_Center_Code = GetStringValue(row.Field(Of String)("service_center_code")),
                                .Service_Center_Desc = GetStringValue(row.Field(Of String)("service_center_desc")),
                                .Device_Condition = GetStringValue(row.Field(Of String)("device_condition"))
                            }) _
                .ToList()
            response.ClaimsCount = response.ClaimInfo.Count()
            response.ClaimExists = IIf(response.ClaimsCount > 0, True, False)

            Return response

        End Function

        Private Function GetStringValue(fieldValue As Object) As String
            Return If(fieldValue Is Nothing Or fieldValue Is DBNull.Value, String.Empty, fieldValue.ToString)
        End Function

    End Class

End Namespace
