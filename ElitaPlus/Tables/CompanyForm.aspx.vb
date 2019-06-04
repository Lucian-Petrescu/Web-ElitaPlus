Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization

Partial Class CompanyForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents moAddressController As UserControlAddress
    Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
    Protected WithEvents UserControlAvailableSelectedCountries As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
    'Protected WithEvents PostalCodeFormatLists As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.SrcDstListChooser

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
    Public Const URL As String = "CompanyForm.aspx"
    Public Const AVAILABLE_COUNTRIES As String = "AVAILABLE_COUNTRIES"
    Public Const SELECTED_COUNTRIES As String = "SELECTED_COUNTRIES"
    Private Const ADDL_DAC_NONE As String = "NONE"
    Private Const DEALER_WO_AGENTS As String = "dealer_wo_agent"
    Private Const LBL_TABLES As String = "table"
    Private Const LBL_COMPANY As String = "COMPANY"

#End Region

#Region "Properties"

    'Public ReadOnly Property AddressCtr() As UserControlAddress
    '    Get
    '        Return moAddressController
    '    End Get
    'End Property

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Company
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Company, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As Company
        Public ScreenSnapShotBO As Company
        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public year As String
        Public CmpnId As Guid
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public blnIsComingFromCopy As Boolean = False
        Public OldCompanyId As Guid = Guid.Empty
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Company(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Me.ErrorCtrl.Clear_Hide() ' REQ-1295
        Me.MasterPage.MessageController.Clear_Hide() ' REQ-1295
        'hide the user control...since we are doing our ownlist.
        'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
        Try
            If Not Me.IsPostBack Then

                'REQ-1295
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_TABLES)
                Me.AddCalendar_New(Me.ImgUniqueCertNumberEffDate, Me.TextboxUniqueCertNumberEffDate)
                UpdateBreadCrum()
                'REQ-1295 : Changes Completed 

                'Date Calendars
                Me.MenuEnabled = False
                ' Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Company
                End If
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                AttributeValues.TranslateHeaders()
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Else
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    'REQ-1295
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(LBL_COMPANY)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_COMPANY)
        End If
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        Me.ChangeEnabledProperty(Me.TextboxDescription, True)
        Me.ChangeEnabledProperty(Me.TextboxCode, True)
        Me.ChangeEnabledProperty(Me.TextboxTaxIdNumber, True)
        Me.ChangeEnabledProperty(Me.TextboxFax, True)
        Me.ChangeEnabledProperty(Me.TextboxPhone, True)
        Me.ChangeEnabledProperty(Me.TextboxEmail, True)
        Me.ChangeEnabledProperty(Me.TextboxRefundToleranceAmount, True)
        Me.ChangeEnabledProperty(Me.TextboxMaxFollowupDays, True)
        Me.ChangeEnabledProperty(Me.TextboxDefaultFollowupDays, True)
        Me.ChangeEnabledProperty(Me.TextboxLegalDisclaimer, True)
        Me.ChangeEnabledProperty(Me.txtSCPreINVWP, True)

        EnableDisableUniqueCertFields()

        If (Me.State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
            'ControlMgr.SetVisibleControl(Me, LabelSCPreInvWP, True)
            'ControlMgr.SetVisibleControl(Me, txtSCPreINVWP, True)
            LabelSCPreInvWP.Style.Add("display", "block")
            txtSCPreINVWP.Style.Add("display", "block")
        Else
            'ControlMgr.SetVisibleControl(Me, LabelSCPreInvWP, False)
            'ControlMgr.SetVisibleControl(Me, txtSCPreINVWP, False)
            LabelSCPreInvWP.Style.Add("display", "none")
            txtSCPreINVWP.Style.Add("display", "none")
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIdNumber", Me.LabelTaxIdNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BusinessCountryId", Me.LabelBusinessCountry)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Phone", Me.LabelPhone1)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Fax", Me.LabelFax)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Email", Me.LabelEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RefundToleranceAmt", Me.LabelRefundToleranceAmt)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LanguageId", Me.LabelLanguage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceMethodId", Me.LabelInvoiceMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimNumberFormatId", Me.LabelClaimNumbFormatId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CertNumberFormatId", Me.LabelCertNumberFormat)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UsePreInvoiceProcessId", Me.LabelUsePreInvProcess)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxFollowupDays", Me.LabelMaxFollowupDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DefaultFollowupDays", Me.LabelDefaultFollowupDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LegalDisclaimer", Me.LabelLegalDisclaimer)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SalutationId", Me.LabelSalutation)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "Address1", Me.Address1Label)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Address2", Me.Address2Label)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "City", Me.CityLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RegionId", Me.StateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PostalCode", Me.ZipLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CountryId", Me.CountryLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyGroupId", Me.LabelCompanyGroup)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyTypeId", Me.LabelComapnyType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UPR_USES_WP_Id", Me.LabelUPR_USES_WP)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MasterClaimProcessingId", Me.labelMasterClaim)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceOrdersByDealerId", Me.lblSvcOrdersByDealer)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RequireItemDescriptionId", Me.lblRequireItemDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimNumberOffset", Me.LabelCLAIM_NUMBER_OFFSET)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UseZipDistrictId", Me.LabelUseZipDistrict)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthDetailRqrdId", Me.LabelAutDetailRqrd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoProcessFileId", Me.LabelAutoProcessId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UseRecoveriesId", Me.LabelUseRecoveries)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AddlDACId", Me.labelAddlDAC)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReportCommissionTaxId", Me.lblReportCommTax)
        ' following is accounting company for Elita->Felita interface
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AcctCompanyId", Me.LabelAcctCompany)


        '09/11/2006 - ALR - Added for auto closing claims
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DaysToCloseClaim", Me.LabelDaysToCloseClaim)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EUMemberId", Me.lblEUMember)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "TimeZoneNameId", Me.Labeltime_zone_name)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ComputeTaxBasedId", Me.lblComputeTaxBased)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BillingByDealerId", Me.LabelBILLING_BY_DEALER)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PoliceRptForLossCovId", Me.lblPoliceRptForLoss)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpSiteId", Me.lblFtpSite)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ReqCustomerLegalInfoId", Me.lblREQ_CUSTOMER_LEGAL_INFO_ID)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UseTransferOfOwnership", Me.lblTransferOfOwnership)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RequiresAgentCodeId", Me.lblRequiresAgntCd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UniqueCertificateNumbersId", Me.LabelUniqueCertificateNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UniqueCertEffectiveDate", Me.LabelUniqueCertNumberEffDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Override_WarrantyPrice_Check", Me.LabelOverride_WarrantyPriceid)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CertnumlookupbyId", Me.lblCertNumLookUpBy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SCPreInvWaitingPeriod", Me.LabelSCPreInvWP)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        ' Me.BindListControlToDataView(Me.cboBusinessCountryId, LookupListNew.DataView(LookupListNew.LK_COUNTRIES))--
        Dim countriesLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboBusinessCountryId.Populate(countriesLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })


        'Me.BindListControlToDataView(Me.cboLanguageId, LookupListNew.DataView(LookupListNew.LK_LANGUAGES))
        Dim languageLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboLanguageId.Populate(languageLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .SortFunc = AddressOf .GetDescription
                })

        'Me.BindListControlToDataView(Me.cboInvoiceMethodId, LookupListNew.DropdownLookupList("IMTH", langId, True))
        Dim invoiceMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("IMTH", Thread.CurrentPrincipal.GetLanguageCode())
        cboInvoiceMethodId.Populate(invoiceMethodLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        ' Me.BindListControlToDataView(Me.cboClaimNumbFormatId, LookupListNew.DataView(LookupListNew.LK_CLAIM_FORMAT))
        Dim claimFormatLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClaimFormat", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboClaimNumbFormatId.Populate(claimFormatLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })
        ' Me.BindListControlToDataView(Me.cboCertNumberFormat, LookupListNew.GetCertNumberFormatLookupList)
        Dim certNumberFormat As ListItem() = CommonConfigManager.Current.ListManager.GetList("CertNumberFormat", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboCertNumberFormat.Populate(certNumberFormat, New PopulateOptions() With
            {
              .AddBlankItem = True
                })
        cboSalutationId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

        'Me.BindListControlToDataView(Me.cboSalutationId, yesNoLkl)
        cboSalutationId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        ' Me.BindListControlToDataView(Me.moCountryDrop_WRITE, LookupListNew.DataView(LookupListNew.LK_COUNTRIES))
        'Dim countryListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Country", Thread.CurrentPrincipal.GetLanguageCode())
        Me.moCountryDrop_WRITE.Populate(countriesLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })
        ' Me.BindListControlToDataView(Me.cboCompanyGrpID, LookupListNew.DataView(LookupListNew.LK_COMPANY_GROUP))
        Dim companyGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CompanyGroup", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboCompanyGrpID.Populate(companyGroupLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .SortFunc = AddressOf .GetDescription
                })
        'Me.BindListControlToDataView(Me.cboCompanyType, LookupListNew.DropdownLookupList(LookupListNew.LK_COTYP, langId, True))
        Dim companyTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("COTYP", Thread.CurrentPrincipal.GetLanguageCode())
        cboCompanyType.Populate(companyTypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboUPR_USES_WP, yesNoLkL)
        cboUPR_USES_WP.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboMasterClaimID, LookupListNew.DropdownLookupList(LookupListNew.LK_MASTERCLAIMPROC, langId, True))
        Dim masterClaims As ListItem() = CommonConfigManager.Current.ListManager.GetList("MASTERCLAIMPROC", Thread.CurrentPrincipal.GetLanguageCode())
        cboMasterClaimID.Populate(masterClaims, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboSvcOrdersByDealerId, yesNoLkL)
        cboSvcOrdersByDealerId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboRequireItemDescription, yesNoLkl)
        cboRequireItemDescription.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboEUMemberId, yesNoLkl)
        cboEUMemberId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboUseZipDistictId, yesNoLkl)
        cboUseZipDistictId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.CboUniqueCertificateNumberID, yesNoLkl)
        CboUniqueCertificateNumberID.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboAuthDetailRqrdId, LookupListNew.DropdownLookupList(LookupListNew.LK_AUTH_DTL, langId))
        Dim authDetailRequiredLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTHDTL", Thread.CurrentPrincipal.GetLanguageCode())
        cboAuthDetailRqrdId.Populate(authDetailRequiredLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboAutoprocessid, yesNoLkL)
        cboAutoprocessid.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboUseRecoveries, yesNoLkL)
        cboUseRecoveries.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboAcctCompany, LookupListNew.DataView(LookupListNew.LK_ACCTCOMPANY))

        Dim acctComapnyLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList("AcctCompany", Thread.CurrentPrincipal.GetLanguageCode())
        Me.cboAcctCompany.Populate(acctComapnyLKL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboAddlDAC, LookupListNew.DropdownLookupList(LookupListNew.LK_ADDL_DAC_CODE, langId))
        Dim addlDacLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ADDAC", Thread.CurrentPrincipal.GetLanguageCode())
        cboAddlDAC.Populate(addlDacLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

        'Me.BindListControlToDataView(Me.cboCLIPMethod, LookupListNew.DropdownLookupList(LookupListNew.LK_CLIPMETHOD, langId, True))
        Dim clipMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLIPMETHOD", Thread.CurrentPrincipal.GetLanguageCode())
        cboCLIPMethod.Populate(clipMethodLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.ddlReportCommTax, yesNoLkL)
        ddlReportCommTax.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboTimeZoneName, LookupListNew.DropdownLookupList(LookupListNew.LK_TIME_ZONE_NAMES, langId, True))
        Dim TimeZoneLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TZN", Thread.CurrentPrincipal.GetLanguageCode())
        cboTimeZoneName.Populate(TimeZoneLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        ' Me.BindListControlToDataView(Me.cboComputeTaxBased, LookupListNew.DropdownLookupList(LookupListNew.LK_COMPUTE_TAX_BASED, langId), , , False)
        Dim computeTaxBasedLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMTAXBASED", Thread.CurrentPrincipal.GetLanguageCode())
        cboComputeTaxBased.Populate(computeTaxBasedLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        'Me.BindListControlToDataView(Me.cboBilling_by_dealer, yesNoLkL)
        cboBilling_by_dealer.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboPoliceRptForLoss, yesNoLkL)
        cboPoliceRptForLoss.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        ' Me.BindListControlToDataView(Me.ddlFtpSiteList, LookupListNew.GetFtpSiteLookupList(), , , True)
        Dim ftpSitelkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("FTPSITE", Thread.CurrentPrincipal.GetLanguageCode())
        Me.ddlFtpSiteList.Populate(ftpSitelkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboREQ_CUSTOMER_LEGAL_INFO_ID, LookupListNew.DropdownLookupList(LookupListNew.LK_CLITYP, langId, True))
        Dim reqCustLegalInfoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLITYP", Thread.CurrentPrincipal.GetLanguageCode())
        cboREQ_CUSTOMER_LEGAL_INFO_ID.Populate(reqCustLegalInfoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboTransferOfOwnership, yesNoLkl)
        cboTransferOfOwnership.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.cboReqAgentCode, yesNoLkl, , , False)
        cboReqAgentCode.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        'Me.BindListControlToDataView(Me.cboOverride_WarrantyPriceid, yesNoLkl, , , False)
        cboOverride_WarrantyPriceid.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        'Me.BindListControlToDataView(Me.cboCertNumLookUpBy, LookupListNew.DropdownLookupList(LookupListNew.LK_CertNumLK, langId, True))
        Dim certNumLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CL", Thread.CurrentPrincipal.GetLanguageCode())
        cboCertNumLookUpBy.Populate(certNumLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        'Me.BindListControlToDataView(Me.ddlUsePreInvProcess, yesNoLkl, , , False)
        ddlUsePreInvProcess.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        'ddlPeriodMileageVal.Populate(yesNoLkl, New PopulateOptions() With
        '                                              {
        '                                                .AddBlankItem = False,
        '                                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                                               })

    End Sub

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged

        Try
            LoadRegionDropown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    'Req-1295
    Private Sub cboReqAgentCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReqAgentCode.SelectedIndexChanged
        Try
            Me.PopulateBOProperty(Me.State.MyBO, "RequiresAgentCodeId", Me.cboReqAgentCode)
            CheckAndDisplayAgentWarning()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub LoadRegionDropown()
        Dim oCountryID As Guid = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CountryId = oCountryID
        'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(oCountryID) 'RegionsByCountry
        'BindListControlToDataView(moRegionDrop_WRITE, oRegionList)
        Dim regionByContryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        moRegionDrop_WRITE.Populate(regionByContryLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
    End Sub
    Protected Sub PopulateFormFromBOs()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.TextboxCode, .Code)
            Me.PopulateControlFromBOProperty(Me.TextboxUniqueCertNumberEffDate, .UniqueCertEffectiveDate)
            Me.PopulateControlFromBOProperty(Me.TextboxTaxIdNumber, .TaxIdNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxFax, .Fax)
            Me.PopulateControlFromBOProperty(Me.TextboxPhone, .Phone)
            Me.PopulateControlFromBOProperty(Me.TextboxEmail, .Email)
            Me.PopulateControlFromBOProperty(Me.TextboxRefundToleranceAmount, .RefundToleranceAmt)
            Me.PopulateControlFromBOProperty(Me.TextboxMaxFollowupDays, .MaxFollowupDays)
            Me.PopulateControlFromBOProperty(Me.TextboxDefaultFollowupDays, .DefaultFollowupDays)
            Me.PopulateControlFromBOProperty(Me.TextboxLegalDisclaimer, .LegalDisclaimer)

            Me.PopulateControlFromBOProperty(Me.Address1Text, .Address1)
            Me.PopulateControlFromBOProperty(Me.Address2Text, .Address2)
            Me.PopulateControlFromBOProperty(Me.CityText, .City)
            Me.PopulateControlFromBOProperty(Me.ZipText, .PostalCode)

            '09/11/2006 - ALR - Added for auto closing claims
            Me.PopulateControlFromBOProperty(Me.TextboxDaysToCloseClaim, .DaysToCloseClaim)

            Me.SetSelectedItem(Me.cboLanguageId, .LanguageId)
            Me.SetSelectedItem(Me.cboBusinessCountryId, .BusinessCountryId)
            Me.SetSelectedItem(Me.cboInvoiceMethodId, .InvoiceMethodId)
            Me.SetSelectedItem(Me.cboClaimNumbFormatId, .ClaimNumberFormatId)
            Me.SetSelectedItem(Me.cboCertNumberFormat, .CertNumberFormatId)
            Me.SetSelectedItem(Me.cboSalutationId, .SalutationId)
            Me.SetSelectedItem(Me.moCountryDrop_WRITE, .CountryId)
            Me.SetSelectedItem(Me.cboCompanyGrpID, .CompanyGroupId)
            LoadRegionDropown()
            Me.SetSelectedItem(Me.moRegionDrop_WRITE, .RegionId)
            Me.SetSelectedItem(Me.cboCompanyType, .CompanyTypeId)
            Me.SetSelectedItem(Me.cboUPR_USES_WP, .UPR_USES_WP_Id)
            Me.SetSelectedItem(Me.cboSvcOrdersByDealerId, .ServiceOrdersByDealerId)
            Me.SetSelectedItem(Me.cboRequireItemDescription, .RequireItemDescriptionId)
            Me.SetSelectedItem(Me.cboEUMemberId, .EUMemberId)
            Me.SetSelectedItem(Me.cboUseZipDistictId, .UseZipDistrictId)
            Me.SetSelectedItem(Me.CboUniqueCertificateNumberID, .UniqueCertificateNumbersId)
            Me.SetSelectedItem(Me.cboAuthDetailRqrdId, .AuthDetailRqrdId)
            Me.SetSelectedItem(Me.cboAutoprocessid, .AutoProcessFileId)
            Me.SetSelectedItem(Me.cboUseRecoveries, .UseRecoveriesId)
            Me.SetSelectedItem(Me.cboAcctCompany, .AcctCompanyId)
            If Not .Override_WarrantyPrice_Check.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboOverride_WarrantyPriceid, .Override_WarrantyPrice_Check)
            End If

            Me.SetSelectedItem(Me.cboAddlDAC, .AddlDACId)
            If .AddlDACId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboAddlDAC, LookupListNew.GetIdFromCode(LookupListNew.LK_ADDL_DAC, ADDL_DAC_NONE))
            End If

            If Not .CertnumlookupbyId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboCertNumLookUpBy, .CertnumlookupbyId)
            End If

            If Not Me.State.IsACopy Then
                PopulateUserControlAvailableSelectedCountries()
            End If

            Me.PopulateControlFromBOProperty(Me.TextboxClaimNumberOffset, .ClaimNumberOffset)
            Me.SetSelectedItem(Me.cboEUMemberId, .EUMemberId)

            If .ClipMethodId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboCLIPMethod, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CLIPMETHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Company.CLIP_METHOD_NONE))
            Else
                Me.SetSelectedItem(Me.cboCLIPMethod, .ClipMethodId)
            End If

            If .ReportCommissionTaxId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.ddlReportCommTax, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId), "N"))
            Else
                Me.SetSelectedItem(Me.ddlReportCommTax, .ReportCommissionTaxId)
            End If

            If Not .TimeZoneNameId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboTimeZoneName, .TimeZoneNameId)
            End If

            If .ComputeTaxBasedId.Equals(System.Guid.Empty) Then
                Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_COMPUTE_TAX_BASED, langId)
                Me.SetSelectedItem(Me.cboComputeTaxBased, LookupListNew.GetIdFromCode(dv, Codes.COMPUTE_TAX_BASED_CUSTOMERS_ADDRESS))
            Else
                Me.SetSelectedItem(Me.cboComputeTaxBased, .ComputeTaxBasedId)
            End If

            Me.SetSelectedItem(Me.cboBilling_by_dealer, .BillingByDealerId)
            Me.SetSelectedItem(Me.cboPoliceRptForLoss, .PoliceRptForLossCovId)
            Me.SetSelectedItem(Me.ddlFtpSiteList, .FtpSiteId)
            Me.SetSelectedItem(Me.cboREQ_CUSTOMER_LEGAL_INFO_ID, .ReqCustomerLegalInfoId)

            If Not .UseTransferOfOwnership.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboTransferOfOwnership, .UseTransferOfOwnership)
            End If

            If Not .RequiresAgentCodeId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboReqAgentCode, .RequiresAgentCodeId)
                CheckAndDisplayAgentWarning()
            End If

            If Not .MasterClaimProcessingId.Equals(System.Guid.Empty) Then
                Dim lstValue As System.Web.UI.WebControls.ListItem
                lstValue = Me.cboMasterClaimID.Items.FindByValue(.MasterClaimProcessingId.ToString())
                If (Not lstValue Is Nothing) Then
                    Me.SetSelectedItem(Me.cboMasterClaimID, .MasterClaimProcessingId)
                End If
            End If
            Me.SetSelectedItem(Me.ddlUsePreInvProcess, .UsePreInvoiceProcessId)
            Me.PopulateControlFromBOProperty(Me.txtSCPreINVWP, .SCPreInvWaitingPeriod)
            '''''claim close rules control
            If (Not Me.State.blnIsComingFromCopy) Then
                ClaimCloseRules.CompanyId = Me.State.MyBO.Id
                ClaimCloseRules.companyCode = Me.State.MyBO.Code
                ClaimCloseRules.Populate()
            End If

            'If Not .EnablePeriodMileageVal Is Nothing Then
            '    Me.SetSelectedItem(Me.ddlPeriodMileageVal, .EnablePeriodMileageVal)
            'Else
            '    Me.SetSelectedItem(Me.ddlPeriodMileageVal, "YESNO-N")
            'End If

            ' Populate Attributes
            AttributeValues.DataBind()
        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()

        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCode)
            Me.PopulateBOProperty(Me.State.MyBO, "LanguageId", Me.cboLanguageId)

            Me.PopulateBOProperty(Me.State.MyBO, "TaxIdNumber", Me.TextboxTaxIdNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "BusinessCountryId", Me.cboBusinessCountryId)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone", Me.TextboxPhone)
            Me.PopulateBOProperty(Me.State.MyBO, "Fax", Me.TextboxFax)
            Me.PopulateBOProperty(Me.State.MyBO, "Email", Me.TextboxEmail.Text.ToLower)
            Me.PopulateBOProperty(Me.State.MyBO, "RefundToleranceAmt", Me.TextboxRefundToleranceAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceMethodId", Me.cboInvoiceMethodId)
            Me.PopulateBOProperty(Me.State.MyBO, "ClaimNumberFormatId", Me.cboClaimNumbFormatId)
            Me.PopulateBOProperty(Me.State.MyBO, "CertNumberFormatId", Me.cboCertNumberFormat)
            Me.PopulateBOProperty(Me.State.MyBO, "MaxFollowupDays", Me.TextboxMaxFollowupDays)
            Me.PopulateBOProperty(Me.State.MyBO, "DefaultFollowupDays", Me.TextboxDefaultFollowupDays)
            Me.PopulateBOProperty(Me.State.MyBO, "LegalDisclaimer", Me.TextboxLegalDisclaimer)
            Me.PopulateBOProperty(Me.State.MyBO, "SalutationId", Me.cboSalutationId)

            Me.PopulateBOProperty(Me.State.MyBO, "Address1", Me.Address1Text)
            Me.PopulateBOProperty(Me.State.MyBO, "Address2", Me.Address2Text)
            Me.PopulateBOProperty(Me.State.MyBO, "City", Me.CityText)
            Me.PopulateBOProperty(Me.State.MyBO, "PostalCode", Me.ZipText)
            Me.PopulateBOProperty(Me.State.MyBO, "RegionId", Me.moRegionDrop_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", Me.moCountryDrop_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "CompanyGroupId", Me.cboCompanyGrpID)
            Me.PopulateBOProperty(Me.State.MyBO, "CompanyTypeId", Me.cboCompanyType)
            Me.PopulateBOProperty(Me.State.MyBO, "UPR_USES_WP_Id", Me.cboUPR_USES_WP)
            Me.PopulateBOProperty(Me.State.MyBO, "MasterClaimProcessingId", Me.cboMasterClaimID)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceOrdersByDealerId", Me.cboSvcOrdersByDealerId)
            Me.PopulateBOProperty(Me.State.MyBO, "RequireItemDescriptionId", Me.cboRequireItemDescription)

            '09/11/2006 - ALR - Added for auto closing claims
            Me.PopulateBOProperty(Me.State.MyBO, "DaysToCloseClaim", Me.TextboxDaysToCloseClaim)

            Me.PopulateBOProperty(Me.State.MyBO, "ClaimNumberOffset", Me.TextboxClaimNumberOffset)
            Me.PopulateBOProperty(Me.State.MyBO, "EUMemberId", Me.cboEUMemberId)
            Me.PopulateBOProperty(Me.State.MyBO, "UseZipDistrictId", Me.cboUseZipDistictId)
            Me.PopulateBOProperty(Me.State.MyBO, "UniqueCertificateNumbersId", Me.CboUniqueCertificateNumberID)
            Me.PopulateBOProperty(Me.State.MyBO, "UniqueCertEffectiveDate", Me.TextboxUniqueCertNumberEffDate)
            Me.PopulateBOProperty(Me.State.MyBO, "AuthDetailRqrdId", Me.cboAuthDetailRqrdId)
            Me.PopulateBOProperty(Me.State.MyBO, "AutoProcessFileId", Me.cboAutoprocessid)
            Me.PopulateBOProperty(Me.State.MyBO, "UseRecoveriesId", Me.cboUseRecoveries)
            Me.PopulateBOProperty(Me.State.MyBO, "AcctCompanyId", Me.cboAcctCompany)
            Me.PopulateBOProperty(Me.State.MyBO, "AddlDACId", Me.cboAddlDAC)
            Me.PopulateBOProperty(Me.State.MyBO, "ClipMethodId", Me.cboCLIPMethod)
            Me.PopulateBOProperty(Me.State.MyBO, "CertnumlookupbyId", Me.cboCertNumLookUpBy)
            Me.PopulateBOProperty(Me.State.MyBO, "ReportCommissionTaxId", Me.ddlReportCommTax)

            Me.PopulateBOProperty(Me.State.MyBO, "TimeZoneNameId", Me.cboTimeZoneName)
            Me.PopulateBOProperty(Me.State.MyBO, "ComputeTaxBasedId", Me.cboComputeTaxBased)
            Me.PopulateBOProperty(Me.State.MyBO, "BillingByDealerId", Me.cboBilling_by_dealer)
            Me.PopulateBOProperty(Me.State.MyBO, "PoliceRptForLossCovId", Me.cboPoliceRptForLoss)

            Me.PopulateBOProperty(Me.State.MyBO, "FtpSiteId", Me.ddlFtpSiteList)
            Me.PopulateBOProperty(Me.State.MyBO, "ReqCustomerLegalInfoId", Me.cboREQ_CUSTOMER_LEGAL_INFO_ID)
            Me.PopulateBOProperty(Me.State.MyBO, "UseTransferOfOwnership", Me.cboTransferOfOwnership)
            Me.PopulateBOProperty(Me.State.MyBO, "RequiresAgentCodeId", Me.cboReqAgentCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Override_WarrantyPrice_Check", Me.cboOverride_WarrantyPriceid)
            Me.PopulateBOProperty(Me.State.MyBO, "UsePreInvoiceProcessId", Me.ddlUsePreInvProcess)
            If (Me.State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                Me.PopulateBOProperty(Me.State.MyBO, "SCPreInvWaitingPeriod", Me.txtSCPreINVWP)
            Else
                Me.PopulateBOProperty(Me.State.MyBO, "SCPreInvWaitingPeriod", "0")
            End If
            'Me.PopulateBOProperty(Me.State.MyBO, "EnablePeriodMileageVal", Me.ddlPeriodMileageVal, False, True)
        End With

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        ' Added for REQ: 1295. In case 'requires Agent Code' is set as Yes and there are some dealers with no agent code then display warning message
        CheckAndDisplayAgentWarning()

    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New Company
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()

        Me.State.IsACopy = True
        Me.State.blnIsComingFromCopy = True
        Me.ClaimCloseRules.HideNewButton(True)
        Me.State.OldCompanyId = Me.State.MyBO.Id

        Me.PopulateBOsFormFrom()

        Dim newObj As New Company
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Nothing
        Me.State.MyBO.Description = Nothing

        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        Dim ContriesIDs As New ArrayList
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In Me.UserControlAvailableSelectedCountries.SelectedList
            ContriesIDs.Add(cmpCountryIdStr)
        Next
        Me.State.MyBO.AttachCountries(ContriesIDs)

        'create the backup copy
        Me.State.ScreenSnapShotBO = New Company
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Me.State.IsACopy = False

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    'Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    Me.MasterPage.MessageController.AddError(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub PopulateUserControlAvailableSelectedCountries()
        Me.UserControlAvailableSelectedCountries.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCountries, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView
            Dim selectedDv As DataView

            Dim availDS As DataSet = Me.State.MyBO.GetAvailableCountries(Me.State.MyBO.Id)
            If availDS.Tables.Count > 0 Then
                availableDv = New DataView(availDS.Tables(0))
            End If

            Dim selectedDS As DataSet = Me.State.MyBO.GetSelectedCountries(Me.State.MyBO.Id)

            If selectedDS.Tables.Count > 0 Then
                selectedDv = New DataView(selectedDS.Tables(0))
            End If

            Me.UserControlAvailableSelectedCountries.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, "country_id")
            Me.UserControlAvailableSelectedCountries.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, "country_id")
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCountries, True)
            UserControlAvailableSelectedCountries.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COUNTRIES)
            UserControlAvailableSelectedCountries.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COUNTRIES)
        End If
    End Sub

    Private Sub CreateNewAccountingCloseInfo()
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim OldCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim oAccCloseInfo As AccountingCloseInfo
        Dim saveDate As DateType
        Me.State.year = DatePart("yyyy", Now).ToString
        Me.State.MyBO.ResetAccountClosingInfoList()

        Dim nNewCompanyIDs As ArrayList
        nNewCompanyIDs = Me.State.MyBO.GetComanies(Me.State.MyBO.CompanyGroupId)
        If nNewCompanyIDs.Count > 0 Then
            For i As Integer = 0 To nNewCompanyIDs.Count - 1
                If Not Me.State.MyBO.Id.Equals(CType(nNewCompanyIDs.Item(i), Guid)) Then
                    For Each oAccCloseInfo In Me.State.MyBO.AssociatedAccCloseInfo(CType(nNewCompanyIDs.Item(i), Guid))
                        saveDate = oAccCloseInfo.ClosingDate
                        Me.State.MyBO.AttachAccCloseInfo(saveDate)
                    Next
                    Exit For
                End If
            Next
        Else
            Dim LastDate As Date = (DateAdd("YYYY", -1, DateAdd("m", -Month(Now) + 12, DateAdd("d", -Date.Today.Day + 1, Now))))

            For I As Integer = 1 To 12
                saveDate = Date.Parse((MiscUtil.LastFridayOfMonth(LastDate.AddMonths(I)).ToString), System.Globalization.CultureInfo.CurrentCulture)
                Me.State.MyBO.AttachAccCloseInfo(saveDate)
            Next
        End If
    End Sub

    'REQ-1295
    Protected Sub CheckAndDisplayAgentWarning()

        If ((Not Me.State Is Nothing) AndAlso (Not Me.State.MyBO Is Nothing) AndAlso
            Me.State.MyBO.RequiresAgentCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
            Dim ds As DataSet = Me.State.MyBO.GetCompanyDealerWithoutAgent(Me.State.MyBO.Id)
            If (Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0) Then
                Dim DealerCnt As Integer = CType(ds.Tables(0).Rows(0)(0).ToString(), Integer)
                If (DealerCnt > 0) Then ' Display Warning in case there are dealers without Agent code settings 
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(DEALER_WO_AGENTS), False)
                End If
            End If
        End If
    End Sub

    Public Function GetYesID() As Guid
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
        Return yesId
    End Function
#End Region

#Region "Handlers-DropDown"

    Private Sub CboUniqueCertificateNumberID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CboUniqueCertificateNumberID.SelectedIndexChanged
        EnableDisableUniqueCertFields()
    End Sub

    Private Sub EnableDisableUniqueCertFields()
        If Me.CboUniqueCertificateNumberID.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
            If Me.GetSelectedItem(Me.CboUniqueCertificateNumberID).Equals(GetYesID) Then
                ControlMgr.SetVisibleControl(Me, Me.LabelUniqueCertNumberEffDate, True)
                ControlMgr.SetVisibleControl(Me, Me.TextboxUniqueCertNumberEffDate, True)
                ControlMgr.SetVisibleControl(Me, Me.ImgUniqueCertNumberEffDate, True)
            Else
                TextboxUniqueCertNumberEffDate.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, Me.LabelUniqueCertNumberEffDate, False)
                ControlMgr.SetVisibleControl(Me, Me.TextboxUniqueCertNumberEffDate, False)
                ControlMgr.SetVisibleControl(Me, Me.ImgUniqueCertNumberEffDate, False)
            End If
        End If
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If Me.State.MyBO.ConstrVoilation = False Then
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                ' Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            Else
                Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click

        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                '  Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                               Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click

        Dim ContriesIDs As New ArrayList
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In Me.UserControlAvailableSelectedCountries.SelectedList
            ContriesIDs.Add(cmpCountryIdStr)
        Next
        Me.State.MyBO.DetachCountries(ContriesIDs)
        Me.State.MyBO.DetachAccountClosingInfo()

        Dim addressDeleted As Boolean
        Try
            'Delete the Address
            'Me.UpdateUserCompany()
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                ' Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                               Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSAVE_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If Me.State.MyBO.UniqueCertificateNumbersId.Equals(yesId) Then
                If Trim(TextboxUniqueCertNumberEffDate.Text) = String.Empty Then
                    ElitaPlusPage.SetLabelError(Me.LabelUniqueCertNumberEffDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.UNIQUE_CERT_NUMBER_EFFECTIVE_DATE_REQD_ERR)
                End If
            End If

            If Me.State.MyBO.IsDirty Then
                If Me.State.MyBO.IsNew Then
                    CreateNewAccountingCloseInfo()
                End If
                Me.State.MyBO.Save()

                ''''REQ-5598
                If (Me.State.blnIsComingFromCopy) Then
                    ''''clone Claim Close Rules
                    Dim objCloseClaimRules As New ClaimCloseRules
                    objCloseClaimRules.CopyClaimCloseRulesToNewCompany(Me.State.OldCompanyId, Me.State.MyBO.Id)
                    Me.State.blnIsComingFromCopy = False
                    Me.ClaimCloseRules.HideNewButton(False)
                End If

                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                ' Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnAccCloseDates, False)
                ' Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
        Catch ex As Exception
            ControlMgr.SetVisibleControl(Me, btnAccCloseDates, False)
            If (Me.State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                LabelSCPreInvWP.Style.Add("display", "block")
                txtSCPreINVWP.Style.Add("display", "block")
            Else
                LabelSCPreInvWP.Style.Add("display", "none")
                txtSCPreINVWP.Style.Add("display", "none")
            End If
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Company(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Company
            End If

            Me.State.blnIsComingFromCopy = False
            'Me.ClaimCloseRules.HideNewButton(False)

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub MoveListItems(ByRef sourceList As ListBox, ByRef destList As ListBox)
        If sourceList.Items.Count > 0 Then
            If sourceList.SelectedIndex <> -1 Then
                Dim postalItem As System.Web.UI.WebControls.ListItem = sourceList.SelectedItem
                destList.Items.Add(postalItem)
                sourceList.Items.Remove(postalItem)
                If destList.Items.Count > 0 Then
                    destList.SelectedIndex = 0
                End If
                If sourceList.Items.Count > 0 Then
                    sourceList.SelectedIndex = 0
                End If

            End If
        End If
    End Sub

    Private Sub btnAccCloseDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccCloseDates.Click
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim OldCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim nNewCompanyIDs As New ArrayList
        nNewCompanyIDs.Add(Me.State.MyBO.Id)
        oUser.UpdateUserCompanies(nNewCompanyIDs)
        ElitaPlusIdentity.Current.ActiveUser.ResetUserCompany()
        oUser.AccountingCompaniesClearCache()
        Me.callPage(Tables.AccountingCloseInfoForm.URL, OldCompanies)
    End Sub

#End Region

    Private Sub UserControlAvailableSelectedCountries_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCountries.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachCountries(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCountries_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCountries.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachCountries(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub TextboxLegalDisclaimer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextboxLegalDisclaimer.TextChanged

    End Sub

    Private Sub ClaimCloseRules_RequestClaimCloseRulesData(ByVal sender As Object, ByRef e As UserControlClaimCloseRules.RequestDataEventArgs) Handles ClaimCloseRules.RequestClaimCloseRulesData
        Dim claimCloseRules As New ClaimCloseRules
        claimCloseRules.CompanyId = Me.State.MyBO.Id
        e.Data = claimCloseRules.GetClaimCloseRules()
    End Sub
End Class
