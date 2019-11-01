Imports System.Threading
Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements


Namespace Certificates

    Partial Class EndorsementForm
        Inherits ElitaPlusSearchPage

#Region "Web controls and private members"
        Public Const PAGETITLE As String = "ENDORSEMENT"
        Public Const PAGETAB As String = "CERTIFICATE"
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moCertificateInfoController As UserControlCertificateInfo_New
        Protected WithEvents moAddressControllerEndorsement As UserControlAddress_New

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

        Public Const MANUFACTURER As String = "Manufacturer"
        Public Const NO_TERM As Integer = 99
        Public Const CERT_STATUS As String = "A"
        Public Const COL_CERT_ITEM_ID As String = "cert_item_id"
        Private Const ACTION_NONE As String = "ACTION_NONE"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CERT_ITEM_COVERAGE_IDX As Integer = 1
        Public Const GRID_COL_RISK_TYPE_DESCRIPTION_IDX As Integer = 2
        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX As Integer = 3
        Public Const GRID_COL_BEGIN_DATE_IDX As Integer = 4
        Public Const GRID_COL_END_DATE_IDX As Integer = 5

        Public Const ID_CONTROL_NAME As String = "lblId"
        Public Const RISK_TYPE_CONTROL_NAME As String = "lblRiskType"
        Public Const COVERAGE_TYPE_CONTROL_NAME As String = "lblCoverageType"
        Public Const BEGIN_DATE_CONTROL_NAME As String = "lblBeginDate"
        Public Const BEGIN_DATE_TEXTBOX_CONTROL_NAME As String = "txtBeginDate"
        Public Const END_DATE_CONTROL_NAME As String = "lblEndDate"
        Private Const MONTH As String = "M"
        Private Const YES As String = "Y"
#End Region

#Region "Parameters"
        Public Class Parameters
            Public CertId As Guid
            Public manufaturerWarranty As Boolean = False
            Public Sub New(ByVal certid As Guid, ByVal Warranty As Boolean)
                Me.CertId = certid
                manufaturerWarranty = Warranty
            End Sub
        End Class
#End Region

