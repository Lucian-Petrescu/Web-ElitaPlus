Public Class DynamicFulfillmentUI
    Inherits System.Web.UI.UserControl
    Public SourceSystem As String
    Public ApiKey As String
    Public ClaimNumber As String
    Public SubscriptionKey As String
    Public BaseAddresss As String
    Public CssUri As String
    Public ScriptUri As String
    Public AccessToken As String
    Public IsLoadError As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class