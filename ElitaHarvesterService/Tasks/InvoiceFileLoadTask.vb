Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Common


Public Class InvoiceFileLoadTask
    Inherits FileLoadTaskBase
#Region "Fields"
    Private _claimLoadFileProcessedId As Guid
    Private _invoiceFileLoad As InvoiceFileLoad
#End Region
#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region
#Region "Properties"
    Private Property ClaimLoadFileProcessedId As Guid
        Get
            Return _claimLoadFileProcessedId
        End Get
        Set(value As Guid)
            _claimLoadFileProcessedId = value
        End Set
    End Property

    Private Property InvoiceFileLoad As InvoiceFileLoad
        Get
            Return _invoiceFileLoad
        End Get
        Set(value As InvoiceFileLoad)
            _invoiceFileLoad = value
        End Set
    End Property

#End Region
    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Try
            Dim claimLoadFileProcessedId As Guid
            If (String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIMLOAD_FILE_PROCESSED_ID))) Then
                Logger.AddError("Argument Claim Load File Processed ID is not supplied in Argument")
                FailReason = "Argument Claim Load File Processed ID is not supplied in Argument"
                Throw New ArgumentException("Argument Claim Load File Processed ID is not supplied in Argument")
            Else
                claimLoadFileProcessedId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIMLOAD_FILE_PROCESSED_ID)))
            End If
            Me.ClaimLoadFileProcessedId = claimLoadFileProcessedId
            InvoiceFileLoad = New InvoiceFileLoad()
            InvoiceFileLoad.Process(Me.ClaimLoadFileProcessedId)
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub
End Class
