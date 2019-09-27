Namespace Certificates


    Partial Class UserControlCertificateInfo
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
            'Me.moCertificateText.ReadOnly = True
            'Me.moStatusText.ReadOnly = True
            'Me.moDealerNameText.ReadOnly = True
            'Me.moWarrantySoldOnText.ReadOnly = True
            Page.SetEnabledForControlFamily(Me, False)

        End Sub

        Public Sub PopulateFormFromCertificateCtrl(ByVal oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
            With oCertificateCtrl
                Me.moCertificateText.Text = .CertNumber
                Me.moWarrantySoldOnText.Text = CType(.WarrantySalesDate.Value, String)
                Me.moWarrantySoldOnText.Text = ElitaPlusPage.GetDateFormattedStringNullable(.WarrantySalesDate.Value)
                Me.moStatusText.Text = .StatusCode
                If Not (.SubscriberStatus.Equals(Guid.Empty)) Then
                    Me.moSubStatusText.Text = LookupListNew.GetCodeFromId("SUBSTAT", .SubscriberStatus)
                Else
                    ControlMgr.SetVisibleControl(Page, Me.moSubStatusLabel, False)
                    ControlMgr.SetVisibleControl(Page, Me.moSubStatusText, False)
                End If

                Me.moCompanyNameText.Text = companyCode
                If Not (.DealerId.Equals(Guid.Empty)) Then
                    Me.moDealerNameText.Text = .getDealerDescription
                    ElitaPlusPage.Trace(Me.Page, "Dealer =" & Me.moDealerNameText.Text & "@ Cert=" & Me.moCertificateText.Text)
                    Me.moDealerGroupText.Text = .getDealerGroupName
                End If
                If Not riskTypeDescription Is Nothing Then
                    Me.TextboxCustomerName.Text = getSalutation(oCertificateCtrl) & .CustomerName
                    Me.TextboxRiskType.Text = riskTypeDescription
                    ControlMgr.SetVisibleControl(Page, Me.moCompanyNameText, True)
                    'ControlMgr.SetVisibleControl(Page, TextboxCustomerName, True)
                    ControlMgr.SetVisibleControl(Page, TextboxRiskType, True)
                    'ControlMgr.SetVisibleControl(Page, Me.LabelCompanyName, True)
                    ControlMgr.SetVisibleControl(Page, LabelCustomerName, True)
                    ControlMgr.SetVisibleControl(Page, LabelRiskType, True)
                    If Not Me.LabelCustomerName.Text.EndsWith(":") Then
                        Me.LabelCustomerName.Text &= ":"
                    End If
                    If Not Me.LabelRiskType.Text.EndsWith(":") Then
                        Me.LabelRiskType.Text &= ":"
                    End If
                Else
                    'ControlMgr.SetVisibleControl(Page, Me.moCompanyText, False)
                    ControlMgr.SetVisibleControl(Page, TextboxCustomerName, False)
                    ControlMgr.SetVisibleControl(Page, TextboxRiskType, False)
                    'ControlMgr.SetVisibleControl(Page, Me.labelCompany, False)
                    ControlMgr.SetVisibleControl(Page, LabelCustomerName, False)
                    ControlMgr.SetVisibleControl(Page, LabelRiskType, False)
                End If



            End With

        End Sub


        Private Function getSalutation(ByVal oCertificateCtrl As Certificate) As String

            '  Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
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