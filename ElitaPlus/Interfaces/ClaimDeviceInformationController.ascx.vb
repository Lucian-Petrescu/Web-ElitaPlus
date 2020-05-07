Public Class ClaimDeviceInformationController
    Inherits System.Web.UI.UserControl

#Region "properties"
    Public ClaimBO As ClaimBase
    Public thisPage As ElitaPlusPage

    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.ThePage.TranslateGridHeader(GvClaimDeviceInformation)
            If Not Me.ClaimBO Is Nothing Then
                Me.GvClaimDeviceInformation.DataSource = Me.ClaimBO.ClaimedEnrolledEquipments()
                Me.GvClaimDeviceInformation.DataBind()
            End If
        End If
    End Sub

End Class