#Region "Properties"


        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property


        Public Property IsEdit() As Boolean
            Get
                Return Me.State._IsEdit
            End Get
            Set(ByVal Value As Boolean)
                Me.State._IsEdit = Value
            End Set
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(Me.State._moCertificate.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate() As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set(ByVal Value As Certificate)
                Me.State._moCertificate = Value
            End Set
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

#Region "Page State"

        Class BaseState
            Public NavCtrl As INavigationController
        End Class

        Class MyState
            Public ScreenSnapShotBO As CertEndorse
            ' Public MyBO As New CertEndorse
            Public MyBO As CertEndorse
            Public StateCertId As Guid
            Public StatemanufaturerWarranty As Boolean
            Public IsLowestCovStrtDtEqual2PrdSalesDt As Boolean
            Public isECSDurationFix As Boolean = False
            Public OrigBeginDate As DateType
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public boChanged As Boolean = False
            Public _IsEdit As Boolean
            Public _moCertificate As Certificate
            Public companyCode As String
            Public searchDV As DataView
            Public IsEditGrd As Boolean
            Public CovId As Guid
            Public CovTerm As Long
            Public MyCovBO As CertItemCoverage
            Public IsDealerEndorsementFlagOn As String
            Public ClaimCountForParentAndChildCert As Integer
            Public AttValueEnableChangingMFG As AttributeValue

        End Class

        Public Sub New()
            MyBase.New(New BaseState)
        End Sub

        Public ReadOnly Property AddressCtr() As UserControlAddress_New
            Get
                Return Me.moAddressControllerEndorsement
            End Get
        End Property

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.MyBO = New CertEndorse
                    Me.State.MyBO.CertId = CType(Me.NavController.ParametersPassed, Parameters).CertId
                    'Me.State.MyBO.CertId = Me.State.StateCertId
                    moCertificate = (CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))

                    Me.State.StatemanufaturerWarranty = CType(Me.NavController.ParametersPassed, Parameters).manufaturerWarranty
                    Me.State.companyCode = GetCompanyCode
                    If Not Me.State.MyBO Is Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                    Me.IsEdit = True
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    'Me.State.MyBO = New CertEndorse(CType(Me.CallingParameters, Guid))
                    Me.State.boChanged = False

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Me.MasterPage.MessageController.Clear_Hide()

            Try
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Me.PAGETAB)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
                Me.UpdateBreadCrum()

                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.CreateNew()
                    End If
                    Trace(Me, "CertEndorse Id=" & GuidControl.GuidToHexString(Me.State.MyBO.CertEndorseId))
                    'Date Calendars
                    Me.AddCalendar(Me.ImageButtonProductSaleDate, Me.TextboxProductSaleDate)
                    Me.AddCalendar(Me.ImageButtonWarrantySaleDate, Me.TextboxWarrantySalesDate)
                    Me.AddCalendar(Me.ImageButtonDocumentIssueDate, Me.moDocumentIssueDateText)

                    ' Me.State.companyCode = GetCompanyCode
                    PopulateDocumentTypeDropdown()
                    PopulateBOsFromCertForm()
                    Me.PopulateFormFromBOs()
                    Me.TranslateGridHeader(Me.grdCoverages)
                    Me.TranslateGridControls(Me.grdCoverages)
                    PopulateCovGrid()
                    SetCovButtonsState(True)
                    Me.EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                AddressCtr.ReAssignTabIndex(10)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Select Case Me.CalledUrl
                    Case LocateServiceCenterForm.URL
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            Try
                Me.State.IsLowestCovStrtDtEqual2PrdSalesDt = IsLowestCovStrtDtEqual2PrdSalesDt()
                If Not IsEdit Then
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, True)
                    ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
                    ControlMgr.SetEnableControl(Me, Me.btnAdd_WRITE, False)
                    Me.ChangeEnabledProperty(Me.TextboxCustomerName, False)
                    Me.ChangeEnabledProperty(Me.TextboxEmailAddress, False)
                    Me.ChangeEnabledProperty(Me.TextboxWorkPhone, False)
                    Me.ChangeEnabledProperty(Me.TextboxHomePhone, False)
                    Me.ChangeEnabledProperty(Me.moNewTaxIdText, False)
                    Me.ChangeEnabledProperty(Me.moIDTypeText, False)
                    Me.ChangeEnabledProperty(Me.moDocumentAgencyText, False)
                    Me.ChangeEnabledProperty(Me.moDocumentIssueDateText, False)
                    If Me.State.StatemanufaturerWarranty Then
                        Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, False)
                        Me.ChangeEnabledProperty(Me.TextboxProductSaleDate, False)
                        Me.ChangeEnabledProperty(Me.ImageButtonProductSaleDate, False)
                        ControlMgr.SetVisibleControl(Me, Me.TextboxWarrantySalesDate, False)
                        ControlMgr.SetVisibleControl(Me, Me.LabelWarrantySalesDate, False)
                        ControlMgr.SetVisibleControl(Me, Me.ImageButtonWarrantySaleDate, False)
                        Me.ChangeEnabledProperty(Me.TextboxSalesPrice, False)
                    Else
                        If Me.State.IsLowestCovStrtDtEqual2PrdSalesDt Then
                            Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, False)
                            Me.ChangeEnabledProperty(Me.TextboxWarrantySalesDate, False)
                            Me.ChangeEnabledProperty(Me.ImageButtonWarrantySaleDate, False)
                            Me.ChangeEnabledProperty(Me.TextboxProductSaleDate, False)
                            Me.ChangeEnabledProperty(Me.ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxProductSaleDate, True)
                            ControlMgr.SetVisibleControl(Me, Me.LabelProductSaleDate, True)
                            ControlMgr.SetVisibleControl(Me, Me.ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.LabelWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.ImageButtonWarrantySaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.LabelManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxManufacturerTerm, False)
                        Else
                            Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, False)
                            Me.ChangeEnabledProperty(Me.TextboxWarrantySalesDate, False)
                            Me.ChangeEnabledProperty(Me.ImageButtonWarrantySaleDate, False)
                            Me.ChangeEnabledProperty(Me.TextboxSalesPrice, False)
                            Me.ChangeEnabledProperty(Me.TextboxProductSaleDate, False)
                            Me.ChangeEnabledProperty(Me.ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.LabelManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, Me.LabelProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.TextboxWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.LabelWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, Me.ImageButtonWarrantySaleDate, False)
                        End If
                        Me.ChangeEnabledProperty(Me.TextboxSalesPrice, False)
                    End If
                    ControlMgr.SetVisibleControl(Me, moLangPrefDropdown, False)
                    ControlMgr.SetVisibleControl(Me, TextboxLangPref, True)
                    ControlMgr.SetVisibleControl(Me, moDocumentTypeText, True)
                    ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonDocumentIssueDate, False)
                    Me.ChangeEnabledProperty(Me.moRGNumberText, False)
                    Me.ChangeEnabledProperty(Me.ImageButtonDocumentIssueDate, False)
                    AddressCtr.EnableControls(True)

                    EnableDisableTaxIdControls(Me.moDocumentTypeText.Text.Trim())
                Else 'Edit Mode
                    ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, True)
                    ControlMgr.SetEnableControl(Me, Me.btnAdd_WRITE, True)
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, False)
                    Me.ChangeEnabledProperty(Me.TextboxCustomerName, True)
                    Me.ChangeEnabledProperty(Me.TextboxEmailAddress, True)
                    Me.ChangeEnabledProperty(Me.TextboxWorkPhone, True)
                    Me.ChangeEnabledProperty(Me.TextboxHomePhone, True)

                    If Me.State.StatemanufaturerWarranty Then
                        Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, True)
                        Me.ChangeEnabledProperty(Me.TextboxProductSaleDate, True)
                        'Me.TextboxProductSaleDate.ReadOnly = True
                        ControlMgr.SetEnableControl(Me, Me.ImageButtonProductSaleDate, True)
                    Else
                        If Me.State.IsLowestCovStrtDtEqual2PrdSalesDt Then
                            Me.ChangeEnabledProperty(Me.TextboxProductSaleDate, True)
                            ControlMgr.SetEnableControl(Me, Me.ImageButtonProductSaleDate, True)
                        Else
                            'Me.ChangeEnabledProperty(Me.TextboxWarrantySalesDate, True)
                            'ControlMgr.SetEnableControl(Me, Me.ImageButtonWarrantySaleDate, True)
                        End If
                    End If
                    Me.ChangeEnabledProperty(Me.TextboxSalesPrice, True)
                    ControlMgr.SetVisibleControl(Me, moLangPrefDropdown, True)
                    ControlMgr.SetVisibleControl(Me, TextboxLangPref, False)
                    ControlMgr.SetVisibleControl(Me, moDocumentTypeText, False)
                    ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonDocumentIssueDate, True)
                    Me.ChangeEnabledProperty(Me.ImageButtonDocumentIssueDate, True)
                    Me.ChangeEnabledProperty(Me.moNewTaxIdText, True)
                    Me.ChangeEnabledProperty(Me.moIDTypeText, True)
                    Me.ChangeEnabledProperty(Me.moDocumentAgencyText, True)
                    Me.ChangeEnabledProperty(Me.moDocumentIssueDateText, True)
                    Me.ChangeEnabledProperty(Me.moRGNumberText, True)

                    AddressCtr.EnableControls(False, True)

                    EnableDisableTaxIdControls(Me.cboDocumentTypeId.SelectedItem.Text)
                End If

                If Me.State.MyBO.AssociatedCertItems.Count > 1 Then
                    ControlMgr.SetVisibleControl(Me, Me.LabelSalesPrice, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxSalesPrice, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.LabelSalesPrice, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxSalesPrice, True)
                End If

                'REQ-1162
                If Me.State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                    Me.moWarrantyInformation1.Attributes("style") = "display: none"
                    Me.moWarrantyInformation11.Attributes("style") = "display: none"
                    Me.moWarrantyInformation2.Attributes("style") = "display: none"
                    Me.moWarrantyInformation3.Attributes("style") = "display: none"
                    Me.moWarrantyInformation4.Attributes("style") = "display: none"
                Else
                    Me.moWarrantyInformation1.Attributes("style") = ""
                    Me.moWarrantyInformation11.Attributes("style") = ""
                    Me.moWarrantyInformation2.Attributes("style") = ""
                    Me.moWarrantyInformation3.Attributes("style") = ""
                    Me.moWarrantyInformation4.Attributes("style") = ""
                End If

                If CheckCertUseCustomer() Then

                    Me.moCustomerFirstNameLabel.Visible = True
                    Me.moCustomerFirstNameText.Visible = True
                    Me.moCustomerMiddleNameLabel.Visible = True
                    Me.moCustomerMiddleNameText.Visible = True
                    Me.moCustomerLastNameLabel.Visible = True
                    Me.moCustomerLastNameText.Visible = True
                    Me.LabelCustomerName.Visible = False
                    Me.TextboxCustomerName.Visible = False
                    Me.TextboxCustomerName.ReadOnly = True

                Else
                    Me.LabelCustomerName.Enabled = True
                    Me.TextboxCustomerName.Visible = True
                End If

                If Me.State.ClaimCountForParentAndChildCert > 0 Then
                    Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelClaimsExist, True)
                Else
                    Me.ChangeEnabledProperty(Me.TextboxManufacturerTerm, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelClaimsExist, False)
                End If
                Me.btnBack.Enabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()

            Try
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CustNamePost", Me.LabelCustomerName)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductSalesDatePost", Me.LabelProductSaleDate)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "WarrantySalesDatePost", Me.LabelWarrantySalesDate)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TermPos", Me.LabelManufacturerTerm)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SalesPrice", Me.LabelSalesPrice)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "LangaugeIdPost", Me.LabelLangPref)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EmailPost", Me.LabelEmailAddress)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "HomePhonePost", Me.LabelHomePhone)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "WorkPhonePost", Me.LabelWorkPhone)

                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentIssueDatePost", Me.moDocumentIssueDateLabel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentTypeIDPost", Me.moDocumentTypeLabel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RgNumberPost", Me.moRGNumberLabel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DocumentAgencyPost", Me.moDocumentAgencyLabel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "IdTypePost", Me.moIDTypeLabel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIDNumbPost", Me.moNewTaxIdLabel)

                Me.ClearGridHeadersAndLabelsErrSign()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub EnableDisableTaxIdControls(ByVal docType As String)

            If docType = "CNPJ" Then
                ControlMgr.SetEnableControl(Me, Me.moIDTypeText, False)
                ControlMgr.SetEnableControl(Me, Me.moDocumentAgencyText, False)
                ControlMgr.SetEnableControl(Me, Me.moRGNumberText, False)
                ControlMgr.SetEnableControl(Me, Me.moDocumentIssueDateText, False)
                ControlMgr.SetEnableControl(Me, Me.ImageButtonDocumentIssueDate, False)
                moIDTypeText.Text = String.Empty
                moDocumentAgencyText.Text = String.Empty
                moDocumentIssueDateText.Text = String.Empty
                moRGNumberText.Text = String.Empty
            Else
                ControlMgr.SetEnableControl(Me, Me.moIDTypeText, True)
                ControlMgr.SetEnableControl(Me, Me.moDocumentAgencyText, True)
                ControlMgr.SetEnableControl(Me, Me.moRGNumberText, True)
                ControlMgr.SetEnableControl(Me, Me.moDocumentIssueDateText, True)
                ControlMgr.SetEnableControl(Me, Me.ImageButtonDocumentIssueDate, True)

            End If
        End Sub

        Private Sub PopulateDocumentTypeDropdown()

            Try
                Dim documentTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode())
                cboDocumentTypeId.Populate(documentTypeList, New PopulateOptions() With
                {
                  .AddBlankItem = True
                })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub PopulateBOsFromCertForm()
            Try
                Me.State.MyBO.Cert.CustomerName = moCertificate.CustomerName
                Me.State.MyBO.Cert.Email = moCertificate.Email
                Me.State.MyBO.Cert.HomePhone = moCertificate.HomePhone
                Me.State.MyBO.Cert.WorkPhone = moCertificate.WorkPhone
                Me.State.MyBO.Cert.ProductSalesDate = moCertificate.ProductSalesDate
                Me.State.MyBO.Cert.WarrantySalesDate = moCertificate.WarrantySalesDate
                Me.State.MyBO.Cert.SalesPrice = moCertificate.SalesPrice
                Me.State.MyBO.Cert.LanguageId = moCertificate.LanguageId
                Me.State.MyBO.Cert.DocumentTypeID = moCertificate.DocumentTypeID
                Me.State.MyBO.Cert.TaxIDNumb = moCertificate.TaxIDNumb
                Me.State.MyBO.Cert.IdType = moCertificate.IdType
                Me.State.MyBO.Cert.RgNumber = moCertificate.RgNumber
                Me.State.MyBO.Cert.DocumentAgency = moCertificate.DocumentAgency
                Me.State.MyBO.Cert.DocumentIssueDate = moCertificate.DocumentIssueDate
                Me.State.MyBO.Cert.AddressChild.Address1 = moCertificate.AddressChild.Address1
                Me.State.MyBO.Cert.AddressChild.Address2 = moCertificate.AddressChild.Address2
                Me.State.MyBO.Cert.AddressChild.Address3 = moCertificate.AddressChild.Address3
                Me.State.MyBO.Cert.AddressChild.CountryId = moCertificate.AddressChild.CountryId
                Me.State.MyBO.Cert.AddressChild.RegionId = moCertificate.AddressChild.RegionId
                Me.State.MyBO.Cert.AddressChild.City = moCertificate.AddressChild.City
                Me.State.MyBO.Cert.AddressChild.PostalCode = moCertificate.AddressChild.PostalCode
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub PopulateFormFromBOs()

            Try
                Me.GetDefaultTerm(True)
                PopulateLangPrefDropdown(Me.moLangPrefDropdown)

                moCertificateInfoController = Me.UserCertificateCtr
                moCertificateInfoController.InitController(Me.State.MyBO.Cert.Id, , Me.State.companyCode)

                With Me.State.MyBO.Cert
                    If Not Me.LabelManufacturerTerm.Text.EndsWith(":") Then Me.LabelManufacturerTerm.Text &= ":"
                    Me.PopulateControlFromBOProperty(Me.TextboxManufacturerTerm, Me.State.MyBO.TermPre)

                    If CheckCertUseCustomer() Then
                        Me.PopulateControlFromBOProperty(Me.moCustomerFirstNameText, .CustomerFirstName)
                        Me.PopulateControlFromBOProperty(Me.moCustomerMiddleNameText, .CustomerMiddleName)
                        Me.PopulateControlFromBOProperty(Me.moCustomerLastNameText, .CustomerLastName)
                        If Not Me.moCustomerFirstNameLabel.Text.EndsWith(":") Then Me.moCustomerFirstNameLabel.Text &= ":"
                        If Not Me.moCustomerMiddleNameLabel.Text.EndsWith(":") Then Me.moCustomerMiddleNameLabel.Text &= ":"
                        If Not Me.moCustomerLastNameLabel.Text.EndsWith(":") Then Me.moCustomerLastNameLabel.Text &= ":"
                    Else
                        Me.PopulateControlFromBOProperty(Me.TextboxCustomerName, .CustomerName)
                        If Not Me.LabelCustomerName.Text.EndsWith(":") Then Me.LabelCustomerName.Text &= ":"
                    End If

                    Me.PopulateControlFromBOProperty(Me.TextboxProductSaleDate, .ProductSalesDate)
                    If Not Me.LabelProductSaleDate.Text.EndsWith(":") Then Me.LabelProductSaleDate.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxWarrantySalesDate, .WarrantySalesDate)
                    If Not Me.LabelWarrantySalesDate.Text.EndsWith(":") Then Me.LabelWarrantySalesDate.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxSalesPrice, .SalesPrice)
                    If Not Me.LabelSalesPrice.Text.EndsWith(":") Then Me.LabelSalesPrice.Text &= ":"

                    If Not (.LanguageId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(Me.moLangPrefDropdown, .LanguageId)
                        Me.PopulateControlFromBOProperty(Me.TextboxLangPref, .getLanguagePrefDesc)
                    End If
                    If Not Me.LabelLangPref.Text.EndsWith(":") Then Me.LabelLangPref.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxWarrantySalesDate, .WarrantySalesDate)
                    If Not Me.LabelWarrantySalesDate.Text.EndsWith(":") Then Me.LabelWarrantySalesDate.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxEmailAddress, .Email)
                    If Not Me.LabelEmailAddress.Text.EndsWith(":") Then Me.LabelEmailAddress.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxHomePhone, .HomePhone)
                    If Not Me.LabelHomePhone.Text.EndsWith(":") Then Me.LabelHomePhone.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.TextboxWorkPhone, .WorkPhone)
                    If Not Me.LabelWorkPhone.Text.EndsWith(":") Then Me.LabelWorkPhone.Text &= ":"

                    If Not Me.moDocumentTypeLabel.Text.EndsWith(":") Then Me.moDocumentTypeLabel.Text &= ":"
                    If Not (.DocumentTypeID.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(Me.cboDocumentTypeId, .DocumentTypeID)
                        Me.PopulateControlFromBOProperty(Me.moDocumentTypeText, .getDocTypeDesc)
                    End If

                    Me.PopulateControlFromBOProperty(Me.moNewTaxIdText, .TaxIDNumb)
                    If Not Me.moNewTaxIdLabel.Text.EndsWith(":") Then Me.moNewTaxIdLabel.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.moIDTypeText, .IdType)
                    If Not Me.moIDTypeLabel.Text.EndsWith(":") Then Me.moIDTypeLabel.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.moRGNumberText, .RgNumber)
                    If Not Me.moRGNumberLabel.Text.EndsWith(":") Then Me.moRGNumberLabel.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.moDocumentAgencyText, .DocumentAgency)
                    If Not Me.moDocumentAgencyLabel.Text.EndsWith(":") Then Me.moDocumentAgencyLabel.Text &= ":"

                    Me.PopulateControlFromBOProperty(Me.moDocumentIssueDateText, .DocumentIssueDate)
                    If Not Me.moDocumentIssueDateLabel.Text.EndsWith(":") Then Me.moDocumentIssueDateLabel.Text &= ":"

                    AddressCtr.Bind(.AddressChild)

                    If Me.State.AttValueEnableChangingMFG Is Nothing Then
                        Dim oDealer As New Dealer(Me.State.MyBO.Cert.DealerId)
                        Me.State.AttValueEnableChangingMFG = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_ENABLE_CHANGING_MFG_TERM_If_NO_CLAIMS_EXIST_In_PARENT_CHILD).FirstOrDefault
                    End If

                    If Not Me.State.AttValueEnableChangingMFG Is Nothing AndAlso Me.State.AttValueEnableChangingMFG.Value = Codes.YESNO_Y Then
                        Me.State.ClaimCountForParentAndChildCert = CertEndorse.GetClaimCountForParentAndChildCert(.Id)
                    Else
                        Me.State.ClaimCountForParentAndChildCert = 0 ' not applicalble 
                    End If


                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub PopulateCovGrid()
            Me.State.searchDV = CertItemCoverage.GetItemCoveragesWithProdSplitWarr(Me.State.MyBO.Cert.Id)
            Me.grdCoverages.AutoGenerateColumns = False
            If (Me.State.searchDV.Count > 0) Then
                Me.grdCoverages.DataSource = Me.State.searchDV
                Me.grdCoverages.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.grdCoverages)
            End If
        End Sub
        Private Sub PopulateCovFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = Me.grdCoverages.EditIndex
            Try
                With Me.State.MyCovBO

                    If (Not .BeginDate.Equals(Guid.Empty)) Then
                        Dim txtBeginDate As TextBox = CType(Me.grdCoverages.Rows(Me.grdCoverages.EditIndex).Cells(Me.GRID_COL_BEGIN_DATE_IDX).FindControl(Me.BEGIN_DATE_TEXTBOX_CONTROL_NAME), TextBox)
                        Me.PopulateControlFromBOProperty(txtBeginDate, .BeginDate)
                        Dim oBeginDateImage As ImageButton = CType(Me.grdCoverages.Rows(Me.grdCoverages.EditIndex).Cells(Me.GRID_COL_BEGIN_DATE_IDX).FindControl("moBeginDateImage"), ImageButton)
                        If (Not oBeginDateImage Is Nothing) Then
                            Me.AddCalendar(oBeginDateImage, txtBeginDate)
                        End If
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateLangPrefDropdown(ByVal langPrefDropDownList As DropDownList)

            Try

                Dim LanguageListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
                langPrefDropDownList.Populate(LanguageListLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub GetDefaultTerm(Optional blnParentOnly As Boolean = False)

            Try
                Me.State.MyBO.TermPre = NO_TERM
                Me.State.MyBO.TermPos = NO_TERM

                If Me.State.StatemanufaturerWarranty Then
                    For Each cov As CertItemCoverage In Me.State.MyBO.AssociatedItemCoverages(blnParentOnly)
                        If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                            Me.State.MyBO.TermPre = CType(DateDiff(DateInterval.Month, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                            Me.State.MyBO.TermPos = Me.State.MyBO.TermPre
                        End If
                    Next
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Dim strDocumentIssueDate As String
            Try
                With Me.State.MyBO.Cert

                    Me.State.MyBO.TermisDirty = False
                    Me.State.MyBO.ProductSalesDatesisDirty = False
                    Me.State.MyBO.WarrantySalesDatesisDirty = False
                    Me.State.MyBO.NameisDirty = False
                    Me.State.MyBO.SalesPriceisDirty = False
                    Me.State.MyBO.AddressisDirty = False
                    Me.State.MyBO.EmailisDirty = False
                    Me.State.MyBO.HomePhoneisDirty = False
                    Me.State.MyBO.LanguageisDirty = False
                    Me.State.MyBO.WorkPhoneisDirty = False
                    Me.State.MyBO.CovisDirty = False
                    Me.State.MyBO.DocTypeisDirty = False
                    Me.State.MyBO.IDTypeisDirty = False
                    Me.State.MyBO.DocAgencyisDirty = False
                    Me.State.MyBO.DocNumberisDirty = False
                    Me.State.MyBO.RGNumberisDirty = False
                    Me.State.MyBO.DocIssueDateisDirty = False

                    If CType(Me.State.MyBO.TermPos, String) <> Me.TextboxManufacturerTerm.Text Then
                        Me.State.MyBO.TermisDirty = True
                    End If

                    Me.PopulateControlFromBOProperty(Me.txtSaleDate, .ProductSalesDate)
                    If Me.txtSaleDate.Text <> Me.TextboxProductSaleDate.Text Then
                        Me.State.MyBO.ProductSalesDatesisDirty = True
                    End If

                    Me.PopulateControlFromBOProperty(Me.txtSaleDate, .WarrantySalesDate)
                    If Me.txtSaleDate.Text <> Me.TextboxWarrantySalesDate.Text Then
                        Me.State.MyBO.WarrantySalesDatesisDirty = True
                    End If

                    If .CustomerName <> Me.TextboxCustomerName.Text Then
                        Me.State.MyBO.NameisDirty = True
                    End If

                    If .Email <> Me.TextboxEmailAddress.Text Then
                        Me.State.MyBO.EmailisDirty = True
                    End If

                    If .HomePhone <> Me.TextboxHomePhone.Text Then
                        Me.State.MyBO.HomePhoneisDirty = True
                    End If

                    If .WorkPhone <> Me.TextboxWorkPhone.Text Then
                        Me.State.MyBO.WorkPhoneisDirty = True
                    End If

                    If .SalesPrice <> CType(Me.TextboxSalesPrice.Text, Decimal) Then
                        Me.State.MyBO.SalesPriceisDirty = True
                    End If

                    If .getLanguagePrefDesc <> moLangPrefDropdown.SelectedItem.Text Then
                        Me.State.MyBO.LanguageisDirty = True
                    End If

                    Me.AddressCtr.PopulateBOFromControl(True, True)
                    If Not Me.AddressCtr.MyBO.IsDeleted Then
                        If Me.AddressCtr.MyBO.IsDirty Then
                            Me.State.MyBO.AddressisDirty = True
                            Me.State.MyBO.AddressPost.CopyFrom(Me.AddressCtr.MyBO)
                        End If
                    End If

                    If .getDocTypeDesc <> cboDocumentTypeId.SelectedItem.Text Then
                        Me.State.MyBO.DocTypeisDirty = True
                    End If

                    If .IdType <> Me.moIDTypeText.Text Then
                        Me.State.MyBO.IDTypeisDirty = True
                    End If

                    If .DocumentAgency <> Me.moDocumentAgencyText.Text Then
                        Me.State.MyBO.DocAgencyisDirty = True
                    End If

                    If .IdentificationNumber <> Me.moNewTaxIdText.Text Then
                        Me.State.MyBO.DocNumberisDirty = True
                    End If

                    If .RgNumber <> Me.moRGNumberText.Text Then
                        Me.State.MyBO.RGNumberisDirty = True
                    End If
                    If Not .DocumentIssueDate Is Nothing Then
                        strDocumentIssueDate = Me.GetDateFormattedString(CType(.DocumentIssueDate, DateType).Value)
                        If strDocumentIssueDate <> Me.moDocumentIssueDateText.Text Then
                            Me.State.MyBO.DocIssueDateisDirty = True
                        End If
                    Else
                        If Me.moDocumentIssueDateText.Text <> String.Empty Then
                            Me.State.MyBO.DocIssueDateisDirty = True
                        End If
                    End If
                End With

                With Me.State.MyBO
                    If .TermisDirty Or .ProductSalesDatesisDirty Or .WarrantySalesDatesisDirty Or
                        .NameisDirty Or .SalesPriceisDirty Or .LanguageisDirty Or .AddressisDirty Or
                        .EmailisDirty Or .HomePhoneisDirty Or .WorkPhoneisDirty Or
                        .DocTypeisDirty Or .IDTypeisDirty Or .DocAgencyisDirty Or .DocNumberisDirty Or
                        .RGNumberisDirty Or .DocIssueDateisDirty Then
                        .PopulateWithDefaultValues(Me.State.MyBO.Cert.Id, Me.State.StatemanufaturerWarranty)
                        Me.PopulateBOProperty(Me.State.MyBO, "CustNamePost", Me.TextboxCustomerName)
                        Me.PopulateBOProperty(Me.State.MyBO, "TermPos", Me.TextboxManufacturerTerm)
                        Me.PopulateBOProperty(Me.State.MyBO, "ProductSalesDatePost", Me.TextboxProductSaleDate)
                        Me.PopulateBOProperty(Me.State.MyBO, "WarrantySalesDatePost", Me.TextboxWarrantySalesDate)
                        Me.PopulateBOProperty(Me.State.MyBO, "SalesPricePost", Me.TextboxSalesPrice)
                        Me.PopulateBOProperty(Me.State.MyBO, "LangaugeIdPost", Me.moLangPrefDropdown)
                        If Not Me.AddressCtr.MyBO.IsDeleted Then
                            If Me.AddressCtr.MyBO.IsDirty Then
                                Me.PopulateBOProperty(Me.State.MyBO, "AddressIdPost", Me.State.MyBO.AddressPost.Id)
                            Else
                                Me.PopulateBOProperty(Me.State.MyBO, "AddressIdPost", Me.State.MyBO.Cert.AddressId)
                            End If
                        Else
                            Me.PopulateBOProperty(Me.State.MyBO, "AddressIdPost", Me.State.MyBO.Cert.AddressId)
                        End If
                        Me.PopulateBOProperty(Me.State.MyBO, "WorkPhonePost", Me.TextboxWorkPhone)
                        Me.PopulateBOProperty(Me.State.MyBO, "HomePhonePost", Me.TextboxHomePhone)
                        Me.PopulateBOProperty(Me.State.MyBO, "EmailPost", Me.TextboxEmailAddress)

                        Me.PopulateBOProperty(Me.State.MyBO, "DocumentTypeIDPost", Me.cboDocumentTypeId)
                        Me.PopulateBOProperty(Me.State.MyBO, "IdTypePost", Me.moIDTypeText)
                        Me.PopulateBOProperty(Me.State.MyBO, "DocumentAgencyPost", Me.moDocumentAgencyText)
                        Me.PopulateBOProperty(Me.State.MyBO, "TaxIDNumbPost", Me.moNewTaxIdText)
                        Me.PopulateBOProperty(Me.State.MyBO, "RgNumberPost", Me.moRGNumberText)
                        Me.PopulateBOProperty(Me.State.MyBO, "DocumentIssueDatePost", Me.moDocumentIssueDateText)

                    End If
                End With

                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

                If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then

                    If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then

                        Me.CreateNew()
                        Me.PopulateBOsFormFrom()
                        Me.State.MyBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

                    End If

                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_OK Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.New_
                            '        ValidateAndSaveBO()
                            '        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            '        Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            '        ValidateAndSaveBO()
                            '        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            '        Me.CreateNewWithCopy()
                    End Select
                End If
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            Me.MasterPage.MessageController.Clear_Hide()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = Nothing '
            Me.NavController.State = Nothing
            Me.GetDefaultTerm()
            Me.State.MyBO.isECSDurationFix = isECSDurationFix(Me.State.MyBO.Cert.DealerId)
            Me.State.MyBO.BeginEdit()
        End Sub
        Public Function isECSDurationFix(ByVal DealerId As Guid) As Boolean

            '   Dim oContract As Contract = Contract.GetCurrentContract(DealerId)
            Dim oCert As New Certificate(Me.State.MyBO.CertId)
            Dim oContract As Contract = Contract.GetContract(DealerId, oCert.WarrantySalesDate.Value)
            If oContract Is Nothing Then
                Throw New GUIException(ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND, ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
            End If

            Dim oYesList As DataView = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim oYesNo As String = LookupListNew.GetCodeFromId(oYesList, oContract.FixedEscDurationFlag)

            If oYesNo = "N" Then
                Return False
                'Return True
            End If

            Return True
            'Return False

        End Function

        Public Function IsLowestCovStrtDtEqual2PrdSalesDt() As Boolean
            Dim blnFlag As Boolean = False
            blnFlag = CertEndorse.IsLowestCovStrtDtEqual2PrdSalesDt(Me.State.MyBO.CertId)
            Return blnFlag
        End Function

        Private Sub SetCovButtonsState(ByVal bIsEdit As Boolean)

            If (bIsEdit) Then
                ControlMgr.SetVisibleControl(Me, BtnSaveCov_WRITE, True)
                ControlMgr.SetVisibleControl(Me, BtnCancelCov, True)
                ControlMgr.SetEnableControl(Me, btnBack, False)
                ControlMgr.SetEnableControl(Me, btnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            Else
                ControlMgr.SetVisibleControl(Me, BtnSaveCov_WRITE, False)
                ControlMgr.SetVisibleControl(Me, BtnCancelCov, False)
                ControlMgr.SetEnableControl(Me, btnBack, True)
                ControlMgr.SetEnableControl(Me, btnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnEdit_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            End If

            If (Me.State.searchDV.Count > 0) Then
                ControlMgr.SetVisibleControl(Me, pnlCovEdit, True)
            Else
                ControlMgr.SetVisibleControl(Me, pnlCovEdit, False)
            End If

        End Sub

        Private Sub cboDocumentTypeId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocumentTypeId.SelectedIndexChanged
            EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

            Try
                If MyBase.PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                    Me.NavController.Navigate(Me, "back", retObj)
                Else
                    Me.PopulateBOsFormFrom()
                    If Me.State.MyBO.IsDirty And Me.State.MyBO.DealerEndorsementFlagValue <> "S" Then
                        Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                        Me.NavController.Navigate(Me, "back", retObj)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try

        End Sub

        Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Dim ValidSalesPrice As Integer
            Dim objComp As New Company(Me.State.MyBO.Cert.CompanyId)
            Dim strTranferOfOwnership As String
            If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
            Else
                strTranferOfOwnership = String.Empty
            End If
            Try

                Me.CreateNew()
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Or CheckCustomerInfoChanged() Then
                    If strTranferOfOwnership = YES And (Me.State.MyBO.NameisDirty Or Me.State.MyBO.DocTypeisDirty Or Me.State.MyBO.DocNumberisDirty) Then
                        If Me.State.MyBO.CustNamePost Is Nothing Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                        Else
                            If Me.State.MyBO.DocumentTypeIDPost.Equals(Guid.Empty) Then
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                            End If
                            If Me.State.MyBO.TaxIDNumbPost Is Nothing Then
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_NUMBER_REQUIRED)
                            End If
                        End If

                        If Me.State.MyBO.CustNamePre = Me.State.MyBO.CustNamePost Or Me.State.MyBO.TaxIDNumbPost = Me.State.MyBO.TaxIDNumbPre Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CUST_NAME_DOC_NUMBER_ERR)
                        End If
                    End If

                    Me.State.MyBO.Save()
                    If CheckCertUseCustomer() Then
                        Me.State.MyBO.SaveCustomerData(Me.State._moCertificate.CustomerId, Me.moCustomerFirstNameText.Text, Me.moCustomerMiddleNameText.Text, Me.moCustomerLastNameText.Text)
                    End If
                    Me.PopulateFormFromBOs()
                    Me.IsEdit = False
                    Me.EnableDisableFields()

                    If Me.State.MyBO.DealerEndorsementFlagValue <> "Y" Then
                        Me.State.MyBO.EndEdit()
                    End If

                    Me.State.boChanged = True
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

                    If Me.State.MyBO.DealerEndorsementFlagValue = "Y" Then
                        Me.State.MyBO.DealerEndorsementFlagValue = "S"
                    End If

                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = Me.State.MyBO.CertEndorseId
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                If CheckErrors(ex) = False Then
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End If
                'Me.EnableDisableFields()
            End Try
        End Sub

        Public Function CheckCertUseCustomer() As Boolean
            If Not Me.State._moCertificate.CustomerId.Equals(Guid.Empty) Then
                Return True
            End If
            Return False
        End Function
        Public Function CheckCustomerInfoChanged() As Boolean
            If CheckCertUseCustomer() Then
                Dim certobj As New Certificate(Me.State._moCertificate.Id)
                If certobj.CustomerFirstName <> Me.moCustomerFirstNameText.Text Then
                    Me.State._moCertificate = certobj
                    Return True
                End If
                If certobj.CustomerMiddleName <> Me.moCustomerMiddleNameText.Text Then
                    Me.State._moCertificate = certobj
                    Return True
                End If
                If certobj.CustomerLastName <> Me.moCustomerLastNameText.Text Then
                    Me.State._moCertificate = certobj
                    Return True
                End If
                Return False

            End If

        End Function

        Private Function CheckErrors(ByVal ex As Exception) As Boolean
            If Not ex.InnerException Is Nothing Then
                If (ex.InnerException.ToString.Contains("ORA-02290: check constraint (ELITA.CK_BEGIN_END_DATES) violated")) Then
                    Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_INVALID_BEGIN_END_DATES_ERR, True)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                Me.IsEdit = False
                Me.CreateNew()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEdit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                Me.IsEdit = True
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelCov_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelCov.Click
            Try
                Me.State.IsEditGrd = False
                grdCoverages.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.SetGridControls(grdCoverages, True)
                PopulateCovGrid()
                SetCovButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSaveCov_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveCov_WRITE.Click
            Dim oNewEndDate As Date, nIndex As Integer
            Try
                nIndex = Me.grdCoverages.EditIndex
                If nIndex < 0 Then
                    Exit Sub
                End If
                Me.CreateNew()

                Dim txtBeginDate As TextBox = CType(Me.grdCoverages.Rows(nIndex).Cells(Me.GRID_COL_BEGIN_DATE_IDX).FindControl(Me.BEGIN_DATE_TEXTBOX_CONTROL_NAME), TextBox)
                Dim lblEndDate As Label = CType(Me.grdCoverages.Rows(nIndex).Cells(Me.GRID_COL_END_DATE_IDX).FindControl(Me.END_DATE_CONTROL_NAME), Label)
                Me.State.CovId = New Guid(CType(Me.grdCoverages.Rows(nIndex).Cells(Me.GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                Me.State.CovTerm = DateDiff(MONTH, CType(Me.hdOrinBeginDate.Value, Date), CType(lblEndDate.Text, DateType).Value)
                If Trim(txtBeginDate.Text) <> String.Empty Then
                    Me.State.MyBO.getNewBeginDateEditedCertItemCov = CType(txtBeginDate.Text, DateType)
                Else
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR)
                End If

                Me.State.MyBO.getEditedCertItemCovId = Me.State.CovId

                If Not Me.State.MyBO.getNewBeginDateEditedCertItemCov Is Nothing Then
                    'display error
                    If Me.State.MyBO.getNewBeginDateEditedCertItemCov.Value < Me.State.MyBO.Cert.WarrantySalesDate.Value Then
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGINDATE_LESSTHAN_WARRSALESDATE_ERR)

                    End If
                    If Me.State.MyBO.getNewBeginDateEditedCertItemCov.Value > CType(lblEndDate.Text, Date) Then
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                    End If
                End If

                oNewEndDate = (New DateType(DateAdd("d", -1, DateAdd(MONTH, Me.State.CovTerm, Me.State.MyBO.getNewBeginDateEditedCertItemCov.Value)))).Value
                If Me.State.MyBO.LocateActiveClaimsByCovIdClaimLossDate(Me.State.CovId, Me.State.MyBO.getNewBeginDateEditedCertItemCov.Value, oNewEndDate) = True Then
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.EXISTING_CLAIMS_WILL_FALL_OUTSIDE_THE_NEW_DATE_RANGE)
                End If

                If CType(Me.hdOrinBeginDate.Value, Date) <> Me.State.MyBO.getNewBeginDateEditedCertItemCov.Value Then
                    Me.State.MyBO.CovisDirty = True
                End If

                If Me.State.MyBO.CovisDirty Then
                    Me.State.MyBO.PopulateWithDefaultValues(Me.State.MyBO.Cert.Id, Me.State.StatemanufaturerWarranty)
                    Me.PopulateBOProperty(Me.State.MyBO, "AddressIdPost", Me.State.MyBO.Cert.AddressId)
                    Me.State.MyBO.Save()
                    Me.PopulateFormFromBOs()
                    Me.State.MyBO.EndEdit()
                    Me.State.boChanged = True
                    grdCoverages.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                    Me.SetGridControls(grdCoverages, False)
                    PopulateCovGrid()
                    SetCovButtonsState(False)
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = Me.State.MyBO.CertEndorseId
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Error Handling"
#End Region

#Region "Handlers_Grid"

        Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = Me.EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    grdCoverages.EditIndex = nIndex
                    Me.hdOrinBeginDate.Value = String.Empty
                    grdCoverages.SelectedIndex = nIndex
                    Me.State.IsEditGrd = True
                    Me.State.CovId = New Guid(CType(Me.grdCoverages.Rows(nIndex).Cells(Me.GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Dim lblBeginDate As Label = CType(Me.grdCoverages.Rows(nIndex).Cells(Me.GRID_COL_BEGIN_DATE_IDX).FindControl(Me.BEGIN_DATE_CONTROL_NAME), Label)
                    Me.hdOrinBeginDate.Value = (CType(lblBeginDate.Text, DateType)).ToString
                    Me.State.MyCovBO = New CertItemCoverage(Me.State.CovId)
                    PopulateCovGrid()
                    PopulateCovFormFromBO(nIndex)
                    Me.SetGridControls(grdCoverages, False)
                    SetCovButtonsState(True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
#End Region


    End Class
End Namespace