
Namespace Certificates


Partial Class EndorsementDetailForm
        Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents ErrorController1 As ErrorController
        Protected WithEvents moCertificateInfoController As UserControlCertificateInfo
        Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
        Protected WithEvents tblManufacturer As System.Web.UI.WebControls.Table
        Protected WithEvents tblWarranty As System.Web.UI.WebControls.Table

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
        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX As Integer = 0
        Public Const GRID_COL_COVERAGE_BEGIN_DATE_PRE_IDX As Integer = 1
        Public Const GRID_COL_COVERAGE_BEGIN_DATE_POST_IDX As Integer = 2
        Public Const GRID_COL_COVERAGE_END_DATE_PRE_IDX As Integer = 3
        Public Const GRID_COL_COVERAGE_END_DATE_POST_IDX As Integer = 4
        Public Const GRID_COL_TERM_PRE_IDX As Integer = 5
        Public Const GRID_COL_TERM_POST_IDX As Integer = 6

#End Region

#Region "Parameters"
        Public Class Parameters
            Public manufaturerWarranty As Boolean = False

            Public Sub New(Warranty As Boolean)
                manufaturerWarranty = Warranty
            End Sub
        End Class
#End Region

#Region "Page State"

        Class MyState
            ' Public MyBO As New CertEndorse
            Public MyBO As CertEndorse
            Public StateCertId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public ManufaWarranty As Boolean
            Public LastErrMsg As String
            Public boChanged As Boolean = False
            Public endorsementId As Guid
            Public _moCertificate As Certificate
            Public _moEndorseCov As CertEndorseCov
            Public companyCode As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    moCertificate = (CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    Me.State.MyBO = New CertEndorse(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID), Guid))
                    Me.State.MyBO.CertId = moCertificate.Id
                    Me.State.ManufaWarranty = CType(NavController.ParametersPassed, Parameters).manufaturerWarranty
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New CertEndorse(CType(CallingParameters, Guid))
                    State.boChanged = False
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Public Property moCertEndorseCov() As CertEndorseCov
            Get
                Return State._moEndorseCov
            End Get
            Set(Value As CertEndorseCov)
                State._moEndorseCov = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value
            End Set
        End Property

        Public ReadOnly Property AddrPreCtr() As UserControlAddress
            Get
                Return moAddrPreController
            End Get
        End Property

        Public ReadOnly Property AddrPostCtr() As UserControlAddress
            Get
                Return moAddrPostController
            End Get
        End Property

#End Region

