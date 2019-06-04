

Partial Class MessagePage
    Inherits System.Web.UI.Page



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ERROR_CONTENT As String = "ERROR_CONTENT"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then

            Dim sMessage As String

            Dim sErrorMessage As String
            sErrorMessage = HttpContext.Current.Session(ERROR_CONTENT).ToString()

            sMessage = Request.QueryString("message")

            If sMessage <> String.Empty Then
                ELPWebConstants.ShowPopup(sMessage.ToString, Me.Page)
                lblMessage.Text = sMessage
            End If



            If sErrorMessage <> String.Empty Then
                lblMessage.Text = sErrorMessage
            End If
        End If

    End Sub

End Class
