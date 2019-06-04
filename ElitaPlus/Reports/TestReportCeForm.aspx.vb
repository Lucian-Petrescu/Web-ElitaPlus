
Namespace Reports

    Partial Class TestReportCeForm
        Inherits ElitaPlusPage

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME As String = "Hi"


#End Region


#Region "Handlers"


#Region " Web Form Designer Generated Code "


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


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Dim params As ReportCeBaseForm.Params = SetParameters()
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            isReportCeVisible.Value = "True"
            
            ' Me.WindowOpen(ReportCeBaseForm.URL, Me.TranslateLabelOrMessage(ReportCeBaseForm.REPORTS_UICODE) & _
            '         ": " & Me.TranslateLabelOrMessage(RPT_FILENAME))
            '   WindowScript()
            
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Function SetParameters() As ReportCeBaseForm.Params

            'Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
            '        { _
            '         New ReportCeBaseForm.RptParam("HiParam", "How are you")}

            ''Dim params As New ReportCeBaseForm.Params(RPT_FILENAME, ReportCeBaseForm.RptFormat.TEXT_TAB, _
            ''                        ReportCeBaseForm.RptAction.SCHEDULE_VIEW, repParams)
            ''Dim params As New ReportCeBaseForm.Params(RPT_FILENAME, ReportCeBaseForm.RptFormat.CRYSTAL, _
            ''                           ReportCeBaseForm.RptAction.SCHEDULE, repParams)
            ''Dim params As New ReportCeBaseForm.Params(RPT_FILENAME, ReportCeBaseForm.RptFormat.CRYSTAL, _
            ''                             ReportCeBaseForm.RptAction.VIEW, repParams)
            'Return params
        End Function

#End Region

        Protected Sub WindowScript()
            Dim sJavaScript As String

            Response.ContentType = "application/octet"
            Response.Redirect("C:\temp\sal.txt")
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            '  sJavaScript &= "windowOpen('" & url & "','" & name & "');" & Environment.NewLine
            sJavaScript &= "document.execCommand('SaveAs', 'True', 'hola');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("WindowScript", sJavaScript)
        End Sub

        'Private Sub isReportCeVisible_ServerChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles isReportCeVisible.ServerChange

        'End Sub
    End Class

End Namespace