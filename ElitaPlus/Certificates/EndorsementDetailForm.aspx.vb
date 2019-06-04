
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

            Public Sub New(ByVal Warranty As Boolean)
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
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    moCertificate = (CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    Me.State.MyBO = New CertEndorse(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID), Guid))
                    Me.State.MyBO.CertId = moCertificate.Id
                    Me.State.ManufaWarranty = CType(Me.NavController.ParametersPassed, Parameters).manufaturerWarranty
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New CertEndorse(CType(Me.CallingParameters, Guid))
                    Me.State.boChanged = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Public Property moCertEndorseCov() As CertEndorseCov
            Get
                Return Me.State._moEndorseCov
            End Get
            Set(ByVal Value As CertEndorseCov)
                Me.State._moEndorseCov = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set(ByVal Value As Certificate)
                Me.State._moCertificate = Value
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CertEndorse
                    End If
                    Trace(Me, "CertEndorse Id=" & GuidControl.GuidToHexString(Me.State.MyBO.CertEndorseId))
                    Me.State.companyCode = GetCompanyCode
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()

                End If
                Me.BindBoPropertiesToLabels()
                Me.PopulateEndorseCoveragesGrid()
                Me.CheckIfComingFromSaveConfirm()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

            Me.ShowMissingTranslations(Me.ErrorCtrl)

        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            Try
                If Me.State.ManufaWarranty Then
                    ControlMgr.SetVisibleControl(Me, Me.txtWarrantySalesDatePre, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtWarrantySalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblWarrantySalesDatePre, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblWarrantySalesDatePos, False)
                    'Me.ChangeEnabledProperty(Me.moProductSaleDatePrePanel, False)
                    Me.ChangeEnabledProperty(Me.txtProductSalesDatepre, False)
                    Me.ChangeEnabledProperty(Me.txtProductSalesDatePost, False)
                    Me.ChangeEnabledProperty(Me.txtTermPre, False)
                    Me.ChangeEnabledProperty(Me.txtTermPos, False)
                Else
                    Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePre, False)
                    Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, Me.moProductSaleDatePrePanel, True)
                    ControlMgr.SetVisibleControl(Me, Me.moProductSaleDatePosPanel, True)
                    Me.ChangeEnabledProperty(Me.txtProductSalesDatepre, False)
                    Me.ChangeEnabledProperty(Me.txtProductSalesDatePost, False)
                    'ControlMgr.SetVisibleControl(Me, Me.txtProductSalesDatepre, False)
                    'ControlMgr.SetVisibleControl(Me, Me.txtProductSalesDatePost, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtTermPre, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtTermPos, False)

                    'ControlMgr.SetVisibleControl(Me, Me.lblProductSaleDatePre, False)
                    'ControlMgr.SetVisibleControl(Me, Me.lblProductSaleDatePos, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblTermPre, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblTermPos, False)
                    ControlMgr.SetVisibleControl(Me, Me.gridTerm, False)
                End If

                'Me.ChangeEnabledProperty(Me.txtProductSalesDatepre, False)
                'Me.ChangeEnabledProperty(Me.txtProductSalesDatePost, False)
                'Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePre, False)
                'Me.ChangeEnabledProperty(Me.txtWarrantySalesDatePost, False)
                'Me.ChangeEnabledProperty(Me.txtTermPre, False)
                'Me.ChangeEnabledProperty(Me.txtTermPos, False)
                'Me.ChangeEnabledProperty(Me.gridEndorseCov, False)

                Me.ChangeEnabledProperty(Me.txtCustNamePre, False)
                Me.ChangeEnabledProperty(Me.txtCustNamePos, False)
                Me.ChangeEnabledProperty(Me.txtBoxSalesPricePre, False)
                Me.ChangeEnabledProperty(Me.txtBoxSalesPricePost, False)
                Me.ChangeEnabledProperty(Me.txtEmailAddrPre, False)
                Me.ChangeEnabledProperty(Me.txtEmailAddrPost, False)
                Me.ChangeEnabledProperty(Me.txtHomePhonePre, False)
                Me.ChangeEnabledProperty(Me.txtHomePhonePost, False)
                Me.ChangeEnabledProperty(Me.txtWorkPhonePre, False)
                Me.ChangeEnabledProperty(Me.txtWorkPhonePost, False)
                Me.ChangeEnabledProperty(Me.txtLangPrefPre, False)
                Me.ChangeEnabledProperty(Me.txtLangPrefPost, False)
                Me.ChangeEnabledProperty(Me.txtIDTypePre, False)
                Me.ChangeEnabledProperty(Me.txtIDTypePost, False)
                Me.ChangeEnabledProperty(Me.txtDocumentTypePre, False)
                Me.ChangeEnabledProperty(Me.txtDocumentTypePost, False)
                Me.ChangeEnabledProperty(Me.txtDocumentAgencyPre, False)
                Me.ChangeEnabledProperty(Me.txtDocumentAgencyPost, False)
                Me.ChangeEnabledProperty(Me.txtDocumentIssueDatePre, False)
                Me.ChangeEnabledProperty(Me.txtDocumentIssueDatePost, False)
                Me.ChangeEnabledProperty(Me.txtRGNumberPre, False)
                Me.ChangeEnabledProperty(Me.txtRGNumberPost, False)
                Me.ChangeEnabledProperty(Me.txtNewTaxIdPre, False)
                Me.ChangeEnabledProperty(Me.txtNewTaxIdPost, False)
                AddrPreCtr.EnableControls(True)
                AddrPostCtr.EnableControls(True)

                Me.btnBack.Enabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()

            Try
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CustNamePre", lblCustomerNamePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CustNamePost", lblCustomerNamePos)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EmailPre", lblEmailAddrPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EmailPost", lblEmailAddrPost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "HomePhonePre", lblHomePhonePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "HomePhonePost", lblHomePhonePost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "WorkPhonePre", lblWorkPhonePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "WorkPhonePost", lblWorkPhonePost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TermPre", lblTermPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TermPos", lblTermPos)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDatePre", Me.lblProductSaleDatePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDatePost", Me.lblProductSaleDatePos)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDatePre", Me.lblWarrantySalesDatePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDatePost", Me.lblWarrantySalesDatePos)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SalesPricePre", Me.lblSalesPricePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SalesPricePost", Me.lblSalesPricePos)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "LangaugeIdPre", Me.lblLangPrefPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "LangaugeIdPost", Me.lblLangPrefPost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentTypeIDPre", Me.lblDocumentTypePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentTypeIDPost", Me.lblDocumentTypePost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIDNumbPre", Me.lblNewTaxIdPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIDNumbPost", Me.lblNewTaxIdPost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RgNumberPre", Me.lblRGNumberPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RgNumberPost", Me.lblRGNumberPost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "IdTypePre", Me.lblIDTypePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "IdTypePre", Me.lblIDTypePost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentAgencyPre", Me.lblDocumentAgencyPre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentAgencyPost", Me.lblDocumentAgencyPost)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentIssueDatePre", Me.lblDocumentIssueDatePre)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentIssueDatePost", Me.lblDocumentIssueDatePost)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


        Protected Sub PopulateFormFromBOs()

            Try
                moCertificateInfoController = Me.UserCertificateCtr
                moCertificateInfoController.InitController(Me.State._moCertificate.Id, , Me.State.companyCode)

                With Me.State.MyBO
                    .LoadTerms(Me.State.MyBO)
                    Me.PopulateControlFromBOProperty(Me.txtTermPre, Me.State.MyBO.TermPre)
                    Me.PopulateControlFromBOProperty(Me.txtTermPos, Me.State.MyBO.TermPos)
                    Me.PopulateControlFromBOProperty(Me.txtCustNamePre, .CustNamePre)
                    Me.PopulateControlFromBOProperty(Me.txtCustNamePos, .CustNamePost)
                    Me.PopulateControlFromBOProperty(Me.txtEmailAddrPre, .EmailPre)
                    Me.PopulateControlFromBOProperty(Me.txtEmailAddrPost, .EmailPost)
                    Me.PopulateControlFromBOProperty(Me.txtHomePhonePre, .HomePhonePre)
                    Me.PopulateControlFromBOProperty(Me.txtHomePhonePost, .HomePhonePost)
                    Me.PopulateControlFromBOProperty(Me.txtWorkPhonePre, .WorkPhonePre)
                    Me.PopulateControlFromBOProperty(Me.txtWorkPhonePost, .WorkPhonePost)
                    Me.PopulateControlFromBOProperty(Me.txtProductSalesDatepre, .ProductSalesDatePre)
                    Me.PopulateControlFromBOProperty(Me.txtProductSalesDatePost, .ProductSalesDatePost)
                    Me.PopulateControlFromBOProperty(Me.txtWarrantySalesDatePre, .WarrantySalesDatePre)
                    Me.PopulateControlFromBOProperty(Me.txtWarrantySalesDatePost, .WarrantySalesDatePost)
                    Me.PopulateControlFromBOProperty(Me.txtBoxSalesPricePre, .SalesPricePre)
                    Me.PopulateControlFromBOProperty(Me.txtBoxSalesPricePost, .SalesPricePost)
                    Me.PopulateControlFromBOProperty(Me.txtLangPrefPre, .getLanguagePrefPreDesc)
                    Me.PopulateControlFromBOProperty(Me.txtLangPrefPost, .getLanguagePrefPostDesc)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentTypePre, .getDocTypePreCode)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentTypePost, .getDocTypeCode)
                    Me.PopulateControlFromBOProperty(Me.txtNewTaxIdPre, .TaxIDNumbPre)
                    Me.PopulateControlFromBOProperty(Me.txtNewTaxIdPost, .TaxIDNumbPost)
                    Me.PopulateControlFromBOProperty(Me.txtIDTypePre, .IdTypePre)
                    Me.PopulateControlFromBOProperty(Me.txtIDTypePost, .IdTypePost)
                    Me.PopulateControlFromBOProperty(Me.txtRGNumberPre, .RgNumberPre)
                    Me.PopulateControlFromBOProperty(Me.txtRGNumberPost, .RgNumberPost)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentAgencyPre, .DocumentAgencyPre)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentAgencyPost, .DocumentAgencyPost)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentIssueDatePre, .DocumentIssueDatePre)
                    Me.PopulateControlFromBOProperty(Me.txtDocumentIssueDatePost, .DocumentIssueDatePost)

                    AddrPreCtr.Bind(.AddressPre)
                    AddrPostCtr.Bind(.AddressPost)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Try
                If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                    If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Me.State.MyBO.Save()
                    End If
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
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
                Dim companyBO As Company = New Company(Me.State._moCertificate.CompanyId)

                Return companyBO.Code
            End Get

        End Property

