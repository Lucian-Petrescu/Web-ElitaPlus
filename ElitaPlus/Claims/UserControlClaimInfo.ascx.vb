Public Class UserControlClaimInfo
    Inherits UserControl


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
        Set(value As String)
        End Set
    End Property

#End Region

    ' This is the initialization Method
    Public Sub InitController(oClaim As ClaimBase)
        PopulateFormFromClaimCtrl(oClaim)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub PopulateFormFromClaimCtrl(oClaim As ClaimBase)
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        CertificateNumberTD.InnerText = oClaim.CertificateNumber
        CLAIMStatusTD.InnerText = LookupListNew.GetClaimStatusFromCode(langId, oClaim.StatusCode)

        If (oClaim.Status = BasicClaimStatus.Active) Then
            cssClassName = "StatActive"
        Else
            cssClassName = "StatClosed"
        End If

        CLAIMStatusTD.Attributes.Item("Class") = CLAIMStatusTD.Attributes.Item("Class") & " " & cssClassName

        Dim oClaimbase As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(oClaim.Id)
        Dim oCertificate As Certificate = New Certificate(oClaim.Certificate.Id)

        If Not (oCertificate.SubscriberStatus.Equals(Guid.Empty)) Then
            SubscriberStatus.InnerText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SUBSCRIBER_STATUS, oCertificate.SubscriberStatus)
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_SUBSCRIBER_STATUS, oCertificate.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            SubscriberStatus.Attributes.Item("Class") = SubscriberStatus.Attributes.Item("Class") & " " & cssClassName
            SubscriberStatusLabelTD.Visible = True
        Else
            ControlMgr.SetVisibleControl(Page, SubscriberStatusLabel, False)
            ControlMgr.SetVisibleControl(Page, SubscriberStatus, False)
            SubscriberStatusLabelTD.Visible = False
        End If

        If oClaim.ClaimAuthorizationType = ClaimAuthorizationType.Single Then
            ServiceCenterLabelTD.Visible = True
            ServiceCenterTD.Visible = True
            ServiceCenterTD.InnerText = CType(oClaim, Claim).ServiceCenter
        Else
            ServiceCenterLabelTD.Visible = False
            ServiceCenterTD.Visible = False
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

        ScrutinyRequiredLabelTD.InnerText = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_YESNO_XCD, scrutinyRequiredCode, langId)
        cssClassName = If(scrutinyRequired, "StatClosed", "StatActive")
        ScrutinyRequiredLabelTD.Attributes.Item("Class") = cssClassName
    End Sub

End Class