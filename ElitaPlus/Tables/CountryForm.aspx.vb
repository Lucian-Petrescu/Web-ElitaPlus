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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Country, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
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

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Country(CType(CallingParameters, Guid))
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        MasterPage.MessageController.Clear_Hide()
        'Individual Policy'

        ErrorControllerDS.Clear_Hide()

        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                TranslateGridHeader(moLineOfBusinessGridView)

                UpdateBreadCrum()

                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If State.MyBO Is Nothing Then
                    State.MyBO = New Country
                End If

                PopulateDropdowns()

                If State.IsNew = True Then
                    CreateNew()
                End If

                PopulateFormFromBOs()
                EnableDisableFields()

            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            'Individual Policy'
            ClearGridViewHeadersAndLabelsErrorSign()

            ' Get all used Line of business once.
            If Not State.UsedLineOfBusinessInContracts.Any() Then
                State.UsedLineOfBusinessInContracts = State.LineOfBusinessOrig.GetUsedLineOfBusinessInContracts(State.MyBO.Id)
            End If

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            cboRegulatoryReporting.Attributes.Add("onchange", "ShowHideRegulatoryExtractDate()")
            cboUseAddressValidation.Attributes.Add("onchange", "ShowHideAllowForceAddressANDAddressConfidenceThreshold()")
            ShowHideAllowForceAddressANDAddressConfidenceThreshold()

        Catch ex As Threading.ThreadAbortException

        Catch ex As Exception
            ' Clean Popup Input
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'Now disable depending on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        BindBOPropertyToLabel(State.MyBO, "Code", LabelCode)
        BindBOPropertyToLabel(State.MyBO, "LanguageId", LabelLanguage)
        BindBOPropertyToLabel(State.MyBO, "PrimaryCurrencyId", LabelPrimaryCurrency)
        BindBOPropertyToLabel(State.MyBO, "SecondaryCurrencyId", LabelSecondaryCurrency)
        BindBOPropertyToLabel(State.MyBO, "BankIDLength", LabelBankIDLength)
        BindBOPropertyToLabel(State.MyBO, "BankAcctNoLength", LabelBankAcctNoLength)
        BindBOPropertyToLabel(State.MyBO, "MailAddrFormat", LabelMailAddrFormat)
        BindBOPropertyToLabel(State.MyBO, "ContactInfoReqFields", LabelCONTACT_INFO_REQ_FIELDS)
        BindBOPropertyToLabel(State.MyBO, "AddressInfoReqFields", LabelAddrInfoReqFields)
        BindBOPropertyToLabel(State.MyBO, "EuropeanCountryId", LabelEuropeanCountry)
        BindBOPropertyToLabel(State.MyBO, "ValidateBankInfoId", LabelValidateBankInfo)
        BindBOPropertyToLabel(State.MyBO, "TaxByProductTypeId", LabelTaxByProductType)
        BindBOPropertyToLabel(State.MyBO, "DefaultScForDeniedClaims", lblDefaultSCForDeniedClaims)
        BindBOPropertyToLabel(State.MyBO, "UseBankListId", lblUseBankList)
        BindBOPropertyToLabel(State.MyBO, "RegulatoryReportingId", LabelRegulatoryReporting)
        BindBOPropertyToLabel(State.MyBO, "NotifyGroupEmail", LabelRegulatoryNotifyGroupEmail)
        BindBOPropertyToLabel(State.MyBO, "LastRegulatoryExtractDate", LabelLastRegulatoryExtractDate)
        BindBOPropertyToLabel(State.MyBO, "CreditScoringPct", LabelCreditScoringPct)
        BindBOPropertyToLabel(State.MyBO, "AbnormalCLMFreqNo", LabelAbnormalClmFrqNo)
        BindBOPropertyToLabel(State.MyBO, "CertCountSuspOP", LabelCertCountSuspOp)
        BindBOPropertyToLabel(State.MyBO, "DefaultSSId", lblDefaultSC)
        BindBOPropertyToLabel(State.MyBO, "IsoCode", lblISOCode)
        BindBOPropertyToLabel(State.MyBO, "PriceListApprovalEmail", lblPriceListApprovalEmail)

        'REQ - 6155
        BindBOPropertyToLabel(State.MyBO, "UseAddressValidationXcd", lblUseAddressValidation)
        BindBOPropertyToLabel(State.MyBO, "AllowForceAddressXcd", lblAllowForceAddress)
        BindBOPropertyToLabel(State.MyBO, "AddressConfidenceThreshold", lblAddressConfidenceThreshold)
        BindBOPropertyToLabel(State.MyBO, "AllowForget", lblAllowForget)
        BindBOPropertyToLabel(State.MyBO, "FullNameFormat", lblFullnameFormat)

        'Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListApprovalEmail", Me.lblPriceListApprovalEmail)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceListApprovalNeeded", lblPriceListApprovalNeeded)

        ClearGridHeadersAndLabelsErrSign()
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
        cboUseAddressValidation.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        'Me.cboAllowForceAddress.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        cboAllowForceAddress.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = String.Empty,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        cboAllowForget.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False,
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetExtendedCode
                                                       })

        cboPriceListApprovalNeeded.Populate(yesNoLkl, New PopulateOptions() With
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
        BindListControlToDataView(SelectedPostalCodeList, State.MyBO.GetSelectedPostalCode(), "DESCRIPTION", "ID", False)
        If SelectedPostalCodeList.Items.Count > 0 Then
            SelectedPostalCodeList.SelectedIndex = 0
        End If
        BindListControlToDataView(AvailPostalCodeList, State.MyBO.GetAvailablePostalCode(), "DESCRIPTION", "ID", False)
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
        State.yesId = (From lst In oYesNoList
                          Where lst.Code = "Y"
                          Select lst.ListItemId).FirstOrDefault()
        State.noId = (From lst In oYesNoList
                         Where lst.Code = "N"
                         Select lst.ListItemId).FirstOrDefault()
        State.yesCode = (From lst In oYesNoList
                            Where lst.Code = "Y"
                            Select lst.ExtendedCode).FirstOrDefault()
        State.noCode = (From lst In oYesNoList
                           Where lst.Code = "N"
                           Select lst.ExtendedCode).FirstOrDefault()

        Dim fullnameformat As ListItem() = CommonConfigManager.Current.ListManager.GetList("FULL_NAME_FMT", Thread.CurrentPrincipal.GetLanguageCode())
        State.fsmslCode = (From lst In fullnameformat
                              Where lst.Code = "FSMSL"
                              Select lst.ExtendedCode).FirstOrDefault()
        With State.MyBO
            PopulateControlFromBOProperty(TextboxDescription, .Description)
            PopulateControlFromBOProperty(TextboxCode, .Code)
            SetSelectedItem(cboLanguageId, .LanguageId)
            SetSelectedItem(cboPrimaryCurrencyId, .PrimaryCurrencyId)
            SetSelectedItem(cboSecondaryCurrencyId, .SecondaryCurrencyId)
            PopulateControlFromBOProperty(TextboxBankIDLength, .BankIDLength)
            PopulateControlFromBOProperty(TextboxBankAcctNoLength, .BankAcctNoLength)
            PopulateControlFromBOProperty(TextboxMailAddrFormat, .MailAddrFormat)
            PopulateControlFromBOProperty(TextboxCONTACT_INFO_REQ_FIELDS, .ContactInfoReqFields)
            PopulateControlFromBOProperty(TextboxAddrInfoReqFields, .AddressInfoReqFields)
            SetSelectedItem(cboEuropeanCountry, .EuropeanCountryId)
            SetSelectedItem(cboValidateBankInfo, .ValidateBankInfoId)

            If .TaxByProductTypeId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboTaxByProductType, State.noId)
            Else
                SetSelectedItem(cboTaxByProductType, .TaxByProductTypeId)
            End If

            'DEF-22411-START
            'Me.SetSelectedItem(Me.cboUseBankList, .UseBankListId)
            If .UseBankListId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboUseBankList, State.noId)
            Else
                SetSelectedItem(cboUseBankList, .UseBankListId)
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
                SetSelectedItem(cboByteCheck, State.noId)
            Else
                SetSelectedItem(cboByteCheck, .RequireByteCheckId)
            End If

            PopulateControlFromBOProperty(TextboxLastRegulatoryExtractDate, .LastRegulatoryExtractDate)
            PopulateControlFromBOProperty(TextboxRegulatoryNotifyGroupEmail, .NotifyGroupEmail)
            SetSelectedItem(cboRegulatoryReporting, .RegulatoryReportingId)

            PopulateControlFromBOProperty(TextboxCreditScoringPct, .CreditScoringPct)
            PopulateControlFromBOProperty(TextboxAbnormalClmFrqNo, .AbnormalCLMFreqNo)
            PopulateControlFromBOProperty(TextboxCertCountSuspOp, .CertCountSuspOP)

            If Not .RegulatoryReportingId.Equals(System.Guid.Empty) AndAlso .RegulatoryReportingId.Equals(State.yesId) Then
                ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
                LabelLastRegulatoryExtractDate.Attributes("style") = ""
                TextboxLastRegulatoryExtractDate.Attributes("style") = ""
            Else
                ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
                LabelLastRegulatoryExtractDate.Attributes("style") = "display: none"
                TextboxLastRegulatoryExtractDate.Attributes("style") = "display: none"
            End If

            'REQ
            If String.IsNullOrEmpty(.UseAddressValidationXcd) Then
                SetSelectedItem(cboUseAddressValidation, State.noCode)
            Else
                SetSelectedItem(cboUseAddressValidation, .UseAddressValidationXcd)
            End If

            If String.IsNullOrEmpty(.AllowForceAddressXcd) Then
                SetSelectedItem(cboAllowForceAddress, State.noCode)
            Else
                SetSelectedItem(cboAllowForceAddress, .AllowForceAddressXcd)
            End If

            If String.IsNullOrEmpty(.AllowForget) Then
                SetSelectedItem(cboAllowForget, State.noCode)
            Else
                SetSelectedItem(cboAllowForget, .AllowForget)
            End If

            If String.IsNullOrEmpty(.PriceListApprovalNeeded) Then
                SetSelectedItem(cboPriceListApprovalNeeded, State.noCode)
            Else
                SetSelectedItem(cboPriceListApprovalNeeded, .PriceListApprovalNeeded)
            End If

            If String.IsNullOrEmpty(.FullNameFormat) Then
                SetSelectedItem(cboFullNameFormat, State.fsmslCode)
            Else
                SetSelectedItem(cboFullNameFormat, .FullNameFormat)
            End If


            PopulateControlFromBOProperty(TextboxAddressConfidenceThreshold, .AddressConfidenceThreshold)
            PopulateControlFromBOProperty(txtISOCode, .IsoCode)

            If .PriceListApprovalEmail Is Nothing OrElse .PriceListApprovalEmail.Equals(String.Empty) Then
                State.PriceListAprrovalEmailIsNull = TranslationBase.TranslateLabelOrMessage("THERE_IS_NO_VALUE")
                PopulateControlFromBOProperty(txtPriceListApprovalEmail, State.PriceListAprrovalEmailIsNull)
            Else
                PopulateControlFromBOProperty(txtPriceListApprovalEmail, .PriceListApprovalEmail)
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
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "Code", TextboxCode)
            PopulateBOProperty(State.MyBO, "LanguageId", cboLanguageId)
            PopulateBOProperty(State.MyBO, "PrimaryCurrencyId", cboPrimaryCurrencyId)
            PopulateBOProperty(State.MyBO, "SecondaryCurrencyId", cboSecondaryCurrencyId)
            PopulateBOProperty(State.MyBO, "BankIDLength", TextboxBankIDLength)
            PopulateBOProperty(State.MyBO, "BankAcctNoLength", TextboxBankAcctNoLength)
            PopulateBOProperty(State.MyBO, "MailAddrFormat", TextboxMailAddrFormat)
            PopulateBOProperty(State.MyBO, "ContactInfoReqFields", TextboxCONTACT_INFO_REQ_FIELDS)
            PopulateBOProperty(State.MyBO, "AddressInfoReqFields", TextboxAddrInfoReqFields)
            PopulateBOProperty(State.MyBO, "EuropeanCountryId", cboEuropeanCountry)
            PopulateBOProperty(State.MyBO, "ValidateBankInfoId", cboValidateBankInfo)
            PopulateBOProperty(State.MyBO, "TaxByProductTypeId", cboTaxByProductType)
            PopulateBOProperty(State.MyBO, "UseBankListId", cboUseBankList)
            PopulateBOProperty(State.MyBO, "RegulatoryReportingId", cboRegulatoryReporting)
            PopulateBOProperty(State.MyBO, "NotifyGroupEmail", TextboxRegulatoryNotifyGroupEmail)
            PopulateBOProperty(State.MyBO, "CreditScoringPct", TextboxCreditScoringPct)
            PopulateBOProperty(State.MyBO, "AbnormalCLMFreqNo", TextboxAbnormalClmFrqNo)
            PopulateBOProperty(State.MyBO, "CertCountSuspOP", TextboxCertCountSuspOp)

            If txtDefaultSCForDeniedClaims.Text.Trim <> String.Empty AndAlso txtDefaultSCForDeniedClaims.Text.Trim.ToUpper <> inpDefaultSCFDCDesc.Value.Trim.ToUpper Then
                ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC_FOR_DENIED_CLAIMS"), txtDefaultSCForDeniedClaims, lblDefaultSCForDeniedClaims, New Exception("Service Center Not Found")))
            Else
                AjaxController.PopulateBOAutoComplete(txtDefaultSCForDeniedClaims, inpDefaultSCFDCDesc,
                            inpDefaultSCFDCId, State.MyBO, "DefaultScForDeniedClaims")
            End If

            'REQ-5546
            If txtDefaultSC.Text.Trim <> String.Empty AndAlso txtDefaultSC.Text.Trim.ToUpper <> inpDefaultSCDesc.Value.Trim.ToUpper Then
                ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC"), txtDefaultSC, lblDefaultSC, New Exception("Service Center Not Found")))
            Else
                AjaxController.PopulateBOAutoComplete(txtDefaultSC, inpDefaultSCDesc,
                            inpDefaultSCId, State.MyBO, "DefaultSCId")
            End If

            ' if actual selected rows are not in the selected list, they must have been removed. 
            Dim selPostalCode As DataView = State.MyBO.GetSelectedPostalCode()
            For iRow As Integer = 0 To selPostalCode.Count - 1
                Dim postalCodeID As Guid = New Guid(CType(selPostalCode(iRow)("ID"), Byte()))

                If SelectedPostalCodeList.Items.FindByValue(postalCodeID.ToString) Is Nothing Then
                    State.MyBO.DetachPostalCodeFormat(postalCodeID.ToString)
                End If
            Next

            ' if actual available rows are not in the available list, they must have been added . 
            Dim availPostalCode As DataView = State.MyBO.GetAvailablePostalCode()
            For iRow As Integer = 0 To availPostalCode.Count - 1
                Dim postalCodeID As Guid = New Guid(CType(availPostalCode(iRow)("ID"), Byte()))

                If AvailPostalCodeList.Items.FindByValue(postalCodeID.ToString) Is Nothing Then
                    State.MyBO.AttachPostalCodeFormat(postalCodeID.ToString)
                End If
            Next

            PopulateBOProperty(State.MyBO, "RequireByteCheckId", cboByteCheck)
            PopulateBOProperty(State.MyBO, "RequireByteCheckId", cboByteCheck)

            'REQ
            PopulateBOProperty(State.MyBO, "UseAddressValidationXcd", cboUseAddressValidation, False, True)
            If Not String.IsNullOrEmpty(cboUseAddressValidation.SelectedValue) AndAlso cboUseAddressValidation.SelectedValue.Equals(State.yesCode) Then
                PopulateBOProperty(State.MyBO, "AllowForceAddressXcd", cboAllowForceAddress, False, True)
                PopulateBOProperty(State.MyBO, "AddressConfidenceThreshold", TextboxAddressConfidenceThreshold)
            Else
                PopulateBOProperty(State.MyBO, "AllowForceAddressXcd", String.Empty)
                PopulateBOProperty(State.MyBO, "AddressConfidenceThreshold", String.Empty)
            End If

            PopulateBOProperty(State.MyBO, "IsoCode", txtISOCode)

            PopulateBOProperty(State.MyBO, "AllowForget", cboAllowForget, False, True)

            PopulateBOProperty(State.MyBO, "PriceListApprovalNeeded", cboPriceListApprovalNeeded, False, True)

            If cboPriceListApprovalNeeded.SelectedValue.Equals(State.noCode) Then
                State.PriceListAprrovalEmailIsNull = TranslationBase.TranslateLabelOrMessage("THERE_IS_NO_VALUE")
                PopulateControlFromBOProperty(txtPriceListApprovalEmail, State.PriceListAprrovalEmailIsNull)
            Else
                PopulateControlFromBOProperty(txtPriceListApprovalEmail, .PriceListApprovalEmail)
            End If

            PopulateBOProperty(State.MyBO, "FullNameFormat", cboFullNameFormat, False, True)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New Country
        PopulateFormFromBOs()
        EnableDisableFields()
        'Individual Policy
        ClearLineOfBusinessState()
    End Sub

    Protected Sub CreateNewWithCopy()

        TextboxDescription.Text = ""
        TextboxCode.Text = ""

        inpDefaultSCFDCId.Value = ""
        inpDefaultSCFDCDesc.Value = ""
        txtDefaultSCForDeniedClaims.Text = ""

        inpDefaultSCId.Value = ""
        inpDefaultSCDesc.Value = ""
        txtDefaultSC.Text = ""

        State.MyBO = New Country
        PopulateBOsFormFrom()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New Country
        State.ScreenSnapShotBO.Clone(State.MyBO)

        'If SelectedPostalCodeList.Items.Count > 0 Then
        '    Me.State.MyBO.AttachPostalCodeFormat(GetListValues(SelectedPostalCodeList))
        'End If

        txtISOCode.Text = ""

        LoadPostalCodeLists()
        'Individual Policy
        ClearLineOfBusinessState()
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
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
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
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""

        'Hide Client controls
        If Not State.MyBO.RegulatoryReportingId.Equals(System.Guid.Empty) AndAlso State.MyBO.RegulatoryReportingId.Equals(State.yesId) Then
            ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
            LabelLastRegulatoryExtractDate.Attributes("style") = ""
            TextboxLastRegulatoryExtractDate.Attributes("style") = ""
        Else
            ControlMgr.SetEnableControl(Me, TextboxLastRegulatoryExtractDate, False)
            LabelLastRegulatoryExtractDate.Attributes("style") = "display: none"
            TextboxLastRegulatoryExtractDate.Attributes("style") = "display: none"
        End If

    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            ' Validate User Selected Required Fields
            ValidateRequiredFields()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION) 'DEF-21578
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED) 'DEF-21578
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ValidateRequiredFields()
        Dim requiredFieldsErrorExist As Boolean = False
        Dim strRequiredFieldSetting As String = String.Empty

        If strRequiredFieldSetting.Trim().Length > 0 Then
            ' Validating Email
            If strRequiredFieldSetting.Contains("[EMAIL]") Then
                If State.MyBO.PriceListApprovalEmail Is Nothing OrElse State.MyBO.PriceListApprovalEmail.Trim() = String.Empty Then
                    MasterPage.MessageController.AddErrorAndShow("EMAIL_IS_REQUIRED_ERR")
                    requiredFieldsErrorExist = True
                End If
            End If

        End If

        If requiredFieldsErrorExist Then
            Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Message.MSG_GUI_INVALID_VALUE)
        End If

    End Sub
    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Country(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Country
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse) 'DEF-21578
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub AddFormat_Click(sender As Object, e As EventArgs) Handles AddFormat.Click
        If AvailPostalCodeList.SelectedIndex <> -1 Then
            Dim SelectedItem As System.Web.UI.WebControls.ListItem
            SelectedItem = AvailPostalCodeList.SelectedItem
            SelectedPostalCodeList.Items.Add(SelectedItem)
            AvailPostalCodeList.Items.Remove(SelectedItem)
            AvailPostalCodeList.ClearSelection()
            SelectedPostalCodeList.ClearSelection()
        End If
    End Sub

    Protected Sub RemoveFormat_Click(sender As Object, e As EventArgs) Handles RemoveFormat.Click
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
    Public Shared Function PopulateServiceCenterDropFDC(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateServiceCenterDrop(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(AjaxState.MyBO.Id)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COUNTRY")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COUNTRY")
        End If
    End Sub
    'REQ - 6155
    Private Sub ShowHideAllowForceAddressANDAddressConfidenceThreshold()
        'With Me.State.MyBO
        If Not String.IsNullOrEmpty(cboUseAddressValidation.SelectedValue) AndAlso cboUseAddressValidation.SelectedValue.Equals(State.yesCode) Then
            lblAllowForceAddress.Attributes("style") = ""
            cboAllowForceAddress.Attributes("style") = ""
            lblAddressConfidenceThreshold.Attributes("style") = ""
            TextboxAddressConfidenceThreshold.Attributes("style") = ""
        Else
            lblAllowForceAddress.Attributes("style") = "display: none"
            cboAllowForceAddress.Attributes("style") = "display: none"
            lblAddressConfidenceThreshold.Attributes("style") = "display: none"
            TextboxAddressConfidenceThreshold.Attributes("style") = "display: none"
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

            If Not State.UsedLineOfBusinessInContracts.Any() Then
                State.UsedLineOfBusinessInContracts = State.LineOfBusinessOrig.GetUsedLineOfBusinessInContracts(State.MyBO.Id)
            End If

            If e.Row.DataItem IsNot Nothing Then

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

                        If dvRow.Code IsNot Nothing Then
                            txtLineBusinessCode.Text = dvRow.Code.ToString
                        End If

                        If dvRow.Description IsNot Nothing Then
                            txtLineBusinessDescription.Text = dvRow.Description.ToString
                        End If

                        ' If LoB is being used in any contract, dont edit code and description.
                        If State.UsedLineOfBusinessInContracts.Contains(dvRow.Id) Then

                            ControlMgr.SetEnableControl(Me, txtLineBusinessCode, False)
                            ControlMgr.SetEnableControl(Me, txtLineBusinessDescription, False)

                            txtLineBusinessCode.BackColor = Color.DarkGray
                            txtLineBusinessDescription.BackColor = Color.DarkGray

                            txtLineBusinessCode.ForeColor = Color.DarkGray
                            txtLineBusinessDescription.ForeColor = Color.DarkGray

                            ddlBusinessType.Enabled = False

                        End If
                    End If

                    If dvRow IsNot Nothing AndAlso Not isEditRow Then
                        e.Row.Cells(LineOfBusinessGridColBusinessType).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_CONTRACT_POLICY_TYPE, dvRow.LineOfBusinessId)
                    End If

                    If (State.LineOfBusinessAction = LineOfbusinessNone) Then
                        Dim DeleteButton_WRITE As ImageButton
                        DeleteButton_WRITE = CType(e.Row.Cells(LineOfBusinessGridColColDeleteBtn).FindControl("DeleteButton_WRITE"), ImageButton)
                        If State.UsedLineOfBusinessInContracts.Contains(dvRow.Id) Then
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
            ElseIf State.LineOfBusinessAction = LineOfbusinessEdit AndAlso (State.LineOfBusinessOrig IsNot Nothing) Then ' set the object to original status
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
    Private Sub PopulateLineOfBusinessGrid(ds As Collections.Generic.List(Of CountryLineOfBusiness))

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
    Private Sub CopyLineOfbusinessObject(objSource As CountryLineOfBusiness, objCountryLB As CountryLineOfBusiness)

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

        TranslateGridHeader(moLineOfBusinessGridView)

    End Sub

#End Region
#End Region
End Class



