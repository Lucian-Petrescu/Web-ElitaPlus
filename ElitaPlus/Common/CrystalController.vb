Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Reflection

Namespace Generic

    
    Public Class CrystalController

#Region "Constants"
        Protected Const EXCEPTION_TEXT As String = "CrystalController can not access Data Source -- "
        Public Const EXPORT_PDF As String = "pdf"
        Public Const EXPORT_RTF As String = "rtf"
        Public Const EXPORT_DOC As String = "doc"
        Public Const EXPORT_XLS As String = "xls"

#End Region

#Region "Attributes"

        Private moReport As ReportDocument
        Private moCachedReportType As Type
        Private moCachedReport As Object
        Private moDataSet As DataSet

#End Region

#Region "Properties"

        Public ReadOnly Property Report() As ReportDocument
            Get
                If moReport Is Nothing Then
                    If moCachedReport Is Nothing Then
                        Dim oConstructor As ConstructorInfo = moCachedReportType.GetConstructor(Type.EmptyTypes)
                        moCachedReport = oConstructor.Invoke(Nothing)
                    End If
                    Dim oCachedReportMethod As MethodInfo = moCachedReportType.GetMethod("CreateReport")
                    moReport = DirectCast(oCachedReportMethod.Invoke(moCachedReport, Nothing), ReportDocument)
                    moReport.SetDataSource(moDataSet)
                    moReport.Refresh()
                End If

                '       ARF Commented out on 09/01/04. BEGIN
                'Dim oSect As Section = moReport.ReportDefinition.Sections("Section2")
                'Dim oTranslation As New TranslationProcess
                'oTranslation.TranslateCrystalHeader(oSect.ReportObjects)
                '       ARF Commented out on 09/01/04. END

                'Dim oItem As ReportObject
                'Dim oText As String
                'For Each oItem In oSect.ReportObjects
                '    If TypeOf oItem Is TextObject Then
                '        oText = CType(oItem, TextObject).Text
                '    End If
                'Next

                Return moReport
            End Get

        End Property

#End Region

#Region "Constructors"
        Public Sub New(ByVal rep As ReportDocument)
            Me.moReport = rep
        End Sub

        Public Sub New(ByVal aDataSet As DataSet, ByVal reportName As String)
            Dim oAssembly As New AssemblyInformation

            moDataSet = aDataSet
            '   moCachedReportType = Type.GetType(reportNameSpace)
            moCachedReportType = oAssembly.FromNameToType(reportName)
            If (moCachedReportType Is Nothing) Or (moDataSet Is Nothing) Then
                Throw New DataNotFoundException(EXCEPTION_TEXT)
            End If
        End Sub
#End Region



        Public Sub GenerateXML(ByVal aDataSet As DataSet, ByVal aPath As String)
            aDataSet.WriteXml(aPath)
        End Sub

        Public Sub ExportToClient(ByVal pg As Page, ByVal aFormat As String)
            ' Declare variables and get the export options.
            Dim oExportOpts As ExportOptions
            oExportOpts = Report.ExportOptions

            pg.Response.ClearHeaders()
            pg.Response.ClearContent()

            Select Case aFormat
                Case EXPORT_PDF
                    oExportOpts.ExportFormatType = ExportFormatType.PortableDocFormat
                    oExportOpts.FormatOptions = New PdfRtfWordFormatOptions
                    pg.Response.ContentType = "application/pdf"
                Case EXPORT_RTF
                    oExportOpts.ExportFormatType = ExportFormatType.RichText
                    oExportOpts.FormatOptions = New PdfRtfWordFormatOptions
                    pg.Response.ContentType = "application/msword"
                Case EXPORT_DOC
                    oExportOpts.ExportFormatType = ExportFormatType.WordForWindows
                    oExportOpts.FormatOptions = New PdfRtfWordFormatOptions
                    pg.Response.ContentType = "application/msword"
                Case EXPORT_XLS
                    oExportOpts.ExportFormatType = ExportFormatType.Excel
                    oExportOpts.FormatOptions = New ExcelFormatOptions
                    pg.Response.ContentType = "application/vnd.ms-excel"

            End Select

            Dim oReq As New ExportRequestContext
            oReq.ExportInfo = oExportOpts
            Dim oSt As Stream = Report.FormatEngine.ExportToStream(oReq)
            oSt.Flush()
            Dim nLength As Integer = CType(oSt.Length, Integer)
            Dim oB(nLength + 1) As Byte

            oSt.Read(oB, 0, nLength)
            oSt.Close()
            pg.Response.BinaryWrite(oB)
            pg.Response.End()

        End Sub

        Public Sub ExportToDisk(ByVal fileName As String, ByVal anExportFormat As ExportFormatType)
            ' Declare variables and get the export options.
            Dim oExportOpts As New ExportOptions
            Dim oDiskOpts As New DiskFileDestinationOptions
            oExportOpts = Report.ExportOptions

            ' Set the export format.
            oExportOpts.ExportFormatType = anExportFormat
            oExportOpts.ExportDestinationType = ExportDestinationType.DiskFile

            ' Set the disk file options.
            oDiskOpts.DiskFileName = fileName
            oExportOpts.DestinationOptions = oDiskOpts

            ' Export the report.
            Report.Export()
        End Sub

    End Class

End Namespace
