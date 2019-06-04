Public Class ReplacementOptions
    Inherits System.Web.UI.UserControl

#Region "properties"
    Public ClaimBO As ClaimBase
    Public thisPage As ElitaPlusPage
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Not Me.ClaimBO Is Nothing Then
                Me.GVReplacementOptions.DataSource = Me.ClaimBO.ReplacementOptions()
                Me.GVReplacementOptions.DataBind()
            End If
            If Not Me.thisPage Is Nothing Then Me.thisPage.TranslateGridHeader(Me.GVReplacementOptions)
        End If
    End Sub

End Class