#End Region

#Region "Page Return Type"
        Public Class ReturnType
                Public LastOperation As DetailPageCommand
                Public EditingBo As Certificate
                Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Certificate, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
        End Class
#End Region

#Region "Coverage Datagrid Related"

        Public Sub PopulateEndorseCoveragesGrid()

            Try
                Dim dv As CertEndorseCov.CertEndorsementCoverageSearchDV = CertEndorseCov.GetEndorsementCoverages(Me.State.MyBO.CertEndorseId)
                Dim todayDate As Date

                If Not Me.State.ManufaWarranty Then
                    Me.gridEndorseCov1.AutoGenerateColumns = False
                    Me.gridEndorseCov1.DataSource = dv
                    Me.gridEndorseCov1.DataBind()
                    ControlMgr.SetVisibleControl(Me, pnManufacturer, False)
                    ControlMgr.SetVisibleControl(Me, pnWarranty, True)
                Else
                    Me.gridEndorseCov.AutoGenerateColumns = False
                    Me.gridEndorseCov.DataSource = dv
                    Me.gridEndorseCov.DataBind()
                    ControlMgr.SetVisibleControl(Me, pnManufacturer, True)
                    ControlMgr.SetVisibleControl(Me, pnWarranty, False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub gridEndorseCov_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridEndorseCov.ItemDataBound

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_BEGIN_DATE_PRE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_PRE), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_BEGIN_DATE_POST_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_POST), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_END_DATE_PRE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_PRE), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_END_DATE_POST_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_POST), Date))
                    e.Item.Cells(Me.GRID_COL_TERM_PRE_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_TERM_PRE).ToString
                    e.Item.Cells(Me.GRID_COL_TERM_POST_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_TERM_POST).ToString

                End If
            Catch ex As Exception
                    Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub gridEndorseCov1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridEndorseCov1.ItemDataBound

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_BEGIN_DATE_PRE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_PRE), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_BEGIN_DATE_POST_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_BEGIN_DATE_POST), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_END_DATE_PRE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_PRE), Date))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_END_DATE_POST_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertEndorseCov.CertEndorsementCoverageSearchDV.GRID_COL_COVERAGE_END_DATE_POST), Date))
                End If
            Catch ex As Exception
                    Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                Me.NavController.Navigate(Me, "back", retObj)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class
End Namespace
