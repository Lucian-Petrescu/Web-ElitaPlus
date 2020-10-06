Imports Assurant.ElitaPlus.External.AppleGSX.Client.Production
Imports Assurant.ElitaPlus.External.Interfaces
Imports System.Security.Cryptography.X509Certificates
Imports System.ServiceModel.Security
Imports System.ServiceModel

Public Class AppleGSXServiceManager_Production
    Implements IAppleGSXServiceManager_Production

    Private m_Client As GsxWSAmIPhonePortClient
    Private m_GsxUserSession As gsxUserSessionType
    Private ms_SyncRoot As Object = New Object()

    Private ReadOnly Property Client
        Get
            If (m_Client Is Nothing) Then
                SyncLock (ms_SyncRoot)
                    If (m_Client Is Nothing) Then
                        m_Client = New GsxWSAmIPhonePortClient()
                        m_Client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "3c 0a 93 a6 c8 21 48 ed f1 df 41 0c 8b 26 98 c0 16 73 74 96")
                    End If
                End SyncLock
            End If
            Return m_Client
        End Get
    End Property

    Private ReadOnly Property Session
        Get
            If (m_GsxUserSession Is Nothing) Then
                SyncLock (ms_SyncRoot)
                    If (m_GsxUserSession Is Nothing) Then
                        ' Authenticate
                        Dim oauthenticateRequestType As New authenticateRequestType
                        Dim oAuthenticateResponse As authenticateResponseType

                        oauthenticateRequestType.userId = "elita.apple-gsx@assurant.com"
                        'oauthenticateRequestType.password = "@Assurant0715"
                        oauthenticateRequestType.languageCode = "en"
                        oauthenticateRequestType.userTimeZone = "est"
                        oauthenticateRequestType.serviceAccountNo = "460117"

                        oAuthenticateResponse = Client.Authenticate(oauthenticateRequestType)


                        ' Create Session
                        m_GsxUserSession = New gsxUserSessionType() With
                                            {
                                                .userSessionId = oAuthenticateResponse.userSessionId
                                            }
                    End If
                End SyncLock
            End If
            Return m_GsxUserSession
        End Get
    End Property


    Public Function FindOriginalDeviceInfo(ByVal pRequest As FindOriginalDeviceInfoRequest) As FindOriginalDeviceInfoResponse Implements IAppleGSXServiceManager_Production.FindOriginalDeviceInfo
        If (String.IsNullOrWhiteSpace(pRequest.ImeiNumber) AndAlso String.IsNullOrWhiteSpace(pRequest.SerialNumber)) Then
            Throw New ArgumentNullException("One of IMEI or Serial Number are required")
        End If

        Dim oSerialNumber As String

        ' Check if Lookup for IMEI to Serial Number needs to be performed
        If (String.IsNullOrWhiteSpace(pRequest.SerialNumber)) Then

            ' Find Serial Number based on IMEI Number
            Dim oImeiToSerialLookupResponse As fetchIOSActivationDetailsResponseType
            oImeiToSerialLookupResponse = Client.FetchIOSActivationDetails(New fetchIOSActivationDetailsRequestType() With
            {
                .alternateDeviceId = pRequest.ImeiNumber,
                .userSession = Session
            })

            oSerialNumber = oImeiToSerialLookupResponse.activationDetailsInfo.serialNumber
        Else
            ' Serial Number supplied in request
            oSerialNumber = pRequest.SerialNumber
        End If
        ' Look for Repairs
        Dim oRepairLookupResponse As iphoneRepairLookupResponseType
        Try
            oRepairLookupResponse = Client.IPhoneRepairLookup(New iphoneRepairLookupRequestType() With
                                                 {
                                                    .lookupRequestData = New iphoneRepairLookupInfoType() With
                                                    {
                                                        .serialNumber = oSerialNumber
                                                                                                            },
                                                    .userSession = Session
                                                 })


            If oRepairLookupResponse.lookupResponseData.Count = 0 Then
                Throw New RepairNotFoundException()
            End If

        Catch ex As Exception
            Throw New RepairNotFoundException()
        End Try


        ' Look for Repairs Details
        Dim oRepairDetailsLookup As iphoneRepairDetailsLookupResponseType
        Try
            oRepairDetailsLookup = Client.IPhoneRepairDetailsLookup(New iphoneRepairDetailsLookupRequestType() With
                                                           {
                                                            .repairConfirmationNumber = oRepairLookupResponse.lookupResponseData.FirstOrDefault().repairConfirmationNumber,
                                                            .userSession = Session
                                                           })

        Catch faultex As FaultException  ''''As per R12.3, out of apple repairs are not supported and below error is thrown. Elita will send a generic message certificate not found to MaxValue.
            'If (faultex.Code.Name = "DEP.ESC.107" And faultex.Message.ToUpper() = "NO CORRESPONDING DISPATCH FOUND.") Then
            '    Throw New RepairNotFoundException()
            'End If
            Throw New RepairNotFoundException()
        End Try

        ' Find IMEI Number based on Serial Number
        Dim oSerialToImeiLookupResponse As fetchIOSActivationDetailsResponseType
        oSerialToImeiLookupResponse = Client.FetchIOSActivationDetails(New fetchIOSActivationDetailsRequestType() With
                                                                               {
                                                                                    .serialNumber = oRepairDetailsLookup.lookupResponseData.FirstOrDefault().newSerialNumber,
                                                                                    .userSession = Session
                                                                               })

        Dim response As New FindOriginalDeviceInfoResponse
        response.ImeiNumber = oSerialToImeiLookupResponse.activationDetailsInfo.imeiNumber
        response.SerialNumber = oSerialToImeiLookupResponse.activationDetailsInfo.serialNumber

        Return response

    End Function



End Class
