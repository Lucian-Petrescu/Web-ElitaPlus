Partial Class ReportContainerForm
    Inherits System.Web.UI.Page
    'Inherits ElitaPlusPage

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        rptContainer.Attributes.Add("src", Request.QueryString("url"))
        hiddenTitle.Value = Request.QueryString("title")
        'SetReportTitle(Request.QueryString("title"))
    End Sub


    'Private Sub SetReportTitle(ByVal title As String)
    '    Dim sJavaScript As String
    '    sJavaScript = "<SCRIPT>" & Environment.NewLine
    '    sJavaScript &= "document.title = '" & title & "';" & Environment.NewLine
    '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
    '    Me.RegisterStartupScript("SetReportTitle", sJavaScript)
    'End Sub

End Class
