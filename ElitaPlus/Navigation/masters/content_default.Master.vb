Public Partial Class content_default
    Inherits MasterBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return Me.ErrorCtrl
        End Get
    End Property

    Public Overrides Property PageTitle() As String
        Get
            Return Me.TITLELABEL.Text
        End Get
        Set(ByVal value As String)
            Me.TITLELABEL.Text = value
        End Set
    End Property

    Public Overrides Property PageTab() As String
        Get
            Return Me.TABLABEL.Text
        End Get
        Set(ByVal value As String)
            Me.TABLABEL.Text = value
        End Set
    End Property

    Public Overrides ReadOnly Property PageForm(ByVal FormName As String) As System.Web.UI.HtmlControls.HtmlForm
        Get
            If Not Me.FindControl(FormName) Is Nothing Then
                Return CType(Me.FindControl(FormName), HtmlForm)
            Else
                Return Nothing
            End If
        End Get
    End Property

End Class