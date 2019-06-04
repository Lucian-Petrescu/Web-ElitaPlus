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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Sub AddError(ByVal err As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddError
        Dim TranslatedError As String
        Dim oPage As ElitaPlusPage
        Dim oTransProcObj As TranslationProcess
        Dim nPrevCount As Integer

        If Translate Then
            oPage = CType(Me.Page, ElitaPlusPage)
            oTransProcObj = oPage.GetTranslationProcessReference()
            nPrevCount = oPage.MissingTranslationsCount
            TranslatedError = TranslationBase.TranslateLabelOrMessage(err)
            If oPage.MissingTranslationsCount > nPrevCount Then
                TranslatedError = err & ": is not translated"
            End If
        Else
            TranslatedError = err
        End If
        If Not Me.txtErrorMsg.Text.Trim.Length = 0 Then
            Me.txtErrorMsg.Text &= System.Environment.NewLine
        End If
        Me.txtErrorMsg.Text &= TranslatedError
    End Sub

    Public Sub AddError(ByVal Err() As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddError
        Dim i As Integer
        For i = 0 To Err.Length - 1
            Dim TranslatedError As String
            If Translate Then
                TranslatedError = TranslationBase.TranslateLabelOrMessage(Err(i))
            Else
                TranslatedError = Err(i)
            End If
            If Not Me.txtErrorMsg.Text.Trim.Length = 0 Then
                Me.txtErrorMsg.Text &= System.Environment.NewLine
            End If
            Me.txtErrorMsg.Text &= TranslatedError
        Next
    End Sub

    Public Sub AddErrorAndShow(ByVal Err As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddErrorAndShow
        Me.AddError(Err, Translate)
        Me.Show()
    End Sub

    Public Sub AddErrorAndShow(ByVal Err() As String, Optional ByVal Translate As Boolean = True) Implements IErrorController.AddErrorAndShow
        Me.AddError(Err, Translate)
        Me.Show()
    End Sub

    Public Sub Clear() Implements IErrorController.Clear
        Me.txtErrorMsg.Text = ""
    End Sub

    Public Sub Show() Implements IErrorController.Show
        If Me.txtErrorMsg.Text Is Nothing OrElse Me.txtErrorMsg.Text.Trim = "" Then
            Return
        End If
        ControlMgr.SetVisibleControl(Page, Me, True)
        ControlMgr.SetVisibleControl(Page, txtErrorMsg, True)
        ControlMgr.SetVisibleControl(Page, footer, True)
    End Sub

    Public Sub Hide() Implements IErrorController.Hide
        If Me.txtErrorMsg.Text Is Nothing OrElse Me.txtErrorMsg.Text.Trim = "" Then
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

    Private Sub btnShowHide_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnShowHide.Click
        If Me.txtErrorMsg.Visible Then
            Hide()
        Else
            Show()
        End If
    End Sub

    Public Property Text() As String Implements IErrorController.Text
        Get
            Return Me.txtErrorMsg.Text
        End Get
        Set(ByVal Value As String)
            Me.txtErrorMsg.Text = Value
        End Set
    End Property
End Class
