Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Configuration
Imports System.Text
Imports Assurant.ElitaPlus.External.AppleCare
Imports Assurant.ElitaPlus.External.Interfaces

Public Class AppleCareEnrollItemUpdateTask
    Inherits TaskBase

#Region "Fields"
    Private _certificateId As Guid
    Private _oCertificate As Certificate
    Private oCertificateItems As List(Of CertItem)

#End Region

#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Properties"
    Private Property CertificateId As Guid
        Get
            Return _certificateId
        End Get
        Set(ByVal value As Guid)
            _certificateId = value
        End Set
    End Property

    Private Property oCertificate As Certificate
        Get
            Return _oCertificate
        End Get
        Set(ByVal value As Certificate)
            _oCertificate = value
        End Set
    End Property

#End Region


#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()

        Dim failedLogs As New StringBuilder

        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
                CertificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))
                If (Not CertificateId.Equals(Guid.Empty)) Then
                    oCertificate = New Certificate(CertificateId)
                    oCertificateItems = oCertificate.Items.ToList()
                    Dim countSuccessFull As Integer = 0

                    For Each certItem As CertItem In oCertificateItems
                        Dim CertIMEI As String
                        CertIMEI = If(certItem.IMEINumber Is Nothing, certItem.SerialNumber, certItem.IMEINumber)
                        If CertIMEI IsNot Nothing Then
                            'call to the AppleCare web service to pull the Apple part
                            Dim appleCare As IAppleCareServiceManager = New AppleCareServiceManager()
                            Dim response As ApplePartResponse = appleCare.GetApplePartFromIMEI(CertIMEI)
                            Dim applePartNumber As String = response.PartNumber

                            If response.ErrorResponse IsNot Nothing And applePartNumber Is Nothing Then
                                failedLogs.AppendLine("Item ID: " + certItem.Id.ToString() + " - IMEI: " + CertIMEI + ". " + response.ErrorResponse.ErrorMessage)
                            ElseIf applePartNumber IsNot Nothing Then
                                Dim errMsg As String
                                errMsg = certItem.ProcessAppleCareEnrollment(applePartNumber)
                                If errMsg.ToUpper() <> "null".ToUpper() Then
                                    failedLogs.AppendLine("Item ID: " + certItem.Id.ToString() + " - IMEI: " + applePartNumber + ". " + errMsg)
                                Else
                                    countSuccessFull += 1
                                End If

                            End If

                        Else
                            failedLogs.AppendLine("Item ID: " + certItem.Id.ToString() + ". No IMEI present in the item.")
                        End If

                    Next

                    If failedLogs.Length > 0 Then
                        failedLogs.AppendLine("Successfully processed " + countSuccessFull.ToString() + " out of " + oCertificateItems.Count.ToString())
                        Me.FailReason = failedLogs.ToString()
                        Throw New Exception(Me.FailReason)
                    End If
                End If
            End If
            Logger.AddDebugLogExit()
        Catch ex As Exception
            If failedLogs Is Nothing Then
                failedLogs.AppendLine(ex.Message)
                Me.FailReason = failedLogs.ToString()
            End If

            Logger.AddError(ex)
            Throw
        End Try
    End Sub
#End Region


#Region "Private Methods"
    Public Shared Function SerializeObj(obj As Object) As String
        Dim xs As New System.Xml.Serialization.XmlSerializer(obj.GetType)
        Dim w As New IO.StringWriter
        xs.Serialize(w, obj)
        Return w.ToString
    End Function

#End Region

End Class
