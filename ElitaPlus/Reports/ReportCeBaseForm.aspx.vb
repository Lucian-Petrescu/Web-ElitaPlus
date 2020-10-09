Namespace Reports

    Partial Class ReportCeBaseForm
        Inherits ReportCeBase


#Region "Constants"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ReportCeBaseForm.aspx"

#End Region

#Region "Variables"


#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Try
                If Not Page.IsPostBack Then
                    SetStateProperties()
                    Dim strReportServer As String = ""

                    strReportServer = Session("REPORT_SERVER")
                    If strReportServer IsNot Nothing AndAlso strReportServer.Equals("SSRS") Then
                        'future SSRS code
                    End If
                End If
            Catch exT As System.Threading.ThreadAbortException
            Catch ex As Exception
                AppConfig.DebugLog(ex.Message)
                State.ErrStatus = SSHelper.RptStatus.SS_UNKNOWN_PROBLEM
            Finally
                If Not Page.IsPostBack Then
                    If Me.State.ErrStatus = SSHelper.RptStatus.PENDING Then
                        'ContinueWaitingForReport()
                    ElseIf Me.State.ErrStatus = SSHelper.RptStatus.SS_MAX_REPORTS Then
                        CloseProgressBarParent(State.ErrStatus.GetName(GetType(SSHelper.RptStatus), State.ErrStatus),
                                          "", State.ErrMsg, "", "")
                    Else
                        CloseProgressBarParent(State.ErrStatus.GetName(GetType(SSHelper.RptStatus), State.ErrStatus),
                                    GetRptViewerName(), State.ErrMsg, GetRptAction(),
                                    ReportCeBase.GetRptFtp(State.moParams))
                    End If

                End If
            End Try
        End Sub

#End Region

#Region "Handlers-Button"

        Private Sub btnHtmlHidden_Click(sender As System.Object, e As System.EventArgs) Handles btnHtmlHidden.Click
            If State.moParams.moAction <> RptAction.SCHEDULE Then
                ViewReport()
                '  CloseReportTimerControl("True")
            End If
            CloseReportTimerControl("True")
        End Sub

        Private Sub btnContinueForReport_Click(sender As System.Object, e As System.EventArgs) Handles btnContinueForReport.Click
            'ContinueWaiting()
        End Sub

#End Region
#End Region

#Region "View"
        Private Sub ViewReport()
            If State.ErrStatus <> SSHelper.RptStatus.SUCCESS Then Return
            ' It will call the Viewer
            If Me.GetRptViewer = Me.RptViewer.WINDOWOPEN Then
                Dim strReportServer As String = ""
                strReportServer = Session("REPORT_SERVER")
                If strReportServer IsNot Nothing AndAlso strReportServer.Equals("SSRS") Then
                    WindowOpen(ReportCeOpenWindowForm.URL & "?REPORT_SERVER=" & strReportServer, TranslationBase.TranslateLabelOrMessage(ReportCeBaseForm.REPORTS_UICODE) &
                                                                   ": " & State.moParams.msRptWindowName)
                Else
                    'A detached window will be used
                    WindowOpen(ReportCeOpenWindowForm.URL & "?file=file.pdf", TranslationBase.TranslateLabelOrMessage(ReportCeBaseForm.REPORTS_UICODE) &
                                                ": " & State.moParams.msRptWindowName)
                End If

            End If
        End Sub
#End Region


    End Class

End Namespace
