Imports System.Collections.Generic
Imports System.ServiceModel
Imports System.Threading
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common.ErrorCodes
Imports Assurant.Elita.ClientIntegration
Imports Assurant.ElitaPlus.BusinessObjectsNew.PolicyService
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Security
Imports ElitaInternalWS.AppServices.SA.SNMPortal

Namespace AppServices.SA
    <ServiceBehavior(Namespace:="http://elita.assurant.com/AppServices/SA")>
    Public Class APRService
        Implements IAPRService


#Region "Properties"

        Dim _model As String = String.Empty

        Dim _make As String = String.Empty

        Private Property DealerId() As Guid
        Private Property OCertificate As Certificate

#End Region

#Region "Member Methods"
        Public Sub UpdateImei(request As UpdateImeiRequest) Implements IAPRService.UpdateImei
            request.Validate("request").HandleFault()

            Dim claimListDv As Certificate.CertificateClaimsDV
            Dim certificateId As Guid?
            Dim companyId As Guid?

            companyId = GetCompanyId(request.CompanyCode)
            If companyId.Equals(Guid.Empty) Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND) &
                                                                                                          " : " & request.CompanyCode)
            End If

            If (Not String.IsNullOrEmpty(request.DealerCode)) And (GetDealerId(companyId, request.DealerCode).Equals(Guid.Empty)) Then
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_DEALER_NOT_FOUND) &
                                                                                                          " : " & request.DealerCode)
            Else
                DealerId = GetDealerId(companyId, request.DealerCode)
            End If

            If (String.IsNullOrEmpty(request.NewSKU)) AndAlso (String.IsNullOrEmpty(request.NewMake) Or String.IsNullOrEmpty(request.NewModel)) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("SKU_MAKE_MODEL_ERROR", TranslationBase.TranslateLabelOrMessage("SKU_MAKE_MODEL_ERROR")), "Invalid Request")
            End If

            Try
                certificateId = CertificateFacade.GetCertificatebyCertNumberAndCompanyCode(request.CertificateNumber, request.CompanyCode)
            Catch ex As Exception
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("ERR_CERTIFICATE_NOT_FOUND", TranslationBase.TranslateLabelOrMessage("ERR_CERTIFICATE_NOT_FOUND")), "Invalid Request")
            End Try


            If (Not certificateId.Equals(Guid.Empty)) Then
                OCertificate = New Certificate(certificateId)
                If Not (OCertificate.StatusCode = "A") Then
                    Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("CERTIFICATE_NOT_ACTIVE", TranslationBase.TranslateLabelOrMessage("CERTIFICATE_NOT_ACTIVE")), "Invalid Request")
                End If
            Else
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("ERR_CERTIFICATE_NOT_FOUND", TranslationBase.TranslateLabelOrMessage("ERR_CERTIFICATE_NOT_FOUND")), "Invalid Request")
            End If

            If (request.OldMEI).Equals(request.NewImei) Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("OLD_NEW_IMEI_DUPLICATE", TranslationBase.TranslateLabelOrMessage("OLD_NEW_IMEI_DUPLICATE")), "Invalid Request")
            End If

            Dim certItemDataView As CertItem.CertItemSearchDV = OCertificate.CertItems

            If certItemDataView.IsSerialNumberExist(request.OldMEI) = False Then
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("OLD_IMEI_NOT_FOUND", TranslationBase.TranslateLabelOrMessage("OLD_IMEI_NOT_FOUND")), "Invalid Request")
            End If

            If Not (String.IsNullOrEmpty(request.NewSKU)) Then
                If (ValidateSku(DealerId, request.NewSKU)) = False Then
                    Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("INVALID_SKU", TranslationBase.TranslateLabelOrMessage("INVALID_SKU")), "Invalid Request")
                End If
            End If

            If OCertificate.StatusCode = "A" Then
                claimListDv = OCertificate.ClaimsWithExtstatus(OCertificate.Id, DealerId, request.OldMEI)
                If claimListDv.Count > 0 Then
                    Dim strRowFilter As String = "extended_status_code <> 'DTC'"
                    claimListDv.RowFilter = strRowFilter
                    If (claimListDv.Count > 0) Then
                        Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("OPEN_CLAIM_EXISTS", TranslationBase.TranslateLabelOrMessage("OPEN_CLAIM_EXISTS")), "Invalid Request")
                    End If
                End If
            End If

            Try

                Dim endorseRequest As EndorsePolicyRequest = New EndorsePolicyRequest
                Dim iteminfo As New UpdateItemInfo()

                If Not (String.IsNullOrEmpty(request.NewSKU)) And (ValidateSku(DealerId, request.NewSKU)) = True Then
                    iteminfo.Manufacturer = _make
                ElseIf Not String.IsNullOrEmpty(request.NewMake) Then
                    iteminfo.Manufacturer = request.NewMake
                Else
                    Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("SKU_MAKE_MODEL_ERROR", TranslationBase.TranslateLabelOrMessage("SKU_MAKE_MODEL_ERROR")), "Invalid Request")
                End If

                If Not (String.IsNullOrEmpty(request.NewSKU)) And (ValidateSku(DealerId, request.NewSKU)) = True Then
                    iteminfo.Model = _model
                ElseIf Not String.IsNullOrEmpty(request.NewModel) Then
                    iteminfo.Model = request.NewModel
                Else
                    Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("SKU_MAKE_MODEL_ERROR", TranslationBase.TranslateLabelOrMessage("SKU_MAKE_MODEL_ERROR")), "Invalid Request")
                End If

                If certItemDataView.Count > 0 Then
                    Dim filter As String = "serial_number = '" & request.OldMEI & "'"
                    certItemDataView.RowFilter = filter
                    If (certItemDataView.Count > 0) Then
                        iteminfo.RiskTypeCode = certItemDataView.Table(0)("risk_type_code")
                    End If
                Else
                    Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("RISK_TYPE_ERROR", TranslationBase.TranslateLabelOrMessage("RISK_TYPE_ERROR")), "Invalid Request")
                End If

                iteminfo.EndorsementReason = 1
                iteminfo.SerialNumber = request.NewImei
                iteminfo.SkuNumber = request.NewSKU
                endorseRequest.CertificateNumber = request.CertificateNumber
                endorseRequest.DealerCode = request.DealerCode

                endorseRequest.Requests = New BasePolicyEndorseAction() {iteminfo}

                WcfClientHelper.Execute(Of PolicyServiceClient, IPolicyService, EndorseResponse)(
                                                                            GetClient(),
                                                                            New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Thread.CurrentPrincipal.GetNetworkId()}},
                                                                            Function(c As PolicyServiceClient)
                                                                                Return c.Endorse(endorseRequest)
                                                                            End Function)

            Catch ex As FaultException
                If Not DirectCast(ex, FaultException(Of EnrollFault)).Detail.FaultMessage Is Nothing Then
                    Dim fault As String
                    fault = DirectCast(ex, FaultException(Of EnrollFault)).Detail.FaultMessage
                    Throw New FaultException(Of InvalidUpdateFault)(New InvalidUpdateFault(), fault)
                End If
                Throw ex
            Catch ex As Exception
                Throw New FaultException(Of Faults.InvalidRequestFault)(New Faults.InvalidRequestFault("UPDATE_IMEI_ERROR", TranslationBase.TranslateLabelOrMessage("UPDATE_IMEI_ERROR")), "Invalid Request")
            End Try
        End Sub

        Private Shared Function GetDealerId(companyId As Guid, dealerCode As String) As Guid
            Dim ds As DataSet

            ds = Company.GetDealerFromCompany(companyId, dealerCode)
            If (ds.Tables(0).Rows.Count = 0) Then
                Return Guid.Empty
            Else
                Return New Guid(CType(ds.Tables(0).Rows(0)("DEALER_ID"), Byte()))
            End If

        End Function

        Private Shared Function GetCompanyId(companycode As String) As Guid
            Dim companyId As Guid = Guid.Empty
            Dim objCompaniesAl As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim i As Integer

            For i = 0 To objCompaniesAl.Count - 1
                Dim objCompany As New Company(CType(objCompaniesAl.Item(i), Guid))
                If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(companycode.ToUpper) Then
                    companyId = objCompany.Id
                End If
            Next
            Return companyId

        End Function

        Private Shared Function GetClient() As PolicyServiceClient
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SVC_CERT_ENROLL), False)
            Dim client = New PolicyServiceClient("CustomBinding_IPolicyService", oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function
        Private Function ValidateSku(id As Guid, skuNumber As String) As Boolean

            Dim dealerBo As Dealer = New Dealer(id)
            Dim wmdal As New WarrantyMasterDAL
            Dim manufacturerId As Guid?
            Dim validSku As Boolean

            If Not (dealerBo Is Nothing) Then
                Dim dsMakeModel As DataSet = wmdal.GetMakeAndModelForDealerFromWM(skuNumber, id)
                Dim dsMfgModel As DataSet
                If Not dsMakeModel Is Nothing AndAlso dsMakeModel.Tables.Count > 0 AndAlso dsMakeModel.Tables(0).Rows.Count = 1 Then
                    manufacturerId = New Guid(CType(dsMakeModel.Tables(0).Rows(0)("Internal_manufacturer_id"), Byte()))
                    _model = dsMakeModel.Tables(0).Rows(0)("Model_Number")
                    _make = dsMakeModel.Tables(0).Rows(0)("Manufacturer_Name")
                    Dim mfgModelDal As New MfgModelDAL
                    dsMfgModel = mfgModelDal.GetMakeAndModelForDealer(manufacturerId, _model, DealerId)

                    If Not dsMfgModel Is Nothing AndAlso dsMfgModel.Tables.Count > 0 Then
                        validSku = True
                    Else
                        validSku = False
                    End If
                Else
                    validSku = False
                End If
            End If
            Return validSku
        End Function
#End Region


    End Class
End Namespace