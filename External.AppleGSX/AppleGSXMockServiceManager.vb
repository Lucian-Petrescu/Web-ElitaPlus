
#If DEBUG Then

Imports Assurant.ElitaPlus.External.Interfaces

Public Class AppleGSXMockServiceManager
    Implements IAppleGSXServiceManager

    Public Function FindOriginalDeviceInfo(pRequest As FindOriginalDeviceInfoRequest) As FindOriginalDeviceInfoResponse Implements IAppleGSXServiceManager.FindOriginalDeviceInfo
        If ((pRequest.ImeiNumber = "1234") OrElse (pRequest.SerialNumber = "S1234")) Then
            Return New FindOriginalDeviceInfoResponse() With
                {
                    .ImeiNumber = "2345",
                    .SerialNumber = "S2345"
                }
        End If
        If ((pRequest.ImeiNumber = "2345") OrElse (pRequest.SerialNumber = "S2345")) Then
            Return New FindOriginalDeviceInfoResponse() With
                {
                    .ImeiNumber = "3456",
                    .SerialNumber = "S3456"
                }
        End If
        If ((pRequest.ImeiNumber = "3456") OrElse (pRequest.SerialNumber = "S3456")) Then
            Return New FindOriginalDeviceInfoResponse() With
                {
                    .ImeiNumber = "4567",
                    .SerialNumber = "S4567"
                }
        End If

        Throw New RepairNotFoundException()

    End Function
End Class

#End If

