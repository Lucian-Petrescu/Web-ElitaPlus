Imports System.IO
Imports Assurant.Common.Zip.aZip
Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Public Class ReportCeBase
        Inherits ElitaPlusSearchPage

#Region "Page Parameters"

        Public Enum RptFormat
            JAVA
            PDF
            TEXT_CSV
            TEXT_TAB
        End Enum

        Public Enum RptAction
            SCHEDULE
            SCHEDULE_VIEW
            VIEW
        End Enum



        Public Class CsvSeparator
            Public Const CSV_SEPARATOR_0166 As String = "¦" ' Alt 0166
            Public Const CSV_SEPARATOR_COMMA As String = "," ' ,
        End Class

        Public Class CsvDelimiter
            Public Shared ReadOnly CSV_DELIMITER_NONE As String = String.Empty
            Public Shared ReadOnly CSV_DELIMITER_DQUOTE As String = """"
        End Class





#Region "Report Parameter Class"

        Public Class RptParam
            Public moCeHelperParameter As ceHelperParameter

            Public Sub New(ByVal parmName As String, ByVal parmValue As String,
            Optional ByVal parmSubReportName As String = ceHelperParameter.EMPTY_CEPARAM)
                moCeHelperParameter = New ceHelperParameter(parmName, parmValue, parmSubReportName)
            End Sub

        End Class
#End Region

        Public Class Params
            Public msRptName As String
            Public msRptWindowName As String
            Public moRptFormat As RptFormat
            Public moAction As RptAction
            Public instanceId As Long
            Public moRptParams() As RptParam
            Public msCsvDelimiter As String
            Public msCsvSeparator As String
            Public moDest As ceDestination
            Public moSched As ceSchedule

            Public Sub New()
                ' Default Values
                instanceId = NO_INSTANCE
                msCsvDelimiter = CsvDelimiter.CSV_DELIMITER_NONE
                msCsvSeparator = CsvSeparator.CSV_SEPARATOR_0166
            End Sub

        End Class

#End Region

#Region "Viewer"

        Public Enum RptViewer
            IFRAME
            WINDOWOPEN
        End Enum

#End Region

#Region "Constants"

        Public Shared REPORTS_UICODE As String = "Reports"
        Public Const SESSION_PARAMETERS_KEY As String = "REPORTCE_BASE_SESSION_PARAMETERS_KEY"
        Public Const SESSION_PARAMETERS_DOWNLOAD_KEY As String = "DOWNLOAD_BASE_SESSION_PARAMETERS_KEY"
        Private ReadOnly MAX_SCHED_MINUTES As Integer = Convert.ToInt32(AppConfig.Application.CeTimeout)
        ' Private ReadOnly MAX_SCHED_MINUTES As Integer = 2
        Private ReadOnly MAX_SCHED_POLL As Integer = Convert.ToInt32((MAX_SCHED_MINUTES * 60) / 5)
        'Private Const CSV_SEPARATOR_0166 As String = "¦" ' Alt 0166
        'Public Const CSV_SEPARATOR_COMMA As String = "," ' ,
        'Public ReadOnly CSV_DELIMITER_NONE As String = String.Empty
        'Public ReadOnly CSV_DELIMITER_DQUOTE As String = """"
        Private Const NO_INSTANCE As Long = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        '  Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

        Public Shared MAX_REPORTS As Integer = 8
        Public Shared MAX_REPORTS_BY_USER As Integer = 3
        Public Shared MAX_SAME_REPORTS_BY_USER As Integer = 2
        Public Shared SESSION_INSTANCE_ID As String = "INSTANCE_ID"

#End Region

#Region "Variables"

        ' Dim moStatus As CEHelper.RptStatus
        Private moError As SSHelper.RptError

#End Region

#Region " Properties "
        Public Property Status() As SSHelper.RptStatus
            Get
                Return moError.status
            End Get
            Set(ByVal Value As SSHelper.RptStatus)
                moError.status = Value
            End Set
        End Property

        Public Property ErrorMsg() As String
            Get
                Return moError.msg
            End Get
            Set(ByVal Value As String)
                moError.msg = Value
            End Set
        End Property

#End Region

#Region "Page State"

