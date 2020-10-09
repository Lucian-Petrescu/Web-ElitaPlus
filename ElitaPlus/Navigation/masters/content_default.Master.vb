Public Partial Class content_default
    Inherits MasterBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return ErrorCtrl
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

End Class