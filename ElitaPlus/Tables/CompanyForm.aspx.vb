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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Company, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
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

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Company(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Me.ErrorCtrl.Clear_Hide() ' REQ-1295
        MasterPage.MessageController.Clear_Hide() ' REQ-1295
        'hide the user control...since we are doing our ownlist.
        'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
        Try
            If Not IsPostBack Then

                'REQ-1295
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_TABLES)
                AddCalendar_New(ImgUniqueCertNumberEffDate, TextboxUniqueCertNumberEffDate)
                UpdateBreadCrum()
                'REQ-1295 : Changes Completed 

                'Date Calendars
                MenuEnabled = False
                ' Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New Company
                End If
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                AttributeValues.TranslateHeaders()
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            Else
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    'REQ-1295
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(LBL_COMPANY)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_COMPANY)
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
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        ChangeEnabledProperty(TextboxDescription, True)
        ChangeEnabledProperty(TextboxCode, True)
        ChangeEnabledProperty(TextboxTaxIdNumber, True)
        ChangeEnabledProperty(TextboxFax, True)
        ChangeEnabledProperty(TextboxPhone, True)
        ChangeEnabledProperty(TextboxEmail, True)
        ChangeEnabledProperty(TextboxRefundToleranceAmount, True)
        ChangeEnabledProperty(TextboxMaxFollowupDays, True)
        ChangeEnabledProperty(TextboxDefaultFollowupDays, True)
        ChangeEnabledProperty(TextboxLegalDisclaimer, True)
        ChangeEnabledProperty(txtSCPreINVWP, True)

        EnableDisableUniqueCertFields()

        If (State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
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
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        BindBOPropertyToLabel(State.MyBO, "Code", LabelCode)
        BindBOPropertyToLabel(State.MyBO, "TaxIdNumber", LabelTaxIdNumber)
        BindBOPropertyToLabel(State.MyBO, "BusinessCountryId", LabelBusinessCountry)
        BindBOPropertyToLabel(State.MyBO, "Phone", LabelPhone1)
        BindBOPropertyToLabel(State.MyBO, "Fax", LabelFax)
        BindBOPropertyToLabel(State.MyBO, "Email", LabelEmail)
        BindBOPropertyToLabel(State.MyBO, "RefundToleranceAmt", LabelRefundToleranceAmt)
        BindBOPropertyToLabel(State.MyBO, "LanguageId", LabelLanguage)
        BindBOPropertyToLabel(State.MyBO, "InvoiceMethodId", LabelInvoiceMethod)
        BindBOPropertyToLabel(State.MyBO, "ClaimNumberFormatId", LabelClaimNumbFormatId)
        BindBOPropertyToLabel(State.MyBO, "CertNumberFormatId", LabelCertNumberFormat)
        BindBOPropertyToLabel(State.MyBO, "UsePreInvoiceProcessId", LabelUsePreInvProcess)
        BindBOPropertyToLabel(State.MyBO, "MaxFollowupDays", LabelMaxFollowupDays)
        BindBOPropertyToLabel(State.MyBO, "DefaultFollowupDays", LabelDefaultFollowupDays)
        BindBOPropertyToLabel(State.MyBO, "LegalDisclaimer", LabelLegalDisclaimer)
        BindBOPropertyToLabel(State.MyBO, "SalutationId", LabelSalutation)

        BindBOPropertyToLabel(State.MyBO, "Address1", Address1Label)
        BindBOPropertyToLabel(State.MyBO, "Address2", Address2Label)
        BindBOPropertyToLabel(State.MyBO, "City", CityLabel)
        BindBOPropertyToLabel(State.MyBO, "RegionId", StateLabel)
        BindBOPropertyToLabel(State.MyBO, "PostalCode", ZipLabel)
        BindBOPropertyToLabel(State.MyBO, "CountryId", CountryLabel)
        BindBOPropertyToLabel(State.MyBO, "CompanyGroupId", LabelCompanyGroup)
        BindBOPropertyToLabel(State.MyBO, "CompanyTypeId", LabelComapnyType)
        BindBOPropertyToLabel(State.MyBO, "UPR_USES_WP_Id", LabelUPR_USES_WP)
        BindBOPropertyToLabel(State.MyBO, "MasterClaimProcessingId", labelMasterClaim)
        BindBOPropertyToLabel(State.MyBO, "ServiceOrdersByDealerId", lblSvcOrdersByDealer)
        BindBOPropertyToLabel(State.MyBO, "RequireItemDescriptionId", lblRequireItemDescription)
        BindBOPropertyToLabel(State.MyBO, "ClaimNumberOffset", LabelCLAIM_NUMBER_OFFSET)
        BindBOPropertyToLabel(State.MyBO, "UseZipDistrictId", LabelUseZipDistrict)
        BindBOPropertyToLabel(State.MyBO, "AuthDetailRqrdId", LabelAutDetailRqrd)
        BindBOPropertyToLabel(State.MyBO, "AutoProcessFileId", LabelAutoProcessId)
        BindBOPropertyToLabel(State.MyBO, "UseRecoveriesId", LabelUseRecoveries)
        BindBOPropertyToLabel(State.MyBO, "AddlDACId", labelAddlDAC)
        BindBOPropertyToLabel(State.MyBO, "ReportCommissionTaxId", lblReportCommTax)
        ' following is accounting company for Elita->Felita interface
        BindBOPropertyToLabel(State.MyBO, "AcctCompanyId", LabelAcctCompany)


        '09/11/2006 - ALR - Added for auto closing claims
        BindBOPropertyToLabel(State.MyBO, "DaysToCloseClaim", LabelDaysToCloseClaim)
        BindBOPropertyToLabel(State.MyBO, "EUMemberId", lblEUMember)

        BindBOPropertyToLabel(State.MyBO, "TimeZoneNameId", Labeltime_zone_name)
        BindBOPropertyToLabel(State.MyBO, "ComputeTaxBasedId", lblComputeTaxBased)
        BindBOPropertyToLabel(State.MyBO, "BillingByDealerId", LabelBILLING_BY_DEALER)
        BindBOPropertyToLabel(State.MyBO, "PoliceRptForLossCovId", lblPoliceRptForLoss)

        BindBOPropertyToLabel(State.MyBO, "FtpSiteId", lblFtpSite)
        BindBOPropertyToLabel(State.MyBO, "ReqCustomerLegalInfoId", lblREQ_CUSTOMER_LEGAL_INFO_ID)
        BindBOPropertyToLabel(State.MyBO, "UseTransferOfOwnership", lblTransferOfOwnership)
        BindBOPropertyToLabel(State.MyBO, "RequiresAgentCodeId", lblRequiresAgntCd)
        BindBOPropertyToLabel(State.MyBO, "UniqueCertificateNumbersId", LabelUniqueCertificateNumber)
        BindBOPropertyToLabel(State.MyBO, "UniqueCertEffectiveDate", LabelUniqueCertNumberEffDate)
        BindBOPropertyToLabel(State.MyBO, "Override_WarrantyPrice_Check", LabelOverride_WarrantyPriceid)
        BindBOPropertyToLabel(State.MyBO, "CertnumlookupbyId", lblCertNumLookUpBy)
        BindBOPropertyToLabel(State.MyBO, "SCPreInvWaitingPeriod", LabelSCPreInvWP)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        ' Me.BindListControlToDataView(Me.cboBusinessCountryId, LookupListNew.DataView(LookupListNew.LK_COUNTRIES))--
        Dim countriesLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
        cboBusinessCountryId.Populate(countriesLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })


        'Me.BindListControlToDataView(Me.cboLanguageId, LookupListNew.DataView(LookupListNew.LK_LANGUAGES))
        Dim languageLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
        cboLanguageId.Populate(languageLkl, New PopulateOptions() With
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
        cboClaimNumbFormatId.Populate(claimFormatLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })
        ' Me.BindListControlToDataView(Me.cboCertNumberFormat, LookupListNew.GetCertNumberFormatLookupList)
        Dim certNumberFormat As ListItem() = CommonConfigManager.Current.ListManager.GetList("CertNumberFormat", Thread.CurrentPrincipal.GetLanguageCode())
        cboCertNumberFormat.Populate(certNumberFormat, New PopulateOptions() With
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
        moCountryDrop_WRITE.Populate(countriesLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })
        ' Me.BindListControlToDataView(Me.cboCompanyGrpID, LookupListNew.DataView(LookupListNew.LK_COMPANY_GROUP))
        Dim companyGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CompanyGroup", Thread.CurrentPrincipal.GetLanguageCode())
        cboCompanyGrpID.Populate(companyGroupLkl, New PopulateOptions() With
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
        cboAcctCompany.Populate(acctComapnyLKL, New PopulateOptions() With
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
        ddlFtpSiteList.Populate(ftpSitelkl, New PopulateOptions() With
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

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged

        Try
            LoadRegionDropown()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    'Req-1295
    Private Sub cboReqAgentCode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboReqAgentCode.SelectedIndexChanged
        Try
            PopulateBOProperty(State.MyBO, "RequiresAgentCodeId", cboReqAgentCode)
            CheckAndDisplayAgentWarning()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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
        With State.MyBO

            PopulateControlFromBOProperty(TextboxDescription, .Description)
            PopulateControlFromBOProperty(TextboxCode, .Code)
            PopulateControlFromBOProperty(TextboxUniqueCertNumberEffDate, .UniqueCertEffectiveDate)
            PopulateControlFromBOProperty(TextboxTaxIdNumber, .TaxIdNumber)
            PopulateControlFromBOProperty(TextboxFax, .Fax)
            PopulateControlFromBOProperty(TextboxPhone, .Phone)
            PopulateControlFromBOProperty(TextboxEmail, .Email)
            PopulateControlFromBOProperty(TextboxRefundToleranceAmount, .RefundToleranceAmt)
            PopulateControlFromBOProperty(TextboxMaxFollowupDays, .MaxFollowupDays)
            PopulateControlFromBOProperty(TextboxDefaultFollowupDays, .DefaultFollowupDays)
            PopulateControlFromBOProperty(TextboxLegalDisclaimer, .LegalDisclaimer)

            PopulateControlFromBOProperty(Address1Text, .Address1)
            PopulateControlFromBOProperty(Address2Text, .Address2)
            PopulateControlFromBOProperty(CityText, .City)
            PopulateControlFromBOProperty(ZipText, .PostalCode)

            '09/11/2006 - ALR - Added for auto closing claims
            PopulateControlFromBOProperty(TextboxDaysToCloseClaim, .DaysToCloseClaim)

            SetSelectedItem(cboLanguageId, .LanguageId)
            SetSelectedItem(cboBusinessCountryId, .BusinessCountryId)
            SetSelectedItem(cboInvoiceMethodId, .InvoiceMethodId)
            SetSelectedItem(cboClaimNumbFormatId, .ClaimNumberFormatId)
            SetSelectedItem(cboCertNumberFormat, .CertNumberFormatId)
            SetSelectedItem(cboSalutationId, .SalutationId)
            SetSelectedItem(moCountryDrop_WRITE, .CountryId)
            SetSelectedItem(cboCompanyGrpID, .CompanyGroupId)
            LoadRegionDropown()
            SetSelectedItem(moRegionDrop_WRITE, .RegionId)
            SetSelectedItem(cboCompanyType, .CompanyTypeId)
            SetSelectedItem(cboUPR_USES_WP, .UPR_USES_WP_Id)
            SetSelectedItem(cboSvcOrdersByDealerId, .ServiceOrdersByDealerId)
            SetSelectedItem(cboRequireItemDescription, .RequireItemDescriptionId)
            SetSelectedItem(cboEUMemberId, .EUMemberId)
            SetSelectedItem(cboUseZipDistictId, .UseZipDistrictId)
            SetSelectedItem(CboUniqueCertificateNumberID, .UniqueCertificateNumbersId)
            SetSelectedItem(cboAuthDetailRqrdId, .AuthDetailRqrdId)
            SetSelectedItem(cboAutoprocessid, .AutoProcessFileId)
            SetSelectedItem(cboUseRecoveries, .UseRecoveriesId)
            SetSelectedItem(cboAcctCompany, .AcctCompanyId)
            If Not .Override_WarrantyPrice_Check.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboOverride_WarrantyPriceid, .Override_WarrantyPrice_Check)
            End If

            SetSelectedItem(cboAddlDAC, .AddlDACId)
            If .AddlDACId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboAddlDAC, LookupListNew.GetIdFromCode(LookupListNew.LK_ADDL_DAC, ADDL_DAC_NONE))
            End If

            If Not .CertnumlookupbyId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboCertNumLookUpBy, .CertnumlookupbyId)
            End If

            If Not State.IsACopy Then
                PopulateUserControlAvailableSelectedCountries()
            End If

            PopulateControlFromBOProperty(TextboxClaimNumberOffset, .ClaimNumberOffset)
            SetSelectedItem(cboEUMemberId, .EUMemberId)

            If .ClipMethodId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboCLIPMethod, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CLIPMETHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Company.CLIP_METHOD_NONE))
            Else
                SetSelectedItem(cboCLIPMethod, .ClipMethodId)
            End If

            If .ReportCommissionTaxId.Equals(System.Guid.Empty) Then
                SetSelectedItem(ddlReportCommTax, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId), "N"))
            Else
                SetSelectedItem(ddlReportCommTax, .ReportCommissionTaxId)
            End If

            If Not .TimeZoneNameId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboTimeZoneName, .TimeZoneNameId)
            End If

            If .ComputeTaxBasedId.Equals(System.Guid.Empty) Then
                Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_COMPUTE_TAX_BASED, langId)
                SetSelectedItem(cboComputeTaxBased, LookupListNew.GetIdFromCode(dv, Codes.COMPUTE_TAX_BASED_CUSTOMERS_ADDRESS))
            Else
                SetSelectedItem(cboComputeTaxBased, .ComputeTaxBasedId)
            End If

            SetSelectedItem(cboBilling_by_dealer, .BillingByDealerId)
            SetSelectedItem(cboPoliceRptForLoss, .PoliceRptForLossCovId)
            SetSelectedItem(ddlFtpSiteList, .FtpSiteId)
            SetSelectedItem(cboREQ_CUSTOMER_LEGAL_INFO_ID, .ReqCustomerLegalInfoId)

            If Not .UseTransferOfOwnership.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboTransferOfOwnership, .UseTransferOfOwnership)
            End If

            If Not .RequiresAgentCodeId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboReqAgentCode, .RequiresAgentCodeId)
                CheckAndDisplayAgentWarning()
            End If

            If Not .MasterClaimProcessingId.Equals(System.Guid.Empty) Then
                Dim lstValue As System.Web.UI.WebControls.ListItem
                lstValue = cboMasterClaimID.Items.FindByValue(.MasterClaimProcessingId.ToString())
                If (lstValue IsNot Nothing) Then
                    SetSelectedItem(cboMasterClaimID, .MasterClaimProcessingId)
                End If
            End If
            SetSelectedItem(ddlUsePreInvProcess, .UsePreInvoiceProcessId)
            PopulateControlFromBOProperty(txtSCPreINVWP, .SCPreInvWaitingPeriod)
            '''''claim close rules control
            If (Not State.blnIsComingFromCopy) Then
                ClaimCloseRules.CompanyId = State.MyBO.Id
                ClaimCloseRules.companyCode = State.MyBO.Code
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

        With State.MyBO
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "Code", TextboxCode)
            PopulateBOProperty(State.MyBO, "LanguageId", cboLanguageId)

            PopulateBOProperty(State.MyBO, "TaxIdNumber", TextboxTaxIdNumber)
            PopulateBOProperty(State.MyBO, "BusinessCountryId", cboBusinessCountryId)
            PopulateBOProperty(State.MyBO, "Phone", TextboxPhone)
            PopulateBOProperty(State.MyBO, "Fax", TextboxFax)
            PopulateBOProperty(State.MyBO, "Email", TextboxEmail.Text.ToLower)
            PopulateBOProperty(State.MyBO, "RefundToleranceAmt", TextboxRefundToleranceAmount)
            PopulateBOProperty(State.MyBO, "InvoiceMethodId", cboInvoiceMethodId)
            PopulateBOProperty(State.MyBO, "ClaimNumberFormatId", cboClaimNumbFormatId)
            PopulateBOProperty(State.MyBO, "CertNumberFormatId", cboCertNumberFormat)
            PopulateBOProperty(State.MyBO, "MaxFollowupDays", TextboxMaxFollowupDays)
            PopulateBOProperty(State.MyBO, "DefaultFollowupDays", TextboxDefaultFollowupDays)
            PopulateBOProperty(State.MyBO, "LegalDisclaimer", TextboxLegalDisclaimer)
            PopulateBOProperty(State.MyBO, "SalutationId", cboSalutationId)

            PopulateBOProperty(State.MyBO, "Address1", Address1Text)
            PopulateBOProperty(State.MyBO, "Address2", Address2Text)
            PopulateBOProperty(State.MyBO, "City", CityText)
            PopulateBOProperty(State.MyBO, "PostalCode", ZipText)
            PopulateBOProperty(State.MyBO, "RegionId", moRegionDrop_WRITE)
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop_WRITE)
            PopulateBOProperty(State.MyBO, "CompanyGroupId", cboCompanyGrpID)
            PopulateBOProperty(State.MyBO, "CompanyTypeId", cboCompanyType)
            PopulateBOProperty(State.MyBO, "UPR_USES_WP_Id", cboUPR_USES_WP)
            PopulateBOProperty(State.MyBO, "MasterClaimProcessingId", cboMasterClaimID)
            PopulateBOProperty(State.MyBO, "ServiceOrdersByDealerId", cboSvcOrdersByDealerId)
            PopulateBOProperty(State.MyBO, "RequireItemDescriptionId", cboRequireItemDescription)

            '09/11/2006 - ALR - Added for auto closing claims
            PopulateBOProperty(State.MyBO, "DaysToCloseClaim", TextboxDaysToCloseClaim)

            PopulateBOProperty(State.MyBO, "ClaimNumberOffset", TextboxClaimNumberOffset)
            PopulateBOProperty(State.MyBO, "EUMemberId", cboEUMemberId)
            PopulateBOProperty(State.MyBO, "UseZipDistrictId", cboUseZipDistictId)
            PopulateBOProperty(State.MyBO, "UniqueCertificateNumbersId", CboUniqueCertificateNumberID)
            PopulateBOProperty(State.MyBO, "UniqueCertEffectiveDate", TextboxUniqueCertNumberEffDate)
            PopulateBOProperty(State.MyBO, "AuthDetailRqrdId", cboAuthDetailRqrdId)
            PopulateBOProperty(State.MyBO, "AutoProcessFileId", cboAutoprocessid)
            PopulateBOProperty(State.MyBO, "UseRecoveriesId", cboUseRecoveries)
            PopulateBOProperty(State.MyBO, "AcctCompanyId", cboAcctCompany)
            PopulateBOProperty(State.MyBO, "AddlDACId", cboAddlDAC)
            PopulateBOProperty(State.MyBO, "ClipMethodId", cboCLIPMethod)
            PopulateBOProperty(State.MyBO, "CertnumlookupbyId", cboCertNumLookUpBy)
            PopulateBOProperty(State.MyBO, "ReportCommissionTaxId", ddlReportCommTax)

            PopulateBOProperty(State.MyBO, "TimeZoneNameId", cboTimeZoneName)
            PopulateBOProperty(State.MyBO, "ComputeTaxBasedId", cboComputeTaxBased)
            PopulateBOProperty(State.MyBO, "BillingByDealerId", cboBilling_by_dealer)
            PopulateBOProperty(State.MyBO, "PoliceRptForLossCovId", cboPoliceRptForLoss)

            PopulateBOProperty(State.MyBO, "FtpSiteId", ddlFtpSiteList)
            PopulateBOProperty(State.MyBO, "ReqCustomerLegalInfoId", cboREQ_CUSTOMER_LEGAL_INFO_ID)
            PopulateBOProperty(State.MyBO, "UseTransferOfOwnership", cboTransferOfOwnership)
            PopulateBOProperty(State.MyBO, "RequiresAgentCodeId", cboReqAgentCode)
            PopulateBOProperty(State.MyBO, "Override_WarrantyPrice_Check", cboOverride_WarrantyPriceid)
            PopulateBOProperty(State.MyBO, "UsePreInvoiceProcessId", ddlUsePreInvProcess)
            If (State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                PopulateBOProperty(State.MyBO, "SCPreInvWaitingPeriod", txtSCPreINVWP)
            Else
                PopulateBOProperty(State.MyBO, "SCPreInvWaitingPeriod", "0")
            End If
            'Me.PopulateBOProperty(Me.State.MyBO, "EnablePeriodMileageVal", Me.ddlPeriodMileageVal, False, True)
        End With

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

        ' Added for REQ: 1295. In case 'requires Agent Code' is set as Yes and there are some dealers with no agent code then display warning message
        CheckAndDisplayAgentWarning()

    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New Company
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()

        State.IsACopy = True
        State.blnIsComingFromCopy = True
        ClaimCloseRules.HideNewButton(True)
        State.OldCompanyId = State.MyBO.Id

        PopulateBOsFormFrom()

        Dim newObj As New Company
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.Code = Nothing
        State.MyBO.Description = Nothing

        PopulateFormFromBOs()
        EnableDisableFields()

        Dim ContriesIDs As New ArrayList
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In UserControlAvailableSelectedCountries.SelectedList
            ContriesIDs.Add(cmpCountryIdStr)
        Next
        State.MyBO.AttachCountries(ContriesIDs)

        'create the backup copy
        State.ScreenSnapShotBO = New Company
        State.ScreenSnapShotBO.Clone(State.MyBO)
        State.IsACopy = False

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    'Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    MasterPage.MessageController.AddError(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub PopulateUserControlAvailableSelectedCountries()
        UserControlAvailableSelectedCountries.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCountries, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView
            Dim selectedDv As DataView

            Dim availDS As DataSet = State.MyBO.GetAvailableCountries(State.MyBO.Id)
            If availDS.Tables.Count > 0 Then
                availableDv = New DataView(availDS.Tables(0))
            End If

            Dim selectedDS As DataSet = State.MyBO.GetSelectedCountries(State.MyBO.Id)

            If selectedDS.Tables.Count > 0 Then
                selectedDv = New DataView(selectedDS.Tables(0))
            End If

            UserControlAvailableSelectedCountries.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, "country_id")
            UserControlAvailableSelectedCountries.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, "country_id")
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
        State.year = DatePart("yyyy", Now).ToString
        State.MyBO.ResetAccountClosingInfoList()

        Dim nNewCompanyIDs As ArrayList
        nNewCompanyIDs = State.MyBO.GetComanies(State.MyBO.CompanyGroupId)
        If nNewCompanyIDs.Count > 0 Then
            For i As Integer = 0 To nNewCompanyIDs.Count - 1
                If Not State.MyBO.Id.Equals(CType(nNewCompanyIDs.Item(i), Guid)) Then
                    For Each oAccCloseInfo In State.MyBO.AssociatedAccCloseInfo(CType(nNewCompanyIDs.Item(i), Guid))
                        saveDate = oAccCloseInfo.ClosingDate
                        State.MyBO.AttachAccCloseInfo(saveDate)
                    Next
                    Exit For
                End If
            Next
        Else
            Dim LastDate As Date = (DateAdd("YYYY", -1, DateAdd("m", -Month(Now) + 12, DateAdd("d", -Date.Today.Day + 1, Now))))

            For I As Integer = 1 To 12
                saveDate = Date.Parse((MiscUtil.LastFridayOfMonth(LastDate.AddMonths(I)).ToString), System.Globalization.CultureInfo.CurrentCulture)
                State.MyBO.AttachAccCloseInfo(saveDate)
            Next
        End If
    End Sub

    'REQ-1295
    Protected Sub CheckAndDisplayAgentWarning()

        If ((State IsNot Nothing) AndAlso (State.MyBO IsNot Nothing) AndAlso
            State.MyBO.RequiresAgentCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
            Dim ds As DataSet = State.MyBO.GetCompanyDealerWithoutAgent(State.MyBO.Id)
            If (ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0) Then
                Dim DealerCnt As Integer = CType(ds.Tables(0).Rows(0)(0).ToString(), Integer)
                If (DealerCnt > 0) Then ' Display Warning in case there are dealers without Agent code settings 
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(DEALER_WO_AGENTS), False)
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
        If CboUniqueCertificateNumberID.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            If GetSelectedItem(CboUniqueCertificateNumberID).Equals(GetYesID) Then
                ControlMgr.SetVisibleControl(Me, LabelUniqueCertNumberEffDate, True)
                ControlMgr.SetVisibleControl(Me, TextboxUniqueCertNumberEffDate, True)
                ControlMgr.SetVisibleControl(Me, ImgUniqueCertNumberEffDate, True)
            Else
                TextboxUniqueCertNumberEffDate.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, LabelUniqueCertNumberEffDate, False)
                ControlMgr.SetVisibleControl(Me, TextboxUniqueCertNumberEffDate, False)
                ControlMgr.SetVisibleControl(Me, ImgUniqueCertNumberEffDate, False)
            End If
        End If
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If State.MyBO.ConstrVoilation = False Then
                HandleErrors(ex, MasterPage.MessageController)
                ' Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            Else
                ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click

        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                '  Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                               HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click

        Dim ContriesIDs As New ArrayList
        Dim cmpCountryIdStr As String
        For Each cmpCountryIdStr In UserControlAvailableSelectedCountries.SelectedList
            ContriesIDs.Add(cmpCountryIdStr)
        Next
        State.MyBO.DetachCountries(ContriesIDs)
        State.MyBO.DetachAccountClosingInfo()

        Dim addressDeleted As Boolean
        Try
            'Delete the Address
            'Me.UpdateUserCompany()
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                ' Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                               HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSAVE_WRITE.Click
        Try
            PopulateBOsFormFrom()
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y)
            If State.MyBO.UniqueCertificateNumbersId.Equals(yesId) Then
                If Trim(TextboxUniqueCertNumberEffDate.Text) = String.Empty Then
                    ElitaPlusPage.SetLabelError(LabelUniqueCertNumberEffDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.UNIQUE_CERT_NUMBER_EFFECTIVE_DATE_REQD_ERR)
                End If
            End If

            If State.MyBO.IsDirty Then
                If State.MyBO.IsNew Then
                    CreateNewAccountingCloseInfo()
                End If
                State.MyBO.Save()

                ''''REQ-5598
                If (State.blnIsComingFromCopy) Then
                    ''''clone Claim Close Rules
                    Dim objCloseClaimRules As New ClaimCloseRules
                    objCloseClaimRules.CopyClaimCloseRulesToNewCompany(State.OldCompanyId, State.MyBO.Id)
                    State.blnIsComingFromCopy = False
                    ClaimCloseRules.HideNewButton(False)
                End If

                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                ' Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnAccCloseDates, False)
                ' Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If
        Catch ex As Exception
            ControlMgr.SetVisibleControl(Me, btnAccCloseDates, False)
            If (State.MyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                LabelSCPreInvWP.Style.Add("display", "block")
                txtSCPreINVWP.Style.Add("display", "block")
            Else
                LabelSCPreInvWP.Style.Add("display", "none")
                txtSCPreINVWP.Style.Add("display", "none")
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Company(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Company
            End If

            State.blnIsComingFromCopy = False
            'Me.ClaimCloseRules.HideNewButton(False)

            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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

    Private Sub btnAccCloseDates_Click(sender As System.Object, e As System.EventArgs) Handles btnAccCloseDates.Click
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim OldCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim nNewCompanyIDs As New ArrayList
        nNewCompanyIDs.Add(State.MyBO.Id)
        oUser.UpdateUserCompanies(nNewCompanyIDs)
        ElitaPlusIdentity.Current.ActiveUser.ResetUserCompany()
        oUser.AccountingCompaniesClearCache()
        callPage(Tables.AccountingCloseInfoForm.URL, OldCompanies)
    End Sub

#End Region

    Private Sub UserControlAvailableSelectedCountries_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCountries.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachCountries(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCountries_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCountries.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachCountries(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub TextboxLegalDisclaimer_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextboxLegalDisclaimer.TextChanged

    End Sub

    Private Sub ClaimCloseRules_RequestClaimCloseRulesData(sender As Object, ByRef e As UserControlClaimCloseRules.RequestDataEventArgs) Handles ClaimCloseRules.RequestClaimCloseRulesData
        Dim claimCloseRules As New ClaimCloseRules
        claimCloseRules.CompanyId = State.MyBO.Id
        e.Data = claimCloseRules.GetClaimCloseRules()
    End Sub
End Class
