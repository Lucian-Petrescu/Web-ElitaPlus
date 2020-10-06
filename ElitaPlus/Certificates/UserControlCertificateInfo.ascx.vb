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
            'Me.moCertificateText.ReadOnly = True
            'Me.moStatusText.ReadOnly = True
            'Me.moDealerNameText.ReadOnly = True
            'Me.moWarrantySoldOnText.ReadOnly = True
            Page.SetEnabledForControlFamily(Me, False)

        End Sub

        Public Sub PopulateFormFromCertificateCtrl(oCertificateCtrl As Certificate, Optional ByVal riskTypeDescription As String = Nothing, Optional ByVal companyCode As String = Nothing)
            With oCertificateCtrl
                moCertificateText.Text = .CertNumber
                moWarrantySoldOnText.Text = CType(.WarrantySalesDate.Value, String)
                moWarrantySoldOnText.Text = ElitaPlusPage.GetDateFormattedStringNullable(.WarrantySalesDate.Value)
                moStatusText.Text = .StatusCode
                If Not (.SubscriberStatus.Equals(Guid.Empty)) Then
                    moSubStatusText.Text = LookupListNew.GetCodeFromId("SUBSTAT", .SubscriberStatus)
                Else
                    ControlMgr.SetVisibleControl(Page, moSubStatusLabel, False)
                    ControlMgr.SetVisibleControl(Page, moSubStatusText, False)
                End If

                moCompanyNameText.Text = companyCode
                If Not (.DealerId.Equals(Guid.Empty)) Then
                    moDealerNameText.Text = .getDealerDescription
                    ElitaPlusPage.Trace(Page, "Dealer =" & moDealerNameText.Text & "@ Cert=" & moCertificateText.Text)
                    moDealerGroupText.Text = .getDealerGroupName
                End If
                If riskTypeDescription IsNot Nothing Then
                    TextboxCustomerName.Text = getSalutation(oCertificateCtrl) & .CustomerName
                    TextboxRiskType.Text = riskTypeDescription
                    ControlMgr.SetVisibleControl(Page, moCompanyNameText, True)
                    'ControlMgr.SetVisibleControl(Page, TextboxCustomerName, True)
                    ControlMgr.SetVisibleControl(Page, TextboxRiskType, True)
                    'ControlMgr.SetVisibleControl(Page, Me.LabelCompanyName, True)
                    ControlMgr.SetVisibleControl(Page, LabelCustomerName, True)
                    ControlMgr.SetVisibleControl(Page, LabelRiskType, True)
                    If Not LabelCustomerName.Text.EndsWith(":") Then
                        LabelCustomerName.Text &= ":"
                    End If
                    If Not LabelRiskType.Text.EndsWith(":") Then
                        LabelRiskType.Text &= ":"
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


        Private Function getSalutation(oCertificateCtrl As Certificate) As String

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