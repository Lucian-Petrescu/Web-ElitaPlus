Public MustInherit Class MasterBase
    Inherits System.Web.UI.MasterPage

    Private Const STYLESHEET As String = "/Styles.css"
    Private Const TILED_STYLESHEET As String = "/tiledCss.css"

    Private Const SCRIPTS As String = "/Navigation/Scripts/GlobalHeader.js"
    Private Const REPORT_SCRIPTS As String = "/Navigation/Scripts/ReportCeMasterScripts.js"

    Private Const SCRIPT_BLOCK As String = "SCRIPT_BLOCK"
    Private Const STYLE_BLOCK As String = "STYLE_BLOCK"
    Private Const TILED_STYLE_BLOCK As String = "TILED_STYLE_BLOCK"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Include the stylesheet and scripts in the master page
        Dim cs As ClientScriptManager = Page.ClientScript

        If (Not ([GetType]().BaseType.Equals(GetType(ElitaBase)))) Then
            If Not cs.IsClientScriptBlockRegistered(STYLE_BLOCK) Then
                cs.RegisterClientScriptBlock([GetType], STYLE_BLOCK, "<STYLE TYPE=""text/css"">@import url(""" & Request.ApplicationPath & STYLESHEET & """);</STYLE>")
            End If
        End If

        If Not cs.IsClientScriptBlockRegistered(TILED_STYLE_BLOCK) Then
            cs.RegisterClientScriptBlock([GetType], TILED_STYLE_BLOCK, "<STYLE TYPE=""text/css"">@import url(""" & Request.ApplicationPath & TILED_STYLESHEET & """);</STYLE>")
        End If

        If Not cs.IsClientScriptIncludeRegistered(SCRIPT_BLOCK) Then
            cs.RegisterClientScriptInclude(SCRIPT_BLOCK, Request.ApplicationPath + SCRIPTS)
            cs.RegisterClientScriptInclude(SCRIPT_BLOCK, Request.ApplicationPath + REPORT_SCRIPTS)
        End If


    End Sub

    'Must Override properties for the base pages to access the controls
    Public MustOverride ReadOnly Property ErrController() As ErrorController
    Public MustOverride ReadOnly Property PageForm(FormName As String) As HtmlForm
    Public MustOverride Property PageTitle() As String
    Public MustOverride Property PageTab() As String

    Public Overridable ReadOnly Property ReportCeInputControl() As Reports.ReportCeInputControl
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable ReadOnly Property ReportExtractInputControl() As Reports.ReportExtractInputControl
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable Property BreadCrum() As String
        Get
            Return Nothing
        End Get
        Set(value As String)

        End Set
    End Property

    Public Overridable Property UsePageTabTitleInBreadCrum() As Boolean
        Get
            Return Nothing
        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Overridable Property DisplayRequiredFieldNote() As Boolean
        Get
            Return Nothing
        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Overridable ReadOnly Property MessageController As IMessageController
        Get
            Throw New NotImplementedException
        End Get
    End Property
End Class
