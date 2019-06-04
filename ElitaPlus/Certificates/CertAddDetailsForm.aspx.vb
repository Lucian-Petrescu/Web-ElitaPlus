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

        Public Sub New(ByVal DealerID As Guid, ByVal DealerName As String)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        CheckIfComingFromSaveConfirm()
        'Clear script and set focus to the first elements
        LitScript.Text = "<script>document.getElementById('" & txtCertNum.ClientID & "').focus();</script>"
        Dim blnHasContract As Boolean = True
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.AddCalendar(Me.btnWarrSalesDate, Me.txtWarrSalesDate)
                Me.AddCalendar(Me.btnProdSalesDate, Me.txtProdSalesDate)
                Me.AddCalendar(Me.btnDateOfBirth, Me.txtDateOfBirth)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CertAddController()
                End If

                Dim currencycode As String, countrycode As String
                Try
                    CertAddController.GetDealerDetailsForCertADD(State.DealerID, currencycode, countrycode, State.MailAddressFormat, State.InstallmentAllowed, State.UseInstallmentDef)
                    Me.State.Dealer = New Dealer(State.DealerID)
                    Me.State.Company = New Company(State.Dealer.CompanyId)
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
                Me.State.isSalutation = Me.GetYesNo(ElitaPlusIdentity.Current.ActiveUser.LanguageId, companyBO.SalutationId)
                If blnHasContract Then PopulateFormFromBOs()
                'End If -- COMMENTED FOR DEF-1115 to include the validation in not is postback
                'START  DEF-1115 Show Contract Validation on Add Certificate
                Dim oContract As Contract
                oContract = Contract.GetContract(Me.State.MyBO.DealerID, System.DateTime.Now)
                If oContract Is Nothing Then
                    'START  DEF-1115 Show Contract Validation on Add Certificate   
                    Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.NO_CONTRACT_FOUND, GetType(Contract), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(Contract).FullName, "Test")
                    'END  DEF-1115 Show Contract Validation on Add Certificate 
                Else
                    lblWarrSalesDate.Text = TranslationBase.TranslateLabelOrMessage(WARR_SALES_DATE)

                    If oContract.MarketingPromotionId = Guid.Empty OrElse Not (GetYesNo(Me.State.Company.LanguageId, oContract.MarketingPromotionId)) Then
                        If (Me.IsNewUI) Then
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
                        If (Me.IsNewUI) Then
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

            Me.txtProdDesc.Attributes.Add("readonly", "readonly")
            Me.ddlProdCodeInfo.Attributes.Add("display", "none")

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
        ClearErrorLabels()
    End Sub
    Private Sub ShowHideControls()
        If State.MyBO.UseCustomerProfile = Codes.YESNO_Y Then 
            moNotUseCustomerProfile.Attributes("style") = "display: none"
        Else
            moUseCustomerProfile1.Attributes("style") = "display: none"
            moUseCustomerProfile2.Attributes("style") = "display: none"
        End If
        If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.Dealer.DealerTypeId) = "3" Then
            PopulateMobileDealerDropdown()
        Else
            mobileDealerFields1.Attributes("style") = "display: none"
            mobileDealerFields2.Attributes("style") = "display: none"
        End If
    End Sub
    Private Sub PopulateAMLControls()
        If Me.State.Company.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) _
        And Me.State.Dealer.RequireCustomerAMLInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CAIT, "0")) Then '0= None 

            Me.moAMLRegulations1.Attributes("style") = "display: none"
            Me.moAMLRegulations2.Attributes("style") = "display: none"
            Me.moAMLRegulations3.Attributes("style") = "display: none"
            Me.moAMLRegulations4.Attributes("style") = "display: none"
        Else
            PopulateMaritalStatusDropdown(Me.ddlMaritalStatus)
            PopulateNationalityDropdown(Me.ddlNationality)
            PopulatePlaceOfBirthDropdown(Me.ddlPlaceOfBirth)
            PopulateGenderDropdown(Me.ddlGender)
            PopulatePersonTypeDropdown(Me.ddlPersonType)

        End If

    End Sub
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                objParam = CType(Me.CallingParameters, Parameters)
                State.DealerID = objParam.DealerID
                State.DealerName = objParam.DealerName
                Me.State.MyBO = New CertAddController(State.DealerID)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster, False)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Dim strBundleURL() As String = CertAddBundleItemForm.URL.ToUpper.Split("/"c)
        Dim strFrmURL() As String = ReturnFromUrl.ToUpper.Split("/"c)

        'return from bundle screen
        If strFrmURL(strFrmURL.Count - 1) = strBundleURL(strBundleURL.Count - 1) Then
            Me.IsReturningFromChild = True
            If Not ReturnPar Is Nothing Then
                State.MyBO = CType(ReturnPar, CertAddController)
            End If
        End If
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
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
        Me.ddlBillFreq.Populate(billFreqLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = False,
                                           .ValueFunc = AddressOf .GetCode
                                 })
        ddlBillFreq.SelectedValue = "0"
    End Sub

    Private Sub PopulateMobileDealerDropdown()
        'BindCodeToListControl(ddlSubscriberStatus, LookupListNew.DropdownLookupList("SUBSTAT", Authentication.LangId, True), "DESCRIPTION", "CODE")
        Dim subscribersLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SUBSTAT", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.ddlSubscriberStatus.Populate(subscribersLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = True,
                                           .ValueFunc = AddressOf .GetCode
                                 })
        ' BindCodeToListControl(ddlMembershipType, LookupListNew.DropdownLookupList("MEMTYPE", Authentication.LangId, True), "DESCRIPTION", "CODE")
        Dim membershipTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MEMTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.ddlMembershipType.Populate(membershipTypeLkl, New PopulateOptions() With
                                 {
                                           .AddBlankItem = True,
                                           .ValueFunc = AddressOf .GetCode
                                 })

    End Sub
    Protected Sub EnableDisablePageControls()

        If Not Me.State.isSalutation Then
            ControlMgr.SetVisibleControl(Me, Me.moSalutationLabel, False)
            ControlMgr.SetVisibleControl(Me, Me.laSemi, False)
            cboSalutationId.Visible = False
        End If

        If State.IsCertSaved Then 'view only mode
            Me.btnSave.Visible = False
            Me.btnNew.Visible = True
            Me.btnCopyNew.Visible = True
            Me.ddlProdCode.Visible = False
            Me.ddlProdCodeInfo.Visible = False
            Me.txtProdCode.Visible = True
            Me.ddlManufacturer.Visible = False
            Me.txtManufacturer.Visible = True
            If Me.State.isSalutation Then
                Me.txtSalutation.Visible = True
            End If

            Me.btnProdSalesDate.Visible = False
            Me.btnWarrSalesDate.Visible = False
            Me.btnDateOfBirth.Visible = False
            Me.btnBundle.Visible = False
            'installment
            Me.ddlBillFreq.Visible = False
            Me.txtBillFreq.Visible = True
            Me.ddlPYMTType.Visible = False
            Me.txtPYMTType.Visible = True
            If Me.State.isSalutation Then
                ControlMgr.SetVisibleControl(Me, Me.moSalutationLabel, True)
                ControlMgr.SetVisibleControl(Me, Me.laSemi, True)
                cboSalutationId.Visible = False
            End If
        Else 'Edit mode
            Me.btnSave.Visible = True
            Me.btnNew.Visible = False
            Me.btnCopyNew.Visible = False
            Me.ddlProdCode.Visible = True
            Me.ddlProdCodeInfo.Visible = True
            Me.txtProdCode.Visible = False
            Me.ddlManufacturer.Visible = True
            Me.txtManufacturer.Visible = False
            Me.btnProdSalesDate.Visible = True
            Me.btnWarrSalesDate.Visible = True
            Me.btnDateOfBirth.Visible = True
            Me.btnBundle.Visible = True
            'installment
            Me.ddlBillFreq.Visible = True
            Me.txtBillFreq.Visible = False
            Me.ddlPYMTType.Visible = True
            Me.txtPYMTType.Visible = False
            If Me.State.isSalutation Then
                ControlMgr.SetVisibleControl(Me, Me.moSalutationLabel, True)
                ControlMgr.SetVisibleControl(Me, Me.laSemi, True)
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

    Protected Sub EnableDisableTextBoxes(ByVal blnReadOnly As Boolean)
        Me.txtCertNum.ReadOnly = blnReadOnly
        Me.txtCertNum.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtWarrSalesDate.ReadOnly = blnReadOnly
        Me.txtWarrSalesDate.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtProdSalesDate.ReadOnly = blnReadOnly
        Me.txtProdSalesDate.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtCertDuration.ReadOnly = blnReadOnly
        Me.txtCertDuration.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtMfgWarranty.ReadOnly = blnReadOnly
        Me.txtMfgWarranty.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtWarrPrice.ReadOnly = blnReadOnly
        Me.txtWarrPrice.CssClass = GetTextBoxStyle(blnReadOnly)

        Me.txtDateOfBirth.ReadOnly = blnReadOnly
        Me.txtDateOfBirth.CssClass = GetTextBoxStyle(blnReadOnly)

        Me.txtRetailPrice.ReadOnly = blnReadOnly
        Me.txtRetailPrice.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtInvNum.ReadOnly = blnReadOnly
        Me.txtInvNum.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtBranchCode.ReadOnly = blnReadOnly
        Me.txtBranchCode.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtSalesRepNum.ReadOnly = blnReadOnly
        Me.txtSalesRepNum.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtModel.ReadOnly = blnReadOnly
        Me.txtModel.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtSerialNum.ReadOnly = blnReadOnly
        Me.txtSerialNum.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtItemCode.ReadOnly = blnReadOnly
        Me.txtItemCode.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtItemDesc.ReadOnly = blnReadOnly
        Me.txtItemDesc.CssClass = GetTextBoxStyle(blnReadOnly)

        Me.txtCustName.ReadOnly = blnReadOnly
        Me.txtCustName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtFirstName.ReadOnly = blnReadOnly
        txtFirstName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtMiddleName.ReadOnly = blnReadOnly
        txtMiddleName.CssClass = GetTextBoxStyle(blnReadOnly)
        txtLastName.ReadOnly = blnReadOnly
        txtLastName.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtTaxID.ReadOnly = blnReadOnly
        Me.txtTaxID.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtHomePhone.ReadOnly = blnReadOnly
        Me.txtHomePhone.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtWorkPhone.ReadOnly = blnReadOnly
        Me.txtWorkPhone.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtEmail.ReadOnly = blnReadOnly
        Me.txtEmail.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtAddress1.ReadOnly = blnReadOnly
        Me.txtAddress1.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtAddress2.ReadOnly = blnReadOnly
        Me.txtAddress2.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtCity.ReadOnly = blnReadOnly
        Me.txtCity.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtState.ReadOnly = blnReadOnly
        Me.txtState.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtZIP.ReadOnly = blnReadOnly
        Me.txtZIP.CssClass = GetTextBoxStyle(blnReadOnly)

        Me.txtBankAcctNum.ReadOnly = blnReadOnly
        Me.txtBankAcctNum.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtBankOwner.ReadOnly = blnReadOnly
        Me.txtBankOwner.CssClass = GetTextBoxStyle(blnReadOnly)
        Me.txtBankRntNum.ReadOnly = blnReadOnly
        Me.txtBankRntNum.CssClass = GetTextBoxStyle(blnReadOnly)

        Me.txtInsAmt.ReadOnly = blnReadOnly
        Me.txtInsAmt.CssClass = GetTextBoxStyle(blnReadOnly)
    End Sub

    Private Function GetTextBoxStyle(ByVal blnReadOnly As Boolean) As String
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
                    Me.ddlProdCode.Populate(prodCodeLkl, New PopulateOptions() With
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
                    Me.ddlProdCodeInfo.Populate(prodCodeInfoLkl, New PopulateOptions() With
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
                    Me.ddlProdCode.Populate(prodCodeLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .TextFunc = AddressOf .GetCode,
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })

                    'BindListTextToDataView(Me.ddlProdCodeInfo, State.ProdCodeList, ProductCode.ProductCodeSearchByDealerDV.COL_DESCRIPTION, ProductCode.ProductCodeSearchByDealerDV.COL_BUNDLED_ITEM, True, False)
                    Dim prodCodeInfoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtProductCodeWithBundledItemByDealer", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                    Me.ddlProdCodeInfo.Populate(prodCodeInfoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .BlankItemValue = "0",
                                                        .TextFunc = AddressOf .GetDescription,
                                                        .ValueFunc = AddressOf .GetCode,
                                                        .SortFunc = AddressOf .GetCode
                                                       })
                End If
            End If
            Me.ddlProdCode.Attributes.Add("onchange", "ShowDesc(document.getElementById('" & Me.txtProdDesc.ClientID & "'), document.getElementById('" & ddlProdCodeInfo.ClientID & "'))")
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
            If Me.State.isSalutation Then
            PopulateSalutationDropdown(cboSalutationId)
        End If


        If ddlManufacturer.Visible Then

            ' BindListTextToDataView(Me.ddlManufacturer, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), "DESCRIPTION", "DESCRIPTION", True)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.CompanyGroupId = compGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            Me.ddlManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
                 {
                     .AddBlankItem = True,
                     .BlankItemValue = "0",
                     .ValueFunc = AddressOf .GetDescription
                })
        End If

        Me.txtDealer.Text = State.DealerName
        literalCurrency.Text = State.MyBO.CurrencyISOCode
        Me.txtCustCountry.Text = State.MyBO.CustomerCountryISOCode
        Me.txtPurchaseCountry.Text = State.MyBO.PurchaseCountryISOCode



        With State.MyBO
            If ddlProdCode.Visible AndAlso .ProductCode <> String.Empty Then
                'Me.SetSelectedItemByText(ddlProdCode, GetProdCodeDropdownText)
                Me.SetSelectedItem(ddlProdCode, .ProductCode)
                ddlProdCodeInfo.SelectedIndex = ddlProdCode.SelectedIndex
            Else
                Me.txtProdCode.Text = GetProdCodeDropdownText()
            End If
            Me.txtProdDesc.Text = State.ProdDesc

            If ddlManufacturer.Visible AndAlso .Make <> String.Empty Then
                Me.SetSelectedItem(ddlManufacturer, .Make)
            End If

            Me.txtCertNum.Text = .CertNum
            If .WarrantySalesDate > Date.MinValue Then
                Me.txtWarrSalesDate.Text = .WarrantySalesDate.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
            Else
                Me.txtWarrSalesDate.Text = ""
            End If
            If .ProductSalesDate > Date.MinValue Then
                Me.txtProdSalesDate.Text = .ProductSalesDate.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
            Else
                Me.txtProdSalesDate.Text = ""
            End If

            If .DateOfBirth > Date.MinValue Then
                Me.txtDateOfBirth.Text = .DateOfBirth.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
            Else
                Me.txtDateOfBirth.Text = ""
            End If

            Me.txtCertDuration.Text = .CertDuration.ToString
            Me.txtMfgWarranty.Text = .ManufacturerDuration.ToString
            Me.txtWarrPrice.Text = .WarrantyPrice.ToString

            Me.txtRetailPrice.Text = .ProductRetailPrice.ToString
            Me.txtInvNum.Text = .InvoiceNumber
            Me.txtBranchCode.Text = .BranchCode
            Me.txtSalesRepNum.Text = .SalesRepNumber
            Me.txtModel.Text = .Model
            Me.txtSerialNum.Text = .SerialNumber
            Me.txtItemCode.Text = .ItemCode
            Me.txtItemDesc.Text = .ItemDescription
            Me.txtManufacturer.Text = .Make
            If Me.State.isSalutation Then
                Me.txtSalutation.Text = Me.State.salnForUserLang  '.Salutation
            End If
            If .UseCustomerProfile = Codes.YESNO_Y Then
                txtFirstName.Text = .FirstName
                txtMiddleName.Text = .MiddleName
                txtLastName.Text = .LastName
            Else
                Me.txtCustName.Text = .CustomerName
            End If

            Me.txtTaxID.Text = .CustomerTaxID
            Me.txtHomePhone.Text = .CustomerHomePhone
            Me.txtWorkPhone.Text = .CustomerWorkPhone
            Me.txtEmail.Text = .CustomerEmail
            Me.txtAddress1.Text = .CustomerAddress1
            Me.txtAddress2.Text = .CustomerAddress2
            Me.txtCity.Text = .CustomerCity
            Me.txtState.Text = .CustomerState
            Me.txtZIP.Text = .CustomerZIP
            Me.moMarketingPromoSerText.Text = .MarketingPromoSer
            Me.moMarketingPromoNumText.Text = .MarketingPromoNum
            Me.txtOccupation.Text = .Occupation

            If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.Dealer.DealerTypeId) = "3" Then
                txtServiceLineNum.Text = .ServiceLineNumber
                txtMembershipNum.Text = .MembershipNum
                If ddlMembershipType.Visible AndAlso .MembershipType <> String.Empty Then
                    SetSelectedItem(ddlMembershipType, .MembershipType)
                End If
                If ddlSubscriberStatus.Visible AndAlso .SubscriberStatus <> String.Empty Then
                    SetSelectedItem(ddlSubscriberStatus, .SubscriberStatus)
                End If
            End If

            If Not .BundledItems Is Nothing Then
                Me.Grid.DataSource = .BundledItems
                Me.Grid.DataBind()
            Else
                Dim li As System.Web.UI.WebControls.ListItem
                For Each li In ddlProdCodeInfo.Items()
                    If li.Value = "Y" Then
                        .InitBundle()
                        Me.Grid.DataSource = .BundledItems
                        Me.Grid.DataBind()
                        Exit For
                    End If
                Next
            End If

            'installment 
            Me.ddlPYMTType.SelectedValue = .PaymentType
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
    Private Function GetYesNo(ByVal LanguageId As Guid, ByVal oId As Guid) As Boolean
        Dim oYesList As DataView = LookupListNew.GetListItemId(oId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
        Dim oYesNo As String = oYesList.Item(FIRST_ROW).Item(CODE).ToString
        If oYesNo = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateSalutationDropdown(ByVal salutationDropDownList As DropDownList)

        Try
            Dim salutation As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("SLTN", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            salutationDropDownList.Populate(salutation, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Function PopulateBOsFromForm(Optional ByVal blnValidateRequired As Boolean = True) As Boolean
        Dim strTemp As String, dtTemp As Date, intTemp As Integer, dblTemp As Double
        Dim obj As New Dealer(Me.State.MyBO.DealerID)
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

            .CertNum = Me.txtCertNum.Text.Trim
            If blnValidateRequired AndAlso .CertNum = String.Empty AndAlso obj.CertificatesAutonumberId = noId Then
                State.ColErrLabels.Add(Me.lblCertNum.ID)
                hasErr = True
                errMsg.Add(lblCertNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
            End If

            State.ProdDesc = Me.ddlProdCodeInfo.SelectedItem.Text
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
                State.ColErrLabels.Add(Me.lblProdCode.ID)
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
                    State.ColErrLabels.Add(Me.lblWarrSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblWarrSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                End If
            Else
                If blnValidateRequired Then
                    .WarrantySalesDate = Date.MinValue
                    State.ColErrLabels.Add(Me.lblWarrSalesDate.ID)
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
                    State.ColErrLabels.Add(Me.lblProdSalesDate.ID)
                    hasErr = True
                    errMsg.Add(lblProdSalesDate.Text & ":" & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                End If
            Else
                If blnValidateRequired Then
                    .ProductSalesDate = Date.MinValue
                    State.ColErrLabels.Add(Me.lblProdSalesDate.ID)
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
                    State.ColErrLabels.Add(Me.labelDateOfBirth.ID)
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
                    State.ColErrLabels.Add(Me.lblCertDuration.ID)
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
                    State.ColErrLabels.Add(Me.lblMfgWarranty.ID)
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
                    State.ColErrLabels.Add(Me.lblWarrPrice.ID)
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
                    State.ColErrLabels.Add(Me.lblRetailPrice.ID)
                    hasErr = True
                    errMsg.Add(lblRetailPrice.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                End If
            End If

            .InvoiceNumber = Me.txtInvNum.Text.Trim
            .BranchCode = Me.txtBranchCode.Text.Trim
            .SalesRepNumber = Me.txtSalesRepNum.Text.Trim
            .Model = Me.txtModel.Text.Trim
            .SerialNumber = Me.txtSerialNum.Text.Trim
            .ItemCode = Me.txtItemCode.Text.Trim
            .ItemDescription = Me.txtItemDesc.Text.Trim
            .Make = Me.ddlManufacturer.SelectedItem.Text.Trim

            If Me.State.isSalutation Then
                Dim salnDV As DataView = LookupListNew.GetSalutationLookupList(State.Company.LanguageId)
                Dim SalutationId As Guid = Me.GetSelectedItem(Me.cboSalutationId)
                Dim SalnDescForCompLang As String = LookupListNew.GetDescriptionFromId(salnDV, SalutationId)
                .Salutation = SalnDescForCompLang
                Me.State.salnForUserLang = Me.cboSalutationId.SelectedItem.Text.Trim
                .SalutationCode = LookupListNew.GetCodeFromId(salnDV, SalutationId)
            End If


            If .UseCustomerProfile =  Codes.YESNO_Y Then
                .FirstName = Me.txtFirstName.Text.Trim
                .MiddleName = Me.txtMiddleName.Text.Trim
                .LastName = Me.txtLastName.Text.Trim
            Else
                .CustomerName = Me.txtCustName.Text.Trim
            End If

            .CustomerTaxID = Me.txtTaxID.Text.Trim
            .CustomerHomePhone = Me.txtHomePhone.Text.Trim
            .CustomerWorkPhone = Me.txtWorkPhone.Text.Trim
            .CustomerEmail = Me.txtEmail.Text.Trim
            .CustomerAddress1 = Me.txtAddress1.Text.Trim
            .CustomerAddress2 = Me.txtAddress2.Text.Trim
            .CustomerCity = Me.txtCity.Text.Trim
            .CustomerState = Me.txtState.Text.Trim
            .CustomerZIP = Me.txtZIP.Text.Trim

            .PaymentType = Me.ddlPYMTType.SelectedValue
            .Occupation = Me.txtOccupation.Text.Trim

            'installment 
            If State.InstallmentAllowed = "Y" AndAlso .PaymentType = "6" Then
                .BankAcctNumber = txtBankAcctNum.Text.Trim
                If blnValidateRequired AndAlso .BankAcctNumber = String.Empty Then
                    State.ColErrLabels.Add(Me.lblBankAcctNum.ID)
                    hasErr = True
                    errMsg.Add(lblBankAcctNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If


                .BankRoutingNumber = txtBankRntNum.Text.Trim
                If blnValidateRequired AndAlso .BankRoutingNumber = String.Empty Then
                    State.ColErrLabels.Add(Me.lblBankRntNum.ID)
                    hasErr = True
                    errMsg.Add(lblBankRntNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                End If

                .BankAcctOwnerName = txtBankOwner.Text.Trim
                If blnValidateRequired AndAlso .BankAcctOwnerName = String.Empty Then
                    State.ColErrLabels.Add(Me.lblBankOwner.ID)
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
                            State.ColErrLabels.Add(Me.lblInsNum.ID)
                            hasErr = True
                            errMsg.Add(lblInsNum.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                        End If
                    Else
                        If blnValidateRequired Then
                            State.ColErrLabels.Add(Me.lblInsNum.ID)
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
                            State.ColErrLabels.Add(Me.lblInsAmt.ID)
                            hasErr = True
                            errMsg.Add(lblInsAmt.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
                        End If
                    Else
                        If blnValidateRequired Then
                            State.ColErrLabels.Add(Me.lblInsAmt.ID)
                            hasErr = True
                            errMsg.Add(lblInsAmt.Text & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED))
                        End If
                    End If
                End If

            End If

            If Not Me.State.Company.ReqCustomerLegalInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CLITYP, "0")) _
                Or Not Me.State.Dealer.RequireCustomerAMLInfoId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_CAIT, "0")) Then '0= None

                If ddlMaritalStatus.SelectedIndex > 0 Then
                    Dim selectedMaritalStatusId As Guid = Me.GetSelectedItem(Me.ddlMaritalStatus)
                    Dim Query = Me.State.MaritalStatusList.FirstOrDefault(Function(r) r.ListItemId = selectedMaritalStatusId)
                    .MaritalStatus = Query.Code
                Else
                    .MaritalStatus = ""
                End If

                If ddlPersonType.SelectedIndex > 0 Then
                    Dim selectedPersonTypeId As Guid = Me.GetSelectedItem(Me.ddlPersonType)
                    Dim Query = Me.State.PersonTypeList.FirstOrDefault(Function(r) r.ListItemId = selectedPersonTypeId)
                    .PersonType = Query.Code
                Else
                    .PersonType = ""
                End If

                If ddlGender.SelectedIndex > 0 Then
                    Dim selectedddlGenderId As Guid = Me.GetSelectedItem(Me.ddlGender)

                    Dim Query = Me.State.GenderList.FirstOrDefault(Function(r) r.ListItemId = selectedddlGenderId)
                    .Gender = Query.Code

                Else
                    .Gender = ""
                End If

                If ddlNationality.SelectedIndex > 0 Then
                    Dim selectedNationalityId As Guid = Me.GetSelectedItem(Me.ddlNationality)
                    Dim Query = Me.State.NationalityList.FirstOrDefault(Function(r) r.ListItemId = selectedNationalityId)
                    .Nationality = Query.Code
                Else
                    .Nationality = ""
                End If

                If ddlPlaceOfBirth.SelectedIndex > 0 Then
                    Dim selectedPlaceOfBirthId As Guid = Me.GetSelectedItem(Me.ddlPlaceOfBirth)
                    Dim Query = Me.State.PlaceOfBirthList.FirstOrDefault(Function(r) r.ListItemId = selectedPlaceOfBirthId)
                    .PlaceOfBirth = Query.Code
                Else
                    .PlaceOfBirth = ""
                End If

                .CUIT_CUIL = moCUIT_CUIL.Text.Trim

            End If
            .MarketingPromoSer = moMarketingPromoSerText.Text
            .MarketingPromoNum = moMarketingPromoNumText.Text

            If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.Dealer.DealerTypeId) = "3" Then
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
            Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
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
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Dim action As DetailPageCommand = State.ActionInProgress

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""

        If Not confResponse Is Nothing Then
            If confResponse = Me.MSG_VALUE_YES Then
                If action = DetailPageCommand.Save Then
                    Me.callPage(CertAddBundleItemForm.URL, State.MyBO)
                ElseIf action = DetailPageCommand.Back Then
                    Me.ReturnToCallingPage(State.DealerID)
                ElseIf action = DetailPageCommand.Redirect_ Then
                    Me.callPage(Certificates.CertificateForm.URL, Me.State.CertID)
                End If
            ElseIf confResponse = Me.MSG_VALUE_NO Then
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
            DisplayMessage(TranslationBase.TranslateLabelOrMessage(Message.SAVE_RECORD_CONFIRMATION) & ". " & TranslationBase.TranslateLabelOrMessage(MSG_PROMPT_FOR_PROCEDD_TO_CERT_DETAIL), "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
            PopulateFormFromBOs()
        Else
            ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
        End If
    End Sub

    'REQ-5681 begin

    Private Sub PopulateMaritalStatusDropdown(ByVal MaritalStatusDropDownList As DropDownList)
        Try

            Dim maritalStatus As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MARITAL_STATUS", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.State.MaritalStatusList = maritalStatus
            MaritalStatusDropDownList.Populate(maritalStatus, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateNationalityDropdown(ByVal nationalityDropDownList As DropDownList)
        Try
            Dim nationalities As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("NATIONALITY", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.State.NationalityList = nationalities
            nationalityDropDownList.Populate(nationalities, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulatePlaceOfBirthDropdown(ByVal PlaceOfBirthDropDownList As DropDownList)
        Try

            Dim placeOfBirth As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PLACEOFBIRTH", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.State.PlaceOfBirthList = placeOfBirth
            PlaceOfBirthDropDownList.Populate(placeOfBirth, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateGenderDropdown(ByVal GenderDropDownList As DropDownList)
        Try

            Dim gender As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("GENDER", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.State.GenderList = gender
            GenderDropDownList.Populate(gender, New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulatePersonTypeDropdown(ByVal PersonTypeDropDownList As DropDownList)
        Try
            Dim personType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PERSON_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.State.PersonTypeList = personType
            PersonTypeDropDownList.Populate(personType, New PopulateOptions() With
                                                         {
                                                                .AddBlankItem = True
                                                          })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'REQ-5681 end
#End Region

#Region "button click handlers"

    Private Sub btnCopyNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyNew.Click
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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If State.IsCertSaved Then
                Me.ReturnToCallingPage(State.DealerID)
            Else
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If PopulateBOsFromForm() Then
                Dim ErrMsg As New Collections.Generic.List(Of String)
                If State.MyBO.Validate(ErrMsg) Then
                    'Prompt user for more bundle items if bundle is allowed
                    If State.AllowBundle = "Y" AndAlso (State.MyBO.GetBundledItemCount < CertAddController.MAX_BUNDLED_ITEMS_ALLOWED) Then
                        Me.DisplayMessage(MSG_PROMPT_FOR_ADD_MORE_BUNDLE_ITEMS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = DetailPageCommand.Save
                    Else
                        SaveTheCertificate(False)
                    End If
                Else
                    ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBundle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBundle.Click
        Try
            PopulateBOsFromForm(False)
            If State.AllowBundle = "Y" Then
                Me.callPage(CertAddBundleItemForm.URL, State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo.Click
        Try
            Me.State.MyBO = New CertAddController()

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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region
End Class