#Region "MyState"


        Class MyState

            Public moRptPath As String
            Public moRptFormat As SSHelper.RepFormat
            Public moRptParams As ArrayList
            Public moParams As Params
            '   Public moStatus As CEHelper.RptStatus
            Private sError As SSHelper.RptError

            Public Property Err() As SSHelper.RptError
                Get
                    Return sError
                End Get
                Set(ByVal Value As SSHelper.RptError)
                    sError = Value
                End Set
            End Property

            Public Property ErrStatus() As SSHelper.RptStatus
                Get
                    Return sError.status
                End Get
                Set(ByVal Value As SSHelper.RptStatus)
                    sError.status = Value
                End Set
            End Property

            Public Property ErrMsg() As String
                Get
                    Return sError.msg
                End Get
                Set(ByVal Value As String)
                    sError.msg = Value
                End Set
            End Property
#Region "Future -1"
            'Public moCommissionPeriodId As Guid = Guid.Empty
            'Public moDealerId As Guid = Guid.Empty
            'Public mnPageIndex As Integer
            'Public searchDV As DataView = Nothing
            'Public oRepInstances As DataSet
            'Public PageSize As Integer = DEFAULT_PAGE_SIZE
            'Public IsGridVisible As Boolean = False
#End Region

            Sub New()
                '  moStatus = CEHelper.RptStatus.SUCCESS
                sError.status = SSHelper.RptStatus.SUCCESS
            End Sub

        End Class
#End Region

        Public Sub New()
            'MyBase.New(New MyState)
            MyBase.New(True)
        End Sub

        Public Sub New(ByVal useExistingState As Boolean)
            MyBase.New(useExistingState)
        End Sub

        'Protected Shadows ReadOnly Property State() As MyState
        '    Get
        '        Return CType(MyBase.State, MyState)
        '    End Get
        'End Property

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Dim st As MyState = Nothing
                Dim key As Type = Me.GetType()
                If Me.StateSession.Contains(key) Then
                    st = CType(Me.StateSession.Item(key), MyState)
                End If
                If st Is Nothing Then
                    st = New MyState
                    Me.StateSession.Item(key) = st
                End If
                Return st
            End Get
        End Property

        Protected Sub SetStateProperties()
            SetStateProperties_NoErrStatus()
            Me.State.ErrStatus = SSHelper.RptStatus.SUCCESS
        End Sub

        Protected Sub SetStateProperties_NoErrStatus()
            Me.State.moParams = CType(Session(SESSION_PARAMETERS_KEY), Params)
            Me.State.moRptPath = AppConfig.SS.RootDir & "\" & Me.State.moParams.msRptName
            Me.State.moRptFormat = GetRepFormat(Me.State.moParams)
            Me.State.moRptParams = GetCeRptParams(Me.State.moParams.moRptParams)
        End Sub

#End Region

