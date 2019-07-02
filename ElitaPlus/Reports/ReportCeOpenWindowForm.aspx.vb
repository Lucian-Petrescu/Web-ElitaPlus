Imports Microsoft.Reporting.WebForms

Namespace Reports

    Partial Class ReportCeOpenWindowForm
        Inherits ReportCeBase
        ' Inherits System.Web.UI.Page

#Region "Constants"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ReportCeOpenWindowForm.aspx"
        'Public Shared REPORTS_UICODE As String = "Reports"
        'Public Const SESSION_PARAMETERS_KEY As String = "REPORTCE_BASE_SESSION_PARAMETERS_KEY"

#End Region
#Region "State"
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
#End Region
#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"
        Protected WithEvents ErrorCtrl As ErrorController
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                If Not Page.IsPostBack Then
                    Dim strReportServer As String = ""
                    strReportServer = Request.QueryString("REPORT_SERVER")
                    If Not strReportServer Is Nothing AndAlso strReportServer.Equals("SSRS") Then
                        'Dim oParams As ReportCeBase.Params = CType(Session(ReportCeBase.SESSION_PARAMETERS_KEY), ReportCeBase.Params)
                        RunSSRSReport(Session("REPORT_NAME"))
                    End If

                End If
            Catch exT As System.Threading.ThreadAbortException
            Catch ex As Exception
                '   Catch ex As Exception
                Me.State.ErrStatus = SSHelper.RptStatus.SS_VIEW_PROBLEM
                Me.HandleErrors(ex, Me.ErrorCtrl)
                ' End Try
                '     Me.State.moStatus = CEHelper.RptStatus.CE_UNKNOWN_PROBLEM
            Finally
                If Not Page.IsPostBack Then
                    If Me.GetRptViewer = Me.RptViewer.IFRAME Then
                        Me.SendReportError(Me.State.ErrStatus.GetName(GetType(SSHelper.RptStatus), Me.State.ErrStatus),
                            Me.State.ErrMsg)
                    End If

                End If
            End Try
        End Sub

#End Region

#End Region

        Public Sub RunSSRSReport(ByVal strReportName As String)

            Try
                Dim oParams As ArrayList
                Dim oParamsList As ReportCeBase.Params
                oParamsList = CType(Session(ReportCeBase.SESSION_PARAMETERS_KEY), ReportCeBase.Params)
                oParams = GetCeRptParams(oParamsList.moRptParams)

                Dim reportParameterCollection() As Microsoft.Reporting.WebForms.ReportParameter
                If oParams.Count > 0 Then
                    reportParameterCollection = New Microsoft.Reporting.WebForms.ReportParameter(oParams.Count - 1) {}

                    Dim index As Integer
                    For index = 0 To oParams.Count - 1
                        reportParameterCollection(index) = New Microsoft.Reporting.WebForms.ReportParameter()
                        reportParameterCollection(index).Name = oParams(index).name
                        reportParameterCollection(index).Values.Add(oParams(index).value)
                    Next

                End If

                Dim oSSHelper As SSHelper = ReportCeBase.GetSSHelper()
                SSRSReportViewer.ServerReport.ReportServerCredentials = oSSHelper.GetReportServerCredentials
                SSRSReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
                SSRSReportViewer.ServerReport.ReportServerUrl = ReportCeBase.GetSSRSReportServerUri
                SSRSReportViewer.ServerReport.ReportPath = ReportCeBase.GetSSRSReportPath & strReportName
                SSRSReportViewer.ServerReport.SetParameters(reportParameterCollection)
                SSRSReportViewer.PromptAreaCollapsed = True

                Dim oSSRSCredential As DataSourceCredentials = oSSHelper.GetDataSourceCredentials(SSRSReportViewer)
                If Not oSSRSCredential Is Nothing Then
                    SSRSReportViewer.ServerReport.SetDataSourceCredentials(New DataSourceCredentials() {oSSRSCredential})
                End If 

                SSRSReportViewer.ServerReport.Refresh()
                System.Threading.Thread.Sleep(1000)
                SSRSReportViewer.ServerReport.Refresh()

            Catch ex As Exception
                'if Report RDL file not found in another language forcing to look for English extension 
                If ex.Message.Contains("rsItemNotFound") And Not strReportName.Contains("_EN") Then
                    RunSSRSReport(strReportName.Substring(0, strReportName.Length - 2) & "EN")
                Else
                    Me.HandleErrors(ex, Me.ErrorCtrl)
                End If
            End Try
        End Sub
    End Class

End Namespace
