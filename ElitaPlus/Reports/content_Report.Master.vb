Public Partial Class content_Report
    Inherits MasterBase

    'Protected WithEvents ReportCeInputControl As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeInputControl
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RegisterClientServerIds()

    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return ErrorCtrl
        End Get
    End Property
    Public Overrides ReadOnly Property ReportCeInputControl() As Reports.ReportCeInputControl
        Get
            Return moReportCeInputControl
        End Get
    End Property

    Public Overrides Property PageTitle() As String
        Get
            Return TITLELABEL.Text
        End Get
        Set(value As String)
            TITLELABEL.Text = value
        End Set
    End Property

    Public Overrides Property PageTab() As String
        Get
            Return TABLABEL.Text
        End Get
        Set(value As String)
            TABLABEL.Text = value
        End Set
    End Property

    Public Overrides ReadOnly Property PageForm(FormName As String) As System.Web.UI.HtmlControls.HtmlForm
        Get
            If FindControl(FormName) IsNot Nothing Then
                Return CType(FindControl(FormName), HtmlForm)
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public Event SelectedViewOption(sender As Object, e As System.EventArgs)

    Protected Sub OnFromDrop_Changed(sender As Object, e As System.EventArgs) _
   Handles moReportCeInputControl.SelectedViewPDFOption
        RaiseEvent SelectedViewOption(sender, e)
    End Sub
 
    'Public Overrides ReadOnly Property ErrController() As ErrorController
    '    Get
    '        Return CType(Me.Master, MasterBase).ErrController
    '    End Get
    'End Property

    'Public Overrides ReadOnly Property ReportCeInputControl() As Reports.ReportCeInputControl
    '    Get
    '        Return Me.moReportCeInputControl
    '    End Get
    'End Property

    'Public Overrides Property PageTitle() As String
    '    Get
    '        Return CType(Me.Master, MasterBase).PageTitle
    '    End Get
    '    Set(ByVal value As String)
    '        CType(Me.Master, MasterBase).PageTitle = value
    '    End Set
    'End Property  

    'Public Overrides Property PageTab() As String
    '    Get
    '        Return CType(Me.Master, MasterBase).PageTab
    '    End Get
    '    Set(ByVal value As String)
    '        CType(Me.Master, MasterBase).PageTab = value
    '    End Set
    'End Property

    'Public Overrides ReadOnly Property PageForm(ByVal FormName As String) As System.Web.UI.HtmlControls.HtmlForm
    '    Get
    '        Return CType(Me.Master, MasterBase).PageForm(FormName)
    '    End Get
    'End Property

    Public Sub RegisterClientServerIds()

        Dim onloadScript As New System.Text.StringBuilder()
        onloadScript.Append("<script type='text/javascript'>")
        onloadScript.Append(Environment.NewLine)
        onloadScript.Append("var PDFControl = '" + ReportCeInputControl.RadioButtonPDFControl.ClientID + "';")
        onloadScript.Append("var TXTControl = '" + ReportCeInputControl.RadioButtonTXTControl.ClientID + "';")
        onloadScript.Append("var VIEWControl = '" + ReportCeInputControl.RadioButtonVIEWControl.ClientID + "';")
        onloadScript.Append("var StatusControl = '" + ReportCeInputControl.ReportControlStatus.ClientID + "';")
        onloadScript.Append("var ControlErrorHidden = '" + ReportCeInputControl.ReportControlErrorHidden.ClientID + "';")
        onloadScript.Append("var ViewerControl = '" + ReportCeInputControl.ReportControlViewer.ClientID + "';")
        onloadScript.Append("var ProgressVisibleControl  = '" + ReportCeInputControl.ProgressControlVisible.ClientID + "';")
        onloadScript.Append("var CloseTimerControl = '" + ReportCeInputControl.ReportCloseTimer.ClientID + "';")
        onloadScript.Append("var ControlViewHidden = '" + ReportCeInputControl.ReportControlViewHidden.ClientID + "';")
        onloadScript.Append("var ControlErrMsg = '" + ReportCeInputControl.ReportControlErrMsg.ClientID + "';")
        onloadScript.Append("var ActionControl = '" + ReportCeInputControl.ReportControlAction.ClientID + "';")
        onloadScript.Append("var FtpControl = '" + ReportCeInputControl.ReportControlFtp.ClientID + "';")


        onloadScript.Append(Environment.NewLine)
        onloadScript.Append("</script>")
        ' Register script with page 

        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())
        Page.ClientScript.RegisterClientScriptBlock([GetType](), "onLoadCall", onloadScript.ToString())

        Session("ReportErrorButton") = ReportCeInputControl.ReportControlErrorHidden.ClientID
        Session("StatusControl") = ReportCeInputControl.ReportControlStatus.ClientID
        Session("ControlErrMsg") = ReportCeInputControl.ReportControlErrMsg.ClientID

    End Sub
End Class