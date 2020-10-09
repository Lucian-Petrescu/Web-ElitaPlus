Public Class ReplacementOptions
    Inherits System.Web.UI.UserControl

#Region "properties"
    Public ClaimBO As ClaimBase
    Public thisPage As ElitaPlusPage
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If ClaimBO IsNot Nothing Then
                GVReplacementOptions.DataSource = ClaimBO.ReplacementOptions()
                GVReplacementOptions.DataBind()
            End If
            If thisPage IsNot Nothing Then thisPage.TranslateGridHeader(GVReplacementOptions)
        End If
    End Sub

End Class