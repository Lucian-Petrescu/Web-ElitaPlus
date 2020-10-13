Imports ElitaInternalWS.Certificates
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.Business

Public Class ClarMaxValueService
    Implements IClarMaxValueService
    Public Const IMEIUPDATESOURCE = "OEM"
    Public Const MAKE_APPLE = "APPLE"
    Private Property CompanyGroupManager As ICompanyGroupManager

    Public Sub New(pCompGroupManager As ICompanyGroupManager)
        If (pCompGroupManager Is Nothing) Then
            Throw New ArgumentNullException("pCompGroupManager")
        End If

        CompanyGroupManager = pCompGroupManager
    End Sub

    Public Function GetCertificate(request As GetCertificateRequest) As GetCertificateResponse Implements IClarMaxValueService.GetCertificate
        Dim oCertificate As Certificate
        Dim oCertificateItem As CertItem
        Dim oManufacturer As String
        Dim oManufacturerId As Guid
        Dim oCounter As Integer

        ' Validate Incoming Request for Mandatory Fields (DataAnnotations)
        ExtensionMethods.Validate(request)

        ' Locate Certificate by Serial Number
        'Dim oGsxManager As IAppleGSXServiceManager
        Dim oGsxManager As IAppleGSXServiceManager_Production

        Dim req As CertificateSerialTaxLookup = DirectCast(request.CertificateSearch, CertificateSerialTaxLookup)
        Dim requestSerialNumber As String = req.SerialNumber
        request.Validate("request").HandleFault()


        ' Find Certificate based on Request
        oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch)

        If (oCertificate Is Nothing) Then
            Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = request.CertificateSearch}, "Certificate Not Found")
        End If

        ''''identification number is validated here as the look up (Certificateserialtaxlookup) validates identification number while
        ''''building the response object and by that time Serial number will be updated in Elita.
        ''''this overload will avoid the possibility of serial number update when the Identification number is not in the certificate.
        request.CertificateSearch.ValidateIdentificatonNumer(oCertificate)

        'get the certificate active item
        oCertificateItem = oCertificate.Items.Where(Function(i) i.ItemNumber = 1).First()

        Dim oClarMaxValueServiceHelper As New ClarMaxValueServiceHelper()

        Dim oRequest As New FindOriginalDeviceInfoRequest() With
            {
                .ImeiNumber = oCertificateItem.SerialNumber,
                .SerialNumber = String.Empty
            }
        Dim oResponse As New FindOriginalDeviceInfoResponse() With
            {
                .ImeiNumber = oCertificateItem.SerialNumber,
                .SerialNumber = String.Empty
            }


        '''check whether serial number is valid in Elita
        Dim count As Integer = (From ci As CertItem In
                                      oCertificate.Items.Where(Function(i) i.ItemNumber = 1 AndAlso i.SerialNumber = req.SerialNumber)).Count


        If (count = 0) Then


            ''''if serial number is not valid in Elita, get the make of the active item 
            oManufacturerId = oCertificate.Items.Where(Function(i) i.ItemNumber = 1).FirstOrDefault.ManufacturerId

            ''''get the make from the certificate based on the manufacturer found above
            oManufacturer = oClarMaxValueServiceHelper.GetManufacturerName(CompanyGroupManager,
                                                                                       oCertificate.Company.CompanyGroupId,
                                                                                       oManufacturerId)

            'If (Not request.CertificateSearch.GetType() Is GetType(CertficateDealerSerialLookUp)) Then
            '    Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = request.CertificateSearch}, "Certificate Not Found")
            'End If



            '''''for more than one loop use the output serial number as input to the service 
            'If (oResponse.SerialNumber <> String.Empty) Then
            '    oRequest.SerialNumber = oResponse.SerialNumber
            'End If

            ''''Run Apple service only for Apple devices
            If (oManufacturer = MAKE_APPLE) Then

                Do

                    oCounter += 1

                    Try
                        oGsxManager = ApplicationContext.Current.Container.Resolve(Of IAppleGSXServiceManager_Production)()
                        'oGsxManager = ApplicationContext.Current.Container.Resolve(Of IAppleGSXServiceManager)()

                        oResponse = oGsxManager.FindOriginalDeviceInfo(oRequest)

                        oRequest.ImeiNumber = oResponse.ImeiNumber
                        oRequest.SerialNumber = oResponse.SerialNumber

                    Catch exInner As RepairNotFoundException
                        Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber() With {.CertificateSearch = request.CertificateSearch}, "Invalid Serial Number")
                    End Try

                    'update serial number with imei number found on apple webservice and locate certificate again
                    'DirectCast(request.CertificateSearch, CertficateDealerSerialLookUp).SerialNumber = oRequest.ImeiNumber

                Loop Until (oCounter = 10 OrElse oResponse.ImeiNumber Is Nothing OrElse oResponse.ImeiNumber = req.SerialNumber)

                If (oCounter = 10 OrElse oResponse.ImeiNumber Is Nothing) Then
                    Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber() With {.CertificateSearch = request.CertificateSearch}, "Invalid Serial Number")
                End If

                'Locate cert item (on the active item) for imei number  found on apple web service and update it with original serial number from the request
                'Dim certItem As CertItem = oCertificate.Items.Where(Function(i) (oResponse.ImeiNumber Is Nothing) _
                '            OrElse (oResponse.ImeiNumber.Trim().Length = 0) _
                '            OrElse (i.SerialNumber = oResponse.ImeiNumber)).OrderByDescending(Function(o) o.EffectiveDate).First()

                If (oCertificateItem.SerialNumber <> requestSerialNumber) Then
                    Dim updateCertItem As CertItem = New CertItem(oCertificateItem.Id)
                    updateCertItem.SerialNumber = requestSerialNumber
                    updateCertItem.ImeiUpdateSource = IMEIUPDATESOURCE
                    updateCertItem.Save()

                    'Fetch updated details for the certificate
                    DirectCast(request.CertificateSearch, CertificateSerialTaxLookup).SerialNumber = requestSerialNumber
                    oCertificate = CertificateServiceHelper.GetCertificate(request.CertificateSearch)
                End If

            End If

        End If

        Dim response As GetCertificateResponse = CertificateServiceV1.BuildCertificateResponse(request, oCertificate)

        Return response


    End Function

End Class
