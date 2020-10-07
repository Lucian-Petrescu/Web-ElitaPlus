Partial  Class ErrorController
    Inherits System.Web.UI.UserControl
    Implements IErrorController
    Protected WithEvents BtnClose As System.Web.UI.WebControls.Button

#Region "Properties"

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Sub AddError(err As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddError
        Dim TranslatedError As String
        Dim oPage As ElitaPlusPage
        Dim oTransProcObj As TranslationProcess
        Dim nPrevCount As Integer

        If Translate Then
            oPage = CType(Page, ElitaPlusPage)
            oTransProcObj = oPage.GetTranslationProcessReference()
            nPrevCount = oPage.MissingTranslationsCount
            TranslatedError = TranslationBase.TranslateLabelOrMessage(err)
            If oPage.MissingTranslationsCount > nPrevCount Then
                TranslatedError = err & ": is not translated"
            End If
        Else
            TranslatedError = err
        End If
        If Not txtErrorMsg.Text.Trim.Length = 0 Then
            txtErrorMsg.Text &= System.Environment.NewLine
        End If
        txtErrorMsg.Text &= TranslatedError
    End Sub

    Public Sub AddError(Err() As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddError
        Dim i As Integer
        For i = 0 To Err.Length - 1
            Dim TranslatedError As String
            If Translate Then
                TranslatedError = TranslationBase.TranslateLabelOrMessage(Err(i))
            Else
                TranslatedError = Err(i)
            End If
            If Not txtErrorMsg.Text.Trim.Length = 0 Then
                txtErrorMsg.Text &= System.Environment.NewLine
            End If
            txtErrorMsg.Text &= TranslatedError
        Next
    End Sub

    Public Sub AddErrorAndShow(Err As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddErrorAndShow
        AddError(Err, Translate)
        Show()
    End Sub

    Public Sub AddErrorAndShow(Err() As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddErrorAndShow
        AddError(Err, Translate)
        Show()
    End Sub

    Public Sub Clear() Implements IErrorController.Clear
        txtErrorMsg.Text = ""
    End Sub

    Public Sub Show() Implements IErrorController.Show
        If txtErrorMsg.Text Is Nothing OrElse txtErrorMsg.Text.Trim = "" Then
            Return
        End If
        ControlMgr.SetVisibleControl(Page, Me, True)
        ControlMgr.SetVisibleControl(Page, txtErrorMsg, True)
        ControlMgr.SetVisibleControl(Page, footer, True)
    End Sub

    Public Sub Hide() Implements IErrorController.Hide
        If txtErrorMsg.Text Is Nothing OrElse txtErrorMsg.Text.Trim = "" Then
            ControlMgr.SetVisibleControl(Page, Me, False)
        Else
            ControlMgr.SetVisibleControl(Page, Me, True)
            ControlMgr.SetVisibleControl(Page, txtErrorMsg, False)
            ControlMgr.SetVisibleControl(Page, footer, False)
        End If

    End Sub

    Public Sub Clear_Hide() Implements IErrorController.Clear_Hide
        Clear()
        Hide()
    End Sub

    Private Sub btnShowHide_Click(sender As System.Object, e As System.Web.UI.ImageClickEventArgs) Handles btnShowHide.Click
        If txtErrorMsg.Visible Then
            Hide()
        Else
            Show()
        End If
    End Sub

    Public Property Text() As String Implements IErrorController.Text
        Get
            Return txtErrorMsg.Text
        End Get
        Set(Value As String)
            txtErrorMsg.Text = Value
        End Set
    End Property
End Class
