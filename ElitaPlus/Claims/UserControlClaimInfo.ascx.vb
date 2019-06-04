Public Class UserControlClaimInfo
    Inherits System.Web.UI.UserControl


#Region "Properties"

    Public ReadOnly Property CertificateNumber() As String
        Get
            Return CertificateNumberTD.InnerText
        End Get
    End Property

    Public ReadOnly Property ServiceCenter() As String
        Get
            Return ServiceCenterTD.InnerText
        End Get
    End Property

    Public ReadOnly Property CLAIMStatus() As String
        Get
            Return CLAIMStatusTD.InnerText
        End Get
    End Property

    Public Property CLAIMStatusCss() As String
        Get
        End Get
        Set(ByVal value As String)
        End Set
    End Property

#End Region

    ' This is the initialization Method
    Public Sub InitController(ByVal oClaim As ClaimBase)
        PopulateFormFromClaimCtrl(oClaim)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub PopulateFormFromClaimCtrl(ByVal oClaim As ClaimBase)
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Me.CertificateNumberTD.InnerText = oClaim.CertificateNumber
        Me.CLAIMStatusTD.InnerText = LookupListNew.GetClaimStatusFromCode(langId, oClaim.StatusCode)

        If (oClaim.Status = BasicClaimStatus.Active) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If

        Me.CLAIMStatusTD.Attributes.Item("Class") = Me.CLAIMStatusTD.Attributes.Item("Class") & " " & cssClassName

        Dim oClaimbase As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(oClaim.Id)
        Dim oCertificate As Certificate = New Certificate(oClaim.Certificate.Id)

        If Not (oCertificate.SubscriberStatus.Equals(Guid.Empty)) Then
            Me.SubscriberStatus.InnerText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SUBSCRIBER_STATUS, oCertificate.SubscriberStatus)
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_SUBSCRIBER_STATUS, oCertificate.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            Me.SubscriberStatus.Attributes.Item("Class") = Me.SubscriberStatus.Attributes.Item("Class") & " " & cssClassName
            SubscriberStatusLabelTD.Visible = True
        Else
            ControlMgr.SetVisibleControl(Page, Me.SubscriberStatusLabel, False)
            ControlMgr.SetVisibleControl(Page, Me.SubscriberStatus, False)
            SubscriberStatusLabelTD.Visible = False
        End If

        If oClaim.ClaimAuthorizationType = ClaimAuthorizationType.Single Then
            Me.ServiceCenterLabelTD.Visible = True
            Me.ServiceCenterTD.Visible = True
            Me.ServiceCenterTD.InnerText = CType(oClaim, Claim).ServiceCenter
        Else
            Me.ServiceCenterLabelTD.Visible = False
            Me.ServiceCenterTD.Visible = False
        End If

        Dim ClaimExtensionsDV As Claim.ClaimExtensionsDV = Claim.GetFraudulentClaimExtensions(oClaim.Id)
        Dim scrutinyRequiredCode As String = String.Empty
        Dim scrutinyRequired As Boolean = False

        If (ClaimExtensionsDV.Count > 0) Then
            ClaimExtensionsDV.Sort = "FIELD_NAME"
            Dim scrutinyRequiredMatches As DataRowView() = ClaimExtensionsDV.FindRows("SCRUTINY_REQUIRED")
            If scrutinyRequiredMatches.Count > 0 Then
                scrutinyRequired = scrutinyRequiredMatches.Any(Function(m) m.Row.Item(1).ToString = Codes.EXT_YESNO_Y)
            End If
        End If
        scrutinyRequiredCode = If(scrutinyRequired, Codes.EXT_YESNO_Y, Codes.EXT_YESNO_N)

        Me.ScrutinyRequiredLabelTD.InnerText = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_YESNO_XCD, scrutinyRequiredCode, langId)
        cssClassName = If(scrutinyRequired, "StatClosed", "StatActive")
        Me.ScrutinyRequiredLabelTD.Attributes.Item("Class") = cssClassName
    End Sub

End Class