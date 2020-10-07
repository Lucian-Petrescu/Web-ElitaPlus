Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security

Partial Class UserControlBankInfo_New
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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


    Public Const BANK_NAME_CHANGE_EVENT_NAME As String = "BankNameChanged"

#End Region

#Region "State"
    Class MyState
        Public myBankInfoBo As BankInfo
        Public payeeAddress As Address
        Public IsNewObjDirty As Boolean = False
        Public transferType As String
        Public Function IsBODirty() As Boolean
            If myBankInfoBo Is Nothing Then
                Return False
            Else
                If myBankInfoBo.IsNew Then
                    Return IsNewObjDirty
                Else
                    Return myBankInfoBo.IsDirty
                End If

                If payeeAddress.IsNew Then
                    Return IsNewObjDirty
                Else
                    Return payeeAddress.IsDirty
                End If
            End If
        End Function
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

    Private _validateBankInfoCountry As String
    Public Property ValidateBankInfoCountry() As String
        Get
            Return _validateBankInfoCountry
        End Get
        Set(value As String)
            _validateBankInfoCountry = value
        End Set
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

#Region "Properties"

#End Region
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        'If Not IsPostBack Then

        '    If Not Me.State.myBankInfoBo Is Nothing Then
        '        BindBoPropertiesToLabels()
        '        'Page.AddLabelDecorations(Me.State.myBankInfoBo)
        '        If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) And (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then
        '            If Me.State.myBankInfoBo.SourceCountryID.Equals(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE)) Then
        '                'Domestic transfer
        '                DomesticTransfer()
        '            Else
        '                Dim objCountry As New Country(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
        '                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
        '                    'International transfer & Destination is European country
        '                    InternationalEUTransfer()
        '                Else
        '                    'International transfer & Destination is not a European country
        '                    InternationalTransfer()
        '                End If
        '            End If
        '        Else
        '            'Domestic transfer
        '            DomesticTransfer()
        '        End If
        '    Else
        '        SetTheRequiredFields()
        '        'Domestic transfer
        '        DomesticTransfer()
        '    End If
        'End If
    End Sub

    Public Sub SetTheRequiredFields()
        If labelNameonAccount.Text.IndexOf("*") <> 0 Then labelNameonAccount.Text = "* " & labelNameonAccount.Text
        If labelBankID.Text.IndexOf("*") <> 0 Then labelBankID.Text = "* " & labelBankID.Text
        If labelBankAccountNo.Text.IndexOf("*") <> 0 Then labelBankAccountNo.Text = "* " & labelBankAccountNo.Text
        If labelCountryOfBank.Text.IndexOf("*") <> 0 Then labelCountryOfBank.Text = "* " & labelCountryOfBank.Text
        If labelSwiftCode.Text.IndexOf("*") <> 0 Then labelSwiftCode.Text = "* " & labelSwiftCode.Text
        If labelIBAN_Number.Text.IndexOf("*") <> 0 Then labelIBAN_Number.Text = "* " & labelIBAN_Number.Text
        If lblBranchDigit.Text.IndexOf("*") <> 0 Then lblBranchDigit.Text = "* " & lblBranchDigit.Text
        If lblAcctDigit.Text.IndexOf("*") <> 0 Then lblAcctDigit.Text = "* " & lblAcctDigit.Text
        If lblBranchNumber.Text.IndexOf("*") <> 0 Then lblBranchNumber.Text = "* " & lblBranchNumber.Text
        If lblBankName.Text.IndexOf("*") <> 0 Then lblBankName.Text = "* " & lblBankName.Text
        If labelAccountType.Text.IndexOf("*") <> 0 Then labelAccountType.Text = "* " & labelAccountType.Text

    End Sub



    Public Sub Bind(bankinfoBo As BankInfo)
        With State
            .myBankInfoBo = bankinfoBo
        End With
        textboxBankID.Text = String.Empty
        textboxBankAccountNo.Text = String.Empty

        ' SwitchToLastFourDigitsLabelMode(Me.State.myBankInfoBo.ValidateFieldsforFR)


        If State.myBankInfoBo.ValidateFieldsforFR Then
            EnableDisableRequiredControls()
        Else

            'If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then --req-5383
            If (Not (State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (State.myBankInfoBo.CountryID.Equals(Guid.Empty))) Then
                If State.myBankInfoBo.SourceCountryID.Equals(State.myBankInfoBo.CountryID) Then
                    'Domestic transfer
                    DomesticTransfer()
                Else
                    Dim objCountry As New Country(State.myBankInfoBo.CountryID)
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                        'International transfer & Destination is European country
                        InternationalEUTransfer()
                    Else
                        'International transfer & Destination is not a European country
                        InternationalTransfer()
                    End If
                End If
                EnableControlsBasedOnCountry(State.myBankInfoBo.CountryID)
            Else
                'Domestic transfer
                DomesticTransfer()
            End If
        End If
        PopulateControlFromBo()

    End Sub

    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            textboxNameAccount.TabIndex = TabIndexStartingNumber
            moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            txtBankName.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            txtBankBranchName.TabIndex = CType(TabIndexStartingNumber + 3, Int16)

            textboxBankID.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            txtBranchNumber.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            txtBranchDigit.TabIndex = CType(TabIndexStartingNumber + 6, Int16)

            textboxBankAccountNo.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
            moAccountTypeDrop.TabIndex = CType(TabIndexStartingNumber + 8, Int16)
            txtAcctDigit.TabIndex = CType(TabIndexStartingNumber + 9, Int16)

            ' Me.txtBranchNumber.TabIndex = CType(TabIndexStartingNumber + 8, Int16)

            textboxSwiftCode.TabIndex = CType(TabIndexStartingNumber + 10, Int16)
            textboxIBAN_Number.TabIndex = CType(TabIndexStartingNumber + 11, Int16)
            txtBankLookupCode.TabIndex = CType(TabIndexStartingNumber + 12, Int16)
            txtBankSubcode.TabIndex = CType(TabIndexStartingNumber + 13, Int16)
            txtBankSortCode.TabIndex = CType(TabIndexStartingNumber + 14, Int16)
            txtTransLimit.TabIndex = CType(TabIndexStartingNumber + 15, Int16)
            cboBankSortCodes.TabIndex = CType(TabIndexStartingNumber + 16, Int16)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "Account_Name", labelNameonAccount)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "Bank_Id", labelBankID)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "Account_Number", labelBankAccountNo)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "CountryID", labelCountryOfBank)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "SwiftCode", labelSwiftCode)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "IbanNumber", labelIBAN_Number)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "AccountTypeId", labelAccountType)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "TransactionLimit", labelTranslimit)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BankLookupCode", labelBanklookup)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BankSubCode", labelbanksubcode)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BankSortCode", labelBankSortCode)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BranchDigit", lblBranchDigit)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "AccountDigit", lblAcctDigit)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BranchNumber", lblBranchNumber)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "BankName", lblBankName)
        Page.BindBOPropertyToLabel(State.myBankInfoBo, "TaxId", lblTaxId)

        Page.BindBOPropertyToLabel(State.payeeAddress, "Address1", labelAddress1)
        Page.BindBOPropertyToLabel(State.payeeAddress, "Address2", labelAddress2)
        Page.BindBOPropertyToLabel(State.payeeAddress, "City", labelCity)
        Page.BindBOPropertyToLabel(State.payeeAddress, "PostalCode", labelPostalCode)

        ' Move to BankInfo BO 02/08/2008
        Page.ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Public Function PopulateAddressInfo() As Address
        State.payeeAddress = New Address()
        BindBoPropertiesToLabels()
        Page.PopulateBOProperty(State.payeeAddress, "Address1", txtAddress1)
        Page.PopulateBOProperty(State.payeeAddress, "Address2", txtAddress2)
        Page.PopulateBOProperty(State.payeeAddress, "City", txtCity)
        Page.PopulateBOProperty(State.payeeAddress, "PostalCode", txtPostalCode)
        Page.PopulateBOProperty(State.payeeAddress, "CountryId", State.myBankInfoBo.CountryID)

        Return State.payeeAddress
    End Function

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType, LTemp As Long
        If State.myBankInfoBo IsNot Nothing Then
            With State.myBankInfoBo
                BindBoPropertiesToLabels()

                If .IsNew AndAlso .Account_Name <> textboxNameAccount.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "Account_Name", textboxNameAccount)

                If .IsNew AndAlso .Bank_Id <> textboxBankID.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "Bank_Id", textboxBankID)

                If .IsNew AndAlso .Account_Number <> textboxBankAccountNo.Text Then State.IsNewObjDirty = True

                Page.PopulateBOProperty(State.myBankInfoBo, "Account_Number", textboxBankAccountNo)

                guidTemp = .CountryID
                Page.PopulateBOProperty(State.myBankInfoBo, "CountryID", moCountryDrop_WRITE)
                If .IsNew AndAlso .CountryID <> guidTemp Then State.IsNewObjDirty = True

                If .IsNew AndAlso .IbanNumber <> textboxIBAN_Number.Text Then State.IsNewObjDirty = True

                Page.PopulateBOProperty(State.myBankInfoBo, "IbanNumber", textboxIBAN_Number)

                If .IsNew AndAlso .SwiftCode <> textboxSwiftCode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "SwiftCode", textboxSwiftCode)

                guidTemp = .AccountTypeId
                Page.PopulateBOProperty(State.myBankInfoBo, "AccountTypeId", moAccountTypeDrop)
                If .IsNew AndAlso .AccountTypeId <> guidTemp Then State.IsNewObjDirty = True

                dTemp = .TransactionLimit
                Page.PopulateBOProperty(State.myBankInfoBo, "TransactionLimit", txtTransLimit)
                If .IsNew Then
                    If (.TransactionLimit Is Nothing) AndAlso (Not (dTemp Is Nothing)) Then
                        State.IsNewObjDirty = True
                    ElseIf (Not (.TransactionLimit Is Nothing)) AndAlso (dTemp Is Nothing) Then
                        State.IsNewObjDirty = True
                    ElseIf (Not (.TransactionLimit Is Nothing)) AndAlso (Not (dTemp Is Nothing)) AndAlso (.TransactionLimit.Value <> dTemp.Value) Then
                        State.IsNewObjDirty = True
                    End If
                End If

                If .IsNew AndAlso .BankLookupCode <> txtBankLookupCode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BankLookupCode", txtBankLookupCode)

                If .IsNew AndAlso .BankSubCode <> txtBankSubcode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BankSubCode", txtBankSubcode)

                If txtBankName.Visible = True Then
                    If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                    Page.PopulateBOProperty(State.myBankInfoBo, "BankName", txtBankName)
                Else
                    'guidTemp = .AccountTypeId
                    Page.PopulateBOProperty(State.myBankInfoBo, "BankName", moBankName, False)
                    If .IsNew AndAlso (moBankName.SelectedIndex <> -1 AndAlso .BankName <> moBankName.SelectedItem.Text) Then State.IsNewObjDirty = True
                End If

                If .IsNew AndAlso .BranchName <> txtBankBranchName.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BranchName", txtBankBranchName)

                If .IsNew AndAlso .BankSortCode <> txtBankSortCode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BankSortCode", txtBankSortCode)

                If cboBankSortCodes.Visible = True Then
                    Page.PopulateBOProperty(State.myBankInfoBo, "BankSortCode", cboBankSortCodes, False)
                    If .IsNew AndAlso cboBankSortCodes.SelectedIndex <> -1 Then State.IsNewObjDirty = True
                End If

                If .IsNew AndAlso .BranchDigit Is Nothing AndAlso .BranchDigit <> LongType.Parse(txtBranchDigit.Text) Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BranchDigit", txtBranchDigit)

                If .IsNew AndAlso .AccountDigit Is Nothing AndAlso .AccountDigit <> LongType.Parse(txtAcctDigit.Text) Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "AccountDigit", txtAcctDigit)

                If .IsNew AndAlso .BranchNumber Is Nothing AndAlso .BranchNumber <> LongType.Parse(txtBranchNumber.Text) Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BranchNumber", txtBranchNumber)

                If .IsNew AndAlso .TaxId <> txtTaxId.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "TaxId", txtTaxId)

                If .IsNew AndAlso .BankInfoAddress.Address1 <> txtAddress1.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo.BankInfoAddress, "Address1", txtAddress1)

                If .IsNew AndAlso .BankInfoAddress.Address2 <> txtAddress2.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo.BankInfoAddress, "Address2", txtAddress2)

                If .IsNew AndAlso .BankInfoAddress.City <> txtCity.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo.BankInfoAddress, "City", txtCity)

                If .IsNew AndAlso .BankInfoAddress.PostalCode <> txtPostalCode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo.BankInfoAddress, "PostalCode", txtPostalCode)

                'If txtAutoBankName.Text.Trim <> String.Empty AndAlso txtAutoBankName.Text.Trim.ToUpper <> inpDefaultBNDesc.Value.Trim.ToUpper Then
                '   ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC_FOR_DENIED_CLAIMS"), txtAutoBankName, lblDefaultSCForDeniedClaims, New Exception("Service Center Not Found")))
                'Else               
                'End If
                ' Move to BankInfo BO 02/08/2008
                If blnValidate AndAlso .IsDirty Then
                    .Validate()
                    If Page.ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End If

                If Not blnExcludeSave Then .Save()
            End With
        End If
    End Sub

    Private Sub PopulateControlFromBo()
        If State.myBankInfoBo IsNot Nothing Then

            PopulateAccountTypeDropdown()
            With State.myBankInfoBo
                If Not .CountryID.Equals(Guid.Empty) Then
                    LoadCountryList(False)
                Else
                    LoadCountryList()
                End If

                If .ValidateFieldsforFR Then
                    Page.PopulateControlFromBOProperty(textboxNameAccount, .Account_Name)
                    Page.PopulateControlFromBOProperty(textboxBankAccountNo, .Account_Number)
                    Page.PopulateControlFromBOProperty(textboxIBAN_Number, .IbanNumber)
                Else
                    Page.PopulateControlFromBOProperty(textboxNameAccount, .Account_Name)
                    Page.PopulateControlFromBOProperty(textboxBankAccountNo, .Account_Number)
                    Page.PopulateControlFromBOProperty(textboxIBAN_Number, .IbanNumber)
                End If

                Page.PopulateControlFromBOProperty(textboxBankID, .Bank_Id)

                Page.PopulateControlFromBOProperty(textboxSwiftCode, .SwiftCode)
                Page.SetSelectedItem(moCountryDrop_WRITE, .CountryID)
                If .AccountTypeId.Equals(System.Guid.Empty) Then
                    moAccountTypeDrop.SelectedIndex = 0
                Else
                    Page.SetSelectedItem(moAccountTypeDrop, .AccountTypeId)
                End If

                If txtBankName.Visible Then
                    Page.PopulateControlFromBOProperty(txtBankName, .BankName)
                ElseIf moBankName.Visible Then
                    If .BankName IsNot Nothing Then Page.SetSelectedItemByText(moBankName, .BankName)
                Else
                    If .BankName IsNot Nothing Then
                        If (moBankName.Items.Count > 0) Then
                            Page.SetSelectedItemByText(moBankName, .BankName)
                        Else
                            Page.PopulateControlFromBOProperty(txtBankName, .BankName)
                        End If
                    End If
                End If

                Page.PopulateControlFromBOProperty(txtTransLimit, .TransactionLimit)
                Page.PopulateControlFromBOProperty(txtBankLookupCode, .BankLookupCode)
                Page.PopulateControlFromBOProperty(txtBankSubcode, .BankSubCode)
                'Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                Page.PopulateControlFromBOProperty(txtBankBranchName, .BranchName)
                Page.PopulateControlFromBOProperty(txtBankSortCode, .BankSortCode)

                If cboBankSortCodes.Visible Then
                    If .BankSortCode IsNot Nothing Then
                        Page.SetSelectedItemByText(cboBankSortCodes, .BankName)
                    ElseIf (cboBankSortCodes.Items.Count > 0) Then
                        Page.SetSelectedItemByText(cboBankSortCodes, .BankName)
                    End If
                End If

                Page.PopulateControlFromBOProperty(txtBranchDigit, .BranchDigit)
                Page.PopulateControlFromBOProperty(txtAcctDigit, .AccountDigit)
                Page.PopulateControlFromBOProperty(txtBranchNumber, .BranchNumber)
                Page.PopulateControlFromBOProperty(txtTaxId, .TaxId)

                Page.PopulateControlFromBOProperty(txtAddress1, .BankInfoAddress.Address1)
                Page.PopulateControlFromBOProperty(txtAddress2, .BankInfoAddress.Address2)
                Page.PopulateControlFromBOProperty(txtCity, .BankInfoAddress.City)
                Page.PopulateControlFromBOProperty(txtPostalCode, .BankInfoAddress.PostalCode)

            End With
        End If
    End Sub

    Public Sub SetCountryValue(oCountryID As Guid)
        Page.SetSelectedItem(moCountryDrop_WRITE, oCountryID)
    End Sub

    Public Sub setSwiftCode(objBankinfo As BankInfo)
        textboxSwiftCode.Text = objBankinfo.BankLookupCode
    End Sub

    Public Sub setCustomerName(customerName As String)
        textboxNameAccount.Text = customerName
    End Sub

    Public Sub ChangeEnabledControlProperty(blnEnabledState As Boolean)
        Page.ChangeEnabledControlProperty(textboxNameAccount, blnEnabledState)
        Page.ChangeEnabledControlProperty(moCountryDrop_WRITE, blnEnabledState)
        Page.ChangeEnabledControlProperty(textboxBankID, blnEnabledState)
        Page.ChangeEnabledControlProperty(textboxBankAccountNo, blnEnabledState)
        Page.ChangeEnabledControlProperty(textboxIBAN_Number, blnEnabledState)
        Page.ChangeEnabledControlProperty(textboxSwiftCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(moAccountTypeDrop, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBankLookupCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBankSubcode, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtTransLimit, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBankName, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBankBranchName, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBankSortCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtBranchDigit, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtAcctDigit, blnEnabledState)
        Page.ChangeEnabledControlProperty(txtTaxId, blnEnabledState)
    End Sub

    Private Sub LoadCountryList(Optional ByVal nothingSelcted As Boolean = True)
        Dim oCountryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
        moCountryDrop_WRITE.Populate(oCountryList, New PopulateOptions() With
                                        {
                                        .AddBlankItem = nothingSelcted
                                        })


    End Sub
    Public Sub LoadBankSortCodeList(oCompany As Company, certTaxIdNumber As String)
        If oCompany.AttributeValues.Contains(Codes.DEFAULT_CLAIM_BANK_SORT_CODE) Then
            If oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_BANK_SORT_CODE) = Codes.YESNO_Y Then
                DisplayCboBankSortCode()
                Dim oBankSortCodeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="BANK_SORT_CODE")
                cboBankSortCodes.Populate(oBankSortCodeList, New PopulateOptions() With
                                            {
                                            .AddBlankItem = True,
                                            .TextFunc = AddressOf PopulateOptions.GetCode,
                                            .ValueFunc = AddressOf PopulateOptions.GetDescription
                                            })
            End If
        End If

        If oCompany.AttributeValues.Contains(Codes.DEFAULT_CLAIM_BANK_SUB_CODE) Then
            txtBankSubcode.Text = oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_BANK_SUB_CODE)
        End If

        If (Not String.IsNullOrEmpty(certTaxIdNumber) And oCompany.AttributeValues.Contains(Codes.AUTO_POPULATE_CERT_TAX_ID)) Then
            If oCompany.AttributeValues.Value(Codes.AUTO_POPULATE_CERT_TAX_ID) = Codes.YESNO_Y Then
                txtTaxId.Text = certTaxIdNumber
            End If
        End If


    End Sub
    Public Sub SetFieldsEmpty()
        txtBankSubcode.Text = String.Empty
        txtTaxId.Text = String.Empty
    End Sub
    Private Sub PopulateAccountTypeDropdown()
        Dim AcctType As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("ACCTTYPE", Thread.CurrentPrincipal.GetLanguageCode())
        moAccountTypeDrop.Populate(AcctType, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })
    End Sub

    Public Sub SetRequiredFieldsForDealerWithGiftCard1(dealer As Dealer)
        If (dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
            If labelAddress1.Text.IndexOf("*") <> 0 Then labelAddress1.Text = "* " & labelAddress1.Text
            If labelCity.Text.IndexOf("*") <> 0 Then labelCity.Text = "* " & labelCity.Text
            If labelPostalCode.Text.IndexOf("*") <> 0 Then labelPostalCode.Text = "* " & labelPostalCode.Text
        End If
    End Sub

    Private Sub PopulateBankNameDropdown(SelectedCountryID As Guid)
        '    Dim obank As New BankName
        Dim BankDV As DataView = New DataView(BankName.LoadBankNameByCountry(SelectedCountryID))
        Page.BindListControlToDataView(moBankName, BankDV, "DESCRIPTION", "ID", True)

    End Sub

    Private Sub DisplayBankID()
        ControlMgr.SetVisibleControl(Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Page, textboxBankID, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceBankID, True)
    End Sub

    Private Sub HideNameonAccount()
        ControlMgr.SetVisibleControl(Page, labelNameonAccount, False)
        ControlMgr.SetVisibleControl(Page, textboxNameAccount, False)
        textboxNameAccount.Text = String.Empty
    End Sub

    Private Sub HideCountryOfBank()
        ControlMgr.SetVisibleControl(Page, labelCountryOfBank, False)
        ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, False)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Page, textboxBankID, False)
        ControlMgr.SetVisibleControl(Page, LabelSpaceBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankNameDP()
        ControlMgr.SetVisibleControl(Page, lblBankName, True)
        ControlMgr.SetVisibleControl(Page, moBankName, True)
        ControlMgr.SetVisibleControl(Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Page, textboxBankID, True)
        textboxBankID.Text = String.Empty
    End Sub
    Private Sub HideBankName()
        ControlMgr.SetVisibleControl(Page, moBankName, False)
        ControlMgr.SetVisibleControl(Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Page, lblBankName, False)

    End Sub

    Private Sub DisplayBankNametxt()
        ControlMgr.SetVisibleControl(Page, lblBankName, True)
        ControlMgr.SetVisibleControl(Page, txtBankName, True)
        ControlMgr.SetVisibleControl(Page, moBankName, False)
        moBankName.SelectedIndex = -1
    End Sub

    Private Sub DisplayBankAccNo()
        ControlMgr.SetVisibleControl(Page, labelBankAccountNo, True)
        ControlMgr.SetVisibleControl(Page, textboxBankAccountNo, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceAccNo, True)
    End Sub

    Private Sub HideBankAccNo()
        ControlMgr.SetVisibleControl(Page, labelBankAccountNo, False)
        ControlMgr.SetVisibleControl(Page, labelBankAccountNo_Last4Digits, False)
        ControlMgr.SetVisibleControl(Page, textboxBankAccountNo, False)
        ControlMgr.SetVisibleControl(Page, LabelSpaceAccNo, False)
        textboxBankAccountNo.Text = String.Empty
    End Sub

    Private Sub DisplaySwiftCode()
        ControlMgr.SetVisibleControl(Page, labelSwiftCode, True)
        ControlMgr.SetVisibleControl(Page, textboxSwiftCode, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceSwiftCode, True)
    End Sub

    Private Sub HideSwiftCode()
        ControlMgr.SetVisibleControl(Page, labelSwiftCode, False)
        ControlMgr.SetVisibleControl(Page, textboxSwiftCode, False)
        ControlMgr.SetVisibleControl(Page, LabelSpaceSwiftCode, False)
        textboxSwiftCode.Text = String.Empty
    End Sub

    Private Sub DisplayIBAN_Number()
        ControlMgr.SetVisibleControl(Page, labelIBAN_Number, True)
        ControlMgr.SetVisibleControl(Page, textboxIBAN_Number, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceIBAN, True)
    End Sub

    Private Sub HideIBAN_Number()
        ControlMgr.SetVisibleControl(Page, labelIBAN_Number, False)
        ControlMgr.SetVisibleControl(Page, textboxIBAN_Number, False)
        ControlMgr.SetVisibleControl(Page, LabelSpaceIBAN, False)
        textboxIBAN_Number.Text = String.Empty
    End Sub
    Private Sub DisplayAccountDigit()
        ControlMgr.SetVisibleControl(Page, lblAcctDigit, True)
        ControlMgr.SetVisibleControl(Page, txtAcctDigit, True)
        ControlMgr.SetVisibleControl(Page, lblBankAcctDigit, True)
    End Sub

    Private Sub HideAccountDigit()
        ControlMgr.SetVisibleControl(Page, lblAcctDigit, False)
        ControlMgr.SetVisibleControl(Page, txtAcctDigit, False)
        ControlMgr.SetVisibleControl(Page, lblBankAcctDigit, False)
        txtAcctDigit.Text = String.Empty
    End Sub

    Private Sub DisplayBranchDigit()
        ControlMgr.SetVisibleControl(Page, lblBranchDigit, True)
        ControlMgr.SetVisibleControl(Page, txtBranchDigit, True)
        ControlMgr.SetVisibleControl(Page, lblBankBranchDigit, True)
    End Sub

    Private Sub HideBranchDigit()
        ControlMgr.SetVisibleControl(Page, lblBranchDigit, False)
        ControlMgr.SetVisibleControl(Page, txtBranchDigit, False)
        ControlMgr.SetVisibleControl(Page, lblBankBranchDigit, False)
        txtBranchDigit.Text = String.Empty
    End Sub

    Private Sub HideTranslimit()
        ControlMgr.SetVisibleControl(Page, labelTranslimit, False)
        ControlMgr.SetVisibleControl(Page, txtTransLimit, False)
        ControlMgr.SetVisibleControl(Page, lblTransLimit, False)
        txtTransLimit.Text = String.Empty
    End Sub
    Private Sub DisplayTranslimit()
        ControlMgr.SetVisibleControl(Page, labelTranslimit, True)
        ControlMgr.SetVisibleControl(Page, txtTransLimit, True)
        ControlMgr.SetVisibleControl(Page, lblTransLimit, True)
    End Sub
    Private Sub HideBankSubCode()
        ControlMgr.SetVisibleControl(Page, labelbanksubcode, False)
        ControlMgr.SetVisibleControl(Page, txtBankSubcode, False)
        ControlMgr.SetVisibleControl(Page, lblBankSubcode, False)
        txtBankSubcode.Text = String.Empty
    End Sub
    Private Sub DisplayBankSubCode()
        ControlMgr.SetVisibleControl(Page, lblBankSubcode, True)
        ControlMgr.SetVisibleControl(Page, txtBankSubcode, True)
        ControlMgr.SetVisibleControl(Page, labelbanksubcode, True)
    End Sub

    Private Sub HideBanklookupCode()
        ControlMgr.SetVisibleControl(Page, labelBanklookup, False)
        ControlMgr.SetVisibleControl(Page, txtBankLookupCode, False)
        ControlMgr.SetVisibleControl(Page, lblBankLookupCode, False)
        txtBankLookupCode.Text = String.Empty
    End Sub
    Private Sub DisplayBankLookupCode()
        ControlMgr.SetVisibleControl(Page, labelBanklookup, True)
        ControlMgr.SetVisibleControl(Page, txtBankLookupCode, True)
        ControlMgr.SetVisibleControl(Page, lblBankLookupCode, True)
    End Sub
    Private Sub HideBankSortCode()
        ControlMgr.SetVisibleControl(Page, labelBankSortCode, False)
        ControlMgr.SetVisibleControl(Page, txtBankSortCode, False)
        ControlMgr.SetVisibleControl(Page, lblBankSortCode, False)
        txtBankSubcode.Text = String.Empty
    End Sub
    Private Sub DisplayBankSortCode()
        ControlMgr.SetVisibleControl(Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, txtBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, lblBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, cboBankSortCodes, False)
    End Sub
    Public Sub HideCboBankSortCode()
        ControlMgr.SetVisibleControl(Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, txtBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, lblBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, cboBankSortCodes, False)
    End Sub
    Private Sub DisplayCboBankSortCode()
        ControlMgr.SetVisibleControl(Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Page, cboBankSortCodes, True)
        ControlMgr.SetVisibleControl(Page, txtBankSortCode, False)
        ControlMgr.SetVisibleControl(Page, lblBankSortCode, False)
    End Sub
    Private Sub HideBranchName()
        ControlMgr.SetVisibleControl(Page, lblBranchName, False)
        ControlMgr.SetVisibleControl(Page, txtBankBranchName, False)
        ControlMgr.SetVisibleControl(Page, lblSpaceBranchName, False)
        txtBankBranchName.Text = String.Empty
    End Sub
    Private Sub DisplayBranchName()
        ControlMgr.SetVisibleControl(Page, lblBranchName, True)
        ControlMgr.SetVisibleControl(Page, txtBankBranchName, True)
        ControlMgr.SetVisibleControl(Page, lblSpaceBranchName, True)
    End Sub

    Private Sub HideBranchNumber()
        ControlMgr.SetVisibleControl(Page, lblBranchNumber, False)
        ControlMgr.SetVisibleControl(Page, txtBranchNumber, False)
        ControlMgr.SetVisibleControl(Page, lblBankBranchNumber, False)
        txtBranchNumber.Text = String.Empty
    End Sub
    Private Sub DisplayBranchNumber()
        ControlMgr.SetVisibleControl(Page, lblBranchNumber, True)
        ControlMgr.SetVisibleControl(Page, txtBranchNumber, True)
        ControlMgr.SetVisibleControl(Page, lblBankBranchNumber, True)
    End Sub
    Public Sub HideTaxId()
        ControlMgr.SetVisibleControl(Page, labelTaxId, False)
        ControlMgr.SetVisibleControl(Page, txtTaxId, False)
        ControlMgr.SetVisibleControl(Page, lblTaxId, False)
        txtTaxId.Text = String.Empty
    End Sub
    Public Sub DisplayTaxId()
        ControlMgr.SetVisibleControl(Page, labelTaxId, True)
        ControlMgr.SetVisibleControl(Page, txtTaxId, True)
        ControlMgr.SetVisibleControl(Page, lblTaxId, True)
    End Sub

    Private Sub DisplayAccountType()
        ControlMgr.SetVisibleControl(Page, labelAccountType, True)
        ControlMgr.SetVisibleControl(Page, moAccountTypeDrop, True)
    End Sub
    Private Sub HideAccountType()
        ControlMgr.SetVisibleControl(Page, labelAccountType, False)
        ControlMgr.SetVisibleControl(Page, moAccountTypeDrop, False)
    End Sub

    Private Sub DisplayAddressControls()
        ControlMgr.SetVisibleControl(Page, labelAddress1, True)
        ControlMgr.SetVisibleControl(Page, lblAddress1, True)
        ControlMgr.SetVisibleControl(Page, txtAddress1, True)

        ControlMgr.SetVisibleControl(Page, labelAddress2, True)
        ControlMgr.SetVisibleControl(Page, lblAddress2, True)
        ControlMgr.SetVisibleControl(Page, txtAddress2, True)

        ControlMgr.SetVisibleControl(Page, labelCity, True)
        ControlMgr.SetVisibleControl(Page, lblCity, True)
        ControlMgr.SetVisibleControl(Page, txtCity, True)

        ControlMgr.SetVisibleControl(Page, labelPostalCode, True)
        ControlMgr.SetVisibleControl(Page, lblPostalCode, True)
        ControlMgr.SetVisibleControl(Page, txtPostalCode, True)
    End Sub

    Private Sub HideAddressControls()
        ControlMgr.SetVisibleControl(Page, labelAddress1, False)
        ControlMgr.SetVisibleControl(Page, lblAddress1, False)
        ControlMgr.SetVisibleControl(Page, txtAddress1, False)

        ControlMgr.SetVisibleControl(Page, labelAddress2, False)
        ControlMgr.SetVisibleControl(Page, lblAddress2, False)
        ControlMgr.SetVisibleControl(Page, txtAddress2, False)

        ControlMgr.SetVisibleControl(Page, labelCity, False)
        ControlMgr.SetVisibleControl(Page, lblCity, False)
        ControlMgr.SetVisibleControl(Page, txtCity, False)

        ControlMgr.SetVisibleControl(Page, labelPostalCode, False)
        ControlMgr.SetVisibleControl(Page, lblPostalCode, False)
        ControlMgr.SetVisibleControl(Page, txtPostalCode, False)
    End Sub

    Private Sub DomesticTransfer()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = True
            State.myBankInfoBo.InternationalEUTransfer = False
            State.myBankInfoBo.InternationalTransfer = False
        End If
        EnableDisableRequiredControls()
    End Sub
    Private Sub InternationalEUTransfer()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = False
            State.myBankInfoBo.InternationalEUTransfer = True
            State.myBankInfoBo.InternationalTransfer = False
        End If
        EnableDisableRequiredControls()
    End Sub

    Private Sub InternationalTransfer()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = False
            State.myBankInfoBo.InternationalEUTransfer = False
            State.myBankInfoBo.InternationalTransfer = True
        End If
        EnableDisableRequiredControls()
    End Sub

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        If (Not (State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) And (Not (Page.GetSelectedItem(moCountryDrop_WRITE).Equals(Guid.Empty))) Then
            If State.myBankInfoBo.SourceCountryID.Equals(Page.GetSelectedItem(moCountryDrop_WRITE)) Then
                'Domestic transfer
                DomesticTransfer()
            Else
                Dim objCountry As New Country(Page.GetSelectedItem(moCountryDrop_WRITE))
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                    'International transfer 
                    InternationalEUTransfer()
                Else
                    'International transfer & Destination is not a European country
                    InternationalTransfer()
                End If
            End If

            ' aDefaultBN.ContextKey = GuidControl.GuidToHexString(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
            EnableControlsBasedOnCountry(Page.GetSelectedItem(moCountryDrop_WRITE))
        Else
            'Domestic transfer
            DomesticTransfer()
        End If

    End Sub

    Public Sub EnableDisableControls()
        ''when control is loaded for first time...assume it is a Domestic transfer
        ''Domestic transfer---display bank Id and bank Account # controls 
        'DisplayBankID()
        'DisplayBankAccNo()

        'DisplayBankLookupCode()
        'DisplayBankSortCode()
        'DisplayBankSubCode()
        'DisplayTranslimit()

        ''Domestic transfer---hide swift code & Iban number controls
        'HideSwiftCode()
        'HideIBAN_Number()

        EnableDisableRequiredControls()
    End Sub

    Public Sub EnableDisableRequiredControls()

        If State.myBankInfoBo IsNot Nothing Then

            If State.myBankInfoBo.ValidateFieldsforFR = True Then

                HideNameonAccount()
                HideCountryOfBank()
                DisplayBankNametxt()
                HideBankId()
                HideBankAccNo()

                DisplayBankLookupCode()
                HideBankSortCode()

                HideBankSubCode()
                HideTranslimit()
                HideBranchName()
                HideAccountDigit()

                HideSwiftCode()
                DisplayIBAN_Number()

                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideTaxId()
                HideBankName()
                HideAccountType()
                HideAddressControls()

            ElseIf State.myBankInfoBo.SepaEUBankTransfer = True Then
                DisplaySwiftCode()
                DisplayIBAN_Number()

                HideAddressControls()
                HideBankName()
                HideBankId()
                HideBankAccNo()
                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideBanklookupCode()
                HideBankSortCode()

                HideBankSubCode()
                HideTranslimit()
                HideTaxId()
                HideBranchName()
                HideAccountType()

            ElseIf State.myBankInfoBo.DomesticTransfer = True Then

                DisplayBankNametxt()
                DisplayBankID()
                DisplayBankAccNo()

                DisplayBankLookupCode()
                DisplayBankSortCode()
                DisplayBankSubCode()
                DisplayTranslimit()
                DisplayBranchName()
                DisplayAccountDigit()

                'Domestic transfer---hide swift code & Iban number controls
                HideSwiftCode()
                'HideIBAN_Number()


                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()

            ElseIf State.myBankInfoBo.InternationalTransfer = True Then

                'International transfer & Destination is not a European country---display bank Id, bank Account #, Swift Code controls 
                DisplayBankNametxt()
                DisplayBankID()
                DisplayBankAccNo()
                DisplaySwiftCode()

                DisplayBankLookupCode()
                DisplayBankSortCode()
                DisplayBankSubCode()
                DisplayTranslimit()


                'International transfer & Destination is European country---hide Iban number controls
                'HideIBAN_Number()

                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()


            ElseIf State.myBankInfoBo.InternationalEUTransfer = True Then

                'International transfer & Destination is European country---hide bank Id and bank Account # controls 
                HideBankName()
                HideBankId()
                HideBankAccNo()

                'International transfer & Destination is European country---display swift code & Iban number controls
                DisplaySwiftCode()
                DisplayIBAN_Number()

                DisplayBankLookupCode()
                DisplayBankSortCode()
                DisplayBankSubCode()
                DisplayTranslimit()

                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()

            Else
                DisplayBankID()
                DisplayBankAccNo()

                'Domestic transfer---hide swift code & Iban number controls
                HideSwiftCode()
                'HideIBAN_Number()
                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()

            End If
        End If
    End Sub

    Public Sub GetBankIdForBankName(sender As Object, e As System.EventArgs) Handles moBankName.SelectedIndexChanged
        If Not Page.GetSelectedItem(moBankName).Equals(Guid.Empty) Then
            Dim boBankName As BankName
            boBankName = New BankName(Page.GetSelectedItem(moBankName))
            textboxBankID.Text = boBankName.Code
        Else
            textboxBankID.Text = String.Empty
        End If
    End Sub

    '#Region "Ajax related"

    '    <System.Web.Services.WebMethod()> _
    '    <Script.Services.ScriptMethod()> _
    '    Public Shared Function PopulateBankNameDrop(ByVal prefixText As String, ByVal count As Integer, ByVal Contextkey As String) As String()
    '        Dim boBank As BankInfo
    '        Dim UCBank As UserControlBankInfo_New
    '        Dim dv As DataView = boBank.GetBankNamebyCountry(New Guid(Contextkey))
    '        Return AjaxController.BindAutoComplete(prefixText, dv)
    '    End Function
    '#End Region

    '    Private Sub txtAutoBankName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAutoBankName.TextChanged
    '        If Not Me.inpDefaultBNId.Value Is Nothing Then
    '            Dim boBank As BankInfo
    '            boBank = New BankInfo(New Guid(Me.inpDefaultBNId.Value))
    '            textboxBankID.Text = boBank.Bank_Id
    '        End If

    '    End Sub

    Public Sub EnableControlsBasedOnCountry(SelectedCountryId As Guid)

        'If Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty)) Then ' req-5383

        If Not (SelectedCountryId.Equals(Guid.Empty)) Then ' req-5383
            Dim ocountry As New Country(SelectedCountryId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, ocountry.UseBankListId) = Codes.YESNO_Y Then
                DisplayBankNameDP()
                DisplayBankID()
                PopulateBankNameDropdown(SelectedCountryId)
                ControlMgr.SetEnableControl(Page, textboxBankID, False)
            Else
                DisplayBankNametxt()
                ControlMgr.SetEnableControl(Page, textboxBankID, True)
            End If
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, ocountry.ValidateBankInfoId) = Codes.Country_Code_Brasil Then
                _validateBankInfoCountry = Codes.Country_Code_Brasil
                State.myBankInfoBo.ValidateFieldsforBR = True

                DisplayBankAccNo()
                DisplayAccountDigit()
                DisplayBranchDigit()
                DisplayBranchName()
                DisplayBranchNumber()
                DisplayAccountType()
                DisplayAddressControls()

                HideSwiftCode()
                'HideIBAN_Number()
                HideBanklookupCode()
                HideBankSortCode()
                HideBankSubCode()
                HideTranslimit()
                'HideAddressControls()

                SetTheRequiredFields()
            ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, ocountry.ValidateBankInfoId) = Codes.Country_Code_Argentina Then
                _validateBankInfoCountry = Codes.Country_Code_Argentina
                State.myBankInfoBo.ValidateFieldsforBR = False
                DisplayAccountType()
                DisplayAddressControls()

                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideAddressControls()
            ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, ocountry.ValidateBankInfoId) = Codes.Country_Code_France) Then
                Dim Payee As String
                If (CType(Parent.FindControl("cboPayeeSelector"), DropDownList) IsNot Nothing) Then

                    If (CType(Parent.FindControl("cboPayeeSelector"), DropDownList).Items.Count > 0 And CType(Parent.FindControl("cboPayeeSelector"), DropDownList).SelectedItem IsNot Nothing) Then
                        Payee = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, ElitaPlusPage.GetSelectedItem(CType(Parent.FindControl("cboPayeeSelector"), DropDownList)))
                    End If

                    If (Payee = ClaimInvoice.PAYEE_OPTION_CUSTOMER) Then
                        _validateBankInfoCountry = Codes.Country_Code_France

                        State.myBankInfoBo.ValidateFieldsforBR = False
                        DisplayIBAN_Number()
                        DisplayAddressControls()

                        HideAccountType()
                        HideBankAccNo()
                        HideAccountDigit()
                        HideBranchDigit()
                        HideBranchName()
                        HideBranchNumber()
                        HideSwiftCode()
                        HideBanklookupCode()
                        HideBankSortCode()
                        HideBankSubCode()
                        HideTranslimit()
                        HideBankName()
                        HideBankId()
                        HideTaxId()
                        SetTheRequiredFields()
                    End If
                End If

            Else
                State.myBankInfoBo.ValidateFieldsforBR = False
                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideAddressControls()
            End If
        End If

    End Sub

    Public Sub DisableAllFields()
        ControlMgr.SetEnableControl(Page, textboxNameAccount, False)
        ControlMgr.SetEnableControl(Page, moCountryDrop_WRITE, False)
        ControlMgr.SetEnableControl(Page, txtBankName, False)
        ControlMgr.SetEnableControl(Page, moBankName, False)
        ControlMgr.SetEnableControl(Page, txtBankBranchName, False)
        ControlMgr.SetEnableControl(Page, textboxBankID, False)
        ControlMgr.SetEnableControl(Page, textboxBankAccountNo, False)
        ControlMgr.SetEnableControl(Page, txtAcctDigit, False)
        ControlMgr.SetEnableControl(Page, moAccountTypeDrop, False)
        ControlMgr.SetEnableControl(Page, txtBranchNumber, False)
        ControlMgr.SetEnableControl(Page, txtBranchDigit, False)
        ControlMgr.SetEnableControl(Page, textboxSwiftCode, False)
        ControlMgr.SetEnableControl(Page, textboxIBAN_Number, False)
        ControlMgr.SetEnableControl(Page, txtBankLookupCode, False)
        ControlMgr.SetEnableControl(Page, txtBankSubcode, False)
        ControlMgr.SetEnableControl(Page, txtBankSortCode, False)
        ControlMgr.SetEnableControl(Page, txtTransLimit, False)
        ControlMgr.SetEnableControl(Page, txtTaxId, False)

        ControlMgr.SetEnableControl(Page, txtAddress1, False)
        ControlMgr.SetEnableControl(Page, txtAddress2, False)
        ControlMgr.SetEnableControl(Page, txtCity, False)
        ControlMgr.SetEnableControl(Page, txtPostalCode, False)

        'SwitchToLastFourDigitsLabelMode(Me.State.myBankInfoBo.ValidateFieldsforFR)

    End Sub



    Public Sub SetRequiredFieldsForDealerWithGiftCard(dealer As Dealer)
        If (dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
            If labelAddress1.Text.IndexOf("*") <> 0 Then labelAddress1.Text = "* " & labelAddress1.Text
            If labelCity.Text.IndexOf("*") <> 0 Then labelCity.Text = "* " & labelCity.Text
            If labelPostalCode.Text.IndexOf("*") <> 0 Then labelPostalCode.Text = "* " & labelPostalCode.Text

        End If
    End Sub

    Private Sub SwitchToLastFourDigitsLabelMode(value As Boolean)
        labelIBAN_Number_Last4Digits.Visible = value
        labelIBAN_Number.Visible = Not value

        labelBankAccountNo_Last4Digits.Visible = value
        labelBankAccountNo.Visible = Not value
    End Sub

    Public Sub SwitchToEditView()
        If State.myBankInfoBo.ValidateFieldsforFR Then
            ControlMgr.SetEnableControl(Page, textboxIBAN_Number, True)
            ControlMgr.SetEnableControl(Page, txtBankLookupCode, True)

            'textboxIBAN_Number.Text = String.Empty
            'txtBankLookupCode.Text = String.Empty

            'labelIBAN_Number_Last4Digits.Visible = False
            ' labelIBAN_Number.Visible = True
        End If
    End Sub
    Public Sub SwitchToReadOnlyView()
        If State.myBankInfoBo.ValidateFieldsforFR Then
            ControlMgr.SetEnableControl(Page, textboxIBAN_Number, False)
            ControlMgr.SetEnableControl(Page, txtBankLookupCode, False)
        End If
    End Sub

    Public Sub LabelTranslations()
        lblBankName.Text = TranslationBase.TranslateLabelOrMessage("BANK_NAME")
        labelNameonAccount.Text = TranslationBase.TranslateLabelOrMessage("NAME_ON_ACCOUNT")
        labelCountryOfBank.Text = TranslationBase.TranslateLabelOrMessage("COUNTRY_OF_BANK")
        lblBranchName.Text = TranslationBase.TranslateLabelOrMessage("BANK_BRANCH_NAME")
        labelBankID.Text = TranslationBase.TranslateLabelOrMessage("BANK_ID")
        lblBranchNumber.Text = TranslationBase.TranslateLabelOrMessage("BRANCH_NUMBER")
        lblBranchDigit.Text = TranslationBase.TranslateLabelOrMessage("BRANCH_DIGIT")
        labelBankAccountNo.Text = TranslationBase.TranslateLabelOrMessage("BANK_ACCOUNT_NO")
        labelBankAccountNo_Last4Digits.Text = TranslationBase.TranslateLabelOrMessage("BANK_ACCOUNT_NO_LAST4DIGITS")
        labelAccountType.Text = TranslationBase.TranslateLabelOrMessage("ACCOUNT_TYPE")
        lblAcctDigit.Text = TranslationBase.TranslateLabelOrMessage("ACCOUNT_DIGIT")
        labelSwiftCode.Text = TranslationBase.TranslateLabelOrMessage("SWIFT_CODE")
        labelIBAN_Number.Text = TranslationBase.TranslateLabelOrMessage("IBAN_NUMBER")
        labelIBAN_Number_Last4Digits.Text = TranslationBase.TranslateLabelOrMessage("IBAN_NUMBER_LAST4DIGITS")
        labelBanklookup.Text = TranslationBase.TranslateLabelOrMessage("BANK_LOOKUP_CODE")
        labelbanksubcode.Text = TranslationBase.TranslateLabelOrMessage("BANK_SUB_CODE")
        labelBankSortCode.Text = TranslationBase.TranslateLabelOrMessage("BANK_SORT_CODE")
        labelTranslimit.Text = TranslationBase.TranslateLabelOrMessage("TRANSACTION_LIMIT")
        labelTaxId.Text = TranslationBase.TranslateLabelOrMessage("TAX_ID")
        labelAddress1.Text = TranslationBase.TranslateLabelOrMessage("ADDRESS1")
        labelAddress2.Text = TranslationBase.TranslateLabelOrMessage("ADDRESS2")
        labelCity.Text = TranslationBase.TranslateLabelOrMessage("CITY")
        labelPostalCode.Text = TranslationBase.TranslateLabelOrMessage("POSTAL_CODE")
    End Sub
End Class

