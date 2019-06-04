
Namespace Interfaces

    Partial Class InterfaceProgressForm
        Inherits System.Web.UI.Page

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
            If Not Page.IsPostBack Then
                If Request.QueryString("Message") Is Nothing Then
                    lblMessage.Text = "Performing Action..."
                Else
                    lblMessage.Text = Request.QueryString("Message")
                End If
                StartProgressBar()
            End If

        End Sub


        Protected Sub StartProgressBar()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "LoadWaitMsg();" & Environment.NewLine
            sJavaScript &= "ShowInterfaceContainer();" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("StartProgressBar", sJavaScript)
        End Sub


    End Class

End Namespace
