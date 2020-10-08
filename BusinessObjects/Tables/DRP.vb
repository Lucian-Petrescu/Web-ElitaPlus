Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.DRPSystem
Imports System.ServiceModel
Imports DRPSystemService = Assurant.ElitaPlus.BusinessObjectsNew.DRPSystem
Partial Public Class DRP
    Private Shared syncRoot As Object = New Object()
    Private Shared oDRPSystemService As DRPSystem.MaxValueRecoveryClient


#Region "Constants"
    Public Const VERSION_NUMBER As String = "1"
#End Region

#Region "Properties"

    Private Shared ReadOnly Property ClientProxy As MaxValueRecoveryClient
        Get

            If (oDRPSystemService Is Nothing OrElse oDRPSystemService.State <> CommunicationState.Opened) Then
                SyncLock syncRoot
                    If (oDRPSystemService Is Nothing OrElse oDRPSystemService.State <> CommunicationState.Opened) Then
                        oDRPSystemService = ServiceHelper.CreateDRPClient()
                    End If
                End SyncLock
            End If
            Return oDRPSystemService
        End Get
    End Property
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function Get_DoesAcceptedOfferExist(IMEI As String) As Boolean
        Dim Result As Boolean
        Dim Auth As DRPSystem.AuthenticationHeader
        Try
            Auth = New DRPSystem.AuthenticationHeader()
            Auth.UserName = ClientProxy.ClientCredentials.UserName.UserName
            Auth.Password = ClientProxy.ClientCredentials.UserName.Password
            Auth.VersionNumber = VERSION_NUMBER
            ClientProxy.Get_DoesAcceptedOfferExist(Auth, IMEI, Result)
            Return Result
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region
End Class