#Region "Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CertEndorse
                    End If
                    Trace(Me, "CertEndorse Id=" & GuidControl.GuidToHexString(State.MyBO.CertEndorseId))
                    State.companyCode = GetCompanyCode
                    PopulateFormFromBOs()
                    EnableDisableFields()

                End If
                BindBoPropertiesToLabels()
                PopulateEndorseCoveragesGrid()
                CheckIfComingFromSaveConfirm()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

            ShowMissingTranslations(ErrorCtrl)

        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            Try
                If State.ManufaWarranty Then
                    ControlMgr.SetVisibleControl(Me, txtWarrantySalesDatePre, False)
                    ControlMgr.SetVisibleControl(Me, txtWarrantySalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, lblWarrantySalesDatePre, False)
                    ControlMgr.SetVisibleControl(Me, lblWarrantySalesDatePos, False)
                    'Me.ChangeEnabledProperty(Me.moProductSaleDatePrePanel, False)
                    ChangeEnabledProperty(txtProductSalesDatepre, False)
                    ChangeEnabledProperty(txtProductSalesDatePost, False)
                    ChangeEnabledProperty(txtTermPre, False)
                    ChangeEnabledProperty(txtTermPos, False)
                Else
                    ChangeEnabledProperty(txtWarrantySalesDatePre, False)
                    ChangeEnabledProperty(txtWarrantySalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, moProductSaleDatePrePanel, True)
                    ControlMgr.SetVisibleControl(Me, moProductSaleDatePosPanel, True)
                    ChangeEnabledProperty(txtProductSalesDatepre, False)
                    ChangeEnabledProperty(txtProductSalesDatePost, False)
                    'ControlMgr.SetVisibleControl(Me, Me.txtProductSalesDatepre, False)
                    'ControlMgr.SetVisibleControl(Me, Me.txtProductSalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, txtTermPre, False)
                    ControlMgr.SetVisibleControl(Me, txtTermPos, False)

                    'ControlMgr.SetVisibleControl(Me, Me.lblProductSaleDatePre, False)
                    'ControlMgr.SetVisibleControl(Me, Me.lblProductSaleDatePos, False)
                    ControlMgr.SetVisibleControl(Me, lblTermPre, False)
                    ControlMgr.SetVisibleControl(Me, lblTermPos, False)
                    ControlMgr.SetVisibleControl(Me, gridTerm, False)
                End If

                'Me.ChangeEnabledProperty(Me.txtProductSalesDatepre, False)
                'Me.ChangeEnabledProperty(Me.txtProductSalesDatePost, False)
                'Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePre, False)
                'Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePost, False)
                'Me.ChangeEnabledProperty(Me.txtTermPre, False)
                'Me.ChangeEnabledProperty(Me.txtTermPos, False)
                'Me.ChangeEnabledProperty(Me.gridEndorseCov, False)

                ChangeEnabledProperty(txtCustNamePre, False)
                ChangeEnabledProperty(txtCustNamePos, False)
                ChangeEnabledProperty(txtBoxSalesPricePre, False)
                ChangeEnabledProperty(txtBoxSalesPricePost, False)
                ChangeEnabledProperty(txtEmailAddrPre, False)
                ChangeEnabledProperty(txtEmailAddrPost, False)
                ChangeEnabledProperty(txtHomePhonePre, False)
                ChangeEnabledProperty(txtHomePhonePost, False)
                ChangeEnabledProperty(txtWorkPhonePre, False)
                ChangeEnabledProperty(txtWorkPhonePost, False)
                ChangeEnabledProperty(txtLangPrefPre, False)
                ChangeEnabledProperty(txtLangPrefPost, False)
                ChangeEnabledProperty(txtIDTypePre, False)
                ChangeEnabledProperty(txtIDTypePost, False)
                ChangeEnabledProperty(txtDocumentTypePre, False)
                ChangeEnabledProperty(txtDocumentTypePost, False)
                ChangeEnabledProperty(txtDocumentAgencyPre, False)
                ChangeEnabledProperty(txtDocumentAgencyPost, False)
                ChangeEnabledProperty(txtDocumentIssueDatePre, False)
                ChangeEnabledProperty(txtDocumentIssueDatePost, False)
                ChangeEnabledProperty(txtRGNumberPre, False)
                ChangeEnabledProperty(txtRGNumberPost, False)
                ChangeEnabledProperty(txtNewTaxIdPre, False)
                ChangeEnabledProperty(txtNewTaxIdPost, False)
                AddrPreCtr.EnableControls(True)
                AddrPostCtr.EnableControls(True)

                btnBack.Enabled = True
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()

            Try
                BindBOPropertyToLabel(State.MyBO, "CustNamePre", lblCustomerNamePre)
                BindBOPropertyToLabel(State.MyBO, "CustNamePost", lblCustomerNamePos)
                BindBOPropertyToLabel(State.MyBO, "EmailPre", lblEmailAddrPre)
                BindBOPropertyToLabel(State.MyBO, "EmailPost", lblEmailAddrPost)
                BindBOPropertyToLabel(State.MyBO, "HomePhonePre", lblHomePhonePre)
                BindBOPropertyToLabel(State.MyBO, "HomePhonePost", lblHomePhonePost)
                BindBOPropertyToLabel(State.MyBO, "WorkPhonePre", lblWorkPhonePre)
                BindBOPropertyToLabel(State.MyBO, "WorkPhonePost", lblWorkPhonePost)
                BindBOPropertyToLabel(State.MyBO, "TermPre", lblTermPre)
                BindBOPropertyToLabel(State.MyBO, "TermPos", lblTermPos)
                BindBOPropertyToLabel(State.MyBO, "ProductSalesDatePre", lblProductSaleDatePre)
                BindBOPropertyToLabel(State.MyBO, "ProductSalesDatePost", lblProductSaleDatePos)
                BindBOPropertyToLabel(State.MyBO, "ProductSalesDatePre", lblWarrantySalesDatePre)
                BindBOPropertyToLabel(State.MyBO, "ProductSalesDatePost", lblWarrantySalesDatePos)
                BindBOPropertyToLabel(State.MyBO, "SalesPricePre", lblSalesPricePre)
                BindBOPropertyToLabel(State.MyBO, "SalesPricePost", lblSalesPricePos)
                BindBOPropertyToLabel(State.MyBO, "LangaugeIdPre", lblLangPrefPre)
                BindBOPropertyToLabel(State.MyBO, "LangaugeIdPost", lblLangPrefPost)
                BindBOPropertyToLabel(State.MyBO, "DocumentTypeIDPre", lblDocumentTypePre)
                BindBOPropertyToLabel(State.MyBO, "DocumentTypeIDPost", lblDocumentTypePost)
                BindBOPropertyToLabel(State.MyBO, "TaxIDNumbPre", lblNewTaxIdPre)
                BindBOPropertyToLabel(State.MyBO, "TaxIDNumbPost", lblNewTaxIdPost)
                BindBOPropertyToLabel(State.MyBO, "RgNumberPre", lblRGNumberPre)
                BindBOPropertyToLabel(State.MyBO, "RgNumberPost", lblRGNumberPost)
                BindBOPropertyToLabel(State.MyBO, "IdTypePre", lblIDTypePre)
                BindBOPropertyToLabel(State.MyBO, "IdTypePre", lblIDTypePost)
                BindBOPropertyToLabel(State.MyBO, "DocumentAgencyPre", lblDocumentAgencyPre)
                BindBOPropertyToLabel(State.MyBO, "DocumentAgencyPost", lblDocumentAgencyPost)
                BindBOPropertyToLabel(State.MyBO, "DocumentIssueDatePre", lblDocumentIssueDatePre)
                BindBOPropertyToLabel(State.MyBO, "DocumentIssueDatePost", lblDocumentIssueDatePost)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub


        Protected Sub PopulateFormFromBOs()

            Try
                moCertificateInfoController = UserCertificateCtr
                moCertificateInfoController.InitController(State._moCertificate.Id, , State.companyCode)

                With State.MyBO
                    .LoadTerms(State.MyBO)
                    PopulateControlFromBOProperty(txtTermPre, State.MyBO.TermPre)
                    PopulateControlFromBOProperty(txtTermPos, State.MyBO.TermPos)
                    PopulateControlFromBOProperty(txtCustNamePre, .CustNamePre)
                    PopulateControlFromBOProperty(txtCustNamePos, .CustNamePost)
                    PopulateControlFromBOProperty(txtEmailAddrPre, .EmailPre)
                    PopulateControlFromBOProperty(txtEmailAddrPost, .EmailPost)
                    PopulateControlFromBOProperty(txtHomePhonePre, .HomePhonePre)
                    PopulateControlFromBOProperty(txtHomePhonePost, .HomePhonePost)
                    PopulateControlFromBOProperty(txtWorkPhonePre, .WorkPhonePre)
                    PopulateControlFromBOProperty(txtWorkPhonePost, .WorkPhonePost)
                    PopulateControlFromBOProperty(txtProductSalesDatepre, .ProductSalesDatePre)
                    PopulateControlFromBOProperty(txtProductSalesDatePost, .ProductSalesDatePost)
                    PopulateControlFromBOProperty(txtWarrantySalesDatePre, .WarrantySalesDatePre)
                    PopulateControlFromBOProperty(txtWarrantySalesDatePost, .WarrantySalesDatePost)
                    PopulateControlFromBOProperty(txtBoxSalesPricePre, .SalesPricePre)
                    PopulateControlFromBOProperty(txtBoxSalesPricePost, .SalesPricePost)
                    PopulateControlFromBOProperty(txtLangPrefPre, .getLanguagePrefPreDesc)
                    PopulateControlFromBOProperty(txtLangPrefPost, .getLanguagePrefPostDesc)
                    PopulateControlFromBOProperty(txtDocumentTypePre, .getDocTypePreCode)
                    PopulateControlFromBOProperty(txtDocumentTypePost, .getDocTypeCode)
                    PopulateControlFromBOProperty(txtNewTaxIdPre, .TaxIDNumbPre)
                    PopulateControlFromBOProperty(txtNewTaxIdPost, .TaxIDNumbPost)
                    PopulateControlFromBOProperty(txtIDTypePre, .IdTypePre)
                    PopulateControlFromBOProperty(txtIDTypePost, .IdTypePost)
                    PopulateControlFromBOProperty(txtRGNumberPre, .RgNumberPre)
                    PopulateControlFromBOProperty(txtRGNumberPost, .RgNumberPost)
                    PopulateControlFromBOProperty(txtDocumentAgencyPre, .DocumentAgencyPre)
                    PopulateControlFromBOProperty(txtDocumentAgencyPost, .DocumentAgencyPost)
                    PopulateControlFromBOProperty(txtDocumentIssueDatePre, .DocumentIssueDatePre)
                    PopulateControlFromBOProperty(txtDocumentIssueDatePost, .DocumentIssueDatePost)

                    AddrPreCtr.Bind(.AddressPre)
                    AddrPostCtr.Bind(.AddressPost)
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Try
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        State.MyBO.Save()
                    End If
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Properties"

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo)
                End If
                Return moCertificateInfoController
            End Get
        End Property


        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State._moCertificate.CompanyId)

                Return companyBO.Code
            End Get

        End Property

