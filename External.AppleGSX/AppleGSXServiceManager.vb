Imports Assurant.ElitaPlus.External.AppleGSX.Client
Imports Assurant.ElitaPlus.External.Interfaces
Imports System.Security.Cryptography.X509Certificates
Imports System.ServiceModel.Security
Public Class AppleGSXServiceManager
    Implements IAppleGSXServiceManager

    Private m_Client As GsxWSLaAspPortClient
    Private m_GsxUserSession As gsxUserSessionType
    Private ms_SyncRoot As Object = New Object()

    Private ReadOnly Property Client
        Get
            If (m_Client Is Nothing) Then
                SyncLock (ms_SyncRoot)
                    If (m_Client Is Nothing) Then
                        m_Client = New GsxWSLaAspPortClient()

                        'm_Client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "bc 65 28 18 ca 35 14 a4 22 ce b3 5f d1 04 88 02 15 1e 56 e3")
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
                        Dim oauthenticateRequestType As New authenticateRequestType()
                        Dim oAuthenticateResponse As authenticateResponseType

                        oauthenticateRequestType.userId = "elita.it@assurant.com"   '"elita.apple-gsx@assurant.com"
                        oauthenticateRequestType.languageCode = "en"
                        oauthenticateRequestType.userTimeZone = "est"
                        oauthenticateRequestType.serviceAccountNo = "460117"

                        oAuthenticateResponse = Me.Client.Authenticate(oauthenticateRequestType)

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


    Public Function FindOriginalDeviceInfo(ByVal pRequest As FindOriginalDeviceInfoRequest) As FindOriginalDeviceInfoResponse Implements IAppleGSXServiceManager.FindOriginalDeviceInfo
        If (String.IsNullOrWhiteSpace(pRequest.ImeiNumber) AndAlso String.IsNullOrWhiteSpace(pRequest.SerialNumber)) Then
            Throw New ArgumentNullException("One of IMEI or Serial Number are required")
        End If

        Dim oSerialNumber As String

        ' Check if Lookup for IMEI to Serial Number needs to be performed
        If (String.IsNullOrWhiteSpace(pRequest.SerialNumber)) Then

            ' Find Serial Number based on IMEI Number
            Dim oImeiToSerialLookupResponse As fetchIOSActivationDetailsResponseType
            oImeiToSerialLookupResponse = Me.Client.FetchIOSActivationDetails(New fetchIOSActivationDetailsRequestType() With
            {
                .alternateDeviceId = pRequest.ImeiNumber,
                .userSession = Me.Session
            })

            oSerialNumber = oImeiToSerialLookupResponse.activationDetailsInfo.serialNumber
        Else
            ' Serial Number supplied in request
            oSerialNumber = pRequest.SerialNumber
        End If
        ' Look for Repairs
        Dim oRepairLookupResponse As repairLookupResponseType
        Try
            oRepairLookupResponse = Me.Client.RepairLookup(New repairLookupRequestType() With
                                                 {
                                                    .lookupRequestData = New repairLookupInfoType() With
                                                    {
                                                        .serialNumber = oSerialNumber
                                                                                                            },
                                                    .userSession = Me.Session
                                                 })


            If oRepairLookupResponse.lookupResponseData.Count = 0 Then
                Throw New RepairNotFoundException()
            End If

        Catch ex As Exception
            Throw New RepairNotFoundException()
        End Try


        ' Look for Repairs Details
        Dim oRepairDetailsLookup As repairDetailsLookupResponseType
        oRepairDetailsLookup = Me.Client.RepairDetailsLookup(New repairDetailsLookupRequestType() With
                                                           {
                                                            .repairConfirmationNumber = oRepairLookupResponse.lookupResponseData.FirstOrDefault().repairConfirmationNumber,
                                                            .userSession = Me.Session
                                                           })

        ' Find IMEI Number based on Serial Number
        Dim oSerialToImeiLookupResponse As fetchIOSActivationDetailsResponseType
        oSerialToImeiLookupResponse = Me.Client.FetchIOSActivationDetails(New fetchIOSActivationDetailsRequestType() With
                                                                               {
                                                                                    .serialNumber = oRepairDetailsLookup.repairDetails.newSerialNumber,
                                                                                    .userSession = Me.Session
                                                                               })

        Dim response As New FindOriginalDeviceInfoResponse
        response.ImeiNumber = oSerialToImeiLookupResponse.activationDetailsInfo.imeiNumber
        response.SerialNumber = oSerialToImeiLookupResponse.activationDetailsInfo.serialNumber

        Return response

    End Function



End Class


