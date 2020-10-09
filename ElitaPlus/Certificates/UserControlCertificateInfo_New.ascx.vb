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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
        End Sub

        ' This is the initialization Method
        Public Sub InitController(certificateId As Guid, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
            Dim oCertificateCtrl As New Certificate(certificateId)
            InitController(oCertificateCtrl, riskTypeDescription, companyCode)

        End Sub

        ' This is the initialization Method
        Public Sub InitController(oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)

            EnableControls()
            PopulateFormFromCertificateCtrl(oCertificateCtrl, riskTypeDescription, companyCode)
        End Sub

#Region "Control Management"

        Public Sub EnableControls()
            Page.SetEnabledForControlFamily(Me, False)
        End Sub

        Public Sub PopulateFormFromCertificateCtrl(oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
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
                ProductRemainLiabilityLimitTD.InnerText = .ProductRemainLiabilityLimit
                ProductTotalPaidAmountTD.InnerText = .ProductTotalPaidAmount
                CustomerNameTD.InnerText = getSalutation(oCertificateCtrl) & .CustomerName
                WarrantySalesDateTD.InnerText = ElitaPlusPage.GetDateFormattedStringNullable(.WarrantySalesDate.Value)

                ScrutinyRequiredLabelTD.InnerText = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_YESNO_XCD, scrutinyRequiredCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                cssClassName = If(scrutinyRequired, "StatClosed", "StatActive")
                ScrutinyRequiredLabelTD.Attributes.Item("Class") = cssClassName

                CompanyCodeTD.InnerText = companyCode
                StatusTD.InnerText = LookupListNew.GetDescriptionFromCode("CSTAT", .StatusCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If

                StatusTD.Attributes.Item("Class") = cssClassName

                If Not (.CustReqCancelDate Is Nothing) Then
                    CustCancelDateTD.InnerText = "(Cancellation Request Date:" + ElitaPlusPage.GetDateFormattedStringNullable(.CustReqCancelDate.Value) + ")"
                    CustCancelDateTD.Attributes.Item("Class") = CustCancelDateTD.Attributes.Item("Class") & " " & "Orange"
                End If

                CertificateNumberTD.InnerText = .CertNumber

                If Not (.SubscriberStatus.Equals(Guid.Empty)) Then
                    SubscriberStatusTD.InnerText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SUBSCRIBER_STATUS, .SubscriberStatus)
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_SUBSCRIBER_STATUS, .SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                        cssClassName = "StatActive"
                    Else
                        cssClassName = "StatClosed"
                    End If
                    SubscriberStatusTD.Attributes.Item("Class") = SubscriberStatusTD.Attributes.Item("Class") & " " & cssClassName
                Else
                    ControlMgr.SetVisibleControl(Page, SubscriberStatusLabel, False)
                    ControlMgr.SetVisibleControl(Page, SubscriberStatusTD, False)
                    ControlMgr.SetVisibleControl(Page, SubscriberStatusLabelTD, False)
                    CustomerNameTD.Attributes.Item("Class") = CustomerNameTD.Attributes.Item("Class").Replace("bor", "")
                End If

                If Not (.DealerId.Equals(Guid.Empty)) Then
                    DealerNameTD.InnerText = .getDealerDescription
                    ElitaPlusPage.Trace(Page, "Dealer =" & DealerNameTD.InnerText & "@ Cert=" & CertificateNumberTD.InnerText)
                    DealerGroupTD.InnerText = .getDealerGroupName
                End If

                ControlMgr.SetVisibleControl(Page, moRestrictedStatus, .CertificateIsRestricted)


            End With

        End Sub


        Private Function getSalutation(oCertificateCtrl As Certificate) As String
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