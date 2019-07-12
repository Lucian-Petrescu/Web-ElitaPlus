Imports System.Web
Imports Assurant.Elita.Web.Forms

Partial Public Class CleanupCacheForm
    Inherits ElitaPlusSearchPage

    Public Const CACHE As String = "CLEANUPCACHEFORM"
    Public Const PAGETAB As String = "ADMIN"
    Public Const ATTENTION_NOTE As String = "ATTENTION_NOTE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        Me.UpdateBreadCrum()
        MasterPage.MessageController.AddWarning(ATTENTION_NOTE, True)
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(CACHE)
    End Sub

    Protected Sub btncache_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncache.Click
        Me.MasterPage.MessageController.Clear()
        Assurant.Elita.Caching.Cache.CleanCache()
        MasterPage.MessageController.AddInformation("Cache Cleaned Successfully", True)
        ControlMgr.SetEnableControl(Me, btncache, False)
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub


End Class