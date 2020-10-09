Public Partial Class content_search
    Inherits masterbase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return CType(Master, MasterBase).ErrController
        End Get
    End Property

    Public Overrides Property PageTitle() As String
        Get
            Return CType(Master, MasterBase).PageTitle
        End Get
        Set(value As String)
            CType(Master, MasterBase).PageTitle = value
        End Set
    End Property

    Public Overrides Property PageTab() As String
        Get
            Return CType(Master, MasterBase).PageTab
        End Get
        Set(value As String)
            CType(Master, MasterBase).PageTab = value
        End Set
    End Property

    Public Overrides ReadOnly Property PageForm(FormName As String) As System.Web.UI.HtmlControls.HtmlForm
        Get
            Return CType(Master, MasterBase).PageForm(FormName)
        End Get
    End Property
End Class