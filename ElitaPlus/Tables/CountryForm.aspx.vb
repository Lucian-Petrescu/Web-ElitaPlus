'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/16/2004)  ********************
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Collections.Generic



Partial Class CountryForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "CountryForm.aspx"
#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True
    Private Shared ListItemValue As Integer

#Region "Line OF Business Related"
    Private Const LineOfBusinessGridColCode As Integer = 0
    Private Const LineOfBusinessGridColDescription As Integer = 1
    Private Const LineOfBusinessGridColBusinessType As Integer = 2
    Private Const LineOfBusinessGridColInUse As Integer = 3
    Private Const LineOfBusinessGridColEditBtn As Integer = 4
    Private Const LineOfBusinessGridColColDeleteBtn As Integer = 5


    Private Const LineOfbusinessNone As Integer = 0
    Private Const LineOfbusinessAdd As Integer = 1
    Private Const LineOfbusinessEdit As Integer = 2
    Private Const LineOfbusinessDelete As Integer = 3
#End Region

#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Country
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Country, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As Country

        Public ScreenSnapShotBO As Country
        Public IsNew As Boolean = False
        Public LineOfBusinessOrig As CountryLineOfBusiness
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public yesId As Guid
        Public noId As Guid
        Public yesCode As String
        Public noCode As String
        Public fsmslCode As String

        'Individual Policy'
        Public LineOfBusinessAction As Integer = LineOfbusinessNone
        Public LineOfBusinessList As List(Of CountryLineOfBusiness)
        Public LineOfBusinessWorkingItem As CountryLineOfBusiness
        Public UsedLineOfBusinessInContracts As New List(Of Guid)
        Public PriceListAprrovalEmailIsNull As String = Nothing
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
                Me.State.MyBO = New Country(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        Me.MasterPage.MessageController.Clear_Hide()
        'Individual Policy'

        ErrorControllerDS.Clear_Hide()

        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                Me.TranslateGridHeader(moLineOfBusinessGridView)

                UpdateBreadCrum()

                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Country
                End If

                PopulateDropdowns()

                If Me.State.IsNew = True Then
                    CreateNew()
                End If

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()

            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            'Individual Policy'
            ClearGridViewHeadersAndLabelsErrorSign()

            ' Get all used Line of business once.
            If Not Me.State.UsedLineOfBusinessInContracts.Any() Then
                Me.State.UsedLineOfBusinessInContracts = Me.State.LineOfBusinessOrig.GetUsedLineOfBusinessInContracts(Me.State.MyBO.Id)
            End If

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

            Me.cboRegulatoryReporting.Attributes.Add("onchange", "ShowHideRegulatoryExtractDate()")
            Me.cboUseAddressValidation.Attributes.Add("onchange", "ShowHideAllowForceAddressANDAddressConfidenceThreshold()")
            Me.ShowHideAllowForceAddressANDAddressConfidenceThreshold()

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            ' Clean Popup Input
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'Now disable depending on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LanguageId", Me.LabelLanguage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PrimaryCurrencyId", Me.LabelPrimaryCurrency)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SecondaryCurrencyId", Me.LabelSecondaryCurrency)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BankIDLength", Me.LabelBankIDLength)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BankAcctNoLength", Me.LabelBankAcctNoLength)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MailAddrFormat", Me.LabelMailAddrFormat)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactInfoReqFields", Me.LabelCONTACT_INFO_REQ_FIELDS)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AddressInfoReqFields", Me.LabelAddrInfoReqFields)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EuropeanCountryId", Me.LabelEuropeanCountry)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ValidateBankInfoId", Me.LabelValidateBankInfo)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxByProductTypeId", Me.LabelTaxByProductType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DefaultScForDeniedClaims", Me.lblDefaultSCForDeniedClaims)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UseBankListId", Me.lblUseBankList)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RegulatoryReportingId", Me.LabelRegulatoryReporting)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "NotifyGroupEmail", Me.LabelRegulatoryNotifyGroupEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LastRegulatoryExtractDate", Me.LabelLastRegulatoryExtractDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CreditScoringPct", Me.LabelCreditScoringPct)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AbnormalCLMFreqNo", Me.LabelAbnormalClmFrqNo)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CertCountSuspOP", Me.LabelCertCountSuspOp)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DefaultSSId", Me.lblDefaultSC)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsoCode", lblISOCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListApprovalEmail", Me.lblPriceListApprovalEmail)

        'REQ - 6155
        Me.BindBOPropertyToLabel(Me.State.MyBO, "UseAddressValidationXcd", Me.lblUseAddressValidation)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowForceAddressXcd", Me.lblAllowForceAddress)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AddressConfidenceThreshold", Me.lblAddressConfidenceThreshold)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowForget", Me.lblAllowForget)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FullNameFormat", Me.lblFullnameFormat)

        'Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListApprovalEmail", Me.lblPriceListApprovalEmail)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListApprovalNeeded", lblPriceListApprovalNeeded)

        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()

        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())

        Dim fullnameformat As ListItem() = CommonConfigManager.Current.ListManager.GetList("FULL_NAME_FMT", Thread.CurrentPrincipal.GetLanguageCode())

        Dim LanguageListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LanguageList", Thread.CurrentPrincipal.GetLanguageCode())
        cboLanguageId.Populate(LanguageListLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                    })

        Dim currencyprimaryListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode())
        cboPrimaryCurrencyId.Populate(currencyprimaryListLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                    })

        Dim currencysecondaryListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode())
        cboSecondaryCurrencyId.Populate(currencysecondaryListLkl, New PopulateOptions() With
                {
                  .AddBlankItem = True
                    })

        cboEuropeanCountry.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Dim validateBankInfoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("VBINFO", Thread.CurrentPrincipal.GetLanguageCode())
        cboValidateBankInfo.Populate(validateBankInfoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        cboTaxByProductType.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        cboUseBankList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        cboByteCheck.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
        cboRegulatoryReporting.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

        'REQ - 6155
        'Me.cboUseAddressValidation.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
        Me.cboUseAddressValidation.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        'Me.cboAllowForceAddress.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        Me.cboAllowForceAddress.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        Me.cboAllowForget.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        Me.cboPriceListApprovalNeeded.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        cboFullNameFormat.Populate(fullnameformat, New PopulateOptions() With
                                           {
                                            .AddBlankItem = False,
                                           .TextFunc = AddressOf .GetDescription,
                                           .ValueFunc = AddressOf .GetExtendedCode
                                           })

    End Sub

    Protected Sub LoadPostalCodeLists()
        BindListControlToDataView(SelectedPostalCodeList, Me.State.MyBO.GetSelectedPostalCode(), "DESCRIPTION", "ID", False)
        If SelectedPostalCodeList.Items.Count > 0 Then
            SelectedPostalCodeList.SelectedIndex = 0
        End If
        BindListControlToDataView(AvailPostalCodeList, Me.State.MyBO.GetAvailablePostalCode(), "DESCRIPTION", "ID", False)
        If AvailPostalCodeList.Items.Count > 0 Then
            AvailPostalCodeList.SelectedIndex = 0
        End If
    End Sub
    Protected Sub PopulateFormFromBOs()

        'Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
        'lkYesNo.RowFilter += " and code='Y'"
        'Me.State.yesId = New Guid(CType(lkYesNo.Item(0).Item("ID"), Byte()))
        'Me.State.yesCode = Codes.YESNO + "-" + CType(lkYesNo.Item(0).Item("CODE"), String)
        'Me.State.noCode = Codes.YESNO + "-" + Codes.YESNO_N

        Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Me.State.yesId = (From lst In oYesNoList
                          Where lst.Code = "Y"
                          Select lst.ListItemId).FirstOrDefault()
        Me.State.noId = (From lst In oYesNoList
                         Where lst.Code = "N"
                         Select lst.ListItemId).FirstOrDefault()
        Me.State.yesCode = (From lst In oYesNoList
                            Where lst.Code = "Y"
                            Select lst.ExtendedCode).FirstOrDefault()
        Me.State.noCode = (From lst In oYesNoList
                           Where lst.Code = "N"
                           Select lst.ExtendedCode).FirstOrDefault()

        Dim fullnameformat As ListItem() = CommonConfigManager.Current.ListManager.GetList("FULL_NAME_FMT", Thread.CurrentPrincipal.GetLanguageCode())
        Me.State.fsmslCode = (From lst In fullnameformat
                              Where lst.Code = "FSMSL"
                              Select lst.ExtendedCode).FirstOrDefault()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.TextboxCode, .Code)
            Me.SetSelectedItem(Me.cboLanguageId, .LanguageId)
            Me.SetSelectedItem(Me.cboPrimaryCurrencyId, .PrimaryCurrencyId)
            Me.SetSelectedItem(Me.cboSecondaryCurrencyId, .SecondaryCurrencyId)
            Me.PopulateControlFromBOProperty(Me.TextboxBankIDLength, .BankIDLength)
            Me.PopulateControlFromBOProperty(Me.TextboxBankAcctNoLength, .BankAcctNoLength)
            Me.PopulateControlFromBOProperty(Me.TextboxMailAddrFormat, .MailAddrFormat)
            Me.PopulateControlFromBOProperty(Me.TextboxCONTACT_INFO_REQ_FIELDS, .ContactInfoReqFields)
            Me.PopulateControlFromBOProperty(Me.TextboxAddrInfoReqFields, .AddressInfoReqFields)
            Me.SetSelectedItem(Me.cboEuropeanCountry, .EuropeanCountryId)
            Me.SetSelectedItem(Me.cboValidateBankInfo, .ValidateBankInfoId)

            If .TaxByProductTypeId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(cboTaxByProductType, Me.State.noId)
            Else
                Me.SetSelectedItem(Me.cboTaxByProductType, .TaxByProductTypeId)
            End If

            'DEF-22411-START
            'Me.SetSelectedItem(Me.cboUseBankList, .UseBankListId)
            If .UseBankListId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(cboUseBankList, Me.State.noId)
            Else
                Me.SetSelectedItem(Me.cboUseBankList, .UseBankListId)
            End If

            'DEF-22411-END
            inpDefaultSCFDCId.Value = .DefaultScForDeniedClaims.ToString
            inpDefaultSCFDCDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .DefaultScForDeniedClaims)
            txtDefaultSCForDeniedClaims.Text = inpDefaultSCFDCDesc.Value

            'Req-5546
            inpDefaultSCId.Value = .DefaultSCId.ToString
            inpDefaultSCDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .DefaultSCId)
            txtDefaultSC.Text = inpDefaultSCDesc.Value

            LoadPostalCodeLists()

            If .RequireByteCheckId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(cboByteCheck, Me.State.noId)
            Else
                Me.SetSelectedItem(Me.cboByteCheck, .RequireByteCheckId)
            End If

            Me.PopulateControlFromBOProperty(Me.TextboxLastRegulatoryExtractDate, .LastRegulatoryExtractDate)
            Me.PopulateControlFromBOProperty(Me.TextboxRegulatoryNotifyGroupEmail, .NotifyGroupEmail)
            Me.SetSelectedItem(Me.cboRegulatoryReporting, .RegulatoryReportingId)

            Me.PopulateControlFromBOProperty(Me.TextboxCreditScoringPct, .CreditScoringPct)
            Me.PopulateControlFromBOProperty(Me.TextboxAbnormalClmFrqNo, .AbnormalCLMFreqNo)
            Me.PopulateControlFromBOProperty(Me.TextboxCertCountSuspOp, .CertCountSuspOP)

            If Not .RegulatoryReportingId.Equals(System.Guid.Empty) AndAlso .RegulatoryReportingId.Equals(Me.State.yesId) Then
                ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
                Me.LabelLastRegulatoryExtractDate.Attributes("style") = ""
                Me.TextboxLastRegulatoryExtractDate.Attributes("style") = ""
            Else
                ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
                Me.LabelLastRegulatoryExtractDate.Attributes("style") = "display: none"
                Me.TextboxLastRegulatoryExtractDate.Attributes("style") = "display: none"
            End If

            'REQ
            If String.IsNullOrEmpty(.UseAddressValidationXcd) Then
                Me.SetSelectedItem(cboUseAddressValidation, Me.State.noCode)
            Else
                Me.SetSelectedItem(Me.cboUseAddressValidation, .UseAddressValidationXcd)
            End If

            If String.IsNullOrEmpty(.AllowForceAddressXcd) Then
                Me.SetSelectedItem(cboAllowForceAddress, Me.State.noCode)
            Else
                Me.SetSelectedItem(Me.cboAllowForceAddress, .AllowForceAddressXcd)
            End If

            If String.IsNullOrEmpty(.AllowForget) Then
                Me.SetSelectedItem(cboAllowForget, Me.State.noCode)
            Else
                Me.SetSelectedItem(Me.cboAllowForget, .AllowForget)
            End If

            If String.IsNullOrEmpty(.PriceListApprovalNeeded) Then
                Me.SetSelectedItem(cboPriceListApprovalNeeded, Me.State.noCode)
            Else
                Me.SetSelectedItem(Me.cboPriceListApprovalNeeded, .PriceListApprovalNeeded)
            End If

            If String.IsNullOrEmpty(.FullNameFormat) Then
                Me.SetSelectedItem(cboFullNameFormat, Me.State.fsmslCode)
            Else
                Me.SetSelectedItem(Me.cboFullNameFormat, .FullNameFormat)
            End If


            Me.PopulateControlFromBOProperty(Me.TextboxAddressConfidenceThreshold, .AddressConfidenceThreshold)
            Me.PopulateControlFromBOProperty(Me.txtISOCode, .IsoCode)

            If .PriceListApprovalEmail Is Nothing OrElse .PriceListApprovalEmail.Equals(String.Empty) Then
                Me.State.PriceListAprrovalEmailIsNull = TranslationBase.TranslateLabelOrMessage("THERE_IS_NO_VALUE")
                Me.PopulateControlFromBOProperty(Me.txtPriceListApprovalEmail, Me.State.PriceListAprrovalEmailIsNull)
            Else
                Me.PopulateControlFromBOProperty(Me.txtPriceListApprovalEmail, .PriceListApprovalEmail)
            End If

            'Me.PopulateControlFromBOProperty(Me.txtPriceListApprovalEmail, .PriceListApprovalEmail)
        End With

        'Individual Policy
        If State.LineOfBusinessList Is Nothing Then
            State.LineOfBusinessList = CountryLineOfBusiness.GetCountryLineOfBusinessList(State.MyBO.Id)
        End If
        PopulateLineOfBusinessGrid(State.LineOfBusinessList)
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCode)
            Me.PopulateBOProperty(Me.State.MyBO, "LanguageId", Me.cboLanguageId)
            Me.PopulateBOProperty(Me.State.MyBO, "PrimaryCurrencyId", Me.cboPrimaryCurrencyId)
            Me.PopulateBOProperty(Me.State.MyBO, "SecondaryCurrencyId", Me.cboSecondaryCurrencyId)
            Me.PopulateBOProperty(Me.State.MyBO, "BankIDLength", Me.TextboxBankIDLength)
            Me.PopulateBOProperty(Me.State.MyBO, "BankAcctNoLength", Me.TextboxBankAcctNoLength)
            Me.PopulateBOProperty(Me.State.MyBO, "MailAddrFormat", Me.TextboxMailAddrFormat)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactInfoReqFields", Me.TextboxCONTACT_INFO_REQ_FIELDS)
            Me.PopulateBOProperty(Me.State.MyBO, "AddressInfoReqFields", Me.TextboxAddrInfoReqFields)
            Me.PopulateBOProperty(Me.State.MyBO, "EuropeanCountryId", Me.cboEuropeanCountry)
            Me.PopulateBOProperty(Me.State.MyBO, "ValidateBankInfoId", Me.cboValidateBankInfo)
            Me.PopulateBOProperty(Me.State.MyBO, "TaxByProductTypeId", Me.cboTaxByProductType)
            Me.PopulateBOProperty(Me.State.MyBO, "UseBankListId", Me.cboUseBankList)
            Me.PopulateBOProperty(Me.State.MyBO, "RegulatoryReportingId", Me.cboRegulatoryReporting)
            Me.PopulateBOProperty(Me.State.MyBO, "NotifyGroupEmail", Me.TextboxRegulatoryNotifyGroupEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "CreditScoringPct", Me.TextboxCreditScoringPct)
            Me.PopulateBOProperty(Me.State.MyBO, "AbnormalCLMFreqNo", Me.TextboxAbnormalClmFrqNo)
            Me.PopulateBOProperty(Me.State.MyBO, "CertCountSuspOP", Me.TextboxCertCountSuspOp)

            If txtDefaultSCForDeniedClaims.Text.Trim <> String.Empty AndAlso txtDefaultSCForDeniedClaims.Text.Trim.ToUpper <> inpDefaultSCFDCDesc.Value.Trim.ToUpper Then
                ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC_FOR_DENIED_CLAIMS"), txtDefaultSCForDeniedClaims, lblDefaultSCForDeniedClaims, New Exception("Service Center Not Found")))
            Else
                AjaxController.PopulateBOAutoComplete(txtDefaultSCForDeniedClaims, inpDefaultSCFDCDesc,
                            inpDefaultSCFDCId, Me.State.MyBO, "DefaultScForDeniedClaims")
            End If

            'REQ-5546
            If txtDefaultSC.Text.Trim <> String.Empty AndAlso txtDefaultSC.Text.Trim.ToUpper <> inpDefaultSCDesc.Value.Trim.ToUpper Then
                ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC"), txtDefaultSC, lblDefaultSC, New Exception("Service Center Not Found")))
            Else
                AjaxController.PopulateBOAutoComplete(txtDefaultSC, inpDefaultSCDesc,
                            inpDefaultSCId, Me.State.MyBO, "DefaultSCId")
            End If

            ' if actual selected rows are not in the selected list, they must have been removed. 
            Dim selPostalCode As DataView = Me.State.MyBO.GetSelectedPostalCode()
            For iRow As Integer = 0 To selPostalCode.Count - 1
                Dim postalCodeID As Guid = New Guid(CType(selPostalCode(iRow)("ID"), Byte()))

                If SelectedPostalCodeList.Items.FindByValue(postalCodeID.ToString) Is Nothing Then
                    Me.State.MyBO.DetachPostalCodeFormat(postalCodeID.ToString)
                End If
            Next

            ' if actual available rows are not in the available list, they must have been added . 
            Dim availPostalCode As DataView = Me.State.MyBO.GetAvailablePostalCode()
            For iRow As Integer = 0 To availPostalCode.Count - 1
                Dim postalCodeID As Guid = New Guid(CType(availPostalCode(iRow)("ID"), Byte()))

                If AvailPostalCodeList.Items.FindByValue(postalCodeID.ToString) Is Nothing Then
                    Me.State.MyBO.AttachPostalCodeFormat(postalCodeID.ToString)
                End If
            Next

            Me.PopulateBOProperty(Me.State.MyBO, "RequireByteCheckId", Me.cboByteCheck)
            Me.PopulateBOProperty(Me.State.MyBO, "RequireByteCheckId", Me.cboByteCheck)

            'REQ
            Me.PopulateBOProperty(Me.State.MyBO, "UseAddressValidationXcd", Me.cboUseAddressValidation, False, True)
            If Not String.IsNullOrEmpty(Me.cboUseAddressValidation.SelectedValue) AndAlso Me.cboUseAddressValidation.SelectedValue.Equals(Me.State.yesCode) Then
                Me.PopulateBOProperty(Me.State.MyBO, "AllowForceAddressXcd", Me.cboAllowForceAddress, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "AddressConfidenceThreshold", Me.TextboxAddressConfidenceThreshold)
            Else
                Me.PopulateBOProperty(Me.State.MyBO, "AllowForceAddressXcd", String.Empty)
                Me.PopulateBOProperty(Me.State.MyBO, "AddressConfidenceThreshold", String.Empty)
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "IsoCode", Me.txtISOCode)

            Me.PopulateBOProperty(Me.State.MyBO, "AllowForget", Me.cboAllowForget, False, True)

            If Not Me.txtPriceListApprovalEmail.Text Is Nothing AndAlso Me.txtPriceListApprovalEmail.Text.Equals(Me.State.PriceListAprrovalEmailIsNull) Then
                Me.txtPriceListApprovalEmail.Text = ""
            End If
            Me.PopulateBOProperty(Me.State.MyBO, "PriceListApprovalEmail", Me.txtPriceListApprovalEmail)

            Me.PopulateBOProperty(Me.State.MyBO, "PriceListApprovalNeeded", Me.cboPriceListApprovalNeeded, False, True)

            Me.PopulateBOProperty(Me.State.MyBO, "FullNameFormat", Me.cboFullNameFormat, False, True)

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New Country
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        'Individual Policy
        ClearLineOfBusinessState()
    End Sub

    Protected Sub CreateNewWithCopy()

        Me.TextboxDescription.Text = ""
        Me.TextboxCode.Text = ""

        inpDefaultSCFDCId.Value = ""
        inpDefaultSCFDCDesc.Value = ""
        txtDefaultSCForDeniedClaims.Text = ""

        inpDefaultSCId.Value = ""
        inpDefaultSCDesc.Value = ""
        txtDefaultSC.Text = ""

        Me.State.MyBO = New Country
        Me.PopulateBOsFormFrom()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New Country
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

        'If SelectedPostalCodeList.Items.Count > 0 Then
        '    Me.State.MyBO.AttachPostalCodeFormat(GetListValues(SelectedPostalCodeList))
        'End If

        Me.txtISOCode.Text = ""

        LoadPostalCodeLists()
        'Individual Policy
        ClearLineOfBusinessState()
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
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
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
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""

        'Hide Client controls
        If Not Me.State.MyBO.RegulatoryReportingId.Equals(System.Guid.Empty) AndAlso Me.State.MyBO.RegulatoryReportingId.Equals(Me.State.yesId) Then
            ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
            Me.LabelLastRegulatoryExtractDate.Attributes("style") = ""
            Me.TextboxLastRegulatoryExtractDate.Attributes("style") = ""
        Else
            ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
            Me.LabelLastRegulatoryExtractDate.Attributes("style") = "display: none"
            Me.TextboxLastRegulatoryExtractDate.Attributes("style") = "display: none"
        End If

    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            ' Validate User Selected Required Fields
            ValidateRequiredFields()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION) 'DEF-21578
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED) 'DEF-21578
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ValidateRequiredFields()
        Dim requiredFieldsErrorExist As Boolean = False
        Dim strRequiredFieldSetting As String = String.Empty

        If strRequiredFieldSetting.Trim().Length > 0 Then
            ' Validating Email
            If strRequiredFieldSetting.Contains("[EMAIL]") Then
                If Me.State.MyBO.PriceListApprovalEmail Is Nothing OrElse Me.State.MyBO.PriceListApprovalEmail.Trim() = String.Empty Then
                    Me.MasterPage.MessageController.AddErrorAndShow("EMAIL_IS_REQUIRED_ERR")
                    requiredFieldsErrorExist = True
                End If
            End If

        End If

        If requiredFieldsErrorExist Then
            Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Message.MSG_GUI_INVALID_VALUE)
        End If

    End Sub
    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Country(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Country
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub AddFormat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddFormat.Click
        If AvailPostalCodeList.SelectedIndex <> -1 Then
            Dim SelectedItem As System.Web.UI.WebControls.ListItem
            SelectedItem = AvailPostalCodeList.SelectedItem
            SelectedPostalCodeList.Items.Add(SelectedItem)
            AvailPostalCodeList.Items.Remove(SelectedItem)
            AvailPostalCodeList.ClearSelection()
            SelectedPostalCodeList.ClearSelection()
        End If
    End Sub

    Protected Sub RemoveFormat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveFormat.Click
        If SelectedPostalCodeList.SelectedIndex <> -1 Then
            Dim SelectedItem As System.Web.UI.WebControls.ListItem
            SelectedItem = SelectedPostalCodeList.SelectedItem
            AvailPostalCodeList.Items.Add(SelectedItem)
            SelectedPostalCodeList.Items.Remove(SelectedItem)
            SelectedPostalCodeList.ClearSelection()
            AvailPostalCodeList.ClearSelection()
        End If
    End Sub

#End Region

#Region "Page Control Events"
    'Protected Sub cboRegulatoryReporting_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboRegulatoryReporting.SelectedIndexChanged        
    '    If cboRegulatoryReporting.SelectedValue.Equals(Me.State.yesId.ToString) Then
    '        ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
    '        ControlMgr.SetVisibleControl(Me, TextboxLastRegulatoryExtractDate, True)
    '        ControlMgr.SetVisibleControl(Me, LabelLastRegulatoryExtractDate, True)
    '    Else
    '        ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
    '        ControlMgr.SetVisibleControl(Me, TextboxLastRegulatoryExtractDate, False)
    '        ControlMgr.SetVisibleControl(Me, LabelLastRegulatoryExtractDate, False)
    '    End If
    'End Sub
#End Region

#Region "Error Handling"

#End Region

#Region "Ajax related"
    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get
    End Property

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateServiceCenterDropFDC(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateServiceCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COUNTRY")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COUNTRY")
        End If
    End Sub
    'REQ - 6155
    Private Sub ShowHideAllowForceAddressANDAddressConfidenceThreshold()
        'With Me.State.MyBO
        If Not String.IsNullOrEmpty(Me.cboUseAddressValidation.SelectedValue) AndAlso Me.cboUseAddressValidation.SelectedValue.Equals(Me.State.yesCode) Then
            Me.lblAllowForceAddress.Attributes("style") = ""
            Me.cboAllowForceAddress.Attributes("style") = ""
            Me.lblAddressConfidenceThreshold.Attributes("style") = ""
            Me.TextboxAddressConfidenceThreshold.Attributes("style") = ""
        Else
            Me.lblAllowForceAddress.Attributes("style") = "display: none"
            Me.cboAllowForceAddress.Attributes("style") = "display: none"
            Me.lblAddressConfidenceThreshold.Attributes("style") = "display: none"
            Me.TextboxAddressConfidenceThreshold.Attributes("style") = "display: none"
        End If
        'End With
    End Sub
#Region "Country LineOfBusiness"

#Region "Line OF Business Grid Operation"
    Protected Sub btnNewBusinessInfo_Click(sender As Object, e As EventArgs) Handles btnNewBusinessInfo.Click
        Try
            State.LineOfBusinessAction = LineOfbusinessAdd
            Dim newLineOfBusiness As New CountryLineOfBusiness()
            newLineOfBusiness.CountryId = State.MyBO.Id
            State.LineOfBusinessWorkingItem = newLineOfBusiness

            If State.LineOfBusinessList Is Nothing Then
                State.LineOfBusinessList = New Collections.Generic.List(Of CountryLineOfBusiness)
            End If
            State.LineOfBusinessList.Add(newLineOfBusiness)

            moLineOfBusinessGridView.SelectedIndex = State.LineOfBusinessList.Count - 1
            moLineOfBusinessGridView.EditIndex = moLineOfBusinessGridView.SelectedIndex
            PopulateLineOfBusinessGrid(State.LineOfBusinessList)

            EnableDisableButtonsForLineOfBusinessGrid()
            GetSelectedGridDropItem(moLineOfBusinessGridView, LineOfBusinessGridColBusinessType)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub moLineOfBusinessGridView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles moLineOfBusinessGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub moLineOfBusinessGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles moLineOfBusinessGridView.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As CountryLineOfBusiness
            Dim isEditRow As Boolean

            If Not Me.State.UsedLineOfBusinessInContracts.Any() Then
                Me.State.UsedLineOfBusinessInContracts = Me.State.LineOfBusinessOrig.GetUsedLineOfBusinessInContracts(Me.State.MyBO.Id)
            End If

            If Not e.Row.DataItem Is Nothing Then

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then

                    dvRow = CType(e.Row.DataItem, CountryLineOfBusiness)
                    isEditRow = False

                    'edit item, populate dropdown and set value
                    If (State.LineOfBusinessAction = LineOfbusinessAdd OrElse State.LineOfBusinessAction = LineOfbusinessEdit) AndAlso State.LineOfBusinessWorkingItem.Id = dvRow.Id Then

                        Dim listcontext As ListContext = New ListContext()
                        Dim populateOptions As PopulateOptions = New PopulateOptions() With
                        {
                        .AddBlankItem = True
                        }

                        isEditRow = True

                        CType(e.Row.Cells(LineOfBusinessGridColBusinessType).FindControl("ddlBusinessType"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("POLTYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

                        If Not dvRow.LineOfBusinessId = Guid.Empty Then
                            SetSelectedItem(CType(e.Row.Cells(LineOfBusinessGridColBusinessType).FindControl("ddlBusinessType"), DropDownList), dvRow.LineOfBusinessId)
                        End If

                        Dim txtLineBusinessDescription As TextBox
                        Dim txtLineBusinessCode As TextBox
                        Dim chkLineBusinessInUse As CheckBox
                        Dim ddlBusinessType As DropDownList

                        Dim EditButton_WRITE As ImageButton

                        txtLineBusinessCode = CType(e.Row.Cells(LineOfBusinessGridColCode).FindControl("txtLineBusinessCode"), TextBox)
                        txtLineBusinessDescription = CType(e.Row.Cells(LineOfBusinessGridColDescription).FindControl("txtLineBusinessDescription"), TextBox)
                        chkLineBusinessInUse = CType(e.Row.Cells(LineOfBusinessGridColCode).FindControl("chkLineBusinessInUse"), CheckBox)
                        ddlBusinessType = CType(e.Row.Cells(LineOfBusinessGridColCode).FindControl("ddlBusinessType"), DropDownList)
                        EditButton_WRITE = CType(e.Row.Cells(LineOfBusinessGridColEditBtn).FindControl("EditButton_WRITE"), ImageButton)

                        chkLineBusinessInUse.Checked = True
                        chkLineBusinessInUse.Enabled = True

                        If Not dvRow.Code Is Nothing Then
                            txtLineBusinessCode.Text = dvRow.Code.ToString
                        End If

                        If Not dvRow.Description Is Nothing Then
                            txtLineBusinessDescription.Text = dvRow.Description.ToString
                        End If

                        ' If LoB is being used in any contract, dont edit code and description.
                        If Me.State.UsedLineOfBusinessInContracts.Contains(dvRow.Id) Then

                            ControlMgr.SetEnableControl(Me, txtLineBusinessCode, False)
                            ControlMgr.SetEnableControl(Me, txtLineBusinessDescription, False)

                            txtLineBusinessCode.BackColor = Color.DarkGray
                            txtLineBusinessDescription.BackColor = Color.DarkGray

                            txtLineBusinessCode.ForeColor = Color.DarkGray
                            txtLineBusinessDescription.ForeColor = Color.DarkGray

                            ddlBusinessType.Enabled = False

                        End If
                    End If

                    If Not dvRow Is Nothing AndAlso Not isEditRow Then
                        e.Row.Cells(LineOfBusinessGridColBusinessType).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_CONTRACT_POLICY_TYPE, dvRow.LineOfBusinessId)
                    End If

                    If (State.LineOfBusinessAction = LineOfbusinessNone) Then
                        Dim DeleteButton_WRITE As ImageButton
                        DeleteButton_WRITE = CType(e.Row.Cells(LineOfBusinessGridColColDeleteBtn).FindControl("DeleteButton_WRITE"), ImageButton)
                        If Me.State.UsedLineOfBusinessInContracts.Contains(dvRow.Id) Then
                            DeleteButton_WRITE.Visible = False ' Hide delete button if LoB cannot be deleted.
                        End If
                    End If



                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorControllerDS)
        End Try
    End Sub

    Protected Sub moLineOfBusinessGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles moLineOfBusinessGridView.RowCommand

        Dim nIndex As Integer
        Dim guidTemp As Guid

        Try
            If e.CommandName = EDIT_COMMAND_NAME OrElse e.CommandName = DELETE_COMMAND_NAME Then
                guidTemp = New Guid(e.CommandArgument.ToString)
                nIndex = State.LineOfBusinessList.FindIndex(Function(r) r.Id = guidTemp)
                State.LineOfBusinessWorkingItem = State.LineOfBusinessList.Item(nIndex)
            End If

            If e.CommandName = EDIT_COMMAND_NAME Then
                moLineOfBusinessGridView.EditIndex = nIndex
                moLineOfBusinessGridView.SelectedIndex = nIndex
                State.LineOfBusinessAction = LineOfbusinessEdit
                PopulateLineOfBusinessGrid(State.LineOfBusinessList)
                SetFocusInGrid(moLineOfBusinessGridView, nIndex, LineOfBusinessGridColCode)
                EnableDisableButtonsForLineOfBusinessGrid()
            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                State.LineOfBusinessAction = LineOfbusinessDelete
                LineOfBusinessDeleteRecord()
            ElseIf (e.CommandName = SAVE_COMMAND_NAME) Then
                SaveLineOfBusinessRecord()
            ElseIf (e.CommandName = CANCEL_COMMAND_NAME) Then
                LineOfBusinessCancelRecord()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region
#Region "LoB Private Methods"

    Private Sub LineOfBusinessDeleteRecord()

        If Not State.LineOfBusinessWorkingItem.IsNew Then
            'if not new object, delete from database
            State.LineOfBusinessWorkingItem.Delete()
            State.LineOfBusinessWorkingItem.SaveWithoutCheckDsCreator()
        End If

        'remove from list
        State.LineOfBusinessList.Remove(State.LineOfBusinessWorkingItem)

        State.LineOfBusinessAction = LineOfbusinessNone
        moLineOfBusinessGridView.SelectedIndex = -1
        moLineOfBusinessGridView.EditIndex = moLineOfBusinessGridView.SelectedIndex

        State.LineOfBusinessWorkingItem = Nothing
        EnableDisableButtonsForLineOfBusinessGrid()
        PopulateLineOfBusinessGrid(State.LineOfBusinessList)

        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
    End Sub
    Private Sub SaveLineOfBusinessRecord()

        Try
            Dim objDDl As DropDownList, objTxt As TextBox, objChk As CheckBox, chkInUse As String

            objTxt = CType(moLineOfBusinessGridView.Rows(moLineOfBusinessGridView.EditIndex).Cells(LineOfBusinessGridColCode).FindControl("txtLineBusinessCode"), TextBox)
            PopulateBOProperty(State.LineOfBusinessWorkingItem, "Code", objTxt.Text)

            objTxt = CType(moLineOfBusinessGridView.Rows(moLineOfBusinessGridView.EditIndex).Cells(LineOfBusinessGridColDescription).FindControl("txtLineBusinessDescription"), TextBox)
            PopulateBOProperty(State.LineOfBusinessWorkingItem, "Description", objTxt.Text)

            objDDl = CType(moLineOfBusinessGridView.Rows(moLineOfBusinessGridView.EditIndex).Cells(LineOfBusinessGridColBusinessType).FindControl("ddlBusinessType"), DropDownList)
            PopulateBOProperty(State.LineOfBusinessWorkingItem, "LineOfBusinessId", objDDl, True, False)

            objChk = CType(moLineOfBusinessGridView.Rows(moLineOfBusinessGridView.EditIndex).Cells(LineOfBusinessGridColInUse).FindControl("chkLineBusinessInUse"), CheckBox)

            If objChk.Checked = True Then
                chkInUse = Codes.YESNO_Y
            Else
                chkInUse = Codes.YESNO_N
            End If

            PopulateBOProperty(State.LineOfBusinessWorkingItem, "InUse", chkInUse)

            If State.MyBO.IsNew = False Then
                State.LineOfBusinessWorkingItem.SaveWithoutCheckDsCreator()
                State.LineOfBusinessList = Nothing
                State.LineOfBusinessList = CountryLineOfBusiness.GetCountryLineOfBusinessList(State.MyBO.Id)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If

            State.LineOfBusinessAction = LineOfbusinessNone
            moLineOfBusinessGridView.SelectedIndex = -1
            moLineOfBusinessGridView.EditIndex = moLineOfBusinessGridView.SelectedIndex

            State.LineOfBusinessWorkingItem = Nothing
            EnableDisableButtonsForLineOfBusinessGrid()

            PopulateLineOfBusinessGrid(State.LineOfBusinessList)

        Catch ex As Exception
            HandleErrors(ex, ErrorControllerDS)
        End Try
    End Sub

    Private Sub LineOfBusinessCancelRecord()

        Try
            moLineOfBusinessGridView.SelectedIndex = -1
            moLineOfBusinessGridView.EditIndex = moLineOfBusinessGridView.SelectedIndex

            If State.LineOfBusinessAction = LineOfbusinessAdd Then
                State.LineOfBusinessList.Remove(State.LineOfBusinessWorkingItem)
            ElseIf State.LineOfBusinessAction = LineOfbusinessEdit AndAlso (Not State.LineOfBusinessOrig Is Nothing) Then ' set the object to original status
                CopyLineOfbusinessObject(State.LineOfBusinessOrig, State.LineOfBusinessWorkingItem)
            End If

            State.LineOfBusinessAction = LineOfbusinessNone
            State.LineOfBusinessWorkingItem = Nothing

            EnableDisableButtonsForLineOfBusinessGrid()
            PopulateLineOfBusinessGrid(State.LineOfBusinessList)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateLineOfBusinessGrid(ByVal ds As Collections.Generic.List(Of CountryLineOfBusiness))

        Dim blnEmptyList As Boolean = False, mySource As Collections.Generic.List(Of CountryLineOfBusiness)

        If ds Is Nothing OrElse ds.Count = 0 Then
            mySource = New Collections.Generic.List(Of CountryLineOfBusiness)
            mySource.Add(New CountryLineOfBusiness())
            blnEmptyList = True
            moLineOfBusinessGridView.DataSource = mySource
        Else
            moLineOfBusinessGridView.DataSource = ds
        End If

        moLineOfBusinessGridView.DataBind()

        If blnEmptyList Then
            moLineOfBusinessGridView.Rows(0).Visible = False
        End If
    End Sub
    Private Sub EnableDisableButtonsForLineOfBusinessGrid()

        If State.LineOfBusinessAction = LineOfbusinessNone Then 'enable buttons on main form
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            SetGridControls(moLineOfBusinessGridView, True)
            EnableDisableFields()
        Else 'disable buttons on main form 
            ControlMgr.SetEnableControl(Me, btnBack, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)

            SetGridControls(moLineOfBusinessGridView, False)

        End If
    End Sub
    Private Sub CopyLineOfbusinessObject(ByVal objSource As CountryLineOfBusiness, ByVal objCountryLB As CountryLineOfBusiness)

        With objSource
            objCountryLB.Code = .Code
            objCountryLB.Description = .Description

            objCountryLB.LineOfBusinessId = .LineOfBusinessId
            objCountryLB.InUse = .InUse
        End With

    End Sub
    Private Sub ClearLineOfBusinessState()

        With State
            .LineOfBusinessAction = LineOfbusinessNone
            .LineOfBusinessList = Nothing
            .LineOfBusinessWorkingItem = Nothing
            .LineOfBusinessOrig = Nothing
            PopulateLineOfBusinessGrid(.LineOfBusinessList)
        End With

    End Sub
    Private Sub SetLineOfBusinessGridHeaders()

        moLineOfBusinessGridView.Columns(LineOfBusinessGridColBusinessType).HeaderText = "BUSINESS_TYPE"
        moLineOfBusinessGridView.Columns(LineOfBusinessGridColInUse).HeaderText = "IN_USE"

        Me.TranslateGridHeader(moLineOfBusinessGridView)

    End Sub

#End Region
#End Region
End Class



