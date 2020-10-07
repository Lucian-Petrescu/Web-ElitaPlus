Imports System.Threading
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Partial Public Class CertAddDetailsForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/Certificates/CertAddDetailsForm.aspx"
    Public Const PAGETITLE As String = "DETAIL" '"ADD_CERTIFICATE"
    Public Const PAGETAB As String = "ADD_CERTIFICATE" '"CERTIFICATES"
    Public Const MSG_PROMPT_FOR_ADD_MORE_BUNDLE_ITEMS As String = "MSG_PROMPT_FOR_ADD_MORE_BUNDLE_ITEMS"
    Public Const MSG_PROMPT_FOR_PROCEDD_TO_CERT_DETAIL As String = "PROCEED_TO_CERT_DETAIL"
    Public Const CODE As String = "Code"
    Public Const DESCRIPTION As String = "Description"
    Public Const FIRST_ROW As Integer = 0
    Public Const INSURANCE_ACTIVATION_DATE As String = "INSURANCE_ACTIVATION_DATE"
    Public Const WARR_SALES_DATE As String = "WARR_SALES_DATE"
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public DealerID As Guid
        Public DealerName As String

        Public Sub New(DealerID As Guid, DealerName As String)
            Me.DealerID = DealerID
            Me.DealerName = DealerName
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    ' This class keeps the current state for the search page.
    Class MyState
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public DealerID As Guid
        Public DealerName As String
        Public MailAddressFormat As String
        Public MyBO As CertAddController
        Public IsCertSaved As Boolean = False
        Public ColErrLabels As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
        'Public ProdCodeList As DataView = Nothing
        Public ProdCodeList As DataView = Nothing
        Public ProdDesc As String = Nothing
        Public AllowBundle As String = "N"
        Public CertID As Guid = Guid.Empty
        Public InstallmentAllowed As String
        Public UseInstallmentDef As String
        Public isSalutation As Boolean
        'Public UseDeliveryDate As Boolean
        Public salnForUserLang As String
        Public Dealer As Dealer
        Public Company As Company
        Public MaritalStatusList As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Public NationalityList As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Public PlaceOfBirthList As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Public GenderList As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Public PersonTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem()

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Page events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        CheckIfComingFromSaveConfirm()
        'Clear script and set focus to the first elements
        LitScript.Text = "<script>document.getElementById('" & txtCertNum.ClientID & "').focus();</script>"
        Dim blnHasContract As Boolean = True
        Try
            If Not IsPostBack Then

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                AddCalendar(btnWarrSalesDate, txtWarrSalesDate)
                AddCalendar(btnProdSalesDate, txtProdSalesDate)
                AddCalendar(btnDateOfBirth, txtDateOfBirth)

                If State.MyBO Is Nothing Then
                    State.MyBO = New CertAddController()
                End If

                Dim currencycode As String, countrycode As String
                Try
                    CertAddController.GetDealerDetailsForCertADD(State.DealerID, currencycode, countrycode, State.MailAddressFormat, State.InstallmentAllowed, State.UseInstallmentDef)
                    State.Dealer = New Dealer(State.DealerID)
                    State.Company = New Company(State.Dealer.CompanyId)
                    With State.MyBO
                        .CurrencyISOCode = currencycode
                        .PurchaseCountryISOCode = countrycode
                        .CustomerCountryISOCode = countrycode
                    End With
                    PopulatePaymentType()
                    PopulateAMLControls()
                    ShowHideControls()
                Catch ex As DataNotFoundException
                    ErrControllerMaster.AddErrorAndShow(ex.Message, True)
                    blnHasContract = False
                End Try

                Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                State.isSalutation = GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, companyBO.SalutationId)
                If blnHasContract Then PopulateFormFromBOs()
                'End If -- COMMENTED FOR DEF-1115 to include the validation in not is postback
                'START  DEF-1115 Show Contract Validation on Add Certificate
                Dim oContract As Contract
                oContract = Contract.GetContract(State.MyBO.DealerID, System.DateTime.Now)
                If oContract Is Nothing Then
                    'START  DEF-1115 Show Contract Validation on Add Certificate   
                    Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.NO_CONTRACT_FOUND, GetType(Contract), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(Contract).FullName, "Test")
                    'END  DEF-1115 Show Contract Validation on Add Certificate 
                Else
                    lblWarrSalesDate.Text = TranslationBase.TranslateLabelOrMessage(WARR_SALES_DATE)

                    If oContract.MarketingPromotionId = Guid.Empty OrElse Not (GetYesNo(State.Company.LanguageId, oContract.MarketingPromotionId)) Then
                        If (IsNewUI) Then
                            If moMarketingPromoNumLabel.Text.StartsWith("<span class=""mandatory"">*</span> ") Then
                                moMarketingPromoNumLabel.Text = moMarketingPromoNumLabel.Text.Replace("<span class=""mandatory"">*</span> ", "")
                            End If
                            If moMarketingPromoSerLabel.Text.StartsWith("<span class=""mandatory"">*</span> ") Then
                                moMarketingPromoSerLabel.Text = moMarketingPromoSerLabel.Text.Replace("<span class=""mandatory"">*</span> ", "")
                            End If
                        Else
                            If moMarketingPromoNumLabel.Text.StartsWith("* ") Then
                                moMarketingPromoNumLabel.Text = moMarketingPromoNumLabel.Text.Replace("* ", "")
                            End If
                            If moMarketingPromoSerLabel.Text.StartsWith("* ") Then
                                moMarketingPromoSerLabel.Text = moMarketingPromoSerLabel.Text.Replace("* ", "")
                            End If
                        End If
                    Else
                        If (IsNewUI) Then
                            If Not moMarketingPromoNumLabel.Text.StartsWith("<span class=""mandatory"">*</span> ") Then
                                moMarketingPromoNumLabel.Text = "<span class=""mandatory"">*</span> " & moMarketingPromoNumLabel.Text
                            End If
                            If Not moMarketingPromoSerLabel.Text.StartsWith("<span class=""mandatory"">*</span> ") Then
                                moMarketingPromoSerLabel.Text = "<span class=""mandatory"">*</span> " & moMarketingPromoSerLabel.Text
                            End If
                        Else
                            If Not moMarketingPromoNumLabel.Text.StartsWith("* ") Then
                                moMarketingPromoNumLabel.Text = "* " & moMarketingPromoNumLabel.Text
                            End If
                            If Not moMarketingPromoSerLabel.Text.StartsWith("* ") Then
                                moMarketingPromoSerLabel.Text = "* " & moMarketingPromoSerLabel.Text
                            End If
                        End If
                    End If
                    'END  DEF-1115 Show Contract Validation on Add Certificate
                End If 'DEF-1115
            End If

            txtProdDesc.Attributes.Add("readonly", "readonly")
            ddlProdCodeInfo.Attributes.Add("display", "none")

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
        ClearErrorLabels()
    End Sub
    Private Sub ShowHideControls()
        If State.MyBO.UseCustomerProfile = Codes.YESNO_Y Then 
            moNotUseCustomerProfile.Attributes("style") = "display: none"
        Else
            moUseCustomerProfile1.Attributes("style") = "display: none"
            moUseCustomerProfile2.Attributes("style") = "display: none"
        End If
        If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.Dealer.DealerTypeId) = "3" Then
            PopulateMobileDealerDropdown()
        Else
            mobileDealerFields1.Attributes("style") = "display: none"
            mobileDealerFields2.Attributes("style") = "display: none"
        End If
    End Sub
    Private Sub PopulateAMLControls()
        If State.Company.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) _
        AndAlso State.Dealer.RequireCustomerAMLInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CAIT, "0")) Then '0= None 

            moAMLRegulations1.Attributes("style") = "display: none"
            moAMLRegulations2.Attributes("style") = "display: none"
            moAMLRegulations3.Attributes("style") = "display: none"
            moAMLRegulations4.Attributes("style") = "display: none"
        Else
            PopulateMaritalStatusDropdown(ddlMaritalStatus)
            PopulateNationalityDropdown(ddlNationality)
            PopulatePlaceOfBirthDropdown(ddlPlaceOfBirth)
            PopulateGenderDropdown(ddlGender)
            PopulatePersonTypeDropdown(ddlPersonType)

        End If

    End Sub
    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                objParam = CType(CallingParameters, Parameters)
                State.DealerID = objParam.DealerID
                State.DealerName = objParam.DealerName
                State.MyBO = New CertAddController(State.DealerID)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster, False)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Dim strBundleURL() As String = CertAddBundleItemForm.URL.ToUpper.Split("/"c)
        Dim strFrmURL() As String = ReturnFromUrl.ToUpper.Split("/"c)

        'return from bundle screen
        If strFrmURL(strFrmURL.Count - 1) = strBundleURL(strBundleURL.Count - 1) Then
            IsReturningFromChild = True
            If ReturnPar IsNot Nothing Then
                State.MyBO = CType(ReturnPar, CertAddController)
            End If
        End If
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            If State.AllowBundle <> "Y" Then
                LitScript.Text = LitScript.Text & "<script>HideBundleGrid(true);</script>"
            Else
                LitScript.Text = LitScript.Text & "<script>HideBundleGrid(false);</script>"
            End If
            If State.InstallmentAllowed <> "Y" OrElse ddlPYMTType.SelectedValue <> "6" Then
                LitScript.Text = LitScript.Text & "<script>ShowInstallInfo(false, false);</script>"
            ElseIf State.UseInstallmentDef = "Y" Then
                LitScript.Text = LitScript.Text & "<script>ShowInstallInfo(true, false);</script>"
            Else
                LitScript.Text = LitScript.Text & "<script>ShowInstallInfo(true, true);</script>"
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "helper functions"
    Private Sub PopulatePaymentType()
        ddlPYMTType.Items.Clear()
        ddlPYMTType.Items.Add(New System.Web.UI.WebControls.ListItem("1 - " & TranslationBase.TranslateLabelOrMessage("CASH"), "1"))
        If State.InstallmentAllowed = "Y" Then
            ddlPYMTType.Items.Add(New System.Web.UI.WebControls.ListItem("6 - " & TranslationBase.TranslateLabelOrMessage("DIRECT_DEBIT"), "6"))
            ddlPYMTType.Enabled = True
            If State.UseInstallmentDef = "Y" Then
                ddlPYMTType.Attributes.Add("onchange", "PYMTTypeChanged(false)")
            Else
                ddlPYMTType.Attributes.Add("onchange", "PYMTTypeChanged(true)")
                populateBillingFrequency()
            End If

        Else
            ddlPYMTType.Enabled = False
        End If
    End Sub

    Private Sub populateBillingFrequency()
        ' Dim dv As DataView
        'dv = LookupListNew.DropdownLookupList("BLFQ", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        ddlBillFreq.Items.Clear()
        'BindListTextToDataView(Me.ddlBillFreq, dv, "DESCRIPTION", "CODE", False)
        Dim billFreqLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("BLFQ", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        ddlBillFreq.Populate(billFreqLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = False,
                                           .ValueFunc = AddressOf .GetCode
                                 })
        ddlBillFreq.SelectedValue = "0"
    End Sub

    Private Sub PopulateMobileDealerDropdown()
        'BindCodeToListControl(ddlSubscriberStatus, LookupListNew.DropdownLookupList("SUBSTAT", Authentication.LangId, True), "DESCRIPTION", "CODE")
        Dim subscribersLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SUBSTAT", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        ddlSubscriberStatus.Populate(subscribersLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = True,
                                           .ValueFunc = AddressOf .GetCode
                                 })
        ' BindCodeToListControl(ddlMembershipType, LookupListNew.DropdownLookupList("MEMTYPE", Authentication.LangId, True), "DESCRIPTION", "CODE")
        Dim membershipTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MEMTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        ddlMembershipType.Populate(membershipTypeLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = True,
                                           .ValueFunc = AddressOf .GetCode
                                 })

    End Sub
    Protected Sub EnableDisablePageControls()

        If Not State.isSalutation Then
            ControlMgr.SetVisibleControl(Me, moSalutationLabel, False)
            ControlMgr.SetVisibleControl(Me, laSemi, False)
            cboSalutationId.Visible = False
        End If

        If State.IsCertSaved Then 'view only mode
            btnSave.Visible = False
            btnNew.Visible = True
            btnCopyNew.Visible = True
            ddlProdCode.Visible = False
            ddlProdCodeInfo.Visible = False
            txtProdCode.Visible = True
            ddlManufacturer.Visible = False
            txtManufacturer.Visible = True
            If State.isSalutation Then
                txtSalutation.Visible = True
            End If

            btnProdSalesDate.Visible = False
            btnWarrSalesDate.Visible = False
            btnDateOfBirth.Visible = False
            btnBundle.Visible = False
            'installment
            ddlBillFreq.Visible = False
            txtBillFreq.Visible = True
            ddlPYMTType.Visible = False
            txtPYMTType.Visible = True
            If State.isSalutation Then
                ControlMgr.SetVisibleControl(Me, moSalutationLabel, True)
                ControlMgr.SetVisibleControl(Me, laSemi, True)
                cboSalutationId.Visible = False
            End If
        Else 'Edit mode
            btnSave.Visible = True
            btnNew.Visible = False
            btnCopyNew.Visible = False
            ddlProdCode.Visible = True
            ddlProdCodeInfo.Visible = True
            txtProdCode.Visible = False
            ddlManufacturer.Visible = True
            txtManufacturer.Visible = False
            btnProdSalesDate.Visible = True
            btnWarrSalesDate.Visible = True
            btnDateOfBirth.Visible = True
            btnBundle.Visible = True
            'installment
            ddlBillFreq.Visible = True
            txtBillFreq.Visible = False
            ddlPYMTType.Visible = True
            txtPYMTType.Visible = False
            If State.isSalutation Then
                ControlMgr.SetVisibleControl(Me, moSalutationLabel, True)
                ControlMgr.SetVisibleControl(Me, laSemi, True)
                ControlMgr.SetVisibleControl(Me, cboSalutationId, True)
            End If

        End If

        If State.MailAddressFormat = "" OrElse State.MailAddressFormat.IndexOf("[ZIP]") > -1 Then
            lblZIP.Visible = True
            If lblZIP.Text.Trim.IndexOf(":") = -1 Then
                lblZIP.Text = lblZIP.Text & ":"
            End If
            txtZIP.Visible = True
        Else
            lblZIP.Visible = False
            txtZIP.Visible = False
        End If
        If State.MailAddressFormat = "" OrElse State.MailAddressFormat.IndexOf("[RGCODE]") > -1 OrElse State.MailAddressFormat.IndexOf("[RGNAME]") > -1 Then
            lblState.Visible = True
            If lblState.Text.Trim.IndexOf(":") = -1 Then
                lblState.Text = lblState.Text & ":"
            End If
            txtState.Visible = True
        Else
            lblState.Visible = False
            txtState.Visible = False
        End If

        EnableDisableTextBoxes(State.IsCertSaved)
    End Sub

    Protected Sub EnableDisableTextBoxes(blnReadOnly As Boolean)
        txtCertNum.ReadOnly = blnReadOnly
        txtCertNum.CssClass = GetTextBoxStyle(blnReadOnly)
        txtWarrSalesDate.ReadOnly = blnReadOnly
        txtWarrSalesDate.CssClass = GetTextBoxStyle(blnReadOnly)
        txtProdSalesDate.ReadOnly = blnReadOnly
        txtProdSalesDate.CssClass = GetTextBoxStyle(blnReadOnly)
        txtCertDuration.ReadOnly = blnReadOnly
        txtCertDuration.CssClass = GetTextBoxStyle(blnReadOnly)
        txtMfgWarranty.ReadOnly = blnReadOnly
        txtMfgWarranty.CssClass = GetTextBoxStyle(blnReadOnly)
        txtWarrPrice.ReadOnly = blnReadOnly
        txtWarrPrice.CssClass = GetTextBoxStyle(blnReadOnly)

        txtDateOfBirth.ReadOnly = blnReadOnly
        txtDateOfBirth.CssClass = GetTextBoxStyle(blnReadOnly)

        txtRetailPrice.ReadOnly = blnReadOnly
        txtRetailPrice.CssClass = GetTextBoxStyle(blnReadOnly)
        txtInvNum.ReadOnly = blnReadOnly
        txtInvNum.CssClass = GetTextBoxStyle(blnReadOnly)
        txtBranchCode.ReadOnly = blnReadOnly
        txtBranchCode.CssClass = GetTextBoxStyle(blnReadOnly)
        txtSalesRepNum.ReadOnly = blnReadOnly
        txtSalesRepNum.CssClass = GetTextBoxStyle(blnReadOnly)
        txtModel.ReadOnly = blnReadOnly
        txtModel.CssClass = GetTextBoxStyle(blnReadOnly)
        txtSerialNum.ReadOnly = blnReadOnly
        txtSerialNum.CssClass = GetTextBoxStyle(blnReadOnly)
        txtItemCode.ReadOnly = blnReadOnly
        txtItemCode.CssClass = GetTextBoxStyle(blnReadOnly)
        txtItemDesc.ReadOnly = blnReadOnly
        txtItemDesc.CssClass = GetTextBoxStyle(blnReadOnly)

        txtCustName.ReadOnly = blnReadOnly
        txtCustName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtFirstName.ReadOnly = blnReadOnly
        txtFirstName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtMiddleName.ReadOnly = blnReadOnly
        txtMiddleName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtLastName.ReadOnly = blnReadOnly
        txtLastName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtTaxID.ReadOnly = blnReadOnly
        txtTaxID.CssClass = GetTextBoxStyle(blnReadOnly)
        txtHomePhone.ReadOnly = blnReadOnly
        txtHomePhone.CssClass = GetTextBoxStyle(blnReadOnly)
        txtWorkPhone.ReadOnly = blnReadOnly
        txtWorkPhone.CssClass = GetTextBoxStyle(blnReadOnly)
        txtEmail.ReadOnly = blnReadOnly
        txtEmail.CssClass = GetTextBoxStyle(blnReadOnly)
        txtAddress1.ReadOnly = blnReadOnly
        txtAddress1.CssClass = GetTextBoxStyle(blnReadOnly)
        txtAddress2.ReadOnly = blnReadOnly
        txtAddress2.CssClass = GetTextBoxStyle(blnReadOnly)
        txtCity.ReadOnly = blnReadOnly
        txtCity.CssClass = GetTextBoxStyle(blnReadOnly)
        txtState.ReadOnly = blnReadOnly
        txtState.CssClass = GetTextBoxStyle(blnReadOnly)
        txtZIP.ReadOnly = blnReadOnly
        txtZIP.CssClass = GetTextBoxStyle(blnReadOnly)

        txtBankAcctNum.ReadOnly = blnReadOnly
        txtBankAcctNum.CssClass = GetTextBoxStyle(blnReadOnly)
        txtBankOwner.ReadOnly = blnReadOnly
        txtBankOwner.CssClass = GetTextBoxStyle(blnReadOnly)
        txtBankRntNum.ReadOnly = blnReadOnly
        txtBankRntNum.CssClass = GetTextBoxStyle(blnReadOnly)

        txtInsAmt.ReadOnly = blnReadOnly
        txtInsAmt.CssClass = GetTextBoxStyle(blnReadOnly)
    End Sub

    Private Function GetTextBoxStyle(blnReadOnly As Boolean) As String
        If blnReadOnly Then
            Return "FLATTEXTBOX"
        Else
            Return ""
        End If
    End Function

    Private Function GetProdCodeDropdownText() As String
        Dim txt As String = String.Empty
        With State.MyBO
            If .ProductConversion Then
                Dim i As Integer
                For i = 0 To State.ProdCodeList.Count - 1
                    If State.ProdCodeList(i)(ProductCodeConversion.ExternalProdCodeWithDescDV.COL_EXTERNATL_PRODUCT_CODE).ToString = .ProductCode Then
                        txt = State.ProdCodeList(i)(ProductCodeConversion.ExternalProdCodeWithDescDV.COL_ALL_PRODUCT_CODE).ToString
                        Exit For
                    End If
                Next
            Else
                txt = .ProductCode
            End If
        End With
        Return txt
    End Function

    Protected Sub PopulateFormFromBOs()
        EnableDisablePageControls()
        'populate dropdown lists
        Dim listcontext As ListContext = New ListContext()
        listcontext.DealerId = State.DealerID

        If ddlProdCode.Visible Then
            If State.ProdCodeList Is Nothing Then
                If State.MyBO.ProductConversion Then
                    State.ProdCodeList = State.MyBO.GetExternalProductCode()
                Else
                    State.ProdCodeList = ProductCode.getListByDealer(State.DealerID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If

            End If
            If ddlProdCode.Visible Then
                If State.MyBO.ProductConversion Then
                    'State.ProdCodeList.Sort = ProductCodeConversion.ExternalProdCodeWithDescDV.COL_EXTERNATL_PRODUCT_CODE
                    'BindListTextToDataView(Me.ddlProdCode, State.ProdCodeList, ProductCodeConversion.ExternalProdCodeWithDescDV.COL_ALL_PRODUCT_CODE, ProductCodeConversion.ExternalProdCodeWithDescDV.COL_EXTERNATL_PRODUCT_CODE, True, False)
                    listcontext.ExcludeDetails = "Y"
                    Dim prodCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtProductCodeByDealer", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                    ddlProdCode.Populate(prodCodeLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .TextFunc = Function(x)
                                                                        Return x.Code + "_" + x.Translation
                                                                    End Function,
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                      })
                    'BindListTextToDataView(Me.ddlProdCodeInfo, State.ProdCodeList, ProductCodeConversion.ExternalProdCodeWithDescDV.COL_DESCRIPTION, ProductCodeConversion.ExternalProdCodeWithDescDV.COL_BUNDLED_ITEM, True, False)
                    Dim prodCodeInfoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtProductCodeWithBundledItemByDealer", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                    ddlProdCodeInfo.Populate(prodCodeInfoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })
                Else
                    'State.ProdCodeList.Sort = ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE
                    'BindListTextToDataView(Me.ddlProdCode, State.ProdCodeList, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, True, False)
                    listcontext.ExcludeDetails = "Y"
                    Dim prodCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ProductCodeByDealer", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                    ddlProdCode.Populate(prodCodeLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .TextFunc = AddressOf .GetCode,
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })

                    'BindListTextToDataView(Me.ddlProdCodeInfo, State.ProdCodeList, ProductCode.ProductCodeSearchByDealerDV.COL_DESCRIPTION, ProductCode.ProductCodeSearchByDealerDV.COL_BUNDLED_ITEM, True, False)
                    Dim prodCodeInfoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtProductCodeWithBundledItemByDealer", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                    ddlProdCodeInfo.Populate(prodCodeInfoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })
                End If
            End If
            ddlProdCode.Attributes.Add("onchange", "ShowDesc(document.getElementById('" & txtProdDesc.ClientID & "'), document.getElementById('" & ddlProdCodeInfo.ClientID & "'))")
                'LitScript.Text = LitScript.Text & "<script>document.getElementById('" & ddlProdCodeInfo.ClientID & "').style.display = 'none';</script>"
                'LiteralTop.Text = LiteralTop.Text & "<script>document.getElementById('" & ddlProdCodeInfo.ClientID & "').style.display = 'none';</script>"
                Dim li As System.Web.UI.WebControls.ListItem
                For Each li In ddlProdCodeInfo.Items()
                    If li.Value = "Y" Then
                        State.MyBO.InitBundle()
                        Exit For
                    End If
                Next
            End If
            If State.isSalutation Then
            PopulateSalutationDropdown(cboSalutationId)
        End If


        If ddlManufacturer.Visible Then

            ' BindListTextToDataView(Me.ddlManufacturer, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), "DESCRIPTION", "DESCRIPTION", True)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.CompanyGroupId = compGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            ddlManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
                 {
                     .AddBlankItem = True,
                     .BlankItemValue = "0",
                     .ValueFunc = AddressOf .GetDescription
                })
        End If

        txtDealer.Text = State.DealerName
        literalCurrency.Text = State.MyBO.CurrencyISOCode
        txtCustCountry.Text = State.MyBO.CustomerCountryISOCode
        txtPurchaseCountry.Text = State.MyBO.PurchaseCountryISOCode



        With State.MyBO
            If ddlProdCode.Visible AndAlso .ProductCode <> String.Empty Then
                'Me.SetSelectedItemByText(ddlProdCode, GetProdCodeDropdownText)
                SetSelectedItem(ddlProdCode, .ProductCode)
                ddlProdCodeInfo.SelectedIndex = ddlProdCode.SelectedIndex
            Else
                txtProdCode.Text = GetProdCodeDropdownText()
            End If
            txtProdDesc.Text = State.ProdDesc

            If ddlManufacturer.Visible AndAlso .Make <> String.Empty Then
                SetSelectedItem(ddlManufacturer, .Make)
            End If

            txtCertNum.Text = .CertNum
            If .WarrantySalesDate > Date.MinValue Then
                txtWarrSalesDate.Text = GetDateFormattedString(.WarrantySalesDate)
            Else
                txtWarrSalesDate.Text = ""
            End If
            If .ProductSalesDate > Date.MinValue Then
                txtProdSalesDate.Text = GetDateFormattedString(.ProductSalesDate)
            Else
                txtProdSalesDate.Text = ""
            End If

            If .DateOfBirth > Date.MinValue Then
                txtDateOfBirth.Text = GetDateFormattedString(.DateOfBirth)
            Else
                txtDateOfBirth.Text = ""
            End If

            txtCertDuration.Text = .CertDuration.ToString
            txtMfgWarranty.Text = .ManufacturerDuration.ToString
            txtWarrPrice.Text = .WarrantyPrice.ToString

            txtRetailPrice.Text = .ProductRetailPrice.ToString
            txtInvNum.Text = .InvoiceNumber
            txtBranchCode.Text = .BranchCode
            txtSalesRepNum.Text = .SalesRepNumber
            txtModel.Text = .Model
            txtSerialNum.Text = .SerialNumber
            txtItemCode.Text = .ItemCode
            txtItemDesc.Text = .ItemDescription
            txtManufacturer.Text = .Make
            If State.isSalutation Then
                txtSalutation.Text = State.salnForUserLang  '.Salutation
            End If
            If .UseCustomerProfile = Codes.YESNO_Y Then
                txtFirstName.Text = .FirstName
                txtMiddleName.Text = .MiddleName
                txtLastName.Text = .LastName
            Else
                txtCustName.Text = .CustomerName
            End If

            txtTaxID.Text = .CustomerTaxID
            txtHomePhone.Text = .CustomerHomePhone
            txtWorkPhone.Text = .CustomerWorkPhone
            txtEmail.Text = .CustomerEmail
            txtAddress1.Text = .CustomerAddress1
            txtAddress2.Text = .CustomerAddress2
            txtCity.Text = .CustomerCity
            txtState.Text = .CustomerState
            txtZIP.Text = .CustomerZIP
            moMarketingPromoSerText.Text = .MarketingPromoSer
            moMarketingPromoNumText.Text = .MarketingPromoNum
            txtOccupation.Text = .Occupation

            If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.Dealer.DealerTypeId) = "3" Then
                txtServiceLineNum.Text = .ServiceLineNumber
                txtMembershipNum.Text = .MembershipNum
                If ddlMembershipType.Visible AndAlso .MembershipType <> String.Empty Then
                    SetSelectedItem(ddlMembershipType, .MembershipType)
                End If
                If ddlSubscriberStatus.Visible AndAlso .SubscriberStatus <> String.Empty Then
                    SetSelectedItem(ddlSubscriberStatus, .SubscriberStatus)
                End If
            End If

            If .BundledItems IsNot Nothing Then
                Grid.DataSource = .BundledItems
                Grid.DataBind()
            Else
                Dim li As System.Web.UI.WebControls.ListItem
                For Each li In ddlProdCodeInfo.Items()
                    If li.Value = "Y" Then
                        .InitBundle()
                        Grid.DataSource = .BundledItems
                        Grid.DataBind()
                        Exit For
                    End If
                Next
            End If

            'installment 
            ddlPYMTType.SelectedValue = .PaymentType
            txtPYMTType.Text = ddlPYMTType.SelectedItem.Text
            If State.InstallmentAllowed = "Y" Then
                txtBankAcctNum.Text = .BankAcctNumber
                txtBankRntNum.Text = .BankRoutingNumber
                txtBankOwner.Text = .BankAcctOwnerName
                If State.UseInstallmentDef <> "Y" Then
                    If ddlBillFreq.Items.Count > 0 AndAlso .BillingFrequency >= 0 Then
                        ddlBillFreq.SelectedValue = .BillingFrequency.ToString
                        txtBillFreq.Text = ddlBillFreq.SelectedItem.Text
                    End If
                    If .NumOfInstallments > 0 Then txtInsNum.Text = .NumOfInstallments.ToString
                    If .InstallmentAmount > 0 Then txtInsAmt.Text = .InstallmentAmount.ToString
                End If
            End If
        End With

    End Sub
    Private Function GetYesNo(LanguageId As Guid, oId As Guid) As Boolean
        Dim oYesList As DataView = LookupListNew.GetListItemId(oId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
        Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
        If oYesNo = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateSalutationDropdown(salutationDropDownList As DropDownList)

        Try
            Dim salutation As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("SLTN", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            salutationDropDownList.Populate(salutation, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Function PopulateBOsFromForm(Optional ByVal blnValidateRequired As Boolean = True) As Boolean
        Dim strTemp As String, dtTemp As Date, intTemp As Integer, dblTemp As Double
        Dim obj As New Dealer(State.MyBO.DealerID)
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)

        Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String), hasErr As Boolean = False

        With State.MyBO
            If ddlManufacturer.Visible Then
                If ddlManufacturer.SelectedIndex > 0 Then
                    .Make = ddlManufacturer.SelectedValue.Trim
                Else
                    .Make = ""
                End If
            End If

            .CertNum = txtCertNum.Text.Trim
            If blnValidateRequired AndAlso .CertNum = String.Empty AndAlso obj.CertificatesAutonumberId = noId Then
                State.ColErrLabels.Add(lblCertNum.ID)
                hasErr = True
                errMsg.Add(lblCertNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
            End If

            State.ProdDesc = ddlProdCodeInfo.SelectedItem.Text
            State.AllowBundle = ddlProdCodeInfo.SelectedValue.ToUpper
            If State.AllowBundle <> "Y" AndAlso .GetBundledItemCount > 0 Then
                .BundledItems.Clear()
                Grid.DataSource = .BundledItems
                Grid.DataBind()
            End If

            If ddlProdCode.Visible Then
                If ddlProdCode.SelectedIndex > 0 Then
                    .ProductCode = ddlProdCode.SelectedValue.Trim
                Else
                    .ProductCode = ""
                End If
            End If
            If blnValidateRequired AndAlso .ProductCode = String.Empty Then
                State.ProdDesc = String.Empty
                State.ColErrLabels.Add(lblProdCode.ID)
                hasErr = True
                errMsg.Add(lblProdCode.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
            End If

            '  If Me.State.UseDeliveryDate = True Then
            'lblWarrSalesDate.Text = TranslationBase.TranslateLabelOrMessage(INSURANCE_ACTIVATION_DATE)
            ' Else
            lblWarrSalesDate.Text = TranslationBase.TranslateLabelOrMessage(WARR_SALES_DATE)
            'End If

            strTemp = txtWarrSalesDate.Text.Trim
            If strTemp <> "" Then
                If Date.TryParse(strTemp, dtTemp) Then
                    .WarrantySalesDate = dtTemp
                Else
                    .WarrantySalesDate = Date.MinValue
                    State.ColErrLabels.Add(lblWarrSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblWarrSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                End If
            Else
                If blnValidateRequired Then
                    .WarrantySalesDate = Date.MinValue
                    State.ColErrLabels.Add(lblWarrSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblWarrSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If
            End If

            strTemp = txtProdSalesDate.Text.Trim
            If strTemp <> "" Then
                If Date.TryParse(strTemp, dtTemp) Then
                    .ProductSalesDate = dtTemp
                Else
                    .ProductSalesDate = Date.MinValue
                    State.ColErrLabels.Add(lblProdSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblProdSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                End If
            Else
                If blnValidateRequired Then
                    .ProductSalesDate = Date.MinValue
                    State.ColErrLabels.Add(lblProdSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblProdSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If
            End If

            strTemp = txtDateOfBirth.Text.Trim
            If strTemp <> "" Then
                If Date.TryParse(strTemp, dtTemp) Then
                    .DateOfBirth = dtTemp
                Else
                    .DateOfBirth = Date.MinValue
                    State.ColErrLabels.Add(labelDateOfBirth.ID)
                    hasErr = True
                    errMsg.Add(labelDateOfBirth.Text & ":" & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                End If
                'Else
                '    If blnValidateRequired Then
                '        .DateOfBirth = Date.MinValue
                '        State.ColErrLabels.Add(Me.lblWarrSalesDate.ID)
                '        hasErr = True
                '        errMsg.Add(lblWarrSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                '    End If
            End If

            strTemp = txtCertDuration.Text.Trim
            If strTemp <> "" Then
                If Integer.TryParse(strTemp, intTemp) Then
                    .CertDuration = intTemp
                Else
                    .CertDuration = 0
                    State.ColErrLabels.Add(lblCertDuration.ID)
                    hasErr = True
                    errMsg.Add(lblCertDuration.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                End If
            End If

            strTemp = txtMfgWarranty.Text.Trim
            If strTemp <> "" Then
                If Integer.TryParse(strTemp, intTemp) Then
                    .ManufacturerDuration = intTemp
                Else
                    .ManufacturerDuration = 0
                    State.ColErrLabels.Add(lblMfgWarranty.ID)
                    hasErr = True
                    errMsg.Add(lblMfgWarranty.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                End If
            End If

            strTemp = txtWarrPrice.Text.Trim
            If strTemp <> "" Then
                If Double.TryParse(strTemp, dblTemp) Then
                    .WarrantyPrice = dblTemp
                Else
                    .WarrantyPrice = 0
                    State.ColErrLabels.Add(lblWarrPrice.ID)
                    hasErr = True
                    errMsg.Add(lblWarrPrice.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                End If
            End If

            strTemp = txtRetailPrice.Text.Trim
            If strTemp <> "" Then
                If Double.TryParse(strTemp, dblTemp) Then
                    .ProductRetailPrice = dblTemp
                Else
                    .ProductRetailPrice = 0
                    State.ColErrLabels.Add(lblRetailPrice.ID)
                    hasErr = True
                    errMsg.Add(lblRetailPrice.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                End If
            End If

            .InvoiceNumber = txtInvNum.Text.Trim
            .BranchCode = txtBranchCode.Text.Trim
            .SalesRepNumber = txtSalesRepNum.Text.Trim
            .Model = txtModel.Text.Trim
            .SerialNumber = txtSerialNum.Text.Trim
            .ItemCode = txtItemCode.Text.Trim
            .ItemDescription = txtItemDesc.Text.Trim
            .Make = ddlManufacturer.SelectedItem.Text.Trim

            If State.isSalutation Then
                Dim salnDV As DataView = LookupListNew.GetSalutationLookupList(State.Company.LanguageId)
                Dim SalutationId As Guid = GetSelectedItem(cboSalutationId)
                Dim SalnDescForCompLang As String = LookupListNew.GetDescriptionFromId(salnDV, SalutationId)
                .Salutation = SalnDescForCompLang
                State.salnForUserLang = cboSalutationId.SelectedItem.Text.Trim
                .SalutationCode = LookupListNew.GetCodeFromId(salnDV, SalutationId)
            End If


            If .UseCustomerProfile =  Codes.YESNO_Y Then
                .FirstName = txtFirstName.Text.Trim
                .MiddleName = txtMiddleName.Text.Trim
                .LastName = txtLastName.Text.Trim
            Else
                .CustomerName = txtCustName.Text.Trim
            End If

            .CustomerTaxID = txtTaxID.Text.Trim
            .CustomerHomePhone = txtHomePhone.Text.Trim
            .CustomerWorkPhone = txtWorkPhone.Text.Trim
            .CustomerEmail = txtEmail.Text.Trim
            .CustomerAddress1 = txtAddress1.Text.Trim
            .CustomerAddress2 = txtAddress2.Text.Trim
            .CustomerCity = txtCity.Text.Trim
            .CustomerState = txtState.Text.Trim
            .CustomerZIP = txtZIP.Text.Trim

            .PaymentType = ddlPYMTType.SelectedValue
            .Occupation = txtOccupation.Text.Trim

            'installment 
            If State.InstallmentAllowed = "Y" AndAlso .PaymentType = "6" Then
                .BankAcctNumber = txtBankAcctNum.Text.Trim
                If blnValidateRequired AndAlso .BankAcctNumber = String.Empty Then
                    State.ColErrLabels.Add(lblBankAcctNum.ID)
                    hasErr = True
                    errMsg.Add(lblBankAcctNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If


                .BankRoutingNumber = txtBankRntNum.Text.Trim
                If blnValidateRequired AndAlso .BankRoutingNumber = String.Empty Then
                    State.ColErrLabels.Add(lblBankRntNum.ID)
                    hasErr = True
                    errMsg.Add(lblBankRntNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If

                .BankAcctOwnerName = txtBankOwner.Text.Trim
                If blnValidateRequired AndAlso .BankAcctOwnerName = String.Empty Then
                    State.ColErrLabels.Add(lblBankOwner.ID)
                    hasErr = True
                    errMsg.Add(lblBankOwner.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If

                If State.UseInstallmentDef <> "Y" Then
                    .BillingFrequency = -1
                    If ddlBillFreq.Items.Count > 0 Then
                        strTemp = ddlBillFreq.SelectedValue
                        If Integer.TryParse(strTemp, intTemp) Then .BillingFrequency = intTemp
                    End If

                    .NumOfInstallments = -1
                    strTemp = txtInsNum.Text.Trim
                    If strTemp <> String.Empty Then
                        If Integer.TryParse(strTemp, intTemp) Then
                            .NumOfInstallments = intTemp
                        Else
                            State.ColErrLabels.Add(lblInsNum.ID)
                            hasErr = True
                            errMsg.Add(lblInsNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                        End If
                    Else
                        If blnValidateRequired Then
                            State.ColErrLabels.Add(lblInsNum.ID)
                            hasErr = True
                            errMsg.Add(lblInsNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                        End If
                    End If

                    .InstallmentAmount = -1
                    strTemp = txtInsAmt.Text.Trim
                    If strTemp <> String.Empty Then
                        If Double.TryParse(strTemp, dblTemp) Then
                            .InstallmentAmount = dblTemp
                        Else
                            State.ColErrLabels.Add(lblInsAmt.ID)
                            hasErr = True
                            errMsg.Add(lblInsAmt.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                        End If
                    Else
                        If blnValidateRequired Then
                            State.ColErrLabels.Add(lblInsAmt.ID)
                            hasErr = True
                            errMsg.Add(lblInsAmt.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                        End If
                    End If
                End If

            End If

            If Not State.Company.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) _
                OrElse Not State.Dealer.RequireCustomerAMLInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CAIT, "0")) Then '0= None

                If ddlMaritalStatus.SelectedIndex > 0 Then
                    Dim selectedMaritalStatusId As Guid = GetSelectedItem(ddlMaritalStatus)
                    Dim Query = State.MaritalStatusList.FirstOrDefault(Function(r) r.ListItemId = selectedMaritalStatusId)
                    .MaritalStatus = Query.Code
                Else
                    .MaritalStatus = ""
                End If

                If ddlPersonType.SelectedIndex > 0 Then
                    Dim selectedPersonTypeId As Guid = GetSelectedItem(ddlPersonType)
                    Dim Query = State.PersonTypeList.FirstOrDefault(Function(r) r.ListItemId = selectedPersonTypeId)
                    .PersonType = Query.Code
                Else
                    .PersonType = ""
                End If

                If ddlGender.SelectedIndex > 0 Then
                    Dim selectedddlGenderId As Guid = GetSelectedItem(ddlGender)

                    Dim Query = State.GenderList.FirstOrDefault(Function(r) r.ListItemId = selectedddlGenderId)
                    .Gender = Query.Code

                Else
                    .Gender = ""
                End If

                If ddlNationality.SelectedIndex > 0 Then
                    Dim selectedNationalityId As Guid = GetSelectedItem(ddlNationality)
                    Dim Query = State.NationalityList.FirstOrDefault(Function(r) r.ListItemId = selectedNationalityId)
                    .Nationality = Query.Code
                Else
                    .Nationality = ""
                End If

                If ddlPlaceOfBirth.SelectedIndex > 0 Then
                    Dim selectedPlaceOfBirthId As Guid = GetSelectedItem(ddlPlaceOfBirth)
                    Dim Query = State.PlaceOfBirthList.FirstOrDefault(Function(r) r.ListItemId = selectedPlaceOfBirthId)
                    .PlaceOfBirth = Query.Code
                Else
                    .PlaceOfBirth = ""
                End If

                .CUIT_CUIL = moCUIT_CUIL.Text.Trim

            End If
            .MarketingPromoSer = moMarketingPromoSerText.Text
            .MarketingPromoNum = moMarketingPromoNumText.Text

            If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.Dealer.DealerTypeId) = "3" Then
                .ServiceLineNumber = txtServiceLineNum.Text.Trim()
                .MembershipNum = txtMembershipNum.Text.Trim()
                If ddlMembershipType.Visible Then
                    If ddlMembershipType.SelectedIndex > 0 Then
                        .MembershipType = ddlMembershipType.SelectedValue.Trim
                    Else
                        .MembershipType = ""
                    End If
                End If
                If ddlSubscriberStatus.Visible Then
                    If ddlSubscriberStatus.SelectedIndex > 0 Then
                        .SubscriberStatus = ddlSubscriberStatus.SelectedValue.Trim
                    Else
                        .SubscriberStatus = ""
                    End If
                End If
            End If
        End With

        If hasErr Then 'show error
            SetErrorLabels()
            ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
            'Throw New GUIException("", "")
        End If
        Return Not hasErr
    End Function

    Private Sub SetErrorLabels()
        If State.ColErrLabels.Count > 0 Then
            Dim lblName As String
            For Each lblName In State.ColErrLabels
                CType(Master.FindControl("ContentPanelMainContentBody").FindControl(lblName), Label).ForeColor = Color.Red
            Next
        End If
    End Sub

    Private Sub ClearErrorLabels()
        If State.ColErrLabels.Count > 0 Then
            Dim lblName As String
            For Each lblName In State.ColErrLabels
                CType(Master.FindControl("ContentPanelMainContentBody").FindControl(lblName), Label).ForeColor = Color.Empty
            Next
            State.ColErrLabels.Clear()
        End If
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Dim action As DetailPageCommand = State.ActionInProgress

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""

        If confResponse IsNot Nothing Then
            If confResponse = MSG_VALUE_YES Then
                If action = DetailPageCommand.Save Then
                    callPage(CertAddBundleItemForm.URL, State.MyBO)
                ElseIf action = DetailPageCommand.Back Then
                    ReturnToCallingPage(State.DealerID)
                ElseIf action = DetailPageCommand.Redirect_ Then
                    callPage(Certificates.CertificateForm.URL, State.CertID)
                End If
            ElseIf confResponse = MSG_VALUE_NO Then
                If action = DetailPageCommand.Save Then
                    SaveTheCertificate(False)
                End If
            End If
        End If
    End Sub

    Private Sub SaveTheCertificate(Optional ByVal blnValidate As Boolean = True)
        Dim ErrMsg As New Collections.Generic.List(Of String) ', CertID As Guid = Guid.Empty
        If State.MyBO.Save(ErrMsg, State.CertID, blnValidate) Then
            State.IsCertSaved = True
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Redirect_
            DisplayMessage(TranslationBase.TranslateLabelOrMessage(Message.SAVE_RECORD_CONFIRMATION) & ". " & TranslationBase.TranslateLabelOrMessage(MSG_PROMPT_FOR_PROCEDD_TO_CERT_DETAIL), "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
            PopulateFormFromBOs()
        Else
            ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
        End If
    End Sub

    'REQ-5681 begin

    Private Sub PopulateMaritalStatusDropdown(MaritalStatusDropDownList As DropDownList)
        Try

            Dim maritalStatus As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MARITAL_STATUS", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            State.MaritalStatusList = maritalStatus
            MaritalStatusDropDownList.Populate(maritalStatus, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateNationalityDropdown(nationalityDropDownList As DropDownList)
        Try
            Dim nationalities As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("NATIONALITY", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            State.NationalityList = nationalities
            nationalityDropDownList.Populate(nationalities, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulatePlaceOfBirthDropdown(PlaceOfBirthDropDownList As DropDownList)
        Try

            Dim placeOfBirth As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PLACEOFBIRTH", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            State.PlaceOfBirthList = placeOfBirth
            PlaceOfBirthDropDownList.Populate(placeOfBirth, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateGenderDropdown(GenderDropDownList As DropDownList)
        Try

            Dim gender As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("GENDER", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            State.GenderList = gender
            GenderDropDownList.Populate(gender, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulatePersonTypeDropdown(PersonTypeDropDownList As DropDownList)
        Try
            Dim personType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PERSON_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            State.PersonTypeList = personType
            PersonTypeDropDownList.Populate(personType, New PopulateOptions() With
                                                         {
                                                                .AddBlankItem = True
                                                          })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'REQ-5681 end
#End Region

#Region "button click handlers"

    Private Sub btnCopyNew_Click(sender As Object, e As System.EventArgs) Handles btnCopyNew.Click
        Try
            Dim objNew As CertAddController = New CertAddController(State.DealerID)
            With State.MyBO
                objNew.CurrencyISOCode = .CurrencyISOCode
                objNew.PurchaseCountryISOCode = .PurchaseCountryISOCode
                objNew.CustomerCountryISOCode = .CustomerCountryISOCode
            End With
            State.MyBO = objNew
            State.IsCertSaved = False
            State.ProdDesc = String.Empty
            State.AllowBundle = "N"
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If State.IsCertSaved Then
                ReturnToCallingPage(State.DealerID)
            Else
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Try

            If PopulateBOsFromForm() Then
                Dim ErrMsg As New Collections.Generic.List(Of String)
                If State.MyBO.Validate(ErrMsg) Then
                    'Prompt user for more bundle items if bundle is allowed
                    If State.AllowBundle = "Y" AndAlso (State.MyBO.GetBundledItemCount < CertAddController.MAX_BUNDLED_ITEMS_ALLOWED) Then
                        DisplayMessage(MSG_PROMPT_FOR_ADD_MORE_BUNDLE_ITEMS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = DetailPageCommand.Save
                    Else
                        SaveTheCertificate(False)
                    End If
                Else
                    ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBundle_Click(sender As Object, e As System.EventArgs) Handles btnBundle.Click
        Try
            PopulateBOsFromForm(False)
            If State.AllowBundle = "Y" Then
                callPage(CertAddBundleItemForm.URL, State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        Try
            State.MyBO = New CertAddController()

            Dim currencycode As String, countrycode As String

            CertAddController.GetDealerDetailsForCertADD(State.DealerID, currencycode, countrycode, State.MailAddressFormat, State.InstallmentAllowed, State.UseInstallmentDef)
            With State.MyBO
                .CurrencyISOCode = currencycode
                .PurchaseCountryISOCode = countrycode
                .CustomerCountryISOCode = countrycode
            End With
            PopulatePaymentType()
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region
End Class