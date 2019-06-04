Public Partial Class content_search
    Inherits masterbase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return CType(Me.Master, MasterBase).ErrController
        End Get
    End Property

    Public Overrides Property PageTitle() As String
        Get
            Return CType(Me.Master, MasterBase).PageTitle
        End Get
        Set(ByVal value As String)
            CType(Me.Master, MasterBase).PageTitle = value
        End Set
    End Property

    Public Overrides Property PageTab() As String
        Get
            Return CType(Me.Master, MasterBase).PageTab
        End Get
        Set(ByVal value As String)
            CType(Me.Master, MasterBase).PageTab = value
        End Set
    End Property

    Public Overrides ReadOnly Property PageForm(ByVal FormName As String) As System.Web.UI.HtmlControls.HtmlForm
        Get
            Return CType(Me.Master, MasterBase).PageForm(FormName)
        End Get
    End Property
End Class