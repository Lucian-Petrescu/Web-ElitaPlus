'Imports System.Linq

Namespace Certificates


    Partial Class UserControlCertificateInfo_New
        Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "





        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const DATE_FORMAT As String = "dd-MMM-yyyy"
#End Region

#Region "Properties"

        Private Shadows ReadOnly Property Page() As ElitaPlusPage
            Get
                Return CType(MyBase.Page, ElitaPlusPage)
            End Get
        End Property

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
        End Sub

        ' This is the initialization Method
        Public Sub InitController(ByVal certificateId As Guid, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
            Dim oCertificateCtrl As New Certificate(certificateId)
            InitController(oCertificateCtrl, riskTypeDescription, companyCode)

        End Sub

        ' This is the initialization Method
        Public Sub InitController(ByVal oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)

            EnableControls()
            PopulateFormFromCertificateCtrl(oCertificateCtrl, riskTypeDescription, companyCode)
        End Sub

#Region "Control Management"

        Public Sub EnableControls()
            Page.SetEnabledForControlFamily(Me, False)
        End Sub

        Public Sub PopulateFormFromCertificateCtrl(ByVal oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
            Dim CertExtensionsDV As Certificate.CertExtensionsDV = Certificate.GetFraudulentCertExtensions(oCertificateCtrl.Id)
            Dim scrutinyRequiredCode As String = String.Empty
            Dim scrutinyRequired As Boolean = False

            If (CertExtensionsDV.Count > 0) Then
                CertExtensionsDV.Sort = "FIELD_NAME"
                Dim scrutinyRequiredMatches As DataRowView() = CertExtensionsDV.FindRows("SCRUTINY_REQUIRED")
                If scrutinyRequiredMatches.Count > 0 Then
                    scrutinyRequired = scrutinyRequiredMatches.Any(Function(m) m.Row.Item(1).ToString = Codes.EXT_YESNO_Y)
                End If
            End If
            scrutinyRequiredCode = If(scrutinyRequired, Codes.EXT_YESNO_Y, Codes.EXT_YESNO_N)

            Dim cssClassName As String
            With oCertificateCtrl
                Me.ProductRemainLiabilityLimitTD.InnerText = .ProductRemainLiabilityLimit
                Me.ProductTotalPaidAmountTD.InnerText = .ProductTotalPaidAmount
                Me.CustomerNameTD.InnerText = getSalutation(oCertificateCtrl) & .CustomerName
                Me.WarrantySalesDateTD.InnerText = ElitaPlusPage.GetDateFormattedStringNullable(.WarrantySalesDate.Value)

                Me.ScrutinyRequiredLabelTD.InnerText = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_YESNO_XCD, scrutinyRequiredCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                cssClassName = If(scrutinyRequired, "StatClosed", "StatActive")
                Me.ScrutinyRequiredLabelTD.Attributes.Item("Class") = cssClassName

                Me.CompanyCodeTD.InnerText = companyCode
                Me.StatusTD.InnerText = LookupListNew.GetDescriptionFromCode("CSTAT", .StatusCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If

                Me.StatusTD.Attributes.Item("Class") = cssClassName

                If Not (.CustReqCancelDate Is Nothing) Then
                    Me.CustCancelDateTD.InnerText = "(Cancellation Request Date:" + ElitaPlusPage.GetDateFormattedStringNullable(.CustReqCancelDate.Value) + ")"
                    Me.CustCancelDateTD.Attributes.Item("Class") = Me.CustCancelDateTD.Attributes.Item("Class") & " " & "Orange"
                End If

                Me.CertificateNumberTD.InnerText = .CertNumber

                If Not (.SubscriberStatus.Equals(Guid.Empty)) Then
                    Me.SubscriberStatusTD.InnerText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SUBSCRIBER_STATUS, .SubscriberStatus)
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_SUBSCRIBER_STATUS, .SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                        cssClassName = "StatActive"
                    Else
                        cssClassName = "StatClosed"
                    End If
                    Me.SubscriberStatusTD.Attributes.Item("Class") = Me.SubscriberStatusTD.Attributes.Item("Class") & " " & cssClassName
                Else
                    ControlMgr.SetVisibleControl(Page, Me.SubscriberStatusLabel, False)
                    ControlMgr.SetVisibleControl(Page, Me.SubscriberStatusTD, False)
                    ControlMgr.SetVisibleControl(Page, Me.SubscriberStatusLabelTD, False)
                    Me.CustomerNameTD.Attributes.Item("Class") = Me.CustomerNameTD.Attributes.Item("Class").Replace("bor", "")
                End If

                If Not (.DealerId.Equals(Guid.Empty)) Then
                    Me.DealerNameTD.InnerText = .getDealerDescription
                    ElitaPlusPage.Trace(Me.Page, "Dealer =" & Me.DealerNameTD.InnerText & "@ Cert=" & Me.CertificateNumberTD.InnerText)
                    Me.DealerGroupTD.InnerText = .getDealerGroupName
                End If

                ControlMgr.SetVisibleControl(Page, Me.moRestrictedStatus, .CertificateIsRestricted)


            End With

        End Sub


        Private Function getSalutation(ByVal oCertificateCtrl As Certificate) As String
            Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(oCertificateCtrl.CompanyId)

            If LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), companyBO.SalutationId) = "Y" Then
                Dim oSalutation As String = oCertificateCtrl.getSalutationDescription & " "
                Return oSalutation.TrimStart
            End If

            Return Nothing

        End Function

#End Region

    End Class

End Namespace