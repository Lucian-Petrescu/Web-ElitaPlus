Partial Class AccountingSettingPopup
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "Constants"
    Public Shared URL As String = "AccountingSettingListForm.aspx"
#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub btnDealer_Click(sender As System.Object, e As System.EventArgs) Handles btnDealer.Click
        OpenWindow(AccountingSettingForm.URL & "?Type=Dealer")
        'Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.Dealer, False))
        'Dim sJavaScript As String
        'sJavaScript = "<SCRIPT>" & Environment.NewLine
        'sJavaScript &= "window.close();" & Environment.NewLine
        'sJavaScript &= "</SCRIPT>" & Environment.NewLine
        'Me.RegisterStartupScript("WindowClose", sJavaScript)

    End Sub

    Private Sub btnServiceCenter_Click(sender As System.Object, e As System.EventArgs) Handles btnServiceCenter.Click
        'Dim sJavaScript As String
        'sJavaScript = "<SCRIPT>" & Environment.NewLine
        'sJavaScript &= "window.close();" & Environment.NewLine
        'sJavaScript &= "</SCRIPT>" & Environment.NewLine
        'Me.RegisterStartupScript("WindowClose", sJavaScript)
        OpenWindow(AccountingSettingForm.URL & "?Type=ServiceCenter")
        'Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AccountingSettingForm.ReturnType.TargetType.ServiceCenter, False))
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "window.open('" & URL & "','Navigation_Content');" & Environment.NewLine
        sJavaScript &= "window.close();</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("WindowOpen", sJavaScript)
    End Sub

    Protected Sub OpenWindow(url As String)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "window.open('" & url & "','Navigation_Content');" & Environment.NewLine
        sJavaScript &= "window.close();</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("WindowOpen", sJavaScript)
    End Sub

   
End Class
