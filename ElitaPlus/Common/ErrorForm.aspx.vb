Imports System.Diagnostics

Partial Class ErrorForm
    Inherits Page
    'Inherits ElitaPlusPage 'System.Web.UI.Page
    Protected WithEvents TextBoxNewProgCode As TextBox
    Protected WithEvents bntAdd As Button
    Protected WithEvents Button1 As Button
    Protected WithEvents Button2 As Button
    Protected WithEvents btnSave As Button
    Protected WithEvents btnDelete As Button
    Protected WithEvents btnCancel As Button
    Protected WithEvents Label2 As Label
    Protected WithEvents DataGridDropdowns As DataGrid
    Protected WithEvents Label7 As Label
    Protected WithEvents Label4 As Label
    Protected WithEvents WorkingPanel As Panel
    Protected WithEvents Label5 As Label
    Protected WithEvents Label6 As Label
    Protected WithEvents ErrorControl As ErrorController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Constants "
    Protected Const CONFIRM_MSG As String = "Are you sure you want to delete the selected dropdowns?"
    Private Const NEW_PROGCODE_CIDX As Integer = 1
    Private Const NEW_TRANS_VALUE_CIDX As Integer = 2
    Private Const NEW_MAINT_BY_USER_CIDX As Integer = 3

    Private Const OLD_PROGCODE_CIDX As Integer = 6
    Private Const OLD_TRANS_VALUE_CIDX As Integer = 8
    Private Const OLD_MAINT_BY_USER_CIDX As Integer = 7

    Private Const DROPDOWN_ID_CIDX As Integer = 5
    Private Const SELECTED_CIDX As Integer = 0
    Private Const ITEMS_CIDX As Integer = 4

    Public Shared PAGE_NAME As String = "~/Common/ErrorForm.aspx"
    Public Shared MESSAGE_KEY_NAME As String = "MESSAGE"


#End Region

#Region "Page State"
#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim sMessage As String
        If Not IsPostBack Then
            Try
                '   Me.MenuEnabled = True
                sMessage = CType(Session(MESSAGE_KEY_NAME), String)
                If sMessage Is Nothing Then
                    sMessage = Request.QueryString("Message")
                Else
                    'Clean the session
                    Session.Remove(MESSAGE_KEY_NAME)
                End If
                If sMessage IsNot Nothing Then
                    txtError.Text = sMessage
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Sub LoadData()
    End Sub

End Class




