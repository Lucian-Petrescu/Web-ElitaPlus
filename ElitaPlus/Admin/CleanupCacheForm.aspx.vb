Imports System.Web
Imports Assurant.Elita.Web.Forms

Partial Public Class CleanupCacheForm
    Inherits ElitaPlusSearchPage

    Public Const CACHE As String = "CLEANUPCACHEFORM"
    Public Const PAGETAB As String = "ADMIN"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        Me.UpdateBreadCrum()
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(CACHE)
    End Sub

    Protected Sub cachebtn_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cachebtn.Click
        Assurant.Elita.Caching.Cache.CleanCache()
        Me.lblnotify.Text = "Cache Cleaned Successfully."
        Me.cachebtn.Enabled = False
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub


End Class