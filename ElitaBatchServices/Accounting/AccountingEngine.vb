Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports System.Threading


Public Class AccountingEngine
    Inherits ElitaMethodBase

    Private _FelitaEngine As FelitaEngine
    Private _FelitaEngineDs As FelitaEngineDs

    Private _CompanyCode As String = String.Empty
    Private _IncludeVendors As String = String.Empty
    Private _AccountingEvents As String = String.Empty
    Private _AccountingDataSet As String = String.Empty

#Region "PROPERTIES"

    Public Property CompanyCode() As String
        Get
            Return _CompanyCode
        End Get
        Set(value As String)
            _CompanyCode = value
        End Set
    End Property

    Public Property IncludeVendors() As String
        Get
            Return _IncludeVendors
        End Get
        Set(value As String)
            _IncludeVendors = value
        End Set
    End Property

    Public Property AccountingEvents() As String
        Get
            Return _AccountingEvents
        End Get
        Set(value As String)
            _AccountingEvents = value
        End Set
    End Property

#End Region

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function Execute() As String

        Dim strResult As String = String.Empty

        Try
            Instrumentation.WriteLog("STARTED")

            'Set the processing flag to true to prevent multiple instances
            SetIsProcessing(True)

            strResult = _FelitaEngine.ProcessWSRequest()

            Instrumentation.WriteLog("FINIHSED WITH : strResult : " & strResult)

        Catch ex As Exception
            Instrumentation.WriteLog("--------------------------------------------")
            Instrumentation.WriteLog("FAILED WITH EXCEPTION : " & ex.Message)
            Instrumentation.WriteLog("FAILED WITH STACK TRACE : " & ex.StackTrace)
            Instrumentation.WriteLog("--------------------------------------------")

            WriteLogs(String.Format("Error in Batch Services: {0}", GetErrorMessages(ex)), ToString, EventLogEntryType.Error)
            Throw ex

        Finally
            'Clear the processing flag
            SetIsProcessing(False)
        End Try

        Return strResult

    End Function

    Public Overrides Function ExecuteAsync() As String

        Try
            Instrumentation.WriteLog("STARTED")

            WriteLogs(String.Format("Beginning ExecuteAsync"), ToString, EventLogEntryType.Information)

            Dim t As New Thread(AddressOf AsyncMethodSub)
            t.Start()
            Instrumentation.WriteLog("FINISHED")

        Catch ex As Exception
            Instrumentation.WriteLog("--------------------------------------------")
            Instrumentation.WriteLog("FAILED WITH EXCEPTION : " & ex.Message)
            Instrumentation.WriteLog("FAILED WITH STACK TRACE : " & ex.StackTrace)
            Instrumentation.WriteLog("--------------------------------------------")
            'Clear the processing flag
            SetIsProcessing(False)
            WriteLogs(String.Format("Error in Batch Services: {0}", GetErrorMessages(ex)), ToString, EventLogEntryType.Error)
            Throw ex
        End Try

        Return "0"

    End Function

    Protected Overrides Function AsyncMethodSub() As String

        Try
            'Set the processing flag to true to prevent multiple instances
            Instrumentation.WriteLog("STARTED")

            SetIsProcessing(True)
            _FelitaEngine.ProcessWSRequest()
            WriteLogs(String.Format("Completed ExecuteAsync"), ToString, EventLogEntryType.SuccessAudit)
            Instrumentation.WriteLog("FINISHED")

        Catch ex As Exception
            Instrumentation.WriteLog("--------------------------------------------")
            Instrumentation.WriteLog("FAILED WITH EXCEPTION : " & ex.Message)
            Instrumentation.WriteLog("FAILED WITH STACK TRACE : " & ex.StackTrace)
            Instrumentation.WriteLog("--------------------------------------------")
            WriteLogs(String.Format("Error in Batch Services: {0}", GetErrorMessages(ex)), ToString, EventLogEntryType.Error)
        Finally
            'Clear the processing flag
            SetIsProcessing(False)
        End Try


        Return "0"

    End Function

    Private Sub SetDataSet()

        Dim dr As FelitaEngineDs.FelitaEngineRow

        _FelitaEngineDs = New FelitaEngineDs

        If _FelitaEngineDs.Tables.Count = 0 Then
            _FelitaEngineDs.Tables.Add(New FelitaEngineDs.FelitaEngineDataTable)
        End If

        dr = CType(_FelitaEngineDs.Tables(0).NewRow, FelitaEngineDs.FelitaEngineRow)
        dr.CompanyId = CompanyCode
        dr.VendorFiles = IncludeVendors
        dr.AccountingEventId = AccountingEvents

        _FelitaEngineDs.Tables(0).Rows.Add(dr)
        _FelitaEngine = New FelitaEngine(_FelitaEngineDs)

    End Sub

    Public Overrides Sub SetDS(xmlData As String)

        _FelitaEngineDs = New FelitaEngineDs
        _FelitaEngineDs.ReadXml(XMLHelper.GetXMLStream(xmlData))
        _FelitaEngine = New FelitaEngine(_FelitaEngineDs)

    End Sub


End Class
