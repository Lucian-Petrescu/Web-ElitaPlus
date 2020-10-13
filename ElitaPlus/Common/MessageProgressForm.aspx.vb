Imports System.Diagnostics

Partial Class MessageProgressForm
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Response.Cache.SetExpires(Convert.ToDateTime("1/1/2050"))

        If Request.QueryString("Message") Is Nothing Then

            lblMessage.Text = "Performing Action..."

        Else

            lblMessage.Text = Request.QueryString("Message")

        End If
    End Sub

End Class
