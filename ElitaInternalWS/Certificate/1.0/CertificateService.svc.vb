Imports Assurant.ElitaPlus.DALObjects
Imports System.Linq
Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports RestSharp
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Configuration
Imports System.Net
'Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItemCoverage

Namespace Certificates
    <ServiceBehavior(Namespace:="http://elita.assurant.com/Certificate")>
    Public Class CertificateServiceV1
        Implements ICertificateServiceV1

        Public Function GetElitaHeader() As ElitaHeader Implements ICertificateServiceV1.GetElitaHeader
            Throw New NotSupportedException()
        End Function

        Private Const ServiceFabricParam As String = "altDeviceId"
        'Private Const ServiceFabricParam = "DiagnosticsManagement_Mexico/api/Warranty?altDeviceId="


        Public Function GetCertificate(request As GetCertificateRequest) As GetCertificateResponse _
            Implements ICertificateServiceV1.GetCertificate
            Dim oCertificate As Certificate

            ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
            ExtensionMethods.Validate(request)


            ' ---------------------------------------------------------------------------------------------------------------------
            ' This is to minimize effect on Max Value. This code should be removed ASAP. No specialized code in generic services
            '''REQ-5784 -- was pulled back from R12.1 commented the code below
            '''If (request.CertificateSearch.GetType() Is GetType(CertficateDealerSerialLookUp)) Then

            '''    Dim req As CertficateDealerSerialLookUp = DirectCast(request.CertificateSearch, CertficateDealerSerialLookUp)

            '''    If (req.DealerCode = "CLAR") Then
            '''        Return New ClarMaxValueService().GetCertificate(request)
            '''    End If

            '''End If
            ' ---------------------------------------------------------------------------------------------------------------------

            ' Find Certificate based on Request
            Try
                oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch, request.RequestedDetails)
            Catch ex As CertificateNotFoundException
                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = ex.CertificateSearch}, "Certificate Not Found")
            End Try

            Dim response As GetCertificateResponse = BuildCertificateResponse(request, oCertificate)

            Return response

        End Function

        Friend Shared Function BuildCertificateResponse(oRequest As GetCertificateRequest, oCertificate As Certificate) As GetCertificateResponse
            Dim strUpgradeFlag As String = Codes.YESNO_N
            Dim dtUpgradeDate As Date
            ' Validate Serial Number and other members not used in Lookup
            oRequest.CertificateSearch.Validate(oCertificate)

            If (oRequest.CertificateSearch.GetType() Is GetType(CertAfterUpgradeLookup)) Then

                Dim req As CertAfterUpgradeLookup = DirectCast(oRequest.CertificateSearch, CertAfterUpgradeLookup)
                strUpgradeFlag = Codes.YESNO_Y
                dtUpgradeDate = req.UpgradeDate
            End If

            Dim response As New GetCertificateResponse

            response.Certificate = New CertificateInfo(oCertificate, strUpgradeFlag, dtUpgradeDate)
            Dim serialNumber As String = CertificateServiceHelper.GetSerialNumber(oRequest.CertificateSearch)

            If (oRequest.RequestedDetails And CertificateDetailTypes.FinanceInfo) = CertificateDetailTypes.FinanceInfo Then
                response.Certificate.Finance = New FinanceInfo(oCertificate, serialNumber)
            End If

            Dim imeiNumberToCheckAppleCare As String, strTemp as string

            If (oRequest.RequestedDetails And CertificateDetailTypes.Items) = CertificateDetailTypes.Items Then

                Dim items As New List(Of ItemInfo)
                For Each ci As CertItem In oCertificate.Items.Where(Function(i) (serialNumber Is Nothing) OrElse (serialNumber.Trim().Length = 0) OrElse (i.SerialNumber = serialNumber))
                    Dim oItemInfo As ItemInfo = New ItemInfo(ci)

                    strTemp = IIf(ci.IMEINumber is Nothing, CI.SerialNumber, CI.IMEINumber)
                    if String.IsNullOrEmpty((strTemp)) = False then
                        imeiNumberToCheckAppleCare = strTemp
                    End If

                    If (oRequest.RequestedDetails And CertificateDetailTypes.Coverages) = CertificateDetailTypes.Coverages Then
                        Dim coverages As New List(Of CoverageInfo)

                        For Each cic As CertItemCoverage In ci.Coverages
                            coverages.Add(New CoverageInfo(cic))
                        Next

                        oItemInfo.Coverages = coverages
                    End If

                    items.Add(oItemInfo)
                Next
                response.Certificate.Items = items
            End If

            If oRequest.applecarecheck.hasvalue AndAlso oRequest.applecarecheck.value = true Then
                If String.IsNullOrEmpty((imeiNumberToCheckAppleCare)) = false Then
                    dim blnHasAppleCare as Boolean, strReplacedImei as string
                    AppleCareCheckAndReplacedImeiLookUp(imeiNumberToCheckAppleCare, oRequest.AppleCareCheckUserDetails, blnHasAppleCare, strReplacedImei)

                    If blnHasAppleCare = False
                        Throw New FaultException(Of CertificateNotEligibleForAppleCare)(New CertificateNotEligibleForAppleCare(), "Certificate Does Not Have Active AppleCare")
                    End If                           
                Else
                        Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber() With {.CertificateSearch = oRequest.CertificateSearch}, "Empty Imei/Serial Number For AppleCare Check")
                End If
            End If

            Return response
        End Function

        Private Shared Sub AppleCareCheckAndReplacedImeiLookUp(strImei As String, strUserDetails As String, ByRef blnHasAppleCare As Boolean, ByRef strReplacedImei As String)
            blnHasAppleCare = False
            strReplacedImei = String.Empty

            Dim oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SERVICE_FABRIC_SERVICE), True)

            Dim strTemp As String
            If String.IsNullOrEmpty(strUserDetails) Then
                strTemp = "ElitaNoValueRecd" 'set to default value so that the call will not break if value is not available
            ElseIf strUserDetails.Length > 20 Then
                strTemp = strUserDetails.Substring(0, 20)
            Else
                strTemp = strUserDetails
            End If

            Dim serviceFabricUrl As String = oWebPasswd.url & strImei & "/" & strTemp
            Dim client = New RestClient(serviceFabricUrl)
            Dim request = New RestRequest(Method.[GET])
            Dim resp As IRestResponse

            Try
                resp = client.Execute(request)
            Catch ex As Exception
                Throw New FaultException(Of ServiceOrderNotFoundFault)(New ServiceOrderNotFoundFault(), "Service Unavailable")
            End Try

            If resp.ResponseStatus = System.Net.HttpStatusCode.InternalServerError Then
                Throw New FaultException(Of ServiceOrderNotFoundFault)(New ServiceOrderNotFoundFault(), "Service Unavailable")
            End If

            If (resp.ResponseStatus <> ResponseStatus.Completed) Then
                Throw New FaultException(Of RequestWasNotSuccessFull)(New RequestWasNotSuccessFull(), "Invalid Request")
            End If

            Dim respObj As JObject = JObject.Parse(resp.Content)

            Dim tokenAcStatus As JToken = respObj.SelectToken("$.AppleCareStatus")
            Dim tokenReplacedImei As JToken = respObj.SelectToken("$.ReplacedImei")

            If Not (tokenAcStatus Is Nothing) AndAlso tokenAcStatus.Type <> JTokenType.Null AndAlso tokenAcStatus <> String.Empty Then
                strTemp = tokenAcStatus
                If strTemp = "Active" Then
                    blnHasAppleCare = True
                End If
            End If

            If Not (tokenReplacedImei Is Nothing) AndAlso tokenReplacedImei.Type <> JTokenType.Null AndAlso tokenReplacedImei <> String.Empty Then
                strReplacedImei = tokenReplacedImei
            End If
        End Sub
        Public Function CancelByExternalCertNum(request As CancelByExternalCertNumRequest) As CancelByExternalCertNumResponse Implements ICertificateServiceV1.CancelByExternalCertNum
            Dim oCertificate As Certificate, strType As String
            Dim response As New CancelByExternalCertNumResponse

            If request.ExternalCertNumType = ExternalCertNumType.ServiceLineNumber Then
                strType = "SLN"
            ElseIf request.ExternalCertNumType = ExternalCertNumType.TaxId Then
                strType = "TAXID"
            End If

            Certificate.CancelCertByExternalNumber(request.CompanyCode, request.DealerCode,
                                                   strType, request.ExternalCertNum,
                                                   request.CancellationDate, request.CancellationReasonCode,
                                                   request.CallerName, Authentication.CurrentUser.NetworkId,
                                                   response.DealerCode, response.CertificateNumber,
                                                   response.ErrorCode, response.ErrorMessage)



            Return response
        End Function

        Private Function GetStringFromDataRow(row As DataRow, strColName As String) As String
            If row(strColName) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(row(strColName), String)
            End If
        End Function

        Private Function GetDateFromDataRow(row As DataRow, strColName As String) As Date
            If row(strColName) Is DBNull.Value Then
                Return Nothing
            Else
                Return DateHelper.GetDateValue(row(strColName).ToString())
            End If
        End Function

        Private Function GetGuidDataRow(row As DataRow, strColName As String) As Guid
            If row(strColName) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(row(strColName), Byte()))
            End If
        End Function

        Private Sub PopulateAppleCertResponse(ByRef response As SearchAppleCertificateByImeiResponse, drSearchResult As DataRow)
            response.CertificateNumber = GetStringFromDataRow(drSearchResult, "cert_number")
            response.CustomerName = GetStringFromDataRow(drSearchResult, "customer_name")
            response.Imei = GetStringFromDataRow(drSearchResult, "IMEI_number")
            response.IdentificationNumber = GetStringFromDataRow(drSearchResult, "identification_number")
            response.ItemDescription = GetStringFromDataRow(drSearchResult, "item_description")
            response.Manufacturer = GetStringFromDataRow(drSearchResult, "make")
            response.Model = GetStringFromDataRow(drSearchResult, "model")
            response.ProductCode = GetStringFromDataRow(drSearchResult, "product_code")
            response.ProductDescription = GetStringFromDataRow(drSearchResult, "product_description")
            response.SkuNumber = GetStringFromDataRow(drSearchResult, "sku_number")
            response.SerialNumber = GetStringFromDataRow(drSearchResult, "serial_number")
            response.ServiceLineNumber = GetStringFromDataRow(drSearchResult, "service_line_number")
            response.Statuscode = GetStringFromDataRow(drSearchResult, "status_Code")
            response.WarrantySalesDate = GetDateFromDataRow(drSearchResult, "warranty_sales_date")
            response.WorkPhone = GetStringFromDataRow(drSearchResult, "work_phone")
            response.DealerCode = GetStringFromDataRow(drSearchResult, "dealer")
        End Sub
        Public Function SearchAppleCertificateByImei(request As SearchAppleCertificateByImeiRequest) As SearchAppleCertificateByImeiResponse Implements ICertificateServiceV1.SearchAppleCertificateByImei
            'request.Validate("request").HandleFault()
            Dim response As New SearchAppleCertificateByImeiResponse
            Dim intErrCode As Integer, strErrMsg As String

            If String.IsNullOrEmpty(request.CompanyCode) = True Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault() With {.CompanyCode = request.CompanyCode}, "Company Is Required")
            End If

            Dim rsSearchResult As DataSet = Certificate.SearchCertificateByImeiNumber(request.CompanyCode, request.DealerCode, request.ImeiNumber, "A", ElitaPlusIdentity.Current.ActiveUser.NetworkId, intErrCode, strErrMsg)
            If intErrCode = 0 Then 'success
                If rsSearchResult.Tables(0).Rows.Count > 0 Then
                    PopulateAppleCertResponse(response, rsSearchResult.Tables(0).Rows(0))
                Else 'Incoming IMEI not found, go to GSX check replacement
                    Dim blnHasAppleCare As Boolean, strReplacedImei As String

                    Dim strImeiNumber As String = request.ImeiNumber

                    'Begin Req 6457 
                    For value As Integer = 0 To 4

                        AppleCareCheckAndReplacedImeiLookUp(strImeiNumber, request.UserDetails, blnHasAppleCare, strReplacedImei)

                        If String.IsNullOrEmpty(strReplacedImei) Then 'Can't locate replaced Imei either
                            Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound(), "Certificate Not Found")
                        End If

                        'search certificate with replaced Imei
                        rsSearchResult = Certificate.SearchCertificateByImeiNumber(request.CompanyCode, request.DealerCode, strReplacedImei, "A", ElitaPlusIdentity.Current.ActiveUser.NetworkId, intErrCode, strErrMsg)

                        If intErrCode = 0 Then 'success
                            If rsSearchResult.Tables(0).Rows.Count > 0 Then 'certificate found with replaced Imei
                                'populate the response
                                PopulateAppleCertResponse(response, rsSearchResult.Tables(0).Rows(0))

                                'update IMEI on cert item    
                                Dim guidCertItemId As Guid = GetGuidDataRow(rsSearchResult.Tables(0).Rows(0), "cert_item_id")
                                Dim strIdentificationType As String = rsSearchResult.Tables(0).Rows(0)("Identification_Type")
                                Dim bleUpdateSuccess As Boolean
                                If guidCertItemId <> Guid.Empty Then
                                    bleUpdateSuccess = Certificate.UpdateImeiNumberAddEvent(guidCertItemId, strReplacedImei, request.ImeiNumber, strIdentificationType, intErrCode, strErrMsg)
                                    If bleUpdateSuccess Then
                                        response.SerialNumber = request.ImeiNumber
                                        'If String.IsNullOrEmpty(response.SerialNumber) = False AndAlso response.SerialNumber = strReplacedImei Then
                                        '    response.SerialNumber = request.ImeiNumber
                                        'End If
                                        response.Imei = request.ImeiNumber
                                        'If String.IsNullOrEmpty(response.Imei) = False AndAlso response.Imei = strReplacedImei Then
                                        '    response.Imei = request.ImeiNumber
                                        'End If
                                    End If
                                End If
                                Exit For
                            Else
                                If value = 4 Then ' Last Attempt
                                    Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound(), "Certificate Not Found")
                                End If
                            End If
                        Else
                            If intErrCode = 400 Then
                                Throw New FaultException(Of CertificateBelongsToDiffDealer)(New CertificateBelongsToDiffDealer() With {.DealerCode = request.DealerCode}, "This switch-up program was bought in another APR")
                            ElseIf value = 4 Then ' Last Attempt
                                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound(), "Certificate Not Found")
                            End If
                        End If

                        strImeiNumber = strReplacedImei
                    Next
                End If
            ElseIf intErrCode = 100 Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault() With {.CompanyCode = request.CompanyCode}, "Company Not Found")
            ElseIf intErrCode = 200 Then
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = request.DealerCode}, "Dealer Not Found")
            ElseIf intErrCode = 400 Then
                Throw New FaultException(Of CertificateBelongsToDiffDealer)(New CertificateBelongsToDiffDealer() With {.DealerCode = request.DealerCode}, "This switch-up program was bought in another APR")
            Else
                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound(), "Certificate Not Found")
            End If
            Return response
        End Function


    End Class
End Namespace

