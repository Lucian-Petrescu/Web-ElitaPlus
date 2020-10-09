Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Public Class FalabellaSLAMonitoringTask
    Inherits TaskBase

#Region "Fields"
    Private _claimId As Guid
#End Region
#Region "Properties"
    Private Property ClaimId As Guid
        Get
            Return _claimId
        End Get
        Set(value As Guid)
            _claimId = value
        End Set
    End Property
#End Region

#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

    Protected Friend Overrides Sub Execute()
        Dim intErrCode As Integer, strErrMsg As String = String.Empty
        Logger.AddDebugLogEnter()
        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_ID))) Then
                ClaimId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_ID)))
                If (Not ClaimId.Equals(Guid.Empty)) Then
                    PublishedTask.CheckSLAClaimStatus(ClaimId, intErrCode, strErrMsg)
                    If intErrCode <> 0 Then
                        Throw New Exception("Falabella SLA Monitoring Err:" & intErrCode.ToString() & " - " & strErrMsg)
                    End If
                End If
            End If
        Catch ex As Exception
            Logger.AddError(ex)
        Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub

End Class