#End Region

#Region "Page Return Type"
        Public Class ReturnType
                Public LastOperation As DetailPageCommand
                Public EditingBo As Certificate
                Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
        End Class
#End Region

#Region "Coverage Datagrid Related"

        Public Sub PopulateEndorseCoveragesGrid()

            Try
                Dim dv As CertEndorseCov.CertEndorsementCoverageSearchDV = CertEndorseCov.GetEndorsementCoverages(State.MyBO.CertEndorseId)
                Dim todayDate As Date

                If Not State.ManufaWarranty Then
                    gridEndorseCov1.AutoGenerateColumns = False
                    gridEndorseCov1.DataSource = dv
                    gridEndorseCov1.DataBind()
                    ControlMgr.SetVisibleControl(Me, pnManufacturer, False)
                    ControlMgr.SetVisibleControl(Me, pnWarranty, True)
                Else
                    gridEndorseCov.AutoGenerateColumns = False
                    gridEndorseCov.DataSource = dv
                    gridEndorseCov.DataBind()
                    ControlMgr.SetVisibleControl(Me, pnManufacturer, True)
                    ControlMgr.SetVisibleControl(Me, pnWarranty, False)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub gridEndorseCov_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridEndorseCov.ItemDataBound

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(GRID_COL_COVERAGE_BEGIN_DATE_PRE_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_PRE), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_BEGIN_DATE_POST_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_POST), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_END_DATE_PRE_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_PRE), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_END_DATE_POST_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_POST), Date))
                    e.Item.Cells(GRID_COL_TERM_PRE_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_TERM_PRE).ToString
                    e.Item.Cells(GRID_COL_TERM_POST_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_TERM_POST).ToString

                End If
            Catch ex As Exception
                    HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub gridEndorseCov1_ItemDataBound(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridEndorseCov1.ItemDataBound

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(GRID_COL_COVERAGE_BEGIN_DATE_PRE_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_PRE), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_BEGIN_DATE_POST_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_POST), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_END_DATE_PRE_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_PRE), Date))
                    e.Item.Cells(GRID_COL_COVERAGE_END_DATE_POST_IDX).Text = GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_POST), Date))
                End If
            Catch ex As Exception
                    HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged)
                NavController.Navigate(Me, "back", retObj)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class
End Namespace