#Region "CeHelper"

        Public Shared Function GetSSHelper() As SSHelper
            Dim oSSHelper As SSHelper

            If Not HttpContext.Current.Session(ELPWebConstants.SSHELPER) Is Nothing Then
                oSSHelper = CType(HttpContext.Current.Session(ELPWebConstants.SSHELPER), SSHelper)
            Else
                Try
                    oSSHelper = GetNewSSHelper()
                Catch ex As Exception
                    If ((AppConfig.SS.MachineNameDr <> AppConfig.CE_NO_DR) AndAlso
                          (AppConfig.SS.ViewerMachineNameDr <> AppConfig.CE_NO_DR)) Then
                        oSSHelper = GetNewSSHelper(True)
                    Else
                        Throw ex
                    End If
                End Try

            End If

            Return oSSHelper

        End Function



        Public Shared Function GetNewSSHelper(Optional ByVal useDr As Boolean = False) As SSHelper
            Dim conn As ssHelperConnection
            Dim oSSHelper As SSHelper
            Dim objParameters As ElitaPlusParameters = CType(System.Threading.Thread.CurrentPrincipal.Identity, ElitaPlusParameters)

            With conn
                .ssHttpProtocol = "//"
                If useDr = False Then
                    .ssMachineName = AppConfig.SS.MachineName
                    .ssViewerMachineName = AppConfig.SS.ViewerMachineName
                Else
                    .ssMachineName = AppConfig.SS.MachineNameDr
                    .ssViewerMachineName = AppConfig.SS.ViewerMachineNameDr
                End If
                .rootDir = AppConfig.SS.RootDir
                .ReportPath = AppConfig.SS.ReportPath
                .ReportServerUrl = AppConfig.SS.ReportServerUrl
                .ssUserID = AppConfig.SS.UserId
                .ssPW = AppConfig.SS.Password
                ' DataBase
                .dbServerType = SsReportServerType.ssServerTypeOracle
                .dbServer = AppConfig.DataBase.Server

                If Not objParameters Is Nothing AndAlso Not objParameters.AppUserId Is Nothing Then
                    .dbUserID = objParameters.AppUserId
                    .dbPW = objParameters.AppPassword
                Else
                    .dbUserID = AppConfig.DataBase.UserId
                    .dbPW = AppConfig.DataBase.Password
                End If

                .domain = AppConfig.SS.Domain
            End With
            oSSHelper = New SSHelper(conn)
            HttpContext.Current.Session(ELPWebConstants.SSHELPER) = oSSHelper

            Return oSSHelper

        End Function


        Public Shared Function GetSSRSReportServerUri() As Uri

            Return New Uri(AppConfig.SS.ReportServerUrl)
            'Return New Uri("http://atl0wsrsd020.cead.prd/ReportServer")

        End Function

        Public Shared Function GetSSRSReportPath() As String

            Return "/" & AppConfig.SS.RootDir & "/" & AppConfig.SS.ReportPath & "/"

        End Function
#End Region


#Region "Mapping Parameters"

        Public Shared Function GetRepFormat(ByVal oParams As ReportCeBase.Params) As SSHelper.RepFormat
            Dim oRptFormat As RptFormat = oParams.moRptFormat
            Dim oRepFormat As SSHelper.RepFormat

            Select Case oRptFormat
                Case RptFormat.JAVA
                    oRepFormat.moSsScheduleFormat = SSHelper.SsReportFormat.ceFormatCrystalReport
                    oRepFormat.moViewer = SSHelper.RptViewer.JAVA
                    oRepFormat.msFileExt = SSHelper.CRYSTAL_EXTENSION
                Case RptFormat.PDF
                    oRepFormat.moSsScheduleFormat = SSHelper.SsReportFormat.ceFormatPDF
                    oRepFormat.moViewer = SSHelper.RptViewer.PDF
                    oRepFormat.msFileExt = SSHelper.PDF_EXTENSION
                Case RptFormat.TEXT_CSV
                    oRepFormat.moSsScheduleFormat = SSHelper.SsReportFormat.ceFormatTextCharacterSeparated
                    oRepFormat.moViewer = SSHelper.RptViewer.TEXT_CSV
                    oRepFormat.msFileExt = SSHelper.CSV_EXTENSION
                    oRepFormat.csvDelimiter = oParams.msCsvDelimiter
                    oRepFormat.csvSeparator = oParams.msCsvSeparator
                Case RptFormat.TEXT_TAB
                    oRepFormat.moSsScheduleFormat = SSHelper.SsReportFormat.ceFormatTextTabSeparatedText
                    oRepFormat.moViewer = SSHelper.RptViewer.TEXT_TAB
                    oRepFormat.msFileExt = SSHelper.CSV_EXTENSION
            End Select

            Return oRepFormat
        End Function



        Public Function GetCeRptParams(ByVal oRptParams() As RptParam) As ArrayList
            Dim oCeRptParams As New ArrayList
            Dim oRptParam As RptParam
            Dim oCeHelperParameter As ceHelperParameter

            If Not oRptParams Is Nothing Then
                For Each oRptParam In oRptParams
                    If Not oRptParam Is Nothing Then
                        oCeRptParams.Add(oRptParam.moCeHelperParameter)
                    Else
                        'TODO Write Log for the error of the missing parameter.
                        'Include report name, parameter value, etc...  
                        ' Write to error log, send email, or raise error.
                    End If
                Next
            End If

            Return oCeRptParams
        End Function
#End Region

#Region "Actions"

        Public Function GetRptAction() As String
            Dim oAction As String

            oAction = [Enum].GetName(GetType(RptAction), Me.State.moParams.moAction)

            Return oAction
        End Function

