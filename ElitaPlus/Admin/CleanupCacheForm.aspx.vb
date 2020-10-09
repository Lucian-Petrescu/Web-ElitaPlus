Imports System.Web
Imports Assurant.Elita.Web.Forms

Partial Public Class CleanupCacheForm
    Inherits ElitaPlusSearchPage

    Public Const CACHE As String = "CLEANUPCACHEFORM"
    Public Const PAGETAB As String = "ADMIN"
    Public Const ATTENTION_NOTE As String = "ATTENTION_NOTE"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        UpdateBreadCrum()
        MasterPage.MessageController.AddWarning(ATTENTION_NOTE, True)
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CACHE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(CACHE)
    End Sub

    Protected Sub btncache_click(sender As Object, e As System.EventArgs) Handles btncache.Click
        MasterPage.MessageController.Clear()
        Assurant.Elita.Caching.Cache.CleanCache()
        MasterPage.MessageController.AddInformation("Cache Cleaned Successfully", True)
        ControlMgr.SetEnableControl(Me, btncache, False)
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub


End Class