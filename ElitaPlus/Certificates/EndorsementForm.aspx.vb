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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
        Private Const PAR As String = " "
#End Region

#Region "Parameters"
        Public Class Parameters
            Public CertId As Guid
            Public manufaturerWarranty As Boolean = False
            Public Sub New(certid As Guid, Warranty As Boolean)
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
                Return State._IsEdit
            End Get
            Set(Value As Boolean)
                State._IsEdit = Value
            End Set
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State._moCertificate.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value
            End Set
        End Property

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Certificate
            Public HasDataChanged As Boolean
            Public HasMFGCoverageChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, hasDataChanged As Boolean, Optional hasMFGCoverageChanged As Boolean = False)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
                Me.HasMFGCoverageChanged = hasMFGCoverageChanged
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
            Public blnMFGChanged As Boolean = False
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
                Return moAddressControllerEndorsement
            End Get
        End Property

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CertEndorse
                    Me.State.MyBO.CertId = CType(NavController.ParametersPassed, Parameters).CertId
                    'Me.State.MyBO.CertId = Me.State.StateCertId
                    moCertificate = (CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))

                    Me.State.StatemanufaturerWarranty = CType(NavController.ParametersPassed, Parameters).manufaturerWarranty
                    Me.State.companyCode = GetCompanyCode
                    If Me.State.MyBO IsNot Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                    IsEdit = True
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    'Me.State.MyBO = New CertEndorse(CType(Me.CallingParameters, Guid))
                    State.boChanged = False

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            MasterPage.MessageController.Clear_Hide()

            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                UpdateBreadCrum()

                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        CreateNew()
                    End If
                    Trace(Me, "CertEndorse Id=" & GuidControl.GuidToHexString(State.MyBO.CertEndorseId))
                    'Date Calendars
                    AddCalendar(ImageButtonProductSaleDate, TextboxProductSaleDate)
                    AddCalendar(ImageButtonWarrantySaleDate, TextboxWarrantySalesDate)
                    AddCalendar(ImageButtonDocumentIssueDate, moDocumentIssueDateText)

                    ' Me.State.companyCode = GetCompanyCode
                    PopulateDocumentTypeDropdown()
                    PopulateBOsFromCertForm()
                    PopulateFormFromBOs()
                    TranslateGridHeader(grdCoverages)
                    TranslateGridControls(grdCoverages)
                    PopulateCovGrid()
                    SetCovButtonsState(True)
                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                AddressCtr.ReAssignTabIndex(10)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Select Case CalledUrl
                    Case LocateServiceCenterForm.URL
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            Try
                State.IsLowestCovStrtDtEqual2PrdSalesDt = IsLowestCovStrtDtEqual2PrdSalesDt()
                If Not IsEdit Then
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                    ControlMgr.SetEnableControl(Me, btnAdd_WRITE, False)
                    ChangeEnabledProperty(TextboxCustomerName, False)
                    ChangeEnabledProperty(TextboxEmailAddress, False)
                    ChangeEnabledProperty(TextboxWorkPhone, False)
                    ChangeEnabledProperty(TextboxHomePhone, False)
                    ChangeEnabledProperty(moNewTaxIdText, False)
                    ChangeEnabledProperty(moIDTypeText, False)
                    ChangeEnabledProperty(moDocumentAgencyText, False)
                    ChangeEnabledProperty(moDocumentIssueDateText, False)
                    If State.StatemanufaturerWarranty Then
                        ChangeEnabledProperty(TextboxManufacturerTerm, False)
                        ChangeEnabledProperty(TextboxProductSaleDate, False)
                        ChangeEnabledProperty(ImageButtonProductSaleDate, False)
                        ControlMgr.SetVisibleControl(Me, TextboxWarrantySalesDate, False)
                        ControlMgr.SetVisibleControl(Me, LabelWarrantySalesDate, False)
                        ControlMgr.SetVisibleControl(Me, ImageButtonWarrantySaleDate, False)
                        ChangeEnabledProperty(TextboxSalesPrice, False)
                    Else
                        If State.IsLowestCovStrtDtEqual2PrdSalesDt Then
                            ChangeEnabledProperty(TextboxManufacturerTerm, False)
                            ChangeEnabledProperty(TextboxWarrantySalesDate, False)
                            ChangeEnabledProperty(ImageButtonWarrantySaleDate, False)
                            ChangeEnabledProperty(TextboxProductSaleDate, False)
                            ChangeEnabledProperty(ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, TextboxProductSaleDate, True)
                            ControlMgr.SetVisibleControl(Me, LabelProductSaleDate, True)
                            ControlMgr.SetVisibleControl(Me, ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, TextboxWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, LabelWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, ImageButtonWarrantySaleDate, False)
                            ControlMgr.SetVisibleControl(Me, LabelManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, TextboxManufacturerTerm, False)
                        Else
                            ChangeEnabledProperty(TextboxManufacturerTerm, False)
                            ChangeEnabledProperty(TextboxWarrantySalesDate, False)
                            ChangeEnabledProperty(ImageButtonWarrantySaleDate, False)
                            ChangeEnabledProperty(TextboxSalesPrice, False)
                            ChangeEnabledProperty(TextboxProductSaleDate, False)
                            ChangeEnabledProperty(ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, LabelManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, TextboxManufacturerTerm, False)
                            ControlMgr.SetVisibleControl(Me, LabelProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, TextboxProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, ImageButtonProductSaleDate, False)
                            ControlMgr.SetVisibleControl(Me, TextboxWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, LabelWarrantySalesDate, False)
                            ControlMgr.SetVisibleControl(Me, ImageButtonWarrantySaleDate, False)
                        End If
                        ChangeEnabledProperty(TextboxSalesPrice, False)
                    End If
                    ControlMgr.SetVisibleControl(Me, moLangPrefDropdown, False)
                    ControlMgr.SetVisibleControl(Me, TextboxLangPref, True)
                    ControlMgr.SetVisibleControl(Me, moDocumentTypeText, True)
                    ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonDocumentIssueDate, False)
                    ChangeEnabledProperty(moRGNumberText, False)
                    ChangeEnabledProperty(ImageButtonDocumentIssueDate, False)
                    AddressCtr.EnableControls(True)

                    EnableDisableTaxIdControls(moDocumentTypeText.Text.Trim())
                Else 'Edit Mode
                    ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
                    ControlMgr.SetEnableControl(Me, btnAdd_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                    ChangeEnabledProperty(TextboxCustomerName, True)
                    ChangeEnabledProperty(TextboxEmailAddress, True)
                    ChangeEnabledProperty(TextboxWorkPhone, True)
                    ChangeEnabledProperty(TextboxHomePhone, True)

                    If State.StatemanufaturerWarranty Then
                        ChangeEnabledProperty(TextboxManufacturerTerm, True)
                        ChangeEnabledProperty(TextboxProductSaleDate, True)
                        'Me.TextboxProductSaleDate.ReadOnly = True
                        ControlMgr.SetEnableControl(Me, ImageButtonProductSaleDate, True)
                    Else
                        If State.IsLowestCovStrtDtEqual2PrdSalesDt Then
                            ChangeEnabledProperty(TextboxProductSaleDate, True)
                            ControlMgr.SetEnableControl(Me, ImageButtonProductSaleDate, True)
                        Else
                            'Me.ChangeEnabledProperty(Me.TextboxWarrantySalesDate, True)
                            'ControlMgr.SetEnableControl(Me, Me.ImageButtonWarrantySaleDate, True)
                        End If
                    End If
                    ChangeEnabledProperty(TextboxSalesPrice, True)
                    ControlMgr.SetVisibleControl(Me, moLangPrefDropdown, True)
                    ControlMgr.SetVisibleControl(Me, TextboxLangPref, False)
                    ControlMgr.SetVisibleControl(Me, moDocumentTypeText, False)
                    ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, True)
                    ControlMgr.SetVisibleControl(Me, ImageButtonDocumentIssueDate, True)
                    ChangeEnabledProperty(ImageButtonDocumentIssueDate, True)
                    ChangeEnabledProperty(moNewTaxIdText, True)
                    ChangeEnabledProperty(moIDTypeText, True)
                    ChangeEnabledProperty(moDocumentAgencyText, True)
                    ChangeEnabledProperty(moDocumentIssueDateText, True)
                    ChangeEnabledProperty(moRGNumberText, True)

                    AddressCtr.EnableControls(False, True)

                    EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
                End If

                If State.MyBO.AssociatedCertItems.Count > 1 Then
                    ControlMgr.SetVisibleControl(Me, LabelSalesPrice, False)
                    ControlMgr.SetVisibleControl(Me, TextboxSalesPrice, False)
                Else
                    ControlMgr.SetVisibleControl(Me, LabelSalesPrice, True)
                    ControlMgr.SetVisibleControl(Me, TextboxSalesPrice, True)
                End If

                'REQ-1162
                If State.MyBO.DealerTypeCode = Codes.DEALER_TYPES__VSC Then
                    moWarrantyInformation1.Attributes("style") = "display: none"
                    moWarrantyInformation11.Attributes("style") = "display: none"
                    moWarrantyInformation2.Attributes("style") = "display: none"
                    moWarrantyInformation3.Attributes("style") = "display: none"
                    moWarrantyInformation4.Attributes("style") = "display: none"
                Else
                    moWarrantyInformation1.Attributes("style") = ""
                    moWarrantyInformation11.Attributes("style") = ""
                    moWarrantyInformation2.Attributes("style") = ""
                    moWarrantyInformation3.Attributes("style") = ""
                    moWarrantyInformation4.Attributes("style") = ""
                End If

                If CheckCertUseCustomer() Then

                    moCustomerFirstNameLabel.Visible = True
                    moCustomerFirstNameText.Visible = True
                    moCustomerMiddleNameLabel.Visible = True
                    moCustomerMiddleNameText.Visible = True
                    moCustomerLastNameLabel.Visible = True
                    moCustomerLastNameText.Visible = True
                    LabelCustomerName.Visible = False
                    TextboxCustomerName.Visible = False
                    TextboxCustomerName.ReadOnly = True

                Else
                    LabelCustomerName.Enabled = True
                    TextboxCustomerName.Visible = True
                End If

                If State.ClaimCountForParentAndChildCert > 0 Then
                    ChangeEnabledProperty(TextboxManufacturerTerm, False)
                    ControlMgr.SetVisibleControl(Me, LabelClaimsExist, True)
                Else
                    ChangeEnabledProperty(TextboxManufacturerTerm, True)
                    ControlMgr.SetVisibleControl(Me, LabelClaimsExist, False)
                End If
                btnBack.Enabled = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()

            Try
                BindBOPropertyToLabel(State.MyBO, "CustNamePost", LabelCustomerName)
                BindBOPropertyToLabel(State.MyBO, "ProductSalesDatePost", LabelProductSaleDate)
                BindBOPropertyToLabel(State.MyBO, "WarrantySalesDatePost", LabelWarrantySalesDate)
                BindBOPropertyToLabel(State.MyBO, "TermPos", LabelManufacturerTerm)
                BindBOPropertyToLabel(State.MyBO, "SalesPrice", LabelSalesPrice)
                BindBOPropertyToLabel(State.MyBO, "LangaugeIdPost", LabelLangPref)
                BindBOPropertyToLabel(State.MyBO, "EmailPost", LabelEmailAddress)
                BindBOPropertyToLabel(State.MyBO, "HomePhonePost", LabelHomePhone)
                BindBOPropertyToLabel(State.MyBO, "WorkPhonePost", LabelWorkPhone)

                BindBOPropertyToLabel(State.MyBO, "DocumentIssueDatePost", moDocumentIssueDateLabel)
                BindBOPropertyToLabel(State.MyBO, "DocumentTypeIDPost", moDocumentTypeLabel)
                BindBOPropertyToLabel(State.MyBO, "RgNumberPost", moRGNumberLabel)
                BindBOPropertyToLabel(State.MyBO, "DocumentAgencyPost", moDocumentAgencyLabel)
                BindBOPropertyToLabel(State.MyBO, "IdTypePost", moIDTypeLabel)
                BindBOPropertyToLabel(State.MyBO, "TaxIDNumbPost", moNewTaxIdLabel)

                ClearGridHeadersAndLabelsErrSign()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub EnableDisableTaxIdControls(docType As String)

            If docType = "CNPJ" Then
                ControlMgr.SetEnableControl(Me, moIDTypeText, False)
                ControlMgr.SetEnableControl(Me, moDocumentAgencyText, False)
                ControlMgr.SetEnableControl(Me, moRGNumberText, False)
                ControlMgr.SetEnableControl(Me, moDocumentIssueDateText, False)
                ControlMgr.SetEnableControl(Me, ImageButtonDocumentIssueDate, False)
                moIDTypeText.Text = String.Empty
                moDocumentAgencyText.Text = String.Empty
                moDocumentIssueDateText.Text = String.Empty
                moRGNumberText.Text = String.Empty
            Else
                ControlMgr.SetEnableControl(Me, moIDTypeText, True)
                ControlMgr.SetEnableControl(Me, moDocumentAgencyText, True)
                ControlMgr.SetEnableControl(Me, moRGNumberText, True)
                ControlMgr.SetEnableControl(Me, moDocumentIssueDateText, True)
                ControlMgr.SetEnableControl(Me, ImageButtonDocumentIssueDate, True)

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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub PopulateBOsFromCertForm()
            Try
                State.MyBO.Cert.CustomerName = moCertificate.CustomerName
                State.MyBO.Cert.Email = moCertificate.Email
                State.MyBO.Cert.HomePhone = moCertificate.HomePhone
                State.MyBO.Cert.WorkPhone = moCertificate.WorkPhone
                State.MyBO.Cert.ProductSalesDate = moCertificate.ProductSalesDate
                State.MyBO.Cert.WarrantySalesDate = moCertificate.WarrantySalesDate
                State.MyBO.Cert.SalesPrice = moCertificate.SalesPrice
                State.MyBO.Cert.LanguageId = moCertificate.LanguageId
                State.MyBO.Cert.DocumentTypeID = moCertificate.DocumentTypeID
                State.MyBO.Cert.TaxIDNumb = moCertificate.TaxIDNumb
                State.MyBO.Cert.IdType = moCertificate.IdType
                State.MyBO.Cert.RgNumber = moCertificate.RgNumber
                State.MyBO.Cert.DocumentAgency = moCertificate.DocumentAgency
                State.MyBO.Cert.DocumentIssueDate = moCertificate.DocumentIssueDate
                State.MyBO.Cert.AddressChild.Address1 = moCertificate.AddressChild.Address1
                State.MyBO.Cert.AddressChild.Address2 = moCertificate.AddressChild.Address2
                State.MyBO.Cert.AddressChild.Address3 = moCertificate.AddressChild.Address3
                State.MyBO.Cert.AddressChild.CountryId = moCertificate.AddressChild.CountryId
                State.MyBO.Cert.AddressChild.RegionId = moCertificate.AddressChild.RegionId
                State.MyBO.Cert.AddressChild.City = moCertificate.AddressChild.City
                State.MyBO.Cert.AddressChild.PostalCode = moCertificate.AddressChild.PostalCode
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub PopulateFormFromBOs()

            Try
                GetDefaultTerm(True)
                PopulateLangPrefDropdown(moLangPrefDropdown)

                moCertificateInfoController = UserCertificateCtr
                moCertificateInfoController.InitController(State.MyBO.Cert.Id, , State.companyCode)

                With State.MyBO.Cert
                    If Not LabelManufacturerTerm.Text.EndsWith(":") Then LabelManufacturerTerm.Text &= ":"
                    PopulateControlFromBOProperty(TextboxManufacturerTerm, State.MyBO.TermPre)

                    If CheckCertUseCustomer() Then
                        PopulateControlFromBOProperty(moCustomerFirstNameText, .CustomerFirstName)
                        PopulateControlFromBOProperty(moCustomerMiddleNameText, .CustomerMiddleName)
                        PopulateControlFromBOProperty(moCustomerLastNameText, .CustomerLastName)
                        If Not moCustomerFirstNameLabel.Text.EndsWith(":") Then moCustomerFirstNameLabel.Text &= ":"
                        If Not moCustomerMiddleNameLabel.Text.EndsWith(":") Then moCustomerMiddleNameLabel.Text &= ":"
                        If Not moCustomerLastNameLabel.Text.EndsWith(":") Then moCustomerLastNameLabel.Text &= ":"
                    Else
                        PopulateControlFromBOProperty(TextboxCustomerName, .CustomerName)
                        If Not LabelCustomerName.Text.EndsWith(":") Then LabelCustomerName.Text &= ":"
                    End If

                    PopulateControlFromBOProperty(TextboxProductSaleDate, .ProductSalesDate)
                    If Not LabelProductSaleDate.Text.EndsWith(":") Then LabelProductSaleDate.Text &= ":"

                    PopulateControlFromBOProperty(TextboxWarrantySalesDate, .WarrantySalesDate)
                    If Not LabelWarrantySalesDate.Text.EndsWith(":") Then LabelWarrantySalesDate.Text &= ":"

                    PopulateControlFromBOProperty(TextboxSalesPrice, .SalesPrice)
                    If Not LabelSalesPrice.Text.EndsWith(":") Then LabelSalesPrice.Text &= ":"

                    If Not (.LanguageId.Equals(Guid.Empty)) Then
                        SetSelectedItem(moLangPrefDropdown, .LanguageId)
                        PopulateControlFromBOProperty(TextboxLangPref, .getLanguagePrefDesc)
                    End If
                    If Not LabelLangPref.Text.EndsWith(":") Then LabelLangPref.Text &= ":"

                    PopulateControlFromBOProperty(TextboxWarrantySalesDate, .WarrantySalesDate)
                    If Not LabelWarrantySalesDate.Text.EndsWith(":") Then LabelWarrantySalesDate.Text &= ":"

                    PopulateControlFromBOProperty(TextboxEmailAddress, .Email)
                    If Not LabelEmailAddress.Text.EndsWith(":") Then LabelEmailAddress.Text &= ":"

                    PopulateControlFromBOProperty(TextboxHomePhone, .HomePhone)
                    If Not LabelHomePhone.Text.EndsWith(":") Then LabelHomePhone.Text &= ":"

                    PopulateControlFromBOProperty(TextboxWorkPhone, .WorkPhone)
                    If Not LabelWorkPhone.Text.EndsWith(":") Then LabelWorkPhone.Text &= ":"

                    If Not moDocumentTypeLabel.Text.EndsWith(":") Then moDocumentTypeLabel.Text &= ":"
                    If Not (.DocumentTypeID.Equals(Guid.Empty)) Then
                        SetSelectedItem(cboDocumentTypeId, .DocumentTypeID)
                        PopulateControlFromBOProperty(moDocumentTypeText, .getDocTypeDesc)
                    End If

                    PopulateControlFromBOProperty(moNewTaxIdText, .TaxIDNumb)
                    If Not moNewTaxIdLabel.Text.EndsWith(":") Then moNewTaxIdLabel.Text &= ":"

                    PopulateControlFromBOProperty(moIDTypeText, .IdType)
                    If Not moIDTypeLabel.Text.EndsWith(":") Then moIDTypeLabel.Text &= ":"

                    PopulateControlFromBOProperty(moRGNumberText, .RgNumber)
                    If Not moRGNumberLabel.Text.EndsWith(":") Then moRGNumberLabel.Text &= ":"

                    PopulateControlFromBOProperty(moDocumentAgencyText, .DocumentAgency)
                    If Not moDocumentAgencyLabel.Text.EndsWith(":") Then moDocumentAgencyLabel.Text &= ":"

                    PopulateControlFromBOProperty(moDocumentIssueDateText, .DocumentIssueDate)
                    If Not moDocumentIssueDateLabel.Text.EndsWith(":") Then moDocumentIssueDateLabel.Text &= ":"

                    AddressCtr.Bind(.AddressChild)

                    If State.AttValueEnableChangingMFG Is Nothing Then
                        Dim oDealer As New Dealer(State.MyBO.Cert.DealerId)
                        State.AttValueEnableChangingMFG = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_ENABLE_CHANGING_MFG_TERM_If_NO_CLAIMS_EXIST_In_PARENT_CHILD).FirstOrDefault
                    End If

                    If State.AttValueEnableChangingMFG IsNot Nothing AndAlso State.AttValueEnableChangingMFG.Value = Codes.YESNO_Y Then
                        State.ClaimCountForParentAndChildCert = CertEndorse.GetClaimCountForParentAndChildCert(.Id)
                    Else
                        State.ClaimCountForParentAndChildCert = 0 ' not applicalble 
                    End If


                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub PopulateCovGrid()
            State.searchDV = CertItemCoverage.GetItemCoveragesWithProdSplitWarr(State.MyBO.Cert.Id)
            grdCoverages.AutoGenerateColumns = False
            If (State.searchDV.Count > 0) Then
                grdCoverages.DataSource = State.searchDV
                grdCoverages.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdCoverages)
            End If
        End Sub
        Private Sub PopulateCovFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = grdCoverages.EditIndex
            Try
                With State.MyCovBO

                    If (Not .BeginDate.Equals(Guid.Empty)) Then
                        Dim txtBeginDate As TextBox = CType(grdCoverages.Rows(grdCoverages.EditIndex).Cells(GRID_COL_BEGIN_DATE_IDX).FindControl(BEGIN_DATE_TEXTBOX_CONTROL_NAME), TextBox)
                        PopulateControlFromBOProperty(txtBeginDate, .BeginDate)
                        Dim oBeginDateImage As ImageButton = CType(grdCoverages.Rows(grdCoverages.EditIndex).Cells(GRID_COL_BEGIN_DATE_IDX).FindControl("moBeginDateImage"), ImageButton)
                        If (oBeginDateImage IsNot Nothing) Then
                            AddCalendar(oBeginDateImage, txtBeginDate)
                        End If
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateLangPrefDropdown(langPrefDropDownList As DropDownList)

            Try

                Dim LanguageListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
                langPrefDropDownList.Populate(LanguageListLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub GetDefaultTerm(Optional blnParentOnly As Boolean = False)

            Try
                State.MyBO.TermPre = NO_TERM
                State.MyBO.TermPos = NO_TERM

                If State.StatemanufaturerWarranty Then
                    For Each cov As CertItemCoverage In State.MyBO.AssociatedItemCoverages(blnParentOnly)
                        If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                            State.MyBO.TermPre = CType(DateDiff(DateInterval.Month, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                            State.MyBO.TermPos = State.MyBO.TermPre
                        End If
                    Next
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Dim strDocumentIssueDate As String
            Try
                With State.MyBO.Cert

                    State.MyBO.TermisDirty = False
                    State.MyBO.ProductSalesDatesisDirty = False
                    State.MyBO.WarrantySalesDatesisDirty = False
                    State.MyBO.NameisDirty = False
                    State.MyBO.SalesPriceisDirty = False
                    State.MyBO.AddressisDirty = False
                    State.MyBO.EmailisDirty = False
                    State.MyBO.HomePhoneisDirty = False
                    State.MyBO.LanguageisDirty = False
                    State.MyBO.WorkPhoneisDirty = False
                    State.MyBO.CovisDirty = False
                    State.MyBO.DocTypeisDirty = False
                    State.MyBO.IDTypeisDirty = False
                    State.MyBO.DocAgencyisDirty = False
                    State.MyBO.DocNumberisDirty = False
                    State.MyBO.RGNumberisDirty = False
                    State.MyBO.DocIssueDateisDirty = False

                    If CType(State.MyBO.TermPos, String) <> TextboxManufacturerTerm.Text Then
                        State.MyBO.TermisDirty = True
                        State.blnMFGChanged = True

                    End If

                    PopulateControlFromBOProperty(txtSaleDate, .ProductSalesDate)
                    If txtSaleDate.Text <> TextboxProductSaleDate.Text Then
                        State.MyBO.ProductSalesDatesisDirty = True
                    End If

                    PopulateControlFromBOProperty(txtSaleDate, .WarrantySalesDate)
                    If txtSaleDate.Text <> TextboxWarrantySalesDate.Text Then
                        State.MyBO.WarrantySalesDatesisDirty = True
                    End If

                    If CheckCertUseCustomer() Then
                        If (.CustomerFirstName <> moCustomerFirstNameText.Text) Or (.CustomerMiddleName <> moCustomerMiddleNameText.Text) Or (.CustomerLastName <> moCustomerLastNameText.Text) Then
                            State.MyBO.NameisDirty = True
                        End If
                    Else
                        If .CustomerName <> TextboxCustomerName.Text Then
                            State.MyBO.NameisDirty = True
                        End If
                    End If

                    If .Email <> TextboxEmailAddress.Text Then
                        State.MyBO.EmailisDirty = True
                    End If

                    If .HomePhone <> TextboxHomePhone.Text Then
                        State.MyBO.HomePhoneisDirty = True
                    End If

                    If .WorkPhone <> TextboxWorkPhone.Text Then
                        State.MyBO.WorkPhoneisDirty = True
                    End If

                    If .SalesPrice <> CType(TextboxSalesPrice.Text, Decimal) Then
                        State.MyBO.SalesPriceisDirty = True
                    End If

                    If .getLanguagePrefDesc <> moLangPrefDropdown.SelectedItem.Text Then
                        State.MyBO.LanguageisDirty = True
                    End If

                    AddressCtr.PopulateBOFromControl(True, True)
                    If Not AddressCtr.MyBO.IsDeleted Then
                        If AddressCtr.MyBO.IsDirty Then
                            State.MyBO.AddressisDirty = True
                            State.MyBO.AddressPost.CopyFrom(AddressCtr.MyBO)
                        End If
                    End If

                    If .getDocTypeDesc <> cboDocumentTypeId.SelectedItem.Text Then
                        State.MyBO.DocTypeisDirty = True
                    End If

                    If .IdType <> moIDTypeText.Text Then
                        State.MyBO.IDTypeisDirty = True
                    End If

                    If .DocumentAgency <> moDocumentAgencyText.Text Then
                        State.MyBO.DocAgencyisDirty = True
                    End If

                    If .IdentificationNumber <> moNewTaxIdText.Text Then
                        State.MyBO.DocNumberisDirty = True
                    End If

                    If .RgNumber <> moRGNumberText.Text Then
                        State.MyBO.RGNumberisDirty = True
                    End If
                    If .DocumentIssueDate IsNot Nothing Then
                        strDocumentIssueDate = GetDateFormattedString(CType(.DocumentIssueDate, DateType).Value)
                        If strDocumentIssueDate <> moDocumentIssueDateText.Text Then
                            State.MyBO.DocIssueDateisDirty = True
                        End If
                    Else
                        If moDocumentIssueDateText.Text <> String.Empty Then
                            State.MyBO.DocIssueDateisDirty = True
                        End If
                    End If
                End With

                With State.MyBO
                    If .TermisDirty Or .ProductSalesDatesisDirty Or .WarrantySalesDatesisDirty Or
                        .SalesPriceisDirty Or .LanguageisDirty Or .AddressisDirty Or
                        .EmailisDirty Or .HomePhoneisDirty Or .WorkPhoneisDirty Or
                        .DocTypeisDirty Or .IDTypeisDirty Or .DocAgencyisDirty Or .DocNumberisDirty Or
                        .RGNumberisDirty Or .DocIssueDateisDirty Then
                        .PopulateWithDefaultValues(State.MyBO.Cert.Id, State.StatemanufaturerWarranty)
                        PopulateBOProperty(State.MyBO, "TermPos", TextboxManufacturerTerm)
                        PopulateBOProperty(State.MyBO, "ProductSalesDatePost", TextboxProductSaleDate)
                        PopulateBOProperty(State.MyBO, "WarrantySalesDatePost", TextboxWarrantySalesDate)
                        PopulateBOProperty(State.MyBO, "SalesPricePost", TextboxSalesPrice)
                        PopulateBOProperty(State.MyBO, "LangaugeIdPost", moLangPrefDropdown)
                        If Not AddressCtr.MyBO.IsDeleted Then
                            If AddressCtr.MyBO.IsDirty Then
                                PopulateBOProperty(State.MyBO, "AddressIdPost", State.MyBO.AddressPost.Id)
                            Else
                                PopulateBOProperty(State.MyBO, "AddressIdPost", State.MyBO.Cert.AddressId)
                            End If
                        Else
                            PopulateBOProperty(State.MyBO, "AddressIdPost", State.MyBO.Cert.AddressId)
                        End If
                        PopulateBOProperty(State.MyBO, "WorkPhonePost", TextboxWorkPhone)
                        PopulateBOProperty(State.MyBO, "HomePhonePost", TextboxHomePhone)
                        PopulateBOProperty(State.MyBO, "EmailPost", TextboxEmailAddress)

                        PopulateBOProperty(State.MyBO, "DocumentTypeIDPost", cboDocumentTypeId)
                        PopulateBOProperty(State.MyBO, "IdTypePost", moIDTypeText)
                        PopulateBOProperty(State.MyBO, "DocumentAgencyPost", moDocumentAgencyText)
                        PopulateBOProperty(State.MyBO, "TaxIDNumbPost", moNewTaxIdText)
                        PopulateBOProperty(State.MyBO, "RgNumberPost", moRGNumberText)
                        PopulateBOProperty(State.MyBO, "DocumentIssueDatePost", moDocumentIssueDateText)
                        If moCustomerFirstNameText.Visible = True And moCustomerMiddleNameText.Visible = True And moCustomerLastNameText.Visible = True Then
                            PopulateBOProperty(State.MyBO, "CustNamePost", moCustomerFirstNameText.Text + " " + moCustomerMiddleNameText.Text + " " + moCustomerLastNameText.Text)
                        ElseIf TextboxCustomerName.Visible = True Then
                            PopulateBOProperty(State.MyBO, "CustNamePost", TextboxCustomerName)
                        End If

                    End If
                End With

                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then

                    If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then

                        CreateNew()
                        PopulateBOsFormFrom()
                        State.MyBO.Save()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

                    End If

                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_OK Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                            NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
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
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            MasterPage.MessageController.Clear_Hide()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = Nothing '
            NavController.State = Nothing
            GetDefaultTerm()
            State.MyBO.isECSDurationFix = isECSDurationFix(State.MyBO.Cert.DealerId)
            State.MyBO.BeginEdit()
        End Sub
        Public Function isECSDurationFix(DealerId As Guid) As Boolean

            '   Dim oContract As Contract = Contract.GetCurrentContract(DealerId)
            Dim oCert As New Certificate(State.MyBO.CertId)
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
            blnFlag = CertEndorse.IsLowestCovStrtDtEqual2PrdSalesDt(State.MyBO.CertId)
            Return blnFlag
        End Function

        Private Sub SetCovButtonsState(bIsEdit As Boolean)

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

            If (State.searchDV.Count > 0) Then
                ControlMgr.SetVisibleControl(Me, pnlCovEdit, True)
            Else
                ControlMgr.SetVisibleControl(Me, pnlCovEdit, False)
            End If

        End Sub

        Private Sub cboDocumentTypeId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDocumentTypeId.SelectedIndexChanged
            EnableDisableTaxIdControls(cboDocumentTypeId.SelectedItem.Text)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click

            Try
                If MyBase.PagePermissionType = FormAuthorization.enumPermissionType.VIEWONLY Then
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                    NavController.Navigate(Me, "back", retObj)
                Else
                    PopulateBOsFormFrom()
                    If State.MyBO.IsDirty And State.MyBO.DealerEndorsementFlagValue <> "S" Then
                        DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged, State.blnMFGChanged)
                        NavController.Navigate(Me, "back", retObj)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try

        End Sub

        Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Dim ValidSalesPrice As Integer
            Dim objComp As New Company(State.MyBO.Cert.CompanyId)
            Dim strTranferOfOwnership As String
            If Not objComp.UseTransferOfOwnership.Equals(Guid.Empty) Then
                strTranferOfOwnership = LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), objComp.UseTransferOfOwnership)
            Else
                strTranferOfOwnership = String.Empty
            End If
            Try

                CreateNew()
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Or CheckCustomerInfoChanged() Then
                    If strTranferOfOwnership = YES And (State.MyBO.NameisDirty Or State.MyBO.DocTypeisDirty Or State.MyBO.DocNumberisDirty) Then
                        If State.MyBO.CustNamePost Is Nothing Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_CUSTOMER_NAME_ERR)
                        Else
                            If State.MyBO.DocumentTypeIDPost.Equals(Guid.Empty) Then
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
                            End If
                            If State.MyBO.TaxIDNumbPost Is Nothing Then
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DOCUMENT_NUMBER_REQUIRED)
                            End If
                        End If

                        If State.MyBO.CustNamePre = State.MyBO.CustNamePost Or State.MyBO.TaxIDNumbPost = State.MyBO.TaxIDNumbPre Then
                            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CUST_NAME_DOC_NUMBER_ERR)
                        End If
                    End If

                    State.MyBO.Save()
                    If CheckCertUseCustomer() Then
                        State.MyBO.SaveCustomerData(State._moCertificate.CustomerId, moCustomerFirstNameText.Text, moCustomerMiddleNameText.Text, moCustomerLastNameText.Text)
                    End If
                    PopulateFormFromBOs()
                    IsEdit = False
                    EnableDisableFields()

                    If State.MyBO.DealerEndorsementFlagValue <> "Y" Then
                        State.MyBO.EndEdit()
                    End If

                    State.boChanged = True
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

                    If State.MyBO.DealerEndorsementFlagValue = "Y" Then
                        State.MyBO.DealerEndorsementFlagValue = "S"
                    End If

                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = State.MyBO.CertEndorseId
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                If CheckErrors(ex) = False Then
                    HandleErrors(ex, MasterPage.MessageController)
                End If
                'Me.EnableDisableFields()
            End Try
        End Sub

        Public Function CheckCertUseCustomer() As Boolean
            If Not State._moCertificate.CustomerId.Equals(Guid.Empty) Then
                Return True
            End If
            Return False
        End Function
        Public Function CheckCustomerInfoChanged() As Boolean
            If CheckCertUseCustomer() Then
                Dim certobj As New Certificate(State._moCertificate.Id)
                If certobj.CustomerFirstName <> moCustomerFirstNameText.Text Then
                    State._moCertificate = certobj
                    Return True
                End If
                If certobj.CustomerMiddleName <> moCustomerMiddleNameText.Text Then
                    State._moCertificate = certobj
                    Return True
                End If
                If certobj.CustomerLastName <> moCustomerLastNameText.Text Then
                    State._moCertificate = certobj
                    Return True
                End If
                Return False

            End If

        End Function

        Private Function CheckErrors(ex As Exception) As Boolean
            If ex.InnerException IsNot Nothing Then
                If (ex.InnerException.ToString.Contains("ORA-02290: check constraint (ELITA.CK_BEGIN_END_DATES) violated")) Then
                    MasterPage.MessageController.AddErrorAndShow(Message.MSG_INVALID_BEGIN_END_DATES_ERR, True)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        Private Sub btnUndo_Write_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                IsEdit = False
                CreateNew()
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnEdit_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                IsEdit = True
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelCov_Click(sender As Object, e As System.EventArgs) Handles BtnCancelCov.Click
            Try
                State.IsEditGrd = False
                grdCoverages.EditIndex = NO_ITEM_SELECTED_INDEX
                SetGridControls(grdCoverages, True)
                PopulateCovGrid()
                SetCovButtonsState(False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSaveCov_WRITE_Click(sender As Object, e As System.EventArgs) Handles BtnSaveCov_WRITE.Click
            Dim oNewEndDate As Date, nIndex As Integer
            Try
                nIndex = grdCoverages.EditIndex
                If nIndex < 0 Then
                    Exit Sub
                End If
                CreateNew()

                Dim txtBeginDate As TextBox = CType(grdCoverages.Rows(nIndex).Cells(GRID_COL_BEGIN_DATE_IDX).FindControl(BEGIN_DATE_TEXTBOX_CONTROL_NAME), TextBox)
                Dim lblEndDate As Label = CType(grdCoverages.Rows(nIndex).Cells(GRID_COL_END_DATE_IDX).FindControl(END_DATE_CONTROL_NAME), Label)
                State.CovId = New Guid(CType(grdCoverages.Rows(nIndex).Cells(GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl(ID_CONTROL_NAME), Label).Text)
                State.CovTerm = DateDiff(MONTH, CType(hdOrinBeginDate.Value, Date), CType(lblEndDate.Text, DateType).Value)
                If Trim(txtBeginDate.Text) <> String.Empty Then
                    State.MyBO.getNewBeginDateEditedCertItemCov = CType(txtBeginDate.Text, DateType)
                Else
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR)
                End If

                State.MyBO.getEditedCertItemCovId = State.CovId

                If State.MyBO.getNewBeginDateEditedCertItemCov IsNot Nothing Then
                    'display error
                    If State.MyBO.getNewBeginDateEditedCertItemCov.Value < State.MyBO.Cert.WarrantySalesDate.Value Then
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGINDATE_LESSTHAN_WARRSALESDATE_ERR)

                    End If
                    If State.MyBO.getNewBeginDateEditedCertItemCov.Value > CType(lblEndDate.Text, Date) Then
                        Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                    End If
                End If

                oNewEndDate = (New DateType(DateAdd("d", -1, DateAdd(MONTH, State.CovTerm, State.MyBO.getNewBeginDateEditedCertItemCov.Value)))).Value
                If State.MyBO.LocateActiveClaimsByCovIdClaimLossDate(State.CovId, State.MyBO.getNewBeginDateEditedCertItemCov.Value, oNewEndDate) = True Then
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.EXISTING_CLAIMS_WILL_FALL_OUTSIDE_THE_NEW_DATE_RANGE)
                End If

                If CType(hdOrinBeginDate.Value, Date) <> State.MyBO.getNewBeginDateEditedCertItemCov.Value Then
                    State.MyBO.CovisDirty = True
                End If

                If State.MyBO.CovisDirty Then
                    State.MyBO.PopulateWithDefaultValues(State.MyBO.Cert.Id, State.StatemanufaturerWarranty)
                    PopulateBOProperty(State.MyBO, "AddressIdPost", State.MyBO.Cert.AddressId)
                    State.MyBO.Save()
                    PopulateFormFromBOs()
                    State.MyBO.EndEdit()
                    State.boChanged = True
                    grdCoverages.EditIndex = NO_ITEM_SELECTED_INDEX
                    SetGridControls(grdCoverages, False)
                    PopulateCovGrid()
                    SetCovButtonsState(False)
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ENDORSE_ID) = State.MyBO.CertEndorseId
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Error Handling"
#End Region

#Region "Handlers_Grid"

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    grdCoverages.EditIndex = nIndex
                    hdOrinBeginDate.Value = String.Empty
                    grdCoverages.SelectedIndex = nIndex
                    State.IsEditGrd = True
                    State.CovId = New Guid(CType(grdCoverages.Rows(nIndex).Cells(GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl(ID_CONTROL_NAME), Label).Text)
                    Dim lblBeginDate As Label = CType(grdCoverages.Rows(nIndex).Cells(GRID_COL_BEGIN_DATE_IDX).FindControl(BEGIN_DATE_CONTROL_NAME), Label)
                    hdOrinBeginDate.Value = (CType(lblBeginDate.Text, DateType)).ToString
                    State.MyCovBO = New CertItemCoverage(State.CovId)
                    PopulateCovGrid()
                    PopulateCovFormFromBO(nIndex)
                    SetGridControls(grdCoverages, False)
                    SetCovButtonsState(True)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
#End Region


    End Class
End Namespace