#End Region

#Region "Schedule"

        Public Shared Function GetRptFtp(ByVal oParams As ReportCeBase.Params) As String
            Dim oFtp As String = String.Empty
            Dim sDate As String = "Now"
            Dim dEmptyDate As DateTime = Nothing

            If ((Not oParams.moSched Is Nothing) AndAlso (Not oParams.moSched.ftp Is Nothing)) Then
                If Not oParams.moSched.startDateTime.Equals(dEmptyDate) Then
                    sDate = ElitaPlusPage.GetLongDateFormattedString(oParams.moSched.startDateTime)
                End If
                oFtp = sDate & " " & oParams.moSched.ftp.directory & oParams.moSched.ftp.filename
            End If

            Return oFtp
        End Function

#End Region

#Region "Access Report"
        Public Shared Function GetReportFormat(ByVal form As ElitaPlusPage) As ReportCeBaseForm.RptFormat
            Dim rptFormat As ReportCeBaseForm.RptFormat
            Dim rptCeInputCtrl As ReportCeInputControl = CType(form.FindControl("moReportCeInputControl"), ReportCeInputControl)
            Dim view As RadioButton
            Dim pdf As RadioButton
            Dim txt As RadioButton
            If Not rptCeInputCtrl Is Nothing Then
                view = CType(rptCeInputCtrl.FindControl("radiobuttonView"), RadioButton)
                pdf = CType(rptCeInputCtrl.FindControl("RadiobuttonPDF"), RadioButton)
                txt = CType(rptCeInputCtrl.FindControl("RadiobuttonTXT"), RadioButton)
            Else
                rptCeInputCtrl = form.TheReportCeInputControl
                view = rptCeInputCtrl.RadioButtonVIEWControl
                pdf = rptCeInputCtrl.RadioButtonPDFControl
                txt = rptCeInputCtrl.RadioButtonTXTControl
            End If

            If (view.Checked) Then
                rptFormat = ReportCeBaseForm.RptFormat.JAVA
            ElseIf (pdf.Checked) Then
                rptFormat = ReportCeBaseForm.RptFormat.PDF
            ElseIf (txt.Checked) Then
                ' rptFormat = ReportCeBaseForm.RptFormat.TEXT_TAB
                rptFormat = ReportCeBaseForm.RptFormat.TEXT_CSV
            End If

            Return (rptFormat)
        End Function

        Public Shared Function GetReportFormat(ByVal ceFormat As String) As ReportCeBaseForm.RptFormat
            Dim oRptFormat As ReportCeBaseForm.RptFormat
            Dim ceRptFormat As SSHelper.RptFormat =
                        CType(SSHelper.RptFormat.Parse(GetType(SSHelper.RptFormat), ceFormat), SSHelper.RptFormat)

            Select Case ceRptFormat
                Case SSHelper.RptFormat.CRYSTAL
                    oRptFormat = ReportCeBaseForm.RptFormat.JAVA
                Case SSHelper.RptFormat.PDF
                    oRptFormat = ReportCeBaseForm.RptFormat.PDF
                Case SSHelper.RptFormat.TEXT_CSV
                    oRptFormat = ReportCeBaseForm.RptFormat.TEXT_CSV
                Case SSHelper.RptFormat.TEXT_TAB
                    oRptFormat = ReportCeBaseForm.RptFormat.TEXT_TAB
                Case Else
                    oRptFormat = Nothing
            End Select

            Return (oRptFormat)
        End Function


        Public Shared Sub EnableReportCe(ByVal form As ElitaPlusPage, ByVal TheReportCeInputControl As ReportCeInputControl)
            ' Enable Progress Bar
            TheReportCeInputControl.IsBarProgressVisible = "True"
            form.InstallReportCe()
        End Sub
#End Region

