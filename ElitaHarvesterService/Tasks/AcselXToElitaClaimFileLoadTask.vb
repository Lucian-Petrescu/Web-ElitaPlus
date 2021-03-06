﻿Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Common

Public Class AcselXToElitaClaimFileLoadTask
    Inherits FileLoadTaskBase
#Region "Fields"
    Private _claimLoadFileProcessedId As Guid
    Private _acselXToEliatClaimFileLoad As AcselXToElitaClaimFileLoad
#End Region

#Region "Constructors"
    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

#Region "Properties"
    Private Property ClaimLoadFileProcessedId As Guid
        Get
            Return _claimLoadFileProcessedId
        End Get
        Set(ByVal value As Guid)
            _claimLoadFileProcessedId = value
        End Set
    End Property

    Private Property AcselXToEliatClaimFileLoad As AcselXToElitaClaimFileLoad
        Get
            Return _acselXToEliatClaimFileLoad
        End Get
        Set(ByVal value As AcselXToElitaClaimFileLoad)
            _acselXToEliatClaimFileLoad = value
        End Set
    End Property

#End Region

    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Try
            Dim claimLoadFileProcessedId As Guid
            If (String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIMLOAD_FILE_PROCESSED_ID))) Then
                Logger.AddError("Argument Claim Load File Processed ID is not supplied in Argument")
                Me.FailReason = "Argument Claim Load File Processed ID is not supplied in Argument"
                Throw New ArgumentException("Argument Claim Load File Processed ID is not supplied in Argument")
            Else
                claimLoadFileProcessedId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIMLOAD_FILE_PROCESSED_ID)))
            End If
            Me.ClaimLoadFileProcessedId = claimLoadFileProcessedId
            Me._acselXToEliatClaimFileLoad = New AcselXToElitaClaimFileLoad()
            Me._acselXToEliatClaimFileLoad.Process(Me.ClaimLoadFileProcessedId)
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub

End Class