#Region "View Report"

        Public Function GetRptViewer() As RptViewer
            Dim oViewer As RptViewer

            If (Me.State.moParams Is Nothing) Then
                SetStateProperties_NoErrStatus()
            End If
            If (Me.State.moParams.moRptFormat = ReportCeBaseForm.RptFormat.TEXT_CSV) OrElse
                    (Me.State.moParams.moRptFormat = ReportCeBaseForm.RptFormat.TEXT_TAB) Then
                oViewer = RptViewer.IFRAME
            Else
                oViewer = RptViewer.WINDOWOPEN
            End If

            Return oViewer
        End Function

        Public Function GetRptViewerName() As String
            Dim oViewer As String

            oViewer = RptViewer.GetName(GetType(RptViewer), GetRptViewer())

            Return oViewer
        End Function

        Private Sub CreateZipFiles(ByVal zipFileName As String, ByVal sourceDirectory As String)
            Try
                Dim sourceDir As String = sourceDirectory.Replace("/", "\")
                Dim destinationFileName As String = sourceDir & "\" & zipFileName
                Dim filter As String = "TXT"
                CreateZipFile(destinationFileName, sourceDir, True, filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub DownloadFile(ByVal fileName As String, ByVal sourceDirectory As String)

            Dim pathFileName As String = sourceDirectory & "\" & fileName

            Response.ClearContent()
            Response.ClearHeaders()
            '  Response.ContentType = "application/x-download" 
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)
            '  Response.AddHeader("Content-Length", byteStream.Length.ToString)
            '  Response.TransmitFile(fullFileNameD)
            Response.TransmitFile(pathFileName)
            Response.Flush()
            Response.End()

        End Sub

        Private Sub SendResponse(ByVal mimeString As String, ByVal byteStream() As Byte, ByVal filename As String)
            Dim sAttachFilename As String

            '  sAttachFilename = "attachment; filename=" & filename  ' Open/Save
            sAttachFilename = "inline; filename=" & MiscUtil.RemoveInvalidChar(filename)
            'sAttachFilename = "inline; filename=" & filename   
            '   sAttachFilename = "filename=" & filename
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", sAttachFilename)
            Response.ContentType = mimeString
            Response.BinaryWrite(byteStream)
            Response.Flush()
            Response.End()
        End Sub

        Private Sub CreateFolder(ByVal folderName As String)
            Dim objDir As New DirectoryInfo(folderName)
            Try
                If Not objDir.Exists() Then
                    objDir.Create()
                End If
            Catch ex As Exception
                '  HandleErrors(ex, Me.ErrCollection)
            End Try
        End Sub
#End Region

#Region "Formating & Validation"

        Public Shared Function FormatDate(ByVal lbl As Label, ByVal sDate As String) As String
            Dim sFormated As String
            Dim tempDate As Date

            GUIException.ValidateDate(lbl, sDate)
            tempDate = DateHelper.GetDateValue(sDate)
            sFormated = tempDate.ToString(SP_DATE_FORMAT)

            Return sFormated
        End Function

        Public Shared Sub ValidateBeginEndDate(ByVal beginLbl As Label, ByVal beginDate As String, ByVal endLbl As Label, ByVal endDate As String)
            Dim tempEndDate As Date
            Dim tempBeginDate As Date

            GUIException.ValidateDate(beginLbl, beginDate)
            tempBeginDate = DateHelper.GetDateValue(beginDate)
            GUIException.ValidateDate(endLbl, endDate)
            tempEndDate = DateHelper.GetDateValue(endDate)
            If tempEndDate < tempBeginDate Then
                ElitaPlusPage.SetLabelError(beginLbl)
                ElitaPlusPage.SetLabelError(endLbl)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
        End Sub

#End Region

#Region "JavaScript"

        Protected Sub SendReportError(ByVal statusMsg As String, ByVal errorMsg As String)
            Dim sJavaScript As String
            Dim transMsg As String = TranslationBase.TranslateLabelOrMessage(statusMsg)

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            '  sJavaScript &= "parent.document.all('moReportCeInputControl_moReportCeStatus').value = '" & transMsg & "';" & Environment.NewLine
            '  sJavaScript &= "parent.document.all('moReportCeInputControl_moReportCeErrorMsg').value = '" & errorMsg & "';" & Environment.NewLine
            sJavaScript &= "parent.SendReportError('" & transMsg & "','" & errorMsg & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("SendReportError", sJavaScript)
        End Sub

        Protected Sub ContinueWaitingForReport()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "buttonContinueForReportClick();" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ContinueWaitingForReport", sJavaScript)
        End Sub
#End Region
    End Class

End Namespace
